using System;
using System.ComponentModel;
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

        private Player _base;

        #endregion Fields (Overlay Class)

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
                _base._lastError = Player.NO_ERROR;
                return _base._overlay;
            }
            set { _base.AV_SetOverlay(value); }
        }

        /// <summary>
        /// Removes the display overlay from the player. Same as Player.Overlay.Window = null.
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
        /// Gets or sets a value that indicates whether the player's display overlay is always shown (default: false).
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
        /// Gets or sets a value that indicates whether the player's display overlay can be activated for input and selection (default: false).
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
        /// Gets or sets the number of milliseconds that the visibilty of the player's display overlay is delayed when restoring the player's minimized display window (form). Set to 0 to disable (default: 200 ms).
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
        /// Gets a value that indicates whether the player's display overlay is active.
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
        /// Gets a value that indicates whether the player has a display overlay (set, but not necessarily active or visible).
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
        /// Gets a value that indicates whether the player's display overlay is active and visible.
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
        /// Gets or sets a value that indicates whether clipping of the player's display overlay is enabled. The overlay is clipped when it protrudes outside the parent form of the player's display window (default: false).
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
        /// Gets or sets a value that indicates the opacity of display overlays displayed on screen copies and player display clones. May require specially designed display overlays (default: OverlayBlend.None).
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
}