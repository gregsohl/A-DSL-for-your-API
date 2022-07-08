using System.Diagnostics;

namespace TurtleScript.Interpreter.ImmediateInterpreter
{
	internal abstract class TurtleScriptFunctionBase
	{
		protected TurtleScriptFunctionBase(
			string name,
			int parameterCount)
		{
			m_Name = name;
			m_ParameterCount = parameterCount;

			// TODO: add key initialization
		}

		public string Key
		{
			[DebuggerStepThrough]
			get { return m_Key; }
		}

		public int ParameterCount
		{
			[DebuggerStepThrough]
			get { return m_ParameterCount; }
		}

		public string Name
		{
			get { return m_Name; }
		}

		private readonly string m_Key;
		private readonly string m_Name;
		private readonly int m_ParameterCount;
	}
}
