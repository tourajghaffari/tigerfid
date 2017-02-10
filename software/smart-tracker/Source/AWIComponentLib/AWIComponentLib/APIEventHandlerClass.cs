using System;
using AWIComponentLib.Communication; 

namespace AWIComponentLib.Events
{
	//public delegate void EnableRelayEvent(ushort n);
	public delegate void AppTagDetectedEvent(AW_API_NET.rfTagEvent_t tagEvent);
    public delegate void AppTagDetectedRSSIEvent(AW_API_NET.rfTagEvent_t tagEvent);
    public delegate void AppTagDetectedSaniEvent(AW_API_NET.rfTagEvent_t tagEvent);
    public delegate void AppTagTamperEvent(AW_API_NET.rfTagEvent_t tagEvent);
	public delegate void AppEnableRelayAckEvent(ushort relay, ushort reader);
	public delegate void AppDisableRelayAckEvent(ushort relay, ushort reader);

	public class APIEventHandler : IAPIEventHandler
	{
		//private CommunicationClass comm = null;
		public static event AppTagDetectedEvent AppTagDetectedEventHandler;	//if static only  one instance can be instantiate
		public static event AppTagDetectedRSSIEvent AppTagDetectedRSSIEventHandler;
        public static event AppTagDetectedSaniEvent AppTagDetectedSaniEventHandler;
        public static event AppEnableRelayAckEvent AppEnableRelayAckEventHandler;
		public static event AppDisableRelayAckEvent AppDisableRelayAckEventHandler;
		//public static event AppTagTamperEvent AppTagTamperEventHandler;


		public APIEventHandler()
		{
			//comm = new CommunicationClass();
		    CommunicationClass.TagDetectedEventHandler += new TagDetectedEvent(TagDetected);//+= new EnableOutputRelay(eventActions_EnableOutputRelayEvent);
			CommunicationClass.TagDetectedRSSIEventHandler += new TagDetectedRSSIEvent(TagDetectedRSSI);
            CommunicationClass.TagDetectedSaniEventHandler += new TagDetectedSaniEvent(TagDetectedSani);
            CommunicationClass.EnableRelayAckEventHandler += new EnableRelayAckEvent(EnableRelayAck);
			CommunicationClass.DisableRelayAckEventHandler += new DisableRelayAckEvent(DisableRelayAck);
            //CommunicationClass.AppTagTamperEventHandler += new AppTagTamperEvent(TagDetectedTamper);
		}

		//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		public void TagDetected(AW_API_NET.rfTagEvent_t tagEvent)
    	{
#if DEBUG
#if TRACE
		   Console.WriteLine("Tag detected event fired");
#endif
#endif
		   lock(CommunicationClass.commLock)
		   {
		      if (AppTagDetectedEventHandler != null)
				 AppTagDetectedEventHandler(tagEvent);
		   }
	    }

		public void TagDetectedRSSI(AW_API_NET.rfTagEvent_t tagEvent)
    	{
#if DEBUG
#if TRACE
		   Console.WriteLine("Tag detected with RSSI event fired");
#endif
#endif
		   lock(CommunicationClass.commLock)
		   {
		      if (AppTagDetectedRSSIEventHandler != null)
				 AppTagDetectedRSSIEventHandler(tagEvent);
		   }
	    }

        public void TagDetectedSani(AW_API_NET.rfTagEvent_t tagEvent)
        {
#if DEBUG
#if TRACE
            Console.WriteLine("Tag detected with Sani event fired");
#endif
#endif
            lock (CommunicationClass.commLock)
            {
                if (AppTagDetectedSaniEventHandler != null)
                    AppTagDetectedSaniEventHandler(tagEvent);
            }
        }

        public void EnableRelayAck(ushort relay, ushort reader)
		{
			//Console.WriteLine("@@EVEVT CLASS ENABLE RELAY ACK " + relay);
			lock(CommunicationClass.commLock)
		    {
			    if(AppEnableRelayAckEventHandler != null)
			        AppEnableRelayAckEventHandler(relay, reader);
			}
		}

		public void DisableRelayAck(ushort relay, ushort reader)
		{
			//Console.WriteLine("@@EVEVT CLASS DISABLE RELAY ACK " + relay);
			lock(CommunicationClass.commLock)
		    {
			    if(AppDisableRelayAckEventHandler != null)
			       AppDisableRelayAckEventHandler(relay, reader);
			}
		}

		public void TagEnabled(AW_API_NET.rfTagEvent_t tagEvent)
		{
#if DEBUG
#if TRACE
		    Console.WriteLine("Tag enabled event fired");
#endif
#endif
		}

		public void TagDisabled(AW_API_NET.rfTagEvent_t tagEvent)
		{
#if DEBUG
#if TRACE
		    Console.WriteLine("Tag enabled event fired");
#endif
#endif
		}

		public void TagQueried(AW_API_NET.rfTagEvent_t tagEvent)
		{
#if DEBUG
#if TRACE
		    Console.WriteLine("Tag queried event fired");
#endif
#endif
        }


		//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

		#region Events handler
		/*
		//This section will provide space to handle the event for invalid tag
		//==============================================================================
		private void Events_ValidTagHandler()
		{
		   Console.WriteLine("Valid tag event fired");

		   //need to add event-actions for this event for specific application
		   eventActions_EnableOutputRelayEvent(1);
		   eventActions_EnableOutputRelayEvent(2);
		}
		
		private void Events_InvalidTagHandler()
		{
		   Console.WriteLine("Invalid tag event fired");
		   
		   //need to add event-actions for this event for specific application
		   eventActions_DisableOutputRelayEvent(1);
		   eventActions_DisableOutputRelayEvent(2);
		   
		}
		//=============================================================================
		#endregion

		#region EventActions Handler
		//This is event actions that can be added to the events - need to be private to
		//enforce being call directly.(encapsulate)
		//=============================================================================
		private void eventActions_EnableOutputRelayEvent(ushort n)
		{
		   Console.WriteLine("Enable output relay1 received");

		   //call a delegate in communication to perform a user defined action
		   eventActions.EnableOutputRelayEventHandler(n);
		}
		
		private void eventActions_DisableOutputRelayEvent(ushort n)
		{
		    Console.WriteLine("Disable output relay1 received");
			eventActions.DisableOutputRelayEventHandler(n);
		}  */
		#endregion

		/*public IEventInterface GetEventsHandler()
		{
		   if (events != null)
			   return (events);
		   else
			   return null;
		}*/

		/*public IEventActionsInterface GetEventActionHandler()
		{
		   if (eventActions != null)
			   return (eventActions);
		   else
			   return null;
		}*/

		//for testing purpose
		//static public void main ()
		//{
		   //IEventInterface events = new EventClass();
		   //events.ValidTagEvent += new ValidTagDetected(Events_ValidTagHandler);
		   //events.ValidTagEventHandler();
		//}
	}
}
