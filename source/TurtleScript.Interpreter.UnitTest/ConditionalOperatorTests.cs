#region Namespaces

using NUnit.Framework;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class ConditionalOperatorTests
	{
		[Test]
		public void AndConditionTrue()
		{
			// Arrange
			var script = "a = 1 < 2 && 3 > 2";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsTrue(variableValue.BooleanValue);
		}

		[Test]
		public void AndConditionFalse()
		{
			// Arrange
			var script = "a = 1 > 2 && 3 > 2";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsFalse(variableValue.BooleanValue);
		}

		[Test]
		public void OrConditionBothTrue()
		{
			// Arrange
			var script = "a = 1 < 2 || 3 > 2";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsTrue(variableValue.BooleanValue);
		}

		[Test]
		public void OrConditionOneTrue()
		{
			// Arrange
			var script = "a = 1 < 2 || 3 < 2";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsTrue(variableValue.BooleanValue);
		}

		[Test]
		public void OrConditionFalse()
		{
			// Arrange
			var script = "a = 1 > 2 || 3 < 2";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsFalse(variableValue.BooleanValue);
		}

		[Test]
		public void AndOrConditionTrue()
		{
			// Arrange
			var script = "a = 1 < 2 || 3 < 2 && 1 == 1";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsTrue(variableValue.BooleanValue);
		}

		[Test]
		public void AndOrConditionFalse()
		{
			// Arrange
			var script = "a = 1 > 2 || 3 < 2 && 1 == 1";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsFalse(variableValue.BooleanValue);
		}

	}
}