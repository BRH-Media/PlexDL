/****************************************************************

    PVS.MediaPlayer - Version 1.0
    September 2020, The Netherlands
    © Copyright 2020 PVS The Netherlands - licensed under The Code Project Open License (CPOL)

    PVS.MediaPlayer uses (part of) the Media Foundation .NET library by nowinskie and snarfle (https://sourceforge.net/projects/mfnet).
    Licensed under either Lesser General Public License v2.1 or BSD.  See license.txt or BSDL.txt for details (http://mfnet.sourceforge.net).

    ****************

    For use with Microsoft Windows 7 or higher, Microsoft .NET Framework version 4.0 or higher and WinForms (any CPU).
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

    This file: DisplayClones.cs

    Player Class
    Extension to file 'Player.cs'.

    Display Clones
    Video Recorder

    ****************

    Many thanks to Microsoft (Windows, .NET Framework, Visual Studio and others), all the people
    writing about programming on the internet (a great source for ideas and solving problems),
    the websites publishing those or other writings about programming, the people responding to the
    PVS.MediaPlayer articles with comments and suggestions and, of course, the people at CodeProject.

    Special thanks to the creators of Media Foundation .NET for their great library.

    Special thanks to Sean Ewington and Deeksha Shenoy of CodeProject who also took care of publishing the many
    code updates and changes in the PVS.MediaPlayer articles in a friendly, fast, and highly competent manner.

    Peter Vegter
    September 2020, The Netherlands

    ****************************************************************/

#region Usings

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

#endregion

#region Disable Some Compiler Warnings

#pragma warning disable IDE0044 // Add readonly modifier

#endregion


namespace PlexDL.Player
{

    // ******************************** Display Clones - Enumerations

    #region Display Clones - Enumerations

    #endregion


    public partial class Player
    {
        /*
            Display Clones

            This section provides easy cloning (copying) of the video display of the player to one or more other display
            controls by using the Win32 BitBlt functions.
        */


        // ******************************** Display Clones - Fields

        #region Display Clones - Fields

        // Display Clone data
        internal class Clone
        {
            internal Control            Control;
            internal CloneQuality       Quality;
            internal CloneLayout        Layout;
            internal CloneFlip          Flip;
            internal bool               HasShape;
            internal DisplayShape       Shape;
            internal bool               HasVideoShape;
            internal ShapeCallback      ShapeCallback;
            internal bool               Drag;
            internal Cursor             DragCursor;
            internal bool               Refresh;
            internal int                Errors;     // count errors because there can be 'no-true-error glitches' (?)
        }

        private const int               DC_DEFAULT_FRAMERATE        = 30;
        private const bool              DC_DEFAULT_OVERLAY_SHOW     = true;
        private const int               DC_BUSY_TIME_OUT            = 1000;

        internal int                    dc_CloneFrameRate           = DC_DEFAULT_FRAMERATE;
        internal bool                   dc_CloneOverlayShow         = DC_DEFAULT_OVERLAY_SHOW;

        internal bool                   dc_HasDisplayClones;
        internal bool                   dc_DisplayClonesRunning;
        internal volatile bool          dc_PaintBusy;
        internal volatile Clone[]       dc_DisplayClones;

        private volatile System.Threading.Timer  dc_Timer;
        internal volatile int           dc_TimerInterval            = 1000 / DC_DEFAULT_FRAMERATE;
        private volatile bool           dc_TimerRestart;

        private Bitmap                  dc_BackBuffer;

        private Region                  dc_RefreshRegion;
        private Rectangle               dc_RefreshRect;
        private bool                    dc_NoSizeDisplay;

        private delegate void           RefreshCloneCallback(int index);
        private RefreshCloneCallback    dc_RefreshCallback;

        private bool                    dc_IsDragging;
        internal Cursor                 dc_OldCursor;
        private Form                    dc_DragForm;
        private Point                   dc_OldLocation;

        private object                  dc_Lock                     = new object();

        #endregion


        // ******************************** Display Clones - Add / DisplayCheck / Clear / Remove

        #region Display Clones - Add / DisplayCheck / Clear / Remove

