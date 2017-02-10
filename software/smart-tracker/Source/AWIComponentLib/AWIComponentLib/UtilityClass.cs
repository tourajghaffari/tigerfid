using System;
using System.Collections;
using AW_API_NET;
using AWIComponentLib.Communication;

namespace AWIComponentLib.Utility
{
	public class UtilityClass
	{
		#region vars

		#endregion

		#region Constructor
		public UtilityClass()
		{
			
		}
		#endregion

		#region Reader

		#region GetRdrFromList (string ip, ref readerStatStruct rdrStatObj, ArrayList rdrList)
		//Gets an reader object from readers on network with matching ip address
		public bool GetRdrFromList (string ip, ref readerStatStruct rdrStatObj, ArrayList rdrList)
		{
			foreach (readerStatStruct rdrObj in rdrList)
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

		#region GetRdrFromList (ushort rdrID, ref readerStatStruct rdrStatObj, ArrayList rdrList)
		//Gets an reader object from readers on network with matching reader address
		public bool GetRdrFromList (ushort rdrID, ref readerStatStruct rdrStatObj, ArrayList rdrList)
		{
			foreach (readerStatStruct rdrObj in rdrList)
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

		#region GetStringIP
		public string GetStringIP (byte[] ip)
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

		#endregion
	}
}
