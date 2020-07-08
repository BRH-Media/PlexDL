using inet;
using LogDel;
using PlexDL.AltoHTTP.Classes;
using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.API.Metadata;
using PlexDL.Common.API.Objects;
using PlexDL.Common.Caching;
using PlexDL.Common.Components;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.PlayerLaunchers;
using PlexDL.Common.Renderers;
using PlexDL.Common.Renderers.DGVRenderers;
using PlexDL.Common.SearchFramework;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.AppOptions.Player;
using PlexDL.Common.Structures.Plex;
using PlexDL.Common.Update;
using PlexDL.PlexAPI;
using PlexDL.PlexAPI.LoginHandler;
using PlexDL.WaitWindow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using UIHelpers;
using Directory = System.IO.Directory;

//using System.Threading.Tasks;

namespace PlexDL.UI
{
    public partial class Home : Form
    {
        #region Form

        #region FormInitialiser

        public Home()
        {
            InitializeComponent();
            tabMain.SelectedIndex = 0;
        }

        #endregion FormInitialiser

        #endregion Form

        private void ManualSectionLoad()
        {
            if (dgvSections.Rows.Count <= 0) return;

            var ipt = GlobalStaticVars.LibUi.showInputForm("Enter section key", "Manual Section Lookup", true, "TV Library");
            if (ipt.txt == "!cancel=user")
                return;
            if (!int.TryParse(ipt.txt, out _))
                UIMessages.Error(@"Section key ust be numeric", @"Validation Error");
            else
                UpdateFromLibraryKey(ipt.txt, ipt.chkd ? ContentType.TvShows : ContentType.Movies);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((dgvMovies.SelectedRows.Count == 1 || dgvEpisodes.SelectedRows.Count == 1) && GlobalStaticVars.Settings.Generic.DoubleClickLaunch)
            {
                if (keyData == Keys.Enter)
                {
                    DoubleClickLaunch();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
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

        private void ItmEpisodeMetadata_Click(object sender, EventArgs e)
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

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
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
            //send this form to the front of the screen
            TopMost = true;
            TopMost = false;
        }

        private void ItmStreamInPVS_Click(object sender, EventArgs e)
        {
            cxtStreamOptions.Close();
            PvsLauncher.LaunchPvs(GlobalStaticVars.CurrentStream, GlobalTables.ReturnCorrectTable());
        }

        private void ItmStreamInVLC_Click(object sender, EventArgs e)
        {
            cxtStreamOptions.Close();
            VlcLauncher.LaunchVlc(GlobalStaticVars.CurrentStream);
        }

        private void ItmStreamInBrowser_Click(object sender, EventArgs e)
        {
            cxtStreamOptions.Close();
            BrowserLauncher.LaunchBrowser(GlobalStaticVars.CurrentStream);
        }

        private void DoConnectFromServer(Server s)
        {
            var address = s.address;
            var port = s.port;

            var connectInfo = new ConnectionInfo
            {
                PlexAccountToken = GlobalStaticVars.GetToken(),
                PlexAddress = address,
                PlexPort = port,
                RelaysOnly = GlobalStaticVars.Settings.ConnectionInfo.RelaysOnly
            };

            GlobalStaticVars.Settings.ConnectionInfo = connectInfo;

            var uri = GlobalStaticVars.GetBaseUri(true);
            //UIMessages.Info(uri);
            var reply = (XmlDocument)WaitWindow.WaitWindow.Show(XmlGet.GetXMLTransactionWorker, "Connecting", uri);
            Flags.IsConnected = true;

            if (GlobalStaticVars.Settings.Generic.ShowConnectionSuccess)
                UIMessages.Info(@"Connection successful!");

            SetProgressLabel("Connected");
            SetDisconnect();

            if (reply.ChildNodes.Count != 0)
                PopulateLibrary(reply);
        }

        private void ItmServerManager_Click(object sender, EventArgs e)
        {
            if (ConnectionChecker.CheckForInternetConnection())
                using (var frm = new ServerManager())
                {
                    if (frm.ShowDialog() != DialogResult.OK) return;

                    GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken = frm.SelectedServer.accessToken;
                    GlobalStaticVars.Settings.ConnectionInfo.PlexAddress = frm.SelectedServer.address;
                    GlobalStaticVars.Settings.ConnectionInfo.PlexPort = frm.SelectedServer.port;
                    GlobalStaticVars.Svr = frm.SelectedServer;
                    DoConnectFromServer(frm.SelectedServer);
                }
            else
                UIMessages.Error(
                    @"No internet connection. Please connect to a network before attempting to start a Plex server connection.",
                    @"Network Error");
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

        private void ItmExportObj_Click(object sender, EventArgs e)
        {
            if (Flags.IsConnected)
                DoStreamExport();
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            if ((dgvMovies.SelectedRows.Count != 1) && (dgvEpisodes.SelectedRows.Count != 1) &&
                (dgvTracks.SelectedRows.Count != 1)) return;
            if (!Flags.IsDownloadRunning && !Flags.IsEngineRunning)
            {
                GlobalStaticVars.Queue = new List<DownloadInfo>();
                switch (GlobalStaticVars.CurrentContentType)
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
                    GlobalStaticVars.Engine.Pause();
                    SetResume();
                    SetProgressLabel(lblProgress.Text + " (Paused)");
                    Flags.IsDownloadPaused = true;
                }
                else
                {
                    GlobalStaticVars.Engine.ResumeAsync();
                    SetPause();
                    Flags.IsDownloadPaused = false;
                }
            }
        }

        private void ItmSetDlDirectory_Click(object sender, EventArgs e)
        {
            SetDownloadDirectory();
        }

        private void BtnHTTPPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Flags.IsConnected || !Flags.IsLibraryFilled) return;

                PlexObject result = null;
                switch (GlobalStaticVars.CurrentContentType)
                {
                    case ContentType.Movies:
                        if (dgvMovies.SelectedRows.Count == 1)
                            result = (PlexMovie)WaitWindow.WaitWindow.Show(GetMovieObjectFromSelectionWorker,
                                "Getting Metadata", false);
                        else
                        {
                            UIMessages.Error(@"No movie is selected", @"Validation Error");
                            return;
                        }
                        break;

                    case ContentType.TvShows:
                        if (dgvEpisodes.SelectedRows.Count == 1)
                            result = (PlexTvShow)WaitWindow.WaitWindow.Show(GetTVObjectFromSelectionWorker,
                                "Getting Metadata", false);
                        else
                        {
                            UIMessages.Error(@"No episode is selected", @"Validation Error");
                            return;
                        }
                        break;

                    case ContentType.Music:
                        if (dgvTracks.SelectedRows.Count == 1)
                        {
                            result = (PlexMusic)WaitWindow.WaitWindow.Show(GetMusicObjectFromSelectionWorker,
                                "Getting Metadata", false);
                        }
                        else
                        {
                            UIMessages.Error(@"No track is selected", @"Validation Error");
                        }
                        break;
                }

                if (result != null)
                    StartStreaming(result);
                else
                {
                    LoggingHelpers.RecordGeneralEntry("Couldn't start streaming; object was null.");
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "StartStreamError");
                LoggingHelpers.RecordGeneralEntry("Couldn't start streaming; an error occured.");
                UIMessages.Error("Streaming preparation error occurred:\n\n" + ex, "Start Stream Error");
            }
        }

        private void ItmMetadata_Click(object sender, EventArgs e)
        {
            Metadata();
        }

        private void ItmLogViewer_Click(object sender, EventArgs e)
        {
            ShowLogViewer();
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
            using (var frm = new CachingMetricsUi())
            {
                frm.Metrics = CachingMetrics.FromLatest();
                frm.ShowDialog();
            }
        }

