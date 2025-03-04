using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MS_CITAS.Application.Dto;
using MS_CITAS.Domain.Models;

namespace MS_CITAS.Interfaces
{
	public interface ICitaService
	{
        IEnumerable<CitaDto> GetAll();
        CitaDto GetById(int id);
        Task<bool> Create(CitaDto cita);
        Task<bool> Update(int id, CitaDto cita);
        bool Delete(int id);
        Task<bool> FinalizarCita(int id, RecetaDto receta);

    }
}