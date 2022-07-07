#region Namespaces

using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;

#endregion Namespaces

namespace TurtleScript.TokenizedInterpreter.UnitTest
{
	public class TurtleScriptParserContextTests
	{
		#region Test Methods

		[Test]
		[Category("Scope")]
		public void Initialization_GlobalScopeOnly()
		{
			// Arrange
			TurtleScriptParserContext context = new TurtleScriptParserContext();

			// Act

			// Assert
			Assert.AreEqual(0, context.ScopeDepth);
			Assert.AreEqual(GLOBAL_SCOPE_NAME, context.Scope.Name);
		}

		[Test]
		[Category("Scope")]
		public void ScopeCreation()
		{
			// Arrange
			TurtleScriptParserContext context = new TurtleScriptParserContext();

			// Act
			context.PushScope(SCOPE_LEVEL_1_NAME);

			// Assert
			Assert.AreEqual(1, context.ScopeDepth);
			Assert.AreEqual(SCOPE_LEVEL_1_NAME, context.Scope.Name);
		}

		[Test]
		[Category("Scope")]
		public void ScopeCreation_TwoLevels()
		{
			// Arrange
			TurtleScriptParserContext context = new TurtleScriptParserContext();

			// Act
			context.PushScope(SCOPE_LEVEL_1_NAME);
			context.PushScope("Scope2");

			// Assert
			Assert.AreEqual(2, context.ScopeDepth);
			Assert.AreEqual("Scope2", context.Scope.Name);
		}

		[Test]
		[Category("Scope")]
		public void ScopeCreation_AddTwoRemoveOne()
		{
			// Arrange
			TurtleScriptParserContext context = new TurtleScriptParserContext();

			// Act
			context.PushScope(SCOPE_LEVEL_1_NAME);
			context.PushScope(SCOPE_LEVEL_2_NAME);

			context.PopScope();

			// Assert
			Assert.AreEqual(1, context.ScopeDepth);
			Assert.AreEqual(SCOPE_LEVEL_1_NAME, context.Scope.Name);
		}

		[Test]
		[Category("Scope")]
		public void ScopeCreation_AddTwoRemoveTwo()
		{
			// Arrange
			TurtleScriptParserContext context = new TurtleScriptParserContext();

			// Act
			context.PushScope(SCOPE_LEVEL_1_NAME);
			context.PushScope(SCOPE_LEVEL_2_NAME);

			context.PopScope();
			context.PopScope();

			// Assert
			Assert.AreEqual(0, context.ScopeDepth);
			Assert.AreEqual(GLOBAL_SCOPE_NAME, context.Scope.Name);
		}

		[Test]
		[Category("Variables")]
		public void GlobalVariable_NoNestedScope()
		{
			// Arrange
			TurtleScriptParserContext context = new TurtleScriptParserContext();

			// Act
			var undefinedVariable1 = UNDEFINED_VARIABLE_1;
			context.DeclareVariable(GLOBALVARIABLE_1, VariableType.Variable, new TokenAssignment(GLOBALVARIABLE_1));

			// Assert
			TurtleScriptParserScope foundInScope;
			bool result;

			result = context.IsVariableDeclared(GLOBALVARIABLE_1, out foundInScope);
			Assert.IsTrue(result);
			Assert.AreEqual(GLOBAL_SCOPE_NAME, foundInScope.Name);

			result = context.IsVariableDeclared(undefinedVariable1, out foundInScope);
			Assert.IsFalse(result);
			Assert.IsNull(foundInScope);
		}

		[Test]
		[Category("Variables")]
		public void GlobalVariable_OneNestedScopeNoDuplicateVariable()
		{
			// Arrange
			TurtleScriptParserContext context = new TurtleScriptParserContext();

			// Act
			var undefinedVariable1 = UNDEFINED_VARIABLE_1;
			context.DeclareVariable(GLOBALVARIABLE_1, VariableType.Variable, new TokenAssignment(GLOBALVARIABLE_1));

			context.PushScope(SCOPE_LEVEL_1_NAME);

			// Assert
			TurtleScriptParserScope foundInScope;
			bool result;

			result = context.IsVariableDeclared(GLOBALVARIABLE_1, out foundInScope);
			Assert.IsTrue(result);
			Assert.AreEqual(GLOBAL_SCOPE_NAME, foundInScope.Name);

			result = context.IsVariableDeclared(undefinedVariable1, out foundInScope);
			Assert.IsFalse(result);
			Assert.IsNull(foundInScope);
		}

