using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;

namespace WebApi
{
    public class WebApiApplication : HttpApplication
    {
        private static IWindsorContainer container;

        public IWindsorContainer Container { get { return container; } }

        protected void Application_Start()
        {
            container = Bootstrapper.InitializeContainer();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UserContext>());
        }

        protected void Application_Stop()
        {
            Bootstrapper.Release(container);
        }
    }
}