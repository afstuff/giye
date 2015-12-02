
Partial Class MENU_GL
    Inherits System.Web.UI.Page

    'Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    Protected BufferStr As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.txtAction.Text = "Log_Out" Then
            'Call DoProc_LogOut()
            Me.txtAction.Text = ""
            'Response.Redirect("LoginP.aspx")
            'Response.Redirect(Request.ApplicationPath & "~/Default.aspx")
            'Response.Redirect(Request.ApplicationPath & "~/m_menu.aspx?menu=home")
            Response.Redirect("Default.aspx")
            Exit Sub
        End If


        'Put user code to initialize the page here
        Dim mKey As String
        Try
            mKey = Page.Request.QueryString("menu").ToString
            'mkey options = MKT UND CLM REIN CRCO ACC ADMIN
        Catch
            mKey = "HOME"
        End Try

        'Response.Write("<br />Menu ID: " & mKey)

        STRMENU_TITLE = "+++ Home Page +++ "
        BufferStr = ""
        Call DoProc_Menu(mKey)

    End Sub

    Private Sub DoProc_Menu(ByVal pvMenu As String)
        Select Case pvMenu.ToUpper
            Case "HOME"
                STRMENU_TITLE = "+++ Group Life Module +++ "
                'AddMenuItem("", "Welcome to ABS Web Accounts Manager", "MainM.aspx?menu=HOME")
                BufferStr = ""
                BufferStr += "<tr>"
                BufferStr += "<td align='center' colspan='2' valign='top'>"
                BufferStr += "&nbsp;<img alt='Image' src='Images/GLife.jpg' style='width: 850px; height: 550px' />&nbsp;"
                BufferStr += "</td>"
                BufferStr += "</tr>"

            Case "GEN"
                STRMENU_TITLE = "+++ Parameters Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                'AddMenuItem("", "Company Data Setup", "javascript:jsDoPopNew_Full('General/GEN110.aspx?TTYPE=COY')")
                AddMenuItem("", "Server Parameters Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")

            Case "GL_CODE"
                'Me.LNK_CODE.BackColor = Drawing.Color.Teal
                'Me.LNK_CODE.Font.Bold = True
                STRMENU_TITLE = "+++ Master Setup Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("Master Setup", "Codes File Setup", "menu_gl.aspx?menu=gl_code_std")
                AddMenuItem("Master Setup", "Codes File Setup", "Codes/PRG_GP_CODES.aspx?optid=ALL&optd=ALL_CODES")

                AddMenuItem("", "Customer Masters", "menu_gl.aspx?menu=gl_code_cust")
                'AddMenuItem("", "Master Codes Setup", "menu_gl.aspx?menu=gl_code_mst")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Rate Masters", "menu_gl.aspx?menu=gl_code_rate")
                AddMenuItem("", "Product Master", "menu_gl.aspx?menu=gl_code_prod")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Underwriting Codes Masters", "menu_gl.aspx?menu=gl_code_und")
                AddMenuItem("", "Reinsurance Codes Masters", "menu_gl.aspx?menu=gl_code_reins")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
            Case "GL_CODE_STD"
                STRMENU_TITLE = "+++ Master Setup >>> Codes File Setup Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=gl_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Codes File Setup", "Nationality Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=001&optd=Nationality")
                AddMenuItem("", "State Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=002&optd=State")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Branch Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=003&optd=Branch")
                AddMenuItem("", "Division Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=004&optd=Division")
                AddMenuItem("", "Department Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=005&optd=Department")
                AddMenuItem("", "Location Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=006&optd=Location")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Occupation Class Setup", "Codes/PRG_GP_CODES.aspx?optid=007&optd=Occupation_Class")
                AddMenuItem("", "Occupation Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=008&optd=Occupation")
                AddMenuItem("", "Religion/Belief Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=009&optd=Religion")
                AddMenuItem("", "Customer Title Codes Setup", " Codes/PRG_GP_CODES.aspx?optid=010&optd=Customer_Title")
                AddMenuItem("", "Rider Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=011&optd=Rider")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Charge Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=012&optd=Charge_Codes")
                AddMenuItem("", "Relation Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=013&optd=Relation")
                AddMenuItem("", "Source of Business Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=014&optd=Source_Of-Business")
                AddMenuItem("", "Gender Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=015&optd=Gender")
                AddMenuItem("", "Agency Location Setup", "Codes/PRG_GP_CODES.aspx?optid=016&optd=Agency_Location")
                AddMenuItem("", "Currency Codes Setup", "Codes/PRG_GP_CODES.aspx?optid=017&optd=Currency_Codes")
                AddMenuItem("", "Marital Status", "Codes/PRG_GP_CODES.aspx?optid=020&optd=Marital_Status")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=gl_code")

            Case "GL_CODE_CUST"
                STRMENU_TITLE = "+++ Master Setup >>> Customer Masters Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=gl_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Customer Masters", "Assured Class Setup", "Codes/PRG_GP_CUST_CLASS.aspx?optid=001&optd=Customer_Category")
                AddMenuItem("", "Assured Details", "Codes/PRG_GP_CUST_DTL.aspx?optid=001&optd=Customer_Details")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Agents/Brokers Category Setup", "Codes/PRG_GP_BRK_CAT.aspx?optid=001&optd=Brokers_Agents_Category")
                AddMenuItem("", "Agency/Brokers Details", "Codes/PRG_GP_BRK_DTL.aspx?optid=001&optd=Brokers_Agents_Details")
                AddMenuItem("", "Marketers Codes Setup (Agencies)", "Codes/PRG_GP_MKT_CD.aspx?optid=001&optd=Marketers_Agency")
                AddMenuItem("", "Contractor Details", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=gl_code")
            Case "GL_CODE_MST"
            Case "GL_CODE_RATE"
                STRMENU_TITLE = "+++ Master Setup >>> Rate Master Codes Setup Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=gl_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Rates", "Rate Type/Category Setup", "")
                AddMenuItem("", "Rate Master Setup", "")
                AddMenuItem("", "Allocation to Investment Rate Setup", "")
                AddMenuItem("", "Agencies Commission Rate Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=gl_code")
            Case "GL_CODE_PROD"
                STRMENU_TITLE = "+++ Master Setup >>> Product Master Codes Setup Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=gl_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Products", "Product Category/Class Setup", "")
                AddMenuItem("", "Product Details", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Cover Master", "")
                AddMenuItem("", "Plan Master", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=gl_code")

            Case "GL_CODE_UND"
                STRMENU_TITLE = "+++ Master Setup >>> Underwriting Codes Masters Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=gl_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Underwriting Codes", "Business Sector Setup", "Codes/PRG_GP_BUS_SEC.aspx")
                AddMenuItem("", "Disability Type Setup", "")
                AddMenuItem("", "Health Status Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Medical Illness Codes Setup", "")
                AddMenuItem("", "Medical Examination Codes Setup", "")
                AddMenuItem("", "Medical Clinic Details Setup", "")
                AddMenuItem("", "Mortality Codes Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Loading Codes Setup", "")
                AddMenuItem("", "Discount Codes Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Policy Condition Codes Setup", "")
                AddMenuItem("", "Loss Types Codes Setup", "")
                AddMenuItem("", "Source of Business Codes Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=il-code")

            Case "GL_CODE_REINS"
                STRMENU_TITLE = "+++ Master Setup >>> Reinsurance Codes Masters Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=gl_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "MENU CAPTION", "PAGE URL")
                AddMenuItem("Reinsurance Codes", "ReAssurer Company Category (Local, Overseas)", "")
                AddMenuItem("", "ReAssurer Company Codes Setup", "")
                AddMenuItem("", "ReAssurer Proportion Setup (Yearly Treaty Arrangement", "")
                AddMenuItem("", "ReAssurer Commission Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=il-code")

            Case "GL_QUOTE"
                STRMENU_TITLE = "+++ Quotation Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("New Quotation", "Group Life Assurance Quotation", "gl_2010.aspx")
                AddMenuItem("", "Group Funeral", "")
                AddMenuItem("", "Group Credit Life Assurance", "")
                AddMenuItem("", "Group Mortage Protection", "")
                AddMenuItem("", "Welfare Scheme", "")
                AddMenuItem("", "Group Tuition", "")
                AddMenuItem("", "Critical Illness", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Reports", "Proposal Status Report", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")

            Case "GL_UND"
                'Me.LNK_UND.BackColor = Drawing.Color.Teal
                'Me.LNK_UND.Font.Bold = True
                STRMENU_TITLE = "+++ Underwriting Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Convert Quotation to Policy", "")
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("New Policy", "Group Life Assurance Policy", "gl_3020.aspx")
                'AddMenuItem("", "Group Funeral", "")
                'AddMenuItem("", "Group Credit Life Assurance", "")
                'AddMenuItem("", "Group Mortage Protection", "")
                'AddMenuItem("", "Welfare Scheme", "")
                'AddMenuItem("", "Group Tuition", "")
                'AddMenuItem("", "Critical Illness", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Transaction", "Debit Note or Credit Note Data Entry", "Transaction/PRG_LI_GRP_PREM_DBCR_NOTE_ENTRY.aspx?TTYPE=ACDI")
                'TTYPE=B
                'AddMenuItem("", "Overriding Commission (ORC) Data Entry", "")
                ''TTYPE=I
                'AddMenuItem("", "Facultative Inward Data Entry", "")
                ''TTYPE=O
                'AddMenuItem("", "Facultative Outward Data Entry", "")

                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Fund", "Welfare Fund Management", "")

                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Reports", "Debit Note Print", "Reports/PRG_LI_GRP_DN_RPT.aspx?TTYPE=DN")
                AddMenuItem("Reports", "Debit Note Without Commission Print", "Reports/PRG_LI_GRP_DN_RPT.aspx?TTYPE=DNNOCOM")
                AddMenuItem("", "Credit Note Print", "Reports/PRG_LI_GRP_DN_RPT.aspx?TTYPE=CN")
                AddMenuItem("", "Credit Note Without Commission Print", "Reports/PRG_LI_GRP_DN_RPT.aspx?TTYPE=CNNOCOM")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Medical Examination Requirement List", "Reports/GRP_MED_1101.aspx?TTYPE=GRP_RPT_MED_1101")
                AddMenuItem("", "Group Medical Examination Test Requirement List", "Reports/GRP_MEDICAL_EXAM_LIST.aspx")
                'AddMenuItem("", "Policy Schedule", "")
                AddMenuItem("", "Print Policy Schedule", "POLICY/PRG_LI_GRP_QUOT_SCHEDULE.aspx?opt=POLY_SCHDLE")

                AddMenuItem("", "Policy Register", "")
                AddMenuItem("", "Premium Report", "")
                AddMenuItem("", "Renewal Register", "")
                AddMenuItem("", "Renewal Schedule", "")
                AddMenuItem("", "Renewal Notices", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Schedule of Members Added", "POLICY/PRG_LI_GRP_QUOT_SCHEDULE.aspx?opt=POLY_SCHDLE")
                AddMenuItem("", "Schedule of Members Deleted", "POLICY/PRG_LI_GRP_QUOT_SCHEDULE.aspx?opt=POLY_SCHDLE_DEL")
                AddMenuItem("", "Schedule of Members in Scheme", "POLICY/PRG_LI_GRP_QUOT_SCHEDULE.aspx?opt=POLY_SCHDLE")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Agency Reports", "Brokers Commission Summary", "")
                AddMenuItem("", "Brokers Commission Statement", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
            Case "GL_ENDORSE"
                'Me.LNK_ENDORSE.BackColor = Drawing.Color.Teal
                'Me.LNK_ENDORSE.Font.Bold = True
                STRMENU_TITLE = "+++ Endorsement Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Operations", "Assured Basic Info. Changes", "")
                'AddMenuItem("", "Premium Details Changes", "")
                AddMenuItem("", "Addition of New Members", "")
                AddMenuItem("", "Deletion of Member", "POLICY/PRG_LI_GRP_POLY_PERSNAL.aspx?optid=DELMEM")
                'AddMenuItem("", "Beneficiary Details Changes", "")
                'AddMenuItem("", "Terms/Duration Changes", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
            Case "GL_PROCESS"
                'Me.LNK_PROCESS.BackColor = Drawing.Color.Teal
                'Me.LNK_PROCESS.Font.Bold = True
                STRMENU_TITLE = "+++ Processing Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "")
                AddMenuItem("Processing", "Processing Lapse Policy", "")
                AddMenuItem("", "Premium Account Position", "")
                AddMenuItem("", "Policy Revival Processing", "")
                AddMenuItem("", "Year End Actuarial Processing", "")
                AddMenuItem("", "Monthly Commission Processing", "")
                AddMenuItem("", "Monthly Commission Supplementary", "")
                AddMenuItem("", "Restore Back Commission Calculation", "")
                AddMenuItem("", "", "")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")

            Case "GL_CLAIM"
                'Me.LNK_CLP.BackColor = Drawing.Color.Teal
                'Me.LNK_CLP.Font.Bold = True
                STRMENU_TITLE = "+++ Claims Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "MENU CAPTION", "PAGE URL")Claim Request Entry
                AddMenuItem("Transactions", "Claim Reported Entry", "Claims/PRG_LI_GRP_CLM_ENTRY.aspx")
                AddMenuItem("", "Waiver of Premium", "Claims/PRG_LI_GRP_CLM_WAIVER.aspx")
                AddMenuItem("", "Paid-Up Policies", "Claims/PRG_LI_GRP_PAIDUP_PROCESS.aspx")
                AddMenuItem("", "Lapse Policies", "Claims/PRG_LI_GRP_LAPSE_PROCESS.aspx")
                AddMenuItem("", "Policy Cancellation Process", "Claims/PRG_LI_GRP_CANCEL_PROCESS.aspx")
                AddMenuItem("", "Policy Reactivation Process", "Claims/PRG_LI_GRP_REVIVE_POLICY.aspx")
                AddMenuItem("", "Maturity Claim Process", "Claims/PRG_LI_GRP_CLM_MATURE.aspx")
                AddMenuItem("", "Partial Maturity Claim Process", "Claims/PRG_LI_CLM_PART_MATURE.aspx")
                AddMenuItem("", "Claims Additions Entry", "")
                'AddMenuItem("", "", "")
                'AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Claims Paid Entry", "")


                AddMenuItem("", "", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Reports", "Claims Reported Report", "")
                AddMenuItem("", "Claims Paid Report", "")
                AddMenuItem("", "Claims Outstanding Report", "")
                AddMenuItem("", "Death Claims Paid Report", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")

            Case "GL_REINS"
                'Me.LNK_REINS.BackColor = Drawing.Color.Teal
                'Me.LNK_REINS.Font.Bold = True
                STRMENU_TITLE = "+++ Reinsurance Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "MENU CAPTION", "PAGE URL")
                AddMenuItem("Transactions", "ReAssurer Data Entry", "")
                AddMenuItem("", "Facultative Data Entry", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Reports", "ReAssurance Report", "Reinsurance/GRP_REINS_1101.aspx?TTYPE=GRP_RPT_REINS_1101")
                AddMenuItem("", "ReAssurance Register", "Reinsurance/GRP_REINS_1101.aspx?TTYPE=GRP_RPT_REINS_1102")
                AddMenuItem("", "ReInsurance Data Entry", "Reinsurance/GRP_REINSURANCE.aspx")
                AddMenuItem("", "ReAssurer Bordeareaux", "")
                AddMenuItem("", "Definite Certificate", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")

            Case "GL_SEC"
                STRMENU_TITLE = "+++ Administration Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Administrator Password Change", "")
                AddMenuItem("", "Create New User", "")
                AddMenuItem("", "User Password Change", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")

            Case "GL_RENEWAL"
                STRMENU_TITLE = "+++ Renewal Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_gl.aspx?menu=home")
                AddMenuItem("", "", "")
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "", "")
                AddMenuItem("Renewals", "Pre-Renewal Processing", "")
                AddMenuItem("", "Renew Scheme", "./policy/PRG_LI_GRP_POLY_MEMBERS_RENEW.aspx") 'blank link


        End Select

    End Sub

    Private Sub AddMenuItem(ByVal LeadItem As String, ByVal MenuItemText As String, ByVal LinkUrl As String)
        Dim myURL As String
        myURL = LinkUrl
        If Trim(myURL) = "" Then
            myURL = "'#'"
        Else
            myURL = "'" & myURL & "'"
        End If

        BufferStr += "<tr>"
        If LeadItem.Trim() = "" Then
            BufferStr += "<td nowrap align='left' valign='top'>&nbsp;&nbsp;</td>"
        Else
            BufferStr += "<td nowrap align='left' valign='top' class='a_sub_menu'>&nbsp;"
            BufferStr += "<img alt='Menu Image' src='Images/ballred.gif' class='MY_IMG_LINK' />&nbsp;"
            BufferStr += LeadItem & "&nbsp;</td>"
        End If

        If MenuItemText.Trim = "" Then
            BufferStr += "<td nowrap align='left' valign='top'>&nbsp;</td>"
        ElseIf MenuItemText.Trim = "UNDER_LINE" Then
            BufferStr += "<td nowrap align='left' valign='top' class='td_under_line'>&nbsp;</td>"
        ElseIf MenuItemText.Trim = "Returns to Previous Page" Then
            BufferStr += "<td nowrap align='left' valign='top' class='td_return_menu'>"
            BufferStr += "<a href=" & myURL & " valign='top' class='a_sub_menu'>" & MenuItemText & "</a>"
            BufferStr += "</td>"
        Else
            BufferStr += "<td nowrap align='left' valign='top' class='td_sub_menu2'>"
            BufferStr += "<img alt='Menu Image' src='Images/ballredx.gif' class='MY_IMG_LINK' />&nbsp;"
            BufferStr += "<a href=" & myURL & " valign='top' class='a_sub_menu2'>" & MenuItemText & "</a>"
            BufferStr += "</td>"
        End If
        BufferStr += "</tr>"
    End Sub

    Protected Sub DoProc_LogOut()

        Dim strSess As String = "STFID"
        Dim intC As Integer = 0
        Dim intCX As Integer = 0
        Dim MyArray(15) As String

        intC = 0
        intCX = 0
        Try
            'Session("STFID") = RTrim(Me.txtNum.Text)
            'Session("STFID") = RTrim("")

            'Session.Keys
            'Session.Count
            'LOOP THROUGH THE SESSION OBJECT
            '*******************************

            'For intC = 0 To Session.Count - 1
            'Response.Write("<br />" & "Item " & intC & " - Key: " & Session.Keys(intC).ToString & " - Value: : " & Session.Item(intC).ToString)
            '
            'Next

            'SAMPLE SESSION DATA
            '*******************
            ''Item 0 - Key: ActiveSess - Value: : 1
            ''Item 1 - Key: StartdDate - Value: : 06/14/2013 7:01:55 PM
            ''Item 2 - Key: connstr - Value: : Provider=SQLOLEDB;Data Source=ABS-PC;Initial Catalog=ABS;User ID=SA;Password=;
            ''Item 3 - Key: connstr_SQL - Value: : Data Source=ABS-PC;Initial Catalog=ABS;User ID=SA;Password=;
            ''Item 4 - Key: CL_COMP_NAME - Value: : CUSTODIAN AND ALLIED INSURANCE PLC
            ''Item(5 - Key) : MyUserIDX(-Value) : ADM()
            ''Item(6 - Key) : MyUserName(-Value) : System(Administrator)
            ''Item 7 - Key: MyUserLevelX - Value: : 0
            ''Item(8 - Key) : MyUserIDX_NIM(-Value) : ADM()
            ''Item(9 - Key) : MyUserName_NIM(-Value) : System(Administrator)
            ''Item 10 - Key: MyUserLevelX_NIM - Value: : 0
            ''Item 11 - Key: MyLogonTime - Value: : 06/14/2013 7:02:05 PM
            ''Item(12 - Key) : MyUserID(-Value) : ADM()


            'For Each strS As String In Session.Keys
            '    '    ' ...
            '    'Response.Write("<br /> Session ID: " & Session.SessionID & " - Key: " & strSess.ToString & " - Value: " & Session.Item(strSess).ToString)

            '    '    If UCase(strSess) = UCase("connstr") Or _
            '    '      UCase(strSess) = UCase("connstr_SQL") Or _
            '    '      UCase(strSess) = UCase("CL_COMP_NAME") Then
            '    '    Else
            '    '        'Session.Remove(strSess)
            '    '    End If
            '    strSess = strS
            '    Response.Write("<br />" & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
            'Next

            For intCX = 0 To Session.Count - 1

                strSess = Session.Keys(intCX).ToString

                If UCase(strSess) = UCase("connstr") Or _
                  UCase(strSess) = UCase("connstr_SQL") Or _
                  UCase(strSess) = UCase("CL_COMP_NAME") Or _
                  UCase(strSess) = UCase("ActiveSess") Or _
                  UCase(strSess) = UCase("StartdDate") Then
                Else
                    intC = intC + 1
                    MyArray(intC) = strSess
                    'Response.Write("<br />" & "Item " & intC & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)

                End If

            Next

            'Response.Write("<br />" & "Item ubound(): " & UBound(MyArray).ToString)
            'Response.Write("<br />" & "Item Length: " & MyArray.Length)

            For intCX = 1 To intC

                strSess = MyArray(intCX).ToString

                Response.Write("<br />" & "Removing session Item " & intCX & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
                Session.Remove(strSess.ToString)
                'Session.Contents.Remove(strSess)

            Next

            'Session.Clear()

        Catch ex As Exception
            Response.Write("<br /> Error has Occured at key: " & strSess.ToString & " Reason: " & ex.Message.ToString)
            'Exit Try
        End Try


    End Sub


End Class
