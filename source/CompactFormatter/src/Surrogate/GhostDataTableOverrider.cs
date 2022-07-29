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
 * $Id: GhostDataSetOverrider.cs 1 2004-08-13 18:29:52Z Angelo $
 * */
#endregion

using System;
using System.Data;
using System.Collections;

namespace CompactFormatter.Surrogate
{
	/// <summary>
	/// Summary description for DataTableOverrider.
	/// </summary>
	[Attributes.Overrider(typeof(System.Data.DataTable))]
	public class GhostDataTableOverrider : Interfaces.IOverrider
	{
		#region ICFormatter Members

		public void Serialize(CompactFormatter parent, System.IO.Stream serializationStream, object graph)
		{
			// Serialize the version for future expansion.
			CustomSerializationHelper.Serialize(serializationStream, VERSION);

			ArrayList colNames = new ArrayList();
			ArrayList colTypes = new ArrayList();
			ArrayList colDateTimeModes = new ArrayList();
			ArrayList dataRows = new ArrayList();

			DataTable dt = (DataTable)graph;
			parent.Serialize(serializationStream, dt.Namespace);
			parent.Serialize(serializationStream, dt.Prefix);
			parent.Serialize(serializationStream, dt.TableName);

			foreach(DataColumn col in dt.Columns) 
			{
				colNames.Add(col.ColumnName); 
				colTypes.Add(col.DataType.FullName);
				colDateTimeModes.Add(col.DateTimeMode);
			}

			foreach(DataRow row in dt.Rows)
				dataRows.Add(row.ItemArray);

			// Now i've to serialize three ArrayList using the CompactFormatter main routines
			parent.Serialize(serializationStream, colNames);
			parent.Serialize(serializationStream, colTypes);
			parent.Serialize(serializationStream, colDateTimeModes);
			parent.Serialize(serializationStream, dataRows);
		}

		public object Deserialize(CompactFormatter parent, System.IO.Stream serializationStream)
		{
			// Pull out the deserialization version.
			CustomSerializationHelper.DeserializeInt32(serializationStream);

			DataTable dt = new DataTable();

			dt.Namespace = (string)parent.Deserialize(serializationStream);
			dt.Prefix = (string)parent.Deserialize(serializationStream);
			dt.TableName = (string)parent.Deserialize(serializationStream);

			ArrayList colNames = (ArrayList)parent.Deserialize(serializationStream);
			ArrayList colTypes = (ArrayList)parent.Deserialize(serializationStream);
			ArrayList colDateTimeModes = (ArrayList)parent.Deserialize(serializationStream);
			ArrayList dataRows = (ArrayList)parent.Deserialize(serializationStream);

			// Add columns
			for(int i=0; i<colNames.Count; i++)
			{
				// Create the data column from the name and data type.
				Type dataType = Type.GetType(colTypes[i].ToString());
				DataColumn col = new DataColumn(colNames[i].ToString(), dataType);     

				// We can only set the DateTimeMode if the column type is DateTime.
				if (dataType == typeof(DateTime))
				{
					col.DateTimeMode = (DataSetDateTime)colDateTimeModes[i];
				}

				// Add the column to the data table.
				dt.Columns.Add(col);
			}

			// Add rows
			for(int i=0; i<dataRows.Count; i++)
			{
				DataRow row = dt.NewRow();
				row.ItemArray = (Object[])dataRows[i];
				dt.Rows.Add(row);
			}

			dt.AcceptChanges();
			return dt;
		}

		#endregion

		#region Private Constants

		private const int VERSION = 0;

		#endregion Private Constants
	}
}
