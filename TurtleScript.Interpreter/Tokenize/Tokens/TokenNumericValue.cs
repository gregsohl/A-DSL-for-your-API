#region Namespaces

using System.Diagnostics;
using System.Globalization;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces


namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenNumericValue : TokenValue
	{

		#region Public Constructors

		static TokenNumericValue()
		{
			m_Default = new TokenNumericValue(0);
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

		public override string ToTurtleScript()
		{
			return Value.ToString(CultureInfo.InvariantCulture);
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return new TurtleScriptValue(Value);
		}

		#endregion Public Methods


		#region Private Fields

		private static readonly TokenNumericValue m_Default;
		private readonly double m_Value;

		#endregion Private Fields
	}
}