using Edesoft.ERP.Application.Common;
using Edesoft.ERP.MVC.MVC;
using Edesoft.ERP.MVC.ViewModel.Global;
using Edesoft.ERP.Tools.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Edesoft.ERP.MVC.Controllers.Common
{
    public class FileController : EdesoftController
    {
        private FileApplication _fileApp;

        public FileController(FileApplication fileApp)
        {
            _fileApp = fileApp;
        }

        [HttpPost]
        public ActionResult Upload(Guid? IdContratante)
        {
            ResultJsonViewModel retorno = new ResultJsonViewModel() { };

            try
            {
                retorno.data = _fileApp.Upload(Request, Server, IdContratante);
            }
            catch (Exception e)
            {
                retorno.status_code = System.Net.HttpStatusCode.BadRequest;
                retorno.message = e.GetFullMessage();
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Download(string path)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string filename = path.Split('/').Last();
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
    }
}