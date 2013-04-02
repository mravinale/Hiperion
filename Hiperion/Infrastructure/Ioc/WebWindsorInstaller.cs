using System.Configuration;
using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Hiperion.Controllers;
using System.Web.Http;
using Hiperion.Infrastructure.Automapper;
using Hiperion.Infrastructure.EF;

namespace Hiperion.Infrastructure.Ioc
{
    internal class WebWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            container.Register( 

                Component.For<IDbContext>()
                         .ImplementedBy<UserContext>()
                         .LifestylePerWebRequest()
                         .DependsOn(Parameter.ForKey("nameOrConnectionString").Eq(connectionString)),
                         
                AllTypes.FromThisAssembly()
                        .Where(type => type.Name.EndsWith("Services") || 
                                       type.Name.EndsWith("Repository") ||
                                       type.Name.EndsWith("Controller"))
                        .WithService.DefaultInterfaces()
                        .LifestyleTransient()
            );

            AutomapperConfiguration.Configure(container.Resolve);
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(container);
        }
    }
}