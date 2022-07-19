namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenPi : TokenNumericValue
	{
		public TokenPi()
		{
		}

		public TokenPi(
			int lineNumber,
			int columnNumber)
			: base(
				3.141592654,
				lineNumber,
				columnNumber)
		{
		}

		public override string ToTurtleScript()
		{
			return "pi";
		}
	}
}