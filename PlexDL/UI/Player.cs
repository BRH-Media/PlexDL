using MetroSet_UI.Extensions;
using MetroSet_UI.Forms;
using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Logging;
using PlexDL.Common.PlayerLaunchers;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.Plex;
using System;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using PlexDL.Player;

namespace PlexDL.UI
{
    public partial class Player : MetroSetForm
    {
        public Timer t1 = new Timer();

        private PlexDL.Player.Player mPlayer;

        public PlexObject StreamingContent { get; set; }

        public DataTable TitlesTable;

        public string PlayingPosition, Duration;

        public bool CanFadeOut = true;

        public bool IsWMP = false;

        public bool IsFullScreen = false;

        public Player()
        {
            InitializeComponent();
            this.styleMain = GlobalStaticVars.GlobalStyle;
            this.styleMain.MetroForm = this;
        }

        private void FadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop(); //this stops the timer if the form is completely displayed
            else
                Opacity += 0.05;
        }

        private void FrmPlayer_Load(object sender, EventArgs e)
        {
            string FormTitle = StreamingContent.StreamInformation.ContentTitle;
            this.Text = FormTitle;
            //player.URL = StreamingContent.StreamUrl;

            if (!PlexDL.Player.Player.MFPresent)
            {
                MessageBox.Show("MediaFoundation is not installed. The player will not be able to stream the selected content :(", "System Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CanFadeOut = false;
                this.Close();
            }

            if (StreamingContent.StreamInformation.Container == "mkv")
            {
                CanFadeOut = false;
                DialogResult msg =
                MessageBox.Show(
                    "PlexDL Matroska (mkv) playback is not supported. Would you like to open the file in VLC Media Player? Note: It must already be installed",
                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msg == DialogResult.No)
                {
                    this.Close();
                }
                else
                {
                    try
                    {
                        VLCLauncher.LaunchVLC(StreamingContent);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occurred whilst trying to launch VLC\n\n" + ex.ToString(), "Launch Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        LoggingHelpers.RecordException(ex.Message, "VLCLaunchError");
                        this.Close();
                    }
                }
            }

            mPlayer = new PlexDL.Player.Player(pnlPlayer);
            mPlayer.Sliders.Position.TrackBar = trkDuration;
            mPlayer.Events.MediaPositionChanged += MPlayer_MediaPositionChanged;
            mPlayer.Events.MediaEnded += MPlayer_ContentFinished;
            mPlayer.Events.MediaStarted += MPlayer_ContentStarted;
            mPlayer.FullScreenMode = FullScreenMode.Form;
            mPlayer.Display.FullScreenMode = FullScreenMode.Display;
            mPlayer.Display.Window = pnlPlayer;
            mPlayer.Display.Mode = DisplayMode.Stretch;
            //MessageBox.Show(TitlesTable.Rows.Count + "\n" +StreamingContent.StreamIndex);
            //MessageBox.Show("Duration: "+StreamingContent.ContentDuration+"\nSize: "+StreamingContent.ByteLength);
        }

        /*
         * PLAYER HOTKEYS:
         * RIGHT ARROW=Skip Forward (Interval)
         * LEFT ARROW=Skip Backward (Interval)
         * UP ARROW=Next Title Index
         * DOWN ARROW=Previous Title Index
         * SPACE=Play/Pause
         */

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!IsWMP)
            {
                if (keyData == (GlobalStaticVars.Settings.Player.KeyBindings.PlayPause))
                {
                    PlayPause();
                    return true;
                }
                else if (keyData == (GlobalStaticVars.Settings.Player.KeyBindings.SkipForward))
                {
                    SkipForward();
                    return true;
                }
                else if (keyData == (GlobalStaticVars.Settings.Player.KeyBindings.SkipBackward))
                {
                    SkipBack();
                    return true;
                }
                else if (keyData == (GlobalStaticVars.Settings.Player.KeyBindings.NextTitle))
                {
                    Stop();
                    NextTitle();
                    return true;
                }
                else if (keyData == (GlobalStaticVars.Settings.Player.KeyBindings.PrevTitle))
                {
                    Stop();
                    PrevTitle();
                    return true;
                }
                else if (keyData == Keys.Escape)
                {
                    if (mPlayer.FullScreen)
                    {
                        mPlayer.FullScreen = false;
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrmPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((GlobalStaticVars.Settings.Generic.AnimationSpeed > 0) && (CanFadeOut))
            {
                e.Cancel = true;
                t1 = new Timer
                {
                    Interval = GlobalStaticVars.Settings.Generic.AnimationSpeed
                };
                t1.Tick += new EventHandler(FadeOut); //this calls the fade out function
                t1.Start();

                if (Opacity == 0)
                {
                    //resume the event - the program can be closed
                    e.Cancel = false;
                }
            }

            if (mPlayer != null)
            {
                mPlayer.Dispose();
            }
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
            TimeSpan fromStart = TimeSpan.FromTicks(e.FromStart);
            TimeSpan toStop = TimeSpan.FromTicks(e.ToStop);

            lblTimeSoFar.Text = fromStart.ToString(@"hh\:mm\:ss");
            lblTotalDuration.Text = toStop.ToString(@"hh\:mm\:ss");
        }

        private void FadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0) //check if opacity is 0
            {
                t1.Stop(); //if it is, we stop the timer
                Close(); //and we try to close the form
            }
            else
                Opacity -= 0.05;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCopyLink_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(StreamingContent.StreamInformation.Link);
        }

