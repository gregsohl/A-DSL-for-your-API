namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenBinaryOperator : TokenBase
	{
		public TokenBinaryOperator(TokenType tokenType)
			: base(tokenType)
		{
		}

		public override string ToTurtleScript()
		{
			string left = Children[0].ToTurtleScript();
			string right = Children[1].ToTurtleScript();

			return $"{left} {AdditiveOperator(TokenType)} {right}";
		}

		private string AdditiveOperator(TokenType tokenType)
		{
			switch (tokenType)
			{
				case TokenType.OpAdd:
					return "+";
				case TokenType.OpConditionalAnd:
					return "&&";
				case TokenType.OpSubtract:
					return "-";
				case TokenType.OpMultiply:
					return "*";
				case TokenType.OpDivide:
					return "/";
				case TokenType.OpModulus:
					return "%";
				case TokenType.OpEqual:
					return "==";
				case TokenType.OpNotEqual:
					return "!=";
				case TokenType.OpGreaterThan:
					return ">";
				case TokenType.OpLessThan:
					return "<";
				case TokenType.OpGreaterThanOrEqual:
					return ">=";
				case TokenType.OpLessThanOrEqual:
					return "<=";
				case TokenType.OpConditionalOr:
					return "||";
				default:
					return "error";
			}
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			TurtleScriptValue leftValue = Children[0].Visit(context);
			TurtleScriptValue rightValue = Children[1].Visit(context);

			TurtleScriptValue result;
			switch (TokenType)
			{
				case TokenType.OpAdd:
					result = new TurtleScriptValue(leftValue.NumericValue + rightValue.NumericValue);
					break;
				case TokenType.OpConditionalAnd:
					result = new TurtleScriptValue(leftValue.BooleanValue && rightValue.BooleanValue);
					break;
				case TokenType.OpSubtract:
					result = new TurtleScriptValue(leftValue.NumericValue - rightValue.NumericValue);
					break;
				case TokenType.OpMultiply:
					result = new TurtleScriptValue(leftValue.NumericValue * rightValue.NumericValue);
					break;
				case TokenType.OpDivide:
					if (rightValue.NumericValue == 0)
					{
						result = new TurtleScriptValue(0);
					}
					else
					{
						result = new TurtleScriptValue(leftValue.NumericValue / rightValue.NumericValue);
					}
					break;
				case TokenType.OpModulus:
					result = new TurtleScriptValue(leftValue.NumericValue % rightValue.NumericValue);
					break;
				case TokenType.OpEqual:
					result = new TurtleScriptValue(leftValue.NumericValue == rightValue.NumericValue);
					break;
				case TokenType.OpNotEqual:
					result = new TurtleScriptValue(leftValue.NumericValue != rightValue.NumericValue);
					break;
				case TokenType.OpGreaterThan:
					result = new TurtleScriptValue(leftValue.NumericValue > rightValue.NumericValue);
					break;
				case TokenType.OpLessThan:
					result = new TurtleScriptValue(leftValue.NumericValue < rightValue.NumericValue);
					break;
				case TokenType.OpGreaterThanOrEqual:
					result = new TurtleScriptValue(leftValue.NumericValue >= rightValue.NumericValue);
					break;
				case TokenType.OpLessThanOrEqual:
					result = new TurtleScriptValue(leftValue.NumericValue <= rightValue.NumericValue);
					break;
				case TokenType.OpConditionalOr:
					result = new TurtleScriptValue(leftValue.BooleanValue || rightValue.BooleanValue);
					break;
				default:
					result = TurtleScriptValue.NULL;
					break;
			}

			return result;

		}

	}
}