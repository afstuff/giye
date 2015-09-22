Imports CrystalDecisions.ReportAppServer.DataDefModel
Imports CrystalDecisions.Reporting
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions
Imports CrystalDecisions.Web
Imports System.Data
Imports System.IO

Partial Class CRViewerN
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String
    Protected PageURLs As String

    Protected STRCOMP_NAME As String

    Protected STRUSER_LOGIN_ID As String
    Protected STRUSER_LOGIN_NAME As String

    Dim strDB_Srv As String = ""
    Dim strDB_Name As String = ""
    Dim strDB_UID As String = ""
    Dim strDB_PWD As String = CStr("")

    Protected strRptParams As String
    Protected strDBParams As String

    Protected arrDB(15) As String
    Protected arrRPT(15) As String
    Protected intDB As Integer
    Protected intRPT As Integer

    Protected blnRet As Boolean

    'Session("connstr") = ConfigurationManager.AppSettings("APPCONN")
    Protected strCONN As String = ""

    Dim strSP_NAME As String = ""

    Protected strReportPath As String
    Protected strRptName As String

    Dim crdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
    'Protected crdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
    'Protected WithEvents crdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

    Dim oConInfo As New CrystalDecisions.Shared.ConnectionInfo()

    Protected arrList_RPT As ArrayList = New ArrayList()
    Protected arrList_DB As ArrayList = New ArrayList()


    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Try
            STRCOMP_NAME = CType(Session("CL_COMP_NAME"), String).ToString
        Catch ex As Exception
            'STRCOMP_NAME = gnComp_Name
            STRCOMP_NAME = "ABC COMPANY LTD"
            STRCOMP_NAME = "Custodian Life Assurance Limited"
        End Try

        If Not (Page.IsPostBack) Then
            PageURLs = ""
            Call DoProc_Init()
        End If

        ' Retrieve report name
        Try
            strRptName = CType(Session("rptname"), String).ToString
            strRptName = RTrim(strRptName) & ".rpt"
        Catch
            strRptName = "rptMissing.aspx"
            Me.lblMessage.Text = "Missing or Invalid report name..." & ""
            Exit Sub
        End Try

        ' Retrieve report field parameters
        Try
            arrList_RPT = CType(Session("rptparams"), ArrayList)
        Catch
            strRptParams = "*** Missing Report Field Parameters ***"
            Me.lblMessage.Text = Me.lblMessage.Text & "<br>" & strRptParams
            Exit Sub
        End Try


        ' Retrieve report database parameters
        Try
            arrList_DB = CType(Session("dbparams"), ArrayList)
        Catch
            strDBParams = "*** Missing Report Database Parameters ***"
            Me.lblMessage.Text = Me.lblMessage.Text & "<br>" & strDBParams
            Exit Sub
        End Try


        If Not (Page.IsPostBack) Then
            blnRet = False
            blnRet = DoConfig_Doc_Open()

            Try
                crdoc.SetParameterValue(0, arrList_RPT(0))
                crdoc.SetParameterValue(1, arrList_RPT(1))
                crdoc.SetParameterValue(2, arrList_RPT(2))

                crdoc.SetParameterValue(3, arrList_DB(0))
                crdoc.SetParameterValue(4, arrList_DB(1))
                crdoc.SetParameterValue(5, arrList_DB(3))
            Catch ex As Exception

            End Try

            'Me.CrystalReportViewer1.ReportSource = crdoc

        Else
            ''crdoc = New CrystalDecisions.CrystalReports.Engine.ReportDocument
            crdoc = CType(Session("mycrdoc"), CrystalDecisions.CrystalReports.Engine.ReportDocument)
            Me.CrystalReportViewer1.ReportSource = crdoc

        End If

        If Not IsPostBack Then
            Call MyShow_Report()
            Session("mycrdoc") = crdoc
        Else
            Try
                crdoc = CType(Session("mycrdoc"), CrystalDecisions.CrystalReports.Engine.ReportDocument)
                Me.CrystalReportViewer1.ReportSource = crdoc
                Call DoConfig_Report_ParametersInfo()
                Session("mycrdoc") = crdoc

            Catch ex As Exception
                Response.Write("<br>Error has occured. <br />Reason:" & ex.Message.ToString)

            End Try

        End If

        If Not (Page.IsPostBack) Then
            Try
                'Me.CrystalReportViewer1.ShowLastPage()
                'Me.txtTotalPageNumber.Text = Me.txtPageNumber.Text
                Me.CrystalReportViewer1.ShowFirstPage()
            Catch ex As Exception

            End Try
        End If

    End Sub

    Protected Sub cmdCloseX_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCloseX.ServerClick

        Try
            oConInfo = Nothing
        Catch ex As Exception

        End Try

        Try
            crdoc.Close()
        Catch ex As Exception
        End Try

        Try
            crdoc.Dispose()
        Catch ex As Exception
        End Try

        Try
            crdoc = Nothing
        Catch ex As Exception
        End Try

        'Try
        '    Me.CrystalReportViewer1.Dispose()
        'Catch ex As Exception

        'End Try

        Dim mystrURL As String = ""
        Try
            'mystrURL = "alert('About to close page ...'); window.close();"
            mystrURL = "window.close();"
            '    'FirstMsg = "javascript:window.close();" & mystrURL
            FirstMsg = "javascript:" & mystrURL

        Catch ex As Exception
        End Try


        'Me.lblMessage.Text = "About to close page..."
        'FirstMsg = "Javascript:alert('About to close page...')"
        'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMessage.Text & "');", True)
        'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "<script language=""JavaScript"">alert('" & Me.lblMessage.Text & "');</script>", True)
        'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "<script language=""JavaScript"">alert('About to close page...');</script>", True)

    End Sub


    Private Sub DoProc_Init()
        Dim intRC As Integer
        intRC = 0
        For intRC = 0 To 15 - 1
            arrRPT(intRC) = ""
            arrDB(intRC) = ""
        Next

    End Sub



    Private Function DoConfig_Doc_Open() As Boolean

        Dim mybln As Boolean = False

        strReportPath = gnGET_REPORT_PATH() & strRptName
        'Response.Write("<br>Report File: " & strReportPath)

        crdoc = New CrystalDecisions.CrystalReports.Engine.ReportDocument()

        If System.IO.File.Exists(strReportPath) = False Then
            'Me.lblMessage.Text = "Unable to open Report or Document: " & strReportPath
            Me.lblMessage.Text = "Sorry!. The system cannot find report: " & UCase(strRptName)
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');window.close();"
            Exit Function
        End If

        Try
            crdoc.Load(strReportPath, OpenReportMethod.OpenReportByTempCopy)
            crdoc.Refresh()

            'Response.Write("<br/>Open OK.")

            mybln = True

        Catch ex As Exception
            Me.lblMessage.Text = "Unable to open report. Reason: " & ex.Message.ToString

            mybln = False

        End Try

        Return mybln

    End Function


    Private Sub MyShow_Report(Optional ByVal pvCODE As String = "")

        'strReportPath = gnGET_REPORT_PATH() & strRptName

        'crdoc = New CrystalDecisions.CrystalReports.Engine.ReportDocument

        'Try

        '    If System.IO.File.Exists(strReportPath) = False Then
        '        Me.lblMessage.Text = "Unable to open Report or Document: " & strReportPath
        '        FirstMsg = "Javascript:alert('" & "Unable to open Report or Document: " & strRptName & "')"
        '        Exit Sub
        '    End If

        '    With crdoc
        '        .Load(strReportPath)

        '    End With

        'Catch ex As Exception
        '    Response.Write("<br>Report File: " & strReportPath)
        '    Response.Write("<br>Error has occured. <br />Reason:" & ex.Message.ToString)
        '    crdoc = Nothing
        '    Exit Sub
        'End Try


        'strDB_Srv = "(LOCAL)"
        'strDB_Name = "abs"
        'strDB_Name = "ABS_LIFE"
        'strDB_UID = "sa"
        'strDB_PWD = ""


        'strDB_Srv = "ABS_ACCT_DSN_RPT"
        'strDB_Name = "C:\AFRIK\RPT-DB\STAPP_DB_RPT"
        'strDB_UID = "Admin"
        'strDB_PWD = ""

        Dim strmyCONN = "Data source=" & strDB_Srv & ";Initial catalog=" & strDB_Name & ";User ID=" & strDB_UID & ";Password=" & strDB_PWD & ";"

        strmyCONN = RTrim(gnGET_CONN_STRING())

        Dim myarrData() As String
        Dim mystrData As String
        myarrData = Split(RTrim(strmyCONN), ";")
        For Each mystrData In myarrData
            Dim myintpos As Integer = InStr(1, mystrData, "=", CompareMethod.Text)
            If myintpos >= 1 Then
                If UCase(Mid(mystrData, 1, myintpos - 1)) = "DATA SOURCE" Then
                    strDB_Srv = UCase(Mid(mystrData, myintpos + 1))
                ElseIf UCase(Mid(mystrData, 1, myintpos - 1)) = "INITIAL CATALOG" Then
                    strDB_Name = Mid(mystrData, myintpos + 1)
                ElseIf UCase(Mid(mystrData, 1, myintpos - 1)) = "USER ID" Then
                    strDB_UID = Mid(mystrData, myintpos + 1)
                ElseIf UCase(Mid(mystrData, 1, myintpos - 1)) = "PASSWORD" Then
                    strDB_PWD = Mid(mystrData, myintpos + 1)
                End If
            End If
        Next

        If Trim(strDB_PWD) = "" Then
        End If
        strmyCONN = "Data Source=" & strDB_Srv & ";Initial Catalog=" & strDB_Name & ";User ID=" & strDB_UID & ";Password=" & strDB_PWD & ";"


        If Trim(pvCODE) = "EXP_RPT" Then
            'Exit Sub
        End If


        Dim oConInfo As New CrystalDecisions.Shared.ConnectionInfo
        With oConInfo
            .AllowCustomConnection = True
            '.Type = ConnectionInfoType.SQL
            .Type = ConnectionInfoType.CRQE

            '.IntegratedSecurity = False

            .ServerName = strDB_Srv
            .DatabaseName = strDB_Name
            .UserID = strDB_UID
            .Password = strDB_PWD

        End With

        Dim myTableName As String = "ABSWT_BS_PL"

        Dim intC As Integer = 0
        Dim intC2 As Integer = 0

        Dim cr_ds_conn As CrystalDecisions.Shared.DataSourceConnections = crdoc.DataSourceConnections
        Dim cr_iconinfo As CrystalDecisions.Shared.IConnectionInfo
        For intC = 0 To cr_ds_conn.Count - 1
            cr_iconinfo = cr_ds_conn.Item(intC)
            With cr_iconinfo
                .SetConnection(CStr(strDB_Srv), CStr(strDB_Name), CStr(strDB_UID), CStr(strDB_PWD))
            End With
        Next


        '*****
        Call SetCrystalLogin(strDB_Srv, strDB_Name, strDB_UID, strDB_PWD, crdoc)
        Call SetDBLogon_Info(oConInfo, crdoc)

        crdoc.Refresh()

        Me.CrystalReportViewer1.ReportSource = crdoc
        Call SetDBTableLogon(oConInfo, Me.CrystalReportViewer1)
        intDB = 0
        intRPT = 0
        intC = 0

        Call DoConfig_Report_ParametersInfo()

    End Sub
    Private Sub SetDBLogon_Info(ByVal myConnectionInfo As CrystalDecisions.Shared.ConnectionInfo, ByVal myReportDocument As CrystalDecisions.CrystalReports.Engine.ReportDocument)
        On Error GoTo Err_Rtn


        Dim myTables As CrystalDecisions.CrystalReports.Engine.Tables = myReportDocument.Database.Tables

        For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
            Dim myTableLogonInfo As CrystalDecisions.Shared.TableLogOnInfo = myTable.LogOnInfo

            myTableLogonInfo.ConnectionInfo = myConnectionInfo
            myTable.Location = myTable.Name
            myTable.ApplyLogOnInfo(myTableLogonInfo)
        Next
        Exit Sub

