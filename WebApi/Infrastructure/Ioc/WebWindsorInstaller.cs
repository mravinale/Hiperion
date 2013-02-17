namespace WebApi.Infrastructure.Ioc
{
    using System.Web.Http.Controllers;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using WebApi.Controllers;

    using System.Web.Http;

    using WebApi.Domain.Repositories;
    using WebApi.Infrastructure.Automapper;
    using WebApi.Infrastructure.EF;

    internal class WebWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            container.Register( 

                Classes.FromAssemblyContaining<UserController>()
                       .BasedOn<IHttpController>()
                       .LifestyleTransient(),

                Component.For<IDbContext>()
                         .ImplementedBy<UserContext>()
                         .LifestylePerWebRequest()
                         .Named("UserContext")
                         .DependsOn(Parameter.ForKey("nameOrConnectionString").Eq(connectionString)),

                Component.For<IUserRepository>()
                         .ImplementedBy<UserRepository>()
                         .LifestyleTransient(),

                AllTypes.FromThisAssembly()
                        .Where(type => type.Name.EndsWith("Services"))
                        .LifestyleTransient()
            );

            AutomapperConfiguration.Configure(container.Resolve);
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(container);
        }
    }
}