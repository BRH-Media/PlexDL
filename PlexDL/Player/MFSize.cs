using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [UnmanagedName("SIZE")]
    internal sealed class MFSize
    {
        public int cx;
        public int cy;

        public MFSize()
        {
            cx = 0;
            cy = 0;
        }

        public MFSize(int iWidth, int iHeight)
        {
            cx = iWidth;
            cy = iHeight;
        }

        public int Width
        {
            get => cx;
            set => cx = value;
        }

        public int Height
        {
            get => cy;
            set => cy = value;
        }
    }
}