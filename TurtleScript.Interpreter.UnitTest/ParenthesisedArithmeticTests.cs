#region Namespaces

using System.Linq;
using System.Text;
using NUnit.Framework;

using TurtleScript.Interpreter.ImmediateInterpreter;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class ParenthesisedArithmeticTests
	{
		[Test]
		public void AdditionAndMultiplication()
		{
			// Arrange
			var script = "a = (1 + 2) * 3";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(9, variableValue.NumericValue);
		}

		[Test]
		public void MultipleLevels()
		{
			// Arrange
			var script = "a = (((1 + 2) * (3 + 4) + 5) * 2)";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(52, variableValue.NumericValue);
		}
	}
}