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
    public class GroupZones
    {
        private static readonly string ConnString = string.Format("DRIVER={{MySQL ODBC 3.51 Driver}};SERVER={0};DATABASE={1};USER={2};PASSWORD={3};OPTION=3;", MainForm.serverMySQL, MainForm.database, MainForm.user, MainForm.password);

        private static readonly string SelectCmd = "SELECT GroupID, ZoneID, Location FROM GroupZones LEFT JOIN Zones ON ZoneID=ID WHERE GroupID=?";
        private static readonly string InsertCmd = "INSERT INTO GroupZones (GroupID, ZoneID) VALUES (?, ?)";
        private static readonly string DeleteCmd = "DELETE FROM GroupZones WHERE GroupID=?";
        private static readonly string HasZoneCmd = "SELECT COUNT(*) FROM GroupZones WHERE GroupID=? AND ZoneID=?";


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<GroupZone> GetZones(int group)
        {
            var listGroupZone = new List<GroupZone>();

            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(SelectCmd, con))
            {
                cmd.Parameters.AddWithValue("GroupID", group);

                try
                {
                    con.Open();
                    using (var db = cmd.ExecuteReader())
                    {
                        while (db.Read())
                        {
                            var zone = new GroupZone(Convert.ToInt32(db["GroupID"]),
                                                     Convert.ToInt32(db["ZoneID"]),
                                                     db["Location"].ToString());
                            listGroupZone.Add(zone);
                        }
                    }
                }
                catch
                {
                }
            }

            return listGroupZone;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void InsertZone(GroupZone zone)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(InsertCmd, con))
            {
                cmd.Parameters.AddWithValue("GroupID", zone.GroupID);
                cmd.Parameters.AddWithValue("ZoneID", zone.ZoneID);

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
        public static bool HasZone(int group, int zone)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(HasZoneCmd, con))
            {
                cmd.Parameters.AddWithValue("GroupID", group);
                cmd.Parameters.AddWithValue("ZoneID", zone);

                try
                {
                    con.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                        return true;
                }
                catch
                {
                }
            }

            return false;
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void DeleteZones(int group)
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

    public class GroupZone
    {
        private int group;
        private int zone;
        private string location;

        #region Constructors
        public GroupZone(int group, int zone, string location = null)
        {
            this.group = group;
            this.zone = zone;
            this.location = location;
        }

        public GroupZone(long group, long zone, string location = null)
        {
            this.group = (int)group;
            this.zone = (int)zone;
            this.location = location;
        }

        #endregion

        #region Properties
        [DataObjectField(true)]
        public int GroupID { get { return group; } set { group = value; } }
        public int ZoneID { get { return zone; } }
        public string Location { get { return location; } }
        #endregion
    }
}
