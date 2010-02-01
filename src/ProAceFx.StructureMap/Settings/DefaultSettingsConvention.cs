using System;
using ProAceFx.Settings;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace ProAceFx.StructureMap.Settings
{
	/// <summary>
	/// Provides a default registration convention for settings classes.
	/// </summary>
	public class DefaultSettingsConvention : IRegistrationConvention
	{
		/// <summary>
		/// Processes the specified type.
		/// </summary>
		/// <param name="type">The type to process.</param>
		/// <param name="registry">Registry to configure.</param>
		public void Process(Type type, Registry registry)
		{
			if (!type.Name.EndsWith("Settings"))
			{
				return;
			}

			registry
				.For(type)
				.EnrichWith((session, original) => session.GetInstance<SettingsPropertyVisitor>().Visit(original))
				.EnrichWith((session, original) => session.GetInstance<ExpandEnvironmentalVariablePropertyVisitor>().Visit(original));
		}
	}
}