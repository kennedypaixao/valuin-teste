using Edesoft.ERP.Domain.DataBase;
using Microsoft.Practices.ServiceLocation;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web;

namespace Edesoft.ERP.Infra.Repositories.Global
{
    public class ContextManager
    {
        private const string ContextKey = "ContextManager.Context";
        private const string DBSqlServerKey = "ContextManager.SqlServer";

        public DbContext Context
        {
            get
            {
                if (HttpContext.Current != null)
                {

                    if (HttpContext.Current.Items[ContextKey] == null)
                    {
                        var context = new EdesoftDataBaseContext();
#if DEBUG
                        context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
                        HttpContext.Current.Items[ContextKey] = context;
                    }

                    return (EdesoftDataBaseContext)HttpContext.Current.Items[ContextKey];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Items[ContextKey] = value;
            }
        }

        public SqlConnection ContextSQLServer
        {
            get
            {

                if (HttpContext.Current != null)
                {

                    if (HttpContext.Current.Items[DBSqlServerKey] == null)
                    {
                        var context = new SqlConnection(Context.Database.Connection.ConnectionString);
                        HttpContext.Current.Items[DBSqlServerKey] = context;
                    }

                    return (SqlConnection)HttpContext.Current.Items[DBSqlServerKey];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Items[DBSqlServerKey] = value;
            }
        }
        public void NewInstanceContext()
        {
            var contextManager = ServiceLocator.Current.GetInstance<ContextManager>();
            var context = new EdesoftDataBaseContext();
            var sqlServer = new SqlConnection(context.Database.Connection.ConnectionString);
            contextManager.Context = context;
            contextManager.ContextSQLServer = sqlServer;

        }
    }
}
