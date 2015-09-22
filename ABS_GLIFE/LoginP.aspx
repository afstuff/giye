<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LoginP.aspx.vb" Inherits="LoginP" %>

<%@ Register src="~/UC_BANP.ascx" tagname="UC_BANP" tagprefix="uc1" %>

<%@ Register src="~/UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
    </script>
    
    <script language="javascript" type="text/javascript">
      var qsfCurrentDemo;
      var qsfDemoWebServicePath;

      function myTest() {
          //alert("Welcome..." + "\n" + "<%=cmdClose.ClientID%>");
          //document.getElementById('<%=cmdClose.ClientID%>').click();
          //document.getElementById('cmdClose').click();
          //document.getElementById('<%=cmdClose.ClientID%>').fireEvent("onclick");
          
      }

</script>
</head>

<body>

    <form id="Form1" runat="server">
    l<!-- start banner --><div id="div_banner" align="center">
                
        <uc1:UC_BANP ID="UC_BANP1" runat="server" />
                
    </div>
    

    <div =id="div_content" align="center">
        <table class="tbl_cont" align="center">
        <tr>
            <td align="left" valign="top" class="td_login_left" style="display:block;">
                <div align="center" style=" border: 1px solid #c0c0c0; display: block;" >
                    <img alt="" src="Images/GLife.jpg" style="width: 300px; height: 180px;" />
                </div>
                <br />
                <div align="center" style=" border: 1px solid #c0c0c0; display: none;" >
                    <img alt="" src="Images/Discussion.jpg" style="width: 300px; height: 180px;" />
                </div>
                <br />
                <div align="center"  style=" border: 1px solid #c0c0c0; display: none;" >
                    <img alt="" src="Images/GLife.jpg" style="width: 300px; height: 180px;" />
                </div>
            </td>
            
            <td align="left" valign="top" class="td_login_right">
	            <table align="center" border="0" cellspacing="0" class="TBL_LOGIN">
	                <tr>
	                    <td align="left" colspan="4" valign="top" class="TBL_LOGIN_TITLE">&nbsp;Login Information</td>
	                </tr>
                    <tr>
                        <td colspan="4" valign="top"><hr /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3" valign="top">&nbsp;Login with your valid User ID and Password</td>
                        <td align="right" colspan="1" valign="top">Date:&nbsp;<%= dteMydate %>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" valign="top"><hr /></td>
                    </tr>
                    <tr style="display: none;">
                        <td colspan="4" valign="top">&nbsp;</td>
                    </tr>
                    <tr>
    	                <td colspan="2" rowspan="5" align="right" valign="top">
    	                    <asp:Image ID="Image1" ImageAlign="Right" ImageUrl="~/Images/LoginKey.JPG" runat="server" 
                                  Height="100px" Width="160px" />
    	                </td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblUser_ID" Text="User ID:" runat="server"></asp:Label>&nbsp;</td>
                        <td align="left"><asp:TextBox ID="txtUserID" Enabled="true" Font-Bold="true" AutoPostBack="true" AutoCompleteType="Disabled" runat="server" 
                                Width="140px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblUser_PWD" Text="Password:" runat="server"></asp:Label>&nbsp;</td>
                        <td align="left"><asp:TextBox ID="txtUser_PWD" Enabled="true" AutoCompleteType="Disabled" TextMode="Password" 
                                runat="server" Width="140px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblUser_Name" Text="User Name:" runat="server"></asp:Label>&nbsp;</td>
                        <td align="left"><asp:TextBox ID="txtUserName" Enabled="false" Font-Bold="true" Font-Size="Large" AutoCompleteType="Disabled"
                                runat="server" Width="280px"></asp:TextBox>&nbsp;</td>
                    </tr>

                    <tr>
                        <td colspan="2">&nbsp;&nbsp;</td>
                    </tr>

                    <tr style="display: none;">
                        <td colspan="4" valign="top"><hr /></td>
                    </tr>

                    <tr>
    	   	            <td align="center" colspan="4">
                	   	    <asp:button id="LoginBtn" Font-Bold="false" Font-Size="Large" Width="100px" runat="server" text="Login..." style="height: 33px"></asp:button>
    	   	                    &nbsp;&nbsp;&nbsp;<input type="button" id="cmdClose" name="cmdClose" value="Close..." style="font-weight:normal; font-size:large; width:100px;" runat="server"  onclick="javascript:window.close();" />
    	   	            </td>
                    </tr>
                    <!--
                    <tr>
                        <td align="center" colspan="4" valign="top">
                            <a class="HREF_MENU2" href="M_MENU.aspx?menu=HOME">Start Application</a>
                            &nbsp;&nbsp;&nbsp;
                            <a class="HREF_MENU2" href="#" onclick="javascript:window.close();">End Application</a>
                        </td>
                    </tr>
                    -->
                    <tr>
                        <td colspan="4"><hr /></td>
                    </tr>

                    <tr>
                        <td colspan="4">&nbsp;
                            <asp:Label id="lblMessage" runat="server" Font-Size="Medium" ForeColor="Red" Font-Bold="false"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="display: none;" colspan="4">&nbsp;<asp:Button ID="cmdHelp" Text="Help..." runat="server" />
                            &nbsp;<asp:Label ID="lblJavaScript" runat="server"></asp:Label>&nbsp;<input type="text" id="Message" /> 
                            &nbsp;<input type="button" value="ClickMe" onclick="DoClick()" />

                        </td>
                    </tr>
                    
                    <tr style="display: none;">
                        <td colspan="4" valign="top">
                            <div align="center">
                                <img alt="" src="Images/Edu_Endow.jpg" style="width: 600px; height: 180px;" />
                            </div>
                        </td>
                    </tr>

                    <tr style="display: none;">
                        <td colspan="4" valign="top">
                            <div align="center">
                                <img alt="" src="Images/GLife.jpg" style="width: 600px; height: 180px;" />
                            </div>
                        </td>
                    </tr>

            	   	<tr>
    	           	    <td nowrap colspan="4" class="Login_Footer"><%=strCopyRight%></td>
    	   	        </tr>
                </table>
            </td>
        </tr>
        </table>
    </div>


<div  align="center" style="display: none;">
                    <asp:Label ID="Label2" runat="server" Text="File(s): "></asp:Label>
                    <asp:FileUpload ID="RadUpload1" runat="server"  MaxFileInputsCount="2"  OverwriteExistingFiles="false"
                        ControlObjectsVisibility="RemoveButtons" />
                    <asp:Button ID="Button1" runat="server" Text="Save"></asp:Button>
</div>
<div align="center" style="display: none;">
                <div id="UploadedFileLog" runat="server">
                    No uploaded files yet.
                </div>
</div>
                                                                      
<div id="div_footer" align="center">    

    <table id="tbl_footer" align="center">
        <tr>
            <td valign="top">
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
