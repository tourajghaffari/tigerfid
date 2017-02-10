using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UserListControl
{
    public partial class UserListViewControl : UserControl
    {
        private int clickedColumnIndex = -1;

        public UserListViewControl()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //ListViewItem lv = sen;
            //lv.
            //if (clickedColumnIndex < 0)
                //return;

            //if (listView1.Columns[0].Text == "Action")
            //if (col.Text != "Action")
            //{
                //if (e.CurrentValue == CheckState.Checked)
                    //e.NewValue = CheckState.Checked;
                //else
                    //e.NewValue = CheckState.Unchecked;
            //} 
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            clickedColumnIndex = e.Column;    
        }
    }
}