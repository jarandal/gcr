﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FileUploadTestSolution._Default" %>

<%@ Register Assembly="DC.SilverlightFileUpload" Namespace="DC.SilverlightFileUpload"
    TagPrefix="DC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
       <DC:MultiFileUploadControl runat="server" ID="fileUpload" Width="500" Height="300" UploadPage="~/FileUpload.ashx" UploadChunkSize="25000000" />
    </div>
    </form>
</body>
</html>
