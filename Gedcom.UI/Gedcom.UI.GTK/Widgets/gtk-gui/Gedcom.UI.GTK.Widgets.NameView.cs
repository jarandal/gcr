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
    
    
    public partial class NameView {
        
        private Gtk.HBox hbox1;
        
        private Gtk.ScrolledWindow scrolledwindow2;
        
        private Gtk.TreeView NamesTreeView;
        
        private Gtk.Notebook notebook1;
        
        private Gtk.Table table1;
        
        private Gtk.Entry GivenEntry;
        
        private Gtk.HBox hbox2;
        
        private Gtk.Label label1;
        
        private Gtk.Label label2;
        
        private Gtk.Label label3;
        
        private Gtk.Label label4;
        
        private Gtk.Label label5;
        
        private Gtk.Label label6;
        
        private Gtk.Button NameSourceButton;
        
        private Gtk.Image image438;
        
        private Gtk.Entry NicknameEntry;
        
        private Gtk.CheckButton PreferedCheckbox;
        
        private Gtk.ComboBoxEntry PrefixComboBoxEntry;
        
        private Gtk.ComboBoxEntry SuffixComboBoxEntry;
        
        private Gtk.Entry SurnameEntry;
        
        private Gtk.ComboBoxEntry SurnamePrefixComboBoxEntry;
        
        private Gtk.Label label7;
        
        private Gedcom.UI.GTK.Widgets.NotesView NotesView;
        
        private Gtk.Label label8;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget Gedcom.UI.GTK.Widgets.NameView
            Stetic.BinContainer.Attach(this);
            this.Name = "Gedcom.UI.GTK.Widgets.NameView";
            // Container child Gedcom.UI.GTK.Widgets.NameView.Gtk.Container+ContainerChild
            this.hbox1 = new Gtk.HBox();
            this.hbox1.Name = "hbox1";
            this.hbox1.Spacing = 6;
            // Container child hbox1.Gtk.Box+BoxChild
            this.scrolledwindow2 = new Gtk.ScrolledWindow();
            this.scrolledwindow2.CanFocus = true;
            this.scrolledwindow2.Name = "scrolledwindow2";
            this.scrolledwindow2.HscrollbarPolicy = ((Gtk.PolicyType)(2));
            this.scrolledwindow2.ShadowType = ((Gtk.ShadowType)(1));
            // Container child scrolledwindow2.Gtk.Container+ContainerChild
            this.NamesTreeView = new Gtk.TreeView();
            this.NamesTreeView.CanFocus = true;
            this.NamesTreeView.Name = "NamesTreeView";
            this.scrolledwindow2.Add(this.NamesTreeView);
            this.hbox1.Add(this.scrolledwindow2);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.hbox1[this.scrolledwindow2]));
            w2.Position = 0;
            // Container child hbox1.Gtk.Box+BoxChild
            this.notebook1 = new Gtk.Notebook();
            this.notebook1.CanFocus = true;
            this.notebook1.Name = "notebook1";
            this.notebook1.CurrentPage = 0;
            // Container child notebook1.Gtk.Notebook+NotebookChild
            this.table1 = new Gtk.Table(((uint)(7)), ((uint)(4)), false);
            this.table1.Name = "table1";
            this.table1.RowSpacing = ((uint)(6));
            this.table1.ColumnSpacing = ((uint)(6));
            this.table1.BorderWidth = ((uint)(6));
            // Container child table1.Gtk.Table+TableChild
            this.GivenEntry = new Gtk.Entry();
            this.GivenEntry.CanFocus = true;
            this.GivenEntry.Name = "GivenEntry";
            this.GivenEntry.IsEditable = true;
            this.GivenEntry.InvisibleChar = '●';
            this.table1.Add(this.GivenEntry);
            Gtk.Table.TableChild w3 = ((Gtk.Table.TableChild)(this.table1[this.GivenEntry]));
            w3.TopAttach = ((uint)(1));
            w3.BottomAttach = ((uint)(2));
            w3.LeftAttach = ((uint)(1));
            w3.RightAttach = ((uint)(4));
            w3.XOptions = ((Gtk.AttachOptions)(4));
            w3.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.hbox2 = new Gtk.HBox();
            this.hbox2.Name = "hbox2";
            this.hbox2.Spacing = 6;
            this.table1.Add(this.hbox2);
            Gtk.Table.TableChild w4 = ((Gtk.Table.TableChild)(this.table1[this.hbox2]));
            w4.TopAttach = ((uint)(3));
            w4.BottomAttach = ((uint)(4));
            w4.LeftAttach = ((uint)(3));
            w4.RightAttach = ((uint)(4));
            w4.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.Xalign = 1F;
            this.label1.LabelProp = "Nickname:";
            this.table1.Add(this.label1);
            Gtk.Table.TableChild w5 = ((Gtk.Table.TableChild)(this.table1[this.label1]));
            w5.TopAttach = ((uint)(5));
            w5.BottomAttach = ((uint)(6));
            w5.XOptions = ((Gtk.AttachOptions)(4));
            w5.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label2 = new Gtk.Label();
            this.label2.Name = "label2";
            this.label2.Xalign = 1F;
            this.label2.LabelProp = "Prefix:";
            this.table1.Add(this.label2);
            Gtk.Table.TableChild w6 = ((Gtk.Table.TableChild)(this.table1[this.label2]));
            w6.XOptions = ((Gtk.AttachOptions)(4));
            w6.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label3 = new Gtk.Label();
            this.label3.Name = "label3";
            this.label3.Xalign = 1F;
            this.label3.LabelProp = "Given:";
            this.table1.Add(this.label3);
            Gtk.Table.TableChild w7 = ((Gtk.Table.TableChild)(this.table1[this.label3]));
            w7.TopAttach = ((uint)(1));
            w7.BottomAttach = ((uint)(2));
            w7.XOptions = ((Gtk.AttachOptions)(4));
            w7.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label4 = new Gtk.Label();
            this.label4.Name = "label4";
            this.label4.Xalign = 1F;
            this.label4.LabelProp = "Surname Prefix:";
            this.table1.Add(this.label4);
            Gtk.Table.TableChild w8 = ((Gtk.Table.TableChild)(this.table1[this.label4]));
            w8.TopAttach = ((uint)(2));
            w8.BottomAttach = ((uint)(3));
            w8.XOptions = ((Gtk.AttachOptions)(4));
            w8.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label5 = new Gtk.Label();
            this.label5.Name = "label5";
            this.label5.Xalign = 1F;
            this.label5.LabelProp = "Surname:";
            this.table1.Add(this.label5);
            Gtk.Table.TableChild w9 = ((Gtk.Table.TableChild)(this.table1[this.label5]));
            w9.TopAttach = ((uint)(3));
            w9.BottomAttach = ((uint)(4));
            w9.XOptions = ((Gtk.AttachOptions)(4));
            w9.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label6 = new Gtk.Label();
            this.label6.Name = "label6";
            this.label6.Xalign = 1F;
            this.label6.LabelProp = "Suffix:";
            this.table1.Add(this.label6);
            Gtk.Table.TableChild w10 = ((Gtk.Table.TableChild)(this.table1[this.label6]));
            w10.TopAttach = ((uint)(4));
            w10.BottomAttach = ((uint)(5));
            w10.XOptions = ((Gtk.AttachOptions)(4));
            w10.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.NameSourceButton = new Gtk.Button();
            this.NameSourceButton.CanFocus = true;
            this.NameSourceButton.Name = "NameSourceButton";
            // Container child NameSourceButton.Gtk.Container+ContainerChild
            this.image438 = new Gtk.Image();
            this.image438.Name = "image438";
            this.image438.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-index", Gtk.IconSize.Button, 20);
            this.NameSourceButton.Add(this.image438);
            this.NameSourceButton.Label = null;
            this.table1.Add(this.NameSourceButton);
            Gtk.Table.TableChild w12 = ((Gtk.Table.TableChild)(this.table1[this.NameSourceButton]));
            w12.TopAttach = ((uint)(5));
            w12.BottomAttach = ((uint)(6));
            w12.LeftAttach = ((uint)(3));
            w12.RightAttach = ((uint)(4));
            w12.XOptions = ((Gtk.AttachOptions)(4));
            w12.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.NicknameEntry = new Gtk.Entry();
            this.NicknameEntry.CanFocus = true;
            this.NicknameEntry.Name = "NicknameEntry";
            this.NicknameEntry.IsEditable = true;
            this.NicknameEntry.InvisibleChar = '●';
            this.table1.Add(this.NicknameEntry);
            Gtk.Table.TableChild w13 = ((Gtk.Table.TableChild)(this.table1[this.NicknameEntry]));
            w13.TopAttach = ((uint)(5));
            w13.BottomAttach = ((uint)(6));
            w13.LeftAttach = ((uint)(1));
            w13.RightAttach = ((uint)(2));
            w13.XOptions = ((Gtk.AttachOptions)(4));
            w13.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.PreferedCheckbox = new Gtk.CheckButton();
            this.PreferedCheckbox.CanFocus = true;
            this.PreferedCheckbox.Name = "PreferedCheckbox";
            this.PreferedCheckbox.Label = "Use as prefered name";
            this.PreferedCheckbox.DrawIndicator = true;
            this.PreferedCheckbox.UseUnderline = true;
            this.table1.Add(this.PreferedCheckbox);
            Gtk.Table.TableChild w14 = ((Gtk.Table.TableChild)(this.table1[this.PreferedCheckbox]));
            w14.TopAttach = ((uint)(6));
            w14.BottomAttach = ((uint)(7));
            w14.RightAttach = ((uint)(4));
            w14.XOptions = ((Gtk.AttachOptions)(1));
            w14.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.PrefixComboBoxEntry = new Gtk.ComboBoxEntry();
            this.PrefixComboBoxEntry.Name = "PrefixComboBoxEntry";
            this.table1.Add(this.PrefixComboBoxEntry);
            Gtk.Table.TableChild w15 = ((Gtk.Table.TableChild)(this.table1[this.PrefixComboBoxEntry]));
            w15.LeftAttach = ((uint)(1));
            w15.RightAttach = ((uint)(2));
            w15.XOptions = ((Gtk.AttachOptions)(4));
            w15.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.SuffixComboBoxEntry = new Gtk.ComboBoxEntry();
            this.SuffixComboBoxEntry.Name = "SuffixComboBoxEntry";
            this.table1.Add(this.SuffixComboBoxEntry);
            Gtk.Table.TableChild w16 = ((Gtk.Table.TableChild)(this.table1[this.SuffixComboBoxEntry]));
            w16.TopAttach = ((uint)(4));
            w16.BottomAttach = ((uint)(5));
            w16.LeftAttach = ((uint)(1));
            w16.RightAttach = ((uint)(2));
            w16.XOptions = ((Gtk.AttachOptions)(4));
            w16.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.SurnameEntry = new Gtk.Entry();
            this.SurnameEntry.CanFocus = true;
            this.SurnameEntry.Name = "SurnameEntry";
            this.SurnameEntry.IsEditable = true;
            this.SurnameEntry.InvisibleChar = '●';
            this.table1.Add(this.SurnameEntry);
            Gtk.Table.TableChild w17 = ((Gtk.Table.TableChild)(this.table1[this.SurnameEntry]));
            w17.TopAttach = ((uint)(3));
            w17.BottomAttach = ((uint)(4));
            w17.LeftAttach = ((uint)(1));
            w17.RightAttach = ((uint)(4));
            w17.XOptions = ((Gtk.AttachOptions)(4));
            w17.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.SurnamePrefixComboBoxEntry = new Gtk.ComboBoxEntry();
            this.SurnamePrefixComboBoxEntry.Name = "SurnamePrefixComboBoxEntry";
            this.table1.Add(this.SurnamePrefixComboBoxEntry);
            Gtk.Table.TableChild w18 = ((Gtk.Table.TableChild)(this.table1[this.SurnamePrefixComboBoxEntry]));
            w18.TopAttach = ((uint)(2));
            w18.BottomAttach = ((uint)(3));
            w18.LeftAttach = ((uint)(1));
            w18.RightAttach = ((uint)(2));
            w18.XOptions = ((Gtk.AttachOptions)(4));
            w18.YOptions = ((Gtk.AttachOptions)(4));
            this.notebook1.Add(this.table1);
            // Notebook tab
            this.label7 = new Gtk.Label();
            this.label7.Name = "label7";
            this.label7.LabelProp = "Name";
            this.notebook1.SetTabLabel(this.table1, this.label7);
            this.label7.ShowAll();
            // Container child notebook1.Gtk.Notebook+NotebookChild
            this.NotesView = new Gedcom.UI.GTK.Widgets.NotesView();
            this.NotesView.Events = ((Gdk.EventMask)(256));
            this.NotesView.Name = "NotesView";
            this.NotesView.DataNotes = false;
            this.NotesView.ListOnly = false;
            this.NotesView.NoteOnly = false;
            this.notebook1.Add(this.NotesView);
            Gtk.Notebook.NotebookChild w20 = ((Gtk.Notebook.NotebookChild)(this.notebook1[this.NotesView]));
            w20.Position = 1;
            // Notebook tab
            this.label8 = new Gtk.Label();
            this.label8.Name = "label8";
            this.label8.LabelProp = "Notes";
            this.notebook1.SetTabLabel(this.NotesView, this.label8);
            this.label8.ShowAll();
            this.hbox1.Add(this.notebook1);
            Gtk.Box.BoxChild w21 = ((Gtk.Box.BoxChild)(this.hbox1[this.notebook1]));
            w21.Position = 1;
            w21.Expand = false;
            w21.Fill = false;
            this.Add(this.hbox1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Show();
            this.NameSourceButton.Clicked += new System.EventHandler(this.OnNameSourceButton_Clicked);
            this.NotesView.ShowSourceCitation += new System.EventHandler<Gedcom.UI.Common.SourceCitationArgs>(this.OnNotesView_ShowSourceCitation);
            this.NotesView.SelectNewNote += new System.EventHandler<Gedcom.UI.Common.NoteArgs>(this.OnNotesView_SelectNewNote);
        }
    }
}
