#region Namespaces

using System;
using System.Collections.Generic;

#endregion Namespaces

namespace TurtleScript.Interpreter
{
	public interface ITurtleScriptRuntime
	{
		string Namespace { get; }

		Dictionary<string, Func<List<TurtleScriptValue>, TurtleScriptValue>> Functions { get; }
	}
}
