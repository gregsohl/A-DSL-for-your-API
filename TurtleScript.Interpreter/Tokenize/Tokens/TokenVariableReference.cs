#region Namespaces

using System;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenVariableReference : TokenBase
	{
		static TokenVariableReference()
		{
			m_Default = new TokenVariableReference(String.Empty);
		}

		public TokenVariableReference(
			string variableName)
			: this(variableName, 0, 0)
		{
		}

		public TokenVariableReference(
			string variableName,
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.VariableReference,
				lineNumber,
				charPositionInLine)
		{
			m_VariableName = variableName;
		}

		public new static TokenVariableReference Default
		{
			get { return m_Default; }
		}

		public string VariableName
		{
			get { return m_VariableName; }
		}

		public override string ToTurtleScript()
		{
			return VariableName;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			var variableValue = context.GetVariableValue(VariableName);

			return variableValue;
		}

		#region Private Fields

		private readonly string m_VariableName;
		private static TokenVariableReference m_Default;

		#endregion Private Fields

	}
}