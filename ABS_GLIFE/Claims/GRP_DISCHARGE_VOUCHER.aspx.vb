
Imports System.Data
Imports System.Data.OleDb

Partial Class GRP_DISCHARGE_VOUCHER
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String
    Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String

    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strP_ID As String
    Protected strP_TYPE As String
    Protected strP_DESC As String
    Protected strPOP_UP As String

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strErrMsg As String

    'Protected FirstMsg As String
    Dim rParams As String() = {"nw", "nw", "nw", "nw", "nw", "nw", "new", "new"}
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        cmdPrint_ASP.Enabled = False

        PageLinks = ""
        'PageLinks = PageLinks & "<a href='javascript:window.close();' runat='server'>Close...</a>"
        PageLinks = "<a href='../MENU_GL.aspx?menu=GL_UND' class='a_sub_menu' style='float:right;'>Return to Menu</a>&nbsp;<br/>"

    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        cmdPrint_ASP.Enabled = False

        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            cboSearch.Items.Clear()
            cboSearch.Items.Add("* Select Insured *")
            Dim dt As DataTable = GET_INSURED(txtSearch.Value.Trim()).Tables(0)
            cboSearch.DataSource = dt
            cboSearch.DataValueField = "VALUE"
            cboSearch.DataTextField = "TEXT"
            cboSearch.DataBind()
        End If
    End Sub

    Public Function GET_INSURED(ByVal sValue As String) As DataSet

        Dim sqlStr As String = "SELECT *, TBIL_GRP_CLM_RPTD_CLM_NO AS VALUE , TBIL_GRP_CLM_RPTD_MEMBERNAME +' - '+ CONVERT(VARCHAR, TBIL_GRP_CLM_RPTD_CLM_NO)+' - '+CONVERT(VARCHAR, TBIL_GRP_CLM_RPTD_POLY_NO)+' - '+ CONVERT(VARCHAR, TBIL_GRP_CLAIM_REPTED_REC_ID) AS TEXT FROM TBIL_GRP_CLAIM_REPTED where TBIL_GRP_CLM_RPTD_MEMBERNAME like '%" + sValue + "%'"
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

    Protected Sub cboSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSearch.SelectedIndexChanged
        Dim a = cboSearch.SelectedValue
        Try
            If IsPostBack Then
                If cboSearch.SelectedIndex = -1 Or cboSearch.SelectedIndex = 0 Then

                Else
                    Dim cboValue As String = cboSearch.SelectedItem.Value
                    Dim dt As DataTable = GET_INSUREDDETAILS(cboValue).Tables(0)
                    Dim dr As DataRow = dt.Rows(0)

                    lblAssured.Text = dr("TBIL_GRP_CLM_RPTD_MEMBERNAME").ToString()

                    Dim dr1 As DataRow = GET_GROUPNAME(dr("TBIL_GRP_CLM_RPTD_POLY_NO").ToString())
                    lblGroup.Text = dr1("NAME").ToString()
                    lblClaim.Text = dr("TBIL_GRP_CLM_RPTD_CLM_NO").ToString()
                    lblPolicy.Text = dr("TBIL_GRP_CLM_RPTD_POLY_NO").ToString()
                    lblMemNum.Text = dr("TBIL_GRP_CLM_MEM_STAFF_NO").ToString()


                End If
            End If



        Catch ex As Exception
            'lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try


    End Sub


    Public Function GET_INSUREDDETAILS(ByVal polyNumber As String) As DataSet

        Dim sqlStr As String = "SELECT * FROM TBIL_GRP_CLAIM_REPTED where TBIL_GRP_CLM_RPTD_CLM_NO = '" + polyNumber + "'"
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

    Public Function GET_GROUPNAME(ByVal polyNumber As String) As DataRow

        Dim sqlStr As String = "SELECT DISTINCT ISNULL(A.TBIL_INSRD_SURNAME, '') +' '+ISNULL(A.TBIL_INSRD_FIRSTNAME, '') AS NAME FROM tbil_ins_detail A, TBIL_GRP_POLICY_DET B WHERE A.TBIL_INSRD_CODE = B.TBIL_POLY_ASSRD_CD AND B.TBIL_POLY_POLICY_NO = '" + polyNumber + "'"
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

            Dim dt As DataTable = ds.Tables(0)
            Dim dr As DataRow = dt.Rows(0)

            Return dr
        Catch ex As Exception
            '_rtnMessage = "Entry failed! " + ex.Message.ToString()

        End Try
        Return Nothing

    End Function

    Protected Sub cmdSave_ASP_Click(sender As Object, e As EventArgs) Handles cmdSave_ASP.Click
        If (rbtMCCD.SelectedIndex < 0) Then
            lblMessage.Text = ""
            lblMessage.Text = "MCCD cannot be empty!"
            FirstMsg = "javascript:alert('" + lblMessage.Text + "')"

            Exit Sub
        End If

        If (rbtBurial.SelectedIndex < 0) Then
            lblMessage.Text = ""
            lblMessage.Text = "BURIAL CERT./ATTESTATION cannot be empty!"
            FirstMsg = "javascript:alert('" + lblMessage.Text + "')"

            Exit Sub
        End If

        If (rbtPolice.SelectedIndex < 0) Then
            lblMessage.Text = ""
            lblMessage.Text = "POLICE REPORT cannot be empty!"
            FirstMsg = "javascript:alert('" + lblMessage.Text + "')"

            Exit Sub
        End If

        If (rbtPolice.SelectedIndex < 0) Then
            lblMessage.Text = ""
            lblMessage.Text = "POLICE REPORT cannot be empty!"
            FirstMsg = "javascript:alert('" + lblMessage.Text + "')"

            Exit Sub
        End If

        If (rbtDeath.SelectedIndex < 0) Then
            lblMessage.Text = ""
            lblMessage.Text = "DEATH CERTIFICATE cannot be empty!"
            FirstMsg = "javascript:alert('" + lblMessage.Text + "')"

            Exit Sub
        End If

        If (rbtKyc.SelectedIndex < 0) Then
            lblMessage.Text = ""
            lblMessage.Text = "KYC OF DEATH cannot be empty!"
            FirstMsg = "javascript:alert('" + lblMessage.Text + "')"

            Exit Sub
        End If

        If (rbtBeneficiary.SelectedIndex < 0) Then
            lblMessage.Text = ""
            lblMessage.Text = "BENEFICIARY cannot be empty!"
            FirstMsg = "javascript:alert('" + lblMessage.Text + "')"

            Exit Sub
        End If


        DoSave(lblPolicy.Text, "", "", lblClaim.Text, Convert.ToInt16(rbtMCCD.SelectedValue), _
               Convert.ToInt16(rbtBurial.SelectedValue), Convert.ToInt16(rbtPolice.SelectedValue), _
               Convert.ToInt16(rbtDeath.SelectedValue), Convert.ToInt16(rbtKyc.SelectedValue), _
               Convert.ToInt16(rbtBeneficiary.SelectedValue))




    End Sub


    Sub DoSave(ByVal polyNum As String, ByVal fileNumber As String, ByVal qnumber As String, ByVal claimNumber As String, ByVal mccd As Int16, ByVal burial As Int16, ByVal police As Int16, ByVal death As Int16, ByVal kyc As Int16, ByVal benef As Int16)
        Dim sqlStr As String = "INSERT INTO TBGL_DV_DOC_CHECKLIST ( TBGL_DV_POLICY_NUMBER,  TBGL_DV_DOC_FILENUM, TBGL_DV_QUOTATION_NO, " _
        + "TBGL_DV_DOC_CLAIM_NUMBER, TBGL_DV_DOC_TRANS_DATE, TBGL_DV_DOC_MCCD, TBGL_DV_DOC_BURY_CERT, TBGL_DV_DOC_POLICE_REP, " _
        + "TBGL_DV_DOC_DEATH_CERT, TBGL_DV_DOC_BENEF_KYC, TBGL_DV_DOC_BENEF_BENEF, TBGL_DV_DOC_FLAG, TBGL_DV_DOC_OPERID, TBGL_DV_DOC_KEYDATE ) " _
        + "VALUES (@TBGL_DV_POLICY_NUMBER, @TBGL_DV_DOC_FILENUM, @TBGL_DV_QUOTATION_NO, @TBGL_DV_DOC_CLAIM_NUMBER, @TBGL_DV_DOC_TRANS_DATE, " _
        + "@TBGL_DV_DOC_MCCD, @TBGL_DV_DOC_BURY_CERT, @TBGL_DV_DOC_POLICE_REP, @TBGL_DV_DOC_DEATH_CERT, @TBGL_DV_DOC_BENEF_KYC, " _
        + "@TBGL_DV_DOC_BENEF_BENEF, @TBGL_DV_DOC_FLAG, @TBGL_DV_DOC_OPERID, @TBGL_DV_DOC_KEYDATE)"

        Dim mystrConn As String = CType("Provider=SQLOLEDB;" + gnGET_CONN_STRING(), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = sqlStr
        cmd.CommandText = "SPGL_INS_DV_DOC_CHECKLIST"
        cmd.CommandType = CommandType.StoredProcedure


        Dim operatorId As String = CType(Session("MyUserIDX"), String)

        cmd.Parameters.AddWithValue("@TBGL_DV_POLICY_NUMBER", polyNum)
        cmd.Parameters.AddWithValue("@TBGL_DV_DOC_FILENUM", fileNumber)
        cmd.Parameters.AddWithValue("@TBGL_DV_QUOTATION_NO", qnumber)
        cmd.Parameters.AddWithValue("@TBGL_DV_DOC_CLAIM_NUMBER", claimNumber)
        cmd.Parameters.AddWithValue("@TBGL_DV_DOC_TRANS_DATE", Date.Today)
        cmd.Parameters.AddWithValue("@TBGL_DV_DOC_MCCD", mccd)
        cmd.Parameters.AddWithValue("@TBGL_DV_DOC_BURY_CERT", burial)
        cmd.Parameters.AddWithValue("@TBGL_DV_DOC_POLICE_REP", police)
        cmd.Parameters.AddWithValue("@TBGL_DV_DOC_DEATH_CERT", death)
        cmd.Parameters.AddWithValue("@TBGL_DV_DOC_BENEF_KYC", kyc)
        cmd.Parameters.AddWithValue("@TBGL_DV_DOC_BENEF_BENEF", benef)
        cmd.Parameters.AddWithValue("@TBGL_DV_DOC_FLAG", "A")
        cmd.Parameters.AddWithValue("@TBGL_DV_DOC_OPERID", operatorId)
        cmd.Parameters.AddWithValue("@TBGL_DV_DOC_KEYDATE", Date.Now)



        Try
            conn.Open()
            Dim saved As Int16 = CType(cmd.ExecuteScalar(), Short)

            cmdPrint_ASP.Enabled = True

            conn.Close()
            lblMessage.Text = ""
            lblMessage.Text = "Entry successful, !!! click print button to print voucher !!!"
            FirstMsg = "javascript:confirm('" + lblMessage.Text + "')"

            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "return confirm(Are you sure!)", True)
            'Dim confirmValue As String = Request.Form("confirm_value")
            'If confirmValue = "Yes" Then
            '    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('You clicked YES!')", True)
            'Else
            '    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('You clicked NO!')", True)
            'End If


        Catch ex As Exception
            cmdPrint_ASP.Enabled = False
            lblMessage.Text = ""
            lblMessage.Text = "Entry failed!"
            FirstMsg = "javascript:alert('" + lblMessage.Text + "')"
        End Try



    End Sub


    Protected Sub cmdPrint_ASP_Click(sender As Object, e As EventArgs) Handles cmdPrint_ASP.Click
        'blnStatus = Get_Grp_ProposalNo(Trim(Me.lblPolicy.Text))

        'If blnStatus = False Then
        '    lblMsg.Text = "Invalid Policy number, POLICY NUMBER DOES NOT EXIST!"
        '    FirstMsg = "javascript:alert('" + lblMsg.Text + "')"
        '    Exit Sub
        'End If

        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        rParams(0) = "rptClmDischargeVouch"
        rParams(1) = "pClaimNo="
        rParams(2) = lblClaim.Text + "&"
        rParams(3) = "pPolicyNo="
        rParams(4) = lblPolicy.Text + "&"
        rParams(5) = "pMemStaffNo="
        rParams(6) = lblMemNum.Text + "&"
        rParams(7) = url


        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
    End Sub
End Class
