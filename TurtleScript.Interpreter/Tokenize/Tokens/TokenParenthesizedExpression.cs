using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenParenthesizedExpression
		: TokenBase
	{
		#region Public Constructors

		public TokenParenthesizedExpression(
			TokenBase childExpression,
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.Parenthesized,
				lineNumber,
				charPositionInLine)
		{
			AddChild(childExpression);

//			m_ChildExpression = childExpression;
		}

		#endregion Public Constructors


		#region Public Properties

		public TokenBase ChildExpression
		{
			get { return Children[0]; }
		}

		#endregion Public Properties


		#region Public Methods

		public override string ToTurtleScript(
			TurtleScriptBuilder builder)
		{
			builder.Append($"({ChildExpression.ToTurtleScript()})");
			return builder.Text;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return ChildExpression.Visit(context);
		}

		#endregion Public Methods


		#region Private Fields

		// private readonly TokenBase m_ChildExpression;

		#endregion Private Fields
	}
}