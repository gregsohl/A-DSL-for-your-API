#region Namespaces

using System;
using System.Drawing;
using System.Windows.Forms;

using Nakov.TurtleGraphics;

#endregion Namespaces

namespace Turtle_Graphics_Example
{
	public partial class DemoForm : Form
	{
		public DemoForm()
		{
			InitializeComponent();
		}

		private void buttonDraw_Click(object sender, EventArgs e)
		{
			Enabled = false;
			try
			{

				// Assign a delay to visualize the drawing process
				Turtle.Delay = 150;

				// Draw a equilateral triangle
				Turtle.PenColor = Color.Green;
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
	}
}
