using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Video methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Video : HideObjectMembers
    {
        #region Fields (Video Class)

        private const int   NO_ERROR = 0;

        private Player      _base;
        private bool        _zoomBusy;
        private bool        _boundsBusy;
        private Size        _maxZoomSize;

        private VideoOverlay _videoOverlayClass;

        #endregion

        internal Video(Player player)
        {
            _base = player;
            _maxZoomSize = new Size(Player.DEFAULT_VIDEO_WIDTH_MAXIMUM, Player.DEFAULT_VIDEO_HEIGHT_MAXIMUM);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the DirectX Video Acceleration (DXVA) option in the player's topology loader is enabled (default: true).
        /// <br/>Disabling this option may (or may not) resolve black screen issues with display clones and screenshots (applies to next media played).
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool Acceleration
        {
            // MFTOPOLOGY_DXVA_MODE.None == 1
            // MFTOPOLOGY_DXVA_MODE.Full == 2
            get
            {
                _base._lastError = NO_ERROR;
                return _base._videoAcceleration == 2;
            }
            set
            {
                _base._lastError = NO_ERROR;
                _base._videoAcceleration = value ? 2 : 1;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the playing media contains video.
        /// </summary>
        public bool Present
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasVideo;
            }
        }

        /// <summary>
        /// Gets or sets the maximum allowed zoom size (width and height in pixels) of the video image
        /// <br/>on the player's display window (default: 12000 x 12000, maximum: 25000 x 25000).
        /// </summary>
        public Size ZoomLimit
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _maxZoomSize;
            }
            set
            {
                if (value.Width < Player.VIDEO_WIDTH_MINIMUM || value.Width > Player.VIDEO_WIDTH_MAXIMUM || value.Height < Player.VIDEO_HEIGHT_MINIMUM || value.Height > Player.VIDEO_HEIGHT_MAXIMUM)
                {
                    _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
                else
                {
                    _base._lastError = NO_ERROR;
                    _maxZoomSize = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the player's full screen display mode on all screens (video wall) is activated (default: false).
        /// <br/>See also: Player.FullScreenMode.
        /// </summary>
        public bool Wall
        {
            get { return _base.FS_GetVideoWallMode(); }
            set { _base.FS_SetVideoWallMode(value); }
        }

        /// <summary>
        /// Gets or sets the active video track of the playing media.
        /// <br/>See also: Player.Video.TrackCount and Player.Video.GetTracks.
        /// </summary>
        public int Track
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._videoTrackCurrent;
            }
            set { _base.AV_SetTrack(value, false); }
        }

        /// <summary>
        /// Gets a string with a detailed description of the active video track of the playing media.
        /// <br/>See also: Player.Video.GetTrackString.
        /// </summary>
        public string TrackString
        {
            get
            {
                if (_base._hasVideo)
                {
                    _base._lastError = NO_ERROR;
                    return _base.AV_GetVideoTracks()[_base._videoTrackCurrent].ToString();
                }
                
                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns a string with a detailed description of the specified video track of the playing media.
        /// <br/>See also: Player.Video.TrackString.
        /// </summary>
        /// <param name="track">The track number of the video track to get the string of.</param>
        public string GetTrackString(int track)
        {
            if (_base._hasVideo)
            {
                if (track >= 0 && track < _base._videoTrackCount)
                {
                    _base._lastError = NO_ERROR;
                    return _base.AV_GetVideoTracks()[track].ToString();
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return string.Empty;
        }

        /// <summary>
        /// Gets the number of video tracks in the playing media.
        /// <br/>See also: Player.Video.Track and Player.Video.GetTracks.
        /// </summary>
        public int TrackCount
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._videoTrackCount;
            }
        }

        /// <summary>
        /// Returns the video tracks in the playing media.
        /// <br/>See also: Player.Video.Track and Player.Video.TrackCount.
        /// </summary>
        public VideoTrack[] GetTracks()
        {
            return _base.AV_GetVideoTracks();
        }

        /// <summary>
        /// Gets the original size (width and height in pixels) of the video image of the playing media,
        /// <br/>adjusted for any non-square pixel aspect ratio and rotation.
        /// </summary>
        public Size SourceSize
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasVideo ? _base._videoSourceSize : Size.Empty;
            }
        }

        /// <summary>
        /// Gets the video frame rate of the playing media.
        /// <br/>Values are in frames per second (fps).
        /// </summary>
        public float FrameRate
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasVideo ? _base._videoFrameRate : 0;
            }
        }

        /// <summary>
        /// Gets or sets the size and location (in pixels) of the video image on the player's display window.
        /// <br/>When set, the player's display mode (Player.Display.Mode) is set to DisplayMode.Manual.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                _base._lastError = NO_ERROR;
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
                    if (!_boundsBusy)
                    {
                        _boundsBusy = true;

                        if ((value.Width >= Player.VIDEO_WIDTH_MINIMUM) && (value.Width <= _maxZoomSize.Width)
                                                                        && (value.Height >= Player.VIDEO_HEIGHT_MINIMUM) && (value.Height <= _maxZoomSize.Height))
                        {
                            //_base._lastError = NO_ERROR;

                            _base._videoBounds = value;
                            _base._videoBoundsClip = Rectangle.Intersect(_base._display.DisplayRectangle, _base._videoBounds);
                            _base._hasVideoBounds = true;

                            if (_base._displayMode == DisplayMode.Manual) _base._display.Refresh();
                            else _base.Display.Mode = DisplayMode.Manual;

                            if (_base._hasVideo)
                            {
                                // TODO - image gets stuck when same size as display - is it _videoDisplay or MF
                                if (_base._videoBounds.X <= 0 || _base._videoBounds.Y <= 0)
                                {
                                    _base._videoDisplay.Width--;
                                    _base._videoDisplay.Width++;
                                }

                                if (_base._hasDisplayShape) _base.AV_UpdateDisplayShape();
                            }

                            _base._mediaVideoBoundsChanged?.Invoke(_base, EventArgs.Empty);
                            _base._lastError = NO_ERROR;
                        }
                        else _base._lastError = HResult.MF_E_OUT_OF_RANGE;

                        _boundsBusy = false;
                    }
                    else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                }
                else _base._lastError = HResult.MF_E_INVALIDREQUEST;
            }
        }

        // Video Zoom, Move, Stretch

        /// <summary>
        /// Increases or decreases the size of the playing video image in the center of the player's display window.
        /// <br/>The player's display mode (Player.Display.Mode) is set to DisplayMode.Manual.
        /// </summary>
        /// <param name="factor">The factor by which the video image is to be zoomed.</param>
        public int Zoom(double factor)
        {
            if (_base._hasVideo) return Zoom(factor, _base._display.Width / 2, _base._display.Height / 2);
            _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Increases or decreases the size of the playing video image at the specified display location.
        /// <br/>The player's display mode (Player.Display.Mode) is set to DisplayMode.Manual.
        /// </summary>
        /// <param name="factor">The factor by which the video image is to be zoomed.</param>
        /// <param name="center">The center location of the zoom on the player's display window.</param>
        public int Zoom(double factor, Point center)
        {
            if (_base._hasVideo) return Zoom(factor, center.X, center.Y);
            _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Increases or decreases the size of the playing video image at the specified location on the player's display window.
        /// <br/>The player's display mode (Player.Display.Mode) is set to DisplayMode.Manual.
        /// </summary>
        /// <param name="factor">The factor by which the video image is to be zoomed.</param>
        /// <param name="xCenter">The horizontal (x) center location of the zoom on the player's display window.</param>
        /// <param name="yCenter">The vertical (y) center location of the zoom on the player's display window.</param>
        public int Zoom(double factor, int xCenter, int yCenter)
        {
            if (_base._hasVideo && factor > 0)
            {
                if (!_zoomBusy)
                {
                    _zoomBusy = true;
                    _base._lastError = NO_ERROR;

                    if (factor != 1)
                    {
                        double width = 0;
                        double height = 0;
                        Rectangle r = _base._videoBounds;

                        if (r.Width < r.Height)
                        {
                            width = r.Width * factor;
                            if (width > _maxZoomSize.Width)
                            {
                                factor = (double)_maxZoomSize.Width / r.Width;
                                width = r.Width * factor;
                            }
                            else if ((width / r.Width) * r.Height > _maxZoomSize.Height)
                            {
                                factor = (double)_maxZoomSize.Height / r.Height;
                                width = r.Width * factor;
                            }
                            r.X = (int)Math.Round(-factor * (xCenter - r.X)) + xCenter;

                            if (width >= 10)
                            {
                                r.Y = (int)Math.Round(-(width / r.Width) * (yCenter - r.Y)) + yCenter;
                                height = (width / r.Width) * r.Height;
                            }
                        }
                        else
                        {
                            height = r.Height * factor;
                            if (height > _maxZoomSize.Height)
                            {
                                factor = (double)_maxZoomSize.Height / r.Height;
                                height = r.Height * factor;
                            }
                            else if ((height / r.Height) * r.Width > _maxZoomSize.Width)
                            {
                                factor = (double)_maxZoomSize.Width / r.Width;
                                height = r.Height * factor;
                            }
                            r.Y = (int)Math.Round(-factor * (yCenter - r.Y)) + yCenter;

                            if (height >= 10)
                            {
                                r.X = (int)Math.Round(-(height / r.Height) * (xCenter - r.X)) + xCenter;
                                width = (height / r.Height) * r.Width;
                            }
                        }

                        r.Width = (int)Math.Round(width);
                        r.Height = (int)Math.Round(height);
                        Bounds = r;
                    }
                    _zoomBusy = false;
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
            else _base._lastError = HResult.MF_E_INVALIDREQUEST;
            return (int)_base._lastError;
        }

        ///// <summary>
        ///// Enlarges the specified part of the playing video image to the entire display window. The player's display mode (Player.Display.Mode) is set to DisplayMode.Manual.
        ///// </summary>
        ///// <param name="area">The part of the video image to enlarge. The value is a normalized rectangle (left, top, right, bottom): the full player's display window is represented by {0.0F, 0.0F, 1.0F, 1.0F}.</param>
        //public int Zoom(RectangleF area)
        //{
        //	if (_base._hasVideo)
        //	{
        //		Rectangle r = new Rectangle((int)(area.X * _base._videoBounds.Width),
        //			(int)(area.Y * _base._videoBounds.Height),
        //			(int)(area.Width * _base._videoBounds.Width),
        //			(int)(area.Height * _base._videoBounds.Height));

        //		r.Width -= r.X; r.Height -= r.Y;

        //		//if (_base._videoBounds.Width <= _maxZoomSize.Width && _base._videoBounds.Height <= _maxZoomSize.Height && (r.X >= 0 && r.X <= (_base._display.Width - 8)) && (r.Y >= 0 && r.Y <= (_base._display.Height - 8)) && (r.X + r.Width <= _base._display.Width) && (r.Y + r.Height <= _base._display.Height))
        //		if (_base._videoBounds.Width <= _maxZoomSize.Width && _base._videoBounds.Height <= _maxZoomSize.Height && (r.X >= 0 && r.X <= _base._videoBounds.Width) && (r.Y >= 0 && r.Y <= _base._videoBounds.Height) && (r.X + r.Width <= _base._videoBounds.Width) && (r.Y + r.Height <= _base._videoBounds.Height))
        //		{
        //			double factorX = (double)_base._display.Width / r.Width;
        //			double factorY = (double)_base._display.Height / r.Height;

        //			if (_base._videoBounds.Width * factorX > _maxZoomSize.Width)
        //			{
        //				double factorX2 = factorX;
        //				factorX = (double)_maxZoomSize.Width / _base._videoBounds.Width;
        //				factorY *= (factorX / factorX2);
        //			}
        //			if (_base._videoBounds.Height * factorY > _maxZoomSize.Height)
        //			{
        //				double factorY2 = factorY;
        //				factorY = (double)_maxZoomSize.Height / _base._videoBounds.Height;
        //				factorX *= (factorY / factorY2);
        //			}

        //			Bounds = new Rectangle(
        //					(int)(((_base._videoBounds.X - r.X) * factorX)),
        //					(int)(((_base._videoBounds.Y - r.Y) * factorY)),
        //					(int)((_base._videoBounds.Width * factorX)),
        //					(int)((_base._videoBounds.Height * factorY)));
        //		}
        //		else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
        //	}
        //	else _base._lastError = HResult.MF_E_INVALIDREQUEST;
        //	return (int)_base._lastError;
        //}

        /// <summary>
        /// Enlarges the specified part of the player's display window to the entire display window of the player.
        /// <br/>Only available when video is playing.
        /// <br/>The player's display mode (Player.Display.Mode) is set to DisplayMode.Manual.
        /// </summary>
        /// <param name="area">The area of the player's display window to enlarge.</param>
        public int Zoom(Rectangle area)
        {
            if (_base._hasVideo)
            {
                if (_base._videoBounds.Width <= _maxZoomSize.Width && _base._videoBounds.Height <= _maxZoomSize.Height && (area.X >= 0 && area.X <= (_base._display.Width - 8)) && (area.Y >= 0 && area.Y <= (_base._display.Height - 8)) && (area.X + area.Width <= _base._display.Width) && (area.Y + area.Height <= _base._display.Height))
                {
                    double factorX = (double)_base._display.Width / area.Width;
                    double factorY = (double)_base._display.Height / area.Height;

                    if (_base._videoBounds.Width * factorX > _maxZoomSize.Width)
                    {
                        double factorX2 = factorX;
                        factorX = (double)_maxZoomSize.Width / _base._videoBounds.Width;
                        factorY *= (factorX / factorX2);
                    }
                    if (_base._videoBounds.Height * factorY > _maxZoomSize.Height)
                    {
                        double factorY2 = factorY;
                        factorY = (double)_maxZoomSize.Height / _base._videoBounds.Height;
                        factorX *= (factorY / factorY2);
                    }

                    Bounds = new Rectangle(
                        (int)(((_base._videoBounds.X - area.X) * factorX)),
                        (int)(((_base._videoBounds.Y - area.Y) * factorY)),
                        (int)((_base._videoBounds.Width * factorX)),
                        (int)((_base._videoBounds.Height * factorY)));
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_INVALIDREQUEST;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Moves the location of the playing video image on the player's display window by the given amount of pixels.
        /// <br/>The player's display mode (Player.Display.Mode) is set to DisplayMode.Manual.
        /// </summary>
        /// <param name="horizontal">The amount of pixels to move the video image in the horizontal (x) direction.</param>
        /// <param name="vertical">The amount of pixels to move the video image in the vertical (y) direction.</param>
        public int Move(int horizontal, int vertical)
        {
            if (_base._hasVideo)
            {
                Bounds = new Rectangle(_base._videoBounds.X + horizontal, _base._videoBounds.Y + vertical, _base._videoBounds.Width, _base._videoBounds.Height);
            }
            else _base._lastError = HResult.MF_E_INVALIDREQUEST;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Increases or decreases the size of the playing video image by the specified number of pixels in the center of the video image.
        /// <br/>The player's display mode (Player.Display.Mode) is set to DisplayMode.Manual.
        /// </summary>
        /// <param name="horizontal">The amount of pixels to stretch the video image in the horizontal (x) direction.</param>
        /// <param name="vertical">The amount of pixels to stretch the video image in the vertical (y) direction.</param>
        public int Stretch(int horizontal, int vertical)
        {
            if (_base._hasVideo)
            {
                Bounds = new Rectangle(_base._videoBounds.X - (horizontal / 2), _base._videoBounds.Y - (vertical / 2), _base._videoBounds.Width + horizontal, _base._videoBounds.Height + vertical);
            }
            else _base._lastError = HResult.MF_E_INVALIDREQUEST;
            return (int)_base._lastError;
        }

        // Video Colors

        /// <summary>
        /// Gets or sets a value that indicates the brightness of the player's video image.
        /// <br/>Values from -1.0 to 1.0 (default: 0.0).
        /// </summary>
        public double Brightness
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._brightness;
            }
            set
            {
                _base.AV_SetBrightness(value, true);
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the contrast of the player's video image.
        /// <br/>Values from -1.0 to 1.0 (default: 0.0).
        /// </summary>
        public double Contrast
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._contrast;
            }
            set
            {
                _base.AV_SetContrast(value, true);
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the hue of the player's video image.
        /// <br/>Values from -1.0 to 1.0 (default: 0.0).
        /// </summary>
        public double Hue
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hue;
            }
            set
            {
                _base.AV_SetHue(value, true);
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the saturation of the player's video image.
        /// <br/>Values from -1.0 to 1.0 (default: 0.0).
        /// </summary>
        public double Saturation
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._saturation;
            }
            set
            {
                _base.AV_SetSaturation(value, true);
            }
        }

        // Copy to Image

        /// <summary>
        /// Returns a copy of the player's currently displayed video image (without display overlay).
        /// <br/>See also: Player.Copy.ToImage.
        /// </summary>
        public Image ToImage()
        {
            return _base.AV_DisplayCopy(true, false);
        }

        /// <summary>
        /// Returns a copy of the player's currently displayed video image (without display overlay) with the specified dimensions.
        /// <br/>See also: Player.Copy.ToImage.
        /// </summary>
        /// <param name="size">The desired size (in pixels) of the longest side of the obtained image while maintaining the aspect ratio.</param>
        public Image ToImage(int size)
        {
            Image theImage = null;
            if (size >= 8)
            {
                Image copy = _base.AV_DisplayCopy(true, false);
                if (copy != null)
                {
                    try
                    {
                        //if (copy.Width > copy.Height) theImage = new Bitmap(copy, size, (size * copy.Height) / copy.Width);
                        //else theImage = new Bitmap(copy, (size * copy.Width) / copy.Height, size);
                        if (copy.Width > copy.Height) theImage = Player.AV_ResizeImage(copy, size, (size * copy.Height) / copy.Width);
                        else theImage = Player.AV_ResizeImage(copy, (size * copy.Width) / copy.Height, size);
                    }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                    copy.Dispose();
                }
            }
            else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            return theImage;
        }

        /// <summary>
        /// Returns a copy of the player's currently displayed video image (without display overlay) with the specified dimensions.
        /// <br/>See also: Player.Copy.ToImage.
        /// </summary>
        /// <param name="width">The desired width (in pixels) of the obtained image.</param>
        /// <param name="height">The desired height (in pixels) of the obtained image.</param>
        public Image ToImage(int width, int height)
        {
            Image theImage = null;
            if (width >= 8 && height >= 8)
            {
                Image copy = _base.AV_DisplayCopy(true, false);
                if (copy != null)
                {
                    //try { theImage = new Bitmap(copy, width, height); }
                    try { theImage = Player.AV_ResizeImage(copy, width, height); }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                    copy.Dispose();
                }
            }
            else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            return theImage;
        }

        /// <summary>
        /// Returns a partial copy of the player's currently displayed video image (without display overlay).
        /// <br/>The area parameters are normalized values between 0.0 and 1.0.
        /// </summary>
        /// <param name="left">The left position of the area of the video image to be obtained.
        /// <br/>Values between 0.0 and 1.0.</param>
        /// <param name="top">The top position of the area of the video image to be obtained.
        /// <br/>Values between 0.0 and 1.0.</param>
        /// <param name="right">The right position (note: not the width) of the area of the video image to be obtained.
        /// <br/>Values between 0.0 and 1.0.</param>
        /// <param name="bottom">The bottom position (note: not the height) of the area of the video image to be obtained.
        /// <br/>Values between 0.0 and 1.0.</param>
        public Image ToImage(float left, float top, float right, float bottom)
        {
            return _base.AV_VideoAreaCopy(left, top, right, bottom);
        }

        /// <summary>
        /// Returns a partial copy of the player's currently displayed video image (without display overlay) with the specified dimensions.
        /// <br/>The area parameters are normalized values between 0.0 and 1.0.
        /// </summary>
        /// <param name="left">The left position of the area of the video image to be obtained.
        /// <br/>Values between 0.0 and 1.0.</param>
        /// <param name="top">The top position of the area of the video image to be obtained.
        /// <br/>Values between 0.0 and 1.0.</param>
        /// <param name="right">The right position (note: not the width) of the area of the video image to be obtained.
        /// <br/>Values between 0.0 and 1.0.</param>
        /// <param name="bottom">The bottom position (note: not the height) of the area of the video image to be obtained.
        /// <br/>Values between 0.0 and 1.0.</param>
        /// <param name="size">The desired size (in pixels) of the longest side of the obtained image while maintaining the aspect ratio.</param>
        public Image ToImage(float left, float top, float right, float bottom, int size)
        {
            Image theImage = null;
            if (size >= 8)
            {
                Image copy = _base.AV_VideoAreaCopy(left, top, right, bottom);
                if (copy != null)
                {
                    try
                    {
                        if (copy.Width > copy.Height) theImage = Player.AV_ResizeImage(copy, size, (size * copy.Height) / copy.Width);
                        else theImage = Player.AV_ResizeImage(copy, (size * copy.Width) / copy.Height, size);
                    }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                    copy.Dispose();
                }
            }
            else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            return theImage;
        }

        /// <summary>
        /// Returns a partial copy of the player's currently displayed video image (without display overlay).
        /// <br/>The area parameter is a normalized rectangle with values between 0.0 and 1.0 (inclusive).
        /// </summary>
        /// <param name="area">A normalized rectangle that specifies the position and size of the area of the video image to be obtained.
        /// <br/>All values (X, Y, Width (= Right) and Height (= Bottom)) between 0.0 and 1.0 (inclusive).</param>
        public Image ToImage(RectangleF area)
        {
            return _base.AV_VideoAreaCopy(area.X, area.Y, area.Width, area.Height);
        }

        /// <summary>
        /// Returns a partial copy of the player's currently displayed video image (without display overlay) with the specified dimensions.
        /// <br/>The area parameter is a normalized rectangle with values between 0.0 and 1.0 (inclusive).
        /// </summary>
        /// <param name="area">A normalized rectangle that specifies the position and size of the area of the video image to be obtained.
        /// <br/>All values (X, Y, Width (= Right) and Height (= Bottom)) between 0.0 and 1.0 (inclusive).</param>
        /// <param name="size">The desired size (in pixels) of the longest side of the obtained image while maintaining the aspect ratio.</param>
        public Image ToImage(RectangleF area, int size)
        {
            return ToImage(area.X, area.Y, area.Width, area.Height, size);
        }

        // Copy to Clipboard

        /// <summary>
        /// Copies the player's currently displayed video image (without display overlay) to the system's clipboard.
        /// <br/>See also: Player.Copy.ToClipboard.
        /// </summary>
        public int ToClipboard()
        {
            Image copy = _base.AV_DisplayCopy(true, false);
            if (copy != null)
            {
                try { Clipboard.SetImage(copy); }
                catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                copy.Dispose();
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Copies the player's currently displayed video image (without display overlay) with the specified dimensions to the system's clipboard.
        /// <br/>See also: Player.Copy.ToClipboard.
        /// </summary>
        /// <param name="size">The size (in pixels) of the longest side of the image while maintaining the aspect ratio.</param>
        public int ToClipboard(int size)
        {
            Image copy = ToImage(size);
            if (copy != null)
            {
                try { Clipboard.SetImage(copy); }
                catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                copy.Dispose();
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Copies the player's currently displayed video image (without display overlay) with the specified dimensions to the system's clipboard.
        /// <br/>See also: Player.Copy.ToClipboard.
        /// </summary>
        /// <param name="width">The width (in pixels) of the image.</param>
        /// <param name="height">The height (in pixels) of the image.</param>
        public int ToClipboard(int width, int height)
        {
            Image copy = ToImage(width, height);
            if (copy != null)
            {
                try { Clipboard.SetImage(copy); }
                catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                copy.Dispose();
            }
            return (int)_base._lastError;
        }

        // Copy to File

        /// <summary>
        /// Saves a copy of the player's currently displayed video image (without display overlay) to the specified file.
        /// <br/>See also: Player.Copy.ToFile.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        public int ToFile(string fileName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if ((fileName != null) && (fileName.Length > 3))
            {
                Image copy = _base.AV_DisplayCopy(true, false);
                if (copy != null)
                {
                    try { copy.Save(fileName, imageFormat); }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                    copy.Dispose();
                }
            }
            else _base._lastError = HResult.ERROR_INVALID_NAME;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Saves a copy of the player's currently displayed video image (without display overlay) with the specified dimensions to the specified file.
        /// <br/>See also: Player.Copy.ToFile.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        /// <param name="size">The size (in pixels) of the longest side of the image to save while maintaining the aspect ratio.</param>
        public int ToFile(string fileName, System.Drawing.Imaging.ImageFormat imageFormat, int size)
        {
            if ((fileName != null) && (fileName.Length > 3))
            {
                Image copy = ToImage(size);
                if (copy != null)
                {
                    try { copy.Save(fileName, imageFormat); }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                    copy.Dispose();
                }
            }
            else _base._lastError = HResult.ERROR_INVALID_NAME;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Saves a copy of the player's currently displayed video image (without display overlay) with the specified dimensions to the specified file.
        /// <br/>See also: Player.Copy.ToFile.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        /// <param name="width">The width (in pixels) of the image to save.</param>
        /// <param name="height">The height (in pixels) of the image to save.</param>
        public int ToFile(string fileName, System.Drawing.Imaging.ImageFormat imageFormat, int width, int height)
        {
            if ((fileName != null) && (fileName.Length > 3))
            {
                Image copy = ToImage(width, height);
                if (copy != null)
                {
                    try { copy.Save(fileName, imageFormat); }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                    copy.Dispose();
                }
            }
            else _base._lastError = HResult.ERROR_INVALID_NAME;
            return (int)_base._lastError;
        }

        // Miscellaneous

        /// <summary>
        /// Gets or sets a value that indicates whether video tracks in subsequent media files are ignored by the player (default: false).
        /// <br/>The video track information remains available. Allows to play audio from media with unsupported video formats.
        /// </summary>
        public bool Cut
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._videoCut;
            }
            set
            {
                _base._videoCut = value;
                if (value) _base._audioCut = false;
                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether video images will be displayed in HD widescreen (16:9) format (default: false).
        /// <br/>For use with incorrectly displayed video images.
        /// <br/>See also: Player.Video.AspectRatio.
        /// </summary>
        public bool Widescreen
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._videoAspectRatio && (_base._videoAspectSize.Width == 16F && _base._videoAspectSize.Height == 9F);
            }
            set
            {
                _base._lastError = _base.AV_SetVideoAspectRatio(value ? new SizeF(16F, 9F) : SizeF.Empty);
            }
        }

        /// <summary>
        /// Gets or sets a custom video image aspect ratio, for example new SizeF(16, 9) for widescreen (default: 0:0 (SizeF.Empty - original ratio)).
        /// <br/>For use with incorrectly displayed video images.
        /// <br/>See also: Player.Video.Widescreen.
        /// </summary>
        public SizeF AspectRatio
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._videoAspectSize;
            }
            set
            {
                _base._lastError = _base.AV_SetVideoAspectRatio(value);
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates how stereoscopic side-by-side/over-under 3D video is displayed (default: Video3DView.NormalImage).
        /// </summary>
        public Video3DView View3D
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._video3DView;
            }
            set
            {
                if (value != _base._video3DView)
                {
                    _base._video3DView = value;
                    if (_base._hasVideo)
                    {
                        _base.AV_SetVideo3DView();
                    }
                    else if (_base._video3DView == Video3DView.NormalImage)
                    {
                        _base._videoCropRect = null;
                        _base._videoAspectSize = Size.Empty;
                        _base._videoWidthRatio = 0F;
                        _base._videoHeightRatio = 0F;
                        if (_base._videoAspectRatio)
                        {
                            _base._videoAspectRatio = false;
                            _base._mediaVideoAspectRatioChanged?.Invoke(_base, EventArgs.Empty);
                        }

                        if (_base._videoCropMode)
                        {
                            _base._videoCropMode = false;
                            _base._mediaVideoCropChanged?.Invoke(_base, EventArgs.Empty);
                        }
                    }
                    _base._mediaVideoView3DChanged?.Invoke(_base, EventArgs.Empty);
                }
                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates which part of the video source image is displayed (default: RectangleF.Empty).
        /// <br/>The value is a normalized rectangle (left, top, right, bottom): the full video image is represented by {0.0F, 0.0F, 1.0F, 1.0F}.
        /// <br/>RectangleF.Empty represents the original video image.
        /// </summary>
        public RectangleF Crop
        {
            get
            {
                _base._lastError = NO_ERROR;
                if (_base._videoCropMode)
                {
                    return new RectangleF(
                        _base._videoCropRect.left,
                        _base._videoCropRect.right,
                        _base._videoCropRect.top,
                        _base._videoCropRect.bottom
                    );
                }
                return RectangleF.Empty;
            }
            set
            {
                bool doUpdate = false;
                _base._lastError = NO_ERROR;

                if (value.IsEmpty || value == new RectangleF(0, 0, 1, 1))
                {
                    if (_base._videoCropMode)
                    {
                        _base._videoCropMode = false;
                        if (_base._hasVideo) _base._videoCropRect = new MFVideoNormalizedRect(0, 0, 1, 1);
                        else _base._videoCropRect = null;
                        doUpdate = true;
                    }
                }
                else
                {
                    if (value.Left >= 0 && value.Left < value.Width &&
                        value.Top >= 0 && value.Top < value.Height &&
                        value.Width <= 1 && value.Height <= 1)
                    {
                        _base._videoCropRect = new MFVideoNormalizedRect(value.Left, value.Top, value.Width, value.Height);
                        _base._videoCropMode = true;
                        doUpdate = true;
                    }
                    else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }

                if (doUpdate)
                {
                    if (_base._video3DView != Video3DView.NormalImage)
                    {
                        _base._video3DView = Video3DView.NormalImage;
                        _base._mediaVideoView3DChanged?.Invoke(_base, EventArgs.Empty);

                    }

                    _base._mediaVideoCropChanged?.Invoke(_base, EventArgs.Empty);
                    if (_base._hasVideo)
                    {
                        _base._display.Invalidate();
                        if (_base._mediaVideoBoundsChanged != null)
                        {
                            Application.DoEvents();
                            _base._mediaVideoBoundsChanged?.Invoke(_base, EventArgs.Empty);
                        }
                    }
                    _base._lastError = NO_ERROR;
                }
            }
        }

        /// <summary>
        /// Provides access to the video bitmap overlay settings of the player (for example, Player.Video.Overlay.Set).
        /// </summary>
        public VideoOverlay Overlay
        {
            get
            {
                if (_videoOverlayClass == null) _videoOverlayClass = new VideoOverlay(_base);
                return _videoOverlayClass;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the rotation angle of the player's video image.
        /// <br/>Values are 0 (no rotation), 90, 180 or 270 degrees (default: 0).
        /// <br/>Not available with webcam video, see Player.Recorder.Rotation.
        /// </summary>
        public VideoRotation Rotation
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._videoRotation;
            }
            set
            {
                if (value != _base._videoRotation)
                {
                    _base._videoRotation = value;
                    if (!_base._webcamMode && _base._hasVideo)
                    {
                        _base.AV_UpdateTopology();
                        if (_base._paused) _base.SetPosition(_base.PositionX + 5);
                        _base._display.Invalidate();
                    }
                    _base._mediaVideoRotationChanged?.Invoke(_base, EventArgs.Empty);
                    _base._lastError = NO_ERROR;
                }
            }
        }
		

        ///// <summary>
        ///// Provides access to the video recorder settings of the player (for example, Player.Video.Recorder.Start).
        ///// </summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public VideoRecorder Recorder
        //{
        //    get
        //    {
        //        if (_base._videoRecorderClass == null) _base._videoRecorderClass = new VideoRecorder(_base, false);
        //        return _base._videoRecorderClass;
        //    }
        //}

        ///// <summary>
        ///// Media Foundation Transforms Test Method.
        ///// </summary>
        //public void GetTransforms()
        //{
        //    Guid MFT_CATEGORY_VIDEO_EFFECT = new Guid(0x12e17c21, 0x532c, 0x4a6e, 0x8a, 0x1c, 0x40, 0x82, 0x5a, 0x73, 0x63, 0x97);
        //    Guid MFT_CATEGORY_AUDIO_EFFECT = new Guid(0x11064c48, 0x3648, 0x4ed0, 0x93, 0x2e, 0x05, 0xce, 0x8a, 0xc8, 0x11, 0xb7);
        //    Guid MFT_FRIENDLY_NAME_Attribute = new Guid(0x314ffbae, 0x5b41, 0x4c95, 0x9c, 0x19, 0x4e, 0x7d, 0x58, 0x6f, 0xac, 0xe3);

        //    IMFActivate[] transforms = null;
        //    int count = 0;
        //    StringBuilder name = new StringBuilder(256);
        //    int length;

        //    try
        //    {
        //        _base._lastError = MFExtern.MFTEnumEx(MFT_CATEGORY_VIDEO_EFFECT, 0, null, null, out transforms, out count);
        //    }
        //    catch { }


        //    for (int i = 0; i < count; i++)
        //    {
        //        transforms[i].GetString(MFT_FRIENDLY_NAME_Attribute, name, name.Capacity, out length);
        //        MessageBox.Show(name.ToString());
        //    }
        //}
    }
}