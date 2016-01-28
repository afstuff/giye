Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class Policy_PRG_LI_GRP_POLY_CO_ASSURERS
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Protected PageLinks As String

    'Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    'Protected BufferStr As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strF_ID As String
    Protected strP_ID As String
    Protected strQ_ID As String

    Protected strP_TYPE As String
    Protected strP_DESC As String

    Protected GenStart_Date As Date = Now
    Protected GenEnd_Date As Date = Now

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strTmp_Value As String = ""
    Dim myarrData() As String

    Dim strErrMsg As String
    Dim li As ListItem
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "TBIL_GRP_POLICY_CO_ASSURER_SHARE"
        STRMENU_TITLE = "Co-Assurer Share"

        Try
            strF_ID = CType(Request.QueryString("optfileid"), String)
            strF_ID = CType(Session("optfileid"), String)
        Catch ex As Exception
            strF_ID = ""
        End Try

        Try
            strQ_ID = CType(Request.QueryString("optquotid"), String)
            strQ_ID = CType(Session("optquotid"), String)
        Catch ex As Exception
            strQ_ID = ""
        End Try

        Try
            strP_ID = CType(Request.QueryString("optpolid"), String)
            strP_ID = CType(Session("optpolid"), String)
        Catch ex As Exception
            strP_ID = ""
        End Try

        If Not (Page.IsPostBack) Then

            Call Proc_DoNew()

            Me.lblMsg.Text = "Status:"
            Me.cmdPrev.Enabled = True
            Me.cmdNext.Enabled = False

            If Trim(strF_ID) <> "" Then
                Me.txtFileNum.Text = RTrim(strF_ID)
                'Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_POLICY_BY_FILE_NO", RTrim(strF_ID), RTrim(""), RTrim(""))
                Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_GL_POLICY_BY_FILE_NO", RTrim(strF_ID), RTrim(""), RTrim(""))
                If oAL.Item(0) = "TRUE" Then
                    '    'Retrieve the record
                    Me.txtQuote_Num.Text = oAL.Item(3)
                    Me.txtPolNum.Text = oAL.Item(4)

                    Me.cmdNext.Enabled = True
                    If UCase(oAL.Item(18).ToString) = "A" Then
                        Me.cmdNew_ASP.Visible = False
                        'Me.cmdDelItem_ASP.Visible = False
                        Me.cmdPrint_ASP.Visible = False
                    End If

                    Call Proc_DataBind()
                Else
                    '    'Destroy i.e remove the array list object from memory
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    Me.lblMsg.Text = "Status: " & oAL.Item(1)
                End If
                oAL = Nothing
            End If

            ' Call gnProc_Populate_Box("GL_COVER_LIST_OTHERS", RTrim(Me.txtProduct_Num.Text), Me.cboCover_Name
            LoadCoAssurers()
        End If


        If Me.txtAction.Text = "New" Then
            Call Proc_DoNew()
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Save" Then
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete_Item" Then
            Call Proc_DoDelItem()
            Me.txtAction.Text = ""
        End If

    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call Proc_DoSave()
        Me.txtAction.Text = ""

    End Sub
    Private Sub Proc_DataBind()
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            objOLEConn = Nothing
        End Try


        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT CO_INS.*"
        strSQL = strSQL & " FROM " & strTable & " AS CO_INS "
        strSQL = strSQL & " WHERE CO_INS.TBIL_POL_CO_ASS_FILE_NO = '" & RTrim(strF_ID) & "'"
        strSQL = strSQL & " AND CO_INS.TBIL_POL_CO_ASS_PROP_NO = '" & RTrim(strQ_ID) & "'"
        strSQL = strSQL & " ORDER BY CO_INS.TBIL_POL_CO_ASS_REC_ID"

        Try
            Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)

            Dim objDS As DataSet = New DataSet()
            objDA.Fill(objDS, strTable)
            With GridView1
                .DataSource = objDS
                .DataBind()
            End With
            objDS.Dispose()
            objDA.Dispose()
            objDS = Nothing
            objDA = Nothing
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
        End Try
        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        objOLEConn = Nothing

        Me.cmdDelItem_ASP.Enabled = False


        Dim P As Integer = 0
        Dim C As Integer = 0

        C = 0
        For P = 0 To Me.GridView1.Rows.Count - 1
            C = C + 1
        Next
        If C >= 1 Then
            'Me.cmdDelete_ASP.Enabled = True
            Me.cmdDelItem_ASP.Enabled = True
        End If
    End Sub

    Private Sub Proc_DoDelete()

        If Trim(Me.txtFileNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtQuote_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Exit Sub
        End Try


        strTable = strTableName

        strREC_ID = Trim(Me.txtFileNum.Text)

        'Delete record
        'Me.textMessage.Text = "Deleting record... "
        strSQL = ""
        strSQL = "DELETE FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_CO_ASS_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_CO_ASS_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"

        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try
            objOLECmd2.Connection = objOLEConn
            objOLECmd2.CommandType = CommandType.Text
            objOLECmd2.CommandText = strSQL
            intC = objOLECmd2.ExecuteNonQuery()

            If intC >= 1 Then
                Call Proc_DoNew()
                Me.lblMsg.Text = "Record deleted successfully."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Else
                Me.lblMsg.Text = "Sorry!. Record not deleted..."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"

        End Try


        objOLECmd2.Dispose()
        objOLECmd2 = Nothing


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing
    End Sub


    Protected Sub Proc_DoDelItem()

        Dim blnRet As Boolean = False
        Dim P As Integer = 0, C As Integer
        Dim myKey As String = "", myKeyX As String = ""


        For P = 0 To Me.GridView1.Rows.Count - 1
            If CType(Me.GridView1.Rows(P).FindControl("chkSel"), CheckBox).Checked Then
                ' Get the currently selected row using the SelectedRow property.
                Dim row As GridViewRow = GridView1.Rows(P)
                myKeyX = myKeyX & row.Cells(2).Text
                myKeyX = myKeyX & " / "

                myKey = Me.GridView1.Rows(P).Cells(2).Text
                'Me.txtNum.Text = Me.GridView1.Rows(P).Cells(4).Text


                ' Display the required value from the selected row.
                'Me.txtRecNo.Text = row.Cells(2).Text


                'Insert codes to delete selected/checked item(s)

                If Trim(myKey) <> "" Then
                    Me.txtRecNo.Text = myKey
                    Call Proc_DoDelete_Record()
                    C = C + 1
                End If

            End If

        Next

        If C >= 1 Then
            'Me.cmdDelItem_ASP.Enabled = False
            'Me.cmdDelItem.Enabled = False

            Call Proc_DataBind()

            Call Proc_DoNew()

            Me.lblMsg.Text = "Record deleted successfully." & " No of item(s) deleted: " & CStr(C)
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            'Me.textMessage.Text = ""

            Me.lblMsg.Text = "Deleted Item(s): " & myKeyX

        Else
            Me.lblMsg.Text = "Record not deleted ..."

        End If

        'Me.txtTreatyNum.Enabled = True
        'Me.txtTreatyNum.Focus()

    End Sub

    Protected Sub Proc_DoDelete_Record()

        If Trim(Me.txtFileNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtQuote_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtRecNo.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblRecNo.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Exit Sub
        End Try


        strREC_ID = Trim(Me.txtFileNum.Text)
        strTable = strTableName

        strSQL = ""
        'Delete record
        '==============================================
        strSQL = ""
        strSQL = "DELETE FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_CO_ASS_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_CO_ASS_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
        strSQL = strSQL & " AND TBIL_POL_CO_ASS_REC_ID = " & Val(RTrim(Me.txtRecNo.Text)) & ""
       
        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try
            With objOLECmd2
                .Connection = objOLEConn
                .CommandType = CommandType.Text
                .CommandText = strSQL
            End With
            intC = objOLECmd2.ExecuteNonQuery()

            If intC >= 1 Then
                'Call Proc_DoNew()
                'Me.lblMsg.Text = "Record deleted successfully."
                'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Else
                'Me.lblMsg.Text = "Sorry!. Record not deleted..."
                'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
        End Try

        objOLECmd2.Dispose()
        objOLECmd2 = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

    End Sub

    Private Sub Proc_DoNew()
        Dim ctrl As Control
        For Each ctrl In Page.Controls
            If TypeOf ctrl Is HtmlForm Then
                Dim subctrl As Control
                For Each subctrl In ctrl.Controls
                    If TypeOf subctrl Is System.Web.UI.WebControls.TextBox Then
                        If subctrl.ID = "txtFileNum" Or _
                           subctrl.ID = "txtQuote_Num" Or _
                           subctrl.ID = "txtPolNum" Or _
                           subctrl.ID = "xyz_123" Then
                        Else
                            'Response.Write("<br> Control ID: " & subctrl.ID)
                            CType(subctrl, TextBox).Text = ""
                        End If
                    End If
                    If TypeOf subctrl Is System.Web.UI.WebControls.DropDownList Then
                        If subctrl.ID = "cboProductClass" Or _
                           subctrl.ID = "cboProduct" Or _
                           subctrl.ID = "xyz_123" Then
                        Else
                            CType(subctrl, DropDownList).SelectedIndex = -1
                        End If
                    End If
                Next
            End If
        Next

        Me.txtRecNo.Text = "0"
        Me.cmdSave_ASP.Enabled = True
    End Sub

    Private Sub Proc_DoSave()
        Dim strMyYear As String = ""
        Dim strMyMth As String = ""
        Dim strMyDay As String = ""

        Dim strMyDte As String = ""
        Dim strMyDteX As String = ""

        Dim dteStart As Date = Now
        Dim dteEnd As Date = Now

        Dim dteStart_RW As Date = Now
        Dim dteEnd_RW As Date = Now

        Dim mydteX As String = ""
        Dim mydte As Date = Now

        Dim dteDOB As Date = Now

        Dim lngDOB_ANB As Integer = 0

        Dim Dte_Current As Date = Now
        Dim Dte_DOB As Date = Now

        If Me.txtFileNum.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtQuote_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'If Me.txtPolNum.Text = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblPolNum.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        If Me.cboCoAssurer.SelectedIndex = 0 Then
            Me.lblMsg.Text = "Missing " & Me.lblCoAssurer.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If txtPercent_Share.Text <> "" Then
            If Not IsNumeric(txtPercent_Share.Text) Then
                Me.lblMsg.Text = "Share must be numeric"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                txtPercent_Share.Focus()
                Exit Sub
            End If
        Else
            txtPercent_Share.Text = "0.00"
        End If

        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try


        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try


        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_CO_ASS_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
        strSQL = strSQL & " AND TBIL_POL_CO_ASS_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        'or
        'objDA.SelectCommand = New System.Data.OleDb.OleDbCommand(strSQL, objOleConn)

        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        'Dim m_rwContact As System.Data.DataRow
        Dim intC As Integer = 0


        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '   Creating a new record

                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()

                drNewRow("TBIL_POL_CO_ASS_MDLE") = "G"
                drNewRow("TBIL_POL_CO_ASS_FILE_NO") = RTrim(Me.txtFileNum.Text)
                drNewRow("TBIL_POL_CO_ASS_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                drNewRow("TBIL_POL_CO_ASS_POLY_NO") = RTrim(Me.txtPolNum.Text)
                drNewRow("TBIL_POL_CO_ASS_CODE") = RTrim(Me.cboCoAssurer.SelectedItem.Value)
                drNewRow("TBIL_POL_CO_ASS_NAME") = RTrim(Me.cboCoAssurer.SelectedItem.Text)
                drNewRow("TBIL_POL_CO_ASS_SHARE") = RTrim(Me.txtPercent_Share.Text)
                drNewRow("TBIL_POL_CO_ASS_FLAG") = "A"
                drNewRow("TBIL_POL_CO_ASS_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_POL_CO_ASS_KEYDTE") = Now

                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                drNewRow = Nothing

                Me.lblMsg.Text = "New Record Saved to Database Successfully."

            Else
                '   Update existing record

                With obj_DT
                    .Rows(0)("TBIL_POL_CO_ASS_MDLE") = "G"
                    .Rows(0)("TBIL_POL_CO_ASS_FILE_NO") = RTrim(Me.txtFileNum.Text)
                    .Rows(0)("TBIL_POL_CO_ASS_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                    .Rows(0)("TBIL_POL_CO_ASS_POLY_NO") = RTrim(Me.txtPolNum.Text)
                    .Rows(0)("TBIL_POL_CO_ASS_CODE") = RTrim(Me.cboCoAssurer.SelectedItem.Value)
                    .Rows(0)("TBIL_POL_CO_ASS_NAME") = RTrim(Me.cboCoAssurer.SelectedItem.Text)
                    .Rows(0)("TBIL_POL_CO_ASS_SHARE") = RTrim(Me.txtPercent_Share.Text)
                    .Rows(0)("TBIL_POL_CO_ASS_FLAG") = "C"
                    '.Rows(0)("TBIL_POL_CO_ASS_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_POL_CO_ASS_KEYDTE") = Now
                End With

                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                Me.lblMsg.Text = "Record Saved to Database Successfully."

            End If

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            Exit Sub
        End Try

        obj_DT.Dispose()
        obj_DT = Nothing

        m_cbCommandBuilder.Dispose()
        m_cbCommandBuilder = Nothing

        If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
            objDA.SelectCommand.Connection.Close()
        End If
        objDA.Dispose()
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        Me.cmdNext.Enabled = True

        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"

        Call Proc_DataBind()
        Call Proc_DoNew()


    End Sub

    Private Function Proc_DoOpenRecord(ByVal FVstrGetType As String, ByVal FVstrRefNum As String, Optional ByVal FVstrRecNo As String = "", Optional ByVal strSearchByWhat As String = "FILE_NUM") As String

        strErrMsg = "false"

        lblMsg.Text = ""
        If Trim(FVstrRefNum) = "" Then
            Return strErrMsg
            Exit Function
        End If

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Return strErrMsg
            Exit Function
        End Try


        strREC_ID = Trim(FVstrRefNum)

        'strTable = strTableName
        'strSQL = ""
        'strSQL = strSQL & "SELECT TOP 1 ADD_TBL.*"
        'strSQL = strSQL & " FROM " & strTable & " AS ADD_TBL"
        'strSQL = strSQL & " WHERE ADD_TBL.TBIL_POL_ADD_FILE_NO = '" & RTrim(strREC_ID) & "'"
        'If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
        '    strSQL = strSQL & " AND ADD_TBL.TBIL_POL_ADD_REC_ID = '" & Val(FVstrRecNo) & "'"
        'End If


        strTable = strTableName
        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_CO_ASS_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_CO_ASS_REC_ID = '" & Val(RTrim(FVstrRecNo)) & "'"


        'strSQL = "SPIL_GET_POLICY_ADD_PREM"
        'strSQL = "SPGL_GET_POLICY_ADD_PREM"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        objOLECmd.CommandType = CommandType.Text
        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strErrMsg = "true"

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POL_CO_ASS_FILE_NO") & vbNullString, String))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POL_CO_ASS_REC_ID") & vbNullString, String))
            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_CO_ASS_PROP_NO") & vbNullString, String))
            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POL_CO_ASS_POLY_NO") & vbNullString, String))
            Me.cboCoAssurer.SelectedValue = RTrim(CType(objOLEDR("TBIL_POL_CO_ASS_CODE") & vbNullString, String))
           Me.txtPercent_Share.Text = RTrim(CType(objOLEDR("TBIL_POL_CO_ASS_SHARE") & vbNullString, String))
           

            Me.lblFileNum.Enabled = False
            'Call DisableBox(Me.txtFileNum)
            'Me.chkFileNum.Enabled = False
            Me.txtFileNum.Enabled = False
            Me.txtQuote_Num.Enabled = False
            Me.txtPolNum.Enabled = False

            Me.cmdNew_ASP.Enabled = True
            'Me.cmdDelete_ASP.Enabled = True
            Me.cmdNext.Enabled = True
            strOPT = "2"
            Me.lblMsg.Text = "Status: Data Modification"

        Else
            Me.cmdNext.Enabled = False
            strOPT = "1"
            Me.lblMsg.Text = "Status: New Entry..."
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

        Return strErrMsg

    End Function

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        'Dim row As GridViewRow = GridView1.Rows(e.NewSelectedIndex)

        GridView1.PageIndex = e.NewPageIndex
        Call Proc_DataBind()
        lblMsg.Text = "Page " & GridView1.PageIndex + 1 & " of " & Me.GridView1.PageCount
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = GridView1.SelectedRow
        ' Display the required value from the selected row.
        Me.txtRecNo.Text = row.Cells(2).Text
        strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, Val(RTrim(Me.txtRecNo.Text)))

        lblMsg.Text = "You selected " & Me.txtFileNum.Text & " / " & Me.txtRecNo.Text & "."
    End Sub

    Protected Sub cmdPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        Session("optfileid") = Trim(Me.txtFileNum.Text).ToString
        Session("optquotid") = Trim(Me.txtQuote_Num.Text).ToString
        Session("optpolid") = Trim(Me.txtPolNum.Text).ToString

        Dim pvURL As String = ""
        'pvURL = "prg_li_grp_poly_members.aspx?q=x"
        pvURL = "prg_li_grp_poly_prem.aspx?q=x"
        Response.Redirect(pvURL)
    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Session("optfileid") = Trim(Me.txtFileNum.Text).ToString
        Session("optquotid") = Trim(Me.txtQuote_Num.Text).ToString
        Session("optpolid") = Trim(Me.txtPolNum.Text).ToString

        Dim pvURL As String = ""
        'pvURL = "prg_li_grp_poly_medic_info.aspx?q=x"
        'pvURL = "prg_li_grp_add_members.aspx?q=x"
        pvURL = "prg_li_grp_poly_members.aspx?q=x"
        Response.Redirect(pvURL)
    End Sub
    Private Sub LoadCoAssurers()
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN
        Dim objOLEComm As OleDbCommand = New OleDbCommand()
        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            lblMsg.Visible = True
            objOLEConn = Nothing
            Exit Sub
        End Try

        Try
            objOLEComm.Connection = objOLEConn
            strSQL = ""
            strSQL = strSQL + "SELECT TBGL_REC_ID, TBGL_DESC FROM TBGL_REINSURANCE"
            strSQL = strSQL + " WHERE TBGL_FLAG NOT IN('D')"
            objOLEComm.CommandText = strSQL
            objOLEComm.CommandType = CommandType.Text
            Dim objOLEReader As OleDbDataReader = objOLEComm.ExecuteReader()
            li = New ListItem
            li.Text = "Select"
            li.Value = "0"
            cboCoAssurer.Items.Add(li)
            While (objOLEReader.Read())
                li = New ListItem
                li.Text = objOLEReader("TBGL_DESC")
                li.Value = objOLEReader("TBGL_REC_ID")
                cboCoAssurer.Items.Add(li)
            End While
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            lblMsg.Visible = True
            Exit Sub
        End Try

        If objOLEComm.Connection.State = ConnectionState.Open Then
            objOLEComm.Connection.Close()
        End If
        '   objOLEComm.Dispose()
        objOLEComm = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing
    End Sub
End Class
