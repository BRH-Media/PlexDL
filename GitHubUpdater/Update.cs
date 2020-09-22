using GitHubUpdater.API;
using GitHubUpdater.DownloadManager;
using Markdig;
using PlexDL.WaitWindow;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GitHubUpdater
{
    public partial class Update : Form
    {
        public UpdateResponse AppUpdate { get; set; } = null;

        public string UpdateDirectory { get; set; } = @"";

        public Update()
        {
            InitializeComponent();
        }

        private void NoUpdate()
        {
            lblUpdateTitle.Text = @"Update Unavailable";
            lblYourVersionValue.Text = @"Unknown";
            Text = @"Error";
            btnDownloadUpdate.Enabled = false;
            btnDownloadUpdate.Text = @"Unavailable";
            browserChanges.DocumentText = @"";
        }

        private void LoadUi()
        {
            if (AppUpdate == null)
            {
                NoUpdate();
            }
            else
            {
                if (AppUpdate.CurrentVersion == null || AppUpdate.UpdateData == null)
                    NoUpdate();
                else
                {
                    var dir = $@"{Globals.UpdateRootDir}\{AppUpdate.UpdateData.id}";

                    //set global
                    UpdateDirectory = dir;

                    var title = AppUpdate.UpdateData.name;
                    var changes = AppUpdate.UpdateData.body;
                    var style = new Stylesheet();
                    var css = style.CssText;

                    var changesHtml = @"<h4>Changelog information is unavailable. Please ask the vendor for more information.</h4>";
                    lblUpdateTitle.Text = !string.IsNullOrEmpty(title) ? title : @"Update Available";

                    if (!string.IsNullOrEmpty(changes))
                        changesHtml = Markdown.ToHtml(changes);

                    var documentHtml = $@"<!DOCTYPE html>
<html>
    <head>
        <meta charset=""utf-8"">
        <meta http-equiv=""X-UA-Compatible"" content=""IE=10"" />
        <style>
            {css}
        </style>
    </head>
    <body>
        {changesHtml}
        <p>Downloads: {NumberDownloads()}</p>
    </body>
</html>";

                    //apply decoded markdown-HTML
                    browserChanges.DocumentText = documentHtml;

                    //set current version
                    lblYourVersionValue.Text = $"v{AppUpdate.CurrentVersion}";
                }

                //recenter the title (text has been changed so the size has too)
                CenterTitle();
            }
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
            return AppUpdate.UpdateData.assets.Sum(a => a.download_count);
        }

        private void Download()
        {
            try
            {
                var status = (ReturnStatus)WaitWindow.Show(DownloadWorker, @"Downloading update files");

                switch (status)
                {
                    case ReturnStatus.Errored:
                        MessageBox.Show(@"An unknown error occurred whilst attempting to download one or more update files", @"Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                        break;

                    case ReturnStatus.NullJob:
                        MessageBox.Show(@"One or more download jobs were invalid; valid jobs have been completed.", @"Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        break;

                    case ReturnStatus.Downloaded:
                        var msg = MessageBox.Show(@"Done! Open download location?", @"Question",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (msg == DialogResult.Yes)
                            Process.Start(UpdateDirectory);

                        break;

                    case ReturnStatus.Cancelled:
                        //don't do anything
                        break;

                    case ReturnStatus.Unknown:
                        MessageBox.Show(@"Download worker returned the 'unknown' job status indicator.", @"Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;

                    default:
                        MessageBox.Show(@"Download worker returned an invalid job status indicator.", @"Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading your update files\n\n{ex}", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //always close form at the end of the download procedure
            Close();
        }

        private void DownloadWorker(object sender, WaitWindowEventArgs e)
        {
            //the final status to deliver to the UpdateClient
            var status = ReturnStatus.Unknown;

            try
            {
                //loop through each GitHub release asset
                foreach (var a in AppUpdate.UpdateData.assets)
                {
                    //location of the unique folder
                    var dirA = $@"{UpdateDirectory}\{a.id}";

                    //each asset has a separate directory inside the 'UpdateId' directory
                    if (!Directory.Exists(dirA))
                        Directory.CreateDirectory(dirA);

                    //construct download job
                    var j = new Job
                    {
                        DownloadUri = new Uri(a.browser_download_url),
                        DownloadPath = $@"{dirA}\{a.name}",
                        DownloadSize = a.size
                    };

                    //download and flush job to disk
                    status = Agent.DoDownload(j).Result;
                }
            }
            catch
            {
                //ignore the error
            }

            //finally, return the final status
            e.Result = status;
        }
    }
}