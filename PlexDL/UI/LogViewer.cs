using LogDel;
using LogDel.Utilities;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.SearchFramework;
using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class LogViewer : Form
    {
        public string dir = AppDomain.CurrentDomain.BaseDirectory + @"\Logs";
        private bool IsFiltered;
        private DataTable logFiltered;
        private DataTable logRecords;

        public LogViewer()
        {
            InitializeComponent();
        }

        public void RefreshLogItems()
        {
            try
            {
                var already = -1;
                if (lstLogFiles.SelectedItem != null)
                    already = lstLogFiles.SelectedIndex;
                lstLogFiles.Items.Clear();
                if (Directory.Exists("Logs"))
                    foreach (var file in Directory.GetFiles("Logs"))
                        if (string.Equals(Path.GetExtension(file).ToLower() ?? "", ".log") ||
                            string.Equals(Path.GetExtension(file).ToLower() ?? "", ".logdel"))
                            lstLogFiles.Items.Add(Path.GetFileName(file));

                if (already > -1)
                {
                    try
                    {
                        lstLogFiles.SelectedIndex = already;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    if (lstLogFiles.Items.Count > 0)
                    {
                        lstLogFiles.SelectedIndex = 0;
                        DoLoadFromSelected();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred whilst refreshing log files. Details:\n\n" + ex, @"Data Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                lstLogFiles.Items.Clear();
            }
        }

        private void LogViewer_Load(object sender, EventArgs e)
        {
            RefreshLogItems();
        }

        private void LstLogFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoLoadFromSelected();
        }

        private DataTable TableFromSelected()
        {
            return LogReader.TableFromFile(@"Logs\" + lstLogFiles.Items[lstLogFiles.SelectedIndex], false);
        }

        private void DoLoadFromSelected()
        {
            try
            {
                if (lstLogFiles.SelectedIndex > -1)
                {
                    var table = TableFromSelected();
                    if (table != null)
                    {
                        dgvMain.DataSource = table;
                        logRecords = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred whilst loading the log file. Details:\n\n" + ex, "Data Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                dgvMain.DataSource = null;
            }
        }

        private void RunTitleSearch()
        {
            try
            {
                if (dgvMain.Rows.Count > 0)
                {
                    if (Search.RunTitleSearch(dgvMain, logRecords))
                    {
                        IsFiltered = true;
                        itmSearch.Text = @"Cancel Search";
                    }
                    else
                    {
                        IsFiltered = false;
                        itmSearch.Text = @"Start Search";
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "SearchError");
                MessageBox.Show(ex.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvMain_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMain.Rows.Count > 0)
            {
                var value = dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                MessageBox.Show(value, @"Cell Content", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void itmRefresh_Click(object sender, EventArgs e)
        {
            RefreshLogItems();
        }

        private void itmSearch_Click(object sender, EventArgs e)
        {
            if (IsFiltered)
            {
                IsFiltered = false;
                logFiltered = null;
                itmSearch.Text = @"Start Search";
                dgvMain.DataSource = logRecords;
            }
            else
            {
                RunTitleSearch();
            }
        }

        private void itmGoToLine_Click(object sender, EventArgs e)
        {
            try
            {
                var ipt = GlobalStaticVars.LibUI.showInputForm(@"Enter Row Number", @"Go To Row");
                if (string.Equals(ipt.txt, "!cancel=user"))
                {
                }
                else if (string.IsNullOrEmpty(ipt.txt))
                {
                    MessageBox.Show(@"Nothing was entered", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!int.TryParse(ipt.txt, out _))
                {
                    MessageBox.Show(@"Specified Value is not numeric", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    foreach (DataGridViewRow row in dgvMain.Rows)
                        if (dgvMain.Rows.IndexOf(row) == Convert.ToInt32(ipt.txt) - 1)
                        {
                            dgvMain.CurrentCell = row.Cells[0];
                            return;
                        }

                    MessageBox.Show(@"Could not find row '" + ipt.txt + @"'", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "SearchError");
                MessageBox.Show(ex.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void itmBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(dir))
                {
                    if (sfdBackup.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(sfdBackup.FileName))
                            File.Delete(sfdBackup.FileName);
                        ZipFile.CreateFromDirectory(dir, sfdBackup.FileName, CompressionLevel.Optimal, false);
                        MessageBox.Show(@"Successfully backed up logs to " + sfdBackup.FileName, @"Message", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(
                        @"Could not backup logs due to no folder existing. This is a clear sign that no logs have been created, however you can check by clicking the Refresh button on the bottom left of this form.",
                        @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred whilst backing up log files. Details:\n\n" + ex, "IO Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void itmCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstLogFiles.SelectedIndex > -1)
                {
                    string sel = dir + @"\" + lstLogFiles.SelectedItem;
                    if (File.Exists(sel))
                    {
                        if (sfdExportCsv.ShowDialog() == DialogResult.OK)
                        {
                            string f = sfdExportCsv.FileName;
                            DataTable t = LogReader.TableFromFile(sel, false, false);
                            t.ToCSV(f);
                            MessageBox.Show("Successfully exported log file to CSV", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        MessageBox.Show("Selected file does not exist", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Nothing is selected", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred whilst exporting your log file. Details:\n\n" + ex, "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void itmJson_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstLogFiles.SelectedIndex > -1)
                {
                    string sel = dir + @"\" + lstLogFiles.SelectedItem;
                    if (File.Exists(sel))
                    {
                        if (sfdExportJson.ShowDialog() == DialogResult.OK)
                        {
                            string f = sfdExportJson.FileName;
                            DataTable t = LogReader.TableFromFile(sel, false, false);
                            t.ToJson(f);
                            MessageBox.Show("Successfully exported log file to JSON", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        MessageBox.Show("Selected file does not exist", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Nothing is selected", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred whilst exporting your log file. Details:\n\n" + ex, "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}