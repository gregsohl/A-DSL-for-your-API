using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenParenthesizedExpression
		: TokenBase
	{
		private readonly TokenBase m_ChildExpression;

		public TokenParenthesizedExpression(
			TokenBase childExpression,
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.Parenthesized,
				lineNumber,
				charPositionInLine)
		{
			m_ChildExpression = childExpression;
		}

		public TokenBase ChildExpression
		{
			get { return m_ChildExpression; }
		}

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