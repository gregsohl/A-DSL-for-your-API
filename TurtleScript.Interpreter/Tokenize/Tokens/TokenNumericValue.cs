#region Namespaces

using System.Diagnostics;
using System.Globalization;
using System.IO;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces


namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenNumericValue : TokenValue
	{

		#region Public Constructors

		static TokenNumericValue()
		{
			m_Default = new TokenNumericValue(0);
		}

		public TokenNumericValue()
		{
		}

		public TokenNumericValue(
			double value)
			: this(value, 0, 0)
		{
			m_Value = value;
		}
		public TokenNumericValue(
			double value,
			int lineNumber,
			int charPositionInLine)
			: base(
				TokenType.Numeric,
				lineNumber, 
				charPositionInLine)
		{
			m_Value = value;
		}
		
		#endregion Public Constructors


		#region Public Properties

		public new static TokenNumericValue Default
		{
			[DebuggerStepThrough]
			get { return m_Default; }
		}

		public double Value
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

			m_Value = (double)parent.Deserialize(stream);
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

		public override string ToTurtleScript()
		{
			return Value.ToString(CultureInfo.InvariantCulture);
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

		private static readonly TokenNumericValue m_Default;
		private double m_Value;

		#endregion Private Fields
	}
}