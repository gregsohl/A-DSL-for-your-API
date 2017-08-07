using System;
using System.Collections.Generic;

using TurtleScript.Interpreter;

namespace ScriptedTurtle.Runtime
{
	public class TurtleMath : ITurtleScriptRuntime
	{
		public TurtleMath()
		{
			m_Functions = new Dictionary<string, TurtleScriptRuntimeFunction>();

			m_Functions.Add("sin", new TurtleScriptRuntimeFunction(Sin, 1));
			m_Functions.Add("cos", new TurtleScriptRuntimeFunction(Cos, 1));
			m_Functions.Add("tan", new TurtleScriptRuntimeFunction(Tan, 1));
		}

		public string Namespace
		{
			get { return "m"; }
		}

		public Dictionary<string, TurtleScriptRuntimeFunction> Functions
		{
			get { return m_Functions; }
		}

		public TurtleScriptValue Sin(List<TurtleScriptValue> parameters)
		{
			double angle = parameters[0].NumericValue;
			return new TurtleScriptValue(Math.Sin(angle));
		}

		public TurtleScriptValue Cos(List<TurtleScriptValue> parameters)
		{
			double angle = parameters[0].NumericValue;
			return new TurtleScriptValue(Math.Cos(angle));
		}

		public TurtleScriptValue Tan(List<TurtleScriptValue> parameters)
		{
			double angle = parameters[0].NumericValue;
			return new TurtleScriptValue(Math.Tan(angle));
		}

		#region Private Fields

		private readonly Dictionary<string, TurtleScriptRuntimeFunction> m_Functions;

		#endregion Private Fields

	}
}
