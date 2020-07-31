using System;
using System.Windows.Forms;

namespace PlexDL.Player
{
    internal sealed class VideoDisplay : Control
    {
        public VideoDisplay()
        {
            SetStyle(ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0084) m.Result = (IntPtr)(-1);
            else base.WndProc(ref m);
        }
    }
}