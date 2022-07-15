#region Namespaces

using System.Diagnostics;

#endregion Namespaces

namespace TurtleScript.Interpreter
{
	internal abstract class TurtleScriptFunctionBase
		: ITurtleScriptFunction
	{
		protected TurtleScriptFunctionBase(
			string name,
			int parameterCount)
		{
			m_Name = name;
			m_ParameterCount = parameterCount;

			m_FunctionIdentifier = CreateFunctionIdentifier(
				name,
				parameterCount);
		}

		public static string CreateFunctionIdentifier(
			string name,
			int parameterCount)
		{
			return name + "_" + parameterCount;
		}

		public string FunctionIdentifier
		{
			[DebuggerStepThrough]
			get { return m_FunctionIdentifier; }
		}

		public int ParameterCount
		{
			[DebuggerStepThrough]
			get { return m_ParameterCount; }
		}

		public string Name
		{
			get { return m_Name; }
		}

		protected bool Equals(
			TurtleScriptFunctionBase other)
		{
			return m_FunctionIdentifier == other.m_FunctionIdentifier;
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object. </param>
		public override bool Equals(
			object obj)
		{
			if (ReferenceEquals(
					null,
					obj))
				return false;
			if (ReferenceEquals(
					this,
					obj))
				return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((TurtleScriptFunctionBase)obj);
		}

		/// <summary>Serves as the default hash function. </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return m_FunctionIdentifier != null ? m_FunctionIdentifier.GetHashCode() : 0;
		}

		private readonly string m_FunctionIdentifier;
		private readonly string m_Name;
		private readonly int m_ParameterCount;
	}
}
