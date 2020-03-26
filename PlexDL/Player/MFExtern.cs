using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    internal static class MFExtern
    {
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mfplat.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFShutdown();

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mfplat.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFStartup(int Version, MFStartup dwFlags);

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        //[DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        //public static extern HResult MFCreatePresentationDescriptor(
        //    int cStreamDescriptors,
        //    [In, MarshalAs(UnmanagedType.LPArray)] IMFStreamDescriptor[] apStreamDescriptors,
        //    out IMFPresentationDescriptor ppPresentationDescriptor
        //);

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFGetService(
            [In] [MarshalAs(UnmanagedType.Interface)]
            object punkObject,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidService,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid riid,
            [Out] [MarshalAs(UnmanagedType.Interface)]
            out object ppvObject
        );

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateVideoRendererActivate(
            IntPtr hwndVideo,
            out IMFActivate ppActivate
        );

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateTopologyNode(
            MFTopologyType NodeType,
            out IMFTopologyNode ppNode
        );

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateSourceResolver(
            out IMFSourceResolver ppISourceResolver
        );

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateMediaSession(
            IMFAttributes pConfiguration,
            out IMFMediaSession ppMediaSession
        );

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateTopology(
            out IMFTopology ppTopo
        );

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateAudioRendererActivate(
            out IMFActivate ppActivate
        );

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mfplat.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateAttributes(
            out IMFAttributes ppMFAttributes,
            int cInitialSize
        );

        public static HResult MFGetAttributeRatio(
            IMFAttributes pAttributes,
            Guid guidKey,
            out int punNumerator,
            out int punDenominator
        )
        {
            return MFGetAttribute2UINT32asUINT64(pAttributes, guidKey, out punNumerator, out punDenominator);
        }

        public static HResult MFGetAttribute2UINT32asUINT64(IMFAttributes pAttributes, Guid guidKey, out int punHigh32, out int punLow32)
        {
            long unPacked;
            HResult hr;

            hr = pAttributes.GetUINT64(guidKey, out unPacked);
            if (hr < 0)
            {
                punHigh32 = punLow32 = 0;
                return hr;
            }

            Unpack2UINT32AsUINT64(unPacked, out punHigh32, out punLow32);

            return hr;
        }

        public static void Unpack2UINT32AsUINT64(long unPacked, out int punHigh, out int punLow)
        {
            var ul = (ulong)unPacked;
            punHigh = (int)(ul >> 32);
            punLow = (int)(ul & 0xffffffff);
        }

        //[DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        //public static extern HResult MFTEnumEx(
        //    [In] Guid MFTransformCategory,
        //    MFT_EnumFlag Flags,
        //    [In, MarshalAs(UnmanagedType.LPStruct)] MFTRegisterTypeInfo pInputType,
        //    [In, MarshalAs(UnmanagedType.LPStruct)] MFTRegisterTypeInfo pOutputType,
        //    //[Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown, SizeParamIndex = 5)] out IMFActivate[] pppMFTActivate,
        //    [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown)] out IMFActivate[] pppMFTActivate,
        //    out int pnumMFTActivate
        //);

        // use with NET 2.0, 3.0 and  3.5 (returns only 1 device) (because of SizeParamIndex)
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true, EntryPoint = "MFEnumDeviceSources")]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFEnumDeviceSourcesEx(
            IMFAttributes pAttributes,
            [MarshalAs(UnmanagedType.LPArray)] out IMFActivate[] pppSourceActivate,
            out int pcSourceActivate);

        // Works only with .NET Framework version 4.0 or higher (because of SizeParamIndex)
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFEnumDeviceSources(
            IMFAttributes pAttributes,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            out IMFActivate[] pppSourceActivate,
            out int pcSourceActivate);

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        //[DllImport("mf.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        //public static extern HResult MFCreateDeviceSourceActivate(
        //    IMFAttributes pAttributes,
        //    out IMFActivate ppActivate
        //);

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateDeviceSource(
            IMFAttributes pAttributes,
            out IMFMediaSource ppSource
        );

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        //[DllImport("Mfreadwrite.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        //public static extern HResult MFCreateSourceReaderFromMediaSource(
        //    IMFMediaSource pMediaSource,
        //    IMFAttributes pAttributes,
        //    out IMFSourceReaderAsync ppSourceReader
        //);

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Mfreadwrite.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateSourceReaderFromMediaSource(
            IMFMediaSource pMediaSource,
            IMFAttributes pAttributes,
            out IMFSourceReader ppSourceReader
        );

        //[DllImport("Mfreadwrite.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        //public static extern HResult MFCreateSourceReaderFromURL(
        //    [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
        //    IMFAttributes pAttributes,
        //    out IMFSourceReader ppSourceReader
        //);

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Mfreadwrite.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateSinkWriterFromURL(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pwszOutputURL,
            IMFByteStream pByteStream,
            IMFAttributes pAttributes,
            out IMFSinkWriter ppSinkWriter
        );

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mfplat.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateMediaType(
            out IMFMediaType ppMFType
        );

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mfplat.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateCollection(
            out IMFCollection ppIMFCollection
        );

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateAggregateSource(
            IMFCollection pSourceCollection,
            out IMFMediaSource ppAggSource
        );
    }
}