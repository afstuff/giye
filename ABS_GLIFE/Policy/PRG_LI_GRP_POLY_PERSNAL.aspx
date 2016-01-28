<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_GRP_POLY_PERSNAL.aspx.vb" Inherits="PRG_LI_GRP_POLY_PERSNAL" %>

<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

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

<style type="text/css">    
 #myframe a.modalCloseImg {
	background:url(../Images/x.png) no-repeat; /* adjust url as required */
	cursor:pointer;
	display:inline;
	position:absolute;
	right:-10px;
	top:-10px;
	width:25px;
	height:29px;
	z-index:3200;
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

                        <tr>
                            <td align="left" colspan="2" valign="top" style="color: Red; font-weight: bold;"><%=STRMENU_TITLE%></td>
                            <td align="right" colspan="1" valign="top" style="display:none;">    
                                &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>
                            </td>
                            <td align="right" colspan="1" valign="top">    
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="250px" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top"><hr /></td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="center" colspan="4" valign="top">
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_GP_PROP_POLICY.aspx?menu=GL_QUOTE')">Go to Menu</a>
                                            &nbsp;&nbsp;<asp:Button ID="cmdPrev" CssClass="cmd_butt" Enabled="false" Text="«..Previous" runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt" Enabled="false"  runat="server" text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:button>
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
                    <td nowrap class="myheader">Personal Information</td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="td_menu">
                        <table align="center" border="0" class="tbl_menu_new">
                                            <tr>
                                                <td align="left" colspan="4" valign="top">
                                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr style="display:none;">
                                                <td align="left" colspan="4" valign="top" class="myMenu_Title">Product/Marketer Info.</td>
                                            </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:CheckBox ID="chkFileNum" AutoPostBack="true" Text="" runat="server" />
                                                    &nbsp;<asp:Label ID="lblFileNum" Text="File No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtFileNum" Enabled="false" Width="150px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Button ID="cmdFileNum" Enabled="false" Text="Get Record" runat="server" />
                                                    &nbsp;<asp:TextBox ID="txtRecNo" Visible="false" Enabled="false" MaxLength="18" Width="40" runat="server"></asp:TextBox></td>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="false" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtPolNum" Width="200px"
                                                        Enabled="false" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Button ID="cmdGetPol" Enabled="false" Text="Go" runat="server" />
                                                    
                                                </td>
                            </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblTrans_Date" Text="Quotation Date:" 
                                                        runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtTrans_Date" MaxLength="10" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="butCal" Visible="false"  runat="server" 
                                                        OnClientClick="OpenModal_Cal('../Calendar1.aspx?popup=YES',this.form.name,'txtTrans_Date','txtTrans_Date')"
                                                        ImageUrl="~/Images/Calendar.bmp" Height="17" />
                                                    &nbsp;<asp:Label ID="lblTrans_Date_Format" Text="dd/mm/yyyy" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top"><asp:Label ID="lblQuote_Num" Text="Quotation No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtQuote_Num" Width="200px" Enabled="true" runat="server"></asp:TextBox>
                                                </td>
                            </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblUWYear" Text="Underwriting Year:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtUWYear" Enabled="true" MaxLength="4" Width="80" runat="server"></asp:TextBox>
                                                </td>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblBusSource" Text="Business Source:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:DropDownList ID="cboBusSource" Width="150px" runat="server">
                                                    </asp:DropDownList>                                                    
                                                    &nbsp;<asp:TextBox ID="txtBusSource" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtBusSourceName" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox></td>    
                            </tr>
                        

                                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblProductClass" Text="Product Category:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboProductClass" AutoPostBack="true" CssClass="selProduct" runat="server" OnTextChanged="DoProc_ProductClass_Change"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtProductClass" Visible="false" Enabled="false" MaxLength="10" Width="50" runat="server"></asp:TextBox></td>                                            
                                                <td align="left" valign="top"><asp:Label ID="lblProduct_Num" Text="Product Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList id="cboProduct" AutoPostBack="true" CssClass="selProduct" runat="server" OnTextChanged="DoProc_Product_Change"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtProduct_Num" Visible="false" Enabled="false" MaxLength="10" Width="20px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtProduct_Name" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblCover_Num" Text="Cover Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboCover_Name" AutoPostBack="false" CssClass="selProduct" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtCover_Num" Visible="false" Enabled="true" MaxLength="10" Width="20" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtCover_Name" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>                                            
                                                <td align="left" valign="top"><asp:Label ID="lblPlan_Num" Text="Plan Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboPlan_Name" AutoPostBack="false" CssClass="selProduct" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtPlan_Num" Visible="false" Enabled="true" MaxLength="10" Width="20" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtPlan_Name" Visible="false" Enabled="false" Width="20" runat="server"></asp:TextBox></td>                                            
                                            </tr>

                                    <tr class="tr_frame_02">
                                        <td nowrap align="left" valign="top"><asp:Label ID="lblBroker_Search" Text="Search for Broker:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtBroker_Search" runat="server"></asp:TextBox>
                                            &nbsp;<asp:Button ID="cmdBroker_Search" Text="Search..." runat="server" OnClick="DoProc_Broker_Search" />
                                            &nbsp;<asp:DropDownList ID="cboBroker_Search" AutoPostBack="True" Width="400px" 
                                                runat="server" OnTextChanged="DoProc_Broker_Change"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblBrokerNum" Text="Broker Code:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtBrokerNum" AutoPostBack="true" MaxLength="10" runat="server" OnTextChanged="DoProc_Validate_Broker"></asp:TextBox>
                                            &nbsp;<asp:Label ID="lblBrokerName" Text="Full Name:" runat="server"></asp:Label>
                                            &nbsp;<asp:TextBox ID="txtBrokerName" Enabled="False" runat="server" Width="250px"></asp:TextBox>
                                            &nbsp;<input type="button" id="cmdBroker_Setup" name="cmdBroker_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('../Codes/PRG_GP_BRK_DTL.aspx?optid=001&optd=Brokers_Agents_Details&popup=YES')" />
                                            &nbsp;<input type="button" id="cmdBroker_Browse" name="cmdBroker_Browse" visible="false" style="display: none;" value="Browse..." onclick="javascript:Sel_Func_Open('BRK','../WebForm3.aspx?popup=YES','Form1','txtBrokerNum','txtBrokerName')" />
                                            &nbsp;<input type="button" id="btnBroker_Browse" name="btnBroker_Browse" visible="true" value="Browse..." onclick="javascript:ShowPopup('BRK_GL','../WebFormX.aspx?popup=NO','Form1','txtBrokerNum','txtBrokerName');" />
                                        </td>
                                    </tr>

                                    <tr class="tr_frame_02">
                                        <td nowrap align="left" valign="top"><asp:Label ID="lblAgcy_Search" Text="Search for Marketer:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtAgcy_Search" runat="server"></asp:TextBox>
                                            &nbsp;<asp:Button ID="cmdAgcy_Search" Text="Search..." runat="server" OnClick="DoProc_Agcy_Search" />
                                            &nbsp;<asp:DropDownList ID="cboAgcy_Search" AutoPostBack="True" Width="400px" 
                                                runat="server" OnTextChanged="DoProc_Agcy_Change"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblAgcyNum" Text="Marketer Code:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtAgcyNum" AutoPostBack="true" MaxLength="10" runat="server" OnTextChanged="DoProc_Validate_Agency"></asp:TextBox>
                                            &nbsp;<asp:Label ID="lblAgcyName" Text="Full Name:" runat="server"></asp:Label>
                                            &nbsp;<asp:TextBox ID="txtAgcyName" Enabled="False" runat="server" Width="250px"></asp:TextBox>
                                            &nbsp;<input type="button" id="cmdAgcy_Setup" name="cmdAgcy_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('../Codes/PRG_GP_MKT_CD.aspx?optid=001&optd=Marketers_Agency&popup=YES')" />
                                            &nbsp;<input type="button" id="cmdAgcy_Browse" name="cmdAgcy_Browse" visible="false" style="display: none;" value="Browse..." onclick="javascript:Sel_Func_Open('MKT','../WebForm3.aspx?popup=YES','Form1','txtAgcyNum','txtAgcyName')" />
                                            &nbsp;<input type="button" id="btnAgcy_Browse" name="btnAgcy_Browse" visible="true" value="Browse..." onclick="javascript:ShowPopup('MKT','../WebFormX.aspx?popup=NO','Form1','txtAgcyNum','txtAgcyName');" />
                                        </td>
                                    </tr>

                                    <tr style="display: none;">
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Assured/Customer Info</td>
                                    </tr>
                                    <tr class="tr_frame_02">
                                        <td align="left" valign="top"><asp:Label ID="lblAssured_Search" Text="Search for Assured:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtAssured_Search" runat="server"></asp:TextBox>
                                            &nbsp;<asp:Button ID="cmdAssured_Search" Text="Search..." runat="server" OnClick="DoProc_Assured_Search" />
                                            &nbsp;<asp:DropDownList ID="cboAssured_Search" AutoPostBack="True" Width="400px" 
                                                runat="server" OnTextChanged="DoProc_Assured_Change"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblAssuredNum" Text="Assured Code:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtAssured_Num" AutoPostBack="true" MaxLength="10" runat="server" OnTextChanged="DoProc_Validate_Assured"></asp:TextBox>
                                            &nbsp;<asp:Label ID="lblAssuredName" Text="Full Name:" runat="server"></asp:Label>
                                            &nbsp;<asp:TextBox ID="txtAssured_Name" Enabled="False" runat="server" Width="250px"></asp:TextBox>
                                            &nbsp;<input type="button" id="cmdAssured_Setup" name="cmdAssured_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('../Codes/PRG_GP_CUST_DTL.aspx?optid=001&optd=Customer_Details&popup=YES')" />
                                            &nbsp;<input type="button" id="cmdAssured_Browse" name="cmdAssured_Browse" visible="false" style="display: none;" value="Browse..." onclick="javascript:Sel_Func_Open('INS','../WebForm3.aspx?popup=YES','Form1','txtAssured_Num','txtAssured_Name')" />
                                            &nbsp;<input type="button" id="btnAssured_Browse" name="btnAssured_Browse" visible="true" value="Browse..." onclick="javascript:ShowPopup('INS_GL','../WebFormX.aspx?popup=NO','Form1','txtAssured_Num','txtAssured_Name');" />
                                        </td>
                                    </tr>

                                    <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblRetirement_Age" Text="Retirement_Age:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtRetirement_Age" Width="40px" MaxLength="3" runat="server"></asp:TextBox>
                                                </td>
                                    </tr>

                                    <tr style="display: none;">
                                        <td align="left" valign="top"><asp:Label ID="lblNationality" Text="Nationality:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3">
                                            <asp:DropDownList ID="cboNationality" Width="220px" runat="server" OnTextChanged="DoProc_Nationality_Change"></asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtNationality" Visible="false" MaxLength="4" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                            &nbsp;<asp:TextBox ID="txtNationalityName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                            &nbsp;<input type="button" id="cmdNationality_Setup" name="cmdNationality_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('PRG_LI_CODES.aspx?optid=001&optd=Nationality&popup=YES')" />
                                            &nbsp;&nbsp;<asp:Button ID="cmdNationality_Refresh" Text="Refresh" runat="server" OnClick="DoProc_Nationality_Refresh" />
                                        </td>
                                    </tr>

                                    <tr style="display: none;">
                                        <td nowrap align="left" valign="top"><asp:Label ID="lblOccupationClass" Text="Occupation Class:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3">
                                            <asp:DropDownList ID="cboOccupationClass" Width="220px" runat="server" OnTextChanged="DoProc_OccupationClass_Change"></asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtOccupationClass" Visible="false" MaxLength="4" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                            &nbsp;<asp:TextBox ID="txtOccupationClassName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                            &nbsp;<input type="button" id="cmdOccupationClass_Setup" name="cmdOccupationClass_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('PRG_LI_CODES.aspx?optid=007&optd=Occupation_Class&popup=YES')" />
                                            &nbsp;&nbsp;<asp:Button ID="cmdOccupationClass_Refresh" Text="Refresh" runat="server" OnClick="DoProc_OccupationClass_Refresh" />
                                        </td>
                                    </tr>

                                    <tr style="display: none;">
                                        <td nowrap align="left" valign="top"><asp:Label ID="lblOccupation" Text="Assured Occupation:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3">
                                            <asp:DropDownList ID="cboOccupation" Width="220px" runat="server" OnTextChanged="DoProc_Occupation_Change"></asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtOccupationNum" Visible="false" MaxLength="4" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                            &nbsp;<asp:TextBox ID="txtOccupationName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                            &nbsp;<input type="button" id="cmdOccupation_Setup" name="cmdOccupation_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('PRG_LI_CODES.aspx?optid=008&optd=Occupation&popup=YES')" />
                                            &nbsp;&nbsp;<asp:Button ID="cmdOccupation_Refresh" Text="Refresh" runat="server" OnClick="DoProc_Occupation_Refresh" />
                                        </td>
                                    </tr>

                                    <tr style="display: none;">
                                                <td align="left" valign="top"><asp:Label ID="lblGender" Text="Gender:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:DropDownList ID="cboGender" Width="150px" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtGender" Visible="false" Enabled="false" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtGenderName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                </td>    
                                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblMaritalStatus" Text="Marital Status:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:DropDownList ID="cboMaritalStatus" Width="150px" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtMaritalStatus" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtMaritalStatusName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                </td>
                                    </tr>

                                    <tr style="display: none;">
                                                <td align="left" valign="top"><asp:Label ID="lblDOB" Text="Date of Birth:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtDOB" MaxLength="10" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Label ID="lblDOB_Format" Text="dd/mm/yyyy" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:Label ID="lblDOB_ANB" Enabled="false" Text="Age (ANB):" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtDOB_ANB" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                </td>
                                    </tr>

                                    <tr style="display: none;">
                                                <td align="left" valign="top"><asp:Label ID="lblDOB_Place" Text="Place of Birth:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtDOB_Place" Visible="true" Enabled="true" MaxLength="25" Width="200px" runat="server"></asp:TextBox>
                                                </td>    
                                                <td align="left" valign="top"><asp:Label ID="lblDOB_Proof" Text="Proof of Age:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:DropDownList ID="cboDOB_Proof" Width="150px" runat="server">
                                                        <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                        <asp:ListItem Value="D">Drivers Licence</asp:ListItem>
                                                        <asp:ListItem Value="P">Passport</asp:ListItem>
                                                        <asp:ListItem Value="N">National ID Card</asp:ListItem>
                                                        <asp:ListItem Value="V">Voters Card</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtDOB_Proof" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtDOB_ProofName" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox>
                                                </td>    
                                    </tr>

                                    <tr style="display: none;">

                                                <td align="left" valign="top"><asp:Label ID="lblReligion" Text="Religion/Belief:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:DropDownList ID="cboReligion" Width="150px" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtReligion" Visible="false" Enabled="false" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtReligionName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                </td>    
                                                <td align="left" valign="top" style="display:none;"><asp:Label ID="lblRelation" Text="Relation:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1" style="display: none;">
                                                    <asp:DropDownList ID="cboRelation" Width="150px" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtRelation" Visible="false" Enabled="false" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtRelationName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                </td>    
                                    </tr>
                                    
                                    <tr style="display: none;">
                                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblHeight" Text="Height:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtHeight" Enabled="true" MaxLength="5" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:DropDownList ID="cboHeight" Width="100px" runat="server">
                                                            <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                            <asp:ListItem Value="F">Feet</asp:ListItem>
                                                            <asp:ListItem Value="M">Meters</asp:ListItem>
                                                          </asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtHeight_Type" Visible="false" Enabled="true" MaxLength="2" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtHeight_TypeName" Visible="false" Enabled="true" Width="40px" runat="server"></asp:TextBox>
                                                </td>    
                                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblWeight" Text="Weight:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtWeight" Enabled="true" MaxLength="5" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:DropDownList ID="cboWeight" Width="100px" runat="server">
                                                            <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                            <asp:ListItem Value="K">Kilo</asp:ListItem>
                                                            <asp:ListItem Value="P">Pound</asp:ListItem>
                                                          </asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtWeight_Type" Visible="false" Enabled="true" MaxLength="2" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtWeight_TypeName" Visible="false" Enabled="true" Width="40px" runat="server"></asp:TextBox>
                                                </td>
                                    </tr>

                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Business Source Info</td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblBraNum" Text="Branch:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3">
                                            <asp:DropDownList ID="cboBranch" Width="220px" runat="server" OnTextChanged="DoProc_Branch_Change"></asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtBraNum" Visible="false" Enabled="true" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                            &nbsp;<asp:TextBox ID="txtBraName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                            &nbsp;<input type="button" id="cmdBranch_Setup" name="cmdBranch_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('../Codes/PRG_GP_CODES.aspx?optid=003&optd=Branch&popup=YES')" />
                                            &nbsp;&nbsp;<asp:Button ID="cmdBranch_Refresh" Text="Refresh" runat="server" OnClick="DoProc_Branch_Refresh" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblDeptNum" Text="Department:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:DropDownList ID="cboDepartment" Width="220px" runat="server" OnTextChanged="DoProc_Dept_Change"></asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtDeptNum" Visible="false" Enabled="true" MaxLength="4" runat="server" Width="40px"></asp:TextBox>
                                            &nbsp;<asp:TextBox ID="txtDeptName" Visible="false" Enabled="false" runat="server" Width="40px"></asp:TextBox>
                                            &nbsp;<input type="button" id="cmdDept_Setup" name="cmdDept_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('../Codes/PRG_GP_CODES.aspx?optid=005&optd=Department&popup=YES')" />
                                            &nbsp;&nbsp;<asp:Button ID="cmdDept_Refresh" Text="Refresh" runat="server" OnClick="DoProc_Dept_Refresh" />
                                        </td>
                                    </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblProStatus" Enabled="false" Text="Proposal Status:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="3">
                                                    <asp:DropDownList ID="cboProStatus" Width="150px" Enabled="false" runat="server">
                                                        <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                        <asp:ListItem Value="P">Proposal</asp:ListItem>
                                                        <asp:ListItem Value="A">Active Policy</asp:ListItem>
                                                        <asp:ListItem Value="W">Waiver</asp:ListItem>
                                                        <asp:ListItem Value="C">Canceled</asp:ListItem>
                                                        <asp:ListItem Value="L">Lapse Policy</asp:ListItem>
                                                        <asp:ListItem Value="R">Reactivated policy</asp:ListItem>
                                                        <asp:ListItem Value="U">Paid Up</asp:ListItem>
                                                    </asp:DropDownList>                                                    
                                                    &nbsp;<asp:TextBox ID="txtProStatus" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtProStatusName" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox>
                                                </td>    
                            </tr>

                                    <tr>
                                        <td align="left" colspan="4" valign="top">&nbsp;</td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" colspan="4" valign="top">
                                            <div id="div_jq_dialog" title="Codes Setup">
                                                Codes Setup
                                                <br />
                                                <input type="button" name="btnJQ_Close" id="btnJQ_Close" value="Close Dialogue" />
                                                <hr />
                                                <table id="table_01" border="0">
                                                    <tr>
                                                      <td>Full Name:</td>
                                                      <td><input type="text" id="txtname" name="txtname" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                      <td>Email Address:</td>
                                                      <td><input type="text" id="txtemail" name="txtemail" /></td>
                                                    </tr>
                                                    <tr><td colspan="2"></td></tr>                                                                                                      
                                                  </table>                                                
                                            </div> 
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Re Insurance / Treaty 
                                            info</td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblRetention" Text="Retention:" 
                                                runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3">
                                                    <asp:TextBox ID="txtRetention" Visible="true" Enabled="true" 
                                                MaxLength="25" Width="142px" runat="server" AutoPostBack="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblFreeMedCovLmt" 
                                                Text="Free Medical Cover Limit:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3">
                                                    <asp:TextBox ID="txtFreeMedCovLmt" Visible="true" Enabled="true" 
                                                MaxLength="25" Width="142px" runat="server" AutoPostBack="True"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblCompShare" 
                                                Text="CLA Share %:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3">
                                                    <asp:TextBox ID="txtCompShare" Visible="true" Enabled="true" 
                                                MaxLength="25" Width="53px" runat="server"></asp:TextBox>
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
