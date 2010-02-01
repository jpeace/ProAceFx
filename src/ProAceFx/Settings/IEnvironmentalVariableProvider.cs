namespace ProAceFx.Settings
{
	/// <summary>
	/// Provides access to environmental variables.
	/// </summary>
	public interface IEnvironmentalVariableProvider
	{
		/// <summary>
		/// Expands all environmental variables within the specified input.
		/// </summary>
		/// <param name="input">The string to expand.</param>
		/// <returns></returns>
		string ExpandEnvironmentalVariables(string input);
	}
}
