using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AWI.SmartTracker
{
    public partial class LoadMsgForm : Form
    {
        private string msg;
        private int len;
        private int ct = 0;

        

        public LoadMsgForm()
        {
            InitializeComponent();
            msg = label.Text;
            len = label.Text.Length;
            label.Text = "";
            MainForm.m_closeMsgBoxEvent += new CloseMsgBoxDelegate(CloseLoadingMsg);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ct >= len)
            {
                ct = 0;
                label.Text = "";
            }

            label.Text += msg.Substring(ct, 1);
            ct += 1;
            this.BringToFront();
        }

        private void CloseLoadingMsg()
        {
            this.Close();
        }
    }
}