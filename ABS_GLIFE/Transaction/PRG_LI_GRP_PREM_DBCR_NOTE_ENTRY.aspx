<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_GRP_PREM_DBCR_NOTE_ENTRY.aspx.vb" Inherits="Transaction_PRG_LI_GRP_PREM_DBCR_NOTE_ENTRY" %>

<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DN/CN Transaction</title>
    
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js"></script>
    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js"></script>

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
                            <td align="left" colspan="2" valign="top" style="color: Red; font-weight: bold;"><%=PageTitle%></td>
                            <td align="right" colspan="1" valign="top">    
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="250px" 
                                    runat="server" AppendDataBoundItems="True"></asp:DropDownList>
    	                        &nbsp;<asp:TextBox ID="txtCode" Visible="false" Width="40px" runat="server"></asp:TextBox>
                            </td>
                            <td align="right" colspan="1" valign="top" style="display:none;">    
                                &nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>&nbsp;
                            </td>
                        </tr>

                                    <tr style="display: none;">
                                        <td align="left" colspan="4" valign="top"><hr /></td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="center" colspan="4" valign="top">
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('../MENU_GL.aspx?menu=GL_UND')">Go to Menu</a>
                                            &nbsp;&nbsp;<asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt" Enabled="false"  runat="server" text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" CssClass="cmd_butt" Enabled="False" runat="server" text="Print"></asp:button>
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
    <td nowrap class="myheader">Debit Note and Credit Note Information</td>
    </tr>

    <tr>
        <td align="center" valign="top" class="td_menu">

        <table class="tbl_menu_new" align="center" cellspacing="1" cellpadding="1" border="0">
            <tr>
       	        <td nowrap align="left" colspan="4"><asp:Label id="lblMessage" Text="Status:" runat="server" Font-Size="Medium" ForeColor="Red" Font-Bold="false"></asp:Label></td>
    	   	</tr>

    		<tr>
    	        <td nowrap align="left"><asp:CheckBox ID="chkTransum" AutoPostBack="true" Text="" runat="server" />
    	        <asp:Label ID="lblTransNum" runat="server">D-Note/C-Note No:</asp:Label></td>
    		    <td nowrap  align="left" colspan="3">
    		        <asp:textbox id="txtTransNum" Enabled="false" MaxLength="10" AutoPostBack="true" Width="130px" runat="server" EnableViewState="true"></asp:textbox>
    		        &nbsp;<asp:Button ID="cmdTransNum" Enabled="false" Text="Get Record" runat="server" style="height: 26px" />
       		        &nbsp;<asp:Label ID="lblTransNum_Remarks" ForeColor="Red" Text="Note: DN/CN is Auto Generated" runat="server"></asp:Label>
    		    </td>
    		</tr>
    		    		<tr>
    	        <td nowrap align="left"><asp:Label ID="lblPolNum" runat="server">Policy Number:</asp:Label>&nbsp;</td>
    		    <td nowrap align="left" colspan="3">
    		        <asp:textbox id="txtPolNum" AutoPostBack="true" MaxLength="35" Width="200px" runat="server" EnableViewState="true" OnTextChanged="DoProc_Validate_Policy"></asp:textbox>
    		        &nbsp;<asp:textbox id="txtRiskNum" Enabled="false" Visible="false" MaxLength="5" Width="20px" runat="server" EnableViewState="true"></asp:textbox>
    		        &nbsp;Find:&nbsp;<asp:textbox id="txtInsuredName" MaxLength="50" Width="120px" runat="server" EnableViewState="true"></asp:textbox>
    		        &nbsp;<asp:Button ID="cmdInsuredSearch" Text="Search" runat="server" /><%--OnClick="DoProc_Insured_Search"--%>
    		        &nbsp;<asp:DropDownList id="cboInsuredName" AutoPostBack="true" runat="server" 
                        Width="220px" AppendDataBoundItems="True"></asp:DropDownList>
                </td>    		        
    		</tr>

    		<tr>
    		    <td nowrap align="left" colspan="1"><asp:Label ID="lblMemberBatchNum" runat="server">Members Batch No:</asp:Label></td>
    		    <td nowrap align="left" colspan="3">
    		        <asp:textbox id="txtMemberBatchNum" Visible="true" MaxLength="10" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
    		        &nbsp;<asp:DropDownList id="cboMemberBatchNum" AutoPostBack="true" Width="200px" runat="server"></asp:DropDownList>
    		        &nbsp;<asp:Button ID="cmdGetBatchList" Enabled="true" Text="Refresh" runat="server" />
    		        &nbsp;<asp:textbox id="txtMemberBatchName" Visible="false" MaxLength="30" Enabled="false" Width="40px" runat="server" EnableViewState="true" ></asp:textbox>&nbsp;&nbsp;<asp:textbox id="txtInsuredNum" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server" EnableViewState="true"></asp:textbox>&nbsp;<asp:textbox id="txtSubRiskNum" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server" EnableViewState="true"></asp:textbox></td>
    		</tr>

    		<tr>
    	        <td nowrap align="left"><asp:Label ID="lblStartDate" runat="server">Start Date:</asp:Label></td>
    		    <td nowrap align="left" colspan="1">
    		        <asp:textbox id="txtStartDate" MaxLength="10" Width="100px" runat="server" EnableViewState="false"></asp:textbox>
    		        &nbsp;<input id="PickStartDate" name="cmdStartDte" type="button" value="..." runat="server" />
    		        &nbsp;<asp:Label ID="lblStartDate_Format" ForeColor="Blue" Text="dd/mm/yyyy" runat="server"></asp:Label></td>    
    		    <td nowrap align="left" colspan="1"><asp:Label ID="lblEndDate" runat="server">Expiry Date:</asp:Label></td>
    		    <td nowrap align="left" colspan="1">
    		        <asp:textbox id="txtEndDate" MaxLength="10" Width="100px" runat="server" EnableViewState="false"></asp:textbox>
    		        &nbsp;<input id="PickEndDate" name="cmdEndDte" type="button" value="..." runat="server" />
    		        &nbsp;<asp:Label ID="lblEndDate_Format" ForeColor="Blue" Text="dd/mm/yyyy" runat="server"></asp:Label></td>
    		</tr>
    		<tr>
    		    <td nowrap align="left" colspan="1">
    	            <asp:Label ID="lblRWDate" ToolTip="** Next Renewal Date **" runat="server">Renewal Date:</asp:Label>
    		    </td>
    		    <td nowrap align="left" colspan="3">
    	            <asp:textbox id="txtRWDate" ToolTip="** Next Renewal Date **" MaxLength="10" Width="100px" runat="server" EnableViewState="false"></asp:textbox>
    	            &nbsp;<input id="PickRWDate" name="cmdRWDte" type="button" value="..." runat="server" />
    		        &nbsp;<asp:Label ID="lblRWDate_Format" ForeColor="Blue" Text="dd/mm/yyyy" runat="server"></asp:Label></td>
    		</tr>

    		<tr>
    	        <td nowrap align="left"><asp:Label ID="lblAgcyNum" runat="server">Broker Code:</asp:Label></td>
    		    <td nowrap align="left" colspan="3">
    		        <asp:textbox id="txtAgcyNum" MaxLength="10" Width="120px" AutoPostBack="true" runat="server" EnableViewState="true" OnTextChanged="DoProc_Validate_Broker"></asp:textbox>
    		        &nbsp;<asp:textbox id="txtAgcyType" Visible="false" Enabled="false" MaxLength="2" Width="20px" runat="server" EnableViewState="true" ></asp:textbox>&nbsp;Find:&nbsp;
    		        &nbsp;<asp:TextBox ID="txtBroker_Search" Width="120px" runat="server"></asp:TextBox>
    		        &nbsp;<asp:Button ID="cmdBroker_Search" Text="Search" runat="server" OnClick="DoProc_Broker_Search" />
                    &nbsp;<asp:DropDownList id="cboAgcyName" AutoPostBack="true" runat="server" Width="220px" OnTextChanged="DoProc_Broker_Change"></asp:DropDownList>
                    &nbsp;<input id="cmdAgcy" name="cmdAgcyNum" type="button" value="..." runat="server" />
    		        &nbsp;<asp:textbox id="txtAgcyName" Visible="false" Enabled="false" MaxLength="30" Width="40px" runat="server" EnableViewState="true" ></asp:textbox></td>
    		</tr>


    		<tr>
    	        <td nowrap align="left"><asp:Label ID="lblTransType" runat="server">Transaction Type:</asp:Label></td>
    	        <td nowrap align="left">    	            
    		        <asp:DropDownList id="cboTransType" runat="server" Width="180px"></asp:DropDownList>
    		        &nbsp;<asp:textbox id="txtTransType" Enabled="false" MaxLength="1" Width="30px" 
                        runat="server" EnableViewState="true" Visible="False"></asp:textbox>
    		        &nbsp;<asp:TextBox ID="txtTransTypeName" Enabled="false" Width="30px" 
                        runat="server" Visible="False"></asp:TextBox>
    		    </td>
    	        <td nowrap align="left" colspan="1">
    	            <asp:Label ID="lblTransCode" runat="server">DN/CN Code:</asp:Label></td>
    	        <td nowrap align="left" colspan="1">
    	            <asp:DropDownList id="cboTransCode" runat="server" Width="120px"></asp:DropDownList>
    	            &nbsp;<asp:textbox id="txtTransCode" Visible="false" Enabled="false" MaxLength="2" Width="30px"  runat="server" EnableViewState="true"></asp:textbox>    		        
    	            &nbsp;<asp:TextBox ID="txtTransCodeName" Visible="false" Enabled="false" Width="30px" runat="server"></asp:TextBox>
    		        &nbsp;<asp:TextBox ID="txtRecNo" Visible="false" Enabled="false" MaxLength="18" Width="40" runat="server"></asp:TextBox>
    		    </td>
    		</tr>

	        <tr>
    	        <td nowrap align="left"><asp:Label ID="lblBusType" runat="server">Business Type:</asp:Label></td>
    		    <td nowrap align="left">
    		        <asp:DropDownList id="cboBusType" runat="server" Width="140px"></asp:DropDownList>
    		        &nbsp;<asp:textbox id="txtBusType" Visible="false" Enabled="false" MaxLength="2" Width="30px" runat="server" EnableViewState="true"></asp:textbox>&nbsp;<asp:textbox id="txtBusTypeName" Visible="false" Enabled="false" MaxLength="2" Width="30px" runat="server" EnableViewState="true"></asp:textbox></td>
    	        <td nowrap align="left" colspan="1"><asp:Label ID="lblRefNum" Enabled="false" runat="server">Ref. DN/CN No:</asp:Label></td>
    		    <td nowrap align="left">
    	            <asp:textbox id="txtRefNum"  Enabled="true" ToolTip="** Enter the reference no of the debit note or credit note you want to reverse or return **" MaxLength="10" Width="100px" AutoPostBack="true" runat="server" EnableViewState="true"></asp:textbox>
    	            &nbsp;<asp:TextBox ID="txtRefCode" MaxLength="2" Enabled="true"  Width="30px" runat="server" EnableViewState="true"></asp:TextBox>&nbsp;<asp:textbox id="txtRefDate" Enabled="true" MaxLength="10" Width="100px" runat="server" EnableViewState="true"></asp:textbox></td>
    		</tr>

    		<tr style="display:none;">
    		    <td nowrap align="left" colspan="1"><asp:Label ID="lblSecNum" runat="server">Sectors:</asp:Label></td>
    		    <td nowrap align="left" colspan="3">
    		        <asp:DropDownList id="cboSecName" Width="250px" runat="server"></asp:DropDownList>
                    &nbsp;<asp:textbox id="txtSecNum" MaxLength="5" Width="30px" runat="server" 
                        EnableViewState="true"></asp:textbox>                    
    		        &nbsp;<asp:textbox id="txtSecName" MaxLength="30" Enabled="false" Width="40px" 
                        runat="server" EnableViewState="true" ></asp:textbox>
    		    </td>
    	    </tr>

    		<tr>
    	        <td nowrap align="left"><asp:Label ID="lblTransDate" runat="server">Billing Date:</asp:Label>&nbsp;</td>
    		    <td nowrap align="left">
    		        <asp:textbox id="txtTransDate" MaxLength="10" Width="100px" runat="server" EnableViewState="false"></asp:textbox>
    		        &nbsp;<input id="PickTransDate" name="cmdTransDte" type="button" value="..." runat="server" />
    		        &nbsp;<asp:Label ID="lblTransDate_Format" ForeColor="Blue" Text="dd/mm/yyyy" runat="server"></asp:Label>
    		    </td>
    	        <td nowrap align="left"><asp:Label ID="lblBraNum" runat="server">Branch Code:</asp:Label>&nbsp;</td>
    		    <td nowrap align="left">
    		        <asp:DropDownList id="cboBranchName" Width="200px" runat="server"></asp:DropDownList>
    		        &nbsp;<asp:textbox id="txtBraNum" Visible="false" MaxLength="5" Width="40px" runat="server" EnableViewState="true"></asp:textbox>&nbsp;<asp:textbox id="txtLocNum" Visible="false" Enabled="false" MaxLength="5" Width="20px" runat="server" EnableViewState="true"></asp:textbox>&nbsp;<asp:textbox id="txtBraName" Visible="false" Enabled="false" MaxLength="30" Width="40px" runat="server" EnableViewState="true" ></asp:textbox>&nbsp;
    		    </td>
    		</tr>    

    		<tr>
    	        <td nowrap align="left"><asp:Label ID="lblTrans_Full_SI" runat="server">Full Sum Assured:</asp:Label></td>
    	        <td nowrap align="left" colspan="1">
    	            <asp:textbox id="txtTrans_Full_SI" MaxLength="13" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
    	        </td>
    	        <td nowrap align="left" colspan="1"><asp:Label ID="lblTrans_Full_Prem" runat="server">Full Gross Premium:</asp:Label></td>
    	        <td nowrap align="left" colspan="1">    
    	            <asp:textbox id="txtTrans_Full_Prem" MaxLength="13" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
    	        </td>     
    		 </tr>       

             <tr>
    	        <td nowrap align="left" colspan="1"><asp:Label ID="lblTrans_Rate" runat="server">Your Company Share %:</asp:Label></td>
    		    <td nowrap align="left" colspan="1">
    	            <asp:textbox id="txtTrans_Rate" MaxLength="5" Width="80px" runat="server" EnableViewState="true" TabIndex="31"></asp:textbox>
    	        </td>     
    	        <td nowrap align="left" colspan="2"><asp:Button ID="cmdTrans_Calculation" Text="Calculate Your SA and Premium" runat="server" /></td>
             </tr>

    		<tr style="background-color: Maroon; color: White; font-weight: bold;">
    	        <td nowrap align="left"><asp:Label ID="lblSumIns" runat="server">Your Sum Assured:</asp:Label></td>
    	        <td nowrap align="left" colspan="1">
    	            <asp:textbox id="txtSumIns" MaxLength="13" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
    	        </td>
    	        <td nowrap align="left" colspan="1"><asp:Label ID="lblGrsPrem" runat="server">Your Gross Premium:</asp:Label></td>
    	        <td nowrap align="left" colspan="1">    
    	            <asp:textbox id="txtGrsPrem" MaxLength="13" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
    	        </td>     
    		 </tr>       


             <tr>
    	        <td nowrap align="left" colspan="1"><asp:Label ID="lblAgcyRate" runat="server">Commission Rate:</asp:Label></td>
    		    <td nowrap align="left" colspan="3">
    	            <asp:textbox id="txtAgcyRate" MaxLength="5" Width="80px" Enabled="false" runat="server" EnableViewState="true" TabIndex="31"></asp:textbox>
    	        </td>     
             </tr>

             <tr>
                <td nowrap align="left"><asp:CheckBox ID="chkProrataYN" ForeColor="Red" Text="Prorate Premium" AutoPostBack="true" runat="server" />&nbsp;</td>
                <td nowrap align="left" colspan="3">
                    <asp:Label ID="lblProRataNDay" Enabled="false" runat="server">No of Day(s) to Pro-rate:</asp:Label>
                    <asp:textbox id="txtProRataNDay" MaxLength="5" Width="50px" runat="server" 
                        EnableViewState="true"></asp:textbox>
    	            &nbsp;<asp:Label ID="lblProRataRDay" Enabled="false" runat="server">Total Risk Day(s):</asp:Label>&nbsp;<asp:textbox 
                        id="txtProRataRDay" MaxLength="5" Width="50px" runat="server" 
                        EnableViewState="true"></asp:textbox>&nbsp;<asp:Label ID="lblTransAmt" Enabled="false" runat="server">Prorata Premium:</asp:Label>&nbsp;<asp:textbox 
                        id="txtTransAmt" MaxLength="13" Width="100px" runat="server" 
                        EnableViewState="true"></asp:textbox></td>
    		</tr>
    		<tr>
    	        <td nowrap align="left"><asp:Label ID="lblTransDescr1" runat="server">Description Line-1:</asp:Label></td>
    	        <td nowrap align="left" colspan="3">
    		        <asp:textbox id="txtTransDescr1" MaxLength="95" Width="600px" runat="server" EnableViewState="true"></asp:textbox>
    		    </td>
    		</tr>
    		<tr style="display: none;">
    	        <td nowrap align="left" colspan="1"><asp:Label ID="lblTransDescr2" runat="server">Description Line-2:</asp:Label></td>
    	        <td nowrap align="left" colspan="3">
    	            <asp:textbox id="txtTransDescr2" MaxLength="40" Width="350px" runat="server" EnableViewState="true"></asp:textbox>
    	        </td>     
    		</tr>


            <tr style="display: none;">                
       	        <td nowrap align="left" colspan="4" class="myMenu_Title">Miscellaneous Information</td>
    	   	</tr>
    	   	
            <tr style="display: none;">
                <td nowrap align="left" colspan="4">
                    <table border="1">
                        <tr>
                            <td align="left" valign="top"><asp:Label ID="lblFileNum" Enabled="true" Text="File No:" runat="server"></asp:Label></td>
                            <td align="left" valign="top"><asp:TextBox ID="txtFileNum" Enabled="false" Width="150px" runat="server"></asp:TextBox></td>
                            <td align="left" valign="top"><asp:Label ID="lblQuote_Num" Enabled="true" Text="Quotation No:" runat="server"></asp:Label></td>
                            <td align="left" valign="top"><asp:TextBox ID="txtQuote_Num" Enabled="false" Width="200px" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            <tr style="display: none;">                
       	        <td nowrap align="left" colspan="4" class="myMenu_Title">Treaty Information</td>
    	   	</tr>
    		<tr style="display: none;">
    	        <td nowrap align="left" colspan="4">
    	            <table border="1">
    	                <tr>
    	                    <td><asp:Label ID="lblRet_SI" runat="server">Retention SI</asp:Label></td>
    	                    <td><asp:Label ID="lblRet_Rate" runat="server">Ret.Rate</asp:Label></td>
    	                    <td><asp:Label ID="lblSurp1_SI" runat="server">1st Surplus SI</asp:Label></td>
    	                    <td><asp:Label ID="lblSurp1_Rate" runat="server">1st S/Rate</asp:Label></td>
    	                    <td><asp:Label ID="lblSurp2_SI" runat="server">2nd Surplus SI</asp:Label></td>
    	                    <td><asp:Label ID="lblSurp2_Rate" runat="server">2nd S/Rate</asp:Label></td>
    	                </tr>
    	                <tr>
    	                    <td><asp:textbox id="txtRet_SI" Enabled="false" MaxLength="13" Width="100px" runat="server" EnableViewState="true" ToolTip="Retention Sum Insured"></asp:textbox></td>
    	                    <td><asp:textbox id="txtRet_Rate" Enabled="false" MaxLength="5" Width="50px" runat="server" EnableViewState="true" ToolTip="Retention Rate"></asp:textbox></td>
    	                    <td><asp:textbox id="txtSurp1_SI" Enabled="false" MaxLength="13" Width="100px" runat="server" EnableViewState="true" ToolTip="First Surplus Sum Insured"></asp:textbox></td>
    	                    <td><asp:textbox id="txtSurp1_Rate" Enabled="false" MaxLength="5" Width="80px" runat="server" EnableViewState="true" ToolTip="First Surplus Rate"></asp:textbox></td>
    	                    <td><asp:textbox id="txtSurp2_SI" Enabled="false" MaxLength="13" Width="100px" runat="server" EnableViewState="true" ToolTip="Second Surplus Sum Insured"></asp:textbox></td>
    	                    <td><asp:textbox id="txtSurp2_Rate" Enabled="false" MaxLength="5" Width="80px" runat="server" EnableViewState="true" ToolTip="Second Surplus Rate"></asp:textbox></td>
    	                </tr>

    	                <tr>
    	                    <td><asp:Label ID="lblQuota_SI" runat="server">Quota SI</asp:Label></td>
    	                    <td><asp:Label ID="lblQuota_Rate" runat="server">Quota Rate</asp:Label></td>
    	                    <td><asp:Label ID="lblFacBal_SI" runat="server">FAC. SI</asp:Label></td>
    	                    <td><asp:Label ID="lblFacBal_Rate" runat="server">FAC.Rate</asp:Label></td>
    	                    <td><asp:Label ID="lblTreatyRef_Num" runat="server">Treaty Flag</asp:Label></td>
    	                    <td colspan="1"><asp:Label ID="lblTreatyRef_Descr" runat="server">Treaty Description</asp:Label></td>
    	                </tr>

    	                <tr>
    	                    <td><asp:textbox id="txtQuota_SI" Enabled="false" MaxLength="13" Width="100px" runat="server" EnableViewState="true" ToolTip="Quota Share Sum Insured"></asp:textbox></td>
    	                    <td><asp:textbox id="txtQuota_Rate" Enabled="false" MaxLength="5" Width="50px" runat="server" EnableViewState="true" ToolTip="Quota Share Rate"></asp:textbox></td>
    	                    <td><asp:textbox id="txtFacBal_SI" Enabled="false" MaxLength="13" Width="100px" runat="server" EnableViewState="true" ToolTip="Balance for Facultative(Sum Insured)"></asp:textbox></td>
    	                    <td><asp:textbox id="txtFacBal_Rate" Enabled="false" MaxLength="5" Width="50px" runat="server" EnableViewState="true" ToolTip="Balance for Facultative(Rate)"></asp:textbox></td>
    	                    <td><asp:textbox id="txtTreatyRef_Num" Enabled="false" MaxLength="4" Width="100px" runat="server" EnableViewState="true"></asp:textbox></td>
    	                    <td colspan="1"><asp:textbox id="txtTreatyRef_Descr" Enabled="false" MaxLength="45" Width="200px" runat="server" EnableViewState="true" ToolTip="Treaty Description"></asp:textbox></td>
    	                </tr>
    	                
    	                <tr style="display: none;">
    	                    <td><asp:TextBox  ID="txtLC_SI" runat="server" Width="100px"></asp:TextBox></td>
    	                    <td><asp:TextBox ID="txtLC_Rate" runat="server" Width="50px"></asp:TextBox></td>
    	                </tr>
    	                
    	            </table>
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
