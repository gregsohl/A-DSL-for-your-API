namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenScript : TokenBase
	{
		public TokenScript()
			: base(TokenType.Script)
		{
		}

		public override string ToTurtleScript()
		{
			return Children[0].ToTurtleScript();
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return Children[0].Visit(context);
		}
	}
}
