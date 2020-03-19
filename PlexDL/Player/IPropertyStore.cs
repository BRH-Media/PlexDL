using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
    internal interface IPropertyStore
    {
        [PreserveSig]
        HResult GetCount(
            out int cProps
        );

        [PreserveSig]
        HResult GetAt(
            [In] int iProp,
            [Out] PropertyKey pkey
        );

        [PreserveSig]
        HResult GetValue(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            PropertyKey key,
            [In] [Out] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IPropertyStore.GetValue", MarshalTypeRef = typeof(PVMarshaler))]
            PropVariant pv
        );

        [PreserveSig]
        HResult SetValue(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            PropertyKey key,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            ConstPropVariant propvar
        );

        [PreserveSig]
        HResult Commit();
    }
}