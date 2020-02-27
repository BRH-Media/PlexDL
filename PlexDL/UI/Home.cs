using libbrhscgui.Components;
using MetroSet_UI.Extensions;
using MetroSet_UI.Forms;
using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Caching;
using PlexDL.Common.Logging;
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

        private readonly PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        public readonly UserInteraction objUI = new UserInteraction();
        public Timer t1 = new Timer();
        public User user = new User();
        public Server svr;

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

        public bool IsConnected = false;
        public bool InitialFill = true;
        public bool IsLibraryFilled = false;
        public bool IsFiltered = false;
        public bool IsTVShow = false;
        public bool IsContentSortingEnabled = true;
        public bool IsDownloadQueueCancelled = false;
        public bool IsDownloadRunning = false;
        public bool IsDownloadPaused = false;
        public bool IsEngineRunning = false;
        public bool IsMsgAlreadyShown = false;
        public bool doNotAttemptAgain = false;
        public bool DownloadAllEpisodes = false;

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

        private void fadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     //check if opacity is 0
            {
                t1.Stop();    //if it is, we stop the timer
                Close();   //and we try to close the form
            }
            else
                Opacity -= 0.05;
        }

        private void fadeIn(object sender, EventArgs e)
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
            string titleValue = "";
            if (table == null)
                table = returnCorrectTable();
            foreach (DataRow r in table.Rows)
            {
                if (dgv.Columns.Contains(key))
                {
                    titleValue = sel.Cells[key].Value.ToString();
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
            if ((dgvContent.SelectedRows.Count == 1) || (dgvTVShows.SelectedRows.Count == 1))
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
                addToLog("Content Parse Started");
                addToLog("Grabbing Titles");
                DataRow infRow;

                //MessageBox.Show(index.ToString());

                metadata = GetEpisodeMetadata(index);

                addToLog("Checking XML validity");
                if (Methods.PlexXmlValid(metadata))
                {
                    infRow = episodesTable.DefaultView[index].Row;

                    addToLog("XML Valid");

                    DownloadInfo dlInfo = getContentDownloadInfo_Xml(metadata);

                    addToLog("Assembling Object");

                    obj.ContentGenre = getContentGenre(metadata);
                    obj.StreamInformation = dlInfo;
                    obj.ContentDuration = dlInfo.ContentDuration;
                    obj.StreamPosterUri = getContentThumbnailUri(metadata);
                    obj.Season = getTVShowSeason(metadata);
                    obj.EpisodesInSeason = episodesTable.Rows.Count;
                    obj.TVShowName = getTVShowTitle(metadata);
                    obj.StreamResolution = getContentResolution(metadata);
                    obj.StreamIndex = index;
                }
                else
                    addToLog("XML Invalid");
            }
            return obj;
        }

        public int GetIndexFromPrimary(string primary)
        {
            DataTable table = returnCorrectTable();
            DataRow row = table.Rows.Find(primary);
            return table.Rows.IndexOf(row);
        }

        private PlexMovie GetObjectFromIndex(int index)
        {
            PlexMovie obj = new PlexMovie();
            XmlDocument metadata;
            if (dgvContent.SelectedRows.Count == 1)
            {
                string sel = dgvContent.SelectedRows[0].Cells[0].Value.ToString();
                //MessageBox.Show("Primary Key:"+returnCorrectTable().PrimaryKey[0].ColumnName);
                //MessageBox.Show("Proper:" + GetIndexFromPrimary(sel) + "\nActual:" + index);
                addToLog("Content Parse Started");
                addToLog("Grabbing Titles");
                DataRow infRow;

                metadata = GetContentMetadata(index);

                addToLog("Checking XML validity");
                if (Methods.PlexXmlValid(metadata))
                {
                    infRow = GetDataRowContent(index, false);

                    addToLog("XML Valid");

                    DownloadInfo dlInfo = getContentDownloadInfo_Xml(metadata);

                    addToLog("Assembling Object");

                    obj.Actors = getActorsFromMetadata(metadata);
                    obj.ContentGenre = getContentGenre(metadata);
                    obj.StreamInformation = dlInfo;
                    obj.ContentDuration = dlInfo.ContentDuration;
                    obj.StreamPosterUri = getContentThumbnailUri(metadata);

                    obj.StreamResolution = getContentResolution(metadata);
                    obj.StreamIndex = index;
                }
                else
                    addToLog("XML Invalid");
            }
            return obj;
        }

        #endregion PlexMovieBuilders

        #region MetadataGatherers

        public XmlDocument getTVShowMetadata(int index)
        {
            addToLog("Sorting existing data");

            DataRow result;

            result = GetDataRowSeries(index);

            string key = result["key"].ToString();
            string baseUri = getBaseUri(false);
            key = key.TrimStart('/');
            string uri = baseUri + key + "/?X-Plex-Token=";

            addToLog("Contacting server");
            XmlDocument reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        public DataRow GetDataRowContent(int index, bool directTable)
        {
            if (!directTable)
                return GetDataRowTbl(returnCorrectTable(), index);
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
            addToLog("Sorting existing data");

            DataRow result;

            result = GetDataRowContent(index, false);

            string key = result["key"].ToString();
            string baseUri = getBaseUri(false);
            key = key.TrimStart('/');
            string uri = baseUri + key + "/?X-Plex-Token=";

            addToLog("Contacting server");
            XmlDocument reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        public XmlDocument GetEpisodeMetadata(int index)
        {
            addToLog("Sorting existing data");

            DataRow result;

            result = GetDataRowEpisodes(index);

            string key = result["key"].ToString();
            string baseUri = getBaseUri(false);
            key = key.TrimStart('/');
            string uri = baseUri + key + "/?X-Plex-Token=";

            addToLog("Contacting server");
            XmlDocument reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        #endregion MetadataGatherers

        #region XMLGatherers

        public XmlDocument GetSeriesXml(int index)
        {
            addToLog("Sorting existing data");

            DataRow result;

            result = GetDataRowContent(index, true);

            string key = result["key"].ToString();
            string baseUri = getBaseUri(false);
            key = key.TrimStart('/');
            string uri = baseUri + key + "/?X-Plex-Token=";

            //MessageBox.Show(uri);

            addToLog("Contacting server");
            XmlDocument reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        public XmlDocument GetEpisodeXml(int index)
        {
            addToLog("Sorting existing data");

            DataRow result;
            result = GetDataRowSeries(index);

            string key = result["key"].ToString();
            string baseUri = getBaseUri(false);
            key = key.TrimStart('/');
            string uri = baseUri + key + "/?X-Plex-Token=";

            //MessageBox.Show(uri);

            addToLog("Contacting server");
            XmlDocument reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        #endregion XMLGatherers

        #region MetadataParsers

        private List<PlexActor> getActorsFromMetadata(XmlDocument metadata)
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

        private Resolution getContentResolution(XmlDocument metadata)
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

        private string getContentGenre(XmlDocument metadata)
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

        private string getTVShowSeason(XmlDocument metadata)
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

        private string getTVShowTitle(XmlDocument metadata)
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

        private string getContentThumbnailUri(XmlDocument metadata)
        {
            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            DataTable video = sections.Tables["Video"];
            DataRow row = video.Rows[0];
            string thumb = row["thumb"].ToString();
            string fullUri = "";
            if (!(thumb == ""))
            {
                string baseUri = getBaseUri(false).TrimEnd('/');
                fullUri = baseUri + thumb + "?X-Plex-Token=" + getToken();
            }
            return fullUri;
        }

        private DataTable returnCorrectTable(bool directTable = false)
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

        private string getToken()
        {
            int index = dgvServers.CurrentCell.RowIndex;
            Server s = plexServers[index];
            return s.accessToken;
        }

        private string getBaseUri(bool incToken)
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

        private int GetSelectedIndex()
        {
            return selectedIndex;
        }

        #endregion StringIntHelpers

        #region ProfileHelpers

        public void loadProfile()
        {
            if (!IsConnected)
            {
                if (ofdLoadProfile.ShowDialog() == DialogResult.OK)
                {
                    doLoadProfile(ofdLoadProfile.FileName);
                }
            }
            else
            {
                MessageBox.Show("You can't load profiles while you're connected; please disconnect first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void saveProfile()
        {
            if (settings.ConnectionInfo.PlexAccountToken != "")
            {
                if (sfdSaveProfile.ShowDialog() == DialogResult.OK)
                {
                    doSaveProfile(sfdSaveProfile.FileName);
                }
            }
            else
            {
                MessageBox.Show("You need to specify an account token before saving a profile", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void doSaveProfile(string fileName, bool silent = false)
        {
            try
            {
                var subReq = settings;
                XmlSerializer xsSubmit = new XmlSerializer(typeof(ApplicationOptions));
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                xmlSettings.Indent = true;
                xmlSettings.IndentChars = ("\t");
                xmlSettings.OmitXmlDeclaration = false;
                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww, xmlSettings))
                    {
                        xsSubmit.Serialize(sww, settings);

                        File.WriteAllText(fileName, sww.ToString());
                    }
                }

                if (!silent)
                {
                    MessageBox.Show("Successfully saved profile!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                addToLog("Saved profile " + fileName);
            }
            catch (Exception ex)
            {
                LoggingHelpers.recordException(ex.Message, "SaveProfileError");
                if (!silent)
                {
                    MessageBox.Show(ex.ToString(), "Error in saving XML Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        public void doLoadProfile(string fileName, bool silent = false)
        {
            try
            {
                ApplicationOptions subReq = null;

                XmlSerializer serializer = new XmlSerializer(typeof(ApplicationOptions));

                StreamReader reader = new StreamReader(fileName);
                subReq = (ApplicationOptions)serializer.Deserialize(reader);
                reader.Close();

                settings = subReq;

                if (!silent)
                {
                    MessageBox.Show("Successfully loaded profile!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                addToLog("Loaded profile " + fileName);
            }
            catch (Exception ex)
            {
                LoggingHelpers.recordException(ex.Message, "LoadProfileError");
                if (!silent)
                {
                    MessageBox.Show(ex.ToString(), "Error in loading XML Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        #endregion ProfileHelpers

        #region ConnectionHelpers

        private void Disconnect()
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
                SetProgressLabel("Disconnected");
                SetConnect();
                SelectMoviesTab();
                IsConnected = false;
            }
        }

        private void Connect()
        {
            try
            {
                using (Connect frm = new Connect())
                {
                    ConnectionInfo existingInfo = new ConnectionInfo();
                    existingInfo.PlexAccountToken = settings.ConnectionInfo.PlexAccountToken;
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
                                renderServersView(servers);
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
                LoggingHelpers.recordException(ex.Message, "ConnectionError");
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
                    renderServersView(servers);
                    doConnectFromSelectedServer();
                }
            }
        }

        private void doConnectFromSelectedServer()
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
                        PlexAccountToken = getToken(),
                        PlexAddress = address,
                        PlexPort = port,
                        RelaysOnly = settings.ConnectionInfo.RelaysOnly
                    };

                    settings.ConnectionInfo = connectInfo;

                    string uri = getBaseUri(true);
                    //MessageBox.Show(uri);
                    XmlDocument reply = (XmlDocument)PlexDL.WaitWindow.WaitWindow.Show(XmlGet.GetXMLTransactionWorker, "Connecting", new object[] { uri, false, true });
                    if (Methods.PlexXmlValid(reply))
                    {
                        IsConnected = true;
                        if (settings.Generic.ShowConnectionSuccess)
                        {
                            MessageBox.Show("Connection successful!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        SetProgressLabel("Connected");
                        SetDisconnect();
                        if (reply.ChildNodes.Count != 0)
                        {
                            populateLibrary(reply);
                        }
                        doNotAttemptAgain = true;
                    }
                    else
                    {
                        MessageBox.Show("Connection failed. Check that '" + s.name + "' exists, that you have the right address, that it is accessible from your network, and that you have permission to access it.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private void populateLibraryWorker(XmlDocument doc)
        {
            if (doc != null)
            {
                try
                {
                    DGVServersEnabled(false);
                    addToLog("Library population requested");
                    string libraryDir = KeyGatherers.getLibraryKey(doc).TrimEnd('/');
                    string baseUri = getBaseUri(false);
                    string uriSectionKey = baseUri + libraryDir + "/?X-Plex-Token=";
                    System.Xml.XmlDocument xmlSectionKey = XmlGet.GetXMLTransaction(uriSectionKey);

                    string sectionDir = KeyGatherers.getSectionKey(xmlSectionKey).TrimEnd('/');
                    string uriSections = baseUri + libraryDir + "/" + sectionDir + "/?X-Plex-Token=";
                    System.Xml.XmlDocument xmlSections = XmlGet.GetXMLTransaction(uriSections);

                    addToLog("Creating new datasets");
                    DataSet sections = new DataSet();
                    sections.ReadXml(new XmlNodeReader(xmlSections));

                    DataTable sectionsTable;
                    sectionsTable = sections.Tables["Directory"];
                    gSectionsTable = sectionsTable;

                    addToLog("Binding to grid");
                    renderLibraryView(sectionsTable);
                    IsLibraryFilled = true;
                    uri = baseUri + libraryDir + "/" + sectionDir + "/";
                    //we can render the content view if a row is already selected
                    DGVServersEnabled(true);
                }
                catch (WebException ex)
                {
                    LoggingHelpers.recordException(ex.Message, "LibPopError");
                    DGVServersEnabled(true);
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        var response = ex.Response as HttpWebResponse;
                        if (response != null)
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
                    LoggingHelpers.recordException(ex.Message, "LibPopError");
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

        private void updateContentViewWorker(XmlDocument doc, bool isTVShow)
        {
            DGVLibraryEnabled(false);

            addToLog("Updating library contents");

            GetTitlesTable(doc, isTVShow);

            IsTVShow = isTVShow;

            if (IsTVShow)
            {
                addToLog("Rendering TV Shows");
                renderTVView(titlesTable);
            }
            else
            {
                addToLog("Rendering Movies");
                renderContentView(titlesTable);
            }
            contentXmlDoc = doc;

            DGVLibraryEnabled(true);

            //MessageBox.Show("ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + titlesTable.Rows.Count.ToString());
        }

        private void updateEpisodeViewWorker(XmlDocument doc)
        {
            DGVSeasonsEnabled(false);
            addToLog("Updating episode contents");

            addToLog("Creating datasets");
            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            episodesTable = sections.Tables["Video"];

            addToLog("Cleaning unwanted data");

            addToLog("Binding to grid");
            renderEpisodesView(episodesTable);

            contentXmlDoc = doc;

            DGVSeasonsEnabled(true);

            //MessageBox.Show("ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + titlesTable.Rows.Count.ToString());
        }

        private void updateSeriesViewWorker(XmlDocument doc)
        {
            DGVContentEnabled(false);
            addToLog("Updating series contents");

            addToLog("Creating datasets");
            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            seriesTable = sections.Tables["Directory"];

            addToLog("Cleaning unwanted data");

            addToLog("Binding to grid");
            renderSeriesView(seriesTable);

            contentXmlDoc = doc;

            DGVContentEnabled(true);

            //MessageBox.Show("ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + titlesTable.Rows.Count.ToString());
        }

        #endregion UpdateWorkers

        #region BackgroundWorkers

        private void wkrGetMetadata_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!System.IO.Directory.Exists(settings.Generic.DownloadDirectory))
                System.IO.Directory.CreateDirectory(settings.Generic.DownloadDirectory);
            if (!System.IO.Directory.Exists(settings.Generic.DownloadDirectory + @"\TV"))
                System.IO.Directory.CreateDirectory(settings.Generic.DownloadDirectory + @"\TV");
            if (!System.IO.Directory.Exists(settings.Generic.DownloadDirectory + @"\Movies"))
                System.IO.Directory.CreateDirectory(settings.Generic.DownloadDirectory + @"\Movies");
            if (IsTVShow)
            {
                if (DownloadAllEpisodes)
                {
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
                this.BeginInvoke((MethodInvoker)delegate
                {
                    lblProgress.Text = "Getting Metadata";
                });
                PlexMovie movie = GetObjectFromSelection();
                DownloadInfo dlInfo = movie.StreamInformation;
                dlInfo.DownloadPath = settings.Generic.DownloadDirectory + @"\Movies";
                queue.Add(dlInfo);
            }
            this.BeginInvoke((MethodInvoker)delegate
            {
                StartDownload(queue, settings.Generic.DownloadDirectory);
            });
        }

        #endregion BackgroundWorkers

        #region UpdateCallbackWorkers

        private void WorkerUpdateContentView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            XmlDocument doc = (XmlDocument)e.Arguments[0];
            updateContentViewWorker(doc, (bool)e.Arguments[1]);
        }

        private void WorkerRenderContentView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            DataTable t = (DataTable)e.Arguments[0];
            renderContentView(t);
        }

        private void WorkerRenderTVView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            DataTable t = (DataTable)e.Arguments[0];
            renderTVView(t);
        }

        private void WorkerUpdateLibraryView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            XmlDocument doc = (XmlDocument)e.Arguments[0];
            populateLibraryWorker(doc);
        }

        private void WorkerUpdateSeriesView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            XmlDocument doc = (XmlDocument)e.Arguments[0];
            updateSeriesViewWorker(doc);
        }

        private void WorkerUpdateEpisodesView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            XmlDocument doc = (XmlDocument)e.Arguments[0];
            updateEpisodeViewWorker(doc);
        }

        private void WorkerUpdateServersView(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            List<Server> servers = (List<Server>)e.Arguments[0];
            renderServersViewWorker(servers);
        }

        #endregion UpdateCallbackWorkers

        #region ContentRenderers

        private void renderContentView(DataTable content)
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

        private void renderTVView(DataTable content)
        {
            if (!(content == null))
            {
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
        }

        private void renderSeriesView(DataTable content)
        {
            if (!(content == null))
            {
                List<string> wantedColumns = settings.DataDisplay.SeriesView.SeriesDisplayColumns;
                List<string> wantedCaption = settings.DataDisplay.SeriesView.SeriesDisplayCaption;

                RenderStruct info = new RenderStruct() { Data = content, WantedColumns = wantedColumns, WantedCaption = wantedCaption };

                GenericRenderer.RenderView(info, dgvSeasons);
            }
        }

        private void renderEpisodesView(DataTable content)
        {
            if (!(content == null))
            {
                List<string> wantedColumns = settings.DataDisplay.EpisodesView.EpisodesDisplayColumns;
                List<string> wantedCaption = settings.DataDisplay.EpisodesView.EpisodesDisplayCaption;

                RenderStruct info = new RenderStruct() { Data = content, WantedColumns = wantedColumns, WantedCaption = wantedCaption };

                GenericRenderer.RenderView(info, dgvEpisodes);
            }
        }

        private void renderServersView(List<Server> servers)
        {
            PlexDL.WaitWindow.WaitWindow.Show(this.WorkerUpdateServersView, "Rendering Servers", new object[] { servers });
        }

        private void renderServersViewWorker(List<Server> servers)
        {
            ServerViewRenderer.RenderView(servers, dgvServers);
        }

        private void dgvLibrary_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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

        private void renderLibraryView(DataTable content)
        {
            if (!(content == null))
            {
                List<string> wantedColumns = settings.DataDisplay.LibraryView.LibraryDisplayColumns;
                List<string> wantedCaption = settings.DataDisplay.LibraryView.LibraryDisplayCaption;

                RenderStruct info = new RenderStruct() { Data = content, WantedColumns = wantedColumns, WantedCaption = wantedCaption };

                GenericRenderer.RenderView(info, dgvLibrary);
            }
        }

        #endregion ContentRenderers

        #region UpdateWaitWorkers

        private void updateContentView(XmlDocument content, bool IsTVShow)
        {
            PlexDL.WaitWindow.WaitWindow.Show(this.WorkerUpdateContentView, "Updating Content", new object[] { content, IsTVShow });
        }

        private void updateSeriesView(XmlDocument content)
        {
            PlexDL.WaitWindow.WaitWindow.Show(this.WorkerUpdateSeriesView, "Updating Series", new object[] { content });
        }

        private void updateEpisodeView(XmlDocument content)
        {
            PlexDL.WaitWindow.WaitWindow.Show(this.WorkerUpdateEpisodesView, "Updating Episodes", new object[] { content });
        }

        private void populateLibrary(XmlDocument content)
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
            bool directMatch = (bool)e.Arguments[1];
            string searchKey = (string)e.Arguments[0];
            DataRow[] rowCollec;
            if (directMatch)
            {
                rowCollec = titlesTable.Select(column + " = '" + searchKey + "'");
            }
            else
            {
                rowCollec = titlesTable.Select(column + " LIKE '%" + searchKey + "%'");
            }
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

        private void addToLog(string logEntry)
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
                LoggingHelpers.logDelWriter("PlexDL.logdel", headers, logEntryToAdd);
        }

        private void DGVDataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            DataGridView parent = (DataGridView)sender;
            //don't show the event to the user; but log it.
            addToLog("Experienced data error in " + parent.Name);
            e.Cancel = true;
        }

        #endregion Logging

        #region Download

        #region DownloadInfoGatherers

        private DownloadInfo getContentDownloadInfo_Index(int index)
        {
            XmlDocument reply;

            if (IsTVShow)
            {
                reply = GetEpisodeMetadata(index);
            }
            else
            {
                reply = GetContentMetadata(index);
            }

            return getContentDownloadInfo_Xml(reply);
        }

        private DownloadInfo getContentDownloadInfo_Xml(XmlDocument xml)
        {
            if (Methods.PlexXmlValid(xml))
            {
                DownloadInfo obj = new DownloadInfo();

                addToLog("Creating new datasets");
                DataSet sections = new DataSet();
                sections.ReadXml(new XmlNodeReader(xml));

                DataTable part = sections.Tables["Part"];
                DataRow video = sections.Tables["Video"].Rows[0];
                string title = video["title"].ToString();
                if (title.Length > settings.Generic.DefaultStringLength)
                    title = title.Substring(0, settings.Generic.DefaultStringLength);
                string thumb = video["thumb"].ToString();
                string thumbnailFullUri = "";
                if (!(thumb == ""))
                {
                    string baseUri = getBaseUri(false).TrimEnd('/');
                    thumbnailFullUri = baseUri + thumb + "?X-Plex-Token=" + getToken();
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
                string fileName = Common.Methods.removeIllegalCharacters(title + "." + container);

                string link = getBaseUri(false).TrimEnd('/') + filePart + "/?X-Plex-Token=" + getToken();
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
                addToLog("Download Cancelled");
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

        private int CheckWebFile(string uri)
        {
            addToLog("Checking web file validity");
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                return (int)response.StatusCode;
            }
            catch (WebException ex)
            {
                LoggingHelpers.recordException(ex.Message, "CheckWebError");
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var errResponse = ex.Response as HttpWebResponse;
                    if (errResponse != null)
                    {
                        return (int)errResponse.StatusCode;
                    }
                    else
                    {
                        return 400;
                    }
                }
            }
            finally
            {
                // Don't forget to close your response.
                if (response != null)
                {
                    response.Close();
                }
            }

            return 200;
        }

        private void StartDownloadEngine()
        {
            engine.QueueProgressChanged += Engine_DownloadProgressChanged;
            engine.QueueCompleted += Engine_DownloadCompleted;

            engine.StartAsync();
            //MessageBox.Show("Started!");
            addToLog("Download is Progressing");
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
                addToLog("Download directory updated to " + settings.Generic.DownloadDirectory);
            }
        }

        private void SetDownloadCompleted()
        {
            pbMain.Value = pbMain.Maximum;
            btnPause.Enabled = false;
            SetResume();
            SetDownloadStart();
            SetProgressLabel("Download Completed");
            addToLog("Download completed");
            engine.Clear();
            IsDownloadRunning = false;
            IsDownloadPaused = false;
            IsEngineRunning = false;
        }

        private void StartDownload(List<DownloadInfo> queue, string location)
        {
            addToLog("Download Process Started");
            pbMain.Value = 0;

            addToLog("Starting HTTP Engine");
            engine = new AltoHttp.DownloadQueue();
            if (queue.Count > 1)
            {
                foreach (DownloadInfo dl in queue)
                {
                    string fqPath = dl.DownloadPath + @"\" + dl.FileName;
                    if (File.Exists(fqPath))
                    {
                        addToLog(dl.FileName + " already exists; will not download.");
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
                    addToLog(dl.FileName + " already exists; get user confirmation.");
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

        private void Engine_DownloadCompleted(object sender, EventArgs e)
        {
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
            EnableContentSorting();
            if (IsTVShow)
                renderTVView(titlesTable);
            else
                renderContentView(titlesTable);
            filteredTable = null;
            IsFiltered = false;
            SetStartSearch();
        }

        private void DisableContentSorting()
        {
            IsContentSortingEnabled = false;
            Methods.SortingEnabled(dgvContent, IsContentSortingEnabled);
        }

        private void EnableContentSorting()
        {
            IsContentSortingEnabled = true;
            Methods.SortingEnabled(dgvContent, IsContentSortingEnabled);
        }

        private void runTitleSearch()
        {
            try
            {
                addToLog("Title search requested");
                if ((dgvContent.Rows.Count > 0) || (dgvTVShows.Rows.Count > 0))
                {
                    SearchOptions start;
                    if (IsTVShow)
                        start = new SearchOptions() { SearchCollection = tvViewTable };
                    else
                        start = new SearchOptions() { SearchCollection = contentViewTable };
                    SearchOptions result = SearchForm.ShowSearch(start);
                    if ((result.SearchTerm == "") || (result.SearchColumn == ""))
                    {
                        return;
                    }
                    else
                    {
                        filteredTable = null;
                        GetFilteredTable(result.SearchTerm, result.SearchColumn, result.SearchDirect, false);
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
                }
                else
                {
                    addToLog("No data to search");
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.recordException(ex.Message, "SearchError");
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void GetFilteredTable(string query, string column, bool isTVShow, bool silent = true)
        {
            DataTable tblFiltered;
            DataRow[] rowCollec = (DataRow[])PlexDL.WaitWindow.WaitWindow.Show(GetSearchEnum, "Filtering Records", new object[] { query, isTVShow, column });
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

        private void DGVEpisodesEnabled(bool enabled)
        {
            if (dgvEpisodes.InvokeRequired)
            {
                dgvEpisodes.BeginInvoke((MethodInvoker)delegate
                {
                    dgvEpisodes.Enabled = enabled;
                });
            }
            else
            {
                dgvEpisodes.Enabled = enabled;
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
            if (keyData == (Keys.Control | Keys.F))
            {
                SearchProcedure();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.O))
            {
                loadProfile();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.S))
            {
                saveProfile();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SetProgressLabel(string status)
        {
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

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
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
                        addToLog("PlexDL Exited");
                        e.Cancel = false;
                    }
                }
            }
            else
            {
                addToLog("PlexDL Exited");

                if (settings.Generic.AnimationSpeed > 0)
                {
                    t1 = new Timer();
                    t1.Interval = settings.Generic.AnimationSpeed;
                    t1.Tick += new EventHandler(fadeOut);  //this calls the fade out function
                    t1.Start();

                    if (Opacity == 0)
                    {
                        //resume the event - the program can be closed
                        e.Cancel = false;
                    }
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (settings.Generic.AnimationSpeed > 0)
            {
                Opacity = 0;      //first the opacity is 0

                t1.Interval = settings.Generic.AnimationSpeed;  //we'll increase the opacity every 10ms
                t1.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity
                t1.Start();
            }
            try
            {
                string curUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                settings.Generic.DownloadDirectory = curUser + "\\Videos\\PlexDL";

                if (!(System.IO.Directory.Exists(settings.Generic.DownloadDirectory)))
                {
                    System.IO.Directory.CreateDirectory(settings.Generic.DownloadDirectory);
                }

                addToLog("PlexDL Started");
            }
            catch (Exception ex)
            {
                LoggingHelpers.recordException(ex.Message, "StartupError");
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
                addToLog("Requesting ibrary contents");
                string contentUri = uri + key + "/all/?X-Plex-Token=";
                XmlDocument contentXml = XmlGet.GetXMLTransaction(contentUri);

                contentXmlDoc = contentXml;
                updateContentView(contentXml, isTVShow);
            }
            catch (WebException ex)
            {
                LoggingHelpers.recordException(ex.Message, "UpdateLibraryError");
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
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
                LoggingHelpers.recordException(ex.Message, "UpdateLibraryError");
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cxtLibrarySections_Opening(object sender, CancelEventArgs e)
        {
            if (dgvLibrary.Rows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        #endregion FormEvents

        #region DGVRowChanges

        private void dgvLibrary_OnRowChange(object sender, EventArgs e)
        {
            if ((dgvLibrary.SelectedRows.Count == 1) && (IsLibraryFilled))
            {
                addToLog("Selection Changed");
                int index = GetTableIndexFromDGV(dgvLibrary, gSectionsTable);
                DataRow r = GetDataRowLibrary(index);

                string key = r["key"].ToString();
                string type = r["type"].ToString();
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

        private void dgvSeasons_OnRowChange(object sender, EventArgs e)
        {
            if (dgvSeasons.SelectedRows.Count == 1)
            {
                int index = GetTableIndexFromDGV(dgvSeasons, seriesTable);
                XmlDocument episodes = GetEpisodeXml(index);
                updateEpisodeView(episodes);
            }
        }

        private void dgvContent_OnRowChange(object sender, EventArgs e)
        {
            if (dgvContent.SelectedRows.Count == 1)
            {
                int index = 0;
                if (IsFiltered)
                {
                    index = GetTableIndexFromDGV(dgvContent, filteredTable);
                }
                else
                {
                    index = GetTableIndexFromDGV(dgvContent, titlesTable);
                }
            }
        }

        private void dgvTVShows_OnRowChange(object sender, EventArgs e)
        {
            if (dgvTVShows.SelectedRows.Count == 1)
            {
                int index = 0;
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
                    updateSeriesView(series);
                }
            }
        }

        private void dgvServers_OnRowChange(object sender, EventArgs e)
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
                    doConnectFromSelectedServer();
                }
            }
        }

        #endregion DGVRowChanges

        #region ButtonClicks

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                //deprecated (planned reintroduction)
                string uri = getBaseUri(true);
                XmlDocument reply = (XmlDocument)PlexDL.WaitWindow.WaitWindow.Show(XmlGet.GetXMLTransactionWorker, "Connecting", new object[] { uri });
                MessageBox.Show("Connection successful!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (WebException ex)
            {
                LoggingHelpers.recordException(ex.Message, "TestConnectionError");
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
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
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }

        private void btnConnect_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Disconnected from server", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (WebException ex)
            {
                LoggingHelpers.recordException(ex.Message, "ConnectionError");
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
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
            if (!IsDownloadRunning && !IsEngineRunning)
            {
                SetProgressLabel("Waiting");
                DownloadAllEpisodes = true;
                DownloadTotal = episodesTable.Rows.Count;
                IsDownloadRunning = true;
                wkrGetMetadata.RunWorkerAsync();
                SetDownloadCancel();
            }
        }

        private void DoDownloadSelected()
        {
            if (!IsDownloadRunning && !IsEngineRunning)
            {
                SetProgressLabel("Waiting");
                DownloadAllEpisodes = false;
                DownloadTotal = 1;
                IsDownloadRunning = true;
                wkrGetMetadata.RunWorkerAsync();
                SetDownloadCancel();
            }
        }

        private string FetchCorrectContentColumnName(string ViewName)
        {
            string vnLower = ViewName.ToLower();

            if (vnLower == "rating")
                return "contentRating";
            else
                return vnLower;
        }

        private string FetchCorrectSortedColumnName()
        {
            return FetchCorrectContentColumnName(dgvContent.SortedColumn.Name);
        }

        private void dgvContent_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*
            if (IsContentSortingEnabled)
            {
                string name = dgvContent.SortedColumn.Name;
                //MessageBox.Show(name);
                if (!IsFiltered)
                {
                    if (dgvContent.SortOrder == SortOrder.Descending)
                    {
                        titlesTable.DefaultView.Sort = name + " DESC";
                    }
                    else if (dgvContent.SortOrder == SortOrder.Ascending)
                    {
                        titlesTable.DefaultView.Sort = name + " ASC";
                    }
                    else
                    {
                        titlesTable.DefaultView.Sort = "";
                    }
                    titlesTable = titlesTable.DefaultView.ToTable();
                    //MessageBox.Show("Titles Table:" + titlesTable.Rows.Count + "\nGridView:" + dgvContent.Rows.Count);
                }
                else
                {
                    if (dgvContent.SortOrder == SortOrder.Descending)
                    {
                        filteredTable.DefaultView.Sort = name + " DESC";
                    }
                    else if (dgvContent.SortOrder == SortOrder.Ascending)
                    {
                        filteredTable.DefaultView.Sort = name + " ASC";
                    }
                    else
                    {
                        filteredTable.DefaultView.Sort = "";
                    }
                    filteredTable = filteredTable.DefaultView.ToTable();
                    //MessageBox.Show("Filtered Table:" + filteredTable.Rows.Count + "\nGridView:" + dgvContent.Rows.Count);
                }
            }
            */
        }

        private void dgvEpisodes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*
            if (dgvEpisodes.SortOrder.ToString() == "Descending")
            {
                episodesTable.DefaultView.Sort = dgvEpisodes.SortedColumn.Name + " DESC";
            }
            else
            {
                episodesTable.DefaultView.Sort = dgvEpisodes.SortedColumn.Name + " ASC";
            }
            episodesTable = episodesTable.DefaultView.ToTable();
            */
        }

        private void dgvSeasons_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*
            if (dgvSeasons.SortOrder.ToString() == "Descending")
            {
                seriesTable.DefaultView.Sort = dgvSeasons.SortedColumn.Name + " DESC";
            }
            else
            {
                seriesTable.DefaultView.Sort = dgvSeasons.SortedColumn.Name + " ASC";
            }
            seriesTable = seriesTable.DefaultView.ToTable();
            */
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if ((dgvContent.SelectedRows.Count == 1) || (dgvTVShows.SelectedRows.Count == 1))
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

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (IsDownloadRunning && IsEngineRunning)
            {
                if (!IsDownloadPaused)
                {
                    engine.Pause();
                    SetResume();
                    lblProgress.Text += " (Paused)";
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
            if (settings.Player.PlaybackEngine == PlaybackMode.PVSPlayer)
            {
                if (!IsDownloadRunning)
                {
                    Player frm = new Player();
                    frm.StreamingContent = stream;
                    frm.TitlesTable = returnCorrectTable();
                    addToLog("Started streaming " + stream.StreamInformation.ContentTitle);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("You cannot stream " + stream.StreamInformation.ContentTitle + " because a download is already running. Cancel the download before attempting to stream within PlexDL.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (settings.Player.PlaybackEngine == PlaybackMode.VLCPlayer)
            {
                VLCLauncher.LaunchVLC(stream.StreamInformation);
            }
            else if (settings.Player.PlaybackEngine == PlaybackMode.Browser)
            {
                BrowserLauncher.LaunchBrowser(stream.StreamInformation);
            }
        }

        private void btnHTTPPlay_Click(object sender, EventArgs e)
        {
            if ((dgvContent.SelectedRows.Count == 1) || (dgvTVShows.SelectedRows.Count == 1))
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

        private void btnProfile_Click(object sender, EventArgs e)
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
                runTitleSearch();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchProcedure();
        }

        private void cxtEpisodeOptions_Opening(object sender, CancelEventArgs e)
        {
            if (dgvEpisodes.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void cxtContentOptions_Opening(object sender, CancelEventArgs e)
        {
            if (dgvContent.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void Metadata(PlexObject result = null)
        {
            if ((dgvContent.SelectedRows.Count == 1) || (dgvTVShows.SelectedRows.Count == 1))
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

        private void btnMetadata_Click(object sender, EventArgs e)
        {
            Metadata();
        }

        #endregion ButtonClicks

        private void lblViewFullLog_LinkClicked(object sender, EventArgs e)
        {
            using (LogViewer frm = new LogViewer())
                frm.ShowDialog();
        }

        private void btnSetDlDir_Click(object sender, EventArgs e)
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
                else if (!int.TryParse(ipt.txt, out int r))
                {
                    MessageBox.Show("Section key ust be numeric", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    UpdateFromLibraryKey(ipt.txt, ipt.chkd);
                }
            }
        }

        private void itmDownloadThisEpisode_Click(object sender, EventArgs e)
        {
            cxtEpisodes.Close();
            DoDownloadSelected();
        }

        private void itmDownloadAllEpisodes_Click(object sender, EventArgs e)
        {
            cxtEpisodes.Close();
            DoDownloadAllEpisodes();
        }

        private void itmManuallyLoadSection_Click(object sender, EventArgs e)
        {
            cxtLibrarySections.Close();
            ManualSectionLoad();
        }

        private void itmEpisodeMetadata_Click(object sender, EventArgs e)
        {
            cxtEpisodeOptions.Close();
            Metadata();
        }

        private void itmDGVDownloadThisEpisode_Click(object sender, EventArgs e)
        {
            cxtEpisodeOptions.Close();
            DoDownloadSelected();
        }

        private void itmDGVDownloadThisSeason_Click(object sender, EventArgs e)
        {
            cxtEpisodeOptions.Close();
            DoDownloadAllEpisodes();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cxtContentOptions.Close();
            Metadata();
        }

        private void itmContentDownload_Click(object sender, EventArgs e)
        {
            cxtContentOptions.Close();
            DoDownloadSelected();
        }

        private void loadProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cxtProfile.Close();
            loadProfile();
        }

        private void saveProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cxtProfile.Close();
            saveProfile();
        }
    }

    public static class DataTableExtensions
    {
        public static void SetColumnsOrder(this DataTable table, params String[] columnNames)
        {
            int columnIndex = 0;
            foreach (var columnName in columnNames)
            {
                table.Columns[columnName].SetOrdinal(columnIndex);
                columnIndex++;
            }
        }
    }
}