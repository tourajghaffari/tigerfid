using System;

namespace AWIComponentLib.Database
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public interface IDBClass : IDisposable
	{
		bool Connect(string server, string port, string database, string user, string pwd);
		bool Connect(string connectionString);
		void Close();
		bool IsConnected();

	}
}
