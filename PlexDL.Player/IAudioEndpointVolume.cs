using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioEndpointVolume
    {
        //[PreserveSig]
        //int NotImpl1();
        //[PreserveSig]
        //int NotImpl2();

        [PreserveSig]
        int RegisterControlChangeNotify([In][MarshalAs(UnmanagedType.Interface)] IAudioEndpointVolumeCallback client);
        [PreserveSig]
        int UnregisterControlChangeNotify([In][MarshalAs(UnmanagedType.Interface)] IAudioEndpointVolumeCallback client);
        [PreserveSig]
        int GetChannelCount([Out][MarshalAs(UnmanagedType.U4)] out uint channelCount);
        [PreserveSig]
        int SetMasterVolumeLevel([In][MarshalAs(UnmanagedType.R4)] float level, [In][MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);
        [PreserveSig]
        int SetMasterVolumeLevelScalar([In][MarshalAs(UnmanagedType.R4)] float level, [In][MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);
        [PreserveSig]
        int GetMasterVolumeLevel([Out][MarshalAs(UnmanagedType.R4)] out float level);
        [PreserveSig]
        int GetMasterVolumeLevelScalar([Out][MarshalAs(UnmanagedType.R4)] out float level);
        [PreserveSig]
        int SetChannelVolumeLevel([In][MarshalAs(UnmanagedType.U4)] uint channelNumber, [In][MarshalAs(UnmanagedType.R4)] float level, [In][MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);
        [PreserveSig]
        int SetChannelVolumeLevelScalar([In][MarshalAs(UnmanagedType.U4)] uint channelNumber, [In][MarshalAs(UnmanagedType.R4)] float level, [In][MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);
        [PreserveSig]
        int GetChannelVolumeLevel([In][MarshalAs(UnmanagedType.U4)] uint channelNumber, [Out][MarshalAs(UnmanagedType.R4)] out float level);
        [PreserveSig]
        int GetChannelVolumeLevelScalar([In][MarshalAs(UnmanagedType.U4)] uint channelNumber, [Out][MarshalAs(UnmanagedType.R4)] out float level);
        [PreserveSig]
        int SetMute( [In][MarshalAs(UnmanagedType.Bool)] bool isMuted, [In][MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);
        [PreserveSig]
        int GetMute( [Out][MarshalAs(UnmanagedType.Bool)] out bool isMuted);
        //[PreserveSig]
        //int GetVolumeStepInfo([Out][MarshalAs(UnmanagedType.U4)] out uint step, [Out][MarshalAs(UnmanagedType.U4)] out uint stepCount);
    }
}