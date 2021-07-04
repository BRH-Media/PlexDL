using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Win32Point
    {
        public int x;
        public int y;

        public Win32Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}