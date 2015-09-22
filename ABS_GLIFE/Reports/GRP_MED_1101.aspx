<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GRP_MED_1101.aspx.vb" Inherits="Reports_GRP_MED_1101" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register src="../UC_BAN.ascx" tagname="UC_BAN" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reassurance Report</title>

    <script type="text/javascript" src="../Cal/calendar_eu.js"></script>    
    <link rel="stylesheet" type="text/css" href="../Cal/calendar.css" />

    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
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
	                <tr style="display: none;">
                        <td align="right" colspan="2" valign="top">    
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="450px" runat="server"></asp:DropDownList>
                        </td>	                
	                </tr>

                    <tr>
                        <td align="left" colspan="2" valign="top" class="myMenu_Title"><%=STRMENU_TITLE%></td>
                    </tr>

                    <tr>
                        <td align="left" colspan="2" valign="top">&nbsp;
                            <asp:Label ID="lblMsg" Text="Status..." Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                        </td>
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

                    
                    <tr>

                        <td align="left" colspan="2" valign="top">

                            <table align="center" border="0">
                                <tr>
                                    <td align="right" colspan="1" valign="top">&nbsp;
                                        <asp:Label ID="lblStart_Date" Text="Start Date:" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" colspan="1" valign="top">&nbsp;
                                        <asp:TextBox ID="txtStart_Date" Font-Bold="true" Width="120px" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                        <script language="JavaScript" type="text/javascript">
                                            new tcal({ 'formname': 'Form1', 'controlname': 'txtStart_Date' });
                                        </script>                                        
                                        &nbsp;&nbsp;<asp:Label ID="lblStart_DateX" Text="dd/mm/yyyy" runat="server"></asp:Label></td>
                                    <td align="right" colspan="1" valign="top">&nbsp;
                                        <asp:Label ID="lblEnd_Date" Text="End Date:" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" colspan="1" valign="top">&nbsp;
                                        <asp:TextBox ID="txtEnd_Date" Font-Bold="true" Width="120px" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                        <script language="JavaScript" type="text/javascript">
                                            new tcal({ 'formname': 'Form1', 'controlname': 'txtEnd_Date' });
                                        </script>
                                        &nbsp;&nbsp;<asp:Label ID="lblEnd_DateX" Text="dd/mm/yyyy" runat="server"></asp:Label></td>
                                </tr>

                                <tr>
                                    <td align="right" colspan="1" valign="top">&nbsp;
                                        <asp:Label ID="lblStart_Pol_Num" Text="Start Policy No:" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" colspan="1" valign="top">&nbsp;
                                        <asp:TextBox ID="txtStart_Pol_Num" Font-Bold="true" Width="200px" Text="0" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" colspan="1" valign="top">&nbsp;
                                        <asp:Label ID="lblEnd_Pol_Num" Text="End Policy No:" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" colspan="1" valign="top">&nbsp;
                                        <asp:TextBox ID="txtEnd_Pol_Num" Font-Bold="true" Width="200px" Text="ZZZ" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
    	                    	    <td nowrap align="right" colspan="1">&nbsp;
    	        	                    <asp:Label ID="lblRA_LIMIT" Text="Medical Free Cover Limit:" runat="server"></asp:Label>
    	                    	    </td>
    	                    	    <td nowrap align="left" colspan="2">&nbsp;
    	        	                    <asp:TextBox ID="txtRA_LIMIT" Font-Bold="true" ForeColor="Red" Width="200px" Text="15000000" runat="server"></asp:TextBox>
                   	        	    </td>
                                </tr>

                            </table>                            

                        </td>

                    </tr>


                    <tr>
    	        	    <td nowrap align="left" colspan="2">&nbsp;
    	        	        <asp:CheckBox ID="chkExport_Xls" Enabled="false" Text="Export to Excel" runat="server" />
    	        	    </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="top">&nbsp;
                            <asp:Button ID="BUT_OK" Enabled="true" Font-Bold="true" 
                                Text="View / Print Report" runat="server" Width="220px" />
                        </td>
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
