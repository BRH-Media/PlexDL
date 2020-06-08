using LogDel.Utilities;
using PlexDL.Common;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class Debug : Form
    {
        public int RefreshCount { get; set; }
        public bool TimerRunning { get; set; } = true;

        public Debug()
        {
            InitializeComponent();
            cbxExportFormat.SelectedIndex = 0;
        }

        private static void ShowError(string error, string type = "Validation Error")
        {
            MessageBox.Show(error, type, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void ShowInfo(string msg, string caption = "Message")
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportSections()
        {
            ProcessExport(radModeTable.Checked ? GlobalTables.SectionsTable : GlobalViews.SectionsViewTable);
        }

        private void ExportMovies()
        {
            ProcessExport(radModeTable.Checked ? GlobalTables.TitlesTable : GlobalViews.MoviesViewTable);
        }

        private void ExportTvTitles()
        {
            ProcessExport(radModeTable.Checked ? GlobalTables.TitlesTable : GlobalViews.TvViewTable);
        }

        private void ExportTitles()
        {
            switch (GlobalStaticVars.CurrentContentType)
            {
                case ContentType.TvShows:
                    ExportTvTitles();
                    break;

                case ContentType.Movies:
                    ExportMovies();
                    break;

                case ContentType.Music:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ExportFiltered()
        {
            if (Flags.IsFiltered)
            {
                ProcessExport(radModeTable.Checked ? GlobalTables.FilteredTable : GlobalViews.FilteredViewTable);
            }
            else
                ShowError("Titles are not currently filtered");
        }

        private void ExportSeasons()
        {
            if (GlobalStaticVars.CurrentContentType == ContentType.TvShows)
            {
                ProcessExport(radModeTable.Checked ? GlobalTables.SeasonsTable : GlobalViews.SeasonsViewTable);
            }
            else
                ShowError("PlexDL is not in TV Mode");
        }

        private void ExportEpisodes()
        {
            if (GlobalStaticVars.CurrentContentType == ContentType.TvShows)
            {
                ProcessExport(radModeTable.Checked ? GlobalTables.EpisodesTable : GlobalViews.EpisodesViewTable);
            }
            else
                ShowError("PlexDL is not in TV Mode");
        }

        private void ExportAlbums()
        {
            if (GlobalStaticVars.CurrentContentType == ContentType.Music)
            {
                ProcessExport(radModeTable.Checked ? GlobalTables.AlbumsTable : GlobalViews.AlbumViewTable);
            }
            else
                ShowError("PlexDL is not in Music Mode");
        }

        private void ExportTracks()
        {
            if (GlobalStaticVars.CurrentContentType == ContentType.Music)
            {
                ProcessExport(radModeTable.Checked ? GlobalTables.TracksTable : GlobalViews.TracksViewTable);
            }
            else
                ShowError("PlexDL is not in Music Mode");
        }

        private void ProcessExport(DataTable data)
        {
            switch (cbxExportFormat.SelectedItem)
            {
                case "CSV":
                    ExportTableCsv(data);
                    break;

                case "XML":
                    ExportTableXml(data);
                    break;

                case "LOGDEL":
                    ExportTableLogdel(data);
                    break;

                case "JSON":
                    ExportTableJson(data);
                    break;
            }
        }

        private void ExportTableCsv(DataTable table)
        {
            try
            {
                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        sfdExport.Filter = @"CSV File|*.csv";
                        sfdExport.DefaultExt = "csv";

                        if (sfdExport.ShowDialog() != DialogResult.OK) return;

                        var file = sfdExport.FileName;
                        table.ToCSV(file);
                        ShowInfo("Success!");
                    }
                    else
                        ShowError("Couldn't export; table has no rows.");
                }
                else
                    ShowError("Couldn't export; table is null.");
            }
            catch (Exception ex)
            {
                ShowError("Error occurred whilst exporting table:\n\n" + ex);
            }
        }

        private void ExportTableLogdel(DataTable table)
        {
            try
            {
                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        sfdExport.Filter = @"LOGDEL File|*.logdel";
                        sfdExport.DefaultExt = "logdel";

                        if (sfdExport.ShowDialog() != DialogResult.OK) return;

                        var file = sfdExport.FileName;
                        table.ToLogdel(file);
                        ShowInfo("Success!");
                    }
                    else
                        ShowError("Couldn't export; table has no rows.");
                }
                else
                    ShowError("Couldn't export; table is null.");
            }
            catch (Exception ex)
            {
                ShowError("Error occurred whilst exporting table:\n\n" + ex);
            }
        }

        private void ExportTableJson(DataTable table)
        {
            try
            {
                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        sfdExport.Filter = @"JSON File|*.json";
                        sfdExport.DefaultExt = "json";

                        if (sfdExport.ShowDialog() != DialogResult.OK) return;

                        var file = sfdExport.FileName;
                        table.ToJson(file);
                        ShowInfo("Success!");
                    }
                    else
                        ShowError("Couldn't export; table has no rows.");
                }
                else
                    ShowError("Couldn't export; table is null.");
            }
            catch (Exception ex)
            {
                ShowError("Error occurred whilst exporting table:\n\n" + ex);
            }
        }

        private void ExportTableXml(DataTable table)
        {
            try
            {
                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        sfdExport.Filter = @"XML File|*.xml";
                        sfdExport.DefaultExt = "xml";

                        if (sfdExport.ShowDialog() != DialogResult.OK) return;

                        var file = sfdExport.FileName;
                        table.ToXml(file);
                        ShowInfo("Success!");
                    }
                    else
                        ShowError("Couldn't export; table has no rows.");
                }
                else
                    ShowError("Couldn't export; table is null.");
            }
            catch (Exception ex)
            {
                ShowError("Error occurred whilst exporting table:\n\n" + ex);
            }
        }

        private void Debug_Load(object sender, EventArgs e)
        {
            RefreshCount = 0;
            UpdatePollRate();
            DoRefresh();
            tmrAutoRefresh.Start();
        }

        private void UpdatePollRate()
        {
            numPollRateValue.Value = tmrAutoRefresh.Interval;
        }

        private void TmrAutoRefresh_Tick(object sender, EventArgs e)
        {
            DoRefresh();
        }

        private void UpdateRefreshCount()
        {
            lblRefreshCountValue.Text = RefreshCount.ToString();
        }

        private void ResetRefreshCounter()
        {
            RefreshCount = 0;
            UpdateRefreshCount();
        }

        private void UpdateRefreshInt()
        {
            if (RefreshCount < int.MaxValue)
                RefreshCount++;
            else
                RefreshCount = 0;
        }

        private void DoRefresh()
        {
            var t = typeof(Flags);
            var fields = t.GetProperties();

            RenderFlags((from p in fields let pValue = p.GetValue(null, null).ToString() select new[] { p.Name, pValue }).ToArray());
            UpdateRefreshInt();
            UpdateRefreshCount();
        }

        private void RenderFlags(IEnumerable<string[]> flags)
        {
            dgvGlobalFlags.DataSource = null;
            var t = new DataTable();
            t.Columns.Add("Flag");
            t.Columns.Add("Value");

            foreach (object[] f in flags)
            {
                if (t.Columns.Count == f.Length)
                    t.Rows.Add(f);
            }

            dgvGlobalFlags.DataSource = t;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DoRefresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var msg = MessageBox.Show(@"Are you sure? If you cancel debugging, you will need to restart PlexDL with the appropriate flags in order to resume.",
                @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msg != DialogResult.Yes) return;
            Flags.IsDebug = false;
            Close();
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            if (TimerRunning)
            {
                TimerRunning = false;
                tmrAutoRefresh.Stop();
                btnTimer.Text = @"Auto-refresh Off";
            }
            else
            {
                TimerRunning = true;
                tmrAutoRefresh.Start();
                btnTimer.Text = @"Auto-refresh On";
            }
        }

        private void tlpDebug_Paint(object sender, PaintEventArgs e)
        {
        }

        private void numPollRateValue_ValueChanged(object sender, EventArgs e)
        {
            tmrAutoRefresh.Interval = (int)numPollRateValue.Value;
        }

        private void btnExportSections_Click(object sender, EventArgs e)
        {
            ExportSections();
        }

        private void btnExportTitles_Click(object sender, EventArgs e)
        {
            ExportTitles();
        }

        private void btnExportFiltered_Click(object sender, EventArgs e)
        {
            ExportFiltered();
        }

        private void btnExportSeasons_Click(object sender, EventArgs e)
        {
            ExportSeasons();
        }

        private void btnExportEpisodes_Click(object sender, EventArgs e)
        {
            ExportEpisodes();
        }

        private void btnExportAlbums_Click(object sender, EventArgs e)
        {
            ExportAlbums();
        }

        private void btnExportTracks_Click(object sender, EventArgs e)
        {
            ExportTracks();
        }

        private void btnResetRefreshCounter_Click(object sender, EventArgs e)
        {
            ResetRefreshCounter();
        }
    }
}