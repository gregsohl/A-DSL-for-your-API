#region Namespaces

using System;
using System.Diagnostics;
using System.Text;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces


namespace TurtleScript.Interpreter.Tokenize
{
	internal class TokenFunctionCall : TokenBase
	{

		#region Public Constructors

		static TokenFunctionCall()
		{
			m_Default = new TokenFunctionCall(
				string.Empty,
				Array.Empty<TokenBase>());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TokenFunctionCall(
			string functionName,
			TokenBase[] parameters)
			: base(TokenType.FunctionDecl)
		{
			m_FunctionName = functionName;
			m_Parameters = parameters;
		}

		#endregion Public Constructors


		#region Public Properties

		public new static TokenFunctionCall Default
		{
			[DebuggerStepThrough]
			get { return m_Default; }
		}

		public string FunctionName
		{
			get { return m_FunctionName; }
		}

		public TokenBase[] Parameters
		{
			get { return m_Parameters; }
		}

		#endregion Public Properties


		#region Public Methods

		public override string ToTurtleScript()
		{
			StringBuilder result = new StringBuilder(FunctionName + "(");

			for (var index = 0; index < Parameters.Length; index++)
			{
				var parameter = Parameters[index];
				result.Append(parameter.ToTurtleScript());
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

		#endregion Public Methods


		#region Private Fields

		private static TokenFunctionCall m_Default;
		private readonly string m_FunctionName;
		private readonly TokenBase[] m_Parameters;

		#endregion Private Fields
	}
}
