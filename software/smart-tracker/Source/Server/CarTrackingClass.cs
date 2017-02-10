using System;
//using MySql.Data.MySqlClient;
using System.Data.Odbc;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Runtime.InteropServices;
using AWIComponentLib.Database;
using System.Drawing;
using AWI.SmartTracker;


namespace CarTracker
{
	/// <summary>
	/// Summary description for CarTrackingClass.
	/// </summary>
	
	[StructLayout(LayoutKind.Sequential)]
	public struct itemInfoSturct
	{
	   public uint tagID;
	   public string type;
	   public string parkingNum;
	   public string location;
	   public string status;
	   public string alarmID;
	   public string eventDescript;
	   public string time;
	   public ushort rdrID;
	   public ushort fgenID;
	}

	public class CarTrackingClass
	{
		public delegate void StatusParkingSlotHandler(itemInfoSturct item);
		public event StatusParkingSlotHandler StatusParkingSlotEvent;

		private OdbcConnection m_connection = null;
		private OdbcDbClass odbcDB = new OdbcDbClass();
		//private MainForm mForm = new MainForm(0);

		public CarTrackingClass() {}
		
		public CarTrackingClass(MainForm form)
		{
			//mForm = form;

			//changes made to support MYSQL Server v5.0 and later
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

			OdbcDbClass.NotifyDBConnectionStatusHandler += new NotifyDBConnectionStatus(DBConnectionStatusHandler);
			
			if (MainForm.m_connection == null)
			{
				if (MainForm.conStr == "")
					return;  //no db connection

				if (MainForm.providerName == dbProvider.SQL)
				{
					if (!odbcDB.Connect(MainForm.conStr))  //SQL
					//if (!odbcDB.Connect("Driver={SQL Native Client};Server=Seyed02;Database=bank;Trusted_Connection=yes;Pooling=False;"))  //SQL
					{						
						return;
					}	
				}
				else if (MainForm.providerName == dbProvider.MySQL)
				{
					if (!odbcDB.Connect("DRIVER={MySQL ODBC 3.51 Driver};SERVER=localhost;DATABASE=parkingtracker;USER=root;PASSWORD=;OPTION=3;"))	//MYSQL
					{
						return;
					}
				}
				else
				{
					return;
				}
			}
			else
				m_connection = MainForm.m_connection;
		}
		
