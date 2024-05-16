using LibraWebApp.Utility;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Services.Description;
using Libra.Dal.Context;
using System.Data.Entity;

namespace LibraWebApp
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			NinjectModule registrations = new NinjectRegistrations();
			var kernel = new StandardKernel(registrations);
			DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            //Database.SetInitializer(new LibraDbInitializer());

            ///Database.SetInitializer(new MigrateDatabaseToLatestVersion<LibraContext, Libra.Dal.Migrations.Configuration>());
            
        }
    }
}
