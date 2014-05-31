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

        public int topicsCount()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return (db.Tema.Where(x => x.id_categoria == this.id_categoria).ToList().Count);
        }

        public Tema lastComment()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var Temas = db.Tema
                        .Where(x => x.id_categoria == this.id_categoria)
                        .ToList();
            int lastId = 0;
            int lastTema = -1;
            if (Temas.Count != 0)
            {
                foreach (var element in Temas)
                {
                    var Comentarios = db.Comentarios
                                     .Where(x => x.id_tema == element.id_tema)
                                    .ToList();
                    foreach (var comment in Comentarios)
                    {
                        if (comment.id_comentario >= lastId)
                        {
                            lastId = comment.id_comentario;
                            lastTema = comment.id_tema;
                        }
                    }
                }
                if (lastTema != -1)
                {
                    Tema tema = db.Tema.Find(lastTema);
                    return tema;
                }
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
    }
}