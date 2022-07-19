#region Namespaces

using System;
using System.IO;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenVariableReference : TokenBase
	{

		#region Public Constructors

		static TokenVariableReference()
		{
			m_Default = new TokenVariableReference(String.Empty);
		}

		public TokenVariableReference()
		{
		}

		public TokenVariableReference(
			string variableName)
			: this(variableName, 0, 0)
		{
		}

		public TokenVariableReference(
			string variableName,
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.VariableReference,
				lineNumber,
				charPositionInLine)
		{
			m_VariableName = variableName;
		}

		#endregion Public Constructors


		#region Public Properties

		public new static TokenVariableReference Default
		{
			get { return m_Default; }
		}

		public string VariableName
		{
			get { return m_VariableName; }
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

			m_VariableName = (string)parent.Deserialize(stream);
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

			parent.Serialize(stream, m_VariableName);
		}

		public override string ToTurtleScript()
		{
			return VariableName;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			var variableValue = context.GetVariableValue(VariableName);

			return variableValue;
		}

		#endregion Public Methods

		#region Private Constants

		private const int VERSION = 1;

		[CompactFormatter.Attributes.NotSerialized]
		private static readonly TokenVariableReference m_Default;

		#endregion Private Constants


		#region Private Fields

		private string m_VariableName;

		#endregion Private Fields

	}
}