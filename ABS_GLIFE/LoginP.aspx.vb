Imports System.Data
Imports System.Data.OleDb
Imports System.Web.Security
Imports System.Web
Imports System.Globalization


Partial Class LoginP
    Inherits System.Web.UI.Page

    'The Navigator-specific stop() method offers a scripted equivalent of clicking
    'the Stop button in the toolbar. Availability of this method allows you to create your
    'own toolbar on your page and hide the toolbar (in the main window with signed
    'scripts or in a subwindow). For example, if you have an image representing the Stop
    'button in your page, you can surround it with a link whose action stops loading, as
    'in the following:
    '   <A HREF=”javascript: void stop()”><IMG SRC=”myStop.gif” BORDER=0></A>
    'A script cannot stop its own document from loading, but it can stop loading of
    'another frame or window. Similarly, if the current document dynamically loads a
    'new image or a multimedia MIME type file as a separate action, the stop() method
    'can halt that process. Even though the stop() method is a window method, it is
    'not tied to any specific window or frame: Stop means stop.

    Dim sUsername As String = ""
    Dim sPassword As String = ""
    Dim strSQL As String
    Dim lrcValidate As String
    Dim strPWD As String

    Protected strCopyRight As String
    Protected dteMydate As String = CType(Format(Now, "dd-MMM-yyyy"), String)

    Protected Structure TabItem
        Dim TabText As String
        Dim TabKey As String
    End Structure

    Protected MenuItems As New ArrayList()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' 	<link href="<%= request.ApplicationPath %>/Styles.css" type=text/css rel=stylesheet />
        ' 	<!-- #include virtual="/abslife/Scripts/Glo.vbs" -->

        'Response.Write("<br/>Virtual URL: " & HttpRuntime.AppDomainAppVirtualPath)
        'Response.Write("<br/>Path: " & Server.MapPath(HttpRuntime.AppDomainAppVirtualPath))

        'Response.Write("Path: " & Server.MapPath("~/Download"))
        'Response.Write("Current Directory: " & My.Computer.FileSystem.CurrentDirectory)

        'Response.Write("<br />Path: " & HttpRuntime.AppDomainAppPath)
        'Response.Write("<br />Virtual Path: " & HttpRuntime.AppDomainAppVirtualPath)
        ''Response.Write("<br />Path: " & Server.MapPath("LoginP.aspx"))
        'Response.Write("<br />Blank Path: " & Server.MapPath(""))
        'Response.Write("<br />Path of Folder 'Download': " & Server.MapPath("Download"))

        'This example returns the current directory and displays it in a message box.
        '   Visual Basic  Copy Code 
        'MsgBox(My.Computer.FileSystem.CurrentDirectory)

        'This example sets the current directory to C:\TestDirectory.
        'Visual Basic  Copy Code 
        'My.Computer.FileSystem.CurrentDirectory = "C:\TestDirectory"


        '***************************************************************************************************

        '' Define the name and type of the client scripts on the page.
        'Dim csname1 As String = "PopupScript"
        'Dim csname2 As String = "ButtonClickScript"
        Dim cstype As Type = Me.GetType()

        If Not (Page.IsPostBack) Then
            'obsolete
            'Page.RegisterStartupScript("starScript", "callKeywords('" + Name + "','Keyword',+'Region');")

            ''new
            'Response.Write("<script language='javascript' type='text/javascript'>alert('Welcome World!');</script>")
            'Response.Write("<script language='javascript' type='text/javascript'>myFunc_Name();</script>")

            'OK
            'Page.ClientScript.RegisterStartupScript(cstype, "starScript", "myShowDialogue('ade','dele');", True)

            'OK
            'ScriptManager.RegisterStartupScript(Me, cstype, "starScript", "myShowDialogue('ADE','DELE');", True)
            'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "starScript", "myShowDialogue('ADE','D E L E');", True)
            'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "starScript", "alert('Hello This is first function from code behind ');", True)


            'ok
            '' Get a ClientScriptManager reference from the Page class.
            'Dim cs As ClientScriptManager = Page.ClientScript

            ''ok
            '' Check to see if the startup script is already registered.
            'If (Not cs.IsStartupScriptRegistered(cstype, csname1)) Then

            '    Dim cstext1 As String = ""
            '    cstext1 = "alert('Hello World');"
            '    'cstext1 = "<script type='text/javascript'>myShowDialogue('ade','dele');</script>"
            '    cs.RegisterStartupScript(cstype, csname1, cstext1, True)

            'End If


            '' Check to see if the client script is already registered.
            'If (Not cs.IsClientScriptBlockRegistered(cstype, csname2)) Then
            '    'If (Not cs.IsStartupScriptRegistered(cstype, csname2)) Then

            '    Dim cstext2 As New StringBuilder()
            '    'cstext2.Append("<script type='text/javascript'> function myShowDialogue('ade','dele')")
            '    cstext2.Append("<script type='text/javascript'> function DoClick()")
            '    cstext2.Append("{ ")
            '    cstext2.Append("document.Form1.lblMessage.value='Text from client script.';")
            '    cstext2.Append("} ")
            '    cstext2.Append("</script>")
            '    cs.RegisterClientScriptBlock(cstype, csname2, cstext2.ToString(), False)
            '    'cs.RegisterStartupScript(cstype, csname2, cstext2.ToString(), True)

            'End If

            'ok
            'Dim strParam1 As String = "Oduwole"
            'Dim strParam2 As String = "Olasunkanmi"
            'lblJavaScript.Text = "<script type='text/javascript'>myShowDialogue('" & strParam1 & "','" & strParam2 & "'" & ");</script>"

        End If


        '***************************************************************************************************


        'mystrAPP_PATH = HttpRuntime.AppDomainAppPath
        'mystrAPP_PATH = HttpRuntime.AppDomainAppVirtualPath

        'Response.Write("<br />Path: " & HttpRuntime.AppDomainAppPath)
        'Response.Write("<br />Path: " & HttpRuntime.AppDomainAppVirtualPath)
        'Response.Write("<br />Path: " & Server.MapPath("LoginP.aspx"))

        ''CType(Me.GridView1.Rows(0).FindControl("chkSel"), CheckBox).Attributes.Add("onclick", "javascript:myproc('" & 123 & "')")
        'Me.cmdHelp.Attributes.Add("onclick", "javascript:myHelp('" & "./I_LIFE/PRG_LI_BRK_CAT.aspx" & "')")



        'Dim XX As String = HttpContext.Current.Request.Url.AbsolutePath.ToLowerInvariant()
        'Dim URL_That_LinkUp_To_Current_Page As System.Uri = HttpContext.Current.Request.UrlReferrer


        'MenuItems.Clear()
        'Dim myTab As New TabItem()
        'myTab.TabText = "Tab Caption"
        'myTab.TabKey = "Tab URL"
        'MenuItems.Add(myTab)

        'Me.DataList1.DataSource = MenuItems
        'Me.DataList1.DataBind()


        'strCopyRight = "Copyright &copy;" & Year(Now) & " " & STRCOMP_NAME
        strCopyRight = "Copyright &copy; " & Year(Now)

        If Not (Page.IsPostBack) Then
            'Call DoProc_LogOut()
            Me.txtUserID.Enabled = True
            Me.txtUserID.Focus()
        End If

    End Sub

    Protected Sub LoginBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoginBtn.Click
        lblMessage.Text = ""
        Dim mystrCONN_Chk As String = ""
        Dim LoginDate As Date
        Dim PassWordExpiryDate As Date
        Dim PasswordExpireDaysLeft As Integer
        Dim status As String = ""
        LoginDate = Convert.ToDateTime(DoConvertToDbDateFormat(Format(DateTime.Now, "dd/MM/yyyy")))

        Dim objOLEConn_Chk As OleDbConnection = Nothing
        Dim objOLECmd_Chk As OleDbCommand = Nothing
        Dim objOLEDR_Chk As OleDbDataReader

        Dim myTmp_Chk As String
        Dim myTmp_Ref As String
        myTmp_Chk = "N"
        myTmp_Ref = ""


        mystrCONN_Chk = CType(Session("connstr"), String)
        objOLEConn_Chk = New OleDbConnection()
        objOLEConn_Chk.ConnectionString = mystrCONN_Chk

        Try
            'open connection to database
            objOLEConn_Chk.Open()
        Catch ex As Exception
            lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn_Chk = Nothing
            Exit Sub
        End Try

        Try
            Dim User_Login = Trim(txtUserID.Text)
            Dim User_Password = Trim(EncryptNew(txtUser_PWD.Text))
            strSQL = "SELECT * FROM SEC_USER_LIFE_DETAIL WHERE SEC_USER_LOGIN='" & User_Login & "' " & _
                   "and SEC_USER_PASSWORD='" & User_Password & "' "
            objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
            objOLECmd_Chk.CommandType = CommandType.Text
            objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
            If (objOLEDR_Chk.Read()) Then
                Session("MyUserIDX") = Trim(Me.txtUserID.Text)
                'Session("MyUserName") = UCase(Me.txtUserName.Text)
                Session("MyUserName") = objOLEDR_Chk("SEC_USER_NAME")
                Session("MyUserRole") = objOLEDR_Chk("SEC_USER_ROLE")
                status = objOLEDR_Chk("SEC_USER_FLAG")
                PasswordExpireDaysLeft = DateDiff(DateInterval.Day, LoginDate, CType(objOLEDR_Chk("passwordexpirydate"), DateTime))
                PassWordExpiryDate = Convert.ToDateTime(DoConvertToDbDateFormat(Format(objOLEDR_Chk("passwordexpirydate"), "dd/MM/yyyy")))
                If Request.QueryString("Goto") <> "" Then
                    Response.Redirect(Request.QueryString("Goto"))
                ElseIf status = "X" Then
                    MsgBox("You have been deactivated, please contact administrator.", 0, "User activation status")
                    'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                ElseIf Trim(objOLEDR_Chk("SEC_USER_PASSWORD") & vbNullString) = Trim(objOLEDR_Chk("firstpassword") & vbNullString) Then
                    Response.Redirect("SEC/PRG_SEC_USER_CHG_PASS.aspx")
                ElseIf PassWordExpiryDate = LoginDate Then
                    'Update user details table
                    UpdatePasswordAtExpiry(txtUserID.Text, Trim(objOLEDR_Chk("SEC_USER_PASSWORD") & vbNullString))
                    MsgBox("Password expired, please change password.", 0, "Password Expiry Notification")
                    Response.Redirect("SEC/PRG_SEC_USER_CHG_PASS.aspx")
                ElseIf PasswordExpireDaysLeft < 3 Then
                    MsgBox("Password will expire in less than " & PasswordExpireDaysLeft & " day(s), please kindly change your password.", 0, "Password Expiry Notification")
                    Response.Redirect("MENU_GL.aspx?menu=HOME")
                Else
                    Response.Redirect("MENU_GL.aspx?menu=HOME")
                End If
            Else
                Me.lblMessage.Text = "Login information is not correct. Enter Valid User ID and Password..."
                Me.txtUserID.Enabled = True
                Me.txtUserID.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            Me.lblMessage.Text = "Error has occured. Reason: " & ex.Message.ToString()
        End Try
        objOLEDR_Chk = Nothing
        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing
        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing
        '' get required user parameters
        'lrcValidate = ""

        ''sUsername = txtUserID.Text
        'sPassword = txtUser_PWD.Text

        'Try
        '    ' try and authenticate the user
        '    lrcValidate = Me.ValidateUserLogin(sUsername, sPassword)
        '    ' test the valid return code of the user authentication
        '    If lrcValidate = "True" Then
        '        Session.Add("MyLogonTime", System.DateTime.Now)
        '        Session.Add("MyUserID", sUsername)
        '        Dim strUserName As String = "User Name: " & CType(Session("MyUserName"), String)
        '        lblMessage.Text = strUserName

        '        'Credentials are ok, redirect back to the page that forced
        '        'authentication, pass the user name and don't persist the cookie

        '        'System.Web.Security.FormsAuthentication.RedirectFromLoginPage(txtUserID.Text, False)


        '        'Response.Redirect("absMain.aspx")
        '        'Response.Redirect(Request.ApplicationPath & "/UNP_FRA.aspx")
        '        'Response.Redirect(Request.ApplicationPath & "/UNP_MENU.aspx")
        '        'Response.Redirect(Request.ApplicationPath & "/UNP_MNU.aspx")

        '        If Request.QueryString("Goto") <> "" Then
        '            Response.Redirect(Request.QueryString("Goto"))
        '        Else
        '            'Response.Redirect("M_MENU.aspx?menu=HOME")
        '            Response.Redirect("MENU_GL.aspx?menu=HOME")

        '        End If

        '    ElseIf lrcValidate = "Invalid_User" Then
        '        lblMessage.Text = "Invalid UserName and Password."
        '    ElseIf lrcValidate = "Invalid_Password" Then
        '        lblMessage.Text = "Password Incorrect, Please try again."
        '    Else
        '        lblMessage.Text = "Error(s) Occured!." & "<BR>" & lrcValidate & "<BR>" & "Unable to Authenticate User at this time."
        '    End If

        'Catch ex As Exception
        '    lblMessage.Text = "Unable to Authenticate User at this time."

        'End Try


        'If Me.txtUserID.Text = "CRU" And Me.txtUser_PWD.Text = "CRU*123" Then
        '    Session("MyUserIDX") = Trim(Me.txtUserID.Text)
        '    Session("MyUserName") = UCase(Me.txtUserName.Text)
        '    If Request.QueryString("Goto") <> "" Then
        '        Response.Redirect(Request.QueryString("Goto"))
        '    Else
        '        Response.Redirect("M_MENU.aspx?menu=HOME")
        '    End If
        'ElseIf Me.txtUserID.Text = "user1" And Me.txtUser_PWD.Text = "pwd*u1" Then
        '    Session("MyUserIDX") = Trim(Me.txtUserID.Text)
        '    Session("MyUserName") = UCase(Me.txtUserName.Text)
        '    If Request.QueryString("Goto") <> "" Then
        '        Response.Redirect(Request.QueryString("Goto"))
        '    Else
        '        Response.Redirect("M_MENU.aspx?menu=HOME")
        '    End If
        'ElseIf Me.txtUserID.Text = "user2" And Me.txtUser_PWD.Text = "pwd*u2" Then
        '    Session("MyUserIDX") = Trim(Me.txtUserID.Text)
        '    Session("MyUserName") = UCase(Me.txtUserName.Text)
        '    If Request.QueryString("Goto") <> "" Then
        '        Response.Redirect(Request.QueryString("Goto"))
        '    Else
        '        Response.Redirect("M_MENU.aspx?menu=HOME")
        '    End If
        'ElseIf Me.txtUserID.Text = "user3" And Me.txtUser_PWD.Text = "pwd*u3" Then
        '    Session("MyUserIDX") = Trim(Me.txtUserID.Text)
        '    Session("MyUserName") = UCase(Me.txtUserName.Text)
        '    If Request.QueryString("Goto") <> "" Then
        '        Response.Redirect(Request.QueryString("Goto"))
        '    Else
        '        Response.Redirect("M_MENU.aspx?menu=HOME")
        '    End If
        'Else
        '    Me.lblMessage.Text = "Login information is not correct. Enter Valid User ID and Password..."
        '    Me.txtUserID.Enabled = True
        '    Me.txtUserID.Focus()
        'End If

    End Sub

    Protected Sub txtUserID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.TextChanged

        lblMessage.Text = ""
        Dim mystrCONN_Chk As String = ""

        Dim objOLEConn_Chk As OleDbConnection = Nothing
        Dim objOLECmd_Chk As OleDbCommand = Nothing
        Dim objOLEDR_Chk As OleDbDataReader

        Dim myTmp_Chk As String
        Dim myTmp_Ref As String
        myTmp_Chk = "N"
        myTmp_Ref = ""


        mystrCONN_Chk = CType(Session("connstr"), String)
        objOLEConn_Chk = New OleDbConnection()
        objOLEConn_Chk.ConnectionString = mystrCONN_Chk

        Try
            'open connection to database
            objOLEConn_Chk.Open()
        Catch ex As Exception
            lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn_Chk = Nothing
            Exit Sub
        End Try

        Try
            Dim User_Login = Trim(txtUserID.Text)
            strSQL = "SELECT * FROM SEC_USER_LIFE_DETAIL WHERE SEC_USER_LOGIN='" & User_Login & "'"
            objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
            objOLECmd_Chk.CommandType = CommandType.Text
            objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
            If (objOLEDR_Chk.Read()) Then
                Session("MyUserIDX") = Trim(Me.txtUserID.Text)
                txtUserName.Text = objOLEDR_Chk("SEC_USER_NAME")
            Else
                Me.lblMessage.Text = "User ID does not exist"
                txtUserName.Text = ""
                Me.txtUserID.Enabled = True
                Me.txtUserID.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            Me.lblMessage.Text = "Error has occured. Reason: " & ex.Message.ToString()
        End Try
        objOLEDR_Chk = Nothing
        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing
        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing


        ' try and authenticate the user

        'sUsername = txtUserID.Text
        'sPassword = txtUser_PWD.Text

        'lrcValidate = Me.ValidateUserLogin(sUsername, sPassword)

        'Me.txtUser_PWD.Enabled = True
        'Me.txtUser_PWD.Focus()

        ''Select Case Trim(Me.txtUserID.Text)
        ''    Case "CRU"
        ''        Me.txtUserName.Text = "System Administrator"
        ''        Me.txtUser_PWD.Enabled = True
        ''        Me.txtUser_PWD.Focus()
        ''    Case "user1"
        ''        Me.txtUserName.Text = UCase("Life User 1")
        ''        Me.txtUser_PWD.Enabled = True
        ''        Me.txtUser_PWD.Focus()
        ''    Case "user2"
        ''        Me.txtUserName.Text = UCase("Life User 2")
        ''        Me.txtUser_PWD.Enabled = True
        ''        Me.txtUser_PWD.Focus()
        ''    Case "user3"
        ''        Me.txtUserName.Text = UCase("Life User 3")
        ''        Me.txtUser_PWD.Enabled = True
        ''        Me.txtUser_PWD.Focus()
        ''    Case Else
        ''End Select

    End Sub


    ' function to perform the database validation for a user name and password
    Public Function ValidateUserLogin(ByVal sUsername As String, ByVal sPassword As String) As String

        Dim strSP As String = String.Empty
        strSP = "SPGL_GET_USER_INFO"

        Dim sConnection As String = CType(Session("connstr"), String)
        Dim conNW As New OleDbConnection(sConnection)
        Dim comNW As New OleDbCommand(strSP, conNW)

        Dim oleDR As OleDbDataReader

        'Response.Write("<br/>Connection: " & sConnection)

        lrcValidate = "False"

        Try

            comNW.CommandType = CommandType.StoredProcedure
            comNW.Parameters.Add("@usergroup", OleDbType.VarChar, 3).Value = "001"
            comNW.Parameters.Add("@userid01", OleDbType.VarChar, 10).Value = sUsername
            comNW.Parameters.Add("@userid02", OleDbType.VarChar, 10).Value = sUsername
            conNW.Open()

            ' execute the command to obtain the resultant dataset
            oleDR = comNW.ExecuteReader()

            ' with the new data reader parse values and place into the return variable
            If (oleDR.Read()) Then
                'Me.UserName.Text = Me.UserName.Text & " - " & oleDR("pwd_code").ToString & vbNullString

                strPWD = RTrim((oleDR("pwd_code").ToString & vbNullString))
                strPWD = MOD_GEN.DecryptNew(strPWD)

                If strPWD = RTrim(sPassword) Then
                    'Session("AccessModules") = ""
                    'Session("MySID") = oleDR("SID")
                    Session("MyUserIDX") = oleDR("pwd_id").ToString & vbNullString
                    Session("MyUserName") = oleDR("pwd_user_name").ToString & vbNullString
                    Session("MyUserLevelX") = oleDR("pwd_user_level").ToString & vbNullString
                    lrcValidate = "True"
                Else
                    lrcValidate = "Invalid_Password"
                End If
                Me.txtUserName.Text = oleDR("pwd_user_name").ToString & vbNullString
            Else
                Me.txtUserName.Text = "..."
                lrcValidate = "Invalid_User"
            End If


        Catch ex As Exception
            'Throw ex
            lrcValidate = ex.Message.ToString
            Me.lblMessage.Text = lrcValidate.ToString
        Finally
            ' dispose of open objects
            oleDR = Nothing

            comNW.Dispose()
            conNW.Close()
        End Try

        ValidateUserLogin = lrcValidate

    End Function

    Protected Sub DoProc_LogOut()

        Dim strSess As String = "STFID"
        Dim intC As Integer = 0
        Dim intCX As Integer = 0
        Dim MyArray(15) As String

        intC = 0
        intCX = 0
        Try
            'Session("STFID") = RTrim(Me.txtNum.Text)
            'Session("STFID") = RTrim("")

            'Session.Keys
            'Session.Count
            'LOOP THROUGH THE SESSION OBJECT
            '*******************************

            'For intC = 0 To Session.Count - 1
            'Response.Write("<br />" & "Item " & intC & " - Key: " & Session.Keys(intC).ToString & " - Value: : " & Session.Item(intC).ToString)
            '
            'Next

            'SAMPLE SESSION DATA
            '*******************
            ''Item 0 - Key: ActiveSess - Value: : 1
            ''Item 1 - Key: StartdDate - Value: : 06/14/2013 7:01:55 PM
            ''Item 2 - Key: connstr - Value: : Provider=SQLOLEDB;Data Source=ABS-PC;Initial Catalog=ABS;User ID=SA;Password=;
            ''Item 3 - Key: connstr_SQL - Value: : Data Source=ABS-PC;Initial Catalog=ABS;User ID=SA;Password=;
            ''Item 4 - Key: CL_COMP_NAME - Value: : CUSTODIAN AND ALLIED INSURANCE PLC
            ''Item(5 - Key) : MyUserIDX(-Value) : ADM()
            ''Item(6 - Key) : MyUserName(-Value) : System(Administrator)
            ''Item 7 - Key: MyUserLevelX - Value: : 0
            ''Item(8 - Key) : MyUserIDX_NIM(-Value) : ADM()
            ''Item(9 - Key) : MyUserName_NIM(-Value) : System(Administrator)
            ''Item 10 - Key: MyUserLevelX_NIM - Value: : 0
            ''Item 11 - Key: MyLogonTime - Value: : 06/14/2013 7:02:05 PM
            ''Item(12 - Key) : MyUserID(-Value) : ADM()


            'For Each strS As String In Session.Keys
            '    '    ' ...
            '    'Response.Write("<br /> Session ID: " & Session.SessionID & " - Key: " & strSess.ToString & " - Value: " & Session.Item(strSess).ToString)

            '    '    If UCase(strSess) = UCase("connstr") Or _
            '    '      UCase(strSess) = UCase("connstr_SQL") Or _
            '    '      UCase(strSess) = UCase("CL_COMP_NAME") Then
            '    '    Else
            '    '        'Session.Remove(strSess)
            '    '    End If
            '    strSess = strS
            '    Response.Write("<br />" & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
            'Next

            For intCX = 0 To Session.Count - 1

                strSess = Session.Keys(intCX).ToString

                If UCase(strSess) = UCase("connstr") Or _
                  UCase(strSess) = UCase("connstr_SQL") Or _
                  UCase(strSess) = UCase("CL_COMP_NAME") Or _
                  UCase(strSess) = UCase("ActiveSess") Or _
                  UCase(strSess) = UCase("StartdDate") Then
                Else
                    intC = intC + 1
                    MyArray(intC) = strSess
                    'Response.Write("<br />" & "Item " & intC & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)

                End If

            Next

            'Response.Write("<br />" & "Item ubound(): " & UBound(MyArray).ToString)
            'Response.Write("<br />" & "Item Length: " & MyArray.Length)

            For intCX = 1 To intC

                strSess = MyArray(intCX).ToString

                'Response.Write("<br />" & "Removing session Item " & intCX & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
                Session.Remove(strSess.ToString)
                'Session.Contents.Remove(strSess)

            Next

            'Session.Clear()

        Catch ex As Exception
            Response.Write("<br /> Error has Occured at key: " & strSess.ToString & " Reason: " & ex.Message.ToString)
            'Exit Try
        End Try


    End Sub

    'Private Sub Proc_FileUpload()
    '    UploadedFileLog.InnerHtml = ""
    '    If RadUpload1.FileName.Count > 0 Then
    '        For Each postedFile As FileUpload In RadUpload1.UploadedFiles
    '            UploadedFileLog.InnerHtml += "<b>Uploaded file inforamation</b>: <hr />"
    '            UploadedFileLog.InnerHtml += "<b>Nick name</b>: " + NickTextBox.Text
    '            If Not [Object].Equals(postedFile, Nothing) Then
    '                If postedFile.ContentLength > 0 Then
    '                    UploadedFileLog.InnerHtml += String.Format("<br /><b>Filename</b>: {0}", postedFile.FileName)
    '                    UploadedFileLog.InnerHtml += String.Format("<br /><b>File Size</b>: {0} bytes", postedFile.ContentLength)
    '                Else
    '                    UploadedFileLog.InnerHtml += "<br />No uploaded files yet."
    '                End If
    '            Else
    '                UploadedFileLog.InnerHtml += "<br />No uploaded files yet."
    '            End If
    '        Next
    '    End If
    'End Sub

    Public Sub UpdatePasswordAtExpiry(ByVal LoginId As String, ByVal NormalPassword As String)
        Dim intC As Long = 0
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM SEC_USER_LIFE_DETAIL"
        strSQL = strSQL & " WHERE SEC_USER_LOGIN = '" & RTrim(LoginId) & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)


        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable

        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count > 0 Then
                '   Update existing record
                With obj_DT
                    .Rows(0)("firstpassword") = NormalPassword
                End With
                intC = objDA.Update(obj_DT)
            End If

        Catch ex As Exception
            Me.lblMessage.Text = ex.Message.ToString
            Exit Sub
        End Try

        m_cbCommandBuilder.Dispose()
        m_cbCommandBuilder = Nothing

        obj_DT.Dispose()
        obj_DT = Nothing

        objDA.Dispose()
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing
    End Sub
    
End Class
