//********************************
// SERVER Main Form Version 3.0.0
//********************************
//------------------------------------------------------------------------------------------
// Change History
//
// Tag						Date		Description
// ----------------------	-----------	----------------------------------------------------
// [deprecated]             03/15/2011  Bank stuff related...
//
//------------------------------------------------------------------------------------------

#region Changes
// V1.1	 Feb01,06  - Added Tag detected with RSSI event and event handler.
//
// V3.0  Aug29,06  - Added CommunicationClass lock to the TagDetected() routine and
//                   changed the order of myReader.GetString(0), (1), .. To prevent
//                   exception and let MyReader.Close() to be called.
//                 - Added CommunicationClass lock to the AWIComponentLib Version 2.0
//                   to the TagDetected() and TagDetectedRSSI(). 
//
// V19.0 Dec14,06  - Modified code to eliminate multiple refresh if a reader is in RS232 mode that keeps
//                   sending powerup packets while the application is in network mode.
//
// V20.0 Dec28,06  - 1 - Fixed the problem with refresh function in the Zone form. (threw exception).
//                   2 - Fixed the problem with closing socket when the reader id > 128.(showing big
//                       big number for reader ID.
//                   3 - Fixed the problem with specific ip in the comm form. (when the specific ip
//                       radio button was selected timer would threw exception.
//
// V21.0 Jan10,07    1 - Added support for RSSI to validate detected tags.                       
//
// V22.0 Jan22, 07   1 - Added protection in powerup notify when updating netip table if GetZone() 
//                       returns null insert blank for that field.
//
// V24.0 Feb01, 07   1 - Adding Report to the application. 
//
// V25.0 Feb01, 07   1 - Adding Report to the application. 
//
// V26.0 Feb12, 07   1 - Fixed the proplem with RSSI functionality. 
//
// V27.0 Feb26, 07   1 - Updated the API DLL from v37 to v38. 
//
// V28.0 Mar01, 07   1 - Updated the API DLL from v38 to v39. 
//
// V29.0 Mar13, 07   1 - Fixed te problem with scrolling main client area(ledger) 
//
// V31.0 Mar21, 07   1 - Fixed te problem with report 
// 
// V32.0 Apr02, 07   1 - Fixed te problem with report and setting the system environment 
//
// V33.0 Apr16, 07   1 - Fixed te problem for seting the tag duplication exp time in config window. 
// 
// V34.0 May04, 07   1 - Fixed te problem when the connection to the MySQL server is gone or when the 
//                       cable is unplugged there will be a msg to the user and the application will
//                       try to reconnect to DB Server.
// 
// V3.0.0 Oct16, 07  1 - Changed mysql database server vesion from 4.1 to 5.0                     

#endregion


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using System.Data.Odbc;
using System.Data;
using AW_API_NET;
using System.Text;
using System.Diagnostics;
using AWIComponentLib.Communication;
using AWIComponentLib.Events;
using AWIComponentLib.Database;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using System.Globalization;
using AWI.Comm;
using System.Linq;

using AWIComponentLib.NetRdrConn;
using AWIComponentLib.Tag;
using AWIComponentLib.Utility.Logging;
using System.Timers;
using System.Collections.Generic;
using AWI.SmartTracker.Properties;
using AWI.SmartTracker.ReportClass;
using System.Reflection;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	//public delegate void ScanNetwork();

	public enum eventType {scan, sockConnect, powerup, closeSocket, readerOffline};
	public enum dbProvider {SQL, MySQL, Oracle};
	public delegate void UpdateIPListView(string ip, int rdr, int host, string status, string rdrStat, eventType e);
	public delegate void UpdateCommStatus(string stat, bool conn);
	public delegate void UpdateRSSIListView(AW_API_NET.rfTagEvent_t tagEvent);
	public delegate void StartTimer1();
    public delegate void CloseMsgBoxDelegate();

    //Visual Studio V2005 changes
    public delegate void UpdateMainStatusBarTextCallback(int panel, string text);
    public delegate void UpdateMainStatusBarIconCallback(int panel, Icon icon);
    public delegate void UpdateMainStatusBarCallback(string panel, string text, Icon icon);
    public delegate void AddIpListviewItemCallback(ListViewItem item);
    public delegate bool RunNonQueryCmdCallback(string sqlStr);
    public delegate void CloseWindowDelegate();


	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.ToolBarButton CommToolBarButton;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.StatusBarPanel ComStatusBarPanel;
		private System.Windows.Forms.StatusBarPanel TimeStatusBarPanel;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.ComponentModel.IContainer components;
		public APINetClass api = new APINetClass();
		public AWIHelperClass awiHelper = new AWIHelperClass();
		private System.Windows.Forms.ToolBarButton ZonesToolBarButton;
		private System.Windows.Forms.ToolBarButton AccRegToolBarButton;
		public CommForm CommDialog = null;
		
        public event UpdateIPListView m_updateIPListView;
		public event StartTimer1 m_startTimer1Event;
		public event UpdateCommStatus m_updateCommStatus;
		public event UpdateRSSIListView m_updateRSSIListView;
        public static CloseMsgBoxDelegate m_closeMsgBoxEvent;

		public static bool rs232Comm = false;
		public static bool netConnection = false;
		public static uint baudrate;
		public static uint commPort;
		public static int spanDays;
		private ushort pktID = 1;
		private ushort pID = 0;
        private uint rs232Port;
        private static bool networkReady = false;
		public static bool dbDisconnectedFlag;
		public static OdbcConnection m_connection = null;
        private NetRdrConnClass NetRdrDisconnection;
		private Object myLock = new Object();
		private Object myLock1 = new Object();
		private Object timerLock = new object();
		private Object tagDetectLock = new object();
		private Object GetParkInfoLock = new object();
		private object disconnLock = new object();
		private object activityTagLock = new object();
		private object historyLock = new object();
		private object errorLock = new object();
		//private Object myTimer1Lock = new object();
		//private Object myTimer2Lock = new object();
		public static CommunicationClass communication;
		public OdbcDbClass odbcDB = new OdbcDbClass();
		private APIEventHandler eventHandler = new APIEventHandler();
		private APIEventHandler myEventHandler = new APIEventHandler();
		public static bool service = false;
		private System.Timers.Timer Relay1Timer;
		private System.Timers.Timer Relay2Timer;
        private System.Timers.Timer Input1Timer;
        private System.Timers.Timer Input2Timer;
        private int MAX_DEMO_RECORDS = 10;
		public static int accRecCount = 0;
		public	static int astRecCount = 0;
		public	static int zoneRecCount = 0;
		public	static bool mdActiveHi;
		private System.Timers.Timer ClockTimer;
		//private bool initListView = false;
		private enum relayStatus {Open, Close};
		private static relayStatus relay1Stat;
		private static relayStatus relay2Stat;
		private System.Windows.Forms.Timer CmdSyncTimer;
		private System.Windows.Forms.Timer CleanupTimer;
		private static ushort readerID;
        public static bool closingApp = false;
		public ushort rs232Reader;
		private MsgForm dbDisconnectMsg;
		//private static bool setRelay01Enable = false;
		//private static bool setRelay02Enable = false;
		private static bool disableRelay01 = false;
		private static bool disableRelay02 = false;
		private bool autoReset = false;
		private ushort counter01 = 0;
		private ushort counter02 = 0;
		//private static ushort cmdIndex = 0;
		private static CmdCollectionClass cmdCollection = new CmdCollectionClass();
		private int lastOpenRelay01Time = 0;
		private int lastOpenRelay02Time = 0;
		private static int resetCounter = 0;
		private static DataCollectionClass dataCollection = new DataCollectionClass();
		private static RelayCollectionClass relayCollection = new RelayCollectionClass();
		private System.Windows.Forms.ToolBarButton HelpToolBarButton;
		private System.Windows.Forms.TabPage m_tabReaderHistory;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem CLearList;
		public static bool socketsConnection = false;
		private static bool CommDialogActivated = false;
        public static event CloseWindowDelegate m_closeWindowEvent;
		//private ActiveWave.CarTracker.AccessImageView accessImageView1;
		//private ActiveWave.CarTracker.AssetImageView assetImageView1;
		private ActiveWave.CarTracker.AccessImageView accessImageView2;
		private ActiveWave.CarTracker.AssetImageView assetImageView2;
		//private ActiveWave.CarTracker.AccessImageView accessImageView1;

		private int testCount = 0;
		private ActiveWave.CarTracker.AccessImageView accessImageView1;
        private ActiveWave.CarTracker.AssetImageView assetImageView1;
		private bool sendTagNotified = false;
		private tagDisplayInfoStruct tagDisplayInfo;
		private System.Windows.Forms.OpenFileDialog CReportOpenFileDialog;
		private tagDisplayCollectionClass tagDisplayCollection = new tagDisplayCollectionClass();
		public static dbProvider providerName = dbProvider.MySQL;  //MySQL server
		//public static dbProvider providerName = dbProvider.SQL;      //SQL server
		public static string server = "";
		public static string database = "";
		public static string serverMySQL = "";
		public static string databaseMySQL = "";
		public static string user = "";
		public static string password = "";
        public static ushort rs232Connection;
        public static ushort networkConnection;
		private bool startPolling;
		private bool dbConnDisplay = false;
		private System.Windows.Forms.ToolBarButton DBToolBarButton;
		private System.Windows.Forms.ToolBarButton MapToolBarButton;
		private System.Windows.Forms.ToolBarButton TempTagtoolBarButton;
		private System.Windows.Forms.ToolBarButton ReportToolBarButton;
		public static string conStr = "";
		public static object powerupLocker = new object();
		private System.Windows.Forms.ToolBarButton AssetToolBarButton;

        private ThreadStart rs232TStart;
        private ThreadStart netTStart;
        private Thread rs232Thread;
        private Thread netThread;

		public bool listIsUpdated = false;  //disconnect sockets
		private DateTime lastTime;
		private bool dbConnectDlgDisplay;
		const int MAX_RETRY_DB_CONNECT = 3;
		
		private object timer2Lock = new object();
		private ReaderStatusStruct[] readerStatus = new ReaderStatusStruct[100]; 
		public static ReaderPowerupStruct[] readerPowerup = new ReaderPowerupStruct[100];
		private int numRdrs = 0;
		private int rdrIndexPoll = 0;
		private bool statusChanged = false;
		private byte[] disconIp;
		public static bool timerStop = true;
		public static ushort tagDupExpTime;
		private System.Windows.Forms.ToolBarButton ConfigTtoolBarButton;
		public static int reconnectCounter = -1;
		private System.Timers.Timer DbStatusTimer;
		private bool reconnecting = false;
		private bool msgDisplayed = false;
		public System.Timers.Timer PollDbTimer;
		private string tempStrConnect = "";
		//public CarTracker.AWIHistoryControl awiHistoryControl1;
		private bool closingProcess = false;
		private bool RdrOffline = false;
		private bool showOffRdrIcon = false;
		private uint rdrsStatus = 0;
		private System.Windows.Forms.StatusBarPanel CommStatusBarPanel;
		public System.Windows.Forms.StatusBarPanel DBStatusBarPanel;
		private System.Windows.Forms.StatusBarPanel DateTimeStatusBarPanel;
		private System.Windows.Forms.StatusBarPanel VersionStatusBarPanel;
		private System.Windows.Forms.StatusBarPanel statusBarPanel2;
		private System.Windows.Forms.StatusBar MainStatusBar;
        //public CarTracker.AWIHistoryControl awiHistoryControl1;
		private System.Windows.Forms.Splitter splitter1;
        private AWI.SmartTracker.AWIHistoryControl awiHistoryControl1 = new AWI.SmartTracker.AWIHistoryControl();
        private ToolBarButton ActionToolBarButton;
        private ListView listView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
		//public CarTracker.AWIHistoryControl awiHistoryControl1;
		private PowerupStruct powerup = new PowerupStruct();
        private ArrayList actionList = new ArrayList();
        private Thread handleEventActionThread;
        private ListViewItem[] lvi;
        private const int ALARM_VIRTUAL_LIST_SIZE = 30000;
        private int virtualListIndex = 0;
        private int lastTrafficIndex = 0;
        private int lastSaniTrafficIndex = 0;
        private bool virtualListLoaded = false;
        private ushort loadCount = 0;
        private LoadMsgForm loadMsg = null;

        //Actions-------------------------------------------
        public const int UNLOCK_DOOR_RELAY_01 = 100;
        public const int TURN_ON_ALARM_LIGHT_RELAY_01 = 101;
        public const int TURN_ON_SIREN_RELAY_01 = 102;

        public const int UNLOCK_DOOR_RELAY_02 = 200;
        public const int TURN_ON_ALARM_LIGHT_RELAY_02 = 201;
        public const int TURN_ON_SIREN_RELAY_02 = 202;

        public const int DISPLAY_TAG_MOVING_DIRECTION = 250;

        public const int EVENT_INPUT_01 = 300;
        public const int EVENT_INPUT_02 = 301;
        public const int EVENT_CALL_TAG_ACCESS = 310;
        public const int EVENT_CALL_TAG_ASSET = 311;
        public const int EVENT_CALL_TAG_INVENTORY = 312;
        public const int EVENT_EMAIL_ACTIVITY = 320;

        //Events ------------------------------------------
        public const int BREACH_ALARM_EVENT = 100;
        public const int TAMPER_ALARM_EVENT = 105;
        public const int ALERT_EVENT = 110;
        public const int TAG_DETECTED = 115;
        public const int INVALID_TAG_DETECTED = 120;
        public const int INPUT_DETECTED = 125;


        private StatusBarPanel NetCommStatusBarPanel;
        private ToolBarButton SearchToolBarButton;
        private ImageList SmallImageList;
        private ToolBarButton toolBarButton;
        private ToolBarButton toolBarButton1;
        private StatusBarPanel SysStatusBarPanel;


        private string ConnString;

		//private uint trafficCounter = 0;

		//public OdbcDataReader myGlobReader = null;

		//= new NotifyDBConnectionStatus(); 

		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		//int myItem = 0;
		public MainForm(int id)
		{
			//changes made to support MYSQL Server v5.0 and later
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

			//dbDisconnectedFlag = false;
		}


		public MainForm()
		{
			InitializeComponent();

#if LITE
            toolBar.Buttons.Remove(TempTagtoolBarButton);
            toolBar.Buttons.Remove(AssetToolBarButton);
            toolBar.Buttons.Remove(DBToolBarButton);
            toolBar.Buttons.Remove(MapToolBarButton);
            toolBar.Buttons.Remove(SearchToolBarButton);
#else
            // 
            // awiHistoryControl1
            // 
            this.awiHistoryControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.awiHistoryControl1.Location = new System.Drawing.Point(0, 466);
            this.awiHistoryControl1.Name = "awiHistoryControl1";
            this.awiHistoryControl1.Size = new System.Drawing.Size(984, 174);
            this.awiHistoryControl1.TabIndex = 44;
            this.Controls.Add(this.awiHistoryControl1);
            this.Controls.SetChildIndex(this.awiHistoryControl1, 2);
#endif

#if SANI
            listView.Columns.Add("SaniStatus", "Sani Unit", 106).TextAlign = HorizontalAlignment.Center;
            listView.Columns.Add("SaniUnitType", "Sani Status", 154).TextAlign = HorizontalAlignment.Center;

            ToolBarButton  StatusSearchButton = new System.Windows.Forms.ToolBarButton();
            StatusSearchButton.ImageIndex = 26;
            StatusSearchButton.Text = "Status";
            StatusSearchButton.ToolTipText = "Search for Sani Tags";

            ToolBarButton StatsButton = new System.Windows.Forms.ToolBarButton();
            StatsButton.ImageIndex = 18;
            StatsButton.Text = "Statistics";
            StatsButton.ToolTipText = "Statistics on Sani Tags";

            toolBar.Buttons.Remove(HelpToolBarButton);
            toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] { StatusSearchButton, StatsButton, HelpToolBarButton });
#endif

            string title = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(
                Assembly.GetExecutingAssembly(), typeof(AssemblyTitleAttribute), false))
               .Title;

            StringBuilder desc = new StringBuilder(title);
#if LITE
            desc.Append(" Lite");
#endif

#if SANI
            desc.Append(" (Sani Faucet)");
