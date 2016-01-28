<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_GRP_CLM_ENTRY.aspx.vb"
    Inherits="Claims_PRG_LI_GRP_CLM_ENTRY" %>

<%@ Register Src="../UC_BANT.ascx" TagName="UC_BANT" TagPrefix="uc1" %>
<%@ Register Src="../UC_FOOT.ascx" TagName="UC_FOOT" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group Life Module</title>

    <script type="text/javascript" src="../JQ/jquery-1.10.2.js"></script>

    <script type="text/javascript" src="../JQ/jquery-ui.js"></script>

    <script type="text/javascript" src="../JQ/jquery.js"></script>

    <script type="text/javascript" src="../JQ/jquery.simplemodal.js"></script>

    <script type="text/javascript" src="../JQ/jquery-ui.css"></script>

    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js"></script>

    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js"></script>

    <script language="javascript" type="text/javascript" src="../Script/SJQ.js"></script>

    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
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
                        <tr>
                            <td align="left" colspan="2" valign="top" style="color: Red; font-weight: bold;">
                                <%=STRMENU_TITLE%>
                            </td>
                            <td align="right" colspan="1" valign="top" style="display: none;">
                                &nbsp;&nbsp;Status:&nbsp;<asp:TextBox ID="txtAction" Visible="true" ForeColor="Gray"
                                    runat="server" EnableViewState="False" Width="50px"></asp:TextBox>
                            </td>
                            <td align="right" colspan="1" valign="top">
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}" onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" Style="height: 26px" />
                                &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="150px" runat="server"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem>** Select Insured **</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4" valign="top">
                                &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('../MENU_GL.aspx?menu=GL_CLAIM')">Go
                                    to Menu</a> &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="cmdNew_ASP" CssClass="cmd_butt"
                                        runat="server" Text="New Data" OnClientClick="JSNew_ASP();"></asp:Button>
                                &nbsp;
                                <asp:Button ID="cmdSave_ASP" CssClass="cmd_butt" runat="server" Text="Save Data">
                                </asp:Button>
                                &nbsp;
                                <asp:Button ID="cmdDelete_ASP" CssClass="cmd_butt" Enabled="false" runat="server"
                                    Text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:Button>
                                &nbsp;
                                <asp:Button ID="cmdPrint_ASP" CssClass="cmd_butt" runat="server" Text="Print" PostBackUrl="~/I_LIFE/PRG_LI_REQ_ENTRY_RPT.aspx">
                                </asp:Button>
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
                <td nowrap class="myheader">
                    Claims Request Entry
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" class="td_menu">
                    <table align="center" border="0" class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <asp:Label ID="lblMsg0" ForeColor="Red" Font-Size="Small" runat="server">Status:</asp:Label>
                                <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1" colspan="4" style="background-color: #B0E0E6;
                                font-family: 'Century Gothic'; font-size: medium; font-weight: bold">
                                Group Policy Info.&nbsp;&nbsp;
                                <asp:Label ID="lblGrpName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:CheckBox ID="chkPolyNum" AutoPostBack="true" Text="Policy #:" runat="server" />
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtPolicyNumber" runat="server" Enabled="False" TabIndex="2"></asp:TextBox>
                                <asp:Button ID="cmdPolyNoGet" Enabled="false" Text="Get Record" runat="server" />
                                <asp:TextBox ID="txtRecNo0" Visible="false" Enabled="false" MaxLength="18" Width="40px"
                                    runat="server" Height="22px"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:CheckBox ID="chkClaimNum" AutoPostBack="true" Text="Claim #:" runat="server" />
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtClaimsNo" runat="server" Enabled="False" TabIndex="1"></asp:TextBox>
                                <asp:Button ID="cmdClaimNoGet" Enabled="false" Text="Get Record" runat="server" Style="height: 26px" />
                                <asp:TextBox ID="txtRecNo" Visible="false" Enabled="false" MaxLength="18" Width="40"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label1" runat="server" Text="Under Writing Year:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtUWY" runat="server" Width="80px" Enabled="False"></asp:TextBox>
                                <asp:TextBox ID="txtMemStaffNo" Visible="false" Enabled="false" MaxLength="18" Width="40"
                                    runat="server"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="Label2" runat="server" Text="Product Code:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtProductCode" runat="server" Enabled="False"></asp:TextBox>
                                <%--<br />--%>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label3" runat="server" Text="Policy Start Date: "></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtPolicyStartDate" runat="server" Enabled="False"></asp:TextBox>
                                <asp:ImageButton ID="butCal" runat="server" OnClientClick="OpenModal_Cal('../Calendar1.aspx?popup=YES',this.form.name,'txtTrans_Date','txtTrans_Date')"
                                    ImageUrl="~/I_LIFE/img/cal.gif" Height="17" Visible="False" />
                                <asp:Label ID="lblTrans_Date_Format" Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="Label4" runat="server" Text="Policy End Date:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtPolicyEndDate" runat="server" Enabled="False"></asp:TextBox>
                                <asp:ImageButton ID="butCal1" runat="server" OnClientClick="OpenModal_Cal('../Calendar1.aspx?popup=YES',this.form.name,'txtTrans_Date','txtTrans_Date')"
                                    ImageUrl="~/I_LIFE/img/cal.gif" Height="17" Visible="False" />
                                <asp:Label ID="lblTrans_Date_Format1" Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="lblFreeCovLmt" runat="server" Text="Free Cover Limit: "></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtFreeCovLmt" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="lblRetention" runat="server" Text="Retention: "></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtRetention" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="lblTotSA" runat="server" Text="Total Sum Assured: "></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtTotSA" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="lblTotPrem" runat="server" Text="Total Premium: "></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtTotPrem" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1" colspan="4" style="background-color: #B0E0E6;
                                font-family: 'Century Gothic'; font-size: medium; font-weight: bold">
                                Selected Member Info
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label5" runat="server" Text="Notification Date:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtNotificationDate" runat="server" TabIndex="3"></asp:TextBox>
                                <asp:ImageButton ID="butCal0" runat="server" OnClientClick="OpenModal_Cal('../Calendar1.aspx?popup=YES',this.form.name,'txtTrans_Date','txtTrans_Date')"
                                    ImageUrl="~/I_LIFE/img/cal.gif" Height="17" Visible="False" />
                                <asp:Label ID="lblTrans_Date_Format0" Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="lblDateOfDeath" runat="server" Text="Date of Death:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtDateOfDeath" runat="server" TabIndex="4"></asp:TextBox>
                                <asp:ImageButton ID="butCal2" runat="server" OnClientClick="OpenModal_Cal('../Calendar1.aspx?popup=YES',this.form.name,'txtTrans_Date','txtTrans_Date')"
                                    ImageUrl="~/I_LIFE/img/cal.gif" Height="17" Visible="False" />
                                <asp:Label ID="lblTrans_Date_Format2" Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="lblBasicSumClaimsLC" runat="server" Text="B. Sum Claimed LC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtBasicSumClaimsLC" runat="server" TabIndex="5" 
                                    Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="lblBasicSumClaimsFC" runat="server" Text="B. Claimed FC:"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtBasicSumClaimsFC" runat="server" TabIndex="6" 
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="lblPremPaidLC" runat="server" Text="Prem. Paid LC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtPremPaidLC" runat="server" TabIndex="7" Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="lblPremPaidFC" runat="server" Text="Prem. Paid FC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtPremPaidFC" runat="server" TabIndex="8" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="lblPremiumLoadedLC" runat="server" Text="Prem. Loaded LC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtPremiumLoadedLC" runat="server" TabIndex="7" 
                                    Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="lblPremiumLoadedFC" runat="server" Text="Prem. Loaded FC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtPremiumLoadedFC" runat="server" TabIndex="8" 
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="lblMemberName" runat="server" Text="Member Name:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtMemberName" runat="server" TabIndex="9" Width="271px" 
                                    Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="lblSysModule" runat="server" Text="System Module:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:DropDownList ID="DdnSysModule" runat="server" TabIndex="10" Width="150px">
                                    <asp:ListItem Value="G" Selected="True">Group Life</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="lblAssuredAge" runat="server" Text="Assured Age:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtAssuredAge" runat="server" TabIndex="9" Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="lblLossType" runat="server" Text="Loss Type:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:DropDownList ID="DdnLossType" runat="server" TabIndex="12" Width="108px">
                                    <asp:ListItem Selected="True">-- Select Item --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="lblClaimDec" runat="server" Text="Cause Of Death:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtClaimDec" runat="server" Height="59px" TextMode="MultiLine" Width="271px"
                                    TabIndex="13"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="lblRemark" runat="server" Text="Remarks:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtRemark" runat="server" Height="59px" TextMode="MultiLine" Width="271px"
                                    TabIndex="13"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1" colspan="4">
                                    <asp:Button ID="Cmd_Add_Benfry" runat="server" Text="Add Beneficiary" 
                                        style="float:right;" />
                                <asp:TextBox ID="txtFileNum" runat="server" TabIndex="9" Width="135px" 
                                        Visible="False"></asp:TextBox>
                                <asp:TextBox ID="txtQuote_Num" runat="server" TabIndex="9" Width="103px" 
                                        Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1" colspan="4">
                                &nbsp;<asp:Panel ID="Panel1" runat="server">
                                    <asp:Label ID="lblClaimDec0" runat="server" Text="Filter Option:"></asp:Label>
                                    <asp:DropDownList ID="DdnFilter" runat="server" Width="250px">
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                        <asp:ListItem Value="1">Insured Name</asp:ListItem>
                                        <asp:ListItem Value="2">Member No.</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtSvalue" runat="server" Width="250px"></asp:TextBox>
                                    <asp:Button ID="searchBtn" runat="server" Text="Search" />
                                </asp:Panel>
                            </td>
                        </tr>
                       <%-- <tr>
                            <td align="left" valign="top" class="style1" colspan="4">
                                <div align="left" style="background-color: White; color: White; border-bottom-style: ridge;
                                    height: 35px;">
                                   <table align="left" border="0" style="background-color: #1C5E55; width: 100%; height: 30px;">
                                        <tr style="font-size: medium; font-weight: bold;">
                                            <td align="left" style="width: 30px;">
                                                &nbsp;
                                            </td>
                                            <td align="left" style="width: 60px;">
                                                &nbsp;
                                            </td>
                                            <td align="center" style="width: 70px;">
                                                Ref.No Sum Assured
                                            </td>
                                            <td align="center" style="width: 100px;">
                                                DOB
                                            </td>
                                            <td align="center" style="width: 40px;">
                                                Age
                                            </td>
                                            <td align="center" style="width: 60px;">
                                                Rate
                                            </td>
                                            <td align="center" style="width: 80px;">
                                                Prem Amt
                                            </td>
                                            <td align="center" style="width: 80px;">
                                                Batch
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="left" valign="top" class="style1" colspan="4">
                                <div class="div_grid" style="height: 350px; overflow: scroll;">
                                    <asp:GridView ID="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl" DataKeyNames="TBIL_POL_MEMB_REC_ID"
                                        HorizontalAlign="Left" AutoGenerateColumns="False" PagerSettings-Position="TopAndBottom"
                                        PagerSettings-Mode="NextPreviousFirstLast" PagerSettings-FirstPageText="First"
                                        PagerSettings-NextPageText="Next" 
                                        PagerSettings-PreviousPageText="Previous" PagerSettings-LastPageText="Last"
                                        EmptyDataText="No data available..." ShowFooter="True">
                                        <PagerStyle CssClass="grd_page_style" />
                                        <HeaderStyle CssClass="grd_header_style" />
                                        <RowStyle CssClass="grd_row_style" />
                                        <SelectedRowStyle CssClass="grd_selrow_style" />
                                        <EditRowStyle CssClass="grd_editrow_style" />
                                        <AlternatingRowStyle CssClass="grd_altrow_style" />
                                        <FooterStyle CssClass="grd_footer_style" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom"
                                            PreviousPageText="Previous"></PagerSettings>
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSel" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowSelectButton="True" ItemStyle-Width="60px">
                                                <ItemStyle Width="40px"></ItemStyle>
                                            </asp:CommandField>
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MEMB_REC_ID" HeaderText="Ref.No"
                                                ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="70px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MEMB_STAFF_NO" HeaderText="Member No."
                                                ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="80px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MEMB_NAME" HeaderText="Member Name"
                                                ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="120px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MEMB_TOT_SA" HeaderText="Sum Assured"
                                                ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" DataFormatString="{0:N2}">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MEMB_BDATE" HeaderText="DOB"
                                                ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true"
                                                DataFormatString="{0:dd MMM yyyy}">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="60px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MEMB_AGE" HeaderText="Age" ItemStyle-Width="40px"
                                                HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="20px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MEMB_RATE" HeaderText="Prem Rate"
                                                ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="60px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MEMB_PREM" HeaderText="Prem Amount"
                                                DataFormatString="{0:N2}" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Left"
                                                ConvertEmptyStringToNull="true" Visible="False">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="60px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Prem. Amt" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTransAmt" runat="server" DataFormatString="{0:N2}" Text='<%#Eval("TBIL_POL_MEMB_PREM") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltxtTotal" runat="server" Text="0.00" DataFormatString="{0:N2}" />
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="80px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MEMB_BATCH_NO" HeaderText="Batch"
                                                ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MEMB_FLAG" HeaderText="STATUS"
                                               ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Left"
                                                ConvertEmptyStringToNull="true" Visible="True">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="60px"></ItemStyle>
                                            </asp:BoundField>
                                            <%--<asp:TemplateField HeaderText="Status" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text="" />
                                                </ItemTemplate>--%>
                                               <%-- <FooterTemplate>
                                                    <asp:Label ID="lbltxtTotal" runat="server" Text="0.00" DataFormatString="{0:N2}" />
                                                </FooterTemplate>--%>
                                                <%--<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="80px"></ItemStyle>
                                            </asp:TemplateField>--%>
                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                &nbsp;
                            </td>
                            <td align="left" valign="top" class="style2">
                                &nbsp;
                            </td>
                            <td align="left" valign="top" class="style3">
                                &nbsp;
                            </td>
                            <td align="left" valign="top" class="style2">
                                &nbsp;
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
                    <table align="center" border="0" class="footer" style="background-color: Black;">
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

    <script>
        $("#txtBasicSumClaimsLC").keypress(function(e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                //display error message
                alert("Invalid keyboard entry!");
                return false;
            }
        })

        $("#txtBasicSumClaimsFC").keypress(function(e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                //display error message
                alert("Invalid keyboard entry!");
                return false;
            }
        })

        $("#txtAdditionalSumClaimsLC").keypress(function(e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                //display error message
                alert("Invalid keyboard entry!");
                return false;
            }
        })

        $("#txtAdditionalSumClaimsFC").keypress(function(e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                //display error message
                alert("Invalid keyboard entry!");
                return false;
            }
        })

        $("#txtPremiumLoadedLC").keypress(function(e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                //display error message
                alert("Invalid keyboard entry!");
                return false;
            }
        })
        $("#txtPremiumLoadedLC").blur(function() {
            $("#txtPremiumLoadedFC").val($("#txtPremiumLoadedLC").val());
        })
        
    </script>

</body>
</html>
