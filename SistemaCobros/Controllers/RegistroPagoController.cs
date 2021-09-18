using SistemaCobros.Models;
using SistemaCobros.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCobros.Controllers
{
    public class RegistroPagoController : Controller
    {
        // GET: RegistroPago
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            List<ListRegistroPagoViewModel> lst = new List<ListRegistroPagoViewModel>();
            using (sistema_cobrosEntities db= 
                new sistema_cobrosEntities())
            {
             lst =
                    (from P in db.Pago
                     orderby P.fecha_pago descending
                     select new ListRegistroPagoViewModel
                     {
                         Id = P.id_pago,
                         Monto = P.monto,
                         Mora = P.mora,
                         Fecha_Pago = P.fecha_pago,
                         Id_cartera = P.id_cartera,
                         Id_tipo_pago = P.id_tipo_pago

                     }).ToList();
            }
            return View(lst);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(InsertRegistroPagoViewModel model)
        {
            try
            {
                using (sistema_cobrosEntities db = new sistema_cobrosEntities())
                {
                    var oRegistroPago = new Pago();
                    oRegistroPago.monto = model.Monto;
                    oRegistroPago.mora = model.Mora;
                    oRegistroPago.fecha_pago = model.Fecha_Pago;
                    oRegistroPago.id_cartera = model.Id_cartera;
                    oRegistroPago.id_tipo_pago = model.Id_tipo_pago;
                    db.Pago.Add(oRegistroPago);
                    db.SaveChanges();

                }
                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }



    }
}