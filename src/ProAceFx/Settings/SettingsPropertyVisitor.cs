using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ProAceFx.Settings
{
	/// <summary>
	/// Provides a decorating mechanism for POCO classes to provide configuration settings.
	/// </summary>
	public class SettingsPropertyVisitor : BasePropertyVisitor
	{
		private readonly ISettingKeyResolver _resolver;
		private readonly NameValueCollection _settingsCache;
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsPropertyVisitor"/> class.
		/// </summary>
		/// <param name="propertyProvider">The property provider.</param>
		/// <param name="settingsProvider">The settings provider.</param>
		/// <param name="resolver">The setting key resolver.</param>
		public SettingsPropertyVisitor(IPropertyInfoProvider propertyProvider, ISettingsProvider settingsProvider, ISettingKeyResolver resolver)
			: base(propertyProvider)
		{
			if (settingsProvider == null)
			{
				throw new ArgumentNullException("settingsProvider");
			}
			if (resolver == null)
			{
				throw new ArgumentNullException("resolver");
			}

			_settingsCache = settingsProvider.GetSettings();
			_resolver = resolver;
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
			foreach (var setting in _settingsCache.AllKeys)
			{
				string key = _resolver.Resolve(instance.GetType(), setting);
				IPropertyInfo property =
					properties.Single(p => (!_resolver.IsCaseSensitive && p.Name.ToLower() == key.ToLower())
													|| (_resolver.IsCaseSensitive && p.Name == key));
				SetPropertyValue(property, instance, _settingsCache[setting]);
			}

			return instance;
		}
	}
}
