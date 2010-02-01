using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace ProAceFx.Settings
{
	/// <summary>
	/// Provides a base class for property visitors.
	/// </summary>
	public abstract class BasePropertyVisitor : IPropertyVisitor
	{
		private readonly IPropertyInfoProvider _propertyProvider;
		/// <summary>
		/// Initializes a new instance of the <see cref="BasePropertyVisitor"/> class with the specified provider.
		/// </summary>
		/// <param name="propertyProvider">The property provider.</param>
		protected BasePropertyVisitor(IPropertyInfoProvider propertyProvider)
		{
			_propertyProvider = propertyProvider;
		}
		/// <summary>
		/// Gets the specified attribute type for the property.
		/// </summary>
		/// <typeparam name="TAttribute">Type of attribute to retrieve.</typeparam>
		/// <param name="property">The property.</param>
		/// <returns></returns>
		public TAttribute GetAttribute<TAttribute>(IPropertyInfo property)
		{
			object[] attributes = property.GetCustomAttributes(typeof(TAttribute), true);
			if (attributes.Length == 1)
			{
				return (TAttribute)attributes[0];
			}

			return default(TAttribute);
		}
		/// <summary>
		/// Sets the value of the specified property.
		/// </summary>
		/// <param name="property">The property to set.</param>
		/// <param name="instance">The instance to set.</param>
		/// <param name="value">The value to set.</param>
		public void SetPropertyValue(IPropertyInfo property, object instance, object value)
		{
			TypeConverterAttribute attribute = GetAttribute<TypeConverterAttribute>(property);
			object convertedValue;
			if (attribute != null)
			{
				Type converterType = Type.GetType(attribute.ConverterTypeName, true, true);
				ITypeConverter converter = (ITypeConverter)ServiceLocator.Current.GetInstance(converterType);
				convertedValue = converter.ConvertTo(property.PropertyType, value);
			}
			else
			{
				convertedValue = Convert.ChangeType(value, property.PropertyType);
			}

			property.SetValue(instance, convertedValue, null);
		}
		/// <summary>
		/// Instructs the visitor to visit the specified instance. of <typeparamref name="TInstance"/>.
		/// </summary>
		/// <typeparam name="TInstance">Type of instance to visit.</typeparam>
		/// <param name="instance">The instance to visit.</param>
		/// <returns></returns>
		public TInstance Visit<TInstance>(TInstance instance)
		{
			var properties = _propertyProvider.GetProperties(instance);
			if (properties == null || !properties.Any())
			{
				return instance;
			}

			return Visit(properties, instance);
		}
		/// <summary>
		/// When overriden in a child class, visits all of the specified properties on the specified instance.
		/// </summary>
		/// <typeparam name="TInstance">Type of instance to visit.</typeparam>
		/// <param name="properties">The sets of properties to visit.</param>
		/// <param name="instance">The associated instance.</param>
		/// <returns></returns>
		protected abstract TInstance Visit<TInstance>(IEnumerable<IPropertyInfo> properties, TInstance instance);
	}
}
