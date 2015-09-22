using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;
using System.Globalization;

using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using System.Web;
using System.Web.Configuration;




namespace CustodianGroupLife.Data
{
    public class hashHelper
    {

       public static MyErrorListing errmsgs = new MyErrorListing();

        public static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = 
                  FormsAuthentication.HashPasswordForStoringInConfigFile(
                                                       saltAndPwd, "SHA1");
            return hashedPwd;
        }


        //public static bool VerifyPassword(Object _user, string suppliedPassword, string objType)
        //{
        //    bool passwordMatch = false;
        //    // Get the salt and pwd from the database based on the user name.
        //    string dbPasswordHash = String.Empty;
        //    string salt = String.Empty;
        //    //objMembersCred memCred;
        //    //objUserCredentials userCred;
        ////    switch (objType)
        //    {
        //        case "member":
        //            memCred = (objMembersCred)_user;
        //            dbPasswordHash = memCred.PassWordHash;
        //            salt = memCred.Salt;
        //            break;
        //        case "admin":
        //            userCred = (objUserCredentials)_user;
        //            dbPasswordHash = userCred.PassWordHash;
        //            salt = userCred.Salt;
        //            break;

        //    }
        //    try
        //    {
        //        // Now take the salt and the password entered by the user
        //        // and concatenate them together.
        //        string passwordAndSalt = String.Concat(suppliedPassword, salt);
        //        // Now hash them
        //        string hashedPasswordAndSalt =
        //                   FormsAuthentication.HashPasswordForStoringInConfigFile(
        //                                                   passwordAndSalt, "SHA1");
        //        // Now verify them.
        //        passwordMatch = hashedPasswordAndSalt.Equals(dbPasswordHash);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ERROR verifying password: " + ex.Message);
        //    }
        //    finally
        //    {
        //    }
        //    return passwordMatch;
        //}

        public static DateTime GoodDate(string yr, string mth, string dy)
        {
            string strDte = yr + "/" + mth + "/" + dy;
            return checkDate(strDte);
        }
        public static DateTime GoodDate(string mydate)
        {
            return moveDateToCulture(mydate);
        }

        private static DateTime checkDate(string dte)
        {
            DateTime myDate = DateTime.Today;
            try
            {
                myDate = Convert.ToDateTime(dte);
            }
            catch 
            {
                throw new Exception("Invalid Date! Please check Format");
            }
            return myDate;

        }
        /// <summary>
        /// Converts DATE type in dd/mm/yyyy format to a yyyymmdd STRING type format.
        /// </summary>
        /// <param name="dte"> date string in dd/mm/yyyy format</param>
        /// <returns>returns a date string 'yyyymmdd'</returns>

        public static string removeDateSeperators(DateTime dte)
        {
            string dy = dte.Day.ToString();
            string myday = dte.Day.ToString();
            string mt = dte.Month.ToString();
            string mth = dte.Month.ToString();
            if (mt.Length == 1)
                mth = "0" + mt;
            if (dy.Length == 1)
                myday = "0" + dy;

            string ky = dte.Year.ToString() + mth + myday;
            return ky;
        }

    /// <summary>
    /// Converts date string in dd/mm/yyyy format to a yyyymmdd format.
    /// </summary>
    /// <param name="dte"> date string in dd/mm/yyyy format</param>
    /// <returns>returns a date string 'yyyymmdd'</returns>
        public static string removeDateSeperators(String dte)
        {
            //split to constituents
            string[] dparts = dte.Split('/');
            if (dparts.Length != 3)
                return "ERROR: Invalid Date Format!";

            string dy = dparts[0];
            string myday = dparts[0];
            string mt = dparts[1];
            string mth = dparts[1];
            if (mt.Length == 1)
                mth = "0" + mt;

            if (dy.Length == 1)
                myday = "0" + dy;

            string ky = dparts[2] + mth + myday;
            return ky;
        }

        public static bool gnTest_TransDate(string MyFunc_Date)
        {
            bool pvbln;
            pvbln = false;

            if (((MyFunc_Date.Length == 10) && (((MyFunc_Date.Substring(2, 1) == "-") || (MyFunc_Date.Substring(2, 1) == "/"))
                && ((MyFunc_Date.Substring(5, 1) == "-")
            || (MyFunc_Date.Substring(5, 1) == "/")))))
            {

            }
            else
            {
                return pvbln;
            }
            string strDteMsg = "Invalid Date";
            string strDteErr = "0";
            //DateTime DteTst;
            string strDte_Start;
            string strDte_End;
            string strDteYY;
            string strDteMM;
            string strDteDD;
            strDteMsg = "";
            strDteErr = "0";
            strDteMsg = "";
            strDteErr = "0";

            strDteDD = MyFunc_Date.Substring(0, 2);
            strDteMM = MyFunc_Date.Substring(3, 2);
            strDteYY = MyFunc_Date.Substring((MyFunc_Date.Length - 4));
            strDteDD = strDteDD.Trim();
            strDteMM = strDteMM.Trim();
            strDteYY = strDteYY.Trim();


            if (((Convert.ToInt16(strDteDD) < 01) || (Convert.ToInt16(strDteDD.Trim()) > 31)))
            {
                strDteMsg = ("  -> Day < 01 or Day > 31 ..." + "\r\n");
                strDteErr = "1";
            }
            if (((Convert.ToInt16(strDteMM.Trim()) < 01)
                        || (Convert.ToInt16(strDteMM.Trim()) > 12)))
            {
                strDteMsg = (strDteMsg + ("  -> Month < 01 or Month > 12 ..." + "\r\n"));
                strDteErr = "1";
            }
            if ((strDteYY.Trim().Length < 4))
            {
                strDteMsg = (strDteMsg + ("  -> Year = 0 digit or Year < 4 digits..." + "\r\n"));
                strDteErr = "1";
            }
            strDte_Start = "";
            strDte_End = "";
            strDte_Start = MyFunc_Date;
            strDte_End = MyFunc_Date;

            switch (strDteMM.Trim())
            {
                case "01":
                case "03":
                case "05":
                case "07":
                case "08":
                case "10":
                case "12":
                    if ((double.Parse(strDteDD) > 31))
                    {
                        strDteMsg = (strDteMsg + ("  -> Invalid day in month. Month <"
                                    + (strDteMM + (">" + (" ends in <" + (" 31 " + (">" + (". Full Date: "
                                    + (strDte_End + "\r\n")))))))));
                        strDteErr = "1";
                    }
                    break;
                case "02":
                    if (double.Parse(strDteYY)%4 == 0)
                    {
                        if ((double.Parse(strDteDD) > 29))
                        {
                            strDteMsg = (strDteMsg + ("  -> Invalid day in month. Month <"
                                        + (strDteMM + (">" + (" ends in <" + (" 29 " + (">" + (". Full Date: "
                                        + (strDte_End + "\r\n")))))))));
                            strDteErr = "1";
                        }
                    }
                    else if ((double.Parse(strDteDD) > 28))
                    {
                        strDteMsg = (strDteMsg + ("  -> Invalid day in month. Month <"
                                    + (strDteMM + (">" + (" ends in <" + (" 28 " + (">" + (". Full Date: "
                                    + (strDte_End + "\r\n")))))))));
                        strDteErr = "1";
                    }
                    break;
                case "04":
                case "06":
                case "09":
                case "11":
                    if ((double.Parse(strDteDD) > 30))
                    {
                        strDteMsg = (strDteMsg + ("  -> Invalid day in month. Month <"
                                    + (strDteMM + (">" + (" ends in <" + (" 30 " + (">" + (". Full Date: "
                                    + (strDte_End + "\r\n")))))))));
                        strDteErr = "1";
                    }
                    break;
            }

            if ((strDteErr != "0"))
            {
                //gnTest_TransDate = false;
                pvbln = false;
            }
            //gnTest_TransDate = true;
            pvbln = true;
            return pvbln;
        }

