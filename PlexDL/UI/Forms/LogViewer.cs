using LogDel;
using LogDel.IO;
using LogDel.Utilities.Export;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.SearchFramework;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using UIHelpers;

#pragma warning disable 1591
#pragma warning disable 414

namespace PlexDL.UI.Forms
{
    public partial class LogViewer : DoubleBufferedForm
    {
        private bool _isFiltered;
        private DataTable _logRecords;

        public LogViewer()
        {
            InitializeComponent();
        }

        public string LogDir { get; set; } = Globals.LogDirectory;

        public void RefreshLogItems()
        {
            try
            {
                //the currently selected index (-1 if not selected)
                var already = lstLogFiles.SelectedIndex;

                //clear all current items
                lstLogFiles.Items.Clear();

                //ensure the logging directory exists
                if (Directory.Exists(LogDir))

                    //loop through each file in the logging directory
                    foreach (var file in Directory.GetFiles(LogDir))

                        //check if the current file is a valid log file based on the file extension
                        if (Path.GetExtension(file).ToLower() == ".log" ||
                            Path.GetExtension(file).ToLower() == ".logdel")

                            //we've ensured it's a valid file, now we add it to the list
                            lstLogFiles.Items.Add(Path.GetFileName(file));

                //the value is -1 if nothing was selected previously
                if (already > -1)
                {
                    //try-catch with empty error response ensures the user doesn't
                    //get bothered if we fail to reselect the previous value
                    try
                    {
                        //attempt to reselect the previous value
                        lstLogFiles.SelectedIndex = already;
                    }
                    catch
                    {
                        // ignored
                    }
                }
                else
                {
                    //ensure we were able to add items to the list
                    if (lstLogFiles.Items.Count <= 0) return;

                    //the default selected index will be 0 (first item in the list)
                    lstLogFiles.SelectedIndex = 0;

                    //load the newly selected index
                    DoLoadFromSelected();
                }
            }
            catch (Exception ex)
            {
                //alert the user
                UIMessages.Error("An error occurred whilst refreshing log files. Details:\n\n" + ex,
                    @"Data Error");

                //clear all logs from the list
                lstLogFiles.Items.Clear();
            }
        }

        private void LogViewer_Load(object sender, EventArgs e)
        {
            //load all logs in the ListBox on loading the viewer
            RefreshLogItems();
        }

        private void LstLogFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            //when we select a new log, load it
            DoLoadFromSelected();
        }

        private DataTable TableFromSelected()
        {
            //get a DataTable from the currently selected ListBox log file
            return LogReader.TableFromFile(
                $"{LogDir}\\{lstLogFiles.Items[lstLogFiles.SelectedIndex]}",
                false);
        }

        private void DoLoadFromSelected()
        {
            try
            {
                // if the index is -1 then nothing is selected at all
                if (lstLogFiles.SelectedIndex <= -1) return;

                //get a table from the currently selected log file
                var table = TableFromSelected();

                //ensure the loaded log is valid (not null)
                if (table == null) return;

                //assign the log to the log viewer grid
                dgvMain.DataSource = table;

                //the original data is stored safely to ensure searches can be cleared correctly
                _logRecords = table;

                //update record status
                SetInterfaceViewingStatus();
                SetInterfaceCurrentLog(lstLogFiles.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                //alert the user
                UIMessages.Error("An error occurred whilst loading the log file. Details:\n\n" + ex,
                    @"Data Error");

                //clear the log viewer grid
                dgvMain.DataSource = null;

                //clear the global
                _logRecords = null;

                //update record view status
                SetInterfaceViewingStatus();
            }
        }

        private void DgvMain_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //ensure the row count is above 0 before continuing
            if (dgvMain.Rows.Count <= 0)
                return;

            //it's above zero, so grab the value of the current cell that was double-clicked
            var value =
                dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            //alert the user to the value they double-clicked
            UIMessages.Info(value, @"Cell Content");
        }

        private void ItmRefresh_Click(object sender, EventArgs e)
        {
            //when the refresh button is clicked, all log files are reloaded into the ListBox
            RefreshLogItems();
        }

        private void CancelSearch()
        {
            //Globals set
            _isFiltered = false;

            //Reset grid back to full log
            dgvMain.DataSource = _logRecords;

            //GUI set
            GuiSetStopSearch();
        }

