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
			m_Variables = new Dictionary<string, TurtleScriptValue>();
			m_ScriptFunctions = new Dictionary<string, TurtleScriptFunction>();
		}

		public TurtleScriptValue GetVariableValue(string variableName)
		{
			TurtleScriptValue variableValue;
			if (m_Variables.TryGetValue(
					variableName,
					out variableValue))
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
			m_Variables[variableName] = variableValue;
		}

		public void SetVariableValue(
			string variableName,
			double variableValue)
		{
			m_Variables[variableName] = new TurtleScriptValue(variableValue);
		}

		private readonly List<ITurtleScriptRuntime> m_RuntimeLibraries;
		private Dictionary<string, TurtleScriptValue> m_Variables;
		private Dictionary<string, TurtleScriptFunction> m_ScriptFunctions;

	}
}
