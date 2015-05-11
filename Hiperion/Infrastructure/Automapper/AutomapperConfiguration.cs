namespace Hiperion.Infrastructure.Automapper
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class AutomapperConfiguration
	{
		public static void Configure()
		{
			var configurators = Assembly.GetExecutingAssembly().GetTypes()
				.Where(t => typeof(IObjectMapperConfigurator).IsAssignableFrom(t)
							&& !t.IsAbstract
							&& !t.IsInterface)
				.Select(Activator.CreateInstance).OfType<IObjectMapperConfigurator>();

			foreach (var configurator in configurators)
			{
				configurator.Apply();
			}
		}
	}
}
