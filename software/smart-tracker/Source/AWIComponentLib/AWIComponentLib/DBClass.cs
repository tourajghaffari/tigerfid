#region changes
//    v5.0 Apr 25, 2007 - added code so if Connect() failed it will set event for broken connection.
#endregion

using System;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;


namespace AWIComponentLib.Database
{
	public enum status {broken, open, close};

	#region delegates 
	public delegate void NotifyDBConnectionStatus(status s, OdbcConnection connection);
	#endregion

	

	public class OdbcDbClass : AWIComponentLib.Database.IDBClass
	{
		private string connectionString;
		private OdbcConnection m_connection;
		private bool connected;

		#region events 
        public static event NotifyDBConnectionStatus NotifyDBConnectionStatusHandler;
		#endregion

		public OdbcDbClass()
		{
			connected = false;
		}

		public bool Connect(string server, string port, string database, string user, string pwd)
		{
           return(false);
		}


        public bool Connect(string connectionStr, bool createDatabase)
        {
            connectionString = connectionStr;
            m_connection = new OdbcConnection(connectionStr);
            //m_connection.set_ConnectionTimeout(2); //sec default=15
            m_connection.StateChange += new StateChangeEventHandler(this.OnStateChangeEventHandler);
            try
            {
                m_connection.Open();
            }
            catch (System.Data.Odbc.OdbcException ex)
            {
                if (createDatabase &&
                    ex.Message.ToLowerInvariant().Contains("unknown database '"))
                {
                    m_connection.Close();
                    m_connection.Dispose();

                    int ix_db = connectionStr.ToLowerInvariant().IndexOf("database=");
                    int len_db = connectionStr.IndexOf(';', ix_db);

                    string name_db = connectionStr.Substring(ix_db + 9, len_db - ix_db - 9);

                    if (MessageBox.Show(string.Format("Database '{0}' does not exist. Would you like to create it?", name_db), "Smart Tracker", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                    {
                        connected = false;
                        if (NotifyDBConnectionStatusHandler != null)
                            NotifyDBConnectionStatusHandler(status.broken, null);

                        return (false);
                    }

                    string conn_db = connectionStr.Substring(0, ix_db) + /*"Database=information_schema;" +*/ connectionStr.Substring(len_db + 1);
                    string cmd_str = string.Format("CREATE DATABASE IF NOT EXISTS {0}", name_db);
                    using (var con = new OdbcConnection(conn_db))
                    using (var cmd = new OdbcCommand(cmd_str, con))
                    {
                        con.Open();

                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (OdbcException)
                        {
                            MessageBox.Show(string.Format("Failed to create database '{0}'. Open MySQL command line application and execute the command:" + Environment.NewLine + "CREATE DATABASE {0};", name_db), "Smart Tracker", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            connected = false;
                            if (NotifyDBConnectionStatusHandler != null)
                                NotifyDBConnectionStatusHandler(status.broken, null);

                            return (false);
                        }
                    }

                    return Connect(connectionStr);
                }
                else if (ex.Message.ToLowerInvariant().Contains("lost connection") ||
                         ex.Message.Contains("(10061)")) //can not connect
                {
                    connected = false;
                    if (NotifyDBConnectionStatusHandler != null)
                        NotifyDBConnectionStatusHandler(status.broken, null);
                }

                m_connection.Close();
                m_connection.Dispose();

                return (false);
            }
            catch (Exception)
            {
                m_connection.Close();
                m_connection.Dispose();

                return (false);
            }

            return (true);
        }


        public bool Connect(string connectionStr)
		{
           connectionString = connectionStr;
		   m_connection = new OdbcConnection(connectionStr);
		   //m_connection.set_ConnectionTimeout(2); //sec default=15
		   m_connection.StateChange += new StateChangeEventHandler(this.OnStateChangeEventHandler);
			try 
			{
				m_connection.Open();
			}
			catch(Exception ex)
			{
				if ((ex.Message.IndexOf("Lost connection") >= 0) || 
					(ex.Message.IndexOf("(10061)") >= 0)) //can not connect
				{
					connected = false;
					if (NotifyDBConnectionStatusHandler != null)
						NotifyDBConnectionStatusHandler(status.broken, null); 
				}
                
				m_connection.Close();
				m_connection.Dispose();

               return(false);
			}

			return (true);
		}

		public void Close()
		{
           m_connection.Close();
		}

		public bool IsConnected()
		{
           return(connected);
		}

		public void Dispose()
		{

		}

		public void OnStateChangeEventHandler (object sender, StateChangeEventArgs e)
		{
			if (e.CurrentState == ConnectionState.Broken)
			{
				connected = false;
				if (NotifyDBConnectionStatusHandler != null)
					NotifyDBConnectionStatusHandler(status.broken, null);
				Console.WriteLine("DB Connection Lost");
			}
			else if (e.CurrentState == ConnectionState.Open)
			{
				connected = true;
				if (NotifyDBConnectionStatusHandler != null)
					NotifyDBConnectionStatusHandler(status.open, m_connection);
				Console.WriteLine("DB Connected");
			}
			if (e.CurrentState == ConnectionState.Closed)
			{
				connected = false;
				if (NotifyDBConnectionStatusHandler != null)
					NotifyDBConnectionStatusHandler(status.close, null);
				Console.WriteLine("DB Connection Closed");
			}
		}
	}
}
