using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MS_CITAS.Models;

namespace MS_CITAS.Interfaces
{
	public interface ICitaService
	{
        IEnumerable<Citas> GetAll();
        Citas GetById(int id);
        Task<bool> Create(Citas cita);
        bool Update(int id, Citas cita);
        bool Delete(int id);

    }
}