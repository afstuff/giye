Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class Policy_PRG_LI_GRP_QUOT_ENTRY
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "TBIL_GRP_QUOTATION_ENTRIES"
        STRMENU_TITLE = "Quotation Slip Entry"

        If Not (Page.IsPostBack) Then
            txtFileNum.Text = 0
        End If


        If Me.txtAction.Text = "New" Then
            Call Proc_DoNew()
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Save" Then
            'Call Proc_DoSave()
            Me.txtAction.Text = ""
        End If

        'If Me.txtAction.Text = "Delete" Then
        'Call DoDelete()
        'Me.txtAction.Text = ""
        'End If

        If Me.txtAction.Text = "Delete_Item" Then
            '  Call Proc_DoDelItem()
            Me.txtAction.Text = ""
        End If
    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call Proc_DoSave()
        Me.txtAction.Text = ""
    End Sub

    Private Sub Proc_DoNew()
        txtProspect.Text = ""
        txtTotEmolument.Text = ""
        txtTotNoStaff.Text = ""
        txtTransDate.Text = ""
        txtRate.Text = ""
        txtPremium.Text = ""
        txtFileNum.Text = 0
        Me.cmdSave_ASP.Enabled = True
        cmdDel_ASP.Enabled = False
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

        If Me.txtProspect.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblProspect.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtTotEmolument.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblTotEmolument.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtTotEmolument.Focus()
            Exit Sub
        End If
        If Not IsNumeric(txtTotEmolument.Text) Then
            Me.lblMsg.Text = "Estimated Total Emolument must be numeric"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtTotEmolument.Focus()
            Exit Sub
        End If



        If Me.txtTotNoStaff.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblTotNoStaff.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtTotNoStaff.Focus()
            Exit Sub
        End If
        If Not IsNumeric(txtTotNoStaff.Text) Then
            Me.lblMsg.Text = "Total Number of staff must be numeric"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtTotNoStaff.Focus()
            Exit Sub
        End If

        If Me.txtTransDate.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblTransDate.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtTransDate.Focus()
            Exit Sub
        End If

        If Me.txtRate.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblRate.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtRate.Focus()
            Exit Sub
        End If
        If Not IsNumeric(txtRate.Text) Then
            Me.lblMsg.Text = "Rate must be numeric"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtRate.Focus()
            Exit Sub
        End If

        If Me.txtPremium.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPremium.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtPremium.Focus()
            Exit Sub
        End If
        If Not IsNumeric(txtPremium.Text) Then
            Me.lblMsg.Text = "Premium must be numeric"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtPremium.Focus()
            Exit Sub
        End If

        If RTrim(Me.txtTransDate.Text) = "" Or Len(Trim(Me.txtTransDate.Text)) <> 10 Then
            Me.lblMsg.Text = "Missing or Invalid date - " & Me.lblTransDate.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtTransDate.Focus()
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtTransDate.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblTransDate.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtTransDate.Focus()
            Exit Sub
        End If
        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Exit Sub
        End If
        Me.txtTransDate.Text = RTrim(strMyDte)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteStart = Format(mydte, "MM/dd/yyyy")

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
        strSQL = strSQL & " WHERE TBIL_QUO_REC_ID = '" & RTrim(txtFileNum.Text) & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        Dim intC As Integer = 0


        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '   Creating a new record

                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()
                ' drNewRow("TBIL_POL_ADD_FILE_NO") = RTrim(Me.txtFileNum.Text)
                drNewRow("TBIL_QUO_PROSPECT") = RTrim(Me.txtProspect.Text)
                drNewRow("TBIL_QUO_TOT_EMOLUMENT") = RTrim(Me.txtTotEmolument.Text)
                drNewRow("TBIL_QUO_NO_OF_STAFF") = RTrim(Me.txtTotNoStaff.Text)
                drNewRow("TBIL_QUO_RATE") = Val(Me.txtRate.Text)
                drNewRow("TBIL_QUO_TRANS_DATE") = dteStart
                drNewRow("TBIL_QUO_PREMIUM") = RTrim(Me.txtPremium.Text)

                drNewRow("TBIL_QUO_FLAG") = "A"
                drNewRow("TBIL_QUO_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_QUO_KEYDTE") = Now

                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                drNewRow = Nothing

                Me.lblMsg.Text = "New Record Saved to Database Successfully."

            Else
                '   Update existing record



                With obj_DT
                    .Rows(0)("TBIL_QUO_REC_ID") = RTrim(Me.txtFileNum.Text)
                    .Rows(0)("TBIL_QUO_PROSPECT") = RTrim(Me.txtProspect.Text)
                    .Rows(0)("TBIL_QUO_TOT_EMOLUMENT") = RTrim(Me.txtTotEmolument.Text)
                    .Rows(0)("TBIL_QUO_NO_OF_STAFF") = RTrim(Me.txtTotNoStaff.Text)
                    .Rows(0)("TBIL_QUO_RATE") = Val(Me.txtRate.Text)
                    .Rows(0)("TBIL_QUO_TRANS_DATE") = dteStart
                    .Rows(0)("TBIL_QUO_PREMIUM") = RTrim(Me.txtPremium.Text)

                    .Rows(0)("TBIL_QUO_FLAG") = "C"
                    .Rows(0)("TBIL_QUO_OPERID") = CType(myUserIDX, String)
                    .Rows(0)("TBIL_QUO_KEYDTE") = Now
                End With
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)
                Me.lblMsg.Text = "Record Saved to Database Successfully."
            End If
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            Exit Sub
        End Try
        Proc_DoNew()
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
        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        Proc_DoNew()
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            cboSearch.Items.Clear()
            cboSearch.Items.Add("*** Select ***")
            Dim dt As DataTable = SearchHelp().Tables(0)
            cboSearch.DataSource = dt
            cboSearch.DataValueField = "TBIL_QUO_REC_ID"
            cboSearch.DataTextField = "TBIL_QUO_PROSPECT"
            cboSearch.DataBind()
        End If
    End Sub

    Protected Function SearchHelp() As DataSet
        strTable = "TBIL_GRP_QUOTATION_ENTRIES"
        strSQL = ""
        strSQL = "SELECT TBIL_QUO_PROSPECT"
        strSQL = strSQL & ", TBIL_QUO_REC_ID"
        strSQL = strSQL & " FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_QUO_PROSPECT LIKE '" & RTrim(txtSearch.Value) & "%'"
        strSQL = strSQL & " AND TBIL_QUO_FLAG <> 'D'"
        strSQL = strSQL & " ORDER BY TBIL_QUO_PROSPECT"


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
            Exit Function
        End Try

        Try

            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            'Dim a As Integer = ds.Tables(0).Rows.Count
            Return ds
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            Exit Function
        End Try
        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

    End Function

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        Proc_DoNew()
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
            Else
                txtFileNum.Text = Me.cboSearch.SelectedItem.Value
                GetQuotation(cboSearch.SelectedValue.Trim())
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
            lblMsg.Visible = True
        End Try
    End Sub

    Private Sub GetQuotation(ByVal RecId As String)
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
            strSQL = ""
            strSQL = "SELECT TOP 1 * FROM " & strTableName
            strSQL = strSQL & " WHERE TBIL_QUO_REC_ID = '" & RecId & "'"
            strSQL = strSQL & " AND TBIL_QUO_FLAG <> 'D'"
            objOLEComm.Connection = objOLEConn
            objOLEComm.CommandText = strSQL
            objOLEComm.CommandType = CommandType.Text
            Dim objOLEReader As OleDbDataReader = objOLEComm.ExecuteReader()
            If objOLEReader.HasRows = True Then
                objOLEReader.Read()
                txtProspect.Text = objOLEReader("TBIL_QUO_PROSPECT")
                txtTotEmolument.Text = objOLEReader("TBIL_QUO_TOT_EMOLUMENT")
                txtTotNoStaff.Text = objOLEReader("TBIL_QUO_NO_OF_STAFF")
                txtRate.Text = objOLEReader("TBIL_QUO_RATE")
                txtPremium.Text = objOLEReader("TBIL_QUO_PREMIUM")
                If Not IsDBNull(objOLEReader("TBIL_QUO_TRANS_DATE")) Then
                    txtTransDate.Text = Format(objOLEReader("TBIL_QUO_TRANS_DATE"), "dd/MM/yyyy")
                End If
                cmdDel_ASP.Enabled = True
            End If
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

    Private Sub Proc_DoDelete()
        Dim xc As Integer = 0

        If Trim(Me.txtFileNum.Text) = "" Then
            Me.lblMsg.Text = "Please select a Quotation to delete"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Dim intC As Long = 0

        'Dim mystrCONN As String = CType(Session("connstr"), String)
        'Dim objOLEConn As New OleDbConnection(mystrCONN)

        'Try
        '    'open connection to database
        '    objOLEConn.Open()
        'Catch ex As Exception
        '    Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
        '    objOLEConn = Nothing
        '    Exit Sub
        'End Try


        'strTable = strTableName

        'strREC_ID = Trim(Me.txtFileNum.Text)

        ''Delete record
        ''Me.textMessage.Text = "Deleting record... "
        'strSQL = ""
        'strSQL = "DELETE FROM " & strTable
        'strSQL = strSQL & " WHERE TBIL_QUO_REC_ID = '" & RTrim(txtFileNum.Text) & "'"

        'Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        'Try
        '    objOLECmd2.Connection = objOLEConn
        '    objOLECmd2.CommandType = CommandType.Text
        '    objOLECmd2.CommandText = strSQL
        '    intC = objOLECmd2.ExecuteNonQuery()

        '    If intC >= 1 Then
        '        Me.lblMsg.Text = "Record deleted successfully."
        '        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
        '        Proc_DoNew()
        '    Else
        '        Me.lblMsg.Text = "Sorry!. Record not deleted..."
        '        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
        '    End If

        'Catch ex As Exception
        '    Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"

        'End Try


        'objOLECmd2.Dispose()
        'objOLECmd2 = Nothing


        'If objOLEConn.State = ConnectionState.Open Then
        '    objOLEConn.Close()
        'End If
        'objOLEConn = Nothing

        'Me.txtNum.Enabled = True
        'Me.txtNum.Focus()


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
        strSQL = strSQL & " WHERE TBIL_QUO_REC_ID = '" & RTrim(txtFileNum.Text) & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        Dim intC As Integer = 0


        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                Me.lblMsg.Text = "No Record to be Deleted"
            Else
                '   Move D to the flag column
                With obj_DT
                    .Rows(0)("TBIL_QUO_FLAG") = "D"
                    .Rows(0)("TBIL_QUO_OPERID") = CType(myUserIDX, String)
                    .Rows(0)("TBIL_QUO_KEYDTE") = Now
                End With
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)
                Me.lblMsg.Text = "Record Deleted from Database Successfully."
            End If
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            Exit Sub
        End Try
        Proc_DoNew()
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
        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"




    End Sub

    Protected Sub cmdDel_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDel_ASP.Click
        Proc_DoDelete()
    End Sub

    Protected Sub Validate()

    End Sub
End Class
