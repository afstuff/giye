
Partial Class Claims_RPT_GRP_DISCHARGE_VOUCHER
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Protected PageLinks As String
    Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String

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

    'Protected FirstMsg As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        cmdPrint_ASP.Enabled = False

        PageLinks = ""
        'PageLinks = PageLinks & "<a href='javascript:window.close();' runat='server'>Close...</a>"
        PageLinks = "<a href='../MENU_GL.aspx?menu=GL_CLAIM' class='a_sub_menu' style='float:right;'>Return to Menu</a>&nbsp;<br/>"

    End Sub



End Class
