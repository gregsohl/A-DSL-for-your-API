namespace TurtleScript.Interpreter.Tokenize
{
	public abstract class TokenValue : TokenBase
	{
		protected TokenValue(TokenType tokenType)
			: base(tokenType)
		{
		}
	}
}