		[Test]
		[Category("Variables")]
		public void ScopedVariable_OneNestedScopeNoDuplicateVariable()
		{
			// Arrange
			TurtleScriptParserContext context = new TurtleScriptParserContext();

			// Act
			context.DeclareVariable(GLOBALVARIABLE_1, VariableType.Variable, new TokenAssignment(GLOBALVARIABLE_1));

			context.PushScope(SCOPE_LEVEL_1_NAME);
			context.DeclareVariable(SCOPED_VARIABLE_1, VariableType.Variable, new TokenAssignment(SCOPED_VARIABLE_1));

			// Assert
			TurtleScriptParserScope foundInScope;
			bool result;

			result = context.IsVariableDeclared(GLOBALVARIABLE_1, out foundInScope);
			Assert.IsTrue(result);
			Assert.AreEqual(GLOBAL_SCOPE_NAME, foundInScope.Name);

			result = context.IsVariableDeclared(SCOPED_VARIABLE_1, out foundInScope);
			Assert.IsTrue(result);
			Assert.AreEqual(SCOPE_LEVEL_1_NAME, foundInScope.Name);

			result = context.IsVariableDeclared(UNDEFINED_VARIABLE_1, out foundInScope);
			Assert.IsFalse(result);
			Assert.IsNull(foundInScope);
		}

		[Test]
		[Category("Variables")]
		public void ScopedVariable_OneNestedScope_ScopeReplacesGlobalVariable()
		{
			// Arrange
			TurtleScriptParserContext context = new TurtleScriptParserContext();

			// Act
			context.DeclareVariable(GLOBALVARIABLE_1, VariableType.Variable, new TokenAssignment(GLOBALVARIABLE_1));

			context.PushScope(SCOPE_LEVEL_1_NAME);
			context.DeclareVariable(GLOBALVARIABLE_1, VariableType.Variable, new TokenAssignment(GLOBALVARIABLE_1));

			// Assert
			TurtleScriptParserScope foundInScope;
			bool result;

			result = context.IsVariableDeclared(GLOBALVARIABLE_1, out foundInScope);
			Assert.IsTrue(result);
			Assert.AreEqual(SCOPE_LEVEL_1_NAME, foundInScope.Name);

		}

		[Test]
		[Category("Variables")]
		public void ScopedVariable_GlobalAfterScopePop()
		{
			// Arrange
			TurtleScriptParserContext context = new TurtleScriptParserContext();

			// Act
			context.DeclareVariable(GLOBALVARIABLE_1, VariableType.Variable, new TokenAssignment(GLOBALVARIABLE_1));

			context.PushScope(SCOPE_LEVEL_1_NAME);
			context.PopScope();

			// Assert
			TurtleScriptParserScope foundInScope;
			bool result;

			result = context.IsVariableDeclared(GLOBALVARIABLE_1, out foundInScope);
			Assert.IsTrue(result);
			Assert.AreEqual(GLOBAL_SCOPE_NAME, foundInScope.Name);

		}

		[Test]
		[Category("Variables")]
		public void ScopedVariable_Scope1AfterScopePop()
		{
			// Arrange
			TurtleScriptParserContext context = new TurtleScriptParserContext();

			// Act
			context.PushScope(SCOPE_LEVEL_1_NAME);
			context.DeclareVariable(SCOPED_VARIABLE_1, VariableType.Variable, new TokenAssignment(SCOPED_VARIABLE_1));

			context.PushScope(SCOPE_LEVEL_2_NAME);
			context.PopScope();

			// Assert
			TurtleScriptParserScope foundInScope;
			bool result;

			result = context.IsVariableDeclared(SCOPED_VARIABLE_1, out foundInScope);
			Assert.IsTrue(result);
			Assert.AreEqual(SCOPE_LEVEL_1_NAME, foundInScope.Name);

		}

		#endregion Test Methods

		#region Private Constants

		private const string GLOBAL_SCOPE_NAME = "Global";
		private const string SCOPE_LEVEL_1_NAME = "Scope1";
		private const string SCOPE_LEVEL_2_NAME = "Scope2";
		private const string GLOBALVARIABLE_1 = "GlobalVar1";
		private const string SCOPED_VARIABLE_1 = "ScopedVar1";
		private const string UNDEFINED_VARIABLE_1 = "UndefinedVar1";

		#endregion Private Constants
	}
}

