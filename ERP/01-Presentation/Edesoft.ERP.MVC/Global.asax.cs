using Edesoft.ERP.Domain.DataBase;
using Edesoft.ERP.Infra.IoC;
using Edesoft.ERP.Infra.Repositories.Global;
using Edesoft.ERP.MVC.MVC;
using Microsoft.Practices.ServiceLocation;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Edesoft.ERP.MVC
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());

			IoC.Init();

			DependencyResolver.SetResolver(new IoCDependencyResolver());

			var contextManager = ServiceLocator.Current.GetInstance<ContextManager>();
			((EdesoftDataBaseContext)contextManager.Context).Initialize();
		}
	}
}
