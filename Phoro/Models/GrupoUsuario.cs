using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phoro.Models
{
    public class GrupoUsuario
    {
        [Key]
        public int id_grupo { get; set; }
        public string nombre_grupo { get; set; }
        public bool creacion_categoria { get; set; }
        public bool eliminar_categoria { get; set; }
        public bool creacion_tema { get; set; }
        public bool eliminar_tema { get; set; }
        public bool publicar_comentario { get; set; }
        public bool eliminar_mensaje { get; set; }
        public bool editar_mensaje { get; set; }
        public bool editar_usuario { get; set; }        
    }
}