        internal HResult DisplayClones_Add(Control[] clones, CloneProperties properties)
        {
            lock (dc_Lock)
            {
                if (!_hasDisplay)
                {
                    _lastError = HResult.MF_E_NOT_AVAILABLE;
                    return _lastError;
                }
                if (clones == null || clones.Length == 0)
                {
                    _lastError = HResult.E_INVALIDARG;
                    return _lastError;
                }

                _lastError = NO_ERROR;

                int addCount = 0;
                bool duplicate = false;
                bool resume = DisplayClones_Pause();

                Control[] addClones = new Control[clones.Length];
                for (int i = 0; i < clones.Length; i++)
                {
                    duplicate = false;
                    if (dc_HasDisplayClones) // check if specified clones already exist
                    {
                        for (int j = 0; j < dc_DisplayClones.Length; j++)
                        {
                            if (clones[i] == null || (dc_DisplayClones[j] != null && clones[i] == dc_DisplayClones[j].Control))
                            {
                                duplicate = true;
                                break;
                            }
                        }
                    }
                    if (!duplicate)
                    {
                        for (int j = i + 1; j < clones.Length; j++) // check for duplicates in specified clones
                        {
                            if (clones[i] == null || clones[i] == clones[j] || clones[i] == _display)
                            {
                                duplicate = true;
                                break;
                            }
                        }
                    }
                    if (!duplicate)
                    {
                        addClones[i] = clones[i];
                        addCount++;
                    }
                }

                if (addCount > 0)
                {
                    int oldCount = 0;
                    int index = 0;

                    if (dc_HasDisplayClones)
                    {
                        for (int i = 0; i < dc_DisplayClones.Length; i++) // count existing display clones (skip any 'null')
                        {
                            if (dc_DisplayClones[i] != null) oldCount++;
                        }
                    }

                    Clone[] newClones = new Clone[oldCount + addCount];
                    if (dc_HasDisplayClones)
                    {
                        for (int i = 0; i < dc_DisplayClones.Length; i++) // add existing display clones
                        {
                            if (dc_DisplayClones[i] != null) newClones[index++] = dc_DisplayClones[i];
                        }
                    }

                    for (int i = 0; i < addClones.Length; i++) // add new display clones
                    {
                        if (addClones[i] != null)
                        {
                            newClones[index] = new Clone
                            {
                                Control = addClones[i]
                            };
                            newClones[index].Control.SizeChanged    += DisplayClones_SizeChanged;
                            newClones[index].Control.Invalidated    += DisplayClones_SizeChanged;
                            newClones[index].Flip                   = properties._flip;
                            newClones[index].Layout                 = properties._layout;
                            newClones[index].Quality                = properties._quality;
                            if (properties._dragEnabled)
                            {
                                newClones[index].Control.MouseDown  += DisplayClones_MouseDown;
                                newClones[index].Drag               = true;
                            }
                            newClones[index].DragCursor             = properties.DragCursor;
                            if (properties._shape != DisplayShape.Normal)
                            {
                                newClones[index].Shape              = properties._shape;
                                newClones[index].HasVideoShape      = properties._videoShape;
                                newClones[index].ShapeCallback      = AV_GetShapeCallback(properties._shape);
                                newClones[index].HasShape           = true;
                                //newClones[index].Control.Invalidate();
                            }
                            newClones[index++].Refresh              = true;
                        }
                    }

                    dc_DisplayClones = newClones;
                    dc_HasDisplayClones = true;
                    _mediaDisplayClonesChanged?.Invoke(this, EventArgs.Empty);

                    if (!dc_DisplayClonesRunning) DisplayClones_Start(); // was not paused (resume == false)
                }
                if (resume) DisplayClones_Resume();
                return _lastError;
            }
        }

        // check if a display clone is the same as the display of the player
        private void DisplayClones_DisplayCheck()
        {
            if (_hasDisplay && dc_HasDisplayClones)
            {
                for (int i = 0; i < dc_DisplayClones.Length; i++)
                {
                    if (dc_DisplayClones[i] != null && dc_DisplayClones[i].Control == _display)
                    {
                        dc_DisplayClones[i].Control.SizeChanged -= DisplayClones_SizeChanged;
                        dc_DisplayClones[i].Control.Invalidated -= DisplayClones_SizeChanged;
                        dc_DisplayClones[i].Control             = null;
                        dc_DisplayClones[i]                     = null;
                    }
                }
            }
        }

        internal HResult DisplayClones_Clear()
        {
            lock (dc_Lock)
            {
                _lastError = NO_ERROR;

                if (dc_HasDisplayClones)
                {
                    DisplayClones_Stop(true);

                    for (int i = 0; i < dc_DisplayClones.Length; i++)
                    {
                        if (dc_DisplayClones[i] != null && dc_DisplayClones[i].Control != null)
                        {
                            try
                            {
                                dc_DisplayClones[i].Control.SizeChanged -= DisplayClones_SizeChanged;
                                dc_DisplayClones[i].Control.Invalidated -= DisplayClones_SizeChanged;
                                if (dc_DisplayClones[i].Drag)
                                {
                                    dc_DisplayClones[i].Control.MouseDown -= DisplayClones_MouseDown;
                                    dc_DisplayClones[i].Drag = false;
                                }
                                if (dc_DisplayClones[i].HasShape)
                                {
                                    dc_DisplayClones[i].HasShape        = false;
                                    dc_DisplayClones[i].ShapeCallback   = null;
                                    if (dc_DisplayClones[i].Control.Region != null)
                                    {
                                        dc_DisplayClones[i].Control.Region.Dispose();
                                        dc_DisplayClones[i].Control.Region = null;
                                    }
                                }
                            }
                            catch { /* ignore */ }
                            dc_DisplayClones[i].Control = null;
                            dc_DisplayClones[i]         = null;
                        }
                    }
                    dc_HasDisplayClones = false;
                    dc_DisplayClones    = null;

                    _mediaDisplayClonesChanged?.Invoke(this, EventArgs.Empty);
                }
                return _lastError;
            }
        }

