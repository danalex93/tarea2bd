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
    public class ComentarioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comentario
        public ActionResult Index()
        {
            var comentarios = db.Comentarios.Include(c => c.Tema).Include(c => c.Usuario);
            return View(comentarios.ToList());
        }

        // GET: Comentario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // GET: Comentario/Create/5
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Request.Cookies["UserSettings"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            ViewBag.id_tema = id;
            return View();
        }

        // POST: Comentario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, [Bind(Include = "id_comentario,id_tema,id_usuario,text")] Comentario comentario)
        {
            if (Request.Cookies["UserSettings"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            comentario.id_usuario = Convert.ToInt32(Request.Cookies["UserSettings"]["Id"]);
            comentario.id_tema = id;
            if (ModelState.IsValid)
            {
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Details","Tema",new {id = id});
            }

            ViewBag.id_tema = new SelectList(db.Tema, "id_tema", "nombre", comentario.id_tema);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", comentario.id_usuario);
            return View(comentario);
        }

        // GET: Comentario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_tema = new SelectList(db.Tema, "id_tema", "nombre", comentario.id_tema);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", comentario.id_usuario);
            return View(comentario);
        }

        // POST: Comentario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_comentario,id_tema,id_usuario,text")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_tema = new SelectList(db.Tema, "id_tema", "nombre", comentario.id_tema);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", comentario.id_usuario);
            return View(comentario);
        }

        // GET: Comentario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // POST: Comentario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentario);
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
