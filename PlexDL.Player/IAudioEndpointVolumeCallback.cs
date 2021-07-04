using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    internal interface IAudioEndpointVolumeCallback
    {
        [PreserveSig]
        int OnNotify([In] IntPtr notificationData);
    }
}