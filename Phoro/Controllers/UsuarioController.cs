﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Phoro.Models;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Phoro.Controllers
{
    public class UsuarioController : Controller
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

        // GET: Usuario
        public ActionResult Index()
        {
            var g = getUserGroup();
            if (g != null)
            {
                ViewBag.show_create = true;
                ViewBag.show_details = true;
                ViewBag.show_edit = g.editar_usuario;
                ViewBag.show_delete = true;
            }
            else
            {
                ViewBag.show_create = true;
                ViewBag.show_details = true;
                ViewBag.show_edit = false;
                ViewBag.show_delete = true;
            }
            var usuarios = db.Usuarios.Include(u => u.Grupo);
            return View(usuarios.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_usuario,id_grupo,nombre,contrasena,cantidad_comentarios,avatar_url,fecha_nacimiento,sexo,fecha_registro")] Usuario usuario)
        {
            usuario.id_grupo = 3;
            usuario.fecha_registro = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                var buzon = new BuzonEntrada();
                buzon.id_usuario = usuario.id_usuario;
                buzon.mensajes = 0;
                buzon.mensajes_sin_leer = 0;
                try
                {
                    db.Usuarios.Add(usuario);
                    db.BuzonEntradas.Add(buzon);
                    db.SaveChanges();
                }
                catch
                {
                    ViewBag.Error = "Usuario/Contraseña erroneos";
                    return View(usuario);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            var g = getUserGroup();
            if (g == null || !(g.editar_usuario))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_grupo = new SelectList(db.GrupoUsuarios, "id_grupo", "nombre_grupo", usuario.id_grupo);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_usuario,id_grupo,nombre,contrasena,cantidad_comentarios,avatar_url,fecha_nacimiento,sexo,fecha_registro")] Usuario usuario)
        {
            var g = getUserGroup();
            if (g == null || !(g.editar_usuario))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_grupo = new SelectList(db.GrupoUsuarios, "id_grupo", "nombre_grupo", usuario.id_grupo);
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Usuario/Login
        public ActionResult Login()
        {
            if (Request.Cookies["UserSettings"] != null)
            {
                return RedirectToAction("Index", "Categoria");
            }
            return View();
        }



        [HttpPost, ActionName("Login")]
        public ActionResult Login(int? id)
        {
            NameValueCollection postData = Request.Form;
            string user, pass;
            if (!string.IsNullOrEmpty(postData["user"]))
            {
                user = postData["user"];
            }
            else
                user = "";

            if (!string.IsNullOrEmpty(postData["pass"]))
            {
                pass = postData["pass"];
            }
            else
                pass = "";

            //Process login
            System.Diagnostics.Debug.Write("user: " + user + "pass: " + pass);
            int loginResult = loginAction(user,pass);
            
            if (loginResult != -1)
            {
                var User = db.Usuarios.Find(loginResult);
                HttpCookie cookie = new HttpCookie("UserSettings");
                cookie["Nombre"] = user;
                cookie["Grupo"] = User.Grupo.id_grupo.ToString();
                cookie["Id"] = User.id_usuario.ToString();
                cookie.Expires = DateTime.Now.AddDays(1d);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Categoria");
            }
            else
            {
                ViewBag.Error = "Usuario/Contraseña erroneo";
                return View();
            }
        }
        
        private int loginAction(string user, string pass)
        {
            try
            {
                var temp = db.Usuarios
                         .Where(x => x.nombre == user)
                         .First();
                Usuario User = db.Usuarios.Find(temp.id_usuario);
                if (User.contrasena == pass)
                    return User.id_usuario;
                else
                    return -1;
            } catch {
                return -1;
            }

        }

        // GET: Usuario/Logout
        public ActionResult Logout()
        {
            if (Request.Cookies["UserSettings"] != null)
            {
                HttpCookie cookie = new HttpCookie("UserSettings");
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Home");
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
