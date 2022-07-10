#region Namespaces

using System;
using System.Text;
using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	internal class TokenFunctionCall : TokenBase
	{
		private readonly string m_FunctionName;
		private readonly TokenBase[] m_Parameters;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TokenFunctionCall(
			string functionName,
			TokenBase[] parameters)
			: base(TokenType.FunctionDecl)
		{
			m_FunctionName = functionName;
			m_Parameters = parameters;
		}

		public string FunctionName
		{
			get { return m_FunctionName; }
		}

		public TokenBase[] Parameters
		{
			get { return m_Parameters; }
		}

		public override string ToTurtleScript()
		{
			StringBuilder result = new StringBuilder(FunctionName + "(");

			for (var index = 0; index < Parameters.Length; index++)
			{
				var parameter = Parameters[index];
				result.Append(parameter.ToTurtleScript(result, 0));
				if (index < Parameters.Length - 1)
				{
					result.Append(',');
				}
			}

			result.Append(')');

			return result.ToString();
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			TurtleScriptValue result = null;

			// Find the function, if it exists.
			// Push scope onto the context
			// Register parameter values in the context
			// Call the function body
			//context.G

			return result;
		}
	}
}
