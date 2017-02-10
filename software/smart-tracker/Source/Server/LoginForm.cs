//SERVER LoginForm
//#################

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Win32;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for LoginForm.
	/// </summary>
	public class LoginForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton SQLRadioButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.TextBox UserNameTextBox;
		private System.Windows.Forms.TextBox PWTextBox;
		private System.Windows.Forms.TextBox ServerTextBox;
		private System.Windows.Forms.TextBox DBTextBox;
		private System.Windows.Forms.TextBox PortTextBox;
		private System.Windows.Forms.RadioButton MySQLRadioButton;

        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LoginForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//changes made to support MYSQL Server v5.0 and later
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

			RegistryKey regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Active Wave", true);
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Active Wave\\Smart Tracker\\");
			string s = (string)reg.GetValue("server");
			if(s != null)
				MainForm.server = s;

			s = (string)reg.GetValue("database");
			if(s != null)
				MainForm.database = s;

			s = (string)reg.GetValue("serverMySQL");
			if(s != null)
				MainForm.serverMySQL = s;

			s = (string)reg.GetValue("user");
			if(s != null)
				MainForm.user = s;

			s = (string)reg.GetValue("provider");
			if(s != null)
			{
               if (s == "SQL")
				   MainForm.providerName = dbProvider.SQL;
				else if (s == "MySQL")
                   MainForm.providerName = dbProvider.MySQL;
			}

			//Registry.LocalMachine.OpenSubKey("Software\\myTestKey", true);

			//if (regVersion.	

				

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MySQLRadioButton = new System.Windows.Forms.RadioButton();
            this.SQLRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.PWTextBox = new System.Windows.Forms.TextBox();
            this.ServerTextBox = new System.Windows.Forms.TextBox();
            this.DBTextBox = new System.Windows.Forms.TextBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MySQLRadioButton);
            this.groupBox1.Controls.Add(this.SQLRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(10, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Server";
            // 
            // MySQLRadioButton
            // 
            this.MySQLRadioButton.AutoSize = true;
            this.MySQLRadioButton.Location = new System.Drawing.Point(136, 25);
            this.MySQLRadioButton.Name = "MySQLRadioButton";
            this.MySQLRadioButton.Size = new System.Drawing.Size(58, 17);
            this.MySQLRadioButton.TabIndex = 1;
            this.MySQLRadioButton.Text = "MySQL";
            this.MySQLRadioButton.Click += new System.EventHandler(this.MySQLRadioButton_Click);
            // 
            // SQLRadioButton
            // 
            this.SQLRadioButton.AutoSize = true;
            this.SQLRadioButton.Location = new System.Drawing.Point(28, 25);
            this.SQLRadioButton.Name = "SQLRadioButton";
            this.SQLRadioButton.Size = new System.Drawing.Size(44, 17);
            this.SQLRadioButton.TabIndex = 0;
            this.SQLRadioButton.Text = "SQL";
            this.SQLRadioButton.Click += new System.EventHandler(this.SQLRadioButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Name: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Server:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Database: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Port: ";
            // 
            // OKButton
            // 
            this.OKButton.AutoSize = true;
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(39, 244);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 26);
            this.OKButton.TabIndex = 11;
            this.OKButton.Text = "OK";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.AutoSize = true;
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(136, 244);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 26);
            this.CloseButton.TabIndex = 12;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(78, 88);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(154, 21);
            this.UserNameTextBox.TabIndex = 2;
            // 
            // PWTextBox
            // 
            this.PWTextBox.Location = new System.Drawing.Point(78, 118);
            this.PWTextBox.Name = "PWTextBox";
            this.PWTextBox.PasswordChar = '*';
            this.PWTextBox.Size = new System.Drawing.Size(154, 21);
            this.PWTextBox.TabIndex = 4;
            this.PWTextBox.UseSystemPasswordChar = true;
            // 
            // ServerTextBox
            // 
            this.ServerTextBox.Location = new System.Drawing.Point(78, 148);
            this.ServerTextBox.Name = "ServerTextBox";
            this.ServerTextBox.Size = new System.Drawing.Size(154, 21);
            this.ServerTextBox.TabIndex = 6;
            // 
            // DBTextBox
            // 
            this.DBTextBox.Location = new System.Drawing.Point(78, 178);
            this.DBTextBox.Name = "DBTextBox";
            this.DBTextBox.Size = new System.Drawing.Size(154, 21);
            this.DBTextBox.TabIndex = 8;
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(78, 208);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.ReadOnly = true;
            this.PortTextBox.Size = new System.Drawing.Size(154, 21);
            this.PortTextBox.TabIndex = 10;
            this.PortTextBox.Text = "3306";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(250, 283);
            this.ControlBox = false;
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.DBTextBox);
            this.Controls.Add(this.ServerTextBox);
            this.Controls.Add(this.PWTextBox);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Login";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void LoginForm_Load(object sender, System.EventArgs e)
		{
			if (MainForm.providerName == dbProvider.SQL)
			{
				SQLRadioButton.Checked = true;
			}
			else if (MainForm.providerName == dbProvider.MySQL)
			{
				MySQLRadioButton.Checked = true;
			}
		}

		private void OKButton_Click(object sender, System.EventArgs e)
		{
			if (SQLRadioButton.Checked)
			{
				MainForm.providerName = dbProvider.SQL;
				if (ServerTextBox.TextLength > 0)
					MainForm.server = ServerTextBox.Text;
			}
			else
			{
				MainForm.providerName = dbProvider.MySQL;
				if (ServerTextBox.TextLength > 0)
					MainForm.serverMySQL = ServerTextBox.Text;
			}

			if (UserNameTextBox.TextLength > 0)
				MainForm.user = UserNameTextBox.Text;

            if (PWTextBox.TextLength > 0)
                MainForm.password = PWTextBox.Text;

			if (DBTextBox.TextLength > 0)
			    MainForm.database = DBTextBox.Text;

            MainForm.PlaySound(1);

			Close();
		}

		private void CancelButton_Click(object sender, System.EventArgs e)
		{
            Application.Exit();
        }

		private void SQLRadioButton_Click(object sender, System.EventArgs e)
		{
			UserNameTextBox.Text = "";
            UserNameTextBox.Enabled = false;

            PWTextBox.Text = "";
            PWTextBox.Enabled = false;

			if (MainForm.server.Length > 0)
				ServerTextBox.Text = MainForm.server;
			else
               ServerTextBox.Text = "";

			if (MainForm.database.Length > 0)
				 DBTextBox.Text = MainForm.database;
		}

		private void MySQLRadioButton_Click(object sender, System.EventArgs e)
		{
			if (MainForm.user.Length > 0)
				UserNameTextBox.Text = MainForm.user;
            else
                UserNameTextBox.Text = "";
            UserNameTextBox.Enabled = true;

            if (MainForm.password.Length > 0)
                PWTextBox.Text = MainForm.password;
            else
                PWTextBox.Text = "";
            PWTextBox.Enabled = true;

            if (MainForm.serverMySQL.Length > 0)
				ServerTextBox.Text = MainForm.serverMySQL;
            else
                ServerTextBox.Text = "";

			if (MainForm.database.Length > 0)
				DBTextBox.Text = MainForm.database;
		}
	}
}
