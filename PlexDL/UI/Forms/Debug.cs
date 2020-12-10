using LogDel.Utilities.Export;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using UIHelpers;

// ReSharper disable InvertIf

#pragma warning disable 1591

namespace PlexDL.UI.Forms
{
    public partial class Debug : DoubleBufferedForm
    {
        public Debug()
        {
            //Windows Forms designer setup
            InitializeComponent();

            //select the first item in the list by default
            cbxExportFormat.SelectedIndex = 0;
        }

        /// <summary>
        /// How many UI refreshes have happened?
        /// </summary>
        public int RefreshCount { get; set; }

        /// <summary>
        /// Is the Refresh timer running?
        /// </summary>
        public bool TimerRunning { get; set; } = true;

        /// <summary>
        /// Library sections export processor
        /// </summary>
        private void ExportSections()
        {
            //use the generic exporter and the PlexDL data provider framework
            ProcessExport(radModeTable.Checked
                ? DataProvider.SectionsProvider.GetRawTable()
                : DataProvider.SectionsProvider.GetViewTable());
        }

        /// <summary>
        /// Plex Movies export processor
        /// </summary>
        private void ExportMovies()
        {
            //use the generic exporter and the PlexDL data provider framework
            ProcessExport(radModeTable.Checked
                ? DataProvider.TitlesProvider.GetRawTable()
                : DataProvider.TitlesProvider.GetViewTable());
        }

        /// <summary>
        /// Plex TV titles export processor
        /// </summary>
        private void ExportTvTitles()
        {
            //use the generic exporter and the PlexDL data provider framework
            ProcessExport(radModeTable.Checked
                ? DataProvider.TitlesProvider.GetRawTable()
                : DataProvider.TitlesProvider.GetViewTable());
        }

