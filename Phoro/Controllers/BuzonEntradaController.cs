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
    public class BuzonEntradaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BuzonEntrada
        public ActionResult Index()
        {
            var buzonEntradas = db.BuzonEntradas.Include(b => b.Usuario);
            return View(buzonEntradas.ToList());
        }

        // GET: BuzonEntrada/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuzonEntrada buzonEntrada = db.BuzonEntradas.Find(id);
            if (buzonEntrada == null)
            {
                return HttpNotFound();
            }
            return View(buzonEntrada);
        }

        // GET: BuzonEntrada/Create
        public ActionResult Create()
        {
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre");
            return View();
        }

        // POST: BuzonEntrada/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_buzon,id_usuario,mensajes,mensajes_sin_leer")] BuzonEntrada buzonEntrada)
        {
            if (ModelState.IsValid)
            {
                db.BuzonEntradas.Add(buzonEntrada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", buzonEntrada.id_usuario);
            return View(buzonEntrada);
        }

        // GET: BuzonEntrada/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuzonEntrada buzonEntrada = db.BuzonEntradas.Find(id);
            if (buzonEntrada == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", buzonEntrada.id_usuario);
            return View(buzonEntrada);
        }

        // POST: BuzonEntrada/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_buzon,id_usuario,mensajes,mensajes_sin_leer")] BuzonEntrada buzonEntrada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buzonEntrada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", buzonEntrada.id_usuario);
            return View(buzonEntrada);
        }

        // GET: BuzonEntrada/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuzonEntrada buzonEntrada = db.BuzonEntradas.Find(id);
            if (buzonEntrada == null)
            {
                return HttpNotFound();
            }
            return View(buzonEntrada);
        }

        // POST: BuzonEntrada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BuzonEntrada buzonEntrada = db.BuzonEntradas.Find(id);
            db.BuzonEntradas.Remove(buzonEntrada);
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
