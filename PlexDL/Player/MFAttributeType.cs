namespace PlexDL.Player
{
    [UnmanagedName("MF_ATTRIBUTE_TYPE")]
    internal enum MFAttributeType
    {
        None = 0x0,
        Blob = 0x1011,
        Double = 0x5,
        Guid = 0x48,
        IUnknown = 13,
        String = 0x1f,
        Uint32 = 0x13,
        Uint64 = 0x15
    }
}