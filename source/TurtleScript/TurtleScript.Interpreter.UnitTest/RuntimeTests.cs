#region Namespaces

using System.Collections.Generic;
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

            TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(
                scriptBuilder.ToString(),
                new List<ITurtleScriptRuntime>() {new SampleRuntime()});

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
	}
}