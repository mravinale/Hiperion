using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Hiperion
{
    public static class HiperionConfig
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
