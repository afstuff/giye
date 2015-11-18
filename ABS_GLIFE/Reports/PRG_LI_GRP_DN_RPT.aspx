<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_GRP_DN_RPT.aspx.vb" Inherits="Reports_PRG_LI_GRP_DN_RPT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register src="../UC_BAN.ascx" tagname="UC_BAN" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Debit Note or Credit Note Print</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/JS_DOC.js"></script>
    
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
	                <tr>
                        <td align="right" colspan="2" valign="top">    
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="500px" runat="server"></asp:DropDownList>
                        </td>	                
	                </tr>

                    <tr>
                        <td align="left" colspan="2" valign="top" class="myMenu_Title"><%=STRMENU_TITLE%></td>
                    </tr>

                    <tr>
                        <td colspan="2"></td>
                    </tr>

                    <tr>
                        <td align="right" colspan="2" valign="top">&nbsp;<%=PageLinks%></td>                           
                    </tr>

                    <tr>
                        <td align="left" colspan="2" valign="top">&nbsp;
                            <asp:Label ID="lblMsg" Text="Status..." Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="right" colspan="1" valign="top">&nbsp;
                            <asp:Label ID="lblTrans_Num" Text="Enter Debit or Credit Note No:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="1" valign="top">&nbsp;
                            <asp:TextBox ID="txtTrans_Num" Font-Bold="true" Width="250px" runat="server"></asp:TextBox>
                            &nbsp;<asp:Button ID="cmdGetRecord" Enabled="true" Text="Get Record..." runat="server" />
                            &nbsp;&nbsp;<asp:TextBox ID="txtFileNum" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                            &nbsp;&nbsp;<asp:TextBox ID="txtQuote_Num" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
    	        	    <td nowrap align="left" colspan="1">&nbsp;
    	        	        </td>
                        <td align="left" colspan="1" valign="top">&nbsp;
                            <asp:Button ID="BUT_OK" Enabled="true" Font-Bold="true" 
                                Text="View / Print Report" runat="server" Width="220px" />
                        </td>
                    </tr>                    

                    <tr>
                        <td align="right" colspan="2" valign="top">&nbsp;<%=PageURLs%>&nbsp;&nbsp;&nbsp;</td>
                    </tr>

                    <tr>
                        <td align="left" colspan="2" valign="top" class="myMenu_Title">Policy Information</td>
                    </tr>

                    <tr style="display: none;">
                        <td align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblPol_Num" Text="Policy Number:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="1">&nbsp;                     
                            <asp:TextBox ID="txtPol_Num" Enabled="false" Font-Bold="true" Font-Size="Large" ForeColor="Red" Width="350px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblAssuredName" Text="Assured Name:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="1">&nbsp;                     
                            <asp:TextBox ID="txtAssured_Name" Enabled="False" runat="server" Width="400px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblProduct_Num" Text="Product:" runat="server"></asp:Label>
                        </td>    
                        <td align="left" colspan="1">&nbsp;
                            <asp:TextBox ID="txtProduct_Name" Enabled="false" Font-Bold="true" Width="260px" runat="server"></asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtProductClass" Visible="false" Enabled="false" MaxLength="10" Width="20" runat="server"></asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtProduct_Num" Visible="false" Enabled="false" MaxLength="10" Width="20px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    

                    <tr>
                        <td align="right" colspan="2" valign="top">&nbsp;</td>
                    </tr>


                    <tr>
                        <td colspan="2"><hr /></td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2" valign="top">&nbsp;<%=PageLinks%></td>                           
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
