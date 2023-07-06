using Edesoft.ERP.Domain.DataBase;
using Edesoft.ERP.Domain.Interfaces;
using Edesoft.ERP.Domain.Interfaces.Security;
using Edesoft.ERP.Infra.Repositories.Global;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using System.Linq;
using System.Text;
using Dapper;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System;
using System.Web.UI.WebControls;
using Edesoft.ERP.Tools.Extensions;
using System.Runtime.InteropServices;
using System.Web;

namespace Edesoft.ERP.Infra.Repositories.Security
{
    public class SecurityRepository : RepositoryBase<Usuarios>, ISecurityRepository
    {
        public Usuarios Login(string Email, string Password)
        {
            string _password = Password.Encrypt();
            return Context.Usuarios.Where(w => w.Email == Email && w.Senha == _password && w.Ativo).FirstOrDefault();
        }

    }
}
