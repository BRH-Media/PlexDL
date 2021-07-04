using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     Guid("8f02d140-56fc-4302-a705-3a97c78be779"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFCaptureEngineClassFactory
    {
        [PreserveSig]
        HResult CreateInstance(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid clsid,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject
        );
    }
}