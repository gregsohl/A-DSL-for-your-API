#region Namespaces

using System;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class ArithmeticTests
	{
		[Test]
		[Category("Success")]
		public void SimpleAddition()
		{
			// Arrange
			var script = "a = 1 + 1";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 2;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		[Category("Success")]
		public void ComplexAddition()
		{
			// Arrange
			var script = "a = 1 + 1 + 2 + 4";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 8;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}


		[Test]
		[Category("Success")]
		public void SimpleSubtraction()
		{
			// Arrange
			var script = "a = 10 - 2";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 8;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		[Category("Success")]
		public void ComplexSubtraction()
		{
			// Arrange
			var script = "a = 10 - 2 - 4";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 4;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		[Category("Success")]
		public void SimpleMultiplication()
		{
			// Arrange
			var script = "a = 5 * 6";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 30;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		[Category("Success")]
		public void ComplexMultiplication()
		{
			// Arrange
			var script = "a = 5 * 6 * 2 * 10";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 600;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		[Category("Success")]
		public void SimpleDivision()
		{
			// Arrange
			var script = "a = 10 / 2";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 5;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		[Category("Success")]
		public void DivideByZero()
		{
			// Arrange
			var script = "a = 5 / 0";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 0;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		[Category("Success")]
		public void ComplexDivision()
		{
			// Arrange
			var script = "a = 100 / 2 / 5";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 10;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		[Category("Success")]
		public void AdditionFirstThenMultiplication()
		{
			// Arrange
			var script = "a = 10 + 10 * 100";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 1010;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.GreaterOrEqual(1, context.GlobalVariableCount);
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		[Category("Success")]
		public void MultiplicationFirstThenAddition()
		{
			// Arrange
			var script = "a = 10 * 100 + 10";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 1010;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		[Category("Success")]
		public void MultiplicationFirstThenDivision()
		{
			// Arrange
			var script = "a = 10 * 100 / 2";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 500;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		[Category("Success")]
		public void DivisionFirstThenMultiplication()
		{
			// Arrange
			var script = "a = 100 / 2 * 10";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 500;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		[Category("Error")]
		public void SimpleAdditionMismatchedOperandTypes()
		{
			// Arrange
			var script = "a = 1 + (1 > 2)";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();

			//Assert.Throws<TurtleScriptExecutionException>(() => 
			//	executor.Execute(
			//		rootToken,
			//		context));

			executor.Execute(
				rootToken,
				context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);
			Assert.IsTrue(executor.IsError, executor.ErrorMessage);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());

			Console.WriteLine($"Execution Error: {executor.ErrorMessage}");
		}

	}
}