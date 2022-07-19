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
 * $Id$
 * */
#endregion

using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace CompactFormatter.Surrogate
{
	/// <summary>
	/// Summary description for DataSetOverrider.
	/// </summary>
	[Attributes.Overrider(typeof(System.Data.DataSet))]
	public class DataSetOverrider : Interfaces.IOverrider
	{
		#region IOverrider Members


		public void Serialize(CompactFormatter parent, System.IO.Stream serializationStream, object graph)
		{
			// Pull out the data set we are serializing.
			DataSet ds = (DataSet)graph;

			// Serialize the version in case we make changes.
			CustomSerializationHelper.Serialize(serializationStream, VERSION);

			// Serialize the top-level properties of the dataset.
			parent.Serialize(serializationStream,ds.DataSetName);
			parent.Serialize(serializationStream,ds.Namespace);
			parent.Serialize(serializationStream,ds.Prefix);
			parent.Serialize(serializationStream,ds.CaseSensitive);
			//parent.Serialize(serializationStream,ds.Locale);
			parent.Serialize(serializationStream,ds.EnforceConstraints);

			parent.Serialize(serializationStream,(DataTable[])new ArrayList(ds.Tables).ToArray(typeof(DataTable)));

			//ForeignKeyConstraints
			parent.Serialize(serializationStream,GetForeignKeyConstraints(ds));

			//Relations
			parent.Serialize(serializationStream,GetRelations(ds));			

			//ExtendedProperties
			parent.Serialize(serializationStream,new ArrayList(ds.ExtendedProperties.Keys));
			parent.Serialize(serializationStream,new ArrayList(ds.ExtendedProperties.Values));

		}

		/*
		Gets foreignkey constraints availabe on the tables in the dataset.
		***Serialized foreign key constraints format : [constraintName]->[parentTableIndex, parentcolumnIndexes]->[childTableIndex, childColumnIndexes]->[AcceptRejectRule, UpdateRule, Delete]->[extendedProperties]***
	*/        
		private ArrayList GetForeignKeyConstraints(DataSet ds) 
		{
			Debug.Assert(ds != null);
        
			ArrayList constraintList = new ArrayList();        
			for (int i = 0; i < ds.Tables.Count; i++) 
			{
				DataTable dt = ds.Tables[i];
				for (int j = 0; j < dt.Constraints.Count; j++) 
				{
					Constraint c = dt.Constraints[j];
					ForeignKeyConstraint fk = c as ForeignKeyConstraint;
					if (fk != null) 
					{
						string constraintName = c.ConstraintName;
						int[] parentInfo = new int[fk.RelatedColumns.Length + 1];
						parentInfo[0] = ds.Tables.IndexOf(fk.RelatedTable);
						for (int k = 1; k < parentInfo.Length; k++) 
						{
							parentInfo[k] = fk.RelatedColumns[k - 1].Ordinal;
						}
                    
						int[] childInfo = new int[fk.Columns.Length + 1];
						childInfo[0] = i;//Since the constraint is on the current table, this is the child table.
						for (int k = 1; k < childInfo.Length; k++) 
						{
							childInfo[k] = fk.Columns[k - 1].Ordinal;
						}
                    
						ArrayList list = new ArrayList();
						list.Add(constraintName);
						list.Add(parentInfo);
						list.Add(childInfo);                    
						list.Add(new int[] { (int) fk.AcceptRejectRule, (int) fk.UpdateRule, (int) fk.DeleteRule });
						Hashtable extendedProperties = new Hashtable();
						if (fk.ExtendedProperties.Keys.Count > 0) 
						{
							foreach (object propertyKey in fk.ExtendedProperties.Keys) 
							{
								extendedProperties.Add(propertyKey, fk.ExtendedProperties[propertyKey]);
							}
						}                    
						list.Add(extendedProperties);
                    
						constraintList.Add(list);
					}
				}
			}
			return constraintList;
		}
    
		/*
			Adds foreignkey constraints to the tables in the dataset. The arraylist contains the serialized format of the foreignkey constraints.
			***Deserialize the foreign key constraints format : [constraintName]->[parentTableIndex, parentcolumnIndexes]->[childTableIndex, childColumnIndexes]->[AcceptRejectRule, UpdateRule, Delete]->[extendedProperties]***
		*/    
		private void SetForeignKeyConstraints(DataSet ds, ArrayList constraintList) 
		{
			Debug.Assert(ds != null);
			Debug.Assert(constraintList != null);
        
			foreach (ArrayList list in constraintList) 
			{
				Debug.Assert(list.Count == 5);
				string constraintName = (string) list[0];            
				int[] parentInfo = (int[]) list[1];
				int[] childInfo = (int[]) list[2];
				int[] rules = (int[]) list[3];            
				Hashtable extendedProperties = (Hashtable) list[4];
            
				//ParentKey Columns.
				Debug.Assert(parentInfo.Length >= 1);
				DataColumn[] parentkeyColumns = new DataColumn[parentInfo.Length - 1];
				for (int i = 0; i < parentkeyColumns.Length; i++) 
				{
					Debug.Assert(ds.Tables.Count > parentInfo[0]);
					Debug.Assert(ds.Tables[parentInfo[0]].Columns.Count > parentInfo[i + 1]);
					parentkeyColumns[i] = ds.Tables[parentInfo[0]].Columns[parentInfo[i + 1]];
				}
            
				//ChildKey Columns.
				Debug.Assert(childInfo.Length >= 1);
				DataColumn[] childkeyColumns = new DataColumn[childInfo.Length - 1];
				for (int i = 0; i < childkeyColumns.Length; i++) 
				{
					Debug.Assert(ds.Tables.Count > childInfo[0]);
					Debug.Assert(ds.Tables[childInfo[0]].Columns.Count > childInfo[i + 1]);                
					childkeyColumns[i] = ds.Tables[childInfo[0]].Columns[childInfo[i + 1]];
				}
            
				//Create the Constraint.
				ForeignKeyConstraint fk = new ForeignKeyConstraint(constraintName, parentkeyColumns, childkeyColumns);
				Debug.Assert(rules.Length == 3);
				fk.AcceptRejectRule = (AcceptRejectRule) rules[0];
				fk.UpdateRule = (Rule) rules[1];
				fk.DeleteRule = (Rule) rules[2];
            
				//Extended Properties.
				Debug.Assert(extendedProperties != null);
				if (extendedProperties.Keys.Count > 0) 
				{                
					foreach (object propertyKey in extendedProperties.Keys) 
					{
						fk.ExtendedProperties.Add(propertyKey, extendedProperties[propertyKey]);
					}
				}
            
				//Add the constraint to the child datatable.
				Debug.Assert(ds.Tables.Count > childInfo[0]);
				ds.Tables[childInfo[0]].Constraints.Add(fk);            
			}
		}    
    
		/*
			Gets relations from the dataset.
			***Serialized relations format : [relationName]->[parentTableIndex, parentcolumnIndexes]->[childTableIndex, childColumnIndexes]->[Nested]->[extendedProperties]***
		*/                
		private ArrayList GetRelations(DataSet ds) 
		{
			Debug.Assert(ds != null);
        
			ArrayList relationList = new ArrayList();
			foreach (DataRelation rel in ds.Relations) 
			{
				string relationName = rel.RelationName;
				int[] parentInfo = new int[rel.ParentColumns.Length + 1];
				parentInfo[0] = ds.Tables.IndexOf(rel.ParentTable);
				for (int j = 1; j < parentInfo.Length; j++) 
				{
					parentInfo[j] = rel.ParentColumns[j - 1].Ordinal;
				}
                
				int[] childInfo = new int[rel.ChildColumns.Length + 1];
				childInfo[0] = ds.Tables.IndexOf(rel.ChildTable);
				for (int j = 1; j < childInfo.Length; j++) 
				{
					childInfo[j] = rel.ChildColumns[j - 1].Ordinal;
				}
                
				ArrayList list = new ArrayList();
				list.Add(relationName);
				list.Add(parentInfo);
				list.Add(childInfo);
				list.Add(rel.Nested);
				Hashtable extendedProperties = new Hashtable();
				if (rel.ExtendedProperties.Keys.Count > 0) 
				{
					foreach (object propertyKey in rel.ExtendedProperties.Keys) 
					{
						extendedProperties.Add(propertyKey, rel.ExtendedProperties[propertyKey]);
					}
				}                    
				list.Add(extendedProperties);            
                
				relationList.Add(list);
			}
			return relationList;
		}
    
		/*
			Adds relations to the dataset. The arraylist contains the serialized format of the relations.
			***Deserialize the relations format : [relationName]->[parentTableIndex, parentcolumnIndexes]->[childTableIndex, childColumnIndexes]->[Nested]->[extendedProperties]***
		*/
		private void SetRelations(DataSet ds, ArrayList relationList) 
		{
			Debug.Assert(ds != null);
			Debug.Assert(relationList != null);
        
			foreach (ArrayList list in relationList) 
			{
				Debug.Assert(list.Count == 5);
				string relationName = (string) list[0];
				int[] parentInfo = (int[]) list[1];
				int[] childInfo = (int[]) list[2];
				bool isNested = (bool) list[3];                
				Hashtable extendedProperties = (Hashtable) list[4];
            
				//ParentKey Columns.
				Debug.Assert(parentInfo.Length >= 1);
				DataColumn[] parentkeyColumns = new DataColumn[parentInfo.Length - 1];
				for (int i = 0; i < parentkeyColumns.Length; i++) 
				{
					Debug.Assert(ds.Tables.Count > parentInfo[0]);
					Debug.Assert(ds.Tables[parentInfo[0]].Columns.Count > parentInfo[i + 1]);                
					parentkeyColumns[i] = ds.Tables[parentInfo[0]].Columns[parentInfo[i + 1]];
				}
            
				//ChildKey Columns.
				Debug.Assert(childInfo.Length >= 1);
				DataColumn[] childkeyColumns = new DataColumn[childInfo.Length - 1];
				for (int i = 0; i < childkeyColumns.Length; i++) 
				{
					Debug.Assert(ds.Tables.Count > childInfo[0]);
					Debug.Assert(ds.Tables[childInfo[0]].Columns.Count > childInfo[i + 1]);                                
					childkeyColumns[i] = ds.Tables[childInfo[0]].Columns[childInfo[i + 1]];
				}
            
				//Create the Relation, without any constraints[Assumption: The constraints are added earlier than the relations]
				DataRelation rel = new DataRelation(relationName, parentkeyColumns, childkeyColumns, false);
				rel.Nested = isNested;
            
				//Extended Properties.
				Debug.Assert(extendedProperties != null);
				if (extendedProperties.Keys.Count > 0) 
				{
					foreach (object propertyKey in extendedProperties.Keys) 
					{
						rel.ExtendedProperties.Add(propertyKey, extendedProperties[propertyKey]);
					}
				}
            
				//Add the relations to the dataset.
				ds.Relations.Add(rel);            
			}
		}

		public object Deserialize(CompactFormatter parent, System.IO.Stream serializationStream)
		{
			// Get the serialization version, if needed.
			CustomSerializationHelper.DeserializeInt32(serializationStream);

			// Reconstruct the dataset.
			DataSet ds = new DataSet((String)parent.Deserialize(serializationStream));
			ds.Namespace = (String)parent.Deserialize(serializationStream);
			ds.Prefix = (String)parent.Deserialize(serializationStream);
			ds.CaseSensitive = (bool)parent.Deserialize(serializationStream);
			//parent.Serialize(serializationStream,ds.Locale);
			ds.EnforceConstraints = (bool)parent.Deserialize(serializationStream);

			DataTable[] datatables = (DataTable[])parent.Deserialize(serializationStream);
			for(int i = 0; i < datatables.Length; i++)
				ds.Tables.Add(datatables[i]);

			//ForeignKeyConstraints
			ArrayList Constraints = (ArrayList)parent.Deserialize(serializationStream);
			SetForeignKeyConstraints(ds,Constraints);

			//Relations
			ArrayList Relations = (ArrayList)parent.Deserialize(serializationStream);
			SetRelations(ds,Relations);

			//ExtendedProperties
			ArrayList keys = (ArrayList)parent.Deserialize(serializationStream);
			ArrayList values = (ArrayList)parent.Deserialize(serializationStream);
		
			for(int i = 0; i < keys.Count; i++)
			{
				ds.ExtendedProperties.Add(keys[i],values[i]);
			}
			return ds;
		}

		#endregion

		#region Private Constants

		private const int VERSION = 0;

		#endregion Private Constants
	}
}
