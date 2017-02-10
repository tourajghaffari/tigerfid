using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using AWIComponentLib.Database;
using AW_API_NET;
using AWIComponentLib.Communication;
using AWIComponentLib.Events;


namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for INVForm.
	/// </summary>
    public class INVForm : System.Windows.Forms.Form, IComparer
    {
        private System.Windows.Forms.ColumnHeader columnHeader29;
        private System.Windows.Forms.ColumnHeader columnHeader30;
        private System.Windows.Forms.ColumnHeader columnHeader31;
        private System.Windows.Forms.ColumnHeader columnHeader40;
        private System.Windows.Forms.ListView INVListView;
        private System.Windows.Forms.GroupBox EnabledGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label TotalEnabledTagsLabel;
        private System.Windows.Forms.Label TotalEnabledResLabel;
        private System.Windows.Forms.Label TotalEnabledNotResLabel;
        private System.Windows.Forms.Button RefreshListButton;
        private MainForm mForm;
        public OdbcDbClass odbcDB = new OdbcDbClass();
        private OdbcConnection m_connection = null;
        private tagGCStruct[] tagGC = new tagGCStruct[250];
        private APIEventHandler eventHandler = new APIEventHandler();
        private CommunicationClass communication;
        private ArrayList rdrSrchList = new ArrayList();
        private ArrayList tagSrchList = new ArrayList();
        private ArrayList LastTagSrchList = new ArrayList();
        private ArrayList selectedTagSrchList = new ArrayList();
        private System.Timers.Timer SerachTimer;
        private bool needUpdateFlag = false;
        private bool searchingAll;
        private bool searchingItems;
        //private uint numRdrSrch;
        private int rdrSrchIndex;
        private int tagSelIndex;
        private int numTries;
        private int m_sortColumn = -1;
        private bool m_sortReverse = false;
        private const int MAX_SEARCH_TRY = 3;
        private const int MAX_SEARCH_TRY_SELECTED = 2;
        private System.Windows.Forms.Label SearchLabel;
        private System.Timers.Timer timer1;
        private bool ShowSrchLabel;
        private System.Windows.Forms.Label TotNumTagsLabel;
        private System.Windows.Forms.Button ClearSelItemButton;
        private System.Windows.Forms.Button StopSrchButton;
        private string searchLocation;
        private string lastDisabledSearchTime;
        private string lastEnabledSearchTime;
        private string lastEnDisabledSearchTime;
        private bool startSearch;
        private GroupBox groupBox2;
        private RadioButton DisabledRadioButton;
        private RadioButton EnabledRadioButton;
        private Button SearchItemButton;
        private Button SearchAllButton;
        private RadioButton Dis_EnabledRadioButton;
        private ColumnHeader columnHeader3;
        private ToolBar toolBar1;
        private string serachTagStrID;
        private ImageList imageList1;
        private ToolBarButton ClearCheckBoxtToolBarButton;
        private ToolBarButton StopSearchingToolBarButton;
        private Label label1;
        private GroupBox DisabledGroupBox;
        private Label TotalDisabledNotResLabel;
        private Label label3;
        private Label TotalDisabledResLabel;
        private Label label7;
        private Label TotalDisabledTagsLabel;
        private Label label9;
        private GroupBox EnableDisableGroupBox;
        private Label NumDisabledNotFoundLabel;
        private Label label14;
        private Label NumDisabledTaglabel;
        private Label label16;
        private Label NumEnabledNotFoundLabel;
        private Label label5;
        private Label NumEnabledTaglabel;
        private Label label10;
        private Label NumTotalTagsLabel;
        private Label label12;
        private Label NumTotalDisabledTagsLabel;
        private Label label13;
        private Label NumTotalEnabledTagsLabel;
        private Label label8;
        private ListView INVDisabledListView;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ListView INVEnaDisabledListView;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader13;
        private ColumnHeader columnHeader14;
        private ColumnHeader columnHeader15;
        private ColumnHeader columnHeader16;
        private ColumnHeader columnHeader17;
        private ColumnHeader columnHeader18;
        private ColumnHeader columnHeader19;
        private ToolBarButton NewToolBarButton;
        private ToolBarButton EditToolBarButton;
        private ToolBarButton CancelToolBarButton;
        private ToolBarButton SaveToolBarButton;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ImageList SmallImageList;
        private GroupBox groupBox1;
        private ListView LocationListView;
        private ColumnHeader columnHeader23;
        private ColumnHeader columnHeader20;
        private ColumnHeader columnHeader21;
        private ColumnHeader columnHeader22;
        private Button SelectAllButton;
        private Button ClearChkButton;
        private Label SearchTimeLabel;
        private Label label2;
        private Label NumTagToSearchLabel;
        private Label label15;
        private IContainer components;
        //private Stopwatch watch = new Stopwatch();

        public INVForm()
        {
            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
        }

        public INVForm(MainForm main)
        {
            CheckForIllegalCrossThreadCalls = false;

            searchingAll = false;
            searchingItems = false;
            mForm = main;
            InitializeComponent();
            //numRdrSrch = 0;
            rdrSrchIndex = 0;
            tagSelIndex = 0;
            startSearch = false;
            lastEnDisabledSearchTime = "";
            lastDisabledSearchTime = "";
            lastEnDisabledSearchTime = "";

            communication = MainForm.communication;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

            m_connection = MainForm.m_connection;

            OdbcDbClass.NotifyDBConnectionStatusHandler += new NotifyDBConnectionStatus(DBConnectionStatusHandler);
            CommunicationClass.TagDetectedEventHandler += new TagDetectedEvent(TagDetected);
            CommunicationClass.TagDetectedRSSIEventHandler += new TagDetectedRSSIEvent(TagDetectedRSSI);
            CommunicationClass.TagDetectedSaniEventHandler += new TagDetectedSaniEvent(TagDetectedSani);
            //main.APIEventHandler.AppTagDetectedEvent += new AppTagDetectedEvent(TagDetected);
            //main.APIEventHandler.AppTagDetectedRSSIEventHandler += new AppTagDetectedRSSIEvent(TagDetectedRSSI);
            CommunicationClass.TagDetectedTamperEventHandler += new TagDetectedTamperEvent(TagDetected);
            CommunicationClass.TagDetectedRSSITamperEventHandler += new TagDetectedRSSITamperEvent(TagDetectedRSSI);
            CommunicationClass.QueryTagEventHandler += new QueryTagAckEvent(this.QueryTagEventNotify);
            MainForm.m_closeWindowEvent += new CloseWindowDelegate(this.CloseWindow);

            //if (Form1.m_connection == null)
            if (m_connection == null)
            {
                //DBStatusLabel.ForeColor = System.Drawing.Color.Red;
                //DBStatusLabel.Text = "Disconnected";

                SearchAllButton.Enabled = false;
                SearchItemButton.Enabled = false;
                RefreshListButton.Enabled = false;

                return;
            }
            //else
            //m_connection = Form1.m_connection;

            LoadSearchListView();
            //LoadRdrSearchList();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(INVForm));
            this.INVListView = new System.Windows.Forms.ListView();
            this.columnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader30 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader31 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader40 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EnabledGroupBox = new System.Windows.Forms.GroupBox();
            this.NumTagToSearchLabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.SearchTimeLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TotalEnabledNotResLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TotalEnabledResLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TotalEnabledTagsLabel = new System.Windows.Forms.Label();
            this.TotNumTagsLabel = new System.Windows.Forms.Label();
            this.SearchItemButton = new System.Windows.Forms.Button();
            this.SearchAllButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.RefreshListButton = new System.Windows.Forms.Button();
            this.SerachTimer = new System.Timers.Timer();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Timers.Timer();
            this.ClearSelItemButton = new System.Windows.Forms.Button();
            this.StopSrchButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Dis_EnabledRadioButton = new System.Windows.Forms.RadioButton();
            this.DisabledRadioButton = new System.Windows.Forms.RadioButton();
            this.EnabledRadioButton = new System.Windows.Forms.RadioButton();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.NewToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.EditToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.CancelToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.ClearCheckBoxtToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.StopSearchingToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.SaveToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.DisabledGroupBox = new System.Windows.Forms.GroupBox();
            this.TotalDisabledNotResLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TotalDisabledResLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TotalDisabledTagsLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.EnableDisableGroupBox = new System.Windows.Forms.GroupBox();
            this.NumTotalDisabledTagsLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.NumTotalEnabledTagsLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.NumDisabledNotFoundLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.NumDisabledTaglabel = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.NumEnabledNotFoundLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.NumEnabledTaglabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.NumTotalTagsLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.INVDisabledListView = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.INVEnaDisabledListView = new System.Windows.Forms.ListView();
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SmallImageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.ClearChkButton = new System.Windows.Forms.Button();
            this.LocationListView = new System.Windows.Forms.ListView();
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EnabledGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SerachTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.DisabledGroupBox.SuspendLayout();
            this.EnableDisableGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // INVListView
            // 
            this.INVListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.INVListView.BackColor = System.Drawing.Color.NavajoWhite;
            this.INVListView.CheckBoxes = true;
            this.INVListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader29,
            this.columnHeader30,
            this.columnHeader31,
            this.columnHeader2,
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader40});
            this.INVListView.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.INVListView.ForeColor = System.Drawing.Color.DarkBlue;
            this.INVListView.FullRowSelect = true;
            this.INVListView.GridLines = true;
            this.INVListView.HideSelection = false;
            this.INVListView.Location = new System.Drawing.Point(6, 74);
            this.INVListView.MultiSelect = false;
            this.INVListView.Name = "INVListView";
            this.INVListView.OwnerDraw = true;
            this.INVListView.Size = new System.Drawing.Size(852, 370);
            this.INVListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.INVListView.TabIndex = 13;
            this.INVListView.UseCompatibleStateImageBehavior = false;
            this.INVListView.View = System.Windows.Forms.View.Details;
            this.INVListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.INVListView_ColumnClick);
            this.INVListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.INVListView_DrawColumnHeader);
            this.INVListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.INVListView_DrawItem);
            this.INVListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.INVListView_DrawSubItem);
            this.INVListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.INVListView_ItemChecked);
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "";
            this.columnHeader29.Width = 30;
            // 
            // columnHeader30
            // 
            this.columnHeader30.Text = "Tag ID";
            this.columnHeader30.Width = 70;
            // 
            // columnHeader31
            // 
            this.columnHeader31.Text = "First Name";
            this.columnHeader31.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Last Name";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Found";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader1.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Location";
            this.columnHeader3.Width = 300;
            // 
            // columnHeader40
            // 
            this.columnHeader40.Text = "Type";
            this.columnHeader40.Width = 70;
            // 
            // EnabledGroupBox
            // 
            this.EnabledGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EnabledGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.EnabledGroupBox.Controls.Add(this.NumTagToSearchLabel);
            this.EnabledGroupBox.Controls.Add(this.label15);
            this.EnabledGroupBox.Controls.Add(this.SearchTimeLabel);
            this.EnabledGroupBox.Controls.Add(this.label2);
            this.EnabledGroupBox.Controls.Add(this.TotalEnabledNotResLabel);
            this.EnabledGroupBox.Controls.Add(this.label6);
            this.EnabledGroupBox.Controls.Add(this.TotalEnabledResLabel);
            this.EnabledGroupBox.Controls.Add(this.label4);
            this.EnabledGroupBox.Controls.Add(this.TotalEnabledTagsLabel);
            this.EnabledGroupBox.Controls.Add(this.TotNumTagsLabel);
            this.EnabledGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnabledGroupBox.Location = new System.Drawing.Point(6, 496);
            this.EnabledGroupBox.Name = "EnabledGroupBox";
            this.EnabledGroupBox.Size = new System.Drawing.Size(268, 210);
            this.EnabledGroupBox.TabIndex = 14;
            this.EnabledGroupBox.TabStop = false;
            this.EnabledGroupBox.Text = "Search Report";
            this.EnabledGroupBox.Enter += new System.EventHandler(this.EnabledGroupBox_Enter);
            // 
            // NumTagToSearchLabel
            // 
            this.NumTagToSearchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NumTagToSearchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumTagToSearchLabel.ForeColor = System.Drawing.Color.Black;
            this.NumTagToSearchLabel.Location = new System.Drawing.Point(222, 68);
            this.NumTagToSearchLabel.Name = "NumTagToSearchLabel";
            this.NumTagToSearchLabel.Size = new System.Drawing.Size(34, 26);
            this.NumTagToSearchLabel.TabIndex = 9;
            this.NumTagToSearchLabel.Text = "0";
            this.NumTagToSearchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Navy;
            this.label15.Location = new System.Drawing.Point(6, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(214, 26);
            this.label15.TabIndex = 8;
            this.label15.Text = "Total Number of  Tags: ";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SearchTimeLabel
            // 
            this.SearchTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SearchTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.SearchTimeLabel.Location = new System.Drawing.Point(222, 180);
            this.SearchTimeLabel.Name = "SearchTimeLabel";
            this.SearchTimeLabel.Size = new System.Drawing.Size(34, 24);
            this.SearchTimeLabel.TabIndex = 7;
            this.SearchTimeLabel.Text = "0";
            this.SearchTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(8, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "Search Time (sec): ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TotalEnabledNotResLabel
            // 
            this.TotalEnabledNotResLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalEnabledNotResLabel.ForeColor = System.Drawing.Color.Red;
            this.TotalEnabledNotResLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TotalEnabledNotResLabel.Location = new System.Drawing.Point(222, 144);
            this.TotalEnabledNotResLabel.Name = "TotalEnabledNotResLabel";
            this.TotalEnabledNotResLabel.Size = new System.Drawing.Size(34, 24);
            this.TotalEnabledNotResLabel.TabIndex = 5;
            this.TotalEnabledNotResLabel.Text = "0";
            this.TotalEnabledNotResLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TotalEnabledNotResLabel.Click += new System.EventHandler(this.TotalEnabledNotResLabel_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(8, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(212, 24);
            this.label6.TabIndex = 4;
            this.label6.Text = "Number of Tags Not Found: ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TotalEnabledResLabel
            // 
            this.TotalEnabledResLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TotalEnabledResLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalEnabledResLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.TotalEnabledResLabel.Location = new System.Drawing.Point(222, 106);
            this.TotalEnabledResLabel.Name = "TotalEnabledResLabel";
            this.TotalEnabledResLabel.Size = new System.Drawing.Size(34, 26);
            this.TotalEnabledResLabel.TabIndex = 3;
            this.TotalEnabledResLabel.Text = "0";
            this.TotalEnabledResLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(8, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 26);
            this.label4.TabIndex = 2;
            this.label4.Text = "Number of Tags Found: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TotalEnabledTagsLabel
            // 
            this.TotalEnabledTagsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TotalEnabledTagsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalEnabledTagsLabel.ForeColor = System.Drawing.Color.Black;
            this.TotalEnabledTagsLabel.Location = new System.Drawing.Point(222, 30);
            this.TotalEnabledTagsLabel.Name = "TotalEnabledTagsLabel";
            this.TotalEnabledTagsLabel.Size = new System.Drawing.Size(34, 26);
            this.TotalEnabledTagsLabel.TabIndex = 1;
            this.TotalEnabledTagsLabel.Text = "0";
            this.TotalEnabledTagsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TotNumTagsLabel
            // 
            this.TotNumTagsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TotNumTagsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotNumTagsLabel.ForeColor = System.Drawing.Color.Navy;
            this.TotNumTagsLabel.Location = new System.Drawing.Point(6, 68);
            this.TotNumTagsLabel.Name = "TotNumTagsLabel";
            this.TotNumTagsLabel.Size = new System.Drawing.Size(214, 26);
            this.TotNumTagsLabel.TabIndex = 0;
            this.TotNumTagsLabel.Text = "Number of  Tags To Search: ";
            this.TotNumTagsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SearchItemButton
            // 
            this.SearchItemButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SearchItemButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchItemButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchItemButton.Location = new System.Drawing.Point(486, 36);
            this.SearchItemButton.Name = "SearchItemButton";
            this.SearchItemButton.Size = new System.Drawing.Size(22, 14);
            this.SearchItemButton.TabIndex = 18;
            this.SearchItemButton.Text = "Specific Tags";
            this.SearchItemButton.Visible = false;
            this.SearchItemButton.Click += new System.EventHandler(this.SearchItemButton_Click);
            // 
            // SearchAllButton
            // 
            this.SearchAllButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SearchAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchAllButton.ForeColor = System.Drawing.Color.Blue;
            this.SearchAllButton.Location = new System.Drawing.Point(206, 34);
            this.SearchAllButton.Name = "SearchAllButton";
            this.SearchAllButton.Size = new System.Drawing.Size(16, 20);
            this.SearchAllButton.TabIndex = 17;
            this.SearchAllButton.Text = "All Tags";
            this.SearchAllButton.Visible = false;
            this.SearchAllButton.Click += new System.EventHandler(this.SearchAllButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.Black;
            this.CloseButton.Location = new System.Drawing.Point(-342, 32);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(18, 22);
            this.CloseButton.TabIndex = 18;
            this.CloseButton.Text = "Close";
            this.CloseButton.Visible = false;
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearButton.ForeColor = System.Drawing.Color.Black;
            this.ClearButton.Location = new System.Drawing.Point(-626, 32);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(18, 22);
            this.ClearButton.TabIndex = 17;
            this.ClearButton.Text = "Clear List";
            this.ClearButton.Visible = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // RefreshListButton
            // 
            this.RefreshListButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshListButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshListButton.Location = new System.Drawing.Point(528, 36);
            this.RefreshListButton.Name = "RefreshListButton";
            this.RefreshListButton.Size = new System.Drawing.Size(10, 16);
            this.RefreshListButton.TabIndex = 19;
            this.RefreshListButton.Text = "Refresh List";
            this.RefreshListButton.Visible = false;
            this.RefreshListButton.Click += new System.EventHandler(this.RefreshListButton_Click);
            // 
            // SerachTimer
            // 
            this.SerachTimer.Enabled = true;
            this.SerachTimer.Interval = 500D;
            this.SerachTimer.SynchronizingObject = this;
            this.SerachTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.SearchTimer_Elapsed);
            // 
            // SearchLabel
            // 
            this.SearchLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.SearchLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchLabel.Location = new System.Drawing.Point(100, 454);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(760, 32);
            this.SearchLabel.TabIndex = 20;
            this.SearchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 750D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // ClearSelItemButton
            // 
            this.ClearSelItemButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClearSelItemButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearSelItemButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearSelItemButton.ForeColor = System.Drawing.Color.Blue;
            this.ClearSelItemButton.Location = new System.Drawing.Point(216, 34);
            this.ClearSelItemButton.Name = "ClearSelItemButton";
            this.ClearSelItemButton.Size = new System.Drawing.Size(28, 20);
            this.ClearSelItemButton.TabIndex = 21;
            this.ClearSelItemButton.Text = "Clear All Checkboxes";
            this.ClearSelItemButton.Visible = false;
            this.ClearSelItemButton.Click += new System.EventHandler(this.ClearSelItemButton_Click);
            // 
            // StopSrchButton
            // 
            this.StopSrchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StopSrchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StopSrchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopSrchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopSrchButton.ForeColor = System.Drawing.Color.Blue;
            this.StopSrchButton.Location = new System.Drawing.Point(-322, 36);
            this.StopSrchButton.Name = "StopSrchButton";
            this.StopSrchButton.Size = new System.Drawing.Size(22, 16);
            this.StopSrchButton.TabIndex = 22;
            this.StopSrchButton.Text = "Stop Search";
            this.StopSrchButton.Visible = false;
            this.StopSrchButton.Click += new System.EventHandler(this.StopSrchButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Dis_EnabledRadioButton);
            this.groupBox2.Controls.Add(this.DisabledRadioButton);
            this.groupBox2.Controls.Add(this.StopSrchButton);
            this.groupBox2.Controls.Add(this.RefreshListButton);
            this.groupBox2.Controls.Add(this.EnabledRadioButton);
            this.groupBox2.Controls.Add(this.ClearButton);
            this.groupBox2.Controls.Add(this.SearchItemButton);
            this.groupBox2.Controls.Add(this.ClearSelItemButton);
            this.groupBox2.Controls.Add(this.SearchAllButton);
            this.groupBox2.Controls.Add(this.CloseButton);
            this.groupBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox2.Location = new System.Drawing.Point(836, 482);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(24, 20);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Options";
            this.groupBox2.Visible = false;
            // 
            // Dis_EnabledRadioButton
            // 
            this.Dis_EnabledRadioButton.AutoSize = true;
            this.Dis_EnabledRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Dis_EnabledRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Dis_EnabledRadioButton.Location = new System.Drawing.Point(592, 30);
            this.Dis_EnabledRadioButton.Name = "Dis_EnabledRadioButton";
            this.Dis_EnabledRadioButton.Size = new System.Drawing.Size(222, 24);
            this.Dis_EnabledRadioButton.TabIndex = 21;
            this.Dis_EnabledRadioButton.TabStop = true;
            this.Dis_EnabledRadioButton.Text = "Enabled / Disabled Tags";
            this.Dis_EnabledRadioButton.UseVisualStyleBackColor = true;
            this.Dis_EnabledRadioButton.Click += new System.EventHandler(this.Dis_EnabledRadioButton_Click);
            // 
            // DisabledRadioButton
            // 
            this.DisabledRadioButton.AutoSize = true;
            this.DisabledRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DisabledRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.DisabledRadioButton.Location = new System.Drawing.Point(312, 30);
            this.DisabledRadioButton.Name = "DisabledRadioButton";
            this.DisabledRadioButton.Size = new System.Drawing.Size(141, 24);
            this.DisabledRadioButton.TabIndex = 20;
            this.DisabledRadioButton.TabStop = true;
            this.DisabledRadioButton.Text = "Disabled Tags";
            this.DisabledRadioButton.UseVisualStyleBackColor = true;
            this.DisabledRadioButton.Click += new System.EventHandler(this.DisabledRadioButton_Click);
            // 
            // EnabledRadioButton
            // 
            this.EnabledRadioButton.AutoSize = true;
            this.EnabledRadioButton.Checked = true;
            this.EnabledRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EnabledRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.EnabledRadioButton.Location = new System.Drawing.Point(34, 30);
            this.EnabledRadioButton.Name = "EnabledRadioButton";
            this.EnabledRadioButton.Size = new System.Drawing.Size(137, 24);
            this.EnabledRadioButton.TabIndex = 19;
            this.EnabledRadioButton.TabStop = true;
            this.EnabledRadioButton.Text = "Enabled Tags";
            this.EnabledRadioButton.UseVisualStyleBackColor = true;
            this.EnabledRadioButton.Click += new System.EventHandler(this.EnabledRadioButton_Click);
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
            this.ClearCheckBoxtToolBarButton,
            this.StopSearchingToolBarButton,
            this.SaveToolBarButton});
            this.toolBar1.ButtonSize = new System.Drawing.Size(45, 45);
            this.toolBar1.CausesValidation = false;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(866, 68);
            this.toolBar1.TabIndex = 24;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // NewToolBarButton
            // 
            this.NewToolBarButton.ImageIndex = 0;
            this.NewToolBarButton.Name = "NewToolBarButton";
            this.NewToolBarButton.Text = "Search All";
            this.NewToolBarButton.ToolTipText = "Search All Tags";
            // 
            // EditToolBarButton
            // 
            this.EditToolBarButton.ImageIndex = 1;
            this.EditToolBarButton.Name = "EditToolBarButton";
            this.EditToolBarButton.Text = "Search Specific";
            this.EditToolBarButton.ToolTipText = "Search Specific Tag";
            // 
            // CancelToolBarButton
            // 
            this.CancelToolBarButton.ImageIndex = 2;
            this.CancelToolBarButton.Name = "CancelToolBarButton";
            this.CancelToolBarButton.Text = "Clear Search";
            this.CancelToolBarButton.ToolTipText = "Clear Search List";
            // 
            // ClearCheckBoxtToolBarButton
            // 
            this.ClearCheckBoxtToolBarButton.ImageIndex = 3;
            this.ClearCheckBoxtToolBarButton.Name = "ClearCheckBoxtToolBarButton";
            this.ClearCheckBoxtToolBarButton.Text = "Clear Checks";
            this.ClearCheckBoxtToolBarButton.ToolTipText = "Clear Checks";
            // 
            // StopSearchingToolBarButton
            // 
            this.StopSearchingToolBarButton.ImageIndex = 4;
            this.StopSearchingToolBarButton.Name = "StopSearchingToolBarButton";
            this.StopSearchingToolBarButton.Text = "Stop Searching";
            this.StopSearchingToolBarButton.ToolTipText = "Stop Searching";
            // 
            // SaveToolBarButton
            // 
            this.SaveToolBarButton.ImageIndex = 5;
            this.SaveToolBarButton.Name = "SaveToolBarButton";
            this.SaveToolBarButton.Text = "Close";
            this.SaveToolBarButton.ToolTipText = "Close Dialog";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "ClrCheckBx.jpg");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "1156.ico");
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(6, 456);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 30);
            this.label1.TabIndex = 25;
            this.label1.Text = "Messages: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DisabledGroupBox
            // 
            this.DisabledGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DisabledGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.DisabledGroupBox.Controls.Add(this.TotalDisabledNotResLabel);
            this.DisabledGroupBox.Controls.Add(this.label3);
            this.DisabledGroupBox.Controls.Add(this.TotalDisabledResLabel);
            this.DisabledGroupBox.Controls.Add(this.label7);
            this.DisabledGroupBox.Controls.Add(this.TotalDisabledTagsLabel);
            this.DisabledGroupBox.Controls.Add(this.label9);
            this.DisabledGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisabledGroupBox.Location = new System.Drawing.Point(0, 74);
            this.DisabledGroupBox.Name = "DisabledGroupBox";
            this.DisabledGroupBox.Size = new System.Drawing.Size(760, 158);
            this.DisabledGroupBox.TabIndex = 26;
            this.DisabledGroupBox.TabStop = false;
            this.DisabledGroupBox.Text = "Inventory Report";
            this.DisabledGroupBox.Visible = false;
            // 
            // TotalDisabledNotResLabel
            // 
            this.TotalDisabledNotResLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TotalDisabledNotResLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalDisabledNotResLabel.ForeColor = System.Drawing.Color.Red;
            this.TotalDisabledNotResLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TotalDisabledNotResLabel.Location = new System.Drawing.Point(276, 118);
            this.TotalDisabledNotResLabel.Name = "TotalDisabledNotResLabel";
            this.TotalDisabledNotResLabel.Size = new System.Drawing.Size(34, 24);
            this.TotalDisabledNotResLabel.TabIndex = 5;
            this.TotalDisabledNotResLabel.Text = "0";
            this.TotalDisabledNotResLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(12, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(262, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Num Disabled Tags Not Found: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TotalDisabledResLabel
            // 
            this.TotalDisabledResLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TotalDisabledResLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalDisabledResLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.TotalDisabledResLabel.Location = new System.Drawing.Point(276, 78);
            this.TotalDisabledResLabel.Name = "TotalDisabledResLabel";
            this.TotalDisabledResLabel.Size = new System.Drawing.Size(34, 24);
            this.TotalDisabledResLabel.TabIndex = 3;
            this.TotalDisabledResLabel.Text = "0";
            this.TotalDisabledResLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(12, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 24);
            this.label7.TabIndex = 2;
            this.label7.Text = "Num Disabled Tags Found: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TotalDisabledTagsLabel
            // 
            this.TotalDisabledTagsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TotalDisabledTagsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalDisabledTagsLabel.ForeColor = System.Drawing.Color.Black;
            this.TotalDisabledTagsLabel.Location = new System.Drawing.Point(274, 38);
            this.TotalDisabledTagsLabel.Name = "TotalDisabledTagsLabel";
            this.TotalDisabledTagsLabel.Size = new System.Drawing.Size(34, 24);
            this.TotalDisabledTagsLabel.TabIndex = 1;
            this.TotalDisabledTagsLabel.Text = "0";
            this.TotalDisabledTagsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Maroon;
            this.label9.Location = new System.Drawing.Point(12, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(260, 24);
            this.label9.TabIndex = 0;
            this.label9.Text = "Total Num Disabled Tags: ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EnableDisableGroupBox
            // 
            this.EnableDisableGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EnableDisableGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.EnableDisableGroupBox.Controls.Add(this.NumTotalDisabledTagsLabel);
            this.EnableDisableGroupBox.Controls.Add(this.label13);
            this.EnableDisableGroupBox.Controls.Add(this.NumTotalEnabledTagsLabel);
            this.EnableDisableGroupBox.Controls.Add(this.label8);
            this.EnableDisableGroupBox.Controls.Add(this.NumDisabledNotFoundLabel);
            this.EnableDisableGroupBox.Controls.Add(this.label14);
            this.EnableDisableGroupBox.Controls.Add(this.NumDisabledTaglabel);
            this.EnableDisableGroupBox.Controls.Add(this.label16);
            this.EnableDisableGroupBox.Controls.Add(this.NumEnabledNotFoundLabel);
            this.EnableDisableGroupBox.Controls.Add(this.label5);
            this.EnableDisableGroupBox.Controls.Add(this.NumEnabledTaglabel);
            this.EnableDisableGroupBox.Controls.Add(this.label10);
            this.EnableDisableGroupBox.Controls.Add(this.NumTotalTagsLabel);
            this.EnableDisableGroupBox.Controls.Add(this.label12);
            this.EnableDisableGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnableDisableGroupBox.Location = new System.Drawing.Point(0, 252);
            this.EnableDisableGroupBox.Name = "EnableDisableGroupBox";
            this.EnableDisableGroupBox.Size = new System.Drawing.Size(712, 158);
            this.EnableDisableGroupBox.TabIndex = 27;
            this.EnableDisableGroupBox.TabStop = false;
            this.EnableDisableGroupBox.Text = "Inventory Report";
            this.EnableDisableGroupBox.Visible = false;
            // 
            // NumTotalDisabledTagsLabel
            // 
            this.NumTotalDisabledTagsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumTotalDisabledTagsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumTotalDisabledTagsLabel.ForeColor = System.Drawing.Color.Black;
            this.NumTotalDisabledTagsLabel.Location = new System.Drawing.Point(798, 62);
            this.NumTotalDisabledTagsLabel.Name = "NumTotalDisabledTagsLabel";
            this.NumTotalDisabledTagsLabel.Size = new System.Drawing.Size(576, 24);
            this.NumTotalDisabledTagsLabel.TabIndex = 13;
            this.NumTotalDisabledTagsLabel.Text = "0";
            this.NumTotalDisabledTagsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Maroon;
            this.label13.Location = new System.Drawing.Point(28, 62);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(282, 24);
            this.label13.TabIndex = 12;
            this.label13.Text = "Total Number of Disabled Tags: ";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NumTotalEnabledTagsLabel
            // 
            this.NumTotalEnabledTagsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumTotalEnabledTagsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumTotalEnabledTagsLabel.ForeColor = System.Drawing.Color.Black;
            this.NumTotalEnabledTagsLabel.Location = new System.Drawing.Point(314, 62);
            this.NumTotalEnabledTagsLabel.Name = "NumTotalEnabledTagsLabel";
            this.NumTotalEnabledTagsLabel.Size = new System.Drawing.Size(576, 24);
            this.NumTotalEnabledTagsLabel.TabIndex = 11;
            this.NumTotalEnabledTagsLabel.Text = "0";
            this.NumTotalEnabledTagsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Maroon;
            this.label8.Location = new System.Drawing.Point(440, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 24);
            this.label8.TabIndex = 10;
            this.label8.Text = "Total Number of Enabled Tags: ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NumDisabledNotFoundLabel
            // 
            this.NumDisabledNotFoundLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumDisabledNotFoundLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumDisabledNotFoundLabel.ForeColor = System.Drawing.Color.Red;
            this.NumDisabledNotFoundLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NumDisabledNotFoundLabel.Location = new System.Drawing.Point(660, 126);
            this.NumDisabledNotFoundLabel.Name = "NumDisabledNotFoundLabel";
            this.NumDisabledNotFoundLabel.Size = new System.Drawing.Size(34, 24);
            this.NumDisabledNotFoundLabel.TabIndex = 9;
            this.NumDisabledNotFoundLabel.Text = "0";
            this.NumDisabledNotFoundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Maroon;
            this.label14.Location = new System.Drawing.Point(344, 126);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(314, 24);
            this.label14.TabIndex = 8;
            this.label14.Text = "Number of Disabled Tags Not Found: ";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NumDisabledTaglabel
            // 
            this.NumDisabledTaglabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumDisabledTaglabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumDisabledTaglabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.NumDisabledTaglabel.Location = new System.Drawing.Point(660, 94);
            this.NumDisabledTaglabel.Name = "NumDisabledTaglabel";
            this.NumDisabledTaglabel.Size = new System.Drawing.Size(34, 24);
            this.NumDisabledTaglabel.TabIndex = 7;
            this.NumDisabledTaglabel.Text = "0";
            this.NumDisabledTaglabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Maroon;
            this.label16.Location = new System.Drawing.Point(344, 94);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(314, 24);
            this.label16.TabIndex = 6;
            this.label16.Text = "Number of  Disabled Tags Found: ";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NumEnabledNotFoundLabel
            // 
            this.NumEnabledNotFoundLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NumEnabledNotFoundLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumEnabledNotFoundLabel.ForeColor = System.Drawing.Color.Red;
            this.NumEnabledNotFoundLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NumEnabledNotFoundLabel.Location = new System.Drawing.Point(314, 126);
            this.NumEnabledNotFoundLabel.Name = "NumEnabledNotFoundLabel";
            this.NumEnabledNotFoundLabel.Size = new System.Drawing.Size(34, 24);
            this.NumEnabledNotFoundLabel.TabIndex = 5;
            this.NumEnabledNotFoundLabel.Text = "0";
            this.NumEnabledNotFoundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(14, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(298, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "Number of  Enabled Tags Not Found: ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NumEnabledTaglabel
            // 
            this.NumEnabledTaglabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NumEnabledTaglabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumEnabledTaglabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.NumEnabledTaglabel.Location = new System.Drawing.Point(314, 94);
            this.NumEnabledTaglabel.Name = "NumEnabledTaglabel";
            this.NumEnabledTaglabel.Size = new System.Drawing.Size(34, 24);
            this.NumEnabledTaglabel.TabIndex = 3;
            this.NumEnabledTaglabel.Text = "0";
            this.NumEnabledTaglabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Maroon;
            this.label10.Location = new System.Drawing.Point(14, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(298, 24);
            this.label10.TabIndex = 2;
            this.label10.Text = "Number of Enabled Tags Found: ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NumTotalTagsLabel
            // 
            this.NumTotalTagsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumTotalTagsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumTotalTagsLabel.ForeColor = System.Drawing.Color.Black;
            this.NumTotalTagsLabel.Location = new System.Drawing.Point(616, 24);
            this.NumTotalTagsLabel.Name = "NumTotalTagsLabel";
            this.NumTotalTagsLabel.Size = new System.Drawing.Size(28, 24);
            this.NumTotalTagsLabel.TabIndex = 1;
            this.NumTotalTagsLabel.Text = "0";
            this.NumTotalTagsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Maroon;
            this.label12.Location = new System.Drawing.Point(26, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(576, 24);
            this.label12.TabIndex = 0;
            this.label12.Text = "Total Number of Tags: ";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // INVDisabledListView
            // 
            this.INVDisabledListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.INVDisabledListView.BackColor = System.Drawing.Color.White;
            this.INVDisabledListView.CheckBoxes = true;
            this.INVDisabledListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.INVDisabledListView.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.INVDisabledListView.ForeColor = System.Drawing.Color.DarkBlue;
            this.INVDisabledListView.FullRowSelect = true;
            this.INVDisabledListView.GridLines = true;
            this.INVDisabledListView.HideSelection = false;
            this.INVDisabledListView.Location = new System.Drawing.Point(6, 74);
            this.INVDisabledListView.MultiSelect = false;
            this.INVDisabledListView.Name = "INVDisabledListView";
            this.INVDisabledListView.OwnerDraw = true;
            this.INVDisabledListView.Size = new System.Drawing.Size(852, 328);
            this.INVDisabledListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.INVDisabledListView.TabIndex = 28;
            this.INVDisabledListView.UseCompatibleStateImageBehavior = false;
            this.INVDisabledListView.View = System.Windows.Forms.View.Details;
            this.INVDisabledListView.Visible = false;
            this.INVDisabledListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.INVDisabledListView_ColumnClick);
            this.INVDisabledListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.INVDisabledListView_DrawColumnHeader);
            this.INVDisabledListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.INVDisabledListView_DrawItem);
            this.INVDisabledListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.INVDisabledListView_DrawSubItem);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "";
            this.columnHeader4.Width = 30;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Tag ID";
            this.columnHeader5.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Location";
            this.columnHeader6.Width = 240;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Found";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 70;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Status";
            this.columnHeader8.Width = 90;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Battery";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 70;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Item Description";
            this.columnHeader10.Width = 250;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Last Detected";
            this.columnHeader11.Width = 170;
            // 
            // INVEnaDisabledListView
            // 
            this.INVEnaDisabledListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.INVEnaDisabledListView.BackColor = System.Drawing.Color.White;
            this.INVEnaDisabledListView.CheckBoxes = true;
            this.INVEnaDisabledListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19});
            this.INVEnaDisabledListView.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.INVEnaDisabledListView.ForeColor = System.Drawing.Color.DarkBlue;
            this.INVEnaDisabledListView.FullRowSelect = true;
            this.INVEnaDisabledListView.GridLines = true;
            this.INVEnaDisabledListView.HideSelection = false;
            this.INVEnaDisabledListView.Location = new System.Drawing.Point(6, 76);
            this.INVEnaDisabledListView.MultiSelect = false;
            this.INVEnaDisabledListView.Name = "INVEnaDisabledListView";
            this.INVEnaDisabledListView.OwnerDraw = true;
            this.INVEnaDisabledListView.Size = new System.Drawing.Size(852, 364);
            this.INVEnaDisabledListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.INVEnaDisabledListView.TabIndex = 29;
            this.INVEnaDisabledListView.UseCompatibleStateImageBehavior = false;
            this.INVEnaDisabledListView.View = System.Windows.Forms.View.Details;
            this.INVEnaDisabledListView.Visible = false;
            this.INVEnaDisabledListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.INVEnaDisabledListView_ColumnClick);
            this.INVEnaDisabledListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.INVEnaDisabledListView_DrawColumnHeader);
            this.INVEnaDisabledListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.INVEnaDisabledListView_DrawItem);
            this.INVEnaDisabledListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.INVEnaDisabledListView_DrawSubItem);
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "";
            this.columnHeader12.Width = 30;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Tag ID";
            this.columnHeader13.Width = 70;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Location";
            this.columnHeader14.Width = 240;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Found";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader15.Width = 70;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Status";
            this.columnHeader16.Width = 90;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Battery";
            this.columnHeader17.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader17.Width = 70;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "Item Description";
            this.columnHeader18.Width = 250;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "Last Detected";
            this.columnHeader19.Width = 170;
            // 
            // SmallImageList
            // 
            this.SmallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SmallImageList.ImageStream")));
            this.SmallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.SmallImageList.Images.SetKeyName(0, "Reader2.ico");
            this.SmallImageList.Images.SetKeyName(1, "Reader2X.ico");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SelectAllButton);
            this.groupBox1.Controls.Add(this.ClearChkButton);
            this.groupBox1.Controls.Add(this.LocationListView);
            this.groupBox1.Location = new System.Drawing.Point(288, 496);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 210);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Location Config";
            // 
            // SelectAllButton
            // 
            this.SelectAllButton.BackColor = System.Drawing.Color.LightSteelBlue;
            this.SelectAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAllButton.ForeColor = System.Drawing.Color.Navy;
            this.SelectAllButton.Location = new System.Drawing.Point(484, 106);
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.Size = new System.Drawing.Size(75, 28);
            this.SelectAllButton.TabIndex = 33;
            this.SelectAllButton.Text = "Select  All";
            this.SelectAllButton.UseVisualStyleBackColor = false;
            this.SelectAllButton.Click += new System.EventHandler(this.SelectAllButton_Click);
            // 
            // ClearChkButton
            // 
            this.ClearChkButton.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClearChkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearChkButton.ForeColor = System.Drawing.Color.Navy;
            this.ClearChkButton.Location = new System.Drawing.Point(484, 60);
            this.ClearChkButton.Name = "ClearChkButton";
            this.ClearChkButton.Size = new System.Drawing.Size(75, 28);
            this.ClearChkButton.TabIndex = 32;
            this.ClearChkButton.Text = "Clear";
            this.ClearChkButton.UseVisualStyleBackColor = false;
            this.ClearChkButton.Click += new System.EventHandler(this.ClearChkButton_Click);
            // 
            // LocationListView
            // 
            this.LocationListView.BackColor = System.Drawing.Color.LightSteelBlue;
            this.LocationListView.CheckBoxes = true;
            this.LocationListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader23,
            this.columnHeader20,
            this.columnHeader21,
            this.columnHeader22});
            this.LocationListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationListView.FullRowSelect = true;
            this.LocationListView.GridLines = true;
            this.LocationListView.Location = new System.Drawing.Point(12, 38);
            this.LocationListView.MultiSelect = false;
            this.LocationListView.Name = "LocationListView";
            this.LocationListView.OwnerDraw = true;
            this.LocationListView.Size = new System.Drawing.Size(458, 160);
            this.LocationListView.SmallImageList = this.SmallImageList;
            this.LocationListView.TabIndex = 31;
            this.LocationListView.UseCompatibleStateImageBehavior = false;
            this.LocationListView.View = System.Windows.Forms.View.Details;
            this.LocationListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.LocationListView_DrawColumnHeader);
            this.LocationListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.LocationListView_DrawSubItem);
            this.LocationListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LocationListView_ItemCheck);
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "Chk";
            this.columnHeader23.Width = 30;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "Stat";
            this.columnHeader20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader20.Width = 32;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "Location";
            this.columnHeader21.Width = 300;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "Reader";
            this.columnHeader22.Width = 90;
            // 
            // INVForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(866, 713);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.INVListView);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.INVDisabledListView);
            this.Controls.Add(this.EnabledGroupBox);
            this.Controls.Add(this.INVEnaDisabledListView);
            this.Controls.Add(this.EnableDisableGroupBox);
            this.Controls.Add(this.DisabledGroupBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Blue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "INVForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.INVForm_FormClosing);
            this.Load += new System.EventHandler(this.INVForm_Load);
            this.EnabledGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SerachTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.DisabledGroupBox.ResumeLayout(false);
            this.EnableDisableGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region CloseWindow()
        private void CloseWindow()
        {
            Close();
        }
        #endregion

        #region DBConnectionStatusHandler()
        private void DBConnectionStatusHandler(status stat, OdbcConnection connect)
        {
            if (stat == status.open)
            {
                m_connection = connect;

                //Form1.reconnectCounter = -1;
                //timer1.Enabled = false;
                //DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
                //DBStatusLabel.Text = "Connected";
                //toolBar1.Enabled = true;
            }
            else if (stat == status.broken)
            {
                m_connection = null;
            }
            else if (stat == status.close)
            {
                m_connection = null;
            }
        }
        #endregion

        #region LoadRdrSearchListView()  
        private void LoadRdrSearchListView()
        {
            string sql = "SELECT Status, Location, ReaderID FROM zones WHERE FieldGenID = '0'";

            OdbcDataReader myReader;

            if (!mForm.RunQueryCmd(sql, out myReader))
            {
                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                        myReader.Close();
                }

                return;
            }
            while (myReader.Read())
            {
                ListViewItem listItem = new ListViewItem();
                listItem.Checked = false;  //checkbox
                try
                {
                    if (myReader.GetString(0) == "Offline")
                        listItem.SubItems.Add("F");  //stat
                    else if (myReader.GetString(0) == "Online")
                        listItem.SubItems.Add("N");  //stat
                    else
                        listItem.SubItems.Add("");  //stat
                }
                catch
                {
                    listItem.SubItems.Add("");  //stat
                }

                try
                {
                    listItem.SubItems.Add(myReader.GetString(1));  //location
                }
                catch
                {
                    listItem.SubItems.Add("");  //loc
                }

                listItem.SubItems.Add(myReader.GetString(2));  //Rdr ID

                LocationListView.Items.Add(listItem);

            }//while
            myReader.Close();
        }
        #endregion        

        #region LoadRdrSearchList()  (OLD CODE)
        /*private void LoadRdrSearchList()
        {
            if (MainForm.m_connection == null) return;

            lock (MainForm.m_connection)
            {
                //string sql = "SELECT ReaderID FROM zones WHERE INVReader = '1' AND FieldGenID = '0'";
                string sql = "SELECT ReaderID FROM zones";
                
                //>>OdbcCommand myCommand = new OdbcCommand(sql, m_connection); 

                OdbcDataReader myReader = null;

                if (!mForm.RunQueryCmd(sql, ref myReader))
                {
                    if (myReader != null)
                    {
                        if (!myReader.IsClosed)
                            myReader.Close();
                    }

                    return;
                }
                
                while (myReader.Read())
                {
                    readerSearchStruct rdrSrch = new readerSearchStruct();
                    rdrSrch.readerID = Convert.ToUInt16(myReader.GetInt16(0)); //rdrid
                    rdrSrch.fgenID = 0;
                    rdrSrch.count = 0;
                    rdrSrchList.Add(rdrSrch);
                }//while
                myReader.Close();
            }//m_connection
        }*/
        #endregion        

        #region LoadRdrSearchList()
        private void LoadRdrSearchList()
        {
            rdrSrchList.Clear();

            for (int i=0; i<LocationListView.Items.Count; i++)
            {
                if (LocationListView.Items[i].Checked)
                {
                    readerSearchStruct rdrSrch = new readerSearchStruct();
                    rdrSrch.readerID = Convert.ToUInt16(LocationListView.Items[i].SubItems[3].Text);
                    rdrSrch.fgenID = 0;
                    rdrSrch.count = 0;
                    rdrSrchList.Add(rdrSrch);
                }
            }                         
        }
        #endregion

        #region LoadInventoryListView(ushort stat)
        private void LoadInventoryListView(ushort stat)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT sku.TagID, zones.Location, Sku.Status, Sku.BatteryStatus, Sku.Description, Sku.TimeStamp FROM sku ");
            if (stat == 0x01)   //enabled
                sql.Append("LEFT JOIN zones ON sku.ZoneID = zones.ID WHERE sku.Status = 'ENABLED' OR sku.Status = 'Enabled'");
            else if (stat == 0x02)   //disabled
                sql.Append("LEFT JOIN zones ON sku.ZoneID = zones.ID WHERE sku.Status = 'DISABLED' OR sku.Status = 'Disabled'");
            else
                sql.Append("LEFT JOIN zones ON sku.ZoneID = zones.ID");  //enabled / disabled


            //>>>>OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection); 

            OdbcDataReader myReader;

            if (!mForm.RunQueryCmd(sql.ToString(), out myReader))
            {
                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                        myReader.Close();
                }

                return;
            }
            /*try
            {
                myReader = myCommand.ExecuteReader();
                //Form1.reconnectCounter = -1;
                //timer1.Enabled = false;
            }
            catch (Exception ex)
            {
                int ret = 0, ret1 = 0;
                if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) || 
                    ((ret1=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                {  
                    //error code 2013
                    //DBStatusLabel.ForeColor = System.Drawing.Color.Red;
                    //DBStatusLabel.Text = "Disconnected";
                    //toolBar1.Enabled = false;
						
                    if (Form1.reconnectCounter < 0)
                    {
                        //Form1.reconnectCounter = 0;
                        //timer1.Enabled = true;
                    }                                
                }

                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                        myReader.Close();
                }
                return;
            }//catch .. try
            */

            int count = 0;
            int eCount = 0;
            int dCount = 0;
            string tagid = "";
            string bat = "";
            while (myReader.Read())
            {
                count += 1;
                ListViewItem listItem = new ListViewItem();
                listItem.Checked = false;  //checkbox
                tagid = myReader.GetString(0);
                tagSearchStruct tagSrch = new tagSearchStruct();
                tagSrch.tagID = tagid;
                tagSrch.found = false;
                tagSrchList.Add(tagSrch);
                listItem.SubItems.Add(tagid);  //tag ID
                try
                {
                    if (!myReader.IsDBNull(1))
                        listItem.SubItems.Add(myReader.GetString(1));  //location
                    else
                        listItem.SubItems.Add("");
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                listItem.SubItems.Add("");  //found

                try
                {
                    string s = myReader.GetString(2);
                    if ((s == "ENABLED") || (s == "Enabled"))
                    {
                        s = "Enabled";
                        eCount += 1;
                    }
                    else if ((s == "DISABLED") || (s == "Disabled"))
                    {
                        s = "Disabled";
                        dCount += 1;
                    }
                    listItem.SubItems.Add(s);  //status
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                //listItem.SubItems.Add("");  //status
                //listItem.SubItems.Add("");  //battery

                try
                {
                    bat = myReader.GetString(3);  //battery
                }
                catch
                {
                    bat = "";
                }
                if (bat == "1")  //OK
                    listItem.SubItems.Add("OK");
                else if (bat == "2")  //Low
                    listItem.SubItems.Add("Low");
                else
                    listItem.SubItems.Add(""); //not known

                try
                {
                    listItem.SubItems.Add(myReader.GetString(4));  //description
                }
                catch
                {
                    listItem.SubItems.Add("");
                }


                try
                {
                    listItem.SubItems.Add(myReader.GetDateTime(5).ToString("MM-dd-yyyy  HH:mm:ss"));  //time stamp
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                //listItem.ForeColor = System.Drawing.Color.DarkBlue;
                //listItem.Selected = true;
                listItem.EnsureVisible();

                if (stat == 1)
                    INVListView.Items.Add(listItem);
                else if (stat == 2)
                    INVDisabledListView.Items.Add(listItem);
                else
                    INVEnaDisabledListView.Items.Add(listItem);

            }//while
            myReader.Close();

            if (stat == 1)
                TotalEnabledTagsLabel.Text = Convert.ToString(count);
            else if (stat == 2)
                TotalDisabledTagsLabel.Text = Convert.ToString(count);
            else if (stat == 3)
            {
                NumTotalTagsLabel.Text = Convert.ToString(count);
                NumTotalEnabledTagsLabel.Text = Convert.ToString(eCount);
                NumTotalDisabledTagsLabel.Text = Convert.ToString(dCount);
            }

            //TotalEnabledTagsLabel.Text = Convert.ToString(count);
        }
        #endregion

        #region LoadSearchListView()
        private void LoadSearchListView()
        {
            int count = 0;
            string tagid = "";

            //load the listview with the data
            OdbcDataReader myReader;

            if (!mForm.RunQueryCmd("SELECT ID, FirstName, LastName, AccType From employees", out myReader))
            {
                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                        myReader.Close();
                }

                return;
            }
            while (myReader.Read())
            {
                count += 1;
                ListViewItem listItem = new ListViewItem();
                listItem.Checked = false;  //checkbox
                tagid = myReader.GetString(0);
                tagSearchStruct tagSrch = new tagSearchStruct();
                tagSrch.tagID = tagid;
                tagSrch.found = false;
                tagSrchList.Add(tagSrch);
                listItem.SubItems.Add(tagid);  //tag ID
                try
                {
                    if (!myReader.IsDBNull(1))
                        listItem.SubItems.Add(myReader.GetString(1));  //fname
                    else
                        listItem.SubItems.Add("");
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                try
                {
                    if (!myReader.IsDBNull(2))
                        listItem.SubItems.Add(myReader.GetString(2));  //lname
                    else
                        listItem.SubItems.Add("");
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                listItem.SubItems.Add("");  //found
                listItem.SubItems.Add("");  //location

                try
                {
                    String temp = myReader.GetString(3);
                    if (String.Compare(temp, "1") == 0)
                        listItem.SubItems.Add("Employee");
                    else if (String.Compare(temp, "3") == 0)
                        listItem.SubItems.Add("Visitor");
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                listItem.EnsureVisible();
                INVListView.Items.Add(listItem);

            }//while

            myReader.Close();

            if (!mForm.RunQueryCmd("SELECT ID, Name From asset", out myReader))
            {
                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                        myReader.Close();
                }

                return;
            }
            while (myReader.Read())
            {
                count += 1;
                ListViewItem listItem = new ListViewItem();
                listItem.Checked = false;  //checkbox
                tagid = myReader.GetString(0);
                tagSearchStruct tagSrch = new tagSearchStruct();
                tagSrch.tagID = tagid;
                tagSrch.found = false;
                tagSrchList.Add(tagSrch);
                listItem.SubItems.Add(tagid);  //tag ID
                try
                {
                    if (!myReader.IsDBNull(1))
                        listItem.SubItems.Add(myReader.GetString(1));  //fname
                    else
                        listItem.SubItems.Add("");
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                try
                {
                    if (!myReader.IsDBNull(2))
                        listItem.SubItems.Add(myReader.GetString(2));  //lname
                    else
                        listItem.SubItems.Add("");
                }
                catch
                {
                    listItem.SubItems.Add("");
                }
                listItem.SubItems.Add("");  //found
                listItem.SubItems.Add("");  //location
                listItem.SubItems.Add("Asset"); //Type

                listItem.EnsureVisible();
                INVListView.Items.Add(listItem);

            }//while

            myReader.Close();

            TotalEnabledTagsLabel.Text = Convert.ToString(count);

            //TotalEnabledTagsLabel.Text = Convert.ToString(count);
        }
        #endregion

        #region IsTagInSystem()
        private bool IsTagInSystem(string tagid)
        {
            string bat = "";

            StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT TagID FROM sku WHERE TagID = ");
            sql.Append("SELECT zones.Location, Sku.Status, Sku.BatteryStatus, Sku.Description, Sku.TimeStamp FROM sku, zones WHERE sku.TagID = ");
            sql.AppendFormat("'{0}'", tagid);

            OdbcDataReader myReader;

            if (!mForm.RunQueryCmd(sql.ToString(), out myReader))
            {
                if (myReader.HasRows)
                {
                    myReader.Read();

                    ListViewItem listItem = new ListViewItem();
                    listItem.Checked = false;  //checkbox                            
                    listItem.SubItems.Add(tagid);  //tag ID
                    try
                    {
                        if (!myReader.IsDBNull(0))
                            listItem.SubItems.Add(myReader.GetString(0));  //location
                        else
                            listItem.SubItems.Add("");
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    listItem.SubItems.Add("");  //found

                    try
                    {
                        string s = myReader.GetString(1);
                        if ((s == "ENABLED") || (s == "Enabled"))
                        {
                            s = "Enabled **";
                        }
                        else if ((s == "DISABLED") || (s == "Disabled"))
                        {
                            s = "Disabled **";
                        }
                        listItem.SubItems.Add(s);  //status
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    try
                    {
                        bat = myReader.GetString(2);  //battery
                    }
                    catch
                    {
                        bat = "";
                    }
                    if (bat == "1")  //OK
                        listItem.SubItems.Add("OK");
                    else if (bat == "2")  //Low
                        listItem.SubItems.Add("Low");
                    else
                        listItem.SubItems.Add(""); //not known

                    try
                    {
                        listItem.SubItems.Add(myReader.GetString(3));  //description
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    try
                    {
                        listItem.SubItems.Add(myReader.GetDateTime(4).ToString("MM-dd-yyyy  HH:mm:ss"));  //time stamp
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    //listItem.ForeColor = System.Drawing.Color.DarkBlue;
                    //listItem.Selected = true;
                    listItem.EnsureVisible();


                    if (DisabledRadioButton.Checked)
                        INVDisabledListView.Items.Add(listItem);
                    else if (Dis_EnabledRadioButton.Checked)
                        INVEnaDisabledListView.Items.Add(listItem);
                }

                myReader.Close();
                return (true);
            }

            myReader.Close();
            return (false);
        }
        #endregion

        #region GetLocation (int reader, int fgen)
        //$CANDIDATE$ This function is a good candidate for being in the component
        public string GetLocation(int reader, int fgen)
        {
            if (MainForm.m_connection == null)
                return ("");

            lock (MainForm.m_connection)
            {
                string str = "Not defined";
                StringBuilder mySelectQuery = new StringBuilder();
                mySelectQuery.Append("SELECT Location FROM zones WHERE ReaderID = ");
                mySelectQuery.AppendFormat("'{0}'", reader);
                mySelectQuery.Append(" AND FieldGenID = ");
                mySelectQuery.AppendFormat("'{0}'", fgen);

                string mySelectStr = mySelectQuery.ToString();

                OdbcCommand myCommand = new OdbcCommand(mySelectStr, m_connection);
                OdbcDataReader myReader = null;

                /*OdbcDataReader myReader = null;

                if (!RunQueryCmd(mySelectStr, ref myReader))
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
                    //timer3.Enabled = false;
                }
                catch (Exception ex)
                {
                    int ret = 0, ret1 = 0;
                    if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) || 
                        ((ret1=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                    {   
                        //error code 2013
                        if (MainForm.reconnectCounter < 0)
                        {
                            MainForm.reconnectCounter = 0;
                            //MainForm.timer3.Enabled = true;
                        }                           
                    }
                    if (myReader != null)
                    {
                        if (!myReader.IsClosed)
                            myReader.Close();
                    }
                    return "";
                }//catch .. try
                

                if (myReader.Read())
                    str = myReader.GetString(0);

                myReader.Close();
                return str;

            }//m_connection
        }//GetLocation
        #endregion

        #region GetZoneInfo (int reader, int fgen, out string loc, out string status, out short threshold, out string ztype)
        public string GetZoneInfo(int reader, int fgen, out string loc, out string status, out short threshold, out string ztype)
        {
            if (MainForm.m_connection == null)
            {
                ztype = "";
                threshold = 0;
                status = "";
                loc = "";
                return ("");
            }

            lock (MainForm.m_connection)
            {
                string str = "";
                status = "";
                StringBuilder mySelectQuery = new StringBuilder();
                mySelectQuery.Append("SELECT ID, Location, RSSI, Threshold, ZoneType FROM zones WHERE ReaderID = ");
                mySelectQuery.AppendFormat("'{0}'", reader);
                mySelectQuery.Append(" AND FieldGenID = ");
                mySelectQuery.AppendFormat("'{0}'", fgen);

                /*
                //>>>>>string mySelectStr = mySelectQuery.ToString();

                //>>>>>>OdbcCommand myCommand = new OdbcCommand(mySelectStr, m_connection);
                OdbcDataReader myReader = null;
                if (!RunQueryCmd(mySelectQuery.ToString(), ref myReader))
                {
                    if (myReader != null)
                    {
                        if (!myReader.IsClosed)
                            myReader.Close();
                    }

                    loc = "";
                    status = "";
                    threshold = 0;
                    ztype = "";
                    return "";
                }
                */

                OdbcDataReader myReader = null;
                OdbcCommand myCommand = new OdbcCommand(mySelectQuery.ToString(), m_connection);

                try
                {
                    myReader = myCommand.ExecuteReader();
                    MainForm.reconnectCounter = -1;
                    //\\timer3.Enabled = false;
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
                            //\\timer3.Enabled = true;
                        }
                     
                    }
                    if (myReader != null)
                    {
                        if (!myReader.IsClosed)
                            myReader.Close();
                    }

                    loc = "";
                    status = "";
                    threshold = 0;
                    ztype = "";
                    return "";
                }//catch .. try

                /*try
                {
                    myReader = myCommand.ExecuteReader();
                    reconnectCounter = -1;
                    timer3.Enabled = false;
                }
                catch (Exception ex)
                {
                    int ret = 0, ret1 = 0;
                    if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) || 
                        ((ret1=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                    {   
                        //error code 2013
                        if (reconnectCounter < 0)
                        {
                            reconnectCounter = 0;
                            timer3.Enabled = true;
                        }                             
                    }
                    if (myReader != null)
                    {
                        if (!myReader.IsClosed)
                            myReader.Close();
                    }
                    loc = "";
                    status = "";
                    threshold = 0;
                    ztype = "";
                    return "";
                }//catch .. try
                */


                if (myReader.Read())
                {
                    str = myReader.GetString(0);  //zone ID
                    try
                    {
                        loc = myReader.GetString(1);  //location
                    }
                    catch
                    {
                        loc = "";
                    }

                    bool b = myReader.GetBoolean(2);
                    if (b)
                    {
                        try
                        {
                            threshold = Convert.ToInt16(myReader.GetInt16(3));  //threshold
                        }
                        catch
                        {
                            threshold = -1;
                        }
                    }
                    else
                        threshold = -1;

                    try
                    {
                        ztype = myReader.GetString(4);  //zone type
                    }
                    catch
                    {
                        ztype = "";
                    }

                    myReader.Close();
                    return (str);
                }
                else
                {
                    loc = "Not Defined";
                    status = "";
                    ztype = "";
                    threshold = 0;
                }

                myReader.Close();
                return str;

            }//lock m_connection
        }
        #endregion

        #region QueryTagEventNotify
        private void QueryTagEventNotify(AW_API_NET.rfTagEvent_t tagEvent)
        {
            short threshold = 0;
            string loc = "";
            string status = "";
            string ztype = "";
            int index = 0;

            if (MainForm.m_connection == null) return;

            ListView searchListView = new ListView();

            lock (MainForm.m_connection)
            {
                Console.WriteLine("### HELLO ### Tag detected event received for search Tags. ID = " + tagEvent.tag.id.ToString() + " GC = " + tagEvent.tag.groupCount.ToString());

                if (EnabledRadioButton.Checked)
                    searchListView = INVListView;
                else if (DisabledRadioButton.Checked)
                    searchListView = INVDisabledListView;
                else
                    searchListView = INVEnaDisabledListView;

                //if (!tagEvent.tag.status.continuousField)
                //return;
                //int srchIndex = -1;
                //lastTagSearchStruct lastTagSearch = new lastTagSearchStruct(tagEvent.tag.id.ToString(), true, !tagEvent.tag.status.batteryLow, tagEvent.tag.status.enabled);
                //if (IsTagInList(tagEvent.tag.id.ToString(), out srchIndex))
                //{
                //LastTagSrchList.RemoveAt(srchIndex);
                //LastTagSrchList.Add(lastTagSearch);
                // }
                //else
                //{
                //LastTagSrchList.Add(lastTagSearch);
                //}

                if (EnabledRadioButton.Checked)
                {
                    if (!tagEvent.tag.status.enabled)
                        return;
                }
                else if (DisabledRadioButton.Checked)
                {
                    if (tagEvent.tag.status.enabled)
                        return;
                }                

                if (searchingAll || searchingItems)
                {
                    string zoneID = GetZoneInfo(tagEvent.reader, tagEvent.fGenerator, out loc, out status, out threshold, out ztype);

                    //if (!IsMultipleTagGC(tagEvent.tag.id.ToString(), Convert.ToUInt16(tagEvent.tag.groupCount)))
                    {
                        index = GetItemIndex(tagEvent.tag.id.ToString());
                        if (index >= 0) //&& (tagEvent.tag.status.enabled))
                        {
                            searchListView.Items[index].SubItems[2].Text = loc;
                            searchListView.Items[index].SubItems[3].Text = "YES";
                            if (tagEvent.tag.status.enabled)
                            {
                                if (searchListView.Items[index].SubItems[4].Text.ToLower() == "disabled")
                                {
                                    needUpdateFlag = true;
                                    searchListView.Items[index].SubItems[4].Text = "Enabled **";
                                    SearchLabel.Text = "**  Tag ID " + tagEvent.tag.id.ToString() + " Status needs to be updated in the system";
                                }
                                else
                                    searchListView.Items[index].SubItems[4].Text = "Enabled";
                            }
                            else
                            {
                                //searchListView.Items[index].SubItems[4].Text = "Disabled";
                                if (searchListView.Items[index].SubItems[4].Text.ToLower() == "enabled")
                                {
                                    needUpdateFlag = true;
                                    searchListView.Items[index].SubItems[4].Text = "Disabled **";
                                    SearchLabel.Text = "**  Tag ID " + tagEvent.tag.id.ToString() + " Status needs to be updated in the system";
                                }
                                else
                                    searchListView.Items[index].SubItems[4].Text = "Disabled";
                            }

                            if (tagEvent.tag.status.batteryLow)
                                searchListView.Items[index].SubItems[5].Text = "LOW";
                            else
                                searchListView.Items[index].SubItems[5].Text = "OK";

                            searchListView.Items[index].SubItems[7].Text = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
                            searchListView.Items[index].ForeColor = System.Drawing.Color.DarkGreen;

                            UpdateTagResultSummary(tagEvent);

                        } //index
                        else  //tag status did not match
                        {                            
                            if (IsTagInSystem(tagEvent.tag.id.ToString()))
                            {
                                needUpdateFlag = true;
                                UpdateTagResultSummary(tagEvent);
                                SearchLabel.Text = "**  Tag ID " + tagEvent.tag.id.ToString() + " Status needs to be updated in the system";
                            }
                        }

                            //needUpdateFlag = true;
                            //UpdateTagResultSummary(tagEvent);  01-01-08

                            /*if (EnabledRadioButton.Checked)
                            {
                                int k = Convert.ToInt32(TotalEnabledResLabel.Text);
                                k += 1;
                                TotalEnabledResLabel.Text = Convert.ToString(k);
                                TotalEnabledNotResLabel.Text = Convert.ToString(Convert.ToInt32(TotalEnabledNotResLabel.Text) - 1);
                            }
                            else if (DisabledRadioButton.Checked)
                            {
                                int k = Convert.ToInt32(TotalDisabledResLabel.Text);
                                k += 1;
                                TotalDisabledResLabel.Text = Convert.ToString(k);
                                TotalDisabledNotResLabel.Text = Convert.ToString(Convert.ToInt32(TotalDisabledNotResLabel.Text) - 1);
                            }
                            else
                            {
                                if (tagEvent.tag.status.enabled)
                                {
                                    int k = Convert.ToInt32(NumEnabledTaglabel.Text);
                                    k += 1;
                                    NumEnabledTaglabel.Text = Convert.ToString(k);
                                    NumEnabledNotFoundLabel.Text = Convert.ToString(Convert.ToInt32(NumEnabledNotFoundLabel.Text) - 1);
                                }
                                else
                                {
                                    int k = Convert.ToInt32(NumDisabledTaglabel.Text);
                                    k += 1;
                                    NumDisabledTaglabel.Text = Convert.ToString(k);
                                    NumDisabledNotFoundLabel.Text = Convert.ToString(Convert.ToInt32(NumDisabledNotFoundLabel.Text) - 1);
                                }

                            }
                            */

                            if (searchingItems)
                                tagSelIndex += 1;

                            //mForm.UpdateSKUTable("Detected", Convert.ToInt32(zoneID), tagEvent.tag.id.ToString());
                        
                    //}index

                        if (searchingAll)
                            SetSrchTagList(tagSrchList, tagEvent.tag.id.ToString(), true);
                        else if (searchingItems)
                            SetSrchTagList(selectedTagSrchList, tagEvent.tag.id.ToString(), true);

                        //Form1.SetVolume(Form1.generalVol);
                        MainForm.PlaySound(1);

                    } // IsMultipleTagGC
                }//searching all
            }//lock
        }
        #endregion

        private void UpdateTagResultSummary(AW_API_NET.rfTagEvent_t tagEvent)
        {
            if (EnabledRadioButton.Checked)
            {
                int k = Convert.ToInt32(TotalEnabledResLabel.Text);
                k += 1;
                TotalEnabledResLabel.Text = Convert.ToString(k);
                TotalEnabledNotResLabel.Text = Convert.ToString(Convert.ToInt32(TotalEnabledNotResLabel.Text) - 1);
            }
            else if (DisabledRadioButton.Checked)
            {
                int k = Convert.ToInt32(TotalDisabledResLabel.Text);
                k += 1;
                TotalDisabledResLabel.Text = Convert.ToString(k);
                TotalDisabledNotResLabel.Text = Convert.ToString(Convert.ToInt32(TotalDisabledNotResLabel.Text) - 1);
            }
            else
            {
                if (tagEvent.tag.status.enabled)
                {
                    int k = Convert.ToInt32(NumEnabledTaglabel.Text);
                    k += 1;
                    NumEnabledTaglabel.Text = Convert.ToString(k);
                    NumEnabledNotFoundLabel.Text = Convert.ToString(Convert.ToInt32(NumEnabledNotFoundLabel.Text) - 1);
                }
                else
                {
                    int k = Convert.ToInt32(NumDisabledTaglabel.Text);
                    k += 1;
                    NumDisabledTaglabel.Text = Convert.ToString(k);
                    NumDisabledNotFoundLabel.Text = Convert.ToString(Convert.ToInt32(NumDisabledNotFoundLabel.Text) - 1);
                }

            }
        }

        #region IsTagInList
        private bool IsTagInList(string tID, out int srchIndex)
        {
            srchIndex = -1;
            foreach (lastTagSearchStruct lastTag in LastTagSrchList)
            {
                srchIndex += 1;

                if ((lastTag.tagID == tID) && (lastTag.found))
                {
                    return (true);
                }
            }

            return (false);
        }
        #endregion

        #region TagDetected
        private void TagDetected(AW_API_NET.rfTagEvent_t tagEvent)
        {
            //short threshold = 0;
            string loc = "";
            //string status = "";
            //string ztype = "";
            int index = 0;

            if (MainForm.m_connection == null) return;
            
            lock (MainForm.m_connection)
            {
                Console.WriteLine("Tag detected event received for search Tags. ID = " + tagEvent.tag.id.ToString() + " GC = " + tagEvent.tag.groupCount.ToString());

                if (!tagEvent.tag.status.continuousField)
                    return;

                if (searchingAll || searchingItems)
                {
                    //string zoneID = GetZoneInfo(tagEvent.reader, tagEvent.fGenerator, out loc, out status, out threshold, out ztype);
                    loc = GetLocation(Convert.ToInt32(tagEvent.reader), Convert.ToInt32(tagEvent.fGenerator));
                    if (!IsMultipleTagGC(tagEvent.tag.id.ToString(), Convert.ToUInt16(tagEvent.tag.groupCount)))
                    {
                        index = GetItemIndex(tagEvent.tag.id.ToString());
                        if (index >= 0)
                        {
                            INVListView.Items[index].SubItems[5].Text = loc;
                            INVListView.Items[index].SubItems[4].Text = "YES";
                            INVListView.Items[index].ForeColor = System.Drawing.Color.DarkGreen;
                            //INVListView.Items[index].SubItems[4].Text = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
                            int k = Convert.ToInt32(TotalEnabledResLabel.Text);
                            k += 1;
                            TotalEnabledResLabel.Text = Convert.ToString(k);
                            TotalEnabledNotResLabel.Text = Convert.ToString((Convert.ToInt32(TotalEnabledNotResLabel.Text) - 1));

                            if (searchingItems)
                                tagSelIndex += 1;

                            //mForm.UpdateSKUTable("Detected", Convert.ToInt32(zoneID), tagEvent.tag.id.ToString());
                        }

                        if (searchingAll)
                            SetSrchTagList(tagSrchList, tagEvent.tag.id.ToString(), true);
                        else if (searchingItems)
                            SetSrchTagList(selectedTagSrchList, tagEvent.tag.id.ToString(), true);

                        //Form1.SetVolume(Form1.generalVol);
                        MainForm.PlaySound(1);

                    } //IsMultipleTagGC
                }//searching all
            }//lock
        }
        #endregion

        #region TagDetectedSani
        private void TagDetectedSani(AW_API_NET.rfTagEvent_t tagEvent)
        {
            //short threshold = 0;
            string loc = "";
            //string status = "";
            //string ztype = "";
            int index = 0;

            if (MainForm.m_connection == null) return;

            lock (MainForm.m_connection)
            {
                Console.WriteLine("Sani Tag detected event received for search Tags. ID = " + tagEvent.tag.id.ToString() + " GC = " + tagEvent.tag.groupCount.ToString());

                if (!tagEvent.tag.status.continuousField)
                    return;

                if (searchingAll || searchingItems)
                {
                    //string zoneID = GetZoneInfo(tagEvent.reader, tagEvent.fGenerator, out loc, out status, out threshold, out ztype);
                    loc = GetLocation(Convert.ToInt32(tagEvent.reader), Convert.ToInt32(tagEvent.fGenerator));
                    if (!IsMultipleTagGC(tagEvent.tag.id.ToString(), Convert.ToUInt16(tagEvent.tag.groupCount)))
                    {
                        index = GetItemIndex(tagEvent.tag.id.ToString());
                        if (index >= 0)
                        {
                            INVListView.Items[index].SubItems[5].Text = loc;
                            INVListView.Items[index].SubItems[4].Text = "YES";
                            INVListView.Items[index].ForeColor = System.Drawing.Color.DarkGreen;
                            //INVListView.Items[index].SubItems[4].Text = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
                            int k = Convert.ToInt32(TotalEnabledResLabel.Text);
                            k += 1;
                            TotalEnabledResLabel.Text = Convert.ToString(k);
                            TotalEnabledNotResLabel.Text = Convert.ToString((Convert.ToInt32(TotalEnabledNotResLabel.Text) - 1));

                            if (searchingItems)
                                tagSelIndex += 1;

                            //mForm.UpdateSKUTable("Detected", Convert.ToInt32(zoneID), tagEvent.tag.id.ToString());
                        }

                        if (searchingAll)
                            SetSrchTagList(tagSrchList, tagEvent.tag.id.ToString(), true);
                        else if (searchingItems)
                            SetSrchTagList(selectedTagSrchList, tagEvent.tag.id.ToString(), true);

                        //Form1.SetVolume(Form1.generalVol);
                        MainForm.PlaySound(1);

                    } //IsMultipleTagGC
                }//searching all
            }//lock
        }
        #endregion

        #region GetItemIndex(string tagID)
        private int GetItemIndex(string tagID)
        {
            ListView lv = new ListView();
            if (EnabledRadioButton.Checked)
                lv = INVListView;
            else if (DisabledRadioButton.Checked)
                lv = INVDisabledListView;
            else
                lv = INVEnaDisabledListView;

            for (int i = 0; i < lv.Items.Count; i++)
            {                
                if ((lv.Items[i].SubItems[1].Text == tagID) &&
                    (lv.Items[i].SubItems[4].Text == "NO"))
                    return i;
            }

            return (-1);
        }
        #endregion

        #region TagDetectedRSSI
        private void TagDetectedRSSI(AW_API_NET.rfTagEvent_t tagEvent)
        {
            //short threshold = 0;
            string loc = "";
            //string status = "";
            //string ztype = "";
            int index = 0;

            if (MainForm.m_connection == null) return;
            lock (MainForm.m_connection)
            {
                Console.WriteLine("Tag detected event received for search Tags. ID = " + tagEvent.tag.id.ToString() + " GC = " + tagEvent.tag.groupCount.ToString());

                if (!tagEvent.tag.status.continuousField)
                    return;

                if (searchingAll || searchingItems)
                {
                    //string zoneID = GetZoneInfo(tagEvent.reader, tagEvent.fGenerator, out loc, out status, out threshold, out ztype);
                    loc = GetLocation(Convert.ToInt32(tagEvent.reader), Convert.ToInt32(tagEvent.fGenerator));

                    if (!IsMultipleTagGC(tagEvent.tag.id.ToString(), Convert.ToUInt16(tagEvent.tag.groupCount)))
                    {                        
                        index = GetItemIndex(tagEvent.tag.id.ToString());
                        if (index >= 0)
                        {
                            INVListView.Items[index].SubItems[5].Text = loc;
                            INVListView.Items[index].SubItems[4].Text = "YES";
                            INVListView.Items[index].ForeColor = System.Drawing.Color.DarkGreen;
                            //INVListView.Items[index].SubItems[7].Text = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
                            int k = Convert.ToInt32(TotalEnabledResLabel.Text);
                            k += 1;
                            TotalEnabledResLabel.Text = Convert.ToString(k);
                            TotalEnabledNotResLabel.Text = Convert.ToString((Convert.ToInt32(TotalEnabledNotResLabel.Text) - 1));

                            //if (searchingItems)
                                //tagSelIndex += 1;
                            //if ((zoneID == "") || (zoneID == null))
                                //zoneID = "0";
                            //mForm.UpdateSKUTable("Detected", Convert.ToInt32(zoneID), tagEvent.tag.id.ToString());
                        }

                        if (searchingAll)
                            SetSrchTagList(tagSrchList, tagEvent.tag.id.ToString(), true);
                        else if (searchingItems)
                            SetSrchTagList(selectedTagSrchList, tagEvent.tag.id.ToString(), true);
                        //Form1.SetVolume(Form1.generalVol);
                        MainForm.PlaySound(1);
                    }
                }//searching
            }//lock

        }//TagDetectedRSSI
        #endregion

        #region IsMultipleTagGC(string id, ushort gc)
        private bool IsMultipleTagGC(string id, ushort gc)
        {
            int index = -1;
            for (int i = 0; i < 250; i++)
            {
                if (tagGC[i].tag == null)
                {
                    if (index == -1)
                        index = i;
                }
                else if (tagGC[i].tag == id)
                {
                    if (tagGC[i].GC == gc)
                        return true;
                    else
                    {
                        tagGC[i].GC = gc;
                        return false;
                    }
                }
            }

            if (index >= 0)
            {
                tagGC[index].tag = id;
                tagGC[index].GC = gc;
            }
            return false;
        }
        #endregion

        #region SearchAllButton_Click()
        private void SearchAllButton_Click(object sender, System.EventArgs e)
        {
            //numRdrSrch = 0;
            //foreach ( readerSearchStruct rdrSearch in rdrSrchList )
            //{
            //rdrSearch.count = 0;
            //numRdrSrch += 1;
            //}

            /*readerSearchStruct rdrSearch = new readerSearchStruct();
            for (int i=0; i<rdrSrchList.Count; i++)
            {
               rdrSearch = (readerSearchStruct)rdrSrchList[i];
               rdrSearch.count = 0;
            }

            rdrSrchIndex = 0;
            numTries = 0;

            for (int i=0; i<INVListView.Items.Count; i++)
            {
                INVListView.Items[i].Checked = false;
                INVListView.Items[i].SubItems[3].Text = "NO";
                INVListView.Items[i].ForeColor = System.Drawing.Color.Blue;
            }

            ClearSrchTagList();
            ShowSrchLabel = true;
            TotalResLabel.Text = "0";
            TotalNotResLabel.Text = "0";

            TotNumTagsLabel.Text = "Total Num Active Tags: "; 
            searchLocation = "";

            mForm.ResetSKUTableResponse();

            searchingAll = true;
            searchingItems = false;
            startSearch = true;

            SearchTimer.Enabled = false;
            SearchTimer.Interval = 500;
            SearchTimer.Enabled = true;
            */
        }
        #endregion

        #region SetSrchTagList(ArrayList list, string id, bool b)
        private void SetSrchTagList(ArrayList list, string id, bool b)
        {
            tagSearchStruct tagSrch = new tagSearchStruct();
            for (int i = 0; i < list.Count; i++)
            {
                tagSrch = (tagSearchStruct)list[i];
                if (tagSrch.tagID == id)
                {
                    list.RemoveAt(i);
                    tagSrch.found = b;
                    list.Insert(i, tagSrch);
                    Console.WriteLine("Tag found param set TRUE TagID = " + id);
                    return;
                }
            }
        }
        #endregion

        #region ClearSrchTagList()
        private void ClearSrchTagList()
        {
            tagSearchStruct tagSrch = new tagSearchStruct();
            for (int i = 0; i < tagSrchList.Count; i++)
            {
                tagSrch = (tagSearchStruct)tagSrchList[i];
                tagSrch.found = false;
                tagSrchList.RemoveAt(i);
                tagSrchList.Insert(i, tagSrch);
            }
        }
        #endregion

        #region AreAllTagsDetected()
        private bool AreAllTagsDetected()
        {
            tagSearchStruct tagSrch = new tagSearchStruct();
            for (int i = 0; i < tagSrchList.Count; i++)
            {
                tagSrch = (tagSearchStruct)tagSrchList[i];
                if ((!tagSrch.found && (tagSrch.tagID.Length > 0)))
                    return (false);
            }

            return (true);
        }
        #endregion

        #region AreAllSelTagsDetected()
        private bool AreAllSelTagsDetected()
        {
            tagSearchStruct tagSrch = new tagSearchStruct();
            for (int i = 0; i < selectedTagSrchList.Count; i++)
            {
                tagSrch = (tagSearchStruct)selectedTagSrchList[i];
                if (!tagSrch.found)
                    return (false);
            }

            return (true);
        }
        #endregion

        #region SearchTimer_Elapsed()
        private void SearchTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (searchingAll)
            {
                if ((rdrSrchList.Count == 0) && startSearch)
                {
                    startSearch = false;
                    searchingAll = false;
                    SearchLabel.ForeColor = System.Drawing.Color.Blue;
                    SearchLabel.Text = "ERROR: No reader is defined for search.";
                    //Form1.SetVolume(Form1.generalVol);
                    StopSearch();
                    MainForm.PlaySound(1);
                    return;
                }

                //Search Timer Interval for sending command to search the tag
                SerachTimer.Interval = 4500;
                Console.WriteLine("Searching ALL Timer Timestamp = " + DateTime.Now.ToString("MM-dd-yyyy  HH:mm:ss"));
                readerSearchStruct rdrSearch = new readerSearchStruct();

                bool allDetected = AreAllTagsDetected();
                if (allDetected || (numTries > (MAX_SEARCH_TRY * rdrSrchList.Count)))
                {
                    searchingAll = false;
                    SearchLabel.ForeColor = System.Drawing.Color.Blue;
                    if (needUpdateFlag)
                        SearchLabel.Text = "Search Completed. **  One or more Tag Status needs to be updated in the system.";
                    else
                        SearchLabel.Text = "Search Completed.";
                    if (EnabledRadioButton.Checked)
                        lastEnabledSearchTime = DateTime.Now.ToString();
                    else if (DisabledRadioButton.Checked)
                        lastDisabledSearchTime = DateTime.Now.ToString();
                    else
                        lastEnDisabledSearchTime = DateTime.Now.ToString();
                    INVListView.Invalidate();

                    /*if (EnabledRadioButton.Checked)
                    {
                        int k = Convert.ToInt32(TotalEnabledResLabel.Text);
                        int ix = Convert.ToInt32(TotalEnabledTagsLabel.Text);
                        int p = ix - k;
                        if (p >= 0)
                            TotalEnabledNotResLabel.Text = Convert.ToString(p);
                    }
                    else if (DisabledRadioButton.Checked)
                    {

                        int k = Convert.ToInt32(TotalEnabledResLabel.Text);
                        int ix = Convert.ToInt32(TotalEnabledTagsLabel.Text);
                        int p = ix - k;
                        if (p >= 0)
                            TotalEnabledNotResLabel.Text = Convert.ToString(p);
                    }
                    else
                    {
                        int k = Convert.ToInt32(NumEnabledTaglabel.Text);
                        int ix = Convert.ToInt32(NumTotalEnabledTagsLabel.Text);
                        int p = ix - k;
                        if (p >= 0)
                            NumEnabledNotFoundLabel.Text = Convert.ToString(p);

                        k = Convert.ToInt32(NumDisabledTaglabel.Text);
                        ix = Convert.ToInt32(NumTotalDisabledTagsLabel.Text);
                        p = ix - k;
                        if (p >= 0)
                            NumDisabledNotFoundLabel.Text = Convert.ToString(p);

                    }
                    */

                    SerachTimer.Interval = 500;
                    //Form1.SetVolume(Form1.generalVol);
                    MainForm.PlaySound(1);

                    toolBar1.Buttons[0].Enabled = true;  //search ALL
                    //SrchAllToolBarButton.Enabled = true;

                    toolBar1.Buttons[1].Enabled = true;  //search specific
                    //SrchSpecToolBarButton.Enabled = true;

                    toolBar1.Buttons[2].Enabled = true;  //Clear Search
                    //ClearSrchToolBarButton.Enabled = true;

                    toolBar1.Buttons[3].Enabled = true;  //clear checks
                    //ClearCheckBoxtToolBarButton.Enabled = true;

                    Dis_EnabledRadioButton.Enabled = true;
                    EnabledRadioButton.Enabled = true;
                    DisabledRadioButton.Enabled = true;

                    return;
                }

                if (rdrSrchIndex >= rdrSrchList.Count)
                    rdrSrchIndex = 0;

                //Rdr Search List Is for holding the selected readers
                rdrSearch = (readerSearchStruct)rdrSrchList[rdrSrchIndex];
                rdrSearch.count += 1;
                rdrSrchIndex += 1;
                numTries += 1;
                //////////////////////////////////////////

                //Stopwatch watch = new Stopwatch();

                //watch.Start();
                //for (int i = 1; i < 1000000; i++) { }   // Execute the task to be timed
                //watch.Stop();

                //Console.WriteLine("Elapsed: {0}", watch.Elapsed);
                //Console.WriteLine("In milliseconds: {0}", watch.ElapsedMilliseconds);
                //Console.WriteLine("In timer ticks: {0}", watch.ElapsedTicks);

                ///////////////////////////////////////////

                //if (rdrSearch.readerID == 44)
                //rdrSearch.fgenID = 2;
                searchLocation = mForm.GetLocation(Convert.ToInt32(rdrSearch.readerID), Convert.ToInt32(rdrSearch.fgenID));
                Console.WriteLine("Search Tags Rdr = " + rdrSearch.readerID.ToString() + "  Time = " + DateTime.Now.ToString("HH:mm:ss"));
                if (EnabledRadioButton.Checked)
                {
                    communication.CallTag(rdrSearch.readerID, rdrSearch.fgenID, 0, "ACC" , "Type");                    
                }
                else
                {
                    //This command should cover all 3 search options when command is search all 
                    //Changed from "ACC" to "ANY" htg 080110
                    communication.QueryTag(rdrSearch.readerID, "ANY");
                }

            }
            else if (searchingItems)
            {
                if ((rdrSrchList.Count == 0) && startSearch)
                {
                    startSearch = false;
                    searchingItems = false;
                    SearchLabel.ForeColor = System.Drawing.Color.Blue;
                    SearchLabel.Text = "ERROR: No reader is defined for search.";
                    //Form1.SetVolume(Form1.generalVol);
                    StopSearch();
                    MainForm.PlaySound(1);
                    return;
                }

                SerachTimer.Interval = 4500;
                Console.WriteLine("Searching ALL Timer Timestamp = " + DateTime.Now.ToString("MM-dd-yyyy  HH:mm:ss"));
                readerSearchStruct rdrSearch = new readerSearchStruct();

                bool allDetected = AreAllSelTagsDetected();
                if (allDetected || (numTries > (MAX_SEARCH_TRY_SELECTED * rdrSrchList.Count)))
                {
                    searchingItems = false;
                    SearchLabel.ForeColor = System.Drawing.Color.Blue;
                    if (needUpdateFlag)
                        SearchLabel.Text = "Search Completed. **  One or more Tag Status needs to be updated in the system.";
                    else
                        SearchLabel.Text = "Search Completed.";
                    if (EnabledRadioButton.Checked)
                        lastEnabledSearchTime = DateTime.Now.ToString();
                    else if (DisabledRadioButton.Checked)
                        lastDisabledSearchTime = DateTime.Now.ToString();
                    else
                        lastEnDisabledSearchTime = DateTime.Now.ToString();
                    INVListView.Invalidate();

                    /*int k = Convert.ToInt32(TotalEnabledResLabel.Text);
                    int ix = Convert.ToInt32(TotalEnabledTagsLabel.Text);
                    int p = ix - k;
                    if (p >= 0)
                        TotalEnabledNotResLabel.Text = Convert.ToString(p);
                     */

                    //Search Timer for Interval going around the network
                    SerachTimer.Interval = 500;

                    toolBar1.Buttons[0].Enabled = true;  //search ALL
                    //SrchAllToolBarButton.Enabled = true;

                    toolBar1.Buttons[1].Enabled = true;  //search specific
                    //SrchSpecToolBarButton.Enabled = true;

                    toolBar1.Buttons[2].Enabled = true;  //Clear Search
                    //ClearSrchToolBarButton.Enabled = true;

                    toolBar1.Buttons[3].Enabled = true;  //clear checks
                    //ClearCheckBoxtToolBarButton.Enabled = true;

                    //Update the search button in the screen
                    Dis_EnabledRadioButton.Enabled = true;
                    EnabledRadioButton.Enabled = true;
                    DisabledRadioButton.Enabled = true;

                    return;
                }

                if (rdrSrchIndex >= rdrSrchList.Count)
                {
                    rdrSrchIndex = 0;
                    tagSelIndex += 1;
                }

                rdrSearch = (readerSearchStruct)rdrSrchList[rdrSrchIndex];
                rdrSearch.count += 1;
                rdrSrchIndex += 1;
                numTries += 1;
                tagSearchStruct selTagSrch = new tagSearchStruct();
                if (tagSelIndex >= selectedTagSrchList.Count)
                    tagSelIndex = 0;
                selTagSrch = (tagSearchStruct)selectedTagSrchList[tagSelIndex];

                serachTagStrID = Convert.ToString(selTagSrch.tagID);
                searchLocation = mForm.GetLocation(Convert.ToInt32(rdrSearch.readerID), Convert.ToInt32(rdrSearch.fgenID));

                if (EnabledRadioButton.Checked)
                    communication.CallTag(rdrSearch.readerID, rdrSearch.fgenID, Convert.ToUInt32(selTagSrch.tagID), "ACC", "Specific", true, true);
                else
                    //This command should cover specific tag with all 3 search options
                    //Changed from "ACC" to "ANY" htg 080110
                    communication.QueryTag(rdrSearch.readerID, Convert.ToUInt32(selTagSrch.tagID), "ANY", true, true);

                //serachTagStrID = Convert.ToString(selTagSrch.tagID);
                //searchLocation = mForm.GetLocation(Convert.ToInt32(rdrSearch.readerID), Convert.ToInt32(rdrSearch.fgenID));                
            }
        }
        #endregion

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (searchingAll || searchingItems)
            {
                if (searchingItems)
                {
                    if (AreAllSelTagsDetected())
                    {
                        searchingItems = false;
                        SearchLabel.ForeColor = System.Drawing.Color.Blue;
                        if (needUpdateFlag)
                            SearchLabel.Text = "Search Completed. **  One or more Tag Status needs to be updated in the system.";
                        else
                            SearchLabel.Text = "Search Completed.";
                        if (EnabledRadioButton.Checked)
                            lastEnabledSearchTime = DateTime.Now.ToString();
                        else if (DisabledRadioButton.Checked)
                            lastDisabledSearchTime = DateTime.Now.ToString();
                        else
                            lastEnDisabledSearchTime = DateTime.Now.ToString();
                        INVListView.Invalidate();

                        /*int k = Convert.ToInt32(TotalEnabledResLabel.Text);
                        int ix = Convert.ToInt32(TotalEnabledTagsLabel.Text);
                        int p = ix - k;
                        if (p >= 0)
                            TotalEnabledNotResLabel.Text = Convert.ToString(p);
                        */

                        SerachTimer.Interval = 500;
                        //Form1.SetVolume(Form1.generalVol);
                        MainForm.PlaySound(1);

                        toolBar1.Buttons[0].Enabled = true;  //search ALL
                        //SrchAllToolBarButton.Enabled = true;

                        toolBar1.Buttons[1].Enabled = true;  //search specific
                        //SrchSpecToolBarButton.Enabled = true;

                        toolBar1.Buttons[2].Enabled = true;  //Clear Search
                        //ClearSrchToolBarButton.Enabled = true;

                        toolBar1.Buttons[3].Enabled = true;  //clear checks
                        //ClearCheckBoxtToolBarButton.Enabled = true;
                        
                        Dis_EnabledRadioButton.Enabled = true;
                        EnabledRadioButton.Enabled = true;
                        DisabledRadioButton.Enabled = true;

                        return;
                    }
                }
                else
                {
                    if (AreAllTagsDetected())
                    {
                        searchingAll = false;
                        SearchLabel.ForeColor = System.Drawing.Color.Blue;
                        if (needUpdateFlag)
                            SearchLabel.Text = "Search Completed. **  One or more Tag Status needs to be updated in the system.";
                        else
                            SearchLabel.Text = "Search Completed.";
                        if (EnabledRadioButton.Checked)
                            lastEnabledSearchTime = DateTime.Now.ToString();
                        else if (DisabledRadioButton.Checked)
                            lastDisabledSearchTime = DateTime.Now.ToString();
                        else
                            lastEnDisabledSearchTime = DateTime.Now.ToString();
                        INVListView.Invalidate();

                        /*int k = Convert.ToInt32(TotalEnabledResLabel.Text);
                        int ix = Convert.ToInt32(TotalEnabledTagsLabel.Text);
                        int p = ix - k;
                        if (p >= 0)
                            TotalEnabledNotResLabel.Text = Convert.ToString(p);
                        */

                        SerachTimer.Interval = 500;
                        //Form1.SetVolume(Form1.generalVol);
                        MainForm.PlaySound(1);

                        toolBar1.Buttons[0].Enabled = true;  //search ALL
                        //SrchAllToolBarButton.Enabled = true;

                        toolBar1.Buttons[1].Enabled = true;  //search specific
                        //SrchSpecToolBarButton.Enabled = true;

                        toolBar1.Buttons[2].Enabled = true;  //Clear Search
                        //ClearSrchToolBarButton.Enabled = true;

                        toolBar1.Buttons[3].Enabled = true;  //clear checks
                        //ClearCheckBoxtToolBarButton.Enabled = true;
                        
                        Dis_EnabledRadioButton.Enabled = true;
                        EnabledRadioButton.Enabled = true;
                        DisabledRadioButton.Enabled = true;

                        return;
                    }
                }

                if (ShowSrchLabel)
                {
                    ShowSrchLabel = false;
                    SearchLabel.ForeColor = System.Drawing.Color.Blue;
                    if (searchLocation == "")
                        SearchLabel.Text = "Searching For Tag " + serachTagStrID + ".";
                    else
                        SearchLabel.Text = "Searching  '" + searchLocation + "'  Location For Tag " + serachTagStrID + ".";
                }
                else
                {
                    ShowSrchLabel = true;
                    //SearchLabel.Text = "";
                    SearchLabel.ForeColor = System.Drawing.Color.Gray;
                    if (searchLocation == "")
                        SearchLabel.Text = "Searching For Tag " + serachTagStrID + ".";
                    else
                        SearchLabel.Text = "Searching  '" + searchLocation + "'  Location For Tag " + serachTagStrID + ".";
                }
            }
        }

        private void SearchItemButton_Click(object sender, System.EventArgs e)
        {
            /*selectedTagSrchList.Clear();
            searchLocation = "";
            int count = 0;

            for (int i=0; i<INVListView.Items.Count; i++)
            {
                if (INVListView.Items[i].Checked)
                {
                    tagSearchStruct selTagSrch = new tagSearchStruct();
                    selTagSrch.found = false;
                    selTagSrch.tagID = INVListView.Items[i].SubItems[1].Text;
                    INVListView.Items[i].SubItems[3].Text = "NO";
                    INVListView.Items[i].ForeColor = System.Drawing.Color.Blue;
                    selectedTagSrchList.Add(selTagSrch);
                    count += 1;
                    mForm.UpdateSKUTable("Not Detected", 0, selTagSrch.tagID);
                }
            }

            if (count == 0)
            {
                SearchLabel.Text = "ERROR: No item is selected.";
                //Form1.SetVolume(Form1.generalVol);
                Form1.PlaySound(1);
                return;
            }

            ShowSrchLabel = true;
            TotalResLabel.Text = "0";
            TotalNotResLabel.Text = "0";
            TotNumTagsLabel.Text = "Total Num Selected Tags: ";
            TotalTagsLabel.Text = Convert.ToString(count);

            rdrSrchIndex = 0;
            tagSelIndex = 0;
            numTries = 0;

            searchingAll = false;
            searchingItems = true;
            startSearch = true;

            SearchTimer.Enabled = false;
            SearchTimer.Interval = 500;
            SearchTimer.Enabled = true;
            */
        }

        private void RefreshListButton_Click(object sender, System.EventArgs e)
        {
            if (searchingAll || searchingItems)
            {
                MsgForm msgDlg = new MsgForm("Search is in process. Can not clear the list at this time");
                //Form1.SetVolume(Form1.generalVol);
                MainForm.PlaySound(1);
                if (msgDlg.ShowDialog(this) == DialogResult.OK)
                {
                    return;
                }
            }
            else
            {
                INVEnaDisabledListView.Items.Clear();
                LoadInventoryListView(3);
            }
        }

        private void ClearSelItemButton_Click(object sender, System.EventArgs e)
        {
            if (searchingAll || searchingItems)
            {
                MsgForm msgDlg = new MsgForm("Search is in process. Can not clear the list at this time");
                //Form1.SetVolume(Form1.generalVol);
                MainForm.PlaySound(1);
                if (msgDlg.ShowDialog(this) == DialogResult.OK)
                {
                    return;
                }
            }
            else
            {
                for (int i = 0; i < INVListView.Items.Count; i++)
                    INVListView.Items[i].Checked = false;
            }
        }

        private void StopSrchButton_Click(object sender, System.EventArgs e)
        {
            if (searchingAll || searchingItems)
            {
                searchingAll = false;
                searchingItems = false;
                startSearch = false;
                SearchLabel.Text = "Search Stopped.";
            }
        }

        private void ClearButton_Click(object sender, System.EventArgs e)
        {
            if (searchingAll || searchingItems)
            {
                MsgForm msgDlg = new MsgForm("Search is in process. Can not clear the list at this time");
                //Form1.SetVolume(Form1.generalVol);
                MainForm.PlaySound(1);
                if (msgDlg.ShowDialog(this) == DialogResult.OK)
                {
                    return;
                }
            }
            else
                INVListView.Items.Clear();
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
                case 1: //tagid

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

        private void INVListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            m_sortReverse = (e.Column == m_sortColumn) ? !m_sortReverse : false;
            m_sortColumn = e.Column;
            INVListView.ListViewItemSorter = this;
            INVListView.Sort();
            INVListView.ListViewItemSorter = null;
        }

        private void ResetMultiTagArray()
        {
            for (int i=0; i<250; i++)
            {
                tagGC[i].tag = "";
                tagGC[i].GC = 0;
            }
        }

        private int GetSearchReaders()
        {
            int count = 0;
            for (int i = 0; i<LocationListView.Items.Count; i++)
            {
                if (LocationListView.Items[i].Checked)
                    count += 1;
            }

            return (count);
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {           
            ListView searchListView = new ListView();
            if (EnabledRadioButton.Checked)
            {
                lastEnabledSearchTime = "";
                searchListView = INVListView;
            }
            else if (DisabledRadioButton.Checked)
            {
                lastDisabledSearchTime = "";
                searchListView = INVDisabledListView;
            }
            else
            {
                lastEnDisabledSearchTime = "";
                searchListView = INVEnaDisabledListView;
            }

            ushort nFound = 0;
            ushort nEnableFound = 0;
            ushort nDisableFound = 0;

            if (e.Button.Text == "Search All")   //@@@@@@@@@  SERACH ALL @@@@@@@@
            {
                if (GetSearchReaders() == 0)
                {
                    MessageBox.Show(this, "No Reader(s) are selected for the search.", "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                serachTagStrID = "";
                /*if (EnabledRadioButton.Checked)
                {
                    lastEnabledSearchTime = "";
                    searchListView = INVListView;
                }
                else if (DisabledRadioButton.Checked)
                {
                    lastDisabledSearchTime = "";
                    searchListView = INVDisabledListView;
                }
                else
                {
                    lastEnDisabledSearchTime = "";
                    searchListView = INVEnaDisabledListView;
                }*/

                ///////////readerSearchStruct rdrSearch = new readerSearchStruct();
                //////////for (int i = 0; i < rdrSrchList.Count; i++)
                /////////{
                    /////////rdrSearch = (readerSearchStruct)rdrSrchList[i];
                    /////////rdrSearch.count = 0;
                /////////}

                rdrSrchIndex = 0;
                numTries = 0;

                
                ResetMultiTagArray();

                for (int i = 0; i < searchListView.Items.Count; i++)
                {
                    if (Dis_EnabledRadioButton.Checked)
                    {
                        if (searchListView.Items[i].SubItems[4].Text == "Enabled")
                        {
                            nEnableFound += 1;
                        }
                        else if (searchListView.Items[i].SubItems[4].Text == "Disabled")
                        {
                            nDisableFound += 1;
                        }
                        else if (searchListView.Items[i].SubItems[4].Text == "Enabled **")
                        {
                            searchListView.Items[i].SubItems[4].Text = "Disabled";
                            nDisableFound += 1;
                        }
                        else if (searchListView.Items[i].SubItems[4].Text == "Disabled **")
                        {
                            searchListView.Items[i].SubItems[4].Text = "Enabled";
                            nEnableFound += 1;
                        }

                        //if (searchListView.Items[i].SubItems[4].Text == "Enabled")
                            //nEnableFound += 1;
                        //else if (searchListView.Items[i].SubItems[4].Text == "Disabled")
                            //nDisableFound += 1;
                    }

                    searchListView.Items[i].Checked = true;
                    searchListView.Items[i].SubItems[4].Text = "NO";
                    searchListView.Items[i].SubItems[5].Text = "";
                    searchListView.Items[i].ForeColor = System.Drawing.Color.Blue;
                }

                ClearSrchTagList();
                ShowSrchLabel = true;
                if (EnabledRadioButton.Checked)
                {
                    ClearSearch();
                    for (int i = 0; i < searchListView.Items.Count; i++)
                    {
                        searchListView.Items[i].Checked = true;
                        searchListView.Items[i].SubItems[4].Text = "NO";
                    }
                    //TotalEnabledResLabel.Text = "0";
                    TotalEnabledNotResLabel.Text = Convert.ToString(searchListView.Items.Count);
                    NumTagToSearchLabel.Text = Convert.ToString(searchListView.Items.Count);

                    //TotNumTagsLabel.Text = "Total Num Active Tags: ";
                }
                else if (DisabledRadioButton.Checked)
                {
                    ClearSearch();
                    for (int i = 0; i < searchListView.Items.Count; i++)
                        searchListView.Items[i].Checked = true;
                    //TotalDisabledResLabel.Text = "0";
                    TotalDisabledNotResLabel.Text = Convert.ToString(searchListView.Items.Count);
                }
                else
                {
                    NumEnabledTaglabel.Text = "0";
                    NumEnabledNotFoundLabel.Text = Convert.ToString(nEnableFound);
                    NumDisabledTaglabel.Text = "0";
                    NumDisabledNotFoundLabel.Text = Convert.ToString(nDisableFound);
                }

                
                searchLocation = "";

                //mForm.ResetSKUTableResponse();

                needUpdateFlag = false;
                searchingAll = true;
                searchingItems = false;
                startSearch = true;

                LoadRdrSearchList();

                SerachTimer.Enabled = false;
                SerachTimer.Interval = 500;
                SerachTimer.Enabled = true;


                toolBar1.Buttons[0].Enabled = false;  //search ALL
                //SrchAllToolBarButton.Enabled = false;

                toolBar1.Buttons[1].Enabled = false;  //search specific
                //SrchSpecToolBarButton.Enabled = false;

                toolBar1.Buttons[2].Enabled = false;  //Clear Search
                //ClearSrchToolBarButton.Enabled = false;

                toolBar1.Buttons[3].Enabled = false;  //clear checks
                //ClearCheckBoxtToolBarButton.Enabled = false;           
                
                Dis_EnabledRadioButton.Enabled = false;
                EnabledRadioButton.Enabled = false;
                DisabledRadioButton.Enabled = false;
                
               
            }
            else if (e.Button.Text == "Search Specific")   //@@@@@@@@@  SERACH SPECIFIC @@@@@@@@
            {
                if (GetSearchReaders() == 0)
                {
                    MessageBox.Show(this, "No Reader(s) are selected for the search.", "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                serachTagStrID = "";
                if (EnabledRadioButton.Checked)
                    lastEnabledSearchTime = "";
                else if (DisabledRadioButton.Checked)
                    lastDisabledSearchTime = "";
                else
                    lastEnDisabledSearchTime = "";

                selectedTagSrchList.Clear();
                searchLocation = "";
                int count = 0;

                ResetMultiTagArray();
                
                for (int i = 0; i < searchListView.Items.Count; i++)
                {
                    //if (searchListView.Items[i].SubItems[3].Text == "YES")
                    //nFound += 1;

                    if (searchListView.Items[i].Checked)
                        nFound += 1;

                    if (Dis_EnabledRadioButton.Checked)
                    {
                        if (searchListView.Items[i].Checked)
                        {
                            //if (searchListView.Items[i].SubItems[3].Text == "YES")
                            //{
                                //if (searchListView.Items[i].SubItems[4].Text == "Enabled")
                                    //nEnableFound += 1;
                                //if (searchListView.Items[i].SubItems[4].Text == "Disabled")
                                    //nDisableFound += 1;
                            //}

                            searchListView.Items[i].SubItems[4].Text = "NO";

                            if (searchListView.Items[i].SubItems[4].Text == "Enabled")
                            {
                                nEnableFound += 1;
                            }                            
                            else if (searchListView.Items[i].SubItems[4].Text == "Disabled")
                            {
                                nDisableFound += 1;
                            }
                            else if (searchListView.Items[i].SubItems[4].Text == "Enabled **")
                            {
                                searchListView.Items[i].SubItems[4].Text = "Disabled";
                                nDisableFound += 1;
                            }
                            else if (searchListView.Items[i].SubItems[4].Text == "Disabled **")
                            {
                                searchListView.Items[i].SubItems[4].Text = "Enabled";
                                nEnableFound += 1;
                            }
                        }
                    }

                    if (searchListView.Items[i].Checked)
                    {
                        tagSearchStruct selTagSrch = new tagSearchStruct();
                        selTagSrch.found = false;
                        selTagSrch.tagID = searchListView.Items[i].SubItems[1].Text;
                        searchListView.Items[i].SubItems[4].Text = "NO";
                        searchListView.Items[i].SubItems[5].Text = "";
                        //if (searchListView.Items[i].SubItems[4].Text.ToLower() == "Enabled **")
                            //searchListView.Items[i].SubItems[4].Text = "Disabled";
                        //else if (searchListView.Items[i].SubItems[4].Text.ToLower() == "Disabled **")
                            //searchListView.Items[i].SubItems[4].Text = "Enabled";
                        searchListView.Items[i].ForeColor = System.Drawing.Color.Blue;
                        selectedTagSrchList.Add(selTagSrch);
                        count += 1;
                        //mForm.UpdateSKUTable("Not Detected", 0, selTagSrch.tagID);
                    }
                }

                if (count == 0)
                {
                    SearchLabel.Text = "ERROR: No item is selected.";
                    //Form1.SetVolume(Form1.generalVol);
                    MainForm.PlaySound(1);
                    return;
                }

                ShowSrchLabel = true;
                //TotalEnabledResLabel.Text = "0";
                //TotalEnabledNotResLabel.Text = "0";
                //TotNumTagsLabel.Text = "Total Num Selected Tags: ";
                //TotalEnabledTagsLabel.Text = Convert.ToString(count);

                if (EnabledRadioButton.Checked)
                {
                    TotalEnabledResLabel.Text = "0"; //Convert.ToString(Convert.ToUInt16(TotalEnabledResLabel.Text) - nFound);
                    TotalEnabledNotResLabel.Text = Convert.ToString(nFound); // Convert.ToString(Convert.ToUInt16(TotalEnabledTagsLabel.Text) - Convert.ToUInt16(TotalEnabledResLabel.Text));
                }
                else if (DisabledRadioButton.Checked)
                {
                    TotalDisabledResLabel.Text = "0";  //Convert.ToString(Convert.ToUInt16(TotalDisabledResLabel.Text) - nFound);
                    TotalDisabledNotResLabel.Text = Convert.ToString(nFound); //Convert.ToString(nNotFound);  //Convert.ToString(Convert.ToUInt16(TotalDisabledTagsLabel.Text) - Convert.ToUInt16(TotalDisabledResLabel.Text));
                }
                else
                {
                    NumEnabledTaglabel.Text = "0";  //Convert.ToString(Convert.ToUInt16(NumEnabledTaglabel.Text) - nEnableFound);
                    NumEnabledNotFoundLabel.Text = Convert.ToString(nEnableFound); // Convert.ToString(Convert.ToUInt16(NumTotalEnabledTagsLabel.Text) - Convert.ToUInt16(NumEnabledTaglabel.Text));

                    NumDisabledTaglabel.Text = "0";   //Convert.ToString(Convert.ToUInt16(NumDisabledTaglabel.Text) - nDisableFound);
                    NumDisabledNotFoundLabel.Text = Convert.ToString(nDisableFound); //Convert.ToString(Convert.ToUInt16(NumTotalDisabledTagsLabel.Text) - Convert.ToUInt16(NumDisabledTaglabel.Text));                    
                }

                rdrSrchIndex = 0;
                tagSelIndex = 0;
                numTries = 0;
                needUpdateFlag = false;

                searchingAll = false;
                searchingItems = true;
                startSearch = true;

                LoadRdrSearchList();

                SerachTimer.Enabled = false;
                SerachTimer.Interval = 500;
                SerachTimer.Enabled = true;

                toolBar1.Buttons[0].Enabled = false;  //search ALL
                //SrchAllToolBarButton.Enabled = false;

                toolBar1.Buttons[1].Enabled = false;  //search specific
                //SrchSpecToolBarButton.Enabled = false;

                toolBar1.Buttons[2].Enabled = false;  //Clear Search
                //ClearSrchToolBarButton.Enabled = false;

                toolBar1.Buttons[3].Enabled = false;  //clear checks
                //ClearCheckBoxtToolBarButton.Enabled = false;  
                
                Dis_EnabledRadioButton.Enabled = false;
                EnabledRadioButton.Enabled = false;
                DisabledRadioButton.Enabled = false;
            }
            else if (e.Button.Text == "Clear Search")
            {
                serachTagStrID = "";
                needUpdateFlag = false;
                if (searchingAll || searchingItems)
                {
                    MsgForm msgDlg = new MsgForm("Search is in process. Can not clear the list at this time");
                    //Form1.SetVolume(Form1.generalVol);
                    MainForm.PlaySound(1);
                    if (msgDlg.ShowDialog(this) == DialogResult.OK)
                    {
                        return;
                    }
                }

                //searchListView.Items.Clear();
                
                ClearSearch();

                /*if (EnabledRadioButton.Checked)
                {
                    INVListView.Items.Clear();
                    LoadInventoryListView(1);
                    TotalEnabledResLabel.Text = "0";
                    TotalEnabledNotResLabel.Text = "0";
                }
                else if (DisabledRadioButton.Checked)
                {
                    INVDisabledListView.Items.Clear();
                    LoadInventoryListView(2);
                    TotalDisabledResLabel.Text = "0";
                    TotalDisabledNotResLabel.Text = "0";
                    TotalEnabledResLabel.Text = "0";
                    TotalEnabledNotResLabel.Text = "0";
                }
                else
                {
                    INVEnaDisabledListView.Items.Clear();

                    NumEnabledTaglabel.Text = "0";
                    NumEnabledNotFoundLabel.Text = "0";

                    NumDisabledTaglabel.Text = "0";
                    NumDisabledNotFoundLabel.Text = "0";
                                     
                    LoadInventoryListView(3);
                }
                
                ResetMultiTagArray();
                */

                //RefreshINVListView();
            }
            else if (e.Button.Text == "Clear Checks")
            {
                if (searchingAll || searchingItems)
                {
                    MsgForm msgDlg = new MsgForm("Search is in process. Can not clear the list at this time");
                    //Form1.SetVolume(Form1.generalVol);
                    MainForm.PlaySound(1);
                    if (msgDlg.ShowDialog(this) == DialogResult.OK)
                    {
                        return;
                    }
                }
                else
                {
                    for (int i = 0; i < searchListView.Items.Count; i++)
                        searchListView.Items[i].Checked = false;
                }
            }
            else if (e.Button.Text == "Stop Searching")
            {
                StopSearch();

                /*if (searchingAll || searchingItems)
                {
                    searchingAll = false;
                    searchingItems = false;
                    startSearch = false;
                    needUpdateFlag = false;
                    SearchLabel.Text = "Search Stopped.";
                }

                toolBar1.Buttons[0].Enabled = true;  //search ALL
                //SrchAllToolBarButton.Enabled = false;

                toolBar1.Buttons[1].Enabled = true;  //search specific
                //SrchSpecToolBarButton.Enabled = false;

                toolBar1.Buttons[2].Enabled = true;  //Clear Search
                //ClearSrchToolBarButton.Enabled = false;

                toolBar1.Buttons[3].Enabled = true;  //clear checks
                //ClearCheckBoxtToolBarButton.Enabled = false;  
                
                Dis_EnabledRadioButton.Enabled = true;
                EnabledRadioButton.Enabled = true;
                DisabledRadioButton.Enabled = true;

                ResetMultiTagArray();
                */
            }
            else if (e.Button.Text == "Close")
            {
                Close();
            }
        }

        private void StopSearch()
        {
            if (searchingAll || searchingItems)
            {
                searchingAll = false;
                searchingItems = false;
                startSearch = false;
                needUpdateFlag = false;
                SearchLabel.Text = "Search Stopped.";
            }

            toolBar1.Buttons[0].Enabled = true;  //search ALL
            //SrchAllToolBarButton.Enabled = false;

            toolBar1.Buttons[1].Enabled = true;  //search specific
            //SrchSpecToolBarButton.Enabled = false;

            toolBar1.Buttons[2].Enabled = true;  //Clear Search
            //ClearSrchToolBarButton.Enabled = false;

            toolBar1.Buttons[3].Enabled = true;  //clear checks
            //ClearCheckBoxtToolBarButton.Enabled = false;  

            Dis_EnabledRadioButton.Enabled = true;
            EnabledRadioButton.Enabled = true;
            DisabledRadioButton.Enabled = true;

            ResetMultiTagArray();
        }

        private void ClearSearch()
        {
            if (EnabledRadioButton.Checked)
            {
                INVListView.Items.Clear();
                //LoadInventoryListView(1);
                LoadSearchListView();
                TotalEnabledResLabel.Text = "0";
                TotalEnabledNotResLabel.Text = "0";
            }
            else if (DisabledRadioButton.Checked)
            {
                INVDisabledListView.Items.Clear();
                LoadInventoryListView(2);
                TotalDisabledResLabel.Text = "0";
                TotalDisabledNotResLabel.Text = "0";
                TotalEnabledResLabel.Text = "0";
                TotalEnabledNotResLabel.Text = "0";
            }
            else
            {
                INVEnaDisabledListView.Items.Clear();

                NumEnabledTaglabel.Text = "0";
                NumEnabledNotFoundLabel.Text = "0";

                NumDisabledTaglabel.Text = "0";
                NumDisabledNotFoundLabel.Text = "0";

                LoadInventoryListView(3);
            }

            ResetMultiTagArray();
        }

        private void RefreshINVListView()
        {
            if (EnabledRadioButton.Checked)
            {
                LoadInventoryListView(1);
                TotalEnabledTagsLabel.Text = "0";
                TotalEnabledResLabel.Text = "0";
                TotalEnabledNotResLabel.Text = "0";
            }
            else if (DisabledRadioButton.Checked)
            {
                LoadInventoryListView(2);
                TotalDisabledTagsLabel.Text = "0";
                TotalDisabledResLabel.Text = "0";
                TotalDisabledNotResLabel.Text = "0";
            }
            else
            {
                LoadInventoryListView(3);
                NumTotalTagsLabel.Text = "0";
                NumTotalEnabledTagsLabel.Text = "0";
                NumTotalDisabledTagsLabel.Text = "0";
                NumEnabledTaglabel.Text = "0";
                NumEnabledNotFoundLabel.Text = "0";
                NumDisabledTaglabel.Text = "0";
                NumDisabledNotFoundLabel.Text = "0";
            }
        }

        private void DisabledRadioButton_Click(object sender, EventArgs e)
        {
            INVListView.Visible = false;
            INVDisabledListView.Visible = true;
            INVEnaDisabledListView.Visible = false;
            INVDisabledListView.BringToFront();

            EnabledGroupBox.Visible = false;
            DisabledGroupBox.Visible = true;
            DisabledGroupBox.BringToFront();
            EnableDisableGroupBox.Visible = false;

            if (lastDisabledSearchTime == "")
            {
                INVDisabledListView.Items.Clear();

                TotalDisabledTagsLabel.Text = "0";
                TotalDisabledResLabel.Text = "0";
                TotalDisabledNotResLabel.Text = "0";
                SearchLabel.Text = "";

                LoadInventoryListView(2);
            }
            else
            {
                SearchLabel.Text = "Last Search Completed on " + lastDisabledSearchTime;
            }
        }

        private void EnabledRadioButton_Click(object sender, EventArgs e)
        {
            INVListView.Visible = true;
            INVDisabledListView.Visible = false;
            INVEnaDisabledListView.Visible = false;
            INVListView.BringToFront();

            EnabledGroupBox.Visible = true;
            DisabledGroupBox.Visible = false;
            DisabledGroupBox.BringToFront();
            EnableDisableGroupBox.Visible = false;

            if (lastEnabledSearchTime == "")
            {
                INVDisabledListView.Items.Clear();

                TotalEnabledTagsLabel.Text = "0";
                TotalEnabledResLabel.Text = "0";
                TotalEnabledNotResLabel.Text = "0";
                SearchLabel.Text = "";

                LoadInventoryListView(1);
            }
            else
            {
                /*INVListView.Items.Clear();
                LoadInventoryListView(1);

                string tagID = INVListView.Items[int].S
                for (int i = 0; i < INVListView.Items.Count; i++)
                {
                    foreach (lastTagSearchStruct lastTag in LastTagSrchList)
                    {
                        if (lastTag.enabled == tID)
                        {
                            return (true);
                        }
                    }
                }*/

                SearchLabel.Text = "Last Search Completed on " + lastEnabledSearchTime;
            }
        }

        private void Dis_EnabledRadioButton_Click(object sender, EventArgs e)
        {
            INVListView.Visible = false;
            INVDisabledListView.Visible = false;
            INVEnaDisabledListView.Visible = true;
            INVEnaDisabledListView.BringToFront();

            EnabledGroupBox.Visible = false;
            DisabledGroupBox.Visible = false;
            EnableDisableGroupBox.BringToFront();
            EnableDisableGroupBox.Visible = true;

            if (lastEnDisabledSearchTime == "")
            {
                INVEnaDisabledListView.Items.Clear();

                NumTotalTagsLabel.Text = "0";
                NumTotalEnabledTagsLabel.Text = "0";
                NumTotalDisabledTagsLabel.Text = "0";
                NumEnabledTaglabel.Text = "0";
                NumEnabledNotFoundLabel.Text = "0";
                NumDisabledTaglabel.Text = "0";
                NumDisabledNotFoundLabel.Text = "0";
                SearchLabel.Text = "";

                LoadInventoryListView(3);
            }
            else
            {
                SearchLabel.Text = "Last Search Completed on " + lastEnDisabledSearchTime;
            }

        }

        private void INVListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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

                //e.DrawText(flags);
                // Draw the subitem text in red to highlight it. 
                Font font = new Font(INVListView.Font, FontStyle.Bold);

                if (e.ColumnIndex == 0)
                {
                    e.DrawDefault = true;
                }
                else if (e.ColumnIndex == 4)
                {
                    if (e.SubItem.Text == "YES")
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.DarkGreen, e.Bounds, sf);
                    else
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.Red, e.Bounds, sf);
                }
                /*
                else if (e.ColumnIndex == 4)
                {
                    Font font1 = new Font(INVListView.Font, FontStyle.Regular);
                    if (e.SubItem.Text.Contains("**") )                        
                        e.Graphics.DrawString(e.SubItem.Text, font1, Brushes.Fuchsia, e.Bounds, sf);             
                    else
                        e.Graphics.DrawString(e.SubItem.Text, font1, Brushes.DarkBlue, e.Bounds, sf);             

                }
                else if (e.ColumnIndex == 5)
                {
                    if (e.SubItem.Text == "LOW")  //low battery
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.Red, e.Bounds, sf);
                    else
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.DarkGreen, e.Bounds, sf);
                }
                */
                else
                    e.Graphics.DrawString(e.SubItem.Text, INVListView.Font, Brushes.DarkBlue, e.Bounds, sf);

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

            //if (e.ColumnIndex == 0) 
            //{
            //e.DrawDefault = true;
            //}
            //////else if ((e.ColumnIndex == 4) && (e.SubItem.Text == "ENABLED"))
            /////{
            // Unless the item is selected, draw the standard 
            // background to make it stand out from the gradient.
            //if ((e.ItemState & ListViewItemStates.Selected) == 0)
            //{
            //e.DrawBackground();
            //}
            // Draw the subitem text in red to highlight it. 

            //////e.Graphics.DrawString(e.SubItem.Text, INVListView.Font, Brushes.Green, e.Bounds, sf);
            /////}
            /////else if ((e.ColumnIndex == 4) && (e.SubItem.Text == "DISABLED"))
            /////{
            /////e.Graphics.DrawString(e.SubItem.Text, INVListView.Font, Brushes.Red, e.Bounds, sf);
            /////}

            /*if ((e.ColumnIndex == 5) && (e.SubItem.Text == "LOW"))  //low battery
            {
                e.Graphics.DrawString(e.SubItem.Text, INVListView.Font, Brushes.Red, e.Bounds, sf);
            }
            else if (e.ColumnIndex == 3)                     
            {
                if(e.SubItem.Text == "YES")
                   e.Graphics.DrawString(e.SubItem.Text, INVListView.Font, Brushes.DarkGreen, e.Bounds, sf);
                else if (e.SubItem.Text == "NO")
                   e.Graphics.DrawString(e.SubItem.Text, INVListView.Font, Brushes.MediumVioletRed, e.Bounds, sf);
                else 
                   e.Graphics.DrawString(e.SubItem.Text, INVListView.Font, Brushes.Blue, e.Bounds, sf);
            }
            else                                 
                e.DrawDefault = true;

            //else if ((e.ColumnIndex == 3) && (e.SubItem.Text == "NO"))
            //{
                //e.Graphics.DrawString(e.SubItem.Text, INVListView.Font, Brushes.MediumVioletRed, e.Bounds, sf);
            //}
            //else
            //{
                                       
                //if ((e.ItemState & ListViewItemStates.Selected) != 0)
                    //e.Graphics.DrawString(e.SubItem.Text, INVListView.Font, Brushes.Maroon, e.Bounds, sf);
            //}
                
            return;

            // Draw normal text for a subitem with a nonnegative 
            // or nonnumerical value.
            //e.DrawText(flags);*/


        }

        private void INVListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

                // Draw the standard header background.

                e.DrawBackground();

                // Draw the header text.
                using (Font headerFont = new Font("Helvetic", 11, FontStyle.Regular))
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Purple, e.Bounds, sf);
                }
            }

            return;
        }

        private void INVListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //LinearGradientBrush brush;

            if (e.ItemIndex % 2 == 0)
            {
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            }


            //if ((e.State & ListViewItemStates.Selected) != 0)
            //{
            // Draw the background and focus rectangle for a selected item.
            //e.Graphics.FillRectangle(Brushes.LightGoldenrodYellow, e.Bounds);
            //e.DrawFocusRectangle();
            //}
            //else
            //{
            // Draw the background for an unselected item.
            //using (brush = new LinearGradientBrush(e.Bounds, Color.LightSteelBlue,
            //Color.LightSlateGray, LinearGradientMode.Horizontal))
            //{
            //e.Graphics.FillRectangle(brush, e.Bounds);
            //}
            // }

            /*if (e.Item.SubItems[3].Text == "YES")
            {
                e.Graphics.DrawString(e.Item.Text, INVListView.Font, Brushes.DarkSeaGreen, e.Bounds);
                e.DrawDefault = true;
            }
            else if (e.Item.SubItems[3].Text == "NO")
            {
                e.Graphics.DrawString(e.Item.Text, INVListView.Font, Brushes.MediumVioletRed, e.Bounds);
                e.DrawDefault = true;
            }
            else
            {
                e.Graphics.DrawString(e.Item.Text, INVListView.Font, Brushes.Maroon, e.Bounds);
                e.DrawDefault = true;
            }*/
        }

        private void INVDisabledListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            m_sortReverse = (e.Column == m_sortColumn) ? !m_sortReverse : false;
            m_sortColumn = e.Column;
            INVDisabledListView.ListViewItemSorter = this;
            INVDisabledListView.Sort();
            INVDisabledListView.ListViewItemSorter = null;
        }

        private void INVDisabledListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

                // Draw the standard header background.

                e.DrawBackground();

                // Draw the header text.
                using (Font headerFont = new Font("Helvetic", 11, FontStyle.Regular))
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Purple, e.Bounds, sf);
                }
            }

            return;
        }

        private void INVDisabledListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (e.ItemIndex % 2 == 0)
            {
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            }
        }

        private void INVDisabledListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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

                // Draw the subitem text in red to highlight it. 
                Font font = new Font(INVDisabledListView.Font, FontStyle.Bold);

                if (e.ColumnIndex == 0)
                {
                    e.DrawDefault = true;
                }
                else if (e.ColumnIndex == 3)
                {
                    if (e.SubItem.Text == "YES")
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.DarkGreen, e.Bounds, sf);
                    else
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.Red, e.Bounds, sf);
                }
                else if (e.ColumnIndex == 4)
                {
                    Font font1 = new Font(INVListView.Font, FontStyle.Regular);
                    if (e.SubItem.Text.Contains("**"))
                        e.Graphics.DrawString(e.SubItem.Text, font1, Brushes.Fuchsia, e.Bounds, sf);
                    else
                        e.Graphics.DrawString(e.SubItem.Text, font1, Brushes.DarkBlue, e.Bounds, sf);   
                }
                else if (e.ColumnIndex == 5)
                {
                    if (e.SubItem.Text == "LOW")  //low battery
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.Red, e.Bounds, sf);
                    else
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.DarkGreen, e.Bounds, sf);
                }
                else
                    e.Graphics.DrawString(e.SubItem.Text, INVListView.Font, Brushes.DarkBlue, e.Bounds, sf);
            }
        }

        private void INVEnaDisabledListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            m_sortReverse = (e.Column == m_sortColumn) ? !m_sortReverse : false;
            m_sortColumn = e.Column;
            INVEnaDisabledListView.ListViewItemSorter = this;
            INVEnaDisabledListView.Sort();
            INVEnaDisabledListView.ListViewItemSorter = null;
        }

        private void INVEnaDisabledListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

                // Draw the standard header background.

                e.DrawBackground();

                // Draw the header text.
                using (Font headerFont = new Font("Helvetic", 11, FontStyle.Regular))
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Purple, e.Bounds, sf);
                }
            }

            return;
        }

        private void INVEnaDisabledListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (e.ItemIndex % 2 == 0)
            {
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            }
        }

        private void INVEnaDisabledListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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

                // Draw the subitem text in red to highlight it. 
                Font font = new Font(INVEnaDisabledListView.Font, FontStyle.Bold);

                if (e.ColumnIndex == 0)
                {
                    e.DrawDefault = true;
                }
                else if (e.ColumnIndex == 3)
                {
                    if (e.SubItem.Text == "YES")
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.DarkGreen, e.Bounds, sf);
                    else
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.Red, e.Bounds, sf);
                }
                else if (e.ColumnIndex == 4)
                {
                    Font font1 = new Font(INVListView.Font, FontStyle.Regular);
                    if (e.SubItem.Text.Contains("**"))
                        e.Graphics.DrawString(e.SubItem.Text, font1, Brushes.Fuchsia, e.Bounds, sf);
                    else
                        e.Graphics.DrawString(e.SubItem.Text, font1, Brushes.DarkBlue, e.Bounds, sf);   
                }
                else if (e.ColumnIndex == 5)
                {
                    if (e.SubItem.Text == "LOW")  //low battery
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.Red, e.Bounds, sf);
                    else
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.DarkGreen, e.Bounds, sf);
                }
                else
                    e.Graphics.DrawString(e.SubItem.Text, INVListView.Font, Brushes.DarkBlue, e.Bounds, sf);
            }
        }

        private void INVForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            searchingAll = false;
            searchingItems = false;
            startSearch = false;
        }

        private void LocationListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

                // Draw the standard header background.
                e.DrawBackground();

                // Draw the header text.                
                using (Font headerFont = new Font("Helvetic", 9, FontStyle.Regular))
                {
                    sf.Alignment = StringAlignment.Center;
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Purple, e.Bounds, sf);
                }
                
            }
        }

        private void LocationListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Font font = new Font(LocationListView.Font, FontStyle.Regular);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            
            //int color = 0;
            if (e.ItemIndex % 2 == 0)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds); //LightCyan
                //color = 1; //blue
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.LightSteelBlue, e.Bounds);
                //color = 2; //yellow
            }

            if (e.ColumnIndex == 0)
            {
                e.DrawDefault = true;
            }
            else if (e.ColumnIndex == 1)
            {
                Image img = null;

                //if (color == 1)
                    //e.Graphics.DrawString(e.Item.Text, font, Brushes.LightGray, e.Bounds, sf);  //LightCyan
                //else
                    //e.Graphics.DrawString(e.Item.Text, font, Brushes.LightSteelBlue, e.Bounds, sf);

                if (e.Item.SubItems[1].Text == "F")
                {
                    img = SmallImageList.Images[1];
                }
                else if (e.Item.SubItems[1].Text == "N")
                {
                    img = SmallImageList.Images[0];
                }

                if (img != null)
                {
                    int x = e.Bounds.X + 2;
                    int imgy = e.Bounds.Y + ((int)(e.Bounds.Height / 2)) - ((int)(img.Height / 2));
                    e.Graphics.DrawImage(img, x, imgy, img.Width, img.Height);
                    x += img.Width + 2;
                }
            }
            else 
            {
                if (e.Item.SubItems[1].Text == "F")
                {
                    e.Graphics.DrawString(e.SubItem.Text, font, Brushes.Red, e.Bounds, sf);  //LightCyan                    
                    //e.Item.Checked = false;
                }
                else
                    e.Graphics.DrawString(e.SubItem.Text, font, Brushes.Navy, e.Bounds, sf);                
            }
        }

        private void INVForm_Load(object sender, EventArgs e)
        {
            LoadRdrSearchListView();

            /*ListViewItem listItem = new ListViewItem();
            listItem.Checked = false;  //checkbox                    
            listItem.SubItems.Add("F");  //stat
            listItem.SubItems.Add("Main Office");  //loc
            listItem.SubItems.Add("2");  //rdr
            LocationListView.Items.Add(listItem);

            ListViewItem listItem1 = new ListViewItem();
            listItem1.Checked = false;  //checkbox                    
            listItem1.SubItems.Add("F");  //stat
            listItem1.SubItems.Add("Lobby");  //loc
            listItem1.SubItems.Add("3");  //rdr
            LocationListView.Items.Add(listItem1);

            ListViewItem listItem2 = new ListViewItem();
            listItem2.Checked = false;  //checkbox                    
            listItem2.SubItems.Add("N");  //stat
            listItem2.SubItems.Add("Engineering Office");  //loc
            listItem2.SubItems.Add("4");  //rdr
            LocationListView.Items.Add(listItem2);

            ListViewItem listItem3 = new ListViewItem();
            listItem3.Checked = false;  //checkbox                    
            listItem3.SubItems.Add("N");  //stat
            listItem3.SubItems.Add("Marketting Office");  //loc
            listItem3.SubItems.Add("5");  //rdr
            LocationListView.Items.Add(listItem3);

            ListViewItem listItem4 = new ListViewItem();
            listItem4.Checked = false;  //checkbox                    
            listItem4.SubItems.Add("F");  //stat
            listItem4.SubItems.Add("Marketting Office");  //loc
            listItem4.SubItems.Add("6");  //rdr
            LocationListView.Items.Add(listItem4);*/
        
        }

        private void EnabledGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void ClearChkButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LocationListView.Items.Count; i++)
                LocationListView.Items[i].Checked = false;
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LocationListView.Items.Count; i++)
            {
                if (LocationListView.Items[i].SubItems[1].Text == "N")
                   LocationListView.Items[i].Checked = true;
            }
        }

        private void LocationListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (LocationListView.Items[e.Index].SubItems[1].Text == "F")
                e.NewValue = CheckState.Unchecked;
        }

        private void TotalEnabledNotResLabel_Click(object sender, EventArgs e)
        {

        }

        private void INVListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int nChecked = 0;

            for (int i = 0; i < INVListView.Items.Count; i++)
            {
                if (INVListView.Items[i].Checked)
                    nChecked += 1;
            }

            NumTagToSearchLabel.Text = nChecked.ToString();
        }

        #region readerSearchStruct
        [StructLayout(LayoutKind.Sequential)]
        private struct readerSearchStruct
        {
            public ushort readerID;
            public ushort fgenID;
            public ushort count;
        }
        #endregion

        #region lastTagSearchStruct
        [StructLayout(LayoutKind.Sequential)]
        private struct lastTagSearchStruct
        {
            public string tagID;
            public bool found;
            public bool battery;
            public bool enabled;

            public lastTagSearchStruct(string id, bool tagFound, bool tagBattery, bool tagEnable)
            {
                tagID = id;
                found = tagFound;
                battery = tagBattery;
                enabled = tagEnable;
            }
        }
        #endregion

        #region tagSearchStruct
        [StructLayout(LayoutKind.Sequential)]
        private struct tagSearchStruct
        {
            public string tagID;
            public bool found;
        }
        #endregion
    }//INVForm class
}//namespace
