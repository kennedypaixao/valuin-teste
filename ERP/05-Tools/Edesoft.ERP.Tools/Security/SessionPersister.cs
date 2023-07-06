using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Edesoft.ERP.Tools.Security
{
    public static class SessionPersister
    {
        static string usernameSessionvar = "EdesoftAcessControl";
        public static bool loggedViaSocialNetwork = false;
        public static string photoSocialNetwork = null;
        public enum Permissions
        {
            PRE_VENDA_INFORMAR_CODIGO_VENDEDOR,
            ORCAMENTO_INFORMAR_CODIGO_VENDEDOR,
        }
        public static bool hasPermission(Permissions permission)
        {
            return true;
        }

        public static AccountPersister User
        {
            get
            {
                var retorno = new AccountPersister();

                if (HttpContext.Current.Session == null)
                {
                    return retorno;
                }

                if (HttpContext.Current.Session[usernameSessionvar] != null)
                {
                    retorno = (AccountPersister)HttpContext.Current.Session[usernameSessionvar];
                    return retorno;
                }

                return retorno;
            }
            set
            {
                HttpContext.Current.Session[usernameSessionvar] = value;
            }
        }


    }
}
