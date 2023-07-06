using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Edesoft.ERP.MVC
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


			routes.MapRoute(
				"UsuarioBackoffice",
				"Usuarios/Backoffice/Index",
				new { controller = "UsuarioBackoffice", action = "Index" }
			);
			
			routes.MapRoute(
				"ClienteBackoffice",
				"ClienteBackoffice/Backoffice/Index",
				new { controller = "ClienteBackoffice", action = "Index" }
			);
			
			routes.MapRoute(
				"AtivosBackoffice",
				"AtivosBackoffice/Backoffice/Index",
				new { controller = "AtivosBackoffice", action = "Index" }
			);


			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
			);
		}
	}
}
