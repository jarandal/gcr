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
    
    
    public partial class RepositoryListDialog {
        
        private Gedcom.UI.GTK.Widgets.RepositoryList RepositoryList;
        
        private Gtk.Button button68;
        
        private Gtk.Button SelectRepositoryButton;
        
        private Gtk.Button button2247;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget Gedcom.UI.GTK.RepositoryListDialog
            this.Events = ((Gdk.EventMask)(256));
            this.Name = "Gedcom.UI.GTK.RepositoryListDialog";
            this.Title = Mono.Unix.Catalog.GetString("RepositoryListDialog");
            // Internal child Gedcom.UI.GTK.RepositoryListDialog.VBox
            Gtk.VBox w1 = this.VBox;
            w1.Events = ((Gdk.EventMask)(256));
            w1.Name = "dialog_VBox";
            w1.BorderWidth = ((uint)(2));
            // Container child dialog_VBox.Gtk.Box+BoxChild
            this.RepositoryList = new Gedcom.UI.GTK.Widgets.RepositoryList();
            this.RepositoryList.Events = ((Gdk.EventMask)(256));
            this.RepositoryList.Name = "RepositoryList";
            w1.Add(this.RepositoryList);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(w1[this.RepositoryList]));
            w2.Position = 0;
            // Internal child Gedcom.UI.GTK.RepositoryListDialog.ActionArea
            Gtk.HButtonBox w3 = this.ActionArea;
            w3.Events = ((Gdk.EventMask)(256));
            w3.Name = "Gedcom.UI.GTK.RepositoryListDialog_ActionArea";
            w3.Spacing = 6;
            w3.BorderWidth = ((uint)(5));
            w3.LayoutStyle = ((Gtk.ButtonBoxStyle)(4));
            // Container child Gedcom.UI.GTK.RepositoryListDialog_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.button68 = new Gtk.Button();
            this.button68.CanDefault = true;
            this.button68.CanFocus = true;
            this.button68.Name = "button68";
            this.button68.UseStock = true;
            this.button68.UseUnderline = true;
            this.button68.Label = "gtk-help";
            this.AddActionWidget(this.button68, -11);
            // Container child Gedcom.UI.GTK.RepositoryListDialog_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.SelectRepositoryButton = new Gtk.Button();
            this.SelectRepositoryButton.CanDefault = true;
            this.SelectRepositoryButton.CanFocus = true;
            this.SelectRepositoryButton.Name = "SelectRepositoryButton";
            this.SelectRepositoryButton.UseStock = true;
            this.SelectRepositoryButton.UseUnderline = true;
            this.SelectRepositoryButton.Label = "gtk-new";
            this.AddActionWidget(this.SelectRepositoryButton, -5);
            Gtk.ButtonBox.ButtonBoxChild w5 = ((Gtk.ButtonBox.ButtonBoxChild)(w3[this.SelectRepositoryButton]));
            w5.Position = 1;
            w5.Expand = false;
            w5.Fill = false;
            // Container child Gedcom.UI.GTK.RepositoryListDialog_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.button2247 = new Gtk.Button();
            this.button2247.CanDefault = true;
            this.button2247.CanFocus = true;
            this.button2247.Name = "button2247";
            this.button2247.UseUnderline = true;
            // Container child button2247.Gtk.Container+ContainerChild
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
            this.button2247.Add(w6);
            this.AddActionWidget(this.button2247, -10);
            Gtk.ButtonBox.ButtonBoxChild w14 = ((Gtk.ButtonBox.ButtonBoxChild)(w3[this.button2247]));
            w14.Position = 2;
            w14.Expand = false;
            w14.Fill = false;
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 400;
            this.DefaultHeight = 300;
            this.Show();
            this.SelectRepositoryButton.Clicked += new System.EventHandler(this.OnSelectRepositoryButton_Clicked);
        }
    }
}
