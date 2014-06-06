using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Phoro.Models
{
    public class MensajePrivado
    {
        [Key]
        public int id_mensaje { get; set; }
        [ForeignKey("Remitente")]
        [Required]
        public int id_remitente { get; set; }
        public virtual Usuario Remitente { get; set; }
        [ForeignKey("Buzon")]
        public int id_buzon { get; set; }
        public virtual BuzonEntrada Buzon { get; set; }
        public bool leido { get; set; }
        [Required(ErrorMessage = "Asunto requerido")]
        [StringLength(20, ErrorMessage = "Debe tener al menos 2 y máximo 20 caracteres.", MinimumLength = 2)]
        public string asunto { get; set; }
        [Required(ErrorMessage = "Mensaje requerido")]
        [StringLength(20, ErrorMessage = "Debe tener al menos 2 y máximo 20 caracteres.", MinimumLength = 2)]
        public string mensaje { get; set; }
        public DateTime fecha_de_envio { get; set; }
    }
}