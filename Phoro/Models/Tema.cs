using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Phoro.Models
{
    public class Tema
    {
        [Key]
        public int id_tema { get; set; }
        [ForeignKey("Categoria")]
        public int id_categoria { get; set; }
        public virtual Categoria Categoria { get; set; }
        [ForeignKey("Usuario")]
        public int id_usuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public string nombre { get; set; }
        public string mensaje { get; set; }
        public bool publico { get; set; }        
    }
}