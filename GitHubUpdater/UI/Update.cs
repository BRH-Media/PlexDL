using GitHubUpdater.API;
using GitHubUpdater.DownloadManager;
using GitHubUpdater.Enums;
using GitHubUpdater.Formatting;
using Markdig;
using PlexDL.Common.Security;
using PlexDL.WaitWindow;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Html = GitHubUpdater.Formatting.Html;

// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault

namespace GitHubUpdater.UI
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
                    UpdateDirectory = dir;

                    var title = AppUpdate.UpdateData.name;
                    var changes = AppUpdate.UpdateData.body;
                    var css = new Stylesheet().CssText;

                    var changesHtml = @"<h4>Changelog information is unavailable. Please ask the vendor for more information.</h4>";
                    lblUpdateTitle.Text = !string.IsNullOrEmpty(title) ? title : @"Update Available";

                    if (!string.IsNullOrEmpty(changes))
                        changesHtml = Markdown.ToHtml(changes);

                    var documentHtml = string.Format(new Html().HtmlText, css, changesHtml, NumberDownloads());

                    //apply decoded markdown-HTML
                    browserChanges.DocumentText = documentHtml;

                    //set current version
                    lblYourVersionValue.Text = $"v{AppUpdate.CurrentVersion}";
                }
            }

            //recenter the title (text has been changed so the size has too)
            CenterTitle();

            //load versioning status
            VersionStatusUpdate();
        }

        private void Update_Load(object sender, EventArgs e)
        {
            //reposition elements and load all update information
            LoadUi();
        }

        private void VersionStatusUpdate()
        {
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

            lblVersionStatus.Text = text;
            lblVersionStatus.ForeColor = Color.FromKnownColor(foreColor);
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
            BeginInvoke((MethodInvoker)delegate
           {
               Download();
           });
        }

        private int NumberDownloads()
        {
            return AppUpdate.UpdateData.assets.Sum(a => a.download_count);
        }

        private void Download(bool waitWindow = true)
        {
            try
            {
                //offload? Depends on a flag
                var status =
                    waitWindow
                    ? (ReturnStatus)WaitWindow.Show(Download, @"Downloading update files")
                    : ExecuteDownload();

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
                ExportError(ex);
                MessageBox.Show($"Error downloading your update files\n\n{ex}", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //always close form at the end of the download procedure
            Close();
        }

        private ReturnStatus ExecuteDownload()
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
                    else
                    {
                        Directory.Delete(dirA);
                        Directory.CreateDirectory(dirA);
                    }

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
            catch (Exception ex)
            {
                ExportError(ex);
            }

            //finally, return the final status
            return status;
        }

        private void Download(object sender, WaitWindowEventArgs e)
        {
            e.Result = ExecuteDownload();
        }

        private void ExportError(Exception ex)
        {
            try
            {
                //file name is MD5-hashed current date and time (for uniqueness)
                var fileName = $"{Md5Helper.CalculateMd5Hash(DateTime.Now.ToString(CultureInfo.CurrentCulture))}.log";

                //store all errors in the UpdateDirectory
                var errorsPath = $@"{UpdateDirectory}\errors";

                //ensure the 'errors' folder exists
                if (!Directory.Exists(errorsPath))
                    Directory.CreateDirectory(errorsPath);

                //full path
                var filePath = $@"{errorsPath}\{fileName}";

                //export error to log
                File.WriteAllText(filePath, ex.ToString());
            }
            catch
            {
                //ignore
            }
        }
    }
}