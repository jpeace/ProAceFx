using Microsoft.Practices.ServiceLocation;
using ProAceFx.Settings;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace ProAceFx.StructureMap.Settings
{
	/// <summary>
	/// Provides a default registration mechanism for settings classes.
	/// </summary>
	public class DefaultSettingsRegistry : Registry
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultSettingsRegistry"/> class using
		/// the default registration convention.
		/// </summary>
		public DefaultSettingsRegistry()
		{
			For<IServiceLocator>().Use<StructureMapServiceLocator>();
			For<IPropertyInfoProvider>().Use<ReflectionPropertyInfoProvider>();
			For<ISettingKeyResolver>().Use<ClassPrefixSettingKeyResolver>();
			Scan(s =>
			     	{
						s.AssembliesFromApplicationBaseDirectory();
			     		s.With(new DefaultSettingsConvention());
			     	});

			ServiceLocator.SetLocatorProvider(ObjectFactory.GetInstance<IServiceLocator>);
		}
	}
}