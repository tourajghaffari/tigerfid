namespace AWI.SmartTracker
{
    partial class ReportViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.EmployeeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rbAlarms = new System.Windows.Forms.RadioButton();
            this.rbEmployees = new System.Windows.Forms.RadioButton();
            this.rbEmployeeHistory = new System.Windows.Forms.RadioButton();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtTagID = new System.Windows.Forms.TextBox();
            this.chkTagID = new System.Windows.Forms.CheckBox();
            this.chkFrom = new System.Windows.Forms.CheckBox();
            this.chkTo = new System.Windows.Forms.CheckBox();
            this.timerAdjustMaxDate = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // EmployeeBindingSource
            // 
            this.EmployeeBindingSource.DataSource = typeof(AWI.SmartTracker.Employee);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.EmployeeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DisplayName = "Employees Report";
            this.reportViewer1.Location = new System.Drawing.Point(0, 90);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.reportViewer1.Size = new System.Drawing.Size(612, 275);
            this.reportViewer1.TabIndex = 10;
            // 
            // rbAlarms
            // 
            this.rbAlarms.AutoSize = true;
            this.rbAlarms.Checked = true;
            this.rbAlarms.Location = new System.Drawing.Point(13, 13);
            this.rbAlarms.Name = "rbAlarms";
            this.rbAlarms.Size = new System.Drawing.Size(57, 17);
            this.rbAlarms.TabIndex = 0;
            this.rbAlarms.TabStop = true;
            this.rbAlarms.Text = "Alarms";
            this.rbAlarms.UseVisualStyleBackColor = true;
            // 
            // rbEmployees
            // 
            this.rbEmployees.AutoSize = true;
            this.rbEmployees.Location = new System.Drawing.Point(13, 37);
            this.rbEmployees.Name = "rbEmployees";
            this.rbEmployees.Size = new System.Drawing.Size(76, 17);
            this.rbEmployees.TabIndex = 1;
            this.rbEmployees.Text = "Employees";
            this.rbEmployees.UseVisualStyleBackColor = true;
            this.rbEmployees.CheckedChanged += new System.EventHandler(this.rbEmployees_CheckedChanged);
            // 
            // rbEmployeeHistory
            // 
            this.rbEmployeeHistory.AutoSize = true;
            this.rbEmployeeHistory.Location = new System.Drawing.Point(13, 61);
            this.rbEmployeeHistory.Name = "rbEmployeeHistory";
            this.rbEmployeeHistory.Size = new System.Drawing.Size(108, 17);
            this.rbEmployeeHistory.TabIndex = 2;
            this.rbEmployeeHistory.Text = "Employee History";
            this.rbEmployeeHistory.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(455, 57);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 9;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // dateTo
            // 
            this.dateTo.Enabled = false;
            this.dateTo.Location = new System.Drawing.Point(208, 59);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(200, 21);
            this.dateTo.TabIndex = 8;
            this.dateTo.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // dateFrom
            // 
            this.dateFrom.Enabled = false;
            this.dateFrom.Location = new System.Drawing.Point(208, 35);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(200, 21);
            this.dateFrom.TabIndex = 6;
            this.dateFrom.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // txtTagID
            // 
            this.txtTagID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtTagID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTagID.Enabled = false;
            this.txtTagID.Location = new System.Drawing.Point(208, 11);
            this.txtTagID.MaxLength = 5;
            this.txtTagID.Name = "txtTagID";
            this.txtTagID.Size = new System.Drawing.Size(59, 21);
            this.txtTagID.TabIndex = 4;
            this.txtTagID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTagID_KeyPress);
            this.txtTagID.Validating += new System.ComponentModel.CancelEventHandler(this.txtTagID_Validating);
            // 
            // chkTagID
            // 
            this.chkTagID.AutoSize = true;
            this.chkTagID.Location = new System.Drawing.Point(140, 14);
            this.chkTagID.Name = "chkTagID";
            this.chkTagID.Size = new System.Drawing.Size(62, 17);
            this.chkTagID.TabIndex = 3;
            this.chkTagID.Text = "Tag ID:";
            this.chkTagID.UseVisualStyleBackColor = true;
            this.chkTagID.CheckedChanged += new System.EventHandler(this.chkTagID_CheckedChanged);
            // 
            // chkFrom
            // 
            this.chkFrom.AutoSize = true;
            this.chkFrom.Location = new System.Drawing.Point(140, 38);
            this.chkFrom.Name = "chkFrom";
            this.chkFrom.Size = new System.Drawing.Size(54, 17);
            this.chkFrom.TabIndex = 5;
            this.chkFrom.Text = "From:";
            this.chkFrom.UseVisualStyleBackColor = true;
            this.chkFrom.CheckedChanged += new System.EventHandler(this.chkFrom_CheckedChanged);
            // 
            // chkTo
            // 
            this.chkTo.AutoSize = true;
            this.chkTo.Location = new System.Drawing.Point(140, 62);
            this.chkTo.Name = "chkTo";
            this.chkTo.Size = new System.Drawing.Size(42, 17);
            this.chkTo.TabIndex = 7;
            this.chkTo.Text = "To:";
            this.chkTo.UseVisualStyleBackColor = true;
            this.chkTo.CheckedChanged += new System.EventHandler(this.chkTo_CheckedChanged);
            // 
            // timerAdjustMaxDate
            // 
            this.timerAdjustMaxDate.Enabled = true;
            this.timerAdjustMaxDate.Interval = 1000;
            this.timerAdjustMaxDate.Tick += new System.EventHandler(this.timerAdjustMaxDate_Tick);
            // 
            // ReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 366);
            this.Controls.Add(this.chkTo);
            this.Controls.Add(this.chkFrom);
            this.Controls.Add(this.chkTagID);
            this.Controls.Add(this.txtTagID);
            this.Controls.Add(this.dateFrom);
            this.Controls.Add(this.dateTo);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.rbEmployeeHistory);
            this.Controls.Add(this.rbEmployees);
            this.Controls.Add(this.rbAlarms);
            this.Controls.Add(this.reportViewer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(620, 400);
            this.Name = "ReportViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report";
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource EmployeeBindingSource;
        private System.Windows.Forms.RadioButton rbAlarms;
        private System.Windows.Forms.RadioButton rbEmployees;
        private System.Windows.Forms.RadioButton rbEmployeeHistory;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.TextBox txtTagID;
        private System.Windows.Forms.CheckBox chkTagID;
        private System.Windows.Forms.CheckBox chkFrom;
        private System.Windows.Forms.CheckBox chkTo;
        private System.Windows.Forms.Timer timerAdjustMaxDate;
        private System.Windows.Forms.ToolTip toolTip;

    }
}