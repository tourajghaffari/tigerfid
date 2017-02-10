using System;

namespace AWIComponentLib.Events
{
	//These are the actions to be formed when a specific event takes place.
	#region EventAction Delegates
	public delegate void EnableOutputRelay(ushort n);
	public delegate void DisableOutputRelay(ushort n);
	#endregion

	public interface IEventActionsInterface
	{
		event EnableOutputRelay  EnableOutputRelayEvent;
		event DisableOutputRelay DisableOutputRelayEvent;

		void EnableOutputRelayEventHandler(ushort n);
		void DisableOutputRelayEventHandler(ushort n);
	}

	public class EventActionClass : IEventActionsInterface
	{
		public EventActionClass() {}

		public event EnableOutputRelay  EnableOutputRelayEvent;
		public event DisableOutputRelay DisableOutputRelayEvent;

		public void EnableOutputRelayEventHandler(ushort n)
		{
		   if (EnableOutputRelayEvent != null)
			   EnableOutputRelayEvent(n);
		}

		public void DisableOutputRelayEventHandler(ushort n)
		{
		   if (DisableOutputRelayEvent != null)
			   DisableOutputRelayEvent(n);
		}
	}
}
