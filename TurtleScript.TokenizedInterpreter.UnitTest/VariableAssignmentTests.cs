#region Namespaces

using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class VariableAssignmentTests
	{
		[Test]
		public void SimpleAssignmentTest()
		{
			// Arrange
			var script = "a = 1";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			Assert.AreEqual(1, interpreter.Variables.Count);

			string variableName = interpreter.Variables.Keys.First();
			TurtleScriptValue variableValue = interpreter.Variables[variableName];

			Assert.AreEqual("a", variableName);

			Assert.AreEqual(1, variableValue.NumericValue);
		}

		[Test]
		public void MultipleVariableAssignmentTest()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 1");
			scriptBuilder.AppendLine("bcd = 200");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			Assert.AreEqual(2, interpreter.Variables.Count);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(1, variableValue.NumericValue);

			variableValue = interpreter.Variables["bcd"];
			Assert.AreEqual(200, variableValue.NumericValue);
		}

		[Test]
		public void VariableReassignmentTest()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 1");
			scriptBuilder.AppendLine("bcd = 200");
			scriptBuilder.AppendLine("a = 100");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			Assert.AreEqual(2, interpreter.Variables.Count);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(100, variableValue.NumericValue);

			variableValue = interpreter.Variables["bcd"];
			Assert.AreEqual(200, variableValue.NumericValue);
		}

	}
}