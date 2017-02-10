using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Globalization;

namespace AWI.SmartTracker
{
    public partial class ReportViewer : Form
    {
        public ReportViewer()
        {
            CultureInfo ci = new CultureInfo("en", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

            InitializeComponent();

            isUpdating = true;
            dateTo.Value = DateLastMinute(DateTime.Now);
            dateTo.MaxDate = DateLastMinute(dateTo.Value);
            dateFrom.Value = DateFirstMinute(dateTo.Value);
            isUpdating = false;

            EmployeesQuery.GetAllEmployees().ForEach(employee =>
            {
                txtTagID.AutoCompleteCustomSource.Add(employee.ID.ToString());
            });
        }

        private void chkTagID_CheckedChanged(object sender, EventArgs e)
        {
            txtTagID.Enabled = chkTagID.Checked;
        }

        private void chkFrom_CheckedChanged(object sender, EventArgs e)
        {
            dateFrom.Enabled = chkFrom.Checked;
            UpdateDateRange();
        }

        private void chkTo_CheckedChanged(object sender, EventArgs e)
        {
            dateTo.Enabled = chkTo.Checked;
            UpdateDateRange();
        }

        private void timerAdjustMaxDate_Tick(object sender, EventArgs e)
        {
            dateTo.MaxDate = DateLastMinute(DateTime.Now);
        }

        private bool oldChkTagID = false;
        private bool oldChkFrom = false;
        private bool oldChkTo = false;
        private void rbEmployees_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEmployees.Checked)
            {
                oldChkTagID = chkTagID.Checked;
                chkTagID.Checked = false;
                chkTagID.Enabled = false;
                oldChkFrom = chkFrom.Checked;
                chkFrom.Checked = false;
                chkFrom.Enabled = false;
                oldChkTo = chkTo.Checked;
                chkTo.Checked = false;
                chkTo.Enabled = false;
            }
            else
            {
                chkTagID.Checked = oldChkTagID;
                chkTagID.Enabled = true;
                chkFrom.Checked = oldChkFrom;
                chkFrom.Enabled = true;
                chkTo.Checked = oldChkTo;
                chkTo.Enabled = true;
            }
        }

        private DateTime DateLastMinute(DateTime date)
        {
            return date.Subtract(date.TimeOfDay).AddDays(1).AddSeconds(-1);
        }

        private DateTime DateFirstMinute(DateTime date)
        {
            return date.Subtract(date.TimeOfDay);
        }


        private bool isUpdating = false;
        private void UpdateDateRange()
        {
            if (!isUpdating)
            {
                isUpdating = true;

                if (dateTo.Enabled)
                {
                    dateFrom.MaxDate = dateTo.Value;
                }
                else
                {
                    dateFrom.MaxDate = DateLastMinute(DateTime.Now);
                }

                if (dateFrom.Enabled)
                {
                    dateTo.MinDate = dateFrom.Value;
                }
                else
                {
                    dateTo.MinDate = dateFrom.MinDate;
                }

                isUpdating = false;
            }
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            UpdateDateRange();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (rbAlarms.Checked)
            {
                Nullable<ushort> tagID = null;
                Nullable<DateTime> from = null;
                Nullable<DateTime> to = null;

                ushort id;
                if (chkTagID.Checked && ushort.TryParse(txtTagID.Text, out id))
                    tagID = id;
                if (chkFrom.Checked)
                    from = dateFrom.Value;
                if (chkTo.Checked)
                    to = dateTo.Value;

                System.Drawing.Printing.PageSettings page = new System.Drawing.Printing.PageSettings();
                page.Landscape = true;
                page.Margins = new System.Drawing.Printing.Margins(50, 50, 0, 0);
                reportViewer1.SetPageSettings(page);
                reportViewer1.LocalReport.ReportEmbeddedResource = "AWI.SmartTracker.ReportClass.Alarms.rdlc";
                EmployeeBindingSource.DataSource = EmployeeHistoryQuery.GetEmployeeHistory(tagID, from, to);
            }
            else if (rbEmployees.Checked)
            {
                System.Drawing.Printing.PageSettings page = new System.Drawing.Printing.PageSettings();
                page.Landscape = false;
                page.Margins = new System.Drawing.Printing.Margins(50, 50, 0, 0);
                reportViewer1.SetPageSettings(page);
                reportViewer1.LocalReport.ReportEmbeddedResource = "AWI.SmartTracker.ReportClass.Employees.rdlc";
                EmployeeBindingSource.DataSource = EmployeesQuery.GetAllEmployees();
            }
            else if (rbEmployeeHistory.Checked)
            {
                Nullable<ushort> tagID = null;
                Nullable<DateTime> from = null;
                Nullable<DateTime> to = null;

                ushort id;
                if (chkTagID.Checked && ushort.TryParse(txtTagID.Text, out id))
                    tagID = id;
                if (chkFrom.Checked)
                    from = dateFrom.Value;
                if (chkTo.Checked)
                    to = dateTo.Value;

                System.Drawing.Printing.PageSettings page = new System.Drawing.Printing.PageSettings();
                page.Landscape = false;
                page.Margins = new System.Drawing.Printing.Margins(50, 50, 0, 0);
                reportViewer1.SetPageSettings(page);
                reportViewer1.LocalReport.ReportEmbeddedResource = "AWI.SmartTracker.ReportClass.EmployeeHistory.rdlc";
                EmployeeBindingSource.DataSource = EmployeeHistoryQuery.GetEmployeeHistory(tagID, from, to);
            }

            try
            {
                reportViewer1.RefreshReport();
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("Failed to load report: {0}", exception), "Report Viewer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTagID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTagID_Validating(object sender, CancelEventArgs e)
        {
            if (txtTagID.TextLength > 0)
            {
                ushort id;
                if (!ushort.TryParse(txtTagID.Text, out id) || (id == 0))
                {
                    e.Cancel = true;
                    toolTip.Show("Invalid Tag ID. Tag ID must be between 1 and 65535.", txtTagID, txtTagID.Width, txtTagID.Height, 3000);
                }
                else
                {
                    toolTip.Hide(txtTagID);
                }
            }
        }
    }
}
