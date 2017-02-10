using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AWI.SmartTracker.ReportClass;

namespace AWI.SmartTracker
{
    public partial class GroupPolicy : Form
    {
        private ListViewItem lvItemZone;

        private ContextMenu ctxGroupsNew = new ContextMenu();
        private ContextMenu ctxGroupsSel = new ContextMenu();

        private ContextMenu ctxZonesNew = new ContextMenu();
        private ContextMenu ctxZonesSel = new ContextMenu();

        private ContextMenu ctxScheduleNew = new ContextMenu();
        private ContextMenu ctxScheduleSel = new ContextMenu();

        private Group currentGroup;

        private List<Zone> listZones;

        private bool IsNew = false;
        private bool HasGroupChanges = false;
        private bool HasZonesChanges = false;
        private bool HasSchedulesChanges = false;

        private Group selectedGroup;
        public Group SelectedGroup { get { return selectedGroup; } }

        public GroupPolicy()
        {
            InitializeComponent();


            MenuItem groupNew = new MenuItem("New Group...");
            groupNew.Click += new EventHandler(groupNew_Click);

            MenuItem groupEdit = new MenuItem("Edit");
            groupEdit.Click += new EventHandler(groupEdit_Click);

            MenuItem groupDelete = new MenuItem("Delete");
            groupDelete.Click += new EventHandler(groupDelete_Click);

            MenuItem groupRename = new MenuItem("Rename");
            groupRename.Click += new EventHandler(groupRename_Click);

            ctxGroupsNew.MenuItems.Add(groupNew);
            ctxGroupsSel.MenuItems.AddRange(new MenuItem[] { groupEdit, new MenuItem("-"), groupDelete, groupRename, new MenuItem("-"), groupNew.CloneMenu() });



            MenuItem zoneAdd = new MenuItem("Add");
            zoneAdd.Click += new EventHandler(zoneAdd_Click);

            MenuItem zoneDelete = new MenuItem("Delete");
            zoneDelete.Click += new EventHandler(zoneDelete_Click);

            ctxZonesNew.MenuItems.Add(zoneAdd);
            ctxZonesSel.MenuItems.AddRange(new MenuItem[] { zoneAdd.CloneMenu(), zoneDelete });

            MenuItem scheduleEdit = new MenuItem("Edit");
            scheduleEdit.Click += new EventHandler(scheduleEdit_Click);

            MenuItem scheduleDelete = new MenuItem("Delete");
            scheduleDelete.Click += new EventHandler(scheduleDelete_Click);

            MenuItem scheduleNew = new MenuItem("New");
            scheduleNew.Click += new EventHandler(scheduleNew_Click);

            ctxScheduleNew.MenuItems.AddRange(new MenuItem[] { scheduleNew });
            ctxScheduleSel.MenuItems.AddRange(new MenuItem[] { scheduleEdit, scheduleDelete, new MenuItem("-"), scheduleNew.CloneMenu() });


            lvGroups.ContextMenu = ctxGroupsNew;
            lvGroupZones.ContextMenu = ctxZonesNew;
            lvGroupSchedules.ContextMenu = ctxScheduleNew;


            var groups = Groups.GetAllGroups();
            groups.ForEach(group =>
                {
                    ListViewItem item = new ListViewItem(new string[] { group.Name, group.Description });
                    item.Tag = group;
                    lvGroups.Items.Add(item);
                });

            listZones = Zones.GetAllZones();

            if (listZones.Count == 0)
                zoneAdd.Enabled = false;

            listZones.ForEach(zone => cbZonesInsert.Items.Add(zone.Location));

            lvGroups.LabelEdit = true;

            ClearEditor();
        }

        void scheduleNew_Click(object sender, EventArgs e)
        {
            GroupSchedule schedule = new GroupSchedule(currentGroup.ID, "", true);

            using (GroupScheduleForm form = new GroupScheduleForm(schedule))
            {
                Hide();

                DialogResult result = form.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    HasSchedulesChanges = true;
                    ListViewItem item = new ListViewItem(new string[] { schedule.Name, schedule.ToString() });
                    item.Tag = schedule;
                    lvGroupSchedules.Items.Add(item);
                }

                Show();
            }
        }

