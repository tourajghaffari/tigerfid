using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using AWI.Comm;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for WaitForm.
	/// </summary>
	public class WaitForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.PictureBox pictureBox5;
		private System.Windows.Forms.PictureBox pictureBox6;
		private System.Timers.Timer timer1;
		private int counter;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WaitForm(CommForm f)
		{
			counter = 0;
            InitializeComponent();
		}

		public void StartTimer()
		{
            timer1.Enabled = true;
		}

		public WaitForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.pictureBox5 = new System.Windows.Forms.PictureBox();
			this.pictureBox6 = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Timers.Timer();
			((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(232, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please Wait . Connecting to the network.";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Silver;
			this.pictureBox1.Location = new System.Drawing.Point(24, 44);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(24, 16);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.Silver;
			this.pictureBox2.Location = new System.Drawing.Point(64, 44);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(24, 16);
			this.pictureBox2.TabIndex = 2;
			this.pictureBox2.TabStop = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.BackColor = System.Drawing.Color.Silver;
			this.pictureBox3.Location = new System.Drawing.Point(104, 44);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(24, 16);
			this.pictureBox3.TabIndex = 3;
			this.pictureBox3.TabStop = false;
			// 
			// pictureBox4
			// 
			this.pictureBox4.BackColor = System.Drawing.Color.Silver;
			this.pictureBox4.Location = new System.Drawing.Point(144, 44);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(24, 16);
			this.pictureBox4.TabIndex = 4;
			this.pictureBox4.TabStop = false;
			// 
			// pictureBox5
			// 
			this.pictureBox5.BackColor = System.Drawing.Color.Silver;
			this.pictureBox5.Location = new System.Drawing.Point(184, 44);
			this.pictureBox5.Name = "pictureBox5";
			this.pictureBox5.Size = new System.Drawing.Size(24, 16);
			this.pictureBox5.TabIndex = 5;
			this.pictureBox5.TabStop = false;
			// 
			// pictureBox6
			// 
			this.pictureBox6.BackColor = System.Drawing.Color.Silver;
			this.pictureBox6.Location = new System.Drawing.Point(224, 44);
			this.pictureBox6.Name = "pictureBox6";
			this.pictureBox6.Size = new System.Drawing.Size(24, 16);
			this.pictureBox6.TabIndex = 6;
			this.pictureBox6.TabStop = false;
			// 
			// timer1
			// 
			this.timer1.Interval = 500;
			this.timer1.SynchronizingObject = this;
			this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
			// 
			// WaitForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(264, 71);
			this.ControlBox = false;
			this.Controls.Add(this.pictureBox6);
			this.Controls.Add(this.pictureBox5);
			this.Controls.Add(this.pictureBox4);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WaitForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sysytem Message";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (counter == 0)
			{
				pictureBox1.BackColor = System.Drawing.Color.Blue;
                counter++;
			}
			else if (counter == 1)
			{
				pictureBox2.BackColor = System.Drawing.Color.Blue;
				counter++;
			}
			else if (counter == 2)
			{
				pictureBox3.BackColor = System.Drawing.Color.Blue;
				counter++;
			}
			else if (counter == 3)
			{
				pictureBox4.BackColor = System.Drawing.Color.Blue;
				counter++;
			}
			else if (counter == 4)
			{
				pictureBox5.BackColor = System.Drawing.Color.Blue;
				counter++;
			}
			else if (counter == 5)
			{
				pictureBox6.BackColor = System.Drawing.Color.Blue;
				counter++;
			}
			else if (counter == 6)
			{
				pictureBox1.BackColor = System.Drawing.Color.Silver;
				pictureBox2.BackColor = System.Drawing.Color.Silver;
				pictureBox3.BackColor = System.Drawing.Color.Silver;
				pictureBox4.BackColor = System.Drawing.Color.Silver;
				pictureBox5.BackColor = System.Drawing.Color.Silver;
                pictureBox6.BackColor = System.Drawing.Color.Silver;
				counter = 0;
			}
		}
	}
}
