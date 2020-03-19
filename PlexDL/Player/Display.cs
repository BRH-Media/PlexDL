using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PlexDL.Player
{
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
            set { SetShape(value, _base._hasVideoShape, _base._hasOverlayClipping); }
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
}