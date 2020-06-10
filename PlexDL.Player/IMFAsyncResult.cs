using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("AC6B7889-0740-4D51-8619-905994A55CC6")]
    internal interface IMFAsyncResult
    {
        [PreserveSig]
        HResult GetState(
            [MarshalAs(UnmanagedType.IUnknown)] out object ppunkState
        );

        [PreserveSig]
        HResult GetStatus();

        [PreserveSig]
        HResult SetStatus(
            [In, MarshalAs(UnmanagedType.Error)] HResult hrStatus
        );

        [PreserveSig]
        HResult GetObject(
            [MarshalAs(UnmanagedType.Interface)] out object ppObject
        );

        [PreserveSig]
        IntPtr GetStateNoAddRef();
    }
}