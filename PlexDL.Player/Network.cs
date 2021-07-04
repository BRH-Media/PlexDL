using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Network methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Network : HideObjectMembers
    {
        #region Fields (Network Class)

        private Player _base;

        #endregion
        
        internal Network(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets statistical network information about the playing media (HTTP not supported).
        /// </summary>
        /// <param name="statistics">Specifies the statistics to be obtained, for example NetworkStatistics.DownloadProgress.</param>
        public long GetStatistics(NetworkStatistics statistics)
        {
            long result = 0;

            if (_base.mf_MediaSession == null) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            else
            {
                try
                {
                    _base._lastError = MFExtern.MFGetService(_base.mf_MediaSession, MFServices.MFNETSOURCE_STATISTICS_SERVICE, typeof(IPropertyStore).GUID, out object store);
                    if (_base._lastError == Player.NO_ERROR)
                    {
                        PropertyKeys.PKEY_NetSource_Statistics.pID = (int)statistics;
                        PropVariant data = new PropVariant();

                        ((IPropertyStore)store).GetValue(PropertyKeys.PKEY_NetSource_Statistics, data);
                        if (statistics == NetworkStatistics.BytesReceived || statistics == NetworkStatistics.SeekRangeStart || statistics == NetworkStatistics.SeekRangeEnd) result = data.GetLong();
                        else result = data.GetInt();

                        data.Dispose();
                        Marshal.ReleaseComObject(store);
                    }
                }
                catch { /* ignored */ }
            }
            return result;
        }

        /// <summary>
        /// Gets or sets a value that indicates the player's low latency network mode (default: false). This property also applies to local media playback and is reset (to false) when media ends playing.
        /// </summary>
        public bool LowLatency
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.mf_LowLatency;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                if (value != _base.mf_LowLatency)
                {
                    _base.mf_LowLatency = value;
                    if (_base._playing) _base.AV_UpdateTopology();
                }
            }
        }

        /*
        bool mf_NetCredentialEnabled;
        bool _hasNetCredentialManager;
        bool _hasHetCredentialCache;
        IMFNetCredentialManager mf_NetCredentialManager;
        IMFNetCredentialCache mf_NetCredentialCache;
        //IMFNetCredential        mf_NetCredential;

        /// <summary>
        /// Gets or sets a value that indicates whether the player's network authentication is enabled (this player only - default: false). Please note: Credentials might be sent in clear text.
        /// </summary>
        public bool EnableAuthentication
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return mf_NetCredentialEnabled;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                mf_NetCredentialEnabled = value;
            }
        }

        /// <summary>
        /// Adds the specified credentials to the network authentication credential cache used by all players (with authentication enabled) in this assembly. See also: Player.Network.EnableAuthentication.
        /// </summary>
        /// <param name="url">The URL for which the authentication is needed.</param>
        /// <param name="realm">The realm for the authentication.</param>
        /// <param name="userName">The user name for the authentication.</param>
        /// <param name="password">The password for the authentication.</param>
        public int AddCredentials(string url, string realm, string userName, string password)
        {
            return AddCredentials(url, realm, userName, password, AuthenticationFlags.None, true);
        }

        /// <summary>
        /// Adds the specified credentials to the network authentication credential cache used by all players (with authentication enabled) in this assembly. See also: Player.Network.EnableAuthentication.
        /// </summary>
        /// <param name="url">The URL for which the authentication is needed.</param>
        /// <param name="realm">The realm for the authentication.</param>
        /// <param name="userName">The user name for the authentication.</param>
        /// <param name="password">The password for the authentication.</param>
        /// <param name="flags">Specifies how the credentials will be used (default: AuthenticationFlags.None).</param>
        /// <param name="allowClearText">Specifies whether the credentials can be sent over the network in plain text when requested (default: true).</param>
        public int AddCredentials(string url, string realm, string userName, string password, AuthenticationFlags flags, bool allowClearText)
        {


            return 0;
        }

        public void GetCredentials(string url, string realm, out string userName, out string password, out AuthenticationFlags flags, out bool allowClearText)
        {
            userName = string.Empty;
            password = string.Empty;
            flags = AuthenticationFlags.None;
            allowClearText = true;
        }

        public void RemoveCredentials(string url, string realm)
        {
        }

        /// <summary>
        /// Net credentials test method.
        /// </summary>
        public void CredentialsTest()
        {
            HResult result = MFExtern.MFCreateCredentialCache(out mf_NetCredentialCache);
            if (result == Player.NO_ERROR)
            {
                IMFNetCredential _netCredential;
                MFNetCredentialRequirements _netCredentialRequirements;
                result = mf_NetCredentialCache.GetCredential("https://CodeProject.com", string.Empty, MFNetAuthenticationFlags.None, out _netCredential, out _netCredentialRequirements);
                MessageBox.Show("Create Credential: " + _base.GetErrorString((int)result));

                byte[] userName = Encoding.ASCII.GetBytes("Peter Vegter" + '\0');
                byte[] password = Encoding.ASCII.GetBytes("Peter1234" + '\0');

                result = _netCredential.SetUser(userName, userName.Length, false);
                if (result == Player.NO_ERROR) result = _netCredential.SetPassword(password, password.Length, false);
                MessageBox.Show("Set User: " + _base.GetErrorString((int)result));

                if (result == Player.NO_ERROR)
                {
                    IMFNetCredential testCredential;
                    result = mf_NetCredentialCache.GetCredential("https://CodeProject.com", string.Empty, MFNetAuthenticationFlags.None, out testCredential, out _netCredentialRequirements);
                    MessageBox.Show("Get Credential: " + _base.GetErrorString((int)result));
                    if (result == Player.NO_ERROR)
                    {
                        byte[] testName = new byte[256];
                        MFInt testData = new MFInt(256);
                        bool testEncrypted = false;
                        result = testCredential.GetUser(testName, testData, testEncrypted);
                        MessageBox.Show("Get User: " + _base.GetErrorString((int)result));

                        if (result == Player.NO_ERROR)
                        {
                            if (testName == null)
                            {
                                MessageBox.Show("User Name: is null");
                            }
                            else
                            {
                                //string testUserName = testName.ToString();
                                string testUserName = Encoding.UTF8.GetString(testName, 0, testName.Length);
                                MessageBox.Show("User Name: " + testUserName);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Create CredentialCache: " + _base.GetErrorString((int)result));
            }

        }
        */
    }
}