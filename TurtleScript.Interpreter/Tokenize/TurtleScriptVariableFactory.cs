#region Namespaces

using System;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	public static class TurtleScriptVariableFactory
	{
		#region Public Methods

		public static ITurtleScriptVariable<T> CreateVariable<T>(
			string name,
			VariableType type,
			TokenBase declaration)
		{
			if (typeof(T) == typeof(TurtleScriptParserVariable))
			{
				return (ITurtleScriptVariable<T>)new TurtleScriptParserVariable(
					name,
					type,
					declaration);
			}

			if (typeof(T) == typeof(TurtleScriptExecutionVariable))
			{
				return (ITurtleScriptVariable<T>)new TurtleScriptExecutionVariable(
					name,
					type,
					declaration);
			}

			throw new InvalidOperationException();
		}

		#endregion Public Methods
	}
}
