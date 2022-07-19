#region Namespaces

using System.Collections.Generic;
using System.IO;
using System.Text;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenBlock : TokenBase
	{

		#region Public Constructors

		public TokenBlock(
		int lineNumber,
		int charPositionInLine)
			: base(TokenType.Block,
				lineNumber,
				charPositionInLine)
		{
			m_FunctionDeclarations = new List<TokenFunctionDeclaration>();
		}

		#endregion Public Constructors


		#region Public Methods

		public override void AddChild(TokenBase token)
		{
			if (token is TokenFunctionDeclaration functionDeclaration)
			{
				m_FunctionDeclarations.Add(functionDeclaration);
				base.AddChild(token);
			}
			else
			{
				base.AddChild(token);
			}
		}

		/// <summary>
		/// This function is invoked by CompactFormatter when deserializing a
		/// Custom Serializable object.
		/// </summary>
		/// <param name="parent">A reference to the CompactFormatter instance which called this method.</param>
		/// <param name="stream">The Stream where object data must be read</param>
		public override void ReceiveObjectData(
			CompactFormatter.CompactFormatter parent,
			Stream stream)
		{
			base.ReceiveObjectData(
				parent,
				stream);

			int version = (int)parent.Deserialize(stream);

			m_FunctionDeclarations =
				new List<TokenFunctionDeclaration>((TokenFunctionDeclaration[])parent.Deserialize(stream));
		}

		/// <summary>
		/// This function is invoked by CompactFormatter when serializing a 
		/// Custom Serializable object.
		/// </summary>
		/// <param name="parent">A reference to the CompactFormatter instance which called this method.</param>
		/// <param name="stream">The Stream where object data must be written</param>
		public override void SendObjectData(
			CompactFormatter.CompactFormatter parent,
			Stream stream)
		{
			base.SendObjectData(
				parent,
				stream);

			parent.Serialize(stream, VERSION);
			parent.Serialize(stream, m_FunctionDeclarations.ToArray());
		}

		public override StringBuilder ToTurtleScript(StringBuilder result, int indentLevel)
		{
			if (Children != null)
			{
				foreach (var child in Children)
				{
					result.AppendLine(Indent(indentLevel) + child.ToTurtleScript());
				}
			}

			if (result.Length >= 2)
			{
				if ((result[result.Length - 2] == '\r') &&
					(result[result.Length - 1] == '\n'))
				{
					result.Length -= 2;
				}
			}

			return result;
		}

		public override string ToTurtleScript()
		{
			return ToTurtleScript(0);
		}

		public override string ToTurtleScript(int indentLevel)
		{
			StringBuilder result = new StringBuilder();

			ToTurtleScript(result, indentLevel);

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

		#endregion Public Methods

		#region Private Constants

		private const int VERSION = 1;

		#endregion Private Constants


		#region Private Fields

		private List<TokenFunctionDeclaration> m_FunctionDeclarations;

		#endregion Private Fields
	}
}
