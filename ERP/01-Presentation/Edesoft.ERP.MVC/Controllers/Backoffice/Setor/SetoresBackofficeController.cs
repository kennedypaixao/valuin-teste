using Edesoft.ERP.Application.Backoffice.Setor;
using Edesoft.ERP.DTO.Backoffice.Setor;
using Edesoft.ERP.MVC.MVC;
using Edesoft.ERP.MVC.ViewModel.Global;
using System.Web.Mvc;
using System;
using Edesoft.ERP.Tools.Extensions;
using Edesoft.ERP.Shared.Roles;

namespace Edesoft.ERP.MVC.Controllers.Backoffice.Setor
{
    [EdesoftAuthorizeRole(RolesDefinition.RoleAtivosIndex, RolesDefinition.ModuloBackoffice)]
    public class SetoresBackofficeController : EdesoftController
    {
        private readonly SetoresApplication _setorApp;

        public SetoresBackofficeController(SetoresApplication setorApp)
        {
            _setorApp = setorApp;
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleAtivosIndex, ClaimRole.CanGet)]
        [HttpGet]
        public JsonResult Get(Guid? Id)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                if ((Id ?? Guid.Empty) == Guid.Empty)
                    retorno.data = _setorApp.GetAll();
                else
                    retorno.data = _setorApp.Get(Id.Value);
            }
            catch (Exception e)
            {
                retorno.status_code = System.Net.HttpStatusCode.BadRequest;
                retorno.message = e.GetFullMessage();
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleAtivosIndex, ClaimRole.CanGet)]
        [HttpGet]
        public JsonResult GetSetor(string setorNome)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                if (string.IsNullOrEmpty(setorNome))
                    retorno.data = _setorApp.GetAll();
                else
                    retorno.data = _setorApp.GetSetor(setorNome);
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
        public JsonResult Post(SetoresBackofficeDto Data)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                _setorApp.Insert(Data);
            }
            catch (Exception e)
            {
                retorno.status_code = System.Net.HttpStatusCode.BadRequest;
                retorno.message = e.GetFullMessage();
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleAtivosIndex, ClaimRole.CanPut)]
        public JsonResult Put(SetoresBackofficeDto Data)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                _setorApp.Update(Data.Id, Data);
            }
            catch (Exception e)
            {
                retorno.status_code = System.Net.HttpStatusCode.BadRequest;
                retorno.message = e.GetFullMessage();
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleAtivosIndex, ClaimRole.CanDelete)]
        public JsonResult Delete(Guid Id)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                _setorApp.Delete(Id);
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