// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Gedcom.UI.GTK {
    
    
    public partial class IndividualListDialog {
        
        private Gedcom.UI.GTK.Widgets.IndividualList IndividualList;
        
        private Gtk.Button button66;
        
        private Gtk.Button button65;
        
        private Gtk.Button button1433;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget Gedcom.UI.GTK.IndividualListDialog
            this.Events = ((Gdk.EventMask)(256));
            this.Name = "Gedcom.UI.GTK.IndividualListDialog";
            this.Title = Mono.Unix.Catalog.GetString("Individual List");
            this.TypeHint = ((Gdk.WindowTypeHint)(1));
            this.Modal = true;
            this.HasSeparator = false;
            // Internal child Gedcom.UI.GTK.IndividualListDialog.VBox
            Gtk.VBox w1 = this.VBox;
            w1.Events = ((Gdk.EventMask)(256));
            w1.Name = "dialog_VBox";
            w1.BorderWidth = ((uint)(2));
            // Container child dialog_VBox.Gtk.Box+BoxChild
            this.IndividualList = new Gedcom.UI.GTK.Widgets.IndividualList();
            this.IndividualList.Events = ((Gdk.EventMask)(256));
            this.IndividualList.Name = "IndividualList";
            w1.Add(this.IndividualList);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(w1[this.IndividualList]));
            w2.Position = 0;
            // Internal child Gedcom.UI.GTK.IndividualListDialog.ActionArea
            Gtk.HButtonBox w3 = this.ActionArea;
            w3.Events = ((Gdk.EventMask)(256));
            w3.Name = "Gedcom.UI.GTK.IndividualListDialog_ActionArea";
            w3.Spacing = 6;
            w3.BorderWidth = ((uint)(5));
            // Container child Gedcom.UI.GTK.IndividualListDialog_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.button66 = new Gtk.Button();
            this.button66.CanDefault = true;
            this.button66.CanFocus = true;
            this.button66.Name = "button66";
            this.button66.UseStock = true;
            this.button66.UseUnderline = true;
            this.button66.Label = "gtk-help";
            this.AddActionWidget(this.button66, -11);
            // Container child Gedcom.UI.GTK.IndividualListDialog_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.button65 = new Gtk.Button();
            this.button65.CanDefault = true;
            this.button65.CanFocus = true;
            this.button65.Name = "button65";
            this.button65.UseStock = true;
            this.button65.UseUnderline = true;
            this.button65.Label = "gtk-new";
            this.AddActionWidget(this.button65, -5);
            Gtk.ButtonBox.ButtonBoxChild w5 = ((Gtk.ButtonBox.ButtonBoxChild)(w3[this.button65]));
            w5.Position = 1;
            w5.Expand = false;
            w5.Fill = false;
            // Container child Gedcom.UI.GTK.IndividualListDialog_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.button1433 = new Gtk.Button();
            this.button1433.CanDefault = true;
            this.button1433.CanFocus = true;
            this.button1433.Name = "button1433";
            this.button1433.UseUnderline = true;
            // Container child button1433.Gtk.Container+ContainerChild
            Gtk.Alignment w6 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
            // Container child GtkAlignment2.Gtk.Container+ContainerChild
            Gtk.HBox w7 = new Gtk.HBox();
            w7.Spacing = 2;
            // Container child GtkHBox2.Gtk.Container+ContainerChild
            Gtk.Image w8 = new Gtk.Image();
            w8.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-apply", Gtk.IconSize.Button, 20);
            w7.Add(w8);
            // Container child GtkHBox2.Gtk.Container+ContainerChild
            Gtk.Label w10 = new Gtk.Label();
            w10.LabelProp = Mono.Unix.Catalog.GetString("_Select");
            w10.UseUnderline = true;
            w7.Add(w10);
            w6.Add(w7);
            this.button1433.Add(w6);
            this.AddActionWidget(this.button1433, -10);
            Gtk.ButtonBox.ButtonBoxChild w14 = ((Gtk.ButtonBox.ButtonBoxChild)(w3[this.button1433]));
            w14.Position = 2;
            w14.Expand = false;
            w14.Fill = false;
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 400;
            this.DefaultHeight = 300;
            this.Show();
        }
    }
}