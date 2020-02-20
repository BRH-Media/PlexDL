/****************************************************************

    PVS.MediaPlayer - Version 0.98.1
    December 2019, The Netherlands
    © Copyright 2019 PVS The Netherlands - licensed under The Code Project Open License (CPOL)

    PVS.MediaPlayer uses (part of) the Media Foundation .NET library by nowinskie and snarfle (https://sourceforge.net/projects/mfnet).
    Licensed under either Lesser General Public License v2.1 or BSD.  See license.txt or BSDL.txt for details (http://mfnet.sourceforge.net).

    ****************

    For use with Microsoft Windows 7 or higher, Microsoft .NET Framework version 2.0 or higher and WinForms any CPU.
    Created with Microsoft Visual Studio.

    Article on CodeProject with information on the use of the PVS.MediaPlayer library:
    https://www.codeproject.com/Articles/109714/PVS-MediaPlayer-Audio-and-Video-Player-Library

    ****************

    The PVS.MediaPlayer library source code is divided into 8 files:

    1. Player.cs        - main source code
    2. SubClasses.cs    - various grouping and information classes
    3. Interop.cs       - unmanaged Win32 functions
    4. PeakMeter.cs     - audio peak level values
    5. DisplayClones.cs - multiple video displays
    6. CursorHide.cs    - hides the mouse cursor during inactivity
    7. Subtitles.cs     - subrip (.srt) subtitles
    8. Infolabel.cs     - custom ToolTip

    Required references:
    System
    System.Drawing
    System.Windows.Forms

    ****************

    This file: SubClasses.cs

    Video Track Class
    Video Stream Struct
    Video Display Class
    WebCam Device Class
    Audio Track Class
    Audio Stream Struct
    Audio Device Class
    Audio Input Device Class
    Slider Value Class
    Metadata Class
    Clone Properties Class
    MF Callback Class
    Hide System Object Members Classes
    Grouping Classes Player

    ****************

    Thanks!

    Many thanks to Microsoft (Windows, .NET Framework, Visual Studio and others), all the people
    writing about programming on the internet (a great source for ideas and solving problems),
    the websites publishing those or other writings about programming, the people responding to the
    PVS.MediaPlayer articles with comments and suggestions and, of course, the people at CodeProject.

    Special thanks to the creators of Media Foundation .NET for their great library!

    Special thanks to Sean Ewington and Deeksha Shenoy of CodeProject who also took care of publishing the many
    code updates and changes in the PVS.MediaPlayer articles in a friendly, fast, and highly competent manner.
    Thank you very much, Sean and Deeksha!

    Peter Vegter
    December 2019, The Netherlands

    ****************************************************************/

#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#endregion Usings

namespace PVS.MediaPlayer
{
    // ******************************** Device Info Class

    #region Device Info Class

