#region Namespaces

using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class VariableReferenceTests
	{
		[Test]
		public void AssignmentAndReference()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 1");
			scriptBuilder.AppendLine("b = a");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(1, variableValue.NumericValue);
		}

		[Test]
		public void AssignmentAndSelfReassignment()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 12345");
			scriptBuilder.AppendLine("a = a");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(12345, variableValue.NumericValue);
		}

		[Test]
		public void AssignmentAndReferenceInExpression()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 1");
			scriptBuilder.AppendLine("b = a + 500");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(501, variableValue.NumericValue);
		}
	}
}