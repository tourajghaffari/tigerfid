using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using ActiveWaveAPIClass;

namespace TagDetector
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	///
 
	
	[StructLayout(LayoutKind.Sequential)]
	public struct tagStruct
	{
		public uint	id;
		public byte type;
		public ushort reader;         
		public ushort fGen;
		public byte groupID;
		public short RSSI;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct dataStruct
	{
		public string fileStr;
		public string listStr;
	}
	
	//[Serializable]
	
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox listBox1;
		public AWI_APIClass api  = new AWI_APIClass();
		private ushort pktID = 0;
		private ushort hostID = 1;
		private TagCollectionClass tagCollection = new TagCollectionClass();
		private DataCollectionClass dataCollection = new DataCollectionClass();
		private ListDisplayCollectionClass listDisplayCollection = new ListDisplayCollectionClass();
		//const string DING_SOUND = "\\IFLASH\\ActiveWave\\TagDetector\\ding.wav";
		//const string ALARM_SOUND = "\\IFLASH\\ActiveWave\\TagDetector\\alarm.wav";
		const string DING_SOUND = "\\Program Files\\ActiveWave\\TagDetector\\ding.wav";
		const string ALARM_SOUND = "\\Program Files\\ActiveWave\\TagDetector\\alarm.wav";
		const string NOTIFY_SOUND = "\\Program Files\\ActiveWave\\TagDetector\\notify.wav";
		private FileStream fs = null;
		private string info;
		private FileInfo fi = null;
		//private string filePath = @"\IFLASH\Active Wave\Tag Detector\TagHistory.txt";
		//private string filePath = @"\IFLASH\ActiveWave\TagDetector\";
		
		//private string filePath = @"\Program Files\ActiveWave\TagDetector\";
		private string filePath = @"\Program Files\TagDetector\";
		
		private string sdCardPath = @"\SD Card\";
		private static bool recording = false;
		private ushort fileSize = 10;
		private Object fileLock = new Object();
		private Object syncLock = new Object();
		private object listLock = new object();
		private bool fileExists = false;
		private ushort readerID = 0;
		private string fName;
		public System.Windows.Forms.Timer timer1;
		private bool disableRelay = false;
		private ushort counter = 0;
		private static bool connection = false;
		//private static bool pause = false;
		private static bool eRelay = false;
		private static bool setEnable = false;
		private static bool setDisable = false;
		private bool autoResetMode = false;
		private uint resetCounter = 0;
		private bool resetInputs  = false;
		private static bool autoReset = false;
		private System.Windows.Forms.Button CallButton;
		private System.Windows.Forms.CheckBox TagIDCheckBox;
		private System.Windows.Forms.TextBox TagIDTextBox;
		private System.Windows.Forms.CheckBox LEDCheckBox;
		public System.Windows.Forms.TextBox RSSITextBox;
		public System.Windows.Forms.ProgressBar RSSIProgressBar;
		private uint port = 4;  //default setting
		private bool portOpen = false;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.Timer timer2;
		private bool enableFlag = false;
		private System.Windows.Forms.Button IncFSButton;
		private System.Windows.Forms.Button DecFSButton;
		private System.Windows.Forms.Button MinFSButton;
		private System.Windows.Forms.Button MaxFSButton;
		private System.Windows.Forms.TextBox FSTextBox;
		Thread displayThread = new Thread(new ThreadStart(ThreadProc));
		private static Form1 thisForm = new Form1(true);
		//private bool fsValueChanged = false;
		private bool fsTextBoxGotFocus = false;
		private System.Windows.Forms.Button SetFSButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox LongRangeCheckBox;
		private bool sendCallTag = false;
		private ushort myCounter = 0;
		private System.Windows.Forms.ListBox listBox3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button OpenSocketButton;
		private int maxMyCounter = 0;
		private System.Windows.Forms.Button CloseSocketButton;
		public System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.TextBox IPTextBox;
		private System.Windows.Forms.CheckBox IPCheckBox;
		private System.Windows.Forms.TextBox IPTextBox1;
		private System.Windows.Forms.CheckBox IPCheckBox1;
		private System.Windows.Forms.TextBox RdrIDTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button EnableButton;
		private System.Windows.Forms.Button DisableButton;
		private System.Windows.Forms.Button QueryButton;
		private System.Windows.Forms.Button EnableReaderButton;
		private System.Windows.Forms.Button DisableReaderButton;
		private System.Windows.Forms.Button QueryReaderButton;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button OpenPortButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox PortNumTextBox;
		private System.Windows.Forms.Button ResetReaderButton;
		private Socket sock = null;

		
		//int count = 1;

		public Form1(bool b)
		{}

		
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			port = ReadComPortNum();
			//displayThread.Start();
			thisForm = this;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.timer1 = new System.Windows.Forms.Timer();
			this.CallButton = new System.Windows.Forms.Button();
			this.TagIDCheckBox = new System.Windows.Forms.CheckBox();
			this.TagIDTextBox = new System.Windows.Forms.TextBox();
			this.LEDCheckBox = new System.Windows.Forms.CheckBox();
			this.RSSIProgressBar = new System.Windows.Forms.ProgressBar();
			this.RSSITextBox = new System.Windows.Forms.TextBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.IncFSButton = new System.Windows.Forms.Button();
			this.DecFSButton = new System.Windows.Forms.Button();
			this.FSTextBox = new System.Windows.Forms.TextBox();
			this.MinFSButton = new System.Windows.Forms.Button();
			this.MaxFSButton = new System.Windows.Forms.Button();
			this.timer2 = new System.Windows.Forms.Timer();
			this.SetFSButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.LongRangeCheckBox = new System.Windows.Forms.CheckBox();
			this.listBox3 = new System.Windows.Forms.ListBox();
			this.button2 = new System.Windows.Forms.Button();
			this.OpenSocketButton = new System.Windows.Forms.Button();
			this.CloseSocketButton = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.IPTextBox = new System.Windows.Forms.TextBox();
			this.IPCheckBox = new System.Windows.Forms.CheckBox();
			this.IPTextBox1 = new System.Windows.Forms.TextBox();
			this.IPCheckBox1 = new System.Windows.Forms.CheckBox();
			this.RdrIDTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.QueryButton = new System.Windows.Forms.Button();
			this.EnableButton = new System.Windows.Forms.Button();
			this.DisableButton = new System.Windows.Forms.Button();
			this.EnableReaderButton = new System.Windows.Forms.Button();
			this.DisableReaderButton = new System.Windows.Forms.Button();
			this.QueryReaderButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.OpenPortButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.PortNumTextBox = new System.Windows.Forms.TextBox();
			this.ResetReaderButton = new System.Windows.Forms.Button();
			// 
			// listBox1
			// 
			this.listBox1.Enabled = false;
			this.listBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
			this.listBox1.Location = new System.Drawing.Point(268, 30);
			this.listBox1.Size = new System.Drawing.Size(268, 173);
			// 
			// timer1
			// 
			this.timer1.Interval = 5000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// CallButton
			// 
			this.CallButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.CallButton.Location = new System.Drawing.Point(8, 262);
			this.CallButton.Size = new System.Drawing.Size(98, 32);
			this.CallButton.Text = "Call Tag";
			this.CallButton.Click += new System.EventHandler(this.CallButton_Click);
			// 
			// TagIDCheckBox
			// 
			this.TagIDCheckBox.Location = new System.Drawing.Point(8, 38);
			this.TagIDCheckBox.Size = new System.Drawing.Size(62, 20);
			this.TagIDCheckBox.Text = "Tag ID";
			this.TagIDCheckBox.Click += new System.EventHandler(this.TagIDCheckBox_Click);
			// 
			// TagIDTextBox
			// 
			this.TagIDTextBox.Location = new System.Drawing.Point(74, 38);
			this.TagIDTextBox.ReadOnly = true;
			this.TagIDTextBox.Size = new System.Drawing.Size(64, 22);
			this.TagIDTextBox.Text = "";
			// 
			// LEDCheckBox
			// 
			this.LEDCheckBox.Location = new System.Drawing.Point(212, 112);
			this.LEDCheckBox.Size = new System.Drawing.Size(52, 20);
			this.LEDCheckBox.Text = "LED";
			// 
			// RSSIProgressBar
			// 
			this.RSSIProgressBar.Location = new System.Drawing.Point(324, 216);
			this.RSSIProgressBar.Maximum = 255;
			this.RSSIProgressBar.Size = new System.Drawing.Size(170, 26);
			// 
			// RSSITextBox
			// 
			this.RSSITextBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular);
			this.RSSITextBox.Location = new System.Drawing.Point(498, 216);
			this.RSSITextBox.ReadOnly = true;
			this.RSSITextBox.Size = new System.Drawing.Size(40, 26);
			this.RSSITextBox.Text = "0";
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.menuItem7);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem3);
			this.menuItem1.MenuItems.Add(this.menuItem4);
			this.menuItem1.Text = "System";
			// 
			// menuItem3
			// 
			this.menuItem3.MenuItems.Add(this.menuItem5);
			this.menuItem3.MenuItems.Add(this.menuItem6);
			this.menuItem3.Text = "Set Communication Type";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click_1);
			// 
			// menuItem5
			// 
			this.menuItem5.Text = "NETWORK";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click_1);
			// 
			// menuItem6
			// 
			this.menuItem6.Text = "RS232";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Text = "Close";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.MenuItems.Add(this.menuItem8);
			this.menuItem7.Text = "Help";
			// 
			// menuItem8
			// 
			this.menuItem8.Text = "About Parking Lot";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// IncFSButton
			// 
			this.IncFSButton.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular);
			this.IncFSButton.Location = new System.Drawing.Point(52, 156);
			this.IncFSButton.Size = new System.Drawing.Size(40, 20);
			this.IncFSButton.Text = "+";
			this.IncFSButton.Click += new System.EventHandler(this.IncFSButton_Click);
			// 
			// DecFSButton
			// 
			this.DecFSButton.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular);
			this.DecFSButton.Location = new System.Drawing.Point(8, 156);
			this.DecFSButton.Size = new System.Drawing.Size(40, 20);
			this.DecFSButton.Text = "-";
			this.DecFSButton.Click += new System.EventHandler(this.DecFSButton_Click);
			// 
			// FSTextBox
			// 
			this.FSTextBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular);
			this.FSTextBox.Location = new System.Drawing.Point(154, 134);
			this.FSTextBox.Size = new System.Drawing.Size(40, 26);
			this.FSTextBox.Text = "0";
			this.FSTextBox.LostFocus += new System.EventHandler(this.FSTextBox_LostFocus);
			this.FSTextBox.GotFocus += new System.EventHandler(this.FSTextBox_GotFocus);
			this.FSTextBox.TextChanged += new System.EventHandler(this.FSTextBox_TextChanged);
			// 
			// MinFSButton
			// 
			this.MinFSButton.Location = new System.Drawing.Point(8, 132);
			this.MinFSButton.Size = new System.Drawing.Size(40, 20);
			this.MinFSButton.Text = "Min";
			this.MinFSButton.Click += new System.EventHandler(this.MinFSButton_Click);
			// 
			// MaxFSButton
			// 
			this.MaxFSButton.Location = new System.Drawing.Point(52, 132);
			this.MaxFSButton.Size = new System.Drawing.Size(40, 20);
			this.MaxFSButton.Text = "Max";
			this.MaxFSButton.Click += new System.EventHandler(this.MaxFSButton_Click);
			// 
			// timer2
			// 
			this.timer2.Interval = 7000;
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// SetFSButton
			// 
			this.SetFSButton.Location = new System.Drawing.Point(110, 136);
			this.SetFSButton.Size = new System.Drawing.Size(40, 24);
			this.SetFSButton.Text = "Set";
			this.SetFSButton.Click += new System.EventHandler(this.SetFSButton_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
			this.label1.Location = new System.Drawing.Point(8, 112);
			this.label1.Size = new System.Drawing.Size(84, 16);
			this.label1.Text = "Field Strength";
			// 
			// LongRangeCheckBox
			// 
			this.LongRangeCheckBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
			this.LongRangeCheckBox.Location = new System.Drawing.Point(110, 112);
			this.LongRangeCheckBox.Size = new System.Drawing.Size(86, 20);
			this.LongRangeCheckBox.Text = "Long Range";
			this.LongRangeCheckBox.Click += new System.EventHandler(this.LongRangeCheckBox_Click);
			this.LongRangeCheckBox.CheckStateChanged += new System.EventHandler(this.LongRangeCheckBox_CheckStateChanged);
			// 
			// listBox3
			// 
			this.listBox3.Location = new System.Drawing.Point(228, 252);
			this.listBox3.Size = new System.Drawing.Size(312, 142);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(334, 396);
			this.button2.Size = new System.Drawing.Size(106, 26);
			this.button2.Text = "Clear Msg List";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// OpenSocketButton
			// 
			this.OpenSocketButton.Enabled = false;
			this.OpenSocketButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.OpenSocketButton.Location = new System.Drawing.Point(8, 186);
			this.OpenSocketButton.Size = new System.Drawing.Size(98, 32);
			this.OpenSocketButton.Text = "OpenSocket";
			this.OpenSocketButton.Click += new System.EventHandler(this.OpenSocketButton_Click);
			// 
			// CloseSocketButton
			// 
			this.CloseSocketButton.Enabled = false;
			this.CloseSocketButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.CloseSocketButton.Location = new System.Drawing.Point(8, 222);
			this.CloseSocketButton.Size = new System.Drawing.Size(98, 32);
			this.CloseSocketButton.Text = "CloseSocket";
			this.CloseSocketButton.Click += new System.EventHandler(this.CloseSocketButton_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(268, 214);
			this.button1.Size = new System.Drawing.Size(52, 30);
			this.button1.Text = "Clear";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(326, 216);
			this.progressBar1.Maximum = 255;
			this.progressBar1.Size = new System.Drawing.Size(168, 26);
			// 
			// IPTextBox
			// 
			this.IPTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.IPTextBox.Location = new System.Drawing.Point(110, 194);
			this.IPTextBox.Size = new System.Drawing.Size(116, 23);
			this.IPTextBox.Text = "";
			// 
			// IPCheckBox
			// 
			this.IPCheckBox.Location = new System.Drawing.Point(230, 196);
			this.IPCheckBox.Size = new System.Drawing.Size(18, 20);
			// 
			// IPTextBox1
			// 
			this.IPTextBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.IPTextBox1.Location = new System.Drawing.Point(110, 222);
			this.IPTextBox1.Size = new System.Drawing.Size(116, 23);
			this.IPTextBox1.Text = "";
			// 
			// IPCheckBox1
			// 
			this.IPCheckBox1.Location = new System.Drawing.Point(230, 226);
			this.IPCheckBox1.Size = new System.Drawing.Size(18, 20);
			// 
			// RdrIDTextBox
			// 
			this.RdrIDTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.RdrIDTextBox.Location = new System.Drawing.Point(212, 38);
			this.RdrIDTextBox.Size = new System.Drawing.Size(52, 23);
			this.RdrIDTextBox.Text = "0";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.label3.Location = new System.Drawing.Point(164, 42);
			this.label3.Size = new System.Drawing.Size(44, 16);
			this.label3.Text = "Rdr ID";
			// 
			// QueryButton
			// 
			this.QueryButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.QueryButton.Location = new System.Drawing.Point(8, 300);
			this.QueryButton.Size = new System.Drawing.Size(98, 32);
			this.QueryButton.Text = "Query Tag";
			this.QueryButton.Click += new System.EventHandler(this.QueryButton_Click);
			// 
			// EnableButton
			// 
			this.EnableButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.EnableButton.Location = new System.Drawing.Point(8, 340);
			this.EnableButton.Size = new System.Drawing.Size(98, 32);
			this.EnableButton.Text = "Enable Tag";
			this.EnableButton.Click += new System.EventHandler(this.EnableButton_Click);
			// 
			// DisableButton
			// 
			this.DisableButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.DisableButton.Location = new System.Drawing.Point(8, 380);
			this.DisableButton.Size = new System.Drawing.Size(98, 32);
			this.DisableButton.Text = "Disable Tag";
			this.DisableButton.Click += new System.EventHandler(this.DisableButton_Click);
			// 
			// EnableReaderButton
			// 
			this.EnableReaderButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.EnableReaderButton.Location = new System.Drawing.Point(118, 302);
			this.EnableReaderButton.Size = new System.Drawing.Size(100, 32);
			this.EnableReaderButton.Text = "Enable Reader";
			this.EnableReaderButton.Click += new System.EventHandler(this.EnableReaderButton_Click);
			// 
			// DisableReaderButton
			// 
			this.DisableReaderButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.DisableReaderButton.Location = new System.Drawing.Point(118, 340);
			this.DisableReaderButton.Size = new System.Drawing.Size(102, 32);
			this.DisableReaderButton.Text = "Disable Reader";
			this.DisableReaderButton.Click += new System.EventHandler(this.DisableReaderButton_Click);
			// 
			// QueryReaderButton
			// 
			this.QueryReaderButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.QueryReaderButton.Location = new System.Drawing.Point(118, 380);
			this.QueryReaderButton.Size = new System.Drawing.Size(102, 32);
			this.QueryReaderButton.Text = "Query Reader";
			this.QueryReaderButton.Click += new System.EventHandler(this.QueryReaderButton_Click);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
			this.label4.Location = new System.Drawing.Point(112, 176);
			this.label4.Size = new System.Drawing.Size(84, 16);
			this.label4.Text = "IP Address";
			// 
			// OpenPortButton
			// 
			this.OpenPortButton.Enabled = false;
			this.OpenPortButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.OpenPortButton.Location = new System.Drawing.Point(8, 68);
			this.OpenPortButton.Size = new System.Drawing.Size(116, 32);
			this.OpenPortButton.Text = "Open RS232 Port";
			this.OpenPortButton.Click += new System.EventHandler(this.OpenPortButton_Click);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.label5.Location = new System.Drawing.Point(146, 76);
			this.label5.Size = new System.Drawing.Size(62, 16);
			this.label5.Text = "Port Num:";
			// 
			// PortNumTextBox
			// 
			this.PortNumTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.PortNumTextBox.Location = new System.Drawing.Point(212, 72);
			this.PortNumTextBox.Size = new System.Drawing.Size(52, 23);
			this.PortNumTextBox.Text = "1";
			// 
			// ResetReaderButton
			// 
			this.ResetReaderButton.Enabled = false;
			this.ResetReaderButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.ResetReaderButton.Location = new System.Drawing.Point(118, 262);
			this.ResetReaderButton.Size = new System.Drawing.Size(100, 32);
			this.ResetReaderButton.Text = "Reset Reader";
			this.ResetReaderButton.Click += new System.EventHandler(this.ResetReaderButton_Click);
			// 
			// Form1
			// 
			this.BackColor = System.Drawing.Color.LightGray;
			this.ClientSize = new System.Drawing.Size(550, 425);
			this.ControlBox = false;
			this.Controls.Add(this.ResetReaderButton);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.PortNumTextBox);
			this.Controls.Add(this.OpenPortButton);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.QueryReaderButton);
			this.Controls.Add(this.DisableReaderButton);
			this.Controls.Add(this.EnableReaderButton);
			this.Controls.Add(this.DisableButton);
			this.Controls.Add(this.EnableButton);
			this.Controls.Add(this.QueryButton);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.RdrIDTextBox);
			this.Controls.Add(this.IPCheckBox1);
			this.Controls.Add(this.IPTextBox1);
			this.Controls.Add(this.IPCheckBox);
			this.Controls.Add(this.IPTextBox);
			this.Controls.Add(this.CloseSocketButton);
			this.Controls.Add(this.OpenSocketButton);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.listBox3);
			this.Controls.Add(this.LongRangeCheckBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.SetFSButton);
			this.Controls.Add(this.MaxFSButton);
			this.Controls.Add(this.MinFSButton);
			this.Controls.Add(this.FSTextBox);
			this.Controls.Add(this.DecFSButton);
			this.Controls.Add(this.IncFSButton);
			this.Controls.Add(this.RSSITextBox);
			this.Controls.Add(this.RSSIProgressBar);
			this.Controls.Add(this.LEDCheckBox);
			this.Controls.Add(this.TagIDTextBox);
			this.Controls.Add(this.TagIDCheckBox);
			this.Controls.Add(this.CallButton);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.progressBar1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.Text = "CE API V5.1.0 Example";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.Load += new System.EventHandler(this.Form1_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new Form1());
		}

		public void PlaySound(ushort type)
		{
			if (type == 1)
				Sound.PlaySound( DING_SOUND, IntPtr.Zero, Sound.PlaySoundFlags.SND_FILENAME | Sound.PlaySoundFlags.SND_ASYNC );
			else if (type == 2)
				Sound.PlaySound( ALARM_SOUND, IntPtr.Zero, Sound.PlaySoundFlags.SND_FILENAME | Sound.PlaySoundFlags.SND_ASYNC );
			else if (type == 3)
				Sound.PlaySound( NOTIFY_SOUND, IntPtr.Zero, Sound.PlaySoundFlags.SND_FILENAME | Sound.PlaySoundFlags.SND_ASYNC );
		}

		private uint ReadComPortNum()
		{
			fi = new FileInfo(filePath+"comport.txt");
			if (fi.Exists)
			{
				try
				{
					fs = fi.Open(FileMode.Open);
				}
				catch
				{
					return (0);
				}

				uint portNum = 0;
				lock(fileLock)
				{
					portNum = (uint)(fs.ReadByte() - 48); //Write(encodedBytes, 0, encodedBytes.Length);
					fs.Close();
				}

				return (portNum);
			}
			else
				return (0);
		}

		public bool WriteComPortNum(string portNum)
		{
			fi = new FileInfo(filePath+"comport.txt");
			if (fi.Exists)
			{
				try
				{
					fs = fi.Open(FileMode.Open);
				}
				catch
				{
					return (false);
				}

				UTF8Encoding utf8 = new UTF8Encoding();
				Byte[] encodedBytes = utf8.GetBytes(portNum);
				lock(fileLock)
				{
					fs.Write(encodedBytes, 0, encodedBytes.Length);
					fs.Flush();
					fs.Close();
				}

				return (true);
			}
			else
				return (false);
		}

		private bool CreateTextFile(ushort rdrID)
		{
			fName = rdrID + "_";
			fName += DateTime.Now.ToString("yyyyMMdd_hhmm");
			fName += ".txt";
			//filePath = fName;

		   

	           
			//if (fi != null)
			//{
			//if file exists append to it
			if (fi.Exists) 
			{
				try
				{
					fs = fi.Open(FileMode.Open);
					fs.Seek(0, SeekOrigin.End);
				}
				catch(Exception ex)
				{
					MessageBox.Show("Could not open file + " + ex.ToString() , "Tag Detector");
					return (false);
				}
			}
			else //new file
			{
				try
				{
					fs = fi.Create();
				}
				catch(Exception ex)
				{
					MessageBox.Show("Could not create file + " + ex.ToString() , "Tag Detector");
					fi = null;
					fs = null;
					return (false);
				}
				//}

				   
				//string info = "RdrID" + "\t" + "FGenID" +  "\t" + "TagType" + "\t" + "TagID" + "\t" + "RSSI" + "\t" + "Timestamp" + "\r\n";
				string info = "RdrID " + "\t";  //6+tab
				info += "FGenID" + "\t"; //6+tab
				info += "Type  " + "\t"; //6+tab
				info += "TagID          " + "\t"; //11+tab
				info += "RSSI  " + "\t"; //6+tab
				info += "Input  " + "\t"; //6+tab
				info += "Timestamp" + "\r\n";

					
				UTF8Encoding utf8 = new UTF8Encoding();
				Byte[] encodedBytes = utf8.GetBytes(info);
				lock(fileLock)
				{
					fs.Write(encodedBytes, 0, encodedBytes.Length);
					fs.Flush();
				}
			}//new file

			fileExists = true;
			UpdateFileRecDisplay(fs.Length);
		  
			return (true);

		}

		//callback function for reader events
		public int OnReaderEvent(ActiveWaveAPIClass.rfReaderEvent_t readerEvent)
		{
			if (readerEvent.eventType == AWI_APIClass.RF_READER_RESET)
			{
				;
			}
			else if (readerEvent.eventType == AWI_APIClass.RF_OPEN_SOCKET)
			{
				string ipStr = "";
				char c;
				for (int i=0; i<readerEvent.ip.Length; i++)
				{
					c = Convert.ToChar(readerEvent.ip[i]);
					if (c != '\0')
						ipStr += Convert.ToString(c);
					else
						break;
				}
				string s = " Socket opened ip = " + ipStr;
				listBox3.Items.Add(s);
			}
			else if (readerEvent.eventStatus == AWI_APIClass.RF_E_TIMEOUT)
			{
				if ((readerEvent.eventType == AWI_APIClass.RF_READER_RESET) ||
					(readerEvent.eventType == AWI_APIClass.RF_TAG_CALL))
				{
					listBox3.Items.Add("TimeOut");
					PlaySound(1);
				}
			}
			else if (readerEvent.eventType == AWI_APIClass.RF_READER_POWERUP)
			{
				RdrIDTextBox.Text = Convert.ToString(readerEvent.reader);

				if (!autoResetMode) 
				{
					listBox3.Items.Add("Powerup OK Rdr " + Convert.ToString(readerEvent.reader));
					RSSIProgressBar.Value = 0;
					RSSITextBox.Text = "";
					EnableReader(readerEvent.host, readerEvent.reader);
					readerID = readerEvent.reader;
					PlaySound(1);
					int ret = api.rfGetReaderFS(1, readerID, 0, AWI_APIClass.SPECIFIC_READER, ++pktID);
				}
				else
					autoResetMode = false;
				
				connection = true;

			}//powerup
			else if (readerEvent.eventType == AWI_APIClass.RF_SET_RDR_FS) 
			{
				FSTextBox.Text = Convert.ToString(readerEvent.smartFgen.fsValue);
				if (readerEvent.smartFgen.longDistance)
					LongRangeCheckBox.Checked = true;
				else
					LongRangeCheckBox.Checked = false;
				PlaySound(1);
				if(sendCallTag)
				{
					CallTag();
				}
			}
			else if (readerEvent.eventType == AWI_APIClass.RF_GET_RDR_FS) 
			{
				FSTextBox.Text = Convert.ToString(readerEvent.smartFgen.fsValue);
				if (readerEvent.smartFgen.longDistance)
					LongRangeCheckBox.Checked = true;
				else
					LongRangeCheckBox.Checked = false;
				PlaySound(1);
			}
			else if ((readerEvent.eventType == AWI_APIClass.RF_READER_ENABLE) ||
					 (readerEvent.eventType == AWI_APIClass.RF_READER_ENABLE_ALL))
			{
				listBox3.Items.Add("Reader " + Convert.ToString(readerEvent.reader) + " Enabled");
				PlaySound(1);
			}
			else if ((readerEvent.eventType == AWI_APIClass.RF_READER_DISABLE) ||
				     (readerEvent.eventType == AWI_APIClass.RF_READER_DISABLE_ALL))
			{
				listBox3.Items.Add("Reader " + Convert.ToString(readerEvent.reader) + " Disabled");
				PlaySound(1);
			}
			else if ((readerEvent.eventType == AWI_APIClass.RF_READER_QUERY) ||
					 (readerEvent.eventType == AWI_APIClass.RF_READER_QUERY_ALL))
			{
				listBox3.Items.Add("Reader " + Convert.ToString(readerEvent.reader) + " Qurey:");
				if (readerEvent.readerInfo.sendRSSI)
					listBox3.Items.Add("RSSI = Enabled");
				else
				    listBox3.Items.Add("RSSI = Disabled");

				if (readerEvent.readerInfo.broadcast)
					listBox3.Items.Add("Broadcast = Enabled");
				else
					listBox3.Items.Add("Broadcast = Disabled");

				if (readerEvent.readerInfo.enableAtPwrUp)
					listBox3.Items.Add("EnableAtPowerup = Enabled");
				else
					listBox3.Items.Add("EnableAtPowerup = Disabled");
							
				PlaySound(1);
			}

			return (0);
		}

		void ClearTagCollection()
		{
			for (int i=0; i<tagCollection.Count; i++)
				tagCollection.RemoveFrom(0);
		}

		//callback function for tag events
		public int OnTagEvent(ActiveWaveAPIClass.rfTagEvent_t tagEvent)
		{
			short ret = 0;
			if (!connection)
				return (-1);

			if (tagEvent.eventType == AWI_APIClass.RF_TAG_QUERY) 
			{
				tagStruct tag = new tagStruct();
				tag.id = tagEvent.tag.id;
				tag.type = tagEvent.tag.tagType;
				tag.reader = tagEvent.reader;
				tag.fGen = tagEvent.fGenerator;
				tag.groupID = tagEvent.tag.groupCount;
				tag.RSSI = tagEvent.RSSI;
				ret = tagCollection.IsEqual(tag);
				if (ret == -1)  //found exact match
					return (0);
				else if (ret >= 0)
				{
					tagCollection.RemoveFrom((int)ret);
				}

				tagCollection.Add(tag);

				//string type = "";
				string s = tagEvent.tag.id.ToString("ID: ######000000   ");
					
				if (tagEvent.tag.tagType == 0x01)
				{
					s += "ACC  ";
					//type = "ACC";
				}
				else if (tagEvent.tag.tagType == 0x02)
				{
					s += "INV  ";
					//type = "INV";
				}
				else if (tagEvent.tag.tagType == 0x03)
				{
					s += "AST  ";
					//type = "AST";
				}
				    
				RSSIProgressBar.Value = 0;
				RSSITextBox.Text = "";
				
				//to make list enanle but did not work
				enableFlag = false;
				listDisplayCollection.Add(s);
				 
				DisplayMsg(s);

				PlaySound(1); 	
			}
			else if (tagEvent.eventType == AWI_APIClass.RF_TAG_ENABLE) 
			{
				tagStruct tag = new tagStruct();
				tag.id = tagEvent.tag.id;
				tag.type = tagEvent.tag.tagType;
				tag.reader = tagEvent.reader;
				tag.fGen = tagEvent.fGenerator;
				tag.groupID = tagEvent.tag.groupCount;
				tag.RSSI = tagEvent.RSSI;
				ret = tagCollection.IsEqual(tag);
				if (ret == -1)  //found exact match
					return (0);
				else if (ret >= 0)
				{
					tagCollection.RemoveFrom((int)ret);
				}

				tagCollection.Add(tag);

				//string type = "";
				string s = tagEvent.tag.id.ToString("ID: ######000000   ");
					
				if (tagEvent.tag.tagType == 0x01)
				{
					s += "ACC  ";
					//type = "ACC";
				}
				else if (tagEvent.tag.tagType == 0x02)
				{
					s += "INV  ";
					//type = "INV";
				}
				else if (tagEvent.tag.tagType == 0x03)
				{
					s += "AST  ";
					//type = "AST";
				}
				    
				RSSIProgressBar.Value = 0;
				RSSITextBox.Text = "";
				
				//to make list enanle but did not work
				enableFlag = false;
				listDisplayCollection.Add(s);
				 
				DisplayMsg(s);

				PlaySound(1); 	
			}
			else if (tagEvent.eventType == AWI_APIClass.RF_TAG_DISABLE) 
			{
				tagStruct tag = new tagStruct();
				tag.id = tagEvent.tag.id;
				tag.type = tagEvent.tag.tagType;
				tag.reader = tagEvent.reader;
				tag.fGen = tagEvent.fGenerator;
				tag.groupID = tagEvent.tag.groupCount;
				tag.RSSI = tagEvent.RSSI;
				ret = tagCollection.IsEqual(tag);
				if (ret == -1)  //found exact match
					return (0);
				else if (ret >= 0)
				{
					tagCollection.RemoveFrom((int)ret);
				}

				tagCollection.Add(tag);

				//string type = "";
				string s = tagEvent.tag.id.ToString("ID: ######000000   ");
					
				if (tagEvent.tag.tagType == 0x01)
				{
					s += "ACC  ";
					//type = "ACC";
				}
				else if (tagEvent.tag.tagType == 0x02)
				{
					s += "INV  ";
					//type = "INV";
				}
				else if (tagEvent.tag.tagType == 0x03)
				{
					s += "AST  ";
					//type = "AST";
				}
				    
				RSSIProgressBar.Value = 0;
				RSSITextBox.Text = "";
				
				//to make list enanle but did not work
				enableFlag = false;
				listDisplayCollection.Add(s);
				 
				DisplayMsg(s);

				PlaySound(1); 	
			}
			else if ((tagEvent.eventType == AWI_APIClass.RF_TAG_DETECTED) || 
				     (tagEvent.eventType == AWI_APIClass.RF_TAG_DETECTED_RSSI))
			{
				tagStruct tag = new tagStruct();
				tag.id = tagEvent.tag.id;
				tag.type = tagEvent.tag.tagType;
				tag.reader = tagEvent.reader;
				tag.fGen = tagEvent.fGenerator;
				tag.groupID = tagEvent.tag.groupCount;
				tag.RSSI = tagEvent.RSSI;
				ret = tagCollection.IsEqual(tag);
				if (ret == -1)  //found exact match
					return (0);
				else if (ret >= 0)
				{
					tagCollection.RemoveFrom((int)ret);
				}

				tagCollection.Add(tag);

				//string type = "";
				string s = tagEvent.tag.id.ToString("ID: ######000000   ");
					
				if (tagEvent.tag.tagType == 0x01)
				{
					s += "ACC  ";
					//type = "ACC";
				}
				else if (tagEvent.tag.tagType == 0x02)
				{
					s += "INV  ";
					//type = "INV";
				}
				else if (tagEvent.tag.tagType == 0x03)
				{
					s += "AST  ";
					//type = "AST";
				}
				    
				if (tagEvent.eventType == AWI_APIClass.RF_TAG_DETECTED_RSSI)
				{
					s += " " + Convert.ToString(tagEvent.RSSI);
					RSSIProgressBar.Value = (int)tagEvent.RSSI;
					RSSITextBox.Text = Convert.ToString(tagEvent.RSSI);
				}
				else
				{
					RSSIProgressBar.Value = 0;
					RSSITextBox.Text = "";
				}

				//to make list enanle but did not work
				enableFlag = false;
				listDisplayCollection.Add(s);
				 
				DisplayMsg(s);

				PlaySound(1); 	
			}

			return (0);
		}

		private void UpdateFileRecDisplay(long bytes)
		{ 
			
		    
		}

		public uint GetPort()
		{
			return port;
		}

		public void ResetReader(ushort rdr)
		{
			ushort cmdType = 0;
			if (pktID >= 224)
				pktID = 0;
			cmdType = AWI_APIClass.ALL_READERS;
			
			int ret = api.rfResetReader(hostID, rdr, 0, cmdType, ++pktID); 
			if (ret < 0)
				DisplayMsg("Reset Failed ret=" + ret);
			RSSIProgressBar.Value = 0;
			RSSITextBox.Text = "";
			PlaySound(1);
		}

		private void EnableReader(ushort hostID, ushort rdrID)
		{
			if (pktID >= 224)
				pktID = 0;
			int ret = api.rfEnableReader(hostID, rdrID, 0, true, AWI_APIClass.SPECIFIC_READER, ++pktID); 
			if (ret < 0)
				DisplayMsg("Enable Failed ret=" + ret);
			//listBox1.Items.Insert(0, "Enable Failed ret=" + ret);
			RSSIProgressBar.Value = 0;
			RSSITextBox.Text = "";
			PlaySound(1);
		}
		
		private void Form1_Load(object sender, System.EventArgs e)
		{
			int ret = 0;
			//uint port = 4;

			api.rfRegisterReaderEvent(new ActiveWaveAPIClass.fReaderEvent(OnReaderEvent));
			api.rfRegisterTagEvent(new ActiveWaveAPIClass.fTagEvent(OnTagEvent));
			
			/*if ((ret=api.rfOpen(115200 , port)) == 0)
			{
				//listBox1.Items.Insert(0, "Open port OK. ret=" + ret);
				DisplayMsg("Open port OK. ret=" + ret);
				portOpen = true;
				ResetReader();
			}
			else
			{
				//listBox1.Items.Insert(0, "Open failed ret=" + ret);
				DisplayMsg("Open failed ret=" + ret);
				PlaySound(1);
			}*/

			RSSIProgressBar.Value = 0;
			RSSITextBox.Text = "";
		}

		public bool IsPortOpen()
		{
			return portOpen;
		}

		public void SetPort(bool b, uint portNum)
		{
			portOpen = b;
			port = portNum;
		}

		public void DisplayMsg(string msg)
		{
			listBox1.Items.Insert(0, msg);
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			PlaySound(3);
			ClearTagCollection();
			listBox1.Items.Clear();
			CallButton.Enabled = true;
		}

		private void ResetFileButton_Click(object sender, System.EventArgs e)
		{
			
		}

		private void StopButton_Click(object sender, System.EventArgs e)
		{
			PlaySound(3);
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
		   
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			listBox1.Enabled = true;
			CallButton.Enabled = true;
			IncFSButton.Enabled = true;
			DecFSButton.Enabled = true;
			MinFSButton.Enabled = true;
			MaxFSButton.Enabled = true;
			SetFSButton.Enabled = true;
			LEDCheckBox.Enabled = true;
			QueryButton.Enabled = true;
			EnableButton.Enabled = true;
			DisableButton.Enabled = true;
			ResetReaderButton.Enabled = true;
			EnableReaderButton.Enabled = true;
			DisableReaderButton.Enabled = true;
			QueryReaderButton.Enabled = true;
			OpenSocketButton.Enabled = true;
			CloseSocketButton.Enabled = true;
			FSTextBox.ReadOnly = false;
			TagIDCheckBox.Enabled = true;
			LongRangeCheckBox.Enabled = true;
			button1.Enabled = true;  //clearList
			timer1.Enabled = false;
			PlaySound(1);
		}

		private void CallButton_Click(object sender, System.EventArgs e)
		{
			sendCallTag = false;
			CallTag();
		}

		private void CallTag()
		{
			listBox1.Items.Clear();
			listBox1.Enabled = false;
			CallButton.Enabled = false;
			IncFSButton.Enabled = false;
			DecFSButton.Enabled = false;
			MinFSButton.Enabled = false;
			MaxFSButton.Enabled = false;
			SetFSButton.Enabled = false;
			LEDCheckBox.Enabled = false;
			QueryButton.Enabled = false;
			EnableButton.Enabled = false;
			DisableButton.Enabled = false;
			ResetReaderButton.Enabled = false;
			EnableReaderButton.Enabled = false;
			DisableReaderButton.Enabled = false;
			QueryReaderButton.Enabled = false;
			OpenSocketButton.Enabled = false;
			CloseSocketButton.Enabled = false;
			TagIDCheckBox.Enabled = false;
			LEDCheckBox.Enabled = false;
			LongRangeCheckBox.Enabled = false;
			FSTextBox.ReadOnly = true;
			button1.Enabled = false;
			listDisplayCollection.Clear();
			//timer1.Enabled = false;

			bool longDelay = true;
			
			if (pktID >= 224)
				pktID = 1;

			rfTagSelect_t tagSelect = new rfTagSelect_t();
			if (TagIDCheckBox.Checked)
			{
				if (TagIDTextBox.Text.Length == 0)
				{
					MessageBox.Show("Need Tag ID"  , "Tag Detector");
					return;
				}

				tagSelect.tagList = new uint[50];
				tagSelect.tagList[0] = Convert.ToUInt32(TagIDTextBox.Text);
				tagSelect.tagType = APIConsts.ALL_TAGS;
				tagSelect.selectType = APIConsts.RF_SELECT_TAG_ID; 
				tagSelect.numTags = 1;
				longDelay = false;
				if (LEDCheckBox.Checked)
					tagSelect.ledOn = true;
			}
			else
			{
				tagSelect.selectType = APIConsts.RF_SELECT_FIELD; 
				tagSelect.numTags = 0;
				if (LEDCheckBox.Checked)
					tagSelect.ledOn = true;
			}

			ClearTagCollection();
			
			/*if (!sendCallTag)
			{
				timer1.Interval = GetMaxWaitValue();
				timer1.Enabled = true;
			}*/

			ushort rdr = Convert.ToUInt16(RdrIDTextBox.Text);
			int ret = api.rfCallTags(1, rdr, 0, 0, tagSelect, true, longDelay, AWI_APIClass.SPECIFIC_READER, ++pktID);
			timer1.Enabled = true;
			timer1.Enabled = true;
		}

		private void TagIDCheckBox_Click(object sender, System.EventArgs e)
		{
			if (TagIDCheckBox.Checked)
				TagIDTextBox.ReadOnly = false;
			else
				TagIDTextBox.ReadOnly = true;
		}

		private void timer2_Tick(object sender, System.EventArgs e)
		{
			if (sendCallTag)
				myCounter += 1000;
			
			if (sendCallTag  && myCounter >= maxMyCounter)
			{
				sendCallTag = false;
				myCounter = 0;

				listBox1.Enabled = true;
				CallButton.Enabled = true;
				IncFSButton.Enabled = true;
				DecFSButton.Enabled = true;
				MinFSButton.Enabled = true;
				MaxFSButton.Enabled = true;
				SetFSButton.Enabled = true;
				LEDCheckBox.Enabled = true;
				FSTextBox.ReadOnly = false;
				TagIDCheckBox.Enabled = true;
				LongRangeCheckBox.Enabled = true;
				button1.Enabled = true;  //clearList
				timer1.Enabled = false;
				PlaySound(1);
			}

			/*
			//tried to make the list enabled but did not work so this timer
			//is turned off.
			if (listDisplayCollection.Count > 0)
				//lock (listLock)
			{
				//listBox1.Enabled = false;
				while (listDisplayCollection.Count > 0)
				{
					DisplayMsg(listDisplayCollection[0]);
					listDisplayCollection.RemoveFrom(0);
				}
				//listBox1.Enabled = true;
				listBox1.Invalidate();
				
			}
			else 
			{
				if (enableFlag)
				   listBox1.Enabled = true;
				else
				   enableFlag = true;
			}
			*/

		}

		private void IncFSButton_Click(object sender, System.EventArgs e)
		{
			sendCallTag = false;
			int ret = api.rfSetReaderFS(1, readerID, 0, AWI_APIClass.RF_INC_FS, 0, false, AWI_APIClass.SPECIFIC_READER, ++pktID);
			PlaySound(1);	
		}

		private void DecFSButton_Click(object sender, System.EventArgs e)
		{
			sendCallTag = false;
			int ret = api.rfSetReaderFS(1, readerID, 0, AWI_APIClass.RF_DEC_FS, 0, false, AWI_APIClass.SPECIFIC_READER, ++pktID);	
		}

		private void MinFSButton_Click(object sender, System.EventArgs e)
		{
			FSTextBox.Text = "0";
			byte fs = 0;
			LongRangeCheckBox.Checked = false;
			sendCallTag = true;
			maxMyCounter = GetMaxWaitValue();
			myCounter = 0;
			int ret = api.rfSetReaderFS(1, readerID, 0, AWI_APIClass.RF_ABS_FS, fs, false, AWI_APIClass.SPECIFIC_READER, ++pktID);	
			PlaySound(1);
		}

		private void MaxFSButton_Click(object sender, System.EventArgs e)
		{
			FSTextBox.Text = "20";
			byte fs = 20;
			LongRangeCheckBox.Checked = true;
			sendCallTag = true;
			maxMyCounter = GetMaxWaitValue();
			myCounter = 0;
			int ret = api.rfSetReaderFS(1, readerID, 0, AWI_APIClass.RF_ABS_FS, fs, true, AWI_APIClass.SPECIFIC_READER, ++pktID);	
			PlaySound(1);
		}

		private void FSTextBox_TextChanged(object sender, System.EventArgs e)
		{
		   
		}

		private void FSTextBox_GotFocus(object sender, System.EventArgs e)
		{
			fsTextBoxGotFocus = true;
		}

		private void FSTextBox_LostFocus(object sender, System.EventArgs e)
		{
			fsTextBoxGotFocus = false; 
		}

		public static void ThreadProc() 
		{
			
			while (true)
			{
				if (thisForm.listDisplayCollection.Count > 0)
				{
					thisForm.listBox1.Enabled = false;
					while (thisForm.listDisplayCollection.Count > 0)
					{
						thisForm.DisplayMsg(thisForm.listDisplayCollection[0]);
						thisForm.listDisplayCollection.RemoveFrom(0);
					}

					thisForm.enableFlag = true;
				}
				else
				{
					if (thisForm.enableFlag)
					{
						thisForm.enableFlag = false;
						thisForm.listBox1.Enabled = true;
						thisForm.IncFSButton.Enabled = true;
						thisForm.DecFSButton.Enabled = true;
						thisForm.MinFSButton.Enabled = true;
						thisForm.MaxFSButton.Enabled = true;
						thisForm.SetFSButton.Enabled = true;
						thisForm.TagIDCheckBox.Enabled = true;
						thisForm.LongRangeCheckBox.Enabled = true;
						thisForm.LEDCheckBox.Enabled = true;
						thisForm.FSTextBox.ReadOnly = false;
					}
				}

				Thread.Sleep(2500);

			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			resetCounter = 0;
			ClearTagCollection();

			listBox1.Enabled = true;
			CallButton.Enabled = true;
			IncFSButton.Enabled = true;
			DecFSButton.Enabled = true;
			MinFSButton.Enabled = true;
			MaxFSButton.Enabled = true;
			SetFSButton.Enabled = true;
			LEDCheckBox.Enabled = true;
			FSTextBox.ReadOnly = false;
			TagIDCheckBox.Enabled = true;
			LongRangeCheckBox.Enabled = true;
			button1.Enabled = true;  //clearList
			timer1.Enabled = false;	

			ushort rdr = Convert.ToUInt16(RdrIDTextBox.Text);
			ResetReader(rdr);


		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			Form2 cfgForm = new Form2(this);
			cfgForm.ShowDialog();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			PlaySound(3);
			WriteComPortNum(Convert.ToString(port));
			connection = false;
			api.rfClose();
			this.Close();
		}

		private void SetFS()
		{
			bool longRange = false;
			int ret = 0;

			if (FSTextBox.Text.Length == 0)
			{
				MessageBox.Show("No FS Value"  , "Tag Detector");
				return;
			}

			byte fs = Convert.ToByte(FSTextBox.Text);
			if ((fs >20) || (fs < 0))
			{
				MessageBox.Show("FS Range 0 - 20"  , "Tag Detector");
				return;
			}

			if (LongRangeCheckBox.Checked)
				longRange = true;

			ret = api.rfSetReaderFS(1, readerID, 0, AWI_APIClass.RF_ABS_FS, fs, longRange, AWI_APIClass.SPECIFIC_READER, ++pktID);
			PlaySound(1);
		}

		private void SetFSButton_Click(object sender, System.EventArgs e)
		{
			sendCallTag = true;
			maxMyCounter = GetMaxWaitValue();
			myCounter = 0;
			SetFS();
		}

		private void LongRangeCheckBox_Click(object sender, System.EventArgs e)
		{
			sendCallTag = true;
		    maxMyCounter = GetMaxWaitValue();
			myCounter = 0;
			SetFS();
		}

		private void LongRangeCheckBox_CheckStateChanged(object sender, System.EventArgs e)
		{
		
		}

		private int GetMaxWaitValue()
		{
			int val = 0;
			if (TagIDCheckBox.Checked)
			{
				if (LEDCheckBox.Checked)
					val = 9000;
				else
					val = 5000;
			}
			else
			{
				if (LEDCheckBox.Checked)
					val = 12000;
				else
					val = 9000;	
			}

			return val;
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			Form3 aboutForm = new Form3();
			aboutForm.ShowDialog();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			listBox1.Items.Clear();
		}

		private void OpenSocketButton_Click(object sender, System.EventArgs e)
		{
			if ((!IPCheckBox.Checked) && (!IPCheckBox1.Checked))
			   return;

			if (IPCheckBox.Checked)
			{
				if (IPTextBox.Text.Length == 0)
					return;
			}
			
			if (IPCheckBox1.Checked)
			{
				if (IPTextBox1.Text.Length == 0)
					return;
			}

			string s = "";
			char[] buf = new char[20];
			byte[] ip = new byte[20]; 

			if (IPCheckBox.Checked)
			{
				s = IPTextBox.Text.ToString();
				buf = s.ToCharArray(); 
				for (int i=0; i<s.Length; i++)
					ip[i] = Convert.ToByte(buf[i]);
				int ret = api.rfOpenSocket(ip, 1, false, AWI_APIClass.SPECIFIC_IP, 100);
				if (ret < 0)
					listBox3.Items.Add("Socket failed to open ip = " + s);
				else
					listBox3.Items.Add("Socket connected ip = " + s);
			}

			if (IPCheckBox1.Checked)
			{
				s = IPTextBox1.Text.ToString();
				buf = s.ToCharArray(); 
				for (int i=0; i<s.Length; i++)
					ip[i] = Convert.ToByte(buf[i]);
				int ret = api.rfOpenSocket(ip, 1, false, AWI_APIClass.SPECIFIC_IP, 100);
				if (ret < 0)
					listBox3.Items.Add("Socket failed to open ip = " + s);
				else
					listBox3.Items.Add("Socket connected ip = " + s);
			}
		}

		private void CloseSocketButton_Click(object sender, System.EventArgs e)
		{
			if ((!IPCheckBox.Checked) && (!IPCheckBox1.Checked))
			{
				int ret = api.rfCloseSocket(null, AWI_APIClass.ALL_IPS);
				if (ret == 0)
					listBox3.Items.Add("All sockets closed");
				else
					listBox3.Items.Add("All sockets failed to close");
				return;
			}

			if (IPCheckBox.Checked)
			{
				if (IPTextBox.Text.Length == 0)
					return;
			}
			
			if (IPCheckBox1.Checked)
			{
				if (IPTextBox1.Text.Length == 0)
					return;
			}

			string s = "";
			char[] buf = new char[20];
			byte[] ip = new byte[20]; 

			if (IPCheckBox.Checked)
			{
				s = IPTextBox.Text.ToString();
				buf = s.ToCharArray(); 
				for (int i=0; i<s.Length; i++)
					ip[i] = Convert.ToByte(buf[i]);
				int ret = api.rfCloseSocket(ip, AWI_APIClass.SPECIFIC_IP);
				if (ret < 0)
					listBox3.Items.Add("Socket failed to close ip = " + s);
				else
					listBox3.Items.Add("Socket closed ip = " + s);
			}

			if (IPCheckBox1.Checked)
			{
				s = IPTextBox1.Text.ToString();
				buf = s.ToCharArray(); 
				for (int i=0; i<s.Length; i++)
					ip[i] = Convert.ToByte(buf[i]);
				int ret = api.rfCloseSocket(ip, AWI_APIClass.SPECIFIC_IP);
				if (ret < 0)
					listBox3.Items.Add("Socket failed to close ip = " + s);
				else
					listBox3.Items.Add("Socket closed ip = " + s);
			}
		}

		private void EnableButton_Click(object sender, System.EventArgs e)
		{
			listBox1.Items.Clear();
			listBox1.Enabled = false;
			CallButton.Enabled = false;
			IncFSButton.Enabled = false;
			DecFSButton.Enabled = false;
			MinFSButton.Enabled = false;
			MaxFSButton.Enabled = false;
			SetFSButton.Enabled = false;
			LEDCheckBox.Enabled = false;
			QueryButton.Enabled = false;
			EnableButton.Enabled = false;
			DisableButton.Enabled = false;
			ResetReaderButton.Enabled = false;
			EnableReaderButton.Enabled = false;
			DisableReaderButton.Enabled = false;
			QueryReaderButton.Enabled = false;
			OpenSocketButton.Enabled = false;
			CloseSocketButton.Enabled = false;
			TagIDCheckBox.Enabled = false;
			LEDCheckBox.Enabled = false;
			LongRangeCheckBox.Enabled = false;
			FSTextBox.ReadOnly = true;
			button1.Enabled = false;
			listDisplayCollection.Clear();

			bool longDelay = true;
			
			if (pktID >= 224)
				pktID = 1;

			rfTagSelect_t tagSelect = new rfTagSelect_t();
			if (TagIDCheckBox.Checked)
			{
				if (TagIDTextBox.Text.Length == 0)
				{
					MessageBox.Show("Need Tag ID"  , "Tag Detector");
					return;
				}

				tagSelect.tagList = new uint[50];
				tagSelect.tagList[0] = Convert.ToUInt32(TagIDTextBox.Text);
				tagSelect.tagType = APIConsts.ALL_TAGS;
				tagSelect.selectType = APIConsts.RF_SELECT_TAG_ID; 
				tagSelect.numTags = 1;
				longDelay = false;
				if (LEDCheckBox.Checked)
					tagSelect.ledOn = true;
			}
			else
			{
				tagSelect.selectType = APIConsts.RF_SELECT_FIELD; 
				tagSelect.numTags = 0;
				if (LEDCheckBox.Checked)
					tagSelect.ledOn = true;
			}

			ClearTagCollection();
			
			ushort rdr = Convert.ToUInt16(RdrIDTextBox.Text);
			int ret = api.rfEnableTags(1, rdr, 0, tagSelect, true, true, longDelay, AWI_APIClass.SPECIFIC_READER, ++pktID);
			timer1.Enabled = true;
			timer1.Enabled = true;
		}

		private void QueryButton_Click(object sender, System.EventArgs e)
		{
			listBox1.Items.Clear();
			listBox1.Enabled = false;
			CallButton.Enabled = false;
			IncFSButton.Enabled = false;
			DecFSButton.Enabled = false;
			MinFSButton.Enabled = false;
			MaxFSButton.Enabled = false;
			SetFSButton.Enabled = false;
			LEDCheckBox.Enabled = false;
			QueryButton.Enabled = false;
			EnableButton.Enabled = false;
			DisableButton.Enabled = false;
			ResetReaderButton.Enabled = false;
			EnableReaderButton.Enabled = false;
			DisableReaderButton.Enabled = false;
			QueryReaderButton.Enabled = false;
			OpenSocketButton.Enabled = false;
			CloseSocketButton.Enabled = false;
			TagIDCheckBox.Enabled = false;
			LEDCheckBox.Enabled = false;
			LongRangeCheckBox.Enabled = false;
			FSTextBox.ReadOnly = true;
			button1.Enabled = false;
			listDisplayCollection.Clear();

			bool longDelay = true;
			
			if (pktID >= 224)
				pktID = 1;

			rfTagSelect_t tagSelect = new rfTagSelect_t();
			if (TagIDCheckBox.Checked)
			{
				if (TagIDTextBox.Text.Length == 0)
				{
					MessageBox.Show("Need Tag ID"  , "Tag Detector");
					return;
				}

				tagSelect.tagList = new uint[50];
				tagSelect.tagList[0] = Convert.ToUInt32(TagIDTextBox.Text);
				tagSelect.tagType = APIConsts.ALL_TAGS;
				tagSelect.selectType = APIConsts.RF_SELECT_TAG_ID; 
				tagSelect.numTags = 1;
				longDelay = false;
				if (LEDCheckBox.Checked)
					tagSelect.ledOn = true;
			}
			else
			{
				tagSelect.selectType = APIConsts.RF_SELECT_FIELD; 
				tagSelect.numTags = 0;
				if (LEDCheckBox.Checked)
					tagSelect.ledOn = true;
			}

			ClearTagCollection();
			
			ushort rdr = Convert.ToUInt16(RdrIDTextBox.Text);
			int ret = api.rfQueryTags(1, rdr, 0, tagSelect, true, longDelay, AWI_APIClass.SPECIFIC_READER, ++pktID);
			timer1.Enabled = true;
			timer1.Enabled = true;
		}

		private void DisableButton_Click(object sender, System.EventArgs e)
		{
			listBox1.Items.Clear();
			listBox1.Enabled = false;
			CallButton.Enabled = false;
			IncFSButton.Enabled = false;
			DecFSButton.Enabled = false;
			MinFSButton.Enabled = false;
			MaxFSButton.Enabled = false;
			SetFSButton.Enabled = false;
			LEDCheckBox.Enabled = false;
			QueryButton.Enabled = false;
			EnableButton.Enabled = false;
			DisableButton.Enabled = false;
			ResetReaderButton.Enabled = false;
			EnableReaderButton.Enabled = false;
			DisableReaderButton.Enabled = false;
			QueryReaderButton.Enabled = false;
			OpenSocketButton.Enabled = false;
			CloseSocketButton.Enabled = false;
			TagIDCheckBox.Enabled = false;
			LEDCheckBox.Enabled = false;
			LongRangeCheckBox.Enabled = false;
			FSTextBox.ReadOnly = true;
			button1.Enabled = false;
			listDisplayCollection.Clear();

			bool longDelay = true;
			
			if (pktID >= 224)
				pktID = 1;

			rfTagSelect_t tagSelect = new rfTagSelect_t();
			if (TagIDCheckBox.Checked)
			{
				if (TagIDTextBox.Text.Length == 0)
				{
					MessageBox.Show("Need Tag ID"  , "Tag Detector");
					return;
				}

				tagSelect.tagList = new uint[50];
				tagSelect.tagList[0] = Convert.ToUInt32(TagIDTextBox.Text);
				tagSelect.tagType = APIConsts.ALL_TAGS;
				tagSelect.selectType = APIConsts.RF_SELECT_TAG_ID; 
				tagSelect.numTags = 1;
				longDelay = false;
				if (LEDCheckBox.Checked)
					tagSelect.ledOn = true;
			}
			else
			{
				tagSelect.selectType = APIConsts.RF_SELECT_FIELD; 
				tagSelect.numTags = 0;
				if (LEDCheckBox.Checked)
					tagSelect.ledOn = true;
			}

			ClearTagCollection();
			
			ushort rdr = Convert.ToUInt16(RdrIDTextBox.Text);
			int ret = api.rfEnableTags(1, rdr, 0, tagSelect, false, true, longDelay, AWI_APIClass.SPECIFIC_READER, ++pktID);
			timer1.Enabled = true;
			timer1.Enabled = true;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			listBox3.Items.Clear();
		}

		private void EnableReaderButton_Click(object sender, System.EventArgs e)
		{
			ushort rdr = Convert.ToUInt16(RdrIDTextBox.Text);
			int ret = api.rfEnableReader(1, rdr, 0, true, AWI_APIClass.SPECIFIC_READER, ++pktID);
			if (ret < 0)
				listBox3.Items.Add("Enable Rdr Failed ret=" + ret);
			PlaySound(1);
		}

		private void DisableReaderButton_Click(object sender, System.EventArgs e)
		{
			ushort rdr = Convert.ToUInt16(RdrIDTextBox.Text);
			int ret = api.rfEnableReader(1, rdr, 0, false, AWI_APIClass.SPECIFIC_READER, ++pktID);
			if (ret < 0)
				listBox3.Items.Add("Disable Rdr Failed ret=" + ret);
			PlaySound(1);
		}

		private void QueryReaderButton_Click(object sender, System.EventArgs e)
		{
			ushort rdr = Convert.ToUInt16(RdrIDTextBox.Text);
			int ret = api.rfQueryReader(1, rdr, 0, AWI_APIClass.SPECIFIC_READER, ++pktID);
			if (ret < 0)
				listBox3.Items.Add("Query Rdr Failed ret=" + ret);
			PlaySound(1);
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem3_Click_1(object sender, System.EventArgs e)
		{
			
		}

		private void menuItem5_Click_1(object sender, System.EventArgs e)
		{
		    int ret = api.rfSetCommType(APIConsts.NETWORKING);
			OpenSocketButton.Enabled = true;
			CloseSocketButton.Enabled = true;
			ResetReaderButton.Enabled = true;
			OpenPortButton.Enabled = false;
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
		   int ret = api.rfSetCommType(APIConsts.RS232);
			OpenSocketButton.Enabled = false;
			CloseSocketButton.Enabled = false;
			ResetReaderButton.Enabled = true;
			OpenPortButton.Enabled = true;
		}

		private void OpenPortButton_Click(object sender, System.EventArgs e)
		{
			ushort port = Convert.ToUInt16(PortNumTextBox.Text);
			int ret = api.rfOpen(115200, port);
		}

		private void ResetReaderButton_Click(object sender, System.EventArgs e)
		{
			resetCounter = 0;
			listBox1.Enabled = false;
			CallButton.Enabled = false;
			IncFSButton.Enabled = false;
			DecFSButton.Enabled = false;
			MinFSButton.Enabled = false;
			MaxFSButton.Enabled = false;
			SetFSButton.Enabled = false;
			LEDCheckBox.Enabled = false;
			QueryButton.Enabled = false;
			EnableButton.Enabled = false;
			DisableButton.Enabled = false;
			ResetReaderButton.Enabled = false;
			EnableReaderButton.Enabled = false;
			DisableReaderButton.Enabled = false;
			QueryReaderButton.Enabled = false;
			OpenSocketButton.Enabled = false;
			CloseSocketButton.Enabled = false;
			TagIDCheckBox.Enabled = false;
			LEDCheckBox.Enabled = false;
			LongRangeCheckBox.Enabled = false;
			FSTextBox.ReadOnly = true;
			button1.Enabled = false;

			ushort rdr = Convert.ToUInt16(RdrIDTextBox.Text);
			ResetReader(rdr);

			timer1.Enabled = true;
			timer1.Enabled = true;
		}
	}//Form1

	
	public class DataCollectionClass : CollectionBase
	{
		public dataStruct this [int index]
		{
			get { return (dataStruct) List[index];}
			set { List[index] = value;}
		}

		public int Add(dataStruct tag)
		{
			return (List.Add(tag));
		}

		public void RemoveFrom(int index)
		{
			List.Remove(List[index]);
		}
	}

	public class TagCollectionClass : CollectionBase
	{
		public tagStruct this [int index]
		{
			get { return (tagStruct) List[index];}
			set { List[index] = value;}
		}

		public int Add(tagStruct tag)
		{
			return (List.Add(tag));
		}

		public void Insert(int index, tagStruct tag)
		{
			List.Insert(index, tag);
		}

		public void Remove(tagStruct tag)
		{
			List.Remove(tag);
		}

		public void RemoveFrom(int index)
		{
			List.Remove(List[index]);
		}

		public bool Contain(tagStruct tag)
		{
			return(List.Contains(tag));
		}

		public void Replace(tagStruct oldTag, tagStruct newTag)
		{
			int i = IndexOf(oldTag.id);
			if (i >= 0) this[i] = newTag;
		}

		public int IndexOf(uint tagId)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].id == tagId) return i;
			}
			return -1;
		}

		public short IsEqual(tagStruct tag)
		{
			for (short i = 0; i < this.Count; i++)
			{
				if ((this[i].id == tag.id) && 
					(this[i].type == tag.type) &&
					(this[i].reader == tag.reader) &&
					(this[i].fGen == tag.fGen))
				{
					byte b = this[i].groupID;
					if (this[i].groupID == tag.groupID)
						return (-1);   //found it exact match
					else
						return (i);   //found it GID no match

				}//if
			}//for loop
			
			return (-2);   //did not find
		}	   
	}

	public class ListDisplayCollectionClass : CollectionBase
	{
		public string this [int index]
		{
			get { return (string) List[index];}
			set { List[index] = value;}
		}

		public int Add(string item)
		{
			return (List.Add(item));
		}

		public void RemoveFrom(int index)
		{
			List.Remove(List[index]);
		}

		public int IndexOf(string item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i] == item) return i;
			}
			return -1;
		}
	}

}//namespace
