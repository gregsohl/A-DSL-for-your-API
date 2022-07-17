using System;
using System.Collections.Generic;

using TurtleScript.Interpreter.Tokenize.Execute;

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
					result = leftValue + rightValue;
					break;
				case TokenType.OpConditionalAnd:
					result = leftValue & rightValue;
					break;
				case TokenType.OpSubtract:
					result = leftValue - rightValue;
					break;
				case TokenType.OpMultiply:
					result = leftValue * rightValue;
					break;
				case TokenType.OpDivide:
					result = leftValue / rightValue;
					break;
				case TokenType.OpModulus:
					result = leftValue % rightValue;
					break;
				case TokenType.OpEqual:
					result = leftValue == rightValue;
					break;
				case TokenType.OpNotEqual:
					result = leftValue != rightValue;
					break;
				case TokenType.OpGreaterThan:
					result = leftValue > rightValue;
					break;
				case TokenType.OpLessThan:
					result = leftValue < rightValue;
					break;
				case TokenType.OpGreaterThanOrEqual:
					result = leftValue >= rightValue;
					break;
				case TokenType.OpLessThanOrEqual:
					result = leftValue <= rightValue;
					break;
				case TokenType.OpConditionalOr:
					result = leftValue | rightValue;
					break;
				default:
					result = TurtleScriptValue.NULL;
					break;
			}

			return result;

		}

	}
}