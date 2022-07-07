namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenNullValue : TokenValue
	{
		public TokenNullValue()
			: base(TokenType.NullValue)
		{
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return TurtleScriptValue.NULL;
		}
	}
}
