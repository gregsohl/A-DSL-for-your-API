using System.Diagnostics;
using System.Globalization;
using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenNumericValue : TokenValue
	{
		private readonly double m_Value;

		public TokenNumericValue(double value)
			: base(TokenType.Numeric)
		{
			m_Value = value;
		}

		public double Value
		{
			[DebuggerStepThrough]
			get { return m_Value; }
		}

		public override string ToTurtleScript()
		{
			return Value.ToString(CultureInfo.InvariantCulture);
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return new TurtleScriptValue(Value);
		}

	}
}