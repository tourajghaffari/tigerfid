using System;
using System.Drawing;
using System.Globalization;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

using Microsoft.Win32;
using AWIComponentLib.Communication;
using AWIComponentLib.Database;
using AWI.SmartTracker;

namespace AWI.Comm
{
	public delegate void Scan(byte[] ip);
	public delegate void ConnectSocket();
	public delegate void ConnectThisSocket(byte[] ip);
	public delegate void DisconnectSocket(byte[] ip);
	public delegate int ResetAllReaders();
	public delegate int ResetReader(ushort rdr, ushort host);
	public delegate int ResetSocketReader(byte[] ip);
	public delegate int OpenSerialPort(uint baud, uint port);
	public delegate int CloseSerialPort();



	public class CommForm : System.Windows.Forms.Form
	{
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
		public event Scan ScanNet;
		public event ConnectSocket ConnectSock;
		public event ConnectThisSocket ConnectThisSock;
		public event DisconnectSocket DisconnectSock;
		public event OpenSerialPort OpenPort;
		public event CloseSerialPort ClosePort;
		public event ResetAllReaders m_resetAllReaders;
		public event ResetReader ResetReaderEvent;
		public event ResetSocketReader ResetSocketReaderEvent;
		private MainForm mForm;
		private System.Windows.Forms.Button CloseConnectButton;
		private System.ComponentModel.IContainer components;
		private Object myLock = new Object();
		private object updateListLock = new object();
		private tagDisplayCollectionClass ipDisplayCollection = new tagDisplayCollectionClass();
		//private tagDisplayInfoStruct tagDisplayInfo;
		private System.Windows.Forms.Button ResetReaderButton;
		private System.Windows.Forms.Label MsgDBLabel;
		private System.Windows.Forms.Label DBLabel;
		private System.Windows.Forms.Label CommLabel;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton AllIPRadioButton;
		private System.Windows.Forms.RadioButton SelectedIPRadioButton;
		private System.Timers.Timer timer1;
		private uint listIndex;
		private int numitems;
		private System.Timers.Timer timer2;
		private int numips;
		private int lastIndex;
		private int tryCount;
		private string lastip;
		private System.Windows.Forms.TextBox IPTextBox;
		private System.Windows.Forms.RadioButton SpecificIPRadioButton;
		private System.Windows.Forms.Button ResetRdrButton;
		private int curIndex;
		private System.Timers.Timer timer3;
		private string selectedIPStr;
		private System.Windows.Forms.CheckBox AnyRdrCheckBox;
		private uint port;

        private string ConnString;

