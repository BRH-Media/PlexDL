using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), TypeLibType(TypeLibTypeFlags.FNonExtensible)]
    [ComImport]
    internal interface IMMDevice
    {
        [PreserveSig]
        void Activate([In] ref Guid iid, [In] uint dwClsCtx, [In] IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
        [PreserveSig]
        void OpenPropertyStore([In] uint stgmAccess, [MarshalAs(UnmanagedType.Interface)] out IPropertyStore ppProperties);
        [PreserveSig]
        void GetId([MarshalAs(UnmanagedType.LPWStr)] out string ppstrId);
        [PreserveSig]
        void GetState(out uint pdwState);
    }
}