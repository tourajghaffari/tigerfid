using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using AWIComponentLib.Database;
using System.Data.Odbc;
using System.Data;
using System.Text;
using System.IO;
using AWI.SmartTracker.ReportClass;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for Form5.
	/// </summary>
	public class AccessTagForm : System.Windows.Forms.Form, IComparer	
	{
		private System.Windows.Forms.Button OpenImageButton;
		private System.Windows.Forms.Button RefreshButton;
		private System.Windows.Forms.Button DeleteButton;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.Button NewButton;
		private System.Windows.Forms.Button PreButton;
		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Button LastButton;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TagIDTextBox;
		private System.Windows.Forms.Button FirstButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox DateTimeTextBox;
		private bool newRec = false;
		private OdbcConnection m_connection = null;
		private Bitmap accImage ;
		private System.Windows.Forms.PictureBox AccPictureBox;
		private string m_imageName = "";
		private System.Windows.Forms.Button EditButton;
		private System.Windows.Forms.OpenFileDialog OpenFileDlg;
		private int MAX_DEMO_RECORDS = 10;
		private int records = 0;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton NewToolBarButton;
		private System.Windows.Forms.ToolBarButton EditToolBarButton;
		private System.Windows.Forms.ToolBarButton CancelToolBarButton;
		private System.Windows.Forms.ToolBarButton SaveToolBarButton;
		private System.Windows.Forms.ToolBarButton DeleteToolBarButton;
		private System.Windows.Forms.ToolBarButton RefreshToolBarButton;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		//private ActiveWave.CarTracker.AccessImageView tagImageView1;
		private System.ComponentModel.IContainer components;
		private OdbcDbClass odbcDB = new OdbcDbClass();
        public AWIHelperClass awiHelper;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label DBStatusLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.TextBox FNameTextBox;
		private System.Windows.Forms.TextBox LNameTextBox;
		private System.Windows.Forms.TextBox DepartmentTextBox;
		private string oldEmployeeID = "";
		private bool m_sortReverse= false;
		private int m_sortColumn = -1;
		private System.Timers.Timer timer1;
        private Label label7;
        private TextBox StaffNumTextBox;
        private TextBox CommentTextBox;
        private Label label9;
        private TextBox TitleTextBox;
        private Label label8;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private Label label10;
        private ListView lvGroups;
		private MainForm mForm;

        private ContextMenu ctxGroupsNew = new ContextMenu();
        private ContextMenu ctxGroupsSel = new ContextMenu();
        private bool hasGroupChanges = false;

		public AccessTagForm() {}

		public AccessTagForm(MainForm form)
		{
            mForm = form;
			InitializeComponent();

            MenuItem groupAdd = new MenuItem("Add");
            groupAdd.Click += new EventHandler(groupAdd_Click);

            MenuItem groupDelete = new MenuItem("Delete");
            groupDelete.Click += new EventHandler(groupDelete_Click);

            ctxGroupsNew.MenuItems.Add(groupAdd);
            ctxGroupsSel.MenuItems.AddRange(new MenuItem[] { groupAdd.CloneMenu(), groupDelete });

            lvGroups.ContextMenu = ctxGroupsNew;

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
			else
			{
				DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
				DBStatusLabel.Text = "Connected";
				m_connection = MainForm.m_connection;
			}

			/*if (MainForm.m_connection == null)
			{

				if (MainForm.providerName == dbProvider.SQL)
				{
					if (MainForm.conStr == "")
						return;  //no db connection

					if (!odbcDB.Connect(MainForm.conStr))  //SQL
					//if (!odbcDB.Connect("Driver={SQL Native Client};Server=Seyed02;Database=bank;Trusted_Connection=yes;Pooling=False;"))  //SQL
					{	
						return;
					}	
				}
				else if (MainForm.providerName == dbProvider.MySQL)
				{
					if (!odbcDB.Connect("DRIVER={MySQL ODBC 3.51 Driver};SERVER=localhost;DATABASE=parkingtracker;USER=root;PASSWORD=;OPTION=3;"))	//MYSQL
					{						
						return;
					}
				}
				else
				{					
					return;
				}
			}
			else
				m_connection = MainForm.m_connection;*/

            awiHelper = new AWIHelperClass(form);

            string mySelectQuery = "SELECT ID, StaffNum, FirstName, LastName, Title, Department, Time, Comment FROM employees WHERE AccType = '1'"; // WHERE Type = 'AST' AND AccType = '1'";
            OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection);

            OdbcDataReader myReader = null;
            try
            {
                myReader = myCommand.ExecuteReader();
                MainForm.reconnectCounter = -1;
                timer1.Enabled = false;
            }
            catch (Exception ex)
            {
                //bool found = false;
                int ret = 0, ret1 = 0, ret2 = 0;
                if (((ret = ex.Message.IndexOf("Lost connection")) >= 0) ||
                    ((ret1 = ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
                    ((ret2 = ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
                {
                    //error code 2013
                    //found = true;
                    DBStatusLabel.ForeColor = System.Drawing.Color.Red;
                    DBStatusLabel.Text = "Disconnected";
                    toolBar1.Enabled = false;

                    if (MainForm.reconnectCounter < 0)
                    {
                        MainForm.reconnectCounter = 0;
                        timer1.Enabled = true;
                    }                    
                }
                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                        myReader.Close();
                }
                return;
                
            }

            bool firstRec = true;
            int myRec = 0;            

            while (myReader.Read())
            {
                myRec += 1;
                if (myRec > MAX_DEMO_RECORDS)
                    break;

                ListViewItem listItem = new ListViewItem("");  //first index
                listItem.SubItems.Add(myReader.GetString(0));  //ID
                try
                {
                    listItem.SubItems.Add(myReader.GetString(1));  //staff num
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
                    listItem.SubItems.Add("");  //lname
                }

                try
                {
                    listItem.SubItems.Add(myReader.GetString(4));  //title
                }
                catch
                {
                    listItem.SubItems.Add("");  //title
                }

                try
                {
                    listItem.SubItems.Add(myReader.GetString(5));  //company
                }
                catch
                {
                    listItem.SubItems.Add("");  //company
                }

                try
                {
                    listItem.SubItems.Add(myReader.GetDateTime(6).ToString("MM-dd-yyyy  HH:mm:ss"));  //time
                }
                catch
                {
                    listItem.SubItems.Add("");  //time
                }

                try
                {
                    listItem.SubItems.Add(myReader.GetString(7));  //comment
                }
                catch
                {
                    listItem.SubItems.Add("");  //comment
                }

                listView.Items.Add(listItem);

                if (firstRec)
                {
                    listItem.Selected = true;
                    firstRec = false;

                    TagIDTextBox.Text = myReader.GetString(0);

                    try
                    {
                        StaffNumTextBox.Text = myReader.GetString(1);
                    }
                    catch
                    {
                        StaffNumTextBox.Text = "";
                    }

                    try
                    {
                        FNameTextBox.Text = myReader.GetString(2);
                    }
                    catch
                    {
                        FNameTextBox.Text = "";
                    }

                    try
                    {
                        LNameTextBox.Text = myReader.GetString(3);
                    }
                    catch
                    {
                        LNameTextBox.Text = "";
                    }

                    try
                    {
                        TitleTextBox.Text = myReader.GetString(4);
                    }
                    catch
                    {
                        TitleTextBox.Text = "";
                    }

                    try
                    {
                        DepartmentTextBox.Text = myReader.GetString(5);
                    }
                    catch
                    {
                        DepartmentTextBox.Text = "";
                    }

                    try
                    {
                        DateTimeTextBox.Text = myReader.GetDateTime(6).ToString("MM-dd-yyyy  HH:mm:ss");
                    }
                    catch
                    {
                        DateTimeTextBox.Text = "";
                    }

                    try
                    {
                        CommentTextBox.Text = myReader.GetString(7);
                    }
                    catch
                    {
                        CommentTextBox.Text = "";
                    }


                    lvGroups.Items.Clear();
                    var accessGroups = EmployeesQuery.GetEmployeeAccessGroup(Convert.ToUInt32(myReader["ID"]));
                    accessGroups.ForEach(group =>
                    {
                        ListViewItem item = new ListViewItem(group.Name);
                        item.Tag = group;
                        lvGroups.Items.Add(item);
                    });
                }
            }

            myReader.Close();

            if (listView.Items.Count > 0)
                listView.Items[0].Selected = true;

			if (TagIDTextBox.Text.Length > 0)
			{
                AccPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "employees"); //GetTagImage(tagID, m_connection);
				AccPictureBox.Invalidate();
			}
		}

        void groupDelete_Click(object sender, EventArgs e)
        {
            if (lvGroups.SelectedItems.Count > 0)
            {
                lvGroups.SelectedItems[0].Remove();
                hasGroupChanges = true;
            }
        }

        void groupAdd_Click(object sender, EventArgs e)
        {
            using (GroupPolicy form = new GroupPolicy())
            {
                DialogResult result = form.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    hasGroupChanges = true;
                    Group group = form.SelectedGroup;
                    ListViewItem item = new ListViewItem(group.Name);
                    item.Tag = group;
                    lvGroups.Items.Add(item);
                }
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessTagForm));
            this.OpenImageButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.NewButton = new System.Windows.Forms.Button();
            this.PreButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.LastButton = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AccPictureBox = new System.Windows.Forms.PictureBox();
            this.FNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TagIDTextBox = new System.Windows.Forms.TextBox();
            this.DateTimeTextBox = new System.Windows.Forms.TextBox();
            this.FirstButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.EditButton = new System.Windows.Forms.Button();
            this.OpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.NewToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.EditToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.CancelToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.SaveToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.DeleteToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.RefreshToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.DBStatusLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LNameTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.DepartmentTextBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Timers.Timer();
            this.label7 = new System.Windows.Forms.Label();
            this.StaffNumTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.CommentTextBox = new System.Windows.Forms.TextBox();
            this.lvGroups = new System.Windows.Forms.ListView();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AccPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenImageButton
            // 
            this.OpenImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenImageButton.Enabled = false;
            this.OpenImageButton.ForeColor = System.Drawing.Color.Blue;
            this.OpenImageButton.Location = new System.Drawing.Point(685, 189);
            this.OpenImageButton.Name = "OpenImageButton";
            this.OpenImageButton.Size = new System.Drawing.Size(54, 26);
            this.OpenImageButton.TabIndex = 4;
            this.OpenImageButton.Text = "Open";
            this.OpenImageButton.Click += new System.EventHandler(this.OpenImageButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.ForeColor = System.Drawing.Color.Blue;
            this.RefreshButton.Location = new System.Drawing.Point(514, 182);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(32, 23);
            this.RefreshButton.TabIndex = 37;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.Visible = false;
            // 
            // DeleteButton
            // 
            this.DeleteButton.ForeColor = System.Drawing.Color.Blue;
            this.DeleteButton.Location = new System.Drawing.Point(450, 177);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(32, 23);
            this.DeleteButton.TabIndex = 36;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.Visible = false;
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.ForeColor = System.Drawing.Color.Blue;
            this.SaveButton.Location = new System.Drawing.Point(422, 142);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(32, 23);
            this.SaveButton.TabIndex = 25;
            this.SaveButton.Text = "Save";
            this.SaveButton.Visible = false;
            // 
            // NewButton
            // 
            this.NewButton.ForeColor = System.Drawing.Color.Blue;
            this.NewButton.Location = new System.Drawing.Point(479, 137);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(36, 23);
            this.NewButton.TabIndex = 34;
            this.NewButton.Text = "New";
            this.NewButton.Visible = false;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // PreButton
            // 
            this.PreButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PreButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreButton.ForeColor = System.Drawing.Color.Blue;
            this.PreButton.Location = new System.Drawing.Point(317, 534);
            this.PreButton.Name = "PreButton";
            this.PreButton.Size = new System.Drawing.Size(75, 27);
            this.PreButton.TabIndex = 33;
            this.PreButton.Text = "Previous";
            this.PreButton.Click += new System.EventHandler(this.PreButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextButton.ForeColor = System.Drawing.Color.Blue;
            this.NextButton.Location = new System.Drawing.Point(398, 534);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 27);
            this.NextButton.TabIndex = 32;
            this.NextButton.Text = "Next";
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // LastButton
            // 
            this.LastButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LastButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastButton.ForeColor = System.Drawing.Color.Blue;
            this.LastButton.Location = new System.Drawing.Point(479, 534);
            this.LastButton.Name = "LastButton";
            this.LastButton.Size = new System.Drawing.Size(75, 27);
            this.LastButton.TabIndex = 31;
            this.LastButton.Text = "Last";
            this.LastButton.Click += new System.EventHandler(this.LastButton_Click);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.AutoArrange = false;
            this.listView.BackColor = System.Drawing.SystemColors.Info;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader7,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader2,
            this.columnHeader8,
            this.columnHeader4,
            this.columnHeader9});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(8, 296);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(772, 230);
            this.listView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView.TabIndex = 29;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.Click += new System.EventHandler(this.listView_Click);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 2;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tag ID";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Staff Num";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "First Name";
            this.columnHeader5.Width = 110;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Last Name";
            this.columnHeader6.Width = 109;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Title";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Department";
            this.columnHeader8.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Date & Time";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 145;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Comment";
            this.columnHeader9.Width = 450;
            // 
            // AccPictureBox
            // 
            this.AccPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AccPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AccPictureBox.Location = new System.Drawing.Point(644, 44);
            this.AccPictureBox.Name = "AccPictureBox";
            this.AccPictureBox.Size = new System.Drawing.Size(136, 140);
            this.AccPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AccPictureBox.TabIndex = 28;
            this.AccPictureBox.TabStop = false;
            // 
            // FNameTextBox
            // 
            this.FNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FNameTextBox.Location = new System.Drawing.Point(110, 100);
            this.FNameTextBox.Name = "FNameTextBox";
            this.FNameTextBox.ReadOnly = true;
            this.FNameTextBox.Size = new System.Drawing.Size(120, 21);
            this.FNameTextBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(574, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 26;
            this.label4.Text = "Date && Time: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 18);
            this.label1.TabIndex = 20;
            this.label1.Text = "Employee Tag ID: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TagIDTextBox
            // 
            this.TagIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TagIDTextBox.Location = new System.Drawing.Point(110, 60);
            this.TagIDTextBox.Name = "TagIDTextBox";
            this.TagIDTextBox.ReadOnly = true;
            this.TagIDTextBox.Size = new System.Drawing.Size(90, 21);
            this.TagIDTextBox.TabIndex = 0;
            // 
            // DateTimeTextBox
            // 
            this.DateTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DateTimeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimeTextBox.Location = new System.Drawing.Point(656, 260);
            this.DateTimeTextBox.Name = "DateTimeTextBox";
            this.DateTimeTextBox.ReadOnly = true;
            this.DateTimeTextBox.Size = new System.Drawing.Size(123, 21);
            this.DateTimeTextBox.TabIndex = 24;
            // 
            // FirstButton
            // 
            this.FirstButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FirstButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstButton.ForeColor = System.Drawing.Color.Blue;
            this.FirstButton.Location = new System.Drawing.Point(236, 534);
            this.FirstButton.Name = "FirstButton";
            this.FirstButton.Size = new System.Drawing.Size(75, 27);
            this.FirstButton.TabIndex = 30;
            this.FirstButton.Text = "First";
            this.FirstButton.Click += new System.EventHandler(this.FirstButton_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(4, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 18);
            this.label3.TabIndex = 24;
            this.label3.Text = "First Name : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // EditButton
            // 
            this.EditButton.ForeColor = System.Drawing.Color.Blue;
            this.EditButton.Location = new System.Drawing.Point(469, 161);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(30, 23);
            this.EditButton.TabIndex = 38;
            this.EditButton.Text = "Edit";
            this.EditButton.Visible = false;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // OpenFileDlg
            // 
            this.OpenFileDlg.RestoreDirectory = true;
            // 
            // toolBar1
            // 
            this.toolBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
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
            this.toolBar1.Location = new System.Drawing.Point(4, 2);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(784, 32);
            this.toolBar1.TabIndex = 5;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // NewToolBarButton
            // 
            this.NewToolBarButton.Name = "NewToolBarButton";
            this.NewToolBarButton.Text = "New";
            // 
            // EditToolBarButton
            // 
            this.EditToolBarButton.Name = "EditToolBarButton";
            this.EditToolBarButton.Text = "Edit";
            // 
            // CancelToolBarButton
            // 
            this.CancelToolBarButton.Name = "CancelToolBarButton";
            this.CancelToolBarButton.Text = "Cancel";
            // 
            // SaveToolBarButton
            // 
            this.SaveToolBarButton.Name = "SaveToolBarButton";
            this.SaveToolBarButton.Text = "Save";
            // 
            // DeleteToolBarButton
            // 
            this.DeleteToolBarButton.Name = "DeleteToolBarButton";
            this.DeleteToolBarButton.Text = "Delete";
            // 
            // RefreshToolBarButton
            // 
            this.RefreshToolBarButton.Name = "RefreshToolBarButton";
            this.RefreshToolBarButton.Text = "Refresh";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(4, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 18);
            this.label2.TabIndex = 42;
            this.label2.Text = "Database Status: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // DBStatusLabel
            // 
            this.DBStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DBStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.DBStatusLabel.Location = new System.Drawing.Point(110, 260);
            this.DBStatusLabel.Name = "DBStatusLabel";
            this.DBStatusLabel.Size = new System.Drawing.Size(90, 18);
            this.DBStatusLabel.TabIndex = 43;
            this.DBStatusLabel.Text = "Disconnected";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(239, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 18);
            this.label5.TabIndex = 44;
            this.label5.Text = "Last :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LNameTextBox
            // 
            this.LNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LNameTextBox.Location = new System.Drawing.Point(281, 100);
            this.LNameTextBox.Name = "LNameTextBox";
            this.LNameTextBox.ReadOnly = true;
            this.LNameTextBox.Size = new System.Drawing.Size(120, 21);
            this.LNameTextBox.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(4, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 18);
            this.label6.TabIndex = 46;
            this.label6.Text = "Department : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // DepartmentTextBox
            // 
            this.DepartmentTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DepartmentTextBox.Location = new System.Drawing.Point(110, 180);
            this.DepartmentTextBox.Name = "DepartmentTextBox";
            this.DepartmentTextBox.ReadOnly = true;
            this.DepartmentTextBox.Size = new System.Drawing.Size(291, 21);
            this.DepartmentTextBox.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(206, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 18);
            this.label7.TabIndex = 47;
            this.label7.Text = "Staff Number :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // StaffNumTextBox
            // 
            this.StaffNumTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StaffNumTextBox.Location = new System.Drawing.Point(298, 60);
            this.StaffNumTextBox.MaxLength = 6;
            this.StaffNumTextBox.Name = "StaffNumTextBox";
            this.StaffNumTextBox.ReadOnly = true;
            this.StaffNumTextBox.Size = new System.Drawing.Size(103, 21);
            this.StaffNumTextBox.TabIndex = 48;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(4, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 18);
            this.label8.TabIndex = 49;
            this.label8.Text = "Title : ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleTextBox.Location = new System.Drawing.Point(110, 140);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.ReadOnly = true;
            this.TitleTextBox.Size = new System.Drawing.Size(291, 21);
            this.TitleTextBox.TabIndex = 50;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(4, 222);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 18);
            this.label9.TabIndex = 51;
            this.label9.Text = "Comment : ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommentTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommentTextBox.Location = new System.Drawing.Point(110, 220);
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.ReadOnly = true;
            this.CommentTextBox.Size = new System.Drawing.Size(669, 21);
            this.CommentTextBox.TabIndex = 52;
            // 
            // lvGroups
            // 
            this.lvGroups.Enabled = false;
            this.lvGroups.FullRowSelect = true;
            this.lvGroups.Location = new System.Drawing.Point(421, 60);
            this.lvGroups.Name = "lvGroups";
            this.lvGroups.Size = new System.Drawing.Size(203, 141);
            this.lvGroups.TabIndex = 53;
            this.lvGroups.UseCompatibleStateImageBehavior = false;
            this.lvGroups.View = System.Windows.Forms.View.List;
            this.lvGroups.ItemActivate += new System.EventHandler(this.lvGroups_ItemActivate);
            this.lvGroups.SelectedIndexChanged += new System.EventHandler(this.lvGroups_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(422, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 54;
            this.label10.Text = "Member of:";
            // 
            // AccessTagForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(791, 569);
            this.Controls.Add(this.lvGroups);
            this.Controls.Add(this.CommentTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.TitleTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.StaffNumTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DepartmentTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LNameTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DBStatusLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.FNameTextBox);
            this.Controls.Add(this.TagIDTextBox);
            this.Controls.Add(this.DateTimeTextBox);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.PreButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.LastButton);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.AccPictureBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FirstButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OpenImageButton);
            this.Controls.Add(this.label10);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccessTagForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Employee Tag Registration Form";
            ((System.ComponentModel.ISupportInitialize)(this.AccPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

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

		/*public Image GetTagImage(string tagId)
        {
			string sql = string.Format("SELECT Image FROM parkingtags WHERE ID = '{0}'", tagId);
			OdbcCommand cmd = new OdbcCommand(sql, m_connection);
            
            byte[] data = cmd.ExecuteScalar() as byte[];
            if (data != null)
            {
               try
               {
                  Stream stream = new MemoryStream(data);
                  return Image.FromStream(stream);
               }
				catch {}
           }
           return null;
        }*/

		private void NewButton_Click(object sender, System.EventArgs e)
		{
			

		    TagIDTextBox.Text = "";
			FNameTextBox.Text = "";
			FNameTextBox.Text = "";
			DepartmentTextBox.Text = "";
			//AptComboBox.Text = "";
			DateTimeTextBox.Text = "";
			OpenImageButton.Enabled = true;
			AccPictureBox.Image = null;
			TagIDTextBox.Focus();

			TagIDTextBox.ReadOnly = false;
			FNameTextBox.ReadOnly = false;
			LNameTextBox.ReadOnly = false;
			DepartmentTextBox.ReadOnly = false;
			//AptComboBox.Enabled = false;
			newRec = true;
			SaveButton.Enabled=  true;

            lvGroups.Items.Clear();
        }

		private void ShowErrorMessage(string msg)
		{
			MessageBox.Show(this, msg, "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void listView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.listView.SelectedIndices.Count <= 0)
				return;

			ListViewItem selItem = listView.SelectedItems[0];
			TagIDTextBox.Text = selItem.SubItems[1].Text;
            StaffNumTextBox.Text = selItem.SubItems[2].Text;
			FNameTextBox.Text = selItem.SubItems[3].Text;
			LNameTextBox.Text = selItem.SubItems[4].Text;
            TitleTextBox.Text = selItem.SubItems[5].Text;
            DepartmentTextBox.Text = selItem.SubItems[6].Text;
			DateTimeTextBox.Text = selItem.SubItems[7].Text;
            CommentTextBox.Text = selItem.SubItems[8].Text;

            lvGroups.Items.Clear();
            var accessGroups = EmployeesQuery.GetEmployeeAccessGroup(uint.Parse(TagIDTextBox.Text));
            accessGroups.ForEach(group =>
            {
                ListViewItem item = new ListViewItem(group.Name);
                item.Tag = group;
                lvGroups.Items.Add(item);
            });

			if (TagIDTextBox.Text.Length > 0)
                AccPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "employees"); //GetTagImage(TagIDTextBox.Text, m_connection);
			newRec = false;
		}

		private void listView_Click(object sender, System.EventArgs e)
		{
			ListViewItem selItem = listView.SelectedItems[0];
		    int index = selItem.Index;
            TagIDTextBox.Text = selItem.SubItems[1].Text;
            StaffNumTextBox.Text = selItem.SubItems[2].Text;
            FNameTextBox.Text = selItem.SubItems[3].Text;
            LNameTextBox.Text = selItem.SubItems[4].Text;
            TitleTextBox.Text = selItem.SubItems[5].Text;
            DepartmentTextBox.Text = selItem.SubItems[6].Text;
            DateTimeTextBox.Text = selItem.SubItems[7].Text;
            CommentTextBox.Text = selItem.SubItems[8].Text;

            lvGroups.Items.Clear();
            var accessGroups = EmployeesQuery.GetEmployeeAccessGroup(uint.Parse(TagIDTextBox.Text));
            accessGroups.ForEach(group =>
            {
                ListViewItem item = new ListViewItem(group.Name);
                item.Tag = group;
                lvGroups.Items.Add(item);
            });
            
            if (TagIDTextBox.Text.Length > 0)
                AccPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "employees"); //GetTagImage(TagIDTextBox.Text, m_connection);
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

		private void OpenImageButton_Click(object sender, System.EventArgs e)
		{
			Stream myStream;
            //OpenFileDialog openFileDlg = new OpenFileDialog();

           //OpenFileDlg.InitialDirectory = "c:\\" ;
           OpenFileDlg.Filter = "JPEG files (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp" ;
           OpenFileDlg.FilterIndex = 2 ;
           //OpenFileDlg.RestoreDirectory = true ;

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

		}//OpenImageButton_Click

		private void EditButton_Click(object sender, System.EventArgs e)
		{
			TagIDTextBox.ReadOnly = true;
			FNameTextBox.ReadOnly = false;
			LNameTextBox.ReadOnly = false;
			DepartmentTextBox.ReadOnly = false;
			//AptComboBox.Enabled = false;;
			OpenImageButton.Enabled = true;
			newRec = false;
			SaveButton.Enabled=  true;
		}

		private Image GetScaledImage(Image image)
        {
           if (image != null)
           {
				// Get image sized to picture box, but maintain aspect ratio
				Size size = AccPictureBox.Size;
				float ar1 = (float)size.Width / (float)size.Height;
				float ar2 = (float)image.Width / (float)image.Height;
				if (ar1 > ar2)
				size.Width = Convert.ToInt32(size.Height * ar2);
				else if (ar2 > ar1)
				size.Height = Convert.ToInt32(size.Width / ar2);

				return new Bitmap(image, size);
           }
           return null;
      }

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
		   if (e.Button.Text == "New")
		   {
				TagIDTextBox.Text = "";
                StaffNumTextBox.Text = "";
				FNameTextBox.Text = "";
			    LNameTextBox.Text = "";
                TitleTextBox.Text = "";
			    DepartmentTextBox.Text = "";
				DateTimeTextBox.Text = "";
                CommentTextBox.Text = "";
				OpenImageButton.Enabled = true;
				AccPictureBox.Image = null;
				TagIDTextBox.Focus();

				TagIDTextBox.ReadOnly = false;
                StaffNumTextBox.ReadOnly = false;
				FNameTextBox.ReadOnly = false;
			    LNameTextBox.ReadOnly = false;
                TitleTextBox.ReadOnly = false;
			    DepartmentTextBox.ReadOnly = false;
                CommentTextBox.ReadOnly = false;
				//AptComboBox.Enabled = false;
				newRec = true;
				SaveButton.Enabled=  true;
                lvGroups.Enabled = true;

                lvGroups.Items.Clear();
		   }
		   else if (e.Button.Text == "Edit")
		   {
               if (listView.Items.Count == 0)
                   return;

			    TagIDTextBox.Focus();
			    oldEmployeeID = TagIDTextBox.Text;
			    TagIDTextBox.ReadOnly = false;
                StaffNumTextBox.ReadOnly = false;
				FNameTextBox.ReadOnly = false;
			    LNameTextBox.ReadOnly = false;
                TitleTextBox.ReadOnly = false;
			    DepartmentTextBox.ReadOnly = false;
                CommentTextBox.ReadOnly = false;
                
				OpenImageButton.Enabled = true;
				newRec = false;
				SaveButton.Enabled=  true;
                lvGroups.Enabled = true;
           }
		   else if (e.Button.Text == "Cancel")
		   {
			    TagIDTextBox.ReadOnly = true;
                StaffNumTextBox.ReadOnly = true;
			    FNameTextBox.ReadOnly = true;
			    LNameTextBox.ReadOnly = true;
                TitleTextBox.ReadOnly = true;
			    DepartmentTextBox.ReadOnly = true;
                CommentTextBox.ReadOnly = true;
				OpenImageButton.Enabled = false;
				SaveButton.Enabled = false;
                lvGroups.Enabled = false;

				if(this.listView.SelectedIndices.Count <= 0)
				{
					TagIDTextBox.Text = "";
                    StaffNumTextBox.Text = "";
					FNameTextBox.Text = "";
					LNameTextBox.Text = "";
                    TitleTextBox.Text = "";
					DepartmentTextBox.Text = "";
					DateTimeTextBox.Text = "";
                    CommentTextBox.Text = "";
					AccPictureBox.Image = null;
                    lvGroups.Items.Clear();
					return;
				}

				ListViewItem selItem = listView.SelectedItems[0];
				TagIDTextBox.Text = selItem.SubItems[1].Text;
                StaffNumTextBox.Text = selItem.SubItems[2].Text;
				FNameTextBox.Text = selItem.SubItems[3].Text;
			    LNameTextBox.Text = selItem.SubItems[4].Text;
                TitleTextBox.Text = selItem.SubItems[5].Text;
			    DepartmentTextBox.Text = selItem.SubItems[6].Text;
				DateTimeTextBox.Text = selItem.SubItems[7].Text;
                CommentTextBox.Text = selItem.SubItems[8].Text;

                lvGroups.Items.Clear();
                var accessGroups = EmployeesQuery.GetEmployeeAccessGroup(Convert.ToUInt32(TagIDTextBox.Text));
                accessGroups.ForEach(group =>
                {
                    ListViewItem item = new ListViewItem(group.Name);
                    item.Tag = group;
                    lvGroups.Items.Add(item);
                });

                if (TagIDTextBox.Text.Length > 0)
                    AccPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "employees");  //GetTagImage(TagIDTextBox.Text, m_connection);
				
		   }
		   else if (e.Button.Text == "Save")
		   {
			    if (TagIDTextBox.Text.Equals(""))
				{
					ShowErrorMessage("Need Tag ID");
					TagIDTextBox.Focus();
					return;
				}

				TagIDTextBox.ReadOnly = true;
                StaffNumTextBox.ReadOnly = true;
				FNameTextBox.ReadOnly = true;
			    LNameTextBox.ReadOnly = true;
                TitleTextBox.ReadOnly = true;
			    DepartmentTextBox.ReadOnly = true;
                CommentTextBox.ReadOnly = true;
				OpenImageButton.Enabled = true;
				SaveButton.Enabled = false;
                lvGroups.Enabled = false;

				int FileSize;
				byte[] rawData = null;
				//FileStream fs;
				//string SQL = "";

				/*MemoryStream stream = new MemoryStream();
				if  (AccPictureBox.Image != null)
				{
					AccPictureBox.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
					FileSize = Convert.ToInt32(stream.Length);
					rawData = new byte[FileSize];
					stream.Position  = 0;
					stream.Read(rawData, 0, FileSize);
					stream.Close();
				}*/

			    if (AccPictureBox.Image != null)
			    {
				   MemoryStream stream = new MemoryStream();
				   AccPictureBox.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
				   FileSize = Convert.ToInt32(stream.Length);
				   rawData = new byte[FileSize];
				   stream.Position  = 0;
				   int n = stream.Read(rawData, 0, FileSize);
				   stream.Close();
			    }
			    else
			    {
				   rawData = new byte[1];
				   rawData[0] = Convert.ToByte(0);
			    }
			
				/*
				fs = new FileStream(m_imageName, FileMode.Open, FileAccess.Read);
				FileSize = Convert.ToInt32(fs.Length);
				rawData = new byte[FileSize];
				fs.Read(rawData, 0, FileSize);
				fs.Close();*/

				DateTime  time = DateTime.Now;
				string timeStr = time.ToString("MM-dd-yyyy HH:mm:ss");


                //awiHelper.SaveParkingTag(false, newRec, "", "Car"+TagIDTextBox.Text, NameTextBox.Text, rawData, TagIDTextBox.Text, "", "", "", "", "", "1", "ACC");
                DateTime dt = new DateTime();
                if (!awiHelper.SaveEmployeesTag(newRec, TagIDTextBox.Text, StaffNumTextBox.Text, FNameTextBox.Text, LNameTextBox.Text, "", TitleTextBox.Text, DepartmentTextBox.Text, "", CommentTextBox.Text, rawData, "1", oldEmployeeID, "ACC", dt))
				   return;

                uint id_old;
                if (uint.TryParse(oldEmployeeID, out id_old))
                    EmployeesQuery.DeleteEmployeeAccessGroups(id_old);

                uint id_new = uint.Parse(TagIDTextBox.Text);
                foreach (ListViewItem item in lvGroups.Items)
                {
                    var group = item.Tag as Group;
                    EmployeesQuery.InsertEmployeeAccessGroup(id_new, group);
                }

				if (!newRec)
				{
					ListViewItem selItem = listView.SelectedItems[0];
					int index = selItem.Index;
					if (index >= 0)
						selItem.Remove();	
				}

				ListViewItem listItem = new ListViewItem("");
			    listItem.SubItems.Add(TagIDTextBox.Text);
                listItem.SubItems.Add(StaffNumTextBox.Text);
				listItem.SubItems.Add(FNameTextBox.Text);
			    listItem.SubItems.Add(LNameTextBox.Text);
                listItem.SubItems.Add(TitleTextBox.Text);               
			    listItem.SubItems.Add(DepartmentTextBox.Text);
				listItem.SubItems.Add(timeStr);
                listItem.SubItems.Add(CommentTextBox.Text);
               
				listView.Items.Add(listItem);
				listItem.EnsureVisible();
				listItem.Selected = true;

				
			}
			else if (e.Button.Text == "Delete")
			{
			     if (listView.Items.Count == 0)
					return;

				 if (TagIDTextBox.Text.Equals(""))
				 {
					ShowErrorMessage("No Tag ID");
					return;
				 }

				if (MessageBox.Show(this, "Delete the record?", "Smart Tracker", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				    return;

				TagIDTextBox.ReadOnly = true;
                StaffNumTextBox.ReadOnly = true;
				FNameTextBox.ReadOnly = true;
			    LNameTextBox.ReadOnly = true;
                TitleTextBox.ReadOnly = true;
			    DepartmentTextBox.ReadOnly = true;
                CommentTextBox.ReadOnly = true;
				OpenImageButton.Enabled = false;
				SaveButton.Enabled = false;
                lvGroups.Enabled = false;

                EmployeesQuery.DeleteEmployeeAccessGroups(uint.Parse(TagIDTextBox.Text));

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

				/*StringBuilder sql1 = new StringBuilder();
				sql1.Append("DELETE FROM tags WHERE TagID = ");         
				sql1.AppendFormat("'ACC{0}'", TagIDTextBox.Text);
				OdbcCommand myCommand1 = new OdbcCommand(sql.ToString(), m_connection);
				
				try
				{
					myCommand1.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					ShowErrorMessage(ex.Message);
					return;
				}*/
            
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
                    StaffNumTextBox.Text = "";
					FNameTextBox.Text = "";
					LNameTextBox.Text = "";
                    TitleTextBox.Text = "";
					DepartmentTextBox.Text = "";					
					DateTimeTextBox.Text = "";
                    CommentTextBox.Text = "";
					AccPictureBox.Image = null;
                    lvGroups.Items.Clear();
				}
				   
				
			}
			else if (e.Button.Text == "Refresh")
			{
				listView.Items.Clear();
				TagIDTextBox.Text = "";                
                StaffNumTextBox.Text = "";
				FNameTextBox.Text = "";
			    LNameTextBox.Text = "";
                TitleTextBox.Text = "";
			    DepartmentTextBox.Text = "";
                CommentTextBox.Text = "";
				AccPictureBox.Image = null;
				SaveButton.Enabled = false;
                TagIDTextBox.ReadOnly = true;
                StaffNumTextBox.ReadOnly = true;
				FNameTextBox.ReadOnly  = true;
			    LNameTextBox.ReadOnly  = true;
                TitleTextBox.ReadOnly = true;
			    DepartmentTextBox.ReadOnly  = true;
                CommentTextBox.ReadOnly = true;
                lvGroups.Enabled = false;
                newRec = false;

				string mySelectQuery = "SELECT ID, StaffNum, FirstName, LastName, Title, Department, Time, Comment FROM employees WHERE AccType = '1'"; // WHERE Type = 'AST' AND AccType = '1'";
				OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection); 

				OdbcDataReader myReader = null;
				try
				{
					myReader = myCommand.ExecuteReader();
					MainForm.reconnectCounter = -1;
					timer1.Enabled = false;
				}
				catch (Exception ex)
				{
					//bool found = false;
					int ret = 0, ret1 = 0, ret2 = 0;
					if (((ret=ex.Message.IndexOf("Lost connection")) >= 0) ||
						((ret1=ex.Message.IndexOf("MySQL server has gone away")) >= 0) ||
						((ret2=ex.Message.IndexOf("(10061)")) >= 0)) //can not connect
					{  
						//error code 2013
						//found = true;
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

					/*ShowErrorMessage(ex.Message);
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;*/
				}

				bool firstRec = true;
				int myRec = 0;
				/*while (myReader.Read())
				{
					myRec += 1;
					if (myRec > MAX_DEMO_RECORDS)
						break;
					ListViewItem listItem = new ListViewItem("");  
					listItem.SubItems.Add(myReader.GetString(0));  //ID
					listItem.SubItems.Add(myReader.GetString(1));  //fname
					listItem.SubItems.Add(myReader.GetString(2));  //lname
					listItem.SubItems.Add(myReader.GetString(3));  //depatment
					listItem.SubItems.Add(myReader.GetString(4));  //time
					listView.Items.Add(listItem);
					if (firstRec)
					{
						//listItem.Selected = true;
						firstRec = false;
						TagIDTextBox.Text = myReader.GetString(0);
						FNameTextBox.Text = myReader.GetString(1);
						LNameTextBox.Text = myReader.GetString(2);
						DeptTextBox.Text = myReader.GetString(3);
						DateTimeTextBox.Text = myReader.GetString(4);
					}
				}//while*/

			   while (myReader.Read())
			   {
				   myRec += 1;
				   if (myRec > MAX_DEMO_RECORDS)
					   break;

				   ListViewItem listItem = new ListViewItem("");  //first index
				   listItem.SubItems.Add(myReader.GetString(0));  //ID
                   try
                   {
                       listItem.SubItems.Add(myReader.GetString(1));  //staff num
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
					   listItem.SubItems.Add("");  //lname
				   }

                   try
				   {
					   listItem.SubItems.Add(myReader.GetString(4));  //title
				   }
				   catch
				   {
					   listItem.SubItems.Add("");  //title
				   }
                   
				   try
				   {
					   listItem.SubItems.Add(myReader.GetString(5));  //department
				   }
				   catch
				   {
                       listItem.SubItems.Add("");  //department
				   }

				   try
				   {
					   //listItem.SubItems.Add(myReader.GetString(4));  //time
					   listItem.SubItems.Add(myReader.GetDateTime(6).ToString("MM-dd-yyyy  HH:mm:ss"));  //time
				   }
				   catch
				   {
					   listItem.SubItems.Add("");  //time
				   }

                   try
				   {
					   listItem.SubItems.Add(myReader.GetString(7));  //comment
				   }
				   catch
				   {
					   listItem.SubItems.Add("");  //comment
				   }
                   
				   listView.Items.Add(listItem);

				   if (firstRec)
				   {
					   listItem.Selected = true;
					   firstRec = false;
					
					   TagIDTextBox.Text = myReader.GetString(0);

                       try
                       {
                           StaffNumTextBox.Text = myReader.GetString(1);
                       }
                       catch
                       {
                           StaffNumTextBox.Text = "";
                       }

					   try
					   {
						   FNameTextBox.Text = myReader.GetString(2);
					   }
					   catch
					   {
						   FNameTextBox.Text = "";
					   }

					   try
					   {
						   LNameTextBox.Text = myReader.GetString(3);
					   }
					   catch
					   {
						   LNameTextBox.Text = "";
					   }

                       try
					   {
                           TitleTextBox.Text = myReader.GetString(4);
					   }
					   catch
					   {
                           TitleTextBox.Text = "";
					   }
                       
					   try
					   {
						   DepartmentTextBox.Text = myReader.GetString(5);
					   }
					   catch
					   {
						   DepartmentTextBox.Text = "";
					   }

					   try
					   {
						   //DateTimeTextBox.Text = myReader.GetString(4);
						   DateTimeTextBox.Text = myReader.GetDateTime(6).ToString("MM-dd-yyyy  HH:mm:ss");
					   }
					   catch
					   {
						   DateTimeTextBox.Text = "";
					   }

                       try
					   {
                           CommentTextBox.Text = myReader.GetString(7);
					   }
					   catch
					   {
                           CommentTextBox.Text = "";
					   }


                       lvGroups.Items.Clear();
                       var accessGroups = EmployeesQuery.GetEmployeeAccessGroup(Convert.ToUInt32(myReader["ID"]));
                       accessGroups.ForEach(group =>
                       {
                           ListViewItem item = new ListViewItem(group.Name);
                           item.Tag = group;
                           lvGroups.Items.Add(item);
                       });
                   }
			    }
                 
			    myReader.Close();

				if (listView.Items.Count > 0)
					listView.Items[0].Selected = true;
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
				case 1: //tagID
				
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

        private void lvGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvGroups.SelectedIndices.Count > 0)
                lvGroups.ContextMenu = ctxGroupsSel;
            else
                lvGroups.ContextMenu = ctxGroupsNew;
        }

        private void lvGroups_ItemActivate(object sender, EventArgs e)
        {
            using (GroupPolicy form = new GroupPolicy())
            {
                DialogResult result = form.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    hasGroupChanges = true;
                    int index = lvGroups.SelectedItems[0].Index;
                    lvGroups.SelectedItems[0].Remove();
                    Group group = form.SelectedGroup;
                    ListViewItem item = new ListViewItem(group.Name);
                    item.Tag = group;
                    lvGroups.Items.Insert(index, item);
                }
            }
        }
	}
}
