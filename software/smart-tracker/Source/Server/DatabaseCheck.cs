using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;

namespace AWI.SmartTracker
{
    [DataObject]
    public class DatabaseCheck
    {
        private static readonly string ConnString = string.Format("DRIVER={{MySQL ODBC 3.51 Driver}};SERVER={0};DATABASE={1};USER={2};PASSWORD={3};OPTION=3;", MainForm.serverMySQL, MainForm.database, MainForm.user, MainForm.password);

        private static readonly string SelectCmd = "SHOW COLUMNS FROM traffic where Field='FirstName'";

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static bool IsOldDatabaseFormat()
        {
            bool old = true;

            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(SelectCmd, con))
            {
                try
                {
                    con.Open();
                    using (var db = cmd.ExecuteReader())
                    {
                        old = !db.HasRows;
                    }
                }
                catch
                {
                }
            }

            return old;
        }
    }
}
