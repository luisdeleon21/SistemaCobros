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
    public class CarteraController : Controller
    {
        private Models.ViewModel.sistema_cobrosEntities1 db = new Models.ViewModel.sistema_cobrosEntities1();

        // GET: Cartera
        public ActionResult Index()
        {
            var cartera = db.Cartera.Include(c => c.Estado).Include(c => c.Factura_Credito).Include(c => c.Mora).Include(c => c.Usuario);
            return View(cartera.ToList());
        }

        // GET: Cartera/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartera cartera = db.Cartera.Find(id);
            if (cartera == null)
            {
                return HttpNotFound();
            }
            return View(cartera);
        }

        // GET: Cartera/Create
        public ActionResult Create()
        {
            ViewBag.id_usuario = new SelectList(db.Estado, "id_estado", "descripcion");
            ViewBag.id_factura = new SelectList(db.Factura_Credito, "id_factura", "estado");
            ViewBag.id_mora = new SelectList(db.Mora, "id_mora", "descripcion");
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre");
            return View();
        }

        // POST: Cartera/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cartera,fecha_vencimiento,saldo,id_factura,id_usuario,id_estado,id_mora")] Cartera cartera)
        {
            if (ModelState.IsValid)
            {
                db.Cartera.Add(cartera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_usuario = new SelectList(db.Estado, "id_estado", "descripcion", cartera.id_usuario);
            ViewBag.id_factura = new SelectList(db.Factura_Credito, "id_factura", "estado", cartera.id_factura);
            ViewBag.id_mora = new SelectList(db.Mora, "id_mora", "descripcion", cartera.id_mora);
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre", cartera.id_usuario);
            return View(cartera);
        }

        // GET: Cartera/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartera cartera = db.Cartera.Find(id);
            if (cartera == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_usuario = new SelectList(db.Estado, "id_estado", "descripcion", cartera.id_usuario);
            ViewBag.id_factura = new SelectList(db.Factura_Credito, "id_factura", "estado", cartera.id_factura);
            ViewBag.id_mora = new SelectList(db.Mora, "id_mora", "descripcion", cartera.id_mora);
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre", cartera.id_usuario);
            return View(cartera);
        }

        // POST: Cartera/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cartera,fecha_vencimiento,saldo,id_factura,id_usuario,id_estado,id_mora")] Cartera cartera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_usuario = new SelectList(db.Estado, "id_estado", "descripcion", cartera.id_usuario);
            ViewBag.id_factura = new SelectList(db.Factura_Credito, "id_factura", "estado", cartera.id_factura);
            ViewBag.id_mora = new SelectList(db.Mora, "id_mora", "descripcion", cartera.id_mora);
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre", cartera.id_usuario);
            return View(cartera);
        }

        // GET: Cartera/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartera cartera = db.Cartera.Find(id);
            if (cartera == null)
            {
                return HttpNotFound();
            }
            return View(cartera);
        }

        // POST: Cartera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cartera cartera = db.Cartera.Find(id);
            db.Cartera.Remove(cartera);
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
