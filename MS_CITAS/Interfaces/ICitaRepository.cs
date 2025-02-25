using System.Collections.Generic;
using MS_CITAS.Models;

namespace MS_CITAS.Interfaces
{
    public interface ICitaRepository
    {
        IEnumerable<Citas> GetAll();
        Citas GetById(int id);
        void Add(Citas cita);
        void Update(Citas cita);
        void Delete(int id);
        bool Exists(int id);
     
    }
}