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
    public class TemaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tema
        public ActionResult Index()
        {
            var Tema = db.Tema.Include(t => t.Categoria).Include(t => t.Usuario);
            return View(Tema.ToList());
        }

        // GET: Tema/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tema tema = db.Tema.Find(id);
            if (tema == null)
            {
                return HttpNotFound();
            }
            return View(tema);
        }

        // GET: Tema/Create
        public ActionResult Create()
        {
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "nombre");
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre");
            return View();
        }

        // POST: Tema/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tema,id_categoria,id_usuario,nombre,mensaje,publico")] Tema tema)
        {
            if (ModelState.IsValid)
            {
                db.Tema.Add(tema);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "nombre", tema.id_categoria);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", tema.id_usuario);
            return View(tema);
        }

        // GET: Tema/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tema tema = db.Tema.Find(id);
            if (tema == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "nombre", tema.id_categoria);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", tema.id_usuario);
            return View(tema);
        }

        // POST: Tema/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tema,id_categoria,id_usuario,nombre,mensaje,publico")] Tema tema)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tema).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "nombre", tema.id_categoria);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", tema.id_usuario);
            return View(tema);
        }

        // GET: Tema/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tema tema = db.Tema.Find(id);
            if (tema == null)
            {
                return HttpNotFound();
            }
            return View(tema);
        }

        // POST: Tema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tema tema = db.Tema.Find(id);
            db.Tema.Remove(tema);
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