		#region ShowErrorMessage
		private void ShowErrorMessage(string msg)
		{
			MessageBox.Show(null, msg, "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		#endregion

		#region DBConnectionStatusHandler
		private void DBConnectionStatusHandler(status stat, OdbcConnection connect)
		{
			if (stat == status.open)
			{
				m_connection = connect;
			}
			else if (stat == status.broken)
			{
				m_connection = null;   
			}
			else if (stat == status.close)
			{
				m_connection = null;
			}
		}
		#endregion

		#region GetParkingStatus
		public void GetParkingStatus(uint tag, string type, ushort rdr, ushort fgen)
		{
			string zoneID, zoneID2, loc=null;
			itemInfoSturct itemInfo = new itemInfoSturct();
			itemInfo.tagID = tag;
			if (type == "ACC")
			   itemInfo.type = "Owner";
			else if (type == "AST")
			   itemInfo.type = "Car";
			else if (type == "INV")
			   itemInfo.type = "Line";

			if ((zoneID = GetZoneID(rdr, fgen, out loc)) == null)
			{
				itemInfo.parkingNum = "";
				itemInfo.location = "";
				itemInfo.alarmID = "";
				itemInfo.status = "";
				itemInfo.eventDescript = "Parking Space Not Defined";
				itemInfo.time = DateTime.Now.ToString();

				if (StatusParkingSlotEvent != null)
					  StatusParkingSlotEvent(itemInfo);
				return;
			}
			else if (zoneID == "error")
				return;

			if ((zoneID2 = GetZoneID(tag, type)) == null)
			{
				itemInfo.parkingNum = "";
				itemInfo.location = "";
				itemInfo.alarmID = "";
				itemInfo.status = "";
				itemInfo.eventDescript = "Owner Parking Space Not Defined";
				itemInfo.time = DateTime.Now.ToString();

				if (StatusParkingSlotEvent != null)
					  StatusParkingSlotEvent(itemInfo);
				return;
			}
			else if (zoneID2 == "error")
				return;

		    if (zoneID != zoneID2)
			{
				itemInfo.parkingNum = zoneID;
				itemInfo.location = loc;
				itemInfo.alarmID = "1";
				itemInfo.status = "Occupied";
				itemInfo.eventDescript = "Wrong Parking Space";
				itemInfo.rdrID = rdr;
				itemInfo.fgenID = fgen;
				itemInfo.time = DateTime.Now.ToString();

				if (StatusParkingSlotEvent != null)
					  StatusParkingSlotEvent(itemInfo);
			}
			else
			{
				itemInfo.parkingNum = zoneID;
				itemInfo.location = loc;
				itemInfo.alarmID = "";
				itemInfo.status = "Occupied";
				itemInfo.eventDescript = "Parking Space OK";
				itemInfo.time = DateTime.Now.ToString();

				if (StatusParkingSlotEvent != null)
					  StatusParkingSlotEvent(itemInfo);
			}
		}
		#endregion

		#region GetZoneID - zonetable
		string GetZoneID(ushort rdr, ushort fgen, out string loc)
		{
			if (MainForm.m_connection == null)
			{
				loc = "";
				return "error";
			}

			lock (MainForm.m_connection)  //dec-06-06
			{
				string str = null, str1 = null;
				StringBuilder mySelectQuery = new StringBuilder();
				mySelectQuery.Append("SELECT ID, Location FROM zones WHERE ReaderID = ");          
				mySelectQuery.AppendFormat("'{0}'", rdr);
				mySelectQuery.Append(" AND FieldGenID = ");
				mySelectQuery.AppendFormat("'{0}'", fgen);
			
				string mySelectStr = mySelectQuery.ToString();
				OdbcDataReader myReader = null;
				OdbcCommand myCommand = new OdbcCommand(mySelectStr, m_connection);
			
				/*try
				{
				   myReader = myCommand.ExecuteReader();
				}
				catch (Exception e)
				{
				   Console.WriteLine(e.ToString());
				   loc = null;
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
				   return ("error");
				}*/

				//lock (MainForm.m_connection)
				//{
				try
				{
					myReader = myCommand.ExecuteReader();
				}
				catch (Exception ex)
				{
					//ShowErrorMessage(ex.Message);
					/*if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return ("");*/

					int ret = 0;
					if ((ret=ex.Message.IndexOf("Lost connection")) >= 0)
					{  
						//error code 2013
						Thread.Sleep(500);

						if (!ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
						{
							MessageBox.Show("Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							if (myReader != null)
							{
								if (!myReader.IsClosed)
									myReader.Close();
							}
							loc = "";
							return "error";
						}                             
					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					loc = "";
					return "error";
				} //catch ..try
				//}//lock
 
				if (myReader.Read())
				{
					str = myReader.GetString(0);
					str1 = myReader.GetString(1);
				}
			
				myReader.Close();
				loc = str1;
				return (str);
			}//lock m_connection
		}
		#endregion
		
		#region ReconnectToDBServer
		public bool ReconnectToDBServer()
		{
			if (MainForm.conStr.Length > 0)
			{
				if (!odbcDB.Connect(MainForm.conStr))	//MYSQL
				{						
					return false;
				}
			}

			return true;
		}
        #endregion 

		#region GetZoneID - parkingtags
		string GetZoneID(uint tag, string type)
		{
			if (MainForm.m_connection == null)
				return "";
			
			lock (MainForm.m_connection) //dec-06-06
			{
				string str = null;
				StringBuilder mySelectQuery = new StringBuilder();
				if (type == "AST")
					mySelectQuery.Append("SELECT ParkingNum FROM parkingtags WHERE CarID = ");
				else if (type == "ACC")
					mySelectQuery.Append("SELECT ParkingNum FROM parkingtags WHERE ID = ");
				else
					return (null);  //wrong tag type

				mySelectQuery.AppendFormat("'{0}'", tag);
		    
			
				string mySelectStr = mySelectQuery.ToString();

				OdbcCommand myCommand = new OdbcCommand(mySelectStr, m_connection);
				OdbcDataReader myReader = null;
				/*try
				{
				   myReader = myCommand.ExecuteReader();
				}
				catch (Exception e)
				{
				   Console.WriteLine(e.ToString());
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
				   return ("error");
				}*/
 
				//lock (MainForm.m_connection)
				//{
				try
				{
					myReader = myCommand.ExecuteReader();
				}
				catch (Exception ex)
				{
					int ret = 0;
					if ((ret=ex.Message.IndexOf("Lost connection")) >= 0)
					{  
						//error code 2013
						Thread.Sleep(500);

						if (!ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
						{
							MessageBox.Show("Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							if (myReader != null)
							{
								if (!myReader.IsClosed)
									myReader.Close();
							}
							return "";
						}                             
					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return "";
				} //catch ..try
				//}//lock

				if (myReader.Read())
					str = myReader.GetString(0);
			
				myReader.Close();
				return (str);
			}//lock m_connection
		}
		#endregion

		#region UpdateTags
		public bool UpdateTags(string oldID, string id, string type, string name, byte[] image)
		{
			string SQL = "UPDATE tags SET TagID=?, ID=?, Type=?, Name=?, image=? WHERE ID=? AND type=?";
			OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
				
			cmd.Parameters.Add(new OdbcParameter("", type+id));
			cmd.Parameters.Add(new OdbcParameter("", id));
			cmd.Parameters.Add(new OdbcParameter("", type));
			cmd.Parameters.Add(new OdbcParameter("", name));
		    cmd.Parameters.Add(new OdbcParameter("", image));
			cmd.Parameters.Add(new OdbcParameter("", oldID));
			cmd.Parameters.Add(new OdbcParameter("", type));

			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				return(false);
			}

			return(true);
		}
		#endregion

        #region SaveParkingTag
		public bool SaveParkingTag(bool registered, bool newRecord, string ID, string carID, string ownerName, byte[] image, string ownerID, string tel, string parkNum, string plateNum, string carBrand, string aptNum, string accType, string type)
		{
			string SQL = "";

			if (newRecord)
			{
				SQL = "INSERT INTO ";
				SQL += "parkingtags (CarID, OwnerName, Image, Time, ID, Telephone, ParkingNum, PlateNum, CarBrand, AptNum, accType, Type) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
			}
			else 
			{
				SQL = "UPDATE parkingtags SET ";
				SQL += "CarID=?, OwnerName=?, Image=?, Time=?, ID=?, Telephone=?, ParkingNum=?, PlateNum=?, CarBrand=?,  AptNum=?, accType=?, Type=? WHERE ID = ?";
			}

			
			OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
				
			cmd.Parameters.Add(new OdbcParameter("", carID));
			cmd.Parameters.Add(new OdbcParameter("", ownerName));
			cmd.Parameters.Add(new OdbcParameter("", image));
			cmd.Parameters.Add(new OdbcParameter("", DateTime.Now));
			cmd.Parameters.Add(new OdbcParameter("", ownerID));
			cmd.Parameters.Add(new OdbcParameter("", tel));
			cmd.Parameters.Add(new OdbcParameter("", parkNum));
			cmd.Parameters.Add(new OdbcParameter("", plateNum));
			cmd.Parameters.Add(new OdbcParameter("", carBrand));
			cmd.Parameters.Add(new OdbcParameter("", aptNum));
			cmd.Parameters.Add(new OdbcParameter("", accType));
			cmd.Parameters.Add(new OdbcParameter("", type));
			if (!newRecord)
			   cmd.Parameters.Add(new OdbcParameter("", ID));

			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				return (false);
			}

			byte[] noImage = new Byte[1];
			noImage[0] = Convert.ToByte(0);
			if (newRecord)
			{
				if (!registered)
				{
					SaveTag(ownerID, "ACC", ownerName, image);
					SaveTag(carID, "AST", plateNum, noImage);	
				}
			}
			else
			{
				UpdateTags(ID, ownerID, "ACC", ownerName, image);
				UpdateTags(ID, carID, "AST", plateNum, noImage);
			}

			return (true);
		}
		#endregion

		#region SaveEmployeesTag
		public bool SaveEmployeesTag(bool newRecord, string ID, string fname, string lname, string depart, byte[] image, string accType, string oldTagID)
		{
			string SQL = "";

			if (newRecord)
			{
				SQL = "INSERT INTO ";
				SQL += "employees (ID, Image, Time, AccType, FirstName, LastName, Department) VALUES (?, ?, ?, ?, ?, ?, ?)";
			}
			else 
			{
				SQL = "UPDATE employees SET ";
				SQL += "ID=?, Image=?, Time=?, AccType=?, FirstName=?, LastName=?, Department=? WHERE ID = ?";
			}

			OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
			cmd.Parameters.Add(new OdbcParameter("", ID));
			cmd.Parameters.Add(new OdbcParameter("", image));
			cmd.Parameters.Add(new OdbcParameter("", DateTime.Now));
			cmd.Parameters.Add(new OdbcParameter("", accType));
			cmd.Parameters.Add(new OdbcParameter("", fname));
			cmd.Parameters.Add(new OdbcParameter("", lname));
			cmd.Parameters.Add(new OdbcParameter("", depart));
			if (!newRecord)
				cmd.Parameters.Add(new OdbcParameter("", oldTagID));

			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				return (false);
			}

			byte[] noImage = new Byte[1];
			noImage[0] = Convert.ToByte(0);
			if (newRecord)
			{
				SaveTag(ID, "ACC", fname + " " + lname, image);	
			}
			else
			{
				UpdateTags(oldTagID, ID, "ACC", fname + " " + lname, image);	
			}

			return (true);
		}
		#endregion

		#region SaveEmployeesTag
		public bool SaveEmployeesTag(bool newRecord, string ID, string fname, string lname, string depart, byte[] image, string accType, string oldTagID, string type)
		{
			string SQL = "";

			if (newRecord)
			{
				SQL = "INSERT INTO ";
				SQL += "employees (ID, Image, Time, AccType, FirstName, LastName, Department) VALUES (?, ?, ?, ?, ?, ?, ?)";
			}
			else 
			{
				SQL = "UPDATE employees SET ";
				SQL += "ID=?, Image=?, Time=?, AccType=?, FirstName=?, LastName=?, Department=? WHERE ID = ?";
			}

			OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
			cmd.Parameters.Add(new OdbcParameter("", ID));
			cmd.Parameters.Add(new OdbcParameter("", image));
			cmd.Parameters.Add(new OdbcParameter("", DateTime.Now));
			cmd.Parameters.Add(new OdbcParameter("", accType));
			cmd.Parameters.Add(new OdbcParameter("", fname));
			cmd.Parameters.Add(new OdbcParameter("", lname));
			cmd.Parameters.Add(new OdbcParameter("", depart));
			if (!newRecord)
				cmd.Parameters.Add(new OdbcParameter("", oldTagID));

			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				return (false);
			}

			byte[] noImage = new Byte[1];
			noImage[0] = Convert.ToByte(0);
			if (newRecord)
			{
				SaveTag(ID, type, fname + " " + lname, image);	
			}
			else
			{
				UpdateTags(oldTagID, ID, type, fname + " " + lname, image);	
			}

			return (true);
		}
		
		#endregion

		#region SaveEmployeesTag
		public bool SaveEmployeesTag(bool newRecord, string ID, string fname, string lname, string depart, byte[] image, string accType, string oldTagID, string type, DateTime expTime)
		{
			string SQL = "";

			if (newRecord)
			{
				SQL = "INSERT INTO ";
				SQL += "employees (ID, Image, Time, AccType, expTime, FirstNAme, LastName, Department) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
			}
			else 
			{
				SQL = "UPDATE employees SET ";
				SQL += "ID=?, Image=?, Time=?, AccType=?, expTime=?, FirstName=?, LastName=?, Department=? WHERE ID=?";
			}

			OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
			cmd.Parameters.Add(new OdbcParameter("", ID));
			cmd.Parameters.Add(new OdbcParameter("", image));
			cmd.Parameters.Add(new OdbcParameter("", DateTime.Now));
			cmd.Parameters.Add(new OdbcParameter("", accType));
			cmd.Parameters.Add(new OdbcParameter("", expTime));
			cmd.Parameters.Add(new OdbcParameter("", fname));
			cmd.Parameters.Add(new OdbcParameter("", lname));
			cmd.Parameters.Add(new OdbcParameter("", depart));

			if (!newRecord)
				cmd.Parameters.Add(new OdbcParameter("", oldTagID));

			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				return (false);
			}

			byte[] noImage = new Byte[1];
			noImage[0] = Convert.ToByte(0);
			if (newRecord)
			{
				SaveTag(ID, type, fname + " " + lname, image);	
			}
			else
			{
				UpdateTags(oldTagID, ID, type, fname + " " + lname, image);	
			}

			return (true);
		}
		
		#endregion

		#region SaveAssetTag
		public bool SaveAssetTag(bool newRecord, string id, bool mobile, string name, string model, string sku, string descript, DateTime timestamp, byte[] image, string oldTagID)
		{
			string SQL = "";

			if (newRecord)
			{
				SQL = "INSERT INTO ";
				SQL += "asset (id, mobile, name, model, sku, description, timestamp, image) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
			}
			else 
			{
				SQL = "UPDATE asset SET ";
				SQL += "ID=?, Mobile=?, Name=?, Model=?, SKU=?, Description=?, Timestamp=?, Image=?  WHERE ID = ?";
			}

			OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
			cmd.Parameters.Add(new OdbcParameter("", id));
			cmd.Parameters.Add(new OdbcParameter("", mobile));
			cmd.Parameters.Add(new OdbcParameter("", name));
			cmd.Parameters.Add(new OdbcParameter("", model));
			cmd.Parameters.Add(new OdbcParameter("", sku));
			cmd.Parameters.Add(new OdbcParameter("", descript));
			cmd.Parameters.Add(new OdbcParameter("", timestamp));
			cmd.Parameters.Add(new OdbcParameter("", image));
			
			if (!newRecord)
				cmd.Parameters.Add(new OdbcParameter("", oldTagID));

			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				return (false);
			}

			byte[] noImage = new Byte[1];
			noImage[0] = Convert.ToByte(0);
			if (newRecord)
			{
				SaveTag(id, "AST", name, image);	
			}
			else
			{
				UpdateTags(oldTagID, id, "AST", name, image);	
			}

			return (true);
		}
		
		#endregion

		#region SaveTag
		public bool SaveTag(string id, string type, string name, byte[] image)
		{
			string SQL = "INSERT INTO tags (TagID, ID, Type, Name, Image) VALUES (?, ?, ?, ?, ?)";
			string str = type+id;
			
			OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
			cmd.CommandText = SQL;
			cmd.Parameters.Add(new OdbcParameter("", str));
			cmd.Parameters.Add(new OdbcParameter("", id));
			cmd.Parameters.Add(new OdbcParameter("", type));
			cmd.Parameters.Add(new OdbcParameter("", name));
			cmd.Parameters.Add(new OdbcParameter("", image));
			
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				return(false);
			}

			return(true);
		}
		#endregion

		#region DeleteTag
		public bool DeleteTag(string id, string type)
		{
			string SQL = "DELETE FROM tags WHERE TagID = ?";
			string str = type+id;
			
			OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
			cmd.CommandText = SQL;
			cmd.Parameters.Add(new OdbcParameter("", str));
			
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				return(false);
			}

			return(true);
		}
		#endregion

