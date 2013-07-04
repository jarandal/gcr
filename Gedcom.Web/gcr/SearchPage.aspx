<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SearchPage.aspx.vb" MasterPageFile="~/Site.Master"  Inherits="Gedcom.Web.SearchPage" %>
<%@ Register src="IndividualListUC.ascx" tagname="IndividualListUC" tagprefix="uc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Genealogía Chilena en Red - Búsqueda</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h7>
         B Ú S Q U E D A
    </h7>

 <h4>
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                Nombres</td>
            <td>
                Apellidos</td>
            <td>
                </td>
        </tr>
        <tr>
            <td class="style1">
                Ingrese los nombres y/o apellidos de la persona que busca:</td>
            <td class="style1">
                <asp:TextBox ID="txtFirstName" runat="server" Width="200px"></asp:TextBox></td>
            <td class="style1">
                <asp:TextBox ID="txtSurName" runat="server" Width="200px"></asp:TextBox></td>
            <td class="style1">
                <asp:Button ID="btnSearch" runat="server" Text="Buscar" /></td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
            <td class="style1" style="font-size:10px" >
                EJEMPLO: MAZA, de la, BARRERA, de la; CASTRO, de; PINO, del</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
    </table>
    </h4>

    <asp:Panel ID="pnlResults" runat="server" Visible="false">
    <h7>
         R E S U L T A D O S</h7>
    <uc1:IndividualListUC ID="ResultList" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlNotFound" runat="server" Visible="false" class="userMessage" EnableViewState="false" >
        No se encontraron registros.
    </asp:Panel>
    <asp:Panel ID="pnlMaxReg" runat="server" Visible="false" class="userMessage"  EnableViewState="false">
        Solo se despliegan los primeros <%=MaxReg()%> registros.
    </asp:Panel>
</asp:Content>