        internal HResult DisplayClones_Remove(Control[] clones)
        {
            lock (dc_Lock)
            {
                _lastError = NO_ERROR;
                if (dc_HasDisplayClones && clones != null)
                {
                    bool clonesStopped = false;
                    bool resume = DisplayClones_Pause();

                    for (int i = 0; i < clones.Length; i++)
                    {
                        if (clones[i] != null && dc_DisplayClones != null) // TODO tooltip problem - check
                        {
                            for (int j = 0; j < dc_DisplayClones.Length; j++)
                            {
                                if (dc_DisplayClones[j] != null && dc_DisplayClones[j].Control == clones[i])
                                {
                                    try
                                    {
                                        dc_DisplayClones[j].Control.SizeChanged -= DisplayClones_SizeChanged;
                                        dc_DisplayClones[j].Control.Invalidated -= DisplayClones_SizeChanged;
                                        if (dc_DisplayClones[j].Drag)
                                        {
                                            dc_DisplayClones[j].Control.MouseDown -= DisplayClones_MouseDown;
                                            dc_DisplayClones[j].Drag = false;
                                        }
                                        if (dc_DisplayClones[j].HasShape)
                                        {
                                            dc_DisplayClones[j].HasShape        = false;
                                            dc_DisplayClones[j].ShapeCallback   = null;
                                            if (dc_DisplayClones[j].Control.Region != null)
                                            {
                                                dc_DisplayClones[j].Control.Region.Dispose();
                                                dc_DisplayClones[j].Control.Region  = null;
                                            }
                                        }
                                        dc_DisplayClones[j].Control.Invalidate();
                                    }
                                    catch { /* ignore */ }

                                    dc_DisplayClones[j].Control = null;
                                    dc_DisplayClones[j]         = null;
                                    clonesStopped               = true;
                                }
                            }
                        }
                    }

                    if (clonesStopped)
                    {
                        int count = 0;
                        for (int i = 0; i < dc_DisplayClones.Length; i++)
                        {
                            if (dc_DisplayClones[i] != null) count++;
                        }
                        if (count > 0)
                        {
                            Clone[] newDisplayClones = new Clone[count];
                            int index = 0;
                            for (int i = 0; i < dc_DisplayClones.Length; i++)
                            {
                                if (dc_DisplayClones[i] != null) newDisplayClones[index++] = dc_DisplayClones[i];
                            }
                            dc_DisplayClones = newDisplayClones;
                        }
                        else
                        {
                            resume = false;
                            DisplayClones_Stop(true);

                            dc_HasDisplayClones = false;
                            dc_DisplayClones = null;

                            if (dc_RefreshRegion != null)
                            {
                                dc_RefreshRegion.Dispose();
                                dc_RefreshRegion = null;
                            }
                        }
                        _mediaDisplayClonesChanged?.Invoke(this, EventArgs.Empty);
                    }
                    if (resume) DisplayClones_Resume();
                }
                return _lastError;
            }
        }

        // remove the clones that generated an error (because their displays were removed without first removing the clone)
        private void DisplayClones_RemoveDropOutClones()
        {
            if (dc_DisplayClones != null)
            {
                //bool removed = false;
                int count = 0;

                for (int i = 0; i < dc_DisplayClones.Length; i++)
                {
                    if (dc_DisplayClones[i] != null) count++;
                }

                if (count == 0)
                {
                    dc_HasDisplayClones = false;
                    dc_DisplayClones = null;
                    //removed = true;
                }
                else if (count < dc_DisplayClones.Length)
                {
                    Clone[] temp = new Clone[count];
                    int index = 0;

                    for (int i = 0; i < dc_DisplayClones.Length; i++)
                    {
                        if (dc_DisplayClones[i] != null)
                        {
                            temp[index++] = dc_DisplayClones[i];
                        }
                    }
                    dc_DisplayClones = temp;
                    //removed = true;
                }

                //if (removed && MediaDisplayClonesChanged != null) MediaDisplayClonesChanged(this, EventArgs.Empty);
            }
        }

        #endregion


        // ******************************** Display Clones - Start / Stop / Pause / Resume

        #region Display Clones - Start / Stop / Pause / Resume

        internal void DisplayClones_Start()
        {
            lock (dc_Lock)
            {
                if (dc_HasDisplayClones)
                {
                    if (!dc_DisplayClonesRunning)
                    {
                        if (_playing || (_hasOverlay && _overlayHold && dc_CloneOverlayShow))
                        {
                            if (dc_RefreshRegion == null) dc_RefreshRegion = new Region();
                            dc_DisplayClonesRunning = true;
                            DisplayClones_StartTimer();

                            //if (MediaDisplayClonesStarted != null) MediaDisplayClonesStarted(this, EventArgs.Empty);
                        }
                    }
                    else if (_playing && _hasOverlay && _overlayHold && dc_CloneOverlayShow)
                    {
                        for (int i = 0; i < dc_DisplayClones.Length; i++)
                        {
                            if (dc_DisplayClones[i] != null) dc_DisplayClones[i].Refresh = true;
                        }
                    }
                }
            }
        }

        internal void DisplayClones_Stop(bool force)
        {
            lock (dc_Lock)
            {
                if (dc_DisplayClonesRunning)
                {
                    if (force || !_hasOverlay || !(_overlayHold && dc_CloneOverlayShow))
                    {
                        DisplayClones_StopTimer();

                        if (dc_RefreshRegion != null)
                        {
                            dc_RefreshRegion.Dispose();
                            dc_RefreshRegion = null;
                        }
                        if (dc_BackBuffer != null)
                        {
                            dc_BackBuffer.Dispose();
                            dc_BackBuffer = null;
                        }
                        dc_DisplayClonesRunning = false;

                        if (!_displayHold && dc_DisplayClones != null)
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

                        //if (MediaDisplayClonesStopped != null) MediaDisplayClonesStopped(this, EventArgs.Empty);
                        DisplayClones_RemoveDropOutClones();
                    }
                }
            }
        }

