using System.Collections.Specialized;

namespace ProAceFx.Settings
{
	/// <summary>
	/// Provides a mechanism for retrieving settings.
	/// </summary>
	public interface ISettingsProvider
	{
		/// <summary>
		/// Gets all settings.
		/// </summary>
		/// <returns></returns>
		NameValueCollection GetSettings();
	}
}
