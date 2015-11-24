Imports System.Data.OleDb
Imports System.Data

Partial Class Policy_PRG_LI_GRP_POLY_MEMBERS_RENEW
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Protected PageLinks As String

    'Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    'Protected BufferStr As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strGen_Msg As String = ""

    Protected strF_ID As String
    Protected strP_ID As String
    Protected strQ_ID As String

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

    Dim dblPrem_Rate As Double = 0
    Dim dblPrem_Rate_Per As Integer = 0
    Dim dblPrem_Amt As Double = 0
    Dim dblPrem_Amt_ProRata As Double = 0
    Dim dblLoad_Amt As Double = 0
    Dim dblTotal_Salary As Double = 0
    Dim dblTotal_SA As Double = 0

    Dim dblFree_Cover_Limit As Double = 0

    Protected GenStart_Date As Date = Now
    Protected GenEnd_Date As Date = Now

    Protected MemJoin_Date As Date = Now
    Protected MemExpiry_Date As Date = Now

    Protected intRisk_Days As Integer = 0
    Protected intDays_Diff As Integer = 0

    Dim strPATH As String = ""

    Dim strErrMsg As String

    Dim lstErrMsgs As IList(Of String)
    Dim _rtnMessage As String

    Dim TotTransAmt As Decimal = 0
    Dim TransAmt As Decimal = 0
    Protected added_Prorata_Premium As Decimal
    Protected added_Prorata_Days As Integer
    Protected added_Used_Days As Integer
    Protected added_SA As Decimal
    Protected add_date_added As Date



    Protected Sub DoProc_Data_Source_Change()
        Call gnGET_SelectedItem(Me.cboData_Source, Me.txtData_Source_SW, Me.txtData_Source_Name, Me.lblMsg)
        Select Case UCase(Trim(Me.txtData_Source_SW.Text))
            Case "M"
                'tr_file_upload.Visible = False
                Me.cmdFile_Upload.Enabled = False
                Me.cmdRenewBtn.Enabled = True
            Case "U"
                'tr_file_upload.Visible = True
                Me.cmdRenewBtn.Enabled = False
            Case Else
                'tr_file_upload.Visible = False
                Me.cmdFile_Upload.Enabled = False
                Me.cmdRenewBtn.Enabled = False
        End Select

        'Response.Write("<br />Code: " & UCase(Trim(Me.txtData_Source_SW.Text)))
        'tr_file_upload.Visible = True

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


    Protected Sub DoProc_Premium_Code_Change()
        Call gnProc_DDL_Get(Me.cboPrem_Rate_Code, RTrim(Me.txtPrem_Rate_Code.Text))
        Call DoGet_SelectedItem(Me.cboPrem_Rate_Code, Me.txtPrem_Rate_Code, Me.txtPrem_Rate_CodeName, Me.lblMsg)
        If Trim(Me.txtPrem_Rate_Code.Text) = "" Then
            Me.cboPrem_Rate_Code.Enabled = True
            Me.txtPrem_Rate.Text = "0.00"
            Me.txtPrem_Rate_Per.Text = "0"
            Exit Sub
        End If

        Dim myRetValue As String = "0"
        Dim myTerm As String = ""
        myTerm = Me.txtPrem_Period_Yr.Text
        Select Case UCase(Me.txtProduct_Num.Text)
            Case "P005"
                myTerm = "1"
            Case "F001", "F002"
                myTerm = "1"
        End Select


        'myRetValue = MOD_GEN.gnGET_RATE("GET_IL_PREMIUM_RATE", "IND", Me.txtPrem_Rate_Code.Text, Me.txtProduct_Num.Text, myTerm, Me.txtDOB_ANB.Text, Me.lblMsg, Me.txtPrem_Rate_Per)
        myRetValue = MOD_GEN.gnGET_RATE("GET_GL_PREMIUM_RATE", "GRP", Me.txtPrem_Rate_Code.Text, Me.txtProduct_Num.Text, myTerm, Val(Me.txtDOB_ANB.Text), Me.lblMsg, Me.txtPrem_Rate_Per)

        'Response.Write("<BR/>Rate Code: " & Me.txtPrem_Rate_Code.Text)
        'Response.Write("<BR/>Product Code: " & Me.txtProduct_Num.Text)
        'Response.Write("<BR/>Period: " & myTerm)
        'Response.Write("<BR/>Age: " & Me.txtDOB_ANB.Text)
        'Response.Write("<BR/>Value: " & myRetValue)

        If Left(LTrim(myRetValue), 3) = "ERR" Then
            Me.cboPrem_Rate_Code.SelectedIndex = -1
            Me.cboPrem_Rate_Code.Enabled = True
            Me.txtPrem_Rate.Text = "0.00"
            Me.txtPrem_Rate_Per.Text = "0"
        Else
            Me.txtPrem_Rate.Text = myRetValue.ToString
        End If

    End Sub

    Public Sub GETMEMBERSBY_BATCHNO_POLYNO(ByVal polyNumber As String, ByVal fileNumber As String, ByVal propNumber As String, ByVal batchNumber As String)
        'Dim rtnString As String
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GRP_GETMEMBERSBY_BATCH_NO"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_POLY_NO", polyNumber)
        cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_FILE_NO", fileNumber)
        cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_PROP_NO", propNumber)
        cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_BATCH_NO", batchNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            'objOledr = cmd.ExecuteReader()

            'If objOledr.HasRows Then
            GridView1.DataSource = cmd.ExecuteReader()
            GridView1.DataBind()
            'Else
            '    _rtnMessage = "Sorry. The system cannot find record with IDs: " + txtPolNum.Text
            'End If


            conn.Close()
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try
    End Sub



    Public Sub GetPolicyBatchNumber(ByVal polyNumber As String)
        'Dim rtnString As String
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GRP_GETBATCH_NO"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_POLY_NO", polyNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            'objOledr = cmd.ExecuteReader()

            'If objOledr.HasRows Then
            cboBatch_Num.DataSource = cmd.ExecuteReader()
            cboBatch_Num.DataTextField = "TBIL_POL_MEMB_BATCH_NO"
            cboBatch_Num.DataValueField = "TBIL_POL_MEMB_BATCH_NO"
            cboBatch_Num.DataBind()
            cboBatch_Num.Items(0).Value = ""
            'Else
            '    _rtnMessage = "Sorry. The system cannot find record with IDs: " + txtPolNum.Text
            'End If


            conn.Close()
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try
    End Sub

    Private Sub Proc_DoGet_Record(ByVal pvCODE As String)

        'Me.tr_file_upload.Visible = False
        Me.cmdFile_Upload.Enabled = False

        'Me.txtFileNum.Text = RTrim(strF_ID)

        Dim oAL As ArrayList
        Dim myDO_WHAT As String = ""
        Dim myDO_VALUE As String = ""

        Select Case UCase(Trim(pvCODE))
            Case "FILE"
                myDO_WHAT = "GET_GL_POLICY_BY_FILE_NO"
                myDO_VALUE = strF_ID
            Case "QUOTATION"
                myDO_WHAT = "GET_GL_POLICY_BY_QUOTATION_NO"
                myDO_VALUE = strQ_ID
            Case "POLICY"
                myDO_WHAT = "GET_GL_POLICY_BY_POLICY_NO"
                myDO_VALUE = strP_ID

            Case Else
                Exit Sub

        End Select

        oAL = MOD_GEN.gnGET_RECORD(myDO_WHAT, RTrim(myDO_VALUE), RTrim(""), RTrim(""))
        If oAL.Item(0) = "TRUE" Then

            Me.txtFileNum.Enabled = False
            Me.txtQuote_Num.Enabled = False
            Me.txtPolNum.Enabled = False

            Me.cmdGetPol.Enabled = False

            '    'Retrieve the record
            '    Response.Write("<br/>Status: " & oAL.Item(0))
            '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
            Me.txtFileNum.Text = oAL.Item(2)
            Me.txtQuote_Num.Text = oAL.Item(3)
            Me.txtPolNum.Text = oAL.Item(4)
            Me.txtProductClass.Text = oAL.Item(5)
            Me.txtProduct_Num.Text = oAL.Item(6)
            Me.txtPrem_Rate_TypeNum.Text = oAL.Item(12)
            Me.txtPrem_Rate_Code.Text = oAL.Item(14)
            Me.txtPrem_Period_Yr.Text = oAL.Item(19)
            If Trim(oAL.Item(20).ToString) <> "" Then
                'GenEnd_Date = CDate(oAL.Item(20).ToString)
                myarrData = Split(Trim(oAL.Item(20).ToString), "/")
                GenStart_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                Me.txtStart_Date.Text = Format(GenStart_Date, "dd/MM/yyyy")
            End If
            If Trim(oAL.Item(21).ToString) <> "" Then
                'GenEnd_Date = CDate(oAL.Item(21).ToString)
                myarrData = Split(Trim(oAL.Item(21).ToString), "/")
                GenEnd_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                Me.txtEnd_Date.Text = Format(GenEnd_Date, "dd/MM/yyyy")
            End If
            Me.txtPrem_Rate.Text = oAL.Item(22)
            Me.txtPrem_Rate_Per.Text = oAL.Item(23)
            Me.txtPrem_SA_Factor.Text = oAL.Item(24)

            Me.lblPrem_Rate_X.Enabled = False
            Me.cboPrem_Rate_Code.Enabled = False
            Select Case UCase(Trim(Me.txtPrem_Rate_TypeNum.Text))
                Case "F"
                    Me.lblPrem_Rate_X.Enabled = True
                    Me.cboPrem_Rate_Code.Enabled = True
                Case "N"
                    Me.lblPrem_Rate_X.Enabled = False
                    Me.cboPrem_Rate_Code.Enabled = False
                Case "T"
                    Me.lblPrem_Rate_X.Enabled = False
                    Me.cboPrem_Rate_Code.Enabled = False
            End Select


            strF_ID = Me.txtFileNum.Text
            strQ_ID = Me.txtQuote_Num.Text
            strP_ID = Me.txtPolNum.Text

            'Call Proc_Batch()

        Else
            Me.lblMsg.Text = "Status: " & oAL.Item(1)
        End If

        Call gnProc_Populate_Box("GL_RATE_TYPE_CODE_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPrem_Rate_Code)
        oAL = Nothing

    End Sub

    Private Sub Proc_Batch()
        'Me.cmdDelItem.Enabled = True

        strF_ID = Me.txtFileNum.Text
        strQ_ID = Me.txtQuote_Num.Text
        strP_ID = Me.txtPolNum.Text

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

        Me.cboBatch_Num.Items.Clear()

        strTable = strTableName
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



    Protected Sub cmdGetPol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetPol.Click

        If Trim(Me.txtPolNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPolNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        GetPolicyBatchNumber(txtPolNum.Text)
        strP_ID = RTrim(Me.txtPolNum.Text)
        Session("strP_ID") = strP_ID
        Call Proc_DoGet_Record("POLICY")

    End Sub

    Protected Sub cmdGetBatch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetBatch.Click

        If txtBatch_Num.Text <> "" And txtPolNum.Text <> "" Then
            GETMEMBERSBY_BATCHNO_POLYNO(txtPolNum.Text, txtFileNum.Text, txtQuote_Num.Text, txtBatch_Num.Text)
        End If

    End Sub

    Protected Sub cboBatch_Num_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBatch_Num.SelectedIndexChanged
        txtBatch_Num.Text = cboBatch_Num.SelectedValue.ToString()
    End Sub
End Class
