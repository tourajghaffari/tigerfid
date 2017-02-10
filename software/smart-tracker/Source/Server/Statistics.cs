using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AWIComponentLib.Database;
using System.Data.Odbc;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace AWI.SmartTracker
{
    public partial class Statistics : Form
    {
        public Statistics()
        {
            InitializeComponent();

            MainForm.m_closeWindowEvent += new CloseWindowDelegate(this.Close);
        }

        private void Statistics_Shown(object sender, EventArgs e)
        {
            ShowStatistics();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ShowStatistics();
        }

        private void ShowStatistics()
        {
            lvStatistics.BeginUpdate();
            lvStatistics.Items.Clear();

            //string SQL = "SELECT TagID, FirstName, LastName, Location, Type, Contaminated, AvgTimeContaminated, Washed FROM (" +
            //             "SELECT sani.TagID, COUNT(*) as Contaminated, AVG(TIME_TO_SEC(TIMEDIFF(sani1.Time, sani.Time))) AS AvgTimeContaminated, sani.Location, sani.Type " +
            //             "FROM sani, sani as sani1 " +
            //             "WHERE sani.TagID = sani1.TagID AND sani.index = sani1.index - 1 AND sani.SaniStatus != sani1.SaniStatus AND sani.SaniStatus  = 'Wash' AND (sani1.SaniStatus = 'Exiting' OR sani1.SaniStatus = 'Fresh' OR sani1.SaniStatus = 'Normal') " +
            //             "GROUP BY TagID" +
            //             ") as t, (" +
            //             "SELECT COUNT(*) as Washed " +
            //             "FROM sani, sani as sani1 " +
            //             "WHERE sani.TagID = sani1.TagID AND sani.index = sani1.index - 1 AND sani.SaniStatus != sani1.SaniStatus AND (sani.SaniStatus = 'Exiting' OR sani.SaniStatus = 'Fresh' OR sani.SaniStatus = 'Normal') AND (sani1.SaniStatus  = 'Exiting' OR sani.SaniStatus  = 'Normal' OR sani.SaniStatus  = 'Fresh') " +
            //             "GROUP BY sani.TagID) as u " +
            //             "JOIN employees WHERE TagID = ID";

            string SQL = "SELECT * FROM (" +
                         "SELECT 'Total' as TagID, '' as FirstName, '' as LastName, '' as Location, '' as Type, Washed, Engaged, Contaminated, Violation FROM (" +
                         "SELECT COUNT(*) as Washed FROM sani WHERE SaniStatus = 'Alcohol Clean' OR SaniStatus = 'Soap Clean'" +
                         ") as w, (" +
                         "SELECT COUNT(*) as Engaged FROM sani WHERE SaniStatus = 'Engaging Patient'" +
                         ") as p, (" +
                         "SELECT COUNT(*) as Contaminated FROM sani WHERE SaniStatus = 'Contaminated Other' OR SaniStatus = 'Contaminated Patient' OR SaniStatus = 'Contaminated Bathroom'" +
                         ") as c, (" +
                         "SELECT COUNT(*) as Violation FROM sani WHERE SaniStatus = 'Violation'" +
                         ") as v " +
                         "UNION " +
                         "SELECT w.TagID, FirstName, LastName, Location, Type, Washed, Engaged, Contaminated, Violation FROM (" +
                         "SELECT TagID, Location, Type, COUNT(*) as Washed FROM sani WHERE SaniStatus = 'Alcohol Clean' OR SaniStatus = 'Soap Clean' GROUP BY TagID" +
                         ") as w, (" +
                         "SELECT TagID, COUNT(*) as Engaged FROM sani WHERE SaniStatus = 'Engaging Patient' GROUP BY TagID" +
                         ") as p, (" +
                         "SELECT TagID, COUNT(*) as Contaminated FROM sani WHERE SaniStatus = 'Contaminated Other' OR SaniStatus = 'Contaminated Patient' OR SaniStatus = 'Contaminated Bathroom' GROUP BY TagID" +
                         ") as c, (" +
                         "SELECT TagID, COUNT(*) as Violation FROM sani WHERE SaniStatus = 'Violation' GROUP BY TagID" +
                         ") as v " +
                         "JOIN employees WHERE w.TagID = ID) as ix " +
                         "ORDER BY TagID='Total' DESC, TagID ASC";
            try
            {
                lock (MainForm.m_connection)
                {
                    using (OdbcCommand cmd = new OdbcCommand(SQL, MainForm.m_connection))
                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem listItem = new ListViewItem(reader.GetString(0)); // TagID
                            listItem.SubItems.Add(reader.GetString(1)); // FirstName
                            listItem.SubItems.Add(reader.GetString(2)); // LastName
                            listItem.SubItems.Add(reader.GetString(3)); // Location

                            try
                            {
                                string type = reader.GetString(4); // Type
                                if (type.CompareTo("1") == 0)
                                    listItem.SubItems.Add("Employee");
                                else if (type.CompareTo("3") == 0)
                                    listItem.SubItems.Add("Visitor");
                                else
                                    listItem.SubItems.Add("");
                            }
                            catch
                            {
                                listItem.SubItems.Add("");
                            }

                            int washed = reader.GetInt32(5);
                            int engaged = reader.GetInt32(6);
                            int contaminated = reader.GetInt32(7);
                            int violation = reader.GetInt32(8);

                            listItem.SubItems.Add(washed.ToString());  //Washed
                            listItem.SubItems.Add(engaged.ToString());  //Engaged
                            listItem.SubItems.Add(contaminated.ToString());  //Contaminated
                            listItem.SubItems.Add(violation.ToString());  //Violation

                            lvStatistics.Items.Add(listItem);
                        }
                    }
                }
            }
            finally
            {
                lvStatistics.EndUpdate();
            }
        }

        bool updating = false;

        private void datePicker_ValueChanged(object sender, EventArgs e)
        {
            if (!updating)
            {
                updating = true;

                if (datePickerStart.Value.TimeOfDay.TotalSeconds != 0)
                    datePickerStart.Value = datePickerStart.Value.Date;

                if (datePickerEnd.Value.TimeOfDay.CompareTo(new TimeSpan(23, 59, 59)) != 0)
                    datePickerEnd.Value = datePickerEnd.Value.Date.Add(new TimeSpan(23, 59, 59));

                if (datePickerStart.Value > datePickerEnd.Value)
                    datePickerEnd.Value = datePickerStart.Value.Date.Add(new TimeSpan(23, 59, 59));

                ShowStatistics();

                updating = false;
            }
        }
    }
}
