using System.Text;
using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenForStatement : TokenBase
	{
		private readonly string m_LoopVariableName;
		private readonly TokenBase m_StartValue;
		private readonly TokenBase m_EndValue;
		private readonly TokenBlock m_Block;

		public TokenForStatement(
			string loopVariableName, 
			TokenBase startValue, 
			TokenBase endValue, 
			TokenBlock block)
			: base(TokenType.ForStatement)
		{
			m_LoopVariableName = loopVariableName;
			m_StartValue = startValue;
			m_EndValue = endValue;
			m_Block = block;
		}

		public string LoopVariableName
		{
			get { return m_LoopVariableName; }
		}

		public TokenBase StartValue
		{
			get { return m_StartValue; }
		}

		public TokenBase EndValue
		{
			get { return m_EndValue; }
		}

		public TokenBlock Block
		{
			get { return m_Block; }
		}

		public override string ToTurtleScript()
		{
			StringBuilder turtleScript = new StringBuilder();

			turtleScript
				.AppendLine($"for {LoopVariableName} = {StartValue.ToTurtleScript()} to {EndValue.ToTurtleScript()} Do");

			turtleScript.AppendLine(Block.ToTurtleScript());

			return turtleScript.ToString();
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			TurtleScriptValue startValue = StartValue.Visit(context);
			TurtleScriptValue endValue = EndValue.Visit(context);

			//if (!startValue.IsNumeric)
			//{
			//	throw new InvalidOperationException(string.Format("For loop starting value must be numeric. Line {0}, Column {1}", context.Start.Line, context.Start.Column));
			//}

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
	}
}
