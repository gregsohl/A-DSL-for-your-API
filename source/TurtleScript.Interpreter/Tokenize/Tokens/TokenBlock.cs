﻿#region Namespaces

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

		public TokenBlock()
		{
		}

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

			m_FunctionDeclarations = null;
			object functions = parent.Deserialize(stream);
			if (functions != null)
			{
				m_FunctionDeclarations =
					new List<TokenFunctionDeclaration>((TokenFunctionDeclaration[])functions);
			}
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
			if (m_FunctionDeclarations != null)
			{
				parent.Serialize(
					stream,
					m_FunctionDeclarations.ToArray());
			}
			else
			{
				parent.Serialize(stream, m_FunctionDeclarations);
			}
		}

		public override string ToTurtleScript(
			TurtleScriptBuilder builder)
		{
			if (Children != null)
			{
				for (var childIndex = 0; childIndex < (Children.Count - 1); childIndex++)
				{
					TokenBase child = Children[childIndex];
					child.ToTurtleScript(builder);
				}

				if (Children.Count >= 1)
				{
					Children[Children.Count - 1].ToTurtleScript(builder);
				}
			}

			return builder.Text;
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