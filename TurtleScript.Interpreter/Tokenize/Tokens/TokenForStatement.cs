#region Namespaces

using System.IO;
using System.Text;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenForStatement : TokenBase
	{
		#region Public Constructors

		public TokenForStatement()
		{
		}

		public TokenForStatement(
			string loopVariableName,
			TokenBase startValue,
			TokenBase endValue,
			TokenBlock block,
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.ForStatement,
				lineNumber,
				charPositionInLine)
		{
			m_LoopVariableName = loopVariableName;
			m_StartValue = startValue;
			m_EndValue = endValue;
			m_Block = block;
		}

		#endregion Public Constructors


		#region Public Properties

		public TokenBlock Block
		{
			get { return m_Block; }
		}

		public TokenBase EndValue
		{
			get { return m_EndValue; }
		}

		public string LoopVariableName
		{
			get { return m_LoopVariableName; }
		}

		public TokenBase StartValue
		{
			get { return m_StartValue; }
		}

		#endregion Public Properties


		#region Public Methods

		/// <summary>
		/// This function is invoked by CompactFormatter when deserializing a
		/// Custom Serializable object.
		/// </summary>
		/// <param name="parent">A reference to the CompactFormatter instance which called this method.</param>
		/// <param name="stream">The Stream where object data must be read</param>
		public override void ReceiveObjectData(
			CompactFormatter.CompactFormatter parent,
			Stream stream)
		{
			base.ReceiveObjectData(
				parent,
				stream);

			int version = (int)parent.Deserialize(stream);

			m_StartValue = (TokenBase)parent.Deserialize(stream);
			m_EndValue = (TokenBase)parent.Deserialize(stream);
			m_LoopVariableName = (string)parent.Deserialize(stream);
			m_Block = (TokenBlock)parent.Deserialize(stream);
		}

		/// <summary>
		/// This function is invoked by CompactFormatter when serializing a 
		/// Custom Serializable object.
		/// </summary>
		/// <param name="parent">A reference to the CompactFormatter instance which called this method.</param>
		/// <param name="stream">The Stream where object data must be written</param>
		public override void SendObjectData(
			CompactFormatter.CompactFormatter parent,
			Stream stream)
		{
			base.SendObjectData(
				parent,
				stream);

			parent.Serialize(stream, VERSION);
			parent.Serialize(stream, m_StartValue);
			parent.Serialize(stream, m_EndValue);
			parent.Serialize(stream, m_LoopVariableName);
			parent.Serialize(stream, m_Block);

		}

		public override string ToTurtleScript(
			TurtleScriptBuilder builder)
		{
			builder.AppendWithIndent($"for {LoopVariableName} = ");

			m_StartValue.ToTurtleScript(builder);

			builder.Append($" to ");

			m_EndValue.ToTurtleScript(builder);

			builder.AppendLine($" do");

			builder.IncrementNestingLevel();

			Block.ToTurtleScript(builder);

			builder.DecrementNestingLevel();

			builder.AppendLine("end");

			return builder.Text;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			TurtleScriptValue startValue = StartValue.Visit(context);
			TurtleScriptValue endValue = EndValue.Visit(context);

			if (!startValue.IsNumeric)
			{
				throw new TurtleScriptExecutionException(string.Format("For loop starting value must be numeric. Line {0}, Column {1}", LineNumber, CharPositionInLine));
			}

			double increment = startValue.NumericValue <= endValue.NumericValue ? 1.0f : -1.0f;

			if (increment == 1)
			{
				for (double index = startValue.NumericValue; index <= endValue.NumericValue; index += increment)
				{
					context.SetVariableValue(
						LoopVariableName,
						VariableType.Variable,
						this,
						index);

					Block.Visit(context);
				}
			}
			else
			{
				for (double index = startValue.NumericValue; index >= endValue.NumericValue; index += increment)
				{
					context.SetVariableValue(
						LoopVariableName,
						VariableType.Variable,
						this,
						index);

					Block.Visit(context);
				}
			}

			return TurtleScriptValue.VOID;

		}

		#endregion Public Methods

		#region Private Constants

		private const int VERSION = 1;

		#endregion Private Constants

		#region Private Fields

		private TokenBlock m_Block;
		private TokenBase m_EndValue;
		private string m_LoopVariableName;
		private TokenBase m_StartValue;

		#endregion Private Fields
	}
}
