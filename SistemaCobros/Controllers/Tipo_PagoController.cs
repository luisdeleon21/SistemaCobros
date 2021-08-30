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
    public class Tipo_PagoController : Controller
    {
        private sistema_cobrosEntities db = new sistema_cobrosEntities();

        // GET: Tipo_Pago
        public ActionResult Index()
        {
            return View(db.Tipo_Pago.ToList());
        }

        // GET: Tipo_Pago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Pago tipo_Pago = db.Tipo_Pago.Find(id);
            if (tipo_Pago == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Pago);
        }

        // GET: Tipo_Pago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Pago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo_pago,descripcion,esRecurrente")] Tipo_Pago tipo_Pago)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Pago.Add(tipo_Pago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_Pago);
        }

        // GET: Tipo_Pago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Pago tipo_Pago = db.Tipo_Pago.Find(id);
            if (tipo_Pago == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Pago);
        }

        // POST: Tipo_Pago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo_pago,descripcion,esRecurrente")] Tipo_Pago tipo_Pago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Pago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Pago);
        }

        // GET: Tipo_Pago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Pago tipo_Pago = db.Tipo_Pago.Find(id);
            if (tipo_Pago == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Pago);
        }

        // POST: Tipo_Pago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Pago tipo_Pago = db.Tipo_Pago.Find(id);
            db.Tipo_Pago.Remove(tipo_Pago);
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
