using PlexDL.Common;
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

        private void Debug_Load(object sender, EventArgs e)
        {
            RefreshCount = 0;
            UpdatePollRate();
            DoRefresh();
            tmrAutoRefresh.Start();
        }

        private void UpdatePollRate()
        {
            lblPollRateValue.Text = tmrAutoRefresh.Interval.ToString() + "ms";
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
            Flags.IsDebug = false;
            Close();
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
    }
}