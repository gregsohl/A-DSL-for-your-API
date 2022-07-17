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
	public class UnaryOperatorTests
	{
		[Test]
		public void UnaryNegation()
		{
			// Arrange
			const string SCRIPT = "a = -(1 + 1)";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = -2;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(SCRIPT);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);
			Assert.AreEqual(SCRIPT, rootToken.ToTurtleScript());

			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}


		[Test]
		public void UnaryNotTrueToFalse()
		{
			// Arrange
			const string SCRIPT = "a = !(1 == 1)";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(SCRIPT);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);
			Assert.AreEqual(SCRIPT, rootToken.ToTurtleScript());

			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.BooleanValue}");
		}

		[Test]
		public void UnaryNotFalseToTrue()
		{
			// Arrange
			const string SCRIPT = "a = !(1 != 1)";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(SCRIPT);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);
			Assert.AreEqual(SCRIPT, rootToken.ToTurtleScript());

			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.BooleanValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.BooleanValue}");
		}

	}
}