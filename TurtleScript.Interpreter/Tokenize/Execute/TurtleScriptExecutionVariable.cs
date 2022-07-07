#region Namespaces

using System.Diagnostics;

#endregion Namespaces


namespace TurtleScript.Interpreter.Tokenize
{
	public class TurtleScriptExecutionVariable : TurtleScriptParserVariable, ITurtleScriptVariable<TurtleScriptExecutionVariable>
	{
		#region Public Constructors

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TurtleScriptExecutionVariable(
			string name,
			VariableType type,
			TokenBase declaration)
			: base(name, type, declaration)
		{
		}

		#endregion Public Constructors


		#region Public Properties

		public TurtleScriptValue Value
		{
			[DebuggerStepThrough]
			get { return m_Value; }
			[DebuggerStepThrough]
			set { m_Value = value; }
		}

		#endregion Public Properties


		#region Public Methods

		public override TurtleScriptValue GetValue()
		{
			// No Action for Parser
			return m_Value;
		}

		public override void SetValue(
			TurtleScriptValue value)
		{
			m_Value = value;
		}

		#endregion Public Methods


		#region Private Fields

		private TurtleScriptValue m_Value;

		#endregion Private Fields

	}
}
