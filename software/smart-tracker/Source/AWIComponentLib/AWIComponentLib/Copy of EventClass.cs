using System;

namespace AWIComponentLib.Events
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	/*public struct TagStruct
    {
      string Id;                 
      string type;                 
    }*/

	#region event delegates
	public delegate void InvalidTagDetected();
	public delegate void ValidTagDetected();
	#endregion

	public interface IEventInterface
	{
	   event InvalidTagDetected InvalidTagEvent;
	   event ValidTagDetected ValidTagEvent;

	   void InvalidTagEventHandler();
	   void ValidTagEventHandler();
	}

	public class EventClass : IEventInterface
	{
		//These are the external events that when the occur an action need to be formed
		//on their behalf.
		public event InvalidTagDetected InvalidTagEvent;
		public event ValidTagDetected ValidTagEvent;
		
		public EventClass(){}
		
		public void InvalidTagEventHandler()
		{
		   if (InvalidTagEvent != null)
			   InvalidTagEvent();
		}

		public void ValidTagEventHandler()
		{
		   if (ValidTagEvent != null)
			   ValidTagEvent();
		}
	}
}
