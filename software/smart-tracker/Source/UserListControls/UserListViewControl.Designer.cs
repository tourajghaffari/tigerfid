namespace UserListControl
{
    partial class UserListViewControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            ""}, -1);
            this.listView1 = new UserListControl.EditListView();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20});
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(476, 196);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listView1_ItemCheck);
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            // 
            // UserListViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Name = "UserListViewControl";
            this.Size = new System.Drawing.Size(476, 196);
            this.ResumeLayout(false);

        }

        #endregion

        public EditListView listView1;

        //private System.Windows.Forms.ListView listView1;
    }
}
