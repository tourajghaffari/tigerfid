using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AWI.SmartTracker
{
    public partial class ReportSelect : Form
    {
        public ReportSelect()
        {
            InitializeComponent();
        }

        public string ReportName
        {
            get
            {
                return grpReportName.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag as string;
            }
        }
    }
}
