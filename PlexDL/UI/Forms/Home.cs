using inet;
using LogDel.IO;
using PlexDL.AltoHTTP.Common.Downloader;
using PlexDL.Common;
using PlexDL.Common.API.PlexAPI;
using PlexDL.Common.API.PlexAPI.IO;
using PlexDL.Common.API.PlexAPI.Metadata.Handlers;
using PlexDL.Common.API.PlexAPI.Objects;
using PlexDL.Common.Caching;
using PlexDL.Common.Components.Controls;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Net;
using PlexDL.Common.PlayerLaunchers;
using PlexDL.Common.Pxz.UI;
using PlexDL.Common.Renderers.Forms.GridView;
using PlexDL.Common.SearchFramework;
using PlexDL.Common.Security;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.AppOptions;
using PlexDL.Common.Structures.AppOptions.Player;
using PlexDL.Common.Structures.Plex;
using PlexDL.Common.Update;
using PlexDL.MyPlex;
using PlexDL.WaitWindow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using UIHelpers;
using Directory = System.IO.Directory;

// ReSharper disable InvertIf

#pragma warning disable 1591

namespace PlexDL.UI.Forms
{
    public partial class Home : DoubleBufferedForm
    {
        public Home()
        {
            InitializeComponent();
            tabMain.SelectedIndex = 0;
        }

        private void BringToTop()
        {
            //send this form to the front of the screen
            TopMost = true;
            TopMost = false;

            //debug mode? If so, send the debug form to the top too
            if (Flags.IsDebug && ObjectProvider.DebugForm != null)
            {
                ObjectProvider.DebugForm.TopMost = true;
                ObjectProvider.DebugForm.TopMost = false;
            }
        }

        private void ManualSectionLoad()
        {
            if (dgvSections.Rows.Count <= 0) return;

            var ipt = ObjectProvider.LibUi.showInputForm("Enter section key", "Manual Section Lookup", true,
                "TV Library");
            if (ipt.txt == "!cancel=user")
                return;
            if (!int.TryParse(ipt.txt, out _))
                UIMessages.Error(@"Section key ust be numeric", @"Validation Error");
            else
                UpdateFromLibraryKey(ipt.txt, ipt.chkd ? ContentType.TvShows : ContentType.Movies);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //there needs to be a row selected in order to execute a player launch
            if (dgvMovies.SelectedRows.Count != 1 || dgvEpisodes.SelectedRows.Count != 1 ||
                dgvTracks.SelectedRows.Count != 1 || !ObjectProvider.Settings.Generic.DoubleClickLaunch)
                return base.ProcessCmdKey(ref msg, keyData);

            if (keyData != Keys.Enter) return base.ProcessCmdKey(ref msg, keyData);

            DoubleClickLaunch();
            return true;
        }

        private void ItmStartSearch_Click(object sender, EventArgs e)
        {
            SearchProcedure();
        }

        private void ItmLoadProfile_Click(object sender, EventArgs e)
        {
            LoadProfile();
        }

        private void ItmSaveProfile_Click(object sender, EventArgs e)
        {
            SaveProfile();
        }

        private void ItmDownloadThisEpisode_Click(object sender, EventArgs e)
        {
            cxtEpisodes.Close();
            DoDownloadSelected();
        }

        private void ItmDownloadAllEpisodes_Click(object sender, EventArgs e)
        {
            cxtEpisodes.Close();
            DoDownloadAll();
        }

        private void ItmManuallyLoadSection_Click(object sender, EventArgs e)
        {
            cxtLibrarySections.Close();
            ManualSectionLoad();
        }

        private void ItmEpisodeMetadataView_Click(object sender, EventArgs e)
        {
            cxtEpisodeOptions.Close();
            Metadata();
        }

        private void ItmDGVDownloadThisEpisode_Click(object sender, EventArgs e)
        {
            cxtEpisodeOptions.Close();
            DoDownloadSelected();
        }

        private void ItmDGVDownloadThisSeason_Click(object sender, EventArgs e)
        {
            cxtEpisodeOptions.Close();
            DoDownloadAll();
        }

        private void ItmContentMetadataView_Click(object sender, EventArgs e)
        {
            cxtMovieOptions.Close();
            Metadata();
        }

        private void ItmDGVDownloadThisMovie_Click(object sender, EventArgs e)
        {
            cxtMovieOptions.Close();
            DoDownloadSelected();
        }

        private void NfyMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BringToTop();
        }

        private void ItmSetDlDirectory_Click(object sender, EventArgs e)
        {
            SetDownloadDirectory();
        }

        private void BtnStream_Click(object sender, EventArgs e)
        {
            StartStreaming();
        }

        private void ItmMetadata_Click(object sender, EventArgs e)
        {
            Metadata();
        }

        private void ItmLogViewer_Click(object sender, EventArgs e)
        {
            ShowLogViewer();
        }

        private void ItmDownloadThisTrack_Click(object sender, EventArgs e)
        {
            cxtTracks.Close();
            DoDownloadSelected();
        }

        private void ItmDownloadThisAlbum_Click(object sender, EventArgs e)
        {
            cxtTracks.Close();
            DoDownloadAll();
        }

        private void CxtTrackOptions_Opening(object sender, CancelEventArgs e)
        {
            if (dgvTracks.SelectedRows.Count == 0) e.Cancel = true;
        }

        private void ItmDGVDownloadThisTrack_Click(object sender, EventArgs e)
        {
            cxtTrackOptions.Close();
            DoDownloadSelected();
        }

        private void ItmDGVDownloadThisAlbum_Click(object sender, EventArgs e)
        {
            cxtTrackOptions.Close();
            DoDownloadAll();
        }

        private void ItmTrackMetadataView_Click(object sender, EventArgs e)
        {
            cxtTrackOptions.Close();
            Metadata();
        }

        private void ItmCleanupAllData_Click(object sender, EventArgs e)
        {
            DoCleanup();
        }

        private void ItmStreamInPVS_Click(object sender, EventArgs e)
        {
            cxtStreamOptions.Close();
            PvsLauncher.LaunchPvs(ObjectProvider.CurrentStream);
        }

        private void ItmStreamInVLC_Click(object sender, EventArgs e)
        {
            cxtStreamOptions.Close();
            VlcLauncher.LaunchVlc(ObjectProvider.CurrentStream);
        }

        private void ItmStreamInBrowser_Click(object sender, EventArgs e)
        {
            cxtStreamOptions.Close();
            BrowserLauncher.LaunchBrowser(ObjectProvider.CurrentStream);
        }

        private void DoConnectFromServer(Server s)
        {
            var address = s.address;
            var port = s.port;

            var connectInfo = new ConnectionInfo
            {
                PlexAccountToken = Strings.GetToken(),
                PlexAddress = address,
                PlexPort = port,
                RelaysOnly = ObjectProvider.Settings.ConnectionInfo.RelaysOnly
            };

            var uri = Strings.GetBaseUri(true);
            var reply = XmlGet.GetXmlTransaction(uri, false, false, false);

            Flags.IsConnected = true;

            ObjectProvider.Settings.ConnectionInfo = connectInfo;

            if (ObjectProvider.Settings.Generic.ShowConnectionSuccess)
                UIMessages.Info(@"Connection successful!");

            SetProgressLabel("Connected");
            SetDisconnect();

            if (reply.ChildNodes.Count != 0)
                PopulateLibrary(reply);
        }

        private void ItmServerManager_Click(object sender, EventArgs e)
        {
            if (Internet.IsConnected)
                using (var frm = new ServerManager())
                {
                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    ObjectProvider.Settings.ConnectionInfo.PlexAccountToken = frm.SelectedServer.accessToken;
                    ObjectProvider.Settings.ConnectionInfo.PlexAddress = frm.SelectedServer.address;
                    ObjectProvider.Settings.ConnectionInfo.PlexPort = frm.SelectedServer.port;
                    ObjectProvider.Svr = frm.SelectedServer;

                    DoConnectFromServer(frm.SelectedServer);
                }
            else
                UIMessages.Error(
                    @"No internet connection. Please connect to a network before attempting to start a Plex server connection.",
                    @"Network Error");
        }

        private void ItmExportObj_Click(object sender, EventArgs e)
        {
            if (Flags.IsConnected)
                DoStreamExport();
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            if (dgvMovies.SelectedRows.Count != 1 && dgvEpisodes.SelectedRows.Count != 1 &&
                dgvTracks.SelectedRows.Count != 1) return;
            if (!Flags.IsDownloadRunning && !Flags.IsEngineRunning)
            {
                ObjectProvider.Queue = new List<StreamInfo>();
                switch (ObjectProvider.CurrentContentType)
                {
                    case ContentType.TvShows:
                        if (dgvEpisodes.SelectedRows.Count == 1) cxtEpisodes.Show(MousePosition);
                        break;

                    case ContentType.Movies:
                        DoDownloadSelected();
                        break;

                    case ContentType.Music:
                        if (dgvTracks.SelectedRows.Count == 1) cxtTracks.Show(MousePosition);
                        break;
                }
            }
            else
            {
                CancelDownload();
            }
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (Flags.IsDownloadRunning && Flags.IsEngineRunning)
            {
                if (!Flags.IsDownloadPaused)
                {
                    ObjectProvider.Engine.Pause();
                    SetResume();
                    SetProgressLabel(lblProgress.Text + " (Paused)");
                    Flags.IsDownloadPaused = true;
                }
                else
                {
                    ObjectProvider.Engine.ResumeAsync();
                    SetPause();
                    Flags.IsDownloadPaused = false;
                }
            }
        }

        private void ItmDisconnect_Click(object sender, EventArgs e)
        {
            if (Flags.IsConnected)
                Disconnect();
        }

        private void ItmAbout_Click(object sender, EventArgs e)
        {
            using (var frm = new About())
            {
                frm.ShowDialog();
            }
        }

        private void ItmCacheMetrics_Click(object sender, EventArgs e)
        {
            using (var frm = new CachingMetricsUI())
            {
                frm.Metrics = CachingMetrics.FromLatest();
                frm.ShowDialog();
            }
        }

        private void ItmSettings_Click(object sender, EventArgs e)
        {
            using (var frm = new Settings())
                frm.ShowDialog();
        }

