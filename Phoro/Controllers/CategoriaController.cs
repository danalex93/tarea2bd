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
    public class CategoriaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private bool isLogged()
        {
            try
            {
                if (!(Request.Cookies["UserSettings"]["Id"] is String))
                {
                    throw new Exception();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private GrupoUsuario getUserGroup()
        {
            try
            {
                var id = int.Parse(Request.Cookies["UserSettings"]["Grupo"]);
                return db.GrupoUsuarios.Find(id);
            }
            catch { }
            return null;
        }

        // GET: Categoria
        public ActionResult Index()
        {
            if (!isLogged())
            {
                return RedirectToAction("Login", "Usuario");
            }
            var g = getUserGroup();
            if (g != null)
            {
                ViewBag.show_create = g.creacion_categoria;
                ViewBag.show_details = true;
                ViewBag.show_edit = true;
                ViewBag.show_delete = g.eliminar_categoria;
            }
            else
            {
                ViewBag.show_create = false;
                ViewBag.show_details = true;
                ViewBag.show_edit = true;
                ViewBag.show_delete = false;
            }
            return View(db.Categorias.ToList());
        }

        // GET: Categoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.Temas = db.Tema
                                .Where(x => x.id_categoria == id)
                                .ToList();
            return View(categoria);
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
            var g = getUserGroup();
            if (g == null || !(g.creacion_categoria)) {
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        // POST: Categoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_categoria,nombre,descripcion,publico")] Categoria categoria)
        {
            var g = getUserGroup();
            if (g == null || !(g.creacion_categoria))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Categorias.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_categoria,nombre,descripcion,publico")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categoria/Delete/5
        public ActionResult Delete(int? id)
        {
            var g = getUserGroup();
            if (g == null || !(g.eliminar_categoria))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var g = getUserGroup();
            if (g == null || !(g.eliminar_categoria))
            {
                return RedirectToAction("Index", "Home");
            }
            Categoria categoria = db.Categorias.Find(id);
            db.Categorias.Remove(categoria);
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
