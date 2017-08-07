#region Namespaces

using System;
using System.Collections.Generic;

#endregion Namespaces

namespace TurtleScript.Interpreter
{
	public class TurtleScriptValue : IComparable<TurtleScriptValue>
	{
		#region Public Constructors

		public TurtleScriptValue(bool booleanValue)
		{
			m_BooleanValue = booleanValue;
			m_IsBoolean = true;
		}

		public TurtleScriptValue(double numericValue)
		{
			m_NumericValue = numericValue;
			m_IsNumeric = true;
		}

		public TurtleScriptValue()
		{
			m_IsNull = true;
		}

		#endregion Public Constructors

		public static readonly TurtleScriptValue NULL = new TurtleScriptValue();
		public static readonly TurtleScriptValue VOID = new TurtleScriptValue();

		#region Public Properties

		public bool IsBoolean
		{
			get { return m_IsBoolean; }
		}

		public bool IsNull
		{
			get { return m_IsNull; }
		}

		public bool IsNumeric
		{
			get { return m_IsNumeric; }
		}

		public double NumericValue
		{
			get { return m_NumericValue; }
		}

		public bool BooleanValue
		{
			get { return m_BooleanValue; }
		}

		#endregion Public Properties

		#region Equality and Comparison Methods

		protected bool Equals(TurtleScriptValue other)
		{
			return 
				m_BooleanValue == other.m_BooleanValue && 
				m_IsBoolean == other.m_IsBoolean && 
				m_NumericValue.Equals(other.m_NumericValue) && 
				m_IsNumeric == other.m_IsNumeric && 
				m_IsNull == other.m_IsNull;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) 
				return false;

			if (ReferenceEquals(this, obj)) 
				return true;

			if (obj.GetType() != this.GetType()) 
				return false;

			return Equals((TurtleScriptValue) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = m_BooleanValue.GetHashCode();
				hashCode = (hashCode * 397) ^ m_IsBoolean.GetHashCode();
				hashCode = (hashCode * 397) ^ m_NumericValue.GetHashCode();
				hashCode = (hashCode * 397) ^ m_IsNumeric.GetHashCode();
				hashCode = (hashCode * 397) ^ m_IsNull.GetHashCode();
				return hashCode;
			}
		}

		public int CompareTo(TurtleScriptValue other)
		{
			if ((m_IsNumeric != other.IsNumeric) ||
			    (m_IsBoolean != other.IsBoolean))
			{
				throw new InvalidOperationException("Cannot compare mismatched data types.");
			}

			if (m_IsNull || other.IsNull)
			{
				throw new InvalidOperationException("Cannot compare null values");
			}

			if (ReferenceEquals(this,other)) 
				return 0;

			if (ReferenceEquals(null, other)) 
				return 1;

			return m_IsNumeric.CompareTo(other.m_IsNumeric);
		}
		
		#endregion Equality and Comparison Methods

		#region Operator Implementations

		public static TurtleScriptValue operator +(
			TurtleScriptValue result1,
			TurtleScriptValue result2)
		{
			if ((result1.IsNumeric != result2.IsNumeric) ||
			    (result1.IsBoolean != result2.IsBoolean))
			{
				throw new InvalidOperationException("Cannot add mismatched data types.");
			}

			if ((result1.IsBoolean) ||
			    (result2.IsBoolean) ||
			    (result1.IsNull) ||
			    (result2.IsNull))
			{
				throw new InvalidOperationException("Invalid data types for addition operation");
			}

			return new TurtleScriptValue(result1.NumericValue + result2.NumericValue);
		}

		public static TurtleScriptValue operator -(
			TurtleScriptValue result1,
			TurtleScriptValue result2)
		{
			if ((result1.IsNumeric != result2.IsNumeric) ||
			    (result1.IsBoolean != result2.IsBoolean))
			{
				throw new InvalidOperationException("Cannot add mismatched data types.");
			}

			if ((result1.IsBoolean) ||
			    (result2.IsBoolean) ||
			    (result1.IsNull) ||
			    (result2.IsNull))
			{
				throw new InvalidOperationException("Invalid data types for subtraction operation");
			}

			return new TurtleScriptValue(result1.NumericValue - result2.NumericValue);
		}

		public static TurtleScriptValue operator *(
			TurtleScriptValue result1,
			TurtleScriptValue result2)
		{
			if ((result1.IsNumeric != result2.IsNumeric) ||
			    (result1.IsBoolean != result2.IsBoolean))
			{
				throw new InvalidOperationException("Cannot add mismatched data types.");
			}

			if ((result1.IsBoolean) ||
			    (result2.IsBoolean) ||
			    (result1.IsNull) ||
			    (result2.IsNull))
			{
				throw new InvalidOperationException("Invalid data types for multiplication operation");
			}

			return new TurtleScriptValue(result1.NumericValue * result2.NumericValue);
		}

		public static TurtleScriptValue operator /(
			TurtleScriptValue result1,
			TurtleScriptValue result2)
		{
			if ((result1.IsNumeric != result2.IsNumeric) ||
			    (result1.IsBoolean != result2.IsBoolean))
			{
				throw new InvalidOperationException("Cannot add mismatched data types.");
			}

			if ((result1.IsBoolean) ||
			    (result2.IsBoolean) ||
			    (result1.IsNull) ||
			    (result2.IsNull))
			{
				throw new InvalidOperationException("Invalid data types for division operation");
			}

			if (result2.NumericValue == 0)
			{
				return new TurtleScriptValue(0);
			}

			return new TurtleScriptValue(result1.NumericValue / result2.NumericValue);
		}

		public static TurtleScriptValue operator !(TurtleScriptValue result)
		{
			if (!result.IsBoolean)
			{
				throw new InvalidOperationException("Invalid data type for Not operation");
			}

			return new TurtleScriptValue(!result.BooleanValue);
		}

		public static TurtleScriptValue operator -(TurtleScriptValue result)
		{
			if (!result.IsNumeric)
			{
				throw new InvalidOperationException("Invalid data type for Negation operation");
			}

			return new TurtleScriptValue(result.NumericValue * -1);
		}


		#endregion Operator Implementations

		#region Private Fields

		private readonly bool m_BooleanValue;
		private readonly bool m_IsBoolean;
		private readonly double m_NumericValue;
		private readonly bool m_IsNumeric;
		private readonly bool m_IsNull;

		#endregion Private Fields
	}
}