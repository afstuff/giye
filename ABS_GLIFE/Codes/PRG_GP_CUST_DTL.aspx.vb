﻿Imports System.Data
Imports System.Data.OleDb
Imports CustodianGroupLife.Data


Partial Class PRG_GP_CUST_DTL
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String
    Protected STRPAGE_TITLE As String

    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strP_ID As String
    Protected strP_TYPE As String
    Protected strP_DESC As String
    Protected strPOP_UP As String

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strErrMsg As String
    Dim MainAcctCode As String
    Dim SubAcctCode As String
    Dim MainAcctDesc As String
    Dim SubCodeInitial As String
    Dim AcctLevel As String
    Dim MainGroup As String
    Dim LedgerType As String
    Dim Sub1Group As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'This program also writes to the chart of account aside from its primary aim which is to create new assured.
        'It also consider Assured class for the prefix of the sub acccounts code
        strTableName = "TBIL_INS_DETAIL"

        Try
            strP_TYPE = CType(Request.QueryString("optid"), String)
            strP_DESC = CType(Request.QueryString("optd"), String)
        Catch ex As Exception
            strP_TYPE = "ERR_TYPE"
            strP_DESC = "ERR_DESC"
        End Try

        Try
            strPOP_UP = CType(Request.QueryString("popup"), String)
        Catch ex As Exception
            strPOP_UP = "NO"
        End Try

        If UCase(Trim(strPOP_UP)) = "YES" Then
            Me.PageAnchor_Return_Link.Visible = False
            PageLinks = "<a class='a_return_menu' href='javascript:window.close();' onclick='javascript:window.close();'>Click here to CLOSE PAGE...</a>"
        Else
            Me.PageAnchor_Return_Link.Visible = True
            PageLinks = ""
        End If

        STRPAGE_TITLE = "Master Codes Setup - " & strP_DESC

        If Trim(strP_TYPE) = "ERR_TYPE" Or Trim(strP_TYPE) = "" Then
            strP_TYPE = ""
        End If

        strP_ID = "L01"
        Me.txtCustID.Text = RTrim(strP_TYPE)
        Me.txtCustID.Text = RTrim("001")

        If Not (Page.IsPostBack) Then
            Me.tr_close_list.Visible = False
            'Call Proc_Populate_Box("IL_INS_MODULE_LIST", Trim("001"), Me.cboCustModule)
            'Call Proc_Populate_Box("IL_INS_CLASS_LIST", Trim("001"), Me.cboCustClass)
            ''Call Proc_Populate_Box("IL_INS_DETAIL_LIST", Trim(Me.txtCustID.Text), Me.cboTransList)

            Call Proc_Populate_Box("GL_INS_MODULE_LIST", Trim("001"), Me.cboCustModule)
            Call Proc_Populate_Box("GL_INS_CLASS_LIST", Trim("001"), Me.cboCustClass)
            'Call Proc_Populate_Box("GL_INS_DETAIL_LIST", Trim(Me.txtCustID.Text), Me.cboTransList)
            Call Proc_DataBind()
            Call DoNew()
            'Me.lblMessage.Text = strSQL
            Me.txtAction.Text = ""
            'Me.txtCustNum.Enabled = True
            'Me.txtCustNum.Focus()
            Me.txtCustName.Enabled = True
            Me.txtCustName.Focus()
        End If

        If Me.txtAction.Text = "New" Then
            Me.tr_close_list.Visible = False
            Call DoNew()
            'Call Proc_OpenRecord(Me.txtNum.Text)
            Me.txtAction.Text = ""
            'Me.txtCustNum.Enabled = True
            'Me.txtCustNum.Focus()
            Me.txtCustName.Enabled = True
            Me.txtCustName.Focus()
        End If

        If Me.txtAction.Text = "Save" Then
            'Call DoSave()
            'Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete" Then
            Call DoDelete()
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete_Item" Then
            'Call DoDelItem()
            Me.txtAction.Text = ""
        End If

    End Sub

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        Me.txtSearch.Value = "ZZZZZ"
        Call Proc_DataBind()
        Me.tr_close_list.Visible = False
        Me.txtSearch.Value = "Search..."

    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call DoSave()
        Me.txtAction.Text = ""

    End Sub

    Protected Sub txtCustNum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustNum.TextChanged
        If RTrim(Me.txtCustNum.Text) <> "" Then
            lblMessage.Text = RTrim(Me.txtCustNum.Text)
            strREC_ID = RTrim(Me.txtCustNum.Text)
            strErrMsg = Proc_OpenRecord(Me.txtCustNum.Text)
        End If

    End Sub

    Protected Sub DoNew()
        Call Proc_DDL_Get(Me.cboCustModule, RTrim("*"))
        Call Proc_DDL_Get(Me.cboCustClass, RTrim("*"))
        Call Proc_DDL_Get(Me.cboTransList, RTrim("*"))

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

        With Me
            .txtRecNo.Text = "0"
            .txtRecNo.Enabled = False

            .txtCustID.Enabled = False
            .chkNum.Checked = True
            .chkNum.Enabled = True
            .chkWrtToChart.Checked = True
            .chkWrtToChart.Enabled = True
            .txtCustModule.Text = ""
            .txtCustClass.Text = ""

            .txtCustNum.ReadOnly = False
            .txtCustNum.Enabled = True
            .txtCustNum.Enabled = False
            .txtCustNum.Text = ""
            .cboTransList.Enabled = False

            .txtCustName.Text = ""
            .txtShortName.Text = ""
            .txtCustAddr01.Text = ""
            .txtCustAddr02.Text = ""
            .txtCustPhone01.Text = ""
            .txtCustPhone02.Text = ""
            .txtCustEmail01.Text = ""
            .txtCustEmail02.Text = ""

            .cmdDelete_ASP.Enabled = False
            .lblMessage.Text = "Status: New Entry..."
        End With
        strREC_ID = ""

    End Sub

    Protected Sub Proc_Populate_Box(ByVal pvCODE As String, ByVal pvTransType As String, ByVal pvcboList As DropDownList)
        'Populate box with codes
        pvcboList.Items.Clear()
        Select Case UCase(Trim(pvCODE))
            Case "IL_INS_MODULE_LIST"
                strTable = strTableName
                strSQL = ""
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "GL_INS_MODULE_LIST"
                strTable = strTableName
                strSQL = ""
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "IL_INS_CLASS_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_INS_CLASS")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_INS_CLASS_CD AS MyFld_Value, TBIL_INS_CLASS_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_INS_CLASS_TYPE = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " ORDER BY TBIL_INS_CLASS_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "GL_INS_CLASS_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_INS_CLASS")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_INS_CLASS_CD AS MyFld_Value, TBIL_INS_CLASS_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_INS_CLASS_TYPE = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " ORDER BY TBIL_INS_CLASS_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "IL_INS_DETAIL_LIST"
                'Try
                '    Me.txtCustModule.Text = cboCustModule.SelectedValue
                'Catch ex As Exception
                'End Try

                strTable = strTableName
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_INSRD_CODE AS MyFld_Value"
                strSQL = strSQL & ",RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,'')) AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_INSRD_ID = '" & RTrim(pvTransType) & "'"
                'strSQL = strSQL & " AND TBIL_INSRD_MDLE = '" & RTrim(Me.txtCustModule.Text) & "'"
                strSQL = strSQL & " AND (TBIL_INSRD_SURNAME LIKE '%" & RTrim(Me.txtSearch.Value) & "%'"
                strSQL = strSQL & " OR TBIL_INSRD_FIRSTNAME LIKE '%" & RTrim(Me.txtSearch.Value) & "%')"
                strSQL = strSQL & " ORDER BY TBIL_INSRD_ID, RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,''))"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "GL_INS_DETAIL_LIST"
                'Try
                '    Me.txtCustModule.Text = cboCustModule.SelectedValue
                'Catch ex As Exception
                'End Try

                strTable = strTableName
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_INSRD_CODE AS MyFld_Value"
                strSQL = strSQL & ",RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,'')) AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_INSRD_ID = '" & RTrim(pvTransType) & "'"
                'strSQL = strSQL & " AND TBIL_INSRD_MDLE = '" & RTrim(Me.txtCustModule.Text) & "'"
                strSQL = strSQL & " AND (TBIL_INSRD_SURNAME LIKE '%" & RTrim(Me.txtSearch.Value) & "%'"
                strSQL = strSQL & " OR TBIL_INSRD_FIRSTNAME LIKE '%" & RTrim(Me.txtSearch.Value) & "%')"
                strSQL = strSQL & " ORDER BY TBIL_INSRD_ID, RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,''))"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

        End Select

    End Sub


    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TBIL_INSRD_REC_ID, TBIL_INSRD_ID, TBIL_INSRD_CODE"
        strSQL = strSQL & ",RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,'')) AS TBIL_INSRD_FULL_NAME"
        strSQL = strSQL & ",RTRIM(ISNULL(TBIL_INSRD_PHONE1,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_PHONE2,'')) AS TBIL_INSRD_PHONE_NUM"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " WHERE TBIL_INSRD_ID = '" & RTrim(Me.txtCustID.Text) & "'"
        strSQL = strSQL & " AND (TBIL_INSRD_SURNAME LIKE '%" & RTrim(Me.txtSearch.Value) & "%'"
        strSQL = strSQL & " OR TBIL_INSRD_FIRSTNAME LIKE '%" & RTrim(Me.txtSearch.Value) & "%')"
        strSQL = strSQL & " ORDER BY TBIL_INSRD_ID, RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,''))"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        'open connection to database
        objOLEConn.Open()

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
        objDS = Nothing
        objDA = Nothing
        'objOLECmd.Dispose()
        'objOLECmd = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        Dim P As Integer = 0
        Dim C As Integer = 0

        C = 0
        For P = 0 To Me.GridView1.Rows.Count - 1
            C = C + 1
        Next
        If C >= 1 Then
            Me.cmdDelete_ASP.Enabled = True
            Me.tr_close_list.Visible = True
            Call Proc_Populate_Box("GL_INS_DETAIL_LIST", Trim("001"), Me.cboTransList)
        Else
            Me.tr_close_list.Visible = False
            Me.cboTransList.Items.Clear()
        End If

    End Sub

    Private Sub DoSave()
        lblMessage.Text = ""
        Dim strMyVal As String

        strMyVal = RTrim(Me.txtCustID.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustID.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        strMyVal = RTrim(Me.txtCustModule.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustModule.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        strMyVal = RTrim(Me.txtCustClass.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustClass.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        strMyVal = RTrim(Me.txtCustNum.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            'Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustNum.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub
        End If

        If Trim(Me.txtCustName.Text) = "" Or RTrim(Me.txtCustName.Text) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustName.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtShortName.Text) = "" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblShortName.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            txtShortName.Focus()
            Exit Sub
        End If

        If LTrim(RTrim(Me.txtCustPhone01.Text)) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblCustPhone01.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        Else
            If IsNumeric(LTrim(RTrim(Me.txtCustPhone01.Text))) And Len(LTrim(RTrim(Me.txtCustPhone01.Text))) = 11 Then
            Else
                Me.lblMessage.Text = "Incorrect/Invalid " & Me.lblCustPhone01.Text
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Exit Sub
            End If
        End If

        If LTrim(RTrim(Me.txtCustPhone02.Text)) = "" Then
            'Me.lblMessage.Text = "Missing " & Me.lblCustPhone02.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub
        Else
            If IsNumeric(LTrim(RTrim(Me.txtCustPhone02.Text))) And Len(LTrim(RTrim(Me.txtCustPhone01.Text))) = 7 Then
            Else
                Me.lblMessage.Text = "Incorrect/Invalid " & Me.lblCustPhone02.Text
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Exit Sub
            End If
        End If


        If LTrim(RTrim(Me.txtCustEmail01.Text)) = "" Then
        Else
            blnStatus = gnParseEmail_Address(RTrim(Me.txtCustEmail01.Text))
            If blnStatus = False Then
                Me.lblMessage.Text = "Incorrect/Invalid " & Me.lblCustEmail01.Text
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Exit Sub

            End If
        End If

        If LTrim(RTrim(Me.txtCustEmail02.Text)) = "" Then
        Else
            blnStatus = gnParseEmail_Address(RTrim(Me.txtCustEmail02.Text))
            If blnStatus = False Then
                Me.lblMessage.Text = "Incorrect/Invalid " & Me.lblCustEmail02.Text
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Exit Sub

            End If
        End If


        If Trim(Me.txtCustAddr01.Text) = "" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustAddr01.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            txtCustAddr01.Focus()
            Exit Sub
        End If

        If Trim(Me.txtCustAddr02.Text) = "" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustAddr02.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            txtCustAddr02.Focus()
            Exit Sub
        End If



        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try


        Dim intC As Long = 0

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try

        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 TBIL_INSRD_CODE FROM " & strTable
        strSQL = strSQL & " WHERE RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,'')) = '" & Trim(Me.txtCustName.Text) & " " & Trim(Me.txtShortName.Text) & "'"
        strSQL = strSQL & " AND TBIL_INSRD_ID = '" & RTrim(Me.txtCustID.Text) & "'"

        Dim chk_objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        chk_objOLECmd.CommandType = CommandType.Text
        'chk_objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID
        Dim chk_objOLEDR As OleDbDataReader

        chk_objOLEDR = chk_objOLECmd.ExecuteReader()
        If (chk_objOLEDR.Read()) Then
            If Trim(Me.txtCustNum.Text) <> Trim(chk_objOLEDR("TBIL_INSRD_CODE") & vbNullString) Then
                Me.lblMessage.Text = "Warning!. The code description you enter already exist..." & _
                  "<br />Please check code: " & RTrim(chk_objOLEDR("TBIL_INSRD_CODE") & vbNullString)
                chk_objOLECmd = Nothing
                chk_objOLEDR = Nothing
                If objOLEConn.State = ConnectionState.Open Then
                    objOLEConn.Close()
                End If
                objOLEConn = Nothing
                Exit Sub
            End If
        End If

        chk_objOLECmd = Nothing
        chk_objOLEDR = Nothing


        Try
            'open connection to database
            objOLEConn.Close()
        Catch ex As Exception
            'Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Me.lblMessage.Text = ex.Message.ToString
            objOLEConn = Nothing
            Exit Sub
        End Try


        If Trim(Me.txtCustNum.Text) = "" Then

        End If

        If RTrim(txtCustNum.Text) = "" Then
            Me.txtCustNum.Text = MOD_GEN.gnGet_Serial_Und("GET_SN_IL_INS", Trim("INS"), Trim("INS"), "XXXX", "XXXX", "DC")
            If Trim(txtCustNum.Text) = "" Or Trim(Me.txtCustNum.Text) = "0" Or Trim(Me.txtCustNum.Text) = "*" Then
                Me.txtCustNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to get the next record id. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtCustNum.Text) = "PARAM_ERR" Then
                Me.txtCustNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to get the next record id - INVALID PARAMETER(S)"
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtCustNum.Text) = "DB_ERR" Then
                Me.txtCustNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to connect to database. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtCustNum.Text) = "ERR_ERR" Then
                Me.txtCustNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to get connection object. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            End If

        End If

        strREC_ID = Trim(Me.txtCustNum.Text)

        objOLEConn.ConnectionString = mystrCONN
        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            'Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try


        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_INSRD_CODE = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_INSRD_ID = '" & RTrim(Me.txtCustID.Text) & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        'or
        'objDA.SelectCommand = New System.Data.OleDb.OleDbCommand(strSQL, objOleConn)

        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        'Dim m_rwContact As System.Data.DataRow


        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '   Creating a new record

                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()

                drNewRow("TBIL_INSRD_ID") = RTrim(Me.txtCustID.Text)
                drNewRow("TBIL_INSRD_CODE") = RTrim(Me.txtCustNum.Text)

                drNewRow("TBIL_INSRD_MDLE") = RTrim(Me.txtCustModule.Text)
                drNewRow("TBIL_INSRD_CLASS") = RTrim(Me.txtCustClass.Text)

                drNewRow("TBIL_INSRD_SURNAME") = Left(RTrim(Me.txtCustName.Text), 49)
                drNewRow("TBIL_INSRD_FIRSTNAME") = Left(LTrim(Me.txtShortName.Text), 49)

                drNewRow("TBIL_INSRD_ADRES1") = Left(LTrim(Me.txtCustAddr01.Text), 39)
                drNewRow("TBIL_INSRD_ADRES2") = Left(LTrim(Me.txtCustAddr02.Text), 39)
                drNewRow("TBIL_INSRD_PHONE1") = Left(LTrim(Me.txtCustPhone01.Text), 11)
                drNewRow("TBIL_INSRD_PHONE2") = Left(LTrim(Me.txtCustPhone02.Text), 11)
                drNewRow("TBIL_INSRD_EMAIL1") = Left(LTrim(Me.txtCustEmail01.Text), 49)
                drNewRow("TBIL_INSRD_EMAIL2") = Left(LTrim(Me.txtCustEmail02.Text), 49)

                drNewRow("TBIL_INSRD_FLAG") = "A"
                drNewRow("TBIL_INSRD_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_INSRD_KEYDTE") = Now

                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                ' Assured code does not write to the chart of account.
                If Me.chkWrtToChart.Checked = True Then
                    'INSERTING INTO ACCOUNT CODES TABLE TBFN_ACCT_CODES
                    MainAcctDesc = ""
                    SubAcctCode = ""
                    SubCodeInitial = Left(Me.txtCustNum.Text, 2)
                    AcctLevel = "S"
                    MainGroup = ""
                    LedgerType = ""
                    Sub1Group = ""

                    MainAcctCode = hashHelper.GetMainAccountCode(cboCustClass.SelectedValue, mystrCONN)
                    Dim SubAcctcodeSuffix = Trim(txtCustNum.Text.Substring(2))
                    Dim SubAcctcodePrefix = Trim(txtCustNum.Text.Substring(0, 2))
                    'If SubCodeInitial = "BR" Then
                    '    'MainAcctCode = "1020080010"
                    '    MainAcctDesc = "TRADE RECEIVABLES - BROKERS"
                    'ElseIf SubCodeInitial = "AC" Then
                    '    'MainAcctCode = "1020080015"
                    '    MainAcctDesc = "TRADE RECEIVABLES - AGENTS"
                    'ElseIf SubCodeInitial = "DC" Then
                    '    'MainAcctCode = "1020080020"
                    '    MainAcctDesc = "TRADE RECEIVABLES - DIRECT CLIENTS"
                    'End If

                    If cboCustClass.SelectedItem.Text = "BROKER BUSINESS" Then
                        MainAcctDesc = "TRADE RECEIVABLES - BROKERS"
                        SubAcctCode = "BR" & SubAcctcodeSuffix
                    ElseIf cboCustClass.SelectedItem.Text = "AGENTS BUSINESS" Then
                        MainAcctDesc = "TRADE RECEIVABLES - AGENTS"
                        SubAcctCode = "AC" & SubAcctcodeSuffix
                    ElseIf cboCustClass.SelectedItem.Text = "DIRECT BUSINESS" Then
                        MainAcctDesc = "TRADE RECEIVABLES - DIRECT CLIENTS"
                        SubAcctCode = "DC" & SubAcctcodeSuffix
                    End If


                    hashHelper.InsertAcctChart("001", MainAcctCode, SubAcctCode, MainAcctDesc, Left(RTrim(Me.txtCustName.Text), 49), AcctLevel, _
                                             MainGroup, "", LedgerType, Sub1Group, "", "", "", "", "", "", "A", DateTime.Now, "001", mystrCONN)
                End If

                drNewRow = Nothing

                Me.lblMessage.Text = "New Record Saved to Database Successfully."

            Else
                '   Update existing record

                'm_rwContact = m_dtContacts.Rows(0)
                'm_rwContact("ContactName") = "Bob Brown"
                'm_rwContact.AcceptChanges()
                'm_dtContacts.AcceptChanges()
                'Dim intC As Integer = m_daDataAdapter.Update(m_dtContacts)


                With obj_DT
                    .Rows(0)("TBIL_INSRD_ID") = RTrim(Me.txtCustID.Text)
                    .Rows(0)("TBIL_INSRD_CODE") = RTrim(Me.txtCustNum.Text)

                    .Rows(0)("TBIL_INSRD_MDLE") = RTrim(Me.txtCustModule.Text)
                    .Rows(0)("TBIL_INSRD_CLASS") = RTrim(Me.txtCustClass.Text)

                    .Rows(0)("TBIL_INSRD_SURNAME") = UCase(Left(RTrim(Me.txtCustName.Text), 49))
                    .Rows(0)("TBIL_INSRD_FIRSTNAME") = UCase(Left(RTrim(Me.txtShortName.Text), 49))

                    .Rows(0)("TBIL_INSRD_ADRES1") = Left(LTrim(Me.txtCustAddr01.Text), 39)
                    .Rows(0)("TBIL_INSRD_ADRES2") = Left(LTrim(Me.txtCustAddr02.Text), 39)
                    .Rows(0)("TBIL_INSRD_PHONE1") = Left(LTrim(Me.txtCustPhone01.Text), 11)
                    .Rows(0)("TBIL_INSRD_PHONE2") = Left(LTrim(Me.txtCustPhone02.Text), 11)
                    .Rows(0)("TBIL_INSRD_EMAIL1") = Left(LTrim(Me.txtCustEmail01.Text), 49)
                    .Rows(0)("TBIL_INSRD_EMAIL2") = Left(LTrim(Me.txtCustEmail02.Text), 49)

                    .Rows(0)("TBIL_INSRD_FLAG") = "C"
                    '.Rows(0)("TBIL_INSRD_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_INSRD_KEYDTE") = Now
                End With

                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                Me.lblMessage.Text = "Record Saved to Database Successfully."

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

        m_cbCommandBuilder.Dispose()
        m_cbCommandBuilder = Nothing

        obj_DT.Dispose()
        obj_DT = Nothing

        If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
            objDA.SelectCommand.Connection.Close()
        End If
        objDA.Dispose()
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        'Me.lblMessage.Text = ""

        Me.txtSearch.Value = RTrim(Me.txtCustName.Text)

        'Call Proc_Populate_Box("IL_INS_DETAIL_LIST", Trim(Me.txtCustID.Text), Me.cboTransList)
        Call Proc_DataBind()
        Me.txtSearch.Value = ""

        DoNew()

        Me.txtCustName.Enabled = True
        Me.txtCustName.Focus()

    End Sub

    Protected Sub DoDelete()

        Dim strMyVal As String
        strMyVal = RTrim(Me.txtCustID.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustID.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtCustNum.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblCustNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0

        strTable = strTableName

        strREC_ID = Trim(Me.txtCustNum.Text)

        strSQL = "SELECT TOP 1 TBIL_INSRD_CODE FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_INSRD_CODE = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_INSRD_ID = '" & RTrim(Me.txtCustID.Text) & "'"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        'open connection to database
        objOLEConn.Open()

        strOPT = "NEW"
        FirstMsg = ""

        Dim objOLEDR As OleDbDataReader = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strOPT = "OLD"
        End If

        ' dispose of open objects
        objOLECmd.Dispose()
        objOLECmd = Nothing

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If
        objOLEDR = Nothing

        Select Case RTrim(strOPT)
            Case "OLD"
                'Delete record
                'Me.lblMessage.Text = "Deleting record... "
                strSQL = ""
                strSQL = "DELETE FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_INSRD_CODE = '" & RTrim(strREC_ID) & "'"
                strSQL = strSQL & " AND TBIL_INSRD_ID = '" & RTrim(Me.txtCustID.Text) & "'"

                Dim objOLECmd2 As OleDbCommand = New OleDbCommand()
                objOLECmd2.Connection = objOLEConn
                objOLECmd2.CommandType = CommandType.Text
                objOLECmd2.CommandText = strSQL
                intC = objOLECmd2.ExecuteNonQuery()
                objOLECmd2.Dispose()
                objOLECmd2 = Nothing
            Case Else
        End Select

        'Try
        'Catch ex As Exception
        'End Try

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        If intC >= 1 Then
            Me.lblMessage.Text = "Record deleted successfully."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        Else
            Me.lblMessage.Text = "Sorry!. Record not deleted..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        End If
        'Me.lblMessage.Text = ""

        'Call Proc_Populate_Box("IL_INS_DETAIL_LIST", Trim(Me.txtCustID.Text), Me.cboTransList)
        'Call Proc_DataBind()

        Me.cmdDelete_ASP.Enabled = False

        Call DoNew()
        Me.txtCustName.Enabled = True
        Me.txtCustName.Focus()

    End Sub

    Protected Sub cboTransList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTransList.SelectedIndexChanged
        Me.txtCustNum.Text = RTrim(Me.cboTransList.SelectedItem.Value)
        If RTrim(Me.txtCustNum.Text) = "*" Or RTrim(Me.txtCustNum.Text) = "" Or RTrim(Me.txtCustNum.Text) = "0" Then
            Me.txtCustNum.Text = ""
            Call DoNew()
            Exit Sub
        End If

        If RTrim(Me.txtCustNum.Text) <> "" Then
            lblMessage.Text = RTrim(Me.txtCustNum.Text)
            strREC_ID = RTrim(Me.txtCustNum.Text)
            strErrMsg = Proc_OpenRecord(Me.txtCustNum.Text)
        End If

    End Sub

    Protected Sub cboGLACCCODE_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGLAccCode.TextChanged
        Me.txtGLAccCode.Text = RTrim(Me.cboGLAccCode.SelectedItem.Value)
        If RTrim(Me.txtGLAccCode.Text) = "*" Then
            Me.txtGLAccCode.Text = ""
            Exit Sub
        End If

    End Sub

    Private Function Proc_OpenRecord(ByVal strRefNo As String) As String

        On Error GoTo myRtn_Err

        strErrMsg = "false"

        lblMessage.Text = ""
        If Trim(strRefNo) = "" Then
            Proc_OpenRecord = strErrMsg
            Return Proc_OpenRecord
        End If

        strREC_ID = Trim(strRefNo)

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 TRN.*"
        strSQL = strSQL & " FROM " & strTable & " AS TRN"
        strSQL = strSQL & " WHERE TRN.TBIL_INSRD_CODE = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TRN.TBIL_INSRD_ID = '" & RTrim(Me.txtCustID.Text) & "'"
        'strSQL = strSQL & " AND TBIL_INSRD_CLASS_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Dim objOLEDR As OleDbDataReader

        'open connection to database
        objOLEConn.Open()

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_INSRD_REC_ID") & vbNullString, String))
            Me.txtCustID.Text = RTrim(CType(objOLEDR("TBIL_INSRD_ID") & vbNullString, String))

            Me.txtCustModule.Text = RTrim(CType(objOLEDR("TBIL_INSRD_MDLE") & vbNullString, String))
            Call Proc_DDL_Get(Me.cboCustModule, RTrim(Me.txtCustModule.Text))
            Me.txtCustClass.Text = RTrim(CType(objOLEDR("TBIL_INSRD_CLASS") & vbNullString, String))
            Call Proc_DDL_Get(Me.cboCustClass, RTrim(Me.txtCustClass.Text))

            Me.txtCustNum.Text = RTrim(CType(objOLEDR("TBIL_INSRD_CODE") & vbNullString, String))
            Me.txtCustName.Text = RTrim(CType(objOLEDR("TBIL_INSRD_SURNAME") & vbNullString, String))
            Me.txtShortName.Text = RTrim(CType(objOLEDR("TBIL_INSRD_FIRSTNAME") & vbNullString, String))

            Me.txtCustAddr01.Text = RTrim(CType(objOLEDR("TBIL_INSRD_ADRES1") & vbNullString, String))
            Me.txtCustAddr02.Text = RTrim(CType(objOLEDR("TBIL_INSRD_ADRES2") & vbNullString, String))
            Me.txtCustPhone01.Text = RTrim(CType(objOLEDR("TBIL_INSRD_PHONE1") & vbNullString, String))
            Me.txtCustPhone02.Text = RTrim(CType(objOLEDR("TBIL_INSRD_PHONE2") & vbNullString, String))
            Me.txtCustEmail01.Text = RTrim(CType(objOLEDR("TBIL_INSRD_EMAIL1") & vbNullString, String))
            Me.txtCustEmail02.Text = RTrim(CType(objOLEDR("TBIL_INSRD_EMAIL2") & vbNullString, String))

            'Me.txtGLAccCode.Text = RTrim(CType(objOLEDR("TBIL_CUST_CAT_CNTRL_ACCT") & vbNullString, String))
            'Call Proc_DDL_Get(Me.cboGLAccCode, RTrim(Me.txtGLAccCode.Text))


            Call DisableBox(Me.txtCustNum)
            strErrMsg = "Status: Data Modification"
            strOPT = "1"
            Me.cmdNew_ASP.Enabled = True
            Me.cmdDelete_ASP.Enabled = True
        Else
            'Me.txtCustNum.Text = ""
            Me.cmdDelete_ASP.Enabled = False
            strErrMsg = "Status: New Entry..."
            Me.txtCustName.Enabled = True
            Me.txtCustName.Focus()
        End If

        ' dispose of open objects
        objOLECmd.Dispose()

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If

        GoTo MyRtn_Ok

myRtn_Err:
        strErrMsg = Err.Number & " - " & Err.Description
MyRtn_Ok:

        objOLECmd = Nothing
        objOLEDR = Nothing
        objOLEConn = Nothing

        lblMessage.Text = strErrMsg
        Proc_OpenRecord = strErrMsg
        Return Proc_OpenRecord

    End Function

    Private Sub DisableBox(ByVal objTxtBox As TextBox)
        Dim c As System.Drawing.Color = Drawing.Color.LightGray
        objTxtBox.ReadOnly = True
        objTxtBox.Enabled = False
        'objTxtBox.BackColor = c

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

    Private Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        'Dim row As GridViewRow = GridView1.Rows(e.NewSelectedIndex)

        GridView1.PageIndex = e.NewPageIndex
        Call Proc_DataBind()
        Me.lblMessage.Text = "Page " & GridView1.PageIndex + 1 & " of " & Me.GridView1.PageCount

    End Sub

    Private Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = GridView1.SelectedRow

        ' Display the required value from the selected row.
        Me.txtRecNo.Text = row.Cells(2).Text

        Me.txtCustID.Text = row.Cells(3).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))

        Me.txtCustNum.Text = row.Cells(4).Text
        Call Proc_DDL_Get(Me.cboTransList, RTrim(Me.txtCustNum.Text))

        Call Proc_OpenRecord(Me.txtCustNum.Text)

        lblMessage.Text = "You selected " & Me.txtCustNum.Text & " / " & Me.txtRecNo.Text & "."

    End Sub

    Private Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If Trim(Me.txtSearch.Value) = "" Or Trim(Me.txtSearch.Value) = "." Or Trim(Me.txtSearch.Value) = "*" Then
        Else
            Call Proc_DataBind()
        End If
    End Sub

    Private Sub cboCustModule_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCustModule.SelectedIndexChanged
        Me.txtCustModule.Text = RTrim(Me.cboCustModule.SelectedItem.Value)
        If RTrim(Me.txtCustModule.Text) = "*" Or RTrim(Me.txtCustModule.Text) = "" Or RTrim(Me.txtCustModule.Text) = "0" Then
            Me.txtCustModule.Text = ""
            Exit Sub
        End If

    End Sub

    Private Sub cboCustClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCustClass.SelectedIndexChanged
        Me.txtCustClass.Text = RTrim(Me.cboCustClass.SelectedItem.Value)
        If RTrim(Me.txtCustClass.Text) = "*" Or RTrim(Me.txtCustClass.Text) = "" Or RTrim(Me.txtCustClass.Text) = "0" Then
            Me.txtCustClass.Text = ""
            Exit Sub
        End If

    End Sub

    Protected Sub chkNum_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkNum.CheckedChanged
        If Me.chkNum.Checked = True Then
            Me.txtCustNum.Enabled = False
            Me.cboTransList.Enabled = False
        Else
            Me.txtCustNum.Enabled = True
            Me.cboTransList.Enabled = True
        End If
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        Me.txtRecNo.Text = ""
        Me.txtCustID.Text = ""
        Me.txtCustClass.Text = ""
        Me.txtCustNum.Text = ""
        Me.txtCustName.Text = ""
        Me.txtShortName.Text = ""
        Me.txtCustAddr01.Text = ""
        Me.txtCustAddr02.Text = ""
        Me.txtCustPhone01.Text = ""
        Me.txtCustPhone02.Text = ""
        Me.txtCustEmail01.Text = ""
        Me.txtCustEmail02.Text = ""
        cboCustModule.SelectedIndex = 0
        cboCustClass.SelectedIndex = 0
        cboTransList.SelectedIndex = 0
    End Sub
End Class
