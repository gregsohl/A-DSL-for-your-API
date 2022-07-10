using System.Diagnostics;
using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenBooleanValue : TokenValue
	{
		private readonly bool m_Value;

		public TokenBooleanValue(bool value)
			: base(TokenType.Boolean)
		{
			m_Value = value;
		}

		public bool Value
		{
			[DebuggerStepThrough]
			get { return m_Value; }
		}

		public override string ToTurtleScript()
		{
			return Value ? "true" : "false";
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return new TurtleScriptValue(Value);
		}
	}
}