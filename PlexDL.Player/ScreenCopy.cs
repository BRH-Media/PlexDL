using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the ScreenCopy methods and properties of the PlexDL.Player.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ScreenCopy : HideObjectMembers
    {
        #region Fields (ScreenCopy Class)

        private Player _base;
        private bool _cloneCopy = true;

        #endregion Fields (ScreenCopy Class)

        internal ScreenCopy(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the display clones copy method (that is fast and does not copy overlapping windows) with ScreenCopyMode.Video and ScreenCopyMode.Display (default: true). When enabled, display overlays are copied according to the Player.DisplayClones.ShowOverlay setting.
        /// </summary>
        public bool CloneMode
        {
            get { return _cloneCopy; }
            set { _cloneCopy = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating the part of the screen to copy with the Player.ScreenCopy methods (default: ScreenCopyMode.Video).
        /// </summary>
        public ScreenCopyMode Mode
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._screenCopyMode;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                _base._screenCopyMode = value;
            }
        }

        /// <summary>
        /// Returns an image from the Player.ScreenCopy.Mode part of the screen. See also: Player.Video.ToImage.
        /// </summary>
        public Image ToImage()
        {
            if (_cloneCopy && (_base._screenCopyMode == ScreenCopyMode.Display || _base._screenCopyMode == ScreenCopyMode.Video))
            {
                return _base.AV_DisplayCopy(_base._screenCopyMode == ScreenCopyMode.Video, true);
            }

            Bitmap memoryImage = null;

            if (_base._hasDisplay && (_base._hasVideo || _base._hasOverlay))
            {
                Rectangle r;

                switch (_base._screenCopyMode)
                {
                    case ScreenCopyMode.Display:
                        r = _base._display.RectangleToScreen(_base._display.DisplayRectangle);
                        break;

                    case ScreenCopyMode.Form:
                        r = _base._display.FindForm().RectangleToScreen(_base._display.FindForm().DisplayRectangle);
                        break;

                    case ScreenCopyMode.Parent:
                        r = _base._display.Parent.RectangleToScreen(_base._display.Parent.DisplayRectangle);
                        break;

                    case ScreenCopyMode.Screen:
                        r = Screen.GetBounds(_base._display);
                        break;

                    default: // ScreenCopyMode.Video
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
        /// <param name="mode">A value indicating the part of the screen to copy. The Player.Screencopy.Mode setting is not changed.</param>
        public Image ToImage(ScreenCopyMode mode)
        {
            ScreenCopyMode oldMode = _base._screenCopyMode;
            _base._screenCopyMode = mode;
            Image image = ToImage();
            _base._screenCopyMode = oldMode;
            return image;
        }

        /// <summary>
        /// Copies an image from the Player.ScreenCopy.Mode part of the screen to the system's clipboard. See also: Player.Video.ToClipboard.
        /// </summary>
        public int ToClipboard()
        {
            Image theImage = ToImage();
            if (_base._lastError == Player.NO_ERROR)
            {
                try { Clipboard.SetImage(theImage); }
                catch (Exception e)
                {
                    _base._lastError = (HResult)Marshal.GetHRForException(e);
                }
                theImage.Dispose();
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Copies an image from the specified ScreenCopyMode part of the screen to the system's clipboard. See also: Player.Video.ToClipboard.
        /// </summary>
        /// <param name="mode">A value indicating the part of the screen to copy. The Player.Screencopy.Mode setting is not changed.</param>
        public int ToClipboard(ScreenCopyMode mode)
        {
            ScreenCopyMode oldMode = _base._screenCopyMode;
            _base._screenCopyMode = mode;
            ToClipboard();
            _base._screenCopyMode = oldMode;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Saves an image from the Player.ScreenCopy.Mode part of the screen to the specified file. See also: Player.Video.ToFile.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        public int ToFile(string fileName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if ((fileName != null) && (fileName.Length > 3))
            {
                Image theImage = ToImage();
                if (_base._lastError == Player.NO_ERROR)
                {
                    try { theImage.Save(fileName, imageFormat); }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                    theImage.Dispose();
                }
            }
            else
            {
                _base._lastError = HResult.ERROR_INVALID_NAME;
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Saves an image from the specified ScreenCopyMode part of the screen to the specified file. See also: Player.Video.ToFile.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="imageFormat">The file format of the image to save.</param>
        /// <param name="mode">A value indicating the part of the screen to copy. The Player.Screencopy.Mode setting is not changed.</param>
        public int ToFile(string fileName, System.Drawing.Imaging.ImageFormat imageFormat, ScreenCopyMode mode)
        {
            ScreenCopyMode oldMode = _base._screenCopyMode;
            _base._screenCopyMode = mode;
            ToFile(fileName, imageFormat);
            _base._screenCopyMode = oldMode;
            return (int)_base._lastError;
        }
    }
}