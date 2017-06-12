#region Namespaces

using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class IfStatementTests
	{
		[Test]
		public void IfStatementTrue()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 15");
			scriptBuilder.AppendLine("if b > 10 do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("end");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(20.0, variableValue.NumericValue);
		}

		[Test]
		public void IfStatementFalse()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 10");
			scriptBuilder.AppendLine("if b > 10 do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("end");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(10.0, variableValue.NumericValue);
		}


		[Test]
		public void IfStatementTrueMultilineBody()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 15");
			scriptBuilder.AppendLine("if b > 10 do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("  b = b * 2");
			scriptBuilder.AppendLine("end");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(40.0, variableValue.NumericValue);
		}

		[Test]
		public void IfElseStatementTrue()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 15");
			scriptBuilder.AppendLine("if b > 10 do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("  b = b * 2");
			scriptBuilder.AppendLine("else do");
			scriptBuilder.AppendLine("  b = 10");
			scriptBuilder.AppendLine("end");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(40.0, variableValue.NumericValue);
		}

		[Test]
		public void IfElseStatementFalse()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 5");
			scriptBuilder.AppendLine("if b > 10 do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("  b = b * 2");
			scriptBuilder.AppendLine("else do");
			scriptBuilder.AppendLine("  b = 10");
			scriptBuilder.AppendLine("end");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(10.0, variableValue.NumericValue);
		}

		[Test]
		public void IfElseIfStatementTrue()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 5");
			scriptBuilder.AppendLine("if b > 10 do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("else if b >= 5 do");
			scriptBuilder.AppendLine("  b = 10");
			scriptBuilder.AppendLine("else do");
			scriptBuilder.AppendLine("  b = 1");
			scriptBuilder.AppendLine("end");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(10.0, variableValue.NumericValue);
		}

		[Test]
		public void IfElseIfStatementFalse()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 2");
			scriptBuilder.AppendLine("if b > 10 do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("else if b >= 5 do");
			scriptBuilder.AppendLine("  b = 10");
			scriptBuilder.AppendLine("else do");
			scriptBuilder.AppendLine("  b = 1");
			scriptBuilder.AppendLine("end");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(1.0, variableValue.NumericValue);
		}

	}
}