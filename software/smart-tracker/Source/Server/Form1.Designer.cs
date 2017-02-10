namespace AWI.SmartTracker
{
    partial class CommForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.RS232TabPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ResetReaderButton = new System.Windows.Forms.Button();
            this.CloseConnectButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ComComboBox = new System.Windows.Forms.ComboBox();
            this.BaudComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NetworkTabPage = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SelectedIPRadioButton = new System.Windows.Forms.RadioButton();
            this.AllIPRadioButton = new System.Windows.Forms.RadioButton();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.SpecificIPRadioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.DisconnectNetButton = new System.Windows.Forms.Button();
            this.ConnectNetButton = new System.Windows.Forms.Button();
            this.ScanButton = new System.Windows.Forms.Button();
            this.IPListView = new System.Windows.Forms.ListView();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.ResetRdrButton = new System.Windows.Forms.Button();
            this.MsgLabel = new System.Windows.Forms.Label();
            this.MsgDBLabel = new System.Windows.Forms.Label();
            this.DBLabel = new System.Windows.Forms.Label();
            this.CommLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Timers.Timer();
            this.timer2 = new System.Timers.Timer();
            this.timer3 = new System.Timers.Timer();
            this.tabControl.SuspendLayout();
            this.RS232TabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.NetworkTabPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.RS232TabPage);
            this.tabControl.Controls.Add(this.NetworkTabPage);
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(10, 10);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(588, 466);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // RS232TabPage
            // 
            this.RS232TabPage.Controls.Add(this.groupBox1);
            this.RS232TabPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RS232TabPage.Location = new System.Drawing.Point(4, 25);
            this.RS232TabPage.Name = "RS232TabPage";
            this.RS232TabPage.Size = new System.Drawing.Size(580, 437);
            this.RS232TabPage.TabIndex = 0;
            this.RS232TabPage.Text = "Serial Port";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ResetReaderButton);
            this.groupBox1.Controls.Add(this.CloseConnectButton);
            this.groupBox1.Controls.Add(this.ConnectButton);
            this.groupBox1.Controls.Add(this.ComComboBox);
            this.groupBox1.Controls.Add(this.BaudComboBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(110, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 308);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Port Setting";
            // 
            // ResetReaderButton
            // 
            this.ResetReaderButton.Enabled = false;
            this.ResetReaderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetReaderButton.ForeColor = System.Drawing.Color.Blue;
            this.ResetReaderButton.Location = new System.Drawing.Point(106, 218);
            this.ResetReaderButton.Name = "ResetReaderButton";
            this.ResetReaderButton.Size = new System.Drawing.Size(150, 28);
            this.ResetReaderButton.TabIndex = 14;
            this.ResetReaderButton.Text = "Reset Reader";
            this.ResetReaderButton.Click += new System.EventHandler(this.ResetReaderButton_Click);
            // 
            // CloseConnectButton
            // 
            this.CloseConnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseConnectButton.ForeColor = System.Drawing.Color.Blue;
            this.CloseConnectButton.Location = new System.Drawing.Point(106, 184);
            this.CloseConnectButton.Name = "CloseConnectButton";
            this.CloseConnectButton.Size = new System.Drawing.Size(150, 28);
            this.CloseConnectButton.TabIndex = 13;
            this.CloseConnectButton.Text = "Disconnect";
            this.CloseConnectButton.Click += new System.EventHandler(this.CloseConnectButton_Click);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectButton.ForeColor = System.Drawing.Color.Blue;
            this.ConnectButton.Location = new System.Drawing.Point(106, 150);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(150, 28);
            this.ConnectButton.TabIndex = 12;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ComComboBox
            // 
            this.ComComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.ComComboBox.Location = new System.Drawing.Point(176, 96);
            this.ComComboBox.Name = "ComComboBox";
            this.ComComboBox.Size = new System.Drawing.Size(62, 24);
            this.ComComboBox.TabIndex = 11;
            this.ComComboBox.Text = "1";
            // 
            // BaudComboBox
            // 
            this.BaudComboBox.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.BaudComboBox.Location = new System.Drawing.Point(176, 52);
            this.BaudComboBox.Name = "BaudComboBox";
            this.BaudComboBox.Size = new System.Drawing.Size(98, 24);
            this.BaudComboBox.TabIndex = 6;
            this.BaudComboBox.Text = "115200";
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(40, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Comm Port Number :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(62, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bits Per Sceond :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NetworkTabPage
            // 
            this.NetworkTabPage.Controls.Add(this.groupBox2);
            this.NetworkTabPage.Controls.Add(this.label2);
            this.NetworkTabPage.Controls.Add(this.DisconnectNetButton);
            this.NetworkTabPage.Controls.Add(this.ConnectNetButton);
            this.NetworkTabPage.Controls.Add(this.ScanButton);
            this.NetworkTabPage.Controls.Add(this.IPListView);
            this.NetworkTabPage.Controls.Add(this.ResetRdrButton);
            this.NetworkTabPage.ForeColor = System.Drawing.Color.Blue;
            this.NetworkTabPage.Location = new System.Drawing.Point(4, 25);
            this.NetworkTabPage.Name = "NetworkTabPage";
            this.NetworkTabPage.Size = new System.Drawing.Size(580, 437);
            this.NetworkTabPage.TabIndex = 1;
            this.NetworkTabPage.Text = "Network";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SelectedIPRadioButton);
            this.groupBox2.Controls.Add(this.AllIPRadioButton);
            this.groupBox2.Controls.Add(this.IPTextBox);
            this.groupBox2.Controls.Add(this.SpecificIPRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(6, 328);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(562, 56);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Command Type";
            // 
            // SelectedIPRadioButton
            // 
            this.SelectedIPRadioButton.Location = new System.Drawing.Point(142, 26);
            this.SelectedIPRadioButton.Name = "SelectedIPRadioButton";
            this.SelectedIPRadioButton.Size = new System.Drawing.Size(108, 24);
            this.SelectedIPRadioButton.TabIndex = 2;
            this.SelectedIPRadioButton.Text = "Selected IP";
            this.SelectedIPRadioButton.Visible = false;
            this.SelectedIPRadioButton.Click += new System.EventHandler(this.SelectedIPRadioButton_Click);
            // 
            // AllIPRadioButton
            // 
            this.AllIPRadioButton.Checked = true;
            this.AllIPRadioButton.Location = new System.Drawing.Point(18, 26);
            this.AllIPRadioButton.Name = "AllIPRadioButton";
            this.AllIPRadioButton.Size = new System.Drawing.Size(62, 24);
            this.AllIPRadioButton.TabIndex = 3;
            this.AllIPRadioButton.TabStop = true;
            this.AllIPRadioButton.Text = "All IPs";
            this.AllIPRadioButton.Click += new System.EventHandler(this.AllIPRadioButton_Click);
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(406, 26);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.ReadOnly = true;
            this.IPTextBox.Size = new System.Drawing.Size(140, 22);
            this.IPTextBox.TabIndex = 11;
            this.IPTextBox.TextChanged += new System.EventHandler(this.IPTextBox_TextChanged);
            // 
            // SpecificIPRadioButton
            // 
            this.SpecificIPRadioButton.Location = new System.Drawing.Point(314, 26);
            this.SpecificIPRadioButton.Name = "SpecificIPRadioButton";
            this.SpecificIPRadioButton.Size = new System.Drawing.Size(90, 24);
            this.SpecificIPRadioButton.TabIndex = 12;
            this.SpecificIPRadioButton.Text = "Specific IP: ";
            this.SpecificIPRadioButton.Click += new System.EventHandler(this.SpecificIPRadioButton_Click);
            this.SpecificIPRadioButton.CheckedChanged += new System.EventHandler(this.SpecificIPRadioButton_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(10, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(556, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "List of Active IP Addresses";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DisconnectNetButton
            // 
            this.DisconnectNetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisconnectNetButton.ForeColor = System.Drawing.Color.Red;
            this.DisconnectNetButton.Location = new System.Drawing.Point(372, 396);
            this.DisconnectNetButton.Name = "DisconnectNetButton";
            this.DisconnectNetButton.Size = new System.Drawing.Size(136, 28);
            this.DisconnectNetButton.TabIndex = 4;
            this.DisconnectNetButton.Text = "Disconnect";
            this.DisconnectNetButton.Click += new System.EventHandler(this.DisconnectNetButton_Click);
            // 
            // ConnectNetButton
            // 
            this.ConnectNetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectNetButton.ForeColor = System.Drawing.Color.Blue;
            this.ConnectNetButton.Location = new System.Drawing.Point(0, 296);
            this.ConnectNetButton.Name = "ConnectNetButton";
            this.ConnectNetButton.Size = new System.Drawing.Size(12, 28);
            this.ConnectNetButton.TabIndex = 2;
            this.ConnectNetButton.Text = "Connect";
            this.ConnectNetButton.Visible = false;
            this.ConnectNetButton.Click += new System.EventHandler(this.ConnectNetButton_Click);
            // 
            // ScanButton
            // 
            this.ScanButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanButton.ForeColor = System.Drawing.Color.Blue;
            this.ScanButton.Location = new System.Drawing.Point(64, 396);
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(136, 28);
            this.ScanButton.TabIndex = 0;
            this.ScanButton.Text = "Connect";
            this.ScanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // IPListView
            // 
            this.IPListView.BackColor = System.Drawing.SystemColors.Info;
            this.IPListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.IPListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPListView.FullRowSelect = true;
            this.IPListView.GridLines = true;
            this.IPListView.HideSelection = false;
            this.IPListView.Location = new System.Drawing.Point(8, 30);
            this.IPListView.MultiSelect = false;
            this.IPListView.Name = "IPListView";
            this.IPListView.Size = new System.Drawing.Size(560, 290);
            this.IPListView.TabIndex = 0;
            this.IPListView.UseCompatibleStateImageBehavior = false;
            this.IPListView.View = System.Windows.Forms.View.Details;
            this.IPListView.DoubleClick += new System.EventHandler(this.IPListView_DoubleClick);
            this.IPListView.SelectedIndexChanged += new System.EventHandler(this.IPListView_SelectedIndexChanged);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Index";
            this.columnHeader7.Width = 40;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IP Address";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Reader ID";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 65;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Host ID";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 55;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Network Status";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 85;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Rdr Status";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Last Update";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 150;
            // 
            // ResetRdrButton
            // 
            this.ResetRdrButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetRdrButton.ForeColor = System.Drawing.Color.Blue;
            this.ResetRdrButton.Location = new System.Drawing.Point(218, 396);
            this.ResetRdrButton.Name = "ResetRdrButton";
            this.ResetRdrButton.Size = new System.Drawing.Size(136, 28);
            this.ResetRdrButton.TabIndex = 14;
            this.ResetRdrButton.Text = "Reset Reader";
            this.ResetRdrButton.Click += new System.EventHandler(this.ResetRdrButton_Click);
            // 
            // MsgLabel
            // 
            this.MsgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MsgLabel.ForeColor = System.Drawing.Color.Blue;
            this.MsgLabel.Location = new System.Drawing.Point(108, 484);
            this.MsgLabel.Name = "MsgLabel";
            this.MsgLabel.Size = new System.Drawing.Size(172, 23);
            this.MsgLabel.TabIndex = 1;
            this.MsgLabel.Text = "None";
            // 
            // MsgDBLabel
            // 
            this.MsgDBLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MsgDBLabel.ForeColor = System.Drawing.Color.Red;
            this.MsgDBLabel.Location = new System.Drawing.Point(514, 484);
            this.MsgDBLabel.Name = "MsgDBLabel";
            this.MsgDBLabel.Size = new System.Drawing.Size(82, 23);
            this.MsgDBLabel.TabIndex = 2;
            this.MsgDBLabel.Text = "Disconnected";
            // 
            // DBLabel
            // 
            this.DBLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DBLabel.ForeColor = System.Drawing.Color.Blue;
            this.DBLabel.Location = new System.Drawing.Point(450, 484);
            this.DBLabel.Name = "DBLabel";
            this.DBLabel.Size = new System.Drawing.Size(64, 23);
            this.DBLabel.TabIndex = 3;
            this.DBLabel.Text = "Database : ";
            // 
            // CommLabel
            // 
            this.CommLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommLabel.ForeColor = System.Drawing.Color.Blue;
            this.CommLabel.Location = new System.Drawing.Point(12, 484);
            this.CommLabel.Name = "CommLabel";
            this.CommLabel.Size = new System.Drawing.Size(94, 23);
            this.CommLabel.TabIndex = 4;
            this.CommLabel.Text = "Communication: ";
            // 
            // timer1
            // 
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.SynchronizingObject = this;
            this.timer2.Elapsed += new System.Timers.ElapsedEventHandler(this.timer2_Elapsed);
            // 
            // timer3
            // 
            this.timer3.Interval = 1000;
            this.timer3.SynchronizingObject = this;
            this.timer3.Elapsed += new System.Timers.ElapsedEventHandler(this.timer3_Elapsed);
            // 
            // CommForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(610, 511);
            this.Controls.Add(this.CommLabel);
            this.Controls.Add(this.DBLabel);
            this.Controls.Add(this.MsgLabel);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.MsgDBLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CommForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Communication";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CommForm_Closing);
            this.tabControl.ResumeLayout(false);
            this.RS232TabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.NetworkTabPage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage RS232TabPage;
        private System.Windows.Forms.TabPage NetworkTabPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox BaudComboBox;
        private System.Windows.Forms.ComboBox ComComboBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.ListView IPListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button ScanButton;
        private System.Windows.Forms.Button ConnectNetButton;
        private System.Windows.Forms.Button DisconnectNetButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label MsgLabel;
        private System.Windows.Forms.Button CloseConnectButton;
        private System.Windows.Forms.Button ResetReaderButton;
        private System.Windows.Forms.Label MsgDBLabel;
        private System.Windows.Forms.Label DBLabel;
        private System.Windows.Forms.Label CommLabel;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton AllIPRadioButton;
        private System.Windows.Forms.RadioButton SelectedIPRadioButton;
        private System.Timers.Timer timer1;
        private System.Timers.Timer timer2;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.RadioButton SpecificIPRadioButton;
        private System.Windows.Forms.Button ResetRdrButton;
        private System.Timers.Timer timer3;
    }
}