        private bool DisplayClones_Pause()
        {
            bool paused = false;
            if (dc_DisplayClonesRunning)
            {
                if (dc_Timer != null)
                {
                    dc_TimerRestart = false;
                    int timeOut = DC_BUSY_TIME_OUT;
                    while (dc_PaintBusy && --timeOut > 0)
                    {
                        Thread.Sleep(1);
                        Application.DoEvents();
                    }
                    paused = true;
                }
            }
            return paused;
        }

        private void DisplayClones_Resume()
        {
            if (dc_DisplayClonesRunning)
            {
                dc_TimerRestart = true;
                dc_Timer.Change(dc_TimerInterval, Timeout.Infinite);
            }
        }

        #endregion


        // ******************************** Display Clones - Timer Start / Stop

        #region Display Clones - Timer Start / Stop

        private void DisplayClones_StartTimer()
        {
            if (dc_Timer == null)
            {
                if (dc_RefreshCallback == null) dc_RefreshCallback = new RefreshCloneCallback(DisplayClones_Invalidate);
                dc_TimerRestart = true;
                dc_Timer = new System.Threading.Timer(DisplayClones_Paint, null, 0, Timeout.Infinite);
            }
        }

        private void DisplayClones_StopTimer()
        {
            if (dc_Timer != null)
            {
                dc_TimerRestart = false;
                dc_Timer.Dispose();
                dc_Timer = null;

                int timeOut = DC_BUSY_TIME_OUT;
                while (dc_PaintBusy && --timeOut > 0)
                {
                    Thread.Sleep(1);
                    Application.DoEvents();
                }
            }
        }

        #endregion


        // ******************************** Display Clones - SizeChanged / Refresh / Update / UpdateShape

        #region Display Clones - SizeChanged / Refresh / Update / UpdateShape

        // also used with clone invalidated (to handle fully 'overlapped' forms)
        private void DisplayClones_SizeChanged(object sender, EventArgs e)
        {
            // don't do this:
            //if (dc_PaintBusy) return;

            if (dc_HasDisplayClones)
            {
                for (int i = 0; i < dc_DisplayClones.Length; i++)
                {
                    if (dc_DisplayClones[i] != null && dc_DisplayClones[i].Control == sender)
                    {
                        dc_DisplayClones[i].Refresh = true;
                        if (dc_DisplayClones[i].HasShape) DisplayClones_UpdateShape(dc_DisplayClones[i]);
                        break;
                    }
                }
            }
        }

        internal void DisplayClones_Refresh()
        {
            if (dc_HasDisplayClones)
            {
                for (int i = 0; i < dc_DisplayClones.Length; i++)
                {
                    if (dc_DisplayClones[i] != null && dc_DisplayClones[i].Control != null)
                    {
                        dc_DisplayClones[i].Refresh = true;
                        if (dc_DisplayClones[i].HasShape) DisplayClones_UpdateShape(dc_DisplayClones[i]);
                    }
                }
            }
        }

        //internal void DisplayClones_Update()
        //{
        //    if (dc_HasDisplayClones)
        //    {
        //        Region      wipe    = null;
        //        Rectangle   bounds;

        //        for (int i = 0; i < dc_DisplayClones.Length; i++)
        //        {
        //            try
        //            {
        //                if (dc_DisplayClones[i] != null && dc_DisplayClones[i].Control != null)
        //                {
        //                    dc_DisplayClones[i].Refresh = true;
        //                    //dc_DisplayClones[i].Control.Invalidate();

        //                    wipe    = new Region(dc_DisplayClones[i].Control.DisplayRectangle);
        //                    bounds  = Rectangle.Empty;

        //                    if ((!_hasVideo && !(_hasOverlay && _overlayHold)) || _displayMode == DisplayMode.Stretch || dc_DisplayClones[i].Layout == CloneLayout.Stretch || dc_DisplayClones[i].Layout == CloneLayout.Cover)// || (_base._hasOverlay && _base._overlayMode == OverlayMode.Display))
        //                    {
        //                        bounds = dc_DisplayClones[i].Control.DisplayRectangle;
        //                    }
        //                    else
        //                    {
        //                        int newSize;
        //                        Rectangle sourceRect = _videoBoundsClip;
        //                        Rectangle destRect = dc_DisplayClones[i].Control.DisplayRectangle;

        //                        double difX = (double)destRect.Width / sourceRect.Width;
        //                        double difY = (double)destRect.Height / sourceRect.Height;

        //                        if (difX < difY)
        //                        {
        //                            newSize = (int)(sourceRect.Height * difX);

        //                            bounds.X = 0;
        //                            bounds.Y = (destRect.Height - newSize) / 2;
        //                            bounds.Width = (int)(sourceRect.Width * difX);
        //                            bounds.Height = newSize;
        //                        }
        //                        else
        //                        {
        //                            newSize = (int)(sourceRect.Width * difY);

