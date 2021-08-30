using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaCobros.Models;

namespace SistemaCobros.Views
{
    public class GestionController : Controller
    {
        private sistema_cobrosEntities db = new sistema_cobrosEntities();

        // GET: Gestion
        public ActionResult Index()
        {
            var gestion = db.Gestion.Include(g => g.Cartera).Include(g => g.Usuario);
            return View(gestion.ToList());
        }

        // GET: Gestion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestion gestion = db.Gestion.Find(id);
            if (gestion == null)
            {
                return HttpNotFound();
            }
            return View(gestion);
        }

        // GET: Gestion/Create
        public ActionResult Create()
        {
            ViewBag.id_cartera = new SelectList(db.Cartera, "id_cartera", "id_cartera");
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre");
            return View();
        }

        // POST: Gestion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_gestion,id_usuario,id_cartera")] Gestion gestion)
        {
            if (ModelState.IsValid)
            {
                db.Gestion.Add(gestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cartera = new SelectList(db.Cartera, "id_cartera", "id_cartera", gestion.id_cartera);
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre", gestion.id_usuario);
            return View(gestion);
        }

        // GET: Gestion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestion gestion = db.Gestion.Find(id);
            if (gestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cartera = new SelectList(db.Cartera, "id_cartera", "id_cartera", gestion.id_cartera);
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre", gestion.id_usuario);
            return View(gestion);
        }

        // POST: Gestion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_gestion,id_usuario,id_cartera")] Gestion gestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cartera = new SelectList(db.Cartera, "id_cartera", "id_cartera", gestion.id_cartera);
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre", gestion.id_usuario);
            return View(gestion);
        }

        // GET: Gestion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestion gestion = db.Gestion.Find(id);
            if (gestion == null)
            {
                return HttpNotFound();
            }
            return View(gestion);
        }

        // POST: Gestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gestion gestion = db.Gestion.Find(id);
            db.Gestion.Remove(gestion);
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
