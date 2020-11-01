using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential), UnmanagedName("MF_SINK_WRITER_STATISTICS")]
    internal struct MF_SINK_WRITER_STATISTICS
    {
        public int cb;

        public long llLastTimestampReceived;
        public long llLastTimestampEncoded;
        public long llLastTimestampProcessed;
        public long llLastStreamTickReceived;
        public long llLastSinkSampleRequest;

        public long qwNumSamplesReceived;
        public long qwNumSamplesEncoded;
        public long qwNumSamplesProcessed;
        public long qwNumStreamTicksReceived;

        public int dwByteCountQueued;
        public long qwByteCountProcessed;

        public int dwNumOutstandingSinkSampleRequests;

        public int dwAverageSampleRateReceived;
        public int dwAverageSampleRateEncoded;
        public int dwAverageSampleRateProcessed;
    }
}