using System.Windows.Forms;

namespace PlexDL.Common.Components
{
    public class DXPanel : Panel
    {
        public DXPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
        }
    }
}