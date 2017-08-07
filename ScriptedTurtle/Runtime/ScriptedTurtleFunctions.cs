#region Namespaces

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

using Nakov.TurtleGraphics;
using TurtleScript.Interpreter;

#endregion Namespaces

namespace ScriptedTurtle.Runtime
{
	public class ScriptedTurtleFunctions : ITurtleScriptRuntime
	{
		public ScriptedTurtleFunctions()
		{
			m_Functions = new Dictionary<string, TurtleScriptRuntimeFunction>();

			m_Functions.Add("forward_1", new TurtleScriptRuntimeFunction(Forward, 1));
			m_Functions.Add("backward_1", new TurtleScriptRuntimeFunction(Backward, 1));
			m_Functions.Add("moveto_2", new TurtleScriptRuntimeFunction(MoveTo, 2));
			m_Functions.Add("rotate_1", new TurtleScriptRuntimeFunction(Rotate, 1));
			m_Functions.Add("rotateto_1", new TurtleScriptRuntimeFunction(RotateTo, 1));
			m_Functions.Add("up_0", new TurtleScriptRuntimeFunction(PenUp, 0));
			m_Functions.Add("down_0", new TurtleScriptRuntimeFunction(PenDown, 0));
			m_Functions.Add("color_1", new TurtleScriptRuntimeFunction(PenColor, 1));
			m_Functions.Add("color_3", new TurtleScriptRuntimeFunction(PenColor, 3));
			m_Functions.Add("size_1", new TurtleScriptRuntimeFunction(PenSize, 1));
			m_Functions.Add("clear_0", new TurtleScriptRuntimeFunction(Clear, 0));
			m_Functions.Add("showturtle_0", new TurtleScriptRuntimeFunction(ToggleTurtle, 0));
			m_Functions.Add("x_0", new TurtleScriptRuntimeFunction(CurrentX, 0));
			m_Functions.Add("y_0", new TurtleScriptRuntimeFunction(CurrentY, 0));
			m_Functions.Add("angle_0", new TurtleScriptRuntimeFunction(CurrentAngle, 0));
			m_Functions.Add("delay_1", new TurtleScriptRuntimeFunction(Delay, 1));
			m_Functions.Add("pause_1", new TurtleScriptRuntimeFunction(Pause, 1));
		}

		public string Namespace
		{
			get { return "t"; }
		}

		public Dictionary<string, TurtleScriptRuntimeFunction> Functions
		{
			get { return m_Functions; }
		}

		public TurtleScriptValue Forward(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 1)
			{
				double distance = parameters[0].NumericValue;

				Turtle.Forward(distance);
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue Backward(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 1)
			{
				double distance = parameters[0].NumericValue;

				Turtle.Backward(distance);
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue MoveTo(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 2)
			{
				double x = parameters[0].NumericValue;
				double y = parameters[1].NumericValue;

				Turtle.MoveTo(x, y);
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue Rotate(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 1)
			{
				double angle = parameters[0].NumericValue;

				Turtle.Rotate(angle);
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue RotateTo(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 1)
			{
				double angle = parameters[0].NumericValue;

				Turtle.RotateTo(angle);
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue PenUp(List<TurtleScriptValue> parameters)
		{
			Turtle.PenUp();
			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue PenDown(List<TurtleScriptValue> parameters)
		{
			Turtle.PenDown();
			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue PenColor(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 1)
			{
				int penColor = (int)parameters[0].NumericValue;

				switch (penColor)
				{
					case 1:
						Turtle.PenColor = Color.Red;
						break;
					case 2:
						Turtle.PenColor = Color.Green;
						break;
					case 3:
						Turtle.PenColor = Color.Blue;
						break;
				}
			}
			else
			{
				if (parameters.Count == 3)
				{
					int red = (int) parameters[0].NumericValue;
					int green = (int) parameters[1].NumericValue;
					int blue = (int) parameters[2].NumericValue;

					Turtle.PenColor = Color.FromArgb(red, green, blue);
				}
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue PenSize(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 1)
			{
				double penSize = parameters[0].NumericValue;

				Turtle.PenSize = penSize;
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue Clear(List<TurtleScriptValue> parameters)
		{
			Turtle.Reset();
			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue ToggleTurtle(List<TurtleScriptValue> parameters)
		{
			Turtle.ShowTurtle = !Turtle.ShowTurtle;
			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue CurrentX(List<TurtleScriptValue> parameters)
		{
			TurtleScriptValue returnValue = new TurtleScriptValue(Turtle.X);
			return returnValue;
		}

		public TurtleScriptValue CurrentY(List<TurtleScriptValue> parameters)
		{
			TurtleScriptValue returnValue = new TurtleScriptValue(Turtle.Y);
			return returnValue;
		}

		public TurtleScriptValue CurrentAngle(List<TurtleScriptValue> parameters)
		{
			TurtleScriptValue returnValue = new TurtleScriptValue(Turtle.Angle);
			return returnValue;
		}

		public TurtleScriptValue Delay(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 1)
			{
				int delay = (int) parameters[0].NumericValue;

				Turtle.Delay = delay;
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue Pause(List<TurtleScriptValue> parameters)
		{
			Turtle.Refresh();

			if (parameters.Count == 1)
			{
				int milliseconds = (int)parameters[0].NumericValue;

				Thread.Sleep(milliseconds);
			}

			return TurtleScriptValue.VOID;
		}

		#region Private Fields

		private Dictionary<string, TurtleScriptRuntimeFunction> m_Functions;

		#endregion Private Fields

	}
}