        private void ItmSettings_Click(object sender, EventArgs e)
        {
            using (var frm = new Settings())
            {
                frm.ShowDialog();
            }
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
                {
                    UIMessages.Error(@"There's no cached data to clear", @"Validation Error");
                }
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
            {
                dgvLog.DataSource = File.Exists(@"Logs\PlexDL.logdel") ? LogReader.TableFromFile(@"Logs\PlexDL.logdel", false) : null;
            }
            else
            {
                dgvLog.DataSource = null;
            }
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

        #region GlobalIntVariables

        public int DownloadIndex;
        public int DownloadTotal;
        public int DownloadsSoFar;

        #endregion GlobalIntVariables

        #region XML/Metadata

        #region PlexMovieBuilders

        private PlexMovie GetMovieObjectFromSelection(bool formatLinkDownload)
        {
            var obj = new PlexMovie();

            if (dgvMovies.SelectedRows.Count != 1) return obj;

            var index = GlobalTables.GetTableIndexFromDgv(dgvMovies);
            obj = ObjectBuilders.GetMovieObjectFromIndex(index, formatLinkDownload);

            return obj;
        }

        private PlexTvShow GetTvObjectFromSelection(bool formatLinkDownload)
        {
            var obj = new PlexTvShow();

            if (dgvTVShows.SelectedRows.Count != 1 || dgvEpisodes.SelectedRows.Count != 1) return obj;

            var index = GlobalTables.GetTableIndexFromDgv(dgvEpisodes, GlobalTables.EpisodesTable);
            obj = ObjectBuilders.GetTvObjectFromIndex(index, formatLinkDownload);

            return obj;
        }

        private PlexMusic GetMusicObjectFromSelection(bool formatLinkDownload)
        {
            var obj = new PlexMusic();

            if (dgvArtists.SelectedRows.Count != 1 || dgvTracks.SelectedRows.Count != 1) return obj;

            var index = GlobalTables.GetTableIndexFromDgv(dgvTracks, GlobalTables.TracksTable);
            obj = ObjectBuilders.GetMusicObjectFromIndex(index, formatLinkDownload);

            return obj;
        }

        #endregion PlexMovieBuilders

        #endregion XML/Metadata

        #region ProfileHelpers

        public void LoadProfile()
        {
            if (!Flags.IsConnected)
            {
                if (ofdLoadProfile.ShowDialog() == DialogResult.OK) DoLoadProfile(ofdLoadProfile.FileName);
            }
            else
            {
                UIMessages.Warning(@"You can't load profiles while you're connected; please disconnect first.",
                    @"Validation Error");
            }
        }

        public void SaveProfile()
        {
            if (string.IsNullOrEmpty(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken))
            {
                UIMessages.Warning(@"You need to authenticate before saving a profile", @"Validation Error");
            }
            else
            {
                if (sfdSaveProfile.ShowDialog() == DialogResult.OK) DoSaveProfile(sfdSaveProfile.FileName);
            }
        }

        public void DoSaveProfile(string fileName, bool silent = false)
        {
            try
            {
                ProfileImportExport.ProfileToFile(fileName, GlobalStaticVars.Settings, silent);

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
                var subReq = ProfileImportExport.ProfileFromFile(fileName, silent);

                try
                {
                    var vStoredVersion = new Version(subReq.Generic.StoredAppVersion);
                    var vThisVersion = new Version(Application.ProductVersion);
                    var vCompare = vThisVersion.CompareTo(vStoredVersion);
                    if (vCompare < 0)
                    {
                        if (!silent)
                        {
                            var result = UIMessages.Question(
                                "You're trying to load a profile made in a newer version of PlexDL. This could have several implications:\n" +
                                "- Possible data loss of your current profile if PlexDL overwrites it\n" +
                                "- Features may not work as intended\n" +
                                "- Increased risk of errors\n\n" +
                                "Are you sure you'd like to proceed?");
                            if (!result)
                                return;
                        }

                        LoggingHelpers.RecordGeneralEntry("Tried to load a profile made in a newer version: " + vStoredVersion + " > " + vThisVersion);
                    }
                    else if (vCompare > 0)
                    {
                        if (!silent)
                        {
                            var result = UIMessages.Question(
                                "You're trying to load a profile made in an earlier version of PlexDL. This could have several implications:\n" +
                                "- Possible data loss of your current profile if PlexDL overwrites it\n" +
                                "- Features may not work as intended\n" +
                                "- Increased risk of errors\n\n" +
                                "Are you sure you'd like to proceed?");
                            if (!result)
                                return;
                        }

                        LoggingHelpers.RecordGeneralEntry("Tried to load a profile made in an earlier version: " + vStoredVersion + " < " + vThisVersion);
                    }
                }
                catch (Exception ex)
                {
                    LoggingHelpers.RecordGeneralEntry("Version information load error: " + ex.Message);
                    LoggingHelpers.RecordException(ex.Message, "VersionLoadError");
                }

                if (subReq == null) return;

                GlobalStaticVars.Settings = subReq;

                if (!silent)
                    UIMessages.Info("Successfully loaded profile!");
                LoggingHelpers.RecordGeneralEntry("Loaded profile " + fileName);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "LoadProfileError");
                if (!silent)
                    UIMessages.Error(ex.ToString(), "Error in loading XML Profile");
            }
        }

        #endregion ProfileHelpers

        #region ConnectionHelpers

        private void Disconnect(bool silent = false)
        {
            if (!Flags.IsConnected) return;
            if (GlobalStaticVars.Engine != null) CancelDownload();

            ClearContentView();
            ClearTVViews();
            ClearMusicViews();
            ClearLibraryViews();
            SetProgressLabel(@"Disconnected from Plex");
            SetConnect();
            SelectMoviesTab();
            Flags.IsConnected = false;
            Flags.IsInitialFill = false;

            if (!silent)
                UIMessages.Info(@"Disconnected from Plex");
        }

        private void DoStreamExport(bool formatLinkDownload = true)
        {
            try
            {
                if ((dgvMovies.SelectedRows.Count == 1) || (dgvEpisodes.SelectedRows.Count == 1) || (dgvTracks.SelectedRows.Count == 1))
                {
                    PlexObject content = null;
                    switch (GlobalStaticVars.CurrentContentType)
                    {
                        case ContentType.Movies:
                            content = GetMovieObjectFromSelection(formatLinkDownload);
                            break;

                        case ContentType.TvShows:
                            content = GetTvObjectFromSelection(formatLinkDownload);
                            break;

                        case ContentType.Music:
                            content = GetMusicObjectFromSelection(formatLinkDownload);
                            break;
                    }

                    if (sfdExport.ShowDialog() == DialogResult.OK)
                        ImportExport.MetadataToFile(sfdExport.FileName, content);
                }
            }
            catch (Exception ex)
            {
                //log and ignore
                LoggingHelpers.RecordGeneralEntry("Export error: " + ex.Message);
                LoggingHelpers.RecordException(ex.Message, "StreamExportError");
            }
        }

        #endregion ConnectionHelpers

        #region Workers

        #region UpdateWorkers

