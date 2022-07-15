#region Namespaces

using System;
using System.Text;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class UserDefiniedFunctionTests
	{
		[Test]
		public void FunctionDeclarationAndCallNoParameters()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc()");
			scriptBuilder.AppendLine("	b = 15");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine("b = 0");
			scriptBuilder.AppendLine("MyFunc()");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 15;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			var variableValue = context.GetVariableValue(VARIABLE_NAME);
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());
			//Console.WriteLine($"Result: variable {VARIABLE_NAME} = {variableValue.NumericValue}");
		}

		[Test]
		public void FunctionDeclarationAndCallOneParameter()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc(myparameter)");
			scriptBuilder.AppendLine("	b = myparameter");
			scriptBuilder.AppendLine("	myparameter = b * 2");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine("MyFunc(10)");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["myparameter"];
			Assert.AreEqual(20, variableValue.NumericValue);
		}

		[Test]
		public void FunctionDeclarationAndCallThreeParameters()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc(value1, value2, value3)");
			scriptBuilder.AppendLine("	b = value1 + value2 - value3");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine("MyFunc(100, 200, 50)");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(250, variableValue.NumericValue);
		}

		[Test]
		public void FunctionDeclarationAfterCall()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 10");
			scriptBuilder.AppendLine("MyFunc()");
			scriptBuilder.AppendLine("def MyFunc()");
			scriptBuilder.AppendLine("	b = 15");
			scriptBuilder.AppendLine("end");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(15.0, variableValue.NumericValue);
		}

	}
}