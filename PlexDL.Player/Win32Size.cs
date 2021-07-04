using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Win32Size
    {
        public int cx;
        public int cy;
    }
}