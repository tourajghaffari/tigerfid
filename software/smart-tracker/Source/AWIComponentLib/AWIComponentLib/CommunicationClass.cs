#region Changes
//     FEB01,06 - added TAG_DETECTED_RSSI event and event handler
//V4.0 DEC20,06 - added CallTag to the commands
//V6.0 May30,07 - adding module for polling the readers on the network and detecting
//                when they go offline and can not be recovered after the retries. An
//                event will be set to let the app know the reader is offline. 
//v6.0 Jul11,07 - changed SetRdrPolling to StartRdrPolling() and added to function
//                TrunOffRdrPolling() and TrunOnRdrPolling() to control polling of
//                readers by a flag.
//V9.0 Aug14,07 - Compiled with Visual Studio 2005 - No compile error but there were
//                runtime error for cross-thread violation. Forced the flag silence
//                so it dos not complain. (constructor)
#endregion

using System;
using System.Timers;
using System.Collections;
using System.Runtime.InteropServices;
using AW_API_NET;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;



namespace AWIComponentLib.Communication
{
    #region enums
    public enum relayStatusType { Open, Close, Unknown };
    //public enum relayStatType {Open, Close};
    #endregion

    #region delegates
    public delegate void PowerupEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void RdrErrorEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void PowerupFGenEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void PowerupSmartFGenEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void EndBroadcastEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void ResetReaderEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void EnableReaderEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void InputChangeEvent(ushort host, ushort rdr, ushort fgen, short input1, short input2);
	public delegate void ResetSmartFGenEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void ScanNetworkEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void OpenSocketEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void CloseSocketEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	public delegate void EnableRelayAckEvent(ushort relay, ushort reader);
	public delegate void DisableRelayAckEvent(ushort relay, ushort reader);
    public delegate void OpenOutputRelayEvent(ushort reader, ushort relay);
    public delegate void CloseOutputRelayEvent(ushort reader, ushort relay);
	
	//public delegate void ReaderOfflineEvent(AW_API_NET.rfReaderEvent_t rdrEvent);
	//public delegate void ReaderOnlineEvent(AW_API_NET.rfReaderEvent_t rdrEvent);

	public delegate void EnableTagAckEvent(AW_API_NET.rfTagEvent_t tagEvent);
	public delegate void DisableTagAckEvent(AW_API_NET.rfTagEvent_t tagEvent);
	public delegate void QueryTagAckEvent(AW_API_NET.rfTagEvent_t tagEvent);
	public delegate void TagDetectedEvent(AW_API_NET.rfTagEvent_t tagEvent);
	public delegate void TagDetectedRSSIEvent(AW_API_NET.rfTagEvent_t tagEvent);
    public delegate void TagDetectedSaniEvent(AW_API_NET.rfTagEvent_t tagEvent);
    public delegate void TagDetectedTamperEvent(AW_API_NET.rfTagEvent_t tagEvent);
	public delegate void TagDetectedRSSITamperEvent(AW_API_NET.rfTagEvent_t tagEvent);
	public delegate void TagErrorEvent(AW_API_NET.rfTagEvent_t tagEvent);

	//public delegate void UpdateRdrOfflineListEvent(ushort rdr);

	

	#endregion

	#region tagStruct
	[StructLayout(LayoutKind.Sequential)]
	public struct tagStruct
	{
		public uint	id;
		public byte type;
		public ushort reader;         
		public ushort fGen;
		public byte groupID;
		public DateTime timeStamp;
	}
	#endregion

	#region readerStruct
	[StructLayout(LayoutKind.Sequential)]
	public struct readerStruct
	{
		public ushort rdrID;        
		public ushort fGenID;
		public CmdRetryCollectionClass cmdRetry; // = new CmdRetryCollectionClass();
	}
	#endregion

    #region relayStruct
    [StructLayout(LayoutKind.Sequential)]
    public struct relayStruct
    {
        public ushort eventRdrID;
        public ushort actionRdrID;
        public ushort actionRelay;
        public bool autoMode;  
        public ushort duration;
        public bool start;
        public DateTime timeStamp;
        public relayStatusType relayStatus;

        public relayStruct (ushort eRdr, ushort aRdr, ushort aRelay, bool relayAutoMode, ushort dur)
        {
            eventRdrID = eRdr;
            actionRdrID = aRdr;
            actionRelay = aRelay;
            autoMode = relayAutoMode;
            duration = dur;
            timeStamp = DateTime.Now;
            start = false;
            relayStatus = relayStatusType.Open; 
        }
    }
    #endregion

	#region readerStatStruct
	[StructLayout(LayoutKind.Sequential)]
	public struct readerStatStruct
	{
		public ushort rdrID;        
		public ushort hostID;
		public bool online;
		private string ip;
		private ushort stat;  //0=offline, 1=online
		private ushort counter;
		private bool processing;
		private int startTimeSec;
		private bool pollReader;
		private bool searchRdr;
		private bool tagFound;

		public readerStatStruct(ushort rdr)
		{
			rdrID = rdr;
			hostID = 0;
			ip = "";
			stat = 0;
			counter = 0;
			processing = false;
			online = false;
			startTimeSec = 0;
			pollReader = true;
			searchRdr = false;
			tagFound = false;
		}

		public ushort GetCounter()
		{
			return (counter);
		}

		public string GetIP()
		{
			return (ip);
		}

		public ushort GetStatus()
		{
			return (stat);
		}

		public bool GetProcessing()
		{
			return (processing);
		}

		public bool GetPollReader()
		{
			return(pollReader);
		}

		public bool GetSrchStatus()
		{
            return (tagFound);
		}

		public void SetCounter(ushort n)
		{
			counter = n;
		}

		public bool GetSearchReader()
		{
			return (searchRdr);
		}

		public void SetIP(string  s)
		{
			ip = s;
		}

		public int GetStartTimeSec()
		{
			return (startTimeSec);
		}

		public void SetStatus(ushort status)
		{
			stat = status;
		}

		public void SetPollReader(bool b)
		{
			pollReader = b;
		}

		public void SetProcessing(bool b)
		{
			if (b)
			{
				processing = b;
				//DateTime timeNow = DateTime.Now;
				//startTimeSec = timeNow.Hour*3600 + timeNow.Minute*60 + timeNow.Second;
			}
		}

		public void SetStartTimeSec(int timeSec)
		{
			startTimeSec = timeSec;
		}

		public void SetStartTimeSec(DateTime time)
		{
			startTimeSec = time.Hour*3600 + time.Minute*60 + time.Second;
		}

		public void SetSearchReader(bool b)
		{
			searchRdr = b;
		}

		public void SetSrchStatus(bool b)
		{
			tagFound = b;
		}

		public void Copy(ref readerStatStruct rdrObj)
	    {
           rdrObj.rdrID = rdrID;
		   rdrObj.hostID = hostID;
           rdrObj.online = online;
           rdrObj.SetIP(ip);
           rdrObj.SetStatus(stat);
		   rdrObj.SetCounter(counter);
		   rdrObj.SetProcessing(processing);
		   rdrObj.SetStartTimeSec(startTimeSec);
		   rdrObj.SetPollReader(pollReader);
		   rdrObj.SetSearchReader(searchRdr);
		}
	}
	#endregion

	#region cmdStruct
	[StructLayout(LayoutKind.Sequential)]
	public struct cmdStruct
	{
		public ushort pktID;        
		public ushort cmd;
		public ushort rdr;
		public ushort relay;
		public ushort host;
		public ushort cmdType;
		public ushort retry;
		public DateTime timeStamp;
	}
	#endregion

	#region callbackStruct 
	[StructLayout(LayoutKind.Sequential)]
	public struct callbackStruct
	{
		public bool rEvent;        
		public bool tEvent;
		public bool sent;
		public int callbackId;
		public AW_API_NET.rfReaderEvent_t rdrEvent;
		public AW_API_NET.rfTagEvent_t tagEvent;
	}
	#endregion

	#region CallbackCollectionClass
	[Serializable]
	public class CallbackCollectionClass : CollectionBase
	{
		public callbackStruct this [int index]
		{
			get { return (callbackStruct) List[index];}
			set { List[index] = value;}
		}

		public int Add(callbackStruct callback)
		{
			return (List.Add(callback));
		}

		public void Insert(int index, callbackStruct callback)
		{
			List.Insert(index, callback);
		}

		public void Remove(callbackStruct callback)
		{
			List.Remove(callback);
		}

		public void RemoveFrom(int index)
		{
		   List.RemoveAt(index);
		}

		public bool Contain(callbackStruct callback)
		{
			return(List.Contains(callback));
		}

		public void Replace(int index, callbackStruct newCallback)
        {
           if (index >= 0) this[index] = newCallback;
        }

		public int IndexOf(int callbackId)
        {
           for (int i = 0; i < this.Count; i++)
           {
              if (this[i].callbackId == callbackId) return i;
           }
           return -1;
        }

		public bool IsEqual(callbackStruct callback)
		{
		   for (short i = 0; i < this.Count; i++)
           {
              if (this[i].callbackId == callback.callbackId)
				 return (true);   //found it match
		   }
			
		   return (false);   //did not find
		}

		public bool GetStatus(int index)
		{
		   return(this[index].sent);
		}

		public short IsEqualTagEvent(ushort type, uint tagId, byte groupID, ushort pktID)
		{
		   for (short i = 0; i < this.Count; i++)
           {
              if (this[i].tagEvent.tag.id == tagId)
			      //(this[i].tagEvent.pktID == pktID))
			  {
				  if ((type == AW_API_NET.APIConsts.RF_TAG_DETECTED) || 
					  (type == AW_API_NET.APIConsts.RF_TAG_DETECTED_RSSI))
				  {
                      if (this[i].tagEvent.tag.groupCount == groupID)
                      {
#if DEBUG
#if TRACE
                          Console.WriteLine("Duplicate Tag Detect Event (All Match): Tag ID {0}", tagId);
#endif
#endif
                          return (-2);   //found it all match
                      }
                      else
                      {
#if DEBUG
#if TRACE
                          Console.WriteLine("Duplicate Tag Detect Event (Match except Group ID): Tag ID {0}", tagId);
#endif
#endif
                          return (i);   //found it match except groupID
                      }
				  }
				  else	//all other commands
				  {
                      if ((this[i].tagEvent.tag.groupCount == groupID) &&
                          (this[i].tagEvent.pktID == pktID))
                      {
#if DEBUG
#if TRACE
                          Console.WriteLine("Duplicate Tag Event (All Match): Tag ID {0}", tagId);
#endif
#endif
                          return (-2);   //found it all match
                      }
                      else
                      {
#if DEBUG
#if TRACE
                          Console.WriteLine("Duplicate Tag Event (Match except Group ID): Tag ID {0}", tagId);
#endif
#endif
                          return (i);   //found it match except groupID
                      }
				  }
			  }
		   }
			
		   return (-1);   //did not find
		}

