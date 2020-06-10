using Markdig;
using PlexDL.AltoHTTP.Classes;
using PlexDL.WaitWindow;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Application = GitHubUpdater.API.Application;

namespace GitHubUpdater
{
    public partial class Update : Form
    {
        public Application UpdateData { get; set; }

        public Update()
        {
            InitializeComponent();
        }

        private void LoadUi()
        {
            if (UpdateData == null)
            {
                lblUpdateTitle.Text = @"Update Unavailable";
                Text = @"Error";
                btnDownloadUpdate.Enabled = false;
                btnDownloadUpdate.Text = @"Unavailable";
                browserChanges.DocumentText = @"";
            }
            else
            {
                var title = UpdateData.name;
                var changes = UpdateData.body;

                tlpDownloadCount.Visible = true;
                lblDownloadsValue.Text = NumberDownloads().ToString();

                var changesHtml = @"<h4>Changelog information is unavailable. Please ask the vendor for more information.</h4>";
                lblUpdateTitle.Text = !string.IsNullOrEmpty(title) ? title : @"Update Available";

                if (!string.IsNullOrEmpty(changes))
                    changesHtml = Markdown.ToHtml(changes);

                var documentHtml = $@"<!DOCTYPE html>
<html>
    <head>
        <meta charset=""utf-8"">
        <style>
            body {{
                font-family: sans-serif;
            }}
        </style>
    </head>
    <body>
        {changesHtml}
    </body>
</html>";

                browserChanges.DocumentText = documentHtml;
            }

            //recenter the title (text has been changed so the size has too)
            CenterTitle();
        }

        private void Update_Load(object sender, EventArgs e)
        {
            LoadUi();
        }

        private void CenterTitle()
        {
            //calculate appropriate x-axis position
            var x = (Width / 2) - (lblUpdateTitle.Width / 2);

            //maintain existing y-axis position
            var y = lblUpdateTitle.Location.Y;

            //construct new location
            var newLocation = new Point(x, y);

            //apply new location to label
            lblUpdateTitle.Location = newLocation;
        }

        private void BtnMaybeLater_Click(object sender, EventArgs e)
        {
        }

        private void BtnDownloadUpdate_Click(object sender, EventArgs e)
        {
            Download();
        }

        private int NumberDownloads()
        {
            return UpdateData.assets.Sum(a => a.download_count);
        }

        private void Download()
        {
            try
            {
                var dir = (string)WaitWindow.Show(DownloadWorker, @"Downloading update file(s)");
                var msg = MessageBox.Show(@"Done! Open download location?", @"Question",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (msg == DialogResult.Yes)
                    Process.Start(dir);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading your update files\n{ex}", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DownloadWorker(object sender, WaitWindowEventArgs e)
        {
            var dir = $@"update_files\{UpdateData.id}";
            var dl = new List<HttpDownloader>();
            foreach (var a in UpdateData.assets)
            {
                var dirA = $@"{dir}\{a.id}";
                if (!Directory.Exists(dirA))
                    Directory.CreateDirectory(dirA);
                var uri = a.browser_download_url;
                var file = $@"{dirA}\{a.name}";

                //only download it if the file doesn't already exist
                if (File.Exists(file)) continue;

                var engine = new HttpDownloader(uri, file);
                dl.Add(engine);
                dl[dl.IndexOf(engine)].StartAsync();
            }

            e.Result = dir;
        }
    }
}