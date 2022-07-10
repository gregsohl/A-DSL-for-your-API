using System.Diagnostics;

namespace TurtleScript.Interpreter.Tokenize.Parse
{
	public class TurtleScriptParserVariable : ITurtleScriptVariable<TurtleScriptParserVariable>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TurtleScriptParserVariable(
			string name,
			VariableType type)
		{
			m_Name = name;
			m_Type = type;
			//m_Declaration = declaration;
		}

		//public TokenBase Declaration
		//{
		//	[DebuggerStepThrough]
		//	get { return m_Declaration; }
		//}

		public string Name
		{
			[DebuggerStepThrough]
			get { return m_Name; }
		}

		public VariableType Type
		{
			[DebuggerStepThrough]
			get { return m_Type; }
		}

		public virtual TurtleScriptValue GetValue()
		{
			// No Action for Parser
			return TurtleScriptValue.NULL;
		}

		public virtual void SetValue(
			TurtleScriptValue value)
		{
			// No Action for Parser
		}

		protected bool Equals(
			TurtleScriptParserVariable other)
		{
			return m_Name == other.m_Name;
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object. </param>
		public override bool Equals(
			object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((TurtleScriptParserVariable)obj);
		}

		/// <summary>Serves as the default hash function. </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return (m_Name != null ? m_Name.GetHashCode() : 0);
		}

		private readonly string m_Name;
		private readonly VariableType m_Type;
		// private readonly TokenBase m_Declaration;
	}
}
