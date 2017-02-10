using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Threading;
using System.Globalization;
using System.Text;
using AWIComponentLib.Database;
using System.Drawing.Drawing2D;


namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for Form4.
	/// </summary>
	
	public class ZonesForm : System.Windows.Forms.Form, IComparer
	{
		private System.Windows.Forms.Button DeleteButton;
		private System.Windows.Forms.Button FirstButton;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox ZoneLocTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button RefreshButton;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Button NewButton;
		private System.Windows.Forms.Button PreButton;
		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Button LastButton;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.TextBox ReaderIDTextBox;
		private System.Windows.Forms.TextBox FGenIDTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox TimeTextBox;
		private bool newRec = false;
		//private MySqlConnection m_connection1 = null;
		private OdbcConnection m_connection = null;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox StatusTextBox;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private int MAX_DEMO_RECORDS = 10;
		//private int records = 0;
		private System.Windows.Forms.TextBox OwnerIDTextBox;
		private System.Windows.Forms.TextBox LineIDTextBox;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton NewToolBarButton;
		private System.Windows.Forms.ToolBarButton EditToolBarButton;
		private System.Windows.Forms.ToolBarButton CancelToolBarButton;
		private System.Windows.Forms.ToolBarButton SaveToolBarButton;
		private System.Windows.Forms.ToolBarButton DeleteToolBarButton;
		private System.Windows.Forms.ToolBarButton RefreshToolBarButton;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ComboBox OwnerIDComboBox;
		//private System.Windows.Forms.ComboBox ZoneIDComboBox;
		private System.Windows.Forms.CheckBox LineTagCheckBox;
		private System.Windows.Forms.Label label7;
		private System.ComponentModel.IContainer components;
		public OdbcDbClass odbcDB = new OdbcDbClass();
		private System.Windows.Forms.TextBox ZoneIDEditBox;
        public AWIHelperClass awiHelper;
		public bool editMode = false;
		private ListViewItem lastSelectedItem = null;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label DBStatusLabel;
		private string zoneID = "";
		private int m_sortColumn = -1;
		private bool m_sortReverse= false;
		private System.Timers.Timer timer1;
		private System.Windows.Forms.CheckBox RSSICheckBox;
		private System.Windows.Forms.TextBox RSSITextBox;
		private System.Windows.Forms.Label RSSILabel;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label EnableRSSILabel;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private MainForm mForm;
        private Label label10;
        private CheckBox RS232CheckBox;
        private Label label11;
        private ColumnHeader columnHeader9;
		private bool rssiChecked;
        //private bool clicked = false;
        private int listIndex = -1;

		public ZonesForm() {}

		public ZonesForm(MainForm main)
		{
			mForm = main;
            awiHelper = new AWIHelperClass(main);
			InitializeComponent();

			rssiChecked = false;
			//changes made to support MYSQL Server v5.0 and later
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

			//string connectionString = string.Format("server={0};port={1};username={2};password={3};database={4};pooling=false", "localhost", "3306", "root", "", "cartracker");	 //Jan PW=AWMYSQL
			//event to get status of database (open, lost, close)
			OdbcDbClass.NotifyDBConnectionStatusHandler += new NotifyDBConnectionStatus(DBConnectionStatusHandler);
			
			if (MainForm.m_connection == null)
			{
				DBStatusLabel.ForeColor = System.Drawing.Color.Red;
				DBStatusLabel.Text = "Disconnected";
				toolBar1.Enabled = false;
				FirstButton.Enabled = false;
				LastButton.Enabled = false;
				NextButton.Enabled = false;
				PreButton.Enabled = false;
				return;
			}
			else
				m_connection = MainForm.m_connection;


			RefreshScreen();
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
            this.components = new System.ComponentModel.Container();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.FirstButton = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.ReaderIDTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ZoneLocTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.NewButton = new System.Windows.Forms.Button();
            this.PreButton = new System.Windows.Forms.Button();
            this.FGenIDTextBox = new System.Windows.Forms.TextBox();
            this.NextButton = new System.Windows.Forms.Button();
            this.LastButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TimeTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.StatusTextBox = new System.Windows.Forms.TextBox();
            this.OwnerIDTextBox = new System.Windows.Forms.TextBox();
            this.LineIDTextBox = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.NewToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.EditToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.CancelToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.SaveToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.DeleteToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.RefreshToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.OwnerIDComboBox = new System.Windows.Forms.ComboBox();
            this.LineTagCheckBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ZoneIDEditBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DBStatusLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Timers.Timer();
            this.RSSICheckBox = new System.Windows.Forms.CheckBox();
            this.RSSITextBox = new System.Windows.Forms.TextBox();
            this.RSSILabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.EnableRSSILabel = new System.Windows.Forms.Label();
            this.RS232CheckBox = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // DeleteButton
            // 
            this.DeleteButton.ForeColor = System.Drawing.Color.Blue;
            this.DeleteButton.Location = new System.Drawing.Point(620, 196);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(30, 24);
            this.DeleteButton.TabIndex = 36;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.Visible = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // FirstButton
            // 
            this.FirstButton.ForeColor = System.Drawing.Color.Blue;
            this.FirstButton.Location = new System.Drawing.Point(190, 612);
            this.FirstButton.Name = "FirstButton";
            this.FirstButton.Size = new System.Drawing.Size(75, 23);
            this.FirstButton.TabIndex = 30;
            this.FirstButton.Text = "First";
            this.FirstButton.Click += new System.EventHandler(this.FirstButton_Click);
            // 
            // listView
            // 
            this.listView.BackColor = System.Drawing.SystemColors.Info;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader5});
            this.listView.ForeColor = System.Drawing.Color.Navy;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(6, 302);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.OwnerDraw = true;
            this.listView.Size = new System.Drawing.Size(640, 300);
            this.listView.TabIndex = 29;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            this.listView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_MouseUp);
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.listView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView_MouseMove);
            this.listView.MouseLeave += new System.EventHandler(this.listView_MouseLeave);
            this.listView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.listView.Click += new System.EventHandler(this.listView_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Location ID";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Location Name";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 200;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Reader ID";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 65;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "FGen ID";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "RSSI Enabled";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Threshold";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Status";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 70;
            // 
            // columnHeader9
            // 
            this.columnHeader9.DisplayIndex = 8;
            this.columnHeader9.Text = "Reader Type";
            this.columnHeader9.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.DisplayIndex = 7;
            this.columnHeader5.Text = "Time";
            this.columnHeader5.Width = 140;
            // 
            // ReaderIDTextBox
            // 
            this.ReaderIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReaderIDTextBox.Location = new System.Drawing.Point(138, 144);
            this.ReaderIDTextBox.Name = "ReaderIDTextBox";
            this.ReaderIDTextBox.ReadOnly = true;
            this.ReaderIDTextBox.Size = new System.Drawing.Size(90, 22);
            this.ReaderIDTextBox.TabIndex = 2;
            this.ReaderIDTextBox.TextChanged += new System.EventHandler(this.DescriptTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(56, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 26;
            this.label4.Text = "FGen ID:  ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(48, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 18);
            this.label3.TabIndex = 24;
            this.label3.Text = "Reader ID:  ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // ZoneLocTextBox
            // 
            this.ZoneLocTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZoneLocTextBox.Location = new System.Drawing.Point(138, 106);
            this.ZoneLocTextBox.Name = "ZoneLocTextBox";
            this.ZoneLocTextBox.ReadOnly = true;
            this.ZoneLocTextBox.Size = new System.Drawing.Size(330, 22);
            this.ZoneLocTextBox.TabIndex = 1;
            this.ZoneLocTextBox.TextChanged += new System.EventHandler(this.ZoneLocTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(40, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 18);
            this.label1.TabIndex = 20;
            this.label1.Text = "Location ID : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(26, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 18);
            this.label2.TabIndex = 22;
            this.label2.Text = " Location Name: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RefreshButton
            // 
            this.RefreshButton.ForeColor = System.Drawing.Color.Blue;
            this.RefreshButton.Location = new System.Drawing.Point(618, 226);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(30, 24);
            this.RefreshButton.TabIndex = 37;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.Visible = false;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.ForeColor = System.Drawing.Color.Blue;
            this.SaveButton.Location = new System.Drawing.Point(584, 198);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(28, 24);
            this.SaveButton.TabIndex = 35;
            this.SaveButton.Text = "Save";
            this.SaveButton.Visible = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // NewButton
            // 
            this.NewButton.ForeColor = System.Drawing.Color.Blue;
            this.NewButton.Location = new System.Drawing.Point(592, 228);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(20, 24);
            this.NewButton.TabIndex = 34;
            this.NewButton.Text = "New";
            this.NewButton.Visible = false;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // PreButton
            // 
            this.PreButton.ForeColor = System.Drawing.Color.Blue;
            this.PreButton.Location = new System.Drawing.Point(418, 612);
            this.PreButton.Name = "PreButton";
            this.PreButton.Size = new System.Drawing.Size(75, 23);
            this.PreButton.TabIndex = 33;
            this.PreButton.Text = "Previous";
            this.PreButton.Click += new System.EventHandler(this.PreButton_Click);
            // 
            // FGenIDTextBox
            // 
            this.FGenIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FGenIDTextBox.Location = new System.Drawing.Point(138, 186);
            this.FGenIDTextBox.Name = "FGenIDTextBox";
            this.FGenIDTextBox.ReadOnly = true;
            this.FGenIDTextBox.Size = new System.Drawing.Size(90, 22);
            this.FGenIDTextBox.TabIndex = 5;
            this.FGenIDTextBox.TextChanged += new System.EventHandler(this.ReaderIDTextBox_TextChanged);
            // 
            // NextButton
            // 
            this.NextButton.ForeColor = System.Drawing.Color.Blue;
            this.NextButton.Location = new System.Drawing.Point(342, 612);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 32;
            this.NextButton.Text = "Next";
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // LastButton
            // 
            this.LastButton.ForeColor = System.Drawing.Color.Blue;
            this.LastButton.Location = new System.Drawing.Point(266, 612);
            this.LastButton.Name = "LastButton";
            this.LastButton.Size = new System.Drawing.Size(75, 23);
            this.LastButton.TabIndex = 31;
            this.LastButton.Text = "Last";
            this.LastButton.Click += new System.EventHandler(this.LastButton_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(40, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 18);
            this.label5.TabIndex = 39;
            this.label5.Text = "Date && Time:  ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TimeTextBox
            // 
            this.TimeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeTextBox.Location = new System.Drawing.Point(138, 266);
            this.TimeTextBox.Name = "TimeTextBox";
            this.TimeTextBox.ReadOnly = true;
            this.TimeTextBox.Size = new System.Drawing.Size(144, 22);
            this.TimeTextBox.TabIndex = 38;
            this.TimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(68, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 18);
            this.label6.TabIndex = 41;
            this.label6.Text = "Status:  ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusTextBox.Location = new System.Drawing.Point(138, 226);
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.ReadOnly = true;
            this.StatusTextBox.Size = new System.Drawing.Size(90, 22);
            this.StatusTextBox.TabIndex = 40;
            // 
            // OwnerIDTextBox
            // 
            this.OwnerIDTextBox.Location = new System.Drawing.Point(624, 48);
            this.OwnerIDTextBox.Name = "OwnerIDTextBox";
            this.OwnerIDTextBox.ReadOnly = true;
            this.OwnerIDTextBox.Size = new System.Drawing.Size(24, 20);
            this.OwnerIDTextBox.TabIndex = 65;
            this.OwnerIDTextBox.Visible = false;
            // 
            // LineIDTextBox
            // 
            this.LineIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineIDTextBox.Location = new System.Drawing.Point(510, 200);
            this.LineIDTextBox.Name = "LineIDTextBox";
            this.LineIDTextBox.ReadOnly = true;
            this.LineIDTextBox.Size = new System.Drawing.Size(22, 22);
            this.LineIDTextBox.TabIndex = 66;
            this.LineIDTextBox.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(55, 12);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
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
            this.toolBar1.ButtonSize = new System.Drawing.Size(55, 40);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(654, 38);
            this.toolBar1.TabIndex = 6;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // NewToolBarButton
            // 
            this.NewToolBarButton.Name = "NewToolBarButton";
            this.NewToolBarButton.Text = "New";
            // 
            // EditToolBarButton
            // 
            this.EditToolBarButton.Name = "EditToolBarButton";
            this.EditToolBarButton.Text = "Edit";
            // 
            // CancelToolBarButton
            // 
            this.CancelToolBarButton.Name = "CancelToolBarButton";
            this.CancelToolBarButton.Text = "Cancel";
            // 
            // SaveToolBarButton
            // 
            this.SaveToolBarButton.Name = "SaveToolBarButton";
            this.SaveToolBarButton.Text = "Save";
            // 
            // DeleteToolBarButton
            // 
            this.DeleteToolBarButton.Name = "DeleteToolBarButton";
            this.DeleteToolBarButton.Text = "Delete";
            // 
            // RefreshToolBarButton
            // 
            this.RefreshToolBarButton.Name = "RefreshToolBarButton";
            this.RefreshToolBarButton.Text = "Refresh";
            // 
            // OwnerIDComboBox
            // 
            this.OwnerIDComboBox.Enabled = false;
            this.OwnerIDComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OwnerIDComboBox.Location = new System.Drawing.Point(622, 72);
            this.OwnerIDComboBox.Name = "OwnerIDComboBox";
            this.OwnerIDComboBox.Size = new System.Drawing.Size(28, 24);
            this.OwnerIDComboBox.TabIndex = 70;
            this.OwnerIDComboBox.Visible = false;
            // 
            // LineTagCheckBox
            // 
            this.LineTagCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LineTagCheckBox.Enabled = false;
            this.LineTagCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineTagCheckBox.Location = new System.Drawing.Point(562, 200);
            this.LineTagCheckBox.Name = "LineTagCheckBox";
            this.LineTagCheckBox.Size = new System.Drawing.Size(16, 24);
            this.LineTagCheckBox.TabIndex = 72;
            this.LineTagCheckBox.Visible = false;
            this.LineTagCheckBox.Click += new System.EventHandler(this.LineTagCheckBox_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(540, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 18);
            this.label7.TabIndex = 73;
            this.label7.Text = "Line Tag ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Visible = false;
            // 
            // ZoneIDEditBox
            // 
            this.ZoneIDEditBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZoneIDEditBox.Location = new System.Drawing.Point(138, 64);
            this.ZoneIDEditBox.Name = "ZoneIDEditBox";
            this.ZoneIDEditBox.ReadOnly = true;
            this.ZoneIDEditBox.Size = new System.Drawing.Size(90, 24);
            this.ZoneIDEditBox.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(428, 266);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 18);
            this.label8.TabIndex = 75;
            this.label8.Text = "Database Status: ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DBStatusLabel
            // 
            this.DBStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DBStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.DBStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DBStatusLabel.Location = new System.Drawing.Point(540, 266);
            this.DBStatusLabel.Name = "DBStatusLabel";
            this.DBStatusLabel.Size = new System.Drawing.Size(102, 18);
            this.DBStatusLabel.TabIndex = 76;
            this.DBStatusLabel.Text = "Disconnected";
            this.DBStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // RSSICheckBox
            // 
            this.RSSICheckBox.Enabled = false;
            this.RSSICheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RSSICheckBox.Location = new System.Drawing.Point(366, 144);
            this.RSSICheckBox.Name = "RSSICheckBox";
            this.RSSICheckBox.Size = new System.Drawing.Size(14, 24);
            this.RSSICheckBox.TabIndex = 3;
            this.RSSICheckBox.Click += new System.EventHandler(this.RSSICheckBox_Click);
            this.RSSICheckBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RSSICheckBox_MouseDown);
            // 
            // RSSITextBox
            // 
            this.RSSITextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RSSITextBox.Location = new System.Drawing.Point(585, 144);
            this.RSSITextBox.MaxLength = 3;
            this.RSSITextBox.Name = "RSSITextBox";
            this.RSSITextBox.ReadOnly = true;
            this.RSSITextBox.Size = new System.Drawing.Size(56, 22);
            this.RSSITextBox.TabIndex = 4;
            // 
            // RSSILabel
            // 
            this.RSSILabel.ForeColor = System.Drawing.Color.Navy;
            this.RSSILabel.Location = new System.Drawing.Point(586, 168);
            this.RSSILabel.Name = "RSSILabel";
            this.RSSILabel.Size = new System.Drawing.Size(57, 16);
            this.RSSILabel.TabIndex = 79;
            this.RSSILabel.Text = "( 0 - 255 )";
            this.RSSILabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Navy;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point(476, 146);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 18);
            this.label9.TabIndex = 80;
            this.label9.Text = "RSSI Threshold: ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EnableRSSILabel
            // 
            this.EnableRSSILabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnableRSSILabel.ForeColor = System.Drawing.Color.Navy;
            this.EnableRSSILabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.EnableRSSILabel.Location = new System.Drawing.Point(380, 146);
            this.EnableRSSILabel.Name = "EnableRSSILabel";
            this.EnableRSSILabel.Size = new System.Drawing.Size(86, 18);
            this.EnableRSSILabel.TabIndex = 81;
            this.EnableRSSILabel.Text = "Enable RSSI  ";
            this.EnableRSSILabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EnableRSSILabel.Click += new System.EventHandler(this.EnableRSSILabel_Click);
            // 
            // RS232CheckBox
            // 
            this.RS232CheckBox.Enabled = false;
            this.RS232CheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RS232CheckBox.Location = new System.Drawing.Point(246, 144);
            this.RS232CheckBox.Name = "RS232CheckBox";
            this.RS232CheckBox.Size = new System.Drawing.Size(14, 24);
            this.RS232CheckBox.TabIndex = 82;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Navy;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Location = new System.Drawing.Point(262, 146);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 18);
            this.label10.TabIndex = 83;
            this.label10.Text = "RS232  ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Navy;
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Location = new System.Drawing.Point(230, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 18);
            this.label11.TabIndex = 84;
            this.label11.Text = "(decimal)  ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ZonesForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(654, 644);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.RS232CheckBox);
            this.Controls.Add(this.EnableRSSILabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.RSSILabel);
            this.Controls.Add(this.RSSITextBox);
            this.Controls.Add(this.RSSICheckBox);
            this.Controls.Add(this.DBStatusLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ZoneIDEditBox);
            this.Controls.Add(this.LineIDTextBox);
            this.Controls.Add(this.OwnerIDTextBox);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.TimeTextBox);
            this.Controls.Add(this.ReaderIDTextBox);
            this.Controls.Add(this.ZoneLocTextBox);
            this.Controls.Add(this.FGenIDTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.LineTagCheckBox);
            this.Controls.Add(this.OwnerIDComboBox);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.FirstButton);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.PreButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.LastButton);
            this.Controls.Add(this.DeleteButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZonesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Smart Tracker Zones Setup Form";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void RefreshScreen()
		{
			if (m_connection == null)
			{
				DBStatusLabel.ForeColor = System.Drawing.Color.Red;
				DBStatusLabel.Text = "Disconnected";
				toolBar1.Enabled = false;
						
				if (MainForm.reconnectCounter < 0)
					MainForm.reconnectCounter = 0;
					
				MainForm.dbDisconnectedFlag = true;

				return;
			}
			else if ((m_connection.State == System.Data.ConnectionState.Broken) ||
				(m_connection.State == System.Data.ConnectionState.Closed))
			{
				DBStatusLabel.ForeColor = System.Drawing.Color.Red;
				DBStatusLabel.Text = "Disconnected";
				toolBar1.Enabled = false;
						
				if (MainForm.reconnectCounter < 0)
					MainForm.reconnectCounter = 0;
					
				MainForm.dbDisconnectedFlag = true;

				return;
			}

			
			{
				string mySelectQuery = "SELECT ID, Location, ReaderID, FieldGenID, RSSI, Threshold, Status, ReaderType, Time FROM zones";
	
				OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection); 
		
				OdbcDataReader myReader = null;
		
		
				try
				{
					myReader = myCommand.ExecuteReader();
				}
				catch (Exception ex)
				{
					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{  
						//error code 2013
						DBStatusLabel.ForeColor = System.Drawing.Color.Red;
						DBStatusLabel.Text = "Disconnected";
						toolBar1.Enabled = false;
					
						if (MainForm.reconnectCounter < 0)
						{
							MainForm.reconnectCounter = 0;
							//timer1.Enabled = true;
						}
						MainForm.dbDisconnectedFlag = true;	
                        
						DBStatusLabel.ForeColor = System.Drawing.Color.Red;
						DBStatusLabel.Text = "Disconnected";
						toolBar1.Enabled = false;
					}

					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				}//catch .. try
		
				MainForm.dbDisconnectedFlag = false;
				MainForm.reconnectCounter = -1;
				DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
				DBStatusLabel.Text = "Connected";
				toolBar1.Enabled = true;

				bool firstRec = true;
		
				while (myReader.Read())
				{
					ListViewItem listItem = new ListViewItem(myReader.GetString(0));  //zone ID
					try
					{
						listItem.SubItems.Add(myReader.GetString(1));  //location
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(myReader.GetString(2));  //reader id
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(myReader.GetString(3));  //field gen id
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						if (myReader.GetBoolean(4))
							listItem.SubItems.Add("True");  //RSSI
						else
							listItem.SubItems.Add("False");  
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(myReader.GetString(5));  //Threshold
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(myReader.GetString(6));  //status
					}
					catch
					{
						listItem.SubItems.Add("");
					}

                    try
                    {
                        if (myReader.GetBoolean(7))
                            listItem.SubItems.Add("RS232");  //RS232
                        else
                            listItem.SubItems.Add("Network");
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

					try
					{
						listItem.SubItems.Add(myReader.GetDateTime(8).ToString("MM-dd-yyyy  HH:mm:ss"));  //time stamp
					}
					catch
					{
						listItem.SubItems.Add("");
					}
				
					listView.Items.Add(listItem);
					if (firstRec)
					{
						listItem.Selected = true;
						firstRec = false;
						ZoneIDEditBox.Text = myReader.GetString(0);
						try
						{
							ZoneLocTextBox.Text = myReader.GetString(1);
						}
						catch
						{
							ZoneLocTextBox.Text = "";
						}

						try
						{
							ReaderIDTextBox.Text = myReader.GetString(2);
						}
						catch
						{
							ReaderIDTextBox.Text = "";
						}

						try
						{
							FGenIDTextBox.Text = myReader.GetString(3);
						}
						catch
						{
							FGenIDTextBox.Text = "";
						}

						try
						{
							if (myReader.GetBoolean(4))
								RSSICheckBox.Checked = true;  //RSSI
							else
								RSSICheckBox.Checked = false;  
						}
						catch
						{
							listItem.SubItems.Add("");
						}

						try
						{
							RSSITextBox.Text = myReader.GetString(5);  //Threshold
						}
						catch
						{
							listItem.SubItems.Add("");
						}

						try
						{
							StatusTextBox.Text = myReader.GetString(6);
						}
						catch
						{
							StatusTextBox.Text = "";
						}

                        try
                        {
                            if (myReader.GetBoolean(7))
                                RS232CheckBox.Checked = true;  //RS232
                            else
                                RS232CheckBox.Checked = false; //Network
                        }
                        catch
                        {
                            RS232CheckBox.Checked = false; //Network
                        }

						try
						{
							TimeTextBox.Text = myReader.GetDateTime(8).ToString("MM-dd-yyyy  HH:mm:ss");  //myReader.GetString(5);//myReader.GetString(5);
						}
						catch
						{
							TimeTextBox.Text = "";
						}
				
					}
				}//while
				myReader.Close();
			}//lock
			
		}

		private void ZoneLocTextBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void Form4_Load(object sender, System.EventArgs e)
		{
		
		}

		private void DescriptTextBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void label4_Click(object sender, System.EventArgs e)
		{
		
		}

		private void label3_Click(object sender, System.EventArgs e)
		{
		
		}

		private void ReaderIDTextBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void ShowErrorMessage(string msg)
		{
            MessageBox.Show(this, msg, "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void DBConnectionStatusHandler(status stat, OdbcConnection connect)
		{
			if (stat == status.open)
			{
				m_connection = connect;
                RefreshScreen();
				/*MainForm.reconnectCounter = -1;
				timer1.Enabled = false;
				DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
				DBStatusLabel.Text = "Connected";
				toolBar1.Enabled = true;*/
			}
			else if (stat == status.broken)
			{
				m_connection = null;
				DBStatusLabel.ForeColor = System.Drawing.Color.Red;
				DBStatusLabel.Text = "Disconnected";
				toolBar1.Enabled = false;
			}
			else if (stat == status.close)
			{
				m_connection = null;
				DBStatusLabel.ForeColor = System.Drawing.Color.Red;
				DBStatusLabel.Text = "Disconnected";
				toolBar1.Enabled = false;
			}
		}

		private void NewButton_Click(object sender, System.EventArgs e)
		{
			

			SaveButton.Enabled = true;
			ZoneIDEditBox.Text = "";
			ZoneLocTextBox.Text = "";
			ReaderIDTextBox.Text = "";
			FGenIDTextBox.Text = "";
			StatusTextBox.Text = "";
			TimeTextBox.Text = "";
			ZoneIDEditBox.Focus();

			ZoneIDEditBox.ReadOnly = false;
			ZoneLocTextBox.ReadOnly = false;
			ReaderIDTextBox.ReadOnly = false;
			FGenIDTextBox.ReadOnly = false;
			RSSICheckBox.Enabled = true;
			RSSICheckBox.Checked = false;
		}

		private void SaveButton_Click(object sender, System.EventArgs e)
		{
		
			if (ZoneIDEditBox.Text.Equals(""))
			{
				ShowErrorMessage("Need Zone ID");
				ZoneIDEditBox.Focus();
				return;
			}

			ZoneIDEditBox.ReadOnly = true;
			ZoneLocTextBox.ReadOnly = true;
			ReaderIDTextBox.ReadOnly = true;
			FGenIDTextBox.ReadOnly = true;
			RSSICheckBox.Enabled = false;

			SaveButton.Enabled = false;

			StringBuilder sql = new StringBuilder();
			sql.Append("INSERT INTO zones (ID, Location, ReaderID, FieldGenID) Values ");         
			sql.AppendFormat("( '{0}', ", ZoneIDEditBox.Text); //gIDTextBox.Text, "AST", OwnerNameTextBox.Text, VINTextBox.Text, DescriptTextBox.Text, null);
			sql.AppendFormat(" '{0}', ", ZoneLocTextBox.Text);
			sql.AppendFormat(" '{0}', ", ReaderIDTextBox.Text);
			sql.AppendFormat(" '{0}' )", FGenIDTextBox.Text);
			OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
			
			try
			{
				myCommand.ExecuteNonQuery();
				//MainForm.reconnectCounter = -1;
				//timer1.Enabled = false;
			}
			catch (Exception ex)
			{
				int ret = 0, ret1 = 0, ret2 = 0;
				if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
					((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
					((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
				{  
					//error code 2013
					if (MainForm.reconnectCounter < 0)
					{
						MainForm.reconnectCounter = 0;
						//timer1.Enabled = true;
					}

					MainForm.dbDisconnectedFlag = true;
				}
				else
				   ShowErrorMessage(ex.Message);
				return;
			}

			DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
			DBStatusLabel.Text = "Connected";
			toolBar1.Enabled = true;

			ListViewItem listItem = new ListViewItem(ZoneIDEditBox.Text);
			listItem.SubItems.Add(ZoneLocTextBox.Text);
			listItem.SubItems.Add(ReaderIDTextBox.Text);
			listItem.SubItems.Add(FGenIDTextBox.Text);
			listItem.SubItems.Add("Offline");
			listItem.SubItems.Add(DateTime.Now.ToString());
			listView.Items.Add(listItem);
			listItem.EnsureVisible();
			listItem.Selected = true;

			
		}

		private void DeleteButton_Click(object sender, System.EventArgs e)
		{
			if (ZoneIDEditBox.Text.Equals(""))
			{
				ShowErrorMessage("No Tag ID");
				return;
			}

			ZoneIDEditBox.ReadOnly = true;
			ZoneLocTextBox.ReadOnly = true;
			ReaderIDTextBox.ReadOnly = true;
			FGenIDTextBox.ReadOnly = true;
			RSSICheckBox.Enabled = false;

			SaveButton.Enabled = false;

			StringBuilder sql = new StringBuilder();
			sql.Append("DELETE FROM zones WHERE ID = ");         
			sql.AppendFormat("'{0}'", ZoneIDEditBox.Text);
			OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
			
			try
			{
				myCommand.ExecuteNonQuery();
				//MainForm.reconnectCounter = -1;
				//timer1.Enabled = false;
			}
			catch (Exception ex)
			{
				int ret = 0, ret1 = 0, ret2 = 0;
				if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
					((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
					((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
				{  
					//error code 2013
					if (MainForm.reconnectCounter < 0)
					{
						MainForm.reconnectCounter = 0;
						//timer1.Enabled = true;
					}

					MainForm.dbDisconnectedFlag = true;
				}
				else
				   ShowErrorMessage(ex.Message);
				return;
			}
            
			DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
			DBStatusLabel.Text = "Connected";
			toolBar1.Enabled = true;

			ListViewItem selItem = listView.SelectedItems[0];
			int index = selItem.Index;
			selItem.Remove();
			if (index > 0)
			{
				listView.Items[index-1].Selected = true;
				listView.Select();
			}
			else if ((index == 0) && (listView.Items.Count > 0))
			{
				listView.Items[0].Selected = true;
				listView.Select();
			}
			else if (listView.Items.Count == 0)
			{
				ZoneIDEditBox.Text = "";
				ZoneLocTextBox.Text = "";
				ReaderIDTextBox.Text = "";
				FGenIDTextBox.Text = "";
				StatusTextBox.Text = "";
				TimeTextBox.Text = "";
			}

			
		}

		private void RefreshButton_Click(object sender, System.EventArgs e)
		{
			listView.Items.Clear();
			ZoneIDEditBox.Text = "";
			ZoneLocTextBox.Text = "";
			ReaderIDTextBox.Text = "";
			FGenIDTextBox.Text = "";
			SaveButton.Enabled = false;

			ZoneIDEditBox.ReadOnly = true;
			ZoneLocTextBox.ReadOnly = true;
			ReaderIDTextBox.ReadOnly = true;
			FGenIDTextBox.ReadOnly = true;
			RSSICheckBox.Enabled = false;

			//string connectionString = string.Format("server={0};port={1};username={2};password={3};database={4};pooling=false", "localhost", "3306", "root", "AWMYSQL", "cartracker");
			
			string mySelectQuery = "SELECT ID, Location, ID, LineID, ReaderID, FieldGenID, Status, Time, Type FROM zones";
			OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection); 
			//m_connection1.Open();
			
			OdbcDataReader myReader = null;
			
			/*try
			{
				myReader = myCommand.ExecuteReader();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				if (myReader != null)
				{
					if (!myReader.IsClosed)
						myReader.Close();
				}
				return;
			}*/

			//lock (MainForm.m_connection)
			{
				try
				{
					myReader = myCommand.ExecuteReader();
					//MainForm.reconnectCounter = -1;
					//timer1.Enabled = false;
				}
				catch (Exception ex)
				{
					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{  
						//error code 2013
						DBStatusLabel.ForeColor = System.Drawing.Color.Red;
						DBStatusLabel.Text = "Disconnected";
						toolBar1.Enabled = false;

						if (MainForm.reconnectCounter < 0)
						{
							MainForm.reconnectCounter = 0;
							//timer1.Enabled = true;
						}

						MainForm.dbDisconnectedFlag = true;

						/*Thread.Sleep(500);
						if (!mForm.ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
						{
							MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							if (myReader != null)
							{
								if (!myReader.IsClosed)
									myReader.Close();
							}
							return;
						}

						DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
						DBStatusLabel.Text = "Connected";
						toolBar1.Enabled = true;*/
					                                  
					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				}//catch .. try
			}//lock
			
			DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
			DBStatusLabel.Text = "Connected";
			toolBar1.Enabled = true;

			bool firstRec = true;
			int myRec = 0;
			while (myReader.Read())
			{
				myRec += 1;
				if (myRec > MAX_DEMO_RECORDS)
					break;
				ListViewItem listItem = new ListViewItem(myReader.GetString(0));  //zone ID
				listItem.SubItems.Add(myReader.GetString(1));  //location
				listItem.SubItems.Add(myReader.GetString(2));  //reader id
				listItem.SubItems.Add(myReader.GetString(3));  //field gen id
				listItem.SubItems.Add(myReader.GetString(4));  //status
				listItem.SubItems.Add(myReader.GetString(5));  //time stamp
				listView.Items.Add(listItem);
				if (firstRec)
				{
					listItem.Selected = true;
					firstRec = false;
					ZoneIDEditBox.Text = myReader.GetString(0);
					ZoneLocTextBox.Text = myReader.GetString(1);
					ReaderIDTextBox.Text = myReader.GetString(2);
					FGenIDTextBox.Text = myReader.GetString(3);
					StatusTextBox.Text = myReader.GetString(4);
					TimeTextBox.Text = myReader.GetString(5);
				}
			}
			
		}

		private void listView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.listView.SelectedIndices.Count <= 0)
				return;

			ListViewItem selItem = listView.SelectedItems[0];
			lastSelectedItem = selItem;

			ZoneIDEditBox.Text = selItem.SubItems[0].Text;
			ZoneLocTextBox.Text = selItem.SubItems[1].Text;
			//LineIDTextBox.Text = selItem.SubItems[2].Text;
			ReaderIDTextBox.Text = selItem.SubItems[2].Text;
			FGenIDTextBox.Text = selItem.SubItems[3].Text;
			if (selItem.SubItems[4].Text == "True")
			{
				RSSICheckBox.Checked = true;
				if (editMode)
					RSSITextBox.ReadOnly = false;
				else
					RSSITextBox.ReadOnly = true;
			}
			else
			{
				RSSICheckBox.Checked = false;
				RSSITextBox.ReadOnly = true;
			}
			RSSITextBox.Text = selItem.SubItems[5].Text;
			StatusTextBox.Text = selItem.SubItems[6].Text;
            if (selItem.SubItems[7].Text == "RS232")
            {
                RS232CheckBox.Checked = true;
            }
            else
            {
                RS232CheckBox.Checked = false;
            }
			TimeTextBox.Text = selItem.SubItems[8].Text;
			//if (selItem.SubItems[7].Text.Equals("1"))
				//LineTagCheckBox.Checked = true;
			//else
			   //LineTagCheckBox.Checked = false;
		}

		private void listView_Click(object sender, System.EventArgs e)
		{
            //clicked = true;
			if(this.listView.SelectedIndices.Count <= 0)
				return;

			ListViewItem selItem = listView.SelectedItems[0];
			lastSelectedItem = selItem;
            listIndex = lastSelectedItem.Index;

			ZoneIDEditBox.Text = selItem.SubItems[0].Text;
			ZoneLocTextBox.Text = selItem.SubItems[1].Text;
			//LineIDTextBox.Text = selItem.SubItems[2].Text;
			ReaderIDTextBox.Text = selItem.SubItems[2].Text;
			FGenIDTextBox.Text = selItem.SubItems[3].Text;
			if (selItem.SubItems[4].Text == "True")
			{
				RSSICheckBox.Checked = true;
				if (editMode)
					RSSITextBox.ReadOnly = false;
				else
					RSSITextBox.ReadOnly = true;
			}
			else
			{
				RSSICheckBox.Checked = false;
				RSSITextBox.ReadOnly = true;
			}
			RSSITextBox.Text = selItem.SubItems[5].Text;
			StatusTextBox.Text = selItem.SubItems[6].Text;
            if (selItem.SubItems[7].Text == "RS232")
            {
                RS232CheckBox.Checked = true;                
            }
            else
            {
                RS232CheckBox.Checked = false;               
            }
			TimeTextBox.Text = selItem.SubItems[8].Text;

			/*ListViewItem selItem = listView.SelectedItems[0];

			ZoneIDEditBox.Text = selItem.SubItems[0].Text;
			ZoneLocTextBox.Text = selItem.SubItems[1].Text;
			//LineIDTextBox.Text = selItem.SubItems[2].Text;
			ReaderIDTextBox.Text = selItem.SubItems[2].Text;
			FGenIDTextBox.Text = selItem.SubItems[3].Text;
			StatusTextBox.Text = selItem.SubItems[4].Text;
			TimeTextBox.Text = selItem.SubItems[5].Text;*/

			//if (selItem.SubItems[6].Text.Equals("1"))
				//LineTagCheckBox.Checked = true;
			//else
			   //LineTagCheckBox.Checked = false;
		}

		private void FirstButton_Click(object sender, System.EventArgs e)
		{
			if (listView.Items.Count > 0)
			{
				listView.Items[0].Selected = true;
				listView.Select();
			}
		}

		private void LastButton_Click(object sender, System.EventArgs e)
		{
			if (listView.Items.Count > 0)
			{
				listView.Items[listView.Items.Count-1].Selected = true;
				listView.Select();
			}
		}

		private void NextButton_Click(object sender, System.EventArgs e)
		{
			ListViewItem selItem = listView.SelectedItems[0];
			int index = selItem.Index;
			if ((index >= 0)&& (index < listView.Items.Count-1))
			{
				listView.Items[index+1].Selected = true;
				listView.Select();
			}
		}

		private void PreButton_Click(object sender, System.EventArgs e)
		{
			ListViewItem selItem = listView.SelectedItems[0];
			int index = selItem.Index;
			if (index >= 1)
			{
				listView.Items[index-1].Selected = true;
				listView.Select();
			}
		}

		private void CancelButton_Click(object sender, System.EventArgs e)
		{
		    ZoneIDEditBox.ReadOnly = true;
			ZoneLocTextBox.ReadOnly = true;
			ReaderIDTextBox.ReadOnly = true;
			FGenIDTextBox.ReadOnly = true;
			RSSICheckBox.Enabled = false;

			SaveButton.Enabled = false;

			if(this.listView.SelectedIndices.Count <= 0)
			{
				ZoneIDEditBox.Text = "";
				ZoneLocTextBox.Text = "";
				ReaderIDTextBox.Text = "";
				FGenIDTextBox.Text = "";
				StatusTextBox.Text = "";
				TimeTextBox.Text = "";
				return;
			}

			ListViewItem selItem = listView.SelectedItems[0];
			ZoneIDEditBox.Text = selItem.SubItems[0].Text;
			ZoneLocTextBox.Text = selItem.SubItems[1].Text;
			ReaderIDTextBox.Text = selItem.SubItems[2].Text;
			FGenIDTextBox.Text = selItem.SubItems[3].Text;
			StatusTextBox.Text = selItem.SubItems[4].Text;
			TimeTextBox.Text = selItem.SubItems[5].Text;
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (MainForm.m_connection == null)
			   return;

			//lock (MainForm.m_connection)
			{
			if (e.Button.Text == "New")
			{
			    
				ZoneIDEditBox.Focus();
				SaveButton.Enabled = true;
				//ZoneIDTextBox.Text = "";
				//ZoneIDComboBox.Enabled = true;
				ZoneIDEditBox.Text = "";
				//LineTagCheckBox.Enabled = true;
				ZoneLocTextBox.Text = "";
				ReaderIDTextBox.Text = "";
				FGenIDTextBox.Text = "";
				StatusTextBox.Text = "";
				TimeTextBox.Text = "";
				OwnerIDTextBox.Text = "";
				RSSITextBox.Text = "";
				//LineIDTextBox.Text = "";
				//LineTagCheckBox.Checked = false;

				ZoneIDEditBox.ReadOnly = false;
				ZoneLocTextBox.ReadOnly = false;
				ReaderIDTextBox.ReadOnly = false;
				FGenIDTextBox.ReadOnly = false;
				RSSICheckBox.Enabled = true;
				RSSICheckBox.Checked = false;
                RS232CheckBox.Checked = false;
                RS232CheckBox.Enabled = true;
				editMode = false;
			}
			else if (e.Button.Text == "Edit")
			{
				editMode = true;
				ZoneIDEditBox.Focus();
				zoneID = ZoneIDEditBox.Text;
				SaveButton.Enabled = true;
				//LineTagCheckBox.Enabled = true;
				//LineTagCheckBox.Checked = false;
				ZoneIDEditBox.ReadOnly = false;
				ZoneLocTextBox.ReadOnly = false;
				ReaderIDTextBox.ReadOnly = false;
				FGenIDTextBox.ReadOnly = false;
				RSSICheckBox.Enabled = true;
                RS232CheckBox.Enabled = true;
				if (RSSICheckBox.Checked)
					RSSITextBox.ReadOnly = false;
				else 
                    RSSITextBox.ReadOnly = true;
			}
			else if (e.Button.Text == "Cancel")
			{
				editMode = false;
				ZoneIDEditBox.ReadOnly = true;
				//ZoneIDComboBox.Enabled = false;
				ZoneLocTextBox.ReadOnly = true;
				ReaderIDTextBox.ReadOnly = true;
				FGenIDTextBox.ReadOnly = true;
				OwnerIDComboBox.Enabled = false;
				RSSICheckBox.Enabled = false;
				RSSITextBox.ReadOnly = true;
                RS232CheckBox.Enabled = false;

				SaveButton.Enabled = false;
				//LineTagCheckBox.Enabled = false;

				if (this.listView.SelectedIndices.Count <= 0)
				{
					ZoneIDEditBox.Text = "";
					//ZoneIDComboBox.Text = "";
					ZoneLocTextBox.Text = "";
					ReaderIDTextBox.Text = "";
					FGenIDTextBox.Text = "";
					StatusTextBox.Text = "";
					TimeTextBox.Text = "";
					RSSICheckBox.Enabled = false;
					RSSICheckBox.Checked = false;
                    RS232CheckBox.Checked = false;
                    RS232CheckBox.Enabled = false;
                    RSSITextBox.Text = "";
					return;
				}

				ListViewItem selItem = listView.SelectedItems[0];
				ZoneIDEditBox.Text = selItem.SubItems[0].Text;
				ZoneLocTextBox.Text = selItem.SubItems[1].Text;
				//LineIDTextBox.Text = selItem.SubItems[2].Text;
				ReaderIDTextBox.Text = selItem.SubItems[2].Text;
				FGenIDTextBox.Text = selItem.SubItems[3].Text;

				if (selItem.SubItems[4].Text == "True")
				{
					RSSICheckBox.Checked = true;
					//RSSITextBox.ReadOnly = false;
					RSSITextBox.Text = selItem.SubItems[5].Text;
				}
				else
				{
					RSSICheckBox.Checked = false;
					//RSSITextBox.ReadOnly = true;
					RSSITextBox.Text = "";
				}

				StatusTextBox.Text = selItem.SubItems[6].Text;

                if (selItem.SubItems[7].Text == "RS232")
                {
                    RS232CheckBox.Checked = true;                    
                }
                else
                {
                    RS232CheckBox.Checked = false;                    
                }

				TimeTextBox.Text = selItem.SubItems[8].Text;

                

				//if (LineIDTextBox.Text == "")
				//LineTagCheckBox.Checked = false;
				//else
				//LineTagCheckBox.Checked = true;

			}
			else if (e.Button.Text == "Save")
			{
				if (ZoneIDEditBox.Text.Equals(""))
					//if (ZoneIDComboBox.Text.Equals(""))
				{
					ShowErrorMessage("Need Door ID");
					ZoneIDEditBox.Focus();
					newRec = false;
					//ZoneIDComboBox.Focus();
					return;
				}

				ushort thresh = 0;
				if (RSSICheckBox.Checked)
				{
					if (RSSITextBox.Text.Length == 0) 
					{
						ShowErrorMessage("Need value for the Threshold.");
						TimeTextBox.Focus();
						newRec = false;
						return;
					}

					if ((Convert.ToInt16(RSSITextBox.Text) < 0) || (Convert.ToInt16(RSSITextBox.Text) > 255))
					{
						ShowErrorMessage("Threshold outside the limit.");
						TimeTextBox.Focus();
						newRec = false;
						return;
					}

					thresh = Convert.ToUInt16(RSSITextBox.Text); 
				}
				else
					thresh = 0;

				DateTime  time = DateTime.Now;
				string timeStr = time.ToString();
				timeStr = timeStr.Substring(0, timeStr.Length - 3); 

				ZoneIDEditBox.ReadOnly = true;
				//ZoneIDComboBox.Enabled = false;
				ZoneLocTextBox.ReadOnly = true;
				ReaderIDTextBox.ReadOnly = true;
				FGenIDTextBox.ReadOnly = true;
				RSSICheckBox.Enabled = false;
				RSSITextBox.ReadOnly = true;
                RS232CheckBox.Enabled = false; 

				SaveButton.Enabled = false;

                //awiHelper.SaveZone(!editMode, zoneID, LineTagCheckBox.Checked, ZoneIDEditBox.Text, ZoneLocTextBox.Text, ReaderIDTextBox.Text, FGenIDTextBox.Text, "Offline", LineIDTextBox.Text);
                if (!awiHelper.SaveZone(!editMode, zoneID, ZoneIDEditBox.Text, ZoneLocTextBox.Text, ReaderIDTextBox.Text, RSSICheckBox.Checked, thresh, FGenIDTextBox.Text, "Offline", RS232CheckBox.Checked))
				{
					MessageBox.Show(this, "Save record failed.");
					return;
				}
				
				/*StringBuilder sql = new StringBuilder();		
				sql.Append("INSERT INTO zones (ID, Location, ReaderID, FieldGenID, Status, Time, ZoneType, LineID) Values ");         
				if (LineTagCheckBox.Checked)
				   sql.AppendFormat("( 'L{0}', ", ZoneIDComboBox.Text); //gIDTextBox.Text, "AST", OwnerNameTextBox.Text, VINTextBox.Text, DescriptTextBox.Text, null);
				else
				   sql.AppendFormat("( '{0}', ", ZoneIDComboBox.Text);
				sql.AppendFormat(" '{0}', ", ZoneLocTextBox.Text);
				sql.AppendFormat(" '{0}', ", ReaderIDTextBox.Text);
				sql.AppendFormat(" '{0}', ", FGenIDTextBox.Text);
				sql.AppendFormat(" '{0}', ", "Offline");
				sql.AppendFormat(" '{0}', ", "");
				if (LineTagCheckBox.Checked)
					sql.AppendFormat(" '{0}', ", 1);
				else 
					sql.AppendFormat(" '{0}', ", 0);
				
				//sql.AppendFormat(" '{0}', ", OwnerIDComboBox.Text);
				sql.AppendFormat(" '{0}' )", LineIDTextBox.Text);
				
				OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
				
				try
				{
					myCommand.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					ShowErrorMessage(ex.Message);
					//m_connection1.Close();
					return;
				}*/

				//if (LineTagCheckBox.Checked)
				//{
				//ZoneIDEditBox.Text = "L" + ZoneIDEditBox.Text;
				//ZoneIDEditBox.Items.Add(ZoneIDComboBox.Text);
				//}

				

				ListViewItem listItem = new ListViewItem(ZoneIDEditBox.Text);
				listItem.SubItems.Add(ZoneLocTextBox.Text);
				//listItem.SubItems.Add(LineIDTextBox.Text);
				listItem.SubItems.Add(ReaderIDTextBox.Text);

				listItem.SubItems.Add(FGenIDTextBox.Text);
				if (RSSICheckBox.Checked)
				{
					listItem.SubItems.Add("True");
					listItem.SubItems.Add(RSSITextBox.Text);

				}
				else
				{
					listItem.SubItems.Add("False");
					listItem.SubItems.Add("");
				}
				listItem.SubItems.Add("Offline");

                if (RS232CheckBox.Checked)
                {
                    listItem.SubItems.Add("RS232");
                }
                else
                {
                    listItem.SubItems.Add("Network");
                }

				listItem.SubItems.Add(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"));
				//if (LineTagCheckBox.Checked)
				//listItem.SubItems.Add("1");
				//else 
				//listItem.SubItems.Add("0");

				if (editMode)
				{
                    if (lastSelectedItem.Index >= 0)
                    {
                        int itemIndx = lastSelectedItem.Index;
                        listView.Items.RemoveAt(lastSelectedItem.Index);                       
                        listView.Items.Insert(itemIndx, listItem);

                        //ListViewItem selItem = listView.SelectedItems[0];
                        //int index = selItem.Index;
                        //listView.Items.RemoveAt(selItem.Index);
                        //listView.Items.Insert(index, listItem);
                    }
				}
				else
					listView.Items.Add(listItem);
				listItem.EnsureVisible();
				listItem.Selected = true;
				RSSITextBox.ReadOnly = true;
				
				editMode = false;
			}
			else if (e.Button.Text == "Delete")
			{
                if (listView.SelectedItems.Count != 0)
                {
                    return;
                }

				if (ZoneIDEditBox.Text.Equals(""))
				{
					ShowErrorMessage("No Tag ID");
					return;
				}

                if (MessageBox.Show(this, "Delete the record?", "Smart Tracker", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					return;

				//ZoneIDTextBox.ReadOnly = true;
				ZoneIDEditBox.ReadOnly = true;
				ZoneLocTextBox.ReadOnly = true;
				ReaderIDTextBox.ReadOnly = true;
				FGenIDTextBox.ReadOnly = true;
				OwnerIDComboBox.Enabled = false;
				LineIDTextBox.ReadOnly = true;
				RSSICheckBox.Enabled = false;
                RS232CheckBox.Enabled = false;
				
				SaveButton.Enabled = false;
				LineTagCheckBox.Enabled = false;

				StringBuilder sql = new StringBuilder();
				sql.Append("DELETE FROM zones WHERE ID = ");         
				sql.AppendFormat("'{0}'", ZoneIDEditBox.Text);
				OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
				
				try
				{
					myCommand.ExecuteNonQuery();
					//MainForm.reconnectCounter = -1;
					//timer1.Enabled = false;
				}
				catch (Exception ex)
				{
					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{  
						//error code 2013
						if (MainForm.reconnectCounter < 0)
						{
							MainForm.reconnectCounter = 0;
							//timer1.Enabled = true;
						}

						MainForm.dbDisconnectedFlag = true;
					}
					else
						ShowErrorMessage(ex.Message);
					return;
				}
				
				DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
				DBStatusLabel.Text = "Connected";
				toolBar1.Enabled = true;

                // EDC march 2010
                if (listView.SelectedItems.Count > 0) {
                    ListViewItem selItem = listView.SelectedItems[0];
                    if (selItem != null) {
                        int index = selItem.Index;
                        selItem.Remove();
                        if (index > 0) {
                            listView.Items[index - 1].Selected = true;
                            listView.Select();
                        } else {
                            if ((index == 0) && (listView.Items.Count > 0)) {
                                listView.Items[0].Selected = true;
                                listView.Select();
                            } else {
                                if (listView.Items.Count == 0) {
                                    //ZoneIDTextBox.Text = "";
                                    ZoneIDEditBox.Text = "";
                                    ZoneLocTextBox.Text = "";
                                    //OwnerIDComboBox.Text = "";
                                    //LineIDTextBox.Text = "";
                                    ReaderIDTextBox.Text = "";
                                    FGenIDTextBox.Text = "";
                                    StatusTextBox.Text = "";
                                    TimeTextBox.Text = "";
                                    RSSICheckBox.Enabled = false;
                                    RSSICheckBox.Checked = false;
                                    RS232CheckBox.Enabled = false;
                                    RS232CheckBox.Checked = false;
                                    RSSITextBox.Text = "";

                                    //LineTagCheckBox.Checked = false;
                                    //LineTagCheckBox.Enabled = false;
                                }
                            }
                        }
                    }
                }
				editMode = false;

			}
			else if (e.Button.Text == "Refresh")
			{
				editMode = false;
				listView.Items.Clear();
				ZoneIDEditBox.Text = "";
				ZoneLocTextBox.Text = "";
				ReaderIDTextBox.Text = "";
				FGenIDTextBox.Text = "";
				SaveButton.Enabled = false;
                

				//ZoneIDTextBox.ReadOnly = true;
				ZoneIDEditBox.ReadOnly = true;
				ZoneLocTextBox.ReadOnly = true;
				ReaderIDTextBox.ReadOnly = true;
				FGenIDTextBox.ReadOnly = true;
				OwnerIDComboBox.Enabled = false;
				RSSICheckBox.Enabled = false;
				RSSITextBox.Text = "";
                RS232CheckBox.Enabled = false;
				//LineIDTextBox.ReadOnly = true;

				//LineTagCheckBox.Enabled = false;

				//string connectionString = string.Format("server={0};port={1};username={2};password={3};database={4};pooling=false", "localhost", "3306", "root", "AWMYSQL", "cartracker");
				
				//lock (MainForm.m_connection)
				//{

                string mySelectQuery = "SELECT ID, Location, ReaderID, FieldGenID, RSSI, Threshold, Status, ReaderType, Time FROM zones";
					OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection); 
				
				
					//OdbcDataReader myReader = myCommand.ExecuteReader();
					OdbcDataReader myReader = null;
				
					/*try
					{
						myReader = myCommand.ExecuteReader();
					}
					catch (Exception ex)
					{
						ShowErrorMessage(ex.Message);
						if (myReader != null)
						{
							if (!myReader.IsClosed)
								myReader.Close();
						}
						return;
					}*/

					//lock (MainForm.m_connection)
					//{
					try
					{
						myReader = myCommand.ExecuteReader();
						//MainForm.reconnectCounter = -1;
						//timer1.Enabled = false;
					}
					catch (Exception ex)
					{
						int ret = 0, ret1 = 0, ret2 = 0;
						if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
							((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
							((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
						{  
							//error code 2013
							DBStatusLabel.ForeColor = System.Drawing.Color.Red;
							DBStatusLabel.Text = "Disconnected";
							toolBar1.Enabled = false;

							if (MainForm.reconnectCounter < 0)
							{
								MainForm.reconnectCounter = 0;
								//timer1.Enabled = true;
							}

							MainForm.dbDisconnectedFlag = true;
							
							/*Thread.Sleep(500);

							if (!mForm.ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
							{
								MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								if (myReader != null)
								{
									if (!myReader.IsClosed)
										myReader.Close();
								}
								return;
							}

							DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
							DBStatusLabel.Text = "Connected";
							toolBar1.Enabled = true;*/
					                                  
						}
						if (myReader != null)
						{
							if (!myReader.IsClosed)
								myReader.Close();
						}
						return;
					}//catch .. try
					//}//lock
			
				    DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
				    DBStatusLabel.Text = "Connected";
				    toolBar1.Enabled = true;

					bool firstRec = true;
					int myRec = 0;
					while (myReader.Read())
					{
						myRec += 1;
						ListViewItem listItem = new ListViewItem(myReader.GetString(0));  //zone ID
						try
						{
							listItem.SubItems.Add(myReader.GetString(1));  //location
						}
						catch
						{
                            listItem.SubItems.Add("");
						}
						//listItem.SubItems.Add(myReader.GetString(2));  //ownerID
						//listItem.SubItems.Add(myReader.GetString(2));  //LineID

						try
						{
							listItem.SubItems.Add(myReader.GetString(2));  //reader id
						}
						catch
						{
                            listItem.SubItems.Add("");
						}

						try
						{
							listItem.SubItems.Add(myReader.GetString(3));  //field gen id
						}
						catch
						{
                            listItem.SubItems.Add("");
						}

						try
						{
							if (myReader.GetBoolean(4))
								listItem.SubItems.Add("True");  //RSSI
							else
								listItem.SubItems.Add("False");  
						}
						catch
						{
							listItem.SubItems.Add("");
						}

						try
						{
							listItem.SubItems.Add(myReader.GetString(5));  //Threshold
						}
						catch
						{
							listItem.SubItems.Add("");
						}

						try
						{
							listItem.SubItems.Add(myReader.GetString(6));  //status
						}
						catch
						{
                            listItem.SubItems.Add("");
						}

                        try
                        {
                            if (myReader.GetBoolean(7))
                                listItem.SubItems.Add("RS232");  //RS232
                            else
                                listItem.SubItems.Add("Network");
                        }
                        catch
                        {
                            listItem.SubItems.Add("");
                        }

						try
						{
							listItem.SubItems.Add(myReader.GetDateTime(8).ToString("MM-dd-yyyy  HH:mm:ss"));  //time stamp
						}
						catch
						{
                            listItem.SubItems.Add("");
						}

                        

						listView.Items.Add(listItem);
						if (firstRec)
						{
							listItem.Selected = true;
							firstRec = false;
							//ZoneIDTextBox.Text = myReader.GetString(0);
							try
							{
								ZoneIDEditBox.Text = myReader.GetString(0);
							}
							catch
							{
                                ZoneIDEditBox.Text = "";
							}

							try
							{
								ZoneLocTextBox.Text = myReader.GetString(1);
							}
							catch
							{
                                ZoneLocTextBox.Text = "";
							}
							//OwnerIDComboBox.Text = myReader.GetString(2);
							//LineIDTextBox.Text = myReader.GetString(2);

							try
							{
								ReaderIDTextBox.Text = myReader.GetString(2);
							}
							catch
							{
                                ReaderIDTextBox.Text = "";
							}

							try
							{
								FGenIDTextBox.Text = myReader.GetString(3);
							}
							catch
							{
                                FGenIDTextBox.Text = "";
							}

							try
							{
								if (myReader.GetBoolean(4))
									RSSICheckBox.Checked = true;
								else
									RSSICheckBox.Checked = false; 
							}
							catch
							{
								RSSICheckBox.Checked = false; 
							}

							try
							{
								RSSITextBox.Text = myReader.GetString(5);  //Threshold
							}
							catch
							{
								RSSITextBox.Text = "";
							}

							try
							{
								StatusTextBox.Text = myReader.GetString(6);
							}
							catch
							{
                                StatusTextBox.Text = "";
							}

                            try
                            {
                                if (myReader.GetBoolean(7))
                                    listItem.SubItems.Add("RS232");  //RS232
                                else
                                    listItem.SubItems.Add("Network");
                            }
                            catch
                            {
                                listItem.SubItems.Add("");
                            }

							try
							{
								TimeTextBox.Text = myReader.GetDateTime(8).ToString("MM-dd-yyyy  HH:mm:ss"); //myReader.GetString(5);
							}
							catch
							{
                                TimeTextBox.Text = "";
							}

							//if (myReader.GetString(7) == "1")
							//LineTagCheckBox.Checked = true;
							//else 
							//LineTagCheckBox.Checked = false;
						}
					}//while

					myReader.Close();
				}
			}//lock m_connection
		}

		private void LineTagRadioButton_Click(object sender, System.EventArgs e)
		{
			OwnerIDComboBox.Enabled = false;
			LineIDTextBox.ReadOnly = false;
			FGenIDTextBox.ReadOnly = true;
		}

		private void ReaderRadioButton_Click(object sender, System.EventArgs e)
		{
			OwnerIDComboBox.Enabled = false;
			LineIDTextBox.ReadOnly = true;
			FGenIDTextBox.ReadOnly = false;
		}

		private void CarTagRadioButton_Click(object sender, System.EventArgs e)
		{
			OwnerIDComboBox.Enabled = false;
			LineIDTextBox.ReadOnly = true;
			FGenIDTextBox.ReadOnly = false;
		}

		private void LineTagCheckBox_Click(object sender, System.EventArgs e)
		{
			if (LineTagCheckBox.Checked)
			{
				FGenIDTextBox.ReadOnly = true;
				LineIDTextBox.ReadOnly = false;
			    FGenIDTextBox.Text = "0";
			}
			else
			{
			    FGenIDTextBox.ReadOnly = false;
				LineIDTextBox.ReadOnly = true;
			    FGenIDTextBox.Text = "";
			}
		}

		private void listView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//int p = 0;
            //clicked = true;
			ListViewItem item = listView.GetItemAt(e.X, e.Y);

			if (item == null && listView.Items.Count > 1)
			{
				lastSelectedItem.Selected = true;
                listIndex = lastSelectedItem.Index;
			}
            else
                lastSelectedItem.Selected = false;
                listIndex = -1;
		}

		private void listView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			m_sortReverse = (e.Column == m_sortColumn) ? !m_sortReverse : false;
			m_sortColumn = e.Column;
			listView.ListViewItemSorter = this;
			listView.Sort();
			listView.ListViewItemSorter = null;
		}
		
		public int Compare(object x, object y)
		{
			ListViewItem item1 = x as ListViewItem;
			ListViewItem item2 = y as ListViewItem;
			int rc = 0;

			rc = SortTags(m_sortColumn, item1, item2);

			return m_sortReverse ? -rc : rc;

		}

		private int SortTags(int column, ListViewItem item1, ListViewItem item2)
		{
			int rc = 0;
			string s1;
			string s2;

			switch (column)
			{
				case 0: //zoneid
                case 2: //rdrid
				case 3: //fgenid
				
					int i1 = Convert.ToInt32(item1.SubItems[column].Text);
					int i2 = Convert.ToInt32(item2.SubItems[column].Text);
					
					if (i1 < i2)
						rc = -1;
					else if (i1 == i2)
						rc = 0;
					else
						rc = 1;
					break;

				default:
					s1 = item1.SubItems[column].Text;
					s2 = item2.SubItems[column].Text;
					rc = string.Compare(s1, s2);
					break;
			}	

			return rc;
		}

		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			/*if (MainForm.reconnectCounter == 3)
			{
				timer1.Enabled = false;
				MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (MainForm.reconnectCounter > 3)
			{
				timer1.Enabled = false;	
			}
			else
			{
				MainForm.reconnectCounter += 1;
				lock (MainForm.m_connection)
				{
					if(mForm.ReconnectToDBServer())
					{
						timer1.Enabled = false;
						MainForm.reconnectCounter = -1;
						DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
						DBStatusLabel.Text = "Connected";
						toolBar1.Enabled = true;
					}
				}
			}*/
		}

		private void RSSICheckBox_Click(object sender, System.EventArgs e)
		{
			if (rssiChecked)
			{
				rssiChecked = false;

				if (RSSICheckBox.Checked)
					RSSITextBox.ReadOnly = false;
				else
					RSSITextBox.ReadOnly = true;
			}
		}

		private void EnableRSSILabel_Click(object sender, System.EventArgs e)
		{
		
		}

		private void RSSICheckBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			rssiChecked = true;
		}

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            /*if ((e.State & ListViewItemStates.Selected) != 0)
            {
                ListViewItem item = e.Item;
                if (item.SubItems[6].Text == "Online")
                    
                // Draw the background and focus rectangle for a selected item.

                e.Graphics.FillRectangle(Brushes.LightSteelBlue, e.Bounds);

                else

                e.Graphics.FillRectangle(Brushes.LightYellow, e.Bounds);

                e.DrawFocusRectangle();

            }*/

            /*else
            {

                // Draw the background for an unselected item.

                using (LinearGradientBrush brush =

                new LinearGradientBrush(e.Bounds, Color.Orange,

                Color.Maroon, LinearGradientMode.Horizontal))
                {

                    e.Graphics.FillRectangle(brush, e.Bounds);

                }

            }*/

            // Draw the item text for views other than the Details view.

            if (listView.View != View.Details)
            {

                e.DrawText();

            }


            /////////////////////////////////////////////////////////////////////
            if ((e.State & ListViewItemStates.Selected) == 0 ) //&& lastSelectedItem.Selected)
            {
                // Draw the background and focus rectangle for a selected item.
                ////e.Graphics.FillRectangle(Brushes.Navy, e.Bounds);
                ////e.DrawFocusRectangle();
                //e.Graphics.DrawString(e.Item.Text, listView.Font, Brushes.Navy, e.Bounds, null);
            }
            else
            {
                

                // Draw the background and focus rectangle for a selected item.
                ////e.Graphics.FillRectangle(Brushes.Navy, e.Bounds);
                ////e.DrawFocusRectangle();
                //e.Graphics.DrawString(e.Item.Text, listView.Font, Brushes.White, e.Bounds, null);
            }

            /*if ((e.State & ListViewItemStates.Selected) == 0)
            //else
            {
                // Draw the background for an unselected item.
                using (LinearGradientBrush brush =
                    new LinearGradientBrush(e.Bounds, Color.LightSeaGreen,
                    Color.LightGreen, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }*/
           
            // Draw the item text for views other than the Details view.
            if (listView.View != View.Details)
            {
                e.DrawText();
            }
        }

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (StringFormat sf = new StringFormat())
            {
                // Store the column text alignment, letting it default
                // to Left if it has not been set to Center or Right.

                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                    break;

                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                    break;
                }


                //Draw the standard Header Text
                e.DrawBackground();
                

                // Draw the header text. Header Font = "Helvetic"
                // Draw the header text. FontStyle = FontStyle.Regular
                using (Font headerFont = new Font("Helvetic", 8, FontStyle.Regular))
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Purple, e.Bounds, sf);                    
                }
            }

            return;
        }

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            //TextFormatFlags flags = TextFormatFlags.Left;

            using (StringFormat sf = new StringFormat())
            {
                // Store the column text alignment, letting it default
                // to Left if it has not been set to Center or Right.

                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        //flags = TextFormatFlags.HorizontalCenter;
                        break;

                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        //flags = TextFormatFlags.Right;
                        break;

                }


                /*if (((e.ItemState & ListViewItemStates.Selected) != 0) && lastSelectedItem.Selected && clicked) // && (e.ItemIndex == listIndex))
                {
                    //e.Graphics.FillRectangle(Brushes.Navy, e.Bounds);
                    //e.DrawFocusRectangle();

                    // Draw the background and focus rectangle for a selected item.
                   
                    e.DrawFocusRectangle(e.Item.Bounds);
                    e.Graphics.FillRectangle(Brushes.Navy, e.Item.Bounds);
                    
                    e.Graphics.DrawString(e.Item.Text, listView.Font, Brushes.White, e.Bounds, null);
                    clicked = false;
                }*/

                /*if (e.ColumnIndex == 0)
                {
                    if (((e.ItemState & ListViewItemStates.Selected) == 0) && clicked)                  
                        e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Yellow, e.Bounds, sf);
                    else
                        e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Navy, e.Bounds, sf);
                }*/                


                if ((e.ColumnIndex == 6) && (e.SubItem.Text == "Online"))
                {
                    // Unless the item is selected, draw the standard 
                    // background to make it stand out from the gradient.
                    //if ((e.ItemState & ListViewItemStates.Selected) == 0)
                    //{
                    //e.DrawBackground();
                    //}
                    // Draw the subitem text in red to highlight it. 

                    e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Green, e.Bounds, sf);
                }
                else if (e.ColumnIndex == 6)
                {
                    e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Red, e.Bounds, sf);
                }
                else
                {
                    if ((e.ItemState & ListViewItemStates.Selected) != 0)
                       e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Navy, e.Bounds, sf);
                }

                return;

                // Draw normal text for a subitem with a nonnegative 
                // or nonnumerical value.
                //e.DrawText(flags);

            }
        }

        private void listView_MouseMove(object sender, MouseEventArgs e)
        {
            /*ListViewItem item = listView.GetItemAt(e.X, e.Y);

            if (item != null && item.Tag == null)
            {

                listView.Invalidate(item.Bounds);

                item.Tag = "tagged";

            }*/


        }

        private void listView_MouseLeave(object sender, EventArgs e)
        {
            //clicked = false;
        }
                                       
	}//form

}//namespace