        public static DateTime moveDateToCulture(String dte)
        {
            //change date from dd/mm/yyyy to mm/dd/yyyy to achieve consonance with the server.
            //It seems that settings from the c# dll calls for date format is changed when it gets to VB.net client. 
            //This is still subject to research before final conclusion though.
            string[] dateparts = dte.Split('/');
            Int16 dy = Convert.ToInt16(dateparts[0]);
            Int16 mt = Convert.ToInt16(dateparts[1]);
            Int16 ky = Convert.ToInt16(dateparts[2]);
            System.DateTime dateInMay = new System.DateTime(ky, mt, dy, 0, 0, 0);
            //String myDate = mt + "/" + dy + "/" + ky;
            return dateInMay;
        }

        public static String DateToServerSetting(String dte)
        {
            //change date from dd/mm/yyyy to mm/dd/yyyy to achieve consonance with the server.
            string[] dateparts = dte.Split('/');
            Int16 dy = Convert.ToInt16(dateparts[0]);
            Int16 mt = Convert.ToInt16(dateparts[1]);
            Int16 ky = Convert.ToInt16(dateparts[2]);
            String myDate = mt + "/" + dy + "/" + ky;
            return myDate;
        }

        public static String DateFromServerSetting(String dte)
        {
            //change date from mm/dd/yyyy to dd/mm/yyyy to achieve consonance with the client.
            string[] dateparts = dte.Split('/');
            Int16 dy = Convert.ToInt16(dateparts[0]);
            Int16 mt = Convert.ToInt16(dateparts[1]);
            Int16 ky = Convert.ToInt16(dateparts[2]);
            String myDate = dy + "/" + mt + "/" + ky;
            return myDate;
        }


        public static Double RealNumberNoSpaces(string str)
        {
            if (str == "")
                return 0;
            return Math.Round(Convert.ToDouble(str), 2);

        }
        public static DateTime RealDateNoSpaces(string str)
        {
            if (str == "")
                return DateTime.Now;
            return Convert.ToDateTime(str);

        }
        public static Double DateDiff(String _startdate, String _enddate)
        {
            TimeSpan ts = Convert.ToDateTime(removeDateSeperators(_enddate)) - Convert.ToDateTime(removeDateSeperators((_startdate)));
            return ts.TotalDays;   
        }

