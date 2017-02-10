using System;
using System.Timers;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using AW_API_NET;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using AWIComponentLib.Communication;
using System.Windows.Forms;
//using AWIComponentLib.NetRdrReconn;


namespace AWIComponentLib.NetRdrConn
{
	/// <summary>
	/// Summary description for NetRdrConnClass.
	/// </summary>
	
	#region delegates
	//they will be used by the app
	public delegate void ReaderOfflineEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void ReaderOnlineEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void AllReadersOnlineEvent(bool b);

	//will be used for NetRdrReconnClass
	public delegate void StartReconnProcess(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void DisplayOfflineRdrEvent(string rdr, string type, string location, string time);
	
	public delegate void UpdateRdrOfflineListEvent(ushort rdr);
	#endregion

    public class NetRdrConnClass
    {
		#region events
		//they will be used by the app
		public static event ReaderOfflineEvent ReaderOfflineEventHandler;
		public static event ReaderOnlineEvent ReaderOnlineEventHandler;
		public static event AllReadersOnlineEvent AllReadersOnlineEventHandler;

		//will be used for NetRdrReconnClass
        public static event StartReconnProcess StartReconnProcessHandler;

		//will be used by OfflineRdrForm
		public static event UpdateRdrOfflineListEvent UpdateRdrOfflineListEventHandler;

		#endregion

		#region vars
		private static ushort MAX_READER_NO_RESPONSE = 0x03;
		private System.Timers.Timer pollReaderTimer;
		private ushort rdrIndexPoll;
		private bool trunOnRdrPolling;
		public static Object myLock = new Object();
		private ArrayList rdrStatusList = new ArrayList();
		private CommunicationClass communication;
		//private NetRdrReconnClass netRdrReconn;
		#endregion

		#region Constructor
		public NetRdrConnClass(CommunicationClass comm)
		{
			rdrIndexPoll = 0;  //index of pollReaderStruct
			trunOnRdrPolling = false; //stop timer for OnPollReaderTimerEvent until it is set
			communication = comm;
			//netRdrReconn = new NetRdrReconnClass(comm);
			CommunicationClass.PowerupEventHandler += new PowerupEvent(this.PowerupReaderNotifty);
			CommunicationClass.RdrErrorEventHandler += new RdrErrorEvent(this.ErrorEventNotify);
			CommunicationClass.EnableReaderEventHandler += new EnableReaderEvent(this.EnableReaderEventNotify);
			CommunicationClass.CloseSocketEventHandler += new CloseSocketEvent(this.CloseSocketEventNotify);
			
			CommunicationClass.TagDetectedEventHandler += new TagDetectedEvent(this.ReaderIsOnlineEventNotify);
			CommunicationClass.TagDetectedRSSIEventHandler += new TagDetectedRSSIEvent(this.ReaderIsOnlineEventNotify);
            CommunicationClass.TagDetectedTamperEventHandler += new TagDetectedTamperEvent(this.ReaderIsOnlineEventNotify);
			CommunicationClass.TagDetectedRSSITamperEventHandler += new TagDetectedRSSITamperEvent(this.ReaderIsOnlineEventNotify);

		}
		#endregion

		#region StartRdrPolling
		public void StartRdrPolling()
		{
			//lock(myLock)
			{
				//commObj = new CommunicationClass(0);

				//timer for polling the readers for connectivity
				//if (b)
				//{
					pollReaderTimer = new System.Timers.Timer();
					pollReaderTimer.Interval = 5000;
					pollReaderTimer.AutoReset = true;
					pollReaderTimer.Enabled = true;
					pollReaderTimer.Elapsed += new ElapsedEventHandler(OnPollEnableReaderTimerEvent);
                    
					trunOnRdrPolling = true;
				//}
				//else
				//{
					//if (trunOnRdrPolling)
					//{
						//pollReaderTimer.Enabled = false;
						//trunOnRdrPolling = false;
					//}
				//}
				//rdrPollTimerStart = b;  //starts/stops the enableReader call in OnPollReaderTimerEvent
			}
		}
		#endregion

		#region StopRdrPolling
		public void StopRdrPolling()
		{
			pollReaderTimer.Enabled = false;
            pollReaderTimer.Close();
            pollReaderTimer.Dispose();
			trunOnRdrPolling = false;
		}
		#endregion

		#region TrunOffRdrPolling
		public void TrunOffRdrPolling()
		{
            if (trunOnRdrPolling)
            {
                pollReaderTimer.Enabled = false;
                pollReaderTimer.Close();
                pollReaderTimer.Dispose();
            }

		    trunOnRdrPolling = false;
            
		}
		#endregion

		#region TrunOnRdrPolling
		public void TrunOnRdrPolling()
		{
			trunOnRdrPolling = true;
		}
		#endregion

		#region OnPollEnableReaderTimerEvent
		private void OnPollEnableReaderTimerEvent(object source, ElapsedEventArgs e)
		{
			//lock(myLock)
			{
				//need to have instances of communicationClass in order to call EnableReader 
				//otherwise need to have the function as static to work
				//CommunicationClass commObj = new CommunicationClass(0);

				//if there is no reader in the list do not process
				//the list gets populated when a net rdr gets powered up

				if (rdrStatusList.Count == 0)
					return;

                if (!trunOnRdrPolling)            
                    pollReaderTimer.Enabled = false;
				else if (trunOnRdrPolling)
				{
					if (rdrIndexPoll >= rdrStatusList.Count)
						rdrIndexPoll = 0;
					
					readerStatStruct rdrStat = new readerStatStruct(0); //create an readerStatStruct object with temp rdr id
					rdrStat = (readerStatStruct)rdrStatusList[rdrIndexPoll];

					Byte[] ip = new Byte[20];
					char[] cIP = new char[20];

					string ipStr = rdrStat.GetIP();
					if (ipStr.Length == 0)
					{
						Console.WriteLine("NetRdrConn  ERROR bad ip = ");
						rdrIndexPoll += 1;
						return;
					}
					cIP = ipStr.ToCharArray(0, ipStr.Length);
					for (int i=0; i<ipStr.Length; i++)
						ip[i] = Convert.ToByte(cIP[i]);


					AW_API_NET.rfReaderEvent_t readerEvent = new rfReaderEvent_t();
					readerEvent.ip = new Byte[20];
					for (int i=0; i<20; i++) readerEvent.ip[i] = 0x00; 
					readerEvent.reader = rdrStat.rdrID;
					readerEvent.host = rdrStat.hostID;
					readerEvent.fGenerator = 0;
					readerEvent.eventType = AW_API_NET.APIConsts.RF_READER_ENABLE;
					int n =0;
					for(n=0; n<readerEvent.ip.Length; n++)
						readerEvent.ip[n] = ip[n];
					
					if ((rdrStat.rdrID > 0) && rdrStat.GetPollReader())
					{
						//if return value is -185 (rdr not found) should remove rdr from rdrStatusList
						int ret = communication.EnableReader(rdrStat.rdrID, rdrStat.hostID);
						if (ret < 0) //some error took place will not go to the error notification process it here
						{
							Console.WriteLine("NetRdrConn - OnPollEnableReader - ERROR in Enable Reader Call err = " + Convert.ToString(ret) + "  rdr = " + Convert.ToString(rdrStat.rdrID));
                            
						
							// 1 - send offline event to the app
							// 2 - closesocket(ip)
							// 3 - send scanNetwork(ip)

							//if (ReaderOfflineEventHandler != null)
								//ReaderOfflineEventHandler(readerEvent);

							//check to see if the counter has reached its MAX
							if (ProcessRdrError (readerEvent, AW_API_NET.APIConsts.RF_E_NO_RESPONSE))
							{
								Console.WriteLine("NetRdrConn - OnPollEnableReader - ProcessRdrError()");
								readerStatStruct readerObj = new readerStatStruct(readerEvent.reader);
								if (GetRdrFromList(ipStr, ref readerObj))
								{
									DateTime timeNow; 
									int tSecNow;
									int sTime;
									if (readerObj.GetProcessing() || !readerObj.online)
									{
										timeNow = DateTime.Now;
										tSecNow = timeNow.Hour*3600 + timeNow.Minute*60 + timeNow.Second;
                                        Console.WriteLine("NetRdrConn  OnErrorNotify startTime=" + DateTime.Now.ToString());
										sTime = readerObj.GetStartTimeSec()+ 3;
										
										/*if (sTime < tSecNow)
										{
											rdrIndexPoll += 1;
											Console.WriteLine("NetRdrConn  OnPollEnable Time NOT EXPIRED. startTime=" + Convert.ToString(sTime) + " TimeNow=" + Convert.ToString(tSecNow));	 
											return;
										}
										else  //time not expired*/
										{
											//Console.WriteLine("NetRdrConn  OnPollEnable Time EXPIRED. Send close and scan startTime=" + Convert.ToString(sTime) + " TimeNow=" + Convert.ToString(tSecNow));
											 readerStatStruct newRdrObj = new readerStatStruct(readerEvent.reader);
											newRdrObj.SetIP(ipStr);
											newRdrObj.hostID = readerObj.hostID;
											newRdrObj.online = false;
											newRdrObj.SetStatus(readerObj.GetStatus());
											newRdrObj.SetCounter(0);
											newRdrObj.SetProcessing(true);
											newRdrObj.SetStartTimeSec(DateTime.Now); //new time
											rdrStatusList.Remove(readerObj);
											rdrStatusList.Add(newRdrObj);
										}
									}//reader offline
									/*else  //this the first time reader is offline
									{
										Console.WriteLine("NetRdrConn  OnPollEnableReader - First Time reader #" + Convert.ToString(readerEvent.reader) + " Offline. set rdrObj");
										readerStatStruct newRdrObj = new readerStatStruct(readerEvent.reader);
										newRdrObj.SetIP(ipStr);
										newRdrObj.hostID = readerObj.hostID;
										newRdrObj.online = false;
										newRdrObj.SetStatus(readerObj.GetStatus());
										newRdrObj.SetCounter(readerObj.GetCounter());
										newRdrObj.SetProcessing(true);
										newRdrObj.SetStartTimeSec(DateTime.Now);
										rdrStatusList.Remove(readerObj);
										rdrStatusList.Add(newRdrObj);
										
									}*/
								}
								else
								{
									Console.WriteLine("NetRdrConn  Could not find reader in rdrStatusList");
								}//GetRdrFromList

								Console.WriteLine("NetRdrConn  OnPollRdr Send OFFLINE Event for rdr=" + Convert.ToString(readerEvent.reader));
								if (ReaderOfflineEventHandler != null)
									ReaderOfflineEventHandler(readerEvent);

								if (UpdateRdrOfflineListEventHandler != null)
                                   UpdateRdrOfflineListEventHandler(readerEvent.reader);
									                                

								//Console.WriteLine("NetRdrConn   Counter = MAX sending closesock and scan for (ret < 0)");
								Console.WriteLine("NetRdrConn  SocketDisconnection() called for ip = " + ipStr);
								ret = communication.SocketDisconnection(ip);
							
								Thread.Sleep(300);
							
								Console.WriteLine("NetRdrConn  ScanNetwork() called for ip = " + ipStr);
								ret = communication.ScanNetwork(ip);
								
							}//ProcessRdrError
							//else
							//{
                                //if (ReaderOfflineEventHandler != null)
								   //ReaderOfflineEventHandler(readerEvent);
							//}
						} // ret < 0

						rdrIndexPoll += 1;

					} //rdrStat.GetPollReader())
					else
                        rdrIndexPoll += 1;	
				}
				
			}//lock
		}
		#endregion

		#region CloseSocketEventNotify
		void CloseSocketEventNotify (AW_API_NET.rfReaderEvent_t readerEvent)
		{
			/*string ip;
			if ((ip=GetStringIP(readerEvent.ip)) != "")
			{
				//if ip exits in the rdrStatusList remove it and replace it otherwise add it.
				readerStatStruct readerObj = new readerStatStruct(readerEvent.reader);
				if (GetRdrFromList (ip, ref readerObj))
					rdrStatusList.Remove(readerObj);
			}*/
		}
		#endregion

		#region PowerupReaderNotifty
		void PowerupReaderNotifty (AW_API_NET.rfReaderEvent_t readerEvent)
		{
			//populating rdrStat for polling rdr module
			//check if reader is on network
			//lock(myLock)
			{
				string ip;
				if ((ip=GetStringIP(readerEvent.ip)) != "")
				{
					//readerStatStruct readerObj = new readerStatStruct(readerEvent.reader);
					//if (GetRdrFromList (ip, ref readerObj))
						//return;
						
					//if ip exits in the rdrStatusList remove it and replace it otherwise add it.
					readerStatStruct readerObj = new readerStatStruct(readerEvent.reader);
					if (GetRdrFromList (ip, ref readerObj))
						rdrStatusList.Remove(readerObj);
				
					readerStatStruct rdrStat = new readerStatStruct(readerEvent.reader);
					rdrStat.hostID = readerEvent.host;
					rdrStat.SetIP(GetStringIP(readerEvent.ip));
					rdrStat.SetStatus(1); //online
					rdrStat.online = true;
					rdrStat.SetProcessing(false);
					
					rdrStatusList.Add(rdrStat);  //polling rdr list
				}
			}
							
		}
		#endregion

		#region ErrorEventNotify
		void ErrorEventNotify (AW_API_NET.rfReaderEvent_t readerEvent)
		{
			//lock(myLock)
			{
				if (readerEvent.eventType == AW_API_NET.APIConsts.RF_READER_ENABLE)
				{
					//if NOT_RESPONDING .....
					//if RESDER BUSY .....
					if (readerEvent.errorStatus == AW_API_NET.APIConsts.RF_E_NO_RESPONSE)
					{
						//find the status of that rdr from reader array
						//readerStatStruct readerObj = new readerStatStruct(readerEvent.reader);
						if (ProcessRdrError (readerEvent, AW_API_NET.APIConsts.RF_E_NO_RESPONSE))
						{
							//int ret = communication.EnableReader(rdrStat.rdrID, rdrStat.hostID);
							//if (ret < 0) //some error took place will not go to the error notification process it here
							//{
	
							Console.WriteLine("NetRdrConn   OnErrorNotify Counter = MAX for NO_RESPONSE Notification. Rdr=" + Convert.ToString(readerEvent.reader));

							// 1 - send offline event to the app
							// 2 - closesocket(ip)
							// 3 - send scanNetwork(ip)

							//if (ReaderOfflineEventHandler != null)
							//ReaderOfflineEventHandler(readerEvent);

							string ip;
							if ((ip=GetStringIP(readerEvent.ip)) == "")
							{
								Console.WriteLine("NetRdrConn  Invalid ip address");
								return;
							}

							readerStatStruct readerObj = new readerStatStruct(readerEvent.reader);
							if (GetRdrFromList(ip, ref readerObj))
							{
								DateTime timeNow; 
								int tSecNow;
								int sTime;
								if (readerObj.GetProcessing() || !readerObj.online)
								{
									timeNow = DateTime.Now;
									tSecNow = timeNow.Hour*3600 + timeNow.Minute*60 + timeNow.Second;
									Console.WriteLine("NetRdrConn  OnErrorNotify startTime= " + DateTime.Now.ToString());
									sTime = readerObj.GetStartTimeSec()+ 3;
									/*if (sTime < tSecNow)
									{
										Console.WriteLine("NetRdrConn  OnErrorNotify Time NOT EXPIRED. startTime=" + Convert.ToString(sTime) + " TimeNow=" + Convert.ToString(tSecNow));	 
										return;
									}
									else*/
									{
										//Console.WriteLine("NetRdrConn  OnErrorNotify Time EXPIRED. startTime=" + Convert.ToString(sTime) + " TimeNow=" + Convert.ToString(tSecNow));
										
										readerStatStruct newRdrObj = new readerStatStruct(readerEvent.reader);
										newRdrObj.SetIP(ip);
										newRdrObj.hostID = readerObj.hostID;
										newRdrObj.online = false;
										newRdrObj.SetStatus(0);
										newRdrObj.SetCounter(0);
										newRdrObj.SetProcessing(true);
										newRdrObj.SetStartTimeSec(DateTime.Now);
										rdrStatusList.Remove(readerObj);
										rdrStatusList.Add(newRdrObj);
									}
								} //if (readerObj.GetProcessing() || !readerObj.online)
								/*else
								{
									Console.WriteLine("NetRdrConn  OnErrorNotify - First Time reader #" + Convert.ToString(readerEvent.reader) + "offline");
									readerStatStruct newRdrObj = new readerStatStruct(readerEvent.reader);
									newRdrObj.SetIP(ip);
									newRdrObj.hostID = readerEvent.host;
									newRdrObj.online = false;
									newRdrObj.SetStatus(0);
									newRdrObj.SetCounter(0);
									newRdrObj.SetProcessing(true);
									newRdrObj.SetStartTimeSec(DateTime.Now);
									rdrStatusList.Remove(readerObj);
									rdrStatusList.Add(newRdrObj);
								}*/
							}//if (GetRdrFromList(ip
							else
							{
								Console.WriteLine("NetRdrConn  Could not find reader in rdrStatusList");
							}

							Console.WriteLine("NetRdrConn  OnPollRdr Send OFFLINE Event for rdr=" + Convert.ToString(readerEvent.reader));
							if (ReaderOfflineEventHandler != null)
								ReaderOfflineEventHandler(readerEvent);

						    Console.WriteLine("NetRdrConn  SocketDisconnection() called for ip = " + ip);
							int ret = communication.SocketDisconnection(readerEvent.ip);
							
							Thread.Sleep(300);
							
							Console.WriteLine("NetRdrConn  ScanNetwork() called for ip = " + ip);
							ret = communication.ScanNetwork(readerEvent.ip);

						
						}//ProcessRdrError
						//else
						//{
							//if (ReaderOfflineEventHandler != null)
								//ReaderOfflineEventHandler(readerEvent);
						//}
					}//NO_RESPONSE
				}//ENABLE_READER
			}

			//need to set an event in NetRdrReconnClass to process reconn to socket
			//check to see if this event already setup for the module
			//call the event with ip included
		}
		#endregion

		#region ReaderIsOnlineEventNotify
		void ReaderIsOnlineEventNotify (AW_API_NET.rfTagEvent_t tagEvent)
		{
			//lock(myLock)
			{
				//if polling rdr is switched on
				readerStatStruct readerObj = new readerStatStruct(tagEvent.reader);
				if (GetRdrFromList (tagEvent.reader, ref readerObj))
				{
					if (readerObj.GetStatus() == 0)
					{

						Byte[] ip = new Byte[20];
						char[] cIP = new char[20];

						string ipStr = readerObj.GetIP();
						if (ipStr.Length == 0)
						{
							Console.WriteLine("NetRdrConn  ERROR bad ip = ");
							rdrIndexPoll += 1;
							return;
						}
						cIP = ipStr.ToCharArray(0, ipStr.Length);
						for (int i=0; i<ipStr.Length; i++)
							ip[i] = Convert.ToByte(cIP[i]);

						AW_API_NET.rfReaderEvent_t readerEvent = new rfReaderEvent_t();
						readerEvent.ip = new Byte[20];
						for (int i=0; i<20; i++) readerEvent.ip[i] = 0x00; 
						readerEvent.reader = readerObj.rdrID;
						readerEvent.host = readerObj.hostID;
						readerEvent.fGenerator = 0;
						readerEvent.eventType = AW_API_NET.APIConsts.RF_READER_ENABLE;
						int n =0;
						for(n=0; n<readerEvent.ip.Length; n++)
							readerEvent.ip[n] = ip[n];

						if (ReaderOnlineEventHandler != null)
						    ReaderOnlineEventHandler(readerEvent);

						//if ip exits in the rdrStatusList remove it and replace it otherwise add it.
						readerStatStruct rdrStat = new readerStatStruct(tagEvent.reader);
						rdrStat.hostID = tagEvent.host;
						//need to fix api to send valid ip with enable rdr
						//rdrStat.SetIP(GetStringIP(readerEvent.ip));
						rdrStat.SetIP(readerObj.GetIP());
						rdrStat.SetStatus(1); //online
						rdrStat.SetCounter(0);
						rdrStat.online = true;
						rdrStat.SetProcessing(false);
						rdrStat.SetStartTimeSec(DateTime.Now);

						rdrStatusList.Remove(readerObj);

						rdrStatusList.Add(rdrStat);  //polling rdr list

                        if (GetAllRdrsStat())
                        {
                            if (AllReadersOnlineEventHandler != null)
                                AllReadersOnlineEventHandler(true);
                        }
                        else
                        {
                            if (AllReadersOnlineEventHandler != null)
                                AllReadersOnlineEventHandler(false);
                        }
					}
				}
					
			}//lock
		}
		#endregion

		#region EnableReaderEventNotify
		void EnableReaderEventNotify (AW_API_NET.rfReaderEvent_t readerEvent)
		{
            if (!trunOnRdrPolling)
                return;
			//lock(myLock)
			{
				if (ReaderOnlineEventHandler != null)
					ReaderOnlineEventHandler(readerEvent);

				//if polling rdr is switched on
				readerStatStruct readerObj = new readerStatStruct(readerEvent.reader);
				if (GetRdrFromList (readerEvent.reader, ref readerObj))
				{
					//if prev rdr stat was offline set the event for online
					//might need to send everytime to update the app about the status
					//if (readerObj.GetStatus() == 1)
				
					//if (ReaderOnlineEventHandler != null)
					//ReaderOnlineEventHandler(readerEvent);
				

					//set the params to online and reset the counter
					//readerObj.SetStatus(1);  //online
					//readerObj.SetCounter(0);
					//-----------------------------------------

					//if ip exits in the rdrStatusList remove it and replace it otherwise add it.
					readerStatStruct rdrStat = new readerStatStruct(readerEvent.reader);
					rdrStat.hostID = readerEvent.host;
					//need to fix api to send valid ip with enable rdr
					//rdrStat.SetIP(GetStringIP(readerEvent.ip));
					rdrStat.SetIP(readerObj.GetIP());
					rdrStat.SetStatus(1); //online
					rdrStat.SetCounter(0);
					rdrStat.online = true;
					rdrStat.SetProcessing(false);

					rdrStatusList.Remove(readerObj);

					rdrStatusList.Add(rdrStat);  //polling rdr list

					if (GetAllRdrsStat())
					{
						if (AllReadersOnlineEventHandler != null)
							AllReadersOnlineEventHandler (true);
						//else
							//AllReadersOnlineEventHandler (false);
					}
					

				}//is reader in the list
					
			}//lock
		}
		#endregion

		#region GetAllRdrsStat 
		bool GetAllRdrsStat()
		{
			foreach (readerStatStruct rdrObj in rdrStatusList)
			{
				//if atleast one reader offline return false
				if ((rdrObj.GetStatus() == 0) && (rdrObj.GetIP() != ""))
				return (false);
			}
			
			return (true); //all online
		}
		#endregion

		#region GetStringIP
		private string GetStringIP (byte[] ip)
		{
			int p = 0;
			string s = "";
			int ct = 0;
			while ((ct <= 3) && (p < 20) &&(ip[p] != 0))
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

			return s;
		}
		#endregion

		#region GetRdrFromList (ushort rdrID, ref readerStatStruct rdrStatObj)
		bool GetRdrFromList (ushort rdrID, ref readerStatStruct rdrStatObj)
		{
			foreach (readerStatStruct rdrObj in rdrStatusList)
			{
				if (rdrObj.rdrID == rdrID)
				{
					rdrStatObj = rdrObj;
					return (true);
				}
			}
			rdrStatObj = new readerStatStruct(0);
			return (false);
		}
		#endregion

		#region GetRdrFromList (string ip, ref readerStatStruct rdrStatObj)
		bool GetRdrFromList (string ip, ref readerStatStruct rdrStatObj)
		{
			foreach (readerStatStruct rdrObj in rdrStatusList)
			{
				if (rdrObj.GetIP() == ip)
				{
					rdrStatObj = rdrObj;
					return (true);
				}
			}
			rdrStatObj = new readerStatStruct(0);
			return (false);
		}
		#endregion

		#region GertRdrObjIndex
        int GertRdrObjIndex(string ip)
        {
			readerStatStruct rdrObj = new readerStatStruct(0);
            for (int i=0; i<rdrStatusList.Count; i++)
			{
				rdrObj = (readerStatStruct)rdrStatusList[i];

				if (rdrObj.GetIP() == ip)
					return(i);
			}

            return(-1);				
        }
		#endregion

		#region ProcessRdrError
		//returns true if the rdr object counter has maxed out
		bool ProcessRdrError (AW_API_NET.rfReaderEvent_t readerEvent, int errorID) //(ushort rdrID, int errorID)
		{
			//lock(myLock)
			{
				//bool ret = false;
				readerStatStruct rdrObj; // = new readerStatStruct(0); //create an readerStatStruct object with temp rdr id
				//rdrStat = (readerStatStruct)rdrStatusList[rdrIndexPoll];
				//foreach (readerStatStruct rdrObj in rdrStatusList)
				for (int i=0; i<rdrStatusList.Count; i++)
				{
					rdrObj = new readerStatStruct(0);
					rdrObj = (readerStatStruct)rdrStatusList[i];

					if (rdrObj.rdrID == readerEvent.reader)
					{
						readerStatStruct rdrObj2 = new readerStatStruct(readerEvent.reader);  //new
                        rdrObj.Copy(ref rdrObj2);  //new

						if ((errorID == AW_API_NET.APIConsts.RF_E_NO_RESPONSE) &&
							(readerEvent.eventType == AW_API_NET.APIConsts.RF_READER_ENABLE))
						{
							//if reached max
							if (rdrObj.GetCounter() >= MAX_READER_NO_RESPONSE)
							{
								//if reader prev stat was online set an event in the application
								//will set event once until the status goes back to online
								//if (rdrObj.GetStatus() == 1) 
								//ret = true;

								Console.WriteLine("NetRdrConn - ProcessRdrError - reader #" + Convert.ToString(readerEvent.reader) + " rdrObj counter Maxed");
								//rdrStatusList.RemoveAt(i);  //new removed
								rdrStatusList.Remove(rdrObj); //new
								
								rdrObj2.SetStatus(0);      //reader offline
								rdrObj2.SetCounter(0);  //reset the counter
								
								//rdrObj.SetProcessing(true);
								//rdrObj.SetCmdSent(false);
								rdrStatusList.Add(rdrObj2);
						
								//@@ start the process to close and open socket
								//@@ this process will handle in NetRdrReconn module
								//if (StartReconnProcessHandler != null)
								//StartReconnProcessHandler(readerEvent);

								return (true);
							}
							else //increment
							{
								Console.WriteLine("NetRdrConn - ProcessRdrError - reader #" + Convert.ToString(readerEvent.reader) + " Incrementing rdrObj counter");
								//increment counter to be used in polling timer to call EnableReader()
								rdrObj2.SetCounter(Convert.ToUInt16(rdrObj.GetCounter() + 1));  //reset the counter
								rdrObj2.SetProcessing(true);
								rdrObj2.online = false;
								//rdrStatusList.RemoveAt(i);  //new removed
								rdrStatusList.Remove(rdrObj);
								if (rdrObj.GetCounter() == 0)
                                   rdrObj2.SetStartTimeSec(DateTime.Now);
								rdrStatusList.Add(rdrObj2);
							}//incremet
						}//NO_RESPONSE
						else if (errorID == AW_API_NET.APIConsts.BUSY)
						{
							//Reader is busy servicing other command and is busy and
							//did not process the EnableReader() command.
							//may need to put some more code in here
							Console.WriteLine("NetRdrConn - ProcessRdrError - reader #" + Convert.ToString(readerEvent.reader) + " is BUSY");
						}	
					}//if reader in list
				}//for loop

				return (false); //no counter not maxed
			}
		}
		#endregion

		#region RemoveRdrFromList
		//do the same thing when adding rdr do a check.
		void RemoveRdrFromList (ushort rdrID)
		{
			foreach (readerStatStruct rdrObj in rdrStatusList)
			{
				if (rdrObj.rdrID == rdrID)
				{
				   rdrStatusList.Remove(rdrObj);
				   return;
				}
			}
		}
		#endregion

		#region SetRdrPolling
		public void SetRdrPolling(string ip, bool b)
		{ 
			//lock(myLock)
			{
				readerStatStruct rdrStatObj = new readerStatStruct(0);
				if (GetRdrFromList(ip, ref rdrStatObj))
				{
					readerStatStruct rdrStatObj1 = new readerStatStruct(0);
					rdrStatObj1.rdrID = rdrStatObj.rdrID;
					rdrStatObj1.hostID = rdrStatObj.hostID;
					rdrStatObj1.SetCounter(rdrStatObj.GetCounter());
					rdrStatObj1.SetPollReader(false);
					rdrStatObj1.online = rdrStatObj.online;
					rdrStatObj1.SetProcessing(rdrStatObj.GetProcessing());
					rdrStatObj1.SetStatus(rdrStatObj.GetStatus());
					rdrStatObj1.SetStartTimeSec(rdrStatObj.GetStartTimeSec());
					rdrStatObj1.SetIP(rdrStatObj.GetIP());
					rdrStatusList.Remove(rdrStatObj);
					rdrStatusList.Add(rdrStatObj1);
				}
			}//lock
		}
		#endregion

	}
}
