using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCobros.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
            try
            {
                using (Models.sistema_cobrosEntities2 db = new Models.sistema_cobrosEntities2())
                {
                    var oUser = (from d in db.Usuario

                                 where d.nombre == User.Trim() && d.contrasena == Pass.Trim()
                                 select d).FirstOrDefault();
                    if (oUser == null)
                    {

                        ViewBag.Error = "Usuario o contraseña invalida";
                        return View();

                    }

                    Session["User"] = oUser;

                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }
    }
}