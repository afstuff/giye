Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Configuration

'Imports Microsoft.Office
'Imports Microsoft.Office.Interop.Access
'Imports Microsoft.Office.Interop.Word
'Imports Microsoft.Office.Interop.Excel

Imports Microsoft.Office.Interop

Partial Class Policy_PRG_LI_GRP_QUOT_SCHEDULE
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String
    Protected PageURLs As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strF_ID As String
    Protected strP_ID As String
    Protected strQ_ID As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Protected strRptName As String
    Protected strReportFile As String
    Protected strRptTitle As String
    Protected strRptTitle2 As String

    Protected strPolNum As String
    Protected strBatNum As String
    Protected strID As String
    Protected strFT As String

    Protected strProc_Year As String
    Protected strProc_Mth As String
    Protected strProc_Date As String

    Protected STRMENU_TITLE As String
    Protected BufferStr As String

    Dim strErrMsg As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strTableName = "TBIL_POLICY_DET"
        strTableName = "TBIL_GRP_POLICY_DET"

        '//MonthName(Month(CurrentDate)) + ' ' + ToWords(Year(CurrentDate))
        'left(ToText(Day({SPIL_PS_PRINT;1.TBIL_PS_START_DATE})),2) + ' day of  ' +  'MonthName(Month({SPIL_PS_PRINT;1.TBIL_PS_START_DATE}))

        'Me.txtDocName.Text = "c:\temp\test1.docx"
        'frmDoc.Attributes.Add("src", "http://localhost/docs/test.doc")

        If Not (Page.IsPostBack()) Then
            'Me.BUT_OK.Enabled = False
            Call Proc_Clear_Session()
            PageURLs = ""
            'Me.txtPro_Pol_Num.Text = "PI/2014/1501/E/E003/I/0000002"
            Me.txtPro_Pol_Num.Text = "GQ/2014/1201/G/G001/G/0000001"
            'Call MyMS_Word_App()
            'Call TestExcel()
        Else
            Call Proc_Clear_Session()
        End If

        PageLinks = ""
        PageLinks = "<a href='PRG_GP_PROP_POLICY.aspx' class='a_sub_menu'>Return to Menu</a>&nbsp;"

        Try
            strOPT = Page.Request.QueryString("opt").ToString
            'strOPT options = I001
        Catch
            strOPT = "PDI_ERR"
        End Try


        Select Case UCase(Trim(strOPT))
            Case "QUOT_SCHDLE_DEL"
                STRMENU_TITLE = UCase("+++ Deleted Members Schedule +++ ")
                BufferStr = ""
            Case "QUOT_SCHDLE"
                STRMENU_TITLE = UCase("+++ Quotation Schedule +++ ")
                BufferStr = ""
            Case "POLY_SCHDLE_DEL"
                STRMENU_TITLE = UCase("+++ Deleted Members Schedule +++ ")
                BufferStr = ""

            Case "QUOT_INVOICE"
                STRMENU_TITLE = UCase("+++ Quotation Invoice +++ ")
                BufferStr = ""
            Case Else
                STRMENU_TITLE = UCase("+++ Quotation Schedule +++ ")
                BufferStr = ""
        End Select


    End Sub

    Protected Sub cboBatch_Num_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBatch_Num.SelectedIndexChanged
        Call gnGET_SelectedItem(Me.cboBatch_Num, Me.txtBatch_Num, Me.txtBatch_Name, Me.lblMsg)

    End Sub

    Protected Sub cmdGetPol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetPol.Click
        If Trim(Me.txtPro_Pol_Num.Text) = "" Or Trim(Me.txtPro_Pol_Num.Text) = "*" Or Trim(Me.txtPro_Pol_Num.Text) = "." Then
            Me.lblMsg.Text = "Missing or Invalid policy number. Please enter valid policy number..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        Else
            Me.lblMsg.Text = "Status..."
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_MSOLE", "MyOpen_MS_Word('" & Me.txtDocName.Text & "');", True)
        End If

        Dim xc As Integer = 0
        For xc = 1 To Len(LTrim(RTrim(Me.txtPro_Pol_Num.Text)))
            If Mid(LTrim(RTrim(Me.txtPro_Pol_Num.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtPro_Pol_Num.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblPro_Pol_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                Exit Sub
            End If
        Next

        blnStatusX = False
        Select Case UCase(Trim(strOPT))
            Case "FIL"
                blnStatusX = Proc_GetRecord("FIL", RTrim(Me.txtPro_Pol_Num.Text))
            Case "QUO"
                blnStatusX = Proc_GetRecord("QUO", RTrim(Me.txtPro_Pol_Num.Text))
            Case "QUOT_SCHDLE"
                blnStatusX = Proc_GetRecord("QUO_SCHEDULE", RTrim(Me.txtPro_Pol_Num.Text))
            Case "POLY_SCHDLE"
                blnStatusX = Proc_GetRecord("POLY_SCHEDULE", RTrim(Me.txtPro_Pol_Num.Text))

            Case "QUOT_INVOICE"
                blnStatusX = Proc_GetRecord("QUO_INVOICE", RTrim(Me.txtPro_Pol_Num.Text))
            Case "POL"
                blnStatusX = Proc_GetRecord("POL", RTrim(Me.txtPro_Pol_Num.Text))
            Case Else
                blnStatusX = False
        End Select
        If blnStatusX = False Then
            'Me.BUT_OK.Enabled = False
            Exit Sub
        End If

        Call Proc_Batch()
        Me.BUT_OK.Enabled = True


    End Sub

    Protected Sub BUT_OK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
        'Call MyMS_Word_Open()
        'Call MyMS_Word_App()


        If Trim(Me.txtPro_Pol_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing policy number. Please enter valid policy number..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        Else
            Me.lblMsg.Text = "Status..."
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_MSOLE", "MyOpen_MS_Word('" & Me.txtDocName.Text & "');", True)
        End If


        blnStatusX = False
        Select Case UCase(Trim(strOPT))
            Case "FIL"
                blnStatusX = Proc_GetRecord("FIL", RTrim(Me.txtPro_Pol_Num.Text))
            Case "QUO"
                blnStatusX = Proc_GetRecord("QUO", RTrim(Me.txtPro_Pol_Num.Text))
            Case "QUOT_SCHDLE", "QUOT_SCHDLE_DEL"
                blnStatusX = Proc_GetRecord("QUO_SCHEDULE", RTrim(Me.txtPro_Pol_Num.Text))
            Case "POLY_SCHDLE", "POLY_SCHDLE_DEL"
                blnStatusX = Proc_GetRecord("POLY_SCHEDULE", RTrim(Me.txtPro_Pol_Num.Text))
            Case "QUOT_INVOICE"
                blnStatusX = Proc_GetRecord("QUO_INVOICE", RTrim(Me.txtPro_Pol_Num.Text))
            Case "POL"
                blnStatusX = Proc_GetRecord("POL", RTrim(Me.txtPro_Pol_Num.Text))
            Case Else
                blnStatusX = False
        End Select
        If blnStatusX = False Then
            'Me.BUT_OK.Enabled = False
            Exit Sub
        End If

        If Trim(Me.txtBatch_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblBatch_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If


        strRptName = "PG_ERR"
        Select Case UCase(Trim(strOPT))
            Case "QUOT_SCHDLE", "POLY_SCHDLE"
                strRptName = "GL_RPT_QUOT_SCHEDULE"
            Case "QUOT_SCHDLE_DEL", "POLY_SCHDLE_DEL"
                strRptName = "GL_RPT_QUOT_SCHEDULE_DELETED"
            Case "QUOT_INVOICE"
                strRptName = "GL_RPT_QUOT_INVOICE"
            Case Else
                strRptName = ""
        End Select

        If Trim(strRptName) = "" Then
            Me.lblMsg.Text = "Status: Missing Report name..."
            Exit Sub
        End If
        strReportFile = strRptName


        gnCOMP_NAME = UCase(CType(Session("CL_COMP_NAME"), String))

        'strRptTitle = "POLICY SCHEDULE " & gnGET_RPT_YYYYMM(Me.txtRptDate.Text)
        strRptTitle = "Report Title"
        strRptTitle2 = "Report Title 2"
        Select Case UCase(Trim(strOPT))
            Case "QUOT_SCHDLE"
                strRptTitle = "QUOTATION SCHEDULE "
                strRptTitle2 = "Report Title 2"
            Case "QUOT_SCHDLE_DEL"
                strRptTitle = "DELETED MEMBERS SCHEDULE "
                strRptTitle2 = "Report Title 2"
            Case "POLY_SCHDLE"
                strRptTitle = "COMMENCEMENT SCHEDULE "
                strRptTitle2 = "Report Title 2"
            Case "POLY_SCHDLE_DEL"
                strRptTitle = "DELETED MEMBERS SCHEDULE "
                strRptTitle2 = "Report Title 2"
            Case "QUOT_INVOICE"
                'strRptTitle = "QUOTATION INVOICE "
                strRptTitle = "*** DEMAND NOTE *** "
                strRptTitle2 = "Report Title 2"
            Case Else
                strRptTitle = "*** Missing Report Title ***"
        End Select


        strID = RTrim("Y")
        strPolNum = RTrim(Me.txtPro_Pol_Num.Text)
        strBatNum = RTrim(Me.txtBatch_Num.Text)
        strProc_Date = ""

        Select Case UCase(Trim(strOPT))
            Case "QUOT_SCHDLE"
                If Me.chkExport_Xls.Checked = True Then
                    Me.BUT_OK.Enabled = False
                    'Call Create_Excel_Quot_Schedule()
                    'Call Proc_DoExport_Data_New()
                    Me.BUT_OK.Enabled = True
                    Exit Sub
                End If
            Case "POLY_SCHDLE"
                If Me.chkExport_Xls.Checked = True Then
                    Me.BUT_OK.Enabled = False
                    'Call Create_Excel_Quot_Schedule()
                    'Call Proc_DoExport_Data_New()
                    Me.BUT_OK.Enabled = True
                    Exit Sub
                End If
            Case "QUOT_INVOICE"
                If Me.chkExport_Xls.Checked = True Then
                    'Call Create_Excel_Quot_Invoice()
                    Exit Sub
                End If
            Case Else
        End Select



        Dim strReportParam As String = ""
        'strReportParam = strReportParam & "&rptparams=" & gnCOMP_NAME & "<*>" & RTrim(strRptTitle) & "<*>" & strRptTitle2
        'strReportParam = strReportParam & "&dbparams=" & RTrim(strPolNum)
        'strReportParam = strReportParam & "<*>" & RTrim(strID)

        Call Proc_Clear_Session()

        Session("rptname") = RTrim(strReportFile)

        Dim myArrList_RPT As ArrayList = Nothing
        Dim myArrList_DB As ArrayList = Nothing

        myArrList_RPT = New ArrayList()
        myArrList_DB = New ArrayList()

        myArrList_RPT.Clear()
        myArrList_DB.Clear()


        'myArrList_DB.Insert(0, RTrim("QUO"))

        Select Case UCase(Trim(strOPT))
            Case "QUOT_SCHDLE", "QUOT_SCHDLE_DEL"
                myArrList_RPT.Insert(0, RTrim(gnCOMP_NAME))
                myArrList_RPT.Insert(1, RTrim(strRptTitle))
                myArrList_RPT.Insert(2, RTrim(strRptTitle2))

                myArrList_DB.Insert(0, RTrim("QUO"))
                myArrList_DB.Insert(1, RTrim(strPolNum))
                myArrList_DB.Insert(2, RTrim(Me.txtFileNum.Text))
                myArrList_DB.Insert(3, RTrim(strBatNum))
                myArrList_DB.Insert(4, RTrim("0"))

            Case "POLY_SCHDLE", "POLY_SCHDLE_DEL"
                myArrList_RPT.Insert(0, RTrim(gnCOMP_NAME))
                myArrList_RPT.Insert(1, RTrim(strRptTitle))
                myArrList_RPT.Insert(2, RTrim(strRptTitle2))

                myArrList_DB.Insert(0, RTrim("POLY"))
                myArrList_DB.Insert(1, RTrim(strPolNum))
                myArrList_DB.Insert(2, RTrim(Me.txtFileNum.Text))
                myArrList_DB.Insert(3, RTrim(strBatNum))
                myArrList_DB.Insert(4, RTrim("0"))

            Case "QUOT_INVOICE"
                myArrList_RPT.Insert(0, RTrim(gnCOMP_NAME))
                myArrList_RPT.Insert(1, RTrim(strRptTitle))
                myArrList_RPT.Insert(2, RTrim(gnComp_Addr1))
                myArrList_RPT.Insert(3, RTrim(gnComp_Addr2) & " Tel: " & RTrim(gnComp_TelNum))
                myArrList_RPT.Insert(4, "RC: " & RTrim(gnComp_RegNum))
                myArrList_RPT.Insert(5, RTrim(strRptTitle2))

                myArrList_DB.Insert(0, RTrim(strPolNum))
                myArrList_DB.Insert(1, RTrim(Me.txtFileNum.Text))
                myArrList_DB.Insert(2, RTrim(strBatNum))
                myArrList_DB.Insert(3, RTrim("G"))

            Case Else
                myArrList_DB.Insert(0, RTrim("XYZ"))

        End Select

        Session("rptparams") = myArrList_RPT
        Session("dbparams") = myArrList_DB


        'myArrList_RPT.Clear()
        'myArrList_RPT = Nothing
        'myArrList_DB.Clear()
        'myArrList_DB = Nothing


        Dim mystrURL As String = ""
        Try
            '    'OK
            '    'mystrURL = "window.open('" & "CRViewer.aspx?rptname=" & RTrim(strReportFile) & strReportParam & "','frmDoc','left=50,top=50,width=1024,height=650,titlebar=yes,z-lock=yes,address=yes,channelmode=1,fullscreen=no,directories=yes,location=yes,toolbar=yes,menubar=yes,status=yes,scrollbars=1,resizable=yes');"
            mystrURL = "window.open('" & "../CRViewerN.aspx?rptname=" & RTrim(strReportFile) & strReportParam & "','','left=50,top=10,width=1024,height=600,titlebar=yes,z-lock=yes,address=yes,channelmode=1,fullscreen=0,directories=yes,location=yes,toolbar=yes,menubar=yes,status=yes,scrollbars=1,resizable=yes');"
            '    'FirstMsg = "javascript:window.close();" & mystrURL
            FirstMsg = "javascript:" & mystrURL
        Catch ex As Exception
            Me.lblMsg.Text = "<br />Unable to connect to report viewer. <br />Reason: " & ex.Message.ToString

        End Try

    End Sub

    Private Sub Create_Excel_Quot_Invoice()

    End Sub

    Private Sub Create_Excel_Quot_Schedule()

        PageURLs = ""

        Dim strMyURL As String = ""
        Dim strMyFolder As String = ""
        Dim strMyPath As String = ""
        Dim strMyFile As String = ""

        Dim strMyFileName As String = ""

        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try

        If Trim(myUserIDX) = "" Then
            myUserIDX = "QUOT_SCHEDULE.xls"
        Else
            myUserIDX = "QUOT_SCHEDULE_" & RTrim(myUserIDX) & ".xls"
        End If

        strMyFolder = "Download"
        'strMyURL = CType(ConfigurationSettings.AppSettings("LIFE_DOC_DL_GLIFE"), String).ToString
        'strMyURL = CType(System.Configuration.ConfigurationManager.AppSettings("LIFE_DOC_DL_GLIFE"), String).ToString

        'Response.Write("<br />Path: " & HttpRuntime.AppDomainAppPath)
        'Response.Write("<br />Virtual Path: " & HttpRuntime.AppDomainAppVirtualPath)

        strMyFile = "TestDoc1.xls"
        strMyFile = RTrim(myUserIDX)

        ' virtual path
        strMyURL = HttpRuntime.AppDomainAppVirtualPath
        If Right(RTrim(strMyURL), 1) = "/" Then
            strMyURL = strMyURL & strMyFolder
        Else
            strMyURL = strMyURL & "/" & strMyFolder
        End If
        strMyURL = strMyURL & "/" & RTrim(strMyFile)


        ' physical path
        strMyPath = Server.MapPath(HttpRuntime.AppDomainAppVirtualPath)
        If Right(RTrim(strMyPath), 1) = "\" Then
            strMyPath = strMyPath & strMyFolder
        Else
            strMyPath = strMyPath & "\" & strMyFolder
        End If
        strMyFileName = strMyPath & "\" & strMyFile


        '   CREATE DIRECTORY IF NOT EXISTS
        Try
            If My.Computer.FileSystem.DirectoryExists(strMyPath) = True Then
            Else
                My.Computer.FileSystem.CreateDirectory(strMyPath)
            End If
        Catch ex As Exception
            'Response.Write(ex.Message.ToString)
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message.ToString
            FirstMsg = "Javascript:alert('Unable to create download folder. Please see your system administrator...')"
            Exit Sub

        End Try


        '   REMOVE PREVIOUSLY CREATED FILE
        Try
            If My.Computer.FileSystem.FileExists(strMyFileName) Then
                My.Computer.FileSystem.DeleteFile(strMyFileName)
            End If

        Catch ex As Exception
            'Response.Write(ex.Message.ToString)
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message.ToString
            FirstMsg = "Javascript:alert('Unable to create report. Please close any open document...')"
            Exit Sub

        End Try


        Dim intRows As Long = 0
        Dim intCols As Long = 0
        Dim intR As Long = 0
        Dim intR1 As Long = 0

        Dim pvCNT As Integer = 0

        Dim myText As String
        Dim myDblTot_Prem As Double = 0
        Dim myDblTmp_Amt As Double = 0

        pvCNT = 0

        myDblTot_Prem = 0

        Dim strFT As String = ""
        strFT = "Y"


        Dim xlApp As Microsoft.Office.Interop.Excel.Application
        Dim xlBook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim xlRange As Microsoft.Office.Interop.Excel.Range

        xlApp = CType(CreateObject("Excel.Application"), Microsoft.Office.Interop.Excel.Application)
        xlBook = CType(xlApp.Workbooks.Add, Microsoft.Office.Interop.Excel.Workbook)

        ' The following disable excel visibility.
        xlApp.Visible = False


        xlSheet = CType(xlBook.Worksheets(1), Microsoft.Office.Interop.Excel.Worksheet)

        ' The following statement shows the sheet.
        xlSheet.Application.Visible = False


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


        strREC_ID = RTrim(Me.txtPro_Pol_Num.Text)

        strSQL = ""
        strSQL = "SPGL_GET_QUOTATION_DET"


        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        ''objOLECmd.CommandType = CommandType.Text
        objOLECmd.CommandType = CommandType.StoredProcedure
        objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim("QUO"))
        objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = strREC_ID
        objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 40).Value = RTrim(Me.txtFileNum.Text)
        objOLECmd.Parameters.Add("p04", OleDbType.VarChar, 10).Value = RTrim(Me.txtBatch_Num.Text)
        objOLECmd.Parameters.Add("p05", OleDbType.VarChar, 18).Value = Val(0)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()

        Do While objOLEDR.Read

            ' The following statement puts text in the second row of the sheet.
            If strFT = "Y" Then
                strFT = "N"

                xlSheet.Cells(1, 2) = CType(Session("CL_COMP_NAME"), String).ToString
                xlRange = xlSheet.Cells(1, 2)
                xlRange.Select()
                'xlRange.Font.Bold = True
                'xlRange.Font.Background
                With xlRange.Font
                    .Bold = True
                    '.Color = RGB(5, 5, 5)
                    '.Color = RGB(75, 139, 203)
                    .Color = QBColor(9)
                    '.ThemeColor = QBColor(2)
                    .Size = 11
                    '.Underline = True
                End With

                'objExcelApp.Range("A1:H1").Select
                ''objExcelApp.Range("A1:H1").AutoFit
                'objExcelApp.Range("A1:H1").Font.Bold = True
                ''objExcelApp.Range("A1:H1").Font.Color = "green"

                'xlSheet.Range("A7:G1").Select()
                ''xlSheet.Range("A7:G1").AutoFit()
                'xlSheet.Range("A7:G1").Font.Bold = True
                'xlSheet.Range("A7:G1").Font.Color = QBColor(10)


                xlSheet.Cells(2, 2) = "QUOTATION SCHEDULE"

                xlSheet.Cells(3, 1) = "TO:"
                xlSheet.Cells(3, 2) = RTrim(CType(objOLEDR("T_INSURED_NAME") & vbNullString, String))

                xlSheet.Cells(4, 1) = "G/L Factor:"
                xlSheet.Cells(4, 2) = RTrim(CType(objOLEDR("T_SA_FACTOR") & vbNullString, String))
                xlSheet.Cells(4, 6) = "Retirement Age:"
                xlSheet.Cells(4, 7) = RTrim(CType(objOLEDR("T_RETIREMENT_AGE") & vbNullString, String))

                xlSheet.Cells(5, 1) = "Product:"
                xlSheet.Cells(5, 2) = RTrim(CType(objOLEDR("T_PRODUCT_NAME") & vbNullString, String))

                xlSheet.Cells(6, 1) = "Quotation No:"
                xlSheet.Cells(6, 2) = RTrim(CType(objOLEDR("T_PROPOSAL_NUM") & vbNullString, String))
                xlSheet.Cells(6, 6) = "Effective Date:"
                If IsDate(objOLEDR("T_START_DATE")) Then
                    xlSheet.Cells(6, 7) = Format(CType(objOLEDR("T_START_DATE"), DateTime), "dd-MMM-yyyy")
                Else
                    xlSheet.Cells(6, 7) = ""
                End If

                xlSheet.Cells(7, 1) = "S/No"
                xlSheet.Cells(7, 2) = "Name"
                xlSheet.Cells(7, 3) = "DOB"
                xlSheet.Cells(7, 4) = "Age"
                xlSheet.Cells(7, 5) = "Total Emolument"
                xlSheet.Cells(7, 6) = "Sum Assured"
                xlSheet.Cells(7, 7) = "Premium"

                intR = 7
                intR1 = intR

            End If


            intR = intR + 1
            'intR1 = intR

            myText = ""

            'T_PREM_AMOUNT
            'T_TOT_PRORATA_PREM
            'myDblTmp_Amt = Val(RTrim(CType(objOLEDR("T_PREM_AMOUNT") & vbNullString, String)))
            myDblTmp_Amt = Val(RTrim(CType(objOLEDR("T_TOT_PRORATA_PREM") & vbNullString, String)))
            myDblTot_Prem = myDblTot_Prem + myDblTmp_Amt

            'For intR = intR1 To intR1 + 5

            '    For intCols = 1 To 7

            '        myText = "row: " & intR & " col: " & intCols
            '        xlSheet.Cells(intR, intCols) = myText

            '    Next
            'Next


            myText = RTrim(CType(objOLEDR("T_SERIAL_NUM") & vbNullString, String))
            xlSheet.Cells(intR, 1) = myText

            myText = RTrim(CType(objOLEDR("T_MEMBER_NAME") & vbNullString, String))
            xlSheet.Cells(intR, 2) = myText

            If IsDate(objOLEDR("T_DOB")) Then
                myText = Format(CType(objOLEDR("T_DOB"), DateTime), "dd/MM/yyyy")
            Else
                myText = ""
            End If
            xlSheet.Cells(intR, 3) = myText

            myText = RTrim(CType(objOLEDR("T_AGE") & vbNullString, String))
            xlSheet.Cells(intR, 4) = myText

            myText = RTrim(CType(objOLEDR("T_TOTAL_EMOLUMENT") & vbNullString, String))
            xlSheet.Cells(intR, 5) = myText

            myText = RTrim(CType(objOLEDR("T_TOTAL_SA") & vbNullString, String))
            xlSheet.Cells(intR, 6) = myText

            'T_PREM_AMOUNT
            'T_TOT_PRORATA_PREM
            myText = RTrim(CType(objOLEDR("T_TOT_PRORATA_PREM") & vbNullString, String))
            xlSheet.Cells(intR, 7) = myText

            pvCNT = pvCNT + 1

        Loop

        intR = intR + 1
        myText = "TOTAL:"
        xlSheet.Cells(intR, 4) = myText
        myText = myDblTot_Prem.ToString
        xlSheet.Cells(intR, 7) = myText

        myText = ""



        Try
            ' The following statement saves the sheet to the C:\Test.xls directory.
            xlSheet.SaveAs(strMyFileName)
            'xlSheet.Saved = True

            'xlBook.Close()
            'xlSheet = Nothing
            'xlBook.Application.Quit()

            ' Optionally, you can call xlApp.Quit to close the workbook.
            xlApp.Quit()
            xlApp.Application.Quit()
            xlApp = Nothing

            If Val(pvCNT) >= 1 Then
                PageURLs = "<a href='" & RTrim(strMyURL) & "' target='_blank' class='a_sub_menu'>View Report... </a>"
                'Call WordDocViewer(strMyURL)
            Else
                PageURLs = ""
            End If

            'Response.Write(strMyURL)
            'Response.Redirect(strMyURL)

            'Call Read_PDF_File(strMyURL, "xls")


        Catch ex As Exception

            'Response.Write(strMyURL & "/" & strMyFile)

            ' The following disable excel visibility.
            'xlApp.Visible = True

            ' The following statement shows the sheet.
            'xlSheet.Application.Visible = True

        End Try


    End Sub

    Private Function Proc_GetRecord(ByVal pvCODE As String, ByVal pvFIL_PRO_POL As String) As Boolean

        Dim mybln As Boolean
        mybln = False

        strF_ID = ""
        'strF_ID = RTrim(Me.txtFileNum.Text)
        'strF_ID = RTrim(Me.txtQuote_Num.Text)


        Dim strGET_WHAT As String = "GET_GL_POLICY_BY_FILE_NO"
        Select Case UCase(Trim(pvCODE))
            Case "FIL"
                strF_ID = RTrim(Me.txtPro_Pol_Num.Text)
                strGET_WHAT = "GET_GL_POLICY_BY_FILE_NO"
            Case "QUO", "QUO_SCHEDULE", "QUO_INVOICE", "POLY_SCHEDULE"
                strF_ID = RTrim(Me.txtPro_Pol_Num.Text)
                strGET_WHAT = "GET_GL_POLICY_BY_QUOTATION_NO"
            Case "POL"
                strF_ID = RTrim(Me.txtPro_Pol_Num.Text)
                strGET_WHAT = "GET_GL_POLICY_BY_POLICY_NO"

            Case Else
                strGET_WHAT = ""
        End Select

        If strGET_WHAT = "" Then
            mybln = False
            Proc_GetRecord = mybln
            Me.lblMsg.Text = "Missing or Invalid parameter code. Required a valid parameter code..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

            Return mybln
            Exit Function
        End If

        Dim oAL As ArrayList
        oAL = MOD_GEN.gnGET_RECORD(strGET_WHAT, RTrim(strF_ID), RTrim(""), RTrim(""))
        If oAL.Item(0) = "TRUE" Then
            '    'Retrieve the record
            '    Response.Write("<br/>Status: " & oAL.Item(0))
            '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))

            Me.txtFileNum.Text = oAL.Item(2)
            Me.txtQuote_Num.Text = oAL.Item(3)
            Me.txtPol_Num.Text = oAL.Item(4)

            Me.txtProductClass.Text = oAL.Item(5)
            Me.txtProduct_Num.Text = oAL.Item(6)
            'Me.txtPlan_Num.Text = oAL.Item(7)
            'Me.txtCover_Num.Text = oAL.Item(8)
            Me.txtPrem_Rate_Code.Text = oAL.Item(14)

            Me.txtAssured_Name.Text = oAL.Item(26)
            Me.txtProduct_Name.Text = oAL.Item(27)
            oAL = Nothing
            mybln = True
        Else
            '    'Destroy i.e remove the array list object from memory
            '    Response.Write("<br/>Status: " & oAL.Item(0))
            Me.lblMsg.Text = "Status: " & oAL.Item(1)
            oAL = Nothing
            mybln = False
        End If

        oAL = Nothing

        Proc_GetRecord = mybln
        Return mybln

    End Function

    Private Sub Read_PDF_File(ByVal StrPath As String, ByVal strType As String)

        Dim Path As String = StrPath

        Dim client As New WebClient()
        Dim buffer As [Byte]() = client.DownloadData(Path)

        Dim strContentType As String = ""
        Select Case Trim(strType)
            Case "xls"
                Response.ContentType = "application/vnd.ms-excel"
                'Response.AddHeader("content-disposition", "attachment;filename=Tr.xls")
            Case "word"
                Response.ContentType = "application/vnd.ms-word"
                'Response.AddHeader("content-disposition", "attachment;filename=Tr.doc")
            Case "pdf"
                Response.ContentType = "application/pdf"
                'Response.AddHeader("content-disposition", "attachment;filename=Tr.pdf")
        End Select

        If buffer IsNot Nothing Then
            Response.AddHeader("content-length", buffer.Length.ToString())
            Response.BinaryWrite(buffer)
        End If

    End Sub


    Private Sub WordDocViewer(ByVal fileName As String)
        Try
            System.Diagnostics.Process.Start(fileName)
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            Call gnProc_Populate_Box("GL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If

    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        strStatus = ""

        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtFileNum.Text = ""
                'Me.txtQuote_Num.Text = ""
                'Me.txtPolNum.Text = ""
                'Me.txtSearch.Value = ""
            Else
                Me.txtFileNum.Text = Me.cboSearch.SelectedItem.Value
                If LTrim(RTrim(Me.txtFileNum.Text)) <> "" Then
                    strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
                    If Trim(strStatus) = "true" Then
                        Call Proc_Batch()
                    End If
                End If
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try


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


    Private Sub Proc_Clear_Session()

        Dim strSess As String = ""
        Dim intC As Integer = 0
        Dim intCX As Integer = 0
        Dim MyArray(15) As String

        intC = 0
        intCX = 0

        Try

            For intCX = 0 To Session.Count - 1

                strSess = Session.Keys(intCX).ToString

                If UCase(strSess) = UCase("rptname") Or _
                  UCase(strSess) = UCase("rptparams") Or _
                  UCase(strSess) = UCase("dbparams") Or _
                  UCase(strSess) = UCase("xxx") Then
                    intC = intC + 1
                    MyArray(intC) = strSess
                    'Response.Write("<br />" & "Item " & intC & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
                Else

                End If

            Next

            For intCX = 1 To intC

                strSess = MyArray(intCX).ToString

                'Response.Write("<br />" & "Removing session Item " & intCX & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
                Session.Remove(strSess.ToString)
                'Session.Contents.Remove(strSess)

            Next

        Catch ex As Exception

        End Try

    End Sub

    '   When you run the application first time and click export you might receive the following error
    '       Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server
    '   To avoid the error you will need to add this event which ensures that the GridView is Rendered before exporting.

    '//override the VerifyRenderingInServerForm() to verify the control
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered 
    End Sub

    ' Override the Render method to ensure that this control
    ' is nested in an HtmlForm server control, between a <form runat=server>
    ' opening tag and a </form> closing tag.
    'Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)

    '    ' Ensure that the control is nested in a server form.
    '    If Not (Page Is Nothing) Then
    '        Page.VerifyRenderingInServerForm(Me)
    '    End If

    '    MyBase.Render(writer)

    'End Sub

    Protected Sub Proc_DoExport_Data_New()

        'If Me.txtFileNum.Text = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        'If Me.txtQuote_Num.Text = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        'If Me.txtPolNum.Text = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblPolNum.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        'If Me.txtBatch_Num.Text = "" Then
        '    'Me.txtFile_Upload.Text = ""
        '    'Me.cmdFile_Upload.Enabled = False
        '    Me.lblMsg.Text = "Missing " & Me.lblBatch_Num.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If



        ' Create a DataTable and filled it with information about
        ' files in the current folder.

        'Note: The DirectoryInfo and FileInfo objects is from the .NET framework,
        ' in the System.IO namespace

        'Dim dt As New DataTable()
        'dt.Columns.Add("FileName", GetType(System.String))
        'dt.Columns.Add("Size", GetType(System.Int32))
        'dt.Columns.Add("Date", GetType(System.String))

        'Dim dr As DataRow

        'Dim dir As New DirectoryInfo(Path.GetDirectoryName(Server.MapPath(Request.Path)))
        'For Each fil As FileInfo In dir.GetFiles()
        '    dr = dt.NewRow

        '    dr(0) = fil.Name
        '    dr(1) = fil.Length
        '    'dr(2) = fil.CreationTime
        '    dr(2) = Format(fil.CreationTime, "dd-MMM-yyyy hh:mm:ss tt")

        '    dt.Rows.Add(dr)
        '    'Response.Write("<br/>File Info: Name = " & fil.Name & " Size = " & fil.Length)
        'Next


        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try

        If Trim(myUserIDX) = "" Then
            'myUserIDX = "GL_QUOT_SCHEDULE.xls"
            myUserIDX = "GL_QUOT_SCHEDULE"
        Else
            'myUserIDX = "GL_QUOT_SCHEDULE_" & RTrim(myUserIDX) & ".xls"
            myUserIDX = "GL_QUOT_SCHEDULE_" & RTrim(myUserIDX)
        End If

        strReportFile = "attachment;filename=" & myUserIDX & "_" & Format(Now, "yyyy-MM-dd").ToString


        Dim strConnString As String = CType(Session("connstr"), String)
        Dim myconn As New OleDbConnection(strConnString)

        Try
            myconn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            myconn = Nothing
            Exit Sub
        End Try


        'Me.cmdPrint_ASP.Enabled = False
        Me.butExport_Data.Enabled = False

        Dim mydata_tbl As New DataTable

        mydata_tbl.Columns.Add("T_SERIAL_NO", GetType(System.String))
        mydata_tbl.Columns.Add("T_PCN", GetType(System.String))
        mydata_tbl.Columns.Add("T_MEMBER_NAME", GetType(System.String))
        mydata_tbl.Columns.Add("T_DOB", GetType(System.String))
        mydata_tbl.Columns.Add("T_AGE", GetType(System.String))
        'mydata_tbl.Columns.Add("T_GENDER", GetType(System.String))
        'mydata_tbl.Columns.Add("T_DESIG", GetType(System.String))
        'mydata_tbl.Columns.Add("T_START_DATE", GetType(System.String))
        'mydata_tbl.Columns.Add("T_END_DATE", GetType(System.String))
        'mydata_tbl.Columns.Add("T_TENOR", GetType(System.String))
        'mydata_tbl.Columns.Add("T_FACTOR", GetType(System.String))
        ''mydata_tbl.Columns.Add("T_BASIC_SAL", GetType(System.String))
        ''mydata_tbl.Columns.Add("T_HOUSE_ALLOW", GetType(System.String))
        ''mydata_tbl.Columns.Add("T_TRANSPORT_ALLOW", GetType(System.String))
        ''mydata_tbl.Columns.Add("T_OTHER_ALLOW", GetType(System.String))
        mydata_tbl.Columns.Add("T_TOTAL_EMOLUMENT", GetType(System.String))
        mydata_tbl.Columns.Add("T_SUM_ASSURED", GetType(System.String))
        'mydata_tbl.Columns.Add("T_ADD_COVER_SA", GetType(System.String))
        mydata_tbl.Columns.Add("T_PREMIUM", GetType(System.String))
        mydata_tbl.Columns.Add("T_SYSTEM_NO", GetType(System.String))


        strTable = strTableName
        strTable = "TBIL_GRP_POLICY_MEMBERS"

        strSQL = ""
        strSQL = strSQL & " SELECT TBIL_POL_MEMB_SNO AS T_SERIAL_NO, TBIL_POL_MEMB_STAFF_NO as T_PCN, TBIL_POL_MEMB_NAME AS T_MEMBER_NAME"
        strSQL = strSQL & " ,TBIL_POL_MEMB_BDATE AS T_DOB"
        strSQL = strSQL & " ,TBIL_POL_MEMB_AGE AS T_AGE"
        'strSQL = strSQL & " ,TBIL_POL_MEMB_CAT AS T_GENDER"
        'strSQL = strSQL & " ,TBIL_POL_MEMB_DESIG AS T_DESIG, TBIL_POL_MEMB_FROM_DT AS T_START_DATE, TBIL_POL_MEMB_TO_DT AS T_END_DATE"
        'strSQL = strSQL & " ,TBIL_POL_MEMB_TENOR AS T_TENOR, TBIL_POL_MEMB_SA_FACTOR AS T_FACTOR"
        'strSQL = strSQL & " ,0 AS T_BASIC_SAL, 0 AS T_HOUSE_ALLOW, 0 AS T_TRANSPORT_ALLOW, 0 AS T_OTHER_ALLOW"
        strSQL = strSQL & " ,TBIL_POL_MEMB_TOT_EMOLUMENT AS T_TOTAL_EMOLUMENT, TBIL_POL_MEMB_TOT_SA AS T_SUM_ASSURED"
        'strSQL = strSQL & " ,0 AS T_ADD_COVER_SA"
        strSQL = strSQL & " ,ISNULL(TBIL_POL_MEMB_PRO_RATE_PREM,0) + ISNULL(TBIL_POL_MEMB_LOAD,0) AS T_PREMIUM"
        'strSQL = strSQL & " ,TBIL_POL_MEMB_REC_ID AS T_SYSTEM_NO"

        strSQL = strSQL & " from " & strTable
        strSQL = strSQL & " where TBIL_POL_MEMB_FILE_NO = '" & RTrim(Me.txtFileNum.Text) & "'"
        strSQL = strSQL & " and TBIL_POL_MEMB_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
        'strSQL = strSQL & " and TBIL_POL_MEMB_POLY_NO = '" & RTrim(Me.txtPolNum.Text) & "'"
        strSQL = strSQL & " and TBIL_POL_MEMB_BATCH_NO = '" & RTrim(Me.txtBatch_Num.Text) & "'"
        'strSQL = strSQL & " and TBIL_POL_MEMB_STATUS in('Q')"
        strSQL = strSQL & " ORDER BY TBIL_POL_MEMB_PROP_NO, TBIL_POL_MEMB_BATCH_NO, TBIL_POL_MEMB_SNO"

        Dim mydata_row As DataRow
        Dim pvFT As String = "Y"
        Dim pvCNT As Integer = 0

        pvFT = "Y"
        pvCNT = 0

        Dim mycmd As OleDbCommand
        mycmd = New OleDbCommand(strSQL, myconn)

        Dim mydata_reader As OleDbDataReader
        mydata_reader = mycmd.ExecuteReader()

        Do While mydata_reader.Read

            If UCase(Trim(pvFT)) = "Y" Then
                pvFT = "N"
            End If

            pvCNT = pvCNT + 1

            mydata_row = mydata_tbl.NewRow

            mydata_row(0) = RTrim(CType(mydata_reader("T_SERIAL_NO") & vbNullString, String))
            mydata_row(1) = RTrim(CType(mydata_reader("T_PCN") & vbNullString, String))
            mydata_row(2) = RTrim(CType(mydata_reader("T_MEMBER_NAME") & vbNullString, String))
            If IsDate(mydata_reader("T_DOB")) Then
                mydata_row(3) = Format(mydata_reader("T_DOB"), "dd/MM/yyyy").ToString
            Else
                mydata_row(3) = RTrim("")
            End If
            mydata_row(4) = RTrim(CType(mydata_reader("T_AGE") & vbNullString, String))
            'mydata_row(5) = RTrim(CType(mydata_reader("T_GENDER") & vbNullString, String))
            'mydata_row(6) = RTrim(CType(mydata_reader("T_DESIG") & vbNullString, String))
            'If IsDate(mydata_reader("T_START_DATE")) Then
            '    mydata_row(7) = Format(mydata_reader("T_START_DATE"), "dd/MM/yyyy").ToString
            'Else
            '    mydata_row(7) = RTrim("")
            'End If
            'If IsDate(mydata_reader("T_END_DATE")) Then
            '    mydata_row(8) = Format(mydata_reader("T_END_DATE"), "dd/MM/yyyy").ToString
            'Else
            '    mydata_row(8) = RTrim("")
            'End If
            'mydata_row(9) = RTrim(CType(mydata_reader("T_TENOR") & vbNullString, String))
            'mydata_row(10) = RTrim(CType(mydata_reader("T_FACTOR") & vbNullString, String))
            ''mydata_row(11) = RTrim(CType(mydata_reader("T_BASIC_SAL") & vbNullString, String))
            ''mydata_row(12) = RTrim(CType(mydata_reader("T_HOUSE_ALLOW") & vbNullString, String))
            ''mydata_row(13) = RTrim(CType(mydata_reader("T_TRANSPORT_ALLOW") & vbNullString, String))
            ''mydata_row(14) = RTrim(CType(mydata_reader("T_OTHER_ALLOW") & vbNullString, String))
            mydata_row(5) = RTrim(CType(mydata_reader("T_TOTAL_EMOLUMENT") & vbNullString, String))
            mydata_row(6) = RTrim(CType(mydata_reader("T_SUM_ASSURED") & vbNullString, String))
            'mydata_row(17) = RTrim("0")
            mydata_row(7) = RTrim(CType(mydata_reader("T_PREMIUM") & vbNullString, String))
            mydata_row(8) = RTrim(CType(mydata_reader("T_SYSTEM_NO") & vbNullString, String))

            mydata_tbl.Rows.Add(mydata_row)

        Loop

        '   To Export all pages
        GridViewN.AllowPaging = False

        '   CREATE DATA
        Me.GridViewN.DataSource = mydata_tbl
        Me.GridViewN.DataBind()

        mydata_reader = Nothing

        mycmd.Dispose()
        mycmd = Nothing

        If myconn.State = ConnectionState.Open Then
            myconn.Close()
        End If
        myconn.Dispose()
        myconn = Nothing



        Me.lblResult.Text = "Total Row: " & pvCNT.ToString & " - " & Me.GridViewN.Rows.Count.ToString
        'If pvCNT >= 1 Then
        '    Response.Write("<br/>Record found...")
        '    Exit Sub
        'Else
        '    Response.Write("<br/>Record not found...")
        '    Exit Sub
        'End If


        If Me.optPDF.Checked = True Then
            Call Proc_DoExport_ToPdf(Me.GridViewN)
            'Me.cmdPrint_ASP.Enabled = True
            'Me.butExport_Data.Enabled = True
            Exit Sub
        End If


        ' strConType = "application/pdf"
        ' strConType = "application/doc"
        ' strConType = "application/vnd.ms-excel"

        ' using System.Net
        'Dim client As New System.Net.WebClient()
        'Dim myPath As String = Server.MapPath("~")

        'Dim strbuffer As [Byte]() = client.DownloadData(myPath)

        Response.Clear()
        Response.Buffer = True
        Response.Charset = ""
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)


        If Me.optPDF.Checked = True Then

            'Response.ContentType = "application/pdf"
            'Response.AddHeader("content-length", strbuffer.Length.ToString())
            'Response.BinaryWrite(strbuffer)

            'If strbuffer IsNot Nothing Then
            '    Response.ContentType = "application/pdf"
            '    Response.AddHeader("content-length", strbuffer.Length.ToString())
            '    Response.BinaryWrite(strbuffer)
            'End If

            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/pdf"
            'Response.AddHeader("content-disposition", "attachment;filename=GL_MembersList_" & Format(Now, "yyyy-MM-dd").ToString & ".pdf")
            Response.AddHeader("content-disposition", strReportFile & ".pdf")
        ElseIf Me.optDOC.Checked = True Then
            Response.ContentType = "application/doc"
            'Response.AddHeader("content-disposition", "attachment;filename=GL_MembersList_" & Format(Now, "yyyy-MM-dd").ToString & ".doc")
            Response.AddHeader("content-disposition", strReportFile & ".doc")
        ElseIf Me.optRTF.Checked = True Then
            Response.ContentType = "application/rtf"
            'Response.AddHeader("content-disposition", "attachment;filename=GL_MembersList_" & Format(Now, "yyyy-MM-dd").ToString & ".rtf")
            Response.AddHeader("content-disposition", strReportFile & ".rtf")
        Else
            Response.ContentType = "application/vnd.ms-excel"
            'Response.AddHeader("content-disposition", "attachment;filename=GL_MembersList_" & Format(Now, "yyyy-MM-dd").ToString & ".xls")
            Response.AddHeader("content-disposition", strReportFile & ".xls")
        End If

        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)


            'GridViewN.HeaderRow.BackColor = Color.White
            'For Each cell As TableCell In GridViewN.HeaderRow.Cells
            '    cell.BackColor = GridViewN.HeaderStyle.BackColor
            'Next
            For Each row As GridViewRow In GridViewN.Rows
                row.BackColor = Drawing.Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = GridViewN.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = GridViewN.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            GridViewN.RenderControl(hw)

            'style to format numbers to string
            Dim style As String = "<style> .textmode { mso-number-format:\@; } </style>"
            Response.Write(style)


            If Me.optPDF.Checked = True Then
                'Dim objSR As New StringReader(sw.ToString())
                'Dim objPDF As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 100.0F, 100.0F, 100.0F, 100.0F)
                'Dim objHW As New iTextSharp.text.html.simpleparser.HTMLWorker(objPDF)

                'iTextSharp.text.pdf.PdfWriter.GetInstance(objPDF, Response.OutputStream)
                ''PdfWriter.GetInstance(objPDF, Response.OutputStream)
                'objPDF.Open()
                'objHW.Parse(objSR)
                'objPDF.Close()

                'Response.Write(objPDF)
                ''Response.Flush()
                'Response.End()

            Else
                Response.Output.Write(sw.ToString())
                Response.Flush()
                Response.[End]()
            End If


        End Using

        'Me.cmdPrint_ASP.Enabled = True
        'Me.butExport_Data.Enabled = True

        'FirstMsg = "Javascript:alert('" & RTrim("Data export successful...") & "')"

    End Sub


    'Method for Export to PDF
    Protected Sub Proc_DoExport_ToPdf(ByVal pvGridView As System.Web.UI.WebControls.GridView)

        ''Call Proc_DoSaveCheckedStates()

        ''disable paging to export all data and make sure to bind griddata before begin
        ''pvGridView.AllowPaging = False
        ''Call Proc_BindGrid(pvGridView)

        'Dim myUserIDX As String = ""
        'Try
        '    myUserIDX = CType(Session("MyUserIDX"), String)
        'Catch ex As Exception
        '    myUserIDX = ""
        'End Try

        'If Trim(myUserIDX) = "" Then
        '    myUserIDX = "GL_QUOT_SCHEDULE.xls"
        '    myUserIDX = "GL_QUOT_SCHEDULE"
        'Else
        '    myUserIDX = "GL_QUOT_SCHEDULE_" & RTrim(myUserIDX) & ".xls"
        '    myUserIDX = "GL_QUOT_SCHEDULE_" & RTrim(myUserIDX)
        'End If

        'Dim fileName As String = "GL_MembersList_" & Format(Now, "yyyy-MM-dd").ToString
        ''fileName = "filename=" & myUserIDX & "_" & Format(Now, "yyyy-MM-dd").ToString
        'fileName = myUserIDX & "_" & Format(Now, "yyyy-MM-dd").ToString

        'Response.ContentType = "application/pdf"
        'Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName & ".pdf"))
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'Dim objSW As New StringWriter()
        'Dim objTW As New HtmlTextWriter(objSW)

        ' '' hide the checkbox
        ''pvGridView.Columns(0).Visible = False

        ''If ViewState("SELECTED_ROWS") IsNot Nothing Then
        ''    Dim objSelectedRowsAL As ArrayList = CType(ViewState("SELECTED_ROWS"), ArrayList)
        ''    For j As Integer = 0 To pvGridView.Rows.Count - 1
        ''        Dim row As GridViewRow = pvGridView.Rows(j)
        ''        Dim rowIndex As Integer = Convert.ToInt32(pvGridView.DataKeys(row.RowIndex).Value)
        ''        If Not objSelectedRowsAL.Contains(rowIndex) Then
        ''            'make invisible because row is not checked
        ''            row.Visible = False
        ''        End If
        ''    Next j
        ''End If

        'pvGridView.RenderControl(objTW)

        'Dim objSR As New StringReader(objSW.ToString())
        'Dim objPDF As New iTextSharp.text.Document(iTextSharp.text.PageSize._11X17, 5.0F, 5.0F, 5.0F, 5.0F)
        ''Dim objPDF As New iTextSharp.text.Document(iTextSharp.text.PageSize.TABLOID, 5.0F, 5.0F, 5.0F, 5.0F)
        'Dim objHW As New iTextSharp.text.html.simpleparser.HTMLWorker(objPDF)

        'iTextSharp.text.pdf.PdfWriter.GetInstance(objPDF, Response.OutputStream)
        ''PdfWriter.GetInstance(objPDF, Response.OutputStream)
        'objPDF.Open()
        'objHW.Parse(objSR)
        'objPDF.Close()

        'Response.Write(objPDF)
        'Response.End()

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

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 PT.TBIL_POLY_FILE_NO, PT.TBIL_POLY_PROPSAL_NO, PT.TBIL_POLY_POLICY_NO"
        strSQL = strSQL & ", PT.TBIL_POLY_PRDCT_CD"
        strSQL = strSQL & ", RTRIM(ISNULL(INSRD.TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(INSRD.TBIL_INSRD_FIRSTNAME,'')) AS T_INSURED_NAME"
        strSQL = strSQL & ", PROD.TBIL_PRDCT_DTL_DESC"
        strSQL = strSQL & " FROM " & strTable & " AS PT"
        strSQL = strSQL & " LEFT JOIN TBIL_INS_DETAIL AS INSRD"
        strSQL = strSQL & " ON INSRD.TBIL_INSRD_CODE = PT.TBIL_POLY_ASSRD_CD"
        strSQL = strSQL & " AND INSRD.TBIL_INSRD_ID = '001'"
        strSQL = strSQL & " LEFT JOIN TBIL_PRODUCT_DETL AS PROD"
        strSQL = strSQL & " ON PROD.TBIL_PRDCT_DTL_CODE = PT.TBIL_POLY_PRDCT_CD"
        strSQL = strSQL & " AND PROD.TBIL_PRDCT_DTL_MDLE IN('GRP','G')"
        strSQL = strSQL & " WHERE PT.TBIL_POLY_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND PT.TBIL_POLY_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If
        'strSQL = strSQL & " AND PT.TBIL_POLY_PROPSAL_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND PT.TBIL_POLY_POLICY_NO = '" & RTrim(strP_ID) & "'"


        'strSQL = "SPIL_GET_POLICY_DET"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        ''objOLECmd.CommandType = CommandType.Text
        'objOLECmd.CommandType = CommandType.StoredProcedure
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        'objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = strREC_ID
        'objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strErrMsg = "true"

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_FILE_NO") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            'Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POLY_REC_ID") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_PROPSAL_NO") & vbNullString, String))
            Me.txtPro_Pol_Num.Text = RTrim(Me.txtQuote_Num.Text)

            'Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_POLICY_NO") & vbNullString, String))
            Me.txtPol_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_POLICY_NO") & vbNullString, String))
            Me.txtAssured_Name.Text = RTrim(CType(objOLEDR("T_INSURED_NAME") & vbNullString, String))


            'Me.txtProductClass.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_CAT") & vbNullString, String))
            Me.txtProduct_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_PRDCT_CD") & vbNullString, String))
            Me.txtProduct_Name.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_DESC") & vbNullString, String))

            strOPT = "2"
            Me.lblMsg.Text = "Status: Policy No: " & Me.txtPro_Pol_Num.Text

        Else

            Me.txtFileNum.Text = RTrim("")
            Me.txtPro_Pol_Num.Text = RTrim("")

            Me.lblMsg.Text = "Status: New Entry..."
            Me.lblMsg.Text = "Sorry!. Unable to get record ..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

            strOPT = "1"

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


    Protected Sub Proc_DoExport_Data_New(ByVal sender As Object, ByVal e As System.EventArgs) Handles butExport_Data.Click

    End Sub

    Protected Sub chkExport_Xls_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkExport_Xls.CheckedChanged

    End Sub
End Class
