#region Namespaces

using System.Collections.Generic;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	public class TurtleScriptExecutionContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TurtleScriptExecutionContext()
		{
			m_RuntimeLibraries = new List<ITurtleScriptRuntime>();
			Variables = new Dictionary<string, TurtleScriptValue>();
			m_ScriptFunctions = new Dictionary<string, TurtleScriptFunction>();
		}

		public TurtleScriptValue GetVariableValue(string variableName)
		{
			if (Variables.TryGetValue(
					variableName,
					out var variableValue))
			{
				return variableValue;
			}


			// TODO: Implement runtime error handling
			return TurtleScriptValue.NULL;

			//throw new InvalidOperationException(
			//	string.Format(
			//		"Reference to an unknown variable, '{0}'. Line {1}, Col {2}",
			//		variableName,
			//		context.Start.Line,
			//		context.Start.Column));

		}

		public void SetVariableValue(
			string variableName,
			TurtleScriptValue variableValue)
		{
			Variables[variableName] = variableValue;
		}

		public void SetVariableValue(
			string variableName,
			double variableValue)
		{
			Variables[variableName] = new TurtleScriptValue(variableValue);
		}

		public Dictionary<string, TurtleScriptValue> Variables { get; }


		private readonly List<ITurtleScriptRuntime> m_RuntimeLibraries;
		private Dictionary<string, TurtleScriptFunction> m_ScriptFunctions;

	}
}
