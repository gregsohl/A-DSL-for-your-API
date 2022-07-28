#region Namespaces

using System;

using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class TestBase
	{

		#region Public Methods

		public TestContext RunTestWithExecutionError(
			string script)
		{
			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();

			executor.Execute(
				rootToken,
				context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);
			Assert.IsTrue(executor.IsError, executor.ErrorMessage);

			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(rootToken.ToTurtleScript());

			Console.WriteLine($"Execution Error: {executor.ErrorMessage}");

			return new TestContext(
				interpreter,
				context,
				executor);

		}

		public TestContext RunTestWithParserError(
			string script)
		{
			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);

			Console.WriteLine("Source Script:");
			Console.WriteLine(script);
			Console.WriteLine();

			// Assert - expect failure
			Assert.IsFalse(success, interpreter.ErrorMessage);

			Console.WriteLine($"Parser Errors:");

			foreach (string message in interpreter.ErrorMessages)
			{
				Console.WriteLine(message);
			}

			return new TestContext(
				interpreter);

		}

		#endregion Public Methods


		#region Protected Methods

		protected TestContext RunTest(
							string script,
			string variableName,
			object expectedValue)
		{
			return RunTest(
				script,
				variableName,
				expectedValue,
				script,
				TurtleScriptValueType.Numeric);
		}

		protected TestContext RunTest(
			string script,
			string variableName,
			object expectedValue,
			TurtleScriptValueType valueType)
		{
			return RunTest(
				script,
				variableName,
				expectedValue,
				script,
				valueType);
		}

		protected TestContext RunTest(
			string script,
			string variableName,
			object expectedValue,
			string expectedRegeneratedScript)
		{
			return RunTest(
				script,
				variableName,
				expectedValue,
				expectedRegeneratedScript,
				TurtleScriptValueType.Numeric);
		}

		protected TestContext RunTest(
			string script,
			string variableName,
			object expectedValue,
			string expectedRegeneratedScript,
			TurtleScriptValueType valueType)
		{
			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			executor.Execute(
				rootToken,
				context);
			string regeneratedTurtleScript = rootToken.ToTurtleScript();

			// Assert
			Assert.IsTrue(
				success,
				interpreter.ErrorMessage);

			Assert.AreEqual(
				expectedRegeneratedScript,
				regeneratedTurtleScript);

			Assert.IsFalse(
				executor.IsError,
				executor.ErrorMessage);

			TurtleScriptValue variableValue = context.GetVariableValue(variableName);
			Assert.AreEqual(
				valueType, 
				variableValue.ValueType);

			if (valueType == TurtleScriptValueType.Numeric)
			{
				Assert.AreEqual(
					expectedValue,
					variableValue.NumericValue);
			}

			if (valueType == TurtleScriptValueType.Boolean)
			{
				Assert.AreEqual(
					expectedValue,
					variableValue.BooleanValue);
			}

			Console.WriteLine("Source Script");
			Console.WriteLine(script);
			Console.WriteLine();
			Console.WriteLine("Regenerated Script via ToTurtleScript");
			Console.WriteLine(regeneratedTurtleScript);
			Console.WriteLine();
			Console.WriteLine($"Result: variable {variableName} = {variableValue.NumericValue}");

			return new TestContext(
				interpreter,
				context,
				executor);
		}

		#endregion Protected Methods
	}
}
