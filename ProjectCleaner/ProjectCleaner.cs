using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectCleaner.Cleaners;

namespace ProjectCleaner
{
	public partial class ProjectCleaner : Form
	{
		private readonly DialogResult[] _cancelledStatuses = new[] { DialogResult.Abort, DialogResult.Cancel, DialogResult.No, DialogResult.None, DialogResult.Retry };

        private readonly IAsyncCleaner _cleaner;
        private readonly IStatusTracker _statusTracker;

        public ProjectCleaner(IStatusTracker statusTracker, IAsyncCleaner cleaner) 
		{
			InitializeComponent();

            _statusTracker = statusTracker;
            _cleaner = cleaner;

            statusTracker.OnIncrement = value => processCounter.Text = value.ToString();
        }

		private RecycleOption RecycleOption
		{
			get
			{
				if (rbDelete.Checked)
				{
					return RecycleOption.DeletePermanently;
				}

				return RecycleOption.SendToRecycleBin;
			}
		}

		private void InitUI()
		{
			btnClean.Enabled = !string.IsNullOrEmpty(txtFolder.Text);
			rbRecycle.Checked = true;
			rbDelete.Checked = false;
		}

        private async Task CleanAsync()
        {
            var filePath = txtFolder.Text.Trim();
            
            if (ValidateForm(filePath))
            {
                processCounter.Text = "0";
                var options = CleanerOptions.None;

                if (cbTempFiles.Checked) options |= CleanerOptions.ClearTemporaryFiles;
                if (cbAspFiles.Checked) options |= CleanerOptions.ClearAspNetFiles;
                if (cbNugetPackages.Checked) options |= CleanerOptions.ClearNugetPackages;
                if (cbNodeModules.Checked) options |= CleanerOptions.ClearNodeModules;

                await _cleaner.CleanAsync(filePath, options, RecycleOption);

                MessageBox.Show(BuildSuccessMessage(_statusTracker), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        
		private void ToggleOptions(bool selected)
		{
			var checkBoxes = groupBox1.Controls.OfType<CheckBox>();
			foreach (var option in checkBoxes)
				option.Checked = selected;
		}

		private void SelectFile()
		{
			var folderBrowser = new FolderBrowserDialog();
			folderBrowser.ShowNewFolderButton = false;

			var result = folderBrowser.ShowDialog(this);

			if (!_cancelledStatuses.Contains(result))
				txtFolder.Text = folderBrowser.SelectedPath.Trim();
		}

        private void LockUI()
        {
            txtFolder.Enabled = false;
            btnBrowse.Enabled = false;
            btnCancel.Enabled = false;
            btnClean.Enabled = false;
            cbAspFiles.Enabled = false;
            cbTempFiles.Enabled = false;
            cbNugetPackages.Enabled = false;
            cbNodeModules.Enabled = false;
            btnSelectAll.Enabled = false;
            btnSelectNone.Enabled = false;
            rbRecycle.Enabled = false;
            rbDelete.Enabled = false;
        }

        private void UnlockUI()
        {
            txtFolder.Enabled = true;
            btnBrowse.Enabled = true;
            btnCancel.Enabled = true;
            btnClean.Enabled = true;
            cbAspFiles.Enabled = true;
            cbTempFiles.Enabled = true;
            cbNugetPackages.Enabled = true;
            cbNodeModules.Enabled = true;
            btnSelectAll.Enabled = true;
            btnSelectNone.Enabled = true;
            rbRecycle.Enabled = true;
            rbDelete.Enabled = true;
        }

        private string BuildSuccessMessage(IStatusTracker statusTracker)
        {
            var sbSuccess = new StringBuilder("Cleaning Successful");

            if (statusTracker.FailedFiles.Any())
            {
                sbSuccess.AppendLine();
                sbSuccess.AppendLine();
                sbSuccess.AppendLine("The following files or folders could not be removed becuase they are either in use by another process or you do not have permission to access them:");
                sbSuccess.AppendLine();

                foreach (var path in statusTracker.FailedFiles.Keys)
                    sbSuccess.AppendLine(path);
            }

            return sbSuccess.ToString();
        }

        private bool ValidateForm(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				MessageBox.Show("Please select a folder to process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			else if (!Directory.Exists(filePath))
			{
				MessageBox.Show("The selected file path is invalid.\r\nPlease review make the necessary corrections.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			else if (System.Text.RegularExpressions.Regex.IsMatch(filePath, @"^[a-zA-Z]\:\\?$"))
			{
				var rootFolderResult = MessageBox.Show("WARNING: You have selected a root drive folder. Processing this will remove all bin and obj folders on the entire drive and could cause unexpected results.\r\n\r\nAre you sure you would like to continue?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				if (_cancelledStatuses.Contains(rootFolderResult))
					return false;
			}

			return true;
		}

		#region Events

		private void ProjectCleaner_Load(object sender, EventArgs e) => InitUI();

		private void btnSelectAll_Click(object sender, EventArgs e) => ToggleOptions(true);

		private void btnSelectNone_Click(object sender, EventArgs e) => ToggleOptions(false);

		private void btnCancel_Click(object sender, EventArgs e) => Close();

		private async void btnClean_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtFolder.Text))
			{
                try
                {
                    LockUI();

                    // set up kind like this to revent UI deadlocks
                    var task = Task.Run(async () => await CleanAsync());
                    await task;

                    UnlockUI();
                }
                catch (Exception)
                {
                    throw; // to make sure exceptions from the inner threads bubble up properly   
                }
			}
		}

		private void txtFolder_TextChanged(object sender, EventArgs e) => btnClean.Enabled = !string.IsNullOrEmpty(txtFolder.Text.Trim());

		private void btnBrowse_Click(object sender, EventArgs e) => SelectFile();

		private void rbDelete_CheckedChanged(object sender, EventArgs e) => rbRecycle.Checked = !rbDelete.Checked;

		private void rbRecycle_CheckedChanged(object sender, EventArgs e) => rbDelete.Checked = !rbRecycle.Checked;

        #endregion Events
    }
}