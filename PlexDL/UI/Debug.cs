using LogDel.Utilities;
using PlexDL.Common;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class Debug : Form
    {
        public int RefreshCount { get; set; } = 0;
        public bool TimerRunning { get; set; } = true;

        public Debug()
        {
            InitializeComponent();
            cbxExportFormat.SelectedIndex = 0;
        }

        private void ShowError(string error, string type = "Validation Error")
        {
            MessageBox.Show(error, type, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowInfo(string msg, string caption = "Message")
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportSections()
        {
            if (radModeTable.Checked)
                ProcessExport(GlobalTables.SectionsTable);
            else
                ProcessExport(GlobalViews.SectionsViewTable);
        }

        private void ExportMovies()
        {
            if (radModeTable.Checked)
                ProcessExport(GlobalTables.TitlesTable);
            else
                ProcessExport(GlobalViews.MoviesViewTable);
        }

        private void ExportTVTitles()
        {
            if (radModeTable.Checked)
                ProcessExport(GlobalTables.TitlesTable);
            else
                ProcessExport(GlobalViews.TvViewTable);
        }

        private void ExportTitles()
        {
            if (GlobalStaticVars.CurrentContentType == ContentType.TvShows)
                ExportTVTitles();
            else if (GlobalStaticVars.CurrentContentType == ContentType.Movies)
                ExportMovies();
        }

        private void ExportFiltered()
        {
            if (Flags.IsFiltered)
            {
                if (radModeTable.Checked)
                    ProcessExport(GlobalTables.FilteredTable);
                else
                    ProcessExport(GlobalViews.FilteredViewTable);
            }
            else
                ShowError("Titles are not currently filtered");
        }

        private void ExportSeasons()
        {
            if (GlobalStaticVars.CurrentContentType == ContentType.TvShows)
            {
                if (radModeTable.Checked)
                    ProcessExport(GlobalTables.SeasonsTable);
                else
                    ProcessExport(GlobalViews.SeasonsViewTable);
            }
            else
                ShowError("PlexDL is not in TV Mode");
        }

        private void ExportEpisodes()
        {
            if (GlobalStaticVars.CurrentContentType == ContentType.TvShows)
            {
                if (radModeTable.Checked)
                    ProcessExport(GlobalTables.EpisodesTable);
                else
                    ProcessExport(GlobalViews.EpisodesViewTable);
            }
            else
                ShowError("PlexDL is not in TV Mode");
        }

        private void ProcessExport(DataTable data)
        {
            if (Equals(cbxExportFormat.SelectedItem, "CSV"))
                ExportTableCsv(data);
            else if (Equals(cbxExportFormat.SelectedItem, "XML"))
                ExportTableXml(data);
            else if (Equals(cbxExportFormat.SelectedItem, "LOGDEL"))
                ExportTableLogdel(data);
            else if (Equals(cbxExportFormat.SelectedItem, "JSON"))
                ExportTableJson(data);
        }

        private void ExportTableCsv(DataTable table)
        {
            try
            {
                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        sfdExport.Filter = "CSV File|*.csv";
                        sfdExport.DefaultExt = "csv";
                        if (sfdExport.ShowDialog() == DialogResult.OK)
                        {
                            string file = sfdExport.FileName;
                            table.ToCSV(file);
                            ShowInfo("Success!");
                        }
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
                        sfdExport.Filter = "LOGDEL File|*.logdel";
                        sfdExport.DefaultExt = "logdel";
                        if (sfdExport.ShowDialog() == DialogResult.OK)
                        {
                            string file = sfdExport.FileName;
                            table.ToLogdel(file);
                            ShowInfo("Success!");
                        }
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
                        sfdExport.Filter = "JSON File|*.json";
                        sfdExport.DefaultExt = "json";
                        if (sfdExport.ShowDialog() == DialogResult.OK)
                        {
                            string file = sfdExport.FileName;
                            table.ToJson(file);
                            ShowInfo("Success!");
                        }
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
                        sfdExport.Filter = "XML File|*.xml";
                        sfdExport.DefaultExt = "xml";
                        if (sfdExport.ShowDialog() == DialogResult.OK)
                        {
                            string file = sfdExport.FileName;
                            table.ToXml(file);
                            ShowInfo("Success!");
                        }
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

        private void DoRefresh()
        {
            Type t = typeof(Flags);
            PropertyInfo[] fields = t.GetProperties();
            List<string[]> values = new List<string[]>();

            foreach (var p in fields)
            {
                string pValue = p.GetValue(null, null).ToString();
                string[] v = new string[] { p.Name, pValue };
                values.Add(v);
            }

            RenderFlags(values.ToArray());
            UpdateRefreshInt();
            UpdateRefreshCount();
        }

        private void RenderFlags(string[][] flags)
        {
            dgvGlobalFlags.DataSource = null;
            DataTable t = new DataTable();
            t.Columns.Add("Flag");
            t.Columns.Add("Value");

            foreach (string[] f in flags)
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

        private void UpdateRefreshInt()
        {
            if (RefreshCount < int.MaxValue)
                RefreshCount++;
            else
                RefreshCount = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var msg = MessageBox.Show("Are you sure? If you cancel debugging, you will need to restart PlexDL with the appropriate flags in order to resume.",
                "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msg == DialogResult.Yes)
            {
                Flags.IsDebug = false;
                Close();
            }
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            if (TimerRunning)
            {
                TimerRunning = false;
                tmrAutoRefresh.Stop();
                btnTimer.Text = "Auto-refresh Off";
            }
            else
            {
                TimerRunning = true;
                tmrAutoRefresh.Start();
                btnTimer.Text = "Auto-refresh On";
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
    }
}