        public static void postFromExcel(String _uploadpath, String _filename, String _username, String _batchno,
            String _minRange, String _maxRange, String _tenor, String _connstring,
            String _prem_sa_factor, String _filenum, String _quote_num, String _poly_num, String _prem_Rate_TypeNum,
            String _prem_rate_per, String _prem_rate_code, String _product_Num,ref List<String> _err_msg, 
            ref int _risk_days, ref int _days_diff, string _genstart_date, string _genend_date,
            string _dtestart, string _dteend, string _memjoin_date, string _data_source_sw, string _prem_Rate, string _entry_Date)
        {
            string strMyYear = "";
            string strMyMth = "";
            string strMyDay = "";
            string strMyDte = "";
            string mydteX = "";
            DateTime mydte = DateTime.Now;
            int lngDOB_ANB = 0;
            DateTime Dte_Current = DateTime.Now;
            DateTime Dte_DOB = DateTime.Now;
            string sFT = "";
            float nRow = 0;
            int nCol = 1;
            int nROW_MIN = int.Parse(_minRange);
            int nROW_MAX = int.Parse(_maxRange);
            string xx = "";
            string my_Batch_Num = _batchno;
            long my_intCNT = 0;
            string my_SNo = "";

            var errmsg = new List<String>();

            double dblPrem_Rate = 0;
            int dblPrem_Rate_Per = 0;
            double dblPrem_Amt = 0;
            double dblPrem_Amt_ProRata = 0;
            double dblLoad_Amt = 0;
            double dblTotal_Salary = 0;
            double dblTotal_SA = 0;
            double dblFree_Cover_Limit = 0;
            
            DateTime my_Dte_DOB = DateTime.Now;
            DateTime my_Dte_Start = DateTime.Now;
            DateTime my_Dte_End = DateTime.Now;
            string my_File_Num = _filenum;
            string my_Prop_Num = _quote_num;
            string my_Poly_Num = _poly_num;
            string my_Staff_Num = "";
            string my_Member_Name = "";
            string my_DOB = "";
            string my_AGE = "";
            string my_Gender = "";
            string my_Designation = "";
            string my_Start_Date = "";
            string my_End_Date = "";
            string my_Tenor = "";
            float my_SA_Factor = 0;
            double my_Basic_Sal = 0;
            double my_House_Allow = 0;
            double my_Transport_Allow = 0;
            double my_Other_Allow = 0;
            double my_Total_Salary = 0;
            double my_Total_SA = 0;
            string my_Medical_YN = "";
            string myRetValue = "0";
            string myTerm = "";
            string dobtest;
            string startdatetest;

            string enddatetest;
            string strGen_Msg = String.Empty;
            string entry_date = String.Empty;


            CultureInfo cu = new CultureInfo("en-GB");
            DateTime dte = DateTime.Today;
            string mystr_con = "";
            string mystr_sql = "";
            bool mybln = false;
            mybln = false;
            string mystr_sn_param = "";
            mystr_sn_param = "GL_MEMBER_SN";
            int mycnt = 0;

            if (_entry_Date == String.Empty)
                entry_date = "getdate()";
            else
                entry_date = "'"+ _entry_Date + "'";

            OleDbConnection myole_con = null;
            OleDbCommand myole_cmd = null;
            mystr_con = _connstring;
            myole_con = new OleDbConnection(mystr_con);
            // remove the 'provider' attribute from the connection string becsuse it is for connection to oledb providers but not for sqlclient
            string[] connParts = {};
            connParts = _connstring.Split(';');

            string mySQLClientConn = connParts[1] + ';' + connParts[2] + ';' + connParts[3] + ';' + connParts[4] + ';';
            String ftm = "Y";
            String ftime = "Y";
            sFT = "Y";




            //obtain the parameters
            string wksheetName = "[MEMBERS$]"; // _filename.ToString(); // _criteria.Substring(3, _criteria.Length - 3);
            //string transtype = _criteria.Substring(0, 3);

           // dte = DateTime.Parse(_txtDateIn, cu);
           // string transdate = hashHelper.removeDateSeperators(dte);

            //string uploadedfilefpath = Server.MapPath(@"~\Docs\monthtransactions.xls");
            string uploadedfilepath = _uploadpath + _filename;
            //            string connectionStringExcel = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + uploadedfilefpath + @";Extended Properties=""Excel 8.0;HDR=YES;""";
            //string connectionStringExcel = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + uploadedfilepath.ToString() + @";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""";
            string ext_property = "Excel 8.0;HDR=YES;IMEX=1;";
            string connectionStringExcel = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + uploadedfilepath.ToString() + ";Extended Properties='" + ext_property + "'";
            string connectionStringMySql = String.Empty;
            connectionStringMySql = mySQLClientConn;

            DbProviderFactory factoryExcel = DbProviderFactories.GetFactory("System.Data.OleDb");
            DbProviderFactory factorySQL = DbProviderFactories.GetFactory("System.Data.SqlClient");

            using (DbConnection connectionExcel = factoryExcel.CreateConnection(),
                   connectionSQL = factorySQL.CreateConnection())
            {
                connectionExcel.ConnectionString = connectionStringExcel;
                connectionSQL.ConnectionString = connectionStringMySql;
                using (DbCommand commandExcel = connectionExcel.CreateCommand(),
                    commandSQL = connectionSQL.CreateCommand())
                {
                    //commandExcel.CommandText = "SELECT "
                    //                           + wksheetName + ".[Serial No],"
                    //                           + wksheetName + ".PCN,"
                    //                           + wksheetName + ".Member_name,"
                    //                           + wksheetName + ".DOB,"
                    //                           + wksheetName + ".Age,"
                    //                           + wksheetName + ".Gender,"
                    //                           + wksheetName + ".Start_Date,"
                    //                           + wksheetName + ".End_Date,"
                    //                           + wksheetName + ".Tenor,"
                    //                           + wksheetName + ".Factor,"
                    //                           + wksheetName + ".Basic_Salary,"
                    //                           + wksheetName + ".Housing_Allow,"
                    //                           + wksheetName + ".Transport_Allow,"
                    //                           + wksheetName + ".Other_Allow,"
                    //                           + wksheetName + ".Total_Emolument,"
                    //                           + wksheetName + ".Other_Allow"
                    //                           + " FROM " + wksheetName;

                    //commandExcel.CommandText = "SELECT [SCH2014$].*"
                    //                           + " FROM [SCH2014$]";// + wksheetName.ToString() + "$";

                    commandExcel.CommandText = "SELECT "
                                               + wksheetName + ".*"
                                               + " FROM " + wksheetName;

                MyLoop_Start:

                    try
                    {
                        connectionExcel.Open();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ERROR!: " + ex.Message);
                    }


                    using (DbDataReader dr = commandExcel.ExecuteReader())
                    {

                        try
                        {
                            int r;
                            int cnt = 0;
                            connectionSQL.Open();

                            while (dr.Read())
                            {

                                nRow += 1; //row
                                if ((nRow < nROW_MIN))
                                {
                                    //goto MyLoop_Start;

                                    break;
                                    
                                }
                                if ((nRow > nROW_MAX))
                                {
                                    //goto MyLoop_999;
                                    break;
                                }
                                if (ftm == "Y")
                                {
                                    ftm = "N";

                                    nRow -= 1;
                                }


                                //test and validate fields before inserting into the database
                                //validation code
                                my_Staff_Num = dr["PCN"].ToString();
                                my_Member_Name = dr["Member_name"].ToString();
                                // Validate DOB

                                my_DOB =  dr["DOB"].ToString().Substring(0,10);
                                my_Start_Date = String.Format("{0:dd/MM/yyyy}", dr["Start_Date"].ToString());
                                my_End_Date = String.Format("{0:dd/MM/yyyy}", dr["End_Date"].ToString()) ;
                                my_AGE = dr["Age"].ToString();
                                my_Gender = dr["Gender"].ToString();
                                my_Designation = dr["Designation"].ToString();
                                my_Tenor = dr["Tenor"].ToString();
                                my_SA_Factor = float.Parse(dr["Factor"].ToString());
                                my_Basic_Sal = double.Parse(dr["Basic_Salary"].ToString());
                                my_House_Allow = double.Parse(dr["Housing_Allow"].ToString());
                                my_Transport_Allow = double.Parse(dr["Transport_Allow"].ToString());
                                my_Other_Allow = double.Parse(dr["Other_Allow"].ToString());
                                my_Total_Salary = double.Parse(dr["Total_Emolument"].ToString());



                                string[] myarrData = my_DOB.Split('/');
                                if ((myarrData.Length != 3))
                                {
                                   strGen_Msg = (" * Row: "
                                                + (nRow.ToString() + (" - Incomplete date of birth - " + my_DOB.ToString())));
                                   if (ftime == "Y")
                                   {
                                       ftime = "N";
                                       _err_msg = ErrRoutine(strGen_Msg);
                                   }
                                    else
                                       _err_msg.Add(ErrRoutine(strGen_Msg).ToString());

                                    continue;

                                    //goto MyLoop_888;
                                }
                                strMyDay = myarrData[0];
                                strMyMth = myarrData[1];
                                strMyYear = myarrData[2].Substring(0, 4);
                                strMyDay = double.Parse(strMyDay).ToString("00");
                                strMyMth = double.Parse(strMyMth).ToString("00");
                                strMyYear = double.Parse(strMyYear).ToString("0000");
                                strMyDte = (strMyDay.Trim() + ("/"
                                            + (strMyMth.Trim() + ("/" + strMyYear.Trim()))));

                                
                                if ((!gnTest_TransDate(strMyDte)))
                                {
                                    strGen_Msg = (" * Row: "
                                                + (nRow.ToString() + (" - Invalid date of birth - " + strMyDte.ToString())));
                                    if (ftime == "Y")
                                    {
                                        ftime = "N";
                                        _err_msg = ErrRoutine(strGen_Msg);
                                    }
                                    else
                                        _err_msg.Add(ErrRoutine(strGen_Msg).ToString());
                                    continue;

                                    //goto MyLoop_888;
                                }

                                 try
                                 {
                                     dobtest = removeDateSeperators(strMyDte);
                                     if (dobtest.Substring(0, 5) != "ERROR")
                                         my_DOB = dobtest;
                                     else
                                         throw new Exception();

                                     string datetoday = removeDateSeperators(DateTime.Now.Date);
                                     string sAge = ExecuteAdHocQry("SELECT * FROM CiFn_ValidateDOB('" + dobtest + "','"+datetoday+ "')", _connstring);
                                     my_AGE = sAge;
                                 }
                                 catch(Exception e) {
                                     //throw new Exception("Invalid Date Of Birth");
                                                         strGen_Msg = (" * Row: "
                                    + (nRow.ToString() + (" - Invalid date of birth - " + strMyDte.ToString())));
                                                         if (ftime == "Y")
                                                         {
                                                             ftime = "N";
                                                             _err_msg = ErrRoutine(strGen_Msg);
                                                         }
                                                         else
                                                             _err_msg.Add(ErrRoutine(strGen_Msg).ToString());
                                                         continue;

//                                     goto MyLoop_888;

                                 }


                                //test start date

                                 myarrData = my_Start_Date.Split('/');
                                 if ((myarrData.Length != 3))
                                 {
                                     strGen_Msg = (" * Row: "
                                                  + (nRow.ToString() + (" - Incomplete start date - " + my_Start_Date.ToString())));
                                     if (ftime == "Y")
                                     {
                                         ftime = "N";
                                         _err_msg = ErrRoutine(strGen_Msg);
                                     }
                                     else
                                         _err_msg.Add(ErrRoutine(strGen_Msg).ToString());
                                     continue;

//                                     goto MyLoop_888;
                                 }

                                 strMyDay = myarrData[0];
                                 strMyMth = myarrData[1];
                                 strMyYear = myarrData[2].Substring(0, 4);
                                 strMyDay = double.Parse(strMyDay).ToString("00");
                                 strMyMth = double.Parse(strMyMth).ToString("00");
                                 strMyYear = double.Parse(strMyYear).ToString("0000");
                                 strMyDte = (strMyDay.Trim() + ("/"
                                             + (strMyMth.Trim() + ("/" + strMyYear.Trim()))));


                                 if ((!gnTest_TransDate(strMyDte)))
                                 {
                                     strGen_Msg = (" * Row: "
                                                 + (nRow.ToString() + (" - Invalid start date - " + strMyDte.ToString())));

                                     if (ftime == "Y")
                                     {
                                         ftime = "N";
                                         _err_msg = ErrRoutine(strGen_Msg);
                                     }
                                     else
                                         _err_msg.Add(ErrRoutine(strGen_Msg).ToString());
                                     continue;
                                      //goto MyLoop_888;
                                 }

                                 try
                                 {

                                     startdatetest = removeDateSeperators(strMyDte);
                                     if (startdatetest.Substring(0, 5) != "ERROR")
                                         my_Start_Date = startdatetest;
                                     else
                                         throw new Exception();

                                 }
                                 catch (Exception e)
                                 {
                                     //throw new Exception("Invalid Date Of Birth");
                                     strGen_Msg = (" * Row: "
                                    + (nRow.ToString() + (" - Invalid Start Date - " + strMyDte.ToString())));
                                     if (ftime == "Y")
                                     {
                                         ftime = "N";
                                         _err_msg = ErrRoutine(strGen_Msg);
                                     }
                                     else
                                         _err_msg.Add(ErrRoutine(strGen_Msg).ToString());
                                     continue;

                                     //goto MyLoop_888;

                                 }


                                 //test end date

                                 myarrData = my_End_Date.Split('/');
                                 if ((myarrData.Length != 3))
                                 {
                                     strGen_Msg = (" * Row: "
                                                  + (nRow.ToString() + (" - Incomplete end date - " + my_End_Date.ToString())));
                                     if (ftime == "Y")
                                     {
                                         ftime = "N";
                                         _err_msg = ErrRoutine(strGen_Msg);
                                     }
                                     else
                                         _err_msg.Add(ErrRoutine(strGen_Msg).ToString());
                                     continue;

                                     //goto MyLoop_888;
                                 }

                                 strMyDay = myarrData[0];
                                 strMyMth = myarrData[1];
                                 strMyYear = myarrData[2].Substring(0, 4);
                                 strMyDay = double.Parse(strMyDay).ToString("00");
                                 strMyMth = double.Parse(strMyMth).ToString("00");
                                 strMyYear = double.Parse(strMyYear).ToString("0000");
                                 strMyDte = (strMyDay.Trim() + ("/"
                                             + (strMyMth.Trim() + ("/" + strMyYear.Trim()))));


                                 if ((!gnTest_TransDate(strMyDte)))
                                 {
                                     strGen_Msg = (" * Row: "
                                                 + (nRow.ToString() + (" - Invalid end date - " + strMyDte.ToString())));
                                     if (ftime == "Y")
                                     {
                                         ftime = "N";
                                         _err_msg = ErrRoutine(strGen_Msg);
                                     }
                                     else
                                         _err_msg.Add(ErrRoutine(strGen_Msg).ToString());
                                     continue;

                                     //goto MyLoop_888;
                                 }

                                 try
                                 {

                                     enddatetest = removeDateSeperators(strMyDte);
                                     if (enddatetest.Substring(0, 5) != "ERROR")
                                         my_End_Date = enddatetest;
                                     else
                                         throw new Exception();

                                 }
                                 catch (Exception e)
                                 {
                                     //throw new Exception("Invalid Date Of Birth");
                                     strGen_Msg = (" * Row: "
                                    + (nRow.ToString() + (" - Invalid End Date - " + strMyDte.ToString())));
                                     if (ftime == "Y")
                                     {
                                         ftime = "N";
                                         _err_msg = ErrRoutine(strGen_Msg);
                                     }
                                     else
                                         _err_msg.Add(ErrRoutine(strGen_Msg).ToString());
                                     continue;

//                                     goto MyLoop_888;

                                 }


                                my_Total_SA = 0;

                                my_Tenor = _tenor;
                                myTerm = my_Tenor;


                                if (sFT.Trim() == "Y")
                                {
                                    sFT = "N";
                                    mystr_sql = "";
                                    mystr_sql = "delete from TBIL_GRP_POLICY_MEMBERS";
                                    mystr_sql = (mystr_sql + (" where TBIL_POL_MEMB_FILE_NO = '"
                                                + (my_File_Num.TrimEnd() + "'")));
                                    mystr_sql = (mystr_sql + (" and TBIL_POL_MEMB_PROP_NO = '"
                                                + (my_Prop_Num.TrimEnd() + "'")));
                                    mystr_sql = (mystr_sql + (" and TBIL_POL_MEMB_BATCH_NO = '"
                                                + my_Batch_Num + "'"));
                                    myole_cmd = new OleDbCommand(mystr_sql, myole_con);
                                    myole_cmd.CommandType = CommandType.Text;
                                    if (myole_con.State == ConnectionState.Closed)
                                        myole_con.Open();
                                    myole_cmd.ExecuteNonQuery();
                                    myole_cmd.Dispose();
                                    myole_cmd = null;
                                    mystr_sql = ("delete from TBIL_UNDW_SYS_GEN_CNT where TBIL_SYS_GEN_CNT_ID = '"
                                                + (mystr_sn_param.TrimEnd() + ("' and TBIL_SYS_GEN_CNT_CODE = '"
                                                + (my_File_Num.TrimEnd() + "'"))));
                                    myole_cmd = new OleDbCommand(mystr_sql, myole_con);
                                    myole_cmd.CommandType = CommandType.Text;
                                    myole_cmd.ExecuteNonQuery();
                                    myole_cmd.Dispose();
                                    myole_cmd = null;
                                }


                                dblPrem_Rate = 0;
                                dblPrem_Rate_Per = 1000;
                                dblPrem_Amt = 0;
                                dblPrem_Amt_ProRata = 0;
                                dblLoad_Amt = 0;
                                if ((my_SA_Factor) == 0)
                                {
                                    my_SA_Factor = float.Parse(_prem_sa_factor);
                                }
                                dblTotal_SA = my_Total_Salary;
                                if (my_SA_Factor != 0)
                                {
                                    dblTotal_SA = my_Total_Salary * my_SA_Factor;
                                }
                                my_Total_SA = dblTotal_SA;
                                if ((dblTotal_SA >= dblFree_Cover_Limit))
                                {
                                    my_Medical_YN = "Y";
                                }

                                my_SNo = gnGet_Serial_No("GET_SN_GL", "GL_MEMBER_SN", _filenum, _quote_num, _connstring);

                                if ((my_Staff_Num.Trim() == ""))
                                {
                                    my_Staff_Num = "STF_" + my_SNo.ToString();
                                }

                                    switch (_prem_Rate_TypeNum)
                                    {
                                        case "F":
                                            dblPrem_Rate = double.Parse(_prem_Rate);
                                            dblPrem_Rate_Per = int.Parse(_prem_rate_per);
                                            break;
                                        case "N":
                                            dblPrem_Rate = double.Parse("0.00");
                                            dblPrem_Rate_Per = int.Parse("0");
                                            break;
                                        case "T":
                                            myRetValue = gnGET_RATE("GET_GL_PREMIUM_RATE", "GRP", _prem_rate_code, _product_Num, myTerm, my_AGE, "", ref _prem_rate_per, String.Empty, _connstring);
                                            if ((myRetValue.TrimStart().Substring(0, 3) == "ERR"))
                                            {
                                                dblPrem_Rate = double.Parse("0.0");
                                                dblPrem_Rate_Per = int.Parse("0");
                                            }
                                            else
                                            {
                                                // Me.txtPrem_Rate.Text = myRetValue.ToString
                                                dblPrem_Rate = double.Parse(myRetValue);
                                            }
                                            break;
                                    }

                                    if (((dblTotal_SA != 0) && ((dblPrem_Rate != 0 && (dblPrem_Rate_Per != 0)))))
                                    {
                                        dblPrem_Amt = (dblTotal_SA
                                                    * (dblPrem_Rate / dblPrem_Rate_Per));
                                        dblPrem_Amt_ProRata = dblPrem_Amt;
                                    }

                                    //_risk_days = Convert.ToInt16(DateDiff(_genstart_date, _genend_date) + 0);
                                    _days_diff = _risk_days;
                                    //_days_diff = Convert.ToInt16(DateDiff(_dtestart, _dteend));
                                    if (((Convert.ToDateTime(_memjoin_date) > Convert.ToDateTime(_genstart_date)) && ((dblPrem_Amt != 0) && (_days_diff != 0))))
                                    {
                                        //Convert.ToDecimal(((dblPrem_Amt / _risk_days)
                                        //            * _days_diff)).ToString("#,##0.00");

                                        dblPrem_Amt_ProRata = Convert.ToDouble(((dblPrem_Amt / _risk_days)
                                                        * _days_diff));

                                    }

                                    
                                    //insert into DB
                                    commandSQL.CommandText = "INSERT INTO TBIL_GRP_POLICY_MEMBERS values ("
                                        + "'" + _filenum + "' "
                                        + ",'G'"
                                        + ",'" + _quote_num + "' "
                                        + ",null" // null passed into policy num deliberately. The original sproc used for inserts does not contain any. 
                                        + ",'" + _batchno + "' "
                                        + ",'" + my_Staff_Num + "' "
                                        + ",'" + my_SNo.Trim() + "' "
                                        + ",null "    //transdate = null
                                        + ",'Q' "
                                        + ",null " //category
                                        + ",'" + my_DOB.ToString() + "' "
                                        + ",'" + my_AGE.ToString() + "' "
                                        + ",'" + my_Start_Date.ToString() + "' "
                                        + ",'" + my_End_Date.ToString() + "' "
                                        + ",null"  //tenor
                                        + ",'" + my_Designation.ToString() + "' "
                                        + ",'" + my_Member_Name.ToString() + "' "
                                        + ",'" + my_SA_Factor.ToString() + "' "
                                        + ",'" + my_Total_Salary.ToString() + "' "
                                        + ",'" + my_Total_SA.ToString() + "' "
                                        + ",'" + my_Medical_YN.ToString() + "' "
                                        + ",'" + dblPrem_Rate.ToString() + "' "
                                        + ",'" + dblPrem_Rate_Per.ToString() + "' "
                                        + ",'" + dblPrem_Amt.ToString() + "' "
                                        + ",'" + dblPrem_Amt_ProRata.ToString() + "' "
                                        + ",'" + dblLoad_Amt + "' "
                                        + ",'" + _data_source_sw + "' "
                                        + ",'" + _filename.ToString() + "' "
                                        + ",null "
                                        + ",'A' "
                                        + ",'" + _username.Trim() + "' "
                                        + ", " + entry_date

                                        + " )";

                                    try {
                                           r = commandSQL.ExecuteNonQuery();

                                        if ((r >= 1)) {
                                            my_intCNT = my_intCNT + 1;
                                    }
                                    else {
                                        strGen_Msg = (" * Error!. Row: " 
                                                    + (nRow.ToString() + " record not save... "));
                                        if (ftime == "Y")
                                        {
                                            ftime = "N";
                                            _err_msg = ErrRoutine(strGen_Msg);
                                        }
                                        else
                                            _err_msg.Add(ErrRoutine(strGen_Msg).ToString());
                                        continue;

//                                            goto MyLoop_888;
                                    }
                                }
                                catch (Exception ex) {
                                    strGen_Msg = (" * Error while saving Row: " 
                                                + (nRow.ToString() + " record... "));
                                    if (ftime == "Y")
                                    {
                                        ftime = "N";
                                        _err_msg = ErrRoutine(strGen_Msg);
                                    }
                                    else
                                        _err_msg.Add(ErrRoutine(strGen_Msg).ToString());
                                    continue;

//                                    goto MyLoop_888;

                                }

                                } //while read end
                            }
                        
                        catch (Exception h)
                        {
                            strGen_Msg = (" * General System Error : "
                                        + (h.ToString() ));
                            if (ftime == "Y")
                            {
                                ftime = "N";
                                _err_msg = ErrRoutine(strGen_Msg);
                                _err_msg.Add(ErrRoutine(strGen_Msg).ToString());

                            }
                            else
                                _err_msg.Add(ErrRoutine(strGen_Msg).ToString());
                            
                            goto MyLoop_End_2;
                            //throw new Exception("ERROR!: " + "may not be setup. Pls Check this number against a valid name and setup it up as a member");
                        }

                    }//end data reader


        MyLoop_888:

                    if ((strGen_Msg != ""))
                    {
                        errmsg.Add(strGen_Msg.ToString());
                        //_err_msg = errmsg;

                    }
                    strGen_Msg = "";
                    connectionSQL.Close();
                    connectionExcel.Close();

                    //goto MyLoop_Start;
                MyLoop_999:
                    try
                    {
                        //ClientScript.RegisterStartupScript(this.GetType(), "scrollMSG_JavaScript", ("scrollMSG_End(" + ("\'" 
                        //                + (this.SB_CONT.ClientID + ("\'" + (",\'" 
                        //                + (this.SB_MSG.ClientID + ("\'" + ")"))))))), true);
                    }
                    catch (Exception ex)
                    {
                    }
                    if ((my_intCNT >= 1))
                    {
                        //FirstMsg = ("Javascript:alert(\'" 
                        //            + ("File Upload successful - ".TrimEnd() 
                        //            + (this.txtFile_Upload.Text + "\')")));
                    }
                    else
                    {
                        //FirstMsg = ("Javascript:alert(\'" 
                        //            + ("File Upload NOT successful - ".TrimEnd() 
                        //            + (this.txtFile_Upload.Text + "\')")));
                    }
                MyLoop_End:
                    my_intCNT = 1;

                MyLoop_End_1:
                    _err_msg = ErrRoutine("Successful!");

                MyLoop_End_2:

                    connectionSQL.Close();
                    connectionExcel.Close();

                }

            }
        }
        protected static List<String> ErrRoutine(String msg)
        {
             var errm = new List<String>();

            if ((msg != ""))
            {
                errm.Add(msg.ToString());
                // errmsgs.ErrorMsgs.Add(msg.ToString());
                errmsgs.ErrorMsgs = errm;

            }
            return errmsgs.ErrorMsgs;

}

