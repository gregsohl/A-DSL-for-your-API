namespace TurtleScript.Interpreter.Tokenize
{
	public abstract class TokenValue : TokenBase
	{
		protected TokenValue(
			TokenType tokenType,
			int lineNumber,
			int charPositionInLine)
			: base(tokenType,
				lineNumber,
				charPositionInLine)
		{
		}
	}
}
