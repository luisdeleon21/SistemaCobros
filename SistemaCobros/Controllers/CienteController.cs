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
    public class CienteController : Controller
    {
        private sistema_cobrosEntities db = new sistema_cobrosEntities();

        // GET: Ciente
        public ActionResult Index()
        {
            var ciente = db.Ciente.Include(c => c.Estado).Include(c => c.Cliente_Tipo_Pago);
            return View(ciente.ToList());
        }

        // GET: Ciente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciente ciente = db.Ciente.Find(id);
            if (ciente == null)
            {
                return HttpNotFound();
            }
            return View(ciente);
        }

        // GET: Ciente/Create
        public ActionResult Create()
        {
            ViewBag.id_estado = new SelectList(db.Estado, "id_estado", "descripcion");
            ViewBag.id_cliente = new SelectList(db.Cliente_Tipo_Pago, "id_cliente_tipo_pago", "no_tarjeta");
            return View();
        }

        // POST: Ciente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cliente,nit,nombre,apellido,telefono,direccion,correo_electronico,id_estado")] Ciente ciente)
        {
            if (ModelState.IsValid)
            {
                db.Ciente.Add(ciente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_estado = new SelectList(db.Estado, "id_estado", "descripcion", ciente.id_estado);
            ViewBag.id_cliente = new SelectList(db.Cliente_Tipo_Pago, "id_cliente_tipo_pago", "no_tarjeta", ciente.id_cliente);
            return View(ciente);
        }

        // GET: Ciente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciente ciente = db.Ciente.Find(id);
            if (ciente == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_estado = new SelectList(db.Estado, "id_estado", "descripcion", ciente.id_estado);
            ViewBag.id_cliente = new SelectList(db.Cliente_Tipo_Pago, "id_cliente_tipo_pago", "no_tarjeta", ciente.id_cliente);
            return View(ciente);
        }

        // POST: Ciente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cliente,nit,nombre,apellido,telefono,direccion,correo_electronico,id_estado")] Ciente ciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_estado = new SelectList(db.Estado, "id_estado", "descripcion", ciente.id_estado);
            ViewBag.id_cliente = new SelectList(db.Cliente_Tipo_Pago, "id_cliente_tipo_pago", "no_tarjeta", ciente.id_cliente);
            return View(ciente);
        }

        // GET: Ciente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciente ciente = db.Ciente.Find(id);
            if (ciente == null)
            {
                return HttpNotFound();
            }
            return View(ciente);
        }

        // POST: Ciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ciente ciente = db.Ciente.Find(id);
            db.Ciente.Remove(ciente);
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
