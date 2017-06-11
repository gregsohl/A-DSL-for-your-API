#region Namespaces

using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class UserDefiniedFunctionTests
	{
		[Test]
		public void FunctionDeclaration()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc()");
			scriptBuilder.AppendLine("	b = 15");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine("MyFunc()");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(15.0, variableValue.NumericValue);
		}

	}
}