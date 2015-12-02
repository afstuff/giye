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

    Protected strRptName As String
    Protected strReportFile As String
    Protected strRptTitle As String
    Protected strRptTitle2 As String

    Protected strTransNum As String

    Protected STRMENU_TITLE As String
    Protected BufferStr As String
    Dim rParams As String() = {"nw", "nw", "new", "new", "new", "new", "new", "new", "new"}


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

        rParams(0) = "rptMEDICAL_UNDER_CLASS_TEST"
        rParams(1) = "pPOLICYNUMBER="
        rParams(2) = txtPolicyNumber.Text + "&"
        'rParams(3) = "PARAM_FILE_NUM="
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
        objOLEDR_Chk = Nothing
        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing

        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing
    End Function

End Class
