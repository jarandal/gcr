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
    
    
    public partial class HeaderView {
        
        private Gtk.Notebook notebook1;
        
        private Gtk.Table table1;
        
        private Gtk.Entry CopyrightEntry;
        
        private Gtk.ScrolledWindow GtkScrolledWindow;
        
        private Gtk.TextView DescriptionTextView;
        
        private Gtk.Label label10;
        
        private Gtk.Label label11;
        
        private Gtk.Label label5;
        
        private Gtk.Label label6;
        
        private Gtk.Label label8;
        
        private Gtk.Entry LanguageEntry;
        
        private Gtk.Entry SourceDateEntry;
        
        private Gtk.Entry SourceNameEntry;
        
        private Gtk.Label label1;
        
        private Gtk.Table table2;
        
        private Gedcom.UI.GTK.Widgets.AddressView AddressView;
        
        private Gtk.Label ApplicationNameLabel;
        
        private Gtk.Label CorporationLabel;
        
        private Gtk.Label label13;
        
        private Gtk.Label label4;
        
        private Gtk.Label label7;
        
        private Gtk.Label VersionLabel;
        
        private Gtk.Label label3;
        
        private Gedcom.UI.GTK.Widgets.SubmitterView SubmitterView;
        
        private Gtk.Label label2;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget Gedcom.UI.GTK.Widgets.HeaderView
            Stetic.BinContainer.Attach(this);
            this.Name = "Gedcom.UI.GTK.Widgets.HeaderView";
            // Container child Gedcom.UI.GTK.Widgets.HeaderView.Gtk.Container+ContainerChild
            this.notebook1 = new Gtk.Notebook();
            this.notebook1.CanFocus = true;
            this.notebook1.Name = "notebook1";
            this.notebook1.CurrentPage = 2;
            this.notebook1.BorderWidth = ((uint)(6));
            // Container child notebook1.Gtk.Notebook+NotebookChild
            this.table1 = new Gtk.Table(((uint)(5)), ((uint)(2)), false);
            this.table1.Name = "table1";
            this.table1.RowSpacing = ((uint)(6));
            this.table1.ColumnSpacing = ((uint)(6));
            this.table1.BorderWidth = ((uint)(6));
            // Container child table1.Gtk.Table+TableChild
            this.CopyrightEntry = new Gtk.Entry();
            this.CopyrightEntry.CanFocus = true;
            this.CopyrightEntry.Name = "CopyrightEntry";
            this.CopyrightEntry.IsEditable = true;
            this.CopyrightEntry.InvisibleChar = '●';
            this.table1.Add(this.CopyrightEntry);
            Gtk.Table.TableChild w1 = ((Gtk.Table.TableChild)(this.table1[this.CopyrightEntry]));
            w1.TopAttach = ((uint)(2));
            w1.BottomAttach = ((uint)(3));
            w1.LeftAttach = ((uint)(1));
            w1.RightAttach = ((uint)(2));
            w1.XOptions = ((Gtk.AttachOptions)(4));
            w1.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.GtkScrolledWindow = new Gtk.ScrolledWindow();
            this.GtkScrolledWindow.Name = "GtkScrolledWindow";
            this.GtkScrolledWindow.ShadowType = ((Gtk.ShadowType)(1));
            // Container child GtkScrolledWindow.Gtk.Container+ContainerChild
            this.DescriptionTextView = new Gtk.TextView();
            this.DescriptionTextView.CanFocus = true;
            this.DescriptionTextView.Name = "DescriptionTextView";
            this.GtkScrolledWindow.Add(this.DescriptionTextView);
            this.table1.Add(this.GtkScrolledWindow);
            Gtk.Table.TableChild w3 = ((Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindow]));
            w3.TopAttach = ((uint)(4));
            w3.BottomAttach = ((uint)(5));
            w3.LeftAttach = ((uint)(1));
            w3.RightAttach = ((uint)(2));
            // Container child table1.Gtk.Table+TableChild
            this.label10 = new Gtk.Label();
            this.label10.Name = "label10";
            this.label10.Xalign = 0F;
            this.label10.Yalign = 0F;
            this.label10.LabelProp = "Description:";
            this.table1.Add(this.label10);
            Gtk.Table.TableChild w4 = ((Gtk.Table.TableChild)(this.table1[this.label10]));
            w4.TopAttach = ((uint)(4));
            w4.BottomAttach = ((uint)(5));
            w4.XOptions = ((Gtk.AttachOptions)(4));
            w4.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label11 = new Gtk.Label();
            this.label11.Name = "label11";
            this.label11.Xalign = 0F;
            this.label11.LabelProp = "Source Name:";
            this.table1.Add(this.label11);
            Gtk.Table.TableChild w5 = ((Gtk.Table.TableChild)(this.table1[this.label11]));
            w5.XOptions = ((Gtk.AttachOptions)(4));
            w5.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label5 = new Gtk.Label();
            this.label5.Name = "label5";
            this.label5.Xalign = 0F;
            this.label5.LabelProp = "Copyright:";
            this.table1.Add(this.label5);
            Gtk.Table.TableChild w6 = ((Gtk.Table.TableChild)(this.table1[this.label5]));
            w6.TopAttach = ((uint)(2));
            w6.BottomAttach = ((uint)(3));
            w6.XOptions = ((Gtk.AttachOptions)(4));
            w6.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label6 = new Gtk.Label();
            this.label6.Name = "label6";
            this.label6.Xalign = 0F;
            this.label6.LabelProp = "Language:";
            this.table1.Add(this.label6);
            Gtk.Table.TableChild w7 = ((Gtk.Table.TableChild)(this.table1[this.label6]));
            w7.TopAttach = ((uint)(3));
            w7.BottomAttach = ((uint)(4));
            w7.XOptions = ((Gtk.AttachOptions)(4));
            w7.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label8 = new Gtk.Label();
            this.label8.Name = "label8";
            this.label8.Xalign = 0F;
            this.label8.LabelProp = "Date:";
            this.table1.Add(this.label8);
            Gtk.Table.TableChild w8 = ((Gtk.Table.TableChild)(this.table1[this.label8]));
            w8.TopAttach = ((uint)(1));
            w8.BottomAttach = ((uint)(2));
            w8.XOptions = ((Gtk.AttachOptions)(4));
            w8.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.LanguageEntry = new Gtk.Entry();
            this.LanguageEntry.CanFocus = true;
            this.LanguageEntry.Name = "LanguageEntry";
            this.LanguageEntry.IsEditable = true;
            this.LanguageEntry.InvisibleChar = '●';
            this.table1.Add(this.LanguageEntry);
            Gtk.Table.TableChild w9 = ((Gtk.Table.TableChild)(this.table1[this.LanguageEntry]));
            w9.TopAttach = ((uint)(3));
            w9.BottomAttach = ((uint)(4));
            w9.LeftAttach = ((uint)(1));
            w9.RightAttach = ((uint)(2));
            w9.XOptions = ((Gtk.AttachOptions)(4));
            w9.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.SourceDateEntry = new Gtk.Entry();
            this.SourceDateEntry.CanFocus = true;
            this.SourceDateEntry.Name = "SourceDateEntry";
            this.SourceDateEntry.IsEditable = true;
            this.SourceDateEntry.InvisibleChar = '●';
            this.table1.Add(this.SourceDateEntry);
            Gtk.Table.TableChild w10 = ((Gtk.Table.TableChild)(this.table1[this.SourceDateEntry]));
            w10.TopAttach = ((uint)(1));
            w10.BottomAttach = ((uint)(2));
            w10.LeftAttach = ((uint)(1));
            w10.RightAttach = ((uint)(2));
            w10.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.SourceNameEntry = new Gtk.Entry();
            this.SourceNameEntry.CanFocus = true;
            this.SourceNameEntry.Name = "SourceNameEntry";
            this.SourceNameEntry.IsEditable = true;
            this.SourceNameEntry.InvisibleChar = '●';
            this.table1.Add(this.SourceNameEntry);
            Gtk.Table.TableChild w11 = ((Gtk.Table.TableChild)(this.table1[this.SourceNameEntry]));
            w11.LeftAttach = ((uint)(1));
            w11.RightAttach = ((uint)(2));
            w11.YOptions = ((Gtk.AttachOptions)(4));
            this.notebook1.Add(this.table1);
            // Notebook tab
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.LabelProp = "File Information";
            this.notebook1.SetTabLabel(this.table1, this.label1);
            this.label1.ShowAll();
            // Container child notebook1.Gtk.Notebook+NotebookChild
            this.table2 = new Gtk.Table(((uint)(4)), ((uint)(2)), false);
            this.table2.Name = "table2";
            this.table2.RowSpacing = ((uint)(6));
            this.table2.ColumnSpacing = ((uint)(6));
            this.table2.BorderWidth = ((uint)(6));
            // Container child table2.Gtk.Table+TableChild
            this.AddressView = new Gedcom.UI.GTK.Widgets.AddressView();
            this.AddressView.Events = ((Gdk.EventMask)(256));
            this.AddressView.Name = "AddressView";
            this.table2.Add(this.AddressView);
            Gtk.Table.TableChild w13 = ((Gtk.Table.TableChild)(this.table2[this.AddressView]));
            w13.TopAttach = ((uint)(3));
            w13.BottomAttach = ((uint)(4));
            w13.RightAttach = ((uint)(2));
            // Container child table2.Gtk.Table+TableChild
            this.ApplicationNameLabel = new Gtk.Label();
            this.ApplicationNameLabel.Name = "ApplicationNameLabel";
            this.ApplicationNameLabel.Xalign = 0F;
            this.ApplicationNameLabel.LabelProp = "label9";
            this.table2.Add(this.ApplicationNameLabel);
            Gtk.Table.TableChild w14 = ((Gtk.Table.TableChild)(this.table2[this.ApplicationNameLabel]));
            w14.LeftAttach = ((uint)(1));
            w14.RightAttach = ((uint)(2));
            w14.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table2.Gtk.Table+TableChild
            this.CorporationLabel = new Gtk.Label();
            this.CorporationLabel.Name = "CorporationLabel";
            this.CorporationLabel.Xalign = 0F;
            this.CorporationLabel.LabelProp = "label11";
            this.table2.Add(this.CorporationLabel);
            Gtk.Table.TableChild w15 = ((Gtk.Table.TableChild)(this.table2[this.CorporationLabel]));
            w15.TopAttach = ((uint)(2));
            w15.BottomAttach = ((uint)(3));
            w15.LeftAttach = ((uint)(1));
            w15.RightAttach = ((uint)(2));
            w15.XOptions = ((Gtk.AttachOptions)(4));
            w15.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table2.Gtk.Table+TableChild
            this.label13 = new Gtk.Label();
            this.label13.Name = "label13";
            this.label13.Xalign = 0F;
            this.label13.LabelProp = "Version:";
            this.table2.Add(this.label13);
            Gtk.Table.TableChild w16 = ((Gtk.Table.TableChild)(this.table2[this.label13]));
            w16.TopAttach = ((uint)(1));
            w16.BottomAttach = ((uint)(2));
            w16.XOptions = ((Gtk.AttachOptions)(4));
            w16.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table2.Gtk.Table+TableChild
            this.label4 = new Gtk.Label();
            this.label4.Name = "label4";
            this.label4.Xalign = 0F;
            this.label4.LabelProp = "Application:";
            this.table2.Add(this.label4);
            Gtk.Table.TableChild w17 = ((Gtk.Table.TableChild)(this.table2[this.label4]));
            w17.XOptions = ((Gtk.AttachOptions)(4));
            w17.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table2.Gtk.Table+TableChild
            this.label7 = new Gtk.Label();
            this.label7.Name = "label7";
            this.label7.Xalign = 0F;
            this.label7.LabelProp = "Corporation:";
            this.table2.Add(this.label7);
            Gtk.Table.TableChild w18 = ((Gtk.Table.TableChild)(this.table2[this.label7]));
            w18.TopAttach = ((uint)(2));
            w18.BottomAttach = ((uint)(3));
            w18.XOptions = ((Gtk.AttachOptions)(4));
            w18.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table2.Gtk.Table+TableChild
            this.VersionLabel = new Gtk.Label();
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Xalign = 0F;
            this.VersionLabel.LabelProp = "label10";
            this.table2.Add(this.VersionLabel);
            Gtk.Table.TableChild w19 = ((Gtk.Table.TableChild)(this.table2[this.VersionLabel]));
            w19.TopAttach = ((uint)(1));
            w19.BottomAttach = ((uint)(2));
            w19.LeftAttach = ((uint)(1));
            w19.RightAttach = ((uint)(2));
            w19.XOptions = ((Gtk.AttachOptions)(4));
            w19.YOptions = ((Gtk.AttachOptions)(4));
            this.notebook1.Add(this.table2);
            Gtk.Notebook.NotebookChild w20 = ((Gtk.Notebook.NotebookChild)(this.notebook1[this.table2]));
            w20.Position = 1;
            // Notebook tab
            this.label3 = new Gtk.Label();
            this.label3.Name = "label3";
            this.label3.LabelProp = "Application";
            this.notebook1.SetTabLabel(this.table2, this.label3);
            this.label3.ShowAll();
            // Container child notebook1.Gtk.Notebook+NotebookChild
            this.SubmitterView = new Gedcom.UI.GTK.Widgets.SubmitterView();
            this.SubmitterView.Events = ((Gdk.EventMask)(256));
            this.SubmitterView.Name = "SubmitterView";
            this.notebook1.Add(this.SubmitterView);
            Gtk.Notebook.NotebookChild w21 = ((Gtk.Notebook.NotebookChild)(this.notebook1[this.SubmitterView]));
            w21.Position = 2;
            // Notebook tab
            this.label2 = new Gtk.Label();
            this.label2.Name = "label2";
            this.label2.LabelProp = "Submitter";
            this.notebook1.SetTabLabel(this.SubmitterView, this.label2);
            this.label2.ShowAll();
            this.Add(this.notebook1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Hide();
            this.SubmitterView.ShowSourceCitation += new System.EventHandler<Gedcom.UI.Common.SourceCitationArgs>(this.OnSubmitterView_ShowSourceCitation);
            this.SubmitterView.SelectNewNote += new System.EventHandler<Gedcom.UI.Common.NoteArgs>(this.OnSubmitterView_SelectNewNote);
            this.SubmitterView.OpenFile += new System.EventHandler<Gedcom.UI.Common.MultimediaFileArgs>(this.OnSubmitterView_OpenFile);
            this.SubmitterView.AddFile += new System.EventHandler<Gedcom.UI.Common.MultimediaFileArgs>(this.OnSubmitterView_AddFile);
        }
    }
}