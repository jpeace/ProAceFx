using System;
using System.Collections.Generic;

namespace ProAceFx.Settings
{
	/// <summary>
	/// Provides a base class for property visitors that require attributes.
	/// </summary>
	/// <typeparam name="TAttribute">The type of attribute to require.</typeparam>
	public abstract class BasePropertyWithAttributeVisitor<TAttribute> : BasePropertyVisitor
		where TAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BasePropertyWithAttributeVisitor{TAttribute}"/> class.
		/// </summary>
		/// <param name="propertyProvider">The property provider.</param>
		protected BasePropertyWithAttributeVisitor(IPropertyInfoProvider propertyProvider)
			: base(propertyProvider)
		{
		}

		/// <summary>
		/// When overriden in a child class, visits all of the specified properties on the specified instance.
		/// </summary>
		/// <typeparam name="TInstance">Type of instance to visit.</typeparam>
		/// <param name="properties">The sets of properties to visit.</param>
		/// <param name="instance">The associated instance.</param>
		/// <returns></returns>
		protected override TInstance Visit<TInstance>(IEnumerable<IPropertyInfo> properties, TInstance instance)
		{
			foreach (var property in properties)
			{
				if (property.PropertyType != typeof(string))
				{
					continue;
				}

				TAttribute attribute = GetAttribute<TAttribute>(property);
				if (attribute == null)
				{
					continue;
				}

				instance = Visit(attribute, property, instance);
			}

			return instance;
		}

		/// <summary>
		/// When overriden in a child class, visits all of the specified properties on the specified instance.
		/// </summary>
		/// <typeparam name="TInstance">Type of instance to visit.</typeparam>
		/// <param name="attribute">The associated attribute.</param>
		/// <param name="property">The sets of property to visit.</param>
		/// <param name="instance">The associated instance.</param>
		/// <returns></returns>
		protected abstract TInstance Visit<TInstance>(TAttribute attribute, IPropertyInfo property, TInstance instance);
	}
}
