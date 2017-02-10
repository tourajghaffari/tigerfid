using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Odbc;
using System.Data;

namespace AWI.SmartTracker.ReportClass
{
    [DataObject]
    public class GroupSchedules
    {
        private static readonly string ConnString = string.Format("DRIVER={{MySQL ODBC 3.51 Driver}};SERVER={0};DATABASE={1};USER={2};PASSWORD={3};OPTION=3;", MainForm.serverMySQL, MainForm.database, MainForm.user, MainForm.password);

        private static readonly string SelectCmd = "SELECT * FROM GroupSchedules WHERE GroupID=?";
        private static readonly string InsertCmd = "INSERT INTO GroupSchedules (GroupID, Name, Access, DateFrom, DateTo, TimeFrom, TimeTo, Mondays, Tuesdays, Wednesdays, Thursdays, Fridays, Saturdays, Sundays) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
        private static readonly string UpdateCmd = "UPDATE GroupSchedules SET Name=?, Access=?, DateFrom=?, DateTo=?, TimeFrom=?, TimeTo=?, Mondays=?, Tuesdays=?, Wednesdays=?, Thursdays=?, Fridays=?, Saturdays=?, Sundays=? WHERE GroupID=?";
        private static readonly string DeleteCmd = "DELETE FROM GroupSchedules WHERE GroupID=?";


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<GroupSchedule> GetSchedules(int group)
        {
            return GetSchedules(group, null, null);
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<GroupSchedule> GetGrantSchedules(int group, DayOfWeek day_week)
        {
            return GetSchedules(group, true, day_week);
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<GroupSchedule> GetDenySchedules(int group, DayOfWeek day_week)
        {
            return GetSchedules(group, false, day_week);
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        private static List<GroupSchedule> GetSchedules(int group, bool? access, DayOfWeek? day_week)
        {
            var listSchedule = new List<GroupSchedule>();

            StringBuilder cmdQuery = new StringBuilder();

            cmdQuery.Append(SelectCmd);
            if (access.HasValue)
                cmdQuery.Append(" AND Access=?");

            if (day_week.HasValue) {
                switch (day_week.Value)
                {
                    case DayOfWeek.Monday:
                        cmdQuery.Append(" AND Mondays=1");
                        break;
                    case DayOfWeek.Tuesday:
                        cmdQuery.Append(" AND Tuesdays=1");
                        break;
                    case DayOfWeek.Wednesday:
                        cmdQuery.Append(" AND Wednesdays=1");
                        break;
                    case DayOfWeek.Thursday:
                        cmdQuery.Append(" AND Thursdays=1");
                        break;
                    case DayOfWeek.Friday:
                        cmdQuery.Append(" AND Fridays=1");
                        break;
                    case DayOfWeek.Saturday:
                        cmdQuery.Append(" AND Saturdays=1");
                        break;
                    case DayOfWeek.Sunday:
                        cmdQuery.Append(" AND Sundays=1");
                        break;
                }
            }


            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(cmdQuery.ToString(), con))
            {
                cmd.Parameters.AddWithValue("GroupID", group);
                if (access.HasValue)
                    cmd.Parameters.AddWithValue("Access", access.Value);

                try
                {
                    con.Open();
                    using (var db = cmd.ExecuteReader())
                    {
                        while (db.Read())
                        {
                            DateTime? date_from;
                            if (Convert.IsDBNull(db["DateFrom"]))
                                date_from = null;
                            else
                                date_from = (DateTime)db["DateFrom"];

                            DateTime? date_to;
                            if (Convert.IsDBNull(db["DateTo"]))
                                date_to = null;
                            else
                                date_to = (DateTime)db["DateTo"];

                            TimeSpan? time_from;
                            if (Convert.IsDBNull(db["TimeFrom"]))
                                time_from = null;
                            else if (db["TimeFrom"] is TimeSpan)
                                time_from = (TimeSpan)db["TimeFrom"];
                            else
                                time_from = null;

                            TimeSpan? time_to;
                            if (Convert.IsDBNull(db["TimeTo"]))
                                time_to = null;
                            else if (db["TimeTo"] is TimeSpan)
                                time_to = (TimeSpan)db["TimeTo"];
                            else
                                time_to = null;

                            var schedule = new GroupSchedule(Convert.ToInt32(db["GroupID"]),
                                                     db["Name"].ToString(),
                                                     Convert.ToBoolean(db["Access"]),
                                                     date_from,
                                                     date_to,
                                                     time_from,
                                                     time_to,
                                                     Convert.ToBoolean(db["Mondays"]),
                                                     Convert.ToBoolean(db["Tuesdays"]),
                                                     Convert.ToBoolean(db["Wednesdays"]),
                                                     Convert.ToBoolean(db["Thursdays"]),
                                                     Convert.ToBoolean(db["Fridays"]),
                                                     Convert.ToBoolean(db["Saturdays"]),
                                                     Convert.ToBoolean(db["Sundays"]));

                            listSchedule.Add(schedule);
                        }
                    }
                }
                catch
                {
                }
            }

            return listSchedule;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void InsertSchedule(GroupSchedule schedule)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(InsertCmd, con))
            {
                cmd.Parameters.AddWithValue("GroupID", schedule.GroupID);
                cmd.Parameters.AddWithValue("Name", schedule.Name);
                cmd.Parameters.AddWithValue("Access", schedule.Access);

                if (schedule.DateFrom.HasValue)
                {
                    cmd.Parameters.AddWithValue("DateFrom", schedule.DateFrom.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("DateFrom", DBNull.Value);
                }

                if (schedule.DateTo.HasValue)
                {
                    cmd.Parameters.AddWithValue("DateTo", schedule.DateTo.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("DateTo", DBNull.Value);
                }

                if (schedule.TimeFrom.HasValue)
                {
                    cmd.Parameters.AddWithValue("TimeFrom", schedule.TimeFrom.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("TimeFrom", DBNull.Value);
                }

                if (schedule.TimeTo.HasValue)
                {
                    cmd.Parameters.AddWithValue("TimeTo", schedule.TimeTo.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("TimeTo", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("Mondays", schedule.Mondays);
                cmd.Parameters.AddWithValue("Tuesdays", schedule.Tuesdays);
                cmd.Parameters.AddWithValue("Wednesdays", schedule.Wednesdays);
                cmd.Parameters.AddWithValue("Thursdays", schedule.Thursdays);
                cmd.Parameters.AddWithValue("Fridays", schedule.Fridays);
                cmd.Parameters.AddWithValue("Saturdays", schedule.Saturdays);
                cmd.Parameters.AddWithValue("Sundays", schedule.Sundays);

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


        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdateSchedule(GroupSchedule schedule)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(UpdateCmd, con))
            {
                cmd.Parameters.AddWithValue("Name", schedule.Name);
                cmd.Parameters.AddWithValue("Access", schedule.Access);

                if (schedule.DateFrom.HasValue)
                {
                    cmd.Parameters.AddWithValue("DateFrom", schedule.DateFrom.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("DateFrom", null);
                }

                if (schedule.DateTo.HasValue)
                {
                    cmd.Parameters.AddWithValue("DateTo", schedule.DateTo.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("DateTo", null);
                }

                if (schedule.TimeFrom.HasValue)
                {
                    cmd.Parameters.AddWithValue("TimeFrom", schedule.TimeFrom.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("TimeFrom", null);
                }

                if (schedule.TimeTo.HasValue)
                {
                    cmd.Parameters.AddWithValue("TimeTo", schedule.TimeTo.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("TimeTo", null);
                }

                cmd.Parameters.AddWithValue("Mondays", schedule.Mondays);
                cmd.Parameters.AddWithValue("Tuesdays", schedule.Tuesdays);
                cmd.Parameters.AddWithValue("Wednesdays", schedule.Wednesdays);
                cmd.Parameters.AddWithValue("Thursdays", schedule.Thursdays);
                cmd.Parameters.AddWithValue("Fridays", schedule.Fridays);
                cmd.Parameters.AddWithValue("Saturdays", schedule.Saturdays);
                cmd.Parameters.AddWithValue("Sundays", schedule.Sundays);

                cmd.Parameters.AddWithValue("GroupID", schedule.GroupID);

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
        public static void DeleteSchedules(int group)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(DeleteCmd, con))
            {
                cmd.Parameters.AddWithValue("GroupID", group);

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

    public class GroupSchedule
    {
        private int group;
        private string name;
        private bool access;
        private Nullable<DateTime> date_from;
        private Nullable<DateTime> date_to;
        private Nullable<TimeSpan> time_from;
        private Nullable<TimeSpan> time_to;
        private bool mondays;
        private bool tuesdays;
        private bool wednesdays;
        private bool thurdays;
        private bool fridays;
        private bool saturdays;
        private bool sundays;

        #region Constructors
        protected GroupSchedule()
        {
            group = -1;
            name = string.Empty;
            access = false;
            date_from = DateTime.MinValue;
            date_to = DateTime.MinValue;
            time_from = TimeSpan.MinValue;
            time_to = TimeSpan.MinValue;
            mondays = false;
            tuesdays = false;
            wednesdays = false;
            thurdays = false;
            fridays = false;
            saturdays = false;
            sundays = false;
        }

        public GroupSchedule(int group, string name, bool access, Nullable<DateTime> date_from = null, Nullable<DateTime> date_to = null, Nullable<TimeSpan> time_from = null, Nullable<TimeSpan> time_to = null, bool mondays = false, bool tuesdays = false, bool wednesdays = false, bool thurdays = false, bool fridays = false, bool saturdays = false, bool sundays = false)
        {
            this.group = group;
            this.name = name;
            this.access = access;
            this.date_from = date_from;
            this.date_to = date_to;
            this.time_from = time_from;
            this.time_to = time_to;
            this.mondays = mondays;
            this.tuesdays = tuesdays;
            this.wednesdays = wednesdays;
            this.thurdays = thurdays;
            this.fridays = fridays;
            this.saturdays = saturdays;
            this.sundays = sundays;
        }
        #endregion

        #region Properties
        [DataObjectField(true)]
        public int GroupID { get { return group; } set { group = value; } }
        public string Name { get { return name; } set { name = value; } }
        public bool Access { get { return access; } set { access = value; } }
        public Nullable<DateTime> DateFrom { get { return date_from; } set { date_from = value; } }
        public Nullable<DateTime> DateTo { get { return date_to; } set { date_to = value; } }
        public Nullable<TimeSpan> TimeFrom { get { return time_from; } set { time_from = value; } }
        public Nullable<TimeSpan> TimeTo { get { return time_to; } set { time_to = value; } }
        public bool Mondays { get { return mondays; } set { mondays = value; } }
        public bool Tuesdays { get { return tuesdays; } set { tuesdays = value; } }
        public bool Wednesdays { get { return wednesdays; } set { wednesdays = value; } }
        public bool Thursdays { get { return thurdays; } set { thurdays = value; } }
        public bool Fridays { get { return fridays; } set { fridays = value; } }
        public bool Saturdays { get { return saturdays; } set { saturdays = value; } }
        public bool Sundays { get { return sundays; } set { sundays = value; } }
        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(access ? "Grant access " : "Deny access ");
            if (date_from.HasValue)
                sb.AppendFormat("from {0} ", date_from.Value.ToString("d"));
            if (date_to.HasValue)
                sb.AppendFormat("to {0} ", date_to.Value.ToString("d"));
            if (time_from.HasValue && time_to.HasValue)
                sb.AppendFormat("between {0} and {1} ", time_from.Value.ToString("hh\\:mm"), time_to.Value.ToString("hh\\:mm"));
            if (mondays && tuesdays && wednesdays && thurdays && fridays && saturdays && sundays)
                sb.Append("everyday");
            else if (mondays && tuesdays && wednesdays && thurdays && fridays && !saturdays && !sundays)
                sb.Append("on weekdays");
            else if (!mondays && !tuesdays && !wednesdays && !thurdays && !fridays && saturdays && sundays)
                sb.Append("on weekends");
            else if (mondays || tuesdays || wednesdays || thurdays || fridays || saturdays || sundays)
            {
                sb.Append("on");

                if (mondays)
                    sb.Append(" Mon");
                if (tuesdays)
                    sb.Append(" Tue");
                if (wednesdays)
                    sb.Append(" Wed");
                if (thurdays)
                    sb.Append(" Thu");
                if (fridays)
                    sb.Append(" Fri");
                if (saturdays)
                    sb.Append(" Sat");
                if (sundays)
                    sb.Append(" Sun");
            }

            return sb.ToString();
        }
    }
}
