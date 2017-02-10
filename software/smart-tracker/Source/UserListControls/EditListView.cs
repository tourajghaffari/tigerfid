using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace UserListControl
{
    public delegate void ActionComboBoxValueChangedDelegate(string value);
    public delegate void DurationComboBoxValueChangedDelegate(string value);
    public delegate void ReaderComboBoxValueChangedDelegate(string value);
    public delegate void FGenComboBoxValueChangedDelegate(string value);

    //public delegate int GetItemActionComboBoxDelegate(int index);
    //public delegate int GetItemDurationComboBoxDelegate();
    public delegate int GetItemReaderComboBoxDelegate(int index);
    public delegate string GetItemLocFGenComboBoxDelegate(ushort rdr, ushort fgen);
    public delegate int GetItemFGenComboBoxDelegate(ushort rdr, int index);
    public delegate void GetItemRdrFgenDelegate(out ushort rdr, out ushort fgen, out string loc);

    public delegate bool LoadReaderDataDelegate();
    public delegate bool GetListItemDelegate(int index, string action, ushort duration, ushort rdr, ushort fgen, string loc);

    public class EditListView : ListView 
	{
		public ListViewItem li;
		private int X=0;
		private int Y=0;
        private int MAX_ROW = 20;
		//private string subItemText ;
		private int subItemSelected = 0 ;
        private int listSelectedRow = 0;
        public System.Windows.Forms.ComboBox ActionCmbBox = new System.Windows.Forms.ComboBox();
        public System.Windows.Forms.ComboBox ActionInputCmbBox = new System.Windows.Forms.ComboBox();
        protected System.Windows.Forms.ComboBox DurationCmbBox = new System.Windows.Forms.ComboBox();
        protected System.Windows.Forms.ComboBox ReaderCmbBox = new System.Windows.Forms.ComboBox();
        protected System.Windows.Forms.ComboBox FGenCmbBox = new System.Windows.Forms.ComboBox();
        public System.Windows.Forms.Label LocationLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;

        public static event ActionComboBoxValueChangedDelegate ActionComboBoxValueChangedEvent;
        public static event DurationComboBoxValueChangedDelegate DurationComboBoxValueChangedEvent;
        public static event ReaderComboBoxValueChangedDelegate ReaderComboBoxValueChangedEvent;
        public static event FGenComboBoxValueChangedDelegate FGenComboBoxValueChangedEvent;

        //public static event GetItemActionComboBoxDelegate GetItemActionComboBoxEvent;
        //public static event GetItemDurationComboBoxDelegate GetItemDurationComboBoxEvent;
        public static event GetItemReaderComboBoxDelegate GetItemReaderComboBoxEvent;
        public static event GetItemFGenComboBoxDelegate GetItemFGenComboBoxEvent;
        public static event GetItemLocFGenComboBoxDelegate GetItemLocFGenComboBoxEvent;

        public static event LoadReaderDataDelegate LoadReaderDataEvent;
        public static event GetItemRdrFgenDelegate GetItemRdrFgenEvent;
        //public static event GetListItemDelegate GetListItemEvent;

        private bool bActionInput = false;

        private Thread loadingThread;

        public static void SendListItem(int index)
        {
            //if (GetListItemEvent != null)
                //GetListItemEvent(in
        }

		public EditListView()
		{        
            //Action
            ActionCmbBox.Items.Add("");
            //ActionCmbBox.Items.Add("Lock Door Relay 01");
            //ActionCmbBox.Items.Add("Lock Door Relay 02");
            ActionCmbBox.Items.Add("Turn On Alarm Light Relay 01");
            ActionCmbBox.Items.Add("Turn On Alarm Light Relay 02");
            ActionCmbBox.Items.Add("Turn On Siren Relay 01");
            ActionCmbBox.Items.Add("Turn On Siren Relay 02");
            ActionCmbBox.Items.Add("Unlock Door Relay 01");
            ActionCmbBox.Items.Add("Unlock Door Relay 02");
            ActionCmbBox.Items.Add("Display Tag Moving Direction");
            ActionCmbBox.Items.Add("Call Access Tag");
            ActionCmbBox.Items.Add("Call Asset Tag");
            ActionCmbBox.Items.Add("Call Inventory Tag");
            ActionCmbBox.Items.Add("E-mail Event Activity");

            ActionCmbBox.Size = new System.Drawing.Size(0, 0);
            ActionCmbBox.Location = new System.Drawing.Point(0, 0);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.ActionCmbBox });
            ActionCmbBox.SelectedIndexChanged += new System.EventHandler(this.ActionCmbSelected);
            ActionCmbBox.LostFocus += new System.EventHandler(HideControlFocusOver);
            ActionCmbBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HideControlKeyPress);
            ActionCmbBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            ActionCmbBox.BackColor = Color.SkyBlue;
            ActionCmbBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ActionCmbBox.Hide();

            //Action
            ActionInputCmbBox.Items.Add("");
            ActionInputCmbBox.Items.Add("Input 01 Timeout");
            ActionInputCmbBox.Items.Add("Input 02 Timeout");
            //ActiInputonCmbBox.Items.Add("Lock Door Relay 01");
            //ActiInputonCmbBox.Items.Add("Lock Door Relay 02");
            ActionInputCmbBox.Items.Add("Turn On Alarm Light Relay 01");
            ActionInputCmbBox.Items.Add("Turn On Alarm Light Relay 02");
            ActionInputCmbBox.Items.Add("Turn On Siren Relay 01");
            ActionInputCmbBox.Items.Add("Turn On Siren Relay 02");
            ActionInputCmbBox.Items.Add("Call Access Tag");
            ActionInputCmbBox.Items.Add("Call Asset Tag");
            ActionInputCmbBox.Items.Add("Call Inventory Tag");
            ActionInputCmbBox.Items.Add("E-mail Event Activity");

            ActionInputCmbBox.Size = new System.Drawing.Size(0, 0);
            ActionInputCmbBox.Location = new System.Drawing.Point(0, 0);
            this.Controls.Add(ActionInputCmbBox);
            ActionInputCmbBox.SelectedIndexChanged += new System.EventHandler(this.ActionInputCmbSelected);
            ActionInputCmbBox.LostFocus += new System.EventHandler(HideControlFocusOver);
            ActionInputCmbBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HideControlKeyPress);
            ActionInputCmbBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            ActionInputCmbBox.BackColor = Color.SkyBlue;
            ActionInputCmbBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ActionInputCmbBox.Hide();


            //Duration
            for (int i = 1; i < 60; i++)
                DurationCmbBox.Items.Add(i + " sec");

            DurationCmbBox.Items.Add("1 min");
            DurationCmbBox.Items.Add("2 min");
            DurationCmbBox.Items.Add("3 min");
            DurationCmbBox.Items.Add("forever");

            DurationCmbBox.Size = new System.Drawing.Size(0, 0);
            DurationCmbBox.Location = new System.Drawing.Point(0, 0);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.DurationCmbBox });
            DurationCmbBox.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            DurationCmbBox.LostFocus += new System.EventHandler(HideControlFocusOver);
            DurationCmbBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HideControlKeyPress);
            DurationCmbBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            DurationCmbBox.BackColor = Color.SkyBlue;
            DurationCmbBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DurationCmbBox.Hide();

            //Reader ID
            ReaderCmbBox.Items.Add("0");

            ReaderCmbBox.Size = new System.Drawing.Size(0, 0);
            ReaderCmbBox.Location = new System.Drawing.Point(0, 0);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.ReaderCmbBox });
            ReaderCmbBox.SelectedIndexChanged += new System.EventHandler(this.ReaderCmbSelected);
            ReaderCmbBox.Sorted = true;
            ReaderCmbBox.LostFocus += new System.EventHandler(HideControlFocusOver);
            ReaderCmbBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HideControlKeyPress);
            ReaderCmbBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            ReaderCmbBox.BackColor = Color.SkyBlue;
            ReaderCmbBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ReaderCmbBox.Hide();


            //FGen ID
            FGenCmbBox.Items.Add("0");
            FGenCmbBox.Text = "0";

            FGenCmbBox.Size = new System.Drawing.Size(0, 0);
            FGenCmbBox.Location = new System.Drawing.Point(0, 0);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.FGenCmbBox });
            FGenCmbBox.Sorted = true;
            FGenCmbBox.SelectedIndexChanged += new System.EventHandler(this.FGenCmbSelected);
            FGenCmbBox.LostFocus += new System.EventHandler(HideControlFocusOver);
            FGenCmbBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HideControlKeyPress);
            FGenCmbBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            FGenCmbBox.BackColor = Color.SkyBlue;
            FGenCmbBox.ForeColor = Color.White;
            FGenCmbBox.DropDownStyle = ComboBoxStyle.DropDownList;
            FGenCmbBox.Hide();

            //EditBox Location
            LocationLabel.Size = new System.Drawing.Size(0, 0);
            LocationLabel.Location = new System.Drawing.Point(0, 0);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.LocationLabel });
            //LocationLabel.SelectedIndexChanged += new System.EventHandler(this.ActionCmbSelected);         
            LocationLabel.LostFocus += new System.EventHandler(HideControlFocusOver);
            LocationLabel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HideControlKeyPress);
            LocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            LocationLabel.BackColor = Color.White;
            //LocationLabel.DropDownStyle = ComboBoxStyle.DropDownList;
            LocationLabel.Hide();


			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			

			this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																			this.columnHeader1, //action
																			this.columnHeader2, //duration
                                                                            this.columnHeader3, //reader ID
																			this.columnHeader4, //Fgen ID
                                                                            this.columnHeader5, //location                                                                            
																		  });
			this.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FullRowSelect = false;
			this.Name = "listView1";
			this.Size = new System.Drawing.Size(0,0);
			this.TabIndex = 0;
			this.View = System.Windows.Forms.View.Details;
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListBoxMouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListBoxMouseUp);
			this.DoubleClick += new System.EventHandler(this.ListBoxItemDoubleClick);
            this.ItemCheck += new ItemCheckEventHandler(this.ListViewItemCheck);
			this.GridLines = true ;
            this.CheckBoxes = false;
            this.View = View.Details;

            this.Items.Clear();

            for (int i = 0; i < MAX_ROW; i++)
            {
                ListViewItem item = new ListViewItem(""); //action
                //item.Checked = true;
                item.SubItems.Add("");  //duration
                item.SubItems.Add("");  //readerid
                item.SubItems.Add("");  //fgenid
                item.SubItems.Add("");  //location
                this.Items.Add(item);
            }
 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Action";
			this.columnHeader1.Width = 220;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Duration";
			this.columnHeader2.Width = 65;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Reader ID";
            this.columnHeader3.Width = 75;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "FGen ID";
            this.columnHeader4.Width = 75;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Location";
            this.columnHeader5.Width = 200;
            

            /*if (GetItemReaderComboBoxEvent != null)
            {
                int rID;
                int index = 0;

                while ((rID=GetItemReaderComboBoxEvent(index)) > 0)
                {
                    if (index == 0)
                        ReaderCmbBox.Items.Clear();

                    if (!ReaderCmbBox.Items.Contains(rID))
                        ReaderCmbBox.Items.Add(rID);
                         
                    index += 1;
                }
            }*/

            loadingThread = new Thread(LoadReader);
            loadingThread.Start();
											
		}

        private void ListViewItemCheck(object sender, ItemCheckEventArgs e)
        {
            //ColumnHeader col = (ColumnHeader)sender;
            //if (col.Text != "Action")

            /*if ((this.Columns[subItemSelected].Text == "Action") && itemClicked)
            {
                itemClicked = false;

                if (e.CurrentValue == CheckState.Checked)
                {
                    e.NewValue = CheckState.Unchecked;                    
                    li.Checked = false;
                }
                else
                {
                    e.NewValue = CheckState.Checked;                    
                    li.Checked = true;
                }
            }
            else if (itemClicked)
            {
                itemClicked = false;

                if (e.CurrentValue == CheckState.Checked)
                {
                    e.NewValue = CheckState.Checked;                    
                    li.Checked = true;
                }
                else
                {
                    e.NewValue = CheckState.Unchecked;                    
                    li.Checked = false;
                }
            }*/

        }

        public int GetListItem(int index, out string action, out ushort duration, out ushort rdr, out ushort fgen, out string loc)
        {
            action = "";
            duration = 0;
            rdr = 0;
            fgen = 0;
            loc = "";

            if (index >= this.Items.Count)
            {
                return (0);
            }
            else
            {
                if (this.Items[index].SubItems[0].Text.Length == 0)
                    return (-1);
                else if ((this.Items[index].SubItems[2].Text.Length == 0) ||  //rdr
                         (this.Items[index].SubItems[3].Text.Length == 0))    //fgen                 
                {
                    return (0);
                }
                else
                {
                    action = this.Items[index].SubItems[0].Text;

                    if (action != "E-mail Event Activity")
                    {
                        string dur_str = this.Items[index].SubItems[1].Text;
                        if (dur_str.Length == 0) //duration
                        {
                            return (0);
                        }

                        switch (dur_str)
                        {
                            case "forever":
                                duration = 0;
                                break;
                            case "3 min":
                                duration = 3 * 60;
                                break;
                            case "2 min":
                                duration = 2 * 60;
                                break;
                            case "1 min":
                                duration = 1 * 60;
                                break;
                            default:
                                duration = Convert.ToUInt16(dur_str.Substring(0, dur_str.IndexOf(' ')));
                                break;
                        }
                    }
                    rdr = Convert.ToUInt16(this.Items[index].SubItems[2].Text);
                    fgen = Convert.ToUInt16(this.Items[index].SubItems[3].Text);
                    loc = this.Items[index].SubItems[4].Text;
                    return (1);
                }
            }
        }

        private void LoadReader()
        {
            while (true)
            {
                if ((GetItemReaderComboBoxEvent != null) && (LoadReaderDataEvent != null))
                {
                    if (LoadReaderDataEvent())
                    {
                        int rID;
                        int index = 0;

                        while ((rID = GetItemReaderComboBoxEvent(index)) > 0)
                        {
                            if (index == 0)
                                ReaderCmbBox.Items.Clear();

                            if (!ReaderCmbBox.Items.Contains(rID))
                                ReaderCmbBox.Items.Add(rID);

                            index += 1;
                        }

                        return;
                    }
                }

                Thread.Sleep(250);

            }//while
        }

        public bool InputAction
        {
            set { bActionInput = value; }
            get { return bActionInput; }
        }

        #region AddItemActionComboBox(value)
        public void AddItemActionComboBox(string value)
        {
            ActionCmbBox.Items.Add(value);
        }
        #endregion

        #region AddItemDurationComboBox(value)
        public void AddItemDurationComboBox(string value)
        {
            DurationCmbBox.Items.Add(value);
        }
        #endregion

        #region AddItemReaderComboBox(value)
        public void AddItemReaderComboBox(string value)
        {
            ReaderCmbBox.Items.Add(value);
        }
        #endregion

        #region AddItemFGenComboBox(value)
        public void AddItemFGenComboBox(string value)
        {
            FGenCmbBox.Items.Add(value);
        }
        #endregion

        #region ClearActionComboBox()
        public void ClearActionComboBox()
        {
            ActionCmbBox.Items.Clear();
        }
        #endregion

        #region ClearDurationComboBox()
        public void ClearDurationComboBox()
        {
            DurationCmbBox.Items.Clear();
        }
        #endregion

        #region ClearReaderComboBox()
        public void ClearReaderComboBox()
        {
            ReaderCmbBox.Items.Clear();
        }
        #endregion

        #region ClearFGenComboBox()
        public void ClearFGenComboBox()
        {
            FGenCmbBox.Items.Clear();
        }
        #endregion

        public void HideControlKeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Escape) ||
                (e.KeyChar == (char)Keys.Enter))
            {
                (sender as Control).Hide();
            }
        }

		private void CmbSelected(object sender , System.EventArgs e)
		{
            int sel = DurationCmbBox.SelectedIndex;
			if ( sel >= 0 )
			{
                string itemSel = DurationCmbBox.Items[sel].ToString();
				li.SubItems[subItemSelected].Text = itemSel;
                if (DurationComboBoxValueChangedEvent != null)
                    DurationComboBoxValueChangedEvent(itemSel);
			}
		}

        private void ActionSelected(string action)
        {
            li.SubItems[subItemSelected].Text = action;
            if (ActionComboBoxValueChangedEvent != null)
            {
                ActionComboBoxValueChangedEvent(action);
                if (GetItemRdrFgenEvent != null)
                {
                    if (action == "")
                    {
                        li.SubItems[1].Text = "";
                        li.SubItems[2].Text = "";
                        li.SubItems[3].Text = "";
                        li.SubItems[4].Text = "";
                        LocationLabel.Text = "";
                    }
                    else
                    {
                        ushort fgen;
                        ushort rdr;
                        string loc;

                        GetItemRdrFgenEvent(out rdr, out fgen, out loc);

                        if (action == "E-mail Event Activity")
                            li.SubItems[1].Text = "";
                        else
                            li.SubItems[1].Text = "5 sec";
                        li.SubItems[2].Text = Convert.ToString(rdr);
                        li.SubItems[3].Text = Convert.ToString(fgen);
                        li.SubItems[4].Text = loc;
                        LocationLabel.Text = loc;
                    }
                }
            }
        }

        private void ActionInputCmbSelected(object sender, EventArgs e)
        {
            int sel = ActionInputCmbBox.SelectedIndex;
            if (sel >= 0)
            {
                string itemSel = ActionInputCmbBox.Items[sel].ToString();
                ActionSelected(itemSel);
            }
        }

        private void ActionCmbSelected(object sender, EventArgs e)
        {
            int sel = ActionCmbBox.SelectedIndex;
            if (sel >= 0)
            {
                string itemSel = ActionCmbBox.Items[sel].ToString();
                ActionSelected(itemSel);
            }
        }

        private void ReaderCmbSelected(object sender, System.EventArgs e)
        {
            int sel = ReaderCmbBox.SelectedIndex;
            if (sel >= 0)
            {
                string rdrStr = ReaderCmbBox.Items[sel].ToString();
                li.SubItems[subItemSelected].Text = rdrStr;
                if (ReaderComboBoxValueChangedEvent != null)
                {
                    ReaderComboBoxValueChangedEvent(rdrStr);

                    if (GetItemFGenComboBoxEvent != null)
                    {
                        int fgen;
                        int index = 0;
                        //string loc = "";

                        while ((fgen = GetItemFGenComboBoxEvent(Convert.ToUInt16(rdrStr), index)) != -1)
                        {
                            if (index == 0)
                                FGenCmbBox.Items.Clear();

                            if (fgen != -2)
                            {
                                FGenCmbBox.Items.Add(fgen);
                                //LocationLabel.Text = loc;
                            }

                            index += 1;
                        }

                        if (FGenCmbBox.Items.Count == 0)
                        {
                            FGenCmbBox.Items.Add("0");
                            FGenCmbBox.Text = "0";
                        }
                        else
                        {
                            FGenCmbBox.Text = FGenCmbBox.Items[0].ToString();
                        }
                    }
                }

                li.SubItems[subItemSelected].Text = rdrStr;
                li.SubItems[subItemSelected+1].Text = FGenCmbBox.Text;
            }
        }

        private void FGenCmbSelected(object sender, System.EventArgs e)
        {
            int sel = FGenCmbBox.SelectedIndex;
            if (sel >= 0)
            {
                string fgenStr = FGenCmbBox.Items[sel].ToString();
                if (this.Items[listSelectedRow].SubItems[2].Text.Length == 0)
                    return;
                ushort selectedRdr = Convert.ToUInt16(this.Items[listSelectedRow].SubItems[2].Text);
                li.SubItems[subItemSelected].Text = fgenStr;
                if (FGenComboBoxValueChangedEvent != null)
                {
                    FGenComboBoxValueChangedEvent(fgenStr);

                    if (GetItemFGenComboBoxEvent != null)
                    {
                        string loc = GetItemLocFGenComboBoxEvent(selectedRdr, Convert.ToUInt16(fgenStr));
                        LocationLabel.Text = loc;
                        li.SubItems[4].Text = loc;
                        
                        //int fgen;
                        //int index = 0;
                        //string loc = "";

                        //while ((fgen = GetItemFGenComboBoxEvent(Convert.ToUInt16(itemSel), index, out loc)) != -1)
                        //{
                            //if (index == 0)
                                //FGenCmbBox.Items.Clear();

                            //if (fgen != -2)
                            //{
                                //FGenCmbBox.Items.Add(fgen);
                                //LocationLabel.Text = loc;
                            //}

                            //index += 1;
                        //}
                    }
                }
            }
        }

        private void HideControlFocusOver(object sender, System.EventArgs e)
		{
            (sender as Control).Hide();
		}

		/*private void EditOver(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == 13 ) 
			{
				li.SubItems[subItemSelected].Text = editBox.Text;
				editBox.Hide();
			}

			if ( e.KeyChar == 27 ) 
				editBox.Hide();
		}*/

		/*private void FocusOver(object sender, System.EventArgs e)
		{
			li.SubItems[subItemSelected].Text = editBox.Text;
			editBox.Hide();
		}*/

        private Rectangle GetColumnLocation(int index)
        {
            Rectangle rect = new Rectangle(0, 0, 0, 0);           
            int spos = 0;
            int epos = 0; 

            for (int i = 0; i <= index; i++)
            {                
                spos = epos;                
                epos += this.Columns[i].Width;
            }

            rect.X = spos;
            rect.Width = this.Columns[index].Width;
            return (rect);

        }

		public  void ListBoxItemDoubleClick(object sender, System.EventArgs e)
		{
            // Check the subitem clicked .
            if (this.SelectedItems.Count == 0)
                return;

            listSelectedRow = this.SelectedItems[0].Index;
                     
			int nStart = X ;
			int spos = 0 ; 
			int epos = this.Columns[0].Width ;
			for ( int i=0; i < this.Columns.Count ; i++)
			{
				if ( nStart > spos && nStart < epos ) 
				{
					subItemSelected = i ;
					break; 
				}
				
				spos = epos ;
                if (i + 1 > Columns.Count - 1)
                    return;
				epos += this.Columns[i+1].Width;
			}

			string colName = this.Columns[subItemSelected].Text ;
            if (colName == "Action")
            {
                //li.Checked = true;
                ComboBox cbAction = bActionInput ? ActionInputCmbBox : ActionCmbBox;

                cbAction.Location = new System.Drawing.Point(spos, li.Bounds.Y);
                cbAction.Size = new System.Drawing.Size(this.Columns[0].Width, li.Bounds.Bottom - li.Bounds.Top);

                cbAction.SelectedItem = this.SelectedItems[0].SubItems[0].Text;

                cbAction.Show();
                //cmbBox.Text = subItemText;
                cbAction.SelectAll();
                cbAction.Focus();
            }
			else if ( colName == "Duration" ) 
			{
                if (this.SelectedItems[0].Text != "E-mail Event Activity")
                {
                    DurationCmbBox.Location = new System.Drawing.Point(spos, li.Bounds.Y);
                    DurationCmbBox.Size = new System.Drawing.Size(this.Columns[1].Width, li.Bounds.Bottom - li.Bounds.Top);

                    DurationCmbBox.SelectedItem = this.SelectedItems[0].SubItems[1].Text;

                    DurationCmbBox.Show();
                    //cmbBox.Text = subItemText;
                    DurationCmbBox.SelectAll();
                    DurationCmbBox.Focus();

                    //if ((this.ItemChecked) && (this.SelectedItems >= 0))
                    //if (this.Items[this.SelectedItems.].Checked)
                    //this.Items[this.SelectedItems].Checked = true;
                    //else
                    //this.Items[this.SelectedItems].Checked = false;
                }
			}
            else if (colName == "Reader ID")
            {                
                ReaderCmbBox.Location = new System.Drawing.Point(spos, li.Bounds.Y);
                ReaderCmbBox.Size = new System.Drawing.Size(this.Columns[2].Width, li.Bounds.Bottom - li.Bounds.Top);

                ReaderCmbBox.SelectedIndex = ReaderCmbBox.FindStringExact(this.SelectedItems[0].SubItems[2].Text);

                Rectangle rect = GetColumnLocation(4);
                LocationLabel.Location = new System.Drawing.Point(rect.X, li.Bounds.Y);
                LocationLabel.Size = new System.Drawing.Size(this.Columns[4].Width, li.Bounds.Bottom - li.Bounds.Top - 1);

                ReaderCmbBox.Show();
                //cmbBox.Text = subItemText;
                ReaderCmbBox.SelectAll();
                ReaderCmbBox.Focus();

//                LocationLabel.Show();
            }
            else if (colName == "FGen ID")
            {
                FGenCmbBox.Location = new System.Drawing.Point(spos, li.Bounds.Y);
                FGenCmbBox.Size = new System.Drawing.Size(this.Columns[3].Width, li.Bounds.Bottom - li.Bounds.Top);

                FGenCmbBox.SelectedIndex = FGenCmbBox.FindStringExact(this.SelectedItems[0].SubItems[3].Text);

                Rectangle rect = GetColumnLocation(4);
                LocationLabel.Location = new System.Drawing.Point(rect.X, li.Bounds.Y);
                LocationLabel.Size = new System.Drawing.Size(this.Columns[4].Width, li.Bounds.Bottom - li.Bounds.Top - 1);

                FGenCmbBox.Show();
                //cmbBox.Text = subItemText;
                FGenCmbBox.SelectAll();
                FGenCmbBox.Focus();

//                LocationLabel.Show();
            }
            //else if (colName == "Location")
            //{                
                //LocationLabel.Location = new System.Drawing.Point(spos, li.Bounds.Y);
                //LocationLabel.Size = new System.Drawing.Size(this.Columns[4].Width, li.Bounds.Bottom - li.Bounds.Top);
                //LocationLabel.Show();
            //}
			
		}

		public void ListBoxMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			li = this.GetItemAt(e.X , e.Y);
			X = e.X ;
			Y = e.Y ;
		}

        public void ListBoxMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            li = this.GetItemAt(e.X, e.Y);
            X = e.X;
            Y = e.Y;
        }

	}
}
