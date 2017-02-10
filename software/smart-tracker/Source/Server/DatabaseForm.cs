using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Globalization;
using AWIComponentLib.Database;
using Microsoft.Win32;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for DatabaseForm.
	/// </summary>
	public class DatabaseForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox PortTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox DBTextBox;
		private System.Windows.Forms.TextBox ServerTextBox;
		private System.Windows.Forms.TextBox PWTextBox;
		private System.Windows.Forms.TextBox UserNameTextBox;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton SQLRadioButton;
		private System.Windows.Forms.Label ConnectStatusLabel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private MainForm mForm;
		private OdbcDbClass odbcDB = new OdbcDbClass();
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button ConnectButton;
		private System.Windows.Forms.Button DisconnectButton;
		private System.Windows.Forms.RadioButton MySQLRadioButton;
		private OdbcConnection m_connection = null;

		public DatabaseForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//changes made to support MYSQL Server v5.0 and later
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";
		}

		public DatabaseForm(MainForm form)
		{
			InitializeComponent();

			//changes made to support MYSQL Server v5.0 and later
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

			mForm = form;
			OdbcDbClass.NotifyDBConnectionStatusHandler += new NotifyDBConnectionStatus(DBConnectionStatusHandler);
			if (MainForm.m_connection == null)
			   DisconnectButton.Enabled = false;
			else
			   ConnectButton.Enabled = false;

			
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
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DBTextBox = new System.Windows.Forms.TextBox();
            this.ServerTextBox = new System.Windows.Forms.TextBox();
            this.PWTextBox = new System.Windows.Forms.TextBox();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MySQLRadioButton = new System.Windows.Forms.RadioButton();
            this.SQLRadioButton = new System.Windows.Forms.RadioButton();
            this.ConnectStatusLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(103, 198);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.ReadOnly = true;
            this.PortTextBox.Size = new System.Drawing.Size(154, 20);
            this.PortTextBox.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(37, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 23);
            this.label5.TabIndex = 18;
            this.label5.Text = "Port: ";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(37, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 23);
            this.label4.TabIndex = 17;
            this.label4.Text = "Database: ";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(37, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 23);
            this.label3.TabIndex = 16;
            this.label3.Text = "Server:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(37, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 15;
            this.label2.Text = "Password: ";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(37, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 23);
            this.label1.TabIndex = 14;
            this.label1.Text = "User Name: ";
            // 
            // DBTextBox
            // 
            this.DBTextBox.Location = new System.Drawing.Point(103, 168);
            this.DBTextBox.Name = "DBTextBox";
            this.DBTextBox.Size = new System.Drawing.Size(154, 20);
            this.DBTextBox.TabIndex = 24;
            // 
            // ServerTextBox
            // 
            this.ServerTextBox.Location = new System.Drawing.Point(103, 138);
            this.ServerTextBox.Name = "ServerTextBox";
            this.ServerTextBox.Size = new System.Drawing.Size(154, 20);
            this.ServerTextBox.TabIndex = 23;
            // 
            // PWTextBox
            // 
            this.PWTextBox.Location = new System.Drawing.Point(103, 108);
            this.PWTextBox.Name = "PWTextBox";
            this.PWTextBox.Size = new System.Drawing.Size(154, 20);
            this.PWTextBox.TabIndex = 22;
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(103, 78);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(154, 20);
            this.UserNameTextBox.TabIndex = 21;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(200, 280);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 26);
            this.CloseButton.TabIndex = 20;
            this.CloseButton.Text = "Close";
            this.CloseButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MySQLRadioButton);
            this.groupBox1.Controls.Add(this.SQLRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(35, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 56);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Server";
            // 
            // MySQLRadioButton
            // 
            this.MySQLRadioButton.Location = new System.Drawing.Point(130, 24);
            this.MySQLRadioButton.Name = "MySQLRadioButton";
            this.MySQLRadioButton.Size = new System.Drawing.Size(64, 24);
            this.MySQLRadioButton.TabIndex = 1;
            this.MySQLRadioButton.Text = "MySQL";
            this.MySQLRadioButton.Click += new System.EventHandler(this.MySQLRadioButton_Click);
            // 
            // SQLRadioButton
            // 
            this.SQLRadioButton.Location = new System.Drawing.Point(40, 24);
            this.SQLRadioButton.Name = "SQLRadioButton";
            this.SQLRadioButton.Size = new System.Drawing.Size(56, 24);
            this.SQLRadioButton.TabIndex = 0;
            this.SQLRadioButton.Text = "SQL";
            this.SQLRadioButton.Click += new System.EventHandler(this.SQLRadioButton_Click);
            // 
            // ConnectStatusLabel
            // 
            this.ConnectStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.ConnectStatusLabel.Location = new System.Drawing.Point(104, 232);
            this.ConnectStatusLabel.Name = "ConnectStatusLabel";
            this.ConnectStatusLabel.Size = new System.Drawing.Size(152, 16);
            this.ConnectStatusLabel.TabIndex = 27;
            this.ConnectStatusLabel.Text = "Disconnected";
            this.ConnectStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(32, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 23);
            this.label6.TabIndex = 28;
            this.label6.Text = "Status: ";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(24, 280);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 26);
            this.ConnectButton.TabIndex = 29;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Location = new System.Drawing.Point(112, 280);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(75, 26);
            this.DisconnectButton.TabIndex = 30;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // DatabaseForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(298, 317);
            this.ControlBox = false;
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ConnectStatusLabel);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DBTextBox);
            this.Controls.Add(this.ServerTextBox);
            this.Controls.Add(this.PWTextBox);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DatabaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database";
            this.Load += new System.EventHandler(this.DatabaseForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void DatabaseForm_Load(object sender, System.EventArgs e)
		{
			if (MainForm.database.Length > 0)
				DBTextBox.Text = MainForm.database;

			if (MainForm.user.Length > 0)
				UserNameTextBox.Text = MainForm.user;

			if (MainForm.providerName == dbProvider.SQL)
			{
				SQLRadioButton.Checked = true;

				if (MainForm.server.Length > 0)
					ServerTextBox.Text = MainForm.server;	
			}
			else if (MainForm.providerName == dbProvider.MySQL)
			{
				MySQLRadioButton.Checked = true;

				if (MainForm.serverMySQL.Length > 0)
					ServerTextBox.Text = MainForm.serverMySQL;
				
			}

			/*if (MainForm.server.Length > 0)
				ServerTextBox.Text = MainForm.server;

			if (MainForm.database.Length > 0)
				DBTextBox.Text = MainForm.database;*/
			
			if (MainForm.m_connection == null)
			{
				ConnectStatusLabel.Text = "Disconnected";
			}
			else
			{
				ConnectStatusLabel.ForeColor = System.Drawing.Color.Blue;
				ConnectStatusLabel.Text = "Connected";
			}
		}

		private void ConnectButton_Click(object sender, System.EventArgs e)
		{
			if (MainForm.m_connection != null)
				mForm.CloseConnection();

			string s = "";

			if (SQLRadioButton.Checked)
			//if (MainForm.providerName == dbProvider.SQL)
			{
				//string s = "";

				s = "Driver={SQL Native Client};";
				if (ServerTextBox.TextLength > 0)
					s += "Server=" + ServerTextBox.Text;
				else
				{
					MainForm.PlaySound(1);
                    MessageBox.Show(this, "No server name", "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
				s += ";";
				if (DBTextBox.TextLength > 0)
					s += "Database=" + DBTextBox.Text;
				else
				{
                    MainForm.PlaySound(1);
                    MessageBox.Show(this, "No database name", "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
				
				s += ";";
				s += "Trusted_Connection=yes;Pooling=False;";
				//if (!odbcDB.Connect("Driver={SQL Native Client};Server=MainForm.server;Database=MainForm.database;Trusted_Connection=yes;Pooling=False;"))  //SQL
				
				MainForm.providerName = dbProvider.SQL;
				MainForm.database = DBTextBox.Text;
				MainForm.server = ServerTextBox.Text;
				
				mForm.odbcDB.Connect(s);
                MainForm.PlaySound(1);
				

				/*if (!odbcDB.Connect(s))  //SQL
				{	
					MainForm.conStr = "";
					ConnectStatusLabel.ForeColor = System.Drawing.Color.Red;
					ConnectStatusLabel.Text = "Disconnected";
					MainForm.PlaySound(1);
					return;
				}*/
	
				//MainForm.conStr = s;
				//ConnectStatusLabel.ForeColor = System.Drawing.Color.Blue;
				//ConnectStatusLabel.Text = "Connected";
			}
			else if (MySQLRadioButton.Checked) //if (MainForm.providerName == dbProvider.MySQL)
			{
				//string s = "";

				s = "DRIVER={MySQL ODBC 3.51 Driver};";
				if (ServerTextBox.TextLength > 0)
					s += "Server=" + ServerTextBox.Text;
				else
				{
                    MainForm.PlaySound(1);
                    MessageBox.Show(this, "No server name", "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
				s += ";";

				if (DBTextBox.TextLength > 0)
					s += "Database=" + DBTextBox.Text;
				else
				{
                    MainForm.PlaySound(1);
                    MessageBox.Show(this, "No database name", "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
				s += ";";

				if (UserNameTextBox.TextLength > 0)
					s += "User=" + UserNameTextBox.Text;
				else
				{
                    MainForm.PlaySound(1);
                    MessageBox.Show(this, "No user name", "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}

				if (PWTextBox.TextLength > 0) {
                    s += ";PASSWORD=" + PWTextBox.Text;
				} else {
				    s += ";PASSWORD=";
                }
				s += ";OPTION=3;";

				MainForm.providerName = dbProvider.MySQL;
				MainForm.database = DBTextBox.Text;
				MainForm.serverMySQL = ServerTextBox.Text;
				MainForm.user = UserNameTextBox.Text;
                MainForm.password = PWTextBox.Text;
				
				mForm.OpenConnection(s);
				//mForm.odbcDB.Connect(s);
                MainForm.PlaySound(1);
				

				/*if (!odbcDB.Connect(s))  //MYSQL
				{	
					MainForm.conStr = "";
					ConnectStatusLabel.ForeColor = System.Drawing.Color.Red;
					ConnectStatusLabel.Text = "Disconnected";
					MainForm.PlaySound(1);
					return;
				}*/
				////////////////////////////////
				//if (!odbcDB.Connect("DRIVER={MySQL ODBC 3.51 Driver};SERVER=localhost;DATABASE=parkingtracker;USER=root;PASSWORD=;OPTION=3;"))	//MYSQL
				//{						
					//return;
				//}
			}
			else
			{					
				return;
			}

			MainForm.conStr = s;
			ConnectStatusLabel.ForeColor = System.Drawing.Color.Blue;
			ConnectStatusLabel.Text = "Connected";

            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Active Wave\\Smart Tracker\\");
			//reg.SetValue("server", MainForm.server);
			reg.SetValue("database", MainForm.database);
		
			if (MainForm.providerName == dbProvider.SQL)
			{
				reg.SetValue("provider", "SQL");
				reg.SetValue("server", MainForm.server);
			}
			else
			{
				reg.SetValue("provider", "MySQL");
				reg.SetValue("user", MainForm.user);
				reg.SetValue("serverMySQL", MainForm.serverMySQL);
			}

			DisconnectButton.Enabled = true;
			ConnectButton.Enabled = false;
            MainForm.PlaySound(1);
		}

		private void DisconnectButton_Click(object sender, System.EventArgs e)
		{
			mForm.CloseConnection();
            
			//mForm.timer4.Enabled = false;
			/*ConnectStatusLabel.Text = "Disconnected";
		    ConnectStatusLabel.ForeColor = System.Drawing.Color.Red;
			ConnectButton.Enabled = true;
			DisconnectButton.Enabled = false;*/
            MainForm.PlaySound(1);
		}

		private void CancelButton_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void DBConnectionStatusHandler(status stat, OdbcConnection connect)
		{
			if (stat == status.open)
			{
				m_connection = connect;
				//mForm.timer4.Enabled = tr;
			}
			else if (stat == status.broken)
			{
				m_connection = null;   
			}
			else if (stat == status.close)
			{
				m_connection = null;

				ConnectStatusLabel.Text = "Disconnected";
		        ConnectStatusLabel.ForeColor = System.Drawing.Color.Red;
			    ConnectButton.Enabled = true;
			    DisconnectButton.Enabled = false;
			}
		}

		private void SQLRadioButton_Click(object sender, System.EventArgs e)
		{
			UserNameTextBox.Text = "";

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

			if (MainForm.server.Length > 0)
				ServerTextBox.Text = MainForm.serverMySQL;

			if (MainForm.database.Length > 0)
				DBTextBox.Text = MainForm.database;
		}
	}
}
