using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Threading;

using AWIComponentLib.Database;
using AWIComponentLib.Communication;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for ConfigForm.
	/// </summary>
	public class ConfigForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox DayComboBox;
		private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CloseButton;
        private GroupBox groupBox2;
        private Button DisableTagButton;
        private Button EnableTagButton;
        private ListView TagListView;
        private ColumnHeader SelectColumnHeader;
        private ColumnHeader IDColumnHeader;
        private ColumnHeader TagTypeColumnHeader;
        private GroupBox groupBox3;
        private Label label3;
        private ComboBox comboBox1;
        private Button ConfigTagButton;
        private Label label2;
        private TextBox textBox1;
        private Button RefreshListButton;
        private ColumnHeader StatusColumnHeader;
        private Label MsgLabel;
        private CheckBox SelectCheckBox;

        private MainForm mForm;
        private Button ShowAllTagsButton;
		private System.ComponentModel.Container components = null;
        private ThreadStart cmdThreadStart;
        private Thread cmdThread;
        private GroupBox groupBox4;
        private TextBox ProgStatReaderIDTextBox;
        private Label label4; 
        private ArrayList tagQueList = new ArrayList();

		public ConfigForm(MainForm form)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            mForm = form;

			if ((MainForm.spanDays >= 1) && (MainForm.spanDays <= 7))
			   DayComboBox.Text = Convert.ToString(MainForm.spanDays);
		    else
			   DayComboBox.Text = "Today";

           
		}

		public ConfigForm()
		{
			
			InitializeComponent();

            //To check the span days boundry - must fall within limits
			if ((MainForm.spanDays >= 1) && (MainForm.spanDays <= 7))
				DayComboBox.Text = Convert.ToString(MainForm.spanDays);
			else
				DayComboBox.Text = "Today";

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "120",
            "Access",
            "Enabled"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "223",
            "Asset",
            "Disabled"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "5",
            "Inventory",
            "Enabled"}, -1);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DayComboBox = new System.Windows.Forms.ComboBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SelectCheckBox = new System.Windows.Forms.CheckBox();
            this.MsgLabel = new System.Windows.Forms.Label();
            this.RefreshListButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ConfigTagButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.DisableTagButton = new System.Windows.Forms.Button();
            this.EnableTagButton = new System.Windows.Forms.Button();
            this.TagListView = new System.Windows.Forms.ListView();
            this.SelectColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.IDColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.TagTypeColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.StatusColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.ShowAllTagsButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ProgStatReaderIDTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.DayComboBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Purple;
            this.groupBox1.Location = new System.Drawing.Point(14, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 68);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ledger Time Span Display";
            
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(18, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of Days: ";
            // 
            // DayComboBox
            // 
            this.DayComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DayComboBox.ForeColor = System.Drawing.Color.Blue;
            this.DayComboBox.Items.AddRange(new object[] {
            "Today",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.DayComboBox.Location = new System.Drawing.Point(132, 28);
            this.DayComboBox.Name = "DayComboBox";
            this.DayComboBox.Size = new System.Drawing.Size(90, 28);
            this.DayComboBox.TabIndex = 0;
            this.DayComboBox.Text = "Today";
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.ForeColor = System.Drawing.Color.Purple;
            this.OKButton.Location = new System.Drawing.Point(156, 562);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(116, 38);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.Purple;
            this.CloseButton.Location = new System.Drawing.Point(360, 562);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(116, 38);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ShowAllTagsButton);
            this.groupBox2.Controls.Add(this.SelectCheckBox);
            this.groupBox2.Controls.Add(this.MsgLabel);
            this.groupBox2.Controls.Add(this.RefreshListButton);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.DisableTagButton);
            this.groupBox2.Controls.Add(this.EnableTagButton);
            this.groupBox2.Controls.Add(this.TagListView);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Purple;
            this.groupBox2.Location = new System.Drawing.Point(12, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(598, 460);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tag Configuration";
            // 
            // SelectCheckBox
            // 
            this.SelectCheckBox.AutoSize = true;
            this.SelectCheckBox.ForeColor = System.Drawing.Color.Blue;
            this.SelectCheckBox.Location = new System.Drawing.Point(20, 370);
            this.SelectCheckBox.Name = "SelectCheckBox";
            this.SelectCheckBox.Size = new System.Drawing.Size(87, 22);
            this.SelectCheckBox.TabIndex = 49;
            this.SelectCheckBox.Text = "Select All";
            this.SelectCheckBox.UseVisualStyleBackColor = true;
            
            // 
            // MsgLabel
            // 
            this.MsgLabel.BackColor = System.Drawing.SystemColors.Info;
            this.MsgLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MsgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MsgLabel.ForeColor = System.Drawing.Color.Blue;
            this.MsgLabel.Location = new System.Drawing.Point(354, 364);
            this.MsgLabel.Name = "MsgLabel";
            this.MsgLabel.Size = new System.Drawing.Size(214, 86);
            this.MsgLabel.TabIndex = 48;
            this.MsgLabel.Text = "Messages: ";
            // 
            // RefreshListButton
            // 
            this.RefreshListButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.RefreshListButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshListButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshListButton.ForeColor = System.Drawing.Color.White;
            this.RefreshListButton.Location = new System.Drawing.Point(124, 368);
            this.RefreshListButton.Name = "RefreshListButton";
            this.RefreshListButton.Size = new System.Drawing.Size(202, 30);
            this.RefreshListButton.TabIndex = 47;
            this.RefreshListButton.Text = "Get Tag(s) Status";
            this.RefreshListButton.UseVisualStyleBackColor = false;
            
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.ConfigTagButton);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Location = new System.Drawing.Point(354, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(214, 184);
            this.groupBox3.TabIndex = 46;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "New Configuration";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(24, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 23);
            this.label3.TabIndex = 50;
            this.label3.Text = "New Type: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox1
            // 
            this.comboBox1.ForeColor = System.Drawing.Color.Blue;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Access",
            "Asset",
            "Inventory"});
            this.comboBox1.Location = new System.Drawing.Point(100, 74);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(84, 26);
            this.comboBox1.TabIndex = 49;
            // 
            // ConfigTagButton
            // 
            this.ConfigTagButton.BackColor = System.Drawing.Color.Blue;
            this.ConfigTagButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConfigTagButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigTagButton.ForeColor = System.Drawing.Color.White;
            this.ConfigTagButton.Location = new System.Drawing.Point(38, 124);
            this.ConfigTagButton.Name = "ConfigTagButton";
            this.ConfigTagButton.Size = new System.Drawing.Size(140, 42);
            this.ConfigTagButton.TabIndex = 48;
            this.ConfigTagButton.Text = "Config Tag";
            this.ConfigTagButton.UseVisualStyleBackColor = false;
            this.ConfigTagButton.Click += new System.EventHandler(this.ConfigTagButton_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(24, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 22);
            this.label2.TabIndex = 47;
            this.label2.Text = "New ID: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Blue;
            this.textBox1.Location = new System.Drawing.Point(100, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(82, 24);
            this.textBox1.TabIndex = 46;
            // 
            // DisableTagButton
            // 
            this.DisableTagButton.BackColor = System.Drawing.Color.Crimson;
            this.DisableTagButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisableTagButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisableTagButton.ForeColor = System.Drawing.Color.White;
            this.DisableTagButton.Location = new System.Drawing.Point(386, 106);
            this.DisableTagButton.Name = "DisableTagButton";
            this.DisableTagButton.Size = new System.Drawing.Size(138, 42);
            this.DisableTagButton.TabIndex = 40;
            this.DisableTagButton.Text = "Disable Tag(s)";
            this.DisableTagButton.UseVisualStyleBackColor = false;
            this.DisableTagButton.Click += new System.EventHandler(this.DisableTagButton_Click);
            // 
            // EnableTagButton
            // 
            this.EnableTagButton.BackColor = System.Drawing.Color.Green;
            this.EnableTagButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EnableTagButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnableTagButton.ForeColor = System.Drawing.Color.White;
            this.EnableTagButton.Location = new System.Drawing.Point(386, 38);
            this.EnableTagButton.Name = "EnableTagButton";
            this.EnableTagButton.Size = new System.Drawing.Size(138, 42);
            this.EnableTagButton.TabIndex = 39;
            this.EnableTagButton.Text = "Enable Tag(s)";
            this.EnableTagButton.UseVisualStyleBackColor = false;
            this.EnableTagButton.Click += new System.EventHandler(this.EnableTagButton_Click);
            // 
            // TagListView
            // 
            this.TagListView.BackColor = System.Drawing.Color.LightSteelBlue;
            this.TagListView.CheckBoxes = true;
            this.TagListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SelectColumnHeader,
            this.IDColumnHeader,
            this.TagTypeColumnHeader,
            this.StatusColumnHeader});
            this.TagListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TagListView.FullRowSelect = true;
            this.TagListView.GridLines = true;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            this.TagListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.TagListView.Location = new System.Drawing.Point(22, 38);
            this.TagListView.MultiSelect = false;
            this.TagListView.Name = "TagListView";
            this.TagListView.OwnerDraw = true;
            this.TagListView.Size = new System.Drawing.Size(304, 314);
            this.TagListView.TabIndex = 38;
            this.TagListView.UseCompatibleStateImageBehavior = false;
            this.TagListView.View = System.Windows.Forms.View.Details;
            
            
            // 
            // SelectColumnHeader
            // 
            this.SelectColumnHeader.Text = "Select";
            this.SelectColumnHeader.Width = 59;
            // 
            // IDColumnHeader
            // 
            this.IDColumnHeader.Text = "ID";
            this.IDColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IDColumnHeader.Width = 75;
            // 
            // TagTypeColumnHeader
            // 
            this.TagTypeColumnHeader.Text = "Type";
            this.TagTypeColumnHeader.Width = 85;
            // 
            // StatusColumnHeader
            // 
            this.StatusColumnHeader.Text = "Status";
            this.StatusColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.StatusColumnHeader.Width = 80;
            // 
            // ShowAllTagsButton
            // 
            this.ShowAllTagsButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.ShowAllTagsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowAllTagsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowAllTagsButton.ForeColor = System.Drawing.Color.White;
            this.ShowAllTagsButton.Location = new System.Drawing.Point(124, 414);
            this.ShowAllTagsButton.Name = "ShowAllTagsButton";
            this.ShowAllTagsButton.Size = new System.Drawing.Size(202, 30);
            this.ShowAllTagsButton.TabIndex = 50;
            this.ShowAllTagsButton.Text = "Show All Registered Tags ";
            this.ShowAllTagsButton.UseVisualStyleBackColor = false;
            this.ShowAllTagsButton.Click += new System.EventHandler(this.ShowAllTagsButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ProgStatReaderIDTextBox);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Purple;
            this.groupBox4.Location = new System.Drawing.Point(370, 10);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(240, 68);
            this.groupBox4.TabIndex = 39;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Programmin Station Reader ";
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(18, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 23);
            this.label4.TabIndex = 1;
            this.label4.Text = "Reader ID: ";
            // 
            // ProgStatReaderIDTextBox
            // 
            this.ProgStatReaderIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgStatReaderIDTextBox.ForeColor = System.Drawing.Color.Blue;
            this.ProgStatReaderIDTextBox.Location = new System.Drawing.Point(96, 30);
            this.ProgStatReaderIDTextBox.Name = "ProgStatReaderIDTextBox";
            this.ProgStatReaderIDTextBox.Size = new System.Drawing.Size(82, 24);
            this.ProgStatReaderIDTextBox.TabIndex = 47;
            // 
            // ConfigForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(624, 606);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration";
            
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        
		

        private void OKButton_Click(object sender, System.EventArgs e)
		{
			
		}

		private void CancelButton_Click(object sender, System.EventArgs e)
		{
		
		}

        

        private void EnableTagButton_Click(object sender, EventArgs e)
        {
            //This routine will enable tag(s) that are in the system
        }

        private void DisableTagButton_Click(object sender, EventArgs e)
        {
           //This routine will disable tag(s) that are in the system
        }

        private void ShowAllTagsButton_Click(object sender, EventArgs e)
        {            
            
        }

        private void ConfigTagButton_Click(object sender, EventArgs e)
        {
            //This routine will config the tag with new id and type
            
        }
    }
}
