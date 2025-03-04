using System.Collections.Generic;
using MS_CITAS.Domain.Models;

namespace MS_CITAS.Interfaces
{
    public interface ICitaRepository
    {
        IEnumerable<Citas> GetAll();
        Citas GetById(int id);
        void Add(Citas cita);
        void Update(Citas cita);
        void Delete(Citas cita);
        bool Exists(int id);
     
    }
}