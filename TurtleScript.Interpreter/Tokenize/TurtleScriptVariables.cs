#region Namespaces

using System.Collections.Generic;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	internal class TurtleScriptVariables<T>
	{
		#region Public Constructors

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TurtleScriptVariables()
		{
			m_VariablesDeclared = new Dictionary<string, ITurtleScriptVariable<T>>();
		}

		#endregion Public Constructors

		public int Count
		{
			get { return m_VariablesDeclared.Count; }
		}

		#region Public Methods

		public void Add(
			string variableName,
			VariableType variableType,
			TokenBase declaration)
		{
			if (!m_VariablesDeclared.ContainsKey(variableName))
			{
				ITurtleScriptVariable<T> variable = TurtleScriptVariableFactory.CreateVariable<T>(
					variableName,
					variableType,
					declaration);

				m_VariablesDeclared.Add(variableName, variable);
			}
		}

		public bool TryGetVariable(
			string variableName,
			out ITurtleScriptVariable<T> variable)
		{
			variable = null;

			if (m_VariablesDeclared.TryGetValue(
					variableName,
					out variable))
			{
				return true;
			}

			return false;
		}

		public TurtleScriptValue GetVariableValue(
			string variableName)
		{
			if (m_VariablesDeclared.TryGetValue(
					variableName,
					out ITurtleScriptVariable<T> variable))
			{
				return variable.GetValue();
			}

			return TurtleScriptValue.NULL;
		}

		public bool IsVariableDeclared(
			string variableName)
		{
			return m_VariablesDeclared.ContainsKey(variableName);
		}
		public void SetVariableValue(
			string variableName,
			TurtleScriptValue value)
		{
			if (m_VariablesDeclared.TryGetValue(
					variableName,
					out ITurtleScriptVariable<T> variable))
			{
				variable.SetValue(value);
			}
		}

		#endregion Public Methods


		#region Private Fields

		private readonly Dictionary<string, ITurtleScriptVariable<T>> m_VariablesDeclared;

		#endregion Private Fields
	}
}
