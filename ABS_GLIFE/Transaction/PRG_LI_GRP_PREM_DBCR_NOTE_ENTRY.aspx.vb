Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Partial Class Transaction_PRG_LI_GRP_PREM_DBCR_NOTE_ENTRY
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    Protected PageTitle As String

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

    Dim strProcDate As String = ""
    Dim strSerialNum As String = ""

    Dim GenStart_Date As Date
    Dim GenEnd_Date As Date
    Dim dteTrans As Date = Now
    Dim dteRef As Date = Now
    Dim dteApproved As Date = Now

    Dim myarrData() As String

    Dim strErrMsg As String

    Protected _strTranType As String = String.Empty
    Protected _strSecNum As String = String.Empty
    Protected _strCNCode As String = String.Empty
    Protected _strBusType As String = String.Empty
    Protected _strSectors As String = String.Empty
    Protected _strTranDate As String = String.Empty
    Protected _strBranchCode As String = String.Empty
    Protected _strPolicyNum As String = String.Empty
    Protected _strBatchNo As String = String.Empty
    Protected _strSA As String = String.Empty
    Protected _strGrossPrem As String = String.Empty
    Protected _strTransDesc As String = String.Empty
    Protected _strUsedDays As String = String.Empty
    Protected _strRiskDays As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strTableName = "TBIL_GRP_POLICY_DNOTE_DETAILS"

        PageLinks = ""
        PageLinks = PageLinks & "<a href='javascript:window.close();' runat='server'>Close...</a>"

        '        FirstMsg = "vbscript:call DoPopReport('" & ReportPage & "', 800, 600)"

        Try
            myTType = Request.QueryString("TTYPE")

        Catch ex As Exception
            myTType = "ACDI"
        Finally

        End Try

        PageTitle = ""
        'Select Case RTrim(myTType)
        '    Case "B"
        '        PageTitle = "ORC Data Entry"
        '        'PageLinks = Request.ApplicationPath & "/Underwriting/UNP200.aspx"
        '    Case "I"
        '        PageTitle = "Facultative Inward Entry"
        '        'PageLinks = Request.ApplicationPath & "/Reinsurance/REI200.aspx"
        '    Case "O"
        '        PageTitle = "Facultative Outward Entry"
        '        'PageLinks = Request.ApplicationPath & "/Reinsurance/REI200.aspx"
        '    Case Else
        '        PageTitle = "Debit/Credit Note Entry"
        '        'PageLinks = Request.ApplicationPath & "/Underwriting/UNP200.aspx"
        'End Select


        If Not (Page.IsPostBack) Then
            Call gnProc_Populate_Box("SECTOR_CODE", "001", Me.cboSecName)

            Call gnProc_Populate_Box("IL_CODE_LIST", "003", Me.cboBranchName)

            Me.cboTransType.Items.Clear()
            Call gnPopulate_DropDownList("UND_RECORD_TYPE", Me.cboTransType, "", "", "(Select item)", "*")

            Me.cboTransCode.Items.Clear()
            Call gnPopulate_DropDownList("D-NOTE_C-NOTE", Me.cboTransCode, "", "", "(Select item)", "*")

            'Populate box with business type
            Me.cboBusType.Items.Clear()
            Call gnPopulate_DropDownList("UND_BUS_TYPE", Me.cboBusType, "", "", "(Select item)", "*")

            Try
                _strTranType = Request.QueryString("transtype")
                _strSecNum = Request.QueryString("secnum")
                _strCNCode = Request.QueryString("cn")
                _strBusType = Request.QueryString("bustype")
                _strSectors = Request.QueryString("sectors")
                _strTranDate = Request.QueryString("billdate")
                _strBranchCode = Request.QueryString("branch")
                _strPolicyNum = Request.QueryString("policyno")
                _strBatchNo = Request.QueryString("batchno")
                _strSA = Request.QueryString("sa")
                _strGrossPrem = Request.QueryString("gprem")
                _strTransDesc = Request.QueryString("transdesc")
                _strUsedDays = Request.QueryString("daysused")
                _strRiskDays = Request.QueryString("riskdays")

                txtTransDate.Text = Trim(_strTranDate)
                txtPolNum.Text = Trim(_strPolicyNum)
                txtSumIns.Text = Trim(_strSA)
                txtSumIns.Text = Math.Round(Convert.ToDecimal(txtSumIns.Text), 2)
                txtTrans_Full_SI.Text = txtSumIns.Text

                txtGrsPrem.Text = Math.Round(Convert.ToDecimal(Trim(_strGrossPrem)), 2)
                txtTrans_Full_Prem.Text = txtGrsPrem.Text
                txtTransAmt.Text = txtGrsPrem.Text
                txtMemberBatchNum.Text = Trim(_strBatchNo)
                txtTransDescr1.Text = Trim(_strTransDesc)
                txtProRataNDay.Text = Convert.ToInt16(_strRiskDays) - Convert.ToInt16(_strUsedDays)
                txtProRataRDay.Text = _strRiskDays.ToString

                GetPolicyDetails()
                Call Proc_DDL_Get(Me.cboTransType, RTrim(_strTranType))
                Call Proc_DDL_Get(Me.cboBusType, RTrim(_strBusType))
                Call Proc_DDL_Get(Me.cboTransCode, RTrim(_strCNCode))
                Call Proc_DDL_Get(Me.cboSecName, RTrim(_strSecNum))
                Call Proc_DDL_Get(Me.cboBranchName, RTrim(_strBranchCode))
                Call Proc_DDL_Get(Me.cboMemberBatchNum, RTrim(_strBatchNo))

                Session("_strCNCode") = _strCNCode
            Catch ex As Exception

            Finally

            End Try

        End If


        'ModGeneral.PickDate(Me.PickTransDate, Me.txtTransDate)
        'ModGeneral.PickDate(Me.PickStartDate, Me.txtStartDate)
        'ModGeneral.PickDate(Me.PickEndDate, Me.txtEndDate)
        'ModGeneral.PickDate(Me.PickRWDate, Me.txtRWDate)

        'ModGeneral.PickFromPopup(PopTypes.AGENCY, Me.cmdAgcy, Me.txtAgcyNum, Me.txtAgcyName)
        ''ModGeneral.PickFromPopup(PopTypes.BRANCH, Me.cmdBra, Me.txtBraNum, Me.txtBraName)


        If Me.txtAction.Text = "New" Then
            Call DoNew()
            Me.txtAction.Text = ""
            'Me.txtTransNum.Enabled = True
            'Me.txtTransNum.Focus()
        End If

        If Me.txtAction.Text = "Save" Then
            'Call DoSave()
            'Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete" Then
            'Call DoDelete()
            Me.txtAction.Text = ""
        End If


    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        Me.txtCode.Text = ""
        Try
            Me.txtCode.Text = cboSearch.SelectedItem.Value
        Catch ex As Exception
            Me.txtCode.Text = ""
        End Try

        If RTrim(Me.txtCode.Text) <> "" Then
            Me.txtTransNum.Text = RTrim(Me.txtCode.Text)
            strREC_ID = RTrim(Me.txtTransNum.Text)
            strErrMsg = Proc_OpenRecord("BY_TRANS_NO", Me.txtTransNum.Text)
        End If

    End Sub


    Protected Sub chkTransum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTransum.CheckedChanged
        If Me.chkTransum.Checked = True Then
            Me.lblTransNum.Enabled = True
            Me.txtTransNum.Enabled = True
            Me.cmdTransNum.Enabled = True
        Else
            Me.lblTransNum.Enabled = False
            Me.txtTransNum.Enabled = False
            Me.cmdTransNum.Enabled = False
        End If

    End Sub

    Protected Sub txtTransNum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTransNum.TextChanged
        Call DoProc_Get_DNCN_Info()
    End Sub

    Protected Sub cmdTransNum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTransNum.Click
        Call DoProc_Get_DNCN_Info()

    End Sub

    Private Sub DoProc_Get_DNCN_Info()
        Select Case Left(Trim(Me.txtTransNum.Text), 1)
            Case "D"
                If IsNumeric(Mid(Trim(Me.txtTransNum.Text), 2)) Then
                    Me.txtTransNum.Text = "D" & Format(Val(Mid(Trim(Me.txtTransNum.Text), 2)), "00000000")
                End If
            Case "C"
                If IsNumeric(Mid(Trim(Me.txtTransNum.Text), 2)) Then
                    Me.txtTransNum.Text = "C" & Format(Val(Mid(Trim(Me.txtTransNum.Text), 2)), "00000000")
                End If
        End Select


        If RTrim(Me.txtTransNum.Text) <> "" Then
            strREC_ID = RTrim(Me.txtTransNum.Text)
            strErrMsg = Proc_OpenRecord("BY_TRANS_NO", Me.txtTransNum.Text)
        End If
    End Sub

    Protected Sub cmdInsuredSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdInsuredSearch.Click
        If LTrim(RTrim(Me.txtInsuredName.Text)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtInsuredName.Text)) <> "" Then
            'Call gnProc_Populate_Box("GL_ASSURED_HELP_SP", "001", Me.cboInsuredName, RTrim(Me.txtInsuredName.Text))
            Call gnProc_Populate_Box("GL_ASSURED_HELP_SP_DNCN", "001", Me.cboInsuredName, RTrim(Me.txtInsuredName.Text))
        End If

    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call DoSave()
        Me.txtAction.Text = ""

    End Sub

    'Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
    '    Call DoNew()
    'End Sub

    Protected Sub cboMemberBatchNum_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMemberBatchNum.SelectedIndexChanged
        Call DoGet_SelectedItem(Me.cboMemberBatchNum, Me.txtMemberBatchNum, Me.txtMemberBatchName, Me.lblMessage)
        If Trim(Me.txtMemberBatchNum.Text) <> "" And Trim(Me.txtPolNum.Text) <> "" Then
            Call Proc_Get_SA_Prem()
        End If

    End Sub

    Private Sub DoGet_SelectedItem(ByVal pvDDL_Control As DropDownList, ByVal pvCtr_Value As TextBox, ByVal pvCtr_Text As TextBox, Optional ByVal pvCtr_Label As Label = Nothing)
        Try
            If pvDDL_Control.SelectedIndex = -1 Or pvDDL_Control.SelectedIndex = 0 Or _
            pvDDL_Control.SelectedItem.Value = "" Or pvDDL_Control.SelectedItem.Value = "*" Then
                pvCtr_Value.Text = ""
                pvCtr_Text.Text = ""
            Else
                pvCtr_Value.Text = pvDDL_Control.SelectedItem.Value
                pvCtr_Text.Text = pvDDL_Control.SelectedItem.Text
            End If
        Catch ex As Exception
            If pvCtr_Label IsNot Nothing Then
                If TypeOf pvCtr_Label Is System.Web.UI.WebControls.Label Then
                    pvCtr_Label.Text = "Error. Reason: " & ex.Message.ToString
                End If
            End If
        End Try

    End Sub



    Protected Sub DoNew()

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

        Call Proc_DDL_Get(Me.cboTransType, RTrim("*"))
        Call Proc_DDL_Get(Me.cboBusType, RTrim("*"))
        Call Proc_DDL_Get(Me.cboTransCode, RTrim("*"))
        Call Proc_DDL_Get(Me.cboSecName, RTrim("*"))
        Call Proc_DDL_Get(Me.cboAgcyName, RTrim("*"))
        'Call Proc_DDL_Get(Me.cboSubRiskName, RTrim("*"))
        Call Proc_DDL_Get(Me.cboBranchName, RTrim("*"))
        Call Proc_DDL_Get(Me.cboInsuredName, RTrim("*"))
        Call Proc_DDL_Get(Me.cboMemberBatchNum, RTrim("*"))

        With Me
            .txtTransNum.ReadOnly = False
            .txtTransNum.Enabled = False
            .chkTransum.Checked = False
            .chkTransum.Enabled = True
            .cmdTransNum.Enabled = False
            .txtTransNum.Text = ""
            .txtSecNum.Text = ""
            .txtSecName.Text = ""
            .txtTransType.Text = ""
            .txtTransCode.Text = ""
            .txtTransDate.Text = ""
            .txtBraNum.Text = ""
            .txtBraName.Text = ""
            .txtLocNum.Text = ""
            .txtFileNum.Text = ""
            .txtQuote_Num.Text = ""
            .txtPolNum.Text = ""
            .txtInsuredName.Text = ""
            .txtInsuredNum.Text = ""
            .txtRiskNum.Text = ""
            .txtSubRiskNum.Text = ""
            .txtMemberBatchNum.Text = ""
            .txtStartDate.Text = ""
            .txtEndDate.Text = ""
            .txtRWDate.Text = ""
            .txtBroker_Search.Text = ""
            .txtAgcyNum.Text = ""
            .txtAgcyType.Text = ""
            .txtAgcyName.Text = ""
            .txtBusType.Text = ""
            .txtRefNum.Text = ""
            .txtRefCode.Text = ""
            .txtRefDate.Text = ""
            .txtTrans_Full_SI.Text = "0"
            .txtTrans_Full_Prem.Text = "0"
            .txtTrans_Rate.Text = "0"
            .txtSumIns.Text = "0"
            .txtGrsPrem.Text = "0"
            .txtAgcyRate.Text = "0"
            .chkProrataYN.Checked = False
            .lblProRataRDay.Enabled = False
            .txtProRataRDay.Text = "365"
            .txtProRataRDay.Enabled = False
            .lblProRataNDay.Enabled = False
            .txtProRataNDay.Text = "0"
            .txtProRataNDay.Enabled = False
            .txtTransAmt.Enabled = False
            .txtTransAmt.Text = "0"
            .txtTransDescr1.Text = ""
            .txtTransDescr2.Text = ""

            '.txtLC_SI.Text = "0"
            '.txtLC_Rate.Text = "0"
            .txtRet_SI.Text = "0"
            .txtRet_Rate.Text = "0"
            .txtSurp1_SI.Text = "0"
            .txtSurp1_Rate.Text = "0"
            .txtSurp2_SI.Text = "0"
            .txtSurp2_Rate.Text = "0"
            .txtQuota_SI.Text = "0"
            .txtQuota_Rate.Text = "0"
            .txtFacBal_SI.Text = "0"
            .txtFacBal_Rate.Text = "0"
            .txtTreatyRef_Num.Text = ""
            .txtTreatyRef_Descr.Text = ""

            .cmdDelete_ASP.Enabled = False
            .lblMessage.Text = "Status: New Entry..."
        End With

    End Sub

    Protected Sub DoSave()

        lblMessage.Text = "Saving data. Please wait..."

        'D00000001 
        Dim intC As Long = 0
        Dim blnRet As Boolean
        blnRet = False


        Dim xc As Integer = 0

        Dim myTmp_Chk As String
        Dim myTmp_Ref As String
        myTmp_Chk = "N"
        myTmp_Ref = ""

        Dim mystrCONN_Chk As String = ""
        Dim objOLEConn_Chk As OleDbConnection = Nothing
        Dim objOLECmd_Chk As OleDbCommand = Nothing

        'Me.txtTransNum.Text = LTrim(RTrim(Me.txtTransNum.Text))
        If Trim(Me.txtTransNum.Text) = "" Then
            'Me.txtTransNum.Text = "F/" & RTrim(Me.txtProduct_Num.Text) & "/" & RTrim("0000001")
            GoTo Proc_Skip_Check
        End If

        '====================================================
        '   START CHECK
        '====================================================

        For xc = 1 To Len(LTrim(RTrim(Me.txtTransNum.Text)))
            If Mid(LTrim(RTrim(Me.txtTransNum.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtTransNum.Text)), xc, 1) = ":" Then
                Me.lblMessage.Text = "Invalid character found in input field - " & Me.lblTransNum.Text
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Exit Sub
            End If
        Next

        myTmp_Chk = "N"
        myTmp_Ref = ""

        mystrCONN_Chk = CType(Session("connstr"), String)
        objOLEConn_Chk = New OleDbConnection()
        objOLEConn_Chk.ConnectionString = mystrCONN_Chk

        Try
            'open connection to database
            objOLEConn_Chk.Open()
        Catch ex As Exception
            Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn_Chk = Nothing
            Exit Sub
        End Try


        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 TBIL_POL_PRM_DCN_REC_ID, TBIL_POL_PRM_DCN_TRANS_NO  FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_PRM_DCN_TRANS_NO = '" & RTrim(txtTransNum.Text) & "'"
        If Val(LTrim(RTrim(Me.txtRecNo.Text))) <> 0 Then
            strSQL = strSQL & " AND TBIL_POL_PRM_DCN_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
        End If


        objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
        'objOLECmd_Chk.CommandTimeout = 180
        objOLECmd_Chk.CommandType = CommandType.Text
        'objOLECmd_Chk.CommandType = CommandType.StoredProcedure
        'objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        'objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 40).Value = strREC_ID
        'objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR_Chk As OleDbDataReader
        objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
        If (objOLEDR_Chk.Read()) Then
            myTmp_Chk = RTrim(CType(objOLEDR_Chk("TBIL_POL_PRM_DCN_REC_ID") & vbNullString, String))
            myTmp_Ref = RTrim(CType(objOLEDR_Chk("TBIL_POL_PRM_DCN_TRANS_NO") & vbNullString, String))
            If Val(myTmp_Chk) <> Val(Me.txtRecNo.Text) Then
                myTmp_Chk = "Y"
                Me.lblMessage.Text = "The Transaction No you enter already exist. \nPlease check Transaction no: " & myTmp_Ref & ""
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                'Me.lblMessage.Text = "Status:"
                'Exit Sub
            Else
                myTmp_Chk = "N"
            End If
        Else
            myTmp_Chk = "N"
        End If


        objOLEDR_Chk = Nothing
        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing

        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing

        If Trim(myTmp_Chk) <> "N" Then
            Exit Sub
        End If
        '====================================================
        '   END CHECK
        '====================================================


