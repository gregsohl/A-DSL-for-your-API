#region Namespaces

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

using Antlr4.Runtime;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	public abstract class TokenBase
	{
		public TokenBase(TokenType tokenType)
		{
			TokenType = tokenType;
		}

		public List<TokenBase> Children
		{
			[DebuggerStepThrough]
			get { return m_Children; }
		}

		public TokenType TokenType
		{
			[DebuggerStepThrough]
			get;
		}

		public void AddChild(TokenBase token)
		{
			if (m_Children == null)
			{
				m_Children = new List<TokenBase>();
			}

			m_Children.Add(token);
		}

		public virtual string ToTurtleScript()
		{
			return string.Empty;
		}

		public abstract TurtleScriptValue Visit(TurtleScriptExecutionContext context);

		private List<TokenBase> m_Children;
	}

	public class TokenScript : TokenBase
	{
		public TokenScript()
			: base(TokenType.Script)
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

	public class TokenBlock : TokenBase
	{
		public TokenBlock()
			: base(TokenType.Block)
		{
		}

		public override string ToTurtleScript()
		{
			StringBuilder result = new StringBuilder();
			foreach (var child in Children)
			{
				result.AppendLine(child.ToTurtleScript());
			}

			if (result.Length >= 2)
			{
				if ((result[result.Length - 2] == '\r') &&
					(result[result.Length - 1] == '\n'))
				{
					result.Length -= 2;
				}
			}

			return result.ToString();
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			foreach (var token in Children)
			{
				token.Visit(context);
			}

			return TurtleScriptValue.VOID;
		}

	}

	public abstract class TokenValue : TokenBase
	{
		protected TokenValue(TokenType tokenType)
			: base(tokenType)
		{
		}
	}

	public class TokenNullValue : TokenValue
	{
		public TokenNullValue()
			: base(TokenType.NullValue)
		{
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return TurtleScriptValue.NULL;
		}
	}

	public class TokenBooleanValue : TokenValue
	{
		public TokenBooleanValue(bool value)
			: base(TokenType.Boolean)
		{
			Value = value;
		}

		public bool Value
		{
			[DebuggerStepThrough]
			get;
		}

		public override string ToTurtleScript()
		{
			return Value ? "true" : "false";
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return new TurtleScriptValue(Value);
		}
	}

	public class TokenNumericValue : TokenValue
	{
		public TokenNumericValue(double value)
			: base(TokenType.Numeric)
		{
			Value = value;
		}

		public double Value
		{
			[DebuggerStepThrough]
			get;
		}

		public override string ToTurtleScript()
		{
			return Value.ToString(CultureInfo.InvariantCulture);
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return new TurtleScriptValue(Value);
		}

	}

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

	public class TokenAssignment : TokenBase
	{
		public TokenAssignment(string variableName)
			: base(TokenType.Assignment)
		{
			VariableName = variableName;
		}

		public string VariableName
		{
			[DebuggerStepThrough]
			get;
		}

		public override string ToTurtleScript()
		{
			return $"{VariableName} = {Children[0].ToTurtleScript()}";
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			var variableValue = Children[0].Visit(context);
			context.SetVariableValue(VariableName, variableValue);

			return TurtleScriptValue.VOID;
		}
	}

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

	public class TokenVariableReference : TokenBase
	{
		public TokenVariableReference(string variableName)
			: base(TokenType.VariableReference)
		{
			VariableName = variableName;
		}

		public string VariableName { get; }

		public override string ToTurtleScript()
		{
			return VariableName;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			var variableValue = context.GetVariableValue(VariableName);

			return variableValue;
		}
	}

	public class TokenIf : TokenBase
	{
		public TokenIf(
			TokenBase block,
			TokenBase conditionalExpression,
			List<Tuple<TokenBase, TokenBase>> elseIf,
			TokenBase elseStatement)
			: base(TokenType.If)
		{
			Block = block;
			ConditionalExpression = conditionalExpression;
			ElseIf = elseIf;
			ElseStatement = elseStatement;
		}

		public TokenBase Block
		{
			[DebuggerStepThrough]
			get;
		}

		public TokenBase ConditionalExpression
		{
			[DebuggerStepThrough]
			get;
		}

		public List<Tuple<TokenBase, TokenBase>> ElseIf
		{
			[DebuggerStepThrough]
			get;
		}

		public TokenBase ElseStatement
		{
			[DebuggerStepThrough]
			get;
		}

		public override string ToTurtleScript()
		{
			string conditionalExpressionTurtleScript = ConditionalExpression.ToTurtleScript();
			string block = Block.ToTurtleScript();
			int indent = 0;

			StringBuilder turtleScript = new StringBuilder($"if ({conditionalExpressionTurtleScript}) Do\r\n");
			AppendBlockToStatementScript(block, turtleScript, indent);

			foreach (Tuple<TokenBase, TokenBase> elseIf in ElseIf)
			{
				indent++;
				turtleScript.AppendLine($"{new string('\t', indent)}elseif ({conditionalExpressionTurtleScript}) Do");
				AppendBlockToStatementScript(block, turtleScript, indent);
			}

			if (ElseStatement != null)
			{
				turtleScript.AppendLine($"{new string('\t', indent)}else");
				AppendBlockToStatementScript(ElseStatement.ToTurtleScript(), turtleScript, indent);
			}

			turtleScript.AppendLine("end");

			return turtleScript.ToString();
		}

		private static void AppendBlockToStatementScript(string block, StringBuilder turtleScript, int indent)
		{
			var blockLines = Regex.Split(block, "\r\n|\r|\n");

			foreach (string blockLine in blockLines)
			{
				turtleScript.AppendLine(new string('\t', indent + 1) + blockLine);
			}
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			TurtleScriptValue ifResult = ConditionalExpression.Visit(context);

			if ((ifResult.IsBoolean) &&
				(ifResult.BooleanValue))
			{
				Block.Visit(context);
				return TurtleScriptValue.VOID;
			}

			foreach (Tuple<TokenBase, TokenBase> elseIf in ElseIf)
			{
				TurtleScriptValue elseIfResult = elseIf.Item1.Visit(context);

				if ((elseIfResult.IsBoolean) &&
					(elseIfResult.BooleanValue))
				{
					elseIf.Item2.Visit(context);
					return TurtleScriptValue.VOID;
				}
			}

			if (ElseStatement != null)
			{
				ElseStatement.Visit(context);
			}

			return TurtleScriptValue.VOID;

		}
	}

	public class TokenPi : TokenNumericValue
	{
		public TokenPi()
			: base(3.141592654)
		{
		}

		public override string ToTurtleScript()
		{
			return "pi";
		}
	}

	public enum TokenType
	{
		Script,
		Block,
		Boolean,
		Numeric,
		NullValue,
		Assignment,
		OpAdd,
		OpSubtract,
		Parenthesized,
		OpMultiply,
		OpDivide,
		OpModulus,
		VariableReference,
		If,
		OpEqual,
		OpNotEqual,
		OpGreaterThan,
		OpLessThan,
		OpGreaterThanOrEqual,
		OpLessThanOrEqual,
		OpConditionalAnd,
		OpConditionalOr,
		Pi,
	}
}
