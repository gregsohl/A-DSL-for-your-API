#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

#endregion Namespaces

namespace TurtleScript.Interpreter
{
	public class TurtleScriptInterpreter
		: TurtleScriptBaseVisitor<TurtleScriptValue>
	{
		#region Constructor
		public TurtleScriptInterpreter(
			string script, 
			List<ITurtleScriptRuntime> runtimeLibraries = null)
		{
			m_Script = script;

			if (runtimeLibraries != null)
			{
				m_RuntimeLibraries = runtimeLibraries;
			}
			else
			{
				// Create an empty list
				m_RuntimeLibraries = new List<ITurtleScriptRuntime>();
			}

			m_TurtleScriptErrorListener = new TurtleScriptErrorListener();
			m_Variables = new Dictionary<string, TurtleScriptValue>();
			m_ScriptFunctions = new Dictionary<string, TurtleScriptFunction>();
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
			m_ScriptFunctions = new Dictionary<string, TurtleScriptFunction>();

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

			SetVariableValue(context.Identifier().GetText(), value);

			return TurtleScriptValue.VOID;
		}

		public override TurtleScriptValue VisitBlock(TurtleScriptParser.BlockContext context)
		{
			foreach (TurtleScriptParser.FunctionDeclContext functionDeclContext in context.functionDecl())
			{
				Visit(functionDeclContext);
			}

			foreach (TurtleScriptParser.StatementContext statementContext in context.statement())
			{
				Visit(statementContext);
			}

			if (context.Return() != null)
			{
				TurtleScriptValue turtleScriptValue = Visit(context.expression());
				return turtleScriptValue;
			}

			return TurtleScriptValue.VOID;
		}

		public override TurtleScriptValue VisitCompareExpression(TurtleScriptParser.CompareExpressionContext context)
		{
			TurtleScriptValue leftValue = Visit(context.expression(0));
			TurtleScriptValue rightValue = Visit(context.expression(1));

			TurtleScriptValue result;

			switch (context.op.Type)
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

		public override TurtleScriptValue VisitFloatExpression(TurtleScriptParser.FloatExpressionContext context)
		{
			double value;

			if (double.TryParse(context.GetText(), out value))
			{
				return new TurtleScriptValue(value);
			}

			throw new InvalidOperationException("");
		}

		public override TurtleScriptValue VisitForStatement(TurtleScriptParser.ForStatementContext context)
		{
			string loopVariableName = context.Identifier().GetText();

			TurtleScriptValue startValue = Visit(context.expression(0));
			TurtleScriptValue endValue = Visit(context.expression(1));

			if (!startValue.IsNumeric)
			{
				throw new InvalidOperationException(string.Format("For loop starting value must be numeric. Line {0}, Column {1}", context.Start.Line, context.Start.Column));
			}

			double increment = startValue.NumericValue <= endValue.NumericValue ? 1.0f : -1.0f;

			if (increment == 1)
			{
				for (double index = startValue.NumericValue; index <= endValue.NumericValue; index += increment)
				{
					SetVariableValue(loopVariableName,
						index);

					Visit(context.block());
				}
			}
			else
			{
				for (double index = startValue.NumericValue; index >= endValue.NumericValue; index += increment)
				{
					SetVariableValue(loopVariableName,
						index);

					Visit(context.block());
				}
			}

			return TurtleScriptValue.VOID;
		}

		public override TurtleScriptValue VisitFunctionCall(TurtleScriptParser.FunctionCallContext context)
		{
			TurtleScriptFunction function;

			if ((context.Identifier() != null) &&
				(TryGetFunction(context, out function)))
			{
				TurtleScriptParser.ExpressionContext[] parameterExpressions = new TurtleScriptParser.ExpressionContext[0];

				if (context.expressionList() != null)
				{
					parameterExpressions = context.expressionList().expression();

					if (parameterExpressions.Length != function.Parameters.Count)
					{
						throw new InvalidOperationException(string.Format("Invalid number of parameters specified for function call at Line {0}, Column {1}", context.Start.Line, context.Start.Column));
					}
				}

				for(int parameterIndex = 0; parameterIndex < function.Parameters.Count; parameterIndex++)
				{
					string parameterName = function.Parameters[parameterIndex].GetText();
					List<TurtleScriptParser.ExpressionContext> parameterContexts = parameterExpressions.ToList();
					TurtleScriptValue parameterValue = Visit(parameterContexts[parameterIndex]);

					SetVariableValue(parameterName, parameterValue);
				}

				TurtleScriptValue returnValue = Visit(function.Body);

				return returnValue;
			}

			if (context.QualifiedIdentifier() != null)
			{
				string fullIdentifier = context.QualifiedIdentifier().GetText();
				string[] identifierParts = fullIdentifier.Split('.');

				string runtimeName = identifierParts[0];
				string functionName = identifierParts[1];

				ITurtleScriptRuntime runtime = GetRuntimeLibrary(context, runtimeName);
				TurtleScriptValue returnValue = CallRuntimeFunction(context, runtime, functionName);

				return returnValue;
			}

			throw new InvalidOperationException(string.Format("Invalid identifier. Function name not previously defined. Line {0}, Column {1}", context.Start.Line, context.Start.Column));
		}

		private bool TryGetFunction(
			TurtleScriptParser.FunctionCallContext context,
			out TurtleScriptFunction function)
		{
			function = null;
			var functionCallName = context.Identifier().GetText();

			int parameterCount = 0;
			if (context.expressionList() != null)
			{
				parameterCount = context.expressionList().expression().Length;
			}

			functionCallName += "_" + parameterCount;

			foreach (var functionToTest in m_ScriptFunctions)
			{
				if ((functionToTest.Key == functionCallName) &&
					(functionToTest.Value.Parameters.Count == parameterCount))
				{
					function = functionToTest.Value;
					return true;
				}
			}

			return false;
		}

		private TurtleScriptValue CallRuntimeFunction(
			TurtleScriptParser.FunctionCallContext context,
			ITurtleScriptRuntime runtime,
			string functionName)
		{
			TurtleScriptRuntimeFunction function;
			if (TryGetRuntimeFunction(context, runtime, functionName, out function))
			{
				TurtleScriptParser.ExpressionContext[] parameterExpressions = new TurtleScriptParser.ExpressionContext[0];

				if (context.expressionList() != null)
				{
					parameterExpressions = context.expressionList().expression();
				}

				if (parameterExpressions.Length != function.ParameterCount)
				{
					throw new InvalidOperationException(string.Format("Invalid number of parameters specified for function call at Line {0}, Column {1}", context.Start.Line, context.Start.Column));
				}

				List<TurtleScriptValue> functionParameters = new List<TurtleScriptValue>();

				for (int parameterIndex = 0; parameterIndex < parameterExpressions.Length; parameterIndex++)
				{
					List<TurtleScriptParser.ExpressionContext> parameterContexts = parameterExpressions.ToList();
					TurtleScriptValue parameterValue = Visit(parameterContexts[parameterIndex]);

					functionParameters.Add(parameterValue);
				}

				TurtleScriptValue returnValue = function.Function(functionParameters);

				return returnValue;
			}

			throw new InvalidOperationException(string.Format("Invalid function name in runtime. Line {0}, Column {1}", context.Start.Line, context.Start.Column));
		}

		private static bool TryGetRuntimeFunction(
			TurtleScriptParser.FunctionCallContext context,
			ITurtleScriptRuntime runtime,
			string functionName,
			out TurtleScriptRuntimeFunction function)
		{
			int parameterCount = 0;
			if (context.expressionList() != null)
			{
				parameterCount = context.expressionList().expression().Length;
			}

			functionName += "_" + parameterCount;

			return runtime.Functions.TryGetValue(functionName, out function);
		}

		private ITurtleScriptRuntime GetRuntimeLibrary(TurtleScriptParser.FunctionCallContext context,
		                                               string runtimeName)
		{
			foreach (ITurtleScriptRuntime turtleScriptRuntime in m_RuntimeLibraries)
			{
				if (turtleScriptRuntime.Namespace == runtimeName)
				{
					return turtleScriptRuntime;
				}
			}

			throw new InvalidOperationException(string.Format("Invalid runtime library name specified on function call. Line {0}, Column {1}", context.Start.Line, context.Start.Column));
		}

		public override TurtleScriptValue VisitFunctionCallExpression(TurtleScriptParser.FunctionCallExpressionContext context)
		{
			TurtleScriptValue returnValue = Visit(context.functionCall());

			return returnValue;
		}

		public override TurtleScriptValue VisitFunctionDecl(TurtleScriptParser.FunctionDeclContext context)
		{
			TurtleScriptParser.FormalParametersContext formalParametersContext = context.formalParameters();
			IParseTree[] formalParameterContexts;

			if (formalParametersContext != null)
			{
				formalParameterContexts = formalParametersContext.formalParameter();
			}
			else
			{
				formalParameterContexts = new IParseTree[0];
			}

			IParseTree block = context.block();

			string functionName = context.Identifier().GetText();

			// Check for function that exists by the same name
			TurtleScriptFunction existingFunction;
			if (m_ScriptFunctions.TryGetValue(context.Identifier().GetText(), out existingFunction))
			{
				throw new InvalidOperationException(string.Format("A function with the name '{0}' already exists. Line {1}, Column {2}", context.Identifier().GetText(), context.Start.Line, context.Start.Column));
			}

			m_ScriptFunctions.Add(functionName, new TurtleScriptFunction(functionName, formalParameterContexts, block));

			return TurtleScriptValue.VOID;
		}

		public override TurtleScriptValue VisitIfStatement(TurtleScriptParser.IfStatementContext context)
		{
			TurtleScriptValue ifResult = Visit(context.ifStat().expression());

			if ((ifResult.IsBoolean) &&
				(ifResult.BooleanValue))
			{
				Visit(context.ifStat().block());
			}
			else
			{
				foreach (TurtleScriptParser.ElseIfStatContext elseIfStatContext in context.elseIfStat())
				{
					ifResult = Visit(elseIfStatContext.expression());

					if ((ifResult.IsBoolean) &&
					    (ifResult.BooleanValue))
					{
						Visit(elseIfStatContext.block());
						return TurtleScriptValue.VOID;
					}
				}

				if (context.elseStat() != null)
				{
					Visit(context.elseStat().block());
				}
			}

			return TurtleScriptValue.VOID;
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

			TurtleScriptValue result = null;
			switch (context.op.Type)
			{
				case TurtleScriptParser.MUL:
					result = new TurtleScriptValue(leftValue.NumericValue * rightValue.NumericValue);
					break;
				case TurtleScriptParser.DIV:
					if (rightValue.NumericValue == 0)
					{
						result = new TurtleScriptValue(0);
					}
					else
					{
						result = new TurtleScriptValue(leftValue.NumericValue / rightValue.NumericValue);
					}
					break;
				case TurtleScriptParser.MOD:
					result = new TurtleScriptValue(leftValue.NumericValue % rightValue.NumericValue);
					break;
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

		public override TurtleScriptValue VisitPiExpression(
			TurtleScriptParser.PiExpressionContext context)
		{
			return new TurtleScriptValue(3.141592654);
		}

		#endregion Public Methods

		#region Private Fields

		private readonly string m_Script;
		private readonly List<ITurtleScriptRuntime> m_RuntimeLibraries;
		private TurtleScriptErrorListener m_TurtleScriptErrorListener;
		private string m_ErrorMessage;
		private Dictionary<string, TurtleScriptValue> m_Variables;
		private Dictionary<string, TurtleScriptFunction> m_ScriptFunctions;

		#endregion Private Fields

		#region Private Methods

		private void SetVariableValue(
			string variableName,
			TurtleScriptValue variableValue)
		{
			m_Variables[variableName] = variableValue;
		}

		private void SetVariableValue(
			string variableName,
			double variableValue)
		{
			m_Variables[variableName] = new TurtleScriptValue(variableValue);
		}

		#endregion Private Methods

	}
}