Err_Rtn:
        Response.Write("<br />*** Database logon error. <br />Error: " & Err.Number & " - " & Err.Description & "<br>")
        Err.Clear()
    End Sub
    Public Shared Sub SetCrystalLogin(ByVal sServer As String, ByVal sCompanyDB As String, ByVal sUser As String, ByVal sPassword As String, _
   ByRef oRpt As CrystalDecisions.CrystalReports.Engine.ReportDocument)

        Dim oDB As CrystalDecisions.CrystalReports.Engine.Database = oRpt.Database
        Dim oTables As CrystalDecisions.CrystalReports.Engine.Tables = oDB.Tables
        Dim oLogonInfo As CrystalDecisions.Shared.TableLogOnInfo

        Dim oConnectInfo As CrystalDecisions.Shared.ConnectionInfo = New CrystalDecisions.Shared.ConnectionInfo()

        oConnectInfo.DatabaseName = sCompanyDB
        oConnectInfo.ServerName = sServer
        oConnectInfo.UserID = sUser
        oConnectInfo.Password = sPassword

        ' Set the logon credentials for all tables
        For Each oTable As CrystalDecisions.CrystalReports.Engine.Table In oTables
            oLogonInfo = oTable.LogOnInfo
            oLogonInfo.ConnectionInfo = oConnectInfo
            oTable.ApplyLogOnInfo(oLogonInfo)
        Next

        ' Check for subreports
        Dim oSections As CrystalDecisions.CrystalReports.Engine.Sections
        Dim oSection As CrystalDecisions.CrystalReports.Engine.Section
        Dim oRptObjs As CrystalDecisions.CrystalReports.Engine.ReportObjects
        Dim oRptObj As CrystalDecisions.CrystalReports.Engine.ReportObject
        Dim oSubRptObj As CrystalDecisions.CrystalReports.Engine.SubreportObject
        Dim oSubRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        oSections = oRpt.ReportDefinition.Sections
        For Each oSection In oSections
            oRptObjs = oSection.ReportObjects
            For Each oRptObj In oRptObjs

                If oRptObj.Kind = CrystalDecisions.Shared.ReportObjectKind.SubreportObject Then

                    ' This is a subreport so set the logon credentials for this report's tables
                    oSubRptObj = CType(oRptObj, CrystalDecisions.CrystalReports.Engine.SubreportObject)
                    ' Open the subreport
                    oSubRpt = oSubRptObj.OpenSubreport(oSubRptObj.SubreportName)

                    oDB = oSubRpt.Database
                    oTables = oDB.Tables
                    For Each oTable As CrystalDecisions.CrystalReports.Engine.Table In oTables
                        oLogonInfo = oTable.LogOnInfo
                        oLogonInfo.ConnectionInfo = oConnectInfo
                        oTable.ApplyLogOnInfo(oLogonInfo)
                    Next
                End If
            Next
        Next
        oRpt.Refresh()

    End Sub

    Private Sub SetDBTableLogon(ByVal objConnInfo As CrystalDecisions.Shared.ConnectionInfo, ByVal objCRV1 As CrystalDecisions.Web.CrystalReportViewer)

        On Error GoTo Err_Rtn

        Dim myTableLogOnInfos As CrystalDecisions.Shared.TableLogOnInfos = Nothing

        myTableLogOnInfos = New CrystalDecisions.Shared.TableLogOnInfos
        myTableLogOnInfos = objCRV1.LogOnInfo

        'objCRV1.LogOnInfo.Clear()

        Dim myTableLogOnInfoN As CrystalDecisions.Shared.TableLogOnInfo
        For Each myTableLogOnInfoN In myTableLogOnInfos
            myTableLogOnInfoN.ConnectionInfo = objConnInfo

        Next
        Exit Sub

