using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaCobros.Models.ViewModel;

namespace SistemaCobros.Views
{
    public class Detalle_GestionController : Controller
    {
        private Models.ViewModel.sistema_cobrosEntities1 db = new Models.ViewModel.sistema_cobrosEntities1();

        // GET: Detalle_Gestion
        public ActionResult Index()
        {
            var detalle_Gestion = db.Detalle_Gestion.Include(d => d.Gestion).Include(d => d.Tipo_Gestion);
            return View(detalle_Gestion.ToList());
        }

        // GET: Detalle_Gestion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_Gestion detalle_Gestion = db.Detalle_Gestion.Find(id);
            if (detalle_Gestion == null)
            {
                return HttpNotFound();
            }
            return View(detalle_Gestion);
        }

        // GET: Detalle_Gestion/Create
        public ActionResult Create()
        {
            ViewBag.id_gestion = new SelectList(db.Gestion, "id_gestion", "id_gestion");
            ViewBag.id_tipo_gestion = new SelectList(db.Tipo_Gestion, "id_tipo_gestion", "descripcion");
            return View();
        }

        // POST: Detalle_Gestion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_detalle_gestion,descripcion,fecha,hora,id_gestion,id_tipo_gestion,comentario")] Detalle_Gestion detalle_Gestion)
        {
            if (ModelState.IsValid)
            {
                db.Detalle_Gestion.Add(detalle_Gestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_gestion = new SelectList(db.Gestion, "id_gestion", "id_gestion", detalle_Gestion.id_gestion);
            ViewBag.id_tipo_gestion = new SelectList(db.Tipo_Gestion, "id_tipo_gestion", "descripcion", detalle_Gestion.id_tipo_gestion);
            return View(detalle_Gestion);
        }

        // GET: Detalle_Gestion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_Gestion detalle_Gestion = db.Detalle_Gestion.Find(id);
            if (detalle_Gestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_gestion = new SelectList(db.Gestion, "id_gestion", "id_gestion", detalle_Gestion.id_gestion);
            ViewBag.id_tipo_gestion = new SelectList(db.Tipo_Gestion, "id_tipo_gestion", "descripcion", detalle_Gestion.id_tipo_gestion);
            return View(detalle_Gestion);
        }

        // POST: Detalle_Gestion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_detalle_gestion,descripcion,fecha,hora,id_gestion,id_tipo_gestion,comentario")] Detalle_Gestion detalle_Gestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle_Gestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_gestion = new SelectList(db.Gestion, "id_gestion", "id_gestion", detalle_Gestion.id_gestion);
            ViewBag.id_tipo_gestion = new SelectList(db.Tipo_Gestion, "id_tipo_gestion", "descripcion", detalle_Gestion.id_tipo_gestion);
            return View(detalle_Gestion);
        }

        // GET: Detalle_Gestion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_Gestion detalle_Gestion = db.Detalle_Gestion.Find(id);
            if (detalle_Gestion == null)
            {
                return HttpNotFound();
            }
            return View(detalle_Gestion);
        }

        // POST: Detalle_Gestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detalle_Gestion detalle_Gestion = db.Detalle_Gestion.Find(id);
            db.Detalle_Gestion.Remove(detalle_Gestion);
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
