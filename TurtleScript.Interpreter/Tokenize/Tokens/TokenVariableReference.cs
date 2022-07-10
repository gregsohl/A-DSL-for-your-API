using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenVariableReference : TokenBase
	{
		private readonly string m_VariableName;

		public TokenVariableReference(string variableName)
			: base(TokenType.VariableReference)
		{
			m_VariableName = variableName;
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
	}
}