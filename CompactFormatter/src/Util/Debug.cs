#region LGPL License
/* 
 * CompactFormatter: A generic formatter for the .NET Compact Framework
 * Copyright (C) 2004  Angelo Scotto (scotto_a@hotmail.com)
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * $Id: Debug.cs 1 2004-08-13 18:29:52Z Angelo $
 * */
#endregion

using System;
using System.Diagnostics;
using System.Text;

// The whole class simply doesn't exists if i'm not building the debug version
#if DEBUG		

namespace CompactFormatter.Util
{
	internal delegate void Write(String msg);
	internal enum DebugLevel {NONE,ERROR,INFO,VERBOSE};

	/// <summary>
	/// Small class used to track debug messages.
	/// </summary>
	public class Debug
	{
		[Conditional("DEBUG")]
		internal void WriteDebug(DebugLevel requestedLevel, string message)
		{
			if (level >= requestedLevel)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(application);
				sb.Append(":");
				sb.Append(message);
				sb.Append(NewLine);

				write(sb.ToString());
			}
		}

		private DebugLevel level;
		private String application;
		private Write write;

		private const String NewLine = "\r\n";

		internal Debug(DebugLevel level,String application,Write write)
		{
			this.level = level;
			this.application = application;
			this.write = write;
		}

		/* Write Delegates */
		public static void WriteScreen(String msg)
		{Console.Write(msg);}
	}
}
#endif
