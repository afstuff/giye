
//  Parameters
//      ScrollHeight – Height of the Scrollable DIV
//      Width – Width of the Scrollable DIV (Optional)
//      IsInUpdatePanel – This parameter must be set to true when the GridView is inside an ASP.Net AJAX UpdatePanel.

    $(document).ready(function () {
        $('#<%=GridView1.ClientID %>').Scrollable({
            ScrollHeight: 300,
            IsInUpdatePanel: true
        });

    });

