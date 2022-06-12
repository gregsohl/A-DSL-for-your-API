#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

using TurtleScript.Interpreter.Tokenize;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	public class TurtleScriptTokenizer
		: TurtleScriptBaseVisitor<TokenBase>
	{
		#region Constructor
		public TurtleScriptTokenizer(
			string script, 
			List<ITurtleScriptRuntime> runtimeLibraries = null)
		{
			m_Script = script;
			m_Tokens = new List<TokenBase>();

			m_RuntimeLibraries = runtimeLibraries ?? new List<ITurtleScriptRuntime>();

			m_TurtleScriptErrorListener = new TurtleScriptErrorListener();
			m_Variables = new Dictionary<string, TokenBase>();
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

		public Dictionary<string, TokenBase> Variables
		{
			get { return m_Variables; }
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

			AntlrInputStream input = new AntlrInputStream(m_Script);

			TurtleScriptLexer lexer = new TurtleScriptLexer(input);

			CommonTokenStream tokenStream = new CommonTokenStream(lexer);

			TurtleScriptParser parser = new TurtleScriptParser(tokenStream);

			parser.RemoveErrorListeners();
			m_TurtleScriptErrorListener = new TurtleScriptErrorListener();
			parser.AddErrorListener(m_TurtleScriptErrorListener);

			parser.BuildParseTree = true;

			m_Variables = new Dictionary<string, TokenBase>();
			m_ScriptFunctions = new Dictionary<string, TurtleScriptFunction>();

			try
			{
				rootToken = Visit(parser.script());
			}
			catch (InvalidOperationException exception)
			{
				m_ErrorMessage = exception.Message;
			}

			return !IsError;
		}

		public void Execute(TokenBase script, TurtleScriptExecutionContext context)
		{
			script.Visit(context);

		}

		public override TokenBase VisitAssignment(TurtleScriptParser.AssignmentContext context)
		{
			TokenBase value = Visit(context.expression());

			TokenBase result = new TokenAssignment(context.Identifier().GetText());
			result.AddChild(value);

			return result;
		}

		public override TokenBase VisitAdditiveExpression(TurtleScriptParser.AdditiveExpressionContext context)
		{
			TokenBase leftValue = Visit(context.expression(0));
			TokenBase rightValue = Visit(context.expression(1));

			TokenBase result = new TokenBinaryOperator(
				context.op.Type == TurtleScriptParser.Add ? TokenType.Add : TokenType.Subtract);

			result.AddChild(leftValue);
			result.AddChild(rightValue);

			return result;
		}

		public override TokenBase VisitFloatExpression(TurtleScriptParser.FloatExpressionContext context)
		{
			if (double.TryParse(context.GetText(), out var value))
			{
				return new TokenNumericValue(value);
			}

			throw new InvalidOperationException($"Invalid numeric value. Line {context.Start.Line}, Column {context.Start.Column}"); 
		}


		public override TokenBase VisitIntExpression(TurtleScriptParser.IntExpressionContext context)
		{
			if (Int32.TryParse(context.GetText(), out var value))
			{
				return new TokenNumericValue(value);
			}

			throw new InvalidOperationException("Invalid integer value");
		}

		public override TokenBase VisitScript(TurtleScriptParser.ScriptContext context)
		{
			TokenBase scriptToken = new TokenScript();

			scriptToken.AddChild(Visit(context.block()));

			return scriptToken;
		}

		public override TokenBase VisitBlock(TurtleScriptParser.BlockContext context)
		{
			TokenBase blockToken = new TokenBlock();

			foreach (TurtleScriptParser.StatementContext statementContext in context.statement())
			{
				blockToken.AddChild(Visit(statementContext));
			}

			return blockToken;
		}

		#endregion Public Methods

		#region Private Fields

		private readonly string m_Script;
		private readonly List<ITurtleScriptRuntime> m_RuntimeLibraries;
		private TurtleScriptErrorListener m_TurtleScriptErrorListener;
		private string m_ErrorMessage;
		private Dictionary<string, TokenBase> m_Variables;
		private Dictionary<string, TurtleScriptFunction> m_ScriptFunctions;
		private List<TokenBase> m_Tokens;

		#endregion Private Fields

		#region Private Methods

		#endregion Private Methods

	}
}