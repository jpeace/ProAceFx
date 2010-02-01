using System;
using System.Reflection;

namespace ProAceFx.Settings
{
	public class PropertyInfoAdapter : IPropertyInfo
	{
		private readonly PropertyInfo _propertyInfo;
		/// <summary>
		/// Initializes a new instance of the <see cref="PropertyInfoAdapter"/> class from the 
		/// specified <see cref="PropertyInfo"/> instance.
		/// </summary>
		/// <param name="propertyInfo">The property info to adapt.</param>
		public PropertyInfoAdapter(PropertyInfo propertyInfo)
		{
			_propertyInfo = propertyInfo;
		}
		/// <summary>
		/// Gets the name of the property.
		/// </summary>
		public string Name
		{
			get { return _propertyInfo.Name; }
		}

		/// <summary>
		/// Gets the type of the property.
		/// </summary>
		public Type PropertyType
		{
			get { return _propertyInfo.PropertyType; }
		}

		/// <summary>
		/// Gets the value of the property.
		/// </summary>
		/// <param name="instance">The associated instance.</param>
		/// <param name="index"></param>
		/// <returns></returns>
		public object GetValue(object instance, object[] index)
		{
			return _propertyInfo.GetValue(instance, index);
		}

		/// <summary>
		/// Sets the value of the property.
		/// </summary>
		/// <param name="instance">The associated instance.</param>
		/// <param name="value">The value to set.</param>
		/// <param name="index"></param>
		public void SetValue(object instance, object value, object[] index)
		{
			_propertyInfo.SetValue(instance, value, index);
		}

		/// <summary>
		/// Returns an array of the associated types.
		/// </summary>
		/// <param name="attributeType"></param>
		/// <param name="inherit"></param>
		/// <returns></returns>
		public object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return _propertyInfo.GetCustomAttributes(attributeType, inherit);
		}
	}
}
