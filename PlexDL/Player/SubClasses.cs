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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#endregion Usings

namespace PlexDL.Player
{
    // ******************************** Device Info Class

    // ******************************** Video Track Class

    // ******************************** Video Stream Struct

    // ******************************** Video Display Class

    // ******************************** Webcam Device Class

    // ******************************** Webcam Property Class

    // ******************************** Webcam Video Format Class

    // ******************************** Audio Track Class

    // ******************************** Audio Stream Struct

    // ******************************** Audio Device Class

    // ******************************** Audio Input Device Class

    // ******************************** Slider Value Class

    // ******************************** Metadata Class

    // ******************************** Clone Properties Class

    // ******************************** Player MF Callback Class

    #region Player MF Callback Class

    // Media Foundation Callback Class

    #endregion Player MF Callback Class

    // ******************************** Hide System Object Members Classes

    // ******************************** Player Grouping Classes



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
                if (value)
                    Player.CH_HideCursor();
                else
                    Player.CH_ShowCursor();

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
            return (int)_base.DisplayClones_Add(new[]
            {
                clone
            }, _defaultProps);
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
                _base.DisplayClones_Add(new[]
                {
                    clone
                }, properties);

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
                _base.DisplayClones_Remove(new[]
                {
                    clone
                });

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
                _base.DisplayClones_Remove(clones);

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
                        if (_base.dc_DisplayClones[i] != null && _base.dc_DisplayClones[i].Control != null)
                            items[index++] = _base.dc_DisplayClones[i].Control;
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
            var index = GetCloneIndex(clone);
            if (index != -1)
            {
                SetCloneProperties(_base.dc_DisplayClones[index], properties);
                _base._lastError = Player.NO_ERROR;
            }
            else
            {
                _base._lastError = HResult.S_FALSE;
            }

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
                if ((!_base._hasVideo && !(_base._hasOverlay && _base._overlayHold)) || _base._displayMode == DisplayMode.Stretch ||
                    _base.dc_DisplayClones[index].Layout == CloneLayout.Stretch ||
                    _base.dc_DisplayClones[index].Layout == CloneLayout.Cover) // || (_base._hasOverlay && _base._overlayMode == OverlayMode.Display))
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
            catch
            {
                /* ignore */
            }
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
            if (_base.st_HasSubtitles && index >= 0 && index < _base.st_SubTitleCount)
                return TimeSpan.FromTicks(_base.st_SubtitleItems[index].StartTime + _base.st_TimeShift);
            return TimeSpan.Zero;
        }

        /// <summary>
        /// Returns the end time (including Player.Subtitles.TimeShift) of the specified item in the player's active subtitles.
        /// </summary>
        /// <param name="index">The index of the item in the player's active subtitles.</param>
        public TimeSpan GetEndTime(int index)
        {
            if (_base.st_HasSubtitles && index >= 0 && index < _base.st_SubTitleCount)
                return TimeSpan.FromTicks(_base.st_SubtitleItems[index].EndTime + _base.st_TimeShift);
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
                        if (_base.st_AudioOnlyEnabled)
                        {
                            _base.Subtitles_Start(true);
                        }
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

}