        /// <summary>
        /// Generic titles export processor (music, TV and movies)
        /// </summary>
        private void ExportTitles()
        {
            //current content type is whatever is currently loaded into the main window
            switch (ObjectProvider.CurrentContentType)
            {
                //only TV content is loaded
                case ContentType.TvShows:
                    ExportTvTitles();
                    break;

                //only movies are loaded
                case ContentType.Movies:
                    ExportMovies();
                    break;

                //only music is loaded
                case ContentType.Music:
                    break;

                //an invalid enum member will trigger an exception
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ExportFiltered()
        {
            ////the application MUST be in the search state (current content type grid is filtered); exporting will be misaligned and corrupt otherwise
            if (Flags.IsFiltered)

                //use the generic exporter and the PlexDL data provider framework
                ProcessExport(radModeTable.Checked
                    ? DataProvider.FilteredProvider.GetRawTable()
                    : DataProvider.FilteredProvider.GetViewTable());
            else
                //inform the user of the problem (invalid mode)
                UIMessages.Error("Titles are not currently filtered");
        }

        private void ExportSeasons()
        {
            //the application MUST be set to the correct mode; exporting will be misaligned and corrupt otherwise
            if (ObjectProvider.CurrentContentType == ContentType.TvShows)

                //use the generic exporter and the PlexDL data provider framework
                ProcessExport(radModeTable.Checked
                    ? DataProvider.SeasonsProvider.GetRawTable()
                    : DataProvider.SeasonsProvider.GetViewTable());
            else
                //inform the user of the problem (invalid mode)
                UIMessages.Error("PlexDL is not in TV Mode");
        }

        private void ExportEpisodes()
        {
            //the application MUST be set to the correct mode; exporting will be misaligned and corrupt otherwise
            if (ObjectProvider.CurrentContentType == ContentType.TvShows)

                //use the generic exporter and the PlexDL data provider framework
                ProcessExport(radModeTable.Checked
                    ? DataProvider.EpisodesProvider.GetRawTable()
                    : DataProvider.EpisodesProvider.GetViewTable());
            else
                //inform the user of the problem (invalid mode)
                UIMessages.Error("PlexDL is not in TV Mode");
        }

        private void ExportAlbums()
        {
            //the application MUST be set to the correct mode; exporting will be misaligned and corrupt otherwise
            if (ObjectProvider.CurrentContentType == ContentType.Music)

                //use the generic exporter and the PlexDL data provider framework
                ProcessExport(radModeTable.Checked
                    ? DataProvider.AlbumsProvider.GetRawTable()
                    : DataProvider.AlbumsProvider.GetViewTable());
            else
                //inform the user of the problem (invalid mode)
                UIMessages.Error("PlexDL is not in Music Mode");
        }

        private void ExportTracks()
        {
            if (ObjectProvider.CurrentContentType == ContentType.Music)

                //use the generic exporter and the PlexDL data provider framework
                ProcessExport(radModeTable.Checked
                    ? DataProvider.TracksProvider.GetRawTable()
                    : DataProvider.TracksProvider.GetViewTable());
            else
                //inform the user of the problem (invalid mode)
                UIMessages.Error("PlexDL is not in Music Mode");
        }

        /// <summary>
        /// Universal export handler; avoids messy function calls.
        /// </summary>
        /// <param name="data"></param>
        private void ProcessExport(DataTable data)
        {
            //what format are we exporting?
            switch (cbxExportFormat.SelectedItem)
            {
                //Comma-separated values
                case "CSV":
                    ExportTableCsv(data);
                    break;

                //eXtensible Markup Language
                case "XML":
                    ExportTableXml(data);
                    break;

                //Logging-delimited
                case "LOGDEL":
                    ExportTableLogdel(data);
                    break;

                //JavaScript Object Notation
                case "JSON":
                    ExportTableJson(data);
                    break;
            }
        }

        /// <summary>
        /// Handles exporting to a comma-separated values file; will prompt the user with a 'Save As' dialog.
        /// </summary>
        /// <param name="table">The data to export; null checks are enforced.</param>
        private void ExportTableCsv(DataTable table)
        {
            try
            {
                //the table mustn't be null; we cannot work on data that doesn't exist
                if (table != null)
                {
                    //the table must have data inside it; we cannot work on data that doesn't exist
                    if (table.Rows.Count > 0)
                    {
                        //dialog values
                        sfdExport.Filter = @"CSV File|*.csv";
                        sfdExport.DefaultExt = "csv";

                        //show the dialog and ensure the user pressed 'OK'
                        if (sfdExport.ShowDialog() != DialogResult.OK)
                            return;

                        //the path of the file to write to/create
                        var file = sfdExport.FileName;

                        //use the export extensions provider to save the data to a CSV file
                        table.ToCsv(file);

                        //inform the user
                        UIMessages.Info("Success!");
                    }
                    else
                        //inform the user of the problem (no data; but not null)
                        UIMessages.Error("Couldn't export; table has no rows.");
                }
                else
                    //inform the user of the problem (table isn't set)
                    UIMessages.Error("Couldn't export; table is null.");
            }
            catch (Exception ex)
            {
                //log the problem
                LoggingHelpers.RecordException(ex.Message, @"DebugExportCSVError");

                //inform the user of the problem (unhandled exception)
                UIMessages.Error("Error occurred whilst exporting table:\n\n" + ex);
            }
        }

        /// <summary>
        /// Handles exporting to a logging-delimited file; will prompt the user with a 'Save As' dialog.
        /// </summary>
        /// <param name="table">The data to export; null checks are enforced.</param>
        private void ExportTableLogdel(DataTable table)
        {
            try
            {
                //the table mustn't be null; we cannot work on data that doesn't exist
                if (table != null)
                {
                    //the table must have data inside it; we cannot work on data that doesn't exist
                    if (table.Rows.Count > 0)
                    {
                        //dialog values
                        sfdExport.Filter = @"LOGDEL File|*.logdel";
                        sfdExport.DefaultExt = "logdel";

                        //show the dialog and ensure the user pressed 'OK'
                        if (sfdExport.ShowDialog() != DialogResult.OK)
                            return;

                        //the path of the file to write to/create
                        var file = sfdExport.FileName;

                        //use the export extensions provider to save the data to a LOGDEL file
                        table.ToLogdel(file);

                        //inform the user
                        UIMessages.Info("Success!");
                    }
                    else
                        //inform the user of the problem (no data; but not null)
                        UIMessages.Error("Couldn't export; table has no rows.");
                }
                else
                    //inform the user of the problem (table isn't set)
                    UIMessages.Error("Couldn't export; table is null.");
            }
            catch (Exception ex)
            {
                //log the problem
                LoggingHelpers.RecordException(ex.Message, @"DebugExportLDError");

                //inform the user of the problem (unhandled exception)
                UIMessages.Error("Error occurred whilst exporting table:\n\n" + ex);
            }
        }

        /// <summary>
        /// Handles exporting to a JavaScript object notation file; will prompt the user with a 'Save As' dialog.
        /// </summary>
        /// <param name="table">The data to export; null checks are enforced.</param>
        private void ExportTableJson(DataTable table)
        {
            try
            {
                //the table mustn't be null; we cannot work on data that doesn't exist
                if (table != null)
                {
                    //the table must have data inside it; we cannot work on data that doesn't exists
                    if (table.Rows.Count > 0)
                    {
                        //dialog values
                        sfdExport.Filter = @"JSON File|*.json";
                        sfdExport.DefaultExt = "json";

                        //show the dialog and ensure the user pressed 'OK'
                        if (sfdExport.ShowDialog() != DialogResult.OK)
                            return;

                        //the path of the file to write to/create
                        var file = sfdExport.FileName;

                        //use the export extensions provider to save the data to a JSON file
                        table.ToJson(file);

                        //inform the user
                        UIMessages.Info("Success!");
                    }
                    else
                        //inform the user of the problem (no data; but not null)
                        UIMessages.Error("Couldn't export; table has no rows.");
                }
                else
                    //inform the user of the problem (table isn't set)
                    UIMessages.Error("Couldn't export; table is null.");
            }
            catch (Exception ex)
            {
                //log the problem
                LoggingHelpers.RecordException(ex.Message, @"DebugExportJSONError");

                //inform the user of the problem (unhandled exception)
                UIMessages.Error("Error occurred whilst exporting table:\n\n" + ex);
            }
        }

        /// <summary>
        /// Handles exporting to an eXtensible markup language file; will prompt the user with a 'Save As' dialog.
        /// </summary>
        /// <param name="table">The data to export; null checks are enforced.</param>
        private void ExportTableXml(DataTable table)
        {
            try
            {
                //the table mustn't be null; we cannot work on data that doesn't exist
                if (table != null)
                {
                    //the table must have data inside it; we cannot work on data that doesn't exist
                    if (table.Rows.Count > 0)
                    {
                        //dialog values
                        sfdExport.Filter = @"XML File|*.xml";
                        sfdExport.DefaultExt = "xml";

                        //show the dialog and ensure the user pressed 'OK'
                        if (sfdExport.ShowDialog() != DialogResult.OK)
                            return;

                        //the path of the file to write to/create
                        var file = sfdExport.FileName;

                        //use the export extensions provider to save the data to an XML file
                        table.ToXml(file);

                        //inform the user
                        UIMessages.Info("Success!");
                    }
                    else
                        //inform the user of the problem (no data; but not null)
                        UIMessages.Error("Couldn't export; table has no rows.");
                }
                else
                    //inform the user of the problem (table isn't set)
                    UIMessages.Error("Couldn't export; table is null.");
            }
            catch (Exception ex)
            {
                //log the problem
                LoggingHelpers.RecordException(ex.Message, @"DebugExportXMLError");

                //inform the user of the problem (unhandled exception)
                UIMessages.Error("Error occurred whilst exporting table:\n\n" + ex);
            }
        }

        private void Startup()
        {
            try
            {
                //reset counter
                RefreshCount = 0;

                //poll rate GUI setup
                UpdatePollRate();

                //initial value refresh
                DoRefresh();

                //start the automatic refresh timer
                tmrAutoRefresh.Start();
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"DebugStartupError");

                //inform the user
                UIMessages.Error($@"Debug monitor startup error: {ex.Message}");
            }
        }

        private void UpdateTimerPollInterval()
        {
            //based on the numeric value the user enters,
            //update the polling interval
            tmrAutoRefresh.Interval = (int)numPollRateValue.Value;
        }

        private void UpdatePollRate()
        {
            //default poll rate is dependent on the timer
            numPollRateValue.Value = tmrAutoRefresh.Interval;
        }

        private void UpdateRefreshCount()
        {
            //update counter GUI to internal counter value
            lblRefreshCountValue.Text = RefreshCount.ToString();
        }

        private void ResetRefreshCounter()
        {
            //reset internal counter
            RefreshCount = 0;

            //update counter GUI
            UpdateRefreshCount();
        }

        private void UpdateRefreshInt()
        {
            //this will avoid overflow problems
            if (RefreshCount < int.MaxValue)
                //the integer has not yet overflowed its upper bound
                RefreshCount++;
            else
                //the integer is about to reach its upper bound; reset it back to 0
                RefreshCount = 0;
        }

        private void DoRefresh()
        {
            //the type of the global Flags registry class
            var t = typeof(Flags);

            //get all properties in the class
            var fields = t.GetProperties();

            //render the properties collected above to the 'Global Flags' grid
            RenderFlags((from p in fields
                         let pValue = p.GetValue(null, null).ToString()
                         select new[] { p.Name, pValue })
                .ToArray());

            //updates the internal counter
            UpdateRefreshInt();

            //updates the GUI counter
            UpdateRefreshCount();
        }

        private void RenderFlags(IEnumerable<string[]> flags)
        {
            //clear the current grid
            dgvGlobalFlags.DataSource = null;

            //construct a new DataTable to use as a data source
            var t = new DataTable();

            //the column that denotes the flag name
            t.Columns.Add("Flag");

            //the column that denotes the flag value
            t.Columns.Add("Value");

            //loop through each flag in the provided array
            foreach (object[] f in flags)

                //make sure the flag object is of the same length as the
                //allowed column length
                if (t.Columns.Count == f.Length)

                    //add the row; checks succeeded
                    t.Rows.Add(f);

            //apply the newly constructed table to the grid
            dgvGlobalFlags.DataSource = t;
        }

        private bool CancelDebugging(bool doClose = true)
        {
            //the UIMessages handler will return true for 'Yes' and false for 'No'
            var msg = UIMessages.Question(
                "Are you sure? If you cancel debugging, " +
                "you will need to restart PlexDL with the appropriate flags in order to resume.");

            //the user must have clicked 'No'; cancel operation
            if (!msg)
                return false;

            //the user must have clicked 'Yes'; disable debugging,
            Flags.IsDebug = false;

            //and close the form.
            if (doClose)
                Close();

            //default
            return true;
        }

        private void TimerToggle()
        {
            //timer on-off toggle check
            if (TimerRunning)
            {
                //disable timer toggle
                TimerRunning = false;

                //stop the timer operation
                tmrAutoRefresh.Stop();

                //reset GUI text
                btnTimer.Text = @"Auto-refresh Off";
            }
            else
            {
                //enable timer toggle
                TimerRunning = true;

                //restart the timer operation
                tmrAutoRefresh.Start();

                //reset GUI text
                btnTimer.Text = @"Auto-refresh On";
            }
        }

        #region Event Handlers

        private void Debug_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Flags.IsMsgAlreadyShown)
            {
                //display question dialog
                var debugChoice = CancelDebugging(false);

                //cancel
                if (!debugChoice)
                    e.Cancel = true;
                else
                {
                    Flags.IsMsgAlreadyShown = true;
                    Close();
                    Flags.IsMsgAlreadyShown = false;
                }
            }
        }

