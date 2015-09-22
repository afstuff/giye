<script language="javascript" type="text/javascript">

function Func_File_Change() {
    var c = 0;
    var cx = 0
    var strfile = "";

    strfile = document.getElementById("My_File_Upload").value;
    // strfile = document.getElementById("My_File_Upload").PostedFile.FileName;
    for (c = 0; c < strfile.length; c++) {
        if (strfile.substring(c, 1) == "") {
        }
        else {
            cx = cx + 1;
        }
    }

    if (cx <= 0) {
        document.getElementById("txtFile_Upload").style.display = "none";
        document.getElementById("txtFile_Upload").style.visibility = "hidden";
        document.getElementById("cmdFile_Upload").disabled = true;
        alert("Missing or Invalid document name...");
        return false;
    }
    else {
        document.getElementById("txtFile_Upload").style.display = "";
        document.getElementById("txtFile_Upload").style.visibility = "visible";
        document.getElementById("txtFile_Upload").value = strfile;
        // document.getElementById("txtFile_Upload").innerHTML = strfile;
        document.getElementById("cmdFile_Upload").disabled = false;
        // 
        return true;
    }
}

</script>