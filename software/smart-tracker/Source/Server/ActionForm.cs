using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;

namespace AWI.SmartTracker
{
    #region rdrFGenLocStruct
    [StructLayout(LayoutKind.Sequential)]
    public struct rdrFGenLocStruct
    {
        public ushort readerID;
        public ushort fGenID;
        public string location;
    }
    #endregion

    public partial class ActionForm : Form
    {
        #region Vars
        private MainForm mForm = null;
        private Boolean editMode = false;
        private ushort numRecords = 0;
        private bool loaded = false;
        private bool ActListChecked = false;
        private int lastSelectedIndex = -1;
        private int oldEvtActID = 0;
        private ArrayList rdrLocArrayList = new ArrayList();        
        #endregion

        #region Constructor ActionForm()
        public ActionForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Constructor ActionForm(Form)
        public ActionForm(MainForm form)
        {
            InitializeComponent();
            mForm = form;

            LoadEventActionData();
            LoadRdrLocArray();

            //this group of events must happen before next group
            //UserListControl.EditListView.GetItemActionComboBoxEvent += new UserListControl.GetItemActionComboBoxDelegate(GetItemActionComboBox);
            //UserListControl.EditListView.GetItemDurationComboBoxEvent += new UserListControl.GetItemDurationComboBoxDelegate(GetItemDurationComboBox);
            UserListControl.EditListView.GetItemReaderComboBoxEvent += new UserListControl.GetItemReaderComboBoxDelegate(GetItemReaderComboBox);
            UserListControl.EditListView.GetItemFGenComboBoxEvent += new UserListControl.GetItemFGenComboBoxDelegate(GetItemFGenComboBox);
            UserListControl.EditListView.GetItemLocFGenComboBoxEvent += new UserListControl.GetItemLocFGenComboBoxDelegate(GetItemLocFGenComboBox);

            //@@@CHG  Form1.m_closeWindowEvent += new CloseWindowDelegate(this.CloseWindow);
            UserListControl.EditListView.ActionComboBoxValueChangedEvent += new UserListControl.ActionComboBoxValueChangedDelegate(ActionComboBoxValueChanged);
            UserListControl.EditListView.DurationComboBoxValueChangedEvent += new UserListControl.DurationComboBoxValueChangedDelegate(DurationComboBoxValueChanged);
            UserListControl.EditListView.ReaderComboBoxValueChangedEvent += new UserListControl.ReaderComboBoxValueChangedDelegate(ReaderComboBoxValueChanged);
            UserListControl.EditListView.FGenComboBoxValueChangedEvent += new UserListControl.FGenComboBoxValueChangedDelegate(FGenComboBoxValueChanged);
            UserListControl.EditListView.LoadReaderDataEvent += new UserListControl.LoadReaderDataDelegate(LoadReaderData);
            UserListControl.EditListView.GetItemRdrFgenEvent += new UserListControl.GetItemRdrFgenDelegate(GetItemRdrFgen);
            //UserListControl.EditListView.GetListItemEvent += new UserListControl.GetListItemDelegate(GetListItem);
            
            if (EventActionListView.Items.Count > 0)
            {
                EditToolBarButton.Enabled = true;
                DeleteToolBarButton.Enabled = true;
            }

            //for (int i = 0; i < 8; i++)
            //{
                //ListViewItem item = ActionListView.SelectedItems[0];
                //FGenIDComboBox.Text = selItem.SubItems[1].Text;
              

            //}

            //userListViewControl1.Items[0].Selected = true;

            EventsComboBox.Items.Remove("Input Detected");
        }
        #endregion

        #region CloseWindow()
        private void CloseWindow()
        {
            Close();
        }
        #endregion

        #region LoadReaderData()
        public bool LoadReaderData()
        {
            return (loaded);
        }
        #endregion

        #region GetItemRdrFgen (rdr, fgen, loc)
        private void GetItemRdrFgen(out ushort rdr, out ushort fgen, out string loc)
        {
            if (ReaderIDComboBox.Text.Length == 0)
                rdr = 0;
            else
                rdr = Convert.ToUInt16(ReaderIDComboBox.Text);

            if (FGenIDComboBox.Text.Length == 0)
                fgen = 0;
            else
                fgen = Convert.ToUInt16(FGenIDComboBox.Text);

            if (LocationTextBox.Text.Length == 0)
                loc = "";
            else
                loc = LocationTextBox.Text;
        }
        #endregion

        #region ActionComboBoxValueChanged(value)
        private void ActionComboBoxValueChanged(string value)
        {
            if (value == "Display Tag Moving Direction")
                LabelTextBox.Text = "To get the direction the tag is moving at least two actions 'Display Tag Moving Direction' should be configured to represent tag moving from one location to another";
            else
                LabelTextBox.Text = "";
        }
        #endregion

        #region DurationComboBoxValueChanged(value)
        private void DurationComboBoxValueChanged(string value)
        {

        }
        #endregion

        #region ReaderComboBoxValueChanged(value)
        private void ReaderComboBoxValueChanged(string value)
        {
            //
        }
        #endregion

        #region FGenComboBoxValueChanged(value)
        private void FGenComboBoxValueChanged(string value)
        {

        }
        #endregion

        #region int GetItemReaderComboBox(index)
        private int GetItemReaderComboBox(int index)
        {
            if (rdrLocArrayList.Count < index + 1)
                return (-1);
            else
            {
                rdrFGenLocStruct rdrLoc = new rdrFGenLocStruct();
                rdrLoc = (rdrFGenLocStruct)rdrLocArrayList[index];
                return (Convert.ToInt32(rdrLoc.readerID));
            }
        }
        #endregion

        #region int GetItemFGenComboBox(rdr)
        private int GetItemFGenComboBox(ushort rdr, int index)
        {
            //loc = "";
            if (rdrLocArrayList.Count < index + 1)
                return (-1);
            else
            {
                rdrFGenLocStruct rdrLoc = new rdrFGenLocStruct();
                rdrLoc = (rdrFGenLocStruct)rdrLocArrayList[index];
                if (rdrLoc.readerID == rdr)
                {
                    //loc = rdrLoc.location;
                    return (Convert.ToInt32(rdrLoc.fGenID));
                }
                else
                    return (-2);
            }
        }
        #endregion

        #region ushort GetItemLocFGenComboBox(rdr, fgen)
        private string GetItemLocFGenComboBox(ushort rdr, ushort fgen)
        {            
            foreach (rdrFGenLocStruct rdrLoc in rdrLocArrayList)
            {
                if ((rdrLoc.readerID == rdr) && (rdrLoc.fGenID == fgen))
                {
                    if (rdrLoc.location.Length > 0)
                        return (rdrLoc.location);
                    else
                        return ("");
                }
            }
            
            return ("");            
        }
        #endregion

