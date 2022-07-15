#region Namespaces

using System;

using Antlr4.Runtime.Tree;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.ImmediateInterpreter;
using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

#endregion Namespaces

namespace TurtleScript.Interpreter
{
	public static class TurtleScriptFunctionFactory
	{
		#region Public Methods

		public static ITurtleScriptFunction CreateFunction<T>(
			string name,
			int parameterCount,
			TokenFunctionDeclaration declaration)
		{
			if (typeof(T) == typeof(TurtleScriptFunction))
			{
				return new TurtleScriptFunction(
					name,
					null,
					null);
			}

			if (typeof(T) == typeof(TurtleScriptParserFunction))
			{
				return new TurtleScriptParserFunction(
					name,
					parameterCount,
					declaration);
			}

			if (typeof(T) == typeof(TurtleScriptExecutionFunction))
			{
				return new TurtleScriptExecutionFunction(
					name,
					parameterCount,
					declaration);
			}

			throw new InvalidOperationException();
		}

		#endregion Public Methods
	}
}
