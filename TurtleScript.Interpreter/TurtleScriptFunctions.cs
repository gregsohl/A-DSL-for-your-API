#region Namespaces

using System.Collections.Generic;
using TurtleScript.Interpreter.Tokenize;

#endregion Namespaces

namespace TurtleScript.Interpreter
{
	internal class TurtleScriptFunctions<T> where T : ITurtleScriptFunction
	{
		#region Public Constructors

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TurtleScriptFunctions()
		{
			m_FunctionsDeclared = new Dictionary<string, T>();
		}

		#endregion Public Constructors


		#region Public Properties

		public int Count
		{
			get { return m_FunctionsDeclared.Count; }
		}

		#endregion Public Properties


		#region Public Methods

		public bool TryAdd(
			T function)
		{
			if (TryGetFunction(
					function.FunctionIdentifier,
					out _))
			{
				return false;
			}

			m_FunctionsDeclared.Add(function.FunctionIdentifier, function);
			return true;
		}


		//public ITurtleScriptFunction Add(
		//	string functionName,
		//	int parameterCount,
		//	TokenFunctionDeclaration declaration)
		//{
		//	if (!TryGetFunction(
		//			functionName,
		//			parameterCount,
		//			out _))
		//	{
		//		ITurtleScriptFunction function = TurtleScriptFunctionFactory.CreateFunction<T>(
		//			functionName,
		//			parameterCount,
		//			declaration);

		//		m_FunctionsDeclared.Add(functionName, function);

		//		return function;
		//	}

		//	return null;
		//}

		public bool TryGetFunction(
			string functionName,
			int parameterCount,
			out T function)
		{
			function = default(T);

			string functionIdentifier = TurtleScriptFunctionBase.CreateFunctionIdentifier(
				functionName,
				parameterCount);

			if (m_FunctionsDeclared.TryGetValue(
					functionIdentifier,
					out function))
			{
				return true;
			}

			return false;
		}

		public bool TryGetFunction(
			string functionIdentifier,
			out T function)
		{
			function = default(T);

			if (m_FunctionsDeclared.TryGetValue(
					functionIdentifier,
					out function))
			{
				return true;
			}

			return false;
		}


		#endregion Public Methods

		#region Private Fields

		private readonly Dictionary<string, T> m_FunctionsDeclared;

		#endregion Private Fields
	}
}
