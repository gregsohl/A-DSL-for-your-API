using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenUnaryOperator : TokenBase
	{
		public TokenUnaryOperator(TokenType tokenType)
			: base(tokenType)
		{
		}

		public override string ToTurtleScript()
		{
			string right = Children[0].ToTurtleScript();

			return $"{UnaryOperator(TokenType)}{right}";
		}

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

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			TurtleScriptValue rightValue = Children[0].Visit(context);

			TurtleScriptValue result;
			switch (TokenType)
			{
				case TokenType.OpUnaryNegation:
					result = new TurtleScriptValue(-1 * rightValue.NumericValue);
					break;
				case TokenType.OpUnaryNot:
					result = new TurtleScriptValue(!rightValue.BooleanValue);
					break;
				default:
					result = TurtleScriptValue.NULL;
					break;
			}

			return result;

		}

	}
}