        public static string gnGet_Serial_No(string pvCODE, string pvRef_Type, string pvRef_Code_A, string pvRef_Code_B, String _connstring)
        {
            string pvSerialNum;
            string pvSQL;
            int intC = 0;
            pvSerialNum = "0";
            pvSQL = "";
            pvSerialNum = "ERR_ERR";
            switch (pvCODE.TrimEnd().ToUpper()) {
                case "GET_SN_IL":
                    switch (pvRef_Type.TrimEnd()) {
                        case "BENEF_SN":
                        case "FUN_SN":
                        case "FUN_COVER_SN":
                            pvSQL = "SPIL_GET_UNDW_GEN_CNT";
                            break;
                        default:
                            pvSerialNum = "PARAM_ERR";
                            return pvSerialNum;
                    }
                    break;
                case "GET_SN_GL":
                    switch (pvRef_Type.TrimEnd()) {
                        case "GL_MEMBER_SN":
                        case "GL_BENEF_SN":
                        case "GL_FUN_SN":
                        case "GL_FUN_COVER_SN":
                            pvSQL = "SPGL_GET_UNDW_GEN_CNT";
                            break;
                        default:
                            pvSerialNum = "PARAM_ERR";
                            return pvSerialNum;
                            
                    }
                    break;
                default:
                    pvSerialNum = "PARAM_ERR";
                    return pvSerialNum;
                    
                }

            string mystrCONN;
            // mystrCONN = CType(ConfigurationManager.AppSettings("APPCONN"), String)
            mystrCONN = _connstring;  //gnGET_CONN_STRING();
            mystrCONN = ("Provider=SQLOLEDB;" + mystrCONN);
            OleDbConnection objOLEConn = new OleDbConnection(mystrCONN);
            try
            {
                // open connection to database
                objOLEConn.Open();
            }
            catch (Exception ex)
            {
                objOLEConn = null;
                return "DB_ERR";
            }
            OleDbCommand objOLECmd = new OleDbCommand(pvSQL, objOLEConn);
            objOLECmd.CommandType = CommandType.StoredProcedure;
            objOLECmd.Parameters.Clear();

            switch (pvRef_Type)
            {
                case "BENEF_SN":
                case "FUN_SN":
                case "FUN_COVER_SN":
                    objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 20).Value = pvRef_Type.TrimEnd();
                    objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = pvRef_Code_A.TrimEnd();
                    objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 40).Value = pvRef_Code_B.TrimEnd();
                    objOLECmd.Parameters.Add("p04", OleDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    objOLECmd.Parameters.Add("p05", OleDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    break;
                case "GL_MEMBER_SN":
                case "GL_BENEF_SN":
                case "GL_FUN_SN":
                case "GL_FUN_COVER_SN":
                    objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 20).Value = pvRef_Type.TrimEnd();
                    objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = pvRef_Code_A.TrimEnd();
                    objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 40).Value = pvRef_Code_B.TrimEnd();
                    objOLECmd.Parameters.Add("p04", OleDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    objOLECmd.Parameters.Add("p05", OleDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    break;
                default:
                    objOLECmd = null;
                    pvSerialNum = "PARAM_ERR";
                    return pvSerialNum;
            }
            switch (pvRef_Type.TrimEnd())
            {
                case "BENEF_SN":
                case "FUN_SN":
                case "FUN_COVER_SN":
                    intC = objOLECmd.ExecuteNonQuery();
                    // pvSerialNum = CType(objOLECmd.Parameters("p10").Value & vbNullString, String)
                    // Call gnASPNET_MsgBox("Serial No: " & pvSerialNum)
                    pvSerialNum = ((string)((objOLECmd.Parameters["p04"].Value + null)));
                    objOLECmd.Dispose();
                    if ((objOLEConn.State == ConnectionState.Open))
                    {
                        objOLEConn.Close();
                    }
                    objOLEConn = null; break;
                case "GL_MEMBER_SN":
                case "GL_BENEF_SN":
                case "GL_FUN_SN":
                case "GL_FUN_COVER_SN":
                    intC = objOLECmd.ExecuteNonQuery();
                    // pvSerialNum = CType(objOLECmd.Parameters("p10").Value & vbNullString, String)
                    // Call gnASPNET_MsgBox("Serial No: " & pvSerialNum)
                    pvSerialNum = ((string)((objOLECmd.Parameters["p04"].Value + null)));
                    objOLECmd.Dispose();
                    if ((objOLEConn.State == ConnectionState.Open))
                    {
                        objOLEConn.Close();
                    }
                    objOLEConn = null; 
                    break;
            }
            return pvSerialNum;

            //if ((objOLEConn.State == ConnectionState.Open))
            //{
            //    objOLEConn.Close();
            //}
            //objOLEConn = null;
            //pvSerialNum = "ERR_ERR";
            //return pvSerialNum;
        }


