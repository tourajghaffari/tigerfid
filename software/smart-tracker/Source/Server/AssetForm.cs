using System;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using System.Data.Odbc;
using AWIComponentLib.Database;
using System.Data;
using System.Text;
using System.IO;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for AssetForm.
	/// </summary>
	public class AssetForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton NewToolBarButton;
		private System.Windows.Forms.ToolBarButton EditToolBarButton;
		private System.Windows.Forms.ToolBarButton CancelToolBarButton;
		private System.Windows.Forms.ToolBarButton SaveToolBarButton;
		private System.Windows.Forms.ToolBarButton DeleteToolBarButton;
		private System.Windows.Forms.ToolBarButton RefreshToolBarButton;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label DBStatusLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox NameTextBox;
		private System.Windows.Forms.TextBox TagIDTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button OpenImageButton;
		private System.Windows.Forms.Button PreButton;
		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Button LastButton;
		private System.Windows.Forms.Button FirstButton;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.PictureBox AstPictureBox;
		private System.Windows.Forms.CheckBox MobileCheckBox;
		private System.Windows.Forms.TextBox ModelTextBox;
		private System.Windows.Forms.TextBox SKUTextBox;
		private System.Windows.Forms.TextBox DateTimeTextBox;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.TextBox DescriptionTextBox;
		private System.Windows.Forms.ListView listView;
		private System.ComponentModel.IContainer components;
		private OdbcDbClass odbcDB = new OdbcDbClass();
        public AWIHelperClass awiHelper;
		private Bitmap astImage ;
		private bool newRec = false;
		private string oldAssetID = "";
		private string m_imageName = "";
		private System.Windows.Forms.OpenFileDialog OpenFileDlg;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Label label8;
		private OdbcConnection m_connection = null;
		private System.Timers.Timer timer1;
		private MainForm mForm;
		

		public AssetForm()
		{
			InitializeComponent();

			//changes made to support MYSQL Server v5.0 and later
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";
		}

		private void DBConnectionStatusHandler(status stat, OdbcConnection connect)
		{
			if (stat == status.open)
			{
				m_connection = connect;
				RefreshListView();
				//MainForm.reconnectCounter = -1;
				//timer1.Enabled = false;
				//DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
				//DBStatusLabel.Text = "Connected";
				//toolBar1.Enabled = true;
				
			}
			else if (stat == status.broken)
			{
				m_connection = null;
				
				DBStatusLabel.ForeColor = System.Drawing.Color.Red;
				DBStatusLabel.Text = "Disconnected";
				toolBar1.Enabled = false;
			}
			else if (stat == status.close)
			{
				m_connection = null;

				DBStatusLabel.ForeColor = System.Drawing.Color.Red;
				DBStatusLabel.Text = "Disconnected";
				toolBar1.Enabled = false;
			}
		}

		public AssetForm(MainForm form)
		{
			mForm = form;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			InitializeComponent();
			OdbcDbClass.NotifyDBConnectionStatusHandler += new NotifyDBConnectionStatus(DBConnectionStatusHandler);
            awiHelper = new AWIHelperClass(form);

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

			RefreshListView();

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
            this.DBStatusLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.TagIDTextBox = new System.Windows.Forms.TextBox();
            this.AstPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OpenImageButton = new System.Windows.Forms.Button();
            this.MobileCheckBox = new System.Windows.Forms.CheckBox();
            this.PreButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.LastButton = new System.Windows.Forms.Button();
            this.FirstButton = new System.Windows.Forms.Button();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ModelTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SKUTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DateTimeTextBox = new System.Windows.Forms.TextBox();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.OpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.timer1 = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.AstPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolBar1
            // 
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
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(576, 32);
            this.toolBar1.TabIndex = 42;
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
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(52, 2);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // DBStatusLabel
            // 
            this.DBStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DBStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.DBStatusLabel.Location = new System.Drawing.Point(110, 236);
            this.DBStatusLabel.Name = "DBStatusLabel";
            this.DBStatusLabel.Size = new System.Drawing.Size(90, 18);
            this.DBStatusLabel.TabIndex = 53;
            this.DBStatusLabel.Text = "Disconnected";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 18);
            this.label2.TabIndex = 52;
            this.label2.Text = "Database Status: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // NameTextBox
            // 
            this.NameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTextBox.Location = new System.Drawing.Point(110, 84);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.ReadOnly = true;
            this.NameTextBox.Size = new System.Drawing.Size(246, 21);
            this.NameTextBox.TabIndex = 46;
            // 
            // TagIDTextBox
            // 
            this.TagIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TagIDTextBox.Location = new System.Drawing.Point(110, 54);
            this.TagIDTextBox.Name = "TagIDTextBox";
            this.TagIDTextBox.ReadOnly = true;
            this.TagIDTextBox.Size = new System.Drawing.Size(120, 21);
            this.TagIDTextBox.TabIndex = 45;
            // 
            // AstPictureBox
            // 
            this.AstPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AstPictureBox.Location = new System.Drawing.Point(424, 84);
            this.AstPictureBox.Name = "AstPictureBox";
            this.AstPictureBox.Size = new System.Drawing.Size(136, 124);
            this.AstPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AstPictureBox.TabIndex = 51;
            this.AstPictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 18);
            this.label1.TabIndex = 44;
            this.label1.Text = "Asset Tag ID: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 18);
            this.label3.TabIndex = 48;
            this.label3.Text = "Asset Name : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // OpenImageButton
            // 
            this.OpenImageButton.Enabled = false;
            this.OpenImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenImageButton.ForeColor = System.Drawing.Color.Black;
            this.OpenImageButton.Location = new System.Drawing.Point(466, 210);
            this.OpenImageButton.Name = "OpenImageButton";
            this.OpenImageButton.Size = new System.Drawing.Size(54, 23);
            this.OpenImageButton.TabIndex = 47;
            this.OpenImageButton.Text = "Open";
            this.OpenImageButton.Click += new System.EventHandler(this.OpenImageButton_Click);
            // 
            // MobileCheckBox
            // 
            this.MobileCheckBox.Enabled = false;
            this.MobileCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MobileCheckBox.Location = new System.Drawing.Point(252, 52);
            this.MobileCheckBox.Name = "MobileCheckBox";
            this.MobileCheckBox.Size = new System.Drawing.Size(14, 24);
            this.MobileCheckBox.TabIndex = 54;
            // 
            // PreButton
            // 
            this.PreButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreButton.ForeColor = System.Drawing.Color.Black;
            this.PreButton.Location = new System.Drawing.Point(362, 518);
            this.PreButton.Name = "PreButton";
            this.PreButton.Size = new System.Drawing.Size(75, 27);
            this.PreButton.TabIndex = 59;
            this.PreButton.Text = "Previous";
            this.PreButton.Click += new System.EventHandler(this.PreButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextButton.ForeColor = System.Drawing.Color.Black;
            this.NextButton.Location = new System.Drawing.Point(286, 518);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 27);
            this.NextButton.TabIndex = 58;
            this.NextButton.Text = "Next";
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // LastButton
            // 
            this.LastButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastButton.ForeColor = System.Drawing.Color.Black;
            this.LastButton.Location = new System.Drawing.Point(210, 518);
            this.LastButton.Name = "LastButton";
            this.LastButton.Size = new System.Drawing.Size(75, 27);
            this.LastButton.TabIndex = 57;
            this.LastButton.Text = "Last";
            this.LastButton.Click += new System.EventHandler(this.LastButton_Click);
            // 
            // FirstButton
            // 
            this.FirstButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstButton.ForeColor = System.Drawing.Color.Black;
            this.FirstButton.Location = new System.Drawing.Point(134, 518);
            this.FirstButton.Name = "FirstButton";
            this.FirstButton.Size = new System.Drawing.Size(75, 27);
            this.FirstButton.TabIndex = 56;
            this.FirstButton.Text = "First";
            this.FirstButton.Click += new System.EventHandler(this.FirstButton_Click);
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionTextBox.Location = new System.Drawing.Point(110, 174);
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.ReadOnly = true;
            this.DescriptionTextBox.Size = new System.Drawing.Size(244, 21);
            this.DescriptionTextBox.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(8, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 18);
            this.label4.TabIndex = 61;
            this.label4.Text = "Date && Time: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ModelTextBox
            // 
            this.ModelTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModelTextBox.Location = new System.Drawing.Point(110, 114);
            this.ModelTextBox.Name = "ModelTextBox";
            this.ModelTextBox.ReadOnly = true;
            this.ModelTextBox.Size = new System.Drawing.Size(118, 21);
            this.ModelTextBox.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(8, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 18);
            this.label5.TabIndex = 63;
            this.label5.Text = "Model #:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SKUTextBox
            // 
            this.SKUTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SKUTextBox.Location = new System.Drawing.Point(110, 144);
            this.SKUTextBox.Name = "SKUTextBox";
            this.SKUTextBox.ReadOnly = true;
            this.SKUTextBox.Size = new System.Drawing.Size(120, 21);
            this.SKUTextBox.TabIndex = 65;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(8, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 18);
            this.label6.TabIndex = 64;
            this.label6.Text = "SKU #: ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(8, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 18);
            this.label7.TabIndex = 66;
            this.label7.Text = "Description:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // DateTimeTextBox
            // 
            this.DateTimeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimeTextBox.Location = new System.Drawing.Point(110, 204);
            this.DateTimeTextBox.Name = "DateTimeTextBox";
            this.DateTimeTextBox.ReadOnly = true;
            this.DateTimeTextBox.Size = new System.Drawing.Size(126, 21);
            this.DateTimeTextBox.TabIndex = 67;
            // 
            // listView
            // 
            this.listView.BackColor = System.Drawing.SystemColors.Info;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader7,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(8, 278);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(558, 232);
            this.listView.TabIndex = 68;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.Click += new System.EventHandler(this.listView_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Mobile";
            this.columnHeader7.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Model #";
            this.columnHeader3.Width = 90;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "SKU #";
            this.columnHeader4.Width = 90;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Description";
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Date & Time";
            this.columnHeader6.Width = 130;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(268, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 18);
            this.label8.TabIndex = 69;
            this.label8.Text = "Mobile";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // AssetForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(576, 551);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.DateTimeTextBox);
            this.Controls.Add(this.SKUTextBox);
            this.Controls.Add(this.ModelTextBox);
            this.Controls.Add(this.DescriptionTextBox);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.TagIDTextBox);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PreButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.LastButton);
            this.Controls.Add(this.FirstButton);
            this.Controls.Add(this.MobileCheckBox);
            this.Controls.Add(this.DBStatusLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AstPictureBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OpenImageButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Asset Tag Registration Form";
            this.Load += new System.EventHandler(this.AssetForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AstPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void AssetForm_Load(object sender, System.EventArgs e)
		{
		
		}

		private void ShowErrorMessage(string msg)
		{
            MessageBox.Show(this, msg, "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (m_connection == null)  //added Dec 06, 06
				return;

            //changes made to support MYSQL Server v5.0 and later
            CultureInfo ci = new CultureInfo("sv-SE", true);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            ci.DateTimeFormat.DateSeparator = "-";

			if (e.Button.Text == "New")
			{
                MobileCheckBox.Checked = false;
				MobileCheckBox.Enabled = true;
				TagIDTextBox.Text = "";
				NameTextBox.Text = "";
				ModelTextBox.Text = "";
				SKUTextBox.Text = "";
				DescriptionTextBox.Text = "";
				DateTimeTextBox.Text = "";
				DateTimeTextBox.Text = "";
				AstPictureBox.Image = null;
				OpenImageButton.Enabled = true;

				TagIDTextBox.Focus();
				TagIDTextBox.ReadOnly = false;
				NameTextBox.ReadOnly = false;
				ModelTextBox.ReadOnly = false;
				SKUTextBox.ReadOnly = false;
				DescriptionTextBox.ReadOnly = false;
				
				newRec = true;
			}
			else if (e.Button.Text == "Edit")
			{
				TagIDTextBox.Focus();
				MobileCheckBox.Enabled = true;
				oldAssetID = TagIDTextBox.Text;
				TagIDTextBox.ReadOnly = false;
				NameTextBox.ReadOnly = false;
				ModelTextBox.ReadOnly = false;
				SKUTextBox.ReadOnly = false;
				DescriptionTextBox.ReadOnly = false;

				OpenImageButton.Enabled = true;
				newRec = false;
			}
			else if (e.Button.Text == "Cancel")
			{
				TagIDTextBox.ReadOnly = true;
				NameTextBox.ReadOnly = true;
				ModelTextBox.ReadOnly = true;
				SKUTextBox.ReadOnly = true;
				DescriptionTextBox.ReadOnly = true;
				DateTimeTextBox.ReadOnly = true;
				MobileCheckBox.Enabled = false;

				OpenImageButton.Enabled = false;

				if(this.listView.SelectedIndices.Count <= 0)
				{
					//MobileCheckBox.Checked = false;
					TagIDTextBox.Text = "";
					NameTextBox.Text = "";
					ModelTextBox.Text = "";
					SKUTextBox.Text = "";
					DescriptionTextBox.Text = "";
					DateTimeTextBox.Text = "";
					OpenImageButton.Enabled = true;

					AstPictureBox.Image = null;
					return;
				}

				ListViewItem selItem = listView.SelectedItems[0];
				TagIDTextBox.Text = selItem.SubItems[0].Text;
                if (selItem.SubItems[1].Text == "True")
					MobileCheckBox.Checked = true;
				else
                    MobileCheckBox.Checked = false;
				NameTextBox.Text = selItem.SubItems[2].Text;
				ModelTextBox.Text = selItem.SubItems[3].Text;
				SKUTextBox.Text = selItem.SubItems[4].Text;
				DescriptionTextBox.Text = selItem.SubItems[5].Text;
				DateTimeTextBox.Text = selItem.SubItems[6].Text;

				if (TagIDTextBox.Text.Length > 0)
                    AstPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "asset");  //GetTagImage(TagIDTextBox.Text, m_connection);
				
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
				MobileCheckBox.Enabled = false;
				NameTextBox.ReadOnly = true;
				ModelTextBox.ReadOnly = true;
				SKUTextBox.ReadOnly = true;
				DescriptionTextBox.ReadOnly = true;
				DateTimeTextBox.ReadOnly = true;

				OpenImageButton.Enabled = true;

				int FileSize;
				byte[] rawData = null;
				//FileStream fs;
				//string SQL = "";

				if (AstPictureBox.Image != null)
				{
					MemoryStream stream = new MemoryStream();
					AstPictureBox.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
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

                if (!awiHelper.SaveAssetTag(newRec, TagIDTextBox.Text, MobileCheckBox.Checked, NameTextBox.Text, ModelTextBox.Text, SKUTextBox.Text, DescriptionTextBox.Text, DateTime.Now, rawData, oldAssetID))  //oldEmployeeID))
					return;
			
				if (!newRec)
				{
					ListViewItem selItem = listView.SelectedItems[0];
					int index = selItem.Index;
					if (index >= 0)
						selItem.Remove();	
				}

				ListViewItem listItem = new ListViewItem(TagIDTextBox.Text);
				if (MobileCheckBox.Checked)
				   listItem.SubItems.Add("True");
				else
                   listItem.SubItems.Add("False");
				listItem.SubItems.Add(NameTextBox.Text);
				listItem.SubItems.Add(ModelTextBox.Text);
				listItem.SubItems.Add(SKUTextBox.Text);
				listItem.SubItems.Add(DescriptionTextBox.Text);
				listItem.SubItems.Add(timeStr);
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
				NameTextBox.ReadOnly = true;
				ModelTextBox.ReadOnly = true;
				SKUTextBox.ReadOnly = true;
				MobileCheckBox.Enabled = false;
				DescriptionTextBox.ReadOnly = true;

				OpenImageButton.Enabled = false;
				//SaveButton.Enabled = false;

				StringBuilder sql = new StringBuilder();
				sql.Append("DELETE FROM asset WHERE ID = ");         
				sql.AppendFormat("'{0}'", TagIDTextBox.Text);
			     
				OdbcCommand myCommand = new OdbcCommand(sql.ToString(), m_connection);
				
				/*try
				{
					myCommand.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					ShowErrorMessage(ex.Message);
					return;
				}*/

				lock (m_connection)
				{
					try
					{
						myCommand.ExecuteNonQuery();
						//MainForm.reconnectCounter = -1;
						//timer1.Enabled = false;
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
						
							//if process of reconnecting started skip
							if (MainForm.reconnectCounter < 0)
							{
								MainForm.reconnectCounter = 0;
								//timer1.Enabled = true;
							} 

							MainForm.dbDisconnectedFlag = true;
						}
						return;
					}//catch .. try
				}//lock

                awiHelper.DeleteTag(TagIDTextBox.Text, "AST");
            
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
					NameTextBox.Text = "";
					ModelTextBox.Text = "";
					SKUTextBox.Text = "";
					DescriptionTextBox.Text = "";
					DateTimeTextBox.Text = "";
					
					DateTimeTextBox.Text = "";
					AstPictureBox.Image = null;
				}
			}
			else if (e.Button.Text == "Refresh")
			{
				listView.Items.Clear();
				TagIDTextBox.Text = "";
				NameTextBox.Text = "";
				ModelTextBox.Text = "";
				SKUTextBox.Text = "";
				DescriptionTextBox.Text = "";
				DateTimeTextBox.Text = "";
				AstPictureBox.Image = null;

				MobileCheckBox.Enabled = false;
				TagIDTextBox.ReadOnly = true;
				NameTextBox.ReadOnly = true;
				ModelTextBox.ReadOnly = true;
				SKUTextBox.ReadOnly = true;
				DescriptionTextBox.ReadOnly = true;
				
				RefreshListView();

				/*newRec = false;

				string mySelectQuery = "SELECT ID, Mobile, Name, Model, SKU, Description, Timestamp FROM asset"; // WHERE Type = 'AST' AND AccType = '1'";
				OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection); 

				OdbcDataReader myReader = null;
				try
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
				}

				bool firstRec = true;
				int myRec = 0;
				while (myReader.Read())
				{
					myRec += 1;
					//if (myRec > MAX_DEMO_RECORDS)
						//break;
					ListViewItem listItem = new ListViewItem(myReader.GetString(0));  //ID
                    if (myReader.GetBoolean(1))
					   listItem.SubItems.Add("True");  //mobile
					else
                       listItem.SubItems.Add("False");  //mobile
					listItem.SubItems.Add(myReader.GetString(2));  //name
					listItem.SubItems.Add(myReader.GetString(3));  //model
					listItem.SubItems.Add(myReader.GetString(4));  //sku
					listItem.SubItems.Add(myReader.GetString(5));  //description
					listItem.SubItems.Add(myReader.GetString(6));  //dateTime

					listView.Items.Add(listItem);
					if (firstRec)
					{
						//listItem.Selected = true;
						firstRec = false;
						TagIDTextBox.Text = myReader.GetString(0);  //id
						if (myReader.GetBoolean(1))
							listItem.SubItems.Add("True");  //mobile
						else
							listItem.SubItems.Add("False");  //mobile
						NameTextBox.Text = myReader.GetString(2);  //name
						ModelTextBox.Text = myReader.GetString(3); //model
						SKUTextBox.Text = myReader.GetString(4);   //sku
						DescriptionTextBox.Text = myReader.GetString(5);  //description
						DateTimeTextBox.Text = myReader.GetString(6);    //datetime 
					}
				}//while
                 
				myReader.Close();

				if (listView.Items.Count > 0)
					listView.Items[0].Selected = true;*/
			}
		}

		private void RefreshListView()
		{
			bool firstRec = true;
			int myRec = 0;

			if (m_connection == null)  //added Dec 06, 06
				return;

			lock (MainForm.m_connection)
			{
				string mySelectQuery = "SELECT ID, Mobile, Name, Model, SKU, Description, Timestamp FROM asset"; // WHERE Type = 'AST' AND AccType = '1'";
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
					//MainForm.reconnectCounter = -1;
					//timer1.Enabled = false;
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
						
						//if process of reconnecting started skip
						if (MainForm.reconnectCounter < 0)
						{
							MainForm.reconnectCounter = 0;
							//timer1.Enabled = true;
						}                              
					}
					if (myReader != null)
					{
						if (!myReader.IsClosed)
							myReader.Close();
					}
					return;

				}//try .. catch
				//}//lock

				DBStatusLabel.ForeColor = System.Drawing.Color.Green;
				DBStatusLabel.Text = "Connected";
				toolBar1.Enabled = true;

				while (myReader.Read())
				{
					myRec += 1;
					//if (myRec > MAX_DEMO_RECORDS)
					//break;
					ListViewItem listItem = new ListViewItem(myReader.GetString(0));  //ID
					if (myReader.GetBoolean(1))
						listItem.SubItems.Add("True");  //mobile
					else
						listItem.SubItems.Add("False");  //mobile
					listItem.SubItems.Add(myReader.GetString(2));  //name
					listItem.SubItems.Add(myReader.GetString(3));  //model
					listItem.SubItems.Add(myReader.GetString(4));  //sku
					listItem.SubItems.Add(myReader.GetString(5));  //description
					listItem.SubItems.Add(myReader.GetString(6));  //dateTime

					listView.Items.Add(listItem);
					if (firstRec)
					{
						//listItem.Selected = true;
						firstRec = false;
						TagIDTextBox.Text = myReader.GetString(0);  //id
						if (myReader.GetBoolean(1))
							listItem.SubItems.Add("True");  //mobile
						else
							listItem.SubItems.Add("False");  //mobile
						NameTextBox.Text = myReader.GetString(2);  //name
						ModelTextBox.Text = myReader.GetString(3); //model
						SKUTextBox.Text = myReader.GetString(4);   //sku
						DescriptionTextBox.Text = myReader.GetString(5);  //description
                        try
                        {
                            DateTimeTextBox.Text = myReader.GetDateTime(6).ToString("MM-dd-yyyy  HH:mm:ss");  //dateTime 
                        }
                        catch (System.InvalidCastException)
                        {

                        }
					}
				}//while
                 
				myReader.Close();

				if (listView.Items.Count > 0)
					listView.Items[0].Selected = true;

				if (TagIDTextBox.Text.Length > 0)
                    AstPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "asset"); 
			}//lock m_connection
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
				if (astImage != null)
				{
					astImage.Dispose();
				}

				// Stretches the image to fit the pictureBox.
				AstPictureBox.SizeMode = PictureBoxSizeMode.StretchImage ;
				astImage = new Bitmap(m_imageName);
				AstPictureBox.Image = (Image)astImage ;
			 
			}
		}

		private void listView_Click(object sender, System.EventArgs e)
		{
			ListViewItem selItem = listView.SelectedItems[0];
			int index = selItem.Index;
			TagIDTextBox.Text = selItem.SubItems[0].Text;  //ID
			if (selItem.SubItems[1].Text == "True")      //mobile
				MobileCheckBox.Checked = true;
			else
                MobileCheckBox.Checked = false;
			NameTextBox.Text = selItem.SubItems[2].Text;      //name
			ModelTextBox.Text = selItem.SubItems[3].Text;     //model
			SKUTextBox.Text = selItem.SubItems[4].Text;       //sku
			DescriptionTextBox.Text = selItem.SubItems[5].Text;  //description
			DateTimeTextBox.Text = selItem.SubItems[6].Text;  //time

			if (TagIDTextBox.Text.Length > 0)
                AstPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "asset");
		}

		private void listView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.listView.SelectedIndices.Count <= 0)
				return;

			ListViewItem selItem = listView.SelectedItems[0];
			TagIDTextBox.Text = selItem.SubItems[0].Text;
			if (selItem.SubItems[1].Text == "True")      //mobile
				MobileCheckBox.Checked = true;
			else
				MobileCheckBox.Checked = false;
			NameTextBox.Text = selItem.SubItems[2].Text;      //name
			ModelTextBox.Text = selItem.SubItems[3].Text;     //model
			SKUTextBox.Text = selItem.SubItems[4].Text;       //sku
			DescriptionTextBox.Text = selItem.SubItems[5].Text;  //description
			DateTimeTextBox.Text = selItem.SubItems[6].Text;  //time

			if (TagIDTextBox.Text.Length > 0)
                AstPictureBox.Image = awiHelper.GetTagImage(TagIDTextBox.Text, "asset"); //GetTagImage(TagIDTextBox.Text, m_connection);
			
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

		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			/*if (MainForm.reconnectCounter > 3)
			{
				timer1.Enabled = false;
				//DBStatusBarPanel.Text = "DB Server : MYSQL               Database : Disconnected";
				MessageBox.Show(this, "Connection to DB Server lost and reconnect failed.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MainForm.reconnectCounter += 1;
				//lock (MainForm.m_connection)  //dec06-06
				{
					if(mForm.ReconnectToDBServer())
					{
						timer1.Enabled = false;
						DBStatusLabel.ForeColor = System.Drawing.Color.Blue;
						DBStatusLabel.Text = "Connected";
						toolBar1.Enabled = true;
					}
				}
			}*/
		}
	}
}
