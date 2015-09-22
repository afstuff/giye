Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Partial Class Policy_PRG_LI_GRP_POLY_CONVERT
    Inherits System.Web.UI.Page


    Protected FirstMsg As String
    Protected PageLinks As String

    Protected STRMENU_TITLE As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strF_ID As String
    Protected strQ_ID As String
    Protected strP_ID As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Dim dteProc As Date

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strTmp_Value As String = ""

    Dim myarrData() As String

    Dim strErrMsg As String
    Protected strUpdate_Sw As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strTableName = "TBIL_POLICY_DET"
        strTableName = "TBIL_GRP_POLICY_DET"

        STRMENU_TITLE = UCase("+++ Convert Quotation to Policy +++ ")

        If Not (Page.IsPostBack) Then
            Call Proc_DoNew()

            Me.cmdFileNum.Enabled = True
            Me.BUT_OK.Enabled = False
            Me.txtPro_Pol_Num.Text = "GQ/2014/1201/G/G001/G/0000001"
            Me.txtFileNum.Text = "GF/2014/1201/G/G001/G/0000001"

            Me.txtPro_Pol_Num.Enabled = True
            Me.txtPro_Pol_Num.Focus()
        End If


        If Me.txtAction.Text = "New" Then
            Me.txtPro_Pol_Num.Text = ""
            Call Proc_DoNew()
            Me.txtAction.Text = ""
            Me.lblMsg.Text = "New Entry..."

            Me.txtPro_Pol_Num.Enabled = True
            Me.txtPro_Pol_Num.Focus()
        End If

    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtFileNum.Text = ""
                Me.txtPro_Pol_Num.Text = ""
                Me.txtPol_Num.Text = ""
                'Me.txtSearch.Value = ""
            Else
                Me.txtFileNum.Text = Me.cboSearch.SelectedItem.Value
                blnStatus = Proc_Validate(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
                If blnStatus = True Then
                    blnStatus = Proc_DoGet_Record(RTrim("PRO"), Trim(Me.txtPro_Pol_Num.Text), RTrim(Me.txtFileNum.Text))
                End If
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try

    End Sub

    Protected Sub cboBatch_Num_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBatch_Num.SelectedIndexChanged
        Call gnGET_SelectedItem(Me.cboBatch_Num, Me.txtBatch_Num, Me.txtBatch_Name, Me.lblMsg)

    End Sub

    Protected Sub cmdFileNum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFileNum.Click

        Dim xc As Integer = 0

        If Trim(Me.txtPro_Pol_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPro_Pol_Num.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(Me.txtPro_Pol_Num.Text)))
            If Mid(LTrim(RTrim(Me.txtPro_Pol_Num.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtPro_Pol_Num.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblPro_Pol_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                Exit Sub
            End If
        Next

        blnStatus = Proc_Validate(RTrim("PRO"), Me.txtPro_Pol_Num.Text, RTrim("0"))
        If blnStatus = False Then
            Exit Sub
        End If

        If Trim(Me.txtFileNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(Me.txtFileNum.Text)))
            If Mid(LTrim(RTrim(Me.txtFileNum.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtFileNum.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblFileNum.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                Exit Sub
            End If
        Next

        Me.lblPWD.Enabled = False
        Me.txtPWD.Enabled = False
        Me.BUT_OK.Enabled = False

        blnStatus = Proc_DoGet_Record(RTrim("PRO"), Trim(Me.txtPro_Pol_Num.Text), RTrim(Me.txtFileNum.Text))
        If blnStatus = True Then
            Call Proc_Batch()
            Me.chkAccept.Enabled = True
            'Me.BUT_OK.Enabled = True
            Exit Sub
        Else
            Me.chkAccept.Enabled = False
            Me.chkAccept.Checked = False
            Me.lblPWD.Enabled = False
            Me.txtPWD.Enabled = False
            Me.BUT_OK.Enabled = False
            Exit Sub
        End If

    End Sub

    Protected Sub chkAccept_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAccept.CheckedChanged
        If Me.chkAccept.Checked = True Then
            Me.lblPWD.Enabled = True
            Me.txtPWD.Enabled = True
            Me.BUT_OK.Enabled = True
        Else
            Me.lblPWD.Enabled = False
            Me.txtPWD.Enabled = False
            Me.BUT_OK.Enabled = False
        End If
    End Sub

    Protected Sub BUT_OK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
        Call Proc_DoConvert()

    End Sub

    Private Function MyCheck_Date(ByVal pvCODE As String, ByVal pvCtr_Date As TextBox, ByVal pvMessage As String, Optional ByVal pvCtr_Label As Label = Nothing) As String

        Dim pvRetVal As String = ""
        Dim pvMsg As String = ""

        pvRetVal = "false"
        pvMsg = pvMessage

        Dim blnX As Boolean = False

        Dim myarrData() As String

        Dim strMyYear As String = ""
        Dim strMyMth As String = ""
        Dim strMyDay As String = ""

        Dim strMyDte As String = ""
        Dim strMyDteX As String = ""

        Dim mydteX As String
        Dim mydte As Date

        Try

            Select Case Trim(pvCODE)
                Case "DATE"
                    If Trim(RTrim(RTrim(pvCtr_Date.Text))) = "" Then
                        pvRetVal = "false"
                        If pvCtr_Label IsNot Nothing Then
                            pvCtr_Label.Text = pvMsg
                        End If
                        ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & pvMsg & "');", True)
                        Return pvRetVal
                        Exit Function
                    End If

                    'Validate date
                    myarrData = Split(pvCtr_Date.Text, "/")
                    If myarrData.Count <> 3 Then
                        pvRetVal = "false"
                        If pvCtr_Label IsNot Nothing Then
                            pvCtr_Label.Text = pvMsg
                        End If
                        ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & pvMsg & "');", True)
                        Return pvRetVal
                        Exit Function
                    End If


                    strMyDay = myarrData(0)
                    strMyMth = myarrData(1)
                    strMyYear = myarrData(2)

                    strMyDay = CType(Format(Val(strMyDay), "00"), String)
                    strMyMth = CType(Format(Val(strMyMth), "00"), String)
                    strMyYear = CType(Format(Val(strMyYear), "0000"), String)

                    strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

                    blnX = MOD_GEN.gnTest_TransDate(strMyDte)
                    If blnX = False Then
                        pvMsg = "Incorrect date. Please enter valid date..."
                        pvRetVal = "false"
                        If pvCtr_Label IsNot Nothing Then
                            pvCtr_Label.Text = pvMsg
                        End If
                        ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & pvMsg & "');", True)
                        Return pvRetVal
                        Exit Function
                    End If

                    mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
                    mydte = Format(CDate(mydteX), "MM/dd/yyyy")
                    pvCtr_Date.Text = Format(mydte, "dd/MM/yyyy")

                    pvRetVal = "true=" & mydteX.ToString

                Case Else
                    pvRetVal = "false"
                    pvMsg = "Missing Required Data Type - DATE"
                    'System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
                    'System.Web.HttpContext.Current.Response.Write("alert(""" & pvMsg & """)" & vbCrLf)
                    'System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
                    ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & pvMsg & "');", True)
            End Select

        Catch ex As Exception
            If pvCtr_Label IsNot Nothing Then
                pvCtr_Label.Text = ex.Message
            End If

        End Try

        Return pvRetVal

    End Function

    Private Sub Proc_DoConvert()
        Dim xc As Integer = 0

        Dim myRetVal As String = ""
        Dim mydte As Date = Now

        dteProc = Now

        Dim myVal As String = ""

        Me.lblMsg.Text = "Status:"

        myRetVal = MyCheck_Date("DATE", Me.txtProc_Date, "Missing " & Me.lblProc_Date.Text, Me.lblMsg)
        If myRetVal = "false" Then
            Exit Sub
        End If
        mydte = Format(CDate(Mid(myRetVal, 6)), "MM/dd/yyyy")
        dteProc = Format(mydte, "MM/dd/yyyy")

        myVal = LTrim(RTrim(Me.txtPro_Pol_Num.Text))
        If Trim(myVal) = "" Or Trim(myVal) = "*" Or Trim(myVal) = "." Or Trim(myVal) = "?" Then
            Me.lblMsg.Text = "Missing input field or Invalid character found in input field - " & Me.lblPro_Pol_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(myVal)))
            If Mid(LTrim(RTrim(myVal)), xc, 1) = ";" Or Mid(LTrim(RTrim(myVal)), xc, 1) = ":" Or Mid(LTrim(RTrim(myVal)), xc, 1) = "?" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblPro_Pol_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                Exit For
                Exit Sub
            End If
        Next

        myVal = LTrim(RTrim(Me.txtFileNum.Text))
        If Trim(myVal) = "" Or Trim(myVal) = "*" Or Trim(myVal) = "." Or Trim(myVal) = "?" Then
            Me.lblMsg.Text = "Missing input field or Invalid character found in input field - " & Me.lblFileNum.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(myVal)))
            If Mid(LTrim(RTrim(myVal)), xc, 1) = ";" Or Mid(LTrim(RTrim(myVal)), xc, 1) = ":" Or Mid(LTrim(RTrim(myVal)), xc, 1) = "?" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblFileNum.Text
                'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                Exit For
                Exit Sub
            End If
        Next

        myVal = LTrim(RTrim(Me.txtBatch_Num.Text))
        If Trim(myVal) = "" Or Trim(myVal) = "*" Or Trim(myVal) = "." Or Trim(myVal) = "?" Then
            Me.lblMsg.Text = "Missing input field or Invalid character found in input field - " & Me.lblBatch_Num.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(myVal)))
            If Mid(LTrim(RTrim(myVal)), xc, 1) = ";" Or Mid(LTrim(RTrim(myVal)), xc, 1) = ":" Or Mid(LTrim(RTrim(myVal)), xc, 1) = "?" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblBatch_Num.Text
                'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                Exit For
                Exit Sub
            End If
        Next


        Dim strMyMsg As String = ""
        Dim dteTrans As Date = Now

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
        'Dim mydte As Date = Now

        'Dim dteDOB As Date = Now

        'Dim lngDOB_ANB As Integer = 0
        'Dim Dte_Proposal As Date = Now
        'Dim Dte_Commence As Date = Now
        'Dim Dte_DOB As Date = Now
        'Dim Dte_Maturity As Date = Now

        Dim myYear As String = ""

        'Validate date
        Me.txtTrans_Date.Text = Trim(Me.txtTrans_Date.Text)
        myarrData = Split(Me.txtTrans_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblTrans_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If
        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)
        If Val(strMyYear) < 1999 Then
            Me.lblMsg.Text = "Error. Receipt year date is less than 1999 ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)
        Me.txtTrans_Date.Text = Trim(strMyDte)

        If RTrim(Me.txtTrans_Date.Text) = "" Or Len(Trim(Me.txtTrans_Date.Text)) <> 10 Then
            Me.lblMsg.Text = "Missing or Invalid date - Receipt Date..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtTrans_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblTrans_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
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
            Me.lblMsg.Text = "Please enter valid date..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Exit Sub
        End If
        Me.txtTrans_Date.Text = RTrim(strMyDte)

        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteTrans = Format(mydte, "MM/dd/yyyy")


        If RTrim(Me.txtTrans_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblTrans_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Exit Sub
        End If

        Call MOD_GEN.gnInitialize_Numeric(Me.txtTrans_Amt)
        If Val(Me.txtTrans_Amt.Text) = 0 Then
            Me.lblMsg.Text = "Missing " & Me.lblTrans_Amt.Text & " or Value is zero..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Exit Sub
        End If


        If Trim(Me.txtPWD.Text) <> "quo_to_pol" Then
            Me.lblMsg.Text = "Invalid Access or Password code..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        'Me.lblMsg.Text = "About to save data into database..."
        'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
        'Exit Sub

        '   Trim(myYear)
        '   RTrim(Me.txtBraNum.Text)
        '   RTrim(Me.txtProductClass.Text)
        '   RTrim(Me.txtProduct_Num.Text)
        '   
        myYear = Trim(Me.txtYear.Text)
        If Trim(txtPol_Num.Text) = "" Then
            Me.txtPol_Num.Text = MOD_GEN.gnGet_Serial_File_Proposal_Policy(RTrim("GET_SN_GL_FIL_PRO_POL"), RTrim("POL"), Trim(myYear), RTrim("GL"), RTrim(Me.txtBraNum.Text), RTrim(Me.txtProductClass.Text), RTrim(Me.txtProduct_Num.Text), RTrim("G"), RTrim(""), RTrim(""))
        End If

        If Trim(txtPol_Num.Text) = "" Or Trim(Me.txtPol_Num.Text) = "0" Or Trim(Me.txtPol_Num.Text) = "*" Then
            Me.txtPol_Num.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get the next POLICY NO. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtPol_Num.Text) = "PARAM_ERR" Then
            Me.txtPol_Num.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get the next POLICY NO - INVALID PARAMETER(S)..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtPol_Num.Text) = "DB_ERR" Then
            Me.txtPol_Num.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to connect to database. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtPol_Num.Text) = "ERR_ERR" Then
            Me.txtPol_Num.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get connection object. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
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


        Me.BUT_OK.Enabled = False

        Dim intRC As Integer = 0

        Dim objOLETran As OleDbTransaction
        ' Start a local transaction.
        objOLETran = objOLEConn.BeginTransaction

        Try

            Dim objOLECmd As OleDbCommand = Nothing

            ' update policy table
            strTable = strTableName
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_POLY_POLICY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " ,TBIL_POLY_PROPSL_ACCPT_STATUS = 'A'"
            strSQL = strSQL & " ,TBIL_POLY_PROPSL_ACCPT_DT = '" & CDate(Format(Now, "MM/dd/yyyy")) & "'"
            strSQL = strSQL & " ,TBIL_POLY_PRPSAL_RECD_DT = '" & CDate(Format(Now, "MM/dd/yyyy")) & "'"
            strSQL = strSQL & " ,TBIL_POLICY_ISSUE_DT = '" & CDate(Format(Now, "MM/dd/yyyy")) & "'"
            strSQL = strSQL & " ,TBIL_POLICY_EFF_DT = '" & CDate(Me.txtPol_Eff_Date.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_POLY_PROPSAL_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_POLY_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"

            objOLECmd = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd.CommandType = CommandType.Text
            intRC = objOLECmd.ExecuteNonQuery()
            objOLECmd.Dispose()
            objOLECmd = Nothing

            Dim objOLECmd_X As OleDbCommand = Nothing

            ' '' update premium information table
            strTable = strTableName
            strTable = "TBIL_POLICY_PREM_INFO"
            strTable = "TBIL_GRP_POLICY_PREM_INFO"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_POL_PRM_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_POL_PRM_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_POL_PRM_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing


            ' update members table
            strTable = strTableName
            strTable = "TBIL_GRP_POLICY_MEMBERS"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_POL_MEMB_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " , TBIL_POL_MEMB_STATUS = 'P'"
            strSQL = strSQL & " , TBIL_POL_MEMB_TRANS_DATE = '" & CDate(Format(dteProc, "MM/dd/yyyy")) & "'"
            strSQL = strSQL & " WHERE TBIL_POL_MEMB_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_POL_MEMB_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            strSQL = strSQL & " AND TBIL_POL_MEMB_BATCH_NO = '" & RTrim(txtBatch_Num.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing

            ' update additional cover members table
            strTable = strTableName
            strTable = "TBIL_GRP_ADD_COVER_MEMBERS"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_ADD_COV_MEMB_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " , TBIL_ADD_COV_MEMB_STATUS = 'P'"
            strSQL = strSQL & " , TBIL_ADD_COV_MEMB_TRANS_DATE = '" & CDate(Format(dteProc, "MM/dd/yyyy")) & "'"
            strSQL = strSQL & " WHERE TBIL_ADD_COV_MEMB_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_ADD_COV_MEMB_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            strSQL = strSQL & " AND TBIL_ADD_COV_MEMB_BATCH_NO = '" & RTrim(txtBatch_Num.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing


            ' update beneficiary table
            strTable = strTableName
            strTable = "TBIL_POLICY_BENEFRY"
            strTable = "TBIL_GRP_POLICY_BENEFRY"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_POL_BENF_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_POL_BENF_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_POL_BENF_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_BENF_BATCH_NO = '" & RTrim(txtBatch_Num.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing


            ' '' update funeral table
            strTable = strTableName
            strTable = "TBIL_FUNERAL_SA_TAB"
            strTable = "TBIL_GRP_FUNERAL_SA_TAB"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_FUN_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_FUN_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_FUN_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'strSQL = strSQL & " AND TBIL_FUN_BATCH_NO = '" & RTrim(txtBatch_Num.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing


            ' '' update additional cover table
            strTable = strTableName
            strTable = "TBIL_POLICY_ADD_PREM"
            strTable = "TBIL_GRP_POLICY_ADD_PREM"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_POL_ADD_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_POL_ADD_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_POL_ADD_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_ADD_BATCH_NO = '" & RTrim(txtBatch_Num.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing


            ' '' update medical information table
            strTable = strTableName
            strTable = "TBIL_POLICY_MEDIC_EXAM"
            strTable = "TBIL_GRP_POLICY_MEDIC_EXAM"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_POL_MED_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_POL_MED_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_POL_MED_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_MED_BATCH_NO = '" & RTrim(txtBatch_Num.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing


            ' '' update medical illness table
            strTable = strTableName
            strTable = "TBIL_POLICY_ILLNESS"
            strTable = "TBIL_GRP_POLICY_ILLNESS"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_POL_ILL_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_POL_ILL_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_POL_ILL_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_ILL_BATCH_NO = '" & RTrim(txtBatch_Num.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing


            ' '' update policy charges table
            strTable = strTableName
            strTable = "TBIL_POLICY_CHG_DTLS"
            strTable = "TBIL_GRP_POLICY_CHG_DTLS"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_POLY_CHG_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_POLY_CHG_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_POLY_CHG_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POLY_CHG_BATCH_NO = '" & RTrim(txtBatch_Num.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing


            ' '' update policy loading and discount table
            strTable = strTableName
            strTable = "TBIL_POLICY_DISCT_LOAD"
            strTable = "TBIL_GRP_POLICY_DISCT_LOAD"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_POL_DISC_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_POL_DISC_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_POL_DISC_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_DISC_BATCH_NO = '" & RTrim(txtBatch_Num.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing


            ' '' update premium calculation details table
            strTable = strTableName
            strTable = "TBIL_POLICY_PREM_DETAILS"
            strTable = "TBIL_GRP_POLICY_PREM_DETAILS"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_POL_PRM_DTL_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_POL_PRM_DTL_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_POL_PRM_DTL_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_PRM_DTL_BATCH_NO = '" & RTrim(txtBatch_Num.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing


            ' '' update premium calculation details table
            strTable = strTableName
            strTable = "TBIL_POLICY_DOC_ITEMS"
            strTable = "TBIL_GRP_POLICY_DOC_ITEMS"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_POL_ITEM_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_POL_ITEM_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_POL_ITEM_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_ITEM_BATCH_NO = '" & RTrim(txtBatch_Num.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing


            '-----------------------------------------------------------------------
            'START SAVE RECEIPT DATA
            '-----------------------------------------------------------------------

            strTable = strTableName
            strTable = "TBIL_POLICY_RECEIPT"
            strTable = "TBIL_GRP_POLICY_RECEIPT"

            strSQL = ""
            strSQL = "SELECT TOP 1 * FROM " & strTable
            strSQL = strSQL & " WHERE TBIL_RCT_FILE_NUM = '" & RTrim(txtFileNum.Text) & "'"
            strSQL = strSQL & " AND TBIL_RCT_PROPOSAL_NUM = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            'strSQL = strSQL & " AND TBIL_RCT_POLICY_NUM = '" & RTrim(txtPol_Num.Text) & "'"

            Dim objDA As System.Data.OleDb.OleDbDataAdapter
            objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
            'objDA.SelectCommand.Connection = objOLEConn
            objDA.SelectCommand.Transaction = objOLETran
            'objDA.SelectCommand.CommandType = CommandType.Text
            'objDA.SelectCommand.CommandText = strSQL
            'or
            'objDA.SelectCommand = New System.Data.OleDb.OleDbCommand(strSQL, objOleConn)

            Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
            m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

            Dim obj_DT As New System.Data.DataTable
            'Dim m_rwContact As System.Data.DataRow
            Dim intC As Integer = 0

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '   Creating a new record
                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()

                drNewRow("TBIL_RCT_MDLE") = RTrim("G")
                drNewRow("TBIL_RCT_FILE_NUM") = RTrim(Me.txtFileNum.Text)
                drNewRow("TBIL_RCT_PROPOSAL_NUM") = RTrim(Me.txtPro_Pol_Num.Text)
                drNewRow("TBIL_RCT_POLICY_NUM") = RTrim(Me.txtPol_Num.Text)

                'drNewRow("TBIL_RCT_BATCH_NUM") = RTrim(Me.txtBatch_Num.Text)

                drNewRow("TBIL_RCT_DATE") = dteTrans
                drNewRow("TBIL_RCT_NUM") = RTrim(Me.txtTrans_Num.Text)
                drNewRow("TBIL_RCT_AMT") = RTrim(Me.txtTrans_Amt.Text)

                drNewRow("TBIL_RCT_FLAG") = "A"
                drNewRow("TBIL_RCT_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_RCT_KEYDTE") = Now

                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                drNewRow = Nothing
                Me.lblMsg.Text = "New Record Saved to Database Successfully."
            Else

                With obj_DT
                    .Rows(0)("TBIL_RCT_MDLE") = RTrim("G")

                    .Rows(0)("TBIL_RCT_FILE_NUM") = RTrim(Me.txtFileNum.Text)
                    .Rows(0)("TBIL_RCT_PROPOSAL_NUM") = RTrim(Me.txtPro_Pol_Num.Text)
                    .Rows(0)("TBIL_RCT_POLICY_NUM") = RTrim(Me.txtPol_Num.Text)

                    '.Rows(0)("TBIL_RCT_BATCH_NUM") = RTrim(Me.txtBatch_Num.Text)

                    .Rows(0)("TBIL_RCT_DATE") = dteTrans
                    .Rows(0)("TBIL_RCT_NUM") = RTrim(Me.txtTrans_Num.Text)
                    .Rows(0)("TBIL_RCT_AMT") = RTrim(Me.txtTrans_Amt.Text)

                End With

                obj_DT.Rows(0)("TBIL_RCT_FLAG") = "C"
                'obj_DT.Rows(0)("TBIL_RCT_OPERID") = CType(myUserIDX, String)
                'obj_DT.Rows(0)("TBIL_RCT_KEYDTE") = Now

                intC = objDA.Update(obj_DT)

                Me.lblMsg.Text = "Record Saved to Database Successfully."
            End If

            obj_DT.Dispose()
            obj_DT = Nothing

            m_cbCommandBuilder.Dispose()
            m_cbCommandBuilder = Nothing


            '-----------------------------------------------------------------------
            'END SAVE RECEIPT DATA
            '-----------------------------------------------------------------------

            ' Commit the transaction.
            objOLETran.Commit()


            If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
                objDA.SelectCommand.Connection.Close()
            End If
            objDA.Dispose()
            objDA = Nothing



            Me.lblMsg.Text = "Proposal record converted to policy. Please note the Policy No generated..."
            strMyMsg = "Proposal record successfully converted to policy. \n\nPolicy No: " & Me.txtPol_Num.Text & "\n\nPlease note down the Policy No generated..."
            Me.chkAccept.Enabled = False
            Me.chkAccept.Checked = False
            Me.lblPWD.Enabled = False
            Me.txtPWD.Enabled = False
            Me.BUT_OK.Enabled = False

            Me.txtPro_Pol_Num.Enabled = False
            Me.txtFileNum.Enabled = False
            Me.cmdFileNum.Enabled = False
            strUpdate_Sw = "Y"

        Catch ex As Exception
            'Console.WriteLine(ex.Message)
            'Me.lblMsg.Text = "Proposal record conversion not successful..."
            Me.lblMsg.Text = "Error Occured. Reason: " & ex.Message.ToString
            strMyMsg = "Error Occured. Reason: " & ex.Message.ToString

            ' Try to rollback the transaction
            Try
                objOLETran.Rollback()
            Catch
                ' Do nothing here; transaction is not active.
            End Try

            strUpdate_Sw = "N"


        End Try


        objOLETran.Dispose()
        objOLETran = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        If UCase(strUpdate_Sw) = "Y" Then
            'Me.lblMsg.Text = "Proposal record converted to policy. Please note the Policy No generated..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & strMyMsg & "');", True)
        Else
            'Me.lblMsg.Text = "Proposal record conversion not successful..."
            FirstMsg = "Javascript:alert('" & strMyMsg & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & strMyMsg & "');", True)
        End If

    End Sub

    Private Function Proc_DoGet_Record(ByVal pvCode As String, ByVal pvProNo As String, ByVal pvFileNo As String) As Boolean

        blnStatusX = False

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
            blnStatusX = False
            Return blnStatusX
            Exit Function
        End Try


        strREC_ID = pvProNo

        Try

            strTable = strTableName

            strSQL = ""
            strSQL = "SPIL_SELECT_PROPOSAL"
            strSQL = "SPGL_SELECT_QUOTATION"

            objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
            ''objOLECmd_Chk.CommandTimeout = 180
            objOLECmd_Chk.CommandType = CommandType.StoredProcedure

            objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 40).Value = LTrim(RTrim(strREC_ID))
            objOLECmd_Chk.Parameters.Add("p02", OleDbType.VarChar, 40).Value = LTrim(RTrim(pvFileNo))
            objOLECmd_Chk.Parameters.Add("p03", OleDbType.VarChar, 3).Value = RTrim("G")

            objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
            If (objOLEDR_Chk.Read()) Then

                Me.txtFileNum.Text = RTrim(CType(objOLEDR_Chk("TBIL_POLY_FILE_NO") & vbNullString, String))
                Me.txtPol_Num.Text = RTrim(CType(objOLEDR_Chk("TBIL_POLY_POLICY_NO") & vbNullString, String))
                Me.txtAssured_Name.Text = RTrim(CType(objOLEDR_Chk("TBIL_INSRD_SURNAME") & vbNullString, String)) & " " & _
                   RTrim(CType(objOLEDR_Chk("TBIL_INSRD_FIRSTNAME") & vbNullString, String))


                If IsDate(objOLEDR_Chk("TBIL_POL_PRM_FROM")) Then
                    Me.txtPol_Eff_Date.Text = Format(CType(objOLEDR_Chk("TBIL_POL_PRM_FROM"), DateTime), "MM/dd/yyyy")
                Else
                    Me.txtPol_Eff_Date.Text = Format(Now, "MM/dd/yyyy")
                End If

                Me.txtYear.Text = RTrim(CType(objOLEDR_Chk("TBIL_POLY_UNDW_YR") & vbNullString, String))
                Me.txtBraNum.Text = RTrim(CType(objOLEDR_Chk("TBIL_POLY_BRANCH_CD") & vbNullString, String))
                Me.txtProductClass.Text = RTrim(CType(objOLEDR_Chk("TBIL_PRDCT_DTL_CAT") & vbNullString, String))
                Me.txtProduct_Num.Text = RTrim(CType(objOLEDR_Chk("TBIL_POLY_PRDCT_CD") & vbNullString, String))
                Me.txtProduct_Name.Text = Trim(CType(objOLEDR_Chk("TBIL_PRDCT_DTL_DESC") & vbNullString, String))

                If IsDate(objOLEDR_Chk("TBIL_RCT_DATE")) Then
                    Me.txtTrans_Date.Text = Format(CType(objOLEDR_Chk("TBIL_RCT_DATE"), DateTime), "dd/MM/yyyy")
                Else
                    Me.txtTrans_Date.Text = ""
                End If
                Me.txtTrans_Num.Text = Trim(CType(objOLEDR_Chk("TBIL_RCT_NUM") & vbNullString, String))
                Me.txtTrans_Amt.Text = RTrim(CType(objOLEDR_Chk("TBIL_RCT_AMT") & vbNullString, String))

                ' check for existence of premium information, premium calculation details
                ' also check for policy status
                If RTrim(CType(objOLEDR_Chk("TBIL_POL_PRM_FILE_NO") & vbNullString, String)) = "" Or _
                   RTrim(CType(objOLEDR_Chk("TBIL_POL_PRM_PROP_NO") & vbNullString, String)) = "" Then
                    myTmp_Chk = "N"
                    blnStatusX = False
                    Me.lblMsg.Text = "Sorry! Premium information must be captured before this conversion."
                ElseIf RTrim(CType(objOLEDR_Chk("TBIL_POL_PRM_DTL_FILE_NO") & vbNullString, String)) = "" Or _
                       RTrim(CType(objOLEDR_Chk("TBIL_POL_PRM_DTL_PROP_NO") & vbNullString, String)) = "" Then
                    myTmp_Chk = "N"
                    blnStatusX = False
                    Me.lblMsg.Text = "Sorry! Premium calculation is yet to be done and saved before this conversion."
                Else
                    If RTrim(CType(objOLEDR_Chk("TBIL_POLY_PROPSL_ACCPT_STATUS") & vbNullString, String)) = "P" Then
                        myTmp_Chk = "Y"
                        blnStatusX = True
                        Me.lblMsg.Text = "Proposal No: " & Me.txtPro_Pol_Num.Text
                    Else
                        myTmp_Chk = "N"
                        blnStatusX = False
                        Me.lblMsg.Text = "Warning! The record you requested for has already been converted."
                    End If
                End If
                '

            Else
                myTmp_Chk = "N"
                blnStatusX = False
                Me.lblMsg.Text = "Record not found for Proposal No: " & Me.txtPro_Pol_Num.Text
            End If

        Catch ex As Exception
            myTmp_Chk = "N"
            blnStatusX = False

            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message.ToString()
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)

        End Try

        objOLEDR_Chk = Nothing

        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing

        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing


        If myTmp_Chk = "N" Then
            'Me.lblMsg.Text = "Record not found for Proposal No: " & Me.txtPro_Pol_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
        End If

        Return blnStatusX

    End Function

    Private Sub Proc_DoNew()

        Me.cmdNew_ASP.Enabled = True
        Me.cmdFileNum.Enabled = True

        Me.txtPro_Pol_Num.Text = ""
        Me.txtPro_Pol_Num.Enabled = True
        Me.txtFileNum.Text = ""
        Me.txtFileNum.Enabled = True

        Me.txtPol_Num.Text = ""
        Me.txtAssured_Name.Text = ""

        Me.txtProductClass.Text = ""
        Me.txtProduct_Num.Text = ""
        Me.txtProduct_Name.Text = ""

        Me.txtBraNum.Text = ""
        Me.txtPol_Eff_Date.Text = ""

        Me.txtTrans_Date.Text = ""
        Me.txtTrans_Num.Text = ""
        Me.txtTrans_Amt.Text = ""

        Me.chkAccept.Enabled = False
        Me.chkAccept.Checked = False

        Me.lblPWD.Enabled = False
        Me.txtPWD.Enabled = False
        Me.txtPWD.Text = ""

        Me.BUT_OK.Enabled = False

    End Sub

    Private Sub Proc_Batch()
        'Me.cmdDelItem.Enabled = True

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
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
        strQ_ID = RTrim(Me.txtPro_Pol_Num.Text)

        Me.cboBatch_Num.Items.Clear()


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
                    If Trim(Me.txtBatch_Num.Text) = "" Then
                        pvBatNum = RTrim(CType(objMem_DR("TBIL_POL_MEMB_BATCH_NO") & vbNullString, String))
                    End If
                End If

                pvCNT = pvCNT + 1

                pvListItem = New ListItem
                pvListItem.Value = RTrim(CType(objMem_DR("TBIL_POL_MEMB_BATCH_NO") & vbNullString, String))
                pvListItem.Text = RTrim(CType(objMem_DR("TBIL_POL_MEMB_BATCH_NO") & vbNullString, String))
                Me.cboBatch_Num.Items.Add(pvListItem)
            Loop

            Me.cboBatch_Num.Items.Insert(0, New ListItem("(select)", "0"))

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

        objOLEConn = Nothing

    End Sub

    'Public Sub ExecuteTransaction(ByVal connectionString As String)

    '    Using connection As New OleDbConnection(connectionString)
    '        Dim command As New OleDbCommand()
    '        Dim transaction As OleDbTransaction

    '        ' Set the Connection to the new OleDbConnection.
    '        command.Connection = connection

    '        ' Open the connection and execute the transaction.
    '        Try
    '            connection.Open()

    '            ' Start a local transaction.
    '            transaction = connection.BeginTransaction()

    '            ' Assign transaction object for a pending local transaction.
    '            command.Connection = connection
    '            command.Transaction = transaction

    '            ' Execute the commands.
    '            command.CommandText = _
    '                "Insert into Region (RegionID, RegionDescription) VALUES (100, 'Description')"
    '            command.ExecuteNonQuery()
    '            command.CommandText = _
    '                "Insert into Region (RegionID, RegionDescription) VALUES (101, 'Description')"
    '            command.ExecuteNonQuery()

    '            ' Commit the transaction.
    '            transaction.Commit()
    '            Console.WriteLine("Both records are written to database.")

    '        Catch ex As Exception
    '            Console.WriteLine(ex.Message)
    '            ' Try to rollback the transaction
    '            Try
    '                transaction.Rollback()

    '            Catch
    '                ' Do nothing here; transaction is not active.
    '            End Try
    '        End Try
    '        ' The connection is automatically closed when the
    '        ' code exits the Using block.
    '    End Using
    'End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            Call gnProc_Populate_Box("GL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If

    End Sub


    Private Function Proc_Validate(ByVal pvCODE As String, ByVal pvFIL_PRO_POL As String, ByVal pvRefNo As String) As Boolean

        blnStatusX = False

        strF_ID = RTrim(Me.txtFileNum.Text)
        strQ_ID = RTrim(Me.txtPro_Pol_Num.Text)
        strP_ID = RTrim(Me.txtPol_Num.Text)

        Dim oAL As ArrayList

        Select Case Trim(pvCODE)
            Case "FIL"
                strF_ID = RTrim(pvFIL_PRO_POL.ToString)
                oAL = MOD_GEN.gnGET_RECORD("GET_GL_POLICY_BY_FILE_NO", RTrim(strF_ID), RTrim(""), RTrim(""))
            Case "PRO"
                strQ_ID = RTrim(pvFIL_PRO_POL.ToString)
                oAL = MOD_GEN.gnGET_RECORD("GET_GL_POLICY_BY_QUOTATION_NO", RTrim(strQ_ID), RTrim(""), RTrim(""))
            Case "POL"
                strP_ID = RTrim(pvFIL_PRO_POL.ToString)
                oAL = MOD_GEN.gnGET_RECORD("GET_GL_POLICY_BY_POLICY_NO", RTrim(strP_ID), RTrim(""), RTrim(""))
            Case Else
                blnStatusX = False
                Proc_Validate = blnStatusX
                Return blnStatusX
                Exit Function
        End Select

        If oAL.Item(0) = "TRUE" Then
            '    'Retrieve the record
            '    Response.Write("<br/>Status: " & oAL.Item(0))
            '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
            Me.txtFileNum.Text = oAL.Item(2)
            Me.txtPro_Pol_Num.Text = oAL.Item(3)
            Me.txtPol_Num.Text = oAL.Item(4)
            Me.txtProductClass.Text = oAL.Item(5)
            Me.txtProduct_Num.Text = oAL.Item(6)

            Me.txtAssured_Name.Text = oAL.Item(26)
            Me.txtProduct_Name.Text = oAL.Item(27)

            ' get list of batches in the policy
            Call Proc_Batch()

        Else
            '    'Destroy i.e remove the array list object from memory
            '    Response.Write("<br/>Status: " & oAL.Item(0))
            Me.lblMsg.Text = "Status: " & oAL.Item(1)

            Me.txtFileNum.Text = ""
            Me.txtPro_Pol_Num.Text = ""
            Me.txtPol_Num.Text = ""
            Me.txtAssured_Name.Text = ""
            Me.txtProductClass.Text = ""
            Me.txtProduct_Num.Text = ""
            Me.txtProduct_Name.Text = ""

            'Me.lblMessage.Text = "Invalid Policy Number. Please enter valid policy number..."
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            FirstMsg = "Javascript:alert('" & "Unable to get policy details..." & "')"
        End If

        oAL = Nothing

        blnStatusX = True
        Proc_Validate = blnStatusX
        Return blnStatusX

    End Function


    Protected Sub txtFileNum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFileNum.TextChanged

    End Sub
End Class
