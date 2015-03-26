using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AtlanticDX.ERP {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");

            routes.MapRoute(
                name: "Default", // Route name
                url: "{area}/{controller}/{action}/{id}", // URL with parameters
                defaults: new { area = "Public", controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "AtlanticDX.ERP.Areas.Public.Controllers" }
            ).DataTokens.Add("Area", "Public"); ;
        }
    }
}