        #region SaveEventAction()
        private bool SaveEventAction(bool editing, int evtActID, string desc, ushort rdrID, ushort fgenID, string location, int eventID, DateTime time, int oldEvtActID)
        {
            //string SQL = "";
            StringBuilder sql = new StringBuilder();

            lock (MainForm.m_connection)
            {
                if (!editing)  //saving
                {
                    sql.Append("INSERT INTO eventaction (EventActionID, Description, ReaderID, FGenID, Location, EventID, Timestamp) VALUES (");
                    sql.AppendFormat(" '{0}', ", evtActID);
                    sql.AppendFormat(" '{0}', ", desc);
                    sql.AppendFormat(" '{0}', ", rdrID);
                    sql.AppendFormat(" '{0}', ", fgenID);
                    sql.AppendFormat(" '{0}', ", location);
                    sql.AppendFormat(" '{0}', ", eventID);
                    sql.AppendFormat(" '{0}') ", time);
                    
                }
                else  //editing
                {
                    sql.Append("UPDATE eventaction SET");
                    sql.AppendFormat(" EventActionID = '{0}',", evtActID);
                    sql.AppendFormat(" Description = '{0}',", desc);
                    sql.AppendFormat(" ReaderID = '{0}',", rdrID);
                    sql.AppendFormat(" FGenID = '{0}',", fgenID);
                    sql.AppendFormat(" Location = '{0}',", location);
                    sql.AppendFormat(" EventID = '{0}',", eventID);
                    sql.AppendFormat(" Timestamp = '{0}'", time);
                    sql.AppendFormat(" WHERE EventActionID = '{0}'", oldEvtActID);   
                }

                if (!(bool)this.Invoke(new RunNonQueryCmdCallback(mForm.RunNonQueryCmd), new object[] { sql.ToString() }))
                {
                    Console.WriteLine("Inserting the record failed in EventAction table");
                    MessageBox.Show(this, "Saving the record failed.", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                return (true);
            }
        }
        #endregion

        #region SaveActionDef()
        private bool SaveActionDef(bool editing, int evtActID, int actID, int dur, ushort rdrID, ushort fgenID, string loc, int oldEvtActID, int index)
        {            
            //string SQL = "";
            StringBuilder sql = new StringBuilder();

            lock (MainForm.m_connection)
            {
                //if (!editing)  //saving
                {
                    sql.Append("INSERT INTO actiondef (EventActionID, ActionID, Duration, ReaderID, FGenID, Location, ActionIndex) VALUES (");
                    sql.AppendFormat(" '{0}', ", evtActID);
                    sql.AppendFormat(" '{0}', ", actID);
                    sql.AppendFormat(" '{0}', ", dur);
                    sql.AppendFormat(" '{0}', ", rdrID);
                    sql.AppendFormat(" '{0}', ", fgenID);
                    sql.AppendFormat(" '{0}', ", loc);
                    sql.AppendFormat(" '{0}') ", index);

                }
                /*else  //editing
                {
                    sql.Append("UPDATE actiondef SET");
                    sql.AppendFormat(" EventActionID = '{0}',", evtActID);
                    sql.AppendFormat(" ActionID = '{0}',", actID);
                    sql.AppendFormat(" Duration = '{0}',", dur);
                    sql.AppendFormat(" ReaderID = '{0}',", rdrID);
                    sql.AppendFormat(" FGenID = '{0}',", fgenID);
                    sql.AppendFormat(" Location = '{0}'", loc);
                    sql.AppendFormat(" WHERE EventActionID = '{0}'", oldEvtActID);
                }*/

                if (!(bool)this.Invoke(new RunNonQueryCmdCallback(mForm.RunNonQueryCmd), new object[] { sql.ToString() }))
                {
                    Console.WriteLine("Inserting the record failed in ActionDef table");
                    MessageBox.Show(this, "Saving the record failed.", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                return (true);
            }
        }
        #endregion

        #region toolbar1_ButtonClick
        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {             
            if (e.Button.Text == "New")
            {
                editMode = false;
                LabelTextBox.Text = "";

                SaveToolBarButton.Enabled = true;
                CancelToolBarButton.Enabled = true;
                EditToolBarButton.Enabled = false;
                DeleteToolBarButton.Enabled = false;
                RefreshToolBarButton.Enabled = false;

                ReaderIDComboBox.Enabled = true;
                EventsComboBox.Enabled = true;
                FGenIDComboBox.Enabled = true;
                EventActionIDTextBox.ReadOnly = false;
                DescTextBox.ReadOnly = false;                
                userListViewControl1.Enabled = true;
                ActionsListView.Items.Clear();

                //for (int i = 0; i < userListViewControl1.Items.Count; i++)
                    //userListViewControl1.Items[i].Checked = false;

                ReaderIDComboBox.Text = "";
                EventsComboBox.Text = "";
                LocationTextBox.Text = "";
                ActionDateTextBox.Text = "";
                FGenIDComboBox.Text = "";
                EventActionIDTextBox.Text = "";
                DescTextBox.Text = "";
                
            }
            else if (e.Button.Text == "Edit")
            {
                editMode = true;
                LabelTextBox.Text = "";
                SaveToolBarButton.Enabled = true;
                NewToolBarButton.Enabled = false;
                RefreshToolBarButton.Enabled = false;
                DeleteToolBarButton.Enabled = false;

                oldEvtActID = Convert.ToInt32(EventActionIDTextBox.Text);
                ReaderIDComboBox.Enabled = true;
                FGenIDComboBox.Enabled = true;
                EventsComboBox.Enabled = true;
                EventActionIDTextBox.ReadOnly = false;
                DescTextBox.ReadOnly = false;
                userListViewControl1.Enabled = true;
                
                //go though ActionsListView and based on that update userListView control
                userListViewControl1.Enabled = true;
                //if (userListViewControl1.listView1.Items.Count > 0)
                {
                    for (int i = 0; i < ActionsListView.Items.Count; i++)
                    {
                        userListViewControl1.listView1.Items[i].SubItems[0].Text = ActionsListView.Items[i].SubItems[0].Text;
                        userListViewControl1.listView1.Items[i].SubItems[1].Text = ActionsListView.Items[i].SubItems[1].Text;
                        userListViewControl1.listView1.Items[i].SubItems[2].Text = ActionsListView.Items[i].SubItems[2].Text;
                        userListViewControl1.listView1.Items[i].SubItems[3].Text = ActionsListView.Items[i].SubItems[3].Text;
                        userListViewControl1.listView1.Items[i].SubItems[4].Text = ActionsListView.Items[i].SubItems[4].Text;
                    }
                }
            }
            else if (e.Button.Text == "Cancel")
            {
                editMode = false;
                EditToolBarButton.Enabled = true;
                NewToolBarButton.Enabled = true;
                RefreshToolBarButton.Enabled = true;

                SaveToolBarButton.Enabled = false;
                ReaderIDComboBox.Enabled = false;
                FGenIDComboBox.Enabled = false;
                EventsComboBox.Enabled = false;
                EventActionIDTextBox.ReadOnly = true;
                DescTextBox.ReadOnly = true;
                
                userListViewControl1.Enabled = false;
                LabelTextBox.Text = "";
                //userListViewControl1.listView1.Items.Clear();

                //for (int i = 0; i < userListViewControl1.Items.Count; i++)
                    //userListViewControl1.Items[i].Checked = false;
                for (int i = 0; i < 20; i++)
                {
                    //userListViewControl1.listView1.Items[i].Checked = false;
                    userListViewControl1.listView1.Items[i].SubItems[0].Text = "";
                    userListViewControl1.listView1.Items[i].SubItems[1].Text = "";
                    userListViewControl1.listView1.Items[i].SubItems[2].Text = "";
                    userListViewControl1.listView1.Items[i].SubItems[3].Text = "";
                    userListViewControl1.listView1.Items[i].SubItems[4].Text = "";
                    userListViewControl1.listView1.LocationLabel.Text = "";
                    userListViewControl1.listView1.LocationLabel.Hide();
                    userListViewControl1.listView1.Items[i].Selected = false;
                }


                if (EventActionListView.Items.Count <= 0)
                {
                    ReaderIDComboBox.Text = "";
                    FGenIDComboBox.Text = "";
                    EventsComboBox.Text = "";
                    LocationTextBox.Text = "";
                    ActionDateTextBox.Text = "";
                    EventActionIDTextBox.Text = "";
                    DescTextBox.Text = "";

                    EditToolBarButton.Enabled = false;
                    DeleteToolBarButton.Enabled = false;
                    SaveToolBarButton.Enabled = false;
                    CancelToolBarButton.Enabled = false;
                    RefreshToolBarButton.Enabled = false;

                    return;
                }

                if (EventActionIDTextBox.Text.Length > 0)
                    LoadActionDef(Convert.ToInt32(EventActionIDTextBox.Text));

                if (EventActionListView.SelectedItems.Count > 0)
                {
                    ListViewItem selItem = EventActionListView.SelectedItems[0];
                    string evID = selItem.SubItems[0].Text.ToString();
                    if (evID.Length > 0)
                        LoadEventActionData(evID);
                }

                

                //DescTextBox.Text = selItem.SubItems[0].Text;


                //go though ActionsListView and based on that update userListView control

                /*FGenIDComboBox.Text = selItem.SubItems[1].Text;
                //RelayIDComboBox.Text = selItem.SubItems[2].Text;
                LocationTextBox.Text = selItem.SubItems[3].Text;
                EventsComboBox.Text = selItem.SubItems[4].Text;
                //ActionsComboBox.Text = selItem.SubItems[5].Text;           
                ActionDateTextBox.Text = selItem.SubItems[7].Text;*/
               
            }
            else if (e.Button.Text == "Save")
            {
                //EditToolBarButton.Enabled = true;
                NewToolBarButton.Enabled = true;

                if (EventActionIDTextBox.Text.Length == 0)
                {
                    MessageBox.Show(this, "No Event Action ID is selected", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (ReaderIDComboBox.Text.Length == 0)
                {
                    MessageBox.Show(this, "No Reader ID is selected", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else if (FGenIDComboBox.Text.Length == 0)
                {
                    MessageBox.Show(this, "No FGen ID is Selected", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }                
                else if (EventsComboBox.Text.Length == 0)
                {
                    MessageBox.Show(this, "No Event is selected.", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (!CheckActionItemsParams())
                    return;

                //for (int i = 0; i < 20; i++)
                //{
                    //if (userListViewControl1.listView1.GetListItem(i, out action, out duration, out rdr, out fgen, out loc) == 1)
                    //{                

                ReaderIDComboBox.Enabled = false;
                FGenIDComboBox.Enabled = false;
                EventsComboBox.Enabled = false;
                EventActionIDTextBox.ReadOnly = true;
                DescTextBox.ReadOnly = true;
                userListViewControl1.Enabled = false;
                DateTime dtime = DateTime.Now;
                int eType = GetEventID(EventsComboBox.Text);

                if (!SaveEventAction(editMode, 
                                Convert.ToInt32(EventActionIDTextBox.Text),
                                DescTextBox.Text,
                                Convert.ToUInt16(ReaderIDComboBox.Text),
                                Convert.ToUInt16(FGenIDComboBox.Text),
                                LocationTextBox.Text,
                                eType, //Convert.ToInt32(EventsComboBox.Text),
                                dtime,
                                oldEvtActID
                    ))

                {
                    MessageBox.Show(this, "Save the record failed.", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                SaveToolBarButton.Enabled = false;
                EditToolBarButton.Enabled = true;
                RefreshToolBarButton.Enabled = true;
                DeleteToolBarButton.Enabled = true;
               

                ListViewItem listItem1 = null;
                int index = 0;
                if (EventActionListView.SelectedItems.Count > 0)
                {
                    listItem1 = EventActionListView.SelectedItems[0];
                    index = listItem1.Index;
                }
                else
                    index = -1;

                if ((editMode) && (index >= 0)) //edit
                {
                    //edit the 
                    EventActionListView.Items[index].SubItems[0].Text = EventActionIDTextBox.Text;
                    EventActionListView.Items[index].SubItems[1].Text = ReaderIDComboBox.Text;
                    EventActionListView.Items[index].SubItems[2].Text = FGenIDComboBox.Text;
                    EventActionListView.Items[index].SubItems[3].Text = EventsComboBox.Text;
                    EventActionListView.Items[index].SubItems[4].Text = LocationTextBox.Text;
                    EventActionListView.Items[index].SubItems[5].Text = DescTextBox.Text;
                    EventActionListView.Items[index].SubItems[6].Text = ActionDateTextBox.Text;            
                }
                else  //save
                {
                    ListViewItem listItem = new ListViewItem(EventActionIDTextBox.Text);
                    listItem.SubItems.Add(ReaderIDComboBox.Text);
                    listItem.SubItems.Add(FGenIDComboBox.Text);
                    listItem.SubItems.Add(EventsComboBox.Text);
                    listItem.SubItems.Add(LocationTextBox.Text);
                    listItem.SubItems.Add(DescTextBox.Text);
                    listItem.SubItems.Add(ActionDateTextBox.Text);                
                    listItem.Selected = true;
                    EventActionListView.Items.Add(listItem);
                }

                string action = "";
                ushort duration = 0;
                ushort rdr = 0;
                ushort fgen = 0;
                string loc = "";
                //int actionIndex = 0;
                int actionID = 0;

                ClearActionDefTable(EventActionIDTextBox.Text);
                
                for (int i = 0; i < 20; i++)
                {
                    if (userListViewControl1.listView1.GetListItem(i, out action, out duration, out rdr, out fgen, out loc) == 1)
                    {
                        //check actionID and rdr if these two 
                        actionID = GetActionID(action);
                        if (!SaveActionDef(editMode,
                                           Convert.ToInt32(EventActionIDTextBox.Text),
                                           actionID,
                                           duration,
                                           rdr,
                                           fgen,
                                           loc,
                                           oldEvtActID,
                                           i
                            ))
                        {
                            MessageBox.Show(this, "Save the record failed.", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            continue;
                        }
                        else
                        {
                            mForm.LoadActionList();
                        }

                        if (i == 0)
                            ActionsListView.Items.Clear();

                        ListViewItem listItem2 = new ListViewItem(action);
                        if (duration == 0)
                        {
                            if (actionID == MainForm.EVENT_EMAIL_ACTIVITY)
                            {
                                listItem2.SubItems.Add("");
                            }
                            else
                            {
                                listItem2.SubItems.Add("forever");
                            }
                        }
                        else if (duration < 60)
                        {
                            listItem2.SubItems.Add(Convert.ToString(duration) + " sec");
                        }
                        else
                        {
                            listItem2.SubItems.Add(Convert.ToString(duration / 60) + " min");
                        }
                        listItem2.SubItems.Add(Convert.ToString(rdr));
                        listItem2.SubItems.Add(Convert.ToString(fgen));
                        listItem2.SubItems.Add(loc);
                        listItem2.SubItems.Add(Convert.ToString(i));
                        ActionsListView.Items.Add(listItem2);                     
                    } //for loop
                }

                EditToolBarButton.Enabled = true;
                DeleteToolBarButton.Enabled = true;                
                ActionDateTextBox.Text = dtime.ToString("MM-dd-yyyy HH:mm:ss");

                /*if (editMode)
                {
                    ListViewItem selItem = EventActionListView.SelectedItems[0];
                    int index = selItem.Index;
                    EventActionListView.Items.RemoveAt(selItem.Index);
                    //EventActionListView.Items.Insert(index, listItem);
                }*/

                LabelTextBox.Text = "";
                editMode = false;

                for (int i = 0; i < 20; i++)
                {
                    userListViewControl1.listView1.Items[i].Checked = false;
                    userListViewControl1.listView1.Items[i].SubItems[0].Text = "";
                    userListViewControl1.listView1.Items[i].SubItems[1].Text = "";
                    userListViewControl1.listView1.Items[i].SubItems[2].Text = "";
                    userListViewControl1.listView1.Items[i].SubItems[3].Text = "";
                    userListViewControl1.listView1.Items[i].SubItems[4].Text = "";
                    userListViewControl1.listView1.LocationLabel.Text = "";
                    userListViewControl1.listView1.LocationLabel.Hide();
                    userListViewControl1.listView1.Items[i].Selected = false;
                }

                //refreshing the screen after edit
                if (EventActionListView.Items.Count <= 0)
                {
                    ReaderIDComboBox.Text = "";
                    FGenIDComboBox.Text = "";
                    EventsComboBox.Text = "";
                    LocationTextBox.Text = "";
                    ActionDateTextBox.Text = "";
                    EventActionIDTextBox.Text = "";
                    DescTextBox.Text = "";
                }

                if (EventActionListView.SelectedItems.Count > 0)
                {
                    ListViewItem selItem = EventActionListView.SelectedItems[0];
                    EventActionIDTextBox.Text = selItem.SubItems[0].Text;
                    DescTextBox.Text = selItem.SubItems[0].Text;
                }

                ReaderIDComboBox.Text = "";
                FGenIDComboBox.Text = "";
                EventsComboBox.Text = "";
                LocationTextBox.Text = "";
                ActionDateTextBox.Text = "";

                LoadEventActionData();

            }
            else if (e.Button.Text == "Delete")
            {
                editMode = false;
                EditToolBarButton.Enabled = true;
                NewToolBarButton.Enabled = true;

                ReaderIDComboBox.Enabled = false;
                FGenIDComboBox.Enabled = false;               
                EventsComboBox.Enabled = false;
                EventActionIDTextBox.ReadOnly = true;
                DescTextBox.ReadOnly = true;

                //ActionTimeComboBox.Enabled = false;

                userListViewControl1.Enabled = false;
                //for (int i = 0; i < userListViewControl1.Items.Count; i++)
                    //userListViewControl1.Items[i].Checked = false;

                if (MessageBox.Show(this, "Delete the record?", "Retail", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                StringBuilder sql = new StringBuilder();
                sql.Append("DELETE FROM eventaction WHERE EventActionID = ");
                sql.AppendFormat("'{0}'", EventActionIDTextBox.Text);
                //sql.Append(" AND RelayID = ");
                //sql.AppendFormat("'{0}'", "0"); //RelayIDComboBox.Text); need to get fix

                //>>>>OdbcCommand myCommand = new OdbcCommand(sql.ToString(), Form1.m_connection);
                if (!mForm.RunNonQueryCmd(sql.ToString()))
                {
                    Console.WriteLine("Deleting the record failed in EventAction table");
                    MessageBox.Show(this, "deleting the record failed. ", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                sql.Length = 0;
                sql.Append("DELETE FROM actionDef WHERE EventActionID = ");
                sql.AppendFormat("'{0}'", EventActionIDTextBox.Text);
                if (!mForm.RunNonQueryCmd(sql.ToString()))
                {
                    Console.WriteLine("Deleting the record failed in EventAction table");
                    MessageBox.Show(this, "Deleting the record failed. ", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                LabelTextBox.Text = "";

               /* if (EventActionListView.Items.Count == 0)
                {
                    ReaderIDComboBox.Enabled = false;
                    FGenIDComboBox.Enabled = false;
                    EventsComboBox.Enabled = false;                  

                    ReaderIDComboBox.Text = "";
                    FGenIDComboBox.Text = "";
                    EventsComboBox.Text = "";
                    LocationTextBox.Text = "";                    
                    ActionDateTextBox.Text = "";
                    
                    EditToolBarButton.Enabled = false;
                    DeleteToolBarButton.Enabled = false;

                    ActionsListView.Items.Clear();
                }
                else
                {*/
                ListViewItem selItem = EventActionListView.SelectedItems[0];
                int index = selItem.Index;
                selItem.Remove();
                if (index > 0)
                {
                    EventActionListView.Items[index - 1].Selected = true;
                    EventActionListView.Select();
                }
                else if ((index == 0) && (EventActionListView.Items.Count > 0))
                {
                    EventActionListView.Items[0].Selected = true;
                    EventActionListView.Select();
                }
                else if (EventActionListView.Items.Count == 0)
                {
                    ReaderIDComboBox.Enabled = false;
                    FGenIDComboBox.Enabled = false;
                    EventsComboBox.Enabled = false;                  

                    ReaderIDComboBox.Text = "";
                    FGenIDComboBox.Text = "";
                    EventsComboBox.Text = "";
                    LocationTextBox.Text = "";                    
                    ActionDateTextBox.Text = "";
                    EventActionIDTextBox.Text = "";
                    DescTextBox.Text = "";
                    
                    EditToolBarButton.Enabled = false;
                    DeleteToolBarButton.Enabled = false;
                    SaveToolBarButton.Enabled = false;
                    CancelToolBarButton.Enabled = false;
                    RefreshToolBarButton.Enabled = false;

                    ActionsListView.Items.Clear();
                }

                for (int i = 0; i < 20; i++)
                {
                    //userListViewControl1.listView1.Items[i].Checked = false;
                    userListViewControl1.listView1.Items[i].SubItems[0].Text = "";
                    userListViewControl1.listView1.Items[i].SubItems[1].Text = "";
                    userListViewControl1.listView1.Items[i].SubItems[2].Text = "";
                    userListViewControl1.listView1.Items[i].SubItems[3].Text = "";
                    userListViewControl1.listView1.Items[i].SubItems[4].Text = "";
                    userListViewControl1.listView1.LocationLabel.Text = "";
                    userListViewControl1.listView1.LocationLabel.Hide();
                    userListViewControl1.listView1.Items[i].Selected = false;
                }
                //========================}
                /*else if (ActionListView.Items.Count == 0)
                {
                    ReaderIDComboBox.Enabled = false;
                    FGenIDComboBox.Enabled = false;
                    RelayIDComboBox.Enabled = false;
                    EventsComboBox.Enabled = false;
                    ActionsComboBox.Enabled = false;
                    ActionTimeComboBox.Enabled = false;

                    ReaderIDComboBox.Text = "";
                    FGenIDComboBox.Text = "";
                    RelayIDComboBox.Text = "";
                    EventsComboBox.Text = "";
                    ActionsComboBox.Text = "";
                    LocationTextBox.Text = "";
                    ActionTimeComboBox.Text = "";
                    ActionDateTextBox.Text = "";
                }*/              
            }
            else if (e.Button.Text == "Refresh")
            {
                editMode = false;
                EditToolBarButton.Enabled = true;
                NewToolBarButton.Enabled = true;

                ReaderIDComboBox.Enabled = false;
                FGenIDComboBox.Enabled = false;
                EventsComboBox.Enabled = false;
                EventActionIDTextBox.ReadOnly = true;
                DescTextBox.ReadOnly = true;

                userListViewControl1.Enabled = false;
        
                if (EventActionListView.Items.Count <= 0)
                {
                    ReaderIDComboBox.Text = "";
                    FGenIDComboBox.Text = "";
                    EventsComboBox.Text = "";
                    LocationTextBox.Text = "";
                    ActionDateTextBox.Text = "";
                    EventActionIDTextBox.Text = "";
                    DescTextBox.Text = "";              
                }

                if (EventActionListView.SelectedItems.Count > 0)
                {
                    ListViewItem selItem = EventActionListView.SelectedItems[0];
                    EventActionIDTextBox.Text = selItem.SubItems[0].Text;
                    DescTextBox.Text = selItem.SubItems[0].Text;
                }
            
                ReaderIDComboBox.Text = "";
                FGenIDComboBox.Text = "";
                EventsComboBox.Text = "";
                LocationTextBox.Text = "";
                ActionDateTextBox.Text = "";                

                LoadEventActionData();
                LabelTextBox.Text = "";

            }//else if refresh
        }//toolbar1_click
        #endregion

        #region ClearActionDefTable (eventAction)
        private void ClearActionDefTable (string eventAction)
        {
            StringBuilder sqlCmd = new StringBuilder();
            sqlCmd.Append("DELETE FROM actiondef WHERE EventActionID = ");
            sqlCmd.AppendFormat("'{0}'", eventAction);

            if (!mForm.RunNonQueryCmd(sqlCmd.ToString()))
            {
                Console.WriteLine("Deleting the record failed in actiondef table");
                MessageBox.Show(this, "deleting the record failed. ", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion        

        #region GetEventID(event)
        private int GetEventID(string eStr)
        {
            int eID = 0;
            switch (eStr.ToUpper())
            {
                case "BREACH ALARM":
                    eID = MainForm.BREACH_ALARM_EVENT;
                break;

                case "TAMPER ALARM":
                    eID = MainForm.TAMPER_ALARM_EVENT;
                break;

                case "ALERT":
                    eID = MainForm.ALERT_EVENT;
                break;

                case "TAG DETECTED":
                    eID = MainForm.TAG_DETECTED;
                break;

                case "INVALID TAG DETECTED":
                    eID = MainForm.INVALID_TAG_DETECTED;
                break;

                case "INPUT DETECTED":
                eID = MainForm.INPUT_DETECTED;
                break;
            }

            return (eID);
        }
        #endregion

        #region GetEventStr(eventID)
        public static string GetEventStr(int eID)
        {
            string eStr = "";
            switch (eID)
            {
                case MainForm.BREACH_ALARM_EVENT:
                    eStr = "Breach Alarm";
                 break;

                case MainForm.TAMPER_ALARM_EVENT:
                    eStr = "Tamper Alarm";
                break;

                case MainForm.ALERT_EVENT:
                    eStr = "Alert";
                break;

                case MainForm.TAG_DETECTED:
                    eStr = "Tag Detected";
                break;

                case MainForm.INVALID_TAG_DETECTED:
                    eStr = "Invalid Tag Detected";
                break;

                case MainForm.INPUT_DETECTED:
                    eStr = "Input Detected";
                break;
            }

            return (eStr);
        }
        #endregion

        #region GetActionID(event)
        private int GetActionID(string aStr)
        {
            int aID = 0;
            
            switch (aStr)
            {
                case "Turn On Alarm Light Relay 01":
                    aID = MainForm.TURN_ON_ALARM_LIGHT_RELAY_01;
                break;

                case "Turn On Alarm Light Relay 02":
                    aID = MainForm.TURN_ON_ALARM_LIGHT_RELAY_02;
                break;

                case "Turn On Siren Relay 01":
                    aID = MainForm.TURN_ON_SIREN_RELAY_01;
                break;

                case "Turn On Siren Relay 02":
                    aID = MainForm.TURN_ON_SIREN_RELAY_02;
                break;

                case "Unlock Door Relay 01":
                    aID = MainForm.UNLOCK_DOOR_RELAY_01;
                break;

                case "Unlock Door Relay 02":
                    aID = MainForm.UNLOCK_DOOR_RELAY_02;
                break;

                case "Display Tag Moving Direction":
                    aID = MainForm.DISPLAY_TAG_MOVING_DIRECTION;
                break;

                case "Input 01 Timeout":
                    aID = MainForm.EVENT_INPUT_01;
                break;

                case "Input 02 Timeout":
                    aID = MainForm.EVENT_INPUT_02;
                break;

                case "Call Access Tag":
                    aID = MainForm.EVENT_CALL_TAG_ACCESS;
                break;

                case "Call Asset Tag":
                    aID = MainForm.EVENT_CALL_TAG_ASSET;
                break;

                case "Call Inventory Tag":
                    aID = MainForm.EVENT_CALL_TAG_INVENTORY;
                break;

                case "E-mail Event Activity":
                    aID = MainForm.EVENT_EMAIL_ACTIVITY;
                break;
            }

            return (aID);
        }
        #endregion

        #region GetActionStr(event)
        public static string GetActionStr(int aID)
        {
            string aStr = "";

            switch (aID)
            {
                case MainForm.TURN_ON_ALARM_LIGHT_RELAY_01: 
                    aStr = "Turn On Alarm Light Relay 01";
                break;

                case MainForm.TURN_ON_ALARM_LIGHT_RELAY_02:
                    aStr = "Turn On Alarm Light Relay 02";
                break;

                case MainForm.TURN_ON_SIREN_RELAY_01:
                    aStr = "Turn On Siren Relay 01";
                break;

                case MainForm.TURN_ON_SIREN_RELAY_02:
                    aStr = "Turn On Siren Relay 02"; 
                break;

                case MainForm.UNLOCK_DOOR_RELAY_01:
                    aStr = "Unlock Door Relay 01";
                break;

                case MainForm.UNLOCK_DOOR_RELAY_02:
                    aStr = "Unlock Door Relay 02";
                break;

                case MainForm.DISPLAY_TAG_MOVING_DIRECTION:
                    aStr = "Display Tag Moving Direction";
                break;

                case MainForm.EVENT_INPUT_01:
                    aStr = "Input 01 Timeout";
                break;

                case MainForm.EVENT_INPUT_02:
                    aStr = "Input 02 Timeout";
                break;

                case MainForm.EVENT_CALL_TAG_ACCESS:
                    aStr = "Call Access Tag";
                break;

                case MainForm.EVENT_CALL_TAG_ASSET:
                    aStr = "Call Asset Tag";
                break;

                case MainForm.EVENT_CALL_TAG_INVENTORY:
                    aStr = "Call Inventory Tag";
                break;

                case MainForm.EVENT_EMAIL_ACTIVITY:
                    aStr = "E-mail Event Activity";
                break;
            }

            return (aStr);
        }
        #endregion

        #region CheckActionItemsParams()
        private bool CheckActionItemsParams()
        {
            string action = "";
            ushort duration = 0;
            ushort rdr = 0;
            ushort fgen = 0;
            string loc = "";
            int ret = 0;
            int index = 0;
            int actID = 0;

            int[,] act = new int[20, 2];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 2; j++)
                    act[i,j] = 0;

            for (int i = 0; i < 20; i++)
            {
                ret = userListViewControl1.listView1.GetListItem(i, out action, out duration, out rdr, out fgen, out loc);
                if (ret == 0)
                {
                    MessageBox.Show(this, "Some the Action list item parameters are not set.", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return (false);
                }
                else if (ret == 1)
                {
                    if (EventsComboBox.SelectedItem.ToString() == "Input Detected")
                    {
                        switch (action)
                        {
                            case "Unlock Door Relay 01":
                            case "Unlock Door Relay 02":
                            case "Display Tag Moving Direction":
                                MessageBox.Show(this, string.Format("Action list item '{0}' is not compatible with event '{1}'.", action, EventsComboBox.SelectedItem.ToString()), "Retail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return (false);
                        }
                    }
                    else
                    {
                        switch (action)
                        {
                            case "Input 01 Timeout":
                            case "Input 02 Timeout":
                                MessageBox.Show(this, string.Format("Action list item '{0}' is not compatible with event '{1}'.", action, EventsComboBox.SelectedItem.ToString()), "Retail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return (false);
                        }
                    }

                    actID = GetActionID(action);
                    for (int j = 0; j < 20; j++)
                    {
                        if ((act[j, 0] == actID) && (act[j, 1] == Convert.ToInt32(rdr)))
                        {
                            MessageBox.Show(this, "Action has already been defined for this event and location.", "Retail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return (false);
                        }
                    }

                    act[index, 0] = actID;
                    act[index, 1] = Convert.ToInt32(rdr);
                    index += 1;
                }
            }

            return (true);
        }
        #endregion

        #region LoadRdrLocArray()
        private void LoadRdrLocArray()
        {
            //rdrFGenLocStruct
            string mySelectQuery = "SELECT ReaderID, FieldGenID, Location FROM zones";
            //OdbcCommand myCommand = new OdbcCommand(mySelectQuery, Form1.m_connection); 
            OdbcDataReader myReader;

            if (!mForm.RunQueryCmd(mySelectQuery, out myReader))
            {
                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                        myReader.Close();
                }

                return;
            }

            //bool firstRec = true;
            int myRec = 0;
            ReaderIDComboBox.Items.Clear();
            while (myReader.Read())
            {

                rdrFGenLocStruct rdrLoc = new rdrFGenLocStruct();

                try
                {
                    rdrLoc.readerID = (ushort)myReader.GetInt16(0);  //reader ID
                    if (!ReaderIDComboBox.Items.Contains(rdrLoc.readerID))
                        ReaderIDComboBox.Items.Add(rdrLoc.readerID);
                }
                catch
                {
                    continue;
                }

                try
                {
                    rdrLoc.fGenID = (ushort)myReader.GetInt16(1);  //FGen ID
                }
                catch
                {
                    continue;
                }

                try
                {
                    rdrLoc.location = myReader.GetString(2);  //Location
                }
                catch
                {
                    rdrLoc.location = "";
                }

                myRec += 1;
                rdrLocArrayList.Add(rdrLoc);

            }//while

            myReader.Close();
        }
        #endregion

        #region LoadEventActionData()
        private void LoadEventActionData()
        {
            string mySelectQuery = "SELECT EventActionID, ReaderID, FGenID, EventID, Location, Description, Timestamp FROM eventaction ORDER BY EventActionID ASC";
            //OdbcCommand myCommand = new OdbcCommand(mySelectQuery, Form1.m_connection); 
            OdbcDataReader myReader;

            if (!mForm.RunQueryCmd(mySelectQuery, out myReader))
            {
                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                        myReader.Close();
                }

                return;
            }

            bool firstRec = true;
            int myRec = 0;

            EventActionListView.Items.Clear();

            while (myReader.Read())
            {
                myRec += 1;
                ListViewItem listItem = new ListViewItem(Convert.ToString(myReader.GetInt32(0)));  //EventActionID
                try
                {
                    listItem.SubItems.Add(Convert.ToString(myReader.GetInt16(1)));  //Reader
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                try
                {
                    listItem.SubItems.Add(Convert.ToString(myReader.GetInt16(2)));  //Fgen
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                try
                {
                    listItem.SubItems.Add(GetEventStr(myReader.GetInt32(3)));  //event
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                try
                {
                    listItem.SubItems.Add(myReader.GetString(4));  //location
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                try
                {
                    listItem.SubItems.Add(myReader.GetString(5));  //description
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                try
                {
                    listItem.SubItems.Add(myReader.GetDateTime(6).ToString("MM-dd-yyyy  HH:mm:ss")); //Timestamp
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                EventActionListView.Items.Add(listItem);

                if (firstRec)
                {
                    listItem.Selected = true;
                    firstRec = false;

                    //int r = myReader.GetInt32(0);
                    try
                    {
                        EventActionIDTextBox.Text = Convert.ToString(myReader.GetInt32(0));  //eventAction ID
                    }
                    catch
                    {
                        EventActionIDTextBox.Text = "";
                    }

                    try
                    {
                        ReaderIDComboBox.Text = Convert.ToString(myReader.GetInt16(1));  //reader
                    }
                    catch
                    {
                        ReaderIDComboBox.Text = "";
                    }

                    try
                    {
                        FGenIDComboBox.Text = Convert.ToString(myReader.GetInt16(2));  //fgen
                    }
                    catch
                    {
                        FGenIDComboBox.Text = "";
                    }

                    try
                    {
                        EventsComboBox.Text = GetEventStr(myReader.GetInt32(3));   //event
                    }
                    catch
                    {
                        EventsComboBox.Text = "";
                    }

                    try
                    {
                        LocationTextBox.Text = myReader.GetString(4);   //location
                    }
                    catch
                    {
                        LocationTextBox.Text = "";
                    }

                    try
                    {
                        DescTextBox.Text = myReader.GetString(5);   //description
                    }
                    catch
                    {
                        DescTextBox.Text = "";
                    }

                    try
                    {
                        ActionDateTextBox.Text = myReader.GetDateTime(6).ToString("MM-dd-yyyy  HH:mm:ss"); //myReader.GetString(5);
                    }
                    catch
                    {
                        ActionDateTextBox.Text = "";
                    }
                }//firstrec

                numRecords += 1;

                EditToolBarButton.Enabled = true;
                DeleteToolBarButton.Enabled = true;

            }//while

            myReader.Close();

            if (numRecords == 0)
            {
                EditToolBarButton.Enabled = false;
                DeleteToolBarButton.Enabled = false;
                SaveToolBarButton.Enabled = false;
                CancelToolBarButton.Enabled = false;
                RefreshToolBarButton.Enabled = false;
            }

            if (EventActionIDTextBox.Text.Length > 0)
                LoadActionDef(Convert.ToInt32(EventActionIDTextBox.Text));
        }
        #endregion

        #region LoadEventActionData(evID)
        private void LoadEventActionData(string evID)
        {
            StringBuilder mySelectQuery = new StringBuilder();
            mySelectQuery.Append("SELECT EventActionID, ReaderID, FGenID, EventID, Location, Description, Timestamp FROM eventaction WHERE EventActionID = ");
            mySelectQuery.AppendFormat("'{0}'", evID);

            //OdbcCommand myCommand = new OdbcCommand(mySelectQuery, Form1.m_connection); 
            OdbcDataReader myReader;

            if (!mForm.RunQueryCmd(mySelectQuery.ToString(), out myReader))
            {
                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                        myReader.Close();
                }

                return;
            }

            if (myReader.Read())
            {

                try
                {
                    EventActionIDTextBox.Text = Convert.ToString(myReader.GetInt32(0));  //eventAction ID
                }
                catch
                {
                    EventActionIDTextBox.Text = "";
                }

                try
                {
                    ReaderIDComboBox.Text = Convert.ToString(myReader.GetInt16(1));  //reader
                }
                catch
                {
                    ReaderIDComboBox.Text = "";
                }

                try
                {
                    FGenIDComboBox.Text = Convert.ToString(myReader.GetInt16(2));  //fgen
                }
                catch
                {
                    FGenIDComboBox.Text = "";
                }

                try
                {
                    EventsComboBox.Text = GetEventStr(myReader.GetInt32(3));   //event
                }
                catch
                {
                    EventsComboBox.Text = "";
                }

                try
                {
                    LocationTextBox.Text = myReader.GetString(4);   //location
                }
                catch
                {
                    LocationTextBox.Text = "";
                }

                try
                {
                    DescTextBox.Text = myReader.GetString(5);   //description
                }
                catch
                {
                    DescTextBox.Text = "";
                }

                try
                {
                    ActionDateTextBox.Text = myReader.GetDateTime(6).ToString("MM-dd-yyyy  HH:mm:ss"); //myReader.GetString(5);
                }
                catch
                {
                    ActionDateTextBox.Text = "";
                }

            }//if

            myReader.Close();

            LoadActionDef(Convert.ToInt32(evID));
        }
        #endregion

        #region LoadActionDef(evtAcionID)
        private void LoadActionDef(int evtAcionID)
        {
            StringBuilder mySelectQuery = new StringBuilder();
            mySelectQuery.Append("SELECT ActionID, Duration, ReaderID, FGenID, Location FROM actiondef WHERE EventActionID = ");
            mySelectQuery.AppendFormat("'{0}'", evtAcionID);
            mySelectQuery.Append(" ORDER BY ActionIndex ASC");
            OdbcDataReader myReader;

            if (!mForm.RunQueryCmd(mySelectQuery.ToString(), out myReader))
            {
                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                        myReader.Close();
                }

                return;
            }

            int myRec = 0;

            ActionsListView.Items.Clear();

            while (myReader.Read())
            {
                myRec += 1;
                int actionID = myReader.GetInt32(0);
                ListViewItem listItem = new ListViewItem(GetActionStr(actionID));  //actionID
                try
                {
                    int duration = myReader.GetInt32(1);
                    if (duration == 0)
                    {
                        if (actionID == MainForm.EVENT_EMAIL_ACTIVITY)
                        {
                            listItem.SubItems.Add("");
                        }
                        else
                        {
                            listItem.SubItems.Add("forever");
                        }
                    }
                    else if (duration < 60)
                    {
                        listItem.SubItems.Add(Convert.ToString(duration) + " sec");
                    }
                    else
                    {
                        listItem.SubItems.Add(Convert.ToString(duration / 60) + " min");
                    }
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                try
                {
                    listItem.SubItems.Add(Convert.ToString(myReader.GetInt16(2)));  //rdr
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                try
                {
                    listItem.SubItems.Add(Convert.ToString(myReader.GetInt16(3)));  //fgen
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                try
                {
                    listItem.SubItems.Add(myReader.GetString(4));  //location
                }
                catch
                {
                    listItem.SubItems.Add("");
                }

                ActionsListView.Items.Add(listItem);

                numRecords += 1;

                EditToolBarButton.Enabled = true;
                DeleteToolBarButton.Enabled = true;

            }//while

            myReader.Close();
        }
        #endregion

        #region ReaderIDComboBox_SelectedValueChanged
        private void ReaderIDComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //This version select comboBox
            if (ReaderIDComboBox.Text.Length > 0)
            {
                ushort rdr = Convert.ToUInt16(ReaderIDComboBox.Text);
                ushort count = 0;
                FGenIDComboBox.Items.Clear();
                foreach (rdrFGenLocStruct rdrLoc in rdrLocArrayList)
                {
                    if (rdrLoc.readerID == rdr)
                    {
                        if (count == 0)
                        {
                            FGenIDComboBox.Text = Convert.ToString(rdrLoc.fGenID);
                            LocationTextBox.Text = rdrLoc.location;
                        }
                        FGenIDComboBox.Items.Add(rdrLoc.fGenID);
                        count++;
                    }
                }
            }

        }
        #endregion

        #region FGenIDComboBox_SelectedIndexChanged
        private void FGenIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ReaderIDComboBox.Text.Length > 0) && (FGenIDComboBox.Text.Length > 0))
            {
                LocationTextBox.Text = "";
                ushort rdr = Convert.ToUInt16(ReaderIDComboBox.Text);
                ushort fgen = Convert.ToUInt16(FGenIDComboBox.Text);
                
                foreach (rdrFGenLocStruct rdrLoc in rdrLocArrayList)
                {
                    if ((rdrLoc.readerID == rdr) && (rdrLoc.fGenID == fgen))
                    {
                        LocationTextBox.Text = rdrLoc.location;
                        break;
                    }                                          
                }
            }
        }
        #endregion 

        #region ActionsListView_ItemChecked (sender, e)
        private void ActionsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (ActListChecked)
                return;
            

            if (e.Item.Index < 0)
                return;

            ///try
            ////{
                ///if (e.Item.Checked)
                    ///userListViewControl1.Items[e.Item.Index].Selected = true;
            ///}
            ///catch (Exception ex)
            ///{
                ///return;
            ///}
            
            /*if (e.Item.Checked) // == CheckState.Checked)
            {
                //Do work on item
                //ActionsListView_ItemChecked.Items[e.Index].Text = "Was just checked.";
                ActionsListView.Items[e.Item.Index].Selected = true;
            }*/

            /*if (ActionTimeComboBox.Text.Length > 0)
            {
                System.Windows.Forms.ListView.SelectedListViewItemCollection listItem = new ListView.SelectedListViewItemCollection(ActionsListView);
                listItem = ActionsListView.SelectedItems;
                if (listItem.Count == 0)
                    return;
                ListViewItem item = listItem[0];
                //int index = listItem[0].Index;
                //ListViewItem item = ActionsListView.SelectedItems[0];
                item.SubItems[1].Text = ActionTimeComboBox.Text;
                int index = item.Index;
                ActionsListView.Items[index].Selected = true;
                ListViewItem item1 = new ListViewItem();
                item1 = (ListViewItem)item.Clone();
                //item1.SubItems[1].Text = ActionTimeComboBox.Text;
                
                ActionsListView.Items.RemoveAt(index);
                ActionsListView.Items.Insert(index, item1);
                

                //ActionsListView.Items[index].SubItems[0].Text = ActionTimeComboBox.Text;
                //ActionsListView.Refresh();
            }*/
        }
        #endregion

        #region ActionsListView_SelectedIndexChanged ( sender, e)
        private void ActionsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if ((ActionTimeComboBox.Text.Length > 0) && (ActionsListView.Items.Count > 0) && loaded)
            {
                //System.Windows.Forms.ListView.SelectedListViewItemCollection listItem = new ListView.SelectedListViewItemCollection(ActionsListView);
                //listItem = ActionsListView.SelectedItems;
                //int index = listItem[0].Index;
                //if (ActionListView.Items[0].)
                {
                    ListViewItem item = ActionsListView.SelectedItems[0];
                    item.SubItems[1].Text = ActionTimeComboBox.Text;
                    int index = item.Index;
                    ListViewItem item1 = new ListViewItem();
                    item1 = (ListViewItem)item.Clone();
                    //item1.SubItems[1].Text = ActionTimeComboBox.Text;

                    ActionsListView.Items.RemoveAt(index);
                    ActionsListView.Items.Insert(index, item1);
                }


                //ActionsListView.Items[index].SubItems[0].Text = ActionTimeComboBox.Text;
                //ActionsListView.Refresh();
            }*/
        }
        #endregion

        #region ActionForm_Load ( sender, e )
        private void ActionForm_Load(object sender, EventArgs e)
        {
            loaded = true;
            LoadEventActionData();
        }
        #endregion

        #region EventActionListView_MouseDown ( sender, e)
        private void EventActionListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (EventActionListView.SelectedItems.Count > 0)
                lastSelectedIndex = EventActionListView.SelectedItems[0].Index;
        }
        #endregion

        #region EventActionListView_MouseUp (sender, e)
        private void EventActionListView_MouseUp(object sender, MouseEventArgs e)
        {
            if ((EventActionListView.SelectedItems.Count == 0) && (lastSelectedIndex >= 0))
                  EventActionListView.Items[lastSelectedIndex].Selected = true;
        }
        #endregion

        #region EventActionListView_SelectedIndexChanged ( sender, e )
        private void EventActionListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EventActionListView.SelectedItems.Count > 0)
            {
                ListViewItem lvItem = EventActionListView.SelectedItems[0];
                string evID = lvItem.SubItems[0].Text.ToString();
                if (evID.Length > 0)
                    LoadEventActionData(evID);

                if (editMode)
                {
                    //go though ActionsListView and based on that update userListView control
                    userListViewControl1.Enabled = true;
                    for (int i = 0; i < ActionsListView.Items.Count; i++)
                    {
                        userListViewControl1.listView1.Items[i].SubItems[0].Text = ActionsListView.Items[i].SubItems[0].Text;
                        userListViewControl1.listView1.Items[i].SubItems[1].Text = ActionsListView.Items[i].SubItems[1].Text;
                        userListViewControl1.listView1.Items[i].SubItems[2].Text = ActionsListView.Items[i].SubItems[2].Text;
                        userListViewControl1.listView1.Items[i].SubItems[3].Text = ActionsListView.Items[i].SubItems[3].Text;
                        userListViewControl1.listView1.Items[i].SubItems[4].Text = ActionsListView.Items[i].SubItems[4].Text;
                    }
                }
            }
        }
        #endregion

        private void EventsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EventsComboBox.SelectedItem.ToString() == "Input Detected")
                userListViewControl1.listView1.InputAction = true;
            else
                userListViewControl1.listView1.InputAction = false;
        }

        #region ActionsListView_ItemSelectionChanged ( sender, e )
        /*private void ActionsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if ((ActionTimeComboBox.Text.Length > 0) && (ActionsListView.Items.Count > 0) && loaded)
            {
                System.Windows.Forms.ListView.SelectedListViewItemCollection listItem = new ListView.SelectedListViewItemCollection(ActionsListView);
                listItem = ActionsListView.SelectedItems;
                if (listItem.Count == 0)
                    return;
                ListViewItem item = listItem[0];
                
                //int index = listItem[0].Index;
                //ListViewItem item = ActionsListView.SelectedItems[0];
                ActListChecked = true;
                if (item.Checked)
                {
                    item.Checked = false;
                    item.SubItems[1].Text = "";
                }
                else
                {
                    item.Checked = true;
                    item.SubItems[1].Text = ActionTimeComboBox.Text;
                }
                int index = item.Index;
                //ActionsListView.Items[index].Selected = true;
                ListViewItem item1 = new ListViewItem();
                item1 = (ListViewItem)item.Clone();
                
                
                //item1.SubItems[1].Text = ActionTimeComboBox.Text;

                ActionsListView.Items.RemoveAt(index);
                ActionsListView.Items.Insert(index, item1);
                ActListChecked = false;

                //ActionsListView.Items[index].SubItems[0].Text = ActionTimeComboBox.Text;
                //ActionsListView.Refresh();
            }
        }*/
        #endregion

    }//class
}//namespace