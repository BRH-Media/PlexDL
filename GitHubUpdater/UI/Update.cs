using GitHubUpdater.API;
using GitHubUpdater.Display;
using GitHubUpdater.Enums;
using GitHubUpdater.Net.DownloadManager;
using Markdig;
using PlexDL.WaitWindow;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Html = GitHubUpdater.Display.Html;

// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault

namespace GitHubUpdater.UI
{
    public partial class Update : Form
    {
        public UpdateResponse AppUpdate { get; set; } = null;

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

        private void LoadUI()
        {
            if (AppUpdate == null)
                NoUpdate();
            else
            {
                if (AppUpdate.CurrentVersion == null || AppUpdate.UpdateData == null)
                    NoUpdate();
                else
                {
                    //Release ID is the update download folder
                    var dir = $@"{Globals.UpdateRootDir}\{AppUpdate.UpdateData.id}";

                    //set global
                    Agent.UpdateDirectory = dir;

                    //GUI setup
                    var title = AppUpdate.UpdateData.name;
                    var changes = AppUpdate.UpdateData.body;
                    var css = new Stylesheet().CssText;

                    //setup the markdown HTML for the mini-browser
                    var changesHtml = @"<h4>Changelog information is unavailable. Please ask the vendor for more information.</h4>";
                    lblUpdateTitle.Text = !string.IsNullOrEmpty(title) ? title : @"Update Available";

                    if (!string.IsNullOrEmpty(changes))
                        changesHtml = Markdown.ToHtml(changes);

                    var documentHtml = string.Format(new Html().HtmlText, css, changesHtml, NumberDownloads());

                    //apply decoded markdown-HTML
                    browserChanges.DocumentText = documentHtml;
                }
            }

            //recenter the title (text has been changed so the size has too)
            CenterTitle();

            //load versioning status
            YourVersionStatusUpdate();
            UpdatedVersionStatusUpdate();
        }

        private void Update_Load(object sender, EventArgs e)
        {
            //reposition elements and load all update information
            LoadUI();
        }

        private void YourVersionStatusUpdate()
        {
            //set current version
            lblYourVersionValue.Text = $"v{AppUpdate.CurrentVersion}";

            //label style
            var text = @"";
            var foreColor = KnownColor.Chocolate;

            switch (AppUpdate.RunVersionCheck())
            {
                case VersionStatus.Outdated:
                    text = @"Outdated";
                    foreColor = KnownColor.DarkRed;
                    break;

                case VersionStatus.UpToDate:
                    text = @"Up-to-date";
                    foreColor = KnownColor.DarkGreen;
                    break;

                case VersionStatus.Bumped:
                    text = @"Bumped";
                    foreColor = KnownColor.DarkGreen;
                    break;

                case VersionStatus.Undetermined:
                    text = @"Undetermined";
                    foreColor = KnownColor.Chocolate;
                    break;
            }

            lblYourlVersionStatus.Text = text;
            lblYourlVersionStatus.ForeColor = Color.FromKnownColor(foreColor);
        }

