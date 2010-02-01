using ProAceFx.Settings;
using Rhino.Mocks;

namespace ProAceFx.Tests.Settings.Scenarios
{
	/// <summary>
	/// Provides a context for integration scenarios.
	/// </summary>
	public class ScenarioContext
	{
		private readonly MockRepository _mockRepository;
		private readonly ISettingsProvider _settingsProvider;
		private readonly IEnvironmentalVariableProvider _environmentalVariableProvider;
		/// <summary>
		/// Initializes a new instance of the <see cref="ScenarioContext"/> class.
		/// </summary>
		/// <param name="mockRepository">The mock repository.</param>
		public ScenarioContext(MockRepository mockRepository)
		{
			_mockRepository = mockRepository;
			_settingsProvider = _mockRepository.DynamicMock<ISettingsProvider>();
			_environmentalVariableProvider = _mockRepository.DynamicMock<IEnvironmentalVariableProvider>();
		}
		/// <summary>
		/// Gets the mocked environmental variable provider.
		/// </summary>
		public IEnvironmentalVariableProvider EnvironmentalVariableProvider
		{
			get { return _environmentalVariableProvider; }
		}
		/// <summary>
		/// Gets the mocked settings provider.
		/// </summary>
		public ISettingsProvider SettingsProvider
		{
			get { return _settingsProvider; }
		}
		/// <summary>
		/// Gets the underlying mock repository.
		/// </summary>
		public MockRepository MockRepository
		{
			get { return _mockRepository; }
		}
	}
}