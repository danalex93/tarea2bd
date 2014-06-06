using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Phoro.Models
{
    public class Comentario
    {
        [Key]
        public int id_comentario { get; set; }
        [ForeignKey("Tema")]
        public int id_tema { get; set; }
        public virtual Tema Tema { get; set; }
        [ForeignKey("Usuario")]
        public int id_usuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        [Required(ErrorMessage = "Texto requerido")]
        [StringLength(20, ErrorMessage = "Debe tener al menos 2 y máximo 20 caracteres.", MinimumLength = 2)]
        public string text { get; set; }
    }
}