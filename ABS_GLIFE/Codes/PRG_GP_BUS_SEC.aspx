<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_GP_BUS_SEC.aspx.vb" Inherits="Codes_PRG_GP_BUS_SEC" %>

<%@ Register src="../UC_BANX.ascx" tagname="UC_BANX" tagprefix="uc1" %>
<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Business Sector Setup</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
   <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
   </script>
</head>

<body onload="<%= FirstMsg %>">
    <form id="Form1" name="Form1" runat="server">

    <!-- start banner -->
    <div id="div_banner" align="center">                      
        
        <uc1:UC_BANX ID="UC_BANX1" runat="server" />
        
    </div>

    <!-- content -->
    <div id="div_content" align="center">

        <table id="tbl_content" align="center" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" valign="top" class="tbl_buttons">
                    <table align="center" cellspacing="0" border="1">
        
                        <tr>
                            <td align="left" colspan="2" valign="baseline"><asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP()"></asp:button>
                                &nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data" OnClientClick="JSSave_ASP()"></asp:button>
                                &nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt" runat="server" text="Delete Data" OnClientClick="JSDelete_ASP()"></asp:button>
                    	        <div style="display: none;">
                    	            &nbsp;&nbsp;Status:&nbsp;
                  	                <asp:textbox id="txtAction" Visible="true" runat="server" EnableViewState="False" Width="50px"></asp:textbox>&nbsp;
                                </div>    
    	                    </td>
    	                    <td align="right" colspan="2" valign="baseline">&nbsp;&nbsp;Find:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}"></input>&nbsp;
                                <asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                    </td>

                        </tr>
                    </table>
                </td>
            </tr>

            <tr style="background-color: White; height: 30px;">
                <td align="left" colspan="3" valign="top">
                    <asp:Label id="textMessage" Text="Status:" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                </td>
                <td align="right" valign="top">                                
                                &nbsp;<a id="PageAnchor_Return_Link" runat="server" class="a_return_menu" href="#" onclick="javascript:JSDO_RETURN('../MENU_GL.aspx?menu=GL_CODE_UND')">Returns to Previous Page</a>
                                &nbsp;<%=PageLinks%>&nbsp;
                </td>
            </tr>            
            
            <tr>
                <td colspan="4" valign="top" class="td_menu">
                    <table align="center" border="0" class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="4" class="myMenu_Title">Business Sector Data Setup</td>
                        </tr>    

                        <tr style="background-color: Maroon; color: White;">
    	                    <td align="left" nowrap><asp:Label ID="Label1" runat="server">Select Record to Modify:</asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3">
                                <asp:DropDownList id="cboSubRiskName" Width="350px" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                		<tr>
    	                    <td align="left" nowrap><asp:Label ID="lblSubRiskNum" runat="server">Sector/Unit Code:</asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtSubRiskNum" MaxLength="4" Width="50px" AutoPostBack="true" runat="server" EnableViewState="true"></asp:textbox>
                            </td>
    		            </tr>
                		<tr>
        	        	    <td align="left" nowrap><asp:Label ID="lblSubRiskName" runat="server">Sector/Unit Name:</asp:Label>&nbsp;</TD>
    	    	            <td align="left" nowrap colspan="3"><asp:textbox id="txtSubRiskName" MaxLength="40" Width="350px" runat="server" EnableViewState="true" ></asp:textbox>&nbsp;</td>
                		</tr>
            	    	<tr>
    	        	        <td align="left" nowrap><asp:Label ID="lblRiskNum" runat="server">H.O.D Full Name:</asp:Label>&nbsp;</td>
    		                <td align="left" nowrap colspan="3"><asp:textbox id="txtBS_HOD_Name" MaxLength="45" Width="350px" runat="server" EnableViewState="true"></asp:textbox>
                            </td>
        	        	</tr>

                        <tr>
                            <td colspan="4">&nbsp;</td>
                        </tr>
    		
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>

        </table>
    </div>


<!-- footer -->
<div id="div_footer" align="center">

    <table id="tbl_footer" align="center">
        <tr>
            <td valign="top">
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
