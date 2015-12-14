<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_GRP_POLY_MEMBERS_ADD.aspx.vb" Inherits="Policy_PRG_LI_GRP_POLY_MEMBERS_ADD" %>

<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Group Life Module</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
       <script language="JavaScript" src="../Cal/calendar_eu.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js"></script>
    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js"></script>
    <script language="javascript" type="text/javascript" src="../Script/ScriptSB.js"></script>

    <script language="javascript" type="text/javascript">
        function Func_File_Change() {
            var c = 0;
            var cx = 0
            var strfile = "";

            strfile = document.getElementById("My_File_Upload").value;
            // strfile = document.getElementById("My_File_Upload").PostedFile.FileName;
            for (c = 0; c < strfile.length; c++) {
                if (strfile.substring(c, 1) == "") {
                }
                else {
                    cx = cx + 1;
                }
            }

            if (cx <= 0) {
                document.getElementById("txtFile_Upload").style.display = "none";
                document.getElementById("txtFile_Upload").style.visibility = "hidden";
                document.getElementById("cmdFile_Upload").disabled = true;
                alert("Missing or Invalid document name...");
                return false;
            }
            else {
                document.getElementById("txtFile_Upload").style.display = "";
                document.getElementById("txtFile_Upload").style.visibility = "visible";
                document.getElementById("txtFile_Upload").value = strfile;
                // document.getElementById("txtFile_Upload").innerHTML = strfile;
                document.getElementById("cmdFile_Upload").disabled = false;
                // 
                return true;
            }
        }

    </script>
    
</head>