Err_Rtn:
        Response.Write("<br />*** Table logon error. <br />Error: " & Err.Number & " - " & Err.Description & "<br>")
        Err.Clear()

    End Sub
    Private Sub DoConfig_Report_ParametersInfo()

        intDB = 0
        intRPT = 0

        Dim intC As Integer = 0
        intC = 0

        Dim ParamFlds As CrystalDecisions.Shared.ParameterFields
        ParamFlds = crdoc.ParameterFields
        Dim crParameterFieldDefinitions As CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinitions
        Dim crParameterFieldDefinition As CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition
        Dim crParameterValues As New ParameterValues
        Dim rptDiscrete_Val As New ParameterDiscreteValue


        crParameterFieldDefinitions = crdoc.DataDefinition.ParameterFields



        For intC = 0 To ParamFlds.Count - 1

            Select Case ParamFlds.Item(intC).ParameterType.ToString
                Case "ReportParameter"      '0
                    intRPT = intRPT + 1
                    Select Case ParamFlds.Item(intC).Name
                        Case "crCompName"
                            rptDiscrete_Val.Value = arrList_RPT(intRPT - 1)
                            crParameterFieldDefinition = crParameterFieldDefinitions.Item(ParamFlds.Item(intC).Name)
                            crParameterValues = crParameterFieldDefinition.CurrentValues
                            crParameterValues.Add(rptDiscrete_Val)
                            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
                            'rptDiscrete_Val = Nothing
                        Case "crCompAddr1"
                            rptDiscrete_Val.Value = arrList_RPT(intRPT - 1)
                            crParameterFieldDefinition = crParameterFieldDefinitions.Item(ParamFlds.Item(intC).Name)
                            crParameterValues = crParameterFieldDefinition.CurrentValues
                            crParameterValues.Add(rptDiscrete_Val)
                            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
                            'rptDiscrete_Val = Nothing

                        Case "crCompAddr2"
                            rptDiscrete_Val.Value = arrList_RPT(intRPT - 1)
                            crParameterFieldDefinition = crParameterFieldDefinitions.Item(ParamFlds.Item(intC).Name)
                            crParameterValues = crParameterFieldDefinition.CurrentValues
                            crParameterValues.Add(rptDiscrete_Val)
                            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
                            'rptDiscrete_Val = Nothing
                        Case "crRegNum"
                            rptDiscrete_Val.Value = arrList_RPT(intRPT - 1)
                            crParameterFieldDefinition = crParameterFieldDefinitions.Item(ParamFlds.Item(intC).Name)
                            crParameterValues = crParameterFieldDefinition.CurrentValues
                            crParameterValues.Add(rptDiscrete_Val)
                            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
                            'rptDiscrete_Val = Nothing

                        Case "crReportTitle"
                            Me.lblMessage.Text = arrList_RPT(intRPT - 1)
                            rptDiscrete_Val.Value = arrList_RPT(intRPT - 1)
                            crParameterFieldDefinition = crParameterFieldDefinitions.Item(ParamFlds.Item(intC).Name)
                            crParameterValues = crParameterFieldDefinition.CurrentValues
                            crParameterValues.Add(rptDiscrete_Val)
                            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
                            'rptDiscrete_Val = Nothing
                        Case Else
                    End Select

                Case "StoreProcedureParameter"      '1
                    intDB = intDB + 1
                    rptDiscrete_Val.Value = arrList_DB(intDB - 1)
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item(ParamFlds.Item(intC).Name)
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(rptDiscrete_Val)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
                    'rptDiscrete_Val = Nothing
                Case "QueryParameter"       '2
                Case "ConnectionParameter"  '3
                Case Else

            End Select

        Next


        'Setting Report Parameter field info with parameter collection object

        'Me.CrystalReportViewer1.ParameterFieldInfo = ParamFlds

        ' export the document to the temporary file.
        'crdoc.Export()

        With Me.CrystalReportViewer1
            .ParameterFieldInfo = crdoc.ParameterFields
            .ReportSource = crdoc
            .EnableDatabaseLogonPrompt = False
            .EnableParameterPrompt = False
            .ReuseParameterValuesOnRefresh = True

            .DisplayGroupTree = True
            .DisplayPage = True
            .HasRefreshButton = True
            .HasPrintButton = True

            .EnableViewState = True

            .HasCrystalLogo = False
            '.Zoom(100)
            .DisplayPage = True

            .RefreshReport()
            .DataBind()

        End With


        'ParamFlds = Nothing

        'crdoc.Close()
        'crdoc = Nothing
        crParameterValues = Nothing

    End Sub

    

    Private Sub MyReport_Param(ByVal Param_Name As String, ByVal Param_Value As String, ByVal rptPrm_Fields As CrystalDecisions.Shared.ParameterFields)

        Try
            Dim rptPrm_Fld As New CrystalDecisions.Shared.ParameterField
            'rptPrm_Fld = New CrystalDecisions.Shared.ParameterField
            rptPrm_Fld.ParameterFieldName = Param_Name

            Dim rptDiscrete_Val As CrystalDecisions.Shared.ParameterDiscreteValue
            rptDiscrete_Val = New CrystalDecisions.Shared.ParameterDiscreteValue
            rptDiscrete_Val.Value = Param_Value



            rptPrm_Fld.CurrentValues.Add(rptDiscrete_Val)
            rptPrm_Fields.Find(Param_Name, "").CurrentValues = rptPrm_Fld.CurrentValues

            'rptPrm_Fields.Add(rptPrm_Fld)

            rptDiscrete_Val = Nothing
            rptPrm_Fld = Nothing

            'Me.lblMessage.Text = Me.lblMessage.Text & "<BR /> Setting report parameters successful - " & Param_Name & " - " & Param_Value & "<BR />"
            Exit Sub

        Catch ex As Exception
            Response.Write("<br/>Error while setting report parameter: " & Param_Name & " - " & Param_Value & "<br />" & "Reason: " & ex.Message.ToString & "<br />")
            'Me.lblMsg.Text = Me.lblMsg.Text & "<BR /> Error while setting report parameter: " & Param_Name & " - " & Param_Value & "<br />" & "Reason: " & ex.Message.ToString & "<br />"
            Exit Sub
        End Try

    End Sub

    




    Private Sub VBConnectionCode(ByVal boReportDocument As ReportDocument)

        Dim strCode(100) As String
        Dim i As Integer = 0

        'strCode(i) = "'**EDIT** Change the path and report name to the report you want to change."
        boReportDocument.Load("c:\reports\yourreport.rpt", OpenReportMethod.OpenReportByTempCopy)

        Dim crTbl As CrystalDecisions.ReportAppServer.DataDefModel.Table
        crTbl = boReportDocument.ReportClientDocument.DatabaseController.Database.Tables(0)

        ''Add code based on the class of table in the report.
        'Select Case crTbl.ClassName
        '    Case "CrystalReports.Procedure"
        '        'strCode(i) = "'Create a new Stored Procedure Table to replace the reports current table."
        '        Dim boTable As New CrystalDecisions.ReportAppServer.DataDefModel.Procedure
        '    Case "CrystalReports.CommandTable"
        '        'strCode(i) = "'Create a new Command Table to replace the reports current table."
        '        Dim boTable As New CrystalDecisions.ReportAppServer.DataDefModel.CommandTable
        '    Case "CrystalReports.Table"
        '        'strCode(i) = "'Create a new Database Table to replace the reports current table."
        '        Dim boTable As New CrystalDecisions.ReportAppServer.DataDefModel.Table
        'End Select

        'strCode(i) = "'boMainPropertyBag: These hold the attributes of the tables ConnectionInfo object"
        'Dim boMainPropertyBag As New PropertyBag
        'strCode(i) = "'boInnerPropertyBag: These hold the attributes for the QE_LogonProperties"
        'strCode(i) = "'In the main property bag (boMainPropertyBag)"
        'Dim boInnerPropertyBag As New PropertyBag
        'strCode(i) = "'Set the attributes for the boInnerPropertyBag"

        Dim crCi As CrystalDecisions.ReportAppServer.DataDefModel.ConnectionInfo
        crCi = crTbl.ConnectionInfo

        Dim boMainPropertyBag As PropertyBag = crCi.Attributes
        Dim boInnerPropertyBag As PropertyBag = boMainPropertyBag.Item("QE_LogonProperties")
        Dim propIDs As Strings
        propIDs = boInnerPropertyBag.PropertyIDs
        Dim propID As String
        For Each propID In propIDs
            boInnerPropertyBag.Add(propID, boInnerPropertyBag(propID).ToString)
        Next

        'strCode(i) = "'Set the attributes for the boMainPropertyBag"
        propIDs = boMainPropertyBag.PropertyIDs
        Dim strVarName As String = "boInnerPropertyBag"
        For Each propID In propIDs
            i += 1
            If propID = "QE_LogonProperties" Then
                'strCode(i) = "'Add the QE_LogonProperties we set in the boInnerPropertyBag Object"
                i += 1
                boMainPropertyBag.Add(propID, boInnerPropertyBag)
            Else
                boMainPropertyBag.Add(propID, boMainPropertyBag(propID).ToString)
            End If
        Next

        'strCode(i) = "'Create a new ConnectionInfo object"
        Dim boConnectionInfo As New CrystalDecisions.ReportAppServer.DataDefModel.ConnectionInfo
        'strCode(i) = "'Pass the database properties to a connection info object"
        boConnectionInfo.Attributes = boMainPropertyBag
        'strCode(i) = "'Set the connection kind"
        boConnectionInfo.Kind = "CrConnectionInfoKindEnum." + crTbl.ConnectionInfo.Kind.ToString
        'strCode(i) = "'**EDIT** Set the User Name and Password if required."
        'i += 1
        boConnectionInfo.UserName = "UserName"
        boConnectionInfo.Password = "Password"
        'strCode(i) = "'Pass the connection information to the table"
        'boTable.ConnectionInfo = boConnectionInfo




        'strCode(i) = "'Get the Database Tables Collection for your report"
        'i += 1
        Dim boTables As CrystalDecisions.ReportAppServer.DataDefModel.Tables = _
                boReportDocument.ReportClientDocument.DatabaseController.Database.Tables

        'strCode(i) = "'For each table in the report:"
        'i += 1
        'strCode(i) = "' - Set the Table Name properties."
        If crTbl.ClassName = "CrystalReports.CommandTable" Then
            'i += 1
            'strCode(i) = "' - Set the Command table's command text."
        End If
        'i += 1
        'strCode(i) = "' - Set the table location in the report to use the new modified table"


        Dim itbl As Integer = 0

        ''Add code based on the class of table in the report.
        Select Case crTbl.ClassName
            Case "CrystalReports.Procedure"
                'strCode(i) = "'Create a new Stored Procedure Table to replace the reports current table."
                Dim boTable As New CrystalDecisions.ReportAppServer.DataDefModel.Procedure

                'strCode(i) = "'Pass the connection information to the table"
                boTable.ConnectionInfo = boConnectionInfo

                For Each crTbl In boReportDocument.ReportClientDocument.DatabaseController.Database.Tables
                    'i += 1
                    boTable.Name = crTbl.Name
                    'i += 1
                    boTable.QualifiedName = crTbl.QualifiedName
                    'i += 1
                    boTable.Alias = crTbl.Alias
                    If crTbl.ClassName = "CrystalReports.CommandTable" Then
                        boTable.CommandText = GetCommandTextVB(crTbl)
                    End If
                    boReportDocument.ReportClientDocument.DatabaseController.SetTableLocation(boTables(itbl.ToString), boTable)
                Next

            Case "CrystalReports.CommandTable"
                'strCode(i) = "'Create a new Command Table to replace the reports current table."
                Dim boTable As New CrystalDecisions.ReportAppServer.DataDefModel.CommandTable

                'strCode(i) = "'Pass the connection information to the table"
                boTable.ConnectionInfo = boConnectionInfo

                For Each crTbl In boReportDocument.ReportClientDocument.DatabaseController.Database.Tables
                    'i += 1
                    boTable.Name = crTbl.Name
                    'i += 1
                    boTable.QualifiedName = crTbl.QualifiedName
                    'i += 1
                    boTable.Alias = crTbl.Alias
                    If crTbl.ClassName = "CrystalReports.CommandTable" Then
                        boTable.CommandText = GetCommandTextVB(crTbl)
                    End If
                    boReportDocument.ReportClientDocument.DatabaseController.SetTableLocation(boTables(itbl.ToString), boTable)
                Next

            Case "CrystalReports.Table"
                'strCode(i) = "'Create a new Database Table to replace the reports current table."
                Dim boTable As New CrystalDecisions.ReportAppServer.DataDefModel.Table

                'strCode(i) = "'Pass the connection information to the table"
                boTable.ConnectionInfo = boConnectionInfo

                For Each crTbl In boReportDocument.ReportClientDocument.DatabaseController.Database.Tables
                    'i += 1
                    boTable.Name = crTbl.Name
                    'i += 1
                    boTable.QualifiedName = crTbl.QualifiedName
                    'i += 1
                    boTable.Alias = crTbl.Alias
                    If crTbl.ClassName = "CrystalReports.CommandTable" Then
                        boTable.CommandText = GetCommandTextVB(crTbl)
                    End If
                    boReportDocument.ReportClientDocument.DatabaseController.SetTableLocation(boTables(itbl.ToString), boTable)
                Next

        End Select


        'strCode(i) = "'Verify the database after adding substituting the new table."
        'strCode(i) = "'To ensure that the table updates properly when adding Command tables or Stored Procedures."
        boReportDocument.VerifyDatabase()


        If crTbl.ClassName = "CrystalReports.Procedure" Then
            'strCode(i) = "'**EDIT** Set the value for the Stored Procedure parameters."
            Dim crParam As CrystalDecisions.Shared.ParameterField
            For Each crParam In boReportDocument.ParameterFields
                If crParam.ReportParameterType = CrystalDecisions.Shared.ParameterType.StoreProcedureParameter Then
                    boReportDocument.SetParameterValue(crParam.Name, "Parameter Value")
                End If
            Next
        End If

        'Return boReportDocument
        'strCode(i) = "End Function"
        'ReDim Preserve strCode(i + 1)
        'txtVBCode.Lines = strCode

    End Sub

    Private Function GetCommandTextVB(ByVal cmTbl As CommandTable) As String
        Dim strCmd1 As String = ""
        Dim strCmd2 As String = ""
        Dim iStart As Integer = 0
        Dim iLength As Integer = 50

        strCmd1 = cmTbl.CommandText.Trim()
        strCmd1 = strCmd1.Replace(Chr(13) + Chr(10), " ")
        If strCmd1.Contains(Chr(34)) Then
            If strCmd1.EndsWith(Chr(34)) Then
                strCmd1 = strCmd1.Remove(strCmd1.Length - 1, 1)
                strCmd1 = strCmd1.Replace(Chr(34), Chr(34) + " + Chr(34) + " + Chr(34))
                strCmd1 += Chr(34) + " + Chr(34)"
            Else
                strCmd1 = strCmd1.Replace(Chr(34), Chr(34) + " + Chr(34) + " + Chr(34))
            End If
        End If

        If strCmd1.Length > 100 Then
            For i As Integer = 0 To System.Math.Round(strCmd1.Length / 50) - 1
                If strCmd1.Contains(Chr(34)) Then
                    iLength = strCmd1.IndexOf(" + Chr(34) + ", iLength + 1) + 12
                    strCmd2 += strCmd1.Substring(iStart, iLength - iStart + 1) + Chr(95) + Chr(13) + Chr(10)
                    iStart = iLength + 1
                    iLength += 50
                Else
                    iLength = strCmd1.IndexOf(Chr(32), iLength + 1)
                    strCmd2 += strCmd1.Substring(iStart, iLength - iStart + 1) + Chr(34) + " + " _
                    + Chr(95) + Chr(13) + Chr(10) + Chr(34)
                    iStart = iLength + 1
                    iLength += 50
                End If

                If strCmd1.Length - iLength <= 50 Then
                    If strCmd1.EndsWith(")") Then
                        strCmd2 += strCmd1.Substring(iStart, strCmd1.Length - iStart)
                    Else
                        strCmd2 += strCmd1.Substring(iStart, strCmd1.Length - iStart) + Chr(34)
                    End If
                    Exit For
                End If
            Next
        Else
            If strCmd1.EndsWith("chr(34)") Then
                strCmd2 = strCmd1
            Else
                strCmd2 = strCmd1 + Chr(34)
            End If
        End If

        Return strCmd2
    End Function




    Protected Sub CrystalReportViewer1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles CrystalReportViewer1.Init

    End Sub

    'Protected Sub cmdCloseX_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCloseX.Click
    '    Response.Redirect("../policy/PRG_LI_GRP_POLY_CONVERT.aspx")
    'End Sub
End Class
