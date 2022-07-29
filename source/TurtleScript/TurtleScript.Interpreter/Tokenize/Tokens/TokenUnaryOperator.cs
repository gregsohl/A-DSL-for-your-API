#region Namespaces

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces


namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenUnaryOperator : TokenBase
	{

		#region Public Constructors

		public TokenUnaryOperator()
		{
		}

		public TokenUnaryOperator(
			TokenType tokenType,
			int lineNumber,
			int charPositionInLine)
			: base(tokenType,
				lineNumber,
				charPositionInLine)
		{
		}

		#endregion Public Constructors


		#region Public Methods

		public override string ToTurtleScript(
			TurtleScriptBuilder builder)
		{
			builder.Append($"{UnaryOperator(TokenType)}");
			Children[0].ToTurtleScript(builder);
			return builder.Text;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			TurtleScriptValue rightValue = Children[0].Visit(context);

			TurtleScriptValue result;
			switch (TokenType)
			{
				case TokenType.OpUnaryNegation:
					result = -rightValue;
					break;
				case TokenType.OpUnaryNot:
					result = !rightValue;
					break;
				default:
					result = TurtleScriptValue.NULL;
					break;
			}

			return result;

		}

		#endregion Public Methods


		#region Private Methods

		private string UnaryOperator(TokenType tokenType)
		{
			switch (tokenType)
			{
				case TokenType.OpUnaryNegation:
					return "-";
				case TokenType.OpUnaryNot:
					return "!";
				default:
					return "error";
			}
		}

		#endregion Private Methods
	}
}