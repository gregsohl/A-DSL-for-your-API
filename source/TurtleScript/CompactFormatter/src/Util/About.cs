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
 * $Id: About.cs 14 2004-08-26 09:08:59Z Angelo $
 * */
#endregion

using System;

namespace CompactFormatter.Util
{
	/// <summary>
	/// A Simple class used to keep track of package version.
	/// </summary>
	public class About
	{
		/// <summary>
		/// Date of last modify at the package.
		/// </summary>
		private static DateTime Date=new DateTime(2004,8,21);

		/// <summary>
		/// Represents the codename of the project.
		/// </summary>
		private const String codename = "GeNova";
		/// <summary>
		/// Major version number.
		/// </summary>
		private const Int32 Major = 1;
		
		/// <summary>
		/// Minor version number.
		/// </summary>
		private const Int32 Minor = 0;

		/// <summary>
		/// Build version number.
		/// </summary>
		private const Int32 Build = 0;

		/// <summary>
		/// String containing the name of the project
		/// </summary>
		private const String Name = "CompactFormatter";


		/// <summary>
		/// returns a string representing the Peerware Version in the format:
		/// MAJOR.MINOR.BUILD
		/// </summary>
		public static String Version
		{
			get
			{
				return Major+"."+Minor+"."+Build;
			}
		}

		/// <summary>
		/// a string containing the newline character sequence
		/// </summary>
		private const string NewLine="\r\n";

		/// <summary>
		/// Returns a string containing all information about the currently used version of Project.
		/// </summary>
		public static String AboutString
		{
			get
			{
				String about=Name+" V"+Version+NewLine+"Codename:"+codename+NewLine+"Modified:"+Date.ToString("d")+NewLine;
				return about;
			}
		}

#if DEBUG
		internal static Debug debug = new Debug(DebugLevel.NONE,
			About.Name,new Write(Debug.WriteScreen));
#endif
	}
}