        private void ItmClearCache_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(CachingFileDir.RootCacheDirectory))
                {
                    if (!UIMessages.Question(@"Are you sure you want to clear the cache?")) return;

                    Directory.Delete(CachingFileDir.RootCacheDirectory, true);
                    UIMessages.Info(@"Successfully deleted cached data");
                }
                else
                    UIMessages.Error(@"There's no cached data to clear", @"Validation Error");
            }
            catch (Exception ex)
            {
                // ReSharper disable once LocalizableElement
                UIMessages.Error("Error whilst trying to delete cached data:\n\n" + ex.Message);
                LoggingHelpers.RecordException(ex.Message, "ClearCacheError");
            }
        }

        private void TabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabLog)
                dgvLog.DataSource =
                    File.Exists($@"{LogWriter.LogDirectory}\PlexDL.logdel") //check if the main log file exists
                        ? LogReader.TableFromFile($@"{LogWriter.LogDirectory}\PlexDL.logdel",
                            false) //if it does, load it.
                        : null; //if it doesn't, clear the grid by applying a null value.
            else
                dgvLog.DataSource = null; //clear log grid to save on memory (it won't be in focus anyway)
        }

        private void TmrWorkerTimeout_Tick(object sender, EventArgs e)
        {
            try
            {
                //we only need this timer to run once - so stop it once the
                //tick interval is reached.
                tmrWorkerTimeout.Stop();

                //check if we're still waiting for the worker to start doing
                //something
                if (!string.Equals(lblProgress.Text.ToLower(), "waiting")) return;

                //it's still waiting; kill the worker thread.
                if (wkrGetMetadata.IsBusy)
                    wkrGetMetadata.Abort();
                //tell the user that the worker timed out
                UIMessages.Error(@"Failed to get metadata; the worker timed out.", @"Data Error");

                //cancel the download silently and with a custom log
                //and label input
                CancelDownload(true, "Worker Timeout");
            }
            catch (ThreadAbortException)
            {
                //nothing; triggering AbortableBackgroundWorker.Abort() might
                //trigger this exception, so just ignore it - it's not a serious
                //issue.
            }
            catch (Exception ex)
            {
                //log and then ignore
                LoggingHelpers.RecordException(ex.Message, "WkrMetadataTimerError");
                CancelDownload(true, "Worker Timeout");
            }
        }

        private static void DoCleanup(object sender, WaitWindowEventArgs e)
        {
            if (e.Arguments.Count == 1)
            {
                //Try and delete the .plexdl AppData folder and all its subfolders and files.
                //This process will be recursive to deeper than level 1.
                var dir = (string)e.Arguments[0];
                Directory.Delete(dir, true);
            }
        }

        private static void DoCleanup(bool waitWindow = true)
        {
            //check if the AppData .plexdl folder actually exists
            if (Directory.Exists(Strings.PlexDlAppData))
            {
                var ask = UIMessages.Question(
                    @"Are you sure you want to do this? This will remove all logging, caching and secure data.",
                    @"Cleanup Confirmation");

                //if they clicked anything other than 'Yes' on the message above, then exit the method.
                //Only 'Yes' produces 'true' as a result.
                if (!ask) return;

                try
                {
                    //should there be a waitwindow for multi-threading?
                    if (waitWindow)
                        //yes, offload to the WaitWindow handler
                        WaitWindow.WaitWindow.Show(DoCleanup, @"Cleaning", Strings.PlexDlAppData);
                    else
                        //no, execute on the GUI thread (inefficient, don't do this)
                        Directory.Delete(Strings.PlexDlAppData, true);

                    //alert user of the success
                    UIMessages.Info(
                        "Successfully removed all PlexDL files in the AppData folder. This means all logging, caching and secure data has been deleted also." +
                        "\n\nNote: This event has not been logged.",
                        @"Cleanup Completed");
                }
                catch (Exception ex)
                {
                    UIMessages.Error(
                        $"Cleanup error occurred:\n\n{ex}\n\nNote: This exception has not been logged",
                        @"Cleanup Failed");
                }
            }
            else
            {
                UIMessages.Error(@"There's no data to cleanup; the data folder doesn't exist yet.",
                    @"Cleanup Failed");
            }
        }

        private void ItmRepo_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Strings.RepoUrl);
            }
            catch (Exception ex)
            {
                UIMessages.Error("Couldn't open repo url. Check exception log.");
                LoggingHelpers.RecordException(ex.Message, @"OpenRepoError");
            }
        }

        private void ItmCheckForUpdates_Click(object sender, EventArgs e)
        {
            UpdateManager.RunUpdateCheck();
        }

        private void ItmRenderKeyColumn_Click(object sender, EventArgs e)
        {
            RenderLibraryView(DataProvider.SectionsProvider.GetRawTable(), itmRenderKeyColumn.Checked);
        }

        private void ItmClearMyToken_Click(object sender, EventArgs e)
        {
            TokenManager.TokenClearProcedure();
        }

        private void ItmOpenDataFolder_Click(object sender, EventArgs e)
        {
            Process.Start(Strings.PlexDlAppData); //open the %APPDATA%\.plexdl folder in Explorer
        }

        private void ItmCast_Click(object sender, EventArgs e)
        {
            DoCast();
        }

        private void DoCast()
        {
            if (dgvMovies.SelectedRows.Count == 1 || dgvEpisodes.SelectedRows.Count == 1)
                Cast.TryCast(ObjectFromSelection());
            else if (dgvTracks.SelectedRows.Count == 1)
                UIMessages.Warning(@"Casting music is not yet supported");
        }

        private void ItmContentCast_Click(object sender, EventArgs e)
        {
            DoCast();
        }

        private void ItmEpisodeCast_Click(object sender, EventArgs e)
        {
            DoCast();
        }

        private void ItmTrackCast_Click(object sender, EventArgs e)
        {
            DoCast();
        }

        private void ItmContentStream_Click(object sender, EventArgs e)
        {
            StartStreaming();
        }

        private void ItmEpisodeStream_Click(object sender, EventArgs e)
        {
            StartStreaming();
        }

        private void ItmTrackStream_Click(object sender, EventArgs e)
        {
            StartStreaming();
        }

        public int DownloadIndex;
        public int DownloadTotal;
        public int DownloadsSoFar;

        private PlexMovie GetMovieObjectFromSelection()
        {
            var obj = new PlexMovie();

            if (dgvMovies.SelectedRows.Count != 1) return obj;

            var index = TableManager.GetTableIndexFromDgv(dgvMovies);
            obj = ObjectBuilders.GetMovieObjectFromIndex(index);

            return obj;
        }

        private PlexTvShow GetTvObjectFromSelection()
        {
            var obj = new PlexTvShow();

            if (dgvTVShows.SelectedRows.Count != 1 || dgvEpisodes.SelectedRows.Count != 1) return obj;

            var index = TableManager.GetTableIndexFromDgv(dgvEpisodes, DataProvider.EpisodesProvider.GetRawTable());
            obj = ObjectBuilders.GetTvObjectFromIndex(index);

            return obj;
        }

        private PlexMusic GetMusicObjectFromSelection()
        {
            var obj = new PlexMusic();

            if (dgvArtists.SelectedRows.Count != 1 || dgvTracks.SelectedRows.Count != 1) return obj;

            var index = TableManager.GetTableIndexFromDgv(dgvTracks, DataProvider.TracksProvider.GetRawTable());
            obj = ObjectBuilders.GetMusicObjectFromIndex(index);

            return obj;
        }

        public void LoadProfile()
        {
            if (!Flags.IsConnected)
            {
                if (ofdLoad.ShowDialog() == DialogResult.OK)
                {
                    //store the file-type of the currently selected file
                    var actExt = Path.GetExtension(ofdLoad.FileName).ToLower();

                    //what we're checking for
                    const string prfExt = @".prof";
                    const string metExt = @".pxz";
                    const string xmlExt = @".pmxml";

                    //execute file-type checks
                    switch (actExt)
                    {
                        //XML settings profile
                        case prfExt:
                            DoLoadProfile(ofdLoad.FileName);
                            break;

                        //multiple metadata file-types work with the same method
                        case xmlExt:
                        case metExt:
                            try
                            {
                                var metadata = MetadataIO.MetadataFromFile(ofdLoad.FileName);
                                UIUtils.RunMetadataWindow(metadata);
                            }
                            catch (Exception ex)
                            {
                                LoggingHelpers.RecordException(ex.Message, @"StartupLoadPxz");
                                UIMessages.Error($"Error occurred whilst loading PXZ file:\n\n{ex}");
                            }

                            break;

                        //anything else can't be handled
                        default:
                            UIMessages.Error(@"Unrecognised file-type");
                            break;
                    }
                }
            }
            else
            {
                UIMessages.Warning(@"You can't load profiles while you're connected; please disconnect first.",
                    @"Validation Error");
            }
        }

        public void SaveProfile()
        {
            if (string.IsNullOrEmpty(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken))
            {
                UIMessages.Warning(@"You need to authenticate before saving a profile", @"Validation Error");
            }
            else
            {
                if (sfdSave.ShowDialog() == DialogResult.OK) DoSaveProfile(sfdSave.FileName);
            }
        }

        public void DoSaveProfile(string fileName, bool silent = false)
        {
            try
            {
                ProfileIO.ProfileToFile(fileName, ObjectProvider.Settings, silent);

                if (!silent)
                    UIMessages.Info(@"Successfully saved profile!");

                LoggingHelpers.RecordGeneralEntry(@"Saved profile " + fileName);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "@SaveProfileError");
                if (!silent)
                    UIMessages.Error(ex.ToString(), @"Error in saving XML Profile");
            }
        }

        public void DoLoadProfile(string fileName, bool silent = false)
        {
            try
            {
                //load the XML file to an object
                var subReq = ProfileIO.ProfileFromFile(fileName, silent);

                //null validation
                if (subReq == null)
                {
                    UIMessages.Error(@"Load failed; profile was null.");
                    return;
                }

                try
                {
                    //check if the version information stored is valid
                    if (!string.IsNullOrWhiteSpace(subReq.Generic.StoredAppVersion))
                    {
                        //construct version objects for comparison
                        var vStoredVersion = new Version(subReq.Generic.StoredAppVersion);
                        var vThisVersion = new Version(Application.ProductVersion);

                        //compare version objects to see which is newer or earlier
                        var vCompare = vThisVersion.CompareTo(vStoredVersion);

                        //below zero means the profile was made in a newer version
                        if (vCompare < 0)
                        {
                            if (!silent)
                            {
                                //ask the user whether they want to continue
                                var result = UIMessages.Question(
                                    "You're trying to load a profile made in a newer version of PlexDL. This could have several implications:\n" +
                                    "- Possible data loss of your current profile if PlexDL overwrites it\n" +
                                    "- Features may not work as intended\n" +
                                    "- Increased risk of errors\n\n" +
                                    "Are you sure you'd like to proceed?");
                                if (!result)
                                    return;
                            }

                            //log event
                            LoggingHelpers.RecordGeneralEntry("Tried to load a profile made in a newer version: " +
                                                              vStoredVersion + " > " + vThisVersion);
                        }

                        //above zero means the profile was made in an earlier version
                        else if (vCompare > 0)
                        {
                            if (!silent)
                            {
                                //ask the user whether they want to continue
                                var result = UIMessages.Question(
                                    "You're trying to load a profile made in an earlier version of PlexDL. This could have several implications:\n" +
                                    "- Possible data loss of your current profile if PlexDL overwrites it\n" +
                                    "- Features may not work as intended\n" +
                                    "- Increased risk of errors\n\n" +
                                    "Are you sure you'd like to proceed?");
                                if (!result)
                                    return;
                            }

                            //log event
                            LoggingHelpers.RecordGeneralEntry("Tried to load a profile made in an earlier version: " +
                                                              vStoredVersion + " < " + vThisVersion);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LoggingHelpers.RecordGeneralEntry("Version information load error: " + ex.Message);
                    LoggingHelpers.RecordException(ex.Message, "VersionLoadError");
                }

                ObjectProvider.Settings = subReq;

                if (!silent)
                    UIMessages.Info("Successfully loaded profile!");
                LoggingHelpers.RecordGeneralEntry("Loaded profile " + fileName);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "LoadProfileError");
                if (!silent)
                    UIMessages.Error(ex.ToString(), "Load Error");
            }
        }

        private void Disconnect(bool silent = false)
        {
            try
            {
                if (!Flags.IsConnected) return;
                if (ObjectProvider.Engine != null) CancelDownload();

                ClearContentView();
                ClearTvViews();
                ClearMusicViews();
                ClearLibraryViews();
                SetProgressLabel(@"Disconnected from Plex");
                SetConnect();
                SelectMoviesTab();
                Flags.IsConnected = false;
                Flags.IsInitialFill = false;

                //drop all data
                DataProvider.DropAllData();
                LoggingHelpers.RecordGeneralEntry(@"All data dropped due to disconnect");

                if (!silent)
                    UIMessages.Info(@"Disconnected from Plex");
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"DisconnectError");
                if (!silent)
                    UIMessages.Error($"Disconnect error:\n\n{ex}");
            }
        }

        private static void DoStreamExport_Handler(object sender, WaitWindowEventArgs e)
        {
            var content = (PlexObject)e.Arguments[0];
            var fileName = (string)e.Arguments[1];
            var poster = e.Arguments.Count > 2 ? (Bitmap)e.Arguments[2] : null;

            MetadataIO.MetadataToFile(fileName, content, poster);
        }

        private void DoStreamExport()
        {
            try
            {
                if (dgvMovies.SelectedRows.Count != 1 && dgvEpisodes.SelectedRows.Count != 1 &&
                    dgvTracks.SelectedRows.Count != 1) return;

                PlexObject content;
                switch (ObjectProvider.CurrentContentType)
                {
                    case ContentType.Movies:
                        content = GetMovieObjectFromSelection();
                        break;

                    case ContentType.TvShows:
                        content = GetTvObjectFromSelection();
                        break;

                    case ContentType.Music:
                        content = GetMusicObjectFromSelection();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (sfdExport.ShowDialog() == DialogResult.OK)
                {
                    WaitWindow.WaitWindow.Show(DoStreamExport_Handler, @"Exporting", content, sfdExport.FileName);
                }
            }
            catch (Exception ex)
            {
                //log and ignore
                LoggingHelpers.RecordGeneralEntry("Export error: " + ex.Message);
                LoggingHelpers.RecordException(ex.Message, "StreamExportError");
            }
        }

        private static void GetTitlesTable(XmlNode doc, ContentType type)
        {
            //setup DataSet for XML parsing
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            //switch through the current content type
            switch (type)
            {
                //Music titles
                case ContentType.Music:

                //TV Show titles
                case ContentType.TvShows:
                    DataProvider.TitlesProvider.SetRawTable(sections.Tables["Directory"]);
                    break;

                //Movie titles
                case ContentType.Movies:
                    DataProvider.TitlesProvider.SetRawTable(sections.Tables["Video"]);
                    break;
            }
        }

        private void SetViewingStatus(bool noRecords = false)
        {
            try
            {
                //view data table providers
                var filteredViewTable = DataProvider.FilteredProvider.GetViewTable();
                var titlesViewTable = DataProvider.TitlesProvider.GetViewTable();

                //total records currently being displayed
                var currentlyViewing = noRecords
                    ? 0
                    : Flags.IsFiltered
                        ? filteredViewTable?.Rows.Count
                          ?? titlesViewTable.Rows.Count
                        : titlesViewTable?.Rows.Count
                          ?? 0;

                //total available records to display
                var totalAvailable = noRecords
                    ? 0
                    : titlesViewTable?.Rows.Count
                      ?? 0;

                //what text to display
                var newText = noRecords
                    ? @"Not Loaded"
                    : $"{currentlyViewing}" +
                      $"/{totalAvailable}";

                //what colour to display
                var newColour = noRecords
                    ? Color.DarkRed
                    : Color.Black;

                //apply the values
                lblViewingValue.Text = newText;
                lblViewingValue.ForeColor = newColour;
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"SetMainViewingStatusError");
            }
        }

        private void PopulateLibrary(object sender, WaitWindowEventArgs e)
            => PopulateLibrary((XmlDocument)e.Arguments[0], false);

        private void PopulateLibrary(XmlDocument doc, bool waitWindow = true)
        {
            try
            {
                //multi-threaded operation
                if (waitWindow)
                {
                    //execute wait window
                    WaitWindow.WaitWindow.Show(PopulateLibrary, "Populating Library", doc);
                }
                else
                {
                    //if we're given a null XML document
                    if (doc == null)

                        //exit; no point working on null info
                        return;

                    //log status
                    LoggingHelpers.RecordGeneralEntry("Library population requested");

                    //directory on the server for sections
                    var libraryDir = KeyGatherers.GetLibraryKey(doc).TrimEnd('/');

                    //server base URI
                    var baseUri = Strings.GetBaseUri(false);

                    //get the library sections key from the Plex API (future-proofing)
                    var uriSectionKey = baseUri + "library/?X-Plex-Token=";

                    //XML for where to find the section key
                    var xmlSectionKey = XmlGet.GetXmlTransaction(uriSectionKey, false, false, false);

                    //the XML API path listing library sections
                    var sectionDir = KeyGatherers.GetSectionKey(xmlSectionKey).TrimEnd('/');

                    //the XML endpoint listing library sections
                    var uriSections = baseUri + libraryDir + "/" + sectionDir + "/?X-Plex-Token=";

                    //XML for all library sections
                    var xmlSections = XmlGet.GetXmlTransaction(uriSections, false, false, false);

                    //log status
                    LoggingHelpers.RecordGeneralEntry("Creating new datasets");

                    //setup DataSet for XML parsing
                    var sections = new DataSet();
                    sections.ReadXml(new XmlNodeReader(xmlSections));

                    //parse out XML DataTable
                    var sectionsTable = sections.Tables["Directory"];

                    //apply the sections DataTable to the sections DataSet
                    DataProvider.SectionsProvider.SetRawTable(sectionsTable);

                    //log status
                    LoggingHelpers.RecordGeneralEntry("Binding to grid");

                    //render library sections
                    RenderLibraryView(sectionsTable);

                    //set globals
                    Flags.IsLibraryFilled = true;
                    Strings.CurrentApiUri = baseUri + libraryDir + "/" + sectionDir + "/";
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "LibPopError");

                //alert the user
                UIMessages.Error(ex.ToString());
            }
        }

        private void UpdateContentView(object sender, WaitWindowEventArgs e)
            => UpdateContentView((XmlDocument)e.Arguments[0], (ContentType)e.Arguments[1], false);

        private void UpdateContentView(XmlNode doc, ContentType type, bool waitWindow = true)
        {
            try
            {
                //multi-threaded operation
                if (waitWindow)
                {
                    //execute wait window
                    WaitWindow.WaitWindow.Show(UpdateContentView, "Updating Content", doc, type);
                }
                else
                {
                    //log status
                    LoggingHelpers.RecordGeneralEntry("Updating library contents");

                    //set the titles table appropriately
                    GetTitlesTable(doc, type);

                    //null validation
                    if (DataProvider.TitlesProvider.GetRawTable() != null)
                    {
                        //set current types
                        ObjectProvider.CurrentContentType = type;

                        //switch through the current content type to decide
                        //how to render it
                        switch (type)
                        {
                            //Movies
                            case ContentType.Movies:

                                //log status
                                LoggingHelpers.RecordGeneralEntry("Rendering Movies");

                                //render the movies
                                RenderMoviesView(DataProvider.TitlesProvider.GetRawTable());

                                //set this in the toolstrip so the user can see how many items are loaded
                                SetViewingStatus();

                                //exit
                                break;

                            //TV Shows
                            case ContentType.TvShows:

                                //log status
                                LoggingHelpers.RecordGeneralEntry("Rendering TV Shows");

                                //render the TV shows and subsequent seasons and episodes
                                RenderTvView(DataProvider.TitlesProvider.GetRawTable());

                                //set this in the toolstrip so the user can see how many items are loaded
                                SetViewingStatus();

                                //exit
                                break;

                            //Music
                            case ContentType.Music:

                                //log status
                                LoggingHelpers.RecordGeneralEntry("Rendering Artists");

                                //render the artists and subsequent albums and tracks
                                RenderArtistsView(DataProvider.TitlesProvider.GetRawTable());

                                //set this in the toolstrip so the user can see how many items are loaded
                                SetViewingStatus();

                                //exit
                                break;

                            //Unsupported content type
                            default:

                                //throw an exception to declare such
                                throw new ArgumentOutOfRangeException(nameof(type), type,
                                    $@"Unrecognised content type: {type}");
                        }
                    }
                    else

                        //log the problem
                        LoggingHelpers.RecordGeneralEntry("Library contents were null; rendering did not occur");
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "UpdateContentViewError");

                //alert the user
                UIMessages.Error(ex.ToString());
            }
        }

        private void UpdateEpisodesView(object sender, WaitWindowEventArgs e)
            => UpdateEpisodesView((XmlDocument)e.Arguments[0], false);

        private void UpdateEpisodesView(XmlNode doc, bool waitWindow = true)
        {
            try
            {
                //multi-threaded operation
                if (waitWindow)
                {
                    //execute wait window
                    WaitWindow.WaitWindow.Show(UpdateEpisodesView, "Updating Episodes", doc);
                }
                else
                {
                    //log status
                    LoggingHelpers.RecordGeneralEntry("Updating episode contents");
                    LoggingHelpers.RecordGeneralEntry("Creating datasets");

                    //setup the DataSet for XML parsing
                    var sections = new DataSet();
                    sections.ReadXml(new XmlNodeReader(doc));

                    //apply new data
                    DataProvider.EpisodesProvider.SetRawTable(sections.Tables["Video"]);

                    //log status
                    LoggingHelpers.RecordGeneralEntry("Cleaning unwanted data");
                    LoggingHelpers.RecordGeneralEntry("Binding to grid");

                    //render the view
                    RenderEpisodesView(DataProvider.EpisodesProvider.GetRawTable());
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "UpdateEpisodesViewError");

                //alert the user
                UIMessages.Error(ex.ToString());
            }
        }

        private void UpdateTracksView(object sender, WaitWindowEventArgs e)
            => UpdateTracksView((XmlDocument)e.Arguments[0], false);

        private void UpdateTracksView(XmlNode doc, bool waitWindow = true)
        {
            try
            {
                //multi-threaded operation
                if (waitWindow)
                {
                    //execute wait window
                    WaitWindow.WaitWindow.Show(UpdateTracksView, "Updating Tracks", doc);
                }
                else
                {
                    //log status
                    LoggingHelpers.RecordGeneralEntry("Updating track contents");
                    LoggingHelpers.RecordGeneralEntry("Creating datasets");

                    //setup the DataSet for XML parsing
                    var sections = new DataSet();
                    sections.ReadXml(new XmlNodeReader(doc));

                    //apply new data
                    DataProvider.TracksProvider.SetRawTable(sections.Tables["Track"]);

                    //log status
                    LoggingHelpers.RecordGeneralEntry("Cleaning unwanted data");
                    LoggingHelpers.RecordGeneralEntry("Binding to grid");

                    //render the data
                    RenderTracksView(DataProvider.TracksProvider.GetRawTable());
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "UpdateTracksViewError");

                //alert the user
                UIMessages.Error(ex.ToString());
            }
        }

        private void UpdateSeriesView(object sender, WaitWindowEventArgs e)
            => UpdateSeriesView((XmlDocument)e.Arguments[0], false);

        private void UpdateSeriesView(XmlNode doc, bool waitWindow = true)
        {
            try
            {
                //multi-threaded operation
                if (waitWindow)
                {
                    //execute wait window
                    WaitWindow.WaitWindow.Show(UpdateSeriesView, "Updating Series", doc);
                }
                else
                {
                    //log status
                    LoggingHelpers.RecordGeneralEntry("Updating series contents");
                    LoggingHelpers.RecordGeneralEntry("Creating datasets");

                    //setup the DataSet for XML parsing
                    var sections = new DataSet();
                    sections.ReadXml(new XmlNodeReader(doc));

                    //apply new data
                    DataProvider.SeasonsProvider.SetRawTable(sections.Tables["Directory"]);

                    //log status
                    LoggingHelpers.RecordGeneralEntry("Cleaning unwanted data");
                    LoggingHelpers.RecordGeneralEntry("Binding to grid");

                    //render the data
                    RenderSeasonsView(DataProvider.SeasonsProvider.GetRawTable());
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "UpdateSeriesViewError");

                //alert the user
                UIMessages.Error(ex.ToString());
            }
        }

        private void UpdateAlbumsView(object sender, WaitWindowEventArgs e)
            => UpdateAlbumsView((XmlDocument)e.Arguments[0], false);

        private void UpdateAlbumsView(XmlNode doc, bool waitWindow = true)
        {
            try
            {
                //multi-threaded operation
                if (waitWindow)
                {
                    //execute wait window
                    WaitWindow.WaitWindow.Show(UpdateAlbumsView, "Updating Albums", doc);
                }
                else
                {
                    //log status
                    LoggingHelpers.RecordGeneralEntry("Updating album contents");
                    LoggingHelpers.RecordGeneralEntry("Creating data-sets");

                    //setup the DataSet for XML parsing
                    var sections = new DataSet();
                    sections.ReadXml(new XmlNodeReader(doc));

                    //apply new data
                    DataProvider.AlbumsProvider.SetRawTable(sections.Tables["Directory"]);

                    //log status
                    LoggingHelpers.RecordGeneralEntry("Cleaning unwanted data");
                    LoggingHelpers.RecordGeneralEntry("Binding to grid");

                    //render the data
                    RenderAlbumsView(DataProvider.AlbumsProvider.GetRawTable());
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "UpdateAlbumsViewError");

                //alert the user
                UIMessages.Error(ex.ToString());
            }
        }

        private void WkrGrabTv()
        {
            if (Flags.IsDownloadAll)
            {
                LoggingHelpers.RecordGeneralEntry(@"Worker is to grab metadata for All Episodes");

                var rowCount = DataProvider.EpisodesProvider.GetRawTable().Rows.Count;

                for (var i = 0; i < rowCount; i++)
                {
                    SetProgressLabel(@"Getting Metadata " + (i + 1) + @"/" + rowCount);

                    var show = ObjectBuilders.GetTvObjectFromIndex(i);
                    var dlInfo = show.StreamInformation;
                    var dir = DownloadLayout.CreateDownloadLayoutTvShow(show, ObjectProvider.Settings,
                        DownloadLayout.MF_PLEX_STANDARD_LAYOUT);
                    dlInfo.DownloadPath = dir.SeasonPath;
                    ObjectProvider.Queue.Add(dlInfo);
                }
            }
            else
            {
                LoggingHelpers.RecordGeneralEntry(@"Worker is to grab Single Episode metadata");

                SetProgressLabel(@"Getting Metadata 1/1");

                var show = GetTvObjectFromSelection();
                var dlInfo = show.StreamInformation;
                var dir = DownloadLayout.CreateDownloadLayoutTvShow(show, ObjectProvider.Settings,
                    DownloadLayout.MF_PLEX_STANDARD_LAYOUT);
                dlInfo.DownloadPath = dir.SeasonPath;
                ObjectProvider.Queue.Add(dlInfo);
            }
        }

        private PlexObject ObjectFromSelection()
        {
            PlexObject p = null;

            try
            {
                if (dgvMovies.SelectedRows.Count == 1 || dgvEpisodes.SelectedRows.Count == 1 ||
                    dgvTracks.SelectedRows.Count == 1)
                {
                    var t = ObjectProvider.CurrentContentType;
                    switch (t)
                    {
                        case ContentType.TvShows:
                            p = GetTvObjectFromSelection();
                            break;

                        case ContentType.Movies:
                            p = GetMovieObjectFromSelection();
                            break;

                        case ContentType.Music:
                            p = GetMusicObjectFromSelection();
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"GetPlexObjectError");
            }

            return p;
        }

        private void WkrGrabMusic()
        {
            //whether or not to download all tracks
            if (Flags.IsDownloadAll)
            {
                //log status
                LoggingHelpers.RecordGeneralEntry(@"Worker is to grab metadata for All Tracks");

                //row count of the raw data table
                var rowCount = DataProvider.TracksProvider.GetRawTable().Rows.Count;

                //go through each
                for (var i = 0; i < rowCount; i++)
                {
                    //set UI status
                    SetProgressLabel(@"Getting Metadata " + (i + 1) + @"/" + rowCount);

                    //get the PlexObject for the specified index
                    var track = ObjectBuilders.GetMusicObjectFromIndex(i);

                    //download information is just the stream information provided by the Plex processor
                    var dlInfo = track.StreamInformation;

                    //create the download directory layout
                    var dir = DownloadLayout.CreateDownloadLayoutMusic(track, ObjectProvider.Settings,
                        DownloadLayout.MF_PLEX_STANDARD_LAYOUT);

                    //the download path should be the album path
                    dlInfo.DownloadPath = dir.AlbumPath;

                    //add download to the queue
                    ObjectProvider.Queue.Add(dlInfo);
                }
            }
            else
            {
                //log status
                LoggingHelpers.RecordGeneralEntry(@"Worker is to grab Single Track metadata");

                //set UI status
                SetProgressLabel(@"Getting Metadata 1/1");

                //get the PlexObject for the specified index
                var track = GetMusicObjectFromSelection();

                //download information is just the stream information provided by the Plex processor
                var dlInfo = track.StreamInformation;

                //create the download directory layout
                var dir = DownloadLayout.CreateDownloadLayoutMusic(track, ObjectProvider.Settings,
                    DownloadLayout.MF_PLEX_STANDARD_LAYOUT);

                //the download path should be the album path
                dlInfo.DownloadPath = dir.AlbumPath;

                //add download to the queue
                ObjectProvider.Queue.Add(dlInfo);
            }
        }

        private void WkrGrabMovie()
        {
            //set UI status
            SetProgressLabel(@"Getting Metadata 1/1");

            //create Movies folder if it doesn't exist
            if (!Directory.Exists($@"{ObjectProvider.Settings.Generic.DownloadDirectory}\Movies"))
                Directory.CreateDirectory($@"{ObjectProvider.Settings.Generic.DownloadDirectory}\Movies");

            var movie = GetMovieObjectFromSelection();
            var dlInfo = movie.StreamInformation;
            dlInfo.DownloadPath = ObjectProvider.Settings.Generic.DownloadDirectory + @"\Movies";
            ObjectProvider.Queue.Add(dlInfo);
        }

        private void WkrGetMetadata_DoWork(object sender, DoWorkEventArgs e)
            => GetMetadata();

        private void GetMetadata()
        {
            try
            {
                //set needed globals
                ObjectProvider.Engine = new HttpDownloadQueue();

                //calculations for bytes per second (global is in Mbps NOT MBps)
                var gSpeed = ObjectProvider.Settings.Generic.DownloadSpeedLimit;

                //convert megabits per second to bytes per second
                var speed = gSpeed > 0
                    ? (long)Math.Round(ObjectProvider.Settings.Generic.DownloadSpeedLimit / (decimal)0.000008)
                    : 0;

                //apply queue download speed limit
                ObjectProvider.Engine.MaxSpeedInBytes = speed;

                //clear and reset the global download queue
                ObjectProvider.Queue = new List<StreamInfo>();

                //log status
                LoggingHelpers.RecordGeneralEntry(@"Metadata worker started");
                LoggingHelpers.RecordGeneralEntry(@"Doing directory checks");

                //invalid download directory specified?
                if (string.IsNullOrWhiteSpace(ObjectProvider.Settings.Generic.DownloadDirectory))

                    //yes, reset to the default download directory
                    ResetDownloadDirectory();

                //log status
                LoggingHelpers.RecordGeneralEntry(@"Grabbing metadata");

                //switch current content type
                switch (ObjectProvider.CurrentContentType)
                {
                    //TV Show
                    case ContentType.TvShows:

                        //log status
                        LoggingHelpers.RecordGeneralEntry(@"Worker is to grab TV Show metadata");

                        //
                        WkrGrabTv();

                        //exit
                        break;

                    //Movies
                    case ContentType.Movies:

                        //log status
                        LoggingHelpers.RecordGeneralEntry(@"Worker is to grab Movie metadata");
                        WkrGrabMovie();

                        //exit
                        break;

                    //Music
                    case ContentType.Music:

                        //log status
                        LoggingHelpers.RecordGeneralEntry(@"Worker is to grab Music metadata");
                        WkrGrabMusic();

                        //exit
                        break;
                }

                //log status
                LoggingHelpers.RecordGeneralEntry("Worker is to invoke downloader thread");

                //invoke the UI
                BeginInvoke((MethodInvoker)delegate
                {
                    //start the download procedure using the global queue
                    StartDownload(ObjectProvider.Queue);

                    //log status
                    LoggingHelpers.RecordGeneralEntry("Worker has started the download process");
                });
            }
            catch (ThreadAbortException)
            {
                //literally nothing; this gets raised when a cancellation happens.
            }
            catch (Exception ex)
            {
                //set UI status
                SetProgressLabel(@"Errored - Check Log");

                //log error
                LoggingHelpers.RecordException(ex.Message, "MetadataWkrError");

                //alert the user
                UIMessages.Error("Error occurred whilst getting needed metadata:\n\n" + ex);
            }
        }

        private void RenderMoviesView(DataTable content)
        {
            //was a null table provided?
            if (content == null)

                //null table; exit
                return;

            //clear all views
            ClearTvViews();
            ClearContentView();
            ClearMusicViews();

            //setup the values for rendering
            var wantedColumns = ObjectProvider.Settings.DataDisplay.MoviesView.DisplayColumns;
            var wantedCaption = ObjectProvider.Settings.DataDisplay.MoviesView.DisplayCaptions;

            //rendering information
            var info = new GenericRenderStruct
            {
                //data to render
                Data = content,

                //columns to render
                WantedColumns = wantedColumns,

                //captions for the columns to render
                WantedCaption = wantedCaption
            };

            //setup the titles view table via rendering
            DataProvider.TitlesProvider.SetViewTable(GenericViewRenderer.RenderView(info, dgvMovies));

            //shift the selected tab to the Movies tab
            SelectMoviesTab();
        }

        private void SelectMoviesTab()
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { tabMain.SelectedTab = tabMovies; });
            else
                tabMain.SelectedTab = tabMovies;
        }

        private void SelectTvTab()
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { tabMain.SelectedTab = tabTV; });
            else
                tabMain.SelectedTab = tabTV;
        }

        private void SelectMusicTab()
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { tabMain.SelectedTab = tabMusic; });
            else
                tabMain.SelectedTab = tabMusic;
        }

        private void ClearContentView()
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { dgvMovies.DataSource = null; });
            else
                dgvMovies.DataSource = null;
        }

        private void ClearTvViews()
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
               {
                   dgvSeasons.DataSource = null;
                   dgvEpisodes.DataSource = null;
                   dgvTVShows.DataSource = null;
               });
            }
            else
            {
                dgvSeasons.DataSource = null;
                dgvEpisodes.DataSource = null;
                dgvTVShows.DataSource = null;
            }
        }

        private void ClearLibraryViews()
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { dgvSections.DataSource = null; });
            else
                dgvSections.DataSource = null;
        }

        private void ClearMusicViews()
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
               {
                   dgvArtists.DataSource = null;
                   dgvAlbums.DataSource = null;
                   dgvTracks.DataSource = null;
               });
            }
            else
            {
                dgvArtists.DataSource = null;
                dgvAlbums.DataSource = null;
                dgvTracks.DataSource = null;
            }
        }

        private void RenderTvView(DataTable content)
        {
            if (content == null) return;

            ClearTvViews();
            ClearContentView();
            ClearMusicViews();

            var wantedColumns = ObjectProvider.Settings.DataDisplay.TvView.DisplayColumns;
            var wantedCaption = ObjectProvider.Settings.DataDisplay.TvView.DisplayCaptions;

            var info = new GenericRenderStruct
            {
                Data = content,
                WantedColumns = wantedColumns,
                WantedCaption = wantedCaption
            };

            DataProvider.TitlesProvider.SetViewTable(GenericViewRenderer.RenderView(info, dgvTVShows));

            SelectTvTab();
        }

        private void RenderArtistsView(DataTable content)
        {
            if (content != null)
            {
                ClearTvViews();
                ClearContentView();
                ClearMusicViews();

                var wantedColumns = ObjectProvider.Settings.DataDisplay.ArtistsView.DisplayColumns;
                var wantedCaption = ObjectProvider.Settings.DataDisplay.ArtistsView.DisplayCaptions;

                var info = new GenericRenderStruct
                {
                    Data = content,
                    WantedColumns = wantedColumns,
                    WantedCaption = wantedCaption
                };

                DataProvider.TitlesProvider.SetViewTable(GenericViewRenderer.RenderView(info, dgvArtists));

                SelectMusicTab();
            }
        }

        private void RenderSeasonsView(DataTable content)
        {
            if (content != null)
            {
                var wantedColumns = ObjectProvider.Settings.DataDisplay.SeriesView.DisplayColumns;
                var wantedCaption = ObjectProvider.Settings.DataDisplay.SeriesView.DisplayCaptions;

                var info = new GenericRenderStruct
                {
                    Data = content,
                    WantedColumns = wantedColumns,
                    WantedCaption = wantedCaption
                };

                DataProvider.SeasonsProvider.SetViewTable(GenericViewRenderer.RenderView(info, dgvSeasons));
            }
        }

        private void RenderEpisodesView(DataTable content)
        {
            if (content != null)
            {
                var wantedColumns = ObjectProvider.Settings.DataDisplay.EpisodesView.DisplayColumns;
                var wantedCaption = ObjectProvider.Settings.DataDisplay.EpisodesView.DisplayCaptions;

                var info = new GenericRenderStruct
                {
                    Data = content,
                    WantedColumns = wantedColumns,
                    WantedCaption = wantedCaption
                };

                DataProvider.EpisodesProvider.SetViewTable(GenericViewRenderer.RenderView(info, dgvEpisodes));
            }
        }

        private void RenderAlbumsView(DataTable content)
        {
            if (content != null)
            {
                var wantedColumns = ObjectProvider.Settings.DataDisplay.AlbumsView.DisplayColumns;
                var wantedCaption = ObjectProvider.Settings.DataDisplay.AlbumsView.DisplayCaptions;

                var info = new GenericRenderStruct
                {
                    Data = content,
                    WantedColumns = wantedColumns,
                    WantedCaption = wantedCaption
                };

                DataProvider.AlbumsProvider.SetViewTable(GenericViewRenderer.RenderView(info, dgvAlbums));
            }
        }

        private void RenderTracksView(DataTable content)
        {
            if (content != null)
            {
                var wantedColumns = ObjectProvider.Settings.DataDisplay.TracksView.DisplayColumns;
                var wantedCaption = ObjectProvider.Settings.DataDisplay.TracksView.DisplayCaptions;

                var info = new GenericRenderStruct
                {
                    Data = content,
                    WantedColumns = wantedColumns,
                    WantedCaption = wantedCaption
                };

                DataProvider.TracksProvider.SetViewTable(GenericViewRenderer.RenderView(info, dgvTracks));
            }
        }

        private void RenderLibraryView(DataTable content, bool renderKey = false)
        {
            if (content == null) return;

            var wantedColumns = ObjectProvider.Settings.DataDisplay.LibraryView.DisplayColumns;
            var wantedCaption = ObjectProvider.Settings.DataDisplay.LibraryView.DisplayCaptions;

            if (renderKey)
            {
                wantedColumns = wantedColumns.Prepend("key").ToList();
                wantedCaption = wantedCaption.Prepend("Key").ToList();
            }

            var info = new GenericRenderStruct
            {
                Data = content,
                WantedColumns = wantedColumns,
                WantedCaption = wantedCaption
            };

            GenericViewRenderer.RenderView(info, dgvSections);
        }

        private void CancelDownload(bool silent = false, string msg = "Download Cancelled")
        {
            //try and kill the worker if it's still trying to do something
            if (wkrGetMetadata.IsBusy) wkrGetMetadata.Abort();

            //check if the Engine's still running; if it is, we can then cancel and clear the download queue.
            if (Flags.IsEngineRunning)
            {
                ObjectProvider.Engine.Cancel();
                ObjectProvider.Engine.Clear();
            }

            //only run the rest if a download is actually running; we've killed the engine, now we need to set the appropriate
            //flags and values.
            if (Flags.IsDownloadRunning)
            {
                //gui settings functions
                SetProgressLabel(msg);
                SetDlOrderLabel(@"~");
                SetSpeedLabel(@"~");
                SetEtaLabel(@"~");
                SetDownloadStart();
                SetResume();

                //misc. gui settings
                pbMain.Value = pbMain.Minimum;
                btnPause.Enabled = false;

                //log download cancelled message
                LoggingHelpers.RecordGeneralEntry(msg);

                //set project global flags
                Flags.IsDownloadRunning = false;
                Flags.IsDownloadPaused = false;
                Flags.IsEngineRunning = false;
                Flags.IsHttpDownloadQueueCancelled = true;

                //set form global indices
                DownloadsSoFar = 0;
                DownloadTotal = 0;
                DownloadIndex = 0;

                if (!silent)
                    UIMessages.Error(msg);
            }
        }

        private void StartDownloadEngine()
        {
            ObjectProvider.Engine.QueueProgressChanged += Engine_DownloadProgressChanged;
            ObjectProvider.Engine.QueueCompleted += Engine_DownloadCompleted;

            ObjectProvider.Engine.StartAsync();
            //UIMessages.Info("Started!");
            LoggingHelpers.RecordGeneralEntry("Download is Progressing");
            Flags.IsDownloadRunning = true;
            Flags.IsEngineRunning = true;
            Flags.IsDownloadPaused = false;
            SetPause();
        }

        private void SetDownloadDirectory()
        {
            if (fbdDownloadPath.ShowDialog() != DialogResult.OK) return;

            ObjectProvider.Settings.Generic.DownloadDirectory = fbdDownloadPath.SelectedPath;
            UIMessages.Info($"Download directory updated to {ObjectProvider.Settings.Generic.DownloadDirectory}");
            LoggingHelpers.RecordGeneralEntry("Download directory updated to " +
                                              ObjectProvider.Settings.Generic.DownloadDirectory);
        }

        private void SetDownloadCompleted(string msg = "Download Completed")
        {
            //gui settings functions
            SetProgressLabel(msg);
            SetDlOrderLabel(@"~");
            SetSpeedLabel(@"~");
            SetEtaLabel(@"~");
            SetDownloadStart();
            SetResume();

            //misc. gui settings
            pbMain.Value = pbMain.Maximum;
            btnPause.Enabled = false;

            //log download completed
            LoggingHelpers.RecordGeneralEntry(msg);

            //clear the download queue (just in case)
            ObjectProvider.Engine.Clear();

            //set the global project flags
            Flags.IsDownloadRunning = false;
            Flags.IsDownloadPaused = false;
            Flags.IsEngineRunning = false;
        }

        private void StartDownload(IReadOnlyList<StreamInfo> queue)
        {
            LoggingHelpers.RecordGeneralEntry("Download Process Started");
            pbMain.Value = 0;

            LoggingHelpers.RecordGeneralEntry("Starting HTTP Engine");
            ObjectProvider.Engine = new HttpDownloadQueue();
            if (queue.Count > 1)
            {
                foreach (var dl in queue)
                {
                    var fqPath = dl.DownloadPath + @"\" + dl.FileName;
                    if (File.Exists(fqPath))
                        LoggingHelpers.RecordGeneralEntry(dl.FileName + " already exists; will not download.");
                    else
                        ObjectProvider.Engine.Add(dl.Links.Download, fqPath);
                }
            }
            else
            {
                var dl = queue[0];
                var fqPath = dl.DownloadPath + @"\" + dl.FileName;
                if (File.Exists(fqPath))
                {
                    LoggingHelpers.RecordGeneralEntry(dl.FileName + " already exists; get user confirmation.");

                    if (UIMessages.Question($@"{dl.FileName} already exists. Skip this title?"))
                    {
                        SetDownloadCompleted();
                        return;
                    }
                }

                ObjectProvider.Engine.Add(dl.Links.Download, fqPath);
            }

            btnPause.Enabled = true;
            StartDownloadEngine();
        }

        private void ShowBalloon(string msg, string title, bool error = false, int timeout = 2000)
        {
            if (!InvokeRequired)
            {
                nfyMain.BalloonTipIcon = error ? ToolTipIcon.Error : ToolTipIcon.Info;

                nfyMain.BalloonTipText = msg;
                nfyMain.BalloonTipTitle = title;
                nfyMain.ShowBalloonTip(timeout);
            }
            else
            {
                BeginInvoke((MethodInvoker)delegate
               {
                   nfyMain.BalloonTipIcon = error ? ToolTipIcon.Error : ToolTipIcon.Info;

                   nfyMain.BalloonTipText = msg;
                   nfyMain.BalloonTipTitle = title;
                   nfyMain.ShowBalloonTip(timeout);
               });
            }
        }

        private void Engine_DownloadCompleted(object sender, EventArgs e)
        {
            ShowBalloon("Download completed!", "Message");
            SetDownloadCompleted();
        }

        private void Engine_DownloadProgressChanged(object sender, EventArgs e)
        {
            try
            {
                //engine values - very important information.

                //Double
                var engineProgress = ObjectProvider.Engine.CurrentProgress;

                //64-bit Long
                var bytesGet = ObjectProvider.Engine.BytesReceived;
                var engineSpeed = ObjectProvider.Engine.CurrentDownloadSpeed;
                var contentSize = ObjectProvider.Engine.CurrentContentLength;

                //proper formatting of engine data for display

                //Double
                var progress = Math.Round(engineProgress);

                //64-bit Long
                var speed = Methods.FormatBytes((long)engineSpeed) + "/s";
                var total = Methods.FormatBytes((long)contentSize);

                //String
                var order = ObjectProvider.Engine.CurrentIndex + 1 + "/" + ObjectProvider.Engine.QueueLength;
                var eta = @"~";

                //it'd be really bad if we tried to divide by 0 and 0
                if (bytesGet > 0 && contentSize > 0)
                {
                    //subtract the byte count we already have from the total we need
                    var diff = contentSize - bytesGet;

                    //~needs to be in millisecond format; so * seconds by 1000~
                    var val = diff / engineSpeed * 1000;

                    //this converts the raw "ETA" data into human-readable information, then sets it up for display.
                    eta = Methods.CalculateTime(val);
                }

                //gui settings functions
                SetProgressLabel(progress + "% of " + total);
                SetDlOrderLabel(order);
                SetSpeedLabel(speed);
                SetEtaLabel(eta);

                //misc. gui settings
                pbMain.Value = (int)progress;

                //UIMessages.Info("Started!");
            }
            catch (Exception ex)
            {
                //gui settings functions
                SetDlOrderLabel(@"~");
                SetSpeedLabel(@"~");
                SetEtaLabel(@"~");
                SetProgressLabel("Download Status Error(s) Occurred - Check Log");

                //log the error
                LoggingHelpers.RecordException(ex.Message, "DLProgressError");
            }
        }

        private void ClearSearch(bool renderTables = true)
        {
            if (Flags.IsFiltered)
            {
                if (renderTables)
                    switch (ObjectProvider.CurrentContentType)
                    {
                        case ContentType.TvShows:
                            RenderTvView(DataProvider.TitlesProvider.GetRawTable());
                            break;

                        case ContentType.Movies:
                            RenderMoviesView(DataProvider.TitlesProvider.GetRawTable());
                            break;

                        case ContentType.Music:
                            RenderArtistsView(DataProvider.TitlesProvider.GetRawTable());
                            break;
                    }

                DataProvider.FilteredProvider.ClearRawTable();
                DataProvider.FilteredProvider.ClearViewTable();

                Flags.IsFiltered = false;
                SetStartSearch();
            }
        }

        private void RunTitleSearch()
        {
            try
            {
                LoggingHelpers.RecordGeneralEntry("Title search requested");
                if (dgvMovies.Rows.Count > 0 || dgvTVShows.Rows.Count > 0 || dgvArtists.Rows.Count > 0)
                {
                    GenericRenderStruct info = null;
                    DataGridView dgv = null;

                    switch (ObjectProvider.CurrentContentType)
                    {
                        case ContentType.TvShows:
                            dgv = dgvTVShows;
                            info = new GenericRenderStruct
                            {
                                Data = DataProvider.TitlesProvider.GetRawTable(),
                                WantedCaption = ObjectProvider.Settings.DataDisplay.TvView.DisplayCaptions,
                                WantedColumns = ObjectProvider.Settings.DataDisplay.TvView.DisplayColumns
                            };
                            break;

                        case ContentType.Movies:
                            dgv = dgvMovies;
                            info = new GenericRenderStruct
                            {
                                Data = DataProvider.TitlesProvider.GetRawTable(),
                                WantedCaption = ObjectProvider.Settings.DataDisplay.MoviesView.DisplayCaptions,
                                WantedColumns = ObjectProvider.Settings.DataDisplay.MoviesView.DisplayColumns
                            };
                            break;

                        case ContentType.Music:
                            dgv = dgvArtists;
                            info = new GenericRenderStruct
                            {
                                Data = DataProvider.TitlesProvider.GetRawTable(),
                                WantedCaption = ObjectProvider.Settings.DataDisplay.ArtistsView.DisplayCaptions,
                                WantedColumns = ObjectProvider.Settings.DataDisplay.ArtistsView.DisplayColumns
                            };
                            break;
                    }

                    //UIMessages.Info(info.Data.Rows.Count.ToString());

                    if (Search.RunTitleSearch(dgv, info, true))
                    {
                        Flags.IsFiltered = true;
                        SetCancelSearch();
                    }
                    else
                    {
                        Flags.IsFiltered = false;
                        DataProvider.FilteredProvider.ClearRawTable();
                        DataProvider.FilteredProvider.ClearViewTable();
                        SetStartSearch();
                    }
                }
                else
                {
                    LoggingHelpers.RecordGeneralEntry("No data to search");
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "SearchError");
                UIMessages.Error(ex.ToString());
            }
        }

        private void SetProgressLabel(string status)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { lblProgress.Text = status; });
            else
                lblProgress.Text = status;
        }

        private void SetSpeedLabel(string speed)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { lblSpeedValue.Text = speed; });
            else
                lblSpeedValue.Text = speed;
        }

        private void SetDlOrderLabel(string dlOrder)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { lblDownloadingValue.Text = dlOrder; });
            else
                lblDownloadingValue.Text = dlOrder;
        }

        private void SetEtaLabel(string eta)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { lblEtaValue.Text = eta; });
            else
                lblEtaValue.Text = eta;
        }

        private void SetDownloadCancel()
        {
            btnDownload.Text = @"Cancel";
        }

        private void SetDownloadStart()
        {
            btnDownload.Text = @"Download";
        }

        private void SetPause()
        {
            btnPause.Text = @"Pause";
        }

        private void SetResume()
        {
            btnPause.Text = @"Resume";
        }

        private void SetStartSearch()
        {
            itmStartSearch.Text = @"Start Search";
            if (DataProvider.TitlesProvider.GetRawTable() != null)
                SetViewingStatus();
        }

        private void SetCancelSearch()
        {
            if (DataProvider.FilteredProvider.GetRawTable() != null && DataProvider.TitlesProvider.GetRawTable() != null)
                SetViewingStatus();
            itmStartSearch.Text = @"Clear Search";
        }

        private void SetConnect()
        {
            itmDisconnect.Enabled = false;
            SetViewingStatus(true);
        }

        private void SetDisconnect()
        {
            itmDisconnect.Enabled = true;
        }

        private void LoadDevStatus()
        {
            var choc = Color.Chocolate;
            var red = Color.DarkRed;
            var green = Color.DarkGreen;
            switch (BuildState.State)
            {
                case DevStatus.InDevelopment:
                    lblDevStatus.ForeColor = choc;
                    lblDevStatus.Text = @"Developer Build";
                    break;

                case DevStatus.InBeta:
                    lblDevStatus.ForeColor = red;
                    lblDevStatus.Text = @"Beta Testing Build";
                    break;

                case DevStatus.ProductionReady:
                    lblDevStatus.ForeColor = green;
                    lblDevStatus.Text = @"Production Build";
                    break;
            }
        }

        private bool ShutdownActioner()
        {
            //is the download engine running?
            if (Flags.IsDownloadRunning || Flags.IsEngineRunning)
            {
                //check if we have already asked the user
                if (Flags.IsMsgAlreadyShown)

                    //yes, exit immediately and do not show the prompt
                    return false;

                //ask the user their preference
                if (UIMessages.Question(@"Are you sure you want to exit PlexDL? A download is still running."))
                {
                    //disable showing this question again
                    Flags.IsMsgAlreadyShown = true;

                    //log PlexDL has exited
                    LoggingHelpers.RecordGeneralEntry("PlexDL Exited");
                }
                else

                    //don't exit
                    return true;
            }
            else

                //log PlexDL has exited
                LoggingHelpers.RecordGeneralEntry("PlexDL Exited");

            //default
            return false;
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
            => e.Cancel = ShutdownActioner();

        private static void ResetDownloadDirectory()
        {
            //the My Videos folder of the current user
            var curUser = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

            //assign the My Videos folder \PlexDL as the default download directory
            ObjectProvider.Settings.Generic.DownloadDirectory = curUser + @"\PlexDL";

            //ensure this directory definitely exists
            if (!Directory.Exists(ObjectProvider.Settings.Generic.DownloadDirectory))
                Directory.CreateDirectory(ObjectProvider.Settings.Generic.DownloadDirectory);
        }

        private void SetSessionId()
            => lblSidValue.Text = Strings.CurrentSessionId;

        private void Home_Move(object sender, EventArgs e)
            => SetDebugLocation();

        private void SetDebugLocation()
        {
            //ensure the debug form and debug mode is setup correctly
            if (ObjectProvider.DebugForm != null && Flags.IsDebug)
            {
                //current location of the Home form
                var currentPoint = Location;

                //top-right corner of Home form
                var x = currentPoint.X + Width;

                //same y as Home form
                var y = currentPoint.Y;

                //assign the debug form location
                ObjectProvider.DebugForm.Location = new Point(x, y);
            }
        }

        private void DebugFormSetup()
        {
            //is Debug mode enabled?
            if (Flags.IsDebug)
            {
                //setup new debug form instance
                ObjectProvider.DebugForm = new Debug();

                //configure the debug form screen location
                SetDebugLocation();

                //screen center origin
                var primaryWidth = Screen.PrimaryScreen.Bounds.Width;

                //reset Home form and debug form to center of screen
                var combinedWidth = Width + ObjectProvider.DebugForm.Width;

                //new x position
                var x = (primaryWidth / 2) - (combinedWidth / 2);

                //assign new x value
                Location = new Point(x, Location.Y);

                //show the debug form
                ObjectProvider.DebugForm.Show();
            }
        }

        private void InitialStartup()
        {
            try
            {
                //did the user enable automatic update checking?
                if (ObjectProvider.Settings.Generic.AutoUpdateEnabled)

                    //yes, perform a silent update check but alert them if one is available
                    UpdateManager.RunUpdateCheck(true);

                //generic initial setup
                DebugFormSetup();
                SetSessionId();
                LoadDevStatus();
                ResetDownloadDirectory();

                //logging
                LoggingHelpers.RecordGeneralEntry("PlexDL Started");
                LoggingHelpers.RecordGeneralEntry($"Data location: {Strings.PlexDlAppData}");
                LoggingHelpers.RecordCacheEvent($"Using cache directory: {CachingFileDir.RootCacheDirectory}", "N/A");
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "StartupError");

                //alert the user
                UIMessages.Error("Startup Error:\n\n" + ex, "Startup Error");
            }
        }

        private void Home_Load(object sender, EventArgs e)
            => InitialStartup();

        private void UpdateFromLibraryKey(string key, ContentType type)
            => WaitWindow.WaitWindow.Show(UpdateFromLibraryKey, @"Getting Metadata", key, type);

        private void UpdateFromLibraryKey(object sender, WaitWindowEventArgs e)
        {
            var type = (ContentType)e.Arguments[1];
            var key = (string)e.Arguments[0];

            try
            {
                Flags.IsInitialFill = true;

                LoggingHelpers.RecordGeneralEntry(@"Requesting library contents");

                var contentUri = Strings.CurrentApiUri + key + @"/all/?X-Plex-Token=";
                var contentXml = XmlGet.GetXmlTransaction(contentUri, false, false, false);

                UpdateContentView(contentXml, type);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "UpdateLibraryError");
                UIMessages.Error(ex.ToString());
            }
        }

        private void CxtLibrarySections_Opening(object sender, CancelEventArgs e)
        {
            if (dgvSections.Rows.Count == 0)
                e.Cancel = true;
        }

        private void DgvSections_OnRowChange(object sender, EventArgs e)
        {
            LibrarySectionChange();
        }

        private void LibrarySectionChange()
        {
            if (dgvSections.SelectedRows.Count != 1 || !Flags.IsLibraryFilled) return;

            LoggingHelpers.RecordGeneralEntry("Selection Changed");

            //don't re-render the grids when clearing the search; this would end badly for performance reasons.
            ClearSearch(false);

            //log search cleared
            LoggingHelpers.RecordGeneralEntry("Cleared possible searches");

            //match the current row to a row in the raw data and then return the index of such
            var index = TableManager.GetTableIndexFromDgv(dgvSections, DataProvider.SectionsProvider.GetRawTable());

            //get the row object using the index above
            var r = RowGet.GetDataRowLibrary(index);

            //values necessary for traversal
            var key = "";
            var type = "";

            //null validation
            if (r != null)
            {
                //is the key column set?
                if (r["key"] != null)

                    //yes, convert to string
                    key = r["key"].ToString();

                //is the type column set?
                if (r["type"] != null)

                    //yes, convert to string
                    type = r["type"].ToString();
            }

            //switch through the content type specified
            switch (type)
            {
                //TV Show
                case "show":
                    UpdateFromLibraryKey(key, ContentType.TvShows);
                    break;

                //Movie
                case "movie":
                    UpdateFromLibraryKey(key, ContentType.Movies);
                    break;

                //Music
                case "artist":
                    UpdateFromLibraryKey(key, ContentType.Music);
                    break;
            }
        }

        private void DgvSeasons_OnRowChange(object sender, EventArgs e)
        {
            if (dgvSeasons.SelectedRows.Count != 1)
                return;

            var index = TableManager.GetTableIndexFromDgv(dgvSeasons, DataProvider.SeasonsProvider.GetRawTable());
            var episodes = XmlMetadataLists.GetEpisodeListXml(index);

            UpdateEpisodesView(episodes.Xml);
        }

        private void DgvAlbums_OnRowChange(object sender, EventArgs e)
        {
            if (dgvAlbums.SelectedRows.Count != 1)
                return;

            var index = TableManager.GetTableIndexFromDgv(dgvAlbums, DataProvider.AlbumsProvider.GetRawTable());
            var tracks = XmlMetadataLists.GetTracksListXml(index);

            UpdateTracksView(tracks.Xml);
        }

        private void DoubleClickLaunch()
        {
            PlexObject stream = null;

            switch (ObjectProvider.CurrentContentType)
            {
                case ContentType.Movies:
                    if (dgvMovies.SelectedRows.Count == 1)
                    {
                        var obj = GetMovieObjectFromSelection();
                        if (obj != null)
                            stream = obj;
                        else
                            LoggingHelpers.RecordGeneralEntry("Double-click stream failed; null object.");
                    }

                    break;

                case ContentType.TvShows:
                    if (dgvEpisodes.SelectedRows.Count == 1 && dgvTVShows.SelectedRows.Count == 1)
                    {
                        var obj = GetTvObjectFromSelection();
                        if (obj != null)
                            stream = obj;
                        else
                            LoggingHelpers.RecordGeneralEntry("Double-click stream failed; null object.");
                    }

                    break;

                case ContentType.Music:
                    if (dgvTracks.SelectedRows.Count == 1 && dgvArtists.SelectedRows.Count == 1)
                    {
                        var obj = GetMusicObjectFromSelection();
                        if (obj != null)
                            stream = obj;
                        else
                            LoggingHelpers.RecordGeneralEntry("Double-click stream failed; null object.");
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            //null stream; exit operation.
            if (stream == null) return;

            if (ObjectProvider.Settings.Player.PlaybackEngine == PlaybackMode.MenuSelector)
                StartStreaming(stream,
                    VlcLauncher.VlcInstalled() ? PlaybackMode.VlcPlayer : PlaybackMode.PvsPlayer);
            else
                StartStreaming(stream);
        }

        private void DoubleClickProcessor(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderType = sender.GetType();
                var gridType = typeof(FlatDataGridView);
                if (senderType == gridType)
                {
                    var gridView = (FlatDataGridView)sender;
                    if (ObjectProvider.Settings.Generic.DoubleClickLaunch && gridView.IsContentTable)
                    {
                        if (gridView.SelectedRows.Count == 1)
                            DoubleClickLaunch();
                    }
                    else
                    {
                        if (gridView.Rows.Count <= 0) return;
                        var value = gridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        UIMessages.Info(value, @"Cell Content");
                    }
                }
                else
                {
                    LoggingHelpers.RecordGeneralEntry(
                        "Double-click launch failed; incorrect object type. Expecting object of type FlatDataGridView.");
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "DoubleClickError");
                LoggingHelpers.RecordGeneralEntry(
                    "Double-click launch failed; an error occurred. Check exception log for more information.");
            }
        }

        //debugging stuff
        /*
                private void XmlMessageBox(XmlNode doc)
                {
                    if (doc != null)
                    {
                        using (var sww = new StringWriter())
                        using (var writer = XmlWriter.Create(sww))
                        {
                            doc.WriteTo(writer);
                            writer.Flush();
                            UIMessages.Info(sww.GetStringBuilder().ToString());
                        }
                    }
                    else
                        UIMessages.Info(@"XML Document was null");
                }
        */

        private void DgvTVShows_OnRowChange(object sender, EventArgs e)
        {
            if (dgvTVShows.SelectedRows.Count != 1) return;

            var index = TableManager.GetTableIndexFromDgv(dgvTVShows, TableManager.ReturnCorrectTable());

            //debugging
            //UIMessages.Info(index.ToString());

            if (ObjectProvider.CurrentContentType != ContentType.TvShows) return;

            var series = XmlMetadataLists.GetSeriesListXml(index);
            //debugging
            //XmlMessageBox(series);
            UpdateSeriesView(series.Xml);
        }

        private void DgvArtists_OnRowChange(object sender, EventArgs e)
        {
            if (dgvArtists.SelectedRows.Count != 1) return;

            var index = TableManager.GetTableIndexFromDgv(dgvArtists, TableManager.ReturnCorrectTable());

            //debugging
            //UIMessages.Info(index.ToString());

            if (ObjectProvider.CurrentContentType != ContentType.Music) return;

            var albums = XmlMetadataLists.GetAlbumsListXml(index);
            //debugging
            //XmlMessageBox(series);
            UpdateAlbumsView(albums.Xml);
        }

        private void DoDownloadAll()
        {
            LoggingHelpers.RecordGeneralEntry("Awaiting download safety checks");
            if (!Flags.IsDownloadRunning && !Flags.IsEngineRunning)
            {
                LoggingHelpers.RecordGeneralEntry("Download process is starting");
                SetProgressLabel("Waiting");
                Flags.IsDownloadAll = true;
                DownloadTotal = TableManager.ReturnCorrectTable(true).Rows.Count;
                Flags.IsDownloadRunning = true;
                if (wkrGetMetadata.IsBusy) wkrGetMetadata.Abort();
                wkrGetMetadata.RunWorkerAsync();
                tmrWorkerTimeout.Start();
                LoggingHelpers.RecordGeneralEntry("Worker invoke process started");
                SetDownloadCancel();
            }
            else
            {
                LoggingHelpers.RecordGeneralEntry("Download process failed; download is already running.");
            }
        }

        private void DoDownloadSelected()
        {
            LoggingHelpers.RecordGeneralEntry("Awaiting download safety checks");
            //UIMessages.Info(ObjectProvider.CurrentContentType.ToString());
            if (!Flags.IsDownloadRunning && !Flags.IsEngineRunning)
            {
                LoggingHelpers.RecordGeneralEntry("Download process is starting");
                SetProgressLabel("Waiting");
                Flags.IsDownloadAll = false;
                DownloadTotal = 1;
                Flags.IsDownloadRunning = true;
                if (wkrGetMetadata.IsBusy) wkrGetMetadata.Abort();
                wkrGetMetadata.RunWorkerAsync();
                tmrWorkerTimeout.Start();
                LoggingHelpers.RecordGeneralEntry("Worker invoke process started");
                SetDownloadCancel();
            }
            else
            {
                LoggingHelpers.RecordGeneralEntry("Download process failed; download is already running.");
            }
        }

        private void StartStreaming()
        {
            try
            {
                if (!Flags.IsConnected || !Flags.IsLibraryFilled) return;

                PlexObject result = null;
                switch (ObjectProvider.CurrentContentType)
                {
                    case ContentType.Movies:
                        if (dgvMovies.SelectedRows.Count == 1)
                        {
                            result = GetMovieObjectFromSelection();
                        }
                        else
                        {
                            UIMessages.Error(@"No movie is selected", @"Validation Error");
                            return;
                        }

                        break;

                    case ContentType.TvShows:
                        if (dgvEpisodes.SelectedRows.Count == 1)
                        {
                            result = GetTvObjectFromSelection();
                        }
                        else
                        {
                            UIMessages.Error(@"No episode is selected", @"Validation Error");
                            return;
                        }

                        break;

                    case ContentType.Music:
                        if (dgvTracks.SelectedRows.Count == 1)
                            result = GetMusicObjectFromSelection();
                        else
                            UIMessages.Error(@"No track is selected", @"Validation Error");

                        break;
                }

                if (result != null)
                    StartStreaming(result);
                else
                    LoggingHelpers.RecordGeneralEntry("Couldn't start streaming; object was null.");
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "StartStreamError");
                LoggingHelpers.RecordGeneralEntry("Couldn't start streaming; an error occured.");
                UIMessages.Error("Streaming preparation error occurred:\n\n" + ex, "Start Stream Error");
            }
        }

        private void StartStreaming(PlexObject stream)
        {
            var def = ObjectProvider.Settings.Player.PlaybackEngine;
            StartStreaming(stream, def);
        }

        private void StartStreaming(PlexObject stream, PlaybackMode playbackEngine)
        {
            //so that cxtStreamOptions can access the current stream's information, a global object has to be used.
            ObjectProvider.CurrentStream = stream;
            switch (playbackEngine)
            {
                case PlaybackMode.PvsPlayer:
                    PvsLauncher.LaunchPvs(stream);
                    break;

                case PlaybackMode.VlcPlayer:
                    VlcLauncher.LaunchVlc(stream);
                    break;

                case PlaybackMode.Browser:
                    BrowserLauncher.LaunchBrowser(stream);
                    break;

                case PlaybackMode.MenuSelector:
                    {
                        //display the options menu where the MousePointer is, but adjust the height a bit so it isn't awkward to operate.
                        var loc = MousePosition;
                        loc.Y = loc.Y - cxtStreamOptions.Height / 2;
                        cxtStreamOptions.Show(loc);
                        break;
                    }
                default:
                    UIMessages.Warning($"Unrecognised Playback Mode (\"{playbackEngine}\")",
                        @"Playback Error");
                    LoggingHelpers.RecordGeneralEntry("Invalid Playback Mode: " + playbackEngine);
                    break;
            }
        }

        private void SearchProcedure()
        {
            if (Flags.IsFiltered)
                ClearSearch();
            else
                RunTitleSearch();
        }

        private void CxtEpisodeOptions_Opening(object sender, CancelEventArgs e)
        {
            if (dgvEpisodes.SelectedRows.Count == 0)
                e.Cancel = true;
        }

        private void CxtContentOptions_Opening(object sender, CancelEventArgs e)
        {
            if (dgvMovies.SelectedRows.Count == 0)
                e.Cancel = true;
        }

        private void Metadata(PlexObject result = null)
        {
            if (dgvMovies.SelectedRows.Count == 1 || dgvEpisodes.SelectedRows.Count == 1 ||
                dgvTracks.SelectedRows.Count == 1)
            {
                if (!Flags.IsDownloadRunning && !Flags.IsEngineRunning)
                {
                    if (result == null)
                        switch (ObjectProvider.CurrentContentType)
                        {
                            case ContentType.Movies:
                                result = GetMovieObjectFromSelection();
                                break;

                            case ContentType.TvShows:
                                result = GetTvObjectFromSelection();
                                break;

                            case ContentType.Music:
                                result = GetMusicObjectFromSelection();
                                break;
                        }

                    using (var frm = new Metadata())
                    {
                        frm.StreamingContent = result;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    UIMessages.Warning(@"You cannot view metadata while an internal download is running",
                        @"Validation Error");
                }
            }
            else if (dgvMovies.Rows.Count == 0 && dgvTVShows.Rows.Count == 0)
            {
                using (var frm = new Metadata())
                {
                    frm.StationaryMode = true;
                    frm.ShowDialog();
                }
            }
        }

        private void ShowLinkViewer(object sender, WaitWindowEventArgs e)
        {
            e.Result = ObjectFromSelection();
        }

        private void ShowLinkViewer(bool viewMode = true)
        {
            //fetch the selected object in the background
            var o = (PlexObject)WaitWindow.WaitWindow.Show(ShowLinkViewer, @"Getting link");

            //ensure a valid object was received
            if (o != null)

                //show the viewer
                LinkViewer.ShowLinkViewer(o, viewMode);
            else

                //alert the user to the null error
                UIMessages.Error(@"Content object was null; couldn't construct a link for you.");
        }

        private static void ShowLogViewer()
        {
            using (var frm = new LogViewer())
            {
                frm.ShowDialog();
            }
        }

        private void ItmTrackSearch_Click(object sender, EventArgs e)
        {
            SearchProcedure();
        }

        private void ItmEpisodeSearch_Click(object sender, EventArgs e)
        {
            SearchProcedure();
        }

        private void ItmContentSearch_Click(object sender, EventArgs e)
        {
            SearchProcedure();
        }

        private void ItmTrackMetadataExport_Click(object sender, EventArgs e)
        {
            DoStreamExport();
        }

        private void ItmEpisodeMetadataExport_Click(object sender, EventArgs e)
        {
            DoStreamExport();
        }

        private void ItmContentMetadataExport_Click(object sender, EventArgs e)
        {
            DoStreamExport();
        }

        private void GenericViewLinkHandler(object sender, EventArgs e)
            => ShowLinkViewer();

        private void GenericDownloadLinkHandler(object sender, EventArgs e)
            => ShowLinkViewer(false);

        private void ItmPxzExplorer_Click(object sender, EventArgs e)
            => PxzExplorer.ShowPxzExplorer();
    }
}