        private void UpdatedVersionStatusUpdate()
        {
            //set updated version
            lblUpdatedVersionValue.Text = AppUpdate.UpdateData.tag_name;

            //label style
            var text = @"";
            var foreColor = KnownColor.Chocolate;

            switch (AppUpdate.Channel)
            {
                case Enums.UpdateChannel.Stable:
                    text = @"Stable Release";
                    foreColor = KnownColor.DarkGreen;
                    break;

                case Enums.UpdateChannel.Development:
                    text = @"Development Release";
                    foreColor = KnownColor.Chocolate;
                    break;

                case Enums.UpdateChannel.Unknown:
                    text = @"Undetermined";
                    foreColor = KnownColor.DarkRed;
                    break;
            }

            lblUpdatedVersionStatus.Text = text;
            lblUpdatedVersionStatus.ForeColor = Color.FromKnownColor(foreColor);
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

        private void BtnDownloadUpdate_Click(object sender, EventArgs e)

            //execute update download
            => Download();

        private int NumberDownloads()

            //sum of all asset downloads denotes the total downloads counter
            => AppUpdate.UpdateData
                .assets.Sum(a => a.download_count);

        private void Download(bool closeForm = true)
        {
            try
            {
                //download execution process
                var status = ExecuteDownload();

                //execute appropriate action
                switch (status)
                {
                    //unknown error occurred
                    case DownloadStatus.Errored:
                        MessageBox.Show(@"An unknown error occurred whilst attempting to download one or more update files", @"Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    //invalid job (one or more were null)
                    case DownloadStatus.NullJob:
                        MessageBox.Show(@"One or more download jobs were invalid; valid jobs have been completed.", @"Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;

                    //invalid download (one or more completed jobs came back null)
                    case DownloadStatus.NullDownload:
                        MessageBox.Show(@"One or more download jobs were returned as null; this means the data received was invalid and some or all downloads did not complete.", @"Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;

                    //all jobs were successful
                    case DownloadStatus.Downloaded:
                        var msg = MessageBox.Show(@"Done! Open download location?", @"Question",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (msg == DialogResult.Yes)
                            Process.Start(Agent.UpdateDirectory);
                        break;

                    //unknown job status; usually indicative of an undiagnosable issue
                    case DownloadStatus.Unknown:
                        MessageBox.Show(@"Download worker returned the 'unknown' job status indicator.", @"Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;

                    //downloader experienced a HTTP timeout error
                    case DownloadStatus.Timeout:
                        MessageBox.Show(@"Download worker experienced a HTTP timeout on one or more operations.", @"Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;

                    //the user cancelled the download operation
                    case DownloadStatus.Cancelled:
                        //don't do anything
                        break;

                    //default actioner if none of the above clauses apply
                    default:
                        MessageBox.Show(@"Download worker returned an invalid job status indicator.", @"Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }
            }
            catch (Exception ex)
            {
                //log the error to a file
                ex.ExportError();

                //alert the user
                MessageBox.Show($"Error downloading your update files\n\n{ex}", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //always close form at the end of the download procedure (if allowed)
            if (closeForm)
                Close();
        }

        /// <summary>
        /// Multi-threaded WaitWindow invocation; do not execute as it is automatic.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteDownload(object sender, WaitWindowEventArgs e)
            =>  //return the control flow back to the original function, but disable the wait window
                e.Result = ExecuteDownload(false);

        private DownloadStatus ExecuteDownload(bool waitWindow = true)
        {
            //wait window
            if (waitWindow)
                return (DownloadStatus)WaitWindow.Show(ExecuteDownload, @"Downloading update");

            //the final status to deliver to the UpdateClient
            var status = DownloadStatus.Unknown;

            try
            {
                //loop through each GitHub release asset
                foreach (var a in AppUpdate.UpdateData.assets)
                {
                    //location of the unique folder
                    var dirA = $@"{Agent.UpdateDirectory}\{a.id}";

                    //each asset has a separate directory inside the 'UpdateId' directory
                    if (!Directory.Exists(dirA))
                        Directory.CreateDirectory(dirA);
                    else
                    {
                        //the directory already exists; delete it then create it once more
                        Directory.Delete(dirA, true);
                        Directory.CreateDirectory(dirA);
                    }

                    //construct download job
                    var j = new Job
                    {
                        //the asset URI to download
                        DownloadUri = new Uri(a.browser_download_url),

                        //the path of the file to save the asset to
                        DownloadPath = $@"{dirA}\{a.name}",

                        //the byte-size of the asset
                        DownloadSize = a.size
                    };

                    //download and flush job to disk
                    status = j.DoDownload();
                }
            }
            catch (Exception ex)
            {
                //log the error to a file
                ex.ExportError();
            }

            //finally, return the final status
            return status;
        }
    }
}