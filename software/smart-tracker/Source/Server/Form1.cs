using System;
using System.Xml;
using System.Drawing;
using System.Globalization;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.Threading;
using AWIComponentLib.Communication;
using AWIComponentLib.Database;

namespace AWI.SmartTracker //CarTracker_V1._0
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public delegate void Scan(byte[] ip);
	public delegate void ConnectSocket();
	public delegate void ConnectThisSocket(byte[] ip);
	public delegate void DisconnectSocket(byte[] ip);
	public delegate int ResetAllReaders();
	public delegate int ResetSocketReader(byte[] ip);
	public delegate int OpenSerialPort(uint baud, uint port);
	public delegate int CloseSerialPort();


	public partial class CommForm : System.Windows.Forms.Form
	{
		private OdbcConnection m_connection = null;
		public event Scan ScanNet;
		public event ConnectSocket ConnectSock;
		public event ConnectThisSocket ConnectThisSock;
		public event DisconnectSocket DisconnectSock;
		public event OpenSerialPort OpenPort;
		public event CloseSerialPort ClosePort;
		public event ResetAllReaders m_resetAllReaders;
		public event ResetSocketReader ResetSocketReaderEvent;
		MainForm mForm = new MainForm(0);
		private Object myLock = new Object();
		private object updateListLock = new object();
		private tagDisplayCollectionClass ipDisplayCollection = new tagDisplayCollectionClass();
		//private tagDisplayInfoStruct tagDisplayInfo;

		//private MySqlDataReader netReader;
		private CommunicationClass communication = new CommunicationClass();
		private OdbcDbClass odbcDB = new OdbcDbClass();
		private uint listIndex;
		private int numitems;
		private int numips;
		private int lastIndex;
		private int tryCount;
		private string lastip;
		private int curIndex;
		private string selectedIPStr;
		private WaitForm waitDlg;
		private Thread MyThread;
		
		public CommForm(MainForm form)
		{
			mForm = form;
			numitems = 0;
			numips = 0;
			
			//changes made to support MYSQL Server v5.0 and later
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

			InitializeComponent();

			MyThread = new Thread(new ThreadStart(WaitMessageThread)); 
				
			OdbcDbClass.NotifyDBConnectionStatusHandler += new NotifyDBConnectionStatus(DBConnectionStatusHandler);
            form.m_updateIPListView += new UpdateIPListView(this.UpdateIPList);  //dec-06-06
			form.m_updateCommStatus += new UpdateCommStatus(this.UpdateCommunicationStat);
			

			if (MainForm.rs232Comm)
			{
				ConnectButton.Enabled = false;
				CloseConnectButton.Enabled = true;
				MsgLabel.Text = "RS232 Connected.";
			}
			else
			{
				ConnectButton.Enabled = true;
				CloseConnectButton.Enabled = false;
			}

			//if (MainForm.socketsConnection)
			//{
				//ScanButton.Enabled = false;
				//ConnectNetButton.Enabled = false;
			//}

			if (MainForm.m_connection == null)
			{
				MsgDBLabel.ForeColor = System.Drawing.Color.Red;
				MsgDBLabel.Text = "Disconnected";
				ScanButton.Enabled = false;
				ConnectNetButton.Enabled = false;
				ResetRdrButton.Enabled = false;
				DisconnectNetButton.Enabled = false;
				ResetReaderButton.Enabled = false;
				ConnectButton.Enabled = false;
				CloseConnectButton.Enabled = false;
				return;
			}
			else
               m_connection = MainForm.m_connection;

			lock (MainForm.m_connection)  //dec-06-06
			{
				MsgDBLabel.ForeColor = System.Drawing.Color.Blue;
				MsgDBLabel.Text = "Connected";
				//m_connection = MainForm.m_connection;
 
				UpdateIPListScreen();

				/*if (mForm.m_connection == null)
				{
					if (MainForm.providerName == dbProvider.SQL)
					{
						if (MainForm.conStr == "")
							return;  //no db connection

						if (!odbcDB.Connect(MainForm.conStr))  //SQL
							//if (!odbcDB.Connect("Driver={SQL Native Client};Server=Seyed02;Database=bank;Trusted_Connection=yes;Pooling=False;"))  //SQL
						{
							MsgDBLabel.ForeColor = System.Drawing.Color.Red;
							MsgDBLabel.Text = "Database : Disconnected";
							return;
						}
						else
						{
							MsgDBLabel.ForeColor = System.Drawing.Color.Blue;
							MsgDBLabel.Text = "Connected";
						}
					}
					else if (MainForm.providerName == dbProvider.MySQL)
					{
						if (!odbcDB.Connect("DRIVER={MySQL ODBC 3.51 Driver};SERVER=localhost;DATABASE=parkingtracker;USER=root;PASSWORD=;OPTION=3;"))	//MYSQL
						{
							MsgDBLabel.ForeColor = System.Drawing.Color.Red;
							MsgDBLabel.Text = "Database : Disconnected";
							return;
						}
						else
						{
							MsgDBLabel.ForeColor = System.Drawing.Color.Blue;
							MsgDBLabel.Text = "Connected";
						}	
					}
					else
					{
						return;
					}
				}
				else
				{
					m_connection = mForm.m_connection;
					MsgDBLabel.ForeColor = System.Drawing.Color.Blue;
					MsgDBLabel.Text = "Connected";
				}*/

				//InitializeComponent();
				//communication.PowerupEventHandler += new PowerupEvent(PowerupReaderNotifty); 

				////form.m_updateIPListView += new UpdateIPListView(this.UpdateIPList);  //dec-06-06
				//form.m_updateIPListView += new UpdateIPListView(this.UpdateIPList);
				////form.m_updateCommStatus += new UpdateCommStatus(this.UpdateCommunicationStat);  //dec-06-06
				//form.m_startTimer1Event += new StartTimer1(this.StartTimer1);
				//ConnectDB();

				/*string mySelectQuery = "SELECT * From netip";
				OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection); 

				OdbcDataReader myReader = null; 
				
				try
				{
					myReader = myCommand.ExecuteReader();
					MainForm.reconnectCounter = -1;
					//timer3.Enabled = false;
				}
				catch (Exception ex)
				{
					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{   
						//error code 2013

						MsgDBLabel.ForeColor = System.Drawing.Color.Red;
						MsgDBLabel.Text = "Disconnected";
						
						if (MainForm.reconnectCounter < 0)
						{
							MainForm.reconnectCounter = 0;
							//timer3.Enabled = true;
						}                         
					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				} //catch ..try
				//}//lock
				
				uint itemIndex = 1;
				string netStat = "";
				string rdrStat = "";
				IPListView.Items.Clear();
				while (myReader.Read())
				{
					ListViewItem listItem = new ListViewItem(itemIndex.ToString());  //index
					listItem.SubItems.Add(myReader.GetString(0));  //ip
					listItem.SubItems.Add(myReader.GetString(1));  //rdrID
					listItem.SubItems.Add(myReader.GetString(2));  //hostID
					netStat = myReader.GetString(3);
					listItem.SubItems.Add(netStat);  //networkStatus
					rdrStat = myReader.GetString(4);
					listItem.SubItems.Add(rdrStat);  //rdrStatus
					
					try
					{
						listItem.SubItems.Add(myReader.GetDateTime(5).ToString("MM-dd-yyyy  HH:mm:ss"));  //connectTime
						//IPListView.Items.Add(listItem);
					}
					catch
					{
						listItem.SubItems.Add("");
						//IPListView.Items.Add("");
					}

					IPListView.Items.Add(listItem);
					itemIndex += 1;

					if ((netStat == "Inactive") && (rdrStat == "Offline"))
						IPListView.Items[IPListView.Items.Count-1].ForeColor = System.Drawing.Color.Red;
					else if ((netStat == "Active") && (rdrStat == "Offline"))
						IPListView.Items[IPListView.Items.Count-1].ForeColor = System.Drawing.Color.Blue;
					else if (rdrStat == "Online")
						IPListView.Items[IPListView.Items.Count-1].ForeColor = System.Drawing.Color.Green;
					//IPListView.Items.Add(listItem);
					//itemIndex += 1;
				}
				myReader.Close();*/

			}//lock m_connection

			
		}

		private void UpdateIPListScreen()
		{
			if (m_connection == null)
			{
				MainForm.dbDisconnectedFlag = true;
				if (MainForm.reconnectCounter == -1)
					MainForm.reconnectCounter = 0;
				MsgDBLabel.ForeColor = System.Drawing.Color.Red;
				MsgDBLabel.Text = "Disconnected";
				ScanButton.Enabled = false;
				ConnectNetButton.Enabled = false;
				ResetRdrButton.Enabled = false;
				DisconnectNetButton.Enabled = false;
				ResetReaderButton.Enabled = false;
				ConnectButton.Enabled = false;
				CloseConnectButton.Enabled = false;

				return;
			}
			else if ((m_connection.State == System.Data.ConnectionState.Broken) ||
				(m_connection.State == System.Data.ConnectionState.Closed))
			{
				MainForm.dbDisconnectedFlag = true;
				if (MainForm.reconnectCounter == -1)
					MainForm.reconnectCounter = 0;
				MsgDBLabel.ForeColor = System.Drawing.Color.Red;
				MsgDBLabel.Text = "Disconnected";
				ScanButton.Enabled = false;
				ResetRdrButton.Enabled = false;
				DisconnectNetButton.Enabled = false;

				return;
			}

			//lock (m_connection)
		//{
			string mySelectQuery = "SELECT * From netip";
			OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection); 

			OdbcDataReader myReader = null; 
			
			try
			{
				myReader = myCommand.ExecuteReader();
				MainForm.reconnectCounter = -1;
				//timer1.Enabled = false;
			}
			catch (Exception ex)
			{
				int ret = 0, ret1 = 0, ret2 = 0;
				if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) || 
					((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
					((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
				{   
					if (MainForm.reconnectCounter == -1)
						MainForm.reconnectCounter = 0;
					MainForm.dbDisconnectedFlag = true;
                    
					MsgDBLabel.ForeColor = System.Drawing.Color.Red;
					MsgDBLabel.Text = "Disconnected";
					ScanButton.Enabled = false;
					ResetRdrButton.Enabled = false;
					DisconnectNetButton.Enabled = false;
				}

				if (myReader != null)
				{
					if (!myReader.IsClosed)
						myReader.Close();
				}
				return;
			} //catch ..try

			MainForm.reconnectCounter = -1;
			MainForm.dbDisconnectedFlag = false;
 
			ScanButton.Enabled = true;
			ResetRdrButton.Enabled = true;
			DisconnectNetButton.Enabled = true;
			MsgDBLabel.ForeColor = System.Drawing.Color.Blue;
			MsgDBLabel.Text = "Connected";

			IPListView.Items.Clear();
			
			uint itemIndex = 1;
			string netStat = "";
			string rdrStat = "";
			while (myReader.Read())
			{
				ListViewItem listItem = new ListViewItem(itemIndex.ToString());  //index
				listItem.SubItems.Add(myReader.GetString(0));  //ip
				listItem.SubItems.Add(myReader.GetString(1));  //rdrID
				listItem.SubItems.Add(myReader.GetString(2));  //hostID
				netStat = myReader.GetString(3);
				listItem.SubItems.Add(netStat);  //networkStatus
				rdrStat = myReader.GetString(4);
				listItem.SubItems.Add(rdrStat);  //rdrStatus
				
				try
				{
					listItem.SubItems.Add(myReader.GetDateTime(5).ToString("MM-dd-yyyy  HH:mm:ss"));  //connectTime
				}
				catch
				{
					listItem.SubItems.Add("");
				}

				IPListView.Items.Add(listItem);
				itemIndex += 1;

				if ((netStat == "Inactive") && (rdrStat == "Offline"))
					IPListView.Items[IPListView.Items.Count-1].ForeColor = System.Drawing.Color.Red;
				else if ((netStat == "Active") && (rdrStat == "Offline"))
					IPListView.Items[IPListView.Items.Count-1].ForeColor = System.Drawing.Color.Blue;
				else if (rdrStat == "Online")
					IPListView.Items[IPListView.Items.Count-1].ForeColor = System.Drawing.Color.Green;
			}
			myReader.Close();
		}

		
		public  void  WaitMessageThread()
		{
			int count = 0;
			while (true)
			{
				if (count == 0)
				{
					count = 1;
					waitDlg = new WaitForm(this);
					waitDlg.Show(); 
					waitDlg.StartTimer();
				}
			}
		}

		private void DBConnectionStatusHandler(status stat, OdbcConnection connect)
		{
			if (stat == status.open)
			{
				m_connection = connect;
				UpdateIPListScreen();
			}
			else if (stat == status.broken)
			{
				m_connection = null;
				if (MainForm.reconnectCounter < 0)
					MainForm.reconnectCounter = 0;
				MainForm.dbDisconnectedFlag = true;

				ScanButton.Enabled = false;
				ResetRdrButton.Enabled = false;
				DisconnectNetButton.Enabled = false;
				MsgDBLabel.ForeColor = System.Drawing.Color.Red;
				MsgDBLabel.Text = "Disconnected";
			}
			else if (stat == status.close)
			{
				m_connection = null;
				if (MainForm.reconnectCounter < 0)
					MainForm.reconnectCounter = 0;
				MainForm.dbDisconnectedFlag = true;

				ScanButton.Enabled = false;
				ResetRdrButton.Enabled = false;
				DisconnectNetButton.Enabled = false;
				MsgDBLabel.ForeColor = System.Drawing.Color.Red;
				MsgDBLabel.Text = "Disconnected";
			}
		}

		void UpdateCommunicationStat (string s, bool conn)
		{
			if (MainForm.dbDisconnectedFlag)
				return;

			if (conn)
			{
			   ConnectButton.Enabled = false;
			   CloseConnectButton.Enabled = true;
			   ResetReaderButton.Enabled = true;
			   MsgLabel.ForeColor = System.Drawing.Color.Blue;
			}
			else
			{
			   ConnectButton.Enabled = true;
			   CloseConnectButton.Enabled = false;
				MsgLabel.ForeColor = System.Drawing.Color.Red;
			}

			MsgLabel.Text = s;
		}

		private void UpdateIPList(string ip, int rdr, int host, string stat, string rdrStat, eventType e)
		{
			//problem with refreshing takes too long.
			//do not populate from the table - first check the ip in the list if it exists only
			//update that row if it does not exits add to the list. rome query from the table.
			
			//string netStat = "";
			//string readerStat = "";
			int index = 0;

			lock (updateListLock)
			{
				if ((e == eventType.sockConnect) || (e == eventType.powerup))
				{
					string s = "";
					if (e == eventType.powerup)
						s = ip + rdr + host + stat + rdrStat + "powerup";
					else 
						s = ip + rdr + host + stat + rdrStat + "sockConnect";
					
					lock(myLock)
					{

						//if (IsDupDisplay(s))
						//return;

						//IPListView.Items.Clear();
						
						//ScanButton.Enabled = false;
						//ConnectNetButton.Enabled = false;

						bool found = false;
						ListViewItem listItem;
						if (FindItem(ip, out index))
						{
							found = true;
							listItem = new ListViewItem(Convert.ToString(index+1));  //index
						}
						else
							listItem = new ListViewItem(Convert.ToString(IPListView.Items.Count-1));  //index	
						
						//listItem = new ListViewItem(Convert.ToString(IPListView.Items.Count+1));  //index

						listItem.SubItems.Add(ip);  //ip
						listItem.SubItems.Add(Convert.ToString(rdr));  //rdrID
						listItem.SubItems.Add(Convert.ToString(host));  //hostID
						
						listItem.SubItems.Add(stat);  
						listItem.SubItems.Add(rdrStat);  
						listItem.SubItems.Add(DateTime.Now.ToString("MM-dd-yyyy  HH:mm:ss"));  //connectTime
						if (!found)
						{
							//V5-CHG IPListView.Items.Add(listItem);
                            this.Invoke(new AddIpListviewItemCallback(this.AddIpListviewItem), new object[] { listItem });
							IPListView.Items[IPListView.Items.Count-1].Selected = true;
						}
						else
						{
							IPListView.Items.RemoveAt(index);
							IPListView.Items.Insert(index, listItem);
							IPListView.Items[index].Selected = true;
						}
						if (e == eventType.sockConnect)
						{
							if (stat == "Active")
							{
								if (SpecificIPRadioButton.Checked)
								{
									ScanButton.Enabled = false;
									ResetRdrButton.Enabled = true;
									DisconnectNetButton.Enabled = true;
								}
								else
								{
									ScanButton.Enabled = true;
									ResetRdrButton.Enabled = true;
									DisconnectNetButton.Enabled = true;
								}

								listItem.ForeColor = System.Drawing.Color.Blue;
							}
							else
							{
								if (SpecificIPRadioButton.Checked)
								{
									ScanButton.Enabled = true;
									ResetRdrButton.Enabled = false;
									DisconnectNetButton.Enabled = false;
								}
								else
								{
									ScanButton.Enabled = true;
									ResetRdrButton.Enabled = true;
									DisconnectNetButton.Enabled = true;
								}
								listItem.ForeColor = System.Drawing.Color.Black;
							}
						}
						else
						{
							if (rdrStat == "Online")
							{
								if (SpecificIPRadioButton.Checked)
								{
									ScanButton.Enabled = false;
									ResetRdrButton.Enabled = true;
									DisconnectNetButton.Enabled = true;
								}
								else
								{
									ScanButton.Enabled = true;
									ResetRdrButton.Enabled = true;
									DisconnectNetButton.Enabled = true;
								}

								listItem.ForeColor = System.Drawing.Color.Green;
							}
							else
							{
								if (SpecificIPRadioButton.Checked)
								{
									if (GetListSubItem(IPTextBox.Text, 4) == "Active")
									{
										ScanButton.Enabled = false;
										ResetRdrButton.Enabled = true;
										DisconnectNetButton.Enabled = true;
									}
									else
									{
										ScanButton.Enabled = true;
										ResetRdrButton.Enabled = true;
										DisconnectNetButton.Enabled = true;
									}
								}
								else
								{
									ScanButton.Enabled = true;
									ResetRdrButton.Enabled = true;
									DisconnectNetButton.Enabled = true;
								}

								listItem.ForeColor = System.Drawing.Color.Blue;
							}
						}
						listItem.EnsureVisible();
						
					}//mylock

				}
				else if (e == eventType.closeSocket) 
				{
					bool found = false;
					ListViewItem listItem;
					if (FindItem(ip, out index))
					{
						found = true;
						listItem = new ListViewItem(Convert.ToString(index+1));  //index
					}
					else
						listItem = new ListViewItem(Convert.ToString(IPListView.Items.Count-1));  //index
						
					if (SpecificIPRadioButton.Checked)
					{
						ScanButton.Enabled = true;
						ResetRdrButton.Enabled = false;
						DisconnectNetButton.Enabled = false;
					}
					else
					{
						ScanButton.Enabled = true;
						ResetRdrButton.Enabled = true;
						DisconnectNetButton.Enabled = true;
					}

					listItem.SubItems.Add(ip);  //ip
					listItem.SubItems.Add(Convert.ToString(rdr));  //rdrID
					listItem.SubItems.Add(Convert.ToString(host));  //hostID
						
					listItem.SubItems.Add("Inactive");  
					listItem.SubItems.Add("Offline");  
					listItem.SubItems.Add(DateTime.Now.ToString("MM-dd-yyyy  HH:mm:ss"));  //disconnectTime
					if (!found)
					{
						IPListView.Items.Add(listItem);
						IPListView.Items[IPListView.Items.Count-1].Selected = true;
					}
					else
					{
						IPListView.Items.RemoveAt(index);
						IPListView.Items.Insert(index, listItem);
						IPListView.Items[index].Selected = true;
					}
					
					listItem.ForeColor = System.Drawing.Color.Red;
					listItem.EnsureVisible();
				} 
			    else if (e == eventType.readerOffline) 
				{

					string s = ip + rdr + host + stat + rdrStat + "readerOffline";

					bool found = false;
					ListViewItem listItem;
					if (FindItem(ip, out index))
					{
						found = true;
						listItem = new ListViewItem(Convert.ToString(index+1));  //index
					}
					else
						listItem = new ListViewItem(Convert.ToString(IPListView.Items.Count-1));  //index
						
					listItem.SubItems.Add(ip);  //ip
					listItem.SubItems.Add(Convert.ToString(rdr));  //rdrID
					listItem.SubItems.Add(Convert.ToString(host));  //hostID
						
					listItem.SubItems.Add(stat);  
					listItem.SubItems.Add(rdrStat);  
					listItem.SubItems.Add(DateTime.Now.ToString("MM-dd-yyyy  HH:mm:ss"));  //offline time
					if (!found)
					{
						IPListView.Items.Add(listItem);
						IPListView.Items[IPListView.Items.Count-1].Selected = true;
					}
					else
					{
						IPListView.Items.RemoveAt(index);
						IPListView.Items.Insert(index, listItem);
						IPListView.Items[index].Selected = true;
					}
					
					listItem.ForeColor = System.Drawing.Color.Red;
					listItem.EnsureVisible();

					if (SpecificIPRadioButton.Checked)
					{
						if (GetListSubItem(IPTextBox.Text, 4) == "Active")
						{
							ScanButton.Enabled = false;
							ResetRdrButton.Enabled = true;
							DisconnectNetButton.Enabled = true;
						}
						else
						{
							ScanButton.Enabled = true;
							ResetRdrButton.Enabled = true;
							DisconnectNetButton.Enabled = true;
						}
					}
					else
					{
						ScanButton.Enabled = true;
						ResetRdrButton.Enabled = true;
						DisconnectNetButton.Enabled = true;
					}
				} 
				else  //scaning
				{
					if (!FindItem(ip, out index))
					{
						ListViewItem listItem = new ListViewItem(listIndex.ToString()); //ip
						//ListViewItem listItem = new ListViewItem(ip); //ip
						listItem.SubItems.Add(ip);  //ip
						listItem.SubItems.Add("");  //rdrID
						listItem.SubItems.Add("");  //hostID
						listItem.SubItems.Add(stat);  //netwotkStatus
						listItem.SubItems.Add(rdrStat);  //rdrStatus
						listItem.SubItems.Add(DateTime.Now.ToString("MM-dd-yyyy  HH:mm:ss"));  //connecTime
						listItem.ForeColor = System.Drawing.Color.Black;
						IPListView.Items.Add(listItem);
						listItem.EnsureVisible();
						listIndex += 1;

						ScanButton.Enabled = true;
						ResetRdrButton.Enabled = true;
						DisconnectNetButton.Enabled = true;
						
					}
					if (IPListView.Items.Count > 0)
						IPListView.Items[0].Selected = true;
				}

					/*IPListView.Items.Clear();
					MsgLabel.Text = "None";

					string mySelectQuery = "SELECT * From netip";
					OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection); 

					OdbcDataReader myReader = null;
					try
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
						return;				   
					}
					  
					listIndex = 1;
					while (myReader.Read())
					{
						//ListViewItem listItem = new ListViewItem(myReader.GetString(0));  //ip
						ListViewItem listItem = new ListViewItem(listIndex.ToString());  //index
		                listItem.SubItems.Add(myReader.GetString(0));  //ip
						listItem.SubItems.Add(myReader.GetString(1));  //rdrID
						listItem.SubItems.Add(myReader.GetString(2));  //hostID
						listItem.SubItems.Add(myReader.GetString(3));  //networkStatus
						listItem.SubItems.Add(myReader.GetString(4));  //rdrStatus
						listItem.SubItems.Add(myReader.GetString(5));  //connectTime
						listItem.ForeColor = System.Drawing.Color.Black;
						IPListView.Items.Add(listItem);
						listItem.EnsureVisible();
						listIndex += 1;
					}

					myReader.Close();

				}  //eventType.closeSocket
				else
				{
					if (!FindItem(ip, out index))
					{
						ListViewItem listItem = new ListViewItem(listIndex.ToString()); //ip
						//ListViewItem listItem = new ListViewItem(ip); //ip
						listItem.SubItems.Add(ip);  //ip
						listItem.SubItems.Add("");  //rdrID
						listItem.SubItems.Add("");  //hostID
						listItem.SubItems.Add(stat);  //netwotkStatus
						listItem.SubItems.Add(rdrStat);  //rdrStatus
						listItem.SubItems.Add(DateTime.Now.ToString());  //connecTime
						listItem.ForeColor = System.Drawing.Color.Black;
						IPListView.Items.Add(listItem);
						listItem.EnsureVisible();
						listIndex += 1;
					}
					if (IPListView.Items.Count > 0)
						IPListView.Items[0].Selected = true;
				}*/

			}//updateListLock
		}

		//private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		//{


		//}
			
		private bool FindItem(string item, out int index)
		{
		   index = -1;
		   for (int i=0; i<IPListView.Items.Count; i++)
		   {
			   try
			   {
				   if (IPListView.Items[i].SubItems[1].Text == item)
				   {
					   index = i;
					   return (true);
				   }
			   }
			   catch
			   {
                   continue;
			   }

		   }

		   return (false);
		}
		public CommForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//changes made to support MYSQL Server v5.0 and later
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

			//string connectionString = string.Format("server={0};port={1};username={2};password={3};database={4};pooling=false", "localhost", "3306", "root", "AWMYSQL", "cartracker");
			//string connectionString = string.Format("server={0};port={1};username={2};password={3};database={4};pooling=false", "localhost", "3306", "root", "", "parkingtracker");
            //Close();
            //m_connection = new MySqlConnection(connectionString);
		    //try 
            //{
              //m_connection.Open();
			  /*string ip = "200.200.195.1";
			  string s = "active";
			  string s1 = "online";
		      int rdr = 2;
		      int host = 1;
			  string sql = string.Format("INSERT INTO netip (IPAddress, ReaderID, HostID, NetworkStatus, RdrStatus) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", ip, rdr, host, s, s1);
			  //MySqlCommand myCommand = new MySqlCommand("INSERT INTO netip (IPAddress, ReaderID, HostID, NetworkStatus, RdrStatus) VALUES('110.110.110.110', 5, 3, 'active', 'online')");
			  MySqlCommand myCommand = new MySqlCommand(sql);
			  myCommand.Connection = m_connection;
			  
			  myCommand.ExecuteNonQuery();
			  //DBStatusBarPanel.Text = "Database : Connected";	*/
		     
            //}
            //catch (MySqlException e) 
            //{
              //MessageBox.Show(e.Message, "ParkingTracker Error Msg" );
			  //MsgLabel.Text = "Connection : DB connection failed";
           // }

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}


		private void ConnectButton_Click(object sender, System.EventArgs e)
		{
			mForm.PlaySound(1);

			uint port = Convert.ToUInt32(ComComboBox.Text);
			uint baud = Convert.ToUInt32(BaudComboBox.Text);
			if (OpenPort != null)
				OpenPort(baud, port);
			/*
			//int ret = mForm.OpenSerialPort(port, baud);
			int ret = communication.OpenSerialPort(baud, port);
			if (ret == 0)
			{
				MsgLabel.Text = "Connection : RS232 Connected.";
				MainForm.rs232Comm = true;
				Close();
			}
			else
			{
			   MessageBox.Show(this, "Opening Communication Port Faild.", "Car Tracker", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}*/
		}

		private bool IsIPListed(string ip)
		{
			for (int i=0; i<IPListView.Items.Count; i++)
			{
				if (IPListView.Items[i].SubItems[1].Text.ToString() == ip)
				{
					return(true);
				}
			}

			return (false);
		}

		private string GetListSubItem(string ip, short index)
		{
			//if index = 0 then ip will be returned
			for (int i=0; i<IPListView.Items.Count; i++)
			{
				string s = IPListView.Items[i].SubItems[1].Text.ToString();
				if (IPListView.Items[i].SubItems[1].Text.ToString() == ip)
				{
					s = IPListView.Items[i].SubItems[index].Text.ToString();
                    return(IPListView.Items[i].SubItems[index].Text.ToString());
				}
			}

			return("");

		}

		public void ScanButton_Click(object sender, System.EventArgs e)
		{
			//if (!AllIPRadioButton.Checked)
			//{
                //MessageBox.Show(this, "The Command Type is not supported with this Scan command. Please use 'All IPs'.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				//return;
			//}

			 mForm.PlaySound(1);

			if (AllIPRadioButton.Checked)
			{
				if (ScanNet != null)
				{
					//StringBuilder sql = new StringBuilder();
					//sql.Append("DELETE * FROM netip");         
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


					for (int i=0; i<100; i++)
					{
						MainForm.readerPowerup[i].online = false;
						MainForm.readerPowerup[i].counter = 0;
					}

					this.Cursor = Cursors.WaitCursor;
					for (int i=0; i<ipDisplayCollection.Count; i++)
						ipDisplayCollection.RemoveFrom(i);

					if (DisconnectSock != null)
					{
						byte[] ip = null;
						DisconnectSock(ip);
						//Thread.Sleep(1000);
					}

					IPListView.Items.Clear();
					listIndex = 1;
					
					//waitDlg = new WaitForm(this);
					//waitDlg.Show(); 
					//waitDlg.StartTimer();

                    //MyThread.Start();
					ScanNet(null);
					this.Cursor = Cursors.Default;
					//waitDlg.Close();
					//MyThread.Suspend();
					

					//waitDlg.Close();
					//ScanButton.Enabled = false;
				}
			}//AllIP
			else  //specific ip
			{
				if (IPTextBox.Text.Length > 0)
				{
					string ipstr = IPTextBox.Text;
					byte[] ip = new byte[16];

					for (int i=0; i<100; i++)
					{
						if (MainForm.readerPowerup[i].ip == ipstr)
						{
							MainForm.readerPowerup[i].online = false;
							MainForm.readerPowerup[i].counter = 0;
							break;
						}
					}

					for (int j=0; j<ipstr.Length; j++)
						ip[j] = Convert.ToByte(ipstr[j]);

					if (DisconnectSock != null)
					{
						DisconnectSock(ip);
						Thread.Sleep(150);
					}

					if (ScanNet != null)
					{
						ScanNet(ip);
						Thread.Sleep(150);
					}

				}
				else
				{
					ShowErrorMessage("Need ip address");
				}
			}


			/* The following code was commented out because API needs to be modified
			   to fully suport these different scenerios

			listIndex = 1;
			if (SelectedIPRadioButton.Checked)
			{
				if ((IPListView.Items.Count > 0) && (IPListView.SelectedItems.Count >= 1))
				{
					ListViewItem item;
					byte[] ip = new byte[16];
					string ipstr;

					for (int i=0; i<IPListView.SelectedItems.Count; i++)
					{
						item = IPListView.SelectedItems[i];
						ipstr = item.SubItems[1].Text;
					
						for (int j=0; j<ipstr.Length; j++)
							ip[j] = Convert.ToByte(ipstr[j]);
					
						if (ScanNet != null)
						{
							ScanNet(ip);
							Thread.Sleep(50);
						}
					}
				}
				else
				{
					ShowErrorMessage("No item is selected");
				}
			}
			else if (SpecificIPRadioButton.Checked)
			{
				if (IPTextBox.Text.Length > 0)
				{
					string ipstr = IPTextBox.Text;
					byte[] ip = new byte[16];

					for (int j=0; j<ipstr.Length; j++)
						ip[j] = Convert.ToByte(ipstr[j]);

					if (ScanNet != null)
					{
						ScanNet(ip);
					}

				}
				else
				{
					ShowErrorMessage("Need ip address");
				}
			}
			else
			{
				mForm.PlaySound(1);

				if (ScanNet != null)
				{
					//StringBuilder sql = new StringBuilder();
					//sql.Append("DELETE * FROM netip");         
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

					this.Cursor = Cursors.WaitCursor;
					for (int i=0; i<ipDisplayCollection.Count; i++)
						ipDisplayCollection.RemoveFrom(i);

					IPListView.Items.Clear();
					listIndex = 1;
					ScanNet(null);
					this.Cursor = Cursors.Default;
				}
			}
			

		   //int ret = mForm.ScanNetwork();
			//if (ret == 0)
				//MsgLabel.Text = "Connection : RS232 Connected.";
			
			*/ //the code was commented out
		}

		/*private void ConnectDB()
		{
		    //string connectionString = string.Format("server={0};port={1};username={2};password={3};database={4};pooling=false", "localhost", "3306", "root", "AWMYSQL", "cartracker");
            string connectionString = string.Format("server={0};port={1};username={2};password={3};database={4};pooling=false", "localhost", "3306", "root", "", "parkingtracker");					
			//Close();
            m_connection = new OdbcConnection(connectionString);
		    try 
            {
			    //myCommand.Connection = m_connection;
				m_connection.Open();
				//myCommand.ExecuteNonQuery();
				//DBStatusBarPanel.Text = "Database : Connected";
		     
            }
            catch (Exception e) 
            {
              MessageBox.Show(e.Message, "CarTracker Error Msg" );
			  MsgLabel.Text = "Connection : DB connection failed";
            }
		}*/

		/*private void CloseConnection()
        {
         if (m_connection != null)
         {
            m_connection.Close();
            m_connection.Dispose();
            m_connection = null;
         }
       }*/

		private void CommForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
		   //CloseConnection();
		   //MsgLabel.Text = "Connection : Comm port Closed";
		}

		private void CloseConnectButton_Click(object sender, System.EventArgs e)
		{
		   mForm.PlaySound(1);

		   if (mForm.CloseSerialPort() >= 0)
		   {
			  MainForm.rs232Comm = false;
			  ConnectButton.Enabled = true;
		      ResetReaderButton.Enabled = false;
			  CloseConnectButton.Enabled = false;


			   if (MainForm.m_connection != null)
			   {
				   StringBuilder sql = new StringBuilder();
				   sql.Append("UPDATE zones SET Status = ");
				   sql.AppendFormat("('{0}')", "Offline");
				   
				   OdbcCommand myCommand = new OdbcCommand(sql.ToString(), MainForm.m_connection);
				
				   lock (MainForm.m_connection)
				   {
					   try
					   {
						   myCommand.ExecuteNonQuery();
					   }
					   catch (Exception ex)
					   {
						   //ShowErrorMessage(ex.Message);
						   return;
					   }
					   //}

					   //FIX ME mForm.awiHistoryControl1.UpdateZoneViewPage();
				   }// lock m_connection
			   }
		   }
		}

		private void ConnectNetButton_Click(object sender, System.EventArgs e)
		{
			if (!AllIPRadioButton.Checked)
			{
				MessageBox.Show(this, "The Command Type is not supported with this Connect command. Please use 'All IPs'.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}

			mForm.PlaySound(1);
			if (ConnectSock != null)
				ConnectSock();

			/* The following code was commented out. need API suport
		    mForm.PlaySound(1);
		    listIndex = 1;
			if (SelectedIPRadioButton.Checked)
			{
				if ((IPListView.Items.Count > 0) && (IPListView.SelectedItems.Count >= 1))
				{
					ListViewItem item;
					byte[] ip = new byte[16];
					string ipstr;

					for (int i=0; i<IPListView.SelectedItems.Count; i++)
					{
						item = IPListView.SelectedItems[i];
						ipstr = item.SubItems[1].Text;
					
						for (int j=0; j<ipstr.Length; j++)
							ip[j] = Convert.ToByte(ipstr[j]);
					
						if (ConnectThisSock != null)
						{
							ConnectThisSock(ip);
							Thread.Sleep(300);
						}
					}
				}
				else
				{
                   ShowErrorMessage("No item is selected");
				}
			}
			else if (SpecificIPRadioButton.Checked)
			{
				if (IPTextBox.Text.Length > 0)
				{
					string ipstr = IPTextBox.Text;
					byte[] ip = new byte[16];

					for (int j=0; j<ipstr.Length; j++)
						ip[j] = Convert.ToByte(ipstr[j]);

					if (ConnectThisSock != null)
					{
						ConnectThisSock(ip);
					}

				}
				else
				{
                   ShowErrorMessage("Need ip address");
				}
			}
		    else
		    {
				 if (ConnectSock != null)
			        ConnectSock();
		    }*/
		}

		private void tabControl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (tabControl.SelectedIndex == 0) //TabPages[0].Text == "Network")
			{
			   //MsgDBLabel.Visible = false;  //rs232
			   MsgLabel.Visible = true;
			   CommLabel.Visible = true;
			   //DBLabel.Visible = false;
			   //MsgLabel.Visible = true;
			}
			else //network
			{
				MsgLabel.Visible = false;
				CommLabel.Visible = false;
                //MsgDBLabel.Visible = false;
			    //MsgDBLabel.Visible = true;
				//DBLabel.Visible = true;
			}

		}

		private void ShowErrorMessage(string msg)
		{
			MessageBox.Show(this, msg, "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void ResetRdrButton_Click(object sender, System.EventArgs e)
		{
		   mForm.PlaySound(1);
		   //listIndex = 1;
			
			if (SelectedIPRadioButton.Checked)  //selected
			{
				if ((IPListView.Items.Count > 0) && (IPListView.SelectedItems.Count >= 1))
				{
					ListViewItem item;
					byte[] ip = new byte[16];
					char[] buf = new char[16];
					string ipstr;
					//int j = 0;

					for (int i=0; i<IPListView.SelectedItems.Count; i++)
					{
						item = IPListView.SelectedItems[i];
						ipstr = item.SubItems[1].Text;
						if (ipstr.Length > 0)
						   selectedIPStr = ipstr;
						else
                           selectedIPStr = "";
					
						for (int j=0; j<ipstr.Length; j++)
							ip[j] = Convert.ToByte(ipstr[j]);

						for (int k=0; k<100; k++)
						{
							if (MainForm.readerPowerup[k].ip == ipstr)
							{
								MainForm.readerPowerup[k].online = false;
								MainForm.readerPowerup[k].counter = 0;
								break;
							}
						}

						if (ResetSocketReaderEvent != null)
						{
							ResetSocketReaderEvent(ip);
							timer2.Interval = 3000;
							//timer2.Enabled = true;
							Thread.Sleep(200);
						}
					}
				}
				else
				{
					ShowErrorMessage("No item is selected");
				}
			}
			else if (SpecificIPRadioButton.Checked)  //specific
			{
				lastIndex = 0;
				tryCount = 0;
				curIndex = 0;
				lastip = "";

				if (IPTextBox.Text.Length > 0)
				{
					string ipstr = IPTextBox.Text;
					selectedIPStr = ipstr;
					byte[] ip = new byte[16];

					for (int j=0; j<ipstr.Length; j++)
						ip[j] = Convert.ToByte(ipstr[j]);

					for (int i=0; i<100; i++)
					{
						if (MainForm.readerPowerup[i].ip == ipstr)
						{
							MainForm.readerPowerup[i].online = false;
							MainForm.readerPowerup[i].counter = 0;
							break;
						}
					}

					if (ResetSocketReaderEvent != null)
					{
						int ret = ResetSocketReaderEvent(ip);
						if (ret == -173)
						{
                            int idx = GetIpIndex(ipstr);
							if (idx >= 0)
							{
                                IPListView.Items[idx].SubItems[4].Text = "Inactive";
								IPListView.Items[idx].SubItems[5].Text = "Ofline";

								ScanButton.Enabled = true;
								ResetRdrButton.Enabled = false;
								DisconnectNetButton.Enabled = false;
							}
						}

						timer2.Interval = 3000;
						//timer2.Enabled = true;
					}

				}
				else
				{
					ShowErrorMessage("Need ip address");
				}
			}
			else //all ips
			{
				//numReaders = 100;
				for (int i=0; i<100; i++)
				{
					MainForm.readerPowerup[i].online = false;
					MainForm.readerPowerup[i].counter = 0;
				}

				//////////////////////////
				for (int i=0; i<IPListView.Items.Count; i++)
				{
					ListViewItem item;
					byte[] ip = new byte[16];
					char[] buf = new char[16];
					string ipstr;
				
				    item = IPListView.Items[i]; //IPListView.SelectedItems[i];
					ipstr = item.SubItems[1].Text;
				
					for (int j=0; j<ipstr.Length; j++)
						ip[j] = Convert.ToByte(ipstr[j]);

					if (ResetSocketReaderEvent != null)
					{
						ResetSocketReaderEvent(ip);
						numips += 1;
						Console.WriteLine("First Time IP SENT : " + ipstr);
						//Thread.Sleep(250); 
					}
	
				}

				MainForm.timerStop = true;  //stop enable reader timer
				lastIndex = 0;
				tryCount = 0;
				curIndex = 0;
				lastip = "";
				timer2.Interval = 3000 + IPListView.Items.Count * 100;
				//timer2.Enabled = true;
				
			}//allips
		}

		private void ResetReaderButton_Click(object sender, System.EventArgs e)
		{
		   mForm.PlaySound(1);

		   if (m_resetAllReaders != null)
		   {
			  m_resetAllReaders();
		   }

           /*static void WriteQuote(XmlWriter writer, string symbol, 
                                    double price, double change, long volume)
            {
               writer.WriteStartElement("Stock");
               writer.WriteAttributeString("Symbol", symbol);
               writer.WriteElementString("Price", XmlConvert.ToString(price));
               writer.WriteElementString("Change", XmlConvert.ToString(change));
               writer.WriteElementString("Volume", XmlConvert.ToString(volume));
               writer.WriteEndElement();
            }*/


           XmlTextWriter writer = new XmlTextWriter("C:\\test.xml", new System.Text.UTF8Encoding());
           writer.Formatting = Formatting.Indented;
           writer.Indentation = 4;
           writer.WriteStartDocument();
           writer.WriteComment("This is a comment");

           writer.WriteStartElement("DBConnection");
           //writer.WriteStartAttribute("prefix", "attrName", "myns");
           //writer.WriteEndAttribute();
           writer.WriteStartElement("Database");
           writer.WriteElementString("dbProvider", "MySQL");
           writer.WriteElementString("dbDriver", "DRIVER={MySQL ODBC 3.51 Driver};");
           writer.WriteElementString("dbName", "bank");
           writer.WriteEndElement();

           writer.WriteStartElement("Server");
           writer.WriteElementString("serverName", "192.168.1.100");
           writer.WriteEndElement();
           
           writer.WriteEndElement();
           writer.WriteEndDocument();
           writer.Flush();

		}

		private void DisconnectNetButton_Click(object sender, System.EventArgs e)
		{
			string ipstr;
			byte[] ip = new byte[16];
			char[] buf = new char[16];
			ListViewItem item = null;

			 mForm.PlaySound(1);

			if (AllIPRadioButton.Checked) //ALL
			{
				if (DisconnectSock != null)
				{
					if (MessageBox.Show(this, "Disconnect All?", "Bank", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
						return;
					byte[] ipp = null;
					DisconnectSock(ipp);
					ScanButton.Enabled = true;
				}
			}
			else if (SelectedIPRadioButton.Checked)  //SELECTED
			{
				if ((IPListView.Items.Count > 0) && (IPListView.SelectedItems.Count >= 1))
				{
					for (int i=0; i<IPListView.SelectedItems.Count; i++)
					{
						item = IPListView.SelectedItems[i];
						ipstr = item.SubItems[1].Text;
					
						for (int j=0; j<ipstr.Length; j++)
							ip[j] = Convert.ToByte(ipstr[j]);

						if (DisconnectSock != null)
						{
							DisconnectSock(ip);
							Thread.Sleep(200);
						}
					}
				}
				else
				{
					ShowErrorMessage("No item is selected");
				}
			}
			else   //SPECIFIC
			{
				if (IPTextBox.Text.Length > 0)
				{
					ipstr = IPTextBox.Text;
					for (int j=0; j<ipstr.Length; j++)
						ip[j] = Convert.ToByte(ipstr[j]);

					if (DisconnectSock != null)
					{
						DisconnectSock(ip);
					}

				}
				else
				{
					ShowErrorMessage("Need ip address");
				}
			}

			/* the following code was commentedout. need api suport
			mForm.PlaySound(1);
			ListViewItem item;
			byte[] ip = new byte[16];
			char[] buf = new char[16];
			string ipstr = "";
			
			ScanButton.Enabled = true;
			ConnectNetButton.Enabled = true;
			ResetRdrButton.Enabled = true;


			if (SelectedIPRadioButton.Checked)
			{
				if ((IPListView.Items.Count > 0) && (IPListView.SelectedItems.Count >= 1))
				{
					for (int i=0; i<IPListView.SelectedItems.Count; i++)
					{
						item = IPListView.SelectedItems[i];
						ipstr = item.SubItems[1].Text;
					
						for (int j=0; j<ipstr.Length; j++)
							ip[j] = Convert.ToByte(ipstr[j]);

						if (DisconnectSock != null)
						{
							DisconnectSock(ip);
							Thread.Sleep(200);
						}
					}
				}
				else
				{
					ShowErrorMessage("No item is selected");
				}
			}
			else if (SpecificIPRadioButton.Checked)
			{
				if (IPTextBox.Text.Length > 0)
				{
					//string ipstr = IPTextBox.Text;
					//byte[] ip = new byte[16];

					for (int j=0; j<ipstr.Length; j++)
						ip[j] = Convert.ToByte(ipstr[j]);

					if (DisconnectSock != null)
					{
						DisconnectSock(ip);
					}

				}
				else
				{
					ShowErrorMessage("Need ip address");
				}
			}
			else
			{
				if (DisconnectSock != null)
				{
					if (MessageBox.Show(this, "Disconnect All?", "Bank", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
						return;
					ip = null;
					DisconnectSock(ip);
				}
			}*/  //the following code was comented out

		   /*
		   mForm.PlaySound(1);
		   listIndex = 1;

		   if (DisconnectSock != null)
		   {
			  DisconnectSock();
		      MainForm.socketsConnection = false;
		      ScanButton.Enabled = true;
			  ConnectNetButton.Enabled = true;
			  mForm.listIsUpdated = false;

		      for (int i=0; i<ipDisplayCollection.Count; i++)
				   ipDisplayCollection.RemoveFrom(i);
		   }*/
		}

		bool IsDupDisplay(string s)
		{
		   //string s = "";
		   //for (int i=0; i<=ct; i++)
		      //s += listItem.SubItems[i].Text.ToString();
		   
			  tagDisplayInfoStruct ipInfo = new tagDisplayInfoStruct();
			  ipInfo.str = s;
		      
			  int index = 0;
			  if ((index=ipDisplayCollection.Exits(ipInfo)) >= 0)
			     return true;
			  else
			  {
			     ipDisplayCollection.Add(ipInfo);
			     return false;
			  }
		}

		private void IPListView_DoubleClick(object sender, System.EventArgs e)
		{
			ListViewItem item = IPListView.SelectedItems[0];
			IPTextBox.Text = item.SubItems[1].Text;
		}

		private void AllIPRadioButton_Click(object sender, System.EventArgs e)
		{
		   //IPTextBox.Text = "";
		   IPTextBox.ReadOnly = true;
			
			ScanButton.Enabled = true;
			ResetRdrButton.Enabled = true;
			DisconnectNetButton.Enabled = true;
			
		}

		private void SpecificIPRadioButton_Click(object sender, System.EventArgs e)
		{
			IPTextBox.ReadOnly = false;
			if (SpecificIPRadioButton.Checked)
			{
				if (IPTextBox.Text.Length == 0)
				{
					ScanButton.Enabled = false;
					ResetRdrButton.Enabled = false;
					DisconnectNetButton.Enabled = false;
				}
				else
				{   
					if (!IsIPListed(IPTextBox.Text))  //no ip found
					{
						ScanButton.Enabled = true;
						ResetRdrButton.Enabled = false;
						DisconnectNetButton.Enabled = false;
					}
					else   //ip is in listview
					{
						if (GetListSubItem(IPTextBox.Text, 4) == "Active")  //network active
						{
							ScanButton.Enabled = false;
							ResetRdrButton.Enabled = true;
							DisconnectNetButton.Enabled = true;
						}
						else
						{
							ScanButton.Enabled = true;
							ResetRdrButton.Enabled = false;
							DisconnectNetButton.Enabled = false;
						}

					}
				}
			}
		}

		private void IPListView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		//private void StartTimer1()
		//{
           //timer1.Enabled = true;
		//}

		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			/*if (numips >= numitems)
			{
                timer1.Enabled = false;
				lastIndex = 0;
				tryCount = 0;
				lastip = "";
				timer2.Enabled = true;
				return;
			}*/


			/*if (numitems > 0) //&& (IPListView.SelectedItems.Count >= 1))
			{
				ListViewItem item;
				byte[] ip = new byte[16];
				char[] buf = new char[16];
				string ipstr;
				
				item = IPListView.Items[numips]; //IPListView.SelectedItems[i];
				ipstr = item.SubItems[1].Text;
				
				for (int j=0; j<ipstr.Length; j++)
					ip[j] = Convert.ToByte(ipstr[j]);

				if (ResetSocketReaderEvent != null)
				{
					ResetSocketReaderEvent(ip);
					numips += 1;
					Console.WriteLine("IP SENT : " + ipstr);
					//timer1.Enabled = false; 
				}
	
			}*/
		}

		private int GetIpIndex(string ip)
		{
			for (int i=0; i<IPListView.Items.Count; i++)
			{
				if (IPListView.Items[i].SubItems[1].Text.ToString() == ip)
					return (i);
			}

			return (-1);
		}

		private int GetItemIndex(string lastip, int startIndex)
		{
			ListViewItem item;
				
			for (int i=startIndex; i<IPListView.Items.Count; i++)
			{
				item = IPListView.Items[i]; //IPListView.SelectedItems[i];
				if (item.SubItems[5].Text == "Offline")
					return (i);
			}
            
			return (-1);
		}

		private int GetSelectedItemIndex(string lastip, int startIndex)
		{
			ListViewItem item;
				
			for (int i=startIndex; i<IPListView.SelectedItems.Count; i++)
			{
				item = IPListView.SelectedItems[i]; //IPListView.SelectedItems[i];
				if (item.SubItems[5].Text == "Offline")
					return (i);
			}
            
			return (-1);
		}

		private bool IsIpOnline(string ip)
		{
			ListViewItem item;
				
			for (int i=0; i<IPListView.Items.Count; i++)
			{
				item = IPListView.SelectedItems[i]; //IPListView.SelectedItems[i];
				if (item.SubItems[1].Text == ip)
				{
					if (item.SubItems[5].Text == "Online")
						return true;
					else
						return false;
				}
			}
            
			return false;
		}

		private void timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (MainForm.m_connection == null)
			    return;

            byte[] ip = new byte[16];
			bool stopTimer = true;
			int startloop = 0;

			//lock (MainForm.powerupLocker)
			lock (MainForm.m_connection)  //dec-06-06
			{
				for (int i=0; i<IPListView.Items.Count; i++)
				{
					if ((MainForm.readerPowerup[i].ip != "") && (!MainForm.readerPowerup[i].online) && (MainForm.readerPowerup[i].counter < 3))
					{
						stopTimer = false;
						break;
					}
				}

				if (stopTimer)
				{
					timer2.Enabled = false;
					MainForm.timerStop = false;  //restart enable reader timer
					return;
				}

				if (AllIPRadioButton.Checked)
				{
					if (curIndex == -1) //if first time
						startloop = 0;
					else
					{
						if (curIndex > IPListView.Items.Count)
						{
							curIndex = -1;
							return;
						}
						else
						{
                           startloop = curIndex;
						}
					}

					for (int i=startloop; i<IPListView.Items.Count; i++)
					{
						if ((!MainForm.readerPowerup[i].online) && (MainForm.readerPowerup[i].counter < 3))
						{
							for (int j=0; j<MainForm.readerPowerup[i].ip.Length; j++)
								ip[j] = Convert.ToByte(MainForm.readerPowerup[i].ip[j]);

							if (ResetSocketReaderEvent != null)
							{
								MainForm.readerPowerup[i].counter += 1;
								ResetSocketReaderEvent(ip);
								Console.WriteLine("FORM1 - timer2 : " + MainForm.readerPowerup[i].ip + "  counter: " + MainForm.readerPowerup[i].counter.ToString() ); 
							}

							curIndex = i;
							return;
						}
					}//for i loop
				}//if all ips
				else if (SpecificIPRadioButton.Checked)  //specific
				{
					if (IPTextBox.Text.Length <= 0)
					{
						timer2.Enabled = false;
						MainForm.timerStop = false;  //restart enable reader timer
						return; 
					}

					string ipStr = IPTextBox.Text;

					for (int i=0; i<100; i++)
					{
						if ((MainForm.readerPowerup[i].ip == ipStr) && 
							(MainForm.readerPowerup[i].online == false) &&
							(MainForm.readerPowerup[i].counter < 3))
						{
							for (int j=0; j<ipStr.Length; j++)
								ip[j] = Convert.ToByte(ipStr[j]);

							if (ResetSocketReaderEvent != null)
							{
								MainForm.readerPowerup[i].counter += 1;
								ResetSocketReaderEvent(ip);
								Console.WriteLine("FORM1 - timer2 : " + MainForm.readerPowerup[i].ip + "  counter: " + MainForm.readerPowerup[i].counter.ToString() ); 
							}

							return;
						}
					}//for i loop
				}//specific
				else   //selected
				{
					string ipStr = selectedIPStr;
					if (ipStr.Length <= 0)
					{
						timer2.Enabled = false;
						MainForm.timerStop = false;  //restart enable reader timer
						return; 
					}

					for (int i=0; i<100; i++)
					{
						if ((MainForm.readerPowerup[i].ip == ipStr) && 
							(MainForm.readerPowerup[i].online == false) &&
							(MainForm.readerPowerup[i].counter < 3))
						{
							for (int j=0; j<ipStr.Length; j++)
								ip[j] = Convert.ToByte(ipStr[j]);

							if (ResetSocketReaderEvent != null)
							{
								MainForm.readerPowerup[i].counter += 1;
								ResetSocketReaderEvent(ip);
								Console.WriteLine("FORM1 - timer2 : " + MainForm.readerPowerup[i].ip + "  counter: " + MainForm.readerPowerup[i].counter.ToString() ); 
							}

							return;
						}
					}//for i loop
				}//selected

				timer2.Enabled = false;
				MainForm.timerStop = false;  //restart enable reader timer

				////////////////////////////////////////////////////////
				//int CurIndex = 0;
				/*byte[] ip = new byte[16];
				ListViewItem item = null;
				string ipstr;

				timer2.Interval = 2000;

				if (tryCount >= 3)
					curIndex = lastIndex+1;

				if (AllIPRadioButton.Checked)
				{
					for (int i=0; i<100; i++)
					{
						if ((!MainForm.readerPowerup[i].online) && (!MainForm.readerPowerup[i].counter < 3))
							stopTimer = false;
					}

					if ((curIndex=GetItemIndex(lastip, curIndex)) >= 0)
						item = IPListView.Items[curIndex];
					else
					{
						timer2.Enabled = false;
						return;
					}
				}
				else if (SpecificIPRadioButton.Checked)
				{
					if (IsIpOnline(IPTextBox.Text) || (tryCount >= 3)) //this ip came online
					{
						timer2.Enabled = false;  //we have gone through all selected ips
						return;
					}
				}
				else
				{
					//if ((curIndex=GetSelectedItemIndex(lastip, curIndex)) >= 0)
					if (curIndex < IPListView.SelectedItems.Count) 
					{
						if (tryCount >= 1) //making sure we have gone though this ip at least once
						{
							item = IPListView.SelectedItems[curIndex];
							if (IsIpOnline(item.SubItems[1].Text)) //this ip came online
							{
								curIndex += 1; //increment index and do next ip
								if (curIndex >= IPListView.SelectedItems.Count)
								{
									timer2.Enabled = false;  //we have gone through all selected ips
									return;
								}
							}
						}
					}
					else
					{
						timer2.Enabled = false;
						return;
					}
				}

				//if ((curIndex=GetItemIndex(lastip, curIndex)) >= 0)
				//{
					//ListViewItem item = IPListView.Items[curIndex]; //IPListView.SelectedItems[i];
				
				if (SelectedIPRadioButton.Checked)
				{
					item = IPListView.SelectedItems[curIndex];
					ipstr = item.SubItems[1].Text;
				}
				else if (SpecificIPRadioButton.Checked)
					ipstr = IPTextBox.Text;
				else
					ipstr = item.SubItems[1].Text;

				if (lastip == ipstr)
					tryCount += 1;
				else
					tryCount = 0;
				lastIndex = curIndex;
				lastip = ipstr;
				for (int j=0; j<ipstr.Length; j++)
					ip[j] = Convert.ToByte(ipstr[j]);

				if (ResetSocketReaderEvent != null)
				{
					ResetSocketReaderEvent(ip);
					Console.WriteLine("FORM1 - IP RESENT : " + ipstr + "  counter: " + tryCount.ToString() ); 
				}
				//}
				//else
					//timer2.Enabled = false; */
			}//lock m_connection

		}

		private void ResetRdrButton_Click_1(object sender, System.EventArgs e)
		{
		
		}

		private void SelectedIPRadioButton_Click(object sender, System.EventArgs e)
		{
			IPTextBox.ReadOnly = true;
		}

		private void timer3_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			/*if (MainForm.reconnectCounter == 3)
			{
				timer3.Enabled = false;
				MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (MainForm.reconnectCounter > 3)
			{
				timer3.Enabled = false;	
			}
			else
			{
				MainForm.reconnectCounter += 1;
				lock (MainForm.m_connection)
				{
					if(mForm.ReconnectToDBServer())
					{
						timer3.Enabled = false;
						MainForm.reconnectCounter = -1;
						MsgDBLabel.ForeColor = System.Drawing.Color.Blue;
						MsgDBLabel.Text = "Connected";
					}
				}
			}*/
		}

		private void SpecificIPRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

        private void AddIpListviewItem(ListViewItem item)
        {
            IPListView.Items.Add(item);
        }

		private void IPTextBox_TextChanged(object sender, System.EventArgs e)
		{
			//if (IPTextBox.Focused)
			{
				if (!IsIPListed(IPTextBox.Text))  //no ip found
				{
					ScanButton.Enabled = true;
					ResetRdrButton.Enabled = false;
					DisconnectNetButton.Enabled = false;
				}
				else   //ip is in listview
				{
					if (GetListSubItem(IPTextBox.Text, 4) == "Active")  //network active
					{
						ScanButton.Enabled = false;
						ResetRdrButton.Enabled = true;
						DisconnectNetButton.Enabled = true;
					}
					else
					{
						ScanButton.Enabled = true;
						ResetRdrButton.Enabled = false;
						DisconnectNetButton.Enabled = false;
					}

				}
			}//focus
		}

	}//comform
}//namespace