        private void TmrCopied_Tick(object sender, EventArgs e)
        {
            //btnCopyLink.Text = "Copy Stream Link";
            tmrCopied.Stop();
        }

        private void TmrRefreshUI_Tick(object sender, EventArgs e)
        {
            /*
            PlayingPosition = CalculateTime(vdo.CurrentPosition);
            lblTimeSoFar.Text = PlayingPosition;
            pbDuration.Value = (int)vdo.CurrentPosition;
            */
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            if (mPlayer.Playing)
            {
                mPlayer.Stop();
                mPlayer.Paused = false;
                SetIconPlay();
            }
        }

        private void Play(string FileName)
        {
            PlexDL.WaitWindow.WaitWindow.Show(PlayWorker, "Loading Stream", new object[]
            {
                FileName
            });
        }

        private void SetIconPause()
        {
            ImageSet images = new ImageSet()
            {
                Focus = PlexDL.Properties.Resources.baseline_pause_black_18dp_white, Idle = PlexDL.Properties.Resources.baseline_pause_black_18dp
            };
            if (btnPlayPause.InvokeRequired)
            {
                btnPlayPause.BeginInvoke((MethodInvoker)delegate
                {
                    btnPlayPause.ImageSet = images;
                });
            }
            else
            {
                btnPlayPause.ImageSet = images;
            }
        }

