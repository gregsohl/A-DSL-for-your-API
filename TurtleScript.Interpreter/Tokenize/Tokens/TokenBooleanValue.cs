#region Namespaces

using System.Diagnostics;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenBooleanValue : TokenValue
	{

		#region Public Constructors

		static TokenBooleanValue()
		{
			m_Default = new TokenBooleanValue(false);
		}

		public TokenBooleanValue(bool value)
			: base(TokenType.Boolean)
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

		public override string ToTurtleScript()
		{
			return Value ? "true" : "false";
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return new TurtleScriptValue(Value);
		}

		#endregion Public Methods


		#region Private Fields

		private static readonly TokenBooleanValue m_Default;
		private readonly bool m_Value;

		#endregion Private Fields
	}
}