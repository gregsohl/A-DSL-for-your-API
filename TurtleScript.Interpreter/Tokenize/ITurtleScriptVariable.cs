using System.Diagnostics;

namespace TurtleScript.Interpreter.Tokenize
{
	public interface ITurtleScriptVariable<T>
	{
		//TokenBase Declaration
		//{
		//	[DebuggerStepThrough]
		//	get;
		//}

		string Name
		{
			[DebuggerStepThrough]
			get;
		}

		VariableType Type
		{
			[DebuggerStepThrough]
			get;
		}

		TurtleScriptValue GetValue();

		void SetValue(TurtleScriptValue value);
	}
}
