using System;

namespace PlexDL.Player
{
    internal static class PropertyKeys
    {
        // Device Info
        internal static PropertyKey PKEY_Device_Description = new PropertyKey { fmtid = new Guid(unchecked((int)0xa45c254e), unchecked((short)0xdf1c), 0x4efd, 0x80, 0x20, 0x67, 0xd1, 0x46, 0xa8, 0x50, 0xe0), pID = 2 };
        internal static PropertyKey PKEY_DeviceInterface_FriendlyName = new PropertyKey { fmtid = new Guid(unchecked(0x026e516e), 0xb814, 0x414b, 0x83, 0xcd, 0x85, 0x6d, 0x6f, 0xef, 0x48, 0x22), pID = 2 };

        // Shell Metadata (MediaTags)
        internal static PropertyKey PKEY_Music_Artist = new PropertyKey { fmtid = new Guid(unchecked(0x56A3372E), 0xCE9C, 0x11D2, 0x9F, 0x0E, 0x00, 0x60, 0x97, 0xC6, 0x86, 0xF6), pID = 2 };
        internal static PropertyKey PKEY_Music_AlbumArtist = new PropertyKey { fmtid = new Guid(unchecked(0x56A3372E), 0xCE9C, 0x11D2, 0x9F, 0x0E, 0x00, 0x60, 0x97, 0xC6, 0x86, 0xF6), pID = 13 };
        internal static PropertyKey PKEY_Title = new PropertyKey { fmtid = new Guid(unchecked(0xF29F85E0), 0x4FF9, 0x1068, 0xAB, 0x91, 0x08, 0x00, 0x2B, 0x27, 0xB3, 0xD9), pID = 2 };
        internal static PropertyKey PKEY_Music_AlbumTitle = new PropertyKey { fmtid = new Guid(unchecked(0x56A3372E), 0xCE9C, 0x11D2, 0x9F, 0x0E, 0x00, 0x60, 0x97, 0xC6, 0x86, 0xF6), pID = 4 };
        internal static PropertyKey PKEY_Music_Genre = new PropertyKey { fmtid = new Guid(unchecked(0x56A3372E), 0xCE9C, 0x11D2, 0x9F, 0x0E, 0x00, 0x60, 0x97, 0xC6, 0x86, 0xF6), pID = 11 };
        internal static PropertyKey PKEY_Media_Duration = new PropertyKey { fmtid = new Guid(unchecked(0x64440490), 0x4C8B, 0x11D1, 0x8B, 0x70, 0x08, 0x00, 0x36, 0xB1, 0x1A, 0x03), pID = 3 };
        internal static PropertyKey PKEY_Music_TrackNumber = new PropertyKey { fmtid = new Guid(unchecked(0x56A3372E), 0xCE9C, 0x11D2, 0x9F, 0x0E, 0x00, 0x60, 0x97, 0xC6, 0x86, 0xF6), pID = 7 };
        internal static PropertyKey PKEY_Media_Year = new PropertyKey { fmtid = new Guid(unchecked(0x56A3372E), 0xCE9C, 0x11D2, 0x9F, 0x0E, 0x00, 0x60, 0x97, 0xC6, 0x86, 0xF6), pID = 5 };
        internal static PropertyKey PKEY_ThumbnailStream = new PropertyKey { fmtid = new Guid(unchecked(0xF29F85E0), 0x4FF9, 0x1068, 0xAB, 0x91, 0x08, 0x00, 0x2B, 0x27, 0xB3, 0xD9), pID = 27 };

        // Network Statistics
        // MFNETSOURCE_STATISTICS - this PKEY is used for all statistics with the pId changed to the proper statistics ID
        internal static PropertyKey PKEY_NetSource_Statistics = new PropertyKey { fmtid = new Guid(unchecked(0x3cb1f274), 0x0505, 0x4c5d, 0xae, 0x71, 0x0a, 0x55, 0x63, 0x44, 0xef, 0xa1), pID = 0 };
    }
}