    /// <summary>
    /// A class that is used as a base class for device information classes.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class DeviceInfo : HideObjectMembers
    {
        #region Fields (Device Class)

        internal string _id;
        internal string _name;
        internal string _adapter;

        #endregion Fields (Device Class)

        /// <summary>
        /// Gets the identifier of the device.
        /// </summary>
        public string Id { get { return _id; } }

        /// <summary>
        /// Gets the description of the device.
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Gets the name of the adapter to which the device is attached.
        /// </summary>
        public string Adapter { get { return _adapter; } }

        /// <summary>
        /// Returns a string that represents this device information.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} ({1})", _name, _adapter);
        }
    }

    #endregion Device Info Class

    // ******************************** Video Track Class

    #region Video Track Class

    /// <summary>
    /// A class that is used to store media video track information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class VideoTrack : HideObjectMembers
    {
        #region VideoTrack Fields

        internal Guid _mediaType;
        internal string _name;
        internal string _language;
        internal float _frameRate;
        internal int _width;
        internal int _height;

        #endregion VideoTrack Fields

        internal VideoTrack()
        {
        }

        /// <summary>
        /// Gets the media type (MF GUID) of the track (see Media Foundation documentation).
        /// </summary>
        public Guid MediaType { get { return _mediaType; } }

        /// <summary>
        /// Gets the name of the track.
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Gets the language of the track.
        /// </summary>
        public string Language { get { return _language; } }

        /// <summary>
        /// Gets the frame rate of the track.
        /// </summary>
        public float FrameRate { get { return _frameRate; } }

        /// <summary>
        /// Gets the video width of the track.
        /// </summary>
        public int Width { get { return _width; } }

        /// <summary>
        /// Gets the video height of the track.
        /// </summary>
        public int Height { get { return _height; } }
    }

    #endregion Video Track Class

    // ******************************** Video Stream Struct

    #region Video Stream Struct

    internal struct VideoStream
    {
        internal Guid MediaType;
        internal int StreamIndex;
        internal bool Selected;
        internal string Name;
        internal string Language;
        internal float FrameRate;
        internal int SourceWidth;
        internal int SourceHeight;
    }

    #endregion Video Stream Struct

    // ******************************** Video Display Class

    #region Video Display Class

    internal sealed class VideoDisplay : Control
    {
        public VideoDisplay()
        {
            SetStyle(ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0084) m.Result = (IntPtr)(-1);
            else base.WndProc(ref m);
        }
    }

    #endregion Video Display Class

    // ******************************** Webcam Device Class

    #region Webcam Device Class

    /// <summary>
    /// A class that is used to provide webcam device information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class WebcamDevice : DeviceInfo
    {
        internal WebcamDevice()
        {
        }
    }

    #endregion Webcam Device Class

    // ******************************** Webcam Property Class

    #region Webcam Property Class

    /// <summary>
    /// A class that is used to store webcam property information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class WebcamProperty : HideObjectMembers
    {
        #region Fields (WebcamProperty Class)

        //internal string _name;
        internal bool _supported;

        internal int _min;
        internal int _max;
        internal int _step;
        internal int _default;
        internal int _current;
        internal bool _autoSupport;
        internal bool _auto;

        #endregion Fields (WebcamProperty Class)

        internal WebcamProperty()
        {
        }

        ///// <summary>
        ///// The name of the property.
        ///// </summary>
        //public string Name
        //{ get { return _name; } }

        /// <summary>
        /// A value indicating whether the property is supported by the webcam.
        /// </summary>
        public bool Supported
        {
            get { return _supported; }
        }

        /// <summary>
        /// The minimum value of the webcam property.
        /// </summary>
        public int Minimum { get { return _min; } }

        /// <summary>
        /// The maximum value of the webcam property.
        /// </summary>
        public int Maximum { get { return _max; } }

        /// <summary>
        /// The default value of the webcam property.
        /// </summary>
        public int Default { get { return _default; } }

        /// <summary>
        /// The step size for the webcam property. The step size is the smallest increment by which the webcam property can change.
        /// </summary>
        public int StepSize { get { return _step; } }

        /// <summary>
        /// Gets or sets the value of the webcam property. When set, the Automatic setting is set to false (manual control).
        /// </summary>
        public int Value
        {
            get { return _current; }
            set
            {
                _current = value;
                _auto = false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the property can be controlled automatically by the webcam. See also: WebcamProperty.AutoEnabled.
        /// </summary>
        public bool AutoSupport
        {
            get { return _autoSupport; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the property is controlled automatically by the webcam. See also: WebcamProperty.AutoSupported.
        /// </summary>
        public bool AutoEnabled
        {
            get { return _auto; }
            set { _auto = value; }
        }
    }

    #endregion Webcam Property Class

    // ******************************** Webcam Video Format Class

    #region Webcam Video Format Class

    /// <summary>
    /// A class that is used to store webcam video output format information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class WebcamFormat : HideObjectMembers
    {
        internal int _streamIndex;
        internal int _typeIndex;
        internal int _width;
        internal int _height;
        internal float _frameRate;

        internal WebcamFormat(int streamIndex, int typeIndex, int width, int height, float frameRate)
        {
            _streamIndex = streamIndex;
            _typeIndex = typeIndex;
            _width = width;
            _height = height;
            _frameRate = frameRate;
        }

        /// <summary>
        /// Gets the track number of the format.
        /// </summary>
        public int Track { get { return _streamIndex; } }

        /// <summary>
        /// Gets the index of the format.
        /// </summary>
        public int Index { get { return _typeIndex; } }

        /// <summary>
        /// Gets the width of the video frames of the format, in pixels.
        /// </summary>
        public int VideoWidth { get { return _width; } }

        /// <summary>
        /// Gets the height of the video frames of the format, in pixels.
        /// </summary>
        public int VideoHeight { get { return _height; } }

        /// <summary>
        /// Gets the video frame rate of the format, in frames per second.
        /// </summary>
        public float FrameRate { get { return _frameRate; } }
    }

    #endregion Webcam Video Format Class

    // ******************************** Audio Track Class

    #region Audio Track Class

    /// <summary>
    /// A class that is used to store media audio track information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class AudioTrack : HideObjectMembers
    {
        #region AudioTrack Fields

        internal Guid _mediaType;
        internal string _name;
        internal string _language;
        internal int _channelCount;
        internal int _samplerate;
        internal int _bitdepth;
        internal int _bitrate;

        #endregion AudioTrack Fields

        internal AudioTrack()
        {
        }

        /// <summary>
        /// Gets the media type (GUID) of the track (see Media Foundation documentation).
        /// </summary>
        public Guid MediaType { get { return _mediaType; } }

        /// <summary>
        /// Gets the name of the track.
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Gets the language of the track.
        /// </summary>
        public string Language { get { return _language; } }

        /// <summary>
        /// Gets the number of channels in the track.
        /// </summary>
        public int ChannelCount { get { return _channelCount; } }

        /// <summary>
        /// Gets the sample rate of the track.
        /// </summary>
        public int SampleRate { get { return _samplerate; } }

        /// <summary>
        /// Gets the bit depth of the track.
        /// </summary>
        public int BitDepth { get { return _bitdepth; } }

        /// <summary>
        /// Gets the bit rate of the track.
        /// </summary>
        public int Bitrate { get { return _bitrate; } }
    }

    #endregion Audio Track Class

    // ******************************** Audio Stream Struct

    #region Audio Stream Struct

    internal struct AudioStream
    {
        internal Guid MediaType;
        internal int StreamIndex;
        internal bool Selected;
        internal string Name;
        internal string Language;
        internal int ChannelCount;
        internal int Samplerate;
        internal int Bitdepth;
        internal int Bitrate;
    }

    #endregion Audio Stream Struct

    // ******************************** Audio Device Class

    #region Audio Device Class

    /// <summary>
    /// A class that is used to provide audio output device information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class AudioDevice : DeviceInfo
    {
        internal AudioDevice()
        {
        }
    }

    #endregion Audio Device Class

    // ******************************** Audio Input Device Class

    #region Audio Input Device Class

    /// <summary>
    /// A class that is used to provide audio input device information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class AudioInputDevice : DeviceInfo
    {
        internal AudioInputDevice()
        {
        }
    }

    #endregion Audio Input Device Class

    // ******************************** Slider Value Class

    #region Slider Value Class

    /// <summary>
    /// A static class that provides location information for values on a slider (trackbar).
    /// </summary>
    [CLSCompliant(true)]
    public static class SliderValue
    {
        #region Fields (SliderValue Class))

        // standard .Net TrackBar track margins (pixels between border and begin/end of track)
        private const int SLIDER_LEFT_MARGIN = 13;

        private const int SLIDER_RIGHT_MARGIN = 14;
        private const int SLIDER_TOP_MARGIN = 13;
        private const int SLIDER_BOTTOM_MARGIN = 14;

        #endregion Fields (SliderValue Class))

        /// <summary>
        /// Returns the slider value at the specified location on the specified slider (trackbar).
        /// </summary>
        /// <param name="slider">The slider whose value should be obtained.</param>
        /// <param name="location">The relative x- and y-coordinates on the slider.</param>
        public static int FromPoint(TrackBar slider, Point location)
        {
            return FromPoint(slider, location.X, location.Y);
        }

        /// <summary>
        /// Returns the slider value at the specified location on the specified slider (trackbar).
        /// </summary>
        /// <param name="slider">The slider whose value should be obtained.</param>
        /// <param name="x">The relative x-coordinate on the slider (for horizontal oriented sliders).</param>
        /// <param name="y">The relative y-coordinate on the slider (for vertical oriented sliders).</param>
        public static int FromPoint(TrackBar slider, int x, int y)
        {
            if (slider == null) return 0;

            float pos;
            if (slider.Orientation == Orientation.Horizontal)
            {
                if (x <= SLIDER_LEFT_MARGIN) pos = 0;
                else if (x >= slider.Width - SLIDER_LEFT_MARGIN) pos = 1;
                else pos = (float)(x - SLIDER_LEFT_MARGIN) / (slider.Width - (SLIDER_LEFT_MARGIN + SLIDER_RIGHT_MARGIN));
            }
            else
            {
                if (y <= SLIDER_TOP_MARGIN) pos = 1;
                else if (y >= slider.Height - SLIDER_TOP_MARGIN) pos = 0;
                else pos = 1 - (float)(y - SLIDER_TOP_MARGIN) / (slider.Height - (SLIDER_TOP_MARGIN + SLIDER_BOTTOM_MARGIN));
            }
            //pos = (pos * (slider.Maximum - slider.Minimum)) + slider.Minimum;
            return (int)(pos * (slider.Maximum - slider.Minimum)) + slider.Minimum;
        }

        /// <summary>
        /// Returns the location of the specified value on the specified slider (trackbar).
        /// </summary>
        /// /// <param name="slider">The slider whose value location should be obtained.</param>
        /// <param name="value">The value of the slider.</param>
        public static Point ToPoint(TrackBar slider, int value)
        {
            Point result = Point.Empty;
            if (slider != null)
            {
                double pos = 0;
                if (value > slider.Minimum)
                {
                    if (value >= slider.Maximum) pos = 1;
                    else pos = (double)(value - slider.Minimum) / (slider.Maximum - slider.Minimum);
                }
                if (slider.Orientation == Orientation.Horizontal) result.X = (int)(pos * (slider.Width - (SLIDER_LEFT_MARGIN + SLIDER_RIGHT_MARGIN)) + 0.5) + SLIDER_LEFT_MARGIN;
                else result.Y = (int)(pos * (slider.Height - (SLIDER_TOP_MARGIN + SLIDER_BOTTOM_MARGIN)) + 0.5) + SLIDER_TOP_MARGIN;
            }
            return result;
        }
    }

    #endregion Slider Value Class

    // ******************************** Metadata Class

    #region Metadata Class

    /// <summary>
    /// A class that is used to store metadata properties obtained from media files.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class Metadata : HideObjectMembers, IDisposable
    {
        #region Fields (Metadata Class)

        internal string _artist;
        internal string _title;
        internal string _album;
        internal int _trackNumber;
        internal string _year;
        internal TimeSpan _duration;
        internal string _genre;
        internal Image _image;

        private bool _disposed;

        #endregion Fields (Metadata Class)

        internal Metadata()
        {
        }

        /// <summary>
        /// Gets the main artist(s)/performer(s)/band/orchestra of the media.
        /// </summary>
        public string Artist { get { return _artist; } }

        /// <summary>
        /// Gets the title of the media.
        /// </summary>
        public string Title { get { return _title; } }

        /// <summary>
        /// Gets the title of the album that contains the media.
        /// </summary>
        public string Album { get { return _album; } }

        /// <summary>
        /// Gets the track number of the media.
        /// </summary>
        public int TrackNumber { get { return _trackNumber; } }

        /// <summary>
        /// Gets the year the media was published.
        /// </summary>
        public string Year { get { return _year; } }

        /// <summary>
        /// Gets the duration (length) of the media.
        /// </summary>
        public TimeSpan Duration { get { return _duration; } }

        /// <summary>
        /// Gets the genre of the media.
        /// </summary>
        public string Genre { get { return _genre; } }

        /// <summary>
        /// Gets the image attached to the media.
        /// </summary>
        public Image Image { get { return _image; } }

        /// <summary>
        /// Remove the media tag information and clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _artist = null;
                _title = null;
                _album = null;
                _year = null;
                _genre = null;
                if (_image != null)
                {
                    try { _image.Dispose(); }
                    catch { /* ignore */ }
                    _image = null;
                }
            }
        }
    }

    #endregion Metadata Class

    // ******************************** Clone Properties Class

    #region Clone Properties Class

    /// <summary>
    /// A class that is used to store display clone properties.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class CloneProperties : HideObjectMembers //, IDisposable
    {
        #region Fields (Clone Properties Class)

        internal CloneQuality _quality = CloneQuality.Auto;
        internal CloneLayout _layout = CloneLayout.Zoom;
        internal CloneFlip _flip = CloneFlip.FlipNone;
        internal DisplayShape _shape = DisplayShape.Normal;
        internal bool _videoShape = true;
        internal bool _dragEnabled = false;
        internal Cursor _dragCursor = Cursors.SizeAll;

        #endregion Fields (Clone Properties Class)

        /// <summary>
        /// Gets or sets the video quality setting of the display clone (default: CloneQuality.Auto).
        /// </summary>
        public CloneQuality Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }

        /// <summary>
        /// Gets or sets the video layout setting of the display clone (default: CloneLayout.Zoom).
        /// </summary>
        public CloneLayout Layout
        {
            get { return _layout; }
            set { _layout = value; }
        }

        /// <summary>
        /// Gets or sets the video flip setting of the display clone (default: Cloneflip.FlipNone).
        /// </summary>
        public CloneFlip Flip
        {
            get { return _flip; }
            set { _flip = value; }
        }

        /// <summary>
        /// Gets or sets the shape of the display clone (default: DisplayShape.Normal). If the display clone is a form, set its BorderStyle to None. See also: VideoShape.
        /// </summary>
        public DisplayShape Shape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Shape property is related to the video image (or to the window) of the display clone (default: true).
        /// </summary>
        public bool ShapeVideo
        {
            get { return _videoShape; }
            set { _videoShape = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the parent window (form) of the display clone can be moved by dragging the display clone (default: false).
        /// </summary>
        public bool DragEnabled
        {
            get { return _dragEnabled; }
            set { _dragEnabled = value; }
        }

        /// <summary>
        /// Gets or sets the cursor that is used when the display clone is dragged (default: Cursors.SizeAll).
        /// </summary>
        public Cursor DragCursor
        {
            get { return _dragCursor; }
            set { _dragCursor = value; }
        }
    }

    #endregion Clone Properties Class

    // ******************************** Player MF Callback Class

    #region Player MF Callback Class

    // Media Foundation Callback Class
    internal sealed class MFCallback : IMFAsyncCallback
    {
        private Player _basePlayer;

        private delegate void EndOfMediaDelegate();

        private EndOfMediaDelegate CallEndOfMedia;

        public MFCallback(Player player)
        {
            _basePlayer = player;
            CallEndOfMedia = new EndOfMediaDelegate(_basePlayer.AV_EndOfMedia);
        }

        public void Dispose()
        {
            _basePlayer = null;
            CallEndOfMedia = null;
        }

        public HResult GetParameters(out MFASync pdwFlags, out MFAsyncCallbackQueue pdwQueue)
        {
            pdwFlags = MFASync.FastIOProcessingCallback;
            pdwQueue = MFAsyncCallbackQueue.Standard;
            return HResult.S_OK;
        }

        public HResult Invoke(IMFAsyncResult result)
        {
            IMFMediaEvent mediaEvent = null;
            MediaEventType mediaType = MediaEventType.MEUnknown;
            HResult hrStatus;

            try
            {
                _basePlayer.mf_MediaSession.EndGetEvent(result, out mediaEvent);
                mediaEvent.GetType(out mediaType);
                mediaEvent.GetStatus(out hrStatus);

                if (!_basePlayer._fileMode && mediaType == MediaEventType.MEVideoCaptureDeviceRemoved)
                {
                    _basePlayer._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
                    hrStatus = Player.NO_ERROR;
                    mediaType = MediaEventType.MEEndOfPresentation;
                }

                if (hrStatus >= 0)
                {
                    if (mediaType == MediaEventType.MEEndOfPresentation)
                    {
                        Control control = _basePlayer._display;
                        if (control == null)
                        {
                            FormCollection forms = Application.OpenForms;
                            if (forms != null && forms.Count > 0) control = forms[0];
                        }
                        if (control != null)
                        {
                            //control.BeginInvoke(new MethodInvoker(delegate { _basePlayer.AV_EndOfMedia(); }));
                            control.BeginInvoke(CallEndOfMedia);
                        }
                        else
                        {
                            _basePlayer.AV_EndOfMedia();
                        }
                    }
                }
                else _basePlayer._lastError = hrStatus;
            }
            finally
            {
                if (_basePlayer.mf_AwaitCallback)
                {
                    _basePlayer.mf_AwaitCallback = false;
                    _basePlayer.WaitForEvent.Set();
                }

                if (mediaType != MediaEventType.MESessionClosed) _basePlayer.mf_MediaSession.BeginGetEvent(this, null);
                if (mediaEvent != null) Marshal.ReleaseComObject(mediaEvent);
            }

            return 0;
        }
    }

    #endregion Player MF Callback Class

    // ******************************** Hide System Object Members Classes

    #region Hide System Object Members Classes

    /// <summary>
    /// Internal class that is used to hide System.Object members in derived classes.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class HideObjectMembers
    {
        #region Hide Inherited System.Object members

        /// <summary>
        /// Gets the type of the current instance.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType() { return base.GetType(); } // this can't be hidden ???

        /// <summary>
        /// Serves as a hash function for a particular object.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return base.GetHashCode(); }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() { return base.ToString(); }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return base.Equals(obj); }

        #endregion Hide Inherited System.Object members
    }

    /// <summary>
    /// Internal class that is used to hide System.Object members in the from EventArgs derived classes.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class HideObjectEventArgs : EventArgs
    {
        #region Hide Inherited System.Object members

        /// <summary>
        /// Gets the type of the current instance.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType() { return base.GetType(); } // this can't be hidden ???

        /// <summary>
        /// Serves as a hash function for a particular object.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return base.GetHashCode(); }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() { return base.ToString(); }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return base.Equals(obj); }

        #endregion Hide Inherited System.Object members
    }

    #endregion Hide System Object Members Classes

    // ******************************** Player Grouping Classes

    #region Audio Class

    /// <summary>
    /// A class that is used to group together the Audio methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Audio : HideObjectMembers
    {
        #region Fields (Audio Class)

        private Player _base;

        #endregion Fields (Audio Class)

        internal Audio(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets a value indicating whether the playing media contains audio.
        /// </summary>
        public bool Present
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasAudio;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the audio output of the player is enabled (default: true).
        /// </summary>
        public bool Enabled
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._audioEnabled;
            }
            set { _base.AV_SetAudioEnabled(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the audio output of the player is muted (default: false).
        /// </summary>
        public bool Mute
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return !_base._audioEnabled;
            }
            set { _base.AV_SetAudioEnabled(!value); }
        }

        /// <summary>
        /// Gets or sets the active audio track of the playing media. See also: Player.Media.GetAudioTracks.
        /// </summary>
        public int Track
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._audioTrackCurrent;
            }
            set { _base.AV_SetTrack(value, true); }
        }

        /// <summary>
        /// Gets the number of audio channels in the active audio track of the playing media. See also: Player.Audio.DeviceChannelCount.
        /// </summary>
        public int ChannelCount
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (!_base._playing) return 0;
                return _base._mediaChannelCount;
            }
        }

        /// <summary>
        /// Gets the number of output channels of the player's audio output device. See also: Player.Audio.ChannelCount.
        /// </summary>
        public int DeviceChannelCount
        {
            get
            {
                _base._lastError = Player.NO_ERROR;

                if (_base.pm_HasPeakMeter) return _base.pm_PeakMeterChannelCount;
                else return Player.Device_GetChannelCount(_base._audioDevice);
            }
        }

        /// <summary>
        /// Gets or sets the value of each individual audio channel (up to 16 channels), values from 0.0 (mute) to 1.0 (max). See also: Player.Audio.ChannelCount and .DeviceChannelCount.
        /// </summary>
        public float[] ChannelVolumes
        {
            get
            {
                _base._lastError = Player.NO_ERROR;

                float[] volumes = new float[Player.MAX_AUDIO_CHANNELS];
                for (int i = 0; i < Player.MAX_AUDIO_CHANNELS; i++)
                {
                    volumes[i] = _base._audioChannelsVolume[i];
                }
                return volumes;
            }
            set
            {
                _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                if (value != null && value.Length > 0)
                {
                    if (_base._audioChannelsVolumeCopy == null) _base._audioChannelsVolumeCopy = new float[Player.MAX_AUDIO_CHANNELS];

                    int length = value.Length;
                    bool valid = true;
                    float newVolume = 0.0f;

                    for (int i = 0; i < Player.MAX_AUDIO_CHANNELS; i++)
                    {
                        if (i < length)
                        {
                            if (value[i] < 0.0f || value[i] > 1.0f)
                            {
                                valid = false;
                                break;
                            }
                            _base._audioChannelsVolumeCopy[i] = value[i];
                            if (i < 2 && value[i] > newVolume) newVolume = value[i];
                        }
                        else _base._audioChannelsVolumeCopy[i] = _base._audioChannelsVolume[i]; //  0.0f;
                    }

                    if (valid)
                    {
                        _base._lastError = Player.NO_ERROR;

                        float newBalance;
                        if (value[0] >= value[1])
                        {
                            if (value[0] == 0.0f) newBalance = 0.0f;
                            else newBalance = (value[1] / value[0]) - 1;
                        }
                        else
                        {
                            if (value[1] == 0.0f) newBalance = 0.0f;
                            else newBalance = 1 - (value[0] / value[1]);
                        }

                        _base.AV_SetAudioChannels(_base._audioChannelsVolumeCopy, newVolume, newBalance);

                        //if (!_base._audioEnabled)
                        //{
                        //    _base._audioEnabled = true;
                        //    if (_base._mediaAudioMuteChanged != null) _base._mediaAudioMuteChanged(_base, EventArgs.Empty);
                        //}
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the audio volume of the player, values from 0.0 (mute) to 1.0 (max) (default: 1.0).
        /// </summary>
        public float Volume
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._audioVolume;
            }
            set { _base.AV_SetAudioVolume(value, true, true); }
        }

        /// <summary>
        /// Gets or sets the audio balance of the player, values from -1.0 (left) to 1.0 (right) (default: 0.0).
        /// </summary>
        public float Balance
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._audioBalance;
            }
            set { _base.AV_SetAudioBalance(value, true, true); }
        }

        /// <summary>
        /// Gets or sets the audio volume of the player's audio output device, value 0.0 (mute) to 1.0 (max).
        /// </summary>
        public float MasterVolume
        {
            get
            {
                float volume = Player.AudioDevice_MasterVolume(_base._audioDevice, 0, false);
                if (volume == -1)
                {
                    volume = 0;
                    _base._lastError = HResult.E_FAIL; // device not ready
                }
                else _base._lastError = Player.NO_ERROR;
                return volume;
            }
            set
            {
                if (value < Player.AUDIO_VOLUME_MINIMUM || value > Player.AUDIO_VOLUME_MAXIMUM)
                {
                    _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
                else
                {
                    float volume = Player.AudioDevice_MasterVolume(_base._audioDevice, value, true);
                    _base._lastError = volume == -1 ? HResult.E_FAIL : Player.NO_ERROR;
                }
            }
        }

        /// <summary>
        /// Gets or sets the audio output device used by the player (default: null). The default audio output device of the system is indicated by null. See also: Player.Audio.GetDevices and .GetDefaultDevice.
        /// </summary>
        public AudioDevice Device
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._audioDevice;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                bool setDevice = false;

                if (value == null)
                {
                    if (_base._audioDevice != null)
                    {
                        _base._audioDevice = null;
                        setDevice = true;
                    }
                }
                else if (_base._audioDevice == null || value._id != _base._audioDevice._id)
                {
                    AudioDevice[] devices = GetDevices();
                    for (int i = 0; i < devices.Length; i++)
                    {
                        if (value._id == devices[i]._id)
                        {
                            _base._audioDevice = devices[i];
                            setDevice = true;
                            break;
                        }
                    }
                    if (!setDevice) _base._lastError = HResult.ERROR_SYSTEM_DEVICE_NOT_FOUND;
                }

                if (setDevice)
                {
                    if (_base._hasAudio) // = also playing
                    {
                        _base.AV_UpdateTopology();
                    }

                    if (_base._lastError == Player.NO_ERROR)
                    {
                        if (_base.pm_HasPeakMeter)
                        {
                            _base.StartSystemDevicesChangedHandlerCheck();
                            _base.PeakMeter_Open(_base._audioDevice, true);
                        }
                        else
                        {
                            if (_base._audioDevice == null) _base.StopSystemDevicesChangedHandlerCheck();
                            else _base.StartSystemDevicesChangedHandlerCheck();
                        }
                        if (_base._mediaAudioDeviceChanged != null) _base._mediaAudioDeviceChanged(_base, EventArgs.Empty);
                    }
                    else
                    {
                        _base.AV_CloseSession(false, true, StopReason.Error);
                    }
                }
            }
        }

        ///// <summary>
        ///// Gets the number of enabled audio output devices from the system.
        ///// </summary>
        //public int DeviceCount
        //{
        //    get
        //    {
        //        _base._lastError = Player.NO_ERROR;
        //        uint count = 0;

        //        IMMDeviceCollection deviceCollection;
        //        IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
        //        deviceEnumerator.EnumAudioEndpoints(EDataFlow.eRender, (uint)DeviceState.Active, out deviceCollection);
        //        Marshal.ReleaseComObject(deviceEnumerator);
        //        if (deviceCollection != null)
        //        {
        //            deviceCollection.GetCount(out count);
        //            Marshal.ReleaseComObject(deviceCollection);
        //        }
        //        return (int)count;
        //    }
        //}

        /// <summary>
        /// Returns a list of the system's enabled audio output devices. Returns null if no enabled audio output devices are present.
        /// </summary>
        public AudioDevice[] GetDevices()
        {
            IMMDeviceCollection deviceCollection;
            IMMDevice device;
            AudioDevice[] audioDevices = null;

            _base._lastError = HResult.MF_E_NO_AUDIO_PLAYBACK_DEVICE;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.EnumAudioEndpoints(EDataFlow.eRender, (uint)DeviceState.Active, out deviceCollection);
            Marshal.ReleaseComObject(deviceEnumerator);

            if (deviceCollection != null)
            {
                uint count;
                deviceCollection.GetCount(out count);

                if (count > 0)
                {
                    audioDevices = new AudioDevice[count];
                    for (int i = 0; i < count; i++)
                    {
                        audioDevices[i] = new AudioDevice();

                        deviceCollection.Item((uint)i, out device);
                        Player.GetDeviceInfo(device, audioDevices[i]);

                        Marshal.ReleaseComObject(device);
                    }
                    _base._lastError = Player.NO_ERROR;
                }
                Marshal.ReleaseComObject(deviceCollection);
            }
            return audioDevices;
        }

        /// <summary>
        /// Returns the system's default audio output device. Returns null if no default audio output device is present.
        /// </summary>
        public AudioDevice GetDefaultDevice()
        {
            IMMDevice device;
            AudioDevice audioDevice = null;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out device);
            Marshal.ReleaseComObject(deviceEnumerator);

            if (device != null)
            {
                audioDevice = new AudioDevice();
                Player.GetDeviceInfo(device, audioDevice);

                Marshal.ReleaseComObject(device);
                _base._lastError = Player.NO_ERROR;
            }
            else
            {
                _base._lastError = HResult.MF_E_NO_AUDIO_PLAYBACK_DEVICE;
            }

            return audioDevice;
        }

        ///// <summary>
        ///// Returns the index of the system's default audio output device in a list of audio output devices. Returns -1 if the device is not in the list.
        ///// </summary>
        ///// <param name="devices">The list of audio output devices in which the system's default audio output device is to be found.</param>
        //public int GetDefaultDeviceIndex(AudioDevice[] devices)
        //{
        //    int index = -1;

        //    if (devices != null && devices.Length > 0)
        //    {
        //        AudioDevice device = GetDefaultDevice();
        //        if (device != null)
        //        {
        //            for (int i = 0; i < devices.Length; i++)
        //            {
        //                if (devices[i].Id == device.Id)
        //                {
        //                    index = i;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return index;
        //}

        //private void GetAudioDeviceInfo(IMMDevice device, AudioDevice audioDevice)
        //{
        //    //if (device != null && audioDevice != null)
        //    {
        //        device.GetId(out audioDevice._id);

        //        IPropertyStore store;
        //        device.OpenPropertyStore((uint)STGM.STGM_READ, out store);

        //        if (store != null)
        //        {
        //            PropVariant property = new PropVariant();

        //            store.GetValue(Player.PKEY_Device_Description, property);
        //            audioDevice._name = (string)property;

        //            store.GetValue(Player.PKEY_DeviceInterface_FriendlyName, property);
        //            audioDevice._adapter = (string)property;

        //            property.Dispose();
        //            Marshal.ReleaseComObject(store);
        //        }
        //    }
        //}
    }

    #endregion Audio Class

    #region Audio Input Class

    /// <summary>
    /// A class that is used to group together the Audio Input methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class AudioInput : HideObjectMembers
    {
        #region Fields (Audio Input Class)

        private Player _base;

        #endregion Fields (Audio Input Class)

        internal AudioInput(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Returns a list of the system's enabled audio input devices. Returns null if no enabled audio input devices are present.
        /// </summary>
        public AudioInputDevice[] GetDevices()
        {
            IMMDeviceCollection deviceCollection;
            IMMDevice device;
            AudioInputDevice[] audioDevices = null;

            _base._lastError = HResult.MF_E_NO_AUDIO_RECORDING_DEVICE;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.EnumAudioEndpoints(EDataFlow.eCapture, (uint)DeviceState.Active, out deviceCollection);
            Marshal.ReleaseComObject(deviceEnumerator);

            if (deviceCollection != null)
            {
                uint count;
                deviceCollection.GetCount(out count);

                if (count > 0)
                {
                    audioDevices = new AudioInputDevice[count];
                    for (int i = 0; i < count; i++)
                    {
                        audioDevices[i] = new AudioInputDevice();

                        deviceCollection.Item((uint)i, out device);
                        Player.GetDeviceInfo(device, audioDevices[i]);

                        Marshal.ReleaseComObject(device);
                    }
                    _base._lastError = Player.NO_ERROR;
                }
                Marshal.ReleaseComObject(deviceCollection);
            }
            return audioDevices;
        }

        /// <summary>
        /// Returns the system's default audio input device. Returns null if no default audio input device is present.
        /// </summary>
        public AudioInputDevice GetDefaultDevice()
        {
            IMMDevice device;
            AudioInputDevice audioDevice = null;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out device);
            Marshal.ReleaseComObject(deviceEnumerator);

            if (device != null)
            {
                audioDevice = new AudioInputDevice();
                Player.GetDeviceInfo(device, audioDevice);

                Marshal.ReleaseComObject(device);
                _base._lastError = Player.NO_ERROR;
            }
            else
            {
                _base._lastError = HResult.MF_E_NO_AUDIO_RECORDING_DEVICE;
            }

            return audioDevice;
        }

        /// <summary>
        /// Gets a value indicating whether an audio input device is playing (on its own or with a webcam device - includes paused audio input). Use the Player.Play method play an audio device.
        /// </summary>
        public bool Playing
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (_base._webcamMode) return _base._webcamAggregated;
                return _base._micMode;
            }
        }

        /// <summary>
        /// Gets or sets the audio input device that is playing (on its own or with a webcam device). Use the Player.Play method to play an audio device.
        /// </summary>
        public AudioInputDevice Device
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._micDevice;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                if (_base._webcamMode || _base._micMode)
                {
                    _base._lastError = Player.NO_ERROR;
                    if ((value == null && _base._micDevice != null) ||
                        (value != null && _base._micDevice == null) ||
                        _base._micDevice._id != value._id)
                    {
                        _base._micDevice = value;
                        _base.AV_UpdateTopology();
                        if (_base._mediaAudioInputDeviceChanged != null) _base._mediaAudioInputDeviceChanged(_base, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Updates or restores the audio playback of the playing input device.
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            if (_base._micMode)
            {
                _base._lastError = Player.NO_ERROR;
                _base.AV_UpdateTopology();
            }
            else _base._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
            return (int)_base._lastError;
        }
    }

    #endregion Audio Input Class

    #region Video Class

    /// <summary>
    /// A class that is used to group together the Video methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Video : HideObjectMembers
    {
        #region Fields (Video Class)

        private Player _base;
        private bool _zoomBusy;
        private bool _boundsBusy;

        #endregion Fields (Video Class)

        internal Video(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets a value indicating whether the playing media contains video.
        /// </summary>
        public bool Present
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasVideo;
            }
        }

        ///// <summary>
        ///// Gets or sets a value indicating whether the display of video of the player is enabled (default: true).
        ///// </summary>
        //public bool Enabled
        //{
        //    get
        //    {
        //        _base._lastError = Player.NO_ERROR;
        //        return _base._videoEnabled;
        //    }
        //    set { _base.AV_SetVideoEnabled(value); }
        //}

        /// <summary>
        /// Gets or sets the active video track of the playing media. See also: Player.Media.GetVideoTracks.
        /// </summary>
        public int Track
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._videoTrackCurrent;
            }
            set { _base.AV_SetTrack(value, false); }
        }

        /// <summary>
        /// Gets or sets the size and location of the video image on the display of the player. When set, the display mode of the player (Player.Display.Mode) is set to Displaymode.Manual.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (!_base._hasVideoBounds && _base._hasVideo)
                {
                    _base.AV_GetDisplayModeSize(_base._displayMode);
                }
                return _base._videoBounds;
            }
            set
            {
                if (_base._hasVideo)
                {
                    if (!_boundsBusy && (value.Width >= 8) && (value.Height >= 8))
                    {
                        _boundsBusy = true;
                        _base._lastError = Player.NO_ERROR;

                        _base._videoBounds = value;
                        _base._videoBoundsClip = Rectangle.Intersect(_base._display.DisplayRectangle, _base._videoBounds);
                        _base._hasVideoBounds = true;

                        if (_base._displayMode == DisplayMode.Manual) _base._display.Refresh();
                        else _base.Display.Mode = DisplayMode.Manual;

                        // TODO - image gets stuck when same size as display - is it _videoDisplay or MF
                        if (_base._videoBounds.X <= 0 || _base._videoBounds.Y <= 0)
                        {
                            _base._videoDisplay.Width--;
                            _base._videoDisplay.Width++;
                        }

                        if (_base._hasDisplayShape) _base.AV_UpdateDisplayShape();

                        if (_base._mediaVideoBoundsChanged != null) _base._mediaVideoBoundsChanged(_base, EventArgs.Empty);

                        _boundsBusy = false;
                    }
                    else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
                else _base._lastError = HResult.S_FALSE;
            }
        }

        /// <summary>
        /// Enlarges or reduces the size of the video image at the center location of the display of the player. The display mode of the player (Player.Display.Mode) is set to Displaymode.Manual.
        /// </summary>
        /// <param name="factor">The factor by which the video image is to be zoomed.</param>
        public int Zoom(double factor)
        {
            if (_base._hasVideo) return Zoom(factor, _base._display.Width / 2, _base._display.Height / 2);
            _base._lastError = HResult.S_FALSE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Enlarges or reduces the size of the player's video image at the specified display location. The display mode of the player (Player.Display.Mode) is set to Displaymode.Manual.
        /// </summary>
        /// <param name="factor">The factor by which the video image is to be zoomed.</param>
        /// <param name="center">The center location of the zoom on the display of the player.</param>
        public int Zoom(double factor, Point center)
        {
            return (Zoom(factor, center.X, center.Y));
        }

        /// <summary>
        /// Enlarges or reduces the size of the player's video image at the specified display location. The display mode of the player (Player.Display.Mode) is set to Displaymode.Manual.
        /// </summary>
        /// <param name="factor">The factor by which the video image is to be zoomed.</param>
        /// <param name="xCenter">The horizontal (x) center location of the zoom on the display of the player.</param>
        /// <param name="yCenter">The vertical (y) center location of the zoom on the display of the player.</param>
        public int Zoom(double factor, int xCenter, int yCenter)
        {
            if (_base._hasVideo && factor > 0)
            {
                _base._lastError = Player.NO_ERROR;

                if (factor != 1)
                {
                    if (_zoomBusy) return (int)_base._lastError;

                    _zoomBusy = true;
                    double width = 0;
                    double height = 0;
                    Rectangle r = new Rectangle(_base._videoBounds.Location, _base._videoBounds.Size);

                    if (r.Width < r.Height)
                    {
                        r.X = (int)Math.Round(-factor * (xCenter - r.X)) + xCenter;
                        width = r.Width * factor;

                        if (width >= 10)
                        {
                            r.Y = (int)Math.Round(-(width / r.Width) * (yCenter - r.Y)) + yCenter;
                            height = (width / r.Width) * r.Height;
                        }
                    }
                    else
                    {
                        r.Y = (int)Math.Round(-factor * (yCenter - r.Y)) + yCenter;
                        height = r.Height * factor;

                        if (height >= 10)
                        {
                            r.X = (int)Math.Round(-(height / r.Height) * (xCenter - r.X)) + xCenter;
                            width = (height / r.Height) * r.Width;
                        }
                    }

                    r.Width = (int)Math.Round(width);
                    r.Height = (int)Math.Round(height);
                    Bounds = r;

                    _zoomBusy = false;
                }
            }
            else _base._lastError = HResult.S_FALSE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Enlarges the specified part of the display of the player to the entire display of the player. The display mode of the player (Player.Display.Mode) is set to Displaymode.Manual.
        /// </summary>
        /// <param name="area">The area of the display of the player to enlarge.</param>
        public int Zoom(Rectangle area)
        {
            if (_base._hasVideo)
            {
                if ((area.X >= 0 && area.X <= (_base._display.Width - 8)) && (area.Y >= 0 && area.Y <= (_base._display.Height - 8)) && (area.X + area.Width <= _base._display.Width) && (area.Y + area.Height <= _base._display.Height))
                {
                    double factorX = (double)_base._display.Width / area.Width;
                    double factorY = (double)_base._display.Height / area.Height;

                    Bounds = new Rectangle(
                        (int)((_base._videoBounds.X - area.X) * factorX),
                        (int)((_base._videoBounds.Y - area.Y) * factorY),
                        (int)(_base._videoBounds.Width * factorX),
                        (int)(_base._videoBounds.Height * factorY));
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.S_FALSE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Moves the location of the video image on the display of the player by the given amount of pixels. The display mode of the player (Player.Display.Mode) is set to Displaymode.Manual.
        /// </summary>
        /// <param name="horizontal">The amount of pixels to move the video image in the horizontal (x) direction.</param>
        /// <param name="vertical">The amount of pixels to move the video image in the vertical (y) direction.</param>
        public int Move(int horizontal, int vertical)
        {
            if (_base._hasVideo)
            {
                Bounds = new Rectangle(_base._videoBounds.X + horizontal, _base._videoBounds.Y + vertical, _base._videoBounds.Width, _base._videoBounds.Height);
            }
            else _base._lastError = HResult.S_FALSE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Enlarges or reduces the size of the player's video image by the given amount of pixels at the center of the video image. The display mode of the player (Player.Display.Mode) is set to Displaymode.Manual.
        /// </summary>
        /// <param name="horizontal">The amount of pixels to stretch the video image in the horizontal (x) direction.</param>
        /// <param name="vertical">The amount of pixels to stretch the video image in the vertical (y) direction.</param>
        public int Stretch(int horizontal, int vertical)
        {
            if (_base._hasVideo)
            {
                Bounds = new Rectangle(_base._videoBounds.X - (horizontal / 2), _base._videoBounds.Y - (vertical / 2), _base._videoBounds.Width + horizontal, _base._videoBounds.Height + vertical);
            }
            else _base._lastError = HResult.S_FALSE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Gets or sets a value indicating the brightness of the player's video image. Values from -1.0 to 1.0 (default: 0.0).
        /// </summary>
        public double Brightness
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._brightness;
            }
            set
            {
                _base.AV_SetBrightness(value, true);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the contrast of the player's video image. Values from -1.0 to 1.0 (default: 0.0).
        /// </summary>
        public double Contrast
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._contrast;
            }
            set
            {
                _base.AV_SetContrast(value, true);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the hue of the player's video image. Values from -1.0 to 1.0 (default: 0.0).
        /// </summary>
        public double Hue
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hue;
            }
            set
            {
                _base.AV_SetHue(value, true);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the saturation of the player's video image. Values from -1.0 to 1.0 (default: 0.0).
        /// </summary>
        public double Saturation
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._saturation;
            }
            set
            {
                _base.AV_SetSaturation(value, true);
            }
        }

        /// <summary>
        /// Returns a copy of the currently displayed video image of the player (without display overlay). See also: Player.ScreenCopy.
        /// </summary>
        public Image ToImage()
        {
            return _base.AV_DisplayCopy(true, false);
        }

        /// <summary>
        /// Copies the currently displayed video image of the player (without display overlay) to the system's clipboard. See also: Player.ScreenCopy.
        /// </summary>
        public int ToClipboard()
        {
            Image theImage = _base.AV_DisplayCopy(true, false);
            if (_base._lastError == Player.NO_ERROR)
            {
                try { Clipboard.SetImage(theImage); }
                catch (Exception e)
                {
                    _base._lastError = (HResult)Marshal.GetHRForException(e);
                }
                theImage.Dispose();
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Saves a copy of the currently displayed video image of the player (without display overlay) to the specified file. See also: Player.ScreenCopy.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        public int ToFile(string fileName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if ((fileName != null) && (fileName.Length > 3))
            {
                Image theImage = _base.AV_DisplayCopy(true, false);
                if (_base._lastError == Player.NO_ERROR)
                {
                    try { theImage.Save(fileName, imageFormat); }
                    catch (Exception e)
                    {
                        _base._lastError = (HResult)Marshal.GetHRForException(e);
                    }
                    theImage.Dispose();
                }
            }
            else
            {
                _base._lastError = HResult.ERROR_INVALID_NAME;
            }
            return (int)_base._lastError;
        }
    }

    #endregion Video Class

    #region Webcam Class

    /// <summary>
    /// A class that is used to group together the Webcam methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Webcam : HideObjectMembers
    {
        #region Fields (Webcam Class)

        private Player _base;

        #endregion Fields (Webcam Class)

        #region Main / Playing / Device / Format / GetDevices / Update

        internal Webcam(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets a value indicating whether a webcam is playing (includes paused webcam). Use the Player.Play method to play a webcam.
        /// </summary>
        public bool Playing
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._webcamMode;
            }
        }

        /// <summary>
        /// Gets the playing webcam device. Use the Player.Play method to play a webcam.
        /// </summary>
        public WebcamDevice Device
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._webcamDevice;
            }
        }

        /// <summary>
        /// Gets or sets the audio input device of the playing webcam. See also: Player.AudioInput.GetDevices.
        /// </summary>
        public AudioInputDevice AudioInput
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (_base._webcamMode) return _base._micDevice;
                return null;
            }
            set
            {
                if (_base._webcamMode)
                {
                    _base._lastError = Player.NO_ERROR;
                    if ((value == null && _base._micDevice != null) ||
                        (value != null && _base._micDevice == null) ||
                        _base._micDevice._id != value._id)
                    {
                        _base._micDevice = value;
                        _base.AV_UpdateTopology();
                        if (_base._mediaAudioInputDeviceChanged != null) _base._mediaAudioInputDeviceChanged(_base, EventArgs.Empty);
                    }
                }
                else _base._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
            }
        }

        /// <summary>
        /// Gets or sets the video output format of the playing webcam.
        /// </summary>
        public WebcamFormat Format
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._webcamFormat;
            }
            set
            {
                if (value == null) _base._lastError = HResult.E_INVALIDARG;
                else
                {
                    if (_base._webcamMode)
                    {
                        _base._lastError = Player.NO_ERROR;
                        if ((value == null && _base._webcamFormat != null) ||
                        (value != null && _base._webcamFormat == null) ||
                        (value._typeIndex != _base._webcamFormat._typeIndex))
                        {
                            _base._webcamFormat = value;
                            _base.AV_UpdateTopology();
                            if (_base._mediaWebcamFormatChanged != null) _base._mediaWebcamFormatChanged(_base, EventArgs.Empty);
                        }
                    }
                    else _base._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
                }
            }
        }

        /// <summary>
        /// Returns a list of the system's enabled webcam devices. Returns null if no enabled webcam devices are present. Use the Player.Play method to play a webcam.
        /// </summary>
        public WebcamDevice[] GetDevices()
        {
            WebcamDevice[] devices = null;
            IMFAttributes attributes;
            IMFActivate[] webcams;
            int webcamCount;
            int length;
            HResult result;

            MFExtern.MFCreateAttributes(out attributes, 1);
            attributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
            if (Environment.Version.Major < 4) // fix for NET 2.0 and 3.5 - only 1 device (? SizeParamIndex error)
            {
                result = MFExtern.MFEnumDeviceSourcesEx(attributes, out webcams, out webcamCount);
                if (webcams != null) webcamCount = webcams.Length;
            }
            else result = MFExtern.MFEnumDeviceSources(attributes, out webcams, out webcamCount);

            if (result == Player.NO_ERROR && webcams != null)
            {
                devices = new WebcamDevice[webcamCount];

                for (int i = 0; i < webcamCount; i++)
                {
                    devices[i] = new WebcamDevice();

#pragma warning disable IDE0059 // Unnecessary assignment of a value
                    webcams[i].GetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME, _base._textBuffer1, _base._textBuffer1.Capacity, out length);
                    devices[i]._name = _base._textBuffer1.ToString();
                    webcams[i].GetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, _base._textBuffer1, _base._textBuffer1.Capacity, out length);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                    devices[i]._id = _base._textBuffer1.ToString();

                    Marshal.ReleaseComObject(webcams[i]);
                }
            }
            Marshal.ReleaseComObject(attributes);

            _base._lastError = result;
            return devices;
        }

        /// <summary>
        /// Updates or restores the audio and video playback of the playing webcam.
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            if (_base._webcamMode)
            {
                _base._lastError = Player.NO_ERROR;
                _base.AV_UpdateTopology();
            }
            else _base._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
            return (int)_base._lastError;
        }

        #endregion Main / Playing / Device / Format / GetDevices / Update

        #region Private Video Control/ProcAmp Methods

        internal WebcamProperty GetControlProperties(CameraControlProperty property)
        {
            HResult result = HResult.ERROR_NOT_READY;

            WebcamProperty settings = new WebcamProperty();
            //settings._name = property.ToString();

            if (_base._webcamMode)
            {
                CameraControlFlags flags;

                IAMCameraControl control = _base.mf_MediaSource as IAMCameraControl;
                result = control.GetRange(property, out settings._min, out settings._max, out settings._step, out settings._default, out flags);

                if (result == Player.NO_ERROR)
                {
                    settings._supported = (flags & CameraControlFlags.Manual) != 0;
                    settings._autoSupport = (flags & CameraControlFlags.Auto) != 0;

                    control.Get(property, out settings._current, out flags);
                    settings._auto = (flags & CameraControlFlags.Auto) != 0;
                }
            }
            _base._lastError = result;
            return settings;
        }

        internal void SetControlProperties(CameraControlProperty property, WebcamProperty value)
        {
            HResult result = HResult.ERROR_NOT_READY;
            if (_base._webcamMode)
            {
                WebcamProperty settings = GetControlProperties(property);
                if (!settings._supported) result = HResult.MF_E_NOT_AVAILABLE;
                else if (value._auto && settings._auto) result = Player.NO_ERROR;
                else if (!value._auto && (value._current < settings._min || value._current > settings._max)) result = HResult.MF_E_OUT_OF_RANGE;

                if (result == HResult.ERROR_NOT_READY)
                {
                    try
                    {
                        result = ((IAMCameraControl)_base.mf_MediaSource).Set(property, value._current, value._auto ? CameraControlFlags.Auto : CameraControlFlags.Manual);
                    }
                    catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
                }
            }
            _base._lastError = result;
        }

        internal WebcamProperty GetProcAmpProperties(VideoProcAmpProperty property)
        {
            HResult result = HResult.ERROR_NOT_READY;

            WebcamProperty settings = new WebcamProperty();
            //settings._name = property.ToString();

            if (_base._webcamMode)
            {
                VideoProcAmpFlags flags;

                IAMVideoProcAmp control = _base.mf_MediaSource as IAMVideoProcAmp;
                result = control.GetRange(property, out settings._min, out settings._max, out settings._step, out settings._default, out flags);

                if (result == Player.NO_ERROR)
                {
                    settings._supported = (flags & VideoProcAmpFlags.Manual) != 0;
                    settings._autoSupport = (flags & VideoProcAmpFlags.Auto) != 0;

                    control.Get(property, out settings._current, out flags);
                    settings._auto = (flags & VideoProcAmpFlags.Auto) != 0;
                }
            }
            _base._lastError = result;
            return settings;
        }

        internal void SetProcAmpProperties(VideoProcAmpProperty property, WebcamProperty value)
        {
            HResult result = HResult.ERROR_NOT_READY;
            if (_base._webcamMode)
            {
                WebcamProperty settings = GetProcAmpProperties(property);
                if (!settings._supported) result = HResult.MF_E_NOT_AVAILABLE;
                else if (value._auto && settings._auto) result = Player.NO_ERROR;
                else if (!value._auto && (value._current < settings._min || value._current > settings._max)) result = HResult.MF_E_OUT_OF_RANGE;

                if (result == HResult.ERROR_NOT_READY)
                {
                    try
                    {
                        result = (HResult)((IAMVideoProcAmp)_base.mf_MediaSource).Set(property, value._current, value._auto ? VideoProcAmpFlags.Auto : VideoProcAmpFlags.Manual);
                    }
                    catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
                }
            }
            _base._lastError = result;
        }

        #endregion Private Video Control/ProcAmp Methods

        #region Video Control Properties

        /// <summary>
        /// Gets or sets the exposure property (if supported) of the active webcam (usage: get, examine, change, set). Values are in log base 2 seconds, for values less than zero the exposure time is 1/2^n seconds (eg. -3 = 1/8), and for values zero or above the exposure time is 2^n seconds (eg. 0 = 1 and 2 = 4).
        /// </summary>
        public WebcamProperty Exposure
        {
            get { return GetControlProperties(CameraControlProperty.Exposure); }
            set { SetControlProperties(CameraControlProperty.Exposure, value); }
        }

        /// <summary>
        /// Gets or sets the flash property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty Flash
        {
            get { return GetControlProperties(CameraControlProperty.Flash); }
            set { SetControlProperties(CameraControlProperty.Flash, value); }
        }

        /// <summary>
        /// Gets or sets the focus property (if supported) of the active webcam (usage: get, examine, change, set). Values represent the distance to the optimally focused target, in millimeters.
        /// </summary>
        public WebcamProperty Focus
        {
            get { return GetControlProperties(CameraControlProperty.Focus); }
            set { SetControlProperties(CameraControlProperty.Focus, value); }
        }

        /// <summary>
        /// Gets or sets the iris property (if supported) of the active webcam (usage: get, examine, change, set). Values are in units of f-stop * 10.
        /// </summary>
        public WebcamProperty Iris
        {
            get { return GetControlProperties(CameraControlProperty.Iris); }
            set { SetControlProperties(CameraControlProperty.Iris, value); }
        }

        /// <summary>
        /// Gets or sets the pan property (if supported) of the active webcam (usage: get, examine, change, set). Values are in degrees.
        /// </summary>
        public WebcamProperty Pan
        {
            get { return GetControlProperties(CameraControlProperty.Pan); }
            set { SetControlProperties(CameraControlProperty.Pan, value); }
        }

        /// <summary>
        /// Gets or sets the roll property (if supported) of the active webcam (usage: get, examine, change, set). Values are in degrees.
        /// </summary>
        public WebcamProperty Roll
        {
            get { return GetControlProperties(CameraControlProperty.Roll); }
            set { SetControlProperties(CameraControlProperty.Roll, value); }
        }

        /// <summary>
        /// Gets or sets the tilt property (if supported) of the active webcam (usage: get, examine, change, set). Values are in degrees.
        /// </summary>
        public WebcamProperty Tilt
        {
            get { return GetControlProperties(CameraControlProperty.Tilt); }
            set { SetControlProperties(CameraControlProperty.Tilt, value); }
        }

        /// <summary>
        /// Gets or sets the zoom property (if supported) of the active webcam (usage: get, examine, change, set). Values are in millimeters.
        /// </summary>
        public WebcamProperty Zoom
        {
            get { return GetControlProperties(CameraControlProperty.Zoom); }
            set { SetControlProperties(CameraControlProperty.Zoom, value); }
        }

        #endregion Video Control Properties

        #region Video ProcAmp Properties

        /// <summary>
        /// Gets or sets the backlight compensation property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty Backlight
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.BacklightCompensation); }
            set { SetProcAmpProperties(VideoProcAmpProperty.BacklightCompensation, value); }
        }

        /// <summary>
        /// Gets or sets the brightness property (if supported) of the active webcam (usage: get, examine, change, set). See also: Player.Video.Brightness.
        /// </summary>
        public WebcamProperty Brightness
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Brightness); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Brightness, value); }
        }

        /// <summary>
        /// Gets or sets the color enable property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty ColorEnable
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.ColorEnable); }
            set { SetProcAmpProperties(VideoProcAmpProperty.ColorEnable, value); }
        }

        /// <summary>
        /// Gets or sets the contrast property (if supported) of the active webcam (usage: get, examine, change, set). See also: Player.Video.Contrast.
        /// </summary>
        public WebcamProperty Contrast
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Contrast); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Contrast, value); }
        }

        /// <summary>
        /// Gets or sets the gain property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty Gain
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Gain); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Gain, value); }
        }

        /// <summary>
        /// Gets or sets the gamma property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty Gamma
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Gamma); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Gamma, value); }
        }

        /// <summary>
        /// Gets or sets the hue property (if supported) of the active webcam (usage: get, examine, change, set). See also: Player.Video.Hue.
        /// </summary>
        public WebcamProperty Hue
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Hue); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Hue, value); }
        }

        /// <summary>
        /// Gets or sets the saturation property (if supported) of the active webcam (usage: get, examine, change, set). See also: Player.Video.Saturation.
        /// </summary>
        public WebcamProperty Saturation
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Saturation); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Saturation, value); }
        }

        /// <summary>
        /// Gets or sets the sharpness property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty Sharpness
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Sharpness); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Sharpness, value); }
        }

        /// <summary>
        /// Gets or sets the whiteBalance property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty WhiteBalance
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.WhiteBalance); }
            set { SetProcAmpProperties(VideoProcAmpProperty.WhiteBalance, value); }
        }

        #endregion Video ProcAmp Properties

        #region Private Video Output Format Methods

        private WebcamFormat[] GetWebcamFormats(string webcamId, bool filter, bool exact, int minWidth, int minHeight, float minFrameRate)
        {
            IMFMediaSource source;
            List<WebcamFormat> list = null;

            HResult result = GetMediaSource(webcamId, out source);
            if (result == Player.NO_ERROR)
            {
                IMFSourceReader reader;

                result = MFExtern.MFCreateSourceReaderFromMediaSource(source, null, out reader);
                if (result == Player.NO_ERROR)
                {
                    HResult readResult = Player.NO_ERROR;
                    IMFMediaType type;

                    int streamIndex = 0;
                    int typeIndex = 0;

                    int num, denum;
                    int width, height;
                    float frameRate = 0;
                    bool match;

                    list = new List<WebcamFormat>(250);

                    while (readResult == Player.NO_ERROR)
                    {
                        readResult = reader.GetNativeMediaType(streamIndex, typeIndex, out type);
                        if (readResult == Player.NO_ERROR)
                        {
                            MFExtern.MFGetAttributeRatio(type, MFAttributesClsid.MF_MT_FRAME_RATE, out num, out denum);
                            if (denum > 0) frameRate = (float)num / denum;
                            MFExtern.MFGetAttributeRatio(type, MFAttributesClsid.MF_MT_FRAME_SIZE, out width, out height);

                            match = true;
                            if (filter)
                            {
                                if (exact)
                                {
                                    if ((minWidth != -1 && width != minWidth) || (minHeight != -1 && height != minHeight) || (minFrameRate != -1 && frameRate != minFrameRate)) match = false;
                                }
                                else if ((minWidth != -1 && width < minWidth) || (minHeight != -1 && height < minHeight) || (minFrameRate != -1 && frameRate < minFrameRate)) match = false;
                            }

                            if (match && !FormatExists(list, width, height, frameRate))
                            {
                                list.Add(new WebcamFormat(streamIndex, typeIndex, width, height, frameRate));
                            }

                            typeIndex++;
                            Marshal.ReleaseComObject(type);
                        }
                        // read formats of 1 track (stream) only - can't switch tracks (?)
                        //else if (readResult == HResult.MF_E_NO_MORE_TYPES)
                        //{
                        //    readResult = Player.NO_ERROR;
                        //    streamIndex++;
                        //    typeIndex = 0;
                        //}
                    }
                    if (reader != null) Marshal.ReleaseComObject(reader);
                }
                if (source != null) Marshal.ReleaseComObject(source);
            }

            _base._lastError = result;
            return (list == null || list.Count == 0) ? null : list.ToArray();
        }

        private bool FormatExists(List<WebcamFormat> list, int width, int height, float frameRate)
        {
            bool exists = false;
            int length = list.Count;

            for (int i = 0; i < length; i++)
            {
                if (list[i]._width == width && list[i]._height == height && list[i]._frameRate == frameRate)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        private HResult GetMediaSource(string webcamId, out IMFMediaSource source)
        {
            IMFAttributes attributes;

            MFExtern.MFCreateAttributes(out attributes, 2);
            attributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
            attributes.SetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, webcamId);

            HResult result = MFExtern.MFCreateDeviceSource(attributes, out source);
            if ((uint)result == 0xC00D36E6) result = HResult.ERROR_DEVICE_NOT_CONNECTED;

            Marshal.ReleaseComObject(attributes);
            return result;
        }

        internal WebcamFormat GetHighFormat(WebcamDevice webcam, bool photo)
        {
            WebcamFormat format = null;

            WebcamFormat[] formats = GetWebcamFormats(webcam._id, false, false, 0, 0, 0);
            if (formats != null)
            {
                format = formats[0];
                int count = formats.Length;
                int frameRate = photo ? 1 : 15;

                for (int i = 1; i < count; i++)
                {
                    if (formats[i]._width >= format._width &&
                        formats[i]._height >= format._height &&
                        formats[i]._frameRate >= format._frameRate)
                    {
                        format = formats[i];
                    }
                    else if (formats[i]._width > format._width &&
                        formats[i]._height > format._height &&
                        formats[i]._frameRate >= frameRate)
                    {
                        format = formats[i];
                    }
                }
            }
            return format;
        }

        internal WebcamFormat GetLowFormat(WebcamDevice webcam)
        {
            WebcamFormat format = null;

            WebcamFormat[] formats = GetWebcamFormats(webcam._id, false, false, 0, 0, 0);
            if (formats != null)
            {
                format = formats[0];
                int count = formats.Length;

                for (int i = 1; i < count; i++)
                {
                    if (formats[i]._width <= format._width &&
                        formats[i]._height <= format._height &&
                        formats[i]._height >= 100 &&
                        formats[i]._frameRate <= format._frameRate &&
                        formats[i]._frameRate >= 15)
                        format = formats[i];
                }
            }
            return format;
        }

        #endregion Private Video Output Format Methods

        #region Video Output Format Methods

        /// <summary>
        /// Returns the available video output formats of the playing webcam. The formats can be used with the Player.Webcam.Format and Player.Play methods.
        /// </summary>
        public WebcamFormat[] GetFormats()
        {
            if (!_base._webcamMode)
            {
                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                return null;
            }
            return GetWebcamFormats(_base._webcamDevice._id, false, false, 0, 0, 0);
        }

        /// <summary>
        /// Returns the available video output formats of the specified webcam. The formats can be used with the Player.Play methods for webcams.
        /// </summary>
        /// <param name="webcam">The webcam from which the video output formats must be obtained.</param>
        public WebcamFormat[] GetFormats(WebcamDevice webcam)
        {
            return GetWebcamFormats(webcam._id, false, false, 0, 0, 0);
        }

        /// <summary>
        /// Returns the available video output formats of the playing webcam that match the specified values. The formats can be used with the Player.Webcam.Format and Player.Play methods.
        /// </summary>
        /// <param name="exact">A value indicating whether the specified values must exactly match the webcam formats or whether they are minimum values.</param>
        /// <param name="width">The minimum width of the video frames. Use -1 to ignore the value.</param>
        /// <param name="height">The minimum height of the video frames. Use -1 to ignore the value.</param>
        /// <param name="frameRate">The minimum frame rate of the video output format. Use -1 to ignore the value.</param>
        public WebcamFormat[] GetFormats(bool exact, int width, int height, float frameRate)
        {
            if (!_base._webcamMode)
            {
                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                return null;
            }
            return GetWebcamFormats(_base._webcamDevice._id, true, exact, width, height, frameRate);
        }

        /// <summary>
        /// Returns the available video output formats (or null) of the specified webcam that match the specified values. The formats can be used with the Player.Play methods for webcams.
        /// </summary>
        /// <param name="webcam">The webcam from which the video output formats must be obtained.</param>
        /// <param name="exact">A value indicating whether the specified values must exactly match the webcam formats or whether they are minimum values.</param>
        /// <param name="width">The minimum width of the video frames. Use -1 to ignore the value.</param>
        /// <param name="height">The minimum height of the video frames. Use -1 to ignore the value.</param>
        /// <param name="frameRate">The minimum frame rate of the video output format. Use -1 to ignore the value.</param>
        public WebcamFormat[] GetFormats(WebcamDevice webcam, bool exact, int width, int height, float frameRate)
        {
            return GetWebcamFormats(webcam._id, true, exact, width, height, frameRate);
        }

        #endregion Video Output Format Methods
    }

    #endregion Webcam Class

    #region Display Class

    /// <summary>
    /// A class that is used to group together the Display methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Display : HideObjectMembers
    {
        #region Fields (Display Class)

        private Player _base;

        private bool _isDragging;
        private Cursor _oldCursor;
        private Cursor _dragCursor = Cursors.SizeAll;
        private bool _setDragCursor = true;
        private Form _dragForm;
        private Point _oldLocation;

        #endregion Fields (Display Class)

        internal Display(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets the display window (form or control) of the player that is used to display video and overlays.
        /// </summary>
        public Control Window
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._display;
            }
            set { _base.AV_SetDisplay(value, true); }
        }

        /// <summary>
        /// Gets or sets the display mode (size and location) of the video image on the display of the player (default: DisplayMode.ZoomCenter). See also: Player.Video.Bounds.
        /// </summary>
        public DisplayMode Mode
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._displayMode;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;

                if (_base._displayMode != value)
                {
                    _base._displayMode = value;
                    if (value == DisplayMode.Manual)
                    {
                        if (!_base._hasVideoBounds)
                        {
                            _base._videoBounds.X = _base._videoBounds.Y = 0;
                            _base._videoBounds.Size = _base._display.Size;
                            _base._videoBoundsClip = _base._videoBounds;
                            _base._hasVideoBounds = true;
                        }
                    }
                    else _base._hasVideoBounds = false;

                    if (_base._hasDisplay) _base._display.Invalidate();
                    if (_base._hasDisplayShape) _base.AV_UpdateDisplayShape();
                    if (_base.dc_DisplayClonesRunning) _base.DisplayClones_Refresh();
                    if (_base._mediaDisplayModeChanged != null) _base._mediaDisplayModeChanged(_base, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Provides access to the display overlay settings of the player (for example, Player.Display.Overlay.Window).
        /// </summary>
        public Overlay Overlay
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.Overlay;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the fullscreen option of the player is activated (default: false).
        /// </summary>
        public bool FullScreen
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._fullScreen;
            }
            set { _base.AV_SetFullScreen(value); }
        }

        /// <summary>
        /// Gets or sets the fullscreen display mode of the player (default: FullScreenMode.Display).
        /// </summary>
        public FullScreenMode FullScreenMode
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._fullScreenMode;
            }
            set { _base.FullScreenMode = value; }
        }

        /// <summary>
        /// Gets the size and location of the parent window (form) of the display window of the player in its normal window state.
        /// </summary>
        public Rectangle RestoreBounds
        {
            get
            {
                Rectangle r = Rectangle.Empty;

                _base._lastError = Player.NO_ERROR;
                if (_base._fullScreen)
                {
                    r = _base._fsFormBounds;
                }
                else
                {
                    if (_base._hasDisplay)
                    {
                        Form f = _base._display.FindForm();
                        r = f.WindowState == FormWindowState.Normal ? f.Bounds : f.RestoreBounds;
                    }
                    else
                    {
                        _base._lastError = HResult.S_FALSE;
                    }
                }
                return r;
            }
        }

        /// <summary>
        /// Gets or sets the shape of the display of the player. If set and the display window is a form, set its border style to None for best results. See also: Player.Display.GetShape and .SetShape.
        /// </summary>
        public DisplayShape Shape
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._displayShape;
            }
            set
            {
                SetShape(value, _base._hasVideoShape, _base._hasOverlayClipping);
            }
        }

        /// <summary>
        /// Gets the shape of the display of the player (default: DisplayShape.Normal).
        /// </summary>
        /// <param name="shape">A value indicating the shape of the display of the player.</param>
        /// <param name="videoShape">A value indicating whether the shape is related to the video image (or to the display window).</param>
        /// <param name="overlayShape">A value indicating whether the shape is also applied to display overlays.</param>
        public int GetShape(out DisplayShape shape, out bool videoShape, out bool overlayShape)
        {
            _base._lastError = Player.NO_ERROR;

            shape = _base._displayShape;
            videoShape = _base._hasVideoShape;
            overlayShape = _base._hasOverlayClipping;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Sets the shape of the display of the player. If the display window is a form, set its border style to None for best results.
        /// </summary>
        /// <param name="shape">A value indicating the shape of the display of the player.</param>
        public int SetShape(DisplayShape shape)
        {
            return SetShape(shape, _base._hasVideoShape, _base._hasOverlayClipping);
        }

        /// <summary>
        /// Sets the shape of the display of the player. If the display window is a form, set its BorderStyle to None.
        /// </summary>
        /// <param name="shape">A value indicating the shape of the display of the player.</param>
        /// <param name="videoShape">A value indicating whether the shape is related to the video image (or to the display window).</param>
        /// <param name="overlayShape">A value indicating whether the shape should also be applied to display overlays.</param>
        public int SetShape(DisplayShape shape, bool videoShape, bool overlayShape)
        {
            _base._lastError = Player.NO_ERROR;

            if (shape == DisplayShape.Normal)
            {
                _base.AV_RemoveDisplayShape(true);
            }
            else
            {
                if (_base._hasDisplay)
                {
                    if (_base._displayShape != shape || videoShape != _base._hasVideoShape || overlayShape != _base._hasOverlayClipping)
                    {
                        _base._displayShape = shape;
                        _base._hasVideoShape = videoShape;
                        _base._displayShapeCallback = _base.AV_GetShapeCallback(shape);
                        _base.AV_SetOverlayClipping(overlayShape);

                        _base._hasDisplayShape = true;
                        _base.AV_UpdateDisplayShape();

                        if (videoShape)
                        {
                            _base._mediaVideoBoundsChanged += _base.AV_DisplayShapeChanged;
                        }
                        if (_base._mediaDisplayShapeChanged != null) _base._mediaDisplayShapeChanged(_base, EventArgs.Empty);
                    }
                }
                else
                {
                    if (_base._displayShape != DisplayShape.Normal) _base.AV_RemoveDisplayShape(true);
                    _base._lastError = HResult.S_FALSE; // No Display
                }
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Gets or sets the shape of the cursor that is used when the display window is dragged (default: Cursors.SizeAll). See also: Player.Display.DragEnabled.
        /// </summary>
        public Cursor DragCursor
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _dragCursor;
            }
            set
            {
                _dragCursor = value;
                _setDragCursor = value != Cursors.Default;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the parent window (form) of the display can be moved by dragging the display window of the player (default: false).
        /// </summary>
        public bool DragEnabled
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._dragEnabled;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                if (value)
                {
                    if (!_base._dragEnabled)
                    {
                        if (!_base._hasDisplay || _base._display.FindForm() == null)
                        {
                            _base._lastError = HResult.S_FALSE;
                        }
                        else
                        {
                            _base._display.MouseDown += Drag_MouseDown;
                            _base._dragEnabled = true;
                        }
                    }
                }
                else if (_base._dragEnabled)
                {
                    _base._display.MouseDown -= Drag_MouseDown;
                    _base._dragEnabled = false;
                }
            }
        }

        /// <summary>
        /// Drags (when enabled) the parent window (form) of the player's display. Use as the mousedown eventhandler of any control other than the player's display, for example, a display overlay.
        /// </summary>
        public void Drag_MouseDown(object sender, MouseEventArgs e)
        {
            if (_base._dragEnabled && !_isDragging)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (sender != null && _base._hasDisplay && !_base._fullScreen)
                    {
                        _dragForm = _base._display.FindForm();
                        if (_base._hasOverlay)
                        {
                            foreach (Form f in Application.OpenForms)
                            {
                                if (f != _base._overlay && f.Owner == _base._overlay.Owner) f.BringToFront();
                            }
                        }
                        _dragForm.Activate();

                        if (_dragForm.WindowState != FormWindowState.Maximized)
                        {
                            Control control = (Control)sender;

                            _oldLocation = control.PointToScreen(e.Location);

                            control.MouseMove += DragDisplay_MouseMove;
                            control.MouseUp += DragDisplay_MouseUp;

                            if (_setDragCursor)
                            {
                                _oldCursor = control.Cursor;
                                control.Cursor = _dragCursor;
                            }

                            _isDragging = true;
                        }
                        else
                        {
                            _dragForm = null;
                        }
                    }
                }
            }
        }

        private void DragDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point location = ((Control)sender).PointToScreen(e.Location);

                _dragForm.Left += location.X - _oldLocation.X;
                _dragForm.Top += location.Y - _oldLocation.Y;
                _oldLocation = location;
            }
        }

        private void DragDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Control control = (Control)sender;

                control.MouseMove -= DragDisplay_MouseMove;
                control.MouseUp -= DragDisplay_MouseUp;
                _dragForm = null;

                if (_setDragCursor) control.Cursor = _oldCursor;

                _isDragging = false;
            }
        }
    }

    #endregion Display Class

    #region CursorHide Class

    /// <summary>
    /// A class that is used to group together the CursorHide methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class CursorHide : HideObjectMembers
    {
        #region Fields (CursorHide Class)

        private Player _base;

        #endregion Fields (CursorHide Class)

        internal CursorHide(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Adds a form to the list of forms that automatically hide the cursor (mouse pointer) during inactivity when media is playing.
        /// </summary>
        /// <param name="form">The form to add to the list.</param>
        public int Add(Form form)
        {
            _base._lastError = Player.CH_AddItem(form, _base); //, _base._display);
            _base._hasCursorHide = Player.CH_HasItems(_base);
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes a form from the list of forms that automatically hide the cursor.
        /// </summary>
        /// <param name="form">The form to remove from the list.</param>
        public int Remove(Form form)
        {
            _base._lastError = Player.CH_RemoveItem(form, _base); //, _base._display);
            _base._hasCursorHide = Player.CH_HasItems(_base);
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes all forms added by this player from the list of forms that automatically hide the cursor.
        /// </summary>
        public int RemoveAll()
        {
            Player.CH_RemovePlayerItems(_base);
            _base._hasCursorHide = false;
            _base._lastError = Player.NO_ERROR;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Gets or sets a value indicating whether automatic cursor hiding is enabled. This option can be used to temporarily disable the hiding of the cursor. This setting is used by all players in this assembly.
        /// </summary>
        public bool Enabled
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return !Player.ch_Disabled;
            }
            set
            {
                //Player.ch_EventArgs._reason = CursorChangedReason.UserCommand;
                Player.CH_Disabled = !value;
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the number of seconds to wait before the cursor is hidden during inactivity when media is playing. This setting is used by all players in this assembly (default: 3 seconds).
        /// </summary>
        public int Delay
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return Player.ch_Delay;
            }
            set
            {
                if (value < 1) value = 1;
                else if (value > 30) value = 30;
                if (value != Player.ch_Delay)
                {
                    Player.ch_Delay = value;
                    Player.ch_Timer.Interval = value == 1 ? 500 : 1000; // value  * 500;
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Get a value indicating whether the cursor has been hidden by the player.
        /// </summary>
        public bool CursorHidden
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return Player.ch_Hidden;
            }
            set
            {
                if (value) { Player.CH_HideCursor(); }
                else { Player.CH_ShowCursor(); }

                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Shows the cursor if it was hidden by the player.
        /// </summary>
        public int ShowCursor()
        {
            Player.CH_ShowCursor();
            _base._lastError = Player.NO_ERROR;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Hides the cursor if the CursorHide option is enabled.
        /// </summary>
        public int HideCursor()
        {
            Player.CH_HideCursor();
            _base._lastError = Player.NO_ERROR;
            return (int)_base._lastError;
        }
    }

    #endregion CursorHide Class

    #region Overlay Class

    /// <summary>
    /// A class that is used to group together the Display Overlay methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Overlay : HideObjectMembers
    {
        #region Fields (Overlay Class)

        private Player _base;

        #endregion Fields (Overlay Class)

        internal Overlay(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets the display overlay of the player.
        /// </summary>
        public Form Window
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._overlay;
            }
            set { _base.AV_SetOverlay(value); }
        }

        /// <summary>
        /// Gets or sets the display mode (video or display size) of the display overlay of the player (default: OverlayMode.Video).
        /// </summary>
        public OverlayMode Mode
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._overlayMode;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                if (value != _base._overlayMode)
                {
                    _base._overlayMode = value;
                    if (_base._hasDisplay && _base._hasOverlayShown)
                    {
                        _base._display.Invalidate();
                        if (_base._hasOverlayClipping) _base.AV_ClipOverlay();
                    }
                    if (_base._mediaOverlayModeChanged != null) _base._mediaOverlayModeChanged(_base, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the display overlay of the player is always shown (default: false).
        /// </summary>
        public bool Hold
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._overlayHold;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                if (value != _base._overlayHold)
                {
                    _base._overlayHold = value;
                    if (_base._hasOverlay)
                    {
                        if (value)
                        {
                            if (_base._hasDisplay && !_base._hasOverlayShown && _base._display.FindForm().Visible)
                            {
                                _base.AV_ShowOverlay();
                                if (_base.dc_HasDisplayClones) _base.DisplayClones_Start();
                            }
                        }
                        else if (_base._hasOverlayShown && (!_base._playing))
                        {
                            _base.AV_HideOverlay();
                            if (_base.dc_HasDisplayClones && !_base._playing) _base.DisplayClones_Stop(false);
                        }
                    }
                    if (_base._mediaOverlayHoldChanged != null) _base._mediaOverlayHoldChanged(_base, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the display overlay of the player can be activated for input and selection (default: false).
        /// </summary>
        public bool CanFocus
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._overlayCanFocus;
            }
            set
            {
                if (value != _base._overlayCanFocus) _base.AV_SetOverlayCanFocus(value);
            }
        }

        /// <summary>
        /// Gets or sets the number of milliseconds that the visibilty of the display overlay of the player is delayed when restoring the minimized display (form) of the player. Set to 0 to disable (default: 200 ms).
        /// </summary>
        public int Delay
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._minimizedInterval;
            }
            set
            {
                if (value == 0)
                {
                    _base._minimizedInterval = 0;
                    _base._minimizeEnabled = false;
                    _base.AV_MinimizeActivate(false);
                }
                else
                {
                    if (value < 100) value = 100;
                    else if (value > 1500) value = 1500;
                    _base._minimizedInterval = value;
                    _base._minimizeEnabled = true;
                    _base.AV_MinimizeActivate(true);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the display overlay of the player is active.
        /// </summary>
        public bool Active
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasOverlayShown;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has a display overlay (set, but not necessarily active).
        /// </summary>
        public bool Present
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasOverlay;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the display overlay of the player is active and visible.
        /// </summary>
        public bool Visible
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasOverlay && _base._overlay.Visible;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether clipping of the display overlay of the player is enabled. The overlay is clipped when it protrudes outside the parent form of the display window of the player (default: false).
        /// </summary>
        public bool Clipping
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasOverlayClipping;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                if (value != _base._hasOverlayClipping)
                {
                    _base.AV_SetOverlayClipping(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the opacity of display overlays displayed on screen copies and player display clones. May require specially designed display overlays (default: OverlayBlend.None).
        /// </summary>
        public OverlayBlend Blend
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._overlayBlend;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                _base._blendFunction.AlphaFormat = value == OverlayBlend.Transparent ? SafeNativeMethods.AC_SRC_ALPHA : SafeNativeMethods.AC_SRC_OVER;
                _base._overlayBlend = value;
            }
        }
    }

    #endregion Overlay Class

    #region DisplayClones Class

    /// <summary>
    /// A class that is used to group together the Display Clones methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class DisplayClones : HideObjectMembers
    {
        #region Fields (DisplayClones Class)

        //private const CloneFlip     DEFAULT_FLIP    = CloneFlip.FlipNone;
        //private const CloneLayout   DEFAULT_LAYOUT  = CloneLayout.Zoom;
        //private const CloneQuality  DEFAULT_QUALITY = CloneQuality.Auto;
        private const int MAX_FRAMERATE = 100;

        private Player _base;
        private CloneProperties _defaultProps;

        #endregion Fields (DisplayClones Class)

        internal DisplayClones(Player player)
        {
            _base = player;
            _defaultProps = new CloneProperties();
        }

        /// <summary>
        /// Adds a display clone to the player.
        /// </summary>
        /// <param name="clone">The form or control to add as a display clone.</param>
        public int Add(Control clone)
        {
            return (int)_base.DisplayClones_Add(new Control[] { clone }, _defaultProps);
        }

        /// <summary>
        /// Adds a display clone to the player.
        /// </summary>
        /// <param name="clone">The control to add as a display clone.</param>
        /// <param name="properties">The properties to be applied to the display clone.</param>
        public int Add(Control clone, CloneProperties properties)
        {
            _base._lastError = HResult.S_FALSE;

            if (clone != null)
            {
                _base.DisplayClones_Add(new Control[] { clone }, properties);
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Adds one or more display clones to the player.
        /// </summary>
        /// <param name="clones">The controls to add as display clones.</param>
        public int AddRange(Control[] clones)
        {
            return (int)_base.DisplayClones_Add(clones, _defaultProps);
        }

        /// <summary>
        /// Adds one or more display clones to the player.
        /// </summary>
        /// <param name="clones">The controls to add as display clones.</param>
        /// <param name="properties">The properties to be applied to the display clones.</param>
        public int AddRange(Control[] clones, CloneProperties properties)
        {
            _base.DisplayClones_Add(clones, properties);
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes a display clone from the player.
        /// </summary>
        /// <param name="clone">The display clone to remove from the player.</param>
        public int Remove(Control clone)
        {
            _base._lastError = Player.NO_ERROR;

            if (clone != null)
            {
                _base.DisplayClones_Remove(new Control[] { clone });
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes one or more display clones from the player.
        /// </summary>
        /// <param name="clones">The display clones to remove from the player.</param>
        public int RemoveRange(Control[] clones)
        {
            _base._lastError = Player.NO_ERROR;

            if (clones != null)
            {
                _base.DisplayClones_Remove(clones);
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes all display clones from the player.
        /// </summary>
        public int RemoveAll()
        {
            return (int)_base.DisplayClones_Clear();
        }

        /// <summary>
        /// Gets the number of display clones of the player.
        /// </summary>
        public int Count
        {
            get
            {
                int count = 0;
                _base._lastError = Player.NO_ERROR;

                if (_base.dc_HasDisplayClones)
                {
                    for (int i = 0; i < _base.dc_DisplayClones.Length; i++)
                    {
                        if (_base.dc_DisplayClones[i] != null && _base.dc_DisplayClones[i].Control != null) count++;
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// Returns a value indicating whether the specified form or control is a display clone of the player.
        /// </summary>
        /// <param name="control">The control to search for.</param>
        public bool Contains(Control control)
        {
            bool found = false;
            _base._lastError = Player.NO_ERROR;

            if (_base.dc_HasDisplayClones && control != null)
            {
                for (int i = 0; i < _base.dc_DisplayClones.Length; i++)
                {
                    if (_base.dc_DisplayClones[i] != null && _base.dc_DisplayClones[i].Control == control)
                    {
                        found = true;
                        break;
                    }
                }
            }
            return found;
        }

        /// <summary>
        /// Returns a list of the display clones of the player.
        /// </summary>
        public Control[] GetList()
        {
            Control[] items = null;
            _base._lastError = Player.NO_ERROR;

            if (_base.dc_HasDisplayClones)
            {
                int count = 0;

                for (int i = 0; i < _base.dc_DisplayClones.Length; i++)
                {
                    if (_base.dc_DisplayClones[i] != null && _base.dc_DisplayClones[i].Control != null) count++;
                }

                if (count > 0)
                {
                    int index = 0;
                    items = new Control[count];

                    for (int i = 0; i < _base.dc_DisplayClones.Length; i++)
                    {
                        if (_base.dc_DisplayClones[i] != null && _base.dc_DisplayClones[i].Control != null) items[index++] = _base.dc_DisplayClones[i].Control;
                    }
                }
            }
            return items;
        }

        /// <summary>
        /// Gets or sets a value indicating the number of video frames per second used for displaying the display clones of the player (default: 30 fps).
        /// </summary>
        public int FrameRate
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.dc_CloneFrameRate;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;

                if (value <= 1) value = 1;
                else if (value > MAX_FRAMERATE) value = MAX_FRAMERATE;
                _base.dc_CloneFrameRate = value;
                _base.dc_TimerInterval = 1000 / value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the display clones of the player also show display overlays (default: true).
        /// </summary>
        public bool ShowOverlay
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.dc_CloneOverlayShow;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                _base.dc_CloneOverlayShow = value;

                if (_base.dc_HasDisplayClones)
                {
                    if (value)
                    {
                        if (!_base.dc_DisplayClonesRunning && _base._hasOverlay && _base._overlayHold)
                        {
                            _base.DisplayClones_Start();
                        }
                    }
                    else if (_base.dc_DisplayClonesRunning)
                    {
                        if (!_base._playing)
                        {
                            _base.DisplayClones_Stop(false);
                        }
                        else if (!_base._hasVideo) // invalidate clone display
                        {
                            for (int i = 0; i < _base.dc_DisplayClones.Length; i++)
                            {
                                if (_base.dc_DisplayClones[i] != null && _base.dc_DisplayClones[i].Control != null)
                                {
                                    _base.dc_DisplayClones[i].Control.Invalidate();
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the shape, quality and other properties of the specified display clone of the player.
        /// </summary>
        /// <param name="clone">The display clone whose properties are to be obtained.</param>
        public CloneProperties GetProperties(Control clone)
        {
            CloneProperties properties = null;

            int index = GetCloneIndex(clone);
            if (index != -1)
            {
                properties = new CloneProperties();
                properties._dragEnabled = _base.dc_DisplayClones[index].Drag;
                properties._flip = _base.dc_DisplayClones[index].Flip;
                properties._layout = _base.dc_DisplayClones[index].Layout;
                properties._quality = _base.dc_DisplayClones[index].Quality;
                if (_base.dc_DisplayClones[index].HasShape)
                {
                    properties._shape = _base.dc_DisplayClones[index].Shape;
                    properties._videoShape = _base.dc_DisplayClones[index].HasVideoShape;
                }
                _base._lastError = Player.NO_ERROR;
            }
            else _base._lastError = HResult.S_FALSE;
            return properties;
        }

        /// <summary>
        /// Sets the shape, quality and other properties of the specified display clone of the player.
        /// </summary>
        /// <param name="clone">The display clone whose properties have to be set.</param>
        /// <param name="properties">The properties that must be set.</param>
        public int SetProperties(Control clone, CloneProperties properties)
        {
            int index = GetCloneIndex(clone);
            if (index != -1)
            {
                SetCloneProperties(_base.dc_DisplayClones[index], properties);
                _base._lastError = Player.NO_ERROR;
            }
            else _base._lastError = HResult.S_FALSE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Sets the shape, quality and other properties of all display clones of the player.
        /// </summary>
        /// <param name="properties">The properties that must be set.</param>
        public int SetProperties(CloneProperties properties)
        {
            _base._lastError = Player.NO_ERROR;

            if (_base.dc_HasDisplayClones)
            {
                for (int i = 0; i < _base.dc_DisplayClones.Length; i++)
                {
                    if (_base.dc_DisplayClones[i] != null && _base.dc_DisplayClones[i].Control != null)
                    {
                        SetCloneProperties(_base.dc_DisplayClones[i], properties);
                    }
                }
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Returns the size and location of the video image of the specified display clone of the player.
        /// </summary>
        /// <param name="clone">The display clone whose video bounds has to be obtained.</param>
        public Rectangle GetVideoBounds(Control clone)
        {
            Rectangle bounds = Rectangle.Empty;

            int index = GetCloneIndex(clone);
            if (index != -1)
            {
                if ((!_base._hasVideo && !(_base._hasOverlay && _base._overlayHold)) || _base._displayMode == DisplayMode.Stretch || _base.dc_DisplayClones[index].Layout == CloneLayout.Stretch || _base.dc_DisplayClones[index].Layout == CloneLayout.Cover)// || (_base._hasOverlay && _base._overlayMode == OverlayMode.Display))
                {
                    bounds = _base.dc_DisplayClones[index].Control.DisplayRectangle;
                }
                else
                {
                    int newSize;
                    Rectangle sourceRect = _base._videoBoundsClip;
                    Rectangle destRect = _base.dc_DisplayClones[index].Control.DisplayRectangle;

                    double difX = (double)destRect.Width / sourceRect.Width;
                    double difY = (double)destRect.Height / sourceRect.Height;

                    if (difX < difY)
                    {
                        newSize = (int)(sourceRect.Height * difX);

                        bounds.X = 0;
                        bounds.Y = (destRect.Height - newSize) / 2;
                        bounds.Width = (int)(sourceRect.Width * difX);
                        bounds.Height = newSize;
                    }
                    else
                    {
                        newSize = (int)(sourceRect.Width * difY);

                        bounds.X = (destRect.Width - newSize) / 2;
                        bounds.Y = 0;
                        bounds.Width = newSize;
                        bounds.Height = (int)(sourceRect.Height * difY);
                    }
                }
            }
            return bounds;
        }

        private void SetCloneProperties(Player.Clone clone, CloneProperties properties)
        {
            if (clone.Drag != properties._dragEnabled)
            {
                if (properties._dragEnabled)
                {
                    clone.Control.MouseDown += _base.DisplayClones_MouseDown;
                    clone.Drag = true;
                }
                else
                {
                    clone.Control.MouseDown -= _base.DisplayClones_MouseDown;
                    clone.Drag = false;
                }
            }
            clone.DragCursor = properties._dragCursor;
            clone.Flip = properties._flip;
            clone.Layout = properties._layout;
            clone.Quality = properties._quality;
            if (clone.Shape != properties._shape || clone.HasVideoShape != properties._videoShape)
            {
                SetCloneShape(clone, properties._shape, properties._videoShape);
            }
            clone.Refresh = true;
        }

        private void SetCloneShape(Player.Clone clone, DisplayShape shape, bool videoShape)
        {
            Region oldRegion = null;

            try
            {
                bool set = shape != DisplayShape.Normal;
                if (clone.HasShape)
                {
                    if (clone.Shape == shape) set = false;
                    else
                    {
                        clone.HasShape = false;
                        clone.ShapeCallback = null;
                        clone.Shape = DisplayShape.Normal;

                        if (!set && clone.Control.Region != null)
                        {
                            clone.Control.Region.Dispose();
                            clone.Control.Region = null;
                            if (_base._paused) clone.Control.Invalidate();
                        }
                        else oldRegion = clone.Control.Region;
                    }
                }
                if (set)
                {
                    clone.Shape = shape;
                    clone.HasVideoShape = videoShape;
                    clone.ShapeCallback = _base.AV_GetShapeCallback(shape);
                    clone.HasShape = true;

                    if (oldRegion != null) oldRegion.Dispose();
                    clone.Control.Invalidate();
                }
            }
            catch { /* ignore */ }
        }

        private int GetCloneIndex(Control clone)
        {
            int index = -1;
            if (_base.dc_HasDisplayClones && clone != null)
            {
                for (int i = 0; i < _base.dc_DisplayClones.Length; i++)
                {
                    if (_base.dc_DisplayClones[i] != null && _base.dc_DisplayClones[i].Control == clone)
                    {
                        index = i;
                        break;
                    }
                }
            }
            _base._lastError = index == -1 ? HResult.S_FALSE : Player.NO_ERROR;
            return index;
        }
    }

    #endregion DisplayClones Class

    #region PointTo Class

    /// <summary>
    /// A class that is used to group together the Point To conversion methods of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class PointTo : HideObjectMembers
    {
        #region Fields (PointTo Class)

        private Player _base;

        #endregion Fields (PointTo Class)

        internal PointTo(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Converts the specified screen location to coordinates of the player's display.
        /// </summary>
        /// <param name="p">The screen coordinate to convert.</param>
        public Point Display(Point p)
        {
            if (_base._hasDisplay && _base._display.Visible)
            {
                _base._lastError = Player.NO_ERROR;
                return _base._display.PointToClient(p);
            }
            _base._lastError = HResult.S_FALSE;
            return new Point(-1, -1);
        }

        /// <summary>
        /// Converts the specified screen location to coordinates of the player's display overlay.
        /// </summary>
        /// <param name="p">The screen coordinate to convert.</param>
        public Point Overlay(Point p)
        {
            if (_base._hasOverlay && _base._overlay.Visible)
            {
                _base._lastError = Player.NO_ERROR;
                return _base._overlay.PointToClient(p);
            }
            _base._lastError = HResult.S_FALSE;
            return new Point(-1, -1);
        }

        /// <summary>
        /// Converts the specified screen location to coordinates of the video image on the display of the player.
        /// </summary>
        /// <param name="p">The screen coordinate to convert.</param>
        public Point Video(Point p)
        {
            Point newP = new Point(-1, -1);
            _base._lastError = HResult.S_FALSE;

            if (_base._hasVideo)
            {
                Point p2 = _base._display.PointToClient(p);
                if (_base._videoBoundsClip.Contains(p2))
                {
                    newP.X = p2.X - _base._videoBounds.X;
                    newP.Y = p2.Y - _base._videoBounds.Y;
                    _base._lastError = Player.NO_ERROR;
                }
            }
            return newP;
        }

        /// <summary>
        /// Returns the slider value at the specified location on the specified slider (trackbar).
        /// </summary>
        /// <param name="slider">The slider whose value should be obtained.</param>
        /// <param name="location">The relative x- and y-coordinates on the slider.</param>
        public int SliderValue(TrackBar slider, Point location)
        {
            return MediaPlayer.SliderValue.FromPoint(slider, location.X, location.Y);
        }

        /// <summary>
        /// Returns the slider value at the specified location on the specified slider (trackbar).
        /// </summary>
        /// <param name="slider">The slider whose value should be obtained.</param>
        /// <param name="x">The relative x-coordinate on the slider (for horizontal oriented sliders).</param>
        /// <param name="y">The relative y-coordinate on the slider (for vertical oriented sliders).</param>
        public int SliderValue(TrackBar slider, int x, int y)
        {
            return MediaPlayer.SliderValue.FromPoint(slider, x, y);
        }
    }

    #endregion PointTo Class

    #region ScreenCopy Class

    /// <summary>
    /// A class that is used to group together the ScreenCopy methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ScreenCopy : HideObjectMembers
    {
        #region Fields (ScreenCopy Class)

        private Player _base;
        private bool _cloneCopy = true;

        #endregion Fields (ScreenCopy Class)

        internal ScreenCopy(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the display clones copy method for copying the player's video or display. Can be disabled in case of problems with the copying method for display clones (default: enabled).
        /// </summary>
        public bool CloneMode
        {
            get { return _cloneCopy; }
            set { _cloneCopy = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating the part of the screen to copy with the Player.ScreenCopy methods (default: ScreenCopyMode.Video).
        /// </summary>
        public ScreenCopyMode Mode
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._screenCopyMode;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                _base._screenCopyMode = value;
            }
        }

        /// <summary>
        /// Returns an image from the Player.ScreenCopy.Mode part of the screen.
        /// </summary>
        public Image ToImage()
        {
            if (_cloneCopy && (_base._screenCopyMode == ScreenCopyMode.Display || _base._screenCopyMode == ScreenCopyMode.Video))
            {
                return _base.AV_DisplayCopy(_base._screenCopyMode == ScreenCopyMode.Video, true);
            }

            Bitmap memoryImage = null;

            if (_base._hasDisplay && (_base._hasVideo || _base._hasOverlay))
            {
                Rectangle r;

                switch (_base._screenCopyMode)
                {
                    case ScreenCopyMode.Display:
                        r = _base._display.RectangleToScreen(_base._display.DisplayRectangle);
                        break;

                    case ScreenCopyMode.Form:
                        r = _base._display.FindForm().RectangleToScreen(_base._display.FindForm().DisplayRectangle);
                        break;

                    case ScreenCopyMode.Parent:
                        r = _base._display.Parent.RectangleToScreen(_base._display.Parent.DisplayRectangle);
                        break;

                    case ScreenCopyMode.Screen:
                        r = Screen.GetBounds(_base._display);
                        break;

                    default: // ScreenCopyMode.Video
                        if (_base._hasVideo) r = _base._display.RectangleToScreen(_base._videoBoundsClip);
                        else r = _base._display.RectangleToScreen(_base._display.DisplayRectangle);
                        break;
                }

                try
                {
                    memoryImage = new Bitmap(r.Width, r.Height);
                    Graphics memoryGraphics = Graphics.FromImage(memoryImage);
                    memoryGraphics.CopyFromScreen(r.Location.X, r.Location.Y, 0, 0, r.Size);
                    memoryGraphics.Dispose();
                    _base._lastError = Player.NO_ERROR;
                }
                catch (Exception e)
                {
                    if (memoryImage != null) { memoryImage.Dispose(); memoryImage = null; }
                    _base._lastError = (HResult)Marshal.GetHRForException(e);
                }
            }
            else
            {
                _base._lastError = HResult.S_FALSE;
            }
            return memoryImage;
        }

        /// <summary>
        /// Sets the Player.ScreenCopy.Mode to the specified value and returns an image from the specified part of the screen.
        /// </summary>
        /// <param name="mode">The value to set the Player.ScreenCopy.Mode to.</param>
        public Image ToImage(ScreenCopyMode mode)
        {
            _base._screenCopyMode = mode;
            return ToImage();
        }

        /// <summary>
        /// Copies an image from the Player.ScreenCopy.Mode part of the screen to the system's clipboard.
        /// </summary>
        public int ToClipboard()
        {
            Image theImage = ToImage();
            if (_base._lastError == Player.NO_ERROR)
            {
                try { Clipboard.SetImage(theImage); }
                catch (Exception e)
                {
                    _base._lastError = (HResult)Marshal.GetHRForException(e);
                }
                theImage.Dispose();
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Sets the Player.ScreenCopy.Mode to the specified value and copies an image from the specified part of the screen to the system's clipboard.
        /// </summary>
        /// <param name="mode">The value to set the Player.ScreenCopy.Mode to.</param>
        public int ToClipboard(ScreenCopyMode mode)
        {
            _base._screenCopyMode = mode;
            return ToClipboard();
        }

        /// <summary>
        /// Saves an image from the Player.ScreenCopy.Mode part of the screen to the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        public int ToFile(string fileName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if ((fileName != null) && (fileName.Length > 3))
            {
                Image theImage = ToImage();
                if (_base._lastError == Player.NO_ERROR)
                {
                    try { theImage.Save(fileName, imageFormat); }
                    catch (Exception e)
                    {
                        _base._lastError = (HResult)Marshal.GetHRForException(e);
                    }
                    theImage.Dispose();
                }
            }
            else
            {
                _base._lastError = HResult.ERROR_INVALID_NAME;
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Sets the Player.ScreenCopy.Mode to the specified value and saves an image from the specified part of the screen to the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        /// <param name="mode">The value to set the Player.ScreenCopy.Mode to.</param>
        public int ToFile(string fileName, System.Drawing.Imaging.ImageFormat imageFormat, ScreenCopyMode mode)
        {
            _base._screenCopyMode = mode;
            return ToFile(fileName, imageFormat);
        }
    }

    #endregion ScreenCopy Class

    #region Sliders Classes

    /// <summary>
    /// A class that is used to group together the Sliders methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Sliders : HideObjectMembers
    {
        #region Fields (Sliders Class)

        //private const int       MAX_SCROLL_VALUE = 60000;
        private Player _base;

        private PositionSlider _positionSliderClass;

        #endregion Fields (Sliders Class)

        internal Sliders(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets the shuttle slider (trackbar for Player.Position.Step method) that is controlled by the player.
        /// </summary>
        public TrackBar Shuttle
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._shuttleSlider;
            }
            set
            {
                if (value != _base._shuttleSlider)
                {
                    if (_base._hasShuttleSlider)
                    {
                        _base._shuttleSlider.MouseDown -= _base.ShuttleSlider_MouseDown;
                        //_base._shuttleSlider.MouseUp    -= _base.ShuttleSlider_MouseUp;
                        _base._shuttleSlider.MouseWheel -= _base.ShuttleSlider_MouseWheel;

                        _base._shuttleSlider = null;
                        _base._hasShuttleSlider = false;
                    }

                    if (value != null)
                    {
                        _base._shuttleSlider = value;

                        _base._shuttleSlider.SmallChange = 1;
                        _base._shuttleSlider.LargeChange = 1;

                        _base._shuttleSlider.TickFrequency = 1;

                        _base._shuttleSlider.Minimum = -5;
                        _base._shuttleSlider.Maximum = 5;
                        _base._shuttleSlider.Value = 0;

                        _base._shuttleSlider.MouseDown += _base.ShuttleSlider_MouseDown;
                        //_base._shuttleSlider.MouseUp    += _base.ShuttleSlider_MouseUp;
                        _base._shuttleSlider.MouseWheel += _base.ShuttleSlider_MouseWheel;

                        //_shuttleSlider.Enabled = _playing;
                        _base._hasShuttleSlider = true;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the audio volume slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar AudioVolume
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._volumeSlider;
            }
            set
            {
                if (value != _base._volumeSlider)
                {
                    if (_base._volumeSlider != null)
                    {
                        _base._volumeSlider.MouseWheel -= _base.VolumeSlider_MouseWheel;
                        _base._volumeSlider.Scroll -= _base.VolumeSlider_Scroll;
                        _base._volumeSlider = null;
                    }

                    if (value != null)
                    {
                        _base._volumeSlider = value;

                        _base._volumeSlider.Minimum = 0;
                        _base._volumeSlider.Maximum = 100;
                        _base._volumeSlider.TickFrequency = 10;
                        _base._volumeSlider.SmallChange = 1;
                        _base._volumeSlider.LargeChange = 10;

                        _base._volumeSlider.Value = (int)(_base._audioVolume * 100);

                        _base._volumeSlider.Scroll += _base.VolumeSlider_Scroll;
                        _base._volumeSlider.MouseWheel += _base.VolumeSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the audio balance slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar AudioBalance
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._balanceSlider;
            }
            set
            {
                if (value != _base._balanceSlider)
                {
                    if (_base._balanceSlider != null)
                    {
                        _base._balanceSlider.MouseWheel -= _base.BalanceSlider_MouseWheel;
                        _base._balanceSlider.Scroll -= _base.BalanceSlider_Scroll;
                        _base._balanceSlider = null;
                    }

                    if (value != null)
                    {
                        _base._balanceSlider = value;

                        _base._balanceSlider.Minimum = -100;
                        _base._balanceSlider.Maximum = 100;
                        _base._balanceSlider.TickFrequency = 20;
                        _base._balanceSlider.SmallChange = 1;
                        _base._balanceSlider.LargeChange = 10;

                        _base._balanceSlider.Value = (int)(_base._audioBalance * 100);
                        _base._balanceSlider.Scroll += _base.BalanceSlider_Scroll;
                        _base._balanceSlider.MouseWheel += _base.BalanceSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the playback speed slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar Speed
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._speedSlider;
            }
            set
            {
                if (value != _base._speedSlider)
                {
                    if (_base._speedSlider != null)
                    {
                        _base._speedSlider.MouseWheel -= _base.SpeedSlider_MouseWheel;
                        _base._speedSlider.Scroll -= _base.SpeedSlider_Scroll;
                        //_base._speedSlider.MouseUp      -= _base.SpeedSlider_MouseUp;
                        _base._speedSlider.MouseDown -= _base.SpeedSlider_MouseDown;

                        _base._speedSlider = null;
                    }

                    if (value != null)
                    {
                        _base._speedSlider = value;

                        _base._speedSlider.Minimum = 0;
                        _base._speedSlider.Maximum = 12;
                        _base._speedSlider.TickFrequency = 1;
                        _base._speedSlider.SmallChange = 1;
                        _base._speedSlider.LargeChange = 1;

                        _base.SpeedSlider_ValueToSlider(_base._speed);

                        _base._speedSlider.MouseDown += _base.SpeedSlider_MouseDown;
                        //_base._speedSlider.MouseUp      += _base.SpeedSlider_MouseUp;
                        _base._speedSlider.Scroll += _base.SpeedSlider_Scroll;
                        _base._speedSlider.MouseWheel += _base.SpeedSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Returns the slider value at the specified location on the specified slider (trackbar).
        /// </summary>
        /// <param name="slider">The slider whose value should be obtained.</param>
        /// <param name="location">The relative x- and y-coordinates on the slider.</param>
        public int PointToValue(TrackBar slider, Point location)
        {
            return SliderValue.FromPoint(slider, location.X, location.Y);
        }

        /// <summary>
        /// Returns the slider value at the specified location on the specified slider (trackbar).
        /// </summary>
        /// <param name="slider">The slider whose value should be obtained.</param>
        /// <param name="x">The relative x-coordinate on the slider (for horizontal oriented sliders).</param>
        /// <param name="y">The relative y-coordinate on the slider (for vertical oriented sliders).</param>
        public int PointToValue(TrackBar slider, int x, int y)
        {
            return SliderValue.FromPoint(slider, x, y);
        }

        /// <summary>
        /// Returns the location of the specified value on the specified slider (trackbar).
        /// </summary>
        /// /// <param name="slider">The slider whose value location should be obtained.</param>
        /// <param name="value">The value of the slider.</param>
        public Point ValueToPoint(TrackBar slider, int value)
        {
            return SliderValue.ToPoint(slider, value);
        }

        /// <summary>
        /// Provides access to the position slider settings of the player (for example, Player.Sliders.Position.TrackBar).
        /// </summary>
        public PositionSlider Position
        {
            get
            {
                if (_positionSliderClass == null) _positionSliderClass = new PositionSlider(_base);
                return _positionSliderClass;
            }
        }

        /// <summary>
        /// Gets or sets the video image brightness slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar Brightness
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._brightnessSlider;
            }
            set
            {
                if (value != _base._brightnessSlider)
                {
                    if (_base._brightnessSlider != null)
                    {
                        _base._brightnessSlider.MouseWheel -= _base.BrightnessSlider_MouseWheel;
                        _base._brightnessSlider.Scroll -= _base.BrightnessSlider_Scroll;
                        _base._brightnessSlider = null;
                    }

                    if (value != null)
                    {
                        _base._brightnessSlider = value;

                        _base._brightnessSlider.Minimum = -100;
                        _base._brightnessSlider.Maximum = 100;
                        _base._brightnessSlider.TickFrequency = 10;
                        _base._brightnessSlider.SmallChange = 1;
                        _base._brightnessSlider.LargeChange = 10;

                        _base._brightnessSlider.Value = (int)(_base._brightness * 100);

                        _base._brightnessSlider.Scroll += _base.BrightnessSlider_Scroll;
                        _base._brightnessSlider.MouseWheel += _base.BrightnessSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the video image contrast slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar Contrast
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._contrastSlider;
            }
            set
            {
                if (value != _base._contrastSlider)
                {
                    if (_base._contrastSlider != null)
                    {
                        _base._contrastSlider.MouseWheel -= _base.ContrastSlider_MouseWheel;
                        _base._contrastSlider.Scroll -= _base.ContrastSlider_Scroll;
                        _base._contrastSlider = null;
                    }

                    if (value != null)
                    {
                        _base._contrastSlider = value;

                        _base._contrastSlider.Minimum = -100;
                        _base._contrastSlider.Maximum = 100;
                        _base._contrastSlider.TickFrequency = 10;
                        _base._contrastSlider.SmallChange = 1;
                        _base._contrastSlider.LargeChange = 10;

                        _base._contrastSlider.Value = (int)(_base._contrast * 100);

                        _base._contrastSlider.Scroll += _base.ContrastSlider_Scroll;
                        _base._contrastSlider.MouseWheel += _base.ContrastSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the video image hue slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar Hue
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hueSlider;
            }
            set
            {
                if (value != _base._hueSlider)
                {
                    if (_base._hueSlider != null)
                    {
                        _base._hueSlider.MouseWheel -= _base.HueSlider_MouseWheel;
                        _base._hueSlider.Scroll -= _base.HueSlider_Scroll;
                        _base._hueSlider = null;
                    }

                    if (value != null)
                    {
                        _base._hueSlider = value;

                        _base._hueSlider.Minimum = -100;
                        _base._hueSlider.Maximum = 100;
                        _base._hueSlider.TickFrequency = 10;
                        _base._hueSlider.SmallChange = 1;
                        _base._hueSlider.LargeChange = 10;

                        _base._hueSlider.Value = (int)(_base._hue * 100);

                        _base._hueSlider.Scroll += _base.HueSlider_Scroll;
                        _base._hueSlider.MouseWheel += _base.HueSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the video image saturation slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar Saturation
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._saturationSlider;
            }
            set
            {
                if (value != _base._saturationSlider)
                {
                    if (_base._saturationSlider != null)
                    {
                        _base._saturationSlider.MouseWheel -= _base.SaturationSlider_MouseWheel;
                        _base._saturationSlider.Scroll -= _base.SaturationSlider_Scroll;
                        _base._saturationSlider = null;
                    }

                    if (value != null)
                    {
                        _base._saturationSlider = value;

                        _base._saturationSlider.Minimum = -100;
                        _base._saturationSlider.Maximum = 100;
                        _base._saturationSlider.TickFrequency = 10;
                        _base._saturationSlider.SmallChange = 1;
                        _base._saturationSlider.LargeChange = 10;

                        _base._saturationSlider.Value = (int)(_base._saturation * 100);

                        _base._saturationSlider.Scroll += _base.SaturationSlider_Scroll;
                        _base._saturationSlider.MouseWheel += _base.SaturationSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }
    }

    /// <summary>
    /// A class that is used to group together the Position Slider methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class PositionSlider : HideObjectMembers
    {
        #region Fields (PositionSlider Class)

        private const int MAX_SCROLL_VALUE = 60000;
        private Player _base;

        #endregion Fields (PositionSlider Class)

        internal PositionSlider(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets the media playback position slider (trackbar) that is controlled by the player.
        /// </summary>
        public MetroSet_UI.Controls.MetroSetTrackBar TrackBar
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._positionSlider;
            }
            set
            {
                if (value != _base._positionSlider)
                {
                    if (_base._hasPositionSlider)
                    {
                        _base._timer.Stop();

                        _base._positionSlider.MouseDown -= _base.PositionSlider_MouseDown;
                        //_base._positionSlider.MouseUp -= _base.PositionSlider_MouseUp;
                        //_base._positionSlider.MouseMove -= _base.PositionSlider_MouseMove;
                        _base._positionSlider.Scroll -= _base.PositionSlider_Scroll;
                        _base._positionSlider.MouseWheel -= _base.PositionSlider_MouseWheel;

                        _base._positionSlider = null;
                        _base._hasPositionSlider = false;

                        _base._psTracking = false;
                        _base._psValue = 0;
                        _base._psBusy = false;
                        _base._psSkipped = false;

                        if (_base._psTimer != null)
                        {
                            _base._psTimer.Dispose();
                            _base._psTimer = null;
                        }
                    }

                    if (value != null)
                    {
                        _base._positionSlider = value;
                        _base._hasPositionSlider = true;

                        SetPositionSliderMode(_base._psHandlesProgress);

                        // add events
                        _base._positionSlider.MouseDown += _base.PositionSlider_MouseDown;
                        //_base._positionSlider.MouseUp += _base.PositionSlider_MouseUp;
                        //_base._positionSlider.MouseMove += _base.PositionSlider_MouseMove;
                        _base._positionSlider.Scroll += _base.PositionSlider_Scroll;
                        _base._positionSlider.MouseWheel += _base.PositionSlider_MouseWheel;

                        if (!_base._playing) _base._positionSlider.Enabled = false;

                        _base._psTimer = new Timer();
                        _base._psTimer.Interval = 100;
                        _base._psTimer.Tick += _base.PositionSlider_TimerTick;
                    }
                    _base.StartMainTimerCheck();
                    _base._lastError = Player.NO_ERROR;
                }
            }
        }

        /// <summary>
        /// Gets or sets the mode (track or progress) of the player's position slider (default: PositionSliderMode.Track).
        /// </summary>
        public PositionSliderMode Mode
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._psHandlesProgress ? PositionSliderMode.Progress : PositionSliderMode.Track;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                SetPositionSliderMode(value != PositionSliderMode.Track);
            }
        }

        private void SetPositionSliderMode(bool progressMode)
        {
            _base._psHandlesProgress = progressMode;
            if (_base._hasPositionSlider)
            {
                if (_base._psHandlesProgress)
                {
                    _base._positionSlider.Minimum = (int)(_base._startTime * Player.TICKS_TO_MS);
                    _base._positionSlider.Maximum = _base._stopTime == 0 ? (_base._mediaLength == 0 ? 10 : (int)(_base._mediaLength * Player.TICKS_TO_MS)) : (int)(_base._stopTime * Player.TICKS_TO_MS);

                    if (_base._playing)
                    {
                        int pos = (int)(_base.PositionX * Player.TICKS_TO_MS);
                        if (pos < _base._positionSlider.Minimum) _base._positionSlider.Value = _base._positionSlider.Minimum;
                        else if (pos > _base._positionSlider.Maximum) _base._positionSlider.Value = _base._positionSlider.Maximum;
                        else _base._positionSlider.Value = pos;
                    }
                }
                else
                {
                    _base._positionSlider.Minimum = 0;
                    _base._positionSlider.Maximum = _base._mediaLength == 0 ? 10 : (int)(_base._mediaLength * Player.TICKS_TO_MS);
                    if (_base._playing) _base._positionSlider.Value = (int)(_base.PositionX * Player.TICKS_TO_MS);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the display of the player is updated immediately when seeking with the player's position slider (default: false).
        /// </summary>
        public bool LiveUpdate
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._psLiveSeek;
            }
            set
            {
                _base._psLiveSeek = value;
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the number of milliseconds that the slider value changes when the scroll box is moved with the mouse wheel (default: 0 (not enabled)).
        /// </summary>
        public int MouseWheel
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._psMouseWheel;
            }
            set
            {
                if (value <= 0) _base._psMouseWheel = 0;
                else if (value > MAX_SCROLL_VALUE) _base._psMouseWheel = MAX_SCROLL_VALUE;
                else _base._psMouseWheel = value;
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating when the audio output is muted when seeking with the player's position slider (default: SilentSeek.OnMoving).
        /// </summary>
        public SilentSeek SilentSeek
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._psSilentSeek;
            }
            set
            {
                _base._psSilentSeek = value;
                _base._lastError = Player.NO_ERROR;
            }
        }
    }

    #endregion Sliders Classes

    #region TaskbarProgress Class

    /// <summary>
    /// A class that is used to group together the Taskbar Progress methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class TaskbarProgress : HideObjectMembers
    {
        #region Fields (TaskbarProgress Class)

        private Player _base;
        private List<TaskbarItem> _taskbarItems;
        internal TaskbarProgressMode _progressMode;

        private class TaskbarItem
        {
            internal Form Form;
            internal IntPtr Handle;
        }

        #endregion Fields (TaskbarProgress Class)

        internal TaskbarProgress(Player player)
        {
            _base = player;

            if (!Player._taskbarProgressEnabled)
            {
                _taskbarItems = new List<TaskbarItem>(4);
                Player.TaskbarInstance = (TaskbarIndicator.ITaskbarList3)new TaskbarIndicator.TaskbarInstance();
                Player._taskbarProgressEnabled = true;
                _progressMode = TaskbarProgressMode.Progress;

                _base._lastError = Player.NO_ERROR;
            }
            else
            {
                _taskbarItems = new List<TaskbarItem>(4);
            }
        }

        #region Public - Taskbar Progress methods and properties

        /// <summary>
        /// Adds a taskbar progress indicator to the player.
        /// </summary>
        /// <param name="form">The form whose taskbar item is added as a progress indicator. Multiple forms and duplicates are allowed.</param>
        public int Add(Form form)
        {
            if (Player._taskbarProgressEnabled)
            {
                lock (_taskbarItems)
                {
                    if (form != null) // && form.ShowInTaskbar)
                    {
                        // check if already exists
                        bool exists = false;
                        for (int i = 0; i < _taskbarItems.Count; i++)
                        {
                            if (_taskbarItems[i].Form == form)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            TaskbarItem item = new TaskbarItem();
                            item.Form = form;
                            item.Handle = form.Handle;

                            _taskbarItems.Add(item);

                            if (_base._playing)
                            {
                                if (_base._paused)
                                {
                                    Player.TaskbarInstance.SetProgressState(item.Handle, TaskbarStates.Paused);
                                    SetValue(_base.PositionX);
                                }
                                else if (!_base._fileMode)
                                {
                                    Player.TaskbarInstance.SetProgressState(item.Handle, TaskbarStates.Indeterminate);
                                }
                            }

                            _base._hasTaskbarProgress = true;
                            _base.StartMainTimerCheck();
                        }
                        _base._lastError = Player.NO_ERROR;
                    }
                    else _base._lastError = HResult.S_FALSE;
                }
            }
            else _base._lastError = HResult.S_FALSE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes a taskbar progress indicator from the player.
        /// </summary>
        /// <param name="form">The form whose taskbar progress indicator is removed.</param>
        public int Remove(Form form)
        {
            if (Player._taskbarProgressEnabled)
            {
                if (_base._hasTaskbarProgress && form != null)
                {
                    lock (_taskbarItems)
                    {
                        for (int index = _taskbarItems.Count - 1; index >= 0; index--)
                        {
                            if (_taskbarItems[index].Form == form || _taskbarItems[index].Form == null)
                            {
                                if (_taskbarItems[index].Form != null)
                                {
                                    Player.TaskbarInstance.SetProgressState(_taskbarItems[index].Handle, TaskbarStates.NoProgress);
                                }
                                _taskbarItems.RemoveAt(index);
                            }
                        }

                        if (_taskbarItems.Count == 0)
                        {
                            _base._hasTaskbarProgress = false;
                            _base.StopMainTimerCheck();
                            _taskbarItems = new List<TaskbarItem>(4);
                        }
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
            else _base._lastError = HResult.S_FALSE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes all taskbar progress indicators from the player.
        /// </summary>
        public int RemoveAll()
        {
            if (Player._taskbarProgressEnabled)
            {
                if (_base._hasTaskbarProgress)
                {
                    _base._hasTaskbarProgress = false;
                    _base.StopMainTimerCheck();
                    SetState(TaskbarStates.NoProgress);
                    _taskbarItems = new List<TaskbarItem>(4);
                }
                _base._lastError = Player.NO_ERROR;
            }
            else _base._lastError = HResult.S_FALSE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes all taskbar progress indicators from the player.
        /// </summary>
        public int Clear()
        {
            return RemoveAll();
        }

        /// <summary>
        /// Gets the number of taskbar progress indicators of the player.
        /// </summary>
        public int Count
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _taskbarItems == null ? 0 : _taskbarItems.Count;
            }
        }

        /// <summary>
        /// Gets a list of the forms that have a taskbar progress indicator of the player.
        /// </summary>
        public Form[] List
        {
            get
            {
                Form[] result = null;
                if (_taskbarItems != null)
                {
                    int count = _taskbarItems.Count;
                    result = new Form[count];
                    for (int i = 0; i < count; i++)
                    {
                        result[i] = _taskbarItems[i].Form;
                    }
                    _base._lastError = Player.NO_ERROR;
                }
                else _base._lastError = HResult.S_FALSE;
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the mode (track or progress) of the taskbar progress indicator (default: TaskbarProgressMode.Progress).
        /// </summary>
        public TaskbarProgressMode Mode
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _progressMode;
            }
            set
            {
                _progressMode = value;
                if (_base._hasTaskbarProgress && _base.Playing && _base._fileMode) SetValue(_base.PositionX);
                _base._lastError = Player.NO_ERROR;
            }
        }

        #endregion Public - Taskbar Progress methods and properties

        #region Private - SetValue / SetState

        internal void SetValue(long progressValue)
        {
            long pos = progressValue;
            long total;

            if (!_base._fileMode)
            {
                total = 1;
                pos = 1;
            }
            else
            {
                if (_progressMode == TaskbarProgressMode.Track)
                {
                    total = _base._mediaLength;
                }
                else
                {
                    if (pos < _base._startTime)
                    {
                        total = _base._stopTime == 0 ? _base._mediaLength : _base._stopTime;
                    }
                    else
                    {
                        if (_base._stopTime == 0) total = _base._mediaLength - _base._startTime;
                        else
                        {
                            if (pos <= _base._stopTime) total = _base._stopTime - _base._startTime;
                            else total = _base._mediaLength - _base._startTime;
                        }
                        pos -= _base._startTime;
                    }
                }
            }

            for (int i = 0; i < _taskbarItems.Count; i++)
            {
                if (_taskbarItems[i].Form != null)
                {
                    try { Player.TaskbarInstance.SetProgressValue(_taskbarItems[i].Handle, (ulong)pos, (ulong)total); }
                    catch { _taskbarItems[i].Form = null; }
                }
            }
        }

        internal void SetState(TaskbarStates taskbarState)
        {
            for (int i = 0; i < _taskbarItems.Count; i++)
            {
                if (_taskbarItems[i].Form != null)
                {
                    try { Player.TaskbarInstance.SetProgressState(_taskbarItems[i].Handle, taskbarState); }
                    catch { _taskbarItems[i].Form = null; }
                }
            }
        }

        #endregion Private - SetValue / SetState
    }

    #endregion TaskbarProgress Class

    #region SystemPanels Class

    /// <summary>
    /// A class that is used to group together the System Panels methods of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class SystemPanels : HideObjectMembers
    {
        #region Fields (SystemPanels Class)

        private Player _base;

        #endregion Fields (SystemPanels Class)

        internal SystemPanels(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Opens the System Audio Mixer Control Panel.
        /// </summary>
        public bool ShowAudioMixer()
        {
            _base._lastError = Player.NO_ERROR;
            return ShowAudioMixer(null);
        }

        /// <summary>
        /// Opens the System Audio Mixer Control Panel.
        /// </summary>
        /// <param name="centerForm">The control panel is centered on top of the specified form.</param>
        public bool ShowAudioMixer(Form centerForm)
        {
            _base._lastError = Player.NO_ERROR;
            return SafeNativeMethods.CenterSystemDialog("SndVol.exe", "", centerForm) || SafeNativeMethods.CenterSystemDialog("SndVol32.exe", "", centerForm);
        }

        /// <summary>
        /// Opens the System Sound Control Panel.
        /// </summary>
        public bool ShowAudioDevices()
        {
            _base._lastError = Player.NO_ERROR;
            return ShowAudioDevices(null);
        }

        /// <summary>
        /// Opens the System Sound Control Panel.
        /// </summary>
        /// <param name="centerForm">The control panel is centered on top of the specified form.</param>
        public bool ShowAudioDevices(Form centerForm)
        {
            _base._lastError = Player.NO_ERROR;
            return SafeNativeMethods.CenterSystemDialog("control", "mmsys.cpl,,0", centerForm);
        }

        /// <summary>
        /// Opens the System Display Control Panel.
        /// </summary>
        public bool ShowDisplaySettings()
        {
            _base._lastError = Player.NO_ERROR;
            return ShowDisplaySettings(null);
        }

        /// <summary>
        /// Opens the System Display Control Panel.
        /// </summary>
        /// <param name="centerForm">The control panel is centered on top of the specified form.</param>
        public bool ShowDisplaySettings(Form centerForm)
        {
            _base._lastError = Player.NO_ERROR;
            return SafeNativeMethods.CenterSystemDialog("control", "desk.cpl", centerForm);
        }
    }

    #endregion SystemPanels Class

    #region Subtitles Class

    /// <summary>
    /// A class that is used to group together the Subtitles methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Subtitles : HideObjectMembers
    {
        #region Fields (Subtitles Class)

        private const int MAX_DIRECTORY_DEPTH = 3;
        private Player _base;

        #endregion Fields (Subtitles Class)

        internal Subtitles(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets a value indicating whether the subtitles of the player are activated (by subscribing to the Player.Events.MediaSubtitleChanged event) (default: false).
        /// </summary>
        public bool Enabled
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.st_SubtitlesEnabled;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the playing media (or the media specified with Player.Subtitles.Filename) has a subtitles (.srt) file.
        /// </summary>
        public bool Exists
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.Subtitles_Exists() != string.Empty;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has active subtitles.
        /// </summary>
        public bool Present
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.st_HasSubtitles;
            }
        }

        /// <summary>
        /// Gets the number of subtitles in the player's active subtitles.
        /// </summary>
        public int Count
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.st_HasSubtitles ? _base.st_SubTitleCount : 0;
            }
        }

        /// <summary>
        /// Gets the index of the current subtitle in the player's active subtitles.
        /// </summary>
        public int Current
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.st_HasSubtitles ? _base.st_CurrentIndex : 0;
            }
        }

        /// <summary>
        /// Returns the text of the current subtitle (usually obtained with the Player.Events.MediaSubtitleChanged event).
        /// </summary>
        public string GetText()
        {
            _base._lastError = Player.NO_ERROR;
            return _base.st_SubtitleOn ? _base.st_SubtitleItems[_base.st_CurrentIndex].Text : string.Empty;
        }

        /// <summary>
        /// Returns the start time (including Player.Subtitles.TimeShift) of the current subtitle.
        /// </summary>
        public TimeSpan GetStartTime()
        {
            _base._lastError = Player.NO_ERROR;
            return _base.st_SubtitleOn ? TimeSpan.FromTicks(_base.st_SubtitleItems[_base.st_CurrentIndex].StartTime + _base.st_TimeShift) : TimeSpan.Zero;
        }

        /// <summary>
        /// Returns the end time (including Player.Subtitles.TimeShift) of the current subtitle.
        /// </summary>
        public TimeSpan GetEndTime()
        {
            _base._lastError = Player.NO_ERROR;
            return _base.st_SubtitleOn ? TimeSpan.FromTicks(_base.st_SubtitleItems[_base.st_CurrentIndex].EndTime + _base.st_TimeShift) : TimeSpan.Zero;
        }

        /// <summary>
        /// Returns the text of the specified item in the player's active subtitles.
        /// </summary>
        /// <param name="index">The index of the item in the player's active subtitles.</param>
        public string GetText(int index)
        {
            _base._lastError = Player.NO_ERROR;
            if (_base.st_HasSubtitles && index >= 0 && index < _base.st_SubTitleCount) return _base.st_SubtitleItems[index].Text;
            return string.Empty;
        }

        /// <summary>
        /// Returns the start time (including Player.Subtitles.TimeShift) of the specified item in the player's active subtitles.
        /// </summary>
        /// <param name="index">The index of the item in the player's active subtitles.</param>
        public TimeSpan GetStartTime(int index)
        {
            _base._lastError = Player.NO_ERROR;
            if (_base.st_HasSubtitles && index >= 0 && index < _base.st_SubTitleCount) return TimeSpan.FromTicks(_base.st_SubtitleItems[index].StartTime + _base.st_TimeShift);
            return TimeSpan.Zero;
        }

        /// <summary>
        /// Returns the end time (including Player.Subtitles.TimeShift) of the specified item in the player's active subtitles.
        /// </summary>
        /// <param name="index">The index of the item in the player's active subtitles.</param>
        public TimeSpan GetEndTime(int index)
        {
            if (_base.st_HasSubtitles && index >= 0 && index < _base.st_SubTitleCount) return TimeSpan.FromTicks(_base.st_SubtitleItems[index].EndTime + _base.st_TimeShift);
            return TimeSpan.Zero;
        }

        /// <summary>
        /// Returns the path and file name of the player's active subtitles file.
        /// </summary>
        public string GetFileName()
        {
            _base._lastError = Player.NO_ERROR;
            if (_base.st_HasSubtitles) return _base.st_SubtitlesName;
            return string.Empty;
        }

        /// <summary>
        /// Gets or sets the text encoding of subtitles (default: Encoding.Default).
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.st_Encoding;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                if (value != _base.st_Encoding)
                {
                    _base.st_Encoding = value;
                    if (_base.st_HasSubtitles) _base.Subtitles_Start(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets the initial directory to search for subtitles files (default: string.Empty (the directory of the playing media)).
        /// </summary>
        public string Directory
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.st_Directory;
            }
            set
            {
                _base._lastError = HResult.S_FALSE;
                if (!string.IsNullOrEmpty(value) && System.IO.Directory.Exists(value))
                {
                    try
                    {
                        _base.st_Directory = Path.GetDirectoryName(value);
                        if (_base.st_SubtitlesEnabled && _base._playing) _base.Subtitles_Start(true);
                        _base._lastError = Player.NO_ERROR;
                    }
                    catch (Exception e)
                    {
                        _base.st_Directory = string.Empty;
                        _base._lastError = (HResult)Marshal.GetHRForException(e);
                    }
                }
                else _base.st_Directory = string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the number of nested directories to search for subtitles files (values 0 to 3, default: 0 (base directory only)).
        /// </summary>
        public int DirectoryDepth
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.st_DirectoryDepth;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;

                if (value <= 0) value = 0;
                else if (value >= MAX_DIRECTORY_DEPTH) value = MAX_DIRECTORY_DEPTH;
                if (value != _base.st_DirectoryDepth)
                {
                    _base.st_DirectoryDepth = value;
                    if (_base.st_SubtitlesEnabled && _base._playing && !_base.st_HasSubtitles) _base.Subtitles_Start(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets the file name of the subtitles file to search for (without directory and extension) (default: string.Empty (the file name of the playing media)). Reset when media starts playing.
        /// </summary>
        public string FileName
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.st_FileName;
            }
            set
            {
                _base._lastError = HResult.S_FALSE;
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        _base.st_FileName = Path.GetFileNameWithoutExtension(value) + Player.SUBTITLES_FILE_EXTENSION;
                        if (_base.st_SubtitlesEnabled && _base._playing) _base.Subtitles_Start(true);
                        _base._lastError = Player.NO_ERROR;
                    }
                    catch (Exception e)
                    {
                        _base.st_FileName = string.Empty;
                        _base._lastError = (HResult)Marshal.GetHRForException(e);
                    }
                }
                else _base.st_FileName = string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the number of milliseconds that subtitles appear earlier (negative values) or later (positive values) than specified by the subtitles data. Reset when media ends playing.
        /// </summary>
        public int TimeShift
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return (int)(_base.st_TimeShift * Player.TICKS_TO_MS);
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                if (value != _base.st_TimeShift)
                {
                    _base.st_TimeShift = value * Player.MS_TO_TICKS; // no check (?)
                    if (_base.st_HasSubtitles) _base.OnMediaPositionChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether any HTML tags are removed from subtitles (default: true).
        /// </summary>
        public bool RemoveHTMLTags
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.st_RemoveHTMLTags;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                if (value != _base.st_RemoveHTMLTags)
                {
                    _base.st_RemoveHTMLTags = value;
                    if (_base.st_HasSubtitles) _base.Subtitles_Start(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether audio only media can activate subtitles (default: false).
        /// </summary>
        public bool AudioOnly
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.st_AudioOnlyEnabled;
            }
            set
            {
                if (value != _base.st_AudioOnlyEnabled)
                {
                    _base._lastError = Player.NO_ERROR;
                    _base.st_AudioOnlyEnabled = value;
                    if (_base.st_SubtitlesEnabled && _base._playing && !_base._hasVideo)
                    {
                        if (_base.st_AudioOnlyEnabled) _base.Subtitles_Start(true);
                        else
                        {
                            if (_base.st_HasSubtitles) _base.Subtitles_Stop();
                        }
                    }
                }
            }
        }
    }

    #endregion Subtitles Class

    #region Position Class

    /// <summary>
    /// A class that is used to group together the Position methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Position : HideObjectMembers
    {
        #region Fields (Position Class)

        private Player _base;

        #endregion Fields (Position Class)

        internal Position(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets the playback position of the playing media, measured from the beginning of the media.
        /// </summary>
        public TimeSpan FromBegin
        {
            get
            {
                if (_base._playing)
                {
                    _base._lastError = Player.NO_ERROR;
                    if (!_base._fileMode) return TimeSpan.FromTicks(_base.PositionX - _base._deviceStart);
                    else return TimeSpan.FromTicks(_base.PositionX);
                }
                else
                {
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    return TimeSpan.Zero;
                }
            }
            set
            {
                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else _base.SetPosition(value.Ticks);
            }
        }

        /// <summary>
        /// Gets or sets the playback position of the playing media, measured from the end of the media.
        /// </summary>
        public TimeSpan ToEnd
        {
            get
            {
                long toEnd = 0;

                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else
                {
                    toEnd = _base._mediaLength - _base.PositionX;
                    if (toEnd < 0) toEnd = 0;
                    _base._lastError = Player.NO_ERROR;
                }
                return TimeSpan.FromTicks(toEnd);
            }
            set
            {
                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else _base.SetPosition(_base._mediaLength - value.Ticks);
            }
        }

        /// <summary>
        /// Gets or sets the playback position of the playing media, measured from its start time. See also: Player.Media.StartTime.
        /// </summary>
        public TimeSpan FromStart
        {
            get
            {
                if (_base._playing)
                {
                    _base._lastError = Player.NO_ERROR;
                    if (!_base._fileMode) return TimeSpan.FromTicks(_base.PositionX - _base._deviceStart);
                    else return TimeSpan.FromTicks(_base.PositionX - _base._startTime);
                }
                else
                {
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    return TimeSpan.Zero;
                }
            }
            set
            {
                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else _base.SetPosition(_base._startTime + value.Ticks);
            }
        }

        /// <summary>
        /// Gets or sets the playback position of the playing media, measured from its stop time. See also: Player.Media.StopTime.
        /// </summary>
        public TimeSpan ToStop
        {
            get
            {
                long toEnd = 0;

                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else
                {
                    if (_base._stopTime == 0)
                    {
                        toEnd = _base._mediaLength - _base.PositionX;
                        if (toEnd < 0) toEnd = 0;
                    }
                    else toEnd = _base._stopTime - _base.PositionX;
                    _base._lastError = Player.NO_ERROR;
                }
                return TimeSpan.FromTicks(toEnd);
            }
            set
            {
                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else _base.SetPosition(_base._stopTime - value.Ticks);
            }
        }

        /// <summary>
        /// Gets or sets the playback position of the playing media relative to its (natural) begin and end time. Values from 0.0 to 1.0. See also: Player.Position.Progress.
        /// </summary>
        public float Track
        {
            get
            {
                if (!_base._fileMode || !_base._playing || _base._mediaLength <= 0)
                {
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    return 0;
                }
                else
                {
                    _base._lastError = Player.NO_ERROR;
                    return (float)_base.PositionX / _base._mediaLength;
                }
            }
            set
            {
                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else
                {
                    if (value >= 0 && value < 1)
                    {
                        _base.SetPosition((long)(value * _base._mediaLength));
                    }
                    else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
            }
        }

        /// <summary>
        /// Gets or sets the playback position of the playing media relative to its (adjustable) start and stop time. Values from 0.0 to 1.0. See also: Player.Position.Track.
        /// </summary>
        public float Progress
        {
            get
            {
                if (!_base._fileMode || !_base._playing)
                {
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    return 0;
                }
                else
                {
                    _base._lastError = Player.NO_ERROR;

                    long pos = _base._stopTime == 0 ? _base._mediaLength : _base._stopTime;
                    if (pos == 0 || pos <= _base._startTime) return 0;

                    float pos2 = (_base.PositionX - _base._startTime) / (pos - _base._startTime);
                    if (pos2 < 0) return 0;
                    return pos2 > 1 ? 1 : pos2;
                }
            }
            set
            {
                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else
                {
                    if (value >= 0 && value < 1)
                    {
                        _base._lastError = Player.NO_ERROR;

                        long pos = _base._stopTime == 0 ? _base._mediaLength : _base._stopTime;
                        if (pos <= _base._startTime) return;

                        _base.SetPosition((long)(value * (pos - _base._startTime)) + _base._startTime);
                    }
                    else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
            }
        }

        /// <summary>
        /// Rewinds the playback position of the playing media to its start time. See also: Player.Media.StartTime.
        /// </summary>
        public int Rewind()
        {
            if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            else _base.SetPosition(_base._startTime);
            return (int)_base._lastError;
        }

        /// <summary>
        /// Changes the playback position of the playing media in any direction by the given amount of seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds to skip (use a negative value to skip backwards).</param>
        public int Skip(int seconds)
        {
            if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            else _base.SetPosition(_base.PositionX + (seconds * Player.ONE_SECOND_TICKS));
            return (int)_base._lastError;
        }

        /// <summary>
        /// Changes the playback position of the playing media in any direction by the given amount of (video) frames. The result can differ from the specified value.
        /// </summary>
        /// <param name="frames">The amount of frames to step (use a negative value to step backwards).</param>
        public int Step(int frames)
        {
            if (!_base._fileMode || !_base._playing)
            {
                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                return (int)_base._lastError;
            }
            else return _base.Step(frames);
        }
    }

    #endregion Position Class

    #region Media Class

    /// <summary>
    /// A class that is used to group together the Media methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Media : HideObjectMembers
    {
        #region Fields (Media Class)

        private Player _base;

        // Media album art information
        private Image _tagImage;

        private DirectoryInfo _directoryInfo;
        private string[] _searchKeyWords = { "*front*", "*cover*" }; // , "*albumart*large*" };
        private string[] _searchExtensions = { ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".tiff" };

        // Wma Guid
        //private static Guid ASF_Header_Guid = new Guid("75B22630-668E-11CF-A6D9-00AA0062CE6C");
        //private static Guid ASF_Content_Description_Guid = new Guid("75B22633-668E-11CF-A6D9-00AA0062CE6C");
        //private static Guid ASF_Extended_Content_Description_Guid = new Guid("D2D0A440-E307-11D2-97F0-00A0C95EA850");
        //private static Guid ASF_Header_Extension_Object_Guid = new Guid("5FBF03B5-A92E-11CF-8EE3-00C00C205365");
        //private static Guid ASF_Metadata_Library_Object = new Guid("44231C94-9498-49D1-A141-1D134E457054");

        #endregion Fields (Media Class)

        internal Media(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets the natural length (duration) of the playing media. See also: Player.Media.GetDuration.
        /// </summary>
        public TimeSpan Length
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (!_base._playing || !_base._fileMode) return TimeSpan.Zero;
                return TimeSpan.FromTicks(_base._mediaLength);
            }
        }

        /// <summary>
        /// Gets the duration of the playing media from its start time to its stop time. See also: Player.Media.GetDuration.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (!_base._playing || !_base._fileMode) return TimeSpan.Zero;
                return _base._stopTime == 0 ? TimeSpan.FromTicks(_base._mediaLength - _base._startTime) : TimeSpan.FromTicks(_base._stopTime - _base._startTime);
            }
        }

        /// <summary>
        /// Returns the duration of the specified part of the playing media.
        /// </summary>
        /// <param name="part">Specifies the part of the playing media whose duration is to be obtained.</param>
        public TimeSpan GetDuration(MediaPart part)
        {
            _base._lastError = Player.NO_ERROR;

            if (!_base._playing || !_base._fileMode) return TimeSpan.Zero;

            switch (part)
            {
                case MediaPart.BeginToEnd:
                    return TimeSpan.FromTicks(_base._mediaLength);
                //break;

                case MediaPart.StartToStop:
                    return _base._stopTime == 0 ? TimeSpan.FromTicks(_base._mediaLength - _base._startTime) : TimeSpan.FromTicks(_base._stopTime - _base._startTime);
                //break;

                case MediaPart.FromStart:
                    return TimeSpan.FromTicks(_base.PositionX - _base._startTime);
                //break;

                case MediaPart.ToEnd:
                    return TimeSpan.FromTicks(_base._mediaLength - _base.PositionX);
                //break;

                case MediaPart.ToStop:
                    return _base._stopTime == 0 ? TimeSpan.FromTicks(_base._mediaLength - _base.PositionX) : TimeSpan.FromTicks(_base._stopTime - _base.PositionX);
                //break;

                //case MediaLength.FromBegin:
                default:
                    return (TimeSpan.FromTicks(_base.PositionX));
                    //break;
            }
        }

        /// <summary>
        /// Returns (part of) the file (or webcam) name of the playing media.
        /// </summary>
        /// <param name="part">Specifies the part of the file name to return.</param>
        public string GetName(MediaName part)
        {
            string mediaName = string.Empty;
            _base._lastError = Player.NO_ERROR;

            if (!_base._fileMode)
            {
                if (part == MediaName.FileName || part == MediaName.FileNameWithoutExtension) mediaName = _base._fileName;
            }
            else if (_base._playing)
            {
                try
                {
                    switch (part)
                    {
                        case MediaName.FileName:
                            mediaName = Path.GetFileName(_base._fileName);
                            break;

                        case MediaName.DirectoryName:
                            mediaName = Path.GetDirectoryName(_base._fileName);
                            break;

                        case MediaName.PathRoot:
                            mediaName = Path.GetPathRoot(_base._fileName);
                            break;

                        case MediaName.Extension:
                            mediaName = Path.GetExtension(_base._fileName);
                            break;

                        case MediaName.FileNameWithoutExtension:
                            mediaName = Path.GetFileNameWithoutExtension(_base._fileName);
                            break;

                        default: // case MediaName.FullPath:
                            mediaName = _base._fileName;
                            break;
                    }
                }
                catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
            }
            return mediaName;
        }

        /// <summary>
        /// Gets the number of audio tracks in the playing media. See also: Player.Media.GetAudioTracks.
        /// </summary>
        public int AudioTrackCount
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._audioTrackCount;
            }
        }

        /// <summary>
        /// Returns a list of the audio tracks in the playing media. Returns null if no audio tracks are present. See also: Player.Media.AudioTrackCount.
        /// </summary>
        public AudioTrack[] GetAudioTracks()
        {
            _base._lastError = Player.NO_ERROR;

            AudioTrack[] tracks = null;
            int count = _base._audioTrackCount;
            if (count > 0)
            {
                tracks = new AudioTrack[count];
                for (int i = 0; i < count; i++)
                {
                    AudioTrack track = new AudioTrack();
                    track._mediaType = _base._audioTracks[i].MediaType;
                    track._name = _base._audioTracks[i].Name;
                    track._language = _base._audioTracks[i].Language;
                    track._channelCount = _base._audioTracks[i].ChannelCount;
                    track._samplerate = _base._audioTracks[i].Samplerate;
                    track._bitdepth = _base._audioTracks[i].Bitdepth;
                    track._bitrate = _base._audioTracks[i].Bitrate;
                    tracks[i] = track;
                }
            }
            return tracks;
        }

        /// <summary>
        /// Gets the number of video tracks in the playing media. See also: Player.Media.GetVideoTracks.
        /// </summary>
        public int VideoTrackCount
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._videoTrackCount;
            }
        }

        /// <summary>
        /// Returns a list of the video tracks in the playing media. Returns null if no video tracks are present. See also: Player.Media.VideoTrackCount.
        /// </summary>
        public VideoTrack[] GetVideoTracks()
        {
            _base._lastError = Player.NO_ERROR;

            VideoTrack[] tracks = null;
            int count = _base._videoTrackCount;
            if (count > 0)
            {
                tracks = new VideoTrack[count];
                for (int i = 0; i < count; i++)
                {
                    VideoTrack track = new VideoTrack();
                    track._mediaType = _base._videoTracks[i].MediaType;
                    track._name = _base._videoTracks[i].Name;
                    track._language = _base._videoTracks[i].Language;
                    track._frameRate = _base._videoTracks[i].FrameRate;
                    track._width = _base._videoTracks[i].SourceWidth;
                    track._height = _base._videoTracks[i].SourceHeight;
                    tracks[i] = track;
                }
            }
            return tracks;
        }

        /// <summary>
        /// Gets the original size of the video image of the playing media.
        /// </summary>
        public Size VideoSourceSize
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasVideo ? _base._videoSourceSize : Size.Empty;
            }
        }

        /// <summary>
        /// Gets the video frame rate of the playing media, in frames per second.
        /// </summary>
        public float VideoFrameRate
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasVideo ? _base._videoFrameRate : 0;
            }
        }

        /// <summary>
        /// Gets or sets the playback (repeat) start time of the playing media. The start time can also be set with the Player.Play instruction.
        /// </summary>
        public TimeSpan StartTime
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return TimeSpan.FromTicks(_base._startTime);
            }
            set
            {
                _base._lastError = Player.NO_ERROR;

                if (!_base._playing || !_base._fileMode) return;

                long newStart = value.Ticks;

                if (_base._startTime == newStart) return;
                if ((_base._stopTime != 0 && newStart >= _base._stopTime) || newStart >= _base._mediaLength)
                {
                    _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                    return;
                }

                _base._startTime = newStart;
                if (newStart > _base.PositionX) _base.PositionX = newStart;
                if (_base._mediaStartStopTimeChanged != null) _base._mediaStartStopTimeChanged(_base, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the playback (repeat) stop time of the playing media. The stop time can also be set with the Player.Play instruction.
        /// TimeSpan.Zero or 00:00:00 = natural end of media.
        /// </summary>
        public TimeSpan StopTime
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return TimeSpan.FromTicks(_base._stopTime);
            }
            set
            {
                _base._lastError = Player.NO_ERROR;

                if (!_base._playing || !_base._fileMode) return;

                long newStop = value.Ticks;

                if (_base._stopTime == newStop) return;
                if ((newStop != 0 && newStop < _base._startTime) || newStop >= _base._mediaLength)
                {
                    _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                    return;
                }

                _base._stopTime = newStop;
                _base.AV_UpdateTopology();
                if (_base._mediaStartStopTimeChanged != null) _base._mediaStartStopTimeChanged(_base, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Returns the metadata properties of the playing media (using ImageSource.MediaOrFolder).
        /// </summary>
        public Metadata GetMetadata()
        {
            if (_base._fileMode && _base._playing)
            {
                return GetMetadata(_base._fileName, ImageSource.MediaOrFolder);
            }
            else
            {
                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                return new Metadata();
            }
        }

        /// <summary>
        /// Returns the metadata properties of the playing media.
        /// </summary>
        /// <param name="imageSource">A value indicating whether and where an image related to the media should be obtained.</param>
        public Metadata GetMetadata(ImageSource imageSource)
        {
            if (_base._fileMode && _base._playing)
            {
                return GetMetadata(_base._fileName, imageSource);
            }
            else
            {
                _base._lastError = HResult.MF_E_NOT_AVAILABLE; ;
                return new Metadata();
            }
        }

        /// <summary>
        /// Returns the metadata properties of the specified media file.
        /// </summary>
        /// <param name="fileName">The path and name of the file whose metadata properties are to be obtained.</param>
        /// <param name="imageSource">A value indicating whether and where an image related to the media should be obtained.</param>
        /// <returns></returns>
        public Metadata GetMetadata(string fileName, ImageSource imageSource)
        {
            Metadata tagInfo;
            _base._lastError = Player.NO_ERROR;

            if (fileName == null || fileName == string.Empty)
            {
                _base._lastError = HResult.E_INVALIDARG;
                return new Metadata();
            }

            if (!new Uri(fileName).IsFile)
            {
                tagInfo = new Metadata();
                {
                    try
                    {
                        tagInfo._title = Path.GetFileNameWithoutExtension(fileName);
                        tagInfo._album = fileName;
                    }
                    catch { /* ignore */ }
                }
                return tagInfo;
            }

            tagInfo = GetMediaTags(fileName, imageSource);

            try
            {
                // Get info from file path
                if (tagInfo._artist == null || tagInfo._artist.Length == 0) tagInfo._artist = Path.GetFileName(Path.GetDirectoryName(fileName));
                if (tagInfo._title == null || tagInfo._title.Length == 0) tagInfo._title = Path.GetFileNameWithoutExtension(fileName);

                // Get album art image (with certain values of 'imageSource')
                if (imageSource == ImageSource.FolderOrMedia || imageSource == ImageSource.FolderOnly || (imageSource == ImageSource.MediaOrFolder && tagInfo._image == null))
                {
                    GetMediaImage(fileName);
                    if (_tagImage != null) // null image not to replace image retrieved from media file with FolderOrMedia
                    {
                        tagInfo._image = _tagImage;
                        _tagImage = null;
                    }
                }
            }
            catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }

            return tagInfo;
        }

        private Metadata GetMediaTags(string fileName, ImageSource imageSource)
        {
            Metadata tagInfo = new Metadata();
            IMFSourceResolver sourceResolver;
            IMFMediaSource mediaSource = null;
            IPropertyStore propStore = null;
            PropVariant propVariant = new PropVariant();

            HResult result = MFExtern.MFCreateSourceResolver(out sourceResolver);
            if (result == Player.NO_ERROR)
            {
                try
                {
                    MFObjectType objectType;
                    object source;

                    result = sourceResolver.CreateObjectFromURL(fileName, MFResolution.MediaSource, null, out objectType, out source);
                    if (result == Player.NO_ERROR)
                    {
                        mediaSource = (IMFMediaSource)source;

                        object store;
                        result = MFExtern.MFGetService(mediaSource, MFServices.MF_PROPERTY_HANDLER_SERVICE, typeof(IPropertyStore).GUID, out store);
                        if (result == Player.NO_ERROR)
                        {
                            propStore = (IPropertyStore)store;

                            // Artist
                            result = propStore.GetValue(PropertyKeys.PKEY_Music_Artist, propVariant);
                            tagInfo._artist = propVariant.GetString();
                            if (string.IsNullOrEmpty(tagInfo._artist))
                            {
                                propStore.GetValue(PropertyKeys.PKEY_Music_AlbumArtist, propVariant);
                                tagInfo._artist = propVariant.GetString();
                            }

                            // Title
                            propStore.GetValue(PropertyKeys.PKEY_Title, propVariant);
                            tagInfo._title = propVariant.GetString();

                            // Album
                            propStore.GetValue(PropertyKeys.PKEY_Music_AlbumTitle, propVariant);
                            tagInfo._album = propVariant.GetString();

                            // Genre
                            propStore.GetValue(PropertyKeys.PKEY_Music_Genre, propVariant);
                            tagInfo._genre = propVariant.GetString();

                            // Duration
                            propStore.GetValue(PropertyKeys.PKEY_Media_Duration, propVariant);
                            tagInfo._duration = TimeSpan.FromTicks((long)propVariant.GetULong());

                            // TrackNumber
                            propStore.GetValue(PropertyKeys.PKEY_Music_TrackNumber, propVariant);
                            tagInfo._trackNumber = (int)propVariant.GetUInt();

                            // Year
                            propStore.GetValue(PropertyKeys.PKEY_Media_Year, propVariant);
                            tagInfo._year = propVariant.GetUInt().ToString();

                            // Image
                            if (imageSource != ImageSource.None && imageSource != ImageSource.FolderOnly)
                            {
                                propStore.GetValue(PropertyKeys.PKEY_ThumbnailStream, propVariant);
                                if (propVariant.ptr != null)
                                {
                                    IStream stream = (IStream)Marshal.GetObjectForIUnknown(propVariant.ptr);

                                    System.Runtime.InteropServices.ComTypes.STATSTG streamInfo;
                                    stream.Stat(out streamInfo, STATFLAG.NoName);

                                    int streamSize = (int)streamInfo.cbSize;
                                    if (streamSize > 0)
                                    {
                                        byte[] buffer = new byte[streamSize];
                                        stream.Read(buffer, streamSize, IntPtr.Zero);

                                        MemoryStream ms = new MemoryStream(buffer, false);
                                        Image image = Image.FromStream(ms);

                                        tagInfo._image = new Bitmap(image);

                                        image.Dispose();
                                        ms.Dispose();
                                    }

                                    Marshal.ReleaseComObject(streamInfo);
                                    Marshal.ReleaseComObject(stream);
                                }
                            }
                        }
                    }
                }
                //catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
                catch { /* ignore */ }
            }

            if (sourceResolver != null) Marshal.ReleaseComObject(sourceResolver);
            if (mediaSource != null) Marshal.ReleaseComObject(mediaSource);
            if (propStore != null) Marshal.ReleaseComObject(propStore);
            propVariant.Dispose();

            return tagInfo;
        }

        // Get mp3 information string help function
        //private string GetMp3String(FileStream fs, byte[] buffer, int frameSize)
        //{
        //    string result;

        //    if (frameSize > buffer.Length) buffer = new byte[frameSize];
        //    fs.Read(buffer, 0, frameSize);
        //    switch (buffer[1])
        //    {
        //        case 0xFF:
        //            result = Encoding.Unicode.GetString(buffer, 1, frameSize - 1).TrimEnd('\0');
        //            break;
        //        case 0xFE:
        //            result = Encoding.BigEndianUnicode.GetString(buffer, 1, frameSize - 1).TrimEnd('\0');
        //            break;
        //        default:
        //            result = Encoding.Default.GetString(buffer, 1, frameSize - 1).TrimEnd('\0');
        //            break;
        //    }

        //    return result.Trim();
        //}

        // Get media information image help function
        private void GetMediaImage(string fileName)
        {
            _directoryInfo = new DirectoryInfo(Path.GetDirectoryName(fileName));
            string searchFileName = Path.GetFileNameWithoutExtension(fileName);
            string searchDirectoryName = _directoryInfo.Name;

            // 1. search using the file name
            if (!SearchMediaImage(searchFileName))
            {
                // 2. search using the directory name
                if (!SearchMediaImage(searchDirectoryName))
                {
                    // 3. search using keywords
                    int i = 0;
                    bool result;
                    do result = SearchMediaImage(_searchKeyWords[i++]);
                    while (!result && i < _searchKeyWords.Length);

                    if (!result)
                    {
                        // 4. find largest file
                        SearchMediaImage("*");
                    }
                }
            }
            _directoryInfo = null;
        }

        // Get media image help function
        private bool SearchMediaImage(string searchName)
        {
            if (searchName.EndsWith(@":\")) return false; // root directory - no folder name (_searchDirectoryName, for example C:\)

            string imageFile = string.Empty;
            long length = 0;
            bool found = false;

            for (int i = 0; i < _searchExtensions.Length; i++)
            {
                FileInfo[] filesFound = _directoryInfo.GetFiles(searchName + _searchExtensions[i]);

                if (filesFound.Length > 0)
                {
                    for (int j = 0; j < filesFound.Length; j++)
                    {
                        if (filesFound[j].Length > length)
                        {
                            length = filesFound[j].Length;
                            imageFile = filesFound[j].FullName;
                            found = true;
                        }
                    }
                }
            }
            if (found) _tagImage = Image.FromFile(imageFile);
            return found;
        }

        ///// <summary>
        ///// Returns the path to a new file, created from the specified embedded resource and with the specified file name, in the system's temporary folder for use with the Player.Play methods.
        ///// </summary>
        ///// <param name="resource">The embedded resource (for example, Properties.Resources.MyMedia) to save to a new file in the system's temporary folder.</param>
        ///// <param name="fileName">The file name (for example, "MyMedia.mp4") to be used for the new file in the system's temporary folder.</param>
        //public string ResourceToFile(byte[] resource, string fileName)
        //{
        //    return _base.AV_ResourceToFile(resource, fileName);
        //}
    }

    #endregion Media Class

    #region Playlist Class

    /// <summary>
    /// A class that is used to group together the Playlist methods of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Playlist : HideObjectMembers
    {
        #region Fields (Playlist Class)

        private Player _base;

        #endregion Fields (Playlist Class)

        internal Playlist(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Opens a playlist as a list of file names. Returns null if no or an empty playlist is found.
        /// </summary>
        /// <param name="playlist">The path and file name of the playlist. Supported file types are .m3u and .m3u8.</param>
        public string[] Open(string playlist)
        {
            List<string> fileNames = null;

            if (string.IsNullOrEmpty(playlist))
            {
                _base._lastError = HResult.ERROR_INVALID_NAME;
            }
            else
            {
                bool validExtension = false;
                bool m3u8 = false;

                string extension = Path.GetExtension(playlist);
                if (extension.Length == 0)
                {
                    playlist += ".m3u";
                    validExtension = true;
                }
                else if (string.Equals(extension, ".m3u", StringComparison.OrdinalIgnoreCase) || (string.Equals(extension, ".ppl", StringComparison.OrdinalIgnoreCase)))
                {
                    validExtension = true;
                }
                else if (string.Equals(extension, ".m3u8", StringComparison.OrdinalIgnoreCase))
                {
                    validExtension = true;
                    m3u8 = true;
                }

                if (validExtension)
                {
                    if (File.Exists(playlist))
                    {
                        StreamReader file = null;
                        string playListPath = Path.GetDirectoryName(playlist);
                        string line;

                        fileNames = new List<string>(16);

                        try
                        {
                            if (m3u8) file = new StreamReader(playlist, Encoding.UTF8);
                            else file = new StreamReader(playlist); // something wrong with Encoding.Default?
                            while ((line = file.ReadLine()) != null)
                            {
                                line = line.TrimStart();
                                // skip if line is empty, #extm3u, #extinf info or # comment
                                if (line != string.Empty && line[0] != '#')
                                {
                                    // get absolute path...
                                    if (line[1] != ':' && !line.Contains(@"://") && !line.Contains(@":\\")) fileNames.Add(Path.GetFullPath(Path.Combine(playListPath, line)));
                                    else fileNames.Add(line);
                                }
                            }
                            _base._lastError = Player.NO_ERROR;
                        }
                        catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }

                        if (file != null) file.Close();
                    }
                    else _base._lastError = HResult.ERROR_FILE_NOT_FOUND;
                }
                else _base._lastError = HResult.ERROR_INVALID_NAME;
            }

            if (fileNames == null || fileNames.Count == 0) return null;
            return fileNames.ToArray();
        }

        /// <summary>
        /// Saves a list of file names in the specified playlist. If the playlist already exists, it is overwritten.
        /// </summary>
        /// <param name="playlist">The path and file name of the playlist. Supported file types are .m3u and .m3u8.</param>
        /// <param name="fileNames">The list of media file names to save in the specified playlist.</param>
        /// <param name="relativePaths">A value indicating whether to use relative (to the playlist) paths with the saved file names.</param>
        public int Save(string playlist, string[] fileNames, bool relativePaths)
        {
            if (string.IsNullOrEmpty(playlist) || fileNames == null || fileNames.Length == 0)
            {
                _base._lastError = HResult.E_INVALIDARG;
            }
            else
            {
                bool validExtension = false;
                bool m3u8 = false;

                string extension = Path.GetExtension(playlist);
                if (extension.Length == 0)
                {
                    playlist += ".m3u";
                    validExtension = true;
                }
                else if (string.Equals(extension, ".m3u", StringComparison.OrdinalIgnoreCase) || (string.Equals(extension, ".ppl", StringComparison.OrdinalIgnoreCase)))
                {
                    validExtension = true;
                }
                else if (string.Equals(extension, ".m3u8", StringComparison.OrdinalIgnoreCase))
                {
                    validExtension = true;
                    m3u8 = true;
                }

                if (validExtension)
                {
                    if (relativePaths)
                    {
                        int count = fileNames.Length;
                        string[] relPaths = new string[count];
                        for (int i = 0; i < count; ++i)
                        {
                            relPaths[i] = GetRelativePath(playlist, fileNames[i]);
                        }
                        fileNames = relPaths;
                    }
                    try
                    {
                        if (m3u8) File.WriteAllLines(playlist, fileNames, Encoding.UTF8);
                        else File.WriteAllLines(playlist, fileNames);
                        _base._lastError = Player.NO_ERROR;
                    }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                }
                else _base._lastError = HResult.ERROR_INVALID_NAME;
            }
            return (int)_base._lastError;
        }

        // Taken from: https://stackoverflow.com/questions/275689/how-to-get-relative-path-from-absolute-path
        // Thanks Dave!
        private string GetRelativePath(string fromPath, string toPath)
        {
            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }
            return relativePath;
        }
    }

    #endregion Playlist Class

    #region Has Class

    /// <summary>
    /// A class that is used to group together the Has (active components) properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Has : HideObjectMembers
    {
        #region Fields (Has Class)

        private Player _base;

        #endregion Fields (Has Class)

        internal Has(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets a value indicating whether the playing media contains audio.
        /// </summary>
        public bool Audio
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasAudio;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has audio output peak level information (by subscribing to the Player.Events.MediaPeakLevelChanged event).
        /// </summary>
        public bool AudioPeakLevels
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.pm_HasPeakMeter;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the playing media contains video.
        /// </summary>
        public bool Video
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasVideo;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has a display overlay.
        /// </summary>
        public bool Overlay
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasOverlay;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has one or more display clones.
        /// </summary>
        public bool DisplayClones
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.dc_HasDisplayClones;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the playing media has active subtitles.
        /// </summary>
        public bool Subtitles
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.st_HasSubtitles;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has one or more taskbar progress indicators.
        /// </summary>
        public bool TaskbarProgress
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasTaskbarProgress;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has a custom shaped display window.
        /// </summary>
        public bool DisplayShape
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasDisplayShape;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing media (includes paused media).
        /// </summary>
        public bool Media
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._playing;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing a webcam (includes paused webcam).
        /// </summary>
        public bool Webcam
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._webcamMode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing an audio input device (with or without a webcam - includes paused audio input).
        /// </summary>
        public bool AudioInput
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return ((_base._webcamMode && _base._webcamAggregated) || _base._micMode);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has a display window.
        /// </summary>
        public bool Display
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasDisplay;
            }
        }
    }

    #endregion Has Class

    #region Speed Class

    /// <summary>
    /// A class that is used to group together the playback Speed methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Speed : HideObjectMembers
    {
        #region Fields (Speed Class)

        private Player _base;

        #endregion Fields (Speed Class)

        internal Speed(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets a value indicating the speed at which media is played by the player (normal speed = 1.0). This setting is adjusted by the player if media cannot be played at the set speed.
        /// </summary>
        public float Rate
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._speed;
            }
            set
            {
                if (value < _base.mf_SpeedMinimum || value > _base.mf_SpeedMaximum)
                {
                    _base._lastError = HResult.MF_E_UNSUPPORTED_RATE;
                }
                else _base.AV_SetSpeed(value, true);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether stream thinning (displaying fewer video frames) should be used when playing media. This option can be used to increase the maximum playback speed of media (default: false).
        /// </summary>
        public bool Boost
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._speedBoost;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                if (value != _base._speedBoost)
                {
                    _base._speedBoost = value;

                    if (_base.mf_RateControl != null)
                    {
                        ((IMFRateSupport)_base.mf_RateControl).GetFastestRate(MFRateDirection.Forward, _base._speedBoost, out _base.mf_SpeedMaximum);
                        ((IMFRateSupport)_base.mf_RateControl).GetSlowestRate(MFRateDirection.Forward, _base._speedBoost, out _base.mf_SpeedMinimum);

                        if (_base._speed != Player.DEFAULT_SPEED)
                        {
                            float trueSpeed;
                            _base.mf_RateControl.GetRate(_base._speedBoost, out trueSpeed);
                            if (_base._speed != trueSpeed)
                            {
                                _base._speed = trueSpeed == 0 ? 1 : trueSpeed;
                                _base.mf_Speed = _base._speed;
                                if (_base._speedSlider != null) _base.SpeedSlider_ValueToSlider(_base._speed);
                                if (_base._mediaSpeedChanged != null) _base._mediaSpeedChanged(this, EventArgs.Empty);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating the minimum speed at which the playing media can be played by the player.
        /// </summary>
        public float Minimum
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.mf_SpeedMinimum;
            }
        }

        /// <summary>
        /// Gets a value indicating the maximum speed at which the playing media can be played by the player.
        /// </summary>
        public float Maximum
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.mf_SpeedMaximum;
            }
        }
    }

    #endregion Speed Class

    #region Events Class

    /// <summary>
    /// A class that is used to group together the Events of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Events : HideObjectMembers
    {
        #region Fields (Events Class)

        private Player _base;

        #endregion Fields (Events Class)

        internal Events(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Occurs when media has finished playing.
        /// </summary>
        public event EventHandler<EndedEventArgs> MediaEnded
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaEnded += value;
            }
            remove { _base._mediaEnded -= value; }
        }

        /// <summary>
        /// Occurs when media has finished playing, just before the Player.Events.MediaEnded event occurs.
        /// </summary>
        public event EventHandler<EndedEventArgs> MediaEndedNotice
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaEndedNotice += value;
            }
            remove { _base._mediaEndedNotice -= value; }
        }

        /// <summary>
        /// Occurs when the repeat setting of the player has changed.
        /// </summary>
        public event EventHandler MediaRepeatChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaRepeatChanged += value;
            }
            remove { _base._mediaRepeatChanged -= value; }
        }

        /// <summary>
        /// Occurs when media has finished playing and is repeated.
        /// </summary>
        public event EventHandler MediaRepeated
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaRepeated += value;
            }
            remove { _base._mediaRepeated -= value; }
        }

        /// <summary>
        /// Occurs when media starts playing.
        /// </summary>
        public event EventHandler MediaStarted
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaStarted += value;
            }
            remove { _base._mediaStarted -= value; }
        }

        /// <summary>
        /// Occurs when the player's pause mode is activated (playing media is paused) or deactivated (paused media is resumed).
        /// </summary>
        public event EventHandler MediaPausedChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaPausedChanged += value;
            }
            remove { _base._mediaPausedChanged -= value; }
        }

        /// <summary>
        /// Occurs when the playback position of the playing media has changed.
        /// </summary>
        public event EventHandler<PositionEventArgs> MediaPositionChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaPositionChanged += value;
            }
            remove { _base._mediaPositionChanged -= value; }
        }

        /// <summary>
        /// Occurs when the start- and/or endtime of the playing media has changed.
        /// </summary>
        public event EventHandler MediaStartStopTimeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaStartStopTimeChanged += value;
            }
            remove { _base._mediaStartStopTimeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the display window of the player has changed.
        /// </summary>
        public event EventHandler MediaDisplayChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaDisplayChanged += value;
            }
            remove { _base._mediaDisplayChanged -= value; }
        }

        /// <summary>
        /// Occurs when the displaymode of the player has changed.
        /// </summary>
        public event EventHandler MediaDisplayModeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaDisplayModeChanged += value;
            }
            remove { _base._mediaDisplayModeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the shape of the display window of the player has changed.
        /// </summary>
        public event EventHandler MediaDisplayShapeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaDisplayShapeChanged += value;
            }
            remove { _base._mediaDisplayShapeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the fullscreen setting of the player has changed.
        /// </summary>
        public event EventHandler MediaFullScreenChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaFullScreenChanged += value;
            }
            remove { _base._mediaFullScreenChanged -= value; }
        }

        /// <summary>
        /// Occurs when the fullscreen mode of the player has changed.
        /// </summary>
        public event EventHandler MediaFullScreenModeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaFullScreenModeChanged += value;
            }
            remove { _base._mediaFullScreenModeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the audio volume of the player has changed.
        /// </summary>
        public event EventHandler MediaAudioVolumeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioVolumeChanged += value;
            }
            remove { _base._mediaAudioVolumeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the audio balance of the player has changed.
        /// </summary>
        public event EventHandler MediaAudioBalanceChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioBalanceChanged += value;
            }
            remove { _base._mediaAudioBalanceChanged -= value; }
        }

        ///// <summary>
        ///// Occurs when the audio enabled setting of the player has changed.
        ///// </summary>
        //public event EventHandler MediaAudioEnabledChanged
        //{
        //    add
        //    {
        //        _base._lastError = Player.NO_ERROR;
        //        _base._mediaAudioMuteChanged += value;
        //    }
        //    remove { _base._mediaAudioMuteChanged -= value; }
        //}

        /// <summary>
        /// Occurs when the audio mute setting of the player has changed.
        /// </summary>
        public event EventHandler MediaAudioMuteChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioMuteChanged += value;
            }
            remove { _base._mediaAudioMuteChanged -= value; }
        }

        /// <summary>
        /// Occurs when the videobounds of the video on the display window of the player have changed (by using VideoBounds, VideoZoom or VideoMove options).
        /// </summary>
        public event EventHandler MediaVideoBoundsChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaVideoBoundsChanged += value;
            }
            remove { _base._mediaVideoBoundsChanged -= value; }
        }

        /// <summary>
        /// Occurs when the playback speed setting of the player has changed.
        /// </summary>
        public event EventHandler MediaSpeedChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaSpeedChanged += value;
            }
            remove { _base._mediaSpeedChanged -= value; }
        }

        /// <summary>
        /// Occurs when the display overlay of the player has changed.
        /// </summary>
        public event EventHandler MediaOverlayChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaOverlayChanged += value;
            }
            remove { _base._mediaOverlayChanged -= value; }
        }

        /// <summary>
        /// Occurs when the display overlay mode setting of the player has changed.
        /// </summary>
        public event EventHandler MediaOverlayModeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaOverlayModeChanged += value;
            }
            remove { _base._mediaOverlayModeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the display overlay hold setting of the player has changed.
        /// </summary>
        public event EventHandler MediaOverlayHoldChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaOverlayHoldChanged += value;
            }
            remove { _base._mediaOverlayHoldChanged -= value; }
        }

        /// <summary>
        /// Occurs when the active state of the display overlay of the player has changed.
        /// </summary>
        public event EventHandler MediaOverlayActiveChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaOverlayActiveChanged += value;
            }
            remove { _base._mediaOverlayActiveChanged -= value; }
        }

        /// <summary>
        /// Occurs when the audio output peak level of the player has changed. Device changes are handled automatically by the player.
        /// </summary>
        public event EventHandler<PeakLevelEventArgs> MediaPeakLevelChanged
        {
            add
            {
                if (_base.PeakMeter_Open(_base._audioDevice, false))
                {
                    if (_base._peakLevelArgs == null) _base._peakLevelArgs = new PeakLevelEventArgs();
                    _base._mediaPeakLevelChanged += value;
                    _base._lastError = Player.NO_ERROR;
                    _base.StartMainTimerCheck();
                }
                else _base._lastError = HResult.ERROR_NOT_READY;
            }
            remove
            {
                if (_base.pm_HasPeakMeter)
                {
                    _base._peakLevelArgs._channelCount = _base.pm_PeakMeterChannelCount;
                    _base._peakLevelArgs._masterPeakValue = -1;
                    _base._peakLevelArgs._channelsValues = _base.pm_PeakMeterValuesStop;
                    value(this, _base._peakLevelArgs);

                    _base._mediaPeakLevelChanged -= value;
                    if (_base._mediaPeakLevelChanged == null)
                    {
                        _base.PeakMeter_Close();
                        _base.StopMainTimerCheck();
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when the current subtitle of the player has changed.
        /// </summary>
        public event EventHandler<SubtitleEventArgs> MediaSubtitleChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;

                if (_base.st_SubtitleChangedArgs == null) _base.st_SubtitleChangedArgs = new SubtitleEventArgs();
                _base._mediaSubtitleChanged += value;

                if (!_base.st_SubtitlesEnabled)
                {
                    _base.st_SubtitlesEnabled = true;
                    if (!_base.st_HasSubtitles && _base._playing)
                    {
                        _base.Subtitles_Start(true);
                        _base.StartMainTimerCheck();
                    }
                }
            }
            remove
            {
                _base._mediaSubtitleChanged -= value;
                if (_base._mediaSubtitleChanged == null)
                {
                    if (_base.st_HasSubtitles)
                    {
                        _base.st_SubtitleOn = false; // prevent 'no title' event firing
                        _base.Subtitles_Stop();
                    }
                    _base.st_SubtitlesEnabled = false;
                    _base.StopMainTimerCheck();
                }
            }
        }

        /// <summary>
        /// Occurs when the active video track of the playing media has changed.
        /// </summary>
        public event EventHandler MediaVideoTrackChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaVideoTrackChanged += value;
            }
            remove { _base._mediaVideoTrackChanged -= value; }
        }

        /// <summary>
        /// Occurs when the active audio track of the playing media has changed.
        /// </summary>
        public event EventHandler MediaAudioTrackChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioTrackChanged += value;
            }
            remove { _base._mediaAudioTrackChanged -= value; }
        }

        /// <summary>
        /// Occurs when the audio output device of the player is set to a different device. The player handles all changes to the audio output devices. You can use this event to update the application interface.
        /// </summary>
        public event EventHandler MediaAudioDeviceChanged
        {
            add
            {
                _base._mediaAudioDeviceChanged += value;
                _base._lastError = Player.NO_ERROR;
                _base.StartSystemDevicesChangedHandlerCheck();
            }
            remove
            {
                if (_base._mediaAudioDeviceChanged != null)
                {
                    _base._mediaAudioDeviceChanged -= value;
                    _base.StopSystemDevicesChangedHandlerCheck();
                }
            }
        }

        /// <summary>
        /// Occurs when the audio output devices of the system have changed. The player handles all changes to the system audio output devices. You can use this event to update the application interface.
        /// </summary>
        public event EventHandler<SystemAudioDevicesEventArgs> MediaSystemAudioDevicesChanged
        {
            add
            {
                if (Player.AudioDevicesClientOpen())
                {
                    Player._mediaSystemAudioDevicesChanged += value;
                    _base._mediaLocalAudioDevicesChanged += value; // used to unsubscribe
                    _base._lastError = Player.NO_ERROR;
                }
                else _base._lastError = HResult.ERROR_NOT_READY;
            }
            remove
            {
                Player._mediaSystemAudioDevicesChanged -= value;
                _base._mediaLocalAudioDevicesChanged -= value; // used to unsubscribe
                if (Player._mediaSystemAudioDevicesChanged == null)
                {
                    //Player.AudioDevicesClientClose();
                    _base.StopSystemDevicesChangedHandlerCheck();
                }
            }
        }

        /// <summary>
        /// Occurs when a video image color attribute (for example, brightness) of the player has changed.
        /// </summary>
        public event EventHandler<VideoColorEventArgs> MediaVideoColorChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaVideoColorChanged += value;
            }
            remove { _base._mediaVideoColorChanged -= value; }
        }

        /// <summary>
        /// Occurs when the audio input device of the player is set to a different device.
        /// </summary>
        public event EventHandler MediaAudioInputDeviceChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioInputDeviceChanged += value;
            }
            remove { _base._mediaAudioInputDeviceChanged -= value; }
        }

        /// <summary>
        /// Occurs when the format of the playing webcam has changed.
        /// </summary>
        public event EventHandler MediaWebcamFormatChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaWebcamFormatChanged += value;
            }
            remove { _base._mediaWebcamFormatChanged -= value; }
        }
    }

    #endregion Events Class
}