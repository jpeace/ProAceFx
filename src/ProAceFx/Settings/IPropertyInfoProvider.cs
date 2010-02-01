using System;
using System.Collections.Generic;

namespace ProAceFx.Settings
{
	/// <summary>
	/// Provides a mechanism for retrieving property metadata for types.
	/// </summary>
	public interface IPropertyInfoProvider
	{
		/// <summary>
		/// Retrieves a collection of property metadata objects for the specified instance of <typeparamref name="TInstance"/>.
		/// </summary>
		/// <typeparam name="TInstance">The type of instance to analyze.</typeparam>
		/// <param name="instance">The instance to analyze.</param>
		/// <returns></returns>
		IEnumerable<IPropertyInfo> GetProperties<TInstance>(TInstance instance);
		/// <summary>
		/// Retrieves a collection of property metadata objects for the specified type.
		/// </summary>
		/// <param name="targetType">The typeto analyze.</param>
		/// <returns></returns>
		IEnumerable<IPropertyInfo> GetProperties(Type targetType);
	}
}
