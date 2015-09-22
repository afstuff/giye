        var my_height = 500;
        var my_width = 960;

        //var strCode = "";
        //var strName = "";

        var my_url = "";
        var my_fr = "";

        $(document).ready(function() {

            // $("#btnJQ_Popup").click(function(e) {
            //    e.preventDefault();
            //    //alert("About to launch jquery popup...");                
            //    // OK
            //    //$("div#div_jq_dialog").show("slow");
            // });


            // $("input#butJQ_Open").click(function(e) {
            //    $("div#div_jq_dialog").show("slow");
            // });

            // $("input#btnJQ_Close").click(function(e) {
            //    $("div#div_jq_dialog").hide(1000);
            // });

        });

        function ShowPopup(fvQRY_TYPE, pvPageURL, fvFRM_Name, fvCTR_Val, fvCTR_Txt) {

            //alert("About to launch jquery popup...");

            var strParam_N = null;

            strParam_N = pvPageURL
            strParam_N = strParam_N + "&QRY_TYPE=" + fvQRY_TYPE;
            strParam_N = strParam_N + "&FRM_NAME=" + fvFRM_Name;
            strParam_N = strParam_N + "&CTR_VAL=" + fvCTR_Val;
            strParam_N = strParam_N + "&CTR_TXT=" + fvCTR_Txt;

            //my_url = "JQ_Modal_Child.aspx";
            //my_url = my_url + "?id=" + strCode + "&name=" + strName;
            my_url = pvPageURL;
            my_url = strParam_N;

            //  width='100%' height='550' style='background: lightblue; border:0;'
            
            my_fr = "<iframe id='myframe'" + " src='" + my_url + "'" + " class='myframe_css' >" + " </iframe>";
            //my_fr = "<iframe id='myframe' src='../WebFormX.aspx?popup=YES&QRY_TYPE=BRK&FRM_NAME=Form1&CTR_VAL=txtBrokerNum&CTR_TXT=txtBrokerNum' width='960' height='550' style='border:0px;' > </iframe>";
            
            // OK
            //$("div#div_jq_dialog").show("slow");
            // OR
            //$("div#div_jq_dialog").show("slow", function() {
            //});


            //$("div#div_jq_dialog").dialog();
            //$("div#div_jq_dialog").load("demo_test.html");

            //            $("div#div_jq_dialog").dialog({
            //                buttons: {
            //                    OK: function() { }
            //                },
            //                title: "Success"
            //            });
            

            $.modal(my_fr, {
            //$("#div_jq_dialog").modal({
                appendTo: 'form',
                //title: "Popup Page",
                autoPosition: true,
                autoOpen: true,
                focus: true, // Disable focus (allows tabbing away from dialog)
                autoResize: true,
                cancel: true,
                close: true,
                closeClass: "modalClose",
                closeHTML: "<a href='#' class='modalCloseImg'><img alt='X' src='../Images/x.png' style='display: none;' /></a>", // <img alt='X' src='../Images/x.png' />
                containerId: "myframe",
                //containerId: "div_jq_dialog",
                containerCss: {
                    backgroundColor: "#fff",
                    borderColor: "#fff",
                    // height: 550,
                    // width: 960
                    padding: 1
                },
                // draggable: true,
                escClose: true, // Allow Esc keypress to close the dialog
                 // minHeight: 550,
                 // minWidth: 960,
                minHeight: my_height,
                minWidth: my_width,
                //hide: "explode",
                modal: true,
                // position: ["10", "20%"],
                // position: ["50%", "50%"],
                //opacity: 0.7,
                overlayCss: { backgroundColor: "#fff" },
                overlayClose: false, //Allow click on overlay to close the dialog
                onShow: function(dialog) { // The callback function used after the modal dialog has opened
                    // // Access elements inside the dialog
                    // Useful for binding events, initializing other plugins, etc.

                    // For example:
                    // $("a", dialog.data).click(function() {
                    //    // do something
                    //    return false;
                    //});
                },
                onClose: function(dialog) {
                    $.modal.close();
                }

            });

        }
