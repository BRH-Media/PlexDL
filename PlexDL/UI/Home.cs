using libbrhscgui.Components;
using MetroSet_UI.Extensions;
using MetroSet_UI.Forms;
using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Caching;
using PlexDL.Common.Logging;
using PlexDL.Common.PlayerLaunchers;
using PlexDL.Common.Renderers;
using PlexDL.Common.Renderers.DGVRenderers;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.AppOptions;
using PlexDL.Common.Structures.AppOptions.Player;
using PlexDL.Common.Structures.Plex;
using PlexDL.PlexAPI;
using PlexDL.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

//using System.Threading.Tasks;

namespace PlexDL.UI
{
    public partial class Home : MetroSetForm
    {
        #region GlobalVariables

        #region GlobalComponentVariables
        public readonly UserInteraction objUI = new UserInteraction();
        public Timer t1 = new Timer();
        public User user = new User();
        public Server svr;
        public PlexObject CurrentStream = null;

        #region Fonts

        public static Font ROBOTO_MEDIUM_12;
        public static Font ROBOTO_REGULAR_11;
        public static Font ROBOTO_MEDIUM_11;
        public static Font ROBOTO_MEDIUM_10;

        #endregion Fonts

        #endregion GlobalComponentVariables

        #region GlobalStringVariables

        public string uri = "";
        public string FilterQuery = "";

        #endregion GlobalStringVariables

        #region GlobalBoolVariables

        public static bool IsConnected = false;
        public static bool InitialFill = true;
        public static bool IsLibraryFilled = false;
        public static bool IsFiltered = false;
        public static bool IsTVShow = false;
        public static bool IsContentSortingEnabled = true;
        public static bool IsDownloadQueueCancelled = false;
        public static bool IsDownloadRunning = false;
        public static bool IsDownloadPaused = false;
        public static bool IsEngineRunning = false;
        public static bool IsMsgAlreadyShown = false;
        public static bool doNotAttemptAgain = false;
        public static bool DownloadAllEpisodes = false;

        #endregion GlobalBoolVariables

        #region GlobalIntVariables

        public int selectedIndex = 0;
        public int DownloadIndex = 0;
        public int DownloadTotal = 0;
        public int logIncrementer = 0;
        public int downloadsSoFar = 0;
        public int alreadyMarkedProgressNumber = 0;

        #endregion GlobalIntVariables

        #region GlobalStaticVariables

        public static AltoHttp.DownloadQueue engine;
        public static List<DownloadInfo> queue;
        public static ApplicationOptions settings = new ApplicationOptions();
        public static MyPlex plex = new MyPlex();

        #endregion GlobalStaticVariables

        #region GlobalXmlDocumentVariables

        public XmlDocument contentXmlDoc;
        public XmlDocument libraryXmlDoc;

        #endregion GlobalXmlDocumentVariables

        #region GlobalDataTableVariables

        public DataTable titlesTable = null;
        public DataTable filteredTable = null;
        public DataTable seriesTable = null;
        public DataTable episodesTable = null;
        public DataTable gSectionsTable = null;
        public DataTable contentViewTable = null;
        public DataTable tvViewTable = null;

        #endregion GlobalDataTableVariables

        #region GlobalListVariables

        public static List<Server> plexServers = null;

        #endregion GlobalListVariables

        #endregion GlobalVariables

        #region Form

        #region FormInitialiser

        public Home()
        {
            InitializeComponent();
            this.styleMain = GlobalStaticVars.GlobalStyle;
            this.styleMain.MetroForm = this;
        }

        #endregion FormInitialiser

        #region FormAnimationMethods

        private void FadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     //check if opacity is 0
            {
                t1.Stop();    //if it is, we stop the timer
                Close();   //and we try to close the form
            }
            else
                Opacity -= 0.05;
        }