        //                            bounds.X = (destRect.Width - newSize) / 2;
        //                            bounds.Y = 0;
        //                            bounds.Width = newSize;
        //                            bounds.Height = (int)(sourceRect.Height * difY);
        //                        }
        //                    }

        //                    //wipe.Xor(_clonesClass.GetVideoBounds(dc_DisplayClones[i].Control));
        //                    wipe.Xor(bounds);
        //                    dc_DisplayClones[i].Control.Invalidate(wipe);
        //                    wipe.Dispose(); wipe = null;

        //                    if (dc_DisplayClones[i].HasShape) DisplayClones_UpdateShape(dc_DisplayClones[i]);
        //                }
        //            }
        //            catch
        //            {
        //                if (wipe != null)
        //                {
        //                    wipe.Dispose();
        //                    wipe = null;
        //                }
        //            }
        //        }
        //    }
        //}

        internal void DisplayClones_UpdateShape(Clone clone)
        {
            //if (clone != null && clone.Control != null && clone.HasShape)
            {
                Rectangle bounds;
                if (clone.HasVideoShape && _clonesClass != null) bounds = _clonesClass.GetVideoBounds(clone.Control);
                else bounds = clone.Control.ClientRectangle;
                try
                {
                    Region update = clone.ShapeCallback(bounds);

                    if (update != null)
                    {
                        if (clone.Control.Region != null) clone.Control.Region.Dispose();
                        clone.Control.Region = update;
                    }
                }
                catch { /* ignore */ }
            }
        }

        #endregion


        // ******************************** Display Clones - Drag

        #region Display Clones - Drag