        void scheduleDelete_Click(object sender, EventArgs e)
        {
            if (lvGroupSchedules.SelectedItems.Count > 0)
            {
                HasSchedulesChanges = true;
                lvGroupSchedules.Items.Remove(lvGroupSchedules.SelectedItems[0]);
            }

            lvGroupSchedules.Focus();
        }

        void scheduleEdit_Click(object sender, EventArgs e)
        {
            GroupSchedule schedule = lvGroupSchedules.SelectedItems[0].Tag as GroupSchedule;

            using (GroupScheduleForm form = new GroupScheduleForm(schedule))
            {
                Hide();

                DialogResult result = form.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    HasSchedulesChanges = true;
                    lvGroupSchedules.SelectedItems[0].Text = schedule.Name;
                    lvGroupSchedules.SelectedItems[0].SubItems[1].Text = schedule.ToString();
                }

                Show();
            }
        }

        void zoneDelete_Click(object sender, EventArgs e)
        {
            if (lvGroupZones.SelectedItems.Count > 0)
            {
                HasZonesChanges = true;
                lvGroupZones.Items.Remove(lvGroupZones.SelectedItems[0]);
            }

            lvGroupZones.Focus();
        }

        void zoneAdd_Click(object sender, EventArgs e)
        {
            lvGroupZones.SelectedItems.Clear();

            lvItemZone = new ListViewItem();
            lvGroupZones.Items.Add(lvItemZone);
            lvItemZone.Selected = true;

            HasZonesChanges = true;

            ShowComboBox(lvItemZone, cbZonesInsert);
        }

        void groupDelete_Click(object sender, EventArgs e)
        {
            if (lvGroups.SelectedItems.Count > 0) {
                Group group = lvGroups.SelectedItems[0].Tag as Group;
                DialogResult result = MessageBox.Show(string.Format("Do you want to delete group policy '{0}'?", group.Name), "Group Policy", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    lvGroups.SelectedItems[0].Remove();
                    Groups.DeleteGroup(group.ID);
                }
            }
        }

        void groupRename_Click(object sender, EventArgs e)
        {
            if (lvGroups.SelectedItems.Count > 0)
                lvGroups.SelectedItems[0].BeginEdit();
        }

        void LoadGroup(Group group)
        {
            lvGroups.Enabled = false;
            btnCancel.Enabled = true;
            btnSave.Enabled = false;
            panelGroup.Enabled = true;
            btnSelect.Enabled = false;

            currentGroup = group;

            lvGroupZones.Items.Clear();
            lvGroupSchedules.Items.Clear();

            if (currentGroup.ID != -1)
            {
                txtName.Text = group.Name;
                txtDescription.Text = group.Description;

                var zones = GroupZones.GetZones(currentGroup.ID);

                zones.ForEach(zone =>
                {
                    ListViewItem item = new ListViewItem(zone.Location);
                    item.Tag = zone;
                    lvGroupZones.Items.Add(item);
                });

                var schedules = GroupSchedules.GetSchedules(currentGroup.ID);

                schedules.ForEach(schedule =>
                {
                    ListViewItem item = new ListViewItem(new string[] { schedule.Name, schedule.ToString() });
                    item.Tag = schedule;
                    lvGroupSchedules.Items.Add(item);
                });
            }

            HasGroupChanges = false;
            HasZonesChanges = false;
            HasSchedulesChanges = false;
        }

        void groupEdit_Click(object sender, EventArgs e)
        {
            IsNew = false;

            LoadGroup(lvGroups.SelectedItems[0].Tag as Group);
        }

        void groupNew_Click(object sender, EventArgs e)
        {
            IsNew = true;

            LoadGroup(new Group());
        }

        private void cbZonesInsert_Select(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex >= 0)
            {
                // Set text of ListView item to match the ComboBox.
                lvItemZone.Text = (sender as ComboBox).Text;
                Zone zone = listZones[(sender as ComboBox).SelectedIndex];
                lvItemZone.Tag = new GroupZone(currentGroup.ID, zone.ID, zone.Location);

                Console.WriteLine("Zone selected {0} {1}", zone.ID, zone.Location);
            }

            // Hide the ComboBox.
            (sender as ComboBox).Visible = false;

            HasZonesChanges = true;
        }

        private void cbZonesInsert_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the user presses ESC.
            switch (e.KeyChar)
            {
                case (char)Keys.Escape:
                    // Reset the original text value, and then hide the ComboBox.
                    (sender as ComboBox).Text = lvItemZone.Text;
                    (sender as ComboBox).Visible = false;
                    break;

                case (char)Keys.Enter:
                    // Hide the ComboBox.
                    (sender as ComboBox).Visible = false;
                    break;
            }
        }

        private void lvGroupZones_MouseUp(object sender, MouseEventArgs e)
        {
            // Get the item on the row that is clicked.
            lvItemZone = (sender as ListView).GetItemAt(e.X, e.Y);

            ShowComboBox(lvItemZone, cbZonesInsert);
        }

        private void ShowComboBox(ListViewItem item, ComboBox combobox)
        {
            // Make sure that an item is clicked.
            if (item != null)
            {
                ListView lv = item.ListView;

                // Get the bounds of the item that is clicked.
                Rectangle ClickedItem = item.Bounds;

                // Verify that the column is completely scrolled off to the left.
                if ((ClickedItem.Left + lv.Columns[0].Width) < 0)
                {
                    // If the cell is out of view to the left, do nothing.
                    return;
                }

                // Verify that the column is partially scrolled off to the left.
                else if (ClickedItem.Left < 0)
                {
                    // Determine if column extends beyond right side of ListView.
                    if ((ClickedItem.Left + lv.Columns[0].Width) > lv.Width)
                    {
                        // Set width of column to match width of ListView.
                        ClickedItem.Width = lv.Width;
                        ClickedItem.X = 0;
                    }
                    else
                    {
                        // Right side of cell is in view.
                        ClickedItem.Width = lv.Columns[0].Width + ClickedItem.Left;
                        ClickedItem.X = 2;
                    }
                }
                else if (lv.Columns[0].Width > lv.Width)
                {
                    ClickedItem.Width = lv.Width;
                }
                else
                {
                    ClickedItem.Width = lv.Columns[0].Width;
                    ClickedItem.X = 2;
                }

                // Adjust the top to account for the location of the ListView.
                ClickedItem.Y += lv.Top;
                ClickedItem.X += lv.Left;

                // Assign calculated bounds to the ComboBox.
                combobox.Bounds = ClickedItem;

                // Set default text for ComboBox to match the item that is clicked.
                if (item.Text.Length == 0)
                {
                    if (combobox.SelectedIndex >= 0)
                    {
                        combobox.SelectedIndex = -1;
                    }
                }
                else
                    combobox.Text = item.Text;

                // Display the ComboBox, and make sure that it is on top with focus.
                combobox.Visible = true;
                combobox.BringToFront();
                combobox.Focus();
            }
        }

        private void lvGroupZones_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Delete:
                    if ((sender as ListView).SelectedItems != null)
                        zoneDelete_Click(sender, new EventArgs());
                    break;
            }
        }

        private void lvGroupZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvGroupZones.SelectedIndices.Count > 0)
            {
                lvGroupZones.ContextMenu = ctxZonesSel;
            }
            else
            {
                lvGroupZones.ContextMenu = ctxZonesNew;
            }
        }

        private void lvGroupSchedule_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Delete:
                    if ((sender as ListView).SelectedItems != null)
                        scheduleDelete_Click(sender, new EventArgs());
                    break;
            }
        }

        private void lvGroupSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvGroupSchedules.SelectedIndices.Count > 0)
            {
                lvGroupSchedules.ContextMenu = ctxScheduleSel;
            }
            else
            {
                lvGroupSchedules.ContextMenu = ctxScheduleNew;
            }
        }

        private void lvGroups_ItemActivate(object sender, EventArgs e)
        {
            btnSelect.PerformClick();

            //groupEdit_Click(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool changed = HasGroupChanges || HasZonesChanges || HasSchedulesChanges;

            if (changed)
            {
                DialogResult result = MessageBox.Show("Do you want to discard changes?", "Group Policy", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == System.Windows.Forms.DialogResult.No)
                    return;
            }

            ClearEditor();
        }

        private void ClearEditor()
        {
            btnCancel.Enabled = false;
            btnSave.Enabled = false;

            txtName.Text = "";
            txtDescription.Text = "";

            lvGroupZones.Items.Clear();
            lvGroupSchedules.Items.Clear();

            panelGroup.Enabled = false;

            lvGroups.Enabled = true;

            HasGroupChanges = false;
            HasZonesChanges = false;
            HasSchedulesChanges = false;

            if (lvGroups.SelectedItems.Count > 0)
                btnSelect.Enabled = true;
            else
                btnSelect.Enabled = false;
        }

        private void SaveCurrentGroup()
        {
            if (IsNew)
            {
                currentGroup.Name = txtName.Text;
                currentGroup.Description = txtDescription.Text;

                Groups.InsertGroup(currentGroup);
                foreach (ListViewItem item in lvGroupZones.Items)
                {
                    GroupZone zone = item.Tag as GroupZone;
                    zone.GroupID = currentGroup.ID;

                    GroupZones.InsertZone(zone);
                }
                foreach (ListViewItem item in lvGroupSchedules.Items)
                {
                    GroupSchedule schedule = item.Tag as GroupSchedule;
                    schedule.GroupID = currentGroup.ID;

                    GroupSchedules.InsertSchedule(schedule);
                }

                var groups = Groups.GetAllGroups();

                lvGroups.BeginUpdate();

                lvGroups.Items.Clear();
                
                groups.ForEach(group =>
                {
                    ListViewItem item = new ListViewItem(new string[] { group.Name, group.Description });
                    item.Tag = group;
                    lvGroups.Items.Add(item);

                    if (group.ID == currentGroup.ID)
                        item.Selected = true;
                });

                lvGroups.EndUpdate();
            }
            else
            {
                if (HasGroupChanges)
                {
                    currentGroup.Name = txtName.Text;
                    currentGroup.Description = txtDescription.Text;

                    Groups.UpdateGroup(currentGroup);

                    lvGroups.SelectedItems[0].Text = currentGroup.Name;
                    lvGroups.SelectedItems[0].SubItems[1].Text = currentGroup.Description;
                }
                if (HasZonesChanges)
                {
                    GroupZones.DeleteZones(currentGroup.ID);

                    foreach (ListViewItem item in lvGroupZones.Items)
                    {
                        GroupZone zone = item.Tag as GroupZone;
                        GroupZones.InsertZone(zone);
                    }
                }
                if (HasSchedulesChanges)
                {
                    GroupSchedules.DeleteSchedules(currentGroup.ID);

                    foreach (ListViewItem item in lvGroupSchedules.Items)
                    {
                        GroupSchedule schedule = item.Tag as GroupSchedule;
                        GroupSchedules.InsertSchedule(schedule);
                    }
                }
            }
        }

        private void lvGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvGroups.SelectedIndices.Count > 0)
            {
                lvGroups.ContextMenu = ctxGroupsSel;
                btnSelect.Enabled = true;
                selectedGroup = lvGroups.SelectedItems[0].Tag as Group;
            }
            else
            {
                lvGroups.ContextMenu = ctxGroupsNew;
                btnSelect.Enabled = false;
                selectedGroup = null;
            }
        }

        private void lvGroups_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Delete:
                    if ((sender as ListView).SelectedItems != null)
                        groupDelete_Click(sender, new EventArgs());
                    break;
            }
        }

        private void GroupPolicy_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool changed = HasGroupChanges || HasZonesChanges || HasSchedulesChanges;

            if (changed)
            {
                DialogResult result = MessageBox.Show("Do you want to save the changes?", "Group Policy", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                switch (result)
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case System.Windows.Forms.DialogResult.Yes:
                        SaveCurrentGroup();
                        break;
                    default:
                        break;
                }
            }
        }

        private void lvGroups_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true;
                return;
            }

            if (e.Label.Length == 0)
            {
                e.CancelEdit = true;
                return;
            }

            Group group = lvGroups.Items[e.Item].Tag as Group;

            if (group != null)
            {
                group.Name = e.Label;
                Groups.UpdateGroup(group);

                lvGroups.Items[e.Item].Tag = Groups.GetGroup(group.ID);
            }
        }

        private void lvGroupSchedules_DoubleClick(object sender, EventArgs e)
        {
            if (lvGroupSchedules.SelectedItems.Count > 0) {
                scheduleEdit_Click(sender, e);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCurrentGroup();

            ClearEditor();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            HasGroupChanges = true;

            if (txtName.TextLength > 0)
                btnSave.Enabled = true;
            else
                btnSave.Enabled = false;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            HasGroupChanges = true;
        }
    }
}
