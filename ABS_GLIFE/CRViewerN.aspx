<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CRViewerN.aspx.vb" Inherits="CRViewerN" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer</title>

    <style type="text/css">
        body { background-color: Black; }
    </style>
    
    <script type="text/javascript">
        <!--
    function GetRadWindow() {
        var oWindow = null;

        if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog   

        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz az well)

        return oWindow;
        

    }

        -->
    </script>
        <script language="javascript" type="text/javascript">
// <!CDATA[

            function cmdCloseX_onclick() {

            }

// ]]>
    </script>

</head>

<body onload="<%= FirstMsg %>">

    <form id="form1" runat="server">

    <div style="background-color: #f1f1f1; border: 1px solid #c0c0c0; border-bottom-style: ridge; margin: 1px auto; height: auto; width: 95%;">
        <table align="center" border="0" width="100%" style="background-color: #f1f1f1;">
            <tr>
                <td align="left"><asp:Label ID="lblMessage" Text="Status..." runat="server" ForeColor="#FF8040"></asp:Label>
                </td>
                <td align="right"colspan="2">&nbsp;<input id="cmdCloseX" type="button" style="font-weight:bold; font-size:medium;" value="Close Page ..." runat="server" onclick="return cmdCloseX_onclick()" />
                
                                </td>
            </tr>
            
        </table>    
    </div>

    <div style="background-color: #ffffff; border: 1px solid #c0c0c0; border-bottom-style: ridge; margin: 1px auto; height: auto; width: 95%;">    
        <div>
            &nbsp;&nbsp;<%=PageURLs%>
        </div>
        
        <br />
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />

        <br />
        
    </div>

    <div style="background-color: #f1f1f1; border: 1px solid #c0c0c0; border-bottom-style: ridge; margin: 1px auto; height: auto; width: 95%;">
        <table align="center" border="0" width="100%" style="background-color: #f1f1f1;">
            <tr>
                <td align="left"colspan="2" style="width: 100%;">All Rights Reserved</td>
            </tr>

        </table>    
    </div>
    
    </form>
</body>
</html>
