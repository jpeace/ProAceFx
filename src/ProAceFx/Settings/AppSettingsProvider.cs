using System.Collections.Specialized;
using System.Configuration;

namespace ProAceFx.Settings
{
	/// <summary>
	/// Provides a mechanism for retrieving settings through the .NET Configuration API.
	/// </summary>
	public class AppSettingsProvider : ISettingsProvider
	{
		/// <summary>
		/// Gets all settings.
		/// </summary>
		/// <returns></returns>
		public NameValueCollection GetSettings()
		{
			return ConfigurationManager.AppSettings;
		}
	}
}
