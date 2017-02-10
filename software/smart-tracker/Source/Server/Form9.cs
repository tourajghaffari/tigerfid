using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using AWIComponentLib.Database;
using System.Data.Odbc;
using System.Data;
using System.Text;
using System.IO;
using System.Threading;
using System.Globalization;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for Form9.
	/// </summary>
	public class Form9 : System.Windows.Forms.Form, IComparer
	{
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton NewToolBarButton;
		private System.Windows.Forms.ToolBarButton EditToolBarButton;
		private System.Windows.Forms.ToolBarButton CancelToolBarButton;
		private System.Windows.Forms.ToolBarButton SaveToolBarButton;
		private System.Windows.Forms.ToolBarButton DeleteToolBarButton;
		private System.Windows.Forms.ToolBarButton RefreshToolBarButton;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TextBox ExpDateTextBox;
		private System.Windows.Forms.Button PreButton;
		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Button LastButton;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button FirstButton;
		private System.Windows.Forms.Label DBStatusLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox ActiveDateTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button OpenImageButton;
		private System.Windows.Forms.PictureBox AccPictureBox;
		private System.Windows.Forms.ComboBox NameComboBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox GuestCheckBox;
		private System.Windows.Forms.DateTimePicker DateTimePicker;
		private System.ComponentModel.IContainer components;
		private int MAX_DEMO_RECORDS = 10;		
		private bool newRec = false;
        public AWIHelperClass awiHelper;
		private string oldEmployeeID = "";
		private System.Windows.Forms.Label label6;
		private OdbcConnection m_connection = null;
		private System.Windows.Forms.OpenFileDialog OpenFileDlg;
		private bool GuestBoxChecked = false;
		private string m_imageName = "";
		private System.Windows.Forms.TextBox CompanyTextBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox FNameTextBox;
		private System.Windows.Forms.TextBox LNameTextBox;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.TextBox TagIDTextBox;
		private System.Windows.Forms.Label EmpNameLabel;
		private System.Windows.Forms.Label Label;
		private Bitmap accImage;
		private MainForm mForm;
		private int m_sortColumn = -1;
		private System.Timers.Timer timer1;
        private TextBox PassportTextBox;
        private Label label7;
        private TextBox CommentTextBox;
        private Label label9;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
		private bool m_sortReverse= false;

		public Form9()
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

		public Form9(MainForm main)
		{
			//
			// Required for Windows Form Designer support
			//
			mForm = main;
			InitializeComponent();

			//changes made to support MYSQL Server v5.0 and later
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

			OdbcDbClass.NotifyDBConnectionStatusHandler += new NotifyDBConnectionStatus(DBConnectionStatusHandler);

			if (MainForm.m_connection == null)
			{
				DBStatusLabel.ForeColor = System.Drawing.Color.Red;
				DBStatusLabel.Text = "Disconnected";
				toolBar1.Enabled = false;
				FirstButton.Enabled = false;
				LastButton.Enabled = false;
				NextButton.Enabled = false;
				PreButton.Enabled = false;
				return;
			}

			lock (MainForm.m_connection)  //dec-06-06
			{
				DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
				DBStatusLabel.Text = "Connected";
				m_connection = MainForm.m_connection;
				//}



                awiHelper = new AWIHelperClass(mForm);

                string mySelectQuery = "SELECT accType, ID, FirstName, LastName, PassportNum, Company, expTime, Time, Comment FROM employees WHERE accType >= '2'"; // WHERE Type = 'AST' AND AccType = '1'";
                OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection);

                OdbcDataReader myReader = null;

                /*try
                {
                    myReader = myCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                    if (myReader != null)
                    {
                        if (!myReader.IsClosed)
                            myReader.Close();
                    }
                    return;
                }*/

                //lock (MainForm.m_connection)
                //{
                try
                {
                    myReader = myCommand.ExecuteReader();
                    MainForm.reconnectCounter = -1;
                    timer1.Enabled = false;
                }
                catch (Exception ex)
                {
                    int ret = 0, ret1 = 0, ret2 = 0;
                    if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                        ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                        ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                    {
                        //error code 2013
                        DBStatusLabel.ForeColor = System.Drawing.Color.Red;
                        DBStatusLabel.Text = "Disconnected";
                        toolBar1.Enabled = false;

                        if (MainForm.reconnectCounter < 0)
                        {
                            MainForm.reconnectCounter = 0;
                            timer1.Enabled = true;
                        }

                        /*Thread.Sleep(500);

                        if (!mForm.ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
                        {
                            MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                    myReader.Close();
                            }
                            return;
                        }

                        DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
                        DBStatusLabel.Text = "Connected";
                        toolBar1.Enabled = true;*/

                    }
                    if (myReader != null)
                    {
                        if (!myReader.IsClosed)
                            myReader.Close();
                    }
                    return;
                }//catch .. try
                //}//lock

                bool firstRec = true;
                int myRec = 0;
                while (myReader.Read())
                {
                    myRec += 1;
                    if (myRec > MAX_DEMO_RECORDS)
                        break;
                    ListViewItem listItem = new ListViewItem("");  //first index
                    try
                    {
                        if (myReader.GetString(0) == "3")
                            listItem.SubItems.Add("Yes");  //guest
                        else
                            listItem.SubItems.Add("No");     //No guest
                    }
                    catch
                    {
                        listItem.SubItems.Add("No");     //No guest
                    }

                    try
                    {
                        listItem.SubItems.Add(myReader.GetString(1));  //ID
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    try
                    {
                        listItem.SubItems.Add(myReader.GetString(2));  //fname
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    try
                    {
                        listItem.SubItems.Add(myReader.GetString(3));  //lname
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    try
                    {
                        listItem.SubItems.Add(myReader.GetString(4));  //passport
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    try
                    {
                        listItem.SubItems.Add(myReader.GetString(5));  //company
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    try
                    {
                        listItem.SubItems.Add(myReader.GetDate(6).ToShortDateString());  //exp time
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    try
                    {
                        listItem.SubItems.Add(myReader.GetString(7));  //act time
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    try
                    {
                        listItem.SubItems.Add(myReader.GetString(8));  //comment
                    }
                    catch
                    {
                        listItem.SubItems.Add("");
                    }

                    listView.Items.Add(listItem);
                    if (firstRec)
                    {
                        //listItem.Selected = true;
                        firstRec = false;
                        try
                        {
                            if (myReader.GetString(0) == "3")
                                GuestCheckBox.Checked = true;  //guest
                            else
                                GuestCheckBox.Checked = false;  //guest
                        }
                        catch
                        {
                            GuestCheckBox.Checked = false;  //guest
                        }

                        try
                        {
                            TagIDTextBox.Text = myReader.GetString(1); //id
                        }
                        catch
                        {
                            TagIDTextBox.Text = "";
                        }

                        try
                        {
                            FNameTextBox.Text = myReader.GetString(2);  //fname
                        }
                        catch
                        {
                            FNameTextBox.Text = "";
                        }

                        try
                        {
                            LNameTextBox.Text = myReader.GetString(3);  //lname
                        }
                        catch
                        {
                            LNameTextBox.Text = "";
                        }

                        try
                        {
                            PassportTextBox.Text = myReader.GetString(4);  //passport
                        }
                        catch
                        {
                            PassportTextBox.Text = "";
                        }

                        try
                        {
                            CompanyTextBox.Text = myReader.GetString(5);  //dept
                        }
                        catch
                        {
                            CompanyTextBox.Text = "";
                        }

                        try
                        {
                            ExpDateTextBox.Text = myReader.GetDate(6).ToShortDateString();  //exp time
                        }
                        catch
                        {
                            ExpDateTextBox.Text = "";
                        }

                        try
                        {
                            ActiveDateTextBox.Text = myReader.GetString(7);	//act time
                        }
                        catch
                        {
                            ActiveDateTextBox.Text = "";
                        }

                        try
                        {
                            CommentTextBox.Text = myReader.GetString(8);	//comment
                        }
                        catch
                        {
                            CommentTextBox.Text = "";
                        }
                    }
                }//while

                myReader.Close();

				if (TagIDTextBox.Text.Length > 0)
				{
                    AccPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "employees"); //GetTagImage(tagID, m_connection);
					AccPictureBox.Invalidate();
				}

				/////////////////////////////////////////////////////////
				mySelectQuery = "SELECT FirstName, LastName FROM employees WHERE AccType = '1'";
				OdbcCommand myCommand1 = new OdbcCommand(mySelectQuery, m_connection); 

				/*try
				{
					myReader = myCommand1.ExecuteReader();
				}
				catch (Exception ex)
				{
					ShowErrorMessage(ex.Message);
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				}*/

				//lock (MainForm.m_connection)
				//{
				try
				{
					myReader = myCommand1.ExecuteReader();
					MainForm.reconnectCounter = -1;
					timer1.Enabled = false;
				}
				catch (Exception ex)
				{
					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{  
						//error code 2013
						DBStatusLabel.ForeColor = System.Drawing.Color.Red;
						DBStatusLabel.Text = "Disconnected";
						toolBar1.Enabled = false;
						
						if (MainForm.reconnectCounter < 0)
						{
							MainForm.reconnectCounter = 0;
							timer1.Enabled = true;
						}

						/*Thread.Sleep(500);

						if (!mForm.ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
						{
							MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							if (myReader != null)
							{
								if (!myReader.IsClosed)
									myReader.Close();
							}
							return;
						}

						DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
						DBStatusLabel.Text = "Connected";
						toolBar1.Enabled = true;*/
					                                  
					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;
				}//catch .. try
				//}//lock
			
				while (myReader.Read())
				{
					NameComboBox.Items.Add(myReader.GetString(0) + " " + myReader.GetString(1));  //name
				}

				myReader.Close();

			}//lock m_connection
			///////////////////////////////////////////////////////////////
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
            this.components = new System.ComponentModel.Container();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.NewToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.EditToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.CancelToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.SaveToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.DeleteToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.RefreshToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ExpDateTextBox = new System.Windows.Forms.TextBox();
            this.PreButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.LastButton = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.FirstButton = new System.Windows.Forms.Button();
            this.DBStatusLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ActiveDateTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenImageButton = new System.Windows.Forms.Button();
            this.AccPictureBox = new System.Windows.Forms.PictureBox();
            this.NameComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CompanyTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GuestCheckBox = new System.Windows.Forms.CheckBox();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.FNameTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.OpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.Label = new System.Windows.Forms.Label();
            this.LNameTextBox = new System.Windows.Forms.TextBox();
            this.TagIDTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.EmpNameLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Timers.Timer();
            this.label7 = new System.Windows.Forms.Label();
            this.PassportTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.CommentTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.AccPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolBar1
            // 
            this.toolBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.AutoSize = false;
            this.toolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.NewToolBarButton,
            this.EditToolBarButton,
            this.CancelToolBarButton,
            this.SaveToolBarButton,
            this.DeleteToolBarButton,
            this.RefreshToolBarButton});
            this.toolBar1.ButtonSize = new System.Drawing.Size(75, 28);
            this.toolBar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(2, 4);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(732, 35);
            this.toolBar1.TabIndex = 42;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // NewToolBarButton
            // 
            this.NewToolBarButton.Name = "NewToolBarButton";
            this.NewToolBarButton.Text = "New";
            this.NewToolBarButton.ToolTipText = "Create new record";
            // 
            // EditToolBarButton
            // 
            this.EditToolBarButton.Name = "EditToolBarButton";
            this.EditToolBarButton.Text = "Edit";
            this.EditToolBarButton.ToolTipText = "Edit record";
            // 
            // CancelToolBarButton
            // 
            this.CancelToolBarButton.Name = "CancelToolBarButton";
            this.CancelToolBarButton.Text = "Cancel";
            this.CancelToolBarButton.ToolTipText = "Cancel New or Edit record";
            // 
            // SaveToolBarButton
            // 
            this.SaveToolBarButton.Name = "SaveToolBarButton";
            this.SaveToolBarButton.Text = "Save";
            this.SaveToolBarButton.ToolTipText = "Save the record";
            // 
            // DeleteToolBarButton
            // 
            this.DeleteToolBarButton.Name = "DeleteToolBarButton";
            this.DeleteToolBarButton.Text = "Delete";
            this.DeleteToolBarButton.ToolTipText = "Delete the record";
            // 
            // RefreshToolBarButton
            // 
            this.RefreshToolBarButton.Name = "RefreshToolBarButton";
            this.RefreshToolBarButton.Text = "Refresh";
            this.RefreshToolBarButton.ToolTipText = "Refresh the screen";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(55, 2);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ExpDateTextBox
            // 
            this.ExpDateTextBox.Location = new System.Drawing.Point(100, 214);
            this.ExpDateTextBox.Name = "ExpDateTextBox";
            this.ExpDateTextBox.ReadOnly = true;
            this.ExpDateTextBox.Size = new System.Drawing.Size(214, 20);
            this.ExpDateTextBox.TabIndex = 6;
            // 
            // PreButton
            // 
            this.PreButton.ForeColor = System.Drawing.Color.Blue;
            this.PreButton.Location = new System.Drawing.Point(448, 580);
            this.PreButton.Name = "PreButton";
            this.PreButton.Size = new System.Drawing.Size(75, 27);
            this.PreButton.TabIndex = 81;
            this.PreButton.Text = "Previous";
            this.PreButton.Click += new System.EventHandler(this.PreButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.ForeColor = System.Drawing.Color.Blue;
            this.NextButton.Location = new System.Drawing.Point(370, 580);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 27);
            this.NextButton.TabIndex = 80;
            this.NextButton.Text = "Next";
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // LastButton
            // 
            this.LastButton.ForeColor = System.Drawing.Color.Blue;
            this.LastButton.Location = new System.Drawing.Point(292, 580);
            this.LastButton.Name = "LastButton";
            this.LastButton.Size = new System.Drawing.Size(75, 27);
            this.LastButton.TabIndex = 79;
            this.LastButton.Text = "Last";
            this.LastButton.Click += new System.EventHandler(this.LastButton_Click);
            // 
            // listView
            // 
            this.listView.BackColor = System.Drawing.SystemColors.Info;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader7,
            this.columnHeader9,
            this.columnHeader8,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader10});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(8, 364);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(716, 204);
            this.listView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView.TabIndex = 77;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.listView.Click += new System.EventHandler(this.listView_Click);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 2;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Guest";
            this.columnHeader5.Width = 40;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tag ID";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "First Name";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Last Name";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "IC / Passport";
            this.columnHeader9.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Company";
            this.columnHeader8.Width = 130;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Expiration Date";
            this.columnHeader4.Width = 110;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Activation Date";
            this.columnHeader6.Width = 125;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Comment";
            this.columnHeader10.Width = 460;
            // 
            // FirstButton
            // 
            this.FirstButton.ForeColor = System.Drawing.Color.Blue;
            this.FirstButton.Location = new System.Drawing.Point(214, 580);
            this.FirstButton.Name = "FirstButton";
            this.FirstButton.Size = new System.Drawing.Size(75, 27);
            this.FirstButton.TabIndex = 78;
            this.FirstButton.Text = "First";
            this.FirstButton.Click += new System.EventHandler(this.FirstButton_Click);
            // 
            // DBStatusLabel
            // 
            this.DBStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.DBStatusLabel.Location = new System.Drawing.Point(100, 334);
            this.DBStatusLabel.Name = "DBStatusLabel";
            this.DBStatusLabel.Size = new System.Drawing.Size(90, 18);
            this.DBStatusLabel.TabIndex = 76;
            this.DBStatusLabel.Text = "Disconnected";
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(4, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 18);
            this.label5.TabIndex = 75;
            this.label5.Text = "Database Status: ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ActiveDateTextBox
            // 
            this.ActiveDateTextBox.Location = new System.Drawing.Point(100, 254);
            this.ActiveDateTextBox.Name = "ActiveDateTextBox";
            this.ActiveDateTextBox.ReadOnly = true;
            this.ActiveDateTextBox.Size = new System.Drawing.Size(214, 20);
            this.ActiveDateTextBox.TabIndex = 74;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 18);
            this.label4.TabIndex = 73;
            this.label4.Text = "Activation Date: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 18);
            this.label1.TabIndex = 72;
            this.label1.Text = "Expiration Date: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OpenImageButton
            // 
            this.OpenImageButton.Enabled = false;
            this.OpenImageButton.ForeColor = System.Drawing.Color.Black;
            this.OpenImageButton.Location = new System.Drawing.Point(502, 252);
            this.OpenImageButton.Name = "OpenImageButton";
            this.OpenImageButton.Size = new System.Drawing.Size(54, 23);
            this.OpenImageButton.TabIndex = 7;
            this.OpenImageButton.Text = "Open";
            this.OpenImageButton.Click += new System.EventHandler(this.OpenImageButton_Click);
            // 
            // AccPictureBox
            // 
            this.AccPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AccPictureBox.Location = new System.Drawing.Point(454, 100);
            this.AccPictureBox.Name = "AccPictureBox";
            this.AccPictureBox.Size = new System.Drawing.Size(142, 148);
            this.AccPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AccPictureBox.TabIndex = 70;
            this.AccPictureBox.TabStop = false;
            // 
            // NameComboBox
            // 
            this.NameComboBox.Enabled = false;
            this.NameComboBox.Location = new System.Drawing.Point(548, 52);
            this.NameComboBox.Name = "NameComboBox";
            this.NameComboBox.Size = new System.Drawing.Size(178, 21);
            this.NameComboBox.Sorted = true;
            this.NameComboBox.TabIndex = 3;
            this.NameComboBox.Visible = false;
            this.NameComboBox.DoubleClick += new System.EventHandler(this.NameComboBox_DoubleClick);
            this.NameComboBox.SelectedIndexChanged += new System.EventHandler(this.NameComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(4, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 18);
            this.label3.TabIndex = 68;
            this.label3.Text = "First Name : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CompanyTextBox
            // 
            this.CompanyTextBox.Location = new System.Drawing.Point(100, 174);
            this.CompanyTextBox.Name = "CompanyTextBox";
            this.CompanyTextBox.ReadOnly = true;
            this.CompanyTextBox.Size = new System.Drawing.Size(212, 20);
            this.CompanyTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(4, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 18);
            this.label2.TabIndex = 66;
            this.label2.Text = "Company: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GuestCheckBox
            // 
            this.GuestCheckBox.Checked = true;
            this.GuestCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GuestCheckBox.Enabled = false;
            this.GuestCheckBox.Location = new System.Drawing.Point(270, 92);
            this.GuestCheckBox.Name = "GuestCheckBox";
            this.GuestCheckBox.Size = new System.Drawing.Size(16, 24);
            this.GuestCheckBox.TabIndex = 0;
            this.GuestCheckBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GuestCheckBox_MouseDown);
            this.GuestCheckBox.CheckedChanged += new System.EventHandler(this.GuestCheckBox_CheckedChanged);
            this.GuestCheckBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GuestCheckBox_MouseUp);
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.Location = new System.Drawing.Point(102, 214);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(194, 20);
            this.DateTimePicker.TabIndex = 83;
            this.DateTimePicker.Visible = false;
            // 
            // FNameTextBox
            // 
            this.FNameTextBox.Location = new System.Drawing.Point(100, 54);
            this.FNameTextBox.Name = "FNameTextBox";
            this.FNameTextBox.ReadOnly = true;
            this.FNameTextBox.Size = new System.Drawing.Size(130, 20);
            this.FNameTextBox.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(288, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 18);
            this.label6.TabIndex = 85;
            this.label6.Text = "Guest";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // OpenFileDlg
            // 
            this.OpenFileDlg.RestoreDirectory = true;
            // 
            // Label
            // 
            this.Label.ForeColor = System.Drawing.Color.Black;
            this.Label.Location = new System.Drawing.Point(252, 54);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(32, 18);
            this.Label.TabIndex = 86;
            this.Label.Text = "Last: ";
            this.Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LNameTextBox
            // 
            this.LNameTextBox.Location = new System.Drawing.Point(286, 52);
            this.LNameTextBox.Name = "LNameTextBox";
            this.LNameTextBox.ReadOnly = true;
            this.LNameTextBox.Size = new System.Drawing.Size(138, 20);
            this.LNameTextBox.TabIndex = 2;
            // 
            // TagIDTextBox
            // 
            this.TagIDTextBox.Location = new System.Drawing.Point(100, 94);
            this.TagIDTextBox.Name = "TagIDTextBox";
            this.TagIDTextBox.ReadOnly = true;
            this.TagIDTextBox.Size = new System.Drawing.Size(100, 20);
            this.TagIDTextBox.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(4, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 18);
            this.label8.TabIndex = 88;
            this.label8.Text = "Tag ID: ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EmpNameLabel
            // 
            this.EmpNameLabel.ForeColor = System.Drawing.Color.Black;
            this.EmpNameLabel.Location = new System.Drawing.Point(444, 54);
            this.EmpNameLabel.Name = "EmpNameLabel";
            this.EmpNameLabel.Size = new System.Drawing.Size(102, 18);
            this.EmpNameLabel.TabIndex = 89;
            this.EmpNameLabel.Text = "Employee Name:";
            this.EmpNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.EmpNameLabel.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(4, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 18);
            this.label7.TabIndex = 90;
            this.label7.Text = "IC / Passport : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PassportTextBox
            // 
            this.PassportTextBox.Location = new System.Drawing.Point(100, 134);
            this.PassportTextBox.Name = "PassportTextBox";
            this.PassportTextBox.ReadOnly = true;
            this.PassportTextBox.Size = new System.Drawing.Size(210, 20);
            this.PassportTextBox.TabIndex = 91;
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(4, 294);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 20);
            this.label9.TabIndex = 92;
            this.label9.Text = "Comment : ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.Location = new System.Drawing.Point(100, 294);
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.ReadOnly = true;
            this.CommentTextBox.Size = new System.Drawing.Size(628, 20);
            this.CommentTextBox.TabIndex = 93;
            // 
            // Form9
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(738, 613);
            this.Controls.Add(this.CommentTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.PassportTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.EmpNameLabel);
            this.Controls.Add(this.TagIDTextBox);
            this.Controls.Add(this.LNameTextBox);
            this.Controls.Add(this.ActiveDateTextBox);
            this.Controls.Add(this.CompanyTextBox);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.FNameTextBox);
            this.Controls.Add(this.ExpDateTextBox);
            this.Controls.Add(this.GuestCheckBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.PreButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.LastButton);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.FirstButton);
            this.Controls.Add(this.DBStatusLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OpenImageButton);
            this.Controls.Add(this.AccPictureBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DateTimePicker);
            this.Controls.Add(this.NameComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form9";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visitor Tag Definition";
            this.Load += new System.EventHandler(this.Form9_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AccPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (MainForm.m_connection == null)
				return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			lock (MainForm.m_connection)  //dec-06-06
			{
				if (e.Button.Text == "New")
				{
				
					GuestCheckBox.Focus();
					TagIDTextBox.Text = "";
					FNameTextBox.Text = "";
					LNameTextBox.Text = "";
                    PassportTextBox.Text = "";
					CompanyTextBox.Text = "";
					NameComboBox.Text = "";
					ExpDateTextBox.Text = "";
					ExpDateTextBox.Visible = false;
					DateTimePicker.Visible = true;
                    CommentTextBox.Text = "";
					OpenImageButton.Enabled = true;
					//FNameTextBox.Visible = false;
					//LNameTextBox.Visible = false;
					AccPictureBox.Image = null;

					TagIDTextBox.ReadOnly = false;
					NameComboBox.Enabled = true;
					NameComboBox.Visible = false;
					FNameTextBox.Visible = true;
					LNameTextBox.Visible = true;
					FNameTextBox.ReadOnly = false;
					LNameTextBox.ReadOnly = false;
                    PassportTextBox.ReadOnly = false;
					CompanyTextBox.ReadOnly = false;
                    CommentTextBox.ReadOnly = false;
					GuestCheckBox.Enabled = true;
					GuestCheckBox.Checked = true;
					newRec = true;
				
				}
				else if (e.Button.Text == "Edit")
				{
					if (listView.Items.Count == 0)
						return;
					FNameTextBox.Focus();
					oldEmployeeID = TagIDTextBox.Text;
					TagIDTextBox.ReadOnly = false;
					FNameTextBox.ReadOnly = false;
					LNameTextBox.ReadOnly = false;
                    PassportTextBox.ReadOnly = false;
					CompanyTextBox.ReadOnly = false;
                    CommentTextBox.ReadOnly = false;
					OpenImageButton.Enabled = true;
					GuestCheckBox.Enabled = true;
					if (GuestCheckBox.Checked)
					{
						NameComboBox.Visible = false;
						EmpNameLabel.Visible = false;
					}
					else
					{
						NameComboBox.Visible = true;
						NameComboBox.Enabled = true;
						EmpNameLabel.Visible = true;
					}

					DateTimePicker.Visible = true;
					ExpDateTextBox.Visible = false;
					newRec = false;
				}
				else if (e.Button.Text == "Cancel")
				{
					TagIDTextBox.ReadOnly = true;
					FNameTextBox.ReadOnly = true;
					FNameTextBox.Visible = true;
					LNameTextBox.ReadOnly = true;
					LNameTextBox.Visible = true;
                    PassportTextBox.ReadOnly = true;
					CompanyTextBox.Visible = true;
					CompanyTextBox.ReadOnly = true;
                    CommentTextBox.ReadOnly = true;
					EmpNameLabel.Visible = false;
					OpenImageButton.Enabled = false;
					NameComboBox.Enabled = false;
					NameComboBox.Visible = false;
					GuestCheckBox.Enabled = false;
					DateTimePicker.Visible = false;
					ExpDateTextBox.Visible = true;
					ExpDateTextBox.ReadOnly = true;

					if(this.listView.SelectedIndices.Count <= 0)
					{
						TagIDTextBox.Text = "";
						FNameTextBox.Text = "";
						//FNameTextBox.Visible = false;
						LNameTextBox.Text = "";
						//LNameTextBox.Visible = false;
						//DeptTextBox.Visible = false;
						ActiveDateTextBox.Text = "";
						GuestCheckBox.Checked = false;
						NameComboBox.Text = "";
						NameComboBox.Visible = false;
						ExpDateTextBox.Text = "";
                        PassportTextBox.Text = "";
                        CommentTextBox.Text = "";
						AccPictureBox.Image = null;
						return;
					}

					ListViewItem selItem = listView.SelectedItems[0];
					if (selItem.SubItems[1].Text == "Yes")
						GuestCheckBox.Checked = true;
					else
						GuestCheckBox.Checked = false;
					TagIDTextBox.Text = selItem.SubItems[2].Text;
					FNameTextBox.Text = selItem.SubItems[3].Text;
					LNameTextBox.Text = selItem.SubItems[4].Text;
                    PassportTextBox.Text = selItem.SubItems[5].Text;
					CompanyTextBox.Text = selItem.SubItems[6].Text;
					ExpDateTextBox.Text = selItem.SubItems[7].Text;
					ActiveDateTextBox.Text = selItem.SubItems[8].Text;
                    CommentTextBox.Text = selItem.SubItems[9].Text;


					if (TagIDTextBox.Text.Length > 0)
                        AccPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "employees");  //GetTagImage(TagIDTextBox.Text, m_connection);
				
				}
				else if (e.Button.Text == "Save")
				{
                    /*  EDC  Dec 2009
                     * there should be no check for selected items on new creation
                    if (listView.SelectedItems.Count == 0)
                    {
                        return;
                    }
                     */

					if (TagIDTextBox.Text.Equals(""))
					{
						ShowErrorMessage("Need Tag ID");
						TagIDTextBox.Focus();
						return;
					}

					if (((FNameTextBox.Text.Equals("") || LNameTextBox.Text.Equals("")) && GuestCheckBox.Checked) || 
						(NameComboBox.Text.Equals("") && !GuestCheckBox.Checked))
					{
						ShowErrorMessage("Need Name");
						FNameTextBox.Focus();
						NameComboBox.Focus();
						return;
					}

					TagIDTextBox.ReadOnly = true;
					NameComboBox.Visible = false;
					EmpNameLabel.Visible = false;
					FNameTextBox.Visible = true;
					FNameTextBox.ReadOnly = true;
					LNameTextBox.Visible = true;
					LNameTextBox.ReadOnly = true;
                    PassportTextBox.ReadOnly = true;
					CompanyTextBox.ReadOnly = true;
                    CommentTextBox.ReadOnly = true;
					OpenImageButton.Enabled = false;
					//ExpDateTextBox.Visible = true;
					//DateTimePicker.Visible = false;

					int FileSize;
					byte[] rawData = null;
					//FileStream fs;
					//string SQL = "";

					if (AccPictureBox.Image != null)
					{
						MemoryStream stream = new MemoryStream();
						AccPictureBox.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
						FileSize = Convert.ToInt32(stream.Length);
						rawData = new byte[FileSize];
						stream.Position  = 0;
						stream.Read(rawData, 0, FileSize);
						stream.Close();
					}
					else
					{
						rawData = new byte[1];
						rawData[0] = Convert.ToByte(0);
					}
			
					DateTime  time = DateTime.Now;
					string timeStr = time.ToString("MM-dd-yyyy  HH:mm:ss");
                
					string accType = "2";  //employee person tag
					if (GuestCheckBox.Checked)
						accType = "3";    //guest person tag
					DateTime expTime = DateTimePicker.Value;
				
					string fname = "";
					string lname = "";
					string dept = "";
				
					fname = FNameTextBox.Text;
					lname = LNameTextBox.Text;
					//dept = CompanyTextBox.Text;

					GuestCheckBox.Enabled = false;
                    //awiHelper.SaveParkingTag(false, newRec, "", "Car"+TagIDTextBox.Text, NameTextBox.Text, rawData, TagIDTextBox.Text, "", "", "", "", "", "1", "ACC");

                    if (!awiHelper.SaveEmployeesTag(newRec, TagIDTextBox.Text, "", fname, lname, PassportTextBox.Text, "", "", CompanyTextBox.Text, CommentTextBox.Text, rawData, accType, oldEmployeeID, "ACC", expTime))
						return;

                    //awiHelper.SaveTag(TagIDTextBox.Text, "ACC", NameTextBox.Text, rawData);
					ExpDateTextBox.Visible = true;
					DateTimePicker.Visible = false;
					ExpDateTextBox.Text = expTime.ToShortDateString();

					if (!newRec)
					{                        
                            ListViewItem selItem = listView.SelectedItems[0];
                            int index = selItem.Index;
                            if (index >= 0)
                                selItem.Remove();                        
					}

					ListViewItem listItem = new ListViewItem("");
					if (GuestCheckBox.Checked)
						listItem.SubItems.Add("Yes");
					else
						listItem.SubItems.Add("No");
					listItem.SubItems.Add(TagIDTextBox.Text);
					listItem.SubItems.Add(fname);
					listItem.SubItems.Add(lname);
                    listItem.SubItems.Add(PassportTextBox.Text);
					listItem.SubItems.Add(dept);
					listItem.SubItems.Add(ExpDateTextBox.Text);
					listItem.SubItems.Add(timeStr);
                    listItem.SubItems.Add(CommentTextBox.Text);                    
					listView.Items.Add(listItem);
					listItem.EnsureVisible();
					listItem.Selected = true;

				
				}
				else if (e.Button.Text == "Delete")
				{
					if (listView.SelectedItems.Count == 0)
						return;

					if (TagIDTextBox.Text.Equals(""))
					{
						ShowErrorMessage("No Tag ID");
						return;
					}

                    if (MessageBox.Show(this, "Delete the record?", "Smart Tracker", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
						return;

					TagIDTextBox.ReadOnly = true;
					FNameTextBox.ReadOnly = true;
					FNameTextBox.Visible = true;
					LNameTextBox.ReadOnly = true;
					LNameTextBox.Visible = true;
                    PassportTextBox.ReadOnly = true;
					CompanyTextBox.ReadOnly = true;
					CompanyTextBox.Visible = true;
					NameComboBox.Visible = false;
					ExpDateTextBox.ReadOnly = true;
					DateTimePicker.Visible = false;
                    CommentTextBox.ReadOnly = true;
					OpenImageButton.Enabled = false;
				

					StringBuilder sql = new StringBuilder();
					sql.Append("DELETE FROM employees WHERE ID = ");         
					sql.AppendFormat("'{0}'", TagIDTextBox.Text);
			     
					OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
				
					try
					{
						myCommand.ExecuteNonQuery();
						MainForm.reconnectCounter = -1;
						timer1.Enabled = false;
					}
					catch (Exception ex)
					{
						int ret = 0, ret1 = 0, ret2 = 0;
						if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
							((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
							((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
						{  
							//error code 2013
							if (MainForm.reconnectCounter < 0)
							{
								MainForm.reconnectCounter = 0;
								timer1.Enabled = true;
							}
						}
						else
							ShowErrorMessage(ex.Message);
						return;
					}

                    //awiHelper.DeleteTag(TagIDTextBox.Text, "ACC");
                    awiHelper.DeleteTag(TagIDTextBox.Text, "ACC");
				
					ListViewItem selItem = listView.SelectedItems[0];
					int index = selItem.Index;
					selItem.Remove();
					if (index > 0)
					{
						listView.Items[index-1].Selected = true;
						listView.Select();
					}
					else if ((index == 0) && (listView.Items.Count > 0))
					{
						listView.Items[0].Selected = true;
						listView.Select();
					}
					else if (listView.Items.Count == 0)
					{
						TagIDTextBox.Text = "";
						FNameTextBox.Text = "";
						LNameTextBox.Text = "";
                        PassportTextBox.Text = "";
						CompanyTextBox.Text = "";
						NameComboBox.Text = "";
						ExpDateTextBox.Text = "";
						ActiveDateTextBox.Text = "";
                        CommentTextBox.Text = "";
						AccPictureBox.Image = null;
					}
				   
				
				}
				else if (e.Button.Text == "Refresh")
				{
					listView.Items.Clear();
					TagIDTextBox.Text = "";
					FNameTextBox.Text = "";
					LNameTextBox.Text = "";
                    PassportTextBox.Text = "";
					CompanyTextBox.Text = "";
                    CommentTextBox.Text = "";
					NameComboBox.Text = "";
					NameComboBox.Visible = false;
					AccPictureBox.Image = null;
					FNameTextBox.ReadOnly  = true;
					FNameTextBox.Visible = true;
					LNameTextBox.ReadOnly  = true;
					LNameTextBox.Visible = true;
                    PassportTextBox.ReadOnly = true;
                    CommentTextBox.ReadOnly = true;
					CompanyTextBox.ReadOnly  = true;
					CompanyTextBox.Visible = true;
					DateTimePicker.Visible = false;
					ExpDateTextBox.Text = "";
					ActiveDateTextBox.Text = "";
					ExpDateTextBox.ReadOnly  = true;
					ActiveDateTextBox.ReadOnly  = true;
					newRec = false;

					string mySelectQuery = "SELECT accType, ID, FirstName, LastName, PassportNum, Company, expTime, Time, Comment FROM employees WHERE accType >= '2'"; // WHERE Type = 'AST' AND AccType = '1'";
					OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection); 

					OdbcDataReader myReader = null;
				
					/*try
					{
						myReader = myCommand.ExecuteReader();
					}
					catch (Exception ex)
					{
						ShowErrorMessage(ex.Message);
						if (myReader != null)
						{
							if (!myReader.IsClosed)
								myReader.Close();
						}
						return;
					}*/

					//lock (MainForm.m_connection)
					//{
					try
					{
						myReader = myCommand.ExecuteReader();
						MainForm.reconnectCounter = -1;
						timer1.Enabled = false;
					}
					catch (Exception ex)
					{
						int ret = 0, ret1 = 0, ret2 = 0;
						if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
							((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
							((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
						{  
							//error code 2013
							DBStatusLabel.ForeColor = System.Drawing.Color.Red;
							DBStatusLabel.Text = "Disconnected";
							toolBar1.Enabled = false;
							
							if (MainForm.reconnectCounter < 0)
							{
								MainForm.reconnectCounter = 0;
								timer1.Enabled = true;
							}

							/*Thread.Sleep(500);

							if (!mForm.ReconnectToDBServer()) //this program sould be done either in main or in the DBInterface.
							{
								MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								if (myReader != null)
								{
									if (!myReader.IsClosed)
										myReader.Close();
								}
								return;
							}

							DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
							DBStatusLabel.Text = "Connected";
							toolBar1.Enabled = true;*/
					                                  
						}
						if (myReader != null)
						{
							if (!myReader.IsClosed)
								myReader.Close();
						}
						return;
					}//catch .. try
					//}//lock

					bool firstRec = true;
					int myRec = 0;
					while (myReader.Read())
					{
						myRec += 1;
						if (myRec > MAX_DEMO_RECORDS)
							break;
						ListViewItem listItem = new ListViewItem("");  //first index
						try
						{
							if (myReader.GetString(0) == "3")
								listItem.SubItems.Add("Yes");  //guest
							else
								listItem.SubItems.Add("No");     //No guest
						}
						catch
						{
                                listItem.SubItems.Add("No");     //No guest
						}

						try
						{
							listItem.SubItems.Add(myReader.GetString(1));  //ID
						}
						catch
						{
                            listItem.SubItems.Add("");
						}

						try
						{
							listItem.SubItems.Add(myReader.GetString(2));  //fname
						}
						catch
						{
                            listItem.SubItems.Add("");
						}

						try
						{
							listItem.SubItems.Add(myReader.GetString(3));  //lname
						}
						catch
						{
                            listItem.SubItems.Add("");
						}

                        try
                        {
                            listItem.SubItems.Add(myReader.GetString(4));  //passport
                        }
                        catch
                        {
                            listItem.SubItems.Add("");
                        }

						try
						{
							listItem.SubItems.Add(myReader.GetString(5));  //company
						}
						catch
						{
                            listItem.SubItems.Add("");
						}

						try
						{
							listItem.SubItems.Add(myReader.GetDate(6).ToShortDateString());  //exp time
						}
						catch
						{
                            listItem.SubItems.Add("");
						}

						try
						{
							listItem.SubItems.Add(myReader.GetString(7));  //act time
						}
						catch
						{
                            listItem.SubItems.Add("");
						}

			            try
						{
							listItem.SubItems.Add(myReader.GetString(8));  //comment
						}
						catch
						{
                            listItem.SubItems.Add("");
						}
                        
						listView.Items.Add(listItem);
						if (firstRec)
						{
							//listItem.Selected = true;
							firstRec = false;
							try
							{
								if (myReader.GetString(0) == "3")
									GuestCheckBox.Checked = true;  //guest
								else
									GuestCheckBox.Checked = false;  //guest
							}
							catch
							{
                               GuestCheckBox.Checked = false;  //guest
							}

							try
							{
								TagIDTextBox.Text = myReader.GetString(1); //id
							}
							catch
							{
                                TagIDTextBox.Text = "";
							}

							try
							{
								FNameTextBox.Text = myReader.GetString(2);  //fname
							}
							catch
							{
                                FNameTextBox.Text = "";
							}

							try
							{
								LNameTextBox.Text = myReader.GetString(3);  //lname
							}
							catch
							{
                                LNameTextBox.Text = "";
							}
                            
                            try
							{
                                PassportTextBox.Text = myReader.GetString(4);  //passport
							}
							catch
							{
                                PassportTextBox.Text = "";
							}

							try
							{
								CompanyTextBox.Text = myReader.GetString(5);  //dept
							}
							catch
							{
                                CompanyTextBox.Text = "";
							}

							try
							{
								ExpDateTextBox.Text = myReader.GetDate(6).ToShortDateString();  //exp time
							}
							catch
							{
                                ExpDateTextBox.Text = "";
							}

							try
							{
								ActiveDateTextBox.Text = myReader.GetString(7);	//act time
							}
							catch
							{
                                ActiveDateTextBox.Text = "";
							}
                            
                            try
							{
                                CommentTextBox.Text = myReader.GetString(8);	//comment
							}
							catch
							{
                                CommentTextBox.Text = "";
							}
						}
					}//while

					myReader.Close();

					if (TagIDTextBox.Text.Length > 0)
					{
                        AccPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "employees"); //GetTagImage(tagID, m_connection);
						AccPictureBox.Invalidate();
					}
				
					if (listView.Items.Count > 0)
						listView.Items[0].Selected = true;
				}
			}//m_connection
		}

		private void ShowErrorMessage(string msg)
		{
            MessageBox.Show(this, msg, "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void GuestCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			if (GuestBoxChecked)
			{
				GuestBoxChecked = false;

				if (GuestCheckBox.Checked)
				{
					NameComboBox.Visible = false;
					EmpNameLabel.Visible = false;
					//FNameTextBox.Visible = true;
					//FNameTextBox.ReadOnly = false;
					//LNameTextBox.Visible = true;
					//LNameLabel.Visible = true;
					//LNameTextBox.ReadOnly = false;
				}
				else
				{
					NameComboBox.Visible = true;
					EmpNameLabel.Visible = true;
				}

				if (newRec)
				{
					NameComboBox.Text = "";
					FNameTextBox.Text = "";
					LNameTextBox.Text = "";
					CompanyTextBox.Text = "";
				}
			}
		}

		private void Form9_Load(object sender, System.EventArgs e)
		{
		
		}

		private void DBConnectionStatusHandler(status stat, OdbcConnection connect)
		{
			if (stat == status.open)
			{
				m_connection = connect;

				MainForm.reconnectCounter = -1;
				timer1.Enabled = false;
				DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
				DBStatusLabel.Text = "Connected";
				toolBar1.Enabled = true;
			}
			else if (stat == status.broken)
			{
				m_connection = null;   
			}
			else if (stat == status.close)
			{
				m_connection = null;
			}
		}

		private void GuestCheckBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			
		}

		private void OpenImageButton_Click(object sender, System.EventArgs e)
		{
			Stream myStream;
			
			OpenFileDlg.Filter = "JPEG files (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp" ;
			OpenFileDlg.FilterIndex = 2 ;

			m_imageName = "";
			if(OpenFileDlg.ShowDialog() == DialogResult.OK)
			{
				if((myStream = OpenFileDlg.OpenFile())!= null)
				{
					m_imageName = OpenFileDlg.FileName;
					myStream.Close();
				}
			}

			if (m_imageName.Length > 0)
			{
				// Sets up an image object to be displayed.
				if (accImage != null)
				{
					accImage.Dispose();
				}

				// Stretches the image to fit the pictureBox.
				AccPictureBox.SizeMode = PictureBoxSizeMode.StretchImage ;
				accImage = new Bitmap(m_imageName);
				AccPictureBox.Image = (Image)accImage ;
			 
			}
		}

		private void listView_Click(object sender, System.EventArgs e)
		{
			ListViewItem selItem = listView.SelectedItems[0];
			int index = selItem.Index;
			if (selItem.SubItems[1].Text == "Yes")
				GuestCheckBox.Checked = true;
			else
                GuestCheckBox.Checked = false;
			TagIDTextBox.Text = selItem.SubItems[2].Text;
			FNameTextBox.Text = selItem.SubItems[3].Text;
			LNameTextBox.Text = selItem.SubItems[4].Text;
			CompanyTextBox.Text = selItem.SubItems[5].Text;
			ExpDateTextBox.Text = selItem.SubItems[6].Text;
			ActiveDateTextBox.Text = selItem.SubItems[7].Text;

			if (TagIDTextBox.Text.Length > 0)
                AccPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "employees"); //GetTagImage(TagIDTextBox.Text, m_connection);
		}

		private void listView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.listView.SelectedIndices.Count <= 0)
				return;

			ListViewItem selItem = listView.SelectedItems[0];
			if (selItem.SubItems[1].Text == "Yes")
				GuestCheckBox.Checked = true;
			else
				GuestCheckBox.Checked = false;
			TagIDTextBox.Text = selItem.SubItems[2].Text;
			FNameTextBox.Text = selItem.SubItems[3].Text;
			LNameTextBox.Text = selItem.SubItems[4].Text;
			CompanyTextBox.Text = selItem.SubItems[5].Text;
			ExpDateTextBox.Text = selItem.SubItems[6].Text;
			ActiveDateTextBox.Text = selItem.SubItems[7].Text;

			if (TagIDTextBox.Text.Length > 0)
                AccPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "employees"); //GetTagImage(TagIDTextBox.Text, m_connection);
			newRec = false;
		}

		private void GuestCheckBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
		   GuestBoxChecked = true;
		}

		private void FirstButton_Click(object sender, System.EventArgs e)
		{
			if (listView.Items.Count > 0)
			{
				listView.Items[0].Selected = true;
				listView.Select();
			}
		}

		private void LastButton_Click(object sender, System.EventArgs e)
		{
			if (listView.Items.Count > 0)
			{
				listView.Items[listView.Items.Count-1].Selected = true;
				listView.Select();
			}
		}

		private void NextButton_Click(object sender, System.EventArgs e)
		{
			if (listView.Items.Count > 0)
			{
				ListViewItem selItem = listView.SelectedItems[0];
				int index = selItem.Index;
				if ((index >= 0)&& (index < listView.Items.Count-1))
				{
					listView.Items[index+1].Selected = true;
					listView.Select();
				}
			}
		}

		private void PreButton_Click(object sender, System.EventArgs e)
		{
			if (listView.Items.Count > 0)
			{
				ListViewItem selItem = listView.SelectedItems[0];
				int index = selItem.Index;
				if (index >= 1)
				{
					listView.Items[index-1].Selected = true;
					listView.Select();
				}
			}
		}

		private void label6_Click(object sender, System.EventArgs e)
		{ 
		
		}

		private void NameComboBox_DoubleClick(object sender, System.EventArgs e)
		{
			
		}

		private void NameComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int len = 0;
			if ((len = NameComboBox.Text.Length) > 0)
			{
				int index = NameComboBox.Text.LastIndexOf(" ");
				string lname = NameComboBox.Text.Substring(index, len-index);
				string fname = NameComboBox.Text.Substring(0, index);
				FNameTextBox.Text = fname;
				LNameTextBox.Text = lname;
			}
		}

		private void listView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			m_sortReverse = (e.Column == m_sortColumn) ? !m_sortReverse : false;
			m_sortColumn = e.Column;
			listView.ListViewItemSorter = this;
			listView.Sort();
			listView.ListViewItemSorter = null;
		}

		public int Compare(object x, object y)
		{
			ListViewItem item1 = x as ListViewItem;
			ListViewItem item2 = y as ListViewItem;
			int rc = 0;

			rc = SortTags(m_sortColumn, item1, item2);

			return m_sortReverse ? -rc : rc;

		}

		private int SortTags(int column, ListViewItem item1, ListViewItem item2)
		{
			int rc = 0;
			string s1;
			string s2;
			//long d1;
			//long d2;


			switch (column)
			{
				case 2: //tagID
				
					int i1 = Convert.ToInt32(item1.SubItems[column].Text);
					int i2 = Convert.ToInt32(item2.SubItems[column].Text);
					
					if (i1 < i2)
						rc = -1;
					else if (i1 == i2)
						rc = 0;
					else
						rc = 1;
					break;

				default:
					s1 = item1.SubItems[column].Text;
					s2 = item2.SubItems[column].Text;
					rc = string.Compare(s1, s2);
					break;
			}	

			return rc;
		}

		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (MainForm.reconnectCounter == 3)
			{
				timer1.Enabled = false;
                MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (MainForm.reconnectCounter > 3)
			{
				timer1.Enabled = false;	
			}
			else
			{
				MainForm.reconnectCounter += 1;
				lock (MainForm.m_connection)
				{
					if(mForm.ReconnectToDBServer())
					{
						timer1.Enabled = false;
						MainForm.reconnectCounter = -1;
						DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
						DBStatusLabel.Text = "Connected";
						toolBar1.Enabled = true;
					}
				}
			}
		}
	}
}
