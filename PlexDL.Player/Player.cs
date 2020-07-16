/****************************************************************

    PVS.MediaPlayer - Version 0.99.1
    July 2020, The Netherlands
    © Copyright 2020 PVS The Netherlands - licensed under The Code Project Open License (CPOL)

    PVS.MediaPlayer uses (part of) the Media Foundation .NET library by nowinskie and snarfle (https://sourceforge.net/projects/mfnet).
    Licensed under either Lesser General Public License v2.1 or BSD.  See license.txt or BSDL.txt for details (http://mfnet.sourceforge.net).

    ****************

    For use with Microsoft Windows 7 or higher, Microsoft .NET Framework version 2.0 or higher and WinForms (any CPU).
    Created with Microsoft Visual Studio.

    Article on CodeProject with information on the use of the PVS.MediaPlayer library:
    https://www.codeproject.com/Articles/109714/PVS-MediaPlayer-Audio-and-Video-Player-Library

    ****************

    The PVS.MediaPlayer library source code is divided into 8 files:

    1. Player.cs        - main source code
    2. SubClasses.cs    - various grouping and information classes
    3. Interop.cs       - unmanaged Win32 functions
    4. AudioDevices.cs  - audio devices and peak meters
    5. DisplayClones.cs - multiple video displays 
    6. CursorHide.cs    - hides the mouse cursor during inactivity
    7. Subtitles.cs     - subrip (.srt) subtitles
    8. Infolabel.cs     - custom ToolTip

    Required references:
    System
    System.Drawing
    System.Windows.Forms

    ****************

    This file: Player.cs

    Player main source code

    ****************

    Many thanks to Microsoft (Windows, .NET Framework, Visual Studio and others), all the people
    writing about programming on the internet (a great source for ideas and solving problems),
    the websites publishing those or other writings about programming, the people responding to the
    PVS.MediaPlayer articles with comments and suggestions and, of course, the people at CodeProject.

    Special thanks to the creators of Media Foundation .NET for their great library.

    Special thanks to Sean Ewington and Deeksha Shenoy of CodeProject who also took care of publishing the many
    code updates and changes in the PVS.MediaPlayer articles in a friendly, fast, and highly competent manner.

    Peter Vegter
    July 2020, The Netherlands

    ****************************************************************/

#region Usings

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#endregion

#region Disable Some Warnings

// programmer's preference:
#pragma warning disable IDE0044 // Add readonly modifier
// the library can be compiled with all versions of c#:
#pragma warning disable IDE1005 // Delegate invocation can be simplified.
#pragma warning disable IDE0018 // Inline variable declaration
#pragma warning disable IDE0017 // Simplify object initialization

#endregion


[assembly: CLSCompliant(true)]

namespace PlexDL.Player
{

    // ******************************** Player - Enumerations

    #region Player - Enumerations

    #endregion


    // ******************************** Player - EventArgs

    #region Player - PositionEventArgs

    #endregion

    #region Player - EndedEventArgs

    #endregion

    #region Player - PeakLevelEventArgs

    #endregion

    #region Player - SubtitleEventArgs

    #endregion

    #region Player - SystemAudioDevicesEventArgs

    #endregion

    #region Player - VideoColorEventArgs

    #endregion


    // ******************************** Player - Delegates (Callbacks)

    #region Player - Delegates (Callbacks)

    #endregion



    // ******************************** Player - Player Class

    /// <summary>
    /// Represents a media player that can be used to play media files using Microsoft Media Foundation.
    /// </summary>
    [CLSCompliant(true)]
    public sealed partial class Player : HideObjectMembers, IDisposable
    {

        // ******************************** Player - Event Declarations

        #region Player - Event Declarations

        internal EventHandler   _mediaStarted;
        internal EventHandler   _mediaPausedChanged;

        internal EventHandler<EndedEventArgs>
                                _mediaEnded;
        internal EventHandler<EndedEventArgs>
                                _mediaEndedNotice;

        internal EventHandler   _mediaRepeatChanged;
        internal EventHandler   _mediaRepeated;

        internal EventHandler<PositionEventArgs>
                                _mediaPositionChanged;

        internal EventHandler   _mediaStartStopTimeChanged;

        internal EventHandler   _mediaDisplayChanged;
        internal EventHandler   _mediaDisplayModeChanged;
        internal EventHandler   _mediaDisplayShapeChanged;

        internal EventHandler   _mediaFullScreenChanged;
        internal EventHandler   _mediaFullScreenModeChanged;

        internal EventHandler   _mediaAudioVolumeChanged;
        internal EventHandler   _mediaAudioBalanceChanged;
        internal EventHandler   _mediaAudioMuteChanged;

        internal EventHandler   _mediaAudioDeviceChanged;

        internal static EventHandler<SystemAudioDevicesEventArgs>
                                _masterSystemAudioDevicesChanged;
        // copy used for unsubsribing:
        internal EventHandler<SystemAudioDevicesEventArgs>
                                _mediaSystemAudioDevicesChanged;

        internal EventHandler   _mediaVideoBoundsChanged;

        internal EventHandler   _mediaSpeedChanged;

        internal EventHandler   _mediaOverlayChanged;
        internal EventHandler   _mediaOverlayModeChanged;
        internal EventHandler   _mediaOverlayHoldChanged;
        internal EventHandler   _mediaOverlayActiveChanged;

        internal EventHandler   _mediaDisplayClonesChanged;

        internal EventHandler<PeakLevelEventArgs>
                                _mediaPeakLevelChanged;
        internal EventHandler<PeakLevelEventArgs>
                                _mediaInputLevelChanged;

        internal EventHandler<SubtitleEventArgs>
                                _mediaSubtitleChanged;

        internal EventHandler   _mediaVideoTrackChanged;
        internal EventHandler   _mediaAudioTrackChanged;

        internal EventHandler<VideoColorEventArgs>
                                _mediaVideoColorChanged;

        internal EventHandler   _mediaAudioInputDeviceChanged;
        internal EventHandler   _mediaWebcamFormatChanged;

        internal EventHandler   _mediaRecorderStarted;
        internal EventHandler   _mediaRecorderStopped;
        internal EventHandler   _mediaRecorderPausedChanged;

        #endregion


        // ******************************** Player - On Event Methods

        #region Player - On Event Methods

        internal void OnMediaPositionChanged()
        {
            //if (MediaPositionChanged != null) // test done at call
            {
                if (_playing)
                {
                    long position = PositionX;
                    long toEnd = _mediaLength - position;
                    if (toEnd < 0) toEnd = 0;

                    _positionArgs._fromBegin    = position;
                    _positionArgs._toEnd        = toEnd;
                    _positionArgs._fromStart    = position - _startTime;
                    _positionArgs._toStop       = _stopTime == 0 ? toEnd : _stopTime - position;
                }
                else
                {
                    _positionArgs._fromBegin    = 0;
                    _positionArgs._toEnd        = 0;
                    _positionArgs._fromStart    = 0;
                    _positionArgs._toStop       = 0;
                }
                _mediaPositionChanged(this, _positionArgs);
            }
        }

        internal void OnMediaSystemDevicesChanged(object sender, SystemAudioDevicesEventArgs args)
        {
            foreach (Delegate del in _masterSystemAudioDevicesChanged.GetInvocationList())
            {
                ISynchronizeInvoke syncTarget = del.Target as ISynchronizeInvoke;
                syncTarget.BeginInvoke(_masterSystemAudioDevicesChanged, new object[] { null, args });

                //((ISynchronizeInvoke)del.Target).Invoke(_mediaSystemAudioDevicesChanged, new object[] { null, args });
            }
        }

        #endregion


        // ******************************** Player - Fields

        #region Player - Fields

        #region Constants

        // Media Foundation Version
        internal const int              MF_VERSION                  = 0x10070;

        // PVS.MediaPlayer Version
        internal const float            VERSION                     = 0.991F;
        internal const string           VERSION_STRING              = "PVS.MediaPlayer 0.99.1";

        // Default Values
        private const string            AUDIO_TRACK_NAME            = "Audio Track ";
        private const string            VIDEO_TRACK_NAME            = "Video Track ";

        internal const int              MEDIUM_BUFFER_SIZE          = 256;
        internal const int              SMALL_BUFFER_SIZE           = 32;

        private const bool              AUDIO_ENABLED_DEFAULT       = true;

        private const float             AUDIO_VOLUME_DEFAULT        = 1.0f;
        internal const float            AUDIO_VOLUME_MINIMUM        = 0.0f;
        internal const float            AUDIO_VOLUME_MAXIMUM        = 1.0f;

        private const float             AUDIO_BALANCE_DEFAULT       = 0.0f;
        private const float             AUDIO_BALANCE_MINIMUM       = -1.0f;
        private const float             AUDIO_BALANCE_MAXIMUM       = 1.0f;

        internal const float            VIDEO_COLOR_MINIMUM         = -1.0f;
        internal const float            VIDEO_COLOR_MAXIMUM         = 1.0f;

        internal const int              VIDEO_WIDTH_MINIMUM         = 8;
        internal const int              VIDEO_HEIGHT_MINIMUM        = 8;
        internal const int              VIDEO_WIDTH_MAXIMUM         = 25000;
        internal const int              VIDEO_HEIGHT_MAXIMUM        = 25000;
        internal const int              DEFAULT_VIDEO_WIDTH_MAXIMUM = 6400; // user video zoom limits
        internal const int              DEFAULT_VIDEO_HEIGHT_MAXIMUM= 6400;

        internal const bool             DEFAULT_IMAGES_ENABLED      = true;
        internal const int              DEFAULT_IMAGES_DURATION     = 50000000; // 5 seconds
        internal const int              DEFAULT_IMAGES_FRAME_RATE   = 16;

        internal const float            DEFAULT_SPEED               = 1.0f;
        internal const bool             DEFAULT_SPEED_BOOST         = false;
        internal const float            DEFAULT_SPEED_MINIMUM       = 0.125f;
        internal const float            DEFAULT_SPEED_MAXIMUM       = 8.0f;
        internal const long             DEFAULT_STEP_MARGIN         = 2000000; // 200 ms

        private const DisplayMode       DEFAULT_DISPLAY_MODE        = DisplayMode.ZoomCenter;
        private const DisplayShape      DEFAULT_DISPLAY_SHAPE       = DisplayShape.Normal;
        private const bool              DEFAULT_DISPLAY_SHAPE_VIDEO = true;

        private const FullScreenMode    DEFAULT_FULLSCREEN_MODE     = FullScreenMode.Display;
        private const OverlayMode       DEFAULT_OVERLAY_MODE        = OverlayMode.Video;
        private const OverlayBlend      DEFAULT_OVERLAY_BLEND       = OverlayBlend.None;

        private const CopyMode          DEFAULT_COPY_MODE           = CopyMode.Video;

        private const int               MAX_FULLSCREEN_PLAYERS      = 16;
        internal const int              MAX_AUDIO_CHANNELS          = 16;

        private const int               DEFAULT_TIMER_INTERVAL      = 100; // ms
        private const int               MINIMUM_TIMER_INTERVAL      = 10;
        private const int               MAXIMUM_TIMER_INTERVAL      = 2000;

        private const bool              DEFAULT_MINIMIZED_ENABLED   = true;
        private const int               DEFAULT_MINIMIZED_INTERVAL  = 200; // ms

        // Start Media
        private const bool              DEFAULT_DOEVENTS_ENABLED    = false;
        private const int               DEFAULT_DOEVENTS_TIMEOUT    = 40;
        private const int               DEFAULT_DOEVENTS_LOOP       = 4000;

        // Fixed Values
        internal const string           SUBTITLES_FILE_EXTENSION    = ".srt";
        internal const string           MP4_FILE_EXTENSION          = ".mp4";

        internal const int              TIMEOUT_1_SECOND            = 1000;
        internal const int              TIMEOUT_5_SECONDS           = 5000;
        internal const int              TIMEOUT_10_SECONDS          = 10000;
        internal const int              TIMEOUT_15_SECONDS          = 15000;
        internal const int              TIMEOUT_30_SECONDS          = 30000;
        internal const int              TIMEOUT_45_SECONDS          = 45000;

        internal const long             ONE_SECOND_TICKS            = 10000000;
        private const long              AUDIO_STEP_TICKS            = 1000000;
        private const int               EOF_MARGIN_MS               = 300;

        internal const int              NO_ERROR                    = 0;
        internal const int              NO_VALUE                    = -1;
        private const int               NO_STREAM_SELECTED          = -1;
        private const float             STOP_VALUE                  = -1;

        internal const long             MS_TO_TICKS                 = 10000;
        internal const float            TICKS_TO_MS                 = 0.0001f;

        private const int               MF_UPDATE_WAIT_MS           = 75;
        private const int               MF_REPEAT_WAIT_MS           = 350;

        #endregion

        // Media Foundation
        internal static bool            MF_Installed;
        internal static bool            MF_Checked;
        internal static int             MF_Checked_Result;

        // Media Foundation Session
        internal bool                   mf_HasSession;
        internal bool                   mf_Replay;

        internal IMFMediaSession        mf_MediaSession;
        internal bool                   mf_LowLatency;
        internal IMFAttributes          mf_SessionConfig;
        internal IMFAttributes          mf_SessionConfigLowLatency;
        internal IMFMediaSource         mf_MediaSource;

        internal IMFVideoDisplayControl mf_VideoDisplayControl;
        internal IMFVideoProcessor      mf_VideoProcessor;
        internal IMFAudioStreamVolume   mf_AudioStreamVolume;
        internal IMFRateControl         mf_RateControl;

        internal IMFClock               mf_Clock;
        private MFCallback              mf_Callback;
        internal bool                   mf_AwaitCallback;
        internal System.Threading.AutoResetEvent WaitForEvent;
        internal bool                   mf_DoEvents                 = DEFAULT_DOEVENTS_ENABLED; // if true, use Application.DoEvents with main media start
        internal bool                   mf_AwaitDoEvents;
        private int                     mf_DoEventsTimeOut          = DEFAULT_DOEVENTS_TIMEOUT;
        private int                     mf_DoEventsLoop             = DEFAULT_DOEVENTS_LOOP;

        // Used with all players fullscreen management
        private static Form[]           _fullScreenForms            = new Form[MAX_FULLSCREEN_PLAYERS];

        // Text Buffers
        internal StringBuilder          _textBuffer1;
        internal StringBuilder          _textBuffer2;

        // Last Error
        internal HResult                _lastError;

        // Display
        internal Control                _display;
        internal bool                   _displayHold;
        internal bool                   _hasDisplay;
        private bool                    _hasDisplayEvents;
        internal DisplayMode            _displayMode                = DEFAULT_DISPLAY_MODE;
        internal VideoDisplay           _videoDisplay;
        internal bool                   _hasVideoDisplay;           // also = playing + hasVideo + videoEnabled
        internal bool                   _dragEnabled;

        // Display Shapes
        internal bool                   _hasDisplayShape;
        internal DisplayShape           _displayShape               = DEFAULT_DISPLAY_SHAPE;
        internal ShapeCallback          _displayShapeCallback;
        private bool                    _displayShapeBlock;
        internal bool                   _hasVideoShape              = DEFAULT_DISPLAY_SHAPE_VIDEO;
        internal GraphicsPath           _customShapePath;

        // Display Overlay
        internal Form                   _overlay;
        internal bool                   _hasOverlay;
        internal OverlayMode            _overlayMode                = DEFAULT_OVERLAY_MODE;
        internal bool                   _overlayHold;
        internal bool                   _overlayCanFocus;
        private bool                    _hasOverlayMenu;
        internal bool                   _hasOverlayShown;
        private bool                    _hasOverlayEvents;
        private bool                    _hasOverlayFocusEvents;
        internal bool                   _hasOverlayClipping;
        private bool                    _hasOverlayClippingEvents;
        internal OverlayBlend           _overlayBlend               = DEFAULT_OVERLAY_BLEND;
        internal SafeNativeMethods.BLENDFUNCTION _blendFunction;

        // Full Screen
        internal bool                   _fullScreen;
        internal FullScreenMode         _fullScreenMode             = DEFAULT_FULLSCREEN_MODE;
        internal Rectangle              _fsFormBounds;
        private FormBorderStyle         _fsFormBorder;
        private Rectangle               _fsParentBounds;
        private BorderStyle             _fsParentBorder;
        private int                     _fsParentIndex;
        private Rectangle               _fsDisplayBounds;
        private BorderStyle             _fsDisplayBorder;
        private int                     _fsDisplayIndex;

        // CursorHide
        internal bool                   _hasCursorHide;

        // Player / Media
        internal PropVariant            mf_StartTime;

        private string                  _playerName;
        internal string                 _fileName;
        internal bool                   _fileMode;
        internal bool                   _hasTempFile;
        private string                  _tempFileName;

        internal long                   _startTime;
        internal long                   _deviceStart;               // webcam / microphone / live stream start time
        internal long                   _stopTime;

        private bool                    _seekBusy;
        private bool                    _seekPending;
        private long                    _seekValue;

        private bool                    _stepMode;                  // used with special resume
        internal long                   _stepMargin                 = DEFAULT_STEP_MARGIN;  // margin at end of file with stepping

        internal bool                   _repeat;
        internal int                    _repeatCount;
        internal bool                   _playing;
        internal bool                   _paused;

        internal float                  _speed                      = DEFAULT_SPEED;
        internal bool                   _speedBoost                 = DEFAULT_SPEED_BOOST;
        internal float                  mf_Speed                    = DEFAULT_SPEED;
        internal float                  mf_SpeedMinimum             = DEFAULT_SPEED_MINIMUM;
        internal float                  mf_SpeedMaximum             = DEFAULT_SPEED_MAXIMUM;
        private bool                    _speedSkipped;

        internal long                   _mediaLength;

        internal bool                   _busyStarting;
        private EndedEventArgs          _endedEventArgs;

        // PlayerStartInfo
        internal bool                   _siFileMode;
        internal string                 _siFileName;
        internal bool                   _siMicMode;                 // microphone - audio input
        private AudioInputDevice        _siMicDevice;
        internal bool                   _siWebcamMode;
        private WebcamDevice            _siWebcamDevice;
        private WebcamFormat            _siWebcamFormat;
        private Control                 _siDisplay;
        internal long                   _siStartTime;
        internal long                   _siStopTime;
        internal bool                   _siRepeat;

        // Video
        internal bool                   _hasVideo;
        internal bool                   _videoCut;
        internal VideoStream[]          _videoTracks;
        internal int                    _videoTrackCount;
        internal int                    _videoTrackBase             = NO_STREAM_SELECTED;
        internal int                    _videoTrackCurrent          = NO_STREAM_SELECTED;
        internal bool                   _hasVideoBounds;
        internal Rectangle              _videoBounds;
        internal Rectangle              _videoBoundsClip;
        internal Size                   _videoSourceSize;
        internal float                  _videoFrameRate;
        private long                    _videoFrameStep;

        // Video Processor
        private bool                    _hasVideoProcessor;
        private bool                    _failVideoProcessor;
        private DXVA2VideoProcessorCaps _videoProcessorCaps;

        // Video Color
        internal DXVA2ProcAmpValues     _procAmpValues;
        internal bool                   _setVideoColor;
        internal double                 _brightness;
        internal bool                   _hasBrightnessRange;
        internal DXVA2ValueRange        _brightnessRange;
        internal double                 _contrast;
        internal bool                   _hasContrastRange;
        internal DXVA2ValueRange        _contrastRange;
        internal double                 _hue;
        internal bool                   _hasHueRange;
        internal DXVA2ValueRange        _hueRange;
        internal double                 _saturation;
        internal bool                   _hasSaturationRange;
        internal DXVA2ValueRange        _saturationRange;

        // Audio
        internal bool                   _hasAudio;
        internal bool                   _audioCut;
        internal bool                   _audioEnabled               = AUDIO_ENABLED_DEFAULT;
        internal float                  _audioVolume                = AUDIO_VOLUME_DEFAULT;
        internal float                  mf_AudioVolume              = AUDIO_VOLUME_DEFAULT;
        internal float                  _audioBalance               = AUDIO_BALANCE_DEFAULT;
        internal float                  mf_AudioBalance             = AUDIO_BALANCE_DEFAULT;
        internal AudioDevice            _audioDevice;
        internal bool                   _hasDeviceChangedHandler;
        internal AudioStream[]          _audioTracks;
        internal int                    _audioTrackCount;
        internal int                    _audioTrackBase             = NO_STREAM_SELECTED;
        internal int                    _audioTrackCurrent          = NO_STREAM_SELECTED;

        internal int                    _audioChannelCount;         // channels used to set audio
        internal int                    _mediaChannelCount;         // channels in playing media

        internal float[]                _audioChannelsVolume;
        internal float[]                _audioChannelsVolumeCopy;
        internal float[]                _audioChannelsMute;

        // Microphone / Audio Input Device
        internal bool                   _micMode;
        internal AudioInputDevice       _micDevice;

        // Webcam
        internal bool                   _webcamMode;
        internal WebcamDevice           _webcamDevice;
        internal WebcamFormat           _webcamFormat;
        internal bool                   _webcamAggregated;
        internal IMFMediaSource         _webcamVideoSource;
        internal IMFMediaSource         _webcamAudioSource;

        // Online Streams
        internal bool                   _fileStreamMode;            // set with online media
        internal bool                   _liveStreamMode;            // set if online media has no length

        // Images
        internal bool                   _imageMode;
        internal bool                   _imagesEnabled              = DEFAULT_IMAGES_ENABLED;
        private int                     _imageBytesPerPixel;
        private Guid                    _imageMediaType;
        internal long                   _imageDuration              = DEFAULT_IMAGES_DURATION;
        internal int                    _imageFrameRate             = DEFAULT_IMAGES_FRAME_RATE;

        // Copy
        internal CopyMode               _copyMode                   = DEFAULT_COPY_MODE;

        // Timer used with position changed events, output level meter
        internal Timer                  _timer;
        internal bool                   _hasPositionEvents;
        private PositionEventArgs       _positionArgs;

        // Peak Level
        internal PeakLevelEventArgs     _outputLevelArgs;
        private bool                    _outputLevelMuted;
        internal PeakLevelEventArgs     _inputLevelArgs;
        private bool                    _inputLevelMuted;

        // Minimized - Overlay Delay
        internal bool                   _minimizeEnabled            = DEFAULT_MINIMIZED_ENABLED;
        private bool                    _minimizeHasEvent;
        private bool                    _minimized;
        private Timer                   _minimizeTimer;
        internal int                    _minimizedInterval          = DEFAULT_MINIMIZED_INTERVAL;
        private double                  _minimizedOpacity;

        // Computer Sleep Disable
        private bool                    _sleepOff;

        // Taskbar Progress
        internal static TaskbarIndicator.ITaskbarList3 TaskbarInstance;
        internal static bool            _taskbarProgressEnabled;
        internal bool                   _hasTaskbarProgress;

        // Miscellaneous
        private bool                    _disposed;

        // Member Grouping Classes
        private Audio                   _audioClass;
        private AudioInput              _audioInputClass;
        private DisplayClones           _clonesClass;
        private Display                 _displayClass;
        private CursorHide              _cursorHideClass;
        private Events                  _eventsClass;
        private Media                   _mediaClass;
        private Images                  _imagesClass;
        private Playlist                _playlistClass;
        private TaskbarProgress         _taskbarProgress;
        private Overlay                 _overlayClass;
        private SystemPanels            _panelsClass;
        private PointTo                 _pointToClass;
        private Position                _positionClass;
        private Copy                    _copyClass;
        private Sliders                 _slidersClass;
        private Subtitles               _subtitlesClass;
        private Video                   _videoClass;
        private Has                     _hasClass;
        private Speed                   _speedClass;
        private Webcam                  _webcamClass;

        #endregion


        // ******************************** Player - Public Members

        // ******************************** Player - Constructor / Dispose / Finalizer / Version / Media Foundation Check

        #region Public - Player Constructor

        /// <summary>
        /// Initializes a new instance of the PVS.MediaPlayer.Player class (creates a new media player).
        /// </summary>
        public Player()
        {
            if (Environment.OSVersion.Version.Major > 6 || (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor > 0))
            {
                _lastError = MFExtern.MFStartup(MF_VERSION, MFStartup.Full);
            }
            else _lastError = HResult.CO_E_WRONGOSFORAPP;

            MF_Checked        = true;
            MF_Checked_Result = (int)_lastError;

            if (_lastError == NO_ERROR)
            {
                MF_Installed        = true;

                MFExtern.MFCreateAttributes(out mf_SessionConfig, 1);
                mf_SessionConfig.SetUINT32(MFAttributesClsid.MF_SESSION_GLOBAL_TIME, 1);

                MFExtern.MFCreateAttributes(out mf_SessionConfigLowLatency, 2);
                mf_SessionConfigLowLatency.SetUINT32(MFAttributesClsid.MF_SESSION_GLOBAL_TIME, 1);
                mf_SessionConfigLowLatency.SetUINT32(MFAttributesClsid.MF_LOW_LATENCY, 1);

                mf_StartTime        = new PropVariant();

                WaitForEvent        = new System.Threading.AutoResetEvent(false);
                mf_Callback         = new MFCallback(this);

                _textBuffer1        = new StringBuilder(MEDIUM_BUFFER_SIZE);
                _textBuffer2        = new StringBuilder(SMALL_BUFFER_SIZE);

                _positionArgs       = new PositionEventArgs();
                _endedEventArgs     = new EndedEventArgs();

                _videoDisplay       = new VideoDisplay();
                _procAmpValues      = new DXVA2ProcAmpValues();

                _audioChannelsVolume = new float[MAX_AUDIO_CHANNELS];
                for (int i = 0; i < MAX_AUDIO_CHANNELS; i++) _audioChannelsVolume[i] = AUDIO_VOLUME_DEFAULT;
                _audioChannelsMute  = new float[MAX_AUDIO_CHANNELS];

                _timer              = new Timer { Interval = DEFAULT_TIMER_INTERVAL };
                _timer.Tick         += AV_TimerTick;

                _minimizeTimer      = new Timer { Interval = DEFAULT_MINIMIZED_INTERVAL };
                _minimizeTimer.Tick += AV_MinimizeTimer_Tick;
            }
            else
            {
                throw new Win32Exception((int)_lastError);
            }
        }

        /// <summary>
        /// Initializes a new instance of the PVS.MediaPlayer.Player class (creates a new media player).
        /// </summary>
        /// <param name="display">The form or control that is used to display video and overlays.</param>
        public Player(Control display) : this(display, null) { }

        /// <summary>
        /// Initializes a new instance of the PVS.MediaPlayer.Player class (creates a new media player).
        /// </summary>
        /// <param name="display">The form or control that is used to display video and overlays.</param>
        /// <param name="overlay">The form that is used as display overlay.</param>
        public Player(Control display, Form overlay) : this()
        {
            if (display != null)
            {
                _lastError = AV_SetDisplay(display, true);
                if (_lastError == NO_ERROR && overlay != null)
                {
                    AV_SetOverlay(overlay);
                }
            }
        }

        #endregion

        #region Public - Player Dispose / Finalizer

        /// <summary>
        /// Removes the player and cleans up any resources being used.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Removes the player and cleans up any resources being used.
        /// </summary>
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (mf_HasSession)
                {
                    try { AV_CloseSession(true, false, StopReason.Finished); }
                    catch { /* ignored */ }
                }

                if (disposing)
                {
                    if (dc_HasDisplayClones)    DisplayClones_Clear();
                    if (_hasTaskbarProgress)    TaskbarProgress.Clear();

                    if (_hasPositionSlider)     Sliders.Position.TrackBar   = null;
                    if (_speedSlider != null)   Sliders.Speed               = null;
                    if (_shuttleSlider != null) Sliders.Shuttle             = null;
                    if (_volumeSlider != null)  Sliders.AudioVolume         = null;
                    if (_balanceSlider != null) Sliders.AudioBalance        = null;

                    if (_fullScreen)
                    {
                        try { AV_ResetFullScreen(); }
                        catch { /* ignored */ }
                    }
                    if (mf_Callback != null)
                    {
                        mf_Callback.Dispose();
                        mf_Callback = null;
                    }
                    if (_timer != null)
                    {
                        _timer.Dispose();
                        _timer = null;
                    }
                    if (_minimizeTimer != null)
                    {
                        _minimizeTimer.Dispose();
                        _minimizeTimer = null;
                    }
                    if (_hasOverlay) AV_SetOverlay(null);

                    _videoDisplay.Dispose();

                    if (_customShapePath != null)   _customShapePath.Dispose();

                    _textBuffer1 = null;
                    _textBuffer2 = null;
                }

                if (_hasCursorHide)
                {
                    try { CH_RemovePlayerItems(this); }
                    catch { /* ignored */ }
                }
                if (pm_HasPeakMeter)
                {
                    try { PeakMeter_Close(); }
                    catch { /* ignored */ }
                }
                if (_mediaSystemAudioDevicesChanged != null)
                {
                    try
                    {
                        Delegate[] handlers = _mediaSystemAudioDevicesChanged.GetInvocationList();
                        for (int i = handlers.Length - 1; i >= 0; --i)
                        {
                            _mediaSystemAudioDevicesChanged -= (EventHandler<SystemAudioDevicesEventArgs>)handlers[i];
                        }
                    }
                    catch { /* ignored */ }
                }
                if (_sleepOff) SafeNativeMethods.SleepStatus = false;

                if (WaitForEvent != null) { WaitForEvent.Close(); WaitForEvent = null; }
                if (mf_StartTime != null) { mf_StartTime.Dispose();  mf_StartTime = null; }

