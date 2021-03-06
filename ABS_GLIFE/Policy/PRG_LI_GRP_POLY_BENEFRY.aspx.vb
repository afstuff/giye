﻿Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class Policy_PRG_LI_GRP_POLY_BENEFRY
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String
    'Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    'Protected BufferStr As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strF_ID As String
    Protected strP_ID As String
    Protected strQ_ID As String
    Protected strM_NO As String
    Protected strR_ID As String

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

    Dim strErrMsg As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "TBIL_GRP_POLICY_BENEFRY"
        STRMENU_TITLE = "Add Beneficiary"


        Try
            strF_ID = CType(Request.QueryString("optfileid"), String)
            strF_ID = CType(Session("optfileid"), String)
        Catch ex As Exception
            strF_ID = ""
        End Try

        Try
            strQ_ID = CType(Request.QueryString("optquotid"), String)
            strQ_ID = CType(Session("optquotid"), String)
        Catch ex As Exception
            strQ_ID = ""
        End Try

        Try
            strP_ID = CType(Request.QueryString("optpolid"), String)
            strP_ID = CType(Session("optpolid"), String)
        Catch ex As Exception
            strP_ID = ""
        End Try

        Try
            strM_NO = CType(Request.QueryString("optmemno"), String)
            strM_NO = CType(Session("optmemno"), String)
        Catch ex As Exception
            strM_NO = ""
        End Try




        If Not (Page.IsPostBack) Then
            Call Proc_DoNew()

            Me.lblMsg.Text = "Status:"
            Me.cmdPrev.Enabled = True
            Me.cmdNext.Enabled = False

            Call gnProc_Populate_Box("IL_CODE_LIST", "013", Me.cboBenef_Relationship)

            If Trim(strF_ID) <> "" Then
                Me.txtFileNum.Text = RTrim(strF_ID)
                Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_GL_POLICY_BY_FILE_NO", RTrim(strF_ID), RTrim(""), RTrim(""))
                If oAL.Item(0) = "TRUE" Then
                    '    'Retrieve the record
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
                    Me.txtQuote_Num.Text = oAL.Item(3)
                    Me.txtPolNum.Text = oAL.Item(4)
                    Me.txtProductClass.Text = oAL.Item(5)
                    Me.txtProduct_Num.Text = oAL.Item(6)
                    txtMemberNo.Text = strM_NO
                    Me.cmdNext.Enabled = True

                    Select Case Trim(Me.txtProduct_Num.Text)
                        Case "F001", "F002"
                            Call gnProc_Populate_Box("IL_FUNERAL_LIST", Me.txtFileNum.Text, Me.cboBenef_Cover_ID, " AND TBIL_FUN_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'")
                            Me.lblBenef_Cover_ID.Enabled = True
                            Me.cboBenef_Cover_ID.Enabled = True
                            Me.txtBenef_Cover_ID.Enabled = True
                            Me.txtBenef_Cover_ID.Text = "0"
                            'Me.txtBenef_Cover_ID.Text = oAL.Item(11)
                        Case Else
                            Me.cboBenef_Cover_ID.Items.Clear()
                            Me.lblBenef_Cover_ID.Enabled = False
                            Me.cboBenef_Cover_ID.Enabled = False
                            Me.txtBenef_Cover_ID.Enabled = False
                            Me.txtBenef_Cover_ID.Text = "0"

                    End Select

                    If UCase(oAL.Item(18).ToString) = "A" Then
                        'Me.cmdNew_ASP.Visible = False
                        'Me.cmdSave_ASP.Visible = False
                        'Me.cmdDelete_ASP.Visible = False
                        'Me.cmdDelItem_ASP.Visible = False
                        Me.cmdPrint_ASP.Visible = False
                    End If

                    Call Proc_DataBind()
                Else
                    '    'Destroy i.e remove the array list object from memory
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    Me.lblMsg.Text = "Status: " & oAL.Item(1)
                End If
                oAL = Nothing
            End If

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

        If Me.txtAction.Text = "Delete_Item" Then
            Call Proc_DoDelItem()
            Me.txtAction.Text = ""
        End If

    End Sub
    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call Proc_DoSave()
        Me.txtAction.Text = ""
    End Sub
    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT *"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " WHERE TBIL_POL_BENF_FILE_NO = '" & RTrim(strF_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_BENF_PROP_NO = '" & RTrim(strQ_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_BENF_MEMBER_NO = '" & RTrim(strM_NO) & "'"
        strSQL = strSQL & " ORDER BY TBIL_POL_BENF_COVER_ID, TBIL_POL_BENF_SNO"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            objOLEConn = Nothing
        End Try

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
        If C >= 1 Then
            'Me.cmdDelete_ASP.Enabled = True
            Me.cmdDelItem_ASP.Enabled = True
        End If

        'C = C + 1
        'Me.txtBenef_SN.Text = C.ToString

    End Sub

    Protected Sub DoProc_Cover_ID_Change()
        Call gnGET_SelectedItem(Me.cboBenef_Cover_ID, Me.txtBenef_Cover_ID, Me.txtBenef_Cover_ID_Name, Me.lblMsg)

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
        strSQL = ""
        strSQL = "DELETE FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_BENF_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_BENF_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"

        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try
            objOLECmd2.Connection = objOLEConn
            objOLECmd2.CommandType = CommandType.Text
            objOLECmd2.CommandText = strSQL
            intC = objOLECmd2.ExecuteNonQuery()

            If intC >= 1 Then
                Call Proc_DoNew()
                Me.lblMsg.Text = "Record deleted successfully."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Else
                Me.lblMsg.Text = "Sorry!. Record not deleted..."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"

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


    Protected Sub Proc_DoDelItem()

        Dim blnRet As Boolean = False
        Dim P As Integer = 0, C As Integer
        Dim myKey As String = "", myKeyX As String = ""


        For P = 0 To Me.GridView1.Rows.Count - 1
            If CType(Me.GridView1.Rows(P).FindControl("chkSel"), CheckBox).Checked Then
                ' Get the currently selected row using the SelectedRow property.
                Dim row As GridViewRow = GridView1.Rows(P)
                myKeyX = myKeyX & row.Cells(2).Text
                myKeyX = myKeyX & " / "

                myKey = Me.GridView1.Rows(P).Cells(2).Text
                'Me.txtNum.Text = Me.GridView1.Rows(P).Cells(4).Text


                ' Display the required value from the selected row.
                'Me.txtRecNo.Text = row.Cells(2).Text


                'Insert codes to delete selected/checked item(s)

                If Trim(myKey) <> "" Then
                    Me.txtRecNo.Text = myKey
                    Call Proc_DoDelete_Record()
                    C = C + 1
                End If

            End If

        Next

        If C >= 1 Then
            'Me.cmdDelItem_ASP.Enabled = False
            'Me.cmdDelItem.Enabled = False

            Call Proc_DataBind()

            Call Proc_DoNew()

            Me.lblMsg.Text = "Record deleted successfully." & " No of item(s) deleted: " & CStr(C)
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            'Me.textMessage.Text = ""

            Me.lblMsg.Text = "Deleted Item(s): " & myKeyX

        Else
            Me.lblMsg.Text = "Record not deleted ..."

        End If

        'Me.txtTreatyNum.Enabled = True
        'Me.txtTreatyNum.Focus()

    End Sub

    Protected Sub Proc_DoDelete_Record()

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

        If Trim(Me.txtRecNo.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblRecNo.Text
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


        strREC_ID = Trim(Me.txtFileNum.Text)
        strTable = strTableName

        strSQL = ""
        'Delete record
        '==============================================
        strSQL = ""
        strSQL = "DELETE FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_BENF_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_BENF_PROP_NO = '" & RTrim(txtQuote_Num.Text) & "'"
        strSQL = strSQL & " AND TBIL_POL_BENF_REC_ID = " & Val(RTrim(Me.txtRecNo.Text)) & ""

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
                'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Else
                'Me.lblMsg.Text = "Sorry!. Record not deleted..."
                'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
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
                           subctrl.ID = "txtMemberNo" Or _
                           subctrl.ID = "cboBenef_Cover_ID" Or _
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
                        CType(subctrl, DropDownList).SelectedIndex = -1
                    End If
                Next
            End If
        Next

        'Me.chkFileNum.Enabled = True
        'Me.chkFileNum.Checked = False
        'Me.lblFileNum.Enabled = False
        'Me.txtFileNum.Enabled = False
        'Me.cmdFileNum.Enabled = False

        Me.txtRecNo.Text = "0"

        'Me.cboProductClass.SelectedIndex = -1
        'Me.cboProduct.SelectedIndex = -1
        'Me.cboCover_Name.SelectedIndex = -1
        'Me.cboPlan_Name.SelectedIndex = -1

        'Me.txtProduct_Num.Text = ""

        Me.cmdSave_ASP.Enabled = True
        'Me.cmdDelItem_ASP.Enabled = False
        'Me.cmdNext.Enabled = False

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


        Dim myTmp_RecStatus = CType(Session("myTmp_RecStatus"), String)


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

        'VALIDATION for DATA NOT OLD START
        If myTmp_RecStatus <> "OLD" Then

            Select Case Trim(Me.txtProduct_Num.Text)
                Case "F001", "F002"
                    If Trim(txtBenef_Cover_ID.Text) = "" Or Val(Me.txtBenef_Cover_ID.Text) = 0 Then
                        Me.lblMsg.Text = "Missing Cover ID for this product. Please contact your service provider..."
                        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                        'Me.lblMsg.Text = "Status:"
                    End If
                Case Else
                    Me.txtBenef_Cover_ID.Text = "0"
            End Select

            Call MOD_GEN.gnGET_SelectedItem(Me.cboBenef_Type, Me.txtBenef_Type, Me.txtBenef_TypeName, Me.lblMsg)
            If Trim(Me.txtBenef_Type.Text) = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblBenef_Type.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            Call MOD_GEN.gnGET_SelectedItem(Me.cboBenef_Category, Me.txtBenef_Category, Me.txtBenef_CategoryName, Me.lblMsg)
            If Trim(Me.txtBenef_Category.Text) = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblBenef_Category.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            If Trim(Me.txtBenef_Name.Text) = "" Or Trim(Me.txtBenef_Name.Text) = "." Or Trim(Me.txtBenef_Name.Text) = "*" Then
                Me.lblMsg.Text = "Missing or invalid " & Me.lblBenef_Name.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            Call MOD_GEN.gnInitialize_Numeric(Me.txtBenef_Percentage)
            If Val(Me.txtBenef_Percentage.Text) <= 0 Then
                Me.lblMsg.Text = "Missing or invalid " & Me.lblBenef_Percentage.Text
                'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'Exit Sub
            End If

            If Trim(Me.txtBnkAcctNo.Text) = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblBnkAcctNo.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            If Trim(Me.txtBnkName_Address.Text) = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblBnkName.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If


            Me.txtBenef_DOB.Text = Trim(Me.txtBenef_DOB.Text)
            If RTrim(Me.txtBenef_DOB.Text) = "" Then
                'Me.txtBenef_Age.Text = "0"
                GoTo Proc_Skip_ANB
            End If
            If RTrim(Me.txtBenef_DOB.Text) = "" Or Len(Trim(Me.txtBenef_DOB.Text)) <> 10 Then
                Me.lblMsg.Text = "Missing or Invalid date - " & Me.lblBenef_DOB.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            'Validate date
            myarrData = Split(Me.txtBenef_DOB.Text, "/")
            If myarrData.Count <> 3 Then
                Me.lblMsg.Text = "Missing or Invalid " & Me.lblBenef_DOB.Text & ". Expecting full date in ddmmyyyy format ..."
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
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
                Exit Sub
            End If
            Me.txtBenef_DOB.Text = RTrim(strMyDte)
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

            If Dte_Current.Month > Dte_DOB.Month Then
                lngDOB_ANB = lngDOB_ANB + 1
            End If
            Me.txtBenef_Age.Text = Trim(Str(lngDOB_ANB))

        End If 'VALIDATION for DATA NOT OLD END
Proc_Skip_ANB:


        Call MOD_GEN.gnGET_SelectedItem(Me.cboBenef_Relationship, Me.txtBenef_Relationship, Me.txtBenef_RelationshipName, Me.lblMsg)
        If Trim(Me.txtBenef_Relationship.Text) = "" Or Trim(Me.txtBenef_Relationship.Text) = "." Or Trim(Me.txtBenef_Relationship.Text) = "*" Then
            Me.lblMsg.Text = "Missing or invalid " & Me.lblBenef_Relationship.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtBenef_Address.Text) = "" Or Trim(Me.txtBenef_Address.Text) = "." Or Trim(Me.txtBenef_Address.Text) = "*" Then
            Me.lblMsg.Text = "Missing or invalid " & Me.lblBenef_Address.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtBenef_GuardianName.Text) = "" Or Trim(Me.txtBenef_GuardianName.Text) = "." Or Trim(Me.txtBenef_GuardianName.Text) = "*" Then
            Me.lblMsg.Text = "Missing or invalid " & Me.lblBenef_GuardianName.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If



        'If Trim(txtBenef_Cover_ID.Text) = "" Then
        '    Me.txtBenef_Cover_ID.Text = MOD_GEN.gnGet_Serial_No(RTrim("GET_SN_IL"), RTrim("FUN_COVER_SN"), Trim(Me.txtFileNum.Text), Trim(Me.txtQuote_Num.Text))
        'End If

        If Trim(txtBenef_SN.Text) = "" Then
            'Me.txtBenef_SN.Text = "0"
        End If

        If Trim(txtBenef_SN.Text) = "" Then
            Me.txtBenef_SN.Text = MOD_GEN.gnGet_Serial_No(RTrim("GET_SN_IL"), RTrim("BENEF_SN"), Trim(Me.txtFileNum.Text), Trim(Me.txtQuote_Num.Text))
        End If


        'Me.lblMsg.Text = "About to submit data... "
        'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

        'If RTrim(txtNum.Text) = "" Then
        '    Me.txtNum.Text = MOD_GEN.gnGet_Serial_Und("GET_SN_IL_UNDW", Trim(strP_ID), Trim(Me.txtGroupNum.Text), "XXXX", "XXXX", "")
        '    If Trim(txtNum.Text) = "" Or Trim(Me.txtNum.Text) = "0" Or Trim(Me.txtNum.Text) = "*" Then
        '        Me.txtNum.Text = ""
        '        Me.lblMessage.Text = "Sorry!. Unable to get the next record id. Please contact your service provider..."
        '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        '        Me.lblMessage.Text = "Status:"
        '        Exit Sub
        '    ElseIf Trim(Me.txtNum.Text) = "PARAM_ERR" Then
        '        Me.txtNum.Text = ""
        '        Me.lblMessage.Text = "Sorry!. Unable to get the next record id - INVALID PARAMETER(S) - " & Trim(strP_ID)
        '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        '        Me.lblMessage.Text = "Status:"
        '        Exit Sub
        '    ElseIf Trim(Me.txtNum.Text) = "DB_ERR" Then
        '        Me.txtNum.Text = ""
        '        Me.lblMessage.Text = "Sorry!. Unable to connect to database. Please contact your service provider..."
        '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        '        Me.lblMessage.Text = "Status:"
        '        Exit Sub
        '    ElseIf Trim(Me.txtNum.Text) = "ERR_ERR" Then
        '        Me.txtNum.Text = ""
        '        Me.lblMessage.Text = "Sorry!. Unable to get connection object. Please contact your service provider..."
        '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        '        Me.lblMessage.Text = "Status:"
        '        Exit Sub
        '    End If

        'End If


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
        strSQL = strSQL & " WHERE TBIL_POL_BENF_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
        'strSQL = strSQL & " AND TBIL_POL_BENF_PROP_NO = '" & RTrim(txtQuote_Num.Text) & "'"
        'If Val(LTrim(RTrim(Me.txtRecNo.Text))) <> 0 Then
        strSQL = strSQL & " AND TBIL_POL_BENF_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
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

                drNewRow("TBIL_POL_BENF_MDLE") = RTrim("G")

                drNewRow("TBIL_POL_BENF_FILE_NO") = RTrim(Me.txtFileNum.Text)
                drNewRow("TBIL_POL_BENF_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                drNewRow("TBIL_POL_BENF_POLY_NO") = RTrim(Me.txtPolNum.Text) 'uncommented by james

                drNewRow("TBIL_POL_BENF_COVER_ID") = Val(Me.txtBenef_Cover_ID.Text)

                drNewRow("TBIL_POL_BENF_SNO") = Val(Me.txtBenef_SN.Text)
                drNewRow("TBIL_POL_BENF_TYPE") = RTrim(Me.txtBenef_Type.Text)
                drNewRow("TBIL_POL_BENF_CAT") = RTrim(Me.txtBenef_Category.Text)

                If Trim(Me.txtBenef_DOB.Text) <> "" Then
                    drNewRow("TBIL_POL_BENF_BDATE") = dteDOB
                End If

                drNewRow("TBIL_POL_BENF_AGE") = Val(Me.txtBenef_Age.Text)
                drNewRow("TBIL_POL_BENF_RELATN_CD") = RTrim(Me.txtBenef_Relationship.Text)
                drNewRow("TBIL_POL_BENF_NAME") = RTrim(Me.txtBenef_Name.Text)

                drNewRow("TBIL_POL_BENF_PCENT") = Val(Me.txtBenef_Percentage.Text)
                drNewRow("TBIL_POL_BENF_GURDN_NM") = RTrim(Me.txtBenef_GuardianName.Text)
                drNewRow("TBIL_POL_BENF_ADRESS") = Trim(Me.txtBenef_Address.Text)
                drNewRow("TBIL_POL_BENF_MEMBER_NO") = Trim(Me.txtMemberNo.Text)
                drNewRow("TBIL_POL_BENF_BNK_ACCT_NO") = Trim(Me.txtBnkAcctNo.Text)
                drNewRow("TBIL_POL_BENF_BNK_NAME_ADD") = Trim(Me.txtBnkName_Address.Text)
                drNewRow("TBIL_POL_BENF_BNK_SORT_CODE") = Trim(Me.txtBnkSortCode.Text)
                drNewRow("TBIL_POL_BENF_FLAG") = "A"
                drNewRow("TBIL_POL_BENF_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_POL_BENF_KEYDTE") = Now

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
                    .Rows(0)("TBIL_POL_BENF_FILE_NO") = RTrim(Me.txtFileNum.Text)
                    .Rows(0)("TBIL_POL_BENF_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                    .Rows(0)("TBIL_POL_BENF_POLY_NO") = RTrim(Me.txtPolNum.Text) 'uncommented by james

                    .Rows(0)("TBIL_POL_BENF_COVER_ID") = Val(Me.txtBenef_Cover_ID.Text)

                    .Rows(0)("TBIL_POL_BENF_SNO") = Val(Me.txtBenef_SN.Text)
                    .Rows(0)("TBIL_POL_BENF_TYPE") = RTrim(Me.txtBenef_Type.Text)
                    .Rows(0)("TBIL_POL_BENF_CAT") = RTrim(Me.txtBenef_Category.Text)

                    If Trim(Me.txtBenef_DOB.Text) <> "" Then
                        .Rows(0)("TBIL_POL_BENF_BDATE") = dteDOB
                    End If

                    .Rows(0)("TBIL_POL_BENF_AGE") = Val(Me.txtBenef_Age.Text)
                    .Rows(0)("TBIL_POL_BENF_RELATN_CD") = RTrim(Me.txtBenef_Relationship.Text)
                    .Rows(0)("TBIL_POL_BENF_NAME") = RTrim(Me.txtBenef_Name.Text)

                    .Rows(0)("TBIL_POL_BENF_PCENT") = Val(Me.txtBenef_Percentage.Text)
                    .Rows(0)("TBIL_POL_BENF_GURDN_NM") = RTrim(Me.txtBenef_GuardianName.Text)
                    .Rows(0)("TBIL_POL_BENF_ADRESS") = Trim(Me.txtBenef_Address.Text)
                    .Rows(0)("TBIL_POL_BENF_MEMBER_NO") = Trim(Me.txtMemberNo.Text)
                    .Rows(0)("TBIL_POL_BENF_BNK_ACCT_NO") = Trim(Me.txtBnkAcctNo.Text)
                    .Rows(0)("TBIL_POL_BENF_BNK_NAME_ADD") = Trim(Me.txtBnkName_Address.Text)
                    .Rows(0)("TBIL_POL_BENF_BNK_SORT_CODE") = Trim(Me.txtBnkSortCode.Text)

                    .Rows(0)("TBIL_POL_BENF_FLAG") = "C"
                    '.Rows(0)("TBIL_POL_BENF_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_POL_BENF_KEYDTE") = Now
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

        Me.cmdNext.Enabled = True


        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"

        Call Proc_DataBind()
        Call Proc_DoNew()


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
        strSQL = strSQL & "SELECT TOP 1 BEN_TBL.*"
        strSQL = strSQL & " FROM " & strTable & " AS BEN_TBL"
        strSQL = strSQL & " WHERE BEN_TBL.TBIL_POL_BENF_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND BEN_TBL.TBIL_POL_BENF_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If
        'strSQL = strSQL & " AND PT.TBIL_POLY_PROPSAL_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND PT.TBIL_POLY_POLICY_NO = '" & RTrim(strP_ID) & "'"

        strSQL = "SPGL_GET_POLICY_BENEFRY"

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
            strErrMsg = "true"

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_FILE_NO") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_REC_ID") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_PROP_NO") & vbNullString, String))
            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_POLY_NO") & vbNullString, String))

            Me.txtBenef_Cover_ID.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_COVER_ID") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboBenef_Cover_ID, RTrim(Me.txtBenef_Cover_ID.Text))

            Me.txtBenef_SN.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_SNO") & vbNullString, String))

            Me.txtBenef_Type.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_TYPE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboBenef_Type, RTrim(Me.txtBenef_Type.Text))

            Me.txtBenef_Category.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_CAT") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboBenef_Category, RTrim(Me.txtBenef_Category.Text))

            Me.txtBenef_Name.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_NAME") & vbNullString, String))
            Me.txtBenef_Percentage.Text = Val(objOLEDR("TBIL_POL_BENF_PCENT") & vbNullString)

            If IsDate(objOLEDR("TBIL_POL_BENF_BDATE")) Then
                Me.txtBenef_DOB.Text = Format(CType(objOLEDR("TBIL_POL_BENF_BDATE"), DateTime), "dd/MM/yyyy")
            End If
            Me.txtBenef_Age.Text = Val(objOLEDR("TBIL_POL_BENF_AGE") & vbNullString)

            Me.txtBenef_Relationship.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_RELATN_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboBenef_Relationship, RTrim(Me.txtBenef_Relationship.Text))

            Me.txtBenef_Address.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_ADRESS") & vbNullString, String))
            Me.txtBenef_GuardianName.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_GURDN_NM") & vbNullString, String))
            Me.txtBnkAcctNo.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_BNK_ACCT_NO") & vbNullString, String))
            Me.txtBnkName_Address.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_BNK_NAME_ADD") & vbNullString, String))
            Me.txtBnkSortCode.Text = RTrim(CType(objOLEDR("TBIL_POL_BENF_BNK_SORT_CODE") & vbNullString, String))


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

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        'Dim row As GridViewRow = GridView1.Rows(e.NewSelectedIndex)

        GridView1.PageIndex = e.NewPageIndex
        Call Proc_DataBind()
        lblMsg.Text = "Page " & GridView1.PageIndex + 1 & " of " & Me.GridView1.PageCount
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
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

    Protected Sub cmdPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        Session("optfileid") = Trim(Me.txtFileNum.Text).ToString
        Session("optquotid") = Trim(Me.txtQuote_Num.Text).ToString
        Session("optpolid") = Trim(Me.txtPolNum.Text).ToString
        Session("optmemno") = Trim(Me.txtMemberNo.Text).ToString
        'Session("optrecid") = Trim(Me.txtRecNo.Text).ToString
        Dim pvURL As String = ""
        'pvURL = "prg_li_grp_poly_members.aspx?q=x"
        pvURL = "~/Claims/PRG_LI_GRP_CLM_ENTRY.aspx?q=x"
        Response.Redirect(pvURL)
        'Response.Redirect("~/Claims/PRG_LI_GRP_CLM_ENTRY")
    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        'Dim pvURL As String = "prg_li_indv_poly_add_cover.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        'pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        'pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        'Response.Redirect(pvURL)
    End Sub
End Class
