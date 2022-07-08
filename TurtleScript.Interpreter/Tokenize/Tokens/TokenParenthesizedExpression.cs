using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenParenthesizedExpression
		: TokenBase
	{
		public TokenParenthesizedExpression(TokenBase childExpression)
			: base(TokenType.Parenthesized)
		{
			ChildExpression = childExpression;
		}

		public TokenBase ChildExpression { get; }

		public override string ToTurtleScript()
		{
			return $"({ChildExpression.ToTurtleScript()})";
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return ChildExpression.Visit(context);
		}
	}
}