        public CommForm(MainForm form)
        {
            ConnString = string.Format("DRIVER={{MySQL ODBC 3.51 Driver}};SERVER={0};DATABASE={1};USER={2};PASSWORD={3};OPTION=3;", MainForm.serverMySQL, MainForm.database, MainForm.user, MainForm.password);

            mForm = form;
            numitems = 0;
            numips = 0;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

            InitializeComponent();

            form.m_updateIPListView += new UpdateIPListView(this.UpdateIPList);  //dec-06-06
            form.m_updateCommStatus += new UpdateCommStatus(this.UpdateCommunicationStat);
            MainForm.m_closeWindowEvent += new CloseWindowDelegate(this.CloseWindow);

            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Active Wave\\Smart Tracker\\");
            port = Convert.ToUInt32(reg.GetValue("port"));
            ComComboBox.Text = Convert.ToString(port);

            if (MainForm.rs232Comm)
            {
                ConnectButton.Enabled = false;
                CloseConnectButton.Enabled = true;
                ResetReaderButton.Enabled = true;
                if (form.rs232Reader > 0)
                    MsgLabel.Text = "Connected,  Reader: Online";
                else
                    MsgLabel.Text = "RS232 Connected.";
            }
            else
            {
                ConnectButton.Enabled = true;
                CloseConnectButton.Enabled = false;
            }

            //if (MainForm.m_connection == null)
            //if (m_connection == null)
            //{
            //    MsgDBLabel.ForeColor = System.Drawing.Color.Red;
            //    MsgDBLabel.Text = "Disconnected";
            //    ScanButton.Enabled = false;
            //    ConnectNetButton.Enabled = false;
            //    ResetRdrButton.Enabled = false;
            //    DisconnectNetButton.Enabled = false;
            //    ResetReaderButton.Enabled = false;
            //    ConnectButton.Enabled = false;
            //    CloseConnectButton.Enabled = false;
            //    return;
            //}

            //lock (MainForm.m_connection)  //dec-06-06
            //lock (m_connection)  //dec-06-06
            {
                MsgDBLabel.ForeColor = System.Drawing.Color.Blue;
                MsgDBLabel.Text = "Connected";

                //need to change the query statement to include fields from zone table (rdr and rdr status)
                //may be netip.zone = zone.ID to get those fields out.
                //string mySelectQuery = "SELECT netip.IPAddress, zones.ReaderID, netip.HostID, netip.NetworkStatus, zones.status, netip.ConnectTime From netip, zones WHERE netip.ZoneID = zones.ID";
                //OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection); 

                string mySelectQuery = "SELECT netip.IPAddress, zones.ReaderID, netip.HostID, netip.NetworkStatus, zones.status, netip.ConnectTime From netip LEFT JOIN zones ON netip.ZoneID = zones.ID";   //, zones WHERE netip.ZoneID = zones.ID";
                //>>>OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection);

                //sql.Append("SELECT history.TagID, zones.Location, history.Event, history.Timestamp FROM history ");
                //sql.Append(" LEFT JOIN zones ON history.ZoneID = zones.ID");

                using (var con = new OdbcConnection(ConnString))
                using (var cmd = new OdbcCommand(mySelectQuery, con))
                {
                    OdbcDataReader myReader;

                    try
                    {
                        con.Open();
                        myReader = cmd.ExecuteReader();
                    }
                    catch
                    {
                        return;
                    }

                    using (myReader)
                    {
                        /*try
                        {
                            myReader = myCommand.ExecuteReader();
                            MainForm.reconnectCounter = -1;
                            timer3.Enabled = false;
                        }
                        catch (Exception ex)
                        {
                            int ret = 0, ret1 = 0;
                            if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) || 
                                ((ret1=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                            {   
                                //error code 2013

                                MsgDBLabel.ForeColor = System.Drawing.Color.Red;
                                MsgDBLabel.Text = "Disconnected";
						
                                if (MainForm.reconnectCounter < 0)
                                {
                                    MainForm.reconnectCounter = 0;
                                    timer3.Enabled = true;
                                }                         
                            }
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                    myReader.Close();
                            }
                            return;
                        } //catch ..try
                        */

                        uint itemIndex = 1;
                        string netStat = "";
                        string rdrStat = "";

                        IPListView.Items.Clear();
                        while (myReader.Read())
                        {
                            ListViewItem listItem = new ListViewItem(itemIndex.ToString());  //index
                            listItem.SubItems.Add(myReader.GetString(0));  //ip

                            try
                            {
                                if (myReader.IsDBNull(1))
                                    listItem.SubItems.Add("");  //rdr ID
                                else
                                    listItem.SubItems.Add(myReader["ReaderID"].ToString());  //rdr ID
                            }
                            catch
                            {
                                listItem.SubItems.Add("");  //rdr ID
                            }

                            try
                            {
                                if (myReader.IsDBNull(2))
                                    listItem.SubItems.Add("");  //hostID
                                else
                                    listItem.SubItems.Add(myReader["HostID"].ToString());  //hostID
                            }
                            catch
                            {
                                listItem.SubItems.Add("");  //hostID
                            }

                            try
                            {
                                netStat = myReader.GetString(3);
                                listItem.SubItems.Add(netStat);  //networkStatus
                            }
                            catch
                            {
                                netStat = "";
                                listItem.SubItems.Add(netStat);  //networkStatus
                            }

                            try
                            {
                                if (myReader.IsDBNull(4))
                                    rdrStat = "";
                                else
                                    rdrStat = myReader.GetString(4);

                                listItem.SubItems.Add(rdrStat);  //rdrStatus
                            }
                            catch
                            {
                                rdrStat = "";
                                listItem.SubItems.Add(rdrStat);  //rdrStatus
                            }

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
                                IPListView.Items[IPListView.Items.Count - 1].ForeColor = System.Drawing.Color.Red;
                            else if ((netStat == "Active") && (rdrStat == "Offline"))
                                IPListView.Items[IPListView.Items.Count - 1].ForeColor = System.Drawing.Color.Blue;
                            else if (rdrStat == "Online")
                                IPListView.Items[IPListView.Items.Count - 1].ForeColor = System.Drawing.Color.Green;

                        }
                    }
                }//lock m_connection
            }
        }

