// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Gedcom.UI.GTK.Widgets {
    
    
    public partial class FamilyMoreView {
        
        private Gtk.Notebook Notebook;
        
        private Gtk.Table table3;
        
        private Gedcom.UI.GTK.Widgets.FactView FactView;
        
        private Gtk.HSeparator hseparator4;
        
        private Gedcom.UI.GTK.Widgets.MarriageView MarriageView;
        
        private Gtk.Label label1;
        
        private Gedcom.UI.GTK.Widgets.AddressView AddressView;
        
        private Gtk.Label label2;
        
        private Gedcom.UI.GTK.Widgets.NotesView NotesView;
        
        private Gtk.Label label3;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget Gedcom.UI.GTK.Widgets.FamilyMoreView
            Stetic.BinContainer.Attach(this);
            this.Events = ((Gdk.EventMask)(256));
            this.Name = "Gedcom.UI.GTK.Widgets.FamilyMoreView";
            // Container child Gedcom.UI.GTK.Widgets.FamilyMoreView.Gtk.Container+ContainerChild
            this.Notebook = new Gtk.Notebook();
            this.Notebook.CanFocus = true;
            this.Notebook.Name = "Notebook";
            this.Notebook.CurrentPage = 2;
            this.Notebook.BorderWidth = ((uint)(6));
            // Container child Notebook.Gtk.Notebook+NotebookChild
            this.table3 = new Gtk.Table(((uint)(3)), ((uint)(5)), false);
            this.table3.Name = "table3";
            this.table3.RowSpacing = ((uint)(6));
            this.table3.ColumnSpacing = ((uint)(12));
            this.table3.BorderWidth = ((uint)(6));
            // Container child table3.Gtk.Table+TableChild
            this.FactView = new Gedcom.UI.GTK.Widgets.FactView();
            this.FactView.Events = ((Gdk.EventMask)(256));
            this.FactView.Name = "FactView";
            this.table3.Add(this.FactView);
            Gtk.Table.TableChild w1 = ((Gtk.Table.TableChild)(this.table3[this.FactView]));
            w1.TopAttach = ((uint)(2));
            w1.BottomAttach = ((uint)(3));
            w1.RightAttach = ((uint)(5));
            // Container child table3.Gtk.Table+TableChild
            this.hseparator4 = new Gtk.HSeparator();
            this.hseparator4.Name = "hseparator4";
            this.table3.Add(this.hseparator4);
            Gtk.Table.TableChild w2 = ((Gtk.Table.TableChild)(this.table3[this.hseparator4]));
            w2.TopAttach = ((uint)(1));
            w2.BottomAttach = ((uint)(2));
            w2.RightAttach = ((uint)(5));
            w2.XOptions = ((Gtk.AttachOptions)(4));
            w2.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table3.Gtk.Table+TableChild
            this.MarriageView = new Gedcom.UI.GTK.Widgets.MarriageView();
            this.MarriageView.Events = ((Gdk.EventMask)(256));
            this.MarriageView.Name = "MarriageView";
            this.table3.Add(this.MarriageView);
            Gtk.Table.TableChild w3 = ((Gtk.Table.TableChild)(this.table3[this.MarriageView]));
            w3.RightAttach = ((uint)(5));
            w3.XOptions = ((Gtk.AttachOptions)(4));
            w3.YOptions = ((Gtk.AttachOptions)(4));
            this.Notebook.Add(this.table3);
            // Notebook tab
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.LabelProp = "Facts";
            this.Notebook.SetTabLabel(this.table3, this.label1);
            this.label1.ShowAll();
            // Container child Notebook.Gtk.Notebook+NotebookChild
            this.AddressView = new Gedcom.UI.GTK.Widgets.AddressView();
            this.AddressView.Events = ((Gdk.EventMask)(256));
            this.AddressView.Name = "AddressView";
            this.Notebook.Add(this.AddressView);
            Gtk.Notebook.NotebookChild w5 = ((Gtk.Notebook.NotebookChild)(this.Notebook[this.AddressView]));
            w5.Position = 1;
            // Notebook tab
            this.label2 = new Gtk.Label();
            this.label2.Name = "label2";
            this.label2.LabelProp = "Addresses";
            this.Notebook.SetTabLabel(this.AddressView, this.label2);
            this.label2.ShowAll();
            // Container child Notebook.Gtk.Notebook+NotebookChild
            this.NotesView = new Gedcom.UI.GTK.Widgets.NotesView();
            this.NotesView.Events = ((Gdk.EventMask)(256));
            this.NotesView.Name = "NotesView";
            this.NotesView.DataNotes = false;
            this.NotesView.ListOnly = false;
            this.NotesView.NoteOnly = false;
            this.Notebook.Add(this.NotesView);
            Gtk.Notebook.NotebookChild w6 = ((Gtk.Notebook.NotebookChild)(this.Notebook[this.NotesView]));
            w6.Position = 2;
            // Notebook tab
            this.label3 = new Gtk.Label();
            this.label3.Name = "label3";
            this.label3.LabelProp = "Notes";
            this.Notebook.SetTabLabel(this.NotesView, this.label3);
            this.label3.ShowAll();
            this.Add(this.Notebook);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Show();
            this.FactView.ShowSourceCitation += new System.EventHandler<Gedcom.UI.Common.SourceCitationArgs>(this.OnFactView_ShowSourceCitation);
            this.FactView.EventRemoved += new System.EventHandler(this.OnFactView_EventRemoved);
            this.FactView.EventAdded += new System.EventHandler(this.OnFactView_EventAdded);
            this.FactView.ShowScrapBook += new System.EventHandler<Gedcom.UI.Common.ScrapBookArgs>(this.OnFactView_ShowScrapBook);
            this.FactView.MoreInformation += new System.EventHandler<Gedcom.UI.Common.FactArgs>(this.OnFactView_MoreInformation);
            this.AddressView.ShowSourceCitation += new System.EventHandler<Gedcom.UI.Common.SourceCitationArgs>(this.OnAddressView_ShowSourceCitation);
            this.AddressView.ShowScrapBook += new System.EventHandler<Gedcom.UI.Common.ScrapBookArgs>(this.OnAddressView_ShowScrapBook);
            this.AddressView.MoreFactInformation += new System.EventHandler<Gedcom.UI.Common.FactArgs>(this.OnAddressView_MoreFactInformation);
            this.NotesView.ShowSourceCitation += new System.EventHandler<Gedcom.UI.Common.SourceCitationArgs>(this.OnNotesView_ShowSourceCitation);
            this.NotesView.SelectNewNote += new System.EventHandler<Gedcom.UI.Common.NoteArgs>(this.OnNotesView_SelectNewNote);
        }
    }
}
