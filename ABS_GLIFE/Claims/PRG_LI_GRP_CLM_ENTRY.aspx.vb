Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class Claims_PRG_LI_GRP_CLM_ENTRY
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    'Protected BufferStr As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strF_ID As String
    Protected strQ_ID As String
    Protected strP_ID As String

    Protected strP_TYPE As String
    Protected strP_DESC As String

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strTmp_Value As String = ""

    Dim myarrData() As String

    Dim strErrMsg As String

    Dim basicLc As Decimal
    Dim basicFc As Decimal
    Dim addLc As Decimal
    Dim addFc As Decimal
    Dim newDateToDb As Date

    Shared _rtnMessage As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'load loss type into combobox
        If Not IsPostBack Then
            'DdnLossType.Items.Add("Select")
            ' DdnLossType.SelectedItem.Text="Select"
            LoadLossTypeCmb()
        End If

        'LoadProductsDescCmb()

        strTable = "TBIL_CLAIM_REPTED"

        Try
            strP_TYPE = CType(Request.QueryString("optid"), String)
            strP_DESC = CType(Request.QueryString("optd"), String)
        Catch ex As Exception
            strP_TYPE = "ERR_TYPE"
            strP_DESC = "ERR_DESC"
        End Try

        STRPAGE_TITLE = "Master Codes Setup - " & strP_DESC

        If Trim(strP_TYPE) = "ERR_TYPE" Or Trim(strP_TYPE) = "" Then
            strP_TYPE = ""
        End If

        strP_ID = "L01"

        If Me.txtAction.Text = "Save" Then
            'Call DoSave()
            'Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete" Then
            'Call DoDelete()
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete_Item" Then
            'Call DoDelItem()
            Me.txtAction.Text = ""
        End If

    End Sub

    Protected Sub chkClaimNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkClaimNum.CheckedChanged

        If Me.chkClaimNum.Checked Then
            txtClaimsNo.Enabled = True
            cmdClaimNoGet.Enabled = True

            txtAction.Text = "Save"
        Else
            txtClaimsNo.Enabled = False
            cmdClaimNoGet.Enabled = False

            txtAction.Text = ""
        End If
    End Sub

    Protected Sub chkPolyNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPolyNum.CheckedChanged
        If chkPolyNum.Checked Then
            txtPolicyNumber.Enabled = True
            cmdPolyNoGet.Enabled = True

            txtAction.Text = "New"
        Else
            txtPolicyNumber.Enabled = False
            cmdPolyNoGet.Enabled = False

            txtAction.Text = ""
        End If
    End Sub

    Public Function GetAllLossTypeCode() As DataSet

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GetAllLossTypeCode"
        cmd.CommandType = CommandType.StoredProcedure

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

    Public Function GetAllProductCodeList() As DataSet

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GetAllProductList"
        cmd.CommandType = CommandType.StoredProcedure

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


    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            'Call gnProc_Populate_Box("PRO_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
            Call gnProc_Populate_Box("GL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If

    End Sub
    Sub LoadLossTypeCmb()

        Dim ds As DataSet = GetAllLossTypeCode()
        Dim dt As DataTable = ds.Tables(0)
        Dim dr As DataRow = dt.NewRow()

        dr("TBIL_COD_SHORT_DESC") = "-- Selecct --"
        dr("TBIL_COD_ITEM") = ""
        dt.Rows.InsertAt(dr, 0)

        DdnLossType.DataSource = dt
        DdnLossType.DataTextField = "TBIL_COD_SHORT_DESC"
        DdnLossType.DataValueField = "TBIL_COD_ITEM"
        DdnLossType.DataBind()

    End Sub

    Sub ClaerAllFields()
        txtPolicyNumber.Text = ""
        txtClaimsNo.Text = ""
        txtUWY.Text = ""
        txtProductCode.Text = ""
        txtPolicyStartDate.Text = ""
        txtPolicyEndDate.Text = ""
        txtNotificationDate.Text = ""
        txtClaimsEffectiveDate.Text = ""
        txtBasicSumClaimsLC.Text = ""
        txtBasicSumClaimsFC.Text = ""
        txtAdditionalSumClaimsLC.Text = ""
        txtAdditionalSumClaimsFC.Text = ""
        txtAssuredAge.Text = ""
        DdnLossType.SelectedIndex = 0
        DdnClaimType.SelectedIndex = 0
        DdnSysModule.SelectedIndex = 0
        txtProductDec.Text = ""

    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        'clear fields
        ClaerAllFields()
        Try
            If cboSearch.SelectedIndex = -1 Or cboSearch.SelectedIndex = 0 Or cboSearch.SelectedItem.Value = "" Or cboSearch.SelectedItem.Value = "*" Then

            Else
                Dim cboValue As String = cboSearch.SelectedItem.Value
                strStatus = GetPolicyDetailsByNumber(cboValue.Trim())
            End If
        Catch ex As Exception
            lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub

    Private Function GetPolicyDetailsByNumber(ByVal policyNumber As String) As String

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SELECT * FROM TBIL_GRP_POLICY_DET " & _
                          "LEFT OUTER JOIN TBIL_GRP_POLICY_PREM_INFO ON TBIL_GRP_POLICY_DET.TBIL_POLY_POLICY_NO = TBIL_GRP_POLICY_PREM_INFO.TBIL_POL_PRM_POLY_NO " & _
                          "LEFT OUTER JOIN TBIL_PRODUCT_DETL ON TBIL_PRODUCT_DETL.TBIL_PRDCT_DTL_CODE = TBIL_GRP_POLICY_PREM_INFO.TBIL_POL_PRM_PRDCT_CD " & _
                          "where (TBIL_POLY_POLICY_NO='" & policyNumber & "')"
        cmd.CommandType = CommandType.Text
        ' cmd.Parameters.AddWithValue("@TBIL_POLY_POLICY_NO", policyNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            If (objOledr.Read()) Then
                strErrMsg = "true"

                txtPolicyNumber.Text = RTrim(CType(objOledr("TBIL_POLY_POLICY_NO") & vbNullString, String))
                txtUWY.Text = CType(objOledr("TBIL_POLY_UNDW_YR") & vbNullString, String)
                txtProductCode.Text = CType(objOledr("TBIL_POLY_PRDCT_CD") & vbNullString, String)
                'txtProductCode0.Text = CType(objOledr("TBIL_PRDCT_DTL_DESC") & vbNullString, String)

                If IsDate(objOledr("TBIL_POL_PRM_FROM")) Then
                    txtPolicyStartDate.Text = Format(CType(objOledr("TBIL_POL_PRM_FROM"), DateTime), "dd/MM/yyyy")
                End If
                If IsDate(objOledr("TBIL_POL_PRM_TO")) Then
                    txtPolicyEndDate.Text = Format(CType(objOledr("TBIL_POL_PRM_TO"), DateTime), "dd/MM/yyyy")
                End If

                'If IsDate(objOledr("TBIL_CLM_RPTD_NOTIF_DT")) Then
                '    txtNotificationDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_NOTIF_DT"), DateTime), "dd/MM/yyyy")
                'End If

                txtBasicSumClaimsLC.Text = Format(CType(objOledr("TBIL_POL_PRM_ANN_CONTRIB_LC"), Decimal), "N2")
                txtBasicSumClaimsFC.Text = Format(CType(objOledr("TBIL_POL_PRM_ANN_CONTRIB_FC"), Decimal), "N2")
                txtAdditionalSumClaimsLC.Text = Format(CType(objOledr("TBIL_POL_PRM_MTH_CONTRIB_LC"), Decimal), "N2")
                txtAdditionalSumClaimsFC.Text = Format(CType(objOledr("TBIL_POL_PRM_MTH_CONTRIB_FC"), Decimal), "N2")
                txtAssuredAge.Text = (objOledr("TBIL_POLY_ASSRD_AGE").ToString)


                If IsDate(objOledr("TBIL_POLICY_EFF_DT")) Then
                    txtClaimsEffectiveDate.Text = Format(CType(objOledr("TBIL_POLICY_EFF_DT"), DateTime), "dd/MM/yyyy")
                End If
                _rtnMessage = "Policy record retrieved!"
            Else
                _rtnMessage = "Unable to retrieve record. Invalid CLAIMS NUMBER!"
            End If
            conn.Close()
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try

        Return _rtnMessage

    End Function

    Private Function GetClaimsDetailsByNumber(ByVal claimNumber As String) As String
        'Dim rtnString As String
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_CLAIMSNUM_SEARCH_FRM_TABLE"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@tbil_clm_rptd_clm_no", claimNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            If (objOledr.Read()) Then
                strErrMsg = "true"

                txtPolicyNumber.Text = RTrim(CType(objOledr("TBIL_CLM_RPTD_POLY_NO") & vbNullString, String))
                txtUWY.Text = CType(objOledr("TBIL_CLM_RPTD_UNDW_YR") & vbNullString, String)
                txtProductCode.Text = CType(objOledr("TBIL_CLM_RPTD_PRDCT_CD") & vbNullString, String)
                'txtProductCode0.Text = CType(objOledr("TBIL_PRDCT_DTL_DESC") & vbNullString, String)


                If IsDate(objOledr("TBIL_CLM_RPTD_POLY_FROM_DT")) Then
                    txtPolicyStartDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_POLY_FROM_DT"), DateTime), "dd/MM/yyyy")
                End If
                If IsDate(objOledr("TBIL_CLM_RPTD_POLY_TO_DT")) Then
                    txtPolicyEndDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_POLY_TO_DT"), DateTime), "dd/MM/yyyy")
                End If

                If IsDate(objOledr("TBIL_CLM_RPTD_NOTIF_DT")) Then
                    txtNotificationDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_NOTIF_DT"), DateTime), "dd/MM/yyyy")
                End If
                If IsDate(objOledr("TBIL_CLM_RPTD_LOSS_DT")) Then
                    txtClaimsEffectiveDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_LOSS_DT"), DateTime), "dd/MM/yyyy")
                End If

                txtBasicSumClaimsLC.Text = Format(CType(objOledr("TBIL_CLM_RPTD_BASIC_LOSS_AMT_LC"), Decimal), "N2")
                txtBasicSumClaimsFC.Text = Format(CType(objOledr("TBIL_CLM_RPTD_BASIC_LOSS_AMT_FC"), Decimal), "N2")
                txtAdditionalSumClaimsLC.Text = Format(CType(objOledr("TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_LC"), Decimal), "N2")
                txtAdditionalSumClaimsFC.Text = Format(CType(objOledr("TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_FC"), Decimal), "N2")

                txtAssuredAge.Text = CType(Convert.ToInt16(objOledr("TBIL_CLM_RPTD_ASSRD_AGE").ToString), String)
                DdnClaimType.SelectedValue = CType(objOledr("TBIL_CLM_RPTD_CLM_TYPE") & vbNullString, String)
                DdnSysModule.SelectedValue = CType(objOledr("TBIL_CLM_RPTD_MDLE") & vbNullString, String)
                DdnLossType.SelectedValue = CType(objOledr("TBIL_CLM_RPTD_LOSS_TYPE") & vbNullString, String)
                txtProductDec.Text = CType(objOledr("TBIL_CLM_RPTD_DESC") & vbNullString, String)

                _rtnMessage = "Claims record retrieved!"

            Else
                _rtnMessage = "Unable to retrieve record. Invalid POLICY NUMBER!"
            End If
            conn.Close()
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try

        Return _rtnMessage
    End Function

    Protected Sub cmdPolyNoGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPolyNoGet.Click
        If txtPolicyNumber.Text <> "" Then
            ClearFormControls()
            lblMsg.Text = GetPolicyDetailsByNumber(txtPolicyNumber.Text.Trim())
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        Else
            lblMsg.Text = "Policy number field cannot be empty!"
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        End If
    End Sub

    Sub ClearFormControls()
        txtUWY.Text = ""
        txtProductCode.Text = ""
        txtPolicyStartDate.Text = ""
        txtPolicyEndDate.Text = ""
        txtNotificationDate.Text = ""
        txtClaimsEffectiveDate.Text = ""
        txtBasicSumClaimsFC.Text = ""
        txtBasicSumClaimsLC.Text = ""
        txtAdditionalSumClaimsLC.Text = ""
        txtAdditionalSumClaimsFC.Text = ""
        txtAssuredAge.Text = ""
        DdnSysModule.SelectedIndex = 0
        DdnClaimType.SelectedIndex = 0
        DdnLossType.SelectedIndex = 0
        txtProductDec.Text = ""

    End Sub

    Protected Sub cmdClaimNoGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClaimNoGet.Click
        If txtClaimsNo.Text <> "" Then
            ClearFormControls()
            lblMsg.Text = GetClaimsDetailsByNumber(txtClaimsNo.Text.Trim())
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        Else
            lblMsg.Text = "Claims number field cannot be empty!"
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        End If
    End Sub

    Private Function AddNewClaimsRequest(ByVal systemModule As String, ByVal polNumber As String, ByVal claimNo As String, ByVal uwy As String, _
                       ByVal productCode As String, ByVal lossType As String, ByVal polStartDate As DateTime, _
                       ByVal polEndDate As DateTime, ByVal notificationDate As DateTime, ByVal claimEffectiveDate As DateTime, ByVal basicSumClc As Decimal, _
                       ByVal basicSumCfc As Decimal, ByVal addSumClc As Decimal, ByVal addSumCfc As Decimal, _
                       ByVal claimDescription As String, ByVal assuredAge As Int16, ByVal lossType2 As String, ByVal flag As String, ByVal dDate As DateTime, ByVal userId As String) As String

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_INS_CLAIMSREQUEST_"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_MDLE", systemModule)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_POLY_NO", polNumber)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLM_NO", claimNo)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_UNDW_YR", uwy)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PRDCT_CD", productCode)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLM_TYPE", lossType)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_POLY_FROM_DT", polStartDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_POLY_TO_DT", polEndDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_LOSS_DT", claimEffectiveDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_NOTIF_DT", notificationDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_BASIC_LOSS_AMT_LC", basicSumClc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_BASIC_LOSS_AMT_FC", basicSumCfc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_LC", addSumClc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_FC", addSumCfc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_DESC", claimDescription)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_ASSRD_AGE", assuredAge)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_LOSS_TYPE", lossType2)

        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_FLAG", flag)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_KEYDTE", dDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_OPERID", userId)

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd

            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()

            Dim dt As DataTable = ds.Tables(0)
            For Each dr As DataRow In dt.Rows
                Dim msg = dr("Msg").ToString()
                If msg = 1 Then
                    '_rtnMessage = "Entry Successful, with CLAIM NUMBER: " + claimNo + " generated!"
                    _rtnMessage = "Entry Successful!"
                Else
                    _rtnMessage = "Entry failed, record already exist!"
                End If
            Next
        Catch ex As Exception
            _rtnMessage = "Entry failed! " + ex.Message.ToString()
        End Try


        Return _rtnMessage

    End Function

    Private Function ChangeClaims(ByVal systemModule As String, ByVal polNumber As String, ByVal claimNo As String, ByVal uwy As String, _
                     ByVal productCode As String, ByVal claimsType As String, ByVal polStartDate As DateTime, _
                     ByVal polEndDate As DateTime, ByVal notificationDate As DateTime, ByVal claimEffectiveDate As DateTime, ByVal basicSumClc As Decimal, _
                     ByVal basicSumCfc As Decimal, ByVal addSumClc As Decimal, ByVal addSumCfc As Decimal, _
                     ByVal claimDescription As String, ByVal assuredAge As Int16, ByVal lossType2 As String, ByVal flag As String, ByVal dDate As DateTime, ByVal userId As String) As String

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_UPDT_CLAIMSREQUEST_"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_MDLE", systemModule)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_POLY_NO", polNumber)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLM_NO", claimNo)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_UNDW_YR", uwy)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PRDCT_CD", productCode)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLM_TYPE", claimsType)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_POLY_FROM_DT", polStartDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_POLY_TO_DT", polEndDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_LOSS_DT", claimEffectiveDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_NOTIF_DT", notificationDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_BASIC_LOSS_AMT_LC", basicSumClc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_BASIC_LOSS_AMT_FC", basicSumCfc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_LC", addSumClc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_FC", addSumCfc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_DESC", claimDescription)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_ASSRD_AGE", assuredAge)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_LOSS_TYPE", lossType2)

        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_FLAG", flag)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_KEYDTE", dDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_OPERID", userId)

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()

            adapter.SelectCommand = cmd

            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()
            'Return ds.GetXml()

            Dim dt As DataTable = ds.Tables(0)
            For Each dr As DataRow In dt.Rows
                Dim msg = dr("Msg").ToString()
                If msg = 1 Then
                    _rtnMessage = "Update successful!"
                ElseIf msg = 0 Then
                    _rtnMessage = "Entry successful!"
                Else
                    _rtnMessage = "Record update failed!"
                End If
            Next

        Catch ex As Exception
            _rtnMessage = "Entry failed! " + ex.Message.ToString()
        End Try


        Return _rtnMessage

    End Function

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Dim str() As String

        'Checking fields for empty values
        If txtPolicyNumber.Text = "" Then
            lblMsg.Text = ""
        End If

        If txtNotificationDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtNotificationDate")
            str = MOD_GEN.DoDate_Process(txtNotificationDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Notification date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtNotificationDate.Focus()
                Exit Sub

            Else
                txtNotificationDate.Text = str(2).ToString()
            End If
        Else
            lblMsg.Text = "Notification Date field is required!"
            FirstMsg = lblMsg.Text
            txtNotificationDate.Focus()
            Exit Sub
        End If

        If txtClaimsEffectiveDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtClaimsEffectiveDate")
            str = MOD_GEN.DoDate_Process(txtClaimsEffectiveDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Claims Effective Date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtClaimsEffectiveDate.Focus()
                Exit Sub

            Else
                txtClaimsEffectiveDate.Text = str(2).ToString()
            End If
        Else
            lblMsg.Text = "Claims Effective Date field is required!"
            FirstMsg = lblMsg.Text
            txtClaimsEffectiveDate.Focus()
            Exit Sub
        End If

        If txtPolicyStartDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtPolicyStartDate")
            str = MOD_GEN.DoDate_Process(txtPolicyStartDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Policy Start Date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtPolicyStartDate.Focus()
                Exit Sub

            Else
                txtPolicyStartDate.Text = str(2).ToString()
            End If
        Else
            lblMsg.Text = "Policy Start Date field is required!"
            FirstMsg = lblMsg.Text
            txtPolicyStartDate.Focus()
            Exit Sub
        End If

        If txtPolicyEndDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtPolicyEndDate")
            str = MOD_GEN.DoDate_Process(txtPolicyEndDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Policy End Date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtPolicyEndDate.Focus()
                Exit Sub

            Else
                txtPolicyEndDate.Text = str(2).ToString()
            End If
        Else
            lblMsg.Text = "Policy End Date field is required!"
            FirstMsg = lblMsg.Text
            txtPolicyEndDate.Focus()
            Exit Sub
        End If

        'end date validation

        If txtBasicSumClaimsLC.Text = "" Then
            lblMsg.Text = "Basic Sum Claimed LC field is required!"
            txtBasicSumClaimsLC.Focus()
            Exit Sub
        Else
            basicLc = Convert.ToDecimal((txtBasicSumClaimsLC.Text).Replace(",", ""))

        End If

        If txtBasicSumClaimsFC.Text = "" Then
            lblMsg.Text = "Basic Sum Claimed FC field is required!"
            txtBasicSumClaimsFC.Focus()
            Exit Sub
        Else
            basicFc = Convert.ToDecimal((txtBasicSumClaimsFC.Text).Replace(",", ""))
        End If

        If txtAdditionalSumClaimsLC.Text = "" Then
            lblMsg.Text = "Additional Sum Claimed LC field is required!"
            txtAdditionalSumClaimsLC.Focus()
            Exit Sub
        Else
            addLc = Convert.ToDecimal((txtAdditionalSumClaimsLC.Text).Replace(",", ""))

        End If

        If txtAdditionalSumClaimsFC.Text = "" Then
            lblMsg.Text = "Additional Sum Claimed FC field is required!"
            txtAdditionalSumClaimsFC.Focus()
            Exit Sub
        Else
            addFc = Convert.ToDecimal((txtAdditionalSumClaimsFC.Text).Replace(",", ""))

        End If

        If txtAssuredAge.Text = "" Then
            lblMsg.Text = "Assured Age field is required!"
            txtAssuredAge.Focus()
            Exit Sub
        End If

        If DdnSysModule.SelectedIndex = 0 Then
            lblMsg.Text = "System Module field is required!"
            DdnSysModule.Focus()
            Exit Sub
        End If

        If DdnClaimType.SelectedIndex = 0 Then
            lblMsg.Text = "Claims Type field is required!"
            DdnClaimType.Focus()
            Exit Sub
        End If

        If DdnLossType.SelectedIndex = 0 Then
            lblMsg.Text = "Loss Type field is required!"
            DdnLossType.Focus()
            Exit Sub
        End If


        If txtProductDec.Text = "" Then
            lblMsg.Text = "Product Description field is required!"
            txtProductDec.Focus()
            Exit Sub
        End If

        Dim newNotifDate As Date = Convert.ToDateTime(DoConvertToDbDateFormat(txtNotificationDate.Text))
        Dim newClaimsEffDate As Date = Convert.ToDateTime(DoConvertToDbDateFormat(txtClaimsEffectiveDate.Text))



        If newNotifDate < Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyStartDate.Text)) _
        Or newNotifDate > Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text)) Then
            Dim errMsg = "Notification date should be within policy start and end date!"
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            FirstMsg = errMsg
            txtPolicyEndDate.Focus()
            Exit Sub
        End If

        If newClaimsEffDate < Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyStartDate.Text)) _
       Or newClaimsEffDate > Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text)) Then
            Dim errMsg = "Claims Effective date should be within policy start and end date!"
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            FirstMsg = errMsg
            txtPolicyEndDate.Focus()
            Exit Sub
        End If

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

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        Dim obj_DT As New System.Data.DataTable
        Dim intC As Integer = 0

        If txtAction.Text = "New" Then
            Dim flag As String = "A"
            Dim dateAdded As DateTime = Now
            Dim operatorId As String = CType(Session("MyUserIDX"), String)




            lblMsg.Text = AddNewClaimsRequest(Convert.ToString(DdnSysModule.SelectedValue.ToString), _
                                          Convert.ToString(txtPolicyNumber.Text), Convert.ToString(txtClaimsNo.Text), _
                                          Convert.ToString(txtUWY.Text), txtProductCode.Text, DdnClaimType.SelectedValue, _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyStartDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyEndDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtClaimsEffectiveDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtNotificationDate.Text)), _
                                          Convert.ToDecimal(basicLc), Convert.ToDecimal(basicFc), _
                                          Convert.ToDecimal(addLc), Convert.ToDecimal(addFc), _
                                          Convert.ToString(txtProductDec.Text), Convert.ToInt16(txtAssuredAge.Text), _
                                          Convert.ToString(DdnLossType.SelectedValue), flag, dateAdded, operatorId)

            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"



            'Dim drNewRow As System.Data.DataRow
            'drNewRow = obj_DT.NewRow()
            'drNewRow("TBIL_POL_ADD_FILE_NO") = RTrim(Me.txtFileNum.Text)
            'drNewRow("TBIL_CLM_RPTD_MDLE") = DdnSysModule.SelectedValue.ToString
            'drNewRow("TBIL_CLM_RPTD_POLY_NO") = txtPolicyNumber.Text
            'drNewRow("TBIL_CLM_RPTD_CLM_NO") = txtClaimsNo.Text
            'drNewRow("TBIL_CLM_RPTD_UNDW_YR") = txtUWY.Text
            'drNewRow("TBIL_CLM_RPTD_PRDCT_CD") = txtProductCode.Text
            'drNewRow("TBIL_CLM_RPTD_CLM_TYPE") = DdnClaimType.SelectedValue
            'drNewRow("TBIL_CLM_RPTD_POLY_FROM_DT") = Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyStartDate.Text))
            'drNewRow("TBIL_CLM_RPTD_POLY_TO_DT") = Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyEndDate.Text))
            'drNewRow("TBIL_CLM_RPTD_NOTIF_DT") = Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtNotificationDate.Text))
            'drNewRow("TBIL_CLM_RPTD_LOSS_DT") = Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtClaimsEffectiveDate.Text))
            'drNewRow("TBIL_CLM_RPTD_BASIC_LOSS_AMT_LC") = Convert.ToDecimal(basicLc)
            'drNewRow("TBIL_CLM_RPTD_BASIC_LOSS_AMT_FC") = Convert.ToDecimal(basicFc)
            'drNewRow("TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_LC") = Convert.ToDecimal(addLc)


            'drNewRow("TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_FC") = Convert.ToDecimal(addFc)
            'drNewRow("TBIL_CLM_RPTD_DESC") = txtProductDec.Text
            'drNewRow("TBIL_CLM_RPTD_ASSRD_AGE") = Convert.ToInt16(txtAssuredAge.Text)
            'drNewRow("TBIL_CLM_RPTD_LOSS_TYPE") = Convert.ToString(DdnLossType.SelectedValue)
            'drNewRow("TBIL_QUO_FLAG") = flag
            'drNewRow("TBIL_QUO_OPERID") = operatorId
            'drNewRow("TBIL_QUO_KEYDTE") = dateAdded
            'obj_DT.Rows.Add(drNewRow)
            'obj_DT.AcceptChanges()
            'intC = objDA.Update(obj_DT)

            'drNewRow = Nothing

            Me.lblMsg.Text = "New Record Saved to Database Successfully."

        Else
            Dim flag As String = "C"
            Dim dateAdded As DateTime = Now
            Dim operatorId As String = CType(Session("MyUserIDX"), String)
            lblMsg.Text = ChangeClaims(Convert.ToString(DdnSysModule.SelectedValue.ToString), _
                                          Convert.ToString(txtPolicyNumber.Text), Convert.ToString(txtClaimsNo.Text), _
                                          Convert.ToString(txtUWY.Text), txtProductCode.Text, DdnClaimType.SelectedValue, _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyStartDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyEndDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtClaimsEffectiveDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtNotificationDate.Text)), _
                                          Convert.ToDecimal(basicLc), Convert.ToDecimal(basicFc), _
                                          Convert.ToDecimal(addLc), Convert.ToDecimal(addFc), _
                                          Convert.ToString(txtProductDec.Text), Convert.ToInt16(txtAssuredAge.Text), _
                                          Convert.ToString(DdnLossType.SelectedValue), flag, dateAdded, operatorId)

            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"


            'strSQL = ""
            'strSQL = "SELECT TOP 1 * FROM " & strTable
            'strSQL = strSQL & " WHERE TBIL_CLM_RPTD_POLY_NO = '" & RTrim(txtPolicyNumber.Text) & "' "
            ''strSQL = strSQL & " AND TBIL_CLM_RPTD_MDLE='G'"

            'objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
            'Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
            'm_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)
            'Try

            'objDA.Fill(obj_DT)

            '    If obj_DT.Rows.Count = 1 Then
            '        With obj_DT
            '            .Rows(0)("TBIL_CLM_RPTD_MDLE") = DdnSysModule.SelectedValue.ToString
            '            .Rows(0)("TBIL_CLM_RPTD_POLY_NO") = txtPolicyNumber.Text
            '            .Rows(0)("TBIL_CLM_RPTD_CLM_NO") = txtClaimsNo.Text
            '            .Rows(0)("TBIL_CLM_RPTD_UNDW_YR") = txtUWY.Text
            '            .Rows(0)("TBIL_CLM_RPTD_PRDCT_CD") = txtProductCode.Text
            '            .Rows(0)("TBIL_CLM_RPTD_CLM_TYPE") = DdnClaimType.SelectedValue
            '            .Rows(0)("TBIL_CLM_RPTD_POLY_FROM_DT") = Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyStartDate.Text))
            '            .Rows(0)("TBIL_CLM_RPTD_POLY_TO_DT") = Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyEndDate.Text))
            '            .Rows(0)("TBIL_CLM_RPTD_NOTIF_DT") = Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtNotificationDate.Text))
            '            .Rows(0)("TBIL_CLM_RPTD_LOSS_DT") = Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtClaimsEffectiveDate.Text))
            '            .Rows(0)("TBIL_CLM_RPTD_BASIC_LOSS_AMT_LC") = Convert.ToDecimal(basicLc)
            '            .Rows(0)("TBIL_CLM_RPTD_BASIC_LOSS_AMT_FC") = Convert.ToDecimal(basicFc)
            '            .Rows(0)("TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_LC") = Convert.ToDecimal(addLc)


            '            .Rows(0)("TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_FC") = Convert.ToDecimal(addFc)
            '            .Rows(0)("TBIL_CLM_RPTD_DESC") = txtProductDec.Text
            '            .Rows(0)("TBIL_CLM_RPTD_ASSRD_AGE") = Convert.ToInt16(txtAssuredAge.Text)
            '            .Rows(0)("TBIL_CLM_RPTD_LOSS_TYPE") = Convert.ToString(DdnLossType.SelectedValue)
            '            .Rows(0)("TBIL_QUO_FLAG") = flag
            '            .Rows(0)("TBIL_QUO_OPERID") = operatorId
            '            .Rows(0)("TBIL_QUO_KEYDTE") = dateAdded
            '        End With
            '        intC = objDA.Update(obj_DT)
            '        Me.lblMsg.Text = "Record Saved to Database Successfully."
            '        m_cbCommandBuilder.Dispose()
            '        m_cbCommandBuilder = Nothing
            '    End If

            'Catch ex As Exception
            '    Me.lblMsg.Text = ex.Message.ToString
            '    Exit Sub
            'End Try
        End If
        ClearFormControls()
        'obj_DT.Dispose()
        'obj_DT = Nothing

        'If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
        '    objDA.SelectCommand.Connection.Close()
        'End If
        'objDA.Dispose()
        'objDA = Nothing
        'If objOLEConn.State = ConnectionState.Open Then
        '    objOLEConn.Close()
        'End If
        'objOLEConn = Nothing
        'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"




    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        If txtAction.Text = "New" Then
            DdnLossType.SelectedIndex = 0
            DdnSysModule.SelectedIndex = 0
            DdnClaimType.SelectedIndex = 0
            txtPolicyNumber.Text = ""
            txtClaimsNo.Text = ""
            txtUWY.Text = ""
            txtProductCode.Text = ""
            'txtProductCode0.Text = ""
            txtPolicyStartDate.Text = ""
            txtPolicyEndDate.Text = ""
            txtClaimsEffectiveDate.Text = ""
            txtNotificationDate.Text = ""
            txtBasicSumClaimsFC.Text = ""
            txtBasicSumClaimsLC.Text = ""
            txtAdditionalSumClaimsLC.Text = ""
            txtAdditionalSumClaimsFC.Text = ""
            txtAssuredAge.Text = ""
            txtProductDec.Text = ""
        End If
    End Sub

    Protected Sub cmdDelete_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete_ASP.Click
        Dim str() As String

        'Checking fields for empty values
        If txtPolicyNumber.Text = "" Then
            lblMsg.Text = ""
        End If

        If txtNotificationDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtNotificationDate")
            str = MOD_GEN.DoDate_Process(txtNotificationDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Notification date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtNotificationDate.Focus()
                Exit Sub

            Else
                txtNotificationDate.Text = str(2).ToString()
            End If

        End If

        If txtClaimsEffectiveDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtClaimsEffectiveDate")
            str = MOD_GEN.DoDate_Process(txtClaimsEffectiveDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Claims Effective Date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtClaimsEffectiveDate.Focus()
                Exit Sub

            Else
                txtClaimsEffectiveDate.Text = str(2).ToString()
            End If

        End If

        If txtPolicyStartDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtPolicyStartDate")
            str = MOD_GEN.DoDate_Process(txtPolicyStartDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Policy Start Date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtPolicyStartDate.Focus()
                Exit Sub

            Else
                txtPolicyStartDate.Text = str(2).ToString()
            End If

        End If

        If txtPolicyEndDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtPolicyEndDate")
            str = MOD_GEN.DoDate_Process(txtPolicyEndDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Policy End Date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtPolicyEndDate.Focus()
                Exit Sub

            Else
                txtPolicyEndDate.Text = str(2).ToString()
            End If

        End If

        'end date validation

        If txtBasicSumClaimsLC.Text = "" Then
            lblMsg.Text = "Basic Sum Claimed LC field is required!"
            txtBasicSumClaimsLC.Focus()
            Exit Sub
        Else
            basicLc = Convert.ToDecimal((txtBasicSumClaimsLC.Text).Replace(",", ""))

        End If

        If txtBasicSumClaimsFC.Text = "" Then
            lblMsg.Text = "Basic Sum Claimed FC field is required!"
            txtBasicSumClaimsFC.Focus()
            Exit Sub
        Else
            basicFc = Convert.ToDecimal((txtBasicSumClaimsFC.Text).Replace(",", ""))
        End If

        If txtAdditionalSumClaimsLC.Text = "" Then
            lblMsg.Text = "Additional Sum Claimed LC field is required!"
            txtAdditionalSumClaimsLC.Focus()
            Exit Sub
        Else
            addLc = Convert.ToDecimal((txtAdditionalSumClaimsLC.Text).Replace(",", ""))

        End If

        If txtAdditionalSumClaimsFC.Text = "" Then
            lblMsg.Text = "Additional Sum Claimed FC field is required!"
            txtAdditionalSumClaimsFC.Focus()
            Exit Sub
        Else
            addFc = Convert.ToDecimal((txtAdditionalSumClaimsFC.Text).Replace(",", ""))

        End If

        If txtAssuredAge.Text = "" Then
            lblMsg.Text = "Assured Age field is required!"
            txtAssuredAge.Focus()
            Exit Sub
        End If

        If DdnSysModule.SelectedIndex = 0 Then
            lblMsg.Text = "System Module field is required!"
            DdnSysModule.Focus()
            Exit Sub
        End If

        If DdnClaimType.SelectedIndex = 0 Then
            lblMsg.Text = "Claims Type field is required!"
            DdnClaimType.Focus()
            Exit Sub
        End If

        If DdnLossType.SelectedIndex = 0 Then
            lblMsg.Text = "Loss Type field is required!"
            DdnLossType.Focus()
            Exit Sub
        End If


        If txtProductDec.Text = "" Then
            lblMsg.Text = "Product Description field is required!"
            txtProductDec.Focus()
            Exit Sub
        End If


        If txtAction.Text = "Delete" Then

            Dim flag As String = "D"
            Dim dateAdded As DateTime = Now
            Dim operatorId As String = CType(Session("MyUserIDX"), String)

            lblMsg.Text = ChangeClaims(Convert.ToString(DdnSysModule.SelectedValue.ToString), _
                                          Convert.ToString(txtPolicyNumber.Text), Convert.ToString(txtClaimsNo.Text), _
                                          Convert.ToString(txtUWY.Text), txtProductCode.Text, DdnLossType.SelectedValue, _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyStartDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyEndDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtClaimsEffectiveDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtNotificationDate.Text)), _
                                          Convert.ToDecimal(basicLc), Convert.ToDecimal(basicFc), _
                                          Convert.ToDecimal(addLc), Convert.ToDecimal(addFc), _
                                          Convert.ToString(txtProductDec.Text), Convert.ToInt16(txtAssuredAge.Text), _
                                          Convert.ToString(DdnLossType.SelectedValue), flag, dateAdded, operatorId)


        End If
    End Sub
    Public Function DoConvertToDbDateFormat(ByVal dateValue As String) As String
        Dim dDate = dateValue.Split(CType("/", Char))
        Dim newDate = dDate(2) + "-" + dDate(1) + "-" + dDate(0)
        Return newDate
    End Function
End Class