        // Retrieve a connection string by specifying the providerName. 
        // Assumes one connection string per provider in the config file. 
        static string GetConnectionStringByProvider(string providerName)
        {
            // Return null on failure. 
            string returnValue = null;

            // Get the collection of connection strings.
            ConnectionStringSettingsCollection settings =
                ConfigurationManager.ConnectionStrings;

            // Walk through the collection and return the first  
            // connection string matching the providerName. 
            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    if (cs.ProviderName == providerName)
                        returnValue = cs.ConnectionString;
                    break;
                }
            }
            return returnValue;
        }




    static string gnGET_RATE(string pvstr_GET_WHAT, 
           string pvstr_MODULE, 
           string pvstr_RATE_CODE, 
           string pvstr_PRODUCT_REF_CODE, 
           string pvstr_PERIOD, 
           string pvstr_AGE, 
           string pvCtr_Label,  
           ref string  pvRef_Misc, 
           string pvRef_Misc_02,
           string connString) {


               string mystr_conn = "";
               string mystr_Table = "";
               string mystr_SQL = "";
               string mystr_Key = "";
               int myint_C = 0;
               string myRetValue = "0";
               mystr_conn = connString;
               mystr_conn = ("Provider=SQLOLEDB;" + mystr_conn);
               OleDbConnection myole_CONN = null;
               myole_CONN = new OleDbConnection(mystr_conn);
               try
               {
                   //  Open connection
                   myole_CONN.Open();
               }
               catch (Exception ex)
               {
                   myole_CONN = null;
                   if (pvCtr_Label != null)
                   {
                           pvCtr_Label = "Error. " + ex.Message.ToString();
                   }
                   return "ERR_CON";
               }
               mystr_SQL = "";
               mystr_SQL = "";
               OleDbCommand myole_CMD = new OleDbCommand();
               myole_CMD.Connection = myole_CONN;

               switch (pvstr_GET_WHAT.Trim())
               {
                   case "GET_IL_PREMIUM_RATE":
                       mystr_SQL = "SPIL_GET_PREM_RATE";
                       myole_CMD.CommandType = CommandType.StoredProcedure;
                       myole_CMD.CommandText = mystr_SQL;
                       myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = pvstr_MODULE.TrimEnd();
                       myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 10).Value = pvstr_RATE_CODE.TrimEnd();
                       myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 10).Value = pvstr_PRODUCT_REF_CODE.TrimEnd();
                       myole_CMD.Parameters.Add("@p04", OleDbType.VarChar, 4).Value = pvstr_PERIOD.TrimEnd();
                       myole_CMD.Parameters.Add("@p05", OleDbType.VarChar, 4).Value = pvstr_AGE.TrimEnd();
                       break;
                   case "GET_GL_PREMIUM_RATE":
                       mystr_SQL = "SPGL_GET_PREM_RATE";
                       myole_CMD.CommandType = CommandType.StoredProcedure;
                       myole_CMD.CommandText = mystr_SQL;
                       myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = pvstr_MODULE.TrimEnd();
                       myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 10).Value = pvstr_RATE_CODE.TrimEnd();
                       myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 10).Value = pvstr_PRODUCT_REF_CODE.TrimEnd();
                       myole_CMD.Parameters.Add("@p04", OleDbType.VarChar, 4).Value = pvstr_PERIOD.TrimEnd();
                       myole_CMD.Parameters.Add("@p05", OleDbType.VarChar, 4).Value = pvstr_AGE.TrimEnd();
                       break;
                   case "GET_IL_EXCHANGE_RATE":
                   case "GET_GL_EXCHANGE_RATE":
                       mystr_SQL = "SPIL_GET_EXCHANGE_RATE";
                       myole_CMD.CommandType = CommandType.StoredProcedure;
                       myole_CMD.CommandText = mystr_SQL;
                       myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = pvstr_MODULE.TrimEnd();
                       myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 10).Value = pvstr_RATE_CODE.TrimEnd();
                       myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 10).Value = pvstr_PRODUCT_REF_CODE.TrimEnd();
                       break;
                   case "GET_IL_MOP_FACTOR":
                   case "GET_GL_MOP_FACTOR":
                       mystr_SQL = "SPIL_GET_MOP_FACTOR";
                       myole_CMD.CommandType = CommandType.StoredProcedure;
                       myole_CMD.CommandText = mystr_SQL;
                       myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = pvstr_MODULE.TrimEnd();
                       myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 10).Value = pvstr_RATE_CODE.TrimEnd();
                       myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 10).Value = pvstr_PRODUCT_REF_CODE.TrimEnd();
                       break;
                   default:
                       myole_CMD = null;
                       myole_CONN = null;
                       if (pvCtr_Label != null )
                       {
                               pvCtr_Label= "Error. Invalid parameter: " + pvstr_GET_WHAT.ToString();
                           
                       }
                       return "ERR_PARAM";
               }

                OleDbDataReader myole_DR;
               try
               {
                   myole_DR = myole_CMD.ExecuteReader();
                   //  with the new data reader parse values and place into the return variable
                   if (myole_DR.Read())
                   {
                       // Me.UserCode.Text = Me.UserName.Text & " - " & oleDR("pwd_code").ToString & vbNullString
                       switch (pvstr_GET_WHAT.Trim())
                       {
                           case "GET_IL_PREMIUM_RATE":
                           case "GET_GL_PREMIUM_RATE":
                               myRetValue = (myole_DR["TBIL_PRM_RT_RATE"] ).ToString();
                               if ((pvRef_Misc == null))
                               {

                               }
                               else
                               {
                                   pvRef_Misc = myole_DR["TBIL_PRM_RT_PER"].ToString() ;
                               }
                               break;
                           case "GET_IL_EXCHANGE_RATE":
                           case "GET_GL_EXCHANGE_RATE":
                               myRetValue = myole_DR["TBIL_EXCH_RATE"].ToString();
                               break;
                           case "GET_IL_MOP_FACTOR":
                           case "GET_GL_MOP_FACTOR":
                               myRetValue = myole_DR["TBIL_MOP_RATE"].ToString();
                               if ((pvRef_Misc == null))
                               {

                               }
                               else
                               {
                                   pvRef_Misc = myole_DR["TBIL_MOP_TYPE_DESC"].ToString();
                               }
                               if ((pvRef_Misc_02 == null))
                               {

                               }
                               else
                               {
                                   pvRef_Misc_02 = myole_DR["TBIL_MOP_DIVIDE"].ToString();
                               }
                               break;
                           default:
                               myRetValue = "ERR_PARAM";
                               if (pvCtr_Label !=null)
                               {
                                       pvCtr_Label = "Error. Invalid parameter: " + pvstr_GET_WHAT.ToString();
                               }
                               break;
                       }
                       myole_DR.Close();
                       myole_CMD.Dispose();
                   }
                   else
                   {
                       myRetValue = "ERR_RNF";
                       if (pvCtr_Label != null)
                       {
                               pvCtr_Label = "Record not found for parameters supplied...";
                       }
                   }
               }
               catch (Exception ex)
               {
                   //    'Throw ex
                   myRetValue = "ERR";
                   if (pvCtr_Label != null)
                   {
                           pvCtr_Label = "Error. " + ex.Message.ToString();
                   }
               }
               // myole_DA.Dispose()
               try
               {
                   //  Close connection
                   myole_CONN.Close();
               }
               catch (Exception ex)
               {
               }
               // myobj_ds = Nothing
               // myole_DA = Nothing
               myole_DR = null;
               myole_CMD = null;
               myole_CONN = null;
               return myRetValue;
           }


    private static String ExecuteAdHocQry(string _qry, String _conn)
    {

        String result = String.Empty;
        string[] connParts = { };
        connParts = _conn.Split(';');

        string mySQLClientConn = connParts[1] + ';' + connParts[2] + ';' + connParts[3] + ';' + connParts[4] + ';';

        using (SqlConnection conn = new SqlConnection(mySQLClientConn))
        using (SqlCommand cmd = new SqlCommand(_qry, conn))
        {
            conn.Open();
             result = cmd.ExecuteScalar().ToString();
            conn.Close();
        }
        return result;
    }
    }
}
