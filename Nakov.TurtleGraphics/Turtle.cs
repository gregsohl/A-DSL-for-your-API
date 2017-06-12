#region Namespaces

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Nakov.TurtleGraphics.Properties;

#endregion

namespace Nakov.TurtleGraphics
{
	public static class Turtle
	{
		#region Public Constants

		public const int DEFAULT_PEN_SIZE = 7;
		public const int DRAW_AREA_SIZE = 10000;
		public static readonly Color DEFAULT_COLOR = Color.Blue;

		#endregion Public Constants

		#region Public Properties

		public static float Angle
		{
			get
			{
				InitOnDemand();
				return m_Angle;
			}
			set
			{
				InitOnDemand();
				m_Angle = value % 360;
				if (m_Angle < 0)
				{
					m_Angle += 360;
				}
			}
		}

		public static int Delay
		{
			get
			{
				InitOnDemand();
				return m_Delay;
			}
			set
			{
				InitOnDemand();
				m_Delay = value;
			}
		}

		public static Color PenColor
		{
			get
			{
				InitOnDemand();
				return m_DrawPen.Color;
			}
			set
			{
				InitOnDemand();
				m_DrawPen.Color = value;
			}
		}

		public static float PenSize
		{
			get
			{
				InitOnDemand();
				return m_DrawPen.Width;
			}
			set
			{
				InitOnDemand();
				m_DrawPen.Width = value;
			}
		}

		public static bool PenVisible
		{
			get
			{
				InitOnDemand();
				return m_PenVisible;
			}
			set
			{
				InitOnDemand();
				m_PenVisible = value;
			}
		}

		public static bool ShowTurtle
		{
			get
			{
				InitOnDemand();
				return m_TurtleHeadImage.Visible;
			}
			set
			{
				InitOnDemand();
				m_TurtleHeadImage.Visible = value;
			}
		}

		public static float X
		{
			get
			{
				InitOnDemand();
				return m_X;
			}
			set
			{
				InitOnDemand();
				m_X = value;
			}
		}
		public static float Y
		{
			get
			{
				InitOnDemand();
				return m_Y;
			}
			set
			{
				InitOnDemand();
				m_Y = value;
			}
		}
		
		#endregion Public Properties

		#region Public Methods

		public static void Backward(float distance = 10)
		{
			Forward(-distance);
		}

		public static void Dispose()
		{
			if (m_DrawControl != null)
			{
				// Release the pen object
				m_DrawPen.Dispose();
				m_DrawPen = null;

				// Release the graphic object
				m_DrawGraphics.Dispose();
				m_DrawGraphics = null;

				// Release the draw surface (image) object
				m_DrawImage.Dispose();
				m_DrawImage = null;

				// Release the turtle (head) image
				m_DrawControl.Controls.Remove(m_TurtleHeadImage);
				m_TurtleHeadImage.Dispose();
				m_TurtleHeadImage = null;

				// Release the drawing control and its associated events
				m_DrawControl.Paint -= DrawControl_Paint;
				m_DrawControl.ClientSizeChanged -= DrawControl_ClientSizeChanged;
				m_DrawControl.Invalidate();
				m_DrawControl = null;
			}
		}

		public static void Forward(float distance = 10)
		{
			var angleRadians = Angle * Math.PI / 180;
			var newX = X + (float)(distance * Math.Sin(angleRadians));
			var newY = Y + (float)(distance * Math.Cos(angleRadians));
			MoveTo(newX, newY);
		}

		public static void Init(Control targetControl = null)
		{
			// Dispose all resources if already allocated
			Dispose();

			// Initialize the drawing control (sufrace)
			m_DrawControl = targetControl;
			
			if (m_DrawControl == null)
			{
				// If no target control is provided, use the currently active form
				m_DrawControl = Form.ActiveForm;
			}
			
			SetDoubleBuffered(m_DrawControl);

			// Create an empty graphics area to be used by the turtle
			m_DrawImage = new Bitmap(DRAW_AREA_SIZE, DRAW_AREA_SIZE); 
			m_DrawControl.Paint += DrawControl_Paint;
			m_DrawControl.ClientSizeChanged += DrawControl_ClientSizeChanged;
			m_DrawGraphics = Graphics.FromImage(m_DrawImage);
			m_DrawGraphics.SmoothingMode = SmoothingMode.AntiAlias;

			// Initialize the pen size and color
			m_DrawPen = new Pen(DEFAULT_COLOR, DEFAULT_PEN_SIZE);
			m_DrawPen.StartCap = LineCap.Round;
			m_DrawPen.EndCap = LineCap.Round;

			// Initialize the turtle position and other settings
			X = 0;
			Y = 0;
			Angle = 0;
			PenVisible = true;
			
			// Initialize the turtle head image
			m_TurtleHeadImage = new PictureBox();
			m_TurtleHeadImage.BackColor = Color.Transparent;
			m_DrawControl.Controls.Add(m_TurtleHeadImage);
		}
		public static void MoveTo(float newX, float newY)
		{
			InitOnDemand();
			var fromX = DRAW_AREA_SIZE / 2 + X;
			var fromY = DRAW_AREA_SIZE / 2 - Y;
			X = newX;
			Y = newY;

			if (PenVisible)
			{
				var toX = DRAW_AREA_SIZE / 2 + X;
				var toY = DRAW_AREA_SIZE / 2 - Y;
				m_DrawGraphics.DrawLine(m_DrawPen, fromX, fromY, toX, toY);
			}

			DrawTurtle();
			PaintAndDelay();
		}

