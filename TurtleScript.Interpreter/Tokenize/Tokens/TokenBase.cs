#region Namespaces

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Antlr4.Runtime;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	public abstract class TokenBase
	{
		protected TokenBase(TokenType tokenType)
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

		public virtual void AddChild(TokenBase token)
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

		public virtual string ToTurtleScript(int indentLevel)
		{
			return string.Empty;
		}

		public virtual StringBuilder ToTurtleScript(
			StringBuilder result,
			int indentLevel)
		{
			return result;
		}

		public abstract TurtleScriptValue Visit(TurtleScriptExecutionContext context);

		protected string Indent(int indentLevel)
		{
			string indentPadding = new string('\t', indentLevel);
			return indentPadding;
		}

		private List<TokenBase> m_Children;
	}
}
