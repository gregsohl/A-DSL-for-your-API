﻿#region Namespaces

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenBinaryOperator : TokenBase
	{
		#region Public Constructors

		public TokenBinaryOperator()
		{
		}

		public TokenBinaryOperator(
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
			Children[0].ToTurtleScript(builder);
			builder.Append($" {AdditiveOperator(TokenType)} ");
			Children[1].ToTurtleScript(builder);

			return builder.Text;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			TurtleScriptValue leftValue = Children[0].Visit(context);
			TurtleScriptValue rightValue = Children[1].Visit(context);

			TurtleScriptValue result = TurtleScriptValue.NULL;

			try
			{
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
			}
			catch (TurtleScriptExecutionException operatorException)
			{
				string message = GetExceptionMessage(operatorException.Message);
				throw new TurtleScriptExecutionException(message, operatorException);
			}

			return result;

		}

		#endregion Public Methods

		#region Private Methods

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
		private string GetExceptionMessage(
			string message)
		{
			return $"{message}, Line {LineNumber}, Col {CharPositionInLine}";
		}

		#endregion Private Methods

	}
}