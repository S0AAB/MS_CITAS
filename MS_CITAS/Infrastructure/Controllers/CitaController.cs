using MS_CITAS.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using MS_CITAS.Domain.Models;
using MS_CITAS.Application.Dto;
namespace MS_CITAS.Controllers
{
    public class CitaController : ApiController
    {
        private readonly ICitaService _citaService;

        // Inyección de dependencias
        public CitaController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpGet]
        public IHttpActionResult GetCitas()
        {
            return Ok(_citaService.GetAll());
        }

        [HttpGet]
        public IHttpActionResult GetCitas(int id)
        {
            try
            {
                return Ok(_citaService.GetById(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostCita(CitaDto nuevaCita)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _citaService.Create(nuevaCita))
                return BadRequest("No se pudo crear la cita.");

            return CreatedAtRoute("DefaultApi", new { id = nuevaCita.Id }, nuevaCita);
        }

        [HttpPost]
        [Route("api/Cita/finalizar/{id}")]
        public async Task<IHttpActionResult> FinalizarCita(int id, [FromBody] RecetaDto receta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _citaService.FinalizarCita(id, receta))
            {
                return Ok("Cita finalizada y receta enviada.");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> PutCitas(int id, CitaDto citaActualizada)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            if (await _citaService.Update(id, citaActualizada))
            {
                return Ok("Cita actualizada exitosamente.");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteCitas(int id)
        {
            if (_citaService.Delete(id))
            {
                return Ok("Cita eliminada exitosamente.");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
