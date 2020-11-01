using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("045FA593-8799-42B8-BC8D-8968C6453507")]
    internal interface IMFMediaBuffer
    {
        [PreserveSig]
        HResult Lock(
            out IntPtr ppbBuffer,
            out int pcbMaxLength,
            out int pcbCurrentLength
        );

        [PreserveSig]
        HResult Unlock();

        [PreserveSig]
        HResult GetCurrentLength(
            out int pcbCurrentLength
        );

        [PreserveSig]
        HResult SetCurrentLength(
            [In] int cbCurrentLength
        );

        [PreserveSig]
        HResult GetMaxLength(
            out int pcbMaxLength
        );
    }
}