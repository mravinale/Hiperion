namespace WebApi.Infrastructure.Ioc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;

    using Castle.Windsor;

    internal sealed class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            this.container = container;
        }

        public object GetService(Type t)
        {
            return this.container.Kernel.HasComponent(t)? this.container.Resolve(t) : null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return this.container.ResolveAll(t).Cast<object>().ToArray();
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(this.container);
        }

        public void Dispose(){}
    }
}