Proc_Skip_Check:



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

        Dim Dte_System As Date = Now

        Dim myTrn_Num As String = ""

        Dim myYear As String = ""
        Dim xx As Integer = 0


        Dim myDnCn_Treaty_Sw = ""
        Dim myQuota_Flag = ""


        'If Trim(Me.txtTransNum.Text) = "" Or RTrim(Me.txtTransNum.Text) = "*" Then
        'Me.lblMessage.Text = "Missing/Invalid " & RTrim(Me.lblTransNum.Text) & " ..."
        'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        'Exit Sub
        'End If

        If Trim(Me.txtTransNum.Text) = "." Or RTrim(Me.txtTransNum.Text) = "*" Then
            Me.lblMessage.Text = "Missing or Invalid " & RTrim(Me.lblTransNum.Text) & " ..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Call DoGet_SelectedItem(Me.cboSecName, Me.txtSecNum, Me.txtSecName, Me.lblMessage)
        If Me.txtSecNum.Text = "" Or Val(Me.txtSecNum.Text) = 0 Then
            'Me.lblMessage.Text = "Missing " & Me.lblSecNum.Text
            Me.lblMessage.Text = "Missing or Invalid business sector code. Required Numeric data..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If


        Call DoGet_SelectedItem(Me.cboTransType, Me.txtTransType, Me.txtTransTypeName, Me.lblMessage)
        If Trim(Me.txtTransType.Text) = "" Or RTrim(Me.txtTransType.Text) = "*" Then
            Me.lblMessage.Text = "Missing or Invalid transaction type..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Call DoGet_SelectedItem(Me.cboTransCode, Me.txtTransCode, Me.txtTransCodeName, Me.lblMessage)
        If Trim(Me.txtTransCode.Text) = "" Or RTrim(Me.txtTransCode.Text) = "*" Then
            Me.lblMessage.Text = "Missing or Invalid debit [D] or credit [C] code..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Me.txtTransDate.Text = Trim(Me.txtTransDate.Text)
        If RTrim(Me.txtTransDate.Text) = "" Then
            Me.lblMessage.Text = "Missing or Invalid billing date..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtTransDate.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMessage.Text = "Missing or Invalid " & Me.lblTransDate.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)
        If Val(Trim(strMyYear)) < 2000 Then
            Me.lblMessage.Text = "Sorry!. Billing year date must be greater than or equal to 2000..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        If Val(Trim(strMyYear)) > Now.Year Then
            Me.lblMessage.Text = "Sorry!. Future billing year date is not allowed..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            Me.lblMessage.Text = "Please enter valid billing date..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
            Exit Sub
        End If
        Me.txtTransDate.Text = RTrim(strMyDte)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteTrans = Format(mydte, "MM/dd/yyyy")

        'If Len(RTrim(Me.txtTransDate.Text)) = 10 Then
        '    strProcDate = Right(RTrim(Me.txtTransDate.Text), 4) & Left(LTrim(Me.txtTransDate.Text), 2)
        'Else
        '    If Mid(LTrim(Me.txtTransDate.Text), 2, 1) = "/" Then
        '        strProcDate = Right(RTrim(Me.txtTransDate.Text), 4) & "0" & Left(LTrim(Me.txtTransDate.Text), 1)
        '    Else
        '        strProcDate = Right(RTrim(Me.txtTransDate.Text), 4) & Left(LTrim(Me.txtTransDate.Text), 2)
        '    End If
        'End If


        'strProcDate = Right(RTrim(strMyDte), 4) & Left(LTrim(strMyDte), 2)
        strProcDate = Right(RTrim(Me.txtTransDate.Text), 4) & Mid(LTrim(Me.txtTransDate.Text), 4, 2)
        blnRet = gnCheck_DateStaus("001", RTrim(strProcDate))
        If blnRet = False Then
            'Me.cmdSave.Enabled = False
            Me.lblMessage.Text = "SORRY!. The date you supplied is NOT OPEN for data entry." & _
                "\n Your input date:" & strProcDate & " - " & Format(CType(dteTrans, Date), "MMMM-dd-yyyy") & _
                "\n Please consult your System Administrator..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Me.cmdSave.Enabled = True
            Exit Sub
        End If


        Call DoGet_SelectedItem(Me.cboBranchName, Me.txtBraNum, Me.txtBraName, Me.lblMessage)
        If RTrim(Me.txtBraNum.Text) = "" Or RTrim(Me.txtBraNum.Text) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid branch code..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Call DoProc_Validate_Policy()
        If Trim(Me.txtPolNum.Text) = "" Or RTrim(Me.txtPolNum.Text) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid policy number..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If


        'If RTrim(Me.txtInsuredName.Text) = "" Or RTrim(Me.txtInsuredName.Text) = "*" Then
        '    Me.lblMessage.Text = "Missing/Invalid insured name..."
        '    FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        '    Exit Sub
        'End If
        If RTrim(Me.txtInsuredNum.Text) = "" Or RTrim(Me.txtInsuredNum.Text) = "*" Then
            Me.lblMessage.Text = "Missing Invalid insured code..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        If RTrim(Me.txtSubRiskNum.Text) = "" Or RTrim(Me.txtSubRiskNum.Text) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid insurance subrisk code..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        If RTrim(Me.txtRiskNum.Text) = "" Or RTrim(Me.txtRiskNum.Text) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid insurance main risk code..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If


        'Call DoGet_SelectedItem(Me.cboMemberBatchNum, Me.txtMemberBatchNum, Me.txtMemberBatchName, Me.lblMessage)
        If Trim(Me.txtMemberBatchNum.Text) = "" Or RTrim(Me.txtMemberBatchNum.Text) = "*" Then
            Me.lblMessage.Text = "Missing or Invalid Members Batch No..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Me.txtStartDate.Text = Trim(Me.txtStartDate.Text)
        If RTrim(Me.txtStartDate.Text) = "" Then
            Me.lblMessage.Text = "Missing or Invalid date - Insurance Start Date..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtStartDate.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMessage.Text = "Missing or Invalid " & Me.lblStartDate.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        If Val(Trim(strMyYear)) < 2000 Then
            Me.lblMessage.Text = "Sorry!. Insurance start year date must be greater than or equal to 2000..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            Me.lblMessage.Text = "Please enter valid start insurance date..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
            Exit Sub
        End If
        Me.txtStartDate.Text = RTrim(strMyDte)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteStart = Format(mydte, "MM/dd/yyyy")

        'dteEnd = DateAdd(DateInterval.Year, Val(Me.txtPrem_Period_Yr.Text), dteStart)
        If Trim(Me.txtEndDate.Text) = "" Then
            'Me.txtEndDate.Text = Format(dteEnd, "dd/MM/yyyy")
        End If



        Me.txtEndDate.Text = Trim(Me.txtEndDate.Text)
        If RTrim(Me.txtEndDate.Text) = "" Then
            Me.lblMessage.Text = "Missing or Invalid date - Insurance End Date..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtEndDate.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMessage.Text = "Missing or Invalid " & Me.lblEndDate.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        'If Val(Trim(strMyYear)) < 2000 Then
        '    Me.lblMessage.Text = "Sorry!. Insurance start year date must be greater than or equal to 2000..."
        '    FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        '    Exit Sub
        'End If

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            Me.lblMessage.Text = "Please enter valid end insurance date..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
            Exit Sub
        End If
        Me.txtEndDate.Text = RTrim(strMyDte)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteEnd = Format(mydte, "MM/dd/yyyy")

        GenEnd_Date = Format(dteEnd, "MM/dd/yyyy")

        If Trim(Me.txtRWDate.Text) = "" And Trim(Me.txtEndDate.Text) <> "" Then
            Me.txtRWDate.Text = Format(DateAdd(DateInterval.Day, 1, GenEnd_Date), "dd/MM/yyyy")
        End If

        If RTrim(Me.txtRWDate.Text) = "" Then
            Me.lblMessage.Text = "Missing or Invalid date - Insurance Renewal Date..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtRWDate.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMessage.Text = "Missing or Invalid " & Me.lblRWDate.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        'If Val(Trim(strMyYear)) < 2000 Then
        '    Me.lblMessage.Text = "Sorry!. Insurance start year date must be greater than or equal to 2000..."
        '    FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        '    Exit Sub
        'End If

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            Me.lblMessage.Text = "Please enter valid renewal insurance date..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
            Exit Sub
        End If
        Me.txtRWDate.Text = RTrim(strMyDte)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteStart_RW = Format(mydte, "MM/dd/yyyy")


        Call DoProc_Validate_Broker()
        If RTrim(Me.txtAgcyNum.Text) = "" Or RTrim(Me.txtAgcyNum.Text) = "*" Then
            Me.lblMessage.Text = "Missing or Invalid Data. Broker or Agent Code is required..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Call DoGet_SelectedItem(Me.cboBusType, Me.txtBusType, Me.txtBusTypeName, Me.lblMessage)
        If Trim(Me.txtBusType.Text) = "" Or RTrim(Me.txtBusType.Text) = "*" Then
            Me.lblMessage.Text = "Missing or Invalid business type..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtBusType.Text) = "RV" Or RTrim(Me.txtBusType.Text) = "RN" Then
            _strCNCode = CType(Session("_strCNCode"), String) ' do not check this ref num if it is a returned prem credit note as a result of membership withdrawal
            If _strCNCode.Trim = String.Empty Then
                If Trim(Me.txtRefNum.Text) = "" Then
                    Me.lblMessage.Text = "Missing reference number. Enter valid reference number..."
                    FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                    Exit Sub
                End If
            End If
        End If

        myTrn_Num = RTrim(Me.txtTransNum.Text)
        If RTrim(Me.txtRefNum.Text) = RTrim(myTrn_Num) Then
            FirstMsg = "Javascript:alert('Mismatch or Invalid or Incorrect reference number. Please enter correct or valid reference no...');"
            Me.txtRefNum.Text = ""
            Me.txtRefCode.Text = ""
            Me.txtRefDate.Text = ""
            Me.txtRefNum.Enabled = True
            Me.txtRefNum.Focus()
        End If


        If RTrim(Me.txtRefNum.Text) <> "" Then
            blnRet = Proc_Get_Ref(RTrim(Me.txtRefNum.Text), RTrim(Me.txtPolNum.Text), RTrim(Me.txtAgcyNum.Text))
            If blnRet = False Then
                FirstMsg = "Javascript:alert('Invalid/Incorrect reference number. Please enter correct/valid reference no...');"
                Me.txtRefNum.Text = ""
                Me.txtRefCode.Text = ""
                Me.txtRefDate.Text = ""
                Me.txtRefNum.Enabled = True
                Me.txtRefNum.Focus()
            End If
        End If

        Me.txtTrans_Full_SI.Text = Trim(LTrim(Me.txtTrans_Full_SI.Text))
        If RTrim(Me.txtTrans_Full_SI.Text) = "" Or Val(Me.txtTrans_Full_SI.Text) = 0 Then
            Me.lblMessage.Text = "Missing or Invalid Data. Please enter the Full Sum Assured..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Me.txtTrans_Full_Prem.Text = Trim(LTrim(Me.txtTrans_Full_Prem.Text))
        If RTrim(Me.txtTrans_Full_Prem.Text) = "" Or Val(Me.txtTrans_Full_Prem.Text) = 0 Then
            Me.lblMessage.Text = "Missing or Invalid Data. Please enter the Full Gross Premium..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Me.txtTrans_Rate.Text = Me.txtTrans_Rate.Text
        If RTrim(Me.txtTrans_Rate.Text) = "" Or Val(Me.txtTrans_Rate.Text) = 0 Then
            Me.lblMessage.Text = "Missing or Invalid Data. Please enter your Company Share Rate..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Me.txtTrans_Rate.Text = "0"
            Exit Sub
        End If

        Call Proc_Update_SA_Prem()

        Me.txtSumIns.Text = Trim(LTrim(Me.txtSumIns.Text))
        If RTrim(Me.txtSumIns.Text) = "" Or Val(Me.txtSumIns.Text) = 0 Then
            Me.lblMessage.Text = "Missing or Invalid Data. Your Company Sum Assured is required..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Me.txtSumIns.Text = "0"
            Exit Sub
        End If
        Me.txtGrsPrem.Text = Trim(LTrim(Me.txtGrsPrem.Text))
        If RTrim(Me.txtGrsPrem.Text) = "" Or Val(Me.txtGrsPrem.Text) = 0 Then
            Me.lblMessage.Text = "Missing or Invalid Data. Your Company Gross Premium is required..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Me.txtGrsPrem.Text = "0"
            Exit Sub
        End If

        Me.txtAgcyRate.Text = Trim(LTrim(Me.txtAgcyRate.Text))
        If RTrim(Me.txtAgcyRate.Text) = "" Then
            Me.lblMessage.Text = "Missing or Invalid Data. Broker or Agent commission rate is required..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Me.txtAgcyRate.Text = "0"
            Exit Sub
        End If
        If Val(Me.txtAgcyRate.Text) = 0 Then
            Me.txtAgcyRate.Text = "0"
            Exit Sub
        End If

        If RTrim(Me.txtTransDescr1.Text) = "" Or RTrim(Me.txtTransDescr1.Text) = "*" Then
            Me.lblMessage.Text = "Missing transaction description..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Dim dblMyTrans_Amt As Double = 0
        Dim dblMyComm_Amt As Double = 0
        Dim dblMyNet_Amt As Double = 0

        dblMyTrans_Amt = CType(Trim(Me.txtGrsPrem.Text), Double)

        ' BASE COMMISSION ON GROSS AMOUNT
        If Val(Trim(Me.txtAgcyRate.Text)) <> 0 And Val(dblMyTrans_Amt) <> 0 Then
            dblMyComm_Amt = dblMyTrans_Amt * Val(Trim(Me.txtAgcyRate.Text)) / 100
        End If

        ' COMPUTE NET AMOUNT
        dblMyNet_Amt = dblMyTrans_Amt - dblMyComm_Amt

        If Val(Me.txtProRataRDay.Text) <> 0 And Val(Me.txtProRataNDay.Text) <> 0 And Val(Me.txtGrsPrem.Text) <> 0 Then
            dblMyTrans_Amt = (Val(Me.txtGrsPrem.Text) / Val(Me.txtProRataRDay.Text)) * Val(Me.txtProRataNDay.Text)
        End If
        Me.txtTransAmt.Text = dblMyTrans_Amt

        ' BASE COMMISSION ON PRO-RATA AMOUNT
        'If Val(Trim(Me.txtAgcyRate.Text)) <> 0 And Val(dblMyTrans_Amt) <> 0 Then
        '    dblMyComm_Amt = dblMyTrans_Amt * Val(Trim(Me.txtAgcyRate.Text)) / 100
        'End If

        ' COMPUTE NET AMOUNT
        'dblMyNet_Amt = dblMyTrans_Amt - dblMyComm_Amt

        'strSerialNum = ""

        'dteDate = CType(RTrim(Me.txtStartDate.Text), Date)
        myYear = Right(RTrim(Me.txtStartDate.Text), 4)
        myYear = Format(dteStart, "yyyy")
        'gnStartDate = CType(RTrim(Me.txtEndDate.Text), Date)
        'Select Case Trim(Me.txtRiskNum.Text)
        '    Case "F"
        '        Me.txtRWDate.Text = Format(gnStartDate, "MM/dd/yyyy")
        '        gnEndDate = gnStartDate.AddYears(1)
        '    Case Else
        '        gnStartDate = gnStartDate.AddDays(1)
        '        Me.txtRWDate.Text = Format(gnStartDate, "MM/dd/yyyy")
        '        gnEndDate = gnStartDate.AddYears(1)
        'End Select


        'dteDateX = CType(RTrim(Me.txtTransDate.Text), Date)
        'strMyYear = CType(Format(dteDateX.Year, "0000"), String)
        'strMyMth = CType(Format(dteDateX.Month, "00"), String)
        'strMyDay = CType(Format(dteDateX.Day, "00"), String)
        'strMyDte = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)


        If Trim(Me.txtRefNum.Text) = "" Then
            'Me.txtRefDate.Text = Trim(strMyDte)
            Me.txtRefDate.Text = Trim(Me.txtTransDate.Text)
            dteRef = dteTrans
        End If

        Select Case RTrim(Me.txtBusType.Text)
            Case "RV", "RN"
            Case Else
                'Me.txtRefDate.Text = Trim(Me.txtTransDate.Text)
                'dteRef = dteTrans
        End Select


        'Me.cmdSave.Enabled = False

        Dim strPol_Prd As String
        strPol_Prd = CType(DateDiff(DateInterval.Day, dteStart, dteEnd), String)
        If Val(strPol_Prd) = 0 Then
            strPol_Prd = 1
        End If
        If strPol_Prd = 364 Then strPol_Prd = 365

        'gnPrem_Amt = CType(Trim(Me.txtGrsPrem.Text), Double)


        '===========================================================
        'START TREATY ROUTINE
        '===========================================================

        myDnCn_Treaty_Sw = "N"
        myQuota_Flag = ""

        blnRet = False
        'blnRet = gnUpdate_Treaty("0", RTrim(myYear), Me.txtSubRiskNum.Text, _
        '  Me.txtTreatyRef_Num, Me.txtTreatyRef_Descr, _
        '  CType(Trim(Me.txtSumIns.Text), Double), CType(Trim(Me.txtGrsPrem.Text), Double), _
        '  Me.txtLC_SI, Me.txtLC_Rate, Me.txtRet_SI, Me.txtRet_Rate, _
        '  Me.txtSurp1_SI, Me.txtSurp1_Rate, Me.txtSurp2_SI, Me.txtSurp2_Rate, _
        '  Me.txtQuota_SI, Me.txtQuota_Rate, Me.txtFacBal_SI, Me.txtFacBal_Rate)

        'If blnRet = False Then
        '    myDnCn_Treaty_Sw = "N"
        'Else
        'End If

        'myQuota_Flag = RTrim(gnQuota_Flag)
        'If Val(Me.txtQuota_SI.Text) > 0 Or Val(Me.txtQuota_Rate.Text) > 0 Then
        '    myQuota_Flag = "Q"
        'End If

        ''Legal cession
        'gnLc_Prem = 0.0#
        'gnLc_Rate = CType(Val(Me.txtLC_Rate.Text), Double)
        'If gnLc_Rate > 0 And gnPrem_Amt > 0 Then
        '    gnLc_Prem = gnLc_Rate * gnPrem_Amt / 100
        'End If
        'gnLc_SI = CType(Val(Me.txtLC_SI.Text), Double)

        ''Retension
        'gnRet_Prem = 0.0#
        'gnRet_Rate = CType(Val(Me.txtRet_Rate.Text), Double)
        'If gnRet_Rate > 0 And gnPrem_Amt > 0 Then
        '    gnRet_Prem = gnRet_Rate * gnPrem_Amt / 100
        'End If
        'gnRet_SI = CType(Val(Me.txtRet_SI.Text), Double)

        ''SURPLUS TREATY
        ''First surplus
        'gnSurp1_Prem = 0.0#
        'gnSurp1_Rate = CType(Val(Me.txtSurp1_Rate.Text), Single)
        'If gnSurp1_Rate > 0 And gnPrem_Amt > 0 Then
        '    gnSurp1_Prem = gnSurp1_Rate * gnPrem_Amt / 100
        'End If
        'gnSurp1_SI = CType(Val(Me.txtSurp1_SI.Text), Single)

        ''Second surplus
        'gnSurp2_Prem = 0.0#
        'gnSurp2_Rate = CType(Val(Me.txtSurp2_Rate.Text), Double)
        'If gnSurp2_Rate > 0 And gnPrem_Amt > 0 Then
        '    gnSurp2_Prem = gnSurp2_Rate * gnPrem_Amt / 100
        'End If
        'gnSurp2_SI = CType(Val(Me.txtSurp2_SI.Text), Double)

        ''Quota share
        'gnQuota_Prem = 0.0#
        'gnQuota_Rate = CType(Val(Me.txtQuota_Rate.Text), Double)
        'If gnQuota_Rate > 0 And gnPrem_Amt > 0 Then
        '    gnQuota_Prem = gnQuota_Rate * gnPrem_Amt / 100
        'End If
        'gnQuota_SI = CType(Val(Me.txtQuota_SI.Text), Double)

        ''Fac. Outward Bal
        'gnFac_Prem = 0.0#
        'gnFac_Rate = CType(Val(Me.txtFacBal_Rate.Text), Double)
        'If gnFac_Rate > 0 And gnPrem_Amt > 0 Then
        '    gnFac_Prem = gnFac_Rate * gnPrem_Amt / 100
        'End If
        'gnFac_SI = CType(Val(Me.txtFacBal_SI.Text), Double)

        ''===========================================================
        ''END TREATY ROUTINE
        ''===========================================================


        'Get next invoice number
        If Trim(Me.txtTransNum.Text) = "" Or RTrim(Me.txtTransNum.Text) = "*" Then
            'Me.txtTransNum.Text = gnGet_Serial_Num("DNCN_INVOICE", RTrim(strProcDate), RTrim(Me.txtBraNum.Text), RTrim(Me.txtSecNum.Text), RTrim(Me.txtTransCode.Text))
            Me.txtTransNum.Text = gnGet_Serial_Num("DNCN_INVOICE_GRP_LIFE", RTrim(strProcDate), RTrim(Me.txtBraNum.Text), RTrim(Me.txtSecNum.Text), RTrim(Me.txtTransCode.Text))
        End If
        If Trim(Me.txtTransNum.Text) = "" Or _
           Trim(Me.txtTransNum.Text) = "." Or _
           Trim(Me.txtTransNum.Text) = ";" Or _
           RTrim(Me.txtTransNum.Text) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & RTrim(Me.lblTransNum.Text) & " ..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If


        If Trim(Me.txtRWDate.Text) = "" Then
            dteStart_RW = DateAdd(DateInterval.Day, 1, dteEnd)
            Me.txtRWDate.Text = Format(dteStart_RW, "dd/MM/yyyy")
        End If

        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try


        strREC_ID = Trim(Me.txtTransNum.Text)

        strTable = strTableName

        strSQL = ""


        '====================================================
        '   START CHECK
        '====================================================

        myTmp_Chk = "N"
        myTmp_Ref = ""

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try


        strSQL = ""
        strSQL = "SELECT TOP 1 TBIL_POL_PRM_DCN_TRANS_NO, TBIL_POL_PRM_DCN_SER_NO FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_PRM_DCN_TRANS_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_PRM_DCN_REC_ID = '" & Val(RTrim(Me.txtRecNo.Text)) & "'"


        Dim objOLECmd_Tmp As OleDbCommand = Nothing
        objOLECmd_Tmp = New OleDbCommand(strSQL, objOLEConn)
        'objOLECmd_Tmp.CommandTimeout = 180
        objOLECmd_Tmp.CommandType = CommandType.Text
        'objOLECmd_Tmp.CommandType = CommandType.StoredProcedure
        'objOLECmd_Tmp.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        'objOLECmd_Tmp.Parameters.Add("p01", OleDbType.VarChar, 40).Value = strREC_ID
        'objOLECmd_Tmp.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)


        Dim objOLEDR_Tmp As OleDbDataReader
        objOLEDR_Tmp = objOLECmd_Tmp.ExecuteReader()
        If (objOLEDR_Tmp.Read()) Then
            myTmp_Chk = "Y"
            'strSerialNum = CType(objOLEDR_Tmp("TBIL_POL_PRM_DCN_SER_NO") & vbNullString, String)
        Else
            myTmp_Chk = "N"
        End If


        objOLEDR_Tmp = Nothing
        objOLECmd_Tmp.Dispose()
        objOLECmd_Tmp = Nothing


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        If Trim(myTmp_Chk) = "N" Then
            'Get next serial number
            strSerialNum = gnGet_Serial_Num("DNCN_SN_GRP_LIFE", RTrim(strProcDate), RTrim(Me.txtBraNum.Text), RTrim(Me.txtSecNum.Text))
        End If

        '====================================================
        '   END CHECK
        '====================================================



        objOLEConn = New OleDbConnection(mystrCONN)
        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try


        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_PRM_DCN_TRANS_NO = '" & RTrim(strREC_ID) & "'"
        'strSQL = strSQL & " AND DNCN_REC_ID = '" & RTrim("001") & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        'objDA.SelectCommand.Connection = objOLEConn
        'objDA.SelectCommand.Transaction = objOLETran
        'objDA.SelectCommand.CommandType = CommandType.Text
        'objDA.SelectCommand.CommandText = strSQL
        'or
        'objDA.SelectCommand = New System.Data.OleDb.OleDbCommand(strSQL, objOleConn)

        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        'Dim m_rwContact As System.Data.DataRow
        intC = 0


        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '   Creating a new record

                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()

                drNewRow("TBIL_POL_PRM_DCN_DCN_PROC_DATE") = RTrim(strProcDate)
                drNewRow("TBIL_POL_PRM_DCN_BATCH_NO") = Val(RTrim(Me.txtSecNum.Text))
                drNewRow("TBIL_POL_PRM_DCN_SER_NO") = Val(RTrim(strSerialNum))

                'drNewRow("TBIL_POL_PRM_DCN_REC_ID") = Val(RTrim(Me.txtRecNo.Text))

                drNewRow("TBIL_POL_PRM_DCN_MDLE") = RTrim("G")

                drNewRow("TBIL_POL_PRM_DCN_FILE_NO") = RTrim(Me.txtFileNum.Text)
                drNewRow("TBIL_POL_PRM_DCN_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                drNewRow("TBIL_POL_PRM_DCN_POLY_NO") = RTrim(Me.txtPolNum.Text)

                drNewRow("TBIL_POL_PRM_DCN_UNDW_YR") = RTrim(myYear)

                drNewRow("TBIL_POL_PRM_DCN_PRDCT_CAT") = RTrim(Me.txtRiskNum.Text)
                drNewRow("TBIL_POL_PRM_DCN_PRDCT_CD") = RTrim(Me.txtSubRiskNum.Text)
                drNewRow("TBIL_POL_PRM_DCN_MEMB_BATCH_NO") = RTrim(Me.txtMemberBatchNum.Text)

                drNewRow("TBIL_POL_PRM_DCN_DB_CR_CD") = RTrim(Me.txtTransCode.Text)
                drNewRow("TBIL_POL_PRM_DCN_BRANCH_NO") = RTrim(Me.txtBraNum.Text)
                drNewRow("TBIL_POL_PRM_DCN_DB_CR_TYP") = RTrim(Me.txtTransType.Text)
                drNewRow("TBIL_POL_PRM_DCN_TRANS_NO") = RTrim(Me.txtRefNum.Text)

                drNewRow("TBIL_POL_PRM_DCN_APRV_SW") = RTrim("N")
                'drNewRow("TBIL_POL_PRM_DCN_APRV_DATE") = dteApproved

                If Trim(Me.txtTransDate.Text) <> "" Then
                    drNewRow("TBIL_POL_PRM_DCN_BILL_DATE") = dteTrans
                    drNewRow("TBIL_POL_PRM_DCN_TRANS_DATE") = dteTrans
                End If

                drNewRow("TBIL_POL_PRM_DCN_TRANS_NO") = Me.txtTransNum.Text

                If Trim(Me.txtRefDate.Text) <> "" Then
                    drNewRow("TBIL_POL_PRM_DCN_REF_DATE") = dteRef
                End If
                drNewRow("TBIL_POL_PRM_DCN_REF_NO") = Me.txtRefNum.Text

                drNewRow("TBIL_POL_PRM_DCN_SOURCE") = RTrim(Me.txtBusType.Text)

                If Trim(Me.txtStartDate.Text) <> "" Then
                    drNewRow("TBIL_POL_PRM_DCN_POL_ST_DT") = dteStart
                End If
                If Trim(Me.txtEndDate.Text) <> "" Then
                    drNewRow("TBIL_POL_PRM_DCN_POL_END_DT") = dteEnd
                End If
                If Trim(Me.txtRWDate.Text) <> "" Then
                    drNewRow("TBIL_POL_PRM_DCN_POL_RW_DT") = dteStart_RW
                End If

                drNewRow("TBIL_POL_PRM_DCN_BRK_CODE") = RTrim(Me.txtAgcyNum.Text)
                drNewRow("TBIL_POL_PRM_DCN_INSRD_CODE") = RTrim(Me.txtInsuredNum.Text)

                drNewRow("TBIL_POL_PRM_DCN_COY_SHARE") = Val(Me.txtTrans_Rate.Text)
                drNewRow("TBIL_POL_PRM_DCN_FULL_SA") = Val(Me.txtTrans_Full_SI.Text)
                drNewRow("TBIL_POL_PRM_DCN_FULL_PREM") = Val(Me.txtTrans_Full_Prem.Text)

                'drNewRow("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                'drNewRow("TBIL_POL_PRM_DCN_FAC_AMT") = Val(0)
                Select Case Trim(Me.txtTransType.Text)
                    Case "A"
                        drNewRow("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                        drNewRow("TBIL_POL_PRM_DCN_FAC_AMT") = Val(0)
                    Case "B"
                        drNewRow("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                        drNewRow("TBIL_POL_PRM_DCN_FAC_AMT") = Val(Me.txtGrsPrem.Text)
                    Case "C", "D"
                        drNewRow("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                        drNewRow("TBIL_POL_PRM_DCN_FAC_AMT") = Val(0)
                    Case "I"
                        drNewRow("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                        drNewRow("TBIL_POL_PRM_DCN_FAC_AMT") = Val(Me.txtGrsPrem.Text)
                    Case "O"
                        drNewRow("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                        drNewRow("TBIL_POL_PRM_DCN_FAC_AMT") = Val(Me.txtGrsPrem.Text)
                    Case "T", "V"
                        drNewRow("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                        drNewRow("TBIL_POL_PRM_DCN_FAC_AMT") = Val(0)
                End Select

                drNewRow("TBIL_POL_PRM_DCN_TRTY_RT") = Val(0)
                drNewRow("TBIL_POL_PRM_DCN_TRTY_AMT") = Val(0)
                drNewRow("TBIL_POL_PRM_DCN_VAT_SW") = RTrim("N")
                drNewRow("TBIL_POL_PRM_DCN_VAT_RT") = Val(0)
                drNewRow("TBIL_POL_PRM_DCN_COMM_RT") = Val(Trim(Me.txtAgcyRate.Text))
                drNewRow("TBIL_POL_PRM_DCN_COMM_AMT") = Val(dblMyComm_Amt)

                drNewRow("TBIL_POL_PRM_DCN_DESC") = RTrim(Me.txtTransDescr1.Text)
                'drNewRow("TBIL_POL_PRM_DCN_DESC_2") = RTrim(Me.txtTransDescr2.Text)

                drNewRow("TBIL_POL_PRM_DCN_PERIOD_DAYS") = Val(Me.txtProRataRDay.Text)
                drNewRow("TBIL_POL_PRM_DCN_PRO_RATA_DAYS") = Val(Me.txtProRataNDay.Text)
                drNewRow("TBIL_POL_PRM_DCN_PRO_RATA_AMT") = Val(Me.txtTransAmt.Text)

                drNewRow("TBIL_POL_PRM_DCN_SA_LC") = Val(Me.txtSumIns.Text)
                drNewRow("TBIL_POL_PRM_DCN_SA_FC") = Val(Me.txtSumIns.Text)
                drNewRow("TBIL_POL_PRM_DCN_AMT_LC") = Val(Me.txtGrsPrem.Text)
                drNewRow("TBIL_POL_PRM_DCN_AMT_FC") = Val(Me.txtGrsPrem.Text)


                drNewRow("TBIL_POL_PRM_DCN_TAG") = ""
                drNewRow("TBIL_POL_PRM_DCN_FLAG") = "A"
                drNewRow("TBIL_POL_PRM_DCN_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_POL_PRM_DCN_KEYDTE") = Now

                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                drNewRow = Nothing

                Me.lblMessage.Text = "New Record Saved to Database Successfully. - Document No " & Me.txtTransNum.Text

            Else
                '   Update existing record

                'm_rwContact = m_dtContacts.Rows(0)
                'm_rwContact("ContactName") = "Bob Brown"
                'm_rwContact.AcceptChanges()
                'm_dtContacts.AcceptChanges()
                'Dim intC As Integer = m_daDataAdapter.Update(m_dtContacts)


                With obj_DT

                    .Rows(0)("TBIL_POL_PRM_DCN_DCN_PROC_DATE") = RTrim(strProcDate)
                    .Rows(0)("TBIL_POL_PRM_DCN_BATCH_NO") = Val(RTrim(Me.txtSecNum.Text))
                    .Rows(0)("TBIL_POL_PRM_DCN_SER_NO") = Val(RTrim(strSerialNum))

                    '.Rows(0)("TBIL_POL_PRM_DCN_REC_ID") = Val(RTrim(Me.txtRecNo.Text))

                    .Rows(0)("TBIL_POL_PRM_DCN_MDLE") = RTrim("G")

                    .Rows(0)("TBIL_POL_PRM_DCN_FILE_NO") = RTrim(Me.txtFileNum.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_POLY_NO") = RTrim(Me.txtPolNum.Text)

                    .Rows(0)("TBIL_POL_PRM_DCN_UNDW_YR") = RTrim(myYear)

                    .Rows(0)("TBIL_POL_PRM_DCN_PRDCT_CAT") = RTrim(Me.txtRiskNum.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_PRDCT_CD") = RTrim(Me.txtSubRiskNum.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_MEMB_BATCH_NO") = RTrim(Me.txtMemberBatchNum.Text)

                    .Rows(0)("TBIL_POL_PRM_DCN_DB_CR_CD") = RTrim(Me.txtTransCode.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_BRANCH_NO") = RTrim(Me.txtBraNum.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_DB_CR_TYP") = RTrim(Me.txtTransType.Text)

                    '.Rows(0)("TBIL_POL_PRM_DCN_APRV_SW") = RTrim("N")
                    '.Rows(0)("TBIL_POL_PRM_DCN_APRV_DATE") = dteApproved

                    If Trim(Me.txtTransDate.Text) <> "" Then
                        .Rows(0)("TBIL_POL_PRM_DCN_BILL_DATE") = dteTrans
                        .Rows(0)("TBIL_POL_PRM_DCN_TRANS_DATE") = dteTrans
                    End If
                    .Rows(0)("TBIL_POL_PRM_DCN_TRANS_NO") = Me.txtTransNum.Text
                    If Trim(Me.txtRefDate.Text) <> "" Then
                        .Rows(0)("TBIL_POL_PRM_DCN_REF_DATE") = dteRef
                    End If
                    .Rows(0)("TBIL_POL_PRM_DCN_REF_NO") = Me.txtRefNum.Text

                    .Rows(0)("TBIL_POL_PRM_DCN_SOURCE") = RTrim(Me.txtBusType.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_POL_ST_DT") = dteStart
                    .Rows(0)("TBIL_POL_PRM_DCN_POL_END_DT") = dteEnd
                    .Rows(0)("TBIL_POL_PRM_DCN_POL_RW_DT") = dteStart_RW

                    .Rows(0)("TBIL_POL_PRM_DCN_BRK_CODE") = RTrim(Me.txtAgcyNum.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_INSRD_CODE") = RTrim(Me.txtInsuredNum.Text)

                    .Rows(0)("TBIL_POL_PRM_DCN_COY_SHARE") = Val(Me.txtTrans_Rate.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_FULL_SA") = Val(Me.txtTrans_Full_SI.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_FULL_PREM") = Val(Me.txtTrans_Full_Prem.Text)

                    '.Rows(0)("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                    '.Rows(0)("TBIL_POL_PRM_DCN_FAC_AMT") = Val(0)

                    Select Case Trim(Me.txtTransType.Text)
                        Case "A"
                            .Rows(0)("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                            .Rows(0)("TBIL_POL_PRM_DCN_FAC_AMT") = Val(0)
                        Case "B"
                            .Rows(0)("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                            .Rows(0)("TBIL_POL_PRM_DCN_FAC_AMT") = Val(Me.txtGrsPrem.Text)
                        Case "C", "D"
                            .Rows(0)("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                            .Rows(0)("TBIL_POL_PRM_DCN_FAC_AMT") = Val(0)
                        Case "I"
                            .Rows(0)("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                            .Rows(0)("TBIL_POL_PRM_DCN_FAC_AMT") = Val(Me.txtGrsPrem.Text)
                        Case "O"
                            .Rows(0)("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                            .Rows(0)("TBIL_POL_PRM_DCN_FAC_AMT") = Val(Me.txtGrsPrem.Text)
                        Case "T", "V"
                            .Rows(0)("TBIL_POL_PRM_DCN_FAC_RT") = Val(0)
                            .Rows(0)("TBIL_POL_PRM_DCN_FAC_AMT") = Val(0)
                    End Select

                    .Rows(0)("TBIL_POL_PRM_DCN_TRTY_RT") = Val(0)
                    .Rows(0)("TBIL_POL_PRM_DCN_TRTY_AMT") = Val(0)
                    .Rows(0)("TBIL_POL_PRM_DCN_VAT_SW") = RTrim("N")
                    .Rows(0)("TBIL_POL_PRM_DCN_VAT_RT") = Val(0)
                    .Rows(0)("TBIL_POL_PRM_DCN_COMM_RT") = Val(Trim(Me.txtAgcyRate.Text))
                    .Rows(0)("TBIL_POL_PRM_DCN_COMM_AMT") = Val(dblMyComm_Amt)

                    .Rows(0)("TBIL_POL_PRM_DCN_DESC") = RTrim(Me.txtTransDescr1.Text)
                    '.Rows(0)("TBIL_POL_PRM_DCN_DESC_2") = RTrim(Me.txtTransDescr2.Text)

                    .Rows(0)("TBIL_POL_PRM_DCN_PERIOD_DAYS") = Val(Me.txtProRataRDay.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_PRO_RATA_DAYS") = Val(Me.txtProRataNDay.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_PRO_RATA_AMT") = Val(Me.txtTransAmt.Text)

                    .Rows(0)("TBIL_POL_PRM_DCN_SA_LC") = Val(Me.txtSumIns.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_SA_FC") = Val(Me.txtSumIns.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_AMT_LC") = Val(Me.txtGrsPrem.Text)
                    .Rows(0)("TBIL_POL_PRM_DCN_AMT_FC") = Val(Me.txtGrsPrem.Text)


                    '.Rows(0)("TBIL_POL_PRM_DCN_TAG") = ""
                    .Rows(0)("TBIL_POL_PRM_DCN_FLAG") = "C"
                    '.Rows(0)("TBIL_POL_PRM_DCN_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_POL_PRM_DCN_KEYDTE") = Now

                End With

                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                Me.lblMessage.Text = "Record Saved to Database Successfully. - Document No " & Me.txtTransNum.Text

            End If


            'Dim dataSet As System.Data.DataSet = New System.Data.DataSet

            'm_daDataAdapter.Fill(dataSet, m_Tbl)
            '' Insert Code to modify data in DataSet here 
            ''   ...
            ''   ...

            ''m_cbCommandBuilder.GetInsertCommand()

            'm_cbCommandBuilder.GetUpdateCommand()

            ''m_cbCommandBuilder.GetDeleteCommand()

            '' Without the OleDbCommandBuilder this line would fail.
            'm_daDataAdapter.Update(dataSet, m_Tbl)


            '' If there is existing data, update it.
            'If m_dtContacts.Rows.Count <> 0 Then
            '    m_dtContacts.Rows(m_rowPosition)("ContactName") = strContactName
            '    m_dtContacts.Rows(m_rowPosition)("State") = strState
            '    m_daDataAdapter.Update(m_dtContacts)
            'Else
            '    '   Creating New Record
            '    Dim drNewRow As System.Data.DataRow = m_dtContacts.NewRow()
            '    drNewRow("ContactName") = strContactName
            '    drNewRow("State") = strState
            '    m_dtContacts.Rows.Add(drNewRow)
            '    m_daDataAdapter.Update(m_dtContacts)
            'End If


            ''To access the first row of your DataTable like this:
            'm_rwContact = m_dtContacts.Rows(0)

            ''To reference the value of a column, you can pass the column name to the DataRow like this:
            '' Change the value of the column.
            'm_rwContact("ContactName") = "Bob Brown"

            ''   or
            '' Get the value of the column.
            'strContactName = m_rwContact("ContactName")


            ''Deleting Record
            '' If there is data, delete the current row.
            'If m_dtContacts.Rows.Count <> 0 Then
            '    m_dtContacts.Rows(m_rowPosition).Delete()
            '    m_daDataAdapter.Update(m_dtContacts)
            'End If


        Catch ex As Exception
            Me.lblMessage.Text = ex.Message.ToString
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


        Me.cmdDelete_ASP.Enabled = True
        Me.txtTransNum.Enabled = False


        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"

        Call DoNew()

        'Me.txtTransNum.Enabled = True
        'Me.txtTransNum.Focus()

    End Sub


    Protected Sub DoProc_Broker_Change()
        Call DoGet_SelectedItem(Me.cboAgcyName, Me.txtAgcyNum, Me.txtBroker_Search, Me.lblMessage)
        'Call DoGet_SelectedItem(Me.cboAgcyName, Me.txtAgcyNum, Me.txtAgcyName, Me.lblMessage)

    End Sub

    Protected Sub DoProc_Broker_Search()
        If RTrim(Me.txtBroker_Search.Text) <> "" Then
            'Call gnProc_Populate_Box("IL_BROKERS_LIST_GL", "001", Me.cboAgcyName, RTrim(Me.txtBroker_Search.Text))
            Call gnProc_Populate_Box("GL_BROKERS_LIST", "001", Me.cboAgcyName, RTrim(Me.txtBroker_Search.Text))
        End If

    End Sub

    Protected Sub DoProc_Insured_Change()
        'Call DoGet_SelectedItem(Me.cboInsuredName, Me.txtPolNum, Me.txtInsuredName, Me.lblMessage)
        Call DoGet_SelectedItem(Me.cboInsuredName, Me.txtFileNum, Me.txtInsuredName, Me.lblMessage)
        'If RTrim(Me.txtPolNum.Text) = "" Then
        If RTrim(Me.txtFileNum.Text) = "" Then
        Else
            'blnStatusX = gnValidate_Codes("POLICY", Me.txtPolNum, Me.txtInsuredName)
            'Call DoProc_Validate_Policy()
            strP_ID = RTrim(Me.txtFileNum.Text)
            Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_GL_POLICY_BY_FILE_NO", RTrim(strP_ID), RTrim(""), RTrim(""))
            If oAL.Item(0) = "TRUE" Then
                '    'Retrieve the record
                '    Response.Write("<br/>Status: " & oAL.Item(0))
                '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
                Me.txtFileNum.Text = oAL.Item(2)
                Me.txtQuote_Num.Text = oAL.Item(3)
                Me.txtPolNum.Text = oAL.Item(4)
                Me.txtRiskNum.Text = oAL.Item(5)
                Me.txtSubRiskNum.Text = oAL.Item(6)
                'Me.txtPrem_Rate_TypeNum.Text = oAL.Item(12)
                'Me.txtPrem_Rate_Code.Text = oAL.Item(14)
                'Me.txtPrem_Period_Yr.Text = oAL.Item(19)
                If Trim(oAL.Item(20).ToString) <> "" Then
                    'GenEnd_Date = CDate(oAL.Item(20).ToString)
                    myarrData = Split(Trim(oAL.Item(20).ToString), "/")
                    GenStart_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                    If Trim(Me.txtStartDate.Text) = "" Then
                        Me.txtStartDate.Text = Format(GenStart_Date, "dd/MM/yyyy")
                    End If
                End If
                If Trim(oAL.Item(21).ToString) <> "" Then
                    'GenEnd_Date = CDate(oAL.Item(21).ToString)
                    myarrData = Split(Trim(oAL.Item(21).ToString), "/")
                    GenEnd_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                    If Trim(Me.txtEndDate.Text) = "" Then
                        Me.txtEndDate.Text = Format(GenEnd_Date, "dd/MM/yyyy")
                    End If
                    If Trim(Me.txtRWDate.Text) = "" And Trim(Me.txtEndDate.Text) <> "" Then
                        Me.txtRWDate.Text = Format(DateAdd(DateInterval.Day, 1, GenEnd_Date), "dd/MM/yyyy")
                    End If

                End If

                Me.txtInsuredName.Text = oAL.Item(26)
                Me.txtInsuredNum.Text = oAL.Item(28)
                If Trim(Me.txtAgcyNum.Text) = "" Then
                    Me.txtAgcyNum.Text = oAL.Item(29)
                End If

                ' get list of batches in the policy
                Call Proc_Batch()

            Else
                '    'Destroy i.e remove the array list object from memory
                '    Response.Write("<br/>Status: " & oAL.Item(0))
                Me.lblMessage.Text = "Status: " & oAL.Item(1)

                Me.txtFileNum.Text = ""
                Me.txtQuote_Num.Text = ""
                Me.txtPolNum.Text = ""
                Me.txtInsuredName.Text = ""
                Me.txtRiskNum.Text = ""
                Me.txtSubRiskNum.Text = ""
                'Me.txtStartDate.Text = ""
                'Me.txtEndDate.Text = ""
                'Me.txtRWDate.Text = ""

                'Me.lblMessage.Text = "Invalid Policy Number. Please enter valid policy number..."
                'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                FirstMsg = "Javascript:alert('" & "Unable to get policy details..." & "')"
            End If
            oAL = Nothing
        End If



    End Sub

    Protected Sub DoProc_Insured_Search()
        If LTrim(RTrim(Me.txtInsuredName.Text)) <> "" Then
            Call gnProc_Populate_Box("GL_ASSURED_HELP_SP_DNCN", "001", Me.cboInsuredName, RTrim(Me.txtInsuredName.Text))
            'Call gnProc_Populate_Box("GL_ASSURED_HELP_SP", "001", Me.cboInsuredName, RTrim(Me.txtInsuredName.Text))
            'Call gnProc_Populate_Box("GL_ASSURED_LIST", "001", Me.cboInsuredName, RTrim(Me.txtInsuredName.Text))
        End If

    End Sub

    Protected Sub DoProc_Validate_Broker()
        If Trim(Me.txtAgcyNum.Text) = "" Then
            Me.txtAgcyName.Text = ""
        Else
            'blnStatus = gnValidate_Codes("BROKER_UND_LIFE", Me.txtAgcyNum, Me.txtAgcyName)
            blnStatus = gnValidate_Codes("BROKER_UND_LIFE", Me.txtAgcyNum, Me.txtBroker_Search)
            If blnStatus = False Then
                Me.lblMessage.Text = "Invalid Broker or Agent Code: " & Me.txtAgcyNum.Text
                Me.txtAgcyNum.Text = ""
                Me.txtAgcyNum.Text = ""
                'Me.lblMsg.Text = "<script type='text/javascript'>myShowDialogue('" & strParam1 & "','" & strParam2 & "'" & ");</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMessage.Text & "');", True)
            End If
        End If

    End Sub

    Protected Sub DoProc_Validate_Policy()
        If RTrim(Me.txtPolNum.Text) = "" Then
            Me.txtInsuredNum.Text = ""
            Me.txtInsuredName.Text = ""
            Me.txtFileNum.Text = ""
            Me.txtQuote_Num.Text = ""
            'Me.txtPolNum.Text = ""
            Me.txtInsuredName.Text = ""
            Me.txtRiskNum.Text = ""
            Me.txtSubRiskNum.Text = ""
        Else
            'blnStatusX = gnValidate_Codes("POLICY", Me.txtPolNum, Me.txtInsuredName)

            strP_ID = RTrim(Me.txtPolNum.Text)
            Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_GL_POLICY_BY_POLICY_NO", RTrim(strP_ID), RTrim(""), RTrim(""))
            If oAL.Item(0) = "TRUE" Then
                '    'Retrieve the record
                '    Response.Write("<br/>Status: " & oAL.Item(0))
                '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
                Me.txtFileNum.Text = oAL.Item(2)
                Me.txtQuote_Num.Text = oAL.Item(3)
                Me.txtPolNum.Text = oAL.Item(4)
                Me.txtRiskNum.Text = oAL.Item(5)
                Me.txtSubRiskNum.Text = oAL.Item(6)
                'Me.txtPrem_Rate_TypeNum.Text = oAL.Item(12)
                'Me.txtPrem_Rate_Code.Text = oAL.Item(14)
                'Me.txtPrem_Period_Yr.Text = oAL.Item(19)
                If Trim(oAL.Item(20).ToString) <> "" Then
                    'GenEnd_Date = CDate(oAL.Item(20).ToString)
                    myarrData = Split(Trim(oAL.Item(20).ToString), "/")
                    GenStart_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                    If Trim(Me.txtStartDate.Text) = "" Then
                        Me.txtStartDate.Text = Format(GenStart_Date, "dd/MM/yyyy")
                    End If
                End If
                If Trim(oAL.Item(21).ToString) <> "" Then
                    'GenEnd_Date = CDate(oAL.Item(21).ToString)
                    myarrData = Split(Trim(oAL.Item(21).ToString), "/")
                    GenEnd_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                    If Trim(Me.txtEndDate.Text) = "" Then
                        Me.txtEndDate.Text = Format(GenEnd_Date, "dd/MM/yyyy")
                    End If
                    If Trim(Me.txtRWDate.Text) = "" And Trim(Me.txtEndDate.Text) <> "" Then
                        Me.txtRWDate.Text = Format(DateAdd(DateInterval.Day, 1, GenEnd_Date), "dd/MM/yyyy")
                    End If

                End If

                Me.txtInsuredName.Text = oAL.Item(26)
                Me.txtInsuredNum.Text = oAL.Item(28)
                If Trim(Me.txtAgcyNum.Text) = "" Then
                    Me.txtAgcyNum.Text = oAL.Item(29)
                End If
                Me.txtAgcyRate.Text = oAL.Item(32).ToString

                ' get list of batches in the policy
                Call Proc_Batch()

            Else
                '    'Destroy i.e remove the array list object from memory
                '    Response.Write("<br/>Status: " & oAL.Item(0))
                Me.lblMessage.Text = "Status: " & oAL.Item(1)

                Me.txtFileNum.Text = ""
                Me.txtQuote_Num.Text = ""
                Me.txtPolNum.Text = ""
                Me.txtInsuredName.Text = ""
                Me.txtRiskNum.Text = ""
                Me.txtSubRiskNum.Text = ""
                'Me.txtStartDate.Text = ""
                'Me.txtEndDate.Text = ""
                'Me.txtRWDate.Text = ""

                'Me.lblMessage.Text = "Invalid Policy Number. Please enter valid policy number..."
                'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                FirstMsg = "Javascript:alert('" & "Unable to get policy details..." & "')"
            End If
            oAL = Nothing
        End If

    End Sub

    Protected Sub DoProc_Validate_Policy_TEST()
        If Me.txtPolNum.Text = "TEST" Then
            Me.txtFileNum.Text = "GF/2014/1201/G/G001/G/0000001"
            Me.txtQuote_Num.Text = "GQ/2014/1201/G/G001/G/0000001"
            Me.txtPolNum.Text = "GP/2014/1201/G/G001/G/0000001"
            Me.txtInsuredNum.Text = "DC00015"
            Me.txtRiskNum.Text = "G"
            Me.txtSubRiskNum.Text = "G001"
            Me.txtAgcyNum.Text = "BR00001"

            '   EDISON CHOUEST OFFSHORE NIGERIA LIMITED
            Me.cboInsuredName.Items.Add(New ListItem("EDISON CHOUEST OFFSHORE NIGERIA LIMITED", "GP/2014/1201/G/G001/G/0000001"))
            Me.cboInsuredName.Items.Insert(0, New ListItem("(select item)", "*"))
            Me.cboInsuredName.SelectedIndex = 1

            '   SCIB INSURANCE BROKERS
            Me.cboAgcyName.Items.Add(New ListItem("SCIB INSURANCE BROKERS", "BR00001"))
            Me.cboAgcyName.Items.Insert(0, New ListItem("(select item)", "*"))
            Me.cboAgcyName.SelectedIndex = 1

            Call Proc_Batch()
            Exit Sub
        End If


        If RTrim(Me.txtPolNum.Text) = "" Then
            Me.txtInsuredNum.Text = ""
            Me.txtInsuredName.Text = ""
            Me.txtFileNum.Text = ""
            Me.txtQuote_Num.Text = ""
            'Me.txtPolNum.Text = ""
            Me.txtInsuredName.Text = ""
            Me.txtRiskNum.Text = ""
            Me.txtSubRiskNum.Text = ""
        Else
            'blnStatusX = gnValidate_Codes("POLICY", Me.txtPolNum, Me.txtInsuredName)

            strP_ID = RTrim(Me.txtPolNum.Text)
            Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_GL_POLICY_BY_POLICY_NO", RTrim(strP_ID), RTrim(txtPolNum.Text), RTrim(""))
            If oAL.Item(0) = "TRUE" Then
                '    'Retrieve the record
                '    Response.Write("<br/>Status: " & oAL.Item(0))
                '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
                Me.txtFileNum.Text = oAL.Item(2)
                Me.txtQuote_Num.Text = oAL.Item(3)
                Me.txtPolNum.Text = oAL.Item(4)
                Me.txtRiskNum.Text = oAL.Item(5)
                Me.txtSubRiskNum.Text = oAL.Item(6)
                'Me.txtPrem_Rate_TypeNum.Text = oAL.Item(12)
                'Me.txtPrem_Rate_Code.Text = oAL.Item(14)
                'Me.txtPrem_Period_Yr.Text = oAL.Item(19)
                If Trim(oAL.Item(20).ToString) <> "" Then
                    'GenEnd_Date = CDate(oAL.Item(20).ToString)
                    myarrData = Split(Trim(oAL.Item(20).ToString), "/")
                    GenStart_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                    If Trim(Me.txtStartDate.Text) = "" Then
                        Me.txtStartDate.Text = Format(GenStart_Date, "dd/MM/yyyy")
                    End If
                End If
                If Trim(oAL.Item(21).ToString) <> "" Then
                    'GenEnd_Date = CDate(oAL.Item(21).ToString)
                    myarrData = Split(Trim(oAL.Item(21).ToString), "/")
                    GenEnd_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                    If Trim(Me.txtEndDate.Text) <> "" Then
                        Me.txtEndDate.Text = Format(GenEnd_Date, "dd/MM/yyyy")
                    End If
                    If Trim(Me.txtRWDate.Text) <> "" Then
                        Me.txtRWDate.Text = Format(DateAdd(DateInterval.Day, 1, GenStart_Date), "dd/MM/yyyy")
                    End If

                End If

                ' get list of batches in the policy
                Call Proc_Batch()

            Else
                '    'Destroy i.e remove the array list object from memory
                '    Response.Write("<br/>Status: " & oAL.Item(0))
                Me.lblMessage.Text = "Status: " & oAL.Item(1)

                Me.txtFileNum.Text = ""
                Me.txtQuote_Num.Text = ""
                Me.txtPolNum.Text = ""
                Me.txtInsuredName.Text = ""
                Me.txtRiskNum.Text = ""
                Me.txtSubRiskNum.Text = ""
                'Me.txtStartDate.Text = ""
                'Me.txtEndDate.Text = ""
                'Me.txtRWDate.Text = ""

                'Me.lblMessage.Text = "Invalid Policy Number. Please enter valid policy number..."
                'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                FirstMsg = "Javascript:alert('" & "Unable to get policy details..." & "')"
            End If
            oAL = Nothing
        End If

    End Sub

    Private Function Proc_OpenRecord(ByVal pvCODE As String, ByVal strRefNo As String) As String

        strErrMsg = "false"

        lblMessage.Text = ""
        If Trim(strRefNo) = "" Then
            Proc_OpenRecord = strErrMsg
            Return Proc_OpenRecord
        End If


        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Return strErrMsg
            Exit Function
        End Try

        strTable = strTableName
        strREC_ID = Trim(strRefNo)


        strSQL = ""
        strSQL = "SPGL_GET_DNCN_INFO"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        'objOLECmd.CommandType = CommandType.Text
        objOLECmd.CommandType = CommandType.StoredProcedure
        objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 40).Value = RTrim(pvCODE)
        objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = strREC_ID
        objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 40).Value = strREC_ID
        objOLECmd.Parameters.Add("p04", OleDbType.VarChar, 40).Value = strREC_ID

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then

            strProcDate = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_DCN_PROC_DATE") & vbNullString, String))
            Me.txtSecNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_BATCH_NO") & vbNullString, String))
            Call Proc_DDL_Get(Me.cboSecName, RTrim(Me.txtSecNum.Text))
            'Me.txtSecName.Text = RTrim(CType(objOLEDR("CTBS_LONG_DESCR") & vbNullString, String))

            strSerialNum = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_SER_NO") & vbNullString, String))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_REC_ID") & vbNullString, String))

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_FILE_NO") & vbNullString, String))
            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_PROP_NO") & vbNullString, String))
            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_POLY_NO") & vbNullString, String))
            Me.txtInsuredNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_INSRD_CODE") & vbNullString, String))
            'Me.txtInsuredName.Text = RTrim(CType(objOLEDR("INSURED_NAME") & vbNullString, String))

            'Me.txtUWRYear.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_UNDW_YR") & vbNullString, String))

            Me.txtRiskNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_PRDCT_CAT") & vbNullString, String))
            Me.txtSubRiskNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_PRDCT_CD") & vbNullString, String))
            'Me.txtSubRiskName.Text = RTrim(CType(objOLEDR("CTSUBRISK_DESCR") & vbNullString, String))


            Me.txtMemberBatchNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_MEMB_BATCH_NO") & vbNullString, String))
            Call Proc_DDL_Get(Me.cboMemberBatchNum, RTrim(Me.txtMemberBatchNum.Text))

            Me.txtTransCode.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_DB_CR_CD") & vbNullString, String))
            Call Proc_DDL_Get(Me.cboTransCode, RTrim(Me.txtTransCode.Text))

            Me.txtBraNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_BRANCH_NO") & vbNullString, String))
            Call Proc_DDL_Get(Me.cboBranchName, RTrim(Me.txtBraNum.Text))
            'Me.txtLocNum.Text = RTrim(CType(objOLEDR("DNCN_LOC_NUM") & vbNullString, String))
            'Me.txtBraName.Text = RTrim(CType(objOLEDR("CTBRA_NAME") & vbNullString, String))

            Me.txtTransType.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_DB_CR_TYP") & vbNullString, String))
            Call Proc_DDL_Get(Me.cboTransType, RTrim(Me.txtTransType.Text))

            If IsDate(objOLEDR("TBIL_POL_PRM_DCN_BILL_DATE")) Then
                Me.txtTransDate.Text = Format(CType(objOLEDR("TBIL_POL_PRM_DCN_BILL_DATE"), Date), "dd/MM/yyyy")
            Else
                Me.txtTransDate.Text = ""
            End If

            Me.txtTransNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_TRANS_NO") & vbNullString, String))

            If IsDate(objOLEDR("TBIL_POL_PRM_DCN_REF_DATE")) Then
                Me.txtRefDate.Text = Format(CType(objOLEDR("TBIL_POL_PRM_DCN_REF_DATE"), Date), "dd/MM/yyyy")
            Else
                Me.txtRefDate.Text = ""
            End If
            Me.txtRefNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_REF_NO") & vbNullString, String))
            'Me.txtRefCode.Text = RTrim(CType(objOLEDR("DNCN_REFCODE") & vbNullString, String))

            Me.txtBusType.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_SOURCE") & vbNullString, String))
            Call Proc_DDL_Get(Me.cboBusType, RTrim(Me.txtBusType.Text))

            If IsDate(objOLEDR("TBIL_POL_PRM_DCN_POL_ST_DT")) Then
                Me.txtStartDate.Text = Format(CType(objOLEDR("TBIL_POL_PRM_DCN_POL_ST_DT"), Date), "dd/MM/yyyy")
            Else
                Me.txtStartDate.Text = ""
            End If
            If IsDate(objOLEDR("TBIL_POL_PRM_DCN_POL_END_DT")) Then
                Me.txtEndDate.Text = Format(CType(objOLEDR("TBIL_POL_PRM_DCN_POL_END_DT"), Date), "dd/MM/yyyy")
            Else
                Me.txtEndDate.Text = ""
            End If
            If IsDate(objOLEDR("TBIL_POL_PRM_DCN_POL_RW_DT")) Then
                Me.txtRWDate.Text = Format(CType(objOLEDR("TBIL_POL_PRM_DCN_POL_RW_DT"), Date), "dd/MM/yyyy")
            Else
                Me.txtRWDate.Text = ""
            End If


            Me.txtAgcyNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_BRK_CODE") & vbNullString, String))
            Call Proc_DDL_Get(Me.cboAgcyName, RTrim(Me.txtAgcyNum.Text))
            'Me.txtAgcyType.Text = RTrim(CType(objOLEDR("DNCN_BUS_SOURCE") & vbNullString, String))
            'Me.txtAgcyName.Text = RTrim(CType(objOLEDR("CTAGCY_NAME") & vbNullString, String))


            Me.txtAgcyRate.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_COMM_RT") & vbNullString, String)

            Me.txtTrans_Rate.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_COY_SHARE") & vbNullString, String)
            Me.txtTrans_Full_SI.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_FULL_SA") & vbNullString, String)
            Me.txtTrans_Full_Prem.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_FULL_PREM") & vbNullString, String)

            Me.txtSumIns.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_SA_LC") & vbNullString, String)
            Me.txtGrsPrem.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_AMT_LC") & vbNullString, String)

            'Me.txtTransAmt.Text = CType(objOLEDR("DNCN_TRANS_AMT") & vbNullString, String)

            Me.txtProRataNDay.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_PERIOD_DAYS") & vbNullString, String)
            Me.txtProRataRDay.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_PRO_RATA_DAYS") & vbNullString, String)
            Me.txtTransAmt.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_PRO_RATA_AMT") & vbNullString, String)

            If Val(Me.txtProRataNDay.Text) <> 0 Then
                Me.chkProrataYN.Checked = True
                Me.lblProRataNDay.Enabled = True
                Me.txtProRataNDay.Enabled = True
                Me.lblProRataRDay.Enabled = True
                Me.txtProRataRDay.Enabled = True
            Else
                Me.chkProrataYN.Checked = False
                Me.lblProRataNDay.Enabled = False
                Me.txtProRataNDay.Enabled = False
                Me.lblProRataRDay.Enabled = False
                Me.txtProRataRDay.Enabled = False
            End If

            Me.txtTransDescr1.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_DESC") & vbNullString, String)
            'Me.txtTransDescr2.Text = CType(objOLEDR("DNCN_DESCR2") & vbNullString, String)

            'Me.txtTreatyRef_Num.Text = RTrim(CType(objOLEDR("DNCN_TREATY_NUM") & vbNullString, String))
            'X = RTrim(CType(objOLEDR("DNCN_TREATY_SW") & vbNullString, String))
            'Me.txtTreatyRef_Descr.Text = RTrim(CType(objOLEDR("DNCN_WK_DESCR") & vbNullString, String))

            Me.txtFacBal_SI.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_FAC_AMT") & vbNullString, String)
            Me.txtFacBal_Rate.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_FAC_RT") & vbNullString, String)


            If Not IsDBNull(objOLEDR("TBIL_POL_PRM_DCN_TRTY_AMT")) Then
                Me.txtRet_SI.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_TRTY_AMT") & vbNullString, String)
            End If
            If Not IsDBNull(objOLEDR("TBIL_POL_PRM_DCN_TRTY_RT")) Then
                Me.txtRet_Rate.Text = CType(objOLEDR("TBIL_POL_PRM_DCN_TRTY_RT") & vbNullString, String)
            End If

            'If Not IsDBNull(objOLEDR("TREATY_LC_SI")) Then
            '    Me.txtLC_SI.Text = CType(objOLEDR("TREATY_LC_SI") & vbNullString, String)
            'End If
            'If Not IsDBNull(objOLEDR("TREATY_LC_RATE")) Then
            '    Me.txtLC_Rate.Text = CType(objOLEDR("TREATY_LC_RATE") & vbNullString, String)
            'End If

            'If Not IsDBNull(objOLEDR("TREATY_SURP1_SI")) Then
            '    Me.txtSurp1_SI.Text = CType(objOLEDR("TREATY_SURP1_SI") & vbNullString, String)
            'End If
            'If Not IsDBNull(objOLEDR("TREATY_SURP1_RATE")) Then
            '    Me.txtSurp1_Rate.Text = CType(objOLEDR("TREATY_SURP1_RATE") & vbNullString, String)
            'End If
            'If Not IsDBNull(objOLEDR("TREATY_SURP2_SI")) Then
            '    Me.txtSurp2_SI.Text = CType(objOLEDR("TREATY_SURP2_SI") & vbNullString, String)
            'End If
            'If Not IsDBNull(objOLEDR("TREATY_SURP2_RATE")) Then
            '    Me.txtSurp2_Rate.Text = CType(objOLEDR("TREATY_SURP2_RATE") & vbNullString, String)
            'End If

            'If Not IsDBNull(objOLEDR("TREATY_QUOTA_SI")) Then
            '    Me.txtQuota_SI.Text = CType(objOLEDR("TREATY_QUOTA_SI") & vbNullString, String)
            'End If
            'If Not IsDBNull(objOLEDR("TREATY_QUOTA_RATE")) Then
            '    Me.txtQuota_Rate.Text = CType(objOLEDR("TREATY_QUOTA_RATE") & vbNullString, String)
            'End If

            'Me.cbControlAccount.SelectedIndex = Me.cbControlAccount.Items.IndexOf(Me.cbControlAccount.Items.FindByValue(CType(objOLEDR("ControlAcctID") & vbNullString, String)))

            Call DisableBox(Me.txtTransNum)
            Me.chkTransum.Enabled = False
            Me.cmdTransNum.Enabled = False

            strErrMsg = "Status: Data Modification"
            strOPT = "1"
            Me.cmdNew_ASP.Enabled = True
            Me.cmdDelete_ASP.Enabled = True
        Else
            Me.cmdDelete_ASP.Enabled = False
            'strErrMsg = "Status: New Entry..."

            'Me.lblMessage.Text = "Invalid/Incorrect " & Me.lblTransNum.Text & " " & Me.txtTransNum.Text
            Me.lblMessage.Text = "Error!. Record not found..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
            Me.txtTransNum.Text = ""

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

        lblMessage.Text = strErrMsg
        Proc_OpenRecord = strErrMsg

        If Trim(Me.txtPolNum.Text) <> "" Then
            Call Proc_Batch()
            If Trim(Me.txtMemberBatchNum.Text) <> "" Then
                'Call DoGet_SelectedItem(Me.cboMemberBatchNum, Me.txtMemberBatchNum, Me.txtMemberBatchName, Me.lblMessage)
                Call Proc_DDL_Get(Me.cboMemberBatchNum, RTrim(Me.txtMemberBatchNum.Text))
            End If
        End If

        Return Proc_OpenRecord


    End Function

    Private Sub DisableBox(ByVal objTxtBox As TextBox)
        Dim c As System.Drawing.Color = Drawing.Color.LightGray
        objTxtBox.ReadOnly = True
        objTxtBox.Enabled = False
        'objTxtBox.BackColor = c

    End Sub

    Private Sub Proc_Batch()
        'Me.cmdDelItem.Enabled = True

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            Me.lblMessage.Text = ex.Message.ToString
            objOLEConn = Nothing
        End Try


        Dim pvFT As String = ""
        Dim pvCNT As Integer = 0
        Dim pvBatNum As String = ""

        Dim pvListItem As ListItem

        pvFT = "Y"
        pvCNT = 0
        pvBatNum = ""

        strF_ID = RTrim(Me.txtFileNum.Text)
        strQ_ID = RTrim(Me.txtQuote_Num.Text)

        Me.cboMemberBatchNum.Items.Clear()


        strTable = strTableName
        strTable = "TBIL_GRP_POLICY_MEMBERS"
        strSQL = ""
        strSQL = strSQL & "SELECT DISTINCT TBIL_POL_MEMB_BATCH_NO"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " WHERE TBIL_POL_MEMB_FILE_NO = '" & RTrim(strF_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_MEMB_PROP_NO = '" & RTrim(strQ_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_MEMB_MDLE IN('G')"
        strSQL = strSQL & " ORDER BY TBIL_POL_MEMB_BATCH_NO"

        Dim objMem_Cmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        'objMem_Cmd.CommandTimeout = 180
        objMem_Cmd.CommandType = CommandType.Text

        Dim objMem_DR As OleDbDataReader

        Try

            objMem_DR = objMem_Cmd.ExecuteReader()

            Do While objMem_DR.Read()

                If UCase(Trim(pvFT)) = "Y" Then
                    pvFT = "N"
                    If Trim(Me.txtMemberBatchNum.Text) = "" Then
                        pvBatNum = RTrim(CType(objMem_DR("TBIL_POL_MEMB_BATCH_NO") & vbNullString, String))
                    End If
                End If

                pvCNT = pvCNT + 1

                pvListItem = New ListItem
                pvListItem.Value = RTrim(CType(objMem_DR("TBIL_POL_MEMB_BATCH_NO") & vbNullString, String))
                pvListItem.Text = RTrim(CType(objMem_DR("TBIL_POL_MEMB_BATCH_NO") & vbNullString, String))
                Me.cboMemberBatchNum.Items.Add(pvListItem)
            Loop

            Me.cboMemberBatchNum.Items.Insert(0, New ListItem("(select)", "0"))

            If Val(pvCNT) >= 1 Then
                objMem_DR.Close()
                objMem_Cmd.Dispose()
            End If

            objMem_DR = Nothing
            objMem_Cmd = Nothing

        Catch ex As Exception

        End Try

        If Val(pvCNT) = 1 Then
            'Me.txtBatch_Num.Text = RTrim(pvBatNum)
        Else
            'Me.txtBatch_Num.Text = RTrim("")
        End If

        objMem_DR = Nothing
        objMem_Cmd = Nothing


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


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



    Private Function Proc_Get_Ref(ByVal pvRef_RefNum As String, ByVal pvRef_PolNum As String, ByVal pvRef_AgcyNum As String) As Boolean

        Dim blnRet As Boolean
        blnRet = False

        dteRef = dteTrans

        strTable = strTableName

        gnSQL = ""
        gnSQL = gnSQL & "select TBIL_POL_PRM_DCN_TRANS_NO,TBIL_POL_PRM_DCN_DB_CR_CD,TBIL_POL_PRM_DCN_BILL_DATE"
        gnSQL = gnSQL & ",TBIL_POL_PRM_DCN_POLY_NO,TBIL_POL_PRM_DCN_BRK_CODE from " & strTable
        gnSQL = gnSQL & " where TBIL_POL_PRM_DCN_TRANS_NO = '" & RTrim(pvRef_RefNum) & "'"
        gnSQL = gnSQL & " and TBIL_POL_PRM_DCN_DB_CR_TYP not in('B','O','I')"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Dim objOLECmd As OleDbCommand = New OleDbCommand(gnSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Dim objOLEDR As OleDbDataReader

        'open connection to database
        objOLEConn.Open()

        objOLEDR = objOLECmd.ExecuteReader()
        Do While objOLEDR.Read
            If RTrim(pvRef_PolNum) = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_POLY_NO") & vbNullString, String)) And _
               RTrim(pvRef_AgcyNum) = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_BRK_CODE") & vbNullString, String)) Then
                Me.txtRefCode.Text = RTrim(objOLEDR("TBIL_POL_PRM_DCN_DB_CR_CD") & vbNullString)
                Me.txtRefDate.Text = Format(CType(objOLEDR("TBIL_POL_PRM_DCN_BILL_DATE"), Date), "MM/dd/yyyy")
                dteRef = Format(CType(objOLEDR("TBIL_POL_PRM_DCN_BILL_DATE"), Date), "MM/dd/yyyy")
                blnRet = True
                Exit Do
            Else
            End If

        Loop

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


        Return blnRet


    End Function

    Private Sub Proc_Get_SA_Prem()
        If Trim(Me.txtPolNum.Text) = "" Then
            Exit Sub
        End If
        If Trim(Me.txtMemberBatchNum.Text) = "" Then
            Exit Sub
        End If

        Dim blnRet As Boolean
        blnRet = False

        dteRef = dteTrans

        strTable = strTableName
        strTable = "TBIL_GRP_POLICY_PREM_DETAILS"

        gnSQL = ""
        gnSQL = gnSQL & "select TOP 1 TBIL_POL_PRM_DTL_SA_LC, TBIL_POL_PRM_DTL_LIFE_COVER_SA_LC"
        gnSQL = gnSQL & ",TBIL_POL_PRM_DTL_BASIC_PRM_LC, TBIL_POL_PRM_DTL_ADDPREM_LC"
        gnSQL = gnSQL & ",TBIL_POL_PRM_DTL_LOADING_LC, TBIL_POL_PRM_DTL_DISCNT_LC"
        gnSQL = gnSQL & ",TBIL_POL_PRM_DTL_CHG_LC, TBIL_POL_PRM_DTL_MOP_PRM_LC"
        gnSQL = gnSQL & ",TBIL_POL_PRM_DTL_TOT_PRM_LC, TBIL_POL_PRM_DTL_ANN_PREM_LC"
        gnSQL = gnSQL & ",TBIL_POL_PRM_DTL_MOP_CONTRB_LC, TBIL_POL_PRM_DTL_ANN_CONTRB_LC"
        gnSQL = gnSQL & ",TBIL_POL_PRM_DTL_FIRST_PRM_LC, TBIL_POL_PRM_DTL_NET_PRM_LC"
        gnSQL = gnSQL & " from " & strTable
        gnSQL = gnSQL & " where TBIL_POL_PRM_DTL_POLY_NO = '" & RTrim(Me.txtPolNum.Text) & "'"
        gnSQL = gnSQL & " and TBIL_POL_PRM_DTL_FILE_NO = '" & RTrim(Me.txtFileNum.Text) & "'"
        gnSQL = gnSQL & " and TBIL_POL_PRM_DTL_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
        gnSQL = gnSQL & " and TBIL_POL_PRM_DTL_MEMB_BATCH_NO = '" & RTrim(Me.txtMemberBatchNum.Text) & "'"
        gnSQL = gnSQL & " and TBIL_POL_PRM_DTL_MDLE in('G','GRP')"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Dim objOLECmd As OleDbCommand = New OleDbCommand(gnSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Dim objOLEDR As OleDbDataReader

        'open connection to database
        objOLEConn.Open()

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            If _strGrossPrem.Trim() = "" Then 'If gross prem is not sent accross the wire, grab the SA and Prem from the DB
                Me.txtTrans_Full_SI.Text = CType(objOLEDR("TBIL_POL_PRM_DTL_SA_LC") & vbNullString, String)
                Me.txtTrans_Full_Prem.Text = CType(objOLEDR("TBIL_POL_PRM_DTL_TOT_PRM_LC") & vbNullString, String)
            End If
            blnRet = True
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


        'Return blnRet

    End Sub

    Protected Sub cmdTrans_Calculation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTrans_Calculation.Click
        Call Proc_Update_SA_Prem()
    End Sub

    Private Sub Proc_Update_SA_Prem()

        Dim Tmp_Amt As Double = 0

        gnSI_Amt = 0
        gnSI_Rate = 0
        gnPrem_Amt = 0
        gnPrem_Rate = 0

        If IsNumeric(Trim(Me.txtTrans_Rate.Text)) Then
            gnSI_Rate = CDbl(Trim(Me.txtTrans_Rate.Text))
        End If
        If IsNumeric(Trim(Me.txtTrans_Full_SI.Text)) Then
            gnSI_Amt = CDbl(Trim(Me.txtTrans_Full_SI.Text))
        End If
        If IsNumeric(Trim(Me.txtTrans_Full_Prem.Text)) Then
            gnPrem_Amt = CDbl(Trim(Me.txtTrans_Full_Prem.Text))
        End If

        Select Case Trim(Me.txtTransType.Text)
            Case "I", "C", "D"

                'Sum insured
                Tmp_Amt = 0
                If gnSI_Amt > 0 And gnSI_Rate > 0 Then
                    Tmp_Amt = gnSI_Amt * gnSI_Rate / 100
                End If
                If Trim(Me.txtSumIns.Text) = "" Or _
                   Val(Trim(Me.txtSumIns.Text)) = 0 Then
                    Me.txtSumIns.Text = Format(Tmp_Amt, "#########0.00")
                End If

                'Gross premium
                Tmp_Amt = 0
                If gnPrem_Amt > 0 And gnSI_Rate > 0 Then
                    Tmp_Amt = gnPrem_Amt * gnSI_Rate / 100
                End If
                If Trim(Me.txtGrsPrem.Text) = "" Or _
                   Val(Trim(Me.txtGrsPrem.Text)) = 0 Then
                    Me.txtGrsPrem.Text = Format(Tmp_Amt, "#########0.00")
                End If

            Case Else
                If Val(Me.txtTrans_Rate.Text) = 100 Then
                    Me.txtSumIns.Text = Me.txtTrans_Full_SI.Text
                    Me.txtGrsPrem.Text = Me.txtTrans_Full_Prem.Text
                Else
                    'Sum insured
                    Tmp_Amt = 0
                    If gnSI_Amt > 0 And gnSI_Rate > 0 Then
                        Tmp_Amt = gnSI_Amt * gnSI_Rate / 100
                    End If
                    If Trim(Me.txtSumIns.Text) = "" Or _
                       Val(Trim(Me.txtSumIns.Text)) = 0 Then
                        Me.txtSumIns.Text = Format(Tmp_Amt, "#########0.00")
                    End If

                    'Gross premium
                    Tmp_Amt = 0
                    If gnPrem_Amt > 0 And gnSI_Rate > 0 Then
                        Tmp_Amt = gnPrem_Amt * gnSI_Rate / 100
                    End If
                    If Trim(Me.txtGrsPrem.Text) = "" Or _
                       Val(Trim(Me.txtGrsPrem.Text)) = 0 Then
                        Me.txtGrsPrem.Text = Format(Tmp_Amt, "#########0.00")
                    End If

                End If

        End Select


    End Sub

    Protected Sub cboTransType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTransType.SelectedIndexChanged

    End Sub

    Protected Sub cmdGetBatchList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetBatchList.Click

    End Sub

    Protected Sub DoProc_Insured_Search(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdInsuredSearch.Click

    End Sub
    Protected Sub GetPolicyDetails()
        Dim e As EventArgs = Nothing
        'Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_GL_POLICY_BY_POLICY_NO", RTrim(txtPolNum.Text), RTrim(""), RTrim(""))
        'If oAL.Item(0) = "TRUE" Then
        '    Me.txtQuote_Num.Text = oAL.Item(3)
        '    Me.txtPolNum.Text = oAL.Item(4)
        '    If Trim(oAL.Item(20).ToString) <> "" Then
        '        'GenEnd_Date = CDate(oAL.Item(20).ToString)
        '        myarrData = Split(Trim(oAL.Item(20).ToString), "/")
        '        GenStart_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
        '        Me.txtStartDate.Text = Format(GenStart_Date, "dd/MM/yyyy")
        '    End If
        '    If Trim(oAL.Item(21).ToString) <> "" Then
        '        'GenEnd_Date = CDate(oAL.Item(21).ToString)
        '        myarrData = Split(Trim(oAL.Item(21).ToString), "/")
        '        GenEnd_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
        '        Me.txtEndDate.Text = Format(GenEnd_Date, "dd/MM/yyyy")
        '    End If
        '    Me.txtAgcyNum.Text = oAL.Item(29)
        '    Me.txtRWDate.Text = oAL.Item(21)
        '    Me.txtAgcyRate.Text = oAL.Item(32)
        DoProc_Validate_Policy()
        Me.txtTrans_Rate.Text = "100"
        'txtMemberBatchNum.Text = 1
        If Trim(Me.txtMemberBatchNum.Text) <> "" And Trim(Me.txtPolNum.Text) <> "" Then
            Proc_Batch()
            Call Proc_Get_SA_Prem()
        End If

        cmdTrans_Calculation_Click(Nothing, e) ' execute the procedure to calculate portion of biz

        'End If

    End Sub

    Protected Sub cboInsuredName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInsuredName.SelectedIndexChanged

    End Sub

    Protected Sub cboBranchName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBranchName.SelectedIndexChanged

    End Sub

    Protected Sub chkProrataYN_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkProrataYN.CheckedChanged

    End Sub

    Protected Sub cmdDelete_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete_ASP.Click

    End Sub
End Class
