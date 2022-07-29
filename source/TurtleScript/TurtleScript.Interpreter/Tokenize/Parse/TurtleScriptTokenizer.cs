#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize.Parse
{
	public class TurtleScriptTokenizer
		: TurtleScriptBaseVisitor<TokenBase>
	{
		#region Public Constructors

		public TurtleScriptTokenizer(
			string script)
			: this(
				script,
				new TurtleScriptParserContext())
		{
		}

		public TurtleScriptTokenizer(
			string script,
			TurtleScriptParserContext parserContext)
		{
			m_Script = script;

			m_TurtleScriptErrorListener = new TurtleScriptErrorListener();
			m_TurtleScriptParserContext = parserContext ?? new TurtleScriptParserContext();
			m_ScriptFunctions = new TurtleScriptFunctions<TurtleScriptParserFunction>();
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

				if (m_TurtleScriptErrorListener.HasMessages)
				{
					return m_TurtleScriptErrorListener.Messages[0];
				}

				return string.Empty;
			}
		}

		public IEnumerable<string> ErrorMessages
		{
			get { return m_TurtleScriptErrorListener.Messages; }
		}

		public bool IsError
		{
			get
			{
				return
				(
					(m_TurtleScriptErrorListener.HasMessages) ||
					(!string.IsNullOrEmpty(m_ErrorMessage))
				);
			}
		}

		#endregion Public Properties


		#region Public Methods

		/// <summary>
		/// Executes the script
		/// </summary>
		/// <returns><c>true</c> if execution is successful, otherwise <c>false</c></returns>
		public bool Parse(out TokenBase rootToken)
		{
			rootToken = null;
			m_ErrorMessage = string.Empty;

			AntlrInputStream input = new AntlrInputStream(m_Script);

			TurtleScriptLexer lexer = new TurtleScriptLexer(input);

			CommonTokenStream tokenStream = new CommonTokenStream(lexer);

			m_Parser = new TurtleScriptParser(tokenStream);

			m_Parser.RemoveErrorListeners();
			m_TurtleScriptErrorListener = new TurtleScriptErrorListener();
			m_Parser.AddErrorListener(m_TurtleScriptErrorListener);

			m_Parser.BuildParseTree = true;

			m_TurtleScriptParserContext = new TurtleScriptParserContext();
			m_ScriptFunctions = new TurtleScriptFunctions<TurtleScriptParserFunction>();

			try
			{
				rootToken = Visit(m_Parser.script());
				if (m_TurtleScriptErrorListener.HasMessages)
				{
					m_ErrorMessage = m_TurtleScriptErrorListener.Messages[0];
				}
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
				context.op.Type == TurtleScriptParser.Add ? TokenType.OpAdd : TokenType.OpSubtract,
				context.Start.Line,
				context.Start.Column);

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

			TokenBase result = new TokenBinaryOperator(
				TokenType.OpConditionalAnd,
				context.Start.Line,
				context.Start.Column);

			result.AddChild(leftValue);
			result.AddChild(rightValue);

			return result;
		}

		public override TokenBase VisitAssignment(TurtleScriptParser.AssignmentContext context)
		{
			TokenBase value = Visit(context.expression());

			var variableName = context.Identifier().GetText();
			TokenBase result = new TokenAssignment(variableName, context.Start.Line, context.Start.Column);
			result.AddChild(value);

			DeclareVariable(variableName, result);

			return result;
		}

		public override TokenBase VisitBlock(TurtleScriptParser.BlockContext context)
		{
			TokenBase blockToken = new TokenBlock(
				context.Start.Line,
				context.Start.Column);

			TurtleScriptParser.FunctionDeclContext[] functionDeclContexts = context.functionDecl();

			foreach (TurtleScriptParser.FunctionDeclContext functionDeclContext in functionDeclContexts)
			{
				var functionDeclToken = Visit(functionDeclContext);
				blockToken.AddChild(functionDeclToken);
			}

			foreach (TurtleScriptParser.StatementContext statementContext in context.statement())
			{
				var statementToken = Visit(statementContext);
				blockToken.AddChild(statementToken);
			}

			return blockToken;
		}

		public override TokenBase VisitCompareExpression(TurtleScriptParser.CompareExpressionContext context)
		{
			TokenBase leftValue = Visit(context.expression(0));
			TokenBase rightValue = Visit(context.expression(1));

			TokenBase operatorToken;
			TokenType tokenType;

			switch (context.op.Type)
			{
				case TurtleScriptParser.EQ:
					tokenType = TokenType.OpEqual;
					break;
				case TurtleScriptParser.NE:
					tokenType = TokenType.OpNotEqual;
					break;
				case TurtleScriptParser.GT:
					tokenType = TokenType.OpGreaterThan;
					break;
				case TurtleScriptParser.LT:
					tokenType = TokenType.OpLessThan;
					break;
				case TurtleScriptParser.GE:
					tokenType = TokenType.OpGreaterThanOrEqual;
					break;
				case TurtleScriptParser.LE:
					tokenType = TokenType.OpLessThanOrEqual;
					break;
				default:
					tokenType = TokenType.OpEqual;
					break;
			}

			operatorToken = new TokenBinaryOperator(
				tokenType,
				context.Start.Line,
				context.Start.Column);

			operatorToken.AddChild(leftValue);
			operatorToken.AddChild(rightValue);

			return operatorToken;
		}

		public override TokenBase VisitFloatExpression(TurtleScriptParser.FloatExpressionContext context)
		{
			if (double.TryParse(context.GetText(), out var value))
			{
				return new TokenNumericValue(
					value,
					context.Start.Line,
					context.Start.Column);
			}

			m_TurtleScriptErrorListener.SyntaxError(
				m_Parser,
				m_Parser.CurrentToken,
				context.Start.Line,
				context.Start.Column,
				"Invalid numeric value",
				null);

			return TokenNumericValue.Default;
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
				executionBlock as TokenBlock,
				context.Start.Line,
				context.Start.Column);
		}

		public override TokenBase VisitFunctionCall(TurtleScriptParser.FunctionCallContext context)
		{
			TurtleScriptParser.ExpressionContext[] parameterExpressions =
				Array.Empty<TurtleScriptParser.ExpressionContext>();

			if (context.expressionList() != null)
			{
				parameterExpressions = context.expressionList().expression();
			}

			List<TurtleScriptParser.ExpressionContext> parameterContexts = parameterExpressions.ToList();
			List<TokenBase> parameterTokens = new List<TokenBase>(parameterContexts.Count);

			for (int parameterIndex = 0; parameterIndex < parameterContexts.Count; parameterIndex++)
			{
				TokenBase parameterToken = Visit(parameterContexts[parameterIndex]);
				parameterTokens.Add(parameterToken);
			}

			if (context.Identifier() != null)
			{
				// User Defined Function
				string functionCallName = context.Identifier().GetText();

				if (m_ScriptFunctions.TryGetFunction(
						functionCallName,
						parameterExpressions.Length,
						out TurtleScriptParserFunction function))
				{

					if (parameterExpressions.Length != function.ParameterCount)
					{
						m_TurtleScriptErrorListener.SyntaxError(
							m_Parser,
							m_Parser.CurrentToken,
							context.Start.Line,
							context.Start.Column,
							string.Format("Invalid number of parameters specified for function call"),
							null);

						return TokenBase.Default;
					}

					TokenFunctionCall result = new TokenFunctionCall(
						functionCallName,
						parameterTokens.ToArray(),
						context.Start.Line,
						context.Start.Column);

					return result;
				}
			}

			if (context.QualifiedIdentifier() != null)
			{
				string fullIdentifier = context.QualifiedIdentifier().GetText();
				string[] identifierParts = fullIdentifier.Split('.');

				string runtimeName = identifierParts[0];
				string functionName = identifierParts[1];

				bool runtimeFound = m_TurtleScriptParserContext.TryGetRuntimeLibrary(
					runtimeName, out ITurtleScriptRuntime runtime);

				TokenFunctionCall result = new TokenFunctionCall(
					fullIdentifier,
					parameterTokens.ToArray(),
					FunctionType.Runtime,
					context.Start.Line,
					context.Start.Column);

				return result;
			}

			m_TurtleScriptErrorListener.SyntaxError(
				m_Parser,
				m_Parser.CurrentToken,
				context.Start.Line,
				context.Start.Column,
				"Invalid call to undefined function",
				null);

			return TokenFunctionCall.Default;
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
			string functionName = context.Identifier().GetText();

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
			}

			TokenFunctionDeclaration functionDeclaration = new TokenFunctionDeclaration(
				functionName,
				parameters.ToArray(),
				context.Start.Line,
				context.Start.Column);


			m_TurtleScriptParserContext.PushScope(functionName);
			foreach (string parameter in parameters)
			{
				m_TurtleScriptParserContext.DeclareVariable(parameter, VariableType.Parameter, functionDeclaration);
			}

			TokenBlock functionBody = (TokenBlock)Visit(context.block());

			functionDeclaration.SetFunctionBody(functionBody);

			m_TurtleScriptParserContext.PopScope();

			// Check for function that exists by the same name
			if (m_ScriptFunctions.TryGetFunction(functionName, parameters.Count, out _))
			{
				m_TurtleScriptErrorListener.SyntaxError(
					m_Parser,
					m_Parser.CurrentToken,
					context.Start.Line,
					context.Start.Column,
					"A function with the name '{0}' already exists",
					null);

				return TokenFunctionDeclaration.Default;
			}

			var newFunction = new TurtleScriptParserFunction(
				functionName,
				formalParameterContexts.Length,
				functionDeclaration);

			bool addResult = m_ScriptFunctions.TryAdd(newFunction);

			return functionDeclaration;
		}


		public override TokenBase VisitIfStatement(TurtleScriptParser.IfStatementContext context)
		{
			TokenBase ifExpression = Visit(context.ifStat().expression());

			TokenBlock block = (TokenBlock)Visit(context.ifStat().block());

			List<Tuple<TokenBase, TokenBase>> elseIfTokens = new List<Tuple<TokenBase, TokenBase>>();

			foreach (TurtleScriptParser.ElseIfStatContext elseIfStatContext in context.elseIfStat())
			{
				TokenBase elseIfExpression = Visit(elseIfStatContext.expression());

				TokenBase elseIfBlock = Visit(elseIfStatContext.block());

				elseIfTokens.Add(new Tuple<TokenBase, TokenBase>(elseIfExpression, elseIfBlock));
			}

			TokenBlock elseStatement = null;

			if (context.elseStat() != null)
			{
				elseStatement = (TokenBlock)Visit(context.elseStat().block());
			}

			return new TokenIf(
				block,
				ifExpression,
				elseIfTokens,
				elseStatement,
				context.Start.Line,
				context.Start.Column);
		}

		public override TokenBase VisitIntExpression(TurtleScriptParser.IntExpressionContext context)
		{
			if (Int32.TryParse(context.GetText(), out var value))
			{
				return new TokenNumericValue(
					value,
					context.Start.Line,
					context.Start.Column);
			}

			m_TurtleScriptErrorListener.SyntaxError(
				m_Parser,
				m_Parser.CurrentToken,
				context.Start.Line,
				context.Start.Column,
				"Invalid integer value",
				null);

			return TokenNumericValue.Default;
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

			TokenBase result = new TokenBinaryOperator(
				multiplicativeOperator,
				context.Start.Line,
				context.Start.Column);

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

			TokenBase result = new TokenBinaryOperator(
				TokenType.OpConditionalOr,
				context.Start.Line,
				context.Start.Column);

			result.AddChild(leftValue);
			result.AddChild(rightValue);

			return result;
		}

		public override TokenBase VisitParenExpression(TurtleScriptParser.ParenExpressionContext context)
		{
			TokenBase childExpression = Visit(context.expression());

			TokenParenthesizedExpression expressionToken = new TokenParenthesizedExpression(
				childExpression,
				context.Start.Line,
				context.Start.Column);

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
			return new TokenPi(
				context.Start.Line,
				context.Start.Column);
		}

		public override TokenBase VisitScript(TurtleScriptParser.ScriptContext context)
		{
			TokenBase scriptToken = new TokenScript(
				context.Start.Line,
				context.Start.Column);

			TokenBase blockToken = Visit(context.block());
			scriptToken.AddChild(blockToken);

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
			TokenUnaryOperator unaryOperatorToken = new TokenUnaryOperator(
				TokenType.OpUnaryNegation,
				context.Start.Line,
				context.Start.Column);
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
			TokenUnaryOperator unaryOperatorToken = new TokenUnaryOperator(
				TokenType.OpUnaryNot,
				context.Start.Line,
				context.Start.Column);
			unaryOperatorToken.AddChild(Visit(context.expression()));
			return unaryOperatorToken;
		}

		public override TokenBase VisitVariableReferenceExpression(TurtleScriptParser.VariableReferenceExpressionContext context)
		{
			var variableName = context.Identifier().GetText();

			if (m_TurtleScriptParserContext.IsVariableDeclared(
					variableName,
					out _))
			{
				return new TokenVariableReference(
					variableName,
					context.Start.Line,
					context.Start.Column);
			}

			m_TurtleScriptErrorListener.SyntaxError(
				m_Parser, 
				m_Parser.CurrentToken, 
				context.Start.Line,
				context.Start.Column,
				string.Format("Reference to an unknown variable, '{0}'", variableName),
				null);

			return TokenVariableReference.Default;
		}

		#endregion Public Methods


		#region Private Fields

		private readonly string m_Script;
		private string m_ErrorMessage;
		private TurtleScriptParser m_Parser;
		private TurtleScriptFunctions<TurtleScriptParserFunction> m_ScriptFunctions;
		private TurtleScriptErrorListener m_TurtleScriptErrorListener;
		private TurtleScriptParserContext m_TurtleScriptParserContext;

		#endregion Private Fields


		#region Private Methods

		private void DeclareVariable(
			string variableName,
			TokenBase declaration)
		{
			if (!m_TurtleScriptParserContext.IsVariableDeclared(variableName, out _))
			{
				m_TurtleScriptParserContext.DeclareVariable(
					variableName,
					VariableType.Variable,
					declaration);
			}
		}


		#endregion Private Methods

	}

}