#region Namespaces

using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize.Execute
{
	public class TurtleScriptExecutionContext
	{
		#region Public Constructors

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TurtleScriptExecutionContext(
			List<ITurtleScriptRuntime> runtimeLibraries = null)
		{
			m_GlobalScope = new TurtleScriptExecutionScope(0, "Global");
			m_ScopeStack = new Stack<TurtleScriptExecutionScope>();
			m_CurrentScope = null;

			m_RuntimeLibraries = runtimeLibraries ?? new List<ITurtleScriptRuntime>();
			m_Functions = new TurtleScriptFunctions<TurtleScriptExecutionFunction>();
		}

		#endregion Public Constructors


		#region Public Properties

		public int GlobalVariableCount
		{
			[DebuggerStepThrough]
			get { return m_GlobalScope.VariableCount; }
		}

		public TurtleScriptExecutionScope Scope
		{
			[DebuggerStepThrough]
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
			[DebuggerStepThrough]
			get
			{
				if (m_CurrentScope != null)
				{
					return m_CurrentScope.Level;
				}

				return 0;
			}
		}

		public int UserDefinedFunctionCount
		{
			[DebuggerStepThrough]
			get { return m_Functions.Count; }
		}

		#endregion Public Properties


		#region Public Methods

		public TurtleScriptValue GetVariableValue(string variableName)
		{
			ITurtleScriptVariable<TurtleScriptExecutionVariable> variable;

			if (m_CurrentScope != null)
			{
				if (m_CurrentScope.TryGetVariable(
						variableName,
						out variable))
				{
					return variable.GetValue();
				}
			}

			if (m_GlobalScope.TryGetVariable(
					variableName,
					out variable))
			{
				return variable.GetValue();
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

		public bool TryGetVariable(
			string variableName,
			out ITurtleScriptVariable<TurtleScriptExecutionVariable> variable)
		{
			if (m_CurrentScope != null)
			{
				if (m_CurrentScope.TryGetVariable(
						variableName,
						out variable))
				{
					return true;
				}
			}

			if (m_GlobalScope.TryGetVariable(
					variableName,
					out variable))
			{
				return true;
			}

			// TODO: Implement runtime error handling
			return false;

			//throw new InvalidOperationException(
			//	string.Format(
			//		"Reference to an unknown variable, '{0}'. Line {1}, Col {2}",
			//		variableName,
			//		context.Start.Line,
			//		context.Start.Column));

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

			m_CurrentScope = new TurtleScriptExecutionScope(level, name);
			m_ScopeStack.Push(m_CurrentScope);
		}

		/// <summary>
		/// Sets the variable value.
		/// </summary>
		/// <remarks>
		/// Rules / order of variable scope identification
		/// <list type="number">
		/// <item>If the variable exists in the current scope, its value is updated there.</item>
		/// <item>If the variable exists in the global scope, its value is updated there.</item>
		/// <item>If there is a current nested scope, the variable is declared and its value is set there.</item>
		/// <item>If no other condition is satisfied, the variable is declared in the global scope and its value is set there.</item>
		/// </list>
		/// </remarks>
		/// <param name="variableName">Name of the variable.</param>
		/// <param name="variableType">Type of the variable.</param>
		/// <param name="declaration">The declaration.</param>
		/// <param name="variableValue">The variable value.</param>
		public void SetVariableValue(
			string variableName,
			VariableType variableType,
			TokenBase declaration,
			TurtleScriptValue variableValue)
		{
			ITurtleScriptVariable<TurtleScriptExecutionVariable> variable;

			// If the variable exists in the current scope, its value is updated there.
			if (m_CurrentScope != null)
			{
				if (m_CurrentScope.TryGetVariable(
						variableName,
						out variable))
				{
					m_CurrentScope.SetVariableValue(variableName, variableValue);
					return;
				}
			}

			// If the variable exists in the global scope, its value is updated there.
			if (m_GlobalScope.TryGetVariable(
					variableName,
					out variable))
			{
				m_GlobalScope.SetVariableValue(variableName, variableValue);
				return;
			}

			// If there is a current nested scope, the variable is declared and its value is set there.
			if (m_CurrentScope != null)
			{
				m_CurrentScope.DeclareVariable(
					variableName,
					variableType,
					declaration);

				m_CurrentScope.SetVariableValue(variableName, variableValue);
				return;
			}

			// If no other condition is satisfied, the variable is declared in the global scope and its value is set there.
			m_GlobalScope.DeclareVariable(
				variableName,
				variableType,
				declaration);

			m_GlobalScope.SetVariableValue(
				variableName,
				variableValue);
		}

		public void SetVariableValue(
			string variableName,
			VariableType variableType,
			TokenBase declaration,
			double variableValue)
		{
			SetVariableValue(
				variableName,
				variableType,
				declaration,
				new TurtleScriptValue(variableValue));
		}

		public void DeclareFunction(
			TokenFunctionDeclaration functionToken)
		{
			TurtleScriptExecutionFunction function = new TurtleScriptExecutionFunction(
				functionToken.FunctionName,
				functionToken.ParameterCount,
				functionToken);

			m_Functions.TryAdd(function);
		}

		public bool TryGetFunction(
			string functionName,
			int parameterCount,
			out TokenFunctionDeclaration functionToken)
		{
			functionToken = null;

			if (m_Functions.TryGetFunction(
					functionName,
					parameterCount,
					out TurtleScriptExecutionFunction function))
			{
				functionToken = function.Declaration;
				return true;
			}

			return false;
		}

		public bool TryGetRuntimeLibrary(
			string runtimeName,
			out ITurtleScriptRuntime runtime)
		{
			runtime = null;
			foreach (ITurtleScriptRuntime turtleScriptRuntime in m_RuntimeLibraries)
			{
				if (turtleScriptRuntime.Namespace == runtimeName)
				{
					runtime = turtleScriptRuntime;
					return true;
				}
			}

			return false;
		}

		public bool TryGetRuntimeFunction(
			ITurtleScriptRuntime runtime,
			string functionName,
			int parameterCount,
			out TurtleScriptRuntimeFunction function)
		{
			var functionIdentifier = TurtleScriptFunctionBase.CreateFunctionIdentifier(
				functionName,
				parameterCount);

			return runtime.Functions.TryGetValue(functionIdentifier, out function);
		}

		#endregion Public Methods


		#region Private Fields

		private readonly TurtleScriptExecutionScope m_GlobalScope;
		private readonly List<ITurtleScriptRuntime> m_RuntimeLibraries;
		private readonly Stack<TurtleScriptExecutionScope> m_ScopeStack;
		private TurtleScriptExecutionScope m_CurrentScope;
		private readonly TurtleScriptFunctions<TurtleScriptExecutionFunction> m_Functions;

		#endregion Private Fields
	}
}
