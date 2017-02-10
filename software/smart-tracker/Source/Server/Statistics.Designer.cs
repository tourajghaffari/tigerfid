namespace AWI.SmartTracker
{
    partial class Statistics
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            this.lvStatistics = new System.Windows.Forms.ListView();
            this.columnHeader30 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader31 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader40 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.datePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.datePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvStatistics
            // 
            this.lvStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStatistics.BackColor = System.Drawing.Color.NavajoWhite;
            this.lvStatistics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader30,
            this.columnHeader31,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader40,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader1});
            this.lvStatistics.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvStatistics.ForeColor = System.Drawing.Color.DarkBlue;
            this.lvStatistics.FullRowSelect = true;
            this.lvStatistics.GridLines = true;
            this.lvStatistics.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            this.lvStatistics.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvStatistics.Location = new System.Drawing.Point(3, 33);
            this.lvStatistics.MultiSelect = false;
            this.lvStatistics.Name = "lvStatistics";
            this.lvStatistics.ShowGroups = false;
            this.lvStatistics.Size = new System.Drawing.Size(988, 332);
            this.lvStatistics.TabIndex = 1;
            this.lvStatistics.UseCompatibleStateImageBehavior = false;
            this.lvStatistics.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader30
            // 
            this.columnHeader30.Text = "Tag ID";
            this.columnHeader30.Width = 56;
            // 
            // columnHeader31
            // 
            this.columnHeader31.Text = "First Name";
            this.columnHeader31.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Last Name";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Location";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader40
            // 
            this.columnHeader40.Text = "Type";
            this.columnHeader40.Width = 85;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "#Washes";
            this.columnHeader4.Width = 70;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "#Engagements";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "#Contaminations";
            this.columnHeader6.Width = 110;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#Violations";
            this.columnHeader1.Width = 78;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(3, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // datePickerEnd
            // 
            this.datePickerEnd.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerEnd.CustomFormat = "MM/dd/yy";
            this.datePickerEnd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerEnd.Location = new System.Drawing.Point(906, 5);
            this.datePickerEnd.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.datePickerEnd.MinDate = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            this.datePickerEnd.Name = "datePickerEnd";
            this.datePickerEnd.Size = new System.Drawing.Size(85, 21);
            this.datePickerEnd.TabIndex = 3;
            this.datePickerEnd.ValueChanged += new System.EventHandler(this.datePicker_ValueChanged);
            // 
            // datePickerStart
            // 
            this.datePickerStart.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerStart.CustomFormat = "MM/dd/yy";
            this.datePickerStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerStart.Location = new System.Drawing.Point(769, 5);
            this.datePickerStart.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.datePickerStart.MinDate = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            this.datePickerStart.Name = "datePickerStart";
            this.datePickerStart.Size = new System.Drawing.Size(85, 21);
            this.datePickerStart.TabIndex = 2;
            this.datePickerStart.ValueChanged += new System.EventHandler(this.datePicker_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(722, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Range:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(873, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "to";
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 368);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.datePickerStart);
            this.Controls.Add(this.datePickerEnd);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lvStatistics);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Statistics";
            this.Text = "Statistics for Sani Tags";
            this.Shown += new System.EventHandler(this.Statistics_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvStatistics;
        private System.Windows.Forms.ColumnHeader columnHeader30;
        private System.Windows.Forms.ColumnHeader columnHeader31;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader40;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.DateTimePicker datePickerEnd;
        private System.Windows.Forms.DateTimePicker datePickerStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}