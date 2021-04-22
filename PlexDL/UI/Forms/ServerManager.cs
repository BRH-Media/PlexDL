using inet;
using PlexDL.AltoHTTP.Common.Net;
using PlexDL.Common.API.PlexAPI;
using PlexDL.Common.Caching;
using PlexDL.Common.Caching.Handlers;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Globals;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Renderers.Forms.GridView;
using PlexDL.Common.Structures.AppOptions;
using PlexDL.MyPlex;
using PlexDL.PlexAPI.LoginHandler.Auth;
using PlexDL.WaitWindow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UIHelpers;

// ReSharper disable InvertIf

#pragma warning disable 1591

namespace PlexDL.UI.Forms
{
    public partial class ServerManager : DoubleBufferedForm
    {
        public ServerManager()
        {
            InitializeComponent();
        }

        public Server SelectedServer { get; set; }
        public bool RenderTokenColumn { get; set; }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (dgvServers.SelectedRows.Count != 1) return base.ProcessCmdKey(ref msg, keyData);
            if (keyData != Keys.Enter) return base.ProcessCmdKey(ref msg, keyData);

            DoConnect();

            return true;
        }

        private void RenderServersView(IReadOnlyCollection<Server> servers)
        {
            WaitWindow.WaitWindow.Show(RenderServersView, @"Updating Grid",
                servers, RenderTokenColumn);

            //update view label
            SetInterfaceViewingStatus();
        }

        private void RenderServersView(object sender, WaitWindowEventArgs e)
        {
            if (e.Arguments.Count == 2)
            {
                var servers = (List<Server>)e.Arguments[0];
                var renderTokenColumn = (bool)e.Arguments[1];

                ServerViewRenderer.RenderView(servers, renderTokenColumn, dgvServers);
            }
        }

        private static void GetServerListWorker(object sender, WaitWindowEventArgs e)
        {
            CachingHelpers.CacheStructureBuilder();
            if (ServerCaching.ServerInCache(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken) &&
                ObjectProvider.Settings.CacheSettings.Mode.EnableServerCaching)
                e.Result = ServerCaching.ServerFromCache(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken);
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
            var result = Relays.GetServerRelays();
            e.Result = result;
        }

