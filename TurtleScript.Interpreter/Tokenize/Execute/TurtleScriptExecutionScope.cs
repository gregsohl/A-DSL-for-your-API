﻿#region Namespaces

using System.Diagnostics;

#endregion Namespaces


namespace TurtleScript.Interpreter.Tokenize
{
	public class TurtleScriptExecutionScope
	{
		#region Public Constructors

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TurtleScriptExecutionScope(
			int level,
			string name)
		{
			m_Level = level;
			m_Name = name;
			m_DeclaredVariables = new TurtleScriptVariables<TurtleScriptExecutionVariable>();
		}

		#endregion Public Constructors


		#region Public Properties

		public int VariableCount
		{
			get { return m_DeclaredVariables.Count; }
		}

		public int Level
		{
			[DebuggerStepThrough]
			get { return m_Level; }
		}

		public string Name
		{
			[DebuggerStepThrough]
			get { return m_Name; }
		}

		#endregion Public Properties


		#region Public Methods

		public void DeclareVariable(
			string variableName,
			VariableType variableType,
			TokenBase declaration)
		{
			m_DeclaredVariables.Add(variableName, variableType, declaration);
		}

		public bool IsVariableDeclared(
			string variableName)
		{
			return m_DeclaredVariables.IsVariableDeclared(variableName);
		}

		public bool TryGetVariable(
			string variableName,
			out ITurtleScriptVariable<TurtleScriptExecutionVariable> variable)
		{
			return m_DeclaredVariables.TryGetVariable(
				variableName,
				out variable);
		}

		public void SetVariableValue(
			string variableName,
			TurtleScriptValue value)
		{
			m_DeclaredVariables.SetVariableValue(variableName, value);
		}

		#endregion Public Methods


		#region Private Fields

		private readonly TurtleScriptVariables<TurtleScriptExecutionVariable> m_DeclaredVariables;
		private readonly int m_Level;
		private readonly string m_Name;

		#endregion Private Fields

	}
}