        private void PopulateLibraryWorker(XmlDocument doc)
        {
            if (doc == null) return;

            try
            {
                LoggingHelpers.RecordGeneralEntry("Library population requested");
                var libraryDir = KeyGatherers.GetLibraryKey(doc).TrimEnd('/');
                var baseUri = GlobalStaticVars.GetBaseUri(false);
                var uriSectionKey = baseUri + libraryDir + "/?X-Plex-Token=";
                var xmlSectionKey = XmlGet.GetXmlTransaction(uriSectionKey);

                var sectionDir = KeyGatherers.GetSectionKey(xmlSectionKey).TrimEnd('/');
                var uriSections = baseUri + libraryDir + "/" + sectionDir + "/?X-Plex-Token=";
                var xmlSections = XmlGet.GetXmlTransaction(uriSections);

                LoggingHelpers.RecordGeneralEntry("Creating new datasets");
                var sections = new DataSet();
                sections.ReadXml(new XmlNodeReader(xmlSections));

                var sectionsTable = sections.Tables["Directory"];
                GlobalTables.SectionsTable = sectionsTable;

                LoggingHelpers.RecordGeneralEntry("Binding to grid");
                RenderLibraryView(sectionsTable);
                Flags.IsLibraryFilled = true;
                GlobalStaticVars.CurrentApiUri = baseUri + libraryDir + "/" + sectionDir + "/";
            }
            catch (WebException ex)
            {
                LoggingHelpers.RecordException(ex.Message, "LibPopError");
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    if (ex.Response is HttpWebResponse response)
                        switch ((int)response.StatusCode)
                        {
                            case 401:
                                UIMessages.Error(
                                    @"The web server denied access to the resource. Check your token and try again. (401)");
                                break;

                            case 404:
                                UIMessages.Error(
                                    @"The web server couldn't serve the request because it couldn't find the resource specified. (404)");
                                break;

                            case 400:
                                UIMessages.Error(
                                    @"The web server couldn't serve the request because the request was bad. (400)");
                                break;
                        }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "LibPopError");
                UIMessages.Error(ex.ToString());
            }
        }

        private void GetTitlesTable(XmlDocument doc, ContentType type)
        {
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            switch (type)
            {
                case ContentType.TvShows:
                    GlobalTables.TitlesTable = sections.Tables["Directory"];
                    break;

                case ContentType.Music:
                    GlobalTables.TitlesTable = sections.Tables["Directory"];
                    break;

                case ContentType.Movies:
                    GlobalTables.TitlesTable = sections.Tables["Video"];
                    break;
            }
        }

        private void UpdateContentViewWorker(XmlDocument doc, ContentType type)
        {
            LoggingHelpers.RecordGeneralEntry("Updating library contents");

            GetTitlesTable(doc, type);

            if (GlobalTables.TitlesTable != null)
            {
                //set this in the toolstrip so the user can see how many items are loaded
                lblViewingValue.Text = GlobalTables.TitlesTable.Rows.Count + "/" + GlobalTables.TitlesTable.Rows.Count;

                GlobalStaticVars.CurrentContentType = type;

                //UIMessages.Info(GlobalStaticVars.CurrentContentType.ToString());

                switch (type)
                {
                    case ContentType.Movies:
                        LoggingHelpers.RecordGeneralEntry("Rendering Movies");
                        RenderMoviesView(GlobalTables.TitlesTable);
                        break;

                    case ContentType.TvShows:
                        LoggingHelpers.RecordGeneralEntry("Rendering TV Shows");
                        RenderTVView(GlobalTables.TitlesTable);
                        break;

                    case ContentType.Music:
                        LoggingHelpers.RecordGeneralEntry("Rendering Artists");
                        RenderArtistsView(GlobalTables.TitlesTable);
                        break;
                }

                //UIMessages.Info("ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + GlobalTables.TitlesTable.Rows.Count.ToString());
            }
            else
            {
                LoggingHelpers.RecordGeneralEntry("Library contents were null; rendering did not occur");
            }
        }

        private void UpdateEpisodeViewWorker(XmlNode doc)
        {
            LoggingHelpers.RecordGeneralEntry("Updating episode contents");

            LoggingHelpers.RecordGeneralEntry("Creating datasets");
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            GlobalTables.EpisodesTable = sections.Tables["Video"];

            LoggingHelpers.RecordGeneralEntry("Cleaning unwanted data");

            LoggingHelpers.RecordGeneralEntry("Binding to grid");
            RenderEpisodesView(GlobalTables.EpisodesTable);

            //UIMessages.Info("ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + GlobalTables.TitlesTable.Rows.Count.ToString());
        }

        private void UpdateTracksViewWorker(XmlNode doc)
        {
            LoggingHelpers.RecordGeneralEntry("Updating track contents");

            LoggingHelpers.RecordGeneralEntry("Creating datasets");
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            GlobalTables.TracksTable = sections.Tables["Track"];

            LoggingHelpers.RecordGeneralEntry("Cleaning unwanted data");

            LoggingHelpers.RecordGeneralEntry("Binding to grid");
            RenderTracksView(GlobalTables.TracksTable);

            //UIMessages.Info("ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + GlobalTables.TitlesTable.Rows.Count.ToString());
        }

        private void UpdateSeriesViewWorker(XmlNode doc)
        {
            LoggingHelpers.RecordGeneralEntry("Updating series contents");

            LoggingHelpers.RecordGeneralEntry("Creating datasets");
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            GlobalTables.SeasonsTable = sections.Tables["Directory"];

            LoggingHelpers.RecordGeneralEntry("Cleaning unwanted data");

            LoggingHelpers.RecordGeneralEntry("Binding to grid");
            RenderSeasonsView(GlobalTables.SeasonsTable);

            //UIMessages.Info("ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + GlobalTables.TitlesTable.Rows.Count.ToString());
        }

        private void UpdateAlbumsViewWorker(XmlNode doc)
        {
            LoggingHelpers.RecordGeneralEntry("Updating album contents");

            LoggingHelpers.RecordGeneralEntry("Creating data-sets");
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            GlobalTables.AlbumsTable = sections.Tables["Directory"];

            LoggingHelpers.RecordGeneralEntry("Cleaning unwanted data");

            LoggingHelpers.RecordGeneralEntry("Binding to grid");
            RenderAlbumsView(GlobalTables.AlbumsTable);

            //UIMessages.Info("ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + GlobalTables.TitlesTable.Rows.Count.ToString());
        }

        #endregion UpdateWorkers

        #region BackgroundWorkers

        private void WkrGrabTv()
        {
            if (Flags.IsDownloadAll)
            {
                LoggingHelpers.RecordGeneralEntry(@"Worker is to grab metadata for All Episodes");

                var rowCount = GlobalTables.EpisodesTable.Rows.Count;

                for (var i = 0; i < rowCount; i++)
                {
                    SetProgressLabel(@"Getting Metadata " + (i + 1) + @"/" + rowCount);

                    var show = ObjectBuilders.GetTvObjectFromIndex(i, true);
                    var dlInfo = show.StreamInformation;
                    var dir = DownloadLayout.CreateDownloadLayoutTVShow(show, GlobalStaticVars.Settings,
                        DownloadLayout.PlexStandardLayout);
                    dlInfo.DownloadPath = dir.SeasonPath;
                    GlobalStaticVars.Queue.Add(dlInfo);
                }
            }
            else
            {
                LoggingHelpers.RecordGeneralEntry(@"Worker is to grab Single Episode metadata");

                SetProgressLabel(@"Getting Metadata 1/1");

                var show = GetTvObjectFromSelection(true);
                var dlInfo = show.StreamInformation;
                var dir = DownloadLayout.CreateDownloadLayoutTVShow(show, GlobalStaticVars.Settings,
                    DownloadLayout.PlexStandardLayout);
                dlInfo.DownloadPath = dir.SeasonPath;
                GlobalStaticVars.Queue.Add(dlInfo);
            }
        }

        private PlexObject ObjectFromSelection()
        {
            PlexObject p = null;

            try
            {
                if ((dgvMovies.SelectedRows.Count == 1) || (dgvEpisodes.SelectedRows.Count == 1) ||
                    (dgvTracks.SelectedRows.Count == 1))
                {
                    var t = GlobalStaticVars.CurrentContentType;
                    switch (t)
                    {
                        case ContentType.TvShows:
                            p = GetTvObjectFromSelection(true);
                            break;

                        case ContentType.Movies:
                            p = GetMovieObjectFromSelection(true);
                            break;

                        case ContentType.Music:
                            p = GetMusicObjectFromSelection(true);
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
            if (Flags.IsDownloadAll)
            {
                LoggingHelpers.RecordGeneralEntry(@"Worker is to grab metadata for All Tracks");

                var rowCount = GlobalTables.TracksTable.Rows.Count;

                for (var i = 0; i < rowCount; i++)
                {
                    SetProgressLabel(@"Getting Metadata " + (i + 1) + @"/" + rowCount);

                    var track = ObjectBuilders.GetMusicObjectFromIndex(i, true);
                    var dlInfo = track.StreamInformation;
                    var dir = DownloadLayout.CreateDownloadLayoutMusic(track, GlobalStaticVars.Settings,
                        DownloadLayout.PlexStandardLayout);
                    dlInfo.DownloadPath = dir.AlbumPath;
                    GlobalStaticVars.Queue.Add(dlInfo);
                }
            }
            else
            {
                LoggingHelpers.RecordGeneralEntry(@"Worker is to grab Single Track metadata");

                SetProgressLabel(@"Getting Metadata 1/1");

                var track = GetMusicObjectFromSelection(true);
                var dlInfo = track.StreamInformation;
                var dir = DownloadLayout.CreateDownloadLayoutMusic(track, GlobalStaticVars.Settings,
                    DownloadLayout.PlexStandardLayout);
                dlInfo.DownloadPath = dir.AlbumPath;
                GlobalStaticVars.Queue.Add(dlInfo);
            }
        }

        private void WkrGrabMovie()
        {
            SetProgressLabel(@"Getting Metadata 1/1");

            var movie = GetMovieObjectFromSelection(true);
            var dlInfo = movie.StreamInformation;
            dlInfo.DownloadPath = GlobalStaticVars.Settings.Generic.DownloadDirectory + @"\Movies";
            GlobalStaticVars.Queue.Add(dlInfo);
        }

        private void WkrGetMetadata_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //set needed globals
                GlobalStaticVars.Engine = new DownloadQueue();
                GlobalStaticVars.Queue = new List<DownloadInfo>();

                LoggingHelpers.RecordGeneralEntry(@"Metadata worker started");
                LoggingHelpers.RecordGeneralEntry(@"Doing directory checks");

                if (string.IsNullOrEmpty(GlobalStaticVars.Settings.Generic.DownloadDirectory) ||
                    string.IsNullOrWhiteSpace(GlobalStaticVars.Settings.Generic.DownloadDirectory)) ResetDownloadDirectory();

                LoggingHelpers.RecordGeneralEntry(@"Grabbing metadata");

                switch (GlobalStaticVars.CurrentContentType)
                {
                    case ContentType.TvShows:
                        LoggingHelpers.RecordGeneralEntry(@"Worker is to grab TV Show metadata");
                        WkrGrabTv();
                        break;

                    case ContentType.Movies:
                        LoggingHelpers.RecordGeneralEntry(@"Worker is to grab Movie metadata");
                        WkrGrabMovie();
                        break;

                    case ContentType.Music:
                        LoggingHelpers.RecordGeneralEntry(@"Worker is to grab Music metadata");
                        WkrGrabMusic();
                        break;
                }

                LoggingHelpers.RecordGeneralEntry("Worker is to invoke downloader thread");

                BeginInvoke((MethodInvoker)delegate
                {
                    StartDownload(GlobalStaticVars.Queue, GlobalStaticVars.Settings.Generic.DownloadDirectory);
                    LoggingHelpers.RecordGeneralEntry("Worker has started the download process");
                });
            }
            catch (Exception ex)
            {
                SetProgressLabel(@"Errored - Check Log");
                UIMessages.Error("Error occurred whilst getting needed metadata:\n\n" + ex);
                LoggingHelpers.RecordException(ex.Message, "MetadataWkrError");
            }
        }

        #endregion BackgroundWorkers

        #region UpdateCallbackWorkers

        private void WorkerUpdateContentView(object sender, WaitWindowEventArgs e)
        {
            var doc = (XmlDocument)e.Arguments[0];
            UpdateContentViewWorker(doc, (ContentType)e.Arguments[1]);
        }

        private void WorkerRenderMoviesView(object sender, WaitWindowEventArgs e)
        {
            var t = (DataTable)e.Arguments[0];
            RenderMoviesView(t);
        }

        private void WorkerRenderTVView(object sender, WaitWindowEventArgs e)
        {
            var t = (DataTable)e.Arguments[0];
            RenderTVView(t);
        }

        private void WorkerUpdateLibraryView(object sender, WaitWindowEventArgs e)
        {
            var doc = (XmlDocument)e.Arguments[0];
            PopulateLibraryWorker(doc);
        }

        private void WorkerUpdateSeriesView(object sender, WaitWindowEventArgs e)
        {
            var doc = (XmlDocument)e.Arguments[0];
            UpdateSeriesViewWorker(doc);
        }

        private void WorkerUpdateEpisodesView(object sender, WaitWindowEventArgs e)
        {
            var doc = (XmlDocument)e.Arguments[0];
            UpdateEpisodeViewWorker(doc);
        }

        private void WorkerUpdateTracksView(object sender, WaitWindowEventArgs e)
        {
            var doc = (XmlDocument)e.Arguments[0];
            UpdateTracksViewWorker(doc);
        }

        private void WorkerUpdateAlbumsView(object sender, WaitWindowEventArgs e)
        {
            var doc = (XmlDocument)e.Arguments[0];
            UpdateAlbumsViewWorker(doc);
        }

        #endregion UpdateCallbackWorkers

        #region ContentRenderers

        private void RenderMoviesView(DataTable content)
        {
            if (!(content == null))
            {
                ClearTVViews();
                ClearContentView();
                ClearMusicViews();

                var wantedColumns = GlobalStaticVars.Settings.DataDisplay.MoviesView.DisplayColumns;
                var wantedCaption = GlobalStaticVars.Settings.DataDisplay.MoviesView.DisplayCaptions;

                var info = new RenderStruct
                {
                    Data = content,
                    WantedColumns = wantedColumns,
                    WantedCaption = wantedCaption
                };

                GlobalViews.MoviesViewTable = GenericRenderer.RenderView(info, dgvMovies);

                SelectMoviesTab();
            }
        }

        private void SelectMoviesTab()
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    tabMain.SelectedTab = tabMovies;
                });
            }
            else
            {
                tabMain.SelectedTab = tabMovies;
            }
        }

        private void SelectTVTab()
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    tabMain.SelectedTab = tabTV;
                });
            }
            else
            {
                tabMain.SelectedTab = tabTV;
            }
        }

        private void SelectMusicTab()
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    tabMain.SelectedTab = tabMusic;
                });
            }
            else
            {
                tabMain.SelectedTab = tabMusic;
            }
        }

        private void ClearContentView()
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate
                {
                    dgvMovies.DataSource = null;
                });
            else
                dgvMovies.DataSource = null;
        }

        private void ClearTVViews()
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
                BeginInvoke((MethodInvoker)delegate
                {
                    dgvSections.DataSource = null;
                });
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

        private void RenderTVView(DataTable content)
        {
            if (content != null)
            {
                ClearTVViews();
                ClearContentView();
                ClearMusicViews();

                var wantedColumns = GlobalStaticVars.Settings.DataDisplay.TvView.DisplayColumns;
                var wantedCaption = GlobalStaticVars.Settings.DataDisplay.TvView.DisplayCaptions;

                var info = new RenderStruct
                {
                    Data = content,
                    WantedColumns = wantedColumns,
                    WantedCaption = wantedCaption
                };

                GlobalViews.TvViewTable = GenericRenderer.RenderView(info, dgvTVShows);

                SelectTVTab();
            }
        }

        private void RenderArtistsView(DataTable content)
        {
            if (content != null)
            {
                ClearTVViews();
                ClearContentView();
                ClearMusicViews();

                var wantedColumns = GlobalStaticVars.Settings.DataDisplay.ArtistsView.DisplayColumns;
                var wantedCaption = GlobalStaticVars.Settings.DataDisplay.ArtistsView.DisplayCaptions;

                var info = new RenderStruct
                {
                    Data = content,
                    WantedColumns = wantedColumns,
                    WantedCaption = wantedCaption
                };

                GlobalViews.ArtistViewTable = GenericRenderer.RenderView(info, dgvArtists);

                SelectMusicTab();
            }
        }

        private void RenderSeasonsView(DataTable content)
        {
            if (content != null)
            {
                var wantedColumns = GlobalStaticVars.Settings.DataDisplay.SeriesView.DisplayColumns;
                var wantedCaption = GlobalStaticVars.Settings.DataDisplay.SeriesView.DisplayCaptions;

                var info = new RenderStruct
                {
                    Data = content,
                    WantedColumns = wantedColumns,
                    WantedCaption = wantedCaption
                };

                GlobalViews.SeasonsViewTable = GenericRenderer.RenderView(info, dgvSeasons);
            }
        }

        private void RenderEpisodesView(DataTable content)
        {
            if (content != null)
            {
                var wantedColumns = GlobalStaticVars.Settings.DataDisplay.EpisodesView.DisplayColumns;
                var wantedCaption = GlobalStaticVars.Settings.DataDisplay.EpisodesView.DisplayCaptions;

                var info = new RenderStruct
                {
                    Data = content,
                    WantedColumns = wantedColumns,
                    WantedCaption = wantedCaption
                };

                GlobalViews.EpisodesViewTable = GenericRenderer.RenderView(info, dgvEpisodes);
            }
        }

        private void RenderAlbumsView(DataTable content)
        {
            if (content != null)
            {
                var wantedColumns = GlobalStaticVars.Settings.DataDisplay.AlbumsView.DisplayColumns;
                var wantedCaption = GlobalStaticVars.Settings.DataDisplay.AlbumsView.DisplayCaptions;

                var info = new RenderStruct
                {
                    Data = content,
                    WantedColumns = wantedColumns,
                    WantedCaption = wantedCaption
                };

                GlobalViews.AlbumViewTable = GenericRenderer.RenderView(info, dgvAlbums);
            }
        }

        private void RenderTracksView(DataTable content)
        {
            if (content != null)
            {
                var wantedColumns = GlobalStaticVars.Settings.DataDisplay.TracksView.DisplayColumns;
                var wantedCaption = GlobalStaticVars.Settings.DataDisplay.TracksView.DisplayCaptions;

                var info = new RenderStruct
                {
                    Data = content,
                    WantedColumns = wantedColumns,
                    WantedCaption = wantedCaption
                };

                GlobalViews.TracksViewTable = GenericRenderer.RenderView(info, dgvTracks);
            }
        }

        private void RenderLibraryView(DataTable content, bool renderKey = false)
        {
            if (content == null) return;

            var wantedColumns = GlobalStaticVars.Settings.DataDisplay.LibraryView.DisplayColumns;
            var wantedCaption = GlobalStaticVars.Settings.DataDisplay.LibraryView.DisplayCaptions;

            if (renderKey)
            {
                wantedColumns = wantedColumns.Prepend("key").ToList();
                wantedCaption = wantedCaption.Prepend("Key").ToList();
            }

            var info = new RenderStruct
            {
                Data = content,
                WantedColumns = wantedColumns,
                WantedCaption = wantedCaption
            };

            GenericRenderer.RenderView(info, dgvSections);
        }

        #endregion ContentRenderers

        #region UpdateWaitWorkers

        private void UpdateContentView(XmlDocument content, ContentType type)
        {
            WaitWindow.WaitWindow.Show(WorkerUpdateContentView, "Updating Content", content, type);
        }

        private void UpdateSeriesView(XmlDocument content)
        {
            WaitWindow.WaitWindow.Show(WorkerUpdateSeriesView, "Updating Series", content);
        }

        private void UpdateEpisodeView(XmlDocument content)
        {
            WaitWindow.WaitWindow.Show(WorkerUpdateEpisodesView, "Updating Episodes", content);
        }

        private void UpdateTracksView(XmlDocument content)
        {
            WaitWindow.WaitWindow.Show(WorkerUpdateTracksView, "Updating Tracks", content);
        }

        private void UpdateAlbumsView(XmlDocument content)
        {
            WaitWindow.WaitWindow.Show(WorkerUpdateAlbumsView, "Updating Albums", content);
        }

        private void PopulateLibrary(XmlDocument content)
        {
            WaitWindow.WaitWindow.Show(WorkerUpdateLibraryView, "Updating Library", content);
        }

        #endregion UpdateWaitWorkers

        #region PlexAPIWorkers

        private void GetMovieObjectFromSelectionWorker(object sender, WaitWindowEventArgs e)
        {
            bool formatLinkDownload = false;
            if (e.Arguments.Count > 0)
                formatLinkDownload = (bool)e.Arguments[0];
            e.Result = GetMovieObjectFromSelection(formatLinkDownload);
        }

        private void GetTVObjectFromSelectionWorker(object sender, WaitWindowEventArgs e)
        {
            bool formatLinkDownload = false;
            if (e.Arguments.Count > 0)
                formatLinkDownload = (bool)e.Arguments[0];
            e.Result = GetTvObjectFromSelection(formatLinkDownload);
        }

        private void GetMusicObjectFromSelectionWorker(object sender, WaitWindowEventArgs e)
        {
            bool formatLinkDownload = false;
            if (e.Arguments.Count > 0)
                formatLinkDownload = (bool)e.Arguments[0];
            e.Result = GetMusicObjectFromSelection(formatLinkDownload);
        }

        #endregion PlexAPIWorkers

        #endregion Workers

        #region Download

        #region DownloadMethods

        private void CancelDownload(bool silent = false, string msg = "Download Cancelled")
        {
            //try and kill the worker if it's still trying to do something
            if (wkrGetMetadata.IsBusy) wkrGetMetadata.Abort();

            //check if the Engine's still running; if it is, we can then cancel and clear the download queue.
            if (Flags.IsEngineRunning)
            {
                GlobalStaticVars.Engine.Cancel();
                GlobalStaticVars.Engine.Clear();
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
                Flags.IsDownloadQueueCancelled = true;

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
            GlobalStaticVars.Engine.QueueProgressChanged += Engine_DownloadProgressChanged;
            GlobalStaticVars.Engine.QueueCompleted += Engine_DownloadCompleted;

            GlobalStaticVars.Engine.StartAsync();
            //UIMessages.Info("Started!");
            LoggingHelpers.RecordGeneralEntry("Download is Progressing");
            Flags.IsDownloadRunning = true;
            Flags.IsEngineRunning = true;
            Flags.IsDownloadPaused = false;
            SetPause();
        }

        private void SetDownloadDirectory()
        {
            if (fbdSave.ShowDialog() != DialogResult.OK) return;

            GlobalStaticVars.Settings.Generic.DownloadDirectory = fbdSave.SelectedPath;
            UIMessages.Info($"Download directory updated to {GlobalStaticVars.Settings.Generic.DownloadDirectory}");
            LoggingHelpers.RecordGeneralEntry("Download directory updated to " + GlobalStaticVars.Settings.Generic.DownloadDirectory);
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
            GlobalStaticVars.Engine.Clear();

            //set the global project flags
            Flags.IsDownloadRunning = false;
            Flags.IsDownloadPaused = false;
            Flags.IsEngineRunning = false;
        }

        private void StartDownload(IReadOnlyList<DownloadInfo> queue, string location)
        {
            LoggingHelpers.RecordGeneralEntry("Download Process Started");
            pbMain.Value = 0;

            LoggingHelpers.RecordGeneralEntry("Starting HTTP Engine");
            GlobalStaticVars.Engine = new DownloadQueue();
            if (queue.Count > 1)
            {
                foreach (var dl in queue)
                {
                    var fqPath = dl.DownloadPath + @"\" + dl.FileName;
                    if (File.Exists(fqPath))
                        LoggingHelpers.RecordGeneralEntry(dl.FileName + " already exists; will not download.");
                    else
                        GlobalStaticVars.Engine.Add(dl.Link, fqPath);
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

                GlobalStaticVars.Engine.Add(dl.Link, fqPath);
            }

            btnPause.Enabled = true;
            StartDownloadEngine();
        }

        #endregion DownloadMethods

        #region DownloadEngineMethods

        private void ShowBalloon(string msg, string title, bool error = false, int timeout = 2000)
        {
            if (!InvokeRequired)
            {
                if (error)
                    nfyMain.BalloonTipIcon = ToolTipIcon.Error;
                else
                    nfyMain.BalloonTipIcon = ToolTipIcon.Info;

                nfyMain.BalloonTipText = msg;
                nfyMain.BalloonTipTitle = title;
                nfyMain.ShowBalloonTip(timeout);
            }
            else
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    if (error)
                        nfyMain.BalloonTipIcon = ToolTipIcon.Error;
                    else
                        nfyMain.BalloonTipIcon = ToolTipIcon.Info;

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
                var engineProgress = GlobalStaticVars.Engine.CurrentProgress;
                var bytesGet = GlobalStaticVars.Engine.BytesReceived;
                var engineSpeed = GlobalStaticVars.Engine.CurrentDownloadSpeed;
                var contentSize = GlobalStaticVars.Engine.CurrentContentLength;

                //proper formatting of engine data for display
                var progress = Math.Round(engineProgress);
                var speed = Methods.FormatBytes(engineSpeed) + "/s";
                var total = Methods.FormatBytes((long)contentSize);
                var order = (GlobalStaticVars.Engine.CurrentIndex + 1) + "/" + GlobalStaticVars.Engine.QueueLength;
                var eta = @"~";

                //it'd be really bad if we tried to divide by 0 and 0
                if (bytesGet > 0 && contentSize > 0)
                {
                    //subtract the byte count we already have from the total we need
                    var diff = contentSize - bytesGet;

                    //~needs to be in milisecond format; so * seconds by 1000~
                    var val = (diff / engineSpeed) * 1000;

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

        #endregion DownloadEngineMethods

        #endregion Download

        #region Search

        private void ClearSearch(bool renderTables = true)
        {
            if (Flags.IsFiltered)
            {
                if (renderTables)
                {
                    switch (GlobalStaticVars.CurrentContentType)
                    {
                        case ContentType.TvShows:
                            RenderTVView(GlobalTables.TitlesTable);
                            break;

                        case ContentType.Movies:
                            RenderMoviesView(GlobalTables.TitlesTable);
                            break;

                        case ContentType.Music:
                            RenderArtistsView(GlobalTables.TitlesTable);
                            break;
                    }
                }

                GlobalTables.FilteredTable = null;
                Flags.IsFiltered = false;
                SetStartSearch();
            }
        }

        private void RunTitleSearch()
        {
            try
            {
                LoggingHelpers.RecordGeneralEntry("Title search requested");
                if ((dgvMovies.Rows.Count > 0) || (dgvTVShows.Rows.Count > 0) || (dgvArtists.Rows.Count > 0))
                {
                    RenderStruct info = null;
                    DataGridView dgv = null;

                    switch (GlobalStaticVars.CurrentContentType)
                    {
                        case ContentType.TvShows:
                            dgv = dgvTVShows;
                            info = new RenderStruct
                            {
                                Data = GlobalTables.TitlesTable,
                                WantedCaption = GlobalStaticVars.Settings.DataDisplay.TvView.DisplayCaptions,
                                WantedColumns = GlobalStaticVars.Settings.DataDisplay.TvView.DisplayColumns
                            };
                            break;

                        case ContentType.Movies:
                            dgv = dgvMovies;
                            info = new RenderStruct
                            {
                                Data = GlobalTables.TitlesTable,
                                WantedCaption = GlobalStaticVars.Settings.DataDisplay.MoviesView.DisplayCaptions,
                                WantedColumns = GlobalStaticVars.Settings.DataDisplay.MoviesView.DisplayColumns
                            };
                            break;

                        case ContentType.Music:
                            dgv = dgvArtists;
                            info = new RenderStruct
                            {
                                Data = GlobalTables.TitlesTable,
                                WantedCaption = GlobalStaticVars.Settings.DataDisplay.ArtistsView.DisplayCaptions,
                                WantedColumns = GlobalStaticVars.Settings.DataDisplay.ArtistsView.DisplayColumns
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
                        GlobalViews.FilteredViewTable = null;
                        GlobalTables.FilteredTable = null;
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

        #endregion Search

        #region UIMethods

        /// <summary>
        ///     Thread-safe way of changing the progress label
        /// </summary>
        /// <param name="status">
        /// </param>
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
            if (GlobalTables.TitlesTable != null)
                lblViewingValue.Text = $"{GlobalTables.TitlesTable.Rows.Count}/{GlobalTables.TitlesTable.Rows.Count}";
        }

        private void SetCancelSearch()
        {
            if (GlobalTables.FilteredTable != null && GlobalTables.TitlesTable != null)
                lblViewingValue.Text = $"{GlobalTables.FilteredTable.Rows.Count}/{GlobalTables.TitlesTable.Rows.Count}";
            itmStartSearch.Text = @"Clear Search";
        }

        private void SetConnect()
        {
            itmDisconnect.Enabled = false;
            lblViewingValue.Text = @"0/0";
        }

        private void SetDisconnect()
        {
            itmDisconnect.Enabled = true;
        }

        #endregion UIMethods

        #region FormEvents

        private void LoadDevStatus()
        {
            var choc = Color.Chocolate;
            var red = Color.DarkRed;
            var green = Color.DarkGreen;
            switch (BuildState.State)
            {
                case DevStatus.InDevelopment:
                    lblBeta.ForeColor = choc;
                    lblBeta.Text = @"Developer Build";
                    break;

                case DevStatus.InBeta:
                    lblBeta.ForeColor = red;
                    lblBeta.Text = @"Beta Testing Build";
                    break;

                case DevStatus.ProductionReady:
                    lblBeta.ForeColor = green;
                    lblBeta.Text = @"Production Build";
                    break;
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Flags.IsDownloadRunning)
            {
                if (Flags.IsMsgAlreadyShown) return;

                if (UIMessages.Question(@"Are you sure you want to exit PlexDL? A download is still running."))
                {
                    Flags.IsMsgAlreadyShown = true;
                    LoggingHelpers.RecordGeneralEntry("PlexDL Exited");
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                LoggingHelpers.RecordGeneralEntry("PlexDL Exited");
            }
        }

        private void ResetDownloadDirectory()
        {
            var curUser = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            GlobalStaticVars.Settings.Generic.DownloadDirectory = curUser + @"\PlexDL";
            if (!Directory.Exists(GlobalStaticVars.Settings.Generic.DownloadDirectory))
                Directory.CreateDirectory(GlobalStaticVars.Settings.Generic.DownloadDirectory);
        }

        private void SetDebugLocation()
        {
            if (GlobalStaticVars.DebugForm != null && Flags.IsDebug)
            {
                Point thisp = Location;
                int x = thisp.X + Width;
                int y = thisp.Y;
                GlobalStaticVars.DebugForm.Location = new Point(x, y);
            }
        }

        private void SetSessionId()
        {
            lblSidValue.Text = GlobalStaticVars.CurrentSessionId;
        }

        private void Home_Move(object sender, EventArgs e)
        {
            SetDebugLocation();
        }

        private void Home_Focus(object sender, EventArgs e)
        {
        }

        private void Home_Load(object sender, EventArgs e)
        {
            try
            {
                if (Flags.IsAutoUpdateEnabled)
                {
                    UpdateManager.RunUpdateCheck(true);
                }

                if (Flags.IsDebug)
                {
                    GlobalStaticVars.DebugForm = new Debug();
                    SetDebugLocation();
                    GlobalStaticVars.DebugForm.Show();
                }

                //UIMessages.Info(GlobalStaticVars.PlexDlAppData);
                //CachingFileDir.RootCacheDirectory = $"{GlobalStaticVars.PlexDlAppData}\\caching";

                SetSessionId();
                LoadDevStatus();
                ResetDownloadDirectory();
                LoggingHelpers.RecordGeneralEntry("PlexDL Started");
                LoggingHelpers.RecordGeneralEntry($"Data location: {GlobalStaticVars.PlexDlAppData}");
                LoggingHelpers.RecordCacheEvent($"Using cache directory: {CachingFileDir.RootCacheDirectory}", "N/A");
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "StartupError");
                UIMessages.Error("Startup Error:\n\n" + ex, "Startup Error");
            }
        }

        private void UpdateFromLibraryKey(string key, ContentType type)
        {
            object[] args =
            {
                key, type
            };
            WaitWindow.WaitWindow.Show(UpdateFromLibraryKey_Worker, @"Getting Metadata", args);
        }

        private void UpdateFromLibraryKey_Worker(object sender, WaitWindowEventArgs e)
        {
            var type = (ContentType)e.Arguments[1];
            var key = (string)e.Arguments[0];
            try
            {
                if (!Flags.IsInitialFill) Flags.IsInitialFill = true;

                LoggingHelpers.RecordGeneralEntry(@"Requesting library contents");
                var contentUri = GlobalStaticVars.CurrentApiUri + key + @"/all/?X-Plex-Token=";
                var contentXml = XmlGet.GetXmlTransaction(contentUri);

                UpdateContentView(contentXml, type);
            }
            catch (WebException ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"UpdateLibraryError");
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    if (ex.Response is HttpWebResponse response)
                        switch ((int)response.StatusCode)
                        {
                            case 401:
                                UIMessages.Error(
                                    @"The web server denied access to the resource. Check your token and try again. (401)");
                                break;

                            case 404:
                                UIMessages.Error(
                                    @"The web server couldn't serve the request because it couldn't find the resource specified. (404)");
                                break;

                            case 400:
                                UIMessages.Error(
                                    @"The web server couldn't serve the request because the request was bad. (400)");
                                break;
                        }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "UpdateLibraryError");
                UIMessages.Error(ex.ToString());
            }
        }

        private void CxtLibrarySections_Opening(object sender, CancelEventArgs e)
        {
            if (dgvSections.Rows.Count == 0) e.Cancel = true;
        }

        #endregion FormEvents

        #region DGVRowChanges

        private void DgvLibrary_OnRowChange(object sender, EventArgs e)
        {
            LibrarySectionChange();
        }

        private void LibrarySectionChange()
        {
            if (dgvSections.SelectedRows.Count != 1 || !Flags.IsLibraryFilled) return;

            LoggingHelpers.RecordGeneralEntry("Selection Changed");
            //don't re-render the grids when clearing the search; this would end badly for performance reasons.
            ClearSearch(false);
            LoggingHelpers.RecordGeneralEntry("Cleared possible searches");
            var index = GlobalTables.GetTableIndexFromDgv(dgvSections, GlobalTables.SectionsTable);
            var r = RowGet.GetDataRowLibrary(index);

            var key = "";
            var type = "";
            if (r != null)
            {
                if (r["key"] != null)
                    key = r["key"].ToString();
                if (r["type"] != null)
                    type = r["type"].ToString();
            }

            switch (type)
            {
                case "show":
                    UpdateFromLibraryKey(key, ContentType.TvShows);
                    break;

                case "movie":
                    UpdateFromLibraryKey(key, ContentType.Movies);
                    break;

                case "artist":
                    UpdateFromLibraryKey(key, ContentType.Music);
                    break;
            }
        }

        private void DgvSeasons_OnRowChange(object sender, EventArgs e)
        {
            if (dgvSeasons.SelectedRows.Count != 1) return;

            var index = GlobalTables.GetTableIndexFromDgv(dgvSeasons, GlobalTables.SeasonsTable);
            var episodes = XmlMetadataGatherers.GetEpisodeXml(index);
            UpdateEpisodeView(episodes);
        }

        private void DgvAlbums_OnRowChange(object sender, EventArgs e)
        {
            if (dgvAlbums.SelectedRows.Count != 1) return;

            var index = GlobalTables.GetTableIndexFromDgv(dgvAlbums, GlobalTables.AlbumsTable);
            var tracks = XmlMetadataGatherers.GetTracksXml(index);
            UpdateTracksView(tracks);
        }

        private void dgvMovies_OnRowChange(object sender, EventArgs e)
        {
            //nothing, more or less.
        }

        private void DoubleClickLaunch(bool formatLinkDownload = false)
        {
            PlexObject stream = null;

            switch (GlobalStaticVars.CurrentContentType)
            {
                case ContentType.Movies:
                    if (dgvMovies.SelectedRows.Count == 1)
                    {
                        var obj = GetMovieObjectFromSelection(formatLinkDownload);
                        if (obj != null)
                        {
                            stream = obj;
                        }
                        else
                            LoggingHelpers.RecordGeneralEntry("Double-click stream failed; null object.");
                    }
                    break;

                case ContentType.TvShows:
                    if (dgvEpisodes.SelectedRows.Count == 1 && dgvTVShows.SelectedRows.Count == 1)
                    {
                        var obj = GetTvObjectFromSelection(formatLinkDownload);
                        if (obj != null)
                        {
                            stream = obj;
                        }
                        else
                            LoggingHelpers.RecordGeneralEntry("Double-click stream failed; null object.");
                    }
                    break;

                case ContentType.Music:
                    if (dgvTracks.SelectedRows.Count == 1 && dgvArtists.SelectedRows.Count == 1)
                    {
                        var obj = GetMusicObjectFromSelection(formatLinkDownload);
                        if (obj != null)
                        {
                            stream = obj;
                        }
                        else
                            LoggingHelpers.RecordGeneralEntry("Double-click stream failed; null object.");
                    }
                    break;
            }

            if (stream != null)
            {
                if (GlobalStaticVars.Settings.Player.PlaybackEngine == PlaybackMode.MenuSelector)
                {
                    if (VlcLauncher.VlcInstalled())
                    {
                        StartStreaming(stream, PlaybackMode.VLCPlayer);
                    }
                    else
                    {
                        StartStreaming(stream, PlaybackMode.PVSPlayer);
                    }
                }
                else
                {
                    StartStreaming(stream);
                }
            }
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
                    if (GlobalStaticVars.Settings.Generic.DoubleClickLaunch && gridView.IsContentTable)
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
                    LoggingHelpers.RecordGeneralEntry("Double-click launch failed; incorrect object type. Expecting object of type FlatDataGridView.");
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "DoubleClickError");
                LoggingHelpers.RecordGeneralEntry("Double-click launch failed; an error occurred. Check exception log for more information.");
            }
        }

        //debugging stuff
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

        private void DgvTVShows_OnRowChange(object sender, EventArgs e)
        {
            if (dgvTVShows.SelectedRows.Count != 1) return;

            var index = GlobalTables.GetTableIndexFromDgv(dgvTVShows, GlobalTables.ReturnCorrectTable());

            //debugging
            //UIMessages.Info(index.ToString());

            if (GlobalStaticVars.CurrentContentType != ContentType.TvShows) return;

            var series = XmlMetadataGatherers.GetSeriesXml(index);
            //debugging
            //XmlMessageBox(series);
            UpdateSeriesView(series);
        }

        private void DgvArtists_OnRowChange(object sender, EventArgs e)
        {
            if (dgvArtists.SelectedRows.Count != 1) return;

            var index = GlobalTables.GetTableIndexFromDgv(dgvArtists, GlobalTables.ReturnCorrectTable());

            //debugging
            //UIMessages.Info(index.ToString());

            if (GlobalStaticVars.CurrentContentType != ContentType.Music) return;

            var albums = XmlMetadataGatherers.GetAlbumsXml(index);
            //debugging
            //XmlMessageBox(series);
            UpdateAlbumsView(albums);
        }

        #endregion DGVRowChanges

        #region ButtonClicks

        private void DoDownloadAll()
        {
            LoggingHelpers.RecordGeneralEntry("Awaiting download safety checks");
            if (!Flags.IsDownloadRunning && !Flags.IsEngineRunning)
            {
                LoggingHelpers.RecordGeneralEntry("Download process is starting");
                SetProgressLabel("Waiting");
                Flags.IsDownloadAll = true;
                DownloadTotal = GlobalTables.ReturnCorrectTable(true).Rows.Count;
                Flags.IsDownloadRunning = true;
                if (wkrGetMetadata.IsBusy) wkrGetMetadata.Abort();
                wkrGetMetadata.RunWorkerAsync();
                tmrWorkerTimeout.Start();
                LoggingHelpers.RecordGeneralEntry("Worker invoke process started");
                SetDownloadCancel();
            }
            else
                LoggingHelpers.RecordGeneralEntry("Download process failed; download is already running.");
        }

        private void DoDownloadSelected()
        {
            LoggingHelpers.RecordGeneralEntry("Awaiting download safety checks");
            //UIMessages.Info(GlobalStaticVars.CurrentContentType.ToString());
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
                LoggingHelpers.RecordGeneralEntry("Download process failed; download is already running.");
        }

        private void StartStreaming(PlexObject stream)
        {
            var def = GlobalStaticVars.Settings.Player.PlaybackEngine;
            StartStreaming(stream, def);
        }

        private void StartStreaming(PlexObject stream, int playbackEngine)
        {
            //so that cxtStreamOptions can access the current stream's information, a global object has to be used.
            GlobalStaticVars.CurrentStream = stream;
            switch (playbackEngine)
            {
                case -1:
                    return;

                case PlaybackMode.PVSPlayer:
                    PvsLauncher.LaunchPvs(stream, GlobalTables.ReturnCorrectTable());
                    break;

                case PlaybackMode.VLCPlayer:
                    VlcLauncher.LaunchVlc(stream);
                    break;

                case PlaybackMode.Browser:
                    BrowserLauncher.LaunchBrowser(stream);
                    break;

                case PlaybackMode.MenuSelector:
                    {
                        //display the options menu where the
                        var loc = new Point(Location.X + btnHTTPPlay.Location.X, Location.Y + btnHTTPPlay.Location.Y);
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
            if (dgvEpisodes.SelectedRows.Count == 0) e.Cancel = true;
        }

        private void CxtContentOptions_Opening(object sender, CancelEventArgs e)
        {
            if (dgvMovies.SelectedRows.Count == 0) e.Cancel = true;
        }

        private void Metadata(PlexObject result = null)
        {
            if ((dgvMovies.SelectedRows.Count == 1) || (dgvEpisodes.SelectedRows.Count == 1) || (dgvTracks.SelectedRows.Count == 1))
            {
                if (!Flags.IsDownloadRunning && !Flags.IsEngineRunning)
                {
                    if (result == null)
                    {
                        switch (GlobalStaticVars.CurrentContentType)
                        {
                            case ContentType.Movies:
                                result = (PlexObject)WaitWindow.WaitWindow.Show(GetMovieObjectFromSelectionWorker,
                                "Getting Metadata", false);
                                break;

                            case ContentType.TvShows:
                                result = (PlexObject)WaitWindow.WaitWindow.Show(GetTVObjectFromSelectionWorker,
                                "Getting Metadata", false);
                                break;

                            case ContentType.Music:
                                result = (PlexMusic)WaitWindow.WaitWindow.Show(GetMusicObjectFromSelectionWorker,
                                "Getting Metadata", false);
                                break;
                        }
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

        private void ShowLogViewer()
        {
            using (var frm = new LogViewer())
            {
                frm.ShowDialog();
            }
        }

        #endregion ButtonClicks

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

        private void ItmTrackMetadata_Click(object sender, EventArgs e)
        {
            cxtTrackOptions.Close();
            Metadata();
        }

        private void ItmCleanupAllData_Click(object sender, EventArgs e)
        {
            DoCleanup();
        }

        private void DoCleanup()
        {
            //check if the AppData .plexdl folder actually exists
            if (Directory.Exists(GlobalStaticVars.PlexDlAppData))
            {
                var ask = UIMessages.Question(
                    @"Are you sure you want to do this? This will remove all logging, caching and secure data.",
                    @"Cleanup Confirmation");

                //if they clicked anything other than 'Yes' on the message above, then exit the method.
                if (!ask) return;

                try
                {
                    //Try and delete the .plexdl AppData folder and all its subfolders and files
                    Directory.Delete(GlobalStaticVars.PlexDlAppData, true);

                    //alert user of the success
                    UIMessages.Info(
                        "Successfully removed all PlexDL files in the AppData folder. This means all logging, caching and secure data has been deleted also.\n\nNote: This event has not been logged.",
                        @"Cleanup Completed");
                }
                catch (Exception ex)
                {
                    UIMessages.Error("Cleanup error occurred:\n\n" + ex + "\n\nNote: This exception has not been logged",
                        @"Cleanup Failed");
                }
            }
            else
                UIMessages.Error(@"There's no data to cleanup; your .plexdl AppData folder does not exist.",
                    @"Cleanup Failed");
        }

        private void ItmRepo_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(GlobalStaticVars.RepoUrl);
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
            RenderLibraryView(GlobalTables.SectionsTable, itmRenderKeyColumn.Checked);
        }

        private static void ShowLinkViewer(PlexObject media)
        {
            var viewer = new LinkViewer { Link = media.StreamInformation.Link };
            viewer.ShowDialog();
        }

        private void ShowLinkViewer_Worker(object sender, WaitWindowEventArgs e)
        {
            e.Result = ObjectFromSelection();
        }

        private void ShowLinkViewer()
        {
            var o = (PlexObject)WaitWindow.WaitWindow.Show(ShowLinkViewer_Worker, @"Getting link");
            if (o != null)
            {
                ShowLinkViewer(o);
            }
            else
            {
                UIMessages.Error(@"Content object was null; couldn't construct a link for you.");
            }
        }

        private void ItmDGVViewTrackDownloadLink_Click(object sender, EventArgs e)
        {
            ShowLinkViewer();
        }

        private void ItmDGVViewMovieDownloadLink_Click(object sender, EventArgs e)
        {
            ShowLinkViewer();
        }

        private void ItmDGVViewEpisodeDownloadLink_Click(object sender, EventArgs e)
        {
            ShowLinkViewer();
        }

        private void ItmClearMyToken_Click(object sender, EventArgs e)
        {
            TokenManager.TokenClearProcedure();
        }

        private void ItmOpenDataFolder_Click(object sender, EventArgs e)
        {
            Process.Start(GlobalStaticVars.PlexDlAppData);
        }
    }
}