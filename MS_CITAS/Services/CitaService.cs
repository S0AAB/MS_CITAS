using MS_CITAS.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MS_CITAS.Models;

namespace MS_CITAS.Services
{
	public class CitaService: ICitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly PersonaServiceAPI _personaServiceAPI;

        //Inyeccion de dependencias

        public CitaService(ICitaRepository citaRepository ,PersonaServiceAPI personaServiceAPI)
        {
            _citaRepository = citaRepository;
            _personaServiceAPI = personaServiceAPI;
        }



        public IEnumerable<Citas> GetAll()
        {
            return _citaRepository.GetAll();
        }

        public Citas GetById(int id)
        {
            return _citaRepository.GetById(id);
        }


        public async Task<bool> Create(Citas cita)
        {

            if (!await ValidarPersona(cita.PacienteId, cita.MedicoId))
            {
                return false;
            }

            _citaRepository.Add(cita);
            return true;
        }



        public bool Update(int id, Citas persona)
        {
            if (!_citaRepository.Exists(id))
                return false;

            _citaRepository.Update(persona);
            return true;
        }

        public bool Delete(int id)
        {
            if (!_citaRepository.Exists(id))
            {
                return false;
            }

            _citaRepository.Delete(id);
            return true;
        }




        private async Task<bool> ValidarPersona(int pacienteId, int medicoId)
        {
            var paciente = await _personaServiceAPI.ObtenerPersona(pacienteId);
            var medico = await _personaServiceAPI.ObtenerPersona(medicoId);

            Debug.WriteLine($"Paciente: {JsonConvert.SerializeObject(paciente)}");
            Debug.WriteLine($"Médico: {JsonConvert.SerializeObject(medico)}");

            // Validar que existan, que sean del tipo correcto (pacient y medico) y que estén activos
            if (paciente == null || medico == null || paciente.TipoPersonaId != 2 || medico.TipoPersonaId != 1 || paciente.Activo!=true || medico.Activo!=true)
            {
                return false;
            }

            return true;
        }





    }
}