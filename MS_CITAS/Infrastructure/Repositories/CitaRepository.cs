using MS_CITAS.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MS_CITAS.Domain.Models;

namespace MS_CITAS.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        //Contexto
        private readonly CitasDBEntities _context;

        //Inyeccion de dependencias
        public CitaRepository(CitasDBEntities context) {
            _context = context;
        }


        public IEnumerable<Citas> GetAll()
        {
            return _context.Citas.ToList();
        }


        public Citas GetById(int id)
        {
           return _context.Citas.Find(id);
        }

        public void Add(Citas cita)
        {
            _context.Citas.Add(cita);
            _context.SaveChanges();
        }

        public void Update(Citas cita)
        {
            _context.Entry(cita).State= EntityState.Modified;
            _context.SaveChanges();

        }

        public void Delete(Citas cita)
        {
            
           _context.Citas.Remove(cita);
           _context.SaveChanges();
            
        }

        public bool Exists(int id)
        {
            return _context.Citas.Any(e => e.Id == id);
        }





    }
}