using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.PlayerLaunchers;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.Plex;
using PlexDL.Player;
using PlexDL.Properties;
using PlexDL.WaitWindow;
using System;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace PlexDL.UI
{
    public partial class Player : Form
    {
        public bool CanFadeOut = true;

        public bool IsFullScreen = false;

        public bool IsWmp = false;

        private PlexDL.Player.Player _mPlayer;

        public string PlayingPosition, Duration;
        public Timer T1 = new Timer();

        public DataTable TitlesTable;

        public Player()
        {
            InitializeComponent();
        }

        public PlexObject StreamingContent { get; set; }

        private void FrmPlayer_Load(object sender, EventArgs e)
        {
            var formTitle = StreamingContent.StreamInformation.ContentTitle;
            Text = formTitle;
            //player.URL = StreamingContent.StreamUrl;

            if (!PlexDL.Player.Player.MFPresent)
            {
                MessageBox.Show(@"MediaFoundation is not installed. The player will not be able to stream the selected content :(", @"System Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CanFadeOut = false;
                Close();
            }

            if (StreamingContent.StreamInformation.Container == "mkv")
            {
                CanFadeOut = false;
                var msg =
                MessageBox.Show(
                    @"PlexDL Matroska (mkv) playback is not supported. Would you like to open the file in VLC Media Player? Note: It must already be installed",
                    @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msg == DialogResult.No)
                    Close();
                else
                    try
                    {
                        VlcLauncher.LaunchVlc(StreamingContent);
                        Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occurred whilst trying to launch VLC\n\n" + ex, @"Launch Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        LoggingHelpers.RecordException(ex.Message, "VLCLaunchError");
                        Close();
                    }
            }

            _mPlayer = new PlexDL.Player.Player(pnlPlayer);
            _mPlayer.Sliders.Position.TrackBar = trkDuration;
            _mPlayer.Events.MediaPositionChanged += MPlayer_MediaPositionChanged;
            _mPlayer.Events.MediaEnded += MPlayer_ContentFinished;
            _mPlayer.Events.MediaStarted += MPlayer_ContentStarted;

            //MessageBox.Show(TitlesTable.Rows.Count + "\n" +StreamingContent.StreamIndex);
            //MessageBox.Show("Duration: "+StreamingContent.ContentDuration+"\nSize: "+StreamingContent.ByteLength);
        }

        /*
         * PLAYER HOT-KEYS:
         * RIGHT ARROW=Skip Forward (Interval)
         * LEFT ARROW=Skip Backward (Interval)
         * UP ARROW=Next Title Index
         * DOWN ARROW=Previous Title Index
         * SPACE=Play/Pause
         */

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!IsWmp)
            {
                if (keyData == GlobalStaticVars.Settings.Player.KeyBindings.PlayPause)
                {
                    PlayPause();
                    return true;
                }

                if (keyData == GlobalStaticVars.Settings.Player.KeyBindings.SkipForward)
                {
                    SkipForward();
                    return true;
                }

                if (keyData == GlobalStaticVars.Settings.Player.KeyBindings.SkipBackward)
                {
                    SkipBack();
                    return true;
                }

                if (keyData == GlobalStaticVars.Settings.Player.KeyBindings.NextTitle)
                {
                    Stop();
                    NextTitle();
                    return true;
                }

                if (keyData == GlobalStaticVars.Settings.Player.KeyBindings.PrevTitle)
                {
                    Stop();
                    PrevTitle();
                    return true;
                }

                if (keyData == Keys.Escape)
                    if (_mPlayer.FullScreen)
                        _mPlayer.FullScreen = false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrmPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_mPlayer != null)
                _mPlayer.Dispose();
        }

        private void MPlayer_ContentFinished(object sender, EventArgs e)
        {
            if (!GlobalStaticVars.Settings.Player.PlayNextTitleAutomatically)
            {
                SetIconPlay();
            }
            else
            {
                SetIconPlay();
                NextTitle();
                PlayPause();
            }
        }

        private void MPlayer_ContentStarted(object sender, EventArgs e)
        {
            //nothing just yet :)
        }

        private void MPlayer_MediaPositionChanged(object sender, PositionEventArgs e)
        {
            var fromStart = TimeSpan.FromTicks(e.FromStart);
            var toStop = TimeSpan.FromTicks(e.ToStop);

            lblTimeSoFar.Text = fromStart.ToString(@"hh\:mm\:ss");
            lblTotalDuration.Text = toStop.ToString(@"hh\:mm\:ss");
        }

        private void TmrCopied_Tick(object sender, EventArgs e)
        {
            //btnCopyLink.Text = "Copy Stream Link";
            tmrCopied.Stop();
        }

        private void Stop()
        {
            if (!_mPlayer.Playing) return;
            _mPlayer.Stop();
            _mPlayer.Paused = false;
            SetIconPlay();
        }

        private void Play(string fileName)
        {
            WaitWindow.WaitWindow.Show(PlayWorker, "Loading Stream", fileName);
        }

        private void SetIconPause()
        {
            btnPlayPause.BackgroundImage = Resources.baseline_pause_black_18dp;
        }

        private void SetIconPlay()
        {
            btnPlayPause.BackgroundImage = Resources.baseline_play_arrow_black_18dp;
        }

        private void StartPlayer(string fileName)
        {
            if (pnlPlayer.InvokeRequired)
            {
                var d = new SafePlayDelegate(StartPlayer);
                pnlPlayer.Invoke(d, fileName);
            }
            else
            {
                _mPlayer.Play(fileName);
            }
        }

        private void PlayWorker(object sender, WaitWindowEventArgs e)
        {
            if (_mPlayer.Playing) return;

            if (Methods.RemoteFileExists(StreamingContent.StreamInformation.Link))
            {
                var fileName = (string)e.Arguments[0];
                StartPlayer(fileName);
                SetIconPause();
            }
            else
            {
                if (InvokeRequired)
                    BeginInvoke((MethodInvoker)delegate
                    {
                        MessageBox.Show(@"Couldn't load the stream because the remote file doesn't exist or returned an error", @"Network Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                else
                    MessageBox.Show(@"Couldn't load the stream because the remote file doesn't exist or returned an error", @"Network Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetBaseUri(bool incToken)
        {
            if (incToken)
                return "http://" + GlobalStaticVars.Settings.ConnectionInfo.PlexAddress + ":" + GlobalStaticVars.Settings.ConnectionInfo.PlexPort +
                       "/?X-Plex-Token=";

            return "http://" + GlobalStaticVars.Settings.ConnectionInfo.PlexAddress + ":" + GlobalStaticVars.Settings.ConnectionInfo.PlexPort + "/";
        }

        private void GetObjectFromIndexCallback(object sender, WaitWindowEventArgs e)
        {
            var index = (int)e.Arguments[0];
            e.Result = GetObjectFromIndexWorker(index);
        }

        private PlexMovie GetObjectFromIndex(int index)
        {
            var result = (PlexMovie)WaitWindow.WaitWindow.Show(GetObjectFromIndexCallback, "Getting Metadata", index);
            return result;
        }

        private PlexMovie GetObjectFromIndexWorker(int index)
        {
            var obj = new PlexMovie();

            var dlInfo = GetContentDownloadInfo(index);

            obj.StreamInformation = dlInfo;
            obj.StreamIndex = index;
            return obj;
        }

        private DownloadInfo GetContentDownloadInfo(int index)
        {
            if (index + 1 <= TitlesTable.Rows.Count)
            {
                var result = TitlesTable.Rows[index];
                var key = result["key"].ToString();
                var baseUri = GetBaseUri(false);
                key = key.TrimStart('/');
                var uri = baseUri + key + "/?X-Plex-Token=";

                var reply = XmlGet.GetXmlTransaction(uri, GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken);

                var obj = GetContentDownloadInfo_Xml(reply);
                return obj;
            }

            MessageBox.Show(@"Index was higher than row count; could not process data.", @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return new DownloadInfo();
        }

        private DownloadInfo GetContentDownloadInfo_Xml(XmlDocument xml)
        {
            var obj = new DownloadInfo();

            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(xml));

            var part = sections.Tables["Part"];
            var video = sections.Tables["Video"].Rows[0];
            var title = video["title"].ToString();
            if (title.Length > GlobalStaticVars.Settings.Generic.DefaultStringLength)
                title = title.Substring(0, GlobalStaticVars.Settings.Generic.DefaultStringLength);
            var thumb = video["thumb"].ToString();
            var thumbnailFullUri = "";
            if (!string.IsNullOrEmpty(thumb))
            {
                var baseUri = GetBaseUri(false).TrimEnd('/');
                thumbnailFullUri = baseUri + thumb + "?X-Plex-Token=" + GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken;
            }

            var partRow = part.Rows[0];

            var filePart = partRow["key"].ToString();
            var container = partRow["container"].ToString();
            var byteLength = Convert.ToInt64(partRow["size"]);
            var contentDuration = Convert.ToInt32(partRow["duration"]);
            var fileName = Methods.RemoveIllegalCharacters(title + "." + container);

            var link = GetBaseUri(false).TrimEnd('/') + filePart + "/?X-Plex-Token=" + GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken;
            obj.Link = link;
            obj.Container = container;
            obj.ByteLength = byteLength;
            obj.ContentDuration = contentDuration;
            obj.FileName = fileName;
            obj.ContentTitle = title;
            obj.ContentThumbnailUri = thumbnailFullUri;

            return obj;
        }

        private void NextTitle()
        {
            if (StreamingContent.StreamIndex + 1 < TitlesTable.Rows.Count)
            {
                var next = GetObjectFromIndex(StreamingContent.StreamIndex + 1);
                StreamingContent = next;
                var formTitle = StreamingContent.StreamInformation.ContentTitle;
                Text = formTitle;
                Refresh();
                //MessageBox.Show(StreamingContent.StreamIndex + "\n" + TitlesTable.Rows.Count);
            }
            else if (StreamingContent.StreamIndex + 1 == TitlesTable.Rows.Count)
            {
                var next = GetObjectFromIndex(0);
                StreamingContent = next;
                var formTitle = StreamingContent.StreamInformation.ContentTitle;
                Text = formTitle;
                Refresh();
                //MessageBox.Show(StreamingContent.StreamIndex + "\n" + TitlesTable.Rows.Count);
            }
        }

        private void PrevTitle()
        {
            if (StreamingContent.StreamIndex != 0)
            {
                var next = GetObjectFromIndex(StreamingContent.StreamIndex - 1);
                StreamingContent = next;
                var formTitle = StreamingContent.StreamInformation.ContentTitle;
                Text = formTitle;
                Refresh();
                //MessageBox.Show(StreamingContent.StreamIndex + "\n" + TitlesTable.Rows.Count);
            }
            else
            {
                var next = GetObjectFromIndex(TitlesTable.Rows.Count - 1);
                StreamingContent = next;
                var formTitle = StreamingContent.StreamInformation.ContentTitle;
                Text = formTitle;
                Refresh();
                //MessageBox.Show(StreamingContent.StreamIndex + "\n" + TitlesTable.Rows.Count);
            }
        }

        private void SkipBack()
        {
            if (_mPlayer.Playing)
            {
                var rewindAmount = GlobalStaticVars.Settings.Player.SkipBackwardInterval * -1;

                _mPlayer.Position.Skip((int)rewindAmount);
            }
        }

        private void SkipForward()
        {
            if (_mPlayer.Playing)
            {
                var stepAmount = GlobalStaticVars.Settings.Player.SkipForwardInterval;

                _mPlayer.Position.Skip((int)stepAmount);
            }
        }

        private void Resume()
        {
            if (_mPlayer.Playing && _mPlayer.Paused)
            {
                _mPlayer.Resume();
                SetIconPause();
            }
        }

        private void Pause()
        {
            if (_mPlayer.Playing && !_mPlayer.Paused)
            {
                _mPlayer.Pause();
                SetIconPlay();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            PlayPause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void btnPrevTitle_Click(object sender, EventArgs e)
        {
            Stop();
            PrevTitle();
        }

        private void btnNextTitle_Click(object sender, EventArgs e)
        {
            Stop();
            NextTitle();
        }

        private void btnSkipForward_Click(object sender, EventArgs e)
        {
            SkipForward();
        }

        private void btnSkipBack_Click(object sender, EventArgs e)
        {
            SkipBack();
        }

        private void PlayPause()
        {
            if (_mPlayer.Playing && !_mPlayer.Paused)
            {
                Pause();
            }
            else
            {
                if (_mPlayer.Paused)
                    Resume();
                else
                    Play(StreamingContent.StreamInformation.Link);
            }
        }

        /*
         *
         * FUTURE FEATURE!
         *
        private void SetIconExitFS()
        {
            if (btnFullScreen.InvokeRequired)
            {
                btnFullScreen.BeginInvoke((MethodInvoker)delegate
                {
                    btnFullScreen.Icon = PlexDL.Properties.Resources.baseline_fullscreen_exit_black_18dp;
                });
            }
            else
            {
                btnFullScreen.Icon = PlexDL.Properties.Resources.baseline_fullscreen_exit_black_18dp;
            }
        }

        private void SetIconOpenFS()
        {
            if (btnFullScreen.InvokeRequired)
            {
                btnFullScreen.BeginInvoke((MethodInvoker)delegate
                {
                    btnFullScreen.Icon = PlexDL.Properties.Resources.baseline_fullscreen_black_18dp;
                });
            }
            else
            {
                btnFullScreen.Icon = PlexDL.Properties.Resources.baseline_fullscreen_black_18dp;
            }
        }
        */

        private delegate void SafePlayDelegate(string fileName);
    }
}