namespace ProAceFx.Settings
{
	/// <summary>
	/// Provides a decorator for objects to visits properties.
	/// </summary>
	public interface IPropertyVisitor
	{
		/// <summary>
		/// Instructs the visitor to visit the specified instance. of <typeparamref name="TInstance"/>.
		/// </summary>
		/// <typeparam name="TInstance">Type of instance to visit.</typeparam>
		/// <param name="instance">The instance to visit.</param>
		/// <returns></returns>
		TInstance Visit<TInstance>(TInstance instance);
	}
}
