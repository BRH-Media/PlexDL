using System;
using System.ComponentModel;
using System.Drawing;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Video Overlay methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class VideoOverlay : HideObjectMembers
    {
        #region Fields (VideoOverlay Class)

        private Player _base;

        #endregion

        internal VideoOverlay(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Sets the player's video bitmap overlay, an image that is alpha-blended with the video image displayed by the player.
        /// </summary>
        /// <param name="image">The image to use as the video bitmap overlay of the player.</param>
        /// <param name="placement">Specifies the relative location and size of the bitmap overlay.</param>
        /// <param name="transparencyKey">Source color key.
        /// <br/>Any pixels in the bitmap overlay that match the color key are rendered as transparent pixels.
        /// <br/>Color.LightGray indicates the existing transparency of the image.
        /// <br/>Color.Empty leaves this setting unchanged.</param>
        /// <param name="opacity">Alpha blending value. The opacity level of the bitmap overlay.
        /// <br/>Values from 0.0 (transparent) to 1.0 (opaque), inclusive.
        /// <br/>A value of -1 leaves this setting unchanged.</param>
        public int Set(Image image, ImagePlacement placement, Color transparencyKey, float opacity)
        {
            if (placement == ImagePlacement.Custom) _base._lastError = HResult.E_INVALIDARG;
            else _base.AV_SetVideoOverlay(image, placement, RectangleF.Empty, transparencyKey, opacity, true);
            return (int)_base._lastError;
        }

        /// <summary>
        /// Sets the player's video bitmap overlay, an image that is alpha-blended with the video image displayed by the player.
        /// </summary>
        /// <param name="image">The image to use as the video bitmap overlay of the player.</param>
        /// <param name="placement">Specifies the relative location and size of the bitmap overlay.</param>
        /// <param name="bounds">Specifies the relative location and size of the bitmap overlay if the placement parameter is set to ImagePlacement.Custom.
        /// <br/>The value is a normalized rectangle, the entire video image is indicated by {0, 0, 1, 1}.
        /// <br/>RectangleF.Empty leaves this setting unchanged.</param>
        /// <param name="transparencyKey">Source color key.
        /// <br/>Any pixels in the bitmap overlay that match the color key are rendered as transparent pixels.
        /// <br/>Color.LightGray indicates the existing transparency of the image.
        /// <br/>Color.Empty leaves this setting unchanged.</param>
        /// <param name="opacity">Alpha blending value. The opacity level of the bitmap overlay.
        /// <br/>Values from 0.0 (transparent) to 1.0 (opaque), inclusive.
        /// <br/>A value of -1 leaves this setting unchanged.</param>
        /// <param name="hold">A value that indicates whether the bitmap overlay is used with all subsequent videos until the setting is changed
        /// <br/>or removed (value = true), or only with the current or next video (value = false).</param>
        public int Set(Image image, ImagePlacement placement, RectangleF bounds, Color transparencyKey, float opacity, bool hold)
        {
            _base.AV_SetVideoOverlay(image, placement, bounds, transparencyKey, opacity, hold);
            return (int)_base._lastError;
        }

        /// <summary>
        /// Sets the size and margins of the video bitmap overlay corner presets (such as ImagePlacement.TopLeftSmall).
        /// </summary>
        /// <param name="smallSize">Sets the relative size of the video bitmap overlay for the small size presets.
        /// <br/>Values greater than zero or -1 to leave this setting unchanged (default: 0.10).</param>
        /// <param name="mediumSize">Sets the relative size to the video image for the medium size presets.
        /// <br/>Values greater than zero or -1 to leave this setting unchanged (default: 0.15).</param>
        /// <param name="largeSize">Sets the relative size to the video image for the large size presets.
        /// <br/>Values greater than zero or -1 to leave this setting unchanged (default: 0.20).</param>
        /// <param name="horizontalMargins">Sets the size (in pixels) of the horizontal (left and right) margins between the bitmap overlay and the edge of the video image.
        /// <br/>Values zero or greater or -1 to leave this setting unchanged (default: 8).</param>
        /// <param name="verticalMargins">Sets the size (in pixels) of the vertical (top and bottom) margins between the bitmap overlay and the edge of the video image.
        /// <br/>Values zero or greater or -1 to leave this setting unchanged (default: 8).</param>
        public int SetPresets(float smallSize, float mediumSize, float largeSize, int horizontalMargins, int verticalMargins)
        {
            if ((smallSize == -1 || smallSize > 0.01f) && (mediumSize == -1 || mediumSize > 0.01f) && (largeSize == -1 || largeSize > 0.01f)) // negative margins (except -1) possible
            {
                if (smallSize != -1) _base._IMAGE_OVERLAY_SMALL = smallSize;
                if (mediumSize != -1) _base._IMAGE_OVERLAY_MEDIUM = mediumSize;
                if (largeSize != -1) _base._IMAGE_OVERLAY_LARGE = largeSize;

                if (horizontalMargins != -1) _base._IMAGE_OVERLAY_MARGIN_HORIZONTAL = horizontalMargins;
                if (verticalMargins != -1) _base._IMAGE_OVERLAY_MARGIN_VERTICAL = verticalMargins;

                _base.AV_ShowVideoOverlay();
                _base._lastError = Player.NO_ERROR;
            }
            else _base._lastError = HResult.E_INVALIDARG;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Updates the player's video bitmap overlay settings.
        /// </summary>
        /// <param name="bounds">Specifies the relative location and size of the video bitmap overlay and sets the image placement setting to ImagePlacement.Custom.
        /// <br/>The location and size of the video image is indicated by {0, 0, 1, 1}.
        /// <br/>RectangleF.Empty leaves this setting unchanged.</param>
        /// <param name="transparencyKey">Source color key.
        /// <br/>Any pixels in the video bitmap overlay that match the color key are rendered as transparent pixels.
        /// <br/>Color.LightGray indicates the existing transparency of the image.
        /// <br/>Color.Empty leaves this setting unchanged.</param>
        /// <param name="opacity">Alpha blending value. The opacity level of the video bitmap overlay.
        /// <br/>Values from 0.0 (transparent) to 1.0 (opaque), inclusive.
        /// <br/>A value of -1 leaves this setting unchanged.</param>
        public int Update(RectangleF bounds, Color transparencyKey, float opacity)
        {
            _base.AV_UpdateVideoOverlay(bounds, transparencyKey, opacity);
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes the player's video bitmap overlay and cleans up any resources being used.
        /// </summary>
        public int Remove()
        {
            _base.AV_RemoveVideoOverlay();
            _base._lastError = Player.NO_ERROR;
            return (int)_base._lastError;
        }
    }
}