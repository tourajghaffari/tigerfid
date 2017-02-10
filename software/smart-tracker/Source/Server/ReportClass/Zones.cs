using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Odbc;

namespace AWI.SmartTracker.ReportClass
{
    [DataObject]
    public class Zones
    {
        private static readonly string ConnString = string.Format("DRIVER={{MySQL ODBC 3.51 Driver}};SERVER={0};DATABASE={1};USER={2};PASSWORD={3};OPTION=3;", MainForm.serverMySQL, MainForm.database, MainForm.user, MainForm.password);

        private static readonly string SelectAllCmd = "SELECT * FROM Zones ORDER BY Location";


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Zone> GetAllZones()
        {
            var listZone = new List<Zone>();

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
                            var zone = new Zone(Convert.ToInt32(db["ID"]),
                                                     db["Location"].ToString(),
                                                Convert.ToInt32(db["ReaderID"]),
                                                Convert.ToInt32(db["FieldGenID"]),
                                                     db["Status"].ToString().Equals("Offline", StringComparison.InvariantCultureIgnoreCase) ? false : true,
                                                Convert.IsDBNull(db["Time"]) ? new DateTime() : (DateTime)db["Time"],
                                                Convert.ToBoolean(db["RSSI"]),
                                                Convert.IsDBNull(db["Threshold"]) ? (short)-1 : Convert.ToInt16(db["Threshold"]),
                                                Convert.ToInt32(db["ReaderType"]));
                            listZone.Add(zone);
                        }
                    }
                }
                catch
                {
                }
            }

            return listZone;
        }
    }

    public class Zone
    {
        private int id;
        private string location;
        private int reader_id;
        private int fgen_id;
        private bool online;
        private DateTime time;
        private bool rssi;
        private short threshold;
        private int type;

        #region Constructors
        public Zone()
        {
            id = -1;
            location = string.Empty;
            reader_id = 0;
            fgen_id = 0;
            online = false;
            time = DateTime.MinValue;
            rssi = false;
            threshold = -1;
            type = 0;
        }

        public Zone(int id, string location, int reader_id, int fgen_id, bool online, DateTime time, bool rssi, short threshold, int type)
        {
            this.id = id;
            this.location = location;
            this.reader_id = reader_id;
            this.fgen_id = fgen_id;
            this.online = online;
            this.time = time;
            this.rssi = rssi;
            this.threshold = threshold;
            this.type = type;
        }
        #endregion

        #region Properties
        [DataObjectField(true)]
        public int ID { get { return id; } }
        public string Location { get { return location; } }
        public int ReaderID { get { return reader_id; } }
        public int FGenID { get { return fgen_id; } }
        public bool Online { get { return online; } }
        public DateTime Time { get { return time; } }
        public bool RSSI { get { return rssi; } }
        public short Threshold { get { return threshold; } }
        public int Type { get { return type; } }
        #endregion
    }
}
