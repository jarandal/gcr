<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RequestPage.aspx.vb" Inherits="Gedcom.Web.RequestPage" %>
<%@ Register assembly="MSCaptcha" namespace="MSCaptcha" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Genealogía Chilena en Red - Formulario de contacto</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:XmlDataSource ID="XmlDataSource1" runat="server">
    <Data>
        <Reasons>
            <Reason Id="" Text="" />
            <Reason Id="100" Text="Felicitaciones" />
            <Reason Id="101" Text="Aporte de antecedentes" />
            <Reason Id="102" Text="Correccion de antecedentes" />
            <Reason Id="200" Text="Solicitud de incorporacion" />
            <Reason Id="201" Text="Solicitud de retiro" />
            <Reason Id="202" Text="Solicitud por infraccion de propiedad intelectual" />
            <Reason Id="203" Text="Otra solicitud" />
        </Reasons>
    </Data>
    </asp:XmlDataSource>
    <h7>
        C O N T A C T O:
        <br />
         <br />
    </h7>
    <table style="width: 500px; " >
        <tr>
            <td  class="requestLabel">
                Tipo de Requerimiento:
            </td>
            <td  class="requestValidation">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="ddlReason" 
                    ErrorMessage="Debe seleccionar un tipo de requerimiento" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:DropDownList ID="ddlReason" runat="server" Width="100%" 
                    DataMember="Reason" DataSourceID="XmlDataSource1" DataTextField="Text" 
                    DataValueField="Id"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td  id="trIndividual" runat="server" class="requestLabel">
                Individuo:
            </td>
            <td  class="requestValidation">
            
            </td>
            <td>
                <asp:Label  ID="lblIndividualName" runat="server"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td  class="requestLabel">
                Nombre:
            </td>
            <td  class="requestValidation">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtName" ErrorMessage="Debe ingresar su nombre" 
                    ForeColor="#FF3300">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="requestLabel">
                Email:
            </td>
            <td  class="requestValidation">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="Debe ingresar un  email válido" 
                    ForeColor="#CC3300" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="Debe ingresar un email" 
                    ForeColor="#FF3300">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="requestLabel">
                Telefono (Opcional):
            </td>
            <td  class="requestValidation">&nbsp;</td>
            <td>
                <asp:TextBox ID="txtPhoneNumber" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="requestLabel">
                Descripción:
            </td>
            <td  class="requestValidation">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtText" ErrorMessage="Debe ingresar una descripción" 
                    ForeColor="#FF3300">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtText" runat="server" TextMode="MultiLine" Width="100%" 
                    Rows="5"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="requestLabel">
                Archivos adjuntos:<br /> (Opcional)
            </td>
            <td  class="requestValidation">&nbsp;</td>
            <td runat="server" id="FileUploadContainer">
                <asp:FileUpload ID="FileUpload1" runat="server" /><br />
                <asp:FileUpload ID="FileUpload2" runat="server" /><br />
                <asp:FileUpload ID="FileUpload3" runat="server" /><br />
                <asp:FileUpload ID="FileUpload4" runat="server" /><br />
                <asp:FileUpload ID="FileUpload5" runat="server" /><br />
            </td>
        </tr>
        <tr id="trCaptcha" runat="server"  >
            <td class="requestLabel">
                Validación:<br />
                (Ingrese el texto de la imagen)</td>
            <td  class="requestValidation">&nbsp;</td>
            <td>
                <cc1:CaptchaControl ID="CaptchaControl1" runat="server" 
                    
                    CustomValidatorErrorMessage="El texto tipeado no concuerda con el texto de la imagen." 
                    CaptchaMaxTimeout="0" />
                <asp:TextBox ID="txtCaptcha" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
         <td  colspan="3" align="right">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                <asp:Button ID="btnEnviar" runat="server" Text="Enviar" />
                </td>
        </tr>
        <tr>
            <td  colspan="3" align="right">
            <asp:Panel ID="pnlOK" runat="server" Visible="false" class="userMessage" EnableViewState="false" >
                    El requerimiento se envió con éxito.
           </asp:Panel>
           <asp:Panel ID="pnlError" runat="server" Visible="false" class="userMessage" EnableViewState="false" >
                    Se ha producido un error al enviar el requerimiento.
           </asp:Panel>
           </td>
        </tr>
    </table>
</asp:Content>
