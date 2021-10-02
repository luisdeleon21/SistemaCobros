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
    public class Factura_CreditoController : Controller
    {
        private Models.ViewModel.sistema_cobrosEntities1 db = new Models.ViewModel.sistema_cobrosEntities1();

        // GET: Factura_Credito
        public ActionResult Index()
        {
            var factura_Credito = db.Factura_Credito.Include(f => f.Ciente);
            return View(factura_Credito.ToList());
        }

        // GET: Factura_Credito/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura_Credito factura_Credito = db.Factura_Credito.Find(id);
            if (factura_Credito == null)
            {
                return HttpNotFound();
            }
            return View(factura_Credito);
        }

        // GET: Factura_Credito/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.Ciente, "id_cliente", "nit");
            return View();
        }

        // POST: Factura_Credito/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_factura,total,cantidad_cuotas,monto_cuota,id_cliente,dia_mes_pago,estado,serie,numero")] Factura_Credito factura_Credito)
        {
            if (ModelState.IsValid)
            {
                db.Factura_Credito.Add(factura_Credito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cliente = new SelectList(db.Ciente, "id_cliente", "nit", factura_Credito.id_cliente);
            return View(factura_Credito);
        }

        // GET: Factura_Credito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura_Credito factura_Credito = db.Factura_Credito.Find(id);
            if (factura_Credito == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.Ciente, "id_cliente", "nit", factura_Credito.id_cliente);
            return View(factura_Credito);
        }

        // POST: Factura_Credito/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_factura,total,cantidad_cuotas,monto_cuota,id_cliente,dia_mes_pago,estado,serie,numero")] Factura_Credito factura_Credito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura_Credito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.Ciente, "id_cliente", "nit", factura_Credito.id_cliente);
            return View(factura_Credito);
        }

        // GET: Factura_Credito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura_Credito factura_Credito = db.Factura_Credito.Find(id);
            if (factura_Credito == null)
            {
                return HttpNotFound();
            }
            return View(factura_Credito);
        }

        // POST: Factura_Credito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura_Credito factura_Credito = db.Factura_Credito.Find(id);
            db.Factura_Credito.Remove(factura_Credito);
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