<body onload="<%= FirstMsg %>">

    <form id="Form1" name="Form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
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
                                        <td align="center" colspan="4" valign="top" style="height: 26px">
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_GP_PROP_POLICY.aspx?menu=GL_QUOTE')">Go to Menu</a>
                                            &nbsp;&nbsp;<asp:Button ID="cmdPrev" CssClass="cmd_butt" Enabled="false" Text="«..Previous" runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdNew_ASP" CssClass="cmd_butt" Visible="false" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
	                                        &nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" Visible="false" Enabled="false" runat="server" text="Save Data"></asp:button>
		                		        	&nbsp;&nbsp;<asp:Button ID="cmdDelItem_ASP" CssClass="cmd_butt" Enabled="false" Text="Delete Item" OnClientClick="JSDelItem_ASP()" runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" CssClass="cmd_butt" Enabled="true" Visible="true" runat="server" text="Print"></asp:button>
                                            &nbsp;&nbsp;<asp:Button ID="cmdNext" CssClass="cmd_butt" Enabled="false" Text="Next..»" runat="server" />
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
                    <td nowrap class="myheader">Members Information</td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="td_menu">
                        <table align="center" border="0" class="tbl_menu_new">
                            <tr>
                                <td align="left" colspan="4" valign="top">
                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                                <td nowrap align="right" valign="top"><asp:Label ID="lblFileNum" Enabled="true" Text="File No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtFileNum" Enabled="false" Width="230px" runat="server"></asp:TextBox></td>
                                                <td align="right" valign="top"><asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="true" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtPolNum" Enabled="true" Width="250px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Button ID="cmdGetPol" Enabled="false" Text="Go" runat="server" />
                                                </td>
                            </tr>
                        
                            <tr>
                                                <td align="right" valign="top" colspan="1">
                                                    <asp:Label ID="lblBatch_Num" Visible="true" Enabled="true" Text="Batch Number:" ToolTip="Enter unique batch number..." runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtBatch_Num" MaxLength="10" Width="80px" runat="server"></asp:TextBox>
                                                    &nbsp;&nbsp;<asp:DropDownList ID="cboBatch_Num" AutoPostBack="true" Width="100px" runat="server"></asp:DropDownList>
                                                    &nbsp;&nbsp;<asp:Button ID="cmdGetBatch" Enabled="true" Text="Get Data" runat="server" />
                                                    &nbsp;<asp:TextBox ID="txtBatch_Name" Visible="false" MaxLength="10" Width="20px" runat="server"></asp:TextBox></td>
                                                <td align="right" valign="top"><asp:Label ID="lblQuote_Num" Enabled="true" Text="Proposal No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtQuote_Num" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
                            </tr>

                                    <tr style="display: none;">
                                                <td nowrap align="right" valign="top"><asp:Label ID="lblPrem_SA_Factor" Enabled="false" Text="Sum Assured Factor:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtPrem_SA_Factor" Enabled="false" MaxLength="3" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;&nbsp;<asp:Label ID="lblRecNo" BorderStyle="Solid" Text="Rec. No:" Enabled="false" runat="server"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="txtRecNo" Enabled="false" Width="60px" runat="server" MaxLength="18"></asp:TextBox></td>
                                                <td nowrap align="right" valign="top"><asp:Label ID="lblProduct" Enabled="false" Text="Product Category/Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtProductClass" Visible="true" Enabled="false" MaxLength="10" Width="80" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtProduct_Num" Visible="true" Enabled="false" MaxLength="10" Width="80px" runat="server"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Members Details</td>
                                    </tr>
                                    
                                    <tr id="SB_CONT" runat="server" style=" display:none;">
                                        <td align="center" colspan="4" valign="top" style="border-style:ridge;">
                                            <div id="SB_DIV" runat="server" align="center" style=" background-color: White; color: Black; font-size: 23px; font-weight:normal;">
                                                &nbsp;<label id="SB_MSG" runat="server"></label>&nbsp;
                                            </div>
                                        </td>
                                    </tr>

                
                            <tr>
                                <td align="left" colspan="4">
                                    <table align="center" border="0" style="width: 100%">
                                        <tr style="background-color: #ADD8E6;">
                                                <td nowrap align="right" valign="top">
                                                    <asp:CheckBox ID="chkData_Source" Text="-" runat="server" />
                                                    &nbsp;<asp:Label ID="lblData_Source" Text="Data Source:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="5">
                                                    <asp:DropDownList ID="cboData_Source" Width="250px" AutoPostBack="true" runat="server" OnTextChanged="DoProc_Data_Source_Change">
                                                        <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                        <asp:ListItem Value="M">Manually Input Data</asp:ListItem>
                                                        <asp:ListItem Value="U">Upload Data From Excel Document</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtData_Source_SW" Width="40" Visible="false" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtData_Source_Name" Width="40" Visible="false" Enabled="false" runat="server"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblRisk_Days" ForeColor="Red" Visible="true" runat="server" Text="Risk Days:"></asp:Label>
                                                    &nbsp;<asp:TextBox ID="txtRisk_Days" Width="60" Visible="true" runat="server"></asp:TextBox>
                                                     <asp:Label ID="Label1" Text="Effective Date:" runat="server"></asp:Label><asp:TextBox ID="txtAdditionDate" runat="server"></asp:TextBox><script language="JavaScript" type="text/javascript">
                                                     new tcal({ 'formname': 'frmLoadMembers', 'controlname': 'txtDeleteDate' });</script> 

                                                </td>
                                        </tr>
                                        <tr id="tr_file_upload" runat="server" style="background-color: #ADD8E6;">
                                                <td nowrap align="right" valign="top"><asp:Label ID="lbl_File_Upload" Text="Select Document:" runat="server"></asp:Label></td>
                                                <td nowrap align="left" valign="top" colspan="5">
                                                    <input type="file" id="My_File_Upload" name="My_File_Upload" runat="server" onchange="Func_File_Change()" onclick="return My_File_Upload_onclick()" />
                                                    &nbsp;<asp:TextBox ID="txtFile_Upload" Enabled="false" Visible="true" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="cmdFile_Upload" Enabled="false" Font-Bold="true" Font-Size="Large" Text="Upload" runat="server" />
                                                    &nbsp;<asp:Label ID="lblFile_Upload_Warning" Visible="false" ForeColor="Red" runat="server" Text="Excel File of .XLS or .XLSX"></asp:Label></td>                                        
                                        </tr>

                                        <tr style="background-color: Maroon; color: White;">
                                            <td align="right" valign="top"><asp:Label ID="lblXLS_Data_Start_No" Text="Start Excel No" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:TextBox ID="txtXLS_Data_Start_No" Width="60px" runat="server"></asp:TextBox> </td>
                                            <td align="left" valign="top" colspan="2"><asp:Label ID="lblXLS_Data_End_No" Text="End Excel No" runat="server"></asp:Label></td>
                                            <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtXLS_Data_End_No" Width="60px" runat="server"></asp:TextBox> </td>
                                            <td align="left" valign="top" colspan="1"><asp:Label ID="lblXLS_Data_Remarks" Font-Bold="true" Text="Applies to Upload option" runat="server"></asp:Label></td>                                            
                                        </tr>
                                        
                                        <tr class="tr_frame" id="HideRow1" runat="server">
                                            <td align="left" valign="top"><asp:Label ID="lblMember_SN" Text="Serial No" 
                                                    runat="server" Visible="False"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblGender" Text="Category" 
                                                    runat="server" Visible="False"></asp:Label></td>
                                            <td align="left" valign="top" colspan="2"><asp:Label ID="lblMember_Name" 
                                                    Text="Member Name" runat="server" Visible="False"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblDesignation_Name" 
                                                    Text="Designation" runat="server" Visible="False"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblMember_DOB" 
                                                    Text="Date of Birth / Age" ToolTip="Date of Birth(dd/mm/yyyy)" runat="server" 
                                                    Visible="False"></asp:Label></td>
                                        </tr>
                                        <tr style="font-size: small;">
                                            <td align="left" valign="top"><asp:TextBox ID="txtMember_SN" Enabled="false" 
                                                    MaxLength="10" Width="100px" runat="server" Visible="False"></asp:TextBox></td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="cboGender" Width="100px" runat="server" Visible="False">
                                                </asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtGender" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtGenderName" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top" colspan="2"><asp:TextBox ID="txtMember_Name" 
                                                    runat="server" Width="200px" Visible="False"></asp:TextBox></td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txtDesignation_Name" Width="150px" runat="server" 
                                                    Visible="False"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top"><asp:TextBox ID="txtMember_DOB" MaxLength="10" 
                                                    Width="100px" ToolTip="Date of Birth(dd/mm/yyyy)" runat="server" 
                                                    AutoPostBack="True" Visible="False"></asp:TextBox>
                                                &nbsp;<asp:TextBox ID="txtDOB_ANB" Enabled="False" Width="40px" runat="server" 
                                                    Visible="False"></asp:TextBox></td>
                                        </tr>
                                        
                                        <tr class="tr_frame" id="HideRow2" runat="server">
                                            <td align="left" valign="top"><asp:Label ID="lblStart_Date" Text="Start Date" 
                                                    ToolTip="Start Date (dd/mm/yyyy)" runat="server" Visible="False"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblEnd_Date" Text="End Date" 
                                                    ToolTip="End Date (dd/mm/yyyy)" runat="server" Visible="False"></asp:Label></td>
                                            <td align="left" valign="top">
                                                <asp:Label ID="lblPrem_Period_Yr" Text="Tenor" runat="server" Visible="False"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td align="left" valign="top">
                                                <asp:Label ID="lblTotal_Emolument" 
                                                    Text="Total Emolument" runat="server" Visible="False"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblMedical_YN" Text="Req Medical" 
                                                    ToolTip="Any medical examination" runat="server" Visible="False"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblPrem_Rate_X" 
                                                    Text="Select Premium Rate" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="font-size: small;">
                                            <td align="left" valign="top"><asp:TextBox ID="txtStart_Date" MaxLength="10" 
                                                    Width="100px" ToolTip="Start Date (dd/mm/yyyy)" runat="server" Visible="False"></asp:TextBox></td>
                                            <td align="left" valign="top"><asp:TextBox ID="txtEnd_Date" MaxLength="10" 
                                                    Width="100px" ToolTip="End Date (dd/mm/yyyy)" runat="server" Visible="False"></asp:TextBox></td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txtPrem_Period_Yr" MaxLength="3" ToolTip="" runat="server" 
                                                    Width="40px" Visible="False"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txtTotal_Emolument" MaxLength="15" ToolTip="" runat="server" 
                                                    Visible="False" AutoPostBack="true" OnTextChanged="DoProc_Premium_Code_Change" ></asp:TextBox></td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="cboMedical_YN" Width="100px" runat="server" 
                                                    Visible="False">
                                                        <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        <asp:ListItem Value="N">No</asp:ListItem>                                                
                                                </asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtMedical_YN" Visible="false" Width="20px" ToolTip="" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtMedical_YN_Name" Visible="false" Width="20px" ToolTip="" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="cboPrem_Rate_Code" Enabled="false" Width="200px" 
                                                    runat="server" AutoPostBack="true" OnTextChanged="DoProc_Premium_Code_Change" 
                                                    Visible="False">
                                                </asp:DropDownList>                                                
                                            </td>
                                        </tr>

                                <tr class="tr_frame"  id="HideRow3" runat="server">
                                    <td align="left" colspan="1">
                                        <asp:Label ID="lblPrem_Rate_Code" Enabled="False" Text="Premium Rate Code:" 
                                            runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td align="left" colspan="1">
                                        <asp:Label ID="lblPrem_Rate" Enabled="False" Text="Premium Rate:" 
                                            runat="server" Visible="False"></asp:Label>
                                        </td>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lblPrem_Rate_Per" Enabled="False" Text="Rate Per:" 
                                            runat="server" Visible="False"></asp:Label>
                                        </td>
                                    <td align="left" colspan="1">
                                        <asp:Label ID="lblPrem_Amt" Enabled="False" Text="Prem Amount:" runat="server" 
                                            Visible="False"></asp:Label>
                                        </td>
                                    <td align="left" colspan="1">
                                        <asp:Label ID="lblErr_List" Visible="false" Enabled="true" ForeColor="Red" Text="Error:" runat="server"></asp:Label>
                                        </td>
                                </tr>

                                <tr style="font-size: small;">
                                    <td align="left" colspan="1">
                                        <asp:TextBox ID="txtPrem_Rate_Code" Visible="False" Enabled="false" 
                                            Width="80px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtPrem_Rate_CodeName" Visible="false" Enabled="false" Width="30px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtPrem_Rate_TypeNum" Visible="False" Enabled="false" 
                                            MaxLength="1" Width="20px" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left" colspan="1">
                                        &nbsp;&nbsp;<asp:TextBox ID="txtPrem_Rate" Enabled="false" Width="90px" 
                                            ToolTip="" runat="server" Visible="False"></asp:TextBox></td>
                                    <td align="left" colspan="2">
                                        <asp:TextBox ID="txtPrem_Rate_Per" Visible="False" Enabled="false" 
                                            MaxLength="5" Width="60px" ToolTip="" runat="server"></asp:TextBox></td>
                                    <td align="left" colspan="1">
                                        <asp:TextBox ID="txtPrem_Amt" Visible="False" Enabled="false" MaxLength="15" 
                                            runat="server"></asp:TextBox></td>
                                    <td align="left" colspan="1">
                                        <asp:DropDownList ID="cboErr_List" Visible="false" Width="200px" runat="server"></asp:DropDownList>
                                        </td>
                                </tr>

                                    </table>
                                </td>
                            </tr>
                            
                                <tr class="tr_frame">
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lblSum_Assured" Enabled="false" Text="Sum Assured" ToolTip="" runat="server"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    <td align="left" colspan="2">
                                        &nbsp;</td>
                                </tr>

                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:TextBox ID="txtSum_Assured" Enabled="false" MaxLength="15" ToolTip="" runat="server"></asp:TextBox><asp:TextBox ID="txtPrem_Amt_Prorata" Visible="false" Enabled="false" MaxLength="15" Width="80px" runat="server"></asp:TextBox><asp:TextBox ID="txtLoad_amt" Visible="false" Enabled="false" MaxLength="15" Width="80px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtGenStart_DateHidden" runat="server" Visible="False"></asp:TextBox>
                                    </td>
                                    <td align="left" colspan="2">
                                        &nbsp;</td>
                                </tr>

                                        <tr>
                                            <td colspan="4"><hr /></td>
                                        </tr>

                    
                            <tr>
                                <td align="center" colspan="4" valign="top">
                                    <table align="center" style="background-color: White; width: 97%;">
                                    
                                        <tr>
                                            <td align="left" colspan="4" valign="top">
                                            <div align="left" style="background-color: White; color: White; border-bottom-style: ridge; height: 35px;">
                                                <table align="left" border="0" style="background-color: #1C5E55; width: 100%; height: 30px;" >
                                                    <tr style="font-size: medium; font-weight: bold;">
                                                        <td align="left" style="width: 30px;">&nbsp;</td>
                                                        <td align="left" style="width: 60px;">&nbsp;</td>
                                                        <td align="center" style="width: 70px;">Ref.No</td>
                                                        <td align="center" style="width: 120px;">Member Name</td>
                                                        <td align="center" style="width: 80px;">Sum Assured</td>
                                                        <td align="center" style="width: 100px;">DOB</td>
                                                        <td align="center" style="width: 40px;">Age</td>
                                                        <td align="center" style="width: 60px;">Rate</td>
                                                        <td align="center" style="width: 80px;">Prem Amt</td>
                                                        <td align="center" style="width: 80px;">Batch</td>
                                                    </tr>
                                                </table>
                                            </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left" colspan="4" valign="top">
                                            <div class="div_grid">
                                                <asp:GridView id="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl"
                                                    DataKeyNames="TBIL_POL_MEMB_REC_ID" HorizontalAlign="Left"
                                                    AutoGenerateColumns="False" AllowPaging="false" AllowSorting="true"
                                                    PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                                    PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next"
                                                    PagerSettings-PreviousPageText="Previous" PagerSettings-LastPageText="Last"
                                                    EmptyDataText="No data available..." 
                                                    GridLines="Both"  ShowHeader="false" ShowFooter="True">  

                        
                                                    <PagerStyle CssClass="grd_page_style" />
                                                    <HeaderStyle CssClass="grd_header_style" />
                                                    <RowStyle CssClass="grd_row_style" />
                                                    <SelectedRowStyle CssClass="grd_selrow_style" />
                                                    <EditRowStyle CssClass="grd_editrow_style" />
                                                    <AlternatingRowStyle CssClass="grd_altrow_style" />
                                                    <FooterStyle CssClass="grd_footer_style" />
                    
                                                    <PagerSettings  FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom" 
                                                        PreviousPageText="Previous">
                                                    </PagerSettings>
                        
                                                    <Columns>
                                                        <asp:TemplateField>
        			                                        <ItemTemplate>
        						                                <asp:CheckBox id="chkSel" runat="server" Width="20px" Checked="true"></asp:CheckBox>
                                                            </ItemTemplate>                                                            
                                                        </asp:TemplateField>
                                
                                                        <asp:CommandField ShowSelectButton="True" ItemStyle-Width="50px" />
                            
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_MEMB_REC_ID" HeaderText="Ref.No" ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_MEMB_NAME" HeaderText="Member Name" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_MEMB_TOT_SA" HeaderText="Sum Assured" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_MEMB_BDATE" HeaderText="DOB" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true"  DataFormatString="{0:dd MMM yyyy}" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_MEMB_AGE" HeaderText="Age" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_MEMB_RATE" HeaderText="Prem Rate" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                         <asp:BoundField readonly="true" DataField="TBIL_POL_MEMB_PREM" HeaderText="Prem Rate" ItemStyle-Width="60px" 
                                                            HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" Visible="false" DataFormatString="{0:N2}"/>
                                                        
                                                        <asp:TemplateField  HeaderText="Prem. Amt" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" >
                                                          <ItemTemplate >
                                                           <asp:Label ID="lblTransAmt" runat="server" DataFormatString="{0:N2}" Text='<%#Eval("TBIL_POL_MEMB_PREM") %>' />
                                                          </ItemTemplate>
                                                           <FooterTemplate>

                                                            <asp:Label ID="lbltxtTotal" runat="server" Text="0.00" DataFormatString="{0:N2}"  />

                                                            </FooterTemplate>

                                                        </asp:TemplateField>
                                                      <asp:BoundField readonly="true" DataField="TBIL_POL_MEMB_BATCH_NO" HeaderText="Batch" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                    </Columns>
   
                                                </asp:GridView>
                                            </div>    
                                            </td>                                            
                                        </tr>
                                                                                
                                    </table>
                                </td>
                            </tr> 
                            
                            <tr>
                                <td align="left" colspan="4" valign="top">
                                    <asp:Label ID="lblResult" Text="Result:" runat="server"></asp:Label>
                                    <asp:CheckBox runat="server" ID="chkDRNote"  Text="Create Debit Note"/><asp:Button ID="butDeleteMembers_ASP" CssClass="cmd_butt" Enabled="TRUE" Font-Bold="true" Text="Add Members" OnClientClick="JSAddItem_ASP()" runat="server" Width="147px"/>
                                    
                                </td>
                            </tr>

                                
                        </table>
                    </td>
                </tr>
            </table>                                                                                
    </div>

       
    <!-- footer content -->                                                                             
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
