using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential), UnmanagedName("MFVideoAlphaBitmap")]
    internal sealed class MFVideoAlphaBitmap
    {
        public bool GetBitmapFromDC;
        public IntPtr stru;
        public MFVideoAlphaBitmapParams paras;
    }
}