Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Net

'Imports Microsoft.Office
'Imports Microsoft.Office.Interop.Access
'Imports Microsoft.Office.Interop.Word
'Imports Microsoft.Office.Interop.Excel

Imports Microsoft.Office.Interop

Partial Class Reports_PRG_LI_GRP_DN_RPT
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
    Protected myTType As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Protected strRptName As String
    Protected strReportFile As String
    Protected strRptTitle As String
    Protected strRptTitle2 As String

    Protected strTransNum As String
    Protected strID As String
    Protected strFT As String

    Protected strProc_Year As String
    Protected strProc_Mth As String
    Protected strProc_Date As String

    Protected STRMENU_TITLE As String
    Protected BufferStr As String

    Dim strErrMsg As String
    Dim rParams As String() = {"nw", "nw", "new", "nw", "nw", "new", "new", "new", "nw", "nw"}



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strTableName = "TBIL_GRP_POLICY_DNOTE_DETAILS"

        If Not (Page.IsPostBack()) Then
            'Me.BUT_OK.Enabled = False
            Call Proc_Clear_Session()
            PageURLs = ""
            'Me.txtTrans_Num.Text = "D00000001"
            'Call MyMS_Word_App()
            'Call TestExcel()
        Else
            Call Proc_Clear_Session()
        End If

        PageLinks = ""
        PageLinks = "<a href='../MENU_GL.aspx?menu=GL_UND' class='a_sub_menu'>Return to Menu</a>&nbsp;"


        Try
            myTType = Request.QueryString("TTYPE")
        Catch ex As Exception
            myTType = "DC"
        Finally

        End Try


        Select Case UCase(Trim(myTType))
            Case "DN", "DNNOCOM"
                STRMENU_TITLE = UCase("+++ Debit Note Print +++ ")
                BufferStr = ""
                lblTrans_Num.Text = "Enter Debit Note"
            Case "CN", "CNNOCOM"
                STRMENU_TITLE = UCase("+++ Credit Note Print +++ ")
                BufferStr = ""
                lblTrans_Num.Text = "Enter Credit Note"
            Case Else
                STRMENU_TITLE = UCase("+++ Debit Note / Credit Note Print +++ ")
                BufferStr = ""
        End Select


    End Sub

    Protected Sub cmdGetRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetRecord.Click
        If RTrim(Me.txtTrans_Num.Text) = "" Then
            Exit Sub
        End If

        If Trim(Me.txtTrans_Num.Text) = "" Or Trim(Me.txtTrans_Num.Text) = "*" Or Trim(Me.txtTrans_Num.Text) = "." Then
            Me.lblMsg.Text = "Missing or Invalid Ref. Number. Please enter valid Ref No..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            'ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        Else
            Me.lblMsg.Text = "Status..."
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_MSOLE", "MyOpen_MS_Word('" & Me.txtDocName.Text & "');", True)
        End If

        Dim xc As Integer = 0
        For xc = 1 To Len(LTrim(RTrim(Me.txtTrans_Num.Text)))
            If Mid(LTrim(RTrim(Me.txtTrans_Num.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtTrans_Num.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.txtTrans_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                'ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & Me.lblMsg.Text & "');", True)
                Exit Sub
            End If
        Next

        blnStatusX = False
        Select Case UCase(Trim(myTType))
            Case "DN", "DNNOCOM"
                blnStatusX = Proc_DoOpenRecord(myTType, RTrim(Me.txtTrans_Num.Text), RTrim("0"))
            Case "CN", "CNNOCOM"
                blnStatusX = Proc_DoOpenRecord(myTType, RTrim(Me.txtTrans_Num.Text), RTrim("0"))
            Case Else
                blnStatusX = False
        End Select

        If blnStatusX = False Then
            'Me.BUT_OK.Enabled = False
            Me.lblMsg.Text = "Unable to get record..."
            Exit Sub
        End If

    End Sub

    Protected Sub BUT_OK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_OK.Click

        Me.lblMsg.Text = "Status:"

        If Trim(Me.txtTrans_Num.Text) = "" Or Trim(Me.txtTrans_Num.Text) = "*" Or Trim(Me.txtTrans_Num.Text) = "." Then
            Me.lblMsg.Text = "Missing or Invalid Ref. Number. Please enter valid Ref No..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            'ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        Else
            Me.lblMsg.Text = "Status..."
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_MSOLE", "MyOpen_MS_Word('" & Me.txtDocName.Text & "');", True)
            'ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & Me.lblMsg.Text & "');", True)
        End If

        Dim xc As Integer = 0
        For xc = 1 To Len(LTrim(RTrim(Me.txtTrans_Num.Text)))
            If Mid(LTrim(RTrim(Me.txtTrans_Num.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtTrans_Num.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.txtTrans_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                'ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & Me.lblMsg.Text & "');", True)
                Exit Sub
            End If
        Next

        blnStatusX = False
        Select Case UCase(Trim(myTType))
            Case "DN", "DNNOCOM"
                blnStatusX = Proc_DoOpenRecord(myTType, RTrim(Me.txtTrans_Num.Text), RTrim("0"))
            Case "CN", "CNNOCOM"
                blnStatusX = Proc_DoOpenRecord(myTType, RTrim(Me.txtTrans_Num.Text), RTrim("0"))
            Case Else
                blnStatusX = False
        End Select
        If blnStatusX = False Then
            'Me.BUT_OK.Enabled = False
            Me.lblMsg.Text = "Unable to get record..."
            Exit Sub
        End If


        strRptName = "PG_ERR"
        Select Case UCase(Trim(myTType))
            Case "DN"
                strRptName = "GL_RPT_DN"
            Case "DNNOCOM"
                strRptName = "GL_RPT_DN_NO_COMM"
            Case "CN"
                strRptName = "GL_RPT_CN"
            Case "CNNOCOM"
                strRptName = "GL_RPT_CN_NO_COMM"
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
        Select Case UCase(Trim(myTType))
            Case "DN", "DNNOCOM"
                strRptTitle = "D E B I T   N O T E"
                strRptTitle2 = "Report Title 2"
            Case "CN", "CNNOCOM"
                strRptTitle = "C R E D I T   N O T E"
                strRptTitle2 = "Report Title 2"
            Case Else
                strRptTitle = "*** Missing Report Title ***"
        End Select


        strID = RTrim("Y")
        strTransNum = RTrim(Me.txtTrans_Num.Text)
        strProc_Date = ""

        Select Case UCase(Trim(myTType))
            Case "DN", "DNNOCOM"
            Case "CN", "CNNOCOM"
            Case Else
        End Select

        Call Proc_Clear_Session()

        Session("rptname") = RTrim(strReportFile)

        Dim myArrList_RPT As ArrayList = Nothing
        Dim myArrList_DB As ArrayList = Nothing

        myArrList_RPT = New ArrayList()
        myArrList_DB = New ArrayList()

        myArrList_RPT.Clear()
        myArrList_DB.Clear()


        'myArrList_DB.Insert(0, RTrim("QUO"))
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        rParams(0) = strRptName

        Select Case UCase(Trim(myTType))
            Case "DN", "DNNOCOM"
                myArrList_RPT.Insert(0, RTrim(gnCOMP_NAME))
                myArrList_RPT.Insert(1, RTrim(strRptTitle))
                myArrList_RPT.Insert(2, RTrim(gnComp_Addr1) & " " & RTrim(gnComp_Addr2))
                myArrList_RPT.Insert(3, " Tel: " & RTrim(gnComp_TelNum) & " " & RTrim(gnComp_TelNum2) & " " & RTrim(gnComp_TelNum3) & " Fax: " & RTrim(gnComp_FaxNum))
                myArrList_RPT.Insert(4, "RC: " & RTrim(gnComp_RegNum))

                myArrList_DB.Insert(0, RTrim("BY_TRANS_NO"))
                myArrList_DB.Insert(1, RTrim("D"))
                myArrList_DB.Insert(2, RTrim("D"))
                myArrList_DB.Insert(3, RTrim(strTransNum))

               

            Case "CN", "CNNOCOM"
                myArrList_RPT.Insert(0, RTrim(gnCOMP_NAME))
                myArrList_RPT.Insert(1, RTrim(strRptTitle))
                myArrList_RPT.Insert(2, RTrim(gnComp_Addr1) & " " & RTrim(gnComp_Addr2))
                myArrList_RPT.Insert(3, " Tel: " & RTrim(gnComp_TelNum) & " " & RTrim(gnComp_TelNum2) & " " & RTrim(gnComp_TelNum3) & " Fax: " & RTrim(gnComp_FaxNum))
                myArrList_RPT.Insert(4, "RC: " & RTrim(gnComp_RegNum))

                myArrList_DB.Insert(0, RTrim("BY_TRANS_NO"))
                myArrList_DB.Insert(1, RTrim("C"))
                myArrList_DB.Insert(2, RTrim("C"))
                myArrList_DB.Insert(3, RTrim(strTransNum))

            Case Else
                myArrList_DB.Insert(0, RTrim("XYZ"))

        End Select

        Session("rptparams") = myArrList_RPT
        Session("dbparams") = myArrList_DB

        rParams(0) = strRptName
        rParams(1) = "P_FLAG="
        rParams(2) = myArrList_DB(0) + "&"
        rParams(3) = "P_DN="
        rParams(4) = myArrList_DB(1) + "&"
        rParams(5) = "P_CN="
        rParams(6) = myArrList_DB(2) + "&"
        rParams(7) = "P_VAL_01="
        rParams(8) = myArrList_DB(3) + "&"
        rParams(9) = url
        'param 1 = 


        Dim strReportParam As String = ""
        'strReportParam = strReportParam & "&rptparams=" & gnCOMP_NAME & "<*>" & RTrim(strRptTitle) & "<*>" & strRptTitle2
        strReportParam = strReportParam & "&rptparams=" & myArrList_RPT(0) & "<*>" & RTrim(myArrList_RPT(2)) & "<*>" & myArrList_RPT(3) & "<*>" & myArrList_RPT(4)
        strReportParam = strReportParam & "&dbparams=" & RTrim(myArrList_DB(0)) & "<*>" & RTrim(myArrList_DB(1)) & "<*>" & RTrim(myArrList_DB(2)) & "<*>" & RTrim(myArrList_DB(3))
        'strReportParam = strReportParam & "<*>" & RTrim(strID)

        'myArrList_RPT.Clear()
        'myArrList_RPT = Nothing
        'myArrList_DB.Clear()
        'myArrList_DB = Nothing

        ''Comments start here 
        'Dim mystrURL As String = ""
        'Try
        '    '    'OK
        '    '    'mystrURL = "window.open('" & "CRViewer.aspx?rptname=" & RTrim(strReportFile) & strReportParam & "','frmDoc','left=50,top=50,width=1024,height=650,titlebar=yes,z-lock=yes,address=yes,channelmode=1,fullscreen=no,directories=yes,location=yes,toolbar=yes,menubar=yes,status=yes,scrollbars=1,resizable=yes');"
        '    mystrURL = "window.open('" & "../CRViewerN.aspx?rptname=" & RTrim(strReportFile) & strReportParam & "','','left=50,top=10,width=1024,height=600,titlebar=yes,z-lock=yes,address=yes,channelmode=1,fullscreen=0,directories=yes,location=yes,toolbar=yes,menubar=yes,status=yes,scrollbars=1,resizable=yes');"
        '    '    'FirstMsg = "javascript:window.close();" & mystrURL
        '    FirstMsg = "javascript:" & mystrURL
        'Catch ex As Exception
        '    Me.lblMsg.Text = "<br />Unable to connect to report viewer. <br />Reason: " & ex.Message.ToString

        'End Try
        ''Comments ends here 
        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
    End Sub

    Private Sub Create_Excel_Quot_Invoice()

    End Sub

    Private Sub Create_Excel_Quot_Schedule()

    End Sub


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
        'If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        'ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
        '    Call gnProc_Populate_Box("GL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        'End If

        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            cboSearch.Items.Clear()
            cboSearch.Items.Add("* Select Insured *")
            Dim dt As DataTable = GET_INSURED_DCNOTE(txtSearch.Value.Trim(), UCase(Trim(myTType))).Tables(0)

            Dim dr As DataRow = dt.NewRow()
            'dr(0) = "* Select Insured *"
            'dr(1) = "*"
            'dt.Rows.InsertAt(dr, 0)
            cboSearch.DataSource = dt
            cboSearch.DataValueField = "TBIL_POL_PRM_DCN_TRANS_NO"
            cboSearch.DataTextField = "MyFld_Text"
            cboSearch.DataBind()
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
                Me.txtTrans_Num.Text = Me.cboSearch.SelectedItem.Value
                If LTrim(RTrim(Me.txtFileNum.Text)) <> "" Then
                    strStatus = Proc_DoOpenRecord(RTrim(myTType), Me.txtTrans_Num.Text, RTrim("0"))
                    If Trim(strStatus) = "true" Then
                    End If
                End If
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try


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
        strSQL = strSQL & "SELECT TOP 1 DC.TBIL_POL_PRM_DCN_FILE_NO, DC.TBIL_POL_PRM_DCN_PROP_NO, DC.TBIL_POL_PRM_DCN_POLY_NO"
        strSQL = strSQL & ", DC.TBIL_POL_PRM_DCN_PRDCT_CD"
        strSQL = strSQL & ", RTRIM(ISNULL(INSRD.TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(INSRD.TBIL_INSRD_FIRSTNAME,'')) AS T_INSURED_NAME"
        strSQL = strSQL & ", PROD.TBIL_PRDCT_DTL_DESC"
        strSQL = strSQL & " FROM " & strTable & " AS DC"
        strSQL = strSQL & " LEFT JOIN TBIL_INS_DETAIL AS INSRD"
        strSQL = strSQL & " ON INSRD.TBIL_INSRD_CODE = DC.TBIL_POL_PRM_DCN_INSRD_CODE"
        strSQL = strSQL & " AND INSRD.TBIL_INSRD_ID = '001'"
        strSQL = strSQL & " LEFT JOIN TBIL_PRODUCT_DETL AS PROD"
        strSQL = strSQL & " ON PROD.TBIL_PRDCT_DTL_CODE = DC.TBIL_POL_PRM_DCN_PRDCT_CD"
        strSQL = strSQL & " AND PROD.TBIL_PRDCT_DTL_MDLE IN('GRP','G')"
        strSQL = strSQL & " WHERE DC.TBIL_POL_PRM_DCN_TRANS_NO = '" & RTrim(strREC_ID) & "'"
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

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_FILE_NO") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            'Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POLY_REC_ID") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_PROP_NO") & vbNullString, String))

            'Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_POLICY_NO") & vbNullString, String))
            Me.txtPol_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_POLY_NO") & vbNullString, String))
            Me.txtAssured_Name.Text = RTrim(CType(objOLEDR("T_INSURED_NAME") & vbNullString, String))


            'Me.txtProductClass.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_CAT") & vbNullString, String))
            Me.txtProduct_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DCN_PRDCT_CD") & vbNullString, String))
            Me.txtProduct_Name.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_DESC") & vbNullString, String))

            Me.lblMsg.Text = "Status: Transaction No: " & Me.txtTrans_Num.Text

        Else

            Me.txtFileNum.Text = RTrim("")
            Me.txtQuote_Num.Text = RTrim("")
            Me.txtPol_Num.Text = RTrim("")
            Me.txtAssured_Name.Text = ""

            Me.txtProductClass.Text = ""
            Me.txtProduct_Num.Text = ""
            Me.txtProduct_Name.Text = ""

            Me.lblMsg.Text = "Status: New Entry..."
            Me.lblMsg.Text = "Sorry!. Unable to get record ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

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

End Class
