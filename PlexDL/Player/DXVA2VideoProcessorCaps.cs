using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential)]
    [UnmanagedName("DXVA2_VideoProcessorCaps")]
    internal struct DXVA2VideoProcessorCaps
    {
        public int DeviceCaps;
        public int InputPool;
        public int NumForwardRefSamples;
        public int NumBackwardRefSamples;
        public int Reserved;
        public int DeinterlaceTechnology;
        public int ProcAmpControlCaps;
        public int VideoProcessorOperations;
        public int NoiseFilterTechnology;
        public int DetailFilterTechnology;
    }
}