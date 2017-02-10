using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Threading;
using System.IO;
using AW_API_NET;
using System.Text;
using System.Diagnostics;
using AWIComponentLib.Communication;
using AWIComponentLib.Events;
using AWIComponentLib.Database;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for AWIHistoryControl.
	/// </summary>
	//public delegate void ShowImage(string name, string id, string timestamp, Image image);

	public class AWIHistoryControl : System.Windows.Forms.UserControl, IComparer	
	{
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.IContainer components;
		//public OdbcConnection m_connection = null;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage TagTabPage;
		private System.Windows.Forms.TabPage TagHistoryTabPage;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListView TagHistoryListView;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label NameLabel;
		private System.Windows.Forms.PictureBox HistoryPictureBox;
		private System.Windows.Forms.DateTimePicker FromDateTimePicker;
		private System.Windows.Forms.DateTimePicker ToDateTimePicker;
		private System.Windows.Forms.Button TagHistRefreshButton;
		//public OdbcDbClass odbcDB = new OdbcDbClass();
		private System.Windows.Forms.TabPage ZonePage;
		private System.Windows.Forms.ListView ZoneListView;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.DateTimePicker ZoneToDateTimePicker;
		private System.Windows.Forms.DateTimePicker ZoneFromDateTimePicker;
		private System.Windows.Forms.ListView ZoneHistoryListView;
		private string TagIDHistory;
		private System.Windows.Forms.Button ZoneRefreshButton;
		private System.Windows.Forms.Label LocationLabel;
		private System.Windows.Forms.DateTimePicker AlarmToDateTimePicker;
		private System.Windows.Forms.DateTimePicker AlarmFromDateTimePicker;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.Button AlarmRefreshButton;
		private System.Windows.Forms.ListView AlarmHistoryListView;
		private System.Windows.Forms.Label NumAlarmLabel;
		private System.Windows.Forms.ListView TagsListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private string zoneIDHistory;
		private int m_sortColumn = -1;
		private System.Windows.Forms.ColumnHeader columnHeader24;
		private System.Windows.Forms.Button TagRefreshbutton;
		private System.Windows.Forms.ColumnHeader columnHeader25;
		private System.Windows.Forms.ColumnHeader columnHeader27;
		private System.Windows.Forms.ColumnHeader columnHeader28;
		private System.Windows.Forms.Button button1;
		private bool m_sortReverse= false;
		private bool tagListDClicked = false;
		private bool zoneListDClicked = false;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private OdbcDbClass odbcDB = new OdbcDbClass();
		private System.Timers.Timer timer1;
		private System.Windows.Forms.Button ZonesRefreshButton;
		private System.Windows.Forms.MenuItem menuItem3;
		private bool lastflname = true;
		//private MainForm mForm = new MainForm(0);
		

		public AWIHistoryControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage ;
			HistoryPictureBox.SizeMode = PictureBoxSizeMode.StretchImage ;
			//FromDateTimePicker.Value = DateTime.Today;
			FromDateTimePicker.Checked = false;
			//OdbcDbClass.NotifyDBConnectionStatusHandler += new NotifyDBConnectionStatus(DBConnectionStatusHandler);
			// TODO: Add any initialization after the InitializeComponent call
			
			menuItem2.Checked = false;
			menuItem1.Checked = true;
			FromDateTimePicker.Value = DateTime.Today;
			ToDateTimePicker.Value = DateTime.Now;
			ZoneFromDateTimePicker.Value = DateTime.Today;
			ZoneToDateTimePicker.Value = DateTime.Now;
			AlarmFromDateTimePicker.Value = DateTime.Today;
			AlarmToDateTimePicker.Value = DateTime.Now;

			/*string s = "";
			s = "DRIVER={MySQL ODBC 3.51 Driver};";
			s += "SERVER=" + MainForm.server;
			s += ";";
			s += "DATABASE=" + MainForm.database;
			s += ";";
			s += "USER=" + MainForm.user;
			s += ";";
			s += "PASSWORD=;OPTION=3;"; 
				
			//if (!odbcDB.Connect("DRIVER={MySQL ODBC 3.51 Driver};SERVER=localhost;DATABASE=parkingtracker;USER=root;PASSWORD=;OPTION=3;"))	//MYSQL
			if (!odbcDB.Connect(s))	//MYSQL
			{						
				return;
			}*/

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AWIHistoryControl));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.TagTabPage = new System.Windows.Forms.TabPage();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.TagRefreshbutton = new System.Windows.Forms.Button();
			this.TagsListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader25 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
			this.TagHistoryTabPage = new System.Windows.Forms.TabPage();
			this.button1 = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.HistoryPictureBox = new System.Windows.Forms.PictureBox();
			this.NameLabel = new System.Windows.Forms.Label();
			this.TagHistRefreshButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.ToDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.FromDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.TagHistoryListView = new System.Windows.Forms.ListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.ZonePage = new System.Windows.Forms.TabPage();
			this.ZonesRefreshButton = new System.Windows.Forms.Button();
			this.ZoneListView = new System.Windows.Forms.ListView();
			this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.LocationLabel = new System.Windows.Forms.Label();
			this.ZoneHistoryListView = new System.Windows.Forms.ListView();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader27 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.ZoneRefreshButton = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.ZoneToDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.label7 = new System.Windows.Forms.Label();
			this.ZoneFromDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.NumAlarmLabel = new System.Windows.Forms.Label();
			this.AlarmHistoryListView = new System.Windows.Forms.ListView();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader28 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader24 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.AlarmRefreshButton = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.AlarmToDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.label9 = new System.Windows.Forms.Label();
			this.AlarmFromDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.timer1 = new System.Timers.Timer();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.TagTabPage.SuspendLayout();
			this.TagHistoryTabPage.SuspendLayout();
			this.panel3.SuspendLayout();
			this.ZonePage.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(150, 174);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.Info;
			this.panel2.Controls.Add(this.label11);
			this.panel2.Controls.Add(this.label10);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.pictureBox1);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.panel2.ForeColor = System.Drawing.Color.White;
			this.panel2.Location = new System.Drawing.Point(0, 4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(148, 168);
			this.panel2.TabIndex = 0;
			// 
			// label11
			// 
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label11.BackColor = System.Drawing.Color.White;
			this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label11.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label11.Location = new System.Drawing.Point(3, 58);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(31, 18);
			this.label11.TabIndex = 16;
			this.label11.Text = "Loc:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.BackColor = System.Drawing.Color.White;
			this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label10.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label10.Location = new System.Drawing.Point(3, 34);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(31, 18);
			this.label10.TabIndex = 15;
			this.label10.Text = "ID:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.White;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label2.Location = new System.Drawing.Point(35, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 18);
			this.label2.TabIndex = 14;
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Location = new System.Drawing.Point(5, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 23);
			this.label1.TabIndex = 13;
			this.label1.Text = "Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new System.Drawing.Point(36, 82);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(80, 82);
			this.pictureBox1.TabIndex = 11;
			this.pictureBox1.TabStop = false;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.White;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label3.Location = new System.Drawing.Point(35, 58);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 18);
			this.label3.TabIndex = 12;
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(150, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 174);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// splitter2
			// 
			this.splitter2.Location = new System.Drawing.Point(153, 0);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(3, 174);
			this.splitter2.TabIndex = 3;
			this.splitter2.TabStop = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.TagTabPage);
			this.tabControl1.Controls.Add(this.TagHistoryTabPage);
			this.tabControl1.Controls.Add(this.ZonePage);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.ImageList = this.imageList1;
			this.tabControl1.Location = new System.Drawing.Point(156, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(824, 174);
			this.tabControl1.TabIndex = 10;
			this.tabControl1.StyleChanged += new System.EventHandler(this.tabControl1_StyleChanged);
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged_1);
			// 
			// TagTabPage
			// 
			this.TagTabPage.ContextMenu = this.contextMenu1;
			this.TagTabPage.Controls.Add(this.TagRefreshbutton);
			this.TagTabPage.Controls.Add(this.TagsListView);
			this.TagTabPage.ImageIndex = 2;
			this.TagTabPage.Location = new System.Drawing.Point(4, 23);
			this.TagTabPage.Name = "TagTabPage";
			this.TagTabPage.Size = new System.Drawing.Size(816, 147);
			this.TagTabPage.TabIndex = 3;
			this.TagTabPage.Text = "Tags";
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3});
			this.contextMenu1.Popup += new System.EventHandler(this.contextMenu1_Popup);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "First Name, Last Name";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Last Name, First Name";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Create Report";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// TagRefreshbutton
			// 
			this.TagRefreshbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.TagRefreshbutton.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.TagRefreshbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TagRefreshbutton.ForeColor = System.Drawing.Color.White;
			this.TagRefreshbutton.Location = new System.Drawing.Point(754, 0);
			this.TagRefreshbutton.Name = "TagRefreshbutton";
			this.TagRefreshbutton.Size = new System.Drawing.Size(62, 18);
			this.TagRefreshbutton.TabIndex = 26;
			this.TagRefreshbutton.Text = "Refresh";
			this.TagRefreshbutton.Click += new System.EventHandler(this.TagRefreshbutton_Click);
			// 
			// TagsListView
			// 
			this.TagsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TagsListView.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(237)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.TagsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						   this.columnHeader1,
																						   this.columnHeader2,
																						   this.columnHeader25,
																						   this.columnHeader3,
																						   this.columnHeader4,
																						   this.columnHeader17});
			this.TagsListView.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.TagsListView.FullRowSelect = true;
			this.TagsListView.GridLines = true;
			this.TagsListView.HideSelection = false;
			this.TagsListView.Location = new System.Drawing.Point(0, 0);
			this.TagsListView.MultiSelect = false;
			this.TagsListView.Name = "TagsListView";
			this.TagsListView.Size = new System.Drawing.Size(816, 147);
			this.TagsListView.TabIndex = 25;
			this.TagsListView.View = System.Windows.Forms.View.Details;
			this.TagsListView.Click += new System.EventHandler(this.TagsListView_Click);
			this.TagsListView.DoubleClick += new System.EventHandler(this.TagsListView_DoubleClick);
			this.TagsListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.TagsListView_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Tag ID";
			this.columnHeader1.Width = 70;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Name";
			this.columnHeader2.Width = 110;
			// 
			// columnHeader25
			// 
			this.columnHeader25.Text = "Department";
			this.columnHeader25.Width = 150;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Event";
			this.columnHeader3.Width = 195;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Location";
			this.columnHeader4.Width = 120;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "Timestamp";
			this.columnHeader17.Width = 115;
			// 
			// TagHistoryTabPage
			// 
			this.TagHistoryTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.TagHistoryTabPage.Controls.Add(this.button1);
			this.TagHistoryTabPage.Controls.Add(this.panel3);
			this.TagHistoryTabPage.Controls.Add(this.NameLabel);
			this.TagHistoryTabPage.Controls.Add(this.TagHistRefreshButton);
			this.TagHistoryTabPage.Controls.Add(this.label5);
			this.TagHistoryTabPage.Controls.Add(this.ToDateTimePicker);
			this.TagHistoryTabPage.Controls.Add(this.label4);
			this.TagHistoryTabPage.Controls.Add(this.FromDateTimePicker);
			this.TagHistoryTabPage.Controls.Add(this.TagHistoryListView);
			this.TagHistoryTabPage.ForeColor = System.Drawing.Color.Blue;
			this.TagHistoryTabPage.ImageIndex = 3;
			this.TagHistoryTabPage.Location = new System.Drawing.Point(4, 23);
			this.TagHistoryTabPage.Name = "TagHistoryTabPage";
			this.TagHistoryTabPage.Size = new System.Drawing.Size(816, 147);
			this.TagHistoryTabPage.TabIndex = 0;
			this.TagHistoryTabPage.Text = "Tag History";
			this.TagHistoryTabPage.Visible = false;
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.button1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.White;
			this.button1.Location = new System.Drawing.Point(630, 6);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(94, 20);
			this.button1.TabIndex = 16;
			this.button1.Text = "Create Report";
			this.button1.Visible = false;
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.Controls.Add(this.HistoryPictureBox);
			this.panel3.Location = new System.Drawing.Point(706, 26);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(108, 119);
			this.panel3.TabIndex = 15;
			// 
			// HistoryPictureBox
			// 
			this.HistoryPictureBox.BackColor = System.Drawing.SystemColors.Desktop;
			this.HistoryPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.HistoryPictureBox.Location = new System.Drawing.Point(14, 18);
			this.HistoryPictureBox.Name = "HistoryPictureBox";
			this.HistoryPictureBox.Size = new System.Drawing.Size(80, 82);
			this.HistoryPictureBox.TabIndex = 12;
			this.HistoryPictureBox.TabStop = false;
			// 
			// NameLabel
			// 
			this.NameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.NameLabel.BackColor = System.Drawing.Color.Teal;
			this.NameLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.NameLabel.ForeColor = System.Drawing.Color.White;
			this.NameLabel.Location = new System.Drawing.Point(266, 2);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(344, 23);
			this.NameLabel.TabIndex = 14;
			this.NameLabel.Text = "Name";
			// 
			// TagHistRefreshButton
			// 
			this.TagHistRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.TagHistRefreshButton.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.TagHistRefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TagHistRefreshButton.ForeColor = System.Drawing.Color.White;
			this.TagHistRefreshButton.Location = new System.Drawing.Point(754, 6);
			this.TagHistRefreshButton.Name = "TagHistRefreshButton";
			this.TagHistRefreshButton.Size = new System.Drawing.Size(62, 20);
			this.TagHistRefreshButton.TabIndex = 11;
			this.TagHistRefreshButton.Text = "Refresh";
			this.TagHistRefreshButton.Click += new System.EventHandler(this.TagHistRefreshButton_Click);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(138, 6);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(22, 18);
			this.label5.TabIndex = 10;
			this.label5.Text = "To:";
			// 
			// ToDateTimePicker
			// 
			this.ToDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ToDateTimePicker.Checked = false;
			this.ToDateTimePicker.CustomFormat = "MM/dd/yy";
			this.ToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.ToDateTimePicker.Location = new System.Drawing.Point(160, 4);
			this.ToDateTimePicker.Name = "ToDateTimePicker";
			this.ToDateTimePicker.Size = new System.Drawing.Size(84, 20);
			this.ToDateTimePicker.TabIndex = 9;
			this.ToDateTimePicker.Value = new System.DateTime(2006, 9, 28, 0, 0, 0, 0);
			this.ToDateTimePicker.CloseUp += new System.EventHandler(this.ToDateTimePicker_CloseUp);
			this.ToDateTimePicker.ValueChanged += new System.EventHandler(this.ToDateTimePicker_ValueChanged);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Location = new System.Drawing.Point(2, 6);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(33, 18);
			this.label4.TabIndex = 8;
			this.label4.Text = "From:";
			// 
			// FromDateTimePicker
			// 
			this.FromDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.FromDateTimePicker.Checked = false;
			this.FromDateTimePicker.CustomFormat = "MM/dd/yy";
			this.FromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.FromDateTimePicker.Location = new System.Drawing.Point(36, 4);
			this.FromDateTimePicker.Name = "FromDateTimePicker";
			this.FromDateTimePicker.Size = new System.Drawing.Size(82, 20);
			this.FromDateTimePicker.TabIndex = 7;
			this.FromDateTimePicker.Value = new System.DateTime(2006, 9, 28, 0, 0, 0, 0);
			this.FromDateTimePicker.CloseUp += new System.EventHandler(this.FromDateTimePicker_CloseUp);
			// 
			// TagHistoryListView
			// 
			this.TagHistoryListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TagHistoryListView.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(237)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.TagHistoryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								 this.columnHeader5,
																								 this.columnHeader6,
																								 this.columnHeader7});
			this.TagHistoryListView.ContextMenu = this.contextMenu1;
			this.TagHistoryListView.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.TagHistoryListView.GridLines = true;
			this.TagHistoryListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.TagHistoryListView.Location = new System.Drawing.Point(0, 26);
			this.TagHistoryListView.MultiSelect = false;
			this.TagHistoryListView.Name = "TagHistoryListView";
			this.TagHistoryListView.Size = new System.Drawing.Size(704, 119);
			this.TagHistoryListView.TabIndex = 6;
			this.TagHistoryListView.View = System.Windows.Forms.View.Details;
			this.TagHistoryListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.TagHistoryListView_ColumnClick);
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Location";
			this.columnHeader5.Width = 140;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Event";
			this.columnHeader6.Width = 195;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Timestamp";
			this.columnHeader7.Width = 125;
			// 
			// ZonePage
			// 
			this.ZonePage.Controls.Add(this.ZonesRefreshButton);
			this.ZonePage.Controls.Add(this.ZoneListView);
			this.ZonePage.ImageIndex = 4;
			this.ZonePage.Location = new System.Drawing.Point(4, 23);
			this.ZonePage.Name = "ZonePage";
			this.ZonePage.Size = new System.Drawing.Size(816, 147);
			this.ZonePage.TabIndex = 4;
			this.ZonePage.Text = "Zones";
			// 
			// ZonesRefreshButton
			// 
			this.ZonesRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ZonesRefreshButton.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ZonesRefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ZonesRefreshButton.ForeColor = System.Drawing.Color.White;
			this.ZonesRefreshButton.Location = new System.Drawing.Point(754, 0);
			this.ZonesRefreshButton.Name = "ZonesRefreshButton";
			this.ZonesRefreshButton.Size = new System.Drawing.Size(62, 19);
			this.ZonesRefreshButton.TabIndex = 27;
			this.ZonesRefreshButton.Text = "Refresh";
			this.ZonesRefreshButton.Click += new System.EventHandler(this.ZonesRefreshButton_Click);
			// 
			// ZoneListView
			// 
			this.ZoneListView.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(237)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.ZoneListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						   this.columnHeader18,
																						   this.columnHeader19,
																						   this.columnHeader20,
																						   this.columnHeader21,
																						   this.columnHeader22});
			this.ZoneListView.ContextMenu = this.contextMenu1;
			this.ZoneListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ZoneListView.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ZoneListView.FullRowSelect = true;
			this.ZoneListView.GridLines = true;
			this.ZoneListView.HideSelection = false;
			this.ZoneListView.Location = new System.Drawing.Point(0, 0);
			this.ZoneListView.MultiSelect = false;
			this.ZoneListView.Name = "ZoneListView";
			this.ZoneListView.Size = new System.Drawing.Size(816, 147);
			this.ZoneListView.SmallImageList = this.imageList1;
			this.ZoneListView.TabIndex = 25;
			this.ZoneListView.View = System.Windows.Forms.View.Details;
			this.ZoneListView.Click += new System.EventHandler(this.ZoneListView_Click);
			this.ZoneListView.DoubleClick += new System.EventHandler(this.ZoneListView_DoubleClick);
			this.ZoneListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ZoneListView_ColumnClick);
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "ID";
			this.columnHeader18.Width = 90;
			// 
			// columnHeader19
			// 
			this.columnHeader19.Text = "Location";
			this.columnHeader19.Width = 200;
			// 
			// columnHeader20
			// 
			this.columnHeader20.Text = "Status";
			this.columnHeader20.Width = 90;
			// 
			// columnHeader21
			// 
			this.columnHeader21.Text = "Timestamp";
			this.columnHeader21.Width = 120;
			// 
			// columnHeader22
			// 
			this.columnHeader22.Text = "Reader/FGen";
			this.columnHeader22.Width = 100;
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage2.Controls.Add(this.LocationLabel);
			this.tabPage2.Controls.Add(this.ZoneHistoryListView);
			this.tabPage2.Controls.Add(this.ZoneRefreshButton);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.ZoneToDateTimePicker);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.ZoneFromDateTimePicker);
			this.tabPage2.ImageIndex = 3;
			this.tabPage2.Location = new System.Drawing.Point(4, 23);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(816, 147);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Zone History";
			this.tabPage2.Visible = false;
			// 
			// LocationLabel
			// 
			this.LocationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LocationLabel.BackColor = System.Drawing.Color.Teal;
			this.LocationLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LocationLabel.ForeColor = System.Drawing.Color.White;
			this.LocationLabel.Location = new System.Drawing.Point(360, 4);
			this.LocationLabel.Name = "LocationLabel";
			this.LocationLabel.Size = new System.Drawing.Size(330, 23);
			this.LocationLabel.TabIndex = 18;
			this.LocationLabel.Text = "Location:";
			// 
			// ZoneHistoryListView
			// 
			this.ZoneHistoryListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ZoneHistoryListView.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(237)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.ZoneHistoryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								  this.columnHeader8,
																								  this.columnHeader9,
																								  this.columnHeader27,
																								  this.columnHeader11,
																								  this.columnHeader23,
																								  this.columnHeader10});
			this.ZoneHistoryListView.ContextMenu = this.contextMenu1;
			this.ZoneHistoryListView.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ZoneHistoryListView.GridLines = true;
			this.ZoneHistoryListView.Location = new System.Drawing.Point(0, 38);
			this.ZoneHistoryListView.MultiSelect = false;
			this.ZoneHistoryListView.Name = "ZoneHistoryListView";
			this.ZoneHistoryListView.Size = new System.Drawing.Size(816, 109);
			this.ZoneHistoryListView.TabIndex = 17;
			this.ZoneHistoryListView.View = System.Windows.Forms.View.Details;
			this.ZoneHistoryListView.Click += new System.EventHandler(this.ZoneHistoryListView_Click);
			this.ZoneHistoryListView.DoubleClick += new System.EventHandler(this.ZoneHistoryListView_DoubleClick);
			this.ZoneHistoryListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ZoneHistoryListView_ColumnClick);
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Tag ID";
			this.columnHeader8.Width = 70;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Name";
			this.columnHeader9.Width = 110;
			// 
			// columnHeader27
			// 
			this.columnHeader27.Text = "Department";
			this.columnHeader27.Width = 150;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Event";
			this.columnHeader11.Width = 195;
			// 
			// columnHeader23
			// 
			this.columnHeader23.Text = "Status";
			this.columnHeader23.Width = 80;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Timestamp";
			this.columnHeader10.Width = 150;
			// 
			// ZoneRefreshButton
			// 
			this.ZoneRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ZoneRefreshButton.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ZoneRefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ZoneRefreshButton.ForeColor = System.Drawing.Color.White;
			this.ZoneRefreshButton.Location = new System.Drawing.Point(754, 6);
			this.ZoneRefreshButton.Name = "ZoneRefreshButton";
			this.ZoneRefreshButton.Size = new System.Drawing.Size(62, 21);
			this.ZoneRefreshButton.TabIndex = 16;
			this.ZoneRefreshButton.Text = "Refresh";
			this.ZoneRefreshButton.Click += new System.EventHandler(this.ZoneRefreshButton_Click);
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label6.ForeColor = System.Drawing.Color.Blue;
			this.label6.Location = new System.Drawing.Point(158, 10);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(22, 19);
			this.label6.TabIndex = 15;
			this.label6.Text = "To:";
			// 
			// ZoneToDateTimePicker
			// 
			this.ZoneToDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ZoneToDateTimePicker.CustomFormat = "MM/dd/yy";
			this.ZoneToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.ZoneToDateTimePicker.Location = new System.Drawing.Point(180, 8);
			this.ZoneToDateTimePicker.Name = "ZoneToDateTimePicker";
			this.ZoneToDateTimePicker.Size = new System.Drawing.Size(82, 20);
			this.ZoneToDateTimePicker.TabIndex = 14;
			this.ZoneToDateTimePicker.Value = new System.DateTime(2006, 9, 28, 0, 0, 0, 0);
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label7.ForeColor = System.Drawing.Color.Blue;
			this.label7.Location = new System.Drawing.Point(2, 10);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(34, 19);
			this.label7.TabIndex = 13;
			this.label7.Text = "From:";
			// 
			// ZoneFromDateTimePicker
			// 
			this.ZoneFromDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ZoneFromDateTimePicker.Checked = false;
			this.ZoneFromDateTimePicker.CustomFormat = "MM/dd/yy";
			this.ZoneFromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.ZoneFromDateTimePicker.Location = new System.Drawing.Point(36, 8);
			this.ZoneFromDateTimePicker.Name = "ZoneFromDateTimePicker";
			this.ZoneFromDateTimePicker.Size = new System.Drawing.Size(82, 20);
			this.ZoneFromDateTimePicker.TabIndex = 12;
			this.ZoneFromDateTimePicker.Value = new System.DateTime(2006, 9, 28, 0, 0, 0, 0);
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage3.Controls.Add(this.NumAlarmLabel);
			this.tabPage3.Controls.Add(this.AlarmHistoryListView);
			this.tabPage3.Controls.Add(this.AlarmRefreshButton);
			this.tabPage3.Controls.Add(this.label8);
			this.tabPage3.Controls.Add(this.AlarmToDateTimePicker);
			this.tabPage3.Controls.Add(this.label9);
			this.tabPage3.Controls.Add(this.AlarmFromDateTimePicker);
			this.tabPage3.Location = new System.Drawing.Point(4, 23);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(816, 147);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Alarm History";
			this.tabPage3.Visible = false;
			// 
			// NumAlarmLabel
			// 
			this.NumAlarmLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.NumAlarmLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.NumAlarmLabel.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.NumAlarmLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.NumAlarmLabel.Location = new System.Drawing.Point(358, 8);
			this.NumAlarmLabel.Name = "NumAlarmLabel";
			this.NumAlarmLabel.Size = new System.Drawing.Size(132, 18);
			this.NumAlarmLabel.TabIndex = 24;
			this.NumAlarmLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// AlarmHistoryListView
			// 
			this.AlarmHistoryListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.AlarmHistoryListView.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(237)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.AlarmHistoryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								   this.columnHeader12,
																								   this.columnHeader13,
																								   this.columnHeader28,
																								   this.columnHeader24,
																								   this.columnHeader16,
																								   this.columnHeader15});
			this.AlarmHistoryListView.ContextMenu = this.contextMenu1;
			this.AlarmHistoryListView.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.AlarmHistoryListView.GridLines = true;
			this.AlarmHistoryListView.Location = new System.Drawing.Point(-2, 38);
			this.AlarmHistoryListView.MultiSelect = false;
			this.AlarmHistoryListView.Name = "AlarmHistoryListView";
			this.AlarmHistoryListView.Size = new System.Drawing.Size(816, 111);
			this.AlarmHistoryListView.TabIndex = 23;
			this.AlarmHistoryListView.View = System.Windows.Forms.View.Details;
			this.AlarmHistoryListView.Click += new System.EventHandler(this.AlarmHistoryListView_Click);
			this.AlarmHistoryListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.AlarmHistoryListView_ColumnClick);
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "Tag ID";
			this.columnHeader12.Width = 70;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "Name";
			this.columnHeader13.Width = 110;
			// 
			// columnHeader28
			// 
			this.columnHeader28.Text = "Department";
			this.columnHeader28.Width = 150;
			// 
			// columnHeader24
			// 
			this.columnHeader24.Text = "Alarm Description";
			this.columnHeader24.Width = 195;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "Location";
			this.columnHeader16.Width = 120;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "Timestamp";
			this.columnHeader15.Width = 150;
			// 
			// AlarmRefreshButton
			// 
			this.AlarmRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AlarmRefreshButton.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.AlarmRefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AlarmRefreshButton.ForeColor = System.Drawing.Color.White;
			this.AlarmRefreshButton.Location = new System.Drawing.Point(754, 8);
			this.AlarmRefreshButton.Name = "AlarmRefreshButton";
			this.AlarmRefreshButton.Size = new System.Drawing.Size(62, 20);
			this.AlarmRefreshButton.TabIndex = 22;
			this.AlarmRefreshButton.Text = "Refresh";
			this.AlarmRefreshButton.Click += new System.EventHandler(this.AlarmRefreshButton_Click);
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.ForeColor = System.Drawing.Color.Blue;
			this.label8.Location = new System.Drawing.Point(152, 10);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(22, 18);
			this.label8.TabIndex = 21;
			this.label8.Text = "To:";
			// 
			// AlarmToDateTimePicker
			// 
			this.AlarmToDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AlarmToDateTimePicker.CustomFormat = "MM/dd/yy";
			this.AlarmToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.AlarmToDateTimePicker.Location = new System.Drawing.Point(174, 8);
			this.AlarmToDateTimePicker.Name = "AlarmToDateTimePicker";
			this.AlarmToDateTimePicker.Size = new System.Drawing.Size(76, 20);
			this.AlarmToDateTimePicker.TabIndex = 20;
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label9.ForeColor = System.Drawing.Color.Blue;
			this.label9.Location = new System.Drawing.Point(4, 12);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(34, 18);
			this.label9.TabIndex = 19;
			this.label9.Text = "From:";
			// 
			// AlarmFromDateTimePicker
			// 
			this.AlarmFromDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AlarmFromDateTimePicker.CustomFormat = "MM/dd/yy";
			this.AlarmFromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.AlarmFromDateTimePicker.Location = new System.Drawing.Point(38, 10);
			this.AlarmFromDateTimePicker.Name = "AlarmFromDateTimePicker";
			this.AlarmFromDateTimePicker.Size = new System.Drawing.Size(84, 20);
			this.AlarmFromDateTimePicker.TabIndex = 18;
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.SynchronizingObject = this;
			this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
			// 
			// AWIHistoryControl
			// 
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.splitter2);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel1);
			this.Name = "AWIHistoryControl";
			this.Size = new System.Drawing.Size(980, 174);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.TagTabPage.ResumeLayout(false);
			this.TagHistoryTabPage.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ZonePage.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		
		public void ShowImage(string name, string location, string id, string timestamp, Image image, string stat)
		{
			if ((stat == "Alarm") || (stat == "Invalid"))
                panel2.BackColor = System.Drawing.SystemColors.Control;
			else
				panel2.BackColor = System.Drawing.SystemColors.Info;
			pictureBox1.Image = image;
			label1.Text = name;
			label2.Text = id; //id
			label3.Text = location; //location
			//panel2.Invalidate();
		}

		private object ExecScalar(string sql)
		{
			if (MainForm.m_connection == null)
				return null;

			object obj = new object();

			//lock (MainForm.m_connection)
            try {
                Monitor.TryEnter(MainForm.m_connection, 100);

				OdbcCommand cmd = new OdbcCommand(sql, MainForm.m_connection);
                try
                {
                    //myReader = myCommand.ExecuteReader();
                    obj = cmd.ExecuteScalar();
                    MainForm.reconnectCounter = -1;
                    timer1.Enabled = false;
                }
                catch (Exception ex)
                {
                    int ret = 0, ret1 = 0, ret2 = 0;
                    if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                        ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                        ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                    {
                        //error code 2013

                        if (MainForm.reconnectCounter < 0)
                        {
                            MainForm.reconnectCounter = 0;
                            timer1.Enabled = true;
                        }
                    }
                    return null;
                } //catch ..try
                finally
                {
                    Monitor.Exit(MainForm.m_connection);
                }

				//return cmd.ExecuteScalar();
				return obj;
			}
            catch
            {
                return obj;
            }
		}

		private Image GetTagImage(string tagId)
		{
			string sql = string.Format("SELECT Image FROM tags WHERE TagID = '{0}'", tagId);
			byte[] data = ExecScalar(sql) as byte[];
			if (data != null && !data.Equals('0'))
			{
				try
				{
					Stream stream = new MemoryStream(data);
                    if (stream == null)
                        return null;
                    if (stream.Length <= 1)
                        return null;

					return Image.FromStream(stream);
				}
				catch (System.ArgumentException) {}
			}
			return null;
		}

		/*private void DBConnectionStatusHandler(status stat, OdbcConnection connect)
		{
			//if (m_connection != null)
				//return;

			if (stat == status.open)
			{
				m_connection = connect;
                
				if (tabControl1.SelectedIndex == 0)
				     UpdateTagsPage();
				//if (providerName == dbProvider.SQL)
					//DBStatusBarPanel.Text = "DB Server : SQL                 Database : Connected";
				//else
					//DBStatusBarPanel.Text = "DB Server : MySQL               Database : Connected";
				//timer2.Enabled = true;
			}
			else if (stat == status.broken)
			{
				m_connection = null;
				//DBStatusBarPanel.Text = "Database : Connection Lost";
			}
			else if (stat == status.close)
			{
				m_connection = null;
				//if (providerName == dbProvider.SQL)
					//DBStatusBarPanel.Text = "DB Server : SQL                 Database : Disconnected";
				//else
					//DBStatusBarPanel.Text = "DB Server : MySQL               Database : Disconnected";
				//timer2.Enabled = false;
			}
		}*/

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void listView3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void listView5_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void dateTimePicker6_ValueChanged(object sender, System.EventArgs e)
		{
		
		}

		private void dateTimePicker3_ValueChanged(object sender, System.EventArgs e)
		{
		
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void label3_Click(object sender, System.EventArgs e)
		{
		
		}

		private void tabControl1_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			if (tabControl1.SelectedIndex == 0)
			{
				//zoneIDHistory = null;
				//TagIDHistory = null;
				UpdateTagsPage(lastflname);
			}
			else if (tabControl1.SelectedIndex == 1)
			{
				//zoneIDHistory = null;
				ToDateTimePicker.Value = DateTime.Now;
				FromDateTimePicker.Value = DateTime.Today; 
				string str = "", str1 = "";
				if ((str=GetTagName(TagIDHistory)) != "")
				{
					str1=GetDepartmentName(TagIDHistory.Remove(0,3));
					NameLabel.Text = str + ",    ID: " + TagIDHistory.Remove(0,3) + ",    " + "Dept: " + str1; //tag name
				}
				else
					NameLabel.Text = "";
				
				if ((TagIDHistory != null) && tagListDClicked)
				{
					tagListDClicked = false;
					ShowTagHistory(TagIDHistory);
				}

				HistoryPictureBox.Image = GetTagImage(TagIDHistory);
			}
			else if (tabControl1.SelectedIndex == 2)
			{
				//TagIDHistory = null;
				//zoneIDHistory = null;
				UpdateZoneViewPage();
			}
			else if (tabControl1.SelectedIndex == 3)
			{
				//TagIDHistory = null;
				FromDateTimePicker.Checked = false;
				ToDateTimePicker.Checked = false;

				ZoneToDateTimePicker.Value = DateTime.Now;
				ZoneFromDateTimePicker.Value = DateTime.Today;
				//NameLabel.Text = "";
				//HistoryPictureBox.Image = null;
				string str;
				if ((str=GetZoneLocationName(zoneIDHistory)) != null)
					LocationLabel.Text = "Location: " + str; //tag name
				else
					LocationLabel.Text = "Location: ";

				if ((zoneIDHistory != null) && zoneListDClicked)
				{
					zoneListDClicked = false;
					ShowZoneHistory(zoneIDHistory);
				}
			}
			else if (tabControl1.SelectedIndex == 4)
			{
				//TagIDHistory = null;
				//zoneIDHistory = null;
			}
		}

		public int GetTabIndex()
		{
           return (tabControl1.SelectedIndex);
		}

		string GetDepartmentName(string tagID)
		{
			if (MainForm.m_connection == null)
				return ("");
		   
			string str = "";
		   
			lock (MainForm.m_connection) //dec06-06
			{
				StringBuilder mySelectQuery = new StringBuilder();

				mySelectQuery.Append("SELECT Department FROM employees WHERE ID = "); 
				mySelectQuery.AppendFormat("'{0}'", tagID);          
				
				string mySelectStr = mySelectQuery.ToString();

				OdbcCommand myCommand = new OdbcCommand(mySelectStr, MainForm.m_connection);  
				OdbcDataReader myReader = null;
		   
			//lock (MainForm.m_connection)
			//{
				try
				{
					myReader = myCommand.ExecuteReader();
					MainForm.reconnectCounter = -1;
					timer1.Enabled = false;
				}
				catch (Exception ex)
				{
					//ShowErrorMessage(ex.Message);
					/*if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return ("");*/

					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{  
						//error code 2013

						if (MainForm.reconnectCounter < 0)
						{
							MainForm.reconnectCounter = 0;
							timer1.Enabled = true;
						}

						/*Thread.Sleep(500);

						if (!ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
						{
							MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							if (myReader != null)
							{
								if (!myReader.IsClosed)
									myReader.Close();
							}
							return "";
						}*/                             
					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return "";
				} //catch ..try
				//}//lock
 
				if (myReader.Read())
				{
					str = myReader.GetString(0);
				}
			
				myReader.Close();
				return str;
			}//lock m_connection
		}

		public void UpdateTagsPage(bool flname)
		{
			//string mySelectQuery = "SELECT tags.ID, tags.Name, tagactivity.Event, tagactivity.Timestamp, zones.Location FROM tags, tagactivity, zones WHERE tagactivity.TagID = 'ACC'+tags.ID AND tagactivity.ZoneID = zones.ID";
			
			if (MainForm.m_connection == null)
				return;

			lock (MainForm.m_connection)  //dec06-06
			{
			string mySelectQuery = "SELECT tags.TagID, employees.FirstName, employees.LastName, employees.Department, tagactivity.Event, zones.Location, tagactivity.Timestamp FROM tags LEFT JOIN tagactivity ON (tags.TagID = tagactivity.TagID) LEFT JOIN employees ON (tags.ID = employees.ID) LEFT JOIN zones ON (tagactivity.ZoneID = zones.ID) ORDER BY tags.TagID";

			OdbcCommand myCommand = new OdbcCommand(mySelectQuery, MainForm.m_connection); 

			OdbcDataReader myReader = null;

			//lock (MainForm.m_connection)
			//{
				/*try
				{
					myReader = myCommand.ExecuteReader();
				}
				catch (Exception ex)
				{
					//ShowErrorMessage(ex.Message);
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				}*/

				try
				{
					myReader = myCommand.ExecuteReader();
					MainForm.reconnectCounter = -1;
					timer1.Enabled = false;
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
							timer1.Enabled = true;
						}

						/*Thread.Sleep(500);

						if (!ReconnectToDBServer()) //this program should be done either in main or in the DBInterface.
						{
							MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							if (myReader != null)
							{
								if (!myReader.IsClosed)
									myReader.Close();
							}
							return;
						}*/
                              
					}//there is disconnection from db server

					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				}//try .. catch
			
			
				TagsListView.Items.Clear();
            
				string id="", fname="", lname="", department="", events="", location="", timestamp=null;
				while (myReader.Read())
				{
					ListViewItem listItem = new ListViewItem(((id=myReader.GetString(0))!=null)?id:" ");  //first index
					//listItem.SubItems.Add(((id=myReader.GetString(0))!=null)?id:" ");  //ID
					try
					{
						//listItem.SubItems.Add(((name=myReader.GetString(1))!=null)?name:" ");  //name
                        if (myReader.IsDBNull(1))
                            fname = "";
                        else
                            fname  = myReader.GetString(1);  //name
					}
					catch
					{
						fname = "";
					}

					try
					{
						//listItem.SubItems.Add(((name=myReader.GetString(1))!=null)?name:" ");  //name
                        if (myReader.IsDBNull(2))
                            lname = "";
                        else
                            lname  = myReader.GetString(2);  //name
					}
					catch
					{
						lname = "";
					}

					if (flname)
                        listItem.SubItems.Add(fname + " " + lname);
					else
						listItem.SubItems.Add(lname + " " + fname);

					try
					{
						listItem.SubItems.Add(((department=myReader.GetString(3))!=null)?department:" ");  //department
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
                        if (myReader.IsDBNull(4))
                            listItem.SubItems.Add("");
                        else
						    listItem.SubItems.Add(((events=myReader.GetString(4))!=null)?events:" ");  //event
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
                        if (myReader.IsDBNull(5))
                            listItem.SubItems.Add("Not Defined");
                        else
						    listItem.SubItems.Add(((location=myReader.GetString(5))!=null)?location:" ");  //location
					}
					catch
					{
						listItem.SubItems.Add("Not Defined");
					}

					try
					{
                        if (myReader.IsDBNull(6))
                            listItem.SubItems.Add("");
                        else
						    listItem.SubItems.Add(((timestamp=myReader.GetDateTime(6).ToString("MM-dd-yyyy  HH:mm:ss"))!=null)?timestamp:" ");  //timestamp
						//listItem.SubItems.Add(((timestamp=myReader.GetString(6))!=null)?timestamp:" ");  //timestamp
					}
					catch
					{
						listItem.SubItems.Add("");
					}
					TagsListView.Items.Add(listItem);
				
				}

				myReader.Close();

			}//lock m_connection
		}

		private void TagsListView_DoubleClick(object sender, System.EventArgs e)
		{
		    ListViewItem selItem = TagsListView.SelectedItems[0];

			if (selItem.Text != null)
			{
				tagListDClicked = true;
				TagIDHistory = selItem.Text;
				tabControl1.SelectedIndex = 1;
				
		
				//FromDateTimePicker.Checked = false;
				//NameLabel.Text = "";
				//HistoryPictureBox.Image = null;
				//ShowTagHistory(selItem.Text);
			}
		}

		private string GetZoneLocationName(string zoneID)
		{
			string str = "";
			if (MainForm.m_connection == null)
				return str;

			lock (MainForm.m_connection)  //dec-06-06
			{

				StringBuilder sql = new StringBuilder();
				sql.AppendFormat("SELECT zones.Location FROM zones WHERE Zones.ID = '{0}'", zoneID);
				string mySelectStr = sql.ToString();
				OdbcCommand myCommand = new OdbcCommand(mySelectStr, MainForm.m_connection);
				OdbcDataReader myReader = null;

				//lock (MainForm.m_connection)
				//{
				/*try
				{
					myReader = myCommand.ExecuteReader();
				}
				catch
				{
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return "";
				}*/

				try
				{
					myReader = myCommand.ExecuteReader();
					MainForm.reconnectCounter = -1;
					timer1.Enabled = false;
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
							timer1.Enabled = true;
						}

						/*Thread.Sleep(500);

						if (!ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
						{
							MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							if (myReader != null)
							{
								if (!myReader.IsClosed)
									myReader.Close();
							}
							return "";
						}*/
					                                  
					}//there is disconnection from db server

					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}

					return "";

				}//try .. catch
				//}//lock

				myReader.Read();
                if (myReader.HasRows)
                {
                    try
                    {
                        str = myReader.GetString(0);
                    }
                    catch
                    {
                        str = "";
                    }
                }
				myReader.Close();
				return str;
			}//lock m_connection
		}

		#region ReconnectToDBServer
		public bool ReconnectToDBServer()
		{
			if (MainForm.conStr.Length > 0)
			{
				if (!odbcDB.Connect(MainForm.conStr))	//MYSQL
				{						
					return false;
				}
			}

			return true;
		}
		#endregion 

		private string GetTagName(string tagID)
		{
			string str = "";
			if (MainForm.m_connection == null)
				return str;

			lock (MainForm.m_connection)  //dec-06-06
			{
				StringBuilder sql = new StringBuilder();
				sql.AppendFormat("SELECT tags.FirstName, tags.LastName FROM tags WHERE tags.TagID = '{0}'", tagID);
				string mySelectStr = sql.ToString();
				OdbcCommand myCommand = new OdbcCommand(mySelectStr, MainForm.m_connection);
				OdbcDataReader myReader = null;

				/*lock (MainForm.m_connection)
				{
					try
					{
						myReader = myCommand.ExecuteReader();
					}
					catch
					{
						if (myReader != null)
						{
							if (!myReader.IsClosed)
								myReader.Close();
						}
						return "";
					}
				}*/

				//lock (MainForm.m_connection)
				//{
				try
				{
					myReader = myCommand.ExecuteReader();
					MainForm.reconnectCounter = -1;
					timer1.Enabled = false;
				}
				catch (Exception ex)
				{
					//ShowErrorMessage(ex.Message);
					/*if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return ("");*/

					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{  
						//error code 2013
						
						if (MainForm.reconnectCounter < 0)
						{
							MainForm.reconnectCounter = 0;
							timer1.Enabled = true;
						}

						/*Thread.Sleep(500);

						if (!ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
						{
							MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							if (myReader != null)
							{
								if (!myReader.IsClosed)
									myReader.Close();
							}
							return "";
						}*/                             
					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return "";

				} //catch ..try
				//}//lock

				myReader.Read();
                if (myReader.HasRows)
                {
                    try
                    {
                        str = string.Format("{0} {1}", myReader.GetString(0), myReader.GetString(1)).Trim();
                    }
                    catch
                    {
                        str = "";
                    }
                }
				myReader.Close();
				return str;
			}//lock m_connection
		}

		private void ShowTagHistory(string tagID)
		{
			if (MainForm.m_connection == null)
				return;

			lock (MainForm.m_connection)
			{
				TagIDHistory = tagID;

				/*
				 SELECT traffic.TagID, tags.Name, tags.ID, zones.Location, traffic.Event, traffic.time
				From traffic
					LEFT JOIN tags
				ON traffic.TagID = tags.ID
					LEFT JOIN zones
				ON traffic.ZoneID = zones.ID
				*/
				StringBuilder sql = new StringBuilder();
				sql.Append("SELECT zones.Location, history.Event, history.Timestamp FROM history, zones");
				sql.AppendFormat(" WHERE (history.ZoneID = zones.ID) AND history.TagID = '{0}'", tagID, tagID);
				sql.AppendFormat(" AND history.Timestamp >= '{0}'", FromDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss"));
				sql.AppendFormat(" AND history.Timestamp <= '{0}'", ToDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss"));
				sql.Append(" ORDER BY Timestamp DESC");

				string mySelectStr = sql.ToString();

				OdbcCommand myCommand = new OdbcCommand(mySelectStr, MainForm.m_connection); 

				OdbcDataReader myReader = null;

				/*//lock (MainForm.m_connection)
				//{
				try
				{
					myReader = myCommand.ExecuteReader();
				}
				catch (Exception ex)
				{
					//ShowErrorMessage(ex.Message);
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				}
				//}*/

				//lock (MainForm.m_connection)
				//{
					try
					{
						myReader = myCommand.ExecuteReader();
						MainForm.reconnectCounter = -1;
						timer1.Enabled = false;
					}
					catch (Exception ex)
					{
						//ShowErrorMessage(ex.Message);
						/*if (myReader != null)
						{
							if (!myReader.IsClosed)
								myReader.Close();
						}
						return ("");*/

						int ret = 0, ret1 = 0, ret2 = 0;
						if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
							((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
							((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
						{  
							//error code 2013
							
							if (MainForm.reconnectCounter < 0)
							{
								MainForm.reconnectCounter = 0;
								timer1.Enabled = true;
							}

							/*Thread.Sleep(500);

							if (!ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
							{
								MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								if (myReader != null)
								{
									if (!myReader.IsClosed)
										myReader.Close();
								}
								return;
							}*/                             
						}
						if (myReader != null)
						{
							if (!myReader.IsClosed)
								myReader.Close();
						}
						return;
					} //catch ..try
				//}//lock
			
			
				//bool firstRec = true;
				//string tagID = "";

				TagHistoryListView.Items.Clear();
				this.Cursor = Cursors.WaitCursor;
                string events, location, timestamp; //name, id;
				while (myReader.Read())
				{
					ListViewItem listItem = new ListViewItem(((location=myReader.GetString(0))!=null)?location:" ");  //location
				
					try
					{
						listItem.SubItems.Add(((events=myReader.GetString(1))!=null)?events:" ");  //events
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						//listItem.SubItems.Add(((timestamp=myReader.GetString(2))!=null)?timestamp:" ");  //timestamp
						listItem.SubItems.Add(((timestamp=myReader.GetDateTime(2).ToString("MM-dd-yyyy  HH:mm:ss"))!=null)?timestamp:" ");  //timestamp
					}
					catch
					{
						listItem.SubItems.Add("");
					}

				

					TagHistoryListView.Items.Add(listItem);
				
				}//while
				this.Cursor = Cursors.Default;
				myReader.Close();
			}//lock	
		}

		private void ShowZoneHistory(string zoneID)
		{
			if (MainForm.m_connection == null)
				return;

			lock (MainForm.m_connection)
			{
				zoneIDHistory = zoneID;

				StringBuilder sql = new StringBuilder();
			
				//sql.Append("SELECT traffic.TagID, tags.Name, traffic.Event, traffic.Status, traffic.Time FROM traffic");
				//sql.Append(" LEFT JOIN tags ON traffic.TagID = tags.ID LEFT JOIN zones ON");
				//sql.AppendFormat(" traffic.ZoneID = '{0}'", zoneID);
				sql.Append("SELECT traffic.TagID, tags.FirstName, tags.LastName, employees.Department, traffic.Event, traffic.Status, traffic.Time FROM traffic");
				sql.Append(" LEFT JOIN tags ON traffic.TagID = tags.ID");
				sql.Append(" LEFT JOIN employees ON tags.ID = employees.ID");
				sql.AppendFormat(" WHERE traffic.ZoneID = '{0}'", zoneID);
			
			
				sql.AppendFormat(" AND traffic.Time >= '{0}'", ZoneFromDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss"));//("MM/dd/yy hh:mm:ss")); //("yyyy/MM/dd")); // HH:mm:ss"));
			
				sql.AppendFormat(" AND traffic.Time <= '{0}'", ZoneToDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss")); //ZoneToDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss")); //("MM/dd/yy  hh:mm:ss")); //("yyyy/MM/dd")); // HH:mm:ss"));
				sql.Append(" ORDER BY traffic.Time DESC, traffic.Index DESC");

			
				string mySelectStr = sql.ToString();

				if ((MainForm.m_connection.State == ConnectionState.Executing) ||
					(MainForm.m_connection.State == ConnectionState.Fetching))
					return;

				OdbcCommand myCommand = new OdbcCommand(mySelectStr, MainForm.m_connection); 

				OdbcDataReader myReader = null;

				/*//lock (MainForm.m_connection)
				//{
				try
				{
					myReader = myCommand.ExecuteReader();
				}
				catch (Exception ex)
				{
					//ShowErrorMessage(ex.Message);
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				}
				//}*/

				//lock (MainForm.m_connection)
				//{
					try
					{
						myReader = myCommand.ExecuteReader();
						MainForm.reconnectCounter = -1;
						timer1.Enabled = false;
					}
					catch (Exception ex)
					{
						//ShowErrorMessage(ex.Message);
						/*if (myReader != null)
						{
							if (!myReader.IsClosed)
								myReader.Close();
						}
						return ("");*/

						int ret = 0, ret1 = 0, ret2 = 0;
						if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
							((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
							((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
						{  
							//error code 2013
							if (MainForm.reconnectCounter < 0)
							{
								MainForm.reconnectCounter = 0;
								timer1.Enabled = true;
							}
							
							/*Thread.Sleep(500);

							if (!ReconnectToDBServer()) //this program should be done either in main or in the DBInterface.
							{
								MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								if (myReader != null)
								{
									if (!myReader.IsClosed)
										myReader.Close();
								}
								return;
							}*/                             
						}
						if (myReader != null)
						{
							if (!myReader.IsClosed)
								myReader.Close();
						}
						return;
					} //catch ..try
				//}//lock
			
			
				//bool firstRec = true;
				//string tagID = "";

				ZoneHistoryListView.Items.Clear();
            
				//string id, name, events, location, timestamp;
				this.Cursor = Cursors.WaitCursor;
				string tid = "";
				while (myReader.Read())
				{
					tid = myReader.GetString(0);
					//Console.WriteLine("ShowZoneHistory ID=" + tid);
					ListViewItem listItem = new ListViewItem(tid);  //TagID
				
					try
					{
                        listItem.SubItems.Add(string.Format("{0} {1}", myReader.GetString(1), myReader.GetString(2)).Trim());  //name
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(myReader.GetString(3));  //depart
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(myReader.GetString(4));  //event
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(myReader.GetString(5));  //status
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						DateTime dt = myReader.GetDateTime(6);
                        listItem.SubItems.Add(dt.ToString("MM-dd-yyyy  HH:mm:ss"));
						//listItem.SubItems.Add(dt.ToShortDateString() + " " + dt.ToLongTimeString());  //(myReader.GetString(4));  //timestamp
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					ZoneHistoryListView.Items.Add(listItem);
				
				}//while
                this.Cursor = Cursors.Default;
				myReader.Close();
			}//lock m_connection
			
		}

		private void ShowAlarmHistory()
		{
			if (MainForm.m_connection == null)
				return;

			lock (MainForm.m_connection)
			{
				StringBuilder sql = new StringBuilder();
			
				/*sql.Append("SELECT traffic.TagID, tags.Name, traffic.Status, zones.Location, traffic.Time FROM traffic");
				sql.Append(" LEFT JOIN tags ON traffic.TagID = tags.ID");
				sql.Append(" LEFT JOIN zones ON traffic.ZoneID = zones.ID");
				sql.Append(" WHERE traffic.Status = 'Invalid'");*/

                sql.Append("SELECT traffic.TagID, traffic.FirstName, traffic.LastName, traffic.Department, traffic.Status, traffic.Location, traffic.Time FROM traffic WHERE (traffic.Status = 'Invalid' OR traffic.Status = 'Alarm' OR traffic.Status = 'Offline' OR traffic.Status = 'Action')");
			
				sql.AppendFormat(" AND (traffic.Time >= '{0}')", AlarmFromDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss"));//("MM/dd/yy hh:mm:ss")); //("yyyy/MM/dd")); // HH:mm:ss"));
				sql.AppendFormat(" AND (traffic.Time <= '{0}')", AlarmToDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss")); //ZoneToDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss")); //("MM/dd/yy  hh:mm:ss")); //("yyyy/MM/dd")); // HH:mm:ss"));
				sql.Append(" ORDER BY traffic.Time DESC");

			
				string mySelectStr = sql.ToString();
            
				if ((MainForm.m_connection.State == ConnectionState.Executing) ||
					(MainForm.m_connection.State == ConnectionState.Fetching))
					return;
				OdbcCommand myCommand = new OdbcCommand(mySelectStr, MainForm.m_connection); 

				OdbcDataReader myReader = null;

				/*//lock (MainForm.m_connection)
				//{
				try
				{
					myReader = myCommand.ExecuteReader();
				}
				catch (Exception ex)
				{
					//ShowErrorMessage(ex.Message);
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				}
				//}*/

				//lock (MainForm.m_connection)
				//{
					try
					{
						myReader = myCommand.ExecuteReader();
						MainForm.reconnectCounter = -1;
						timer1.Enabled = false;
					}
					catch (Exception ex)
					{
						//ShowErrorMessage(ex.Message);
						/*if (myReader != null)
						{
							if (!myReader.IsClosed)
								myReader.Close();
						}
						return ("");*/

						int ret = 0, ret1 = 0, ret2 = 0;
						if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
							((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
							((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
						{  
							//error code 2013

							if (MainForm.reconnectCounter < 0)
							{
								MainForm.reconnectCounter = 0;
								timer1.Enabled = true;
							}

							/*Thread.Sleep(500);

							if (!ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
							{
								MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								if (myReader != null)
								{
									if (!myReader.IsClosed)
										myReader.Close();
								}
								return;
							}*/                             
						}
						if (myReader != null)
						{
							if (!myReader.IsClosed)
								myReader.Close();
						}
						return;
					} //catch ..try
				//}//lock
			
			
				//bool firstRec = true;
				//string tagID = "";

				AlarmHistoryListView.Items.Clear();
            
				int nAlarm = 0;
				this.Cursor = Cursors.WaitCursor;
				while (myReader.Read())
				{
					ListViewItem listItem = new ListViewItem(myReader.GetString(0));  //TagID
				
					try
					{
                        listItem.SubItems.Add(string.Format("{0} {1}", myReader.GetString(1), myReader.GetString(2)).Trim());  //name
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(myReader.GetString(3));  //dept
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(myReader.GetString(4));  //status
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(myReader.GetString(5));  //location
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						//DateTime dt = myReader.GetDateTime(5);
						//listItem.SubItems.Add(dt.ToShortDateString() + " " + dt.ToLongTimeString());  //(myReader.GetString(4));  //timestamp
						listItem.SubItems.Add(myReader.GetDateTime(6).ToString("MM-dd-yyy  HH:mm:ss"));  //(myReader.GetString(4));  //timestamp
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					AlarmHistoryListView.Items.Add(listItem);
					nAlarm++;
				
				}//while

				this.Cursor = Cursors.Default;
				myReader.Close();
			
			    NumAlarmLabel.Text = "Number of alarms: " +  nAlarm.ToString();
           }//lock m_connection
			
		}

		private void FromDateTimePicker_CloseUp(object sender, System.EventArgs e)
		{
		   
		}

		private void ToDateTimePicker_CloseUp(object sender, System.EventArgs e)
		{
		   
		}

		private void ToDateTimePicker_ValueChanged(object sender, System.EventArgs e)
		{
            //if (ToDateTimePicker.Checked)
			   	//ToDateTimePicker.Value = DateTime.Now;
		}

		private void TagHistRefreshButton_Click(object sender, System.EventArgs e)
		{
			if (TagIDHistory != null)
			{
				//ToDateTimePicker.Value = DateTime.Now;
				//FromDateTimePicker.Value = DateTime.Today; 
				ShowTagHistory(TagIDHistory);
			}
		}

		private void TagsListView_Click(object sender, System.EventArgs e)
		{
			ListViewItem selItem = TagsListView.SelectedItems[0];
			if (selItem.Text != null)
			{
				TagIDHistory = selItem.Text;  //TagID
			}
		}

		public void UpdateZoneViewPage()
		{
			ZoneListView.Items.Clear();

			if (MainForm.m_connection == null)
				return;

            try
            {
                Monitor.TryEnter(MainForm.m_connection);

                try
                {
                    string sql = "SELECT ID, Location, Status, Time, CONCAT(ReaderID, '/', FieldGenID) AS 'Reader/FGen' FROM zones ORDER BY ID";
                    OdbcCommand myCommand = new OdbcCommand(sql, MainForm.m_connection);
                    OdbcDataReader myReader = null;

                    /*lock (MainForm.m_connection)
                    {
                        try
                        {
                            myReader = myCommand.ExecuteReader();
                        }
                        catch
                        {
                            if (myReader != null)
                            {
                                myReader.Close();	
                            }

                            return;
                        }
                    }*/

                    //lock (MainForm.m_connection)
                    //{
                    try
                    {
                        myReader = myCommand.ExecuteReader();
                        MainForm.reconnectCounter = -1;
                        timer1.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        //ShowErrorMessage(ex.Message);
                        /*if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                                myReader.Close();
                        }
                        return ("");*/

                        int ret = 0, ret1 = 0, ret2 = 0;
                        if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                            ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                            ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                        {
                            //error code 2013

                            if (MainForm.reconnectCounter < 0)
                            {
                                MainForm.reconnectCounter = 0;
                                timer1.Enabled = true;
                            }
                            /*Thread.Sleep(500);

                            if (!ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
                            {
                                MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                if (myReader != null)
                                {
                                    if (!myReader.IsClosed)
                                        myReader.Close();
                                }
                                return;
                            }*/
                        }
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                                myReader.Close();
                        }
                        return;
                    } //catch ..try
                    //}//lock

                    string stat = "";
                    //int i = 0;
                    while (myReader.Read())
                    {
                        ListViewItem listItem = new ListViewItem(myReader.GetString(0));  //zoneID
                        listItem.UseItemStyleForSubItems = false;

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
                            listItem.SubItems.Add((stat = myReader.GetString(2)));  //status
                            if (stat == "Online")
                            {
                                listItem.SubItems[2].ForeColor = System.Drawing.Color.DarkGreen;
                                listItem.ImageIndex = 1;
                            }
                            else
                            {
                                listItem.SubItems[2].ForeColor = System.Drawing.Color.Red;
                                listItem.ImageIndex = 0;
                            }
                        }
                        catch
                        {
                            listItem.SubItems.Add("");
                        }

                        try
                        {
                            listItem.SubItems.Add(myReader.GetDateTime(3).ToString("MM-dd-yyyy  HH:mm:ss"));  //timestamp
                        }
                        catch
                        {
                            listItem.SubItems.Add("");
                        }

                        try
                        {
                            listItem.SubItems.Add(myReader.GetString(4));  //rdr/fgen
                        }
                        catch
                        {
                            listItem.SubItems.Add("");
                        }

                        ZoneListView.Items.Add(listItem);
                        //ZoneListView.Items[i++].SubItems[0].ForeColor = System.Drawing.Color.Red;


                    }//while

                    myReader.Close();

                    ZoneListView.Refresh();
                }
                finally
                {
                    Monitor.Exit(MainForm.m_connection);
                }
            }
            catch { }
		}

		public void UpdateZoneViewPage(string status, DateTime time, ushort rdrID, ushort fgenID)
		{
			//ZoneListView.Items.Clear();

			if (MainForm.m_connection == null)
				return;

			lock (MainForm.m_connection)  //dec06-06
			{
				StringBuilder mySelectQuery = new StringBuilder();
				mySelectQuery.Append("SELECT ID, Location FROM zones WHERE ReaderID = ");          
				mySelectQuery.AppendFormat("{0}", rdrID);
				//mySelectQuery.Append(" AND FieldGenID = ");
				//mySelectQuery.AppendFormat("{0}", fgenID);
				string mySelectStr = mySelectQuery.ToString();
					
				OdbcCommand myCommand = new OdbcCommand(mySelectStr, MainForm.m_connection);
				OdbcDataReader myReader = null;

				
				try
				{
					myReader = myCommand.ExecuteReader();
					MainForm.reconnectCounter = -1;
					timer1.Enabled = false;
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
							timer1.Enabled = true;
						}                
					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				} //catch ..try
				
				string id = "";
				string loc = "";

				bool found = false;
				int index = 0;
				if (myReader.Read())
				{
					try
					{
						id = myReader.GetString(0);
					}
					catch
					{
						myReader.Close();
                        return;
					}

					try
					{
						loc = myReader.GetString(1);
					}
					catch
					{
                        loc = "";
					}

					for (int j=0; j<ZoneListView.Items.Count; j++)
					{
						//Console.WriteLine("updateZone  " +  ZoneListView.Items[ix].Text + " vs " + id);
						if (ZoneListView.Items[j].Text == id)
						{
							ZoneListView.Items[j].Remove();
						    found = true;
							index = j;
							break;
						}
					}

					if (!found)
					{
						myReader.Close();
						return;
					}

					ListViewItem listItem = new ListViewItem(id);  //zoneID
					listItem.UseItemStyleForSubItems = false;

					try
					{
						listItem.SubItems.Add(loc);  //location
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(status);  //status
						if (status == "Online")
						{
							listItem.SubItems[2].ForeColor = System.Drawing.Color.DarkGreen;
							listItem.ImageIndex = 1;
						}
						else  //offline
						{
							listItem.SubItems[2].ForeColor = System.Drawing.Color.Red;
							listItem.ImageIndex = 0;
						}
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(time.ToString("MM-dd-yyyy  HH:mm:ss"));  //timestamp
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					try
					{
						listItem.SubItems.Add(Convert.ToString(rdrID) + "/" + Convert.ToString(fgenID));  //rdr/fgen
					}
					catch
					{
						listItem.SubItems.Add("");
					}
           
					if (found)
					    ZoneListView.Items.Insert(index, listItem);
					else
                        ZoneListView.Items.Add(listItem);

				}//if

				myReader.Close();

				ZoneListView.Refresh();
			}//lock m_connection
		}

		private void ZoneHistoryListView_Click(object sender, System.EventArgs e)
		{
			
		}

		private void ZoneListView_Click(object sender, System.EventArgs e)
		{
			ListViewItem item = ZoneListView.SelectedItems[0];
			zoneIDHistory = item.Text;
		}

		private void ZoneRefreshButton_Click(object sender, System.EventArgs e)
		{
			if (zoneIDHistory != null)
			{
				//ZoneToDateTimePicker.Value = DateTime.Now;
				//ZoneFromDateTimePicker.Value = DateTime.Today;
				ShowZoneHistory(zoneIDHistory);
			}
		}

		private void ZoneHistoryListView_DoubleClick(object sender, System.EventArgs e)
		{
			
		}

		private void ZoneListView_DoubleClick(object sender, System.EventArgs e)
		{
			ListViewItem item = ZoneListView.SelectedItems[0];
			if (item.Text != null)
			{
				zoneListDClicked = true;
				zoneIDHistory = item.Text;
				tabControl1.SelectedIndex = 3;
				
		        
				//ShowZoneHistory(zoneIDHistory);
			}
		}

		private void AlarmRefreshButton_Click(object sender, System.EventArgs e)
		{
			//AlarmToDateTimePicker.Value = DateTime.Now;
			//AlarmFromDateTimePicker.Value = DateTime.Today;
		    ShowAlarmHistory();
		}

		

		private void TagsListView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			m_sortReverse = (e.Column == m_sortColumn) ? !m_sortReverse : false;
			m_sortColumn = e.Column;
		    TagsListView.ListViewItemSorter = this;
			TagsListView.Sort();
			TagsListView.ListViewItemSorter = null;
		}
		#region IComparer Members

		public int Compare(object x, object y)
		{
			ListViewItem item1 = x as ListViewItem;
			ListViewItem item2 = y as ListViewItem;
			//IRfidTagActivity activity1 = item1.Tag as IRfidTagActivity;
			//IRfidTagActivity activity2 = item2.Tag as IRfidTagActivity;

			int rc = 0;

			if (tabControl1.SelectedIndex == 0)
				rc = SortTags(m_sortColumn, item1, item2);
			else if (tabControl1.SelectedIndex == 1)
				rc = SortTagHistory(m_sortColumn, item1, item2);
			else if (tabControl1.SelectedIndex == 2)
				rc = SortZones(m_sortColumn, item1, item2);
			else if (tabControl1.SelectedIndex == 3)
				rc = SortZoneHistory(m_sortColumn, item1, item2);
			else if (tabControl1.SelectedIndex == 4)
				rc = SortAlarms(m_sortColumn, item1, item2);

			return m_sortReverse ? -rc : rc;

		}

		private int SortTags(int column, ListViewItem item1, ListViewItem item2)
		{
			int rc = 0;
			string s1;
			string s2;

			switch (column)
			{
				case 0: //tagID
				
					int i1 = Convert.ToInt32(item1.SubItems[column].Text.Remove(0, 3));
					int i2 = Convert.ToInt32(item2.SubItems[column].Text.Remove(0, 3));
					
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

		private int SortTagHistory(int column, ListViewItem item1, ListViewItem item2)
		{
			int rc = 0;
			string s1;
			string s2;

			switch (column)
			{	
				default:
					s1 = item1.SubItems[column].Text;
					s2 = item2.SubItems[column].Text;
					rc = string.Compare(s1, s2);
				break;
			}
	
			return rc;
		}

		private int SortZones(int column, ListViewItem item1, ListViewItem item2)
		{
			int rc = 0;
			string s1;
			string s2;

			switch (column)
			{
				case 0: //tagID
				
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

		private int SortZoneHistory(int column, ListViewItem item1, ListViewItem item2)
		{
			int rc = 0;
			string s1;
			string s2;

			switch (column)
			{
				case 0: //tagID
				
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

		private int SortAlarms(int column,  ListViewItem item1, ListViewItem item2)
		{
			int rc = 0;
			string s1;
			string s2;

			switch (column)
			{
				case 0: //tagID
				
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
		#endregion

		private void TagHistoryListView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			m_sortReverse = (e.Column == m_sortColumn) ? !m_sortReverse : false;
			m_sortColumn = e.Column;
			TagHistoryListView.ListViewItemSorter = this;
			TagHistoryListView.Sort();
			TagHistoryListView.ListViewItemSorter = null;
		}

		private void ZoneListView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			m_sortReverse = (e.Column == m_sortColumn) ? !m_sortReverse : false;
			m_sortColumn = e.Column;
			ZoneListView.ListViewItemSorter = this;
			ZoneListView.Sort();
			ZoneListView.ListViewItemSorter = null;
		}

		private void ZoneHistoryListView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			m_sortReverse = (e.Column == m_sortColumn) ? !m_sortReverse : false;
			m_sortColumn = e.Column;
			ZoneHistoryListView.ListViewItemSorter = this;
			ZoneHistoryListView.Sort();
			ZoneHistoryListView.ListViewItemSorter = null;
		}

		private void AlarmHistoryListView_Click(object sender, System.EventArgs e)
		{
			
		}

		private void AlarmHistoryListView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			m_sortReverse = (e.Column == m_sortColumn) ? !m_sortReverse : false;
			m_sortColumn = e.Column;
			AlarmHistoryListView.ListViewItemSorter = this;
			AlarmHistoryListView.Sort();
			AlarmHistoryListView.ListViewItemSorter = null;
		}

		private void TagRefreshbutton_Click(object sender, System.EventArgs e)
		{
			UpdateTagsPage(lastflname);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
		   lastflname = true;
		   menuItem2.Checked = false;
		   menuItem1.Checked = true;
		   UpdateTagsPage(lastflname);
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			lastflname = false;
			menuItem2.Checked = true;
            menuItem1.Checked = false;
			UpdateTagsPage(lastflname);
		}

		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			
				if (MainForm.reconnectCounter == 3)
				{
					timer1.Enabled = false;
					MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
						if(ReconnectToDBServer())
						{
							timer1.Enabled = false;
							MainForm.reconnectCounter = -1;
						}
					}
				}
			
		}

		private void ZonesRefreshButton_Click(object sender, System.EventArgs e)
		{
		    UpdateZoneViewPage();
		}

		private void tabControl1_StyleChanged(object sender, System.EventArgs e)
		{
		
		}

		private void contextMenu1_Popup(object sender, System.EventArgs e)
		{
			if (tabControl1.SelectedIndex == 0)
			{
				contextMenu1.MenuItems[0].Visible = true;
				contextMenu1.MenuItems[1].Visible = true;
			}
			else
			{
               contextMenu1.MenuItems[0].Visible = false;
			   contextMenu1.MenuItems[1].Visible = false;
			}
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			if (tabControl1.SelectedIndex == 0)
			{
				contextMenu1.MenuItems[0].Visible = true;
				contextMenu1.MenuItems[1].Visible = true;
			}
			else
			{
				;
				//contextMenu1.MenuItems[0].Visible = false;
				//contextMenu1.MenuItems[1].Visible = false;
			}
		}
	}
}
