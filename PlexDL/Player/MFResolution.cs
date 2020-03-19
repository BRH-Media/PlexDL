using System;

namespace PlexDL.Player
{
    [Flags]
    [UnmanagedName("MF_RESOLUTION_* defines")]
    internal enum MFResolution
    {
        None = 0x0,
        MediaSource = 0x00000001,
        ByteStream = 0x00000002,
        ContentDoesNotHaveToMatchExtensionOrMimeType = 0x00000010,
        KeepByteStreamAliveOnFail = 0x00000020,
        DisableLocalPlugins = 0x40,
        PluginControlPolicyApprovedOnly = 0x80,
        PluginControlPolicyWebOnly = 0x100,
        PluginControlPolicyWebOnlyEdgemode = 0x00000200,
        Read = 0x00010000,
        Write = 0x00020000,
    }
}