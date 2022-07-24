#region Namespaces

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using CompactFormatter;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{

	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenBase : CompactFormatter.Interfaces.ICSerializable
	{

		#region Public Constructors

		static TokenBase()
		{
			m_Default = new TokenBase(
				Tokenize.TokenType.Default,
				0,
				0);
		}

		#endregion Public Constructors


		#region Public Properties

		public static TokenBase Default
		{
			[DebuggerStepThrough]
			get { return m_Default; }
		}

		public int CharPositionInLine
		{
			[DebuggerStepThrough]
			get { return m_CharPositionInLine; }
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

		public int LineNumber
		{
			[DebuggerStepThrough]
			get { return m_LineNumber; }
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


		/// <summary>
		/// This function is invoked by CompactFormatter when deserializing a
		/// Custom Serializable object.
		/// </summary>
		/// <param name="parent">A reference to the CompactFormatter instance which called this method.</param>
		/// <param name="stream">The Stream where object data must be read</param>
		public virtual void ReceiveObjectData(
			CompactFormatter.CompactFormatter parent,
			Stream stream)
		{
			int version = (int)parent.Deserialize(stream);

			m_TokenType = (TokenType)parent.Deserialize(stream);
			
			object children = parent.Deserialize(stream);
			m_Children = children != null ? new List<TokenBase>((TokenBase[])children) : null;

			m_LineNumber = (int)parent.Deserialize(stream);
			m_CharPositionInLine = (int)parent.Deserialize(stream);
		}

		/// <summary>
		/// This function is invoked by CompactFormatter when serializing a 
		/// Custom Serializable object.
		/// </summary>
		/// <param name="parent">A reference to the CompactFormatter instance which called this method.</param>
		/// <param name="stream">The Stream where object data must be written</param>
		public virtual void SendObjectData(
			CompactFormatter.CompactFormatter parent,
			Stream stream)
		{
			parent.Serialize(stream, VERSION);

			parent.Serialize(stream, m_TokenType);
			if (m_Children != null)
			{
				parent.Serialize(stream, m_Children.ToArray());
			}
			else
			{
				parent.Serialize(stream, m_Children);
			}

			parent.Serialize(stream, m_LineNumber);
			parent.Serialize(stream, m_CharPositionInLine);
		}

		public virtual string ToTurtleScript()
		{
			return string.Empty;
		}

		public virtual string ToTurtleScript(
			TurtleScriptBuilder builder)
		{
			return string.Empty;
		}

		public virtual TurtleScriptValue Visit(
			TurtleScriptExecutionContext context)
		{
			return TurtleScriptValue.NULL;
		}

		#endregion Public Methods


		#region Protected Constructors

		protected TokenBase()
		{
		}

		protected TokenBase(
			TokenType tokenType)
		{
			m_TokenType = tokenType;
		}

		protected TokenBase(
			TokenType tokenType,
			int lineNumber,
			int charPositionInLine)
		{
			m_TokenType = tokenType;
			m_LineNumber = lineNumber;
			m_CharPositionInLine = charPositionInLine;
		}

		#endregion Protected Constructors


		#region Protected Methods

		protected string Indent(int indentLevel)
		{
			string indentPadding = new string('\t', indentLevel);
			return indentPadding;
		}

		#endregion Protected Methods


		#region Private Constants

		private const int VERSION = 1;

		#endregion Private Constants

		#region Private Fields

		[CompactFormatter.Attributes.NotSerialized]
		private static readonly TokenBase m_Default;

		private TokenType m_TokenType;
		private List<TokenBase> m_Children;

		private int m_LineNumber;
		private int m_CharPositionInLine;


		#endregion Private Fields


	}
}
