#region Namespaces

using System;
using System.Collections.Generic;

#endregion Namespaces

namespace TurtleScript.Interpreter
{
	public class TurtleScriptValue : IComparable<TurtleScriptValue>
	{
		#region Public Constructors

		public TurtleScriptValue(bool boolValue)
		{
			m_BoolValue = boolValue;
			m_IsBoolean = true;
		}

		public TurtleScriptValue(float numericValue)
		{
			m_NumericValue = numericValue;
			m_IsNumeric = true;
		}

		#endregion Public Constructors

		#region Public Properties

		public static Comparer<TurtleScriptValue> ValueComparer { get; } = new ValueRelationalComparer();

		public bool IsBoolean
		{
			get { return m_IsBoolean; }
		}

		public bool IsNumeric
		{
			get { return m_IsNumeric; }
		}

		public float NumericValue
		{
			get { return m_NumericValue; }
		}

		public bool BoolValue
		{
			get { return m_BoolValue; }
		}

		#endregion Public Properties

		#region Public Methods

		public int CompareTo(TurtleScriptValue other)
		{
			if (ReferenceEquals(this,
				other)) return 0;
			if (ReferenceEquals(null,
				other)) return 1;
			return m_NumericValue.CompareTo(other.m_NumericValue);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null,
				obj)) return false;
			if (ReferenceEquals(this,
				obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((TurtleScriptValue)obj);
		}

		public override int GetHashCode()
		{
			return m_NumericValue.GetHashCode();
		}

		#endregion Public Methods

		#region Protected Methods

		protected bool Equals(TurtleScriptValue other)
		{
			return m_NumericValue.Equals(other.m_NumericValue);
		}

		#endregion Protected Methods


		#region Private Fields

		private readonly bool m_BoolValue;
		private readonly bool m_IsBoolean;
		private readonly float m_NumericValue;
		private readonly bool m_IsNumeric;

		#endregion Private Fields

		#region Private Classes

		private sealed class ValueRelationalComparer : Comparer<TurtleScriptValue>
		{

			#region Public Methods

			public override int Compare(TurtleScriptValue x,
										TurtleScriptValue y)
			{
				if (ReferenceEquals(x,
					y)) return 0;
				if (ReferenceEquals(null,
					y)) return 1;
				if (ReferenceEquals(null,
					x)) return -1;
				return x.m_NumericValue.CompareTo(y.m_NumericValue);
			}

			#endregion Public Methods
		}

		#endregion Private Classes
	}
}