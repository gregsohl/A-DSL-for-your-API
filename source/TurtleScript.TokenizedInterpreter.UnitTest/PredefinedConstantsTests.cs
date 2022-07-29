#region Namespaces

using System;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Parse;
using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class PredefinedConstantsTests : TestBase
	{
		private const double PI = 3.141592654;

		[Test]
		public void Pi()
		{
			// Arrange
			const string SCRIPT = "a = pi";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = PI;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		public void PiMath()
		{
			// Arrange
			const string SCRIPT = "a = pi * 2";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = PI * 2;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}


	}
}