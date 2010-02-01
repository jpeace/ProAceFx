using System;

namespace ProAceFx.Settings
{
	/// <summary>
	/// Discovers the attributes of a property and provides access to property metadata.
	/// </summary>
	public interface IPropertyInfo
	{
		/// <summary>
		/// Gets the name of the property.
		/// </summary>
		string Name { get; }
		/// <summary>
		/// Gets the type of the property.
		/// </summary>
		Type PropertyType { get; }
		/// <summary>
		/// Gets the value of the property.
		/// </summary>
		/// <param name="instance">The associated instance.</param>
		/// <param name="index"></param>
		/// <returns></returns>
		object GetValue(object instance, object[] index);
		/// <summary>
		/// Sets the value of the property.
		/// </summary>
		/// <param name="instance">The associated instance.</param>
		/// <param name="value">The value to set.</param>
		/// <param name="index"></param>
		void SetValue(object instance, object value, object[] index);
		/// <summary>
		/// Returns an array of the associated types.
		/// </summary>
		/// <param name="attributeType"></param>
		/// <param name="inherit"></param>
		/// <returns></returns>
		object[] GetCustomAttributes(Type attributeType, bool inherit);
	}
}
