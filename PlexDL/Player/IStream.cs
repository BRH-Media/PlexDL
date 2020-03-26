using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using STATSTG = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace PlexDL.Player
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0c733a30-2a1c-11ce-ade5-00aa0044773d")]
    internal interface IStream : ISequentialStream
    {
        #region ISequentialStream Methods

        [PreserveSig]
        new HResult Read(
            [In] [Out] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
            byte[] buffer,
            [In] int bytesCount,
            [In] IntPtr bytesRead
        );

        [PreserveSig]
        new HResult Write(
            [In] [Out] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
            byte[] buffer,
            [In] int bytesCount,
            [In] IntPtr bytesWritten
        );

        #endregion ISequentialStream Methods

        [PreserveSig]
        HResult Seek(
            [In] long offset,
            [In] SeekOrigin origin,
            [In] IntPtr newPosition
        );

        [PreserveSig]
        HResult SetSize(
            [In] long newSize
        );

        [PreserveSig]
        HResult CopyTo(
            [In] IStream otherStream,
            [In] long bytesCount,
            [In] IntPtr bytesRead,
            [In] IntPtr bytesWritten
        );

        //[PreserveSig]
        //HResult Commit(
        //    [In] STGC commitFlags
        //    );
        [PreserveSig]
        HResult Commit();

        [PreserveSig]
        HResult Revert();

        [PreserveSig]
        HResult LockRegion(
            [In] long offset,
            [In] long bytesCount,
            [In] int lockType
        );

        [PreserveSig]
        HResult UnlockRegion(
            [In] long offset,
            [In] long bytesCount,
            [In] int lockType
        );

        [PreserveSig]
        HResult Stat(
            [Out] out STATSTG statstg,
            [In] STATFLAG statFlag
        );

        [PreserveSig]
        HResult Clone(
            [Out] out IStream clonedStream
        );
    }
}