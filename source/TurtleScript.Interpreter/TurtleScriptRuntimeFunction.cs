using System;
using System.Collections.Generic;

namespace TurtleScript.Interpreter
{
	public class TurtleScriptRuntimeFunction
	{
		public TurtleScriptRuntimeFunction(
			Func<List<TurtleScriptValue>, TurtleScriptValue> function,
		    int parameterCount)
		{
			m_Function = function;
			m_ParameterCount = parameterCount;
		}

		public Func<List<TurtleScriptValue>, TurtleScriptValue> Function => m_Function;

		public int ParameterCount
		{
			get { return m_ParameterCount; }
		}

		private readonly Func<List<TurtleScriptValue>, TurtleScriptValue> m_Function;
		private readonly int m_ParameterCount;
	}
}