        private void SetIconPlay()
        {
            ImageSet images = new ImageSet()
            {
                Focus = PlexDL.Properties.Resources.baseline_play_arrow_black_18dp_white, Idle = PlexDL.Properties.Resources.baseline_play_arrow_black_18dp
            };
            if (btnPlayPause.InvokeRequired)
            {
                btnPlayPause.BeginInvoke((MethodInvoker)delegate
                {
                    btnPlayPause.ImageSet = images;
                });
            }
            else
            {
                btnPlayPause.ImageSet = images;
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

        private void StartPlayer(string fileName)
        {
            if (pnlPlayer.InvokeRequired)
            {
                var d = new SafePlayDelegate(StartPlayer);
                pnlPlayer.Invoke(d, new object[]
                {
                    fileName
                });
            }
            else
            {
                mPlayer.Play(fileName);
            }
        }

        private void PlayWorker(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            if (!mPlayer.Playing)
            {
                if (Methods.RemoteFileExists(StreamingContent.StreamInformation.Link))
                {
                    string FileName = (string)e.Arguments[0];
                    StartPlayer(FileName);
                    SetIconPause();
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show("Couldn't load the stream because the remote file doesn't exist or returned an error", "Network Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    }
                    else
                    {
                        MessageBox.Show("Couldn't load the stream because the remote file doesn't exist or returned an error", "Network Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnPrevTitle_Click(object sender, EventArgs e)
        {
            Stop();
            PrevTitle();
        }

        private string GetBaseUri(bool incToken)
        {
            if (incToken)
            {
                return "http://" + GlobalStaticVars.Settings.ConnectionInfo.PlexAddress + ":" + GlobalStaticVars.Settings.ConnectionInfo.PlexPort + "/?X-Plex-Token=";
            }
            else
            {
                return "http://" + GlobalStaticVars.Settings.ConnectionInfo.PlexAddress + ":" + GlobalStaticVars.Settings.ConnectionInfo.PlexPort + "/";
            }
        }

        private void GetObjectFromIndexCallback(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            int index = (int)e.Arguments[0];
            e.Result = GetObjectFromIndexWorker(index);
        }

        private PlexMovie GetObjectFromIndex(int index)
        {
            PlexMovie result = (PlexMovie)PlexDL.WaitWindow.WaitWindow.Show(GetObjectFromIndexCallback, "Getting Metadata", new object[]
            {
                index
            });
            return result;
        }

        private PlexMovie GetObjectFromIndexWorker(int index)
        {
            PlexMovie obj = new PlexMovie();

            DownloadInfo dlInfo = GetContentDownloadInfo(index);

            obj.StreamInformation = dlInfo;
            obj.StreamIndex = index;
            return obj;
        }

        private DownloadInfo GetContentDownloadInfo(int index)
        {
            if ((index + 1) <= TitlesTable.Rows.Count)
            {
                DataRow result = TitlesTable.Rows[index];
                DownloadInfo obj;
                string key = result["key"].ToString();
                string baseUri = GetBaseUri(false);
                key = key.TrimStart('/');
                string uri = baseUri + key + "/?X-Plex-Token=";

                XmlDocument reply = XmlGet.GetXMLTransaction(uri, GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken);

                obj = GetContentDownloadInfo_Xml(reply);
                return obj;
            }
            else
            {
                MessageBox.Show("Index was higher than row count; could not process data.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DownloadInfo();
            }
        }

        private DownloadInfo GetContentDownloadInfo_Xml(XmlDocument xml)
        {
            DownloadInfo obj = new DownloadInfo();

            DataSet sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(xml));

            DataTable part = sections.Tables["Part"];
            DataRow video = sections.Tables["Video"].Rows[0];
            string title = video["title"].ToString();
            if (title.Length > GlobalStaticVars.Settings.Generic.DefaultStringLength)
                title = title.Substring(0, GlobalStaticVars.Settings.Generic.DefaultStringLength);
            string thumb = video["thumb"].ToString();
            string thumbnailFullUri = "";
            if (!string.IsNullOrEmpty(thumb))
            {
                string baseUri = GetBaseUri(false).TrimEnd('/');
                thumbnailFullUri = baseUri + thumb + "?X-Plex-Token=" + GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken;
            }

            DataRow partRow = part.Rows[0];

            string filePart = partRow["key"].ToString();
            string container = partRow["container"].ToString();
            long byteLength = Convert.ToInt64(partRow["size"]);
            int contentDuration = Convert.ToInt32(partRow["duration"]);
            string fileName = Common.Methods.RemoveIllegalCharacters(title + "." + container);

            string link = GetBaseUri(false).TrimEnd('/') + filePart + "/?X-Plex-Token=" + GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken;
            obj.Link = link;
            obj.Container = container;
            obj.ByteLength = byteLength;
            obj.ContentDuration = contentDuration;
            obj.FileName = fileName;
            obj.ContentTitle = title;
            obj.ContentThumbnailUri = thumbnailFullUri;

            return obj;
        }

        private void BtnNextTitle_Click(object sender, EventArgs e)
        {
            Stop();
            NextTitle();
        }

        private void NextTitle()
        {
            if (((StreamingContent.StreamIndex + 1) < TitlesTable.Rows.Count))
            {
                PlexMovie next = GetObjectFromIndex(StreamingContent.StreamIndex + 1);
                StreamingContent = next;
                string FormTitle = StreamingContent.StreamInformation.ContentTitle;
                this.Text = FormTitle;
                this.Refresh();
                //MessageBox.Show(StreamingContent.StreamIndex + "\n" + TitlesTable.Rows.Count);
            }
            else if ((StreamingContent.StreamIndex + 1) == TitlesTable.Rows.Count)
            {
                PlexMovie next = GetObjectFromIndex(0);
                StreamingContent = next;
                string FormTitle = StreamingContent.StreamInformation.ContentTitle;
                this.Text = FormTitle;
                this.Refresh();
                //MessageBox.Show(StreamingContent.StreamIndex + "\n" + TitlesTable.Rows.Count);
            }
        }

        private void PrevTitle()
        {
            if (StreamingContent.StreamIndex != 0)
            {
                PlexMovie next = GetObjectFromIndex(StreamingContent.StreamIndex - 1);
                StreamingContent = next;
                string FormTitle = StreamingContent.StreamInformation.ContentTitle;
                this.Text = FormTitle;
                this.Refresh();
                //MessageBox.Show(StreamingContent.StreamIndex + "\n" + TitlesTable.Rows.Count);
            }
            else
            {
                PlexMovie next = GetObjectFromIndex(TitlesTable.Rows.Count - 1);
                StreamingContent = next;
                string FormTitle = StreamingContent.StreamInformation.ContentTitle;
                this.Text = FormTitle;
                this.Refresh();
                //MessageBox.Show(StreamingContent.StreamIndex + "\n" + TitlesTable.Rows.Count);
            }
        }

        private void SkipBack()
        {
            if (mPlayer.Playing)
            {
                decimal rewindAmount = (GlobalStaticVars.Settings.Player.SkipBackwardInterval) * -1;

                mPlayer.Position.Skip((int)rewindAmount);
            }
        }

        private void SkipForward()
        {
            if (mPlayer.Playing)
            {
                decimal stepAmount = GlobalStaticVars.Settings.Player.SkipForwardInterval;

                mPlayer.Position.Skip((int)stepAmount);
            }
        }

        private void BtnSkipBack_Click(object sender, EventArgs e)
        {
            SkipBack();
        }

        private void BtnSkipForward_Click(object sender, EventArgs e)
        {
            SkipForward();
        }

        private void Resume()
        {
            if ((mPlayer.Playing) && (mPlayer.Paused))
            {
                mPlayer.Resume();
                SetIconPause();
            }
        }

        private void Pause()
        {
            if ((mPlayer.Playing) && (!mPlayer.Paused))
            {
                mPlayer.Pause();
                SetIconPlay();
            }
        }

        private void PlayPause()
        {
            if ((mPlayer.Playing) && (!mPlayer.Paused))
            {
                Pause();
            }
            else
            {
                if (mPlayer.Paused)
                {
                    Resume();
                }
                else
                {
                    Play(StreamingContent.StreamInformation.Link);
                }
            }
        }

        private void BtnPlayPause_Click(object sender, EventArgs e)
        {
            PlayPause();
        }
    }
}