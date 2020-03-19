namespace PlexDL.Player
{
    [UnmanagedName("MF_TOPOLOGY_TYPE")]
    internal enum MFTopologyType
    {
        Max = -1,
        OutputNode = 0,
        SourcestreamNode = 1,
        TeeNode = 3,
        TransformNode = 2
    }
}