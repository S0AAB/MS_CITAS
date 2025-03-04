using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS_CITAS.Application.Dto
{
    public class CitaDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Lugar { get; set; }
        public string Estado { get; set; }
        public string Motivo { get; set; }
        public string Notas { get; set; }
    }
}