namespace PlexDL.Player
{
    [UnmanagedName("MFSTREAMSINK_MARKER_TYPE")]
    internal enum MFStreamSinkMarkerType
    {
        Default,
        EndOfSegment,
        Tick,
        Event
    }
}