using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("5BC8A76B-869A-46A3-9B03-FA218A66AEBE")]
    internal interface IMFCollection
    {
        [PreserveSig]
        HResult GetElementCount(
            out int pcElements
        );

        [PreserveSig]
        HResult GetElement(
            [In] int dwElementIndex,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnkElement
        );

        [PreserveSig]
        HResult AddElement(
            [In] [MarshalAs(UnmanagedType.IUnknown)]
            object pUnkElement
        );

        [PreserveSig]
        HResult RemoveElement(
            [In] int dwElementIndex,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnkElement
        );

        [PreserveSig]
        HResult InsertElementAt(
            [In] int dwIndex,
            [In] [MarshalAs(UnmanagedType.IUnknown)]
            object pUnknown
        );

        [PreserveSig]
        HResult RemoveAllElements();
    }
}