using System;

namespace ProAceFx.Settings
{
	/// <summary>
	/// Provides custom type conversion operations.
	/// </summary>
	public interface ITypeConverter
	{
		/// <summary>
		/// Converts the value to the specified destination type.
		/// </summary>
		/// <param name="destinationType">The type to convert to.</param>
		/// <param name="value">The value to convert.</param>
		/// <returns></returns>
		object ConvertTo(Type destinationType, object value);
	}
}
