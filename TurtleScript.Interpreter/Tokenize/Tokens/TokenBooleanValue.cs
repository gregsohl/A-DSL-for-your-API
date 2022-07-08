using System.Diagnostics;
using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenBooleanValue : TokenValue
	{
		public TokenBooleanValue(bool value)
			: base(TokenType.Boolean)
		{
			Value = value;
		}

		public bool Value
		{
			[DebuggerStepThrough]
			get;
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