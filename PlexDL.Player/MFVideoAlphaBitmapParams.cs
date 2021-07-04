using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential), UnmanagedName("MFVideoAlphaBitmapParams")]
    internal sealed class MFVideoAlphaBitmapParams
    {
        public MFVideoAlphaBitmapFlags dwFlags;
        public int clrSrcKey;
        public MFRect rcSrc;
        public MFVideoNormalizedRect nrcDest;
        public float fAlpha;
        public int dwFilterMode;
    }
}