        private void GuiSetStartSearch()
        {
            //ensure the GUI is set correctly to when a search has already started
            itmSearchTerm.Enabled = false;
            itmThisSession.Enabled = false;
            itmCancelSearch.Enabled = true;

            //update record view status
            SetInterfaceViewingStatus();
        }

        private void GuiSetStopSearch()
        {
            //ensure the GUI is set correctly to when a search has been stopped, and is available
            itmSearchTerm.Enabled = true;
            itmThisSession.Enabled = true;
            itmCancelSearch.Enabled = false;

            //update record view status
            SetInterfaceViewingStatus();
        }

        private void SetInterfaceCurrentLog(string currentLog)
        {
            try
            {
                //null check encompasses the validity
                var isValid = !string.IsNullOrWhiteSpace(currentLog);

                //just the file name (in-case we get given a path)
                var fileName = Path.GetFileName(currentLog);

                //set the fore colour dependent on status
                lblCurrentLogFile.ForeColor = isValid
                    ? Color.Black
                    : Color.DarkRed;

                //set text dependent on status
                lblCurrentLogFile.Text = isValid
                    ? fileName
                    : @"Not Loaded";
            }
            catch (Exception ex)
            {
                //log the error but don't inform the user
                LoggingHelpers.RecordException(ex.Message, @"LogViewerCurrentLogStatusError");
            }
        }

        private void SetInterfaceViewingStatus()
        {
            try
            {
                //check for nulls
                if (_logRecords != null && dgvMain.DataSource != null)
                {
                    //data length counters
                    var gridCount = dgvMain.Rows.Count;
                    var totalCount = _logRecords.Rows.Count;

                    //the total can't exceed the current amount
                    lblViewingValue.Text = totalCount < gridCount
                        ? $@"{gridCount}/{totalCount}"
                        : $@"{totalCount}/{totalCount}";
                }
                else
                    //set the viewing values to 0
                    lblViewingValue.Text = @"0/0";
            }
            catch (Exception ex)
            {
                //log the error but don't inform the user
                LoggingHelpers.RecordException(ex.Message, @"LogViewerViewingStatusError");
            }
        }

        private void ItmSearchTerm_Click(object sender, EventArgs e)
        {
            //if we're already in a search, then we cancel it
            if (_isFiltered)
                CancelSearch();
            //otherwise, start a new search
            else
                RunTitleSearch();
        }

        private void ItmGoToLine_Click(object sender, EventArgs e)
        {
            try
            {
                //the row count must be above zero to perform a line seek
                if (dgvMain.Rows.Count <= 0)
                    return;

                //use libbrhscgui to show an input form
                var ipt =
                    ObjectProvider.LibUi.showInputForm(@"Enter Row Number", @"Go To Row");

                //cancel string passed by the legacy library (libbrhscgui)
                if (ipt.txt == "!cancel=user")
                {
                    //do nothing (exit)
                }

                //check if the value was null
                else if (string.IsNullOrWhiteSpace(ipt.txt))

                    //alert the user to the error
                    UIMessages.Error(@"Nothing was entered",
                        @"Validation Error");

                //check if the value isn't numeric
                else if (!int.TryParse(ipt.txt, out _))

                    //alert the user to the error
                    UIMessages.Error(@"Specified value is not numeric",
                        @"Validation Error");

                //all validation checks succeeded, perform line seek
                else
                {
                    //loop through each row in the grid
                    foreach (DataGridViewRow row in dgvMain.Rows)
                    {
                        //index of the current row
                        var rowIndex = dgvMain.Rows.IndexOf(row);

                        //the index we're looking for
                        var seekTo = Convert.ToInt32(ipt.txt) - 1;

                        //skip to next row if we're not on the correct one
                        if (rowIndex != seekTo) continue;

                        //correct row; highlight the row
                        dgvMain.CurrentCell = row.Cells[0];

                        //exit operation
                        return;
                    }

                    //if the row isn't found, the loop will exit and arrive here;
                    //which will display the error message below.
                    UIMessages.Error(@"Couldn't find row '" + ipt.txt + @"'",
                        @"Validation Error");
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "LogFindLineError");

                //report the error to the user
                UIMessages.Error(ex.ToString());
            }
        }

