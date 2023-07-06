using Edesoft.ERP.MVC.MVC;
using Edesoft.ERP.Shared.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edesoft.ERP.MVC.Controllers.Edesoft
{
	[EdesoftAuthorizeRole(RolesDefinition.All)]
	public class EdesoftCommonController: EdesoftController
	{
        // GET: Edesoft
        public ActionResult DefaultGridArea()
        {
            return PartialView("_defaultGridArea");
        }
    }
}