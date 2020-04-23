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
            DoRefresh();
            tmrUpdateRef.Start();
        }

        private void tmrUpdateRef_Tick(object sender, EventArgs e)
        {
            DoRefresh();
        }

        private void DoRefresh()
        {
            RefreshCount++;
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
                tmrUpdateRef.Stop();
                btnTimer.Text = "Timer Off";
            }
            else
            {
                TimerRunning = true;
                tmrUpdateRef.Start();
                btnTimer.Text = "Timer On";
            }
        }
    }
}