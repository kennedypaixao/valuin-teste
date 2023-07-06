using Edesoft.ERP.Application.Seguranca.Usuario;
using Edesoft.ERP.DTO.Backoffice.Usuario;
using Edesoft.ERP.MVC.MVC;
using Edesoft.ERP.MVC.ViewModel.Global;
using Edesoft.ERP.Shared.Roles;
using Edesoft.ERP.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roles = Edesoft.ERP.Shared.Roles.Roles;

namespace Edesoft.ERP.MVC.Controllers.Seguranca
{
	[EdesoftAuthorizeRole(RolesDefinition.RoleUsuariosIndex , RolesDefinition.ModuloSeguranca)]
	public class UsuariosController : EdesoftController
	{
		private UsuarioApplication _usuarioApp;

		public UsuariosController(UsuarioApplication usuarioApp)
		{
			_usuarioApp = usuarioApp;
		}

		[EdesoftAuthorizeClaim(RolesDefinition.RoleUsuariosIndex, ClaimRole.CanGet)]
		public ActionResult Index()
		{
			return View();
		}

		[EdesoftAuthorizeClaim(RolesDefinition.RoleUsuariosIndex, ClaimRole.CanGet)]
		public ActionResult _modal()
		{
			return PartialView();
		}

		[EdesoftAuthorizeClaim(RolesDefinition.RoleUsuariosIndex, ClaimRole.CanGet)]
		[HttpGet]
		public JsonResult Get(Guid? Id)
		{
			ResultJsonViewModel retorno = new ResultJsonViewModel();

			try
			{
				if ((Id ?? Guid.Empty) == Guid.Empty)
					retorno.data = _usuarioApp.GetAll();
				else
					retorno.data = _usuarioApp.Get(Id.Value);
			}
			catch (Exception e)
			{
				retorno.status_code = System.Net.HttpStatusCode.BadRequest;
				retorno.message = e.GetFullMessage();
			}

			return Json(retorno, JsonRequestBehavior.AllowGet);
		}

		[EdesoftAuthorizeClaim(RolesDefinition.RoleUsuariosIndex, ClaimRole.CanPost)]
		[HttpPost]
		public JsonResult Post(UsuarioBackofficeDto Data)
		{
			ResultJsonViewModel retorno = new ResultJsonViewModel();

			try
			{
				_usuarioApp.Insert(Data);
			}
			catch (Exception e)
			{
				retorno.status_code = System.Net.HttpStatusCode.BadRequest;
				retorno.message = e.GetFullMessage();
			}

			return Json(retorno, JsonRequestBehavior.AllowGet);
		}
		[EdesoftAuthorizeClaim(RolesDefinition.RoleUsuariosIndex, ClaimRole.CanPut)]
		public JsonResult Put(UsuarioBackofficeDto Data)
		{
			ResultJsonViewModel retorno = new ResultJsonViewModel();

			try
			{
				_usuarioApp.Update(Data.Id, Data);
			}
			catch (Exception e)
			{
				retorno.status_code = System.Net.HttpStatusCode.BadRequest;
				retorno.message = e.GetFullMessage();
			}

			return Json(retorno, JsonRequestBehavior.AllowGet);
		}

		[EdesoftAuthorizeClaim(RolesDefinition.RoleUsuariosIndex, ClaimRole.CanDelete)]
		public JsonResult Delete(Guid Id)
		{
			ResultJsonViewModel retorno = new ResultJsonViewModel();

			try
			{
				_usuarioApp.Delete(Id);
			}
			catch (Exception e)
			{
				retorno.status_code = System.Net.HttpStatusCode.BadRequest;
				retorno.message = e.GetFullMessage();
			}

			return Json(retorno, JsonRequestBehavior.AllowGet);
		}

	}
}