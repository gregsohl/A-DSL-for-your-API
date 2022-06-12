#region Namespaces

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

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
				case TokenType.Add:
					return "+";
				case TokenType.Subtract:
					return "-";
				default:
					return "error";
			}
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			TurtleScriptValue leftValue = Children[0].Visit(context);
			TurtleScriptValue rightValue = Children[1].Visit(context);

			TurtleScriptValue result;
			if (TokenType == TokenType.Add)
			{
				result = new TurtleScriptValue(leftValue.NumericValue + rightValue.NumericValue);
			}
			else
			{
				result = new TurtleScriptValue(leftValue.NumericValue - rightValue.NumericValue);
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
			context.SetVariableValue(VariableName, Children[0].Visit(context));

			return TurtleScriptValue.VOID;
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
		Add,
		Subtract,

	}
}
