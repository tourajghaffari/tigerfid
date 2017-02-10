using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AWI.SmartTracker.ReportClass;

namespace AWI.SmartTracker
{
    public partial class GroupScheduleForm : Form
    {
        private GroupSchedule groupSchedule;

        public GroupScheduleForm(GroupSchedule schedule)
        {
            InitializeComponent();

            groupSchedule = schedule;

            txtName.Text = groupSchedule.Name;
            
            if (groupSchedule.Access)
                rbGrant.Checked = true;
            else
                rbDeny.Checked = true;

            if (!groupSchedule.DateFrom.HasValue)
            {
                dateFrom.Value = DateTime.Now;
                chkDateFrom.Checked = false;
            }
            else
            {
                dateFrom.Value = groupSchedule.DateFrom.Value;
                chkDateFrom.Checked = true;
            }
            if (!groupSchedule.DateTo.HasValue)
            {
                dateTo.Value = DateTime.Now;
                chkDateTo.Checked = false;
            }
            else
            {
                dateTo.Value = groupSchedule.DateTo.Value;
                chkDateTo.Checked = true;
            }
            if (!groupSchedule.TimeFrom.HasValue)
            {
                timeFrom.Value = DateTime.Now.Date.Add(new TimeSpan(0, 0, 0));
                timeTo.Value = DateTime.Now.Date.Add(new TimeSpan(23, 59, 59));
                chkTimeInterval.Checked = false;
            }
            else
            {
                timeFrom.Value = DateTime.Now.Date.Add(groupSchedule.TimeFrom.Value);
                timeTo.Value = DateTime.Now.Date.Add(groupSchedule.TimeTo.Value);
                chkTimeInterval.Checked = true;
            }

            chkMon.Checked = groupSchedule.Mondays;
            chkTue.Checked = groupSchedule.Tuesdays;
            chkWed.Checked = groupSchedule.Wednesdays;
            chkThu.Checked = groupSchedule.Thursdays;
            chkFri.Checked = groupSchedule.Fridays;
            chkSat.Checked = groupSchedule.Saturdays;
            chkSun.Checked = groupSchedule.Sundays;
        }

        private void chkDateFrom_CheckedChanged(object sender, EventArgs e)
        {
            dateFrom.Enabled = chkDateFrom.Checked;
        }

        private void chkDateTo_CheckedChanged(object sender, EventArgs e)
        {
            dateTo.Enabled = chkDateTo.Checked;
        }

        private void chkTimeInterval_CheckedChanged(object sender, EventArgs e)
        {
            timeFrom.Enabled = chkTimeInterval.Checked;
            timeTo.Enabled = chkTimeInterval.Checked;
            lblTimeInterval.Enabled = chkTimeInterval.Checked;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.TextLength > 0)
                btnSave.Enabled = true;
            else
                btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime? date_from;
            if (chkDateFrom.Checked)
                date_from = dateFrom.Value.Date;
            else
                date_from = null;

            DateTime? date_to;
            if (chkDateTo.Checked)
                date_to = dateTo.Value.Date;
            else
                date_to = null;

            TimeSpan? time_from;
            TimeSpan? time_to;
            if (chkTimeInterval.Checked)
            {
                time_from = timeFrom.Value.Subtract(timeFrom.Value.Date);
                time_to = timeTo.Value.Subtract(timeTo.Value.Date);
            }
            else
            {
                time_from = null;
                time_to = null;
            }

            groupSchedule.Name = txtName.Text;
            groupSchedule.Access = rbGrant.Checked;
            groupSchedule.DateFrom = date_from;
            groupSchedule.DateTo = date_to;
            groupSchedule.TimeFrom = time_from;
            groupSchedule.TimeTo = time_to;
            groupSchedule.Mondays = chkMon.Checked;
            groupSchedule.Tuesdays = chkTue.Checked;
            groupSchedule.Wednesdays = chkWed.Checked;
            groupSchedule.Thursdays = chkThu.Checked;
            groupSchedule.Fridays = chkFri.Checked;
            groupSchedule.Saturdays = chkSat.Checked;
            groupSchedule.Sundays = chkSun.Checked;
        }
    }
}