		void UpdateCommunicationStat (string s, bool conn)
		{
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateCommunicationStat(s, conn)));
                return;
            }

            //This function updates communication status
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

		/*private void UpdateIPList(string ip, int rdr, int host, string stat, string rdrStat, eventType e)
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
						
						listItem.SubItems.Add(ip);  //ip
						listItem.SubItems.Add(Convert.ToString(rdr));  //rdrID
						listItem.SubItems.Add(Convert.ToString(host));  //hostID
						
						listItem.SubItems.Add(stat);  
						listItem.SubItems.Add(rdrStat);  
						listItem.SubItems.Add(DateTime.Now.ToString("MM-dd-yyyy  HH:mm:ss"));  //connectTime
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
						if (e == eventType.sockConnect)
						{
							//if (stat == "Active")
								//listItem.ForeColor = System.Drawing.Color.Blue;
							//else
								//listItem.ForeColor = System.Drawing.Color.Black;
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
							//if (rdrStat == "Online")
								//listItem.ForeColor = System.Drawing.Color.Green;
							//else
								//listItem.ForeColor = System.Drawing.Color.Blue;
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

			//}//updateListLock
		//}

        private void UpdateIPList(string ip, int rdr, int host, string stat, string rdrStat, eventType e)
		{
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateIPList(ip, rdr, host, stat, rdrStat, e)));
                return;
            }

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
                        if ((index >= IPListView.Items.Count) || (index < 0))
							return;

						listItem.SubItems.Add(ip);  //ip
						listItem.SubItems.Add(Convert.ToString(rdr));  //rdrID
						listItem.SubItems.Add(Convert.ToString(host));  //hostID
						
						listItem.SubItems.Add(stat);  
						listItem.SubItems.Add(rdrStat);  
						listItem.SubItems.Add(DateTime.Now.ToString("MM-dd-yyyy  HH:mm:ss"));  //connectTime
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
					
			}//updateListLock
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

		//private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		//{


		//}
			
		private bool FindItem(string item, out int index)
		{
            //This function Finds Item
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

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.RS232TabPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AnyRdrCheckBox = new System.Windows.Forms.CheckBox();
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
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.RS232TabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AnyRdrCheckBox);
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
            // AnyRdrCheckBox
            // 
            this.AnyRdrCheckBox.ForeColor = System.Drawing.Color.Blue;
            this.AnyRdrCheckBox.Location = new System.Drawing.Point(104, 264);
            this.AnyRdrCheckBox.Name = "AnyRdrCheckBox";
            this.AnyRdrCheckBox.Size = new System.Drawing.Size(104, 24);
            this.AnyRdrCheckBox.TabIndex = 15;
            this.AnyRdrCheckBox.Text = "Any Reader";
            this.AnyRdrCheckBox.Visible = false;
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
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
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
            this.label1.Text = "Bits Per Second :";
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
            this.NetworkTabPage.UseVisualStyleBackColor = true;
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
            this.SelectedIPRadioButton.Enabled = false;
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
            // 
            // SpecificIPRadioButton
            // 
            this.SpecificIPRadioButton.Location = new System.Drawing.Point(314, 26);
            this.SpecificIPRadioButton.Name = "SpecificIPRadioButton";
            this.SpecificIPRadioButton.Size = new System.Drawing.Size(90, 24);
            this.SpecificIPRadioButton.TabIndex = 12;
            this.SpecificIPRadioButton.Text = "Specific IP: ";
            this.SpecificIPRadioButton.Click += new System.EventHandler(this.SpecificIPRadioButton_Click);
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
            this.IPListView.SelectedIndexChanged += new System.EventHandler(this.IPListView_SelectedIndexChanged);
            this.IPListView.DoubleClick += new System.EventHandler(this.IPListView_DoubleClick);
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
            this.columnHeader6.Text = "Last Update Time";
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
            this.MsgLabel.Size = new System.Drawing.Size(332, 23);
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
            this.timer1.Interval = 4000D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000D;
            this.timer2.SynchronizingObject = this;
            this.timer2.Elapsed += new System.Timers.ElapsedEventHandler(this.timer2_Elapsed);
            // 
            // timer3
            // 
            this.timer3.Interval = 1000D;
            this.timer3.SynchronizingObject = this;
            this.timer3.Elapsed += new System.Timers.ElapsedEventHandler(this.timer3_Elapsed);
            // 
            // CommForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(610, 515);
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

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		//[STAThread]
		//static void Main() 
		//{
		//Application.Run(new MainForm());
		//}

		private void ConnectButton_Click(object sender, System.EventArgs e)
		{
			//MainForm.PlaySound(1);

			port = Convert.ToUInt32(ComComboBox.Text);
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

		public void ScanButton_Click(object sender, System.EventArgs e)
		{
			//if (!AllIPRadioButton.Checked)
			//{
			//MessageBox.Show(this, "The Command Type is not supported with this Scan command. Please use 'All IPs'.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			//return;
			//}

			if (AllIPRadioButton.Checked)
			{
				if (ScanNet != null)
				{
					//StringBuilder sql = new StringBuilder();
					//sql.Append("DELETE * FROM netip");         
					/*OdbcCommand myCommand = new OdbcCommand("TRUNCATE TABLE netip", m_connection);
				
					try
					{
						myCommand.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						ShowErrorMessage(ex.Message);
						return;
					}*/
					if (!mForm.RunNonQueryCmd("TRUNCATE TABLE netip"))
					{
						ShowErrorMessage("Failed to clear table.");
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
						Thread.Sleep(1500);
					}

					IPListView.Items.Clear();
					listIndex = 1;
					ScanNet(null);
					this.Cursor = Cursors.Default;
					//ScanButton.Enabled = false;
				}
			}//AllIP
			else if (SelectedIPRadioButton.Checked) //selected ip
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

						if (MainForm.readerPowerup[i].ip == ipstr)
						{
							MainForm.readerPowerup[i].online = false;
							MainForm.readerPowerup[i].counter = 0;
							break;
						}
					
						for (int j=0; j<ipstr.Length; j++)
							ip[j] = Convert.ToByte(ipstr[j]);

						if (DisconnectSock != null)
						{
							DisconnectSock(ip);
							Thread.Sleep(200);
						}

						if (ScanNet != null)
						{
							ScanNet(ip);
							Thread.Sleep(200);
						}
					}
				}
				else
				{
					ShowErrorMessage("No item is selected");
				}
			}
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
						Thread.Sleep(200);
					}

					if (ScanNet != null)
					{
						ScanNet(ip);
						Thread.Sleep(200);
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
					
						for (int ix=0; ix<ipstr.Length; ix++)
							ip[ix] = Convert.ToByte(ipstr[ix]);
					
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

					for (int ix=0; ix<ipstr.Length; ix++)
						ip[ix] = Convert.ToByte(ipstr[ix]);

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
				MainForm.PlaySound(1);

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
            //This function closes the dialog box
			//CloseConnection();
			//MsgLabel.Text = "Connection : Comm port Closed";
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Active Wave\\Smart Tracker\\");
			uint sysPort = Convert.ToUInt32(reg.GetValue("port"));
			if (port != sysPort)
				reg.SetValue("port", Convert.ToString(port));
		}        

		private void CloseConnectButton_Click(object sender, System.EventArgs e)
		{
			//MainForm.PlaySound(1);
            //This function closes connect Button
			if (mForm.CloseSerialPort() >= 0)
			{
				MainForm.rs232Comm = false;
				ConnectButton.Enabled = true;
				ResetReaderButton.Enabled = false;
				CloseConnectButton.Enabled = false;
				mForm.SetRs232Reader(0);


				StringBuilder sql = new StringBuilder();
				sql.Append("UPDATE zones SET Status = ");
				sql.AppendFormat("('{0}')", "Offline");
				sql.Append(", Time = ");
				sql.AppendFormat("'{0}'", DateTime.Now);
				   
				//<<<OdbcCommand myCommand = new OdbcCommand(sql.ToString(), MainForm.m_connection);
				
				//lock (m_connection)
				{
					if (!mForm.RunNonQueryCmd(sql.ToString()))
						return;
					/*try
					{
						myCommand.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						//ShowErrorMessage(ex.Message);
						return;
					}*/
					//}

				}// lock m_connection
			}
		}

		private void ConnectNetButton_Click(object sender, System.EventArgs e)
		{
            //This function connects the network
			if (!AllIPRadioButton.Checked)
			{
                MessageBox.Show(this, "The Command Type is not supported with this Connect command. Please use 'All IPs'.", "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}

			//MainForm.PlaySound(1);
			if (ConnectSock != null)
				ConnectSock();

			/* The following code was commented out. need API suport
			MainForm.PlaySound(1);
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
					
						for (int ix=0; ix<ipstr.Length; ix++)
							ip[ix] = Convert.ToByte(ipstr[ix]);
					
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

					for (int ix=0; ix<ipstr.Length; ix++)
						ip[ix] = Convert.ToByte(ipstr[ix]);

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

        #region CloseWindow()
        private void CloseWindow()
        {
            Close();
        }
        #endregion

        private void ShowErrorMessage(string msg)
		{
            MessageBox.Show(this, msg, "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

        private void ResetRdrButton_Click(object sender, System.EventArgs e)
        {
            //This function resets the reader
            //MainForm.PlaySound(1);
            //listIndex = 1;

            if (SelectedIPRadioButton.Checked)  //selected
            {
                if ((IPListView.Items.Count > 0) && (IPListView.SelectedItems.Count >= 1))
                {
                    ListViewItem item;
                    byte[] ip = new byte[16];
                    char[] buf = new char[16];
                    string ipstr;
                    //int ix = 0;

                    for (int i = 0; i < IPListView.SelectedItems.Count; i++)
                    {
                        item = IPListView.SelectedItems[i];
                        ipstr = item.SubItems[1].Text;
                        if (ipstr.Length > 0)
                            selectedIPStr = ipstr;
                        else
                            selectedIPStr = "";

                        for (int j = 0; j < ipstr.Length; j++)
                            ip[j] = Convert.ToByte(ipstr[j]);

                        for (int k = 0; k < 100; k++)
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
                            timer2.Enabled = true;
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
                    string ipstr1 = IPTextBox.Text;
                    selectedIPStr = ipstr1;
                    byte[] ip = new byte[16];

                    for (int j = 0; j < ipstr1.Length; j++)
                        ip[j] = Convert.ToByte(ipstr1[j]);

                    ListViewItem item;
                    for (int i = 0; i < IPListView.Items.Count; i++)
                    {
                        item = IPListView.Items[i]; //IPListView.SelectedItems[i];
                        string ipstr = item.SubItems[1].Text;
                        uint rdr = 0;
                        if (item.SubItems[2].Text.Length > 0)
                            rdr = Convert.ToUInt16(item.SubItems[2].Text);

                        if (ipstr == ipstr1)
                        {
                            item.SubItems[5].Text = "Offline";
                            IPListView.Items.RemoveAt(i);
                            item.ForeColor = System.Drawing.Color.Blue;
                            IPListView.Items.Insert(i, item);

                            StringBuilder sql = new StringBuilder();
                            sql.Append("UPDATE zones SET Status = ");
                            sql.AppendFormat("('{0}')", "Offline");
                            sql.Append(", Time = ");
                            sql.AppendFormat("'{0}'", DateTime.Now);
                            sql.Append(" WHERE ReaderID = ");
                            sql.AppendFormat("('{0}')", rdr);

                            //<<<OdbcCommand myCommand = new OdbcCommand(sql.ToString(), MainForm.m_connection);

                            if (!mForm.RunNonQueryCmd(sql.ToString()))
                                return;
                            /*try
                            {
                                myCommand.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                //ShowErrorMessage(ex.Message);
                                return;
                            }*/

                            break;
                        }
                    }

                    for (int i = 0; i < 100; i++)
                    {
                        if (MainForm.readerPowerup[i].ip == ipstr1)
                        {
                            MainForm.readerPowerup[i].online = false;
                            MainForm.readerPowerup[i].counter = 0;
                            break;
                        }
                    }

                    if (ResetSocketReaderEvent != null)
                    {
                        ResetSocketReaderEvent(ip);

                        timer2.Interval = 3000;
                        timer2.Enabled = true;
                    }

                }
                else
                {
                    ShowErrorMessage("Need ip address");
                }
            }
            else //all ips
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("UPDATE zones SET Status = ");
                sql.AppendFormat("('{0}')", "Offline");
                sql.Append(", Time = ");
                sql.AppendFormat("'{0}'", DateTime.Now);

                //>>>>>OdbcCommand myCommand = new OdbcCommand(sql.ToString(), MainForm.m_connection);
                if (!mForm.RunNonQueryCmd(sql.ToString()))
                    return;
                /*try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //ShowErrorMessage(ex.Message);
                    return;
                }*/

                for (int i = 0; i < 100; i++)
                {
                    MainForm.readerPowerup[i].online = false;
                    MainForm.readerPowerup[i].counter = 0;
                }

                for (int i = 0; i < IPListView.Items.Count; i++)
                {
                    ListViewItem item;
                    byte[] ip = new byte[16];
                    char[] buf = new char[16];
                    string ipstr;

                    item = IPListView.Items[i]; //IPListView.SelectedItems[i];
                    ipstr = item.SubItems[1].Text;


                    item.SubItems[5].Text = "Offline";
                    IPListView.Items.RemoveAt(i);
                    item.ForeColor = System.Drawing.Color.Blue;
                    IPListView.Items.Insert(i, item);

                    for (int j = 0; j < ipstr.Length; j++)
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
                timer2.Enabled = true;

            }//allips
        }

		private void ResetReaderButton_Click(object sender, System.EventArgs e)
		{
            //This function resets the reader
			//MainForm.PlaySound(1);
            ushort rdr = 0;
			if (AnyRdrCheckBox.Checked)
			{
				if (m_resetAllReaders != null)
				{
					m_resetAllReaders();
				}
			}
			else
			{
                rdr = mForm.GetRs232Reader();
				if (rdr == 0)
				{
					if (m_resetAllReaders != null)
					{
						m_resetAllReaders();
					}
				}
				else
				{
					if (ResetReaderEvent != null)
					{
						ResetReaderEvent(rdr, 1);
					}
				}
			}
		}

		private void DisconnectNetButton_Click(object sender, System.EventArgs e)
		{
            //This function Disconnects from the network
			string ipstr;
			byte[] ip = new byte[16];
			char[] buf = new char[16];
			ListViewItem item = null;

			if (AllIPRadioButton.Checked)
			{
				if (DisconnectSock != null)
				{
					if (MessageBox.Show(this, "Disconnect All?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
						return;
					byte[] ipp = null;
					DisconnectSock(ipp);
					ScanButton.Enabled = true;
				}
			}
			else if (SelectedIPRadioButton.Checked)
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
			else
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
			MainForm.PlaySound(1);
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
					
						for (int ix=0; ix<ipstr.Length; ix++)
							ip[ix] = Convert.ToByte(ipstr[ix]);

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

					for (int ix=0; ix<ipstr.Length; ix++)
						ip[ix] = Convert.ToByte(ipstr[ix]);

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
			MainForm.PlaySound(1);
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
			if ((item.SubItems[5].Text.ToString() == "Offline") && SpecificIPRadioButton.Checked)
				ScanButton.Enabled = true;
		}

		private void AllIPRadioButton_Click(object sender, System.EventArgs e)
		{
			//IPTextBox.Text = "";
			IPTextBox.ReadOnly = true;
		}

		private void SpecificIPRadioButton_Click(object sender, System.EventArgs e)
		{
			IPTextBox.ReadOnly = false;
		}

		private void IPListView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

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
				
				for (int ix=0; ix<ipstr.Length; ix++)
					ip[ix] = Convert.ToByte(ipstr[ix]);

				if (ResetSocketReaderEvent != null)
				{
					ResetSocketReaderEvent(ip);
					numips += 1;
					Console.WriteLine("IP SENT : " + ipstr);
					//timer1.Enabled = false; 
				}
	
			}*/
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
			byte[] ip = new byte[16];
			bool stopTimer = true;
			int startloop = 0;

			//lock (MainForm.powerupLocker)
			//lock (m_connection)  //dec-06-06
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
					string ipStr = selectedIPStr;

					try
					{
						if (ipStr.Length <= 0)
						{
							timer2.Enabled = false;
							MainForm.timerStop = false;  //restart enable reader timer
							return; 
						}
					}
					catch
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
				for (int ix=0; ix<ipstr.Length; ix++)
					ip[ix] = Convert.ToByte(ipstr[ix]);

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
			//@@ @@ Main window will handle reconnect
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
				lock (m_connection)
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
		}//timer3

	}//comform

	#region ReaderPowerupStruct
	[StructLayout(LayoutKind.Sequential)]
	public struct ReaderPowerupStruct
	{
		public string ip;
		public bool online;
		public ushort counter;
	}
	#endregion
}
