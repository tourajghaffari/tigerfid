using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Collections;

namespace AWI.SmartTracker.ReportClass
{
    ///<summary>
    /// Data Access Class
    ///</summary>
    [DataObject]
    public class TrafficQuery
    {
        ///<summary>
        /// Connection String
        ///</summary>
        private static readonly string ConnString = string.Format("DRIVER={{MySQL ODBC 3.51 Driver}};SERVER={0};DATABASE={1};USER={2};PASSWORD={3};OPTION=3;", MainForm.serverMySQL, MainForm.database, MainForm.user, MainForm.password);

        private static readonly string SelectCmd = "SELECT * FROM Traffic";
        private static readonly string InsertCmd = "INSERT INTO Traffic (TagID, Type, FirstName, LastName, Department, ZoneID, Location, Status, Event, Time) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

        ///<summary>
        /// Select all persons with filter method
        ///</summary>
        ///<param name=”id”>Id of the person (default -1) </param>
        ///<param name=”name”>Name of the person or first letters of his name</param>
        ///<param name=”mobile”>mobile number of the person</param>
        ///<returns>list of Employee object</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static IEnumerable<Traffic> SelectAll()
        {
            return GetAllTraffic();
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
        public static Traffic[] GetArraySource()
        {
            return GetAllTraffic().ToArray();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static ArrayList GetArrayListSource()
        {
            ArrayList arrayList = new ArrayList();
            foreach (var traffic in GetAllTraffic())
            {
                arrayList.Add(traffic);
            }
            return arrayList;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Traffic> GetAllTraffic()
        {
            var listTraffic = new List<Traffic>();

            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(SelectCmd, con))
            {
                try
                {
                    con.Open();
                    using (var db = cmd.ExecuteReader())
                    {
                        while (db.Read())
                        {
                            var traffic = new Traffic(Convert.ToInt32(db["TagID"]),
                                                       (TagType)Convert.ToByte(db["Type"]),
                                                    db["FirstName"].ToString(),
                                                    db["LastName"].ToString(),
                                                    db["Department"].ToString(),
                                                    db["Status"].ToString(),
                                                    db["Event"].ToString(),
                                                    Convert.ToInt32(db["ZoneID"]),
                                                    db["Location"].ToString(),
                                                    (DateTime)db["Time"]);
                            listTraffic.Add(traffic);
                        }
                    }
                }
                catch
                {
                }
            }

            return listTraffic;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void Insert(Traffic traffic)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(InsertCmd, con))
            {
                cmd.Parameters.AddWithValue("TagID", traffic.TagID);
                cmd.Parameters.AddWithValue("Type", (byte)traffic.TagType);
                cmd.Parameters.AddWithValue("FirstName", traffic.FirstName);
                cmd.Parameters.AddWithValue("LastName", traffic.LastName);
                cmd.Parameters.AddWithValue("Department", traffic.Department);
                cmd.Parameters.AddWithValue("ZoneID", traffic.Zone);
                cmd.Parameters.AddWithValue("Location", traffic.Location);
                cmd.Parameters.AddWithValue("Status", traffic.Status);
                cmd.Parameters.AddWithValue("Event", traffic.Event);
                cmd.Parameters.AddWithValue("Time", traffic.Time);

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
    }

    public class Traffic
    {
        private int tag_id;
        private TagType tag_type;
        private string firstname;
        private string lastname;
        private string department;
        private int zone;
        private string location;
        private string status;
        private string event_description;
        private DateTime time;

        #region Constructors
        public Traffic()
        {
            tag_id = 0;
            tag_type = (byte)0;
            firstname = string.Empty;
            lastname = string.Empty;
            department = string.Empty;
            zone = 0;
            location = string.Empty;
            status = string.Empty;
            event_description = string.Empty;
            time = DateTime.MinValue;
        }

        public Traffic(int tag_id, TagType tag_type, string firstname, string lastname, string department, string status, string event_description, int zone, string location, DateTime time)
        {
            this.tag_id = tag_id;
            this.tag_type = tag_type;
            this.firstname = firstname;
            this.lastname = lastname;
            this.department = department;
            this.status = status;
            this.event_description = event_description;
            this.zone = zone;
            this.location = location;
            this.time = time;
        }
        #endregion

        #region Properties
        [DataObjectField(true)]
        public int TagID { get { return tag_id; } }
        public TagType TagType { get { return tag_type; } }
        public string FirstName { get { return firstname; } }
        public string LastName { get { return lastname; } }
        public string Department { get { return department; } }
        public string Name { get { return string.Format("{0} {1}", firstname, lastname).Trim(); } }
        public int Zone { get { return zone; } }
        public string Location { get { return location; } }
        public string Status { get { return status; } }
        public string Event { get { return event_description; } }
        public DateTime Time { get { return time; } }
        #endregion
    }
}
