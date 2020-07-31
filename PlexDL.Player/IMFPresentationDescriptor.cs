using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("03CB2711-24D7-4DB6-A17F-F3A7A479A536"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFPresentationDescriptor : IMFAttributes
    {
        #region IMFAttributes methods

        [PreserveSig]
        new HResult GetItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFPresentationDescriptor.GetItem", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
        );

        [PreserveSig]
        new HResult GetItemType(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out MFAttributeType pType
        );

        [PreserveSig]
        new HResult CompareItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant Value,
            [MarshalAs(UnmanagedType.Bool)] out bool pbResult
        );

        [PreserveSig]
        new HResult Compare(
            [MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs,
            MFAttributesMatchType MatchType,
            [MarshalAs(UnmanagedType.Bool)] out bool pbResult
        );

        [PreserveSig]
        new HResult GetUINT32(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out int punValue
        );

        [PreserveSig]
        new HResult GetUINT64(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out long punValue
        );

        [PreserveSig]
        new HResult GetDouble(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out double pfValue
        );

        [PreserveSig]
        new HResult GetGUID(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out Guid pguidValue
        );

        [PreserveSig]
        new HResult GetStringLength(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out int pcchLength
        );

        [PreserveSig]
        new HResult GetString(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszValue,
            int cchBufSize,
            out int pcchLength
        );

        [PreserveSig]
        new HResult GetAllocatedString(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue,
            out int pcchLength
        );

        [PreserveSig]
        new HResult GetBlobSize(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out int pcbBlobSize
        );

        [PreserveSig]
        new HResult GetBlob(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pBuf,
            int cbBufSize,
            out int pcbBlobSize
        );

        // Use GetBlob instead of this
        [PreserveSig]
        new HResult GetAllocatedBlob(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out IntPtr ip,  // Read w/Marshal.Copy, Free w/Marshal.FreeCoTaskMem
            out int pcbSize
        );

        [PreserveSig]
        new HResult GetUnknown(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppv
        );

        [PreserveSig]
        new HResult SetItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant Value
        );

        [PreserveSig]
        new HResult DeleteItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey
        );

        [PreserveSig]
        new HResult DeleteAllItems();

        [PreserveSig]
        new HResult SetUINT32(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            int unValue
        );

        [PreserveSig]
        new HResult SetUINT64(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            long unValue
        );

        [PreserveSig]
        new HResult SetDouble(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            double fValue
        );

        [PreserveSig]
        new HResult SetGUID(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidValue
        );

        [PreserveSig]
        new HResult SetString(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.LPWStr)] string wszValue
        );

        [PreserveSig]
        new HResult SetBlob(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pBuf,
            int cbBufSize
        );

        [PreserveSig]
        new HResult SetUnknown(
            [MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnknown
        );

        [PreserveSig]
        new HResult LockStore();

        [PreserveSig]
        new HResult UnlockStore();

        [PreserveSig]
        new HResult GetCount(
            out int pcItems
        );

        [PreserveSig]
        new HResult GetItemByIndex(
            int unIndex,
            out Guid pguidKey,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFPresentationDescriptor.GetItemByIndex", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
        );

        [PreserveSig]
        new HResult CopyAllItems(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest
        );

        #endregion IMFAttributes methods

        [PreserveSig]
        HResult GetStreamDescriptorCount(
            out int pdwDescriptorCount
        );

        [PreserveSig]
        HResult GetStreamDescriptorByIndex(
            [In] int dwIndex,
            [MarshalAs(UnmanagedType.Bool)] out bool pfSelected,
            [MarshalAs(UnmanagedType.Interface)] out IMFStreamDescriptor ppDescriptor
        );

        [PreserveSig]
        HResult SelectStream(
            [In] int dwDescriptorIndex
        );

        [PreserveSig]
        HResult DeselectStream(
            [In] int dwDescriptorIndex
        );

        [PreserveSig]
        HResult Clone(
            [MarshalAs(UnmanagedType.Interface)] out IMFPresentationDescriptor ppPresentationDescriptor
        );
    }
}