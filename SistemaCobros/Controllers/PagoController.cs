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
    public class PagoController : Controller
    {
        private Models.ViewModel.sistema_cobrosEntities1 db = new Models.ViewModel.sistema_cobrosEntities1();

        // GET: Pago
        public ActionResult Index()
        {
            var pago = db.Pago.Include(p => p.Cartera).Include(p => p.Tipo_Pago);
            return View(pago.ToList());
        }

        // GET: Pago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            ViewBag.id_cartera = new SelectList(db.Cartera, "id_cartera", "id_cartera");
            ViewBag.id_tipo_pago = new SelectList(db.Tipo_Pago, "id_tipo_pago", "descripcion");
            return View();
        }

        // POST: Pago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pago,monto,mora,fecha_pago,id_cartera,id_tipo_pago")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                db.Pago.Add(pago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cartera = new SelectList(db.Cartera, "id_cartera", "id_cartera", pago.id_cartera);
            ViewBag.id_tipo_pago = new SelectList(db.Tipo_Pago, "id_tipo_pago", "descripcion", pago.id_tipo_pago);
            return View(pago);
        }

        // GET: Pago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cartera = new SelectList(db.Cartera, "id_cartera", "id_cartera", pago.id_cartera);
            ViewBag.id_tipo_pago = new SelectList(db.Tipo_Pago, "id_tipo_pago", "descripcion", pago.id_tipo_pago);
            return View(pago);
        }

        // POST: Pago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pago,monto,mora,fecha_pago,id_cartera,id_tipo_pago")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cartera = new SelectList(db.Cartera, "id_cartera", "id_cartera", pago.id_cartera);
            ViewBag.id_tipo_pago = new SelectList(db.Tipo_Pago, "id_tipo_pago", "descripcion", pago.id_tipo_pago);
            return View(pago);
        }

        // GET: Pago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // POST: Pago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pago pago = db.Pago.Find(id);
            db.Pago.Remove(pago);
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
