using System.Collections.Generic;

namespace TurtleScript.Interpreter.UnitTest
{
	public class SampleRuntime : ITurtleScriptRuntime
	{
		public SampleRuntime()
		{
			m_Functions = new Dictionary<string, TurtleScriptRuntimeFunction>();

			m_Functions.Add("square_1", new TurtleScriptRuntimeFunction(Square, 1));
			m_Functions.Add("sum_3", new TurtleScriptRuntimeFunction(Sum, 3));
		}

		public string Namespace
		{
			get { return "test"; }
		}

		public Dictionary<string, TurtleScriptRuntimeFunction> Functions
		{
			get { return m_Functions; }
		}

		public TurtleScriptValue Square(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 1)
			{
				double value = parameters[0].NumericValue;

				TurtleScriptValue returnValue = new TurtleScriptValue(value * value);

				return returnValue;
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue Sum(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 3)
			{
				double value1 = parameters[0].NumericValue;
				double value2 = parameters[1].NumericValue;
				double value3 = parameters[2].NumericValue;

				TurtleScriptValue returnValue = new TurtleScriptValue(value1 + value2 + value3);

				return returnValue;
			}

			return TurtleScriptValue.VOID;
		}

		#region Private Fields

		private Dictionary<string, TurtleScriptRuntimeFunction> m_Functions;

		#endregion Private Fields
	}
}
