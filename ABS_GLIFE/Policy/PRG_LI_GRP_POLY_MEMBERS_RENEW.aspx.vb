Imports Microsoft.Office.Interop

Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Configuration
Imports System.Collections
Imports System.Data.Common
Imports System.Linq
Imports System.Web
Imports System.Web.Configuration
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Globalization
Imports System.Collections.Generic
Imports CustodianGroupLife.Data
Imports CustodianGroupLife.Model
Imports CustodianGroupLife.Repositories

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


    Dim strMyYear As String = ""
    Dim strMyMth As String = ""
    Dim strMyDay As String = ""

    Dim strMyDte As String = ""

    Dim mydteX As String = ""
    Dim mydte As Date = Now

    Dim lngDOB_ANB As Integer = 0

    Dim Dte_Current As Date = Now
    Dim Dte_DOB As Date = Now

    Dim sFT As String = ""
    Dim nRow As Integer = 2
    Dim nCol As Integer = 1

    Dim nROW_MIN As Integer = 0
    Dim nROW_MAX As Integer = 0

    Dim xx As String = ""


    Dim my_Batch_Num As String = ""

    Dim my_intCNT As Long = 0
    Dim my_SNo As String = ""

    Dim my_Dte_DOB As Date = Now
    Dim my_Dte_Start As Date = Now
    Dim my_Dte_End As Date = Now

    Dim my_File_Num As String = ""
    Dim my_Prop_Num As String = ""
    Dim my_Poly_Num As String = ""
    Dim my_Staff_Num As String = ""
    Dim my_Member_Name As String = ""
    Dim my_DOB As String = ""
    Dim my_AGE As String = ""
    Dim my_Gender As String = ""
    Dim my_Designation As String = ""
    Dim my_Start_Date As String = ""
    Dim my_End_Date As String = ""
    Dim my_Tenor As String = ""
    Dim my_SA_Factor As Single = 0
    Dim my_Basic_Sal As Double = 0
    Dim my_House_Allow As Double = 0
    Dim my_Transport_Allow As Double = 0
    Dim my_Other_Allow As Double = 0
    Dim my_Total_Salary As Double = 0
    Dim my_Total_SA As Double = 0

    Dim my_Medical_YN As String = ""

    Dim myRetValue As String = "0"
    Dim myTerm As String = ""




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


    Public Sub GETMEMBERSBY_BATCHNO_POLYNO(ByVal polyNumber As String, ByVal fileNumber As String, ByVal propNumber As String)
        'Dim rtnString As String , ByVal batchNumber As String
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
        'cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_BATCH_NO", batchNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            Dim dt As DataTable = New DataTable
            dt.Load(objOledr)
            Dim numRows As Integer = dt.Rows.Count
            lblResult.Text = "Result: " + numRows.ToString + " members listed."

            'If objOledr.HasRows Then
            GridView1.DataSource = dt
            GridView1.DataBind()



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
        txtQuote_Num.Text = ""
        txtFileNum.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""


        'Me.tr_file_upload.Visible = False
        'Me.cmdFile_Upload.Enabled = False

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
            'Me.txtPolNum.Enabled = False

            'Me.cmdGetPol.Enabled = False

            '    'Retrieve the record
            '    Response.Write("<br/>Status: " & oAL.Item(0))
            '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
            Me.txtFileNum.Text = oAL.Item(2)
            Me.txtQuote_Num.Text = oAL.Item(3)
            Me.txtPolNum.Text = oAL.Item(4)
            Me.txtProductClass.Text = oAL.Item(5)
            Me.txtProduct_Num.Text = oAL.Item(6)
            'Me.txtPrem_Rate_TypeNum.Text = oAL.Item(12)
            'Me.txtPrem_Rate_Code.Text = oAL.Item(14)
            'Me.txtPrem_Period_Yr.Text = oAL.Item(19)
            If Trim(oAL.Item(20).ToString) <> "" Then
                'GenEnd_Date = CDate(oAL.Item(20).ToString)
                myarrData = Split(Trim(oAL.Item(20).ToString), "/")
                GenStart_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                'Me.txtStart_Date.Text = Format(GenStart_Date, "dd/MM/yyyy")
            End If
            If Trim(oAL.Item(21).ToString) <> "" Then
                'GenEnd_Date = CDate(oAL.Item(21).ToString)
                myarrData = Split(Trim(oAL.Item(21).ToString), "/")
                GenEnd_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                'Me.txtEnd_Date.Text = Format(GenEnd_Date, "dd/MM/yyyy")
            End If
            'Me.txtPrem_Rate.Text = oAL.Item(22)
            'Me.txtPrem_Rate_Per.Text = oAL.Item(23)
            Me.txtPrem_SA_Factor.Text = oAL.Item(24)

            'Me.lblPrem_Rate_X.Enabled = False
            'Me.cboPrem_Rate_Code.Enabled = False
            'Select Case UCase(Trim(Me.txtPrem_Rate_TypeNum.Text))
            '    Case "F"
            '        Me.lblPrem_Rate_X.Enabled = True
            '        Me.cboPrem_Rate_Code.Enabled = True
            '    Case "N"
            '        Me.lblPrem_Rate_X.Enabled = False
            '        Me.cboPrem_Rate_Code.Enabled = False
            '    Case "T"
            '        Me.lblPrem_Rate_X.Enabled = False
            '        Me.cboPrem_Rate_Code.Enabled = False
            'End Select


            strF_ID = Me.txtFileNum.Text
            strQ_ID = Me.txtQuote_Num.Text
            strP_ID = Me.txtPolNum.Text

            'Call Proc_Batch()

        Else
            Me.lblMsg.Text = "Status: " & oAL.Item(1)
        End If

        'Call gnProc_Populate_Box("GL_RATE_TYPE_CODE_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPrem_Rate_Code)
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
        lblMsg.Text = ""
        If Trim(Me.txtPolNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPolNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'GetPolicyBatchNumber(txtPolNum.Text) , txtBatch_Num.Text
        strP_ID = RTrim(Me.txtPolNum.Text)
        Session("strP_ID") = strP_ID
        Call Proc_DoGet_Record("POLICY")
        Call GET_POLICYDATE_BY_FILENO(txtFileNum.Text)
        Call GETMEMBERSBY_BATCHNO_POLYNO(txtPolNum.Text, txtFileNum.Text, txtQuote_Num.Text)

    End Sub

    Protected Sub cmdGetBatch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetBatch.Click

        'If txtBatch_Num.Text <> "" And txtPolNum.Text <> "" Then
        '    'GETMEMBERSBY_BATCHNO_POLYNO(txtPolNum.Text, txtFileNum.Text, txtQuote_Num.Text, txtBatch_Num.Text)
        'End If

    End Sub

    Protected Sub cboBatch_Num_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBatch_Num.SelectedIndexChanged
        txtBatch_Num.Text = cboBatch_Num.SelectedValue.ToString()
    End Sub

    '    Protected Sub cmdFile_Upload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFile_Upload.Click

    '       ' Me.cmdFile_Upload.Enabled = False

    '        If Me.txtFileNum.Text = "" Then
    '            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
    '            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            Exit Sub
    '        End If

    '        If Me.txtQuote_Num.Text = "" Then
    '            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
    '            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            Exit Sub
    '        End If

    '        'If Me.txtPolNum.Text = "" Then
    '        '    Me.lblMsg.Text = "Missing " & Me.lblPolNum.Text
    '        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '        '    Exit Sub
    '        'End If

    '        'If Val(Trim(Me.txtPrem_SA_Factor.Text)) = 0 Then
    '        '    Me.lblMsg.Text = "Missing " & Me.lblPrem_SA_Factor.Text
    '        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '        '    Exit Sub
    '        'End If

    '        If Me.txtBatch_Num.Text = "" Then
    '            Me.txtFile_Upload.Text = ""
    '            Me.cmdFile_Upload.Enabled = False
    '            Me.lblMsg.Text = "Missing " & Me.lblBatch_Num.Text
    '            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            Exit Sub
    '        End If

    '        If Val(Trim(Me.txtRisk_Days.Text)) = 0 Then
    '            Me.lblMsg.Text = "Missing " & Me.lblRisk_Days.Text
    '            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            Exit Sub
    '        End If

    '        Call gnGET_SelectedItem(Me.cboData_Source, Me.txtData_Source_SW, Me.txtData_Source_Name, Me.lblMsg)
    '        Select Case UCase(Trim(Me.txtData_Source_SW.Text))
    '            Case "M"
    '                'Call Proc_DoSave()
    '                'Me.tr_file_upload.Visible = False
    '                Me.cmdFile_Upload.Enabled = False
    '            Case "U"
    '                'If My_File_Upload..HasFile Then
    '                'End If

    '                'Dim FileName As String = Path.GetFileName(My_File_Upload.PostedFile.FileName)
    '                'Dim Extension As String = Path.GetExtension(My_File_Upload.PostedFile.FileName)
    '                'Dim FolderPath As String = ConfigurationManager.AppSettings("LIFE_DOC_PATH")

    '                Dim myfil As System.Web.HttpPostedFile = Me.My_File_Upload.PostedFile
    '                'Me.txtFile_Upload.Text = Trim(Me.My_File_Upload.PostedFile.FileName).ToString
    '                'Me.txtFile_Upload.Text = myfil.FileName
    '                Me.txtFile_Upload.Text = Path.GetFileName(My_File_Upload.PostedFile.FileName)


    '                If Trim(Me.txtFile_Upload.Text) = "" Then
    '                    Me.lblMsg.Text = "Missing document or file name ..."
    '                    FirstMsg = "Javascript:alert('Missing document or file name. \nPlease select excel document with file extension .XLS or .XLSX')"
    '                    Me.txtFile_Upload.Text = ""
    '                    Exit Sub
    '                End If

    '                If Right(LCase(Trim(Me.txtFile_Upload.Text)), 3) = "xls" Or _
    '                   Right(LCase(Trim(Me.txtFile_Upload.Text)), 4) = "xlsx" Then
    '                Else
    '                    Me.txtFile_Upload.Text = ""
    '                    Me.lblMsg.Text = "Invalid document or file type. Expecting file of type .XLS or .XLSX ..."
    '                    FirstMsg = "Javascript:alert('Invalid document or file name. \nPlease select excel document with file extension .XLS or .XLSX')"
    '                    Exit Sub
    '                End If

    '                Try
    '                    'strPATH = CType(ConfigurationManager.ConnectionStrings("LIFE_DOC_PATH").ToString, String)
    '                    'strPATH = CType(ConfigurationManager.AppSettings("LIFE_DOC_PATH").ToString, String)
    '                    strPATH = Server.MapPath("~/App_Data/Schedules/")

    '                    Dim strFilePath As String = ""
    '                    'strFilePath = strPATH & Me.txtFile_Upload.Text
    '                    strFilePath = Server.MapPath("~/App_Data/Schedules/" & Me.txtFile_Upload.Text)
    '                    'post file to the server
    '                    My_File_Upload.PostedFile.SaveAs(strFilePath)


    '                    'Response.Write("<br/>Path: " & strFilePath)


    '                Catch ex As Exception
    '                    Me.txtFile_Upload.Text = ""
    '                    Me.lblMsg.Text = "Error has occured. <br />Reason: " & ex.Message.ToString
    '                    FirstMsg = "Javascript:alert('" & "Unable to upload document or file to the server" & "')"
    '                    Exit Sub
    '                End Try

    '                Me.cmdFile_Upload.Enabled = False

    '                If Me.chkData_Source.Checked = True Then
    '                    Call Proc_DoSave_OLE()
    '                Else
    '                    Call Proc_DoSave_Upload()
    '                End If
    '                'Me.tr_file_upload.Visible = False

    '            Case Else
    '                Me.lblMsg.Text = "Missing or Invalid " & Me.lblData_Source.Text
    '                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '                Exit Sub

    '        End Select

    '    End Sub
    '    Private Sub Proc_DoSave_Upload()

    '        'Dim xlWSheet As Excel.Worksheet
    '        'Dim sVar As String = xlWSheet.Range("C5").Value.ToString()

    '        'GF/2014/1201/G/G001/G/0000001

    '        cboErr_List.Items.Clear()

    '        If Me.txtBatch_Num.Text = "" Then
    '            Me.txtFile_Upload.Text = ""
    '            Me.cmdFile_Upload.Enabled = False
    '            Me.lblMsg.Text = "Missing " & Me.lblBatch_Num.Text
    '            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            Exit Sub
    '        End If

    '        If Val(Trim(Me.txtXLS_Data_Start_No.Text)) < 1 Then
    '            Me.lblMsg.Text = "Error. Minimum start excel no should be 1 "
    '            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            Exit Sub
    '        End If
    '        If Val(Trim(Me.txtXLS_Data_End_No.Text)) < 1 Or Val(Trim(Me.txtXLS_Data_End_No.Text)) < Val(Trim(Me.txtXLS_Data_Start_No.Text)) Then
    '            Me.lblMsg.Text = "Error. Either excel end no less than 1 or less than excel start no "
    '            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            Exit Sub
    '        End If

    '        blnStatusX = Proc_Batch_Check()
    '        If blnStatusX = False Then
    '            Exit Sub
    '        End If

    '        Me.lblMsg.Text = "File Name: " & Me.txtFile_Upload.Text
    '        'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

    '        If Trim(Me.txtFile_Upload.Text) = "" Then
    '            Me.txtFile_Upload.Text = ""
    '            Me.lblMsg.Text = "Missing document or file name ..."
    '            FirstMsg = "Javascript:alert('Missing document or file name. \nPlease select excel document with file extension .XLS or .XLSX')"
    '            Exit Sub
    '        End If

    '        If Right(LCase(Trim(Me.txtFile_Upload.Text)), 3) = "xls" Or _
    '           Right(LCase(Trim(Me.txtFile_Upload.Text)), 4) = "xlsx" Then
    '        Else
    '            Me.txtFile_Upload.Text = ""
    '            Me.lblMsg.Text = "Invalid document or file type. Expecting file of type .XLS or .XLSX ..."
    '            FirstMsg = "Javascript:alert('Invalid document or file name. \nPlease select excel document with file extension .XLS or .XLSX')"
    '            Exit Sub
    '        End If


    '        'strPATH = CType(ConfigurationManager.ConnectionStrings("LIFE_DOC_PATH").ToString, String)
    '        strPATH = CType(ConfigurationManager.AppSettings("LIFE_DOC_PATH").ToString, String)

    '        Dim strFilename As String = "C:\Temp\test1.xls"
    '        strFilename = strPATH & Me.txtFile_Upload.Text
    '        'strFilename = Server.MapPath(strPATH & Me.txtFile_Upload.Text)


    '        If System.IO.File.Exists(strFilename) = False Then
    '            Me.lblMsg.Text = "Document or file does not exist on the server ..."
    '            FirstMsg = "Javascript:alert('Document or file does not exist on the server')"
    '            Exit Sub
    '        End If

    '        Me.cmdFile_Upload.Enabled = False
    '        'Me.lblMsg.Text = UCase("File Upload successful.")

    '        Try

    '            'Dim myxls_app_Demo As Microsoft.Office.Interop.Excel.Application = Nothing
    '            'myxls_app_Demo = New Microsoft.Office.Interop.Excel.Application
    '            'Dim myxls_app_Demo As Excel.Application
    '            'myxls_app_Demo = New Excel.Application()

    '            'myxls_app_Demo.Quit()
    '            'myxls_app_Demo.Application.Quit()
    '            'myxls_app_Demo = Nothing
    '        Catch ex As Exception
    '            Me.lblMsg.Text = "Error has occured. Reason: " & UCase(ex.Message.ToString)
    '            FirstMsg = "Javascript:alert('" & RTrim("Unable to declare Excel object") & "')"
    '            Exit Sub

    '        End Try


    '        Dim strMyYear As String = ""
    '        Dim strMyMth As String = ""
    '        Dim strMyDay As String = ""

    '        Dim strMyDte As String = ""

    '        Dim mydteX As String = ""
    '        Dim mydte As Date = Now

    '        Dim lngDOB_ANB As Integer = 0

    '        Dim Dte_Current As Date = Now
    '        Dim Dte_DOB As Date = Now

    '        Dim sFT As String = ""
    '        Dim nRow As Integer = 1
    '        Dim nCol As Integer = 1

    '        Dim nROW_MIN As Integer = 0
    '        Dim nROW_MAX As Integer = 0

    '        Dim xx As String = ""

    '        Dim my_Batch_Num As String = ""

    '        Dim my_intCNT As Long = 0
    '        Dim my_SNo As String = ""

    '        Dim my_Dte_DOB As Date = Now
    '        Dim my_Dte_Start As Date = Now
    '        Dim my_Dte_End As Date = Now

    '        Dim my_File_Num As String = ""
    '        Dim my_Prop_Num As String = ""
    '        Dim my_Poly_Num As String = ""
    '        Dim my_Staff_Num As String = ""
    '        Dim my_Member_Name As String = ""
    '        Dim my_DOB As String = ""
    '        Dim my_AGE As String = ""
    '        Dim my_Gender As String = ""
    '        Dim my_Designation As String = ""
    '        Dim my_Start_Date As String = ""
    '        Dim my_End_Date As String = ""
    '        Dim my_Tenor As String = ""
    '        Dim my_SA_Factor As Single = 0
    '        Dim my_Basic_Sal As Double = 0
    '        Dim my_House_Allow As Double = 0
    '        Dim my_Transport_Allow As Double = 0
    '        Dim my_Other_Allow As Double = 0
    '        Dim my_Total_Salary As Double = 0
    '        Dim my_Total_SA As Double = 0

    '        Dim my_Medical_YN As String = ""

    '        Dim myRetValue As String = "0"
    '        Dim myTerm As String = ""

    '        sFT = "Y"

    '        nRow = 0
    '        nCol = 0

    '        my_intCNT = 0

    '        'Dim key As Object
    '        ' Dim returnValue As Object

    '        '      ' ************************************************************************
    '        '      ' OK
    '        '      Dim app As Excel.Application = New Excel.Application()
    '        '      Dim workbook As Excel.Workbook
    '        '      Dim worksheet As Excel.Worksheet

    '        '      Dim xlsrange As Excel.Range

    '        '      Dim intC As Integer = 0

    '        '      strFilename = "H:\ABS-WEB\NIMASAOL\Database\Data Upload.xls"

    '        '      workbook = app.Workbooks.Open(strFilename, _
    '        'Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _
    '        'Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)


    '        '      'workbook = app.Workbooks.Open(strFilename)
    '        '      worksheet = workbook.Worksheets(1)

    '        '      'If (worksheet.Cells(1, 1).ToString() = "") Then
    '        '      'End If
    '        '      'Dim strname As String = worksheet.Cells(1, 2).ToString()
    '        '      'Response.Write("<br/>Cell Data " & strname)
    '        '      'Response.Write("<br/>Row: " & nRow & " - Col: " & worksheet.Cells(1, 3).ToString())

    '        '      For nRow = 1 To 5

    '        '          xlsrange = worksheet.Cells(nRow, 4)
    '        '          'If (xlsrange Is Nothing Or xlsrange.Value2 Is Nothing) Then
    '        '          'Response.Write("<br/>Range object is null...")
    '        '          'Else
    '        '          Response.Write("<br/>Row: " & nRow & " - Range Value: " & xlsrange.Value.ToString() & " - Range Value2: " & xlsrange.Value2.ToString())
    '        '          'End If

    '        '      Next

    '        '      ' ************************************************************************


    '        'Try
    '        '    Dim xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
    '        '    xlApp = CType(CreateObject("Excel.Application"), Microsoft.Office.Interop.Excel.Application)
    '        'Catch ex As Exception
    '        '    Me.lblMsg.Text = "Error has occured. Reason: " & UCase(ex.Message.ToString)
    '        '    Response.Write("<br />Unable to create excel object. Reason: <br />" & Me.lblMsg.Text)
    '        '    'FirstMsg = "Javascript:alert('" & RTrim("Unable to create Excel object") & "')"
    '        '    'Exit Sub

    '        'End Try


    '        'Dim xlBook As Microsoft.Office.Interop.Excel.Workbook
    '        'Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet

    '        'xlBook = CType(xlApp.Workbooks.Add,  _
    '        '            Microsoft.Office.Interop.Excel.Workbook)
    '        'xlSheet = CType(xlBook.Worksheets(1),  _
    '        '            Microsoft.Office.Interop.Excel.Worksheet)

    '        '' The following statement puts text in the second row of the sheet.
    '        'xlSheet.Cells(2, 2) = "This is column B row 2"
    '        '' The following statement shows the sheet.
    '        'xlSheet.Application.Visible = True
    '        '' The following statement saves the sheet to the C:\Test.xls directory.
    '        'xlSheet.SaveAs("C:\Test.xls")

    '        '' Optionally, you can call xlApp.Quit to close the workbook.
    '        ''xlApp.Quit()

    '        'Dim myxls_app As Excel.Application = New Excel.Application()
    '        'Dim myxls_workbook As Excel.Workbook
    '        'Dim myxls_worksheet As Excel.Worksheet

    '        'Dim myxls_range As Excel.Range


    '        ''Dim myxls_app As Microsoft.Office.Interop.Excel.Application
    '        ''Dim myxls_workbook As Microsoft.Office.Interop.Excel.Workbook
    '        ''Dim myxls_worksheet As Microsoft.Office.Interop.Excel.Worksheet

    '        ''Dim myxls_sheets As Microsoft.Office.Interop.Excel.Sheets
    '        ''Dim myxls_range As Microsoft.Office.Interop.Excel.Range


    '        '' *******************
    '        ''myxls_app = New Microsoft.Office.Interop.Excel.Application
    '        'myxls_app = New Excel.Application()

    '        ''myxls_workbook = New Microsoft.Office.Interop.Excel.Workbook


    '        Try
    '            ' myxls_workbook = myxls_app.Workbooks.Open(strFilename, , ReadOnly:=True)
    '            ' myxls_workbook = myxls_app.Workbooks.Open("c:\xlsdoc.xlsx")
    '            'myxls_workbook = myxls_app.Workbooks.Open(strFilename)

    '        Catch ex As Exception

    '            'myxls_worksheet = Nothing

    '            '    'myxls_workbook.SaveAs(strSaveFilename, Excel.XlFileFormat.xlWorkbookDefault)
    '            '    'myxls_workbook.Close(SaveChanges:=False)
    '            '    'myxls_workbook.Close(False)

    '            '    'myxls_workbook.Close(False)
    '            'myxls_workbook = Nothing


    '            '    'myxls_app.Workbooks.Close()
    '            'myxls_app.Quit()
    '            'myxls_app.Application.Quit()
    '            'myxls_app = Nothing

    '            Me.lblMsg.Text = "Error has occured. Reason: " & UCase(ex.Message.ToString)
    '            FirstMsg = "Javascript:alert('" & RTrim("Unable to access data") & "')"
    '            Exit Sub

    '        End Try


    '        ''   open workbook
    '        ''myxls_worksheet = myxls_app.Worksheets(1)
    '        ''myxls_worksheet = myxls_workbook.Sheets("sheetname_or_indexno")
    '        ''myxls_worksheet = myxls_workbook.Sheets("Sheet1")
    '        ''myxls_worksheet = myxls_workbook.Sheets(1)
    '        'myxls_worksheet = myxls_workbook.Worksheets(1)



    '        ''myxls_sheets = myxls_workbook.Sheets()
    '        ''myxls_sheets.Item(1)

    '        'myxls_range = myxls_worksheet.Cells


    '        Dim mystr_con As String = CType(Session("connstr"), String)
    '        Dim myole_con As OleDbConnection = New OleDbConnection(mystr_con)

    '        Try
    '            '    myole_con.Open()
    '        Catch ex As Exception
    '            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
    '            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
    '            FirstMsg = "Javascript:alert('" & "Unable to connect to database" & "')"

    '            GoTo MyLoop_End
    '        End Try


    '        Dim myUserIDX As String = ""
    '        Try
    '            myUserIDX = CType(Session("MyUserIDX"), String)
    '        Catch ex As Exception
    '            myUserIDX = "SYS"
    '        End Try


    '        Dim mystr_sql As String = ""
    '        Dim mystr_sn_param As String = ""
    '        Dim mycnt As Integer = 0

    '        mystr_sn_param = "GL_MEMBER_SN"

    '        my_File_Num = Me.txtFileNum.Text
    '        my_Prop_Num = Me.txtQuote_Num.Text
    '        my_Poly_Num = Me.txtPolNum.Text


    '        strGen_Msg = ""
    '        Me.lblErr_List.Visible = False
    '        Me.cboErr_List.Items.Clear()
    '        Me.cboErr_List.Visible = False

    '        my_intCNT = 0

    '        Dim myole_cmd As OleDbCommand = Nothing

    '        nROW_MIN = Val(Me.txtXLS_Data_Start_No.Text)
    '        nROW_MAX = Val(Me.txtXLS_Data_End_No.Text)
    '        nRow = 0

    '        Try
    '            'ClientScript.RegisterStartupScript(Me.GetType(), "scrollMSG_JavaScript", "scrollMSG(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")", True)
    '        Catch ex As Exception

    '        End Try

    '        '*************************************************************************************
    '        'Gather the validated values from the form and pass 
    '        'to the hashHelper function
    '        '*************************************************************************************

    '        'HERE CAHNGE
    '        'call the hashhelper function and pass the form values into it
    '        hashHelper.postFromExcel(strPATH, strFilename, myUserIDX, my_Batch_Num, nROW_MIN, nROW_MAX, Me.txtPrem_Period_Yr.Text, mystr_con, _
    '       Me.txtPrem_SA_Factor.Text, my_File_Num, my_Prop_Num, my_Poly_Num, txtPrem_Rate_TypeNum.Text, txtPrem_Rate_Per.Text, txtPrem_Rate_Code.Text, _
    '       txtProduct_Num.Text, lstErrMsgs, Convert.ToInt16(txtRisk_Days.Text), 0, GenStart_Date, GenEnd_Date, txtStart_Date.Text, txtEnd_Date.Text, _
    '       MemJoin_Date, txtData_Source_SW.Text, txtPrem_Rate.Text, String.Empty)
    '        GoTo MyLoop_999a


    'MyLoop_Start:
    '        nRow = nRow + 1

    '        If nRow < nROW_MIN Then
    '            GoTo MyLoop_Start
    '        End If

    '        If nRow > nROW_MAX Then
    '            GoTo MyLoop_999
    '        End If

    '        'If nRow <= 2 Then
    '        '    GoTo MyLoop_Start
    '        'End If

    '        'xx = myxls_worksheet.Cells(nRow, 1).ToString
    '        'If Val(xx) = 0 Then
    '        'GoTo MyLoop_Start
    '        'End If

    '        xx = ""
    '        'xx = myxls_worksheet.Cells(nRow, 3).ToString

    '        'myxls_range = myxls_worksheet.Cells(nRow, 3)
    '        'xx = myxls_range.Text.ToString
    '        ''xx = myxls_range.Item(nRow, 3)

    '        'If Trim(xx.ToString) = "" Then
    '        '    GoTo MyLoop_Start
    '        'End If


    '        ' The following statement puts text in the second row of the sheet.
    '        ' xlSheet.Cells(2, 2) = "This is column B row 2"

    '        'ok
    '        'xx = myxls_worksheet.Cells(nRow, 3).Text.ToString


    '        ' Initialize variables
    '        strGen_Msg = ""

    '        'my_File_Num = ""
    '        my_Staff_Num = ""
    '        my_Member_Name = ""
    '        my_DOB = ""
    '        my_AGE = ""
    '        my_Gender = ""
    '        my_Designation = ""
    '        my_Start_Date = ""
    '        my_End_Date = ""
    '        my_Tenor = "1"
    '        my_Tenor = Me.txtPrem_Period_Yr.Text
    '        my_SA_Factor = Val(Trim(Me.txtPrem_SA_Factor.Text))
    '        my_Basic_Sal = Val(0)
    '        my_House_Allow = Val(0)
    '        my_Transport_Allow = Val(0)
    '        my_Other_Allow = Val(0)
    '        my_Total_Salary = Val(0)
    '        my_Total_SA = Val(0)
    '        my_Medical_YN = "N"


    '        'myxls_range = myxls_worksheet.Cells(nRow, 1)
    '        'my_SNo = myxls_range.Text.ToString

    '        'myxls_range = myxls_worksheet.Cells(nRow, 2)
    '        'my_Staff_Num = myxls_range.Text.ToString

    '        'myxls_range = myxls_worksheet.Cells(nRow, 3)
    '        'my_Member_Name = myxls_range.Text.ToString

    '        ' ******************
    '        ' START DOB
    '        ' ******************
    '        Try
    '            'myxls_range = myxls_worksheet.Cells(nRow, 4)
    '            'my_DOB = myxls_range.Text.ToString
    '            'my_DOB = Format(myxls_range.Text, "dd/MM/yyyy")
    '            'my_DOB = CDate(my_DOB).ToString
    '            If Not IsDate(my_DOB) Then
    '                'my_DOB = Format(CDate(my_DOB), "dd/MM/yyyy")
    '            End If

    '        Catch ex As Exception
    '            'myxls_range = myxls_worksheet.Cells(nRow, 4)
    '            'my_DOB = CType(myxls_range.Text, String)
    '            'my_DOB = Format(myxls_range.Text, "dd/MM/yyyy")
    '        End Try
    '        If Val(Mid(my_DOB, 4, 2)) > 12 Then
    '            'my_DOB = Mid(LTrim(my_DOB), 4, 2) & "/" & Left(LTrim(my_DOB), 2) & "/" & Right(RTrim(my_DOB), 4)
    '        End If
    '        ' ******************
    '        ' END DOB
    '        ' ******************

    '        'myxls_range = myxls_worksheet.Cells(nRow, 5)
    '        'my_AGE = myxls_range.Text.ToString

    '        'myxls_range = myxls_worksheet.Cells(nRow, 6)
    '        'my_Gender = myxls_range.Text.ToString

    '        'myxls_range = myxls_worksheet.Cells(nRow, 7)
    '        'my_Designation = myxls_range.Text.ToString

    '        'myxls_range = myxls_worksheet.Cells(nRow, 8)
    '        'my_Start_Date = myxls_range.Text.ToString

    '        'myxls_range = myxls_worksheet.Cells(nRow, 9)
    '        'my_End_Date = myxls_range.Text.ToString

    '        'myxls_range = myxls_worksheet.Cells(nRow, 10)
    '        'my_Tenor = myxls_range.Text.ToString

    '        'myxls_range = myxls_worksheet.Cells(nRow, 11)
    '        'my_SA_Factor = Val(myxls_range.Text.ToString)

    '        'myxls_range = myxls_worksheet.Cells(nRow, 12)
    '        'Try
    '        '    my_Basic_Sal = Val(myxls_range.Text.ToString)
    '        'Catch ex As Exception
    '        '    my_Basic_Sal = Val(0)
    '        'End Try

    '        'myxls_range = myxls_worksheet.Cells(nRow, 13)
    '        'Try
    '        '    my_House_Allow = Val(myxls_range.Text.ToString)
    '        'Catch ex As Exception
    '        '    my_House_Allow = Val(0)
    '        'End Try

    '        'myxls_range = myxls_worksheet.Cells(nRow, 14)
    '        'Try
    '        '    my_Transport_Allow = Val(myxls_range.Text.ToString)
    '        'Catch ex As Exception
    '        '    my_Transport_Allow = Val(0)
    '        'End Try

    '        'myxls_range = myxls_worksheet.Cells(nRow, 15)
    '        'Try
    '        '    my_Other_Allow = Val(myxls_range.Text.ToString)
    '        'Catch ex As Exception
    '        '    my_Other_Allow = Val(0)
    '        'End Try

    '        'myxls_range = myxls_worksheet.Cells(nRow, 16)
    '        'Try
    '        '    my_Total_Salary = Val(myxls_range.Text.ToString)
    '        'Catch ex As Exception
    '        '    my_Total_Salary = Val(0)
    '        'End Try

    '        my_Total_SA = 0

    '        my_Tenor = Me.txtPrem_Period_Yr.Text
    '        myTerm = my_Tenor


    '        ' Response.Write("<br />Start Date: " & my_Start_Date & " - End Date: " & my_End_Date & " - DOB Date: " & my_DOB)

    '        'Validate date
    '        myarrData = Split(my_DOB, "/")
    '        If myarrData.Count <> 3 Then
    '            'Me.lblMsg.Text = "Missing or Invalid " & Me.lblMember_DOB.Text & ". Expecting full date in ddmmyyyy format ..."
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Incomplete date of birth - " & my_DOB.ToString
    '            GoTo MyLoop_888
    '        End If

    '        strMyDay = myarrData(0)
    '        strMyMth = myarrData(1)
    '        strMyYear = Left(myarrData(2), 4)

    '        strMyDay = CType(Format(Val(strMyDay), "00"), String)
    '        strMyMth = CType(Format(Val(strMyMth), "00"), String)
    '        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

    '        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

    '        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
    '        If blnStatusX = False Then
    '            'Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Invalid date of birth - " & strMyDte.ToString
    '            GoTo MyLoop_888
    '        End If

    '        'Me.txtMember_DOB.Text = RTrim(strMyDte)
    '        ''mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
    '        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
    '        mydte = Format(CDate(mydteX), "MM/dd/yyyy")

    '        my_Dte_DOB = Format(mydte, "MM/dd/yyyy")
    '        Dte_DOB = my_Dte_DOB

    '        Dte_Current = Now
    '        lngDOB_ANB = Val(DateDiff("yyyy", Dte_Current, my_Dte_DOB))
    '        If lngDOB_ANB < 0 Then
    '            lngDOB_ANB = lngDOB_ANB * -1
    '        End If

    '        If Dte_Current.Month >= Dte_DOB.Month Then
    '            lngDOB_ANB = lngDOB_ANB + 1
    '        End If
    '        If Val(my_AGE) = 0 Or Trim(my_AGE) = "" Then
    '            my_AGE = Trim(Str(lngDOB_ANB))
    '        End If

    '        ' ***********************************************************


    '        'Validate date
    '        myarrData = Split(my_Start_Date, "/")
    '        If myarrData.Count <> 3 Then
    '            'Me.lblMsg.Text = "Missing or Invalid " & Me.lblStart_Date.Text & ". Expecting full date in ddmmyyyy format ..."
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Incomplete start date - " & my_Start_Date.ToString
    '            GoTo MyLoop_888
    '        End If

    '        strMyDay = myarrData(0)
    '        strMyMth = myarrData(1)
    '        strMyYear = Left(myarrData(2), 4)

    '        strMyDay = CType(Format(Val(strMyDay), "00"), String)
    '        strMyMth = CType(Format(Val(strMyMth), "00"), String)
    '        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

    '        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

    '        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
    '        If blnStatusX = False Then
    '            'Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Invalid start date - " & strMyDte.ToString
    '            GoTo MyLoop_888
    '        End If

    '        'Me.txtMember_DOB.Text = RTrim(strMyDte)
    '        ''mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
    '        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
    '        mydte = Format(CDate(mydteX), "MM/dd/yyyy")

    '        my_Dte_Start = Format(mydte, "MM/dd/yyyy")
    '        MemJoin_Date = my_Dte_Start


    '        ' ***********************************************************

    '        'Validate date
    '        myarrData = Split(my_End_Date, "/")
    '        If myarrData.Count <> 3 Then
    '            'Me.lblMsg.Text = "Missing or Invalid " & Me.lblEnd_Date.Text & ". Expecting full date in ddmmyyyy format ..."
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Incomplete end date - " & my_End_Date.ToString
    '            GoTo MyLoop_888
    '        End If

    '        strMyDay = myarrData(0)
    '        strMyMth = myarrData(1)
    '        strMyYear = Left(myarrData(2), 4)

    '        strMyDay = CType(Format(Val(strMyDay), "00"), String)
    '        strMyMth = CType(Format(Val(strMyMth), "00"), String)
    '        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

    '        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

    '        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
    '        If blnStatusX = False Then
    '            'Me.lblMsg.Text = "Please enter valid date..."
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Invalid end date - " & strMyDte.ToString
    '            GoTo MyLoop_888
    '        End If

    '        'Me.txtEnd_Date.Text = RTrim(strMyDte)
    '        ''mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
    '        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
    '        mydte = Format(CDate(mydteX), "MM/dd/yyyy")

    '        my_Dte_End = Format(mydte, "MM/dd/yyyy")


    '        ' ***********************************************************

    '        If my_Dte_Start > my_Dte_End Then
    '            'Me.lblMsg.Text = "Error!. Start Date greater than End Date... "
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Start Date greater than End Date... "
    '            GoTo MyLoop_888
    '        End If

    '        ' ***********************************************************

    '        'my_Dte_DOB = Now
    '        'my_AGE = "21"
    '        'my_Dte_Start = Now
    '        'my_Dte_End = Now

    '        ' ***********************************************************

    '        If Trim(sFT) = "Y" Then
    '            sFT = "N"
    '            ' delete previous uploaded record
    '            mystr_sql = ""
    '            mystr_sql = "delete from TBIL_GRP_POLICY_MEMBERS"
    '            mystr_sql = mystr_sql & " where TBIL_POL_MEMB_FILE_NO = '" & RTrim(my_File_Num) & "'"
    '            mystr_sql = mystr_sql & " and TBIL_POL_MEMB_PROP_NO = '" & RTrim(my_Prop_Num) & "'"
    '            mystr_sql = mystr_sql & " and TBIL_POL_MEMB_BATCH_NO = '" & RTrim(Me.txtBatch_Num.Text) & "'"
    '            myole_cmd = New OleDbCommand(mystr_sql, myole_con)
    '            myole_cmd.CommandType = CommandType.Text
    '            myole_cmd.ExecuteNonQuery()
    '            myole_cmd.Dispose()
    '            myole_cmd = Nothing

    '            ' delete previous counter record
    '            mystr_sql = "delete from TBIL_UNDW_SYS_GEN_CNT where TBIL_SYS_GEN_CNT_ID = '" & RTrim(mystr_sn_param) & "' and TBIL_SYS_GEN_CNT_CODE = '" & RTrim(my_File_Num) & "'"
    '            myole_cmd = New OleDbCommand(mystr_sql, myole_con)
    '            myole_cmd.CommandType = CommandType.Text
    '            myole_cmd.ExecuteNonQuery()
    '            myole_cmd.Dispose()
    '            myole_cmd = Nothing

    '        End If

    '        dblPrem_Rate = 0
    '        dblPrem_Rate_Per = 1000
    '        dblPrem_Amt = 0
    '        dblPrem_Amt_ProRata = 0
    '        dblLoad_Amt = 0

    '        If Val(my_SA_Factor) = 0 Then
    '            my_SA_Factor = Val(Trim(Me.txtPrem_SA_Factor.Text))
    '        End If

    '        dblTotal_SA = CDbl(Trim(my_Total_Salary))
    '        If Val(my_SA_Factor) <> 0 Then
    '            dblTotal_SA = CDbl(Trim(my_Total_Salary)) * Val(Trim(my_SA_Factor))
    '        End If
    '        my_Total_SA = dblTotal_SA

    '        If dblTotal_SA >= dblFree_Cover_Limit Then
    '            my_Medical_YN = "Y"
    '        End If


    '        'Me.lblMsg.Text = xx.ToString
    '        'Response.Write("<br />row: " & nRow & " col: " & xx.ToString)

    '        my_Batch_Num = Me.txtBatch_Num.Text
    '        Me.txtBatch_Num.Enabled = False

    '        my_SNo = MOD_GEN.gnGet_Serial_No(RTrim("GET_SN_GL"), RTrim("GL_MEMBER_SN"), Trim(Me.txtFileNum.Text), Trim(Me.txtQuote_Num.Text))

    '        If Trim(my_Staff_Num) = "" Then
    '            my_Staff_Num = "STF_" & my_SNo.ToString
    '        End If

    '        Select Case UCase(Trim(Me.txtPrem_Rate_TypeNum.Text))
    '            Case "F"
    '                dblPrem_Rate = Val(Me.txtPrem_Rate.Text)
    '                dblPrem_Rate_Per = Val(Me.txtPrem_Rate_Per.Text)
    '            Case "N"
    '                dblPrem_Rate = "0.00"
    '                dblPrem_Rate_Per = "0"
    '            Case "T"
    '                myRetValue = MOD_GEN.gnGET_RATE("GET_GL_PREMIUM_RATE", "GRP", Me.txtPrem_Rate_Code.Text, Me.txtProduct_Num.Text, myTerm, Val(my_AGE), Me.lblMsg, Me.txtPrem_Rate_Per)
    '                If Left(LTrim(myRetValue), 3) = "ERR" Then
    '                    Me.cboPrem_Rate_Code.SelectedIndex = -1
    '                    'Me.txtPrem_Rate.Text = "0.00"
    '                    'Me.txtPrem_Rate_Per.Text = "0"
    '                    dblPrem_Rate = "0.00"
    '                    dblPrem_Rate_Per = "0"
    '                Else
    '                    'Me.txtPrem_Rate.Text = myRetValue.ToString
    '                    dblPrem_Rate = Trim(myRetValue.ToString)
    '                End If

    '        End Select


    '        'Response.Write("<br/>Value: " & dblPrem_Rate & " - " & myRetValue.ToString)

    '        'Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Rate)
    '        'Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Rate_Per)

    '        'dblPrem_Rate = CDbl(Trim(Me.txtPrem_Rate.Text))
    '        'dblPrem_Rate_Per = CDbl(Trim(Me.txtPrem_Rate_Per.Text))

    '        If dblTotal_SA <> 0 And dblPrem_Rate <> 0 And dblPrem_Rate_Per <> 0 Then
    '            dblPrem_Amt = dblTotal_SA * dblPrem_Rate / dblPrem_Rate_Per
    '            dblPrem_Amt_ProRata = dblPrem_Amt
    '        End If

    '        intRisk_Days = Val(DateDiff(DateInterval.Day, GenStart_Date, GenEnd_Date)) + 0
    '        intRisk_Days = Val(Me.txtRisk_Days.Text)
    '        'intDays_Diff = Val(DateDiff(DateInterval.Day, MemJoin_Date, GenEnd_Date)) + 0
    '        intDays_Diff = Val(DateDiff(DateInterval.Day, my_Dte_Start, my_Dte_End))

    '        If MemJoin_Date > GenStart_Date And dblPrem_Amt <> 0 And intDays_Diff <> 0 Then
    '            dblPrem_Amt_ProRata = Format((dblPrem_Amt / intRisk_Days) * intDays_Diff, "#########0.00")
    '        End If

    '        mystr_sql = "insert into table_name(fld1, fld1) values(@val1, @val2)"
    '        mystr_sql = "SPGL_TBIL_GRP_POLICY_MEMBERS_INSERT"

    '        myole_cmd = New OleDbCommand()
    '        myole_cmd.Connection = myole_con
    '        'myole_cmd.CommandType = CommandType.Text
    '        myole_cmd.CommandType = CommandType.StoredProcedure
    '        myole_cmd.CommandText = mystr_sql

    '        myole_cmd.Parameters.AddWithValue("@p01", RTrim(my_File_Num))
    '        myole_cmd.Parameters.AddWithValue("@p02", Val(0))
    '        myole_cmd.Parameters.AddWithValue("@p03", RTrim("G"))
    '        myole_cmd.Parameters.AddWithValue("@p04", RTrim(my_Prop_Num))
    '        myole_cmd.Parameters.AddWithValue("@p05", RTrim(my_Poly_Num))
    '        myole_cmd.Parameters.AddWithValue("@p05A", RTrim(my_Batch_Num))
    '        myole_cmd.Parameters.AddWithValue("@p05B", RTrim(my_Staff_Num))
    '        myole_cmd.Parameters.AddWithValue("@p06", Val(my_SNo))
    '        myole_cmd.Parameters.AddWithValue("@p07", RTrim(my_Gender))
    '        myole_cmd.Parameters.AddWithValue("@p08", Format(my_Dte_DOB, "MM/dd/yyyy"))
    '        myole_cmd.Parameters.AddWithValue("@p09", Val(my_AGE))
    '        myole_cmd.Parameters.AddWithValue("@p10", Format(my_Dte_Start, "MM/dd/yyyy"))
    '        myole_cmd.Parameters.AddWithValue("@p11", Format(my_Dte_End, "MM/dd/yyyy"))
    '        myole_cmd.Parameters.AddWithValue("@p12", Val(my_Tenor))
    '        myole_cmd.Parameters.AddWithValue("@p13", RTrim(my_Designation))
    '        myole_cmd.Parameters.AddWithValue("@p14", Left(RTrim(my_Member_Name), 95))
    '        myole_cmd.Parameters.AddWithValue("@p14A", CDbl(Trim(my_SA_Factor)))
    '        myole_cmd.Parameters.AddWithValue("@p14B", CDbl(Trim(my_Total_Salary)))
    '        myole_cmd.Parameters.AddWithValue("@p15", CDbl(Trim(my_Total_SA)))
    '        myole_cmd.Parameters.AddWithValue("@p16", RTrim(my_Medical_YN))

    '        myole_cmd.Parameters.AddWithValue("@p17", CDbl(dblPrem_Rate))
    '        myole_cmd.Parameters.AddWithValue("@p18", CDbl(dblPrem_Rate_Per))
    '        myole_cmd.Parameters.AddWithValue("@p19", CDbl(dblPrem_Amt))
    '        myole_cmd.Parameters.AddWithValue("@p20", CDbl(dblPrem_Amt_ProRata))
    '        myole_cmd.Parameters.AddWithValue("@p21", CDbl(dblLoad_Amt))

    '        myole_cmd.Parameters.AddWithValue("@p22", RTrim(Me.txtData_Source_SW.Text))
    '        myole_cmd.Parameters.AddWithValue("@p23", RTrim(Me.txtFile_Upload.Text))

    '        myole_cmd.Parameters.AddWithValue("@p24", vbNull)
    '        myole_cmd.Parameters.AddWithValue("@p25", RTrim("A"))
    '        myole_cmd.Parameters.AddWithValue("@p26", RTrim(myUserIDX))
    '        myole_cmd.Parameters.AddWithValue("@p27", Format(Now, "MM/dd/yyyy"))


    '        Try
    '            mycnt = myole_cmd.ExecuteNonQuery()
    '            If mycnt >= 1 Then
    '                my_intCNT = my_intCNT + 1
    '            Else
    '                strGen_Msg = " * Error!. Row: " & nRow.ToString & " record not save... "
    '            End If
    '        Catch ex As Exception
    '            strGen_Msg = " * Error while saving Row: " & nRow.ToString & " record... "

    '        End Try

    '        myole_cmd.Dispose()
    '        myole_cmd = Nothing

    'MyLoop_888:
    '        If strGen_Msg <> "" Then
    '            Me.cboErr_List.Items.Add(strGen_Msg.ToString)
    '            Me.lblErr_List.Visible = True
    '            Me.cboErr_List.Visible = True
    '        End If

    '        strGen_Msg = ""

    '        GoTo MyLoop_Start

    'MyLoop_999:

    '        Try
    '            ClientScript.RegisterStartupScript(Me.GetType(), "scrollMSG_JavaScript", "scrollMSG_End(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")", True)
    '        Catch ex As Exception

    '        End Try

    '        If my_intCNT >= 1 Then
    '            FirstMsg = "Javascript:alert('" & RTrim("File Upload successful - ") & Me.txtFile_Upload.Text & "')"
    '        Else
    '            FirstMsg = "Javascript:alert('" & RTrim("File Upload NOT successful - ") & Me.txtFile_Upload.Text & "')"
    '        End If

    'MyLoop_999a:
    '        If lstErrMsgs.Count > 1 Then
    '            For i = 0 To lstErrMsgs.Count - 1
    '                cboErr_List.Items.Add(lstErrMsgs.Item(i))
    '            Next

    '            Me.lblErr_List.Visible = True
    '            Me.cboErr_List.Visible = True


    '            FirstMsg = "Javascript:alert('" & RTrim("File Upload NOT successful - ") & Me.txtFile_Upload.Text & "')"

    '        Else
    '            Try
    '                ClientScript.RegisterStartupScript(Me.GetType(), "scrollMSG_JavaScript", "scrollMSG_End(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")", True)
    '            Catch ex As Exception

    '            End Try

    '            FirstMsg = "Javascript:alert('" & RTrim("File Upload successful - ") & Me.txtFile_Upload.Text & "')"

    '        End If
    '        GoTo MyLoop_End

    'MyLoop_End:


    '        myole_cmd = Nothing

    '        If myole_con.State = ConnectionState.Open Then
    '            myole_con.Close()
    '        End If
    '        myole_con = Nothing


    '        ''myxls_sheets = Nothing

    '        'myxls_worksheet = Nothing


    '        ''myxls_workbook.SaveAs(strSaveFilename, Excel.XlFileFormat.xlWorkbookDefault)
    '        ''myxls_workbook.Close(SaveChanges:=False)
    '        ''myxls_workbook.Close(False)

    '        'myxls_workbook.Close(False)
    '        'myxls_workbook = Nothing


    '        ''myxls_app.Workbooks.Close()
    '        'myxls_app.Quit()
    '        'myxls_app.Application.Quit()
    '        'myxls_app = Nothing


    '        'Call Proc_Batch()
    '        ' Call Proc_DataBind()
    '        GETMEMBERSBY_BATCHNO_POLYNO(txtPolNum.Text, txtFileNum.Text, txtQuote_Num.Text, txtBatch_Num.Text)


    '    End Sub

    Private Function Proc_ExcelDoc_New() As String
        Return String.Empty
    End Function

    Private Function Proc_Batch_Check() As Boolean

        '************************************************************
        ' START BATCH NUMBER EXIST CHECKING
        '************************************************************

        strTable = strTableName
        Dim mystr_con As String = ""
        Dim mystr_sql As String = ""
        Dim mybln As Boolean = False

        mybln = False

        Dim myole_con As OleDbConnection = Nothing
        Dim myole_cmd As OleDbCommand = Nothing

        mystr_con = CType(Session("connstr"), String)
        myole_con = New OleDbConnection(mystr_con)

        Try

            myole_con.Open()

            mystr_sql = ""
            mystr_sql = "SELECT TOP 1 TBIL_POL_MEMB_PROP_NO from " & strTable
            mystr_sql = mystr_sql & " where TBIL_POL_MEMB_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
            'mystr_sql = mystr_sql & " and TBIL_POL_MEMB_POLY_NO = '" & RTrim(Me.txtPolNum.Text) & "'"
            'mystr_sql = mystr_sql & " and TBIL_POL_MEMB_FILE_NO = '" & RTrim(Me.txtFileNum.Text) & "'"
            mystr_sql = mystr_sql & " and TBIL_POL_MEMB_BATCH_NO = '" & RTrim(Me.txtBatch_Num.Text) & "'"
            mystr_sql = mystr_sql & " and TBIL_POL_MEMB_STATUS IN('P')"

            myole_cmd = New OleDbCommand(mystr_sql, myole_con)
            myole_cmd.CommandType = CommandType.Text

            Dim myole_dr As OleDbDataReader = myole_cmd.ExecuteReader
            If myole_dr.Read() Then
                mybln = False
                Me.lblMsg.Text = "Sorry. The batch number you enter already exist. \nPlease enter unique batch number..."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Else
                mybln = True
            End If

            myole_cmd.Dispose()
            myole_dr = Nothing

        Catch ex As Exception
            mybln = False
            Me.lblMsg.Text = ex.Message
        End Try

        myole_cmd = Nothing
        myole_con.Close()
        myole_con = Nothing


        Return mybln
        Exit Function

        '************************************************************
        ' END BATCH NUMBER EXIST CHECKING
        '************************************************************

    End Function

    '    Private Sub Proc_DoSave_OLE()

    '        'Dim xlWSheet As Excel.Worksheet
    '        'Dim sVar As String = xlWSheet.Range("C5").Value.ToString()

    '        'GF/2014/1201/G/G001/G/0000001

    '        cboErr_List.Items.Clear()

    '        If Me.txtBatch_Num.Text = "" Then
    '            'Me.txtFile_Upload.Text = ""
    '            'Me.cmdFile_Upload.Enabled = False
    '            Me.lblMsg.Text = "Missing " & Me.lblBatch_Num.Text
    '            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            Exit Sub
    '        End If

    '        If Val(Trim(Me.txtXLS_Data_Start_No.Text)) < 1 Then
    '            Me.lblMsg.Text = "Error. Minimum start excel no should be 1 "
    '            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            Exit Sub
    '        End If
    '        If Val(Trim(Me.txtXLS_Data_End_No.Text)) < 1 Or Val(Trim(Me.txtXLS_Data_End_No.Text)) < Val(Trim(Me.txtXLS_Data_Start_No.Text)) Then
    '            Me.lblMsg.Text = "Error. Either excel end no less than 1 or less than excel start no "
    '            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            Exit Sub
    '        End If

    '        blnStatusX = Proc_Batch_Check()
    '        If blnStatusX = False Then
    '            Exit Sub
    '        End If

    '        Me.lblMsg.Text = "File Name: " & Me.txtFile_Upload.Text
    '        'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

    '        If Trim(Me.txtFile_Upload.Text) = "" Then
    '            Me.txtFile_Upload.Text = ""
    '            Me.lblMsg.Text = "Missing document or file name ..."
    '            FirstMsg = "Javascript:alert('Missing document or file name. \nPlease select excel document with file extension .XLS or .XLSX')"
    '            Exit Sub
    '        End If

    '        If Right(LCase(Trim(Me.txtFile_Upload.Text)), 3) = "xls" Or _
    '           Right(LCase(Trim(Me.txtFile_Upload.Text)), 4) = "xlsx" Then
    '        Else
    '            Me.txtFile_Upload.Text = ""
    '            Me.lblMsg.Text = "Invalid document or file type. Expecting file of type .XLS or .XLSX ..."
    '            FirstMsg = "Javascript:alert('Invalid document or file name. \nPlease select excel document with file extension .XLS or .XLSX')"
    '            Exit Sub
    '        End If


    '        'strPATH = CType(ConfigurationManager.ConnectionStrings("LIFE_DOC_PATH").ToString, String)
    '        'strPATH = CType(ConfigurationManager.AppSettings("LIFE_DOC_PATH").ToString, String)

    '        Dim strFilename As String
    '        Dim strFileNameOnly As String = txtFile_Upload.Text
    '        'strFilename = strPATH & Me.txtFile_Upload.Text
    '        strPATH = Server.MapPath("~/App_Data/Schedules/")
    '        strFilename = strPATH & Me.txtFile_Upload.Text

    '        If System.IO.File.Exists(strFilename) = False Then
    '            Me.lblMsg.Text = "Document or file does not exist on the server ..."
    '            FirstMsg = "Javascript:alert('Document or file does not exist on the server')"
    '            Exit Sub
    '        End If

    '        Me.cmdFile_Upload.Enabled = False
    '        'Me.lblMsg.Text = UCase("File Upload successful.")

    '        'Try

    '        'Dim myxls_app_Demo As Microsoft.Office.Interop.Excel.Application = Nothing
    '        'myxls_app_Demo = New Microsoft.Office.Interop.Excel.Application
    '        'Dim myxls_app_Demo As Excel.Application
    '        'myxls_app_Demo = New Excel.Application()

    '        'myxls_app_Demo.Quit()
    '        'myxls_app_Demo.Application.Quit()
    '        'myxls_app_Demo = Nothing
    '        'Catch ex As Exception
    '        'Me.lblMsg.Text = "Error has occured. Reason: " & UCase(ex.Message.ToString)
    '        'FirstMsg = "Javascript:alert('" & RTrim("Unable to declare Excel object") & "')"
    '        'Exit Sub

    '        'End Try


    '        sFT = "Y"

    '        nRow = 2
    '        nCol = 0

    '        my_intCNT = 0

    '        'Dim key As Object
    '        ' Dim returnValue As Object

    '        '      ' ************************************************************************
    '        '      ' OK
    '        '      Dim app As Excel.Application = New Excel.Application()
    '        '      Dim workbook As Excel.Workbook
    '        '      Dim worksheet As Excel.Worksheet

    '        '      Dim xlsrange As Excel.Range

    '        '      Dim intC As Integer = 0

    '        '      strFilename = "H:\ABS-WEB\NIMASAOL\Database\Data Upload.xls"

    '        '      workbook = app.Workbooks.Open(strFilename, _
    '        'Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _
    '        'Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)


    '        '      'workbook = app.Workbooks.Open(strFilename)
    '        '      worksheet = workbook.Worksheets(1)

    '        '      'If (worksheet.Cells(1, 1).ToString() = "") Then
    '        '      'End If
    '        '      'Dim strname As String = worksheet.Cells(1, 2).ToString()
    '        '      'Response.Write("<br/>Cell Data " & strname)
    '        '      'Response.Write("<br/>Row: " & nRow & " - Col: " & worksheet.Cells(1, 3).ToString())

    '        '      For nRow = 1 To 5
    '        '
    '        '          xlsrange = worksheet.Cells(nRow, 4)
    '        '          'If (xlsrange Is Nothing Or xlsrange.Value2 Is Nothing) Then
    '        '          'Response.Write("<br/>Range object is null...")
    '        '          'Else
    '        '          Response.Write("<br/>Row: " & nRow & " - Range Value: " & xlsrange.Value.ToString() & " - Range Value2: " & xlsrange.Value2.ToString())
    '        '          'End If

    '        '      Next

    '        '      ' ************************************************************************


    '        'Try
    '        '    Dim xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
    '        '    xlApp = CType(CreateObject("Excel.Application"), Microsoft.Office.Interop.Excel.Application)
    '        'Catch ex As Exception
    '        '    Me.lblMsg.Text = "Error has occured. Reason: " & UCase(ex.Message.ToString)
    '        '    Response.Write("<br />Unable to create excel object. Reason: <br />" & Me.lblMsg.Text)
    '        '    'FirstMsg = "Javascript:alert('" & RTrim("Unable to create Excel object") & "')"
    '        '    'Exit Sub

    '        'End Try


    '        'Dim xlBook As Microsoft.Office.Interop.Excel.Workbook
    '        'Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet

    '        'xlBook = CType(xlApp.Workbooks.Add,  _
    '        '            Microsoft.Office.Interop.Excel.Workbook)
    '        'xlSheet = CType(xlBook.Worksheets(1),  _
    '        '            Microsoft.Office.Interop.Excel.Worksheet)

    '        '' The following statement puts text in the second row of the sheet.
    '        'xlSheet.Cells(2, 2) = "This is column B row 2"
    '        '' The following statement shows the sheet.
    '        'xlSheet.Application.Visible = True
    '        '' The following statement saves the sheet to the C:\Test.xls directory.
    '        'xlSheet.SaveAs("C:\Test.xls")

    '        '' Optionally, you can call xlApp.Quit to close the workbook.
    '        ''xlApp.Quit()

    '        'Dim myxls_app As Excel.Application = New Excel.Application()
    '        Dim myxls_workbook As Excel.Workbook
    '        Dim myxls_worksheet As Excel.Worksheet

    '        Dim myxls_range As Excel.Range


    '        ''Dim myxls_app As Microsoft.Office.Interop.Excel.Application
    '        ''Dim myxls_workbook As Microsoft.Office.Interop.Excel.Workbook
    '        ''Dim myxls_worksheet As Microsoft.Office.Interop.Excel.Worksheet

    '        ''Dim myxls_sheets As Microsoft.Office.Interop.Excel.Sheets
    '        ''Dim myxls_range As Microsoft.Office.Interop.Excel.Range


    '        '' *******************
    '        ''myxls_app = New Microsoft.Office.Interop.Excel.Application
    '        'myxls_app = New Excel.Application()

    '        ''myxls_workbook = New Microsoft.Office.Interop.Excel.Workbook


    '        Try
    '            ' myxls_workbook = myxls_app.Workbooks.Open(strFilename, , ReadOnly:=True)
    '            ' myxls_workbook = myxls_app.Workbooks.Open("c:\xlsdoc.xlsx")
    '            'myxls_workbook = myxls_app.Workbooks.Open(strFilename)

    '        Catch ex As Exception

    '            'myxls_worksheet = Nothing

    '            '    'myxls_workbook.SaveAs(strSaveFilename, Excel.XlFileFormat.xlWorkbookDefault)
    '            '    'myxls_workbook.Close(SaveChanges:=False)
    '            '    'myxls_workbook.Close(False)

    '            '    'myxls_workbook.Close(False)
    '            'myxls_workbook = Nothing


    '            '    'myxls_app.Workbooks.Close()
    '            'myxls_app.Quit()
    '            'myxls_app.Application.Quit()
    '            'myxls_app = Nothing

    '            Me.lblMsg.Text = "Error has occured. Reason: " & UCase(ex.Message.ToString)
    '            FirstMsg = "Javascript:alert('" & RTrim("Unable to access data") & "')"
    '            Exit Sub

    '        End Try


    '        ''   open workbook
    '        ''myxls_worksheet = myxls_app.Worksheets(1)
    '        ''myxls_worksheet = myxls_workbook.Sheets("sheetname_or_indexno")
    '        ''myxls_worksheet = myxls_workbook.Sheets("Sheet1")
    '        ''myxls_worksheet = myxls_workbook.Sheets(1)
    '        'myxls_worksheet = myxls_workbook.Worksheets(1)



    '        ''myxls_sheets = myxls_workbook.Sheets()
    '        ''myxls_sheets.Item(1)

    '        'myxls_range = myxls_worksheet.Cells


    '        Dim mystr_con As String = CType(Session("connstr"), String)
    '        Dim myole_con As OleDbConnection = New OleDbConnection(mystr_con)

    '        Try
    '            myole_con.Open()
    '        Catch ex As Exception
    '            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
    '            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
    '            FirstMsg = "Javascript:alert('" & "Unable to connect to database" & "')"

    '            GoTo MyLoop_End
    '        End Try


    '        Dim myUserIDX As String = ""
    '        Try
    '            myUserIDX = CType(Session("MyUserIDX"), String)
    '        Catch ex As Exception
    '            myUserIDX = "SYS"
    '        End Try


    '        Dim mystr_sql As String = ""
    '        Dim mystr_sn_param As String = ""
    '        Dim mycnt As Integer = 0

    '        mystr_sn_param = "GL_MEMBER_SN"

    '        my_File_Num = Me.txtFileNum.Text
    '        my_Prop_Num = Me.txtQuote_Num.Text
    '        my_Poly_Num = Me.txtPolNum.Text
    '        my_Batch_Num = Me.txtBatch_Num.Text


    '        strGen_Msg = ""
    '        Me.lblErr_List.Visible = False
    '        Me.cboErr_List.Items.Clear()
    '        Me.cboErr_List.Visible = False

    '        my_intCNT = 0

    '        Dim myole_cmd As OleDbCommand = Nothing

    '        nROW_MIN = Val(Me.txtXLS_Data_Start_No.Text)
    '        nROW_MAX = Val(Me.txtXLS_Data_End_No.Text)
    '        nRow = 2

    '        Try
    '            'ClientScript.RegisterStartupScript(Me.GetType(), "scrollMSG_JavaScript", "scrollMSG(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")", True)
    '        Catch ex As Exception

    '        End Try
    '        '*************************************************************************************
    '        'Gather the validated values from the form and pass 
    '        'to the hashHelper function
    '        '*************************************************************************************


    '        'call the hashhelper function and pass the form values into it
    '        hashHelper.postFromExcel(strPATH, txtFile_Upload.Text.Trim, myUserIDX, my_Batch_Num, nROW_MIN, nROW_MAX, Me.txtPrem_Period_Yr.Text, mystr_con, _
    '       Me.txtPrem_SA_Factor.Text, my_File_Num, my_Prop_Num, my_Poly_Num, txtPrem_Rate_TypeNum.Text, txtPrem_Rate_Per.Text, txtPrem_Rate_Code.Text, _
    '       txtProduct_Num.Text, lstErrMsgs, Convert.ToInt16(txtRisk_Days.Text), 0, GenStart_Date, GenEnd_Date, txtStart_Date.Text, txtEnd_Date.Text, _
    '       MemJoin_Date, txtData_Source_SW.Text, txtPrem_Rate.Text, String.Empty)
    '        GoTo MyLoop_999a



    'MyLoop_Start:
    '        nRow = nRow + 1

    '        If nRow < nROW_MIN Then
    '            GoTo MyLoop_Start
    '        End If

    '        If nRow > nROW_MAX Then
    '            GoTo MyLoop_999
    '        End If

    '        'If nRow <= 2 Then
    '        '    GoTo MyLoop_Start
    '        'End If

    '        'xx = myxls_worksheet.Cells(nRow, 1).ToString
    '        'If Val(xx) = 0 Then
    '        'GoTo MyLoop_Start
    '        'End If

    '        xx = ""
    '        'xx = myxls_worksheet.Cells(nRow, 3).ToString

    '        myxls_range = myxls_worksheet.Cells(nRow, 3)
    '        xx = myxls_range.Text.ToString
    '        'xx = myxls_range.Item(nRow, 3)

    '        If Trim(xx.ToString) = "" Then
    '            GoTo MyLoop_Start
    '        End If


    '        ' The following statement puts text in the second row of the sheet.
    '        ' xlSheet.Cells(2, 2) = "This is column B row 2"

    '        'ok
    '        'xx = myxls_worksheet.Cells(nRow, 3).Text.ToString


    '        ' Initialize variables
    '        strGen_Msg = ""

    '        'my_File_Num = ""
    '        my_Staff_Num = ""
    '        my_Member_Name = ""
    '        my_DOB = ""
    '        my_AGE = ""
    '        my_Gender = ""
    '        my_Designation = ""
    '        my_Start_Date = ""
    '        my_End_Date = ""
    '        my_Tenor = "1"
    '        my_Tenor = Me.txtPrem_Period_Yr.Text
    '        my_SA_Factor = Val(Trim(Me.txtPrem_SA_Factor.Text))
    '        my_Basic_Sal = Val(0)
    '        my_House_Allow = Val(0)
    '        my_Transport_Allow = Val(0)
    '        my_Other_Allow = Val(0)
    '        my_Total_Salary = Val(0)
    '        my_Total_SA = Val(0)
    '        my_Medical_YN = "N"


    '        'myxls_range = myxls_worksheet.Cells(nRow, 1)
    '        'my_SNo = myxls_range.Text.ToString

    '        myxls_range = myxls_worksheet.Cells(nRow, 2)
    '        my_Staff_Num = myxls_range.Text.ToString

    '        myxls_range = myxls_worksheet.Cells(nRow, 3)
    '        my_Member_Name = myxls_range.Text.ToString

    '        ' ******************
    '        ' START DOB
    '        ' ******************
    '        Try
    '            myxls_range = myxls_worksheet.Cells(nRow, 4)
    '            my_DOB = myxls_range.Text.ToString
    '            'my_DOB = Format(myxls_range.Text, "dd/MM/yyyy")
    '            'my_DOB = CDate(my_DOB).ToString
    '            If Not IsDate(my_DOB) Then
    '                'my_DOB = Format(CDate(my_DOB), "dd/MM/yyyy")
    '            End If

    '        Catch ex As Exception
    '            myxls_range = myxls_worksheet.Cells(nRow, 4)
    '            my_DOB = CType(myxls_range.Text, String)
    '            'my_DOB = Format(myxls_range.Text, "dd/MM/yyyy")
    '        End Try
    '        If Val(Mid(my_DOB, 4, 2)) > 12 Then
    '            'my_DOB = Mid(LTrim(my_DOB), 4, 2) & "/" & Left(LTrim(my_DOB), 2) & "/" & Right(RTrim(my_DOB), 4)
    '        End If
    '        ' ******************
    '        ' END DOB
    '        ' ******************

    '        myxls_range = myxls_worksheet.Cells(nRow, 5)
    '        my_AGE = myxls_range.Text.ToString

    '        myxls_range = myxls_worksheet.Cells(nRow, 6)
    '        my_Gender = myxls_range.Text.ToString

    '        myxls_range = myxls_worksheet.Cells(nRow, 7)
    '        my_Designation = myxls_range.Text.ToString

    '        myxls_range = myxls_worksheet.Cells(nRow, 8)
    '        my_Start_Date = myxls_range.Text.ToString

    '        myxls_range = myxls_worksheet.Cells(nRow, 9)
    '        my_End_Date = myxls_range.Text.ToString

    '        myxls_range = myxls_worksheet.Cells(nRow, 10)
    '        my_Tenor = myxls_range.Text.ToString

    '        myxls_range = myxls_worksheet.Cells(nRow, 11)
    '        my_SA_Factor = Val(myxls_range.Text.ToString)

    '        myxls_range = myxls_worksheet.Cells(nRow, 12)
    '        Try
    '            my_Basic_Sal = Val(myxls_range.Text.ToString)
    '        Catch ex As Exception
    '            my_Basic_Sal = Val(0)
    '        End Try

    '        myxls_range = myxls_worksheet.Cells(nRow, 13)
    '        Try
    '            my_House_Allow = Val(myxls_range.Text.ToString)
    '        Catch ex As Exception
    '            my_House_Allow = Val(0)
    '        End Try

    '        myxls_range = myxls_worksheet.Cells(nRow, 14)
    '        Try
    '            my_Transport_Allow = Val(myxls_range.Text.ToString)
    '        Catch ex As Exception
    '            my_Transport_Allow = Val(0)
    '        End Try

    '        myxls_range = myxls_worksheet.Cells(nRow, 15)
    '        Try
    '            my_Other_Allow = Val(myxls_range.Text.ToString)
    '        Catch ex As Exception
    '            my_Other_Allow = Val(0)
    '        End Try

    '        myxls_range = myxls_worksheet.Cells(nRow, 16)
    '        Try
    '            my_Total_Salary = Val(myxls_range.Text.ToString)
    '        Catch ex As Exception
    '            my_Total_Salary = Val(0)
    '        End Try

    '        my_Total_SA = 0

    '        my_Tenor = Me.txtPrem_Period_Yr.Text
    '        myTerm = my_Tenor


    '        ' Response.Write("<br />Start Date: " & my_Start_Date & " - End Date: " & my_End_Date & " - DOB Date: " & my_DOB)

    '        'Validate date
    '        myarrData = Split(my_DOB, "/")
    '        If myarrData.Count <> 3 Then
    '            'Me.lblMsg.Text = "Missing or Invalid " & Me.lblMember_DOB.Text & ". Expecting full date in ddmmyyyy format ..."
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Incomplete date of birth - " & my_DOB.ToString
    '            GoTo MyLoop_888
    '        End If

    '        strMyDay = myarrData(0)
    '        strMyMth = myarrData(1)
    '        strMyYear = Left(myarrData(2), 4)

    '        strMyDay = CType(Format(Val(strMyDay), "00"), String)
    '        strMyMth = CType(Format(Val(strMyMth), "00"), String)
    '        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

    '        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

    '        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
    '        If blnStatusX = False Then
    '            'Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Invalid date of birth - " & strMyDte.ToString
    '            GoTo MyLoop_888
    '        End If

    '        'Me.txtMember_DOB.Text = RTrim(strMyDte)
    '        ''mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
    '        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
    '        mydte = Format(CDate(mydteX), "MM/dd/yyyy")

    '        my_Dte_DOB = Format(mydte, "MM/dd/yyyy")
    '        Dte_DOB = my_Dte_DOB

    '        Dte_Current = Now
    '        lngDOB_ANB = Val(DateDiff("yyyy", Dte_Current, my_Dte_DOB))
    '        If lngDOB_ANB < 0 Then
    '            lngDOB_ANB = lngDOB_ANB * -1
    '        End If

    '        If Dte_Current.Month >= Dte_DOB.Month Then
    '            lngDOB_ANB = lngDOB_ANB + 1
    '        End If
    '        If Val(my_AGE) = 0 Or Trim(my_AGE) = "" Then
    '            my_AGE = Trim(Str(lngDOB_ANB))
    '        End If

    '        ' ***********************************************************


    '        'Validate date
    '        myarrData = Split(my_Start_Date, "/")
    '        If myarrData.Count <> 3 Then
    '            'Me.lblMsg.Text = "Missing or Invalid " & Me.lblStart_Date.Text & ". Expecting full date in ddmmyyyy format ..."
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Incomplete start date - " & my_Start_Date.ToString
    '            GoTo MyLoop_888
    '        End If

    '        strMyDay = myarrData(0)
    '        strMyMth = myarrData(1)
    '        strMyYear = Left(myarrData(2), 4)

    '        strMyDay = CType(Format(Val(strMyDay), "00"), String)
    '        strMyMth = CType(Format(Val(strMyMth), "00"), String)
    '        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

    '        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

    '        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
    '        If blnStatusX = False Then
    '            'Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Invalid start date - " & strMyDte.ToString
    '            GoTo MyLoop_888
    '        End If

    '        'Me.txtMember_DOB.Text = RTrim(strMyDte)
    '        ''mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
    '        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
    '        mydte = Format(CDate(mydteX), "MM/dd/yyyy")

    '        my_Dte_Start = Format(mydte, "MM/dd/yyyy")
    '        MemJoin_Date = my_Dte_Start


    '        ' ***********************************************************

    '        'Validate date
    '        myarrData = Split(my_End_Date, "/")
    '        If myarrData.Count <> 3 Then
    '            'Me.lblMsg.Text = "Missing or Invalid " & Me.lblEnd_Date.Text & ". Expecting full date in ddmmyyyy format ..."
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Incomplete end date - " & my_End_Date.ToString
    '            GoTo MyLoop_888
    '        End If

    '        strMyDay = myarrData(0)
    '        strMyMth = myarrData(1)
    '        strMyYear = Left(myarrData(2), 4)

    '        strMyDay = CType(Format(Val(strMyDay), "00"), String)
    '        strMyMth = CType(Format(Val(strMyMth), "00"), String)
    '        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

    '        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

    '        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
    '        If blnStatusX = False Then
    '            'Me.lblMsg.Text = "Please enter valid date..."
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Invalid end date - " & strMyDte.ToString
    '            GoTo MyLoop_888
    '        End If

    '        'Me.txtEnd_Date.Text = RTrim(strMyDte)
    '        ''mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
    '        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
    '        mydte = Format(CDate(mydteX), "MM/dd/yyyy")

    '        my_Dte_End = Format(mydte, "MM/dd/yyyy")


    '        ' ***********************************************************

    '        If my_Dte_Start > my_Dte_End Then
    '            'Me.lblMsg.Text = "Error!. Start Date greater than End Date... "
    '            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
    '            'Exit Sub
    '            strGen_Msg = " * Row: " & nRow.ToString & " - Start Date greater than End Date... "
    '            GoTo MyLoop_888
    '        End If


    '        If Trim(sFT) = "Y" Then
    '            sFT = "N"
    '            ' delete previous uploaded record
    '            mystr_sql = ""
    '            mystr_sql = "delete from TBIL_GRP_POLICY_MEMBERS"
    '            mystr_sql = mystr_sql & " where TBIL_POL_MEMB_FILE_NO = '" & RTrim(my_File_Num) & "'"
    '            mystr_sql = mystr_sql & " and TBIL_POL_MEMB_PROP_NO = '" & RTrim(my_Prop_Num) & "'"
    '            mystr_sql = mystr_sql & " and TBIL_POL_MEMB_BATCH_NO = '" & RTrim(Me.txtBatch_Num.Text) & "'"
    '            myole_cmd = New OleDbCommand(mystr_sql, myole_con)
    '            myole_cmd.CommandType = CommandType.Text
    '            myole_cmd.ExecuteNonQuery()
    '            myole_cmd.Dispose()
    '            myole_cmd = Nothing

    '            ' delete previous counter record
    '            mystr_sql = "delete from TBIL_UNDW_SYS_GEN_CNT where TBIL_SYS_GEN_CNT_ID = '" & RTrim(mystr_sn_param) & "' and TBIL_SYS_GEN_CNT_CODE = '" & RTrim(my_File_Num) & "'"
    '            myole_cmd = New OleDbCommand(mystr_sql, myole_con)
    '            myole_cmd.CommandType = CommandType.Text
    '            myole_cmd.ExecuteNonQuery()
    '            myole_cmd.Dispose()
    '            myole_cmd = Nothing

    '        End If

    '        dblPrem_Rate = 0
    '        dblPrem_Rate_Per = 1000
    '        dblPrem_Amt = 0
    '        dblPrem_Amt_ProRata = 0
    '        dblLoad_Amt = 0

    '        If Val(my_SA_Factor) = 0 Then
    '            my_SA_Factor = Val(Trim(Me.txtPrem_SA_Factor.Text))
    '        End If

    '        dblTotal_SA = CDbl(Trim(my_Total_Salary))
    '        If Val(my_SA_Factor) <> 0 Then
    '            dblTotal_SA = CDbl(Trim(my_Total_Salary)) * Val(Trim(my_SA_Factor))
    '        End If
    '        my_Total_SA = dblTotal_SA

    '        If dblTotal_SA >= dblFree_Cover_Limit Then
    '            my_Medical_YN = "Y"
    '        End If


    '        'Me.lblMsg.Text = xx.ToString
    '        'Response.Write("<br />row: " & nRow & " col: " & xx.ToString)

    '        my_Batch_Num = Me.txtBatch_Num.Text
    '        Me.txtBatch_Num.Enabled = False

    '        my_SNo = MOD_GEN.gnGet_Serial_No(RTrim("GET_SN_GL"), RTrim("GL_MEMBER_SN"), Trim(Me.txtFileNum.Text), Trim(Me.txtQuote_Num.Text))

    '        If Trim(my_Staff_Num) = "" Then
    '            my_Staff_Num = "STF_" & my_SNo.ToString
    '        End If

    '        Select Case UCase(Trim(Me.txtPrem_Rate_TypeNum.Text))
    '            Case "F"
    '                dblPrem_Rate = Val(Me.txtPrem_Rate.Text)
    '                dblPrem_Rate_Per = Val(Me.txtPrem_Rate_Per.Text)
    '            Case "N"
    '                dblPrem_Rate = "0.00"
    '                dblPrem_Rate_Per = "0"
    '            Case "T"
    '                myRetValue = MOD_GEN.gnGET_RATE("GET_GL_PREMIUM_RATE", "GRP", Me.txtPrem_Rate_Code.Text, Me.txtProduct_Num.Text, myTerm, Val(my_AGE), Me.lblMsg, Me.txtPrem_Rate_Per)
    '                If Left(LTrim(myRetValue), 3) = "ERR" Then
    '                    Me.cboPrem_Rate_Code.SelectedIndex = -1
    '                    'Me.txtPrem_Rate.Text = "0.00"
    '                    'Me.txtPrem_Rate_Per.Text = "0"
    '                    dblPrem_Rate = "0.00"
    '                    dblPrem_Rate_Per = "0"
    '                Else
    '                    'Me.txtPrem_Rate.Text = myRetValue.ToString
    '                    dblPrem_Rate = Trim(myRetValue.ToString)
    '                End If

    '        End Select


    '        'Response.Write("<br/>Value: " & dblPrem_Rate & " - " & myRetValue.ToString)

    '        'Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Rate)
    '        'Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Rate_Per)

    '        'dblPrem_Rate = CDbl(Trim(Me.txtPrem_Rate.Text))
    '        'dblPrem_Rate_Per = CDbl(Trim(Me.txtPrem_Rate_Per.Text))

    '        If dblTotal_SA <> 0 And dblPrem_Rate <> 0 And dblPrem_Rate_Per <> 0 Then
    '            dblPrem_Amt = dblTotal_SA * dblPrem_Rate / dblPrem_Rate_Per
    '            dblPrem_Amt_ProRata = dblPrem_Amt
    '        End If

    '        intRisk_Days = Val(DateDiff(DateInterval.Day, GenStart_Date, GenEnd_Date)) + 0
    '        intRisk_Days = Val(Me.txtRisk_Days.Text)
    '        'intDays_Diff = Val(DateDiff(DateInterval.Day, MemJoin_Date, GenEnd_Date)) + 0
    '        intDays_Diff = Val(DateDiff(DateInterval.Day, my_Dte_Start, my_Dte_End))

    '        If MemJoin_Date > GenStart_Date And dblPrem_Amt <> 0 And intDays_Diff <> 0 Then
    '            dblPrem_Amt_ProRata = Format((dblPrem_Amt / intRisk_Days) * intDays_Diff, "#########0.00")
    '        End If




    '        '**********************************************************************
    '        ' Below is Sunkanmi's attempt to write into the DB schedule but
    '        ' some of the values will be picked from the form and sent into the 
    '        ' hashHelper function call built by James. This is done just before the 
    '        ' entry into the my_loop_start
    '        '**********************************************************************
    '        'mystr_sql = "insert into table_name(fld1, fld1) values(@val1, @val2)"

    '        'mystr_sql = "SPGL_TBIL_GRP_POLICY_MEMBERS_INSERT"

    '        'myole_cmd = New OleDbCommand()
    '        'myole_cmd.Connection = myole_con
    '        ''myole_cmd.CommandType = CommandType.Text
    '        'myole_cmd.CommandType = CommandType.StoredProcedure
    '        'myole_cmd.CommandText = mystr_sql

    '        'myole_cmd.Parameters.AddWithValue("@p01", RTrim(my_File_Num))
    '        'myole_cmd.Parameters.AddWithValue("@p02", Val(0))
    '        'myole_cmd.Parameters.AddWithValue("@p03", RTrim("G"))
    '        'myole_cmd.Parameters.AddWithValue("@p04", RTrim(my_Prop_Num))
    '        'myole_cmd.Parameters.AddWithValue("@p05", RTrim(my_Poly_Num))
    '        'myole_cmd.Parameters.AddWithValue("@p05A", RTrim(my_Batch_Num))
    '        'myole_cmd.Parameters.AddWithValue("@p05B", RTrim(my_Staff_Num))
    '        'myole_cmd.Parameters.AddWithValue("@p06", Val(my_SNo))
    '        'myole_cmd.Parameters.AddWithValue("@p07", RTrim(my_Gender))
    '        'myole_cmd.Parameters.AddWithValue("@p08", Format(my_Dte_DOB, "MM/dd/yyyy"))
    '        'myole_cmd.Parameters.AddWithValue("@p09", Val(my_AGE))
    '        'myole_cmd.Parameters.AddWithValue("@p10", Format(my_Dte_Start, "MM/dd/yyyy"))
    '        'myole_cmd.Parameters.AddWithValue("@p11", Format(my_Dte_End, "MM/dd/yyyy"))
    '        'myole_cmd.Parameters.AddWithValue("@p12", Val(my_Tenor))
    '        'myole_cmd.Parameters.AddWithValue("@p13", RTrim(my_Designation))
    '        'myole_cmd.Parameters.AddWithValue("@p14", Left(RTrim(my_Member_Name), 95))
    '        'myole_cmd.Parameters.AddWithValue("@p14A", CDbl(Trim(my_SA_Factor)))
    '        'myole_cmd.Parameters.AddWithValue("@p14B", CDbl(Trim(my_Total_Salary)))
    '        'myole_cmd.Parameters.AddWithValue("@p15", CDbl(Trim(my_Total_SA)))
    '        'myole_cmd.Parameters.AddWithValue("@p16", RTrim(my_Medical_YN))

    '        'myole_cmd.Parameters.AddWithValue("@p17", CDbl(dblPrem_Rate))
    '        'myole_cmd.Parameters.AddWithValue("@p18", CDbl(dblPrem_Rate_Per))
    '        'myole_cmd.Parameters.AddWithValue("@p19", CDbl(dblPrem_Amt))
    '        'myole_cmd.Parameters.AddWithValue("@p20", CDbl(dblPrem_Amt_ProRata))
    '        'myole_cmd.Parameters.AddWithValue("@p21", CDbl(dblLoad_Amt))

    '        'myole_cmd.Parameters.AddWithValue("@p22", RTrim(Me.txtData_Source_SW.Text))
    '        'myole_cmd.Parameters.AddWithValue("@p23", RTrim(Me.txtFile_Upload.Text))

    '        'myole_cmd.Parameters.AddWithValue("@p24", vbNull)
    '        'myole_cmd.Parameters.AddWithValue("@p25", RTrim("A"))
    '        'myole_cmd.Parameters.AddWithValue("@p26", RTrim(myUserIDX))
    '        'myole_cmd.Parameters.AddWithValue("@p27", Format(Now, "MM/dd/yyyy"))


    '        'Try
    '        '    mycnt = myole_cmd.ExecuteNonQuery()
    '        '    If mycnt >= 1 Then
    '        '        my_intCNT = my_intCNT + 1
    '        '    Else
    '        '        strGen_Msg = " * Error!. Row: " & nRow.ToString & " record not save... "
    '        '    End If
    '        'Catch ex As Exception
    '        '    strGen_Msg = " * Error while saving Row: " & nRow.ToString & " record... "

    '        'End Try

    '        'myole_cmd.Dispose()
    '        'myole_cmd = Nothing








    'MyLoop_888:
    '        If strGen_Msg <> "" Then
    '            Me.cboErr_List.Items.Add(strGen_Msg.ToString)
    '            Me.lblErr_List.Visible = True
    '            Me.cboErr_List.Visible = True
    '        End If

    '        strGen_Msg = ""

    '        GoTo MyLoop_Start


    'MyLoop_999:

    '        Try
    '            ClientScript.RegisterStartupScript(Me.GetType(), "scrollMSG_JavaScript", "scrollMSG_End(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")", True)
    '        Catch ex As Exception

    '        End Try

    '        If my_intCNT >= 1 Then
    '            FirstMsg = "Javascript:alert('" & RTrim("File Upload successful - ") & Me.txtFile_Upload.Text & "')"
    '        Else
    '            FirstMsg = "Javascript:alert('" & RTrim("File Upload NOT successful - ") & Me.txtFile_Upload.Text & "')"
    '        End If

    'MyLoop_999a:
    '        If lstErrMsgs.Count > 1 Then
    '            For i = 0 To lstErrMsgs.Count - 1
    '                cboErr_List.Items.Add(lstErrMsgs.Item(i))
    '            Next

    '            Me.lblErr_List.Visible = True
    '            Me.cboErr_List.Visible = True


    '            FirstMsg = "Javascript:alert('" & RTrim("File Upload NOT successful - ") & Me.txtFile_Upload.Text & "')"

    '        Else
    '            Try
    '                ClientScript.RegisterStartupScript(Me.GetType(), "scrollMSG_JavaScript", "scrollMSG_End(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")", True)
    '            Catch ex As Exception

    '            End Try

    '            FirstMsg = "Javascript:alert('" & RTrim("File Upload successful - ") & Me.txtFile_Upload.Text & "')"

    '        End If
    '        GoTo MyLoop_End

    'MyLoop_End:


    '        myole_cmd = Nothing

    '        If myole_con.State = ConnectionState.Open Then
    '            myole_con.Close()
    '        End If
    '        myole_con = Nothing


    '        ''myxls_sheets = Nothing

    '        myxls_worksheet = Nothing


    '        ''myxls_workbook.SaveAs(strSaveFilename, Excel.XlFileFormat.xlWorkbookDefault)
    '        ''myxls_workbook.Close(SaveChanges:=False)
    '        ''myxls_workbook.Close(False)

    '        'myxls_workbook.Close(False)
    '        'myxls_workbook = Nothing


    '        'myxls_app.Workbooks.Close()
    '        'myxls_app.Quit()
    '        'myxls_app.Application.Quit()
    '        'myxls_app = Nothing


    '        'Call Proc_Batch()
    '        'Call Proc_DataBind()
    '        GETMEMBERSBY_BATCHNO_POLYNO(txtPolNum.Text, txtFileNum.Text, txtQuote_Num.Text, txtBatch_Num.Text)



    '    End Sub


    Public Sub GET_POLICYDATE_BY_FILENO(ByVal fileNumber As String)
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GRP_POLICYDATE_BY_FILENO"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_FILE_NO", fileNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            If (objOledr.Read()) Then
                Dim newStartDate As Date = Convert.ToDateTime(objOledr("TBIL_POL_PRM_FROM"))
                Dim newEndDate As Date = Convert.ToDateTime(objOledr("TBIL_POL_PRM_TO"))

                newStartDate = newStartDate.AddYears(1)
                newEndDate = newEndDate.AddYears(1)
                txtStartDate.Text = newStartDate.ToString("dd/MM/yyyy")
                txtEndDate.Text = newEndDate.ToString("dd/MM/yyyy")

            End If

            conn.Close()
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try
    End Sub

    Public Sub DoClaimRenewal(ByVal polyNumber As String, ByVal fileNumber As String, ByVal propNumber As String, ByVal startDate As Date, ByVal endDate As Date)
        ', ByVal batchNumber As String       
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GRP_CLAIMRENEWALBY_BATCH_NO"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_POLY_NO", polyNumber)
        cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_FILE_NO", fileNumber)
        cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_PROP_NO", propNumber)
        cmd.Parameters.AddWithValue("@TBIL_POL_PREM_INFO_STARTDATE", startDate)
        cmd.Parameters.AddWithValue("@TBIL_POL_PREM_INFO_ENDDATE", endDate)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()

            Dim dt As DataTable = New DataTable
            dt.Load(objOledr)
            Dim numRows As Integer = dt.Rows.Count
            lblResult.Text = "Result: " + numRows.ToString + " members listed."

            'If objOledr.HasRows Then
            GridView1.DataSource = dt
            GridView1.DataBind()


            'Else
            '    _rtnMessage = "Sorry. The system cannot find record with IDs: " + txtPolNum.Text
            'End If
            lblMsg.Text = "The policy " + polyNumber.ToString + " hes been renewed successfully!"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            conn.Close()
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try
    End Sub

    Protected Sub btnRenewClaim_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenewClaim.Click

        If txtPolNum.Text <> "" Then
            DoClaimRenewal(txtPolNum.Text, txtFileNum.Text, txtQuote_Num.Text, MOD_GEN.DoConvertToDbDateFormat(txtStartDate.Text), MOD_GEN.DoConvertToDbDateFormat(txtEndDate.Text))

        End If

    End Sub


    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging

        'Dim row As GridViewRow = GridView1.Rows(e.NewSelectedIndex)

        GridView1.PageIndex = e.NewPageIndex
        GETMEMBERSBY_BATCHNO_POLYNO(txtPolNum.Text, txtFileNum.Text, txtQuote_Num.Text)
        'Call Proc_DataBindGrid()
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

        Dim lblPrice1 As Label = GridView1.FooterRow.FindControl("lbltxtTotal")

        lblMsg.Text = "You selected " & Me.txtPolNum.Text & " / " & Me.txtRecNo.Text & "."


    End Sub


End Class
