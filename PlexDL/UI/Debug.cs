using LogDel.Utilities;
using PlexDL.Common;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.UI
{
    public partial class Debug : DoubleBufferedForm
    {
        public Debug()
        {
            InitializeComponent();
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

        private void ExportSections()
        {
            ProcessExport(radModeTable.Checked ? DataProvider.SectionsProvider.GetRawTable() : DataProvider.SectionsProvider.GetViewTable());
        }

        private void ExportMovies()
        {
            ProcessExport(radModeTable.Checked ? DataProvider.TitlesProvider.GetRawTable() : DataProvider.TitlesProvider.GetViewTable());
        }

        private void ExportTvTitles()
        {
            ProcessExport(radModeTable.Checked ? DataProvider.TitlesProvider.GetRawTable() : DataProvider.TitlesProvider.GetViewTable());
        }

        private void ExportTitles()
        {
            switch (ObjectProvider.CurrentContentType)
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
                ProcessExport(radModeTable.Checked ? DataProvider.FilteredProvider.GetRawTable() : DataProvider.FilteredProvider.GetViewTable());
            else
                UIMessages.Error("Titles are not currently filtered");
        }

        private void ExportSeasons()
        {
            if (ObjectProvider.CurrentContentType == ContentType.TvShows)
                ProcessExport(radModeTable.Checked ? DataProvider.SeasonsProvider.GetRawTable() : DataProvider.SeasonsProvider.GetViewTable());
            else
                UIMessages.Error("PlexDL is not in TV Mode");
        }

        private void ExportEpisodes()
        {
            if (ObjectProvider.CurrentContentType == ContentType.TvShows)
                ProcessExport(radModeTable.Checked ? DataProvider.EpisodesProvider.GetRawTable() : DataProvider.EpisodesProvider.GetViewTable());
            else
                UIMessages.Error("PlexDL is not in TV Mode");
        }

        private void ExportAlbums()
        {
            if (ObjectProvider.CurrentContentType == ContentType.Music)
                ProcessExport(radModeTable.Checked ? DataProvider.AlbumsProvider.GetRawTable() : DataProvider.AlbumsProvider.GetViewTable());
            else
                UIMessages.Error("PlexDL is not in Music Mode");
        }

        private void ExportTracks()
        {
            if (ObjectProvider.CurrentContentType == ContentType.Music)
                ProcessExport(radModeTable.Checked ? DataProvider.TracksProvider.GetRawTable() : DataProvider.TracksProvider.GetViewTable());
            else
                UIMessages.Error("PlexDL is not in Music Mode");
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
                        UIMessages.Info("Success!");
                    }
                    else
                    {
                        UIMessages.Error("Couldn't export; table has no rows.");
                    }
                }
                else
                {
                    UIMessages.Error("Couldn't export; table is null.");
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error("Error occurred whilst exporting table:\n\n" + ex);
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
                        UIMessages.Info("Success!");
                    }
                    else
                    {
                        UIMessages.Error("Couldn't export; table has no rows.");
                    }
                }
                else
                {
                    UIMessages.Error("Couldn't export; table is null.");
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error("Error occurred whilst exporting table:\n\n" + ex);
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
                        UIMessages.Info("Success!");
                    }
                    else
                    {
                        UIMessages.Error("Couldn't export; table has no rows.");
                    }
                }
                else
                {
                    UIMessages.Error("Couldn't export; table is null.");
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error("Error occurred whilst exporting table:\n\n" + ex);
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
                        UIMessages.Info("Success!");
                    }
                    else
                    {
                        UIMessages.Error("Couldn't export; table has no rows.");
                    }
                }
                else
                {
                    UIMessages.Error("Couldn't export; table is null.");
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error("Error occurred whilst exporting table:\n\n" + ex);
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

            RenderFlags((from p in fields let pValue = p.GetValue(null, null).ToString() select new[] { p.Name, pValue })
                .ToArray());
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
                if (t.Columns.Count == f.Length)
                    t.Rows.Add(f);

            dgvGlobalFlags.DataSource = t;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            DoRefresh();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            var msg = UIMessages.Question(
                @"Are you sure? If you cancel debugging, you will need to restart PlexDL with the appropriate flags in order to resume.");

            if (!msg) return;

            Flags.IsDebug = false;
            Close();
        }

        private void BtnTimer_Click(object sender, EventArgs e)
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

        private void NumPollRateValue_ValueChanged(object sender, EventArgs e)
        {
            tmrAutoRefresh.Interval = (int)numPollRateValue.Value;
        }

        private void BtnExportSections_Click(object sender, EventArgs e)
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