using System;
using AW_API_NET;

//This is the interface for AWIComponentLib
namespace AWIComponentLib
{
	public interface IAPIEventHandler
	{
		void TagDetected(AW_API_NET.rfTagEvent_t tagEvent);
		void TagEnabled(AW_API_NET.rfTagEvent_t tagEvent);
		void TagDisabled(AW_API_NET.rfTagEvent_t tagEvent);
		void TagQueried(AW_API_NET.rfTagEvent_t tagEvent);
	}
}
