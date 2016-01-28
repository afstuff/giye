
Imports System.Data
Imports System.Data.OleDb

Partial Class Claims_RPT_GRP_CLM_ENTRY
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
    Dim rParams As String() = {"nw", "nw", "nw", "nw", "new", "new"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PageLinks = ""
        'PageLinks = PageLinks & "<a href='javascript:window.close();' runat='server'>Close...</a>"
        PageLinks = "<a href='../MENU_GL.aspx?menu=GL_CLAIM' class='a_sub_menu'>Return to Menu</a>&nbsp;"

    End Sub

    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click
        blnStatus = Get_Grp_ProposalNo(Trim(Me.lblPolicy.Text))

        If blnStatus = False Then
            lblMsg.Text = "Invalid Policy number, POLICY NUMBER DOES NOT EXIST!"
            FirstMsg = "javascript:alert('" + lblMsg.Text + "')"
            Exit Sub
        End If

        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        rParams(0) = "rptGrpClaimRequest"
        rParams(1) = "pCLAIMNUMBER="
        rParams(2) = txtClaimNumber.Text + "&"
        rParams(3) = "pPOLICYNUMBER="
        rParams(4) = lblPolicy.Text + "&"
        rParams(5) = url


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
            strSQL = "SELECT TBIL_GRP_CLM_RPTD_MEMBERNAME, TBIL_GRP_CLM_RPTD_CLM_NO, TBIL_GRP_CLM_RPTD_POLY_NO FROM TBIL_GRP_CLAIM_REPTED where TBIL_GRP_CLM_RPTD_POLY_NO = '" + polyNo + "' and TBIL_GRP_CLM_RPTD_CLM_NO = '" & txtClaimNumber.Text & "'"
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
            'Call gnProc_Populate_Box("GL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
            'DoSearch(txtSearch.Value)

            Dim ds As DataSet = GET_INSUREDDETAILS(Me.txtSearch.Value)
            Dim dt As DataTable = ds.Tables(0)
            cboSearch.DataSource = dt
            Dim dr As DataRow = dt.NewRow()
            dr.Item("TBIL_GRP_CLM_RPTD_MEMBERNAME") = "...Select..."
            dr.Item("TBIL_GRP_CLM_RPTD_CLM_NO") = ""
            dt.Rows.InsertAt(dr, 0)


            cboSearch.DataValueField = "TBIL_GRP_CLM_RPTD_CLM_NO"
            cboSearch.DataTextField = "TBIL_GRP_CLM_RPTD_MEMBERNAME"
            cboSearch.DataBind()


        End If
    End Sub

    Public Sub DoSearch(ByVal sValue As String)

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            'Return strErrMsg
            Exit Sub
        End Try


        'strREC_ID = Trim(FVstrRefNum)


        Dim sql As String = "SELECT PT.TBIL_POLY_REC_ID ,PT.TBIL_POLY_FILE_NO AS MyFld_Value , " _
+ "RTRIM(ISNULL(INSRD.TBIL_INSRD_SURNAME,'')) + ' ' +  RTRIM(ISNULL(INSRD.TBIL_INSRD_FIRSTNAME,'')) + ' - ' + " _
+ "RTRIM(ISNULL(PT.TBIL_POLY_FILE_NO,'')) + ' - ' + RTRIM(ISNULL(PT.TBIL_POLY_PROPSAL_NO,'')) + ' - ' + " _
+ "RTRIM(ISNULL(PT.TBIL_POLY_POLICY_NO,'')) + ' ' + RTRIM('') AS MyFld_Text ,PT.TBIL_POLY_PROPSAL_NO , " _
+ "PT.TBIL_POLY_POLICY_NO as MyFld_Value ,PT.TBIL_POLY_ASSRD_CD ,INSRD.TBIL_INSRD_MDLE FROM TBIL_GRP_POLICY_DET PT LEFT " _
+ " JOIN TBIL_INS_DETAIL AS INSRD ON INSRD.TBIL_INSRD_CODE = PT.TBIL_POLY_ASSRD_CD WHERE (INSRD.TBIL_INSRD_SURNAME " _
+ "LIKE '%' " + sValue + " '%' OR INSRD.TBIL_INSRD_FIRSTNAME LIKE '%' " + sValue + " '%') AND " _
+ "INSRD.TBIL_INSRD_MDLE IN('GRP','G') ORDER BY INSRD.TBIL_INSRD_SURNAME, INSRD.TBIL_INSRD_FIRSTNAME"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(sql, objOLEConn)
        objOLECmd.CommandTimeout = 180
        objOLECmd.CommandType = CommandType.Text

        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = objOLECmd
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        objOLEConn.Close()
        Dim dt As DataTable
        dt = Ds.Tables(0)

        cboSearch.DataSource = dt
        cboSearch.DataValueField = "MyFld_Value"
        cboSearch.DataTextField = "MyFld_Text"
        cboSearch.DataBind()

    End Sub


    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        Dim a = cboSearch.SelectedValue
        Try
            If IsPostBack Then
                If cboSearch.SelectedIndex = -1 Or cboSearch.SelectedIndex = 0 Then

                Else
                    Dim cboValue As String = cboSearch.SelectedItem.Value
                    txtClaimNumber.Text = cboValue.ToString()

                    Dim dt As DataTable = GET_INSUREDDETAILS1(cboValue).Tables(0)
                    Dim dr As DataRow = dt.Rows(0)


                    lblPolicy.Text = dr("TBIL_GRP_CLM_RPTD_POLY_NO").ToString()


                End If
            End If



        Catch ex As Exception
            'lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try

    End Sub

    Public Function GET_INSUREDDETAILS(ByVal sValue As String) As DataSet

        Dim sqlStr As String = "SELECT TBIL_GRP_CLM_RPTD_MEMBERNAME, TBIL_GRP_CLM_RPTD_CLM_NO, TBIL_GRP_CLM_RPTD_POLY_NO FROM TBIL_GRP_CLAIM_REPTED where TBIL_GRP_CLM_RPTD_MEMBERNAME LIKE '%" + sValue + "%'"
        Dim mystrConn As String = CType("Provider=SQLOLEDB;" + gnGET_CONN_STRING(), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = sqlStr
        cmd.CommandType = CommandType.Text
        'cmd.Parameters.AddWithValue("@PARAM_01", sValue)
        'cmd.Parameters.AddWithValue("@PARAM_02", sValue)
        'cmd.Parameters.AddWithValue("@PARAM_TYPE", "GRP")

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()
            Return ds
        Catch ex As Exception
            '_rtnMessage = "Entry failed! " + ex.Message.ToString()

        End Try
        Return Nothing

    End Function

    Public Function GET_INSUREDDETAILS1(ByVal sValue As String) As DataSet

        Dim sqlStr As String = "SELECT TBIL_GRP_CLM_RPTD_MEMBERNAME, TBIL_GRP_CLM_RPTD_CLM_NO, TBIL_GRP_CLM_RPTD_POLY_NO FROM TBIL_GRP_CLAIM_REPTED where TBIL_GRP_CLM_RPTD_CLM_NO = '" + sValue + "'"
        Dim mystrConn As String = CType("Provider=SQLOLEDB;" + gnGET_CONN_STRING(), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = sqlStr
        cmd.CommandType = CommandType.Text
        'cmd.Parameters.AddWithValue("@PARAM_01", sValue)
        'cmd.Parameters.AddWithValue("@PARAM_02", sValue)
        'cmd.Parameters.AddWithValue("@PARAM_TYPE", "GRP")

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()
            Return ds
        Catch ex As Exception
            '_rtnMessage = "Entry failed! " + ex.Message.ToString()

        End Try
        Return Nothing

    End Function

    'Private Function Proc_DoOpenRecord(ByVal FVstrGetType As String, ByVal FVstrRefNum As String, Optional ByVal FVstrRecNo As String = "", Optional ByVal strSearchByWhat As String = "FILE_NUM") As String

    '    strErrMsg = "false"

    '    lblMsg.Text = ""
    '    If Trim(FVstrRefNum) = "" Then
    '        Return strErrMsg
    '        Exit Function
    '    End If

    '    Dim mystrCONN As String = CType(Session("connstr"), String)
    '    Dim objOLEConn As New OleDbConnection(mystrCONN)

    '    Try
    '        'open connection to database
    '        objOLEConn.Open()
    '    Catch ex As Exception
    '        Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
    '        objOLEConn = Nothing
    '        Return strErrMsg
    '        Exit Function
    '    End Try


    '    strREC_ID = Trim(FVstrRefNum)

    '    strTable = strTableName
    '    strSQL = ""
    '    strSQL = strSQL & "SELECT TOP 1 PT.*"
    '    strSQL = strSQL & " FROM " & strTable & " AS PT"
    '    strSQL = strSQL & " WHERE PT.TBIL_POLY_FILE_NO = '" & RTrim(strREC_ID) & "'"
    '    If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
    '        strSQL = strSQL & " AND PT.TBIL_POLY_REC_ID = '" & Val(FVstrRecNo) & "'"
    '    End If
    '    'strSQL = strSQL & " AND PT.TBIL_POLY_PROPSAL_NO = '" & RTrim(strQ_ID) & "'"
    '    'strSQL = strSQL & " AND PT.TBIL_POLY_POLICY_NO = '" & RTrim(strP_ID) & "'"

    '    strSQL = "SPGL_GET_POLICY_DET"

    '    Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
    '    objOLECmd.CommandTimeout = 180
    '    'objOLECmd.CommandType = CommandType.Text
    '    objOLECmd.CommandType = CommandType.StoredProcedure
    '    objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
    '    objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = strREC_ID
    '    objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

    '    Dim objOLEDR As OleDbDataReader

    '    objOLEDR = objOLECmd.ExecuteReader()
    '    If (objOLEDR.Read()) Then
    '        strErrMsg = "true"

    '        Me.txtClaimNumber.Text = RTrim(CType(objOLEDR("TBIL_POLY_POLICY_NO") & vbNullString, String))


    '    Else



    '    End If


    '    ' dispose of open objects
    '    objOLECmd.Dispose()
    '    objOLECmd = Nothing

    '    If objOLEDR.IsClosed = False Then
    '        objOLEDR.Close()
    '    End If
    '    objOLEDR = Nothing

    '    If objOLEConn.State = ConnectionState.Open Then
    '        objOLEConn.Close()
    '    End If
    '    objOLEConn = Nothing

    '    Return strErrMsg

    'End Function


End Class
