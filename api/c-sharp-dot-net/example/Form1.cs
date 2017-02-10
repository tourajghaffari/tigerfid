using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Timers;
using AW_API_NET;

// ***********************************************************************
// Sep 2009          Eddy Celis (EDC)
// - Support for VS 2008
// - Use dispatcher on API callbacks to allow for controls update
// - Use list box message queue and timer to speed up callback handling
// - Add "rfOpenSocketRdr" button.  Use if rdr ID and IP address are 
//   known to allow for specific socket open without doing "all" IP open..
//   
// ***********************************************************************

namespace WindowsApplication1
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox IPListBox;
		private System.Windows.Forms.TextBox ReaderIDTextBox;
		private System.Windows.Forms.TextBox HostIDTextBox;
		private System.Windows.Forms.ComboBox TagTypeComboBox;
		private System.Windows.Forms.TextBox TagIDTextBox;
		private System.Windows.Forms.ListBox MsgListBox;
		private System.Windows.Forms.Button OpenSerialButton;
        private System.Windows.Forms.Button CloseSerialButton;
        private IContainer components;
		private System.Windows.Forms.Button ClearButton;
		private System.Windows.Forms.Button ResetRdrButton;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Button EnableRdrButton;
		private System.Windows.Forms.Button ScanNetworkButton;
		//private System.Windows.Forms.Button ResetButton;
		
		//Get an instance of API
		public APINetClass apiClass = new APINetClass();
		private System.Windows.Forms.Button OpenConnectButton;
		private System.Windows.Forms.Button CloseSockButton;
		private System.Windows.Forms.CheckBox BroadcastRdrCheckBox;
		private System.Windows.Forms.Button QueryTagButton1;
		private System.Windows.Forms.Button EnableTagButton;
		private System.Windows.Forms.CheckBox AnyTypeCheckBox;
		private System.Windows.Forms.CheckBox AnyIDCheckBox;
		private System.Windows.Forms.CheckBox LongDelayCheckBox;

		//if reader event is registered
		private bool rdrEventRegistered = false;
		private System.Windows.Forms.Button QueryRdrButton;
		private System.Windows.Forms.Button CallTagButton;
		private System.Windows.Forms.Button ReadTagButton;
		private System.Windows.Forms.Button WriteTagButton;
		private System.Windows.Forms.TextBox AddressTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox LenTextBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox DataTextBox1;
		private System.Windows.Forms.TextBox DataTextBox2;
		private System.Windows.Forms.TextBox DataTextBox3;
		private System.Windows.Forms.TextBox DataTextBox4;
		private System.Windows.Forms.TextBox DataTextBox5;
		private System.Windows.Forms.Button ResetSmartFGenbutton;
		private System.Windows.Forms.Button QuerySTDFGenbutton;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox BroadcastFGenCheckBox;
		private System.Windows.Forms.RadioButton RespondSpecRadioButton;
		private System.Windows.Forms.RadioButton RespondAnyRadioButton;
		private System.Windows.Forms.TextBox SmartFGenTextBox;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.TextBox ComPortTextBox;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.RadioButton Baud115200RadioButton;
		private System.Windows.Forms.RadioButton Baud9600RadioButton;
		private System.Windows.Forms.Button QuerySmartFGenButton;
		private System.Windows.Forms.Button GetTagTempButton;
		private System.Windows.Forms.Button GetTagTempConfigButton;
		private System.Windows.Forms.RadioButton AllIPSRadioButton;
		private System.Windows.Forms.RadioButton SpecificIPRadioButton;
		private System.Windows.Forms.TextBox IPTextBox;
		private System.Windows.Forms.Label IPLabel;
		private System.Windows.Forms.Button SetTagLoggingTimestampButton;
		private System.Windows.Forms.Button GetTagLoggingTimestampButton;
		private System.Windows.Forms.Button ScanIPButton;

        private Queue msgQ;
        private System.Windows.Forms.Timer timer1;

        private delegate int readerEventDispatch(AW_API_NET.rfReaderEvent_t readerEvent);
        private readerEventDispatch readerEventHandlerDispatch;
        private Button OpenConnSpecRdr;
    
        private delegate int tagEventDispatch(AW_API_NET.rfTagEvent_t tagEvent);
        private tagEventDispatch tagEventHandlerDispatch;

		//This function will be called from API to pass info about the reader 
		//in the rfReaderEvent_t structure
        // Use dispatcher to allow for control updates Sep 2009 (EDC)
        public int ReaderEvent(AW_API_NET.rfReaderEvent_t readerEvent)
        {
            return (int)Invoke(readerEventHandlerDispatch, new object[] { readerEvent });
        }

        public int ReaderEventHandler(AW_API_NET.rfReaderEvent_t readerEvent)
		{
			string str = Convert.ToString(item);
			str += "- "	;
			int ret = 0;

			if (readerEvent.errorStatus != AW_API_NET.APIConsts.RF_E_NO_ERROR)
			{
			   str += "Event = ";
			   str += readerEvent.eventType;
			   str += "   error = ";
			   str += readerEvent.errorStatus;
			   str += "   status = ";
			   str += readerEvent.eventStatus;
			   str += "   pktID = ";
			   str += readerEvent.pktID;

               addMsgQ(str);
			   return (0);
			}

			switch (readerEvent.eventType)
			{
			    case AW_API_NET.APIConsts.RF_RELAY_ENABLE:
				case AW_API_NET.APIConsts.RF_RELAY_ENABLE_ALL:
					str += "rfEnableReader()";
					str += "   pktID = ";
					str += readerEvent.pktID;
					str += "   readerID = ";
					str += readerEvent.reader;
	      
					if (readerEvent.eventStatus == AW_API_NET.APIConsts.RF_INVALID_PACKET)
					{
						str += "    Error Code = ";
						str += readerEvent.errorStatus;
					}
					else
					{
						str += "   was successful.";
					}
                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_READER_DISABLE:
					str += "rfDisableReader()";
					str += "   pktID = ";
					str += readerEvent.pktID;
					str += "   readerID = ";
					str += readerEvent.reader;
	      
					if (readerEvent.eventStatus == AW_API_NET.APIConsts.RF_INVALID_PACKET)
					{
						str += "    Error Code = ";
						str += readerEvent.errorStatus;
					}
					else
					{
						str += "   was successful.";
					}

                    addMsgQ(str);
                    break;
			
				case AW_API_NET.APIConsts.RF_READER_ENABLE:
					str += "rfEnableReader()";
					str += "   pktID = ";
					str += readerEvent.pktID;
					str += "   readerID = ";
					str += readerEvent.reader;
	      
					if (readerEvent.eventStatus == AW_API_NET.APIConsts.RF_INVALID_PACKET)
					{
						str += "    Error Code = ";
						str += readerEvent.errorStatus;
					}
					else
					{
						str += "   was successful.";
					}

                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_READER_POWERUP:
					str += "Reader Powered Up.";
					str += "   hostID = ";
					str += readerEvent.host;
					str += "   readerID = ";
					str += readerEvent.reader;
		   
					if (ret != AW_API_NET.APIConsts.RF_S_DONE)
					{
						str += "   Error Code = ";
						str += readerEvent.errorStatus;
					}
					else
					{
						str += "   was successful.";
					}

                    addMsgQ(str);

                    HostIDTextBox.Text = Convert.ToString(readerEvent.host, 10);
                    ReaderIDTextBox.Text = Convert.ToString(readerEvent.reader, 10);
				    break;

				case AW_API_NET.APIConsts.RF_STD_FGEN_POWERUP:
					str += "STD FGen Powered Up.";
					str += "   hostID = ";
					str += readerEvent.host;
					str += "   FGenID = ";
					str += readerEvent.fGenerator;
					SmartFGenTextBox.Text = Convert.ToString(readerEvent.fGenerator);

                    addMsgQ(str);

					HostIDTextBox.Text = Convert.ToString(readerEvent.host, 10);
					ReaderIDTextBox.Text = Convert.ToString(readerEvent.reader, 10);
				    break;

				case AW_API_NET.APIConsts.RF_SMART_FGEN_POWERUP:
					str += "Smart FGen Powered Up.";
					str += "   hostID = ";
					str += readerEvent.host;
					str += "   FGenID = ";
					str += readerEvent.smartFgen.ID;
					str += "   was successful.";
					SmartFGenTextBox.Text = Convert.ToString(readerEvent.fGenerator);

                    addMsgQ(str);

					HostIDTextBox.Text = Convert.ToString(readerEvent.host, 10);
					ReaderIDTextBox.Text = Convert.ToString(readerEvent.reader, 10);
				    break;

			   case AW_API_NET.APIConsts.RF_END_OF_BROADCAST:
				   str += "End Of Broadcast.   pktID = ";
				   str += readerEvent.pktID;
                   addMsgQ(str);
                   break;

			    case AW_API_NET.APIConsts.RF_READER_RESET:
				case AW_API_NET.APIConsts.RF_READER_RESET_ALL:
					str += "rfResetReader()";
					str += "   pktID = ";
					str += readerEvent.pktID;
					str += "   readerID = ";
					str += readerEvent.reader;
	      
					if (readerEvent.eventStatus == AW_API_NET.APIConsts.RF_INVALID_PACKET)
					{
						str += "    Error Code = ";
						str += readerEvent.errorStatus;
					}
					else
					{
						str += "   was successful.";
					}

                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_RESET_SMART_FGEN:
				case AW_API_NET.APIConsts.RF_RESET_SMART_FGEN_ALL:
					str += "rfResetSmartFGen()";
					str += "   FGen ID = ";
					str += readerEvent.smartFgen.ID;
					str += "   ReaderID = ";
					str += readerEvent.reader;
					str += "   pktID = ";
					str += readerEvent.pktID;
                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_READER_QUERY:
				case AW_API_NET.APIConsts.RF_READER_QUERY_ALL:
					str += "rfQueryReader()";
					str += "   pktID = ";
					str += readerEvent.pktID;
					str += "   readerID = ";
					str += readerEvent.reader;
					str += "   Send RSSI = ";
					//if (readerEvent.readerInfo.sendRSSI)
					  //str += "enabled";
					//else
					  //str += "disabled";
	      
					if (readerEvent.eventStatus == AW_API_NET.APIConsts.RF_INVALID_PACKET)
					{
						str += "    Error Code = ";
						str += readerEvent.errorStatus;
					}
					else
					{
						str += "   was successful.";
					}

                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_QUERY_STD_FGEN:
					str += "rfQuerySTDFgen()";
					str += "   readerID = ";
					str += readerEvent.smartFgen.readerID;
					str += "   Field strength = ";
					str += readerEvent.smartFgen.fsValue;
				    str += "   TX time = ";
					str += readerEvent.smartFgen.txTime + " sec";
					str += "   Motion detect = ";
					if (readerEvent.smartFgen.mDetectActive)
						str += "enabled";
					else
						str += "disabled";
					str += "   pktID = ";
					str += readerEvent.pktID;

                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_QUERY_SMART_FGEN:
				case AW_API_NET.APIConsts.RF_QUERY_SMART_FGEN_ALL:

					str += "rfQuerySmartFgen()";
					str += "   assigned readerID = ";
					str += readerEvent.smartFgen.readerID;
					str += "   Field strength = ";
					str += readerEvent.smartFgen.fsValue;
				    str += "   TX time = ";
					str += readerEvent.smartFgen.txTime + " sec";
					str += "   Motion detect = ";
					if (readerEvent.smartFgen.mDetectActive)
						str += "enabled";
					else
						str += "disabled";
					str += "   pktID = ";
					str += readerEvent.pktID;

                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_SCAN_NETWORK:
					str = GetStringIP(readerEvent.ip);
					if (str.Length > 0)
					{
					  IPListBox.Items.Add(GetStringIP(readerEvent.ip));
                      IPTextBox.Text = GetStringIP(readerEvent.ip);
					  item += 1;
					}
				break;

				case AW_API_NET.APIConsts.RF_SCAN_IP:
					str = GetStringIP(readerEvent.ip);
					
					if (!FindIP(str))
					{
						if (str.Length > 0)
						{
							IPListBox.Items.Add(GetStringIP(readerEvent.ip));
							item += 1;
						}
					}
				break;

				case AW_API_NET.APIConsts.RF_OPEN_SOCKET:
                    str += "socket opened successfully. IP = " + GetStringIP(readerEvent.ip);
                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_CLOSE_SOCKET:
					str += "rfCloseSocket()   pktID = ";
					str += readerEvent.pktID;
					
					if (readerEvent.ip[0] != 0x00)
					{
						str += "   IP = ";
						str += GetStringIP(readerEvent.ip);
					}

					if (readerEvent.eventStatus == AW_API_NET.APIConsts.RF_INVALID_PACKET)
					{
						str += "   Error Code = ";
						str += readerEvent.errorStatus;
					}
					else
					{
						str += "   was successful.";
					}
                    addMsgQ(str);
                    break;

			}//switch

			return(0);
		}

		private int item = 1;
		private ushort pktID = 1;
		rfTagSelect_t tagSelect = new rfTagSelect_t();

        //This function will be called from API to pass info about the 
		//tag in the rfTagEvent_t structure
        // Use dispatcher to allow for control updates. Sep 2009 (EDC)
        public int TagEvent(AW_API_NET.rfTagEvent_t tagEvent)
        {
            return (int)Invoke(tagEventHandlerDispatch, new object[] { tagEvent });
        }

		public int TagEventHandler(AW_API_NET.rfTagEvent_t tagEvent)
		{
		   
			string str = Convert.ToString(item);
			str += "- "	;

			if (tagEvent.errorStatus != AW_API_NET.APIConsts.RF_E_NO_ERROR)
			{
			   str += "Event = ";
			   str += tagEvent.eventType;
			   str += "   error = ";
			   str += tagEvent.errorStatus;
			   str += "   status = ";
			   str += tagEvent.eventStatus;
			   str += "   pktID = ";
			   str += tagEvent.pktID;

               addMsgQ(str);
               return (0);
			}

			
			switch (tagEvent.eventType)
			{
				case AW_API_NET.APIConsts.RF_GET_TAG_TEMP:

					str += "rfGetTagTemp()";
					str += "   tagID = ";
					str += tagEvent.tag.id;
					str += "   type = ";
					if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
					   str += "access";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
					   str += "asset";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
					   str += "inventory";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.FACTORY_TAG)
					   str += "factory";
					str += "   reader = ";
					str += tagEvent.reader;
					str += "   pktID = ";
					str += tagEvent.pktID;

                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_TAG_TEMP:

					str = "       tagID = ";
					str += tagEvent.tag.id;
					str += "   Tag Temperature = ";
					str += tagEvent.tag.temp.temperature.ToString("F");
					if (tagEvent.tag.temp.status == AW_API_NET.APIConsts.RF_TAG_TEMP_NORM)
						str += "   NORMAL";
					else if (tagEvent.tag.temp.status == AW_API_NET.APIConsts.RF_TAG_TEMP_LOW)
						str += "   LOW";
					else if (tagEvent.tag.temp.status == AW_API_NET.APIConsts.RF_TAG_TEMP_HIGH)
						str += "   HIGH";
					str += "   reader = ";
					str += tagEvent.reader;
                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_GET_TAG_TEMP_CONFIG:

					str += "rfGetTagTempConfig()";
					str += "   tagID = ";
					str += tagEvent.tag.id;
					str += "   type = ";
					if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
					   str += "access";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
					   str += "asset";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
					   str += "inventory";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.FACTORY_TAG)
					   str += "factory";
					str += "   reader = ";
					str += tagEvent.reader;
					str += "   FG = ";
					str += tagEvent.fGenerator;
					str += "   pktID = ";
					str += tagEvent.pktID;
                    addMsgQ(str);
					
					str = "       ";
					if (tagEvent.tag.temp.rptUnderLowerLimit)
		                str += "Report Under Low Temp Limit = Yes";
					else
						str += "Report Under Low Temp Limit = No";
                    addMsgQ(str);

					str = "       "; 
					if (tagEvent.tag.temp.rptOverUpperLimit)
						str += "Report Over Upper Temp Limit = Yes";
					else
						str += "Report Over Upper Temp Limit = No";
                    addMsgQ(str);

					str = "       ";					
					if (tagEvent.tag.temp.rptPeriodicRead)
					str += "Report Periodic Read = Yes";
					else
					str += "Report Periodic Read = No";
                    addMsgQ(str);

					str = "       ";					
					str += "Number of Reads per Average = ";
					str += tagEvent.tag.temp.numReadAve;
                    addMsgQ(str);

					str = "       ";					
					str += "Periodic Report Time = ";
					str += tagEvent.tag.temp.periodicRptTime;
					if (tagEvent.tag.temp.periodicTimeType == AW_API_NET.APIConsts.RF_TIME_HOUR)
						str += "  Hour";
					else
						str += "  Minute";
                    addMsgQ(str);

					str = "        Lower Limit Temp = " + tagEvent.tag.temp.lowerLimitTemp.ToString("F");
                    addMsgQ(str);

					str = "        Upper Limit Temp = " + tagEvent.tag.temp.upperLimitTemp.ToString("F");
                    addMsgQ(str);

					if (tagEvent.tag.temp.enableTempLogging)
					   str = "        Enable Tag Temp Logging = Yes";
					else
					   str = "        Enable Tag Temp Logging = No";
                    addMsgQ(str);

					if (tagEvent.tag.temp.logging)
						str = "        Tag is Logging Tag Temp = Yes";
					else
						str = "        Tag is Logging Tag Temp = No";
                    addMsgQ(str);

					if (tagEvent.tag.temp.wrapAround)
						str = "        Warp-around is Enabled = Yes";
					else
						str = "        Warp-around is Enabled = No";
                    addMsgQ(str);

				break;

				case AW_API_NET.APIConsts.RF_SET_TAG_TEMP_LOG_TIMESTAMP:
					str += "rfSetTagTempLogTimestamp()";
					str += "   tagID = ";
					str += tagEvent.tag.id;
					str += "   type = ";
					if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
						str += "access";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
						str += "asset";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
						str += "inventory";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.FACTORY_TAG)
						str += "factory";
					str += "   reader = ";
					str += tagEvent.reader;
					str += "   pktID = ";
					str += tagEvent.pktID;

                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_GET_TAG_TEMP_LOG_TIMESTAMP:
					str += "rfGetTagTempLogTimestamp()";
					str += "   tagID = ";
					str += tagEvent.tag.id;
					str += "   type = ";
					if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
						str += "access";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
						str += "asset";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
						str += "inventory";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.FACTORY_TAG)
						str += "factory";
					str += "   reader = ";
					str += tagEvent.reader;
					str += "   FG = ";
					str += tagEvent.fGenerator;
					str += "   pktID = ";
					str += tagEvent.pktID;
                    addMsgQ(str);
					
					str = "       ";
					str += tagEvent.logTimestamp.month.ToString();
					str += "/" + tagEvent.logTimestamp.day.ToString();
					str += "/" + tagEvent.logTimestamp.year.ToString();
					str += "    " + tagEvent.logTimestamp.hour.ToString();
					str += ":" + tagEvent.logTimestamp.min.ToString();
					str += ":" + tagEvent.logTimestamp.sec.ToString();

                    addMsgQ(str);
                    break;
					
			    case AW_API_NET.APIConsts.RF_TAG_ENABLE:

					str += "rfEnableTags()";
					str += "   tagID = ";
					str += tagEvent.tag.id;
					str += "   type = ";
					if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
					   str += "access";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
					   str += "asset";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
					   str += "inventory";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.FACTORY_TAG)
					   str += "factory";
					str += "   reader = ";
					str += tagEvent.reader;
					str += "   pktID = ";
					str += tagEvent.pktID;

                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_TAG_DISABLE:

					str += "rfDisableTags()";
					str += "   tagID = ";
					str += tagEvent.tag.id;
					str += "   type = ";
					if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
					   str += "access";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
					   str += "asset";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
					   str += "inventory";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.FACTORY_TAG)
					   str += "factory";
					str += "   reader = ";
					str += tagEvent.reader;
					str += "   pktID = ";
					str += tagEvent.pktID;

                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_TAG_QUERY:

					str += "rfQueryTags()";
					str += "   tagID = ";
					str += tagEvent.tag.id;
					str += "   type = ";
					if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
					   str += "access";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
					   str += "asset";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
					   str += "inventory";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.FACTORY_TAG)
					   str += "factory";
					if (tagEvent.tag.status.enabled)
					   str += "   Enabled";
				    else
					   str += "   Disabled";
					if (tagEvent.tag.status.batteryLow)
						str += "   Battery LOW";
					else
						str += "   Battery OK";
					str += "   reader = ";
					str += tagEvent.reader;
					str += "   pktID = ";
					str += tagEvent.pktID;

                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_TAG_DETECTED:
				case AW_API_NET.APIConsts.RF_TAG_DETECTED_RSSI:
                case AW_API_NET.APIConsts.RF_TAG_DETECTED_SANI:

                    if (tagEvent.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED_SANI)
                    {
                        str += "Sani ";
                    }
                    str += "Tag detected";
					str += "   tagID = ";
					str += tagEvent.tag.id;
					str += "   type = ";
					if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
					   str += "access";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
					   str += "asset";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
					   str += "inventory";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.FACTORY_TAG)
					   str += "factory";
					if ((tagEvent.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED_RSSI) ||
                        (tagEvent.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED_SANI))
					{
						str += "   RSSI = ";
						str += tagEvent.RSSI;
					}
					str += "   reader = ";
					str += tagEvent.reader;
                    if (tagEvent.eventType == AW_API_NET.APIConsts.RF_TAG_DETECTED_SANI)
                    {
                        str += "  Unit = ";
                        str += tagEvent.tag.sani.UnitType.ToString();
                        str += "  Status = ";
                        str += tagEvent.tag.sani.Status.ToString();
                    }

                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_TAG_READ:

					str += "rfReadTags()";
					str += "   tagID = ";
					str += tagEvent.tag.id;
					str += "   type = ";
					if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
					   str += "access";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
					   str += "asset";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
					   str += "inventory";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.FACTORY_TAG)
					   str += "factory";
					str += "   data = ";
					for (int i=0; i<tagEvent.tag.dataLen; i++)
					{
						str += tagEvent.tag.data[i];
					    str += " ";
				    }
					str += "   reader = ";
					str += tagEvent.reader;
					str += "   pktID = ";
					str += tagEvent.pktID;

                    addMsgQ(str);
                    break;

				case AW_API_NET.APIConsts.RF_TAG_WRITE:

					str += "rfWriteTags()";
					str += "   tagID = ";
					str += tagEvent.tag.id;
					str += "   type = ";
					if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ACCESS_TAG)
					   str += "access";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.ASSET_TAG)
					   str += "asset";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.INVENTORY_TAG)
					   str += "inventory";
					else if (tagEvent.tag.tagType == AW_API_NET.APIConsts.FACTORY_TAG)
					   str += "factory";
					str += "   reader = ";
					str += tagEvent.reader;
					str += "   pktID = ";
					str += tagEvent.pktID;

                    addMsgQ(str);
                    break;
			}
				
		   return (0);
		}

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			tagSelect.tagList = new uint[50];
            // Create message Q
            msgQ = new Queue();
            timer1.Enabled = true;
            // Set up dispatchers  Sep 2009 (EDC)
            readerEventHandlerDispatch = new readerEventDispatch(ReaderEventHandler);
            tagEventHandlerDispatch = new tagEventDispatch(TagEventHandler);
		}

        // Timer to consume listbox messages
        private void timer1_Tick(object sender, EventArgs e)
        {
            string msg;

            lock (msgQ) {
                while (msgQ.Count != 0) {
                    msg = (string)msgQ.Dequeue();
                    MsgListBox.Items.Add(msg);
                }
            }
            if (MsgListBox.Items.Count > 0) {
                MsgListBox.SetSelected(MsgListBox.Items.Count - 1, true);
                MsgListBox.SetSelected(MsgListBox.Items.Count - 1, false);
            }
        }

        private void addMsgQ(string msg)
        {
            lock (msgQ) {
                msgQ.Enqueue(msg);
                item += 1;
            }
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            timer1.Enabled = false;
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.OpenSerialButton = new System.Windows.Forms.Button();
            this.ScanNetworkButton = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.Baud9600RadioButton = new System.Windows.Forms.RadioButton();
            this.Baud115200RadioButton = new System.Windows.Forms.RadioButton();
            this.ComPortTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.CloseSerialButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.IPLabel = new System.Windows.Forms.Label();
            this.ScanIPButton = new System.Windows.Forms.Button();
            this.AllIPSRadioButton = new System.Windows.Forms.RadioButton();
            this.SpecificIPRadioButton = new System.Windows.Forms.RadioButton();
            this.IPListBox = new System.Windows.Forms.ListBox();
            this.CloseSockButton = new System.Windows.Forms.Button();
            this.OpenConnectButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ResetRdrButton = new System.Windows.Forms.Button();
            this.HostIDTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ReaderIDTextBox = new System.Windows.Forms.TextBox();
            this.BroadcastRdrCheckBox = new System.Windows.Forms.CheckBox();
            this.QueryRdrButton = new System.Windows.Forms.Button();
            this.EnableRdrButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.GetTagLoggingTimestampButton = new System.Windows.Forms.Button();
            this.SetTagLoggingTimestampButton = new System.Windows.Forms.Button();
            this.GetTagTempConfigButton = new System.Windows.Forms.Button();
            this.GetTagTempButton = new System.Windows.Forms.Button();
            this.DataTextBox5 = new System.Windows.Forms.TextBox();
            this.DataTextBox4 = new System.Windows.Forms.TextBox();
            this.DataTextBox3 = new System.Windows.Forms.TextBox();
            this.DataTextBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DataTextBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LenTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.AddressTextBox = new System.Windows.Forms.TextBox();
            this.WriteTagButton = new System.Windows.Forms.Button();
            this.ReadTagButton = new System.Windows.Forms.Button();
            this.LongDelayCheckBox = new System.Windows.Forms.CheckBox();
            this.AnyTypeCheckBox = new System.Windows.Forms.CheckBox();
            this.AnyIDCheckBox = new System.Windows.Forms.CheckBox();
            this.TagIDTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TagTypeComboBox = new System.Windows.Forms.ComboBox();
            this.CallTagButton = new System.Windows.Forms.Button();
            this.QueryTagButton1 = new System.Windows.Forms.Button();
            this.EnableTagButton = new System.Windows.Forms.Button();
            this.MsgListBox = new System.Windows.Forms.ListBox();
            this.ClearButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.RespondAnyRadioButton = new System.Windows.Forms.RadioButton();
            this.RespondSpecRadioButton = new System.Windows.Forms.RadioButton();
            this.BroadcastFGenCheckBox = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.QuerySmartFGenButton = new System.Windows.Forms.Button();
            this.SmartFGenTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ResetSmartFGenbutton = new System.Windows.Forms.Button();
            this.QuerySTDFGenbutton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.OpenConnSpecRdr = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenSerialButton
            // 
            this.OpenSerialButton.Location = new System.Drawing.Point(36, 64);
            this.OpenSerialButton.Name = "OpenSerialButton";
            this.OpenSerialButton.Size = new System.Drawing.Size(144, 23);
            this.OpenSerialButton.TabIndex = 0;
            this.OpenSerialButton.Text = "Open Serial Port";
            this.OpenSerialButton.Click += new System.EventHandler(this.OpenSerialButton_Click);
            // 
            // ScanNetworkButton
            // 
            this.ScanNetworkButton.Location = new System.Drawing.Point(34, 60);
            this.ScanNetworkButton.Name = "ScanNetworkButton";
            this.ScanNetworkButton.Size = new System.Drawing.Size(142, 23);
            this.ScanNetworkButton.TabIndex = 1;
            this.ScanNetworkButton.Text = "Scan Network";
            this.ScanNetworkButton.Click += new System.EventHandler(this.ScanNetworkButton_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.Baud9600RadioButton);
            this.groupBox.Controls.Add(this.Baud115200RadioButton);
            this.groupBox.Controls.Add(this.ComPortTextBox);
            this.groupBox.Controls.Add(this.label12);
            this.groupBox.Controls.Add(this.CloseSerialButton);
            this.groupBox.Controls.Add(this.OpenSerialButton);
            this.groupBox.Location = new System.Drawing.Point(14, 18);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(212, 126);
            this.groupBox.TabIndex = 2;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Serial Port";
            // 
            // Baud9600RadioButton
            // 
            this.Baud9600RadioButton.Location = new System.Drawing.Point(110, 40);
            this.Baud9600RadioButton.Name = "Baud9600RadioButton";
            this.Baud9600RadioButton.Size = new System.Drawing.Size(72, 16);
            this.Baud9600RadioButton.TabIndex = 14;
            this.Baud9600RadioButton.Text = "9600";
            this.Baud9600RadioButton.Click += new System.EventHandler(this.Baud9600RadioButton_Click);
            // 
            // Baud115200RadioButton
            // 
            this.Baud115200RadioButton.Checked = true;
            this.Baud115200RadioButton.Location = new System.Drawing.Point(110, 22);
            this.Baud115200RadioButton.Name = "Baud115200RadioButton";
            this.Baud115200RadioButton.Size = new System.Drawing.Size(72, 16);
            this.Baud115200RadioButton.TabIndex = 13;
            this.Baud115200RadioButton.TabStop = true;
            this.Baud115200RadioButton.Text = "115200";
            this.Baud115200RadioButton.Click += new System.EventHandler(this.Baud115200RadioButton_Click);
            // 
            // ComPortTextBox
            // 
            this.ComPortTextBox.Location = new System.Drawing.Point(70, 26);
            this.ComPortTextBox.Name = "ComPortTextBox";
            this.ComPortTextBox.Size = new System.Drawing.Size(28, 20);
            this.ComPortTextBox.TabIndex = 12;
            this.ComPortTextBox.Text = "1";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(12, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 16);
            this.label12.TabIndex = 11;
            this.label12.Text = "Com Port:  ";
            // 
            // CloseSerialButton
            // 
            this.CloseSerialButton.Enabled = false;
            this.CloseSerialButton.Location = new System.Drawing.Point(36, 96);
            this.CloseSerialButton.Name = "CloseSerialButton";
            this.CloseSerialButton.Size = new System.Drawing.Size(144, 23);
            this.CloseSerialButton.TabIndex = 1;
            this.CloseSerialButton.Text = "Close Serial Port";
            this.CloseSerialButton.Click += new System.EventHandler(this.CloseSerialButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OpenConnSpecRdr);
            this.groupBox2.Controls.Add(this.IPTextBox);
            this.groupBox2.Controls.Add(this.IPLabel);
            this.groupBox2.Controls.Add(this.ScanIPButton);
            this.groupBox2.Controls.Add(this.AllIPSRadioButton);
            this.groupBox2.Controls.Add(this.SpecificIPRadioButton);
            this.groupBox2.Controls.Add(this.IPListBox);
            this.groupBox2.Controls.Add(this.CloseSockButton);
            this.groupBox2.Controls.Add(this.OpenConnectButton);
            this.groupBox2.Controls.Add(this.ScanNetworkButton);
            this.groupBox2.Location = new System.Drawing.Point(16, 154);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 282);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Network";
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(54, 116);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.ReadOnly = true;
            this.IPTextBox.Size = new System.Drawing.Size(120, 20);
            this.IPTextBox.TabIndex = 21;
            // 
            // IPLabel
            // 
            this.IPLabel.Location = new System.Drawing.Point(34, 118);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(20, 16);
            this.IPLabel.TabIndex = 20;
            this.IPLabel.Text = "IP:  ";
            // 
            // ScanIPButton
            // 
            this.ScanIPButton.Enabled = false;
            this.ScanIPButton.Location = new System.Drawing.Point(34, 90);
            this.ScanIPButton.Name = "ScanIPButton";
            this.ScanIPButton.Size = new System.Drawing.Size(142, 23);
            this.ScanIPButton.TabIndex = 19;
            this.ScanIPButton.Text = "ScanIP";
            this.ScanIPButton.Click += new System.EventHandler(this.ScanIPButton_Click);
            // 
            // AllIPSRadioButton
            // 
            this.AllIPSRadioButton.Checked = true;
            this.AllIPSRadioButton.Location = new System.Drawing.Point(128, 26);
            this.AllIPSRadioButton.Name = "AllIPSRadioButton";
            this.AllIPSRadioButton.Size = new System.Drawing.Size(54, 24);
            this.AllIPSRadioButton.TabIndex = 18;
            this.AllIPSRadioButton.TabStop = true;
            this.AllIPSRadioButton.Text = "All IPs";
            this.AllIPSRadioButton.Click += new System.EventHandler(this.AllIPSRadioButton_Click);
            // 
            // SpecificIPRadioButton
            // 
            this.SpecificIPRadioButton.Location = new System.Drawing.Point(34, 26);
            this.SpecificIPRadioButton.Name = "SpecificIPRadioButton";
            this.SpecificIPRadioButton.Size = new System.Drawing.Size(76, 24);
            this.SpecificIPRadioButton.TabIndex = 17;
            this.SpecificIPRadioButton.Text = "Specific IP";
            this.SpecificIPRadioButton.CheckedChanged += new System.EventHandler(this.SpecificIPRadioButton_CheckedChanged);
            // 
            // IPListBox
            // 
            this.IPListBox.Location = new System.Drawing.Point(34, 224);
            this.IPListBox.Name = "IPListBox";
            this.IPListBox.Size = new System.Drawing.Size(142, 69);
            this.IPListBox.TabIndex = 4;
            // 
            // CloseSockButton
            // 
            this.CloseSockButton.Location = new System.Drawing.Point(34, 196);
            this.CloseSockButton.Name = "CloseSockButton";
            this.CloseSockButton.Size = new System.Drawing.Size(144, 23);
            this.CloseSockButton.TabIndex = 3;
            this.CloseSockButton.Text = "Close Connection";
            this.CloseSockButton.Click += new System.EventHandler(this.CloseSockButton_Click);
            // 
            // OpenConnectButton
            // 
            this.OpenConnectButton.Location = new System.Drawing.Point(34, 143);
            this.OpenConnectButton.Name = "OpenConnectButton";
            this.OpenConnectButton.Size = new System.Drawing.Size(144, 23);
            this.OpenConnectButton.TabIndex = 2;
            this.OpenConnectButton.Text = "Open Connection";
            this.OpenConnectButton.Click += new System.EventHandler(this.OpenConnectButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ResetRdrButton);
            this.groupBox3.Controls.Add(this.HostIDTextBox);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.ReaderIDTextBox);
            this.groupBox3.Controls.Add(this.BroadcastRdrCheckBox);
            this.groupBox3.Controls.Add(this.QueryRdrButton);
            this.groupBox3.Controls.Add(this.EnableRdrButton);
            this.groupBox3.Location = new System.Drawing.Point(16, 450);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(208, 188);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Reader";
            // 
            // ResetRdrButton
            // 
            this.ResetRdrButton.Location = new System.Drawing.Point(36, 26);
            this.ResetRdrButton.Name = "ResetRdrButton";
            this.ResetRdrButton.Size = new System.Drawing.Size(144, 23);
            this.ResetRdrButton.TabIndex = 9;
            this.ResetRdrButton.Text = "Reset Reader";
            this.ResetRdrButton.Click += new System.EventHandler(this.ResetRdrButton_Click);
            // 
            // HostIDTextBox
            // 
            this.HostIDTextBox.Location = new System.Drawing.Point(78, 158);
            this.HostIDTextBox.Name = "HostIDTextBox";
            this.HostIDTextBox.Size = new System.Drawing.Size(44, 20);
            this.HostIDTextBox.TabIndex = 8;
            this.HostIDTextBox.Text = "1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Host ID: ";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Reader ID: ";
            // 
            // ReaderIDTextBox
            // 
            this.ReaderIDTextBox.Location = new System.Drawing.Point(80, 130);
            this.ReaderIDTextBox.Name = "ReaderIDTextBox";
            this.ReaderIDTextBox.Size = new System.Drawing.Size(42, 20);
            this.ReaderIDTextBox.TabIndex = 5;
            this.ReaderIDTextBox.Text = "2";
            // 
            // BroadcastRdrCheckBox
            // 
            this.BroadcastRdrCheckBox.Checked = true;
            this.BroadcastRdrCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BroadcastRdrCheckBox.Location = new System.Drawing.Point(126, 130);
            this.BroadcastRdrCheckBox.Name = "BroadcastRdrCheckBox";
            this.BroadcastRdrCheckBox.Size = new System.Drawing.Size(74, 24);
            this.BroadcastRdrCheckBox.TabIndex = 4;
            this.BroadcastRdrCheckBox.Text = "Broadcast";
            // 
            // QueryRdrButton
            // 
            this.QueryRdrButton.Location = new System.Drawing.Point(36, 94);
            this.QueryRdrButton.Name = "QueryRdrButton";
            this.QueryRdrButton.Size = new System.Drawing.Size(144, 23);
            this.QueryRdrButton.TabIndex = 3;
            this.QueryRdrButton.Text = "Query Reader";
            this.QueryRdrButton.Click += new System.EventHandler(this.QueryRdrButton_Click);
            // 
            // EnableRdrButton
            // 
            this.EnableRdrButton.Location = new System.Drawing.Point(36, 60);
            this.EnableRdrButton.Name = "EnableRdrButton";
            this.EnableRdrButton.Size = new System.Drawing.Size(144, 23);
            this.EnableRdrButton.TabIndex = 2;
            this.EnableRdrButton.Text = "Enable Reader";
            this.EnableRdrButton.Click += new System.EventHandler(this.EnableRdrButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.GetTagLoggingTimestampButton);
            this.groupBox4.Controls.Add(this.SetTagLoggingTimestampButton);
            this.groupBox4.Controls.Add(this.GetTagTempConfigButton);
            this.groupBox4.Controls.Add(this.GetTagTempButton);
            this.groupBox4.Controls.Add(this.DataTextBox5);
            this.groupBox4.Controls.Add(this.DataTextBox4);
            this.groupBox4.Controls.Add(this.DataTextBox3);
            this.groupBox4.Controls.Add(this.DataTextBox2);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.DataTextBox1);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.LenTextBox);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.AddressTextBox);
            this.groupBox4.Controls.Add(this.WriteTagButton);
            this.groupBox4.Controls.Add(this.ReadTagButton);
            this.groupBox4.Controls.Add(this.LongDelayCheckBox);
            this.groupBox4.Controls.Add(this.AnyTypeCheckBox);
            this.groupBox4.Controls.Add(this.AnyIDCheckBox);
            this.groupBox4.Controls.Add(this.TagIDTextBox);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.TagTypeComboBox);
            this.groupBox4.Controls.Add(this.CallTagButton);
            this.groupBox4.Controls.Add(this.QueryTagButton1);
            this.groupBox4.Controls.Add(this.EnableTagButton);
            this.groupBox4.Location = new System.Drawing.Point(240, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(382, 376);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tag ";
            // 
            // GetTagLoggingTimestampButton
            // 
            this.GetTagLoggingTimestampButton.Location = new System.Drawing.Point(14, 346);
            this.GetTagLoggingTimestampButton.Name = "GetTagLoggingTimestampButton";
            this.GetTagLoggingTimestampButton.Size = new System.Drawing.Size(170, 22);
            this.GetTagLoggingTimestampButton.TabIndex = 31;
            this.GetTagLoggingTimestampButton.Text = "rfGetTagTempLogTimestamp";
            this.GetTagLoggingTimestampButton.Click += new System.EventHandler(this.GetTagLoggingTimestampButton_Click);
            // 
            // SetTagLoggingTimestampButton
            // 
            this.SetTagLoggingTimestampButton.Location = new System.Drawing.Point(14, 318);
            this.SetTagLoggingTimestampButton.Name = "SetTagLoggingTimestampButton";
            this.SetTagLoggingTimestampButton.Size = new System.Drawing.Size(170, 22);
            this.SetTagLoggingTimestampButton.TabIndex = 30;
            this.SetTagLoggingTimestampButton.Text = "rfSetTagTempLogTimestamp";
            this.SetTagLoggingTimestampButton.Click += new System.EventHandler(this.SetTagLoggingTimestampButton_Click);
            // 
            // GetTagTempConfigButton
            // 
            this.GetTagTempConfigButton.Location = new System.Drawing.Point(14, 290);
            this.GetTagTempConfigButton.Name = "GetTagTempConfigButton";
            this.GetTagTempConfigButton.Size = new System.Drawing.Size(144, 22);
            this.GetTagTempConfigButton.TabIndex = 29;
            this.GetTagTempConfigButton.Text = "Get Tag Temp Config";
            this.GetTagTempConfigButton.Click += new System.EventHandler(this.GetTagTempConfigButton_Click);
            // 
            // GetTagTempButton
            // 
            this.GetTagTempButton.Location = new System.Drawing.Point(14, 264);
            this.GetTagTempButton.Name = "GetTagTempButton";
            this.GetTagTempButton.Size = new System.Drawing.Size(144, 22);
            this.GetTagTempButton.TabIndex = 28;
            this.GetTagTempButton.Text = "Get Tag Temp";
            this.GetTagTempButton.Click += new System.EventHandler(this.GetTagTempButton_Click);
            // 
            // DataTextBox5
            // 
            this.DataTextBox5.Location = new System.Drawing.Point(348, 220);
            this.DataTextBox5.Name = "DataTextBox5";
            this.DataTextBox5.Size = new System.Drawing.Size(28, 20);
            this.DataTextBox5.TabIndex = 27;
            // 
            // DataTextBox4
            // 
            this.DataTextBox4.Location = new System.Drawing.Point(312, 220);
            this.DataTextBox4.Name = "DataTextBox4";
            this.DataTextBox4.Size = new System.Drawing.Size(28, 20);
            this.DataTextBox4.TabIndex = 26;
            // 
            // DataTextBox3
            // 
            this.DataTextBox3.Location = new System.Drawing.Point(276, 220);
            this.DataTextBox3.Name = "DataTextBox3";
            this.DataTextBox3.Size = new System.Drawing.Size(28, 20);
            this.DataTextBox3.TabIndex = 25;
            // 
            // DataTextBox2
            // 
            this.DataTextBox2.Location = new System.Drawing.Point(242, 220);
            this.DataTextBox2.Name = "DataTextBox2";
            this.DataTextBox2.Size = new System.Drawing.Size(28, 20);
            this.DataTextBox2.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(286, 242);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 16);
            this.label9.TabIndex = 23;
            this.label9.Text = "Max = 12  Bytes ";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(312, 170);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 16);
            this.label8.TabIndex = 22;
            this.label8.Text = "Max = 12";
            // 
            // DataTextBox1
            // 
            this.DataTextBox1.Location = new System.Drawing.Point(208, 220);
            this.DataTextBox1.Name = "DataTextBox1";
            this.DataTextBox1.Size = new System.Drawing.Size(28, 20);
            this.DataTextBox1.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(170, 222);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "Data: ";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(300, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Len:";
            // 
            // LenTextBox
            // 
            this.LenTextBox.Location = new System.Drawing.Point(330, 188);
            this.LenTextBox.Name = "LenTextBox";
            this.LenTextBox.Size = new System.Drawing.Size(40, 20);
            this.LenTextBox.TabIndex = 18;
            this.LenTextBox.Text = "12";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(168, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Address: ";
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.Location = new System.Drawing.Point(218, 186);
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.Size = new System.Drawing.Size(50, 20);
            this.AddressTextBox.TabIndex = 16;
            this.AddressTextBox.Text = "224";
            // 
            // WriteTagButton
            // 
            this.WriteTagButton.Location = new System.Drawing.Point(14, 218);
            this.WriteTagButton.Name = "WriteTagButton";
            this.WriteTagButton.Size = new System.Drawing.Size(144, 22);
            this.WriteTagButton.TabIndex = 15;
            this.WriteTagButton.Text = "Write Tag";
            this.WriteTagButton.Click += new System.EventHandler(this.WriteTagButton_Click);
            // 
            // ReadTagButton
            // 
            this.ReadTagButton.Location = new System.Drawing.Point(14, 184);
            this.ReadTagButton.Name = "ReadTagButton";
            this.ReadTagButton.Size = new System.Drawing.Size(144, 22);
            this.ReadTagButton.TabIndex = 14;
            this.ReadTagButton.Text = "Read Tag";
            this.ReadTagButton.Click += new System.EventHandler(this.ReadTagButton_Click);
            // 
            // LongDelayCheckBox
            // 
            this.LongDelayCheckBox.Location = new System.Drawing.Point(232, 22);
            this.LongDelayCheckBox.Name = "LongDelayCheckBox";
            this.LongDelayCheckBox.Size = new System.Drawing.Size(92, 24);
            this.LongDelayCheckBox.TabIndex = 13;
            this.LongDelayCheckBox.Text = "Long Delay";
            // 
            // AnyTypeCheckBox
            // 
            this.AnyTypeCheckBox.Location = new System.Drawing.Point(148, 22);
            this.AnyTypeCheckBox.Name = "AnyTypeCheckBox";
            this.AnyTypeCheckBox.Size = new System.Drawing.Size(74, 24);
            this.AnyTypeCheckBox.TabIndex = 12;
            this.AnyTypeCheckBox.Text = "Any Type";
            // 
            // AnyIDCheckBox
            // 
            this.AnyIDCheckBox.Location = new System.Drawing.Point(148, 50);
            this.AnyIDCheckBox.Name = "AnyIDCheckBox";
            this.AnyIDCheckBox.Size = new System.Drawing.Size(64, 24);
            this.AnyIDCheckBox.TabIndex = 11;
            this.AnyIDCheckBox.Text = "Any ID";
            // 
            // TagIDTextBox
            // 
            this.TagIDTextBox.Location = new System.Drawing.Point(64, 52);
            this.TagIDTextBox.Name = "TagIDTextBox";
            this.TagIDTextBox.Size = new System.Drawing.Size(82, 20);
            this.TagIDTextBox.TabIndex = 10;
            this.TagIDTextBox.Text = "100";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(18, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Tag ID:  ";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tag Type:  ";
            // 
            // TagTypeComboBox
            // 
            this.TagTypeComboBox.Items.AddRange(new object[] {
            "Access",
            "Asset",
            "Inventory",
            "Factory"});
            this.TagTypeComboBox.Location = new System.Drawing.Point(72, 22);
            this.TagTypeComboBox.Name = "TagTypeComboBox";
            this.TagTypeComboBox.Size = new System.Drawing.Size(74, 21);
            this.TagTypeComboBox.TabIndex = 6;
            this.TagTypeComboBox.Text = "Access";
            // 
            // CallTagButton
            // 
            this.CallTagButton.Location = new System.Drawing.Point(14, 150);
            this.CallTagButton.Name = "CallTagButton";
            this.CallTagButton.Size = new System.Drawing.Size(144, 22);
            this.CallTagButton.TabIndex = 5;
            this.CallTagButton.Text = "Call Tag";
            this.CallTagButton.Click += new System.EventHandler(this.CallTagButton_Click);
            // 
            // QueryTagButton1
            // 
            this.QueryTagButton1.Location = new System.Drawing.Point(14, 118);
            this.QueryTagButton1.Name = "QueryTagButton1";
            this.QueryTagButton1.Size = new System.Drawing.Size(144, 22);
            this.QueryTagButton1.TabIndex = 4;
            this.QueryTagButton1.Text = "Query Tag";
            this.QueryTagButton1.Click += new System.EventHandler(this.QueryTagButton1_Click);
            // 
            // EnableTagButton
            // 
            this.EnableTagButton.Location = new System.Drawing.Point(14, 86);
            this.EnableTagButton.Name = "EnableTagButton";
            this.EnableTagButton.Size = new System.Drawing.Size(144, 22);
            this.EnableTagButton.TabIndex = 2;
            this.EnableTagButton.Text = "Enable Tag";
            this.EnableTagButton.Click += new System.EventHandler(this.EnableTagButton_Click);
            // 
            // MsgListBox
            // 
            this.MsgListBox.Location = new System.Drawing.Point(242, 398);
            this.MsgListBox.Name = "MsgListBox";
            this.MsgListBox.Size = new System.Drawing.Size(594, 212);
            this.MsgListBox.TabIndex = 6;
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(466, 618);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 7;
            this.ClearButton.Text = "Clear List";
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.RespondAnyRadioButton);
            this.groupBox5.Controls.Add(this.RespondSpecRadioButton);
            this.groupBox5.Controls.Add(this.BroadcastFGenCheckBox);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.QuerySmartFGenButton);
            this.groupBox5.Controls.Add(this.SmartFGenTextBox);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.ResetSmartFGenbutton);
            this.groupBox5.Controls.Add(this.QuerySTDFGenbutton);
            this.groupBox5.Location = new System.Drawing.Point(632, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(202, 374);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Field Generator ";
            // 
            // RespondAnyRadioButton
            // 
            this.RespondAnyRadioButton.Checked = true;
            this.RespondAnyRadioButton.Enabled = false;
            this.RespondAnyRadioButton.Location = new System.Drawing.Point(52, 272);
            this.RespondAnyRadioButton.Name = "RespondAnyRadioButton";
            this.RespondAnyRadioButton.Size = new System.Drawing.Size(142, 24);
            this.RespondAnyRadioButton.TabIndex = 26;
            this.RespondAnyRadioButton.TabStop = true;
            this.RespondAnyRadioButton.Text = "Respond to any rdr";
            // 
            // RespondSpecRadioButton
            // 
            this.RespondSpecRadioButton.Enabled = false;
            this.RespondSpecRadioButton.Location = new System.Drawing.Point(52, 246);
            this.RespondSpecRadioButton.Name = "RespondSpecRadioButton";
            this.RespondSpecRadioButton.Size = new System.Drawing.Size(142, 24);
            this.RespondSpecRadioButton.TabIndex = 25;
            this.RespondSpecRadioButton.Text = "Respond to specific rdr";
            // 
            // BroadcastFGenCheckBox
            // 
            this.BroadcastFGenCheckBox.Location = new System.Drawing.Point(30, 218);
            this.BroadcastFGenCheckBox.Name = "BroadcastFGenCheckBox";
            this.BroadcastFGenCheckBox.Size = new System.Drawing.Size(164, 24);
            this.BroadcastFGenCheckBox.TabIndex = 24;
            this.BroadcastFGenCheckBox.Text = "Broadcast to all smart fgen";
            this.BroadcastFGenCheckBox.CheckedChanged += new System.EventHandler(this.BroadcastFGenCheckBox_CheckedChanged);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(48, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 22);
            this.label11.TabIndex = 23;
            this.label11.Text = "SMART FGEN";
            // 
            // QuerySmartFGenButton
            // 
            this.QuerySmartFGenButton.Location = new System.Drawing.Point(29, 182);
            this.QuerySmartFGenButton.Name = "QuerySmartFGenButton";
            this.QuerySmartFGenButton.Size = new System.Drawing.Size(144, 23);
            this.QuerySmartFGenButton.TabIndex = 13;
            this.QuerySmartFGenButton.Text = "Query Smart FGen";
            this.QuerySmartFGenButton.Click += new System.EventHandler(this.QuerySmartFGenButton_Click);
            // 
            // SmartFGenTextBox
            // 
            this.SmartFGenTextBox.Location = new System.Drawing.Point(102, 26);
            this.SmartFGenTextBox.Name = "SmartFGenTextBox";
            this.SmartFGenTextBox.Size = new System.Drawing.Size(52, 20);
            this.SmartFGenTextBox.TabIndex = 12;
            this.SmartFGenTextBox.Text = "3";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(42, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 16);
            this.label10.TabIndex = 11;
            this.label10.Text = "FGen ID:  ";
            // 
            // ResetSmartFGenbutton
            // 
            this.ResetSmartFGenbutton.Location = new System.Drawing.Point(30, 152);
            this.ResetSmartFGenbutton.Name = "ResetSmartFGenbutton";
            this.ResetSmartFGenbutton.Size = new System.Drawing.Size(144, 23);
            this.ResetSmartFGenbutton.TabIndex = 1;
            this.ResetSmartFGenbutton.Text = "Reset Smart FGen";
            this.ResetSmartFGenbutton.Click += new System.EventHandler(this.ResetSmartFGenbutton_Click);
            // 
            // QuerySTDFGenbutton
            // 
            this.QuerySTDFGenbutton.Enabled = false;
            this.QuerySTDFGenbutton.Location = new System.Drawing.Point(30, 64);
            this.QuerySTDFGenbutton.Name = "QuerySTDFGenbutton";
            this.QuerySTDFGenbutton.Size = new System.Drawing.Size(144, 23);
            this.QuerySTDFGenbutton.TabIndex = 0;
            this.QuerySTDFGenbutton.Text = "Query STD FGen";
            this.QuerySTDFGenbutton.Click += new System.EventHandler(this.QuerySTDFGenbutton_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // OpenConnSpecRdr
            // 
            this.OpenConnSpecRdr.Location = new System.Drawing.Point(34, 167);
            this.OpenConnSpecRdr.Name = "OpenConnSpecRdr";
            this.OpenConnSpecRdr.Size = new System.Drawing.Size(144, 23);
            this.OpenConnSpecRdr.TabIndex = 22;
            this.OpenConnSpecRdr.Text = "Open Conn Specific Rdr";
            this.OpenConnSpecRdr.Click += new System.EventHandler(this.OpenConnSpecRdr_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(844, 648);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.MsgListBox);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Test API NET - CSharp  V8.44";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void OpenSerialButton_Click(object sender, System.EventArgs e)
		{
			int ret = 0;
			string s;

			uint com = Convert.ToUInt16(ComPortTextBox.Text);
			uint baud;
			if (Baud115200RadioButton.Checked)
				baud = 115200;
			else
			   baud = 9600;

			if (!rdrEventRegistered)
			{
			    //Create new function to handle all reader events and pass it to API
			    AW_API_NET.fReaderEvent rdrEventHandler = new AW_API_NET.fReaderEvent(ReaderEvent); 
			
			    //Register Reader event for callbacks
			    ret = apiClass.rfRegisterReaderEvent(rdrEventHandler);
		   
			    s = Convert.ToString(item);
			    s += " - Reader events callback function registered";
                addMsgQ(s);
				rdrEventRegistered = true;
			}

			//Create new function to handle all tag events and pass it to API
			AW_API_NET.fTagEvent tagEventHandler = new AW_API_NET.fTagEvent(TagEvent); 
			
			//Register Tag event for callbacks
			ret = apiClass.rfRegisterTagEvent(tagEventHandler);

			s = Convert.ToString(item);
            s += " - Tag events callback function registered";
            addMsgQ(s);

			//Open serial port
			ret = apiClass.rfOpen(baud, com);
			s = Convert.ToString(item);
			s += " - Open Serial Port - ret Code = ";
			s += ret;
            addMsgQ(s);
            if (ret == 0)
			{
				CloseSerialButton.Enabled = true;
			    OpenSerialButton.Enabled = false;
			}
		}

		private bool FindIP(string ip)
		{
			for (int i=0; i<IPListBox.Items.Count; i++)
			{
				if (IPListBox.Items[i].ToString() == ip)
					return (true);
			}

			return (false);
		}
		private void CloseSerialButton_Click(object sender, System.EventArgs e)
		{
			//Close serial port
			int ret = apiClass.rfClose();
			string s = Convert.ToString(item);
			s += " - Close Serial Port - ret Code = ";
			s += ret;
            addMsgQ(s);
            OpenSerialButton.Enabled = true;
		}

		private void ResetRdrButton_Click(object sender, System.EventArgs e)
		{
			//Reset reader
			ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);

			int ret = 0;
			if (BroadcastRdrCheckBox.Checked)
				ret = apiClass.rfResetReader(host, rdr, 0, AW_API_NET.APIConsts.ALL_READERS, ++pktID);
			else
	            ret = apiClass.rfResetReader(host, rdr, 0, AW_API_NET.APIConsts.SPECIFIC_READER, ++pktID);

			string s = Convert.ToString(item);
			s += " - Reset Reader - ret Code = ";
			s += ret;
            addMsgQ(s);
        }

		private void ClearButton_Click(object sender, System.EventArgs e)
		{
		   item = 0;
		   MsgListBox.Items.Clear();
		}

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

		private void EnableRdrButton_Click(object sender, System.EventArgs e)
		{
			ushort cmdType = 0;
			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;
		    ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
		    int ret = apiClass.rfEnableReader(host, rdr, 0, true, cmdType, ++pktID);
			string s = Convert.ToString(item);
			s += " - Enable Reader - ret Code = ";
			s += ret;
            addMsgQ(s);
        }

		private void ScanNetworkButton_Click(object sender, System.EventArgs e)
		{
			int ret = 0;
			string s;

			if (!rdrEventRegistered)
			{
			    //Create new function to handle all reader events and pass it to API
			    AW_API_NET.fReaderEvent rdrEventHandler = new AW_API_NET.fReaderEvent(ReaderEvent); 
			
			    //Register Reader event for callbacks
			    ret = apiClass.rfRegisterReaderEvent(rdrEventHandler);
		   
			    s = Convert.ToString(item);
			    s += " - Reader events callback function registered";
                addMsgQ(s);

				//Create new function to handle all tag events and pass it to API
			    AW_API_NET.fTagEvent tagEventHandler = new AW_API_NET.fTagEvent(TagEvent); 
			
			    //Register tag event for callbacks
			    ret = apiClass.rfRegisterTagEvent(tagEventHandler);
		   
			    s = Convert.ToString(item);
			    s += " - Tag events callback function registered";
                addMsgQ(s);
            }

			IPListBox.Items.Clear();
		    ret = apiClass.rfScanNetwork(++pktID);
			s = Convert.ToString(item);
			s += " - ScanNetwork - ret Code = ";
			s += ret;
            addMsgQ(s);
        }

		private void OpenConnectButton_Click(object sender, System.EventArgs e)
		{
			string msg;
			string s;
			int ret = 0;
			char[] buf = new char[20];
			byte[] ip = new byte[20]; 

			ushort host = Convert.ToUInt16(HostIDTextBox.Text);

			if (AllIPSRadioButton.Checked)
			{
				if (IPListBox.Items.Count > 0)
				{
					msg = Convert.ToString(item);
					msg += " - Open Socket Connections";
                    addMsgQ(msg);
                }

				for (int i=0; i<IPListBox.Items.Count; i++)
				{
					s = IPListBox.Items[i].ToString();
					buf = s.ToCharArray(0, s.Length);
					for (int j=0; j<s.Length-1; j++)
						ip[i] = Convert.ToByte(buf[i]);
					ret = apiClass.rfOpenSocket(ip, host, false, AW_API_NET.APIConsts.ALL_IPS, ++pktID);
				}
			}
			else
			{
				if (IPTextBox.Text.Length > 0)
				{
					s = IPTextBox.Text;
					buf = s.ToCharArray(0, s.Length);
					for (int i=0; i<s.Length; i++)
						ip[i] = Convert.ToByte(buf[i]);
					ret = apiClass.rfOpenSocket(ip, host, false, AW_API_NET.APIConsts.SPECIFIC_IP, ++pktID);
					msg = Convert.ToString(item);
					msg += " - Open Socket Connection";
					msg += "  Return Code = " + ret.ToString();
                    addMsgQ(msg);
                }
				else
				{
                    addMsgQ("Need Specific IP");
                }
			}
		}

        private void OpenConnSpecRdr_Click(object sender, EventArgs e)
        {
			string msg;
			string s;
			int ret = 0;
			char[] buf = new char[20];
			byte[] ip = new byte[20]; 

			ushort host = Convert.ToUInt16(HostIDTextBox.Text);
            ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
            if (IPTextBox.Text.Length > 0) {
                s = IPTextBox.Text;
                buf = s.ToCharArray(0, s.Length);
                for (int i = 0; i < s.Length; i++)
                    ip[i] = Convert.ToByte(buf[i]);
                ret = apiClass.rfOpenSocketRdr(ip, host, false, AW_API_NET.APIConsts.SPECIFIC_IP, rdr, ++pktID);
                msg = string.Format("{0} - Open Socket Connection Specific reader ({1}). Return code: {2}", item, rdr, ret.ToString());
                addMsgQ(msg);
            } else {
                addMsgQ("Need Specific IP");
            }
        }


		private void CloseSockButton_Click(object sender, System.EventArgs e)
		{
			string s;
			string msg = "";
			int ret = 0;
			char[] buf = new char[20];
			byte[] ip = new byte[20];

			if (IPListBox.Items.Count > 0)
			{
				msg = Convert.ToString(item);
				msg += " - Close Connection";
			}

			if (AllIPSRadioButton.Checked)
			{
				for (int i=0; i<IPListBox.Items.Count; i++)
				{
					s = IPListBox.Items[i].ToString();
					buf = s.ToCharArray(0, s.Length);
					for (int j=0; j<s.Length-1; j++)
						ip[i] = Convert.ToByte(buf[i]);
					apiClass.rfCloseSocket(ip, AW_API_NET.APIConsts.ALL_IPS);
					msg += "  Return Code = " + ret.ToString();
                    addMsgQ(msg);
                }

				IPListBox.Items.Clear();
			}
			else
			{
				if (IPTextBox.Text.Length > 0)
				{
					s = IPTextBox.Text;
					buf = s.ToCharArray(0, s.Length);
					for (int i=0; i<s.Length; i++)
						ip[i] = Convert.ToByte(buf[i]);
					ret = apiClass.rfCloseSocket(ip, AW_API_NET.APIConsts.SPECIFIC_IP);
					msg += "  Return Code = " + ret.ToString();
                    addMsgQ(msg);
                    int indx = 0;
					if ((indx=IPListBox.FindStringExact(s)) >= 0)
						IPListBox.Items.RemoveAt(indx);
				}
				else
				{
                    addMsgQ("Need Specific IP");
                }
			}
		}

		private void EnableTagButton_Click(object sender, System.EventArgs e)
		{
		    ushort cmdType = 0;
		    ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
		    bool setTxTimeInt = false;
		    bool timeInt = false;
			uint tagID = 0;

			//rfTagSelect_t tagSelect = new rfTagSelect_t();
 
			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;

			if ((AnyTypeCheckBox.Checked) && (AnyIDCheckBox.Checked))
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_FIELD;
			else if (AnyTypeCheckBox.Checked)
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE;
			else
			{
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				tagID = Convert.ToUInt32(TagIDTextBox.Text);
			}

			if (TagTypeComboBox.Text == "Access")
				tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
			else if (TagTypeComboBox.Text == "Asset")
				tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
			else if (TagTypeComboBox.Text == "Inventory")
				tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
			else 
				tagSelect.tagType = AW_API_NET.APIConsts.FACTORY_TAG;

			tagSelect.tagList[0] = tagID;
			tagSelect.numTags = 1;
			tagSelect.ledOn = true;
			tagSelect.speakerOn =true;

			if(LongDelayCheckBox.Checked)
			{
				setTxTimeInt = true;
				timeInt = true; //long
			}
			else 
			{
				setTxTimeInt = true;
				timeInt = false; //short
			}

		    int ret = apiClass.rfEnableTags(host, rdr, 0, tagSelect, true, setTxTimeInt, timeInt, cmdType, ++pktID);
		    string s = Convert.ToString(item);
		    s += " - Enable Tags - ret Code = ";
		    s += ret;
			s += "  pktID = ";
		    s += pktID - 1;
            addMsgQ(s);

	    }

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
		
		}

		private void QueryRdrButton_Click(object sender, System.EventArgs e)
		{
			ushort cmdType = 0;
			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;
		    ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
		    int ret = apiClass.rfQueryReader(host, rdr, 0, cmdType, ++pktID);
			string s = Convert.ToString(item);
			s += " - Query Reader - ret Code = ";
			s += ret;
            addMsgQ(s);
        }

		private void QueryTagButton1_Click(object sender, System.EventArgs e)
		{
		    ushort cmdType = 0;
		    ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
		    bool setTxTimeInt = false;
		    bool timeInt = false;
			uint tagID = 0;

			AW_API_NET.rfTagSelect_t tagSelect = new AW_API_NET.rfTagSelect_t();
 
			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;

			if ((AnyTypeCheckBox.Checked) && (AnyIDCheckBox.Checked))
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_FIELD;
			else if (AnyTypeCheckBox.Checked)
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE;
			else
			{
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				tagID = Convert.ToUInt32(TagIDTextBox.Text);
			}

			if (TagTypeComboBox.Text == "Access")
				tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
			else if (TagTypeComboBox.Text == "Asset")
				tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
			else if (TagTypeComboBox.Text == "Inventory")
				tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
			else 
				tagSelect.tagType = AW_API_NET.APIConsts.FACTORY_TAG;

			tagSelect.tagList = new uint[50];
			tagSelect.tagList[0] = tagID;
			tagSelect.numTags = 1;

			if(LongDelayCheckBox.Checked)
			{
				setTxTimeInt = true;
				timeInt = true; //long
			}
			else 
			{
				setTxTimeInt = true;
				timeInt = false; //short
			}

		    int ret = apiClass.rfQueryTags(host, rdr, 0, ref tagSelect, setTxTimeInt, timeInt, cmdType, ++pktID);
		    string s = Convert.ToString(item);
		    s += " - Query Tags - ret Code = ";
		    s += ret;
			s += "  pktID = ";
		    s += pktID - 1;
            addMsgQ(s);
        }

		private void CallTagButton_Click(object sender, System.EventArgs e)
		{
		    ushort cmdType = 0;
		    ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
		    bool setTxTimeInt = false;
		    bool timeInt = false;
			uint tagID = 0;

			AW_API_NET.rfTagSelect_t tagSelect = new AW_API_NET.rfTagSelect_t();
 
			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;

			if ((AnyTypeCheckBox.Checked) && (AnyIDCheckBox.Checked))
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_FIELD;
			else if (AnyTypeCheckBox.Checked)
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE;
			else
			{
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				tagID = Convert.ToUInt32(TagIDTextBox.Text);
			}

			if (TagTypeComboBox.Text == "Access")
				tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
			else if (TagTypeComboBox.Text == "Asset")
				tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
			else if (TagTypeComboBox.Text == "Inventory")
				tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
			else 
				tagSelect.tagType = AW_API_NET.APIConsts.FACTORY_TAG;

			tagSelect.tagList = new uint[50];
			tagSelect.tagList[0] = tagID;
			tagSelect.numTags = 1;

			if(LongDelayCheckBox.Checked)
			{
				setTxTimeInt = true;
				timeInt = true; //long
			}
			else 
			{
				setTxTimeInt = true;
				timeInt = false; //short
			}

		    int ret = apiClass.rfCallTags(host, rdr, 0, 0, ref tagSelect, setTxTimeInt, timeInt, cmdType, ++pktID);
		    string s = Convert.ToString(item);
		    s += " - Call Tags - ret Code = ";
		    s += ret;
			s += "  pktID = ";
		    s += pktID - 1;
            addMsgQ(s);
        }

		private void ReadTagButton_Click(object sender, System.EventArgs e)
		{
		    ushort cmdType = 0;
		    ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
			uint addr = Convert.ToUInt32(AddressTextBox.Text); 
			ushort len = Convert.ToUInt16(LenTextBox.Text);
		    bool setTxTimeInt = false;
		    bool timeInt = false;
			uint tagID = 0;

			AW_API_NET.rfTagSelect_t tagSelect = new AW_API_NET.rfTagSelect_t();
 
			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;

			if ((AnyTypeCheckBox.Checked) && (AnyIDCheckBox.Checked))
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_FIELD;
			else if (AnyTypeCheckBox.Checked)
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE;
			else
			{
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				tagID = Convert.ToUInt32(TagIDTextBox.Text);
			}

			if (TagTypeComboBox.Text == "Access")
				tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
			else if (TagTypeComboBox.Text == "Asset")
				tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
			else if (TagTypeComboBox.Text == "Inventory")
				tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
			else 
				tagSelect.tagType = AW_API_NET.APIConsts.FACTORY_TAG;

			tagSelect.tagList = new uint[50];
			tagSelect.tagList[0] = tagID;
			tagSelect.numTags = 1;

			if(LongDelayCheckBox.Checked)
			{
				setTxTimeInt = true;
				timeInt = true; //long
			}
			else 
			{
				setTxTimeInt = true;
				timeInt = false; //short
			}

		    int ret = apiClass.rfReadTags(host, rdr, 0, ref tagSelect, addr, len, setTxTimeInt, timeInt, cmdType, ++pktID);
		    string s = Convert.ToString(item);
		    s += " - Read Tags - ret Code = ";
		    s += ret;
			s += "  pktID = ";
		    s += pktID - 1;
            addMsgQ(s);
        }

		private void WriteTagButton_Click(object sender, System.EventArgs e)
		{
		    ushort cmdType = 0;
		    ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
			uint addr = Convert.ToUInt32(AddressTextBox.Text); 
			ushort len = Convert.ToUInt16(LenTextBox.Text);
		    bool setTxTimeInt = false;
		    bool timeInt = false;
			uint tagID = 0;
			byte[] data = new byte[12];

			AW_API_NET.rfTagSelect_t tagSelect = new AW_API_NET.rfTagSelect_t();
 
			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;

			if ((AnyTypeCheckBox.Checked) && (AnyIDCheckBox.Checked))
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_FIELD;
			else if (AnyTypeCheckBox.Checked)
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE;
			else
			{
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				tagID = Convert.ToUInt32(TagIDTextBox.Text);
			}

			if (TagTypeComboBox.Text == "Access")
				tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
			else if (TagTypeComboBox.Text == "Asset")
				tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
			else if (TagTypeComboBox.Text == "Inventory")
				tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
			else 
				tagSelect.tagType = AW_API_NET.APIConsts.FACTORY_TAG;

			tagSelect.tagList = new uint[50];
			tagSelect.tagList[0] = tagID;
			tagSelect.numTags = 1;

			if(LongDelayCheckBox.Checked)
			{
				setTxTimeInt = true;
				timeInt = true; //long
			}
			else 
			{
				setTxTimeInt = true;
				timeInt = false; //short
			}

            if (len > 0)
                data[0] = Convert.ToByte(DataTextBox1.Text);

            if (len > 1)
                data[1] = Convert.ToByte(DataTextBox2.Text);

            if (len > 2)
                data[2] = Convert.ToByte(DataTextBox3.Text);

            if (len > 3)
                data[3] = Convert.ToByte(DataTextBox4.Text);

            if (len > 4)
                data[4] = Convert.ToByte(DataTextBox5.Text);

		    int ret = apiClass.rfWriteTags(host, rdr, 0, ref tagSelect, addr, len, data, setTxTimeInt, timeInt, cmdType, ++pktID);
		    string s = Convert.ToString(item);
		    s += " - Write Tags - ret Code = ";
		    s += ret;
			s += "  pktID = ";
		    s += pktID - 1;
            addMsgQ(s);
        }

		private void BroadcastFGenCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
		   if (BroadcastFGenCheckBox.Checked)
		   {
			  RespondSpecRadioButton.Enabled = true;
			  RespondAnyRadioButton.Enabled = true;
		   }
		   else
		   {
			  RespondSpecRadioButton.Enabled = false;
			  RespondAnyRadioButton.Enabled = false;
		   }
		}

		private void Baud9600RadioButton_Click(object sender, System.EventArgs e)
		{
			QuerySTDFGenbutton.Enabled = true;
			ResetRdrButton.Enabled = false;
			EnableRdrButton.Enabled = false;
			QueryRdrButton.Enabled = false;
			EnableTagButton.Enabled = false;
			QueryTagButton1.Enabled = false;
			CallTagButton.Enabled = false;
			ReadTagButton.Enabled = false;
			WriteTagButton.Enabled = false;
			ResetSmartFGenbutton.Enabled = false;
			QuerySmartFGenButton.Enabled = false;
		}

		private void Baud115200RadioButton_Click(object sender, System.EventArgs e)
		{
		   QuerySTDFGenbutton.Enabled = false;
		   ResetRdrButton.Enabled = true;
			EnableRdrButton.Enabled = true;
			QueryRdrButton.Enabled = true;
			EnableTagButton.Enabled = true;
			QueryTagButton1.Enabled = true;
			CallTagButton.Enabled = true;
			ReadTagButton.Enabled = true;
			WriteTagButton.Enabled = true;
			ResetSmartFGenbutton.Enabled = true;
			QuerySmartFGenButton.Enabled = true;
		}

		private void QuerySTDFGenbutton_Click(object sender, System.EventArgs e)
		{
			ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort fgen = Convert.ToUInt16(SmartFGenTextBox.Text);
		    int ret = apiClass.rfQuerySTDFGen(host, fgen, ++pktID);
			string s = Convert.ToString(item);
		    s += " - Query STD FGen - ret Code = ";
		    s += ret;
			s += "  pktID = ";
		    s += pktID - 1;
            addMsgQ(s);
        }

		private void ResetSmartFGenbutton_Click(object sender, System.EventArgs e)
		{
		    ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
			ushort fgen = Convert.ToUInt16(SmartFGenTextBox.Text);
			ushort cmdType = 0;
			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;
		    int ret = apiClass.rfResetSmartFGen(host, rdr, 0, fgen, cmdType, false, ++pktID);
			
			string s = Convert.ToString(item);
		    s += " - Reset Smart FGen - ret Code = ";
		    s += ret;
			s += "  pktID = ";
		    s += pktID - 1;
            addMsgQ(s);
        }

		private void QuerySmartFGenButton_Click(object sender, System.EventArgs e)
		{
		    ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
			ushort fgen = Convert.ToUInt16(SmartFGenTextBox.Text);
			ushort cmdType = 0;
			ushort bcastType = (ushort)AW_API_NET.APIConsts.RESPOND_SPEC_RDR;
			bool bcast = false;

			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;

			if (BroadcastFGenCheckBox.Checked)
            {
			    bcast = true;
                if (RespondSpecRadioButton.Checked)
                   bcastType = (ushort)AW_API_NET.APIConsts.RESPOND_SPEC_RDR;
                else
                   bcastType = (ushort)AW_API_NET.APIConsts.RESPOND_ANY_RDR;
			}
         
		    int ret = apiClass.rfQuerySmartFGen(host, rdr, 0, fgen, cmdType, bcast, bcastType, ++pktID);
			
			string s = Convert.ToString(item);
		    s += " - Query Smart FGen - ret Code = ";
		    s += ret;
			s += "  pktID = ";
		    s += pktID - 1;
            addMsgQ(s);
        }

		private void GetTagTempButton_Click(object sender, System.EventArgs e)
		{
		    ushort cmdType = 0;
		    ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
		    bool setTxTimeInt = false;
		    bool timeInt = false;
			uint tagID = 0;

			AW_API_NET.rfTagSelect_t tagSelect = new AW_API_NET.rfTagSelect_t();
 
			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;

			if ((AnyTypeCheckBox.Checked) && (AnyIDCheckBox.Checked))
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_FIELD;
			else if (AnyTypeCheckBox.Checked)
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE;
			else
			{
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				tagID = Convert.ToUInt32(TagIDTextBox.Text);
			}

			if (TagTypeComboBox.Text == "Access")
				tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
			else if (TagTypeComboBox.Text == "Asset")
				tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
			else if (TagTypeComboBox.Text == "Inventory")
				tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
			else 
				tagSelect.tagType = AW_API_NET.APIConsts.FACTORY_TAG;

			tagSelect.tagList = new uint[50];
			tagSelect.tagList[0] = tagID;
			tagSelect.numTags = 1;

			if(LongDelayCheckBox.Checked)
			{
				setTxTimeInt = true;
				timeInt = true; //long
			}
			else 
			{
				setTxTimeInt = true;
				timeInt = false; //short
			}

		    int ret = apiClass.rfGetTagTemp(host, rdr, 0, tagSelect, setTxTimeInt, timeInt, cmdType, ++pktID);
		    string s = Convert.ToString(item);
		    s += " - Get Tags Temp - ret Code = ";
		    s += ret;
			s += "  pktID = ";
		    s += pktID - 1;
            addMsgQ(s);
        }

		private void GetTagTempConfigButton_Click(object sender, System.EventArgs e)
		{
		    ushort cmdType = 0;
		    ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
		    bool setTxTimeInt = false;
		    bool timeInt = false;
			uint tagID = 0;

			AW_API_NET.rfTagSelect_t tagSelect = new AW_API_NET.rfTagSelect_t();
 
			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;

			if ((AnyTypeCheckBox.Checked) && (AnyIDCheckBox.Checked))
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_FIELD;
			else if (AnyTypeCheckBox.Checked)
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE;
			else
			{
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				tagID = Convert.ToUInt32(TagIDTextBox.Text);
			}

			if (TagTypeComboBox.Text == "Access")
				tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
			else if (TagTypeComboBox.Text == "Asset")
				tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
			else if (TagTypeComboBox.Text == "Inventory")
				tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
			else 
				tagSelect.tagType = AW_API_NET.APIConsts.FACTORY_TAG;

			tagSelect.tagList = new uint[50];
			tagSelect.tagList[0] = tagID;
			tagSelect.numTags = 1;

			if(LongDelayCheckBox.Checked)
			{
				setTxTimeInt = true;
				timeInt = true; //long
			}
			else 
			{
				setTxTimeInt = true;
				timeInt = false; //short
			}

		    int ret = apiClass.rfGetTagTempConfig(host, rdr, 0, tagSelect, setTxTimeInt, timeInt, cmdType, ++pktID);
		    string s = Convert.ToString(item);
		    s += " - Get Tags Temp Config - ret Code = ";
		    s += ret;
			s += "  pktID = ";
		    s += pktID - 1;
            addMsgQ(s);
        }

		private void AllIPSRadioButton_Click(object sender, System.EventArgs e)
		{
			if (AllIPSRadioButton.Checked)
			{
				IPTextBox.ReadOnly = true;
				ScanIPButton.Enabled = false;
				ScanNetworkButton.Enabled = true;
			}
			else
			{
				IPTextBox.ReadOnly = false;
				ScanIPButton.Enabled = true;
				ScanNetworkButton.Enabled = false;
			}
		}

		private void SpecificIPRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			if (SpecificIPRadioButton.Checked)
			{
				IPTextBox.ReadOnly = false;
				ScanIPButton.Enabled = true;
				ScanNetworkButton.Enabled = false;
			}
			else
			{
				IPTextBox.ReadOnly = true;
				ScanIPButton.Enabled = false;
				ScanNetworkButton.Enabled = true;
			}
		}

		public void RegisterAPIEvents()
		{
			int ret = 0;
			string s;

			if (!rdrEventRegistered)
			{
				//Create new function to handle all reader events and pass it to API
				AW_API_NET.fReaderEvent rdrEventHandler = new AW_API_NET.fReaderEvent(ReaderEvent); 
							
				//Register Reader event for callbacks
				ret = apiClass.rfRegisterReaderEvent(rdrEventHandler);
						   
				s = Convert.ToString(item);
				s += " - Reader events callback function registered";
                addMsgQ(s);
                rdrEventRegistered = true;

				//Create new function to handle all tag events and pass it to API
				AW_API_NET.fTagEvent tagEventHandler = new AW_API_NET.fTagEvent(TagEvent); 
							
				//Register tag event for callbacks
				ret = apiClass.rfRegisterTagEvent(tagEventHandler);
						   
				s = Convert.ToString(item);
				s += " - Tag events callback function registered";
                addMsgQ(s);
            }
		}

		private void ScanIPButton_Click(object sender, System.EventArgs e)
		{
			Byte[] ip = new Byte[20];
			char[] cIP = new char[20];
			string s;

			RegisterAPIEvents();

			string ipStr = IPTextBox.Text; 
			cIP = ipStr.ToCharArray(0, ipStr.Length);
			for (int i=0; i<ipStr.Length; i++)
				ip[i] = Convert.ToByte(cIP[i]);
			int ret = apiClass.rfScanIP(ip, ++pktID);
			s = Convert.ToString(item);
			s += " - ScanIP - ret Code = ";
			s += ret;
            addMsgQ(s);
        }

		private void SetTagLoggingTimestampButton_Click(object sender, System.EventArgs e)
		{
			ushort cmdType = 0;
			ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
			bool setTxTimeInt = false;
			bool timeInt = false;
			uint tagID = 0;

			AW_API_NET.rfTagSelect_t tagSelect = new AW_API_NET.rfTagSelect_t();
 
			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;

			if ((AnyTypeCheckBox.Checked) && (AnyIDCheckBox.Checked))
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_FIELD;
			else if (AnyTypeCheckBox.Checked)
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE;
			else
			{
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				tagID = Convert.ToUInt32(TagIDTextBox.Text);
			}

			if (TagTypeComboBox.Text == "Access")
				tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
			else if (TagTypeComboBox.Text == "Asset")
				tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
			else if (TagTypeComboBox.Text == "Inventory")
				tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
			else 
				tagSelect.tagType = AW_API_NET.APIConsts.FACTORY_TAG;

			tagSelect.tagList = new uint[50];
			tagSelect.tagList[0] = tagID;
			tagSelect.numTags = 1;

			if(LongDelayCheckBox.Checked)
			{
				setTxTimeInt = true;
				timeInt = true; //long
			}
			else 
			{
				setTxTimeInt = true;
				timeInt = false; //short
			}

			int ret = apiClass.rfSetTagTempLogTimestamp(host, rdr, 0, tagSelect, setTxTimeInt, timeInt, cmdType, ++pktID);
			string s = Convert.ToString(item);
			s += " - Set Tags Temp Logging Timestamp - ret Code = ";
			s += ret;
			s += "  pktID = ";
			s += pktID - 1;
            addMsgQ(s);
        }

		private void GetTagLoggingTimestampButton_Click(object sender, System.EventArgs e)
		{
			ushort cmdType = 0;
			ushort host = Convert.ToUInt16(HostIDTextBox.Text);
			ushort rdr = Convert.ToUInt16(ReaderIDTextBox.Text);
			bool setTxTimeInt = false;
			bool timeInt = false;
			uint tagID = 0;

			AW_API_NET.rfTagSelect_t tagSelect = new AW_API_NET.rfTagSelect_t();
 
			if (BroadcastRdrCheckBox.Checked)
				cmdType =  AW_API_NET.APIConsts.ALL_READERS;
			else
				cmdType = AW_API_NET.APIConsts.SPECIFIC_READER;

			if ((AnyTypeCheckBox.Checked) && (AnyIDCheckBox.Checked))
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_FIELD;
			else if (AnyTypeCheckBox.Checked)
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE;
			else
			{
				tagSelect.selectType = AW_API_NET.APIConsts.RF_SELECT_TAG_ID;
				tagID = Convert.ToUInt32(TagIDTextBox.Text);
			}

			if (TagTypeComboBox.Text == "Access")
				tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG;
			else if (TagTypeComboBox.Text == "Asset")
				tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG;
			else if (TagTypeComboBox.Text == "Inventory")
				tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG;
			else 
				tagSelect.tagType = AW_API_NET.APIConsts.FACTORY_TAG;

			tagSelect.tagList = new uint[50];
			tagSelect.tagList[0] = tagID;
			tagSelect.numTags = 1;

			if(LongDelayCheckBox.Checked)
			{
				setTxTimeInt = true;
				timeInt = true; //long
			}
			else 
			{
				setTxTimeInt = true;
				timeInt = false; //short
			}

			int ret = apiClass.rfGetTagTempLogTimestamp(host, rdr, 0, tagSelect, setTxTimeInt, timeInt, cmdType, ++pktID);
			string s = Convert.ToString(item);
			s += " - Get Tags Temp Logging Timestamp - ret Code = ";
			s += ret;
			s += "  pktID = ";
			s += pktID - 1;
            addMsgQ(s);
        }

	}
}
