#region change history
//seyed #001 (Dec 13, 05) - compiler complains about needing instance of an object in version 2.0 - removed in V3.0
//seyed #002 (Dec 13, 05) - error in displaying tag name for location in  tagHistory tab V2.0
//seyed #003 (Dec 13, 05) - compiler complains about needing instance of an object V2.0 - removed in V3.0
//seyed #004 (Dec 21, 05) - error in displaying tag name in ZoneHistory V3.0
#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Threading;
using System.Windows.Forms;
//using ActiveWave.RfidDb;

namespace ActiveWave.Mapper
{
	public class TagHistoryView : System.Windows.Forms.UserControl, IComparer
	{
      private delegate void RefreshHandler();

      //private RfidDbController m_rfid = RfidDbController.theRfidDbController;
      //private IRfidTag m_tag = null;
      //private IRfidReader m_reader = null;
      private int m_sortColumn = -1;
      private bool m_sortReverse = false;
      private Color m_backColor0 = Color.FromArgb(255, 255, 255);
      private Color m_backColor1 = Color.FromArgb(244, 244, 255);
      private System.Windows.Forms.DateTimePicker m_dtFrom;
      private System.Windows.Forms.DateTimePicker m_dtTo;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.ListView m_lvHistory;
      private System.Windows.Forms.ColumnHeader columnHeader3;
      private System.Windows.Forms.ColumnHeader columnHeader2;
      private System.Windows.Forms.ColumnHeader columnHeader4;
      private ActiveWave.Controls.TitleBar m_lblName;
      private System.Windows.Forms.LinkLabel m_lnkRefresh;
      private System.Windows.Forms.Label m_lblEventCount;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private enum HistoryType { Tags, Zones }; //seyed #004

      #region Constructor
      public TagHistoryView()
		{
			InitializeComponent();
         
         // Set initial dates to span all of today
         m_dtFrom.Value = DateTime.Today;
         m_dtTo.Value = m_dtFrom.Value.AddDays(1);

         // Some kind bug in DateTimePicker with the Checked property
         // This seems to resolve it
         m_dtFrom.Checked  = true;
         m_dtTo.Checked    = true;
         m_dtFrom.Checked  = false;
         m_dtTo.Checked    = false;
      }
      #endregion

      #region Properties
      /*public new IRfidTag Tag
      {
         get { return m_tag; }
         set 
         {
            m_reader = null;
            m_tag = value;
            RefreshData();
         }
      }*/

