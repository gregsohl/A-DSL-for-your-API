#region Namespaces

using System.Diagnostics;
using System.Text;

using TurtleScript.Interpreter.Utility;

#endregion Namespaces


namespace TurtleScript.Interpreter.Tokenize
{
	public class TurtleScriptBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TurtleScriptBuilder(int initialNestingLevel = 0)
			: this(
				initialNestingLevel,
				"  ",
				"\r\n")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TurtleScriptBuilder(
			int initialNestingLevel,
			string indentWith,
			string lineEnding)
		{
			m_NestingLevel = initialNestingLevel;
			m_IndentWith = indentWith;
			m_LineEnding = lineEnding;
			m_Builder = new StringBuilder();
		}


		public string Text
		{
			[DebuggerStepThrough]
			get { return m_Builder.ToString(); }
		}

		public void Append(
			char value)
		{
			m_Builder.Append(value);
		}

		public void Append(
			string value)
		{
			m_Builder.Append(value);
		}

		public void Append(
			string formatString,
			params object[] values)
		{
			m_Builder.AppendFormat(
				formatString,
				values);
		}

		public void AppendWithIndent(
			string value)
		{
			m_Builder.Append(Indent());
			m_Builder.Append(value);
		}

		public void AppendIndent(
			string formatString,
			params object[] values)
		{
			m_Builder.Append(Indent());
			m_Builder.AppendFormat(
				formatString,
				values);
		}

		public void AppendLineWithIndent(
			string value)
		{
			m_Builder.Append(Indent());
			m_Builder.AppendLine(value);
		}

		public void AppendLine(
			string formatString,
			params object[] values)
		{
			m_Builder.Append(Indent());
			m_Builder.AppendFormat(
				formatString,
				values);
			m_Builder.AppendLine();
		}

		public void AppendLine()
		{
			m_Builder.AppendLine();
		}

		public int DecrementNestingLevel()
		{
			--m_NestingLevel;
			return m_NestingLevel;
		}

		public int IncrementNestingLevel()
		{
			return ++m_NestingLevel;
		}

		/// <summary>
		/// Trim trailing line break if the script is single line
		/// </summary>
		public void Trim()
		{
			int indexOf = m_Builder.IndexOf("\r\n");
			if ((indexOf >=0) &&
				(indexOf == m_Builder.Length - 2))
			{
				if ((m_Builder.Length >= 2) &&
					(m_Builder[m_Builder.Length - 2] == '\r') &&
					(m_Builder[m_Builder.Length - 1] == '\n'))
					m_Builder.Length -= 2;
			}
		}

		private readonly StringBuilder m_Builder;
		private int m_NestingLevel;
		private string m_IndentWith;
		private string m_LineEnding;

		private string Indent()
		{
			string indentPadding = string.Empty;

			for (int indent = 0; indent < m_NestingLevel; indent++)
			{
				indentPadding += m_IndentWith;
			}
			return indentPadding;
		}

	}
}
