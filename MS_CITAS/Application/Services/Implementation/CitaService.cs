using MS_CITAS.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MS_CITAS.Domain.Models;
using System;
using MS_CITAS.Application.Dto;
using MS_CITAS.Application.Services.Implementation;
using AutoMapper;

namespace MS_CITAS.Services
{
    public class CitaService : ICitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly PersonaServiceAPI _personaServiceAPI;
        private readonly EmisorMQ _emisorMQ;
        private readonly IMapper _mapper;

        // Inyección de dependencias
        public CitaService(ICitaRepository citaRepository, PersonaServiceAPI personaServiceAPI, EmisorMQ emisorMQ, IMapper mapper)
        {
            _citaRepository = citaRepository;
            _personaServiceAPI = personaServiceAPI;
            _emisorMQ = emisorMQ;
            _mapper = mapper;
        }

        public IEnumerable<CitaDto> GetAll()
        {
            var citas = _citaRepository.GetAll();
            return _mapper.Map<IEnumerable<CitaDto>>(citas);
        }

        public CitaDto GetById(int id)
        {
            var cita = _citaRepository.GetById(id);
            if (cita == null)
                throw new KeyNotFoundException("Cita no encontrada.");

            return _mapper.Map<CitaDto>(cita);
        }

        public async Task<bool> Create(CitaDto citaDto)
        {
            if (!await ValidarPersona(citaDto.PacienteId, citaDto.MedicoId))
                return false;

            var cita = _mapper.Map<Citas>(citaDto);
            _citaRepository.Add(cita);
            return true;
        }

        public async Task<bool> Update(int id, CitaDto citaDto)
        {
            if (!_citaRepository.Exists(id))
                return false;

            if (!await ValidarPersona(citaDto.PacienteId, citaDto.MedicoId))
                return false;

            var citaExistente = _citaRepository.GetById(id);
            _mapper.Map(citaDto, citaExistente);  // Mapear DTO sobre la entidad existente

            _citaRepository.Update(citaExistente);
            return true;
        }

        public async Task<bool> FinalizarCita(int id, RecetaDto receta)
        {
            var cita = _citaRepository.GetById(id);
            if (cita == null || receta == null)
                return false;

            cita.Estado = "Finalizada";
            _citaRepository.Update(cita);

            // Enviar receta a RabbitMQ
            try
            {
                await _emisorMQ.PublicarMensaje(receta);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar receta: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            if (!_citaRepository.Exists(id))
                return false;

            var cita = _citaRepository.GetById(id);
            _citaRepository.Delete(cita);
            return true;
        }

        private async Task<bool> ValidarPersona(int pacienteId, int medicoId)
        {
            var paciente = await _personaServiceAPI.ObtenerPersona(pacienteId);
            var medico = await _personaServiceAPI.ObtenerPersona(medicoId);

            Debug.WriteLine($"Paciente: {JsonConvert.SerializeObject(paciente)}");
            Debug.WriteLine($"Médico: {JsonConvert.SerializeObject(medico)}");

            return paciente != null && medico != null &&
                   paciente.TipoPersonaId == 2 && medico.TipoPersonaId == 1 &&
                   paciente.Activo && medico.Activo;
        }
    }
}