		#region SaveZone (bool newRecord, string zoneID, string id, string location, string rdrID, string fgenID, string status)
		//public void SaveZone(bool newRecord, string zoneID, bool lineTagCheck, string id, string location, string rdrID, string fgenID, string status, string lineID)
		public bool SaveZone(bool newRecord, string zoneID, string id, string location, string rdrID, string fgenID, string status)
		{
			string SQL = "";
			
			if (newRecord)
			{
				SQL = "INSERT INTO ";
				SQL += "zones (ID, Location, ReaderID, FieldGenID, Status, Time) VALUES (?, ?, ?, ?, ?, ?)";
			}
			else 
			{
				SQL = "UPDATE zones SET ";
				SQL += "ID=?, Location=?, ReaderID=?, FieldGenID=?, Status=?, Time=? WHERE ID = ?";
				//OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
				//cmd.Parameters.Add(new OdbcParameter("", location));
				//cmd.Parameters.Add(new OdbcParameter("", id));
			}
			
			//string str = "L"+id;
			
			OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
			cmd.CommandText = SQL;
            
			//if (lineTagCheck)   
			    //cmd.Parameters.Add(new OdbcParameter("", str));
			//else
            cmd.Parameters.Add(new OdbcParameter("", id));
			cmd.Parameters.Add(new OdbcParameter("", location));
			cmd.Parameters.Add(new OdbcParameter("", rdrID));
			cmd.Parameters.Add(new OdbcParameter("", fgenID));
			cmd.Parameters.Add(new OdbcParameter("", status));
			cmd.Parameters.Add(new OdbcParameter("", DateTime.Now));
            //cmd.Parameters.Add(new OdbcParameter("", 0));   //zoneType
			//cmd.Parameters.Add(new OdbcParameter("", " ")); //slotStatus
			//if (lineTagCheck)
			   //cmd.Parameters.Add(new OdbcParameter("", "1"));
			//else
			   //cmd.Parameters.Add(new OdbcParameter("", "0"));
			//cmd.Parameters.Add(new OdbcParameter("", lineID));
            if (!newRecord)
			    cmd.Parameters.Add(new OdbcParameter("", zoneID));
			
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				return (false);
			}

