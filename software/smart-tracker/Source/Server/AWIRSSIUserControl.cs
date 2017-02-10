using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using AWIComponentLib.Communication;
using System.Runtime.InteropServices;

namespace AWI.SmartTracker
{
	public delegate void SetRSSIProgressBar(short rssi, int index);

	public class AWIRSSIUserControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ComboBox TagTypeComboBox;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button ResetALLButton;
		private System.Windows.Forms.Button button1;
		//private CarTracker.ListViewEx RSSIListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.TextBox TagTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
        private AWI.SmartTracker.ListViewEx RSSIListView;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Timers.Timer timer1;
		
		//user defined var
		public event SetRSSIProgressBar m_setRSSIProgressBar;
		private CommunicationClass communication;
		//private summaryStruct[] summary = new summaryStruct[100];
		private ushort numReaders;

		/// </summary>
		private System.ComponentModel.Container components = null;

		public AWIRSSIUserControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
            communication = MainForm.communication;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.TagTypeComboBox = new System.Windows.Forms.ComboBox();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.ResetALLButton = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
			this.TagTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.RSSIListView = new AWI.SmartTracker.ListViewEx();
			this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
			this.timer1 = new System.Timers.Timer();
			((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
			this.SuspendLayout();
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "RSSI";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader2.Width = 210;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Location";
			this.columnHeader6.Width = 250;
			// 
			// TagTypeComboBox
			// 
			this.TagTypeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TagTypeComboBox.Items.AddRange(new object[] {
																 "ACC",
																 "AST",
																 "INV"});
			this.TagTypeComboBox.Location = new System.Drawing.Point(376, 8);
			this.TagTypeComboBox.Name = "TagTypeComboBox";
			this.TagTypeComboBox.Size = new System.Drawing.Size(144, 32);
			this.TagTypeComboBox.TabIndex = 28;
			this.TagTypeComboBox.Text = "ACC";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Timestamp";
			this.columnHeader5.Width = 160;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "High RSSI";
			this.columnHeader13.Width = 94;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "% Failed";
			this.columnHeader12.Width = 75;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "Ave RSSI";
			this.columnHeader15.Width = 82;
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "Low RSSI";
			this.columnHeader14.Width = 89;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Pass / Fail";
			this.columnHeader4.Width = 100;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Blue;
			this.label2.Location = new System.Drawing.Point(272, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 28);
			this.label2.TabIndex = 27;
			this.label2.Text = "Tag Type: ";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.Red;
			this.label5.Location = new System.Drawing.Point(680, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(204, 28);
			this.label5.TabIndex = 35;
			this.label5.Text = "Stopped";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Blue;
			this.label4.Location = new System.Drawing.Point(568, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 28);
			this.label4.TabIndex = 34;
			this.label4.Text = "Test Status: ";
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button3.ForeColor = System.Drawing.Color.Blue;
			this.button3.Location = new System.Drawing.Point(720, 376);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(164, 40);
			this.button3.TabIndex = 33;
			this.button3.Text = "Start";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Blue;
			this.label3.Location = new System.Drawing.Point(16, 352);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(200, 24);
			this.label3.TabIndex = 32;
			this.label3.Text = "RSSI Test Summary: ";
			// 
			// ResetALLButton
			// 
			this.ResetALLButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ResetALLButton.ForeColor = System.Drawing.Color.Blue;
			this.ResetALLButton.Location = new System.Drawing.Point(720, 480);
			this.ResetALLButton.Name = "ResetALLButton";
			this.ResetALLButton.Size = new System.Drawing.Size(164, 36);
			this.ResetALLButton.TabIndex = 31;
			this.ResetALLButton.Text = "Reset All";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.Blue;
			this.button1.Location = new System.Drawing.Point(720, 432);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(164, 40);
			this.button1.TabIndex = 30;
			this.button1.Text = "Reset Summary";
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Reader ID";
			this.columnHeader1.Width = 96;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Threshold";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader3.Width = 210;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "GID";
			this.columnHeader16.Width = 1;
			// 
			// TagTextBox
			// 
			this.TagTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TagTextBox.Location = new System.Drawing.Point(96, 16);
			this.TagTextBox.Name = "TagTextBox";
			this.TagTextBox.Size = new System.Drawing.Size(124, 29);
			this.TagTextBox.TabIndex = 26;
			this.TagTextBox.Text = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 28);
			this.label1.TabIndex = 25;
			this.label1.Text = "Tag ID: ";
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader7,
																						this.columnHeader8,
																						this.columnHeader9,
																						this.columnHeader10,
																						this.columnHeader11,
																						this.columnHeader12,
																						this.columnHeader13,
																						this.columnHeader14,
																						this.columnHeader15});
			this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(16, 376);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(692, 196);
			this.listView1.TabIndex = 29;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "ReaderID";
			this.columnHeader7.Width = 85;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Run #";
			this.columnHeader8.Width = 65;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "# Pass";
			this.columnHeader9.Width = 62;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "# Failed";
			this.columnHeader10.Width = 69;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "% Pass";
			this.columnHeader11.Width = 65;
			// 
			// RSSIListView
			// 
			this.RSSIListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						   this.columnHeader17,
																						   this.columnHeader18,
																						   this.columnHeader19,
																						   this.columnHeader20,
																						   this.columnHeader21,
																						   this.columnHeader22,
																						   this.columnHeader23});
			this.RSSIListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RSSIListView.GridLines = true;
			this.RSSIListView.Location = new System.Drawing.Point(16, 72);
			this.RSSIListView.Name = "RSSIListView";
			this.RSSIListView.Size = new System.Drawing.Size(872, 252);
			this.RSSIListView.TabIndex = 36;
			this.RSSIListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "Reader ID";
			this.columnHeader17.Width = 96;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "Location";
			this.columnHeader18.Width = 250;
			// 
			// columnHeader19
			// 
			this.columnHeader19.Text = "RSSI";
			this.columnHeader19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader19.Width = 210;
			// 
			// columnHeader20
			// 
			this.columnHeader20.Text = "Threshold";
			this.columnHeader20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader20.Width = 210;
			// 
			// columnHeader21
			// 
			this.columnHeader21.Text = "Pass / Fail";
			this.columnHeader21.Width = 100;
			// 
			// columnHeader22
			// 
			this.columnHeader22.Text = "Timestamp";
			this.columnHeader22.Width = 160;
			// 
			// columnHeader23
			// 
			this.columnHeader23.Text = "GID";
			this.columnHeader23.Width = 1;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 4000;
			this.timer1.SynchronizingObject = this;
			this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
			// 
			// AWIRSSIUserControl
			// 
			this.Controls.Add(this.RSSIListView);
			this.Controls.Add(this.TagTypeComboBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ResetALLButton);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.TagTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listView1);
			this.Name = "AWIRSSIUserControl";
			this.Size = new System.Drawing.Size(904, 592);
			((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (TagTextBox.Text.Length == 0)
				return;
			ushort rdr = 3;
			communication.CallTag(rdr, 0, Convert.ToUInt32(TagTextBox.Text), TagTypeComboBox.Text.ToString(), "Specific");
		}

		private void ResetALLButton_Click(object sender, System.EventArgs e)
		{
			//timer1.Enabled = true;
		}

		private short GetThreshold(ushort reader, out int index)
		{
			for (int i=0; i<RSSIListView.Items.Count; i++)
			{
				string s = RSSIListView.Items[i].Text.ToString();
				string s1 = RSSIListView.Items[i].SubItems[0].Text.ToString();
				if (Convert.ToUInt16(RSSIListView.Items[i].SubItems[0].Text.ToString()) == reader)
				{
					index = i;
					return (Convert.ToInt16(RSSIListView.Items[i].SubItems[3].Text.ToString()));
				}
			}

			index = -1;
			return (0);
		}

		public void DisplayRSSI(AW_API_NET.rfTagEvent_t tagEvent)
		{
			//get reader id
			//get rssi
			//get index of the reader on the list
			//get the rssi from the list
			//Console.WriteLine("TagID = " + tagEvent.tag.id.ToString());
			//Console.WriteLine("TextTagID = " + TagTextBox.Text.ToString());
			//Console.WriteLine("Tag Type = " + TagTypeComboBox.Text.ToString());

			Console.WriteLine("TagID = " + tagEvent.tag.id.ToString() + "  RSSI = " + tagEvent.RSSI.ToString());

			if ((TagTextBox.Text.Length > 0) && (TagTypeComboBox.Text.Length > 0))
			{
				if (tagEvent.tag.id.ToString() != TagTextBox.Text.ToString())
					return;
				else
				{
					switch (tagEvent.tag.tagType)
					{
						case 1:  //access
							if (TagTypeComboBox.Text.ToString() != "ACC")
								return;
							break;

						case 2:  //inventory
							if (TagTypeComboBox.Text.ToString() != "INV")
								return;
							break;

						case 3:  //asset
							if (TagTypeComboBox.Text.ToString() != "AST")
								return;
							break;

						default:
							return;

					}//switch

                    //return;

				}//if id not match
			} //if no id
			else
				return;

			int index = 0;
			try
			{
				short threshold = GetThreshold(tagEvent.reader, out index);
				if (index >= 0)
				{
					RSSIListView.Items[index].BackColor = System.Drawing.Color.LightYellow;
					//Console.WriteLine("TEST 01 - List GC = " + RSSIListView.Items[index].SubItems[6].Text.ToString());
					Console.WriteLine("TEST 02 - ID = " + tagEvent.tag.id.ToString() + "  Tag GC = " + tagEvent.tag.groupCount.ToString() + "  GC List = " + RSSIListView.Items[index].SubItems[6].Text.ToString());
					//if new group ID
					if ((RSSIListView.Items[index].SubItems[6].Text.ToString() == "2") ||
						(RSSIListView.Items[index].SubItems[6].Text.ToString() != tagEvent.tag.groupCount.ToString()))
					{
						//summary[index].numRuns += 1;
						//summary[index].numRead += 1;
						//summary[index].rssi += Convert.ToUInt64(tagEvent.RSSI);
           
						if (tagEvent.RSSI >= threshold)
						{
							RSSIListView.Items[index].SubItems[4].Text = "PASS";
							//summary[index].numPass += 1;
							//if (summary[index].highRSSI < tagEvent.RSSI)
							//summary[index].highRSSI = tagEvent.RSSI;
						}
						else
						{
							RSSIListView.Items[index].SubItems[4].Text = "FAIL";
							//if (summary[index].lowRSSI > tagEvent.RSSI)
							//summary[index].lowRSSI = tagEvent.RSSI;
						}

						RSSIListView.Items[index].SubItems[6].Text = tagEvent.tag.groupCount.ToString();	
						RSSIListView.Items[index].SubItems[4].ForeColor = System.Drawing.Color.DarkGreen;
						RSSIListView.Items[index].SubItems[2].Text = Convert.ToString(tagEvent.RSSI);
						ProgressBar pb = new ProgressBar();
						pb = (ProgressBar)RSSIListView.GetEmbeddedControl(2, index);
						pb.Value = Convert.ToInt32(tagEvent.RSSI);
					}
					else
					{
						// same group id
						//Console.WriteLine("TEST 03 - Tag GC = " + tagEvent.tag.groupCount.ToString());
						//Console.WriteLine("TEST 04 - LIST GC = " + RSSIListView.Items[index].SubItems[6].Text.ToString());
						Console.WriteLine("TEST 03 - ID = " + tagEvent.tag.id.ToString() + "  Tag GC = " + tagEvent.tag.groupCount.ToString() + "  GC List = " + RSSIListView.Items[index].SubItems[6].Text.ToString());
						if (RSSIListView.Items[index].SubItems[6].Text.ToString() == tagEvent.tag.groupCount.ToString())
						{
							//Console.WriteLine("TEST 05 - Pass/Fail = " + RSSIListView.Items[index].SubItems[4].Text.ToString());
							
							//summary[index].numRead += 1;
							//summary[index].rssi += Convert.ToUInt64(tagEvent.RSSI);

							if (RSSIListView.Items[index].SubItems[4].Text.ToString() == "FAIL")
							{
								if (tagEvent.RSSI >= threshold)
								{
									RSSIListView.Items[index].SubItems[4].Text = "PASS";
								
									//RSSIListView.Items[index].SubItems[4].Text = "PASS";
									RSSIListView.Items[index].SubItems[4].ForeColor = System.Drawing.Color.DarkGreen;
									RSSIListView.Items[index].SubItems[2].Text = Convert.ToString(tagEvent.RSSI);
									ProgressBar pb = new ProgressBar();
									pb = (ProgressBar)RSSIListView.GetEmbeddedControl(2, index);
									pb.Value = Convert.ToInt32(tagEvent.RSSI);

									//summary[index].numPass += 1;
									//if (summary[index].highRSSI < tagEvent.RSSI)
									//summary[index].highRSSI = tagEvent.RSSI;
								}
								else
								{
									//if (summary[index].lowRSSI > tagEvent.RSSI)
									//summary[index].lowRSSI = tagEvent.RSSI;
								}
							}
							else //pass
							{
								if (tagEvent.RSSI >= threshold)
								{
									RSSIListView.Items[index].SubItems[4].ForeColor = System.Drawing.Color.DarkGreen;
									RSSIListView.Items[index].SubItems[2].Text = Convert.ToString(tagEvent.RSSI);
									ProgressBar pb = new ProgressBar();
									pb = (ProgressBar)RSSIListView.GetEmbeddedControl(2, index);
									pb.Value = Convert.ToInt32(tagEvent.RSSI);
									//if (summary[index].highRSSI < tagEvent.RSSI)
									//summary[index].highRSSI = tagEvent.RSSI;
								}
								else
								{
									//if (summary[index].lowRSSI > tagEvent.RSSI)
									//summary[index].lowRSSI = tagEvent.RSSI;
								}
							}//pass
						}//same GID
					}//else
				}//index
			}//try
			catch //(Exception ex)
			{
				Console.WriteLine("Setting PASS /FAIL Failed");
			}
			
	
			

			/*if (RSSIListView.Items[index].SubItems[4].Text.ToString() == "FAIL")
							{
								RSSIListView.Items[index].SubItems[6].Text = tagEvent.tag.groupCount.ToString();
								RSSIListView.Items[index].SubItems[4].Text = "PASS";
								RSSIListView.Items[index].SubItems[4].ForeColor = System.Drawing.Color.DarkGreen;
								RSSIListView.Items[index].SubItems[2].Text = Convert.ToString(tagEvent.RSSI);
								ProgressBar pb = new ProgressBar();
								pb = (ProgressBar)RSSIListView.GetEmbeddedControl(2, index);
								pb.Value = Convert.ToInt32(tagEvent.RSSI);
							}
						}
					}
					catch (Exception ex)
					{
						 Console.WriteLine("Setting PASS /FAIL Failed");
					}
				}
			}*/

			//compare two rssi
			//display rssi pass/fail
			//calculate the sumary
			//dislay the summery
		}

		public void AddProgressBar(ListViewItem item)
		{
			ProgressBar pb = new ProgressBar();
			pb.Minimum = 0;
			pb.Maximum = 255;
			pb.Step = 10;
			pb.Value = 0;
			RSSIListView.Items.Add(item);
			RSSIListView.AddEmbeddedControl(pb, 2, item.Index);
			//summary[numReaders].rdrID = Convert.ToUInt16(item.SubItems[0].Text);
			//summary[numReaders].numRuns = 0;
			//summary[numReaders].numPass = 0;
			//summary[numReaders].lowRSSI = 0;
			//summary[numReaders].highRSSI = 0;
			//summary[numReaders].aveRSSI = 0;
			numReaders += 1;
		}

		public void AddTrackBar(ListViewItem item)
		{
			TrackBar tb = new TrackBar();
			tb.Minimum = 0;
			tb.Maximum = 255;
			tb.TickFrequency = 10;
			tb.AutoSize = false;
			tb.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			try
			{
				tb.Value = Convert.ToInt32(item.SubItems[3].Text);
			}
			catch
			{
				tb.Value = 0;
			}
			//RSSIListView.Items.Add(item);
			tb.ValueChanged += new EventHandler(tb_ValueChanged);
			RSSIListView.AddTBEmbeddedControl(tb, 3, item.Index);
		}

		private void tb_ValueChanged(object sender, EventArgs e)
		{
			int index = RSSIListView.GetTBControlIndex((TrackBar)sender);
			if (index >= 0)
			{
				TrackBar tb = (TrackBar)RSSIListView.GetTBEmbeddedControl(3, index);
				RSSIListView.Items[index].SubItems[3].Text = tb.Value.ToString();
			}
             
		}
	}
}
