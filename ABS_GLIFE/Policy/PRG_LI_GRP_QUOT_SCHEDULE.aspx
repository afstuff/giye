<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_GRP_QUOT_SCHEDULE.aspx.vb" Inherits="Policy_PRG_LI_GRP_QUOT_SCHEDULE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register src="../UC_BAN.ascx" tagname="UC_BAN" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quotation Schedule</title>
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
                            <asp:Label ID="lblPro_Pol_Num" Text="Quotation Number:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="1" valign="top">&nbsp;
                            <asp:TextBox ID="txtPro_Pol_Num" Width="250px" runat="server"></asp:TextBox>
                            &nbsp;<asp:Button ID="cmdGetPol" Enabled="true" Text="Get Record..." runat="server" />
                            &nbsp;&nbsp;<asp:TextBox ID="txtFileNum" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                            &nbsp;&nbsp;<asp:TextBox ID="txtQuote_Num" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                        </td>
                    </tr>

            		<tr>
    	        	    <td nowrap align="right" colspan="1">&nbsp;
    	        	        <asp:Label ID="lblBatch_Num" runat="server">Members Batch No:</asp:Label></td>
                        <td align="left" colspan="1" valign="top">&nbsp;
            		        <asp:textbox id="txtBatch_Num" Visible="true" MaxLength="5" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
    	        	        &nbsp;<asp:DropDownList id="cboBatch_Num" AutoPostBack="true" Width="200px" runat="server"></asp:DropDownList>
    		                &nbsp;<asp:textbox id="txtBatch_Name" Visible="false" MaxLength="30" Enabled="false" Width="40px" runat="server" EnableViewState="true" ></asp:textbox>
    		            </td>
    	        	</tr>

                    <tr>
    	        	    <td nowrap align="left" colspan="1">&nbsp;
    	        	        <asp:CheckBox ID="chkExport_Xls" Text="Export to Excel" runat="server" Visible="False" />
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
           Policy Information</td>
                    </tr>

                    <tr style="display: none;">
                        <td align="right" colspan="1" style="font-weight: 700">&nbsp;
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
                            &nbsp;<asp:TextBox ID="txtProductClass" Visible="false" Enabled="false" MaxLength="10" Width="20" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtProduct_Num" Visible="false" Enabled="false" MaxLength="10" Width="20px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtPrem_Rate_Code" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>
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

                            <tr style="display: none;">
                                <td align="left" colspan="2" valign="top">
                                    <asp:Label ID="lblResult" Text="Result:" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr style="display: none;">
                                <td align="left" colspan="2" valign="top">
                                <div align="left" style=" background-color: White; font-size:small; overflow: scroll; padding-bottom: 15px; height: 300px; display: none;" >
                                    <asp:GridView ID="GridViewN" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" GridLines="Both"
                                        AutoGenerateColumns="true" AllowPaging="false" ShowHeader="true" runat="server"
                                        PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                        PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next"
                                        PagerSettings-PreviousPageText="Previous" PagerSettings-LastPageText="Last">            
                                    </asp:GridView>
                                </div>
                                </td>
                            </tr>                                        

                            <tr style="display: none;">
                                <td align="left" colspan="2" valign="top">
                                    <asp:Label ID="lblExport" Text="Export to:" runat="server"></asp:Label>
                                    &nbsp;<asp:RadioButton ID="optPDF" GroupName="optExport" Checked="false" Text="PDF" runat="server" />
                                    &nbsp;<asp:RadioButton ID="optDOC" GroupName="optExport" Checked="false" Text="MS Word" runat="server" />
                                    &nbsp;<asp:RadioButton ID="optRTF" GroupName="optExport" Checked="false" Text="Rich Text" runat="server" />
                                    &nbsp;<asp:RadioButton ID="optExcel" GroupName="optExport" Checked="true" Text="Excel (Default)" runat="server" />
                                    &nbsp;&nbsp;<asp:Button ID="butExport_Data" Enabled="true" Text="Export Data..." runat="server" OnClick="Proc_DoExport_Data_New" />
                                </td>
                            </tr>                                

                            <tr style="display: none;">
                                <td align="left" colspan="2" valign="top">
                                    <asp:Label ID="lblExcel_Export" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
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
            <td align="center" valign="top">
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
