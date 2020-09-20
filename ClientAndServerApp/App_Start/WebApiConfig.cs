using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TestWebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{userid}/{id}/{move}",
               
                defaults: new {
                    id = RouteParameter.Optional,
                    userid = RouteParameter.Optional,
                    move = RouteParameter.Optional
                }
            );

        }
    }
}
