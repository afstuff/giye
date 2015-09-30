<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_GRP_QUOT_ENTRY.aspx.vb" Inherits="Policy_PRG_LI_GRP_QUOT_ENTRY" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Group Life Module</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js"> </script>
    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js"> </script>
            
    <style type="text/css">
        .style2
        {
            width: 164px;
            height: 31px;
        }
        .style6
        {
            width: 174px;
            height: 30px;
        }
        .style7
        {
            height: 30px;
        }
        .style8
        {
            width: 252px;
            height: 30px;
        }
        .style9
        {
            width: 174px;
            height: 31px;
        }
        .style10
        {
            height: 31px;
        }
        .style11
        {
            width: 252px;
            height: 33px;
        }
        .style12
        {
            width: 174px;
            height: 33px;
        }
        .style13
        {
            height: 33px;
        }
        .style14
        {
            height: 28px;
        }
        .style15
        {
            width: 174px;
            height: 28px;
        }
        .style17
        {
            width: 252px;
            height: 28px;
        }
    </style>
            
</head>

<body onload="<%= FirstMsg %>">
    <form id="Form1" name="Form1" runat="server">

    <!-- start banner -->
    <div id="div_banner" align="center">
    
        <uc1:UC_BANT ID="UC_BANT1" runat="server" />
    
    </div>

    <!-- start header -->
    <div id="div_header" align="center">
        <table id="tbl_header" align="center">
            <tr>
                <td align="left" valign="top" class="myMenu_Title_02">
                    <table border="0" width="100%">

                       <%-- <tr style="display: none;">--%>
                        <tr>
                            <td align="left" colspan="2" valign="top"><%=STRMENU_TITLE%></td>
                            <td align="right" colspan="2" valign="top">    
                                &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>&nbsp;&nbsp;Find:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;&nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;&nbsp;<asp:DropDownList ID="cboSearch" runat="server" Height="26px" 
                                    Width="150px" AppendDataBoundItems="True" AutoPostBack="True">
                                    <asp:ListItem>*** Select ***</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                                    <tr style="display: none;">
                                        <td align="left" colspan="4" valign="top"><hr /></td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="center" colspan="4" valign="top" style="height: 26px">
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_GP_PROP_POLICY.aspx?menu=GL_QUOTE')">Go to Menu</a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
	                                        &nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
		                		        	&nbsp;&nbsp;<asp:Button ID="cmdDel_ASP" CssClass="cmd_butt" Enabled="false" 
                                                Font-Bold="False" Text="Delete" OnClientClick="return ConfirmDelete()"
                                                runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" CssClass="cmd_butt" Enabled="False" Visible="false" runat="server" text="Print"></asp:button>
                                            &nbsp;&nbsp;</td>                                        
                                    </tr>

                        
                    </table>                    
                </td>
            </tr>
        </table>
    </div>


    <!-- start content -->
    <div id="div_content" align="center">
        <table class="tbl_cont" align="center">
                <tr>
                    <td nowrap class="myheader">Quotation Slip Entry</td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="td_menu">
                        <table align="center" border="0" class="tbl_menu_new" style="height:500px;">
                            <tr>
                                <td align="left" colspan="5" valign="top" class="style14">
                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                                <td nowrap align="left" valign="top" class="style6">
                                                    <asp:Label ID="lblProspect" Enabled="False" Text="Prospect:" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" colspan="1" class="style7">
                                                    <asp:TextBox ID="txtProspect" runat="server" 
                                                        Width="322px" style="margin-left: 0px"></asp:TextBox>
                                                </td>
                                                <td align="left" valign="top" colspan="2" class="style8">
                                                    </td>
                                                <td align="left" valign="top" colspan="1" class="style7">                                                    
                                                </td>
                            </tr>

                            <tr>
                                                <td align="left" valign="top" colspan="1" class="style9">
                                                    <asp:Label ID="lblTotEmolument" 
                                                        Text="Estimated Total Emoluments:" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" class="style10">
                                                    <asp:TextBox ID="txtTotEmolument" 
                                                        Width="191px" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="left" valign="top" class="style2">
                                                </td>
                                                <td align="left" valign="top" colspan="2" class="style10">
                                                </td>
                            </tr>

                                            <tr>
                                                <td nowrap align="left" valign="top" class="style6"><asp:Label ID="lblTotNoStaff" 
                                                        Text="Total Number of staff:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1" class="style7">                                                    
                                                    <asp:TextBox ID="txtTotNoStaff" Enabled="true" 
                                                        Width="109px" runat="server"></asp:TextBox></td>                                            
                                                <td align="left" valign="top" colspan="2" class="style8"></td>
                                                <td align="left" valign="top" colspan="1" class="style7">                                                    
                                                    <asp:TextBox ID="txtFileNum" runat="server" Visible="False"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td nowrap align="left" valign="top" class="style12"><asp:Label ID="lblTransDate" 
                                                        Text="Transaction Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1" class="style13">                                                    
                                                    <asp:TextBox ID="txtTransDate" MaxLength="10" Width="191px" 
                                                        runat="server"></asp:TextBox><asp:Label ID="lblDOB_Format" Enabled="False" 
                                                        Text="dd/mm/yyyy" runat="server"></asp:Label></td>                                            
                                                <td align="left" valign="top" colspan="2" class="style11">
                                                    </td>
                                                <td align="left" valign="top" colspan="1" class="style13">                                                    
                                                    </td>
                                            </tr>

                                            <tr>
                                                <td nowrap align="left" valign="top" class="style6">
                                                    <asp:Label ID="lblRate" 
                                                        Text="Rate:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1" class="style7">                                                    
                                                    <asp:TextBox ID="txtRate" 
                                                        Width="189px" runat="server" style="margin-bottom: 0px"></asp:TextBox>
                                                </td>                                            
                                                <td align="left" valign="top" colspan="2" class="style8">
                                                    </td>
                                                <td align="left" valign="top" colspan="1" class="style7">                                                    
                                                    </td>
                                            </tr>

                                            <tr>
                                                <td nowrap align="left" valign="top" class="style15">
                                                    <asp:Label ID="lblPremium" 
                                                        Text="Premium:" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" colspan="1" class="style14">                                                    
                                                    <asp:TextBox ID="txtPremium" Width="191px" 
                                                        runat="server"></asp:TextBox>
                                                </td>                                            
                                                <td align="left" valign="top" colspan="2" class="style17">
                                                    </td>
                                                <td align="left" valign="top" colspan="1" class="style14">                                                    
                                                    </td>
                                            </tr>

                            <tr>
                                <td align="center" colspan="5" valign="top">
                                    &nbsp;</td>
                            </tr>                            
                        </table>
                    </td>
                </tr>
            </table>                                                                                
    </div>
                                                                                
<div id="div_footer" align="center">    

    <table id="tbl_footer" align="center">
        <tr>
            <td valign="top">
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td colspan="4">                                                        
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
<script language="javascript" type="text/javascript">
    function ConfirmDelete() {
        // e.preventDefault();
        var returnMsg;
        var result = confirm("Are you sure you want to delete this record?");
        console.log("Working")
        if (result) {
            returnMsg= true;
        }
        else {
            returnMsg= false;
        }
        return returnMsg;
        console.log(returnMsg)
    }
</script>
</html>
