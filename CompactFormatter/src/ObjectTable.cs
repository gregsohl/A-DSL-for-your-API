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
 * $Id: ObjectTable.cs 14 2004-08-26 09:08:59Z Angelo $
 * */
#endregion

using System;

namespace CompactFormatter
{
	/// <summary>
	/// Summary description for ObjectTable.
	/// </summary>
	public class ObjectTable : System.Collections.ArrayList
	{
		private const int TOP = 50;
		private const int WIDTH = 5;

		public ObjectTable(int len) : base(len)
		{}

		public int AddPlaceholder()
		{
			return this.Add(null);
		}

		public override int Add(object value)
		{
			if (this.Count >= TOP) this.RemoveRange(0,WIDTH);
			return base.Add(value);
		}


	}
}
