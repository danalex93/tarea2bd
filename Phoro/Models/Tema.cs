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
        [Required(ErrorMessage = "Nombre requerido")]
        [StringLength(20, ErrorMessage = "Debe tener al menos 2 y máximo 20 caracteres.", MinimumLength=2)]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Mensaje requerido")]
        [StringLength(20, ErrorMessage = "Debe tener al menos 2 y máximo 20 caracteres.", MinimumLength = 2)]
        public string mensaje { get; set; }
        [Required]
        public bool publico { get; set; }

        public int commentsAmount()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return (db.Comentarios.Where(x => x.id_tema == this.id_tema).ToList().Count);
        }
    }
}