		public bool IsEqualRdrEvent(callbackStruct callback)
		{
		   //need to be coded
		   /*for (short i = 0; i < this.Count; i++)
           {
              if (this[i].callbackId == callback.callbackId)
				 return (true);   //found it match
		   }*/
			
		   return (false);   //did not find
		}
	}
	#endregion

	#region TagCollectionClass
	[Serializable]
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

		public bool IsExpired(int index, byte type)
		{
		   DateTime time = new DateTime(this[index].timeStamp.Ticks);
		   DateTime timeNow = DateTime.Now; 
		   int tSec = time.Hour*3600 + time.Minute*60 + time.Second;
		   int tSecNow = timeNow.Hour*3600 + timeNow.Minute*60 + timeNow.Second;
		   //if (type == AW_API_NET.APIConsts.INVENTORY_TAG)
		   //{
		       //if (tSecNow > (tSec + 7))  //timeToExpireTags))  //time to expire before sending querytag for linetag
		          //return (true);
		       //else
		          //return (false);
		   //}
		   //else
		   //{
			  if (tSecNow > (tSec + 7))   //timeToExpireTags + 5))  //time to expire for tags
		          return (true);
		       else
		          return (false);
		   //}

		}

		public short IsEqual(tagStruct tag, out short index)
		{
		   for (short i = 0; i < this.Count; i++)
           {
              if ((this[i].id == tag.id) && 
				  (this[i].type == tag.type))
			  {
				  if ((this[i].reader == tag.reader) &&
				      (this[i].fGen == tag.fGen) &&
				      (this[i].groupID == tag.groupID))
				  {
					  index = i;
					  if (IsExpired(i, 0))
						  return(i);      //foun match but expired
					  else
					      return (-1);   //found it match
				  }
				  else
				  {
				     index = i;
					 return (i);   //found it no match
			      }

			  }//if
		   }//for loop
			
		   index = -2;
		   return (-2);   //did not find
		}