        private void ItmBackup_Click(object sender, EventArgs e)
        {
            try
            {
                //ensure the logging directory exists
                if (Directory.Exists(LogDir))
                {
                    //the OK button must be pressed in the dialog
                    if (sfdBackup.ShowDialog() != DialogResult.OK)
                        return;

                    //if the file already exists, delete it
                    if (File.Exists(sfdBackup.FileName))
                        File.Delete(sfdBackup.FileName);

                    //create a new ZIP file of all the logs in the logging directory
                    ZipFile.CreateFromDirectory(LogDir, sfdBackup.FileName, CompressionLevel.Optimal, false);

                    //inform the user of the successful operation
                    UIMessages.Info(@"Successfully backed up logs to " + sfdBackup.FileName);
                }
                else
                    //alert the user
                    UIMessages.Error(
                        @"Could not backup logs due to no folder existing. This is a clear sign that no logs have been created, however you can check by clicking the Refresh button on the bottom left of this form.",
                        @"Validation Error");
            }
            catch (Exception ex)
            {
                //report the error to the user
                UIMessages.Error("An error occurred whilst backing up log files. Details:\n\n" + ex,
                    "IO Error");
            }
        }

        private void ItmCSV_Click(object sender, EventArgs e)
        {
            try
            {
                //ensure a log file is selected in the list
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
                        UIMessages.Error("Selected file does not exist",
                            "Validation Error");
                }
                else
                    UIMessages.Error("Nothing is selected",
                        "Validation Error");
            }
            catch (Exception ex)
            {
                //inform the user of the error
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
                        UIMessages.Error("Selected file does not exist",
                            "Validation Error");
                }
                else
                    UIMessages.Error("Nothing is selected",
                        "Validation Error");
            }
            catch (Exception ex)
            {
                //inform the user of the error
                UIMessages.Error("Error occurred whilst exporting your log file. Details:\n\n" + ex,
                    "IO Error");
            }
        }

        private void SessionIdSearch(string sessionId, bool directMatch = true)
        {
            try
            {
                //ensure the grid has records loaded
                if (dgvMain.Rows.Count <= 0) return;

                //ensure the 'SessionID' column exists
                if (SessionIdPresent())
                {
                    //the search rule dictates the matching pattern
                    var rule = directMatch
                        ? SearchRule.EqualsKey
                        : SearchRule.ContainsKey;

                    //the information to be used for initiating the grid search
                    var searchContext = new SearchData
                    {
                        SearchColumn = "SessionID",
                        SearchRule = rule,
                        SearchTable = _logRecords,
                        SearchTerm = sessionId
                    };

                    //utilise the common search framework to start a grid search
                    if (Search.RunTitleSearch(dgvMain, searchContext))
                    {
                        //set the grid status
                        _isFiltered = true;

                        //update the GUI to reflect a search being active
                        GuiSetStartSearch();
                    }
                    else
                        //if the common framework returned false, then an error occurred; cancel the search and reset the GUI.
                        CancelSearch();
                }
                else
                {
                    //inform the user of the error
                    UIMessages.Error("Couldn't find a valid 'SessionID' column. Have you got the correct log loaded?",
                        "Validation Error");

                    //if the common framework returned false, then an error occurred; cancel the search and reset the GUI.
                    CancelSearch();
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "LogSearchError");

                //alert the user
                UIMessages.Error(ex.ToString());

                //reset the GUI and clear the search
                CancelSearch();
            }
        }

        private void RunTitleSearch()
        {
            try
            {
                //the search will not work on a 0 row count
                if (dgvMain.Rows.Count <= 0) return;

                //utilise the common search framework to start a grid search
                if (Search.RunTitleSearch(dgvMain, _logRecords))
                {
                    //set the grid status
                    _isFiltered = true;

                    //update the GUI to reflect a search being active
                    GuiSetStartSearch();
                }
                else
                    //if the common framework returned false, then an error occurred; cancel the search and reset the GUI.
                    CancelSearch();
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "LogSearchError");

                //inform the user of the error
                UIMessages.Error(ex.ToString());

                //reset the GUI and clear the search
                CancelSearch();
            }
        }

        private void ItmThisSession_Click(object sender, EventArgs e)
        {
            //use the current SessionID (generated randomly on each launch) to filter the log
            SessionIdSearch(Strings.CurrentSessionId);
        }

        private bool SessionIdPresent()
        {
            //ensure the 'SessionID' column exists and that records are loaded into memory
            return _logRecords != null
                   && _logRecords.Columns.
                       Contains("SessionID");
        }

        private void ItmCancelSearch_Click(object sender, EventArgs e)
        {
            //if we already have a search active, cancel it; otherwise do nothing
            if (_isFiltered)
                CancelSearch();
        }
    }
}