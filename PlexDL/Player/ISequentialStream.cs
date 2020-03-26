using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000000c-0000-0000-C000-000000000046")]
    internal interface ISequentialStream
    {
        [PreserveSig]
        HResult Read(
            [In] [Out] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
            byte[] buffer,
            [In] int bytesCount,
            [In] IntPtr bytesRead
        );

        [PreserveSig]
        HResult Write(
            [In] [Out] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
            byte[] buffer,
            [In] int bytesCount,
            [In] IntPtr bytesWritten
        );
    }
}