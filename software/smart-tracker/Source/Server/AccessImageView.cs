using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
//using ActiveWave.RfidDb;
//using ActiveWave.Controls; [juanhere]

namespace ActiveWave.CarTracker
{
	public class AccessImageView : System.Windows.Forms.UserControl
	{
      private ActiveWave.Controls.TitleBar m_titleBar;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label m_lblTagId;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label m_lblLocation;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox m_picImage;
      
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      #region Constructor
      public AccessImageView()
		{
			InitializeComponent();
         
         this.Tag = null;
         //m_rfid.TagChanged += new RfidDb.TagEventHandler(OnTagChanged);
         //m_rfid.TagRemoved += new RfidDb.TagEventHandler(OnTagRemoved);
      }
      #endregion

      /*#region Properties
      public new IRfidTag Tag
      {
         get { return m_tag; }
         set
         {
            // Always reset tracking for now
            m_chkTrack.Checked = false;
            m_chkTrack.Enabled = (value != null);

            m_tag = value;
            RefreshTag();
         }
      }
      #endregion*/

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_titleBar = new ActiveWave.Controls.TitleBar();
			this.panel1 = new System.Windows.Forms.Panel();
			this.m_picImage = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.m_lblTagId = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.m_lblLocation = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_titleBar
			// 
			this.m_titleBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_titleBar.BackColor = System.Drawing.Color.Navy;
			this.m_titleBar.BorderColor = System.Drawing.Color.White;
			this.m_titleBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_titleBar.ForeColor = System.Drawing.Color.White;
			this.m_titleBar.GradientColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.m_titleBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.m_titleBar.Location = new System.Drawing.Point(0, 0);
			this.m_titleBar.Name = "m_titleBar";
			this.m_titleBar.ShadowColor = System.Drawing.Color.Black;
			this.m_titleBar.ShadowOffset = new System.Drawing.Size(1, 1);
			this.m_titleBar.Size = new System.Drawing.Size(190, 24);
			this.m_titleBar.TabIndex = 0;
			this.m_titleBar.Text = "Name: ";
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.m_picImage);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.m_lblTagId);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.m_lblLocation);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(10, 32);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(168, 188);
			this.panel1.TabIndex = 22;
			// 
			// m_picImage
			// 
			this.m_picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_picImage.Location = new System.Drawing.Point(26, 76);
			this.m_picImage.Name = "m_picImage";
			this.m_picImage.Size = new System.Drawing.Size(114, 100);
			this.m_picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.m_picImage.TabIndex = 29;
			this.m_picImage.TabStop = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(6, 26);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 16);
			this.label3.TabIndex = 28;
			this.label3.Text = "Apartment Nr:";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(84, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 16);
			this.label4.TabIndex = 27;
			// 
			// m_lblTagId
			// 
			this.m_lblTagId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lblTagId.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_lblTagId.Location = new System.Drawing.Point(52, 8);
			this.m_lblTagId.Name = "m_lblTagId";
			this.m_lblTagId.Size = new System.Drawing.Size(110, 16);
			this.m_lblTagId.TabIndex = 26;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(6, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 16);
			this.label2.TabIndex = 25;
			this.label2.Text = "Telephone Nr:";
			// 
			// m_lblLocation
			// 
			this.m_lblLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lblLocation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_lblLocation.Location = new System.Drawing.Point(84, 44);
			this.m_lblLocation.Name = "m_lblLocation";
			this.m_lblLocation.Size = new System.Drawing.Size(78, 16);
			this.m_lblLocation.TabIndex = 24;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(6, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 16);
			this.label1.TabIndex = 23;
			this.label1.Text = "Tag ID:";
			// 
			// AccessImageView
			// 
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.m_titleBar);
			this.Name = "AccessImageView";
			this.Size = new System.Drawing.Size(190, 234);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

      #region User events
      private void Track_CheckedChanged(object sender, System.EventArgs e)
      {
         /*if (m_tag != null)
         {
            if (m_chkTrack.Checked && TagTrackOn != null)
               TagTrackOn(m_tag);
            if (!m_chkTrack.Checked && TagTrackOff != null)
               TagTrackOff(m_tag);
         }*/
      }
      #endregion

      /*#region RFID events
      private void OnTagChanged(IRfidTag tag)
      {
         if (m_tag == tag)
         {
            RefreshTag();
         }
      }

      private void OnTagRemoved(IRfidTag tag)
      {
         if (m_tag == tag)
         {
            this.Tag = null;
         }
      }
      #endregion */

      private void RefreshTag()
      {
         /*if (m_tag != null)
         {
            IRfidReader reader = m_rfid.GetReader(m_tag.ReaderId);

            m_titleBar.Text      = m_tag.Name;
            m_lblTagId.Text      = m_tag.Id;
            m_lblLocation.Text   = (reader != null) ? reader.Name : string.Empty;
            m_lblTimestamp.Text  = (m_tag.Timestamp != DateTime.MinValue) ? m_tag.Timestamp.ToString("MM/dd/yyyy hh:mm:ss tt") : string.Empty;
            m_picImage.Image     = GetScaledImage(m_tag.Image);
         }
         else
         {
            m_titleBar.Text      = string.Empty;
            m_lblTagId.Text      = string.Empty;
            m_lblLocation.Text   = string.Empty;
            m_lblTimestamp.Text  = string.Empty;
            m_picImage.Image     = null;

         }
         this.Visible = (m_tag != null);*/
      }

      private Image GetScaledImage(Image image)
      {
         if (image != null)
         {
            // Get image sized to picture box, but maintain aspect ratio
            Size size = m_picImage.Size;
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

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}
   }
}
