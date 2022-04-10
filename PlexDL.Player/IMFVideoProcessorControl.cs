using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("A3F675D5-6119-4f7f-A100-1D8B280F0EFB")]
    internal interface IMFVideoProcessorControl
    {
        //[PreserveSig]
        //HResult SetBorderColor(
        //    MFARGB pBorderColor
        //);
        [PreserveSig]
        HResult SetBorderColor();

        [PreserveSig]
        HResult SetSourceRectangle(
            MFRect pSrcRect
        );

        [PreserveSig]
        HResult SetDestinationRectangle(
            MFRect pDstRect
        );

        [PreserveSig]
        HResult SetMirror(
            //MF_VIDEO_PROCESSOR_MIRROR eMirror
        );
        //[PreserveSig]
        //HResult SetMirror();

        //[PreserveSig]
        //HResult SetRotation(
        //	MF_VIDEO_PROCESSOR_ROTATION eRotation
        //);
        [PreserveSig]
        HResult SetRotation();

        [PreserveSig]
        HResult SetConstrictionSize(
            [In] MFSize pConstrictionSize
        );
    }
}