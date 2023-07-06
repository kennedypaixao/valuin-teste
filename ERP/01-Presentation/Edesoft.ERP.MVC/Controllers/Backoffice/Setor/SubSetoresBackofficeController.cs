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
    public class SubSetoresBackofficeController : EdesoftController
    {
        private readonly SubSetoresApplication _subSetorApp;
        public SubSetoresBackofficeController(SubSetoresApplication subSetorApp)
        {
            _subSetorApp = subSetorApp;
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleAtivosIndex, ClaimRole.CanGet)]
        [HttpGet]
        public JsonResult Get(Guid? Id)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                if ((Id ?? Guid.Empty) == Guid.Empty)
                    retorno.data = _subSetorApp.GetAll();
                else
                    retorno.data = _subSetorApp.Get(Id.Value);
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
        public JsonResult GetBySetor(Guid? Id)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                if ((Id ?? Guid.Empty) == Guid.Empty)
                    retorno.data = _subSetorApp.GetAll();
                else
                    retorno.data = _subSetorApp.GetBySetor(Id.Value);
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
        public JsonResult GetSubSetorBySetorIdNome(Guid? setorId, string subSetorNome)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                retorno.data = _subSetorApp.GetSubSetorBySetorIdNome(setorId, subSetorNome);
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
        public JsonResult Post(SubSetoresBackofficeDto Data)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                retorno.data = _subSetorApp.Insert(Data);
            }
            catch (Exception e)
            {
                retorno.status_code = System.Net.HttpStatusCode.BadRequest;
                retorno.message = e.GetFullMessage();
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleAtivosIndex, ClaimRole.CanPut)]
        public JsonResult Put(SubSetoresBackofficeDto Data)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                _subSetorApp.Update(Data.Id, Data);
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
                _subSetorApp.Delete(Id);
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