using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Copy methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Copy : HideObjectMembers
    {
        #region Fields (Copy Class)

        private Player  _base;
        private bool    _cloneCopy = true;

        #endregion


        internal Copy(Player player)
        {
            _base = player;
        }


        /// <summary>
        /// Gets or sets a value that specifies whether to use the display clones copy method (which is fast and does not copy overlapping windows) with CopyMode.Video and CopyMode.Display (default: true). When enabled, display overlays are copied according to the Player.DisplayClones.ShowOverlay setting.
        /// </summary>
        public bool CloneMode
        {
            get { return _cloneCopy; }
            set{ _cloneCopy = value; }
        }

        /// <summary>
        /// Gets or sets a value that specifies which part of the screen to copy with the Player.Copy methods (default: CopyMode.Video).
        /// </summary>
        public CopyMode Mode
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._copyMode;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                _base._copyMode = value;
            }
        }


        /// <summary>
        /// Returns an image from the Player.Copy.Mode part of the screen. See also: Player.Video.ToImage.
        /// </summary>
        public Image ToImage()
        {
            if (_cloneCopy && (_base._copyMode == CopyMode.Display || _base._copyMode == CopyMode.Video))
            {
                return _base.AV_DisplayCopy(_base._copyMode == CopyMode.Video, true);
            }

            Bitmap memoryImage = null;

            if (_base._hasDisplay && (_base._hasVideo || _base._hasOverlayShown))
            {
                Rectangle r;

                switch (_base._copyMode)
                {
                    case CopyMode.Display:
                        r = _base._display.RectangleToScreen(_base._display.DisplayRectangle);
                        break;
                    case CopyMode.Form:
                        r = _base._display.FindForm().RectangleToScreen(_base._display.FindForm().DisplayRectangle);
                        break;
                    case CopyMode.Parent:
                        r = _base._display.Parent.RectangleToScreen(_base._display.Parent.DisplayRectangle);
                        break;
                    case CopyMode.Screen:
                        r = Screen.GetBounds(_base._display);
                        break;

                    default: // CopyMode.Video
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
                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
            return memoryImage;
        }

        /// <summary>
        /// Returns an image from the specified part of the screen. See also: Player.Video.ToImage.
        /// </summary>
        /// <param name="mode">A value that indicates the part of the screen to copy. The Player.Copy.Mode setting is not changed.</param>
        public Image ToImage(CopyMode mode)
        {
            CopyMode oldMode = _base._copyMode;
            _base._copyMode = mode;
            Image image = ToImage();
            _base._copyMode = oldMode;
            return image;
        }

        /// <summary>
        /// Returns an image from the Player.Copy.Mode part of the screen with the specified dimensions. See also: Player.Video.ToImage.
        /// </summary>
        /// <param name="size">The size of the longest side of the image while maintaining the aspect ratio.</param>
        public Image ToImage(int size)
        {
            Image theImage = null;
            if (size >= 8)
            {
                Image copy = ToImage();
                if (copy != null)
                {
                    try
                    {
                        //if (copy.Width > copy.Height) theImage = new Bitmap(copy, size, (size * copy.Height) / copy.Width);
                        //else theImage = new Bitmap(copy, (size * copy.Width) / copy.Height, size);
                        if (copy.Width > copy.Height) theImage = _base.AV_ResizeImage(copy, size, (size * copy.Height) / copy.Width);
                        else theImage = _base.AV_ResizeImage(copy, (size * copy.Width) / copy.Height, size);
                    }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                    copy.Dispose();
                }
            }
            else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            return theImage;
        }

        /// <summary>
        /// Returns an image from the specified part of the screen with the specified dimensions. See also: Player.Video.ToImage.
        /// </summary>
        /// <param name="size">The size of the longest side of the image while maintaining the aspect ratio.</param>
        /// <param name="mode">A value that indicates the part of the screen to copy. The Player.Copy.Mode setting is not changed.</param>
        public Image ToImage(int size, CopyMode mode)
        {
            CopyMode oldMode = _base._copyMode;
            _base._copyMode = mode;
            Image image = ToImage(size);
            _base._copyMode = oldMode;
            return image;
        }

        /// <summary>
        /// Returns an image from the Player.Copy.Mode part of the screen with the specified dimensions. See also: Player.Video.ToImage.
        /// </summary>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        public Image ToImage(int width, int height)
        {
            Image theImage = null;
            if (width >= 8 && height >= 8)
            {
                Image copy = ToImage();
                if (copy != null)
                {
                    //try { theImage = new Bitmap(copy, width, height); }
                    try { theImage = _base.AV_ResizeImage(copy, width, height); }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                    copy.Dispose();
                }
            }
            else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            return theImage;
        }

        /// <summary>
        /// Returns an image from the specified part of the screen with the specified dimensions. See also: Player.Video.ToImage.
        /// </summary>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="mode">A value that indicates the part of the screen to copy. The Player.Copy.Mode setting is not changed.</param>
        public Image ToImage(int width, int height, CopyMode mode)
        {
            CopyMode oldMode = _base._copyMode;
            _base._copyMode = mode;
            Image image = ToImage(width, height);
            _base._copyMode = oldMode;
            return image;
        }


        /// <summary>
        /// Copies an image from the Player.Copy.Mode part of the screen to the system's clipboard. See also: Player.Video.ToClipboard.
        /// </summary>
        public int ToClipboard()
        {
            Image copy = ToImage();
            if (copy != null)
            {
                try { Clipboard.SetImage(copy); }
                catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                copy.Dispose();
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Copies an image from the specified CopyMode part of the screen to the system's clipboard. See also: Player.Video.ToClipboard.
        /// </summary>
        /// <param name="mode">A value that indicates the part of the screen to copy. The Player.Copy.Mode setting is not changed.</param>
        public int ToClipboard(CopyMode mode)
        {
            CopyMode oldMode = _base._copyMode;
            _base._copyMode = mode;
            ToClipboard();
            _base._copyMode = oldMode;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Copies an image from the Player.Copy.Mode part of the screen with the specified dimensions to the system's clipboard. See also: Player.Video.ToClipboard.
        /// </summary>
        /// <param name="size">The size of the longest side of the image while maintaining the aspect ratio</param>
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
        /// Copies an image from the specified CopyMode part of the screen with the specified dimensions to the system's clipboard. See also: Player.Video.ToClipboard.
        /// </summary>
        /// <param name="size">The size of the longest side of the image while maintaining the aspect ratio.</param>
        /// <param name="mode">A value that indicates the part of the screen to copy. The Player.Copy.Mode setting is not changed.</param>
        public int ToClipboard(int size, CopyMode mode)
        {
            CopyMode oldMode = _base._copyMode;
            _base._copyMode = mode;
            ToClipboard(size);
            _base._copyMode = oldMode;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Copies an image from the Player.Copy.Mode part of the screen with the specified dimensions to the system's clipboard. See also: Player.Video.ToClipboard.
        /// </summary>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
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

        /// <summary>
        /// Copies an image from the specified CopyMode part of the screen with the specified dimensions to the system's clipboard. See also: Player.Video.ToClipboard.
        /// </summary>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="mode">A value that indicates the part of the screen to copy. The Player.Copy.Mode setting is not changed.</param>
        public int ToClipboard(int width, int height, CopyMode mode)
        {
            CopyMode oldMode = _base._copyMode;
            _base._copyMode = mode;
            ToClipboard(width, height);
            _base._copyMode = oldMode;
            return (int)_base._lastError;
        }


        /// <summary>
        /// Saves an image from the Player.Copy.Mode part of the screen to the specified file. See also: Player.Video.ToFile.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        public int ToFile(string fileName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if ((fileName != null) && (fileName.Length > 3))
            {
                Image copy = ToImage();
                if (copy != null)
                {
                    try { copy.Save(fileName, imageFormat); }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                    copy.Dispose();
                }
            }
            else  _base._lastError = HResult.ERROR_INVALID_NAME;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Saves an image from the specified CopyMode part of the screen to the specified file. See also: Player.Video.ToFile.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        /// <param name="mode">A value that indicates the part of the screen to copy. The Player.Copy.Mode setting is not changed.</param>
        public int ToFile(string fileName, System.Drawing.Imaging.ImageFormat imageFormat, CopyMode mode)
        {
            CopyMode oldMode = _base._copyMode;
            _base._copyMode = mode;
            ToFile(fileName, imageFormat);
            _base._copyMode = oldMode;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Saves an image from the Player.Copy.Mode part of the screen with the specified dimensions to the specified file. See also: Player.Video.ToFile.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        /// <param name="size">The size of the longest side of the image to save while maintaining the aspect ratio.</param>
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
        /// Saves an image from the specified CopyMode part of the screen with the specified dimensions to the specified file. See also: Player.Video.ToFile.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        /// <param name="size">The size of the longest side of the image to save while maintaining the aspect ratio.</param>
        /// <param name="mode">A value that indicates the part of the screen to copy. The Player.Copy.Mode setting is not changed.</param>
        public int ToFile(string fileName, System.Drawing.Imaging.ImageFormat imageFormat, int size, CopyMode mode)
        {
            CopyMode oldMode = _base._copyMode;
            _base._copyMode = mode;
            ToFile(fileName, imageFormat, size);
            _base._copyMode = oldMode;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Saves an image from the Player.Copy.Mode part of the screen withe the specified dimensions to the specified file. See also: Player.Video.ToFile.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        /// <param name="width">The width of the image to save.</param>
        /// <param name="height">The height of the image to save.</param>
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

        /// <summary>
        /// Saves an image from the specified CopyMode part of the screen with the specified dimensions to the specified file. See also: Player.Video.ToFile.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        /// <param name="width">The width of the image to save.</param>
        /// <param name="height">The height of the image to save.</param>
        /// <param name="mode">A value that indicates the part of the screen to copy. The Player.Copy.Mode setting is not changed.</param>
        public int ToFile(string fileName, System.Drawing.Imaging.ImageFormat imageFormat, int width, int height, CopyMode mode)
        {
            CopyMode oldMode = _base._copyMode;
            _base._copyMode = mode;
            ToFile(fileName, imageFormat, width, height);
            _base._copyMode = oldMode;
            return (int)_base._lastError;
        }

    }
}