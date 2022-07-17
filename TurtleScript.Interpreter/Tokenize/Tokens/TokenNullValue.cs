using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenNullValue : TokenValue
	{
		public TokenNullValue()
			: base(TokenType.NullValue, 0, 0)
		{
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return TurtleScriptValue.NULL;
		}
	}
}
