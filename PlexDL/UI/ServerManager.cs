using inet;
using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Caching;
using PlexDL.Common.Caching.Handlers;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Renderers.DGVRenderers;
using PlexDL.Common.Structures;
using PlexDL.PlexAPI;
using PlexDL.PlexAPI.LoginHandler;
using PlexDL.PlexAPI.LoginHandler.Auth.Enums;
using PlexDL.WaitWindow;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.UI
{
    public partial class ServerManager : Form
    {
        public ServerManager()
        {
            InitializeComponent();
        }

        public Server SelectedServer { get; set; }
        public bool RenderTokenColumn { get; set; } = false;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (dgvServers.SelectedRows.Count != 1) return base.ProcessCmdKey(ref msg, keyData);
            if (keyData != Keys.Enter) return base.ProcessCmdKey(ref msg, keyData);

            DoConnect();
            return true;
        }

        private void RenderServersView(IReadOnlyCollection<Server> servers)
        {
            WaitWindow.WaitWindow.Show(RenderServersView_Worker, @"Updating Grid", servers, RenderTokenColumn);
        }

        private void RenderServersView_Worker(object sender, WaitWindowEventArgs e)
        {
            var servers = (List<Server>)e.Arguments[0];
            var renderTokenColumn = (bool)e.Arguments[1];

            ServerViewRenderer.RenderView(servers, renderTokenColumn, dgvServers);
        }

        private static void GetServerListWorker(object sender, WaitWindowEventArgs e)
        {
            Helpers.CacheStructureBuilder();
            if (ServerCaching.ServerInCache(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken) &&
                ObjectProvider.Settings.CacheSettings.Mode.EnableServerCaching)
            {
                e.Result = ServerCaching.ServerFromCache(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken);
            }
            else
            {
                var result = ObjectProvider.PlexProvider.GetServers(ObjectProvider.User);
                if (ObjectProvider.Settings.CacheSettings.Mode.EnableServerCaching)
                    ServerCaching.ServerToCache(result, ObjectProvider.User.authenticationToken);
                e.Result = result;
            }
        }

        private static void GetRelaysListWorker(object sender, WaitWindowEventArgs e)
        {
            var result = Relays.GetServerRelays(ObjectProvider.User.authenticationToken);
            e.Result = result;
        }

        private void LoadServers(bool silent = false)
        {
            try
            {
                //check if there's a connection before trying to contact Plex.tv
                if (ConnectionChecker.CheckForInternetConnection())
                {
                    if (!IsTokenSet()) return;

                    var serversResult = WaitWindow.WaitWindow.Show(GetServerListWorker, "Fetching Servers");
                    var servers = (List<Server>)serversResult;
                    if (servers.Count == 0)
                    {
                        if (!silent)
                            UIMessages.Error(
                                @"No servers found for current account token. Please update your token or try a direct connection.",
                                @"Authentication Error");
                    }
                    else
                    {
                        ObjectProvider.PlexServers = servers;
                        RenderServersView(servers);
                        itmConnect.Enabled = true;
                    }
                }
                else
                {
                    // trying to connect on no connection will not end well; alert the user.
                    if (!silent)
                        UIMessages.Warning(
                            @"No internet connection. Please connect to a network before attempting to load servers.",
                            @"Network Error");
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                    UIMessages.Error("Server retrieval error\n\n" + ex, @"Data Error");
                LoggingHelpers.RecordException(ex.Message, "ServerGetError");
            }
        }

        private void LoadRelays(bool silent = false)
        {
            try
            {
                //check if there's a connection before trying to contact Plex.tv
                if (ConnectionChecker.CheckForInternetConnection())
                {
                    if (!IsTokenSet()) return;

                    var serversResult = WaitWindow.WaitWindow.Show(GetRelaysListWorker, "Fetching Relays");
                    var servers = (List<Server>)serversResult;
                    if (servers.Count == 0)
                    {
                        if (!silent)
                            UIMessages.Error(
                                @"No relays found for current account token. Please update your token or try a direct connection.",
                                @"Authentication Error");
                    }
                    else
                    {
                        ObjectProvider.PlexServers = servers;
                        RenderServersView(servers);
                        itmConnect.Enabled = true;
                    }
                }
                else
                {
                    // trying to connect on no connection will not end well; alert the user.
                    if (!silent)
                        UIMessages.Warning(
                            @"No internet connection. Please connect to a network before attempting to load relays.",
                            @"Network Error");
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                    UIMessages.Error("Relay retrieval error\n\n" + ex, @"Data Error");
                LoggingHelpers.RecordException(ex.Message, "RelayGetError");
            }
        }

        private Server CurrentServer()
        {
            var index = dgvServers.CurrentCell.RowIndex;
            return ObjectProvider.PlexServers[index];
        }

        private void DoConnect()
        {
            try
            {
                if (ConnectionChecker.CheckForInternetConnection())
                {
                    if (dgvServers.SelectedRows.Count != 1 || !IsTokenSet()) return;

                    //make sure that we can connect to this server.
                    //This way, we can avoid unwanted errors for the
                    //user.
                    var s = CurrentServer();
                    var testUrl = TestConnection(s);

                    if (testUrl.ConnectionSuccess)
                    {
                        SelectedServer = s;
                        ObjectProvider.Svr = s;
                        ObjectProvider.User.authenticationToken = s.accessToken;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        if (testUrl.LastException != null)
                        {
                            UIMessages.Error(
                                testUrl.StatusCode != "Undetermined"
                                    ? $@"Connection failed. The server result was: {testUrl.StatusCode}"
                                    : $@"Connection failed. The error was: {testUrl.LastException.Message}",
                                @"Network Error");
                            LoggingHelpers.RecordException(testUrl.LastException.Message, @"TestConnectionError");
                        }
                        else
                            UIMessages.Error($@"Couldn't connect to ""{s.address}:{s.port}""", @"Network Error");
                    }
                }
                else
                {
                    // trying to connect on no connection will not end well; alert the user.
                    UIMessages.Warning(
                        @"No internet connection. Please connect to a network before attempting to start a Plex server connection.",
                        @"Network Error");
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"ConnectionError");
                UIMessages.Error("Server connection attempt failed\n\n" + ex);
            }
        }

        //another method to handle testing a connection
        //this one's here for logging purposes and such, and is used
        //solely by the Server Manager
        private static WebCheck TestConnection(Server svr)
        {
            var value = new WebCheck();

            try
            {
                var uri = ConnectionLink(svr);

                //UIMessages.Info(uri);

                var testUrl = WebCheck.TestUrl(uri);

                value = testUrl;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordGeneralEntry($"Couldn't connect to \"{svr.address}:{svr.port}\"");
                LoggingHelpers.RecordException(ex.Message, "ConnectionTestError");
            }

            return value;
        }

        private static string ConnectionLink(Server svr)
        {
            return $"http://{svr.address}:{svr.port}/?X-Plex-Token={svr.accessToken}";
        }

        private void itmConnect_Click(object sender, EventArgs e)
        {
            DoConnect();
        }

        private void RunDirectConnect(bool localLink)
        {
            var info = new ConnectionInfo()
            {
                PlexAccountToken = ObjectProvider.User.authenticationToken,
                PlexAddress = ""
            };
            RunDirectConnect(localLink, info);
        }

        private void RunDirectConnect(bool localLink, ConnectionInfo info, bool diffToken = false)
        {
            var servers = new List<Server>();
            using (var frmDir = new DirectConnect())
            {
                frmDir.ConnectionInfo = info;
                frmDir.DifferentToken = diffToken;
                frmDir.LoadLocalLink = localLink;

                if (frmDir.ShowDialog() != DialogResult.OK) return;
                if (!frmDir.Success) return;

                ObjectProvider.Settings.ConnectionInfo = frmDir.ConnectionInfo;
                ObjectProvider.User.authenticationToken = frmDir.ConnectionInfo.PlexAccountToken;
                var s = new Server
                {
                    accessToken = ObjectProvider.User.authenticationToken,
                    address = ObjectProvider.Settings.ConnectionInfo.PlexAddress,
                    port = ObjectProvider.Settings.ConnectionInfo.PlexPort,
                    name = "DirectConnect"
                };
                servers.Add(s);
                ObjectProvider.PlexServers = servers;
                SelectedServer = s;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void dgvServers_SelectionChanged(object sender, EventArgs e)
        {
            itmConnect.Enabled = dgvServers.SelectedRows.Count == 1;
        }

        private void dgvServers_DoubleClick(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count == 1)
                DoConnect();
        }

        private void SelectServer()
        {
            try
            {
                //if we already selected a server, then we can remember this selection based on globals and clever indexing.
                if (ObjectProvider.Svr != null)
                {
                    var svrIndex = ObjectProvider.PlexServers.IndexOf(ObjectProvider.Svr);
                    dgvServers.Rows[svrIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                //log then ignore the error
                LoggingHelpers.RecordException(ex.Message, "SvrIndexingError");
            }
        }

        private void ProfileDefinedServer()
        {
            //check to see if a loaded profile instated some valid server details
            if (CheckProfileDefinedServer())
            {
                //refresh from app.config file(s)
                Properties.Settings.Default.Reload();

                //parse the values
                var shown = Properties.Settings.Default.PLSShown;
                var disable = Properties.Settings.Default.DisablePLSOnShown;

                if (shown && disable) return;

                const string msg =
                    @"It appears your loaded profile contains a previously loaded server. Would you like to prefill those details and connect?";

                if (UIMessages.Question(msg))
                {
                    LoadProfileDefinedServer();
                    PlsShown(true, false);
                    return;
                }

                if (!disable) return;

                UIMessages.Info(
                    @"We won't ask again. You can reenable this dialog via the global application config file (not your profile).");
                PlsShown(true);
            }
        }

        private static void PlsShown(bool value, bool save = true)
        {
            Properties.Settings.Default.PLSShown = value;
            if (save)
                Properties.Settings.Default.Save();
        }

        private void DgvServers_DoubleClick(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count == 1)
            {
                DoConnect();
            }
        }

        private void ServerManager_Load(object sender, EventArgs e)
        {
            try
            {
                //check if there's an internet connection first; it may have been disconnected while the window was closed.
                if (ConnectionChecker.CheckForInternetConnection())
                {
                    //check to see if a loaded profile instated some valid server details. If there are, we can potentially fast-forward
                    //the connection process!
                    ProfileDefinedServer();

                    // this check must be done before checking the count, because if it is null, we'll get an error for trying to access "Count" on a null object.
                    if (ObjectProvider.PlexServers == null) return;
                    if (ObjectProvider.PlexServers.Count <= 0) return;

                    RenderServersView(ObjectProvider.PlexServers);
                    SelectServer();

                    itmConnect.Enabled = true;
                    itmClearServers.Enabled = true;
                    itmLoad.Enabled = true;
                }
                else
                {
                    // trying to connect on no connection will not end well; close this window if there's no connection and alert the user.
                    UIMessages.Warning(
                        @"No internet connection. Please connect to a network before attempting to start a Plex server connection.",
                        @"Network Error");
                    Close();
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"ServerManagerLoadError");
                UIMessages.Error("Server manager failed to load necessary data\n\n" + ex);
            }
        }

        private void ItmDirectConnection_Click(object sender, EventArgs e)
        {
            if (IsTokenSet())
                RunDirectConnect(false);
        }

        private void ItmLocalLink_Click(object sender, EventArgs e)
        {
            if (IsTokenSet())
                RunDirectConnect(true);
        }

        private void ItmRelays_Click(object sender, EventArgs e)
        {
            if (IsTokenSet())
                LoadRelays();
        }

        private void ItmServers_Click(object sender, EventArgs e)
        {
            if (IsTokenSet())
                LoadServers();
        }

        private void ItmViaPlexTv_Click(object sender, EventArgs e)
        {
            try
            {
                //check if there's a connection before trying to update the authentication token
                if (ConnectionChecker.CheckForInternetConnection())
                {
                    var auth = AuthRoutine.GetAuthToken();

                    var r = auth.Result;

                    switch (r)
                    {
                        case AuthResult.Success:
                            if (ApplyToken(auth.Token))
                            {
                                UIMessages.Info(@"Successfully connected to Plex.tv. You can now load and connect to your servers/relays.", @"Success");
                                LoadServers(true);
                            }
                            else
                                UIMessages.Error(@"An unknown error occurred; we couldn't apply your account token.");

                            break;

                        case AuthResult.Cancelled: //nothing
                            break;

                        case AuthResult.Failed: //alert the user to the failure
                            UIMessages.Error(@"Failed to apply your account token; the licensing authority didn't authorise the transaction or values were not valid.");
                            break;

                        case AuthResult.Error: //alert the user to the error
                            UIMessages.Error(@"An unknown error occurred; we couldn't apply your account token.");
                            break;

                        case AuthResult.Invalid: //alert the user
                            UIMessages.Error(@"Failed to apply your account token; details were invalid.");
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                    // trying to connect on no connection will not end well; alert the user.
                    UIMessages.Warning(
                        @"No internet connection. Please connect to a network before attempting to authenticate.",
                        @"Network Error");
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ConnectionError");
                UIMessages.Error("Connection Error:\n\n" + ex, @"Connection Error");
            }
        }

        private void ItmViaToken_Click(object sender, EventArgs e)
        {
            try
            {
                //check if there's a connection before trying to update the authentication token
                if (ConnectionChecker.CheckForInternetConnection())
                    using (var frm = new Authenticate())
                    {
                        var existingInfo = new ConnectionInfo
                        {
                            PlexAccountToken = ObjectProvider.Settings.ConnectionInfo.PlexAccountToken
                        };
                        frm.ConnectionInfo = existingInfo;

                        if (frm.ShowDialog() != DialogResult.OK) return;
                        if (!frm.Success) return;

                        if (ApplyToken(frm.ConnectionInfo.PlexAccountToken))
                        {
                            UIMessages.Info(
                                @"Token applied successfully. You can now load servers and relays from Plex.tv");
                            LoadServers(true);
                        }
                        else
                            UIMessages.Error(@"An unknown error occurred");
                    }
                else
                    // trying to connect on no connection will not end well; alert the user.
                    UIMessages.Warning(
                        @"No internet connection. Please connect to a network before attempting to authenticate.",
                        @"Network Error");
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ConnectionError");
                UIMessages.Error("Connection Error:\n\n" + ex, @"Connection Error");
            }
        }

        private static bool CheckProfileDefinedServer()
        {
            var settings = ObjectProvider.Settings.ConnectionInfo;
            var defaultAddress = new ConnectionInfo().PlexAddress;
            return !string.IsNullOrWhiteSpace(settings.PlexAccountToken) &&
                   !string.IsNullOrWhiteSpace(settings.PlexAddress) &&
                   settings.PlexAddress != defaultAddress;
        }

        private void LoadProfileDefinedServer()
        {
            if (!CheckProfileDefinedServer()) return;

            var info = ObjectProvider.Settings.ConnectionInfo;
            RunDirectConnect(false, info, true);
        }

        private static bool IsTokenSet()
        {
            return !string.IsNullOrEmpty(ObjectProvider.User.authenticationToken);
        }

        private bool ApplyToken(string token, bool silent = true)
        {
            try
            {
                //check if there's a connection before trying to update the authentication token
                if (ConnectionChecker.CheckForInternetConnection())
                {
                    ObjectProvider.User.authenticationToken = token;
                    ObjectProvider.Settings.ConnectionInfo.PlexAccountToken = token;
                    itmLoad.Enabled = true;
                    itmClearServers.Enabled = true;
                    dgvServers.DataSource = null;
                    if (!silent)
                        UIMessages.Info(
                            @"Token applied successfully. You can now load servers and relays from Plex.tv");

                    return true;
                }

                // trying to connect on no connection will not end well; alert the user.
                if (!silent)
                    UIMessages.Warning(
                        @"No internet connection. Please connect to a network before attempting to authenticate.",
                        @"Network Error");
                return false;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ConnectionError");
                if (!silent)
                    UIMessages.Error("Connection Error:\n\n" + ex, @"Connection Error");
                return false;
            }
        }

        private void ItmClearServers_Click(object sender, EventArgs e)
        {
            dgvServers.DataSource = null;
            //force a repaint
            dgvServers.Invalidate();
            ObjectProvider.PlexServers = null;
            itmConnect.Enabled = false;
            itmLoad.Enabled = false;
            itmClearServers.Enabled = false;
        }

        private void ItmRenderTokenColumn_Click(object sender, EventArgs e)
        {
            RenderTokenColumn = itmRenderTokenColumn.Checked;
            if (ObjectProvider.PlexServers != null)
                RenderServersView(ObjectProvider.PlexServers);
        }

        private void CxtServers_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Flags.IsDebug || dgvServers.SelectedRows.Count == 0)
                e.Cancel = true;
        }

        private void ItmViewLink_Click(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count == 1)
            {
                var lnk = new LinkViewer() { Link = ConnectionLink(CurrentServer()) };
                lnk.ShowDialog();
            }
        }

        private void ItmViewAccountToken_Click(object sender, EventArgs e)
        {
            var token = ObjectProvider.User.authenticationToken;

            if (string.IsNullOrEmpty(token)) return;

            UIMessages.Info($@"Your account token is: {token}

We've also copied it to the clipboard for you :)");

            Clipboard.SetText(token);
        }

        private void ItmCopyServerToken_Click(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count == 1)
            {
                var token = CurrentServer().accessToken;

                if (string.IsNullOrEmpty(token)) return;

                Clipboard.SetText(token);
            }
        }
    }
}