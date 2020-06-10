namespace PlexDL.Player
{
    [UnmanagedName("MFASYNC_CALLBACK_QUEUE_ defines")]
    internal enum MFAsyncCallbackQueue
    {
        Undefined = 0x00000000,
        Standard = 0x00000001,
        RT = 0x00000002,
        IO = 0x00000003,
        Timer = 0x00000004,
        MultiThreaded = 0x00000005,
        LongFunction = 0x00000007,
        PrivateMask = unchecked((int)0xFFFF0000),
        All = unchecked((int)0xFFFFFFFF)
    }
}