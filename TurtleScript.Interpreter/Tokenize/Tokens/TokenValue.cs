namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public abstract class TokenValue : TokenBase
	{
		protected TokenValue()
		{
		}

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
