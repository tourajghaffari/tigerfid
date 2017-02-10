using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
//using ActiveWave.RfidDb;
using ActiveWave.Controls;

namespace ActiveWave.CarTracker
{
	public class TagImageView : System.Windows.Forms.UserControl
	{
      //public event TagEventHandler TagTrackOn = null;
      //public event TagEventHandler TagTrackOff = null;

      //private RfidDbController m_rfid = RfidDbController.theRfidDbController;
      //private IRfidTag m_tag = null;

      private System.Windows.Forms.PictureBox m_picImage;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label m_lblLocation;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label m_lblTagId;
      private ActiveWave.Controls.TitleBar m_titleBar;
      
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      #region Constructor
      public TagImageView()
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
			this.m_picImage = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.m_lblLocation = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.m_lblTagId = new System.Windows.Forms.Label();
			this.m_titleBar = new ActiveWave.Controls.TitleBar();
			this.SuspendLayout();
			// 
			// m_picImage
			// 
			this.m_picImage.Location = new System.Drawing.Point(24, 72);
			this.m_picImage.Name = "m_picImage";
			this.m_picImage.Size = new System.Drawing.Size(160, 128);
			this.m_picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.m_picImage.TabIndex = 12;
			this.m_picImage.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "Tag ID:";
			// 
			// m_lblLocation
			// 
			this.m_lblLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lblLocation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_lblLocation.Location = new System.Drawing.Point(72, 48);
			this.m_lblLocation.Name = "m_lblLocation";
			this.m_lblLocation.Size = new System.Drawing.Size(128, 16);
			this.m_lblLocation.TabIndex = 15;
			this.m_lblLocation.Text = "location";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 16);
			this.label2.TabIndex = 16;
			this.label2.Text = "Location:";
			// 
			// m_lblTagId
			// 
			this.m_lblTagId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lblTagId.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_lblTagId.Location = new System.Drawing.Point(72, 32);
			this.m_lblTagId.Name = "m_lblTagId";
			this.m_lblTagId.Size = new System.Drawing.Size(72, 16);
			this.m_lblTagId.TabIndex = 19;
			this.m_lblTagId.Text = "tagid";
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
			this.m_titleBar.Size = new System.Drawing.Size(208, 23);
			this.m_titleBar.TabIndex = 0;
			this.m_titleBar.Text = "Title";
			// 
			// TagImageView
			// 
			this.Controls.Add(this.m_titleBar);
			this.Controls.Add(this.m_lblTagId);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.m_lblLocation);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.m_picImage);
			this.Name = "TagImageView";
			this.Size = new System.Drawing.Size(208, 208);
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
   }
}
