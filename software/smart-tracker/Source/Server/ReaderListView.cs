using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
//using ActiveWave.RfidDb;

namespace ActiveWave.Mapper
{
   public class ReaderListView : System.Windows.Forms.UserControl, IComparer
   {
      //public event ReaderEventHandler ReaderSelected = null;
      //public event ReaderEventHandler ReaderActivated = null;

      //private RfidDbController m_rfid = RfidDbController.theRfidDbController;
      private int m_sortColumn = -1;
      private bool m_sortReverse= false;
      private Comparer m_comparer = new Comparer(CultureInfo.CurrentCulture);
      //private int m_statusColumn = -1;

      private System.Windows.Forms.ListView m_listView;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      #region Constructor
      public ReaderListView()
		{
			InitializeComponent();

         //m_rfid.ReaderAdded   += new RfidDb.ReaderEventHandler(OnReaderAdded);
         //m_rfid.ReaderChanged += new RfidDb.ReaderEventHandler(OnReaderAdded);
         //m_rfid.ReaderRemoved += new RfidDb.ReaderEventHandler(OnReaderRemoved);
      }

      #endregion

      #region Properties
      /*public IRfidReader SelectedReader
      {
         get
         { 
            if (m_listView.SelectedItems.Count > 0)
               return m_listView.SelectedItems[0].Tag as IRfidReader;
            else 
               return null;
         }
         set
         {
            m_listView.SelectedItems.Clear();
            ListViewItem item = FindItem(value);
            if (item != null) item.Selected = true;
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
         this.m_listView = new System.Windows.Forms.ListView();
         this.SuspendLayout();
         // 
         // m_listView
         // 
         this.m_listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_listView.FullRowSelect = true;
         this.m_listView.GridLines = true;
         this.m_listView.HideSelection = false;
         this.m_listView.Location = new System.Drawing.Point(0, 0);
         this.m_listView.MultiSelect = false;
         this.m_listView.Name = "m_listView";
         this.m_listView.Size = new System.Drawing.Size(384, 152);
         this.m_listView.TabIndex = 0;
         this.m_listView.View = System.Windows.Forms.View.Details;
         this.m_listView.ItemActivate += new System.EventHandler(this.ListView_ItemActivate);
         this.m_listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
         this.m_listView.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
         // 
         // ReaderListView
         // 
         this.Controls.Add(this.m_listView);
         this.Name = "ReaderListView";
         this.Size = new System.Drawing.Size(384, 150);
         this.ResumeLayout(false);

      }
		#endregion

      #region User events
      private void ListView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
      {
         m_sortReverse = (e.Column == m_sortColumn) ? !m_sortReverse : false;
         m_sortColumn = e.Column;
         m_listView.ListViewItemSorter = this;
         m_listView.Sort();
         m_listView.ListViewItemSorter = null;
      }
      
      private void ListView_ItemActivate(object sender, System.EventArgs e)
      {
         //if (ReaderActivated != null) ReaderActivated(this.SelectedReader);
      }
      
      private void ListView_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         //if (ReaderSelected != null) ReaderSelected(this.SelectedReader);
      }
      
      #endregion

      #region RFID events
      /*private void OnReaderAdded(IRfidReader reader)
      {
         InitListView(reader);
         AddItem(reader);
      }

      private void OnReaderRemoved(IRfidReader reader)
      {
         ListViewItem item = FindItem(reader);
         if (item != null) item.Remove();
      }*/
      #endregion

      #region Misc
      /*private void AddItem(IRfidReader reader)
      {
         ListViewItem item = FindItem(reader);
         if (item == null)
         {
            item = m_listView.Items.Add(string.Empty);
            item.UseItemStyleForSubItems = false;
         }
   
         item.Tag = reader;
         object[] data = reader.DisplayData.ItemArray;
         for (int idx = 0; idx < data.Length; idx++)
         {
            if (idx >= item.SubItems.Count)
               item.SubItems.Add(data[idx].ToString());
            else
               item.SubItems[idx].Text = data[idx].ToString();
         }

         UpdateStatus(item);
      }

      private ListViewItem FindItem(IRfidReader reader)
      {
         if (reader != null)
         {
            foreach (ListViewItem item in m_listView.Items)
            {
               IRfidReader r = item.Tag as IRfidReader;
               if (r.Id == reader.Id) return item;
            }
         }
         return null;
      }*/

      private void UpdateStatus(ListViewItem item)
      {
         // If there is a status column, set color based on reader status
         /*if (m_statusColumn >= 0)
         {
            IRfidReader reader = item.Tag as IRfidReader;
            Color fc, bc;
            switch (reader.Status.ToLower())
            {
               case "online":
                  fc = m_listView.ForeColor;
                  bc = m_listView.BackColor;
                  break;
               case "offline":
                  fc = Color.White;
                  bc = Color.Red;
                  break;
               default:
                  fc = Color.Black;
                  bc = Color.Yellow;
                  break;
            }
            item.SubItems[m_statusColumn].ForeColor = fc;
            item.SubItems[m_statusColumn].BackColor = bc;
         }*/
      }

      /*private void InitListView(IRfidReader reader)
      {
         if (m_listView.Columns.Count == 0)
         {
            m_statusColumn = -1;
            for (int idx = 0; idx < reader.DisplayData.Table.Columns.Count; idx++)
            {
               DataColumn col = reader.DisplayData.Table.Columns[idx];
               m_listView.Columns.Add(col.Caption, 100, HorizontalAlignment.Left);
               if (string.Compare(col.Caption, "status", true) == 0)
               {
                  m_statusColumn = idx;
               }
            }         
         }
      }*/
      
      public int Compare(object x, object y)
      {
         /*ListViewItem item1 = x as ListViewItem;
         ListViewItem item2 = y as ListViewItem;
         IRfidReader reader1 = item1.Tag as IRfidReader;
         IRfidReader reader2 = item2.Tag as IRfidReader;
         object data1 = reader1.DisplayData.ItemArray[m_sortColumn];
         object data2 = reader2.DisplayData.ItemArray[m_sortColumn];

         int rc = 0;
         if (data1 == System.DBNull.Value) 
            rc = -1;
         else if (data2 == System.DBNull.Value)
            rc = 1;
         else rc = m_comparer.Compare(data1, data2);

         return m_sortReverse ? -rc : rc;*/
	     return 0;
      }
      #endregion
   }
}
