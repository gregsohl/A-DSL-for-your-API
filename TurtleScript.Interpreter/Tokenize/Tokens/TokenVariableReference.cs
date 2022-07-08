using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenVariableReference : TokenBase
	{
		public TokenVariableReference(string variableName)
			: base(TokenType.VariableReference)
		{
			VariableName = variableName;
		}

		public string VariableName { get; }

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