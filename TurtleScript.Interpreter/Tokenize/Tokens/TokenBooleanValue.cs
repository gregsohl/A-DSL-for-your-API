#region Namespaces

using System.Diagnostics;
using System.IO;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenBooleanValue : TokenValue
	{

		#region Public Constructors

		static TokenBooleanValue()
		{
			m_Default = new TokenBooleanValue(false);
		}

		public TokenBooleanValue()
		{
		}

		public TokenBooleanValue(bool value)
			: this(value, 0, 0)
		{
			m_Value = value;
		}

		public TokenBooleanValue(
			bool value,
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.Boolean,
				lineNumber,
				charPositionInLine)
		{
			m_Value = value;
		}

		

		#endregion Public Constructors


		#region Public Properties

		public new static TokenBooleanValue Default
		{
			[DebuggerStepThrough]
			get { return m_Default; }
		}

		public bool Value
		{
			[DebuggerStepThrough]
			get { return m_Value; }
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

			m_Value = (bool)parent.Deserialize(stream);
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

			parent.Serialize(stream, m_Value);
		}

		public override string ToTurtleScript(
			TurtleScriptBuilder builder)
		{
			builder.Append(Value ? "true" : "false");
			return builder.Text;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return new TurtleScriptValue(Value);
		}

		#endregion Public Methods

		#region Private Constants

		private const int VERSION = 1;

		#endregion Private Constants

		#region Private Fields

		[CompactFormatter.Attributes.NotSerialized]
		private static readonly TokenBooleanValue m_Default;
		private bool m_Value;

		#endregion Private Fields
	}
}