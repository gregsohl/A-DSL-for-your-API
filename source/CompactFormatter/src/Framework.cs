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
 * $Id: Framework.cs 1 2004-08-13 18:29:52Z Angelo $
 * */
#endregion

using System;

namespace CompactFormatter
{

	/// <summary>
	/// All possible platform under which CFormatter could be launched.
	/// <remarks>Really speaking actually i'm just interested in differentiate 
	/// between CompactFramework and other Frameworks</remarks>
	/// </summary>
	public enum FrameworkVersion{NETCF10, NET10, NET11, MONO, DOTGNU, SSCLI, UNKNOWN}
	/// <summary>
	/// This class is used to detect Framework actually running under CFormatter.
	/// </summary>
	public class Framework
	{

		/// <summary>
		/// A single static method is the core of Framework class.
		/// This Detect method is used to detect which implementation is being used
		/// by CompactFormatter
		/// </summary>
		/// <returns>A FrameworkVersion enum representing the implementation used
		/// </returns>
		public static FrameworkVersion Detect()
		{
			OperatingSystem os = Environment.OSVersion;

			/* This is uncorrect on the long run:
			 * Actually Platform is simply telling us that the OS is Unix but
			 * this value (128) is defined only under Mono.
			 * In fact this code will not detect Mono under windows and, if dotGNU
			 * uses the same code (128), this code will also uncorrectly identify
			 * dotGNU as MONO.
			 * As already said this is ok because we're particularly interested 
			 * in differentiate between NETCF and other implementations.
			 * */
			if ((int)os.Platform == 128) 
				return FrameworkVersion.MONO;

			if (os.Platform == PlatformID.WinCE && os.Version.Revision == -1)
				return FrameworkVersion.NETCF10;

			if (Environment.Version.Major == 1)
			{
				if(Environment.Version.Minor == 1)
					return FrameworkVersion.NET11;
				else if (Environment.Version.Minor == 0)
					return FrameworkVersion.NET10;
			}

			

			return FrameworkVersion.UNKNOWN;
		}
	}
}
