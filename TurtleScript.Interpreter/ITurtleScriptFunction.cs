using System.Diagnostics;

namespace TurtleScript.Interpreter
{
	public interface ITurtleScriptFunction
	{
		string FunctionIdentifier
		{
			[DebuggerStepThrough]
			get;
		}

		int ParameterCount
		{
			[DebuggerStepThrough]
			get;
		}

		string Name { get; }
	}
}
