using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenScript : TokenBase
	{
		public TokenScript()
		{
		}

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
			TurtleScriptBuilder builder = new TurtleScriptBuilder();
			ToTurtleScript(builder);
			builder.Trim();
			return builder.Text;
		}

		public override string ToTurtleScript(
			TurtleScriptBuilder builder)
		{
			Children[0].ToTurtleScript(builder);
			return builder.Text;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return Children[0].Visit(context);
		}
	}
}
