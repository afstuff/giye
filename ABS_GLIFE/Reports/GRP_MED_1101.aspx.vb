
Partial Class Reports_GRP_MED_1101
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String
    Protected PageURLs As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Dim strREC_ID As String
    Protected myTType As String = "0"

    Dim dteStart As Date
    Dim dteEnd As Date

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Protected strRptName As String
    Protected strReportFile As String
    Protected strRptTitle As String
    Protected strRptTitle2 As String

    Protected strTransNum As String

    Protected STRMENU_TITLE As String
    Protected BufferStr As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        PageLinks = ""
        'PageLinks = PageLinks & "<a href='javascript:window.close();' runat='server'>Close...</a>"
        PageLinks = "<a href='../MENU_GL.aspx?menu=GL_UND' class='a_sub_menu'>Return to Menu</a>&nbsp;"

        Try
            myTType = Request.QueryString("TTYPE")
        Catch ex As Exception
            myTType = "XYZ"
        Finally

        End Try

        Select Case UCase(Trim(myTType))
            Case "GRP_RPT_MED_1101"
                STRMENU_TITLE = UCase("+++ Medical Examination Requirement Report +++ ")
                BufferStr = ""
            Case Else
                STRMENU_TITLE = UCase("+++ Report +++ ")
                BufferStr = ""
        End Select

    End Sub

    Protected Sub BUT_OK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_OK.Click

        Dim myRetVal As String = ""
        Dim mydte As Date = Now

        dteStart = Now
        dteEnd = Now

        Dim myVal As String = ""
        Dim mySt_Pol As String = ""
        Dim myEn_Pol As String = ""
        Dim myRA_LIMIT As String = "15000000"

        Me.lblMsg.Text = "Status:"

        myRetVal = MyCheck_Date("DATE", Me.txtStart_Date, "Missing " & Me.lblStart_Date.Text, Me.lblMsg)
        If myRetVal = "false" Then
            Exit Sub
        End If
        mydte = Format(CDate(Mid(myRetVal, 6)), "MM/dd/yyyy")
        dteStart = Format(mydte, "MM/dd/yyyy")

        myRetVal = MyCheck_Date("DATE", Me.txtEnd_Date, "Missing " & Me.lblEnd_Date.Text, Me.lblMsg)
        If myRetVal = "false" Then
            Exit Sub
        End If
        mydte = Format(CDate(Mid(myRetVal, 6)), "MM/dd/yyyy")
        dteEnd = Format(mydte, "MM/dd/yyyy")

        If dteEnd < dteStart Then
            Me.lblMsg.Text = "Sorry. START DATE cannot be greater than END DATE"
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        Dim xc As Integer = 0

        '====================================================
        '   START CHECK
        '====================================================

        myVal = LTrim(RTrim(Me.txtStart_Pol_Num.Text))
        If Trim(myVal) = "" Or Trim(myVal) = "*" Or Trim(myVal) = "." Or Trim(myVal) = "?" Then
            Me.lblMsg.Text = "Missing input field or Invalid character found in input field - " & Me.lblStart_Pol_Num.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If


        For xc = 1 To Len(LTrim(RTrim(myVal)))
            If Mid(LTrim(RTrim(myVal)), xc, 1) = ";" Or Mid(LTrim(RTrim(myVal)), xc, 1) = ":" Or Mid(LTrim(RTrim(myVal)), xc, 1) = "?" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblStart_Pol_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        Next

        myVal = LTrim(RTrim(Me.txtEnd_Pol_Num.Text))
        If Trim(myVal) = "" Or Trim(myVal) = "*" Or Trim(myVal) = "." Or Trim(myVal) = "?" Then
            Me.lblMsg.Text = "Missing input field or Invalid character found in input field - " & Me.lblEnd_Pol_Num.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If


        For xc = 1 To Len(LTrim(RTrim(myVal)))
            If Mid(LTrim(RTrim(myVal)), xc, 1) = ";" Or Mid(LTrim(RTrim(myVal)), xc, 1) = ":" Or Mid(LTrim(RTrim(myVal)), xc, 1) = "?" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblEnd_Pol_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        Next

        myVal = LTrim(RTrim(Me.txtRA_LIMIT.Text))
        If Val(myVal) = 0 Or Trim(myVal) = "" Or Trim(myVal) = "*" Or Trim(myVal) = "." Or Trim(myVal) = "?" Then
            Me.lblMsg.Text = "Missing input field or Invalid character found in input field - " & Me.lblRA_LIMIT.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If
        If Not IsNumeric(myVal) Then
            Me.lblMsg.Text = "Invalid value found in input field. Numeric data required - " & Me.lblRA_LIMIT.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        mySt_Pol = LTrim(RTrim(Me.txtStart_Pol_Num.Text))
        myEn_Pol = LTrim(RTrim(Me.txtEnd_Pol_Num.Text))
        myRA_LIMIT = Val(Me.txtRA_LIMIT.Text)

        strRptName = "PG_ERR"
        strRptName = RTrim(myTType)

        Select Case UCase(Trim(myTType))
            Case "XYZ"
                strRptName = ""
            Case Else
                'strRptName = ""
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
            Case "GRP_RPT_MED_1101"
                strRptTitle = "MEDICAL EXAMINATION REQUIREMENT REPORT"
                strRptTitle2 = "Report Title 2"
            Case Else
                strRptTitle = "*** Missing Report Title ***"
        End Select


        'strID = RTrim("Y")
        'strTransNum = RTrim(Me.txtTrans_Num.Text)
        'strProc_Date = ""

        Select Case UCase(Trim(myTType))
            Case "GRP_RPT_MED_1101"
                If Me.chkExport_Xls.Checked = True Then
                    'Call Create_Excel_Quot_Schedule()
                    'Exit Sub
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

        Select Case UCase(Trim(myTType))
            Case "GRP_RPT_MED_1101"
                myArrList_RPT.Insert(0, RTrim(gnCOMP_NAME))
                myArrList_RPT.Insert(1, RTrim(strRptTitle))
                myArrList_RPT.Insert(2, RTrim(gnComp_Addr1) & " " & RTrim(gnComp_Addr2))
                myArrList_RPT.Insert(3, " Tel: " & RTrim(gnComp_TelNum) & " " & RTrim(gnComp_TelNum2) & " " & RTrim(gnComp_TelNum3) & " Fax: " & RTrim(gnComp_FaxNum))
                myArrList_RPT.Insert(4, "RC: " & RTrim(gnComp_RegNum))

                myArrList_DB.Insert(0, RTrim(Format(dteStart, "MM/dd/yyyy").ToString))
                myArrList_DB.Insert(1, RTrim(Format(dteEnd, "MM/dd/yyyy").ToString))
                myArrList_DB.Insert(2, RTrim(mySt_Pol))
                myArrList_DB.Insert(3, RTrim(myEn_Pol))
                myArrList_DB.Insert(4, RTrim(myRA_LIMIT))

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

End Class
