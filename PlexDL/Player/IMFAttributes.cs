using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace PlexDL.Player
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [Guid("2CD2D921-C447-44A7-A13C-4ADABFC247E3")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFAttributes
    {
        [PreserveSig]
        HResult GetItem(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            [In] [Out] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFAttributes.GetItem", MarshalTypeRef = typeof(PVMarshaler))]
            PropVariant pValue
        );

        [PreserveSig]
        HResult GetItemType(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            out MFAttributeType pType
        );

        [PreserveSig]
        HResult CompareItem(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            ConstPropVariant Value,
            [MarshalAs(UnmanagedType.Bool)] out bool pbResult
        );

        [PreserveSig]
        HResult Compare(
            [MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs,
            MFAttributesMatchType MatchType,
            [MarshalAs(UnmanagedType.Bool)] out bool pbResult
        );

        [PreserveSig]
        HResult GetUINT32(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            out int punValue
        );

        [PreserveSig]
        HResult GetUINT64(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            out long punValue
        );

        [PreserveSig]
        HResult GetDouble(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            out double pfValue
        );

        [PreserveSig]
        HResult GetGUID(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            out Guid pguidValue
        );

        [PreserveSig]
        HResult GetStringLength(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            out int pcchLength
        );

        [PreserveSig]
        HResult GetString(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            [Out] [MarshalAs(UnmanagedType.LPWStr)]
            StringBuilder pwszValue,
            int cchBufSize,
            out int pcchLength
        );

        [PreserveSig]
        HResult GetAllocatedString(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue,
            out int pcchLength
        );

        [PreserveSig]
        HResult GetBlobSize(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            out int pcbBlobSize
        );

        [PreserveSig]
        HResult GetBlob(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            [Out] [MarshalAs(UnmanagedType.LPArray)]
            byte[] pBuf,
            int cbBufSize,
            out int pcbBlobSize
        );

        // Use GetBlob instead of this
        [PreserveSig]
        HResult GetAllocatedBlob(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            out IntPtr ip, // Read w/Marshal.Copy, Free w/Marshal.FreeCoTaskMem
            out int pcbSize
        );

        [PreserveSig]
        HResult GetUnknown(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppv
        );

        [PreserveSig]
        HResult SetItem(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            ConstPropVariant Value
        );

        [PreserveSig]
        HResult DeleteItem(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey
        );

        [PreserveSig]
        HResult DeleteAllItems();

        [PreserveSig]
        HResult SetUINT32(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            int unValue
        );

        [PreserveSig]
        HResult SetUINT64(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            long unValue
        );

        [PreserveSig]
        HResult SetDouble(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            double fValue
        );

        [PreserveSig]
        HResult SetGUID(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidValue
        );

        [PreserveSig]
        HResult SetString(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string wszValue
        );

        [PreserveSig]
        HResult SetBlob(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidKey,
            [In] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] pBuf,
            int cbBufSize
        );

        [PreserveSig]
        HResult SetUnknown(
            [MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In] [MarshalAs(UnmanagedType.IUnknown)]
            object pUnknown
        );

        [PreserveSig]
        HResult LockStore();

        [PreserveSig]
        HResult UnlockStore();

        [PreserveSig]
        HResult GetCount(
            out int pcItems
        );

        [PreserveSig]
        HResult GetItemByIndex(
            int unIndex,
            out Guid pguidKey,
            [In] [Out] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFAttributes.GetItemByIndex", MarshalTypeRef = typeof(PVMarshaler))]
            PropVariant pValue
        );

        [PreserveSig]
        HResult CopyAllItems(
            [In] [MarshalAs(UnmanagedType.Interface)]
            IMFAttributes pDest
        );
    }
}