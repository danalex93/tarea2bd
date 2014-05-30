using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Phoro.Models;

namespace Phoro.Controllers
{
    public class GrupoUsuarioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GrupoUsuario
        public ActionResult Index()
        {
            return View(db.GrupoUsuarios.ToList());
        }

        // GET: GrupoUsuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoUsuario grupoUsuario = db.GrupoUsuarios.Find(id);
            if (grupoUsuario == null)
            {
                return HttpNotFound();
            }
            return View(grupoUsuario);
        }

        // GET: GrupoUsuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GrupoUsuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_grupo,nombre_grupo,creacion_categoria,eliminar_categoria,creacion_tema,eliminar_tema,publicar_comentario,eliminar_mensaje,editar_mensaje,editar_usuario")] GrupoUsuario grupoUsuario)
        {
            if (ModelState.IsValid)
            {
                db.GrupoUsuarios.Add(grupoUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupoUsuario);
        }

        // GET: GrupoUsuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoUsuario grupoUsuario = db.GrupoUsuarios.Find(id);
            if (grupoUsuario == null)
            {
                return HttpNotFound();
            }
            return View(grupoUsuario);
        }

        // POST: GrupoUsuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_grupo,nombre_grupo,creacion_categoria,eliminar_categoria,creacion_tema,eliminar_tema,publicar_comentario,eliminar_mensaje,editar_mensaje,editar_usuario")] GrupoUsuario grupoUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupoUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupoUsuario);
        }

        // GET: GrupoUsuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoUsuario grupoUsuario = db.GrupoUsuarios.Find(id);
            if (grupoUsuario == null)
            {
                return HttpNotFound();
            }
            return View(grupoUsuario);
        }

        // POST: GrupoUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GrupoUsuario grupoUsuario = db.GrupoUsuarios.Find(id);
            db.GrupoUsuarios.Remove(grupoUsuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
