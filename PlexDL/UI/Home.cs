using libbrhscgui.Components;
using MetroSet_UI.Extensions;
using MetroSet_UI.Forms;
using PlexAPI;
using PlexDL.Common;
using PlexDL.Common.Caching;
using PlexDL.Common.Structures;
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
using System.Reflection;
using System.Runtime.InteropServices;
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

        public bool connected = false;
        public bool InitialFill = true;
        public bool libraryFilled = false;
        public bool IsFiltered = false;
        public bool IsTVShow = false;
        public bool IsContentSortingEnabled = true;
        public bool DownloadQueueCancelled = false;
        public bool DownloadAllEpisodes = false;
        public bool downloadIsRunning = false;
        public bool downloadIsPaused = false;
        public bool engineIsRunning = false;
        public bool msgAlreadyShown = false;
        public bool doNotAttemptAgain = false;

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
        public static AppOptions settings = new AppOptions();
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

        public DataView contentView = null;
        public DataView episodesView = null;
        public DataView seasonsView = null;
        public DataView sectionsView = null;
        public DataView tvView = null;

        #endregion GlobalDataTableVariables

        #region GlobalListVariables

        public List<Server> plexServers = null;

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

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pvd, [In] ref uint pcFonts);

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

                //MetroSetMessageBox.Show(this, index.ToString());

                metadata = GetEpisodeMetadata(index);

                infRow = episodesTable.DefaultView[index].Row;

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
                //MetroSetMessageBox.Show(this, "Primary Key:"+returnCorrectTable().PrimaryKey[0].ColumnName);
                //MetroSetMessageBox.Show(this, "Proper:" + GetIndexFromPrimary(sel) + "\nActual:" + index);
                addToLog("Content Parse Started");
                addToLog("Grabbing Titles");
                DataRow infRow;

                if (IsTVShow)
                    metadata = GetEpisodeMetadata(index);
                else
                    metadata = GetContentMetadata(index);

                infRow = GetDataRowContent(index, false);

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
            return obj;
        }

        #endregion PlexMovieBuilders

        #region KeyGatherers

        private string getLibraryKey(System.Xml.XmlDocument doc)
        {
            string key = "";

            using (XmlReader reader = new XmlNodeReader(doc))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag
                        switch (reader.Name.ToString())
                        {
                            case "Directory":
                                if (reader.GetAttribute("title") == "library")
                                {
                                    string localKey = reader.GetAttribute("key");
                                    key = localKey;
                                }
                                break;
                        }
                    }
                }
                return key;
            }
        }

        private string getSectionKey(System.Xml.XmlDocument doc)
        {
            string key = "";

            addToLog("Parsing XML Reply");
            using (XmlReader reader = new XmlNodeReader(doc))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        addToLog("Checking for directories");
                        //return only when you have START tag
                        switch (reader.Name.ToString())
                        {
                            case "Directory":
                                if (reader.GetAttribute("title") == "Library Sections")
                                {
                                    string localKey = reader.GetAttribute("key");
                                    key = localKey;
                                    addToLog("Found " + key);
                                }
                                break;
                        }
                    }
                }
                return key;
            }
        }

        #endregion KeyGatherers

        #region GetXMLTransaction

        public XmlDocument GetXMLTransaction(string uri, string secret = "", bool forceNoCache = false)
        {
            //Create the cache folder structure
            Helpers.CacheStructureBuilder();

            if (XMLCaching.XMLInCache(uri) && !forceNoCache)
            {
                XmlDocument XMLResponse = XMLCaching.XMLFromCache(uri);
                if (XMLResponse != null)
                {
                    return XMLResponse;
                }
                else
                {
                    return GetXMLTransaction(uri, "", true);
                }
            }
            else
            {
                //Declare XMLResponse document
                XmlDocument XMLResponse = null;
                //Declare an HTTP-specific implementation of the WebRequest class.
                HttpWebRequest objHttpWebRequest;
                //Declare an HTTP-specific implementation of the WebResponse class
                HttpWebResponse objHttpWebResponse = null;
                //Declare a generic view of a sequence of bytes
                Stream objResponseStream = null;
                //Declare XMLReader
                XmlTextReader objXMLReader;

                string secret2;
                if (secret == "")
                {
                    secret2 = MatchUriToToken(uri);
                }
                else
                {
                    secret2 = secret;
                }
                if (secret2 == "")
                {
                    secret2 = settings.ConnectionInfo.PlexAccountToken;
                }
                string fullUri = uri + secret2;

                //6MetroSetMessageBox.Show(this, fullUri);

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                //Creates an HttpWebRequest for the specified URL.
                objHttpWebRequest = (HttpWebRequest)WebRequest.Create(fullUri);
                //---------- Start HttpRequest
                try
                {
                    //Set HttpWebRequest properties
                    objHttpWebRequest.Method = "GET";
                    objHttpWebRequest.KeepAlive = false;
                    //---------- End HttpRequest
                    //Sends the HttpWebRequest, and waits for a response.
                    objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();
                    //---------- Start HttpResponse, Return code 200
                    if (objHttpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        doNotAttemptAgain = false;
                        //Get response stream
                        objResponseStream = objHttpWebResponse.GetResponseStream();
                        //Load response stream into XMLReader
                        objXMLReader = new XmlTextReader(objResponseStream);
                        //Declare XMLDocument
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.Load(objXMLReader);
                        //Set XMLResponse object returned from XMLReader
                        XMLResponse = xmldoc;
                        //Close XMLReader
                        objXMLReader.Close();
                    }
                    else if (objHttpWebResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        MetroSetMessageBox.Show(this, "The web server denied access to the resource. Check your token and try again.");
                    }
                    //Close Steam
                    objResponseStream.Close();
                    //Close HttpWebResponse
                    objHttpWebResponse.Close();

                    recordTransaction(fullUri, ((int)objHttpWebResponse.StatusCode).ToString());
                }
                catch (Exception ex)
                {
                    recordException(ex.Message, "XMLTransactionError");
                    recordTransaction(fullUri, "Undetermined");
                    MetroSetMessageBox.Show(this, "Error Occurred in XML Transaction\n\n" + ex.ToString(), "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    doNotAttemptAgain = true;
                    return new XmlDocument();
                }
                finally
                {
                    //Release objects
                    objXMLReader = null;
                    objResponseStream = null;
                    objHttpWebResponse = null;
                    objHttpWebRequest = null;
                }
                XMLCaching.XMLToCache(XMLResponse, uri);
                return XMLResponse;
            }
        }

        #endregion GetXMLTransaction

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
            XmlDocument reply = GetXMLTransaction(uri);
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
            XmlDocument reply = GetXMLTransaction(uri);
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
            XmlDocument reply = GetXMLTransaction(uri);
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

            //MetroSetMessageBox.Show(this, uri);

            addToLog("Contacting server");
            XmlDocument reply = GetXMLTransaction(uri);
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

            //MetroSetMessageBox.Show(this, uri);

            addToLog("Contacting server");
            XmlDocument reply = GetXMLTransaction(uri);
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

        private string MatchUriToToken(string uri)
        {
            foreach (Server s in plexServers)
            {
                string serverUri = "http://" + s.address + ":" + s.port + "/";
                if (uri.Contains(serverUri))
                {
                    return s.accessToken;
                }
            }
            return "";
        }

        public string GetFileExtensionFromUrl(string url)
        {
            url = url.Split('?')[0];
            url = url.Split('/').Last();
            string final = url.Contains('.') ? url.Substring(url.LastIndexOf('.')) : "";
            addToLog("Extension parse: " + final);
            return final;
        }

        private int GetSelectedIndex()
        {
            return selectedIndex;
        }

        #endregion StringIntHelpers

        #region ProfileHelpers

        public void loadProfile()
        {
            if (ofdLoadProfile.ShowDialog() == DialogResult.OK)
            {
                doLoadProfile(ofdLoadProfile.FileName);
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
                MetroSetMessageBox.Show(this, "You need to specify an account token before saving a profile", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void doSaveProfile(string fileName, bool silent = false)
        {
            try
            {
                var subReq = settings;
                XmlSerializer xsSubmit = new XmlSerializer(typeof(AppOptions));
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
                    MetroSetMessageBox.Show(this, "Successfully saved profile!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                addToLog("Saved profile " + fileName);
            }
            catch (Exception ex)
            {
                recordException(ex.Message, "SaveProfileError");
                if (!silent)
                {
                    MetroSetMessageBox.Show(this, ex.ToString(), "Error in saving XML Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        public void doLoadProfile(string fileName, bool silent = false)
        {
            try
            {
                AppOptions subReq = null;

                XmlSerializer serializer = new XmlSerializer(typeof(AppOptions));

                StreamReader reader = new StreamReader(fileName);
                subReq = (AppOptions)serializer.Deserialize(reader);
                reader.Close();

                settings = subReq;

                if (!silent)
                {
                    MetroSetMessageBox.Show(this, "Successfully loaded profile!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                addToLog("Loaded profile " + fileName);
            }
            catch (Exception ex)
            {
                recordException(ex.Message, "LoadProfileError");
                if (!silent)
                {
                    MetroSetMessageBox.Show(this, ex.ToString(), "Error in loading XML Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        #endregion ProfileHelpers

        #region ConnectionHelpers

        private void Disconnect()
        {
            if (connected)
            {
                if (engine != null)
                {
                    CancelDownload();
                }
                dgvLibrary.DataSource = null;
                dgvContent.DataSource = null;
                dgvServers.DataSource = null;
                dgvSeasons.DataSource = null;
                dgvEpisodes.DataSource = null;
                SetProgressLabel("Disconnected");
                SetConnect();
                connected = false;
            }
        }

        private void Connect()
        {
            try
            {
                using (Connect frm = new Connect())
                {
                    ConnectionInformation existingInfo = new ConnectionInformation();
                    existingInfo.PlexAccountToken = settings.ConnectionInfo.PlexAccountToken;
                    frm.ConnectionInfo = existingInfo;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        ConnectionInformation result = frm.ConnectionInfo;
                        settings.ConnectionInfo = result;
                        user.authenticationToken = result.PlexAccountToken;

                        object serversResult;
                        if (settings.ConnectionInfo.RelaysOnly)
                            serversResult = PlexDL.WaitWindow.WaitWindow.Show(GetServerListWorker, "Getting Relays");
                        else
                            serversResult = PlexDL.WaitWindow.WaitWindow.Show(GetServerListWorker, "Getting Servers");

                        List<Server> servers = (List<Server>)serversResult;
                        if (servers.Count == 0)
                        {
                            DialogResult msg = MetroSetMessageBox.Show(this, "No servers found for entered account token. Would you like to try a direct connection?", "Authentication Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (msg == DialogResult.Yes)
                            {
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
                                    }
                                }
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
                }
            }
            catch (Exception ex)
            {
                recordException(ex.Message, "ConnectionError");
                MetroSetMessageBox.Show(this, "Connection Error:\n\n" + ex.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetContentGridViews();
                SetConnect();
            }
        }

        private void doConnectFromSelectedServer()
        {
            if (!doNotAttemptAgain)
            {
                if (dgvServers.CurrentCell != null)
                {
                    int index = dgvServers.CurrentCell.RowIndex;
                    Server s = plexServers[index];
                    string address = s.address;
                    int port = s.port;

                    ConnectionInformation connectInfo;

                    connectInfo = new ConnectionInformation()
                    {
                        PlexAccountToken = getToken(),
                        PlexAddress = address,
                        PlexPort = port,
                        RelaysOnly = settings.ConnectionInfo.RelaysOnly
                    };

                    settings.ConnectionInfo = connectInfo;

                    string uri = getBaseUri(true);
                    //MetroSetMessageBox.Show(this, uri);
                    XmlDocument reply = (XmlDocument)PlexDL.WaitWindow.WaitWindow.Show(GetXMLTransactionWorker, "Connecting", new object[] { uri });
                    if (Methods.PlexXmlValid(reply))
                    {
                        connected = true;
                        if (settings.Generic.ShowConnectionSuccess)
                        {
                            MetroSetMessageBox.Show(this, "Connection successful!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        SetProgressLabel("Connected");
                        SetDisconnect();
                        if (reply.ChildNodes.Count != 0)
                        {
                            populateLibrary(reply);
                        }
                        doNotAttemptAgain = true;
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
                    string libraryDir = getLibraryKey(doc).TrimEnd('/');
                    string baseUri = getBaseUri(false);
                    string uriSectionKey = baseUri + libraryDir + "/?X-Plex-Token=";
                    //MetroSetMessageBox.Show(this, uriSectionKey + token);
                    System.Xml.XmlDocument xmlSectionKey = GetXMLTransaction(uriSectionKey);

                    string sectionDir = getSectionKey(xmlSectionKey).TrimEnd('/');
                    string uriSections = baseUri + libraryDir + "/" + sectionDir + "/?X-Plex-Token=";
                    //MetroSetMessageBox.Show(this, uriSections+token);
                    System.Xml.XmlDocument xmlSections = GetXMLTransaction(uriSections);

                    addToLog("Creating new datasets");
                    DataSet sections = new DataSet();
                    sections.ReadXml(new XmlNodeReader(xmlSections));

                    DataTable sectionsTable;
                    sectionsTable = sections.Tables["Directory"];
                    gSectionsTable = sectionsTable;

                    addToLog("Binding to grid");
                    renderLibraryView(sectionsTable);
                    libraryFilled = true;
                    uri = baseUri + libraryDir + "/" + sectionDir + "/";
                    //we can render the content view if a row is already selected
                    if (dgvLibrary.SelectedRows.Count != 0)
                    {
                        int index = dgvLibrary.SelectedRows[0].Cells[0].RowIndex;
                        DataRow r = GetDataRowLibrary(index);
                        string key = r["key"].ToString();
                        string contentUri = uri + key + "/all/?X-Plex-Token=";
                        XmlDocument contentXml = GetXMLTransaction(contentUri);

                        contentXmlDoc = contentXml;

                        //update content view, because when we updated this list, we selected the first row automatically.

                        string type = r["type"].ToString();
                        if (type == "show")
                        {
                            updateContentView(contentXml, true);
                        }
                        else if (type == "movie")
                        {
                            updateContentView(contentXml, false);
                        }
                    }
                    DGVServersEnabled(true);
                }
                catch (WebException ex)
                {
                    recordException(ex.Message, "LibPopError");
                    DGVServersEnabled(true);
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        var response = ex.Response as HttpWebResponse;
                        recordTransaction(response.ResponseUri.ToString(), ((int)response.StatusCode).ToString());
                        if (response != null)
                        {
                            switch ((int)response.StatusCode)
                            {
                                case 401:
                                    MetroSetMessageBox.Show(this, "The web server denied access to the resource. Check your token and try again. (401)");
                                    break;

                                case 404:
                                    MetroSetMessageBox.Show(this, "The web server couldn't serve the request because it couldn't find the resource specified. (404)");
                                    break;

                                case 400:
                                    MetroSetMessageBox.Show(this, "The web server couldn't serve the request because the request was bad. (400)");
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
                    recordException(ex.Message, "LibPopError");
                    DGVServersEnabled(true);
                    MetroSetMessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Double buffering can make DGV slow in remote desktop
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dgvLibrary.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dgvLibrary, true, null);
            }

            GetTitlesTable(doc, isTVShow);

            IsTVShow = isTVShow;

            addToLog("Creating datasets");

            addToLog("Cleaning unwanted data");

            addToLog("Binding to grid");

            if (IsTVShow)
                renderTVView(titlesTable);
            else
                renderContentView(titlesTable);

            contentXmlDoc = doc;

            DGVLibraryEnabled(true);

            //MetroSetMessageBox.Show(this, "ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + titlesTable.Rows.Count.ToString());
        }

        private void updateEpisodeViewWorker(XmlDocument doc)
        {
            DGVSeasonsEnabled(false);
            addToLog("Updating episode contents");

            // Double buffering can make DGV slow in remote desktop
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dgvLibrary.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dgvLibrary, true, null);
            }

            addToLog("Creating datasets");
            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            episodesTable = sections.Tables["Video"];

            addToLog("Cleaning unwanted data");

            addToLog("Binding to grid");
            renderEpisodesView(episodesTable);

            contentXmlDoc = doc;

            DGVSeasonsEnabled(true);

            //MetroSetMessageBox.Show(this, "ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + titlesTable.Rows.Count.ToString());
        }

        private void updateSeriesViewWorker(XmlDocument doc)
        {
            DGVContentEnabled(false);
            addToLog("Updating series contents");

            // Double buffering can make DGV slow in remote desktop
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dgvLibrary.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dgvLibrary, true, null);
            }

            addToLog("Creating datasets");
            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(doc));

            seriesTable = sections.Tables["Directory"];

            addToLog("Cleaning unwanted data");

            addToLog("Binding to grid");
            renderSeriesView(seriesTable);

            contentXmlDoc = doc;

            DGVContentEnabled(true);

            //MetroSetMessageBox.Show(this, "ContentTable: " + contentTable.Rows.Count.ToString() + "\nTitlesTable: " + titlesTable.Rows.Count.ToString());
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
                        TVShowDirectoryLayout dir = DownloadLayouts.CreateDownloadLayoutTVShow(show, settings, DownloadLayouts.PlexStandardLayout);
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
                    TVShowDirectoryLayout dir = DownloadLayouts.CreateDownloadLayoutTVShow(show, settings, DownloadLayouts.PlexStandardLayout);
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

        private void SetHeaderText(DataGridView dgv, DataTable table)
        {
            //Copy column captions into DataGridView
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (table.Columns[col.Name].Caption != null)
                    col.HeaderText = table.Columns[col.Name].Caption;
            }
        }

        private List<string> OrderMatch(List<string> ordered, List<string> unordered)
        {
            List<string> newList = new List<string>();
            newList = unordered.OrderBy(d => ordered.IndexOf(d)).ToList();
            return newList;
        }

        private void renderContentView(DataTable content)
        {
            if (!(content == null))
            {
                ClearTVViews();
                ClearContentView();

                contentView = new DataView(content);

                List<string> currentColumns = new List<string>();
                List<string> currentCaption = new List<string>();
                List<string> wantedColumns = settings.DataDisplay.ContentView.ContentDisplayColumns;
                List<string> wantedCaption = settings.DataDisplay.ContentView.ContentDisplayCaption;

                DataTable dgvBind = new DataTable();

                //check if appropriate columns are part of the table; then we can verify and add them to the view.
                foreach (DataColumn c in content.Columns)
                {
                    if (wantedColumns.Contains(c.ColumnName))
                    {
                        int index = wantedColumns.IndexOf(c.ColumnName);
                        string caption = wantedCaption[index];
                        c.Caption = caption;
                        currentCaption.Add(caption);
                        currentColumns.Add(c.ColumnName);
                    }
                }

                currentColumns = OrderMatch(wantedColumns, currentColumns);

                dgvBind = contentView.ToTable(false, currentColumns.ToArray());

                contentViewTable = dgvBind;

                if (dgvContent.InvokeRequired)
                {
                    dgvContent.BeginInvoke((MethodInvoker)delegate
                    {
                        dgvContent.DataSource = dgvBind;
                        SetHeaderText(dgvContent, content);
                        dgvContent.Refresh();
                        if (!(tabMain.SelectedTab == tabMovies))
                            tabMain.SelectedTab = tabMovies;
                    });
                }
                else
                {
                    dgvContent.DataSource = dgvBind;
                    SetHeaderText(dgvContent, content);
                    dgvContent.Refresh();
                    if (!(tabMain.SelectedTab == tabMovies))
                        tabMain.SelectedTab = tabMovies;
                }
            }
        }

        private void ClearContentView()
        {
            dgvContent.DataSource = null;
        }

        private void renderTVView(DataTable content)
        {
            if (!(content == null))
            {
                ClearTVViews();
                ClearContentView();

                tvView = new DataView(content);

                List<string> currentColumns = new List<string>();
                List<string> currentCaption = new List<string>();
                List<string> wantedColumns = settings.DataDisplay.TVView.TVDisplayColumns;
                List<string> wantedCaption = settings.DataDisplay.TVView.TVDisplayCaption;

                DataTable dgvBind = new DataTable();

                //check if appropriate columns are part of the table; then we can verify and add them to the view.
                foreach (DataColumn c in content.Columns)
                {
                    if (wantedColumns.Contains(c.ColumnName))
                    {
                        int index = wantedColumns.IndexOf(c.ColumnName);
                        string caption = wantedCaption[index];
                        c.Caption = caption;
                        currentCaption.Add(caption);
                        currentColumns.Add(c.ColumnName);
                    }
                }

                currentColumns = OrderMatch(wantedColumns, currentColumns);

                dgvBind = tvView.ToTable(false, currentColumns.ToArray());

                tvViewTable = dgvBind;

                if (dgvTVShows.InvokeRequired)
                {
                    dgvTVShows.BeginInvoke((MethodInvoker)delegate
                    {
                        dgvTVShows.DataSource = dgvBind;
                        SetHeaderText(dgvTVShows, content);
                        dgvTVShows.Refresh();
                        if (!(tabMain.SelectedTab == tabTV))
                            tabMain.SelectedTab = tabTV;
                    });
                }
                else
                {
                    dgvTVShows.DataSource = dgvBind;
                    SetHeaderText(dgvTVShows, content);
                    dgvTVShows.Refresh();
                    if (!(tabMain.SelectedTab == tabTV))
                        tabMain.SelectedTab = tabTV;
                }
            }
        }

        private void renderSeriesView(DataTable content)
        {
            if (!(content == null))
            {
                seasonsView = new DataView(content);

                List<string> currentColumns = new List<string>();
                List<string> currentCaption = new List<string>();
                List<string> wantedColumns = settings.DataDisplay.SeriesView.SeriesDisplayColumns;
                List<string> wantedCaption = settings.DataDisplay.SeriesView.SeriesDisplayCaption;

                DataTable dgvBind = new DataTable();

                //check if appropriate columns are part of the table; then we can verify and add them to the view.
                foreach (DataColumn c in content.Columns)
                {
                    if (wantedColumns.Contains(c.ColumnName))
                    {
                        int index = wantedColumns.IndexOf(c.ColumnName);
                        string caption = wantedCaption[index];
                        c.Caption = caption;
                        currentCaption.Add(caption);
                        currentColumns.Add(c.ColumnName);
                    }
                }

                currentColumns = OrderMatch(wantedColumns, currentColumns);

                dgvBind = seasonsView.ToTable(false, currentColumns.ToArray());

                if (dgvSeasons.InvokeRequired)
                {
                    dgvSeasons.BeginInvoke((MethodInvoker)delegate
                    {
                        dgvSeasons.DataSource = dgvBind;
                        SetHeaderText(dgvSeasons, content);
                        dgvSeasons.Refresh();
                    });
                }
                else
                {
                    dgvSeasons.DataSource = dgvBind;
                    SetHeaderText(dgvSeasons, content);
                    dgvSeasons.Refresh();
                }
            }
        }

        private void renderEpisodesView(DataTable content)
        {
            if (!(content == null))
            {
                episodesView = new DataView(content);

                List<string> currentColumns = new List<string>();
                List<string> currentCaption = new List<string>();
                List<string> wantedColumns = settings.DataDisplay.EpisodesView.EpisodesDisplayColumns;
                List<string> wantedCaption = settings.DataDisplay.EpisodesView.EpisodesDisplayCaption;

                DataTable dgvBind = new DataTable();

                //check if appropriate columns are part of the table; then we can verify and add them to the view.
                foreach (DataColumn c in content.Columns)
                {
                    if (wantedColumns.Contains(c.ColumnName))
                    {
                        int index = wantedColumns.IndexOf(c.ColumnName);
                        string caption = wantedCaption[index];
                        c.Caption = caption;
                        currentCaption.Add(caption);
                        currentColumns.Add(c.ColumnName);
                    }
                }

                currentColumns = OrderMatch(wantedColumns, currentColumns);

                dgvBind = episodesView.ToTable(false, currentColumns.ToArray());

                if (dgvEpisodes.InvokeRequired)
                {
                    dgvEpisodes.BeginInvoke((MethodInvoker)delegate
                    {
                        dgvEpisodes.DataSource = dgvBind;
                        SetHeaderText(dgvEpisodes, content);
                        dgvEpisodes.Refresh();
                    });
                }
                else
                {
                    dgvEpisodes.DataSource = dgvBind;
                    SetHeaderText(dgvEpisodes, content);
                    dgvEpisodes.Refresh();
                }
            }
        }

        private void renderServersView(List<Server> servers)
        {
            PlexDL.WaitWindow.WaitWindow.Show(this.WorkerUpdateServersView, "Updating Servers", new object[] { servers });
        }

        private void renderServersViewWorker(List<Server> servers)
        {
            DataColumn Name = new DataColumn("Name", typeof(string));
            DataColumn Address = new DataColumn("Address", typeof(string));
            DataColumn Port = new DataColumn("Port", typeof(string));

            DataTable dgvBind = new DataTable("Servers");
            dgvBind.Columns.Add(Name);
            dgvBind.Columns.Add(Address);
            dgvBind.Columns.Add(Port);

            int i = 0;
            foreach (Server r1 in servers)
            {
                i += 1;

                dgvBind.Rows.Add(r1.name, r1.address, r1.port.ToString());
            }
            if (dgvServers.InvokeRequired)
            {
                dgvServers.BeginInvoke((MethodInvoker)delegate
                {
                    dgvServers.DataSource = dgvBind;
                    foreach (DataGridViewColumn c in dgvServers.Columns)
                    {
                        c.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    dgvServers.Refresh();
                    doConnectFromSelectedServer();
                });
            }
            else
            {
                dgvServers.DataSource = dgvBind;
                foreach (DataGridViewColumn c in dgvServers.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dgvServers.Refresh();
                doConnectFromSelectedServer();
            }
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
                sectionsView = new DataView(content);

                List<string> currentColumns = new List<string>();
                List<string> currentCaption = new List<string>();
                List<string> wantedColumns = settings.DataDisplay.LibraryView.LibraryDisplayColumns;
                List<string> wantedCaption = settings.DataDisplay.LibraryView.LibraryDisplayCaption;

                DataTable dgvBind = new DataTable();

                //check if appropriate columns are part of the table; then we can verify and add them to the view.
                foreach (DataColumn c in content.Columns)
                {
                    if (wantedColumns.Contains(c.ColumnName))
                    {
                        int index = wantedColumns.IndexOf(c.ColumnName);
                        string caption = wantedCaption[index];
                        c.Caption = caption;
                        currentCaption.Add(caption);
                        currentColumns.Add(c.ColumnName);
                    }
                }

                currentColumns = OrderMatch(wantedColumns, currentColumns);

                dgvBind = sectionsView.ToTable(false, currentColumns.ToArray());

                if (dgvLibrary.InvokeRequired)
                {
                    dgvLibrary.BeginInvoke((MethodInvoker)delegate
                    {
                        dgvLibrary.DataSource = dgvBind;
                        SetHeaderText(dgvLibrary, content);
                        dgvLibrary.Refresh();
                    });
                }
                else
                {
                    dgvLibrary.DataSource = dgvBind;
                    SetHeaderText(dgvLibrary, content);
                    dgvLibrary.Refresh();
                }
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
                List<Server> result = GetServerRelays(user.authenticationToken);
                e.Result = result;
            }
        }

        private void GetXMLTransactionWorker(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            string uri = (string)e.Arguments[0];
            e.Result = GetXMLTransaction(uri, settings.ConnectionInfo.PlexAccountToken);
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
            if (directMatch)
            {
                OrderedEnumerableRowCollection<DataRow> rowCollec = titlesTable.AsEnumerable()
                                    .Where(row => row.Field<String>(column) == searchKey)
                                    .OrderByDescending(row => row.Field<String>(column));
                e.Result = rowCollec;
            }
            else
            {
                OrderedEnumerableRowCollection<DataRow> rowCollec = titlesTable.AsEnumerable()
                                .Where(row => row.Field<String>(column).ToLower().Contains(searchKey.ToLower()))
                                .OrderByDescending(row => row.Field<String>(column));
                e.Result = rowCollec;
            }
        }

        private void GetSearchTable(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            OrderedEnumerableRowCollection<DataRow> rowCollec = (OrderedEnumerableRowCollection<DataRow>)e.Arguments[0];
            e.Result = rowCollec.CopyToDataTable();
        }

        #endregion SearchWorkers

        #endregion Workers

        #region Logging

        public static void recordException(string message, string type)
        {
            ////Options weren't too great performance-wise, so I ended up using a stack-walk.
            ////If there's minimal errors happening at once, this shouldn't be a problem, otherwise disable
            ////The in-app setting to prevent this method from firing.
            if (settings.Logging.EnableExceptionLogDel)
            {
                var stackTrace = new System.Diagnostics.StackTrace();
                string function = stackTrace.GetFrame(1).GetMethod().Name;
                string[] headers = { "DateTime", "ExceptionMessage", "OccurredIn", "ExceptionType" };
                string[] LogEntry = { DateTime.Now.ToString(), message, function, type };
                logDelWriter("ExceptionLog.logdel", headers, LogEntry);
            }
        }

        private static void recordTransaction(string uri, string statusCode)
        {
            if (settings.Logging.EnableXMLTransactionLogDel)
            {
                string[] headers = { "DateTime", "Uri", "StatusCode" };
                string[] LogEntry = { DateTime.Now.ToString(), uri, statusCode };
                logDelWriter("TransactionLog.logdel", headers, LogEntry);
            }
        }

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
                logDelWriter("PlexDL.logdel", headers, logEntryToAdd);
        }

        private void DGVDataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            DataGridView parent = (DataGridView)sender;
            //don't show the event to the user; but log it.
            addToLog("Experienced data error in " + parent.Name);
            e.Cancel = true;
        }

        private static void logDelWriter(string fileName, string[] headers, string[] logEntry)
        {
            try
            {
                if (!System.IO.Directory.Exists("Logs"))
                    System.IO.Directory.CreateDirectory("Logs");

                string logdelLine = "";
                string fqFile = @"Logs\" + fileName;

                foreach (string l in logEntry)
                {
                    logdelLine += l + "!";
                }
                logdelLine = logdelLine.TrimEnd('!');

                string headersString = "###";
                foreach (string h in headers)
                {
                    headersString += h + "!";
                }
                headersString = headersString.TrimEnd('!');

                if (File.Exists(fqFile))
                {
                    string existing = File.ReadAllText(fqFile);
                    if (existing != "")
                    {
                        string contentToWrite = existing + "\n" + logdelLine;
                        File.WriteAllText(fqFile, contentToWrite);
                    }
                    else
                    {
                        string contentToWrite = headersString + "\n" + logdelLine;
                        File.WriteAllText(fqFile, contentToWrite);
                    }
                }
                else
                {
                    string contentToWrite = headersString + "\n" + logdelLine;
                    File.WriteAllText(fqFile, contentToWrite);
                }
            }
            catch
            {
                //ignore the error
            }
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

            string filePart = partRow["key"].ToString();
            string container = partRow["container"].ToString();
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

        #endregion DownloadInfoGatherers

        #region DownloadMethods

        private void CancelDownload()
        {
            if (wkrGetMetadata.IsBusy)
            {
                wkrGetMetadata.Abort();
            }
            if (engineIsRunning)
            {
                engine.Cancel();
                engine.Clear();
            }
            if (downloadIsRunning)
            {
                SetProgressLabel("Download Cancelled");
                SetDownloadStart();
                SetResume();
                pbMain.Value = 100;
                addToLog("Download Cancelled");
                downloadIsRunning = false;
                downloadIsPaused = false;
                engineIsRunning = false;
                DownloadQueueCancelled = true;
                downloadsSoFar = 0;
                DownloadTotal = 0;
                DownloadIndex = 0;
                MetroSetMessageBox.Show(this, "Download cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                recordException(ex.Message, "CheckWebError");
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
            //MetroSetMessageBox.Show(this, "Started!");
            addToLog("Download is Progressing");
            downloadIsRunning = true;
            engineIsRunning = true;
            downloadIsPaused = false;
            SetPause();
        }

        private void SetDownloadDirectory()
        {
            if (fbdSave.ShowDialog() == DialogResult.OK)
            {
                settings.Generic.DownloadDirectory = fbdSave.SelectedPath;
                MetroSetMessageBox.Show(this, "Download directory updated to " + settings.Generic.DownloadDirectory, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                addToLog("Download directory updated to " + settings.Generic.DownloadDirectory);
            }
        }

        private void SetDownloadCompleted()
        {
            pbMain.Value = pbMain.Maximum;
            SetResume();
            btnPause.Enabled = false;
            SetDownloadStart();
            SetProgressLabel("Download Completed");
            addToLog("Download completed");
            engine.Clear();
            downloadIsRunning = false;
            downloadIsPaused = false;
            engineIsRunning = false;
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
                    DialogResult msg = MetroSetMessageBox.Show(this, dl.FileName + " already exists. Skip this title?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msg == DialogResult.Yes)
                    {
                        SetDownloadCompleted();
                        return;
                    }
                }
                engine.Add(dl.Link, fqPath);
            }
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

            //MetroSetMessageBox.Show(this, "Started!");
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

        private void SortingEnabled(DataGridView dgv, bool enabled)
        {
            if (enabled)
            {
                foreach (DataGridViewColumn c in dgv.Columns)
                    c.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            else
            {
                foreach (DataGridViewColumn c in dgv.Columns)
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void DisableContentSorting()
        {
            IsContentSortingEnabled = false;
            SortingEnabled(dgvContent, IsContentSortingEnabled);
        }

        private void EnableContentSorting()
        {
            IsContentSortingEnabled = true;
            SortingEnabled(dgvContent, IsContentSortingEnabled);
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
                recordException(ex.Message, "SearchError");
                MetroSetMessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void GetFilteredTable(string query, string column, bool isTVShow, bool silent = true)
        {
            DataTable tblFiltered;
            OrderedEnumerableRowCollection<DataRow> rowCollec = (OrderedEnumerableRowCollection<DataRow>)PlexDL.WaitWindow.WaitWindow.Show(GetSearchEnum, "Filtering Records", new object[] { query, isTVShow, column });
            FilterQuery = query;
            if (rowCollec.Count() > 0)
            {
                tblFiltered = (DataTable)PlexDL.WaitWindow.WaitWindow.Show(GetSearchTable, "Binding", new object[] { rowCollec });
            }
            else
            {
                if (!silent)
                    MetroSetMessageBox.Show(this, "No Results Found for '" + query + "'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            IsFiltered = true;
            filteredTable = tblFiltered;
            //MetroSetMessageBox.Show(this, "Filtered Table:" + filteredTable.Rows.Count + "\nTitles Table:" + titlesTable.Rows.Count);
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

        private static Image InvertGDI(Image imgSource)
        {
            Bitmap bmpDest = null;

            using (Bitmap bmpSource = new Bitmap(imgSource))
            {
                bmpDest = new Bitmap(bmpSource.Width, bmpSource.Height);

                for (int x = 0; x < bmpSource.Width; x++)
                {
                    for (int y = 0; y < bmpSource.Height; y++)
                    {
                        Color clrPixel = bmpSource.GetPixel(x, y);

                        clrPixel = Color.FromArgb(255 - clrPixel.R, 255 -
                           clrPixel.G, 255 - clrPixel.B);

                        bmpDest.SetPixel(x, y, clrPixel);
                    }
                }
            }

            return (Image)bmpDest;
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

        #region Events

        #region PaintOverrides

        private void dgvSeasons_Paint(object sender, PaintEventArgs e)
        {
            if ((dgvSeasons.Rows.Count == 0) && (connected))
            {
                TextRenderer.DrawText(e.Graphics, "No TV Seasons Found",
                    dgvSeasons.Font, dgvSeasons.ClientRectangle,
                    dgvSeasons.ForeColor, dgvSeasons.BackgroundColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvEpisodes_Paint(object sender, PaintEventArgs e)
        {
            if ((dgvEpisodes.Rows.Count == 0) && (connected))
            {
                TextRenderer.DrawText(e.Graphics, "No TV Episodes Found",
                    dgvEpisodes.Font, dgvEpisodes.ClientRectangle,
                    dgvEpisodes.ForeColor, dgvEpisodes.BackgroundColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvServers_Paint(object sender, PaintEventArgs e)
        {
            if (dgvServers.Rows.Count == 0)
            {
                TextRenderer.DrawText(e.Graphics, "No Servers Found",
                    dgvServers.Font, dgvServers.ClientRectangle,
                    dgvServers.ForeColor, dgvServers.BackgroundColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvLibrary_Paint(object sender, PaintEventArgs e)
        {
            if ((dgvLibrary.Rows.Count == 0) && (connected))
            {
                TextRenderer.DrawText(e.Graphics, "No Library Sections Found",
                    dgvLibrary.Font, dgvLibrary.ClientRectangle,
                    dgvLibrary.ForeColor, dgvLibrary.BackgroundColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvContent_Paint(object sender, PaintEventArgs e)
        {
            if (dgvContent.Rows.Count == 0)
            {
                TextRenderer.DrawText(e.Graphics, "Not Connected",
                    dgvContent.Font, dgvContent.ClientRectangle,
                    dgvContent.ForeColor, dgvContent.BackgroundColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        #endregion PaintOverrides

        #region FormEvents

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (downloadIsRunning)
            {
                if (!(msgAlreadyShown))
                {
                    DialogResult msg = MetroSetMessageBox.Show(this, "Are you sure you want to exit PlexDL? A download is still running.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msg == DialogResult.Yes)
                    {
                        msgAlreadyShown = true;
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

        private FontFamily LoadFont(byte[] fontResource)
        {
            int dataLength = fontResource.Length;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontResource, 0, fontPtr, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(fontPtr, (uint)fontResource.Length, IntPtr.Zero, ref cFonts);
            privateFontCollection.AddMemoryFont(fontPtr, dataLength);

            return privateFontCollection.Families.Last();
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
                recordException(ex.Message, "StartupError");
                MetroSetMessageBox.Show(this, "Startup Error:\n\n" + ex.ToString(), "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                XmlDocument contentXml = GetXMLTransaction(contentUri);

                contentXmlDoc = contentXml;
                updateContentView(contentXml, isTVShow);
            }
            catch (WebException ex)
            {
                recordException(ex.Message, "UpdateLibraryError");
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    recordTransaction(response.ResponseUri.ToString(), ((int)response.StatusCode).ToString());
                    if (response != null)
                    {
                        switch ((int)response.StatusCode)
                        {
                            case 401:
                                MetroSetMessageBox.Show(this, "The web server denied access to the resource. Check your token and try again. (401)");
                                break;

                            case 404:
                                MetroSetMessageBox.Show(this, "The web server couldn't serve the request because it couldn't find the resource specified. (404)");
                                break;

                            case 400:
                                MetroSetMessageBox.Show(this, "The web server couldn't serve the request because the request was bad. (400)");
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
                recordException(ex.Message, "UpdateLibraryError");
                MetroSetMessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if ((dgvLibrary.SelectedRows.Count == 1) && (libraryFilled))
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
                if (!(address == settings.ConnectionInfo.PlexAddress))
                {
                    doNotAttemptAgain = false;
                    //MetroSetMessageBox.Show(this, "attempted connection");
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
                XmlDocument reply = (XmlDocument)PlexDL.WaitWindow.WaitWindow.Show(GetXMLTransactionWorker, "Connecting", new object[] { uri });
                MetroSetMessageBox.Show(this, "Connection successful!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (WebException ex)
            {
                recordException(ex.Message, "TestConnectionError");
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    recordTransaction(response.ResponseUri.ToString(), ((int)response.StatusCode).ToString());
                    if (response != null)
                    {
                        switch ((int)response.StatusCode)
                        {
                            case 401:
                                MetroSetMessageBox.Show(this, "The web server denied access to the resource. Check your token and try again. (401)");
                                break;

                            case 404:
                                MetroSetMessageBox.Show(this, "The web server couldn't serve the request because it couldn't find the resource specified. (404)");
                                break;

                            case 400:
                                MetroSetMessageBox.Show(this, "The web server couldn't serve the request because the request was bad. (400)");
                                break;
                        }
                    }
                    else
                    {
                        MetroSetMessageBox.Show(this, "Unknown status code; the server failed to serve the request. (?)");
                    }
                }
                else
                {
                    MetroSetMessageBox.Show(this, "An unknown error occurred:\n\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!connected)
                {
                    if (CheckForInternetConnection())
                    {
                        Connect();
                    }
                    else
                    {
                        MetroSetMessageBox.Show(this, "No internet connection", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Disconnect();
                    MetroSetMessageBox.Show(this, "Disconnected from server", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (WebException ex)
            {
                recordException(ex.Message, "ConnectionError");
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    recordTransaction(response.ResponseUri.ToString(), ((int)response.StatusCode).ToString());
                    if (response != null)
                    {
                        switch ((int)response.StatusCode)
                        {
                            case 401:
                                MetroSetMessageBox.Show(this, "The web server denied access to the resource. Check your token and try again. (401)");
                                break;

                            case 404:
                                MetroSetMessageBox.Show(this, "The web server couldn't serve the request because it couldn't find the resource specified. (404)");
                                break;

                            case 400:
                                MetroSetMessageBox.Show(this, "The web server couldn't serve the request because the request was bad. (400)");
                                break;
                        }
                    }
                    else
                    {
                        MetroSetMessageBox.Show(this, "Unknown status code; the server failed to serve the request. (?)");
                    }
                }
                else
                {
                    MetroSetMessageBox.Show(this, "An unknown error occurred:\n\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        private void DoDownloadAllEpisodes()
        {
            if (!downloadIsRunning && !engineIsRunning)
            {
                SetProgressLabel("Waiting");
                DownloadAllEpisodes = true;
                DownloadTotal = episodesTable.Rows.Count;
                downloadIsRunning = true;
                wkrGetMetadata.RunWorkerAsync();
                SetDownloadCancel();
            }
        }

        private void DoDownloadSelected()
        {
            if (!downloadIsRunning && !engineIsRunning)
            {
                SetProgressLabel("Waiting");
                DownloadAllEpisodes = false;
                DownloadTotal = 1;
                downloadIsRunning = true;
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
                //MetroSetMessageBox.Show(this, name);
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
                    //MetroSetMessageBox.Show(this, "Titles Table:" + titlesTable.Rows.Count + "\nGridView:" + dgvContent.Rows.Count);
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
                    //MetroSetMessageBox.Show(this, "Filtered Table:" + filteredTable.Rows.Count + "\nGridView:" + dgvContent.Rows.Count);
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
                if (!downloadIsRunning && !engineIsRunning)
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
            if (downloadIsRunning && engineIsRunning)
            {
                if (!downloadIsPaused)
                {
                    engine.Pause();
                    SetResume();
                    lblProgress.Text += " (Paused)";
                    downloadIsPaused = true;
                }
                else
                {
                    engine.ResumeAsync();
                    SetPause();
                    downloadIsPaused = false;
                }
            }
        }

        public static bool _IsPrivate(string ipAddress)
        {
            int[] ipParts = ipAddress.Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(s => int.Parse(s)).ToArray();
            // in private ip range
            if (ipParts[0] == 10 ||
                (ipParts[0] == 192 && ipParts[1] == 168) ||
                (ipParts[0] == 172 && (ipParts[1] >= 16 && ipParts[1] <= 31)))
            {
                return true;
            }

            // IP Address is probably public.
            // This doesn't catch some VPN ranges like OpenVPN and Hamachi.
            return false;
        }

        private List<Server> GetServerRelays(string token, bool matchCurrentList = true)
        {
            try
            {
                List<Server> relays = new List<Server>();
                string uri = "https://plex.tv/api/resources?includeHttps=1&amp;includeRelay=1&amp;X-Plex-Token=";
                XmlDocument reply = GetXMLTransaction(uri, token);
                XmlNode root = reply.SelectSingleNode("MediaContainer");
                //MessageBox.Show(root.Name);
                if (root.HasChildNodes)
                {
                    for (int i = 0; i < root.ChildNodes.Count; i++)
                    {
                        XmlNode node = root.ChildNodes[i];
                        //MessageBox.Show(node.Name);
                        string accessToken = "";
                        if (node.Attributes["accessToken"] != null)
                        {
                            accessToken = node.Attributes["accessToken"].Value.ToString();
                        }
                        string name = node.Attributes["name"].Value.ToString();
                        string address = "";
                        string local = "";
                        int port = 0;
                        if (node.HasChildNodes)
                        {
                            foreach (XmlNode n in node.ChildNodes)
                            {
                                Uri u = new Uri(n.Attributes["uri"].Value.ToString());
                                string u_parse = u.Host;
                                string[] u_parse_split = u_parse.Split('.');
                                local = n.Attributes["address"].Value.ToString();

                                //MessageBox.Show(u_parse_temp);
                                if (u_parse.Contains(".plex.direct") && (!_IsPrivate(local)))
                                {
                                    address = u_parse;
                                    port = Convert.ToInt32(n.Attributes["port"].Value);
                                    Server svrRelay = new Server()
                                    {
                                        address = address,
                                        port = port,
                                        name = name,
                                        accessToken = accessToken
                                    };
                                    relays.Add(svrRelay);
                                    break;
                                }
                            }
                        }
                    }
                }
                //MessageBox.Show(relays.Count.ToString());
                return relays;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Relay Retrieval Error\n\n" + ex.ToString(), "Relay Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Home.recordException(ex.Message, "GetRelaysError");
                return new List<Server>();
            }
        }

        private void ReplaceServersWithRelays()
        {
            plexServers = GetServerRelays(settings.ConnectionInfo.PlexAccountToken);
            dgvServers.DataSource = null;
            renderServersView(plexServers);
        }

        private void StartStreaming(PlexObject stream)
        {
            if (settings.Player.PlaybackEngine == PlaybackMode.PVSPlayer)
            {
                if (!downloadIsRunning)
                {
                    Player frm = new Player();
                    frm.StreamingContent = stream;
                    frm.TitlesTable = returnCorrectTable();
                    addToLog("Started streaming " + stream.StreamInformation.ContentTitle);
                    frm.ShowDialog();
                }
                else
                {
                    MetroSetMessageBox.Show(this, "You cannot stream " + stream.StreamInformation.ContentTitle + " because a download is already running. Cancel the download before attempting to stream within PlexDL.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MetroSetMessageBox.Show(this, "No episode is selected", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ClearTVViews()
        {
            dgvSeasons.DataSource = null;
            dgvEpisodes.DataSource = null;
            dgvTVShows.DataSource = null;
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
                if (!downloadIsRunning && !engineIsRunning)
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
                    MetroSetMessageBox.Show(this, "You cannot view metadata while an internal download is running");
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

        #endregion Events

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
                    MetroSetMessageBox.Show(this, "Section key ust be numeric", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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