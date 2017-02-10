namespace AWI.SmartTracker
{
    partial class GroupScheduleForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.rbGrant = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rbDeny = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.chkDateTo = new System.Windows.Forms.CheckBox();
            this.chkDateFrom = new System.Windows.Forms.CheckBox();
            this.chkTimeInterval = new System.Windows.Forms.CheckBox();
            this.timeFrom = new System.Windows.Forms.DateTimePicker();
            this.timeTo = new System.Windows.Forms.DateTimePicker();
            this.lblTimeInterval = new System.Windows.Forms.Label();
            this.chkMon = new System.Windows.Forms.CheckBox();
            this.chkTue = new System.Windows.Forms.CheckBox();
            this.chkWed = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkThu = new System.Windows.Forms.CheckBox();
            this.chkFri = new System.Windows.Forms.CheckBox();
            this.chkSat = new System.Windows.Forms.CheckBox();
            this.chkSun = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(65, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(145, 21);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // rbGrant
            // 
            this.rbGrant.AutoSize = true;
            this.rbGrant.Checked = true;
            this.rbGrant.Location = new System.Drawing.Point(65, 41);
            this.rbGrant.Name = "rbGrant";
            this.rbGrant.Size = new System.Drawing.Size(52, 17);
            this.rbGrant.TabIndex = 3;
            this.rbGrant.TabStop = true;
            this.rbGrant.Text = "Grant";
            this.rbGrant.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Access:";
            // 
            // rbDeny
            // 
            this.rbDeny.AutoSize = true;
            this.rbDeny.Location = new System.Drawing.Point(132, 41);
            this.rbDeny.Name = "rbDeny";
            this.rbDeny.Size = new System.Drawing.Size(50, 17);
            this.rbDeny.TabIndex = 4;
            this.rbDeny.Text = "Deny";
            this.rbDeny.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Date Interval:";
            // 
            // dateFrom
            // 
            this.dateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateFrom.Enabled = false;
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFrom.Location = new System.Drawing.Point(127, 84);
            this.dateFrom.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dateFrom.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(83, 21);
            this.dateFrom.TabIndex = 7;
            // 
            // dateTo
            // 
            this.dateTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTo.Enabled = false;
            this.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTo.Location = new System.Drawing.Point(127, 111);
            this.dateTo.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dateTo.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(83, 21);
            this.dateTo.TabIndex = 9;
            // 
            // chkDateTo
            // 
            this.chkDateTo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDateTo.Location = new System.Drawing.Point(65, 115);
            this.chkDateTo.Name = "chkDateTo";
            this.chkDateTo.Size = new System.Drawing.Size(54, 17);
            this.chkDateTo.TabIndex = 8;
            this.chkDateTo.Text = "To:";
            this.chkDateTo.UseVisualStyleBackColor = true;
            this.chkDateTo.CheckedChanged += new System.EventHandler(this.chkDateTo_CheckedChanged);
            // 
            // chkDateFrom
            // 
            this.chkDateFrom.AutoSize = true;
            this.chkDateFrom.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDateFrom.Location = new System.Drawing.Point(65, 88);
            this.chkDateFrom.Name = "chkDateFrom";
            this.chkDateFrom.Size = new System.Drawing.Size(54, 17);
            this.chkDateFrom.TabIndex = 6;
            this.chkDateFrom.Text = "From:";
            this.chkDateFrom.UseVisualStyleBackColor = true;
            this.chkDateFrom.CheckedChanged += new System.EventHandler(this.chkDateFrom_CheckedChanged);
            // 
            // chkTimeInterval
            // 
            this.chkTimeInterval.AutoSize = true;
            this.chkTimeInterval.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkTimeInterval.Location = new System.Drawing.Point(13, 137);
            this.chkTimeInterval.Name = "chkTimeInterval";
            this.chkTimeInterval.Size = new System.Drawing.Size(93, 17);
            this.chkTimeInterval.TabIndex = 10;
            this.chkTimeInterval.Text = "Time Interval:";
            this.chkTimeInterval.UseVisualStyleBackColor = true;
            this.chkTimeInterval.CheckedChanged += new System.EventHandler(this.chkTimeInterval_CheckedChanged);
            // 
            // timeFrom
            // 
            this.timeFrom.CustomFormat = "HH:mm";
            this.timeFrom.Enabled = false;
            this.timeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeFrom.Location = new System.Drawing.Point(65, 159);
            this.timeFrom.Name = "timeFrom";
            this.timeFrom.ShowUpDown = true;
            this.timeFrom.Size = new System.Drawing.Size(55, 21);
            this.timeFrom.TabIndex = 11;
            // 
            // timeTo
            // 
            this.timeTo.CustomFormat = "HH:mm";
            this.timeTo.Enabled = false;
            this.timeTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeTo.Location = new System.Drawing.Point(155, 159);
            this.timeTo.Name = "timeTo";
            this.timeTo.ShowUpDown = true;
            this.timeTo.Size = new System.Drawing.Size(55, 21);
            this.timeTo.TabIndex = 13;
            // 
            // lblTimeInterval
            // 
            this.lblTimeInterval.AutoSize = true;
            this.lblTimeInterval.Enabled = false;
            this.lblTimeInterval.Location = new System.Drawing.Point(130, 164);
            this.lblTimeInterval.Name = "lblTimeInterval";
            this.lblTimeInterval.Size = new System.Drawing.Size(17, 13);
            this.lblTimeInterval.TabIndex = 12;
            this.lblTimeInterval.Text = "to";
            // 
            // chkMon
            // 
            this.chkMon.AutoSize = true;
            this.chkMon.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkMon.Location = new System.Drawing.Point(65, 206);
            this.chkMon.Name = "chkMon";
            this.chkMon.Size = new System.Drawing.Size(46, 17);
            this.chkMon.TabIndex = 15;
            this.chkMon.Text = "Mon";
            this.chkMon.UseVisualStyleBackColor = true;
            // 
            // chkTue
            // 
            this.chkTue.AutoSize = true;
            this.chkTue.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkTue.Location = new System.Drawing.Point(117, 206);
            this.chkTue.Name = "chkTue";
            this.chkTue.Size = new System.Drawing.Size(44, 17);
            this.chkTue.TabIndex = 16;
            this.chkTue.Text = "Tue";
            this.chkTue.UseVisualStyleBackColor = true;
            // 
            // chkWed
            // 
            this.chkWed.AutoSize = true;
            this.chkWed.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkWed.Location = new System.Drawing.Point(168, 206);
            this.chkWed.Name = "chkWed";
            this.chkWed.Size = new System.Drawing.Size(48, 17);
            this.chkWed.TabIndex = 17;
            this.chkWed.Text = "Wed";
            this.chkWed.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Weekdays:";
            // 
            // chkThu
            // 
            this.chkThu.AutoSize = true;
            this.chkThu.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkThu.Location = new System.Drawing.Point(92, 230);
            this.chkThu.Name = "chkThu";
            this.chkThu.Size = new System.Drawing.Size(44, 17);
            this.chkThu.TabIndex = 18;
            this.chkThu.Text = "Thu";
            this.chkThu.UseVisualStyleBackColor = true;
            // 
            // chkFri
            // 
            this.chkFri.AutoSize = true;
            this.chkFri.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkFri.Location = new System.Drawing.Point(144, 230);
            this.chkFri.Name = "chkFri";
            this.chkFri.Size = new System.Drawing.Size(38, 17);
            this.chkFri.TabIndex = 19;
            this.chkFri.Text = "Fri";
            this.chkFri.UseVisualStyleBackColor = true;
            // 
            // chkSat
            // 
            this.chkSat.AutoSize = true;
            this.chkSat.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkSat.Location = new System.Drawing.Point(92, 254);
            this.chkSat.Name = "chkSat";
            this.chkSat.Size = new System.Drawing.Size(42, 17);
            this.chkSat.TabIndex = 20;
            this.chkSat.Text = "Sat";
            this.chkSat.UseVisualStyleBackColor = true;
            // 
            // chkSun
            // 
            this.chkSun.AutoSize = true;
            this.chkSun.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkSun.Location = new System.Drawing.Point(144, 254);
            this.chkSun.Name = "chkSun";
            this.chkSun.Size = new System.Drawing.Size(44, 17);
            this.chkSun.TabIndex = 21;
            this.chkSun.Text = "Sun";
            this.chkSun.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(120, 286);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(28, 286);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // GroupScheduleForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(222, 321);
            this.ControlBox = false;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkSun);
            this.Controls.Add(this.chkSat);
            this.Controls.Add(this.chkFri);
            this.Controls.Add(this.chkThu);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkWed);
            this.Controls.Add(this.chkTue);
            this.Controls.Add(this.chkMon);
            this.Controls.Add(this.lblTimeInterval);
            this.Controls.Add(this.timeTo);
            this.Controls.Add(this.timeFrom);
            this.Controls.Add(this.chkTimeInterval);
            this.Controls.Add(this.chkDateFrom);
            this.Controls.Add(this.dateTo);
            this.Controls.Add(this.chkDateTo);
            this.Controls.Add(this.dateFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rbDeny);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbGrant);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GroupScheduleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Schedule";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.RadioButton rbGrant;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbDeny;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.CheckBox chkDateTo;
        private System.Windows.Forms.CheckBox chkDateFrom;
        private System.Windows.Forms.CheckBox chkTimeInterval;
        private System.Windows.Forms.DateTimePicker timeFrom;
        private System.Windows.Forms.DateTimePicker timeTo;
        private System.Windows.Forms.Label lblTimeInterval;
        private System.Windows.Forms.CheckBox chkMon;
        private System.Windows.Forms.CheckBox chkTue;
        private System.Windows.Forms.CheckBox chkWed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkThu;
        private System.Windows.Forms.CheckBox chkFri;
        private System.Windows.Forms.CheckBox chkSat;
        private System.Windows.Forms.CheckBox chkSun;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}