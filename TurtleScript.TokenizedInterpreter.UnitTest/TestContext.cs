#region Namespaces

using System.Diagnostics;

using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class TestContext
	{

		#region Public Constructors

		public TestContext(
			TurtleScriptTokenizer interpreter)
			: this(interpreter, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TestContext(
			TurtleScriptTokenizer interpreter,
			TurtleScriptExecutionContext executionContext,
			TurtleScriptExecutor executor)
		{
			m_Interpreter = interpreter;
			m_ExecutionContext = executionContext;
			m_Executor = executor;
		}

		#endregion Public Constructors


		#region Public Properties

		public TurtleScriptExecutionContext ExecutionContext
		{
			[DebuggerStepThrough]
			get { return m_ExecutionContext; }
		}

		public TurtleScriptExecutor Executor
		{
			[DebuggerStepThrough]
			get { return m_Executor; }
		}

		public TurtleScriptTokenizer Interpreter
		{
			[DebuggerStepThrough]
			get { return m_Interpreter; }
		}

		#endregion Public Properties


		#region Private Fields

		readonly TurtleScriptExecutionContext m_ExecutionContext;
		readonly TurtleScriptExecutor m_Executor;
		readonly TurtleScriptTokenizer m_Interpreter;

		#endregion Private Fields
	}
}
