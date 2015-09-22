Imports System.Data
Imports System.Data.OleDb

Partial Class Codes_PRG_GP_BUS_SEC
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String
    Protected STRPAGE_TITLE As String

    Protected strP_ID As String
    Protected strP_TYPE As String
    Protected strP_DESC As String
    Protected strPOP_UP As String

    Protected myTType As String

    Dim strREC_ID As String
    Dim strTable As String
    Dim strSQL As String
    Dim strErrMsg As String
    Protected strOPT As String = "0"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            strPOP_UP = CType(Request.QueryString("popup"), String)
        Catch ex As Exception
            strPOP_UP = "NO"
        End Try

        If UCase(Trim(strPOP_UP)) = "YES" Then
            Me.PageAnchor_Return_Link.Visible = False
            PageLinks = "<a class='a_return_menu' href='javascript:window.close();' onclick='javascript:window.close();'>Click here to CLOSE PAGE...</a>"
        Else
            Me.PageAnchor_Return_Link.Visible = True
            PageLinks = ""
        End If

        If Not Page.IsPostBack Then
            'Populate box with insurance main class
            Call DoProc_Populate_Sectors()

            Me.cmdDelete_ASP.Enabled = False
            textMessage.Text = RTrim("New Entry...")
            Me.txtSubRiskNum.Focus()

        End If

        If Me.txtAction.Text = "New" Then
            Call DoNew()
            Me.txtAction.Text = ""
            Me.txtSubRiskNum.Enabled = True
            Me.txtSubRiskNum.Focus()
        End If

        If Page.IsPostBack Then
            If Me.txtAction.Text = "Save" Then
                Call DoSave()
                Me.txtAction.Text = ""
            End If
        End If

        If Me.txtAction.Text = "Delete" Then
            Call DoDelete()
            Me.txtAction.Text = ""
        End If

    End Sub


    Protected Sub txtSubRiskNum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubRiskNum.TextChanged
        If RTrim(Me.txtSubRiskNum.Text) <> "" Then
            textMessage.Text = RTrim(Me.txtSubRiskNum.Text)
            strREC_ID = RTrim(Me.txtSubRiskNum.Text)
            strErrMsg = Proc_OpenRecord(Me.txtSubRiskNum.Text)
        End If

    End Sub

    Protected Sub DoNew()
        Me.cboSubRiskName.Enabled = True
        Call Proc_DDL_Get(Me.cboSubRiskName, RTrim("*"))

        'Scan through textboxes on page or form
        'Dim ctrl As Control
        'For Each ctrl In Page.Controls
        '    If TypeOf ctrl Is HtmlForm Then
        '        Dim subctrl As Control
        '        For Each subctrl In ctrl.Controls
        '            If TypeOf subctrl Is System.Web.UI.WebControls.TextBox Then
        '                CType(subctrl, TextBox).Text = ""
        '            End If
        '        Next
        '    End If
        'Next

        With Me
            .txtSubRiskNum.ReadOnly = False
            .txtSubRiskNum.Enabled = True
            .txtSubRiskNum.Text = ""
            .txtSubRiskName.Text = ""
            .txtBS_HOD_Name.Text = ""

            .cmdDelete_ASP.Enabled = False
            .textMessage.Text = "Status: New Entry..."
        End With
        strREC_ID = ""

    End Sub

    Private Sub DoSave()
        textMessage.Text = ""

        If Trim(Me.txtSubRiskNum.Text) = "" Or RTrim(Me.txtSubRiskNum.Text) = "*" Then
            Me.textMessage.Text = "Missing/Invalid business sector code..."
            FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtSubRiskName.Text) = "" Or RTrim(Me.txtSubRiskName.Text) = "*" Then
            Me.textMessage.Text = "Missing/Invalid  business sector name or description..."
            FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            Exit Sub
        End If

        'Dim strRiskNum As String
        'strRiskNum = RTrim(Me.txtRiskNum.Text)
        If RTrim(Me.txtBS_HOD_Name.Text) = "" Or RTrim(Me.txtBS_HOD_Name.Text) = "*" Then
            Me.textMessage.Text = "Missing/Invalid Head of Department Name for this business sector..."
            FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0
        Dim strmyFields As String
        Dim strmyParams As String

        strmyFields = ""
        strmyParams = ""

        strREC_ID = Trim(Me.txtSubRiskNum.Text)

        strTable = "ABSBUSECTAB"
        strSQL = ""
        strSQL = "SELECT CTBS_NUM FROM " & strTable
        strSQL = strSQL & " WHERE CTBS_NUM = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND CTBS_ID = '" & RTrim("001") & "'"


        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        'open connection to database
        objOLEConn.Open()

        Dim mySQLDS As New SqlDataSource

        Dim objOLEDR As OleDbDataReader = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            'Save existing record

            If objOLEDR.IsClosed = False Then
                objOLEDR.Close()
            End If

            strSQL = ""
            strSQL = strSQL & "UPDATE " & strTable & " SET"
            strSQL = strSQL & " CTBS_ID = '" & RTrim("001") & "'"
            strSQL = strSQL & ",CTBS_NUM = '" & RTrim(Me.txtSubRiskNum.Text) & "'"
            strSQL = strSQL & ",CTBS_LONG_DESCR = '" & Left(RTrim(Me.txtSubRiskName.Text), 40) & "'"
            strSQL = strSQL & ",CTBS_SHORT_DESCR = '" & Left(RTrim(Me.txtSubRiskName.Text), 20) & "'"
            strSQL = strSQL & ",CTBS_HOD_NAME = '" & Left(RTrim(Me.txtBS_HOD_Name.Text), 45) & "'"
            strSQL = strSQL & " WHERE CTBS_NUM = '" & RTrim(Me.txtSubRiskNum.Text) & "'"
            strSQL = strSQL & " AND CTBS_ID = '" & RTrim("001") & "'"

            Dim objOLECmd2 As OleDbCommand = New OleDbCommand()
            objOLECmd2.Connection = objOLEConn
            objOLECmd2.CommandType = CommandType.Text
            objOLECmd2.CommandText = strSQL
            intC = objOLECmd2.ExecuteNonQuery()
            objOLECmd2.Dispose()
            objOLECmd2 = Nothing

            Me.textMessage.Text = "Record Saved to Database Successfully."
        Else
            'Save new record

            'Specify the database fields
            strmyFields = ""
            strmyFields = strmyFields & "CTBS_ID,CTBS_NUM,CTBS_LONG_DESCR,CTBS_SHORT_DESCR,CTBS_HOD_NAME"
            strmyFields = strmyFields & ",CTBS_FLAG,CTBS_KEYDTE,CTBS_OPERID"

            'Specify the field parameters, same as database fields, but prefix it with the @ sign
            strmyParams = ""
            strmyParams = strmyParams & "@CTBS_ID,@CTBS_NUM,@CTBS_LONG_DESCR,@CTBS_SHORT_DESCR,@CTBS_HOD_NAME"
            strmyParams = strmyParams & ",@CTBS_FLAG,@CTBS_KEYDTE,@CTBS_OPERID"

            mySQLDS.ConnectionString = CType(Session("connstr_SQL"), String)

            With mySQLDS
                .InsertCommandType = SqlDataSourceCommandType.Text
                .InsertCommand = "INSERT INTO " & strTable & "(" & strmyFields & ")" & " VALUES(" & strmyParams & ")"
                .InsertParameters.Add("CTBS_ID", RTrim("001"))
                .InsertParameters.Add("CTBS_NUM", RTrim(Me.txtSubRiskNum.Text))
                .InsertParameters.Add("CTBS_LONG_DESCR", Left(RTrim(Me.txtSubRiskName.Text), 40))
                .InsertParameters.Add("CTBS_SHORT_DESCR", Left(RTrim(Me.txtSubRiskName.Text), 20))
                .InsertParameters.Add("CTBS_HOD_NAME", Left(RTrim(Me.txtBS_HOD_Name.Text), 45))
                .InsertParameters.Add("CTBS_FLAG", RTrim("A"))
                .InsertParameters.Add("CTBS_KEYDTE", CType(Format(Now, "MM/dd/yyyy"), Date))
                .InsertParameters.Add("CTBS_OPERID", RTrim(Session("MyUserIDX")))
            End With


            Try
                intC = mySQLDS.Insert()
                Me.textMessage.Text = "New Record Saved to Database Successfully."
            Catch ex As Exception
                Me.textMessage.Text = "Error!. Record not save. See your system administrator..."

            End Try


        End If

        mySQLDS = Nothing

        ' dispose of open objects
        objOLECmd.Dispose()
        objOLECmd = Nothing

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If
        objOLEDR = Nothing


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        Me.cmdDelete_ASP.Enabled = True
        Me.txtSubRiskNum.Enabled = False

        Call DoProc_Populate_Sectors()

        FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
        'Me.textMessage.Text = ""

        DoNew()

    End Sub

    Protected Sub DoDelete()

        If Trim(Me.txtSubRiskNum.Text) = "" Then
            Me.textMessage.Text = "Missing sub-risk code..."
            FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0

        strTable = "ABSBUSECTAB"

        strREC_ID = Trim(Me.txtSubRiskNum.Text)

        strSQL = "SELECT CTBS_NUM FROM " & strTable
        strSQL = strSQL & " WHERE CTBS_NUM = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND CTBS_ID = '" & RTrim("001") & "'"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        'open connection to database
        objOLEConn.Open()

        strOPT = "NEW"
        FirstMsg = ""

        Dim objOLEDR As OleDbDataReader = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strOPT = "OLD"
        End If

        ' dispose of open objects
        objOLECmd.Dispose()
        objOLECmd = Nothing

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If
        objOLEDR = Nothing

        Select Case RTrim(strOPT)
            Case "OLD"
                'Delete record
                'Me.textMessage.Text = "Deleting record... "
                strSQL = ""
                strSQL = "DELETE FROM " & strTable
                strSQL = strSQL & " WHERE CTBS_NUM = '" & RTrim(strREC_ID) & "'"
                strSQL = strSQL & " AND CTBS_ID = '" & RTrim("001") & "'"

                Dim objOLECmd2 As OleDbCommand = New OleDbCommand()
                objOLECmd2.Connection = objOLEConn
                objOLECmd2.CommandType = CommandType.Text
                objOLECmd2.CommandText = strSQL
                intC = objOLECmd2.ExecuteNonQuery()
                objOLECmd2.Dispose()
                objOLECmd2 = Nothing
            Case Else
        End Select

        'Try
        'Catch ex As Exception
        'End Try

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        Me.cmdDelete_ASP.Enabled = False

        Call DoProc_Populate_Sectors()

        Me.textMessage.Text = "Record deleted successfully."
        FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "');"
        'Me.textMessage.Text = ""

        Call DoNew()

    End Sub

    Protected Sub cboSubRiskName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSubRiskName.SelectedIndexChanged
        Me.txtSubRiskNum.Text = RTrim(Me.cboSubRiskName.SelectedItem.Value)
        If RTrim(Me.txtSubRiskNum.Text) = "" Or RTrim(Me.txtSubRiskNum.Text) = "*" Or RTrim(Me.txtSubRiskNum.Text) = "0" Then
            Me.txtSubRiskNum.Text = ""
            Call DoNew()
            Exit Sub
        End If

        If RTrim(Me.txtSubRiskNum.Text) <> "" Then
            textMessage.Text = RTrim(Me.txtSubRiskNum.Text)
            strREC_ID = RTrim(Me.txtSubRiskNum.Text)
            strErrMsg = Proc_OpenRecord(Me.txtSubRiskNum.Text)
        End If

    End Sub

    Private Function Proc_OpenRecord(ByVal strRefNo As String) As String

        strErrMsg = "false"

        textMessage.Text = ""
        If Trim(strRefNo) = "" Then
            Proc_OpenRecord = strErrMsg
            Return Proc_OpenRecord
        End If

        strREC_ID = Trim(strRefNo)

        strTable = "ABSBUSECTAB"
        strSQL = ""
        strSQL = strSQL & "SELECT BS.*"
        strSQL = strSQL & " FROM " & strTable & " AS BS"
        strSQL = strSQL & " WHERE BS.CTBS_NUM = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND BS.CTBS_ID = '" & RTrim("001") & "'"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Dim objOLEDR As OleDbDataReader

        'open connection to database
        objOLEConn.Open()

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            Me.txtSubRiskNum.Text = RTrim(CType(objOLEDR("CTBS_NUM") & vbNullString, String))
            Me.txtSubRiskName.Text = RTrim(CType(objOLEDR("CTBS_LONG_DESCR") & vbNullString, String))
            Me.txtBS_HOD_Name.Text = RTrim(CType(objOLEDR("CTBS_HOD_NAME") & vbNullString, String))

            Call Proc_DDL_Get(Me.cboSubRiskName, RTrim(Me.txtSubRiskNum.Text))


            Call DisableBox(Me.txtSubRiskNum)
            strErrMsg = "Status: Data Modification"
            strOPT = "1"
            Me.cmdNew_ASP.Enabled = True
            Me.cmdDelete_ASP.Enabled = True
        Else
            'Me.txtSubRiskName.Text = ""
            Me.cmdDelete_ASP.Enabled = False
            strErrMsg = "Status: New Entry..."
            'Me.txtSubRiskName.Enabled = True
            'Me.txtSubRiskName.Focus()
        End If


        ' dispose of open objects
        objOLECmd.Dispose()
        objOLECmd = Nothing

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If
        objOLEDR = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        textMessage.Text = strErrMsg
        Proc_OpenRecord = strErrMsg
        Return Proc_OpenRecord

    End Function

    Private Sub DisableBox(ByVal objTxtBox As TextBox)
        Dim c As System.Drawing.Color = Drawing.Color.LightGray
        objTxtBox.ReadOnly = True
        objTxtBox.Enabled = False
        'objTxtBox.BackColor = c

    End Sub

    Private Sub Proc_CloseDB(ByVal myOLECmd As OleDbCommand, ByVal myOLEConn As OleDbConnection)
        myOLECmd.Dispose()
        If myOLEConn.State = ConnectionState.Open Then
            myOLEConn.Close()
        End If

    End Sub

    Private Sub Proc_DDL_Get(ByVal pvDDL As DropDownList, ByVal pvRef_Value As String)
        On Error Resume Next
        pvDDL.SelectedIndex = pvDDL.Items.IndexOf(pvDDL.Items.FindByValue(CType(RTrim(pvRef_Value), String)))

    End Sub

    Private Sub DoProc_Populate_Sectors()

        'Populate box with business sector codes

        Me.cboSubRiskName.Items.Clear()
        strTable = "ABSBUSECTAB"

        strSQL = ""
        strSQL = "SELECT CTBS_NUM AS MyFld_Value,CTBS_LONG_DESCR AS MyFld_Text FROM " & strTable
        strSQL = strSQL & " WHERE CTBS_ID IN('001')"
        strSQL = strSQL & " AND CTBS_NUM NOT IN(99999)"
        Call gnPopulate_DropDownList("SECTOR_CODE", Me.cboSubRiskName, strSQL, "select from list", "0")

    End Sub

End Class
