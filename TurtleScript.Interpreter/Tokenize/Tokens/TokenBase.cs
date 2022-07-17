#region Namespaces

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenBase
	{

		#region Public Constructors

		static TokenBase()
		{
			m_Default = new TokenBase(Tokenize.TokenType.Default);
		}

		#endregion Public Constructors


		#region Public Properties

		public static TokenBase Default
		{
			[DebuggerStepThrough]
			get { return m_Default; }
		}

		public List<TokenBase> Children
		{
			[DebuggerStepThrough]
			get
			{
				return m_Children ?? new List<TokenBase>();
			}
		}

		public bool HasChildren
		{
			get
			{
				return 
					(m_Children != null) &&
					(m_Children.Count > 0);
			}
		}

		public TokenType TokenType
		{
			[DebuggerStepThrough]
			get { return m_TokenType; }
		}

		#endregion Public Properties


		#region Public Methods

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

		public virtual TurtleScriptValue Visit(
			TurtleScriptExecutionContext context)
		{
			return TurtleScriptValue.NULL;
		}

		#endregion Public Methods


		#region Protected Constructors

		protected TokenBase(TokenType tokenType)
		{
			m_TokenType = tokenType;
		}

		#endregion Protected Constructors


		#region Protected Methods

		protected string Indent(int indentLevel)
		{
			string indentPadding = new string('\t', indentLevel);
			return indentPadding;
		}

		#endregion Protected Methods


		#region Private Fields

		private static readonly TokenBase m_Default;
		private readonly TokenType m_TokenType;
		private List<TokenBase> m_Children;

		#endregion Private Fields
	}
}