        private void LoadServers(bool silent = false)
        {
            try
            {
                //check if there's a connection before trying to contact Plex.tv
                if (Internet.IsConnected)
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
                if (Internet.IsConnected)
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
                if (Internet.IsConnected)
                {
                    if (dgvServers.SelectedRows.Count != 1 || !IsTokenSet()) return;

                    //make sure that we can connect to this server.
                    //This way, we can avoid unwanted errors for the
                    //user.
                    var s = CurrentServer();
                    var testUrl = TestConnection(s);

                    //connection success test
                    if (testUrl.ConnectionSuccess)
                    {
                        //apply internal
                        SelectedServer = s;

                        //apply globals
                        ObjectProvider.Svr = s;
                        ObjectProvider.User.authenticationToken = s.accessToken;

                        //form operation
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
                    // trying to connect on no connection will not end well; alert the user.
                    UIMessages.Warning(
                        @"No internet connection. Please connect to a network before attempting to start a Plex server connection.",
                        @"Network Error");
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"ConnectionError");
                UIMessages.Error("Server connection attempt failed\n\n" + ex);
            }
        }

        private void ItmConnect_Click(object sender, EventArgs e)
        {
            DoConnect();
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
            => $@"http://{svr.address}:{svr.port}/?X-Plex-Token={svr.accessToken}";

        private void RunDirectConnect(bool localLink)
        {
            var info = new ConnectionInfo
            {
                PlexAccountToken = ObjectProvider.User.authenticationToken,
                PlexAddress = ""
            };

            RunDirectConnect(localLink, info);
        }

        private void RunDirectConnect(bool localLink, ConnectionInfo info, bool diffToken = false)
        {
            //new list of servers
            var servers = new List<Server>();

            //construct a new direct connection dialog
            using (var frmDir = new DirectConnect())
            {
                //setup form prerequisities
                frmDir.ConnectionInfo = info;
                frmDir.DifferentToken = diffToken;
                frmDir.LoadLocalLink = localLink;

                //dialog verification
                if (frmDir.ShowDialog() != DialogResult.OK) return;
                if (!frmDir.Success) return;

                //set auth globals
                ObjectProvider.Settings.ConnectionInfo = frmDir.ConnectionInfo;
                ObjectProvider.User.authenticationToken = frmDir.ConnectionInfo.PlexAccountToken;

                //construct new server obejct from supplied direct connection information
                var s = new Server
                {
                    accessToken = ObjectProvider.User.authenticationToken,
                    address = ObjectProvider.Settings.ConnectionInfo.PlexAddress,
                    port = ObjectProvider.Settings.ConnectionInfo.PlexPort,
                    name = "DirectConnect"
                };

                //apply listing information for auth
                servers.Add(s);
                ObjectProvider.PlexServers = servers;

                //set globals
                SelectedServer = s;
                DialogResult = DialogResult.OK;

                //close the GUI
                Close();
            }
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

        private void SetInterfaceViewingStatus()
        {
            try
            {
                //check for nulls
                if (ObjectProvider.PlexServers != null && dgvServers.DataSource != null)
                {
                    //data length counters
                    var gridCount = dgvServers.Rows.Count;
                    var totalCount = ObjectProvider.PlexServers.Count;

                    //the total can't exceed the current amount
                    lblViewingValue.Text = totalCount < gridCount
                        ? $@"{gridCount}/{totalCount}"
                        : $@"{totalCount}/{totalCount}";
                }
                else
                    //set the viewing values to 0
                    lblViewingValue.Text = @"0/0";
            }
            catch (Exception ex)
            {
                //log the error but don't inform the user
                LoggingHelpers.RecordException(ex.Message, @"ServerManagerViewingStatusError");
            }
        }

        private void SetInterfaceAuthenticationStatus(bool isAuthenticated)
        {
            try
            {
                //set the fore colour dependent on status
                lblAuthenticationStatus.ForeColor = isAuthenticated
                                        ? Color.DarkGreen
                                        : Color.DarkRed;

                //set text dependent on status
                lblAuthenticationStatus.Text = isAuthenticated
                                        ? @"Authenticated"
                                        : @"Unauthenticated";
            }
            catch (Exception ex)
            {
                //log the error but don't inform the user
                LoggingHelpers.RecordException(ex.Message, @"ServerManagerAuthStatusError");
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

                //don't show the message if these are both true (cancel execution)
                if (shown && disable)
                    return;

                //the message to display to the user
                const string msg =
                    "It appears your loaded profile contains a previously loaded server. " +
                    "Would you like to prefill those details and connect?";

                //prompt the user (true for 'Yes', false for 'No')
                if (UIMessages.Question(msg))
                {
                    LoadProfileDefinedServer();
                    PlsShown(true, false);

                    return;
                }

                if (!disable)
                    return;

                UIMessages.Info(
                    @"We won't ask again. You can reenable this dialog via the global application config file (not your profile).");
                PlsShown(true);
            }
        }

        private static void PlsShown(bool value, bool save = true)
        {
            //set the settings file in memory (not to disk)
            Properties.Settings.Default.PLSShown = value;

            //whether or not to commit to the settings file
            if (save)
                Properties.Settings.Default.Save();
        }

        private void DgvServers_SelectionChanged(object sender, EventArgs e)
            => itmConnect.Enabled =
                dgvServers.SelectedRows.Count == 1;

        private void DgvServers_DoubleClick(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count == 1)
                DoConnect();
        }

        private void ServerManager_Load(object sender, EventArgs e)
        {
            try
            {
                //default status update
                SetInterfaceAuthenticationStatus(false);

                //check if there's an internet connection first; it may have been disconnected while the window was closed.
                if (Internet.IsConnected)
                {
                    //check to see if a loaded profile instated some valid server details. If there are, we can potentially fast-forward
                    //the connection process!
                    ProfileDefinedServer();

                    // this check must be done before checking the count, because if it is null, we'll get an error for trying to access "Count" on a null object.
                    if (ObjectProvider.PlexServers == null) return;
                    if (ObjectProvider.PlexServers.Count <= 0) return;

                    RenderServersView(ObjectProvider.PlexServers);
                    SelectServer();

                    itmLoad.Enabled = true;
                    itmDisplay.Enabled = true;

                    //status update
                    SetInterfaceAuthenticationStatus(true);
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
                if (Internet.IsConnected)
                {
                    var auth = AuthRoutine.GetAuthToken();

                    var r = auth.Result;

                    switch (r)
                    {
                        case PlexAPI.LoginHandler.Auth.Enums.AuthStatus.Success:
                            if (ApplyToken(auth.Token))
                            {
                                UIMessages.Info(
                                    @"Successfully connected to Plex.tv. You can now load and connect to your servers/relays.",
                                    @"Success");
                                LoadServers(true);

                                //status update
                                SetInterfaceAuthenticationStatus(true);
                            }
                            else

                                //alert the user to the error
                                UIMessages.Error(@"An unknown error occurred; we couldn't apply your account token.");

                            break;

                        case PlexAPI.LoginHandler.Auth.Enums.AuthStatus.Cancelled: //nothing
                            break;

                        case PlexAPI.LoginHandler.Auth.Enums.AuthStatus.Failed: //alert the user to the failure
                            UIMessages.Error(
                                @"Failed to apply your account token; the ticket authority didn't authorise the transaction or values were not valid.");
                            break;

                        case PlexAPI.LoginHandler.Auth.Enums.AuthStatus.Error: //alert the user to the error
                            UIMessages.Error(@"An unknown error occurred; we couldn't apply your account token.");
                            break;

                        case PlexAPI.LoginHandler.Auth.Enums.AuthStatus.Invalid: //alert the user
                            UIMessages.Error(@"Failed to apply your account token; details were invalid.");
                            break;

                        case PlexAPI.LoginHandler.Auth.Enums.AuthStatus.IncorrectResponse:
                            UIMessages.Error(
                                @"Failed to get an authentication ticket from Plex.tv; an incorrectly formatted response was received.");
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
                //log the error
                LoggingHelpers.RecordException(ex.Message, "ConnectionError");

                //alert the user to the error
                UIMessages.Error("Connection Error:\n\n" + ex, @"Connection Error");
            }
        }

        private void ItmViaToken_Click(object sender, EventArgs e)
        {
            try
            {
                //check if there's a connection before trying to update the authentication token
                if (Internet.IsConnected)

                    //new token input form
                    using (var frm = new Authenticate())
                    {
                        //construct a new authentication information descriptor
                        var existingInfo = new ConnectionInfo
                        {
                            PlexAccountToken = ObjectProvider.Settings.ConnectionInfo.PlexAccountToken
                        };

                        //apply the descriptor
                        frm.ConnectionInfo = existingInfo;

                        //show the dialog and ensure 'OK' was pressed
                        if (frm.ShowDialog() != DialogResult.OK)
                            return;

                        //exit if the attempt was unsuccessful
                        if (!frm.Success)
                            return;

                        //apply the new token
                        if (ApplyToken(frm.ConnectionInfo.PlexAccountToken))
                        {
                            //alert the user to the status
                            UIMessages.Info(
                                @"Token applied successfully. You can now load servers and relays from Plex.tv");

                            //render the servers view
                            LoadServers(true);

                            //status update
                            SetInterfaceAuthenticationStatus(true);
                        }
                        else

                            //alert the user to the error
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
                //log the error
                LoggingHelpers.RecordException(ex.Message, "ConnectionError");

                //alert the user to the error
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
                if (Internet.IsConnected)
                {
                    ObjectProvider.User.authenticationToken = token;
                    ObjectProvider.Settings.ConnectionInfo.PlexAccountToken = token;
                    itmLoad.Enabled = true;
                    itmDisplay.Enabled = true;
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
            itmLoad.Enabled = false;
            itmDisplay.Enabled = false;
        }

        private void ItmRenderTokenColumn_Click(object sender, EventArgs e)
        {
            RenderTokenColumn = itmRenderTokenColumn.Checked;
            if (ObjectProvider.PlexServers != null)
                RenderServersView(ObjectProvider.PlexServers);
        }

        private void CxtServers_Opening(object sender, CancelEventArgs e)
        {
            if (!Flags.IsDebug || dgvServers.SelectedRows.Count == 0)
                e.Cancel = true;
        }

        private void ItmViewLink_Click(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count == 1)
                LinkViewer.ShowLinkViewer(ConnectionLink(CurrentServer()), false);
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