using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential)]
    [UnmanagedName("DXVA2_ValueRange")]
    internal struct DXVA2ValueRange
    {
        public int MinValue;
        public int MaxValue;
        public int DefaultValue;
        public int StepSize;
    }
}