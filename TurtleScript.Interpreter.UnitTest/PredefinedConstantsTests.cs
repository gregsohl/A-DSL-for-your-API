#region Namespaces

using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class PredefinedConstantsTests
	{
		private const double PI = 3.141592654;

		[Test]
		public void Pi()
		{
			// Arrange
			var script = "a = pi";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(PI, variableValue.NumericValue);
		}

		[Test]
		public void PiMath()
		{
			// Arrange
			var script = "a = pi * 2";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(PI * 2, variableValue.NumericValue);
		}


	}
}