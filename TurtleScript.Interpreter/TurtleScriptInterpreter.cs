#region Namespaces

using System;
using System.Collections.Generic;
using Antlr4.Runtime;

#endregion Namespaces

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
			m_Variables = new Dictionary<string, TurtleScriptValue>();
		}

		#endregion Constructor

		#region Public Properties

		public string ErrorMessage
		{
			get
			{
				if (!string.IsNullOrEmpty(m_ErrorMessage))
				{
					return m_ErrorMessage;
				}

				return m_TurtleScriptErrorListener.Message;
			}
		}

		public bool IsError
		{
			get
			{
				return
				(
					(!string.IsNullOrEmpty(m_TurtleScriptErrorListener.Message)) ||
					(!string.IsNullOrEmpty(m_ErrorMessage))
				);
			}
		}

		public Dictionary<string, TurtleScriptValue> Variables
		{
			get { return m_Variables; }
		}

		#endregion Public Properties

		#region Public Methods

		/// <summary>
		/// Executes the script
		/// </summary>
		/// <returns><c>true</c> if execution is successful, otherwise <c>false</c></returns>
		public bool Execute()
		{
			AntlrInputStream input = new AntlrInputStream(m_Script);

			TurtleScriptLexer lexer = new TurtleScriptLexer(input);

			CommonTokenStream tokenStream = new CommonTokenStream(lexer);

			TurtleScriptParser parser = new TurtleScriptParser(tokenStream);

			parser.RemoveErrorListeners();
			m_TurtleScriptErrorListener = new TurtleScriptErrorListener();
			parser.AddErrorListener(m_TurtleScriptErrorListener);

			parser.BuildParseTree = true;

			m_Variables = new Dictionary<string, TurtleScriptValue>();

			try
			{
				TurtleScriptValue turtleScriptValue = Visit(parser.script());
			}
			catch (InvalidOperationException exception)
			{
				m_ErrorMessage = exception.Message;
			}

			return !IsError;
		}

		public override TurtleScriptValue VisitAdditiveExpression(TurtleScriptParser.AdditiveExpressionContext context)
		{
			TurtleScriptValue leftValue = Visit(context.expression(0));
			TurtleScriptValue rightValue = Visit(context.expression(1));

			TurtleScriptValue result;
			if (context.op.Type == TurtleScriptParser.ADD)
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

			TurtleScriptValue result = new TurtleScriptValue(leftValue.BooleanValue && rightValue.BooleanValue);

			return result;
		}

		public override TurtleScriptValue VisitAssignment(TurtleScriptParser.AssignmentContext context)
		{
			TurtleScriptValue value = Visit(context.expression());

			m_Variables[context.Identifier().GetText()] = value;

			return TurtleScriptValue.VOID;
		}

		public override TurtleScriptValue VisitBlock(TurtleScriptParser.BlockContext context)
		{
			foreach (TurtleScriptParser.StatementContext statementContext in context.statement())
			{
				Visit(statementContext);
			}

			if (context.Return() != null)
			{
				TurtleScriptValue turtleScriptValue = Visit(context.expression());
				return turtleScriptValue;
			}

			return TurtleScriptValue.NULL;
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
			float value;

			if (float.TryParse(context.GetText(), out value))
			{
				return new TurtleScriptValue(value);
			}

			throw new InvalidOperationException("");
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
			if (context.op.Type == TurtleScriptParser.MUL)
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

			TurtleScriptValue result = new TurtleScriptValue(leftValue.BooleanValue || rightValue.BooleanValue);

			return result;
		}

		public override TurtleScriptValue VisitParenExpression(TurtleScriptParser.ParenExpressionContext context)
		{
			TurtleScriptValue value = Visit(context.expression());

			return value;
		}

		public override TurtleScriptValue VisitScript(TurtleScriptParser.ScriptContext context)
		{
			return Visit(context.block());
		}

		public override TurtleScriptValue VisitStatement(TurtleScriptParser.StatementContext context)
		{
			return base.VisitStatement(context);
		}

		public override TurtleScriptValue VisitUnaryNegationExpression(TurtleScriptParser.UnaryNegationExpressionContext context)
		{
			TurtleScriptValue value = Visit(context.expression());

			return -value;
		}

		public override TurtleScriptValue VisitUnaryNotExpression(TurtleScriptParser.UnaryNotExpressionContext context)
		{
			TurtleScriptValue value = Visit(context.expression());

			return !value;
		}

		public override TurtleScriptValue VisitVariableReferenceExpression(TurtleScriptParser.VariableReferenceExpressionContext context)
		{
			TurtleScriptValue variableValue;
			if (m_Variables.TryGetValue(
				context.Identifier().GetText(),
				out variableValue))
			{
				return variableValue;
			}

			throw new InvalidOperationException(string.Format("Reference to an unknown variable, '{0}'. Line {1}, Col {2}", context.Identifier().GetText(), context.Start.Line, context.Start.Column));
		}
		
		#endregion Public Methods

		#region Private Fields

		private readonly string m_Script;
		private TurtleScriptErrorListener m_TurtleScriptErrorListener;
		private string m_ErrorMessage;
		private Dictionary<string, TurtleScriptValue> m_Variables;

		#endregion Private Fields
	}
}