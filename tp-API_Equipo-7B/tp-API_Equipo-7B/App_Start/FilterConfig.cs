using System.Web;
using System.Web.Mvc;

namespace tp_API_Equipo_7B
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
