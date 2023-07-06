using Edesoft.ERP.Application.Backoffice.Cliente;
using Edesoft.ERP.DTO.Backoffice.Cliente;
using Edesoft.ERP.MVC.MVC;
using Edesoft.ERP.MVC.ViewModel.Global;
using Edesoft.ERP.Shared.Constants;
using Edesoft.ERP.Shared.Roles;
using Edesoft.ERP.Tools.Extensions;
using System;
using System.Web.Mvc;

namespace Edesoft.ERP.MVC.Controllers.Backoffice
{
    [EdesoftAuthorizeRole(RolesDefinition.RoleClientesIndex, RolesDefinition.ModuloBackoffice)]
    public class ClienteBackofficeController : EdesoftController
    {
        private ClienteApplication _clienteApp;

        public ClienteBackofficeController(ClienteApplication clienteApp)
        {
            _clienteApp = clienteApp;
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleClientesIndex, ClaimRole.CanGet)]
        public ActionResult Index()
        {
            return View();
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleClientesIndex, ClaimRole.CanGet)]
        public ActionResult _modal()
        {
            return PartialView();
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleClientesIndex, ClaimRole.CanGet)]
        [HttpGet]
        public JsonResult Get(Guid? id)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                if ((id ?? Guid.Empty) == Guid.Empty)
                    retorno.data = _clienteApp.GetAll();
                else
                    retorno.data = _clienteApp.Get(id.Value);
            }
            catch (Exception e)
            {
                retorno.status_code = System.Net.HttpStatusCode.BadRequest;
                retorno.message = e.GetFullMessage();
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleClientesIndex, ClaimRole.CanPost)]
        [HttpPost]
        public JsonResult Post(ClienteBackofficeDto Data)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                _clienteApp.Insert(Data);
            }
            catch (Exception e)
            {
                retorno.status_code = System.Net.HttpStatusCode.BadRequest;
                retorno.message = e.GetFullMessage();
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleClientesIndex, ClaimRole.CanPut)]
        [HttpPost]
        public JsonResult Put(ClienteBackofficeDto Data)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                _clienteApp.Update(Data.Id, Data);
            }
            catch (Exception e)
            {
                retorno.status_code = System.Net.HttpStatusCode.BadRequest;
                retorno.message = e.GetFullMessage();
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [EdesoftAuthorizeClaim(RolesDefinition.RoleClientesIndex, ClaimRole.CanDelete)]
        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel();

            try
            {
                _clienteApp.Delete(id);
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