using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Display Overlay methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Overlay : HideObjectMembers
    {
        #region Fields (Overlay Class)

        private const int   NO_ERROR = 0;

        private Player      _base;

        #endregion

        internal Overlay(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets the player's display overlay.
        /// </summary>
        public Form Window
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._overlay;
            }
            set { _base.AV_SetOverlay(value); }
        }

        /// <summary>
        /// Sets the specified form as the player's display overlay.
        /// <br/>Same as Player.Overlay.Window = form.
        /// </summary>
        /// <param name="form">The form to be set as the player's display overlay.</param>
        public void Set(Form form)
        {
            _base.AV_SetOverlay(form);
        }

        /// <summary>
        /// Removes the display overlay from the player.
        /// <br/>Same as Player.Overlay.Window = null.
        /// </summary>
        public void Remove()
        {
            _base.AV_RemoveOverlay(true);
        }

        /// <summary>
        /// Gets or sets the display mode (video or display size) of the player's display overlay (default: OverlayMode.Video).
        /// </summary>
        public OverlayMode Mode
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._overlayMode;
            }
            set
            {
                //_base._lastError = NO_ERROR;
                if (value != _base._overlayMode)
                {
                    _base._overlayMode = value;
                    if (_base._hasDisplay && _base._hasOverlayShown)
                    {
                        _base._display.Invalidate();
                        if (_base._hasOverlayClipping) _base.AV_ClipOverlay();
                    }
                    _base._mediaOverlayModeChanged?.Invoke(_base, EventArgs.Empty);
                }
                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the player's display overlay is always shown (default: false).
        /// <br/>By default, a display overlay is only shown when media is playing. 
        /// </summary>
        public bool Hold
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._overlayHold;
            }
            set
            {
                //_base._lastError = NO_ERROR;
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
                            bool tempHold = _base._displayHold;
                            _base._displayHold = false;
                            _base.AV_HideOverlay();
                            //if (_base.dc_HasDisplayClones && !_base._playing) _base.DisplayClones_Stop(false);
                            _base._displayHold = tempHold;
                        }
                    }
                    _base._mediaOverlayHoldChanged?.Invoke(_base, EventArgs.Empty);
                }
                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player's display overlay can be
        /// <br/>activated for input and selection (default: false).
        /// </summary>
        public bool CanFocus
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._overlayCanFocus;
            }
            set
            {
                if (value != _base._overlayCanFocus) _base.AV_SetOverlayCanFocus(value);
            }
        }

        /// <summary>
        /// Gets or sets the number of milliseconds that the visibilty of the player's display overlay is delayed
        /// <br/>when restoring the player's minimized display window (default: 200 ms).
        /// <br/>A value of 0 (zero) disables the delay.
        /// </summary>
        public int Delay
        {
            get
            {
                _base._lastError = NO_ERROR;
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
        /// Gets a value indicating whether the player's display overlay is active.
        /// <br/>See also: Player.Overlay.Present and Player.Overlay.Visible.
        /// </summary>
        public bool Active
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasOverlayShown;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has a display overlay (set, but not necessarily active or visible).
        /// <br/>See also: Player.Overlay.Active and Player.Overlay.Visible.
        /// </summary>
        public bool Present
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasOverlay;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player's display overlay is active and visible.
        /// <br/>See also: Player.Overlay.Active and Player.Overlay.Present.
        /// </summary>
        public bool Visible
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasOverlay && _base._overlay.Visible;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether clipping of the player's display overlay is enabled (default: false).
        /// <br/>The overlay is clipped when it protrudes outside the parent form of the player's display window..
        /// </summary>
        public bool Clipping
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasOverlayClipping;
            }
            set
            {
                _base._lastError = NO_ERROR;
                if (value != _base._hasOverlayClipping)
                {
                    _base.AV_SetOverlayClipping(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the opacity of display overlays when displayed on
        /// <br/>screen copies and display clones (default: OverlayBlend.None).
        /// </summary>
        public OverlayBlend Blend
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._overlayBlend;
            }
            set
            {
                _base._lastError = NO_ERROR;
                _base._blendFunction.AlphaFormat = value == OverlayBlend.Transparent ? SafeNativeMethods.AC_SRC_ALPHA : SafeNativeMethods.AC_SRC_OVER;
                _base._overlayBlend = value;
            }
        }

        /// <summary>
        /// Gets the size and location (in pixels) of the display overlay window
        /// <br/>relative to the player's display window.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                Rectangle bounds;

                _base._lastError = NO_ERROR;
                if (_base._hasOverlayShown)
                {
                    if (_base._hasVideo && _base._overlayMode == OverlayMode.Video) bounds = _base._videoBounds;
                    else bounds = new Rectangle(Point.Empty, _base._display.Size);
                }
                else bounds = Rectangle.Empty;

                return bounds;
            }
        }

    }
}