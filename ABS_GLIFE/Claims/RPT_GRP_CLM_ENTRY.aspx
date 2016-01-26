<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RPT_GRP_CLM_ENTRY.aspx.vb" Inherits="Claims_RPT_GRP_CLM_ENTRY" %>

<!DOCTYPE html>

<%@ Register src="../UC_BAN.ascx" tagname="UC_BAN" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Group Claims</title>

    <script type="text/javascript" src="../Cal/calendar_eu.js"></script>    
    <link rel="stylesheet" type="text/css" href="../Cal/calendar.css" />

    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 238px;
        }
        .auto-style2 {
            font-weight: bold;
        }
    </style>
</head>

<body onload="<%= FirstMsg %>">
    <form id="Form1" runat="server">

    <!-- start banner -->
    <div id="div_banner" align="center">        
        <uc1:UC_BAN ID="UC_BAN1" Visible="true" runat="server" />        
    </div>

    <div id="div_content" align="center">
        <table id="tbl_content" align="center">
        <tr>
            <td align="center" valign="top" class="td_menu">
	            <table align="center" border="0" cellspacing="0" class="tbl_menu_new">
	                <tr style="">
                        <td align="right" valign="top" colspan="2">    
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="300px" 
                                    runat="server"></asp:DropDownList>
                        </td>	                
	                </tr>

                    <tr>
                        <td align="left" valign="top" class="myMenu_Title" colspan="2"><%=STRMENU_TITLE%><asp:Label 
                                ID="Label2" runat="server" 
                                Text="Group/Member Claim Reported"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="left" valign="top" colspan="2">&nbsp;
                            <asp:Label ID="lblMsg" Text="Status..." Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2"><hr /></td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" dir="ltr">
                            <asp:Label ID="Label1" runat="server" Text="Claim Number:"></asp:Label>
                            &nbsp;</td>                           
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtClaimNumber" runat="server" Width="250px"></asp:TextBox>
                            <asp:Button ID="cmdPrint_ASP" Enabled="true" Font-Bold="true" 
                                Text="View / Print Report" runat="server" Width="220px" />
                        </td>                           
                    </tr>
                    <tr>
                        <td align="right" valign="top" class="auto-style1">
                            <asp:Label ID="Label8" runat="server" Text="Policy Number:"></asp:Label>
                            </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lblPolicy" runat="server" CssClass="auto-style2" ForeColor="Red"></asp:Label>
                            </td>
                    </tr>                    


                    <tr>
                        <td align="right" valign="top" class="auto-style1">&nbsp;</td>
                        <td align="left" valign="top">&nbsp;</td>
                    </tr>                    


                    <tr>
                        <td colspan="2"><hr /></td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" colspan="2">&nbsp;<%=PageLinks%></td>                           
                    </tr>
                    <tr>
                        <td colspan="2"><hr /></td>
                    </tr>

				</table>
			</td>
        </tr>
        </table>
    </div>

<div id="div_footer" align="center">    

    <table id="tbl_footer" align="center">
        <tr>
            <td align="center" valign="top">
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td colspan="2">                                                                                                               
                            <uc2:UC_FOOT ID="UC_FOOT1" runat="server" />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>    

    </form>
</body>
</html>