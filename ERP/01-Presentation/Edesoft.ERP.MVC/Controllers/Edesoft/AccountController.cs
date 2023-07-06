using Edesoft.ERP.Application.Security;
using Edesoft.ERP.Tools.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Edesoft.ERP.MVC.MVC;
using Edesoft.ERP.MVC.ViewModel.Global;
using System.Net;

namespace Edesoft.ERP.MVC.Controllers.Edesoft
{
	public class AccountController : EdesoftController
	{
		private SecurityApplication _securityApp;

		public AccountController(SecurityApplication securityApp)
		{
			_securityApp = securityApp;
		}
		[AllowAnonymous]
		public ActionResult Login(string message = "")
		{
			ViewBag.message = message;
			return View();
		}

		[HttpPost]
		public ActionResult Login(string emailAddress, string password, bool rememberMe = false, string redirect = null)
		{
			var dataReturn = new ResultJsonViewModel();
			try
			{
				dataReturn.status_code = HttpStatusCode.OK;
				dataReturn.message = "";

				//var user = _securityApp.Login(emailAddress, password);
				//if (user == null || !user.Active)
				//{
				//	throw new Exception("Usuário / senha inválidos!");
				//}
				//SessionPersister.User = user;
				string redirectTo = redirect ?? "Home/Index";

				return Redirect($"~/Home/Default/#/{redirectTo}");

			}
			catch (Exception erro)
			{
				dataReturn.status_code = HttpStatusCode.BadRequest;
				dataReturn.message = erro.Message;
			}
			ViewBag.message = "Usuário / senha inválidos!";
			return View();
		}


		[AllowAnonymous]
		public ActionResult Logoff()
		{
			Session.Abandon();

			FormsAuthentication.SignOut();
			return RedirectToAction("Login");

		}
	}
}