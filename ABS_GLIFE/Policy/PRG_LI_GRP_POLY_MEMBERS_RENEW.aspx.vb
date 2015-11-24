
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
                Me.cmdSave_ASP.Enabled = True
            Case "U"
                'tr_file_upload.Visible = True
                Me.cmdSave_ASP.Enabled = False
            Case Else
                'tr_file_upload.Visible = False
                Me.cmdFile_Upload.Enabled = False
                Me.cmdSave_ASP.Enabled = False
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




End Class
