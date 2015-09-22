<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MENU_GL.aspx.vb" Inherits="MENU_GL" %>

<%@ Register src="UC_BANM.ascx" tagname="UC_BANM" tagprefix="uc1" %>

<%@ Register src="~/UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group Life Module</title>
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
    </script>
</head>
<body>
    <form id="Form1" name="Form1" runat="server">
    
    <!-- start banner -->
    <div id="div_banner" align="center">
        
        <uc1:UC_BANM ID="UC_BANM1" runat="server" />
        
    </div>
    
    <!-- start header -->
    <div id="div_header" align="center">
        <asp:Panel ID="menuPanel_main" CssClass="menuPanel_main" runat="server">&nbsp;&nbsp;
            <!--
            &nbsp;<a class="HREF_MENU2" href="#" onclick="javascript:JSDO_RETURN('../../M_MENU.aspx?menu=HOME')">Main Menu</a>&nbsp;
            -->
            
            <asp:LinkButton ID="LNK_CODE" Enabled="true" runat="server" Text="Master Setup" PostBackUrl="MENU_GL.aspx?menu=GL_CODE"></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LNK_QUOTE" Enabled="true" runat="server" Text="Quotations" PostBackUrl="~/Policy/PRG_GP_PROP_POLICY.aspx?menu=GL_QUOTE"></asp:LinkButton>&nbsp;
            
            <asp:LinkButton ID="LNK_UND" Enabled="true" runat="server" Text="Underwriting" PostBackUrl="MENU_GL.aspx?menu=GL_UND"></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LNK_ENDORSE" Enabled="true" runat="server" Text="Endorsement" PostBackUrl="MENU_GL.aspx?menu=GL_ENDORSE"></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LNK_PROCESS" Enabled="true" runat="server" Text="Processing" PostBackUrl="MENU_GL.aspx?menu=GL_PROCESS"></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LNK_CLP" Enabled="true" runat="server" Text="Claims" PostBackUrl="MENU_GL.aspx?menu=GL_CLAIM"></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LNK_REINS" Enabled="true" runat="server" Text="Reinsurance" PostBackUrl="MENU_GL.aspx?menu=GL_REINS"></asp:LinkButton>&nbsp;
            <!-- &nbsp;<a class="HREF_MENU2" href="#" onclick="javascript:JSDO_RETURN('../M_MENU.aspx?menu=HOME')">Main Menu</a>&nbsp; -->
            <asp:LinkButton ID="LNK_LOGOFF" Enabled="true" runat="server" Text="LOG OFF" OnClientClick="javascript:JSDO_LOG_OUT();"></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LNK_LOGOFF_X" Enabled="true" runat="server" Text="Log Off" Visible="false" PostBackUrl="LoginP.aspx"></asp:LinkButton>&nbsp;

            <div style="display: none;">
                &nbsp;<asp:Label ID="lblAction" ForeColor="LightGray" Text="Status:" runat="server"></asp:Label>
                &nbsp;<asp:textbox id="txtAction" ForeColor="LightGray" Text="" Visible="true" runat="server" EnableViewState="False" Width="30px"></asp:textbox>
            </div>
        </asp:Panel>
    </div>
    
    <div =id="div_content" align="center">
        <table id="tbl_content" align="center">
        <tr>
            <td align="left" valign="top" class="td_menu">
	            <table align="center" border="0" cellspacing="0" class="tbl_menu_new">
                    <tr>
                        <td align="left" colspan="2" valign="top" class="myMenu_Title"><%=STRMENU_TITLE%></td>
                    </tr>
                    <%=BufferStr%>
                    <tr>
                        <td align="left" colspan="2" valign="top">&nbsp;</td>
                    </tr>
				</table>
			</td>
        </tr>
        </table>
    </div>

<div id="div_footer" align="center">    

    <table id="tbl_footer" align="center">
        <tr>
            <td align="left" valign="top">
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td align="left">                                                        
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
