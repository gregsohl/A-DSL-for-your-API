
using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

using TurtleScript.Interpreter.Tokenize;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TurtleScriptTokenizer
		: TurtleScriptBaseVisitor<TokenBase>
	{


		#region Public Constructors

		public TurtleScriptTokenizer(
			string script, 
			List<ITurtleScriptRuntime> runtimeLibraries = null)
		{
			m_Script = script;
			new List<TokenBase>();

			m_RuntimeLibraries = runtimeLibraries ?? new List<ITurtleScriptRuntime>();

			m_TurtleScriptErrorListener = new TurtleScriptErrorListener();
			m_VariablesDeclared = new Dictionary<string, TokenBase>();
			m_ScriptFunctions = new Dictionary<string, TurtleScriptFunction>();
		}

		#endregion Public Constructors


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

		public Dictionary<string, TokenBase> VariablesDeclared
		{
			get { return m_VariablesDeclared; }
		}

		#endregion Public Properties


		#region Public Methods

		public void Execute(TokenBase script, TurtleScriptExecutionContext context)
		{
			script.Visit(context);

		}

		/// <summary>
		/// Executes the script
		/// </summary>
		/// <returns><c>true</c> if execution is successful, otherwise <c>false</c></returns>
		public bool Parse(out TokenBase rootToken)
		{
			rootToken = null;

			AntlrInputStream input = new AntlrInputStream(m_Script);

			TurtleScriptLexer lexer = new TurtleScriptLexer(input);

			CommonTokenStream tokenStream = new CommonTokenStream(lexer);

			m_Parser = new TurtleScriptParser(tokenStream);

			m_Parser.RemoveErrorListeners();
			m_TurtleScriptErrorListener = new TurtleScriptErrorListener();
			m_Parser.AddErrorListener(m_TurtleScriptErrorListener);

			m_Parser.BuildParseTree = true;

			m_VariablesDeclared = new Dictionary<string, TokenBase>();
			m_ScriptFunctions = new Dictionary<string, TurtleScriptFunction>();

			try
			{
				rootToken = Visit(m_Parser.script());
				m_ErrorMessage = m_TurtleScriptErrorListener.Message;
			}
			catch (InvalidOperationException exception)
			{
				m_ErrorMessage = exception.Message;
			}

			return !IsError;
		}
		public override TokenBase VisitAdditiveExpression(TurtleScriptParser.AdditiveExpressionContext context)
		{
			TokenBase leftValue = Visit(context.expression(0));
			TokenBase rightValue = Visit(context.expression(1));

			TokenBase result = new TokenBinaryOperator(
				context.op.Type == TurtleScriptParser.Add ? TokenType.OpAdd : TokenType.OpSubtract);

			result.AddChild(leftValue);
			result.AddChild(rightValue);

			return result;
		}

		/// <summary>
		/// Visit a parse tree produced by the <c>andExpression</c>
		/// labeled alternative in <see cref="TurtleScriptParser.expression"/>.
		/// <para>
		/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
		/// on <paramref name="context"/>.
		/// </para>
		/// </summary>
		/// <param name="context">The parse tree.</param>
		/// <return>The visitor result.</return>
		public override TokenBase VisitAndExpression(TurtleScriptParser.AndExpressionContext context)
		{
			TokenBase leftValue = Visit(context.expression(0));
			TokenBase rightValue = Visit(context.expression(1));

			TokenBase result = new TokenBinaryOperator(TokenType.OpConditionalAnd);

			result.AddChild(leftValue);
			result.AddChild(rightValue);

			return result;
		}

		public override TokenBase VisitAssignment(TurtleScriptParser.AssignmentContext context)
		{
			TokenBase value = Visit(context.expression());

			var variableName = context.Identifier().GetText();
			TokenBase result = new TokenAssignment(variableName);
			result.AddChild(value);

			DeclareVariable(variableName, result);

			return result;
		}

		public override TokenBase VisitBlock(TurtleScriptParser.BlockContext context)
		{
			TokenBase blockToken = new TokenBlock();

			foreach (TurtleScriptParser.FunctionDeclContext functionDeclContext in context.functionDecl())
			{
				Visit(functionDeclContext);
			}

			foreach (TurtleScriptParser.StatementContext statementContext in context.statement())
			{
				blockToken.AddChild(Visit(statementContext));
			}

			return blockToken;
		}

		public override TokenBase VisitCompareExpression(TurtleScriptParser.CompareExpressionContext context)
		{
			TokenBase leftValue = Visit(context.expression(0));
			TokenBase rightValue = Visit(context.expression(1));

			TokenBase operatorToken;


			switch (context.op.Type)
			{
				case TurtleScriptParser.EQ:
					operatorToken = new TokenBinaryOperator(TokenType.OpEqual);
					break;
				case TurtleScriptParser.NE:
					operatorToken = new TokenBinaryOperator(TokenType.OpNotEqual);
					break;
				case TurtleScriptParser.GT:
					operatorToken = new TokenBinaryOperator(TokenType.OpGreaterThan);
					break;
				case TurtleScriptParser.LT:
					operatorToken = new TokenBinaryOperator(TokenType.OpLessThan);
					break;
				case TurtleScriptParser.GE:
					operatorToken = new TokenBinaryOperator(TokenType.OpGreaterThanOrEqual);
					break;
				case TurtleScriptParser.LE:
					operatorToken = new TokenBinaryOperator(TokenType.OpLessThanOrEqual);
					break;
				default:
					operatorToken = new TokenBinaryOperator(TokenType.OpEqual);
					break;
			}

			operatorToken.AddChild(leftValue);
			operatorToken.AddChild(rightValue);

			return operatorToken;
		}

		public override TokenBase VisitFloatExpression(TurtleScriptParser.FloatExpressionContext context)
		{
			if (double.TryParse(context.GetText(), out var value))
			{
				return new TokenNumericValue(value);
			}

			throw new InvalidOperationException($"Invalid numeric value. Line {context.Start.Line}, Column {context.Start.Column}");
		}

		public override TokenBase VisitForStatement(TurtleScriptParser.ForStatementContext context)
		{
			string loopVariableName = context.Identifier().GetText();

			// TODO: Figure out what to do for the TokenBase parameter in the variable declaration
			DeclareVariable(loopVariableName, null);

			TokenBase startValueExpression = Visit(context.expression(0));
			TokenBase endValueExpression = Visit(context.expression(1));

			TokenBase executionBlock = Visit(context.block());

			return new TokenForStatement(
				loopVariableName,
				startValueExpression,
				endValueExpression,
				executionBlock as TokenBlock);
		}

		/// <summary>
		/// Visit a parse tree produced by <see cref="TurtleScriptParser.functionDecl"/>.
		/// <para>
		/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
		/// on <paramref name="context"/>.
		/// </para>
		/// </summary>
		/// <param name="context">The parse tree.</param>
		/// <return>The visitor result.</return>
		public override TokenBase VisitFunctionDecl(TurtleScriptParser.FunctionDeclContext context)
		{
			TurtleScriptParser.FormalParametersContext formalParametersContext = context.formalParameters();
			IParseTree[] formalParameterContexts;

			if (formalParametersContext != null)
			{
				formalParameterContexts = formalParametersContext.formalParameter();
			}
			else
			{
				formalParameterContexts = Array.Empty<IParseTree>();
			}

			List<string> parameters = new List<string>(formalParameterContexts.Length);

			foreach (var formalParameterContext in formalParameterContexts)
			{
				var parameterName = formalParameterContext.GetText();
				parameters.Add(parameterName);

				// Note presence of the parameter for variable resolution
				DeclareVariable(parameterName, null);
			}

			TokenBlock functionBody = (TokenBlock)Visit(context.block());

			string functionName = context.Identifier().GetText();
			functionName += "_" + formalParameterContexts.Length;

			// Check for function that exists by the same name
			if (m_ScriptFunctions.TryGetValue(functionName, out _))
			{
				throw new InvalidOperationException(
					string.Format(
						"A function with the name '{0}' already exists. Line {1}, Column {2}",
						context.Identifier().GetText(),
						context.Start.Line,
						context.Start.Column));
			}

			m_ScriptFunctions.Add(functionName, new TurtleScriptFunction(functionName, formalParameterContexts, null));

			return new TokenFunctionDeclaration(
				functionName,
				parameters.ToArray(),
				functionBody);
		}

		public override TokenBase VisitIfStatement(TurtleScriptParser.IfStatementContext context)
		{
			TokenBase ifExpression = Visit(context.ifStat().expression());

			TokenBase block = Visit(context.ifStat().block());

			List<Tuple<TokenBase, TokenBase>> elseIfTokens = new List<Tuple<TokenBase, TokenBase>>();

			foreach (TurtleScriptParser.ElseIfStatContext elseIfStatContext in context.elseIfStat())
			{
				TokenBase elseIfExpression = Visit(elseIfStatContext.expression());

				TokenBase elseIfBlock = Visit(elseIfStatContext.block());

				elseIfTokens.Add(new Tuple<TokenBase, TokenBase>(elseIfExpression, elseIfBlock));
			}

			TokenBase elseStatement = null;

			if (context.elseStat() != null)
			{
				elseStatement = Visit(context.elseStat().block());
			}

			return new TokenIf(
				block,
				ifExpression,
				elseIfTokens,
				elseStatement);
		}

		public override TokenBase VisitIntExpression(TurtleScriptParser.IntExpressionContext context)
		{
			if (Int32.TryParse(context.GetText(), out var value))
			{
				return new TokenNumericValue(value);
			}

			throw new InvalidOperationException("Invalid integer value");
		}

		public override TokenBase VisitMultiplicativeOpExpression(TurtleScriptParser.MultiplicativeOpExpressionContext context)
		{
			TokenBase leftValue = Visit(context.expression(0));
			TokenBase rightValue = Visit(context.expression(1));

			TokenType multiplicativeOperator = TokenType.OpMultiply;
			switch (context.op.Type)
			{
				case TurtleScriptParser.Mul:
					multiplicativeOperator = TokenType.OpMultiply;
					break;
				case TurtleScriptParser.Div:
					multiplicativeOperator = TokenType.OpDivide;
					break;
				case TurtleScriptParser.Mod:
					multiplicativeOperator = TokenType.OpModulus;
					break;
			}

			TokenBase result = new TokenBinaryOperator(multiplicativeOperator);

			result.AddChild(leftValue);
			result.AddChild(rightValue);

			return result;
		}

		/// <summary>
		/// Visit a parse tree produced by the <c>orExpression</c>
		/// labeled alternative in <see cref="TurtleScriptParser.expression"/>.
		/// <para>
		/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
		/// on <paramref name="context"/>.
		/// </para>
		/// </summary>
		/// <param name="context">The parse tree.</param>
		/// <return>The visitor result.</return>
		public override TokenBase VisitOrExpression(TurtleScriptParser.OrExpressionContext context)
		{
			TokenBase leftValue = Visit(context.expression(0));
			TokenBase rightValue = Visit(context.expression(1));

			TokenBase result = new TokenBinaryOperator(TokenType.OpConditionalOr);

			result.AddChild(leftValue);
			result.AddChild(rightValue);

			return result;
		}
		public override TokenBase VisitParenExpression(TurtleScriptParser.ParenExpressionContext context)
		{
			TokenBase childExpression = Visit(context.expression());

			TokenParenthesizedExpression expressionToken = new TokenParenthesizedExpression(childExpression);

			return expressionToken;
		}

		/// <summary>
		/// Visit a parse tree produced by the <c>piExpression</c>
		/// labeled alternative in <see cref="TurtleScriptParser.expression"/>.
		/// <para>
		/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
		/// on <paramref name="context"/>.
		/// </para>
		/// </summary>
		/// <param name="context">The parse tree.</param>
		/// <return>The visitor result.</return>
		public override TokenBase VisitPiExpression(TurtleScriptParser.PiExpressionContext context)
		{
			return new TokenPi();
		}

		public override TokenBase VisitScript(TurtleScriptParser.ScriptContext context)
		{
			TokenBase scriptToken = new TokenScript();

			scriptToken.AddChild(Visit(context.block()));

			return scriptToken;
		}

		/// <summary>
		/// Visit a parse tree produced by the <c>unaryNegationExpression</c>
		/// labeled alternative in <see cref="TurtleScriptParser.expression"/>.
		/// <para>
		/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
		/// on <paramref name="context"/>.
		/// </para>
		/// </summary>
		/// <param name="context">The parse tree.</param>
		/// <return>The visitor result.</return>
		public override TokenBase VisitUnaryNegationExpression(TurtleScriptParser.UnaryNegationExpressionContext context)
		{
			TokenUnaryOperator unaryOperatorToken = new TokenUnaryOperator(TokenType.OpUnaryNegation);
			unaryOperatorToken.AddChild(Visit(context.expression()));
			return unaryOperatorToken;
		}

		/// <summary>
		/// Visit a parse tree produced by the <c>unaryNotExpression</c>
		/// labeled alternative in <see cref="TurtleScriptParser.expression"/>.
		/// <para>
		/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
		/// on <paramref name="context"/>.
		/// </para>
		/// </summary>
		/// <param name="context">The parse tree.</param>
		/// <return>The visitor result.</return>
		public override TokenBase VisitUnaryNotExpression(TurtleScriptParser.UnaryNotExpressionContext context)
		{
			TokenUnaryOperator unaryOperatorToken = new TokenUnaryOperator(TokenType.OpUnaryNot);
			unaryOperatorToken.AddChild(Visit(context.expression()));
			return unaryOperatorToken;
		}

		public override TokenBase VisitVariableReferenceExpression(TurtleScriptParser.VariableReferenceExpressionContext context)
		{
			var variableName = context.Identifier().GetText();

			if (m_VariablesDeclared.TryGetValue(
					variableName,
					out var variableDeclaration))
			{
				return new TokenVariableReference(variableName);
			}

			this.m_TurtleScriptErrorListener.SyntaxError(
				m_Parser, 
				m_Parser.CurrentToken, 
				context.Start.Line,
				context.Start.Column,
				string.Format("Reference to an unknown variable, '{0}'. Line {1}, Col {2}", variableName, context.Start.Line, context.Start.Column),
				null);

			return new TokenVariableReference(variableName);
		}

		#endregion Public Methods


		#region Private Fields

		private readonly List<ITurtleScriptRuntime> m_RuntimeLibraries;
		private readonly string m_Script;
		private string m_ErrorMessage;
		private TurtleScriptParser m_Parser;
		private Dictionary<string, TurtleScriptFunction> m_ScriptFunctions;
		private TurtleScriptErrorListener m_TurtleScriptErrorListener;
		private Dictionary<string, TokenBase> m_VariablesDeclared;

		#endregion Private Fields


		#region Private Methods

		private void DeclareVariable(
			string variableName,
			TokenBase declaration)
		{
			if (!m_VariablesDeclared.ContainsKey(variableName))
			{
				m_VariablesDeclared[variableName] = declaration;
			}
		}

		#endregion Private Methods

	}

}