using System;
using System.Collections.Generic;
using System.Linq;

namespace ProAceFx.Settings
{
	/// <summary>
	/// Provides a mechanism for retrieving property metadata for types via reflection.
	/// </summary>
	public class ReflectionPropertyInfoProvider : IPropertyInfoProvider
	{
		/// <summary>
		/// Retrieves a collection of property metadata objects for the specified instance of <typeparamref name="TInstance"/>.
		/// </summary>
		/// <typeparam name="TInstance">The type of instance to analyze.</typeparam>
		/// <param name="instance">The instance to analyze.</param>
		/// <returns></returns>
		public IEnumerable<IPropertyInfo> GetProperties<TInstance>(TInstance instance)
		{
			return GetProperties(instance.GetType());
		}
		/// <summary>
		/// Retrieves a collection of property metadata objects for the specified type.
		/// </summary>
		/// <param name="targetType">The typeto analyze.</param>
		/// <returns></returns>
		public IEnumerable<IPropertyInfo> GetProperties(Type targetType)
		{
			return targetType.GetProperties().Select(p => new PropertyInfoAdapter(p) as IPropertyInfo);
		}
	}
}