        internal void DisplayClones_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !dc_IsDragging)
            {
                Control clone = (Control)sender;

                dc_DragForm = clone.FindForm();
                if (dc_DragForm.WindowState != FormWindowState.Maximized)
                {
                    dc_IsDragging = true;

                    dc_OldLocation = clone.PointToScreen(e.Location);
                    clone.MouseMove += DisplayClones_MouseMove;
                    clone.MouseUp += DisplayClones_MouseUp;

                    dc_OldCursor = clone.Cursor;
                    for (int i = 0; i < dc_DisplayClones.Length; i++)
                    {
                        if (dc_DisplayClones[i] != null && dc_DisplayClones[i].Control == clone)
                        {
                            clone.Cursor = dc_DisplayClones[i].DragCursor;
                            break;
                        }
                    }
                }
                else { dc_DragForm = null; }
            }
        }

        private void DisplayClones_MouseMove(object sender, MouseEventArgs e)
        {
            if (dc_IsDragging)
            {
                Point location = ((Control)sender).PointToScreen(e.Location);

                dc_DragForm.Left += location.X - dc_OldLocation.X;
                dc_DragForm.Top += location.Y - dc_OldLocation.Y;
                dc_OldLocation = location;
            }
        }

        private void DisplayClones_MouseUp(object sender, MouseEventArgs e)
        {
            if (dc_IsDragging)
            {
                Control clone = (Control)sender;
                clone.MouseMove -= DisplayClones_MouseMove;
                clone.MouseUp -= DisplayClones_MouseUp;
                dc_DragForm = null;

                clone.Cursor = dc_OldCursor;

                dc_IsDragging = false;
            }
        }

        #endregion


        // ******************************** Display Clones - Paint

        #region Display Clones - Paint

        // Paint the display clones
        private void DisplayClones_Paint(object state)
        {
            if (dc_PaintBusy || !_hasDisplay || !dc_TimerRestart) return;
            dc_PaintBusy = true;

            // Player display too small check
            #region Player display too small check

            try
            {
                if (_display.DisplayRectangle.Width < 4 || _display.DisplayRectangle.Height < 4)
                {
                    if (!dc_NoSizeDisplay)
                    {
                        dc_NoSizeDisplay = true;
                        for (int i = 0; i < dc_DisplayClones.Length; i++)
                        {
                            if (dc_DisplayClones[i] != null)
                            {
                                dc_RefreshRegion.MakeEmpty();
                                dc_RefreshRegion.Union(dc_DisplayClones[i].Control.DisplayRectangle);
                                dc_DisplayClones[i].Control.Invoke(dc_RefreshCallback, new object[] { i });
                                dc_DisplayClones[i].Refresh = false;
                            }
                        }
                    }
                    if (dc_TimerRestart) dc_Timer.Change(dc_TimerInterval, Timeout.Infinite);
                    dc_PaintBusy = false;
                    return;
                }
                dc_NoSizeDisplay = false;
            }
            catch
            {
                if (dc_TimerRestart) dc_Timer.Change(dc_TimerInterval, Timeout.Infinite);
                dc_PaintBusy = false;
                return;
            }

            #endregion

            // Determine whether also to paint the display overlay
            bool overlayMode = dc_CloneOverlayShow && (_hasOverlay && _overlay.Visible);

            // Paint only if there's a video image or a display overlay (hold)
            if (_hasVideo || overlayMode)
            {
                double      difX;
                double      difY;
                int         newSize;
                bool        fullSize        = false;

                IntPtr      sourceHdc       = IntPtr.Zero;
                Graphics    sourceGraphics  = null;
                Rectangle   sourceRect;

                Graphics    tempGraphics    = null;
                IntPtr      tempHdc         = IntPtr.Zero;

                // Get source - video and/or overlay
                #region Get source - video and/or overlay

                if (overlayMode)
                {
                    try
                    {
                        int transparentColor = ColorTranslator.ToWin32(_overlay.TransparencyKey);

                        // create a 'screencopy' in backBuffer
                        if (!_hasVideo || _overlayMode == OverlayMode.Display)
                        {
                            sourceRect = _display.DisplayRectangle; // with overlay - same size as display
                        }
                        else
                        {
                            sourceRect = _videoBoundsClip; // with overlay - same size as video
                            if (sourceRect.Width <= 2 || sourceRect.Height <= 2)
                            {
                                sourceRect  = _display.DisplayRectangle;
                                fullSize    = true;
                            }
                        }

                        // create buffer
                        if (dc_BackBuffer == null || sourceRect.Width != dc_BackBuffer.Width || sourceRect.Height != dc_BackBuffer.Height)
                        {
                            if (dc_BackBuffer != null) dc_BackBuffer.Dispose();
                            dc_BackBuffer = new Bitmap(sourceRect.Width, sourceRect.Height);
                        }
                        sourceGraphics  = Graphics.FromImage(dc_BackBuffer);
                        sourceHdc       = sourceGraphics.GetHdc();

                        // copy display to buffer
                        tempGraphics    = _display.CreateGraphics();
                        tempHdc         = tempGraphics.GetHdc();
                        SafeNativeMethods.BitBlt(sourceHdc, 0, 0, sourceRect.Width, sourceRect.Height, tempHdc, sourceRect.X, sourceRect.Y, SafeNativeMethods.SRCCOPY);
                        tempGraphics.ReleaseHdc(tempHdc);
                        tempGraphics.Dispose();
                        tempGraphics    = null;

                        // copy overlay to buffer - transparent + opacity
                        tempGraphics    = _overlay.CreateGraphics();
                        tempHdc         = tempGraphics.GetHdc();

                        if (_overlay.Opacity == 1 || _overlayBlend == OverlayBlend.None)
                        {
                            SafeNativeMethods.TransparentBlt(sourceHdc, 0, 0, sourceRect.Width, sourceRect.Height, tempHdc, 0, 0, _overlay.Width, _overlay.Height, transparentColor);
                        }
                        else
                        {
                            // can't get AlphaBlend to work as form opacity

                            //Bitmap backBuffer = new Bitmap(sourceRect.Width, sourceRect.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                            //Graphics g = Graphics.FromImage(backBuffer);

                            ////SolidBrush b = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
                            ////g.FillRectangle(b, 0, 0, 200, 200);
                            ////b.Dispose();

                            //IntPtr gHdc = g.GetHdc();

                            ////transparentColor = ColorTranslator.ToWin32(Color.Black);
                            //SafeNativeMethods.TransparentBlt(gHdc, 0, 0, sourceRect.Width, sourceRect.Height, tempHdc, 0, 0, _overlay.Width, _overlay.Height, transparentColor);
                            ////SafeNativeMethods.BitBlt(gHdc, 0, 0, sourceRect.Width, sourceRect.Height, tempHdc, 0, 0, SafeNativeMethods.SRCCOPY);
                            ////backBuffer.MakeTransparent(backBuffer.GetPixel(1,1));

                            ////Color c = backBuffer.GetPixel(1, 1);
                            ////MessageBox.Show(c.ToString());

                            ////g.ReleaseHdc(gHdc);
                            ////SolidBrush b2 = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
                            ////g.FillRectangle(b2, 0, 0, 100, 100);
                            ////b2.Dispose();
                            ////gHdc = g.GetHdc();

                            //dc_BlendFunction.SourceConstantAlpha = (byte)(_overlay.Opacity * 0xFF);
                            //dc_BlendFunction.AlphaFormat = 0;
                            //SafeNativeMethods.AlphaBlend(sourceHdc, 0, 0, sourceRect.Width, sourceRect.Height, gHdc, 0, 0, _overlay.Width, _overlay.Height, dc_BlendFunction);

                            //g.ReleaseHdc(gHdc);
                            //g.Dispose();

                            //backBuffer.Dispose();

                            _blendFunction.SourceConstantAlpha = (byte)(_overlay.Opacity * 0xFF);
                            SafeNativeMethods.AlphaBlend(sourceHdc, 0, 0, sourceRect.Width, sourceRect.Height, tempHdc, 0, 0, _overlay.Width, _overlay.Height, _blendFunction);
                        }

                        tempGraphics.ReleaseHdc(tempHdc);
                        tempGraphics.Dispose();
                        tempGraphics = null;

                        sourceRect.X = 0;
                        sourceRect.Y = 0; // backBuffer is source, no margins
                    }
                    catch
                    {
                        if (tempGraphics != null)
                        {
                            if (tempHdc != IntPtr.Zero) tempGraphics.ReleaseHdc(tempHdc);
                            tempGraphics.Dispose();
                            //tempGraphics = null;
                        }
                        if (sourceGraphics != null)
                        {
                            if (sourceHdc != IntPtr.Zero) sourceGraphics.ReleaseHdc(sourceHdc);
                            sourceGraphics.Dispose();
                            sourceGraphics = null;
                        }

                        try
                        {
                            sourceGraphics  = _display.CreateGraphics(); // player display is source
                            sourceHdc       = sourceGraphics.GetHdc();
                            sourceRect      = _videoBoundsClip;
                        }
                        catch
                        {
                            if (sourceGraphics != null)
                            {
                                if (sourceHdc != IntPtr.Zero) sourceGraphics.ReleaseHdc(sourceHdc);
                                sourceGraphics.Dispose();
                            }
                            if (dc_TimerRestart) dc_Timer.Change(dc_TimerInterval, Timeout.Infinite);
                            dc_PaintBusy = false;
                            return;
                        }
                    }
                }
                else
                {
                    try
                    {
                        sourceGraphics  = _display.CreateGraphics(); // player display is source
                        sourceHdc       = sourceGraphics.GetHdc();
                        sourceRect      = _videoBoundsClip;
                    }
                    catch
                    {
                        if (sourceGraphics != null)
                        {
                            if (sourceHdc != IntPtr.Zero) sourceGraphics.ReleaseHdc(sourceHdc);
                            sourceGraphics.Dispose();
                        }
                        if (dc_TimerRestart) dc_Timer.Change(dc_TimerInterval, Timeout.Infinite);
                        dc_PaintBusy = false;
                        return;
                    }
                }

                #endregion

                // Paint the clones - one by one
                #region Paint the clones - one by one

                for (int i = 0; i < dc_DisplayClones.Length && dc_TimerRestart; i++)
                {
                    Graphics    destGraphics = null;
                    Rectangle   destRect;
                    IntPtr      destHdc      = IntPtr.Zero;
                    CloneFlip   flipMode;

                    try
                    {
                        if (dc_DisplayClones[i] != null && dc_DisplayClones[i].Control.Visible)
                        {
                            flipMode        = dc_DisplayClones[i].Flip;
                            destGraphics    = dc_DisplayClones[i].Control.CreateGraphics();
                            destRect        = dc_DisplayClones[i].Control.DisplayRectangle;
                            destHdc         = destGraphics.GetHdc();

                            if (_displayMode == DisplayMode.Stretch || fullSize || dc_DisplayClones[i].Layout == CloneLayout.Stretch)
                            {
                                dc_DisplayClones[i].Refresh = false;

                                // set quality
                                if (dc_DisplayClones[i].Quality != CloneQuality.Normal)
                                {
                                    if (dc_DisplayClones[i].Quality == CloneQuality.Auto)
                                    {
                                        if (destRect.Width < sourceRect.Width || destRect.Height < sourceRect.Height)
                                            SafeNativeMethods.SetStretchBltMode(destHdc, SafeNativeMethods.STRETCH_HALFTONE);
                                    }
                                    else SafeNativeMethods.SetStretchBltMode(destHdc, SafeNativeMethods.STRETCH_HALFTONE);
                                }

                                if (flipMode == CloneFlip.FlipNone)
                                {
                                    SafeNativeMethods.StretchBlt(destHdc, destRect.X, destRect.Y, destRect.Width, destRect.Height,
                                        sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                                }
                                else if (flipMode == CloneFlip.FlipX)
                                {
                                    SafeNativeMethods.StretchBlt(destHdc, destRect.X + destRect.Width - 1, destRect.Y, -destRect.Width, destRect.Height,
                                        sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                                }
                                else if (flipMode == CloneFlip.FlipY)
                                {
                                    SafeNativeMethods.StretchBlt(destHdc, destRect.X, destRect.Y + destRect.Height - 1, destRect.Width, -destRect.Height,
                                        sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                                }
                                else //if (flip == FlipType.FlipXY)
                                {
                                    SafeNativeMethods.StretchBlt(destHdc, destRect.X + destRect.Width - 1, destRect.Y + destRect.Height - 1, -destRect.Width, -destRect.Height,
                                        sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                                }
                            }
                            else
                            {
                                difX = (double)destRect.Width / sourceRect.Width;
                                difY = (double)destRect.Height / sourceRect.Height;

                                if ((dc_DisplayClones[i].Layout == CloneLayout.Zoom && (difX < difY)) || (dc_DisplayClones[i].Layout == CloneLayout.Cover && (difX > difY)))
                                //if (difX < difY)
                                {
                                    newSize                 = (int)(sourceRect.Height * difX);
                                    dc_RefreshRect.X        = 0;
                                    dc_RefreshRect.Y        = (destRect.Height - newSize) / 2;
                                    dc_RefreshRect.Width    = (int)(sourceRect.Width * difX);
                                    dc_RefreshRect.Height   = newSize;

                                    if (dc_DisplayClones[i].Refresh)
                                    {
                                        dc_DisplayClones[i].Refresh = false;

                                        dc_RefreshRegion.MakeEmpty();
                                        dc_RefreshRegion.Union(destRect);
                                        dc_RefreshRegion.Exclude(dc_RefreshRect);

                                        dc_DisplayClones[i].Control.Invoke(dc_RefreshCallback, new object[] { i });
                                    }

                                    // set quality
                                    if (dc_DisplayClones[i].Quality != CloneQuality.Normal)
                                    {
                                        if (dc_DisplayClones[i].Quality == CloneQuality.Auto)
                                        {
                                            if (dc_RefreshRect.Width < sourceRect.Width || dc_RefreshRect.Height < sourceRect.Height)
                                                SafeNativeMethods.SetStretchBltMode(destHdc, SafeNativeMethods.STRETCH_HALFTONE);
                                        }
                                        else SafeNativeMethods.SetStretchBltMode(destHdc, SafeNativeMethods.STRETCH_HALFTONE);
                                    }

                                    if (flipMode == CloneFlip.FlipNone)
                                    {
                                        SafeNativeMethods.StretchBlt(destHdc, 0, dc_RefreshRect.Y, dc_RefreshRect.Width, newSize,
                                            sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                                    }
                                    else if (flipMode == CloneFlip.FlipX)
                                    {
                                        SafeNativeMethods.StretchBlt(destHdc, dc_RefreshRect.Width, dc_RefreshRect.Y, -dc_RefreshRect.Width, newSize,
                                            sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                                    }
                                    else if (flipMode == CloneFlip.FlipY)
                                    {
                                        SafeNativeMethods.StretchBlt(destHdc, 0, dc_RefreshRect.Y + newSize - 1, dc_RefreshRect.Width, -newSize,
                                            sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                                    }
                                    else //if (flip == FlipType.FlipXY)
                                    {
                                        SafeNativeMethods.StretchBlt(destHdc, dc_RefreshRect.Width, dc_RefreshRect.Y + newSize - 1, -dc_RefreshRect.Width, -newSize,
                                            sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                                    }
                                }
                                else
                                {
                                    newSize                 = (int)(sourceRect.Width * difY);
                                    dc_RefreshRect.X        = (destRect.Width - newSize) / 2;
                                    dc_RefreshRect.Y        = 0;
                                    dc_RefreshRect.Width    = newSize;
                                    dc_RefreshRect.Height   = (int)(sourceRect.Height * difY);

                                    if (dc_DisplayClones[i].Refresh)
                                    {
                                        dc_DisplayClones[i].Refresh = false;
                                        dc_RefreshRegion.MakeEmpty();
                                        dc_RefreshRegion.Union(destRect);
                                        dc_RefreshRegion.Exclude(dc_RefreshRect);

                                        dc_DisplayClones[i].Control.Invoke(dc_RefreshCallback, new object[] { i });
                                    }

                                    // set quality
                                    if (dc_DisplayClones[i].Quality != CloneQuality.Normal)
                                    {
                                        if (dc_DisplayClones[i].Quality == CloneQuality.Auto)
                                        {
                                            if (dc_RefreshRect.Width < sourceRect.Width || dc_RefreshRect.Height < sourceRect.Height)
                                                SafeNativeMethods.SetStretchBltMode(destHdc, SafeNativeMethods.STRETCH_HALFTONE);
                                        }
                                        else SafeNativeMethods.SetStretchBltMode(destHdc, SafeNativeMethods.STRETCH_HALFTONE);
                                    }

                                    if (flipMode == CloneFlip.FlipNone)
                                    {
                                        SafeNativeMethods.StretchBlt(destHdc, dc_RefreshRect.X, 0, newSize, dc_RefreshRect.Height,
                                            sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                                    }
                                    else if (flipMode == CloneFlip.FlipX)
                                    {
                                        SafeNativeMethods.StretchBlt(destHdc, dc_RefreshRect.X + newSize - 1, 0, -newSize, dc_RefreshRect.Height,
                                            sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                                    }
                                    else if (flipMode == CloneFlip.FlipY)
                                    {
                                        SafeNativeMethods.StretchBlt(destHdc, dc_RefreshRect.X, dc_RefreshRect.Height - 1, newSize, -dc_RefreshRect.Height,
                                            sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                                    }
                                    else //if (flip == FlipType.FlipXY)
                                    {
                                        SafeNativeMethods.StretchBlt(destHdc, dc_RefreshRect.X + newSize - 1, dc_RefreshRect.Height - 1, -newSize, -dc_RefreshRect.Height,
                                            sourceHdc, sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, SafeNativeMethods.SRCCOPY_U);
                                    }
                                }
                            }
                            dc_DisplayClones[i].Errors = 0;
                        }
                    }
                    catch
                    {
                        if (dc_DisplayClones != null && i < dc_DisplayClones.Length && dc_DisplayClones[i] != null)
                        {
                            if (++dc_DisplayClones[i].Errors > 5)
                            {
                                dc_DisplayClones[i].Control = null;
                                dc_DisplayClones[i] = null;
                            }
                        }
                    }

                    if (destGraphics != null)
                    {
                        if (destHdc != IntPtr.Zero) destGraphics.ReleaseHdc(destHdc);
                        destGraphics.Dispose();
                    }
                }

                #endregion

                // Release source items
                #region Release source items

                sourceGraphics.ReleaseHdc(sourceHdc);
                sourceGraphics.Dispose();

                #endregion

            }
            if (dc_TimerRestart) dc_Timer.Change(dc_TimerInterval, Timeout.Infinite);
            dc_PaintBusy = false;
        }

        // Invoke / Callback from DisplayClonesTimer_Elapsed
        private void DisplayClones_Invalidate(int index)
        {
            try
            {
                dc_DisplayClones[index].Control.Invalidate(dc_RefreshRegion);
            }
            catch { /* ignore */ }
        }

        #endregion

    }
}
