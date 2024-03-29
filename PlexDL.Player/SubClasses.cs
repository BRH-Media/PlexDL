﻿/****************************************************************

    PVS.MediaPlayer - Version 1.7
    January 2022, The Netherlands
    © Copyright 2022 PVS The Netherlands - licensed under The Code Project Open License (CPOL)

    PVS.MediaPlayer uses (part of) the Media Foundation .NET library by nowinskie and snarfle (https://sourceforge.net/projects/mfnet).
    Licensed under either Lesser General Public License v2.1 or BSD.  See license.txt or BSDL.txt for details (http://mfnet.sourceforge.net).

    ****************

    For use with Microsoft Windows 7 or higher*, Microsoft .NET Core 3.1, .NET 4, 5, 6 or higher and WinForms (any CPU).
    * Use of the recorders requires Windows 8 or later.

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
    8. Infolabel.cs     - movable small pop-up window

    Required references:

    System
    System.Drawing
    System.Windows.Forms

    ****************

    This file: SubClasses.cs

    Device Info Class
    Video Track Class
    Video Stream Struct
    Video Display Class
    WebCam Device Class
    Webcam Property Class
    Webcam Video Format Class
    Webcam Settings Class
    Audio Track Class
    Audio Stream Struct
    Audio Device Class
    Audio Input Device Class
    Slider Value Class
    Metadata Class
    Media Chapters Class
    Recording Information Class
    // Motion Information Class
    Display Clone Properties Class
    OverlayForm Class
    OverlayLabel Class

    Player MF Callback Class
    Hide System Object Members Classes

    Grouping Classes Player

    ****************

    Thanks!

    Thank you for your comments, suggestions and 5 star votes. You keep this library alive.

    Special thanks to the creators of Media Foundation .NET for their great library!

    Special thanks to Sean Ewington and Deeksha Shenoy of CodeProject who also took care of publishing the many
    code updates and changes in the PVS.MediaPlayer articles in a friendly, fast, and highly competent manner.
    Thank you very much, Sean and Deeksha!


    Peter Vegter
    January 2022, The Netherlands

    ****************************************************************/

#region Usings

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#endregion

#region Disable Some Compiler Warnings

#pragma warning disable IDE0044 // Add readonly modifier

#endregion


namespace PlexDL.Player
{

    // ******************************** Device Info Class (Abstract Class)

    #region Device Info Class

    #endregion


    // ******************************** Video Track Class

    #region Video Track Class

    #endregion


    // ******************************** Video Stream Struct

    #region Video Stream Struct

    #endregion


    // ******************************** Video Display Class

    #region Video Display Class

    #endregion


    // ******************************** Webcam Device Class

    #region Webcam Device Class

    #endregion


    // ******************************** Webcam Property Info Class

    #region Webcam Property Class

    #endregion


    // ******************************** Webcam Video Format Class

    #region Webcam Video Format Class

    #endregion


    // ******************************** Webcam Settings Class

    #region Webcam Settings Class

    #endregion


    // ******************************** Audio Track Class

    #region Audio Track Class

    #endregion


    // ******************************** Audio Stream Struct

    #region Audio Stream Struct

    #endregion


    // ******************************** Audio Device Class

    #region Audio Device Class

    #endregion


    // ******************************** Audio Input Device Class

    #region Audio Input Device Class

    #endregion


    // ******************************** Slider Value Class

    #region Slider Value Class

    #endregion


    // ******************************** Metadata Class

    #region Metadata Class

    #endregion


    // ******************************** Media Chapter Class

    #region Media Chapter Class

    #endregion


    // ******************************** Recording Information Class

    #region Recording Information Class

    #endregion


	// ******************************** Motion Information Class

	#region Motion Information Class

	///// <summary>
	///// A class that is used to provide motion detection information.
	///// </summary>
	//[CLSCompliant(true)]
	//public sealed class MotionInfo : HideObjectMembers
	//{
	//	#region Fields (Motion Info Class)

	//	internal int _darker;
	//	internal int _lighter;
	//	internal int _total;

	//	#endregion

	//	internal MotionInfo() { }

	//	/// <summary>
	//	/// Returns the amount of pixels in the image with a decrease in light intensity.
	//	/// <br/>The value is a normalized indicator with a value between 0 and 1000.
	//	/// </summary>
	//	public int Darker { get { return _darker; } }

	//	/// <summary>
	//	/// Returns the amount of pixels in the image with an increase in light intensity.
	//	/// <br/>The value is a normalized indicator with a value between 0 and 1000.
	//	/// </summary>
	//	public int Lighter { get { return _lighter; } }

