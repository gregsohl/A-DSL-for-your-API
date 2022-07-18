#region Namespaces

using System;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter
{
	/// <summary>
	/// Represents a value within the execution of a TurtleScript script. 
	/// </summary>
	/// <seealso cref="System.IComparable&lt;TurtleScript.Interpreter.TurtleScriptValue&gt;" />
	public class TurtleScriptValue : IComparable<TurtleScriptValue>
	{
		#region Public Constructors

		public TurtleScriptValue(bool booleanValue)
		{
			m_BooleanValue = booleanValue;
			m_IsBoolean = true;
			m_ValueType = TurtleScriptValueType.Boolean;
		}

		public TurtleScriptValue(double numericValue)
		{
			m_NumericValue = numericValue;
			m_IsNumeric = true;
			m_ValueType = TurtleScriptValueType.Numeric;
		}

		public TurtleScriptValue()
		{
			m_IsNull = true;
			m_ValueType = TurtleScriptValueType.Null;
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

		public TurtleScriptValueType ValueType
		{
			get
			{
				return m_ValueType;
			}
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
				throw new TurtleScriptExecutionException("Cannot compare mismatched data types.");
			}

			if (m_IsNull || other.IsNull)
			{
				throw new TurtleScriptExecutionException("Cannot compare null values");
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
			TurtleScriptValue left,
			TurtleScriptValue right)
		{
			if (left.ValueType != right.ValueType)
			{
				throw new TurtleScriptExecutionException("Mismatched data types for Addition operation");
			}

			if ((left.IsBoolean) ||
			    (right.IsBoolean) ||
			    (left.IsNull) ||
			    (right.IsNull))
			{
				throw new TurtleScriptExecutionException("Invalid data types for Addition operation");
			}

			return new TurtleScriptValue(left.NumericValue + right.NumericValue);
		}

		public static TurtleScriptValue operator -(
			TurtleScriptValue left,
			TurtleScriptValue right)
		{
			if (left.ValueType != right.ValueType)
			{
				throw new TurtleScriptExecutionException("Mismatched data types for Subtraction operation");
			}

			if ((left.IsBoolean) ||
			    (right.IsBoolean) ||
			    (left.IsNull) ||
			    (right.IsNull))
			{
				throw new TurtleScriptExecutionException("Invalid data types for Subtraction operation");
			}

			return new TurtleScriptValue(left.NumericValue - right.NumericValue);
		}

		public static TurtleScriptValue operator *(
			TurtleScriptValue left,
			TurtleScriptValue right)
		{
			if (left.ValueType != right.ValueType)
			{
				throw new TurtleScriptExecutionException("Mismatched data types for Multiplication operation");
			}

			if ((left.IsBoolean) ||
			    (right.IsBoolean) ||
			    (left.IsNull) ||
			    (right.IsNull))
			{
				throw new TurtleScriptExecutionException("Invalid data types for Multiplication operation");
			}

			return new TurtleScriptValue(left.NumericValue * right.NumericValue);
		}

		public static TurtleScriptValue operator /(
			TurtleScriptValue left,
			TurtleScriptValue right)
		{
			if (left.ValueType != right.ValueType)
			{
				throw new TurtleScriptExecutionException("Mismatched data types for Division operation.");
			}

			if ((left.IsBoolean) ||
			    (right.IsBoolean) ||
			    (left.IsNull) ||
			    (right.IsNull))
			{
				throw new TurtleScriptExecutionException("Invalid data types for Division operation");
			}

			if (right.NumericValue == 0)
			{
				return new TurtleScriptValue(0);
			}

			return new TurtleScriptValue(left.NumericValue / right.NumericValue);
		}

		public static TurtleScriptValue operator %(
			TurtleScriptValue left,
			TurtleScriptValue right)
		{
			if (left.ValueType != right.ValueType)
			{
				throw new TurtleScriptExecutionException("Mismatched data types for Modulus operation");
			}

			if (!left.IsNumeric)
			{
				throw new TurtleScriptExecutionException("Invalid data types for Modulus operation");
			}

			if (right.NumericValue == 0)
			{
				return new TurtleScriptValue(0);
			}

			return new TurtleScriptValue(left.NumericValue % right.NumericValue);
		}

		public static TurtleScriptValue operator !(TurtleScriptValue value)
		{
			if (!value.IsBoolean)
			{
				throw new TurtleScriptExecutionException("Invalid data type for Not operation");
			}

			return new TurtleScriptValue(!value.BooleanValue);
		}

		public static TurtleScriptValue operator -(TurtleScriptValue value)
		{
			if (!value.IsNumeric)
			{
				throw new TurtleScriptExecutionException("Invalid data type for Negation operation");
			}

			return new TurtleScriptValue(value.NumericValue * -1);
		}

		public static TurtleScriptValue operator ==(TurtleScriptValue left, TurtleScriptValue right)
		{
			return new TurtleScriptValue(left.Equals(right));
		}

		public static TurtleScriptValue operator !=(TurtleScriptValue left, TurtleScriptValue right)
		{
			return new TurtleScriptValue(!left.Equals(right));
		}


		public static TurtleScriptValue operator >(TurtleScriptValue left, TurtleScriptValue right)
		{
			if (left.ValueType != right.ValueType)
			{
				throw new TurtleScriptExecutionException("Mismatched data types for Greater Than operation");
			}

			if (!left.IsNumeric)
			{
				throw new TurtleScriptExecutionException("Invalid data type for Greater Than operation");
			}

			return new TurtleScriptValue(left.NumericValue > right.NumericValue);
		}

		public static TurtleScriptValue operator <(TurtleScriptValue left, TurtleScriptValue right)
		{
			if (left.ValueType != right.ValueType)
			{
				throw new TurtleScriptExecutionException("Mismatched data types for Less Than operation");
			}

			if (!left.IsNumeric)
			{
				throw new TurtleScriptExecutionException("Invalid data type for Less Than operation");
			}

			return new TurtleScriptValue(left.NumericValue < right.NumericValue);
		}

		public static TurtleScriptValue operator >=(TurtleScriptValue left, TurtleScriptValue right)
		{
			if (left.ValueType != right.ValueType)
			{
				throw new TurtleScriptExecutionException("Mismatched data types for Greater Than or Equal operation");
			}

			if (!left.IsNumeric)
			{
				throw new TurtleScriptExecutionException("Invalid data type for Greater Than or Equal operation");
			}

			return new TurtleScriptValue(left.NumericValue >= right.NumericValue);
		}

		public static TurtleScriptValue operator <=(TurtleScriptValue left, TurtleScriptValue right)
		{
			if (left.ValueType != right.ValueType)
			{
				throw new TurtleScriptExecutionException("Mismatched data types for Less Than or Equal operation");
			}

			if (!left.IsNumeric)
			{
				throw new TurtleScriptExecutionException("Invalid data type for Less Than or Equal operation");
			}

			return new TurtleScriptValue(left.NumericValue <= right.NumericValue);
		}

		public static TurtleScriptValue operator &(TurtleScriptValue left, TurtleScriptValue right)
		{
			if (left.ValueType != right.ValueType)
			{
				throw new TurtleScriptExecutionException("Mismatched data types for Logical AND operation");
			}

			if (!left.IsBoolean)
			{
				throw new TurtleScriptExecutionException("Invalid data type for Logical AND operation");
			}

			return new TurtleScriptValue(left.BooleanValue && right.BooleanValue);
		}

		public static TurtleScriptValue operator |(TurtleScriptValue left, TurtleScriptValue right)
		{
			if (left.ValueType != right.ValueType)
			{
				throw new TurtleScriptExecutionException("Mismatched data types for Logical AND operation");
			}

			if (!left.IsBoolean)
			{
				throw new TurtleScriptExecutionException("Invalid data type for Logical AND operation");
			}

			return new TurtleScriptValue(left.BooleanValue || right.BooleanValue);
		}


		#endregion Operator Implementations

		#region Private Fields

		private readonly bool m_BooleanValue;
		private readonly bool m_IsBoolean;
		private readonly double m_NumericValue;
		private readonly bool m_IsNumeric;
		private readonly bool m_IsNull;
		private readonly TurtleScriptValueType m_ValueType;

		#endregion Private Fields


	}
}