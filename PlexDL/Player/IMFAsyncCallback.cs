using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("A27003CF-2354-4F2A-8D6A-AB7CFF15437E")]
    internal interface IMFAsyncCallback
    {
        [PreserveSig]
        HResult GetParameters(
            out MFASync pdwFlags,
            out MFAsyncCallbackQueue pdwQueue
        );

        [PreserveSig]
        HResult Invoke(
            [In] [MarshalAs(UnmanagedType.Interface)]
            IMFAsyncResult pAsyncResult
        );
    }
}