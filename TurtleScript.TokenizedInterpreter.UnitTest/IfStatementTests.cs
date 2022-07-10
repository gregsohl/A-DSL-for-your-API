#region Namespaces

using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

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

			const string VARIABLE_NAME1 = "b";
			const double EXPECTED_VALUE1 = 20;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME1);
			Assert.AreEqual(EXPECTED_VALUE1, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME1} = {variableValue.NumericValue}");

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

			const string VARIABLE_NAME1 = "b";
			const double EXPECTED_VALUE1 = 10;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME1);
			Assert.AreEqual(EXPECTED_VALUE1, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME1} = {variableValue.NumericValue}");
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

			const string VARIABLE_NAME1 = "b";
			const double EXPECTED_VALUE1 = 40;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME1);
			Assert.AreEqual(EXPECTED_VALUE1, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME1} = {variableValue.NumericValue}");
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

			const string VARIABLE_NAME1 = "b";
			const double EXPECTED_VALUE1 = 40;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME1);
			Assert.AreEqual(EXPECTED_VALUE1, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME1} = {variableValue.NumericValue}");
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

			const string VARIABLE_NAME1 = "b";
			const double EXPECTED_VALUE1 = 10;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME1);
			Assert.AreEqual(EXPECTED_VALUE1, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME1} = {variableValue.NumericValue}");
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

			const string VARIABLE_NAME1 = "b";
			const double EXPECTED_VALUE1 = 10;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME1);
			Assert.AreEqual(EXPECTED_VALUE1, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME1} = {variableValue.NumericValue}");
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

			const string VARIABLE_NAME1 = "b";
			const double EXPECTED_VALUE1 = 1;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME1);
			Assert.AreEqual(EXPECTED_VALUE1, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME1} = {variableValue.NumericValue}");
		}

	}
}