#region Namespaces

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Nakov.TurtleGraphics;
using TurtleScript.Interpreter;

using ScriptedTurtle.Runtime;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

#endregion Namespaces

namespace ScriptedTurtle
{
	public partial class ScriptedTurtleForm : Form
	{

		public ScriptedTurtleForm()
		{
			InitializeComponent();
		}

		private TokenBase m_ScriptToken;

		private void buttonDraw_Click(object sender, EventArgs e)
		{
			Enabled = false;
			try
			{

				// Assign a delay to visualize the drawing process
				Turtle.Delay = 150;

				// Draw a equilateral triangle
				Turtle.PenColor = Color.Green;
				Turtle.PenSize = 5;
				Turtle.Rotate(30);
				Turtle.Forward(200);
				Turtle.Rotate(120);
				Turtle.Forward(200);
				Turtle.Rotate(120);
				Turtle.Forward(200);

				// Draw a line in the triangle
				Turtle.Rotate(-30);
				Turtle.PenUp();
				Turtle.Backward(50);
				Turtle.PenDown();
				Turtle.PenColor = Color.Red;
				Turtle.PenSize = 5;
				Turtle.Backward(100);
				Turtle.PenColor = Turtle.DEFAULT_COLOR;
				Turtle.PenSize = Turtle.DEFAULT_PEN_SIZE;
				Turtle.PenUp();
				Turtle.Forward(150);
				Turtle.PenDown();
				Turtle.Rotate(30);
			}
			finally
			{
				Enabled = true;
			}

			UpdateStatusBar();
		}

		private void buttonDrawSpiral_Click(object sender, EventArgs e)
		{
			Enabled = false;

			try
			{
				Turtle.PenColor = Color.Red;
				Turtle.Delay = 50;
			
				for (int i = 0; i < 25; i++)
				{
					Turtle.Forward(i * 5);
					Turtle.Rotate(30 + i);
				}

			}
			finally
			{
				Enabled = true;
			}

			UpdateStatusBar();
		}

		private void buttonReset_Click(object sender, EventArgs e)
		{
			Turtle.Reset();
			UpdateStatusBar();
		}

		private void buttonShowHideTurtle_Click(object sender, EventArgs e)
		{
			Turtle.ShowTurtle = !Turtle.ShowTurtle;

			if (Turtle.ShowTurtle)
			{
				buttonShowHideTurtle.Text = "Show &Turtle";
			}
			else
			{
				buttonShowHideTurtle.Text = "Hide &Turtle";
			}
		}

		private void DemoForm_Load(object sender, EventArgs e)
		{
			Turtle.Init(panelSurface);
			UpdateStatusBar();

			Turtle.Delay = 100;
		}

		private void UpdateStatusBar()
		{
			statusBar.Items["Coordinates"].Text = string.Format("X:{0} Y:{1}", Turtle.X, Turtle.Y);
			statusBar.Items["Angle"].Text = string.Format("Angle:{0}", Turtle.Angle);
			statusBar.Items["PenStatus"].Text = string.Format("Pen: {0}", Turtle.PenVisible ? "Down" : "Up");
			statusBar.Items["PenColor"].Text = Turtle.PenColor.ToString();
			statusBar.Items["PenSize"].Text = string.Format("Size: {0}", Turtle.PenSize);
		}

		private void buttonSetAngle_Click(object sender, EventArgs e)
		{
			string angleText = InputPrompt.ShowDialog("Angle", "Set Turtle Angle");

			float angle;
			if (float.TryParse(angleText, out angle))
			{
				Turtle.Angle = angle;
				UpdateStatusBar();
			}
		}

		private void buttonPenStatus_Click(object sender, EventArgs e)
		{
			Turtle.PenVisible = !Turtle.PenVisible;

			if (Turtle.PenVisible)
			{
				buttonPenStatus.Text = "Pen &Up";
			}
			else
			{
				buttonPenStatus.Text = "Pen &Down";
			}

			UpdateStatusBar();

		}

		private void buttonColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			
			if (colorDialog.ShowDialog(this) == DialogResult.OK)
			{
				Turtle.PenColor = colorDialog.Color;

				UpdateStatusBar();
			}
		}

