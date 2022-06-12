#region Namespaces

using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	internal class TokenizerTests
	{

		[Test]
		public void TokenizeVariableAssignment()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 1 + 2");

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);
		}

		[Test]
		public void TokenizeVariableAssignmentToTurtleScript()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			const string SCRIPT = "a = 1 + 2";
			scriptBuilder.AppendLine(SCRIPT);

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);

			// Assert
			var generatedScript = rootToken.ToTurtleScript();
			Assert.AreEqual(SCRIPT, generatedScript);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(generatedScript);
		}

		[Test]
		public void TokenizeVariableAssignmentExecute()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 1 + 2");

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());
			bool success = interpreter.Parse(out TokenBase rootToken);

			// Act
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();

			interpreter.Execute(rootToken, context);

			// Assert
			const string VARIABLE_NAME = "a";
			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(3, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void AdditionAndMultiplication()
		{
			// Arrange
			const string SCRIPT = "a = (1 + 2) * 3";

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(SCRIPT);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);
			Assert.AreEqual(SCRIPT, rootToken.ToTurtleScript());

			const string VARIABLE_NAME = "a";
			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.AreEqual(9, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");

		}

	}
}
