function ShowPopup_Msg(strMSG) {
    try {
        alert(strMSG);
    }
    catch (ex) {
        alert("Error has occured!. Reason: " + ex.message);
    }
}

function Sel_Func_OK(fvopt) {
    //                window.opener.UpdateFields (forename.value, surname.value);

    //var myvar1 = $("iframe[src='WebFormX.aspx']").contents().find("#txtValue").val();

    //var $form = $("<form/>").attr("id", "data_form")
    //          .attr("action", "Page2.aspx")
    //          .attr("method", "post");

    try {

        var fvVal = document.getElementById("hidCustID").value;
        var fvTxt = document.getElementById("hidCustName").value;
        var fvFRM_Name = document.getElementById("hidFRM_NAME").value;
        var fvCTR_Val = "#" + document.getElementById("hidCTR_VAL").value;
        var fvCTR_Txt = "#" + document.getElementById("hidCTR_TXT").value;

        // ok
        //parent.document.form1.txtCode.value = strval;
        //parent.document.form1.txtName.value = strName;

        // ok
        //parent.document.getElementById("txtCode").value = strval;
        //parent.document.getElementById("txtName").value = strName;

        // ok
        //window.parent.jQuery("#txtCode").val(fvVal);
        //window.parent.jQuery("#txtName").val(fvTxt);

        window.parent.jQuery(fvCTR_Val).val(fvVal);
        window.parent.jQuery(fvCTR_Txt).val(fvTxt);

        // ok
        // The Iframe instance is removed after dialog close.
        //window.parent.jQuery("#myframe").remove();
        // Note: After the above codes, you cannot use ...modal.close();

        // ok
        window.parent.jQuery.modal.close();

        // ok
        //window.parent.$("#" + "form1").modal(close());

        //window.parent.jQuery("#form1").modal(close());

        //alert("About to unload popup...");

    }

    catch (ex) {
        alert("Error has occured!. Reason: " + ex.message);
    }

}

function Sel_Func_Cancel() {
    try {

        //window.close();
        //alert("Cancel button is clicked...");

        //window.close();
        //$.modal.close();

        // The Iframe instance is removed after dialog close.
        //window.parent.jQuery("#myframe").remove();
        window.parent.jQuery.modal.close();
    }

    catch (ex) {
        alert("Error has occured!. Reason: " + ex.message);
    }

}
