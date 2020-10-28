using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Globals;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.PlayerLaunchers;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.Plex;
using PlexDL.Player;
using PlexDL.Properties;
using PlexDL.WaitWindow;
using System;
using System.Windows.Forms;
using UIHelpers;

// ReSharper disable ArrangeThisQualifier

#pragma warning disable 1591

namespace PlexDL.UI
{
    public partial class Player : Form
    {
        private PlexDL.Player.Player _mPlayer;

        private bool _isWmp = ObjectProvider.Settings.Player.ForceWmpMode;

        public PlexObject StreamingContent { get; set; }

        public Player()
        {
            InitializeComponent();
        }

        //double-buffering override for the entire form
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; //turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void SwitchWmp(bool pvsSetup = true)
        {
            //PVS
            tlpMaster.Enabled = !_isWmp;
            tlpMaster.Visible = !_isWmp;

            //WMP COM
            wmpMain.Enabled = _isWmp;
            wmpMain.Visible = _isWmp;

            //PVS setup
            if (!_isWmp && pvsSetup)
            {
                //PVS setup
                _mPlayer = new PlexDL.Player.Player();

                //events and slider configuration
                _mPlayer.Sliders.Position.TrackBar = trkDuration;
                _mPlayer.Sliders.AudioVolume = trkVolume;
                _mPlayer.Events.MediaPositionChanged += MPlayer_MediaPositionChanged;
                _mPlayer.Events.MediaEnded += MPlayer_ContentFinished;
                _mPlayer.Events.MediaStarted += MPlayer_ContentStarted;

                //render area setup
                _mPlayer.Display.Mode = DisplayMode.ZoomCenter;
                _mPlayer.Display.Window = pnlPlayer;

                //fix parent
                _mPlayer.FS_ResetDisplayParent();
            }
            else if (_isWmp)
                wmpMain.URL = StreamingContent.StreamInformation.Links.View;
        }

