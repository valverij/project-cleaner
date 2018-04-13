namespace ProjectCleaner
{
	partial class ProjectCleaner
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.cbTempFiles = new System.Windows.Forms.CheckBox();
            this.cbAspFiles = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbRecycle = new System.Windows.Forms.RadioButton();
            this.rbDelete = new System.Windows.Forms.RadioButton();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.processCounterLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.processCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbNugetPackages = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(12, 19);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(39, 13);
            this.lblFolder.TabIndex = 0;
            this.lblFolder.Text = "Folder:";
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(57, 16);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(292, 20);
            this.txtFolder.TabIndex = 1;
            this.txtFolder.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(355, 14);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(68, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // cbTempFiles
            // 
            this.cbTempFiles.AutoSize = true;
            this.cbTempFiles.Location = new System.Drawing.Point(6, 19);
            this.cbTempFiles.Name = "cbTempFiles";
            this.cbTempFiles.Size = new System.Drawing.Size(127, 17);
            this.cbTempFiles.TabIndex = 3;
            this.cbTempFiles.Text = "Clear Temporary Files";
            this.cbTempFiles.UseVisualStyleBackColor = true;
            // 
            // cbAspFiles
            // 
            this.cbAspFiles.AutoSize = true;
            this.cbAspFiles.Location = new System.Drawing.Point(6, 42);
            this.cbAspFiles.Name = "cbAspFiles";
            this.cbAspFiles.Size = new System.Drawing.Size(176, 17);
            this.cbAspFiles.TabIndex = 4;
            this.cbAspFiles.Text = "Clear Temporary ASP.NET Files";
            this.cbAspFiles.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbNugetPackages);
            this.groupBox1.Controls.Add(this.rbRecycle);
            this.groupBox1.Controls.Add(this.rbDelete);
            this.groupBox1.Controls.Add(this.cbAspFiles);
            this.groupBox1.Controls.Add(this.cbTempFiles);
            this.groupBox1.Location = new System.Drawing.Point(15, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 112);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cleaner Options";
            // 
            // rbRecycle
            // 
            this.rbRecycle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbRecycle.AutoSize = true;
            this.rbRecycle.Location = new System.Drawing.Point(129, 89);
            this.rbRecycle.Name = "rbRecycle";
            this.rbRecycle.Size = new System.Drawing.Size(122, 17);
            this.rbRecycle.TabIndex = 6;
            this.rbRecycle.TabStop = true;
            this.rbRecycle.Text = "Send to Recycle Bin";
            this.rbRecycle.UseVisualStyleBackColor = true;
            this.rbRecycle.CheckedChanged += new System.EventHandler(this.rbRecycle_CheckedChanged);
            // 
            // rbDelete
            // 
            this.rbDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbDelete.AutoSize = true;
            this.rbDelete.Location = new System.Drawing.Point(6, 89);
            this.rbDelete.Name = "rbDelete";
            this.rbDelete.Size = new System.Drawing.Size(117, 17);
            this.rbDelete.TabIndex = 5;
            this.rbDelete.TabStop = true;
            this.rbDelete.Text = "Delete Permanently";
            this.rbDelete.UseVisualStyleBackColor = true;
            this.rbDelete.CheckedChanged += new System.EventHandler(this.rbDelete_CheckedChanged);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectNone.Location = new System.Drawing.Point(96, 163);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(75, 23);
            this.btnSelectNone.TabIndex = 6;
            this.btnSelectNone.Text = "Select None";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAll.Location = new System.Drawing.Point(15, 163);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnClean
            // 
            this.btnClean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClean.Location = new System.Drawing.Point(267, 163);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 23);
            this.btnClean.TabIndex = 7;
            this.btnClean.Text = "Clean";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(348, 163);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processCounterLabel,
            this.processCounter});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 190);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(435, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 9;
            this.statusStrip.Text = "statusStrip1";
            // 
            // processCounterLabel
            // 
            this.processCounterLabel.Name = "processCounterLabel";
            this.processCounterLabel.Size = new System.Drawing.Size(106, 17);
            this.processCounterLabel.Text = "Objects processed:";
            // 
            // processCounter
            // 
            this.processCounter.Name = "processCounter";
            this.processCounter.Size = new System.Drawing.Size(13, 17);
            this.processCounter.Text = "0";
            // 
            // cbNugetPackages
            // 
            this.cbNugetPackages.AutoSize = true;
            this.cbNugetPackages.Location = new System.Drawing.Point(6, 65);
            this.cbNugetPackages.Name = "cbNugetPackages";
            this.cbNugetPackages.Size = new System.Drawing.Size(133, 17);
            this.cbNugetPackages.TabIndex = 7;
            this.cbNugetPackages.Text = "Clear Nuget Packages";
            this.cbNugetPackages.UseVisualStyleBackColor = true;
            // 
            // ProjectCleaner
            // 
            this.AcceptButton = this.btnClean;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(435, 212);
            this.Controls.Add(this.btnSelectNone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectCleaner";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project Cleaner";
            this.Load += new System.EventHandler(this.ProjectCleaner_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblFolder;
		private System.Windows.Forms.TextBox txtFolder;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.CheckBox cbTempFiles;
		private System.Windows.Forms.CheckBox cbAspFiles;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnSelectNone;
		private System.Windows.Forms.Button btnSelectAll;
		private System.Windows.Forms.Button btnClean;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.RadioButton rbRecycle;
		private System.Windows.Forms.RadioButton rbDelete;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel processCounterLabel;
        private System.Windows.Forms.ToolStripStatusLabel processCounter;
        private System.Windows.Forms.CheckBox cbNugetPackages;
    }
}