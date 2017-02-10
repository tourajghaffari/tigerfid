using System;
using System.IO;
using System.Globalization;

namespace AWIComponentLib.Utility.Logging
{	
	public class LogClass
	{
		#region vars
		public static bool logging;
		private bool fileCreated;
		private string fileName;
		private string path;
		private long maxSize;
		private FileStream wfile;
		private StreamWriter writer;
		#endregion

		#region constructor
		public LogClass()
		{
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

			logging = false;
			fileCreated = false;
			fileName = "";
			path = "";
			maxSize = 102400000;  //bytes - 100,000k default 100Meg
		}
		#endregion

		#region destructor
		~LogClass()
		{
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

			if (logging)
			{
				try
				{
					if (WriteLine(DateTime.Now.ToString() + "  Logging Stopped.") < 0)
						Console.WriteLine("LOG ERROR: Failed to write to Log file.");
				}
				catch (Exception)
				{
					try
					{
						Console.WriteLine("LOG ERROR: Failed to write to Log file.");
					}
					catch {}
				}

				logging = false;
				try 
				{
					writer.Close();
				}
				catch {}
			}

			if (fileCreated)
			{
				fileCreated = false;
				try
				{
					wfile.Close();
				}
				catch {};

			}	
		}
		#endregion

		#region StartLogging()
		public void StartLogging()
		{
			logging = true;

			try
			{
				if (WriteLine(DateTime.Now.ToString() + "  Logging Started.") < 0)
					Console.WriteLine("LOG ERROR: Failed to write to Log file.");
			}
			catch
			{
                Console.WriteLine("LOG ERROR: Failed to write to Log file.");
			}
		}
		#endregion

		#region StopLogging()
		public void StopLogging()
		{
			if (logging)
			{
				try
				{
					if (WriteLine(DateTime.Now.ToString() + "  Logging Stopped.") < 0)
						Console.WriteLine("LOG ERROR: Failed to write to Log file.");
				}
				catch 
				{
                    Console.WriteLine("LOG ERROR: Failed to write to Log file.");
				}

				logging = false;
				try
				{
					writer.Close();
				}
				catch {}
			}
            
			if (fileCreated)
			{
				fileCreated = false;
				try
				{
					wfile.Close();
				}
				catch {}
			}

			
		}
		#endregion

		#region CreateLogFile()
		public int CreateLogFile(string fName, string fPath, short type)
		{
			string fullFileName = fPath + fName;
			try
			{
				if (type == 1)  //new
				{
					//check if the file exits, notify application
					if (File.Exists(fullFileName))
						return(-2);

					wfile = new FileStream (fullFileName, FileMode.CreateNew, FileAccess.Write);
				}
				else if (type == 2)  //append
					wfile = new FileStream (fullFileName, FileMode.Append, FileAccess.Write);
				else if (type == 3)  //new overwrite
					wfile = new FileStream (fullFileName, FileMode.Create, FileAccess.Write);

				writer = new StreamWriter(wfile);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
                return(-1);
			}

			writer.AutoFlush = true;
			fileName = fName;
			path = fPath;

			fileCreated = true;

			return (0);
		}
		#endregion

		#region WriteLine()
		public int WriteLine(string data)
		{
			if (logging && wfile.CanWrite)
			{
				if (data.Length == 0)
					return (-1);

                if (wfile.Length < maxSize)
                    writer.WriteLine(data);

				return(0);
			}
			else
               return (-1);
		}
		#endregion

		#region ReadLine()
		public string ReadLine()
		{
			return ("");
		}
		#endregion

		#region Flush()
		public void Flush()
		{
			writer.Flush();
			writer.Close();
			wfile.Flush();
			wfile.Close();
		}
		#endregion

		#region ClearFile()
		public void ClearFile()
		{
			wfile.SetLength(0);
		}
		#endregion

		#region GetPath()
		public string GetPath()
		{
			return (path);
		}
		#endregion

		#region GetFileName()
		public string GetFileName()
		{
			return (fileName);
		}
		#endregion

		#region GetFileSize()
		public long GetFileSize()
		{
			return (wfile.Length);
		}
		#endregion

		#region GetMaxSize()
		public long GetMaxSize()
		{
			return (maxSize);
		}
		#endregion

		#region SetMaxSize(int max)
		public void GetMaxSize(long max)
		{
			maxSize = max;
		}
		#endregion
	}
}