        private void FrmPlayer_Load(object sender, EventArgs e)
        {
            var formTitle = StreamingContent.StreamInformation.ContentTitle;
            Text = formTitle ?? "Player";
            //player.URL = StreamingContent.StreamUrl;

            if (!PlexDL.Player.Player.MFPresent)
            {
                UIMessages.Error(
                    @"MediaFoundation is not installed. The player will not be able to stream the selected content; reverting to WMP mode.",
                    @"Playback Error");
                _isWmp = true;
            }

            if (StreamingContent.StreamInformation.Container == "mkv")
            {
                var msg =
                    UIMessages.Question(
                        @"PlexDL Matroska (mkv) playback is not supported. Would you like to open the file in VLC Media Player? Note: It must already be installed");

                if (msg)
                    try
                    {
                        VlcLauncher.LaunchVlc(StreamingContent);
                        Close();
                    }
                    catch (Exception ex)
                    {
                        UIMessages.Error("Error occurred whilst trying to launch VLC\n\n" + ex,
                            @"Launch Error");
                        LoggingHelpers.RecordException(ex.Message, "VLCLaunchError");
                        Close();
                    }
                else
                    Close();
            }

            //decide appropriate player
            SwitchWmp();

            //UIMessages.Info(TitlesTable.Rows.Count + "\n" +StreamingContent.StreamIndex);
            //UIMessages.Info("Duration: "+StreamingContent.ContentDuration+"\nSize: "+StreamingContent.ByteLength);
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
            //hotkeys are disabled during Windows Media Player mode
            if (_isWmp) return base.ProcessCmdKey(ref msg, keyData);

            if (keyData == ObjectProvider.Settings.Player.KeyBindings.PlayPause)
            {
                PlayPause();
                return true;
            }

            if (keyData == ObjectProvider.Settings.Player.KeyBindings.SkipForward)
            {
                SkipForward();
                return true;
            }

            if (keyData == ObjectProvider.Settings.Player.KeyBindings.SkipBackward)
            {
                SkipBack();
                return true;
            }

            if (keyData == ObjectProvider.Settings.Player.KeyBindings.NextTitle)
            {
                Stop();
                NextTitle();
                return true;
            }

            if (keyData == ObjectProvider.Settings.Player.KeyBindings.PrevTitle)
            {
                Stop();
                PrevTitle();
                return true;
            }

            if (keyData == ObjectProvider.Settings.Player.KeyBindings.FullscreenToggle)
            {
                ToggleFullscreen();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrmPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            wmpMain?.Dispose();
            _mPlayer?.Dispose();
        }

        private void MPlayer_ContentFinished(object sender, EventArgs e)
        {
            if (!ObjectProvider.Settings.Player.PlayNextTitleAutomatically)
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

        private static void MPlayer_ContentStarted(object sender, EventArgs e)
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

        private void Stop()
        {
            if (!_mPlayer.Playing && !_mPlayer.Paused) return;
            _mPlayer.Stop();
            _mPlayer.Paused = false;
            btnStop.Enabled = false;
            SetIconPlay();

            //tooltip set
            tipMain.SetToolTip(btnPlayPause, @"Play Title");
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
            //multi-threaded wholesomeness
            if (pnlPlayer.InvokeRequired)
            {
                //multi-threaded goodness
                pnlPlayer.BeginInvoke((MethodInvoker)delegate
               {
                   //invoke myself again, but on the UI thread this time
                   StartPlayer(fileName);
               });
            }
            else
            {
                //weird workaround suggested by Peter
                this.Width++; this.Width--;

                //start playback
                _mPlayer.Play(fileName);
            }
        }

        private void PlayWorker(object sender, WaitWindowEventArgs e)
        {
            if (_mPlayer.Playing) return;

            if (Methods.RemoteFileExists(StreamingContent.StreamInformation.Links.View))
            {
                var fileName = (string)e.Arguments[0];

                StartPlayer(fileName);

                SetIconPause();

                //tooltip set
                tipMain.SetToolTip(btnPlayPause, @"Pause Playback");

                //stop button enable
                btnStop.Enabled = true;
            }
            else
                UIMessages.Error(
                    @"Couldn't load the stream because the remote file doesn't exist or returned an error",
                    @"Network Error");
        }

        private void ToggleFullscreen()
        {
            var fsStatus = _mPlayer.FullScreen;

            if (fsStatus)
                SetIconOpenFs();
            else
                SetIconExitFs();

            Fullscreen(!fsStatus);
        }

        private void Fullscreen(bool t)
        {
            _mPlayer.FullScreenMode = FullScreenMode.Form;
            _mPlayer.FullScreen = t;
        }

        private static void GetObjectFromIndexCallback(object sender, WaitWindowEventArgs e)
        {
            var index = (int)e.Arguments[0];
            e.Result = GetObjectFromIndexWorker(index);
        }

        private static PlexMovie GetObjectFromIndex(int index)
        {
            var result = (PlexMovie)WaitWindow.WaitWindow.Show(GetObjectFromIndexCallback, "Getting Metadata", index);
            return result;
        }

        private static PlexMovie GetObjectFromIndexWorker(int index)
        {
            var obj = new PlexMovie();

            var dlInfo = GetContentDownloadInfo(index);

            obj.StreamInformation = dlInfo;
            obj.StreamIndex = index;
            return obj;
        }

        private static StreamInfo GetContentDownloadInfo(int index)
        {
            if (index + 1 <= TableManager.ReturnCorrectTable().Rows.Count)
            {
                var result = TableManager.ReturnCorrectTable().Rows[index];
                var key = result["key"].ToString();
                var baseUri = Strings.GetBaseUri(false);
                key = key.TrimStart('/');
                var uri = baseUri + key + "/?X-Plex-Token=";

                var reply = XmlGet.GetXmlTransaction(uri, ObjectProvider.Settings.ConnectionInfo.PlexAccountToken);

                var obj = DownloadInfoGatherers.GetContentDownloadInfo(reply);
                return obj;
            }

            UIMessages.Error(@"Index was higher than row count; could not process data.",
                @"Indexing Error");
            return new StreamInfo();
        }

        private void NextTitle()
        {
            //check if the table has been loaded correctly before trying
            //to get data from it.
            if (!TableManager.ActiveTableFilled) return;

            if (StreamingContent.StreamIndex + 1 < TableManager.ReturnCorrectTable().Rows.Count)
            {
                var next = GetObjectFromIndex(StreamingContent.StreamIndex + 1);
                StreamingContent = next;
                var formTitle = StreamingContent.StreamInformation.ContentTitle;
                Text = formTitle;
                Refresh();
                //UIMessages.Info(StreamingContent.StreamIndex + "\n" + TitlesTable.Rows.Count);
            }
            else if (StreamingContent.StreamIndex + 1 == TableManager.ReturnCorrectTable().Rows.Count)
            {
                var next = GetObjectFromIndex(0);
                StreamingContent = next;
                var formTitle = StreamingContent.StreamInformation.ContentTitle;
                Text = formTitle;
                Refresh();
                //UIMessages.Info(StreamingContent.StreamIndex + "\n" + TitlesTable.Rows.Count);
            }
        }

        private void PrevTitle()
        {
            //check if the table has been loaded correctly before trying
            //to get data from it.
            if (!TableManager.ActiveTableFilled) return;

            if (StreamingContent.StreamIndex != 0)
            {
                var next = GetObjectFromIndex(StreamingContent.StreamIndex - 1);
                StreamingContent = next;
                var formTitle = StreamingContent.StreamInformation.ContentTitle;
                Text = formTitle;
                Refresh();
                //UIMessages.Info(StreamingContent.StreamIndex + "\n" + TitlesTable.Rows.Count);
            }
            else
            {
                var next = GetObjectFromIndex(TableManager.ReturnCorrectTable().Rows.Count - 1);
                StreamingContent = next;
                var formTitle = StreamingContent.StreamInformation.ContentTitle;
                Text = formTitle;
                Refresh();
                //UIMessages.Info(StreamingContent.StreamIndex + "\n" + TitlesTable.Rows.Count);
            }
        }

        private void SkipBack()
        {
            if (_mPlayer.Playing)
            {
                var rewindAmount = ObjectProvider.Settings.Player.SkipBackwardInterval * -1;

                _mPlayer.Position.Skip((int)rewindAmount);
            }
        }

        private void SkipForward()
        {
            if (_mPlayer.Playing)
            {
                var stepAmount = ObjectProvider.Settings.Player.SkipForwardInterval;

                _mPlayer.Position.Skip((int)stepAmount);
            }
        }

        private void Resume()
        {
            if (_mPlayer.Playing && _mPlayer.Paused)
            {
                _mPlayer.Resume();
                SetIconPause();

                //tooltip set
                tipMain.SetToolTip(btnPlayPause, @"Pause Playback");
            }
        }

        private void Pause()
        {
            if (_mPlayer.Playing && !_mPlayer.Paused)
            {
                _mPlayer.Pause();
                SetIconPlay();

                //tooltip set
                tipMain.SetToolTip(btnPlayPause, @"Resume Playback");
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnPlayPause_Click(object sender, EventArgs e)
        {
            PlayPause();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void BtnPrevTitle_Click(object sender, EventArgs e)
        {
            Stop();
            PrevTitle();
        }

        private void BtnNextTitle_Click(object sender, EventArgs e)
        {
            Stop();
            NextTitle();
        }

        private void BtnSkipForward_Click(object sender, EventArgs e)
        {
            SkipForward();
        }

        private void BtnSkipBack_Click(object sender, EventArgs e)
        {
            SkipBack();
        }

        private void BtnFullScreen_Click(object sender, EventArgs e)
        {
            ToggleFullscreen();
        }

        private void PlayPause()
        {
            if (_mPlayer.Playing && !_mPlayer.Paused)
                Pause();
            else
            {
                if (_mPlayer.Paused)
                    Resume();
                else
                    Play(StreamingContent.StreamInformation.Links.View);
            }
        }

        private void SetIconExitFs()
        {
            if (btnFullScreen.InvokeRequired)
                btnFullScreen.BeginInvoke((MethodInvoker)delegate
               {
                   btnFullScreen.BackgroundImage = Resources.baseline_fullscreen_exit_black_18dp;
               });
            else
                btnFullScreen.BackgroundImage = Resources.baseline_fullscreen_exit_black_18dp;
        }

        private void SetIconOpenFs()
        {
            if (btnFullScreen.InvokeRequired)
                btnFullScreen.BeginInvoke((MethodInvoker)delegate
               {
                   btnFullScreen.BackgroundImage = Resources.baseline_fullscreen_black_18dp;
               });
            else
                btnFullScreen.BackgroundImage = Resources.baseline_fullscreen_black_18dp;
        }

        private void TrkVolume_Scroll(object sender, EventArgs e)
        {
            //update volume label
            lblVolume.Text = $@"{trkVolume.Value}%";
        }
    }
}