			return (true);
		}
		#endregion

		#region SaveZone (bool newRecord, string zoneID, string id, string location, string rdrID, bool rssi, ushort threshold, string fgenID, string status)
		//public void SaveZone(bool newRecord, string zoneID, bool lineTagCheck, string id, string location, string rdrID, string fgenID, string status, string lineID)
		public bool SaveZone(bool newRecord, string zoneID, string id, string location, string rdrID, bool rssi, ushort threshold, string fgenID, string status)
		{
			string SQL = "";
			
			if (newRecord)
			{
				SQL = "INSERT INTO ";
				SQL += "zones (ID, Location, ReaderID, FieldGenID, RSSI, Threshold, Status, Time) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
			}
			else 
			{
				SQL = "UPDATE zones SET ";
				SQL += "ID=?, Location=?, ReaderID=?, FieldGenID=?, RSSI=?, Threshold=?, Status=?, Time=? WHERE ID = ?";
				//OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
				//cmd.Parameters.Add(new OdbcParameter("", location));
				//cmd.Parameters.Add(new OdbcParameter("", id));
			}
			
			//string str = "L"+id;
			
			OdbcCommand cmd = new OdbcCommand(SQL, m_connection);
			cmd.CommandText = SQL;
            
			//if (lineTagCheck)   
			//cmd.Parameters.Add(new OdbcParameter("", str));
			//else
			cmd.Parameters.Add(new OdbcParameter("", id));
			cmd.Parameters.Add(new OdbcParameter("", location));
			cmd.Parameters.Add(new OdbcParameter("", rdrID));
			cmd.Parameters.Add(new OdbcParameter("", fgenID));
			cmd.Parameters.Add(new OdbcParameter("", rssi));
			cmd.Parameters.Add(new OdbcParameter("", threshold));
			cmd.Parameters.Add(new OdbcParameter("", status));
			cmd.Parameters.Add(new OdbcParameter("", DateTime.Now));
			//cmd.Parameters.Add(new OdbcParameter("", 0));   //zoneType
			//cmd.Parameters.Add(new OdbcParameter("", " ")); //slotStatus
			//if (lineTagCheck)
			//cmd.Parameters.Add(new OdbcParameter("", "1"));
			//else
			//cmd.Parameters.Add(new OdbcParameter("", "0"));
			//cmd.Parameters.Add(new OdbcParameter("", lineID));
			if (!newRecord)
				cmd.Parameters.Add(new OdbcParameter("", zoneID));
			
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				return (false);
			}

			return (true);
		}
		#endregion

		#region GetTagImage
		public Image GetTagImage(string tagId, string table)
		{
			string sql = string.Format("SELECT Image FROM {0} WHERE ID = '{1}'", table, tagId);
			OdbcCommand cmd = new OdbcCommand(sql, m_connection);
            
			byte[] data = cmd.ExecuteScalar() as byte[];
			if (data != null)
			{
				try
				{
					Stream stream = new MemoryStream(data);
					return Image.FromStream(stream);
				}
				catch {}
			}
			return null;
		}
		#endregion

	}
}
