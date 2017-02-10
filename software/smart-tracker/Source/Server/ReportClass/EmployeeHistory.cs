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
    public class EmployeeHistoryQuery
    {
        private static readonly string ConnString = string.Format("DRIVER={{MySQL ODBC 3.51 Driver}};SERVER={0};DATABASE={1};USER={2};PASSWORD={3};OPTION=3;", MainForm.serverMySQL, MainForm.database, MainForm.user, MainForm.password);

        private static readonly string SelectCmd = "SELECT TagID, FirstName, LastName, Department, Time, Location, Event FROM traffic";

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static IEnumerable<EmployeeHistory> Select(int id)
        {
            return GetEmployeeHistory(id);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable GetDataTableSource(int id)
        {
            DataTable dataTable = new DataTable();
            OdbcDataAdapter dataAdapter;
            using (dataAdapter = new OdbcDataAdapter(string.Format(SelectCmd, id), ConnString))
            {
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static IDataAdapter GetDataAdapterSource(int id)
        {
            return new OdbcDataAdapter(string.Format(SelectCmd, id), ConnString);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static EmployeeHistory[] GetArraySource(int id)
        {
            return GetEmployeeHistory(id).ToArray();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static ArrayList GetArrayListSource(int id)
        {
            ArrayList arrayList = new ArrayList();
            foreach (var employee_history in GetEmployeeHistory(id))
            {
                arrayList.Add(employee_history);
            }
            return arrayList;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<EmployeeHistory> GetEmployeeHistory(Nullable<int> id = null, Nullable<DateTime> from = null, Nullable<DateTime> to = null)
        {
            var listEmployeeHistory = new List<EmployeeHistory>();

            StringBuilder cmd_fmt = new StringBuilder(SelectCmd);

            bool first = true;

            if (id != null)
            {
                cmd_fmt.Append(" WHERE TagID = ?");
                first = false;
            }
            if (from != null)
            {
                cmd_fmt.Append(first ? " WHERE" : " AND");
                cmd_fmt.Append(" Time >= ?");
                first = false;
            }
            if (to != null)
            {
                cmd_fmt.Append(first ? " WHERE" : " AND");
                cmd_fmt.Append(" Time <= ?");
                first = false;
            }

            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(cmd_fmt.ToString(), con))
            {
                if (id != null)
                    cmd.Parameters.AddWithValue("TagID", id.Value);
                if (from != null)
                    cmd.Parameters.AddWithValue("Time", from.Value);
                if (to != null)
                    cmd.Parameters.AddWithValue("Time", to.Value);

                try
                {
                    con.Open();
                    using (var db = cmd.ExecuteReader())
                    {
                        while (db.Read())
                        {
                            try
                            {
                                var person = new EmployeeHistory(Convert.ToInt32(db["TagID"]),
                                                        db["FirstName"].ToString(),
                                                        db["LastName"].ToString(),
                                                        db["Department"].ToString(),
                                                        (DateTime)db["Time"],
                                                        db["Location"].ToString(),
                                                        db["Event"].ToString());

                                listEmployeeHistory.Add(person);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
                catch
                {
                }
            }

            return listEmployeeHistory;
        }

    }


    public class EmployeeHistory
    {
        private int id;
        private string firstname;
        private string lastname;
        private string department;
        private DateTime time;
        private string location;
        private string eventname;

        #region Constructors
        public EmployeeHistory()
        {
            id = 0;
            firstname = string.Empty;
            lastname = string.Empty;
            department = string.Empty;
            time = DateTime.MinValue;
            location = string.Empty;
            eventname = string.Empty;
        }

        public EmployeeHistory(int id, string firstname, string lastname, string department, DateTime time, string location, string eventname)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.department = department;
            this.time = time;
            this.location = location;
            this.eventname = eventname;
        }
        #endregion

        #region Properties
        [DataObjectField(true)]
        public int ID { get { return id; } }
        public string FirstName { get { return firstname; } }
        public string LastName { get { return lastname; } }
        public string Department { get { return department; } }
        public string Name { get { return string.Format("{0} {1}", firstname, lastname).Trim(); } }
        public string Event { get { return eventname; } }
        public DateTime Time { get { return time; } }
        public string Location { get { return location; } }
        #endregion
    }
}