#endif
            this.Text = desc.ToString();


            //changes made to support MYSQL Server v5.0 and later
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

            dbConnectDlgDisplay = true;
            using (LoginForm loginDlg = new LoginForm())
            {
                if (loginDlg.ShowDialog() != DialogResult.OK)
                {
                    dbConnectDlgDisplay = false;
                    Application.Exit();
                    return;
                }
            }
            dbConnectDlgDisplay = false;

		    communication = new CommunicationClass();
            NetRdrDisconnection = new NetRdrConnClass(communication);

	        dbDisconnectedFlag = false;
			dbDisconnectMsg = null;
			startPolling = false;
	        //SetEnvVars(0);

			powerup.ip = "";
			powerup.timestamp = DateTime.Now;

			for (int i=0; i<100; i++)
			{
				readerPowerup[i].ip = "";
				readerPowerup[i].online = false;
				readerPowerup[i].counter = 0;
			}

            
            lvi = new ListViewItem[ALARM_VIRTUAL_LIST_SIZE];

			CommunicationClass.PowerupEventHandler += new PowerupEvent(this.PowerupReaderNotifty);
			CommunicationClass.RdrErrorEventHandler += new RdrErrorEvent(this.ErrorEventNotify);
			CommunicationClass.ResetReaderEventHandler += new ResetReaderEvent(this.ResetReaderEventNotify);
			CommunicationClass.EnableReaderEventHandler += new EnableReaderEvent(this.EnableReaderEventNotify);
			CommunicationClass.ScanNetworkEventHandler += new ScanNetworkEvent(this.ScanNetworkEventNotify);
			CommunicationClass.OpenSocketEventHandler += new OpenSocketEvent(this.OpenSocketEventNotify);
			CommunicationClass.CloseSocketEventHandler += new CloseSocketEvent(this.CloseSocketEventNotify);
			CommunicationClass.EnableTagEventHandler += new EnableTagAckEvent(this.EnableTagEventNotify);
			CommunicationClass.QueryTagEventHandler += new QueryTagAckEvent(this.QueryTagEventNotify);
			CommunicationClass.InputChangeEventHandler += new InputChangeEvent(this.InputChangeEventNotify);

            CommunicationClass.TagDetectedRSSITamperEventHandler += new TagDetectedRSSITamperEvent(TagDetectedRSSI);
            CommunicationClass.TagDetectedTamperEventHandler += new TagDetectedTamperEvent(TagDetected);

			APIEventHandler.AppTagDetectedEventHandler += new AppTagDetectedEvent(TagDetected);
			APIEventHandler.AppTagDetectedRSSIEventHandler += new AppTagDetectedRSSIEvent(TagDetectedRSSI);
            APIEventHandler.AppTagDetectedSaniEventHandler += new AppTagDetectedSaniEvent(TagDetectedSani);
			
			OdbcDbClass.NotifyDBConnectionStatusHandler += new NotifyDBConnectionStatus(DBConnectionStatusHandler);
			
			CommunicationClass.EnableRelayAckEventHandler += new EnableRelayAckEvent(EnableRelayAck);
			CommunicationClass.DisableRelayAckEventHandler += new DisableRelayAckEvent(DisableRelayAck);


			//Connect("localhost", "3306", "root", "AWMYSQL", "cartracker");
			//Connect("localhost", "3306", "root", "", "parkingtracker");

			relay1Stat = relayStatus.Close;
			relay2Stat = relayStatus.Close;
			//ClockTimer.Enabled = true;

			lastTime = DateTime.Today;

            rs232TStart = new ThreadStart(StartRS232);
            netTStart = new ThreadStart(StartNetwork);
            rs232Thread = new Thread(rs232TStart);
            netThread = new Thread(netTStart);

			/// <summary>
			/// The main entry point for the application.
			/// </summary>

			RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Active Wave\\Smart Tracker\\");
			try
			{
				spanDays = Convert.ToInt32(reg.GetValue("viewDays"));
			}
			catch
			{
				spanDays = 1;
			}

			try
			{
				mdActiveHi = Convert.ToBoolean(reg.GetValue("mdActiveHi"));
			}
			catch
			{
				reg.SetValue("mdActiveHi", false);
				mdActiveHi = false;
			}
			reg.SetValue("viewDays", MainForm.spanDays);

			try
			{
				if (reg.GetValue("tagDupExpTime") == null)
				{
					reg.SetValue("tagDupExpTime", 10);
					tagDupExpTime = 10;
				}
				else
				    tagDupExpTime = Convert.ToUInt16(reg.GetValue("tagDupExpTime"));
			}
			catch
			{
				reg.SetValue("tagDupExpTime", 10);
				tagDupExpTime = 10;
			}

			DateTime dt = DateTime.Today;
			DateTime dt1 = DateTime.Today.AddDays(spanDays);
			TimeSpan dt2 = dt1 - dt;
			DateTime dt3 = DateTime.Today.Subtract(dt2);
			lastTime = dt3;

            try
            {
                rs232Connection = Convert.ToUInt16(reg.GetValue("RS232"));
            }
            catch
            {
                rs232Connection = 0;
            }

            try
            {
                networkConnection = Convert.ToUInt16(reg.GetValue("Network"));
            }
            catch
            {
                networkConnection = 0;
            }

            if ((rs232Connection == 0) && (networkConnection == 0))
                networkConnection = 1;

			if (MainForm.providerName == dbProvider.SQL)
			{
				string s = "";
				s = "Driver={SQL Native Client};";
				s += "Server=" + MainForm.server;
				s += ";";
				s += "Database=" + MainForm.database;
				s += ";";
				s += "Trusted_Connection=yes;Pooling=False;";
				//if (!odbcDB.Connect("Driver={SQL Native Client};Server=MainForm.server;Database=MainForm.database;Trusted_Connection=yes;Pooling=False;"))  //SQL
                
                ConnString = s;

                if (!odbcDB.Connect(s))  //SQL
				{	
					//conStr = "";
                    MessageBox.Show(this, "Could not connect to database", "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
	
				conStr = s;
			}
			else if (MainForm.providerName == dbProvider.MySQL)
			{
				string s = "";
				s = "DRIVER={MySQL ODBC 3.51 Driver};";
				s += "SERVER=" + MainForm.serverMySQL;
				s += ";";
				s += "DATABASE=" + MainForm.database;
				s += ";";
				s += "USER=" + MainForm.user;
				s += ";";
				s += "PASSWORD=" + MainForm.password;
                s += ";OPTION=3;";

                ConnString = s;

                //if (!odbcDB.Connect("DRIVER={MySQL ODBC 3.51 Driver};SERVER=localhost;DATABASE=parkingtracker;USER=root;PASSWORD=;OPTION=3;"))	//MYSQL
				if (!odbcDB.Connect(s, true))	//MYSQL
				{	
					tempStrConnect = s;
					dbConnDisplay = false;
					conStr =  s;
					//timer4.Enabled = true;
					return;
				}

				conStr = s;
			}
			else
			{
				return;
			}

			//RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Active Wave\\bank\\");
			//reg.SetValue("server", MainForm.server);
			reg.SetValue("database", MainForm.database);
			reg.SetValue("user", MainForm.user);
			//trafficCounter = Convert.ToUInt32(reg.GetValue("count"));

			if (MainForm.providerName == dbProvider.SQL)
			{
				reg.SetValue("provider", "SQL");
				reg.SetValue("server", MainForm.server);
			}
			else
			{
				reg.SetValue("provider", "MySQL");
				reg.SetValue("serverMySQL", MainForm.serverMySQL);
			}


            awiHelper.StatusParkingSlotEvent += new AWI.SmartTracker.AWIHelperClass.StatusParkingSlotHandler(StatusParkingSlot);
            rs232Port = Convert.ToUInt32(reg.GetValue("port"));

            ReaderOutputRelayManager.Elapsed += new ReaderOutputRelayElapsedHandler(ReaderOutputRelayManager_Elapsed);
		}

        void ReaderOutputRelayManager_Elapsed(ushort reader, ushort relay, string description, DateTime time, uint tag_id, TagType tag_type)
        {
            Console.WriteLine("ReaderOutputRelay: Reader {0} Relay {1} Description {2} Time {3} TagID {4} TagType {5}", reader, relay, description, time, tag_id, tag_type);
            communication.DisableOutputRelay(reader, relay);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.CommToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.AccRegToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.TempTagtoolBarButton = new System.Windows.Forms.ToolBarButton();
            this.AssetToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.ZonesToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.DBToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.MapToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.ReportToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.ConfigTtoolBarButton = new System.Windows.Forms.ToolBarButton();
            this.ActionToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.SearchToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.HelpToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.ComStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.TimeStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.CLearList = new System.Windows.Forms.MenuItem();
            this.m_tabReaderHistory = new System.Windows.Forms.TabPage();
            this.Relay1Timer = new System.Timers.Timer();
            this.Relay2Timer = new System.Timers.Timer();
            this.ClockTimer = new System.Timers.Timer();
            this.CleanupTimer = new System.Windows.Forms.Timer(this.components);
            this.CmdSyncTimer = new System.Windows.Forms.Timer(this.components);
            this.CReportOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.DbStatusTimer = new System.Timers.Timer();
            this.PollDbTimer = new System.Timers.Timer();
            this.CommStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.DBStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.DateTimeStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.VersionStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
            this.MainStatusBar = new System.Windows.Forms.StatusBar();
            this.SysStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.NetCommStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SmallImageList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ComStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Relay1Timer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Relay2Timer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClockTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PollDbTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateTimeStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VersionStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SysStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NetCommStatusBarPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.CommToolBarButton,
            this.AccRegToolBarButton,
            this.TempTagtoolBarButton,
            this.AssetToolBarButton,
            this.ZonesToolBarButton,
            this.DBToolBarButton,
            this.MapToolBarButton,
            this.ReportToolBarButton,
            this.ConfigTtoolBarButton,
            this.ActionToolBarButton,
            this.SearchToolBarButton,
            this.HelpToolBarButton,
            this.toolBarButton1});
            this.toolBar.ButtonSize = new System.Drawing.Size(38, 42);
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.imageList;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(984, 67);
            this.toolBar.TabIndex = 0;
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
            this.toolBar.Click += new System.EventHandler(this.toolBar_Click);
            // 
            // CommToolBarButton
            // 
            this.CommToolBarButton.ImageIndex = 13;
            this.CommToolBarButton.Name = "CommToolBarButton";
            this.CommToolBarButton.Text = "Comm";
            this.CommToolBarButton.ToolTipText = "Communication Configuration";
            // 
            // AccRegToolBarButton
            // 
            this.AccRegToolBarButton.ImageIndex = 14;
            this.AccRegToolBarButton.Name = "AccRegToolBarButton";
            this.AccRegToolBarButton.Text = "Employee";
            this.AccRegToolBarButton.ToolTipText = "Employee Tag Definition";
            // 
            // TempTagtoolBarButton
            // 
            this.TempTagtoolBarButton.ImageIndex = 27;
            this.TempTagtoolBarButton.Name = "TempTagtoolBarButton";
            this.TempTagtoolBarButton.Text = "Visitor";
            this.TempTagtoolBarButton.ToolTipText = "Visitor Tag Definition";
            // 
            // AssetToolBarButton
            // 
            this.AssetToolBarButton.ImageIndex = 20;
            this.AssetToolBarButton.Name = "AssetToolBarButton";
            this.AssetToolBarButton.Text = "Asset";
            // 
            // ZonesToolBarButton
            // 
            this.ZonesToolBarButton.ImageIndex = 11;
            this.ZonesToolBarButton.Name = "ZonesToolBarButton";
            this.ZonesToolBarButton.Text = "Zone";
            this.ZonesToolBarButton.ToolTipText = "Zone Definition  Form";
            // 
            // DBToolBarButton
            // 
            this.DBToolBarButton.ImageIndex = 15;
            this.DBToolBarButton.Name = "DBToolBarButton";
            this.DBToolBarButton.Text = "Database";
            this.DBToolBarButton.ToolTipText = "Database Definition Form";
            // 
            // MapToolBarButton
            // 
            this.MapToolBarButton.ImageIndex = 16;
            this.MapToolBarButton.Name = "MapToolBarButton";
            this.MapToolBarButton.Text = "Mapper";
            this.MapToolBarButton.ToolTipText = "Mapper Application";
            // 
            // ReportToolBarButton
            // 
            this.ReportToolBarButton.ImageIndex = 18;
            this.ReportToolBarButton.Name = "ReportToolBarButton";
            this.ReportToolBarButton.Text = "Report";
            // 
            // ConfigTtoolBarButton
            // 
            this.ConfigTtoolBarButton.ImageIndex = 24;
            this.ConfigTtoolBarButton.Name = "ConfigTtoolBarButton";
            this.ConfigTtoolBarButton.Text = "Configure";
            this.ConfigTtoolBarButton.Visible = false;
            // 
            // ActionToolBarButton
            // 
            this.ActionToolBarButton.ImageIndex = 25;
            this.ActionToolBarButton.Name = "ActionToolBarButton";
            this.ActionToolBarButton.Text = "Action";
            this.ActionToolBarButton.ToolTipText = "Configure Input / Output Relay";
            // 
            // SearchToolBarButton
            // 
            this.SearchToolBarButton.ImageIndex = 26;
            this.SearchToolBarButton.Name = "SearchToolBarButton";
            this.SearchToolBarButton.Text = "Search";
            this.SearchToolBarButton.ToolTipText = "Search for tags";
            // 
            // HelpToolBarButton
            // 
            this.HelpToolBarButton.ImageIndex = 28;
            this.HelpToolBarButton.Name = "HelpToolBarButton";
            this.HelpToolBarButton.Text = "Help";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Visible = false;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            this.imageList.Images.SetKeyName(8, "");
            this.imageList.Images.SetKeyName(9, "");
            this.imageList.Images.SetKeyName(10, "");
            this.imageList.Images.SetKeyName(11, "");
            this.imageList.Images.SetKeyName(12, "");
            this.imageList.Images.SetKeyName(13, "");
            this.imageList.Images.SetKeyName(14, "");
            this.imageList.Images.SetKeyName(15, "");
            this.imageList.Images.SetKeyName(16, "map32.bmp");
            this.imageList.Images.SetKeyName(17, "2.ico");
            this.imageList.Images.SetKeyName(18, "");
            this.imageList.Images.SetKeyName(19, "");
            this.imageList.Images.SetKeyName(20, "");
            this.imageList.Images.SetKeyName(21, "");
            this.imageList.Images.SetKeyName(22, "");
            this.imageList.Images.SetKeyName(23, "");
            this.imageList.Images.SetKeyName(24, "config.bmp");
            this.imageList.Images.SetKeyName(25, "Action.ico");
            this.imageList.Images.SetKeyName(26, "BINOCULR.bmp");
            this.imageList.Images.SetKeyName(27, "guest32.bmp");
            this.imageList.Images.SetKeyName(28, "help32.bmp");
            // 
            // ComStatusBarPanel
            // 
            this.ComStatusBarPanel.Name = "ComStatusBarPanel";
            // 
            // TimeStatusBarPanel
            // 
            this.TimeStatusBarPanel.Name = "TimeStatusBarPanel";
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.Name = "statusBarPanel1";
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.CLearList});
            // 
            // CLearList
            // 
            this.CLearList.Index = 0;
            this.CLearList.Text = "Clear List";
            this.CLearList.Click += new System.EventHandler(this.ClearList_Click);
            // 
            // m_tabReaderHistory
            // 
            this.m_tabReaderHistory.Location = new System.Drawing.Point(4, 22);
            this.m_tabReaderHistory.Name = "m_tabReaderHistory";
            this.m_tabReaderHistory.Size = new System.Drawing.Size(650, 166);
            this.m_tabReaderHistory.TabIndex = 1;
            this.m_tabReaderHistory.Text = "Reader History";
            // 
            // Relay1Timer
            // 
            this.Relay1Timer.Interval = 5000D;
            this.Relay1Timer.SynchronizingObject = this;
            this.Relay1Timer.Elapsed += new System.Timers.ElapsedEventHandler(this.Relay1Timer_Elapsed);
            // 
            // Relay2Timer
            // 
            this.Relay2Timer.Interval = 7000D;
            this.Relay2Timer.SynchronizingObject = this;
            this.Relay2Timer.Elapsed += new System.Timers.ElapsedEventHandler(this.Relay2Timer_Elapsed);
            // 
            // ClockTimer
            // 
            this.ClockTimer.Interval = 1000D;
            this.ClockTimer.SynchronizingObject = this;
            this.ClockTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.ClockTimer_Elapsed);
            // 
            // CleanupTimer
            // 
            this.CleanupTimer.Enabled = true;
            this.CleanupTimer.Interval = 2500;
            this.CleanupTimer.Tick += new System.EventHandler(this.CleanupTimer_Tick);
            // 
            // CmdSyncTimer
            // 
            this.CmdSyncTimer.Interval = 6000;
            this.CmdSyncTimer.Tick += new System.EventHandler(this.CmdSyncTimer_Tick);
            // 
            // CReportOpenFileDialog
            // 
            this.CReportOpenFileDialog.Filter = "Crystal report files|*.rpt";
            this.CReportOpenFileDialog.InitialDirectory = "c:\\";
            this.CReportOpenFileDialog.ShowHelp = true;
            this.CReportOpenFileDialog.Title = "Crystal Report Files Dir";
            // 
            // DbStatusTimer
            // 
            this.DbStatusTimer.Enabled = true;
            this.DbStatusTimer.Interval = 1650D;
            this.DbStatusTimer.SynchronizingObject = this;
            this.DbStatusTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.DbStatusTimer_Elapsed);
            // 
            // PollDbTimer
            // 
            this.PollDbTimer.Enabled = true;
            this.PollDbTimer.Interval = 2000D;
            this.PollDbTimer.SynchronizingObject = this;
            this.PollDbTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.PollDbTimer_Elapsed);
            // 
            // CommStatusBarPanel
            // 
            this.CommStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.CommStatusBarPanel.Icon = ((System.Drawing.Icon)(resources.GetObject("CommStatusBarPanel.Icon")));
            this.CommStatusBarPanel.MinWidth = 0;
            this.CommStatusBarPanel.Name = "CommStatusBarPanel";
            this.CommStatusBarPanel.Text = "RS232 Reader: Offline";
            this.CommStatusBarPanel.Width = 149;
            // 
            // DBStatusBarPanel
            // 
            this.DBStatusBarPanel.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.DBStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.DBStatusBarPanel.Icon = ((System.Drawing.Icon)(resources.GetObject("DBStatusBarPanel.Icon")));
            this.DBStatusBarPanel.Name = "DBStatusBarPanel";
            this.DBStatusBarPanel.Text = "DB Server: Disconnected";
            this.DBStatusBarPanel.Width = 162;
            // 
            // DateTimeStatusBarPanel
            // 
            this.DateTimeStatusBarPanel.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.DateTimeStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.DateTimeStatusBarPanel.Name = "DateTimeStatusBarPanel";
            this.DateTimeStatusBarPanel.Width = 10;
            // 
            // VersionStatusBarPanel
            // 
            this.VersionStatusBarPanel.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.VersionStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.VersionStatusBarPanel.Name = "VersionStatusBarPanel";
            this.VersionStatusBarPanel.Text = "Version 8.3.0";
            this.VersionStatusBarPanel.Width = 81;
            // 
            // statusBarPanel2
            // 
            this.statusBarPanel2.Name = "statusBarPanel2";
            this.statusBarPanel2.Width = 200;
            // 
            // MainStatusBar
            // 
            this.MainStatusBar.Location = new System.Drawing.Point(0, 640);
            this.MainStatusBar.Name = "MainStatusBar";
            this.MainStatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.SysStatusBarPanel,
            this.NetCommStatusBarPanel,
            this.CommStatusBarPanel,
            this.DBStatusBarPanel,
            this.DateTimeStatusBarPanel,
            this.VersionStatusBarPanel,
            this.statusBarPanel2});
            this.MainStatusBar.ShowPanels = true;
            this.MainStatusBar.Size = new System.Drawing.Size(984, 25);
            this.MainStatusBar.TabIndex = 2;
            this.MainStatusBar.Text = "statusBar1";
            this.MainStatusBar.DrawItem += new System.Windows.Forms.StatusBarDrawItemEventHandler(this.MainStatusBar_DrawItem);
            // 
            // SysStatusBarPanel
            // 
            this.SysStatusBarPanel.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.SysStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.SysStatusBarPanel.Icon = ((System.Drawing.Icon)(resources.GetObject("SysStatusBarPanel.Icon")));
            this.SysStatusBarPanel.Name = "SysStatusBarPanel";
            this.SysStatusBarPanel.Text = "System Not Ready";
            this.SysStatusBarPanel.Width = 129;
            // 
            // NetCommStatusBarPanel
            // 
            this.NetCommStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.NetCommStatusBarPanel.Icon = ((System.Drawing.Icon)(resources.GetObject("NetCommStatusBarPanel.Icon")));
            this.NetCommStatusBarPanel.Name = "NetCommStatusBarPanel";
            this.NetCommStatusBarPanel.Text = "Network Reader(s): Online ( 0 )    Offline ( 0 )";
            this.NetCommStatusBarPanel.Width = 260;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 637);
            this.splitter1.MinExtra = 42;
            this.splitter1.MinSize = 173;
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(984, 3);
            this.splitter1.TabIndex = 45;
            this.splitter1.TabStop = false;
            // 
            // listView
            // 
            this.listView.BackColor = System.Drawing.SystemColors.Info;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader9,
            this.columnHeader7,
            this.columnHeader8});
            this.listView.ContextMenu = this.contextMenu1;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 67);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.OwnerDraw = true;
            this.listView.Size = new System.Drawing.Size(984, 570);
            this.listView.SmallImageList = this.SmallImageList;
            this.listView.TabIndex = 47;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.VirtualMode = true;
            this.listView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.listView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.listView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            this.listView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listView_RetrieveVirtualItem);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Stat";
            this.columnHeader1.Width = 38;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tag ID";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 85;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 175;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Location";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 260;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Event Description";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 275;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Time";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 160;
            // 
            // SmallImageList
            // 
            this.SmallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SmallImageList.ImageStream")));
            this.SmallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.SmallImageList.Images.SetKeyName(0, "1923.ico");
            this.SmallImageList.Images.SetKeyName(1, "141.ico");
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(984, 665);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.MainStatusBar);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Tracker";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ComStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Relay1Timer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Relay2Timer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClockTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PollDbTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateTimeStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VersionStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SysStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NetCommStatusBarPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		//this function set environment for crystal report and MAY BE COULD be omitted
		private void SetEnvVars(int v)
		{
			RegistryKey envReg = null;
			try
			{
				envReg = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment\\");
			}
			catch
			{
                MessageBox.Show(this, "Failed to get system registry key for Reports", "Banks");
			}
			
			string os = "";
			string drv = "";

			try
			{
				os = Convert.ToString(envReg.GetValue("OS"));
				drv = Environment.ExpandEnvironmentVariables("%SystemDrive%");
			}
			catch
			{
               MessageBox.Show(this, "Failed to get system variable for Reports", "Banks");
			}

			if (drv.Length > 0)
			{
				string s = "";
				s = drv+"\\WINDOWS";

				try
				{
					envReg.SetValue("windir", s);
				}
				catch
				{
					MessageBox.Show(this, "Failed to set system variable for Reports", "Banks");
				}
			}
			else
				MessageBox.Show(this, "Failed to set system variable for Reports", "Banks");
		}

		private void toolBar_Click(object sender, System.EventArgs e)
		{
		   
		}

        #region StartRS232()
        //Thread running rs232
        private void StartRS232()
        {
            if (OpenSerialPort(115200, rs232Port) >= 0)
                communication.ResetReader(0, 1);
            else
            {
                MessageBox.Show(this, "Open comm port failed.", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }
        #endregion

        #region StartNetwork()
        private class ReaderIPScan
        {
            private byte[] ip;
            private int count;

            public ReaderIPScan(byte[] ip)
            {
                this.ip = ip;
                count = 0;
            }

            public byte[] IP { get { return ip; } }
            public int Count { get { return count; } set { count = value; } }
        }

        //Thread running network
        private void StartNetwork()
        {
            Console.WriteLine("StartNetwork start " + DateTime.Now.ToString());
            ScanNetwork(null);
            Console.WriteLine("StartNetwork End " + DateTime.Now.ToString());
            //Thread.Sleep(500); //(8000);
            //Form1.SetVolume(Form1.generalVol);
            // CHG 11-16 PlaySound(OK);
            //Thread.Sleep(1000);
            // CHG 11-16 PlaySound(OK);
            //NetRdrDisconnection.SetRdrPolling(true);  //start checking rdrs
            Thread.Sleep(1000); //(5000);
            if (!closingApp)
                NetRdrDisconnection.StartRdrPolling();
            //NetRdrDisconnection.TrunOnRdrPolling();
            networkReady = true;

            Dictionary<string, ReaderIPScan> offline_readers = new Dictionary<string, ReaderIPScan>();

            while (!closingApp)
            {
                Thread.Sleep(6000);
                string ConnString = string.Format("DRIVER={{MySQL ODBC 3.51 Driver}};SERVER={0};DATABASE={1};USER={2};PASSWORD={3};OPTION=3;", MainForm.serverMySQL, MainForm.database, MainForm.user, MainForm.password);

                string SelectCmd = "SELECT IPAddress, RdrStatus FROM NetIP";

                using (var con = new OdbcConnection(ConnString))
                using (var cmd = new OdbcCommand(SelectCmd, con))
                {
                    try
                    {
                        con.Open();
                        using (var db = cmd.ExecuteReader())
                        {
                            while (db.Read())
                            {
                                string ip_str = db["IPAddress"].ToString();
                                bool online = db["RdrStatus"].ToString().Equals("Online");
                                if (online)
                                {
                                    if (offline_readers.ContainsKey(ip_str))
                                    {
                                        Console.WriteLine("Monitor: Removing online reader {0}", ip_str);
                                        offline_readers.Remove(ip_str);
                                    }
                                }
                                else
                                {
                                    if (offline_readers.ContainsKey(ip_str))
                                        offline_readers[ip_str].Count++;
                                    else
                                    {
                                        byte[] ip_byte = new byte[16];
                                        for (int ix = 0; ix < ip_str.Length; ix++)
                                            ip_byte[ix] = Convert.ToByte(ip_str[ix]);

                                        offline_readers.Add(ip_str, new ReaderIPScan(ip_byte));
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                foreach (KeyValuePair<string, ReaderIPScan> reader in offline_readers)
                {
                    if (!closingApp)
                    {
                        if (reader.Value.Count >= 2)
                        {
                            reader.Value.Count = 0;
                            if (communication != null)
                            {
                                Console.WriteLine("Monitor: Search for offline reader {0}", reader.Key);
                                ScanNetwork(reader.Value.IP);
                            }
                        }
                        else
                        {
                            if ((reader.Key.Length > 0) && (m_updateIPListView != null))
                                m_updateIPListView(reader.Key, 0, 0, "Active", "Offline", eventType.sockConnect);

                            if (communication != null)
                            {
                                Console.WriteLine("Monitor: Retry #{1} offline reader {0}", reader.Key, reader.Value.Count + 1);
                                communication.ResetReaderSocket(1, reader.Value.IP);
                            }
                        }
                    }
                }
            }

            //PlaySound(3);
            //Thread.Sleep(1000);
            //PlaySound(3);
        }
        #endregion

		public bool ReconnectToDBServer()
		{
			if (conStr.Length > 0)
			{
				if (!odbcDB.Connect(conStr))	//MYSQL
				{
					return false;
				}
			}

			return true;
		}


		#region private bool RunQueryCmd(string qryStr, OdbcDataReader dataRdr)
        public bool RunQueryCmd(string qryStr, out OdbcDataReader dataRdr)
        {
            dataRdr = null;

            if (ConnString == null)
                return false;

            var con = new OdbcConnection(ConnString);
            var myCommand = new OdbcCommand(qryStr, con);

            try
            {
                con.Open();
                dataRdr = myCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                int ret = 0, ret1 = 0, ret2 = 0;
                if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                    ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                    ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                {
                    if (reconnectCounter == -1)
                        reconnectCounter = 0;
                    dbDisconnectedFlag = true;
                }

                con.Close();


                return (false);
            }

            if (dbDisconnectedFlag)
            {
                try
                {
                    CommStatusBarPanel.Text = "RS232 Reader: Online";
                    CommStatusBarPanel.Icon = Resources.ReaderConnected;

                }
                catch //(Exception ex)
                {
                    //MainStatusBar.Panels[0].Icon = null;
                    CommStatusBarPanel.Icon = null;
                }

                DBStatusBarPanel.Text = "DB Server : MYSQL  Connected";
                try
                {
                    DBStatusBarPanel.Icon = Resources.DbConnected;
                }
                catch
                {
                    DBStatusBarPanel.Icon = null;
                }
            }

            dbDisconnectedFlag = false;
            reconnectCounter = -1;
            if (msgDisplayed)
            {
                msgDisplayed = false;
                if (dbDisconnectMsg != null)
                {
                    dbDisconnectMsg.Close();
                    dbDisconnectMsg.Dispose();
                }
            }

            return (true);
        }
		#endregion

		#region PollDB
        private void PollDB()
        {
            //This function polls the database
            if (m_connection == null)
            {
                dbDisconnectedFlag = true;
                if (reconnectCounter == -1)
                    reconnectCounter = 0;
                return;
            }
            else if ((m_connection.State == System.Data.ConnectionState.Broken) ||
                     (m_connection.State == System.Data.ConnectionState.Closed))
            {
                dbDisconnectedFlag = true;
                if (reconnectCounter == -1)
                    reconnectCounter = 0;
                return;
            }

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

            StringBuilder mySelectQuery = new StringBuilder();
            mySelectQuery.Append("SELECT Description FROM tagtypes");
            string mySelectStr = mySelectQuery.ToString();

            OdbcDataReader myReader;

            if (!RunQueryCmd(mySelectStr, out myReader))
            {
                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                        myReader.Close();
                }

                dbDisconnectedFlag = true;
                dbConnDisplay = false;
                if (reconnectCounter == -1)
                    reconnectCounter = 0;

                return;
            }

            myReader.Close();

            dbDisconnectedFlag = false;
            reconnectCounter = -1;

            if (!dbConnDisplay)
            {
                dbConnDisplay = true;
                DBStatusBarPanel.Text = "DB Server : MySQL  Connected";

                try
                {
                    DBStatusBarPanel.Icon = Resources.DbConnected;
                }
                catch
                {
                    DBStatusBarPanel.Icon = null;
                }

                //12-26-07 PollTrafficTable();
                awiHistoryControl1.UpdateTagsPage(true);
                PlaySound(3);
            }
        }
		#endregion

        #region PollTrafficTable()
        private void PollTrafficTable()
		{	
			//This function polls Traffic Table	  
			if (m_connection == null)
				return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			lock (m_connection)
			{                
				///string mySelectQuery = string.Format("SELECT Index, TagID, Name, ZoneID, Location, Status, Event, Time From traffic Where Time > '{0}'", lastTime);
				StringBuilder mySelectQuery = new StringBuilder();
				mySelectQuery.Append("SELECT Status, TagID, Name, ZoneID, Location, Event, Time, type FROM traffic WHERE Time > ");
				mySelectQuery.AppendFormat("{0}", lastTime.ToString("yyyyMMddHHmmss"));
				string mySelectStr = mySelectQuery.ToString();

				OdbcCommand myCommand = new OdbcCommand(mySelectStr, m_connection); 
				///OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection);
			
				OdbcDataReader myReader = null;

				

				//lock (m_connection)
				//{
				try
				{
					myReader = myCommand.ExecuteReader();
					reconnectCounter = -1;
					//\\timer3.Enabled = false;
				}
				catch (Exception ex)
				{
					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{  
						//error code 2013
						dbDisconnectedFlag = true;
						dbConnDisplay = false;
						if (reconnectCounter == -1)
							reconnectCounter = 0;

						                           
					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				}//catch .. try
				//}//lock
			
				if (!dbConnDisplay)
				{
					dbConnDisplay = true;
					DBStatusBarPanel.Text = "DB Server : MySQL  Connected";
				
					try
					{
						DBStatusBarPanel.Icon = Resources.DbConnected;
					}
					catch
					{
						DBStatusBarPanel.Icon = null;
					}
				
					awiHistoryControl1.UpdateTagsPage(true);
					PlaySound(3);
				}

				string stat = "";
				string tagid = "";
				string name = "";
				string location = "";
                string lastTimeStr = "";
				uint type = 0;
				Image tagImage = null;
				bool newData = false;
                ListViewItem listItem;
	
				while (myReader.Read())
				{
					try
					{
                        if (myReader.IsDBNull(0))
                            stat = "";
                        else
                            stat = myReader.GetString(0);

                        if ((stat == "OnLine") || (stat == "Online"))
                        {
                            listItem = new ListViewItem("ON");
							listView.ForeColor = System.Drawing.Color.Green;
                            
                        }
						else if ((stat == "OffLine") || (stat == "Offline")) 
                        {
                            listItem = new ListViewItem("OF");
							listView.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (stat == "Invalid")
                        {
                            listItem = new ListViewItem("IN");
                            listItem.ImageIndex = 0;
                            listView.ForeColor = System.Drawing.Color.Purple;
                        }
                        else if (stat == "Valid")
                        {
                            listItem = new ListViewItem("VA");
                            listView.ForeColor = System.Drawing.Color.Blue;
                        }
                        else
                        {
                            listItem = new ListViewItem("");
                            listView.ForeColor = System.Drawing.Color.Black;

                        }
                    }
                    catch
                    {
                        listItem = new ListViewItem("");
                    }

						//ListViewItem listItem = new ListViewItem();
					try	
                    {
						tagid = myReader.GetString(1);  //tag id
						if (tagid.Length == 0)
							continue;
						listItem.SubItems.Add(tagid );  //tag id
                    }
                    catch
                    {
                        continue;
                    }
                                        
                    try
                    {
                        if (myReader.IsDBNull(2))
                            name = "";
                        else
					        name = myReader.GetString(2);   //name
					    listItem.SubItems.Add(name);    //name
                    }
                    catch
                    {
                        listItem.SubItems.Add("");    //name
                    }
						
                    try
                    {
                        if (myReader.IsDBNull(3))
                            location = "";
                        else
                            location = myReader.GetString(3);
					    listItem.SubItems.Add(location);  //location
                    }
                    catch
                    {
                       listItem.SubItems.Add("");  //location
                    }

                    try
                    {
                        if (myReader.IsDBNull(4))
                            listItem.SubItems.Add("");  //event description
                        else
                            listItem.SubItems.Add(myReader.GetString(4));  //event description
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    try
                    {
                        if (myReader.IsDBNull(5))
                            lastTimeStr = "";
                        else
                            lastTimeStr = myReader.GetString(5);
                    }
                    catch
                    {
                        lastTimeStr = "";
                    }

                    try
                    {
                        if (myReader.IsDBNull(5))
                            type = 0;
                        else
                            type = Convert.ToUInt32(myReader.GetInt32(6));  //type
                    }
                    catch
                    {
                        type = 0;
                    }

                    listItem.SubItems.Add(lastTime.ToString());  //time
                        					
                    lvi[virtualListIndex++] = listItem;
						
					newData = true;
									
				}//while
				myReader.Close();

                listView.VirtualListSize = virtualListIndex;

                if (listView.Items.Count >= 1)
                {
                    listView.Items[listView.Items.Count-1].EnsureVisible();
                    listView.Items[listView.Items.Count-1].Selected = true;
                }

                
			
				if (newData && (tagid != null))
				{
					string stype = "";
                    if (type == 0) stype = "ALL";
					else if (type == 1) stype = "ACC";
                    else if (type == 2) stype = "INV";
					else if (type == 3) stype = "AST";                    
					else return;
					tagImage = GetTagImage(tagid, stype);
					awiHistoryControl1.ShowImage(name, location, tagid, lastTime.ToString(), tagImage, stat);
				}
			}//lock m_connection
        }
        #endregion

        #region PollVirtualList()
        private bool updatingVirtualList = false;
        private readonly object lockVirtualList = new object();

        private void PollVirtualList()
        {
            lock (lockVirtualList)
            {
                if (updatingVirtualList)
                    return;

                updatingVirtualList = true;
            }

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

            //lock (m_connection)
            {
                ///string mySelectQuery = string.Format("SELECT Index, TagID, Name, ZoneID, Location, Status, Event, Time From traffic Where Time > '{0}'", lastTime);
                StringBuilder mySelectQuery = new StringBuilder();
                //mySelectQuery.Append("SELECT Status, TagID, Name, ZoneID, Location, Event, Time, Type, traffic.Index FROM traffic WHERE Time > ");
#if SANI
                mySelectQuery.Append("SELECT Status, TagID, FirstName, LastName, Location, Event, Time, Type, traffic.Index as Ix, FALSE, '', '' FROM traffic WHERE Time > ");
#else
                mySelectQuery.Append("SELECT Status, TagID, FirstName, LastName, Location, Event, Time, Type, traffic.Index FROM traffic WHERE Time > ");
#endif
                mySelectQuery.AppendFormat("{0}", lastTime.ToString("yyyyMMddHHmmss"));
                mySelectQuery.Append(" AND traffic.Index > ");
                mySelectQuery.AppendFormat("'{0}'", lastTrafficIndex);
#if SANI
                mySelectQuery.Append(" UNION ALL ");
                mySelectQuery.Append("SELECT Status, TagID, FirstName, LastName, Location, Event, Time, Type, sani.Index as Ix, TRUE, SaniUnitType, SaniStatus FROM sani WHERE Time > ");
                mySelectQuery.AppendFormat("{0}", lastTime.ToString("yyyyMMddHHmmss"));
                mySelectQuery.Append(" AND sani.Index > ");
                mySelectQuery.AppendFormat("'{0}'", lastSaniTrafficIndex);
                mySelectQuery.Append(" ORDER BY Time, Ix");
#endif
                string mySelectStr = mySelectQuery.ToString();

                using (var con = new OdbcConnection(ConnString))
                using (var myCommand = new OdbcCommand(mySelectStr, con))
                {
                    try
                    {
                        con.Open();
                        using (var myReader = myCommand.ExecuteReader())
                        {
                            string stat = "";
                            string tagid = "";
                            string firstname = "";
                            string lastname = "";
                            string location = "";
                            string lastTimeStr = "";
                            uint type = 0;
                            Image tagImage = null;
                            bool newData = false;
                            ListViewItem listItem;

                            while (myReader.Read())
                            {
                                try
                                {
                                    if (myReader.IsDBNull(0))   //status (0)
                                        stat = "";
                                    else
                                        stat = myReader.GetString(0);

                                    if ((stat == "OnLine") || (stat == "Online"))
                                    {
                                        listItem = new ListViewItem("ON");
                                        listView.ForeColor = System.Drawing.Color.Green;

                                    }
                                    else if ((stat == "OffLine") || (stat == "Offline"))
                                    {
                                        listItem = new ListViewItem("OF");
                                        listView.ForeColor = System.Drawing.Color.Red;
                                    }
                                    else if (stat == "Invalid")
                                    {
                                        listItem = new ListViewItem("IN");
                                        listItem.ImageIndex = 0;
                                        listView.ForeColor = System.Drawing.Color.Purple;
                                    }
                                    else if (stat == "Valid")
                                    {
                                        listItem = new ListViewItem("VA");
                                        listView.ForeColor = System.Drawing.Color.Blue;
                                    }
                                    else
                                    {
                                        listItem = new ListViewItem("");
                                        listView.ForeColor = System.Drawing.Color.Black;

                                    }
                                }
                                catch
                                {
                                    listItem = new ListViewItem("");
                                }

                                //ListViewItem listItem = new ListViewItem();
                                try
                                {
                                    tagid = myReader.GetString(1);  //tag id (1)
                                    if (tagid.Length == 0)
                                    {
                                        listItem.SubItems.Add("");
                                    }
                                    else
                                    {
                                        listItem.SubItems.Add(tagid);  //tag id
                                    }
                                }
                                catch
                                {
                                    continue;
                                }

                                try
                                {
                                    if (myReader.IsDBNull(2))
                                        firstname = "";
                                    else
                                        firstname = myReader.GetString(2);   //name (2)

                                    if (myReader.IsDBNull(3))
                                        lastname = "";
                                    else
                                        lastname = myReader.GetString(3);   //name (2)

                                    listItem.SubItems.Add(string.Format("{0} {1}", firstname, lastname).Trim());    //name
                                }
                                catch
                                {
                                    listItem.SubItems.Add("");    //name
                                }

                                try
                                {
                                    if (myReader.IsDBNull(4))
                                        location = "";
                                    else
                                        location = myReader.GetValue(4).ToString();
                                    listItem.SubItems.Add(location);  //location (3)
                                }
                                catch
                                {
                                    listItem.SubItems.Add("");  //location
                                }

                                try
                                {
                                    if (myReader.IsDBNull(5))
                                        listItem.SubItems.Add("");  //event description (4)
                                    else
                                        listItem.SubItems.Add(myReader.GetString(5));  //event description
                                }
                                catch
                                {
                                    listItem.SubItems.Add("");
                                }

                                try
                                {
                                    if (myReader.IsDBNull(6))  //time (5)
                                        lastTimeStr = "";
                                    else
                                        lastTimeStr = myReader.GetDateTime(6).ToString("MM-dd-yyyy HH:mm:ss");
                                }
                                catch
                                {
                                    lastTimeStr = "";
                                }

                                try
                                {
                                    if (myReader.IsDBNull(7))
                                        type = 0;
                                    else
                                        type = Convert.ToUInt32(myReader.GetInt32(7));  //type (6)
                                }
                                catch
                                {
                                    type = 0;
                                }

                                listItem.SubItems.Add(lastTimeStr);  //time

#if SANI
                                try
                                {

                                    if (myReader.GetBoolean(9))
                                    {
                                        lastSaniTrafficIndex = (Int32)myReader.GetInt64(8);  //Index
                                    }
                                    else
                                    {
                                        lastTrafficIndex = (Int32)myReader.GetInt64(8);  //Index
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (myReader.IsDBNull(10))
                                        listItem.SubItems.Add("");  //SaniUnitType (9)
                                    else
                                        listItem.SubItems.Add(myReader.GetString(10));  //SaniUnitType
                                }
                                catch
                                {
                                    listItem.SubItems.Add("");
                                }

                                try
                                {
                                    if (myReader.IsDBNull(11))
                                        listItem.SubItems.Add("");  //SaniStatus (10)
                                    else
                                        listItem.SubItems.Add(myReader.GetString(11));  //SaniStatus
                                }
                                catch
                                {
                                    listItem.SubItems.Add("");
                                }
#else
                                try
                                {
                                    lastTrafficIndex = (Int32)myReader.GetInt64(8);  //Index
                                }
                                catch
                                {
                                }
#endif

                                lvi[virtualListIndex++] = listItem;

                                newData = true;

                            }//while

                            try
                            {
                                listView.VirtualListSize = virtualListIndex;

                                if (listView.Items.Count >= 1)
                                {
                                    listView.Items[listView.Items.Count - 1].EnsureVisible();
                                    listView.Items[listView.Items.Count - 1].Selected = true;
                                }
                            }
                            catch { }


                            if (newData && (tagid != null))
                            {
                                string stype = "";
                                if (type == 1) stype = "ACC";
                                else if (type == 3) stype = "AST";
                                else if (type == 2) stype = "INV";
                                else if (type == 0) stype = "ALL";
                                else return;
                                tagImage = GetTagImage(tagid, stype);
                                awiHistoryControl1.ShowImage(string.Format("{0} {1}", firstname, lastname).Trim(), location, tagid, lastTime.ToString(), tagImage, stat);
                            }
                        }

                        reconnectCounter = -1;
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
                            dbDisconnectedFlag = true;
                            dbConnDisplay = false;
                            if (reconnectCounter == -1)
                                reconnectCounter = 0;
                        }
                        return;
                    }//catch .. try
                    //}//lock
                }

                if (!dbConnDisplay)
                {
                    dbConnDisplay = true;
                    DBStatusBarPanel.Text = "DB Server : MySQL  Connected";

                    try
                    {
                        DBStatusBarPanel.Icon = Resources.DbConnected;
                    }
                    catch
                    {
                        DBStatusBarPanel.Icon = null;
                    }

                    awiHistoryControl1.UpdateTagsPage(true);
                    PlaySound(3);
                }
            }//lock m_connection

            updatingVirtualList = false;
        }
        #endregion

        #region BuildVirtualList()
        private void BuildVirtualList()
        {
            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

            //lock (m_connection)
            {
                ///string mySelectQuery = string.Format("SELECT Index, TagID, Name, ZoneID, Location, Status, Event, Time From traffic Where Time > '{0}'", lastTime);
                StringBuilder mySelectQuery = new StringBuilder();
                //mySelectQuery.Append("SELECT Status, TagID, Name, ZoneID, Location, Event, Time, Type, traffic.Index FROM traffic WHERE Time > ");
#if SANI
                mySelectQuery.Append("SELECT Status, TagID, FirstName, LastName, Location, Event, Time, Type, traffic.Index as Ix, FALSE, '', '' FROM traffic WHERE Time > ");
#else
                mySelectQuery.Append("SELECT Status, TagID, FirstName, LastName, Location, Event, Time, Type, traffic.Index FROM traffic WHERE Time > ");
#endif
                mySelectQuery.AppendFormat("{0}", lastTime.ToString("yyyyMMddHHmmss"));
#if SANI
                mySelectQuery.Append(" UNION ALL ");
                mySelectQuery.Append("SELECT Status, TagID, FirstName, LastName, Location, Event, Time, Type, sani.Index as Ix, TRUE, SaniUnitType, SaniStatus FROM sani WHERE Time > ");
                mySelectQuery.AppendFormat("{0}", lastTime.ToString("yyyyMMddHHmmss"));
                mySelectQuery.Append(" ORDER BY Time, Ix");
#endif
                string mySelectStr = mySelectQuery.ToString();


                using (var con = new OdbcConnection(ConnString))
                using (var myCommand = new OdbcCommand(mySelectStr, con))
                {
                    try
                    {
                        con.Open();
                        using (var myReader = myCommand.ExecuteReader())
                        {
                            string stat = "";
                            string tagid = "";
                            string firstname = "";
                            string lastname = "";
                            string location = "";
                            string lastTimeStr = "";
                            uint type = 0;
                            Image tagImage = null;
                            bool newData = false;
                            ListViewItem listItem;

                            virtualListLoaded = false;

                            while (myReader.Read())
                            {
                                try
                                {
                                    if (myReader.IsDBNull(0))   //status(0)
                                        stat = "";
                                    else
                                        stat = myReader.GetString(0);

                                    if ((stat == "OnLine") || (stat == "Online"))
                                    {
                                        listItem = new ListViewItem("ON");
                                        listView.ForeColor = System.Drawing.Color.Green;

                                    }
                                    else if ((stat == "OffLine") || (stat == "Offline"))
                                    {
                                        listItem = new ListViewItem("OF");
                                        listView.ForeColor = System.Drawing.Color.Red;
                                    }
                                    else if (stat == "Invalid")
                                    {
                                        listItem = new ListViewItem("IN");
                                        listItem.ImageIndex = 0;
                                        listView.ForeColor = System.Drawing.Color.Purple;
                                    }
                                    else if (stat == "Valid")
                                    {
                                        listItem = new ListViewItem("VA");
                                        listView.ForeColor = System.Drawing.Color.Blue;
                                    }
                                    else
                                    {
                                        listItem = new ListViewItem("");
                                        listView.ForeColor = System.Drawing.Color.Black;

                                    }
                                }
                                catch
                                {
                                    listItem = new ListViewItem("");
                                }

                                //ListViewItem listItem = new ListViewItem();
                                try
                                {
                                    tagid = myReader.GetString(1);  //tag id (1)
                                    if (tagid.Length == 0)
                                    {
                                        listItem.SubItems.Add("");
                                    }
                                    else
                                    {
                                        listItem.SubItems.Add(tagid);  //tag id
                                    }
                                }
                                catch
                                {
                                    continue;
                                }

                                try
                                {
                                    if (myReader.IsDBNull(2))
                                        firstname = "";
                                    else
                                        firstname = myReader.GetString(2);   //name (2)

                                    if (myReader.IsDBNull(3))
                                        lastname = "";
                                    else
                                        lastname = myReader.GetString(3);   //name (2)

                                    listItem.SubItems.Add(string.Format("{0} {1}", firstname, lastname).Trim());    //name
                                }
                                catch
                                {
                                    listItem.SubItems.Add("");    //name
                                }

                                try
                                {
                                    if (myReader.IsDBNull(4))
                                        location = "";
                                    else
                                        location = myReader.GetValue(4).ToString();
                                    listItem.SubItems.Add(location);  //location (3)
                                }
                                catch
                                {
                                    listItem.SubItems.Add("");  //location
                                }

                                try
                                {
                                    if (myReader.IsDBNull(5))
                                        listItem.SubItems.Add("");  //event description (4)
                                    else
                                        listItem.SubItems.Add(myReader.GetString(5));  //event description
                                }
                                catch
                                {
                                    listItem.SubItems.Add("");
                                }

                                try
                                {
                                    if (myReader.IsDBNull(6))  //time (5)
                                        lastTimeStr = "";
                                    else
                                        lastTimeStr = myReader.GetDateTime(6).ToString("MM-dd-yyyy HH:mm:ss");
                                }
                                catch
                                {
                                    lastTimeStr = "";
                                }

                                try
                                {
                                    if (myReader.IsDBNull(7))
                                        type = 0;
                                    else
                                        type = Convert.ToUInt32(myReader.GetInt32(7));  //type (6)
                                }
                                catch
                                {
                                    type = 0;
                                }

                                listItem.SubItems.Add(lastTimeStr);  //time

#if SANI
                                try
                                {
                                    if (myReader.GetBoolean(9))
                                    {
                                        lastSaniTrafficIndex = (Int32)myReader.GetInt64(8);  //Index
                                    }
                                    else
                                    {
                                        lastTrafficIndex = (Int32)myReader.GetInt64(8);  //Index
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (myReader.IsDBNull(10))
                                        listItem.SubItems.Add("");  //SaniUnitType (9)
                                    else
                                        listItem.SubItems.Add(myReader.GetString(10));  //SaniUnitType
                                }
                                catch
                                {
                                    listItem.SubItems.Add("");
                                }

                                try
                                {
                                    if (myReader.IsDBNull(11))
                                        listItem.SubItems.Add("");  //SaniStatus (10)
                                    else
                                        listItem.SubItems.Add(myReader.GetString(11));  //SaniStatus
                                }
                                catch
                                {
                                    listItem.SubItems.Add("");
                                }
#else
                                try
                                {
                                    lastTrafficIndex = (Int32)myReader.GetInt64(8);  //Index
                                }
                                catch
                                {
                                }
#endif

                                lvi[virtualListIndex++] = listItem;

                                newData = true;

                            }//while

                            listView.VirtualListSize = virtualListIndex;

                            if (listView.Items.Count >= 1)
                            {
                                listView.Items[listView.Items.Count - 1].EnsureVisible();
                                listView.Items[listView.Items.Count - 1].Selected = true;
                            }



                            if (newData && (tagid != null))
                            {
                                string stype = "";
                                if (type == 1) stype = "ACC";
                                else if (type == 3) stype = "AST";
                                else if (type == 2) stype = "INV";
                                else if (type == 0) stype = "ALL";

                                else return;
                                tagImage = GetTagImage(tagid, stype);
                                awiHistoryControl1.ShowImage(string.Format("{0} {1}", firstname, lastname).Trim(), location, tagid, lastTime.ToString(), tagImage, stat);
                            }
                        }

                        reconnectCounter = -1;
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
                            dbDisconnectedFlag = true;
                            dbConnDisplay = false;
                            if (reconnectCounter == -1)
                                reconnectCounter = 0;


                        }
                        return;
                    }//catch .. try
                    //}//lock
                }

                if (!dbConnDisplay)
                {
                    dbConnDisplay = true;
                    DBStatusBarPanel.Text = "DB Server : MySQL  Connected";

                    try
                    {
                        DBStatusBarPanel.Icon = Resources.DbConnected;
                    }
                    catch
                    {
                        DBStatusBarPanel.Icon = null;
                    }

                    awiHistoryControl1.UpdateTagsPage(true);
                    PlaySound(3);
                }

                virtualListLoaded = true;

            }//lock m_connection
        }
        #endregion

        private Image GetTagImage(string tagId, string type)
        {
            if (m_connection == null)
                return null;

            byte[] data = null;
            string sql = string.Format("SELECT Image FROM tags WHERE TagID = '{0}'", type + tagId);  //"ACC"+tagId);

            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(sql, con))
            {
                try
                {
                    con.Open();
                    data = cmd.ExecuteScalar() as byte[];
                }
                catch //(Exception e)
                {
                    return null;
                }
            }

            if (data != null && data.Equals('0'))
            {
                try
                {
                    Stream stream = new MemoryStream(data);
                    return Image.FromStream(stream);
                }
                catch { }
            }

            return null;
        }

        #region GetRs232Reader()
        public ushort GetRs232Reader()
        {
            return (rs232Reader);
        }
        #endregion

        #region SetRs232Reader(ushort rdr)
        public void SetRs232Reader(ushort rdr)
        {
            rs232Reader = rdr;
        }
        #endregion

		private void DBConnectionStatusHandler(status stat, OdbcConnection connect)
		{
			if (stat == status.open)
			{
				m_connection = connect;
				if (conStr.Length == 0)
					conStr = tempStrConnect; 
				lock (m_connection)
				{
#if SANI
                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS sani (" +
                                                                        "`Index` int(10) unsigned NOT NULL auto_increment," +
                                                                        "`TagID` varchar(12) default NULL," +
                                                                        "`FirstName` varchar(45) default NULL," +
                                                                        "`LastName` varchar(45) default NULL," +
                                                                        "`ZoneID` int(11) default NULL," +
                                                                        "`Location` varchar(45) default NULL," +
                                                                        "`Status` varchar(20) default NULL," +
                                                                        "`SaniUnitType` varchar(20) default NULL," +
                                                                        "`SaniStatus` varchar(21) default NULL," +
                                                                        "`Event` varchar(75) default NULL," +
                                                                        "`Image` mediumblob," +
                                                                        "`Type` int(11) default NULL," +
                                                                        "`Department` varchar(100) default NULL," +
                                                                        "`Time` datetime NOT NULL default '0000-00-00 00:00:00'," +
                                                                        "PRIMARY KEY  (`Index`)" +
                                                                        ") ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS sani_duplicates (" +
                                                                        "`Index` int(10) unsigned NOT NULL auto_increment," +
                                                                        "`TagID` varchar(12) default NULL," +
                                                                        "`SaniStatus` varchar(21) default NULL," +
                                                                        "`EventCount` int(8) default NULL," +
                                                                        "PRIMARY KEY  (`Index`)" +
                                                                        ") ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }
#endif
                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS actiondef (" +
                                                                        "`Index` int(10) unsigned NOT NULL auto_increment," +
                                                                        "`EventActionID` int(10) unsigned NOT NULL default '0'," +
                                                                        "`ActionID` int(10) unsigned NOT NULL default '0'," +
                                                                        "`Duration` int(10) unsigned NOT NULL default '0'," +
                                                                        "`ReaderID` smallint(6) default NULL," +
                                                                        "`FGenID` smallint(6) default NULL," +
                                                                        "`Location` varchar(45) default NULL," +
                                                                        "`ActionIndex` int(10) unsigned NOT NULL default '0'," +
                                                                        "PRIMARY KEY  (`Index`)" +
                                                                        ") ENGINE=MyISAM AUTO_INCREMENT=42 DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS asset (" +
                                                                        "`ID` int(16) unsigned NOT NULL default '0'," +
                                                                        "`Mobile` tinyint(1) default NULL," +
                                                                        "`Name` varchar(45) default NULL," +
                                                                        "`Model` varchar(45) default NULL," +
                                                                        "`SKU` varchar(30) default NULL," +
                                                                        "`Description` varchar(100) default NULL," +
                                                                        "`Timestamp` datetime default NULL," +
                                                                        "`Image` mediumblob," +
                                                                        "PRIMARY KEY  (`ID`)" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS EmployeeAccess (" +
                                                                        "`ID` int(16) unsigned NOT NULL," +
                                                                        "`GroupID` int unsigned NOT NULL" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS employees (" +
                                                                        "`ID` int(16) unsigned NOT NULL default '0'," +
                                                                        "`Image` mediumblob," +
                                                                        "`Time` datetime default NULL," +
                                                                        "`AccType` smallint(6) NOT NULL default '0'," +
                                                                        "`ExpTime` datetime default NULL," +
                                                                        "`FirstName` varchar(45) default NULL," +
                                                                        "`LastName` varchar(45) default NULL," +
                                                                        "`Company` varchar(100) default NULL," +
                                                                        "`Title` varchar(45) default NULL," +
                                                                        "`Comment` varchar(200) default NULL," +
                                                                        "`StaffNum` varchar(6) default NULL," +
                                                                        "`Department` varchar(75) default NULL," +
                                                                        "`PassportNum` varchar(45) default NULL," +
                                                                        "PRIMARY KEY  (`ID`)" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS eventaction (" +
                                                                        "`EventActionID` int(10) unsigned NOT NULL default '0'," +
                                                                        "`Description` varchar(100) default NULL," +
                                                                        "`ReaderID` smallint(6) default NULL," +
                                                                        "`FGenID` smallint(6) default NULL," +
                                                                        "`Location` varchar(45) default NULL," +
                                                                        "`EventID` int(10) unsigned NOT NULL default '0'," +
                                                                        "`Timestamp` datetime default NULL," +
                                                                        "PRIMARY KEY  (`EventActionID`)" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS history (" +
                                                                        "`TagID` varchar(15) NOT NULL default ''," +
                                                                        "`Event` varchar(50) NOT NULL default ''," +
                                                                        "`ZoneID` int unsigned NOT NULL default '0'," +
                                                                        "`Timestamp` datetime default NULL" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS netip (" +
                                                                        "`IPAddress` varchar(17) NOT NULL default '000.000.000.000'," +
                                                                        "`ReaderID` smallint unsigned default NULL," +
                                                                        "`HostID` smallint unsigned default NULL," +
                                                                        "`NetworkStatus` varchar(10) default NULL," +
                                                                        "`RdrStatus` varchar(10) default NULL," +
                                                                        "`ConnectTime` datetime default NULL," +
                                                                        "`ZoneID` int unsigned unsigned default NULL," +
                                                                        "`Location` varchar(45) default NULL," +
                                                                        "PRIMARY KEY  (`IPAddress`)" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS rssi (" +
                                                                        "`ReaderID` smallint unsigned NOT NULL default '0'," +
                                                                        "`TagID` smallint unsigned NOT NULL default '0'," +
                                                                        "`RSSI` tinyint unsigned NOT NULL default '0'," +
                                                                        "`Timestamp` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP," +
                                                                        "`Index` int(10) unsigned NOT NULL auto_increment," +
                                                                        "PRIMARY KEY  (`Index`)" +
                                                                        ") ENGINE=MyISAM AUTO_INCREMENT=11999 DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS tagactivity (" +
                                                                        "`TagID` varchar(12) NOT NULL default ''," +
                                                                        "`Event` varchar(50) NOT NULL default ''," +
                                                                        "`ZoneID` int unsigned NOT NULL default '0'," +
                                                                        "`Timestamp` datetime NOT NULL default '0000-00-00 00:00:00'," +
                                                                        "PRIMARY KEY  (`TagID`)" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS tags (" +
                                                                        "`TagID` varchar(15) NOT NULL default ''," +
                                                                        "`ID` smallint unsigned NOT NULL default '0'," +
                                                                        "`Type` char(3) NOT NULL default ''," +
                                                                        "`FirstName` varchar(45) default NULL," +
                                                                        "`LastName` varchar(45) default NULL," +
                                                                        "`Image` mediumblob," +
                                                                        "PRIMARY KEY  (`TagID`)" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS Groups (" +
                                                                        "`GroupID` int unsigned NOT NULL auto_increment unique," +
                                                                        "`Name` varchar(32) NOT NULL," +
                                                                        "`Description` varchar(256) NOT NULL default ''," +
                                                                        "PRIMARY KEY  (`GroupID`)" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS GroupZones (" +
                                                                        "`GroupID` int unsigned NOT NULL," +
                                                                        "`ZoneID` int unsigned NOT NULL" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS GroupSchedules (" +
                                                                        "`GroupID` int unsigned NOT NULL," +
                                                                        "`Name` varchar(32) NOT NULL," +
                                                                        "`Access` bool NOT NULL," +
                                                                        "`DateFrom` date default NULL," +
                                                                        "`DateTo` date default NULL," +
                                                                        "`TimeFrom` time default NULL," +
                                                                        "`TimeTo` time default NULL," +
                                                                        "`Mondays` bool NOT NULL default '0'," +
                                                                        "`Tuesdays` bool NOT NULL default '0'," +
                                                                        "`Wednesdays` bool NOT NULL default '0'," +
                                                                        "`Thursdays` bool NOT NULL default '0'," +
                                                                        "`Fridays` bool NOT NULL default '0'," +
                                                                        "`Saturdays` bool NOT NULL default '0'," +
                                                                        "`Sundays` bool NOT NULL default '0'" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS tagtypes (" +
                                                                        "`Type` char(3) NOT NULL default ''," +
                                                                        "`Description` varchar(45) NOT NULL default ''," +
                                                                        "`Icon` blob" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS traffic (" +
                                                                        "`Index` int(10) unsigned NOT NULL auto_increment," +
                                                                        "`TagID` varchar(12) default NULL," +
                                                                        "`FirstName` varchar(45) default NULL," +
                                                                        "`LastName` varchar(45) default NULL," +
                                                                        "`ZoneID` int(11) default NULL," +
                                                                        "`Location` varchar(45) default NULL," +
                                                                        "`Status` varchar(20) default NULL," +
                                                                        "`Event` varchar(75) default NULL," +
                                                                        "`Image` mediumblob," +
                                                                        "`Type` int(11) default NULL," +
                                                                        "`Department` varchar(100) default NULL," +
                                                                        "`Time` datetime NOT NULL default '0000-00-00 00:00:00'," +
                                                                        "PRIMARY KEY  (`Index`)" +
                                                                        ") ENGINE=MyISAM AUTO_INCREMENT=499 DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    using (OdbcCommand cmdCreateTable = new OdbcCommand("CREATE TABLE IF NOT EXISTS zones (" +
                                                                        "`ID` int unsigned NOT NULL default '0'," +
                                                                        "`Location` varchar(45) NOT NULL default ''," +
                                                                        "`ReaderID` smallint unsigned NOT NULL default '0'," +
                                                                        "`FieldGenID` smallint unsigned NOT NULL default '0'," +
                                                                        "`Status` varchar(50) NOT NULL default 'Offline'," +
                                                                        "`Time` datetime default NULL," +
                                                                        "`RSSI` tinyint unsigned NOT NULL default '0'," +
                                                                        "`Threshold` tinyint unsigned default NULL," +
                                                                        "`ReaderType` tinyint(1) unsigned NOT NULL default '0'," +
                                                                        "PRIMARY KEY  (`ID`)" +
                                                                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1",
                                                                        m_connection))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    if (DatabaseCheck.IsOldDatabaseFormat())
                    {
                        MessageBox.Show("Database with old format detected. Please close the application and delete all tables from the database (MySQL DROP TABLE command). The application will create the new format tables next time it executes.", "SmartTracker", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    BuildVirtualList();
					//FIX ME awiHistoryControl1.UpdateTagsPage(true);
					PlaySound(3);
				}
			}
			else if (stat == status.broken)
			{
				m_connection = null;
				dbConnDisplay = false;
				DBStatusBarPanel.Text = "Database : Connection Lost";

				try
				{
                    DBStatusBarPanel.Icon = Resources.DbDisconnected;
				}
				catch
				{
                    DBStatusBarPanel.Icon = null;
				}
			}
			else if (stat == status.close)
			{
				m_connection = null;
				dbConnDisplay = false;
				if (providerName == dbProvider.SQL)
				{
					DBStatusBarPanel.Text = "DB Server : SQL  Disconnected";	
				}
				else
				{
					DBStatusBarPanel.Text = "DB Server : MySQL  Disconnected";
				}
                
				try
				{
                    DBStatusBarPanel.Icon = Resources.DbDisconnected;
				}
				catch
				{
                    DBStatusBarPanel.Icon = null;
				}

				//timer2.Enabled = false;
			}
		}

		private void EnableTagEventNotify(AW_API_NET.rfTagEvent_t tagEvent)
		{
			Console.WriteLine("RECEIVED ENABLE TAG  " +  DateTime.Now.ToString());
			short b = CommunicationClass.CheckMultiTag(tagEvent);
			if ((b == -2) || (b == 0)) 
			{
				//create enable command to match and delete
				cmdStruct cmd = new cmdStruct();
				cmd.rdrID = tagEvent.reader;
				cmd.tagID = tagEvent.tag.id;
				if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
					cmd.tagType = "AST";
				else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
					cmd.tagType = "ACC";
				else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
					cmd.tagType = "INV";
				cmd.cmd = 0x01;  //enable tag
				short index = 0;
				if ((index = cmdCollection.IsEqual(cmd)) >= 0)
					cmdCollection.RemoveFrom(index);

				sendTagNotified = false;
				ListViewItem listItem = new ListViewItem("");
				listItem.SubItems.Add(tagEvent.tag.id.ToString());          //tagid
				listItem.SubItems.Add("Car");           //tag type
				listItem.SubItems.Add("");     //parking slot number
				listItem.SubItems.Add("");       //location
				if (tagEvent.pktID < 100)
					listItem.SubItems.Add("Wrong Space");         //status
				else
					listItem.SubItems.Add("Illegal Parking");         //status
				listItem.SubItems.Add("");        //alarm id
				if (tagEvent.pktID < 100)
					listItem.SubItems.Add("Notified tag " + tagEvent.tag.id.ToString() + " for wrong parking space");  //event description
				else
					listItem.SubItems.Add("Notified tag " + tagEvent.tag.id.ToString() + " for parking crossing the line");  //event description
				listItem.SubItems.Add(DateTime.Now.ToString());           //time
				string cStr = tagEvent.tag.id.ToString();
				cStr += tagEvent.reader.ToString();
				cStr += tagEvent.fGenerator.ToString();
				if (!IsDupTagDisplay(listItem, 5, cStr))
				{
                    lvi[virtualListIndex++] = listItem;
				}

			}
		}

		private void QueryTagEventNotify(AW_API_NET.rfTagEvent_t tagEvent)
		{
			//build comand query to match and delete
			Console.WriteLine("RECEIVED  QUERY ACK COMMAND  "  + DateTime.Now.ToString() );

			cmdStruct cmd = new cmdStruct();
			cmd.rdrID = tagEvent.reader;
			cmd.tagID = tagEvent.tag.id;
			if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
				cmd.tagType = "AST";
			else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
				cmd.tagType = "ACC";
			else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
				cmd.tagType = "INV";
			cmd.cmd = 0x02;  //query tag

			short index = 0;
			if ((index = cmdCollection.IsEqual(cmd)) >= 0)
				cmdCollection.RemoveFrom(index);

			if (!tagEvent.tag.status.tamperSwitch)
				return;

			short b = CommunicationClass.CheckMultiTag(tagEvent);
			if ((b == -2) || (b == 0)) 
			{
				lock(timerLock)
				{
					string loc = "";
					string status = "";
					string pNum = GetParkInfo (tagEvent.tag.id, out loc, out status);
					ListViewItem listItem = new ListViewItem("");
					listItem.SubItems.Add(tagEvent.tag.id.ToString());          //tagid
					listItem.SubItems.Add("Line");           //tag type
					listItem.SubItems.Add(pNum);     //parking slot number
					listItem.SubItems.Add(loc);       //location
					listItem.SubItems.Add("Illegal Parking");         //status
					listItem.SubItems.Add("2");        //alarm id
					listItem.SubItems.Add("Car is parked crossing the line");  //event description
					listItem.SubItems.Add(DateTime.Now.ToString());           //time
					string cStr = tagEvent.tag.id.ToString();
					cStr += tagEvent.reader.ToString();
					cStr += tagEvent.fGenerator.ToString();
					if (!IsDupTagDisplay(listItem, 5, cStr))
					{
						listView.Items.Add(listItem);
                        listView.Items[listView.Items.Count-1].EnsureVisible();
						listView.Items[listView.Items.Count-1].ForeColor = System.Drawing.Color.Red;
						PlaySound(2);

						uint tagID;
						Console.WriteLine("RECEIVED  QUERY ACK BEFOR  "  + DateTime.Now.ToString() );
						if (communication.GetAssetTag(tagEvent.reader, tagEvent.fGenerator, out tagID))
						{
							Console.WriteLine("RECEIVED  QUERY ACK AFTER  "  + DateTime.Now.ToString() );
							cmdStruct cmd1 = new cmdStruct();
							cmd1.rdrID = tagEvent.reader;
							cmd1.tagID = tagID;
							cmd1.tagType = "AST";
							cmd1.cmd = 0x01;  //enable tag
							cmd1.led = true;
							cmd1.speaker = true;
							cmd1.timeStamp = DateTime.Now;
							cmd1.waitTime = DateTime.Now;
							cmd1.retry = 0;
							cmd1.type = 0x01;  //line
							if ((pID < 100) || (pID > 224))
								pID = 100;
							else
								pID += 1;
							cmd1.pID = pID;  //line violation
							index = 0;

							//put the command in the que in case it not got through
							if ((index = cmdCollection.IsEqual(cmd1)) < 0)
							{
								cmdCollection.Add(cmd1);
							}

							//send the command now
							communication.EnableTag(tagEvent.reader, tagID, "AST", true, true, pID);
						}
					}
				}//lock
			}
		}

		bool IsDupTagDisplay(ListViewItem listItem, ushort ct, string cStr)
		{

			string s = "";
			for (int i=0; i<=ct; i++)
				s += listItem.SubItems[i].Text.ToString();
			tagDisplayInfoStruct tdinfo = new tagDisplayInfoStruct();
			tdinfo.str = s;
			tdinfo.compStr = cStr;
			tdinfo.timeStamp = DateTime.Now;

			int index = 0;
			if ((index=tagDisplayCollection.Exits(cStr)) >= 0)
			{
				if (tagDisplayCollection.IsExpired(index))
				{
					Console.WriteLine("Tag Expired Remove:" + tagDisplayCollection[index].str + "  timestamp:" + tagDisplayCollection[index].timeStamp);
					tagDisplayCollection.RemoveFrom(index);
					tagDisplayCollection.Add(tdinfo);
					Console.WriteLine("Tag Add:" + tdinfo.str + "  timestamp:" + tdinfo.timeStamp);
					return (false);
				}
				else
					return (true);
					
			}
			else
			{
				tagDisplayCollection.Add(tdinfo);
				return (false);
			}
			  
			//}//else

		}


		private void StatusParkingSlot(itemInfoSturct item)
		{
			ListViewItem listItem = new ListViewItem("");
			listItem.SubItems.Add(item.tagID.ToString());          //tagid
			listItem.SubItems.Add(item.type);           //tag type
			listItem.SubItems.Add(item.parkingNum);     //parking slot number
			listItem.SubItems.Add(item.location);       //location
			listItem.SubItems.Add(item.status);         //status
			listItem.SubItems.Add(item.alarmID);        //alarm id
			listItem.SubItems.Add(item.eventDescript);  //event description
			listItem.SubItems.Add(item.time);           //time

			string emptyStr = "";
			if (!IsDupTagDisplay(listItem, 5, emptyStr))
			{
				listView.Items.Add(listItem);
                listView.Items[listView.Items.Count-1].EnsureVisible();
			}
			else
				return;
			
			if ((item.alarmID == "1") || (item.alarmID == "2"))
			{
				listView.Items[listView.Items.Count-1].ForeColor = System.Drawing.Color.Red;
				PlaySound(2);
			}
			else if ((item.eventDescript == "Owner Parking Space Not Defined") || 
				(item.eventDescript == "Parking Space Not Defined"))
			{
				listView.Items[listView.Items.Count-1].ForeColor = System.Drawing.Color.Purple;
				PlaySound(1);
			}
			else if (item.eventDescript == "Parking Space OK")
			{
				listView.Items[listView.Items.Count-1].ForeColor = System.Drawing.Color.Blue;
				PlaySound(1);
			}

			if ((item.alarmID == "1") || (item.alarmID == "2"))
			{
				//enable tag with led on
				string type = null;
				if (item.type == "Car")
					type = "AST";
				else if (item.type == "Owner")
					type = "ACC";
				else
					return;
				sendTagNotified = true;
				//CleanupTimer.Enabled = true;
				Console.WriteLine("sendTagNotified  ENABLED");
				cmdStruct cmd = new cmdStruct();
				cmd.rdrID = item.rdrID;
				cmd.tagID = item.tagID;
				cmd.tagType = type;
				cmd.waitTime = DateTime.Now;
				cmd.timeStamp = DateTime.Now;
				if (pID >= 100) 
					pID = 1;
				else
					pID += 1;
				cmd.pID = pID;  //space violation
				cmd.cmd = 0x01;  //enable tag
				cmd.led = true;
				cmd.speaker = false;
				cmd.retry = 0;
				cmd.type = 0x00;
				short index = 0;

				//put the command in que in case it did not get executed
				if ((index = cmdCollection.IsEqual(cmd)) < 0)
				{
					cmdCollection.Add(cmd);
				}

				//send the command now
				communication.EnableTag(item.rdrID, item.tagID, type, true, true, pID); 
			}

		}

		string GetDepartmentName(string tagID)
		{
			if (MainForm.m_connection == null)
				return ("");
		   
			string str = "";
		   
			lock (m_connection)  //dec-06-06
			{
				StringBuilder mySelectQuery = new StringBuilder();

				mySelectQuery.Append("SELECT Department FROM employees WHERE ID = "); 
				mySelectQuery.AppendFormat("'{0}'", tagID);          
			
				string mySelectStr = mySelectQuery.ToString();

				OdbcCommand myCommand = new OdbcCommand(mySelectStr, MainForm.m_connection);  
				OdbcDataReader myReader = null;
		   
				try
				{
					myReader = myCommand.ExecuteReader();
					reconnectCounter = -1;
				}
				catch (Exception ex)
				{
					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{  
						//error code 2013

						if (reconnectCounter < 0)
						{
							reconnectCounter = 0;
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
				{
					str = myReader.GetString(0);
				}
			
				myReader.Close();
				return str;
			}//lock m_connection
		}

		private void TagDetected (AW_API_NET.rfTagEvent_t tagEvent)
		{
			if (CommDialogActivated || (m_connection == null) || closingProcess)
				return;
			if (autoReset)
				resetCounter = 0;

            Console.WriteLine("TagDetected: ID {0}", tagEvent.tag.id);

            //here there is need to send eneable/disable relay 1/2
			//if invalid tag alam/warning
			//if valid tag enable(open) relay 1,2
			//tagEvent.tag.
			lock(CommunicationClass.commLock) //&&&&LOCLLOCK

				lock(tagDetectLock)
				{
					//lock(timerLock)
				{
					//string zoneID = "0";
					string loc = "";
					string status = "";
					string parkNum = "";
					string pName = "";
					string accType = "";
					string dept = "";
					string eventDesc = "";
					string note = "";
					bool alarm = false;
					string eventStatus = "";
					string stype = "";

			 
					//parkNum = zoneID
					parkNum = GetParkInfo(tagEvent.reader, tagEvent.fGenerator, out loc, out status);
					//Console.WriteLine("ZONE ID = " + parkNum);
					pName = GetTagName(tagEvent.tag.id, tagEvent.tag.tagType, out note);
					dept = GetDepartmentName(Convert.ToString(tagEvent.tag.id));
					accType = GetTagAccType(tagEvent.tag.id);

					EnableReaderStatus(tagEvent.reader, tagEvent.host, tagEvent.fGenerator);
				   
					if (tagEvent.tag.tagType == 1) 
						stype = "ACC";
					else if (tagEvent.tag.tagType == 3) 
						stype = "AST";
					Image tagImage = GetTagImage(Convert.ToString(tagEvent.tag.id), stype);
				   
					DateTime t = new DateTime();
					t = DateTime.Now;

                    if (Convert.ToString(tagEvent.tag.id) == "401")
                        t = DateTime.Now;

					if (IsTagValid(tagEvent.tag.id, tagEvent.tag.tagType))
					{
					   
						if (!IsTagDuplicated(tagEvent.tag.id, tagEvent.tag.tagType))
						{
							//call tag or tag entered a field
							if (tagEvent.tag.status.continuousField == true)
							{
							   
								ListViewItem listItem = new ListViewItem("");
								listItem.SubItems.Add(tagEvent.tag.id.ToString());     //parcel ID
								listItem.SubItems.Add(pName);           //parcel description
								//listItem.SubItems.Add(parkNum);        //zone ID
								listItem.SubItems.Add(loc);       //location
								//listItem.SubItems.Add("Valid");       //status

                                var access = true;

								if (tagEvent.tag.tagType == 0x03) //asset
								{
									if (note.Length > 0) //mobile true
									{
										listItem.SubItems.Add("Valid");       //status
										eventStatus = "Valid";
										listItem.SubItems.Add("Asset moving with permission"); 
										eventDesc = "Asset moving with permission";
									}
									else
									{
										listItem.SubItems.Add("Alarm");       //status
										eventStatus = "Alarm";
										listItem.SubItems.Add("Asset moving without permission");
										eventDesc = "Asset moving without permission";
										alarm = true;
									}
								}
                                else if (tagEvent.tag.tagType == 0x01) //Access
                                {
                                    listItem.SubItems.Add("Valid");       //status
                                    eventStatus = "Valid";

                                    access = EmployeesQuery.CheckAccess(tagEvent.tag.id, Convert.ToInt32(parkNum), DateTime.Now);

                                    if (accType == "Guest")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                        eventDesc = String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                    }
                                    else if (accType == "Temporary")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));
                                        eventDesc = String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                    }
                                    else
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                        eventDesc = String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                    }
                                }
                                else
                                {
                                    listItem.SubItems.Add("Valid");       //status
                                    eventStatus = "Valid";

                                    if (accType == "Guest")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                        eventDesc = String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                    }
                                    else if (accType == "Temporary")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));
                                        eventDesc = String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                    }
                                    else
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                        eventDesc = String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                    }
                                }
								listItem.SubItems.Add(DateTime.Now.ToString());         //time
								string cStr = tagEvent.tag.id.ToString();
                                cStr += tagEvent.reader.ToString();
								cStr += tagEvent.fGenerator.ToString();
								if (!IsDupTagDisplay(listItem, 5, cStr))
								{
									if (alarm)
									{
										PlaySound(2);
									}
									else
									{
										PlaySound(1);
									}
									PopulateActivityTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, false, eventDesc);
									PopulateHistoryTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, eventDesc);
									SaveTraffic(tagEvent.tag.id.ToString(), pName, null, dept, parkNum, loc, eventStatus, eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType));
                                    if (access)
                                    {
                                        StartActionThread(TAG_DETECTED, tagEvent);
                                    }
								}
							   
							}
							else   //tag detected by a reader probably at the gate
							{
								ListViewItem listItem = new ListViewItem("");
								listItem.SubItems.Add(tagEvent.tag.id.ToString());   //tagid
								listItem.SubItems.Add(pName);         //tag description
								listItem.SubItems.Add(loc);       //location

                                var access = true;

								if (tagEvent.tag.tagType == 0x03) //asset
								{
									if (note.Length > 0) //mobile true
									{
										listItem.SubItems.Add("Valid");       //status
										eventStatus = "Valid";
										listItem.SubItems.Add("Asset moving with permission"); 
										eventDesc = "Asset moving with permission";
									}
									else
									{
										listItem.SubItems.Add("Alarm");       //status
										eventStatus = "Alarm";
										alarm = true;
										listItem.SubItems.Add("Asset moving without permission");
										eventDesc = "Asset moving without permission";
									}
								}
                                else if (tagEvent.tag.tagType == 0x01) //Access
                                {
                                    listItem.SubItems.Add("Valid");       //status
                                    eventStatus = "Valid";

                                    access = EmployeesQuery.CheckAccess(tagEvent.tag.id, Convert.ToInt32(parkNum), DateTime.Now);

                                    if (accType == "Guest")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                        eventDesc = String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                    }
                                    else if (accType == "Temporary")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));
                                        eventDesc = String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                    }
                                    else
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                        eventDesc = String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                    }
                                }
                                else
								{
									listItem.SubItems.Add("Valid");       //status
									eventStatus = "Valid";

                                    if (accType == "Guest")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                        eventDesc = String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                    }
                                    else if (accType == "Temporary")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));
                                        eventDesc = String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                    }
                                    else
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                        eventDesc = String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                    }
                                }

								listItem.SubItems.Add(DateTime.Now.ToString());           //time
								string cStr = tagEvent.tag.id.ToString();
								cStr += tagEvent.reader.ToString();
								cStr += tagEvent.fGenerator.ToString();
								if (!IsDupTagDisplay(listItem, 5, cStr))
								{
									if (alarm)
									{
										PlaySound(2);
									}
									else
									{
										PlaySound(1);
									}
									PopulateActivityTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, false, eventDesc);
									PopulateHistoryTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, eventDesc);
									SaveTraffic(tagEvent.tag.id.ToString(), pName, null, dept, parkNum, loc, eventStatus, eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType));
                                    if (access)
                                    {
                                        StartActionThread(TAG_DETECTED, tagEvent);
                                    }
                                }
							}//tag detected by a reader  

						}//IsTagDuplicated
						else
						{
							//tag already exits
						   
							short b = CommunicationClass.CheckMultiTag(tagEvent);
							if ((b == -2) || (b >= 0))    //multiTag
							{
								//call tag or tag entered a field
								if (tagEvent.tag.status.continuousField == true)
								{
								   
									ListViewItem listItem = new ListViewItem("");
									listItem.SubItems.Add(tagEvent.tag.id.ToString());   //tagid
									listItem.SubItems.Add(pName);         //tag description
									listItem.SubItems.Add(loc);       //location

                                    var access = true;

									if (tagEvent.tag.tagType == 0x03) //asset
									{
										if (note.Length > 0) //mobile true
										{
											listItem.SubItems.Add("Valid");       //status
											eventStatus = "Valid";
											listItem.SubItems.Add("Asset moving with permission"); 
											eventDesc = "Asset moving with permission";
										}
										else
										{
											listItem.SubItems.Add("Alarm");       //status
											eventStatus = "Alarm";
											alarm = true;
											listItem.SubItems.Add("Asset moving without permission");
											eventDesc = "Asset moving without permission";
										}
									}
                                    else if (tagEvent.tag.tagType == 0x01) //Access
                                    {
                                        listItem.SubItems.Add("Valid");       //status
                                        eventStatus = "Valid";

                                        access = EmployeesQuery.CheckAccess(tagEvent.tag.id, Convert.ToInt32(parkNum), DateTime.Now);

                                        if (accType == "Guest")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                            eventDesc = String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                        }
                                        else if (accType == "Temporary")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));
                                            eventDesc = String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                        }
                                        else
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                            eventDesc = String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                        }
                                    }
                                    else
									{
										listItem.SubItems.Add("Valid");       //status
										eventStatus = "Valid";

                                        if (accType == "Guest")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                            eventDesc = String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                        }
                                        else if (accType == "Temporary")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));
                                            eventDesc = String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                        }
                                        else
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                            eventDesc = String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                        }
                                    }

									listItem.SubItems.Add(DateTime.Now.ToString());           //time
									string cStr = tagEvent.tag.id.ToString();
									cStr += tagEvent.reader.ToString();
									cStr += tagEvent.fGenerator.ToString();
									if (!IsDupTagDisplay(listItem, 5, cStr))
									{
										if (alarm)
										{
											PlaySound(2);
										}
										else
										{
											PlaySound(1);
										}
										PopulateActivityTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, false, eventDesc);
										PopulateHistoryTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, eventDesc);
										SaveTraffic(tagEvent.tag.id.ToString(), pName, null, dept, parkNum, loc, eventStatus, eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType));
                                        if (access)
                                        {
                                            StartActionThread(TAG_DETECTED, tagEvent);
                                        }
                                    }
								}//continous field
								else
								{
									ListViewItem listItem = new ListViewItem("");
									listItem.SubItems.Add(tagEvent.tag.id.ToString());   //tagid
									listItem.SubItems.Add(pName);         //tag description
									listItem.SubItems.Add(loc);       //location

                                    var access = true;

									if (tagEvent.tag.tagType == 0x03) //asset
									{
										if (note.Length > 0) //mobile true
										{
											listItem.SubItems.Add("Valid");       //status
											eventStatus = "Valid";
											listItem.SubItems.Add("Asset moving with permission"); 
											eventDesc = "Asset moving with permission";
										}
										else
										{
											listItem.SubItems.Add("Alarm");       //status
											eventStatus = "Alarm";
											listItem.SubItems.Add("Asset moving without permission");
											eventDesc = "Asset moving without permission";
											alarm = true;
										}
									}
                                    else if (tagEvent.tag.tagType == 0x01) //Access
                                    {
                                        listItem.SubItems.Add("Valid");       //status
                                        eventStatus = "Valid";

                                        access = EmployeesQuery.CheckAccess(tagEvent.tag.id, Convert.ToInt32(parkNum), DateTime.Now);

                                        if (accType == "Guest")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                            eventDesc = String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                        }
                                        else if (accType == "Temporary")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));
                                            eventDesc = String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                        }
                                        else
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                            eventDesc = String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                        }
                                    }
                                    else
									{
										listItem.SubItems.Add("Valid");       //status
										eventStatus = "Valid";

                                        if (accType == "Guest")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                            eventDesc = String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                        }
                                        else if (accType == "Temporary")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));
                                            eventDesc = String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                        }
                                        else
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                            eventDesc = String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                        }
                                    }

									listItem.SubItems.Add(DateTime.Now.ToString());           //time
									string cStr = tagEvent.tag.id.ToString();
									cStr += tagEvent.reader.ToString();
									cStr += tagEvent.fGenerator.ToString();
									if (!IsDupTagDisplay(listItem, 5, cStr))
									{
										Console.WriteLine(DateTime.Now.ToString());
										if (alarm)
										{
											PlaySound(2);
										}
										else
										{
											PlaySound(1);
										}
										PopulateActivityTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, false, eventDesc);
										PopulateHistoryTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, eventDesc);
										SaveTraffic(tagEvent.tag.id.ToString(), pName, null, dept, parkNum, loc, eventStatus, eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType));
                                        if (access)
                                        {
                                            StartActionThread(TAG_DETECTED, tagEvent);
                                        }
                                    }
								}
 
							}//if not multitag
						   
						}//if tag	duplicated
					}//if valid tag
					else	 //invalid tag
					{
						short b = CommunicationClass.CheckMultiTag(tagEvent);
						//9999 zoneID = GetZoneID(tagEvent.reader, tagEvent.fGenerator);
						if ((b == -2) || (b >= 0))		
						{	  
							//Tag call or tag entered a field
							if (tagEvent.tag.status.continuousField == true)
							{
								ListViewItem listItem = new ListViewItem("IN");
								listItem.SubItems.Add(tagEvent.tag.id.ToString());   //tagid
								listItem.SubItems.Add(pName);         //tag description
								listItem.SubItems.Add(loc);       //location
								eventStatus = "Invalid";


                                if (accType == "Guest")
                                {
                                    listItem.SubItems.Add(String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                    eventDesc = String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                }
                                else if (accType == "Temporary")
                                {
                                    listItem.SubItems.Add(String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));
                                    eventDesc = String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                }
                                else
                                {
                                    listItem.SubItems.Add(String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                    eventDesc = String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                }

								listItem.SubItems.Add(DateTime.Now.ToString());           //time
								string cStr = tagEvent.tag.id.ToString();
								cStr += tagEvent.reader.ToString();
								cStr += tagEvent.fGenerator.ToString();
								if (!IsDupTagDisplay(listItem, 5, cStr))
								{
                                    SaveTraffic(tagEvent.tag.id.ToString(), pName, null, dept, parkNum, loc, "Invalid", eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType));
                                    StartActionThread(INVALID_TAG_DETECTED, tagEvent);
                                }
						
								PlaySound(1);
							}
							else  //invalid tag detected by the reader
							{
								ListViewItem listItem = new ListViewItem("IN");
								listItem.SubItems.Add(tagEvent.tag.id.ToString());   //tagid
								listItem.SubItems.Add(pName);         //tag description
								listItem.SubItems.Add(loc);       //location
								eventStatus = "Invalid";

                                if (accType == "Guest")
                                {
                                    listItem.SubItems.Add(String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                    eventDesc = String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                }
                                else if (accType == "Temporary")
                                {
                                    listItem.SubItems.Add(String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));
                                    eventDesc = String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                }
                                else
                                {
                                    listItem.SubItems.Add(String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                    eventDesc = String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                }

								listItem.SubItems.Add(DateTime.Now.ToString());           //time
								string cStr = tagEvent.tag.id.ToString();
								cStr += tagEvent.reader.ToString();
								cStr += tagEvent.fGenerator.ToString();
								if (!IsDupTagDisplay(listItem, 5, cStr))
								{                                    
                                    SaveTraffic(tagEvent.tag.id.ToString(), pName, null, dept, parkNum, loc, "Invalid",eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType));
                                    StartActionThread(INVALID_TAG_DETECTED, tagEvent);
                                }
					
								PlaySound(1);
							}//invalid tag by reader

						}// (b == -2) || (b >= 0)
					}//invalid tag

				}//timerlock
				}//communication lock
		}//TagDetected

		private void TagDetectedRSSI (AW_API_NET.rfTagEvent_t tagEvent)
		{
			if (CommDialogActivated || (m_connection == null) || closingProcess)
				return;
			if (autoReset)
				resetCounter = 0;

            Console.WriteLine("TagDetected RSSI: ID {0}, cmdType {1}, Event {2}, Status {3}, Sani Status {4}, Unit {5}", tagEvent.tag.id, tagEvent.cmdType, tagEvent.eventType, tagEvent.eventStatus, tagEvent.tag.sani.Status.Description(), tagEvent.tag.sani.UnitType.Description());

			//here there is need to send eneable/disable relay 1/2
			//if invalid tag alam/warning
			//if valid tag enable(open) relay 1,2
			//tagEvent.tag.
			lock(CommunicationClass.commLock) //&&&&LOCLLOCK

				lock(tagDetectLock)
				{
					//lock(timerLock)
				{
					//string zoneID = "0";
					string loc = "";
					string status = "";
					string parkNum = "";
					string pName = "";
					string accType = "";
					string dept = "";
					string eventDesc = "";
					string note = "";
					bool alarm = false;
					string eventStatus = "";
					string stype = "";
					short threshold = 0;
                    int localIndex = 0;


                    if (listView.Items.Count == 0)
                        localIndex = 0;
                    else
                        localIndex = listView.Items.Count - 1;

					//parkNum = zoneID
					parkNum = GetZoneInfo(tagEvent.reader, tagEvent.fGenerator, out loc, out status, out threshold);
					//Console.WriteLine("ZONE ID = " + parkNum);
					
					//This is a test routine as requested by Jerry Jan24,07
					SaveRSSI(tagEvent);

					if (m_updateRSSIListView != null)
						m_updateRSSIListView(tagEvent);

					if (tagEvent.RSSI < threshold)
					{
						//Console.WriteLine("Tag was rejected Threshold = " + Convert.ToString(threshold) + "  RSSI = " + Convert.ToString(tagEvent.RSSI));
                        return;
					}

					//Console.WriteLine("Tag was accepted Threshold = " + Convert.ToString(threshold) + "  RSSI = " + Convert.ToString(tagEvent.RSSI));

					pName = GetTagName(tagEvent.tag.id, tagEvent.tag.tagType, out note);
					dept = GetDepartmentName(Convert.ToString(tagEvent.tag.id));
					accType = GetTagAccType(tagEvent.tag.id);

					EnableReaderStatus(tagEvent.reader, tagEvent.host, tagEvent.fGenerator);
				   
					if (tagEvent.tag.tagType == 1) 
						stype = "ACC";
					else if (tagEvent.tag.tagType == 3) 
						stype = "AST";
					Image tagImage = GetTagImage(Convert.ToString(tagEvent.tag.id), stype);
				   
					DateTime t = new DateTime();
					t = DateTime.Now;

                    //if (Convert.ToString(tagEvent.tag.id) == "401")  TESTING
                        //t = DateTime.Now;

					if (IsTagValid(tagEvent.tag.id, tagEvent.tag.tagType))
					{
					   
						if (!IsTagDuplicated(tagEvent.tag.id, tagEvent.tag.tagType))
						{
							//call tag or tag entered a field
							if (tagEvent.tag.status.continuousField == true)
							{
							   
								ListViewItem listItem = new ListViewItem("VA");
								listItem.SubItems.Add(tagEvent.tag.id.ToString());     //parcel ID
								listItem.SubItems.Add(pName);           //parcel description
								//listItem.SubItems.Add(parkNum);        //zone ID
								listItem.SubItems.Add(loc);       //location
								//listItem.SubItems.Add("Valid");       //status

                                var access = true;

								if (tagEvent.tag.tagType == 0x03) //asset
								{
									if (note.Length > 0) //mobile true
									{
										//listItem.SubItems.Add("Valid");       //status
										eventStatus = "Valid";
										listItem.SubItems.Add("Asset moving with permission"); 
										eventDesc = "Asset moving with permission";
									}
									else
									{
										//listItem.SubItems.Add("Alarm");       //status
										eventStatus = "Alarm";
										listItem.SubItems.Add("Asset moving without permission");
										eventDesc = "Asset moving without permission";
										alarm = true;
									}
								}
                                else if (tagEvent.tag.tagType == 0x01) //Access
                                {
                                    //listItem.SubItems.Add("Valid");       //status
                                    eventStatus = "Valid";

                                    access = EmployeesQuery.CheckAccess(tagEvent.tag.id, Convert.ToInt32(parkNum), DateTime.Now);

                                    if (accType == "Guest")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                        eventDesc = String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                    }
                                    else if (accType == "Temporary")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));
                                        eventDesc = String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                    }
                                    else
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                        eventDesc = String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                    }
                                }
                                else
								{
									//listItem.SubItems.Add("Valid");       //status
									eventStatus = "Valid";

									if (accType == "Guest")
									{
										listItem.SubItems.Add(String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
										eventDesc = String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
									}
									else if (accType == "Temporary")
									{
										listItem.SubItems.Add(String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));
										eventDesc = String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
									}
									else
									{
										listItem.SubItems.Add(String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                        eventDesc = String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
									}
								}
								listItem.SubItems.Add(DateTime.Now.ToString());         //time
								string cStr = tagEvent.tag.id.ToString();
								cStr += tagEvent.reader.ToString();
								cStr += tagEvent.fGenerator.ToString();
								if (!IsDupTagDisplay(listItem, 5, cStr))
								{
									if (alarm)
									{
										PlaySound(2);
									}
									else
									{
										PlaySound(1);
									}
									PopulateActivityTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, false, eventDesc);
									PopulateHistoryTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, eventDesc);
									SaveTraffic(tagEvent.tag.id.ToString(), pName, null, dept, parkNum, loc, eventStatus, eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType));
                                    if (access)
                                    {
                                        StartActionThread(TAG_DETECTED, tagEvent);
                                    }
                                }
							   
							}
							else   //tag detected by a reader probably at the gate
							{
								ListViewItem listItem = new ListViewItem("VA");
								listItem.SubItems.Add(tagEvent.tag.id.ToString());   //tagid
								listItem.SubItems.Add(pName);         //tag description
								listItem.SubItems.Add(loc);       //location

                                var access = true;

								if (tagEvent.tag.tagType == 0x03) //asset
								{
									if (note.Length > 0) //mobile true
									{
										eventStatus = "Valid";
										listItem.SubItems.Add("Asset moving with permission"); 
										eventDesc = "Asset moving with permission";
									}
									else
									{
										eventStatus = "Alarm";
										alarm = true;
										listItem.SubItems.Add("Asset moving without permission");
										eventDesc = "Asset moving without permission";
									}
								}
                                else if (tagEvent.tag.tagType == 0x01) //Access
                                {
                                    eventStatus = "Valid";

                                    access = EmployeesQuery.CheckAccess(tagEvent.tag.id, Convert.ToInt32(parkNum), DateTime.Now);

                                    if (accType == "Guest")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                        eventDesc = String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                    }
                                    else if (accType == "Temporary")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));
                                        eventDesc = String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                    }
                                    else
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                        eventDesc = String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                    }
                                }
                                else
								{
									eventStatus = "Valid";

                                    if (accType == "Guest")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                        eventDesc = String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                    }
                                    else if (accType == "Temporary")
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));
                                        eventDesc = String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                    }
                                    else
                                    {
                                        listItem.SubItems.Add(String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                        eventDesc = String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                    }
                                }

								listItem.SubItems.Add(DateTime.Now.ToString());           //time
								string cStr = tagEvent.tag.id.ToString();
								cStr += tagEvent.reader.ToString();
								cStr += tagEvent.fGenerator.ToString();
								if (!IsDupTagDisplay(listItem, 5, cStr))
								{
									if (alarm)
									{
										PlaySound(2);
									}
									else
									{
										PlaySound(1);
									}
									PopulateActivityTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, false, eventDesc);
									PopulateHistoryTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, eventDesc);
									SaveTraffic(tagEvent.tag.id.ToString(), pName, null, dept, parkNum, loc, eventStatus, eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType));
                                    if (access)
                                    {
                                        StartActionThread(TAG_DETECTED, tagEvent);
                                    }
                                }
							}//tag detected by a reader  

						}//IsTagDuplicated
						else
						{
							//tag already exits
						   
							short b = CommunicationClass.CheckMultiTag(tagEvent);
							//if (!communication.CheckMultiTag(tagEvent))
							if ((b == -2) || (b >= 0))    //multiTag
							{
								//call tag or tag entered a field
								if (tagEvent.tag.status.continuousField == true)
								{
								   
									ListViewItem listItem = new ListViewItem("VA");
									listItem.SubItems.Add(tagEvent.tag.id.ToString());   //tagid
									listItem.SubItems.Add(pName);         //tag description
									listItem.SubItems.Add(loc);       //location

                                    var access = true;

									if (tagEvent.tag.tagType == 0x03) //asset
									{
										if (note.Length > 0) //mobile true
										{
											eventStatus = "Valid";
											listItem.SubItems.Add("Asset moving with permission"); 
											eventDesc = "Asset moving with permission";
										}
										else
										{
											eventStatus = "Alarm";
											alarm = true;
											listItem.SubItems.Add("Asset moving without permission");
											eventDesc = "Asset moving without permission";
										}
									}
                                    else if (tagEvent.tag.tagType == 0x01) //Access
                                    {
                                        eventStatus = "Valid";

                                        access = EmployeesQuery.CheckAccess(tagEvent.tag.id, Convert.ToInt32(parkNum), DateTime.Now);

                                        if (accType == "Guest")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                            eventDesc = String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                        }
                                        else if (accType == "Temporary")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));
                                            eventDesc = String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                        }
                                        else
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                            eventDesc = String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                        }
                                    }
                                    else
									{
										eventStatus = "Valid";

                                        if (accType == "Guest")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                            eventDesc = String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                        }
                                        else if (accType == "Temporary")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));
                                            eventDesc = String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                        }
                                        else
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                            eventDesc = String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                        }
                                    }

									listItem.SubItems.Add(DateTime.Now.ToString());           //time
									string cStr = tagEvent.tag.id.ToString();
									cStr += tagEvent.reader.ToString();
									cStr += tagEvent.fGenerator.ToString();
									if (!IsDupTagDisplay(listItem, 5, cStr))
									{
										if (alarm)
										{
											PlaySound(2);
										}
										else
										{
											PlaySound(1);
										}
										PopulateActivityTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, false, eventDesc);
										PopulateHistoryTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, eventDesc);
										SaveTraffic(tagEvent.tag.id.ToString(), pName, null, dept, parkNum, loc, eventStatus, eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType));
                                        if (access)
                                        {
                                            StartActionThread(TAG_DETECTED, tagEvent);
                                        }
									}
								   
								}//continous field
								else
								{
									ListViewItem listItem = new ListViewItem("VA");
									listItem.SubItems.Add(tagEvent.tag.id.ToString());   //tagid
									listItem.SubItems.Add(pName);         //tag description
									listItem.SubItems.Add(loc);       //location

                                    var access = true;

									if (tagEvent.tag.tagType == 0x03) //asset
									{
										if (note.Length > 0) //mobile true
										{
											eventStatus = "Valid";
											listItem.SubItems.Add("Asset moving with permission"); 
											eventDesc = "Asset moving with permission";
										}
										else
										{
											eventStatus = "Alarm";
											listItem.SubItems.Add("Asset moving without permission");
											eventDesc = "Asset moving without permission";
											alarm = true;
										}
									}
                                    else if (tagEvent.tag.tagType == 0x01) //Access
                                    {
                                        eventStatus = "Valid";

                                        access = EmployeesQuery.CheckAccess(tagEvent.tag.id, Convert.ToInt32(parkNum), DateTime.Now);

                                        if (accType == "Guest")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                            eventDesc = String.Format("{0}Tag Detected{1} - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                        }
                                        else if (accType == "Temporary")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));
                                            eventDesc = String.Format("{0}Tag Detected{1} - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                        }
                                        else
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission"));   //event description
                                            eventDesc = String.Format("{0}Tag Detected{1}", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "", access ? "" : " without permission");
                                        }
                                    }
                                    else
									{
										eventStatus = "Valid";
                                        if (accType == "Guest")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                            eventDesc = String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                        }
                                        else if (accType == "Temporary")
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));
                                            eventDesc = String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                        }
                                        else
                                        {
                                            listItem.SubItems.Add(String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                            eventDesc = String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                        }
                                    }

									listItem.SubItems.Add(DateTime.Now.ToString());           //time
									string cStr = tagEvent.tag.id.ToString();
									cStr += tagEvent.reader.ToString();
									cStr += tagEvent.fGenerator.ToString();
									if (!IsDupTagDisplay(listItem, 5, cStr))
									{
										Console.WriteLine(DateTime.Now.ToString());
										if (alarm)
										{
											PlaySound(2);
										}
										else
										{
											PlaySound(1);
										}
										PopulateActivityTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, false, eventDesc);
										PopulateHistoryTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, eventDesc);
										SaveTraffic(tagEvent.tag.id.ToString(), pName, null, dept, parkNum, loc, eventStatus, eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType));
                                        if (access)
                                        {
                                            StartActionThread(TAG_DETECTED, tagEvent);
                                        }
                                    }
								}
							}//if not multitag
						   
						}//if tag	duplicated
					}//if valid tag
					else	 //invalid tag
					{
						short b = CommunicationClass.CheckMultiTag(tagEvent);
						//9999 zoneID = GetZoneID(tagEvent.reader, tagEvent.fGenerator);
						if ((b == -2) || (b >= 0))		
						{	  
							//Tag call or tag entered a field
							if (tagEvent.tag.status.continuousField == true)
							{
								ListViewItem listItem = new ListViewItem("IN");
								listItem.SubItems.Add(tagEvent.tag.id.ToString());   //tagid
								listItem.SubItems.Add(pName);         //tag description
								listItem.SubItems.Add(loc);       //location
								eventStatus = "Invalid";

                                if (accType == "Guest")
                                {
                                    listItem.SubItems.Add(String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                    eventDesc = String.Format("{0}Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                }
                                else if (accType == "Temporary")
                                {
                                    listItem.SubItems.Add(String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));
                                    eventDesc = String.Format("{0}Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                }
                                else
                                {
                                    listItem.SubItems.Add(String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                    eventDesc = String.Format("{0}Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                }

								listItem.SubItems.Add(DateTime.Now.ToString());           //time
								string cStr = tagEvent.tag.id.ToString();
								cStr += tagEvent.reader.ToString();
								cStr += tagEvent.fGenerator.ToString();
								if (!IsDupTagDisplay(listItem, 5, cStr))
								{
									SaveTraffic(tagEvent.tag.id.ToString(), pName, null, dept, parkNum, loc, "Invalid", eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType));
                                    StartActionThread(INVALID_TAG_DETECTED, tagEvent);
                                }
						
								PlaySound(1);
							}
							else  //invalid tag detected by the reader
							{
								ListViewItem listItem = new ListViewItem("IN");
								listItem.SubItems.Add(tagEvent.tag.id.ToString());   //tagid
								listItem.SubItems.Add(pName);         //tag description
								listItem.SubItems.Add(loc);       //location
								eventStatus = "Invalid";
                                if (accType == "Guest")
                                {
                                    listItem.SubItems.Add(String.Format("{0}Invalid Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                    eventDesc = String.Format("{0}Invalid Tag Detected - Guest", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                }
                                else if (accType == "Temporary")
                                {
                                    listItem.SubItems.Add(String.Format("{0}Invalid Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));
                                    eventDesc = String.Format("{0}Invalid Tag Detected - Temporary", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                }
                                else
                                {
                                    listItem.SubItems.Add(String.Format("{0}Invalid Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : ""));   //event description
                                    eventDesc = String.Format("{0}Invalid Tag Detected", tagEvent.tag.status.tamperSwitch ? "Tamper on " : "");
                                }

								listItem.SubItems.Add(DateTime.Now.ToString());           //time
								string cStr = tagEvent.tag.id.ToString();
								cStr += tagEvent.reader.ToString();
								cStr += tagEvent.fGenerator.ToString();
								if (!IsDupTagDisplay(listItem, 5, cStr))
								{
									SaveTraffic(tagEvent.tag.id.ToString(), pName, null, dept, parkNum, loc, "Invalid",eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType));
                                    StartActionThread(INVALID_TAG_DETECTED, tagEvent);
                                }
					
								PlaySound(1);
								//communication.EnableOutput(2, readerID, true);
							}//invalid tag by reader
						}// (b == -2) || (b >= 0)
					}//invalid tag
				}//timerlock
				}//communication lock
		}//TagDetectedRSSI

        private void TagDetectedSani(AW_API_NET.rfTagEvent_t tagEvent)
        {
            if (CommDialogActivated || (m_connection == null) || closingProcess)
                return;
            if (autoReset)
                resetCounter = 0;

            Console.WriteLine("TagDetected Sani: ID {0}, Sani Status {1}, Unit {2}, Event Cnt {3}", tagEvent.tag.id, tagEvent.tag.sani.Status.Description(), tagEvent.tag.sani.UnitType.Description(), tagEvent.tag.sani.EventCnt);

            //here there is need to send enable/disable relay 1/2
            //if invalid tag alarm/warning
            //if valid tag enable(open) relay 1,2
            //tagEvent.tag.
            lock (CommunicationClass.commLock) //&&&&LOCLLOCK

                lock (tagDetectLock)
                {
                    //lock(timerLock)
                    {
                        //string zoneID = "0";
                        string loc = "";
                        string status = "";
                        string parkNum = "";
                        string first_name = "";
                        string last_name = "";
                        string accType = "";
                        string dept = "";
                        string eventDesc = "";
                        string note = "";
                        bool alarm = false;
                        string eventStatus = "";
                        string stype = "";


                        //parkNum = zoneID
                        parkNum = GetParkInfo(tagEvent.reader, tagEvent.fGenerator, out loc, out status);
                        //Console.WriteLine("ZONE ID = " + parkNum);
                        GetTagFirstAndLastName(tagEvent.tag.id, tagEvent.tag.tagType, out first_name, out last_name, out note);
                        dept = GetDepartmentName(Convert.ToString(tagEvent.tag.id));
                        accType = GetTagAccType(tagEvent.tag.id);

                        EnableReaderStatus(tagEvent.reader, tagEvent.host, tagEvent.fGenerator);

                        if (tagEvent.tag.tagType == 1)
                            stype = "ACC";
                        else if (tagEvent.tag.tagType == 3)
                            stype = "AST";
                        Image tagImage = GetTagImage(Convert.ToString(tagEvent.tag.id), stype);

                        DateTime t = new DateTime();
                        t = DateTime.Now;

                        if (Convert.ToString(tagEvent.tag.id) == "401")
                            t = DateTime.Now;

                        if (IsTagValid(tagEvent.tag.id, tagEvent.tag.tagType))
                        {
                            if (!IsSaniTagDuplicated(tagEvent.tag))
                            {
                                ListViewItem listItem = new ListViewItem("");
                                listItem.SubItems.Add(tagEvent.tag.id.ToString());     //parcel ID
                                listItem.SubItems.Add(string.Format("{0} {1}", first_name, last_name).Trim());           //parcel description
                                //listItem.SubItems.Add(parkNum);        //zone ID
                                listItem.SubItems.Add(loc);       //location
                                //listItem.SubItems.Add("Valid");       //status

                                if (tagEvent.tag.tagType == 0x03) //asset
                                {
                                    if (note.Length > 0) //mobile true
                                    {
                                        listItem.SubItems.Add("Valid");       //status
                                        eventStatus = "Valid";
                                        listItem.SubItems.Add("Asset moving with permission");
                                        eventDesc = "Asset moving with permission";
                                    }
                                    else
                                    {
                                        listItem.SubItems.Add("Alarm");       //status
                                        eventStatus = "Alarm";
                                        listItem.SubItems.Add("Asset moving without permission");
                                        eventDesc = "Asset moving without permission";
                                        alarm = true;
                                    }
                                }
                                else
                                {
                                    listItem.SubItems.Add("Valid");       //status
                                    eventStatus = "Valid";

                                    if (accType == "Guest")
                                    {
                                        listItem.SubItems.Add("Sani Tag Detected - Guest");   //event description
                                        eventDesc = "Sani Tag Detected - Guest";
                                    }
                                    else if (accType == "Temporary")
                                    {
                                        listItem.SubItems.Add("Sani Tag Detected - Temporary");
                                        eventDesc = "Sani Tag Detected - Temporary";
                                    }
                                    else
                                    {
                                        listItem.SubItems.Add("Sani Tag Detected");   //event description
                                        eventDesc = "Sani Tag Detected";
                                    }
                                }


                                //listItem.SubItems.Add(DateTime.Now.ToString());         //time
                                //string cStr = tagEvent.tag.id.ToString();
                                //cStr += tagEvent.reader.ToString();
                                //cStr += tagEvent.fGenerator.ToString();
                                //if (IsDupTagDisplay(listItem, 5, cStr))
                                {
                                    if (alarm)
                                    {
                                        PlaySound(2);
                                    }
                                    else
                                    {
                                        PlaySound(1);
                                    }
                                    PopulateActivityTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, false, eventDesc);
                                    PopulateHistoryTable(tagEvent.tag.id, tagEvent.tag.tagType, parkNum, eventDesc);
                                    SaveSaniTraffic(tagEvent.tag.id.ToString(), first_name, last_name, dept, parkNum, loc, eventStatus, eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType), tagEvent.tag.sani);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Non-multitag Event Detected");
                            }
                        }//if valid tag
                        else	 //invalid tag
                        {
                            if (!IsSaniTagDuplicated(tagEvent.tag))
                            {
                                ListViewItem listItem = new ListViewItem("IN");
                                listItem.SubItems.Add(tagEvent.tag.id.ToString());   //tagid
                                listItem.SubItems.Add(string.Format("{0} {1}", first_name, last_name).Trim());         //tag description
                                listItem.SubItems.Add(loc);       //location
                                eventStatus = "Invalid";


                                if (accType == "Guest")
                                {
                                    listItem.SubItems.Add("Sani Tag Detected - Guest");   //event description
                                    eventDesc = "Sani Tag Detected - Guest";
                                }
                                else if (accType == "Temporary")
                                {
                                    listItem.SubItems.Add("Sani Tag Detected - Temporary");
                                    eventDesc = "Sani Tag Detected - Temporary";
                                }
                                else
                                {
                                    listItem.SubItems.Add("Sani Tag Detected");   //event description
                                    eventDesc = "Sani Tag Detected";
                                }

                                //listItem.SubItems.Add(DateTime.Now.ToString());           //time
                                //string cStr = tagEvent.tag.id.ToString();
                                //cStr += tagEvent.reader.ToString();
                                //cStr += tagEvent.fGenerator.ToString();
                                //if (!IsDupTagDisplay(listItem, 5, cStr))
                                {
                                    SaveSaniTraffic(tagEvent.tag.id.ToString(), first_name, last_name, dept, parkNum, loc, "Invalid", eventDesc, DateTime.Now, Convert.ToUInt32(tagEvent.tag.tagType), tagEvent.tag.sani);
                                }

                                PlaySound(1);
                            }
                            else
                            {
                                Console.WriteLine("Non-multitag Invalid Event Detected");
                            }
                        }//invalid tag

                    }//timerlock
                }//communication lock
        }//TagDetectedSani

        void SaveRSSI(AW_API_NET.rfTagEvent_t tagEvent)
		{
			if (m_connection == null)
				return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			lock (m_connection)
			{	    
				//string  SQL = "INSERT INTO rssi (ReaderID, TagID, RSSI, Timestamp) VALUES (?, ?, ?, ?)";
                string SQL = "INSERT INTO rssi (ReaderID, TagID, RSSI) VALUES (?, ?, ?)";
				  
				OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
				cmd.CommandText = SQL;
	            
				cmd.Parameters.Add(new OdbcParameter("", Convert.ToString(tagEvent.reader)));
                cmd.Parameters.Add(new OdbcParameter("", Convert.ToString(tagEvent.tag.id)));
				cmd.Parameters.Add(new OdbcParameter("", Convert.ToString(tagEvent.RSSI)));
				//cmd.Parameters.Add(new OdbcParameter("", DateTime.Now));

				try
				{
					cmd.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					ShowErrorMessage(ex.Message);
					return;
				}
			}
		}

        void SaveSaniTraffic(string tagid, string first_name, string last_name, string department, string zoneid, string location, string status, string events, DateTime time, uint type, rfTagSani_t sani)
        {
            if (m_connection == null)
                return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

            lock (m_connection)
            {
                string SQL;
                OdbcCommand cmd;

                SQL = "INSERT INTO sani (TagID, FirstName, LastName, ZoneID, Location, Status, SaniUnitType, SaniStatus, Event, Time, Type, Department) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                cmd = new OdbcCommand(SQL, m_connection);
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new OdbcParameter("", tagid));  //1
                if (first_name.Length == 0)
                    cmd.Parameters.Add(new OdbcParameter("", DBNull.Value));
                else
                    cmd.Parameters.Add(new OdbcParameter("", first_name));  //2
                if (last_name.Length == 0)
                    cmd.Parameters.Add(new OdbcParameter("", DBNull.Value));
                else
                    cmd.Parameters.Add(new OdbcParameter("", last_name));  //2
                if (zoneid.Length == 0)
                    zoneid = "0";
                cmd.Parameters.Add(new OdbcParameter("", Convert.ToInt32(zoneid)));  //3
                cmd.Parameters.Add(new OdbcParameter("", location));  //4
                cmd.Parameters.Add(new OdbcParameter("", status));    //5
                cmd.Parameters.Add(new OdbcParameter("", sani.UnitType.ToString()));    //5
                cmd.Parameters.Add(new OdbcParameter("", sani.Status.Description()));    //5
                cmd.Parameters.Add(new OdbcParameter("", events));    //6
                cmd.Parameters.Add(new OdbcParameter("", time));      //7
                cmd.Parameters.Add(new OdbcParameter("", Convert.ToInt32(type)));   //8
                if (department.Length == 0)
                    cmd.Parameters.Add(new OdbcParameter("", DBNull.Value));
                else
                    cmd.Parameters.Add(new OdbcParameter("", department));   //9
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch //(Exception ex)
                {
                    //ShowErrorMessage(ex.Message);
                    return;
                }
            }
        }

		void SaveTraffic(string tagid, string firstname, string lastname, string department, string zoneid, string location, string status, string events, DateTime time, uint type)
		{
			if (m_connection == null)
				return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			lock (m_connection)
			{	    
                string SQL = "INSERT INTO traffic (TagID, FirstName, LastName, ZoneID, Location, Status, Event, Time, Type, Department) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
				  
				OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
				cmd.CommandText = SQL;
                if (tagid == "")
                    cmd.Parameters.Add(new OdbcParameter("", DBNull.Value));  //1
                else
                    cmd.Parameters.Add(new OdbcParameter("", Convert.ToInt32(tagid)));  //1
                if (firstname.Length == 0)
                    cmd.Parameters.Add(new OdbcParameter("", DBNull.Value));
                else
				    cmd.Parameters.Add(new OdbcParameter("", firstname));  //2
                if ((lastname == null) || (lastname.Length == 0))
                    cmd.Parameters.Add(new OdbcParameter("", DBNull.Value));
                else
                    cmd.Parameters.Add(new OdbcParameter("", lastname));  //2
                if (zoneid.Length == 0)
                    zoneid = "0";
				cmd.Parameters.Add(new OdbcParameter("", Convert.ToInt32(zoneid)));  //3
				cmd.Parameters.Add(new OdbcParameter("", location));  //4
                cmd.Parameters.Add(new OdbcParameter("", status));    //5
                cmd.Parameters.Add(new OdbcParameter("", events));    //6
                cmd.Parameters.Add(new OdbcParameter("", time));      //7
                cmd.Parameters.Add(new OdbcParameter("", Convert.ToInt32(type)));   //8
                if (department.Length == 0)
                    cmd.Parameters.Add(new OdbcParameter("", DBNull.Value));
                else
				    cmd.Parameters.Add(new OdbcParameter("", department));   //9
				try
				{
					cmd.ExecuteNonQuery();
				}
				catch //(Exception ex)
				{
					//ShowErrorMessage(ex.Message);
					return;
				}
			}
		}

		void ProcessLineTag(AW_API_NET.rfTagEvent_t tagEvent)
		{
			//build query command and add to queue
			cmdStruct cmd = new cmdStruct();
			cmd.rdrID = tagEvent.reader;
			cmd.tagID = tagEvent.tag.id;
			if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
				cmd.tagType = "ACC";
			else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
				cmd.tagType = "AST";
			else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
				cmd.tagType = "INV";
			cmd.cmd = 0x02;  //query tag
			cmd.led = true;
			cmd.speaker = false;
			cmd.waitTime = DateTime.Now;
			cmd.timeStamp = DateTime.Now;
			cmd.retry = 0;
			short index = 0;
			if ((index = cmdCollection.IsEqual(cmd)) < 0)
			{
				cmdCollection.Add(cmd);
			}

		}

	  
		bool IsLineTagValid (uint ID)
		{
			if (m_connection == null)
				return (false);

			StringBuilder mySelectQuery = new StringBuilder();
			mySelectQuery.Append("SELECT LineID FROM zones WHERE LineID = ");          
			mySelectQuery.AppendFormat("'{0}'", ID);
			
			string mySelectStr = mySelectQuery.ToString();
					
			OdbcCommand myCommand = new OdbcCommand(mySelectStr, m_connection); 
			OdbcDataReader myReader = null; 
		    
			if (!IsConnected())
			{
				ShowErrorMessage("Connection lost");
				return false;
			}

			try	  //STOP 04 @@@@@@@@@@@@@@@@
			{
				myReader = myCommand.ExecuteReader(); 
			}
			catch //(Exception e)
			{
				//ShowErrorMessage("STOP 04  " + e.Message);
				if (myReader != null)
				{
					if (!myReader.IsClosed)
						myReader.Close();
				}
				return(false);
			}
 
			if (myReader.Read())
			{
				myReader.Close();
				return (true);
			}
			else
			{
				myReader.Close();
				return (false);
			}
		}

		bool IsTagValid(uint ID, byte type)
		{
			if (m_connection == null)
				return (false);

			lock (m_connection)
			{
				string tagID = "";

                if (type == AW_API_NET.APIConsts.ACCESS_TAG)
                    tagID = "ACC" + Convert.ToString(ID);
                else if (type == AW_API_NET.APIConsts.ASSET_TAG)
                    tagID = "AST" + Convert.ToString(ID);
                else if (type == AW_API_NET.APIConsts.INVENTORY_TAG)
                    tagID = "INV" + Convert.ToString(ID);
                else if (type == AW_API_NET.APIConsts.ALL_TAGS)
                    tagID = "ALL" + Convert.ToString(ID);
                else
                    return (false);

			//lock (m_connection)
			//{
				StringBuilder mySelectQuery = new StringBuilder();
				mySelectQuery.Append("SELECT TagID FROM tags WHERE TagID = ");          
				mySelectQuery.AppendFormat("'{0}'", tagID);
			
				string mySelectStr = mySelectQuery.ToString();
					
				OdbcCommand myCommand = new OdbcCommand(mySelectStr, m_connection); 
				OdbcDataReader myReader = null; 
		    
				if (!IsConnected())
				{
					ShowErrorMessage("Connection lost");
					return false;
				}

				try	  //STOP 04 @@@@@@@@@@@@@@@@
				{
					myReader = myCommand.ExecuteReader(); 
				}
				catch //(Exception e)
				{
					//ShowErrorMessage("STOP 04  " + e.Message);
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return(false);
				}
 
				if (myReader.Read())
				{
					myReader.Close();
					return (true);
				}
				else
				{
					myReader.Close();
					return (false);
				}
			}//lock
		}

        bool IsSaniTagDuplicated(rfTag_t tag)
        {
            if (tag.sani.EventCnt == 0)
                return true;

            if (tag.sani.UnitType == rfSaniUnitType_e.None)
                return true;

            lock (m_connection)
            {
                string SQL;
                OdbcCommand cmd;

                SQL = String.Format("SELECT * FROM sani_duplicates WHERE TagID = {0} AND SaniStatus = '{1}' AND EventCount = {2}", tag.id, tag.sani.Status.Description(), tag.sani.EventCnt);
                using (cmd = new OdbcCommand(SQL, m_connection))
                {
                    try
                    {
                        int found = cmd.ExecuteNonQuery();
                        if (found > 0)  // discard duplicate
                        {
                            return true;
                        }
                    }
                    catch
                    {
                    }
                }

                // Add tag information into auxiliary table
                SQL = String.Format("SELECT * FROM sani_duplicates WHERE TagID = {0}", tag.id);
                bool add = true;
                using (cmd = new OdbcCommand(SQL, m_connection))
                {
                    try
                    {
                        int found = cmd.ExecuteNonQuery();
                        if (found > 0)
                        {
                            add = false;
                        }
                    }
                    catch
                    {
                    }
                }

                if (add)
                    SQL = String.Format("INSERT INTO sani_duplicates SET TagID = {0}, SaniStatus = '{1}', EventCount = {2}", tag.id, tag.sani.Status.Description(), tag.sani.EventCnt);
                else
                    SQL = String.Format("UPDATE sani_duplicates SET SaniStatus = '{1}', EventCount = {2} WHERE TagID = {0}", tag.id, tag.sani.Status.Description(), tag.sani.EventCnt);
                using (cmd = new OdbcCommand(SQL, m_connection))
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                    }
                }
            }

            return false;
        }

		bool IsTagDuplicated(uint ID, byte type)
		{
			if (m_connection == null)
				return (true);

			lock (m_connection)
			{
				string tagID = "";

				if (type == AW_API_NET.APIConsts.ACCESS_TAG)
					tagID = "ACC" + Convert.ToString(ID);			
				else if (type == AW_API_NET.APIConsts.ASSET_TAG)
					tagID = "AST" + Convert.ToString(ID);
				else if (type == AW_API_NET.APIConsts.INVENTORY_TAG)
					tagID = "INV" + Convert.ToString(ID);
				else
					return (false);

				StringBuilder mySelectQuery = new StringBuilder();
				mySelectQuery.Append("SELECT TagID FROM tagactivity WHERE TagID = ");          
				mySelectQuery.AppendFormat("'{0}'", tagID);
			
				string mySelectStr = mySelectQuery.ToString();
					
				OdbcCommand myCommand = new OdbcCommand(mySelectStr, m_connection); 
				OdbcDataReader myReader = null;
				try
				{
					myReader = myCommand.ExecuteReader();
					reconnectCounter = -1;
					//\\timer3.Enabled = false;
				}
				catch (Exception ex)
				{
					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{   
						//error code 2013
						if (reconnectCounter < 0)
						{
							reconnectCounter = 0;
						}
					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return false;
				}//catch .. try
				//}//lock
 
				if (myReader.Read())
				{
					myReader.Close();
					return (true);
				}
				else
				{
					myReader.Close();
					return (false);
				}
			}//lock m_connection
		}
		public int CloseSerialPort()
		{
			int ret = communication.CloseSerialPort();
			rs232Comm = false;
            this.CommStatusBarPanel.Text = "RS232 Reader: Offline";
            if (m_updateCommStatus != null)
				m_updateCommStatus("Disconnected", false);

			try
			{
                MainStatusBar.Panels[0].Icon = Resources.ReaderDisconnected;
			}
			catch
			{
				MainStatusBar.Panels[0].Icon = null;
			}
			PlaySound(1);

			return (ret);
		}

		public int OpenSerialPort(uint baud, uint port)
		{
			int ret = communication.OpenSerialPort(baud, port);
			if (ret >= 0)
			{
				baudrate = baud;
				commPort = port;
                this.CommStatusBarPanel.Text = "RS232 Reader: Offline";
                if (m_updateCommStatus != null)
					m_updateCommStatus("Connected", true);
			}
			else
			{
                this.CommStatusBarPanel.Text = "RS232 Reader: Offline";
                if (m_updateCommStatus != null)
					m_updateCommStatus(this.CommStatusBarPanel.Text, false);
			}
            
			return ret;
		}

		public void ScanNetwork(byte[] ip)
		{
			int ret = communication.ScanNetwork(ip);
		}

		public void SocketConnection()
		{
			int ret = communication.SocketConnection();
		}

		public void SocketConnection(byte[] ip)
		{
			int ret = communication.SocketConnection(ip);
		}

		public void SocketDisconnection(byte[] ip)
		{
			int ret = communication.SocketDisconnection(ip);  //-160
			if ((m_updateIPListView != null) && (ret == -160))
			{
				int param = 0;
				m_updateIPListView(GetStringIP(ip), param, param, "Inactive", "Offline", eventType.closeSocket);
			}
		}

		string GetZoneID(int rdr, int fgen)
		{
			if (m_connection == null)
				return ("");

			lock (m_connection)  //dec-06-06
			{
				StringBuilder mySelectQuery = new StringBuilder();
				mySelectQuery.Append("SELECT ID FROM zones WHERE ReaderID = ");          
				mySelectQuery.AppendFormat("{0}", rdr);
				mySelectQuery.Append(" AND FieldGenID = ");
				mySelectQuery.AppendFormat("{0}", fgen);
				string mySelectStr = mySelectQuery.ToString();
						
				OdbcCommand myCommand = new OdbcCommand(mySelectStr, m_connection);
				OdbcDataReader myReader = null;

				try
				{
					myReader = myCommand.ExecuteReader();
					reconnectCounter = -1;
				}
				catch (Exception ex)
				{
					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{   
						//error code 2013
						if (reconnectCounter < 0)
						{
							reconnectCounter = 0;
						}

					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return "0";
				}//catch .. try

				string zoneID = ""; 
				if (myReader.Read())
					zoneID = myReader.GetString(0); 
				myReader.Close();
				return (zoneID);
			}//lock m_connection
		}

		public int TagEvent(AW_API_NET.rfTagEvent_t tagEvent)
		{
            //This function handles the tagEvent.

			int item = 0;

			string str = Convert.ToString(item);
			str += "- "	;

			if (tagEvent.errorStatus != AW_API_NET.APIConsts.RF_E_NO_ERROR)
			{
				
				return (0);
			}

			switch (tagEvent.eventType)
			{
				case AW_API_NET.APIConsts.RF_TAG_DETECTED:
				case AW_API_NET.APIConsts.RF_TAG_DETECTED_RSSI:						
					break;

			}

			return (0);
		}

		void PowerupReaderNotifty (AW_API_NET.rfReaderEvent_t readerEvent)
		{
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			if (m_connection == null)
				return;

			lock (m_connection)   //added on Dec 06, 06
			{
					string str = GetStringIP(readerEvent.ip);
				    if (str.Length == 0)
						return;

				int ret = communication.ConfigInputPort(readerEvent.reader, readerEvent.host, (ushort)AW_API_NET.APIConsts.REPORT_INPUT_CHANGE, (ushort)AW_API_NET.APIConsts.REPORT_INPUT_CHANGE, false);
				//There is a routine in timer1 that resets the ip string every 6 seconds
				if (powerup.ip == str)
				{
					Console.WriteLine("REJECTED powerup ip equals " + str + "  " + DateTime.Now.ToString());
					return;
				}
				else
				{
					powerup.ip = str;
					powerup.timestamp = DateTime.Now;
					Console.WriteLine("ACCEPTED powerup ip equals " + str + "  " + DateTime.Now.ToString());
				}

					for (int i=0; i<100; i++)
					{
						if (readerPowerup[i].ip == str)
						{
							readerPowerup[i].online = true; 
							break;
						}
					}
               
					StringBuilder sql = new StringBuilder();
					sql.Append("UPDATE zones SET Status = ");
					sql.AppendFormat("('{0}')", "Online");
					sql.Append(" WHERE ReaderID = ");         
					sql.AppendFormat("'{0}'", readerEvent.reader);

					OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
	
		    
					try
					{
						myCommand.ExecuteNonQuery();
					}
					catch //(Exception ex)
					{
						//ShowErrorMessage(ex.Message);  //Do not send to app retutn will catch it next time
						return;
					}

					sql.Length = 0;
					sql.Append("UPDATE netip SET RdrStatus = ");
					sql.AppendFormat("'{0}'", "Online");
					sql.Append(", ReaderID = ");
					sql.AppendFormat("'{0}'", readerEvent.reader);
					sql.Append(", HostID = ");
					sql.AppendFormat("'{0}'", readerEvent.host);
					sql.Append(", ConnectTime = ");
					sql.AppendFormat("'{0}'", DateTime.Now);
					sql.Append(", ZoneID = ");
					try
					{
                        if (GetZoneID(readerEvent.reader, 0) != "")
						   sql.AppendFormat("'{0}'", Convert.ToUInt32(GetZoneID(readerEvent.reader, 0)));
                        else
                           sql.AppendFormat("'{0}'", "0");
					}
					catch //(Exception ex)
					{
                        sql.AppendFormat("'{0}'", "0");
					}
					sql.Append(" WHERE IPAddress = ");         
					sql.AppendFormat("'{0}'", str);
					OdbcCommand myCommand2 = new OdbcCommand(sql.ToString(), m_connection);
	
					try
					{
						myCommand2.ExecuteNonQuery();
					}
					catch //(Exception ex)
					{
						//ShowErrorMessage(ex.Message);   //Do not send to app return will catch it next time
						return;
					}
				    
				

				if (m_updateIPListView != null)
				{
					m_updateIPListView(str, readerEvent.reader, readerEvent.host, "Active", "Online", eventType.powerup);
				}
						//item += 1;
					//}
					//}
					
					readerID = readerEvent.reader;  //this should work for RS232
                    if (rs232Reader == readerID)
                    {
                        rs232Comm = true;

                        try
                        {
                            CommStatusBarPanel.Text = "RS232 Reader: Online";
                            CommStatusBarPanel.Icon = Resources.ReaderConnected;

                        }
                        catch //(Exception ex)
                        {
                            CommStatusBarPanel.Icon = null;
                        }

                    }

                    if (m_updateCommStatus != null)
						m_updateCommStatus("Connected,  Reader: Online", true);

                    if (awiHistoryControl1 != null)
                        awiHistoryControl1.UpdateZoneViewPage("Online", DateTime.Now, readerEvent.reader, 0);

                    SetReaderStatusBar();

					//Commented out for debugging purpose put back later
					//try
					//{
                    //    MainStatusBar.Panels[0].Icon = Resources.ReaderConnected;
                    //    CommStatusBarPanel.Icon = Resources.ReaderConnected;
					//}
					//catch
					//{
						//MainStatusBar.Panels[0].Icon = null;
                        //CommStatusBarPanel.Icon = null;
					//}

					PlaySound(3);

				//}//powerupLocker
			}//lock m_connection
		}

		void UpdateZoneRdrStat(AW_API_NET.rfReaderEvent_t readerEvent, string rdrStat)
		{
            //This function update zone reader status
			if (m_connection == null)
				return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			lock (m_connection)
			{
				StringBuilder sql = new StringBuilder();
				sql.Append("UPDATE zones SET Status = ");
				sql.AppendFormat("('{0}')", rdrStat);
				sql.Append(" WHERE ReaderID = ");         
				sql.AppendFormat("'{0}'", readerEvent.reader);
				//sql.Append(" AND FieldGenID = ");
				//sql.AppendFormat("'{0}' ", readerEvent.fGenerator);

				OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
	
				
				try
				{
					myCommand.ExecuteNonQuery();
				}
				catch //(Exception ex)
				{
					//ShowErrorMessage(ex.Message);  //Do not send to app retutn will catch it next time
					return;
				}
				
			}//lock m_connection
		}

		void UpdateZoneRdrStat(ushort reader, ushort fgen, string rdrStat)
		{
			if (m_connection == null)
				return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			lock (m_connection)
			{
				StringBuilder sql = new StringBuilder();
				sql.Append("UPDATE zones SET Status = ");
				sql.AppendFormat("('{0}')", rdrStat);
				sql.Append(" WHERE ReaderID = ");         
				sql.AppendFormat("'{0}'", reader);
				//sql.Append(" AND FieldGenID = ");
				//sql.AppendFormat("'{0}' ", fgen);

				OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
	
				
				try
				{
					myCommand.ExecuteNonQuery();
				}
				catch //(Exception ex)
				{
					//ShowErrorMessage(ex.Message);  //Do not send to app retutn will catch it next time
					return;
				}
				
			}//lock m_connection
		}

		void UpdateNetipStat(AW_API_NET.rfReaderEvent_t readerEvent, string netStat, string rdrStat)
		{
			if (m_connection == null)
				return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			lock (m_connection)
			{
				StringBuilder sql = new StringBuilder();
				
				string ip = GetStringIP(readerEvent.ip);
				
				sql.Length = 0;
				sql.Append("UPDATE netip SET RdrStatus = ");
				sql.AppendFormat("'{0}'", rdrStat);
				sql.Append(", NetworkStatus = ");
				sql.AppendFormat("'{0}'", netStat);
				sql.Append(", ReaderID = ");
				sql.AppendFormat("'{0}'", readerEvent.reader);
				sql.Append(", HostID = ");
				sql.AppendFormat("'{0}'", readerEvent.host);
				sql.Append(", ConnectTime = ");
				sql.AppendFormat("'{0}'", DateTime.Now);
				
				sql.Append(" WHERE IPAddress = ");         
				sql.AppendFormat("'{0}'", ip);
				OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
	
				
				try
				{
					myCommand.ExecuteNonQuery();
				}
				catch //(Exception ex)
				{
					//ShowErrorMessage(ex.Message);   //Do not send to app retutn will catch it next time
					return;
				}

                SetReaderStatusBar();

			}//lock m_connection	
		}

		void UpdateNetipStat(ushort rdr, ushort host, string netStat, string rdrStat)
		{
			if (m_connection == null)
				return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			lock (m_connection)
			{
				StringBuilder sql = new StringBuilder();
				
				string ip = GetStringIP(rdr);
				
				if (ip.Length == 0)
				   return;

				sql.Length = 0;
				sql.Append("UPDATE netip SET RdrStatus = ");
				sql.AppendFormat("'{0}'", rdrStat);
				sql.Append(", NetworkStatus = ");
				sql.AppendFormat("'{0}'", netStat);
				sql.Append(", ReaderID = ");
				sql.AppendFormat("'{0}'", rdr);
				sql.Append(", HostID = ");
				sql.AppendFormat("'{0}'", host);
				sql.Append(", ConnectTime = ");
				sql.AppendFormat("'{0}'", DateTime.Now);
				
				sql.Append(" WHERE IPAddress = ");         
				sql.AppendFormat("'{0}'", ip);
				OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
	
				
				try
				{
					myCommand.ExecuteNonQuery();
				}
				catch //(Exception ex)
				{
					//ShowErrorMessage(ex.Message);   //Do not send to app retutn will catch it next time
					return;
				}

                SetReaderStatusBar();

			}//lock m_connection	
		}

		void UpdateListviewRdrStat(AW_API_NET.rfReaderEvent_t readerEvent, string netStat, string rdrStat)
		{
            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			string ip = GetStringIP(readerEvent.ip);
			if (ip.Length > 0)
			{
				if (m_updateIPListView != null)
				{
					m_updateIPListView(ip, readerEvent.reader, readerEvent.host, netStat, rdrStat, eventType.readerOffline);
				}

				readerID = readerEvent.reader;  //this should work for RS232
				
				ListViewItem listItem = new ListViewItem(""); 
				listItem.SubItems.Add("Reader #"+readerID);   //tagid
				listItem.SubItems.Add("");   //tag description
				//listItem.SubItems.Add(GetZoneID(readerEvent.reader, 0));   //zoneID
				listItem.SubItems.Add(GetLocation(readerEvent.reader, 0));   //location
				listItem.SubItems.Add(rdrStat);   //status
				listItem.SubItems.Add("Reader ID " + readerID + "  " + rdrStat);   //event description
				listItem.SubItems.Add(DateTime.Now.ToString());    //time
				string emptyStr = "";
				if (!IsDupTagDisplay(listItem, 5, emptyStr))  
				{
					listView.Items.Add(listItem);
                    listView.Items[listView.Items.Count-1].EnsureVisible();
					listView.Items[listView.Items.Count-1].ForeColor = System.Drawing.Color.Red;
				}
				PlaySound(2);

				SetReaderStatusBar();
				
				awiHistoryControl1.UpdateZoneViewPage();
				//void SaveTraffic(string tagid, string name, string department, string zoneid, string location, string status, string events, DateTime time, uint type)
				SaveTraffic("Reader #"+readerID, "", "", "", GetZoneID(readerEvent.reader, 0), GetLocation(readerEvent.reader, 0), rdrStat, "Reader ID " + readerID + "  " + rdrStat, DateTime.Now, rdrsStatus);
			}
		}

        uint SetReaderStatusBar()
        {
            if (m_connection == null)
                return 0;

            if (this.InvokeRequired)
            {
                uint ret = 0;
                this.Invoke(new Action(() => ret = SetReaderStatusBar()));
                return ret;
            }

            uint stat = 0;
            rdrsStatus = 0;

            ushort online = 0;
            ushort offline = 0;
            stat = AreAllNetRdrsOffline(out online, out offline);
            NetCommStatusBarPanel.Text = "Network Reader(s): Offline ( " + Convert.ToString(offline) + " )   Online ( " + Convert.ToString(online) + " )";

            if (stat == 99) //mix
            {
                rdrsStatus = 99;
                RdrOffline = true;
                if (m_updateCommStatus != null)
                    m_updateCommStatus("Disconnected", false);

                try
                {
                    NetCommStatusBarPanel.Icon = Resources.ReaderDisconnected;
                }
                catch
                {
                    NetCommStatusBarPanel.Icon = null;
                }

                if (!this.Disposing)
                    this.Invoke(new UpdateMainStatusBarCallback(this.UpdateMainStatusBarText), new object[] { "System", "System Not Ready!", Resources.DbDisconnected });
            }
            else if (stat == 98) //all offline
            {
                rdrsStatus = 98;
                RdrOffline = false;
                try
                {
                    NetCommStatusBarPanel.Icon = Resources.ReaderDisconnected;

                }
                catch //(Exception ex)
                {
                    NetCommStatusBarPanel.Icon = null;
                }

                if (m_updateCommStatus != null)
                    m_updateCommStatus("Disconnected", false);

                try
                {
                    CommStatusBarPanel.Icon = Resources.ReaderDisconnected;
                }
                catch
                {
                    CommStatusBarPanel.Icon = null;
                }

                if (!this.Disposing)
                    this.Invoke(new UpdateMainStatusBarCallback(this.UpdateMainStatusBarText), new object[] { "System", "System Not Ready", Resources.DbDisconnected });
            }
            else if (stat == 97) //all online
            {
                rdrsStatus = 97;
                RdrOffline = false;
                if (m_updateCommStatus != null)
                    m_updateCommStatus("Connected", false);

                try
                {
                    NetCommStatusBarPanel.Icon = Resources.ReaderConnected;
                }
                catch
                {
                    NetCommStatusBarPanel.Icon = null;
                }

                if (!closingApp)
                    if (!this.Disposing)
                        this.Invoke(new UpdateMainStatusBarCallback(this.UpdateMainStatusBarText), new object[] { "System", "System All Ready", Resources.Checked });
            }

            if (loadMsg != null)
            {
                if (m_closeMsgBoxEvent != null)
                    m_closeMsgBoxEvent();
            }

            return stat;
        }

        uint AreAllNetRdrsOffline(out ushort online, out ushort offline)
        {
            online = 0;
            offline = 0;

            bool allOnline = true;
            bool allOffline = true;


            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand("SELECT zones.Status FROM zones, netip WHERE netip.ZoneID = zones.ID AND zones.ReaderType = '0'", con))
            {
                con.Open();

                OdbcDataReader myReader = null;

                try
                {
                    myReader = cmd.ExecuteReader();
                    reconnectCounter = -1;
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
                        if (reconnectCounter < 0)
                        {
                            reconnectCounter = 0;
                            //\\timer3.Enabled = true;
                        }
                    }
                    if (myReader != null)
                    {
                        if (!myReader.IsClosed)
                            myReader.Close();
                    }
                    return 0;
                }//catch .. try

                string statStr = "";

                while (myReader.Read())
                {
                    statStr = myReader.GetString(0);
                    if (statStr == "Online")
                    {
                        allOffline = false;
                        online += 1;
                    }
                    else if (statStr == "Offline")
                    {
                        allOnline = false;
                        offline += 1;
                    }
                }

                myReader.Close();
                //}//lock
                if (!allOnline && !allOffline) //all not on or off - mix
                    return (99);  //mix
                else if (allOffline)
                    return (98);  //all offline
                else
                    return (97);  //all online
            }
        }

	   //This is not called by any module.
	   void UpdateNetipListview(AW_API_NET.rfReaderEvent_t readerEvent, string netStat, string rdrStat)
	   {
           //This function update network list view
            if (m_connection == null)
				return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			lock (m_connection)
			{
				StringBuilder sql = new StringBuilder();
				sql.Append("UPDATE zones SET Status = ");
				sql.AppendFormat("('{0}')", rdrStat);
				sql.Append(" WHERE ReaderID = ");         
				sql.AppendFormat("'{0}'", readerEvent.reader);
				sql.Append(" AND FieldGenID = ");
				sql.AppendFormat("'{0}' ", readerEvent.fGenerator);

				OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
	
		    
					try
					{
						myCommand.ExecuteNonQuery();
					}
					catch //(Exception ex)
					{
						//ShowErrorMessage(ex.Message);  //Do not send to app retutn will catch it next time
						return;
					}

				string ip = GetStringIP(readerEvent.ip);
				sql.Length = 0;
				sql.Append("UPDATE netip SET RdrStatus = ");
				sql.AppendFormat("'{0}'", rdrStat);
				sql.Append(", NetworkStatus = ");
				sql.AppendFormat("'{0}'", netStat);
				sql.Append(", ReaderID = ");
				sql.AppendFormat("'{0}'", readerEvent.reader);
				sql.Append(", HostID = ");
				sql.AppendFormat("'{0}'", readerEvent.host);
				sql.Append(", ConnectTime = ");
				sql.AppendFormat("'{0}'", DateTime.Now);
				sql.Append(" WHERE IPAddress = ");         
				sql.AppendFormat("'{0}'", ip);
				OdbcCommand myCommand2 = new OdbcCommand(sql.ToString(), m_connection);
	
					try
					{
						myCommand2.ExecuteNonQuery();
					}
					catch //(Exception ex)
					{
						//ShowErrorMessage(ex.Message);   //Do not send to app retutn will catch it next time
						return;
					}

				if (ip.Length > 0)
				{
					if (m_updateIPListView != null)
					{
						m_updateIPListView(ip, readerEvent.reader, readerEvent.host, netStat, rdrStat, eventType.readerOffline);
					}
					//item += 1;
				}
					
				readerID = readerEvent.reader;  //this should work for RS232

				ListViewItem listItem = new ListViewItem(""); 
				listItem.SubItems.Add("Reader #"+readerID);   //tagid
				listItem.SubItems.Add("");   //tag description
				listItem.SubItems.Add(GetZoneID(readerEvent.reader, 0));   //zoneID
				listItem.SubItems.Add(GetLocation(readerEvent.reader, 0));   //location
				listItem.SubItems.Add(rdrStat);   //status
				listItem.SubItems.Add("Reader ID " + readerID + "  " + rdrStat);   //event description
				listItem.SubItems.Add(DateTime.Now.ToString());    //time
                lvi[virtualListIndex++] = listItem;

			   
				awiHistoryControl1.UpdateZoneViewPage();
			   
				//Commented out for debugging purpose put back later
				try
				{
                    //MainStatusBar.Panels[0].Icon = Resources.ReaderConnected; 
                    CommStatusBarPanel.Icon = Resources.ReaderConnected;
				}
				catch
				{
                    //MainStatusBar.Panels[0].Icon = null; 
                    CommStatusBarPanel.Icon = null;
				}

				PlaySound(2);

			}//lock m_connection
		}

		#region GetZoneInfo (int reader, int fgen, out string loc, out string status, out short threshold)
		//returns park slot num, loc = location, status = status
       string GetZoneInfo(int reader, int fgen, out string loc, out string status, out short threshold)
       {
           //string t;
           string str = "";
           status = "";
           StringBuilder mySelectQuery = new StringBuilder();
           mySelectQuery.Append("SELECT ID, Location, RSSI, Threshold FROM zones WHERE ReaderID = ");
           mySelectQuery.AppendFormat("'{0}'", reader);
           mySelectQuery.Append(" AND FieldGenID = ");
           mySelectQuery.AppendFormat("'{0}'", fgen);

           using (var con = new OdbcConnection(ConnString))
           using (var cmd = new OdbcCommand(mySelectQuery.ToString(), con))
           {
               try
               {
                   con.Open();
                   using (var myReader = cmd.ExecuteReader())
                   {
                       reconnectCounter = -1;

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
                       }
                       else
                       {
                           loc = "Not Defined";
                           status = "";
                           threshold = 0;
                       }
                       return (str);
                   }
               }
               catch (Exception ex)
               {
                   int ret = 0, ret1 = 0, ret2 = 0;
                   if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                       ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                       ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                   {
                       //error code 2013
                       if (reconnectCounter < 0)
                       {
                           reconnectCounter = 0;
                           //\\timer3.Enabled = true;
                       }
                   }
               }//catch .. try
           }

           loc = "";
           status = "";
           threshold = 0;
           return "";
       }
		#endregion

	    #region GetParkInfo (int reader, int fgen, out string loc, out string status)
	   //returns park slot num, loc = location, status = status
       string GetParkInfo(int reader, int fgen, out string loc, out string status)
       {
           lock (GetParkInfoLock)
           {
               string str = "";
               status = "";
               StringBuilder mySelectQuery = new StringBuilder();
               mySelectQuery.Append("SELECT ID, Location FROM zones WHERE ReaderID = ");
               mySelectQuery.AppendFormat("'{0}'", reader);
               mySelectQuery.Append(" AND FieldGenID = ");
               mySelectQuery.AppendFormat("'{0}'", fgen);

               using (var con = new OdbcConnection(ConnString))
               using (var cmd = new OdbcCommand(mySelectQuery.ToString(), con))
               {
                   try
                   {
                       con.Open();
                       using (var myReader = cmd.ExecuteReader())
                       {
                           reconnectCounter = -1;
                           if (myReader.Read())
                           {
                               str = myReader.GetString(0);  //zoneID
                               try
                               {
                                   loc = myReader.GetString(1);
                               }
                               catch
                               {
                                   loc = "";
                               }
                               return (str);
                           }
                           else
                           {
                               loc = "Not Defined";
                               status = "";
                           }

                           return str;
                       }
                   }
                   catch (Exception ex)
                   {
                       int ret = 0, ret1 = 0, ret2 = 0;
                       if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                           ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                           ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                       {
                           //error code 2013
                           if (reconnectCounter < 0)
                           {
                               reconnectCounter = 0;
                               //\\timer3.Enabled = true;
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
							 
                               DBStatusBarPanel.Text = "DB Server : MySQL               Database : Disconnected";
                               loc = "";
                               status = "";
                               return "";
                           }*/
                       }
                   }//catch .. try
                   //}//lock 

                   /*lock (m_connection)
                   {
                       try
                       {
                           myReader = myCommand.ExecuteReader(); 
                       }
                       catch (Exception ex)
                       {
                           //ShowErrorMessage(ex.Message);
                           loc = "";
                           status = "";
                           if (myReader != null)
                           {
                               if (!myReader.IsClosed)
                                   myReader.Close();
                           }
                           return "";
                       }
                   }*/

                   //lock (m_connection)
                   //{
               }//lock
           }

           loc = "";
           status = "";
           return "";
       }
	  #endregion

	    #region GetParkInfo (uint lineID, out string loc, out string status)
       string GetParkInfo(uint lineID, out string loc, out string status)
       {
           string str = "";
           StringBuilder mySelectQuery = new StringBuilder();
           mySelectQuery.Append("SELECT ID, Location, SlotStatus FROM zones WHERE LineID = ");
           mySelectQuery.AppendFormat("'{0}'", lineID);

           using (var con = new OdbcConnection(ConnString))
           using (var cmd = new OdbcCommand(mySelectQuery.ToString(), con))
           {
               try
               {
                   con.Open();
                   using (var myReader = cmd.ExecuteReader())
                   {
                       reconnectCounter = -1;

                       if (myReader.Read())
                       {
                           //NOTE NOTE make sure the order is correct (0), (1), (2), ...
                           //otherwise exception will happen myReader.Close() will not e called.
                           str = myReader.GetString(0);
                           loc = myReader.GetString(1);
                           status = myReader.GetString(2);
                       }
                       else
                       {
                           loc = "Not Defined";
                           status = "";
                       }

                       return str;
                   }
               }

               /*lock (m_connection)
               {
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
                       loc = "";
                       status = "";
                       return("");
                   }
               }*/

               //lock (m_connection)
               //{
               catch (Exception ex)
               {
                   int ret = 0, ret1 = 0, ret2 = 0;
                   if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                       ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                       ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                   {
                       //error code 2013
                       if (reconnectCounter < 0)
                       {
                           reconnectCounter = 0;
                           //\\timer3.Enabled = true;
                       }

                       /*
                       Thread.Sleep(500);

                       if (!ReconnectToDBServer()) //this program should be done either in main or in the DBInterface.
                       {
                           MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                           if (myReader != null)
                           {
                               if (!myReader.IsClosed)
                                   myReader.Close();
                           }
							 
                           DBStatusBarPanel.Text = "DB Server : MySQL               Database : Disconnected";
                           loc = "";
                           status = "";
                           return "";
                       }*/
                   }
               }//catch .. try
               //}//lock

           }//lock m_connection

           loc = "";
           status = "";
           return "";
       }
	   #endregion

	    #region GetTagName()
       string GetTagName(uint tagID, byte type, out string note)
       {
           note = "";

           string str = "";

           StringBuilder mySelectQuery = new StringBuilder();
           if (type == 0x01) //acc
               mySelectQuery.Append("SELECT FirstName, LastName FROM employees WHERE ID = ");
           else if (type == 0x03)  //ast
               mySelectQuery.Append("SELECT Name, mobile FROM asset WHERE ID = ");
           else
               return "";
           mySelectQuery.AppendFormat("'{0}'", tagID);


           using (var con = new OdbcConnection(ConnString))
           using (var cmd = new OdbcCommand(mySelectQuery.ToString(), con))
           {
               try
               {
                   con.Open();
                   using (var myReader = cmd.ExecuteReader())
                   {
                       reconnectCounter = -1;

                       if (myReader.Read())
                       {
                           try
                           {
                               str = myReader.GetString(0);  //first name
                           }
                           catch
                           {
                               str = "";
                           }
                           str += " ";


                           if (type == 0x03)
                           {   //ast
                               try
                               {           // EDC March 2010
                                   if (myReader.GetBoolean(1))
                                       note = "Active Mobile";
                               }
                               catch
                               {
                                   note = "";
                               }
                           }
                           else
                           {
                               try
                               {
                                   str += myReader.GetString(1);  //last name
                               }
                               catch
                               {
                                   str += "";
                               }
                           }
                       }

                       return str;
                   }
               }

               /*lock (m_connection)
               {
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
                       return ("");
                   }
               }*/

               //lock (m_connection)
               //{
               catch (Exception ex)
               {
                   int ret = 0, ret1 = 0, ret2 = 0;
                   if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                       ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                       ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                   {
                       //error code 2013
                       if (reconnectCounter < 0)
                       {
                           reconnectCounter = 0;
                           //\\timer3.Enabled = true;
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
							 
                           DBStatusBarPanel.Text = "DB Server : MySQL               Database : Disconnected";
                           return "";
                       } */
                   }

               }//catch .. try
               //}//lock

           }//lock m_connection
           return "";
       }
		#endregion

       #region GetTagFirstAndLastName()
       void GetTagFirstAndLastName(uint tagID, byte type, out string first_name, out string last_name, out string note)
       {
           note = "";
           first_name = "";
           last_name = "";

           StringBuilder mySelectQuery = new StringBuilder();
           if (type == 0x01) //acc
               mySelectQuery.Append("SELECT FirstName, LastName FROM employees WHERE ID = ");
           else if (type == 0x03)  //ast
               mySelectQuery.Append("SELECT Name, mobile FROM asset WHERE ID = ");
           else
               return;
           mySelectQuery.AppendFormat("'{0}'", tagID);


           using (var con = new OdbcConnection(ConnString))
           using (var cmd = new OdbcCommand(mySelectQuery.ToString(), con))
           {
               try
               {
                   con.Open();
                   using (var myReader = cmd.ExecuteReader())
                   {
                       reconnectCounter = -1;

                       if (myReader.Read())
                       {
                           try
                           {
                               first_name = myReader.GetString(0);  //first name
                           }
                           catch
                           {
                           }


                           if (type == 0x03)
                           {   //ast
                               try
                               {           // EDC March 2010
                                   if (myReader.GetBoolean(1))
                                       note = "Active Mobile";
                               }
                               catch
                               {
                               }
                           }
                           else
                           {
                               try
                               {
                                   last_name = myReader.GetString(1);  //last name
                               }
                               catch
                               {
                               }
                           }
                       }

                       return;
                   }
               }

               /*lock (m_connection)
               {
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
                       return ("");
                   }
               }*/

               //lock (m_connection)
               //{
               catch (Exception ex)
               {
                   int ret = 0, ret1 = 0, ret2 = 0;
                   if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                       ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                       ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                   {
                       //error code 2013
                       if (reconnectCounter < 0)
                       {
                           reconnectCounter = 0;
                           //\\timer3.Enabled = true;
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
							 
                           DBStatusBarPanel.Text = "DB Server : MySQL               Database : Disconnected";
                           return "";
                       } */
                   }

               }//catch .. try
               //}//lock

           }//lock m_connection
           return;
       }
       #endregion

        string GetTagAccType(uint tagID)
       {
           string str = "";
           StringBuilder mySelectQuery = new StringBuilder();
           mySelectQuery.Append("SELECT accType FROM employees WHERE ID = ");
           mySelectQuery.AppendFormat("'{0}'", tagID);

           using (var con = new OdbcConnection(ConnString))
           using (var cmd = new OdbcCommand(mySelectQuery.ToString(), con))
           {
               try
               {
                   con.Open();
                   using (var myReader = cmd.ExecuteReader())
                   {
                       reconnectCounter = -1;

                       if (myReader.Read())
                           str = myReader.GetString(0);

                       if (str == "1")
                           str = "Employee";
                       else if (str == "2")
                           str = "Temporary";
                       else if (str == "3")
                           str = "Guest";
                       else
                           str = " ";

                       return str;
                   }
               }

                /*lock (m_connection)
                {
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
                        return ("");
                    }
                }*/

                //lock (m_connection)
               //{
               catch (Exception ex)
               {
                   int ret = 0, ret1 = 0, ret2 = 0;
                   if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                       ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                       ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                   {
                       //error code 2013
                       if (reconnectCounter < 0)
                       {
                           reconnectCounter = 0;
                           //\\timer3.Enabled = true;
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
							 
                           DBStatusBarPanel.Text = "DB Server : MySQL               Database : Disconnected";
                           return "";
                       }*/
                   }
               }//catch .. try
               //}//lock

           }//lock m_connection
           return "";
       }

	   //$CANDIDATE$ This function is a good candidate for being in the component
       public string GetLocation(int reader, int fgen)
       {
           string str = "Not defined";
           StringBuilder mySelectQuery = new StringBuilder();
           mySelectQuery.Append("SELECT Location FROM zones WHERE ReaderID = ");
           mySelectQuery.AppendFormat("'{0}'", reader);
           mySelectQuery.Append(" AND FieldGenID = ");
           mySelectQuery.AppendFormat("'{0}'", fgen);

           using (var con = new OdbcConnection(ConnString))
           using (var cmd = new OdbcCommand(mySelectQuery.ToString(), con))
           {
               try
               {
                   con.Open();
                   using (var myReader = cmd.ExecuteReader())
                   {
                       reconnectCounter = -1;

                       if (myReader.Read())
                           str = myReader.GetString(0);

                       return str;
                   }
               }

               /*lock (m_connection)
               {
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
                       return ("");
                   }
               }*/

               //lock (m_connection)
               //{
               catch (Exception ex)
               {
                   int ret = 0, ret1 = 0, ret2 = 0;
                   if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                       ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                       ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                   {
                       //error code 2013
                       if (reconnectCounter < 0)
                       {
                           reconnectCounter = 0;
                           //\\timer3.Enabled = true;
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
							 
                           DBStatusBarPanel.Text = "DB Server : MySQL               Database : Disconnected";
                           return "";
                       }*/
                   }
               }//catch .. try
               //}//lock


           }//m_connection
           return "";
       }

	   string GetLocation(string zone)
	   {
		  return null;
	   }

       void ErrorEventNotify(AW_API_NET.rfReaderEvent_t readerEvent)
       {
           //Console.WriteLine("ErrorEventNotify - Main Screen");

           //lock(errorLock)
           if (readerEvent.eventType == AW_API_NET.APIConsts.RF_READER_ENABLE)
           {
               Console.WriteLine("Reader# " + Convert.ToString(readerEvent.reader) + "  Offline");
               string ip = GetStringIP(readerEvent.ip);

               for (int i = 0; i < rdrIndexPoll; i++)
               {
                   if ((readerStatus[i].ip == ip) &&
                       (!readerStatus[i].displayed))
                   {
                       if (!readerStatus[i].displayed)
                       {
                           readerStatus[i].displayed = true;
                           readerStatus[i].rdrStatus = "Offline";
                           readerStatus[i].netStatus = "Inactive";
                           UpdateZoneRdrStat(readerEvent, "Offline");
                           UpdateNetipStat(readerEvent, "Inactive", "Offline");
                           UpdateListviewRdrStat(readerEvent, "Inactive", "Offline");
                       }
                       break;
                   }
               }
           }

           /*   if (communication.IsSocketConnected(ip))
              //statusChanged = true;
              //if (statusChanged)
              { 
                  //yes socket is connected using echo - disconnect the api socket - scan the socket(ip)
                  //connect socket(ip) - do enablereader

                  Console.WriteLine("Socket is connected");

              //////////OCT 16, 06 ///////////////////////
                  //Console.WriteLine("Socket is disconnected  " + ip);

                  //UpdateNetipListview(readerEvent, "Active", "Offline");

                  for (int i=0; i<rdrIndexPoll; i++)
                  {
                     if ((readerStatus[i].ip == ip) &&
                         (!readerStatus[i].displayed))              
                     {
						     
                        readerStatus[i].netStatus = "Active";
                        readerStatus[i].rdrStatus = "Offline";
                        readerStatus[i].counter = 0;

                        //UpdateNetipListview(readerEvent, "Active", "Offline");
                        UpdateZoneRdrStat(readerEvent, "Offline");
                        UpdateNetipStat(readerEvent, "Active", "Offline");
                        //UpdateListviewRdrStat(readerEvent, "Active", "Offline");

                        readerStatus[i].displayed = true;
                        break;
                     }
                 }
              ////////////////////////////////////////////

                  //today if (statusChanged)
                  //today {
                      //today timerStop = true;
                      //statusChanged = false;

                     //today  Console.WriteLine("Disconnected socket actions " + ip); //GetStringIP(disconIp));

                      //today reconnecting = true;
                      //today int r = communication.SocketDisconnection(readerEvent.ip);
						   
                      //Thread.Sleep(250);
                      //r = communication.ScanNetwork(disconIp);
                      //Thread.Sleep(250);
                      //r = communication.SocketConnection(disconIp);
                      //CmdSyncTimer.Enabled = false;
					   
                      //today for (int i=0; i<rdrIndexPoll; i++)
                      //today {
                          //today if (readerStatus[i].ip == ip)
                          //today {
                               //today readerStatus[i].netStatus = "Inactive";
                               //today readerStatus[i].rdrStatus = "Offline";
                               //today readerStatus[i].counter = 0;
                               //today break;
                          //today }
                      //today }

                      //today timerStop = false;
						   
                      //if (PopulateReaderStatusList())
                      //{
                          //////////rdrIndexPoll = 0;
                          //////////CmdSyncTimer.Interval = 5000;
                          //////////CmdSyncTimer.Enabled = true;
                          //timerStop = false;
                      //}
                  //today }//if status changed
              } //IsSocketConnected()
              else
              {
                  Console.WriteLine("Socket is disconnected  " + ip);

                  //UpdateNetipListview(readerEvent, "Inactive", "Offline");

                  for (int i=0; i<rdrIndexPoll; i++)
                  {
                     if ((readerStatus[i].ip == ip) &&
                         (!readerStatus[i].displayed))              
                     {
						     
                        readerStatus[i].netStatus = "Inactive";
                        readerStatus[i].rdrStatus = "Offline";
                        readerStatus[i].counter = 0;

                        //UpdateNetipListview(readerEvent, "Inactive", "Offline");
                        UpdateZoneRdrStat(readerEvent, "Offline");
                        UpdateNetipStat(readerEvent, "Inactive", "Offline");

                        readerStatus[i].displayed = true;
                        break;
                     }
                 }
                  //today disconIp = Encoding.ASCII.GetBytes(ip);
                  //today statusChanged = true;
              }
          }//errorLock	 
      }*/

           /*string str = "Event = ";
           str += readerEvent.eventType;
           str += "   error = ";
           str += readerEvent.errorStatus;
           str += "   status = ";
           str += readerEvent.eventStatus;
           str += "   pktID = ";
           str += readerEvent.pktID;*/

           //Console.WriteLine("ErrorEventNotify  - " + str);
           //this.CommStatusBarPanel.Text = "Communication : Connected        Reader : Online";
           //if (m_updateCommStatus != null)
           //m_updateCommStatus(this.CommStatusBarPanel.Text, true);
       }
		
	   void ResetReaderEventNotify (AW_API_NET.rfReaderEvent_t readerEvent)
	   {
		   //Console.WriteLine("ResetReaderEventNotify - Main Screen");
	   }

       void EnableReaderEventNotify(AW_API_NET.rfReaderEvent_t readerEvent)
       {
           //Console.WriteLine("Reader #" + Convert.ToString(readerEvent.reader) + "  is online");
           EnableReaderStatus(readerEvent);
           SetReaderStatusBar();

           /*string ip = GetStringIP(readerEvent.ip);
					   
            for (int i=0; i<rdrIndexPoll; i++)
            {
                if (readerStatus[i].ip == ip)
                {
                    Console.WriteLine("ENABLE_READER ACK Reader #" + Convert.ToString(readerEvent.reader) + "  " + readerStatus[i].rdrStatus);
                    if (readerStatus[i].rdrStatus == "Offline")
                    {
                        UpdateZoneRdrStat(readerEvent, "Online");
                        UpdateNetipStat(readerEvent, "Active", "Online");
                        awiHistoryControl1.UpdateZoneViewPage();
					    
                    }
 
                    //if (readerStatus[i].netStatus == "Active")
                    readerStatus[i].rdrStatus = "Online";
                    readerStatus[i].netStatus = "Active";
                    readerStatus[i].counter = 0;
                    break;
                }
            }*/
       }

       void InputChangeEventNotify(ushort host, ushort rdr, ushort fgen, short input1, short input2)
       {
           if ((mdActiveHi && ((input1 == AW_API_NET.APIConsts.NORMAL_CLOSED) || (input1 == AW_API_NET.APIConsts.FAULTY_CLOSED) || (input2 == AW_API_NET.APIConsts.NORMAL_CLOSED) || (input2 == AW_API_NET.APIConsts.FAULTY_CLOSED))) ||
               (!mdActiveHi && ((input1 == AW_API_NET.APIConsts.NORMAL_OPEN) || (input1 == AW_API_NET.APIConsts.FAULTY_OPEN) || (input2 == AW_API_NET.APIConsts.NORMAL_OPEN) || (input2 == AW_API_NET.APIConsts.FAULTY_OPEN))))
           {
               bool hasInput1 = mdActiveHi ?
                   (input1 == AW_API_NET.APIConsts.NORMAL_OPEN) ||
                   (input1 == AW_API_NET.APIConsts.FAULTY_OPEN) :
                   (input1 == AW_API_NET.APIConsts.NORMAL_CLOSED) ||
                   (input1 == AW_API_NET.APIConsts.FAULTY_CLOSED);

               bool hasInput2 = mdActiveHi ?
                   (input2 == AW_API_NET.APIConsts.NORMAL_OPEN) ||
                   (input2 == AW_API_NET.APIConsts.FAULTY_OPEN) :
                   (input2 == AW_API_NET.APIConsts.NORMAL_CLOSED) ||
                   (input2 == AW_API_NET.APIConsts.FAULTY_CLOSED);

               foreach (actionStruct eAction in actionList)
               {
                   if ((eAction.eType == INPUT_DETECTED) && (eAction.eRdr == rdr) && (eAction.eFGen == fgen))
                   {
                       if (hasInput1 && eAction.actions.Exists(item => item.actionType == EVENT_INPUT_01))
                       {
                           actionItemStruct action_input = eAction.actions.Find(item => (item.actionType == EVENT_INPUT_01));
                           actionItemStruct[] actions = eAction.actions.Where(item => (item.actionType != EVENT_INPUT_01) && (item.actionType != EVENT_INPUT_02)).ToArray();
                           ReaderInputRelayManager.Add(rdr, fgen, InputType.Input1, action_input.duration, eAction.Description, actions, 0, 0);
                       }
                       if (hasInput2 && eAction.actions.Exists(item => item.actionType == EVENT_INPUT_02))
                       {
                           actionItemStruct action_input = eAction.actions.Find(item => (item.actionType == EVENT_INPUT_02));
                           actionItemStruct[] actions = eAction.actions.Where(item => (item.actionType != EVENT_INPUT_01) && (item.actionType != EVENT_INPUT_02)).ToArray();
                           ReaderInputRelayManager.Add(rdr, fgen, InputType.Input2, action_input.duration, eAction.Description, actions, 0, 0);
                       }
                   }
               }
           }
       }

		void EnableReaderStatus(AW_API_NET.rfReaderEvent_t readerEvent)
		{
			string ip = GetStringIP(readerEvent.ip);
					   
			for (int i=0; i<rdrIndexPoll; i++)
			{
				if (readerStatus[i].ip == ip)
				{
					Console.WriteLine("ENABLE_READER ACK Reader #" + Convert.ToString(readerEvent.reader) + "  " + readerStatus[i].rdrStatus);
					if (readerStatus[i].rdrStatus == "Offline")
					{
						UpdateZoneRdrStat(readerEvent, "Online");
						UpdateNetipStat(readerEvent, "Active", "Online");
						awiHistoryControl1.UpdateZoneViewPage();
					    
					}
 
					//if (readerStatus[i].netStatus == "Active")
					readerStatus[i].rdrStatus = "Online";
					readerStatus[i].netStatus = "Active";
					readerStatus[i].displayed = false;
					readerStatus[i].counter = 0;
					break;
				}
			}
		}

		void EnableReaderStatus(ushort rdr, ushort host, ushort fgen)
		{
			string ip = GetStringIP(rdr);
					   
			for (int i=0; i<rdrIndexPoll; i++)
			{
				if (readerStatus[i].ip == ip)
				{
					
					if (readerStatus[i].rdrStatus == "Offline")
					{
						UpdateZoneRdrStat(rdr, fgen,"Online");
						UpdateNetipStat(rdr, host, "Active", "Online");
						awiHistoryControl1.UpdateZoneViewPage();
					    
					}
 
					//if (readerStatus[i].netStatus == "Active")
					readerStatus[i].rdrStatus = "Online";
					readerStatus[i].netStatus = "Active";
					readerStatus[i].displayed = false;
					readerStatus[i].counter = 0;
                    SetReaderStatusBar();
					break;
				}
			}
		}
        void ScanNetworkEventNotify(AW_API_NET.rfReaderEvent_t readerEvent)
        {
            OdbcConnection con;
            OdbcCommand cmd;

            //today if (reconnecting)
            //today {
            //reconnecting = false;
            //today Console.WriteLine("ScanNetworkEventNotify " + GetStringIP(readerEvent.ip));
            //today int r = communication.SocketConnection(readerEvent.ip);

            //today }

            string str = GetStringIP(readerEvent.ip);
            if (str.Length > 0)
            {
                string sql = string.Format("SELECT IPAddress FROM netip WHERE IPAddress = '{0}'", str);
                using (con = new OdbcConnection(ConnString))
                using (cmd = new OdbcCommand(sql, con))
                {
                    try
                    {
                        con.Open();
                        using (var myReader = cmd.ExecuteReader())
                        {
                            reconnectCounter = -1;

                            //Should check here if SACN_IP
                            if (myReader.HasRows)
                            {
                                ScanIPUpdateList(str);
                                SocketConnection(readerEvent.ip);
                                return;
                            }
                        }
                    }
                    //lock (m_connection)
                    //{
                    catch (Exception ex)
                    {
                        int ret = 0, ret1 = 0, ret2 = 0;
                        if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                            ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                            ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                        {
                            //error code 2013
                            if (reconnectCounter < 0)
                            {
                                reconnectCounter = 0;
                                //\\timer3.Enabled = true;
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
									 
                                DBStatusBarPanel.Text = "DB Server : MySQL               Database : Disconnected";
                                return;
                            }*/

                        }
                        return;
                    }//catch .. try
                    //}//lock


                    //Console.WriteLine("ScanNetworkEventNotify - Main Screen");

                    //OdbcCommand cmd = new OdbcCommand();
                    sql = "INSERT INTO netip(IPAddress, ReaderID, HostID, NetworkStatus, RdrStatus, ConnectTime) VALUES (?, ?, ?, ?, ?, ?)";

                    using (con = new OdbcConnection(ConnString))
                    using (cmd = new OdbcCommand(sql, con))
                    {
                        cmd.Parameters.Add(new OdbcParameter("", str));
                        cmd.Parameters.Add(new OdbcParameter("", "0"));
                        cmd.Parameters.Add(new OdbcParameter("", "0"));
                        cmd.Parameters.Add(new OdbcParameter("", "Inactive"));
                        cmd.Parameters.Add(new OdbcParameter("", "Offline"));
                        cmd.Parameters.Add(new OdbcParameter("", DateTime.Now));

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }


                                 //cmd.Parameters.Add("?IPAddress", str);
                        // cmd.Parameters.Add("?ReaderID", "");
                        //cmd.Parameters.Add("?HostID", "");
                        //cmd.Parameters.Add("?NetworkStatus", "Inactive");
                        // cmd.Parameters.Add("?RdrStatus", "Offline");
                        // cmd.Parameters.Add("?ConnectTime", "");

                                 //lock (m_connection)
                        //{
                        catch //(Exception ex)
                        {
                            //ShowErrorMessage(ex.Message);
                            //return;
                            ;
                        }
                        //}
                    }

                    if (m_updateIPListView != null)
                    {
                        m_updateIPListView(str, 0, 1, "Inactive", "Offline", eventType.scan);
                        //PlaySound(1);
                    }

                    SocketConnection(readerEvent.ip);

                    //if (CommDialog != null)
                    // {
                    //if (CommDialog.ConnectSock != null)
                    //CommDialog.ConnectSock();
                    //}

                }//lock m_connection

            }//m_connection != null
        }

        void ScanIPUpdateList(string ip)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE netip SET NetworkStatus = 'Inactive', RdrStatus = 'Offline'");
            sql.Append(" WHERE IPAddress = ");
            sql.AppendFormat("'{0}'", ip);

            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(sql.ToString(), con))
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch //(Exception ex)
                {
                    ;
                }


                if (m_updateIPListView != null)
                {
                    m_updateIPListView(ip, 0, 0, "Inactive", "Offline", eventType.closeSocket);
                    PlaySound(1);
                }
            }
        }


        void OpenSocketEventNotify(AW_API_NET.rfReaderEvent_t readerEvent)
        {
            //Console.WriteLine("OpenSocketEventNotify ");
            lock (myLock)
            {
                string str = GetStringIP(readerEvent.ip);

                for (int i = 0; i < rdrIndexPoll; i++)
                {
                    if (readerStatus[i].ip == str)
                    {
                        readerStatus[i].netStatus = "Active";
                        readerStatus[i].counter = 0;
                        break;
                    }
                }

                bool found = false;
                for (int i = 0; i < 100; i++)
                {
                    if (readerPowerup[i].ip == str)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        if (readerPowerup[i].ip == "")
                        {
                            readerPowerup[i].ip = str;
                            break;
                        }
                    }
                }

                Console.WriteLine("OpenSocketEventNotify " + str);
                statusChanged = false; //this is person place for this line
                reconnecting = false;  // same as above

                //if (m_connection != null)
                //{
                if (str.Length > 0)
                {
                    netConnection = true;
                    string SQL = string.Format("UPDATE netip Set NetworkStatus = 'Active' WHERE IPAddress = '{0}'", str);

                    socketsConnection = true;

                    using (var con = new OdbcConnection(ConnString))
                    using (var cmd = new OdbcCommand(SQL, con))
                    {
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine("MainForm - OpenSocketEventNotify.   " + ex.Message)
                                //ShowErrorMessage(ex.Message);
                            ; //return;
                        }
                    }
                    //}

                    //lock(myLock)
                    //{
                    if (str.Length > 0)
                    {
                        if (m_updateIPListView != null)
                        {
                            m_updateIPListView(str, 0, 0, "Active", "Offline", eventType.sockConnect);
                            //PlaySound(1);
                        }
                        //item += 1;
                    }

                    ResetSocketReader(readerEvent.ip);

                    //}
                }//mylock
            }//lock m_connection
        }

        void CloseSocketEventNotify(AW_API_NET.rfReaderEvent_t readerEvent)
        {
            OdbcConnection con;
            OdbcCommand cmd;

            /*if (m_connection != null)
            {
                OdbcCommand myCommand = new OdbcCommand("TRUNCATE TABLE netip", m_connection);
			
                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                    return;
                }

                lock(myLock)
                {
                    if (m_updateIPListView != null)
                    {
                        m_updateIPListView(null, 0, 0, "", "", eventType.closeSocket);
                    }
                }
            }*/

            //today if (reconnecting)
            //today {
            //today Console.WriteLine("CloseSocketEventNotify " + GetStringIP(readerEvent.ip));
            //today int r = communication.ScanNetwork(readerEvent.ip);
            //Console.WriteLine("CloseSocketEventNotify " + GetStringIP(readerEvent.ip));
            //today }

            Console.WriteLine("Reader ID For Closing Socket = " + Convert.ToUInt16(readerEvent.reader));

            //lock (disconnLock)
            //{
            //netConnection = true;
            string str = GetStringIP(readerEvent.ip);

            //string SQL = string.Format("UPDATE netip Set NetworkStatus = 'Inactive', RdrStatus = 'Offline'"); 

            //cmd.Connection = m_connection;
            //cmd.CommandText = SQL;
            //socketsConnection = true;
            ////////////////////////////////
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE netip SET NetworkStatus = 'Inactive', RdrStatus = 'Offline'");
            sql.Append(" WHERE IPAddress = ");
            sql.AppendFormat("'{0}'", str);

            //sql.Append(" WHERE ReaderID = ");         
            //sql.AppendFormat("'{0}'", readerEvent.reader);
            //sql.Append(" AND FGenID = ");         
            //sql.AppendFormat("'{0}'", readerEvent.fGenerator);
            using (con = new OdbcConnection(ConnString))
            using (cmd = new OdbcCommand(sql.ToString(), con))
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("close socket - failed to update netip table. " + ex.Message);
                    return;
                }
            }
            sql = new StringBuilder();
            sql.Append("UPDATE zones SET Status = ");
            sql.AppendFormat("('{0}')", "Offline");
            sql.Append(" WHERE ReaderID = ");
            sql.AppendFormat("'{0}'", readerEvent.reader);
            //sql1.Append(" AND FieldGenID = ");         
            //sql1.AppendFormat("'{0}'", readerEvent.fGenerator);

            using (con = new OdbcConnection(ConnString))
            using (cmd = new OdbcCommand(sql.ToString(), con))
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    ShowErrorMessage("close socket - failed to update zones table. " + ex.Message);
                    return;
                }
                //}
                //}
            }

            if (m_updateIPListView != null)
            {
                m_updateIPListView(str, readerEvent.reader, readerEvent.host, "Inactive", "Offline", eventType.closeSocket);
            }
            Console.WriteLine("Offline - rdr = " + readerEvent.reader);
            if (awiHistoryControl1 != null)
                awiHistoryControl1.UpdateZoneViewPage("Offline", DateTime.Now, readerEvent.reader, 0);
            SetReaderStatusBar();

            try
            {
                //Commented out for debugging put it back later
                //Console.WriteLine("XXX I am here 04");
                //MainStatusBar.Panels[0].Icon = Resources.ReaderDiscnnected;
                CommStatusBarPanel.Icon = Resources.ReaderDisconnected;
            }
            catch
            {
                //MainStatusBar.Panels[0].Icon = null;
                CommStatusBarPanel.Icon = null;
            }
            ///////////////////////////////////////////////////////

            //}//disconnLock

            //PlaySound(1);



            netConnection = false;
            this.CommStatusBarPanel.Text = "RS232 Reader: Offline";
            if (m_updateCommStatus != null)
                m_updateCommStatus(this.CommStatusBarPanel.Text, true);
        }

        public int ReaderEvent(AW_API_NET.rfReaderEvent_t readerEvent)
        {
            //person var
            int item = 0;

            string str = Convert.ToString(item);
            str += "- ";
            int ret = 0;

            if (readerEvent.errorStatus != AW_API_NET.APIConsts.RF_E_NO_ERROR)
            {
                str += "Event = ";
                str += readerEvent.eventType;
                str += "   error = ";
                str += readerEvent.errorStatus;
                str += "   status = ";
                str += readerEvent.eventStatus;
                str += "   pktID = ";
                str += readerEvent.pktID;

                //Console.WriteLine(str);
                //MsgListBox.Items.Add(str);
                item += 1;
                return (0);
            }

            switch (readerEvent.eventType)
            {
                case AW_API_NET.APIConsts.RF_READER_POWERUP:

                    //if (m_connection != null)
                    //{
                    StringBuilder sql = new StringBuilder();
                    sql.Append("UPDATE zones SET Status = ");
                    sql.AppendFormat("('{0}')", "Online");
                    sql.Append(" WHERE ReaderID = ");
                    sql.AppendFormat("'{0}'", readerEvent.reader);
                    sql.Append(" AND FieldGenID = ");
                    sql.AppendFormat("'{0}' ", readerEvent.fGenerator);

                    OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);

                    //lock (m_connection)
                    //{
                    try
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    catch //(Exception ex)
                    {
                        //ShowErrorMessage(ex.Message);
                        return (-1);
                    }
                    //}
                    //}

                    str += "Reader Powered Up.";
                    str += "   hostID = ";
                    str += readerEvent.host;
                    str += "   readerID = ";
                    str += readerEvent.reader;

                    if (ret != AW_API_NET.APIConsts.RF_S_DONE)
                    {
                        str += "   Error Code = ";
                        str += readerEvent.errorStatus;
                    }
                    else
                    {
                        str += "   was successful.";
                    }

                    item += 1;

                    //HostIDTextBox.Text = Convert.ToString(readerEvent.host, 10);
                    //ReaderIDTextBox.Text = Convert.ToString(readerEvent.reader, 10);
                    break;

                case AW_API_NET.APIConsts.RF_STD_FGEN_POWERUP:
                    str += "STD FGen Powered Up.";
                    str += "   hostID = ";
                    str += readerEvent.host;
                    str += "   FGenID = ";
                    str += readerEvent.fGenerator;
                    //SmartFGenTextBox.Text = Convert.ToString(readerEvent.fGenerator);

                    //MsgListBox.Items.Add(str);
                    item += 1;

                    //HostIDTextBox.Text = Convert.ToString(readerEvent.host, 10);
                    //ReaderIDTextBox.Text = Convert.ToString(readerEvent.reader, 10);
                    break;

                case AW_API_NET.APIConsts.RF_SMART_FGEN_POWERUP:
                    str += "Smart FGen Powered Up.";
                    str += "   hostID = ";
                    str += readerEvent.host;
                    str += "   FGenID = ";
                    str += readerEvent.smartFgen.ID;
                    str += "   was successful.";
                    //SmartFGenTextBox.Text = Convert.ToString(readerEvent.fGenerator);

                    //MsgListBox.Items.Add(str);
                    item += 1;

                    //HostIDTextBox.Text = Convert.ToString(readerEvent.host, 10);
                    //ReaderIDTextBox.Text = Convert.ToString(readerEvent.reader, 10);
                    break;

                case AW_API_NET.APIConsts.RF_END_OF_BROADCAST:
                    str += "End Of Broacast.   pktID = ";
                    str += readerEvent.pktID;
                    //MsgListBox.Items.Add(str);
                    item += 1;
                    break;

                case AW_API_NET.APIConsts.RF_READER_RESET:
                case AW_API_NET.APIConsts.RF_READER_RESET_ALL:
                    str += "rfResetReader()";
                    str += "   pktID = ";
                    str += readerEvent.pktID;
                    str += "   readerID = ";
                    str += readerEvent.reader;

                    if (readerEvent.eventStatus == AW_API_NET.APIConsts.RF_INVALID_PACKET)
                    {
                        str += "    Error Code = ";
                        str += readerEvent.errorStatus;
                    }
                    else
                    {
                        str += "   was successful.";
                    }

                    //MsgListBox.Items.Add(str);
                    item += 1;
                    break;

                case AW_API_NET.APIConsts.RF_RESET_SMART_FGEN:
                case AW_API_NET.APIConsts.RF_RESET_SMART_FGEN_ALL:
                    str += "rfResetSmartFGen()";
                    str += "   FGen ID = ";
                    str += readerEvent.smartFgen.ID;
                    str += "   ReaderID = ";
                    str += readerEvent.reader;
                    str += "   pktID = ";
                    str += readerEvent.pktID;
                    //MsgListBox.Items.Add(str);
                    item += 1;
                    break;

                case AW_API_NET.APIConsts.RF_SCAN_NETWORK:
                    /*str = GetStringIP(readerEvent.ip);
                    if (str.Length > 0)
                    {
                      MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
                      string SQL = "INSERT INTO netip VALUES(?IPAddress, ?ReaderID, ?HostID, ?NetworkStatus, ?RdrStatus, ?ConnectTime)";
						
                      cmd.Connection = m_connection;
                        cmd.CommandText = SQL;
                        cmd.Parameters.Add("?IPAddress", str);
                        cmd.Parameters.Add("?ReaderID", "");
                        cmd.Parameters.Add("?HostID", "");
                        cmd.Parameters.Add("?NetworkStatus", "Inactive");
                        cmd.Parameters.Add("?RdrStatus", "Offline");
                        cmd.Parameters.Add("?ConnectTime", "");
			
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            ShowErrorMessage(ex.Message);
                            //return;
                        }

                      if (m_updateIPListView != null)
                            m_updateIPListView(str, 0, 1, "Inactive", "Offline", eventType.scan);
                      item += 1;
                    }*/
                    break;

                case AW_API_NET.APIConsts.RF_OPEN_SOCKET:

                    ///////////
                    try
                    {
                        str = GetStringIP(readerEvent.ip);
                    }
                    catch
                    {
                        str = "";
                    }

                    //if (m_connection != null)
                    //{
                    if (str.Length > 0)
                    {
                        OdbcCommand cmd = new OdbcCommand();
                        string SQL = string.Format("UPDATE netip Set NetworkStatus = 'Active' WHERE IPAddress = '{0}'", str);

                        cmd.Connection = m_connection;
                        cmd.CommandText = SQL;

                        //lock (m_connection)
                        //{
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch //(Exception ex)
                        {
                            //ShowErrorMessage(ex.Message);
                            //return;
                            ;
                        }
                        //}
                    }
                    //}

                    //lock(myLock)
                    //{
                    if (str.Length > 0)
                    {
                        if (m_updateIPListView != null)
                        {
                            m_updateIPListView(str, 0, 0, "Active", "", eventType.sockConnect);
                        }
                        item += 1;
                    }
                    //}
                    break;

                case AW_API_NET.APIConsts.RF_CLOSE_SOCKET:
                    str += "rfCloseSocket()   pktID = ";
                    str += readerEvent.pktID;

                    if (readerEvent.ip[0] != 0x00)
                    {
                        str += "   IP = ";
                        //str += GetStringIP(readerEvent.ip);
                    }

                    if (readerEvent.eventStatus == AW_API_NET.APIConsts.RF_INVALID_PACKET)
                    {
                        str += "   Error Code = ";
                        str += readerEvent.errorStatus;
                    }
                    else
                    {
                        str += "   was successful.";
                    }
                    //MsgListBox.Items.Add(str);
                    item += 1;
                    break;

                /*case AW_API_NET.APIConsts.RF_RELAY_ENABLE:
                    Console.WriteLine("Relay Enable ACK");
                break;

                case AW_API_NET.APIConsts.RF_RELAY_DISABLE:
                   Console.WriteLine("Relay Disable ACK");
                break;*/

            }//switch

            return (0);
        }//ReaderEvent

        #region ResetSocketReader
        private int ResetSocketReader(byte[] ip)
        {
            if (pktID > 224)
                pktID = 1;
            int ret = communication.ResetReaderSocket(1, ip); //, ++pktID);
            return ret;
        }
        #endregion

		private string GetStringIP (byte[] ip)
		{
			int p = 0;
			string s = "";
			int ct = 0;
			try
			{
				while ((ct <= 3) && p < ip.ToString().Length &&(ip[p] != 0))
				{
					if (ip[p] != 46) 
						s += Convert.ToInt16(ip[p++]) - 48;
					else
					{ 
						ct++;
						p++;
						s += ".";
					}
				}
			}
			catch
			{
				s = "";
			}

			return s;
		}

		private string GetStringIP (ushort rdr)
		{
			string str = "";
			int rdrID = Convert.ToInt32(rdr);
			if (m_connection != null)
			{
				//str = GetStringIP(rdr);
				//if (str.Length > 0)
				{
					lock (m_connection)
					{
						string sql = string.Format("SELECT IPAddress FROM netip WHERE ReaderID = '{0}'", rdrID);
						OdbcCommand myCommand = new OdbcCommand(sql, m_connection);
						OdbcDataReader myReader = null;

						try
						{
							myReader = myCommand.ExecuteReader();
							reconnectCounter = -1;
							//\\timer3.Enabled = false;
						}
						catch (Exception ex)
						{
							int ret = 0, ret1 = 0, ret2 = 0;
							if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
								((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
								((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
							{   
								//error code 2013
								if (reconnectCounter < 0)
								{
									reconnectCounter = 0;
									//\\timer3.Enabled = true;
								}
							}
							if (myReader != null)
							{
								if (!myReader.IsClosed)
									myReader.Close();
							}
							return "";
						}//catch .. try
					
	 
					    //Should check here if SACN_IP
					    if (myReader.HasRows)
						{
							str = myReader.GetString(0);
							myReader.Close();
							return str;
						}

						myReader.Close();
					}//lock
				}
			}//m-connection

			return str;
		}

	   /*private bool Connect(string server, string port, string user, string pwd, string db)
       {
          string connectionString = string.Format("server={0};port={1};username={2};password={3};database={4};pooling=false", server, port, user, pwd, db);
          Close();
          m_connection = new MySqlConnection(connectionString);
		  try 
          {
              m_connection.Open();
			  DBStatusBarPanel.Text = "Database : Connected";
		     
          }
          catch (MySqlException e) 
          {
              MessageBox.Show(e.Message, "CarTracker Error Msg" );
			  DBStatusBarPanel.Text = "Database : Disconnected";
			  return false;
          }
 
		  return true;
       }
   
      public bool Connect(string connectionString)
      {
         CloseConnection();
         m_connection = new MySqlConnection(connectionString);
         //m_connection.Open();

	     try 
         {
              m_connection.Open();
         }
         catch (MySqlException e) 
         {
              MessageBox.Show(e.Message, "CarTracker Error Msg" );
			  //MsgLabel.Text = "Connection : DB connection failed";
			 return false;
         }

	     return true;
      }*/

      public void CloseConnection()
      {
          lock (m_connection)
          {
              if (m_connection != null)
              {
                  m_connection.Close();
                  //m_connection.Dispose();
                  //12-07-07 m_connection = null;
              }
          }
      }
      
	  public void OpenConnection(string s)
      {
         odbcDB.Connect(s);
      }

      public bool IsConnected()
      {
         return m_connection != null;
      }

	  public int API_ResetAllReaders()
	  {
	     /*if (pktID >= 224)
			pktID = 1;
	     else
		    pktID++;
		 int ret = api.rfResetReader(1, 2, 0, APINetClass.ALL_READERS, pktID); 
	     return (ret);*/
	     int ret = communication.ResetAllReaders();
		 return (ret);
	  }

	  private bool PopulateReaderStatusList()
	  {
		  if (m_connection == null)
			  return false;

          //changes made to support MYSQL Server v5.0 and later
          CultureInfo ci = new CultureInfo("sv-SE", true);
          System.Threading.Thread.CurrentThread.CurrentCulture = ci;
          ci.DateTimeFormat.DateSeparator = "-";

		  lock (m_connection)
		  {
			  for (int i=0; i<100; i++)
			  {
				  readerStatus[i].ip = "";
				  readerStatus[i].netStatus = "";
				  readerStatus[i].rdrStatus = "";
				  readerStatus[i].rdrID = 0;
				  readerStatus[i].hostID = 0;
				  readerStatus[i].counter = 0;
				  readerStatus[i].displayed = false;
			  }

			  string mySelectQuery = "SELECT * From netip";
			  OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection); 

			  OdbcDataReader myReader = null; 
			  /*try
			  {
				  myReader = myCommand.
				  ExecuteReader();
			  }
			  catch (Exception ex)
			  {
				  //ShowErrorMessage(ex.Message);
				  //m_connection.Close();
				  if (myReader != null)
				  {
					  if (!myReader.IsClosed)
						  myReader.Close();
				  }
				  return false;
			  }*/

			  //lock (m_connection)
			  //{
			  try
			  {
				  myReader = myCommand.ExecuteReader();
				  reconnectCounter = -1;
				  //\\timer3.Enabled = false;
			  }
			  catch (Exception ex)
			  {
				  int ret = 0, ret1 = 0, ret2 = 0;
				  if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
					  ((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
					  ((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
				  {   
					  //error code 2013
					  if (reconnectCounter < 0)
					  {
						  reconnectCounter = 0;
						  //\\timer3.Enabled = true;
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
							 
						  DBStatusBarPanel.Text = "DB Server : MySQL               Database : Disconnected";
						  return false;
					  }*/                              
				  }
				  if (myReader != null)
				  {
					  if (!myReader.IsClosed)
						  myReader.Close();
				  }
				  return false;
			  }//catch .. try
			  //}//lock
				
			  numRdrs = 0;
			  while (myReader.Read())
			  {
				  readerStatus[numRdrs].ip = myReader.GetString(0);        //ip
				  readerStatus[numRdrs].rdrID = myReader.GetInt32(1);      //rdr
				  readerStatus[numRdrs].hostID = myReader.GetInt32(2);      //host
				  readerStatus[numRdrs].netStatus = myReader.GetString(3);  //netstatus
				  readerStatus[numRdrs].rdrStatus = myReader.GetString(4);  //rdrstatus
				  //readerStatus[numRdrs].timeStamp = Convert.ToDateTime(myReader.GetString(5));  //timestamp
				  readerStatus[numRdrs].nextCmd = 1;                       //enablerdr
				  numRdrs += 1;
			  }
			  myReader.Close();

			  return true;
		  }//lock m_connection
	  }

	  private bool AreAllSocketsClosed()
	  {
		  if (m_connection == null)
			  return true;

		  lock (m_connection)
		  {
			  string mySelectQuery = "SELECT * From netip WHERE NetworkStatus = 'Active'";
			  OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection); 

			  OdbcDataReader myReader = null;
 
			  /*try
			  {
				  myReader = myCommand.ExecuteReader();
			  }
			  catch (Exception ex)
			  {
				  if (myReader != null)
				  {
					  if (!myReader.IsClosed)
						  myReader.Close();
				  }
				  return true;
			  }*/

			  //lock (m_connection)
			  //{
			  try
			  {
				  myReader = myCommand.ExecuteReader();
				  reconnectCounter = -1;
				  //\\timer3.Enabled = false;
			  }
			  catch (Exception ex)
			  {
				  int ret = 0, ret1 = 0, ret2 = 0;
				  if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
					  ((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
					  ((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
				  {   
					  //error code 2013
					  if (reconnectCounter < 0)
					  {
						  reconnectCounter = 0;
						  //\\timer3.Enabled = true;
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
							 
						  DBStatusBarPanel.Text = "DB Server : MySQL               Database : Disconnected";
						  return true;
					  }*/                              
				  }
				  if (myReader != null)
				  {
					  if (!myReader.IsClosed)
						  myReader.Close();
				  }
				  return true;
			  }//catch .. try
			  //}//lock
				
			  if (myReader.Read())
			  {
				  myReader.Close();
				  return false;
			  }
			  else
			  {
				  myReader.Close();
				  return true;
			  }
		  }//lock m_connection
		  
	  }

	  private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
	  {
          //This function handles the toolbar button click
		  if (e.Button.Text == "Comm")
		  {
			  CmdSyncTimer.Enabled = false;
			  CommDialog = new CommForm(this);
			  CommDialog.ScanNet += new Scan(this.ScanNetwork);
			  CommDialog.ConnectSock += new ConnectSocket(this.SocketConnection);
			  CommDialog.ConnectThisSock += new ConnectThisSocket(this.SocketConnection);
			  CommDialog.DisconnectSock += new DisconnectSocket(this.SocketDisconnection);
			  CommDialog.OpenPort += new OpenSerialPort(this.OpenSerialPort);
			  CommDialog.ClosePort += new CloseSerialPort(this.CloseSerialPort);
			  CommDialog.ResetSocketReaderEvent += new ResetSocketReader(this.ResetSocketReader);

			  //CommDialog.ClosePort += new CarTracker.CloseSerialPort(this.CloseSerialPort);
			  CommDialog.m_resetAllReaders += new ResetAllReaders(this.API_ResetAllReaders);
			 


			  CommDialogActivated = true;
			  if (CommDialog.ShowDialog(this) == DialogResult.OK)
			  {
				  
			  }
			  else
			  {
				  ;
			  }

			  

			  CommDialog = null;

			  CommDialogActivated = false;
			  if (PopulateReaderStatusList())
			  {
				  rdrIndexPoll = 0;
				  timerStop = false;
				  //CHG 11-20 CmdSyncTimer.Enabled = true;
			  }
			  else
			  {
                  //error message
			  }
			  //CommDialog.Dispose();
		  }
		  else if (e.Button.Text == "Configure")
		  {
            
			  ConfigForm configDlg = new ConfigForm(this);
			  if (configDlg.ShowDialog(this) == DialogResult.OK)
			  {
				  DateTime dt = DateTime.Today;
				  DateTime dt1 = DateTime.Today.AddDays(spanDays);
				  TimeSpan dt2 = dt1 - dt;
				  DateTime dt3 = DateTime.Today.Subtract(dt2);
				  lastTime = dt3;
				  listView.Items.Clear();
				  try
				  {
                      RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Active Wave\\Smart Tracker\\");
					  reg.SetValue("viewDays", MainForm.spanDays);
					  reg.SetValue("mdActiveHi", MainForm.mdActiveHi);
				  }
				  catch {;}
				  //12-26-07 PollTrafficTable();
			  }
			  else
			  {
				  ;
			  }
			  configDlg.Dispose();
		  }
		  else if (e.Button.Text == "Asset")
		  {
			  AssetForm assetDlg = new AssetForm(this);
			  if (assetDlg.ShowDialog(this) == DialogResult.OK)
			  {
				  ;
			  }
			  else
			  {
				  ;
			  }
			  assetDlg.Dispose();
		  }
          else if (e.Button.Text == "Search")
          {
              APIEventHandler.AppTagDetectedEventHandler -= new AppTagDetectedEvent(TagDetected);//+= new EnableOutputRelay(eventActions_EnableOutputRelayEvent);
              APIEventHandler.AppTagDetectedRSSIEventHandler -= new AppTagDetectedRSSIEvent(TagDetectedRSSI);
              APIEventHandler.AppTagDetectedSaniEventHandler -= new AppTagDetectedSaniEvent(TagDetectedSani);

              INVForm searchDlg = new INVForm(this);
              if (searchDlg.ShowDialog(this) == DialogResult.OK)
              {
                  ;
              }
              else
              {
                  ;
              }
              searchDlg.Dispose();

              APIEventHandler.AppTagDetectedEventHandler += new AppTagDetectedEvent(TagDetected);//+= new EnableOutputRelay(eventActions_EnableOutputRelayEvent);
              APIEventHandler.AppTagDetectedRSSIEventHandler += new AppTagDetectedRSSIEvent(TagDetectedRSSI);
              APIEventHandler.AppTagDetectedSaniEventHandler += new AppTagDetectedSaniEvent(TagDetectedSani);
          }
#if SANI
          else if (e.Button.Text == "Status")
          {
              APIEventHandler.AppTagDetectedEventHandler -= new AppTagDetectedEvent(TagDetected);//+= new EnableOutputRelay(eventActions_EnableOutputRelayEvent);
              APIEventHandler.AppTagDetectedRSSIEventHandler -= new AppTagDetectedRSSIEvent(TagDetectedRSSI);
              APIEventHandler.AppTagDetectedSaniEventHandler -= new AppTagDetectedSaniEvent(TagDetectedSani);

              using (StatusSearch searchDlg = new StatusSearch(this))
              {
                  searchDlg.ShowDialog(this);
              }

              APIEventHandler.AppTagDetectedEventHandler += new AppTagDetectedEvent(TagDetected);//+= new EnableOutputRelay(eventActions_EnableOutputRelayEvent);
              APIEventHandler.AppTagDetectedRSSIEventHandler += new AppTagDetectedRSSIEvent(TagDetectedRSSI);
              APIEventHandler.AppTagDetectedSaniEventHandler += new AppTagDetectedSaniEvent(TagDetectedSani);
          }
          else if (e.Button.Text == "Statistics")
          {
              using (Statistics statsDlg = new Statistics())
              {
                  statsDlg.ShowDialog(this);
              }
          }
#endif
          else if (e.Button.Text == "Action")
          {
              ActionForm actionDlg = new ActionForm(this);
              if (actionDlg.ShowDialog(this) == DialogResult.OK)
              {
                  ;
              }
              else
              {
                  ;
              }
              actionDlg.Dispose();

              LoadActionList();
          }
		  else if (e.Button.Text == "Report")
		  {
              using (ReportViewer viewer = new ReportViewer())
                  viewer.ShowDialog();
		  }
		  else if (e.Button.Text == "Database")
		  {
			  DatabaseForm dbDlg = new DatabaseForm(this);
			
			  if (dbDlg.ShowDialog(this) == DialogResult.OK)
			  {
				  // Read the contents of testDialog's TextBox.
				  ;
			  }
			  else
			  {
				  ;
			  }
			  dbDlg.Dispose();
		  }
		  else if (e.Button.Text == "Employee")
		  {
			  AccessTagForm AccTagRegDlg = new AccessTagForm(this);
			

			  if (AccTagRegDlg.ShowDialog(this) == DialogResult.OK)
			  {
				  // Read the contents of testDialog's TextBox.
				  ;
			  }
			  else
			  {
				  ;
			  }
			  AccTagRegDlg.Dispose();
		  }
		  else if (e.Button.Text == "Zone")
		  {
			  ZonesForm zonesDlg = new ZonesForm(this);
			

			  if (zonesDlg.ShowDialog(this) == DialogResult.OK)
			  {
				  // Read the contents of testDialog's TextBox.
				  ;
			  }
			  else
			  {
				  ;
			  }
			  zonesDlg.Dispose();
              UpdateCommunicationStatusBar();
		  }
		  else if (e.Button.Text == "Visitor")
		  {
			  

			  Form9 tempTagDlg = new Form9(this);
			

			  if (tempTagDlg.ShowDialog(this) == DialogResult.OK)
			  {
				  // Read the contents of testDialog's TextBox.
				  ;
			  }
			  else
			  {
				  ;
			  }
			  tempTagDlg.Dispose();
		  }
		  else if (e.Button.Text == "Mapper")
		  {
			  Process mapper = new Process();

			  //*****************NOTE NOTE NOTE ******************************************
			 
			  //this line should be active for the release
			  mapper.StartInfo.FileName = "Mapper.exe";
			 
			  //for inhouse testing       
              //mapper.StartInfo.FileName = "C:\\AWI Softwares\\Smart Tracker\\V3.0.0\\Server\\bin\\Debug\\MapperV7.0.exe";
              //mapper.StartInfo.WorkingDirectory = "C:\\AWI Softwares\\Smart Tracker\\V3.0.0\\Server\\bin\\Debug\\";
			                                      
			  mapper.Start();
			 

			  //Getting mapper.exe path
			  //---------------------------------
			  //string fileName = "myfile.ext";
			  //string path = @"\mydir\";
			  //string fullPath;
			  //fullPath = Path.GetFullPath(path);
		  }
			  /*else if (e.Button.Text == "Report")
			  {
				  BankReportForm bankReportDlg = new BankReportForm(this);
			

				  if (bankReportDlg.ShowDialog(this) == DialogResult.OK)
				  {
					  // Read the contents of testDialog's TextBox.
					  ;
				  }
				  else
				  {
					  ;
				  }
				  bankReportDlg.Dispose();
			  }*/
		  else if (e.Button.Text == "Help")
		  {
			  /*RSSIConfigForm rssiDlg = new RSSIConfigForm(this);
			  if (rssiDlg.ShowDialog(this) == DialogResult.OK)
			  {
				  // Read the contents of testDialog's TextBox.
				  ;
			  }
			  else
			  {
				  ;
			  }
			  rssiDlg.Dispose();*/ 

			  HelpForm helpDlg = new HelpForm();
			
			  if (helpDlg.ShowDialog(this) == DialogResult.OK)
			  {
				  // Read the contents of testDialog's TextBox.
				  ;
			  }
			  else
			  {
				  ;
			  }
			  helpDlg.Dispose();
                      
		  }
	     
	  }
	  
      private void UpdateCommunicationStatusBar()
      {
          //This function Updates Communication StatusBar
          ushort rdr = 0;
          //ushort startCommCounter = 0;

          if (rs232Connection > 0)
          {
              if (startRS232Communication(out rdr))
              {
                  rs232Reader = rdr;
                  //startCommCounter += 1;
              }
              //else
              //{
                  //MainStatusBar.Panels.RemoveAt(2);
              //}
          }
          else
          {
              //MainStatusBar.Panels.RemoveAt(2);
              MainStatusBar.Panels.Remove(CommStatusBarPanel);
          }

          if (networkConnection > 0)
          {
              if (startNetworkCommunication())
              {
                  ;//startCommCounter += 1;
              }
              //else
             // {
                  //MainStatusBar.Panels.RemoveAt(1);
              //}
          }
          else
          {
              //MainStatusBar.Panels.RemoveAt(1);
              MainStatusBar.Panels.Remove(NetCommStatusBarPanel);
          }

          //if (startCommCounter == 0)
          //{
              //MainStatusBar.Panels.Insert(1, NetCommStatusBarPanel);
              //MainStatusBar.Panels.Insert(2, CommStatusBarPanel);
          //}
      }

	  private void ShowErrorMessage(string msg)
	  {
          MessageBox.Show(this, msg, "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	  }

	  private void MainStatusBar_DrawItem(object sender, System.Windows.Forms.StatusBarDrawItemEventArgs sbdevent)
	  {
		 //change the color of statusbar items
		 //int n = 0;
	  }

	  private void Relay2Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
	  {
		  if (relay2Stat == relayStatus.Open) // || (relay2Stat == relayStatus.Close))
		  {
			  communication.DisableOutput(2, readerID, true);
		  }
		  Relay2Timer.Enabled = false;
	  }

	  private void Relay1Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
	  {
	     if (relay1Stat == relayStatus.Open)
		 {
			communication.DisableOutput(1, readerID, true);
		 }
	  }

        private void ClockTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
	   {
	     //DateTime dt = DateTime.Now;
           if (m_connection == null)
               return;

		  if ((m_connection.State != ConnectionState.Open) || closingProcess)  //added on Dec 06, 06
			  return;

          if (virtualListLoaded)
          {
              if (loadCount == 0)
              {
                  loadCount += 1;
                  if (listView.Items.Count >= 1)
                  {
                      //listView.Items[listView.Items.Count - 1].EnsureVisible();
                      //listView.Items[listView.Items.Count - 1].Selected = true;
                  }
              }

              this.BeginInvoke(new Action(PollVirtualList));
              
          }

		  //lock (m_connection)   //added on Dec 06, 06
		  {
			  //V5_CHG MainStatusBar.Panels[2].Text = DateTime.Now.ToString("MM-dd-yyyy  hh:mm:ss tt");
              this.BeginInvoke(new UpdateMainStatusBarCallback(this.UpdateMainStatusBarText), new object[] { "Time", DateTime.Now.ToString("MM-dd-yyyy  hh:mm:ss tt"), null }); //panelNum, text              

			  if (RdrOffline && (rdrsStatus == 99))
			  {
				  if (showOffRdrIcon)
				  {
					  showOffRdrIcon = false;
                      //V5_CHG CommStatusBarPanel.Text = "Communication: Connected   Readers: Online";
                      //this.Invoke(new UpdateMainStatusBarTextCallback(this.UpdateMainStatusBarText), new object[] { 2, "RS232 Reader: Online" }); 
                      
                      try
					  {
						  //Console.WriteLine("XXX I am here 05");
                          //V5_CHG MainStatusBar.Panels[0].Icon = Resources.ReaderDisconnected;
                          this.Invoke(new UpdateMainStatusBarIconCallback(this.UpdateMainStatusBarIcon), new object[] { 1, Resources.ReaderDisconnected });
					  }
					  catch
					  {
                          //V5_CHG MainStatusBar.Panels[0].Icon = null;
                          this.Invoke(new UpdateMainStatusBarIconCallback(this.UpdateMainStatusBarIcon), new object[] { 1, null });
					  }
				  }
				  else
				  {
					  showOffRdrIcon = true;
					  //CommStatusBarPanel.Text = "Communication: Connected   Readers: Online";
                      //this.CommStatusBarPanel.Text = "RS232 Reader: Online";
					  try
					  {
                          //V5_CHG MainStatusBar.Panels[0].Icon = Resources.ReaderConnected;
                          this.Invoke(new UpdateMainStatusBarIconCallback(this.UpdateMainStatusBarIcon), new object[] { 1, Resources.ReaderConnected }); 
					  }
					  catch
					  {
						  //V5_CHG MainStatusBar.Panels[0].Icon = null;
                          this.Invoke(new UpdateMainStatusBarIconCallback(this.UpdateMainStatusBarIcon), new object[] { 1, null });
					  }
				  }

			  }

			  if (powerup.ip.Length > 0)
			  {
				  DateTime pwTime = powerup.timestamp.AddSeconds(6);
				  if (pwTime < DateTime.Now)
				  {
                     powerup.ip = "";
				  }
			  }

		  }//lock
	  }
	  
	  private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
	  {
		  //RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Active Wave\\bank\\");
		  //reg.SetValue("count", trafficCounter);

          if (MessageBox.Show(this, "Close Smart Tracker?", "Smart Tracker", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
          {
              e.Cancel = true;
              return;
          }

		  CultureInfo ci = new CultureInfo("sv-SE", true);
		  System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
		  ci.DateTimeFormat.DateSeparator = "-";

          closingProcess = true;
          closingApp = true;

          WaitOnThread();

		  //Set back the environment variable
		  //SetEnvVars(1);

		  if (rs232Comm) 
		  {
			  communication.CloseSerialPort();
		  }

          

          if (netConnection || socketsConnection)
		  {
			  /*if (!AreAllSocketsClosed())
			  {
				  if (MessageBox.Show(this, "Please Make sure Readers are disconnected from the network before closing the application. Do you want continue closing?", "Bank", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				  {
					  e.Cancel = true;
                      closingProcess = false;
					  return;
				  }
			  }*/
              
              //closingProcess = true;
			  DbStatusTimer.Enabled = false;
			  PollDbTimer.Enabled = false;
              ClockTimer.Enabled = false;

              //[deprecated]
			  //Set back the environment variable
			  //SetEnvVars(1);

              communication.SocketDisconnection(null);

              CommunicationClass.PowerupEventHandler -= new PowerupEvent(this.PowerupReaderNotifty);
              CommunicationClass.RdrErrorEventHandler -= new RdrErrorEvent(this.ErrorEventNotify);
              CommunicationClass.ResetReaderEventHandler -= new ResetReaderEvent(this.ResetReaderEventNotify);
              CommunicationClass.EnableReaderEventHandler -= new EnableReaderEvent(this.EnableReaderEventNotify);
              CommunicationClass.ScanNetworkEventHandler -= new ScanNetworkEvent(this.ScanNetworkEventNotify);
              CommunicationClass.OpenSocketEventHandler -= new OpenSocketEvent(this.OpenSocketEventNotify);
              CommunicationClass.CloseSocketEventHandler -= new CloseSocketEvent(this.CloseSocketEventNotify);
              CommunicationClass.EnableTagEventHandler -= new EnableTagAckEvent(this.EnableTagEventNotify);
              CommunicationClass.QueryTagEventHandler -= new QueryTagAckEvent(this.QueryTagEventNotify);
              CommunicationClass.InputChangeEventHandler -= new InputChangeEvent(this.InputChangeEventNotify);

              CommunicationClass.TagDetectedRSSITamperEventHandler -= new TagDetectedRSSITamperEvent(TagDetectedRSSI);
              CommunicationClass.TagDetectedTamperEventHandler -= new TagDetectedTamperEvent(TagDetected);

              APIEventHandler.AppTagDetectedEventHandler -= new AppTagDetectedEvent(TagDetected);//+= new EnableOutputRelay(eventActions_EnableOutputRelayEvent);
              APIEventHandler.AppTagDetectedRSSIEventHandler -= new AppTagDetectedRSSIEvent(TagDetectedRSSI);
              APIEventHandler.AppTagDetectedSaniEventHandler -= new AppTagDetectedSaniEvent(TagDetectedSani);

              OdbcDbClass.NotifyDBConnectionStatusHandler -= new NotifyDBConnectionStatus(DBConnectionStatusHandler);

              CommunicationClass.EnableRelayAckEventHandler -= new EnableRelayAckEvent(EnableRelayAck);
              CommunicationClass.DisableRelayAckEventHandler -= new DisableRelayAckEvent(DisableRelayAck);

              WaitOnThread();

              try
              {
                  //NetRdrDisconnection.SetRdrPolling(false);
                  NetRdrDisconnection.TrunOffRdrPolling();
              }
              catch (Exception ex)
              {
                  Console.WriteLine(ex.Message.ToString());
              }
          }

            communication.CloseSerialPort();
            if (communication.IsGeneralThreadAlive())
                communication.AbortGeneralThread();


              if (netConnection || socketsConnection)
              {
                  if (m_connection == null)
			     return;
			  
			  lock (m_connection)
			  {
				  OdbcCommand cmd = new OdbcCommand();
				   
				  StringBuilder sql = new StringBuilder();
				  sql.Append("UPDATE netip SET NetworkStatus = 'Inactive', RdrStatus = 'Offline', ConnectTime = ");
		          sql.AppendFormat("'{0}'", DateTime.Now);
				  OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
				   
				   
				  try
				  {
					  myCommand.ExecuteNonQuery();
				  }
				  catch //(Exception ex)
				  {
					  ;
				  }
			  //}//lock
		  //} //netConnection

		  

		  //if (m_connection != null)
		  //{
			  StringBuilder sql2 = new StringBuilder();
			  sql2.Append("UPDATE zones SET Status = ");
			  sql2.AppendFormat("('{0}')", "Offline");
			  sql2.AppendFormat(", Time = '{0}'", DateTime.Now);
				   
			  OdbcCommand myCommand2 = new OdbcCommand(sql2.ToString(), m_connection);
				
			  //lock (m_connection)
			  //{
				  try
				  {
					  myCommand2.ExecuteNonQuery();
				  }
				  catch //(Exception ex)
				  {
					  //ShowErrorMessage(ex.Message);
					  return;
				  }
			  }//lock m_coneection
		  }
	  }

	  private void MainForm_Activated(object sender, System.EventArgs e)
	  {
		
	  }

      private class ReaderRelay
      {
          private ushort reader;
          private ushort relay;

          public ReaderRelay(ushort reader, ushort relay)
          {
              this.reader = reader;
              this.relay = relay;
          }

          public ushort Reader { get { return reader; } }
          public ushort Relay { get { return relay; } }

          public override bool Equals(object obj)
          {
              if (obj is ReaderRelay)
              {
                  ReaderRelay comp = (ReaderRelay)obj;
                  if ((comp.reader == reader) && (comp.relay == relay))
                      return true;
                  else
                      return false;
              }
              else
                  return base.Equals(obj);
          }

          public override int GetHashCode()
          {
              return (reader << 16) | relay;
          }
      }

      private class TagRelayAction
      {
          public enum TagType : byte
          {
              Access = 1,
              Inventory = 2,
              Asset = 3
          }

          private ushort id;
          private TagType type;
          private string action;

          public TagRelayAction(ushort id, TagType type, string action)
          {
              this.id = id;
              this.type = type;
              this.action = action;
          }

          public TagRelayAction(ushort id, byte type, string action)
          {
              this.id = id;
              this.type = (TagType)type;
              this.action = action;
          }

          public TagRelayAction(uint id, byte type, string action)
          {
              this.id = (ushort)id;
              this.type = (TagType)type;
              this.action = action;
          }

          public ushort ID { get { return id; } }
          public TagType Type { get { return type; } }
          public string Action { get { return action; } }

          public override bool Equals(object obj)
          {
              if (obj is TagRelayAction)
              {
                  TagRelayAction comp = (TagRelayAction)obj;
                  if ((comp.id == id) && (comp.type == type) && (comp.action == action))
                      return true;
                  else
                      return false;
              }
              else
                  return base.Equals(obj);
          }

          public override int GetHashCode()
          {
              return ((id << 8) | (byte)type) * 13 + action.GetHashCode();
          }
      }

      List<KeyValuePair<ReaderRelay, TagRelayAction>> ReaderRelayTag = new List<KeyValuePair<ReaderRelay, TagRelayAction>>();

	  private void EnableRelayAck(ushort relay, ushort reader)
	  {

#warning Test
          return;


          CultureInfo ci = (CultureInfo)CultureInfo.InvariantCulture.Clone();
          ci.DateTimeFormat.DateSeparator = "-";

          lock (timerLock)
		  {
			  string parkNum = "";
			  string loc = "";
			  string status = "";
			  parkNum = GetParkInfo (reader, 0, out loc, out status);

		  if (relay == 0x01)
		  {
			  Console.WriteLine("SECTION 2 - RELAY 1 OPENED   " + DateTime.Now.ToString());
			  counter01 = 0;
			  disableRelay01 = true;
			  lastOpenRelay01Time = DateTime.Now.Hour*60+60 + DateTime.Now.Hour*60 + DateTime.Now.Second;

              var match = ReaderRelayTag.FirstOrDefault(item => (item.Key.Reader == reader) && (item.Key.Relay == relay));
              var tag = match.Value;

              string name = "";
              string department = "";
              if (tag.Type == TagRelayAction.TagType.Access)
              {
                  var employee = EmployeesQuery.GetEmployee(tag.ID);
                  name = employee.Name;
                  department = employee.Department;
              }

              ListViewItem listItemA = new ListViewItem("");
			  listItemA.SubItems.Add(tag.ID.ToString());   //tagid	 
			  listItemA.SubItems.Add(name);   //tag type
			  listItemA.SubItems.Add(loc);   //location
			  listItemA.SubItems.Add(tag.Action);   //event description
              listItemA.SubItems.Add(DateTime.Now.ToString(ci));    //time
#if SANI
              listItemA.SubItems.Add("");
              listItemA.SubItems.Add("");
#endif
              lvi[virtualListIndex++] = listItemA;

              SaveTraffic(tag.ID.ToString(), name, null, department, parkNum, loc, "Relay Open", tag.Action, DateTime.Now, (byte)tag.Type);

              PlaySound(1);
		  }
		  else if (relay == 0x02)
		  {
			  Console.WriteLine("SECTION 2 - RELAY 2 OPENED   " + DateTime.Now.ToString());
			  counter02 = 0;
			  lastOpenRelay02Time = DateTime.Now.Hour*60+60 + DateTime.Now.Hour*60 + DateTime.Now.Second;
			  disableRelay02 = true;

              var match = ReaderRelayTag.FirstOrDefault(item => (item.Key.Reader == reader) && (item.Key.Relay == relay));
              var tag = match.Value;

              string name = "";
              string department = "";
              if (tag.Type == TagRelayAction.TagType.Access)
              {
                  var employee = EmployeesQuery.GetEmployee(tag.ID);
                  name = employee.Name;
                  department = employee.Department;
              }

              ListViewItem listItemA = new ListViewItem("");
			  listItemA.SubItems.Add(tag.ID.ToString());   //tagid	 
			  listItemA.SubItems.Add(name);   //tag type
			  listItemA.SubItems.Add(loc);   //location
			  listItemA.SubItems.Add(tag.Action);   //event description
              listItemA.SubItems.Add(DateTime.Now.ToString(ci));    //time
#if SANI
              listItemA.SubItems.Add("");
              listItemA.SubItems.Add("");
#endif
              lvi[virtualListIndex++] = listItemA;

              SaveTraffic(tag.ID.ToString(), name, null, department, parkNum, loc, "Relay Open", tag.Action, DateTime.Now, (byte)tag.Type);

			  PlaySound(1);
		  }

		  if (autoReset)
		     resetCounter = 0;

		  }//lock

      }

	  private void DisableRelayAck(ushort relay, ushort reader)
	  {
#warning Test
          return;


          CultureInfo ci = (CultureInfo)CultureInfo.InvariantCulture.Clone();
          ci.DateTimeFormat.DateSeparator = "-";

          lock (timerLock)
		  {
			  string parkNum = "";
			  string loc = "";
			  string status = "";
			  parkNum = GetParkInfo (reader, 0, out loc, out status);

		  if (relay == 0x01)
		  {
		     Console.WriteLine("SECTION 3 - RELAY 1 CLOSED   " + DateTime.Now.ToString());

             var match = ReaderRelayTag.FirstOrDefault(item => (item.Key.Reader == reader) && (item.Key.Relay == relay));
             var tag = match.Value;
             ReaderRelayTag.Remove(match);

             string name = "";
             string department = "";
             if (tag.Type == TagRelayAction.TagType.Access)
             {
                 var employee = EmployeesQuery.GetEmployee(tag.ID);
                 name = employee.Name;
                 department = employee.Department;
             }

             ListViewItem listItemA = new ListViewItem("");
             listItemA.SubItems.Add(tag.ID.ToString());   //tagid	 
             listItemA.SubItems.Add(name);   //tag type
             listItemA.SubItems.Add(loc);   //location
             listItemA.SubItems.Add(tag.Action);   //event description
             listItemA.SubItems.Add(DateTime.Now.ToString(ci));    //time
#if SANI
              listItemA.SubItems.Add("");
              listItemA.SubItems.Add("");
#endif
             lvi[virtualListIndex++] = listItemA;

             SaveTraffic(tag.ID.ToString(), name, null, department, parkNum, loc, "Relay Close", tag.Action, DateTime.Now, (byte)tag.Type);

			  PlaySound(1);

			  disableRelay01 = false;

		  }
		  else if (relay == 0x02)
		  {
			  Console.WriteLine("SECTION 3 - RELAY 2 CLOSED   " + DateTime.Now.ToString());

              var match = ReaderRelayTag.FirstOrDefault(item => (item.Key.Reader == reader) && (item.Key.Relay == relay));
              var tag = match.Value;
              ReaderRelayTag.Remove(match);

              string name = "";
              string department = "";
              if (tag.Type == TagRelayAction.TagType.Access)
              {
                  var employee = EmployeesQuery.GetEmployee(tag.ID);
                  name = employee.Name;
                  department = employee.Department;
              }

              ListViewItem listItemA = new ListViewItem("");
              listItemA.SubItems.Add(tag.ID.ToString());   //tagid	 
              listItemA.SubItems.Add(name);   //tag type
              listItemA.SubItems.Add(loc);   //location
              listItemA.SubItems.Add(tag.Action);   //event description
              listItemA.SubItems.Add(DateTime.Now.ToString(ci));    //time
#if SANI
              listItemA.SubItems.Add("");
              listItemA.SubItems.Add("");
#endif
              lvi[virtualListIndex++] = listItemA;

              SaveTraffic(tag.ID.ToString(), name, null, department, parkNum, loc, "Relay Close", tag.Action, DateTime.Now, (byte)tag.Type);

              PlaySound(1);

			  disableRelay02 = false;

		  }

		  resetCounter = 0;
		  }//lock
      }

		private void CleanupTimer_Tick(object sender, System.EventArgs e)
		{
		}

		private void CmdSyncTimer_Tick(object sender, System.EventArgs e)
		{
			if (m_connection == null)
				return;

			lock (m_connection)   //added on Dec 06, 06
			{
				if (!timerStop)
				{
					if (rdrIndexPoll >= numRdrs)
						rdrIndexPoll = 0;
					if ((readerStatus[rdrIndexPoll].rdrID > 0) && (readerStatus[rdrIndexPoll].hostID > 0)) // && readerStatus[rdrIndexPoll].counter <= 3)           // && (readerStatus[rdrIndexPoll].netStatus == "Active"))														
					{
						communication.EnableReader(Convert.ToUInt16(readerStatus[rdrIndexPoll].rdrID), Convert.ToUInt16(readerStatus[rdrIndexPoll].hostID));
						readerStatus[rdrIndexPoll].counter += 1;
					}
					rdrIndexPoll += 1;
				}
			}

		}

		public static void PlaySound(ushort type)
		{
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();

            if (type == 1)
            {
                player.Stream = Resources.Ding;
                player.Play();
            }
            else if (type == 2)
            {
                player.Stream = Resources.Alert;
                player.Play();
            }
            else if (type == 3)
            {
#warning Missing Connect Wave File
                //player.Stream = Resources.Conn;
                //player.Play();
            }
		}

		private void UpdateList(uint tagID, string zoneID, bool validTag)
		{
            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

		    ListViewItem listItem = new ListViewItem(""); 
			listItem.SubItems.Add(Convert.ToString(tagID));  //tagid
			listItem.SubItems.Add(zoneID);
            if (validTag)
                listItem.SubItems.Add("Valid Tag Detected");  //event
            else
            {
                listItem.SubItems.Add("Invalid Tag Detected");  //event                
            }
			listItem.SubItems.Add(DateTime.Now.ToString());  //timeStamp
			
            lvi[virtualListIndex++] = listItem;

			listView.Items[listView.Items.Count-1].EnsureVisible();
		}

		private void PopulateActivityTable(uint tagID, byte tagType, string zoneID, bool update, string events)
		{
			if (m_connection == null)
				return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			lock (m_connection)
			{
				lock(activityTagLock)
				{
			
					string tag = "";
					if (tagType == AW_API_NET.APIConsts.ACCESS_TAG)
						tag = "ACC"+tagID;
					else if (tagType == AW_API_NET.APIConsts.ASSET_TAG)
						tag = "AST"+tagID;
					else if (tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
						tag = "INV"+tagID;
					else
						return;

					StringBuilder mySelectQuery = new StringBuilder();
					mySelectQuery.Append("SELECT TagID FROM tagactivity WHERE TagID = ");          
					mySelectQuery.AppendFormat("'{0}'", tag);
			
					string mySelectStr = mySelectQuery.ToString();

					OdbcCommand myCommand = new OdbcCommand(mySelectStr, m_connection);  
					OdbcDataReader myReader = null;
		   
					try
					{
						myReader = myCommand.ExecuteReader();
						reconnectCounter = -1;
					}
					catch (Exception ex)
					{
						int ret = 0, ret1 = 0, ret2 = 0;
						if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
							((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
							((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
						{   
							//error code 2013
							
							if (reconnectCounter < 0)
							{
								reconnectCounter = 0;
							}

						}
						if (myReader != null)
						{
							if (!myReader.IsClosed)
								myReader.Close();
						}
						return;
					}//catch .. try

					if (myReader.Read())
						update = true;
					else
						update = false;

					myReader.Close();
				
					string SQL;
					if (update)
						SQL = "UPDATE tagactivity SET ZoneID=?, Event=?, Timestamp=?  WHERE TagID=?";              
					else
						SQL = "INSERT INTO tagactivity (TagID, ZoneID, Event, Timestamp) VALUES (?, ?, ?, ?)";
		        
					OdbcCommand cmd = new OdbcCommand(SQL, m_connection);

					if (!update)
						cmd.Parameters.Add(new OdbcParameter("", tag));
					if (zoneID == "")
						cmd.Parameters.Add(new OdbcParameter("", Convert.ToInt32(0)));
					else
						cmd.Parameters.Add(new OdbcParameter("", Convert.ToInt32(zoneID)));
					cmd.Parameters.Add(new OdbcParameter("", events));
					cmd.Parameters.Add(new OdbcParameter("", DateTime.Now));

					if (update)
						cmd.Parameters.Add(new OdbcParameter("", tag));

					try
					{
						cmd.ExecuteNonQuery();
					}
					catch //(Exception ex)
					{
						return;
					}
				}//activityTagLock
			}//lock m_connection
		}

		private void PopulateHistoryTable(uint tagID, byte tagType, string zoneID, string events)
		{
			if ((m_connection == null) || (zoneID == "")) 
				return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			lock (m_connection)
			{
				lock(historyLock)
				{
					string tag = "";
                    if (tagType == AW_API_NET.APIConsts.ACCESS_TAG)
                        tag = "ACC" + tagID;
                    else if (tagType == AW_API_NET.APIConsts.ASSET_TAG)
                        tag = "AST" + tagID;
                    else if (tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
                        tag = "INV" + tagID;
                    else if (tagType == 0)
                        tag = "";
                    else
						return;
				
					string	SQL = "INSERT INTO history (TagID, ZoneID, Event, Timestamp) VALUES (?, ?, ?, ?)";
		        
					OdbcCommand cmd = new OdbcCommand(SQL, m_connection);

					cmd.Parameters.Add(new OdbcParameter("", tag));
					cmd.Parameters.Add(new OdbcParameter("", Convert.ToInt32(zoneID)));
					cmd.Parameters.Add(new OdbcParameter("", events));
					cmd.Parameters.Add(new OdbcParameter("", DateTime.Now));

					//lock (m_connection)
					//{
					try
					{
						cmd.ExecuteNonQuery();
					}
					catch //(Exception ex)
					{
						//ShowErrorMessage(ex.Message);
						return;
					}
					//}
				}//historyLock
			}//lock m_connection
		}

		private void listView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
		  if (e.Button == MouseButtons.Right)
		  {
             
		  }

		}

		private void ClearList_Click(object sender, System.EventArgs e)
		{
			listView.Items.Clear();
		}
		
		private void listView_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{

		}

		private void listView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
            loadMsg = new LoadMsgForm();
            loadMsg.Show();

			//startPolling = true;
            ClockTimer.Enabled = true;
            LoadActionList();

            if (rs232Connection > 0)
               rs232Thread.Start();

            if (networkConnection > 0)
                netThread.Start();
            
            UpdateCommunicationStatusBar();
		}

        private bool startRS232Communication(out ushort rdr)
        {
            rdr = 0;
            if (m_connection == null)
                return (false);

            bool retVal = false;

            lock (m_connection)  
            {
                string mySelectStr = "SELECT ReaderID FROM zones WHERE ReaderType = '1'";                
                OdbcCommand myCommand = new OdbcCommand(mySelectStr, m_connection);
                OdbcDataReader myReader = null;
                
                try
                {
                    myReader = myCommand.ExecuteReader();
                    reconnectCounter = -1;                
                }
                catch (Exception ex)
                {
                    int ret = 0, ret1 = 0, ret2 = 0;
                    if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                        ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                        ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                    {
                        //error code 2013
                        if (reconnectCounter < 0)
                        {
                            reconnectCounter = 0;
                        }                        
                    }
                    if (myReader != null)
                    {
                        if (!myReader.IsClosed)
                            myReader.Close();
                    }
                    return false;
                }//catch .. try

                if (myReader.Read())
                {
                    rdr = Convert.ToUInt16(myReader.GetInt16(0));
                    retVal = true;
                }
                
                myReader.Close();
                return (retVal);
            }//lock m_connection
        }

        private bool startNetworkCommunication()
        {
            if (m_connection == null)
                return (false);

            bool retVal = false;

            lock (m_connection)
            {
                string mySelectStr = "SELECT ReaderType FROM zones WHERE ReaderType = '0'";
                OdbcCommand myCommand = new OdbcCommand(mySelectStr, m_connection);
                OdbcDataReader myReader = null;

                try
                {
                    myReader = myCommand.ExecuteReader();
                    reconnectCounter = -1;
                }
                catch (Exception ex)
                {
                    int ret = 0, ret1 = 0, ret2 = 0;
                    if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                        ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                        ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                    {
                        //error code 2013
                        if (reconnectCounter < 0)
                        {
                            reconnectCounter = 0;
                        }
                    }
                    if (myReader != null)
                    {
                        if (!myReader.IsClosed)
                            myReader.Close();
                    }
                    return false;
                }//catch .. try

                if (myReader.Read())
                    retVal = true;

                myReader.Close();
                return (retVal);
            }//lock m_connection
        }

        private void DbStatusTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (dbDisconnectedFlag && !dbConnectDlgDisplay)
			{
				if (reconnectCounter == MAX_RETRY_DB_CONNECT)
				{
					//>>timer3.Enabled = false;
					DBStatusBarPanel.Text = "";
					DBStatusBarPanel.Text = "DB Server : MySQL  Disconnected";
					try
					{
						DBStatusBarPanel.Icon = Resources.DbDisconnected;
					}
					catch
					{
						DBStatusBarPanel.Icon = null;
					}

					try
					{
						//CommStatusBarPanel.Text = "System Not Ready";
                        CommStatusBarPanel.Text = "RS232 Reader: Online";
						CommStatusBarPanel.Icon = Resources.ReaderDisconnected;
                    
					}
					catch //(Exception ex)
					{
						//MainStatusBar.Panels[0].Icon = null;
                        CommStatusBarPanel.Icon = null;
					}
					Console.WriteLine("Main timer3 count=" + Convert.ToString(reconnectCounter));
					if (!msgDisplayed)
					{
						if (!msgDisplayed)
						{
							msgDisplayed = true;
							//MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							dbDisconnectMsg = new MsgForm("Lost Connection To Database.\n" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"));
							dbDisconnectMsg.Show();
						}	
					}

					reconnectCounter += 1;
					ReconnectToDBServer();
				}
				else if (reconnectCounter > MAX_RETRY_DB_CONNECT)
				{
					if (reconnectCounter > 1000)
						reconnectCounter = MAX_RETRY_DB_CONNECT + 1;	
					else
						reconnectCounter += 1;

					Console.WriteLine("Main timer3 count=" + Convert.ToString(reconnectCounter));
					try
					{
						DBStatusBarPanel.Icon = Resources.DbDisconnected;
					}
					catch
					{
						DBStatusBarPanel.Icon = null;
					}

					ReconnectToDBServer();

				}
				else
				{
					reconnectCounter += 1;
					//msgDisplayed = false;
					Console.WriteLine("Main timer3 connect count=" + Convert.ToString(reconnectCounter));
					ReconnectToDBServer();
				}
			}//if dbDisconnectFlag

		}

		private void PollDbTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			//lock (m_connection) already embedded in PollDB()
			if (startPolling)
			   PollDB();
		}

		private void awiHistoryControl1_Load(object sender, System.EventArgs e)
		{
		
		}

        private void UpdateMainStatusBarText(string panel, string text, Icon icon)
        {
            //This function update Main status bar
            switch (panel)
            {
                case "System":

                    SysStatusBarPanel.Text = text;
                    if (icon == null)
                        SysStatusBarPanel.Icon = null;
                    else
                    {
                        try
                        {
                            SysStatusBarPanel.Icon = icon;
                        }
                        catch
                        {
                            SysStatusBarPanel.Icon = null;
                        }
                    }
                break;

                case "Network":
                    
                    NetCommStatusBarPanel.Text = text;
                    if (icon == null)
                        NetCommStatusBarPanel.Icon = null;
                    else
                    {
                        try
                        {
                            SysStatusBarPanel.Icon = icon;
                        }
                        catch
                        {
                            SysStatusBarPanel.Icon = null;
                        }
                    }
                break;

                case "RS232":
                    
                    CommStatusBarPanel.Text = text;
                    if (icon == null)
                        CommStatusBarPanel.Icon = null;
                    else
                    {
                        try
                        {
                            SysStatusBarPanel.Icon = icon;
                        }
                        catch
                        {
                            SysStatusBarPanel.Icon = null;
                        }
                    }
                break;

                case "Database":

                    DBStatusBarPanel.Text = text;
                    if (icon == null)
                        DBStatusBarPanel.Icon = null;
                    else
                    {
                        try
                        {
                            SysStatusBarPanel.Icon = icon;
                        }
                        catch
                        {
                            SysStatusBarPanel.Icon = null;
                        }
                    }
                break;

                case "Time":                    
                    DateTimeStatusBarPanel.Text = text;                    
                break;

                case "Version":                    
                    VersionStatusBarPanel.Text = text; 
                break;
            }            
        }

        private void UpdateMainStatusBarText(int panel, string text)
        {
            MainStatusBar.Panels[panel].Text = text;
        }

        private void UpdateMainStatusBarIcon(int panel, Icon icon)
        {
            MainStatusBar.Panels[panel].Icon = icon;
        }

        #region private bool RunNonQueryCmd(OdbcCommand cmd)
        public bool RunNonQueryCmd(OdbcCommand cmd)
        {
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if ((ex.Message.IndexOf("Lost connection") >= 0) ||
                    (ex.Message.IndexOf("(10061)") >= 0)) //can not connect
                {
                    if (reconnectCounter == -1)
                        reconnectCounter = 0;
                    dbDisconnectedFlag = true;
                }

                return false;
            }

            if (dbDisconnectedFlag)
            {
                try
                {
                    //CommStatusBarPanel.Text = "System Ready";
                    CommStatusBarPanel.Text = "RS232 Reader: Online";
                    CommStatusBarPanel.Icon = Resources.ReaderConnected;

                }
                catch //(Exception ex)
                {
                    //MainStatusBar.Panels[0].Icon = null;
                    CommStatusBarPanel.Icon = null;
                }

                DBStatusBarPanel.Text = "DB Server : MySQL  Connected";
                try
                {
                    DBStatusBarPanel.Icon = Resources.DbConnected;
                }
                catch
                {
                    DBStatusBarPanel.Icon = null;
                }
            }

            dbDisconnectedFlag = false;
            reconnectCounter = -1;
            if (msgDisplayed)
            {
                msgDisplayed = false;
                if (dbDisconnectMsg != null)
                {
                    dbDisconnectMsg.Close();
                    dbDisconnectMsg.Dispose();
                }
            }


            return (true);
        }
        #endregion

        #region private bool RunNonQueryCmd(string qryStr)
        public bool RunNonQueryCmd(string qryStr)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var myCommand = new OdbcCommand(qryStr, con))
            {
                try
                {
                    con.Open();

                    myCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    if ((ex.Message.IndexOf("Lost connection") >= 0) ||
                        (ex.Message.IndexOf("(10061)") >= 0)) //can not connect
                    {
                        if (reconnectCounter == -1)
                            reconnectCounter = 0;
                        dbDisconnectedFlag = true;
                    }

                    return false;
                }
            }

            if (dbDisconnectedFlag)
            {
                try
                {
                    //CommStatusBarPanel.Text = "System Ready";
                    CommStatusBarPanel.Text = "RS232 Reader: Online";
                    CommStatusBarPanel.Icon = Resources.ReaderConnected;

                }
                catch //(Exception ex)
                {
                    //MainStatusBar.Panels[0].Icon = null;
                    CommStatusBarPanel.Icon = null;
                }

                DBStatusBarPanel.Text = "DB Server : MySQL  Connected";
                try
                {
                    DBStatusBarPanel.Icon = Resources.DbConnected;
                }
                catch
                {
                    DBStatusBarPanel.Icon = null;
                }
            }

            dbDisconnectedFlag = false;
            reconnectCounter = -1;
            if (msgDisplayed)
            {
                msgDisplayed = false;
                if (dbDisconnectMsg != null)
                {
                    dbDisconnectMsg.Close();
                    dbDisconnectMsg.Dispose();
                }
            }
            return (true);
        }
        #endregion

        #region LoadActionList()
        public void LoadActionList()
        {
            String query = "SELECT eventaction.ReaderID, eventaction.FGenID, eventaction.EventID, actiondef.ReaderID, actiondef.ActionID, actiondef.Duration, eventaction.Description FROM eventaction, actiondef WHERE eventaction.EventActionID = actiondef.EventActionID";
            OdbcDataReader myReader;
            if (!RunQueryCmd(query, out myReader))
            {
                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                        myReader.Close();
                }

                //msgbox
                return;
            }

            ushort eRdr = 0;
            ushort efgen = 0;
            ushort aRdr = 0;
            ushort dur = 0;
            ushort aRel = 0;
            int eType = 0;
            int aType = 0;
            string desc;

            actionList.Clear();

            while (myReader.Read())
            {

                try
                {
                    eRdr = Convert.ToUInt16(myReader.GetString(0));
                    efgen = Convert.ToUInt16(myReader.GetString(1));
                    eType = Convert.ToInt32(myReader.GetString(2));
                    aRdr = Convert.ToUInt16(myReader.GetString(3));
                    aType = Convert.ToInt32(myReader.GetString(4));
                    dur = Convert.ToUInt16(myReader.GetString(5));
                    desc = myReader.GetString(6);
                    aRel = GetRelayNum(aType);
                    actionStruct eAction = new actionStruct(eRdr, efgen, eType, desc);  //aRel, dur, aType);                       
                    actionList.Add(eAction);
                    eAction.AddAction(aRdr, aRel, dur, aType);
                    if ((aRel == 1) || (aRel == 2))
                        communication.ConfigOutputRelay(eRdr, aRdr, aRel, dur);
                }
                catch //(Exception ex)
                {
                    Console.WriteLine("Reading from EventAction or ActionDef table Failed");
                    continue;
                }
            }

            myReader.Close();
        }
        #endregion

        #region GetRelayNum(aType)
        private ushort GetRelayNum(int aType)
        {
            //This function gets relay number
            ushort r = 0;

            switch (aType)
            {
                case UNLOCK_DOOR_RELAY_01:
                case TURN_ON_ALARM_LIGHT_RELAY_01:
                case TURN_ON_SIREN_RELAY_01:
                    r = 1;
                    break;

                case UNLOCK_DOOR_RELAY_02:
                case TURN_ON_ALARM_LIGHT_RELAY_02:
                case TURN_ON_SIREN_RELAY_02:
                    r = 2;
                    break;
            }

            return (r);
        }
        #endregion

        #region StartActionThread
        void StartActionThread(int evt, rfTagEvent_t tagEvent)
        {
            ushort rdr = tagEvent.reader;
            ushort fgen = tagEvent.fGenerator;

            try
            {
                WaitOnThread(); //force thread wait if other thread is active new object[] { progress, oItems,

                ParameterizedThreadStart threadStartAction = new ParameterizedThreadStart(HandleEventAction);
                handleEventActionThread = new Thread(threadStartAction);
                handleEventActionThread.Start(new object[] { evt, rdr, fgen, tagEvent.tag });
            }
            catch
            {
            }
        }
        #endregion

        #region WaitOnThread()
        void WaitOnThread()
        {
            try
            {
                // Queue thread: do not attempt Join if there is no active thread
                if (handleEventActionThread != null)
                {
                    if (handleEventActionThread.IsAlive == true)
                    {
                        handleEventActionThread.Join();
                    }

                    //UPDATE GUI, //REFRESH GUI 
                }
            }
            catch
            {
            }
        }
        #endregion

        public class InputTimer : System.Timers.Timer
        {
            private actionStruct actionStruct;
            private InputType input;

            public new event ElapsedEventHandler Elapsed;

            public InputTimer(actionStruct action, InputType input)
                : base()
            {
                AutoReset = false;
                actionStruct = action;
                this.input = input;

                ushort duration = 0;

                foreach (actionItemStruct act in action.actions)
                {
                    if ((act.actionType == EVENT_INPUT_01) && (input == InputType.Input1))
                    {
                        duration = act.duration;
                        break;
                    }

                    if ((act.actionType == EVENT_INPUT_02) && (input == InputType.Input2))
                    {
                        duration = act.duration;
                        break;
                    }
                }

                base.AutoReset = false;
                base.Interval = duration;
                base.Enabled = true;
                base.Elapsed += new System.Timers.ElapsedEventHandler(InputTimer_Elapsed);
                base.Start();
            }

            private void InputTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                if (Elapsed != null)
                {
                    Elapsed(this, e);
                }
            }
        }

        public class InputTimerPool
        {
            private class ReaderInfo
            {
                private ushort _reader;
                private ushort _fgen;
                private InputType _input;

                public ReaderInfo(ushort reader, ushort fgen, InputType input)
                {
                    _reader = reader;
                    _fgen = fgen;
                    _input = input;
                }

                public ushort Reader { get { return _reader; } }
                public ushort FGen { get { return _fgen; } }
                public InputType Input { get { return _input; } }

                public override bool Equals(object obj)
                {
                    if (obj is ReaderInfo)
                    {
                        ReaderInfo info = (ReaderInfo)obj;

                        if (info.Reader != _reader)
                            return false;
                        if (info.FGen != _fgen)
                            return false;
                        if (info.Input != _input)
                            return false;

                        return true;
                    }

                    return false;
                }
            }

            Dictionary<ReaderInfo, InputTimer> list;

            public InputTimerPool()
            {
                list = new Dictionary<ReaderInfo, InputTimer>();
            }

            public void Add(actionStruct action, InputType input)
            {
                ReaderInfo info = new ReaderInfo(action.eRdr, action.eFGen, input);
                if (!list.ContainsKey(info))
                {

                }
            }
        }

        void Input1Timer_Timeout(object sender, System.Timers.ElapsedEventArgs e)
        {
        }

        #region HandleEventAction(Event, rdr, fgen)
        private void HandleEventAction(object parameters)//(int evt, ushort rdr, ushort fgen)
        {
            object[] param = (object[])parameters;
            int evt = (int)param[0];
            ushort rdr = (ushort)param[1];
            ushort fgen = (ushort)param[2];

            bool input01 = false;
            bool input02 = false;
            if (evt == INPUT_DETECTED)
            {
                input01 = (bool)param[4];
                input02 = (bool)param[5];

                Console.WriteLine("Input Detected: Reader {0}, FGen {1}, Input #1 {2}, Input #2 {3}", rdr, fgen, input01, input02);
            }

            actionItemStruct actions;

            string loc;
            string status;
            string note;
            string parkNum = GetParkInfo(rdr, fgen, out loc, out status);
            string pName;
            string dept;

            int zone = int.Parse(parkNum);

            string tag_id_str;
            uint tag_id;
            byte tag_type;

            Employee employee = null;

            if (param[3] == null)
            {
                tag_id_str = "";
                tag_id = 0;
                tag_type = 0;
                if (evt == INPUT_DETECTED)
                {
                    if (input01 && input02)
                    {
                        pName = "Input 01 & Input 02";
                    }
                    else if (input01)
                    {
                        pName = "Input 01";
                    }
                    else if (input02)
                    {
                        pName = "Input 02";
                    }
                    else
                    {
                        pName = "";
                    }
                }
                else
                {
                    pName = "";
                }
                note = "";
                dept = "";
            }
            else
            {
                rfTag_t tag = (rfTag_t)param[3];

                tag_id_str = tag.id.ToString();
                tag_id = tag.id;
                tag_type = tag.tagType;

                pName = GetTagName(tag.id, tag.tagType, out note);
                dept = GetDepartmentName(tag_id_str);

                if (tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
                    employee = EmployeesQuery.GetEmployee(tag.id);
            }

            ListViewItem listItem = new ListViewItem();
            listItem.SubItems.Add(tag_id_str);   //tagid
            listItem.SubItems.Add(pName);         //tag description
            listItem.SubItems.Add(loc);       //location

            string cStr = tag_id_str + ",";
            cStr += tag_type + ",";
            cStr += rdr.ToString() + ",";
            cStr += fgen.ToString() + ",";
            cStr += pName + ",";
            cStr += dept + ",";
            cStr += parkNum + ",";
            cStr += loc + ",";
            cStr += "Action,";

            lock (CommunicationClass.relSyncObj)
            {
                foreach (actionStruct eAction in actionList)
                {
                    if ((eAction.eType == evt) && (eAction.eRdr == rdr) && (eAction.eFGen == fgen))
                    {
                        bool valid;
                        if (eAction.eType == MainForm.INPUT_DETECTED)
                        {
                            valid = false;
                            for (int i = 0; (i < eAction.actions.Count) && !valid; i++)
                            {
                                actions = (actionItemStruct)eAction.actions[i];

                                if (((actions.actionType == EVENT_INPUT_01) && input01) ||
                                    ((actions.actionType == EVENT_INPUT_02) && input02))
                                {
                                    valid = true;
                                }
                            }
                        }
                        else
                        {
                            valid = true;
                        }

                        if (valid)
                        {
                            if ((eAction.actions.Count > 0) &&
                                (!IsDupTagDisplay(listItem, 3, cStr + "," + eAction.eType)))
                            {
                                for (int i = 0; i < eAction.actions.Count; i++)
                                {
                                    actions = (actionItemStruct)eAction.actions[i];
                                    switch (actions.actionType)
                                    {
                                        case MainForm.TURN_ON_ALARM_LIGHT_RELAY_01:
                                        case MainForm.TURN_ON_ALARM_LIGHT_RELAY_02:
                                        case MainForm.TURN_ON_SIREN_RELAY_01:
                                        case MainForm.TURN_ON_SIREN_RELAY_02:
                                        case MainForm.UNLOCK_DOOR_RELAY_01:
                                        case MainForm.UNLOCK_DOOR_RELAY_02:
                                            if (actions.duration == 0)
                                            {
                                                Console.WriteLine("ReaderOutputRelay: Set Reader {0} Relay {1} Duration Forever, Time {2}", actions.aRdr, actions.aRelay, DateTime.Now);
                                            }
                                            else
                                            {
                                                Console.WriteLine("ReaderOutputRelay: Set Reader {0} Relay {1} Duration {2}, Time {3}", actions.aRdr, actions.aRelay, actions.duration, DateTime.Now);
                                                ReaderOutputRelayManager.Add(actions.aRdr, actions.aRelay, actions.duration, ActionForm.GetActionStr(actions.actionType), tag_id, tag_type);
                                            }


                                            communication.EnableOutputRelay(actions.aRdr, actions.aRelay);

                                            if (tag_type == AW_API_NET.APIConsts.ACCESS_TAG)
                                            {
                                                string name;
                                                string firstname = "";
                                                string lastname = "";
                                                string department = "";
                                                DateTime time = DateTime.Now;

                                                if (employee != null)
                                                {
                                                    firstname = employee.FirstName;
                                                    lastname = employee.LastName;
                                                    department = employee.Department;
                                                }

                                                name = string.Format("{0} {1}", firstname, lastname).Trim();


                                                CultureInfo ci = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                                                ci.DateTimeFormat.DateSeparator = "-";

                                                ListViewItem listItemA = new ListViewItem("");
                                                listItemA.SubItems.Add(tag_id.ToString());   //tagid	 
                                                listItemA.SubItems.Add(name);   //tag type
                                                listItemA.SubItems.Add(loc);   //location
                                                listItemA.SubItems.Add(ActionForm.GetActionStr(actions.actionType));   //event description
                                                listItemA.SubItems.Add(time.ToString(ci));    //time
#if SANI
                                                listItemA.SubItems.Add("");
                                                listItemA.SubItems.Add("");
#endif
                                                lvi[virtualListIndex++] = listItemA;

                                                //SaveTraffic(tag_id.ToString(), firstname, lastname, department, parkNum, loc, "Relay Open", tag.Action, DateTime.Now, (byte)tag.Type);
                                                TrafficQuery.Insert(new Traffic((int)tag_id, (TagType)tag_type, firstname, lastname, department, "Action", ActionForm.GetActionStr(actions.actionType), zone, loc, time));

                                                PlaySound(1);
                                            }
                                            else
                                            {

                                            }
                                            Thread.Sleep(100); //chg 500 -> 100
                                            break;

                                        case MainForm.DISPLAY_TAG_MOVING_DIRECTION:
                                            break;

                                        case MainForm.EVENT_INPUT_01:
                                            if (input01)
                                            {
                                                if (actions.duration == 0)
                                                {
                                                    Console.WriteLine("Monitor Input 01 until inactive");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Monitor Input 01 for {0} seconds", actions.duration);
                                                }
                                            }
                                            break;

                                        case MainForm.EVENT_INPUT_02:
                                            if (input02)
                                            {
                                                if (actions.duration == 0)
                                                {
                                                    Console.WriteLine("Monitor Input 02 until inactive");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Monitor Input 02 for {0} seconds", actions.duration);
                                                }
                                            }
                                            break;

                                        case MainForm.EVENT_CALL_TAG_ACCESS:
                                            Thread.Sleep(100);
                                            communication.CallTag(rdr, fgen, 0, "ACC", "All");
                                            break;

                                        case MainForm.EVENT_CALL_TAG_ASSET:
                                            Thread.Sleep(100);
                                            communication.CallTag(rdr, fgen, 0, "AST", "All");
                                            break;

                                        case MainForm.EVENT_CALL_TAG_INVENTORY:
                                            Thread.Sleep(100);
                                            communication.CallTag(rdr, fgen, 0, "INV", "All");
                                            break;

                                        case MainForm.EVENT_EMAIL_ACTIVITY:
                                            if (evt != INPUT_DETECTED) // e-mail only at timeout
                                            {
                                                Console.WriteLine("Send e-mail for {0} on {1}", ActionForm.GetEventStr(eAction.eType), loc);
                                            }
                                            break;
                                    }
                                }

                                PopulateHistoryTable(tag_id, tag_type, parkNum, ActionForm.GetEventStr(eAction.eType));
                                SaveTraffic(tag_id_str, pName, null, dept, parkNum, loc, "Action", ActionForm.GetEventStr(eAction.eType), DateTime.Now, Convert.ToUInt32(tag_type));
                            }
                        }
                    }
                }
            }

            if (handleEventActionThread != null)
                handleEventActionThread = null;
        }
        #endregion

        #region listView_DrawItem()
        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            
        }
        #endregion

        #region listView_DrawSubItem()
        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Font font = new Font(listView.Font, FontStyle.Regular);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            int color = 0;
            if (e.ItemIndex % 2 == 0)
            {
                e.Graphics.FillRectangle(Brushes.LightSteelBlue, e.Bounds); //LightCyan
                color = 1; //blue
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.LightYellow, e.Bounds);
                color = 2; //yellow
            }

            if (e.ColumnIndex == 0)
            {
                Image img = null;;
               
                if (color == 1)
                    e.Graphics.DrawString(e.Item.Text, font, Brushes.LightSteelBlue, e.Bounds, sf);  //LightCyan
                else
                    e.Graphics.DrawString(e.Item.Text, font, Brushes.LightYellow, e.Bounds, sf);

                if (e.Item.SubItems[0].Text == "IN")
                {                    
                    img = SmallImageList.Images[0];
                }
                else if (e.Item.SubItems[0].Text == "VA")
                {
                    img = SmallImageList.Images[1];
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
               e.Graphics.DrawString(e.SubItem.Text, font, Brushes.Navy, e.Bounds, sf);

        }
        #endregion

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

                // Draw the standard header background.
                e.DrawBackground();

                // Draw the header text.
                if (e.ColumnIndex == 0)
                {
                    using (Font headerFont = new Font("Helvetic", 9, FontStyle.Regular))
                    {
                        sf.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Purple, e.Bounds, sf);
                    }
                }
                else
                {
                    using (Font headerFont = new Font("Helvetic", 13, FontStyle.Regular))
                    {
                        e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Purple, e.Bounds, sf);
                    }
                }
            }
        }

        private void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = lvi[e.ItemIndex];            
        }
    }//Form2

	#region cmdStruct
	[StructLayout(LayoutKind.Sequential)]
	public struct cmdStruct
	{
		public ushort rdrID;        
		public uint tagID;
		public string tagType;
		public bool led;
		public bool speaker;
		public ushort retry;
		public ushort cmd;
		public ushort pID;
		public ushort type;
		public DateTime timeStamp;
		public DateTime waitTime;
	}
	#endregion

	#region CmdCollectionClass
	[Serializable]
	public class CmdCollectionClass : CollectionBase
	{
		public cmdStruct this [int index]
		{
			get { return (cmdStruct) List[index];}
			set { List[index] = value;}
		}

		public int GetRetryCount (int index)
		{
			return(this[index].retry);
		}

		public int Add(cmdStruct cmd)
		{
			return (List.Add(cmd));
		}

		public void Insert(int index, cmdStruct cmd)
		{
			List.Insert(index, cmd);
		}

		public void Remove(cmdStruct cmd)
		{
			List.Remove(cmd);
		}

		public void RemoveFrom(int index)
		{
		   List.Remove(List[index]);
		}

		public bool Contain(cmdStruct cmd)
		{
			return(List.Contains(cmd));
		}

		public short IsEqual(cmdStruct cmd)
		{
		   for (short i = 0; i < this.Count; i++)
           {
              if ((this[i].rdrID == cmd.rdrID) &&
				  (this[i].tagID == cmd.tagID) &&
				  (this[i].tagType == cmd.tagType) &&
				  (this[i].cmd == cmd.cmd))
				  return (i);   //found it match
		   }//for loop
			
		   return (-1);   //did not find
		}

		public bool IsExpired(int index)
		{
		   DateTime time = new DateTime(this[index].timeStamp.Ticks);
		   DateTime timeNow = DateTime.Now; 
		   int tSec = time.Hour*3600 + time.Minute*60 + time.Second;
		   int tSecNow = timeNow.Hour*3600 + timeNow.Minute*60 + timeNow.Second;
		   if (tSecNow > (tSec + 21))  //21 sec to quit for sending this command
		   {
		      //Console.WriteLine("EXPIRED - timeStamp Start: " + time.ToLongTimeString() + "   timeStamp Now: " + DateTime.Now.ToLongTimeString() + "  Index = " + index);
		      return (true);
		   }
		   else
		   {
		      //Console.WriteLine("timeStamp Start: " + time.ToLongTimeString() + "   timeStamp Now: " + DateTime.Now.ToLongTimeString() + "  Index = " + index);
		      return (false);
		   }
		}

		public bool WaitTimeIsExpired(int index)
		{
		   DateTime time = new DateTime(this[index].timeStamp.Ticks);
		   DateTime timeNow = DateTime.Now; 
		   int tSec = time.Hour*3600 + time.Minute*60 + time.Second;
		   int tSecNow = timeNow.Hour*3600 + timeNow.Minute*60 + timeNow.Second;
		   if (tSecNow > (tSec + 6))  //wait to send query command
		   {
		      //Console.WriteLine("EXPIRED - timeStamp Start: " + time.ToLongTimeString() + "   timeStamp Now: " + DateTime.Now.ToLongTimeString() + "  Index = " + index);
		      return (true);
		   }
		   else
		   {
		      //Console.WriteLine("timeStamp Start: " + time.ToLongTimeString() + "   timeStamp Now: " + DateTime.Now.ToLongTimeString() + "  Index = " + index);
		      return (false);
		   }
		}
	}
	#endregion

	#region commandStruct
	/*[StructLayout(LayoutKind.Sequential)]
		public struct commandStruct
		{
			public ushort cmdID;        
			public bool sent;
			public ushort relay;
			public ushort counter01;
			public ushort counter02;
			public ushort rdrID; 
		}*/
	#endregion

	#region CommandCollectionClass
		/*[Serializable]
		public class CommandCollectionClass : CollectionBase
		{
			public commandStruct this [int index]
			{
				get { return (commandStruct) List[index];}
				set { List[index] = value;}
			}

			public int Add(commandStruct cmd)
			{
				return (List.Add(cmd));
			}

			public void Insert(int index, commandStruct cmd)
			{
				List.Insert(index, cmd);
			}

			public void Remove(commandStruct cmd)
			{
				List.Remove(cmd);
			}

			public void RemoveFrom(int index)
			{
			List.Remove(List[index]);
			}

			public bool Contain(commandStruct cmd)
			{
				return(List.Contains(cmd));
			}

			public void Replace(int index, commandStruct cmd)
			{
				if (index >= 0) this[index] = cmd;
			}

			public void UpdateCounter01(int index, ushort count)
			{
				commandStruct command = new commandStruct();
				command = this[index];
				command.counter01 = count;
				this[index] = command;
			}

			public void UpdateCounter02(int index, ushort count)
			{
				commandStruct command = new cmdStruct();
				command = this[index];
				command.counter02 = count;
				this[index] = command;
			}

			public void UpdateSent(int index, bool b)
			{
				commandStruct command = new commandStruct();
				command = this[index];
				command.sent = b;
				this[index] = command;
			}

			public bool GetStat(int index)
			{
				return (this[index].sent);
			}
		}*/
	#endregion

	#region dataStruct
	    [StructLayout(LayoutKind.Sequential)]
		public struct dataStruct
		{
			public uint tagID;
			public byte tagType;
			public bool validTag;
			public ushort eventID;
			public ushort rdrID;
			public ushort fgenID;
			public bool update;
		}
	    #endregion

	#region DataCollectionClass
	    [Serializable]
	    public class DataCollectionClass : CollectionBase
	    {
			public dataStruct this [int index]
			{
				get { return (dataStruct) List[index];}
				set { List[index] = value;}
			}

			public int Add(dataStruct data)
			{
				return (List.Add(data));
			}

			public void Insert(int index, dataStruct data)
			{
				List.Insert(index, data);
			}

			public void Remove(dataStruct data)
			{
				List.Remove(data);
			}

			public void RemoveFrom(int index)
			{
				List.Remove(List[index]);
			}
		}
	    #endregion

    #region relayStruct
	    [StructLayout(LayoutKind.Sequential)]
		public struct relayStruct
		{
			public DateTime timeStamp;
			public byte tagType;
			public ushort rdrID;
			public ushort relayID;
			public bool open;
		}
	    #endregion

	#region tagDisplayInfoStruct
	    [StructLayout(LayoutKind.Sequential)]
		public struct tagDisplayInfoStruct
		{
			public DateTime timeStamp;
			public string str;
			public string compStr;
		}
	    #endregion

	#region tagDisplayCollectionClass
	    [Serializable]
	    public class tagDisplayCollectionClass : CollectionBase
	    {
			public tagDisplayInfoStruct this [int index]
			{
				get { return (tagDisplayInfoStruct) List[index];}
				set { List[index] = value;}
			}

			public int Add(tagDisplayInfoStruct tagDisplay)
			{
				return (List.Add(tagDisplay));
			}

			public void Insert(int index, tagDisplayInfoStruct tagDisplay)
			{
				List.Insert(index, tagDisplay);
			}

			public void Remove(tagDisplayInfoStruct tagDisplay)
			{
				List.Remove(tagDisplay);
			}

			public void RemoveFrom(int index)
			{
				List.Remove(List[index]);
			}

			public int Exits(tagDisplayInfoStruct tagDisplay)
			{
			    for (int i = 0; i < this.Count; i++)
				{
                   if (this[i].str == tagDisplay.str) 
					  return i;
				}
				return -1;
			}

			public int Exits(string  s)
			{
				for (int i = 0; i < this.Count; i++)
				{
				   if (this[i].compStr == s) 
					  return i;
				}
				return -1;
			}

			public bool IsExpired(int index)
		    {
				DateTime time = new DateTime(this[index].timeStamp.Ticks);
				DateTime timeNow = DateTime.Now;
                TimeSpan span = timeNow - time;
                // EDC   April 2010
                //       date was not taken into account for timediff calculation, so at midnight-->boom!
                //int tSec = time.Hour * 3600 + time.Minute * 60 + time.Second;
                //int tSecNow = timeNow.Hour * 3600 + timeNow.Minute * 60 + timeNow.Second;
                //if (tSecNow > (tSec + MainForm.tagDupExpTime))	//tagDupExpTime sec for tag to expired from same group
                if (span.TotalSeconds > MainForm.tagDupExpTime)	//tagDupExpTime sec for tag to expired from same group
				{
					Console.WriteLine("EXPIRED - timeStamp Start: " + time.ToLongTimeString() + "   timeStamp Now: " + DateTime.Now.ToLongTimeString() + "  Index = " + index + "  " + this[index].str);
					return (true);
				}
				else
				{
					Console.WriteLine("NOT EXPIRED - timeStamp Start: " + time.ToLongTimeString() + "   timeStamp Now: " + DateTime.Now.ToLongTimeString() + "  Index = " + index + "  " + this[index].str);
					return (false);
				}
		    }

		}
	    #endregion

	#region RelayCollectionClass
	    [Serializable]
	    public class RelayCollectionClass : CollectionBase
	    {
			public relayStruct this [int index]
			{
				get { return (relayStruct) List[index];}
				set { List[index] = value;}
			}

			public int Add(relayStruct data)
			{
				return (List.Add(data));
			}

			public void Insert(int index, relayStruct data)
			{
				List.Insert(index, data);
			}

			public void Remove(relayStruct data)
			{
				List.Remove(data);
			}

			public void RemoveFrom(int index)
			{
				List.Remove(List[index]);
			}

			public bool Exits(relayStruct relay)
			{
			    for (int i = 0; i < this.Count; i++)
				{
                   if ((this[i].rdrID == relay.rdrID) &&
                       (this[i].relayID == relay.relayID) &&
				       (this[i].open == relay.open)) 
					  return true;
				}
				return false;
			}
		}
	    #endregion

	#region ReaderStatusStruct
	[StructLayout(LayoutKind.Sequential)]
	public struct ReaderStatusStruct
	{
		public DateTime timeStamp;
		public string ip;
		public int rdrID;
		public int hostID;
		public string rdrStatus;
		public string netStatus;
		public int counter;
		public int nextCmd;
		public bool displayed;
	}
	#endregion

	#region ReaderPowerupStruct
	[StructLayout(LayoutKind.Sequential)]
	public struct ReaderPowerupStruct
	{
		public string ip;
		public bool online;
		public ushort counter;
	}
	#endregion

	#region PowerupStruct
	[StructLayout(LayoutKind.Sequential)]
	public struct PowerupStruct
	{
		public string ip;
		public DateTime timestamp;
	}
	#endregion

    #region actionItemStruct
    [StructLayout(LayoutKind.Sequential)]
    public struct actionItemStruct
    {
        public ushort aRdr;
        public ushort aRelay;
        public ushort duration;
        public int actionType;
        public bool started;

        public actionItemStruct(ushort actionRdr, ushort actionRel, ushort dur, int actType)
        {
            aRdr = actionRdr;
            aRelay = actionRel;
            duration = dur;
            actionType = actType;
            started = false;
        }
    }
    #endregion

    #region actionStruct
    [StructLayout(LayoutKind.Sequential)]
    public struct actionStruct
    {
        public ushort eRdr;
        public ushort eFGen;
        public int eType;
        public List<actionItemStruct> actions;
        private string description;

        public actionStruct(ushort eventRdr, ushort fgen, int evtType, string description)
        {
            actions = new List<actionItemStruct>();
            eRdr = eventRdr;
            eFGen = fgen;
            eType = evtType;
            this.description = description;
        }

        public void AddAction(ushort actionRdr, ushort actionRel, ushort dur, int actType)
        {
            actionItemStruct aItem = new actionItemStruct(actionRdr, actionRel, dur, actType);
            actions.Add(aItem);
        }

        public string Description { get { return description; } }
    }
    #endregion

    #region tagGCStruct
    [StructLayout(LayoutKind.Sequential)]
    public struct tagGCStruct
    {
        public string tag;
        public ushort GC;
    }
    #endregion

    #region tagQueueStruct
    [StructLayout(LayoutKind.Sequential)]
    public struct tagQueueStruct
    {
        public string id;
        public string type;
        public ushort retry;
        public DateTime timestamp;
    }
    #endregion

}//namespace
