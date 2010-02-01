using System;

namespace ProAceFx.Settings
{
	/// <summary>
	/// Provides a mechanism for resolving keys for settings.
	/// </summary>
	public interface ISettingKeyResolver
	{
		/// <summary>
		/// Resolves the key for the specific type.
		/// </summary>
		/// <param name="type">The type of settings class.</param>
		/// <param name="key">The key being resolved.</param>
		/// <returns></returns>
		string Resolve(Type type, string key);
		/// <summary>
		/// Gets a flag indicating whether the resolution is case sensitive.
		/// </summary>
		bool IsCaseSensitive { get; }
	}
}
