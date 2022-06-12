#region Namespaces

using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class ForLoopTests
	{
		[Test]
		public void Simple()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 0");
			scriptBuilder.AppendLine("for a = 1 to 5 do");
			scriptBuilder.AppendLine("b = b + a");
			scriptBuilder.AppendLine("end");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(15, variableValue.NumericValue);
		}


		[Test]
		public void Reverse()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 0");
			scriptBuilder.AppendLine("for a = 5 to 1 do");
			scriptBuilder.AppendLine("b = b + a");
			scriptBuilder.AppendLine("end");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(15, variableValue.NumericValue);
		}


	}
}