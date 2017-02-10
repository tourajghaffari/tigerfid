using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.Odbc;
using AWI.SmartTracker;

namespace CarTracker
{
	/// <summary>
	/// Summary description for RSSIForm.
	/// </summary>
	public class RSSIForm : System.Windows.Forms.Form
	{
		//private CarTracker.AWIRssiControl awiRssiControl1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RSSIForm()
		{
			InitializeComponent();
		}

		public RSSIForm(MainForm mForm)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//mForm.m_updateRSSIListView += new UpdateRSSIListView(this.UpdateRSSIList);
			//int rssi = 255;
			//int threshold = 125;

			////////////////////////////////////////////////
			lock (MainForm.m_connection)
			{
				string mySelectQuery = "SELECT ReaderID, Location, Threshold FROM zones";
				OdbcCommand myCommand = new OdbcCommand(mySelectQuery, MainForm.m_connection); 
				
				OdbcDataReader myReader = null;
				
				try
				{
					myReader = myCommand.ExecuteReader();
					MainForm.reconnectCounter = -1;
				}
				catch (Exception ex)
				{
					int ret = 0, ret1 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) || 
						((ret1=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{  
						//error code 2013	
						if (MainForm.reconnectCounter < 0)
						{
							MainForm.reconnectCounter = 0;
							//timer1.Enabled = true;
						}	                                  
					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				}//catch .. try
			
				while (myReader.Read())
				{
					ListViewItem listItem;
					try
					{
						listItem = new ListViewItem(myReader.GetString(0));  //reader ID
					}
					catch
					{
						continue;
					}
					
					try
					{
						listItem.SubItems.Add(myReader.GetString(1));  //location
					}
					catch
					{
						listItem.SubItems.Add("");
					}

					listItem.SubItems.Add("0");  //RSSI

					try
					{
						listItem.SubItems.Add(myReader.GetString(2));  //threshold
					}
					catch
					{
						listItem.SubItems.Add("0");
					}

					
					listItem.SubItems.Add(" ");  //threshold
					listItem.SubItems.Add(" ");  //timestamp
                    listItem.SubItems.Add("2");  //groupID

					//rssiControl1.AddProgressBar(listItem);
					//rssiControl1.AddTrackBar(listItem);

				}//while
				myReader.Close();

		}//m_connection


			///////////////////////////////////////////////

			//Read from zones table here
			/*for (int i=0; i<=5; i++)
			{
				ListViewItem listItem = new ListViewItem("5");   //reader ID
				listItem.SubItems.Add("Main Exit Door");  //location
				listItem.SubItems.Add(rssi.ToString());  //rssi
				//listItem.SubItems.Add(DateTime.Now.ToString());  //timeStamp
				rssiControl1.AddProgressBar(listItem);
				listItem.SubItems.Add(threshold.ToString());  //threshold
				rssiControl1.AddTrackBar(listItem);
			}*/
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.awiRssiControl1 = new CarTracker.AWIRssiControl();
			this.SuspendLayout();
			// 
			// awiRssiControl1
			// 
			this.awiRssiControl1.Location = new System.Drawing.Point(8, 8);
			this.awiRssiControl1.Name = "awiRssiControl1";
			this.awiRssiControl1.Size = new System.Drawing.Size(904, 584);
			this.awiRssiControl1.TabIndex = 0;
			// 
			// RSSIForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(922, 599);
			this.Controls.Add(this.awiRssiControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "RSSIForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RSSI Configuration Form";
			this.Load += new System.EventHandler(this.RSSIForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void RSSIForm_Load(object sender, System.EventArgs e)
		{
		
		}

		private void UpdateRSSIList(AW_API_NET.rfTagEvent_t tagEvent)
		{

			//rssiControl1.DisplayRSSI(tagEvent);

		}
	}
}
