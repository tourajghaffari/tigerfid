using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Globalization;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for RSSIConfigForm.
	/// </summary>
	public class RSSIConfigForm : System.Windows.Forms.Form
	{
        private AWI.SmartTracker.AWIRSSIUserControl awirssiUserControl1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RSSIConfigForm()
		{
			InitializeComponent();
		}

		public RSSIConfigForm(MainForm mForm)
		{
			InitializeComponent();

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";
			mForm.m_updateRSSIListView += new UpdateRSSIListView(this.UpdateRSSIList);

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
					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
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
		}

		private void UpdateRSSIList(AW_API_NET.rfTagEvent_t tagEvent)
		{

			//rssiControl1.DisplayRSSI(tagEvent);

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
            this.awirssiUserControl1 = new AWI.SmartTracker.AWIRSSIUserControl();
			this.SuspendLayout();
			// 
			// awirssiUserControl1
			// 
			this.awirssiUserControl1.Location = new System.Drawing.Point(8, 8);
			this.awirssiUserControl1.Name = "awirssiUserControl1";
			this.awirssiUserControl1.Size = new System.Drawing.Size(904, 592);
			this.awirssiUserControl1.TabIndex = 0;
			// 
			// RSSIConfigForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(920, 605);
			this.Controls.Add(this.awirssiUserControl1);
			this.Name = "RSSIConfigForm";
			this.Text = "RSSIConfigForm";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
