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
    public class Tipo_GestionController : Controller
    {
        private Models.ViewModel.sistema_cobrosEntities1 db = new Models.ViewModel.sistema_cobrosEntities1();

        // GET: Tipo_Gestion
        public ActionResult Index()
        {
            return View(db.Tipo_Gestion.ToList());
        }

        // GET: Tipo_Gestion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Gestion tipo_Gestion = db.Tipo_Gestion.Find(id);
            if (tipo_Gestion == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Gestion);
        }

        // GET: Tipo_Gestion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Gestion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo_gestion,descripcion")] Tipo_Gestion tipo_Gestion)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Gestion.Add(tipo_Gestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_Gestion);
        }

        // GET: Tipo_Gestion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Gestion tipo_Gestion = db.Tipo_Gestion.Find(id);
            if (tipo_Gestion == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Gestion);
        }

        // POST: Tipo_Gestion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo_gestion,descripcion")] Tipo_Gestion tipo_Gestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Gestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Gestion);
        }

        // GET: Tipo_Gestion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Gestion tipo_Gestion = db.Tipo_Gestion.Find(id);
            if (tipo_Gestion == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Gestion);
        }

        // POST: Tipo_Gestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Gestion tipo_Gestion = db.Tipo_Gestion.Find(id);
            db.Tipo_Gestion.Remove(tipo_Gestion);
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
