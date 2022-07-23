#region Namespaces

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

using TurtleScript.Interpreter;
using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

#endregion Namespaces

namespace TurtleScript.TokenizedInterpreter.UnitTest
{
	public class RuntimeTests
	{
		[Test]
		public void SingleParameter()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = test.square(2)");

			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 4;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder
				.ToString(),
				new List<ITurtleScriptRuntime>() { new SampleRuntime() });
			TurtleScriptExecutionContext context =
				new TurtleScriptExecutionContext(
					new List<ITurtleScriptRuntime>() { new SampleRuntime() });
			TurtleScriptExecutor executor = new TurtleScriptExecutor();


			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			executor.Execute(rootToken, context);

			// Assert
			// Parser Success
			Assert.IsTrue(success, interpreter.ErrorMessage);

			// Executor Success
			Assert.IsFalse(executor.IsError, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void ThreeParameters()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = test.sum(2, 4, 6)");

			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 12;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder
					.ToString(),
				new List<ITurtleScriptRuntime>() { new SampleRuntime() });
			TurtleScriptExecutionContext context =
				new TurtleScriptExecutionContext(
					new List<ITurtleScriptRuntime>() { new SampleRuntime() });
			TurtleScriptExecutor executor = new TurtleScriptExecutor();


			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			executor.Execute(rootToken, context);

			// Assert
			// Parser Success
			Assert.IsTrue(success, interpreter.ErrorMessage);

			// Executor Success
			Assert.IsFalse(executor.IsError, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}
	}
}