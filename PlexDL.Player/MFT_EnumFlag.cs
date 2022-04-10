using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("MFT_ENUM_FLAG")]
    internal enum MFT_EnumFlag
    {
        None = 0x00000000,
        SyncMFT = 0x00000001,   // Enumerates V1 MFTs. This is default.
        AsyncMFT = 0x00000002,   // Enumerates only software async MFTs also known as V2 MFTs
        Hardware = 0x00000004,   // Enumerates V2 hardware async MFTs
        FieldOfUse = 0x00000008,   // Enumerates MFTs that require unlocking
        LocalMFT = 0x00000010,   // Enumerates Locally (in-process) registered MFTs
        TranscodeOnly = 0x00000020,   // Enumerates decoder MFTs used by transcode only
        SortAndFilter = 0x00000040,   // Apply system local, do not use and preferred sorting and filtering
        SortAndFilterApprovedOnly = 0x000000C0,   // Similar to MFT_ENUM_FLAG_SORTANDFILTER, but apply a local policy of: MF_PLUGIN_CONTROL_POLICY_USE_APPROVED_PLUGINS
        SortAndFilterWebOnly = 0x00000140,   // Similar to MFT_ENUM_FLAG_SORTANDFILTER, but apply a local policy of: MF_PLUGIN_CONTROL_POLICY_USE_WEB_PLUGINS
        SortAndFilterWebOnlyEdgemode = 0x00000240,
        All = 0x0000003F    // Enumerates all MFTs including SW and HW MFTs and applies filtering
    }
}