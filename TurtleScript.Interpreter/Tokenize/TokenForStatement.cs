using System.Text;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenForStatement : TokenBase
	{
		public TokenForStatement(
			string loopVariableName, 
			TokenBase startValue, 
			TokenBase endValue, 
			TokenBlock block)
			: base(TokenType.ForStatement)
		{
			LoopVariableName = loopVariableName;
			StartValue = startValue;
			EndValue = endValue;
			Block = block;
		}

		public string LoopVariableName { get; } 

		public TokenBase StartValue { get; }

		public TokenBase EndValue { get; }

		public TokenBlock Block { get; }

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
					context.SetVariableValue(LoopVariableName,
						index);

					Block.Visit(context);
				}
			}
			else
			{
				for (double index = startValue.NumericValue; index >= endValue.NumericValue; index += increment)
				{
					context.SetVariableValue(LoopVariableName,
						index);

					Block.Visit(context);
				}
			}

			return TurtleScriptValue.VOID;

		}
	}
}