      /*public IRfidReader Reader
      {
         get { return m_reader; }
         set 
         {
            m_tag = null;
            m_reader = value;
            RefreshData();
         }
      }*/
      #endregion

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
         this.m_dtFrom = new System.Windows.Forms.DateTimePicker();
         this.m_dtTo = new System.Windows.Forms.DateTimePicker();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.m_lvHistory = new System.Windows.Forms.ListView();
         this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
         this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
         this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
         this.m_lblName = new ActiveWave.Controls.TitleBar();
         this.m_lnkRefresh = new System.Windows.Forms.LinkLabel();
         this.m_lblEventCount = new System.Windows.Forms.Label();
         this.m_lblName.SuspendLayout();
         this.SuspendLayout();
         // 
         // m_dtFrom
         // 
         this.m_dtFrom.Checked = false;
         this.m_dtFrom.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
         this.m_dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
         this.m_dtFrom.Location = new System.Drawing.Point(48, 32);
         this.m_dtFrom.Name = "m_dtFrom";
         this.m_dtFrom.ShowCheckBox = true;
         this.m_dtFrom.Size = new System.Drawing.Size(168, 20);
         this.m_dtFrom.TabIndex = 1;
         this.m_dtFrom.Value = new System.DateTime(2005, 9, 1, 21, 20, 58, 825);
         // 
         // m_dtTo
         // 
         this.m_dtTo.Checked = false;
         this.m_dtTo.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
         this.m_dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
         this.m_dtTo.Location = new System.Drawing.Point(256, 32);
         this.m_dtTo.Name = "m_dtTo";
         this.m_dtTo.ShowCheckBox = true;
         this.m_dtTo.Size = new System.Drawing.Size(168, 20);
         this.m_dtTo.TabIndex = 0;
         this.m_dtTo.Value = new System.DateTime(2005, 9, 1, 21, 20, 58, 765);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(232, 34);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(21, 16);
         this.label1.TabIndex = 2;
         this.label1.Text = "To:";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(8, 34);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(34, 16);
         this.label2.TabIndex = 3;
         this.label2.Text = "From:";
         // 
         // m_lvHistory
         // 
         this.m_lvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_lvHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.m_lvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                      this.columnHeader4,
                                                                                      this.columnHeader2,
                                                                                      this.columnHeader3});
         this.m_lvHistory.FullRowSelect = true;
         this.m_lvHistory.GridLines = true;
         this.m_lvHistory.Location = new System.Drawing.Point(8, 64);
         this.m_lvHistory.Name = "m_lvHistory";
         this.m_lvHistory.Size = new System.Drawing.Size(568, 208);
         this.m_lvHistory.TabIndex = 4;
         this.m_lvHistory.View = System.Windows.Forms.View.Details;
         this.m_lvHistory.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewHistory_ColumnClick);
         // 
         // columnHeader4
         // 
         this.columnHeader4.Text = "Name";
         this.columnHeader4.Width = 150;
         // 
         // columnHeader2
         // 
         this.columnHeader2.Text = "Event";
         this.columnHeader2.Width = 120;
         // 
         // columnHeader3
         // 
         this.columnHeader3.Text = "Timestamp";
         this.columnHeader3.Width = 140;
         // 
         // m_lblName
         // 
         this.m_lblName.AllowDrop = true;
         this.m_lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_lblName.BackColor = System.Drawing.Color.Navy;
         this.m_lblName.BorderColor = System.Drawing.Color.White;
         this.m_lblName.Controls.Add(this.m_lnkRefresh);
         this.m_lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
         this.m_lblName.ForeColor = System.Drawing.Color.White;
         this.m_lblName.GradientColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
         this.m_lblName.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
         this.m_lblName.Location = new System.Drawing.Point(0, 0);
         this.m_lblName.Name = "m_lblName";
         this.m_lblName.ShadowColor = System.Drawing.Color.Black;
         this.m_lblName.ShadowOffset = new System.Drawing.Size(1, 1);
         this.m_lblName.Size = new System.Drawing.Size(584, 24);
         this.m_lblName.TabIndex = 5;
         this.m_lblName.Text = "Title";
         // 
         // m_lnkRefresh
         // 
         this.m_lnkRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.m_lnkRefresh.AutoSize = true;
         this.m_lnkRefresh.BackColor = System.Drawing.Color.Transparent;
         this.m_lnkRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.m_lnkRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
         this.m_lnkRefresh.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
         this.m_lnkRefresh.LinkColor = System.Drawing.Color.White;
         this.m_lnkRefresh.Location = new System.Drawing.Point(528, 4);
         this.m_lnkRefresh.Name = "m_lnkRefresh";
         this.m_lnkRefresh.Size = new System.Drawing.Size(48, 17);
         this.m_lnkRefresh.TabIndex = 6;
         this.m_lnkRefresh.TabStop = true;
         this.m_lnkRefresh.Text = "Refresh";
         this.m_lnkRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Refresh_LinkClicked);
         // 
         // m_lblEventCount
         // 
         this.m_lblEventCount.Location = new System.Drawing.Point(440, 34);
         this.m_lblEventCount.Name = "m_lblEventCount";
         this.m_lblEventCount.Size = new System.Drawing.Size(88, 16);
         this.m_lblEventCount.TabIndex = 7;
         this.m_lblEventCount.Text = "0 Events";
         // 
         // TagHistoryView
         // 
         this.Controls.Add(this.m_lblEventCount);
         this.Controls.Add(this.m_lblName);
         this.Controls.Add(this.m_lvHistory);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.m_dtFrom);
         this.Controls.Add(this.m_dtTo);
         this.Controls.Add(this.label1);
         this.Name = "TagHistoryView";
         this.Size = new System.Drawing.Size(584, 280);
         this.m_lblName.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      #region User events
      private void ListViewHistory_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
      {
         m_sortReverse = (e.Column == m_sortColumn) ? !m_sortReverse : false;
         m_sortColumn = e.Column;
         m_lvHistory.ListViewItemSorter = this;
         m_lvHistory.Sort();
         m_lvHistory.ListViewItemSorter = null;
      }

      private void Refresh_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
      {
         RefreshData();
      }

      #endregion

      #region Misc
      public void RefreshData()
      {
         Thread thread = new Thread(new ThreadStart(RefreshThread));
         thread.Start();
      }

      public void RefreshThread()
      {
         if (this.InvokeRequired)
         {
            this.BeginInvoke(new RefreshHandler(RefreshThread));
            return;
         }

         /*if (m_reader != null)
            GetReaderHistory();
         else 
            GetTagHistory();*/

         m_lblEventCount.Text = m_lvHistory.Items.Count.ToString() + " Events";
         m_lblEventCount.Visible = m_lvHistory.Items.Count > 0;
      }

      private void GetTagHistory()
      {
         /*m_lvHistory.Items.Clear();
         m_lvHistory.Columns[0].Text = "Location";

	     //seyed #001 (added)- compiler complains about needing instance of an object
		 //ActiveWave.Controls.TitleBar m_lblName = new ActiveWave.Controls.TitleBar();
	     //seyed #001 (Dec 13, 2005)-------------------------------------------------

         m_lblName.Text = string.Empty;

         if (m_tag != null)
         {
            try
            {
               m_lblName.Text = m_tag.Name;
               DateTime dtFrom = m_dtFrom.Checked ? m_dtFrom.Value : DateTime.MinValue;
               DateTime dtTo   = m_dtTo.Checked   ? m_dtTo.Value   : DateTime.MaxValue;
               IRfidTagActivity[] tagActivityList = m_rfid.RfidDb.GetTagHistory(m_tag.Id, dtFrom, dtTo);
               
				//AddItems(tagActivityList); //seyed #004 
				AddItems(tagActivityList, HistoryType.Tags);
            }
            catch {} // ignore errors for now
         }   */
      }

      private void GetReaderHistory()
      {
         /*m_lvHistory.Items.Clear();
         m_lvHistory.Columns[0].Text = "Name";

	     //seyed #003 (added)- compiler complains about needing instance of an object
		 //ActiveWave.Controls.TitleBar m_lblName = new ActiveWave.Controls.TitleBar();
	     //seyed #003 (Dec 13, 2005)-------------------------------------------------

         m_lblName.Text = string.Empty;

         if (m_reader != null)
         {
            try
            {
               m_lblName.Text = m_reader.Name;
               DateTime dtFrom = m_dtFrom.Checked ? m_dtFrom.Value : DateTime.MinValue;
               DateTime dtTo   = m_dtTo.Checked   ? m_dtTo.Value   : DateTime.MaxValue;
               IRfidTagActivity[] tagActivityList = m_rfid.RfidDb.GetReaderHistory(m_reader.Id, dtFrom, dtTo);
               
				//AddItems(tagActivityList);  //seyed #004
				AddItems(tagActivityList, HistoryType.Zones);
            }
            catch {} // ignore errors for now
         }*/
      }

      //private void AddItems(IRfidTagActivity[] tagActivityList)   //seyed #004
      /*private void AddItems(IRfidTagActivity[] tagActivityList, HistoryType type)
	  {
         m_lvHistory.BeginUpdate();
         try
         {
            bool even = false;
            foreach (IRfidTagActivity tagActivity in tagActivityList)
            {
               IRfidTag tag = m_rfid.Tags[tagActivity.TagId];
               string name = (tag != null) ? tag.Name : tagActivity.TagId;

               //seyed #002 error in displaying tag name for location in  tagHistory tab
			   //displays tag name instead of location

			   //item = new ListViewItem(tagActivity.Location);

			   //seyed #002 (Dec 21, 2005) ---------------------------------------------

			   //seyed #004 error in displaying rdr/fgen for name in zoneHistory tab
			   //displays tag name instead of location in tagHistory and
			   //displays rdr/fgen instead of name in zoneHistory
			   //removed seyed #2 and inserted seyed #3 to fix the problem
			   //new param for AddItems added enum{Tags, Zones}

				ListViewItem item;
				if (type == HistoryType.Zones)
				   item = new ListViewItem(name);
				else
				   item = new ListViewItem(tagActivity.Location);

				//seyed #004 (Dec 21, 2005) ------------------------------------------
               
				item.BackColor = even ? m_backColor0 : m_backColor1;
               item.SubItems.Add(tagActivity.Event);
               item.SubItems.Add(tagActivity.Timestamp.ToString("MM/dd/yyyy hh:mm:ss tt"));
               item.Tag = tagActivity;
               m_lvHistory.Items.Add(item);

               even = !even;
            }
         }
         catch
         {
            throw;
         }
         finally
         {
            m_lvHistory.EndUpdate();
         }
      }*/

      public int Compare(object x, object y)
      {
         /*ListViewItem item1 = x as ListViewItem;
         ListViewItem item2 = y as ListViewItem;
         IRfidTagActivity activity1 = item1.Tag as IRfidTagActivity;
         IRfidTagActivity activity2 = item2.Tag as IRfidTagActivity;

         int rc = 0;
         switch (m_sortColumn)
         {
            case 3:  // Timestamp
               rc = DateTime.Compare(activity1.Timestamp, activity2.Timestamp);
               break;
            default:
               string s1 = item1.SubItems[m_sortColumn].Text;
               string s2 = item2.SubItems[m_sortColumn].Text;
               rc = string.Compare(s1, s2);
               break;
         }
         return m_sortReverse ? -rc : rc;  */
	     return 0;
      }
      #endregion

   }
}
