Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Partial Class Policy_ReceiptBrowse
    Inherits System.Web.UI.Page
    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String
    Dim strSchKey As String
    Public publicMsgs As String
    Dim msg As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.EnableViewState = True
        strTableName = "TBFN_TRANS_FILE TRANS"
        If Not Page.IsPostBack Then
            strSchKey = Request.QueryString("MainAcct")
            Session("MainAcct") = strSchKey
            txtParentCode.Text = strSchKey
            If strSchKey IsNot Nothing Then
                fillBrowseValues()
            End If
            'updateFlag = False
            'Session("updateFlag") = updateFlag
        Else 'post back
        End If
    End Sub
    Protected Sub butGO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butGO.Click
        If cmbChoice.SelectedValue = "0" Then
            msg = "Please Choose the Criteria to Browse With"
            publicMsgs = "javascript:alert('" + msg + "')"
            Exit Sub
        End If
        Proc_DataBind()
    End Sub
    Private Sub Proc_DataBind()
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            objOLEConn = Nothing
        End Try
        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TRANS.*"
        strSQL = strSQL & ", CHART.TBFN_ACCT_SUB_DESC"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " left outer JOIN tbfn_acct_codes CHART ON TRANS.TBFN_GL_SUB_ACCT=CHART.TBFN_ACCT_SUB_CD"
        strSQL = strSQL & " WHERE TBFN_GL_TRANS_TYP='R'"
        If cmbChoice.SelectedValue = "Code" Then
            strSQL = strSQL & " AND TRANS.TBFN_GL_SUB_ACCT like '%" & RTrim(txtSearch.Text) & "%'"
        ElseIf cmbChoice.SelectedValue = "Name" Then
            strSQL = strSQL & " AND CHART.TBFN_ACCT_SUB_DESC like '%" & RTrim(txtSearch.Text) & "%'"
        End If

        Try
            Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)
            Dim objDS As DataSet = New DataSet()
            objDA.Fill(objDS, strTable)
            With grdView
                .DataSource = objDS
                .DataBind()
            End With
            objDS.Dispose()
            objDA.Dispose()

            objDS = Nothing
            objDA = Nothing

        Catch ex As Exception

        End Try


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing
    End Sub

    Private Sub fillBrowseValues()
        txtSearch.Text = strSchKey
        cmbSearchAccount.SelectedIndex = 4
    End Sub

    Protected Sub grdView_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdView.PageIndexChanging
        grdView.PageIndex = e.NewPageIndex
        Proc_DataBind()
    End Sub

    Protected Sub grdView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdView.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'assuming that the required value column is the second column in gridview
            CType(e.Row.FindControl("butSelect"), Button).Attributes.Add("Onclick", ("javascript:GetRowValue('" _
                            + (e.Row.Cells(1).Text + "," _
                            + e.Row.Cells(2).Text + "," _
                            + e.Row.Cells(3).Text + "," _
                            + e.Row.Cells(4).Text + "')")))
        End If
    End Sub
End Class
