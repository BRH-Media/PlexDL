using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Caching;
using PlexDL.Common.Caching.Handlers;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Renderers.DGVRenderers;
using PlexDL.Common.Structures;
using PlexDL.PlexAPI;
using PlexDL.WaitWindow;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            if (ServerCaching.ServerInCache(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) &&
                GlobalStaticVars.Settings.CacheSettings.Mode.EnableServerCaching)
            {
                e.Result = ServerCaching.ServerFromCache(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken);
            }
            else
            {
                var result = GlobalStaticVars.Plex.GetServers(GlobalStaticVars.User);
                if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableServerCaching)
                    ServerCaching.ServerToCache(result, GlobalStaticVars.User.authenticationToken);
                e.Result = result;
            }
        }

        private static void GetRelaysListWorker(object sender, WaitWindowEventArgs e)
        {
            var result = Relays.GetServerRelays(GlobalStaticVars.User.authenticationToken);
            e.Result = result;
        }

        private void LoadServers(bool silent = false)
        {
            try
            {
                //check if there's a connection before trying to contact Plex.tv
                if (wininet.CheckForInternetConnection())
                {
                    if (!IsTokenSet()) return;

                    var serversResult = WaitWindow.WaitWindow.Show(GetServerListWorker, "Fetching Servers");
                    var servers = (List<Server>)serversResult;
                    if (servers.Count == 0)
                    {
                        if (!silent)
                            MessageBox.Show(@"No servers found for current account token. Please update your token or try a direct connection.",
                                @"Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        GlobalStaticVars.PlexServers = servers;
                        RenderServersView(servers);
                        itmConnect.Enabled = true;
                    }
                }
                else
                {
                    // trying to connect on no connection will not end well; alert the user.
                    if (!silent)
                        MessageBox.Show(@"No internet connection. Please connect to a network before attempting to load servers.", @"Network Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                    MessageBox.Show("Server retrieval error\n\n" + ex, @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "ServerGetError");
            }
        }

        private void LoadRelays(bool silent = false)
        {
            try
            {
                //check if there's a connection before trying to contact Plex.tv
                if (wininet.CheckForInternetConnection())
                {
                    if (!IsTokenSet()) return;

                    var serversResult = WaitWindow.WaitWindow.Show(GetRelaysListWorker, "Fetching Relays");
                    var servers = (List<Server>)serversResult;
                    if (servers.Count == 0)
                    {
                        if (!silent)
                            MessageBox.Show(@"No relays found for current account token. Please update your token or try a direct connection.",
                                @"Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        GlobalStaticVars.PlexServers = servers;
                        RenderServersView(servers);
                        itmConnect.Enabled = true;
                    }
                }
                else
                {
                    // trying to connect on no connection will not end well; alert the user.
                    if (!silent)
                        MessageBox.Show(@"No internet connection. Please connect to a network before attempting to load relays.", @"Network Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                    MessageBox.Show("Relay retrieval error\n\n" + ex, @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "RelayGetError");
            }
        }

        private Server CurrentServer()
        {
            var index = dgvServers.CurrentCell.RowIndex;
            return GlobalStaticVars.PlexServers[index];
        }

        private void DoConnect()
        {
            try
            {
                if (wininet.CheckForInternetConnection())
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
                        GlobalStaticVars.Svr = s;
                        GlobalStaticVars.User.authenticationToken = s.accessToken;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        if (testUrl.LastException != null)
                        {
                            MessageBox.Show(
                                    testUrl.StatusCode != "Undetermined"
                                    ? $@"Connection failed. The server result was: {testUrl.StatusCode}"
                                    : $@"Connection failed. The error was: {testUrl.LastException.Message}",
                                @"Network Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LoggingHelpers.RecordException(testUrl.LastException.Message, @"TestConnectionError");
                        }
                        else
                            MessageBox.Show($@"Couldn't connect to ""{s.address}:{s.port}""", @"Network Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // trying to connect on no connection will not end well; alert the user.
                    MessageBox.Show(@"No internet connection. Please connect to a network before attempting to start a Plex server connection.",
                        @"Network Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"ConnectionError");
                MessageBox.Show("Server connection attempt failed\n\n" + ex, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                var testUrl = WebCheck.TestUrl(uri);

                value = testUrl;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordGenericEntry($"Couldn't connect to \"{svr.address}:{svr.port}\"");
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
            var servers = new List<Server>();
            using (var frmDir = new DirectConnect())
            {
                frmDir.ConnectionInfo.PlexAccountToken = GlobalStaticVars.User.authenticationToken;
                frmDir.LoadLocalLink = localLink;

                if (frmDir.ShowDialog() != DialogResult.OK) return;
                if (!frmDir.Success) return;

                GlobalStaticVars.Settings.ConnectionInfo = frmDir.ConnectionInfo;
                GlobalStaticVars.User.authenticationToken = frmDir.ConnectionInfo.PlexAccountToken;
                var s = new Server
                {
                    accessToken = GlobalStaticVars.User.authenticationToken,
                    address = GlobalStaticVars.Settings.ConnectionInfo.PlexAddress,
                    port = GlobalStaticVars.Settings.ConnectionInfo.PlexPort,
                    name = "DirectConnect"
                };
                servers.Add(s);
                GlobalStaticVars.PlexServers = servers;
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
                if (GlobalStaticVars.Svr != null)
                {
                    var svrIndex = GlobalStaticVars.PlexServers.IndexOf(GlobalStaticVars.Svr);
                    dgvServers.Rows[svrIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                //log then ignore the error
                LoggingHelpers.RecordException(ex.Message, "SvrIndexingError");
            }
        }

        private void ServerManager_Load(object sender, EventArgs e)
        {
            try
            {
                //check if there's an internet connection first; it may have been disconnected while the window was closed.
                if (wininet.CheckForInternetConnection())
                {
                    // this check must be done before checking the count, because if it is null, we'll get an error for trying to access "Count" on a null object.
                    if (GlobalStaticVars.PlexServers != null)
                        if (GlobalStaticVars.PlexServers.Count > 0)
                        {
                            RenderServersView(GlobalStaticVars.PlexServers);
                            SelectServer();

                            itmConnect.Enabled = true;
                            itmClearServers.Enabled = true;
                            itmLoad.Enabled = true;
                        }
                }
                else
                {
                    // trying to connect on no connection will not end well; close this window if there's no connection and alert the user.
                    MessageBox.Show(@"No internet connection. Please connect to a network before attempting to start a Plex server connection.",
                        @"Network Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"ServerManagerLoadError");
                MessageBox.Show("Server manager failed to load necessary data\n\n" + ex, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void itmDirectConnection_Click(object sender, EventArgs e)
        {
            if (IsTokenSet())
                RunDirectConnect(false);
        }

        private void itmLocalLink_Click(object sender, EventArgs e)
        {
            if (IsTokenSet())
                RunDirectConnect(true);
        }

        private void itmRelays_Click(object sender, EventArgs e)
        {
            if (IsTokenSet())
                LoadRelays();
        }

        private void itmServers_Click(object sender, EventArgs e)
        {
            if (IsTokenSet())
                LoadServers();
        }

        private void itmViaPlexTv_Click(object sender, EventArgs e)
        {
            try
            {
                //check if there's a connection before trying to update the authentication token
                if (wininet.CheckForInternetConnection())
                    using (var frm = new PlexLogin())
                    {
                        if (frm.ShowDialog() != DialogResult.OK) return;
                        if (!frm.Success) return;

                        if (ApplyToken(frm.AccountToken))
                            LoadServers(true);
                        else
                            MessageBox.Show(@"An unknown error occurred", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                else
                    // trying to connect on no connection will not end well; alert the user.
                    MessageBox.Show(@"No internet connection. Please connect to a network before attempting to authenticate.", @"Network Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ConnectionError");
                MessageBox.Show("Connection Error:\n\n" + ex, @"Connection Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void itmViaToken_Click(object sender, EventArgs e)
        {
            try
            {
                //check if there's a connection before trying to update the authentication token
                if (wininet.CheckForInternetConnection())
                    using (var frm = new Authenticate())
                    {
                        var existingInfo = new ConnectionInfo
                        {
                            PlexAccountToken = GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken
                        };
                        frm.ConnectionInfo = existingInfo;

                        if (frm.ShowDialog() != DialogResult.OK) return;
                        if (!frm.Success) return;

                        if (ApplyToken(frm.ConnectionInfo.PlexAccountToken))
                        {
                            MessageBox.Show(@"Token applied successfully. You can now load servers and relays from Plex.tv", @"Message",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadServers(true);
                        }
                        else
                            MessageBox.Show(@"An unknown error occurred", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                else
                    // trying to connect on no connection will not end well; alert the user.
                    MessageBox.Show(@"No internet connection. Please connect to a network before attempting to authenticate.", @"Network Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ConnectionError");
                MessageBox.Show("Connection Error:\n\n" + ex, @"Connection Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static bool IsTokenSet()
        {
            return !string.IsNullOrEmpty(GlobalStaticVars.User.authenticationToken);
        }

        private bool ApplyToken(string token, bool silent = true)
        {
            try
            {
                //check if there's a connection before trying to update the authentication token
                if (wininet.CheckForInternetConnection())
                {
                    GlobalStaticVars.User.authenticationToken = token;
                    GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken = token;
                    itmLoad.Enabled = true;
                    itmClearServers.Enabled = true;
                    dgvServers.DataSource = null;
                    if (!silent)
                        MessageBox.Show(@"Token applied successfully. You can now load servers and relays from Plex.tv", @"Message",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }

                // trying to connect on no connection will not end well; alert the user.
                if (!silent)
                    MessageBox.Show(@"No internet connection. Please connect to a network before attempting to authenticate.", @"Network Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ConnectionError");
                if (!silent)
                    MessageBox.Show("Connection Error:\n\n" + ex, @"Connection Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return false;
            }
        }

        private void itmClearServers_Click(object sender, EventArgs e)
        {
            dgvServers.DataSource = null;
            //force a repaint
            dgvServers.Invalidate();
            GlobalStaticVars.PlexServers = null;
            itmConnect.Enabled = false;
            itmLoad.Enabled = false;
            itmClearServers.Enabled = false;
        }

        private void itmRenderTokenColumn_Click(object sender, EventArgs e)
        {
            RenderTokenColumn = itmRenderTokenColumn.Checked;
            if (GlobalStaticVars.PlexServers != null)
                RenderServersView(GlobalStaticVars.PlexServers);
        }

        private void cxtServers_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Flags.IsDebug || dgvServers.SelectedRows.Count == 0)
                e.Cancel = true;
        }

        private void itmViewLink_Click(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count == 1)
                MessageBox.Show(ConnectionLink(CurrentServer()), @"Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}