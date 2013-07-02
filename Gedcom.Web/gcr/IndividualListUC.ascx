<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="IndividualListUC.ascx.vb" Inherits="Gedcom.Web.IndividualListUC" %>
<asp:GridView ID="gvIndividuals" runat="server" AutoGenerateColumns="False" 
    Width="100%">
    <Columns>
        <asp:HyperLinkField DataNavigateUrlFields="Original_Id"  DataTextField="FullName" DataTextFormatString="{0}" 
            DataNavigateUrlFormatString="IndividualPage.aspx?ID={0}" HeaderText="Nombre" />
        <asp:BoundField DataField="BirthDate" HeaderText="Fecha de Nacimiento" 
            DataFormatString="{0:d}" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="BirthPlace" HeaderText="Lugar de Nacimiento" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    </Columns>
</asp:GridView>

