using System;
using System.Collections.Specialized;
using NUnit.Framework;
using ProAceFx.Settings;
using Rhino.Mocks;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace ProAceFx.Tests.Settings.Scenarios.SettingsWithVariables
{
	/// <summary>
	/// Provides a scenario for testing settings with variables.
	/// </summary>
	public class Scenario : BaseScenario
	{
		[Test]
		public void Variable_Value_Is_Expanded()
		{
			string replacedValue = Guid.NewGuid().ToString("N");
			Context
				.SettingsProvider
				.Expect(p => p.GetSettings())
				.Return(new NameValueCollection
				        	{
								{"TestSettings.SettingA", "%X%"}
				        	});
			Context
				.EnvironmentalVariableProvider
				.Expect(p => p.ExpandEnvironmentalVariables("%X%"))
				.IgnoreArguments()
				.Return(replacedValue);

			Context.MockRepository.ReplayAll();

			var settings = ObjectFactory.GetInstance<TestSettings>();
			Assert.AreEqual(settings.SettingA, replacedValue);
		}
		/// <summary>
		/// Creates a registry for the scenario if applicable.
		/// </summary>
		/// <returns></returns>
		protected override Registry ScenarioRegistry()
		{
			return new SettingsWithVariablesRegistry();
		}
		/// <summary>
		/// Scenario-specific registry.
		/// </summary>
		public class SettingsWithVariablesRegistry : Registry
		{
			public SettingsWithVariablesRegistry()
			{
				For<TestSettings>().Use<TestSettings>();
			}
		}
	}
	/// <summary>
	/// Test settings.
	/// </summary>
	public class TestSettings
	{
		[ExpandEnvironmentalVariable]
		public string SettingA { get; set; }
	}
}
