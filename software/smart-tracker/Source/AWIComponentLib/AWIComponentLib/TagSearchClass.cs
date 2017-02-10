using System;
using System.Collections;
using System.Threading;
using System.Timers;
using System.Runtime.InteropServices;
using AW_API_NET;
using AWIComponentLib.Communication;
using AWIComponentLib.Utility;
using System.Windows.Forms;

namespace AWIComponentLib.Tag
{
	#region delegates
    public delegate void SearchTagEvent(ushort rdr, ushort host, ushort fgen, uint tag);
	#endregion

	#region TagSrchStruct
	[StructLayout(LayoutKind.Sequential)]
	public struct tagSrchStruct
	{
		public ushort reader;         
		public ushort host;
		public ushort fGen;
		public short rssi;

		tagSrchStruct(int a)
		{
           reader = 0;
	       host = 0;
		   fGen = 0;
		   rssi = 0;
		}
	}
	#endregion

    public class TagSearchClass
	{
		#region events
		public static SearchTagEvent SearchTagEventHandler;
		#endregion

		#region vars
        private ArrayList rdrStatusList = new ArrayList();
		private UtilityClass utility = new UtilityClass();
		private ArrayList tagSrchList = new ArrayList();
		private Object myLock = new Object();
		private System.Timers.Timer searchReaderTimer;
		private CommunicationClass communication;
		private int srchIndex;
		private bool startSearch;
		private uint srchTagID;
		private string srchTagType;
		private ushort counter;
		private static ushort MAX_TRY = 3;
		#endregion

		#region Constructor
		public TagSearchClass(CommunicationClass comm)
		{
			#region events
			communication = comm;
			CommunicationClass.PowerupEventHandler += new PowerupEvent(this.PowerupReaderEventNotifty);
			CommunicationClass.QueryTagEventHandler += new QueryTagAckEvent(this.QueryTagEventNotifty);
			
			srchIndex = -1;
			startSearch = false;
			srchTagID = 0;
			counter = 0;

			searchReaderTimer = new System.Timers.Timer();
			searchReaderTimer.Interval = 3000;
			searchReaderTimer.AutoReset = true;
			searchReaderTimer.Enabled = true;
			searchReaderTimer.Elapsed += new ElapsedEventHandler(OnSearchReaderTimer);
			#endregion
		}
		#endregion

		#region SearchTag(uint tagID, string type)
		public void SearchTag(uint tagID, string type)
		{
			counter = 0;
			srchIndex = 0;
			srchTagID = tagID;
            srchTagType = type;
            startSearch = true;
		}
		#endregion

		#region ProcessSrchList
		private void ProcessSrchList()
		{
			ushort rdr = 0;
			ushort host = 0;
			ushort fgen = 0;
			short rssi = 0;

			foreach (tagSrchStruct tagListObj in tagSrchList)
			{
				if (tagListObj.rssi >= rssi)
				{
					rdr = tagListObj.reader;
					host = tagListObj.host;
					fgen = tagListObj.fGen;
				}
			}
 
            if (SearchTagEventHandler != null)
				SearchTagEventHandler(rdr, host, fgen, srchTagID);
		}
		#endregion

		#region OnSearchReaderTimer
		private void OnSearchReaderTimer(object source, ElapsedEventArgs e)
		{
			lock(myLock)
			{
				if (startSearch) 
				{
					if (srchIndex >= rdrStatusList.Count)
					{             
						if (counter >= MAX_TRY)
						{
                           startSearch = false;
						   srchIndex = 0;
						   ProcessSrchList();
						   return;
						}

						srchIndex = 0;
						counter += 1;
					}

					readerStatStruct rdrStat = new readerStatStruct(0); //create an readerStatStruct object with temp rdr id
					
					for (int n=srchIndex; n<rdrStatusList.Count; n++)
					{
						rdrStat = (readerStatStruct)rdrStatusList[n];

						if (rdrStat.GetSearchReader())  
						{
							if (rdrStat.GetCounter() < MAX_TRY) 
							{
								if (!rdrStat.GetSrchStatus()) //tagFound
								{
									Console.WriteLine ("OnSearchTimer(TagSearchClass) send Query Cmd time:" + DateTime.Now.ToString());
									int ret = communication.QueryTag(rdrStat.rdrID, srchTagID, "AST");
									return;
								}
							}
						}

						srchIndex += 1;
					}

				}//if (startSearch) 
			}//lock
		}
		#endregion

		#region QueryTagEventNotifty
		void QueryTagEventNotifty(AW_API_NET.rfTagEvent_t tagEvent)
		{
			lock(myLock)
			{
				if (tagEvent.tag.id == srchTagID)
				{
					tagSrchStruct tagSrchObj = new tagSrchStruct();
                    tagSrchObj.host = tagEvent.host;
                    tagSrchObj.reader = tagEvent.reader;
					tagSrchObj.fGen = tagEvent.fGenerator;
                    tagSrchObj.rssi = tagEvent.RSSI;
					tagSrchList.Add(tagSrchObj);

					//if rdr exits in the rdrStatusList remove it and replace it otherwise add it.
					readerStatStruct readerObj = new readerStatStruct(tagEvent.reader);
					if (utility.GetRdrFromList (tagEvent.reader, ref readerObj, rdrStatusList))
					{
						//make the timer to start processing 
						startSearch = false;
						srchIndex = 0;        
						counter = 0;
						
						readerStatStruct rdrStat = new readerStatStruct(tagEvent.reader);
						readerObj.Copy(ref rdrStat);
						rdrStatusList.Remove(readerObj);
                        rdrStat.SetSrchStatus(true);
						rdrStatusList.Add(rdrStat); 
 
						Console.WriteLine ("QueryTagNotify(TagSearchClass) ProcessSrchList time:" + DateTime.Now.ToString());
						ProcessSrchList();
					}
				}//if (tagEvent.tag.id == srchTagID)
			}//lock
		}
        #endregion

		#region PowerupReaderEventNotifty
		void PowerupReaderEventNotifty(AW_API_NET.rfReaderEvent_t readerEvent)
		{
			//populating rdrStat for polling rdr module
			//check if reader is on network
			lock(myLock)
			{
				string ip;
				if ((ip=utility.GetStringIP(readerEvent.ip)) != "")
				{	
					//if ip exits in the rdrStatusList remove it and replace it otherwise add it.
					readerStatStruct readerObj = new readerStatStruct(readerEvent.reader);
					if (utility.GetRdrFromList (ip, ref readerObj, rdrStatusList))
						rdrStatusList.Remove(readerObj);
				
					readerStatStruct rdrStat = new readerStatStruct(readerEvent.reader);
					rdrStat.hostID = readerEvent.host;
					rdrStat.SetIP(utility.GetStringIP(readerEvent.ip));
					rdrStat.SetStatus(1); //online
					rdrStat.SetProcessing(false);
					
					rdrStatusList.Add(rdrStat);  
				}
			}
		}
		#endregion

		#region SetSearchReader(ushort rdr)
		//application will set the reader to be a search rdr by looking at zone table, rdr type
		public void SetSearchReader(ushort rdr)
		{
			readerStatStruct readerObj = new readerStatStruct(rdr);
			if (utility.GetRdrFromList (rdr, ref readerObj, rdrStatusList))
			{
               readerStatStruct readerObj2 = new readerStatStruct(rdr);
               readerObj.Copy(ref readerObj);
               rdrStatusList.Remove(readerObj);
               readerObj2.SetSearchReader(true);
               rdrStatusList.Add(readerObj2);
			}
		}
		#endregion
	}
}
