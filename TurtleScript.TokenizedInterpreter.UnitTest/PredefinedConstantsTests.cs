#region Namespaces

using System;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Parse;
using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class PredefinedConstantsTests
	{
		private const double PI = 3.141592654;

		[Test]
		public void Pi()
		{
			// Arrange
			var script = "a = pi";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = PI;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			(new TurtleScriptExecutor()).Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);
			Assert.AreEqual(script, rootToken.ToTurtleScript());

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void PiMath()
		{
			// Arrange
			var script = "a = pi * 2";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = PI * 2;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			(new TurtleScriptExecutor()).Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);
			Assert.AreEqual(script, rootToken.ToTurtleScript());

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}


	}
}