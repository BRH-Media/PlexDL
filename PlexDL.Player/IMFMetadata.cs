using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("F88CFB8C-EF16-4991-B450-CB8C69E51704")]
    internal interface IMFMetadata
    {
        [PreserveSig]
        HResult SetLanguage(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszRFC1766
        );

        [PreserveSig]
        HResult GetLanguage(
            [MarshalAs(UnmanagedType.LPWStr)] out string ppwszRFC1766
        );

        [PreserveSig]
        HResult GetAllLanguages(
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFMetadata.GetAllLanguages", MarshalTypeRef = typeof(PVMarshaler))] PropVariant ppvLanguages
        );

        [PreserveSig]
        HResult SetProperty(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszName,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant ppvValue
        );

        [PreserveSig]
        HResult GetProperty(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszName,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFMetadata.GetProperty", MarshalTypeRef = typeof(PVMarshaler))] PropVariant ppvValue
        );

        [PreserveSig]
        HResult DeleteProperty(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszName
        );

        [PreserveSig]
        HResult GetAllPropertyNames(
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFMetadata.GetAllPropertyNames", MarshalTypeRef = typeof(PVMarshaler))] PropVariant ppvNames
        );
    }
}