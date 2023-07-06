using Edesoft.ERP.Tools.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Edesoft.ERP.MVC.MVC.Security;
using Edesoft.ERP.Shared.Roles;
using Roles = Edesoft.ERP.Shared.Roles.Roles;
using System.Net.Http.Headers;

namespace Edesoft.ERP.MVC.MVC
{
	public class EdesoftController : Controller
	{
		protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return new JsonNetResult()
			{
				Data = data,
				ContentType = contentType,
				ContentEncoding = contentEncoding,
				JsonRequestBehavior = behavior,
				MaxJsonLength = Int32.MaxValue
			};
		}
	}

	public class EdesoftAuthorizeRole : AuthorizeAttribute
	{
		private readonly double[] allowedroles;

		public EdesoftAuthorizeRole(params double[] role)
		{
			this.allowedroles = role;
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (SessionPersister.User == null || string.IsNullOrEmpty(SessionPersister.User.Name))
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
			}
			else
			{
				var roles = new List<Roles>();
				foreach (var role in allowedroles)
					roles.Add(new Roles(role, ClaimRole.CanGet));

				CustomPrincipal cm = new CustomPrincipal(SessionPersister.User);
				if (!roles.Any(p=> p.RoleId == RolesDefinition.All) && !cm.IsInRole(roles))
				{
					filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Error401" }));
				}
			}
		}
	}


	public class EdesoftAuthorizeClaim : AuthorizeAttribute
	{
		private readonly double allowedroles;
		private readonly ClaimRole allowedClaims;

		public EdesoftAuthorizeClaim(double role, ClaimRole claim)
		{
			this.allowedroles = role;
			this.allowedClaims = claim;
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (SessionPersister.User == null || string.IsNullOrEmpty(SessionPersister.User.Name))
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
			}
			else
			{
				CustomPrincipal cm = new CustomPrincipal(SessionPersister.User);
				var roles = new List<Roles>();
				roles.Add(new Roles(allowedroles, allowedClaims));
				if (!roles.Any(p => p.RoleId == RolesDefinition.All) && !cm.IsInRole(roles))
				{
					filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Error401" }));
				}
			}
		}
	}

	public class JsonNetResult : JsonResult
	{
		public JsonNetResult()
		{
			Settings = new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
			};
			Settings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
		}

		public JsonSerializerSettings Settings { get; private set; }

		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
				throw new InvalidOperationException("JSON GET is not allowed");

			HttpResponseBase response = context.HttpContext.Response;
			response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

			if (this.ContentEncoding != null)
				response.ContentEncoding = this.ContentEncoding;
			if (this.Data == null)
				return;

			var scriptSerializer = JsonSerializer.Create(this.Settings);

			using (var sw = new StringWriter())
			{
				scriptSerializer.Serialize(sw, this.Data);
				response.Write(sw.ToString());
			}

		}
	}

	public class CompressFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			HttpResponseBase response = filterContext.HttpContext.Response;

			if (response.Filter is GZipStream || response.Filter is DeflateStream || response.Filter == null) return;

			HttpRequestBase request = filterContext.HttpContext.Request;

			string acceptEncoding = request.Headers["Accept-Encoding"];

			if (string.IsNullOrEmpty(acceptEncoding)) return;

			acceptEncoding = acceptEncoding.ToLower();

			if (acceptEncoding.Contains("gzip"))
			{
				response.AppendHeader("Content-encoding", "gzip");
				response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
			}
			else if (acceptEncoding.Contains("deflate"))
			{
				response.AppendHeader("Content-encoding", "deflate");
				response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
			}
		}
	}
}