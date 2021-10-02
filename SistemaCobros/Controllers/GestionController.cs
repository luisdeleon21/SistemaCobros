using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaCobros.Models;
using System.Net.Mail;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Text;

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
        public ActionResult Llamar(int id)
        {
            const string accountSid = "AC7b0d5774132892557d5650104b57639f";
            const string authToken = "f31dacfa2bd152b8cf691837a987a25c";
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber("+50255649290");
            var from = new PhoneNumber("+14133728334");
            var call = CallResource.Create(to, from,
                url: new Uri("http://demo.twilio.com/docs/voice.xml"));

            Detalle_Gestion detalle = new Detalle_Gestion();
            detalle.descripcion = "Se realizo llamada telefonica";
            detalle.fecha = DateTime.Now.Date;
            detalle.hora = DateTime.Now.TimeOfDay;
            detalle.id_gestion = id;
            detalle.id_tipo_gestion = 1;
            detalle.comentario = "Llamada automática";

            db.Detalle_Gestion.Add(detalle);
            db.SaveChanges();
            return RedirectToAction("Index", "Detalle_Gestion");


        }
        public ActionResult EnvioSMS(int idgestion, int? id)
        {
            string accountSid = "AC7b0d5774132892557d5650104b57639f";
            string authToken = "f31dacfa2bd152b8cf691837a987a25c";
            Gestion gestion = db.Gestion.Find(idgestion);
            Ciente cliente = db.Ciente.Find(id);

            TwilioClient.Init(accountSid, authToken);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Estimado Cliente: ").Append(cliente.nombre).Append(cliente.apellido);

            sb.AppendLine("Su cuota esta proxima a vencer, debe efectuar su pago de Q").Append(gestion.Cartera.Factura_Credito.monto_cuota).Append(" Antes de ").
            Append(gestion.Cartera.Factura_Credito.dia_mes_pago).Append(" de cada mes.");
            sb.AppendLine("Atentamete,");
            sb.AppendLine("Sistema de cobros ");

            var message = MessageResource.Create(
                body: sb.ToString(),
                from: new Twilio.Types.PhoneNumber("+14133728334"),
                to: new Twilio.Types.PhoneNumber("+50255649290")
            );

            Detalle_Gestion detalle = new Detalle_Gestion();
            detalle.descripcion = "Envio de recordatorio de pago SMS";
            detalle.fecha = DateTime.Now.Date;
            detalle.hora = DateTime.Now.TimeOfDay;
            detalle.id_gestion = idgestion;
            detalle.id_tipo_gestion = 2;
            detalle.comentario = "SMS automático";

            db.Detalle_Gestion.Add(detalle);
            db.SaveChanges();
            return RedirectToAction("Index", "Detalle_Gestion");

          
        }

        public ActionResult EnvioCorreo(int idgestion, int? id)
        {
           
            Gestion gestion = db.Gestion.Find(id);
            Ciente cliente = db.Ciente.Find(id);


            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Estimado Cliente: ").Append(cliente.nombre).Append(cliente.apellido).Append("\n");
            sb.AppendLine("Su cuota esta proxima a vencer, debe efectuar su pago de Q").Append(gestion.Cartera.Factura_Credito.monto_cuota).Append(" Antes de ").
            Append(gestion.Cartera.Factura_Credito.dia_mes_pago).Append(" de cada mes.").Append("\n");
            sb.AppendLine("Atentamete,").Append("\n");
            sb.AppendLine("Sistema de cobros ").Append("\n");

            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("sistema.cobrosumg@gmail.com", "Sistema de cobros", System.Text.Encoding.UTF8);//Correo de salida
            correo.To.Add(cliente.correo_electronico); //Correo destino?
            correo.Subject = "Recordatorio de cobro"; //Asunto
            correo.Body = sb.ToString(); //Mensaje del correo
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
            smtp.Port = 587; //Puerto de salida
            smtp.Credentials = new System.Net.NetworkCredential("sistema.cobrosumg@gmail.com", "Sistema123@");//Cuenta de correo
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.EnableSsl = true;//True si el servidor de correo permite ssl
            smtp.Send(correo);

            Detalle_Gestion detalle = new Detalle_Gestion();
            detalle.descripcion = "Envio de recordatorio de pago Correo electronico";
            detalle.fecha = DateTime.Now.Date;
            detalle.hora = DateTime.Now.TimeOfDay;
            detalle.id_gestion = idgestion;
            detalle.id_tipo_gestion = 3;
            detalle.comentario = "Correo electronico automático";

            db.Detalle_Gestion.Add(detalle);
            db.SaveChanges();

            return RedirectToAction("Index", "Detalle_Gestion");
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
