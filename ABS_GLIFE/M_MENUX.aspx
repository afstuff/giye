<%@ Page Language="VB" AutoEventWireup="false" CodeFile="M_MENUX.aspx.vb" Inherits="M_MENUX" %>

<%@ Register src="UC_BANMX.ascx" tagname="UC_BANMX" tagprefix="uc1" %>
<%@ Register src="~/UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Life Application Module</title>
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
    </script>

</head>
<body>
    <form id="Form1" runat="server">
    <!-- start banner -->
    <div id="div_banner" align="center">
                
        <uc1:UC_BANMX ID="UC_BANMX1" runat="server" />
                
    </div>
    
    <!-- start header -->
    <div id="div_header" align="center">
    <table border="0" class="tablemax" style="background-color: white; width: 100%;">
    <tr style="display: none;">
        <td align="right" valign="top" colspan="2">
            <div>
                <asp:Label ID="lblAction" ForeColor="LightGray" Text="Status:" runat="server"></asp:Label>
                &nbsp;<asp:textbox id="txtAction" ForeColor="LightGray" Text="" Visible="true" runat="server" EnableViewState="False" Width="30px"></asp:textbox></div>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" style="height: 500px; width: 300px;">
            <table align="left" border="0" style="width: 300px">
                    <%=BufferStr%>                
            </table>
        </td>

        <td align="left" valign="top" style="height: 500px; width: 650px;">
            <table align="left" border="0" class="tbl_menu_main">
                <tr>
                    <td align="left" colspan="2" valign="top" class="menuPanel_main">MAIN MENU</td>
                </tr>
                
                <tr>
                    <td align="left" colspan="2" valign="top">&nbsp;</td>
                </tr>
                
                <tr style="display: none;">
                    <td align="left" colspan="2" valign="top">
                        &nbsp;<asp:LinkButton ID="LNK_HOME" Enabled="true" runat="server"></asp:LinkButton>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" valign="top"><img alt="" src="Images/arrow_animated.gif" class="MY_IMG_LINK2" />
                        &nbsp;<asp:LinkButton ID="LNK_IL" Enabled="true" runat="server"></asp:LinkButton>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" valign="top"><img alt="" src="Images/arrow_animated.gif" class="MY_IMG_LINK2" />
                        &nbsp;<asp:LinkButton ID="LNK_GL" Enabled="true" runat="server"></asp:LinkButton>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" valign="top"><img alt="" src="Images/arrow_animated.gif" class="MY_IMG_LINK2" />
                        &nbsp;<asp:LinkButton ID="LNK_ANNUITY" Enabled="true" runat="server"></asp:LinkButton>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" valign="top"><img alt="" src="Images/arrow_animated.gif" class="MY_IMG_LINK2" />
                        &nbsp;<asp:LinkButton ID="LNK_ACC" Enabled="true" runat="server"></asp:LinkButton>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" valign="top">
                        <asp:LinkButton ID="LNK_ADMIN" Enabled="false" runat="server"></asp:LinkButton>&nbsp;
                    </td>
                </tr>

                <tr>
                    <td align="left" colspan="2" valign="top">
                        <asp:LinkButton ID="LnkBut_LogOff" runat="server" Text="LOG OFF" OnClientClick="javascript:window.close();"></asp:LinkButton>
                    </td>
                </tr>

            </table>
        </td>
    </tr>
    </table>

    </div>
    

<div id="div_footer" align="center">    

    <table id="tbl_footer" align="center">
        <tr>
            <td valign="top" colspan="2">
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td>
                                
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