                MFExtern.MFShutdown();
            }
        }

        /// <summary>
        /// Removes the player and cleans up any resources being used.
        /// </summary>
        ~Player()
        {
            Dispose(false);
        }

        #endregion

        #region Public - PVS.MediaPlayer Version

        /// <summary>
        /// Gets the version number of this PVS.MediaPlayer library.
        /// </summary>
        public static float Version
        {
            get { return VERSION; }
        }

        /// <summary>
        /// Gets the version string of this PVS.MediaPlayer library.
        /// </summary>
        public static string VersionString
        {
            get { return VERSION_STRING; }
        }

        #endregion

        #region Public - Media Foundation Check

        private static void DoSystemCheck()
        {
            if (!MF_Checked)
            {
                MF_Checked = true;
                HResult result = HResult.CO_E_WRONGOSFORAPP;

                if (Environment.OSVersion.Version.Major > 6 || (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor > 0))
                {
                    try
                    {
                        result = MFExtern.MFStartup(MF_VERSION, MFStartup.Full);
                        if (result == NO_ERROR)
                        {
                            Application.DoEvents();
                            MFExtern.MFShutdown();
                            MF_Installed = true;
                        }
                    }
                    catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
                }
                MF_Checked_Result = (int)result;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the required Microsoft Windows 7 or later and Microsoft Media Foundation are available on the system being used.
        /// </summary>
        public static bool MFPresent
        {
            get
            {
                if (!MF_Checked) DoSystemCheck();
                return MF_Installed;
            }
        }

        /// <summary>
        /// Gets a result (error) code that indicates whether the required Microsoft Windows 7 or later and Microsoft Media Foundation are available on the system (0 = no error (available)).
        /// </summary>
        public static int MFPresent_ResultCode
        {
            get
            {
                if (!MF_Checked) DoSystemCheck();
                return MF_Checked_Result;
            }
        }

        /// <summary>
        /// Gets a (localized) description that indicates whether the required Microsoft Windows 7 or later and Microsoft Media Foundation are available on the system.
        /// </summary>
        public static string MFPresent_ResultString
        {
            get
            {
                if (!MF_Checked) DoSystemCheck();
                return GetErrorText(MF_Checked_Result);
            }
        }

        #endregion


        // ******************************** Player - Play

        #region Public - Play

        /// <summary>
        /// Starts playing the specified media.
        /// </summary>
        /// <param name="fileName">The path and file name of the media to be played.</param>
        public int Play(string fileName)
        {
            _siFileName     = fileName;
            _siFileMode     = true;
            _siDisplay      = _display;
            _siStartTime    = 0;
            _siStopTime     = 0;
            _siRepeat       = _repeat;

            return (int)AV_Play();
        }

        /// <summary>
        /// Starts playing the specified media.
        /// </summary>
        /// <param name="fileName">The path and file name of the media to be played.</param>
        /// <param name="repeat">A value that indicates whether to repeat playback when the media has finished playing.</param>
        public int Play(string fileName, bool repeat)
        {
            _siFileName     = fileName;
            _siFileMode     = true;
            _siDisplay      = _display;
            _siStartTime    = 0;
            _siStopTime     = 0;
            _siRepeat       = repeat;

            return (int)AV_Play();
        }

        /// <summary>
        /// Starts playing the specified media.
        /// </summary>
        /// <param name="fileName">The path and file name of the media to be played.</param>
        /// <param name="display">The form or control to use to display the video of the media.</param>
        public int Play(string fileName, Control display)
        {
            _siFileName     = fileName;
            _siFileMode     = true;
            _siDisplay      = display;
            _siStartTime    = 0;
            _siStopTime     = 0;
            _siRepeat       = _repeat;

            return (int)AV_Play();
        }

        /// <summary>
        /// Starts playing the specified media.
        /// </summary>
        /// <param name="fileName">The path and file name of the media to be played.</param>
        /// <param name="display">The form or control to use to display the video of the media.</param>
        /// <param name="repeat">A value that indicates whether to repeat playback when the media has finished playing.</param>
        public int Play(string fileName, Control display, bool repeat)
        {
            _siFileName     = fileName;
            _siFileMode     = true;
            _siDisplay      = display;
            _siStartTime    = 0;
            _siStopTime     = 0;
            _siRepeat       = repeat;

            return (int)AV_Play();
        }

        /// <summary>
        /// Starts playing the specified media.
        /// </summary>
        /// <param name="fileName">The path and file name of the media to be played.</param>
        /// <param name="startTime">The time offset where the media should start playing or restart if it is repeated.</param>
        /// <param name="stopTime">The time offset where the media should stop playing or restart if it is repeated (use TimeSpan.Zero or 00:00:00 to indicate the natural end of the media).</param>
        public int Play(string fileName, TimeSpan startTime, TimeSpan stopTime)
        {
            _siFileName     = fileName;
            _siFileMode     = true;
            _siDisplay      = _display;
            _siStartTime    = startTime.Ticks;
            _siStopTime     = stopTime.Ticks;
            _siRepeat       = _repeat;

            return (int)AV_Play();
        }

        /// <summary>
        /// Starts playing the specified media.
        /// </summary>
        /// <param name="fileName">The path and file name of the media to be played.</param>
        /// <param name="startTime">The time offset where the media should start playing or restart if it is repeated.</param>
        /// <param name="stopTime">The time offset where the media should stop playing or restart if it is repeated (use TimeSpan.Zero or 00:00:00 to indicate the natural end of the media).</param>
        /// <param name="repeat">A value that indicates whether to repeat playback when the media has finished playing.</param>
        public int Play(string fileName, TimeSpan startTime, TimeSpan stopTime, bool repeat)
        {
            _siFileName     = fileName;
            _siFileMode     = true;
            _siDisplay      = _display;
            _siStartTime    = startTime.Ticks;
            _siStopTime     = stopTime.Ticks;
            _siRepeat       = repeat;

            return (int)AV_Play();
        }

        /// <summary>
        /// Starts playing the specified media.
        /// </summary>
        /// <param name="fileName">The path and file name of the media to be played.</param>
        /// <param name="display">The form or control to use to display the video of the media.</param>
        /// <param name="startTime">The time offset where the media should start playing or restart if it is repeated.</param>
        /// <param name="stopTime">The time offset where the media should stop playing or restart if it is repeated (use TimeSpan.Zero or 00:00:00 to indicate the natural end of the media).</param>
        public int Play(string fileName, Control display, TimeSpan startTime, TimeSpan stopTime)
        {
            _siFileName     = fileName;
            _siFileMode     = true;
            _siDisplay      = display;
            _siStartTime    = startTime.Ticks;
            _siStopTime     = stopTime.Ticks;
            _siRepeat       = _repeat;

            return (int)AV_Play();
        }

        /// <summary>
        /// Starts playing the specified media.
        /// </summary>
        /// <param name="fileName">The path and file name of the media to be played.</param>
        /// <param name="display">The form or control to use to display the video of the media.</param>
        /// <param name="startTime">The time offset where the media should start playing or restart if it is repeated.</param>
        /// <param name="stopTime">The time offset where the media should stop playing or restart if it is repeated (use TimeSpan.Zero or 00:00:00 to indicate the natural end of the media).</param>
        /// <param name="repeat">A value that indicates whether to repeat playback when the media has finished playing.</param>
        public int Play(string fileName, Control display, TimeSpan startTime, TimeSpan stopTime, bool repeat)
        {
            _siFileName     = fileName;
            _siFileMode     = true;
            _siDisplay      = display;
            _siStartTime    = startTime.Ticks;
            _siStopTime     = stopTime.Ticks;
            _siRepeat       = repeat;

            return (int)AV_Play();
        }

        #endregion

        #region Public - Play Byte Array

        /// <summary>
        /// Starts playing the specified byte array with media content.
        /// </summary>
        /// <param name="byteArray">The byte array (for example, Properties.Resources.MyMovie) to be played.</param>
        /// <param name="fileName">The file name for the temporary file of the byte array. The file is deleted afterwards.</param>
        public int Play(byte[] byteArray, string fileName)
        {
            _siDisplay      = _display;
            _siStartTime    = 0;
            _siStopTime     = 0;
            _siRepeat       = _repeat;

            return (int)AV_PlayByteArray(byteArray, fileName);
        }

        /// <summary>
        /// Starts playing the specified byte array with media content.
        /// </summary>
        /// <param name="byteArray">The byte array (for example, Properties.Resources.MyMovie) to be played.</param>
        /// <param name="fileName">The file name for the temporary file of the byte array. The file is deleted afterwards.</param>
        /// <param name="repeat">A value that indicates whether to repeat playback when the media has finished playing.</param>
        public int Play(byte[] byteArray, string fileName, bool repeat)
        {
            _siDisplay      = _display;
            _siStartTime    = 0;
            _siStopTime     = 0;
            _siRepeat       = repeat;

            return (int)AV_PlayByteArray(byteArray, fileName);
        }

        /// <summary>
        /// Starts playing the specified byte array with media content.
        /// </summary>
        /// <param name="byteArray">The byte array (for example, Properties.Resources.MyMovie) to be played.</param>
        /// <param name="fileName">The file name for the temporary file of the byte array. The file is deleted afterwards.</param>
        /// <param name="display">The form or control to be used to display the video of the media.</param>
        public int Play(byte[] byteArray, string fileName,   Control display)
        {
            _siDisplay      = display;
            _siStartTime    = 0;
            _siStopTime     = 0;
            _siRepeat       = _repeat;

            return (int)AV_PlayByteArray(byteArray, fileName);
        }

        /// <summary>
        /// Starts playing the specified byte array with media content.
        /// </summary>
        /// <param name="byteArray">The byte array (for example, Properties.Resources.MyMovie) to be played.</param>
        /// <param name="fileName">The file name for the temporary file of the byte array. The file is deleted afterwards.</param>
        /// <param name="display">The form or control to be used to display the video of the media.</param>
        /// <param name="repeat">A value that indicates whether to repeat playback when the media has finished playing.</param>
        public int Play(byte[] byteArray, string fileName, Control display, bool repeat)
        {
            _siDisplay      = display;
            _siStartTime    = 0;
            _siStopTime     = 0;
            _siRepeat       = repeat;

            return (int)AV_PlayByteArray(byteArray, fileName);
        }

        /// <summary>
        /// Starts playing the specified byte array with media content.
        /// </summary>
        /// <param name="byteArray">The byte array (for example, Properties.Resources.MyMovie) to be played.</param>
        /// <param name="fileName">The file name for the temporary file of the byte array. The file is deleted afterwards.</param>
        /// <param name="startTime">The time offset where the media should start playing or restart if it is repeated.</param>
        /// <param name="stopTime">The time offset where the media should stop playing or rewind if it is repeated (use TimeSpan.Zero or 00:00:00 to indicate the natural end of the media).</param>
        public int Play(byte[] byteArray, string fileName, TimeSpan startTime, TimeSpan stopTime)
        {
            _siDisplay      = _display;
            _siStartTime    = startTime.Ticks;
            _siStopTime     = stopTime.Ticks;
            _siRepeat       = _repeat;

            return (int)AV_PlayByteArray(byteArray, fileName);
        }

        /// <summary>
        /// Starts playing the specified byte array with media content.
        /// </summary>
        /// <param name="byteArray">The byte array (for example, Properties.Resources.MyMovie) to be played.</param>
        /// <param name="fileName">The file name for the temporary file of the byte array. The file is deleted afterwards.</param>
        /// <param name="startTime">The time offset where the media should start playing or restart if it is repeated.</param>
        /// <param name="stopTime">The time offset where the media should stop playing or rewind if it is repeated (use TimeSpan.Zero or 00:00:00 to indicate the natural end of the media).</param>
        /// <param name="repeat">A value that indicates whether to repeat playback when the media has finished playing.</param>
        public int Play(byte[] byteArray, string fileName, TimeSpan startTime, TimeSpan stopTime, bool repeat)
        {
            _siDisplay      = _display;
            _siStartTime    = startTime.Ticks;
            _siStopTime     = stopTime.Ticks;
            _siRepeat       = repeat;

            return (int)AV_PlayByteArray(byteArray, fileName);
        }

        /// <summary>
        /// Starts playing the specified byte array with media content.
        /// </summary>
        /// <param name="byteArray">The byte array (for example, Properties.Resources.MyMovie) to be played.</param>
        /// <param name="fileName">The file name for the temporary file of the byte array. The file is deleted afterwards.</param>
        /// <param name="display">The form or control to be used to display the video of the media.</param>
        /// <param name="startTime">The time offset where the media should start playing or restart if it is repeated.</param>
        /// <param name="stopTime">The time offset where the media should stop playing or rewind if it is repeated (use TimeSpan.Zero or 00:00:00 to indicate the natural end of the media).</param>
        public int Play(byte[] byteArray, string fileName, Control display, TimeSpan startTime, TimeSpan stopTime)
        {
            _siDisplay      = display;
            _siStartTime    = startTime.Ticks;
            _siStopTime     = stopTime.Ticks;
            _siRepeat       = _repeat;

            return (int)AV_PlayByteArray(byteArray, fileName);
        }

        /// <summary>
        /// Starts playing the specified byte array with media content.
        /// </summary>
        /// <param name="byteArray">The byte array (for example, Properties.Resources.MyMovie) to be played.</param>
        /// <param name="fileName">The file name for the temporary file of the byte array. The file is deleted afterwards.</param>
        /// <param name="display">The form or control to be used to display the video of the media.</param>
        /// <param name="startTime">The time offset where the media should start playing or restart if it is repeated.</param>
        /// <param name="stopTime">The time offset where the media should stop playing or rewind if it is repeated (use TimeSpan.Zero or 00:00:00 to indicate the natural end of the media).</param>
        /// <param name="repeat">A value that indicates whether to repeat playback when the media has finished playing.</param>
        public int Play(byte[] byteArray, string fileName, Control display, TimeSpan startTime, TimeSpan stopTime, bool repeat)
        {
            _siDisplay      = display;
            _siStartTime    = startTime.Ticks;
            _siStopTime     = stopTime.Ticks;
            _siRepeat       = repeat;

            return (int)AV_PlayByteArray(byteArray, fileName);
        }

        #endregion

        #region Public - Play Webcam

        /// <summary>
        /// Starts playing the specified webcam.
        /// </summary>
        /// <param name="webcam">The webcam to be played.</param>
        public int Play(WebcamDevice webcam)
        {
            return Play(webcam, null, _display, null);
        }

        /// <summary>
        /// Starts playing the specified webcam.
        /// </summary>
        /// <param name="webcam">The webcam to be played.</param>
        /// <param name="audioInput">The audio input device to be played.</param>
        public int Play(WebcamDevice webcam, AudioInputDevice audioInput)
        {
            return Play(webcam, audioInput, _display, null);
        }

        /// <summary>
        /// Starts playing the specified webcam.
        /// </summary>
        /// <param name="webcam">The webcam to be played.</param>
        /// <param name="display">The form or control to use to display the video of the webcam.</param>
        /// <returns></returns>
        public int Play(WebcamDevice webcam, Control display)
        {
            return Play(webcam, null, display, null);
        }

        /// <summary>
        /// Starts playing the specified webcam.
        /// </summary>
        /// <param name="webcam">The webcam to be played.</param>
        /// <param name="audioInput">The audio input device to be played.</param>
        /// <param name="display">The form or control to use to display the video of the webcam.</param>
        /// <returns></returns>
        public int Play(WebcamDevice webcam, AudioInputDevice audioInput, Control display)
        {
            return Play(webcam, audioInput, display, null);
        }

        /// <summary>
        /// Starts playing the specified webcam.
        /// </summary>
        /// <param name="webcam">The webcam to be played.</param>
        /// <param name="quality">A value that indicates the quality of the webcam's video output.</param>
        /// <returns></returns>
        public int Play(WebcamDevice webcam, WebcamQuality quality)
        {
            if (!CheckPlayWebcam(webcam, _display)) return (int)_lastError;

            WebcamFormat format = null;
            if (quality == WebcamQuality.High) format = Webcam.GetHighFormat(webcam, false);
            else if (quality == WebcamQuality.Photo) format = Webcam.GetHighFormat(webcam, true);
            else if (quality == WebcamQuality.Low) format = Webcam.GetLowFormat(webcam);

            return PlayCheckedWebcam(webcam, null, _display, format);
        }

        /// <summary>
        /// Starts playing the specified webcam.
        /// </summary>
        /// <param name="webcam">The webcam to be played.</param>
        /// <param name="audioInput">The audio input device to be played.</param>
        /// <param name="quality">A value that indicates the quality of the webcam's video output.</param>
        public int Play(WebcamDevice webcam, AudioInputDevice audioInput, WebcamQuality quality)
        {
            if (!CheckPlayWebcam(webcam, _display)) return (int)_lastError;

            WebcamFormat format = null;
            if (quality == WebcamQuality.High) format = Webcam.GetHighFormat(webcam, false);
            else if (quality == WebcamQuality.Photo) format = Webcam.GetHighFormat(webcam, true);
            else if (quality == WebcamQuality.Low) format = Webcam.GetLowFormat(webcam);

            return PlayCheckedWebcam(webcam, audioInput, _display, format);
        }

        /// <summary>
        /// Starts playing the specified webcam.
        /// </summary>
        /// <param name="webcam">The webcam to be played.</param>
        /// <param name="display">The form or control to use to display the video of the webcam.</param>
        /// <param name="quality">A value that indicates the quality of the webcam's video output.</param>
        public int Play(WebcamDevice webcam, Control display, WebcamQuality quality)
        {
            if (!CheckPlayWebcam(webcam, display)) return (int)_lastError;

            WebcamFormat format = null;
            if (quality == WebcamQuality.High) format = Webcam.GetHighFormat(webcam, false);
            else if (quality == WebcamQuality.Photo) format = Webcam.GetHighFormat(webcam, true);
            else if (quality == WebcamQuality.Low) format = Webcam.GetLowFormat(webcam);

            return PlayCheckedWebcam(webcam, null, display, format);
        }

        /// <summary>
        /// Starts playing the specified webcam.
        /// </summary>
        /// <param name="webcam">The webcam to be played.</param>
        /// <param name="audioInput">The audio input device to be played.</param>
        /// <param name="display">The form or control to use to display the video of the webcam.</param>
        /// <param name="quality">A value that indicates the quality of the webcam's video output.</param>
        public int Play(WebcamDevice webcam, AudioInputDevice audioInput, Control display, WebcamQuality quality)
        {
            if (!CheckPlayWebcam(webcam, display)) return (int)_lastError;

            WebcamFormat format = null;
            if (quality == WebcamQuality.High) format = Webcam.GetHighFormat(webcam, false);
            else if (quality == WebcamQuality.Photo) format = Webcam.GetHighFormat(webcam, true);
            else if (quality == WebcamQuality.Low) format = Webcam.GetLowFormat(webcam);

            return PlayCheckedWebcam(webcam, audioInput, display, format);
        }

        /// <summary>
        /// Starts playing the specified webcam.
        /// </summary>
        /// <param name="webcam">The webcam to be played.</param>
        /// <param name="format">The video output format of the webcam. See also Player.Webcam.GetFormats.</param>
        /// <returns></returns>
        public int Play(WebcamDevice webcam, WebcamFormat format)
        {
            return Play(webcam, null, _display, format);
        }

        /// <summary>
        /// Starts playing the specified webcam.
        /// </summary>
        /// <param name="webcam">The webcam to be played.</param>
        /// <param name="audioInput">The audio input device to be played.</param>
        /// <param name="format">The video output format of the webcam. See also Player.Webcam.GetFormats.</param>
        /// <returns></returns>
        public int Play(WebcamDevice webcam, AudioInputDevice audioInput, WebcamFormat format)
        {
            return Play(webcam, audioInput, _display, format);
        }

        /// <summary>
        /// Starts playing the specified webcam.
        /// </summary>
        /// <param name="webcam">The webcam to be played.</param>
        /// <param name="display">The form or control to use to display the video of the webcam.</param>
        /// <param name="format">The video output format of the webcam. See also Player.Webcam.GetFormats.</param>
        /// <returns></returns>
        public int Play(WebcamDevice webcam, Control display, WebcamFormat format)
        {
            return Play(webcam, null, display, format);
        }

        /// <summary>
        /// Starts playing the specified webcam.
        /// </summary>
        /// <param name="webcam">The webcam to be played.</param>
        /// <param name="audioInput">The audio input device to be played.</param>
        /// <param name="display">The form or control to use to display the video of the webcam.</param>
        /// <param name="format">The video output format of the webcam. See also Player.Webcam.GetFormats.</param>
        public int Play(WebcamDevice webcam, AudioInputDevice audioInput, Control display, WebcamFormat format)
        {
            if (!CheckPlayWebcam(webcam, display)) return (int)_lastError;

            _siFileName     = webcam._name;
            _siWebcamMode   = true;
            _siWebcamDevice = webcam;
            _siMicDevice    = audioInput;
            _siDisplay      = display;
            _siWebcamFormat = format;
            _siStartTime    = 0;
            _siStopTime     = 0;
            _siRepeat       = false;

            return (int)AV_Play();
        }

        private bool CheckPlayWebcam(WebcamDevice webcam, Control display)
        {
            _lastError = NO_ERROR;

            if (webcam == null || string.IsNullOrWhiteSpace(webcam._id)) _lastError = HResult.E_INVALIDARG;
            else if (display == null) _lastError = HResult.ERROR_INVALID_WINDOW_HANDLE;

            return _lastError == NO_ERROR;
        }

        private int PlayCheckedWebcam(WebcamDevice webcam, AudioInputDevice audioInput, Control display, WebcamFormat format)
        {
            _siFileName     = webcam._name;
            _siWebcamMode   = true;
            _siWebcamDevice = webcam;
            _siMicDevice    = audioInput;
            _siDisplay      = display;
            _siWebcamFormat = format;
            _siStartTime    = 0;
            _siStopTime     = 0;
            _siRepeat       = false;

            return (int)AV_Play();
        }

        #endregion

        #region Public - Play Audio Input Device

        /// <summary>
        /// Starts playing the specified audio input device.
        /// </summary>
        /// <param name="audioInput">The audio input device to be played.</param>
        public int Play(AudioInputDevice audioInput)
        {
            if (audioInput == null || string.IsNullOrWhiteSpace(audioInput._id))
            {
                _lastError = HResult.E_INVALIDARG;
                return (int)_lastError;
            }

            _siFileName     = audioInput._name;
            _siMicMode      = true;
            _siMicDevice    = audioInput;
            _siDisplay      = _display;
            _siStartTime    = 0;
            _siStopTime     = 0;
            _siRepeat       = false;

            return (int)AV_Play();
        }

        //internal void ActivateMediaInputEvent()
        //{
        //    if (pm_InputMeterPending && _micDevice != null)
        //    {
        //        if (InputMeter_Open(_micDevice, false))
        //        {
        //            if (_inputLevelArgs == null) _inputLevelArgs = new PeakLevelEventArgs();
        //            _mediaInputLevelChanged += value;
        //            pm_InputMeterPending = false;
        //            StartMainTimerCheck();
        //        }
        //    }
        //}

        #endregion

        #region Public - PlayUnblock / PlayTimeOut

        /// <summary>
        /// Gets or sets a value that indicates whether the "Application.DoEvents" method will be used while waiting for media to start to keep the main ui thread more responsive. For special purposes only (default: false).
        /// </summary>
        public bool PlayUnblock
        {
            get
            {
                _lastError = NO_ERROR;
                return mf_DoEvents;
            }
            set
            {
                mf_DoEvents = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the maximum time in seconds to wait for media to start when the "Player.PlayUnblock" property is activated (values from 10 to 180 seconds, default: 40 seconds).
        /// </summary>
        public int PlayTimeOut
        {
            get
            {
                _lastError = NO_ERROR;
                return mf_DoEventsTimeOut;
            }
            set
            {
                if (value < 10 || value > 180) _lastError = HResult.MF_E_OUT_OF_RANGE;
                else
                {
                    mf_DoEventsTimeOut = value;
                    mf_DoEventsLoop = value * 100;
                }
            }
        }

        #endregion


        #region Public - Pause / Resume / Stop / Reset

        /// <summary>
        /// Activates the player's pause mode (pauses media playback).
        /// </summary>
        public int Pause()
        {
            _lastError = NO_ERROR;

            if (!_paused)
            {
                if (_playing)
                {
                    mf_AwaitCallback = true;
                    _lastError          = mf_MediaSession.Pause();
                    //mf_AwaitCallback    = true;
                    WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);
                }
                if (_lastError == NO_ERROR)
                {
                    _paused = true;
                    if (_playing)
                    {
                        if (pm_HasPeakMeter)
                        {
                            _outputLevelArgs._channelCount    = pm_PeakMeterChannelCount;
                            _outputLevelArgs._masterPeakValue = STOP_VALUE;
                            _outputLevelArgs._channelsValues  = pm_PeakMeterValuesStop;
                            _mediaPeakLevelChanged(this, _outputLevelArgs);
                        }
                        if (_hasTaskbarProgress)
                        {
                            if (!_fileMode) _taskbarProgress.SetValue(1);
                            _taskbarProgress.SetState(TaskbarProgressState.Paused);
                        }
                        _timer.Stop();
                    }
                    if (_mediaPausedChanged != null) _mediaPausedChanged(this, EventArgs.Empty);
                }
            }
            return (int)_lastError;
        }

        /// <summary>
        /// Deactivates the player's pause mode (resumes playback of paused media).
        /// </summary>
        public int Resume()
        {
            _lastError = NO_ERROR;

            if (_paused)
            {
                if (_playing)
                {
                    if (_stepMode)
                    {
                        _stepMode = false;
                        _paused = false;
                        AV_UpdateTopology();
                    }
                    else
                    {
                        mf_StartTime.type = ConstPropVariant.VariantType.None;
                        _lastError = mf_MediaSession.Start(Guid.Empty, mf_StartTime);
                        mf_AwaitCallback = true;
                        WaitForEvent.WaitOne(TIMEOUT_10_SECONDS);

                        if (_lastError == NO_ERROR)
                        {
                            _paused = false;
                            StartMainTimerCheck();
                            if (_hasTaskbarProgress)
                            {
                                if (!_fileMode) _taskbarProgress.SetState(TaskbarProgressState.Indeterminate);
                                else _taskbarProgress.SetState(TaskbarProgressState.Normal);
                            }
                        }
                    }
                }
                else _paused = false;

                if (_lastError == NO_ERROR && _mediaPausedChanged != null) _mediaPausedChanged(this, EventArgs.Empty);
            }
            return (int)_lastError;
        }

        /// <summary>
        /// Stops media playback.
        /// </summary>
        public int Stop()
        {
            if (_playing)
            {
                AV_CloseSession(false, true, StopReason.UserStop);
            }
            _lastError = NO_ERROR;
            return NO_ERROR;
        }

        /// <summary>
        /// Stops media playback and resets most player settings to their default values.
        /// </summary>
        public int Reset()
        {
            // TODO

            Stop();

            if (_displayMode != DEFAULT_DISPLAY_MODE)
            {
                _displayMode        = DEFAULT_DISPLAY_MODE;
                _hasVideoBounds     = false;
                if (_hasDisplay) _display.Invalidate();
                if (_mediaDisplayModeChanged != null) _mediaDisplayModeChanged(this, EventArgs.Empty);
            }

            AV_SetAudioVolume(AUDIO_VOLUME_DEFAULT, true, true);
            AV_SetAudioBalance(AUDIO_BALANCE_DEFAULT, true, true);
            AV_SetAudioEnabled(AUDIO_ENABLED_DEFAULT);
            //AV_SetVideoEnabled(DEFAULT_VIDEO_ENABLED);

            // Video Colors
            _brightness     = 0;
            _contrast       = 0;
            _hue            = 0;
            _saturation     = 0;
            _setVideoColor  = false;

            Paused = false;
            Repeat = false;
            AV_SetSpeed(DEFAULT_SPEED, true);

            _lastError = NO_ERROR;
            return NO_ERROR;
        }

        #endregion

        #region Public - Playing / Paused

        /// <summary>
        /// Gets a value that indicates whether media is playing (including paused media).
        /// </summary>
        public bool Playing
        {
            get
            {
                _lastError = NO_ERROR;
                return _playing;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the player's pause mode is activated.
        /// </summary>
        public bool Paused
        {
            get
            {
                _lastError = NO_ERROR;
                return _paused;
            }
            set
            {
                if (value) Pause();
                else Resume();
            }
        }

        #endregion

        #region Public - Position / Step

        /// <summary>
        /// Provides access to the playback position settings of the player (for example, Player.Position.FromBegin).
        /// </summary>
        public Position Position
        {
            get
            {
                if (_positionClass == null) _positionClass = new Position(this);
                return _positionClass;
            }
        }

        internal long PositionX
        {
            get
            {
                long presTime = 0;
                _lastError = NO_ERROR;

                if (_playing)
                {
                    if (_psTracking) presTime = _positionSlider.Value * MS_TO_TICKS;
                    else
                    {
                        long sysTime;
                        int count = 20;
                        // (???) this strange construction seems necessary when webcams are playing (else prestime returns often 0)
                        while (presTime == 0 && count > 0)
                        {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                            mf_Clock.GetCorrelatedTime(0, out presTime, out sysTime);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                            if (--count > 0 && presTime == 0) System.Threading.Thread.Sleep(10);
                        }
                    }
                }
                return presTime;
            }
            set
            {
                _lastError = NO_ERROR;

                if (_seekBusy)
                {
                    _seekPending = true;
                    _seekValue = value;
                }
                else if (_playing)
                {
                    _seekBusy = true;

                    if (!_paused)
                    {
                        mf_MediaSession.Pause();
                        System.Threading.Thread.Sleep(5);
                        //mf_AwaitCallback = true;
                        //WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);
                    }

                    mf_StartTime.type = ConstPropVariant.VariantType.Int64;

                    do
                    {
                        _seekPending = false;

                        if (value <= 0) value = 0;
                        else if (value > _mediaLength) value = _mediaLength - 1000000;

                        mf_StartTime.longValue = value;
                        mf_MediaSession.Start(Guid.Empty, mf_StartTime);

                        // this or ...
                        //mf_AwaitCallback = true;
                        //WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);

                        // ... this
                        System.Threading.Thread.Sleep(50);

                        if (!_seekPending)
                        {
                            if (_hasTaskbarProgress) _taskbarProgress.SetValue(value);
                            if (_mediaPositionChanged != null) OnMediaPositionChanged();
                        }

                        value = _seekValue;

                    } while (_seekPending);

                    if (_paused)
                    {
                        mf_MediaSession.Start(Guid.Empty, mf_StartTime);
                        //mf_AwaitCallback = true;
                        //WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);
                        //System.Threading.Thread.Sleep(50);

                        mf_MediaSession.Pause();
                        mf_AwaitCallback = true;
                        WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);
                        //System.Threading.Thread.Sleep(50);

                    }
                    _seekBusy = false;
                }
            }
        }

        internal void SetPosition(long position)
        {
            if (!_fileMode) _lastError = HResult.E_NOTIMPL;
            else if (!_playing) _lastError = HResult.ERROR_NOT_READY;
            else
            {
                _lastError = NO_ERROR;

                PositionX = position;
                if (_hasPositionSlider)
                {
                    int position_ms = (int)(position * TICKS_TO_MS);
                    if (position_ms < _positionSlider.Minimum)
                    {
                        _positionSlider.Value = _positionSlider.Minimum;
                    }
                    else if (position_ms > _positionSlider.Maximum)
                    {
                        _positionSlider.Value = _positionSlider.Maximum;
                    }
                    else _positionSlider.Value = position_ms;
                }
            }
        }

        internal int Step(int frames)
        {
            if (!_fileMode) _lastError = HResult.E_NOTIMPL;
            else if (!_playing) _lastError = HResult.ERROR_NOT_READY;
            else
            {
                try
                {
                    long position = PositionX;
                    long endTime = _stopTime == 0 ? _mediaLength : _stopTime;
                    if (position < endTime - _stepMargin)
                    {
                        _lastError = NO_ERROR;

                        if (frames == 1 && _paused && _hasVideo)
                        {
                            _stepMode = true;

                            mf_RateControl.SetRate(false, 0);
                            mf_StartTime.type = ConstPropVariant.VariantType.None;
                            mf_MediaSession.Start(Guid.Empty, mf_StartTime);

                            mf_AwaitCallback = true;
                            WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);

                            Application.DoEvents();

                            if (_hasPositionSlider)
                            {
                                int position_ms = (int)(position * TICKS_TO_MS);
                                if (position_ms < _positionSlider.Minimum)
                                {
                                    _positionSlider.Value = _positionSlider.Minimum;
                                }
                                else if (position_ms > _positionSlider.Maximum)
                                {
                                    _positionSlider.Value = _positionSlider.Maximum;
                                }
                                else _positionSlider.Value = position_ms;
                            }
                            if (_hasTaskbarProgress) _taskbarProgress.SetValue(position);
                            if (_mediaPositionChanged != null) OnMediaPositionChanged();

                        }
                        else if (position > 0)
                        {
                            SetPosition(position + (frames * (_hasVideo ? _videoFrameStep : AUDIO_STEP_TICKS)));
                            System.Threading.Thread.Sleep(1);
                            Application.DoEvents();
                        }
                    }
                    else _lastError = HResult.MF_E_OUT_OF_RANGE;
                }
                catch { _lastError = HResult.MF_E_OUT_OF_RANGE; }
            }
            return (int)_lastError;
        }

        #endregion

        #region  Public - Repeat

        /// <summary>
        /// Gets or sets a value that indicates whether to repeat media playback when finished.
        /// </summary>
        public bool Repeat
        {
            get
            {
                _lastError = NO_ERROR;
                return _repeat;
            }
            set
            {
                _lastError = NO_ERROR;
                if (_repeat != value)
                {
                    _repeat = value;
                    if (_fileMode && _playing && value)
                    {
                        long pos = PositionX;
                        if (pos < _startTime || (_stopTime != 0 && pos > _stopTime))
                        {
                            AV_EndOfMedia();
                        }
                    }
                    if (_mediaRepeatChanged != null) _mediaRepeatChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets the number of times that (a part of) the playing media has been repeated.
        /// </summary>
        public int RepeatCount
        {
            get
            {
                _lastError = NO_ERROR;
                return _repeatCount;
            }
        }

        #endregion


        // ******************************** Player - Various Public Members

        #region Public - Audio

        /// <summary>
        /// Provides access to the audio settings of the player (for example, Player.Audio.Volume).
        /// </summary>
        public Audio Audio
        {
            get
            {
                if (_audioClass == null) _audioClass = new Audio(this);
                return _audioClass;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the player's audio output is muted (default: false).
        /// </summary>
        public bool Mute
        {
            get { return !_audioEnabled; }
            set { AV_SetAudioEnabled(!value); }
        }

        #endregion

        #region Public - Audio Input

        /// <summary>
        /// Provides access to the audio input settings of the player (for example, Player.AudioInput.GetDevices).
        /// </summary>
        public AudioInput AudioInput
        {
            get
            {
                if (_audioInputClass == null) _audioInputClass = new AudioInput(this);
                return _audioInputClass;
            }
        }

        #endregion

        #region Public - Video

        /// <summary>
        /// Provides access to the video settings of the player (for example, Player.Video.Present).
        /// </summary>
        public Video Video
        {
            get
            {
                if (_videoClass == null) _videoClass = new Video(this);
                return _videoClass;
            }
        }

        #endregion

        #region Public - Webcam

        /// <summary>
        /// Provides access to the webcam settings of the player (for example, Player.Webcam.GetDevices).
        /// </summary>
        public Webcam Webcam
        {
            get
            {
                if (_webcamClass == null) _webcamClass = new Webcam(this);
                return _webcamClass;
            }
        }

        #endregion

        #region Public - Low Latency

        /// <summary>
        /// Gets or sets a value that indicates the player's low latency mode (default: false).
        /// </summary>
        public bool LowLatency
        {
            get
            {
                _lastError = NO_ERROR;
                return mf_LowLatency;
            }
            set
            {
                _lastError = NO_ERROR;
                if (value != mf_LowLatency)
                {
                    mf_LowLatency = value;
                    if (_playing) AV_UpdateTopology();
                }
            }
        }

        #endregion

        #region Public - Display

        /// <summary>
        /// Provides access to the display settings of the player (for example, Player.Display.Window).
        /// </summary>
        public Display Display
        {
            get
            {
                if (_displayClass == null) _displayClass = new Display(this);
                return _displayClass;
            }
        }

        #endregion

        #region Public - FullScreen

        /// <summary>
        /// Gets or sets a value that indicates whether the player's full screen mode is activated (default: false).
        /// </summary>
        public bool FullScreen
        {
            get
            {
                _lastError = NO_ERROR;
                return _fullScreen;
            }
            set { AV_SetFullScreen(value); }
        }

        /// <summary>
        /// Gets or sets the player's full screen display mode (default: FullScreenMode.Display).
        /// </summary>
        public FullScreenMode FullScreenMode
        {
            get
            {
                _lastError = NO_ERROR;
                return _fullScreenMode;
            }
            set
            {
                if (_fullScreen) AV_SetFullScreenMode(value, true);
                else
                {
                    _lastError = NO_ERROR;
                    _fullScreenMode = value;
                    if (_mediaFullScreenModeChanged != null) _mediaFullScreenModeChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion

        #region Public - Overlay

        /// <summary>
        /// Provides access to the display overlay settings of the player (for example, Player.Overlay.Window).
        /// </summary>
        public Overlay Overlay
        {
            get
            {
                if (_overlayClass == null) _overlayClass = new Overlay(this);
                return _overlayClass;
            }
        }

        // Overlay Delay helper functions

        private void AV_MinimizeTimer_Tick(object sender, EventArgs e)
        {
            _minimizeTimer.Stop();
            if (_hasOverlay)
            {
                if (_minimized)
                {
                    _minimizedOpacity = _overlay.Opacity;
                    _overlay.Opacity = 0;
                }
                else
                {
                    _overlay.Opacity = _minimizedOpacity;
                }
            }
        }

        private void AV_Minimize_SizeChanged(object sender, EventArgs e)
        {
            if (_minimized && _overlay.Owner.WindowState != FormWindowState.Minimized)
            {
                _minimized = false;
                _minimizeTimer.Interval = _minimizedInterval;
                _minimizeTimer.Start();
            }
            else if (_hasOverlay && _overlay.Owner.WindowState == FormWindowState.Minimized)
            {
                _minimized = true;
                _minimizeTimer.Interval = DEFAULT_MINIMIZED_INTERVAL;
                _minimizeTimer.Start();
            }
        }

        internal void AV_MinimizeActivate(bool active)
        {
            if (active)
            {
                if (!_minimizeEnabled || !_hasOverlay || _overlay.Owner == null) return;

                if (!_minimizeHasEvent)
                {
                    _overlay.Owner.SizeChanged += AV_Minimize_SizeChanged;
                    _minimizeHasEvent = true;
                }
                if (!_minimized && _overlay.Owner.WindowState == FormWindowState.Minimized)
                {
                    _minimizedOpacity = _overlay.Opacity;
                    _overlay.Opacity = 0;
                    _minimized = true;
                }
            }
            else
            {
                if (!_minimizeHasEvent) return;

                if (_overlay.Owner != null) _overlay.Owner.SizeChanged -= AV_Minimize_SizeChanged;
                _minimizeHasEvent = false;
                if (_minimized)
                {
                    _overlay.Opacity = _minimizedOpacity;
                    _minimized = false;
                }
            }
        }

        #endregion

        #region Public - Copy

        /// <summary>
        /// Provides access to the copy settings of the player (for example, Player.Copy.ToImage).
        /// </summary>
        public Copy Copy
        {
            get
            {
                if (_copyClass == null) _copyClass = new Copy(this);
                return _copyClass;
            }
        }

        #endregion

        #region Public - Error Information

        /// <summary>
        /// Gets a value that indicates whether an error has occurred with the last player instruction.
        /// </summary>
        public bool LastError
        {
            get { return _lastError != NO_ERROR; }
        }

        /// <summary>
        /// Gets the code of the last error of the player that occurred (0 = no error).
        /// </summary>
        public int LastErrorCode
        {
            get { return (int)_lastError; }
        }

        /// <summary>
        /// Gets a (localized) description of the last error of the player that has occurred.
        /// </summary>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        public string LastErrorString
        {
            get { return GetErrorText((int)_lastError); }
        }

        /// <summary>
        /// Returns a (localized) description of the specified error code.
        /// </summary>
        /// <param name="errorCode">The error code whose description needs to be obtained.</param>
        public string GetErrorString(int errorCode)
        {
            return GetErrorText(errorCode);
        }

        /// <summary>
        /// Returns a (localized) description of the specified error code.
        /// </summary>
        /// <param name="errorCode">The error code whose description needs to be obtained.</param>
        public static string GetErrorText(int errorCode)
        {
            const int UNKNOWN_FILE_NOT_FOUND        = unchecked((int)0xc00d001a);
            const int UNKNOWN_SERVER_NOT_FOUND      = unchecked((int)0xc00d0035);
            const int INTERNET_NAME_NOT_RESOLVED    = unchecked((int)0x80072ee7);

            if (errorCode == UNKNOWN_FILE_NOT_FOUND)          errorCode = (int)HResult.ERROR_FILE_NOT_FOUND;
            //else if (errorCode == UNKNOWN_SERVER_NOT_FOUND) errorCode = (int)HResult.ERROR_SERVER_NOT_FOUND;
            else if (errorCode == UNKNOWN_SERVER_NOT_FOUND || errorCode == INTERNET_NAME_NOT_RESOLVED) errorCode = (int)HResult.ERROR_SERVER_NOT_CONNECTED;

            return new Win32Exception(errorCode).Message;
        }

        #endregion

        #region Public - Player Name

        /// <summary>
        /// Gets or sets the name of the player (default: Player.VersionString).
        /// </summary>
        public string Name
        {
            get
            {
                _lastError = NO_ERROR;
                //return _playerName == null ? VERSION_STRING : _playerName;
                if (string.IsNullOrWhiteSpace(_playerName)) return VERSION_STRING;
                return _playerName;
            }
            set
            {
                _lastError = NO_ERROR;
                _playerName = value;
            }
        }

        #endregion

        #region Public - Media

        /// <summary>
        /// Provides access to the media settings of the player (for example, Player.Media.GetName).
        /// </summary>
        public Media Media
        {
            get
            {
                if (_mediaClass == null) _mediaClass = new Media(this);
                return _mediaClass;
            }
        }

        // GetLength helper function
        private long Length
        {
            get
            {
                _lastError = NO_ERROR;
                return _mediaLength;
            }
        }

        internal MediaSourceType AV_GetSourceType()
        {
            MediaSourceType sourceType = MediaSourceType.None;

            // this order of checking should not be changed
            if (_imageMode) sourceType = MediaSourceType.Image;
            else if (_liveStreamMode) sourceType = MediaSourceType.LiveStream;
            else if (_fileStreamMode) sourceType = MediaSourceType.FileStream;
            else if (_fileMode)
            {
                if (_hasTempFile) sourceType = MediaSourceType.ByteArray;
                else sourceType = MediaSourceType.File;
            }
            else if (_webcamMode) sourceType = MediaSourceType.Webcam;
            else if (_webcamAggregated) sourceType = MediaSourceType.WebcamWithAudio;
            else if (_micMode) sourceType = MediaSourceType.AudioInput;

            return sourceType;
        }

        internal MediaSourceCategory AV_GetSourceCategory()
        {
            MediaSourceCategory sourceCategory = MediaSourceCategory.None;

            if (_fileStreamMode || _liveStreamMode) sourceCategory = MediaSourceCategory.OnlineFile;
            else if (_fileMode) sourceCategory = MediaSourceCategory.LocalFile;
            else if (_webcamMode || _webcamAggregated || _micMode) sourceCategory = MediaSourceCategory.LocalCapture;

            return sourceCategory;
        }

        #endregion

        #region Public - Images

        /// <summary>
        /// Provides access to the images settings of the player (for example, Player.Images.Enabled).
        /// </summary>
        public Images Images
        {
            get
            {
                if (_imagesClass == null) _imagesClass = new Images(this);
                return _imagesClass;
            }
        }

        #endregion

        #region Public - Playlist

        /// <summary>
        /// Provides access to the playlist methods of the player (for example, Player.Playlist.Open).
        /// </summary>
        public Playlist Playlist
        {
            get
            {
                if (_playlistClass == null) _playlistClass = new Playlist(this);
                return _playlistClass;
            }
        }

        #endregion

        #region Public - TaskbarProgress

        /// <summary>
        /// Provides access to the taskbar progress indicator settings of the player (for example, Player.TaskbarProgress.Add).
        /// </summary>
        public TaskbarProgress TaskbarProgress
        {
            get
            {
                if (_taskbarProgress == null) _taskbarProgress = new TaskbarProgress(this);
                return _taskbarProgress;
            }
        }

        #endregion

        #region Public - PointTo

        /// <summary>
        /// Provides access to the point conversion methods of the player (for example, Player.PointTo.Display).
        /// </summary>
        public PointTo PointTo
        {
            get
            {
                if (_pointToClass == null) _pointToClass = new PointTo(this);
                return _pointToClass;
            }
        }

        #endregion

        #region Public - Timer Interval

        /// <summary>
        /// Gets or sets the interval of the player's events timer in milliseconds (default: 100 ms).
        /// </summary>
        public int TimerInterval
        {
            get
            {
                _lastError = NO_ERROR;
                return _timer.Interval;
            }
            set
            {
                //_lastError = HResult.MF_E_OUT_OF_RANGE;
                //if (value < MINIMUM_TIMER_INTERVAL) _timer.Interval = MINIMUM_TIMER_INTERVAL;
                //else if (value > MAXIMUM_TIMER_INTERVAL) _timer.Interval = MAXIMUM_TIMER_INTERVAL;
                if (value < MINIMUM_TIMER_INTERVAL || value > MAXIMUM_TIMER_INTERVAL) _lastError = HResult.MF_E_OUT_OF_RANGE;
                else
                {
                    _lastError = NO_ERROR;
                    _timer.Interval = value;
                }
            }
        }

        #endregion

        #region Public - System Control Panels

        /// <summary>
        /// Provides access to the system audio and display control panels (for example, Player.SystemPanels.ShowAudioMixer).
        /// </summary>
        public SystemPanels SystemPanels
        {
            get
            {
                if (_panelsClass == null) _panelsClass = new SystemPanels(this);
                return _panelsClass;
            }
        }

        #endregion

        #region Public - Sleep Mode

        /// <summary>
        /// Gets or sets a value that indicates whether the system's sleep mode is disabled (default: false).
        /// </summary>
        public bool SleepDisabled
        {
            get
            {
                _lastError = NO_ERROR;
                return _sleepOff;
            }
            set
            {
                if (value != _sleepOff)
                {
                    _sleepOff = value;
                    SafeNativeMethods.SleepStatus = _sleepOff;
                }
                _lastError = NO_ERROR;
            }
        }

        #endregion

        #region Public - Has

        /// <summary>
        /// Provides access to information about the current status of the player (for example, Player.Has.Video).
        /// </summary>
        public Has Has
        {
            get
            {
                if (_hasClass == null) _hasClass = new Has(this);
                return _hasClass;
            }
        }

        #endregion

        #region Public - Speed

        /// <summary>
        /// Provides access to the playback speed settings of the player (for example, Player.Speed.Rate).
        /// </summary>
        public Speed Speed
        {
            get
            {
                if (_speedClass == null) _speedClass = new Speed(this);
                return _speedClass;
            }
        }

        #endregion

        #region Public - Events

        /// <summary>
        /// Provides access to the events of the player (for example, Player.Events.MediaEnded).
        /// </summary>
        public Events Events
        {
            get
            {
                if (_eventsClass == null) _eventsClass = new Events(this);
                return _eventsClass;
            }
        }

        #endregion


        // ******************************** Player - Private Members

        // ******************************** Player - Play

        #region Private - Byte Array To File

        internal string AV_ByteArrayToFile(byte[] byteArray, string fileName)
        {
            string path = string.Empty;

            if (fileName != null) fileName = fileName.Trim();

            if (byteArray == null || byteArray.Length <= 4) _lastError = HResult.E_INVALIDARG;
            else if (fileName == null || fileName.Length < 4) _lastError = HResult.ERROR_INVALID_NAME;
            else
            {
                try
                {
                    if (Path.IsPathRooted(fileName)) path = fileName;
                    else path = Path.Combine(Path.GetTempPath(), fileName);
                    File.WriteAllBytes(path, byteArray);
                    _lastError = NO_ERROR;
                }
                catch (Exception e)
                {
                    path = string.Empty;
                    _lastError = (HResult)Marshal.GetHRForException(e);
                }
            }
            return path;
        }

        #endregion

        #region Private - Image To Video

        private HResult AV_ImageToVideo(string fileName)
        {
            // already checked at calling method PlayMedia:
            //if (string.IsNullOrWhiteSpace(fileName)) _lastError = HResult.E_INVALIDARG;
            //else if (!File.Exists(fileName)) _lastError = HResult.ERROR_FILE_NOT_FOUND;
            //else
            {
                Bitmap frame = null;
                try
                {
                    frame = new Bitmap(fileName);

                    // mp4 video size has to be even.
                    int width = frame.Width % 2 == 0 ? frame.Width : frame.Width - 1;
                    int height = frame.Height % 2 == 0 ? frame.Height : frame.Height - 1;
                    if (width == height)
                    {
                        if (width != frame.Width) width += 2;
                        else if (height != frame.Height) height += 2;
                        else height -= 2;
                    }
                    if (width != frame.Width || height != frame.Height)
                    {
                        Bitmap oldFrame = frame;
                        frame = new Bitmap(frame, width, height);
                        oldFrame.Dispose();
                    }

                    //if (frame.Width * frame.Height < 16) _lastError = HResult.MF_E_OUT_OF_RANGE;
                    //else
                    {
                        try
                        {
                            _imageBytesPerPixel = Image.GetPixelFormatSize(frame.PixelFormat) / 8;
                            if (_imageBytesPerPixel == 0) _imageBytesPerPixel = 1;
                        }
                        catch { _imageBytesPerPixel = 4; }

                        if (_imageBytesPerPixel == 3) _imageMediaType = MFMediaType.RGB24;
                        else if (_imageBytesPerPixel == 4) _imageMediaType = MFMediaType.RGB32;
                        else _imageMediaType = MFMediaType.RGB8;

                        _tempFileName = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(fileName) + MP4_FILE_EXTENSION);
                        _hasTempFile = true;

                        IMFSinkWriter sinkWriter = AV_CreateImageWriter(_tempFileName, frame.Width, frame.Height, _imageMediaType, _imageFrameRate);
                        if (sinkWriter != null)
                        {
                            AV_WriteImageFrame(sinkWriter, frame);

                            sinkWriter.Finalize_();
                            Marshal.ReleaseComObject(sinkWriter);
                            sinkWriter = null;
                        }

                        if (_lastError != NO_ERROR) AV_RemoveTempFile();
                    }
                }
                catch { _lastError = HResult.MF_E_UNSUPPORTED_BYTESTREAM_TYPE; } // return 'not an image'

                if (frame != null) frame.Dispose();
            }
            return _lastError;
        }

        internal IMFSinkWriter AV_CreateImageWriter(string fileName, int width, int height, Guid mediaType, int frameRate)
        {
            const int VIDEO_BIT_RATE = 800000;

            IMFSinkWriter sinkWriter;
            IMFMediaType mediaTypeOut;
            IMFMediaType mediaTypeIn;
            int streamIndex = 0;

            HResult result = MFExtern.MFCreateSinkWriterFromURL(fileName, null, null, out sinkWriter);
            if (result == NO_ERROR)
            {
                result = MFExtern.MFCreateMediaType(out mediaTypeOut);
                if (result == NO_ERROR)
                {
                    result = mediaTypeOut.SetGUID(MFAttributesClsid.MF_MT_MAJOR_TYPE, MFMediaType.Video);
                    if (result == NO_ERROR)
                    {
                        // if changing the encoder also change the file extension (IMAGES_FILE_EXTENSION)
                        result = mediaTypeOut.SetGUID(MFAttributesClsid.MF_MT_SUBTYPE, MFMediaType.H264);
                        if (result == NO_ERROR)
                        {
                            result = mediaTypeOut.SetUINT32(MFAttributesClsid.MF_MT_AVG_BITRATE, VIDEO_BIT_RATE);
                            if (result == NO_ERROR)
                            {
                                result = mediaTypeOut.SetUINT32(MFAttributesClsid.MF_MT_INTERLACE_MODE, 2); // 2 = Progressive
                                if (result == NO_ERROR)
                                {
                                    result = MFExtern.MFSetAttributeSize(mediaTypeOut, MFAttributesClsid.MF_MT_FRAME_SIZE, width, height);
                                    if (result == NO_ERROR)
                                    {
                                        result = MFExtern.MFSetAttributeRatio(mediaTypeOut, MFAttributesClsid.MF_MT_FRAME_RATE, frameRate, 1);
                                        if (result == NO_ERROR)
                                        {
                                            result = MFExtern.MFSetAttributeRatio(mediaTypeOut, MFAttributesClsid.MF_MT_PIXEL_ASPECT_RATIO, 1, 1);
                                            if (result == NO_ERROR)
                                            {
                                                result = sinkWriter.AddStream(mediaTypeOut, out streamIndex);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (mediaTypeOut != null) Marshal.ReleaseComObject(mediaTypeOut);
            }

            if (result == NO_ERROR)
            {
                result = MFExtern.MFCreateMediaType(out mediaTypeIn);
                if (result == NO_ERROR)
                {
                    result = mediaTypeIn.SetGUID(MFAttributesClsid.MF_MT_MAJOR_TYPE, MFMediaType.Video);
                    if (result == NO_ERROR)
                    {
                        result = mediaTypeIn.SetGUID(MFAttributesClsid.MF_MT_SUBTYPE, mediaType);
                        if (result == NO_ERROR)
                        {
                            result = mediaTypeIn.SetUINT32(MFAttributesClsid.MF_MT_INTERLACE_MODE, 2); // 2 = Progressive
                            if (result == NO_ERROR)
                            {
                                result = MFExtern.MFSetAttributeSize(mediaTypeIn, MFAttributesClsid.MF_MT_FRAME_SIZE, width, height);
                                if (result == NO_ERROR)
                                {
                                    result = MFExtern.MFSetAttributeRatio(mediaTypeIn, MFAttributesClsid.MF_MT_FRAME_RATE, 1, 1);
                                    if (result == NO_ERROR)
                                    {
                                        result = MFExtern.MFSetAttributeRatio(mediaTypeIn, MFAttributesClsid.MF_MT_PIXEL_ASPECT_RATIO, 1, 1);
                                        if (result == NO_ERROR)
                                        {
                                            result = sinkWriter.SetInputMediaType(streamIndex, mediaTypeIn, null);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (mediaTypeIn != null) Marshal.ReleaseComObject(mediaTypeIn);
            }

            if (result == NO_ERROR)
            {
                result = sinkWriter.BeginWriting();
                if (result != NO_ERROR)
                {
                    Marshal.ReleaseComObject(sinkWriter);
                    sinkWriter = null;
                }
            }

            _lastError = result;
            return sinkWriter;
        }

        internal void AV_WriteImageFrame(IMFSinkWriter sinkWriter, Bitmap image)
        {
            HResult result = NO_ERROR;

            System.Drawing.Imaging.BitmapData bmpData = null;
            try { bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, image.PixelFormat); }
            catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }

            if (result == NO_ERROR)
            {
                IMFMediaBuffer buffer;
                int cbWidth = _imageBytesPerPixel * image.Width;
                int cbBuffer = cbWidth * image.Height;
                result = MFExtern.MFCreateMemoryBuffer(cbBuffer, out buffer);

                if (result == NO_ERROR)
                {
                    IntPtr data;
                    int maxLength, currentLength;
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                    result = buffer.Lock(out data, out maxLength, out currentLength);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                    if (result == NO_ERROR)
                    {
                        //result = MFExtern.MFCopyImage(data, cbWidth, bmpData.Scan0, cbWidth, cbWidth, image.Height);
                        result = MFExtern.MFCopyImage(data, cbWidth, bmpData.Scan0 + ((image.Height - 1) * cbWidth), -cbWidth, cbWidth, image.Height);
                        buffer.Unlock();

                        if (result == NO_ERROR)
                        {
                            buffer.SetCurrentLength(cbBuffer);

                            IMFSample sample;
                            result = MFExtern.MFCreateSample(out sample);

                            if (result == NO_ERROR)
                            {
                                result = sample.AddBuffer(buffer);
                                if (result == NO_ERROR)
                                {
                                    result = sample.SetSampleTime(0);
                                    if (result == NO_ERROR)
                                    {
                                        result = sample.SetSampleDuration(_imageDuration);
                                        if (result == NO_ERROR)
                                        {
                                            result = sinkWriter.WriteSample(0, sample);
                                        }
                                    }
                                }
                                Marshal.ReleaseComObject(sample);
                            }
                        }
                    }
                    Marshal.ReleaseComObject(buffer);
                }
                image.UnlockBits(bmpData);
            }
            _lastError = result;
        }

        #endregion

        #region Private - Play / PlayByteArray / PlayMedia / SetTopology / UpdateTopology

        internal HResult AV_Play()
        {
            if (_busyStarting)
            {
                _siFileMode     = false;
                _siMicMode      = false;
                _siMicDevice    = null;
                _siWebcamMode   = false;
                _siWebcamDevice = null;
                _lastError      = HResult.MF_E_STATE_TRANSITION_PENDING;
                return _lastError;
            }

            if (_siFileMode)
            {
                if (_siFileName != null) _siFileName = _siFileName.Trim();

                if (_siFileName == null || _siFileName.Length < 4)
                {
                    _lastError = HResult.ERROR_INVALID_NAME;
                    return _lastError;
                }

                if (_siStopTime != 0)
                {
                    if (_siStopTime < 0 || _siStopTime - _siStartTime < 1)
                    {
                        _lastError = HResult.MF_E_OUT_OF_RANGE;
                        return _lastError;
                    }
                }
            }

            _busyStarting = true;

            if (_playing) AV_CloseSession(false, true, StopReason.AutoStop);
            else _lastError = NO_ERROR;

            _fileName           = _siFileName;
            _fileMode           = _siFileMode;
            _siFileMode         = false;

            _micMode            = _siMicMode;
            _siMicMode          = false;
            _micDevice          = _siMicDevice;

            _webcamMode         = _siWebcamMode;
            _siWebcamMode       = false;
            _webcamDevice       = _siWebcamDevice;
            _siWebcamDevice     = null;
            _webcamFormat       = _siWebcamFormat;

            if (_siDisplay != _display)
            {
                AV_SetDisplay(_siDisplay, false);
                Application.DoEvents();
            }

            if (_siStartTime != 0 || _siStopTime != 0)
            {
                _startTime = _siStartTime;
                _stopTime  = _siStopTime;
                if (_mediaStartStopTimeChanged != null) _mediaStartStopTimeChanged(this, EventArgs.Empty);
            }
            if (_repeat != _siRepeat)
            {
                _repeat = _siRepeat;
                if (_mediaRepeatChanged != null) _mediaRepeatChanged(this, EventArgs.Empty);
            }

            HResult result = AV_PlayMedia();

            if (result == NO_ERROR)
            {
                if (!_fileMode) _deviceStart = PositionX;

                if (_hasDisplayShape) AV_UpdateDisplayShape();

                if (_overlay != null)
                {
                    if (!_hasOverlayShown)
                    {
                        AV_SetOverlay(_overlay);
                        Application.DoEvents();
                    }
                    else _display.Invalidate();
                }

                if (_fileMode && _hasPositionSlider)
                {
                    _positionSlider.Enabled = true;
                    if (_psHandlesProgress)
                    {
                        _positionSlider.Minimum = (int)(_startTime * TICKS_TO_MS);
                        if (_mediaLength <= (EOF_MARGIN_MS * MS_TO_TICKS))
                        {
                            _positionSlider.Maximum = _stopTime == 0 ? (int)(_mediaLength * TICKS_TO_MS) : (int)(_stopTime * TICKS_TO_MS);
                        }
                        else
                        {
                            _positionSlider.Maximum = _stopTime == 0 ? (int)(_mediaLength * TICKS_TO_MS - EOF_MARGIN_MS) : (int)(_stopTime * TICKS_TO_MS);
                        }
                    }
                    else
                    {
                        _positionSlider.Minimum = 0;
                        if (_mediaLength <= (EOF_MARGIN_MS * MS_TO_TICKS)) _positionSlider.Maximum = (int)(_mediaLength * TICKS_TO_MS);// - EOF_MARGIN_MS;
                        else _positionSlider.Maximum = (int)(_mediaLength * TICKS_TO_MS) - EOF_MARGIN_MS;
                    }
                }

                if (_fileMode && _paused && _startTime != 0 && _hasTaskbarProgress && _taskbarProgress._progressMode == TaskbarProgressMode.Track)
                {
                    if (!mf_Replay) _taskbarProgress.SetValue(_startTime);
                }

                if (dc_HasDisplayClones)
                {
                    DisplayClones_Start();
                    if (_displayHold)
                    {
                        Application.DoEvents();
                        for (int i = 0; i < dc_DisplayClones.Length; i++)
                        {
                            try
                            {
                                if (dc_DisplayClones[i] != null && dc_DisplayClones[i].Control != null && dc_DisplayClones[i].Control.Visible)
                                {
                                    while (dc_PaintBusy) { Application.DoEvents(); }
                                    dc_DisplayClones[i].Control.Invalidate();
                                }
                            }
                            catch { /* ignore */ }
                        }
                    }
                }
                if (_fileMode && st_SubtitlesEnabled) Subtitles_Start(false);

                if (_mediaStarted != null) _mediaStarted(this, EventArgs.Empty);

                if (_fileMode && _mediaPositionChanged != null)
                {
                    _hasPositionEvents = true;
                    OnMediaPositionChanged();
                }
                else _hasPositionEvents = false;

                if (_fileMode && _hasPositionSlider && _paused) _positionSlider.Value = (int)(_startTime * TICKS_TO_MS);
                StartMainTimerCheck();
            }

            _busyStarting = false;

            _lastError = result;
            return result;
        }

        private HResult AV_PlayByteArray(byte[] byteArray, string fileName)
        {
            if (_busyStarting)
            {
                _siFileMode = false;
                _lastError = HResult.MF_E_STATE_TRANSITION_PENDING;
                return _lastError;
            }

            _siFileName = AV_ByteArrayToFile(byteArray, fileName);

            if (_lastError == NO_ERROR)
            {
                _siFileMode     = true;
                _hasTempFile    = true;
                _tempFileName   = _siFileName;

                return AV_Play();
            }
            else return _lastError;
        }

        private HResult AV_PlayMedia()
        {
            HResult result;

            if (mf_MediaSession == null)
            {
                if (mf_LowLatency) MFExtern.MFCreateMediaSession(mf_SessionConfigLowLatency, out mf_MediaSession);
                else MFExtern.MFCreateMediaSession(mf_SessionConfig, out mf_MediaSession);
            }
            mf_MediaSession.BeginGetEvent(mf_Callback, null);

            // Get Media Source
            IMFAttributes attributes;
            if (_micMode)
            {
                MFExtern.MFCreateAttributes(out attributes, 2);
                attributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_GUID);
                attributes.SetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ENDPOINT_ID, _micDevice._id);
                result = MFExtern.MFCreateDeviceSource(attributes, out mf_MediaSource);
                if (result == HResult.MF_E_ATTRIBUTENOTFOUND || result == HResult.ERROR_PATH_NOT_FOUND) result = HResult.MF_E_NO_AUDIO_RECORDING_DEVICE;
                Marshal.ReleaseComObject(attributes);
            }
            else if (_webcamMode)
            {
                // webcams with sound: there is usually a delay when using the aggregate mode, can't find the reason why or a solution
                // a better result can be obtained by using 2 players, one for webcam video playback and one for audio playback
                if (_micDevice != null)
                {
                    _webcamAggregated = true;

                    MFExtern.MFCreateAttributes(out attributes, 2);
                    attributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
                    attributes.SetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, _webcamDevice._id);
                    result = MFExtern.MFCreateDeviceSource(attributes, out _webcamVideoSource);
                    if (result == HResult.MF_E_ATTRIBUTENOTFOUND || result == HResult.ERROR_PATH_NOT_FOUND) result = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
                    Marshal.ReleaseComObject(attributes);

                    if (result == NO_ERROR)
                    {
                        MFExtern.MFCreateAttributes(out attributes, 2);
                        attributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_GUID);
                        attributes.SetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ENDPOINT_ID, _micDevice._id);
                        result = MFExtern.MFCreateDeviceSource(attributes, out _webcamAudioSource);
                        if (result == HResult.MF_E_ATTRIBUTENOTFOUND || result == HResult.ERROR_PATH_NOT_FOUND) result = HResult.MF_E_NO_AUDIO_RECORDING_DEVICE;
                        Marshal.ReleaseComObject(attributes);

                        if (result == NO_ERROR)
                        {
                            IMFCollection collection;
                            result = MFExtern.MFCreateCollection(out collection);
                            if (result == NO_ERROR)
                            {
                                result = collection.AddElement(_webcamVideoSource);
                                if (result == NO_ERROR)
                                {
                                    result = collection.AddElement(_webcamAudioSource);
                                    if (result == NO_ERROR) result = MFExtern.MFCreateAggregateSource(collection, out mf_MediaSource);
                                }
                                Marshal.ReleaseComObject(collection);
                            }
                        }
                    }
                }
                else
                {
                    MFExtern.MFCreateAttributes(out attributes, 2);
                    attributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
                    attributes.SetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, _webcamDevice._id);

                    result = MFExtern.MFCreateDeviceSource(attributes, out mf_MediaSource);
                    if (result == HResult.MF_E_ATTRIBUTENOTFOUND || result == HResult.ERROR_PATH_NOT_FOUND) result = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
                    Marshal.ReleaseComObject(attributes);
                }

                if (result == NO_ERROR && _webcamFormat != null)
                {
                    MFExtern.MFCreateAttributes(out attributes, 1);
                    attributes.SetUINT32(MFAttributesClsid.MF_SOURCE_READER_DISCONNECT_MEDIASOURCE_ON_SHUTDOWN, 1);

                    IMFSourceReader reader;
                    result = MFExtern.MFCreateSourceReaderFromMediaSource(mf_MediaSource, attributes, out reader);
                    if (result == NO_ERROR)
                    {
                        IMFMediaType type;
                        result = reader.GetNativeMediaType(_webcamFormat.Track, _webcamFormat.Index, out type);
                        if (result == NO_ERROR) result = reader.SetCurrentMediaType(0, null, type);
                        Marshal.ReleaseComObject(reader);
                    }
                    Marshal.ReleaseComObject(attributes);
                }
            }
            else
            {
                IMFSourceResolver sourceResolver;
                result = MFExtern.MFCreateSourceResolver(out sourceResolver);
                if (result == NO_ERROR)
                {

#pragma warning disable IDE0059 // Unnecessary assignment of a value (here: CreateObjectFromURL objectType (can't be removed))

                    MFObjectType objectType;
                    object source;

                    result = sourceResolver.CreateObjectFromURL(_fileName, MFResolution.MediaSource | MFResolution.KeepByteStreamAliveOnFail, null, out objectType, out source);
                    if (result == HResult.MF_E_UNSUPPORTED_BYTESTREAM_TYPE)
                    {
                        result = sourceResolver.CreateObjectFromURL(_fileName, MFResolution.MediaSource | MFResolution.ContentDoesNotHaveToMatchExtensionOrMimeType, null, out objectType, out source);
                        if (_imagesEnabled && result == HResult.MF_E_UNSUPPORTED_BYTESTREAM_TYPE)
                        {
                            result = AV_ImageToVideo(_fileName);
                            if (result == NO_ERROR)
                            {
                                _imageMode = true; //_fileMode - don't set to false
                                result = sourceResolver.CreateObjectFromURL(_tempFileName, MFResolution.MediaSource, null, out objectType, out source);
                                if (result != NO_ERROR && _hasTempFile) AV_RemoveTempFile();
                            }
                        }
                    }

                    mf_MediaSource = (IMFMediaSource)source;
                    Marshal.ReleaseComObject(sourceResolver);

#pragma warning restore IDE0059 // Unnecessary assignment of a value

                }
            }

            if (result == NO_ERROR)
            {
                IMFPresentationDescriptor sourcePD = null;
                try
                {
                    result = mf_MediaSource.CreatePresentationDescriptor(out sourcePD);
                    if (result == NO_ERROR)
                    {
                        if (mf_Replay && _fileMode) // || _liveStreamMode)) // = not webcam or mic
                        {
                            bool getNewLength = false;

                            if (_audioTrackCurrent != _audioTrackBase)
                            {
                                sourcePD.DeselectStream(_audioTracks[_audioTrackBase].StreamIndex);
                                sourcePD.SelectStream(_audioTracks[_audioTrackCurrent].StreamIndex);
                                getNewLength = true;
                            }

                            if (_videoTrackCurrent != _videoTrackBase)
                            {
                                sourcePD.DeselectStream(_videoTracks[_videoTrackBase].StreamIndex);
                                sourcePD.SelectStream(_videoTracks[_videoTrackCurrent].StreamIndex);
                                getNewLength = true;
                            }

                            if (getNewLength)
                            {
                                sourcePD.GetUINT64(MFAttributesClsid.MF_PD_DURATION, out _mediaLength);
                                //if (result == NO_ERROR && _mediaLength == 0) result = HResult.MF_E_NO_DURATION;
                                if (_mediaLength <= 0)
                                {
                                    _fileMode = false;
                                    _liveStreamMode = true;
                                }
                                else
                                {
                                    try { if (!new Uri(_fileName).IsFile) { _fileStreamMode = true; } }
                                    catch { /* ignore */ }
                                }
                            }
                        }
                        else
                        {
                            if (_fileMode)
                            {
                                sourcePD.GetUINT64(MFAttributesClsid.MF_PD_DURATION, out _mediaLength);
                                if (_mediaLength <= _stopTime)
                                {
                                    _stopTime = 0;
                                    if (_mediaStartStopTimeChanged != null) _mediaStartStopTimeChanged(this, EventArgs.Empty);
                                }
                                if (_mediaLength <= 0)
                                {
                                    _fileMode = false;
                                    _liveStreamMode = true;
                                }
                                else
                                {
                                    try { if (!new Uri(_fileName).IsFile) { _fileStreamMode = true; } }
                                    catch { /* ignore */ }
                                }
                            }

                            if (result == NO_ERROR)
                            {
                                int count = 0;
                                result = sourcePD.GetStreamDescriptorCount(out count);
                                if (result == NO_ERROR && count == 0) result = HResult.MF_E_MEDIA_SOURCE_NO_STREAMS_SELECTED;

                                if (result == NO_ERROR)
                                {
                                    _audioTracks = new AudioStream[count];
                                    _audioTrackCount = 0;
                                    _audioTrackBase = NO_STREAM_SELECTED;
                                    _audioTrackCurrent = NO_STREAM_SELECTED;

                                    _videoTracks = new VideoStream[count];
                                    _videoTrackCount = 0;
                                    _videoTrackBase = NO_STREAM_SELECTED;
                                    _videoTrackCurrent = NO_STREAM_SELECTED;

                                    IMFStreamDescriptor sourceSD;
                                    IMFMediaTypeHandler typeHandler = null;
                                    IMFMediaType type = null;

                                    Guid guidMajorType;
                                    bool selected;
                                    HResult hasName;
                                    HResult hasLanguage;
                                    int length;

                                    // Get the streams
                                    for (int i = 0; i < count; i++)
                                    {
                                        sourcePD.GetStreamDescriptorByIndex(i, out selected, out sourceSD);
                                        try
                                        {
                                            sourceSD.GetMediaTypeHandler(out typeHandler);
                                            typeHandler.GetMajorType(out guidMajorType);

                                            hasName = sourceSD.GetString(MFAttributesClsid.MF_SD_STREAM_NAME, _textBuffer1, _textBuffer1.Capacity, out length);
                                            hasLanguage = sourceSD.GetString(MFAttributesClsid.MF_SD_LANGUAGE, _textBuffer2, _textBuffer2.Capacity, out length);

                                            if (guidMajorType == MFMediaType.Audio)
                                            {
                                                _audioTracks[_audioTrackCount].StreamIndex = i;
                                                _audioTracks[_audioTrackCount].Selected = selected;
                                                if (!_audioCut && selected && _audioTrackBase == NO_STREAM_SELECTED)
                                                {
                                                    _audioTrackBase = _audioTrackCount;
                                                    _audioTrackCurrent = _audioTrackCount;
                                                }

                                                if (hasName == NO_ERROR) _audioTracks[_audioTrackCount].Name = _textBuffer1.ToString();
                                                else
                                                {
                                                    if (_webcamMode || _micMode)
                                                    {
                                                        _textBuffer1.Length = 0;
                                                        _textBuffer1.Append(_micDevice._name).Append(" (").Append(_micDevice._adapter).Append(")");
                                                        _audioTracks[_audioTrackCount].Name = _textBuffer1.ToString();
                                                        _textBuffer1.Length = 0;
                                                    }
                                                    else _audioTracks[_audioTrackCount].Name = AUDIO_TRACK_NAME + (_audioTrackCount + 1);
                                                }

                                                if (hasLanguage == NO_ERROR) _audioTracks[_audioTrackCount].Language = _textBuffer2.ToString();
                                                else _audioTracks[_audioTrackCount].Language = string.Empty;

                                                typeHandler.GetCurrentMediaType(out type);
                                                if (type != null)
                                                {
                                                    int avgBytes;

                                                    type.GetGUID(MFAttributesClsid.MF_MT_SUBTYPE, out _audioTracks[_audioTrackCount].MediaType);
                                                    type.GetUINT32(MFAttributesClsid.MF_MT_AUDIO_NUM_CHANNELS, out _audioTracks[_audioTrackCount].ChannelCount);
                                                    type.GetUINT32(MFAttributesClsid.MF_MT_AUDIO_SAMPLES_PER_SECOND, out _audioTracks[_audioTrackCount].Samplerate);
                                                    type.GetUINT32(MFAttributesClsid.MF_MT_AUDIO_BITS_PER_SAMPLE, out _audioTracks[_audioTrackCount].Bitdepth);
                                                    type.GetUINT32(MFAttributesClsid.MF_MT_AUDIO_AVG_BYTES_PER_SECOND, out avgBytes);
                                                    _audioTracks[_audioTrackCount].Bitrate = (int)((avgBytes * 0.008) + 0.5);
                                                }
                                                else
                                                {
                                                    _audioTracks[_audioTrackCount].ChannelCount = 2;
                                                }
                                                _audioTrackCount++;
                                            }
                                            else if (guidMajorType == MFMediaType.Video)
                                            {
                                                // allow only 1 webcam track
                                                if (!_webcamMode || _videoTrackCount == 0)
                                                {
                                                    _videoTracks[_videoTrackCount].StreamIndex = i;
                                                    _videoTracks[_videoTrackCount].Selected = selected;
                                                    if (!_videoCut && selected && _videoTrackBase == NO_STREAM_SELECTED)
                                                    {
                                                        _videoTrackBase = _videoTrackCount;
                                                        _videoTrackCurrent = _videoTrackCount;
                                                    }

                                                    if (hasName == NO_ERROR) _videoTracks[_videoTrackCount].Name = _textBuffer1.ToString();
                                                    else
                                                    {
                                                        if (_webcamMode) _videoTracks[_videoTrackCount].Name = _webcamDevice._name;
                                                        else _videoTracks[_videoTrackCount].Name = VIDEO_TRACK_NAME + (_videoTrackCount + 1);
                                                    }

                                                    if (hasLanguage == NO_ERROR) _videoTracks[_videoTrackCount].Language = _textBuffer2.ToString();
                                                    else _videoTracks[_videoTrackCount].Language = string.Empty;

                                                    typeHandler.GetCurrentMediaType(out type);
                                                    if (type != null)
                                                    {
                                                        int num, denum;

                                                        type.GetGUID(MFAttributesClsid.MF_MT_SUBTYPE, out _videoTracks[_videoTrackCount].MediaType);
                                                        MFExtern.MFGetAttributeRatio(type, MFAttributesClsid.MF_MT_FRAME_RATE, out num, out denum);
                                                        if (denum > 0) _videoTracks[_videoTrackCount].FrameRate = (float)(uint)num / denum;
                                                        MFExtern.MFGetAttributeRatio(type, MFAttributesClsid.MF_MT_FRAME_SIZE, out _videoTracks[_videoTrackCount].SourceWidth, out _videoTracks[_videoTrackCount].SourceHeight);
                                                    }
                                                    _videoTrackCount++;
                                                }
                                            }
                                            // we should get here other streams but Media Foundation 'silently' ignores them: only audio and video streams.
                                        }
                                        finally
                                        {
                                            if (type != null) { Marshal.ReleaseComObject(type); type = null; }
                                            if (typeHandler != null) { Marshal.ReleaseComObject(typeHandler); typeHandler = null; }
                                            if (sourceSD != null) { Marshal.ReleaseComObject(sourceSD); sourceSD = null; }
                                        }
                                    }
                                    //if (mf_AudioStreamCount == 0 && mf_VideoStreamCount == 0) result = HResult.MF_E_MEDIA_SOURCE_NO_STREAMS_SELECTED;
                                }
                            }
                        }
                    }
                }
                catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
                finally { if (result != NO_ERROR && sourcePD != null) { Marshal.ReleaseComObject(sourcePD); sourcePD = null; } }

                // Set the topology
                if (result == NO_ERROR)
                {
                    result = SetTopology(sourcePD);
                    Marshal.ReleaseComObject(sourcePD); sourcePD = null;

                    // Get controls and start the session
                    if (result == NO_ERROR)
                    {
                        mf_MediaSession.GetClock(out mf_Clock);
                        try
                        {
                            if (_fileMode)
                            {
                                mf_StartTime.type = ConstPropVariant.VariantType.Int64;
                                mf_StartTime.longValue = _startTime;
                            }
                            else mf_StartTime.type = ConstPropVariant.VariantType.None;
                            result = mf_MediaSession.Start(Guid.Empty, mf_StartTime);

                            if (result == NO_ERROR)
                            {
                                mf_HasSession = true;

                                if (mf_DoEvents)
                                {
                                    mf_AwaitDoEvents = true;
                                    for (int i = 0; i < mf_DoEventsLoop && mf_AwaitDoEvents; i++)
                                    {
                                        System.Threading.Thread.Sleep(10);
                                        Application.DoEvents();
                                    }
                                    if (mf_AwaitDoEvents) _lastError = HResult.COR_E_TIMEOUT;
                                }
                                else
                                {
                                    mf_AwaitCallback = true;
                                    WaitForEvent.WaitOne(TIMEOUT_30_SECONDS);
                                }

                                if (_lastError != NO_ERROR)
                                {
                                    if (_lastError == HResult.MF_E_HW_MFT_FAILED_START_STREAMING) result = HResult.ERROR_BUSY;
                                    else result = _lastError;
                                }

                                if (result == NO_ERROR)
                                {
                                    if (_paused)
                                    {
                                        mf_MediaSession.Pause();
                                        mf_AwaitCallback = true;
                                        WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);

                                        if (_hasTaskbarProgress) _taskbarProgress.SetState(TaskbarProgressState.Paused);
                                    }
                                    else if (_hasTaskbarProgress && !_fileMode) _taskbarProgress.SetState(TaskbarProgressState.Indeterminate);

                                    // check speed (for update topo)
                                    if (_speed != DEFAULT_SPEED)
                                    {
                                        float trueSpeed;
                                        mf_RateControl.GetRate(_speedBoost, out trueSpeed);
                                        if (_speed != trueSpeed)
                                        {
                                            _speed = trueSpeed == 0 ? 1 : trueSpeed;
                                            mf_Speed = _speed;
                                            if (_speedSlider != null) SpeedSlider_ValueToSlider(_speed);
                                            if (_mediaSpeedChanged != null) _mediaSpeedChanged(this, EventArgs.Empty);
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
                    }
                }
            }

            dc_PaintBusy = false;

            if (result == NO_ERROR)
            {
                if (!mf_Replay)
                {
                    if (_hasDisplay) _display.Invalidate();
                    Application.DoEvents();
                }

                _playing = true;
                //if (_mediaStarted != null)
                //{
                //    _lastError = result;
                //    _mediaStarted(this, EventArgs.Empty);
                //}

                if (_displayHold) AV_ClearHold();
            }
            else
            {
                _lastError = result;
                AV_CloseSession(false, false, StopReason.Error); // with no MediaEnded event
            }

            _lastError = result;
            return result;
        }

        private HResult SetTopology(IMFPresentationDescriptor sourcePD)
        {
            IMFTopology topology = null;
            IMFStreamDescriptor sourceSD = null;

            IMFTopologyNode sourceNode = null;
            IMFTopologyNode outputNode = null;

            IMFActivate rendererActivate = null;

            bool selected;
            HResult result = NO_ERROR;

            _hasAudio = false;
            _hasVideo = false;

            bool setAudio = false;
            bool setVideo = false;

            mf_AudioVolume = AUDIO_VOLUME_DEFAULT;
            mf_AudioBalance = AUDIO_BALANCE_DEFAULT;
            mf_Speed = DEFAULT_SPEED;

            _mediaChannelCount = 0;
            _audioChannelCount = 0;

            try
            {
                result = MFExtern.MFCreateTopology(out topology);
                if (result == NO_ERROR)
                {
                    // Set Audio Stream
                    if (_audioTrackCurrent != NO_STREAM_SELECTED)
                    {
                        try
                        {
                            IMMDeviceCollection deviceCollection;
                            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
                            deviceEnumerator.EnumAudioEndpoints(EDataFlow.eRender, (uint)DeviceState.Active, out deviceCollection);
                            Marshal.ReleaseComObject(deviceEnumerator); deviceEnumerator = null;

                            if (deviceCollection != null)
                            {
                                uint deviceCount;
                                deviceCollection.GetCount(out deviceCount);
                                Marshal.ReleaseComObject(deviceCollection); deviceCollection = null;

                                if (deviceCount > 0)
                                {
                                    sourcePD.GetStreamDescriptorByIndex(_audioTracks[_audioTrackCurrent].StreamIndex, out selected, out sourceSD);

                                    MFExtern.MFCreateTopologyNode(MFTopologyType.SourcestreamNode, out sourceNode);
                                    sourceNode.SetUnknown(MFAttributesClsid.MF_TOPONODE_SOURCE, mf_MediaSource);
                                    sourceNode.SetUnknown(MFAttributesClsid.MF_TOPONODE_PRESENTATION_DESCRIPTOR, sourcePD);
                                    sourceNode.SetUnknown(MFAttributesClsid.MF_TOPONODE_STREAM_DESCRIPTOR, sourceSD);

                                    MFExtern.MFCreateTopologyNode(MFTopologyType.OutputNode, out outputNode);

                                    MFExtern.MFCreateAudioRendererActivate(out rendererActivate);
                                    if (_audioDevice != null)
                                    {
                                        rendererActivate.SetString(MFAttributesClsid.MF_AUDIO_RENDERER_ATTRIBUTE_ENDPOINT_ID, _audioDevice._id);
                                    }
                                    outputNode.SetObject(rendererActivate);

                                    topology.AddNode(sourceNode);
                                    topology.AddNode(outputNode);
                                    sourceNode.ConnectOutput(0, outputNode, 0);

                                    _mediaChannelCount = _audioTracks[_audioTrackCurrent].ChannelCount;
                                    setAudio = true;
                                }
                            }
                        }
                        finally
                        {
                            if (sourceSD != null) { Marshal.ReleaseComObject(sourceSD); sourceSD = null; }
                            if (sourceNode != null) { Marshal.ReleaseComObject(sourceNode); sourceNode = null; }
                            if (outputNode != null) { Marshal.ReleaseComObject(outputNode); outputNode = null; }
                            if (rendererActivate != null) { Marshal.ReleaseComObject(rendererActivate); rendererActivate = null; }
                        }
                    }

                    // Set Video Stream
                    if (_videoTrackCurrent != NO_STREAM_SELECTED && _display != null)
                    {
                        try
                        {
                            _hasVideo = true; // put here because of overlay resizing

                            sourcePD.GetStreamDescriptorByIndex(_videoTracks[_videoTrackCurrent].StreamIndex, out selected, out sourceSD);

                            MFExtern.MFCreateTopologyNode(MFTopologyType.SourcestreamNode, out sourceNode);
                            sourceNode.SetUnknown(MFAttributesClsid.MF_TOPONODE_SOURCE, mf_MediaSource);
                            sourceNode.SetUnknown(MFAttributesClsid.MF_TOPONODE_PRESENTATION_DESCRIPTOR, sourcePD);
                            sourceNode.SetUnknown(MFAttributesClsid.MF_TOPONODE_STREAM_DESCRIPTOR, sourceSD);

                            MFExtern.MFCreateTopologyNode(MFTopologyType.OutputNode, out outputNode);

                            if (!_hasVideoDisplay)
                            {
                                _videoDisplay.SetBounds(0, 0, 32, 32);
                                _display.Controls.Add(_videoDisplay);
                                _videoDisplay.SendToBack();
                                _hasVideoDisplay = true;
                            }
                            MFExtern.MFCreateVideoRendererActivate(_videoDisplay.Handle, out rendererActivate);
                            outputNode.SetObject(rendererActivate);

                            topology.AddNode(sourceNode);
                            topology.AddNode(outputNode);
                            sourceNode.ConnectOutput(0, outputNode, 0);

                            _videoSourceSize.Width = _videoTracks[_videoTrackCurrent].SourceWidth;
                            _videoSourceSize.Height = _videoTracks[_videoTrackCurrent].SourceHeight;

                            _videoFrameRate = _videoTracks[_videoTrackCurrent].FrameRate;
                            if (_videoFrameRate > 0) _videoFrameStep = (long)(ONE_SECOND_TICKS / _videoFrameRate);
                            else _videoFrameStep = AUDIO_STEP_TICKS;

                            setVideo = true;
                        }
                        finally
                        {
                            if (sourceSD != null) { Marshal.ReleaseComObject(sourceSD); sourceSD = null; }
                            if (sourceNode != null) { Marshal.ReleaseComObject(sourceNode); sourceNode = null; }
                            if (outputNode != null) { Marshal.ReleaseComObject(outputNode); outputNode = null; }
                            if (rendererActivate != null) { Marshal.ReleaseComObject(rendererActivate); rendererActivate = null; }
                        }
                    }
                }
            }
            catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
            finally { if (result != NO_ERROR && topology != null) { Marshal.ReleaseComObject(topology); topology = null; } }

            if (result == NO_ERROR)
            {
                topology.SetUINT64(MFAttributesClsid.MF_TOPOLOGY_HARDWARE_MODE, 1);
                topology.SetUINT64(MFAttributesClsid.MF_TOPOLOGY_DXVA_MODE, 2);
                topology.SetUINT64(MFAttributesClsid.MF_TOPOLOGY_PROJECTSTART, 0);

                if (_stopTime == 0) topology.SetUINT64(MFAttributesClsid.MF_TOPOLOGY_PROJECTSTOP, long.MaxValue);
                else topology.SetUINT64(MFAttributesClsid.MF_TOPOLOGY_PROJECTSTOP, _stopTime);

                result = mf_MediaSession.SetTopology(MFSessionSetTopologyFlags.Immediate, topology);
                Marshal.ReleaseComObject(topology); topology = null;

                if (result == NO_ERROR)
                {
                    mf_AwaitCallback = true;
                    if (!WaitForEvent.WaitOne(TIMEOUT_15_SECONDS)) result = HResult.MF_E_TOPO_CANNOT_CONNECT;
                    else result = _lastError;
                    
                    System.Threading.Thread.Sleep(MF_UPDATE_WAIT_MS);

                    if (result == HResult.MF_E_NO_AUDIO_PLAYBACK_DEVICE && setVideo)
                    {
                        setAudio = false;
                        _lastError = NO_ERROR; // play video without audio device
                        result = NO_ERROR;
                    }

                    if (result == NO_ERROR)
                    {
                        if (setVideo)
                        {
                            object displayControl;
                            result = MFExtern.MFGetService(mf_MediaSession, MFServices.MR_VIDEO_RENDER_SERVICE, typeof(IMFVideoDisplayControl).GUID, out displayControl);
                            if (result == NO_ERROR)
                            {
                                mf_VideoDisplayControl = displayControl as IMFVideoDisplayControl;
                                mf_VideoDisplayControl.SetAspectRatioMode(MFVideoAspectRatioMode.None);
                                mf_VideoDisplayControl.SetRenderingPrefs(MFVideoRenderPrefs.DoNotRepaintOnStop | MFVideoRenderPrefs.DoNotRenderBorder | MFVideoRenderPrefs.DoNotClipToDevice);

                                if (_setVideoColor)
                                {
                                    if (_brightness != 0) MF_SetBrightness(_brightness);
                                    if (_contrast != 0) MF_SetContrast(_contrast);
                                    if (_hue != 0) MF_SetHue(_hue);
                                    if (_saturation != 0) MF_SetSaturation(_saturation);
                                }
                            }
                            else _hasVideo = false;
                            //if (!mf_Replay) result = NO_ERROR; // do not delete
                        }

                        if (result == NO_ERROR)
                        {
                            if (setAudio)
                            {
                                object streamVolume;
                                result = MFExtern.MFGetService(mf_MediaSession, MFServices.MR_STREAM_VOLUME_SERVICE, typeof(IMFAudioStreamVolume).GUID, out streamVolume);
                                if (result == NO_ERROR)
                                {
                                    mf_AudioStreamVolume = streamVolume as IMFAudioStreamVolume;
                                    mf_AudioStreamVolume.GetChannelCount(out _audioChannelCount);
                                    _hasAudio = true;

                                    if (_audioEnabled)
                                    {
                                        AV_SetAudioVolume(_audioVolume, false, false);
                                        AV_SetAudioBalance(_audioBalance, false, false);
                                    }
                                    else
                                    {
                                        mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsMute);
                                    }
                                }
                                else _hasAudio = false;
                                //if (!mf_Replay) result = NO_ERROR; // do not delete
                            }

                            if (result == NO_ERROR)
                            {
                                // Speed
                                object rate;
                                result = MFExtern.MFGetService(mf_MediaSession, MFServices.MF_RATE_CONTROL_SERVICE, typeof(IMFRateControl).GUID, out rate);
                                if (result == NO_ERROR)
                                {
                                    mf_RateControl = rate as IMFRateControl;

                                    ((IMFRateSupport)mf_RateControl).GetFastestRate(MFRateDirection.Forward, _speedBoost, out mf_SpeedMaximum);
                                    ((IMFRateSupport)mf_RateControl).GetSlowestRate(MFRateDirection.Forward, _speedBoost, out mf_SpeedMinimum);

                                    // Reverse speed - can't set reverse speed?
                                    //if (((IMFRateSupport)mf_RateControl).GetFastestRate(MFRateDirection.Reverse, false, out mf_SpeedReverseMaximum) == NO_ERROR)
                                    //{
                                    //    ((IMFRateSupport)mf_RateControl).GetSlowestRate(MFRateDirection.Reverse, false, out mf_SpeedReverseMinimum);
                                    //}
                                    //else if (((IMFRateSupport)mf_RateControl).GetFastestRate(MFRateDirection.Reverse, true, out mf_SpeedReverseMaximum) == NO_ERROR)
                                    //{
                                    //    ((IMFRateSupport)mf_RateControl).GetSlowestRate(MFRateDirection.Reverse, true, out mf_SpeedReverseMinimum);
                                    //    mf_SpeedReverseThin = true;
                                    //}
                                    //else mf_SpeedReverse = false;

                                    if (_speed != DEFAULT_SPEED)
                                    {
                                        bool speedChanged = false;

                                        if (_speed < mf_SpeedMinimum) { _speed = mf_SpeedMinimum; speedChanged = true; }
                                        else if (_speed > mf_SpeedMaximum) { _speed = mf_SpeedMaximum; speedChanged = true; }

                                        if (_speed != mf_Speed)
                                        {
                                            if (mf_RateControl.SetRate(_speedBoost, _speed) == NO_ERROR)
                                            {
                                                mf_AwaitCallback = true;
                                                WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);
                                                mf_Speed = _speed;
                                            }
                                        }
                                        if (speedChanged)
                                        {
                                            if (_speedSlider != null) SpeedSlider_ValueToSlider(_speed);
                                            if (_mediaSpeedChanged != null) _mediaSpeedChanged(this, EventArgs.Empty);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        // used with changing audio device, tracks, stopTime while playing
        internal void AV_UpdateTopology()
        {
            if (_busyStarting)
            {
                _lastError = HResult.MF_E_STATE_TRANSITION_PENDING;
                return;
            }
            _lastError = NO_ERROR;

            _busyStarting = true;
            mf_Replay     = true;

            _playing = false;
            _stepMode = false;
            _timer.Stop();

            if (mf_VideoDisplayControl != null) { Marshal.ReleaseComObject(mf_VideoDisplayControl); mf_VideoDisplayControl = null; }
            if (mf_VideoProcessor != null) { Marshal.ReleaseComObject(mf_VideoProcessor); mf_VideoProcessor = null; }
            if (mf_AudioStreamVolume != null) { Marshal.ReleaseComObject(mf_AudioStreamVolume); mf_AudioStreamVolume = null; }
            //if (mf_AudioSimpleVolume != null) { Marshal.ReleaseComObject(mf_AudioSimpleVolume); mf_AudioSimpleVolume = null; }
            if (mf_RateControl != null) { Marshal.ReleaseComObject(mf_RateControl); mf_RateControl = null; }

            _hasVideoProcessor  = false;
            _failVideoProcessor = false;
            _hasBrightnessRange = false;
            _hasContrastRange   = false;
            _hasHueRange        = false;
            _hasSaturationRange = false;

            long oldStartTime = _startTime;
            if (_fileMode)
            {
                long presTime, sysTime;
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                mf_Clock.GetCorrelatedTime(0, out presTime, out sysTime);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                _startTime = presTime;
                if (!_paused) _startTime += 500000;
            }

            mf_MediaSession.Close();
            mf_AwaitCallback = true;
            WaitForEvent.WaitOne(TIMEOUT_15_SECONDS);

            // if webcamMode
            Application.DoEvents();

            mf_MediaSource.Shutdown();
            mf_MediaSession.Shutdown();

            Marshal.ReleaseComObject(mf_MediaSource); mf_MediaSource = null;
            Marshal.ReleaseComObject(mf_MediaSession); mf_MediaSession = null;

            // is this necessary?
            if (_webcamAggregated)
            {
                if (_webcamAudioSource != null)
                {
                    _webcamAudioSource.Shutdown();
                    Marshal.ReleaseComObject(_webcamAudioSource);
                    _webcamAudioSource = null;
                }
                if (_webcamVideoSource != null)
                {
                    _webcamVideoSource.Shutdown();
                    Marshal.ReleaseComObject(_webcamVideoSource);
                    _webcamVideoSource = null;
                }
                _webcamAggregated = false;
            }

            mf_Clock = null;
            mf_HasSession = false;

            _hasVideoBounds = false;
            AV_PlayMedia();

            _startTime = oldStartTime;

            mf_Replay = false;
            _busyStarting = false;

            if (_lastError == NO_ERROR)
            {
                // TODO ?
                if (_paused)
                {
                    if (_hasAudio && _audioEnabled) mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsMute);

                    mf_StartTime.type = ConstPropVariant.VariantType.None;

                    _lastError = mf_MediaSession.Start(Guid.Empty, mf_StartTime);
                    mf_AwaitCallback = true;
                    WaitForEvent.WaitOne(TIMEOUT_10_SECONDS);

                    System.Threading.Thread.Sleep(MF_UPDATE_WAIT_MS);

                    _lastError = mf_MediaSession.Pause();
                    mf_AwaitCallback = true;
                    WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);

                    if (_hasAudio && _audioEnabled)
                    {
                        System.Threading.Thread.Sleep(MF_UPDATE_WAIT_MS);
                        mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsVolume);
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(MF_UPDATE_WAIT_MS);
                }
                StartMainTimerCheck();
            }
        }

        #endregion

        #region Private - Close Session / Remove TempFile / Clear Hold

        internal void AV_CloseSession(bool purge, bool stopped, StopReason reason)
        {
            if (mf_HasSession)
            {
                _timer.Stop();
                _playing = false;

                _endedEventArgs._mediaName  = _fileName;
                _endedEventArgs._sourceType = AV_GetSourceType();
                _endedEventArgs._error      = (int)_lastError;

                // ********************************

                _stepMode = false;
                if (_psTracking) PositionSlider_StopTracking();

                if (pm_HasPeakMeter)
                {
                    _outputLevelArgs._channelCount    = pm_PeakMeterChannelCount;
                    _outputLevelArgs._masterPeakValue = STOP_VALUE;
                    _outputLevelArgs._channelsValues  = pm_PeakMeterValuesStop;

                    _mediaPeakLevelChanged(this, _outputLevelArgs);
                }
                _hasPositionEvents = false;

                if (st_HasSubtitles) Subtitles_Stop();
                if (dc_HasDisplayClones) DisplayClones_Stop(false);
                if (_hasTaskbarProgress) _taskbarProgress.SetState(TaskbarProgressState.NoProgress);

                // ******************************** MF

                if (mf_VideoDisplayControl != null) { Marshal.ReleaseComObject(mf_VideoDisplayControl); mf_VideoDisplayControl = null; }
                if (mf_VideoProcessor != null)      { Marshal.ReleaseComObject(mf_VideoProcessor); mf_VideoProcessor = null; }
                if (mf_AudioStreamVolume != null)   { Marshal.ReleaseComObject(mf_AudioStreamVolume); mf_AudioStreamVolume = null; }
                if (mf_RateControl != null)         { Marshal.ReleaseComObject(mf_RateControl); mf_RateControl = null; }

                _hasVideoProcessor  = false;
                _failVideoProcessor = false;
                _hasBrightnessRange = false;
                _hasContrastRange   = false;
                _hasHueRange        = false;
                _hasSaturationRange = false;

                if (mf_Clock != null)
                {
                    Marshal.ReleaseComObject(mf_Clock);
                    mf_Clock = null;
                }

                if (mf_MediaSession != null)
                {
                    mf_MediaSession.Close();
                    //mf_AwaitCallback = true;
                    //WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);

                    mf_MediaSession.Shutdown();
                    Marshal.ReleaseComObject(mf_MediaSession);
                    mf_MediaSession = null;
                }

                if (mf_MediaSource != null)
                {
                    mf_MediaSource.Shutdown();
                    Marshal.ReleaseComObject(mf_MediaSource);
                    mf_MediaSource = null;

                    // is this necessary?
                    if (_webcamAggregated)
                    {
                        if (_webcamAudioSource != null)
                        {
                            _webcamAudioSource.Shutdown(); 
                            Marshal.ReleaseComObject(_webcamAudioSource); 
                            _webcamAudioSource = null;
                        }
                        if (_webcamVideoSource != null)
                        {
                            _webcamVideoSource.Shutdown();
                            Marshal.ReleaseComObject(_webcamVideoSource);
                            _webcamVideoSource = null;
                        }
                        _webcamAggregated = false;
                    }
                }

                if (_hasVideoDisplay && (!_displayHold || reason == StopReason.UserStop || reason == StopReason.Error))
                {
                    _display.Controls.Remove(_videoDisplay);
                    _hasVideoDisplay = false;
                }

                // ********************************

                if (_hasDisplay)
                {
                    if ((!_displayHold || reason == StopReason.UserStop || reason == StopReason.Error) && (stopped && reason != StopReason.AutoStop))
                    {
                        _display.Invalidate();
                        if (dc_HasDisplayClones)
                        {
                            for (int i = 0; i < dc_DisplayClones.Length; i++)
                            {
                                try
                                {
                                    if (dc_DisplayClones[i] != null && dc_DisplayClones[i].Control != null && dc_DisplayClones[i].Control.Visible)
                                    {
                                        dc_DisplayClones[i].Control.Invalidate();
                                    }
                                }
                                catch { /* ignore */ }
                            }
                        }
                    }
                    //else if (dc_HasDisplayClones)
                    //{
                    //    for (int i = 0; i < dc_DisplayClones.Length; i++)
                    //    {
                    //        try
                    //        {
                    //            if (dc_DisplayClones[i] != null && dc_DisplayClones[i].Control != null && dc_DisplayClones[i].Control.Visible)
                    //            {
                    //                dc_DisplayClones[i].Refresh = true;
                    //            }
                    //        }
                    //        catch { /* ignore */ }
                    //    }
                    //}
                }

                AV_RemoveDisplay(purge);
                Application.DoEvents();

                mf_HasSession       = false;

                bool startStopEvent = false;
                if ((_startTime != 0 || _stopTime != 0) && _fileMode && !mf_Replay && _mediaStartStopTimeChanged != null) startStopEvent = true;
                _startTime          = 0;
                _stopTime           = 0;
                _repeatCount        = 0;
                _mediaLength        = 0;
                if (startStopEvent) _mediaStartStopTimeChanged(this, EventArgs.Empty);

                _hasAudio           = false;
                _audioTracks        = null;
                _audioTrackCount    = 0;
                _audioTrackBase     = NO_STREAM_SELECTED;
                _audioTrackCurrent  = NO_STREAM_SELECTED;
                //mf_AudioFrameRate = 0;
                //mf_AudioFrameStep = 0;
                _mediaChannelCount  = 0;

                _hasVideo           = false;
                _videoTracks        = null;
                _videoTrackCount    = 0;
                _videoTrackCurrent  = NO_STREAM_SELECTED;
                _videoTrackBase     = NO_STREAM_SELECTED;
                _videoFrameRate     = 0;
                _videoFrameStep     = 0;

                //mf_Speed            = DEFAULT_SPEED;
                mf_SpeedMinimum     = DEFAULT_SPEED_MINIMUM;
                mf_SpeedMaximum     = DEFAULT_SPEED_MAXIMUM;

                //mf_SpeedReverse     = true;
                //mf_SpeedReverseThin = false;
                //mf_SpeedReverseMinimum = DEFAULT_SPEED_REVERSE_MINIMUM;
                //mf_SpeedReverseMaximum = DEFAULT_SPEED_REVERSE_MAXIMUM;

                //if (_displayMode != DisplayMode.Manual)
                //{
                //    _hasVideoBounds = false;
                //    _videoBounds = Rectangle.Empty;
                //}

                if (_hasPositionSlider)
                {
                    _positionSlider.Value = _positionSlider.Minimum;
                    _positionSlider.Enabled = false;
                }
                //if (_hasShuttleSlider) _shuttleSlider.Enabled = false;

                if (_hasTempFile) AV_RemoveTempFile();

                if (_fileMode && !mf_Replay && _mediaPositionChanged != null) OnMediaPositionChanged();

                _fileMode       = false;
                _webcamMode     = false;
                _webcamDevice   = null;
                _webcamFormat   = null;
                _micMode        = false;
                _micDevice      = null;
                _fileStreamMode = false;
                _liveStreamMode = false;
                _imageMode      = false;

                if (stopped)
                {
                    if (_mediaEndedNotice != null)
                    {
                        _endedEventArgs._reason = reason;
                        _mediaEndedNotice(this, _endedEventArgs);
                    }
                    if (_mediaEnded != null)
                    {
                        _endedEventArgs._reason = reason;
                        _mediaEnded(this, _endedEventArgs);
                    }
                }
            }
            _lastError = NO_ERROR;
        }

        private void AV_RemoveTempFile()
        {
            if (_hasTempFile)
            {
                try { File.Delete(_tempFileName); }
                catch { /* ignore */ }

                _hasTempFile = false;
                _tempFileName = string.Empty;
            }
        }

        internal void AV_ClearHold()
        {
            // already checked for _displayHold
            //if (_displayHold && !_playing && _hasVideoDisplay)

            if (!_playing && _hasVideoDisplay)
            {
                _display.Controls.Remove(_videoDisplay);
                _hasVideoDisplay = false;
                _display.Invalidate();

                if (dc_HasDisplayClones)
                {
                    for (int i = 0; i < dc_DisplayClones.Length; i++)
                    {
                        try
                        {
                            if (dc_DisplayClones[i] != null && dc_DisplayClones[i].Control != null && dc_DisplayClones[i].Control.Visible)
                            {
                                dc_DisplayClones[i].Control.Invalidate();
                            }
                        }
                        catch { /* ignore */ }
                    }
                }
            }
        }

        #endregion


        // ******************************** Player - Various Private Members

        #region Private - Device Information

        internal static void GetDeviceInfo(IMMDevice device, DeviceInfo deviceInfo)
        {
            //if (device != null && deviceInfo != null)
            {
                device.GetId(out deviceInfo._id);

                IPropertyStore store;
                device.OpenPropertyStore((uint)STGM.STGM_READ, out store);

                if (store != null)
                {
                    PropVariant property = new PropVariant();

                    store.GetValue(PropertyKeys.PKEY_Device_Description, property);
                    deviceInfo._name = (string)property;

                    store.GetValue(PropertyKeys.PKEY_DeviceInterface_FriendlyName, property);
                    deviceInfo._adapter = (string)property;

                    property.Dispose();
                    Marshal.ReleaseComObject(store);
                }
            }
        }

        #endregion

        #region Private - Video

        internal void AV_CreateVideoProcessor()
        {
            if (mf_MediaSession == null || _hasVideoProcessor || _failVideoProcessor) return;

            object videoProcessor;
            HResult result = MFExtern.MFGetService(mf_MediaSession, MFServices.MR_VIDEO_MIXER_SERVICE, typeof(IMFVideoProcessor).GUID, out videoProcessor);
            if (result == NO_ERROR)
            {
                mf_VideoProcessor = videoProcessor as IMFVideoProcessor;

                Guid processorMode = Guid.Empty;
                int processorModeCount; // wrong value?
                Guid[] processorModes;

#pragma warning disable IDE0059 // Unnecessary assignment of a value
                result = mf_VideoProcessor.GetAvailableVideoProcessorModes(out processorModeCount, out processorModes);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                if (result == NO_ERROR && processorModes != null && processorModes.Length > 0)
                {
                    processorMode = processorModes[0];
                    if (!_playing) result = mf_VideoProcessor.SetVideoProcessorMode(processorMode);
                }

                if (result == NO_ERROR)
                {
                    // TODO - also get video effects/filters?
                    result = mf_VideoProcessor.GetVideoProcessorCaps(processorMode, out _videoProcessorCaps);
                    if (result == NO_ERROR)
                    {
                        _hasVideoProcessor = true;
                    }
                }

                if (result != NO_ERROR)
                {
                    Marshal.ReleaseComObject(mf_VideoProcessor);
                    mf_VideoProcessor = null;
                    _failVideoProcessor = true;
                }
            }
        }

        internal void AV_SetBrightness(double brightness, bool setSlider)
        {
            if (brightness < VIDEO_COLOR_MINIMUM || brightness > VIDEO_COLOR_MAXIMUM)
            {
                _lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else
            {
                if (brightness != _brightness && _hasVideo) _lastError = MF_SetBrightness(brightness);
                else _lastError = NO_ERROR;

                if (_lastError == NO_ERROR)
                {
                    _brightness = brightness;
                    _setVideoColor = _brightness != 0 || _contrast != 0 || _hue != 0 || _saturation != 0;

                    if (_mediaVideoColorChanged != null)
                    {
                        VideoColorEventArgs e = new VideoColorEventArgs(VideoColorAttribute.Brightness, brightness);
                        _mediaVideoColorChanged(this, e);
                    }

                    if (_brightnessSlider != null && setSlider)
                    {
                        _brightnessSlider.Value = (int)(brightness * 100);
                    }
                }
            }
        }

        internal HResult MF_SetBrightness(double brightness)
        {
            HResult result = HResult.MF_E_NOT_AVAILABLE;

            if (!_hasVideoProcessor && !_failVideoProcessor) AV_CreateVideoProcessor();
            if (_hasVideoProcessor)
            {
                if ((_videoProcessorCaps.ProcAmpControlCaps & (int)DXVA2ProcAmp.Brightness) != 0)
                {
                    if (!_hasBrightnessRange)
                    {
                        mf_VideoProcessor.GetProcAmpRange(DXVA2ProcAmp.Brightness, out _brightnessRange);
                        _hasBrightnessRange = true;
                    }

                    int trueValue;
                    if (brightness == 0) trueValue = _brightnessRange.DefaultValue;
                    else if (brightness < 0) trueValue = (int)(_brightnessRange.DefaultValue - Math.Abs(brightness * (_brightnessRange.DefaultValue - _brightnessRange.MinValue)));
                    else trueValue = (int)(_brightnessRange.DefaultValue + Math.Abs(brightness * (_brightnessRange.MaxValue - _brightnessRange.DefaultValue)));

                    _procAmpValues.Brightness = trueValue;
                    result = mf_VideoProcessor.SetProcAmpValues(DXVA2ProcAmp.Brightness, _procAmpValues);

                    if (result == NO_ERROR) mf_VideoDisplayControl.RepaintVideo();
                }
            }

            if (result != NO_ERROR) _brightness = 0;
            return result;
        }

        internal void AV_SetContrast(double contrast, bool setSlider)
        {
            if (contrast < VIDEO_COLOR_MINIMUM || contrast > VIDEO_COLOR_MAXIMUM)
            {
                _lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else
            {
                if (contrast != _contrast && _hasVideo) _lastError = MF_SetContrast(contrast);
                else _lastError = NO_ERROR;

                if (_lastError == NO_ERROR)
                {
                    _contrast = contrast;
                    _setVideoColor = _brightness != 0 || _contrast != 0 || _hue != 0 || _saturation != 0;

                    if (_mediaVideoColorChanged != null)
                    {
                        VideoColorEventArgs e = new VideoColorEventArgs(VideoColorAttribute.Contrast, contrast);
                        _mediaVideoColorChanged(this, e);
                    }

                    if (_contrastSlider != null && setSlider)
                    {
                        _contrastSlider.Value = (int)(contrast * 100);
                    }
                }
            }
        }

        internal HResult MF_SetContrast(double contrast)
        {
            HResult result = HResult.MF_E_NOT_AVAILABLE;

            if (!_hasVideoProcessor && !_failVideoProcessor) AV_CreateVideoProcessor();
            if (_hasVideoProcessor)
            {
                if ((_videoProcessorCaps.ProcAmpControlCaps & (int)DXVA2ProcAmp.Contrast) != 0)
                {
                    if (!_hasContrastRange)
                    {
                        mf_VideoProcessor.GetProcAmpRange(DXVA2ProcAmp.Contrast, out _contrastRange);
                        _hasContrastRange = true;
                    }

                    int trueValue;
                    if (contrast == 0) trueValue = _contrastRange.DefaultValue;
                    else if (contrast < 0) trueValue = (int)(_contrastRange.DefaultValue - Math.Abs(contrast * (_contrastRange.DefaultValue - _contrastRange.MinValue))) + 1;
                    else trueValue = (int)(_contrastRange.DefaultValue + Math.Abs(contrast * (_contrastRange.MaxValue - _contrastRange.DefaultValue)));

                    _procAmpValues.Contrast = trueValue;
                    result = mf_VideoProcessor.SetProcAmpValues(DXVA2ProcAmp.Contrast, _procAmpValues);

                    if (result == NO_ERROR) mf_VideoDisplayControl.RepaintVideo();
                }
            }

            if (result != NO_ERROR) _contrast = 0;
            return result;
        }

        internal void AV_SetHue(double hue, bool setSlider)
        {
            if (hue < VIDEO_COLOR_MINIMUM || hue > VIDEO_COLOR_MAXIMUM)
            {
                _lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else
            {
                if (hue != _hue && _hasVideo) _lastError = MF_SetHue(hue);
                else _lastError = NO_ERROR;

                if (_lastError == NO_ERROR)
                {
                    _hue = hue;
                    _setVideoColor = _brightness != 0 || _contrast != 0 || _hue != 0 || _saturation != 0;

                    if (_mediaVideoColorChanged != null)
                    {
                        VideoColorEventArgs e = new VideoColorEventArgs(VideoColorAttribute.Hue, hue);
                        _mediaVideoColorChanged(this, e);
                    }

                    if (_hueSlider != null && setSlider)
                    {
                        _hueSlider.Value = (int)(hue * 100);
                    }
                }
            }
        }

        internal HResult MF_SetHue(double hue)
        {
            HResult result = HResult.MF_E_NOT_AVAILABLE;

            if (!_hasVideoProcessor && !_failVideoProcessor) AV_CreateVideoProcessor();
            if (_hasVideoProcessor)
            {
                if ((_videoProcessorCaps.ProcAmpControlCaps & (int)DXVA2ProcAmp.Hue) != 0)
                {
                    if (!_hasHueRange)
                    {
                        mf_VideoProcessor.GetProcAmpRange(DXVA2ProcAmp.Hue, out _hueRange);
                        _hasHueRange = true;
                    }

                    int trueValue;
                    if (hue == 0) trueValue = _hueRange.DefaultValue;
                    else if (hue < 0) trueValue = (int)(_hueRange.DefaultValue - Math.Abs(hue * (_hueRange.DefaultValue - _hueRange.MinValue)));
                    else trueValue = (int)(_hueRange.DefaultValue + Math.Abs(hue * (_hueRange.MaxValue - _hueRange.DefaultValue)));

                    _procAmpValues.Hue = trueValue;
                    result = mf_VideoProcessor.SetProcAmpValues(DXVA2ProcAmp.Hue, _procAmpValues);

                    if (result == NO_ERROR) mf_VideoDisplayControl.RepaintVideo();
                }
            }

            if (result != NO_ERROR) _hue = 0;
            return result;
        }

        internal void  AV_SetSaturation(double saturation, bool setSlider)
        {
            if (saturation < VIDEO_COLOR_MINIMUM || saturation > VIDEO_COLOR_MAXIMUM)
            {
                _lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else
            {
                if (saturation != _saturation && _hasVideo) _lastError = MF_SetSaturation(saturation);
                else _lastError = NO_ERROR;

                if (_lastError == NO_ERROR)
                {
                    _saturation = saturation;
                    _setVideoColor = _brightness != 0 || _contrast != 0 || _hue != 0 || _saturation != 0;

                    if (_mediaVideoColorChanged != null)
                    {
                        VideoColorEventArgs e = new VideoColorEventArgs(VideoColorAttribute.Saturation, saturation);
                        _mediaVideoColorChanged(this, e);
                    }

                    if (_saturationSlider != null && setSlider)
                    {
                        _saturationSlider.Value = (int)(saturation * 100);
                    }
                }
            }
        }

        internal HResult MF_SetSaturation(double saturation)
        {
            HResult result = HResult.MF_E_NOT_AVAILABLE;

            if (!_hasVideoProcessor && !_failVideoProcessor) AV_CreateVideoProcessor();
            if (_hasVideoProcessor)
            {
                if ((_videoProcessorCaps.ProcAmpControlCaps & (int)DXVA2ProcAmp.Saturation) != 0)
                {
                    if (!_hasSaturationRange)
                    {
                        mf_VideoProcessor.GetProcAmpRange(DXVA2ProcAmp.Saturation, out _saturationRange);
                        _hasSaturationRange = true;
                    }

                    int trueValue;
                    if (saturation == 0) trueValue = _saturationRange.DefaultValue;
                    else if (saturation < 0) trueValue = (int)(_saturationRange.DefaultValue - Math.Abs(saturation * (_saturationRange.DefaultValue - _saturationRange.MinValue)));
                    else trueValue = (int)(_saturationRange.DefaultValue + Math.Abs(saturation * (_saturationRange.MaxValue - _saturationRange.DefaultValue)));

                    _procAmpValues.Saturation = trueValue;
                    result = mf_VideoProcessor.SetProcAmpValues(DXVA2ProcAmp.Saturation, _procAmpValues);

                    if (result == NO_ERROR) mf_VideoDisplayControl.RepaintVideo();
                }
            }

            if (result != NO_ERROR) _saturation = 0;
            return result;
        }

        #endregion

        #region Private - Audio

        internal void AV_SetAudioEnabled(bool enabled)
        {
            _lastError = NO_ERROR;

            if (enabled != _audioEnabled)
            {
                _audioEnabled = enabled;
                if (_hasAudio)
                {
                    if (enabled) mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsVolume);
                    else mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsMute);
                }
                if (_mediaAudioMuteChanged != null) _mediaAudioMuteChanged(this, EventArgs.Empty);
            }
        }

        internal void AV_SetAudioVolume(float volume, bool setSlider, bool signal)
        {
            if (volume < AUDIO_VOLUME_MINIMUM || volume > AUDIO_VOLUME_MAXIMUM)
            {
                _lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else
            {
                _lastError = NO_ERROR;

                if (volume != mf_AudioVolume)
                {
                    _audioVolume    = volume;
                    mf_AudioVolume  = volume;

                    for (int i = 0; i < MAX_AUDIO_CHANNELS; i++) { _audioChannelsVolume[i] = volume; }

                    if (_audioBalance != AUDIO_BALANCE_DEFAULT)
                    {
                        if (_audioBalance < 0) _audioChannelsVolume[1] = volume * (1 + _audioBalance);
                        else _audioChannelsVolume[0] = volume * (1 - _audioBalance);
                    }

                    if (_hasAudio && _audioEnabled) mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsVolume);

                    if (_volumeSlider != null && setSlider) _volumeSlider.Value = (int)(_audioVolume * 100);
                    if (_mediaAudioVolumeChanged != null && signal) _mediaAudioVolumeChanged(this, EventArgs.Empty);
                }
            }
        }

        internal void AV_SetAudioBalance(float balance, bool setSlider, bool signal)
        {
            if (balance < AUDIO_BALANCE_MINIMUM || balance > AUDIO_BALANCE_MAXIMUM)
            {
                _lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else
            {
                _lastError = NO_ERROR;

                if (balance != mf_AudioBalance)
                {
                    _audioBalance   = balance;
                    mf_AudioBalance = balance;

                    // TODO surround audio balance
                    if (_audioBalance <= 0)
                    {
                        _audioChannelsVolume[0] = _audioVolume;
                        _audioChannelsVolume[1] = _audioVolume * (1 + _audioBalance);
                        //_audioChannelsVolume[2] = _audioVolume;
                        //_audioChannelsVolume[3] = _audioVolume;
                        //_audioChannelsVolume[4] = _audioVolume;
                        //_audioChannelsVolume[5] = _audioVolume * (1 + _audioBalance);
                    }
                    else
                    {
                        _audioChannelsVolume[0] = _audioVolume * (1 - _audioBalance);
                        _audioChannelsVolume[1] = _audioVolume;
                        //_audioChannelsVolume[2] = _audioVolume;
                        //_audioChannelsVolume[3] = _audioVolume;
                        //_audioChannelsVolume[4] = _audioVolume * (1 - _audioBalance);
                        //_audioChannelsVolume[5] = _audioVolume;
                    }

                    if (_hasAudio && _audioEnabled) mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsVolume);

                    if (_balanceSlider != null && setSlider) _balanceSlider.Value = (int)(_audioBalance * 100);
                    if (_mediaAudioBalanceChanged != null && signal) _mediaAudioBalanceChanged(this, EventArgs.Empty);
                }
            }
        }

        internal void AV_SetAudioChannels(float[] volumes, float volume, float balance)
        {
            // only called from Player.Audio.ChannelVolumes with MAX_AUDIO_CHANNELS volumes

            for (int i = 0; i < MAX_AUDIO_CHANNELS; i++) _audioChannelsVolume[i] = volumes[i];

            if (_hasAudio && _audioEnabled) mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsVolume);

            if (volume != _audioVolume)
            {
                _audioVolume    = volume;
                mf_AudioVolume  = volume;
                if (_volumeSlider != null) _volumeSlider.Value = (int)(volume * 100);
                if (_mediaAudioVolumeChanged != null) _mediaAudioVolumeChanged(this, EventArgs.Empty);
            }

            if (balance != _audioBalance)
            {
                _audioBalance   = balance;
                mf_AudioBalance = balance;
                if (_balanceSlider != null) _balanceSlider.Value = (int)(balance * 100);
                if (_mediaAudioBalanceChanged != null) _mediaAudioBalanceChanged(this, EventArgs.Empty);
            }
        }

        internal void AV_SetTrack(int track, bool audioTrack)
        {
            _lastError = NO_ERROR;

            if (_playing)
            {
                bool setTrack = false;

                if (audioTrack)
                {
                    if (track < 0 || track >= _audioTrackCount) _lastError = HResult.MF_E_OUT_OF_RANGE;
                    else if (track != _audioTrackCurrent) setTrack = true;
                }
                else
                {
                    if (track < 0 || track >= _videoTrackCount) _lastError = HResult.MF_E_OUT_OF_RANGE;
                    else if (track != _videoTrackCurrent) setTrack = true;
                }

                if (setTrack)
                {
                    if (audioTrack) _audioTrackCurrent = track;
                    else _videoTrackCurrent = track;

                    AV_UpdateTopology();

                    if (_lastError == NO_ERROR)
                    {
                        if (audioTrack)
                        {
                            if (_mediaAudioTrackChanged != null) _mediaAudioTrackChanged(this, EventArgs.Empty);
                        }
                        else
                        {
                            if (_mediaVideoTrackChanged != null) _mediaVideoTrackChanged(this, EventArgs.Empty);
                        }
                    }
                    else
                    {
                        AV_CloseSession(false, true, StopReason.Error);
                    }
                }
            }
        }

        #endregion

        #region Private - Display

        internal HResult AV_SetDisplay(Control newDisplay, bool setAll)
        {
            bool changeDisplay      = false;
            bool oldFullScreen      = false;
            bool setPositionSlider  = false;
            bool setTaskbarProgress = false;
            bool setDragEnabled     = false;

            HResult retVal = NO_ERROR;

            if (newDisplay != _display)
            {
                if (_playing) // && newDisplay != null)
                {
                    _siDisplay          = newDisplay;
                    long oldStartTime   = _startTime;
                    _siStartTime        = PositionX;

                    if (_hasPositionSlider)
                    {
                        _hasPositionSlider  = false;
                        setPositionSlider   = true;
                    }

                    AV_CloseSession(false, false, StopReason.AutoStop);

                    if (setPositionSlider) _hasPositionSlider = true;

                    retVal      = AV_Play();
                    _startTime  = oldStartTime;
                }
                else
                {
                    if (_hasDisplay)
                    {
                        if (dc_HasDisplayClones && (newDisplay == null)) DisplayClones_Clear();
                        if (_hasDisplayShape && newDisplay == null) AV_RemoveDisplayShape(true);

                        changeDisplay       = true;
                        oldFullScreen       = _fullScreen;
                        Form oldOverlay     = _overlay;
                        bool oldHasOverlay  = _hasOverlay;

                        if (_hasTaskbarProgress)
                        {
                            TaskbarProgress.Remove(_display.FindForm());
                            setTaskbarProgress = true;
                        }

                        if (_dragEnabled)
                        {
                            Display.DragEnabled = false;
                            setDragEnabled = true;
                        }

                        AV_RemoveDisplay(true);

                        _overlay = oldOverlay;
                        _hasOverlay = oldHasOverlay;
                    }
                    if (newDisplay != null)
                    {
                        _display = newDisplay;
                        _hasDisplay = true;

                        AV_SetDisplayEvents();
                        Application.DoEvents();
                        if (setAll)
                        {
                            AV_SetFullScreen(changeDisplay ? oldFullScreen : _fullScreen);
                            AV_SetOverlay(_overlay);
                        }
                        if (setTaskbarProgress) TaskbarProgress.Add(_display.FindForm());
                        if (setDragEnabled) Display.DragEnabled = true;
                    }
                    if (dc_HasDisplayClones && _hasDisplay) DisplayClones_DisplayCheck();
                }
                if (_mediaDisplayChanged != null) _mediaDisplayChanged(this, EventArgs.Empty);
            }
            _lastError = retVal;
            return retVal;
        }

        internal void AV_RemoveDisplay(bool purge)
        {
            if (_hasDisplay)
            {
                if (_hasOverlay)
                {
                    if (purge)
                    {
                        AV_RemoveOverlay(true);
                    }
                    else
                    {
                        if (!_overlayHold) AV_RemoveOverlay(false);
                    }
                }
                if (purge)
                {
                    if (_playing && _hasVideo) AV_CloseSession(false, true, StopReason.AutoStop);

                    if (_fullScreen)        AV_ResetFullScreen();
                    if (_hasDisplayEvents)  AV_RemoveDisplayEvents();

                    _display    = null;
                    _hasDisplay = false;
                }
            }
        }

        internal void AV_GetDisplayModeSize(DisplayMode mode)
        {
            if (_hasDisplay)
            {
                int     left    = 0;
                int     top     = 0;
                int     width;
                int     height;

                double  difX;
                double  difY;

                if (_hasVideo)
                {
                    width   = _videoSourceSize.Width;
                    height  = _videoSourceSize.Height;
                }
                else
                {
                    width   = _display.DisplayRectangle.Width;
                    height  = _display.DisplayRectangle.Height;
                }

                switch (mode)
                {
                    case DisplayMode.Manual: // manual
                        left    = _videoBounds.Left;
                        top     = _videoBounds.Top;
                        height  = _videoBounds.Height;
                        width   = _videoBounds.Width;
                        break;

                    case DisplayMode.Center: // center
                        left    = (_display.DisplayRectangle.Width - width) / 2;
                        top     = (_display.DisplayRectangle.Height - height) / 2;
                        break;

                    case DisplayMode.Zoom:  // zoom
                        difX    = (double)_display.DisplayRectangle.Width / width;
                        difY    = (double)_display.DisplayRectangle.Height / height;

                        if (difX < difY)
                        {
                            width   = (int)((width * difX) + 0.5);
                            height  = (int)((height * difX) + 0.5);
                        }
                        else
                        {
                            width   = (int)((width * difY) + 0.5);
                            height  = (int)((height * difY) + 0.5);
                        }
                        break;

                    case DisplayMode.ZoomCenter: // zoom and center
                        difX = (double)_display.DisplayRectangle.Width / width;
                        difY = (double)_display.DisplayRectangle.Height / height;

                        if (difX < difY)
                        {
                            width = (int)((width * difX) + 0.5);
                            height = (int)((height * difX) + 0.5);
                            top = (_display.DisplayRectangle.Height - height) / 2;
                        }
                        else
                        {
                            width = (int)((width * difY) + 0.5);
                            height = (int)((height * difY) + 0.5);
                            left = (_display.DisplayRectangle.Width - width) / 2;
                        }
                        break;

                    case DisplayMode.Stretch: // stretch
                        width   = _display.DisplayRectangle.Width;
                        height  = _display.DisplayRectangle.Height;
                        break;

                    case DisplayMode.CoverCenter: // cover (fill but keep aspect ratio) and center
                        difX    = (double)_display.DisplayRectangle.Width / width;
                        difY    = (double)_display.DisplayRectangle.Height / height;

                        if (difX > difY)
                        {
                            width   = (int)((width * difX) + 0.5);
                            height  = (int)((height * difX) + 0.5);
                            top     = (_display.DisplayRectangle.Height - height) / 2;
                        }
                        else
                        {
                            width   = (int)((width * difY) + 0.5);
                            height  = (int)((height * difY) + 0.5);
                            left    = (_display.DisplayRectangle.Width - width) / 2;
                        }
                        break;

                    case DisplayMode.SizeToFit: // size to fit
                        if ((width - _display.DisplayRectangle.Width > 0) || (height - _display.DisplayRectangle.Height > 0))
                        {
                            difX    = (double)_display.DisplayRectangle.Width / width;
                            difY    = (double)_display.DisplayRectangle.Height / height;

                            if (difX < difY)
                            {
                                width   = (int)((width * difX) + 0.5);
                                height  = (int)((height * difX) + 0.5);
                            }
                            else
                            {
                                width   = (int)((width * difY) + 0.5);
                                height  = (int)((height * difY) + 0.5);
                            }
                        }
                        break;

                    case DisplayMode.SizeToFitCenter: // size to fit and center
                        if ((width - _display.DisplayRectangle.Width > 0) || (height - _display.DisplayRectangle.Height > 0))
                        {
                            difX    = (double)_display.DisplayRectangle.Width / width;
                            difY    = (double)_display.DisplayRectangle.Height / height;

                            if (difX < difY)
                            {
                                width   = (int)((width * difX) + 0.5);
                                height  = (int)((height * difX) + 0.5);
                                top     = (_display.DisplayRectangle.Height - height) / 2;
                            }
                            else
                            {
                                width   = (int)((width * difY) + 0.5);
                                height  = (int)((height * difY) + 0.5);
                                left    = (_display.DisplayRectangle.Width - width) / 2;
                            }
                        }
                        else
                        {
                            left    = (_display.DisplayRectangle.Width - width) / 2;
                            top     = (_display.DisplayRectangle.Height - height) / 2;
                        }
                        break;
                }

                if (mode != DisplayMode.Manual) _videoBounds = new Rectangle(left, top, width, height);
                _videoBoundsClip = Rectangle.Intersect(_display.DisplayRectangle, _videoBounds);
                _hasVideoBounds = true;
            }
            else
            {
                _hasVideoBounds = false;
            }
        }

        internal Image AV_DisplayCopy(bool videoMode, bool withOverlay)
        {
            Bitmap image = null;
            _lastError   = HResult.MF_E_NOT_AVAILABLE;

            if (_hasVideo || _hasOverlayShown)
            {
                try
                {
                    Rectangle   rect;
                    Graphics    gSource;

                    if (!_hasVideo) videoMode = false;

                    if (videoMode)
                    {
                        rect    = Rectangle.Intersect(_display.ClientRectangle, _videoDisplay.Bounds);
                        gSource = _videoDisplay.CreateGraphics();
                    }
                    else
                    {
                        rect    = _display.ClientRectangle;
                        gSource = _display.CreateGraphics();
                    }

                    image            = new Bitmap(rect.Width, rect.Height, gSource);
                    Graphics gDest   = Graphics.FromImage(image);

                    IntPtr hdcSource = gSource.GetHdc();
                    IntPtr hdcDest   = gDest.GetHdc();

                    if (videoMode) SafeNativeMethods.BitBlt(hdcDest, 0, 0, rect.Width, rect.Height, hdcSource, rect.X - _videoDisplay.Bounds.X, rect.Y - _videoDisplay.Bounds.Y, SafeNativeMethods.SRCCOPY);
                    else SafeNativeMethods.BitBlt(hdcDest, 0, 0, rect.Width, rect.Height, hdcSource, 0, 0, SafeNativeMethods.SRCCOPY);

                    if (withOverlay && _hasOverlay)
                    {
                        int transparentColor = ColorTranslator.ToWin32(_overlay.TransparencyKey);

                        Graphics gOverlay = _overlay.CreateGraphics();
                        IntPtr hdcOverlay = gOverlay.GetHdc();

                        if (videoMode)
                        {
                            if (_overlay.Opacity == 1 || _overlayBlend == OverlayBlend.None)
                            {
                                if (_overlayMode == OverlayMode.Display) SafeNativeMethods.TransparentBlt(hdcDest, 0, 0, rect.Width, rect.Height, hdcOverlay, _videoBoundsClip.X, _videoBoundsClip.Y, _videoBoundsClip.Width, _videoBoundsClip.Height, transparentColor);
                                else SafeNativeMethods.TransparentBlt(hdcDest, 0, 0, rect.Width, rect.Height, hdcOverlay, 0, 0, _videoBoundsClip.Width, _videoBoundsClip.Height, transparentColor);
                            }
                            else
                            {
                                _blendFunction.SourceConstantAlpha = (byte)(_overlay.Opacity * 0xFF);
                                if (_overlayMode == OverlayMode.Display) SafeNativeMethods.AlphaBlend(hdcDest, 0, 0, rect.Width, rect.Height, hdcOverlay, _videoBoundsClip.X, _videoBoundsClip.Y, _videoBoundsClip.Width, _videoBoundsClip.Height, _blendFunction);
                                else SafeNativeMethods.AlphaBlend(hdcDest, 0, 0, rect.Width, rect.Height, hdcOverlay, 0, 0, _videoBoundsClip.Width, _videoBoundsClip.Height, _blendFunction);
                            }
                        }
                        else
                        {
                            if (_overlay.Opacity == 1 || _overlayBlend == OverlayBlend.None)
                            {
                                if (_overlayMode == OverlayMode.Display) SafeNativeMethods.TransparentBlt(hdcDest, 0, 0, _overlay.ClientRectangle.Width, _overlay.ClientRectangle.Height, hdcOverlay, 0, 0, _overlay.ClientRectangle.Width, _overlay.ClientRectangle.Height, transparentColor);
                                else SafeNativeMethods.TransparentBlt(hdcDest, _videoBoundsClip.X, _videoBoundsClip.Y, _videoBoundsClip.Width, _videoBoundsClip.Height, hdcOverlay, 0, 0, _overlay.ClientRectangle.Width, _overlay.ClientRectangle.Height, transparentColor);
                            }
                            else
                            {
                                _blendFunction.SourceConstantAlpha = (byte)(_overlay.Opacity * 0xFF);
                                if (_overlayMode == OverlayMode.Display) SafeNativeMethods.AlphaBlend(hdcDest, 0, 0, _overlay.ClientRectangle.Width, _overlay.ClientRectangle.Height, hdcOverlay, 0, 0, _overlay.ClientRectangle.Width, _overlay.ClientRectangle.Height, _blendFunction);
                                else SafeNativeMethods.AlphaBlend(hdcDest, _videoBoundsClip.X, _videoBoundsClip.Y, _videoBoundsClip.Width, _videoBoundsClip.Height, hdcOverlay, 0, 0, _overlay.ClientRectangle.Width, _overlay.ClientRectangle.Height, _blendFunction);
                            }
                        }

                        gOverlay.ReleaseHdc(hdcOverlay);
                        gOverlay.Dispose();
                    }

                    // this should be outside try/catch
                    if (hdcSource  != null) gSource.ReleaseHdc(hdcSource);
                    if (gSource    != null) gSource.Dispose();

                    if (hdcDest != null) gDest.ReleaseHdc(hdcDest);
                    if (gDest   != null) gDest.Dispose();

                    _lastError = NO_ERROR;
                }
                catch { /* ignore */ }
            }
            return image;
        }

        internal Bitmap AV_ResizeImage(Image image, int width, int height)
        {
            Bitmap copy = new Bitmap(width, height);
            copy.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            Graphics g              = Graphics.FromImage(copy);
            g.InterpolationMode     = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode         = SmoothingMode.HighQuality;
            g.CompositingMode       = CompositingMode.SourceCopy;
            g.CompositingQuality    = CompositingQuality.HighQuality;
            g.PixelOffsetMode       = PixelOffsetMode.HighQuality;

            System.Drawing.Imaging.ImageAttributes imageAttrs = new System.Drawing.Imaging.ImageAttributes();
            imageAttrs.SetWrapMode(WrapMode.TileFlipXY);

            g.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttrs);

            imageAttrs.Dispose();
            g.Dispose();

            return copy;
        }

        #endregion

        #region Private - Display Shapes

        internal void AV_UpdateDisplayShape()
        {
            if (_display.Region != null) { _display.Region.Dispose(); _display.Region = null; }

            if (_hasVideoShape) _display.Region = _displayShapeCallback(Rectangle.Intersect(_videoBounds, _display.ClientRectangle));
            else _display.Region = _displayShapeCallback(_display.ClientRectangle);
            _display.Invalidate();

            if (_hasOverlay && _hasOverlayClipping) AV_ClipOverlay();
        }

        internal void AV_RemoveDisplayShape(bool overlayClipping)
        {
            DisplayShape oldShape = _displayShape;

            _displayShapeCallback = null;
            _displayShape = DisplayShape.Normal;

            if (_hasDisplay && _display.Region != null)
            {
                _display.Region.Dispose();
                _display.Region = null;
            }
            _hasDisplayShape = false;

            if (_hasVideoShape)
            {
                _mediaVideoBoundsChanged -= AV_DisplayShapeChanged;
            }

            if (overlayClipping) AV_SetOverlayClipping(false);

            if (oldShape != DisplayShape.Normal)
            {
                if (_paused && _hasVideo) mf_VideoDisplayControl.RepaintVideo();
                if (_mediaDisplayShapeChanged != null) _mediaDisplayShapeChanged(this, EventArgs.Empty);
            }
        }

        internal void AV_DisplayShapeChanged(object sender, EventArgs e)
        {
            if (_hasDisplayShape) AV_UpdateDisplayShape();
        }

        internal ShapeCallback AV_GetShapeCallback(DisplayShape shape)
        {
            ShapeCallback callback = null;

            switch (shape)
            {
                case DisplayShape.ArrowDown:
                    callback = AV_PresetArrowDownShape;
                    break;

                case DisplayShape.ArrowLeft:
                    callback = AV_PresetArrowLeftShape;
                    break;

                case DisplayShape.ArrowRight:
                    callback = AV_PresetArrowRightShape;
                    break;

                case DisplayShape.ArrowUp:
                    callback = AV_PresetArrowUpShape;
                    break;

                case DisplayShape.Bars:
                    callback = AV_PresetBarsShape;
                    break;

                case DisplayShape.Beams:
                    callback = AV_PresetBeamsShape;
                    break;

                case DisplayShape.Circle:
                    callback = AV_PresetCircleShape;
                    break;

                case DisplayShape.Cross:
                    callback = AV_PresetCrossShape;
                    break;

                case DisplayShape.Custom:
                    callback = AV_PresetCustomShape;
                    break;

                case DisplayShape.Diamond:
                    callback = AV_PresetDiamondShape;
                    break;

                case DisplayShape.Frame:
                    callback = AV_PresetFrameShape;
                    break;

                case DisplayShape.Heart:
                    callback = AV_PresetHeartShape;
                    break;

                case DisplayShape.Hexagon:
                    callback = AV_PresetHexagonalShape;
                    break;

                case DisplayShape.Oval:
                    callback = AV_PresetOvalShape;
                    break;

                case DisplayShape.Rectangle:
                    callback = AV_PresetRectangularShape;
                    break;

                case DisplayShape.Ring:
                    callback = AV_PresetRingShape;
                    break;

                case DisplayShape.Rounded:
                    callback = AV_PresetRoundedShape;
                    break;

                case DisplayShape.Square:
                    callback = AV_PresetSquareShape;
                    break;

                case DisplayShape.Star:
                    callback = AV_PresetStarShape;
                    break;

                case DisplayShape.Tiles:
                    callback = AV_PresetTilesShape;
                    break;

                case DisplayShape.TriangleDown:
                    callback = AV_PresetTriangleDownShape;
                    break;

                case DisplayShape.TriangleLeft:
                    callback = AV_PresetTriangleLeftShape;
                    break;

                case DisplayShape.TriangleRight:
                    callback = AV_PresetTriangleRightShape;
                    break;

                case DisplayShape.TriangleUp:
                    callback = AV_PresetTriangleUpShape;
                    break;

                    //default: // case DisplayShape.Normal:
                    //    // callback = null;
                    //    break;
            }
            return callback;
        }

        private Region AV_PresetShapeSetPoints(PointF[] points)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(points);

            Region region = new Region(path);
            path.Dispose();

            return region;
        }


        internal Region AV_PresetArrowDownShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 6f;
            float y = r.Height / 6f;

            PointF[] points = new PointF[8];

            points[0].X = r.X + x;
            points[1].X = points[0].X;
            points[2].X = r.X;
            points[3].X = r.X + (3 * x);
            points[4].X = r.X + (6 * x);
            points[5].X = r.X + (5 * x);
            points[6].X = points[5].X;
            points[7].X = points[0].X;

            points[0].Y = r.Y;
            points[1].Y = r.Y + (3 * y);
            points[2].Y = points[1].Y;
            points[3].Y = r.Y + (6 * y);
            points[4].Y = points[1].Y;
            points[5].Y = points[1].Y;
            points[6].Y = r.Y;
            points[7].Y = r.Y;

            return AV_PresetShapeSetPoints(points);
        }

        internal Region AV_PresetArrowLeftShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 6f;
            float y = r.Height / 6f;

            PointF[] points = new PointF[8];

            points[0].X = r.X + (3 * x);
            points[1].X = r.X;
            points[2].X = points[0].X;
            points[3].X = points[0].X;
            points[4].X = r.X + (6 * x);
            points[5].X = points[4].X;
            points[6].X = points[0].X;
            points[7].X = points[0].X;

            points[0].Y = r.Y;
            points[1].Y = r.Y + (3 * y);
            points[2].Y = r.Y + (6 * y);
            points[3].Y = r.Y + (5 * y);
            points[4].Y = points[3].Y;
            points[5].Y = r.Y + y;
            points[6].Y = points[5].Y;
            points[7].Y = r.Y;

            return AV_PresetShapeSetPoints(points);
        }

        internal Region AV_PresetArrowRightShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 6f;
            float y = r.Height / 6f;

            PointF[] points = new PointF[8];

            points[0].X = r.X + (3 * x);
            points[1].X = points[0].X;
            points[2].X = r.X;
            points[3].X = r.X;
            points[4].X = points[0].X;
            points[5].X = points[0].X;
            points[6].X = r.X + (6 * x);
            points[7].X = points[0].X;

            points[0].Y = r.Y;
            points[1].Y = r.Y + y;
            points[2].Y = points[1].Y;
            points[3].Y = r.Y + (5 * y);
            points[4].Y = points[3].Y;
            points[5].Y = r.Y + (6 * y);
            points[6].Y = r.Y + (3 * y);
            points[7].Y = r.Y;

            return AV_PresetShapeSetPoints(points);
        }

        internal Region AV_PresetArrowUpShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 6f;
            float y = r.Height / 6f;

            PointF[] points = new PointF[8];

            points[0].X = r.X + (3 * x);
            points[1].X = r.X;
            points[2].X = r.X + x;
            points[3].X = points[2].X;
            points[4].X = r.X + (5 * x);
            points[5].X = points[4].X;
            points[6].X = r.X + (6 * x);
            points[7].X = points[0].X;

            points[0].Y = r.Y;
            points[1].Y = r.Y + (3 * y);
            points[2].Y = points[1].Y;
            points[3].Y = r.Y + (6 * y);
            points[4].Y = points[3].Y;
            points[5].Y = points[1].Y;
            points[6].Y = points[1].Y;
            points[7].Y = r.Y;

            return AV_PresetShapeSetPoints(points);
        }

        internal Region AV_PresetBarsShape(Rectangle shapeBounds)
        {
            const float BARS = 5;

            float width = shapeBounds.Width / (2 * BARS - 1);
            RectangleF r = new RectangleF(shapeBounds.X, shapeBounds.Y, width, shapeBounds.Height);
            width = 2 * width;

            GraphicsPath path = new GraphicsPath();
            for (int i = 0; i < BARS; i++)
            {
                path.AddRectangle(r);
                r.X += width;
            }

            Region region = new Region(path);
            path.Dispose();

            return region;
        }

        internal Region AV_PresetBeamsShape(Rectangle shapeBounds)
        {
            const float BEAMS = 5;

            float height = shapeBounds.Height / (2 * BEAMS - 1);
            RectangleF r = new RectangleF(shapeBounds.X, shapeBounds.Y, shapeBounds.Width, height);
            height = 2 * height;

            GraphicsPath path = new GraphicsPath();
            for (int i = 0; i < BEAMS; i++)
            {
                path.AddRectangle(r);
                r.Y += height;
            }

            Region region = new Region(path);
            path.Dispose();

            return region;
        }

        internal Region AV_PresetCircleShape(Rectangle shapeBounds)
        {
            GraphicsPath path = new GraphicsPath();
            if (shapeBounds.Width < shapeBounds.Height)
            {
                shapeBounds.Y += (shapeBounds.Height - shapeBounds.Width) / 2;
                shapeBounds.Height = shapeBounds.Width;
            }
            else
            {
                shapeBounds.X += (shapeBounds.Width - shapeBounds.Height) / 2;
                shapeBounds.Width = shapeBounds.Height;
            }
            path.AddEllipse(shapeBounds);

            Region region = new Region(path);
            path.Dispose();

            return region;
        }

        internal Region AV_PresetCrossShape(Rectangle shapeBounds)
        {
            float dX = shapeBounds.Width / 3f;
            float dY = shapeBounds.Height / 3f;

            GraphicsPath path = new GraphicsPath();

            path.FillMode = FillMode.Winding;
            path.AddRectangle(new RectangleF(shapeBounds.X + dX, shapeBounds.Y, dX, shapeBounds.Height));
            path.AddRectangle(new RectangleF(shapeBounds.X, shapeBounds.Y + dY, shapeBounds.Width, dY));

            Region region = new Region(path);
            path.Dispose();

            return region;
        }

        internal Region AV_PresetCustomShape(Rectangle shapeBounds)
        {
            if (_displayShapeBlock || _customShapePath == null) return null;

            Region region;

            try
            {
                GraphicsPath path = (GraphicsPath)_customShapePath.Clone();
                path.Transform(new Matrix(path.GetBounds(), new PointF[] { new Point (shapeBounds.Left, shapeBounds.Top),
                    new Point (shapeBounds.Right, shapeBounds.Top), new Point (shapeBounds.Left, shapeBounds.Bottom) }));
                region = new Region(path);
                path.Dispose();
            }
            catch
            {
                _customShapePath.Dispose();
                _customShapePath = null;
                region = null;
            }

            return region;
        }

        internal Region AV_PresetDiamondShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 6f;
            float y = r.Height / 6f;

            PointF[] points = new PointF[5];

            points[0].X = r.X + (3 * x);
            points[1].X = r.X;
            points[2].X = points[0].X;
            points[3].X = r.X + (6 * x);
            points[4].X = points[0].X;

            points[0].Y = r.Y;
            points[1].Y = r.Y + (3 * y);
            points[2].Y = r.Y + (6 * y);
            points[3].Y = points[1].Y;
            points[4].Y = r.Y;

            return AV_PresetShapeSetPoints(points);
        }

        internal Region AV_PresetFrameShape(Rectangle shapeBounds)
        {
            const float THICKNESS = 0.25f; // between 0 and 0.5 exclusive

            IntPtr handle1 = SafeNativeMethods.CreateRoundRectRgn(
                shapeBounds.Left,
                shapeBounds.Top,
                shapeBounds.Left + shapeBounds.Width + 1,
                shapeBounds.Top + shapeBounds.Height + 1,
                48,
                48);
            Region region1 = Region.FromHrgn(handle1);

            int width;
            if (shapeBounds.Width <= shapeBounds.Height) width = (int)(shapeBounds.Width * THICKNESS);
            else width = (int)(shapeBounds.Height * THICKNESS);

            IntPtr handle2 = SafeNativeMethods.CreateRoundRectRgn(
                shapeBounds.Left + width,
                shapeBounds.Top + width,
                shapeBounds.Left + shapeBounds.Width - width + 1,
                shapeBounds.Top + shapeBounds.Height - width + 1,
                48,
                48);
            Region region2 = Region.FromHrgn(handle2);

            region1.Exclude(region2);
            region2.Dispose();

            SafeNativeMethods.DeleteObject(handle1);
            SafeNativeMethods.DeleteObject(handle2);

            return region1;
        }

        internal Region AV_PresetHeartShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 6f;
            float y = r.Height / 6f;

            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Winding;

            path.AddEllipse(new RectangleF(r.X, r.Y, 3.1f * x, 3 * y));
            path.AddEllipse(new RectangleF(r.X + (2.9f * x), r.Y, 3.1f * x, 3 * y));

            PointF[] points = new PointF[4];

            points[0].X = r.X + (0.085f * x);
            points[0].Y = r.Y + (2f * y);

            points[1].X = r.X + (5.78f * x);
            points[1].Y = points[0].Y;

            points[2].X = r.X + (3 * x);
            points[2].Y = r.Y + (6 * y);

            points[3].X = points[0].X;
            points[3].Y = points[0].Y;

            path.AddCurve(points, 0.15f);

            Region region = new Region(path);
            path.Dispose();

            return region;
        }

        internal Region AV_PresetHexagonalShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 4f;
            float y = r.Height / 4f;

            PointF[] points = new PointF[7];

            points[0].X = r.X + x;
            points[1].X = r.X;
            points[2].X = points[0].X;
            points[3].X = r.X + (3 * x);
            points[4].X = r.X + (4 * x);
            points[5].X = points[3].X;
            points[6].X = points[0].X;

            points[0].Y = r.Y;
            points[1].Y = r.Y + (2 * y);
            points[2].Y = r.Y + (4 * y);
            points[3].Y = points[2].Y;
            points[4].Y = points[1].Y;
            points[5].Y = r.Y;
            points[6].Y = r.Y;

            return AV_PresetShapeSetPoints(points);
        }

        internal Region AV_PresetOvalShape(Rectangle shapeBounds)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(shapeBounds);

            Region region = new Region(path);
            path.Dispose();

            return region;
        }

        internal Region AV_PresetRectangularShape(Rectangle shapeBounds)
        {
            Region region = new Region(shapeBounds);
            return region;
        }

        internal Region AV_PresetRingShape(Rectangle shapeBounds)
        {
            const float THICKNESS = 0.25f; // between 0 and 0.5 exclusive

            float width;
            if (shapeBounds.Width <= shapeBounds.Height) width = shapeBounds.Width * THICKNESS;
            else width = shapeBounds.Height * THICKNESS;
            RectangleF r = new RectangleF(shapeBounds.X + width, shapeBounds.Y + width, shapeBounds.Width - (2 * width), shapeBounds.Height - (2 * width));

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(shapeBounds);
            path.AddEllipse(r);
            Region region = new Region(path);

            path.Dispose();
            return region;
        }

        internal Region AV_PresetRoundedShape(Rectangle shapeBounds)
        {
            IntPtr handle = SafeNativeMethods.CreateRoundRectRgn(
               shapeBounds.Left,
               shapeBounds.Top,
               shapeBounds.Left + shapeBounds.Width + 1,
               shapeBounds.Top + shapeBounds.Height + 1,
               48,
               48);

            Region region = Region.FromHrgn(handle);
            SafeNativeMethods.DeleteObject(handle);

            return region;
        }

        internal Region AV_PresetSquareShape(Rectangle shapeBounds)
        {
            GraphicsPath path = new GraphicsPath();
            if (shapeBounds.Width < shapeBounds.Height)
            {
                shapeBounds.Y += (shapeBounds.Height - shapeBounds.Width) / 2;
                shapeBounds.Height = shapeBounds.Width;
            }
            else
            {
                shapeBounds.X += (shapeBounds.Width - shapeBounds.Height) / 2;
                shapeBounds.Width = shapeBounds.Height;
            }
            path.AddRectangle(shapeBounds);

            Region region = new Region(path);
            path.Dispose();

            return region;
        }

        internal Region AV_PresetStarShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 6f;
            float y = r.Height / 6f;

            PointF[] points = new PointF[17];

            points[0].X = r.X;
            points[1].X = r.X + (2.5f * x);
            points[2].X = r.X + (3 * x);
            points[3].X = r.X + (3.5f * x);
            points[4].X = r.X + (6 * x);
            points[5].X = r.X + (4.5f * x);
            points[6].X = points[4].X;
            points[7].X = points[5].X;
            points[8].X = points[4].X;
            points[9].X = points[3].X;
            points[10].X = points[2].X;
            points[11].X = points[1].X;
            points[12].X = r.X;
            points[13].X = r.X + (1.5f * x);
            points[14].X = r.X;
            points[15].X = points[13].X;
            points[16].X = r.X;

            points[0].Y = r.Y;
            points[1].Y = r.Y + (1.5f * y);
            points[2].Y = r.Y;
            points[3].Y = points[1].Y;
            points[4].Y = r.Y;
            points[5].Y = r.Y + (2.5f * y);
            points[6].Y = r.Y + (3 * y);
            points[7].Y = r.Y + (3.5f * y);
            points[8].Y = r.Y + (6 * y);
            points[9].Y = r.Y + (4.5f * y);
            points[10].Y = points[8].Y;
            points[11].Y = points[9].Y;
            points[12].Y = points[8].Y;
            points[13].Y = points[7].Y;
            points[14].Y = points[6].Y;
            points[15].Y = points[5].Y;
            points[16].Y = r.Y;

            return AV_PresetShapeSetPoints(points);
        }

        internal Region AV_PresetTilesShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 3f;
            float y = r.Height / 3f;

            PointF[] points = new PointF[21];

            points[0].X = r.X;
            points[1].X = r.X;
            points[2].X = r.X + x;
            points[3].X = r.X + x;
            points[4].X = r.X;
            points[5].X = r.X;
            points[6].X = r.X + x;
            points[7].X = r.X + x;
            points[8].X = r.X + (2 * x);
            points[9].X = r.X + (2 * x);
            points[10].X = r.X + (3 * x);
            points[11].X = r.X + (3 * x);
            points[12].X = r.X + (2 * x);
            points[13].X = r.X + (2 * x);
            points[14].X = r.X + (3 * x);
            points[15].X = r.X + (3 * x);
            points[16].X = r.X + (2 * x);
            points[17].X = r.X + (2 * x);
            points[18].X = r.X + x;
            points[19].X = r.X + x;
            points[20].X = r.X;

            points[0].Y = r.Y;
            points[1].Y = r.Y + y;
            points[2].Y = r.Y + y;
            points[3].Y = r.Y + (2 * y);
            points[4].Y = r.Y + (2 * y);
            points[5].Y = r.Y + (3 * y);
            points[6].Y = r.Y + (3 * y);
            points[7].Y = r.Y + (2 * y);
            points[8].Y = r.Y + (2 * y);
            points[9].Y = r.Y + (3 * y);
            points[10].Y = r.Y + (3 * y);
            points[11].Y = r.Y + (2 * y);
            points[12].Y = r.Y + (2 * y);
            points[13].Y = r.Y + y;
            points[14].Y = r.Y + y;
            points[15].Y = r.Y;
            points[16].Y = r.Y;
            points[17].Y = r.Y + y;
            points[18].Y = r.Y + y;
            points[19].Y = r.Y;
            points[20].Y = r.Y;

            return AV_PresetShapeSetPoints(points);
        }

        internal Region AV_PresetTriangleDownShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 4f;
            float y = r.Height / 4f;

            PointF[] points = new PointF[4];

            points[0].X = r.X;
            points[1].X = r.X + (2 * x);
            points[2].X = r.X + (4 * x);
            points[3].X = r.X;

            points[0].Y = r.Y;
            points[1].Y = r.Y + (4 * y);
            points[2].Y = r.Y;
            points[3].Y = r.Y;

            return AV_PresetShapeSetPoints(points);
        }

        internal Region AV_PresetTriangleLeftShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 4f;
            float y = r.Height / 4f;

            PointF[] points = new PointF[4];

            points[0].X = r.X + (4 * x);
            points[1].X = r.X;
            points[2].X = points[0].X;
            points[3].X = points[0].X;

            points[0].Y = r.Y;
            points[1].Y = r.Y + (2 * y);
            points[2].Y = r.Y + (4 * y);
            points[3].Y = r.Y;

            return AV_PresetShapeSetPoints(points);
        }

        internal Region AV_PresetTriangleRightShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 4f;
            float y = r.Height / 4f;

            PointF[] points = new PointF[4];

            points[0].X = r.X;
            points[1].X = r.X;
            points[2].X = r.X + (4 * x);
            points[3].X = r.X;

            points[0].Y = r.Y;
            points[1].Y = r.Y + (4 * y);
            points[2].Y = r.Y + (2 * y);
            points[3].Y = r.Y;

            return AV_PresetShapeSetPoints(points);
        }

        internal Region AV_PresetTriangleUpShape(Rectangle shapeBounds)
        {
            Rectangle r = shapeBounds;

            // x and y units
            float x = r.Width / 4f;
            float y = r.Height / 4f;

            PointF[] points = new PointF[4];

            points[0].X = r.X + (2 * x);
            points[1].X = r.X;
            points[2].X = r.X + (4 * x);
            points[3].X = points[0].X;

            points[0].Y = r.Y;
            points[1].Y = r.Y + (4 * y);
            points[2].Y = points[1].Y;
            points[3].Y = r.Y;

            return AV_PresetShapeSetPoints(points);
        }

        #endregion

        #region Private - Display Overlay

        internal void AV_SetOverlay(Form theOverlay)
        {
            if (theOverlay == null)
            {
                if (_hasOverlay)
                {
                    AV_RemoveOverlay(true);
                    if (_mediaOverlayChanged != null) _mediaOverlayChanged(this, EventArgs.Empty);
                }
                _lastError = NO_ERROR;
                return;
            }

            if (_display == null)
            {
                _lastError = HResult.MF_E_NOT_AVAILABLE;
                return;
            }

            if (theOverlay.GetType() == _display.FindForm().GetType())
            {
                _lastError = HResult.E_INVALIDARG;
                return;
            }
            if (theOverlay == _overlay)
            {
                if (_playing || _overlayHold)
                {
                    _overlay.Owner = _display.FindForm();
                    _overlay.SendToBack();
                    if (_overlay.ContextMenuStrip == null && _display.ContextMenuStrip != null)
                    {
                        _overlay.ContextMenuStrip = _display.ContextMenuStrip;
                        _hasOverlayMenu           = true;
                    }

                    if (!_hasOverlayShown) AV_ShowOverlay();
                    else if (!_hasVideo && _overlayMode == OverlayMode.Video && (_overlay.Size != _display.DisplayRectangle.Size))
                    {
                        _overlay.Location = _display.PointToScreen(_display.DisplayRectangle.Location);
                        _overlay.Size     = _display.DisplayRectangle.Size;
                    }
                }
                _lastError = NO_ERROR;
                return;
            }

            if (_hasOverlay) AV_RemoveOverlay(true);

            Form myOverlay              = theOverlay;
            myOverlay.Owner             = _display.FindForm();

            myOverlay.ControlBox        = false;
            myOverlay.FormBorderStyle   = FormBorderStyle.None;
            myOverlay.MaximizeBox       = false;
            myOverlay.MinimizeBox       = false;
            myOverlay.ShowIcon          = false;
            myOverlay.HelpButton        = false;
            myOverlay.ShowInTaskbar     = false;
            myOverlay.SizeGripStyle     = SizeGripStyle.Hide;
            myOverlay.StartPosition     = FormStartPosition.Manual;

            myOverlay.MinimumSize       = new Size(0, 0);
            myOverlay.MaximumSize       = myOverlay.MinimumSize;
            myOverlay.AutoSize          = false;
            myOverlay.IsMdiContainer    = false;
            myOverlay.TopMost           = false;
            myOverlay.UseWaitCursor     = false;
            myOverlay.WindowState       = FormWindowState.Normal;

            if (myOverlay.TransparencyKey.IsEmpty) myOverlay.TransparencyKey = myOverlay.BackColor;
            if (myOverlay.ContextMenuStrip == null && _display.ContextMenuStrip != null)
            {
                myOverlay.ContextMenuStrip = _display.ContextMenuStrip;
                _hasOverlayMenu = true;
            }
            _overlay    = myOverlay;
            _hasOverlay = true;

            if (_playing || _overlayHold) AV_ShowOverlay();

            if (_mediaOverlayChanged != null) _mediaOverlayChanged(this, EventArgs.Empty);

            _lastError = NO_ERROR;
        }

        internal void AV_ShowOverlay()
        {
            if (_hasOverlay)
            {
                bool oldFocus = (Form.ActiveForm == _overlay.Owner || Form.ActiveForm == _overlay); //.ContainsFocus || av_Overlay.Owner.Focused;
                double myOpacity = _overlay.Opacity;

                _overlay.Opacity = 0;
                _overlay.Location = _display.PointToScreen(_display.DisplayRectangle.Location);

                _hasOverlayShown = true;
                AV_SetOverlayEvents();

                _display.Invalidate();
                Application.DoEvents();

                if (_hasOverlayClipping) AV_ClipOverlay();

                if (_display.Visible && _overlay.Owner.WindowState != FormWindowState.Minimized)
                {
                    //SafeNativeMethods.ShowWindow(_overlay.Handle, 4);
                    SafeNativeMethods.ShowWindow(_overlay.Handle, 8);
                    //_overlay.SendToBack(); //triggers mousedown event ???

                    foreach (Form f in Application.OpenForms)
                    {
                        if (f != _overlay && f.Owner == _overlay.Owner) f.BringToFront();
                    }

                    if (oldFocus) _overlay.Owner.Activate();
                    Application.DoEvents();
                }
                else if (_minimizeEnabled)
                {
                    _minimizedOpacity = myOpacity;
                    myOpacity = 0;
                }

                _overlay.Opacity = myOpacity;
                Application.DoEvents();

                if (dc_HasDisplayClones) DisplayClones_Start();
                if (_mediaOverlayActiveChanged != null) _mediaOverlayActiveChanged(this, EventArgs.Empty);
            }
        }

        internal void AV_HideOverlay()
        {
            if (_hasOverlay)
            {
                AV_RemoveOverlayEvents();
                _overlay.Hide();
                _hasOverlayShown = false;

                if (dc_DisplayClonesRunning && !_playing && _overlayHold) DisplayClones_Stop(false);
                if (_mediaOverlayActiveChanged != null) _mediaOverlayActiveChanged(this, EventArgs.Empty);
            }
        }

        internal void AV_RemoveOverlay(bool purge)
        {
            if (_hasOverlay)
            {
                _display.FindForm().Focus();
                if (purge)
                {
                    if (_hasOverlayEvents) AV_RemoveOverlayEvents();
                    if (_hasOverlayMenu)
                    {
                        _overlay.ContextMenuStrip   = null;
                        _hasOverlayMenu             = false;
                    }
                    _overlay.Hide();
                    _overlay.Owner      = null;
                    _hasOverlayShown    = false;
                    _overlay            = null;
                    _hasOverlay         = false;

                    if (dc_DisplayClonesRunning)
                    {
                        if (!_playing && _overlayHold)
                        {
                            DisplayClones_Stop(false);
                        }
                        else if (!_hasVideo) // invalidate clone display
                        {
                            for (int i = 0; i < dc_DisplayClones.Length; i++)
                            {
                                if (dc_DisplayClones[i] != null && dc_DisplayClones[i].Control != null)
                                {
                                    dc_DisplayClones[i].Control.Invalidate();
                                }
                            }
                        }
                    }
                }
                else
                {
                    AV_HideOverlay();
                }
            }
            _lastError = NO_ERROR;
        }

        internal void AV_SetOverlayCanFocus(bool value)
        {
            if (_hasOverlay)
            {
                if (value)
                {
                    AV_RemoveOverlayFocusEvents();
                }
                else
                {
                    AV_SetOverlayFocusEvents();

                    //_overlay.SendToBack();
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f != _overlay && f.Owner == _overlay.Owner) f.BringToFront();
                    }

                    //if (_overlay == Form.ActiveForm) _display.FindForm().Activate();
                    _display.FindForm().Activate();
                }
            }
            _overlayCanFocus = value;
            _lastError = NO_ERROR;
        }

        internal void AV_ClipOverlay()
        {
            if (!_hasOverlay || _overlay == null) return;

            if (_hasDisplayShape)
            {
                if (dc_HasDisplayClones)
                {
                    DisplayClones_Pause();
                    Application.DoEvents();
                    foreach (Control c in _overlay.Controls) c.Visible = false;
                }

                Region temp = _display.Region.Clone();
                if (_hasVideo && _overlayMode == OverlayMode.Video) temp.Translate(_videoBounds.X < 0 ? 0 : -_videoBounds.X, _videoBounds.Y < 0 ? 0 : -_videoBounds.Y);
                if (_overlay.Region != null)
                {
                    _overlay.Region.Dispose();
                    _overlay.Region = null;
                }
                _overlay.Region = temp;
                _overlay.Invalidate();

                if (dc_HasDisplayClones)
                {
                    foreach (Control c in _overlay.Controls) c.Visible = true;
                    Application.DoEvents();
                    DisplayClones_Refresh();
                    Application.DoEvents();
                    DisplayClones_Resume();
                }
            }
            else
            {
                bool setClip    = false;
                Rectangle clip  = new Rectangle(0, 0, 10000, 10000);
                Rectangle r     = _display.FindForm().RectangleToScreen(_display.FindForm().ClientRectangle);

                if (r.X > _overlay.Left)
                {
                    clip.X  = r.X - _overlay.Left;
                    setClip = true;
                }

                if (r.Y > _overlay.Top)
                {
                    clip.Y = r.Y - _overlay.Top;
                    setClip = true;
                }

                if (_overlay.Right > r.Right)
                {
                    clip.Width = _overlay.Width - (_overlay.Right - r.Right) - clip.X;
                    setClip = true;
                }

                if (_overlay.Bottom > r.Bottom)
                {
                    clip.Height = _overlay.Height - (_overlay.Bottom - r.Bottom) - clip.Y;
                    setClip = true;
                }

                if (setClip)
                {
                    if (_overlay.Region != null)
                    {
                        _overlay.Region.Dispose();
                        _overlay.Region = null;
                    }
                    _overlay.Region = new Region(clip);
                    _overlay.Invalidate();
                }
                else if (_overlay.Region != null)
                {
                    _overlay.Region.Dispose();
                    _overlay.Region = null;
                }
            }
        }

        internal void AV_SetOverlayClipping(bool enable)
        {
            if (enable)
            {
                if (!_hasOverlayClipping)
                {
                    _hasOverlayClipping = true;
                    if (!(_display is Form))
                    {
                        _display.FindForm().Resize += AV_DoResize;
                        _hasOverlayClippingEvents = true;
                        if (_hasOverlay && _overlay != null)
                        {
                            _display.FindForm().Invalidate();
                            //AV_ClipOverlay();
                        }
                    }
                }
                AV_ClipOverlay();
            }
            else
            {
                if (_hasOverlayClipping)
                {
                    _hasOverlayClipping = false;
                    if (_hasOverlayClippingEvents)
                    {
                        _display.FindForm().Resize -= AV_DoResize;
                        _hasOverlayClippingEvents = false;
                        if (_hasOverlay && _overlay != null)
                        {
                            if (_overlay.Region != null)
                            {
                                _overlay.Region.Dispose();
                                _overlay.Region = null;
                                _overlay.Invalidate();
                                _display.FindForm().Invalidate();
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Private - FullScreen

        internal HResult AV_SetFullScreen(bool value)
        {
            _lastError = NO_ERROR;

            if (_display == null)
            {
                _lastError = HResult.ERROR_INVALID_WINDOW_HANDLE;
            }
            else
            {
                if (value)
                {
                    if (!_fullScreen)
                    {
                        _lastError = !AV_AddFullScreen(_display.FindForm()) ? HResult.MF_E_NOT_AVAILABLE : AV_SetFullScreenMode(_fullScreenMode, true);
                    }
                }
                else
                {
                    if (_fullScreen)
                    {
                        AV_ResetFullScreen();
                    }
                }
            }
            return _lastError;
        }

        private static bool AV_AddFullScreen(Form theForm)
        {
            int i = 0;

            for (; i < MAX_FULLSCREEN_PLAYERS; i++)
            {
                if (_fullScreenForms[i] == theForm) return false;
            }

            for (i = 0; i < MAX_FULLSCREEN_PLAYERS; i++)
            {
                if (_fullScreenForms[i] == null)
                {
                    _fullScreenForms[i] = theForm;
                    return true;
                }
            }

            return false;
        }

        private static void AV_RemoveFullScreen(Form theForm)
        {
            for (int i = 0; i < MAX_FULLSCREEN_PLAYERS; i++)
            {
                if (_fullScreenForms[i] == theForm)
                {
                    _fullScreenForms[i] = null;
                    break;
                }
            }
        }

        private void AV_ResetFullScreen()
        {
            FullScreenMode saveMode = _fullScreenMode;

            if (_fullScreen)
            {
                //if (FullScreenMode != FullScreenMode.Form) AV_SetFullScreenMode(FullScreenMode.Form, false);

                _displayShapeBlock = true;

                if (FullScreenMode != FullScreenMode.Form && FullScreenMode != FullScreenMode.Form_AllScreens)
                {
                    //if (FullScreenMode < FullScreenMode.Display_AllScreens) AV_SetFullScreenMode(FullScreenMode.Form, false);
                    //else AV_SetFullScreenMode(FullScreenMode.Form_AllScreens, false);

                    if (FullScreenMode < FullScreenMode.Display_AllScreens)
                    {
                        if (_fullScreenMode == FullScreenMode.Parent)
                        {
                            FS_ResetDisplayParent();
                        }
                        else
                        {
                            FS_ResetDisplay();
                            if (_display.Parent.GetType().BaseType != typeof(Form)) FS_ResetDisplayParent();
                        }
                    }
                    else
                    {
                        if (_fullScreenMode == FullScreenMode.Parent_AllScreens)
                        {
                            FS_ResetDisplayParent();
                        }
                        else
                        {
                            FS_ResetDisplay();
                            if (_display.Parent.GetType().BaseType != typeof(Form)) FS_ResetDisplayParent();

                        }
                    }
                }

                Form theForm            = (Form)_display.TopLevelControl;
                theForm.FormBorderStyle = _fsFormBorder;
                theForm.Bounds          = _fsFormBounds;
                _fullScreenMode         = saveMode;
                _fullScreen             = false;

                AV_RemoveFullScreen(theForm);

                _displayShapeBlock = false;
                if (_displayShape == DisplayShape.Custom) AV_UpdateDisplayShape();

                if (_mediaFullScreenChanged != null) _mediaFullScreenChanged(this, EventArgs.Empty);
            }
        }

        private HResult AV_SetFullScreenMode(FullScreenMode mode, bool doEvent)
        {
            Form theForm = null;
            bool fChanged = false;

            // switch between fullscreen one screen and all screens
            if (_fullScreen && ((mode < FullScreenMode.Display_AllScreens && _fullScreenMode >= FullScreenMode.Display_AllScreens)
                || (mode >= FullScreenMode.Display_AllScreens && _fullScreenMode < FullScreenMode.Display_AllScreens)))
            {
                AV_ResetFullScreen();
                Application.DoEvents();
            }

            _displayShapeBlock = true;

            if (!_fullScreen)
            {
                if (_display.GetType().BaseType == typeof(Form)) theForm = (Form)_display;
                else theForm = (Form)_display.TopLevelControl;

                _fsFormBounds = theForm.WindowState == FormWindowState.Maximized ? theForm.RestoreBounds : theForm.Bounds;
                _fsFormBorder = theForm.FormBorderStyle;

                Rectangle r = mode < FullScreenMode.Display_AllScreens ? Screen.GetBounds(_display) : SystemInformation.VirtualScreen;
                theForm.FormBorderStyle = FormBorderStyle.None;
                SafeNativeMethods.SetWindowPos(theForm.Handle, IntPtr.Zero, r.Left, r.Top, r.Width, r.Height, SafeNativeMethods.SWP_SHOWWINDOW);

                _fullScreenMode = mode < FullScreenMode.Display_AllScreens ? FullScreenMode.Form : FullScreenMode.Form_AllScreens;
                _fullScreen = true;

                if (theForm == _display || mode == FullScreenMode.Form || mode == FullScreenMode.Form_AllScreens)
                {
                    _displayShapeBlock = false;
                    if (_displayShape == DisplayShape.Custom) AV_UpdateDisplayShape();

                    if (_mediaFullScreenChanged != null) _mediaFullScreenChanged(this, EventArgs.Empty);
                    return NO_ERROR;
                }
                fChanged = true;
            }

            if (mode != _fullScreenMode)
            {
                switch (mode)
                {
                    case FullScreenMode.Display:
                        if (_fullScreenMode == FullScreenMode.Form)
                        {
                            if (_display.Parent.GetType().BaseType != typeof(Form))
                            {
                                if (_display.Parent.Parent.GetType().BaseType == typeof(Form)) FS_SetDisplayParent();
                                else
                                {
                                    _displayShapeBlock = false;
                                    return HResult.MF_E_NOT_AVAILABLE;
                                }
                            }
                        }
                        FS_SetDisplay();
                        _fullScreenMode = FullScreenMode.Display;
                        break;

                    case FullScreenMode.Display_AllScreens:
                        if (_fullScreenMode == FullScreenMode.Form_AllScreens)
                        {
                            if (_display.Parent.GetType().BaseType != typeof(Form))
                            {
                                if (_display.Parent.Parent.GetType().BaseType == typeof(Form)) FS_SetDisplayParent();
                                else
                                {
                                    _displayShapeBlock = false;
                                    return HResult.MF_E_NOT_AVAILABLE;
                                }
                            }
                        }
                        FS_SetDisplay();
                        _fullScreenMode = FullScreenMode.Display_AllScreens;
                        break;

                    case FullScreenMode.Parent:
                        if (_fullScreenMode == FullScreenMode.Display)
                        {
                            FS_ResetDisplay();
                            _fullScreenMode = _display.Parent.GetType().BaseType == typeof(Form) ? FullScreenMode.Form : FullScreenMode.Parent;
                        }
                        else
                        {
                            if (_display.Parent.GetType().BaseType != typeof(Form))
                            {
                                if (_fullScreenMode < FullScreenMode.Display_AllScreens)
                                {
                                    if (_display.Parent.Parent.GetType().BaseType == typeof(Form)) FS_SetDisplayParent();
                                    else
                                    {
                                        _displayShapeBlock = false;
                                        return HResult.MF_E_NOT_AVAILABLE;
                                    }
                                }
                                _fullScreenMode = FullScreenMode.Parent;
                            }
                            else _fullScreenMode = FullScreenMode.Form;
                        }
                        break;

                    case FullScreenMode.Parent_AllScreens:
                        if (_fullScreenMode == FullScreenMode.Display_AllScreens)
                        {
                            FS_ResetDisplay();
                            _fullScreenMode = _display.Parent.GetType().BaseType == typeof(Form) ? FullScreenMode.Form_AllScreens : FullScreenMode.Parent_AllScreens;
                        }
                        else
                        {
                            if (_display.Parent.GetType().BaseType != typeof(Form))
                            {
                                if (_fullScreenMode >= FullScreenMode.Display_AllScreens)
                                {
                                    if (_display.Parent.Parent.GetType().BaseType == typeof(Form)) FS_SetDisplayParent();
                                    else
                                    {
                                        _displayShapeBlock = false;
                                        return HResult.MF_E_NOT_AVAILABLE;
                                    }
                                }
                                _fullScreenMode = FullScreenMode.Parent_AllScreens;
                            }
                            else _fullScreenMode = FullScreenMode.Form_AllScreens;
                        }
                        break;

                    case FullScreenMode.Form:
                        if (_fullScreenMode == FullScreenMode.Parent)
                        {
                            FS_ResetDisplayParent();
                        }
                        else
                        {
                            FS_ResetDisplay();
                            if (_display.Parent.GetType().BaseType != typeof(Form)) FS_ResetDisplayParent();
                        }
                        _fullScreenMode = FullScreenMode.Form;
                        break;

                    case FullScreenMode.Form_AllScreens:
                        if (_fullScreenMode == FullScreenMode.Parent_AllScreens)
                        {
                            FS_ResetDisplayParent();
                        }
                        else
                        {
                            FS_ResetDisplay();
                            if (_display.Parent.GetType().BaseType != typeof(Form)) FS_ResetDisplayParent();

                        }
                        _fullScreenMode = FullScreenMode.Form_AllScreens;
                        break;
                }

                if (doEvent && _mediaFullScreenModeChanged != null) _mediaFullScreenModeChanged(this, EventArgs.Empty);
            }

            _displayShapeBlock = false;
            if (_displayShape == DisplayShape.Custom) AV_UpdateDisplayShape();

            if (fChanged && _mediaFullScreenChanged != null) _mediaFullScreenChanged(this, EventArgs.Empty);
            if (theForm != null) Application.DoEvents();

            return NO_ERROR;
        }

        private void FS_SetDisplay()
        {
            _fsDisplayIndex     = _display.Parent.Controls.GetChildIndex(_display);
            _fsDisplayBounds    = _display.Bounds;
            try
            {
                _fsDisplayBorder                = ((Panel)_display).BorderStyle;
                ((Panel)_display).BorderStyle   = BorderStyle.None;
            }
            catch { /* ignored */ }

            if (_fullScreenMode != FullScreenMode.Display && _fullScreenMode != FullScreenMode.Display_AllScreens)
            {
                Rectangle r = _display.Parent.Bounds;
                r.X = 0;  r.Y = 0;
                _display.Bounds = r;
            }
            _display.BringToFront();
        }

        private void FS_ResetDisplay()
        {
            try
            {
                ((Panel)_display).BorderStyle = _fsDisplayBorder;
            }
            catch { /* ignored */ }
            _display.Bounds = _fsDisplayBounds;
            _display.Parent.Controls.SetChildIndex(_display, _fsDisplayIndex);
        }

        private void FS_SetDisplayParent()
        {
            _fsParentIndex  = _display.Parent.Parent.Controls.GetChildIndex(_display.Parent);
            _fsParentBounds = _display.Parent.Bounds;
            try
            {
                _fsParentBorder                      = ((Panel)_display.Parent).BorderStyle;
                ((Panel)_display.Parent).BorderStyle = BorderStyle.None;
            }
            catch { /* ignored */ }

            Rectangle r             = _display.Parent.Parent.Bounds;
            r.X = r.Y               = 0;
            _display.Parent.Bounds  = r;
            _display.Parent.BringToFront();
        }

        private void FS_ResetDisplayParent()
        {
            try
            {
                ((Panel)_display.Parent).BorderStyle = _fsParentBorder;
            }
            catch { /* ignored */ }
            _display.Parent.Bounds = _fsParentBounds;
            _display.Parent.Parent.Controls.SetChildIndex(_display.Parent, _fsParentIndex);
        }

        internal bool FS_GetVideoWallMode()
        {
            if (_hasDisplay)
            {
                _lastError = Player.NO_ERROR;
                return _fullScreen && _fullScreenMode == FullScreenMode.Display_AllScreens;
            }
            _lastError = HResult.ERROR_INVALID_WINDOW_HANDLE;
            return false;
        }

        internal void FS_SetVideoWallMode(bool activate)
        {
            if (_hasDisplay)
            {
                if (activate)
                {
                    if (!(_fullScreen && _fullScreenMode == FullScreenMode.Display_AllScreens))
                    {
                        FullScreenMode = FullScreenMode.Display_AllScreens;
                        AV_SetFullScreen(true);
                    }
                }
                else if (_fullScreen && _fullScreenMode >= FullScreenMode.Display_AllScreens)
                {
                    AV_SetFullScreen(false);
                }
                _lastError = Player.NO_ERROR;
            }
            else _lastError = HResult.ERROR_INVALID_WINDOW_HANDLE;
        }

        #endregion

        #region Private - Speed

        internal void AV_SetSpeed(float speed, bool setSlider)
        {
            _lastError = NO_ERROR;

            if (speed <= mf_SpeedMinimum) speed = mf_SpeedMinimum;
            else if (speed > mf_SpeedMaximum) speed = mf_SpeedMaximum;

            if (speed != mf_Speed)
            {
                if (_playing)
                {
                    if (mf_RateControl != null)
                    {
                        _speed = speed;
                        if (_speedSlider != null && setSlider) SpeedSlider_ValueToSlider(speed);

                        if (_fileStreamMode || _liveStreamMode || (_hasVideo && (_audioDevice == null || !_hasAudio)))
                        {
                            mf_AwaitCallback = true;
                            mf_RateControl.SetRate(_speedBoost, speed);
                            //mf_AwaitCallback = true;
                            WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);
                        }
                        else AV_UpdateTopology();

                        mf_Speed = speed;
                        if (_mediaSpeedChanged != null) _mediaSpeedChanged(this, EventArgs.Empty);
                    }
                    else
                    {
                        _lastError = HResult.MF_E_NOT_AVAILABLE;
                    }
                }
                else
                {
                    _speed = speed;
                    mf_Speed = speed;

                    if (_speedSlider != null && setSlider) SpeedSlider_ValueToSlider(speed);
                    if (_mediaSpeedChanged != null) _mediaSpeedChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion


        // ******************************** Player - Player Events

        #region Private - Player Events

        private void AV_SetDisplayEvents()
        {
            if (!_hasDisplayEvents)
            {
                try
                {
                    _display.Paint      += AV_DoPaint;
                    _display.Layout     += AV_DoLayout;
                    _hasDisplayEvents   = true;
                }
                catch { /* ignored */ }
            }
        }

        private void AV_RemoveDisplayEvents()
        {
            if (_hasDisplayEvents)
            {
                try
                {
                    _display.Paint  -= AV_DoPaint;
                    _display.Layout -= AV_DoLayout;
                }
                catch { /* ignored */ }
                _hasDisplayEvents = false;
                AV_RemoveOverlayEvents();
            }
        }

        private void AV_SetOverlayEvents()
        {
            if (!_hasOverlayEvents)
            {
                try
                {
                    Form parent = _display.FindForm();
                    parent.Move += AV_DoMove;
                    if (parent.IsMdiChild) parent.MdiParent.Move += AV_DoMove;

                    if (!(_display is Form))
                    {
                        _display.Move += AV__DoDisplayMove;
                        if (_hasOverlayClipping)
                        {
                            parent.Resize += AV_DoResize;
                            _hasOverlayClippingEvents = true;
                        }
                    }

                    _overlay.FormClosing += AV_Overlay_FormClosing;

                    _hasOverlayEvents = true;
                    if (!_overlayCanFocus) AV_SetOverlayFocusEvents();
                }
                catch { /* ignored */ }
                AV_MinimizeActivate(true);
            }
        }

        private void AV_RemoveOverlayEvents()
        {
            if (_hasOverlayEvents)
            {
                try
                {
                    Form parent = _display.FindForm();
                    parent.Move -= AV_DoMove;
                    if (parent.IsMdiChild) parent.MdiParent.Move -= AV_DoMove;

                    if (!(_display is Form)) _display.Move -= AV__DoDisplayMove;
                    if (_hasOverlayClippingEvents)
                    {
                        parent.Resize -= AV_DoResize;
                        _hasOverlayClippingEvents = false;
                        if (_overlay.Region != null)
                        {
                            _overlay.Region.Dispose();
                            _overlay.Region = null;
                        }
                    }

                    _overlay.FormClosing -= AV_Overlay_FormClosing;
                    AV_RemoveOverlayFocusEvents();
                }
                catch { /* ignored */ }
                _hasOverlayEvents = false;
                AV_MinimizeActivate(false);
            }
        }

        // Prevent overlay being closed by user (with OverlayCanFocus)
        private static void AV_Overlay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) e.Cancel = true;
        }

        private void AV_SetOverlayFocusEvents()
        {
            if (!_hasOverlayFocusEvents)
            {
                try
                {
                    _overlay.Activated      += AV_DoActivated;
                    _hasOverlayFocusEvents  = true;
                }
                catch { /* ignored */ }
            }
        }

        private void AV_RemoveOverlayFocusEvents()
        {
            if (_hasOverlayFocusEvents)
            {
                try
                {
                    _overlay.Activated -= AV_DoActivated;
                }
                catch { /* ignored */ }
                _hasOverlayFocusEvents = false;
            }
        }

        private void AV_DoLayout(object sender, LayoutEventArgs e)
        {
            if (_hasDisplayShape) AV_UpdateDisplayShape();
            else _display.Invalidate();
        }

        private void AV_DoPaint(object sender, PaintEventArgs e)
        {
            if ((_display.Width < 4) || (_display.Height < 4)) return;

            AV_GetDisplayModeSize(_displayMode);

            if (_hasOverlayEvents && _hasOverlayShown)
            {
                _overlay.SuspendLayout();
                if (_overlayMode == OverlayMode.Video && _hasVideo) // && _hasVideoBounds)
                {
                    _overlay.Location   = _display.PointToScreen(_videoBoundsClip.Location);
                    _overlay.Size       = _videoBoundsClip.Size;
                }
                else
                {
                    _overlay.Location   = _display.PointToScreen(Point.Empty);
                    _overlay.Size       = _display.DisplayRectangle.Size;
                }
                _overlay.ResumeLayout();
            }

            if (mf_VideoDisplayControl != null)
            {
                _videoDisplay.Bounds = _videoBounds;
                mf_VideoDisplayControl.SetVideoPosition(null, _videoDisplay.DisplayRectangle);
                mf_VideoDisplayControl.RepaintVideo();
            }
        }

        private void AV_DoMove(object sender, EventArgs e)
        {
            if (_overlayMode == OverlayMode.Display || !_hasVideoBounds)
            {
                _overlay.Location = _display.PointToScreen(_display.DisplayRectangle.Location);
            }
            else
            {
                if (_overlayMode == OverlayMode.Video)
                {
                    if (_videoBounds.Left < 0)
                    {
                        if (_videoBounds.Top < 0) _overlay.Location = _display.PointToScreen(_display.DisplayRectangle.Location);
                        else
                        {
                            _overlay.Location = _display.PointToScreen(new Point(_display.DisplayRectangle.Location.X, _videoBounds.Y));
                        }
                    }
                    else if (_videoBounds.Top < 0)
                    {
                        _overlay.Location = _display.PointToScreen(new Point(_videoBounds.X, _display.DisplayRectangle.Location.Y));
                    }
                    else _overlay.Location = _display.PointToScreen(_videoBounds.Location);
                }
                else // if (_overlayMode == OverlayMode.Clipped)
                {
                    _overlay.Location = _display.PointToScreen(_videoBounds.Location);
                }
            }
        }

        private void AV__DoDisplayMove(object sender, EventArgs e)
        {
            _display.Invalidate();
            if (_hasOverlayClipping)
            {
                _display.Refresh();
                AV_ClipOverlay();
            }
        }

        private void AV_DoResize(object sender, EventArgs e)
        {
            AV_ClipOverlay();
        }

        private void AV_DoActivated(object sender, EventArgs e)
        {
            if (_hasOverlayEvents)
            {
                if (_overlay.Owner.IsMdiChild)
                {
                    _overlay.Owner.MdiParent.Activate();
                    Application.DoEvents();
                }
                else
                {
                    Application.DoEvents();
                    _overlay.Owner.Activate();
                }
            }
        }

        internal void AV_EndOfMedia()
        {
            if (_repeat && _lastError == NO_ERROR && _fileMode)
            {
                if (!_busyStarting)
                {
                    _busyStarting = true;

                    if (_repeatCount++ > 0)
                    {
                        // without this, audio will start to stutter after a few repeats
                        System.Threading.Thread.Sleep(MF_REPEAT_WAIT_MS);
                    }

                    mf_StartTime.type = ConstPropVariant.VariantType.Int64;
                    mf_StartTime.longValue = _startTime;

                    _lastError = mf_MediaSession.Start(Guid.Empty, mf_StartTime);
                    mf_AwaitCallback = true;
                    if (!WaitForEvent.WaitOne(TIMEOUT_10_SECONDS)) _lastError = HResult.COR_E_TIMEOUT;

                    _busyStarting = false;

                    if (_lastError == NO_ERROR)
                    {
                        if (_mediaRepeated != null) _mediaRepeated(this, EventArgs.Empty);
                    }
                    else
                    {
                        AV_CloseSession(false, true, StopReason.Error);
                    }
                }
            }
            else
            {
                if (_lastError == NO_ERROR) AV_CloseSession(false, true, StopReason.Finished);
                else AV_CloseSession(false, true, StopReason.Error);
            }
        }

        #endregion

      
        // ******************************** Player - Main Timer

        #region Private - Main Timer Start / Stop / Event

        internal void StartMainTimerCheck()
        {
            if (!_timer.Enabled && _playing && !_paused && ((_hasAudio && pm_HasPeakMeter) || (_fileMode && (_hasPositionEvents || _hasPositionSlider || _hasTaskbarProgress) || pm_HasInputMeter))) _timer.Start();
        }

        internal void StopMainTimerCheck()
        {
            if (_timer.Enabled && (!_playing || _paused) || ((!_hasAudio || !pm_HasPeakMeter) && (!_fileMode || (!_hasPositionEvents && !_hasPositionSlider && !_hasTaskbarProgress) && !pm_HasInputMeter))) _timer.Stop();
        }

        internal void AV_TimerTick(object sender, EventArgs e)
        {
            if (!mf_Replay && !_seekBusy)
            {
                if (_hasPositionSlider)
                {
                    long pos = PositionX;
                    int val = (int)(PositionX * TICKS_TO_MS);

                    if (val <= _positionSlider.Minimum) _positionSlider.Value = _positionSlider.Minimum;
                    else if (val >= _positionSlider.Maximum) _positionSlider.Value = _positionSlider.Maximum;
                    else _positionSlider.Value = val;

                    if (_hasTaskbarProgress) _taskbarProgress.SetValue(pos);
                }
                else if (_hasTaskbarProgress)
                {
                    _taskbarProgress.SetValue(PositionX);
                }

                if (_mediaPositionChanged != null) OnMediaPositionChanged();
            }

            if (pm_HasPeakMeter)
            {
                if (_audioVolume == 0 || !_audioEnabled || _paused)
                {
                    if (!_outputLevelMuted)
                    {
                        _outputLevelArgs._channelCount    = pm_PeakMeterChannelCount;
                        _outputLevelArgs._masterPeakValue = STOP_VALUE;
                        _outputLevelArgs._channelsValues  = pm_PeakMeterValuesStop;
                        _outputLevelMuted                 = true;
                        _mediaPeakLevelChanged(this, _outputLevelArgs);
                    }
                }
                else
                {
                    PeakMeter_GetValues();
                    _outputLevelArgs._channelCount    = pm_PeakMeterChannelCount;
                    _outputLevelArgs._masterPeakValue = pm_PeakMeterMasterValue;
                    _outputLevelArgs._channelsValues  = pm_PeakMeterValues;
                    _outputLevelMuted                 = false;
                    _mediaPeakLevelChanged(this, _outputLevelArgs);
                }
            }

            if (pm_HasInputMeter)
            {
                if (_paused)
                {
                    if (!_inputLevelMuted)
                    {
                        _inputLevelArgs._channelCount    = pm_InputMeterChannelCount;
                        _inputLevelArgs._masterPeakValue = STOP_VALUE;
                        _inputLevelArgs._channelsValues  = pm_InputMeterValuesStop;
                        _inputLevelMuted                 = true;
                        _mediaInputLevelChanged(this, _inputLevelArgs);
                    }
                }
                else
                {
                    InputMeter_GetValues();
                    _inputLevelArgs._channelCount    = pm_InputMeterChannelCount;
                    _inputLevelArgs._masterPeakValue = pm_InputMeterMasterValue;
                    _inputLevelArgs._channelsValues  = pm_InputMeterValues;
                    _inputLevelMuted                 = false;
                    _mediaInputLevelChanged(this, _inputLevelArgs);
                }
            }
        }

        #endregion


        // ******************************** Player - Slider (TrackBar) Managers

        #region Public - Slider (TrackBar) Managers

        #region Public - Sliders

        /// <summary>
        /// Provides access to the sliders (trackbars) settings of the player (for example, Player.Sliders.Position).
        /// </summary>
        public Sliders Sliders
        {
            get
            {
                if (_slidersClass == null) _slidersClass = new Sliders(this);
                return _slidersClass;
            }
        }

        #endregion

        #region Public - PositionSlider Manager

        #region PositionSlider Fields

        internal int        _psMouseWheel       = 0;    // 0 = disabled
        internal int        _psMouseWheelShift  = 5000;

        internal TrackBar   _positionSlider;
        internal bool       _hasPositionSlider;

        internal bool       _psHorizontal;
        internal bool       _psLiveSeek;        // = true; from v 0.95 false
        internal SilentSeek _psSilentSeek       = SilentSeek.OnMoving;
        internal bool       _psTracking;

        internal bool       _psHandlesProgress  = false;
        internal int        _psValue;
        internal bool       _psBusy;
        internal bool       _psSkipped;

        internal bool       _psMuteOnMove;
        internal bool       _psMuteAlways;
        private bool        _psMuted;

        internal Timer      _psTimer;

        #endregion

        internal void PositionSlider_MouseDown(object sender, MouseEventArgs e)
        {
            if (_playing && _fileMode && _mediaLength > 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _psTracking = true;
                    _psSkipped  = false;
                    _psMuted = false;

                    _timer.Stop();
                    _positionSlider.Scroll -= PositionSlider_Scroll;

                    float pos;
                    if (_psHorizontal)
                    {
                        if (e.X <= 13) pos = 0;
                        else if (e.X >= _positionSlider.Width - 13) pos = 1;
                        else pos = (float)(e.X - 13) / (_positionSlider.Width - 27);
                    }
                    else
                    {
                        if (e.Y <= 13) pos = 1;
                        else if (e.Y >= _positionSlider.Height - 13) pos = 0;
                        else pos = 1 - (float)(e.Y - 13) / (_positionSlider.Height - 27);
                    }

                    if (_psHandlesProgress)
                    {
                        if (Math.Abs(_positionSlider.Value - (int)(pos * (_positionSlider.Maximum - _positionSlider.Minimum)) + _positionSlider.Minimum) > 5) _positionSlider.Value = (int)(pos * (_positionSlider.Maximum - _positionSlider.Minimum)) + _positionSlider.Minimum;
                    }
                    else if (Math.Abs(_positionSlider.Value - (int)(pos * _positionSlider.Maximum)) > 5) _positionSlider.Value = (int)(pos * _positionSlider.Maximum);
                    _psValue = _positionSlider.Value;

                    if (_psLiveSeek)
                    {
                        if (_hasAudio && _audioEnabled)
                        {
                            if (_paused || _psSilentSeek == SilentSeek.Always)
                            {
                                _psMuteAlways = true;
                                //mf_AudioStreamVolume.GetAllVolumes(_audioChannelCount, _audioChannelsVolume);
                                mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsMute);
                                _psMuted = true;
                            }
                            else if (_psSilentSeek == SilentSeek.OnMoving) _psMuteOnMove = true;
                        }
                        PositionX = _positionSlider.Value * MS_TO_TICKS;
                    }
                    else
                    {
                        if (_hasAudio && _audioEnabled && _paused)
                        {
                            mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsMute);
                            _psMuted = true;
                        }

                        if (st_HasSubtitles)
                        {
                            st_SubtitleChangedArgs._index    = NO_VALUE;
                            st_SubtitleChangedArgs._subtitle = string.Empty;
                            _mediaSubtitleChanged(this, st_SubtitleChangedArgs);
                            st_SubtitlesBusy                 = true;
                        }
                    }

                    _positionSlider.MouseMove += PositionSlider_MouseMove;
                    _positionSlider.MouseUp   += PositionSlider_MouseUp;
                }
            }
            else
            {
                _positionSlider.Value = 0;
                _psValue = 0;
            }
        }

        internal void PositionSlider_MouseMove(object sender, MouseEventArgs e)
        {
            if (_psBusy) _psSkipped = true;
            else
            {
                if (_positionSlider.Value != _psValue)
                {
                    _psBusy = true;
                    if (_psMuteOnMove)
                    {
                        if (!_psTimer.Enabled)
                        {
                            //mf_AudioStreamVolume.GetAllVolumes(_audioChannelCount, _audioChannelsVolume);
                            mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsMute);
                            _psMuted = true;
                        }
                    }
                    do
                    {
                        _psSkipped = false;
                        if (_psLiveSeek)
                        {
                            PositionX = _positionSlider.Value * MS_TO_TICKS;
                            // bad & causes problems but needed to update application position counters
                            // not needed when using PositionX method1:
                            //if (_seekCustom) Application.DoEvents();
                            Application.DoEvents();
                        }
                        else
                        {
                            if (_hasTaskbarProgress) _taskbarProgress.SetValue(_positionSlider.Value * MS_TO_TICKS);
                            if (_mediaPositionChanged != null) OnMediaPositionChanged();
                        }

                    } while (_psSkipped);

                    if (_psMuteOnMove) { _psTimer.Stop(); _psTimer.Start(); }
                    _psBusy = false;
                }
            }
        }

        internal void PositionSlider_MouseUp(object sender, MouseEventArgs e)
        {
            //PositionSlider_StopTracking();

            if (!_psLiveSeek) PositionX = _positionSlider.Value * MS_TO_TICKS;

            PositionSlider_StopTracking();

            if (!_paused) StartMainTimerCheck();

            //_psTracking = false;
        }

        internal void PositionSlider_TimerTick(object sender, EventArgs e)
        {
            mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsVolume);
            _psTimer.Stop();
            _psMuted = false;
        }

        // also called from Close Session
        private void PositionSlider_StopTracking()
        {
            if (_psLiveSeek)
            {
                if (_psMuteAlways)
                {
                    _psMuteAlways = false;
                    //if (_paused) System.Threading.Thread.Sleep(MF_UPDATE_WAIT_MS); // prevent audio 'click'
                    //mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsVolume);
                }
                else if (_psMuteOnMove)
                {
                    if (_psTimer.Enabled)
                    {
                        _psTimer.Stop();
                        //if (_paused) System.Threading.Thread.Sleep(MF_UPDATE_WAIT_MS); // prevent audio 'click'
                        //mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsVolume);
                    }
                    _psMuteOnMove = false;
                }

                //if (_psMuted)
                //{
                //    //if (_paused) System.Threading.Thread.Sleep(300); // prevent audio 'click'
                //    mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsVolume);
                //    _psMuted = false;
                //}
            }
            else
            {
                st_SubtitlesBusy = false;
            }

            if (_psMuted)
            {
                if (_paused) System.Threading.Thread.Sleep(300); // prevent audio 'click'
                mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsVolume);
                _psMuted = false;
            }

            _positionSlider.Scroll += PositionSlider_Scroll;
            _positionSlider.MouseMove -= PositionSlider_MouseMove;
            _positionSlider.MouseUp -= PositionSlider_MouseUp;

            _psTracking = false;
        }

        internal void PositionSlider_Scroll(object sender, EventArgs e)
        {
            if (_playing && _fileMode && _mediaLength > 0)
            {
                if (!_psTracking)
                {
                    if (_psBusy) _psSkipped = true;
                    else
                    {
                        _psBusy = true;
                        do
                        {
                            _psSkipped = false;
                            PositionX = _positionSlider.Value * MS_TO_TICKS;

                        } while (_psSkipped);
                        _psBusy = false;
                    }
                }
            }
            else
            {
                _positionSlider.Value = 0;
                _psValue = 0;
            }
        }

        internal void PositionSlider_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;

            if (!_psBusy && _playing && _psMouseWheel > 0)
            {
                _psBusy = true;

                int pos = _positionSlider.Value;
                if (e.Delta > 0)
                {
                    if (Control.ModifierKeys == Keys.Shift) pos += _psMouseWheelShift;
                    else pos += _psMouseWheel;
                    if (pos > _positionSlider.Maximum) pos = _positionSlider.Maximum;
                }
                else
                {
                    if (Control.ModifierKeys == Keys.Shift) pos -= _psMouseWheelShift;
                    else pos -= _psMouseWheel;
                    if (pos < _positionSlider.Minimum) pos = _positionSlider.Minimum;
                }

                _positionSlider.Value = pos;
                PositionX = pos * MS_TO_TICKS;

                _psBusy = false;
            }
        }

        #endregion

        #region Public - ShuttleSlider Manager

        internal TrackBar   _shuttleSlider;
        internal bool       _hasShuttleSlider;
        private bool        _pauseSet;

        internal void ShuttleSlider_MouseDown(object sender, MouseEventArgs e)
        {
            if (Control.MouseButtons == MouseButtons.Left)
            {
                _shuttleSlider.MouseUp += ShuttleSlider_MouseUp;
                if (_playing && _fileMode)
                {
                    _pauseSet = !_paused;
                    if (!_paused)
                    {
                        mf_MediaSession.Pause();
                        _paused = true;
                    }

                    if (_hasAudio && _audioEnabled) mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsMute);

                    if (_audioDevice == null) // mf bug - rate sets system default audio device
                    {
                        mf_AwaitCallback = true;
                        mf_RateControl.SetRate(_speedBoost, mf_SpeedMinimum);
                        //mf_AwaitCallback = true;
                        WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);
                    }

                    long pos;
                    long stepTicks = _hasVideo ? _videoFrameStep : AUDIO_STEP_TICKS;
                    int sliderVal;

                    while (Control.MouseButtons == MouseButtons.Left)
                    {
                        sliderVal = _shuttleSlider.Value;
                        if (sliderVal != 0)
                        {
                            pos = PositionX;
                            if (pos > 0) // catch errors
                            {
                                SetPosition(pos + (sliderVal * stepTicks));
                                System.Threading.Thread.Sleep(40);
                            }
                        }
                        Application.DoEvents();
                    }
                }
            }
        }

        internal void ShuttleSlider_MouseUp(object sender, MouseEventArgs e)
        {
            _shuttleSlider.MouseUp -= ShuttleSlider_MouseUp;

            _shuttleSlider.Value = 0;

            if (_playing)
            {
                if (_audioDevice == null)
                {
                    mf_AwaitCallback = true;
                    mf_RateControl.SetRate(_speedBoost, _speed);
                    //mf_AwaitCallback = true;
                    WaitForEvent.WaitOne(TIMEOUT_5_SECONDS);
                }
                else
                {
                    AV_UpdateTopology();
                }

                if (_pauseSet) Resume();
                if (_hasAudio && _audioEnabled) mf_AudioStreamVolume.SetAllVolumes(_audioChannelCount, _audioChannelsVolume);
            }
        }

        internal void ShuttleSlider_MouseWheel(object sender, MouseEventArgs e)
        {
            // no longer supported
            ((HandledMouseEventArgs)e).Handled = true;
        }

        #endregion

        #region Public - VolumeSlider Manager

        internal TrackBar _volumeSlider;

        internal void VolumeSlider_Scroll(object sender, EventArgs e)
        {
            AV_SetAudioVolume(_volumeSlider.Value * 0.01f, false, true);
        }

        internal void VolumeSlider_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;

            float volume = _audioVolume;

            if (e.Delta > 0) volume += 0.1f;
            else volume -= 0.1f;

            if (volume >= AUDIO_VOLUME_MAXIMUM) volume = AUDIO_VOLUME_MAXIMUM;
            else if (volume < AUDIO_VOLUME_MINIMUM) volume = AUDIO_VOLUME_MINIMUM;

            AV_SetAudioVolume((float)Math.Round(volume, 3), true, true);
        }

        #endregion

        #region Public - BalanceSlider Manager

        internal TrackBar _balanceSlider;

        internal void BalanceSlider_Scroll(object sender, EventArgs e)
        {
            AV_SetAudioBalance(_balanceSlider.Value * 0.01f, false, true);
        }

        internal void BalanceSlider_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;

            float balance = _audioBalance;

            if (e.Delta > 0) balance += 0.1f;
            else balance -= 0.1f;

            if (balance >= AUDIO_BALANCE_MAXIMUM) balance = AUDIO_BALANCE_MAXIMUM;
            else if (balance < AUDIO_BALANCE_MINIMUM) balance = AUDIO_BALANCE_MINIMUM;

            AV_SetAudioBalance((float)Math.Round(balance, 3), true, true);
        }

        #endregion

        #region Public - SpeedSlider Manager

        internal TrackBar   _speedSlider;
        internal bool       _speedSliderBusy;
        private float       _scrollSpeed = 1.0f;
        private bool        _mouseDown;

        internal void SpeedSlider_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _scrollSpeed = _speed;
                _speedSkipped = false;
                _speedSlider.MouseUp += SpeedSlider_MouseUp;
                _mouseDown = true;
            }
        }

        internal void SpeedSlider_Scroll(object sender, EventArgs e)
        {
            if (_speedSliderBusy)
            {
                _speedSkipped = true;
            }
            else
            {
                _speedSliderBusy = true;
                do
                {
                    _speedSkipped = false;

                    switch (_speedSlider.Value)
                    {
                        case 0:
                            _scrollSpeed = 0.10f;
                            break;
                        case 1:
                            _scrollSpeed = 0.25f;
                            break;
                        case 2:
                            _scrollSpeed = 0.33f;
                            break;
                        case 3:
                            _scrollSpeed = 0.50f;
                            break;
                        case 4:
                            _scrollSpeed = 0.67f;
                            break;
                        case 5:
                            _scrollSpeed = 0.75f;
                            break;
                        case 6:
                            _scrollSpeed = 1.0f;
                            break;
                        case 7:
                            _scrollSpeed = 1.5f;
                            break;
                        case 8:
                            _scrollSpeed = 2.0f;
                            break;
                        case 9:
                            _scrollSpeed = 2.5f;
                            break;
                        case 10:
                            _scrollSpeed = 3.0f;
                            break;
                        case 11:
                            _scrollSpeed = 3.5f;
                            break;
                        default:
                            _scrollSpeed = 4.0f;
                            break;
                    }

                    if (_playing)
                    {
                        if (_scrollSpeed < mf_SpeedMinimum)
                        {
                            _scrollSpeed = mf_SpeedMinimum;
                            SpeedSlider_ValueToSlider(_scrollSpeed);
                        }
                        else if (_scrollSpeed > mf_SpeedMaximum)
                        {
                            _scrollSpeed = mf_SpeedMaximum;
                            SpeedSlider_ValueToSlider(_scrollSpeed);
                        }
                    }

                } while (_speedSkipped);

                if (!_mouseDown)
                {
                    AV_SetSpeed(_scrollSpeed, false);
                }
                else
                {
                    if (_mediaSpeedChanged != null)
                    {
                        _speed = _scrollSpeed;
                        _mediaSpeedChanged(this, EventArgs.Empty);
                    }
                }
                _speedSliderBusy = false;
            }
        }

        internal void SpeedSlider_MouseUp(object sender, MouseEventArgs e)
        {
            _speedSlider.MouseUp -= SpeedSlider_MouseUp;
            _mouseDown = false;

            //if (_audioDevice != null)
            {
                //mf_Speed = 0;
                AV_SetSpeed(_scrollSpeed, false);
            }
            //else
            //{
            //    if (_playing && _scrollSpeed != _speed)
            //    {
            //        SpeedSlider_ValueToSlider(_speed);
            //        if (_mediaSpeedChanged != null) _mediaSpeedChanged(this, EventArgs.Empty);
            //    }
            //}
        }

        internal void SpeedSlider_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
            // can't use with mf bug
        }

        internal void SpeedSlider_ValueToSlider(float speed)
        {
            int sliderVal;

            if (speed < 0.875)
            {
                if (speed < 0.175)      sliderVal = 0;
                else if (speed < 0.292) sliderVal = 1;
                else if (speed < 0.416) sliderVal = 2;
                else if (speed < 0.583) sliderVal = 3;
                else if (speed < 0.708) sliderVal = 4;
                else sliderVal = 5;
            }
            else
            {
                if (speed < 1.250)      sliderVal = 6;
                else if (speed < 1.750) sliderVal = 7;
                else if (speed < 2.250) sliderVal = 8;
                else if (speed < 2.750) sliderVal = 9;
                else if (speed < 3.250) sliderVal = 10;
                else if (speed < 3.750) sliderVal = 11;
                else sliderVal = 12;
            }

            _speedSlider.Value = sliderVal;
        }

        #endregion

        #region Public - Brightness Slider Manager

        internal TrackBar _brightnessSlider;

        internal void BrightnessSlider_Scroll(object sender, EventArgs e)
        {
            AV_SetBrightness(_brightnessSlider.Value * 0.01, false);
        }

        internal void BrightnessSlider_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;

            double change = 0.01;
            if (e.Delta < 0) change = -0.01;
            AV_SetBrightness(Math.Round(_brightness + change, 3), true);
        }

        #endregion

        #region Public - Contrast Slider Manager

        internal TrackBar _contrastSlider;

        internal void ContrastSlider_Scroll(object sender, EventArgs e)
        {
            AV_SetContrast(_contrastSlider.Value * 0.01, false);
        }

        internal void ContrastSlider_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;

            double change = 0.01;
            if (e.Delta < 0) change = -0.01;
            AV_SetContrast(Math.Round(_contrast + change, 3), true);
        }

        #endregion

        #region Public - Hue Slider Manager

        internal TrackBar _hueSlider;

        internal void HueSlider_Scroll(object sender, EventArgs e)
        {
            AV_SetHue(_hueSlider.Value * 0.01, false);
        }

        internal void HueSlider_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;

            double change = 0.01;
            if (e.Delta < 0) change = -0.01;
            AV_SetHue(Math.Round(_hue + change, 3), true);
        }

        #endregion

        #region Public - Saturation Slider Manager

        internal TrackBar _saturationSlider;

        internal void SaturationSlider_Scroll(object sender, EventArgs e)
        {
            AV_SetSaturation(_saturationSlider.Value * 0.01, false);
        }

        internal void SaturationSlider_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;

            double change = 0.01;
            if (e.Delta < 0) change = -0.01;
            AV_SetSaturation(Math.Round(_saturation + change, 3), true);
        }

        #endregion

        #endregion

    }
}
