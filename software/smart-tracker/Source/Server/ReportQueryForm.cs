using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;

namespace AWI.SmartTracker
{
	/// <summary>
	/// Summary description for ReportQueryForm.
	/// </summary>
	public class ReportQueryForm : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Label IDLabel;
		private System.Windows.Forms.TextBox IDTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button CloseButton;
		public  string id;
		public DateTime fromDate;
		public DateTime toDate;
		private System.Windows.Forms.DateTimePicker FromDateTimePicker;
		private System.Windows.Forms.DateTimePicker ToDateTimePicker;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ReportQueryForm()
		{
			InitializeComponent();
		}

		public ReportQueryForm(ReportForm rform)
		{
			CultureInfo ci = new CultureInfo("sv-SE", true);
			System.Threading.Thread.CurrentThread.CurrentCulture = ci; 
			ci.DateTimeFormat.DateSeparator = "-";

			InitializeComponent();
			FromDateTimePicker.Value = DateTime.Now;
			ToDateTimePicker.Value = DateTime.Now;

			if (rform.alarm)
			{
               IDLabel.Enabled = false;
			   IDTextBox.ReadOnly = true;
			}
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
            this.IDLabel = new System.Windows.Forms.Label();
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.FromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ToDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.OKButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IDLabel
            // 
            this.IDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDLabel.Location = new System.Drawing.Point(24, 24);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(56, 20);
            this.IDLabel.TabIndex = 0;
            this.IDLabel.Text = "Tag ID: ";
            this.IDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IDTextBox
            // 
            this.IDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDTextBox.Location = new System.Drawing.Point(84, 24);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.Size = new System.Drawing.Size(164, 22);
            this.IDTextBox.TabIndex = 1;
            // 
            // FromDateTimePicker
            // 
            this.FromDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromDateTimePicker.Location = new System.Drawing.Point(84, 72);
            this.FromDateTimePicker.Name = "FromDateTimePicker";
            this.FromDateTimePicker.Size = new System.Drawing.Size(248, 22);
            this.FromDateTimePicker.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "From:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "To:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ToDateTimePicker
            // 
            this.ToDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToDateTimePicker.Location = new System.Drawing.Point(84, 124);
            this.ToDateTimePicker.Name = "ToDateTimePicker";
            this.ToDateTimePicker.Size = new System.Drawing.Size(248, 22);
            this.ToDateTimePicker.TabIndex = 4;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(76, 188);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(84, 28);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(224, 188);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(84, 28);
            this.CloseButton.TabIndex = 7;
            this.CloseButton.Text = "Cancel";
            // 
            // ReportQueryForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(372, 233);
            this.ControlBox = false;
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ToDateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FromDateTimePicker);
            this.Controls.Add(this.IDTextBox);
            this.Controls.Add(this.IDLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportQueryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report Query Form";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void OKButton_Click(object sender, System.EventArgs e)
		{
			id = IDTextBox.Text;
			fromDate = FromDateTimePicker.Value;
			toDate = ToDateTimePicker.Value;
			Close();
		}
	}
}
