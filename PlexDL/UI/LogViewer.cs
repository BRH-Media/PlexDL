using LogDel;
using LogDel.Utilities;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.SearchFramework;
using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.UI
{
    public partial class LogViewer : DoubleBufferedForm
    {
        private bool _isFiltered;

#pragma warning disable 414
        private DataTable _logFiltered;
#pragma warning restore 414
        private DataTable _logRecords;

        public LogViewer()
        {
            InitializeComponent();
        }

        public string LogDir { get; set; } = Vars.LogDirectory;

        public void RefreshLogItems()
        {
            try
            {
                var already = -1;
                if (lstLogFiles.SelectedItem != null)
                    already = lstLogFiles.SelectedIndex;
                lstLogFiles.Items.Clear();
                if (Directory.Exists(LogDir))
                    foreach (var file in Directory.GetFiles(LogDir))
                        if (string.Equals(Path.GetExtension(file).ToLower(), ".log") ||
                            string.Equals(Path.GetExtension(file).ToLower(), ".logdel"))
                            lstLogFiles.Items.Add(Path.GetFileName(file));

                if (already > -1)
                {
                    try
                    {
                        lstLogFiles.SelectedIndex = already;
                    }
                    catch
                    {
                        // ignored
                    }
                }
                else
                {
                    if (lstLogFiles.Items.Count <= 0) return;

                    lstLogFiles.SelectedIndex = 0;
                    DoLoadFromSelected();
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error("An error occurred whilst refreshing log files. Details:\n\n" + ex,
                    @"Data Error");
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
            return LogReader.TableFromFile($"{LogDir}\\{lstLogFiles.Items[lstLogFiles.SelectedIndex]}", false);
        }

        private void DoLoadFromSelected()
        {
            try
            {
                if (lstLogFiles.SelectedIndex <= -1) return;

                var table = TableFromSelected();

                if (table == null) return;

                dgvMain.DataSource = table;
                _logRecords = table;
            }
            catch (Exception ex)
            {
                UIMessages.Error("An error occurred whilst loading the log file. Details:\n\n" + ex,
                    @"Data Error");
                dgvMain.DataSource = null;
            }
        }

        private void DgvMain_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMain.Rows.Count <= 0) return;

            var value = dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            UIMessages.Info(value, @"Cell Content");
        }

        private void ItmRefresh_Click(object sender, EventArgs e)
        {
            RefreshLogItems();
        }

        private void CancelSearch()
        {
            //Globals set
            _isFiltered = false;
            _logFiltered = null;

            //GUI set
            GuiSetStopSearch();

            //Reset grid back to full log
            dgvMain.DataSource = _logRecords;
        }

        private void GuiSetStartSearch()
        {
            itmSearchTerm.Enabled = false;
            itmThisSession.Enabled = false;
            itmCancelSearch.Enabled = true;
        }

        private void GuiSetStopSearch()
        {
            itmSearchTerm.Enabled = true;
            itmThisSession.Enabled = true;
            itmCancelSearch.Enabled = false;
        }

        private void ItmSearchTerm_Click(object sender, EventArgs e)
        {
            if (_isFiltered)
                CancelSearch();
            else
                RunTitleSearch();
        }

        private void ItmGoToLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMain.Rows.Count <= 0) return;

                var ipt = ObjectProvider.LibUi.showInputForm(@"Enter Row Number", @"Go To Row");

                if (string.Equals(ipt.txt, "!cancel=user"))
                {
                    // do nothing (exit)
                }
                else if (string.IsNullOrEmpty(ipt.txt))
                {
                    UIMessages.Error(@"Nothing was entered",
                        @"Validation Error");
                }
                else if (!int.TryParse(ipt.txt, out _))
                {
                    UIMessages.Error(@"Specified value is not numeric",
                        @"Validation Error");
                }
                else
                {
                    foreach (DataGridViewRow row in dgvMain.Rows)
                        if (dgvMain.Rows.IndexOf(row) == Convert.ToInt32(ipt.txt) - 1)
                        {
                            dgvMain.CurrentCell = row.Cells[0];
                            return;
                        }

                    UIMessages.Error(@"Couldn't find row '" + ipt.txt + @"'",
                        @"Validation Error");
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "LogFindLineError");
                UIMessages.Error(ex.ToString());
            }
        }

        private void ItmBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(LogDir))
                {
                    if (sfdBackup.ShowDialog() != DialogResult.OK) return;

                    if (File.Exists(sfdBackup.FileName))
                        File.Delete(sfdBackup.FileName);
                    ZipFile.CreateFromDirectory(LogDir, sfdBackup.FileName, CompressionLevel.Optimal, false);
                    UIMessages.Info(@"Successfully backed up logs to " + sfdBackup.FileName);
                }
                else
                {
                    UIMessages.Error(
                        @"Could not backup logs due to no folder existing. This is a clear sign that no logs have been created, however you can check by clicking the Refresh button on the bottom left of this form.",
                        @"Validation Error");
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error("An error occurred whilst backing up log files. Details:\n\n" + ex,
                    "IO Error");
            }
        }

        private void ItmCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstLogFiles.SelectedIndex > -1)
                {
                    var sel = LogDir + @"\" + lstLogFiles.SelectedItem;
                    if (File.Exists(sel))
                    {
                        if (sfdExportCsv.ShowDialog() != DialogResult.OK) return;

                        var f = sfdExportCsv.FileName;
                        var t = LogReader.TableFromFile(sel, false, false);
                        t.ToCSV(f);
                        UIMessages.Info("Successfully exported log file to CSV",
                            "Success");
                    }
                    else
                    {
                        UIMessages.Error("Selected file does not exist",
                            "Validation Error");
                    }
                }
                else
                {
                    UIMessages.Error("Nothing is selected",
                        "Validation Error");
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error("Error occurred whilst exporting your log file. Details:\n\n" + ex,
                    "IO Error");
            }
        }

        private void ItmJson_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstLogFiles.SelectedIndex > -1)
                {
                    var sel = LogDir + @"\" + lstLogFiles.SelectedItem;
                    if (File.Exists(sel))
                    {
                        if (sfdExportJson.ShowDialog() != DialogResult.OK) return;

                        var f = sfdExportJson.FileName;
                        var t = LogReader.TableFromFile(sel, false, false);
                        t.ToJson(f);
                        UIMessages.Info("Successfully exported log file to JSON",
                            "Success");
                    }
                    else
                    {
                        UIMessages.Error("Selected file does not exist",
                            "Validation Error");
                    }
                }
                else
                {
                    UIMessages.Error("Nothing is selected",
                        "Validation Error");
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error("Error occurred whilst exporting your log file. Details:\n\n" + ex,
                    "IO Error");
            }
        }

        private void MenuMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void SessionIdSearch(string sessionId, bool directMatch = true)
        {
            try
            {
                if (dgvMain.Rows.Count <= 0) return;

                if (SessionIdPresent())
                {
                    var rule = directMatch ? SearchRule.EqualsKey : SearchRule.ContainsKey;

                    var searchContext = new SearchData
                    {
                        SearchColumn = "SessionID",
                        SearchRule = rule,
                        SearchTable = _logRecords,
                        SearchTerm = sessionId
                    };

                    if (Search.RunTitleSearch(dgvMain, searchContext))
                    {
                        _isFiltered = true;
                        GuiSetStartSearch();
                    }
                    else
                    {
                        CancelSearch();
                    }
                }
                else
                {
                    UIMessages.Error("Couldn't find a valid 'SessionID' column. Have you got the correct log loaded?",
                        "Validation Error");
                    CancelSearch();
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "LogSearchError");
                UIMessages.Error(ex.ToString());
                CancelSearch();
            }
        }

        private void RunTitleSearch()
        {
            try
            {
                if (dgvMain.Rows.Count <= 0) return;

                if (Search.RunTitleSearch(dgvMain, _logRecords))
                {
                    _isFiltered = true;
                    GuiSetStartSearch();
                }
                else
                {
                    CancelSearch();
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "LogSearchError");
                UIMessages.Error(ex.ToString());
                CancelSearch();
            }
        }

        private void ItmThisSession_Click(object sender, EventArgs e)
        {
            SessionIdSearch(Strings.CurrentSessionId);
        }

        private bool SessionIdPresent()
        {
            return _logRecords != null && _logRecords.Columns.Contains("SessionID");
        }

        private void ItmCancelSearch_Click(object sender, EventArgs e)
        {
            if (_isFiltered)
                CancelSearch();
        }
    }
}