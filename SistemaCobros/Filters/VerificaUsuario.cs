using SistemaCobros.Controllers;
using SistemaCobros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCobros.Filters
{
    public class VerificaUsuario : ActionFilterAttribute
    {
        private Usuario oUsuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                oUsuario = (Usuario)HttpContext.Current.Session["User"];
                if (oUsuario == null)
                {

                    if (filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                    }



                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }

        }
    }
}