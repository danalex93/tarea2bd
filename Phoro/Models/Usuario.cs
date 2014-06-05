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

        public LinkedList<Tema> last5Topics()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            LinkedList<Tema> list = new LinkedList<Tema>();
            int forMax = 5;
            try
            {
                var Temas = db.Tema
                        .Where(x => x.id_usuario == this.id_usuario)
                        .OrderByDescending(x => x.id_tema)
                        .ToList();
                if (Temas.Count < 5)
                {
                    forMax = Temas.Count;
                }
                for (int i = 0; i < 5; i++)
                {
                    list.AddLast(Temas[i]);
                }
            }
            catch
            {

            }
            return list;
        }

        public LinkedList<Comentario> last5Comments()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            LinkedList<Comentario> list = new LinkedList<Comentario>();
            int forMax = 5;
            try
            {
                var Comentarios = db.Comentarios
                        .Where(x => x.id_usuario == this.id_usuario)
                        .OrderByDescending(x => x.id_tema)
                        .ToList();
                if (Comentarios.Count < 5)
                {
                    forMax = Comentarios.Count;
                }
                for (int i = 0; i < forMax; i++)
                {
                    list.AddLast(Comentarios[i]);
                }
            }
            catch
            {
                list.Clear();
            }
            return list;
        }

        public int commentsMade()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            int Amount = 0;
            try
            {
                Amount = db.Comentarios
                        .Where(x => x.id_usuario == this.id_usuario)
                        .OrderByDescending(x => x.id_tema)
                        .ToList().Count;
            }
            catch
            {

            }
            return Amount;
        }

        public int getBuzon()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var result = db.BuzonEntradas
                .Where(x => x.Usuario.id_usuario == this.id_usuario).First();
            return result.id_buzon;
        }

    }
}