using System;
using System.ComponentModel.DataAnnotations;


namespace MS_CITAS.Application.Dto
{
	public class RecetaDto
	{

        public int RecetaId { get; set; }

        [Required]
        public string CodigoUnico { get; set; } 

        [Required(ErrorMessage = "El PacienteId es obligatorio.")]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "La Descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La Fecha de Creación es obligatoria.")]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaVencimiento { get; set; } // Puede ser opcional

        [Required(ErrorMessage = "El EstadoId es obligatorio.")]
        public int EstadoId { get; set; }
    }
}