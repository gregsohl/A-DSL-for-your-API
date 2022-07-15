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
			TurtleScriptValue result = TurtleScriptValue.NULL;

			// Find the function, if it exists.
			if (context.TryGetFunction(
					m_FunctionName,
					m_Parameters.Length,
					out TokenFunctionDeclaration function))
			{
				context.PushScope(m_FunctionName);

				for (var parameterIndex = 0; parameterIndex < m_Parameters.Length; parameterIndex++)
				{
					TokenBase parameter = m_Parameters[parameterIndex];
					string parameterName = function.ParameterNames[parameterIndex];
					TurtleScriptValue parameterValue = parameter.Visit(context);
					context.SetVariableValue(parameterName, VariableType.Parameter, null, parameterValue);
				}

				result = function.FunctionBody.Visit(context);

				context.PopScope();
			}

			// Push scope onto the context
			// Register parameter values in the context
			// Call the function body
			//context.G

			return result;
		}
	}
}
