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

Partial Class Policy_PRG_LI_GRP_POLY_MEMBERS_ADD
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

    Dim TotTransAmt As Decimal = 0
    Dim TransAmt As Decimal = 0
    Protected added_Prorata_Premium As Decimal
    Protected added_Prorata_Days As Integer
    Protected added_Used_Days As Integer
    Protected added_SA As Decimal
    Protected add_date_added As Date


    '*********************************************************
    'Variable declarations for the data from Excel worksheet
    'Copied here from former location _do_save function
    '*********************************************************
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
    Dim blnRet As Boolean = False
    Dim dteDOB As Date = Now
    Dim tenor As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.cmdFile_Upload.Attributes.Add("OnClientClick", "javascript:scrollMSG(" & ")")
        Me.cmdFile_Upload.Attributes.Add("OnClick", "javascript:return scrollMSG(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")")

        ''*********************************************************************************************
        '   JQuery library to enable scroller for ASP.NET GridView
        '<script language="javascript" type="text/javascript" src="../JQ/jquery-1.4.1.min.js"></script>
        '<script language="javascript" type="text/javascript" src="../JQ/ScrollableGridViewPlugin_ASP.NetAJAXmin.js"></script>

        '<script type="text/javascript">
        '    $(document).ready(function() {
        '        $('#<%=GridView1.ClientID %>').Scrollable({
        '            ScrollHeight: 300,
        '            IsInUpdatePanel: true
        '        })
        '    })
        '</script>
        ''*********************************************************************************************

        'dblFree_Cover_Limit = 7500000
        dblFree_Cover_Limit = 15000000

        ' If [cmdFile_upload] button is click, try to check if the file upload contains document
        '
        ' Me.cmdFile_Upload.Attributes.Add("onClick", "javascript:Func_File_Change()")

        'GF/2014/1201/G/G001/G/0000001
        'GF/2014/1201/G/G001/G/0000001

        strPATH = "c:\temp\"

        strTableName = "TBIL_POLICY_BENEFRY"
        strTableName = "TBIL_GRP_POLICY_MEMBERS"

        STRMENU_TITLE = "Proposal Screen"
        'STRMENU_TITLE = "Investment Plus Proposal"

        Try
            'strF_ID = CType(Request.QueryString("optfileid"), String)
            strF_ID = CType(Session("optfileid"), String)
        Catch ex As Exception
            strF_ID = ""
        End Try

        Try
            'strQ_ID = CType(Request.QueryString("optquotid"), String)
            strQ_ID = CType(Session("optquotid"), String)
        Catch ex As Exception
            strQ_ID = ""
        End Try

        Try
            'strP_ID = CType(Request.QueryString("optpolid"), String)
            strP_ID = CType(Session("optpolid"), String)
        Catch ex As Exception
            strP_ID = ""
        End Try


        If Not (Page.IsPostBack) Then
            HideRow1.Visible = False
            HideRow2.Visible = False
            HideRow3.Visible = False
            cboMedical_YN.SelectedValue = "N"
            Call Proc_DoNew()
            If DateTime.IsLeapYear(Year(DateTime.Now)) Then
                Me.txtRisk_Days.Text = "366"
            Else
                Me.txtRisk_Days.Text = "365"
            End If
            'Me.txtRisk_Days.Text = "365"
            Me.txtDOB_ANB.Text = "0"
            Me.txtData_Source_SW.Text = ""
            Me.txtData_Source_Name.Text = ""
            Me.txtFile_Upload.Text = ""
            Me.txtPrem_Period_Yr.Text = "1"
            Me.txtBatch_Num.Text = ""
            Me.txtXLS_Data_Start_No.Text = "1"
            Me.txtXLS_Data_End_No.Text = "1000"
            Me.txtPrem_Rate_TypeNum.Text = ""
            Me.txtPrem_Rate_Code.Text = ""
            Me.txtPrem_Rate.Text = "0"
            Me.txtPrem_Rate_Per.Text = "0"

            'Me.cmdSave_ASP.Enabled = True

            Me.lblMsg.Text = "Status:"
            Me.cmdPrev.Enabled = True
            Me.cmdNext.Enabled = False

            'Call gnProc_Populate_Box("IL_CODE_LIST", "015", Me.cboGender)
            Call gnPopulate_DropDownList("GL_MEMEBER_CATEGORY", Me.cboGender, "", "", "{select)", "*")

            If Trim(strF_ID) <> "" Then
                'Me.tr_file_upload.Visible = False
                Me.cmdFile_Upload.Enabled = False

                Me.txtFileNum.Text = RTrim(strF_ID)
                'Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_POLICY_BY_FILE_NO", RTrim(strF_ID), RTrim(""), RTrim(""))
                Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_GL_POLICY_BY_FILE_NO", RTrim(strF_ID), RTrim(""), RTrim(""))
                If oAL.Item(0) = "TRUE" Then
                    '    'Retrieve the record
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
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
                        txtGenStart_DateHidden.Text = Me.txtStart_Date.Text
                        txtPolStart_Date.Text = Me.txtStart_Date.Text
                    End If
                    If Trim(oAL.Item(21).ToString) <> "" Then
                        'GenEnd_Date = CDate(oAL.Item(21).ToString)
                        myarrData = Split(Trim(oAL.Item(21).ToString), "/")
                        GenEnd_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                        Me.txtEnd_Date.Text = Format(GenEnd_Date, "dd/MM/yyyy")
                        txtPolEnd_Date.Text = Me.txtEnd_Date.Text
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

                    If UCase(oAL.Item(18).ToString) = "A" Then
                        Me.cmdNew_ASP.Visible = False
                        Me.cmdSave_ASP.Visible = False
                        'Me.cmdDelete_ASP.Visible = False
                        Me.cmdDelItem_ASP.Visible = False
                        Me.cmdPrint_ASP.Visible = False
                    End If


                    Call Proc_Batch()
                    If RTrim(Me.txtBatch_Num.Text) <> "" Then
                        Call Proc_DataBind()
                    End If
                    Call Proc_LoadRate()
                Else
                    '    'Destroy i.e remove the array list object from memory
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    Me.lblMsg.Text = "Status: " & oAL.Item(1)
                End If
                oAL = Nothing
            End If

            Call gnProc_Populate_Box("GL_RATE_TYPE_CODE_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPrem_Rate_Code)
            Call Proc_DDL_Get(Me.cboPrem_Rate_Code, Me.txtPrem_Rate_Code.Text)

            If Trim(strF_ID) <> "" Then
                'Call Proc_OpenRecord(Me.txtNum.Text)
            End If
            'If Trim(strQ_ID) <> "" Then
            '    Me.txtQuote_Num.Text = RTrim(strQ_ID)
            'End If
            'If Trim(strP_ID) <> "" Then
            '    Me.txtPolNum.Text = RTrim(strP_ID)
            'End If

        End If


        If Me.txtAction.Text = "New" Then
            Call Proc_DoNew()
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Save" Then
            'Call Proc_DoSave()
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete" Then
            'Call DoDelete()
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Add_Item" Then
            Call Proc_DoAddItem()
            Me.txtAction.Text = ""
        End If


    End Sub

    Protected Sub cboBatch_Num_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBatch_Num.SelectedIndexChanged
        If cboBatch_Num.SelectedIndex <> 0 Then
            ' Me.txtBatch_Num.Text = cboBatch_Num.SelectedValue
            Call gnGET_SelectedItem(Me.cboBatch_Num, Me.txtBatch_Num, Me.txtBatch_Name, Me.lblMsg)
            If Trim(Me.txtBatch_Num.Text) <> "" Then
                Me.cmdNext.Enabled = True
            Else
            End If
            Call Proc_DataBind()
        End If
    End Sub

    Protected Sub cmdGetBatch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetBatch.Click
        If Trim(Me.txtBatch_Num.Text) = "" Then
            Call gnGET_SelectedItem(Me.cboBatch_Num, Me.txtBatch_Num, Me.txtBatch_Name, Me.lblMsg)
        End If
        If Trim(Me.txtBatch_Num.Text) <> "" Then
            Call Proc_DataBind()
        Else
            'Me.cmdNext.Enabled = False
        End If


    End Sub

    Protected Sub cmdFile_Upload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFile_Upload.Click

        Me.cmdFile_Upload.Enabled = False

        If Me.txtFileNum.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtQuote_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtBatch_Num.Text = "" Then
            Me.txtFile_Upload.Text = ""
            Me.cmdFile_Upload.Enabled = False
            Me.lblMsg.Text = "Missing " & Me.lblBatch_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Val(Trim(Me.txtRisk_Days.Text)) = 0 Then
            Me.lblMsg.Text = "Missing " & Me.lblRisk_Days.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'If Me.txtAdditionDate.Text = "" Then
        '    Me.lblMsg.Text = "Missing effective date"
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        'Dim str() As String
        'str = DoDate_Process(txtAdditionDate.Text, txtAdditionDate)
        'If (str(2) = Nothing) Then
        '    Dim errMsg = str(0).Insert(18, " effective date, ")
        '    lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
        '    lblMsg.Visible = True
        '    txtAdditionDate.Focus()
        '    Exit Sub
        'Else
        '    txtAdditionDate.Text = str(2).ToString()
        'End If


        Call gnGET_SelectedItem(Me.cboData_Source, Me.txtData_Source_SW, Me.txtData_Source_Name, Me.lblMsg)
        Select Case UCase(Trim(Me.txtData_Source_SW.Text))
            Case "M"
                Me.cmdFile_Upload.Enabled = False
            Case "U"

                Dim myfil As System.Web.HttpPostedFile = Me.My_File_Upload.PostedFile
                Me.txtFile_Upload.Text = Path.GetFileName(My_File_Upload.PostedFile.FileName)


                If Trim(Me.txtFile_Upload.Text) = "" Then
                    Me.lblMsg.Text = "Missing document or file name ..."
                    FirstMsg = "Javascript:alert('Missing document or file name. \nPlease select excel document with file extension .XLS or .XLSX')"
                    Me.txtFile_Upload.Text = ""
                    Exit Sub
                End If

                If Right(LCase(Trim(Me.txtFile_Upload.Text)), 3) = "xls" Or _
                   Right(LCase(Trim(Me.txtFile_Upload.Text)), 4) = "xlsx" Then
                Else
                    Me.txtFile_Upload.Text = ""
                    Me.lblMsg.Text = "Invalid document or file type. Expecting file of type .XLS or .XLSX ..."
                    FirstMsg = "Javascript:alert('Invalid document or file name. \nPlease select excel document with file extension .XLS or .XLSX')"
                    Exit Sub
                End If

                Try
                    strPATH = Server.MapPath("~/App_Data/Schedules/")

                    Dim strFilePath As String = ""
                    strFilePath = Server.MapPath("~/App_Data/Schedules/" & Me.txtFile_Upload.Text)
                    'post file to the server
                    My_File_Upload.PostedFile.SaveAs(strFilePath)

                Catch ex As Exception
                    Me.txtFile_Upload.Text = ""
                    Me.lblMsg.Text = "Error has occured. <br />Reason: " & ex.Message.ToString
                    FirstMsg = "Javascript:alert('" & "Unable to upload document or file to the server" & "')"
                    Exit Sub
                End Try

                Me.cmdFile_Upload.Enabled = False

                If Me.chkData_Source.Checked = True Then
                    Call Proc_DoSave_OLE()
                Else
                    Call Proc_DoSave_Upload()
                End If
                'Me.tr_file_upload.Visible = False

            Case Else
                Me.lblMsg.Text = "Missing or Invalid " & Me.lblData_Source.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub

        End Select

    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click

        If Me.txtBatch_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblBatch_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Call gnGET_SelectedItem(Me.cboData_Source, Me.txtData_Source_SW, Me.txtData_Source_Name, Me.lblMsg)
        Select Case UCase(Trim(Me.txtData_Source_SW.Text))
            Case "M"
                Call Proc_DoSave()
                'Me.tr_file_upload.Visible = False
            Case "U"
                If Val(Trim(Me.txtRecNo.Text)) = 0 Then
                    Call Proc_DoSave_Upload()
                Else
                    Call Proc_DoSave()
                End If
                'Me.tr_file_upload.Visible = False

            Case Else
                Me.lblMsg.Text = "Missing or Invalid " & Me.lblData_Source.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub

        End Select

        Me.txtAction.Text = ""
        'tr_file_upload.Visible = False

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
                txtGenStart_DateHidden.Text = Me.txtStart_Date.Text
                txtPolStart_Date.Text = Me.txtStart_Date.Text
            End If
            If Trim(oAL.Item(21).ToString) <> "" Then
                'GenEnd_Date = CDate(oAL.Item(21).ToString)
                myarrData = Split(Trim(oAL.Item(21).ToString), "/")
                GenEnd_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                Me.txtEnd_Date.Text = Format(GenEnd_Date, "dd/MM/yyyy")
                txtPolEnd_Date.Text = Me.txtEnd_Date.Text
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

            Call Proc_Batch()
            If RTrim(Me.txtBatch_Num.Text) <> "" Then
                Call Proc_DataBind()
            End If
            Call Proc_LoadRate()
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
                Me.lblMsg.Text = "Sorry. The batch number you entered already exist. \nPlease enter unique batch number..."
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

    Private Sub Proc_BindGrid(ByVal pvGridView As System.Web.UI.WebControls.GridView)

        Dim strConnString As String = CType(Session("connstr"), String)

        Dim mycon As New OleDbConnection(strConnString)

        strTable = strTableName

        strSQL = ""
        strSQL = strSQL & " SELECT TBIL_POL_MEMB_SNO AS T_SERIAL_NO, TBIL_POL_MEMB_STAFF_NO as T_PCN, TBIL_POL_MEMB_NAME AS T_MEMBER_NAME"
        strSQL = strSQL & " ,TBIL_POL_MEMB_BDATE AS T_DOB"
        strSQL = strSQL & " ,TBIL_POL_MEMB_AGE AS T_AGE, TBIL_POL_MEMB_CAT AS T_GENDER"
        strSQL = strSQL & " ,TBIL_POL_MEMB_DESIG AS T_DESIG, TBIL_POL_MEMB_FROM_DT AS T_START_DATE, TBIL_POL_MEMB_TO_DT AS T_END_DATE"
        strSQL = strSQL & " ,TBIL_POL_MEMB_TENOR AS T_TENOR, TBIL_POL_MEMB_SA_FACTOR AS T_FACTOR"
        strSQL = strSQL & " ,0 AS T_BASIC_SAL, 0 AS T_HOUSE_ALLOW, 0 AS T_TRANSPORT_ALLOW, 0 AS T_OTHER_ALLOW"
        strSQL = strSQL & " ,TBIL_POL_MEMB_TOT_EMOLUMENT AS T_TOTAL_EMOLUMENT, TBIL_POL_MEMB_TOT_SA AS T_SUM_ASSURED"

        strSQL = strSQL & " from " & strTable
        strSQL = strSQL & " where TBIL_POL_MEMB_FILE_NO = '" & RTrim(Me.txtFileNum.Text) & "'"
        strSQL = strSQL & " and TBIL_POL_MEMB_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
        'strSQL = strSQL & " and TBIL_POL_MEMB_POLY_NO = '" & RTrim(Me.txtPolNum.Text) & "'"
        strSQL = strSQL & " and TBIL_POL_MEMB_BATCH_NO = '" & RTrim(Me.txtBatch_Num.Text) & "'"
        'strSQL = strSQL & " and TBIL_POL_MEMB_STATUS in('Q')"
        strSQL = strSQL & " AND TBIL_POL_MEMB_FLAG NOT IN('D')" 'do not include deleted items

        strSQL = strSQL & " ORDER BY TBIL_POL_MEMB_FILE_NO, TBIL_POL_MEMB_BATCH_NO, TBIL_POL_MEMB_SNO"


        Dim mycmd As New OleDbCommand(strSQL)
        Dim myda As New OleDbDataAdapter()

        mycmd.Connection = mycon
        myda.SelectCommand = mycmd

        Dim mydt As New DataTable()
        myda.Fill(mydt)

        pvGridView.DataSource = mydt
        pvGridView.DataBind()

    End Sub

    'Private Sub BindGrid()
    '    Dim strConnString As String = CType(Session("connstr"), String)
    '    Using con As New SqlConnection(strConnString)
    '        imports cmd As New SqlCommand("SELECT * FROM Customers")
    '            imports sda As New SqlDataAdapter()
    '        cmd.Connection = con
    '        sda.SelectCommand = cmd
    '                imports dt As New DataTable()
    '        sda.Fill(dt)
    '        GridView1.DataSource = dt
    '        GridView1.DataBind()
    '                End imports
    '            End imports
    '        End imports
    '    End imports
    'End Sub

    Private Sub Proc_DataBind()
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


        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT *"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " WHERE TBIL_POL_MEMB_FILE_NO = '" & RTrim(strF_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_MEMB_PROP_NO = '" & RTrim(strQ_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_MEMB_BATCH_NO = '" & RTrim(Me.txtBatch_Num.Text) & "'"
        strSQL = strSQL & " AND TBIL_POL_MEMB_MDLE IN('G')"
        strSQL = strSQL & " AND TBIL_POL_MEMB_FLAG NOT IN('D')" 'do not include deleted items
        strSQL = strSQL & " ORDER BY TBIL_POL_MEMB_FILE_NO, TBIL_POL_MEMB_BATCH_NO, TBIL_POL_MEMB_SNO"


        Try

            'Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
            'objOLECmd.CommandType = CommandType.Text
            'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID
            'Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)
            'objDA.SelectCommand = objOLECmd

            Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)

            Dim objDS As DataSet = New DataSet()
            objDA.Fill(objDS, strTable)

            'Dim objDV As New DataView
            'objDV = objDS.Tables(strTable).DefaultView
            'objDV.Sort = "ACT_REC_NO"
            'Session("myobjDV") = objDV

            'With Me.DataGrid1
            '.DataSource = objDS
            '.DataBind()
            'End With

            With GridView1
                .DataSource = objDS
                .DataBind()
            End With

            'With Me.Repeater1
            '.DataSource = objDS
            '.DataBind()
            'End With

            'objDV.Dispose()
            'objDV = Nothing

            objDS.Dispose()
            objDA.Dispose()

            objDS = Nothing
            objDA = Nothing
            'objOLECmd.Dispose()
            'objOLECmd = Nothing


        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString

        End Try


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        objOLEConn = Nothing

        Me.cmdDelItem_ASP.Enabled = False


        Dim P As Integer = 0
        Dim C As Integer = 0

        C = 0
        For P = 0 To Me.GridView1.Rows.Count - 1
            C = C + 1
        Next
        Me.lblResult.Text = "Total Row: " & C.ToString

        If C >= 1 Then
            'Me.cmdDelete_ASP.Enabled = True
            Me.cmdDelItem_ASP.Enabled = True
            Me.cmdNext.Enabled = True
            ' Me.txtBatch_Num.Enabled = False
        Else
            Me.cmdNext.Enabled = False
            Me.txtBatch_Num.Enabled = True
        End If

        'C = C + 1
        'Me.txtBenef_SN.Text = C.ToString

    End Sub

    Private Sub Proc_DDL_Get(ByVal pvDDL As DropDownList, ByVal pvRef_Value As String)
        On Error Resume Next
        pvDDL.SelectedIndex = pvDDL.Items.IndexOf(pvDDL.Items.FindByValue(CType(RTrim(pvRef_Value), String)))

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

    Protected Sub DoProc_Data_Source_Change()
        Call gnGET_SelectedItem(Me.cboData_Source, Me.txtData_Source_SW, Me.txtData_Source_Name, Me.lblMsg)
        Select Case UCase(Trim(Me.txtData_Source_SW.Text))
            Case "M"
                'tr_file_upload.Visible = False
                Me.cmdFile_Upload.Enabled = False
                Me.cmdSave_ASP.Enabled = True
                ShowControls()
            Case "U"
                'tr_file_upload.Visible = True
                Me.cmdSave_ASP.Enabled = False
                HideControls()
            Case Else
                'tr_file_upload.Visible = False
                Me.cmdFile_Upload.Enabled = False
                Me.cmdSave_ASP.Enabled = False
                HideControls()
        End Select

        'Response.Write("<br />Code: " & UCase(Trim(Me.txtData_Source_SW.Text)))
        'tr_file_upload.Visible = True

    End Sub

    Protected Sub DoProc_Premium_Code_Change()
        'Added by Azeez bcos  "txtPrem_Rate_Code.Text" is sending a empty value
        txtPrem_Rate_Code.Text = Me.cboPrem_Rate_Code.SelectedValue
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
            If txtTotal_Emolument.Text = "" Then
                lblMsg.Text = "Total Emolument must no be empty"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            Me.txtPrem_Rate.Text = myRetValue.ToString

            dblPrem_Amt = 0
            dblPrem_Amt_ProRata = 0
            dblTotal_SA = 0

            dblTotal_Salary = CDbl(Trim(Me.txtTotal_Emolument.Text))
            dblTotal_Salary = CDbl(Trim(Me.txtTotal_Emolument.Text))

            dblTotal_SA = dblTotal_Salary
            If Val(Me.txtPrem_SA_Factor.Text) <> 0 Then
                dblTotal_SA = dblTotal_Salary * Val(Trim(Me.txtPrem_SA_Factor.Text))
            End If

            Me.txtSum_Assured.Text = dblTotal_SA.ToString


            dblPrem_Rate = CDbl(Trim(Me.txtPrem_Rate.Text))
            dblPrem_Rate_Per = CDbl(Trim(Me.txtPrem_Rate_Per.Text))
            If dblTotal_SA <> 0 And dblPrem_Rate <> 0 And dblPrem_Rate_Per <> 0 Then
                dblPrem_Amt = dblTotal_SA * dblPrem_Rate / dblPrem_Rate_Per
                dblPrem_Amt_ProRata = dblPrem_Amt
                txtPrem_Amt.Text = dblPrem_Amt
            End If
        End If
    End Sub

    Private Sub Proc_DoDelete()

        If Trim(Me.txtFileNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtQuote_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Exit Sub
        End Try


        strTable = strTableName

        strREC_ID = Trim(Me.txtFileNum.Text)

        'Delete record
        'Me.textMessage.Text = "Deleting record... "
        ' Delete permanently removes the poly record from the DB

        strSQL = ""
        strSQL = "DELETE FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_MEMB_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_MEMB_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
        strSQL = strSQL & " AND TBIL_POL_MEMB_REC_ID = " & Val(RTrim(Me.txtRecNo.Text)) & ""

        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try
            objOLECmd2.Connection = objOLEConn
            objOLECmd2.CommandType = CommandType.Text
            objOLECmd2.CommandText = strSQL
            intC = objOLECmd2.ExecuteNonQuery()

            If intC >= 1 Then
                Call Proc_DoNew()
                Me.lblMsg.Text = "Record deleted successfully."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Else
                Me.lblMsg.Text = "Sorry!. Record not deleted..."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

        End Try


        objOLECmd2.Dispose()
        objOLECmd2 = Nothing


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        'Me.txtNum.Enabled = True
        'Me.txtNum.Focus()

    End Sub


    Protected Sub Proc_DoAddItem()
        blnRet = True

        Dim P As Integer = 0, C As Integer
        Dim myKey As String = "", myKeyX As String = ""
        Dim add_start_date As Date
        'Validate all fields before parsing the grid data

        If Trim(Me.txtFileNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            blnRet = False

            Exit Sub
        End If

        If Trim(Me.txtQuote_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            blnRet = False

            Exit Sub
        End If

        If Trim(Me.txtRecNo.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblRecNo.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            blnRet = False

            Exit Sub
        End If



        '****************************************
        'Validate Effective Date of Member Addtion
        '****************************************

        'Azeez Comments Start here
        'If RTrim(Me.txtAdditionDate.Text) = "" Then
        '    Me.lblMsg.Text = "Missing Effective Date of Addition ... "
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    blnRet = False
        '    Exit Sub
        'End If

        'If RTrim(Me.txtAdditionDate.Text) = "" Or Len(Trim(Me.txtAdditionDate.Text)) <> 10 Then
        '    Me.lblMsg.Text = "Missing or Invalid Addition date "
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    blnRet = False
        '    Exit Sub
        'End If

        ''Validate date
        'myarrData = Split(Me.txtAdditionDate.Text, "/")
        'If myarrData.Count <> 3 Then
        '    Me.lblMsg.Text = "Missing or Invalid Added Date. Expecting full date in ddmmyyyy format ..."
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    blnRet = False
        '    Exit Sub
        'End If

        'strMyDay = myarrData(0)
        'strMyMth = myarrData(1)
        'strMyYear = myarrData(2)

        'strMyDay = CType(Format(Val(strMyDay), "00"), String)
        'strMyMth = CType(Format(Val(strMyMth), "00"), String)
        'strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        'strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        'blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        'If blnStatusX = False Then
        '    Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    blnRet = False
        '    Exit Sub
        'End If
        'Me.txtAdditionDate.Text = RTrim(strMyDte)
        'mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        ''add_date_added = General_Date_Validation(txtAdditionDate.Text, "Added")
        'add_date_added = mydteX ' Format(CDate(add_date_added), "MM/dd/yyyy")
        'Azeez Comments ends here

        'myarrData = Split(Me.txtStart_Date.Text, "/")

        'strMyDay = myarrData(0)
        'strMyMth = myarrData(1)
        'strMyYear = myarrData(2)

        'strMyDay = CType(Format(Val(strMyDay), "00"), String)
        'strMyMth = CType(Format(Val(strMyMth), "00"), String)
        'strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        'mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        'mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        'add_start_date = Format(mydte, "MM/dd/yyyy")

        add_start_date = General_Date_Validation(txtStart_Date.Text, "Start")
        add_start_date = Format(CDate(add_start_date), "MM/dd/yyyy")

        Dim policy_start_date As Date
        policy_start_date = CDate(DoConvertToDbDateFormat(txtPolStart_Date.Text))
        '****************************************
        'Validate End
        '****************************************

        Dim Prem_added As Decimal = 0
        added_SA = 0
        For P = 0 To Me.GridView1.Rows.Count - 1
            If CType(Me.GridView1.Rows(P).FindControl("chkSel"), CheckBox).Checked Then
                ' Get the currently selected row imports the SelectedRow property.
                Dim row As GridViewRow = GridView1.Rows(P)
                myKeyX = myKeyX & row.Cells(2).Text
                myKeyX = myKeyX & " / "
                myKey = Me.GridView1.Rows(P).Cells(2).Text

                'Delete selected/checked item(s)
                If Trim(myKey) <> "" Then
                    Me.txtRecNo.Text = myKey
                    Prem_added = Prem_added + Convert.ToDecimal(Me.GridView1.Rows(P).Cells(8).Text)
                    added_SA = added_SA + Convert.ToDecimal(Me.GridView1.Rows(P).Cells(4).Text)
                    'Dim add_date = Me.GridView1.Rows(P).Cells(9).Text
                    add_date_added = CDate(DoConvertToDbDateFormat(Me.GridView1.Rows(P).Cells(10).Text))
                    C = C + 1

                    'added_Used_Days = DateDiff(DateInterval.Day, CDate(add_date_added), CDate(add_start_date))
                    added_Used_Days = DateDiff(DateInterval.Day, add_date_added, policy_start_date)
                    added_Prorata_Days = Convert.ToInt16(txtRisk_Days.Text) - Math.Abs(added_Used_Days)
                    added_Prorata_Premium = Prem_added * (added_Prorata_Days / Convert.ToInt16(txtRisk_Days.Text))
                    Call Proc_DoAdd_Record()
                End If

            End If

        Next

        If C >= 1 Then
            'Me.cmdDelItem_ASP.Enabled = False
            'Me.cmdDelItem.Enabled = False

            Call Proc_DataBind()

            Call Proc_DoNew()

            Me.lblMsg.Text = "Record Added successfully." & " No of item(s) Added: " & CStr(C)
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.textMessage.Text = ""

            Me.lblMsg.Text = "Added Item(s): " & myKeyX

        Else
            Me.lblMsg.Text = "Record not Added ..."

        End If

        'Calculate the Prorated Premium as at the point of deletion of member(s)
        'added_Used_Days = DateDiff(DateInterval.Day, CDate(add_date_added), CDate(add_start_date))
        'added_Prorata_Days = Convert.ToInt16(txtRisk_Days.Text) - Math.Abs(added_Used_Days)
        'added_Prorata_Premium = Prem_added * (added_Prorata_Days / Convert.ToInt16(txtRisk_Days.Text))

    End Sub

    Protected Sub Proc_DoAdd_Record()


        Dim intC As Long = 0

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Exit Sub
        End Try


        strREC_ID = Trim(Me.txtFileNum.Text)
        strTable = strTableName

        strSQL = ""
        'Additional member record i.e. move 'Z' to the reocrd flag
        '==============================================
        strSQL = ""
        strSQL = "Update " & strTable
        strSQL = strSQL & " SET TBIL_POL_MEMB_FLAG = 'Z'"
        strSQL = strSQL & ",TBIL_POL_MEMB_KEYDTE = '" & add_date_added
        strSQL = strSQL & "' WHERE TBIL_POL_MEMB_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_MEMB_PROP_NO = '" & RTrim(txtQuote_Num.Text) & "'"
        strSQL = strSQL & " AND TBIL_POL_MEMB_REC_ID = " & Val(RTrim(Me.txtRecNo.Text)) & ""

        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try
            With objOLECmd2
                .Connection = objOLEConn
                .CommandType = CommandType.Text
                .CommandText = strSQL
            End With
            intC = objOLECmd2.ExecuteNonQuery()

            If intC >= 1 Then
                'Call Proc_DoNew()
                'Me.lblMsg.Text = "Record deleted successfully."
                'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Else
                'Me.lblMsg.Text = "Sorry!. Record not deleted..."
                'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        End Try

        objOLECmd2.Dispose()
        objOLECmd2 = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

    End Sub




    Private Sub Proc_DoNew()

        'Call Proc_DDL_Get(Me.cboransList, RTrim("*"))

        'Scan through textboxes on page or form
        'Try
        'Catch ex As Exception

        'End Try


        Dim ctrl As Control
        For Each ctrl In Page.Controls
            If TypeOf ctrl Is HtmlForm Then
                Dim subctrl As Control
                For Each subctrl In ctrl.Controls
                    If TypeOf subctrl Is System.Web.UI.WebControls.TextBox Then
                        If subctrl.ID = "txtFileNum" Or _
                           subctrl.ID = "txtQuote_Num" Or _
                           subctrl.ID = "txtPolNum" Or _
                           subctrl.ID = "txtProductClass" Or _
                           subctrl.ID = "txtProduct_Num" Or _
                           subctrl.ID = "cboBenef_Cover_ID" Or _
                           subctrl.ID = "txtData_Source_SW" Or _
                           subctrl.ID = "txtData_Source_Name" Or _
                           subctrl.ID = "txtFile_Upload" Or _
                           subctrl.ID = "txtPrem_Period_Yr" Or _
                           subctrl.ID = "txtBatch_Num" Or _
                           subctrl.ID = "txtXLS_Data_Start_No" Or _
                           subctrl.ID = "txtXLS_Data_End_No" Or _
                           subctrl.ID = "txtPrem_Rate_TypeNum" Or _
                           subctrl.ID = "txtPrem_Rate" Or _
                           subctrl.ID = "txtPrem_Rate_Per" Or _
                           subctrl.ID = "txtPrem_Rate_Code" Or _
                           subctrl.ID = "txtPrem_SA_Factor" Or _
                           subctrl.ID = "txtRisk_Days" Or _
                           subctrl.ID = "txtStart_Date" Or _
                           subctrl.ID = "txtEnd_Date" Or _
                           subctrl.ID = "txtPolStart_Date" Or _
                           subctrl.ID = "txtPolEnd_Date" Or _
                           subctrl.ID = "txtGenStart_DateHidden" Or _
                           subctrl.ID = "xyz_123" Then
                            'Control(ID) : txtAction
                            'Control(ID) : txtFileNum
                            'Control(ID) : txtPolNum
                            'Control(ID) : txtQuote_Num
                            'Control(ID) : txtRecNo
                            'Control(ID) : txtBenef_SN
                            'Control(ID) : txtBenef_Type
                            'Control(ID) : txtBenef_TypeName
                            'Control(ID) : txtBenef_Category
                            'Control(ID) : txtBenef_CategoryName
                            'Control(ID) : txtBenef_Name
                            'Control(ID) : txtBenef_Percentage
                            'Control(ID) : txtBenef_DOB
                            'Control(ID) : txtBenef_Age
                            'Control(ID) : txtBenef_Relationship
                            'Control(ID) : txtBenef_RelationshipName
                            'Control(ID) : txtBenef_Address
                            'Control(ID) : txtBenef_GuardianName
                        Else
                            'Response.Write("<br> Control ID: " & subctrl.ID)
                            CType(subctrl, TextBox).Text = ""
                        End If
                    End If
                    If TypeOf subctrl Is System.Web.UI.WebControls.DropDownList Then
                        If subctrl.ID = "cboData_Source" Or _
                           subctrl.ID = "cboBatch_Num" Or _
                           subctrl.ID = "cboPrem_Rate_Code" Or _
                           subctrl.ID = "cboMedical_YN" Or _
                           subctrl.ID = "xyz_123" Then
                        Else
                            CType(subctrl, DropDownList).SelectedIndex = -1
                        End If
                    End If
                Next
            End If
        Next

        Me.txtFileNum.Enabled = False
        Me.txtQuote_Num.Enabled = False
        Me.txtPolNum.Enabled = True

        Me.cmdGetPol.Enabled = True

        'Me.txtFileNum.Text = ""
        'Me.txtQuote_Num.Text = ""
        'Me.txtPolNum.Text = ""

        Me.lblPrem_SA_Factor.Enabled = True
        'Me.txtPrem_SA_Factor.Text = ""
        Me.txtPrem_SA_Factor.Enabled = True

        Me.txtBatch_Num.Enabled = True
        Me.txtBatch_Num.Text = ""
        Me.cmdGetBatch.Enabled = True

        Me.txtRecNo.Text = "0"

        'Me.txtStart_Date.Text = Format(GenStart_Date, "dd/MM/yyyy")
        'Me.txtEnd_Date.Text = Format(GenEnd_Date, "dd/MM/yyyy")

        Me.cmdPrev.Enabled = False
        Me.cmdSave_ASP.Enabled = True
        'Me.cmdDelItem_ASP.Enabled = False
        Me.cmdNext.Enabled = False

    End Sub

    Private Sub Proc_DoSave()

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

        Dim dteDOB As Date = Now

        Dim lngDOB_ANB As Integer = 0

        Dim Dte_Current As Date = Now
        Dim Dte_DOB As Date = Now

        If Me.txtFileNum.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtQuote_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'If Me.txtPolNum.Text = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblPolNum.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If


        If Val(Trim(Me.txtPrem_SA_Factor.Text)) = 0 Then
            Me.lblMsg.Text = "Missing " & Me.lblPrem_SA_Factor.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtBatch_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblBatch_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Call MOD_GEN.gnGET_SelectedItem(Me.cboData_Source, Me.txtData_Source_SW, Me.txtData_Source_Name, Me.lblMsg)
        If Trim(Me.txtData_Source_SW.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblData_Source.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Call MOD_GEN.gnGET_SelectedItem(Me.cboGender, Me.txtGender, Me.txtGenderName, Me.lblMsg)
        If Trim(Me.txtGender.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblGender.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtMember_Name.Text) = "" Or Trim(Me.txtMember_Name.Text) = "." Or Trim(Me.txtMember_Name.Text) = "*" Then
            Me.lblMsg.Text = "Missing or invalid " & Me.lblMember_Name.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtDesignation_Name.Text) = "" Or Trim(Me.txtDesignation_Name.Text) = "." Or Trim(Me.txtDesignation_Name.Text) = "*" Then
            Me.lblMsg.Text = "Missing or invalid " & Me.lblDesignation_Name.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
        End If


        Me.txtMember_DOB.Text = Trim(Me.txtMember_DOB.Text)
        If RTrim(Me.txtMember_DOB.Text) = "" And Val(Me.txtDOB_ANB.Text) = 0 Then
            Me.lblMsg.Text = "Missing Date of Birth or Age ... "
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If RTrim(Me.txtMember_DOB.Text) = "" And Val(Me.txtDOB_ANB.Text) < 0 Then
            Me.lblMsg.Text = "Missing or Invalid Date of Birth or Age ... "
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dte_Current = Now
        'If Val(Me.txtDOB_ANB.Text) <> 0 Then
        '    dteDOB = DateAdd(DateInterval.Year, Val(Me.txtDOB_ANB.Text) * -1, Dte_Current)
        '    Me.txtMember_DOB.Text = Format(dteDOB, "dd/MM/yyyy")
        'End If
        If RTrim(Me.txtMember_DOB.Text) = "" Then
            Me.lblMsg.Text = "Missing Date of Birth or Age ... "
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'GoTo Proc_Skip_ANB
            Exit Sub
        End If

        If RTrim(Me.txtMember_DOB.Text) = "" Or Len(Trim(Me.txtMember_DOB.Text)) <> 10 Then
            Me.lblMsg.Text = "Missing or Invalid date - " & Me.lblMember_DOB.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtMember_DOB.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblMember_DOB.Text & ". Expecting full date in ddmmyyyy format ..."
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
            Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        Me.txtMember_DOB.Text = RTrim(strMyDte)
        'mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteDOB = Format(mydte, "MM/dd/yyyy")

        Dte_DOB = dteDOB

        Dte_Current = Now
        lngDOB_ANB = Val(DateDiff("yyyy", Dte_Current, Dte_DOB))
        If lngDOB_ANB < 0 Then
            lngDOB_ANB = lngDOB_ANB * -1
        End If

        If Dte_Current.Month >= Dte_DOB.Month Then
            lngDOB_ANB = lngDOB_ANB
        End If
        Me.txtDOB_ANB.Text = Trim(Str(lngDOB_ANB))

Proc_Skip_ANB:


        'Validate date
        myarrData = Split(Me.txtStart_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblStart_Date.Text & ". Expecting full date in ddmmyyyy format ..."
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
        Me.txtStart_Date.Text = Trim(strMyDte)

        If RTrim(Me.txtStart_Date.Text) = "" Or Len(Trim(Me.txtStart_Date.Text)) <> 10 Then
            Me.lblMsg.Text = "Missing or Invalid date - " & Me.lblStart_Date.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtStart_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblStart_Date.Text & ". Expecting full date in ddmmyyyy format ..."
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
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        Me.txtStart_Date.Text = RTrim(strMyDte)
        'mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteStart = Format(mydte, "MM/dd/yyyy")



        'Validate date
        myarrData = Split(Me.txtEnd_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblEnd_Date.Text & ". Expecting full date in ddmmyyyy format ..."
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
        Me.txtEnd_Date.Text = Trim(strMyDte)

        If RTrim(Me.txtEnd_Date.Text) = "" Or Len(Trim(Me.txtEnd_Date.Text)) <> 10 Then
            Me.lblMsg.Text = "Missing or Invalid date - " & Me.lblStart_Date.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtEnd_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblEnd_Date.Text & ". Expecting full date in ddmmyyyy format ..."
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
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        Me.txtEnd_Date.Text = RTrim(strMyDte)
        'mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteEnd = Format(mydte, "MM/dd/yyyy")

        If dteStart > dteEnd Then
            Me.lblMsg.Text = "Error!. Start Date greater than End Date... "
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Test if member start date is within policy period
        If dteStart < CDate(DoConvertToDbDateFormat(txtPolStart_Date.Text)) Or _
                               dteStart > CDate(DoConvertToDbDateFormat(txtPolEnd_Date.Text)) Then
            lblMsg.Text = "Member start date is not within policy period"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtStart_Date.Focus()
            Exit Sub
        End If

        'Test if member end date is the same with policy end date
        If dteEnd <> CDate(DoConvertToDbDateFormat(txtPolEnd_Date.Text)) Then
            lblMsg.Text = "Member end date should be the same with policy end date"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtEnd_Date.Focus()
            Exit Sub
        End If

        'If txtAdditionDate.Text = "" Then
        '    Me.lblMsg.Text = "Missing or invalid effective date... "
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        'add_date_added = General_Date_Validation(txtAdditionDate.Text, "Effective")
        'add_date_added = Format(CDate(add_date_added), "MM/dd/yyyy")


        Me.txtPrem_Period_Yr.Text = Trim(Me.txtPrem_Period_Yr.Text)
        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Period_Yr)
        If Val(Me.txtPrem_Period_Yr.Text) <= 0 Then
            Me.lblMsg.Text = "Missing or invalid Tenor... "
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


        Me.txtTotal_Emolument.Text = Trim(Me.txtTotal_Emolument.Text)
        Call MOD_GEN.gnInitialize_Numeric(Me.txtTotal_Emolument)
        If Val(Me.txtTotal_Emolument.Text) <= 0 Then
            Me.lblMsg.Text = "Missing or invalid " & Me.lblTotal_Emolument.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


        Call MOD_GEN.gnGET_SelectedItem(Me.cboMedical_YN, Me.txtMedical_YN, Me.txtMedical_YN_Name, Me.lblMsg)
        If Trim(Me.txtMedical_YN.Text) = "" Or Trim(Me.txtMedical_YN.Text) = "." Or Trim(Me.txtMedical_YN.Text) = "*" Then
            Me.lblMsg.Text = "Missing or invalid " & Me.lblMedical_YN.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Select Case UCase(Trim(Me.txtPrem_Rate_TypeNum.Text))
            Case "F"    ' fixed rate
            Case "N"    ' no premium
                Me.txtPrem_Rate.Text = "0"
            Case "T"    ' premium is from table
                Call DoProc_Premium_Code_Change()
        End Select

        Me.txtPrem_Rate.Text = Trim(Me.txtPrem_Rate.Text)
        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Rate)

        Me.txtPrem_Rate_Per.Text = Trim(Me.txtPrem_Rate_Per.Text)
        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Rate_Per)

        Select Case UCase(Trim(Me.txtPrem_Rate_TypeNum.Text))
            Case "F", "T"    ' fixed rate and table rate
                If Val(Me.txtPrem_Rate.Text) <= 0 Then
                    Me.lblMsg.Text = "Missing or invalid premium rate... "
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Exit Sub
                End If

                If Val(Me.txtPrem_Rate_Per.Text) <= 0 Then
                    Me.lblMsg.Text = "Missing or invalid premium rate per. e.g 1000 or 100... "
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Exit Sub
                End If

        End Select

        dblPrem_Amt = 0
        dblPrem_Amt_ProRata = 0
        dblTotal_SA = 0

        dblTotal_Salary = CDbl(Trim(Me.txtTotal_Emolument.Text))
        dblTotal_Salary = CDbl(Trim(Me.txtTotal_Emolument.Text))

        dblTotal_SA = dblTotal_Salary
        If Val(Me.txtPrem_SA_Factor.Text) <> 0 Then
            dblTotal_SA = dblTotal_Salary * Val(Trim(Me.txtPrem_SA_Factor.Text))
        End If

        Me.txtSum_Assured.Text = dblTotal_SA.ToString


        dblPrem_Rate = CDbl(Trim(Me.txtPrem_Rate.Text))
        dblPrem_Rate_Per = CDbl(Trim(Me.txtPrem_Rate_Per.Text))
        If dblTotal_SA <> 0 And dblPrem_Rate <> 0 And dblPrem_Rate_Per <> 0 Then
            dblPrem_Amt = dblTotal_SA * dblPrem_Rate / dblPrem_Rate_Per
            dblPrem_Amt_ProRata = dblPrem_Amt
        End If


        'intRisk_Days = DateDiff(DateInterval.Day, GenStart_Date, GenEnd_Date)
        'intDays_Diff = DateDiff(DateInterval.Day, MemJoin_Date, GenEnd_Date)

        intRisk_Days = Val(DateDiff(DateInterval.Day, GenStart_Date, GenEnd_Date)) + 0
        intRisk_Days = Val(Me.txtRisk_Days.Text)
        tenor = CInt(txtRisk_Days.Text) 'Azeez: Tenor should be equals to risk days at inception of policy


        'intDays_Diff = Val(DateDiff(DateInterval.Day, MemJoin_Date, GenEnd_Date)) + 0
        'intDays_Diff = Val(DateDiff(DateInterval.Day, my_Dte_Start, my_Dte_End))
        intDays_Diff = Val(DateDiff(DateInterval.Day, dteStart, dteEnd))

        'Added by Azeez
        'Initially both MemJoin_Date and GenStart_Date looses their value 
        'Start date equals join date for a particular member
        MemJoin_Date = dteStart
        GenStart_Date = Convert.ToDateTime(DoConvertToDbDateFormat(txtGenStart_DateHidden.Text))

        If MemJoin_Date > GenStart_Date And dblPrem_Amt <> 0 And intDays_Diff <> 0 Then
            dblPrem_Amt_ProRata = Format((dblPrem_Amt / intRisk_Days) * intDays_Diff, "#########0.00")
            tenor = intDays_Diff 'Azeez: Tenor
        End If

        If dblTotal_SA >= dblFree_Cover_Limit Then
            If Trim(Me.txtMedical_YN.Text) = "" Then
                Me.txtMedical_YN.Text = "Y"
            End If
        End If

        'If Trim(txtBenef_Cover_ID.Text) = "" Then
        '    Me.txtBenef_Cover_ID.Text = MOD_GEN.gnGet_Serial_No(RTrim("GET_SN_IL"), RTrim("FUN_COVER_SN"), Trim(Me.txtFileNum.Text), Trim(Me.txtQuote_Num.Text))
        'End If

        If Trim(txtMember_SN.Text) = "" Then
            'Me.txtBenef_SN.Text = "0"
        End If

        If Trim(txtMember_SN.Text) = "" Then
            Me.txtMember_SN.Text = MOD_GEN.gnGet_Serial_No(RTrim("GET_SN_GL"), RTrim("GL_MEMBER_SN"), Trim(Me.txtFileNum.Text), Trim(Me.txtQuote_Num.Text))
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


        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_MEMB_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
        'strSQL = strSQL & " AND TBIL_POL_MEMB_PROP_NO = '" & RTrim(txtQuote_Num.Text) & "'"
        'If Val(LTrim(RTrim(Me.txtRecNo.Text))) <> 0 Then
        strSQL = strSQL & " AND TBIL_POL_MEMB_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
        'End If


        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        'or
        'objDA.SelectCommand = New System.Data.OleDb.OleDbCommand(strSQL, objOleConn)

        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        'Dim m_rwContact As System.Data.DataRow
        Dim intC As Integer = 0


        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '   Creating a new record

                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()

                drNewRow("TBIL_POL_MEMB_FILE_UPLOAD_SW") = RTrim(Me.txtData_Source_SW.Text)
                drNewRow("TBIL_POL_MEMB_FILE_UPLOAD_NAME") = RTrim(Me.txtFile_Upload.Text)
                drNewRow("TBIL_POL_MEMB_MDLE") = RTrim("G")

                drNewRow("TBIL_POL_MEMB_STATUS") = "Q"

                drNewRow("TBIL_POL_MEMB_FILE_NO") = RTrim(Me.txtFileNum.Text)
                drNewRow("TBIL_POL_MEMB_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                drNewRow("TBIL_POL_MEMB_POLY_NO") = RTrim(Me.txtPolNum.Text)

                'drNewRow("TBIL_POL_MEMB_COVER_ID") = Val(Me.txtBenef_Cover_ID.Text)

                drNewRow("TBIL_POL_MEMB_BATCH_NO") = RTrim(Me.txtBatch_Num.Text)

                drNewRow("TBIL_POL_MEMB_SNO") = Val(Me.txtMember_SN.Text)
                drNewRow("TBIL_POL_MEMB_CAT") = RTrim(Me.txtGender.Text)

                If Trim(Me.txtMember_DOB.Text) <> "" Then
                    drNewRow("TBIL_POL_MEMB_BDATE") = dteDOB
                End If
                drNewRow("TBIL_POL_MEMB_AGE") = Val(Me.txtDOB_ANB.Text)

                If Trim(Me.txtStart_Date.Text) <> "" Then
                    drNewRow("TBIL_POL_MEMB_FROM_DT") = dteStart
                End If
                If Trim(Me.txtEnd_Date.Text) <> "" Then
                    drNewRow("TBIL_POL_MEMB_TO_DT") = dteEnd
                End If
                'drNewRow("TBIL_POL_MEMB_EFF_DT") = add_date_added

                ' drNewRow("TBIL_POL_MEMB_TENOR") = Val(Me.txtPrem_Period_Yr.Text)
                drNewRow("TBIL_POL_MEMB_TENOR") = tenor
                drNewRow("TBIL_POL_MEMB_DESIG") = Left(RTrim(Me.txtDesignation_Name.Text), 40)
                drNewRow("TBIL_POL_MEMB_NAME") = Left(RTrim(Me.txtMember_Name.Text), 98)

                drNewRow("TBIL_POL_MEMB_SA_FACTOR") = Val(Trim(Me.txtPrem_SA_Factor.Text))
                drNewRow("TBIL_POL_MEMB_TOT_EMOLUMENT") = CDbl(Trim(Me.txtTotal_Emolument.Text))
                drNewRow("TBIL_POL_MEMB_TOT_SA") = CDbl(Trim(Me.txtSum_Assured.Text))
                drNewRow("TBIL_POL_MEMB_MEDICAL_YN") = RTrim(Me.txtMedical_YN.Text)

                drNewRow("TBIL_POL_MEMB_RATE_CODE") = Me.txtPrem_Rate_Code.Text
                drNewRow("TBIL_POL_MEMB_RATE") = RTrim(Trim(Me.txtPrem_Rate.Text))
                drNewRow("TBIL_POL_MEMB_RATE_PER") = Val(Trim(Me.txtPrem_Rate_Per.Text))

                drNewRow("TBIL_POL_MEMB_PREM") = CDbl(dblPrem_Amt)
                drNewRow("TBIL_POL_MEMB_PRO_RATE_PREM") = CDbl(dblPrem_Amt_ProRata)
                drNewRow("TBIL_POL_MEMB_LOAD") = CDbl(dblLoad_Amt)

                drNewRow("TBIL_POL_MEMB_FLAG") = "A"
                drNewRow("TBIL_POL_MEMB_OPERID") = CType(myUserIDX, String)
                'drNewRow("TBIL_POL_MEMB_KEYDTE") = add_date_added
                drNewRow("TBIL_POL_MEMB_KEYDTE") = Now
                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                drNewRow = Nothing

                Me.lblMsg.Text = "New Record Saved to Database Successfully."

            Else
                '   Update existing record

                'm_rwContact = m_dtContacts.Rows(0)
                'm_rwContact("ContactName") = "Bob Brown"
                'm_rwContact.AcceptChanges()
                'm_dtContacts.AcceptChanges()
                'Dim intC As Integer = m_daDataAdapter.Update(m_dtContacts)


                With obj_DT

                    .Rows(0)("TBIL_POL_MEMB_FILE_UPLOAD_SW") = RTrim(Me.txtData_Source_SW.Text)
                    .Rows(0)("TBIL_POL_MEMB_FILE_UPLOAD_NAME") = RTrim(Me.txtFile_Upload.Text)
                    .Rows(0)("TBIL_POL_MEMB_MDLE") = RTrim("G")

                    .Rows(0)("TBIL_POL_MEMB_FILE_NO") = RTrim(Me.txtFileNum.Text)
                    .Rows(0)("TBIL_POL_MEMB_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                    .Rows(0)("TBIL_POL_MEMB_POLY_NO") = RTrim(Me.txtPolNum.Text)

                    '.Rows(0)("TBIL_POL_MEMB_COVER_ID") = Val(Me.txtBenef_Cover_ID.Text)

                    .Rows(0)("TBIL_POL_MEMB_BATCH_NO") = RTrim(Me.txtBatch_Num.Text)

                    .Rows(0)("TBIL_POL_MEMB_SNO") = Val(Me.txtMember_SN.Text)
                    .Rows(0)("TBIL_POL_MEMB_CAT") = RTrim(Me.txtGender.Text)

                    If Trim(Me.txtMember_DOB.Text) <> "" Then
                        .Rows(0)("TBIL_POL_MEMB_BDATE") = dteDOB
                    End If
                    .Rows(0)("TBIL_POL_MEMB_AGE") = Val(Me.txtDOB_ANB.Text)

                    If Trim(Me.txtStart_Date.Text) <> "" Then
                        .Rows(0)("TBIL_POL_MEMB_FROM_DT") = dteStart
                    End If
                    If Trim(Me.txtEnd_Date.Text) <> "" Then
                        .Rows(0)("TBIL_POL_MEMB_TO_DT") = dteEnd
                    End If
                    '.Rows(0)("TBIL_POL_MEMB_EFF_DT") = add_date_added
                    '.Rows(0)("TBIL_POL_MEMB_TENOR") = Val(Me.txtPrem_Period_Yr.Text)
                    .Rows(0)("TBIL_POL_MEMB_TENOR") = tenor
                    .Rows(0)("TBIL_POL_MEMB_DESIG") = Left(RTrim(Me.txtDesignation_Name.Text), 40)
                    .Rows(0)("TBIL_POL_MEMB_NAME") = Left(RTrim(Me.txtMember_Name.Text), 98)

                    .Rows(0)("TBIL_POL_MEMB_SA_FACTOR") = Val(Trim(Me.txtPrem_SA_Factor.Text))
                    .Rows(0)("TBIL_POL_MEMB_TOT_EMOLUMENT") = CDbl(Trim(Me.txtTotal_Emolument.Text))
                    .Rows(0)("TBIL_POL_MEMB_TOT_SA") = CDbl(Trim(Me.txtSum_Assured.Text))
                    .Rows(0)("TBIL_POL_MEMB_MEDICAL_YN") = RTrim(Me.txtMedical_YN.Text)
                    .Rows(0)("TBIL_POL_MEMB_RATE_CODE") = Me.txtPrem_Rate_Code.Text
                    .Rows(0)("TBIL_POL_MEMB_RATE") = CDbl(Trim(Me.txtPrem_Rate.Text))
                    .Rows(0)("TBIL_POL_MEMB_RATE_PER") = Val(Trim(Me.txtPrem_Rate_Per.Text))

                    .Rows(0)("TBIL_POL_MEMB_PREM") = CDbl(dblPrem_Amt)
                    .Rows(0)("TBIL_POL_MEMB_PRO_RATE_PREM") = CDbl(dblPrem_Amt_ProRata)
                    .Rows(0)("TBIL_POL_MEMB_LOAD") = CDbl(dblLoad_Amt)

                    .Rows(0)("TBIL_POL_MEMB_FLAG") = "C"
                    '.Rows(0)("TBIL_POL_MEMB_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_POL_MEMB_KEYDTE") = Now
                End With

                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                Me.lblMsg.Text = "Record Saved to Database Successfully."

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
            Me.lblMsg.Text = ex.Message.ToString
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

        Me.txtBatch_Num.Enabled = False
        Me.cmdNext.Enabled = True


        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

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

        Me.txtBatch_Num.Enabled = False
        Me.cboBatch_Num.Enabled = False
        Call Proc_Batch()
        Call Proc_DataBind()
        Call Proc_DoNew()


    End Sub

    Private Sub Proc_DoSave_OLE()



        'Dim xlWSheet As Excel.Worksheet
        'Dim sVar As String = xlWSheet.Range("C5").Value.ToString()

        'GF/2014/1201/G/G001/G/0000001

        cboErr_List.Items.Clear()

        If Me.txtBatch_Num.Text = "" Then
            Me.txtFile_Upload.Text = ""
            Me.cmdFile_Upload.Enabled = False
            Me.lblMsg.Text = "Missing " & Me.lblBatch_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Val(Trim(Me.txtXLS_Data_Start_No.Text)) < 1 Then
            Me.lblMsg.Text = "Error. Minimum start excel no should be 1 "
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Val(Trim(Me.txtXLS_Data_End_No.Text)) < 1 Or Val(Trim(Me.txtXLS_Data_End_No.Text)) < Val(Trim(Me.txtXLS_Data_Start_No.Text)) Then
            Me.lblMsg.Text = "Error. Either excel end no less than 1 or less than excel start no "
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        blnStatusX = Proc_Batch_Check()
        If blnStatusX = False Then
            Exit Sub
        End If

        Me.lblMsg.Text = "File Name: " & Me.txtFile_Upload.Text
        'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

        If Trim(Me.txtFile_Upload.Text) = "" Then
            Me.txtFile_Upload.Text = ""
            Me.lblMsg.Text = "Missing document or file name ..."
            FirstMsg = "Javascript:alert('Missing document or file name. \nPlease select excel document with file extension .XLS or .XLSX')"
            Exit Sub
        End If

        If Right(LCase(Trim(Me.txtFile_Upload.Text)), 3) = "xls" Or _
           Right(LCase(Trim(Me.txtFile_Upload.Text)), 4) = "xlsx" Then
        Else
            Me.txtFile_Upload.Text = ""
            Me.lblMsg.Text = "Invalid document or file type. Expecting file of type .XLS or .XLSX ..."
            FirstMsg = "Javascript:alert('Invalid document or file name. \nPlease select excel document with file extension .XLS or .XLSX')"
            Exit Sub
        End If


        'strPATH = CType(ConfigurationManager.ConnectionStrings("LIFE_DOC_PATH").ToString, String)
        'strPATH = CType(ConfigurationManager.AppSettings("LIFE_DOC_PATH").ToString, String)

        Dim strFilename As String
        Dim strFileNameOnly As String = txtFile_Upload.Text
        'strFilename = strPATH & Me.txtFile_Upload.Text
        strPATH = Server.MapPath("~/App_Data/Schedules/")
        strFilename = strPATH & Me.txtFile_Upload.Text

        If System.IO.File.Exists(strFilename) = False Then
            Me.lblMsg.Text = "Document or file does not exist on the server ..."
            FirstMsg = "Javascript:alert('Document or file does not exist on the server')"
            Exit Sub
        End If

        Me.cmdFile_Upload.Enabled = False
        'Me.lblMsg.Text = UCase("File Upload successful.")

        'Try

        'Dim myxls_app_Demo As Microsoft.Office.Interop.Excel.Application = Nothing
        'myxls_app_Demo = New Microsoft.Office.Interop.Excel.Application
        'Dim myxls_app_Demo As Excel.Application
        'myxls_app_Demo = New Excel.Application()

        'myxls_app_Demo.Quit()
        'myxls_app_Demo.Application.Quit()
        'myxls_app_Demo = Nothing
        'Catch ex As Exception
        'Me.lblMsg.Text = "Error has occured. Reason: " & UCase(ex.Message.ToString)
        'FirstMsg = "Javascript:alert('" & RTrim("Unable to declare Excel object") & "')"
        'Exit Sub

        'End Try


        sFT = "Y"

        nRow = 2
        nCol = 0

        my_intCNT = 0

        'Dim key As Object
        ' Dim returnValue As Object

        '      ' ************************************************************************
        '      ' OK
        '      Dim app As Excel.Application = New Excel.Application()
        '      Dim workbook As Excel.Workbook
        '      Dim worksheet As Excel.Worksheet

        '      Dim xlsrange As Excel.Range

        '      Dim intC As Integer = 0

        '      strFilename = "H:\ABS-WEB\NIMASAOL\Database\Data Upload.xls"

        '      workbook = app.Workbooks.Open(strFilename, _
        'Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _
        'Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)


        '      'workbook = app.Workbooks.Open(strFilename)
        '      worksheet = workbook.Worksheets(1)

        '      'If (worksheet.Cells(1, 1).ToString() = "") Then
        '      'End If
        '      'Dim strname As String = worksheet.Cells(1, 2).ToString()
        '      'Response.Write("<br/>Cell Data " & strname)
        '      'Response.Write("<br/>Row: " & nRow & " - Col: " & worksheet.Cells(1, 3).ToString())

        '      For nRow = 1 To 5
        '
        '          xlsrange = worksheet.Cells(nRow, 4)
        '          'If (xlsrange Is Nothing Or xlsrange.Value2 Is Nothing) Then
        '          'Response.Write("<br/>Range object is null...")
        '          'Else
        '          Response.Write("<br/>Row: " & nRow & " - Range Value: " & xlsrange.Value.ToString() & " - Range Value2: " & xlsrange.Value2.ToString())
        '          'End If

        '      Next

        '      ' ************************************************************************


        'Try
        '    Dim xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
        '    xlApp = CType(CreateObject("Excel.Application"), Microsoft.Office.Interop.Excel.Application)
        'Catch ex As Exception
        '    Me.lblMsg.Text = "Error has occured. Reason: " & UCase(ex.Message.ToString)
        '    Response.Write("<br />Unable to create excel object. Reason: <br />" & Me.lblMsg.Text)
        '    'FirstMsg = "Javascript:alert('" & RTrim("Unable to create Excel object") & "')"
        '    'Exit Sub

        'End Try


        'Dim xlBook As Microsoft.Office.Interop.Excel.Workbook
        'Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet

        'xlBook = CType(xlApp.Workbooks.Add,  _
        '            Microsoft.Office.Interop.Excel.Workbook)
        'xlSheet = CType(xlBook.Worksheets(1),  _
        '            Microsoft.Office.Interop.Excel.Worksheet)

        '' The following statement puts text in the second row of the sheet.
        'xlSheet.Cells(2, 2) = "This is column B row 2"
        '' The following statement shows the sheet.
        'xlSheet.Application.Visible = True
        '' The following statement saves the sheet to the C:\Test.xls directory.
        'xlSheet.SaveAs("C:\Test.xls")

        '' Optionally, you can call xlApp.Quit to close the workbook.
        ''xlApp.Quit()

        'Dim myxls_app As Excel.Application = New Excel.Application()
        Dim myxls_workbook As Excel.Workbook
        Dim myxls_worksheet As Excel.Worksheet

        Dim myxls_range As Excel.Range


        ''Dim myxls_app As Microsoft.Office.Interop.Excel.Application
        ''Dim myxls_workbook As Microsoft.Office.Interop.Excel.Workbook
        ''Dim myxls_worksheet As Microsoft.Office.Interop.Excel.Worksheet

        ''Dim myxls_sheets As Microsoft.Office.Interop.Excel.Sheets
        ''Dim myxls_range As Microsoft.Office.Interop.Excel.Range


        '' *******************
        ''myxls_app = New Microsoft.Office.Interop.Excel.Application
        'myxls_app = New Excel.Application()

        ''myxls_workbook = New Microsoft.Office.Interop.Excel.Workbook


        Try
            ' myxls_workbook = myxls_app.Workbooks.Open(strFilename, , ReadOnly:=True)
            ' myxls_workbook = myxls_app.Workbooks.Open("c:\xlsdoc.xlsx")
            'myxls_workbook = myxls_app.Workbooks.Open(strFilename)

        Catch ex As Exception

            'myxls_worksheet = Nothing

            '    'myxls_workbook.SaveAs(strSaveFilename, Excel.XlFileFormat.xlWorkbookDefault)
            '    'myxls_workbook.Close(SaveChanges:=False)
            '    'myxls_workbook.Close(False)

            '    'myxls_workbook.Close(False)
            'myxls_workbook = Nothing


            '    'myxls_app.Workbooks.Close()
            'myxls_app.Quit()
            'myxls_app.Application.Quit()
            'myxls_app = Nothing

            Me.lblMsg.Text = "Error has occured. Reason: " & UCase(ex.Message.ToString)
            FirstMsg = "Javascript:alert('" & RTrim("Unable to access data") & "')"
            Exit Sub

        End Try


        ''   open workbook
        ''myxls_worksheet = myxls_app.Worksheets(1)
        ''myxls_worksheet = myxls_workbook.Sheets("sheetname_or_indexno")
        ''myxls_worksheet = myxls_workbook.Sheets("Sheet1")
        ''myxls_worksheet = myxls_workbook.Sheets(1)
        'myxls_worksheet = myxls_workbook.Worksheets(1)



        ''myxls_sheets = myxls_workbook.Sheets()
        ''myxls_sheets.Item(1)

        'myxls_range = myxls_worksheet.Cells


        Dim mystr_con As String = CType(Session("connstr"), String)
        Dim myole_con As OleDbConnection = New OleDbConnection(mystr_con)

        Try
            myole_con.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            FirstMsg = "Javascript:alert('" & "Unable to connect to database" & "')"

            GoTo MyLoop_End
        End Try


        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = "SYS"
        End Try


        Dim mystr_sql As String = ""
        Dim mystr_sn_param As String = ""
        Dim mycnt As Integer = 0

        mystr_sn_param = "GL_MEMBER_SN"

        my_File_Num = Me.txtFileNum.Text
        my_Prop_Num = Me.txtQuote_Num.Text
        my_Poly_Num = Me.txtPolNum.Text
        my_Batch_Num = Me.txtBatch_Num.Text


        strGen_Msg = ""
        Me.lblErr_List.Visible = False
        Me.cboErr_List.Items.Clear()
        Me.cboErr_List.Visible = False

        my_intCNT = 0

        Dim myole_cmd As OleDbCommand = Nothing

        nROW_MIN = Val(Me.txtXLS_Data_Start_No.Text)
        nROW_MAX = Val(Me.txtXLS_Data_End_No.Text)
        nRow = 2

        Try
            'ClientScript.RegisterStartupScript(Me.GetType(), "scrollMSG_JavaScript", "scrollMSG(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")", True)
        Catch ex As Exception

        End Try
        'add_date_added = General_Date_Validation(txtAdditionDate.Text, "Effective")
        'add_date_added = Format(CDate(add_date_added), "MM/dd/yyyy")
        '*************************************************************************************
        'Gather the validated values from the form and pass 
        'to the hashHelper function
        '*************************************************************************************

        'Added by Azeez
        'Initially GenStart_Date looses value 
        GenStart_Date = Convert.ToDateTime(DoConvertToDbDateFormat(txtGenStart_DateHidden.Text))

        'call the hashhelper function and pass the form values into it
        hashHelper.postFromExcel(strPATH, txtFile_Upload.Text.Trim, myUserIDX, my_Batch_Num, nROW_MIN, nROW_MAX, Me.txtPrem_Period_Yr.Text, mystr_con, _
       Me.txtPrem_SA_Factor.Text, my_File_Num, my_Prop_Num, my_Poly_Num, txtPrem_Rate_TypeNum.Text, txtPrem_Rate_Per.Text, txtPrem_Rate_Code.Text, _
       txtProduct_Num.Text, lstErrMsgs, Convert.ToInt16(txtRisk_Days.Text), 0, GenStart_Date, GenEnd_Date, txtStart_Date.Text, txtEnd_Date.Text, _
       MemJoin_Date, txtData_Source_SW.Text, txtPrem_Rate.Text, add_date_added)
        GoTo MyLoop_999a



MyLoop_Start:
        nRow = nRow + 1

        If nRow < nROW_MIN Then
            GoTo MyLoop_Start
        End If

        If nRow > nROW_MAX Then
            GoTo MyLoop_999
        End If

        'If nRow <= 2 Then
        '    GoTo MyLoop_Start
        'End If

        'xx = myxls_worksheet.Cells(nRow, 1).ToString
        'If Val(xx) = 0 Then
        'GoTo MyLoop_Start
        'End If

        xx = ""
        'xx = myxls_worksheet.Cells(nRow, 3).ToString

        myxls_range = myxls_worksheet.Cells(nRow, 3)
        xx = myxls_range.Text.ToString
        'xx = myxls_range.Item(nRow, 3)

        If Trim(xx.ToString) = "" Then
            GoTo MyLoop_Start
        End If


        ' The following statement puts text in the second row of the sheet.
        ' xlSheet.Cells(2, 2) = "This is column B row 2"

        'ok
        'xx = myxls_worksheet.Cells(nRow, 3).Text.ToString


        ' Initialize variables
        strGen_Msg = ""

        'my_File_Num = ""
        my_Staff_Num = ""
        my_Member_Name = ""
        my_DOB = ""
        my_AGE = ""
        my_Gender = ""
        my_Designation = ""
        my_Start_Date = ""
        my_End_Date = ""
        my_Tenor = "1"
        my_Tenor = Me.txtPrem_Period_Yr.Text
        my_SA_Factor = Val(Trim(Me.txtPrem_SA_Factor.Text))
        my_Basic_Sal = Val(0)
        my_House_Allow = Val(0)
        my_Transport_Allow = Val(0)
        my_Other_Allow = Val(0)
        my_Total_Salary = Val(0)
        my_Total_SA = Val(0)
        my_Medical_YN = "N"


        'myxls_range = myxls_worksheet.Cells(nRow, 1)
        'my_SNo = myxls_range.Text.ToString

        myxls_range = myxls_worksheet.Cells(nRow, 2)
        my_Staff_Num = myxls_range.Text.ToString

        myxls_range = myxls_worksheet.Cells(nRow, 3)
        my_Member_Name = myxls_range.Text.ToString

        ' ******************
        ' START DOB
        ' ******************
        Try
            myxls_range = myxls_worksheet.Cells(nRow, 4)
            my_DOB = myxls_range.Text.ToString
            'my_DOB = Format(myxls_range.Text, "dd/MM/yyyy")
            'my_DOB = CDate(my_DOB).ToString
            If Not IsDate(my_DOB) Then
                'my_DOB = Format(CDate(my_DOB), "dd/MM/yyyy")
            End If

        Catch ex As Exception
            myxls_range = myxls_worksheet.Cells(nRow, 4)
            my_DOB = CType(myxls_range.Text, String)
            'my_DOB = Format(myxls_range.Text, "dd/MM/yyyy")
        End Try
        If Val(Mid(my_DOB, 4, 2)) > 12 Then
            'my_DOB = Mid(LTrim(my_DOB), 4, 2) & "/" & Left(LTrim(my_DOB), 2) & "/" & Right(RTrim(my_DOB), 4)
        End If
        ' ******************
        ' END DOB
        ' ******************

        myxls_range = myxls_worksheet.Cells(nRow, 5)
        my_AGE = myxls_range.Text.ToString

        myxls_range = myxls_worksheet.Cells(nRow, 6)
        my_Gender = myxls_range.Text.ToString

        myxls_range = myxls_worksheet.Cells(nRow, 7)
        my_Designation = myxls_range.Text.ToString

        myxls_range = myxls_worksheet.Cells(nRow, 8)
        my_Start_Date = myxls_range.Text.ToString

        myxls_range = myxls_worksheet.Cells(nRow, 9)
        my_End_Date = myxls_range.Text.ToString

        myxls_range = myxls_worksheet.Cells(nRow, 10)
        my_Tenor = myxls_range.Text.ToString

        myxls_range = myxls_worksheet.Cells(nRow, 11)
        my_SA_Factor = Val(myxls_range.Text.ToString)

        myxls_range = myxls_worksheet.Cells(nRow, 12)
        Try
            my_Basic_Sal = Val(myxls_range.Text.ToString)
        Catch ex As Exception
            my_Basic_Sal = Val(0)
        End Try

        myxls_range = myxls_worksheet.Cells(nRow, 13)
        Try
            my_House_Allow = Val(myxls_range.Text.ToString)
        Catch ex As Exception
            my_House_Allow = Val(0)
        End Try

        myxls_range = myxls_worksheet.Cells(nRow, 14)
        Try
            my_Transport_Allow = Val(myxls_range.Text.ToString)
        Catch ex As Exception
            my_Transport_Allow = Val(0)
        End Try

        myxls_range = myxls_worksheet.Cells(nRow, 15)
        Try
            my_Other_Allow = Val(myxls_range.Text.ToString)
        Catch ex As Exception
            my_Other_Allow = Val(0)
        End Try

        myxls_range = myxls_worksheet.Cells(nRow, 16)
        Try
            my_Total_Salary = Val(myxls_range.Text.ToString)
        Catch ex As Exception
            my_Total_Salary = Val(0)
        End Try

        my_Total_SA = 0

        my_Tenor = Me.txtPrem_Period_Yr.Text
        myTerm = my_Tenor


        ' Response.Write("<br />Start Date: " & my_Start_Date & " - End Date: " & my_End_Date & " - DOB Date: " & my_DOB)

        'Validate date
        myarrData = Split(my_DOB, "/")
        If myarrData.Count <> 3 Then
            'Me.lblMsg.Text = "Missing or Invalid " & Me.lblMember_DOB.Text & ". Expecting full date in ddmmyyyy format ..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Incomplete date of birth - " & my_DOB.ToString
            GoTo MyLoop_888
        End If

        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = Left(myarrData(2), 4)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            'Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Invalid date of birth - " & strMyDte.ToString
            GoTo MyLoop_888
        End If

        'Me.txtMember_DOB.Text = RTrim(strMyDte)
        ''mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")

        my_Dte_DOB = Format(mydte, "MM/dd/yyyy")
        Dte_DOB = my_Dte_DOB

        Dte_Current = Now
        lngDOB_ANB = Val(DateDiff("yyyy", Dte_Current, my_Dte_DOB))
        If lngDOB_ANB < 0 Then
            lngDOB_ANB = lngDOB_ANB * -1
        End If

        If Dte_Current.Month >= Dte_DOB.Month Then
            lngDOB_ANB = lngDOB_ANB + 1
        End If
        If Val(my_AGE) = 0 Or Trim(my_AGE) = "" Then
            my_AGE = Trim(Str(lngDOB_ANB))
        End If

        ' ***********************************************************


        'Validate date
        myarrData = Split(my_Start_Date, "/")
        If myarrData.Count <> 3 Then
            'Me.lblMsg.Text = "Missing or Invalid " & Me.lblStart_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Incomplete start date - " & my_Start_Date.ToString
            GoTo MyLoop_888
        End If

        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = Left(myarrData(2), 4)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            'Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Invalid start date - " & strMyDte.ToString
            GoTo MyLoop_888
        End If

        'Me.txtMember_DOB.Text = RTrim(strMyDte)
        ''mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")

        my_Dte_Start = Format(mydte, "MM/dd/yyyy")
        MemJoin_Date = my_Dte_Start


        ' ***********************************************************

        'Validate date
        myarrData = Split(my_End_Date, "/")
        If myarrData.Count <> 3 Then
            'Me.lblMsg.Text = "Missing or Invalid " & Me.lblEnd_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Incomplete end date - " & my_End_Date.ToString
            GoTo MyLoop_888
        End If

        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = Left(myarrData(2), 4)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            'Me.lblMsg.Text = "Please enter valid date..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Invalid end date - " & strMyDte.ToString
            GoTo MyLoop_888
        End If

        'Me.txtEnd_Date.Text = RTrim(strMyDte)
        ''mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")

        my_Dte_End = Format(mydte, "MM/dd/yyyy")


        ' ***********************************************************

        If my_Dte_Start > my_Dte_End Then
            'Me.lblMsg.Text = "Error!. Start Date greater than End Date... "
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Start Date greater than End Date... "
            GoTo MyLoop_888
        End If


        If Trim(sFT) = "Y" Then
            sFT = "N"
            ' delete previous uploaded record
            mystr_sql = ""
            mystr_sql = "delete from TBIL_GRP_POLICY_MEMBERS"
            mystr_sql = mystr_sql & " where TBIL_POL_MEMB_FILE_NO = '" & RTrim(my_File_Num) & "'"
            mystr_sql = mystr_sql & " and TBIL_POL_MEMB_PROP_NO = '" & RTrim(my_Prop_Num) & "'"
            mystr_sql = mystr_sql & " and TBIL_POL_MEMB_BATCH_NO = '" & RTrim(Me.txtBatch_Num.Text) & "'"
            myole_cmd = New OleDbCommand(mystr_sql, myole_con)
            myole_cmd.CommandType = CommandType.Text
            myole_cmd.ExecuteNonQuery()
            myole_cmd.Dispose()
            myole_cmd = Nothing

            ' delete previous counter record
            mystr_sql = "delete from TBIL_UNDW_SYS_GEN_CNT where TBIL_SYS_GEN_CNT_ID = '" & RTrim(mystr_sn_param) & "' and TBIL_SYS_GEN_CNT_CODE = '" & RTrim(my_File_Num) & "'"
            myole_cmd = New OleDbCommand(mystr_sql, myole_con)
            myole_cmd.CommandType = CommandType.Text
            myole_cmd.ExecuteNonQuery()
            myole_cmd.Dispose()
            myole_cmd = Nothing

        End If

        dblPrem_Rate = 0
        dblPrem_Rate_Per = 1000
        dblPrem_Amt = 0
        dblPrem_Amt_ProRata = 0
        dblLoad_Amt = 0

        If Val(my_SA_Factor) = 0 Then
            my_SA_Factor = Val(Trim(Me.txtPrem_SA_Factor.Text))
        End If

        dblTotal_SA = CDbl(Trim(my_Total_Salary))
        If Val(my_SA_Factor) <> 0 Then
            dblTotal_SA = CDbl(Trim(my_Total_Salary)) * Val(Trim(my_SA_Factor))
        End If
        my_Total_SA = dblTotal_SA

        If dblTotal_SA >= dblFree_Cover_Limit Then
            my_Medical_YN = "Y"
        End If


        'Me.lblMsg.Text = xx.ToString
        'Response.Write("<br />row: " & nRow & " col: " & xx.ToString)

        my_Batch_Num = Me.txtBatch_Num.Text
        Me.txtBatch_Num.Enabled = False

        my_SNo = MOD_GEN.gnGet_Serial_No(RTrim("GET_SN_GL"), RTrim("GL_MEMBER_SN"), Trim(Me.txtFileNum.Text), Trim(Me.txtQuote_Num.Text))

        If Trim(my_Staff_Num) = "" Then
            my_Staff_Num = "STF_" & my_SNo.ToString
        End If

        Select Case UCase(Trim(Me.txtPrem_Rate_TypeNum.Text))
            Case "F"
                dblPrem_Rate = Val(Me.txtPrem_Rate.Text)
                dblPrem_Rate_Per = Val(Me.txtPrem_Rate_Per.Text)
            Case "N"
                dblPrem_Rate = "0.00"
                dblPrem_Rate_Per = "0"
            Case "T"
                myRetValue = MOD_GEN.gnGET_RATE("GET_GL_PREMIUM_RATE", "GRP", Me.txtPrem_Rate_Code.Text, Me.txtProduct_Num.Text, myTerm, Val(my_AGE), Me.lblMsg, Me.txtPrem_Rate_Per)
                If Left(LTrim(myRetValue), 3) = "ERR" Then
                    Me.cboPrem_Rate_Code.SelectedIndex = -1
                    'Me.txtPrem_Rate.Text = "0.00"
                    'Me.txtPrem_Rate_Per.Text = "0"
                    dblPrem_Rate = "0.00"
                    dblPrem_Rate_Per = "0"
                Else
                    'Me.txtPrem_Rate.Text = myRetValue.ToString
                    dblPrem_Rate = Trim(myRetValue.ToString)
                End If

        End Select


        'Response.Write("<br/>Value: " & dblPrem_Rate & " - " & myRetValue.ToString)

        'Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Rate)
        'Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Rate_Per)

        'dblPrem_Rate = CDbl(Trim(Me.txtPrem_Rate.Text))
        'dblPrem_Rate_Per = CDbl(Trim(Me.txtPrem_Rate_Per.Text))

        If dblTotal_SA <> 0 And dblPrem_Rate <> 0 And dblPrem_Rate_Per <> 0 Then
            dblPrem_Amt = dblTotal_SA * dblPrem_Rate / dblPrem_Rate_Per
            dblPrem_Amt_ProRata = dblPrem_Amt
        End If

        intRisk_Days = Val(DateDiff(DateInterval.Day, GenStart_Date, GenEnd_Date)) + 0
        intRisk_Days = Val(Me.txtRisk_Days.Text)
        'intDays_Diff = Val(DateDiff(DateInterval.Day, MemJoin_Date, GenEnd_Date)) + 0
        intDays_Diff = Val(DateDiff(DateInterval.Day, my_Dte_Start, my_Dte_End))

        If MemJoin_Date > GenStart_Date And dblPrem_Amt <> 0 And intDays_Diff <> 0 Then
            dblPrem_Amt_ProRata = Format((dblPrem_Amt / intRisk_Days) * intDays_Diff, "#########0.00")
        End If




        '**********************************************************************
        ' Below is Sunkanmi's attempt to write into the DB schedule but
        ' some of the values will be picked from the form and sent into the 
        ' hashHelper function call built by James. This is done just before the 
        ' entry into the my_loop_start
        '**********************************************************************
        'mystr_sql = "insert into table_name(fld1, fld1) values(@val1, @val2)"

        'mystr_sql = "SPGL_TBIL_GRP_POLICY_MEMBERS_INSERT"

        'myole_cmd = New OleDbCommand()
        'myole_cmd.Connection = myole_con
        ''myole_cmd.CommandType = CommandType.Text
        'myole_cmd.CommandType = CommandType.StoredProcedure
        'myole_cmd.CommandText = mystr_sql

        'myole_cmd.Parameters.AddWithValue("@p01", RTrim(my_File_Num))
        'myole_cmd.Parameters.AddWithValue("@p02", Val(0))
        'myole_cmd.Parameters.AddWithValue("@p03", RTrim("G"))
        'myole_cmd.Parameters.AddWithValue("@p04", RTrim(my_Prop_Num))
        'myole_cmd.Parameters.AddWithValue("@p05", RTrim(my_Poly_Num))
        'myole_cmd.Parameters.AddWithValue("@p05A", RTrim(my_Batch_Num))
        'myole_cmd.Parameters.AddWithValue("@p05B", RTrim(my_Staff_Num))
        'myole_cmd.Parameters.AddWithValue("@p06", Val(my_SNo))
        'myole_cmd.Parameters.AddWithValue("@p07", RTrim(my_Gender))
        'myole_cmd.Parameters.AddWithValue("@p08", Format(my_Dte_DOB, "MM/dd/yyyy"))
        'myole_cmd.Parameters.AddWithValue("@p09", Val(my_AGE))
        'myole_cmd.Parameters.AddWithValue("@p10", Format(my_Dte_Start, "MM/dd/yyyy"))
        'myole_cmd.Parameters.AddWithValue("@p11", Format(my_Dte_End, "MM/dd/yyyy"))
        'myole_cmd.Parameters.AddWithValue("@p12", Val(my_Tenor))
        'myole_cmd.Parameters.AddWithValue("@p13", RTrim(my_Designation))
        'myole_cmd.Parameters.AddWithValue("@p14", Left(RTrim(my_Member_Name), 95))
        'myole_cmd.Parameters.AddWithValue("@p14A", CDbl(Trim(my_SA_Factor)))
        'myole_cmd.Parameters.AddWithValue("@p14B", CDbl(Trim(my_Total_Salary)))
        'myole_cmd.Parameters.AddWithValue("@p15", CDbl(Trim(my_Total_SA)))
        'myole_cmd.Parameters.AddWithValue("@p16", RTrim(my_Medical_YN))

        'myole_cmd.Parameters.AddWithValue("@p17", CDbl(dblPrem_Rate))
        'myole_cmd.Parameters.AddWithValue("@p18", CDbl(dblPrem_Rate_Per))
        'myole_cmd.Parameters.AddWithValue("@p19", CDbl(dblPrem_Amt))
        'myole_cmd.Parameters.AddWithValue("@p20", CDbl(dblPrem_Amt_ProRata))
        'myole_cmd.Parameters.AddWithValue("@p21", CDbl(dblLoad_Amt))

        'myole_cmd.Parameters.AddWithValue("@p22", RTrim(Me.txtData_Source_SW.Text))
        'myole_cmd.Parameters.AddWithValue("@p23", RTrim(Me.txtFile_Upload.Text))

        'myole_cmd.Parameters.AddWithValue("@p24", vbNull)
        'myole_cmd.Parameters.AddWithValue("@p25", RTrim("A"))
        'myole_cmd.Parameters.AddWithValue("@p26", RTrim(myUserIDX))
        'myole_cmd.Parameters.AddWithValue("@p27", Format(Now, "MM/dd/yyyy"))


        'Try
        '    mycnt = myole_cmd.ExecuteNonQuery()
        '    If mycnt >= 1 Then
        '        my_intCNT = my_intCNT + 1
        '    Else
        '        strGen_Msg = " * Error!. Row: " & nRow.ToString & " record not save... "
        '    End If
        'Catch ex As Exception
        '    strGen_Msg = " * Error while saving Row: " & nRow.ToString & " record... "

        'End Try

        'myole_cmd.Dispose()
        'myole_cmd = Nothing








MyLoop_888:
        If strGen_Msg <> "" Then
            Me.cboErr_List.Items.Add(strGen_Msg.ToString)
            Me.lblErr_List.Visible = True
            Me.cboErr_List.Visible = True
        End If

        strGen_Msg = ""

        GoTo MyLoop_Start


MyLoop_999:

        Try
            ClientScript.RegisterStartupScript(Me.GetType(), "scrollMSG_JavaScript", "scrollMSG_End(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")", True)
        Catch ex As Exception

        End Try

        If my_intCNT >= 1 Then
            FirstMsg = "Javascript:alert('" & RTrim("File Upload successful - ") & Me.txtFile_Upload.Text & "')"
        Else
            FirstMsg = "Javascript:alert('" & RTrim("File Upload NOT successful - ") & Me.txtFile_Upload.Text & "')"
        End If

MyLoop_999a:
        If lstErrMsgs.Count > 1 Then
            For i = 0 To lstErrMsgs.Count - 1
                cboErr_List.Items.Add(lstErrMsgs.Item(i))
            Next

            Me.lblErr_List.Visible = True
            Me.cboErr_List.Visible = True


            FirstMsg = "Javascript:alert('" & RTrim("File Upload NOT successful - ") & Me.txtFile_Upload.Text & "')"

        Else
            Try
                ClientScript.RegisterStartupScript(Me.GetType(), "scrollMSG_JavaScript", "scrollMSG_End(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")", True)
            Catch ex As Exception

            End Try

            FirstMsg = "Javascript:alert('" & RTrim("File Upload successful - ") & Me.txtFile_Upload.Text & "')"

        End If
        GoTo MyLoop_End

MyLoop_End:


        myole_cmd = Nothing

        If myole_con.State = ConnectionState.Open Then
            myole_con.Close()
        End If
        myole_con = Nothing



        myxls_worksheet = Nothing

        Call Proc_Batch()
        Call Proc_DataBind()

    End Sub
    Private Function General_Date_Validation(ByVal _fromDateControl As String, ByVal _valueDescr As String) As String


        If RTrim(_fromDateControl) = "" Then
            Me.lblMsg.Text = "Missing " & _valueDescr & " Date "
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Function
        End If


        If RTrim(_fromDateControl) = "" Or Len(Trim(_fromDateControl)) <> 10 Then
            Me.lblMsg.Text = "Missing or Invalid date "
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Function
        End If

        'Validate date
        myarrData = Split(_fromDateControl, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & _fromDateControl & " Date. Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Function
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
            Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Return Nothing
        End If
        'Me.txtAdditionDate.Text = RTrim(strMyDte)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)

        Return mydteX
    End Function

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
    '       MemJoin_Date, txtData_Source_SW.Text, txtPrem_Rate.Text, add_date_added)
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


    '        Call Proc_Batch()
    '        Call Proc_DataBind()

    '    End Sub

    Private Sub Proc_DoSave_Upload()

        'Dim xlWSheet As Excel.Worksheet
        'Dim sVar As String = xlWSheet.Range("C5").Value.ToString()

        'GF/2014/1201/G/G001/G/0000001

        cboErr_List.Items.Clear()

        If Me.txtBatch_Num.Text = "" Then
            Me.txtFile_Upload.Text = ""
            Me.cmdFile_Upload.Enabled = False
            Me.lblMsg.Text = "Missing " & Me.lblBatch_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Val(Trim(Me.txtXLS_Data_Start_No.Text)) < 1 Then
            Me.lblMsg.Text = "Error. Minimum start excel no should be 1 "
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Val(Trim(Me.txtXLS_Data_End_No.Text)) < 1 Or Val(Trim(Me.txtXLS_Data_End_No.Text)) < Val(Trim(Me.txtXLS_Data_Start_No.Text)) Then
            Me.lblMsg.Text = "Error. Either excel end no less than 1 or less than excel start no "
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        blnStatusX = Proc_Batch_Check()
        If blnStatusX = False Then
            Exit Sub
        End If

        Me.lblMsg.Text = "File Name: " & Me.txtFile_Upload.Text
        'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

        If Trim(Me.txtFile_Upload.Text) = "" Then
            Me.txtFile_Upload.Text = ""
            Me.lblMsg.Text = "Missing document or file name ..."
            FirstMsg = "Javascript:alert('Missing document or file name. \nPlease select excel document with file extension .XLS or .XLSX')"
            Exit Sub
        End If

        If Right(LCase(Trim(Me.txtFile_Upload.Text)), 3) = "xls" Or _
           Right(LCase(Trim(Me.txtFile_Upload.Text)), 4) = "xlsx" Then
        Else
            Me.txtFile_Upload.Text = ""
            Me.lblMsg.Text = "Invalid document or file type. Expecting file of type .XLS or .XLSX ..."
            FirstMsg = "Javascript:alert('Invalid document or file name. \nPlease select excel document with file extension .XLS or .XLSX')"
            Exit Sub
        End If


        'Commented by Azeez because this section does not correspond to the section in Proc_DoSave_OLE
        'strPATH = CType(ConfigurationManager.AppSettings("LIFE_DOC_PATH").ToString, String)

        'Dim strFilename As String = "C:\Temp\test1.xls"
        'strFilename = strPATH & Me.txtFile_Upload.Text



        Dim strFilename As String
        Dim strFileNameOnly As String = txtFile_Upload.Text
        'strFilename = strPATH & Me.txtFile_Upload.Text
        strPATH = Server.MapPath("~/App_Data/Schedules/")
        strFilename = strPATH & Me.txtFile_Upload.Text



        If System.IO.File.Exists(strFilename) = False Then
            Me.lblMsg.Text = "Document or file does not exist on the server ..."
            FirstMsg = "Javascript:alert('Document or file does not exist on the server')"
            Exit Sub
        End If

        Me.cmdFile_Upload.Enabled = False
        'Me.lblMsg.Text = UCase("File Upload successful.")

        Try

            'Dim myxls_app_Demo As Microsoft.Office.Interop.Excel.Application = Nothing
            'myxls_app_Demo = New Microsoft.Office.Interop.Excel.Application
            'Dim myxls_app_Demo As Excel.Application
            'myxls_app_Demo = New Excel.Application()

            'myxls_app_Demo.Quit()
            'myxls_app_Demo.Application.Quit()
            'myxls_app_Demo = Nothing
        Catch ex As Exception
            Me.lblMsg.Text = "Error has occured. Reason: " & UCase(ex.Message.ToString)
            FirstMsg = "Javascript:alert('" & RTrim("Unable to declare Excel object") & "')"
            Exit Sub

        End Try


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
        Dim nRow As Integer = 1
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

        sFT = "Y"

        nRow = 0
        nCol = 0

        my_intCNT = 0

        'Dim key As Object
        ' Dim returnValue As Object

        '      ' ************************************************************************
        '      ' OK
        '      Dim app As Excel.Application = New Excel.Application()
        '      Dim workbook As Excel.Workbook
        '      Dim worksheet As Excel.Worksheet

        '      Dim xlsrange As Excel.Range

        '      Dim intC As Integer = 0

        '      strFilename = "H:\ABS-WEB\NIMASAOL\Database\Data Upload.xls"

        '      workbook = app.Workbooks.Open(strFilename, _
        'Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _
        'Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)


        '      'workbook = app.Workbooks.Open(strFilename)
        '      worksheet = workbook.Worksheets(1)

        '      'If (worksheet.Cells(1, 1).ToString() = "") Then
        '      'End If
        '      'Dim strname As String = worksheet.Cells(1, 2).ToString()
        '      'Response.Write("<br/>Cell Data " & strname)
        '      'Response.Write("<br/>Row: " & nRow & " - Col: " & worksheet.Cells(1, 3).ToString())

        '      For nRow = 1 To 5

        '          xlsrange = worksheet.Cells(nRow, 4)
        '          'If (xlsrange Is Nothing Or xlsrange.Value2 Is Nothing) Then
        '          'Response.Write("<br/>Range object is null...")
        '          'Else
        '          Response.Write("<br/>Row: " & nRow & " - Range Value: " & xlsrange.Value.ToString() & " - Range Value2: " & xlsrange.Value2.ToString())
        '          'End If

        '      Next

        '      ' ************************************************************************


        'Try
        '    Dim xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
        '    xlApp = CType(CreateObject("Excel.Application"), Microsoft.Office.Interop.Excel.Application)
        'Catch ex As Exception
        '    Me.lblMsg.Text = "Error has occured. Reason: " & UCase(ex.Message.ToString)
        '    Response.Write("<br />Unable to create excel object. Reason: <br />" & Me.lblMsg.Text)
        '    'FirstMsg = "Javascript:alert('" & RTrim("Unable to create Excel object") & "')"
        '    'Exit Sub

        'End Try


        'Dim xlBook As Microsoft.Office.Interop.Excel.Workbook
        'Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet

        'xlBook = CType(xlApp.Workbooks.Add,  _
        '            Microsoft.Office.Interop.Excel.Workbook)
        'xlSheet = CType(xlBook.Worksheets(1),  _
        '            Microsoft.Office.Interop.Excel.Worksheet)

        '' The following statement puts text in the second row of the sheet.
        'xlSheet.Cells(2, 2) = "This is column B row 2"
        '' The following statement shows the sheet.
        'xlSheet.Application.Visible = True
        '' The following statement saves the sheet to the C:\Test.xls directory.
        'xlSheet.SaveAs("C:\Test.xls")

        '' Optionally, you can call xlApp.Quit to close the workbook.
        ''xlApp.Quit()

        'Dim myxls_app As Excel.Application = New Excel.Application()
        'Dim myxls_workbook As Excel.Workbook
        'Dim myxls_worksheet As Excel.Worksheet

        'Dim myxls_range As Excel.Range


        ''Dim myxls_app As Microsoft.Office.Interop.Excel.Application
        ''Dim myxls_workbook As Microsoft.Office.Interop.Excel.Workbook
        ''Dim myxls_worksheet As Microsoft.Office.Interop.Excel.Worksheet

        ''Dim myxls_sheets As Microsoft.Office.Interop.Excel.Sheets
        ''Dim myxls_range As Microsoft.Office.Interop.Excel.Range


        '' *******************
        ''myxls_app = New Microsoft.Office.Interop.Excel.Application
        'myxls_app = New Excel.Application()

        ''myxls_workbook = New Microsoft.Office.Interop.Excel.Workbook


        Try
            ' myxls_workbook = myxls_app.Workbooks.Open(strFilename, , ReadOnly:=True)
            ' myxls_workbook = myxls_app.Workbooks.Open("c:\xlsdoc.xlsx")
            'myxls_workbook = myxls_app.Workbooks.Open(strFilename)

        Catch ex As Exception

            'myxls_worksheet = Nothing

            '    'myxls_workbook.SaveAs(strSaveFilename, Excel.XlFileFormat.xlWorkbookDefault)
            '    'myxls_workbook.Close(SaveChanges:=False)
            '    'myxls_workbook.Close(False)

            '    'myxls_workbook.Close(False)
            'myxls_workbook = Nothing


            '    'myxls_app.Workbooks.Close()
            'myxls_app.Quit()
            'myxls_app.Application.Quit()
            'myxls_app = Nothing

            Me.lblMsg.Text = "Error has occured. Reason: " & UCase(ex.Message.ToString)
            FirstMsg = "Javascript:alert('" & RTrim("Unable to access data") & "')"
            Exit Sub

        End Try


        ''   open workbook
        ''myxls_worksheet = myxls_app.Worksheets(1)
        ''myxls_worksheet = myxls_workbook.Sheets("sheetname_or_indexno")
        ''myxls_worksheet = myxls_workbook.Sheets("Sheet1")
        ''myxls_worksheet = myxls_workbook.Sheets(1)
        'myxls_worksheet = myxls_workbook.Worksheets(1)



        ''myxls_sheets = myxls_workbook.Sheets()
        ''myxls_sheets.Item(1)

        'myxls_range = myxls_worksheet.Cells


        Dim mystr_con As String = CType(Session("connstr"), String)
        Dim myole_con As OleDbConnection = New OleDbConnection(mystr_con)

        Try
            '    myole_con.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            FirstMsg = "Javascript:alert('" & "Unable to connect to database" & "')"

            GoTo MyLoop_End
        End Try


        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = "SYS"
        End Try


        Dim mystr_sql As String = ""
        Dim mystr_sn_param As String = ""
        Dim mycnt As Integer = 0

        mystr_sn_param = "GL_MEMBER_SN"

        my_File_Num = Me.txtFileNum.Text
        my_Prop_Num = Me.txtQuote_Num.Text
        my_Poly_Num = Me.txtPolNum.Text
        my_Batch_Num = Me.txtBatch_Num.Text

        strGen_Msg = ""
        Me.lblErr_List.Visible = False
        Me.cboErr_List.Items.Clear()
        Me.cboErr_List.Visible = False

        my_intCNT = 0

        Dim myole_cmd As OleDbCommand = Nothing

        nROW_MIN = Val(Me.txtXLS_Data_Start_No.Text)
        nROW_MAX = Val(Me.txtXLS_Data_End_No.Text)
        nRow = 0

        Try
            'ClientScript.RegisterStartupScript(Me.GetType(), "scrollMSG_JavaScript", "scrollMSG(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")", True)
        Catch ex As Exception

        End Try

        '*************************************************************************************
        'Gather the validated values from the form and pass 
        'to the hashHelper function
        '*************************************************************************************

        'HERE CAHNGE
        'call the hashhelper function and pass the form values into it
        'Commented by Azeez becasue it should b a replica of Proc_DoSave_OLE which is working fine
        ' hashHelper.postFromExcel(strPATH, strFilename, myUserIDX, my_Batch_Num, nROW_MIN, nROW_MAX, Me.txtPrem_Period_Yr.Text, mystr_con, _
        'Me.txtPrem_SA_Factor.Text, my_File_Num, my_Prop_Num, my_Poly_Num, txtPrem_Rate_TypeNum.Text, txtPrem_Rate_Per.Text, txtPrem_Rate_Code.Text, _
        'txtProduct_Num.Text, lstErrMsgs, Convert.ToInt16(txtRisk_Days.Text), 0, GenStart_Date, GenEnd_Date, txtStart_Date.Text, txtEnd_Date.Text, _
        'MemJoin_Date, txtData_Source_SW.Text, txtPrem_Rate.Text, String.Empty)
        ' GoTo MyLoop_999a

        'Added by Azeez
        'Initially GenStart_Date looses value 
        GenStart_Date = Convert.ToDateTime(DoConvertToDbDateFormat(txtGenStart_DateHidden.Text))

        hashHelper.postFromExcel(strPATH, txtFile_Upload.Text.Trim, myUserIDX, my_Batch_Num, nROW_MIN, nROW_MAX, Me.txtPrem_Period_Yr.Text, mystr_con, _
     Me.txtPrem_SA_Factor.Text, my_File_Num, my_Prop_Num, my_Poly_Num, txtPrem_Rate_TypeNum.Text, txtPrem_Rate_Per.Text, txtPrem_Rate_Code.Text, _
     txtProduct_Num.Text, lstErrMsgs, Convert.ToInt16(txtRisk_Days.Text), 0, GenStart_Date, GenEnd_Date, txtStart_Date.Text, txtEnd_Date.Text, _
     MemJoin_Date, txtData_Source_SW.Text, txtPrem_Rate.Text, String.Empty)
        GoTo MyLoop_999a


MyLoop_Start:
        nRow = nRow + 1

        If nRow < nROW_MIN Then
            GoTo MyLoop_Start
        End If

        If nRow > nROW_MAX Then
            GoTo MyLoop_999
        End If

        'If nRow <= 2 Then
        '    GoTo MyLoop_Start
        'End If

        'xx = myxls_worksheet.Cells(nRow, 1).ToString
        'If Val(xx) = 0 Then
        'GoTo MyLoop_Start
        'End If

        xx = ""
        'xx = myxls_worksheet.Cells(nRow, 3).ToString

        'myxls_range = myxls_worksheet.Cells(nRow, 3)
        'xx = myxls_range.Text.ToString
        ''xx = myxls_range.Item(nRow, 3)

        'If Trim(xx.ToString) = "" Then
        '    GoTo MyLoop_Start
        'End If


        ' The following statement puts text in the second row of the sheet.
        ' xlSheet.Cells(2, 2) = "This is column B row 2"

        'ok
        'xx = myxls_worksheet.Cells(nRow, 3).Text.ToString


        ' Initialize variables
        strGen_Msg = ""

        'my_File_Num = ""
        my_Staff_Num = ""
        my_Member_Name = ""
        my_DOB = ""
        my_AGE = ""
        my_Gender = ""
        my_Designation = ""
        my_Start_Date = ""
        my_End_Date = ""
        my_Tenor = "1"
        my_Tenor = Me.txtPrem_Period_Yr.Text
        my_SA_Factor = Val(Trim(Me.txtPrem_SA_Factor.Text))
        my_Basic_Sal = Val(0)
        my_House_Allow = Val(0)
        my_Transport_Allow = Val(0)
        my_Other_Allow = Val(0)
        my_Total_Salary = Val(0)
        my_Total_SA = Val(0)
        my_Medical_YN = "N"


        'myxls_range = myxls_worksheet.Cells(nRow, 1)
        'my_SNo = myxls_range.Text.ToString

        'myxls_range = myxls_worksheet.Cells(nRow, 2)
        'my_Staff_Num = myxls_range.Text.ToString

        'myxls_range = myxls_worksheet.Cells(nRow, 3)
        'my_Member_Name = myxls_range.Text.ToString

        ' ******************
        ' START DOB
        ' ******************
        Try
            'myxls_range = myxls_worksheet.Cells(nRow, 4)
            'my_DOB = myxls_range.Text.ToString
            'my_DOB = Format(myxls_range.Text, "dd/MM/yyyy")
            'my_DOB = CDate(my_DOB).ToString
            If Not IsDate(my_DOB) Then
                'my_DOB = Format(CDate(my_DOB), "dd/MM/yyyy")
            End If

        Catch ex As Exception
            'myxls_range = myxls_worksheet.Cells(nRow, 4)
            'my_DOB = CType(myxls_range.Text, String)
            'my_DOB = Format(myxls_range.Text, "dd/MM/yyyy")
        End Try
        If Val(Mid(my_DOB, 4, 2)) > 12 Then
            'my_DOB = Mid(LTrim(my_DOB), 4, 2) & "/" & Left(LTrim(my_DOB), 2) & "/" & Right(RTrim(my_DOB), 4)
        End If
        ' ******************
        ' END DOB
        ' ******************

        'myxls_range = myxls_worksheet.Cells(nRow, 5)
        'my_AGE = myxls_range.Text.ToString

        'myxls_range = myxls_worksheet.Cells(nRow, 6)
        'my_Gender = myxls_range.Text.ToString

        'myxls_range = myxls_worksheet.Cells(nRow, 7)
        'my_Designation = myxls_range.Text.ToString

        'myxls_range = myxls_worksheet.Cells(nRow, 8)
        'my_Start_Date = myxls_range.Text.ToString

        'myxls_range = myxls_worksheet.Cells(nRow, 9)
        'my_End_Date = myxls_range.Text.ToString

        'myxls_range = myxls_worksheet.Cells(nRow, 10)
        'my_Tenor = myxls_range.Text.ToString

        'myxls_range = myxls_worksheet.Cells(nRow, 11)
        'my_SA_Factor = Val(myxls_range.Text.ToString)

        'myxls_range = myxls_worksheet.Cells(nRow, 12)
        'Try
        '    my_Basic_Sal = Val(myxls_range.Text.ToString)
        'Catch ex As Exception
        '    my_Basic_Sal = Val(0)
        'End Try

        'myxls_range = myxls_worksheet.Cells(nRow, 13)
        'Try
        '    my_House_Allow = Val(myxls_range.Text.ToString)
        'Catch ex As Exception
        '    my_House_Allow = Val(0)
        'End Try

        'myxls_range = myxls_worksheet.Cells(nRow, 14)
        'Try
        '    my_Transport_Allow = Val(myxls_range.Text.ToString)
        'Catch ex As Exception
        '    my_Transport_Allow = Val(0)
        'End Try

        'myxls_range = myxls_worksheet.Cells(nRow, 15)
        'Try
        '    my_Other_Allow = Val(myxls_range.Text.ToString)
        'Catch ex As Exception
        '    my_Other_Allow = Val(0)
        'End Try

        'myxls_range = myxls_worksheet.Cells(nRow, 16)
        'Try
        '    my_Total_Salary = Val(myxls_range.Text.ToString)
        'Catch ex As Exception
        '    my_Total_Salary = Val(0)
        'End Try

        my_Total_SA = 0

        my_Tenor = Me.txtPrem_Period_Yr.Text
        myTerm = my_Tenor


        ' Response.Write("<br />Start Date: " & my_Start_Date & " - End Date: " & my_End_Date & " - DOB Date: " & my_DOB)

        'Validate date
        myarrData = Split(my_DOB, "/")
        If myarrData.Count <> 3 Then
            'Me.lblMsg.Text = "Missing or Invalid " & Me.lblMember_DOB.Text & ". Expecting full date in ddmmyyyy format ..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Incomplete date of birth - " & my_DOB.ToString
            GoTo MyLoop_888
        End If

        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = Left(myarrData(2), 4)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            'Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Invalid date of birth - " & strMyDte.ToString
            GoTo MyLoop_888
        End If

        'Me.txtMember_DOB.Text = RTrim(strMyDte)
        ''mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")

        my_Dte_DOB = Format(mydte, "MM/dd/yyyy")
        Dte_DOB = my_Dte_DOB

        Dte_Current = Now
        lngDOB_ANB = Val(DateDiff("yyyy", Dte_Current, my_Dte_DOB))
        If lngDOB_ANB < 0 Then
            lngDOB_ANB = lngDOB_ANB * -1
        End If

        If Dte_Current.Month >= Dte_DOB.Month Then
            lngDOB_ANB = lngDOB_ANB + 1
        End If
        If Val(my_AGE) = 0 Or Trim(my_AGE) = "" Then
            my_AGE = Trim(Str(lngDOB_ANB))
        End If

        ' ***********************************************************


        'Validate date
        myarrData = Split(my_Start_Date, "/")
        If myarrData.Count <> 3 Then
            'Me.lblMsg.Text = "Missing or Invalid " & Me.lblStart_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Incomplete start date - " & my_Start_Date.ToString
            GoTo MyLoop_888
        End If

        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = Left(myarrData(2), 4)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            'Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Invalid start date - " & strMyDte.ToString
            GoTo MyLoop_888
        End If

        'Me.txtMember_DOB.Text = RTrim(strMyDte)
        ''mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")

        my_Dte_Start = Format(mydte, "MM/dd/yyyy")
        MemJoin_Date = my_Dte_Start


        ' ***********************************************************

        'Validate date
        myarrData = Split(my_End_Date, "/")
        If myarrData.Count <> 3 Then
            'Me.lblMsg.Text = "Missing or Invalid " & Me.lblEnd_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Incomplete end date - " & my_End_Date.ToString
            GoTo MyLoop_888
        End If

        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = Left(myarrData(2), 4)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            'Me.lblMsg.Text = "Please enter valid date..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Invalid end date - " & strMyDte.ToString
            GoTo MyLoop_888
        End If

        'Me.txtEnd_Date.Text = RTrim(strMyDte)
        ''mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")

        my_Dte_End = Format(mydte, "MM/dd/yyyy")


        ' ***********************************************************

        If my_Dte_Start > my_Dte_End Then
            'Me.lblMsg.Text = "Error!. Start Date greater than End Date... "
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            strGen_Msg = " * Row: " & nRow.ToString & " - Start Date greater than End Date... "
            GoTo MyLoop_888
        End If

        ' ***********************************************************

        'my_Dte_DOB = Now
        'my_AGE = "21"
        'my_Dte_Start = Now
        'my_Dte_End = Now

        ' ***********************************************************

        If Trim(sFT) = "Y" Then
            sFT = "N"
            ' delete previous uploaded record
            mystr_sql = ""
            mystr_sql = "delete from TBIL_GRP_POLICY_MEMBERS"
            mystr_sql = mystr_sql & " where TBIL_POL_MEMB_FILE_NO = '" & RTrim(my_File_Num) & "'"
            mystr_sql = mystr_sql & " and TBIL_POL_MEMB_PROP_NO = '" & RTrim(my_Prop_Num) & "'"
            mystr_sql = mystr_sql & " and TBIL_POL_MEMB_BATCH_NO = '" & RTrim(Me.txtBatch_Num.Text) & "'"
            myole_cmd = New OleDbCommand(mystr_sql, myole_con)
            myole_cmd.CommandType = CommandType.Text
            myole_cmd.ExecuteNonQuery()
            myole_cmd.Dispose()
            myole_cmd = Nothing

            ' delete previous counter record
            mystr_sql = "delete from TBIL_UNDW_SYS_GEN_CNT where TBIL_SYS_GEN_CNT_ID = '" & RTrim(mystr_sn_param) & "' and TBIL_SYS_GEN_CNT_CODE = '" & RTrim(my_File_Num) & "'"
            myole_cmd = New OleDbCommand(mystr_sql, myole_con)
            myole_cmd.CommandType = CommandType.Text
            myole_cmd.ExecuteNonQuery()
            myole_cmd.Dispose()
            myole_cmd = Nothing

        End If

        dblPrem_Rate = 0
        dblPrem_Rate_Per = 1000
        dblPrem_Amt = 0
        dblPrem_Amt_ProRata = 0
        dblLoad_Amt = 0

        If Val(my_SA_Factor) = 0 Then
            my_SA_Factor = Val(Trim(Me.txtPrem_SA_Factor.Text))
        End If

        dblTotal_SA = CDbl(Trim(my_Total_Salary))
        If Val(my_SA_Factor) <> 0 Then
            dblTotal_SA = CDbl(Trim(my_Total_Salary)) * Val(Trim(my_SA_Factor))
        End If
        my_Total_SA = dblTotal_SA

        If dblTotal_SA >= dblFree_Cover_Limit Then
            my_Medical_YN = "Y"
        End If


        'Me.lblMsg.Text = xx.ToString
        'Response.Write("<br />row: " & nRow & " col: " & xx.ToString)

        my_Batch_Num = Me.txtBatch_Num.Text
        Me.txtBatch_Num.Enabled = False

        my_SNo = MOD_GEN.gnGet_Serial_No(RTrim("GET_SN_GL"), RTrim("GL_MEMBER_SN"), Trim(Me.txtFileNum.Text), Trim(Me.txtQuote_Num.Text))

        If Trim(my_Staff_Num) = "" Then
            my_Staff_Num = "STF_" & my_SNo.ToString
        End If

        Select Case UCase(Trim(Me.txtPrem_Rate_TypeNum.Text))
            Case "F"
                dblPrem_Rate = Val(Me.txtPrem_Rate.Text)
                dblPrem_Rate_Per = Val(Me.txtPrem_Rate_Per.Text)
            Case "N"
                dblPrem_Rate = "0.00"
                dblPrem_Rate_Per = "0"
            Case "T"
                myRetValue = MOD_GEN.gnGET_RATE("GET_GL_PREMIUM_RATE", "GRP", Me.txtPrem_Rate_Code.Text, Me.txtProduct_Num.Text, myTerm, Val(my_AGE), Me.lblMsg, Me.txtPrem_Rate_Per)
                If Left(LTrim(myRetValue), 3) = "ERR" Then
                    Me.cboPrem_Rate_Code.SelectedIndex = -1
                    'Me.txtPrem_Rate.Text = "0.00"
                    'Me.txtPrem_Rate_Per.Text = "0"
                    dblPrem_Rate = "0.00"
                    dblPrem_Rate_Per = "0"
                Else
                    'Me.txtPrem_Rate.Text = myRetValue.ToString
                    dblPrem_Rate = Trim(myRetValue.ToString)
                End If

        End Select


        'Response.Write("<br/>Value: " & dblPrem_Rate & " - " & myRetValue.ToString)

        'Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Rate)
        'Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Rate_Per)

        'dblPrem_Rate = CDbl(Trim(Me.txtPrem_Rate.Text))
        'dblPrem_Rate_Per = CDbl(Trim(Me.txtPrem_Rate_Per.Text))

        If dblTotal_SA <> 0 And dblPrem_Rate <> 0 And dblPrem_Rate_Per <> 0 Then
            dblPrem_Amt = dblTotal_SA * dblPrem_Rate / dblPrem_Rate_Per
            dblPrem_Amt_ProRata = dblPrem_Amt
        End If

        intRisk_Days = Val(DateDiff(DateInterval.Day, GenStart_Date, GenEnd_Date)) + 0
        intRisk_Days = Val(Me.txtRisk_Days.Text)
        'intDays_Diff = Val(DateDiff(DateInterval.Day, MemJoin_Date, GenEnd_Date)) + 0
        intDays_Diff = Val(DateDiff(DateInterval.Day, my_Dte_Start, my_Dte_End))

        If MemJoin_Date > GenStart_Date And dblPrem_Amt <> 0 And intDays_Diff <> 0 Then
            dblPrem_Amt_ProRata = Format((dblPrem_Amt / intRisk_Days) * intDays_Diff, "#########0.00")
        End If


        'Commented by Azeez since there exist hashhelper that perform this same function
        'mystr_sql = "insert into table_name(fld1, fld1) values(@val1, @val2)"
        'mystr_sql = "SPGL_TBIL_GRP_POLICY_MEMBERS_INSERT"

        'myole_cmd = New OleDbCommand()
        'myole_cmd.Connection = myole_con
        ''myole_cmd.CommandType = CommandType.Text
        'myole_cmd.CommandType = CommandType.StoredProcedure
        'myole_cmd.CommandText = mystr_sql

        'myole_cmd.Parameters.AddWithValue("@p01", RTrim(my_File_Num))
        'myole_cmd.Parameters.AddWithValue("@p02", Val(0))
        'myole_cmd.Parameters.AddWithValue("@p03", RTrim("G"))
        'myole_cmd.Parameters.AddWithValue("@p04", RTrim(my_Prop_Num))
        'myole_cmd.Parameters.AddWithValue("@p05", RTrim(my_Poly_Num))
        'myole_cmd.Parameters.AddWithValue("@p05A", RTrim(my_Batch_Num))
        'myole_cmd.Parameters.AddWithValue("@p05B", RTrim(my_Staff_Num))
        'myole_cmd.Parameters.AddWithValue("@p06", Val(my_SNo))
        'myole_cmd.Parameters.AddWithValue("@p07", RTrim(my_Gender))
        'myole_cmd.Parameters.AddWithValue("@p08", Format(my_Dte_DOB, "MM/dd/yyyy"))
        'myole_cmd.Parameters.AddWithValue("@p09", Val(my_AGE))
        'myole_cmd.Parameters.AddWithValue("@p10", Format(my_Dte_Start, "MM/dd/yyyy"))
        'myole_cmd.Parameters.AddWithValue("@p11", Format(my_Dte_End, "MM/dd/yyyy"))
        'myole_cmd.Parameters.AddWithValue("@p12", Val(my_Tenor))
        'myole_cmd.Parameters.AddWithValue("@p13", RTrim(my_Designation))
        'myole_cmd.Parameters.AddWithValue("@p14", Left(RTrim(my_Member_Name), 95))
        'myole_cmd.Parameters.AddWithValue("@p14A", CDbl(Trim(my_SA_Factor)))
        'myole_cmd.Parameters.AddWithValue("@p14B", CDbl(Trim(my_Total_Salary)))
        'myole_cmd.Parameters.AddWithValue("@p15", CDbl(Trim(my_Total_SA)))
        'myole_cmd.Parameters.AddWithValue("@p16", RTrim(my_Medical_YN))

        'myole_cmd.Parameters.AddWithValue("@p17", CDbl(dblPrem_Rate))
        'myole_cmd.Parameters.AddWithValue("@p18", CDbl(dblPrem_Rate_Per))
        'myole_cmd.Parameters.AddWithValue("@p19", CDbl(dblPrem_Amt))
        'myole_cmd.Parameters.AddWithValue("@p20", CDbl(dblPrem_Amt_ProRata))
        'myole_cmd.Parameters.AddWithValue("@p21", CDbl(dblLoad_Amt))

        'myole_cmd.Parameters.AddWithValue("@p22", RTrim(Me.txtData_Source_SW.Text))
        'myole_cmd.Parameters.AddWithValue("@p23", RTrim(Me.txtFile_Upload.Text))

        'myole_cmd.Parameters.AddWithValue("@p24", vbNull)
        'myole_cmd.Parameters.AddWithValue("@p25", RTrim("A"))
        'myole_cmd.Parameters.AddWithValue("@p26", RTrim(myUserIDX))
        'myole_cmd.Parameters.AddWithValue("@p27", Format(Now, "MM/dd/yyyy"))


        'Try
        '    mycnt = myole_cmd.ExecuteNonQuery()
        '    If mycnt >= 1 Then
        '        my_intCNT = my_intCNT + 1
        '    Else
        '        strGen_Msg = " * Error!. Row: " & nRow.ToString & " record not save... "
        '    End If
        'Catch ex As Exception
        '    strGen_Msg = " * Error while saving Row: " & nRow.ToString & " record... "

        'End Try

        'myole_cmd.Dispose()
        'myole_cmd = Nothing

MyLoop_888:
        If strGen_Msg <> "" Then
            Me.cboErr_List.Items.Add(strGen_Msg.ToString)
            Me.lblErr_List.Visible = True
            Me.cboErr_List.Visible = True
        End If

        strGen_Msg = ""

        GoTo MyLoop_Start

MyLoop_999:

        Try
            ClientScript.RegisterStartupScript(Me.GetType(), "scrollMSG_JavaScript", "scrollMSG_End(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")", True)
        Catch ex As Exception

        End Try

        If my_intCNT >= 1 Then
            FirstMsg = "Javascript:alert('" & RTrim("File Upload successful - ") & Me.txtFile_Upload.Text & "')"
        Else
            FirstMsg = "Javascript:alert('" & RTrim("File Upload NOT successful - ") & Me.txtFile_Upload.Text & "')"
        End If

MyLoop_999a:
        If lstErrMsgs.Count > 1 Then
            For i = 0 To lstErrMsgs.Count - 1
                cboErr_List.Items.Add(lstErrMsgs.Item(i))
            Next

            Me.lblErr_List.Visible = True
            Me.cboErr_List.Visible = True


            FirstMsg = "Javascript:alert('" & RTrim("File Upload NOT successful - ") & Me.txtFile_Upload.Text & "')"

        Else
            Try
                ClientScript.RegisterStartupScript(Me.GetType(), "scrollMSG_JavaScript", "scrollMSG_End(" & "'" & Me.SB_CONT.ClientID & "'" & ",'" & Me.SB_MSG.ClientID & "'" & ")", True)
            Catch ex As Exception

            End Try

            FirstMsg = "Javascript:alert('" & RTrim("File Upload successful - ") & Me.txtFile_Upload.Text & "')"

        End If
        GoTo MyLoop_End

MyLoop_End:


        myole_cmd = Nothing

        If myole_con.State = ConnectionState.Open Then
            myole_con.Close()
        End If
        myole_con = Nothing


        ''myxls_sheets = Nothing

        'myxls_worksheet = Nothing


        ''myxls_workbook.SaveAs(strSaveFilename, Excel.XlFileFormat.xlWorkbookDefault)
        ''myxls_workbook.Close(SaveChanges:=False)
        ''myxls_workbook.Close(False)

        'myxls_workbook.Close(False)
        'myxls_workbook = Nothing


        ''myxls_app.Workbooks.Close()
        'myxls_app.Quit()
        'myxls_app.Application.Quit()
        'myxls_app = Nothing


        Call Proc_Batch()
        Call Proc_DataBind()

    End Sub

    Private Function Proc_ExcelDoc_New() As String
        Return String.Empty
    End Function


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
        strSQL = strSQL & "SELECT TOP 1 MEMB_TBL.*"
        strSQL = strSQL & " FROM " & strTable & " AS MEMB_TBL"
        strSQL = strSQL & " WHERE BEN_TBL.TBIL_POL_MEMB_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND BEN_TBL.TBIL_POL_MEMB_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If
        'strSQL = strSQL & " AND PT.TBIL_POLY_PROPSAL_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND PT.TBIL_POLY_POLICY_NO = '" & RTrim(strP_ID) & "'"

        strSQL = "SPIL_GET_POLICY_MEMBERS"
        strSQL = "SPGL_GET_POLICY_MEMBERS"

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
            ShowControls()
            strErrMsg = "true"
            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_FILE_NO") & vbNullString, String))
            Me.txtFileNum.Enabled = False

            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_REC_ID") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_PROP_NO") & vbNullString, String))
            Me.txtQuote_Num.Enabled = False

            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_POLY_NO") & vbNullString, String))
            Me.txtPolNum.Enabled = False
            Me.cmdGetPol.Enabled = False

            Me.txtData_Source_SW.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_FILE_UPLOAD_SW") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboData_Source, RTrim(Me.txtData_Source_SW.Text))

            Select Case UCase(Trim(Me.txtData_Source_SW.Text))
                Case "M"
                    'tr_file_upload.Visible = False
                    Me.cmdFile_Upload.Enabled = False
                Case "U"
                    'tr_file_upload.Visible = True
                    Me.cmdFile_Upload.Enabled = False
                Case Else
                    'tr_file_upload.Visible = False
                    Me.cmdFile_Upload.Enabled = False
            End Select

            Me.txtFile_Upload.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_FILE_UPLOAD_NAME") & vbNullString, String))

            Me.txtBatch_Num.Text = RTrim(objOLEDR("TBIL_POL_MEMB_BATCH_NO") & vbNullString)
            'Me.txtBatch_Num.Enabled = False
            Me.cboBatch_Num.Enabled = False

            Me.txtMember_SN.Text = Val(RTrim(CType(objOLEDR("TBIL_POL_MEMB_SNO") & vbNullString, String)))

            Me.txtGender.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_CAT") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboGender, RTrim(Me.txtGender.Text))

            If IsDate(objOLEDR("TBIL_POL_MEMB_BDATE")) Then
                Me.txtMember_DOB.Text = Format(CType(objOLEDR("TBIL_POL_MEMB_BDATE"), DateTime), "dd/MM/yyyy")
            End If
            Me.txtDOB_ANB.Text = Val(objOLEDR("TBIL_POL_MEMB_AGE") & vbNullString)

            If IsDate(objOLEDR("TBIL_POL_MEMB_FROM_DT")) Then
                Me.txtStart_Date.Text = Format(CType(objOLEDR("TBIL_POL_MEMB_FROM_DT"), DateTime), "dd/MM/yyyy")
            End If
            If IsDate(objOLEDR("TBIL_POL_MEMB_TO_DT")) Then
                Me.txtEnd_Date.Text = Format(CType(objOLEDR("TBIL_POL_MEMB_TO_DT"), DateTime), "dd/MM/yyyy")
            End If

            'If IsDate(objOLEDR("TBIL_POL_MEMB_EFF_DT")) Then
            '    Me.txtAdditionDate.Text = Format(CType(objOLEDR("TBIL_POL_MEMB_EFF_DT"), DateTime), "dd/MM/yyyy")
            'End If

            'Azeez: Tenor was commented because in database it is calculated in days while it is needed as yearly(1) to get prem rate

            'Me.txtPrem_Period_Yr.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_TENOR") & vbNullString, String))
            Me.txtDesignation_Name.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_DESIG") & vbNullString, String))
            Me.txtMember_Name.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_NAME") & vbNullString, String))

            If Val(RTrim(CType(objOLEDR("TBIL_POL_MEMB_SA_FACTOR") & vbNullString, String))) <> 0 Then
                Me.txtPrem_SA_Factor.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_SA_FACTOR") & vbNullString, String))
            End If

            Me.txtTotal_Emolument.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_TOT_EMOLUMENT") & vbNullString, String))
            Me.txtSum_Assured.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_TOT_SA") & vbNullString, String))

            Me.txtMedical_YN.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_MEDICAL_YN") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboMedical_YN, RTrim(Me.txtMedical_YN.Text))

            Me.txtPrem_Rate_Code.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_RATE_CODE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Rate_Code, RTrim(Me.txtPrem_Rate_Code.Text))

            Me.txtPrem_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_RATE") & vbNullString, String))
            Me.txtPrem_Rate_Per.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_RATE_PER") & vbNullString, String))
            Me.txtPrem_Amt.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_PREM") & vbNullString, String))
            Me.txtPrem_Amt_Prorata.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_PRO_RATE_PREM") & vbNullString, String))
            Me.txtLoad_amt.Text = RTrim(CType(objOLEDR("TBIL_POL_MEMB_LOAD") & vbNullString, String))

            Me.lblFileNum.Enabled = False
            'Call DisableBox(Me.txtFileNum)
            'Me.chkFileNum.Enabled = False
            Me.txtFileNum.Enabled = False
            Me.txtQuote_Num.Enabled = False
            Me.txtPolNum.Enabled = False

            Me.cmdNew_ASP.Enabled = True
            'Me.cmdDelete_ASP.Enabled = True
            Me.cmdNext.Enabled = True

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
            Me.cmdNext.Enabled = False

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

    Private Sub Proc_LoadRate()

        If Trim(Me.txtFileNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtQuote_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim intC As Integer = 0

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Exit Sub
        End Try


        strREC_ID = Trim(Me.txtFileNum.Text)
        strTable = strTableName

        strSQL = ""
        'Delete record
        '==============================================
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 TBIL_POL_PRM_RT_TAB_FIX, TBIL_POL_PRM_RATE_CD, TBIL_POL_PRM_RATE, TBIL_POL_PRM_RATE_PER"
        strSQL = strSQL & " , TBIL_POL_PRM_RT_FIXED, TBIL_POL_PRM_RT_FIX_PER"
        strSQL = strSQL & " FROM TBIL_GRP_POLICY_PREM_INFO"
        strSQL = strSQL & " WHERE TBIL_POL_PRM_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_PRM_PROP_NO = '" & RTrim(txtQuote_Num.Text) & "'"

        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()
        Dim objOLEDR As OleDbDataReader

        Try
            With objOLECmd2
                .Connection = objOLEConn
                .CommandType = CommandType.Text
                .CommandText = strSQL
            End With
            objOLEDR = objOLECmd2.ExecuteReader()
            If (objOLEDR.Read()) Then
                Me.txtPrem_Rate_TypeNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_TAB_FIX") & vbNullString, String))
                Me.txtPrem_Rate_Code.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RATE_CD") & vbNullString, String))

                Select Case UCase(Trim(Me.txtPrem_Rate_TypeNum.Text))
                    Case "F"
                        Me.txtPrem_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_FIXED") & vbNullString, String))
                        Me.txtPrem_Rate_Per.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_FIX_PER") & vbNullString, String))
                    Case "N"
                        Me.txtPrem_Rate.Text = RTrim("0")
                        Me.txtPrem_Rate_Per.Text = RTrim("0")
                    Case "T"
                        Me.txtPrem_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RATE") & vbNullString, String))
                        Me.txtPrem_Rate_Per.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RATE_PER") & vbNullString, String))
                End Select
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        End Try

        objOLEDR = Nothing

        objOLECmd2.Dispose()
        objOLECmd2 = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging

        'Dim row As GridViewRow = GridView1.Rows(e.NewSelectedIndex)

        GridView1.PageIndex = e.NewPageIndex
        Call Proc_DataBind()
        lblMsg.Text = "Page " & GridView1.PageIndex + 1 & " of " & Me.GridView1.PageCount

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lblPrice As Label = CType(e.Row.FindControl("lblTransAmt"), Label)
            TransAmt = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TBIL_POL_MEMB_PRO_RATE_PREM")))
            TotTransAmt = (TotTransAmt + TransAmt)

        End If
        If (e.Row.RowType = DataControlRowType.Footer) Then
            Dim lblTotal As Label = CType(e.Row.FindControl("lbltxtTotal"), Label)
            lblTotal.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N}", New Object() {TotTransAmt})
        End If

        'format fields
        Dim ea As GridViewRowEventArgs = CType(e, GridViewRowEventArgs)
        If (ea.Row.RowType = DataControlRowType.DataRow) Then
            Dim drv As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TBIL_POL_MEMB_PRO_RATE_PREM"))

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

        'Me.txtGroupNum.Text = row.Cells(3).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))

        'Me.txtNum.Text = row.Cells(4).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtNum.Text))

        strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, Val(RTrim(Me.txtRecNo.Text)))

        lblMsg.Text = "You selected " & Me.txtFileNum.Text & " / " & Me.txtRecNo.Text & "."

    End Sub


    'Private Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting

    'End Sub

    'Private Sub GridView1_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.Sorted
    '    ' Display the sort expression and sort direction.
    '    Me.lblMessage.Text = "Sorting by " & _
    '      GridView1.SortExpression.ToString() & " in " & GridView1.SortDirection.ToString() & " order."

    'End Sub


    Protected Sub cmdPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        Session("optfileid") = Trim(Me.txtFileNum.Text).ToString
        Session("optquotid") = Trim(Me.txtQuote_Num.Text).ToString
        Session("optpolid") = Trim(Me.txtPolNum.Text).ToString

        Dim pvURL As String = ""
        pvURL = "prg_li_grp_poly_prem.aspx?q=x"
        'Response.Redirect(pvURL)

    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Session("optfileid") = Trim(Me.txtFileNum.Text).ToString
        Session("optquotid") = Trim(Me.txtQuote_Num.Text).ToString
        Session("optpolid") = Trim(Me.txtPolNum.Text).ToString
        Session("optbatno") = Trim(Me.txtBatch_Num.Text).ToString

        Dim pvURL As String = ""
        pvURL = "prg_li_grp_poly_prem_calc.aspx?go=add_cov"
        Response.Redirect(pvURL)


    End Sub


    Protected Sub butDeleteMembers_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDeleteMembers_ASP.Click
        Dim mystrURL As String = String.Empty
        Dim formParam As String = String.Empty
        strP_ID = CType(Session("strP_ID"), String)
        Call Proc_DoGet_Record("POLICY")

        formParam = "transtype=A&secnum=1&cn=D&bustype=AD&sectors=1&billdate=" & Format(Now.Date, "dd/MM/yyyy") & "&branch=1501&policyno=" & txtPolNum.Text & "&sa=" & added_SA _
        & "&gprem=" & added_Prorata_Premium & "&batchno=" & txtBatch_Num.Text & "&transdesc=Debit Note for Member(s)" & lblMsg.Text _
        & "&daysused=" & Math.Abs(added_Used_Days) & "&riskdays=" & txtRisk_Days.Text

        '1:      .Transaction_type()
        '2:      .txtSecNum()
        '3:      .CN(CODE)
        '4:      .Business(Type)
        '5:      .secteors()
        '6.      `date
        '7:      .brach(code)
        '8:      .policy(num)
        '9:      .sum(assured)
        '10:     .gross(premium)

        If chkDRNote.Checked = True And blnRet = True Then
            Try
                mystrURL = "window.open('" & "..\\Transaction\\PRG_LI_GRP_PREM_DBCR_NOTE_ENTRY.aspx?" & RTrim(formParam) & "','','left=50,top=50,width=1024,height=650,titlebar=yes,z-lock=yes,address=yes,channelmode=1,fullscreen=no,directories=yes,location=yes,toolbar=yes,menubar=yes,status=yes,scrollbars=1,resizable=yes');"
                FirstMsg = "javascript:" & mystrURL
            Catch ex As Exception
                Me.lblMsg.Text = "<br />Unable to display the Debit Note Screen. <br />Reason: " & ex.Message.ToString

            End Try

        End If
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged

    End Sub

    Protected Sub chkDRNote_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDRNote.CheckedChanged

    End Sub

    Protected Sub cmdGetPol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetPol.Click
        If Trim(Me.txtPolNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPolNum.Text
            Exit Sub
        End If

        strP_ID = RTrim(Me.txtPolNum.Text)
        Session("strP_ID") = strP_ID
        Call Proc_DoGet_Record("POLICY")

    End Sub
    Private Sub ShowControls()
        txtDOB_ANB.Visible = True
        HideRow1.Visible = True
        HideRow2.Visible = True
        HideRow3.Visible = True
        'txtPrem_Rate_Code.Enabled = True
        txtPrem_Rate.Enabled = True
        txtPrem_Rate_Per.Enabled = True
        'txtPrem_Amt.Enabled = True
        lblMember_SN.Visible = True
        lblGender.Visible = True
        lblMember_Name.Visible = True
        lblDesignation_Name.Visible = True
        lblMember_DOB.Visible = True
        txtMember_Name.Visible = True
        txtMember_SN.Visible = True
        cboGender.Visible = True
        txtDesignation_Name.Visible = True
        txtMember_DOB.Visible = True
        lblStart_Date.Visible = True
        lblEnd_Date.Visible = True
        lblPrem_Period_Yr.Visible = True
        lblTotal_Emolument.Visible = True
        'lblMedical_YN.Visible = True
        lblPrem_Rate_X.Visible = True
        txtStart_Date.Visible = True
        txtEnd_Date.Visible = True
        txtPrem_Period_Yr.Visible = True
        txtTotal_Emolument.Visible = True
        'cboMedical_YN.Visible = True
        cboPrem_Rate_Code.Visible = True
        lblPrem_Rate_Code.Visible = True
        txtPrem_Rate_Code.Visible = True
        txtPrem_Rate.Visible = True
        txtPrem_Rate_Per.Visible = True
        txtPrem_Amt.Visible = True
        lblPrem_Rate_Per.Visible = True
        lblPrem_Amt.Visible = True
        lblPrem_Rate.Visible = True
        cmdSave_ASP.Visible = True
        cmdSave_ASP.Enabled = True
        cboPrem_Rate_Code.Enabled = True
    End Sub
    Private Sub HideControls()
        txtDOB_ANB.Visible = False
        HideRow1.Visible = False
        HideRow2.Visible = False
        HideRow3.Visible = False
        txtPrem_Rate_Code.Enabled = False
        txtPrem_Rate.Enabled = False
        txtPrem_Rate_Per.Enabled = False
        txtPrem_Amt.Enabled = False
        lblMember_SN.Visible = False
        lblGender.Visible = False
        lblMember_Name.Visible = False
        lblDesignation_Name.Visible = False
        lblMember_DOB.Visible = False
        txtMember_Name.Visible = False
        txtMember_SN.Visible = False
        cboGender.Visible = False
        txtDesignation_Name.Visible = False
        txtMember_DOB.Visible = False
        lblStart_Date.Visible = False
        lblEnd_Date.Visible = False
        lblPrem_Period_Yr.Visible = False
        lblTotal_Emolument.Visible = False
        'lblMedical_YN.Visible = False
        lblPrem_Rate_X.Visible = False
        txtStart_Date.Visible = False
        txtEnd_Date.Visible = False
        txtPrem_Period_Yr.Visible = False
        txtTotal_Emolument.Visible = False
        'cboMedical_YN.Visible = False
        cboPrem_Rate_Code.Visible = False
        lblPrem_Rate_Code.Visible = False
        txtPrem_Rate_Code.Visible = False
        txtPrem_Rate.Visible = False
        txtPrem_Rate_Per.Visible = False
        txtPrem_Amt.Visible = False
        lblPrem_Rate_Per.Visible = False
        lblPrem_Amt.Visible = False
        lblPrem_Rate.Visible = False
        cmdSave_ASP.Visible = False
        cmdSave_ASP.Enabled = False
        txtSum_Assured.Enabled = False
        cboPrem_Rate_Code.Enabled = False
    End Sub

    Protected Sub txtMember_DOB_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMember_DOB.TextChanged
        If txtMember_DOB.Text <> "" Then
            myarrData = Split(Me.txtMember_DOB.Text, "/")
            If myarrData.Count <> 3 Then
                Me.lblMsg.Text = "Missing or Invalid " & Me.lblMember_DOB.Text & ". Expecting full date in ddmmyyyy format ..."
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
                Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
            Me.txtMember_DOB.Text = RTrim(strMyDte)
            'mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
            mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
            mydte = Format(CDate(mydteX), "MM/dd/yyyy")
            dteDOB = Format(mydte, "MM/dd/yyyy")

            Dte_DOB = dteDOB

            Dte_Current = Now
            lngDOB_ANB = Val(DateDiff("yyyy", Dte_Current, Dte_DOB))
            If lngDOB_ANB < 0 Then
                lngDOB_ANB = lngDOB_ANB * -1
            End If

            If Dte_Current.Month >= Dte_DOB.Month Then
                lngDOB_ANB = lngDOB_ANB
            End If
            ' Me.txtDOB_ANB.Text = Trim(str(lngDOB_ANB))
            Me.txtDOB_ANB.Text = lngDOB_ANB.ToString()
        End If
    End Sub
End Class
