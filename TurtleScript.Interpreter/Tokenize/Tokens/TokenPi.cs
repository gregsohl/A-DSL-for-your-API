namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenPi : TokenNumericValue
	{
		public TokenPi()
			: base(3.141592654)
		{
		}

		public override string ToTurtleScript()
		{
			return "pi";
		}
	}
}