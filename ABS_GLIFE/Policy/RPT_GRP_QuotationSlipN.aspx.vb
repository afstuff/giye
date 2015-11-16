Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class Policy_RPT_GRP_QuotationSlipN
    Inherits System.Web.UI.Page
    Dim ErrorInd As String
    Protected FirstMsg As String
    Protected STRMENU_TITLE As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean
    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String
    Dim rParams As String() = {"nw", "nw", "new", "new", "new", "new", "new", "new", "new"}
    Protected PageLinks As String
    Dim strREC_ID As String
    Protected strOPT As String = "0"
    Protected BufferStr As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "TBIL_GRP_QUOTATION_ENTRIES"
        PageLinks = ""
        PageLinks = "<a href='PRG_GP_PROP_POLICY.aspx' class='a_sub_menu'>Return to Menu</a>&nbsp;"

        Try
            strOPT = Page.Request.QueryString("opt").ToString
            'strOPT options = I001
        Catch
            strOPT = "PDI_ERR"
        End Try

        Select Case UCase(Trim(strOPT))
            Case "QUOT_SLIP"
                STRMENU_TITLE = UCase("+++ Quotation Slip +++ ")
                BufferStr = ""
        End Select

    End Sub
    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        'If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        'ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
        '    cboSearch.Items.Clear()
        '    cboSearch.Items.Add("*** Select ***")
        '    Dim dt As DataTable = SearchHelp().Tables(0)
        '    cboSearch.DataSource = dt
        '    cboSearch.DataValueField = "TBIL_QUO_REC_ID"
        '    cboSearch.DataTextField = "TBIL_QUO_PROSPECT"
        '    cboSearch.DataBind()
        'End If
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            cboSearch.Items.Clear()
            cboSearch.Items.Add("*** Select ***")
            Dim dt As DataTable = SearchHelp().Tables(0)
            cboSearch.DataSource = dt
            cboSearch.DataValueField = "MyFld_Value"
            cboSearch.DataTextField = "MyFld_Text"
            cboSearch.DataBind()
        End If
    End Sub
    Protected Function SearchHelp() As DataSet
        'strTable = "TBIL_GRP_QUOTATION_ENTRIES"
        'strSQL = ""
        'strSQL = "SELECT TBIL_QUO_PROSPECT"
        'strSQL = strSQL & ", TBIL_QUO_REC_ID"
        'strSQL = strSQL & " FROM " & strTable
        'strSQL = strSQL & " WHERE TBIL_QUO_PROSPECT LIKE '" & RTrim(txtSearch.Value) & "%'"
        'strSQL = strSQL & " AND TBIL_QUO_FLAG <> 'D'"
        'strSQL = strSQL & " ORDER BY TBIL_QUO_PROSPECT"


        'Dim mystrCONN As String = CType(Session("connstr"), String)
        'Dim objOLEConn As New OleDbConnection()
        'objOLEConn.ConnectionString = mystrCONN

        'Try
        '    'open connection to database
        '    objOLEConn.Open()
        'Catch ex As Exception
        '    Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
        '    'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
        '    objOLEConn = Nothing
        '    Exit Function
        'End Try

        'Try

        '    Dim adapter As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)
        '    Dim ds As DataSet = New DataSet()
        '    adapter.Fill(ds)
        '    'Dim a As Integer = ds.Tables(0).Rows.Count
        '    Return ds
        'Catch ex As Exception
        '    Me.lblMsg.Text = ex.Message.ToString
        '    Exit Function
        'End Try
        'If objOLEConn.State = ConnectionState.Open Then
        '    objOLEConn.Close()
        'End If
        'objOLEConn = Nothing

        strTable = "TBIL_INS_DETAIL"
        strSQL = strSQL & "SELECT TBIL_INSRD_REC_ID AS MyFld_Rec_ID, TBIL_INSRD_ID AS MyFld_ID, TBIL_INSRD_CODE AS MyFld_Value"
        strSQL = strSQL & ",RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,'')) AS MyFld_Text"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " WHERE TBIL_INSRD_MDLE IN('PRO','P')"
        strSQL = strSQL & " AND (TBIL_INSRD_SURNAME LIKE '%" & RTrim(Me.txtSearch.Value) & "%'"
        strSQL = strSQL & " OR TBIL_INSRD_FIRSTNAME LIKE '%" & RTrim(Me.txtSearch.Value) & "%')"
        '  strSQL = strSQL & " AND TBIL_QUO_FLAG <> 'D'"
        'strSQL = strSQL & " ORDER BY TBIL_QUO_PROSPECT"

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
        txtProspect.Text = ""
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
            Else
                ' txtFileNum.Text = Me.cboSearch.SelectedItem.Value
                txtFileNum.Text = Me.cboSearch.SelectedItem.Value
                txtProspect.Text = Me.cboSearch.SelectedItem.Text
                GetQuotation(cboSearch.SelectedValue.Trim())
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
            lblMsg.Visible = True
        End Try
    End Sub

    Private Sub GetQuotation(ByVal RecId As String)
        lblMsg.Text = ""
        lblMsg.Visible = False
        'Dim mystrCONN As String = CType(Session("connstr"), String)
        'Dim objOLEConn As New OleDbConnection()
        'objOLEConn.ConnectionString = mystrCONN
        'Dim objOLEComm As OleDbCommand = New OleDbCommand()

        'Try
        '    'open connection to database
        '    objOLEConn.Open()
        'Catch ex As Exception
        '    Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
        '    'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
        '    lblMsg.Visible = True
        '    objOLEConn = Nothing
        '    Exit Sub
        'End Try


        'Try
        '    strSQL = ""
        '    strSQL = "SELECT TOP 1 * FROM " & strTableName
        '    strSQL = strSQL & " WHERE TBIL_QUO_REC_ID = '" & RecId & "'"
        '    strSQL = strSQL & " AND TBIL_QUO_FLAG <> 'D'"
        '    objOLEComm.Connection = objOLEConn
        '    objOLEComm.CommandText = strSQL
        '    objOLEComm.CommandType = CommandType.Text
        '    Dim objOLEReader As OleDbDataReader = objOLEComm.ExecuteReader()
        '    If objOLEReader.HasRows = True Then
        '        objOLEReader.Read()
        '        txtProspect.Text = objOLEReader("TBIL_QUO_PROSPECT")
        '    End If
        'Catch ex As Exception
        '    Me.lblMsg.Text = ex.Message.ToString
        '    lblMsg.Visible = True
        '    Exit Sub
        'End Try

        'If objOLEComm.Connection.State = ConnectionState.Open Then
        '    objOLEComm.Connection.Close()
        'End If
        ''   objOLEComm.Dispose()
        'objOLEComm = Nothing

        'If objOLEConn.State = ConnectionState.Open Then
        '    objOLEConn.Close()
        'End If
        'objOLEConn = Nothing


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
            strSQL = strSQL & " WHERE TBIL_QUO_PROSPECT_ID = '" & RecId & "'"
            strSQL = strSQL & " AND TBIL_QUO_FLAG <> 'D'"

            objOLEComm.Connection = objOLEConn
            objOLEComm.CommandText = strSQL
            objOLEComm.CommandType = CommandType.Text
            Dim objOLEReader As OleDbDataReader = objOLEComm.ExecuteReader()
            If objOLEReader.HasRows = True Then
                objOLEReader.Read()
                txtProspect.Text = objOLEReader("TBIL_QUO_PROSPECT")
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
    Private Sub ValidateControls(ByRef ErrorInd As String)
        If (txtProspect.Text = String.Empty) Then
            lblMsg.Text = "Prospect must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtFileNum.Text = String.Empty) Then
            lblMsg.Text = "Please search for the Quotation Slip you want to print"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
    End Sub
    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click
        ErrorInd = ""
        lblMsg.Text = ""
        ValidateControls(ErrorInd)
        If ErrorInd = "Y" Then
            Exit Sub
        End If
        rParams(0) = "rptQuotationSlip1"
        rParams(1) = "PARAM_RECID="
        rParams(2) = txtFileNum.Text + "&"
        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        txtFileNum.Text = ""
        txtProspect.Text = ""
    End Sub
End Class
