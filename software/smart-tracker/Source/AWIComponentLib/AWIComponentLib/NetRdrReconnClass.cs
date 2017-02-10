using System;
using System.Timers;
using System.Collections;
using System.Runtime.InteropServices;
using AW_API_NET;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using AWIComponentLib.Communication;
using AWIComponentLib.NetRdrConn;

namespace AWIComponentLib.NetRdrReconn
{
	/// <summary>
	/// Summary description for NetRdrReconnClass.
	/// </summary>

	public class NetRdrReconnClass
	{
		#region constructor
		public NetRdrReconnClass(CommunicationClass comm)
		{
			communication = comm;

			startProcessEvents = false;
			scanRdrIpListIndex = -1;
		    openSockRdrIpListIndex = -1;
		    resetRdrIpListIndex = -1;

			//Scanner
			OnRdrScanTimer = new Timer();
			OnRdrScanTimer.Interval = 1200;
			OnRdrScanTimer.AutoReset = true;
			OnRdrScanTimer.Enabled = true;
			OnRdrScanTimer.Elapsed += new ElapsedEventHandler(OnRdrScanTimerEvent);

			//open socket
			OnRdrOpenSocketTimer = new Timer();
			OnRdrOpenSocketTimer.Interval = 1400;
			OnRdrOpenSocketTimer.AutoReset = true;
			OnRdrOpenSocketTimer.Enabled = true;
			OnRdrOpenSocketTimer.Elapsed += new ElapsedEventHandler(OnRdrOpenSocketTimerEvent);

			// reset/powerup reader
			OnRdrRestTimer = new Timer();
			OnRdrRestTimer.Interval = 3000;
			OnRdrRestTimer.AutoReset = true;
			OnRdrRestTimer.Enabled = true;
			OnRdrRestTimer.Elapsed += new ElapsedEventHandler(OnRdrRestTimerEvent);

			//api = new APINetClass();
			pktID = 1;

			#region events
			//these events will be used by the communication class to let the module know of the events
			CommunicationClass.PowerupEventHandler += new PowerupEvent(this.PowerupReaderNotifty);
			CommunicationClass.ScanNetworkEventHandler += new ScanNetworkEvent(this.ScanNetworkEventNotify);
			CommunicationClass.OpenSocketEventHandler += new OpenSocketEvent(this.OpenSocketEventNotify);
			CommunicationClass.CloseSocketEventHandler += new CloseSocketEvent(this.CloseSocketEventNotify);
			NetRdrConnClass.StartReconnProcessHandler += new StartReconnProcess(this.StartReconnProcessNotify);
			#endregion
		}
		#endregion

		#region vars
        private APINetClass api;
		private ushort pktID;
		private ArrayList scanRdrIpList = new ArrayList();
		private ArrayList openSockRdrIpList = new ArrayList();
		private ArrayList resetRdrIpList = new ArrayList();
		public static Object myLock = new Object();
		private int scanRdrIpListIndex;
		private int openSockRdrIpListIndex;
		private int resetRdrIpListIndex;
		private Timer OnRdrScanTimer;
		private Timer OnRdrOpenSocketTimer;
		private Timer OnRdrRestTimer;
		private bool startProcessEvents;
		private CommunicationClass communication;
		#endregion

		#region OnRdrScanTimerEvent
		private void OnRdrScanTimerEvent(object source, ElapsedEventArgs e)
		{
			lock(myLock)
			{
				if ((scanRdrIpList.Count > 0) && startProcessEvents)
				{
					if (scanRdrIpListIndex >= scanRdrIpList.Count)
						scanRdrIpListIndex = 0;

					Byte[] ip = new Byte[20];
					char[] cIP = new char[20];
			
					string ipStr = (string)scanRdrIpList[scanRdrIpListIndex]; 
					cIP = ipStr.ToCharArray(0, ipStr.Length);
					for (int i=0; i<ipStr.Length; i++)
						ip[i] = Convert.ToByte(cIP[i]);

				
					if (pktID >= 224)
						pktID = 1;

					//int ret = api.rfScanIP(ip, pktID++);
					int ret = communication.ScanNetwork(ip);
					scanRdrIpListIndex += 1;
					Console.WriteLine("NetRdrReconn  rfScanIP() was called");
				}
			}//lock	
		}
		#endregion 

		#region OnRdrOpenSocketTimerEvent
		private void OnRdrOpenSocketTimerEvent(object source, ElapsedEventArgs e)
		{
			lock(myLock)
			{
				if ((openSockRdrIpList.Count > 0) && startProcessEvents)
				{
					if (openSockRdrIpListIndex >= openSockRdrIpList.Count)
						openSockRdrIpListIndex = 0;

					Byte[] ip = new Byte[20];
					char[] cIP = new char[20];
			
					string ipStr = (string)openSockRdrIpList[openSockRdrIpListIndex]; 
					cIP = ipStr.ToCharArray(0, ipStr.Length);
					for (int i=0; i<ipStr.Length; i++)
						ip[i] = Convert.ToByte(cIP[i]);

				
					if (pktID >= 224)
						pktID = 1;

					//int ret = api.rfOpenSocket(ip, 1, false, AW_API_NET.APIConsts.SPECIFIC_IP, pktID++);
					int ret = communication.SocketConnection(ip);
					openSockRdrIpListIndex += 1;
					Console.WriteLine("NetRdrReconn  rfOpenSocket() was called");
				}
			}//lock	
		}
		#endregion 

