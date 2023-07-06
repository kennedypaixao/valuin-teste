using Edesoft.ERP.Application.Edesoft;
using Edesoft.ERP.MVC.MVC;
using Edesoft.ERP.MVC.ViewModel.Global;
using Edesoft.ERP.Shared.Roles;
using Edesoft.ERP.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edesoft.ERP.MVC.Controllers.Edesoft
{
	[EdesoftAuthorizeRole(RolesDefinition.All)]
	public class CEPController : Controller
    {
		private CEPApplication _cepApp;

		public CEPController(CEPApplication cepApp)
		{
			_cepApp = cepApp;
		}
		[HttpGet]
		public JsonResult GetByCEP(string nrCep, Guid? IdCep = null)
		{
			ResultJsonViewModel retorno = new ResultJsonViewModel();

			try
			{
				retorno.data = _cepApp.GetByCEP(nrCep, IdCep);
			}
			catch (Exception e)
			{
				retorno.status_code = System.Net.HttpStatusCode.BadRequest;
				retorno.message = e.GetFullMessage();
			}

			return Json(retorno, JsonRequestBehavior.AllowGet);
		}
		[HttpGet]
		public JsonResult GetModelCidadeCombo()
		{
			ResultJsonViewModel retorno = new ResultJsonViewModel();

			try
			{
				retorno.data = _cepApp.GetModelCidadeCombo();
			}
			catch (Exception e)
			{
				retorno.status_code = System.Net.HttpStatusCode.BadRequest;
				retorno.message = e.GetFullMessage();
			}

			return Json(retorno, JsonRequestBehavior.AllowGet);
		}
		[HttpGet]
		public JsonResult GetModelUFCombo()
		{
			ResultJsonViewModel retorno = new ResultJsonViewModel();

			try
			{
				retorno.data = _cepApp.GetModelUFCombo();
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