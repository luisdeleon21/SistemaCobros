using System;
using SistemaCobros.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCobros.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private Usuario oUsuario;
        private Models.ViewModel.sistema_cobrosEntities1 db = new Models.ViewModel.sistema_cobrosEntities1();
        private int idOperacion;

        public AuthorizeUser(int idOperacion = 0)
        {
            this.idOperacion = idOperacion;
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String nombreOperacion = "";
            String nombreModulo = "";
            try
            {
                oUsuario = (Usuario)HttpContext.Current.Session["User"];
                var lstMisOperaciones = from m in db.rol_operacion
                                        where m.id_rol == oUsuario.id_rol
                                            && m.operacion_id == idOperacion
                                        select m;


                if (lstMisOperaciones.ToList().Count() == 0)
                {
                    var oOperacion = db.Operacion.Find(idOperacion);
                    int? idModulo = oOperacion.modulo_id;

                    nombreModulo = getNombreDelModulo(idModulo);
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion + "&modulo=" + nombreModulo + "&msjeErrorExcepcion=");
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion + "&modulo=" + nombreModulo + "&msjeErrorExcepcion=" + ex.Message);
            }
        }



        public string getNombreDelModulo(int? idModulo)
        {
            var modulo = from m in db.Modulo
                         where m.modulo_id == idModulo
                         select m.nombre;
            String nombreModulo;
            try
            {
                nombreModulo = modulo.First();
            }
            catch (Exception)
            {
                nombreModulo = "";
            }
            return nombreModulo;
        }

    }
}