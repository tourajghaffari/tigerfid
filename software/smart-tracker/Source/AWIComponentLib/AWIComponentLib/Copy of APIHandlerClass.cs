using System;
using AWIComponentLib.Communication; 

namespace AWIComponentLib.Events
{
	public delegate void EnableRelayEvent(ushort n);

	public class APIHandler	: I
	{
		private IEventInterface events = null;
		private CommunicationClass comm = null;
		private IEventActionsInterface eventActions = null;

		public static event EnableRelayEvent EnableRelayHandler;

		public APIHandler()
		{
			events = new EventClass();
		    events.ValidTagEvent += new ValidTagDetected(Events_ValidTagHandler);
			events.InvalidTagEvent += new InvalidTagDetected(Events_InvalidTagHandler);

			eventActions = new EventActionClass();
			eventActions.EnableOutputRelayEvent += new EnableOutputRelay(eventActions_EnableOutputRelayEvent);
			eventActions.DisableOutputRelayEvent += new DisableOutputRelay(eventActions_DisableOutputRelayEvent);
	
		}

		#region Events handler
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
		}
		#endregion

		public IEventInterface GetEventsHandler()
		{
		   if (events != null)
			   return (events);
		   else
			   return null;
		}

		public IEventActionsInterface GetEventActionHandler()
		{
		   if (eventActions != null)
			   return (eventActions);
		   else
			   return null;
		}

		//for testing purpose
		//static public void main ()
		//{
		   //IEventInterface events = new EventClass();
		   //events.ValidTagEvent += new ValidTagDetected(Events_ValidTagHandler);
		   //events.ValidTagEventHandler();
		//}
	}
}
