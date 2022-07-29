#region Namespaces

using NUnit.Framework;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class ArithmeticTests
	{
		[Test]
		public void SimpleAddition()
		{
			// Arrange
			var script = "a = 1 + 1";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(2, variableValue.NumericValue);
		}

		[Test]
		public void ComplexAddition()
		{
			// Arrange
			var script = "a = 1 + 1 + 2 + 4";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(8, variableValue.NumericValue);
		}


		[Test]
		public void SimpleSubtraction()
		{
			// Arrange
			var script = "a = 10 - 2";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(8, variableValue.NumericValue);
		}

		[Test]
		public void ComplexSubtraction()
		{
			// Arrange
			var script = "a = 10 - 2 - 4";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(4, variableValue.NumericValue);
		}

		[Test]
		public void SimpleMultiplication()
		{
			// Arrange
			var script = "a = 5 * 6";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(30, variableValue.NumericValue);
		}

		[Test]
		public void ComplexMultiplication()
		{
			// Arrange
			var script = "a = 5 * 6 * 2 * 10";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(600, variableValue.NumericValue);
		}

		[Test]
		public void SimpleDivision()
		{
			// Arrange
			var script = "a = 10 / 2";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(5, variableValue.NumericValue);
		}

		[Test]
		public void DivideByZero()
		{
			// Arrange
			var script = "a = 5 / 0";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(0, variableValue.NumericValue);
		}

		[Test]
		public void ComplexDivision()
		{
			// Arrange
			var script = "a = 100 / 2 / 5";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(10, variableValue.NumericValue);
		}

		[Test]
		public void AdditionFirstThenMultiplication()
		{
			// Arrange
			var script = "a = 10 + 10 * 100";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(1010, variableValue.NumericValue);
		}

		[Test]
		public void MultiplicationFirstThenAddition()
		{
			// Arrange
			var script = "a = 10 * 100 + 10";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(1010, variableValue.NumericValue);
		}

		[Test]
		public void MultiplicationFirstThenDivision()
		{
			// Arrange
			var script = "a = 10 * 100 / 2";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(500, variableValue.NumericValue);
		}

		[Test]
		public void DivisionFirstThenMultiplication()
		{
			// Arrange
			var script = "a = 100 / 2 * 10";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(500, variableValue.NumericValue);
		}
	}
}