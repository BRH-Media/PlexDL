using System;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store display clone properties.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class CloneProperties : HideObjectMembers
    {
        #region Fields (Clone Properties Class)

        internal CloneQuality   _quality        = CloneQuality.Auto;
        internal CloneLayout    _layout         = CloneLayout.Zoom;
        internal CloneFlip      _flip           = CloneFlip.FlipNone;
        internal DisplayShape   _shape          = DisplayShape.Normal;
        internal bool           _videoShape     = true;
        internal bool           _dragEnabled;
        internal Cursor         _dragCursor     = Cursors.SizeAll;

        #endregion

        /// <summary>
        /// Gets or sets the video quality of the display clone (default: CloneQuality.Auto).
        /// </summary>
        public CloneQuality Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }

        /// <summary>
        /// Gets or sets the video layout (display mode) of the display clone (default: CloneLayout.Zoom).
        /// </summary>
        public CloneLayout Layout
        {
            get { return _layout; }
            set { _layout = value; }
        }

        /// <summary>
        /// Gets or sets the video flip mode of the display clone (default: CloneFlip.FlipNone).
        /// </summary>
        public CloneFlip Flip
        {
            get { return _flip; }
            set { _flip = value; }
        }

        /// <summary>
        /// Gets or sets the shape of the display clone window (default: DisplayShape.Normal).
        /// <br/>See also: CloneProperties.ShapeVideo.
        /// </summary>
        public DisplayShape Shape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the CloneProperties.Shape property applies to the video image
        /// <br/>or to the display window of the display clone (default: true (video image)).
        /// </summary>
        public bool ShapeVideo
        {
            get { return _videoShape; }
            set { _videoShape = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the parent window (form) of the display clone
        /// <br/>can be moved by dragging the display clone window (default: false).
        /// <br/>See also: CloneProperties.DragCursor.
        /// </summary>
        public bool DragEnabled
        {
            get { return _dragEnabled; }
            set { _dragEnabled = value; }
        }

        /// <summary>
        /// Gets or sets the cursor that is used when the display clone window is dragged (default: Cursors.SizeAll).
        /// <br/>See also: CloneProperties.DragEnabled.
        /// </summary>
        public Cursor DragCursor
        {
            get { return _dragCursor; }
            set { _dragCursor = value; }
        }
    }
}