/*
 *  $Id: IndividualListDialog.cs 183 2008-06-08 15:31:15Z davek $
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

using Gedcom;

namespace Gedcom.UI.GTK
{
	
	
	public partial class IndividualListDialog : Gtk.Dialog
	{
		#region Variables
				
		#endregion
		
		#region Constructors
		
		public IndividualListDialog()
		{
			this.Build();
		}
		
		#endregion
		
		#region EventHandlers
	
		#endregion
		
		#region Properties
		
		public GedcomDatabase Database
		{
			get { return IndividualList.Database; }
			set { IndividualList.Database = value; }
		}
		
		public GedcomRecord Record
		{
			get { return IndividualList.Record; }
			set { IndividualList.Record = value; }
		}
		
		public Gedcom.UI.GTK.Widgets.IndividualList List
		{
			get { return IndividualList; }
		}
		
		#endregion
	}
}
