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

			Console.WriteLine("Regenerated Script, ToTurtleScript");
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
			var variableValue = context.GetVariableValue("a");
			Assert.IsTrue(variableValue.IsNumeric);
			Assert.AreEqual(3, variableValue.NumericValue);
		}

	}
}
