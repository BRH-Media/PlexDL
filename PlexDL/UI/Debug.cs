using PlexDL.Common;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
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
            ExportTableCsv(GlobalTables.SectionsTable);
        }

        private void ExportTitles()
        {
            ExportTableCsv(GlobalTables.TitlesTable);
        }

        private void ExportFiltered()
        {
            if (Flags.IsFiltered)
                ExportTableCsv(GlobalTables.FilteredTable);
            else
                ShowError("Titles are not currently filtered");
        }

        private void ExportSeasons()
        {
            if (Flags.IsTVShow)
                ExportTableCsv(GlobalTables.SeasonsTable);
            else
                ShowError("PlexDL is not in TV Mode");
        }

        private void ExportEpisodes()
        {
            if (Flags.IsTVShow)
                ExportTableCsv(GlobalTables.EpisodesTable);
            else
                ShowError("PlexDL is not in TV Mode");
        }

        private void ExportTableCsv(DataTable table)
        {
            try
            {
                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        if (sfdExportCsv.ShowDialog() == DialogResult.OK)
                        {
                            string file = sfdExportCsv.FileName;
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