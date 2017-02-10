namespace AWI.SmartTracker
{
    partial class ActionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
            //if (disposing && (components != null))
            //{
                //components.Dispose();
            //}
            //base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionForm));
            this.label1 = new System.Windows.Forms.Label();
            this.ReaderIDComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LocationTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.EventsComboBox = new System.Windows.Forms.ComboBox();
            this.EventActionListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ActionDateTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.NewToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.EditToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.CancelToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.SaveToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.DeleteToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.RefreshToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.FGenIDComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.EventActionIDTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DescTextBox = new System.Windows.Forms.TextBox();
            this.ActionsListView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label9 = new System.Windows.Forms.Label();
            this.process1 = new System.Diagnostics.Process();
            this.LabelTextBox = new System.Windows.Forms.TextBox();
            this.userListViewControl1 = new UserListControl.UserListViewControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(14, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reader ID: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ReaderIDComboBox
            // 
            this.ReaderIDComboBox.Enabled = false;
            this.ReaderIDComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReaderIDComboBox.FormattingEnabled = true;
            this.ReaderIDComboBox.Location = new System.Drawing.Point(106, 132);
            this.ReaderIDComboBox.Name = "ReaderIDComboBox";
            this.ReaderIDComboBox.Size = new System.Drawing.Size(82, 28);
            this.ReaderIDComboBox.TabIndex = 1;
            this.ReaderIDComboBox.SelectedValueChanged += new System.EventHandler(this.ReaderIDComboBox_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(466, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Event Location: ";
            // 
            // LocationTextBox
            // 
            this.LocationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LocationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationTextBox.Location = new System.Drawing.Point(590, 182);
            this.LocationTextBox.Name = "LocationTextBox";
            this.LocationTextBox.ReadOnly = true;
            this.LocationTextBox.Size = new System.Drawing.Size(250, 26);
            this.LocationTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(16, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Events:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EventsComboBox
            // 
            this.EventsComboBox.Enabled = false;
            this.EventsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EventsComboBox.FormattingEnabled = true;
            this.EventsComboBox.Items.AddRange(new object[] {
            "Input Detected",
            "Tag Detected",
            "Invalid Tag Detected",
            "Breach Alarm",
            "Tamper Alarm"});
            this.EventsComboBox.Location = new System.Drawing.Point(78, 182);
            this.EventsComboBox.Name = "EventsComboBox";
            this.EventsComboBox.Size = new System.Drawing.Size(360, 28);
            this.EventsComboBox.TabIndex = 7;
            this.EventsComboBox.SelectedIndexChanged += new System.EventHandler(this.EventsComboBox_SelectedIndexChanged);
            // 
            // EventActionListView
            // 
            this.EventActionListView.BackColor = System.Drawing.SystemColors.Info;
            this.EventActionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.EventActionListView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EventActionListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EventActionListView.FullRowSelect = true;
            this.EventActionListView.GridLines = true;
            this.EventActionListView.HideSelection = false;
            this.EventActionListView.Location = new System.Drawing.Point(10, 476);
            this.EventActionListView.MultiSelect = false;
            this.EventActionListView.Name = "EventActionListView";
            this.EventActionListView.Size = new System.Drawing.Size(308, 200);
            this.EventActionListView.TabIndex = 12;
            this.EventActionListView.UseCompatibleStateImageBehavior = false;
            this.EventActionListView.View = System.Windows.Forms.View.Details;
            this.EventActionListView.SelectedIndexChanged += new System.EventHandler(this.EventActionListView_SelectedIndexChanged);
            this.EventActionListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EventActionListView_MouseDown);
            this.EventActionListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EventActionListView_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "E.A ID";
            this.columnHeader1.Width = 45;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Rdr ID";
            this.columnHeader7.Width = 48;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "FGen ID";
            this.columnHeader8.Width = 57;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Event";
            this.columnHeader9.Width = 150;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Location";
            this.columnHeader10.Width = 161;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Description";
            this.columnHeader11.Width = 202;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Date";
            this.columnHeader12.Width = 117;
            // 
            // ActionDateTextBox
            // 
            this.ActionDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ActionDateTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActionDateTextBox.Location = new System.Drawing.Point(592, 130);
            this.ActionDateTextBox.MaxLength = 6;
            this.ActionDateTextBox.Name = "ActionDateTextBox";
            this.ActionDateTextBox.ReadOnly = true;
            this.ActionDateTextBox.Size = new System.Drawing.Size(248, 26);
            this.ActionDateTextBox.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(542, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 23);
            this.label6.TabIndex = 42;
            this.label6.Text = "Date: ";
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.AutoSize = false;
            this.toolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.NewToolBarButton,
            this.EditToolBarButton,
            this.CancelToolBarButton,
            this.SaveToolBarButton,
            this.DeleteToolBarButton,
            this.RefreshToolBarButton});
            this.toolBar1.ButtonSize = new System.Drawing.Size(65, 55);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(852, 62);
            this.toolBar1.TabIndex = 44;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // NewToolBarButton
            // 
            this.NewToolBarButton.ImageIndex = 0;
            this.NewToolBarButton.Name = "NewToolBarButton";
            this.NewToolBarButton.Text = "New";
            this.NewToolBarButton.ToolTipText = "New Record";
            // 
            // EditToolBarButton
            // 
            this.EditToolBarButton.Enabled = false;
            this.EditToolBarButton.ImageIndex = 1;
            this.EditToolBarButton.Name = "EditToolBarButton";
            this.EditToolBarButton.Text = "Edit";
            this.EditToolBarButton.ToolTipText = "Edit Record";
            // 
            // CancelToolBarButton
            // 
            this.CancelToolBarButton.ImageIndex = 2;
            this.CancelToolBarButton.Name = "CancelToolBarButton";
            this.CancelToolBarButton.Text = "Cancel";
            this.CancelToolBarButton.ToolTipText = "Cancel Command";
            // 
            // SaveToolBarButton
            // 
            this.SaveToolBarButton.Enabled = false;
            this.SaveToolBarButton.ImageIndex = 5;
            this.SaveToolBarButton.Name = "SaveToolBarButton";
            this.SaveToolBarButton.Text = "Save";
            this.SaveToolBarButton.ToolTipText = "Save Record";
            // 
            // DeleteToolBarButton
            // 
            this.DeleteToolBarButton.Enabled = false;
            this.DeleteToolBarButton.ImageIndex = 4;
            this.DeleteToolBarButton.Name = "DeleteToolBarButton";
            this.DeleteToolBarButton.Text = "Delete";
            this.DeleteToolBarButton.ToolTipText = "Delete Record";
            // 
            // RefreshToolBarButton
            // 
            this.RefreshToolBarButton.ImageIndex = 3;
            this.RefreshToolBarButton.Name = "RefreshToolBarButton";
            this.RefreshToolBarButton.Text = "Refresh";
            this.RefreshToolBarButton.ToolTipText = "Refresh List";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            // 
            // FGenIDComboBox
            // 
            this.FGenIDComboBox.Enabled = false;
            this.FGenIDComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FGenIDComboBox.FormattingEnabled = true;
            this.FGenIDComboBox.Items.AddRange(new object[] {
            "1",
            "2"});
            this.FGenIDComboBox.Location = new System.Drawing.Point(364, 130);
            this.FGenIDComboBox.Name = "FGenIDComboBox";
            this.FGenIDComboBox.Size = new System.Drawing.Size(72, 28);
            this.FGenIDComboBox.TabIndex = 46;
            this.FGenIDComboBox.SelectedIndexChanged += new System.EventHandler(this.FGenIDComboBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(286, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 20);
            this.label7.TabIndex = 45;
            this.label7.Text = "FGen ID: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(12, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 51;
            this.label3.Text = "Actions:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(12, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 20);
            this.label5.TabIndex = 55;
            this.label5.Text = "Event-Action ID:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EventActionIDTextBox
            // 
            this.EventActionIDTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EventActionIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EventActionIDTextBox.Location = new System.Drawing.Point(140, 84);
            this.EventActionIDTextBox.Name = "EventActionIDTextBox";
            this.EventActionIDTextBox.ReadOnly = true;
            this.EventActionIDTextBox.Size = new System.Drawing.Size(46, 26);
            this.EventActionIDTextBox.TabIndex = 56;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(270, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 20);
            this.label8.TabIndex = 58;
            this.label8.Text = "Description:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DescTextBox
            // 
            this.DescTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescTextBox.Location = new System.Drawing.Point(366, 82);
            this.DescTextBox.Name = "DescTextBox";
            this.DescTextBox.ReadOnly = true;
            this.DescTextBox.Size = new System.Drawing.Size(476, 26);
            this.DescTextBox.TabIndex = 59;
            // 
            // ActionsListView
            // 
            this.ActionsListView.BackColor = System.Drawing.SystemColors.Info;
            this.ActionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.ActionsListView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ActionsListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActionsListView.FullRowSelect = true;
            this.ActionsListView.GridLines = true;
            this.ActionsListView.HideSelection = false;
            this.ActionsListView.Location = new System.Drawing.Point(326, 478);
            this.ActionsListView.MultiSelect = false;
            this.ActionsListView.Name = "ActionsListView";
            this.ActionsListView.Size = new System.Drawing.Size(514, 200);
            this.ActionsListView.TabIndex = 60;
            this.ActionsListView.UseCompatibleStateImageBehavior = false;
            this.ActionsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Action";
            this.columnHeader2.Width = 230;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Duration";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Rdr ID";
            this.columnHeader4.Width = 47;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "FG ID";
            this.columnHeader5.Width = 45;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Location";
            this.columnHeader6.Width = 182;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(30, 430);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 32);
            this.label9.TabIndex = 62;
            this.label9.Text = "Note:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // LabelTextBox
            // 
            this.LabelTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.LabelTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTextBox.ForeColor = System.Drawing.Color.Maroon;
            this.LabelTextBox.Location = new System.Drawing.Point(80, 428);
            this.LabelTextBox.Multiline = true;
            this.LabelTextBox.Name = "LabelTextBox";
            this.LabelTextBox.ReadOnly = true;
            this.LabelTextBox.Size = new System.Drawing.Size(760, 36);
            this.LabelTextBox.TabIndex = 63;
            // 
            // userListViewControl1
            // 
            this.userListViewControl1.Location = new System.Drawing.Point(80, 238);
            this.userListViewControl1.Name = "userListViewControl1";
            this.userListViewControl1.Size = new System.Drawing.Size(760, 176);
            this.userListViewControl1.TabIndex = 61;
            // 
            // ActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 683);
            this.Controls.Add(this.LabelTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.userListViewControl1);
            this.Controls.Add(this.ActionsListView);
            this.Controls.Add(this.DescTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.EventActionIDTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FGenIDComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.ActionDateTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.EventActionListView);
            this.Controls.Add(this.EventsComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LocationTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ReaderIDComboBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Blue;
            this.MaximizeBox = false;
            this.Name = "ActionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Event-Action Configuration Form ";
            this.Load += new System.EventHandler(this.ActionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ReaderIDComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LocationTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox EventsComboBox;
        private System.Windows.Forms.ListView EventActionListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox ActionDateTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton NewToolBarButton;
        private System.Windows.Forms.ToolBarButton EditToolBarButton;
        private System.Windows.Forms.ToolBarButton CancelToolBarButton;
        private System.Windows.Forms.ToolBarButton SaveToolBarButton;
        private System.Windows.Forms.ToolBarButton DeleteToolBarButton;
        private System.Windows.Forms.ToolBarButton RefreshToolBarButton;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox FGenIDComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox EventActionIDTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox DescTextBox;
        private System.Windows.Forms.ListView ActionsListView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private UserListControl.UserListViewControl userListViewControl1;
        private System.Windows.Forms.Label label9;
        private System.Diagnostics.Process process1;
        private System.Windows.Forms.TextBox LabelTextBox;
    }
}