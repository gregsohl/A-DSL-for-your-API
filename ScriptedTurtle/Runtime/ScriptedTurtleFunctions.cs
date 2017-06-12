#region Namespaces

using System;
using System.Collections.Generic;
using System.Drawing;
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

			m_Functions.Add("forward", new TurtleScriptRuntimeFunction(Forward, 1));
			m_Functions.Add("backward", new TurtleScriptRuntimeFunction(Backward, 1));
			m_Functions.Add("moveto", new TurtleScriptRuntimeFunction(MoveTo, 2));
			m_Functions.Add("rotate", new TurtleScriptRuntimeFunction(Rotate, 1));
			m_Functions.Add("rotateto", new TurtleScriptRuntimeFunction(RotateTo, 1));
			m_Functions.Add("up", new TurtleScriptRuntimeFunction(PenUp, 0));
			m_Functions.Add("down", new TurtleScriptRuntimeFunction(PenDown, 0));
			m_Functions.Add("color", new TurtleScriptRuntimeFunction(PenColor, 1));
			m_Functions.Add("size", new TurtleScriptRuntimeFunction(PenSize, 1));
			m_Functions.Add("clear", new TurtleScriptRuntimeFunction(Clear, 0));
			m_Functions.Add("showturtle", new TurtleScriptRuntimeFunction(ToggleTurtle, 0));
			m_Functions.Add("x", new TurtleScriptRuntimeFunction(CurrentX, 0));
			m_Functions.Add("y", new TurtleScriptRuntimeFunction(CurrentY, 0));
			m_Functions.Add("angle", new TurtleScriptRuntimeFunction(CurrentAngle, 0));
			m_Functions.Add("delay", new TurtleScriptRuntimeFunction(Delay, 1));
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
				float distance = parameters[0].NumericValue;

				Turtle.Forward(distance);
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue Backward(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 1)
			{
				float distance = parameters[0].NumericValue;

				Turtle.Backward(distance);
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue MoveTo(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 2)
			{
				float x = parameters[0].NumericValue;
				float y = parameters[1].NumericValue;

				Turtle.MoveTo(x, y);
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue Rotate(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 1)
			{
				float angle = parameters[0].NumericValue;

				Turtle.Rotate(angle);
			}

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue RotateTo(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 1)
			{
				float angle = parameters[0].NumericValue;

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

			return TurtleScriptValue.VOID;
		}

		public TurtleScriptValue PenSize(List<TurtleScriptValue> parameters)
		{
			if (parameters.Count == 1)
			{
				float penSize = parameters[0].NumericValue;

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

		#region Private Fields

		private Dictionary<string, TurtleScriptRuntimeFunction> m_Functions;

		#endregion Private Fields

	}
}
