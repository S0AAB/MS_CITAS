using MS_CITAS.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;
using MS_CITAS.Models;
namespace MS_CITAS.Controllers
{
    
    public class CitaController : ApiController
    {
        private readonly ICitaService _citaService;
        

        //Inyeccion de dependencias
        public CitaController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        // GET: api/Citas
        [HttpGet]
        public IEnumerable<Citas> GetCitas()
        {
            return _citaService.GetAll();
           
        }


        // GET: api/Citas/{id}
        [HttpGet]
        public IHttpActionResult GetCitas(int id)
        {
            var cita = _citaService.GetById(id);
            if (cita == null)
                return NotFound();

            return Ok(cita);
        }

        // POST: api/Citas
        [HttpPost]
        public async Task<IHttpActionResult> PostCita(Citas nuevaCita)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool creado = await _citaService.Create(nuevaCita);

            if (!creado)
                return BadRequest("No se pudo crear la cita.");

            //Devuelve la  cita creada si es correcto
            return CreatedAtRoute("DefaultApi", new { id = nuevaCita.Id }, nuevaCita);
        }

        // PUT: api/Citas/{id}
        [HttpPut]
        public IHttpActionResult PutCitas(int id, Citas citaActualizada)
        {   
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool actualizado = _citaService.Update(id, citaActualizada);
            if (!actualizado)

                return NotFound();

            return Ok("Cita actualizada exitosamente.");
        }

        // DELETE: api/citas/{id}
        [HttpDelete]
        public IHttpActionResult DeleteCitas(int id)
        {
            bool eliminado = _citaService.Delete(id);
            if (!eliminado)
                return NotFound();

            return Ok("Cita eliminada exitosamente.");
        }
    }
}
