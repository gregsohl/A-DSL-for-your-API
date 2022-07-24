#region Namespaces

using System.Diagnostics;
using System.Text;

#endregion Namespaces


namespace TurtleScript.Interpreter.Tokenize
{
	public class TurtleScriptBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TurtleScriptBuilder(int initialNestingLevel = 0)
		{
			m_NestingLevel = initialNestingLevel;
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

		public int DerementNestingLevel()
		{
			--m_NestingLevel;
			return m_NestingLevel;
		}

		public int IncrementNestingLevel()
		{
			return ++m_NestingLevel;
		}

		public void Trim()
		{
			if ((m_Builder.Length >= 2) &&
				(m_Builder[m_Builder.Length - 2] == '\r') &&
				(m_Builder[m_Builder.Length - 1] == '\n'))
				m_Builder.Length -= 2;

		}

		private readonly StringBuilder m_Builder;
		private int m_NestingLevel;

		private string Indent()
		{
			string indentPadding = new string('\t', m_NestingLevel);
			return indentPadding;
		}

	}
}
