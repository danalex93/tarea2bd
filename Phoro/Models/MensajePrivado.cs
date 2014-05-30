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
        public int id_remitente { get; set; }
        public virtual Usuario Remitente { get; set; }
        [ForeignKey("Buzon")]
        public int id_buzon { get; set; }
        public virtual BuzonEntrada Buzon { get; set; }
        public bool leido { get; set; }
        public string mensaje { get; set; }
        public DateTime fecha_de_envio { get; set; }
    }
}