		public static void PenDown()
		{
			PenVisible = true;
		}

		public static void PenUp()
		{
			PenVisible = false;
		}

		public static void Reset()
		{
			m_DrawGraphics.Clear(Color.White);
			PenSize = DEFAULT_PEN_SIZE;
			PenColor = DEFAULT_COLOR;
			Angle = 0;
			PenDown();
			Delay = 10;
		}

		public static void Rotate(float angleDelta)
		{
			InitOnDemand();
			Angle += angleDelta;
			DrawTurtle();
			PaintAndDelay();
		}
		
		public static void RotateTo(float newAngle)
		{
			InitOnDemand();
			Angle = newAngle;
			DrawTurtle();
			PaintAndDelay();
		}

		#endregion Public Methods

		#region Private Fields

		private static float m_Angle;
		private static int m_Delay;
		private static Control m_DrawControl;
		private static Graphics m_DrawGraphics;
		private static Image m_DrawImage;
		private static Pen m_DrawPen;
		private static bool m_PenVisible;
		private static PictureBox m_TurtleHeadImage;
		private static float m_X;
		private static float m_Y;

		#endregion Private Fields

		#region Private Methods

		private static void DrawControl_ClientSizeChanged(object sender, EventArgs e)
		{
			m_DrawControl.Invalidate();
			DrawTurtle();
		}

		private static void DrawControl_Paint(object sender, PaintEventArgs e)
		{
			if (m_DrawControl != null)
			{
				var top = (m_DrawControl.ClientSize.Width - DRAW_AREA_SIZE) / 2;
				var left = (m_DrawControl.ClientSize.Height - DRAW_AREA_SIZE) / 2;
				// TODO: needs a fix -> does not work correctly when drawControl has AutoScroll
				e.Graphics.DrawImage(m_DrawImage, top, left);
			}
		}

		private static void DrawTurtle()
		{
			if (ShowTurtle)
			{
				var turtleImg = Resources.Turtle;
				turtleImg = RotateImage(turtleImg, m_Angle);

				m_TurtleHeadImage.BackgroundImage = turtleImg;
				m_TurtleHeadImage.Width = turtleImg.Width;
				m_TurtleHeadImage.Height = turtleImg.Height;

				var turtleX = 1 + m_DrawControl.ClientSize.Width / 2 + X - m_TurtleHeadImage.Width / 2;
				var turtleY = 1 + m_DrawControl.ClientSize.Height / 2 - Y - m_TurtleHeadImage.Height / 2;

				m_TurtleHeadImage.Left = (int)Math.Round(turtleX);
				m_TurtleHeadImage.Top = (int)Math.Round(turtleY);
			}
		}

		private static void InitOnDemand()
		{
			// Create the drawing surface if it does not already exist
			if (m_DrawControl == null)
			{
				Init();
			}
		}

		private static void PaintAndDelay()
		{
			m_DrawControl.Invalidate();

			if (Delay == 0)
			{
				// No delay -> invalidate the control, so it will be repainted later
			}
			else
			{
				// Immediately paint the control and them delay
				m_DrawControl.Update();
				Thread.Sleep(Delay);
				Application.DoEvents();
			}
		}

		private static Bitmap RotateImage(Bitmap bitmap, float angleDegrees)
		{
			Bitmap rotatedImage = new Bitmap(bitmap.Width, bitmap.Height);
			
			using (Graphics g = Graphics.FromImage(rotatedImage))
			{
				// Set the rotation point as the center into the matrix
				g.TranslateTransform(bitmap.Width / 2, bitmap.Height / 2);

				// Rotate
				g.RotateTransform(angleDegrees);

				// Restore the rotation point into the matrix
				g.TranslateTransform(-bitmap.Width / 2, -bitmap.Height / 2);

				// Draw the image on the new bitmap
				g.DrawImage(bitmap, new Point(0, 0));
			}
			
			bitmap.Dispose();

			return rotatedImage;
		}

		private static void SetDoubleBuffered(Control control)
		{
			// set instance non-public property with name "DoubleBuffered" to true
			typeof(Control).InvokeMember("DoubleBuffered",
				BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
				null, control, new object[] { true });
		}

		#endregion Private Methods

	}
}