		   /*
			  {
				  //startTime = this[i].timeStamp.Day*24*3600 + this[i].timeStamp.Hour*3600 + this[i].timeStamp.Minute*60 + this[i].timeStamp.Second + CommunicationClass.delay;
				  //endTime = tag.timeStamp.Day*24*3600 + tag.timeStamp.Hour*3600 + tag.timeStamp.Minute*60 + tag.timeStamp.Second;
				  //if (startTime > endTime)
					  return (1);   //found it
				  //else
				     //return (false);
			  }
		      else
			    return (false);  
           }//for
		  
		   return (false);
		}*/
	}
	#endregion 

	#region CmdRetryCollectionClass
	[Serializable]
	public class CmdRetryCollectionClass : CollectionBase
	{
		public cmdStruct this [int index]
		{
			get { return (cmdStruct) List[index];}
			set { List[index] = value;}
		}

		public int GetRetryCount (int index)
		{
			return(this[index].relay);
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

		public void Replace(cmdStruct oldCmd, cmdStruct newCmd)
        {
           int i = IndexOf(oldCmd.pktID);
           if (i >= 0) this[i] = newCmd;
        }

		public int IndexOf(ushort pktID)
        {
           for (int i = 0; i < this.Count; i++)
           {
              if (this[i].pktID == pktID) return i;
           }
           return -1;
        }

		public bool IsEqual(cmdStruct cmd)
		{
		   for (short i = 0; i < this.Count; i++)
           {
              if (this[i].pktID == cmd.pktID)
				 return (true);   //found it match
		   }//for loop
			
		   return (false);   //did not find
		}

		public bool IsExpired(int index)
		{
		   DateTime time = new DateTime(this[index].timeStamp.Ticks);
		   DateTime timeNow = DateTime.Now; 
		   int tSec = time.Hour*3600 + time.Minute*60 + time.Second;
		   int tSecNow = timeNow.Hour*3600 + timeNow.Minute*60 + timeNow.Second;
		   if (tSecNow > (tSec + 5))
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

	#region ReaderCollectionClass
	[Serializable]
	public class ReaderCollectionClass : CollectionBase
	{
		public readerStruct this [int index]
		{
			get { return (readerStruct) List[index];}
			set { List[index] = value;}
		}

		public int Add(readerStruct rdr)
		{
			rdr.cmdRetry = new CmdRetryCollectionClass();
			return (List.Add(rdr));
		}

		public void AddCmd(int index, cmdStruct cmd)
		{
			this[index].cmdRetry.Add(cmd);
		}

		public void RemoveCmd(int indexRdr, int indexCmd)
		{
			this[indexRdr].cmdRetry.RemoveFrom(indexCmd);
		}

		public void Insert(int index, readerStruct rdr)
		{
			rdr.cmdRetry = new CmdRetryCollectionClass();
			List.Insert(index, rdr);
		}

		public void Remove(readerStruct rdr)
		{
			List.Remove(rdr);
		}

		public bool Contain(readerStruct rdr)
		{
			return(List.Contains(rdr));
		}

		public void Replace(readerStruct oldRdr, readerStruct newRdr)
        {
           int i = IndexOf(oldRdr.rdrID);
		   newRdr.cmdRetry = new CmdRetryCollectionClass();
           if (i >= 0) this[i] = newRdr;
        }

		public int IndexOf(ushort rdrId)
        {
           for (int i = 0; i < this.Count; i++)
           {
              if (this[i].rdrID == rdrId) return i;
           }
           return -1;
        }

		public int IndexOfCmd(ushort rdrId, ushort pktID)
        {
           for (int i = 0; i < this.Count; i++)
           {
              if (this[i].rdrID == rdrId)
			  {
			     return this[i].cmdRetry.IndexOf(pktID);
			  }
           }
           return -1;
        }

		public bool IsEqual(readerStruct rdr)
		{
		   for (short i = 0; i < this.Count; i++)
           {
              if (this[i].rdrID == rdr.rdrID)
				 return (true);   //found it match
		   }//for loop
			
		   return (false);   //did not find
		}
	}
	#endregion

	#region IPCollectionClass
	[Serializable]
	public class IPCollectionClass : CollectionBase
	{
		public string this [int index]
		{
			get { return (string) List[index];}
			set { List[index] = value;}
		}

		public int Add(string ip)
		{
			return (List.Add(ip));
		}

		public void Insert(int index, string ip)
		{
			List.Insert(index, ip);
		}

		public void Remove(string ip)
		{
			List.Remove(ip);
		}

		public bool Contain(string ip)
		{
			return(List.Contains(ip));
		}
	}
	#endregion

	public class CommunicationClass
	{
        
		#region variables
		private APINetClass api;
		public static Object commLock = new Object();
		private static bool rs232Comm = false;
		private static uint baudrate;
		private static uint commPort;
		private ushort pktID = 1;
		//tagStruct detectedTag;
		private static ushort ENABLE_RELAY = 0x02;
		private static ushort DISABLE_RELAY = 0x03;
		private static ushort ENABLE_READER = 0x04;
		private static ushort RDR_RETRY_MAX_COUNT = 0x03;
		//private static ushort RDR_RETRY_MAX_TIME = 0x05;
		private const int TAG_EXPIERD_TIME = 6;
		private static Object myLock = new Object();
		//public static double delay = 5;  //sec
		//private int timeToExpireTags = 7;
		private System.Timers.Timer cmdTimer;
        private System.Timers.Timer callbackTimer;
		//private Timer pollReaderTimer;
		//private bool rdrPollTimerStart;
		//private ushort rdrIndexPoll;
		//private bool trunOnRdrPolling;
		//private ArrayList rdrStatusList = new ArrayList();
		private static CommunicationClass commObj;
        //public static Object syncObj = new Object();
        public static Object relSyncObj = new Object();
        private static ArrayList relayArrayList = new ArrayList();
        private Thread generalWorkerThread;
        private static bool activateCloseOutput;
        private static bool runGeneralThread;
        
        private static AW_API_NET.fReaderEvent rdrEventHandler;
        private static AW_API_NET.fTagEvent tagEventHandler;
        #endregion

		#region events
		public static event PowerupEvent PowerupEventHandler;
		public static event RdrErrorEvent RdrErrorEventHandler;
		public static event PowerupFGenEvent PowerupFGenEventHandler;
		public static event PowerupSmartFGenEvent PowerupSmartFGenEventHandler;
		public static event EndBroadcastEvent EndBroadcastEventHandler;
		public static event ResetReaderEvent ResetReaderEventHandler;
		public static event InputChangeEvent InputChangeEventHandler;
		public static event EnableReaderEvent EnableReaderEventHandler;
		public static event ResetSmartFGenEvent ResetSmartFGenEventHandler;
		public static event ScanNetworkEvent ScanNetworkEventHandler;
		public static event OpenSocketEvent OpenSocketEventHandler;
		public static event CloseSocketEvent CloseSocketEventHandler;

		public static event EnableTagAckEvent EnableTagEventHandler;
		public static event DisableTagAckEvent DisableTagEventHandler;
		public static event QueryTagAckEvent QueryTagEventHandler;
		public static event TagDetectedEvent TagDetectedEventHandler;
		public static event TagDetectedRSSIEvent TagDetectedRSSIEventHandler;
        public static event TagDetectedSaniEvent TagDetectedSaniEventHandler;
        public static event TagDetectedTamperEvent TagDetectedTamperEventHandler;
		public static event TagDetectedRSSITamperEvent TagDetectedRSSITamperEventHandler;
		public event TagErrorEvent TagErrorEventHandler;
		public static event EnableRelayAckEvent EnableRelayAckEventHandler;
		public static event DisableRelayAckEvent DisableRelayAckEventHandler;
        public static event OpenOutputRelayEvent OpenOutputRelayEventHandler;
        public static event CloseOutputRelayEvent CloseOutputRelayEventHandler;

		//will be used by OfflineRdrForm
		//public event UpdateRdrOfflineListEvent UpdateRdrOfflineListEventHandler;

		//public static event ReaderOfflineEvent ReaderOfflineEventHandler;
		//public static event ReaderOnlineEvent ReaderOnlineEventHandler;
		#endregion

		#region collections
		private static IPCollectionClass IPCollection = new IPCollectionClass();
		private static TagCollectionClass tagCollection = new TagCollectionClass();
		private static ReaderCollectionClass rdrCollection = new ReaderCollectionClass();
		private static CallbackCollectionClass	callbackCollection = new CallbackCollectionClass();
		#endregion

		#region OnCmdTimedEvent
		private static void OnCmdTimedEvent(object source, ElapsedEventArgs e) 
		{
			int rdrIndx = 0;
			int cmdIndx = 0;

			for (int i=0; i<rdrCollection.Count; i++)
			{
				for (int j=0; j<rdrCollection[i].cmdRetry.Count; j++)
				{
					cmdIndx = rdrCollection.IndexOfCmd(rdrCollection[i].rdrID, rdrCollection[i].cmdRetry[j].pktID); 
					if (rdrCollection[i].cmdRetry.IsExpired(cmdIndx))   //[j].timeStamp > rdrCollection[i].cmdRetry[j].timeStamp.AddSeconds(5))
					{
						rdrIndx = rdrCollection.IndexOf(rdrCollection[i].rdrID);
						rdrCollection.RemoveCmd(rdrIndx, cmdIndx);
						//Console.WriteLine("CMD struct was removed in timer ");
					}
				}
			}
		}
		#endregion

		#region OnCallbackTimedEvent
		private static void OnCallbackTimedEvent(object source, ElapsedEventArgs e)
		{
			lock(myLock)
			{
				if (callbackCollection.Count > 0)
				{
					//for (int i=0; i<callbackCollection.Count; i++)
					//{
					//if (callbackCollection[i].rEvent)
					//{
					//Console.WriteLine("Reader callback called");
					//}
					if ((callbackCollection[0].tEvent) && !callbackCollection.GetStatus(0))
					{
						//TagDetectedRSSIEventHandler(callbackCollection[i].tagEvent);   //old Apr 06
						AW_API_NET.rfTagEvent_t tagEvent2 = new rfTagEvent_t();
						tagEvent2 = callbackCollection[0].tagEvent;

						callbackCollection.RemoveFrom(0);

						if (tagEvent2.eventType == AW_API_NET.APIConsts.RF_TAG_QUERY)
							QueryTagEventHandler(tagEvent2);  
						else if (tagEvent2.eventType == AW_API_NET.APIConsts.RF_TAG_ENABLE)
							EnableTagEventHandler(tagEvent2); 
						else if (tagEvent2.eventType == AW_API_NET.APIConsts.RF_TAG_DISABLE)
							DisableTagEventHandler(tagEvent2);
						else if (tagEvent2.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED_RSSI)
							TagDetectedRSSIEventHandler(tagEvent2);  //new Apr 06
						else if (tagEvent2.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED) 
							TagDetectedEventHandler(tagEvent2);  //new Apr 06
						else if (tagEvent2.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED_TAMPERED) 
							TagDetectedTamperEventHandler(tagEvent2);
						else if (tagEvent2.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED_RSSI_TAMPERED) 
							TagDetectedRSSITamperEventHandler(tagEvent2);
                        else if (tagEvent2.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED_SANI)
                            TagDetectedSaniEventHandler(tagEvent2);
						
					
						/*if (callbackCollection[0].tagEvent.eventType == AW_API_NET.APIConsts.RF_TAG_QUERY)
							QueryTagEventHandler(callbackCollection[0].tagEvent);  
						else if (callbackCollection[0].tagEvent.eventType == AW_API_NET.APIConsts.RF_TAG_ENABLE)
							EnableTagEventHandler(callbackCollection[0].tagEvent);  
						else if ((callbackCollection[0].tagEvent.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED_RSSI) ||
								 (callbackCollection[0].tagEvent.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED))
							TagDetectedEventHandler(callbackCollection[0].tagEvent);  //new Apr 06
						
						//may be instead of replacing it jus do a delete and new entry  APR 06
						callbackCollection.RemoveFrom(0);  */
						
						//callbackCollection[i].SetStatus(1);
						//------------------------------------
						//callbackCollection.RemoveFrom(0);
						////callbackStruct callback  = new callbackStruct();
						////callback.callbackId = callbackCollection[i].callbackId;
						////callback.rEvent = callbackCollection[i].rEvent;
						////callback.tEvent = callbackCollection[i].tEvent;
						////callback.rdrEvent = callbackCollection[i].rdrEvent;
						////callback.tagEvent = callbackCollection[i].tagEvent;
						////callback.sent = true;
						////callbackCollection.Replace(i, callback);

						//--------------------------------
						//Console.WriteLine("Tag callback called + id:"+ callbackCollection[i].tagEvent.tag.id+ "  gID:"+callbackCollection[i].tagEvent.tag.groupCount);
						return;
						//}
					}
				}

				/*if (callbackCollection.Count > 0)
				{
					for (int i=0; i<callbackCollection.Count; i++)
					{
					   if (callbackCollection[i].rEvent)
					   {
						   //Console.WriteLine("Reader callback called");
					   }
					   else if ((callbackCollection[i].tEvent) && !callbackCollection.GetStatus(i))
					   {
						   //TagDetectedRSSIEventHandler(callbackCollection[i].tagEvent);   //old Apr 06
						
						   if (callbackCollection[i].tagEvent.eventType == AW_API_NET.APIConsts.RF_TAG_QUERY)
							   QueryTagEventHandler(callbackCollection[i].tagEvent);  
						   else if (callbackCollection[i].tagEvent.eventType == AW_API_NET.APIConsts.RF_TAG_ENABLE)
							   EnableTagEventHandler(callbackCollection[i].tagEvent);  
						   else if ((callbackCollection[i].tagEvent.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED_RSSI) ||
							   (callbackCollection[i].tagEvent.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED))
							   TagDetectedEventHandler(callbackCollection[i].tagEvent);  //new Apr 06
						
						   //may be instead of replacing it jus do a delete and new entry  APR 06

						   //callbackCollection[i].SetStatus(1);
						   //------------------------------------
						   callbackCollection.RemoveFrom(i);
						   ////callbackStruct callback  = new callbackStruct();
						   ////callback.callbackId = callbackCollection[i].callbackId;
						   ////callback.rEvent = callbackCollection[i].rEvent;
						   ////callback.tEvent = callbackCollection[i].tEvent;
						   ////callback.rdrEvent = callbackCollection[i].rdrEvent;
						   ////callback.tagEvent = callbackCollection[i].tagEvent;
						   ////callback.sent = true;
						   ////callbackCollection.Replace(i, callback);

						   //--------------------------------
						   //Console.WriteLine("Tag callback called + id:"+ callbackCollection[i].tagEvent.tag.id+ "  gID:"+callbackCollection[i].tagEvent.tag.groupCount);
						   return;
					   }
					}
				}*/
			}//lock
		}
		#endregion

        #region ~CommunicationClass distructor
        ~CommunicationClass()
		{
           if (commObj != null)
			   Console.WriteLine("Destroctor is called");                      
        }
        #endregion

        #region CommunicationClass constructor
        public CommunicationClass()
		{
			api = new APINetClass();
			
            rdrEventHandler = new AW_API_NET.fReaderEvent(ReaderEvent);
			api.rfRegisterReaderEvent(rdrEventHandler);
			tagEventHandler = new AW_API_NET.fTagEvent(TagEvent);
            api.rfRegisterTagEvent(tagEventHandler);
			//NetRdrConnClass.ReaderOfflineEventHandler +=new ReaderOfflineEvent(this.ReaderOfflineEventNotify);

			commObj = null;

			//rdrPollTimerStart = false;  //stop timer for OnPollReaderTimerEvent until it is set
			//rdrIndexPoll = 0;  //index of pollReaderStruct
			//trunOnRdrPolling = false;

            cmdTimer = new System.Timers.Timer();
			cmdTimer.Interval = 1000;
			cmdTimer.AutoReset = true;
			cmdTimer.Enabled = true;
			cmdTimer.Elapsed += new ElapsedEventHandler(OnCmdTimedEvent);

            callbackTimer = new System.Timers.Timer();
			callbackTimer.Interval = 500;
			callbackTimer.AutoReset = true;
			callbackTimer.Enabled = true;
			callbackTimer.Elapsed += new ElapsedEventHandler(OnCallbackTimedEvent);

            generalWorkerThread = new Thread(GeneralThread);
            runGeneralThread = true;
            StartGeneralThread();
           
			//timer for polling the readers for connectivity
			/*pollReaderTimer = new Timer();
			pollReaderTimer.Interval = 1500;
			pollReaderTimer.AutoReset = true;
			pollReaderTimer.Enabled = true;
			pollReaderTimer.Elapsed += new ElapsedEventHandler(OnPollReaderTimerEvent);*/
		}
		#endregion

        #region GeneralThread()
        private void GeneralThread()
        {
            while (runGeneralThread)
            {
                if (activateCloseOutput)
                {
                    //check relayArraylist for autoMod and expiration of timestamp
                    DateTime t;
                    lock (relSyncObj)
                    {
                        foreach (relayStruct relay in relayArrayList)
                        {
                            if (relay.start)
                            {
                                //relay.pending = false;
                                t = relay.timeStamp;
                                t = t.AddSeconds(relay.duration); //sec
                                if (DateTime.Now >= t)
                                {
                                    //send closeRelay
                                    DisableOutputRelay(relay.actionRdrID, relay.actionRelay);
                                    //Console.WriteLine("Communication  CLOSE RELAY - Rdr:" + relay.actionRdrID.ToString() + "  Relay:" + relay.actionRelay.ToString() + "  StartTime:" + relay.timeStamp.ToString() + " CloseTime:" + DateTime.Now.ToString());
                                    Thread.Sleep(100); //msec  chg 500 -> 100
                                }
                            }
                        }
                    }
                }//activateCloseOutput

                //if (relayArrayList.Count == 0)
                    //activateCloseOutput = false;
                 

                Thread.Sleep(1000);

            }//while
        }
        #endregion

        #region Cleanup()
        public void Cleanup()
        {
            runGeneralThread = false;
            generalWorkerThread.Join(500);
            Console.WriteLine("######################## IAM IN CLEANUP() IN COMMUNICATION CLASS #############");
            cmdTimer.Enabled = false;
            cmdTimer.Dispose();

            callbackTimer.Enabled = false;
            callbackTimer.Dispose();
        }
        #endregion

        #region GetGeneralThreadStatus()
        private bool GetGeneralThreadStatus()
        {
            return (generalWorkerThread.IsAlive);
        }
        #endregion

        #region StartGeneralThread()
        private void StartGeneralThread()
        {
            generalWorkerThread.Start();
        }
        #endregion

#if DEPRECATED
        #region SuspendGeneralThread()
        private void SuspendGeneralThread()
        {
            generalWorkerThread.Suspend();
        }
        #endregion
#endif

        #region AbortGeneralThread()
        public void AbortGeneralThread()
        {
            //generalWorkerThread.Abort();
            runGeneralThread = false;
            generalWorkerThread.Join(100);
        }
        #endregion

        #region IsGeneralThreadAlive()
        public bool IsGeneralThreadAlive()
        {
            return (generalWorkerThread.IsAlive);
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

		#region SendTagToApp
		private bool SendTagToApp(AW_API_NET.rfTagEvent_t tagEvent)
		{
			/*if (CheckMultiTag_A(tagEvent))
			{
			   Console.WriteLine("Tag is in container");
			   return (false);
			}
			else
			{
			   Console.WriteLine("Tag is not in container");
			   return (true);
			}*/

			return(true);
		}
		#endregion

		#region CheckMultiTag_A
		private short CheckMultiTag_A(AW_API_NET.rfTagEvent_t tagEvent)
		{
			if (tagCollection.Count == 0)
				return (0);

			tagStruct detTag = new tagStruct();
			detTag.id = tagEvent.tag.id;
			detTag.type = tagEvent.tag.tagType;
			detTag.reader = tagEvent.reader;
			detTag.fGen = tagEvent.fGenerator;
			detTag.groupID = tagEvent.tag.groupCount;
			detTag.timeStamp = DateTime.Now;
			short index = 0;
			short  ret = tagCollection.IsEqual(detTag, out index);
			if (ret == 1)
				return (1);  //found it and matched
			else if (ret == -1)
			{   //tagCollection.
				//found and no match
				tagCollection.Add(detTag);
				return (-1);
			}
			else
			{
				//did not find it
				tagCollection.Add(detTag);
				return (0);
			}
		}
		#endregion

		#region CheckMultiTag
		public static short CheckMultiTag(AW_API_NET.rfTagEvent_t tagEvent)
		{
			lock(myLock)
			{
				//if (tagCollection.Count == 0)
				//return (0);

				tagStruct detTag = new tagStruct();
				detTag.id = tagEvent.tag.id;
				detTag.type = tagEvent.tag.tagType;
				detTag.reader = tagEvent.reader;
				detTag.fGen = tagEvent.fGenerator;
				detTag.groupID = tagEvent.tag.groupCount;
				detTag.timeStamp = DateTime.Now;

				if (tagCollection.Count == 0)
				{
					if ((tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG) ||
						(tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG))
					{
						tagCollection.Add(detTag);
						return (-2);
					}
				}

				short index = 0;
				short  ret = tagCollection.IsEqual(detTag, out index);


				//remove inv if stay in que more than time specified
				if (index >= 0) //ret=-1, 0, 1, 2, ..
				{
					if ((tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG) ||
						(tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG))
					{
						if (tagCollection.IsExpired(index, tagEvent.tag.tagType))
						{
							//found it no match
						     
							tagCollection.RemoveAt(index); //Replace(tagCollection.List[ret], detTag); 
							tagCollection.Add(detTag);
							return (-2);
						}
					}
				}

				if (ret == -1)
					return (-1);  //found it and matched
				else if (ret == -2)
				{   
					//did not find
					tagCollection.Add(detTag);
					return (-2);
				}
				else
				{
					//found it no match
					tagCollection.RemoveAt(index); //Replace(tagCollection.List[ret], detTag); 
					tagCollection.Add(detTag);
					return (0);
				}
			}//lock
		}
		#endregion

		#region InitCallback
		private void InitCallback(callbackStruct callback)
		{
			callback.rEvent = false;
			callback.tEvent = false;
			callback.sent = false;
		}
		#endregion

		#region LoadCallbackCollection
		void LoadCallbackCollection(AW_API_NET.rfTagEvent_t tagEvent)
		{
		   int ret = callbackCollection.IsEqualTagEvent(tagEvent.eventType, tagEvent.tag.id, tagEvent.tag.groupCount, tagEvent.pktID);
		   if ( ret != -2)
	       {
				if (ret >= 0)
				   callbackCollection.RemoveFrom(Convert.ToInt16(ret));
				callbackStruct callback = new callbackStruct();
				InitCallback(callback);
				callback.callbackId = callbackCollection.Count;
				callback.tEvent = true;
				callback.tagEvent = tagEvent;
				callbackCollection.Add(callback);
			}
		}
		#endregion

		#region TagEvent 
		public int TagEvent(AW_API_NET.rfTagEvent_t tagEvent)
		{
			//short ret = 0;

			#region RF_E_ERROR
			if (tagEvent.errorStatus != AW_API_NET.APIConsts.RF_E_NO_ERROR)
			{
				if (TagErrorEventHandler != null)
				{
					callbackStruct callback = new callbackStruct();
					InitCallback(callback);
					callback.callbackId = callbackCollection.Count;
					callback.tEvent = true;
					callback.tagEvent = tagEvent;
					callbackCollection.Add(callback);
					//TagErrorEventHandler(tagEvent);
				}
				return (0);
			}
			#endregion

			switch (tagEvent.eventType)
			{

					#region RF_TAG_QUERY
				    case AW_API_NET.APIConsts.RF_TAG_QUERY:
						
						if (QueryTagEventHandler != null)
						{
							//Console.WriteLine("Disable Tag event was sent to app.");
							QueryTagEventHandler(tagEvent);
						}

						/*if (QueryTagEventHandler != null) 
						{
							//if (SendTagToApp(tagEvent))
							lock(commLock)
							{
								//if (TagErrorEventHandler != null)
							{
								ret = callbackCollection.IsEqualTagEvent(tagEvent.eventType, tagEvent.tag.id, tagEvent.tag.groupCount, tagEvent.pktID);
								if ( ret != -2)
								{
									if (ret > 0)
										callbackCollection.RemoveFrom(Convert.ToInt16(ret));
									callbackStruct callback = new callbackStruct();
									InitCallback(callback);
									callback.callbackId = callbackCollection.Count;
									callback.tEvent = true;
									callback.tagEvent = tagEvent;
									callbackCollection.Add(callback);
									//TagDetectedEventHandler(tagEvent);
								}
							}
							    
							}
						}*/
					break;
					#endregion

					#region RF_TAG_ENABLE
				    case AW_API_NET.APIConsts.RF_TAG_ENABLE:
						
						if (EnableTagEventHandler != null)
						{
							//Console.WriteLine("Disable Tag event was sent to app.");
							EnableTagEventHandler(tagEvent);
						}

						/*if (EnableTagEventHandler != null) 
						{
							//if (SendTagToApp(tagEvent))
							lock(commLock)
							{
								//if (TagErrorEventHandler != null)
							{
								ret = callbackCollection.IsEqualTagEvent(tagEvent.eventType, tagEvent.tag.id, tagEvent.tag.groupCount, tagEvent.pktID);
								if ( ret != -2)
								{
									if (ret > 0)
										callbackCollection.RemoveFrom(Convert.ToInt16(ret));
									callbackStruct callback = new callbackStruct();
									InitCallback(callback);
									callback.callbackId = callbackCollection.Count;
									callback.tEvent = true;
									callback.tagEvent = tagEvent;
									callbackCollection.Add(callback);
									//TagDetectedEventHandler(tagEvent);
								}
							}
							    
							}
						}*/
					break;
					#endregion

					#region RF_TAG_DISABLE
				    case AW_API_NET.APIConsts.RF_TAG_DISABLE:
						//lock(commLock)
						{
							if (DisableTagEventHandler != null)
							{
								//Console.WriteLine("Disable Tag event was sent to app.");
								DisableTagEventHandler(tagEvent);
							}
						}

						/*if (DisableTagEventHandler != null) 
						{
							//if (SendTagToApp(tagEvent))
							lock(commLock)
							{
								//if (TagErrorEventHandler != null)
							
								ret = callbackCollection.IsEqualTagEvent(tagEvent.eventType, tagEvent.tag.id, tagEvent.tag.groupCount, tagEvent.pktID);
								if ( ret != -2)
								{
									if (ret > 0)
										callbackCollection.RemoveFrom(Convert.ToInt16(ret));
									callbackStruct callback = new callbackStruct();
									InitCallback(callback);
									callback.callbackId = callbackCollection.Count;
									callback.tEvent = true;
									callback.tagEvent = tagEvent;
									callbackCollection.Add(callback);
									//TagDetectedEventHandler(tagEvent);
								}
								    
							}
						}*/
					break;
					#endregion

					#region RF_TAG_DETECTED
				    case AW_API_NET.APIConsts.RF_TAG_DETECTED:

                        //check if no 433 and tamper
						if (!tagEvent.tag.status.continuousField && tagEvent.tag.status.tamperSwitch)
						{
							if (TagDetectedTamperEventHandler != null)
							{
								tagEvent.eventType = AW_API_NET.APIConsts.RF_TAG_DETECTED_TAMPERED;
								TagDetectedTamperEventHandler(tagEvent);
							}
						}
						else
						{
							if (TagDetectedEventHandler != null)
							{
                                TagDetectedEventHandler(tagEvent);
							}
						}
				    
					    /*
					    //check if no 433 and tamper
						if (!tagEvent.tag.status.continuousField && tagEvent.tag.status.tamperSwitch)
						{
							if ((TagDetectedTamperEventHandler != null) && (rdrCollection.IndexOf(tagEvent.reader) >= 0))
							{
								tagEvent.eventType = AW_API_NET.APIConsts.RF_TAG_DETECTED_TAMPERED;
								lock(commLock)
								{
									LoadCallbackCollection(tagEvent);
								}
							}
						}
						else if ((TagDetectedEventHandler != null) && (rdrCollection.IndexOf(tagEvent.reader) >= 0))
						{
							//if (SendTagToApp(tagEvent))
							lock(commLock)
							{
								//if (TagErrorEventHandler != null)
							{
								ret = callbackCollection.IsEqualTagEvent(tagEvent.eventType, tagEvent.tag.id, tagEvent.tag.groupCount, tagEvent.pktID);
								if ( ret != -2)
								{
									if (ret >= 0)
										callbackCollection.RemoveFrom(Convert.ToInt16(ret));
									callbackStruct callback = new callbackStruct();
									InitCallback(callback);
									callback.callbackId = callbackCollection.Count;
									callback.tEvent = true;
									callback.tagEvent = tagEvent;
									callbackCollection.Add(callback);
									//TagDetectedEventHandler(tagEvent);

									///////////////////////////////
									/*tagStruct detTag = new tagStruct();
										detTag.id = tagEvent.tag.id;
										detTag.type = tagEvent.tag.tagType;
										detTag.reader = tagEvent.reader;
										detTag.fGen = tagEvent.fGenerator;
										detTag.groupID = tagEvent.tag.groupCount;
										detTag.timeStamp = DateTime.Now;
										short index = 0;
										if (tagCollection.Count > 0)
										ret = tagCollection.IsEqual(detTag, out index);
										else
										{
										tagCollection.Add(detTag);
										return (-2);
										}

										//remove inv if stay in que more than 3 sec
										if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)  
										{
											if (tagCollection.IsExpired(index, tagEvent.tag.tagType))
											{
												//found it no match
												tagCollection.RemoveAt(ret); //Replace(tagCollection.List[ret], detTag); 
												tagCollection.Add(detTag);
												return (-2);
											}
										}

										if (ret == -1)
											return (-1);  //found it and matched
										else if (ret == -2)
										{   
											//did not find
											tagCollection.Add(detTag);
											return (-2);
										}
										else
										{
											//found it no match
											tagCollection.RemoveAt(ret); //Replace(tagCollection.List[ret], detTag); 
											tagCollection.Add(detTag);
											return (0);
										}  
									////////////////////////////
								}
							}
							    
							}
						}*/
					break;
					#endregion

					#region RF_TAG_DETECTED_RSSI
				    case AW_API_NET.APIConsts.RF_TAG_DETECTED_RSSI:
					   //if (TagDetectedRSSIEventHandler != null) 
						   //TagDetectedRSSIEventHandler(tagEvent);

						//if (!tagEvent.tag.status.continuousField && tagEvent.tag.status.tamperSwitch)
					    if (tagEvent.tag.status.tamperSwitch)
						{
							if (TagDetectedRSSITamperEventHandler != null)
							{
								tagEvent.eventType = AW_API_NET.APIConsts.RF_TAG_DETECTED_RSSI_TAMPERED;
								TagDetectedRSSITamperEventHandler(tagEvent);
							}
						}
						else
						{
							if (TagDetectedRSSIEventHandler != null)
								TagDetectedRSSIEventHandler(tagEvent);
						}

					    /*if (!tagEvent.tag.status.continuousField && tagEvent.tag.status.tamperSwitch)
						{
							if ((TagDetectedRSSITamperEventHandler != null) && (rdrCollection.IndexOf(tagEvent.reader) >= 0))
							{
								tagEvent.eventType = AW_API_NET.APIConsts.RF_TAG_DETECTED_RSSI_TAMPERED;
								lock(commLock)
								{
									LoadCallbackCollection(tagEvent);
								}
							}
						}
						else if ((TagDetectedRSSIEventHandler != null) && (rdrCollection.IndexOf(tagEvent.reader) >= 0))
						{
							lock(commLock)
							{
								//if (TagErrorEventHandler != null)
								{
								ret = callbackCollection.IsEqualTagEvent(tagEvent.eventType, tagEvent.tag.id, tagEvent.tag.groupCount, tagEvent.pktID);
								if ( ret != -2)
								{
									if (ret >= 0)
										callbackCollection.RemoveFrom(Convert.ToInt16(ret));
									callbackStruct callback = new callbackStruct();
									InitCallback(callback);
									callback.callbackId = callbackCollection.Count;
									callback.tEvent = true;
									callback.tagEvent = tagEvent;
									callbackCollection.Add(callback);
									//TagDetectedEventHandler(tagEvent);
								}
								//else
								}
							   
							}
						}*/
					break;
					#endregion

                    #region RF_TAG_DETECTED_SANI
                    case AW_API_NET.APIConsts.RF_TAG_DETECTED_SANI:
                    if (TagDetectedSaniEventHandler != null)
                        TagDetectedSaniEventHandler(tagEvent);
                    break;
                    #endregion
            }

			return (0);
		}
		#endregion

		#region ReaderEvent
		public int ReaderEvent(AW_API_NET.rfReaderEvent_t readerEvent)
		{
			string ipStr = null;
			int indexRdr = 0;
			int indexCmd = 0;
			short input1 = -1;
			short input2 = -1;

			if (readerEvent.errorStatus != AW_API_NET.APIConsts.RF_E_NO_ERROR)
			{
				if (readerEvent.errorStatus == AW_API_NET.APIConsts.RF_E_NO_RESPONSE)
				{
					if (RdrErrorEventHandler != null)
						RdrErrorEventHandler(readerEvent);
				}

				//#####
				/*if (trunOnRdrPolling)
				{
					//find the status of that rdr from reader array
					readerStatStruct readerObj = new readerStatStruct(readerEvent.reader);
					if (GetRdrFromList (readerEvent.reader, out readerObj))
					{
                        //check what is the error type
						if (readerEvent.errorStatus == AW_API_NET.APIConsts.RF_E_NO_RESPONSE)
						{
							//if reached max
							if (readerObj.counter >= MAX_READER_NO_RESPONSE)
							{
								//if reader prev stat was online set an event in the application
								//will set event once until the status goes back to online
								if (readerObj.stat == 0) 
								{
                                   //set offline event in the app if the event is requested by app.
								   if (ReaderOfflineEventHandler != null)
									   ReaderOfflineEventHandler(readerEvent);
								}

                                readerObj.stat = 1;      //reader offline
								readerObj.counter = 0;  //reset the counter
								
								//start the process to close and open socket
							}
							else
							{
							   //increment counter to be used in polling timer to call EnableReader()
                               readerObj.counter += 1;
							}
						}
						else if (readerEvent.errorStatus == AW_API_NET.APIConsts.BUSY)
						{
							//Reader is busy servicing other command and is busy and
							//did not process the EnableReader() command.
                            //may need to put some more code in here
						} 
					}

				   
				   
				   //if NOT_RESPONDING .....
					   
				   //if RESDER BUSY .....
				}*/

					/*indexRdr = rdrCollection.IndexOf(readerEvent.reader);
					indexCmd = rdrCollection.IndexOfCmd(readerEvent.reader, readerEvent.pktID);
					if (readerEvent.eventType == AW_API_NET.APIConsts.RF_RELAY_DISABLE) 
					{
						if ((indexCmd >= 0) && (indexRdr >= 0))
						{
							if (rdrCollection[indexRdr].cmdRetry[indexCmd].relay < RDR_RETRY_MAX_COUNT)
							{
								cmdStruct cmd  = new cmdStruct();
								cmd = rdrCollection[indexRdr].cmdRetry[indexCmd];
								cmd.retry += 1;
								rdrCollection[indexRdr].cmdRetry.Replace(rdrCollection[indexRdr].cmdRetry[indexCmd], cmd);
								DisableOutput(readerEvent.relay, readerEvent.reader, false);
							}
							else
							{
								//Console.WriteLine("CMD removed in ReaderEvent MAXCOUNT Error Case. cmdIndex = " + indexCmd); // - timeStamp Start: " + time.ToLongTimeString() + "   timeStamp Now: " + DateTime.Now.ToLongTimeString() + "  Index = " + index);
								rdrCollection.RemoveCmd(indexRdr, indexCmd);
							}
						}
					}*/

					//if ((indexCmd >= 0) && (indexRdr >= 0))
					//rdrCollection.RemoveCmd(indexRdr, indexCmd);
					//DisableOutput(ushort relay, ushort rdr, bool firstCall)
				//}

				//if (RdrErrorEventHandler != null)
					//RdrErrorEventHandler(readerEvent);

				return (0);
			}

			switch (readerEvent.eventType)
			{
				case AW_API_NET.APIConsts.RF_READER_POWERUP:
                     
					if (PowerupEventHandler != null)
					{
						if (rdrCollection.IndexOf(readerEvent.reader) == -1)
						{
							readerStruct rdr = new readerStruct();
							rdr.rdrID = readerEvent.reader;
							rdrCollection.Add(rdr);

							//#####
							//populating rdrStat for polling rdr module
							//check if reader is on network
							/*if (commObj != null)
							{
								if (GetStringIP(readerEvent.ip) != "")
								{
									readerStatStruct rdrStat = new readerStatStruct(readerEvent.reader);
									rdrStat.hostID = readerEvent.host;
									rdrStat.ip = GetStringIP(readerEvent.ip);
									rdrStat.stat = 1; //online
									commObj.rdrStatusList.Add(rdrStat);  //polling rdr list
								}
							}*/
						}

						//let the app know about the powerup event
						PowerupEventHandler(readerEvent);

						//@@ if event set for NetRdrReconn, let the module know about the event
					}
					break;

				case AW_API_NET.APIConsts.RF_STD_FGEN_POWERUP:
					if (PowerupFGenEventHandler != null)
						PowerupFGenEventHandler(readerEvent);
				break;

				case AW_API_NET.APIConsts.RF_SMART_FGEN_POWERUP:
					if (PowerupSmartFGenEventHandler != null)
						PowerupSmartFGenEventHandler(readerEvent);
				break;

				case AW_API_NET.APIConsts.RF_END_OF_BROADCAST:
					if (EndBroadcastEventHandler != null)
					{
						Console.WriteLine("End of Broadcast event was sent to app.");
						EndBroadcastEventHandler(readerEvent);
					}
				break;

				case AW_API_NET.APIConsts.RF_READER_RESET:
				case AW_API_NET.APIConsts.RF_READER_RESET_ALL:
					if (ResetReaderEventHandler != null)
						ResetReaderEventHandler(readerEvent);
				break;

				case AW_API_NET.APIConsts.RF_READER_ENABLE:
				case AW_API_NET.APIConsts.RF_READER_ENABLE_ALL:

					if (EnableReaderEventHandler != null)
						EnableReaderEventHandler(readerEvent);

					//#####
					//if polling rdr is switched on
					/*if (trunOnRdrPolling)
					{
						readerStatStruct readerObj = new readerStatStruct(readerEvent.reader);
						if (GetRdrFromList (readerEvent.reader, out readerObj))
						{
							//if prev rdr stat was offline set the event for online
							if (readerObj.stat == 1)
							{
								if (ReaderOnlineEventHandler != null)
									ReaderOnlineEventHandler(readerEvent);
							}

							//set the params to online and reset the counter
                            readerObj.stat = 0;  //online
                            readerObj.counter = 0;
						}
					}*/
				break;

				case AW_API_NET.APIConsts.RF_RESET_SMART_FGEN:
				case AW_API_NET.APIConsts.RF_RESET_SMART_FGEN_ALL:
					if (ResetSmartFGenEventHandler != null)
						ResetSmartFGenEventHandler(readerEvent);
				break;

				case AW_API_NET.APIConsts.RF_SCAN_NETWORK:
				case AW_API_NET.APIConsts.RF_SCAN_IP:
					if (ScanNetworkEventHandler != null)
					{
						ipStr = GetStringIP(readerEvent.ip);
						if (IPCollection.Count > 0)
						{
							if (!IPCollection.Contain(ipStr))
								IPCollection.Add(ipStr);
						}
						else
						{
							IPCollection.Add(ipStr);
						}
						
						//let the app know about the scanning the ip
						ScanNetworkEventHandler(readerEvent);

						//@@ if event set for NetRdrReconn, let the module know about the event
					}
				break;

				case AW_API_NET.APIConsts.RF_OPEN_SOCKET:

					//let the app know about the open socket event
					if (OpenSocketEventHandler != null)
						OpenSocketEventHandler(readerEvent);

					//@@ if event set for NetRdrReconn, let the module know about the event

				break;

				case AW_API_NET.APIConsts.RF_CLOSE_SOCKET:

					//RemoveRdrFromList(readerEvent.reader);
					
					if (CloseSocketEventHandler != null)
						CloseSocketEventHandler(readerEvent);
				break;

				case AW_API_NET.APIConsts.RF_RELAY_ENABLE:
					//Console.WriteLine("@@COMM CLASS RELAY ENABLED");
                    Console.WriteLine("Communication  OPEN RELAY - Rdr:" + Convert.ToString(readerEvent.reader) + "  Relay:" + Convert.ToString(readerEvent.relay) + " OpenTime:" + DateTime.Now.ToString());
					indexRdr = rdrCollection.IndexOf(readerEvent.reader);
					indexCmd = rdrCollection.IndexOfCmd(readerEvent.reader, readerEvent.pktID);
					if ((indexCmd >= 0) && (indexRdr >= 0))
					{
						rdrCollection.RemoveCmd(indexRdr, indexCmd);
					}

                    lock (relSyncObj)
                    {
                        if (activateCloseOutput)
                            SetStartCloseRelay(readerEvent.reader, readerEvent.relay);
                    }

					//Console.WriteLine("Comm Relay Enable ACK " + readerEvent.relay);
					//lock (commLock)
					//{
						if (EnableRelayAckEventHandler != null)
							EnableRelayAckEventHandler(readerEvent.relay, readerEvent.reader);
					//}


                    //this part will replace the above code in the future
                    //if (OpenOutputRelayEventHandler != null)
                        //OpenOutputRelayEventHandler(readerEvent.reader, readerEvent.relay);

				break;

				case AW_API_NET.APIConsts.RF_RELAY_DISABLE:
					//Console.WriteLine("@@COMM CLASS RELAY DISABLED");
					indexRdr = rdrCollection.IndexOf(readerEvent.reader);
					indexCmd = rdrCollection.IndexOfCmd(readerEvent.reader, readerEvent.pktID);
					if ((indexCmd >= 0) && (indexRdr >= 0))
					{
						//Console.WriteLine("CMD struct was removed in ACK Disable ");
						rdrCollection.RemoveCmd(indexRdr, indexCmd);
					}
					//Console.WriteLine("Comm Relay Disable ACK " + readerEvent.relay);

					//lock (commLock)
					//{
						if (DisableRelayAckEventHandler != null)
							DisableRelayAckEventHandler(readerEvent.relay, readerEvent.reader);
					//}

                        Console.WriteLine("Communication  CLOSE RELAY - Rdr:" + readerEvent.reader.ToString() + "  Relay:" + readerEvent.relay.ToString() + " CloseTime:" + DateTime.Now.ToString());

                    lock (relSyncObj)
                    {
                        if (activateCloseOutput)
                            SetStopCloseRelay(readerEvent.reader, readerEvent.relay);
                    }

                    //this part will replace the above code in the future
                    //if (CloseOutputRelayEventHandler != null)
                        //CloseOutputRelayEventHandler(readerEvent.reader, readerEvent.relay);

				break;

				case AW_API_NET.APIConsts.RF_GET_INPUT_STATUS:
				case AW_API_NET.APIConsts.RF_GET_INPUT_STATUS_ALL:

					if (readerEvent.data[0] == AW_API_NET.APIConsts.NORMAL_CLOSED) 
						input1 = AW_API_NET.APIConsts.NORMAL_CLOSED;
					else if (readerEvent.data[0] == AW_API_NET.APIConsts.NORMAL_OPEN) 
						input1 = AW_API_NET.APIConsts.NORMAL_OPEN;
					else if (readerEvent.data[0] == AW_API_NET.APIConsts.FAULTY_CLOSED) 
						input1 = AW_API_NET.APIConsts.FAULTY_CLOSED;
					else if (readerEvent.data[0] == AW_API_NET.APIConsts.FAULTY_OPEN) 
						input1 = AW_API_NET.APIConsts.FAULTY_OPEN;

					if (readerEvent.data[1] == AW_API_NET.APIConsts.NORMAL_CLOSED) 
						input2 = AW_API_NET.APIConsts.NORMAL_CLOSED;
					else if (readerEvent.data[1] == AW_API_NET.APIConsts.NORMAL_OPEN) 
						input2 = AW_API_NET.APIConsts.NORMAL_OPEN;
					else if (readerEvent.data[1] == AW_API_NET.APIConsts.FAULTY_CLOSED) 
						input2 = AW_API_NET.APIConsts.FAULTY_CLOSED;
					else if (readerEvent.data[1] == AW_API_NET.APIConsts.FAULTY_OPEN) 
						input2 = AW_API_NET.APIConsts.FAULTY_OPEN;
			   
					if (InputChangeEventHandler != null)
						InputChangeEventHandler(readerEvent.host, readerEvent.reader, readerEvent.fGenerator, input1, input2);
				break;
					

			}//switch

			return (0);
		}
		#endregion

        #region SetStartCloseRelay(reader, relay, dur)
        private void SetStartCloseRelay(ushort reader, ushort relay, ushort dur)
        {

           lock (relSyncObj)
           {
                relayStruct rel = new relayStruct();

                for (int i = 0; i < relayArrayList.Count; i++)
                {
                    rel = (relayStruct)relayArrayList[i];
                    if ((rel.actionRdrID == reader) && (rel.actionRelay == relay))
                    {
                        /*if (!rel.start)
                        {
                            rel.start = true;
                            rel.timeStamp = DateTime.Now;
                            relayArrayList.RemoveAt(i);
                            relayArrayList.Add(rel);
                        }
                        else*/
                        //{
                            //t = relay.timeStamp;
                            //t = t.AddSeconds(relay.duration); //sec
                            //if (DateTime.Now >= t)

                            TimeSpan ts = rel.timeStamp.AddSeconds(Convert.ToInt64(rel.duration)) - DateTime.Now;
                            if (ts.Seconds < dur)
                            {
                               rel.timeStamp = DateTime.Now;
                               rel.duration = dur;
                               relayArrayList.RemoveAt(i);
                               relayArrayList.Add(rel); 
                            }
                        //}

                        //return;
                    }
                }
           }
        }
        #endregion

        #region SetStartCloseRelay(reader, relay)
        private void SetStartCloseRelay(ushort reader, ushort relay)
        {

            lock (relSyncObj)
            {
                relayStruct rel = new relayStruct();

                for (int i = 0; i < relayArrayList.Count; i++)
                {
                    rel = (relayStruct)relayArrayList[i];
                    if ((rel.actionRdrID == reader) && (rel.actionRelay == relay))
                    {
                        if (!rel.start)
                        {
                            rel.start = true;
                            rel.timeStamp = DateTime.Now;
                            relayArrayList.RemoveAt(i);
                            relayArrayList.Add(rel);
                        }
                        
                        return;
                    }
                }
            }
        }
        #endregion

        #region SetStopCloseRelay(reader, relay)
        private void SetStopCloseRelay(ushort reader, ushort relay)
        {
            lock (relSyncObj)
            {
                relayStruct rel = new relayStruct();
                for (int i = 0; i < relayArrayList.Count; i++)
                {
                    rel = (relayStruct)relayArrayList[i];
                    if ((rel.actionRdrID == reader) && (rel.actionRelay == relay))
                    {
                        if (rel.start)
                        {
                            rel.start = false;
                            relayArrayList.RemoveAt(i);
                            relayArrayList.Add(rel);
                        }

                        return;
                    }

                }
            }
        }
        #endregion

		#region OpenSerialPort
		public int OpenSerialPort(uint baud, uint port)
		{
			//lock (commLock)
			{
				int ret = 0;
				if ((ret = api.rfOpen(baud, port)) >= 0)
				{
					baudrate = baud;
					commPort = port;
					rs232Comm = true;
				}
				else
					rs232Comm = false;
				return(ret);
			}
		}
		#endregion

		#region CloseSerialPort
		public int CloseSerialPort()
		{
			//lock (commLock)
			{
				int ret = 0;
				if ((ret = api.rfClose()) >= 0)
					rs232Comm = false;
				return(ret);
			}
		}
		#endregion

		#region ScanNetwork
		public int ScanNetwork(byte[] ip)
		{
			//lock (commLock)
			{
				int ret = 0;

				if (pktID >= 223)
					pktID  = 1;
				else
					pktID += 1;

				if (ip == null)
				{
					//byte[] ip = {0x0};
					//int ret = api.rfCloseSocket(ip, AW_API_NET.APIConsts.ALL_IPS);
					IPCollection.Clear();
					ret = api.rfScanNetwork(pktID);
				}
				else
				{
					ret = api.rfScanIP(ip, pktID);
				}
				return (ret);
			}//lock
		}
		#endregion

		#region IsSocketConnected
		public bool IsSocketConnected(string ip)
		{
			Socket sock = null;
			bool ret = true;
			byte[] msg = Encoding.ASCII.GetBytes("hello");
           
			try
			{ 
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				//create server endpoint
				IPEndPoint serverEndPoint = new IPEndPoint(Dns.GetHostEntry(ip).AddressList[0], 7);
				
				sock.Connect(serverEndPoint);

				sock.Send(msg, 0, msg.Length, SocketFlags.None);

				int totBytes = 0;
				int bytesRcvd = 0;

				while (totBytes < msg.Length)
				{
					if ((bytesRcvd = sock.Receive(msg, totBytes, msg.Length - totBytes, SocketFlags.None)) == 0)
					{
						ret = false;
						break;
					}
					totBytes += bytesRcvd;
				}
			}
			catch //(Exception e)
			{
                ret = false;
			}
		    finally
	        {
                sock.Close();
	        }
           
			return ret;
	    }
		#endregion

		#region SocketConnection()
		public int SocketConnection()
	    {
			//lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
				    pktID += 1;

				byte[] ip = {0x0};
				int ret = api.rfOpenSocket(ip, 1, false, AW_API_NET.APIConsts.ALL_IPS, pktID);
				return (ret);
			}
	    }
		#endregion

		#region SocketConnection(ip)
		public int SocketConnection(byte[] ip)
		{
			//lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
				   pktID += 1;

				//byte[] ip = {0x0};
				int ret = api.rfOpenSocket(ip, 1, false, AW_API_NET.APIConsts.SPECIFIC_IP, pktID);
				return (ret);
			}
		}
		#endregion

		#region SocketDisconnection(ip)
		public int SocketDisconnection(byte[] ip)
	    {
			//lock (commLock)
			{
				int ret = 0;
		   
				if (ip == null)
					ret = api.rfCloseSocket(ip, AW_API_NET.APIConsts.ALL_IPS);
				else
					ret = api.rfCloseSocket(ip, AW_API_NET.APIConsts.SPECIFIC_IP);
				//return (api.rfCloseSocket(null, AW_API_NET.APIConsts.ALL_IPS));
                //NetRdrDisconnection.SetRdrPolling(ipStr, false);
				return (ret);
			}
	    }
		#endregion

		#region ResetAllReaders
		public int ResetAllReaders()
	    {
			//lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
				   pktID += 1;

				return (api.rfResetReader(1, 0, 0, AW_API_NET.APIConsts.ALL_READERS, pktID));
			}
	    }
		#endregion

		#region ResetReader
		public int ResetReader(ushort rdr, ushort host)
		{
			//lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
				   pktID += 1;


				if (rdr == 0)
					return (api.rfResetReader(host, rdr, 0, AW_API_NET.APIConsts.ALL_READERS, pktID));
				else
					return (api.rfResetReader(host, rdr, 0, AW_API_NET.APIConsts.SPECIFIC_READER, pktID));
			}
		}
		#endregion

		#region ResetRS232Reader
		public int ResetRS232Reader(ushort rdr, ushort host)
		{
			//lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
					pktID += 1;


				if (rdr == 0)
					return (api.rfResetReader(host, rdr, 0, 559, pktID));  //559 = ALL_RS232_READERS
				else
					return (api.rfResetReader(host, rdr, 0, AW_API_NET.APIConsts.SPECIFIC_READER, pktID));
			}
		}
		#endregion

		#region EnableReader
		public int EnableReader(ushort rdr, ushort host)
		{
			//lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
					pktID += 1;

				return (api.rfEnableReader(host, rdr, 0, true, AW_API_NET.APIConsts.SPECIFIC_READER, pktID));
			}
		}
		#endregion

		#region ResetReaderSocket
		public int ResetReaderSocket(ushort host, byte[] ip)
		{
			//lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
				   pktID += 1;

				return (api.rfResetReaderSocket(host, ip, pktID));
			}
		}
		#endregion

		#region RemoveCallback
		public static void RemoveCallback()
		{
			callbackCollection.RemoveFrom(0);
		}
		#endregion

		#region EnableTag (ushort rdr, uint tag, string type, bool led, bool spk, ushort pID)
		public int EnableTag(ushort rdr, uint tag, string type, bool led, bool spk, ushort pID)
		{
		   //if (pktID > 224)
			  //pktID  = 1;
			//lock (commLock)
			{
				AW_API_NET.rfTagSelect_t  tagSelect = new rfTagSelect_t();
				tagSelect.tagList = new uint[50];
				tagSelect.numTags = 1;
				tagSelect.tagList[0] = tag;
				if (type == "AST")
					tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
				else if (type == "ACC")
					tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
				else if (type == "INV")
					tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
				else
					return (-1);
				tagSelect.speakerOn = spk;
				tagSelect.ledOn = led;
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				int ret = api.rfEnableTags(1, rdr, 0, tagSelect, true, true, false, AW_API_NET.APIConsts.SPECIFIC_READER, pID);
				return (ret);
			}
		}
		#endregion

		#region EnableTag (ushort rdr, uint tag, string type, bool led, bool spk)
		public int EnableTag(ushort rdr, uint tag, string type, bool enable, bool led, bool spk)
		{
			//lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
				   pktID += 1;

				AW_API_NET.rfTagSelect_t  tagSelect = new rfTagSelect_t();
				tagSelect.tagList = new uint[50];
				tagSelect.numTags = 1;
				tagSelect.tagList[0] = tag;
				if (type == "AST")
					tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
				else if (type == "ACC")
					tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
				else if (type == "INV")
					tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
				else
					return (-1);
				tagSelect.speakerOn = spk;
				tagSelect.ledOn = led;
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				int ret = api.rfEnableTags(1, rdr, 0, tagSelect, enable, true, false, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);
				return (ret);
			}
		}
		#endregion

		#region EnableTag (ushort rdr, uint tag, string type, bool enable)
		public int EnableTag(ushort rdr, uint tag, string type, bool enable)
		{
			//lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
				   pktID += 1;

				AW_API_NET.rfTagSelect_t  tagSelect = new rfTagSelect_t();
				tagSelect.tagList = new uint[50];
				tagSelect.numTags = 1;
				tagSelect.tagList[0] = tag;
				if (type == "AST")
					tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
				else if (type == "ACC")
					tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
				else if (type == "INV")
					tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
				else
					return (-1);
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				int ret = api.rfEnableTags(1, rdr, 0, tagSelect, enable, true, false, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);
				return (ret);
			}
		}
		#endregion

        #region QueryTag (ushort rdr, uint tag, string type)
        public int QueryTag(ushort rdr, uint tag, string type)
		{
			//lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
				   pktID += 1;

				AW_API_NET.rfTagSelect_t  tagSelect = new rfTagSelect_t();
				tagSelect.tagList = new uint[50];
				tagSelect.numTags = 1;
				tagSelect.tagList[0] = tag;
				if (type == "AST")
					tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
				else if (type == "ACC")
					tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
				else if (type == "INV")
					tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
				else
					return (-1);
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				int ret = api.rfQueryTags(1, rdr, 0, ref tagSelect, true, true, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);
				return (ret);
			}
		}
		#endregion

		#region QueryTag (ushort rdr, string type)
		public int QueryTag(ushort rdr, string type)
		{
			//lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
				   pktID += 1;

				AW_API_NET.rfTagSelect_t  tagSelect = new rfTagSelect_t();
				tagSelect.tagList = new uint[50];
				//tagSelect.numTags = 1;
				//tagSelect.tagList[0] = tag;
				if (type == "AST")
					tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
				else if (type == "ACC")
					tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
				else if (type == "INV")
					tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
				else
					return (-1);
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE;
				int ret = api.rfQueryTags(1, rdr, 0, ref tagSelect, true, true, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);
				return (ret);
			}
		}
		#endregion

        #region QueryTag (ushort rdr, uint tag, string type, bool led, bool speaker)
        public int QueryTag(ushort rdr, uint tag, string type, bool led, bool speaker)
        {
            //lock (commLock)
            {
                if (pktID >= 223)
                    pktID = 1;
                else
                    pktID += 1;

                AW_API_NET.rfTagSelect_t tagSelect = new rfTagSelect_t();
                tagSelect.tagList = new uint[50];
                tagSelect.numTags = 1;
                tagSelect.tagList[0] = tag;
                tagSelect.ledOn = led;
                tagSelect.speakerOn = speaker;
                if (type == "AST")
                    tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
                else if (type == "ACC")
                    tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
                else if (type == "INV")
                    tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
                else
                    return (-1);
                tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
                int ret = api.rfQueryTags(1, rdr, 0, ref tagSelect, true, true, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);
                return (ret);
            }
        }
        #endregion

        #region CallTag (ushort rdr, ushort fgen, uint tag, string tagType, string cmdType)
        public int CallTag(ushort rdr, ushort fgen, uint tag, string tagType, string cmdType)
		{
			//lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
				   pktID += 1;

				AW_API_NET.rfTagSelect_t  tagSelect = new rfTagSelect_t();
				tagSelect.tagList = new uint[50];
			
				if (tagType == "AST")
					tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
				else if (tagType == "ACC")
					tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
				else if (tagType == "INV")
					tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
				else
					return (-1);

				if (cmdType == "Specific")
				{
					tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
					tagSelect.numTags = 1;
					tagSelect.tagList[0] = tag;
				}
				else if (cmdType == "Type")
				{
					tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE;
					tagSelect.numTags = 0;
				}
				else if (cmdType == "All")
				{
					tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_FIELD;
					tagSelect.numTags = 0;
				}
				else
					return (-1);

				int ret = api.rfCallTags(1, rdr, 0, fgen, ref tagSelect, true, true, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);
				return (ret);
			}
		}
		#endregion

        #region CallTag (ushort rdr, ushort fgen, uint tag, string tagType, string cmdType, bool led, bool speaker)
        public int CallTag(ushort rdr, ushort fgen, uint tag, string tagType, string cmdType, bool led, bool speaker)
        {
            //lock (commLock)
            {
                if (pktID >= 223)
                    pktID = 1;
                else
                    pktID += 1;

                AW_API_NET.rfTagSelect_t tagSelect = new rfTagSelect_t();
                tagSelect.tagList = new uint[50];

                tagSelect.ledOn = led;
                tagSelect.speakerOn = speaker;

                if (tagType == "AST")
                    tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
                else if (tagType == "ACC")
                    tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
                else if (tagType == "INV")
                    tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
                else
                    return (-1);

                if (cmdType == "Specific")
                {
                    tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
                    tagSelect.numTags = 1;
                    tagSelect.tagList[0] = tag;
                }
                else if (cmdType == "Type")
                {
                    tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE;
                    tagSelect.numTags = 0;
                }
                else if (cmdType == "All")
                {
                    tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_FIELD;
                    tagSelect.numTags = 0;
                }
                else
                    return (-1);

                int ret = api.rfCallTags(1, rdr, 0, fgen, ref tagSelect, true, true, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);
                return (ret);
            }
        }
        #endregion

		#region EnableOutput
		public int EnableOutput(ushort relay, ushort rdr, bool firstCall)
	    {
			lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
				  pktID += 1;

				if (firstCall)
				{
					cmdStruct cmd = new cmdStruct();
					cmd.pktID = pktID;
					cmd.rdr = rdr;
					cmd.retry = 0;
					cmd.host = 1;
					cmd.cmd = ENABLE_RELAY;
					cmd.cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;
					cmd.relay = relay;
					cmd.timeStamp = DateTime.Now;
					int index = rdrCollection.IndexOf(rdr);
					if (index >= 0)
					{
						rdrCollection.AddCmd(index, cmd); 
					}
				}

				int ret = api.rfEnableRelay(1, rdr, 0, relay, true, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);
				return (ret);
			}
	    }
		#endregion

		#region DisableOutput
		public int DisableOutput(ushort relay, ushort rdr, bool firstCall)
	    {
			lock (commLock)
			{
				if (pktID >= 223)
					pktID  = 1;
				else
				   pktID += 1;

				if (firstCall)
				{
					cmdStruct cmd = new cmdStruct();
					cmd.pktID = pktID;
					cmd.rdr = rdr;
					cmd.retry = 0;
					cmd.host = 1;
					cmd.cmd = DISABLE_RELAY;
					cmd.cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;
					cmd.relay = relay;
					cmd.timeStamp = DateTime.Now;
					int index = rdrCollection.IndexOf(rdr);
					if (index >= 0)
					{
						rdrCollection.AddCmd(index, cmd); 
					}
				}

				//Console.WriteLine("Disable relay cmd sent " + relay + "firstcall = " + firstCall);
				int ret = api.rfEnableRelay(1, rdr, 0, relay, false, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);
				return (ret);
			}//lock
	    }
		#endregion

        #region ClearOutputRelays()
        public void ClearOutputRelays()
        {
            relayArrayList.Clear();
        }
        #endregion

        #region AddToRelayArrayList(relay)
        private void AddToRelayArrayList(relayStruct rel)
        {
            relayStruct rely = new relayStruct();
            for (int i = 0; i < relayArrayList.Count; i++)
            {
                rely = (relayStruct)relayArrayList[i];
                if ((rely.eventRdrID == rel.eventRdrID) &&  
                    (rely.actionRdrID == rel.actionRdrID) && (rely.actionRelay == rel.actionRelay))
                {
                    //rely = rel; //do not do anything
                    if (rely.duration == rel.duration)
                        return;
                    else
                    {
                        rely.duration = rel.duration;
                        return;
                    }
                }
            }

            lock (relSyncObj)
            {
                relayArrayList.Add(rel);
            }
        }
        #endregion

        #region ConfigOutputRelay (rdrID, rel, time)
        public void ConfigOutputRelay(ushort eRdrID, ushort aRdrID, ushort aRel, ushort dur)
        {
            bool autoMode = true;  //for future implementation
            relayStruct relay = new relayStruct(eRdrID, aRdrID, aRel, autoMode, dur);
            AddToRelayArrayList(relay);
            //relayArrayList.Add(relay);
            if (autoMode & !activateCloseOutput)
               activateCloseOutput = true;
           
        }
        #endregion

        #region EnableOutputRelay (ushort rdr, ushort relay)
        public int EnableOutputRelay(ushort rdr, ushort relayNum)
        {
            if (pktID >= 223)
                pktID = 1;
            else
                pktID += 1;

            int ret = api.rfEnableRelay(1, rdr, 0, relayNum, true, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);

            return (ret);
        }
        #endregion

        #region EnableOutputRelay (ushort rdr, ushort relay, dur)
        public int EnableOutputRelay(ushort rdr, ushort relayNum, ushort dur)
        {
            if (pktID >= 223)
                pktID = 1;
            else
                pktID += 1;

            int ret = 0;
            if (IsRelayEnabled(rdr, relayNum))
            {
                SetStartCloseRelay(rdr, relayNum, dur); //renew timestamp
                Console.WriteLine("COMMUNICATION - OPEN RELAY ***RENEW*** TIMESTAMP = " + Convert.ToString(rdr) + "  RELAY = " + Convert.ToString(relayNum) + " Time: " + DateTime.Now);
            }
            else
            {
                ret = api.rfEnableRelay(1, rdr, 0, relayNum, true, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);
                //Console.WriteLine("CMMUNICATION - OPEN RELAY COMMAND Reader = " + Convert.ToString(rdr) + "  RELAY = " + Convert.ToString(relayNum) + " Time: " + DateTime.Now);
            }

            return (ret);
        }
        #endregion

        #region IsRelayEnabled (rdr, relay)
        private bool IsRelayEnabled(ushort rdr, ushort relay)
        {
            relayStruct rel = new relayStruct();
            for (int i = 0; i < relayArrayList.Count; i++)
            {
                rel = (relayStruct)relayArrayList[i];
                if ((rel.actionRdrID == rdr) && (rel.actionRelay == relay))
                {
                    if (rel.start)
                        return (true);
                }
            }

            return (false);
        }
        #endregion

        #region DisableOutputRelay (ushort rdr, ushort relay)
        public int DisableOutputRelay(ushort rdr, ushort relay)
        {
            if (pktID >= 223)
			   pktID  = 1;
			else
			   pktID += 1;

           int ret = api.rfEnableRelay(1, rdr, 0, relay, false, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);
           return (ret);
        }
        #endregion
        
		#region GetAssetTag
		public bool GetAssetTag(ushort reader, ushort fgen, out uint tagID)
		{
		   for (int i=0; i<=tagCollection.Count; i++)
		   {
			   if ((tagCollection[i].type == AW_API_NET.APIConsts.ASSET_TAG) && (tagCollection[i].reader == reader)) //&&
				   //(tagCollection[i].fGen == fgen))
			   {
				   tagID = tagCollection[i].id;
				   return (true);
			   }
		   }

		   tagID = 0;
		   return (false);
		}
		#endregion

		#region ConfigInputPort(ushort input1Cfg, ushort input2Cfg)
		public int ConfigInputPort(ushort rdr, ushort host, ushort input1Cfg, ushort input2Cfg, bool supervised)
		{
			if (pktID >= 223)
				pktID  = 1;
			else
				pktID += 1;

			int ret = api.rfConfigInputPort(host, rdr, 0, input1Cfg, input2Cfg, supervised, AW_API_NET.APIConsts.SPECIFIC_READER, pktID);
            return (ret);
		}
		#endregion
	}
}

#region Not Used
/*#region ScanNetwork
		public int ScanNetwork(byte[] ip)
		{
			int ret = 0;

			if (pktID > 224)
				pktID  = 1;

			if (ip == null)
			{
				//byte[] ip = {0x0};
				//int ret = api.rfCloseSocket(ip, AW_API_NET.APIConsts.ALL_IPS);
				IPCollection.Clear();
				ret = api.rfScanNetwork(pktID++);
			}
			else
			{
				ret = api.rfScanIP(ip, pktID++);
			}
			return (ret);
		}
#endregion
		*/
#endregion
