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
using AWI.SmartTracker.ReportClass;

namespace AWI.SmartTracker
{
    ///<summary>
    /// Data Access Class
    ///</summary>
    [DataObject]
    public class EmployeesQuery
    {
        ///<summary>
        /// Connection String
        ///</summary>
        private static readonly string ConnString = string.Format("DRIVER={{MySQL ODBC 3.51 Driver}};SERVER={0};DATABASE={1};USER={2};PASSWORD={3};OPTION=3;", MainForm.serverMySQL, MainForm.database, MainForm.user, MainForm.password);

        private static readonly string SelectAllCmd = "SELECT * FROM Employees ORDER BY ID";
        private static readonly string SelectCmd = "SELECT * FROM Employees WHERE ID = ?";
        private static readonly string SelectAccessCmd = "SELECT * FROM EmployeeAccess LEFT JOIN Groups ON EmployeeAccess.GroupID=Groups.GroupID WHERE ID=?";
        private static readonly string DeleteAccessCmd = "DELETE FROM EmployeeAccess WHERE ID = ?";
        private static readonly string InsertAccessCmd = "INSERT INTO EmployeeAccess (ID, GroupID) VALUES (?, ?)";

        ///<summary>
        /// Select all persons with filter method
        ///</summary>
        ///<param name=”id”>Id of the person (default -1) </param>
        ///<param name=”name”>Name of the person or first letters of his name</param>
        ///<param name=”mobile”>mobile number of the person</param>
        ///<returns>list of Employee object</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static IEnumerable<Employee> SelectAll()
        {
            return GetAllEmployees();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable GetDataTableSource()
        {
            DataTable dataTable = new DataTable();
            OdbcDataAdapter dataAdapter;
            using (dataAdapter = new OdbcDataAdapter(SelectCmd, ConnString))
            {
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static IDataAdapter GetDataAdapterSource()
        {
            return new OdbcDataAdapter(SelectCmd, ConnString);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Employee[] GetArraySource()
        {
            return GetAllEmployees().ToArray();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static ArrayList GetArrayListSource()
        {
            ArrayList arrayList = new ArrayList();
            foreach (var employee in GetAllEmployees())
            {
                arrayList.Add(employee);
            }
            return arrayList;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Employee> GetAllEmployees()
        {
            var listEmployee = new List<Employee>();

            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(SelectAllCmd, con))
            {
                try
                {
                    con.Open();
                    using (var db = cmd.ExecuteReader())
                    {
                        while (db.Read())
                        {
                            var person = new Employee(Convert.ToInt32(db["ID"]),
                                                    db["FirstName"].ToString(),
                                                    db["LastName"].ToString(),
                                                    db["Department"].ToString());
                            listEmployee.Add(person);
                        }
                    }
                }
                catch
                {
                }
            }

            return listEmployee;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Group> GetEmployeeAccessGroup(uint id)
        {
            var listGroups = new List<Group>();

            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(SelectAccessCmd, con))
            {
                cmd.Parameters.AddWithValue("ID", (int)id);
                 
                try
                {
                    con.Open();
                    using (var db = cmd.ExecuteReader())
                    {
                        while (db.Read())
                        {
                            var group = new Group(Convert.ToInt32(db["GroupID"]),
                                                    db["Name"].ToString(),
                                                    db["Description"].ToString());
                            listGroups.Add(group);
                        }
                    }
                }
                catch
                {
                }
            }

            return listGroups;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void InsertEmployeeAccessGroup(uint id, Group group)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(InsertAccessCmd, con))
            {
                cmd.Parameters.AddWithValue("ID", (int)id);
                cmd.Parameters.AddWithValue("GroupID", group.ID);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                }
            }
        }


        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void DeleteEmployeeAccessGroups(uint id)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(DeleteAccessCmd, con))
            {
                cmd.Parameters.AddWithValue("ID", (int)id);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Employee GetEmployee(uint id)
        {
            return GetEmployee((int)id);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Employee GetEmployee(int id)
        {
            StringBuilder cmd_fmt = new StringBuilder(SelectCmd);

            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(cmd_fmt.ToString(), con))
            {
                cmd.Parameters.AddWithValue("ID", id);

                try
                {
                    con.Open();
                    using (var db = cmd.ExecuteReader())
                    {
                        while (db.Read())
                        {
                            var person = new Employee(Convert.ToInt32(db["ID"]),
                                                    db["FirstName"].ToString(),
                                                    db["LastName"].ToString(),
                                                    db["Department"].ToString());

                            return person;
                        }
                    }
                }
                catch
                {
                }
            }

            return null;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static bool CheckAccess(uint id, int zone, DateTime date)
        {
            var groups = GetEmployeeAccessGroup(id);
            foreach (var group in groups)
            {
                var valid = GroupZones.HasZone(group.ID, zone);
                if (!valid)
                    return false;

                var denySchedules = GroupSchedules.GetDenySchedules(group.ID, date.DayOfWeek);
                foreach (var schedule in denySchedules)
                {
                    bool match_date = false;
                    if (schedule.DateFrom.HasValue && schedule.DateTo.HasValue)
                    {
                        if ((date.Date >= schedule.DateFrom.Value) &&
                            (date.Date >= schedule.DateTo.Value))
                        {
                            match_date = true;
                        }
                    }
                    else if (schedule.DateFrom.HasValue)
                    {
                        if (date.Date >= schedule.DateFrom.Value)
                        {
                            match_date = true;
                        }
                    }
                    else if (schedule.DateTo.HasValue)
                    {
                        if (date.Date <= schedule.DateTo.Value)
                        {
                            match_date = true;
                        }
                    }
                    else
                    {
                        match_date = true;
                    }

                    bool match_time = false;
                    if (schedule.TimeFrom.HasValue && schedule.TimeTo.HasValue)
                    {
                        if ((date.TimeOfDay >= schedule.TimeFrom.Value) &&
                            (date.TimeOfDay <= schedule.TimeTo.Value))
                        {
                            match_time = true;
                        }
                    }
                    else if (schedule.TimeFrom.HasValue)
                    {
                        if (date.TimeOfDay >= schedule.TimeFrom.Value)
                        {
                            match_time = true;
                        }
                    }
                    else if (schedule.TimeTo.HasValue)
                    {
                        if (date.TimeOfDay <= schedule.TimeTo.Value)
                        {
                            match_time = true;
                        }
                    }
                    else
                    {
                        match_time = true;
                    }

                    if (match_date && match_time)
                        return false;
                }

                var grantSchedules = GroupSchedules.GetGrantSchedules(group.ID, date.DayOfWeek);
                foreach (var schedule in grantSchedules)
                {
                    bool match_date = false;
                    if (schedule.DateFrom.HasValue && schedule.DateTo.HasValue)
                    {
                        if ((date.Date >= schedule.DateFrom.Value) &&
                            (date.Date >= schedule.DateTo.Value))
                        {
                            match_date = true;
                        }
                    }
                    else if (schedule.DateFrom.HasValue)
                    {
                        if (date.Date >= schedule.DateFrom.Value)
                        {
                            match_date = true;
                        }
                    }
                    else if (schedule.DateTo.HasValue)
                    {
                        if (date.Date <= schedule.DateTo.Value)
                        {
                            match_date = true;
                        }
                    }
                    else
                    {
                        match_date = true;
                    }

                    bool match_time = false;
                    if (schedule.TimeFrom.HasValue && schedule.TimeTo.HasValue)
                    {
                        if ((date.TimeOfDay >= schedule.TimeFrom.Value) &&
                            (date.TimeOfDay <= schedule.TimeTo.Value))
                        {
                            match_time = true;
                        }
                    }
                    else if (schedule.TimeFrom.HasValue)
                    {
                        if (date.TimeOfDay >= schedule.TimeFrom.Value)
                        {
                            match_time = true;
                        }
                    }
                    else if (schedule.TimeTo.HasValue)
                    {
                        if (date.TimeOfDay <= schedule.TimeTo.Value)
                        {
                            match_time = true;
                        }
                    }
                    else
                    {
                        match_time = true;
                    }

                    if (match_date && match_time)
                        return true;
                }
            }

            return false;
        }
    }


    public class Employee
    {
        private int id;
        private string firstname;
        private string lastname;
        private string department;

        #region Constructors
        public Employee()
        {
            id = 0;
            firstname = string.Empty;
            lastname = string.Empty;
            department = string.Empty;
        }

        public Employee(int id, string firstname, string lastname, string department)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.department = department;
        }
        #endregion

        #region Properties
        [DataObjectField(true)]
        public int ID { get { return id; } }
        public string FirstName { get { return firstname; } }
        public string LastName { get { return lastname; } }
        public string Department { get { return department; } }
        public string Name { get { return string.Format("{0} {1}", firstname, lastname).Trim(); } }
        #endregion
    }
}
