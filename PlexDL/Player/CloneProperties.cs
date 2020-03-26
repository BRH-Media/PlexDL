using System;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store display clone properties.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class CloneProperties : HideObjectMembers //, IDisposable
    {
        #region Fields (Clone Properties Class)

        internal CloneQuality _quality = CloneQuality.Auto;
        internal CloneLayout _layout = CloneLayout.Zoom;
        internal CloneFlip _flip = CloneFlip.FlipNone;
        internal DisplayShape _shape = DisplayShape.Normal;
        internal bool _videoShape = true;
        internal bool _dragEnabled;
        internal Cursor _dragCursor = Cursors.SizeAll;

        #endregion Fields (Clone Properties Class)

        /// <summary>
        /// Gets or sets the video quality setting of the display clone (default: CloneQuality.Auto).
        /// </summary>
        public CloneQuality Quality
        {
            get => _quality;
            set => _quality = value;
        }

        /// <summary>
        /// Gets or sets the video layout setting of the display clone (default: CloneLayout.Zoom).
        /// </summary>
        public CloneLayout Layout
        {
            get => _layout;
            set => _layout = value;
        }

        /// <summary>
        /// Gets or sets the video flip setting of the display clone (default: Cloneflip.FlipNone).
        /// </summary>
        public CloneFlip Flip
        {
            get => _flip;
            set => _flip = value;
        }

        /// <summary>
        /// Gets or sets the shape of the display clone (default: DisplayShape.Normal). If the display clone is a form, set its BorderStyle to None. See also: VideoShape.
        /// </summary>
        public DisplayShape Shape
        {
            get => _shape;
            set => _shape = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Shape property is related to the video image (or to the window) of the display clone (default: true).
        /// </summary>
        public bool ShapeVideo
        {
            get => _videoShape;
            set => _videoShape = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the parent window (form) of the display clone can be moved by dragging the display clone (default: false).
        /// </summary>
        public bool DragEnabled
        {
            get => _dragEnabled;
            set => _dragEnabled = value;
        }

        /// <summary>
        /// Gets or sets the cursor that is used when the display clone is dragged (default: Cursors.SizeAll).
        /// </summary>
        public Cursor DragCursor
        {
            get => _dragCursor;
            set => _dragCursor = value;
        }
    }
}