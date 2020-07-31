using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PlexDL.Player
{
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
        /// Converts the specified screen location to coordinates of the player's display window.
        /// </summary>
        /// <param name="p">The screen coordinate to convert.</param>
        public Point Display(Point p)
        {
            if (_base._hasDisplay && _base._display.Visible)
            {
                _base._lastError = Player.NO_ERROR;
                return _base._display.PointToClient(p);
            }
            _base._lastError = HResult.MF_E_NOT_AVAILABLE;
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
            _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return new Point(-1, -1);
        }

        /// <summary>
        /// Converts the specified screen location to coordinates of the video image on the player's display window.
        /// </summary>
        /// <param name="p">The screen coordinate to convert.</param>
        public Point Video(Point p)
        {
            Point newP = new Point(-1, -1);
            _base._lastError = HResult.MF_E_NOT_AVAILABLE;

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
            return PlexDL.Player.SliderValue.FromPoint(slider, location.X, location.Y);
        }

        /// <summary>
        /// Returns the slider value at the specified location on the specified slider (trackbar).
        /// </summary>
        /// <param name="slider">The slider whose value should be obtained.</param>
        /// <param name="x">The relative x-coordinate on the slider (for horizontal oriented sliders).</param>
        /// <param name="y">The relative y-coordinate on the slider (for vertical oriented sliders).</param>
        public int SliderValue(TrackBar slider, int x, int y)
        {
            return PlexDL.Player.SliderValue.FromPoint(slider, x, y);
        }
    }
}