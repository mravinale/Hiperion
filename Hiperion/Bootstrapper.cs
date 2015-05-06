namespace Hiperion
{
    using Castle.Windsor;
    using Infrastructure.Ioc;

    public static class Bootstrapper
	{
		public static IWindsorContainer InitializeContainer()
		{
            return new WindsorContainer().Install(new WebWindsorInstaller());
		}

		public static void Release(IWindsorContainer container)
		{
			container.Dispose();
		}
	}
}