	//	/// <summary>
	//	/// Returns the amount of pixels in the image with a change in light intensity.
	//	/// <br/>The value is a normalized indicator with a value between 0 and 1000.
	//	/// </summary>
	//	public int Total { get { return _total; } }
	//}

	#endregion


	// ******************************** Display Clone Properties Class

	#region Display Clone Properties Class

    #endregion


	// ******************************** OverlayForm Class

	#region OverlayForm Class

    #endregion


    // ******************************** OverlayLabel Class

    #region OverlayLabel Class

    #endregion


    // ******************************** Player MF CallBack Class

    #region Player MF CallBack Class

    // Media Foundation Player CallBack Class

    #endregion


	// ******************************** Hide System Object Members Classes

	#region  Hide System Object Members Classes

    #endregion



    // ******************************** Player Grouping Classes

    #region Audio Class

    #endregion

	#region Audio Multi Track Class

    #endregion

	#region Audio Input Class

    #endregion

    #region Video Class

    #endregion

    #region Video Overlay Class

    #endregion

	#region Webcam Class

    #endregion

	#region Recorder Class

    #endregion

    #region Motion Detection Class

    #endregion

	#region Display Class

    #endregion

    #region Cursor Hide Class

    /// <summary>
    /// A class that is used to group together the CursorHide methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class CursorHide : HideObjectMembers
    {
        #region Fields (CursorHide Class)

        private const int   NO_ERROR = 0;

        private Player      _base;

        #endregion

        internal CursorHide(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Adds the specified form to the forms that automatically hide the cursor (mouse pointer)
        /// <br/>during inactivity when media is playing.
        /// </summary>
        /// <param name="form">The form to add.</param>
        public int Add(Form form)
        {
            _base._lastError = Player.CH_AddItem(form, _base); //, _base._display);
            _base._hasCursorHide = Player.CH_HasItems(_base);
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes the specified form from the forms that automatically hide the cursor (mouse pointer).
        /// </summary>
        /// <param name="form">The form to remove.</param>
        public int Remove(Form form)
        {
            _base._lastError = Player.CH_RemoveItem(form, _base); //, _base._display);
            _base._hasCursorHide = Player.CH_HasItems(_base);
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes all forms added by this player from the forms that automatically hide the cursor (mouse pointer).
        /// </summary>
        public int RemoveAll()
        {
            Player.CH_RemovePlayerItems(_base);
            _base._hasCursorHide = false;
            _base._lastError = NO_ERROR;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Gets or sets a value indicating whether automatic hiding of the cursor (mouse pointer) is enabled.
        /// <br/>This option can be used to temporarily disable the hiding of the cursor.
        /// <br/>This setting is used by all players in this assembly.
        /// </summary>
        public bool Enabled
        {
            get
            {
                _base._lastError = NO_ERROR;
                return !Player.ch_Disabled;
            }
            set
            {
                //Player.ch_EventArgs._reason = CursorChangedReason.UserCommand;
                Player.CH_Disabled = !value;
                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the number of seconds to wait before the cursor (mouse pointer) is hidden
        /// <br/>during inactivity when media is playing (default: 3 seconds).
        /// <br/>This setting is used by all players in this assembly.
        /// </summary>
        public int Delay
        {
            get
            {
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Get a value indicating whether the cursor (mouse pointer) has been hidden by the player.
        /// </summary>
        public bool CursorHidden
        {
            get
            {
                _base._lastError = NO_ERROR;
                return Player.ch_Hidden;
            }
            set
            {
                if (value) { Player.CH_HideCursor(); }
                else { Player.CH_ShowCursor(); }

                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Shows the cursor (mouse pointer) if it was hidden by the player.
        /// <br/>The cursor is also shown again when its position is changed.
        /// </summary>
        public int ShowCursor()
        {
            Player.CH_ShowCursor();
            _base._lastError = NO_ERROR;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Hides the cursor (mouse pointer) on the forms that automatically hide the cursor.
        /// </summary>
        public int HideCursor()
        {
            Player.CH_HideCursor();
            _base._lastError = NO_ERROR;
            return (int)_base._lastError;
        }
    }

    #endregion

    #region Overlay Class

    #endregion

    #region Display Clones Class

    /// <summary>
    /// A class that is used to group together the Display Clones methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class DisplayClones : HideObjectMembers
    {
        #region Fields (DisplayClones Class)

        private const int       NO_ERROR        = 0;
        //private const CloneFlip     DEFAULT_FLIP    = CloneFlip.FlipNone;
        //private const CloneLayout   DEFAULT_LAYOUT  = CloneLayout.Zoom;
        //private const CloneQuality  DEFAULT_QUALITY = CloneQuality.Auto;
        private const int       MAX_FRAMERATE   = 100;

        private Player          _base;
        private CloneProperties _defaultProps;

        #endregion

        internal DisplayClones(Player player)
        {
            _base = player;
            _defaultProps = new CloneProperties();
        }

        /// <summary>
        /// Adds the specified control as a display clone to the player.
        /// </summary>
        /// <param name="clone">The form or control to add as a display clone.</param>
        public int Add(Control clone)
        {
            return (int)_base.DisplayClones_Add(new Control[] { clone }, _defaultProps);
        }

        /// <summary>
        /// Adds the specified control as a display clone to the player.
        /// </summary>
        /// <param name="clone">The control to add as a display clone.</param>
        /// <param name="properties">The properties to be applied to the display clone.</param>
        public int Add(Control clone, CloneProperties properties)
        {
            _base._lastError = HResult.E_INVALIDARG;

            if (clone != null)
            {
                _base.DisplayClones_Add(new Control[] { clone }, properties);
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Adds the specified controls as display clones to the player.
        /// </summary>
        /// <param name="clones">The controls to add as display clones.</param>
        public int AddRange(Control[] clones)
        {
            return (int)_base.DisplayClones_Add(clones, _defaultProps);
        }

        /// <summary>
        /// Adds the specified controls as display clones to the player.
        /// </summary>
        /// <param name="clones">The controls to add as display clones.</param>
        /// <param name="properties">The properties to be applied to the display clones.</param>
        public int AddRange(Control[] clones, CloneProperties properties)
        {
            _base.DisplayClones_Add(clones, properties);
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes the specified display clone from the player.
        /// </summary>
        /// <param name="clone">The display clone to remove from the player.</param>
        public int Remove(Control clone)
        {
            _base._lastError = NO_ERROR;

            if (clone != null)
            {
                _base.DisplayClones_Remove(new Control[] { clone });
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes the specified display clones from the player.
        /// </summary>
        /// <param name="clones">The display clones to remove from the player.</param>
        public int RemoveRange(Control[] clones)
        {
            _base._lastError = NO_ERROR;

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
                _base._lastError = NO_ERROR;

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
        /// Returns a value indicating whether the specified control is a display clone of the player.
        /// </summary>
        /// <param name="control">The control to search for.</param>
        public bool Contains(Control control)
        {
            bool found = false;
            _base._lastError = NO_ERROR;

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
        /// Returns the player's display clones.
        /// </summary>
        public Control[] GetClones()
        {
            Control[] items = null;
            _base._lastError = NO_ERROR;

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
        /// Gets or sets a value that indicates the number of video frames per second (fps)
        /// <br/>used for displaying the player's display clones (default: 30 fps).
        /// </summary>
        public int FrameRate
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base.dc_CloneFrameRate;
            }
            set
            {
                _base._lastError = NO_ERROR;

                if (value <= 1) value = 1;
                else if (value > MAX_FRAMERATE) value = MAX_FRAMERATE;
                _base.dc_CloneFrameRate = value;
                _base.dc_TimerInterval = 1000 / value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player's display clones also show display overlays (default: true).
        /// </summary>
        public bool ShowOverlay
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base.dc_CloneOverlayShow;
            }
            set
            {
                _base._lastError = NO_ERROR;
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
        /// Gets the adjustable properties of the specified display clone of the player.
        /// </summary>
        /// <param name="clone">The display clone whose properties are to be obtained.</param>
        public CloneProperties GetProperties(Control clone)
        {
            CloneProperties properties = null;

            int index = GetCloneIndex(clone);
            if (index != -1)
            {
                properties = new CloneProperties
                {
                    _dragEnabled    = _base.dc_DisplayClones[index].Drag,
                    _flip           = _base.dc_DisplayClones[index].Flip,
                    _layout         = _base.dc_DisplayClones[index].Layout,
                    _quality        = _base.dc_DisplayClones[index].Quality
                };

                if (_base.dc_DisplayClones[index].HasShape)
                {
                    properties._shape       = _base.dc_DisplayClones[index].Shape;
                    properties._videoShape  = _base.dc_DisplayClones[index].HasVideoShape;
                }
                _base._lastError = NO_ERROR;
            }
            return properties;
        }

        /// <summary>
        /// Sets the specified properties to the specified display clone of the player.
        /// </summary>
        /// <param name="clone">The display clone whose properties are to be set.</param>
        /// <param name="properties">The properties to be set.</param>
        public int SetProperties(Control clone, CloneProperties properties)
        {
            int index = GetCloneIndex(clone);
            if (index != -1)
            {
                SetCloneProperties(_base.dc_DisplayClones[index], properties);
                _base._lastError = NO_ERROR;
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Sets the specified properties to all display clones of the player.
        /// </summary>
        /// <param name="properties">The properties to be set.</param>
        public int SetProperties(CloneProperties properties)
        {
            _base._lastError = NO_ERROR;

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
                    Rectangle destRect  = _base.dc_DisplayClones[index].Control.DisplayRectangle;

                    double difX         = (double)destRect.Width / sourceRect.Width;
                    double difY         = (double)destRect.Height / sourceRect.Height;

                    if (difX < difY)
                    {
                        newSize         = (int)(sourceRect.Height * difX);

                        bounds.X        = 0;
                        bounds.Y        = (destRect.Height - newSize) / 2;
                        bounds.Width    = (int)(sourceRect.Width * difX);
                        bounds.Height   = newSize;
                    }
                    else
                    {
                        newSize         = (int)(sourceRect.Width * difY);

                        bounds.X        = (destRect.Width - newSize) / 2;
                        bounds.Y        = 0;
                        bounds.Width    = newSize;
                        bounds.Height   = (int)(sourceRect.Height * difY);
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
                    //while (_base.dc_PaintBusy)
                    //{
                    //    System.Threading.Thread.Sleep(1);
                    //    Application.DoEvents();
                    //}
                    clone.Control.Invalidate();
                }
            }
            catch { /* ignored */ }
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
            _base._lastError = index == -1 ? HResult.E_INVALIDARG : NO_ERROR;
            return index;
        }
    }

    #endregion

    #region Point To Class

    #endregion

    #region Copy Class

    #endregion

    #region Sliders Classes

    #endregion

    #region Taskbar Progress Class

    #endregion

    #region System Panels Class

    #endregion

    #region Subtitles Class

    /// <summary>
    /// A class that is used to group together the Subtitles methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Subtitles : HideObjectMembers
    {
        #region Fields (Subtitles Class)

        private const int   NO_ERROR            = 0;
        private const int   MAX_DIRECTORY_DEPTH = 3;

        private Player      _base;

        #endregion

        internal Subtitles(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets a value indicating whether the player's subtitles are activated (default: false).
        /// <br/>Subtitles are activated by subscribing to the Player.Events.MediaSubtitleChanged event.
        /// </summary>
        public bool Enabled
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base.st_SubtitlesEnabled;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the playing media, or the media specified with Player.Subtitles.Filename,
        /// <br/>has a subtitles (.srt) file.
        /// </summary>
        public bool Exists
        {
            get
            {
                _base._lastError = NO_ERROR;
                //return _base.Subtitles_Exists() != string.Empty;
                return _base.Subtitles_Exists().Length > 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has active subtitles.
        /// </summary>
        public bool Present
        {
            get
            {
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
                return _base.st_HasSubtitles ? _base.st_CurrentIndex : 0;
            }
        }

        /// <summary>
        /// Returns the text of the current subtitle (usually obtained with the Player.Events.MediaSubtitleChanged event).
        /// </summary>
        public string GetText()
        {
            _base._lastError = NO_ERROR;
            return _base.st_SubtitleOn ? _base.st_SubtitleItems[_base.st_CurrentIndex].Text : string.Empty;
        }

        /// <summary>
        /// Returns the start time (including Player.Subtitles.TimeShift) of the current subtitle.
        /// </summary>
        public TimeSpan GetStartTime()
        {
            _base._lastError = NO_ERROR;
            return _base.st_SubtitleOn ? TimeSpan.FromTicks(_base.st_SubtitleItems[_base.st_CurrentIndex].StartTime + _base.st_TimeShift) : TimeSpan.Zero;
        }

        /// <summary>
        /// Returns the end time (including Player.Subtitles.TimeShift) of the current subtitle.
        /// </summary>
        public TimeSpan GetEndTime()
        {
            _base._lastError = NO_ERROR;
            return _base.st_SubtitleOn ? TimeSpan.FromTicks(_base.st_SubtitleItems[_base.st_CurrentIndex].EndTime + _base.st_TimeShift) : TimeSpan.Zero;
        }

        /// <summary>
        /// Returns the text of the specified item in the player's active subtitles.
        /// </summary>
        /// <param name="index">The index of the item in the player's active subtitles.</param>
        public string GetText(int index)
        {
            _base._lastError = NO_ERROR;
            if (_base.st_HasSubtitles && index >= 0 && index < _base.st_SubTitleCount) return _base.st_SubtitleItems[index].Text;
            return string.Empty;
        }

        /// <summary>
        /// Returns the start time (including Player.Subtitles.TimeShift) of the specified item
        /// <br/>in the player's active subtitles.
        /// </summary>
        /// <param name="index">The index of the item in the player's active subtitles.</param>
        public TimeSpan GetStartTime(int index)
        {
            _base._lastError = NO_ERROR;
            if (_base.st_HasSubtitles && index >= 0 && index < _base.st_SubTitleCount) return TimeSpan.FromTicks(_base.st_SubtitleItems[index].StartTime + _base.st_TimeShift);
            return TimeSpan.Zero;
        }

        /// <summary>
        /// Returns the end time (including Player.Subtitles.TimeShift) of the specified item
        /// <br/>in the player's active subtitles.
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
            _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
                return _base.st_Encoding;
            }
            set
            {
                _base._lastError = NO_ERROR;
                if (value != _base.st_Encoding)
                {
                    _base.st_Encoding = value;
                    if (_base.st_SubtitlesEnabled && _base._playing) _base.Subtitles_Start(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets the initial directory to search for subtitles files
        /// <br/>(default: string.Empty (the directory of the playing media)).
        /// </summary>
        public string Directory
        {
            get
            {
                _base._lastError = NO_ERROR;
                if (_base.st_Directory == null) _base.st_Directory = string.Empty;
                return _base.st_Directory;
            }
            set
            {
                _base._lastError = HResult.E_INVALIDARG;
                if (!string.IsNullOrWhiteSpace(value) && System.IO.Directory.Exists(value))
                {
                    try
                    {
                        _base.st_Directory = Path.GetDirectoryName(value);
                        if (_base.st_SubtitlesEnabled && _base._playing) _base.Subtitles_Start(true);
                        _base._lastError = NO_ERROR;
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
        /// Gets or sets a value that indicates the number of nested directories to search for subtitles files.
        /// <br/>Values from 0 to 3 (default: 0 (base directory only)).
        /// </summary>
        public int DirectoryDepth
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base.st_DirectoryDepth;
            }
            set
            {
                _base._lastError = NO_ERROR;

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
        /// Gets or sets the file name of the subtitles file to search for (without directory and extension)
        /// <br/>(default: string.Empty (the file name of the playing media)).
        /// <br/>Reset to string.Empty when media starts playing.
        /// </summary>
        public string FileName
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base.st_FileName;
            }
            set
            {
                _base._lastError = NO_ERROR;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    try
                    {
                        _base.st_FileName = Path.GetFileNameWithoutExtension(value) + Player.SUBTITLES_FILE_EXTENSION;
                        if (_base.st_SubtitlesEnabled && _base._playing) _base.Subtitles_Start(true);
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
        /// Gets or sets a value that indicates the number of milliseconds that subtitles appear earlier (negative values)
        /// <br/>or later (positive values) than specified by the subtitles data.
        /// <br/>Reset when media ends playing.
        /// </summary>
        public int TimeShift
        {
            get
            {
                _base._lastError = NO_ERROR;
                return (int)(_base.st_TimeShift * Player.TICKS_TO_MS);
            }
            set
            {
                _base._lastError = NO_ERROR;
                if (value != _base.st_TimeShift)
                {
                    _base.st_TimeShift = value * Player.MS_TO_TICKS; // no check (?)
                    if (_base.st_HasSubtitles) _base.OnMediaPositionChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether any HTML tags are removed from subtitles (default: true).
        /// </summary>
        public bool RemoveHTMLTags
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base.st_RemoveHTMLTags;
            }
            set
            {
                _base._lastError = NO_ERROR;
                if (value != _base.st_RemoveHTMLTags)
                {
                    _base.st_RemoveHTMLTags = value;
                    if (_base.st_HasSubtitles) _base.Subtitles_Start(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether media files containing only audio (and no video)
        /// <br/>can activate subtitles (default: false).
        /// </summary>
        public bool AudioOnly
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base.st_AudioOnlyEnabled;
            }
            set
            {
                if (value != _base.st_AudioOnlyEnabled)
                {
                    _base._lastError = NO_ERROR;
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

    #endregion

    #region Position Class

    #endregion

    #region Media Class

    #endregion

    #region Chapters Class

    #endregion

	#region Images Class

    #endregion

    #region Playlist Class

    #endregion

    #region Has Class

    #endregion

	#region Speed Class

    #endregion

	#region Network Class

    #endregion

    #region Drag And Drop Class

    #endregion

    #region Events Class

    #endregion

    /*

    // ******************************** Video Recorder Class

    /// <summary>
    /// Represents a video recorder that can be used to store video images in a file.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class VideoRecorder : HideObjectMembers
    {
        #region Fields (Video Recorder Class)

        // Constants
        private const int       BUSY_TIME_OUT           = 1000;
        private const int       DEFAULT_FRAME_RATE      = 15;
        private const bool      DEFAULT_SHOW_OVERLAY    = true;

        // Base Player
        private Player          _base;
        private bool            _basePaused;

        // Recorder Type
        private bool            _webcam;

        // Timer
        private volatile System.Threading.Timer
                                _timer;
        private volatile int    _timerInterval;
        private volatile bool   _timerRestart;
        private volatile bool   _timerBusy;

        // Buffers
        private IMFMediaBuffer  _mediaBuffer;
        private Bitmap          _bitmapBuffer;
        private int             _bytesPerPixel  = 4;
        private Guid             _mediaType     = MFMediaType.RGB32;

        // Sink Writer
        private string          _fileName;
        private IMFSinkWriter   _sinkWriter;

        // Recorder Settings
        private int             _frameRate              = DEFAULT_FRAME_RATE;
        private bool            _showOverlay            = DEFAULT_SHOW_OVERLAY;

        internal bool           _recording;
        private bool            _paused;

        #endregion


        internal VideoRecorder(Player player, bool webCam)
        {
            _base = player;
            _webcam = webCam;
        }

        public int Start(string fileName)
        {
            return (int)_base._lastError;
        }

        public int Start(string fileName, int frameRate)
        {
            return (int)_base._lastError;
        }

        public int Start(string fileName, bool showOverlay)
        {
            return (int)_base._lastError;
        }

        public int Start(string fileName, int frameRate, bool showOverlay)
        {
            return (int)_base._lastError;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Pause()
        {
            return (int)_base._lastError;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Resume()
        {
            return (int)_base._lastError;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Stop()
        {
            if (_recording)
            {
                _base.Events.MediaPausedChanged -= BasePlayer_MediaPausedChanged;
                _base.Events.MediaEndedNotice -= BasePlayer_MediaEndedNotice;

                // stop timer etc.

                _recording = false;
                _paused = false;
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Gets or sets the number of video frames to record per second. Values from 0 to 60 (default: 15).
        /// </summary>
        public int FrameRate
        {
            get
            {
                return _frameRate;
            }
            set
            {
            }

        }

        /// <summary>
        /// Gets or sets a value that indicates whether display overlays (if any) are also recorded (default: true).
        /// </summary>
        public bool ShowOverlay
        {
            get
            {
                return _showOverlay;
            }
            set
            {
            }
        }

        // ********

        private int StartRecording()
        {
            // create timer etc.

            _base.Events.MediaPausedChanged += BasePlayer_MediaPausedChanged;
            _base.Events.MediaEndedNotice += BasePlayer_MediaEndedNotice;

            return (int)_base._lastError;
        }

        private void BasePlayer_MediaPausedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BasePlayer_MediaEndedNotice(object sender, EndedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private HResult Recorder_Init()
        {
            // check status
            if (!_base._hasVideo || (_webcam && !_base._webcamMode)) return HResult.MF_E_NOT_AVAILABLE;

            HResult result = NO_ERROR;

            // create even size
            int width = _base._videoBoundsClip.Width % 2 == 0 ? _base._videoBoundsClip.Width : _base._videoBoundsClip.Width - 1;
            int height = _base._videoBoundsClip.Height % 2 == 0 ? _base._videoBoundsClip.Height : _base._videoBoundsClip.Height - 1;
            if (_showOverlay && (_base._hasOverlay && _base._overlay.Visible))
            {
                width = _base._display.DisplayRectangle.Width;
                height = _base._display.DisplayRectangle.Height;
                if (width == height)
                {
                    if (width != _base._display.DisplayRectangle.Width) width += 2;
                    else if (height != _base._display.DisplayRectangle.Height) height += 2;
                    else height -= 2;
                }
            }
            else if (width == height)
            {
                if (width != _base._videoBoundsClip.Width) width += 2;
                else if (height != _base._videoBoundsClip.Height) height += 2;
                else height -= 2;
            }

            // set pixel depth (TO DO calculate)
            //_bytesPerPixel = 4;
            //_mediaType = MFMediaType.RGB32;

            // create sinkwriter
            _sinkWriter = CreateSinkWriter(_fileName, width, height);
            if (_base._lastError == NO_ERROR && _sinkWriter != null)
            {
                // create buffer
                _bitmapBuffer = new Bitmap(width, height);



                // add pixel depth

                //result = MFExtern.MFCreateMemoryBuffer(bufferSize, out wr_MediaBuffer);
                if (result == NO_ERROR)
                {
                }
            }


            return result;
        }

        #region Recorder - Timer Start / Stop

        private void Recorder_StartTimer()
        {
            if (_timer == null)
            {
                _timerRestart = true;
                _timer = new System.Threading.Timer(Recorder_Callback, null, 0, System.Threading.Timeout.Infinite);
            }
        }

        private void Recorder_StopTimer()
        {
            if (_timer != null)
            {
                _timerRestart = false;
                _timer.Dispose();
                _timer = null;

                int timeOut = BUSY_TIME_OUT;
                while (_timerBusy && --timeOut > 0)
                {
                    System.Threading.Thread.Sleep(1);
                    Application.DoEvents();
                }
            }
        }

        #endregion

        private IMFSinkWriter CreateSinkWriter(string fileName, int width, int height)
        {
            const int VIDEO_BIT_RATE = 800000;

            int streamIndex = 0;

            HResult result = MFExtern.MFCreateSinkWriterFromURL(fileName, null, null, out IMFSinkWriter sinkWriter);
            if (result == NO_ERROR)
            {
                result = MFExtern.MFCreateMediaType(out IMFMediaType mediaTypeOut);
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
                                        result = MFExtern.MFSetAttributeRatio(mediaTypeOut, MFAttributesClsid.MF_MT_FRAME_RATE, _frameRate, 1);
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
                result = MFExtern.MFCreateMediaType(out IMFMediaType mediaTypeIn);
                if (result == NO_ERROR)
                {
                    result = mediaTypeIn.SetGUID(MFAttributesClsid.MF_MT_MAJOR_TYPE, MFMediaType.Video);
                    if (result == NO_ERROR)
                    {
                        result = mediaTypeIn.SetGUID(MFAttributesClsid.MF_MT_SUBTYPE, _mediaType); // MFMediaType.RGB32);
                        if (result == NO_ERROR)
                        {
                            result = mediaTypeIn.SetUINT32(MFAttributesClsid.MF_MT_INTERLACE_MODE, 2); // 2 = Progressive
                            if (result == NO_ERROR)
                            {
                                result = MFExtern.MFSetAttributeSize(mediaTypeIn, MFAttributesClsid.MF_MT_FRAME_SIZE, width, height);
                                if (result == NO_ERROR)
                                {
                                    result = MFExtern.MFSetAttributeRatio(mediaTypeIn, MFAttributesClsid.MF_MT_FRAME_RATE, _frameRate, 1);
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

            if (result != NO_ERROR && sinkWriter != null)
            {
                Marshal.ReleaseComObject(sinkWriter);
                sinkWriter = null;
            }

            _base._lastError = result;
            return sinkWriter;
        }

        private void Recorder_Callback(object state)
        {
            if (!_timerBusy && _timerRestart)
            {
                IntPtr destHdc = IntPtr.Zero;
                Graphics destGraphics = null;
                Rectangle sourceRect = _base._videoBoundsClip;

                Graphics sourceGraphics = null;
                IntPtr sourceHdc = IntPtr.Zero;

                _timerBusy = true;

                try
                {
                    destGraphics = Graphics.FromImage(_bitmapBuffer); destHdc = destGraphics.GetHdc();
                    sourceGraphics = _base._display.CreateGraphics(); sourceHdc = sourceGraphics.GetHdc();

                    if (_showOverlay && (_base._hasOverlay && _base._overlay.Visible))
                    {
                        if (_base._overlayMode == OverlayMode.Display) sourceRect = _base._display.DisplayRectangle; // with overlay - same size as display

                        // copy display to buffer
                        SafeNativeMethods.StretchBlt(destHdc, 0, 0, _bitmapBuffer.Width, _bitmapBuffer.Height, sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                        sourceGraphics.ReleaseHdc(sourceHdc); sourceGraphics.Dispose(); sourceGraphics = null;

                        // copy overlay to buffer - transparent + opacity
                        sourceGraphics = _base._overlay.CreateGraphics(); sourceHdc = sourceGraphics.GetHdc();
                        if (_base._overlay.Opacity == 1 || _base._overlayBlend == OverlayBlend.None)
                        {
                            SafeNativeMethods.TransparentBlt(destHdc, 0, 0, _bitmapBuffer.Width, _bitmapBuffer.Height, sourceHdc, 0, 0, _base._overlay.Width, _base._overlay.Height, ColorTranslator.ToWin32(_base._overlay.TransparencyKey));
                        }
                        else
                        {
                            _base._blendFunction.SourceConstantAlpha = (byte)(_base._overlay.Opacity * 0xFF);
                            SafeNativeMethods.AlphaBlend(destHdc, 0, 0, _bitmapBuffer.Width, _bitmapBuffer.Height, sourceHdc, 0, 0, _base._overlay.Width, _base._overlay.Height, _base._blendFunction);
                        }
                    }
                    else
                    {
                        // copy display to buffer
                        SafeNativeMethods.StretchBlt(destHdc, 0, 0, _bitmapBuffer.Width, _bitmapBuffer.Height, sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                    }

                    sourceGraphics.ReleaseHdc(sourceHdc); sourceGraphics.Dispose(); sourceGraphics = null;
                    destGraphics.ReleaseHdc(destHdc); destGraphics.Dispose(); destGraphics = null;

                    //Recorder_WriteImageFrame(IMFSinkWriter sinkWriter, Bitmap image)
                }
                catch
                {
                    if (sourceGraphics != null)
                    {
                        if (sourceHdc != IntPtr.Zero) sourceGraphics.ReleaseHdc(sourceHdc);
                        sourceGraphics.Dispose();
                    }
                    if (destGraphics != null)
                    {
                        if (destHdc != IntPtr.Zero) destGraphics.ReleaseHdc(destHdc);
                        destGraphics.Dispose();
                    }

                }
            }

            if (_timerRestart) _timer.Change(_timerInterval, System.Threading.Timeout.Infinite);
            _timerBusy = false;
        }

        internal void Recorder_WriteImageFrame(IMFSinkWriter sinkWriter, Bitmap image)
        {
            HResult result = NO_ERROR;

            System.Drawing.Imaging.BitmapData bmpData = null;
            try { bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, image.PixelFormat); }
            catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }

            if (result == NO_ERROR)
            {
                int cbWidth = 0;// _imageBytesPerPixel * image.Width;
                int cbBuffer = cbWidth * image.Height;
                result = MFExtern.MFCreateMemoryBuffer(cbBuffer, out IMFMediaBuffer buffer);

                if (result == NO_ERROR)
                {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                    result = buffer.Lock(out IntPtr data, out int maxLength, out int currentLength);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                    if (result == NO_ERROR)
                    {
                        //result = MFExtern.MFCopyImage(data, cbWidth, bmpData.Scan0, cbWidth, cbWidth, image.Height);
                        result = MFExtern.MFCopyImage(data, cbWidth, bmpData.Scan0 + ((image.Height - 1) * cbWidth), -cbWidth, cbWidth, image.Height);
                        buffer.Unlock();

                        if (result == NO_ERROR)
                        {
                            buffer.SetCurrentLength(cbBuffer);

                            result = MFExtern.MFCreateSample(out IMFSample sample);
                            if (result == NO_ERROR)
                            {
                                result = sample.AddBuffer(buffer);
                                if (result == NO_ERROR)
                                {
                                    result = sample.SetSampleTime(0);
                                    if (result == NO_ERROR)
                                    {
                                        result = sample.SetSampleDuration(0);// _imageDuration);
                                        if (result == NO_ERROR)
                                        {
                                            sinkWriter.WriteSample(0, sample);
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
        }

        internal void Recorder_WriteImageFram2()
        {
            HResult result = NO_ERROR;

            System.Drawing.Imaging.BitmapData bmpData = null;
            try { bmpData = _bitmapBuffer.LockBits(new Rectangle(0, 0, _bitmapBuffer.Width, _bitmapBuffer.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, _bitmapBuffer.PixelFormat); }
            catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }

            if (result == NO_ERROR)
            {
                int cbWidth = _bytesPerPixel * _bitmapBuffer.Width;
                int cbBuffer = cbWidth * _bitmapBuffer.Height;
                result = MFExtern.MFCreateMemoryBuffer(cbBuffer, out IMFMediaBuffer buffer);

                if (result == NO_ERROR)
                {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                    result = buffer.Lock(out IntPtr data, out int maxLength, out int currentLength);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                    if (result == NO_ERROR)
                    {
                        //result = MFExtern.MFCopyImage(data, cbWidth, bmpData.Scan0, cbWidth, cbWidth, image.Height);
                        result = MFExtern.MFCopyImage(data, cbWidth, bmpData.Scan0 + ((_bitmapBuffer.Height - 1) * cbWidth), -cbWidth, cbWidth, _bitmapBuffer.Height);
                        buffer.Unlock();

                        if (result == NO_ERROR)
                        {
                            buffer.SetCurrentLength(cbBuffer);

                            result = MFExtern.MFCreateSample(out IMFSample sample);
                            if (result == NO_ERROR)
                            {
                                result = sample.AddBuffer(buffer);
                                if (result == NO_ERROR)
                                {
                                    result = sample.SetSampleTime(0);
                                    if (result == NO_ERROR)
                                    {
                                        result = sample.SetSampleDuration(0);// _imageDuration);
                                        if (result == NO_ERROR)
                                        {
                                            _sinkWriter.WriteSample(0, sample);
                                        }
                                    }
                                }
                                Marshal.ReleaseComObject(sample);
                            }
                        }
                    }
                    Marshal.ReleaseComObject(buffer);
                }
                _bitmapBuffer.UnlockBits(bmpData);
            }
        }
    }
    */

}
