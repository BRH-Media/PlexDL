using System;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that can be used as a base class for PVS.MediaPlayer display overlays of type Form. Contains logic to prevent unwanted activation of the overlay.
    /// </summary>
    [CLSCompliant(true)]
    public class OverlayForm : Form
    {
        private bool _clickThrough;

        /// <summary>
        /// Gets or sets a value indicating whether the overlay is transparent for mouse events (default: false).
        /// </summary>
        public bool ClickThrough
        {
            get { return _clickThrough; }
            set { _clickThrough = value; }
        }

        /// <summary>
        /// Gets a (system) value indicating the window will not be activated when it is shown.
        /// </summary>
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        /// <summary>
        /// Raises the Control.HandleCreated event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // switch off form animation
            if (Environment.OSVersion.Version.Major >= 6)
            {
#pragma warning disable CA1806 // Do not ignore method results
                SafeNativeMethods.DwmSetWindowAttribute(Handle, SafeNativeMethods.DWMWA_TRANSITIONS_FORCEDISABLED, true, 4);
#pragma warning restore CA1806 // Do not ignore method results
            }
        }

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows Message to process.</param>
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST      = 0x0084;
            const int HTTRANSPARENT     = -1;
            const int WM_MOUSEACTIVATE  = 0x0021;
            const int MA_NOACTIVATE     = 0x0003;

            // prevent form activation
            if (m.Msg == WM_NCHITTEST)
            {
                if (_clickThrough) m.Result = (IntPtr)HTTRANSPARENT;   // this is for "click through":
                else base.WndProc(ref m);
            }
            else if (m.Msg == WM_MOUSEACTIVATE) m.Result = (IntPtr)MA_NOACTIVATE;   // this is for "don't activate with mouse click":
            else base.WndProc(ref m);
        }
    }
}