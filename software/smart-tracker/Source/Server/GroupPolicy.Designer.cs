namespace AWI.SmartTracker
{
    partial class GroupPolicy
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
            this.lvGroups = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelGroup = new System.Windows.Forms.Panel();
            this.lvGroupSchedules = new InheritedListView.ListViewFocus();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvGroupZones = new InheritedListView.ListViewFocus();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbZonesInsert = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSelect = new System.Windows.Forms.Button();
            this.panelGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvGroups
            // 
            this.lvGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnDescription});
            this.lvGroups.FullRowSelect = true;
            this.lvGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvGroups.HideSelection = false;
            this.lvGroups.Location = new System.Drawing.Point(12, 12);
            this.lvGroups.Name = "lvGroups";
            this.lvGroups.ShowGroups = false;
            this.lvGroups.Size = new System.Drawing.Size(227, 359);
            this.lvGroups.TabIndex = 0;
            this.lvGroups.UseCompatibleStateImageBehavior = false;
            this.lvGroups.View = System.Windows.Forms.View.Details;
            this.lvGroups.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvGroups_AfterLabelEdit);
            this.lvGroups.ItemActivate += new System.EventHandler(this.lvGroups_ItemActivate);
            this.lvGroups.SelectedIndexChanged += new System.EventHandler(this.lvGroups_SelectedIndexChanged);
            this.lvGroups.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvGroups_KeyPress);
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 80;
            // 
            // columnDescription
            // 
            this.columnDescription.Text = "Description";
            this.columnDescription.Width = 120;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(489, 377);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(408, 377);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelGroup
            // 
            this.panelGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGroup.Controls.Add(this.lvGroupSchedules);
            this.panelGroup.Controls.Add(this.lvGroupZones);
            this.panelGroup.Controls.Add(this.cbZonesInsert);
            this.panelGroup.Controls.Add(this.label4);
            this.panelGroup.Controls.Add(this.label3);
            this.panelGroup.Controls.Add(this.txtDescription);
            this.panelGroup.Controls.Add(this.txtName);
            this.panelGroup.Controls.Add(this.label2);
            this.panelGroup.Controls.Add(this.label1);
            this.panelGroup.Location = new System.Drawing.Point(245, 13);
            this.panelGroup.Name = "panelGroup";
            this.panelGroup.Size = new System.Drawing.Size(319, 358);
            this.panelGroup.TabIndex = 4;
            // 
            // lvGroupSchedules
            // 
            this.lvGroupSchedules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvGroupSchedules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.lvGroupSchedules.FullRowSelect = true;
            this.lvGroupSchedules.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvGroupSchedules.HideSelection = false;
            this.lvGroupSchedules.Location = new System.Drawing.Point(4, 228);
            this.lvGroupSchedules.Name = "lvGroupSchedules";
            this.lvGroupSchedules.ShowGroups = false;
            this.lvGroupSchedules.Size = new System.Drawing.Size(315, 129);
            this.lvGroupSchedules.TabIndex = 16;
            this.lvGroupSchedules.UseCompatibleStateImageBehavior = false;
            this.lvGroupSchedules.View = System.Windows.Forms.View.Details;
            this.lvGroupSchedules.SelectedIndexChanged += new System.EventHandler(this.lvGroupSchedule_SelectedIndexChanged);
            this.lvGroupSchedules.DoubleClick += new System.EventHandler(this.lvGroupSchedules_DoubleClick);
            this.lvGroupSchedules.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvGroupSchedule_KeyPress);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Name";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Description";
            this.columnHeader7.Width = 230;
            // 
            // lvGroupZones
            // 
            this.lvGroupZones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvGroupZones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvGroupZones.FullRowSelect = true;
            this.lvGroupZones.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvGroupZones.HideSelection = false;
            this.lvGroupZones.Location = new System.Drawing.Point(4, 68);
            this.lvGroupZones.Name = "lvGroupZones";
            this.lvGroupZones.ShowGroups = false;
            this.lvGroupZones.Size = new System.Drawing.Size(315, 129);
            this.lvGroupZones.TabIndex = 15;
            this.lvGroupZones.UseCompatibleStateImageBehavior = false;
            this.lvGroupZones.View = System.Windows.Forms.View.Details;
            this.lvGroupZones.SelectedIndexChanged += new System.EventHandler(this.lvGroupZones_SelectedIndexChanged);
            this.lvGroupZones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvGroupZones_KeyPress);
            this.lvGroupZones.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvGroupZones_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 290;
            // 
            // cbZonesInsert
            // 
            this.cbZonesInsert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZonesInsert.FormattingEnabled = true;
            this.cbZonesInsert.Location = new System.Drawing.Point(85, 44);
            this.cbZonesInsert.Name = "cbZonesInsert";
            this.cbZonesInsert.Size = new System.Drawing.Size(121, 21);
            this.cbZonesInsert.TabIndex = 14;
            this.cbZonesInsert.Visible = false;
            this.cbZonesInsert.SelectedIndexChanged += new System.EventHandler(this.cbZonesInsert_Select);
            this.cbZonesInsert.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbZonesInsert_KeyPress);
            this.cbZonesInsert.Leave += new System.EventHandler(this.cbZonesInsert_Select);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Schedule:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Zones:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(104, 21);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(215, 21);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(4, 21);
            this.txtName.MaxLength = 32;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(93, 21);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Access";
            this.columnHeader3.Width = 55;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name";
            this.columnHeader4.Width = 90;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Description";
            this.columnHeader5.Width = 145;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 290;
            // 
            // btnSelect
            // 
            this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSelect.Enabled = false;
            this.btnSelect.Location = new System.Drawing.Point(163, 378);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            // 
            // GroupPolicy
            // 
            this.AcceptButton = this.btnSelect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 412);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.panelGroup);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lvGroups);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupPolicy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Group Policy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GroupPolicy_FormClosing);
            this.panelGroup.ResumeLayout(false);
            this.panelGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvGroups;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnDescription;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panelGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private InheritedListView.ListViewFocus lvGroupZones;
        private System.Windows.Forms.Label label3;
        private InheritedListView.ListViewFocus lvGroupSchedules;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ComboBox cbZonesInsert;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button btnSelect;
    }
}