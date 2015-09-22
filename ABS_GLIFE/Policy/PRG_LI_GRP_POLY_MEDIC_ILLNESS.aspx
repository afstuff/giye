<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_GRP_POLY_MEDIC_ILLNESS.aspx.vb" Inherits="Policy_PRG_LI_GRP_POLY_MEDIC_ILLNESS" %>

<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group Life Module</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>
    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js">
    </script>
            
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

                        <tr style="display: none;">
                            <td align="left" colspan="2" valign="top"><%=STRMENU_TITLE%></td>
                            <td align="right" colspan="2" valign="top">    
                                &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>&nbsp;&nbsp;Find:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;&nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;&nbsp;<asp:DropDownList ID="cboSearch" runat="server" Height="26px" 
                                    Width="150px"></asp:DropDownList>
                            </td>
                        </tr>

                                    <tr style="display: none;">
                                        <td align="left" colspan="4" valign="top"><hr /></td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="center" colspan="4" valign="top">
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_GP_PROP_POLICY.aspx?menu=GL_QUOTE')">Go to Menu</a>
                                            &nbsp;&nbsp;<asp:Button ID="cmdPrev" CssClass="cmd_butt" Enabled="false" Text="«..Previous" runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
	                                        &nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
		                		        	&nbsp;&nbsp;<asp:Button ID="cmdDelItem_ASP" CssClass="cmd_butt" Enabled="false" Font-Bold="true" Text="Delete Item" OnClientClick="JSDelItem_ASP()" runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" CssClass="cmd_butt" Enabled="False" runat="server" text="Print"></asp:button>
                                            &nbsp;&nbsp;<asp:Button ID="cmdNext" CssClass="cmd_butt" Enabled="false" Text="Next..»" runat="server" />
                                            &nbsp;&nbsp;
                                        </td>
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
                    <td align="left" nowrap class="myheader">Medical Illness Information</td>
                </tr>
                <tr>
                    <td align="left" valign="top" class="td_menu">
                        <table align="center" border="0" class="tbl_menu_new">
                            <tr>
                                <td align="left" colspan="4" valign="top">
                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblFileNum" Enabled="false" Text="File No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtFileNum" Enabled="false" Width="230px" runat="server"></asp:TextBox></td>
                                                <td align="right" valign="top"><asp:Label ID="lblPolNum" Text="Policy No:" Enabled="false" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtPolNum" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
                            </tr>
                        
                            <tr>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblQuote_Num" Enabled="false" Width="250px" Text="Proposal No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtQuote_Num" Enabled="false" Width="230px" runat="server"></asp:TextBox></td>
                                                <td align="right" valign="top" colspan="1"><asp:Label ID="lblRecNo" BorderStyle="Solid" Text="Rec. No:" Enabled="false" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtRecNo" Enabled="false" runat="server" MaxLength="18"></asp:TextBox>
                                                </td>
                            </tr>

                            <tr>
                                                <td nowrap align="left" valign="top" colspan="1">
                                                    <asp:Label ID="lblBatch_Num" Visible="true" Enabled="false" Text="Batch Number:" ToolTip="Enter unique batch number..." runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtBatch_Num" Enabled="false" MaxLength="10" Width="80px" runat="server"></asp:TextBox>
                                                    &nbsp;&nbsp;<asp:DropDownList ID="cboBatch_Num" Enabled="false" AutoPostBack="true" Width="100px" runat="server"></asp:DropDownList>
                                                    &nbsp;&nbsp;<asp:Button ID="cmdGetBatch" Enabled="false" Text="Get Data" runat="server" />
                                                    &nbsp;<asp:TextBox ID="txtBatch_Name" Visible="false" MaxLength="10" Width="20px" runat="server"></asp:TextBox>
                                                </td>
                                            <td align="right" valign="top"><asp:Label ID="lblMember_SN" Enabled="false" Text="Serial No" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:TextBox ID="txtMember_SN" Enabled="false" MaxLength="10" Width="100px" runat="server"></asp:TextBox></td>
                            </tr>


                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Medical Illness Details</td>
                                    </tr>
                            
                            <tr>
                                <td align="left" colspan="4" class="td_frame">
                                    <table align="center" border="1">
                                        <tr class="tr_frame">
                                                <td align="left" valign="top"><asp:Label ID="lblMed_Illness_Date" Text="Illness Date" runat="server"></asp:Label>
                                                    <br /><asp:Label ID="lblMed_Illness_Date_Format" Text="dd/mm/yyyy" runat="server"></asp:Label>
                                                </td>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblMed_Illness_Code" Text="Illness Type" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="font-size: small;">
                                                <td align="left" valign="top"><asp:TextBox ID="txtMed_Illness_Date" MaxLength="10" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top">
                                                    <asp:DropDownList ID="cboMed_Illness_Code" Width="300px" runat="server">
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtMed_Illness_Code" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtMed_Illness_Name" Visible="false" Enabled="false" 
                                                        Width="40px" runat="server" Height="22px"></asp:TextBox>
                                                </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>

                                        <tr>
                                            <td colspan="4"><hr /></td>
                                        </tr>
                    
                            <tr>
                                <td align="center" colspan="4" valign="top">
                                    <table align="center" style="background-color: White; width: 95%;">
                                        <tr>
                                            <td align="left" colspan="4" valign="top">
                                                <asp:GridView id="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl"
                                                    DataKeyNames="TBIL_POL_ILL_REC_ID" HorizontalAlign="Left"
                                                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="true" PageSize="10"
                                                    PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                                    PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next"
                                                    PagerSettings-PreviousPageText="Previous" PagerSettings-LastPageText="Last"
                                                    EmptyDataText="No data available..."
                                                    GridLines="Both" ShowFooter="True">                        

                        
                                                    <PagerStyle CssClass="grd_page_style" />
                                                    <HeaderStyle CssClass="grd_header_style" />
                                                    <RowStyle CssClass="grd_row_style" />
                                                    <SelectedRowStyle CssClass="grd_selrow_style" />
                                                    <EditRowStyle CssClass="grd_editrow_style" />
                                                    <AlternatingRowStyle CssClass="grd_altrow_style" />
                                                    <FooterStyle CssClass="grd_footer_style" />
                    
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom" 
                                                        PreviousPageText="Previous">
                                                    </PagerSettings>
                        
                                                    <Columns>
                                                        <asp:TemplateField>
        			                                        <ItemTemplate>
        						                                <asp:CheckBox id="chkSel" runat="server"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                
                                                        <asp:CommandField ShowSelectButton="True" />
                            
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_ILL_REC_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_ILL_FILE_NO" HeaderText="File No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />                                                        
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_ILLNESS_DATE" HeaderText="Illness Date" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" DataFormatString="{0:dd MMM yyyy}" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_ILLNESS_NAME" HeaderText="Illness Type" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                    </Columns>
   
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
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
</html>
