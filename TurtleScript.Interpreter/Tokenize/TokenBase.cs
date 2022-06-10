#region Namespaces

using System.Collections.Generic;
using System.Diagnostics;

using Antlr4.Runtime;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenBase
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

		public string ToTurtleScript()
		{
			return string.Empty;
		}

		private List<TokenBase> m_Children;
	}

	public class TokenValue : TokenBase
	{
		public TokenValue(TokenType tokenType)
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
	}

	public class TokenBinaryOperator : TokenBase
	{
		public TokenBinaryOperator(TokenType tokenType)
			: base(tokenType)
		{
		}
	}

	public class TokenAssignment : TokenBase
	{
		public TokenAssignment(string variableName)
			: base(TokenType.Assignment)
		{
			m_VariableName = variableName;
		}

		private string m_VariableName;
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
		Subtrract,

	}
}
