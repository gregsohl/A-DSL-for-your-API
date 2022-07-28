#region Namespaces

using System;
using System.Globalization;

using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class NumericConstantTests : TestBase
	{
		[Test]
		[Category("Success")]
		public void IntTooBig()
		{
			// Arrange
			string script = "a = ";
			script += "99999999" + int.MaxValue.ToString("N0", CultureInfo.InvariantCulture).Replace(",", "");

			RunTestWithParserError(
				script);
		}

		[Test]
		[Category("Success")]
		public void NumericTooBig()
		{
			// Arrange
			string script = "a = ";
			script += "99999999" + double.MaxValue.ToString("N0", CultureInfo.InvariantCulture).Replace(",", "") + "123456.345";

			RunTestWithParserError(
				script);
		}


		[Test]
		[Category("Success")]
		public void NumericMultipleDecimalPoints()
		{
			// Arrange
			const string SCRIPT = "a = 123.456.78";

			RunTestWithParserError(
				SCRIPT);
		}

		[Test]
		[Category("Success")]
		public void IntDoubleNegation()
		{
			// Arrange
			const string SCRIPT = "a = --123";

			RunTest(
				SCRIPT,
				"a",
				123);
		}

	}
}