		private void buttonPenSize_Click(object sender, EventArgs e)
		{
			string penSizeText = InputPrompt.ShowDialog("Size", "Set Pen Size");

			int penSize;
			if (int.TryParse(penSizeText, out penSize))
			{
				Turtle.PenSize = penSize;
				UpdateStatusBar();
			}

		}

		private void buttonForward_Click(object sender, EventArgs e)
		{
			string distanceText = InputPrompt.ShowDialog("Distance", "Enter Move Distance");

			int distance;
			if (int.TryParse(distanceText, out distance))
			{
				Turtle.Forward(distance);
				UpdateStatusBar();
			}
		}

		private void buttonBackward_Click(object sender, EventArgs e)
		{
			string distanceText = InputPrompt.ShowDialog("Distance", "Enter Move Distance");

			int distance;
			if (int.TryParse(distanceText, out distance))
			{
				Turtle.Backward(distance);
				UpdateStatusBar();
			}
		}

		private void buttonExecute_Click(object sender, EventArgs e)
		{
			var runtimeFunctions = new ScriptedTurtleFunctions();
			var mathRuntimeFunctions = new TurtleMath();

			TurtleScriptInterpreter interpreter =
				new TurtleScriptInterpreter(
					txtScript.Text,
					new List<ITurtleScriptRuntime> { runtimeFunctions, mathRuntimeFunctions });

			buttonExecute.Enabled = false;

			bool result;
			Cursor = Cursors.WaitCursor;

			try
			{
				result = interpreter.Execute();
			}
			finally
			{
				buttonExecute.Enabled = true;
				Cursor = Cursors.Default;
			}

			if (!result)
			{
				MessageBox.Show(this,
					interpreter.ErrorMessage,
					"Error");
			}

			UpdateStatusBar();
		}

		private void buttonRunTokenized_Click(object sender, EventArgs e)
		{
			var runtimeFunctions = new ScriptedTurtleFunctions();
			var mathRuntimeFunctions = new TurtleMath();

			TurtleScriptParserContext parserContext =
				new TurtleScriptParserContext(
					new List<ITurtleScriptRuntime> { runtimeFunctions, mathRuntimeFunctions });
			TurtleScriptTokenizer parser = new TurtleScriptTokenizer(
				txtScriptTokenized.Text,
				parserContext);

			TurtleScriptExecutionContext executionContext = new TurtleScriptExecutionContext(
				new List<ITurtleScriptRuntime> { runtimeFunctions, mathRuntimeFunctions });
			TurtleScriptExecutor executor = new TurtleScriptExecutor();


			m_ScriptToken = null;

			buttonExecute.Enabled = false;

			Cursor = Cursors.WaitCursor;

			try
			{
				bool parserResult = parser.Parse(out TokenBase scriptToken);

				if (!parserResult)
				{
					MessageBox.Show(this,
						parser.ErrorMessage,
						"Error");
				}
				else
				{
					m_ScriptToken = scriptToken;
				}

				txtScriptTokenizedDecompiled.Text = scriptToken.ToTurtleScript();
				Application.DoEvents();

				executor.Execute(scriptToken, executionContext);

				if (executor.IsError)
				{
					MessageBox.Show(this,
						executor.ErrorMessage,
						"Error");
				}

			}
			finally
			{
				buttonExecute.Enabled = true;
				Cursor = Cursors.Default;
			}


			UpdateStatusBar();
		}


		private void buttonSaveTokenized_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog dialog = new SaveFileDialog())
			{
				dialog.Filter = "Turtlescript (*.tsscript)|*.tsscript|All Files (*.*)|*.*";
				dialog.DefaultExt = "tsscript";

				if (dialog.ShowDialog(this) == DialogResult.OK)
				{
					File.WriteAllBytes(
						dialog.FileName,
						TokenSerializer.SerializeToArray(m_ScriptToken));
				}
			}
		}

		private void buttonLoadTokenized_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.Filter = "Turtlescript (*.tsscript)|*.tsscript|All Files (*.*)|*.*";
				dialog.DefaultExt = "tsscript";

				if (dialog.ShowDialog(this) == DialogResult.OK)
				{
					byte[] fileContent = File.ReadAllBytes(dialog.FileName);
					m_ScriptToken = TokenSerializer.DeserializeFromArray(fileContent);

					txtScriptTokenizedDecompiled.Text = m_ScriptToken.ToTurtleScript();
				}
			}
		}
	}
}
