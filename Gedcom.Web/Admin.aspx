<%@ Page Title="About Us" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Admin.aspx.vb" Inherits="Gedcom.Web.Admin" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Genealogía Chilena en Red - Acerca de</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel ID="pnlLogin" runat="server">
    <table style="width: 500px;">
        <tr>
            <td style="width: 150px;">
                 Email:
            </td>
            
            <td>
                 <asp:TextBox ID="txtUser" runat="server" width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td> Contraseña:
            </td>
            <td>
<asp:TextBox ID="txtPassword" runat="server" width="100%" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblLoginError" runat="server" EnableViewState="False" ForeColor="Red" 
                    Text="Email o contraseña incorrectos." Visible="False" ></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" /><br />
     </asp:Panel>
  
   <br />
<asp:Panel ID="pnlLogout" runat="server">    
    <asp:Label ID="lblAdminMode" runat="server" EnableViewState="False" 
         Text="Usted esta en modo de administración, podrá eliminar o restaurar registros eliminados." ></asp:Label>
    <br />
    <asp:Button ID="btnLogout" runat="server" Text="Finalizar Sesión" /><br />
    </asp:Panel>

    </asp:Content>
