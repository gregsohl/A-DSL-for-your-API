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
		public void SingleParameter()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = test.square(2)");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString(), new List<ITurtleScriptRuntime>() {new SampleRuntime()});

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(4, variableValue.NumericValue);
		}

		[Test]
		public void ThreeParameters()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = test.sum(2, 4, 6)");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString(), new List<ITurtleScriptRuntime>() { new SampleRuntime() });

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(12, variableValue.NumericValue);
		}


		private class SampleRuntime : ITurtleScriptRuntime
		{
			public SampleRuntime()
			{
				m_Functions = new Dictionary<string, TurtleScriptRuntimeFunction>();

				m_Functions.Add("square", new TurtleScriptRuntimeFunction(Square, 1));
				m_Functions.Add("sum", new TurtleScriptRuntimeFunction(Sum, 3));
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
					float value = parameters[0].NumericValue;

					TurtleScriptValue returnValue = new TurtleScriptValue(value * value);

					return returnValue;
				}

				return TurtleScriptValue.VOID;
			}

			public TurtleScriptValue Sum(List<TurtleScriptValue> parameters)
			{
				if (parameters.Count == 3)
				{
					float value1 = parameters[0].NumericValue;
					float value2 = parameters[1].NumericValue;
					float value3 = parameters[2].NumericValue;

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
}