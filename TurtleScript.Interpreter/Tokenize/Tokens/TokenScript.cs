using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenScript : TokenBase
	{
		public TokenScript(
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.Script,
				lineNumber,
				charPositionInLine)
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
