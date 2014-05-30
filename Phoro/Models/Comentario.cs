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
        public string text { get; set; }
    }
}