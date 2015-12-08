Imports System.Data
Imports System.Data.OleDb

Partial Class Reports_GRP_MEDICAL_EXAM_LIST
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String
    Protected PageURLs As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Dim strREC_ID As String
    Protected myTType As String = "0"

    Dim dteStart As Date
    Dim dteEnd As Date

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strErrMsg As String

    Protected strRptName As String
    Protected strReportFile As String
    Protected strRptTitle As String
    Protected strRptTitle2 As String

    Protected strTransNum As String

    Protected STRMENU_TITLE As String
    Protected BufferStr As String
    Dim rParams As String() = {"nw", "nw", "new", "new"}


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PageLinks = ""
        'PageLinks = PageLinks & "<a href='javascript:window.close();' runat='server'>Close...</a>"
        PageLinks = "<a href='../MENU_GL.aspx?menu=GL_UND' class='a_sub_menu'>Return to Menu</a>&nbsp;"

    End Sub

    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click
        blnStatus = Get_Grp_ProposalNo(Trim(Me.txtPolicyNumber.Text))

        If blnStatus = False Then
            lblMsg.Text = "Invalid Policy number, POLICY NUMBER DOES NOT EXIST!"
            FirstMsg = "javascript:alert('" + lblMsg.Text + "')"
            Exit Sub
        End If

        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        rParams(0) = "rptMEDICAL_UNDER_CLASS_TEST"
        rParams(1) = "pPOLICYNUMBER="
        rParams(2) = txtPolicyNumber.Text + "&"
        rParams(3) = url
        'rParams(4) = txtFileNo.Text + "&"
        'rParams(5) = "PARAM_BATCH_NUM="
        'rParams(6) = txtBatchNo.Text + "&"
        'rParams(7) = "PARAM_MODULE="
        'rParams(8) = "G&"

        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")

    End Sub

    Public Function Get_Grp_ProposalNo(ByVal polyNo As String) As Boolean
        lblMsg.Text = ""
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
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn_Chk = Nothing
            blnStatus = False
            'Exit Sub
        End Try

        Try
            strTable = strTableName
            strSQL = ""
            strSQL = "SELECT * FROM TBIL_GRP_POLICY_MEMBERS WHERE TBIL_POL_MEMB_POLY_NO='" & polyNo & "'"
            objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
            objOLECmd_Chk.CommandType = CommandType.Text

            objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
            If (objOLEDR_Chk.Read()) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try


    End Function

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            Call gnProc_Populate_Box("GL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If
    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtPolicyNumber.Text = ""

            Else
                Me.txtPolicyNumber.Text = Me.cboSearch.SelectedItem.Value
                strStatus = Proc_DoOpenRecord(RTrim("POL"), Me.txtPolicyNumber.Text, RTrim("0"))


            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
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

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 PT.*"
        strSQL = strSQL & " FROM " & strTable & " AS PT"
        strSQL = strSQL & " WHERE PT.TBIL_POLY_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND PT.TBIL_POLY_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If
        'strSQL = strSQL & " AND PT.TBIL_POLY_PROPSAL_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND PT.TBIL_POLY_POLICY_NO = '" & RTrim(strP_ID) & "'"

        strSQL = "SPGL_GET_POLICY_DET"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        'objOLECmd.CommandType = CommandType.Text
        objOLECmd.CommandType = CommandType.StoredProcedure
        objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = strREC_ID
        objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strErrMsg = "true"

            Me.txtPolicyNumber.Text = RTrim(CType(objOLEDR("TBIL_POLY_FILE_NO") & vbNullString, String))


        Else



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


End Class
