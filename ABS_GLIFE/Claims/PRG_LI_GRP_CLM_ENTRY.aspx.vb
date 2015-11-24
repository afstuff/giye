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

    Dim TotTransAmt As Decimal = 0
    Dim TransAmt As Decimal = 0

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


    Private Sub Proc_DataBindGrid()
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            objOLEConn = Nothing
        End Try

        If txtPolicyNumber.Text <> "" Then
            strF_ID = txtPolicyNumber.Text
        End If

        strTableName = "TBIL_GRP_POLICY_MEMBERS"

        strSQL = ""
        strSQL = strSQL & "SELECT *"
        strSQL = strSQL & " FROM " & strTableName & " "
        strSQL = strSQL & " WHERE TBIL_POL_MEMB_POLY_NO = '" & RTrim(strF_ID) & "'"
        'strSQL = strSQL & " WHERE TBIL_POL_MEMB_FILE_NO = '" & RTrim(strF_ID) & "'"
        'strSQL = strSQL & " AND TBIL_POL_MEMB_PROP_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND TBIL_POL_MEMB_BATCH_NO = '" & RTrim(Me.txtBatch_Num.Text) & "'"
        strSQL = strSQL & " AND TBIL_POL_MEMB_MDLE IN('G')"
        strSQL = strSQL & " AND TBIL_POL_MEMB_FLAG NOT IN('D')" 'do not include deleted items
        strSQL = strSQL & " ORDER BY TBIL_POL_MEMB_FILE_NO, TBIL_POL_MEMB_BATCH_NO, TBIL_POL_MEMB_SNO"


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
        'Me.cmdDelItem_ASP.Enabled = False
        Dim P As Integer = 0
        Dim C As Integer = 0

        C = 0
        For P = 0 To Me.GridView1.Rows.Count - 1
            C = C + 1
        Next
        'Me.lblResult.Text = "Total Row: " & C.ToString

        'If C >= 1 Then
        '    Me.cmdDelItem_ASP.Enabled = True
        '    Me.cmdNext.Enabled = True
        '    Me.txtBatch_Num.Enabled = False
        'Else
        '    Me.cmdNext.Enabled = False
        '    Me.txtBatch_Num.Enabled = True
        'End If

    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging

        'Dim row As GridViewRow = GridView1.Rows(e.NewSelectedIndex)

        GridView1.PageIndex = e.NewPageIndex
        Call Proc_DataBindGrid()
        lblMsg.Text = "Page " & GridView1.PageIndex + 1 & " of " & Me.GridView1.PageCount

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lblPrice As Label = CType(e.Row.FindControl("lblTransAmt"), Label)
            TransAmt = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TBIL_POL_MEMB_PREM")))
            TotTransAmt = (TotTransAmt + TransAmt)

        End If
        If (e.Row.RowType = DataControlRowType.Footer) Then
            Dim lblTotal As Label = CType(e.Row.FindControl("lbltxtTotal"), Label)
            lblTotal.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N}", New Object() {TotTransAmt})
        End If

        'format fields
        Dim ea As GridViewRowEventArgs = CType(e, GridViewRowEventArgs)
        If (ea.Row.RowType = DataControlRowType.DataRow) Then
            Dim drv As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TBIL_POL_MEMB_PREM"))

            If Not Convert.IsDBNull(drv) Then
                Dim iParsedValue As Decimal = 0
                If Decimal.TryParse(drv.ToString, iParsedValue) Then
                    Dim cell As TableCell = ea.Row.Cells(8)
                    cell.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N}", New Object() {iParsedValue})
                End If
            End If
        End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        ' Get the currently selected row imports the SelectedRow property.
        Dim row As GridViewRow = GridView1.SelectedRow

        ' Display the required value from the selected row.
        Me.txtRecNo.Text = row.Cells(2).Text

        strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtPolicyNumber.Text, Val(RTrim(Me.txtRecNo.Text)))

        Dim lblPrice1 As Label = GridView1.FooterRow.FindControl("lbltxtTotal")
        txtPremPaidLC.Text = lblPrice1.Text
        txtPremPaidFC.Text = lblPrice1.Text

        txtPremPaidLC.Enabled = False
        txtPremPaidFC.Enabled = False
        txtBasicSumClaimsLC.Enabled = False
        txtBasicSumClaimsFC.Enabled = False
        txtAssuredAge.Enabled = False
        txtMemberName.Enabled = False


        lblMsg.Text = "You selected " & Me.txtPolicyNumber.Text & " / " & Me.txtRecNo.Text & "."


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

        strTable = strTableName = "TBIL_GRP_POLICY_MEMBERS"
        strSQL = "SELECT TOP 1 * FROM TBIL_GRP_POLICY_MEMBERS WHERE TBIL_POL_MEMB_REC_ID = '" + FVstrRecNo + "'"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.CommandType = CommandType.StoredProcedure
        objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = strREC_ID
        objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strErrMsg = "true"

            'Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_FILE_NO") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_REC_ID") & vbNullString, String))

            Me.txtAssuredAge.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_AGE") & vbNullString, String))
            Me.txtBasicSumClaimsLC.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_TOT_SA") & vbNullString, String))
            Me.txtBasicSumClaimsFC.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_TOT_SA") & vbNullString, String))
            txtMemberName.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_NAME") & vbNullString, String))

            'Me.txtData_Source_SW.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_FILE_UPLOAD_SW") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboData_Source, RTrim(Me.txtData_Source_SW.Text))

            'Select Case UCase(Trim(Me.txtData_Source_SW.Text))
            '    Case "M"
            '        'tr_file_upload.Visible = False
            '        Me.cmdFile_Upload.Enabled = False
            '    Case "U"
            '        'tr_file_upload.Visible = True
            '        Me.cmdFile_Upload.Enabled = False
            '    Case Else
            '        'tr_file_upload.Visible = False
            '        Me.cmdFile_Upload.Enabled = False
            'End Select

            'Me.txtFile_Upload.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_FILE_UPLOAD_NAME") & vbNullString, String))

            'Me.txtBatch_Num.Text = RTrim(objOLEDR("TBIL_POL_MEMB_BATCH_NO") & vbNullString)
            ''Me.txtBatch_Num.Enabled = False
            'Me.cboBatch_Num.Enabled = False

            'Me.txtMember_SN.Text = Val(RTrim(CType(objOLEDR("TBIL_POL_MEMB_SNO") & vbNullString, String)))

            'Me.txtGender.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_CAT") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboGender, RTrim(Me.txtGender.Text))

            'If IsDate(objOLEDR("TBIL_POL_MEMB_BDATE")) Then
            '    Me.txtMember_DOB.Text = Format(CType(objOLEDR("TBIL_POL_MEMB_BDATE"), DateTime), "dd/MM/yyyy")
            'End If
            'Me.txtDOB_ANB.Text = Val(objOLEDR("TBIL_POL_MEMB_AGE") & vbNullString)

            'If IsDate(objOLEDR("TBIL_POL_MEMB_FROM_DT")) Then
            '    Me.txtStart_Date.Text = Format(CType(objOLEDR("TBIL_POL_MEMB_FROM_DT"), DateTime), "dd/MM/yyyy")
            'End If
            'If IsDate(objOLEDR("TBIL_POL_MEMB_TO_DT")) Then
            '    Me.txtEnd_Date.Text = Format(CType(objOLEDR("TBIL_POL_MEMB_TO_DT"), DateTime), "dd/MM/yyyy")
            'End If

            'Me.txtPrem_Period_Yr.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_TENOR") & vbNullString, String))
            'Me.txtDesignation_Name.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_DESIG") & vbNullString, String))
            'Me.txtMember_Name.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_NAME") & vbNullString, String))

            'If Val(RTrim(CType(objOLEDR("TBIL_POL_MEMB_SA_FACTOR") & vbNullString, String))) <> 0 Then
            '    Me.txtPrem_SA_Factor.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_SA_FACTOR") & vbNullString, String))
            'End If

            'Me.txtTotal_Emolument.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_TOT_EMOLUMENT") & vbNullString, String))
            'Me.txtSum_Assured.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_TOT_SA") & vbNullString, String))

            'Me.txtMedical_YN.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_MEDICAL_YN") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboMedical_YN, RTrim(Me.txtMedical_YN.Text))

            'Call gnProc_DDL_Get(Me.cboPrem_Rate_Code, RTrim(Me.txtPrem_Rate_Code.Text))

            'Me.txtPrem_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_RATE") & vbNullString, String))
            'Me.txtPrem_Rate_Per.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_RATE_PER") & vbNullString, String))
            'Me.txtPrem_Amt.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_PREM") & vbNullString, String))
            'Me.txtPrem_Amt_Prorata.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_PRO_RATE_PREM") & vbNullString, String))
            'Me.txtLoad_amt.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_LOAD") & vbNullString, String))

            'Me.lblFileNum.Enabled = False
            ''Call DisableBox(Me.txtFileNum)
            ''Me.chkFileNum.Enabled = False
            'Me.txtFileNum.Enabled = False
            'Me.txtQuote_Num.Enabled = False
            'Me.txtPolNum.Enabled = False

            'Me.cmdNew_ASP.Enabled = True
            ''Me.cmdDelete_ASP.Enabled = True
            'Me.cmdNext.Enabled = True

            'If RTrim(CType(objOLEDR("TBIL_POLY_PROPSL_ACCPT_STATUS") & vbNullString, String)) = "A" Then
            '    Me.chkFileNum.Enabled = False
            '    Me.lblFileNum.Enabled = False
            '    Me.txtFileNum.Enabled = False
            '    Me.cmdFileNum.Enabled = False
            '    Me.cmdSave_ASP.Enabled = False
            '    Me.cmdDelete_ASP.Enabled = False
            'End If

            strOPT = "2"
            Me.lblMsg.Text = "Status: Data Modification"

        Else
            'Me.lblFileNum.Enabled = True
            'Call DisableBox(Me.txtFileNum)
            'Me.chkFileNum.Enabled = True
            'Me.chkFileNum.Checked = False
            'Me.txtFileNum.Enabled = True
            'Me.txtQuote_Num.Enabled = True
            'Me.txtPolNum.Enabled = True

            'Me.cmdDelete_ASP.Enabled = False
            'Me.cmdNext.Enabled = False

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
        'If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        'ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
        '    'Call gnProc_Populate_Box("PRO_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        '    Call gnProc_Populate_Box("GL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        'End If
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            cboSearch.Items.Clear()
            cboSearch.Items.Add("* Select Insured *")
            Dim dt As DataTable = GET_INSURED(txtSearch.Value.Trim()).Tables(0)
            cboSearch.DataSource = dt
            cboSearch.DataValueField = "TBIL_POLY_POLICY_NO"
            cboSearch.DataTextField = "MyFld_Text"
            cboSearch.DataBind()
        End If
    End Sub
    Sub LoadLossTypeCmb()

        Dim ds As DataSet = GetAllLossTypeCode()
        Dim dt As DataTable = ds.Tables(0)
        Dim dr As DataRow = dt.NewRow()

        dr("TBIL_COD_LONG_DESC") = "-- Selecct --"
        dr("TBIL_COD_ITEM") = ""
        dt.Rows.InsertAt(dr, 0)

        DdnLossType.DataSource = dt
        DdnLossType.DataTextField = "TBIL_COD_LONG_DESC"
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
        txtDateOfDeath.Text = ""
        txtBasicSumClaimsLC.Text = ""
        txtBasicSumClaimsFC.Text = ""
        txtPremPaidLC.Text = ""
        txtPremPaidFC.Text = ""
        txtAssuredAge.Text = ""
        DdnLossType.SelectedIndex = 0
        'DdnClaimType.SelectedIndex = 0
        DdnSysModule.SelectedIndex = 0
        txtClaimDec.Text = ""

    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        'clear fields
        ClaerAllFields()
        Try
            If cboSearch.SelectedIndex = -1 Or cboSearch.SelectedIndex = 0 Or cboSearch.SelectedItem.Value = "" Or cboSearch.SelectedItem.Value = "*" Then

            Else
                Dim cboValue As String = cboSearch.SelectedItem.Value
                strStatus = GetPolicyDetailsByNumber(cboValue.Trim())
                Proc_DataBindGrid()
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

                If cboSearch.SelectedIndex > 0 Then
                    Dim name As String = cboSearch.SelectedItem.Text.ToString
                    Dim nameArr As String() = name.Split("-")

                    lblGrpName.Text = "Name: " + nameArr(0)
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
        cmd.CommandText = "SPIL_GRP_CLAIMSNUM_SEARCH_FRM_TABLE"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@tbil_clm_rptd_clm_no", claimNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            If (objOledr.Read()) Then
                strErrMsg = "true"

                txtPolicyNumber.Text = RTrim(CType(objOledr("TBIL_GRP_CLM_RPTD_POLY_NO") & vbNullString, String))
                txtUWY.Text = CType(objOledr("TBIL_GRP_CLM_RPTD_UNDW_YR") & vbNullString, String)
                txtProductCode.Text = CType(objOledr("TBIL_GRP_CLM_RPTD_PRDCT_CD") & vbNullString, String)
                'txtProductCode0.Text = CType(objOledr("TBIL_PRDCT_DTL_DESC") & vbNullString, String)


                If IsDate(objOledr("TBIL_GRP_CLM_RPTD_POLY_FROM_DT")) Then
                    txtPolicyStartDate.Text = Format(CType(objOledr("TBIL_GRP_CLM_RPTD_POLY_FROM_DT"), DateTime), "dd/MM/yyyy")
                End If
                If IsDate(objOledr("TBIL_GRP_CLM_RPTD_POLY_TO_DT")) Then
                    txtPolicyEndDate.Text = Format(CType(objOledr("TBIL_GRP_CLM_RPTD_POLY_TO_DT"), DateTime), "dd/MM/yyyy")
                End If

                If IsDate(objOledr("TBIL_GRP_CLM_RPTD_NOTIF_DT")) Then
                    txtNotificationDate.Text = Format(CType(objOledr("TBIL_GRP_CLM_RPTD_NOTIF_DT"), DateTime), "dd/MM/yyyy")
                End If
                If IsDate(objOledr("TBIL_GRP_CLM_RPTD_DATEOFDEATH_DT")) Then
                    txtDateOfDeath.Text = Format(CType(objOledr("TBIL_GRP_CLM_RPTD_DATEOFDEATH_DT"), DateTime), "dd/MM/yyyy")
                End If

                txtBasicSumClaimsLC.Text = Format(CType(objOledr("TBIL_GRP_CLM_RPTD_BASIC_LOSS_AMT_LC"), Decimal), "N2")
                txtBasicSumClaimsFC.Text = Format(CType(objOledr("TBIL_GRP_CLM_RPTD_BASIC_LOSS_AMT_FC"), Decimal), "N2")
                txtPremPaidLC.Text = Format(CType(objOledr("TBIL_GRP_CLM_RPTD_PREMIUM_PAID_AMT_LC"), Decimal), "N2")
                txtPremPaidFC.Text = Format(CType(objOledr("TBIL_GRP_CLM_RPTD_PREMIUM_PAID_AMT_FC"), Decimal), "N2")
                txtPremiumLoadedLC.Text = Format(CType(objOledr("TBIL_GRP_CLM_RPTD_PREMIUM_LOADED_LC"), Decimal), "N2")
                txtPremiumLoadedFC.Text = Format(CType(objOledr("TBIL_GRP_CLM_RPTD_PREMIUM_LOADED_FC"), Decimal), "N2")
                txtMemberName.Text = CType(objOledr("TBIL_GRP_CLM_RPTD_MEMBERNAME") & vbNullString, String)

                txtAssuredAge.Text = CType(Convert.ToInt16(objOledr("TBIL_GRP_CLM_RPTD_ASSRD_AGE").ToString), String)
                DdnSysModule.SelectedValue = CType(objOledr("TBIL_GRP_CLM_RPTD_MDLE") & vbNullString, String)
                DdnLossType.SelectedValue = CType(objOledr("TBIL_GRP_CLM_RPTD_LOSS_TYPE") & vbNullString, String)
                txtClaimDec.Text = CType(objOledr("TBIL_GRP_CLM_RPTD_DESC") & vbNullString, String)
                txtRemark.Text = CType(objOledr("TBIL_GRP_CLM_RPTD_REMARK") & vbNullString, String)

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
        txtDateOfDeath.Text = ""
        txtBasicSumClaimsFC.Text = ""
        txtBasicSumClaimsLC.Text = ""
        txtPremPaidLC.Text = ""
        txtPremPaidFC.Text = ""
        txtAssuredAge.Text = ""
        DdnSysModule.SelectedIndex = 0
        'DdnClaimType.SelectedIndex = 0
        DdnLossType.SelectedIndex = 0
        txtClaimDec.Text = ""

    End Sub

    Protected Sub cmdClaimNoGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClaimNoGet.Click
        If txtClaimsNo.Text <> "" Then
            ClearFormControls()
            lblMsg.Text = GetClaimsDetailsByNumber(txtClaimsNo.Text.Trim())
            'FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        Else
            lblMsg.Text = "Claims number field cannot be empty!"
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        End If
    End Sub

    Private Function AddNewClaimsRequest(ByVal systemModule As String, ByVal polNumber As String, ByVal claimNo As String, ByVal uwy As String, _
                       ByVal productCode As String, ByVal lossType As String, ByVal polStartDate As DateTime, _
                       ByVal polEndDate As DateTime, ByVal notificationDate As DateTime, ByVal dateOfDeath As DateTime, ByVal basicSumClc As Decimal, _
                       ByVal basicSumCfc As Decimal, ByVal premiumPaidLc As Decimal, ByVal premiumPaidFc As Decimal, ByVal premiumLoadedLc As Decimal, ByVal premiumLoadedFc As Decimal, _
                       ByVal claimDescription As String, ByVal claimRemark As String, ByVal memberName As String, ByVal assuredAge As Int16, _
                       ByVal flag As String, ByVal dDate As DateTime, ByVal userId As String) As String

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GRP_INS_CLAIMSREQUEST_"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_MDLE", systemModule)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_POLY_NO", polNumber)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_CLM_NO", claimNo)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_UNDW_YR", uwy)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_PRDCT_CD", productCode)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_POLY_FROM_DT", Convert.ToDateTime(polStartDate))
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_POLY_TO_DT", Convert.ToDateTime(polEndDate))
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_NOTIF_DT", Convert.ToDateTime(notificationDate))
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_DATEOFDEATH_DT", Convert.ToDateTime(dateOfDeath))
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_BASIC_LOSS_AMT_LC", basicSumClc)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_BASIC_LOSS_AMT_FC", basicSumCfc)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_PREMIUM_PAID_AMT_LC", premiumPaidLc)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_PREMIUM_PAID_AMT_FC", premiumPaidFc)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_PREMIUM_LOADED_LC", premiumLoadedLc)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_PREMIUM_LOADED_FC", premiumLoadedFc)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_MEMBERNAME", memberName)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_ASSRD_AGE", Convert.ToInt16(assuredAge))
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_LOSS_TYPE", lossType)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_DESC", claimDescription)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_REMARK", claimRemark)

        'cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_FLAG", flag)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_KEYDTE", dDate)
        cmd.Parameters.AddWithValue("@TBIL_GRP_CLM_RPTD_OPERID", userId)

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

    Private Function ChangeClaims(ByVal systemModule As String, ByVal polNumber As String, ByVal uwy As String, _
                     ByVal productCode As String, ByVal claimsType As String, ByVal polStartDate As DateTime, _
                     ByVal polEndDate As DateTime, ByVal notificationDate As DateTime, ByVal claimEffectiveDate As DateTime, ByVal basicSumClc As Decimal, _
                     ByVal basicSumCfc As Decimal, ByVal addSumClc As Decimal, ByVal addSumCfc As Decimal, _
                     ByVal claimDescription As String, ByVal assuredAge As Int16, ByVal lossType2 As String, ByVal flag As String, ByVal dDate As DateTime, ByVal userId As String) As String

        'ByVal claimNo As String,
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_UPDT_CLAIMSREQUEST_"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_MDLE", systemModule)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_POLY_NO", polNumber)
        'cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLM_NO", claimNo)
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

        If txtClaimsNo.Text = "" Then
            lblMsg.Text = "Claim Number cannot be empty!"
            FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
            Exit Sub
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

        If txtDateOfDeath.Text <> "" Then
            Dim ctrlId As Control = FindControl("lblDateOfDeath")
            str = MOD_GEN.DoDate_Process(txtDateOfDeath.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Date Of Death, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtDateOfDeath.Focus()
                Exit Sub

            Else
                txtDateOfDeath.Text = str(2).ToString()
            End If
        Else
            'lblMsg.Text = "Date of death field is required!"
            'FirstMsg = lblMsg.Text
            'txtDateOfDeath.Focus()
            'Exit Sub
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

        If txtPremPaidLC.Text = "" Then
            lblMsg.Text = "Additional Sum Claimed LC field is required!"
            txtPremPaidLC.Focus()
            Exit Sub
        Else
            addLc = Convert.ToDecimal((txtPremPaidLC.Text).Replace(",", ""))

        End If

        If txtPremPaidFC.Text = "" Then
            lblMsg.Text = "Additional Sum Claimed FC field is required!"
            txtPremPaidFC.Focus()
            Exit Sub
        Else
            addFc = Convert.ToDecimal((txtPremPaidFC.Text).Replace(",", ""))

        End If

        If txtAssuredAge.Text = "" Then
            lblMsg.Text = "Assured Age field is required!"
            txtAssuredAge.Focus()
            Exit Sub
        End If

        If txtPremiumLoadedLC.Text = "" Then
            txtPremiumLoadedLC.Text = "0.00"
            txtPremiumLoadedFC.Text = "0.00"
        End If

        If txtPremiumLoadedFC.Text = "" Then
            txtPremiumLoadedLC.Text = "0.00"
            txtPremiumLoadedFC.Text = "0.00"
        End If

        'If DdnSysModule.SelectedIndex = 0 Then
        '    lblMsg.Text = "System Module field is required!"
        '    DdnSysModule.Focus()
        '    Exit Sub
        'End If

        'If DdnClaimType.SelectedIndex = 0 Then
        '    lblMsg.Text = "Claims Type field is required!"
        '    DdnClaimType.Focus()
        '    Exit Sub
        'End If

        If DdnLossType.SelectedIndex = 0 Then
            lblMsg.Text = "Loss Type field is required!"
            DdnLossType.Focus()
            Exit Sub
        End If


        If txtClaimDec.Text = "" Then
            lblMsg.Text = "Product Description field is required!"
            txtClaimDec.Focus()
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

        'If txtAction.Text = "New" Then
        Dim flag As String = "A"
        Dim dateAdded As DateTime = Now
        Dim operatorId As String = CType(Session("MyUserIDX"), String)


        Dim rtn As String = AddNewClaimsRequest( _
        Convert.ToString(DdnSysModule.SelectedItem.Value), Convert.ToString(txtPolicyNumber.Text), _
        Convert.ToString(txtClaimsNo.Text), Convert.ToString(txtUWY.Text), _
        Convert.ToString(txtProductCode.Text), Convert.ToString(DdnLossType.SelectedItem.Value), _
        Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyStartDate.Text)), _
        Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyEndDate.Text)), _
        Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtNotificationDate.Text)), _
        Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtDateOfDeath.Text)), _
        Convert.ToDecimal(txtBasicSumClaimsLC.Text), Convert.ToDecimal(txtBasicSumClaimsFC.Text), _
        Convert.ToDecimal(txtPremPaidLC.Text), Convert.ToDecimal(txtPremPaidFC.Text), Convert.ToDecimal(txtPremiumLoadedLC.Text), _
        Convert.ToDecimal(txtPremiumLoadedFC.Text), Convert.ToString(txtClaimDec.Text), Convert.ToString(txtRemark.Text), _
        Convert.ToString(txtMemberName.Text), Convert.ToInt16(txtAssuredAge.Text), flag, dateAdded, operatorId)

        If True Then
            rtn = "Entry Successful!"
            Me.lblMsg.Text = "New Record Saved to Database Successfully."
        Else
            Me.lblMsg.Text = "Record Update Successfully."
        End If

        FirstMsg = "javascript:alert('" + lblMsg.Text + "');"


        ClearFormControls()

    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        If txtAction.Text = "New" Then
            DdnLossType.SelectedIndex = 0
            DdnSysModule.SelectedIndex = 0
            'DdnClaimType.SelectedIndex = 0
            txtPolicyNumber.Text = ""
            txtClaimsNo.Text = ""
            txtUWY.Text = ""
            txtProductCode.Text = ""
            'txtProductCode0.Text = ""
            txtPolicyStartDate.Text = ""
            txtPolicyEndDate.Text = ""
            txtDateOfDeath.Text = ""
            txtNotificationDate.Text = ""
            txtBasicSumClaimsFC.Text = ""
            txtBasicSumClaimsLC.Text = ""
            txtPremPaidLC.Text = ""
            txtPremPaidFC.Text = ""
            txtAssuredAge.Text = ""
            txtClaimDec.Text = ""
        End If
    End Sub

    Protected Sub cmdDelete_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete_ASP.Click
        Dim str() As String

        'Checking fields for empty values
        If txtPolicyNumber.Text = "" Then
            lblMsg.Text = ""
        End If

        If txtPremiumLoadedLC.Text <> txtPremiumLoadedFC.Text Then
            lblMsg.Text = "Premium Loaded value not consistent!"
            FirstMsg = lblMsg.Text
            txtPremPaidLC.Focus()
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

        If txtDateOfDeath.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtDateOfDeath")
            str = MOD_GEN.DoDate_Process(txtDateOfDeath.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Claims Effective Date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtDateOfDeath.Focus()
                Exit Sub

            Else
                txtDateOfDeath.Text = str(2).ToString()
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

        If txtPremPaidLC.Text = "" Then
            lblMsg.Text = "Additional Sum Claimed LC field is required!"
            txtPremPaidLC.Focus()
            Exit Sub
        Else
            addLc = Convert.ToDecimal((txtPremPaidLC.Text).Replace(",", ""))

        End If

        If txtPremPaidFC.Text = "" Then
            lblMsg.Text = "Additional Sum Claimed FC field is required!"
            txtPremPaidFC.Focus()
            Exit Sub
        Else
            addFc = Convert.ToDecimal((txtPremPaidFC.Text).Replace(",", ""))

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

        'If DdnClaimType.SelectedIndex = 0 Then
        '    lblMsg.Text = "Claims Type field is required!"
        '    DdnClaimType.Focus()
        '    Exit Sub
        'End If

        If DdnLossType.SelectedIndex = 0 Then
            lblMsg.Text = "Loss Type field is required!"
            DdnLossType.Focus()
            Exit Sub
        End If


        If txtClaimDec.Text = "" Then
            lblMsg.Text = "Product Description field is required!"
            txtClaimDec.Focus()
            Exit Sub
        End If


        If txtAction.Text = "Delete" Then

            Dim flag As String = "D"
            Dim dateAdded As DateTime = Now
            Dim operatorId As String = CType(Session("MyUserIDX"), String)
            ' Convert.ToString(txtClaimsNo.Text),
            lblMsg.Text = ChangeClaims(Convert.ToString(DdnSysModule.SelectedValue.ToString), _
                                          Convert.ToString(txtPolicyNumber.Text), _
                                          Convert.ToString(txtUWY.Text), txtProductCode.Text, DdnLossType.SelectedValue, _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyStartDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyEndDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtDateOfDeath.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtNotificationDate.Text)), _
                                          Convert.ToDecimal(basicLc), Convert.ToDecimal(basicFc), _
                                          Convert.ToDecimal(addLc), Convert.ToDecimal(addFc), _
                                          Convert.ToString(txtClaimDec.Text), Convert.ToInt16(txtAssuredAge.Text), _
                                          Convert.ToString(DdnLossType.SelectedValue), flag, dateAdded, operatorId)


        End If
    End Sub
    Public Function DoConvertToDbDateFormat(ByVal dateValue As String) As String
        Dim dDate = dateValue.Split(CType("/", Char))
        Dim newDate = dDate(2) + "-" + dDate(1) + "-" + dDate(0)
        Return newDate
    End Function

    Protected Sub searchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchBtn.Click

        If txtPolicyNumber.Text <> "" And txtSvalue.Text <> "" Then
            DoFilter(txtPolicyNumber.Text, txtSvalue.Text, DdnFilter.SelectedIndex)
        End If

    End Sub

    Public Sub DoFilter(ByVal polyNumber As String, ByVal memberName As String, ByVal sType As Integer)
        'Dim rtnString As String
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GRP_CLAIMSMEMBER_SEARCH"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_POLY_NO", polyNumber)
        cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_NAME", memberName)
        cmd.Parameters.AddWithValue("@sType", sType)

        Try
            conn.Open()
            'Dim objOledr As OleDbDataReader
            'objOledr = cmd.ExecuteReader()

            GridView1.DataSource = cmd.ExecuteReader()
            GridView1.DataBind()

            conn.Close()
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try
    End Sub

    Protected Sub DdnFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdnFilter.SelectedIndexChanged

        If DdnFilter.SelectedIndex = 0 Then
            txtSvalue.Text = ""
        End If

    End Sub
End Class
