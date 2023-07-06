using Edesoft.ERP.MVC.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Edesoft.ERP.Tools.Security;
using Edesoft.ERP.Shared.Roles;

namespace Edesoft.ERP.MVC.Controllers
{
	//[EdesoftAuthorizeRole(RolesDefinition.All)]
	public class HomeController : EdesoftController
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Default()
		{
			return View();
		}

		//[EdesoftAuthorizeRole(RolesDefinition.All)]
		public ActionResult ImageProfile()
		{

			byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("/content/images/users/default.png"));// Convert.FromBase64String(SessionPersister.User.ProfilePhoto);
			return base.File(imageBytes, "image/jpeg", $"{SessionPersister.User.Id}.jpg");
		}
	}
}