using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TagDetector
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class Form2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button OpenPortButton;
		private System.Windows.Forms.TextBox ComPortTextBox;
		private System.Windows.Forms.Label label1;
		private Form1 m_form = new Form1(true);
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.Label MsgLabel;
		private uint port = 0;
	
		public Form2()
		{
			InitializeComponent();
		}

		public Form2(Form1 mForm)
		{
			InitializeComponent();
			m_form = mForm;
			//ComPortTextBox.Text = mForm.port.ToString();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.OpenPortButton = new System.Windows.Forms.Button();
			this.ComPortTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.CloseButton = new System.Windows.Forms.Button();
			this.MsgLabel = new System.Windows.Forms.Label();
			// 
			// OpenPortButton
			// 
			this.OpenPortButton.Location = new System.Drawing.Point(34, 66);
			this.OpenPortButton.Size = new System.Drawing.Size(108, 30);
			this.OpenPortButton.Text = "Open Port";
			this.OpenPortButton.Click += new System.EventHandler(this.OpenPortButton_Click);
			// 
			// ComPortTextBox
			// 
			this.ComPortTextBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular);
			this.ComPortTextBox.Location = new System.Drawing.Point(106, 32);
			this.ComPortTextBox.Size = new System.Drawing.Size(34, 26);
			this.ComPortTextBox.Text = "4";
			this.ComPortTextBox.GotFocus += new System.EventHandler(this.ComPortTextBox_GotFocus);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
			this.label1.Location = new System.Drawing.Point(30, 36);
			this.label1.Size = new System.Drawing.Size(74, 20);
			this.label1.Text = "Com. Port: ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// CloseButton
			// 
			this.CloseButton.Location = new System.Drawing.Point(34, 100);
			this.CloseButton.Size = new System.Drawing.Size(108, 30);
			this.CloseButton.Text = "Close";
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// MsgLabel
			// 
			this.MsgLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.MsgLabel.ForeColor = System.Drawing.Color.Green;
			this.MsgLabel.Location = new System.Drawing.Point(8, 140);
			this.MsgLabel.Size = new System.Drawing.Size(158, 20);
			this.MsgLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// Form2
			// 
			this.ClientSize = new System.Drawing.Size(172, 171);
			this.ControlBox = false;
			this.Controls.Add(this.MsgLabel);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ComPortTextBox);
			this.Controls.Add(this.OpenPortButton);
			this.Location = new System.Drawing.Point(35, 45);
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Text = "Com. Port";
			this.Load += new System.EventHandler(this.Form2_Load);

		}
		#endregion

		private void Form2_Load(object sender, System.EventArgs e)
		{
		   ComPortTextBox.Text = m_form.GetPort().ToString();
		   port = m_form.GetPort();
		}

		private void OpenPortButton_Click(object sender, System.EventArgs e)
		{
			int ret = 0;

			m_form.RSSIProgressBar.Value = 0;
			m_form.RSSITextBox.Text = "";

			if (m_form.IsPortOpen())
			{
				ret = m_form.api.rfClose();
				if (ret < 0)
				{
					m_form.SetPort(false, port);
					m_form.DisplayMsg("Close failed ret=" + ret);
					return;
				}
				else
                   m_form.DisplayMsg("Port Closed OK. ret=" + ret);
			}
			
			port = Convert.ToUInt32(ComPortTextBox.Text);
			ret = m_form.api.rfOpen(115200 , port);
			if (ret >= 0)
			{
				m_form.SetPort(true, port);
				m_form.WriteComPortNum(Convert.ToString(port));
				m_form.DisplayMsg("Open port OK. ret=" + ret);
				m_form.PlaySound(1);
                m_form.ResetReader(0);
				MsgLabel.ForeColor = System.Drawing.Color.Green;
				MsgLabel.Text = "Port Opened Successfully";
			}
			else
			{
				m_form.DisplayMsg("Open failed ret=" + ret);
				m_form.SetPort(false, port);
				m_form.PlaySound(1);
				MsgLabel.ForeColor = System.Drawing.Color.Red;
				MsgLabel.Text = "Open Port Failed";
			}
		}

		private void ComPortTextBox_GotFocus(object sender, System.EventArgs e)
		{
			//inputPanel1.Enabled = true;
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
