using System.Web;
using System.Web.Mvc;

namespace SistemaCobros
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filters.VerificaUsuario());
        }
    }
}
