#region Namespaces

using System.Linq;
using System.Text;
using NUnit.Framework;

using TurtleScript.Interpreter.ImmediateInterpreter;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class UserDefiniedFunctionTests
	{
		[Test]
		public void FunctionDeclarationAndCallNoParameters()
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
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(15.0, variableValue.NumericValue);
		}

		[Test]
		public void FunctionDeclarationAndCallOneParameter()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc(myparameter)");
			scriptBuilder.AppendLine("	b = myparameter");
			scriptBuilder.AppendLine("	myparameter = b * 2");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine("MyFunc(10)");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["myparameter"];
			Assert.AreEqual(20, variableValue.NumericValue);
		}

		[Test]
		public void FunctionDeclarationAndCallThreeParameters()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc(value1, value2, value3)");
			scriptBuilder.AppendLine("	b = value1 + value2 - value3");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine("MyFunc(100, 200, 50)");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(250, variableValue.NumericValue);
		}

		[Test]
		public void FunctionDeclarationAfterCall()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("MyFunc()");
			scriptBuilder.AppendLine("def MyFunc()");
			scriptBuilder.AppendLine("	b = 15");
			scriptBuilder.AppendLine("end");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(15.0, variableValue.NumericValue);
		}

	}
}