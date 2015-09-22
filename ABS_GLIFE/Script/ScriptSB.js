
// setTimeout Method
// -----------------
// Evaluates an expression after a specified number of milliseconds has elapsed. 
// Syntax
// iTimerID = window.setTimeout(vCode, iMilliSeconds [, sLanguage])

// Examples
// The following example uses the setTimeout method to evaluate a simple expression after one second has elapsed.
// window.setTimeout("alert('Hello, world')", 1000);

// The following example uses the setTimeout method to evaluate a slightly more complex expression after one second has elapsed.
// var sMsg = "Hello, world";
// window.setTimeout("alert(" + sMsg + ")", 1000);

// clearTimeout Method
// -------------------
// Cancels a time-out that was set with the setTimeout method. 
// Syntax
// window.clearTimeout(iTimeoutID)

var msgx;
var mytitle;

var mymsg;

var pos = 0;
var blnT = 1;

msgx = "Please Wait. Processing...";
msgx = "..." + msgx;

var oInterval = "";
var oInterval2 = "";

var oTimeout = "";
var oTimeout2 = "";

var oTAG_CONT = null;
var oTAG_MSG = null;

// call ScollMSG function
// scrollMSG();

//  onLoad="timerONE=window.setTimeout('slide(120,0)',20);"
//  onunload="window.alert(' Good Bye ')"
//  onunload="proc_close();

function scrollMSG(tag_cont, tag_msg) {

            oTAG_CONT = "SB_CONT";
            oTAG_MSG = "SB_MSG";
            
            oTAG_CONT = tag_cont;
            oTAG_MSG = tag_msg;

        var myreply;
        try {            
            myreply = confirm("*** ARE YOU SURE YOU WANT TO UPLOAD DATA NOW ? *** ");
            if (myreply != true) {
                alert("Data Upload Operation Cancelled...");
                return false;
            }
        }
        catch (ex_err) {
            alert("Error has occured. Reason: " + ex_err.message);
            return false;
        }


        try {
            // document.getElementById("SB_CONT").style.display = "";
            // document.getElementById("SB_CONT").style.visibility = "visible";
            document.getElementById(oTAG_CONT).style.display = "";
            document.getElementById(oTAG_CONT).style.visibility = "visible";
        }
        catch (ex_err) {
            alert("Error has occured. Reason: " + ex_err.message);
            return false;
        }

        scrollMSG_Start(oTAG_CONT, oTAG_MSG);
        return true;

}

function scrollMSG_Start(tag_cont, tag_msg) {

    //mytitle = msgx.substring(pos, msgx.length) + msgx.substring(0, pos);

    //document.title = mytitle;
    //document.status = mytitle;

    pos++;
    if (pos > msgx.length) pos = 0;

    if (blnT == 1) {
        mymsg = "### Processing. PLEASE WAIT...";
        blnT = 0;
    }
    else {
        mymsg = "";
        blnT = 1;
    }

    // try {

        // oTimeout2 = window.setTimeout("scrollMSG_Start()", 800);
        // document.getElementById("SB_MSG").innerHTML = mymsg;
        // document.getElementById(tag_msg).innerHTML = mymsg;
        document.getElementById(oTAG_MSG).innerHTML = mymsg;

        oTimeout2 = window.setTimeout("scrollMSG_Start(" + oTAG_CONT + "," + oTAG_MSG + ")", 800);
        // oInterval2 = window.setInterval("scrollMSG_Start(" + tag_cont + "," + tag_msg + ")", 500);
        return true;
    // }
    // catch (ex_err) {
    //     // alert("Error has occured. Reason: " + ex_err.message);
    //     return false;
    // }

}


function scrollMSG_End(tag_cont, tag_msg) {
    if (oTimeout2 == null || oTimeout2 == "undefined" || oTimeout2 == "") {
        oTimeout2 = null;
    }
    else {
        window.clearTimeout(oTimeout2);
        oTimeout2 = null;
    }

    oTimeout2 = null;
    try {
        // document.getElementById("SB_CONT").style.display = "";
        // document.getElementById("SB_CONT").style.visibility = "visible";
        // document.getElementById("SB_MSG").innerHTML = "";
        // document.getElementById("SB_MSG").value = "";
        // document.getElementById(tag_msg).innerHTML = "";

        document.getElementById(tag_cont).style.display = "none";
        document.getElementById(tag_cont).style.visibility = "hidden";
    }
    catch (ex_err) {
        // alert("Error has occured. Reason: " + ex_err.message);
        // return false;
    }

    // window.alert("Operation Ended...");
    return true;
    
}
