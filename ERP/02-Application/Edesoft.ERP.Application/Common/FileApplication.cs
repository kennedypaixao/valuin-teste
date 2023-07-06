using Edesoft.ERP.Tools.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Edesoft.ERP.Application.Common
{
    public class FileApplication
    {
        public string Upload(HttpRequestBase clientInfo, HttpServerUtilityBase server, Guid? IdContratante)
        {
            try
            {
                string fileDir = "";

                HttpPostedFileBase file = clientInfo.Files[0];
                string fname;

                // Checking for Internet Explorer  
                if (clientInfo.Browser.Browser.ToUpper() == "IE" || clientInfo.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                }

                fname = Guid.NewGuid() + Path.GetExtension(fname);

                var uidContratante = IdContratante ?? SessionPersister.User.IdContratante;

                string path = server.MapPath($"~/Upload/{IdContratante}");
                Directory.CreateDirectory(path);
                fname = Path.Combine(path, fname);
                fileDir = fname;

                file.SaveAs(fname);

                return $"{IdContratante}/{Path.GetFileName(fname)}";
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
