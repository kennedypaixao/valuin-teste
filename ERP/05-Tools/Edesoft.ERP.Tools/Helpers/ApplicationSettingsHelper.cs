using System.Configuration;

namespace Edesoft.ERP.Tools.Helpers
{
    public static class ApplicationSettingsHelper
    {
        public static string GetKey(string Key)
        {

            var retorno = ConfigurationManager.AppSettings[Key].ToString();

            return retorno;

        }
    }
}