        private void Debug_Load(object sender, EventArgs e) => Startup();

        private void TmrAutoRefresh_Tick(object sender, EventArgs e) => DoRefresh();

        private void BtnRefresh_Click(object sender, EventArgs e) => DoRefresh();

        private void BtnCancel_Click(object sender, EventArgs e) => CancelDebugging();

        private void NumPollRateValue_ValueChanged(object sender, EventArgs e) => UpdateTimerPollInterval();

        private void BtnTimer_Click(object sender, EventArgs e) => TimerToggle();

        private void BtnExportSections_Click(object sender, EventArgs e) => ExportSections();

        private void BtnExportTitles_Click(object sender, EventArgs e) => ExportTitles();

        private void BtnExportFiltered_Click(object sender, EventArgs e) => ExportFiltered();

        private void BtnExportSeasons_Click(object sender, EventArgs e) => ExportSeasons();

        private void BtnExportEpisodes_Click(object sender, EventArgs e) => ExportEpisodes();

        private void BtnExportAlbums_Click(object sender, EventArgs e) => ExportAlbums();

        private void BtnExportTracks_Click(object sender, EventArgs e) => ExportTracks();

        private void BtnResetRefreshCounter_Click(object sender, EventArgs e) => ResetRefreshCounter();

        #endregion Event Handlers
    }
}