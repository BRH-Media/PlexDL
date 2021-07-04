namespace PlexDL.Player
{
    [UnmanagedName("MFSTARTUP_* defines")]
    internal enum MFStartup
    {
        NoSocket = 0x1,
#pragma warning disable CA1069 // Enums values should not be duplicated
        Lite = 0x1,
#pragma warning restore CA1069 // Enums values should not be duplicated
        Full = 0
    }
}