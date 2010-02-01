using NUnit.Framework;
using ProAceFx.Settings;
using ProAceFx.StructureMap.Settings;
using Rhino.Mocks;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace ProAceFx.Tests.Settings.Scenarios
{
	/// <summary>
	/// Provides a base class for integration scenarios to test members of the <see cref="ProAceFx.Settings"/> namespace.
	/// </summary>
	[TestFixture]
	public abstract class BaseScenario
	{
		private ScenarioContext _context;
		[SetUp]
		public void Initialize()
		{
			_context = new ScenarioContext(new MockRepository());
			ObjectFactory.Initialize(x =>
			{
				// Common
				x.AddRegistry<DefaultSettingsRegistry>();

				// App-specific
				x.For<ISettingsProvider>().Use(_context.SettingsProvider);
				x.For<IEnvironmentalVariableProvider>().Use(_context.EnvironmentalVariableProvider);

				// Scenarios
				var scenarioRegistry = ScenarioRegistry();
				if(scenarioRegistry != null)
				{
					x.AddRegistry(scenarioRegistry);
				}
			});
		}
		/// <summary>
		/// Gets to current context.
		/// </summary>
		public ScenarioContext Context
		{
			get { return _context; }
		}
		/// <summary>
		/// Creates a registry for the scenario if applicable.
		/// </summary>
		/// <returns></returns>
		protected abstract Registry ScenarioRegistry();
	}
}