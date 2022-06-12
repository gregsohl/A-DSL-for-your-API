#region Namespaces

using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;

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

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(2, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void ComplexAddition()
		{
			// Arrange
			var script = "a = 1 + 1 + 2 + 4";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(8, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}


		[Test]
		public void SimpleSubtraction()
		{
			// Arrange
			var script = "a = 10 - 2";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(8, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void ComplexSubtraction()
		{
			// Arrange
			var script = "a = 10 - 2 - 4";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(4, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void SimpleMultiplication()
		{
			// Arrange
			var script = "a = 5 * 6";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(30, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void ComplexMultiplication()
		{
			// Arrange
			var script = "a = 5 * 6 * 2 * 10";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(600, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void SimpleDivision()
		{
			// Arrange
			var script = "a = 10 / 2";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(5, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void DivideByZero()
		{
			// Arrange
			var script = "a = 5 / 0";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(0, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void ComplexDivision()
		{
			// Arrange
			var script = "a = 100 / 2 / 5";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(10, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void AdditionFirstThenMultiplication()
		{
			// Arrange
			var script = "a = 10 + 10 * 100";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(1010, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void MultiplicationFirstThenAddition()
		{
			// Arrange
			var script = "a = 10 * 100 + 10";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(1010, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void MultiplicationFirstThenDivision()
		{
			// Arrange
			var script = "a = 10 * 100 / 2";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(500, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void DivisionFirstThenMultiplication()
		{
			// Arrange
			var script = "a = 100 / 2 * 10";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(500, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}
	}
}