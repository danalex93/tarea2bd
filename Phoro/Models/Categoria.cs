using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phoro.Models
{
    public class Categoria
    {
        [Key]
        public int id_categoria { get; set;  }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool publico { get; set; }
    }
}