		#region OnRdrRestTimerEvent
		private void OnRdrRestTimerEvent(object source, ElapsedEventArgs e)
		{
			lock(myLock)
			{
				if ((resetRdrIpList.Count > 0) && startProcessEvents) //flag to prevent from processing normal communication routines
				{
					if (resetRdrIpListIndex >= resetRdrIpList.Count)
						resetRdrIpListIndex = 0;

					Byte[] ip = new Byte[20];
					char[] cIP = new char[20];
			
					string ipStr = (string)resetRdrIpList[resetRdrIpListIndex]; 
					cIP = ipStr.ToCharArray(0, ipStr.Length);
					for (int i=0; i<ipStr.Length; i++)
						ip[i] = Convert.ToByte(cIP[i]);

				
					if (pktID >= 224)
						pktID = 1;

					//need to check these api function - may need to use ResetReader
					//int ret = api.rfResetReaderSocket(1, ip, pktID++);
					//int ret = api.rfResetReader(1, 0, 0, AW_API_NET.APIConsts.ALL_READERS, pktID++);
					//int ret = communication.ResetReaderSocket(1, ip);
					int ret = communication.ResetAllReaders();
					resetRdrIpListIndex += 1;
					Console.WriteLine("NetRdrReconn  rfResetReaderSocket() was called time=" + DateTime.Now.ToString());
				}
			}//lock	
		}
		#endregion 

		#region StartReconnProcessNotify
		private void StartReconnProcessNotify(AW_API_NET.rfReaderEvent_t rdrEvent)
		{
			string ip;
			int ret;

			lock(myLock)
			{
				Console.WriteLine("NetRdrReconn  StartReconnProcessNotify is called");
				startProcessEvents = true;

				if ((ip=GetStringIP(rdrEvent.ip)) != "")
				{
					if (pktID >= 224)
						pktID = 1;

					//add ip to the list to get scaned
					if (!scanRdrIpList.Contains(ip))
					{
						ret = api.rfCloseSocket(rdrEvent.ip, AW_API_NET.APIConsts.SPECIFIC_IP);
						scanRdrIpList.Add(ip); //string

						//add to the scaniplist index
						if (scanRdrIpListIndex < 0)
							scanRdrIpListIndex = 0;
					}
				
					//ret = api.rfScanIP(rdrEvent.ip, pktID++); 
				}
			}//lock
		}
		#endregion

		#region ScanNetworkEventNotify
		private void ScanNetworkEventNotify(AW_API_NET.rfReaderEvent_t rdrEvent)
		{
			string ip;

			lock(myLock)
			{
				if (startProcessEvents) //flag to prevent from processing normal communication routines
				{
					Console.WriteLine("NetRdrReconn  ScanNetworkEventNotify is received");

					if ((ip=GetStringIP(rdrEvent.ip)) != "")
					{
						//remove it from scan list and add to opensock list
						scanRdrIpList.Remove(ip);
						if (scanRdrIpList.Count == 0)
							scanRdrIpListIndex = -1;

						openSockRdrIpList.Add(ip);

						if (openSockRdrIpListIndex < 0)
							openSockRdrIpListIndex = 0;

						//if (pktID >= 224)
						//pktID = 1;
						//ret = api.rfOpenSocket(rdrEvent.ip, 1, false, AW_API_NET.APIConsts.SPECIFIC_IP, pktID++); 
					}
				}
			}
		}
		#endregion

		#region OpenSocketEventNotify 
		private void OpenSocketEventNotify(AW_API_NET.rfReaderEvent_t rdrEvent)
		{
			string ip;

			lock(myLock)
			{
				if (startProcessEvents) //flag to prevent from processing normal communication routines
				{
					Console.WriteLine("NetRdrReconn  OpenSocketEventNotify is received");
					if ((ip=GetStringIP(rdrEvent.ip)) != "")
					{
						openSockRdrIpList.Remove(ip);
						if (openSockRdrIpList.Count == 0)
							openSockRdrIpListIndex = -1;

						resetRdrIpList.Add(ip);

						if (resetRdrIpListIndex < 0)
							resetRdrIpListIndex = 0;
					
					}
				}
			}
		}
		#endregion

		#region PowerupReaderNotifty
		private void PowerupReaderNotifty(AW_API_NET.rfReaderEvent_t rdrEvent)
		{
			string ip;

			lock(myLock)
			{
				if (startProcessEvents) //flag to prevent from processing normal communication routines
				{
					Console.WriteLine("NetRdrReconn  PowerupReaderNotifty is received");

					if ((ip=GetStringIP(rdrEvent.ip)) != "")
					{
						//remove it from reset reader list
						resetRdrIpList.Remove(ip);
						if (resetRdrIpList.Count == 0)
							resetRdrIpListIndex = -1;
					}
				}
			}
			
		}
		#endregion

		#region CloseSocketEventNotify
		private void CloseSocketEventNotify(AW_API_NET.rfReaderEvent_t rdrEvent)
		{

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

	}
}