        private void FadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop();   //this stops the timer if the form is completely displayed
            else
                Opacity += 0.05;
        }

        #endregion FormAnimationMethods

        #endregion Form

        #region XML/Metadata

        #region PlexMovieBuilders

        public int GetTableIndexFromDGV(DataGridView dgv, DataTable table = null, string key = "title")
        {
            DataGridViewRow sel = dgv.SelectedRows[0];
            if (table == null)
                table = ReturnCorrectTable();
            foreach (DataRow r in table.Rows)
            {
                if (dgv.Columns.Contains(key))
                {
                    string titleValue = sel.Cells[key].Value.ToString();
                    if (r[key].ToString() == titleValue)
                    {
                        return table.Rows.IndexOf(r);
                    }
                }
            }
            return 0;
        }

        private PlexMovie GetObjectFromSelection()
        {
            PlexMovie obj = new PlexMovie();
            if ((dgvContent.SelectedRows.Count == 1) || (dgvEpisodes.SelectedRows.Count == 1))
            {
                int index = GetTableIndexFromDGV(dgvContent);
                obj = GetObjectFromIndex(index);
            }
            return obj;
        }

        private PlexTVShow GetTVObjectFromSelection()
        {
            PlexTVShow obj = new PlexTVShow();
            if ((dgvTVShows.SelectedRows.Count == 1) && (dgvEpisodes.SelectedRows.Count == 1))
            {
                int index = GetTableIndexFromDGV(dgvEpisodes, episodesTable);
                obj = GetTVObjectFromIndex(index);
            }
            return obj;
        }

        private PlexTVShow GetTVObjectFromIndex(int index)
        {
            PlexTVShow obj = new PlexTVShow();
            XmlDocument metadata;
            if (dgvTVShows.SelectedRows.Count == 1)
            {
                AddToLog("Content Parse Started");
                AddToLog("Grabbing Titles");

                metadata = GetEpisodeMetadata(index);

                AddToLog("Checking XML validity");
                if (Methods.PlexXmlValid(metadata))
                {

                    AddToLog("XML Valid");

                    DownloadInfo dlInfo = GetContentDownloadInfo_Xml(metadata);

                    AddToLog("Assembling Object");

                    obj.ContentGenre = GetContentGenre(metadata);
                    obj.StreamInformation = dlInfo;
                    obj.Season = GetTVShowSeason(metadata);
                    obj.EpisodesInSeason = episodesTable.Rows.Count;
                    obj.TVShowName = GetTVShowTitle(metadata);
                    obj.StreamResolution = GetContentResolution(metadata);
                    obj.StreamIndex = index;
                }
                else
                    AddToLog("XML Invalid");
            }
            return obj;
        }

        public int GetIndexFromPrimary(string primary)
        {
            DataTable table = ReturnCorrectTable();
            DataRow row = table.Rows.Find(primary);
            return table.Rows.IndexOf(row);
        }

        private PlexMovie GetObjectFromIndex(int index)
        {
            PlexMovie obj = new PlexMovie();
            XmlDocument metadata;
            if (dgvContent.SelectedRows.Count == 1)
            {
                AddToLog("Content Parse Started");
                AddToLog("Grabbing Titles");
                metadata = GetContentMetadata(index);

                AddToLog("Checking XML validity");
                if (Methods.PlexXmlValid(metadata))
                {
                    _ = GetDataRowContent(index, false);

                    AddToLog("XML Valid");

                    DownloadInfo dlInfo = GetContentDownloadInfo_Xml(metadata);

                    AddToLog("Assembling Object");

                    obj.Actors = GetActorsFromMetadata(metadata);
                    obj.ContentGenre = GetContentGenre(metadata);
                    obj.StreamInformation = dlInfo;
                    obj.StreamResolution = GetContentResolution(metadata);
                    obj.StreamIndex = index;
                }
                else
                    AddToLog("XML Invalid");
            }
            return obj;
        }

        #endregion PlexMovieBuilders

        #region MetadataGatherers

        public XmlDocument GetTVShowMetadata(int index)
        {
            AddToLog("Sorting existing data");

            DataRow result;

            result = GetDataRowSeries(index);

            string key = result["key"].ToString();
            string baseUri = GetBaseUri(false);
            key = key.TrimStart('/');
            string uri = baseUri + key + "/?X-Plex-Token=";

            AddToLog("Contacting server");
            XmlDocument reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        public DataRow GetDataRowContent(int index, bool directTable)
        {
            if (!directTable)
                return GetDataRowTbl(ReturnCorrectTable(), index);
            else
            {
                if (IsFiltered)
                {
                    return GetDataRowTbl(filteredTable, index);
                }
                else
                {
                    return GetDataRowTbl(titlesTable, index);
                }
            }
        }

        public DataRow GetDataRowSeries(int index)
        {
            return GetDataRowTbl(seriesTable, index);
        }

        public DataRow GetDataRowEpisodes(int index)
        {
            return GetDataRowTbl(episodesTable, index);
        }

        public DataRow GetDataRowLibrary(int index)
        {
            return GetDataRowTbl(gSectionsTable, index);
        }

        public XmlDocument GetContentMetadata(int index)
        {
            AddToLog("Sorting existing data");

            DataRow result;

            result = GetDataRowContent(index, false);

            string key = result["key"].ToString();
            string baseUri = GetBaseUri(false);
            key = key.TrimStart('/');
            string uri = baseUri + key + "/?X-Plex-Token=";

            AddToLog("Contacting server");
            XmlDocument reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        public XmlDocument GetEpisodeMetadata(int index)
        {
            AddToLog("Sorting existing data");

            DataRow result;

            result = GetDataRowEpisodes(index);

            string key = result["key"].ToString();
            string baseUri = GetBaseUri(false);
            key = key.TrimStart('/');
            string uri = baseUri + key + "/?X-Plex-Token=";

            AddToLog("Contacting server");
            XmlDocument reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        #endregion MetadataGatherers

        #region XMLGatherers

        public XmlDocument GetSeriesXml(int index)
        {
            AddToLog("Sorting existing data");

            DataRow result;

            result = GetDataRowContent(index, true);

            string key = result["key"].ToString();
            string baseUri = GetBaseUri(false);
            key = key.TrimStart('/');
            string uri = baseUri + key + "/?X-Plex-Token=";

            //MessageBox.Show(uri);

            AddToLog("Contacting server");
            XmlDocument reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        public XmlDocument GetEpisodeXml(int index)
        {
            AddToLog("Sorting existing data");

            DataRow result;
            result = GetDataRowSeries(index);

            string key = result["key"].ToString();
            string baseUri = GetBaseUri(false);
            key = key.TrimStart('/');
            string uri = baseUri + key + "/?X-Plex-Token=";

            //MessageBox.Show(uri);

            AddToLog("Contacting server");
            XmlDocument reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        #endregion XMLGatherers

        #region MetadataParsers

        private List<PlexActor> GetActorsFromMetadata(XmlDocument metadata)
        {
            List<PlexActor> actors = new List<PlexActor>();

            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            DataTable dtActors = sections.Tables["Role"];

            if (!(dtActors == null))
            {
                foreach (DataRow r in dtActors.Rows)
                {
                    PlexActor a = new PlexActor();
                    string thumb = "";
                    string role = "Unknown";
                    string name = "Unknown";
                    if (dtActors.Columns.Contains("thumb"))
                        if (r["thumb"] != null)
                            thumb = r["thumb"].ToString();
                    if (dtActors.Columns.Contains("role"))
                        if (r["role"] != null)
                            role = r["role"].ToString();
                    if (dtActors.Columns.Contains("tag"))
                        if (r["tag"] != null)
                            name = r["tag"].ToString();
                    a.ThumbnailUri = thumb;
                    a.ActorRole = role;
                    a.ActorName = name;
                    actors.Add(a);
                }
            }

            return actors;
        }

        private Resolution GetContentResolution(XmlDocument metadata)
        {
            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            DataTable video = sections.Tables["Media"];
            DataRow row = video.Rows[0];
            int width = 0;
            if (video.Columns.Contains("width"))
                if (row["width"] != null)
                    width = Convert.ToInt32(row["width"]);
            int height = 0;
            if (video.Columns.Contains("height"))
                if (row["height"] != null)
                    height = Convert.ToInt32(row["height"]);
            Resolution result = new Resolution()
            {
                Width = width,
                Height = height
            };
            return result;
        }

        private string GetContentGenre(XmlDocument metadata)
        {
            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            DataTable video = sections.Tables["Genre"];
            string genre = "Unknown";
            if (!(video == null))
            {
                DataRow row = video.Rows[0];
                genre = row["tag"].ToString();
            }
            return genre;
        }

        private string GetTVShowSeason(XmlDocument metadata)
        {
            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            DataTable video = sections.Tables["Video"];
            string season = "Unknown Season";
            if (!(video == null))
            {
                DataRow row = video.Rows[0];
                season = row["parentTitle"].ToString();
            }
            return season;
        }

        private string GetTVShowTitle(XmlDocument metadata)
        {
            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            DataTable video = sections.Tables["Video"];
            string title = "Unknown Title";
            if (!(video == null))
            {
                DataRow row = video.Rows[0];
                title = row["grandparentTitle"].ToString();
            }
            return title;
        }

        private DataTable ReturnCorrectTable(bool directTable = false)
        {
            if (IsTVShow && !directTable)
                return episodesTable;
            else
            {
                if (IsFiltered)
                    return filteredTable;
                else
                    return titlesTable;
            }
        }

        #endregion MetadataParsers

        #endregion XML/Metadata

        #region Helpers

        #region StringIntHelpers

        private string GetToken()
        {
            int index = dgvServers.CurrentCell.RowIndex;
            Server s = plexServers[index];
            return s.accessToken;
        }

        private string GetBaseUri(bool incToken)
        {
            if (incToken)
            {
                return "http://" + settings.ConnectionInfo.PlexAddress + ":" + settings.ConnectionInfo.PlexPort + "/?X-Plex-Token=";
            }
            else
            {
                return "http://" + settings.ConnectionInfo.PlexAddress + ":" + settings.ConnectionInfo.PlexPort + "/";
            }
        }

        #endregion StringIntHelpers

        #region ProfileHelpers

        public void LoadProfile()
        {
            if (!IsConnected)
            {
                if (ofdLoadProfile.ShowDialog() == DialogResult.OK)
                {
                    DoLoadProfile(ofdLoadProfile.FileName);
                }
            }
            else
            {
                MessageBox.Show("You can't load profiles while you're connected; please disconnect first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void SaveProfile()
        {
            if (string.IsNullOrEmpty(settings.ConnectionInfo.PlexAccountToken))
            {
                MessageBox.Show("You need to specify an account token before saving a profile", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (sfdSaveProfile.ShowDialog() == DialogResult.OK)
                {
                    DoSaveProfile(sfdSaveProfile.FileName);
                }
            }
        }

        public void DoSaveProfile(string fileName, bool silent = false)
        {
            try
            {
                var subReq = settings;
                XmlSerializer xsSubmit = new XmlSerializer(typeof(ApplicationOptions));
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = ("\t"),
                    OmitXmlDeclaration = false
                };
                XmlWriterSettings xmlSettings = xmlWriterSettings;
                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww, xmlSettings))
                    {
                        xsSubmit.Serialize(sww, settings);

                        //delete the existing file; the user was asked if they wanted to replace it.
                        if (File.Exists(fileName))
                            File.Delete(fileName);

                        File.WriteAllText(fileName, sww.ToString());
                    }
                }

                if (!silent)
                {
                    MessageBox.Show("Successfully saved profile!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                AddToLog("Saved profile " + fileName);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "SaveProfileError");
                if (!silent)
                {
                    MessageBox.Show(ex.ToString(), "Error in saving XML Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        public void DoLoadProfile(string fileName, bool silent = false)
        {
            try
            {
                ApplicationOptions subReq = null;

                XmlSerializer serializer = new XmlSerializer(typeof(ApplicationOptions));

                StreamReader reader = new StreamReader(fileName);
                subReq = (ApplicationOptions)serializer.Deserialize(reader);
                reader.Close();
                settings = null;
                settings = subReq;

                if (!silent)
                {
                    MessageBox.Show("Successfully loaded profile!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                AddToLog("Loaded profile " + fileName);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "LoadProfileError");
                if (!silent)
                {
                    MessageBox.Show(ex.ToString(), "Error in loading XML Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        #endregion ProfileHelpers

        #region ConnectionHelpers

        private void Disconnect(bool silent = false)
        {
            if (IsConnected)
            {
                if (engine != null)
                {
                    CancelDownload();
                }
                ClearContentView();
                ClearTVViews();
                ClearLibraryViews();
                SetProgressLabel("Disconnected from Plex");
                SetConnect();
                SelectMoviesTab();
                IsConnected = false;

                if (!silent)
                    MessageBox.Show("Disconnected from Plex", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Connect()
        {
            try
            {
                using (Connect frm = new Connect())
                {
                    ConnectionInfo existingInfo = new ConnectionInfo
                    {
                        PlexAccountToken = settings.ConnectionInfo.PlexAccountToken
                    };
                    frm.ConnectionInfo = existingInfo;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        ConnectionInfo result = frm.ConnectionInfo;
                        settings.ConnectionInfo = result;
                        user.authenticationToken = result.PlexAccountToken;

                        if (!result.DirectOnly)
                        {

                            object serversResult;
                            if (settings.ConnectionInfo.RelaysOnly)
                                serversResult = PlexDL.WaitWindow.WaitWindow.Show(GetServerListWorker, "Getting Relays");
                            else
                                serversResult = PlexDL.WaitWindow.WaitWindow.Show(GetServerListWorker, "Getting Servers");

                            List<Server> servers = (List<Server>)serversResult;
                            if (servers.Count == 0)
                            {
                                //to make it look nicer xD
                                string r = "";
                                if (settings.ConnectionInfo.RelaysOnly)
                                    r = "relays";
                                else
                                    r = "servers";

                                DialogResult msg = MessageBox.Show("No " + r + " found for entered account token. Would you like to try a direct connection?", "Authentication Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                                if (msg == DialogResult.Yes)
                                {
                                    RunDirectConnect();
                                }
                                else if (msg == DialogResult.No)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                plexServers = servers;
                                RenderServersView(servers);
                            }
                        }
                        else
                        {
                            RunDirectConnect();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ConnectionError");
                MessageBox.Show("Connection Error:\n\n" + ex.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetContentGridViews();
                SetConnect();
            }
        }

        private void RunDirectConnect()
        {
            List<Server> servers = new List<Server>();
            using (DirectConnect frmDir = new DirectConnect())
            {
                frmDir.ConnectionInfo.PlexAccountToken = user.authenticationToken;
                if (frmDir.ShowDialog() == DialogResult.OK)
                {
                    settings.ConnectionInfo = frmDir.ConnectionInfo;
                    Server s = new Server()
                    {
                        accessToken = user.authenticationToken,
                        address = settings.ConnectionInfo.PlexAddress,
                        port = settings.ConnectionInfo.PlexPort,
                        name = "DirectConnect"
                    };
                    servers.Add(s);
                    plexServers = servers;
                    RenderServersView(servers);
                    DoConnectFromSelectedServer();
                }
            }
        }

        private void DoStreamExport()
        {
            try
            {
                if (dgvContent.SelectedRows.Count == 1 || dgvEpisodes.SelectedRows.Count == 1)
                {
                    PlexObject content = null;
                    if (IsTVShow)
                        content = GetTVObjectFromSelection();
                    else
                        content = GetObjectFromSelection();

                    if (sfdExport.ShowDialog() == DialogResult.OK)
                    {
                        ImportExport.MetadataToFile(sfdExport.FileName, content);
                    }
                }
            }
            catch (Exception ex)
            {
                //log and ignore
                AddToLog("Export error: " + ex.Message);
                LoggingHelpers.RecordException(ex.Message, "StreamExportError");
                return;
            }
        }

        private void DoConnectFromSelectedServer()
        {
            if (!doNotAttemptAgain)
            {
                if (dgvServers.SelectedRows.Count == 1)
                {
                    int index = dgvServers.CurrentCell.RowIndex;
                    Server s = plexServers[index];
                    string address = s.address;
                    int port = s.port;

                    ConnectionInfo connectInfo;

                    connectInfo = new ConnectionInfo()
                    {
                        PlexAccountToken = GetToken(),
                        PlexAddress = address,
                        PlexPort = port,
                        RelaysOnly = settings.ConnectionInfo.RelaysOnly
                    };

                    settings.ConnectionInfo = connectInfo;

                    string uri = GetBaseUri(true);
                    //MessageBox.Show(uri);
                    XmlDocument reply = (XmlDocument)PlexDL.WaitWindow.WaitWindow.Show(XmlGet.GetXMLTransactionWorker, "Connecting", new object[] { uri, false, true });
                    if (Methods.PlexXmlValid(reply))
                    {

                        if (settings.Generic.ShowConnectionSuccess)
                        {
                            MessageBox.Show("Connection successful!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        if (reply.ChildNodes.Count != 0)
                        {
                            PopulateLibrary(reply);
                        }
                        doNotAttemptAgain = true;
                    }
                    else
                    {
                        MessageBox.Show("Connection failed. Check that '" + s.name + "' exists, that you have the right address, that it is accessible from your network, and that you have permission to access it.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    SetProgressLabel("Connected to Plex");
                    SetDisconnect();
                    IsConnected = true;
                }
            }
        }

        #endregion ConnectionHelpers

        #endregion Helpers

        #region Workers

        #region UpdateWorkers

        private DataRow GetDataRowTbl(DataTable table, int index)
        {
            return table.Rows[index];
        }

        private void PopulateLibraryWorker(XmlDocument doc)
        {
            if (doc != null)
            {
                try
                {
                    DGVServersEnabled(false);
                    AddToLog("Library population requested");
                    string libraryDir = KeyGatherers.GetLibraryKey(doc).TrimEnd('/');
                    string baseUri = GetBaseUri(false);
                    string uriSectionKey = baseUri + libraryDir + "/?X-Plex-Token=";
                    System.Xml.XmlDocument xmlSectionKey = XmlGet.GetXMLTransaction(uriSectionKey);

                    string sectionDir = KeyGatherers.GetSectionKey(xmlSectionKey).TrimEnd('/');
                    string uriSections = baseUri + libraryDir + "/" + sectionDir + "/?X-Plex-Token=";
                    System.Xml.XmlDocument xmlSections = XmlGet.GetXMLTransaction(uriSections);

                    AddToLog("Creating new datasets");
                    DataSet sections = new DataSet();
                    sections.ReadXml(new XmlNodeReader(xmlSections));

                    DataTable sectionsTable;
                    sectionsTable = sections.Tables["Directory"];
                    gSectionsTable = sectionsTable;

                    AddToLog("Binding to grid");
                    RenderLibraryView(sectionsTable);
                    IsLibraryFilled = true;
                    uri = baseUri + libraryDir + "/" + sectionDir + "/";
                    //we can render the content view if a row is already selected
                    DGVServersEnabled(true);
                }
                catch (WebException ex)
                {
                    LoggingHelpers.RecordException(ex.Message, "LibPopError");
                    DGVServersEnabled(true);
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        if (ex.Response is HttpWebResponse response)
                        {
                            switch ((int)response.StatusCode)
                            {
                                case 401:
                                    MessageBox.Show("The web server denied access to the resource. Check your token and try again. (401)");
                                    break;

                                case 404:
                                    MessageBox.Show("The web server couldn't serve the request because it couldn't find the resource specified. (404)");
                                    break;

                                case 400:
                                    MessageBox.Show("The web server couldn't serve the request because the request was bad. (400)");
                                    break;
                            }
                        }
                        else
                        {
                            // no http status code available
                        }
                    }
                    else
                    {
                        // no http status code available
                    }
                }
                catch (Exception ex)
                {
                    LoggingHelpers.RecordException(ex.Message, "LibPopError");
                    DGVServersEnabled(true);
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void GetTitlesTable(XmlDocument doc, bool isTVShow)
        {
            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            if (isTVShow)
            {
                titlesTable = sections.Tables["Directory"];
            }
            else
            {
                titlesTable = sections.Tables["Video"];
            }
        }

        private void UpdateContentViewWorker(XmlDocument doc, bool isTVShow)
        {
            DGVLibraryEnabled(false);

            AddToLog("Updating library contents");

            GetTitlesTable(doc, isTVShow);

            IsTVShow = isTVShow;

            if (IsTVShow)
            {
                AddToLog("Rendering TV Shows");
                RenderTVView(titlesTable);
            }
            else
            {
                AddToLog("Rendering Movies");
                RenderContentView(titlesTable);
            }
            contentXmlDoc = doc;

            DGVLibraryEnabled(true);

            //MessageBox.Show("ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + titlesTable.Rows.Count.ToString());
        }

        private void UpdateEpisodeViewWorker(XmlDocument doc)
        {
            DGVSeasonsEnabled(false);
            AddToLog("Updating episode contents");

            AddToLog("Creating datasets");
            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            episodesTable = sections.Tables["Video"];

            AddToLog("Cleaning unwanted data");

            AddToLog("Binding to grid");
            RenderEpisodesView(episodesTable);

            contentXmlDoc = doc;

            DGVSeasonsEnabled(true);

            //MessageBox.Show("ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + titlesTable.Rows.Count.ToString());
        }

        private void UpdateSeriesViewWorker(XmlDocument doc)
        {
            DGVContentEnabled(false);
            AddToLog("Updating series contents");

            AddToLog("Creating datasets");
            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            seriesTable = sections.Tables["Directory"];

            AddToLog("Cleaning unwanted data");

            AddToLog("Binding to grid");
            RenderSeriesView(seriesTable);

            contentXmlDoc = doc;

            DGVContentEnabled(true);

            //MessageBox.Show("ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + titlesTable.Rows.Count.ToString());
        }

        #endregion UpdateWorkers

        

        #region BackgroundWorkers

        private void WkrGetMetadata_DoWork(object sender, DoWorkEventArgs e)
        {
            AddToLog("Metadata worker started");
            AddToLog("Doing directory checks");
            if (string.IsNullOrEmpty(settings.Generic.DownloadDirectory) || string.IsNullOrWhiteSpace(settings.Generic.DownloadDirectory))
            {
                ResetDownloadDirectory();
            }
            string tv = settings.Generic.DownloadDirectory + @"\TV";
            string movies = settings.Generic.DownloadDirectory + @"\Movies";
            if (!System.IO.Directory.Exists(tv))
            {
                System.IO.Directory.CreateDirectory(tv);
                AddToLog("Created " + tv);
            }
            if (!System.IO.Directory.Exists(movies))
            {
                System.IO.Directory.CreateDirectory(movies);
                AddToLog(movies);
            }
            AddToLog("Grabbing metadata");
            if (IsTVShow)
            {
                AddToLog("Worker is to grab TV Show metadata");
                if (DownloadAllEpisodes)
                {
                    AddToLog("Worker is to grab metadata for All Episodes");
                    int index = 0;
                    foreach (DataRow r in episodesTable.Rows)
                    {
                        decimal percent = Math.Round(((decimal)index + 1) / episodesTable.Rows.Count) * 100;
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            lblProgress.Text = "Getting Metadata " + (index + 1) + "/" + episodesTable.Rows.Count;
                        });
                        PlexTVShow show = GetTVObjectFromIndex(index);
                        DownloadInfo dlInfo = show.StreamInformation;
                        TVShowDirectoryLayout dir = DownloadLayout.CreateDownloadLayoutTVShow(show, settings, DownloadLayout.PlexStandardLayout);
                        dlInfo.DownloadPath = dir.SeasonPath;
                        queue.Add(dlInfo);
                        index += 1;
                    }
                }
                else
                {
                    AddToLog("Worker is to grab Single Episode metadata");
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        lblProgress.Text = "Getting Metadata";
                    });
                    PlexTVShow show = GetTVObjectFromSelection();
                    DownloadInfo dlInfo = show.StreamInformation;
                    TVShowDirectoryLayout dir = DownloadLayout.CreateDownloadLayoutTVShow(show, settings, DownloadLayout.PlexStandardLayout);
                    dlInfo.DownloadPath = dir.SeasonPath;
                    queue.Add(dlInfo);
                }
            }
            else
            {
                AddToLog("Worker is to grab Movie metadata");
                this.BeginInvoke((MethodInvoker)delegate
                {
                    lblProgress.Text = "Getting Metadata";
                });
                PlexMovie movie = GetObjectFromSelection();
                DownloadInfo dlInfo = movie.StreamInformation;
                dlInfo.DownloadPath = settings.Generic.DownloadDirectory + @"\Movies";
                queue.Add(dlInfo);
            }
            AddToLog("Worker is to invoke downloader thread");
            this.BeginInvoke((MethodInvoker)delegate
            {
                StartDownload(queue, settings.Generic.DownloadDirectory);
                AddToLog("Worker has started the download process");
            });
        }

        #endregion BackgroundWorkers

        #region UpdateCallbackWorkers

        private void WorkerUpdateContentView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            XmlDocument doc = (XmlDocument)e.Arguments[0];
            UpdateContentViewWorker(doc, (bool)e.Arguments[1]);
        }

        private void WorkerRenderContentView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            DataTable t = (DataTable)e.Arguments[0];
            RenderContentView(t);
        }

        private void WorkerRenderTVView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            DataTable t = (DataTable)e.Arguments[0];
            RenderTVView(t);
        }

        private void WorkerUpdateLibraryView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            XmlDocument doc = (XmlDocument)e.Arguments[0];
            PopulateLibraryWorker(doc);
        }

        private void WorkerUpdateSeriesView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            XmlDocument doc = (XmlDocument)e.Arguments[0];
            UpdateSeriesViewWorker(doc);
        }

        private void WorkerUpdateEpisodesView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            XmlDocument doc = (XmlDocument)e.Arguments[0];
            UpdateEpisodeViewWorker(doc);
        }

        private void WorkerUpdateServersView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            List<Server> servers = (List<Server>)e.Arguments[0];
            RenderServersViewWorker(servers);
        }

        #endregion UpdateCallbackWorkers

        #region ContentRenderers

        private void RenderContentView(DataTable content)
        {
            if (!(content == null))
            {
                ClearTVViews();
                ClearContentView();

                List<string> wantedColumns = settings.DataDisplay.ContentView.ContentDisplayColumns;
                List<string> wantedCaption = settings.DataDisplay.ContentView.ContentDisplayCaption;

                RenderStruct info = new RenderStruct() { Data = content, WantedColumns = wantedColumns, WantedCaption = wantedCaption };

                contentViewTable = GenericRenderer.RenderView(info, dgvContent);

                if (this.InvokeRequired)
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        SelectMoviesTab();
                    });
                else
                {
                    SelectMoviesTab();
                }
            }
        }

        private void SelectMoviesTab(bool checkSelected = true)
        {
            if (checkSelected)
            {
                if (tabMain.SelectedTab != tabMovies)
                {
                    tabMain.SelectedTab = tabMovies;
                }
            }
            else
            {
                tabMain.SelectedTab = tabMovies;
            }
        }

        private void SelectTVTab(bool checkSelected = true)
        {
            if (checkSelected)
            {
                if (tabMain.SelectedTab != tabTV)
                {
                    tabMain.SelectedTab = tabTV;
                }
            }
            else
            {
                tabMain.SelectedTab = tabTV;
            }
        }

        private void ClearContentView()
        {
            dgvContent.DataSource = null;
        }

        private void ClearTVViews()
        {
            dgvSeasons.DataSource = null;
            dgvEpisodes.DataSource = null;
            dgvTVShows.DataSource = null;
        }

        private void ClearLibraryViews(bool clearServers = true, bool clearLibrary = true)
        {
            if (clearServers)
                dgvServers.DataSource = null;
            if (clearLibrary)
                dgvLibrary.DataSource = null;
        }

        private void RenderTVView(DataTable content)
        {
            if (content == null)
            {
                return;
            }
            ClearTVViews();
            ClearContentView();

            List<string> wantedColumns = settings.DataDisplay.TVView.TVDisplayColumns;
            List<string> wantedCaption = settings.DataDisplay.TVView.TVDisplayCaption;

            RenderStruct info = new RenderStruct() { Data = content, WantedColumns = wantedColumns, WantedCaption = wantedCaption };

            tvViewTable = GenericRenderer.RenderView(info, dgvTVShows);

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    SelectTVTab();
                });
            else
            {
                SelectTVTab();
            }
        }

        private void RenderSeriesView(DataTable content)
        {
            if (content == null)
            {
                return;
            }
            List<string> wantedColumns = settings.DataDisplay.SeriesView.SeriesDisplayColumns;
            List<string> wantedCaption = settings.DataDisplay.SeriesView.SeriesDisplayCaption;

            RenderStruct info = new RenderStruct() { Data = content, WantedColumns = wantedColumns, WantedCaption = wantedCaption };

            GenericRenderer.RenderView(info, dgvSeasons);
        }

        private void RenderEpisodesView(DataTable content)
        {
            if (content == null)
            {
                return;
            }
            List<string> wantedColumns = settings.DataDisplay.EpisodesView.EpisodesDisplayColumns;
            List<string> wantedCaption = settings.DataDisplay.EpisodesView.EpisodesDisplayCaption;

            RenderStruct info = new RenderStruct() { Data = content, WantedColumns = wantedColumns, WantedCaption = wantedCaption };

            GenericRenderer.RenderView(info, dgvEpisodes);
        }

        private void RenderServersView(List<Server> servers)
        {
            PlexDL.WaitWindow.WaitWindow.Show(this.WorkerUpdateServersView, "Rendering Servers", new object[] { servers });
        }

        private void RenderServersViewWorker(List<Server> servers)
        {
            ServerViewRenderer.RenderView(servers, dgvServers);
        }

        private void DgvLibrary_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvLibrary.SortOrder.ToString() == "Descending") // Check if sorting is Descending
            {
                gSectionsTable.DefaultView.Sort = dgvLibrary.SortedColumn.Name + " DESC"; // Get Sorted Column name and sort it in Descending order
            }
            else
            {
                gSectionsTable.DefaultView.Sort = dgvLibrary.SortedColumn.Name + " ASC";  // Otherwise sort it in Ascending order
            }
            gSectionsTable = gSectionsTable.DefaultView.ToTable(); // The Sorted View converted to DataTable and then assigned to table object.
        }

        private void RenderLibraryView(DataTable content)
        {
            if (content == null)
            {
                return;
            }
            List<string> wantedColumns = settings.DataDisplay.LibraryView.LibraryDisplayColumns;
            List<string> wantedCaption = settings.DataDisplay.LibraryView.LibraryDisplayCaption;

            RenderStruct info = new RenderStruct() { Data = content, WantedColumns = wantedColumns, WantedCaption = wantedCaption };

            GenericRenderer.RenderView(info, dgvLibrary);
        }

        #endregion ContentRenderers

        #region UpdateWaitWorkers

        private void UpdateContentView(XmlDocument content, bool IsTVShow)
        {
            PlexDL.WaitWindow.WaitWindow.Show(this.WorkerUpdateContentView, "Updating Content", new object[] { content, IsTVShow });
        }

        private void UpdateSeriesView(XmlDocument content)
        {
            PlexDL.WaitWindow.WaitWindow.Show(this.WorkerUpdateSeriesView, "Updating Series", new object[] { content });
        }

        private void UpdateEpisodeView(XmlDocument content)
        {
            PlexDL.WaitWindow.WaitWindow.Show(this.WorkerUpdateEpisodesView, "Updating Episodes", new object[] { content });
        }

        private void PopulateLibrary(XmlDocument content)
        {
            PlexDL.WaitWindow.WaitWindow.Show(this.WorkerUpdateLibraryView, "Updating Library", new object[] { content });
        }

        #endregion UpdateWaitWorkers

        #region PlexAPIWorkers

        private void GetServerListWorker(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            Helpers.CacheStructureBuilder();
            if (!settings.ConnectionInfo.RelaysOnly)
            {
                if (ServerCaching.ServerInCache(user.authenticationToken))
                {
                    e.Result = ServerCaching.ServerFromCache(user.authenticationToken);
                }
                else
                {
                    List<Server> result = plex.GetServers(user);
                    ServerCaching.ServerToCache(result, user.authenticationToken);
                    e.Result = result;
                }
            }
            else
            {
                List<Server> result = Relays.GetServerRelays(user.authenticationToken);
                e.Result = result;
            }
        }

        private void GetObjectFromSelectionWorker(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            e.Result = GetObjectFromSelection();
        }

        private void GetTVObjectFromSelectionWorker(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            e.Result = GetTVObjectFromSelection();
        }

        #endregion PlexAPIWorkers

        #region SearchWorkers

        private void GetSearchEnum(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            string column = (string)e.Arguments[2];
            int searchRule = (int)e.Arguments[1];
            string searchKey = (string)e.Arguments[0];
            DataRow[] rowCollec = titlesTable.Select(SearchRuleIDs.SQLSearchFromRule(column, searchKey, searchRule));
            e.Result = rowCollec;
        }

        private void GetSearchTable(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            DataRow[] rowCollec = (DataRow[])e.Arguments[0];
            e.Result = rowCollec.CopyToDataTable();
        }

        #endregion SearchWorkers

        #endregion Workers

        #region Logging

        private void AddToLog(string logEntry)
        {
            logIncrementer += 1;
            string[] headers = { "ID", "DateTime", "Entry" };
            string[] logEntryToAdd = { logIncrementer.ToString(), DateTime.Now.ToString(), logEntry };
            string logLine = ">>" + logEntry;
            if (lstLog.InvokeRequired)
            {
                lstLog.BeginInvoke((MethodInvoker)delegate
                {
                    lstLog.Items.Add(logLine);
                });
            }
            else
                lstLog.Items.Add(logLine);
            if (settings.Logging.EnableGenericLogDel)
                LoggingHelpers.LogDelWriter("PlexDL.logdel", headers, logEntryToAdd);
        }

        private void DGVDataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            DataGridView parent = (DataGridView)sender;
            //don't show the event to the user; but log it.
            AddToLog("Experienced data error in " + parent.Name);
            e.Cancel = true;
        }

        #endregion Logging

        #region Download

        #region DownloadInfoGatherers

        private DownloadInfo GetContentDownloadInfo_Xml(XmlDocument xml)
        {
            if (Methods.PlexXmlValid(xml))
            {
                DownloadInfo obj = new DownloadInfo();

                AddToLog("Creating new datasets");
                DataSet sections = new DataSet();
                sections.ReadXml(new XmlNodeReader(xml));

                DataTable part = sections.Tables["Part"];
                DataRow video = sections.Tables["Video"].Rows[0];
                string title = video["title"].ToString();
                if (title.Length > settings.Generic.DefaultStringLength)
                    title = title.Substring(0, settings.Generic.DefaultStringLength);
                string thumb = video["thumb"].ToString();
                string thumbnailFullUri = "";
                if (string.IsNullOrEmpty(thumb))
                {
                }
                else
                {
                    string baseUri = GetBaseUri(false).TrimEnd('/');
                    thumbnailFullUri = baseUri + thumb + "?X-Plex-Token=" + GetToken();
                }

                DataRow partRow = part.Rows[0];

                string filePart = "";
                string container = "";
                if (partRow["key"] != null)
                    filePart = partRow["key"].ToString();
                if (partRow["container"] != null)
                    container = partRow["container"].ToString();
                long byteLength = Convert.ToInt64(partRow["size"]);
                int contentDuration = Convert.ToInt32(partRow["duration"]);
                string fileName = Common.Methods.RemoveIllegalCharacters(title + "." + container);

                string link = GetBaseUri(false).TrimEnd('/') + filePart + "/?X-Plex-Token=" + GetToken();
                obj.Link = link;
                obj.Container = container;
                obj.ByteLength = byteLength;
                obj.ContentDuration = contentDuration;
                obj.FileName = fileName;
                obj.ContentTitle = title;
                obj.ContentThumbnailUri = thumbnailFullUri;

                return obj;
            }
            else
            {
                return new DownloadInfo();
            }
        }

        #endregion DownloadInfoGatherers

        #region DownloadMethods

        private void CancelDownload()
        {
            if (wkrGetMetadata.IsBusy)
            {
                wkrGetMetadata.Abort();
            }
            if (IsEngineRunning)
            {
                engine.Cancel();
                engine.Clear();
            }
            if (IsDownloadRunning)
            {
                SetProgressLabel("Download Cancelled");
                SetDownloadStart();
                SetResume();
                pbMain.Value = pbMain.Maximum;
                btnPause.Enabled = false;
                AddToLog("Download Cancelled");
                IsDownloadRunning = false;
                IsDownloadPaused = false;
                IsEngineRunning = false;
                IsDownloadQueueCancelled = true;
                downloadsSoFar = 0;
                DownloadTotal = 0;
                DownloadIndex = 0;
                MessageBox.Show("Download cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartDownloadEngine()
        {
            engine.QueueProgressChanged += Engine_DownloadProgressChanged;
            engine.QueueCompleted += Engine_DownloadCompleted;

            engine.StartAsync();
            //MessageBox.Show("Started!");
            AddToLog("Download is Progressing");
            IsDownloadRunning = true;
            IsEngineRunning = true;
            IsDownloadPaused = false;
            SetPause();
        }

        private void SetDownloadDirectory()
        {
            if (fbdSave.ShowDialog() == DialogResult.OK)
            {
                settings.Generic.DownloadDirectory = fbdSave.SelectedPath;
                MessageBox.Show("Download directory updated to " + settings.Generic.DownloadDirectory, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AddToLog("Download directory updated to " + settings.Generic.DownloadDirectory);
            }
        }

        private void SetDownloadCompleted()
        {
            pbMain.Value = pbMain.Maximum;
            btnPause.Enabled = false;
            SetResume();
            SetDownloadStart();
            SetProgressLabel("Download Completed");
            AddToLog("Download completed");
            engine.Clear();
            IsDownloadRunning = false;
            IsDownloadPaused = false;
            IsEngineRunning = false;
        }

        private void StartDownload(List<DownloadInfo> queue, string location)
        {
            AddToLog("Download Process Started");
            pbMain.Value = 0;

            AddToLog("Starting HTTP Engine");
            engine = new AltoHttp.DownloadQueue();
            if (queue.Count > 1)
            {
                foreach (DownloadInfo dl in queue)
                {
                    string fqPath = dl.DownloadPath + @"\" + dl.FileName;
                    if (File.Exists(fqPath))
                    {
                        AddToLog(dl.FileName + " already exists; will not download.");
                    }
                    else
                    {
                        engine.Add(dl.Link, fqPath);
                    }
                }
            }
            else
            {
                DownloadInfo dl = queue[0];
                string fqPath = dl.DownloadPath + @"\" + dl.FileName;
                if (File.Exists(fqPath))
                {
                    AddToLog(dl.FileName + " already exists; get user confirmation.");
                    DialogResult msg = MessageBox.Show(dl.FileName + " already exists. Skip this title?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msg == DialogResult.Yes)
                    {
                        SetDownloadCompleted();
                        return;
                    }
                }
                engine.Add(dl.Link, fqPath);
            }
            btnPause.Enabled = true;
            StartDownloadEngine();
        }

        #endregion DownloadMethods

        #region DownloadEngineMethods

        private void ShowBalloon(string msg, string title, bool error = false, int timeout = 2000)
        {
            if (!this.InvokeRequired)
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
                this.BeginInvoke((MethodInvoker)delegate
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
            double CurrentProgress = Math.Round((double)engine.CurrentProgress);
            string speed = Common.Methods.FormatBytes(engine.CurrentDownloadSpeed) + "/s";
            string order = "(" + (engine.CurrentIndex + 1).ToString() + "/" + engine.QueueLength.ToString() + ")";

            SetProgressLabel(CurrentProgress.ToString() + "% " + order + " @ " + speed);

            pbMain.Value = (int)CurrentProgress;

            //MessageBox.Show("Started!");
        }

        #endregion DownloadEngineMethods

        #endregion Download

        #region Search

        private void ClearSearch()
        {
            if (IsTVShow)
                RenderTVView(titlesTable);
            else
                RenderContentView(titlesTable);
            filteredTable = null;
            IsFiltered = false;
            SetStartSearch();
        }

        private void RunTitleSearch()
        {
            try
            {
                AddToLog("Title search requested");
                if ((dgvContent.Rows.Count > 0) || (dgvTVShows.Rows.Count > 0))
                {
                    SearchOptions start;
                    if (IsTVShow)
                        start = new SearchOptions() { SearchCollection = tvViewTable };
                    else
                        start = new SearchOptions() { SearchCollection = contentViewTable };
                    SearchOptions result = SearchForm.ShowSearch(start);
                    if (!string.IsNullOrEmpty(result.SearchTerm) && !string.IsNullOrEmpty(result.SearchColumn))
                    {
                        filteredTable = null;
                        GetFilteredTable(result.SearchTerm, result.SearchColumn, result.SearchRule, false);
                        if (filteredTable != null)
                        {
                            //DisableContentSorting();
                            SetCancelSearch();
                            if (IsTVShow)
                                PlexDL.WaitWindow.WaitWindow.Show(WorkerRenderTVView, "Rendering TV", new object[] { filteredTable });
                            else
                                PlexDL.WaitWindow.WaitWindow.Show(WorkerRenderContentView, "Rendering Movies", new object[] { filteredTable });
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    AddToLog("No data to search");
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "SearchError");
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void GetFilteredTable(string query, string column, int searchRule, bool silent = true)
        {
            DataTable tblFiltered;
            DataRow[] rowCollec = (DataRow[])PlexDL.WaitWindow.WaitWindow.Show(GetSearchEnum, "Filtering Records", new object[] { query, searchRule, column });
            FilterQuery = query;
            if (rowCollec.Count() > 0)
            {
                tblFiltered = (DataTable)PlexDL.WaitWindow.WaitWindow.Show(GetSearchTable, "Binding", new object[] { rowCollec });
            }
            else
            {
                if (!silent)
                    MessageBox.Show("No Results Found for '" + query + "'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            IsFiltered = true;
            filteredTable = tblFiltered;
            //MessageBox.Show("Filtered Table:" + filteredTable.Rows.Count + "\nTitles Table:" + titlesTable.Rows.Count);
        }

        #endregion Search

        #region UIMethods

        private void DGVContentEnabled(bool enabled)
        {
            if (dgvContent.InvokeRequired)
            {
                dgvContent.BeginInvoke((MethodInvoker)delegate
                {
                    dgvContent.Enabled = enabled;
                });
            }
            else
            {
                dgvContent.Enabled = enabled;
            }
        }

        private void DGVLibraryEnabled(bool enabled)
        {
            if (dgvLibrary.InvokeRequired)
            {
                dgvLibrary.BeginInvoke((MethodInvoker)delegate
                {
                    dgvLibrary.Enabled = enabled;
                });
            }
            else
            {
                dgvLibrary.Enabled = enabled;
            }
        }

        private void DGVSeasonsEnabled(bool enabled)
        {
            if (dgvSeasons.InvokeRequired)
            {
                dgvSeasons.BeginInvoke((MethodInvoker)delegate
                {
                    dgvSeasons.Enabled = enabled;
                });
            }
            else
            {
                dgvSeasons.Enabled = enabled;
            }
        }

        private void DGVServersEnabled(bool enabled)
        {
            if (dgvServers.InvokeRequired)
            {
                dgvServers.BeginInvoke((MethodInvoker)delegate
                {
                    dgvServers.Enabled = enabled;
                });
            }
            else
            {
                dgvServers.Enabled = enabled;
            }
        }

        //HOTKEYS
        //DO NOT CHANGE
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {

                case (Keys.Control | Keys.F):
                    SearchProcedure();
                    return true;

                case (Keys.Control | Keys.M):
                    Metadata();
                    return true;

                case (Keys.Control | Keys.O):
                    LoadProfile();
                    return true;

                case (Keys.Control | Keys.S):
                    SaveProfile();
                    return true;

                case (Keys.Control | Keys.E):
                    if (IsConnected)
                        DoStreamExport();
                    return true;

                case (Keys.Control | Keys.C):
                    if (!IsConnected)
                        Connect();
                    return true;

                case (Keys.Control | Keys.D):
                    if (IsConnected)
                        Disconnect();
                    return true;
                case (Keys.Control | Keys.L):
                    ShowLogViewer();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Thread-safe way of changing the progress label
        /// </summary>
        /// <param name="status"></param>
        private void SetProgressLabel(string status)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    lblProgress.Text = status;
                });
            }
            else
                lblProgress.Text = status;
        }

        private void SetDownloadCancel()
        {
            ImageSet images = new ImageSet() { Idle = Resources.baseline_cancel_black_18dp, Focus = Resources.baseline_cancel_black_18dp_white };
            btnDownload.ImageSet = images;
        }

        private void SetDownloadStart()
        {
            ImageSet images = new ImageSet() { Idle = Resources.baseline_cloud_download_black_18dp, Focus = Resources.baseline_cloud_download_black_18dp_white };
            btnDownload.ImageSet = images;
        }

        private void SetPause()
        {
            ImageSet images = new ImageSet() { Idle = Resources.baseline_pause_black_18dp, Focus = Resources.baseline_pause_black_18dp_white };
            btnPause.ImageSet = images;
        }

        private void SetResume()
        {
            ImageSet images = new ImageSet() { Idle = Resources.baseline_play_arrow_black_18dp, Focus = Resources.baseline_play_arrow_black_18dp_white };
            btnPause.ImageSet = images;
        }

        private void SetStartSearch()
        {
            ImageSet images = new ImageSet() { Idle = Resources.baseline_search_black_18dp, Focus = Resources.baseline_search_black_18dp_white };
            btnSearch.ImageSet = images;
        }

        private void SetCancelSearch()
        {
            ImageSet images = new ImageSet() { Idle = Resources.baseline_cancel_black_18dp, Focus = Resources.baseline_cancel_black_18dp_white };
            btnSearch.ImageSet = images;
        }

        private void SetConnect()
        {
            ImageSet images = new ImageSet() { Idle = Resources.baseline_power_black_18dp, Focus = Resources.baseline_power_black_18dp_white };
            btnConnect.ImageSet = images;
        }

        private void SetDisconnect()
        {
            ImageSet images = new ImageSet() { Idle = Resources.baseline_power_off_black_18dp, Focus = Resources.baseline_power_off_black_18dp_white };
            btnConnect.ImageSet = images;
        }

        #endregion UIMethods

        #region FormEvents

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (IsDownloadRunning)
            {
                if (!(IsMsgAlreadyShown))
                {
                    DialogResult msg = MessageBox.Show("Are you sure you want to exit PlexDL? A download is still running.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msg == DialogResult.Yes)
                    {
                        IsMsgAlreadyShown = true;
                        AddToLog("PlexDL Exited");
                        e.Cancel = false;
                    }
                }
            }
            else
            {
                AddToLog("PlexDL Exited");

                if (settings.Generic.AnimationSpeed > 0)
                {
                    t1 = new Timer
                    {
                        Interval = settings.Generic.AnimationSpeed
                    };
                    t1.Tick += new EventHandler(FadeOut);  //this calls the fade out function
                    t1.Start();

                    if (Opacity == 0)
                    {
                        //resume the event - the program can be closed
                        e.Cancel = false;
                    }
                }
            }
        }

        private void ResetDownloadDirectory()
        {
            string curUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            settings.Generic.DownloadDirectory = curUser + "\\Videos\\PlexDL";
            if (!(System.IO.Directory.Exists(settings.Generic.DownloadDirectory)))
            {
                System.IO.Directory.CreateDirectory(settings.Generic.DownloadDirectory);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            TestForm frm = new TestForm();
            //frm.ShowDialog();

            if (settings.Generic.AnimationSpeed > 0)
            {
                Opacity = 0;      //first the opacity is 0

                t1.Interval = settings.Generic.AnimationSpeed;  //we'll increase the opacity every 10ms
                t1.Tick += new EventHandler(FadeIn);  //this calls the function that changes opacity
                t1.Start();
            }
            try
            {
                ResetDownloadDirectory();
                AddToLog("PlexDL Started");
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "StartupError");
                MessageBox.Show("Startup Error:\n\n" + ex.ToString(), "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void UpdateFromLibraryKey(string key, bool isTVShow)
        {
            object[] args = new object[] { key, isTVShow };
            WaitWindow.WaitWindow.Show(UpdateFromLibraryKey_Worker, "Getting Metadata", args);
        }

        private void UpdateFromLibraryKey_Worker(object sender, WaitWindow.WaitWindowEventArgs e)
        {
            bool isTVShow = (bool)e.Arguments[1];
            string key = (string)e.Arguments[0];
            try
            {
                AddToLog("Requesting ibrary contents");
                string contentUri = uri + key + "/all/?X-Plex-Token=";
                XmlDocument contentXml = XmlGet.GetXMLTransaction(contentUri);

                contentXmlDoc = contentXml;
                UpdateContentView(contentXml, isTVShow);
            }
            catch (WebException ex)
            {
                LoggingHelpers.RecordException(ex.Message, "UpdateLibraryError");
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (ex.Response is HttpWebResponse response)
                    {
                        switch ((int)response.StatusCode)
                        {
                            case 401:
                                MessageBox.Show("The web server denied access to the resource. Check your token and try again. (401)");
                                break;

                            case 404:
                                MessageBox.Show("The web server couldn't serve the request because it couldn't find the resource specified. (404)");
                                break;

                            case 400:
                                MessageBox.Show("The web server couldn't serve the request because the request was bad. (400)");
                                break;
                        }
                    }
                    else
                    {
                        // no http status code available
                    }
                }
                else
                {
                    // no http status code available
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "UpdateLibraryError");
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void CxtLibrarySections_Opening(object sender, CancelEventArgs e)
        {
            if (dgvLibrary.Rows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        #endregion FormEvents

        #region DGVRowChanges

        private void DgvLibrary_OnRowChange(object sender, EventArgs e)
        {
            if ((dgvLibrary.SelectedRows.Count == 1) && (IsLibraryFilled))
            {
                AddToLog("Selection Changed");
                int index = GetTableIndexFromDGV(dgvLibrary, gSectionsTable);
                DataRow r = GetDataRowLibrary(index);

                string key = "";
                string type = "";
                if (r != null)
                {
                    if (r["key"] != null)
                        key = r["key"].ToString();
                    if (r["type"] != null)
                        type = r["type"].ToString();
                }
                if (type == "show")
                {
                    UpdateFromLibraryKey(key, true);
                }
                else if (type == "movie")
                {
                    UpdateFromLibraryKey(key, false);
                }
            }
        }

        private void DgvSeasons_OnRowChange(object sender, EventArgs e)
        {
            if (dgvSeasons.SelectedRows.Count == 1)
            {
                int index = GetTableIndexFromDGV(dgvSeasons, seriesTable);
                XmlDocument episodes = GetEpisodeXml(index);
                UpdateEpisodeView(episodes);
            }
        }

        private void DgvContent_OnRowChange(object sender, EventArgs e)
        {
            //nothing, more or less.
        }

        private void DgvTVShows_OnRowChange(object sender, EventArgs e)
        {
            if (dgvTVShows.SelectedRows.Count == 1)
            {
                int index;
                if (IsFiltered)
                {
                    index = GetTableIndexFromDGV(dgvTVShows, filteredTable);
                }
                else
                {
                    index = GetTableIndexFromDGV(dgvTVShows, titlesTable);
                }

                if (IsTVShow)
                {
                    XmlDocument series = GetSeriesXml(index);
                    UpdateSeriesView(series);
                }
            }
        }

        private void DgvServers_OnRowChange(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count == 1)
            {
                int index = dgvServers.CurrentCell.RowIndex;
                Server s = plexServers[index];
                string address = s.address;
                if (address != settings.ConnectionInfo.PlexAddress)
                {
                    doNotAttemptAgain = false;
                    //MessageBox.Show("attempted connection");
                    DoConnectFromSelectedServer();
                }
            }
        }

        #endregion DGVRowChanges

        #region ButtonClicks

        private void BtnTest_Click(object sender, EventArgs e)
        {
            try
            {
                //deprecated (planned reintroduction)
                string uri = GetBaseUri(true);
                XmlDocument reply = (XmlDocument)PlexDL.WaitWindow.WaitWindow.Show(XmlGet.GetXMLTransactionWorker, "Connecting", new object[] { uri });
                MessageBox.Show("Connection successful!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (WebException ex)
            {
                LoggingHelpers.RecordException(ex.Message, "TestConnectionError");
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (ex.Response is HttpWebResponse response)
                    {
                        switch ((int)response.StatusCode)
                        {
                            case 401:
                                MessageBox.Show("The web server denied access to the resource. Check your token and try again. (401)");
                                break;

                            case 404:
                                MessageBox.Show("The web server couldn't serve the request because it couldn't find the resource specified. (404)");
                                break;

                            case 400:
                                MessageBox.Show("The web server couldn't serve the request because the request was bad. (400)");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unknown status code; the server failed to serve the request. (?)");
                    }
                }
                else
                {
                    MessageBox.Show("An unknown error occurred:\n\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool CheckForInternetConnection()
        {
            return InternetGetConnectedState(out _, 0);
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnected)
                {
                    if (CheckForInternetConnection())
                    {
                        Connect();
                    }
                    else
                    {
                        MessageBox.Show("No internet connection", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Disconnect();
                }
            }
            catch (WebException ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ConnectionError");
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (ex.Response is HttpWebResponse response)
                    {
                        switch ((int)response.StatusCode)
                        {
                            case 401:
                                MessageBox.Show("The web server denied access to the resource. Check your token and try again. (401)");
                                break;

                            case 404:
                                MessageBox.Show("The web server couldn't serve the request because it couldn't find the resource specified. (404)");
                                break;

                            case 400:
                                MessageBox.Show("The web server couldn't serve the request because the request was bad. (400)");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unknown status code; the server failed to serve the request. (?)");
                    }
                }
                else
                {
                    MessageBox.Show("An unknown error occurred:\n\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        private void DoDownloadAllEpisodes()
        {
            AddToLog("Awaiting download safety checks");
            if (!IsDownloadRunning && !IsEngineRunning)
            {
                AddToLog("Download process is starting");
                SetProgressLabel("Waiting");
                DownloadAllEpisodes = true;
                DownloadTotal = episodesTable.Rows.Count;
                IsDownloadRunning = true;
                wkrGetMetadata.RunWorkerAsync();
                AddToLog("Worker invoke process started");
                SetDownloadCancel();
            }
            else
                AddToLog("Download process failed; download is already running.");
        }

        private void DoDownloadSelected()
        {
            AddToLog("Awaiting download safety checks");
            if (!IsDownloadRunning && !IsEngineRunning)
            {
                AddToLog("Download process is starting");
                SetProgressLabel("Waiting");
                DownloadAllEpisodes = false;
                DownloadTotal = 1;
                IsDownloadRunning = true;
                wkrGetMetadata.RunWorkerAsync();
                AddToLog("Worker invoke process started");
                SetDownloadCancel();
            }
            else
                AddToLog("Download process failed; download is already running.");
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            if ((dgvContent.SelectedRows.Count == 1) || (dgvEpisodes.SelectedRows.Count == 1))
            {
                if (!IsDownloadRunning && !IsEngineRunning)
                {
                    queue = new List<DownloadInfo>();
                    if (IsTVShow)
                    {
                        if (dgvEpisodes.SelectedRows.Count == 1)
                        {
                            cxtEpisodes.Show(MousePosition);
                        }
                    }
                    else
                    {
                        DoDownloadSelected();
                    }
                }
                else
                {
                    CancelDownload();
                }
            }
        }

        private void ResetContentGridViews()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    dgvContent.DataSource = null;
                    dgvSeasons.DataSource = null;
                    dgvEpisodes.DataSource = null;
                });
            }
            else
            {
                dgvContent.DataSource = null;
                dgvSeasons.DataSource = null;
                dgvEpisodes.DataSource = null;
            }
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (IsDownloadRunning && IsEngineRunning)
            {
                if (!IsDownloadPaused)
                {
                    engine.Pause();
                    SetResume();
                    SetProgressLabel(lblProgress.Text + " (Paused)");
                    IsDownloadPaused = true;
                }
                else
                {
                    engine.ResumeAsync();
                    SetPause();
                    IsDownloadPaused = false;
                }
            }
        }

        private void StartStreaming(PlexObject stream)
        {
            //so that cxtStreamOptions can access the current stream's information, a global object has to be used.
            CurrentStream = stream;
            if (settings.Player.PlaybackEngine == PlaybackMode.PVSPlayer)
            {
                PVSLauncher.LaunchPVS(stream, ReturnCorrectTable());
            }
            else if (settings.Player.PlaybackEngine == PlaybackMode.VLCPlayer)
            {
                VLCLauncher.LaunchVLC(stream.StreamInformation);
            }
            else if (settings.Player.PlaybackEngine == PlaybackMode.Browser)
            {
                BrowserLauncher.LaunchBrowser(stream.StreamInformation);
            }
            else if (settings.Player.PlaybackEngine == PlaybackMode.MenuSelector)
            {
                //display the options menu where the
                Point loc = new Point(this.Location.X + btnHTTPPlay.Location.X, this.Location.Y + btnHTTPPlay.Location.Y);
                cxtStreamOptions.Show(loc);
            }
            else
            {
                MessageBox.Show("Unrecognised Playback Mode (\"" + settings.Player.PlaybackEngine + "\")", "Playback Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AddToLog("Invalid Playback Mode: " + settings.Player.PlaybackEngine);
            }
        }

        private void BtnHTTPPlay_Click(object sender, EventArgs e)
        {
            if ((dgvContent.SelectedRows.Count == 1) || (dgvEpisodes.SelectedRows.Count == 1))
            {
                PlexObject result;
                if (!IsTVShow)
                {
                    result = (PlexMovie)PlexDL.WaitWindow.WaitWindow.Show(GetObjectFromSelectionWorker, "Getting Metadata");
                }
                else
                {
                    if (dgvEpisodes.SelectedRows.Count == 1)
                    {
                        result = (PlexTVShow)PlexDL.WaitWindow.WaitWindow.Show(GetTVObjectFromSelectionWorker, "Getting Metadata");
                    }
                    else
                    {
                        MessageBox.Show("No episode is selected", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                StartStreaming(result);
            }
        }

        private void BtnProfile_Click(object sender, EventArgs e)
        {
            cxtProfile.Show(MousePosition);
        }

        private void SearchProcedure()
        {
            if (IsFiltered)
            {
                ClearSearch();
            }
            else
            {
                RunTitleSearch();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchProcedure();
        }

        private void CxtEpisodeOptions_Opening(object sender, CancelEventArgs e)
        {
            if (dgvEpisodes.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void CxtContentOptions_Opening(object sender, CancelEventArgs e)
        {
            if (dgvContent.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void Metadata(PlexObject result = null)
        {
            if ((dgvContent.SelectedRows.Count == 1) || (dgvEpisodes.SelectedRows.Count == 1))
            {
                if (!IsDownloadRunning && !IsEngineRunning)
                {
                    if (result == null)
                    {
                        if (!IsTVShow)
                        {
                            result = (PlexObject)PlexDL.WaitWindow.WaitWindow.Show(GetObjectFromSelectionWorker, "Getting Metadata");
                        }
                        else
                        {
                            result = (PlexObject)PlexDL.WaitWindow.WaitWindow.Show(GetTVObjectFromSelectionWorker, "Getting Metadata");
                        }
                    }
                    using (Metadata frm = new Metadata())
                    {
                        frm.StreamingContent = result;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("You cannot view metadata while an internal download is running", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if ((dgvContent.Rows.Count == 0) && (dgvTVShows.Rows.Count == 0))
            {
                using (Metadata frm = new Metadata())
                {
                    frm.StationaryMode = true;
                    frm.ShowDialog();
                }
            }
        }

        private void BtnMetadata_Click(object sender, EventArgs e)
        {
            Metadata();
        }

        private void ShowLogViewer()
        {
            using (LogViewer frm = new LogViewer())
                frm.ShowDialog();
        }

        #endregion ButtonClicks

        private void LblViewFullLog_LinkClicked(object sender, EventArgs e)
        {
            ShowLogViewer();
        }

        private void BtnSetDlDir_Click(object sender, EventArgs e)
        {
            SetDownloadDirectory();
        }

        private void ManualSectionLoad()
        {
            if (dgvLibrary.Rows.Count > 0)
            {
                InputResult ipt = objUI.showInputForm("Enter section key", "Manual Section Lookup", true, "TV Library");
                if (ipt.txt == "!cancel=user")
                {
                    return;
                }
                else if (!int.TryParse(ipt.txt, out _))
                {
                    MessageBox.Show("Section key ust be numeric", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    UpdateFromLibraryKey(ipt.txt, ipt.chkd);
                }
            }
        }

        private void ItmDownloadThisEpisode_Click(object sender, EventArgs e)
        {
            cxtEpisodes.Close();
            DoDownloadSelected();
        }

        private void ItmDownloadAllEpisodes_Click(object sender, EventArgs e)
        {
            cxtEpisodes.Close();
            DoDownloadAllEpisodes();
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
            DoDownloadAllEpisodes();
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cxtContentOptions.Close();
            Metadata();
        }

        private void ItmContentDownload_Click(object sender, EventArgs e)
        {
            cxtContentOptions.Close();
            DoDownloadSelected();
        }

        private void LoadProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cxtProfile.Close();
            LoadProfile();
        }

        private void SaveProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cxtProfile.Close();
            SaveProfile();
        }

        private void NfyMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //send this form to the front of the screen
            this.TopMost = true;
            this.TopMost = false;
        }

        private void ItmStreamInPVS_Click(object sender, EventArgs e)
        {
            cxtStreamOptions.Close();
            PVSLauncher.LaunchPVS(CurrentStream, ReturnCorrectTable());
        }

        private void ItmStreamInVLC_Click(object sender, EventArgs e)
        {
            cxtStreamOptions.Close();
            VLCLauncher.LaunchVLC(CurrentStream.StreamInformation);
        }

        private void ItmStreamInBrowser_Click(object sender, EventArgs e)
        {
            cxtStreamOptions.Close();
            BrowserLauncher.LaunchBrowser(CurrentStream.StreamInformation);
        }
    }
}