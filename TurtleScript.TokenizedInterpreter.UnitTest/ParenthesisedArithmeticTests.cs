#region Namespaces

using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class ParenthesisedArithmeticTests
	{
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