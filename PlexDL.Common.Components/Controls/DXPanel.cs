using System.Windows.Forms;

namespace PlexDL.Common.Components.Controls
{
    public class DXPanel : Panel
    {
        public DXPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
        }
    }
}