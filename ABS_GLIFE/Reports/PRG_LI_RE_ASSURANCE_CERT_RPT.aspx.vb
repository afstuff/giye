Imports System.Data.OleDb
Imports System.Data
Partial Class Reports_PRG_LI_RE_ASSURANCE_CERT_RPT
    Inherits System.Web.UI.Page
    Dim ErrorInd As String
    Protected FirstMsg As String
    Protected STRMENU_TITLE As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean
    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String
    Dim rParams As String() = {"nw", "nw", "new", "new"}
    Protected PageLinks As String
    Dim strREC_ID As String
    Protected strOPT As String = "0"
    Protected BufferStr As String

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            cboSearch.Items.Clear()
            cboSearch.Items.Add("* Select Insured *")
            Dim dt As DataTable = GET_INSURED(txtSearch.Value.Trim()).Tables(0)
            cboSearch.DataSource = dt
            cboSearch.DataValueField = "TBIL_POLY_POLICY_NO"
            cboSearch.DataTextField = "MyFld_Text"
            cboSearch.DataBind()
        End If
    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        'clear fields
        txtPolicyNo.Text = String.Empty
        Initialize()
        Try
            If cboSearch.SelectedIndex = -1 Or cboSearch.SelectedIndex = 0 Or cboSearch.SelectedItem.Value = "" Or cboSearch.SelectedItem.Value = "*" Then

            Else
                txtPolicyNo.Text = cboSearch.SelectedItem.Value
                GetPolicyInfo(txtPolicyNo.Text)
            End If
        Catch ex As Exception
            lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub
    Private Sub Initialize()
        'txtPolicyNo.Text = String.Empty
        txtSchemeName.Text = String.Empty
        txtEndDate.Text = String.Empty
        txtStartDate.Text = String.Empty
    End Sub

    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click
        ErrorInd = ""
        lblMsg.Text = ""
        ValidateControls(ErrorInd)
        If ErrorInd = "Y" Then
            Exit Sub
        End If

        Dim myResult As Boolean
        myResult = DetermineReInsurance(txtPolicyNo.Text)
        If myResult = False Then
            lblMsg.Text = "No member to reassure for this scheme"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        rParams(0) = "rptReAssuranceCertRpt"
        rParams(1) = "pPolicyNo="
        rParams(2) = txtPolicyNo.Text + "&"
        rParams(3) = url
        Session("ReportParams") = rParams

        Response.Redirect("../PrintView.aspx")
    End Sub
    Private Sub ValidateControls(ByRef ErrorInd As String)
        If (txtPolicyNo.Text = String.Empty) Then
            lblMsg.Text = "Policy number must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        'If (txtFileNum.Text = String.Empty) Then
        '    lblMsg.Text = "Please search for the Quotation Slip you want to print"
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    lblMsg.Visible = True
        '    ErrorInd = "Y"
        '    Exit Sub
        'End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "TBIL_GRP_POLICY_DET"
        If Not (Page.IsPostBack()) Then
         PageLinks = ""
            PageLinks = "<a href='../MENU_GL.aspx?menu=GL_UND' class='a_sub_menu'>Return to Menu</a>&nbsp;"

            Try
                strOPT = Page.Request.QueryString("opt").ToString
                'strOPT options = I001
            Catch
                strOPT = "PDI_ERR"
            End Try


            Select Case UCase(Trim(strOPT))
                Case "REASS_CERT"
                    STRMENU_TITLE = UCase("+++ Print Reassurance Cert +++ ")
                    BufferStr = ""
                Case Else
                    STRMENU_TITLE = UCase("+++ Print Reassurance Cert +++ ")
                    BufferStr = ""
            End Select
        End If
    End Sub

    Private Sub GetPolicyInfo(ByVal policyno As String)
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN
        Dim objOLEComm As OleDbCommand = New OleDbCommand()

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            lblMsg.Visible = True
            objOLEConn = Nothing
            Exit Sub
        End Try


        Try

            strSQL = ""
            strSQL = "SELECT DET.[TBIL_POLY_POLICY_NO], INS.[TBIL_INSRD_SURNAME] ,  INS.[TBIL_INSRD_FIRSTNAME], "
            strSQL = strSQL & "PREM.[TBIL_POL_PRM_FROM], PREM.[TBIL_POL_PRM_TO]  FROM " & strTableName & " AS DET"
            strSQL = strSQL & " INNER JOIN tbil_ins_detail AS INS ON DET.TBIL_POLY_ASSRD_CD = INS.TBIL_INSRD_CODE"
            strSQL = strSQL & " INNER JOIN TBIL_GRP_POLICY_PREM_INFO AS PREM ON DET.TBIL_POLY_POLICY_NO= PREM.TBIL_POL_PRM_POLY_NO"
            strSQL = strSQL & " WHERE DET.[TBIL_POLY_POLICY_NO] = '" & policyno & "'"
            strSQL = strSQL & " AND DET.[TBIL_POLY_FLAG] <> 'D'"

            objOLEComm.Connection = objOLEConn
            objOLEComm.CommandText = strSQL
            objOLEComm.CommandType = CommandType.Text
            Dim objOLEReader As OleDbDataReader = objOLEComm.ExecuteReader()
            If objOLEReader.HasRows = True Then
                objOLEReader.Read()
                txtPolicyNo.Text = objOLEReader("TBIL_POLY_POLICY_NO")
                txtSchemeName.Text = objOLEReader("TBIL_INSRD_SURNAME") & " " & objOLEReader("TBIL_INSRD_FIRSTNAME")
                If Not IsDBNull(objOLEReader("TBIL_POL_PRM_FROM")) Then _
                txtStartDate.Text = Format(objOLEReader("TBIL_POL_PRM_FROM"), "dd/MM/yyyy")
                If Not IsDBNull(objOLEReader("TBIL_POL_PRM_TO")) Then _
                txtEndDate.Text = Format(objOLEReader("TBIL_POL_PRM_TO"), "dd/MM/yyyy")
            End If
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            lblMsg.Visible = True
            Exit Sub
        End Try

        If objOLEComm.Connection.State = ConnectionState.Open Then
            objOLEComm.Connection.Close()
        End If
        '   objOLEComm.Dispose()
        objOLEComm = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

    End Sub

    Private Function DetermineReInsurance(ByVal policyno As String) As Boolean
        Dim result As Boolean
        result = False
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN
        Dim objOLEComm As OleDbCommand = New OleDbCommand()

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            lblMsg.Visible = True
            objOLEConn = Nothing
            Exit Function
        End Try


        Try
            strSQL = ""
            strSQL = "SELECT DET.[TBIL_POLY_POLICY_NO], INS.[TBIL_INSRD_SURNAME] ,  INS.[TBIL_INSRD_FIRSTNAME], "
            strSQL = strSQL & "PREM.[TBIL_POL_PRM_FROM], PREM.[TBIL_POL_PRM_TO]  FROM " & strTableName & " AS DET"
            strSQL = strSQL & " INNER JOIN tbil_ins_detail AS INS ON DET.TBIL_POLY_ASSRD_CD = INS.TBIL_INSRD_CODE"
            strSQL = strSQL & " INNER JOIN TBIL_GRP_POLICY_PREM_INFO AS PREM ON DET.TBIL_POLY_POLICY_NO= PREM.TBIL_POL_PRM_POLY_NO"
            strSQL = strSQL & " INNER JOIN [TBIL_GRP_POLICY_MEMBERS] AS MEM ON DET.[TBIL_POLY_POLICY_NO] = MEM.[TBIL_POL_MEMB_POLY_NO]"
            strSQL = strSQL & " WHERE DET.[TBIL_POLY_POLICY_NO] = '" & policyno & "'"
            strSQL = strSQL & " AND DET.[TBIL_POLY_FLAG] <> 'D'"
            'strSQL = strSQL & " AND MEM.[TBIL_POL_MEMB_TOT_SA] > " & 10000000 & ""
            strSQL = strSQL & " AND MEM.[TBIL_POL_MEMB_TOT_SA] * DET.[TBIL_POLY_COMP_SHARE]/100 > DET.[TBIL_POLY_RETENTION]"


            objOLEComm.Connection = objOLEConn
            objOLEComm.CommandText = strSQL
            objOLEComm.CommandType = CommandType.Text
            Dim objOLEReader As OleDbDataReader = objOLEComm.ExecuteReader()
            If objOLEReader.HasRows = True Then
                result = True
            End If
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            lblMsg.Visible = True
            Exit Function
        End Try

        If objOLEComm.Connection.State = ConnectionState.Open Then
            objOLEComm.Connection.Close()
        End If
        '   objOLEComm.Dispose()
        objOLEComm = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing
        Return result
    End Function

    Protected Sub cmdGetRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetRecord.Click
        'clear fields
        Initialize()
        Try
            If txtPolicyNo.Text = "" Then
                Me.lblMsg.Text = "Policy number must not be empty"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                lblMsg.Visible = True
                Exit Sub
            Else
                GetPolicyInfo(txtPolicyNo.Text)
            End If
        Catch ex As Exception
            lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        Initialize()
        txtPolicyNo.Text = ""
    End Sub
End Class
