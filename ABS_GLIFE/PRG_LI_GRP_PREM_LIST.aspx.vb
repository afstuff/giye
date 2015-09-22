Imports Microsoft.Reporting.WebForms
Partial Class PRG_LI_GRP_PREM_LIST
    Inherits System.Web.UI.Page
    Protected FirstMsg As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            'Dim mystrURL = "window.open('http://192.168.10.73:88/ABSReportSvr?/ABSLIFEReports/OtherReceiptList&rs:Command=Render&rc:LinkTarget=main target=main','','left=50,top=10,width=1024,height=600,titlebar=yes,z-lock=yes,address=yes,channelmode=1,fullscreen=0,directories=yes,location=yes,toolbar=yes,menubar=yes,status=yes,scrollbars=1,resizable=yes');"
            'Dim mystrURL = "window.open('http://192.168.10.73:88/ABSReportSvr?/ABSLIFEReports/OtherReceiptList&rs:Command=Render&rc:LinkTarget=main target=main','','left=50,top=10,width=1024,height=600,titlebar=yes,z-lock=yes,address=yes,channelmode=1,fullscreen=0,directories=yes,location=yes,toolbar=yes,menubar=yes,status=yes,scrollbars=1,resizable=yes');"
            Dim mystrURL = "window.open('http://192.168.10.73:88/ABSReportSvr?/ABSLIFEReports/OtherReceiptList&rs:Command=Render&rc:LinkTarget=main target=main','','left=50,top=10,width=1024,height=600,titlebar=yes,z-lock=yes,address=yes,channelmode=1,fullscreen=0,directories=yes,location=yes,toolbar=yes,menubar=yes,status=yes,scrollbars=1,resizable=yes');"
            '    'FirstMsg = "javascript:window.close();" & mystrURL
            FirstMsg = "javascript:" & mystrURL
        Catch ex As Exception
            ' Me.lblMsg.Text = "<br />Unable to connect to report viewer. <br />Reason: " & ex.Message.ToString

        End Try
    End Sub

    
End Class
