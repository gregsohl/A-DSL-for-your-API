#region Namespaces

using System;
using System.Collections.Generic;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize.Parse
{
	public class TurtleScriptParserContext
	{
		#region Public Constructors

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TurtleScriptParserContext()
		{
			m_RuntimeLibraries = new List<ITurtleScriptRuntime>();
			m_ScriptFunctions = new Dictionary<string, TurtleScriptParserFunction>();
			m_GlobalScope = new TurtleScriptParserScope(0, "Global");
			m_ScopeStack = new Stack<TurtleScriptParserScope>();

			m_CurrentScope = null;
		}

		#endregion Public Constructors


		#region Public Properties

		public TurtleScriptParserScope Scope
		{
			get
			{
				if (m_CurrentScope != null)
				{
					return m_CurrentScope;
				}

				return m_GlobalScope;
			}
		}

		public int ScopeDepth
		{
			get
			{
				if (m_CurrentScope != null)
				{
					return m_CurrentScope.Level;
				}

				return 0;
			}
		}

		#endregion Public Properties


		#region Public Methods

		public void DeclareVariable(
			string variableName,
			VariableType variableType,
			TokenBase declaration)
		{
			if (m_CurrentScope != null)
			{
				m_CurrentScope.DeclareVariable(variableName, variableType, declaration);
			}
			else
			{
				m_GlobalScope.DeclareVariable(variableName, variableType, declaration);
			}
		}

		/// <summary>
		/// Determines whether the specified variable is declared. Searches in the
		/// current scope and global scope
		/// </summary>
		/// <param name="variableName">Name of the variable.</param>
		/// <param name="scope">The scope the variable is declared in.</param>
		/// <returns>
		///   <c>true</c> if the variable is declared; otherwise, <c>false</c>.
		/// </returns>
		public bool IsVariableDeclared(
			string variableName,
			out TurtleScriptParserScope scope)
		{
			scope = null;

			if (m_CurrentScope != null)
			{
				if (m_CurrentScope.IsVariableDeclared(variableName))
				{
					scope = m_CurrentScope;
					return true;
				}
			}

			bool isVariableDeclared = m_GlobalScope.IsVariableDeclared(variableName);
			if (isVariableDeclared)
			{
				scope = m_GlobalScope;
			}

			return isVariableDeclared;
		}

		public void PopScope()
		{
			if (m_ScopeStack.Count > 0)
			{
				m_ScopeStack.Pop();

				if (m_ScopeStack.Count > 0)
				{
					m_CurrentScope = m_ScopeStack.Peek();
				}
				else
				{
					m_CurrentScope = null;
				}
			}
		}

		public void PushScope(string name)
		{
			int level = 1;

			if (m_CurrentScope != null)
			{
				level = m_CurrentScope.Level + 1;
			}

			m_CurrentScope = new TurtleScriptParserScope(level, name);
			m_ScopeStack.Push(m_CurrentScope);
		}

		public bool TryGetRuntimeLibrary(
			string runtimeName,
			out ITurtleScriptRuntime foundRuntime)
		{
			foreach (ITurtleScriptRuntime turtleScriptRuntime in m_RuntimeLibraries)
			{
				if (turtleScriptRuntime.Namespace == runtimeName)
				{
					foundRuntime = turtleScriptRuntime;
					return true;
				}
			}

			foundRuntime = null;
			return false;
		}

		#endregion Public Methods


		#region Private Fields

		private readonly TurtleScriptParserScope m_GlobalScope;
		private readonly List<ITurtleScriptRuntime> m_RuntimeLibraries;
		private readonly Stack<TurtleScriptParserScope> m_ScopeStack;
		private TurtleScriptParserScope m_CurrentScope;
		private Dictionary<string, TurtleScriptParserFunction> m_ScriptFunctions;

		#endregion Private Fields
	}
}
