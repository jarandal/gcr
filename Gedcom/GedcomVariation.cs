/*
 *  $Id: GedcomVariation.cs 183 2008-06-08 15:31:15Z davek $
 * 
 *  Copyright (C) 2007 David A Knight <david@ritter.demon.co.uk>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA
 *
 */

using System;

namespace Gedcom
{
	
	
	public class GedcomVariation
	{
		#region Variables
		
		private GedcomDatabase _database;
		
		private string _Value;
		private string _VariationType;
		
		GedcomChangeDate _ChangeDate;
		
		// FIXME: at least for GedcomName variations we need to support
		// personal name pieces here
		
		protected object _Data;
		
		#endregion
		
		#region Constructors
		
		public GedcomVariation()
		{
		}
		
		#endregion
		
		#region Properties
		
		public GedcomDatabase Database
		{
			get { return _database; }
			set { _database = value; }
		}
		
		public string Value
		{
			get { return _Value; }
			set 
			{
				if (value != _Value)
				{
					_Value = value; 
					Changed();
				}
			}
		}
		
		public string VariationType
		{
			get { return _VariationType; }
			set 
			{ 
				if (value != _VariationType)
				{
					_VariationType = value; 
					Changed();
				}
			}
		}
				
		public GedcomChangeDate ChangeDate
		{
			get { return _ChangeDate; }
			set { _ChangeDate = value; }
		}
		
		#endregion
		
		#region Methods
				
		protected virtual void Changed()
		{
			if (_database == null)
			{
//				System.Console.WriteLine("Changed() called on record with no database set");
//				
//				System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace();
//				foreach (System.Diagnostics.StackFrame f in trace.GetFrames())
//				{
//					System.Console.WriteLine(f);
//				}
			}
			else if (!_database.Loading)
			{
				if (_ChangeDate == null)
				{
					_ChangeDate = new GedcomChangeDate(_database);
					// FIXME: what level?
				}
				DateTime now = DateTime.Now;
				
				_ChangeDate.Date1 = now.ToString("dd MMM yyyy");
				_ChangeDate.Time = now.ToString("hh:mm:ss");
			}
		}
		
		#endregion
	}
}
