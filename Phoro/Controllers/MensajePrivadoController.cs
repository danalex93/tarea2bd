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
    public class MensajePrivadoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MensajePrivado
        public ActionResult Index()
        {
            var mensajePrivadoes = db.MensajePrivadoes.Include(m => m.Buzon).Include(m => m.Remitente);
            return View(mensajePrivadoes.ToList());
        }

        // GET: MensajePrivado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MensajePrivado mensajePrivado = db.MensajePrivadoes.Find(id);
            if (mensajePrivado == null)
            {
                return HttpNotFound();
            }
            return View(mensajePrivado);
        }

        // GET: MensajePrivado/Create
        public ActionResult Create()
        {
            ViewBag.id_buzon = new SelectList(db.BuzonEntradas, "id_buzon", "id_buzon");
            ViewBag.id_remitente = new SelectList(db.Usuarios, "id_usuario", "nombre");
            return View();
        }

        // POST: MensajePrivado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_mensaje,id_remitente,id_buzon,leido,mensaje,fecha_de_envio")] MensajePrivado mensajePrivado)
        {
            if (ModelState.IsValid)
            {
                db.MensajePrivadoes.Add(mensajePrivado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_buzon = new SelectList(db.BuzonEntradas, "id_buzon", "id_buzon", mensajePrivado.id_buzon);
            ViewBag.id_remitente = new SelectList(db.Usuarios, "id_usuario", "nombre", mensajePrivado.id_remitente);
            return View(mensajePrivado);
        }

        // GET: MensajePrivado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MensajePrivado mensajePrivado = db.MensajePrivadoes.Find(id);
            if (mensajePrivado == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_buzon = new SelectList(db.BuzonEntradas, "id_buzon", "id_buzon", mensajePrivado.id_buzon);
            ViewBag.id_remitente = new SelectList(db.Usuarios, "id_usuario", "nombre", mensajePrivado.id_remitente);
            return View(mensajePrivado);
        }

        // POST: MensajePrivado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_mensaje,id_remitente,id_buzon,leido,mensaje,fecha_de_envio")] MensajePrivado mensajePrivado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensajePrivado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_buzon = new SelectList(db.BuzonEntradas, "id_buzon", "id_buzon", mensajePrivado.id_buzon);
            ViewBag.id_remitente = new SelectList(db.Usuarios, "id_usuario", "nombre", mensajePrivado.id_remitente);
            return View(mensajePrivado);
        }

        // GET: MensajePrivado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MensajePrivado mensajePrivado = db.MensajePrivadoes.Find(id);
            if (mensajePrivado == null)
            {
                return HttpNotFound();
            }
            return View(mensajePrivado);
        }

        // POST: MensajePrivado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MensajePrivado mensajePrivado = db.MensajePrivadoes.Find(id);
            db.MensajePrivadoes.Remove(mensajePrivado);
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
