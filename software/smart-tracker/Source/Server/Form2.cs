using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Text;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class HelpForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel label5;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblBuild;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.PictureBox m_picLogin;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HelpForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            string title = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(
                Assembly.GetExecutingAssembly(), typeof(AssemblyTitleAttribute), false))
               .Title;

            string version = ((AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(
                Assembly.GetExecutingAssembly(), typeof(AssemblyFileVersionAttribute), false))
                .Version;

            StringBuilder desc = new StringBuilder(title);
#if LITE
            desc.Append(" Lite");
#endif

#if SANI
            desc.Append(" (Sani Faucet)");
#endif
            desc.Append(" V");
            desc.Append(version);

            lblVersion.Text = desc.ToString();

            DateTime date = new DateTime(2000, 1, 1);

            string[] parts = Assembly.GetExecutingAssembly().FullName.Split(',');

            string[] versionParts = parts[1].Split('.');

            date = date.AddDays(Int32.Parse(versionParts[2]));

            date = date.AddSeconds(Int32.Parse(versionParts[3]) * 2);

            if (System.TimeZoneInfo.Local.IsDaylightSavingTime(date))
            {
                date = date.AddHours(1);
            }

            lblBuild.Text = string.Format("Built {0}",
                date.ToString("g", System.Globalization.CultureInfo.InvariantCulture));
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblBuild = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.m_picLogin = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_picLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "ActiveWave Inc.";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(12, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(258, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Congress Corporate Plaza";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(12, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(258, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "902 Clintmoore Road. Suite 118";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(12, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(258, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Boca Raton, FL 33487";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(12, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(258, 23);
            this.label5.TabIndex = 4;
            this.label5.TabStop = true;
            this.label5.Text = "www.activewaveinc.com";
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.Blue;
            this.lblVersion.Location = new System.Drawing.Point(12, 248);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(260, 23);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "Smart Tracker V?.?.0.0";
            // 
            // lblBuild
            // 
            this.lblBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuild.ForeColor = System.Drawing.Color.Blue;
            this.lblBuild.Location = new System.Drawing.Point(12, 278);
            this.lblBuild.Name = "lblBuild";
            this.lblBuild.Size = new System.Drawing.Size(258, 23);
            this.lblBuild.TabIndex = 7;
            this.lblBuild.Text = "Build  ??? ??, ????";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(12, 308);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(260, 23);
            this.label9.TabIndex = 8;
            // 
            // m_picLogin
            // 
            this.m_picLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_picLogin.BackColor = System.Drawing.Color.White;
            this.m_picLogin.Image = ((System.Drawing.Image)(resources.GetObject("m_picLogin.Image")));
            this.m_picLogin.Location = new System.Drawing.Point(2, 2);
            this.m_picLogin.Name = "m_picLogin";
            this.m_picLogin.Size = new System.Drawing.Size(280, 75);
            this.m_picLogin.TabIndex = 15;
            this.m_picLogin.TabStop = false;
            // 
            // HelpForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(282, 343);
            this.Controls.Add(this.m_picLogin);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblBuild);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Smart Tracker ";
            ((System.ComponentModel.ISupportInitialize)(this.m_picLogin)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
	}
}
