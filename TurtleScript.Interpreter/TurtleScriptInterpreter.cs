using System;
using Antlr4.Runtime;

namespace TurtleScript.Interpreter
{
	public class TurtleScriptInterpreter
		: TurtleScriptBaseVisitor<TurtleScriptValue>
	{
		#region Constructor

		public TurtleScriptInterpreter(string script)
		{
			m_Script = script;
			m_TurtleScriptErrorListener = new TurtleScriptErrorListener();
		}

		#endregion Constructor

		#region Public Properties

		public string ErrorMessage
		{
			get { return m_TurtleScriptErrorListener.Message; }
		}

		public bool IsError
		{
			get { return !string.IsNullOrEmpty(m_TurtleScriptErrorListener.Message); }
		}
		#endregion Public Properties

		#region Public Properties

		public void Execute()
		{
			AntlrInputStream input = new AntlrInputStream(m_Script);

			TurtleScriptLexer lexer = new TurtleScriptLexer(input);

			CommonTokenStream tokenStream = new CommonTokenStream(lexer);

			TurtleScriptParser parser = new TurtleScriptParser(tokenStream);

			parser.RemoveErrorListeners();
			m_TurtleScriptErrorListener = new TurtleScriptErrorListener();
			parser.AddErrorListener(m_TurtleScriptErrorListener);

			parser.BuildParseTree = true;

			Visit(parser.script());
		}

		public override TurtleScriptValue VisitAdditiveExpression(TurtleScriptParser.AdditiveExpressionContext context)
		{
			TurtleScriptValue leftValue = Visit(context.expression(0));
			TurtleScriptValue rightValue = Visit(context.expression(1));

			TurtleScriptValue result;
			if (context.op.TokenIndex == TurtleScriptParser.ADD)
			{
				result = new TurtleScriptValue(leftValue.NumericValue + rightValue.NumericValue);
			}
			else
			{
				result = new TurtleScriptValue(leftValue.NumericValue - rightValue.NumericValue);
			}

			return result;
		}

		public override TurtleScriptValue VisitAndExpression(TurtleScriptParser.AndExpressionContext context)
		{
			TurtleScriptValue leftValue = Visit(context.expression(0));
			TurtleScriptValue rightValue = Visit(context.expression(1));

			TurtleScriptValue result = new TurtleScriptValue(leftValue.BoolValue && rightValue.BoolValue);

			return result;
		}

		public override TurtleScriptValue VisitAssignment(TurtleScriptParser.AssignmentContext context)
		{
			return base.VisitAssignment(context);
		}

		public override TurtleScriptValue VisitBlock(TurtleScriptParser.BlockContext context)
		{
			return base.VisitBlock(context);
		}

		public override TurtleScriptValue VisitCompareExpression(TurtleScriptParser.CompareExpressionContext context)
		{
			TurtleScriptValue leftValue = Visit(context.expression(0));
			TurtleScriptValue rightValue = Visit(context.expression(1));

			TurtleScriptValue result;

			switch (context.op.TokenIndex)
			{
				case TurtleScriptParser.EQ:
					result = new TurtleScriptValue(leftValue.NumericValue == rightValue.NumericValue);
					break;
				case TurtleScriptParser.NE:
					result = new TurtleScriptValue(leftValue.NumericValue != rightValue.NumericValue);
					break;
				case TurtleScriptParser.GT:
					result = new TurtleScriptValue(leftValue.NumericValue > rightValue.NumericValue);
					break;
				case TurtleScriptParser.LT:
					result = new TurtleScriptValue(leftValue.NumericValue < rightValue.NumericValue);
					break;
				case TurtleScriptParser.GE:
					result = new TurtleScriptValue(leftValue.NumericValue >= rightValue.NumericValue);
					break;
				case TurtleScriptParser.LE:
					result = new TurtleScriptValue(leftValue.NumericValue <= rightValue.NumericValue);
					break;
				default:
					result = new TurtleScriptValue(false);
					break;
			}

			return result;
		}

		public override TurtleScriptValue VisitElseIfStat(TurtleScriptParser.ElseIfStatContext context)
		{
			return base.VisitElseIfStat(context);
		}

		public override TurtleScriptValue VisitElseStat(TurtleScriptParser.ElseStatContext context)
		{
			return base.VisitElseStat(context);
		}

		public override TurtleScriptValue VisitExpression(TurtleScriptParser.ExpressionContext context)
		{
			return base.VisitExpression(context);
		}

		public override TurtleScriptValue VisitExpressionList(TurtleScriptParser.ExpressionListContext context)
		{
			return base.VisitExpressionList(context);
		}

		public override TurtleScriptValue VisitFloatExpression(TurtleScriptParser.FloatExpressionContext context)
		{
			return base.VisitFloatExpression(context);
		}

		public override TurtleScriptValue VisitFormalParameter(TurtleScriptParser.FormalParameterContext context)
		{
			return base.VisitFormalParameter(context);
		}

		public override TurtleScriptValue VisitFormalParameters(TurtleScriptParser.FormalParametersContext context)
		{
			return base.VisitFormalParameters(context);
		}

		public override TurtleScriptValue VisitForStatement(TurtleScriptParser.ForStatementContext context)
		{
			return base.VisitForStatement(context);
		}

		public override TurtleScriptValue VisitFunctionCall(TurtleScriptParser.FunctionCallContext context)
		{
			return base.VisitFunctionCall(context);
		}

		public override TurtleScriptValue VisitFunctionCallExpression(TurtleScriptParser.FunctionCallExpressionContext context)
		{
			return base.VisitFunctionCallExpression(context);
		}

		public override TurtleScriptValue VisitFunctionDecl(TurtleScriptParser.FunctionDeclContext context)
		{
			return base.VisitFunctionDecl(context);
		}

		public override TurtleScriptValue VisitIfStat(TurtleScriptParser.IfStatContext context)
		{
			return base.VisitIfStat(context);
		}

		public override TurtleScriptValue VisitIfStatement(TurtleScriptParser.IfStatementContext context)
		{
			return base.VisitIfStatement(context);
		}

		public override TurtleScriptValue VisitIntExpression(TurtleScriptParser.IntExpressionContext context)
		{
			int value;
			if (Int32.TryParse(context.GetText(), out value))
			{
				return new TurtleScriptValue(value);
			}

			throw new InvalidOperationException("");
		}

		public override TurtleScriptValue VisitMultiplicativeOpExpression(TurtleScriptParser.MultiplicativeOpExpressionContext context)
		{
			TurtleScriptValue leftValue = Visit(context.expression(0));
			TurtleScriptValue rightValue = Visit(context.expression(1));

			TurtleScriptValue result;
			if (context.op.TokenIndex == TurtleScriptParser.MUL)
			{
				result = new TurtleScriptValue(leftValue.NumericValue * rightValue.NumericValue);
			}
			else
			{
				if (rightValue.NumericValue == 0)
				{
					result = new TurtleScriptValue(0);
				}
				else
				{
					result = new TurtleScriptValue(leftValue.NumericValue / rightValue.NumericValue);
				}
			}

			return result;
		}

		public override TurtleScriptValue VisitOrExpression(TurtleScriptParser.OrExpressionContext context)
		{
			TurtleScriptValue leftValue = Visit(context.expression(0));
			TurtleScriptValue rightValue = Visit(context.expression(1));

			TurtleScriptValue result = new TurtleScriptValue(leftValue.BoolValue || rightValue.BoolValue);

			return result;
		}

		public override TurtleScriptValue VisitParenExpression(TurtleScriptParser.ParenExpressionContext context)
		{
			return base.VisitParenExpression(context);
		}

		public override TurtleScriptValue VisitScript(TurtleScriptParser.ScriptContext context)
		{
			return base.VisitScript(context);
		}

		public override TurtleScriptValue VisitStatement(TurtleScriptParser.StatementContext context)
		{
			return base.VisitStatement(context);
		}

		public override TurtleScriptValue VisitUnaryNegationExpression(TurtleScriptParser.UnaryNegationExpressionContext context)
		{
			return base.VisitUnaryNegationExpression(context);
		}

		public override TurtleScriptValue VisitUnaryNotExpression(TurtleScriptParser.UnaryNotExpressionContext context)
		{
			return base.VisitUnaryNotExpression(context);
		}

		public override TurtleScriptValue VisitVariableReferenceExpression(TurtleScriptParser.VariableReferenceExpressionContext context)
		{
			return base.VisitVariableReferenceExpression(context);
		}
		#endregion Public Properties

		#region Private Fields

		private readonly string m_Script;
		private TurtleScriptErrorListener m_TurtleScriptErrorListener;

		#endregion Private Fields
	}
}