<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="IndividualUC.ascx.vb" Inherits="Gedcom.Web.IndividualUC" %>
<asp:Panel ID="pnlIndividual" runat="server">
    <asp:Panel ID="pnlName" runat="server" CssClass="pnlDeath">
        <asp:HyperLink ID="hlnkNames" runat="server"></asp:HyperLink><br />
    </asp:Panel>
    <asp:Panel ID="pnlBirth" runat="server" CssClass="pnlBirth">
        <img alt="Notas" src="../img/baby.png" />
        <asp:Label ID="lblBirth" runat="server" ></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlDeath" runat="server" CssClass="pnlDeath">
    <img alt="Notas" src="../img/death.png" />
    <asp:Label ID="lblDeath" runat="server" ></asp:Label>
    </asp:Panel>
</asp:Panel>
