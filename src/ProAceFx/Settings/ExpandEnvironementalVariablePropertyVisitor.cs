namespace ProAceFx.Settings
{
	/// <summary>
	/// Provides a mechanism for automatically expanding environmental variables.
	/// </summary>
	public class ExpandEnvironmentalVariablePropertyVisitor : BasePropertyWithAttributeVisitor<ExpandEnvironmentalVariableAttribute>
	{
		private readonly IEnvironmentalVariableProvider _environmentalVariableProvider;
		/// <summary>
		/// Initializes a new instance of the <see cref="ExpandEnvironmentalVariablePropertyVisitor"/> class.
		/// </summary>
		/// <param name="propertyProvider">The property provider.</param>
		/// <param name="environmentalVariableProvider">The environmental variable provider.</param>
		public ExpandEnvironmentalVariablePropertyVisitor(IPropertyInfoProvider propertyProvider, IEnvironmentalVariableProvider environmentalVariableProvider) 
			: base(propertyProvider)
		{
			_environmentalVariableProvider = environmentalVariableProvider;
		}

		/// <summary>
		/// When overriden in a child class, visits all of the specified properties on the specified instance.
		/// </summary>
		/// <typeparam name="TInstance">Type of instance to visit.</typeparam>
		/// <param name="attribute">The associated attribute.</param>
		/// <param name="property">The sets of property to visit.</param>
		/// <param name="instance">The associated instance.</param>
		/// <returns></returns>
		protected override TInstance Visit<TInstance>(ExpandEnvironmentalVariableAttribute attribute, IPropertyInfo property, TInstance instance)
		{
			string expandedValue = _environmentalVariableProvider.ExpandEnvironmentalVariables(property.GetValue(instance, null).ToString());
			property.SetValue(instance, expandedValue, null);

			return instance;
		}
	}
}
