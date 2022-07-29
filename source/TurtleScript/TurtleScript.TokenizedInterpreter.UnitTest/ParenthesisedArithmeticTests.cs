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
	public class ParenthesisedArithmeticTests : TestBase
	{
		[Test]
		public void AdditionAndMultiplication()
		{
			// Arrange
			const string SCRIPT = "a = (1 + 2) * 3";
			const string VARIABLE_NAME = "a";
			const int EXPECTED_VALUE = 9;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}


		[Test]
		public void MultipleLevels()
		{
			// Arrange
			const string SCRIPT = "a = (((1 + 2) * (3 + 4) + 5) * 2)";
			const string VARIABLE_NAME = "a";
			const int EXPECTED_VALUE = 52;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

	}
}