#region Namespaces

using System.Collections.Generic;

using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Dfa;
using Antlr4.Runtime.Sharpen;

#endregion Namespaces

namespace TurtleScript.Interpreter
{
	public class TurtleScriptErrorListener : IParserErrorListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TurtleScriptErrorListener()
		{
			m_Messages = new List<string>();
		}

		public bool HasMessages
		{ get { return m_Messages.Count > 0; } }

		public string[] Messages
		{ get { return m_Messages.ToArray(); } }


		public void SyntaxError(
			IRecognizer recognizer,
			IToken offendingSymbol,
			int line,
			int charPositionInLine,
			string msg,
			RecognitionException e)
		{
			m_Messages.Add(string.Format("{0}. Line {1}, Col {2}", msg, line, charPositionInLine));
		}

		/// <summary>
		/// This method is called by the parser when a full-context prediction
		/// results in an ambiguity.
		/// </summary>
		/// <remarks>
		/// This method is called by the parser when a full-context prediction
		/// results in an ambiguity.
		/// <p>Each full-context prediction which does not result in a syntax error
		/// will call either
		/// <see cref="M:Antlr4.Runtime.IParserErrorListener.ReportContextSensitivity(Antlr4.Runtime.Parser,Antlr4.Runtime.Dfa.DFA,System.Int32,System.Int32,System.Int32,Antlr4.Runtime.Atn.SimulatorState)" />
		/// or
		/// <see cref="M:Antlr4.Runtime.IParserErrorListener.ReportAmbiguity(Antlr4.Runtime.Parser,Antlr4.Runtime.Dfa.DFA,System.Int32,System.Int32,System.Boolean,Antlr4.Runtime.Sharpen.BitSet,Antlr4.Runtime.Atn.ATNConfigSet)" />
		/// .</p>
		/// <p>
		/// When
		/// <paramref name="ambigAlts" />
		/// is not null, it contains the set of potentially
		/// viable alternatives identified by the prediction algorithm. When
		/// <paramref name="ambigAlts" />
		/// is null, use
		/// <see cref="P:Antlr4.Runtime.Atn.ATNConfigSet.RepresentedAlternatives" />
		/// to obtain the represented
		/// alternatives from the
		/// <paramref name="configs" />
		/// argument.</p>
		/// <p>When
		/// <paramref name="exact" />
		/// is
		/// <see langword="true" />
		/// , <em>all</em> of the potentially
		/// viable alternatives are truly viable, i.e. this is reporting an exact
		/// ambiguity. When
		/// <paramref name="exact" />
		/// is
		/// <see langword="false" />
		/// , <em>at least two</em> of
		/// the potentially viable alternatives are viable for the current input, but
		/// the prediction algorithm terminated as soon as it determined that at
		/// least the <em>minimum</em> potentially viable alternative is truly
		/// viable.</p>
		/// <p>When the
		/// <see cref="F:Antlr4.Runtime.Atn.PredictionMode.LlExactAmbigDetection" />
		/// prediction
		/// mode is used, the parser is required to identify exact ambiguities so
		/// <paramref name="exact" />
		/// will always be
		/// <see langword="true" />
		/// .</p>
		/// </remarks>
		/// <param name="recognizer">the parser instance</param>
		/// <param name="dfa">the DFA for the current decision</param>
		/// <param name="startIndex">the input index where the decision started</param>
		/// <param name="stopIndex">the input input where the ambiguity was identified</param>
		/// <param name="exact">
		/// <see langword="true" />
		/// if the ambiguity is exactly known, otherwise
		/// <see langword="false" />
		/// . This is always
		/// <see langword="true" />
		/// when
		/// <see cref="F:Antlr4.Runtime.Atn.PredictionMode.LlExactAmbigDetection" />
		/// is used.
		/// </param>
		/// <param name="ambigAlts">
		/// the potentially ambiguous alternatives, or
		/// <see langword="null" />
		/// to indicate that the potentially ambiguous alternatives are the complete
		/// set of represented alternatives in
		/// <paramref name="configs" />
		/// </param>
		/// <param name="configs">
		/// the ATN configuration set where the ambiguity was
		/// identified
		/// </param>
		public void ReportAmbiguity(
			Parser recognizer,
			DFA dfa,
			int startIndex,
			int stopIndex,
			bool exact,
			BitSet ambigAlts,
			ATNConfigSet configs)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// This method is called when an SLL conflict occurs and the parser is about
		/// to use the full context information to make an LL decision.
		/// </summary>
		/// <remarks>
		/// This method is called when an SLL conflict occurs and the parser is about
		/// to use the full context information to make an LL decision.
		/// <p>If one or more configurations in
		/// <c>configs</c>
		/// contains a semantic
		/// predicate, the predicates are evaluated before this method is called. The
		/// subset of alternatives which are still viable after predicates are
		/// evaluated is reported in
		/// <paramref name="conflictingAlts" />
		/// .</p>
		/// </remarks>
		/// <param name="recognizer">the parser instance</param>
		/// <param name="dfa">the DFA for the current decision</param>
		/// <param name="startIndex">the input index where the decision started</param>
		/// <param name="stopIndex">the input index where the SLL conflict occurred</param>
		/// <param name="conflictingAlts">
		/// The specific conflicting alternatives. If this is
		/// <see langword="null" />
		/// , the conflicting alternatives are all alternatives
		/// represented in
		/// <c>configs</c>
		/// .
		/// </param>
		/// <param name="conflictState">
		/// the simulator state when the SLL conflict was
		/// detected
		/// </param>
		public void ReportAttemptingFullContext(
			Parser recognizer,
			DFA dfa,
			int startIndex,
			int stopIndex,
			BitSet conflictingAlts,
			SimulatorState conflictState)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// This method is called by the parser when a full-context prediction has a
		/// unique result.
		/// </summary>
		/// <remarks>
		/// This method is called by the parser when a full-context prediction has a
		/// unique result.
		/// <p>Each full-context prediction which does not result in a syntax error
		/// will call either
		/// <see cref="M:Antlr4.Runtime.IParserErrorListener.ReportContextSensitivity(Antlr4.Runtime.Parser,Antlr4.Runtime.Dfa.DFA,System.Int32,System.Int32,System.Int32,Antlr4.Runtime.Atn.SimulatorState)" />
		/// or
		/// <see cref="M:Antlr4.Runtime.IParserErrorListener.ReportAmbiguity(Antlr4.Runtime.Parser,Antlr4.Runtime.Dfa.DFA,System.Int32,System.Int32,System.Boolean,Antlr4.Runtime.Sharpen.BitSet,Antlr4.Runtime.Atn.ATNConfigSet)" />
		/// .</p>
		/// <p>For prediction implementations that only evaluate full-context
		/// predictions when an SLL conflict is found (including the default
		/// <see cref="T:Antlr4.Runtime.Atn.ParserATNSimulator" />
		/// implementation), this method reports cases
		/// where SLL conflicts were resolved to unique full-context predictions,
		/// i.e. the decision was context-sensitive. This report does not necessarily
		/// indicate a problem, and it may appear even in completely unambiguous
		/// grammars.</p>
		/// <p>
		/// <c>configs</c>
		/// may have more than one represented alternative if the
		/// full-context prediction algorithm does not evaluate predicates before
		/// beginning the full-context prediction. In all cases, the final prediction
		/// is passed as the
		/// <paramref name="prediction" />
		/// argument.</p>
		/// <p>Note that the definition of "context sensitivity" in this method
		/// differs from the concept in
		/// <see cref="F:Antlr4.Runtime.Atn.DecisionInfo.contextSensitivities" />
		/// .
		/// This method reports all instances where an SLL conflict occurred but LL
		/// parsing produced a unique result, whether or not that unique result
		/// matches the minimum alternative in the SLL conflicting set.</p>
		/// </remarks>
		/// <param name="recognizer">the parser instance</param>
		/// <param name="dfa">the DFA for the current decision</param>
		/// <param name="startIndex">the input index where the decision started</param>
		/// <param name="stopIndex">
		/// the input index where the context sensitivity was
		/// finally determined
		/// </param>
		/// <param name="prediction">the unambiguous result of the full-context prediction</param>
		/// <param name="acceptState">
		/// the simulator state when the unambiguous prediction
		/// was determined
		/// </param>
		public void ReportContextSensitivity(
			Parser recognizer,
			DFA dfa,
			int startIndex,
			int stopIndex,
			int prediction,
			SimulatorState acceptState)
		{
			throw new System.NotImplementedException();
		}

		private readonly List<string> m_Messages;

	}
}