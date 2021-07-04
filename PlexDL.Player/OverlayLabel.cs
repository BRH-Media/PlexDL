using System;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that can be used as a base class for labels on PVS.MediaPlayer display overlays. Contains logic for better display of text on transparent overlays. 
    /// </summary>
    [CLSCompliant(true)]
    public class OverlayLabel : Label
    {
        /// <summary>
        /// Initializes a new instance of the OverlayLabel class.
        /// </summary>
        public OverlayLabel()
        {
            UseCompatibleTextRendering = true;
        }

        /// <summary>
        /// Raises the Control.Paint event.
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            base.OnPaint(e);
        }
    }
}