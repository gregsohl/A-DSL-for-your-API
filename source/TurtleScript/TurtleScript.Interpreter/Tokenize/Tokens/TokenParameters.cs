#region Namespaces

using System.Collections.Generic;
using System.IO;
using System.Text;

using CompactFormatter.Interfaces;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	internal class TokenParameterDeclaration : TokenBase
	{

		#region Public Constructors

		public TokenParameterDeclaration()
		{
		}

		public TokenParameterDeclaration(string parameterName,
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.Parameter,
				lineNumber,
				charPositionInLine)
		{
			m_ParameterName = parameterName;
		}

		#endregion Public Constructors

		#region Public Properties

		public string ParameterName
		{
			get { return m_ParameterName; }
		}

		#endregion Public Properties

		#region Public Methods

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

			m_ParameterName = (string)parent.Deserialize(stream);
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

			parent.Serialize(stream, m_ParameterName);
		}

		public override string ToTurtleScript(
			TurtleScriptBuilder builder)
		{
			builder.Append(ParameterName);
			return builder.Text;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return TurtleScriptValue.NULL;
		}

		#endregion Public Methods

		#region Private Constants

		private const int VERSION = 1;

		#endregion Private Constants

		#region Private Fields

		private string m_ParameterName;

		#endregion Private Fields
	}

	[CompactFormatter.Attributes.Serializable(Custom = true)]
	internal class TokenParameterDeclarationList : TokenBase
	{

		#region Public Constructors

		public TokenParameterDeclarationList()
		{
		}

		public TokenParameterDeclarationList(TokenParameterDeclaration[] parameters,
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.ParameterList,
				lineNumber,
				charPositionInLine)
		{
			m_Parameters = parameters;
		}

		#endregion Public Constructors


		#region Public Properties

		public TokenParameterDeclaration[] Parameters
		{
			get { return m_Parameters; }
		}

		#endregion Public Properties


		#region Public Methods

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

			m_Parameters = (TokenParameterDeclaration[])parent.Deserialize(stream);
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

			parent.Serialize(stream, m_Parameters);
		}

		public override string ToTurtleScript(
			TurtleScriptBuilder builder)
		{
			for (var index = 0; index < Parameters.Length; index++)
			{
				TokenParameterDeclaration parameterDeclaration = Parameters[index];
				parameterDeclaration.ToTurtleScript(builder);

				if (index < Parameters.Length - 1)
				{
					builder.Append(", ");
				}
			}

			return builder.Text;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return TurtleScriptValue.NULL;
		}

		#endregion Public Methods

		#region Private Constants

		private const int VERSION = 1;

		#endregion Private Constants

		#region Private Fields

		private TokenParameterDeclaration[] m_Parameters;

		#endregion Private Fields
	}
}
