#region Namespaces

using System;

using NUnit.Framework;

using KellermanSoftware.CompareNetObjects;

using TurtleScript.Interpreter.Tokenize;

#endregion Namespaces


namespace TurtleScript.TokenizedInterpreter.UnitTest
{
	internal class TokenSerializationTests
	{
		[Test]
		[Category("Success")]
		public void SerializeDeserialize_NumericValue()
		{
			// Arrange
			TokenNumericValue token = new TokenNumericValue(55, 1, 2);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			TokenNumericValue reconstitutedToken =
				(TokenNumericValue)TokenSerializer.DeserializeFromArray(serializedData);

			// Assert
			VerifySerialization(token,
				reconstitutedToken);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_BooleanValue()
		{
			// Arrange
			TokenBooleanValue token = new TokenBooleanValue(true, 1, 2);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			TokenBooleanValue reconstitutedToken =
				(TokenBooleanValue)TokenSerializer.DeserializeFromArray(serializedData);

			// Assert
			VerifySerialization(token,
				reconstitutedToken);
		}


		private static void VerifySerialization(
			TokenBase token,
			TokenBase reconstitutedToken)
		{
			CompareLogic compareLogic = new CompareLogic();
			ComparisonResult comparisonResult = compareLogic.Compare(
				token,
				reconstitutedToken);

			if (!comparisonResult.AreEqual)
			{
				Console.WriteLine(comparisonResult.DifferencesString);
			}
			Assert.IsTrue(comparisonResult.AreEqual);
		}
	}
}
