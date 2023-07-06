using Edesoft.ERP.MVC.MVC;
using Edesoft.ERP.MVC.ViewModel.Global;
using Edesoft.ERP.Tools.Extensions;
using System;
using System.Web.Mvc;
using Edesoft.ERP.Shared.Roles;
using Edesoft.ERP.Application.Backoffice.AtivosBackoffice;
using Edesoft.ERP.DTO.Backoffice.Ativos;

namespace Edesoft.ERP.MVC.Controllers.Backoffice.AtivosBackoffice
{
    [EdesoftAuthorizeRole(RolesDefinition.RoleAtivosIndex, RolesDefinition.ModuloBackoffice)]
    public class AtivosBackofficeController : EdesoftController
    {
        private readonly AtivosBackofficeApplication _ativoApp;
        public AtivosBackofficeController(AtivosBackofficeApplication ativoApp)
        {
            _ativoApp = ativoApp;
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleAtivosIndex, ClaimRole.CanGet)]
        public ActionResult Index()
        {
            return View();
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleAtivosIndex, ClaimRole.CanGet)]
        public ActionResult _modal()
        {
            return PartialView();
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleAtivosIndex, ClaimRole.CanGet)]
        [HttpGet]
        public JsonResult Get(Guid? Id)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                if ((Id ?? Guid.Empty) == Guid.Empty)
                    retorno.data = _ativoApp.GetAll();
                else
                    retorno.data = _ativoApp.Get(Id.Value);
            }
            catch (Exception e)
            {
                retorno.status_code = System.Net.HttpStatusCode.BadRequest;
                retorno.message = e.GetFullMessage();
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleAtivosIndex, ClaimRole.CanPost)]
        [HttpPost]
        public JsonResult Post(AtivosBackofficeDto Data)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                _ativoApp.Insert(Data);
            }
            catch (Exception e)
            {
                retorno.status_code = System.Net.HttpStatusCode.BadRequest;
                retorno.message = e.GetFullMessage();
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleAtivosIndex, ClaimRole.CanPut)]
        [HttpPost]
        public JsonResult Put(AtivosBackofficeDto Data)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                _ativoApp.Update(Data.Id, Data);
            }
            catch (Exception e)
            {
                retorno.status_code = System.Net.HttpStatusCode.BadRequest;
                retorno.message = e.GetFullMessage();
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleAtivosIndex, ClaimRole.CanDelete)]
        [HttpPost]
        public JsonResult Delete(Guid Id)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                _ativoApp.Delete(Id);
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