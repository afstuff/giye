<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_GRP_PREM_LIST.aspx.vb" Inherits="PRG_LI_GRP_PREM_LIST" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="<%= FirstMsg %>">
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Button ID="Button1" runat="server" Text="Button" />
        <br />
    
    </div>
    <rsweb:ReportViewer ID="ReportViewer" runat="server">
    </rsweb:ReportViewer>
    </form>
</body>
</html>
