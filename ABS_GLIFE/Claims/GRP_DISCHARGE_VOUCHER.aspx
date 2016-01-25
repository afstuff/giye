<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GRP_DISCHARGE_VOUCHER.aspx.vb" Inherits="GRP_DISCHARGE_VOUCHER" %>

<%@ Register Src="~/UC_BAN.ascx" TagName="UC_BAN" TagPrefix="uc1" %>
<%@ Register Src="~/UC_FOOT.ascx" TagName="UC_FOOT" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PREMIUM DISCHARGE VOUCHER</title>

    <script type="text/javascript" src="~/Cal/calendar_eu.js"></script>

    <link rel="stylesheet" type="text/css" href="../../Cal/calendar.css" />
    <link rel="Stylesheet" href="~/SS_ILIFE.css" type="text/css" />
   
    <style type="text/css">
        .style2 {
            width: 107px;
        }
    </style>
</head>
<body onload="<%= FirstMsg %>">
    <form id="Form1" name="Form1" runat="server">
        <!-- start banner -->
        <div id="div_banner" align="center">
            <uc1:UC_BAN ID="UC_BAN1" Visible="true" runat="server" />
        </div>
        <!-- content -->
        <div id="div_content" align="center">
            <table id="tbl_content" align="center" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" colspan="4" class="tbl_buttons">
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
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
                                &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="True" Width="150px" runat="server" AppendDataBoundItems="True">
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
                                &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('../MENU_GL.aspx?menu=GL_CLAIM')">
                                   <%-- Go to Menu--%>
                                </a> &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="cmdNew_ASP" CssClass="cmd_butt"
                                        runat="server" Text="New Data" OnClientClick="JSNew_ASP();">
                                </asp:Button>
                                &nbsp;
                                <asp:Button ID="cmdSave_ASP" CssClass="cmd_butt" runat="server" Text="Save Data"></asp:Button>
                                &nbsp;
                                <asp:Button ID="cmdDelete_ASP" CssClass="cmd_butt" Enabled="False" runat="server"
                                    Text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:Button>
                                &nbsp;
                                <asp:Button ID="cmdPrint_ASP" CssClass="cmd_butt" runat="server" Text="Print" 
                                    PostBackUrl="~/I_LIFE/PRG_LI_CLM_PART_MATURE_RPT.aspx">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <style>
                    .comboBx {
                        margin-top: 10px;
                    }

                    .style3 {
                        width: 279px;
                    }

                    .style4 {
                        width: 143px;
                    }
                    .auto-style1 {
                    }
                    .auto-style2 {
                        width: 301px;
                    }
                    .auto-style3 {
                        width: 301px;
                        height: 23px;
                    }
                    .auto-style4 {
                        height: 23px;
                    }
                    .auto-style5 {
                        height: 25px;
                    }
                </style>
                <tr>
                    <td align="center" colspan="4" valign="top" class="td_menu">
                        <table align="center" border="0" cellpadding="1" cellspacing="1" class="tbl_menu_new">
                            <tr>
                                <td align="left" colspan="2" class="myMenu_Title">
                                    <%=STRPAGE_TITLE%>
                                    <asp:Label ID="Label1" runat="server" Text="+++ PREMIUM DISCHARGE VOUCHER +++"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" nowrap colspan="2">
                                    <asp:Label ID="lblMessage" Text="Staus:" runat="server" Font-Size="Small" ForeColor="Red"
                                        Font-Bold="True"></asp:Label>
                                    &nbsp;<a id="PageAnchor_Return_Link" runat="server" class="a_return_menu" href="#"
                                        style="float: right;" visible="False">Returns to Previous Page</a> &nbsp;<%=PageLinks%>&nbsp;
                                    <%--onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=il_code_cust')"--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap class="auto-style5">
                                    <asp:Label ID="Label9" runat="server" Text="Assured Name:"></asp:Label>
                                </td>
                                <td align="left" nowrap class="auto-style5">
                                    <asp:Label ID="lblAssured" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap class="auto-style4">
                                    <asp:Label ID="Label10" runat="server" Text="Group Name:"></asp:Label>
                                </td>
                                <td align="left" nowrap class="auto-style4">
                                    <asp:Label ID="lblGroup" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap>
                                    <asp:Label ID="Label11" runat="server" Text="Claim Number:"></asp:Label>
                                </td>
                                <td align="left" nowrap>
                                    <asp:Label ID="lblClaim" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap>
                                    <asp:Label ID="Label12" runat="server" Text="Policy Number:"></asp:Label>
                                </td>
                                <td align="left" nowrap>
                                    <asp:Label ID="lblPolicy" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td align="right" nowrap>
                                    <asp:Label ID="Label15" runat="server" Text="Member Number:"></asp:Label>
                                </td>
                                <td align="left" nowrap>
                                    <asp:TextBox ID="TextBox3" runat="server" Width="300px"></asp:TextBox>
                                    <asp:Button ID="searchMemberNumBtn" runat="server" Text="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" nowrap colspan="2" class="auto-style1" style="padding: 5px; background-color: #006699; font-family: 'Century Gothic'; color: #FFFFFF; font-weight: bold">
                                    <asp:Label ID="Label2" runat="server" Text="DISCHARGE VOUCHER"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap class="auto-style2">
                                    <asp:Label ID="Label3" runat="server" Text="MCCD:"></asp:Label>
                                </td>
                                <td align="left" nowrap>
                                    <asp:RadioButtonList ID="rbtMCCD" runat="server" RepeatDirection="Horizontal" Width="300px">
                                        <asp:ListItem Value="0">Waived</asp:ListItem>
                                        <asp:ListItem Value="1">Submited</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap class="auto-style2">
                                    <asp:Label ID="Label4" runat="server" Text="BURIAL CERT./ATTESTATION:"></asp:Label>
                                </td>
                                <td align="left" nowrap>
                                    <asp:RadioButtonList ID="rbtBurial" runat="server" RepeatDirection="Horizontal" Width="300px">
                                        <asp:ListItem Value="0">Waived</asp:ListItem>
                                        <asp:ListItem Value="1">Submited</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap class="auto-style2">
                                    <asp:Label ID="Label5" runat="server" Text="POLICE REPORT:"></asp:Label>
                                </td>
                                <td align="left" nowrap>
                                    <asp:RadioButtonList ID="rbtPolice" runat="server" RepeatDirection="Horizontal" Width="300px">
                                        <asp:ListItem Value="0">Waived</asp:ListItem>
                                        <asp:ListItem Value="1">Submited</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap class="auto-style2">
                                    <asp:Label ID="Label6" runat="server" Text="DEATH CERTIFICATE:"></asp:Label>
                                </td>
                                <td align="left" nowrap>
                                    <asp:RadioButtonList ID="rbtDeath" runat="server" RepeatDirection="Horizontal" Width="300px">
                                        <asp:ListItem Value="0">Waived</asp:ListItem>
                                        <asp:ListItem Value="1">Submited</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap class="auto-style3">
                                    <asp:Label ID="Label7" runat="server" Text="KYC OF DEATH:"></asp:Label>
                                </td>
                                <td align="left" nowrap class="auto-style4">
                                    <asp:RadioButtonList ID="rbtKyc" runat="server" RepeatDirection="Horizontal" Width="300px">
                                        <asp:ListItem Value="0">Waived</asp:ListItem>
                                        <asp:ListItem Value="1">Submited</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap class="auto-style2">
                                    <asp:Label ID="Label8" runat="server" Text="BENEFICIARY:"></asp:Label>
                                </td>
                                <td align="left" nowrap>
                                    <asp:RadioButtonList ID="rbtBeneficiary" runat="server" RepeatDirection="Horizontal" Width="300px">
                                        <asp:ListItem Value="0">Waived</asp:ListItem>
                                        <asp:ListItem Value="1">Submited</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap class="auto-style2">&nbsp;
                                </td>
                                <td align="left" nowrap>&nbsp;
                                    </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap class="auto-style2">&nbsp;</td>
                                <td align="left" nowrap>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
            </table>
        </div>
        <!-- footer -->
        <div id="div_footer" align="center">
            <table id="tbl_footer" align="center">
                <tr>
                    <td valign="top">
                        <table align="center" border="0" class="footer" style="background-color: Black;">
                            <tr>
                                <td>
                                    <uc2:UC_FOOT ID="UC_FOOT" runat="server" />
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
