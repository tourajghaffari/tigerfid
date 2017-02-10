using System;
using System.Data;
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using AWIComponentLib.Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for ReportForm.
	/// </summary>
	public class ReportForm : System.Windows.Forms.Form
	{
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer;
		private System.ComponentModel.Container components = null;
		private ConnectionInfo m_connectionInfo = new ConnectionInfo();
		private TableLogOnInfos tableLogonInfos = new TableLogOnInfos();
		private TableLogOnInfo logonInfo = new TableLogOnInfo();
        private ReportDocument myReport;
		private MainForm mForm = new MainForm(0);
		private OdbcConnection m_connection = null;
		private string rptFileName;
		private string id;
		private DateTime fromDate;
		private DateTime toDate;
        public bool alarm;
        private AlarmsReport AlarmsReport1;
		public bool failed;

		public ReportForm()
		{
			InitializeComponent();
		}

		public ReportForm(MainForm form, string  fileName, DateTime from, DateTime to)
		{
			//changes made to support MYSQL Server v5.0 and later
			mForm = form;
			rptFileName = fileName;
			InitializeComponent();
			alarm = false;
			failed = false;

            crystalReportViewer.SetProductLocale("en");

            CultureInfo ci = new CultureInfo("en"/*"sv-SE"*/, true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

			DataSet myData = new DataSet();
			OdbcDataAdapter myAdapter = new OdbcDataAdapter();

			string mySelectQuery = "";
			StringBuilder mySelectStrQuery = new StringBuilder();

			if (rptFileName == "EmployeesReport.rpt")
			{
				mySelectQuery = "SELECT ID, FirstName, LastName, Department FROM employees";
                //myReport = EmployeesReport1;
			}
			else if (rptFileName == "ZonesReport.rpt")
			{
				mySelectQuery = "SELECT ID, Location, ReaderID, FieldGenID, Status, Time FROM zones";
                //myReport = ZonesReport1;
			}
			else if (rptFileName == "ZoneHistoryReport.rpt")
			{
				ReportQueryForm reportQForm = new ReportQueryForm(this);
				reportQForm.IDLabel.Text = "Zone ID: ";
				if (reportQForm.ShowDialog(this) == DialogResult.OK)
				{
					id = reportQForm.id; 
					fromDate = reportQForm.fromDate;
					toDate = reportQForm.toDate;
				}
				else
				{
					failed = true;
					return;
				}
				reportQForm.Dispose();

				mySelectStrQuery.Append("SELECT ZoneID, TagID, Name, Department, Location, Event, Time FROM traffic WHERE ZoneID = ");
				mySelectStrQuery.AppendFormat("'{0}'", id);
				mySelectStrQuery.AppendFormat(" AND Type < 90");
				mySelectStrQuery.AppendFormat(" AND Time >= '{0}'", fromDate.ToString("yyyy-MM-dd"));
				if (toDate.Date == DateTime.Now.Date)
					mySelectStrQuery.AppendFormat(" AND Time <= '{0}'", toDate.ToString("yyyy-MM-dd HH:mm:ss"));
				else
				{
					DateTime date = toDate.AddDays(1);
					mySelectStrQuery.AppendFormat(" AND Time <= '{0}'", date.ToString("yyyy-MM-dd"));
				}
				mySelectStrQuery.Append(" ORDER BY Time DESC");

				mySelectQuery = mySelectStrQuery.ToString();
                //myReport = ZoneHistoryReport1;
			}
			else if (rptFileName == "EmployeeHistoryReport.rpt")
			{
				ReportQueryForm reportQForm = new ReportQueryForm(this);
				reportQForm.IDLabel.Text = "Tag ID: ";
				if (reportQForm.ShowDialog(this) == DialogResult.OK)
				{
					id = reportQForm.id; 
					fromDate = reportQForm.fromDate;
					toDate = reportQForm.toDate;
				}
				else
				{
					failed = true;
					return;
				}
				reportQForm.Dispose();

				mySelectStrQuery.Append("SELECT TagID, Name, Time, Location FROM traffic WHERE TagID = ");
				mySelectStrQuery.AppendFormat("'{0}'", id);
				mySelectStrQuery.AppendFormat(" AND Time >= '{0}'", fromDate.ToString("yyyy-MM-dd"));
				if (toDate.Date == DateTime.Now.Date)
					mySelectStrQuery.AppendFormat(" AND Time <= '{0}'", toDate.ToString("yyyy-MM-dd HH:mm:ss"));
				else
				{
					DateTime date = toDate.AddDays(1);
					mySelectStrQuery.AppendFormat(" AND Time <= '{0}'", date.ToString("yyyy-MM-dd"));
				}
				mySelectStrQuery.Append(" ORDER BY Time DESC");
               
				mySelectQuery = mySelectStrQuery.ToString();
                //myReport = EmployeeHistoryReport1;
			}
			else if (rptFileName == "AlarmsReport.rpt")
			{
				alarm = true;
				ReportQueryForm reportQForm = new ReportQueryForm(this);
				if (reportQForm.ShowDialog(this) == DialogResult.OK)
				{
					id = reportQForm.id; 
					fromDate = reportQForm.fromDate;
					toDate = reportQForm.toDate;
				}
				else
				{
					failed = true;
					return;
				}
				reportQForm.Dispose();

				mySelectStrQuery.Append("SELECT TagID, Name, Department, Location, Event, Time, Status FROM traffic WHERE (");
				mySelectStrQuery.Append(" Status = 'Invalid' OR Status = 'Alarm' OR Status = 'Offline')");
				mySelectStrQuery.AppendFormat(" AND Time >= '{0}'", fromDate.ToString("yyyy-MM-dd"));
				if (toDate.Date == DateTime.Now.Date)
					mySelectStrQuery.AppendFormat(" AND Time <= '{0}'", toDate.ToString("yyyy-MM-dd HH:mm:ss"));
				else
				{
					DateTime date = toDate.AddDays(1);
					mySelectStrQuery.AppendFormat(" AND Time <= '{0}'", date.ToString("yyyy-MM-dd"));
				}
				mySelectStrQuery.Append(" ORDER BY Time DESC");

				mySelectQuery = mySelectStrQuery.ToString();
                //myReport = AlarmsReport1;
			}
			else
				return;

            m_connection = MainForm.m_connection;

            try
            {
                OdbcCommand myCommand = new OdbcCommand(mySelectQuery, m_connection);

                myAdapter.SelectCommand = myCommand;
                try
                {
                    myAdapter.Fill(myData);
                }
                catch
                {
                    return;
                }

                if (rptFileName == "EmployeesReport.rpt")
                {
                }
                else if (rptFileName == "ZonesReport.rpt")
                {
                    //ZonesReport1;
                }
                else if (rptFileName == "ZoneHistoryReport.rpt")
                {
                    //ZoneHistoryReport1;
                }
                else if (rptFileName == "EmployeeHistoryReport.rpt")
                {
                    //EmployeeHistoryReport1;
                }
                else if (rptFileName == "AlarmsReport.rpt")
                {
                    //AlarmsReport1.SetDataSource(myData.Tables[0]);
                    myReport = new ReportDocument();
                    myReport.Load(Application.StartupPath + (Application.StartupPath.EndsWith("\\") ? "" : "\\") + "AlarmsReport.rpt");
                    crystalReportViewer.ReportSource = myReport;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Report could not be created",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
			
		}

		void AssignDataSet(ReportDocument oReport, DataSet dsData)
		{    
			DataSet dsNew = dsData.Copy();

			// Remove primary key info. CR9 does not appreciate this information!!!    
			foreach (DataTable dataTable in dsNew.Tables)    
			{        
				foreach (DataColumn dataCol in dataTable.PrimaryKey)        
				{            
					dataCol.AutoIncrement = false;        
				}        
				
				dataTable.PrimaryKey = null;    
			}     
			
			// Now assign the dataset to all tables in the main report    
			foreach (Table oTable in myReport.Database.Tables)  //oReport.Database.Tables)    
			{        
				oTable.SetDataSource(dsNew);    
			}     
			
			// Now loop through all the sections and its objects to do the same for the subreports    
			foreach (Section crSection in myReport.ReportDefinition.Sections)    // oReport.ReportDefinition.Sections)    
			{        
				// In each section we need to loop through all the reporting objects        
				foreach (ReportObject crObject in crSection.ReportObjects)        
				{            
					if (crObject.Kind == ReportObjectKind.SubreportObject)            
					{                
						SubreportObject crSubReport = (SubreportObject)crObject;                
						ReportDocument  crSubDoc = crSubReport.OpenSubreport(crSubReport.SubreportName);                 
						
						foreach (Table oTable in crSubDoc.Database.Tables)                
						{                    
							oTable.SetDataSource(dsNew);                
						}            
					}        
				}    
			}

			//crystalReportViewer.ReportSource = oReport;
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
            this.crystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewer
            // 
            this.crystalReportViewer.ActiveViewIndex = -1;
            this.crystalReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crystalReportViewer.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer.Name = "crystalReportViewer";
            this.crystalReportViewer.ShowCloseButton = false;
            this.crystalReportViewer.ShowGroupTreeButton = false;
            this.crystalReportViewer.ShowParameterPanelButton = false;
            this.crystalReportViewer.Size = new System.Drawing.Size(942, 623);
            this.crystalReportViewer.TabIndex = 0;
            this.crystalReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // ReportForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(942, 623);
            this.Controls.Add(this.crystalReportViewer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report";
            this.ResumeLayout(false);

		}
		#endregion

		private void SetLogonInfo()
		{
			try
			{
                logonInfo.ConnectionInfo.ServerName = MainForm.serverMySQL;
				logonInfo.ConnectionInfo.UserID = MainForm.user;
				logonInfo.ConnectionInfo.Password = "";
				logonInfo.ConnectionInfo.DatabaseName = MainForm.database;
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message);
			}
		}
	}
}
