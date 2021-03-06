﻿using System;
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

        // GET: Tema
        public ActionResult Index()
        {
            var g = getUserGroup();
            if (g != null)
            {
                ViewBag.show_create = g.creacion_tema;
                ViewBag.show_details = true;
                ViewBag.show_edit = true;
                ViewBag.show_delete = g.eliminar_tema;
            }
            else
            {
                ViewBag.show_create = false;
                ViewBag.show_details = true;
                ViewBag.show_edit = true;
                ViewBag.show_delete = false;
            }
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
            ViewBag.Comentarios = db.Comentarios
                                .Where(x => x.id_tema == id)
                                .ToList();
            return View(tema);
        }

        // GET: Tema/Create/5
        public ActionResult Create(int? id)
        {
            var g = getUserGroup();
            if (g == null || !(g.creacion_tema))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.id_categoria = id;
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre");
            return View();
        }

        // POST: Tema/Create/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, [Bind(Include = "id_tema,id_categoria,id_usuario,nombre,mensaje,publico")] Tema tema)
        {
            var g = getUserGroup();
            if (g == null || !(g.creacion_tema))
            {
                return RedirectToAction("Index", "Home");
            }
            tema.id_categoria = id;
            if (ModelState.IsValid)
            {
                db.Tema.Add(tema);
                db.SaveChanges();
                return RedirectToAction("Details","Categoria", new{id = id});
            }

            ViewBag.id_categoria = id;
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
            var g = getUserGroup();
            if (g == null || !(g.eliminar_tema))
            {
                return RedirectToAction("Index", "Home");
            }
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
            var g = getUserGroup();
            if (g == null || !(g.eliminar_tema))
            {
                return RedirectToAction("Index", "Home");
            }
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
