using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Phoro.Models
{
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }
        [ForeignKey("Grupo")]
        public int id_grupo { get; set; }
        public virtual GrupoUsuario Grupo { get; set; }
        public string nombre { get; set; }
        public string contrasena { get; set; }
        public int cantidad_comentarios { get; set; }
        public string avatar_url { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string sexo { get; set; }
        public DateTime fecha_registro { get; set; }

    }
}