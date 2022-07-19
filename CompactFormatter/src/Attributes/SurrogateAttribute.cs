using System;

namespace CompactFormatter.Attributes
{
	/// <summary>
	/// A Surrogate is a static method used to instantiate a particular class.
	/// This is necessary because CompactFormatter have no idea about how to
	/// instantiate a class using reflection, usually it invokes a parameterless
	/// constructor but sometimes, in the runtime classes, this constructor is not
	/// present or, even worse, no constructor at all is present.
	/// In these situations, a surrogate can be used by the CompactFormatter to 
	/// build an "uninitialized" instance of the class.
	/// Remember, all Surrogates MUST BE static methods!
	/// </summary>
	[AttributeUsage(AttributeTargets.Method,AllowMultiple = true, Inherited = false)]
	public class SurrogateAttribute : System.Attribute
	{
		private Type surrogateOf;

		public Type SurrogateOf
		{
			get
			{
				return surrogateOf;
			}
		}

		public SurrogateAttribute(Type surrogateOf)
		{
			this.surrogateOf = surrogateOf;
		}
	}
}
