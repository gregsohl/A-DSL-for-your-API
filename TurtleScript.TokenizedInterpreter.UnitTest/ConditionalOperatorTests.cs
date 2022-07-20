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
	public class ConditionalOperatorTests
	{
		[Test]
		public void AndConditionTrue()
		{
			// Arrange
			var script = "a = 1 < 2 && 3 > 2";
			const string VARIABLE_NAME = "a";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsTrue(variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void AndConditionFalse()
		{
			// Arrange
			var script = "a = 1 > 2 && 3 > 2";

			const string VARIABLE_NAME = "a";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsFalse(variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void OrConditionBothTrue()
		{
			// Arrange
			var script = "a = 1 < 2 || 3 > 2";

			const string VARIABLE_NAME = "a";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsTrue(variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void OrConditionOneTrue()
		{
			// Arrange
			var script = "a = 1 < 2 || 3 < 2";

			const string VARIABLE_NAME = "a";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsTrue(variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void OrConditionFalse()
		{
			// Arrange
			var script = "a = 1 > 2 || 3 < 2";

			const string VARIABLE_NAME = "a";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsFalse(variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void AndOrConditionTrue()
		{
			// Arrange
			var script = "a = 1 < 2 || 3 < 2 && 1 == 1";

			const string VARIABLE_NAME = "a";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsTrue(variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void AndOrConditionFalse()
		{
			// Arrange
			var script = "a = 1 > 2 || 3 < 2 && 1 == 1";

			const string VARIABLE_NAME = "a";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.IsFalse(variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void Not_Simple_True()
		{
			// Arrange
			var script = "a = !(1 > 2)";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}


		[Test]
		public void NotOrConditionTrue()
		{
			// Arrange
			var script = "a = !(1 < 2) || (3 > 2)";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void NotAndConditionTrue()
		{
			// Arrange
			var script = "a = !(!(1 < 2) && (3 > 2))";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void GreaterThan_True()
		{
			// Arrange
			var script = "a = 3 > 2";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void GreaterThanOrEqual_Greater_True()
		{
			// Arrange
			var script = "a = 3 >= 2";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void GreaterThanOrEqual_Equal_True()
		{
			// Arrange
			var script = "a = 3 >= 3";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void GreaterThan_False()
		{
			// Arrange
			var script = "a = 3 > 4";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void GreaterThanOrEqual_Greater_False()
		{
			// Arrange
			var script = "a = 3 >= 4";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}


		[Test]
		public void LessThan_True()
		{
			// Arrange
			var script = "a = 2 < 3";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void LessThanOrEqual_Greater_True()
		{
			// Arrange
			var script = "a = 2 <= 3";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void LessThanOrEqual_Equal_True()
		{
			// Arrange
			var script = "a = 3 <= 3";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void LessThan_False()
		{
			// Arrange
			var script = "a = 4 < 3";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void LessThanOrEqual_Greater_False()
		{
			// Arrange
			var script = "a = 4 <= 3";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsBoolean);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}



	}
}