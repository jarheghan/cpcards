using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

<<<<<<< HEAD
namespace CP_Cards
=======
namespace TGIESHOW.CREDENTIAL
>>>>>>> 6a0a102160b99a84263848a67b0587d838aca32d
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
