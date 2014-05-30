using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Phoro.Models
{
    public class BuzonEntrada
    {
        [Key]
        public int id_buzon { get; set; }
        [ForeignKey("Usuario")]
        public int id_usuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int mensajes { get; set; }
        public int mensajes_sin_leer { get; set; }
    }
}