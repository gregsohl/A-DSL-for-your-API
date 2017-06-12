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

		public int ParameterCount => m_ParameterCount;

		private Func<List<TurtleScriptValue>, TurtleScriptValue> m_Function;
		private int m_ParameterCount;
	}
}
