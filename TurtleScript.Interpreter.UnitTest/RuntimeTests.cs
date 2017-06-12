#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class RuntimeTests
	{
		[Test]
		public void AssignmentAndReference()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = test.square(2)");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString(), new List<ITurtleScriptRuntime>() {new SampleRuntime()});

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(4, variableValue.NumericValue);
		}


		private class SampleRuntime : ITurtleScriptRuntime
		{
			public SampleRuntime()
			{
				m_Functions = new Dictionary<string, Func<List<TurtleScriptValue>, TurtleScriptValue>>();

				m_Functions.Add("square", Square);
			}

			public string Namespace
			{
				get { return "test"; }
			}

			public Dictionary<string, Func<List<TurtleScriptValue>, TurtleScriptValue>> Functions
			{
				get { return m_Functions; }
			}

			public TurtleScriptValue Square(List<TurtleScriptValue> parameters)
			{
				if (parameters.Count == 1)
				{
					float value = parameters[0].NumericValue;

					TurtleScriptValue returnValue = new TurtleScriptValue(value * value);

					return returnValue;
				}

				return TurtleScriptValue.VOID;
			}

			private Dictionary<string, Func<List<TurtleScriptValue>, TurtleScriptValue>> m_Functions;

		}

	}
}