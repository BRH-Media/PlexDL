/****************************************************************

    PVS.MediaPlayer - Version 1.0
    September 2020, The Netherlands
    © Copyright 2020 PVS The Netherlands - licensed under The Code Project Open License (CPOL)

    PVS.MediaPlayer uses (part of) the Media Foundation .NET library by nowinskie and snarfle (https://sourceforge.net/projects/mfnet).
    Licensed under either Lesser General Public License v2.1 or BSD.  See license.txt or BSDL.txt for details (http://mfnet.sourceforge.net).

    ****************

    For use with Microsoft Windows 7 or higher, Microsoft .NET Framework version 4.0 or higher and WinForms (any CPU).
    Created with Microsoft Visual Studio.

    Article on CodeProject with information on the use of the PVS.MediaPlayer library:
    https://www.codeproject.com/Articles/109714/PVS-MediaPlayer-Audio-and-Video-Player-Library

    ****************

    The PVS.MediaPlayer library source code is divided into 8 files:

    1. Player.cs        - main source code
    2. SubClasses.cs    - various grouping and information classes
    3. Interop.cs       - unmanaged Win32 functions
    4. AudioDevices.cs  - audio devices and peak meters
    5. DisplayClones.cs - multiple video displays 
    6. CursorHide.cs    - hides the mouse cursor during inactivity
    7. Subtitles.cs     - subrip (.srt) subtitles
    8. Infolabel.cs     - custom ToolTip

    Required references:
    System
    System.Drawing
    System.Windows.Forms

    ****************

    This file: Interop.cs

    Native Methods
    Taskbar Indicator
    Media Foundation
    Windows Core Audio API

    ****************

    Many thanks to Microsoft (Windows, .NET Framework, Visual Studio and others), all the people
    writing about programming on the internet (a great source for ideas and solving problems),
    the websites publishing those or other writings about programming, the people responding to the
    PVS.MediaPlayer articles with comments and suggestions and, of course, the people at CodeProject.

    Special thanks to the creators of Media Foundation .NET for their great library.

    Special thanks to Sean Ewington and Deeksha Shenoy of CodeProject who also took care of publishing the many
    code updates and changes in the PVS.MediaPlayer articles in a friendly, fast, and highly competent manner.

    Peter Vegter
    September 2020, The Netherlands

    ****************************************************************/

#region Usings

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

#region Disable Some Compiler Warnings

#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0018 // Inline variable declaration
#pragma warning disable IDE0017 // Simplify object initialization
#pragma warning disable IDE0041 // Use 'is null' check
#pragma warning disable IDE0038 // Use pattern matching

#endregion


namespace PlexDL.Player
{

    // ******************************** Native Methods - DLL Import

    #region Native Methods

    [System.Security.SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        // ******************************** Win32 Windows

        #region Win32 Windows

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        internal const uint SWP_NOSIZE = 0x1;
        internal const uint SWP_NOMOVE = 0X2;
        internal const uint SWP_NOZORDER = 0X4;
        internal const uint SWP_HIDEWINDOW = 0x80;
        internal const uint SWP_SHOWWINDOW = 0x40;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [StructLayout(LayoutKind.Sequential)]
        internal struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

        #endregion

        // ******************************** BitBlt (VideoCopy)

        #region BitBlt

        internal const int SRCCOPY = 0xCC0020;
        //internal const int CAPTUREBLT       = 0x40000000;
        //internal const int MIXBLT           = 0x40CC0020;

        internal const uint SRCCOPY_U = 0xCC0020;
        //internal const uint CAPTUREBLT_U    = 0x40000000;
        //internal const uint MIXBLT_U        = 0x40CC0020;

        internal const int STRETCH_HALFTONE = 4; // quality mode setting for SetStretchBltMode

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        [DllImport("gdi32.dll")]
        internal static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest,
        IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, uint dwRop);

        [DllImport("gdi32.dll")]
        internal static extern int SetStretchBltMode(IntPtr hdc, int iStretchMode);

        [DllImport("Msimg32.dll")]
        internal static extern bool TransparentBlt(IntPtr hdcDest, // handle to destination DC
        int nXOriginDest, // x-coord of destination upper-left corner
        int nYOriginDest, // y-coord of destination upper-left corner
        int nWidthDest, // width of destination rectangle
        int hHeightDest, // height of destination rectangle
        IntPtr hdcSrc, // handle to source DC
        int nXOriginSrc, // x-coord of source upper-left corner
        int nYOriginSrc, // y-coord of source upper-left corner
        int nWidthSrc, // width of source rectangle
        int nHeightSrc, // height of source rectangle
        int crTransparent // color to make transparent
        );

        [DllImport("gdi32.dll", EntryPoint = "GdiAlphaBlend")]
        internal static extern bool AlphaBlend(IntPtr hdcDest,
        int nXOriginDest,
        int nYOriginDest,
        int nWidthDest,
        int nHeightDest,
        IntPtr hdcSrc,
        int nXOriginSrc,
        int nYOriginSrc,
        int nWidthSrc,
        int nHeightSrc,
        BLENDFUNCTION blendFunction);

        internal const byte AC_SRC_OVER = 0x00;
        internal const byte AC_SRC_ALPHA = 0x01;

        //[StructLayout(LayoutKind.Sequential)]
        //internal struct BLENDFUNCTION
        //{
        //    byte BlendOp;
        //    byte BlendFlags;
        //    internal byte SourceConstantAlpha;
        //    internal byte AlphaFormat;

        //    public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
        //    {
        //        BlendOp = op;
        //        BlendFlags = flags;
        //        SourceConstantAlpha = alpha;
        //        AlphaFormat = format;
        //    }
        //}

        [StructLayout(LayoutKind.Sequential)]
        internal struct BLENDFUNCTION
        {
            byte BlendOp;
            byte BlendFlags;
            internal byte SourceConstantAlpha;
            internal byte AlphaFormat;
        }

        #endregion

        // ******************************** Center System Dialogs

        #region Center System Dialogs

        internal static bool CenterSystemDialog(string fileName, string arguments, Form baseForm) //Rectangle itemBounds)
        {
            bool result = true;

            try
            {
                IntPtr hWndCurrent = GetForegroundWindow();

                System.Diagnostics.Process showControlPanel = new System.Diagnostics.Process
                {
                    StartInfo = { FileName = fileName, Arguments = arguments }
                };
                showControlPanel.Start();
                showControlPanel.Dispose();

                if (baseForm != null)
                {
                    int i = 0;
                    IntPtr hWnd = GetForegroundWindow();
                    while (i < 100 && hWnd == hWndCurrent)
                    {
                        ++i;
                        System.Threading.Thread.Sleep(10);
                        hWnd = GetForegroundWindow();
                    }

                    if (hWnd != hWndCurrent)
                    {
                        Rectangle r1 = new Rectangle();
                        Rectangle r2 = Screen.GetWorkingArea(baseForm);
                        Rect r3;

                        GetWindowRect(hWnd, out r3);

                        r1.Width = r3.Right - r3.Left + 1;
                        r1.Height = r3.Bottom - r3.Top + 1;

                        r1.X = baseForm.Bounds.Left + (baseForm.Bounds.Width - r1.Width) / 2;
                        if (r1.X < r2.X) r1.X = r2.X + 2;
                        else if (r1.X + r1.Width > r2.Width) r1.X = r2.Width - r1.Width - 2;

                        r1.Y = baseForm.Bounds.Top + (baseForm.Bounds.Height - r1.Height) / 2;
                        if (r1.Y < r2.Y) r1.Y = r2.Y + 2;
                        else if (r1.Y + r1.Height > r2.Height) r1.Y = r2.Height - r1.Height - 2;

                        SetWindowPos(hWnd, IntPtr.Zero, r1.X, r1.Y, 0, 0, SWP_NOSIZE | SWP_NOZORDER | SWP_SHOWWINDOW);
                    }
                }
            }
            catch { result = false; }
            return result;
        }

        #endregion

        // ******************************** Change System Sleep Mode

        #region Change System Sleep Mode

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern uint SetThreadExecutionState(uint esFlags);

        private const uint ES_NO_SLEEP = 0x80000003;
        private static int _sleepCount;
        private static uint _oldState;

        internal static bool SleepStatus
        {
            //get { return sleepCount > 0; }
            set
            {
                if (value)
                {
                    uint flags = SetThreadExecutionState(ES_NO_SLEEP);
                    if (++_sleepCount == 1) _oldState = flags;
                }
                else
                {
                    if (_sleepCount > 0)
                    {
                        if (--_sleepCount == 0) SetThreadExecutionState(_oldState);
                    }
                }
            }
        }

        #endregion


        // ******************************** Rounded Rectangle

        #region Rounded Rectangle (used with InfoLabel + Preset Display Clip)

        [DllImport("Gdi32.dll")] // EntryPoint = "CreateRoundRectRgn")]
        internal static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect, // x-coordinate of upper-left corner
                int nTopRect, // y-coordinate of upper-left corner
                int nRightRect, // x-coordinate of lower-right corner
                int nBottomRect, // y-coordinate of lower-right corner
                int nWidthEllipse, // height of ellipse
                int nHeightEllipse // width of ellipse
             );

        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);

        #endregion
    }

    #endregion


    // ******************************** Taskbar Indicator - Com Import

    #region Taskbar Indicator

    // TaskbarProgress class based on code by WhoIsRich at:
    // stackoverflow.com/questions/1295890/windows-7-progress-bar-in-taskbar-in-c

    #endregion


    // ******************************** Media Foundation - Com Import

    #region Media Foundation

    #region Enums

    //[UnmanagedName("Unnamed enum")]
    //internal enum DXVA2Filters
    //{
    //    None = 0,
    //    NoiseFilterLumaLevel = 1,
    //    NoiseFilterLumaThreshold = 2,
    //    NoiseFilterLumaRadius = 3,
    //    NoiseFilterChromaLevel = 4,
    //    NoiseFilterChromaThreshold = 5,
    //    NoiseFilterChromaRadius = 6,
    //    DetailFilterLumaLevel = 7,
    //    DetailFilterLumaThreshold = 8,
    //    DetailFilterLumaRadius = 9,
    //    DetailFilterChromaLevel = 10,
    //    DetailFilterChromaThreshold = 11,
    //    DetailFilterChromaRadius = 12
    //}

    //[Flags, UnmanagedName("MFBYTESTREAM_* defines")]
    //internal enum MFByteStreamCapabilities
    //{
    //    None = 0x00000000,
    //    IsReadable = 0x00000001,
    //    IsWritable = 0x00000002,
    //    IsSeekable = 0x00000004,
    //    IsRemote = 0x00000008,
    //    IsDirectory = 0x00000080,
    //    HasSlowSeek = 0x00000100,
    //    IsPartiallyDownloaded = 0x00000200,
    //    ShareWrite = 0x00000400,
    //    DoesNotUseNetwork = 0x00000800,
    //}

    //[Flags, UnmanagedName("MFT_ENUM_FLAG")]
    //internal enum MFT_EnumFlag
    //{
    //    None = 0x00000000,
    //    SyncMFT = 0x00000001,   // Enumerates V1 MFTs. This is default.
    //    AsyncMFT = 0x00000002,   // Enumerates only software async MFTs also known as V2 MFTs
    //    Hardware = 0x00000004,   // Enumerates V2 hardware async MFTs
    //    FieldOfUse = 0x00000008,   // Enumerates MFTs that require unlocking
    //    LocalMFT = 0x00000010,   // Enumerates Locally (in-process) registered MFTs
    //    TranscodeOnly = 0x00000020,   // Enumerates decoder MFTs used by transcode only
    //    SortAndFilter = 0x00000040,   // Apply system local, do not use and preferred sorting and filtering
    //    SortAndFilterApprovedOnly = 0x000000C0,   // Similar to MFT_ENUM_FLAG_SORTANDFILTER, but apply a local policy of: MF_PLUGIN_CONTROL_POLICY_USE_APPROVED_PLUGINS
    //    SortAndFilterWebOnly = 0x00000140,   // Similar to MFT_ENUM_FLAG_SORTANDFILTER, but apply a local policy of: MF_PLUGIN_CONTROL_POLICY_USE_WEB_PLUGINS
    //    SortAndFilterWebOnlyEdgemode = 0x00000240,
    //    All = 0x0000003F    // Enumerates all MFTs including SW and HW MFTs and applies filtering
    //}

    //[UnmanagedName("MF_VIDEO_PROCESSOR_MIRROR")]
    //internal enum MF_VIDEO_PROCESSOR_MIRROR
    //{
    //    None = 0,
    //    Horizontal = 1,
    //    Vertical = 2
    //}

    //[UnmanagedName("MFVideoInterlaceMode")]
    //internal enum MFVideoInterlaceMode
    //{
    //    FieldInterleavedLowerFirst = 4,
    //    FieldInterleavedUpperFirst = 3,
    //    FieldSingleLower = 6,
    //    FieldSingleUpper = 5,
    //    ForceDWORD = 0x7fffffff,
    //    Last = 8,
    //    MixedInterlaceOrProgressive = 7,
    //    Progressive = 2,
    //    Unknown = 0
    //}

    #endregion

    #region Structs

    //[StructLayout(LayoutKind.Sequential, Pack = 8), UnmanagedName("MFCLOCK_PROPERTIES")]
    //internal struct MFClockProperties
    //{
    //    public long qwCorrelationRate;
    //    public Guid guidClockId;
    //    public MFClockRelationalFlags dwClockFlags;
    //    public long qwClockFrequency;
    //    public int dwClockTolerance;
    //    public int dwClockJitter;
    //}

    #endregion

    //#region Abstract Classes

    //abstract internal class COMBase
    //{
    //    public static bool Succeeded(HResult hr)
    //    {
    //        return hr >= 0;
    //    }

    //    public static bool Failed(HResult hr)
    //    {
    //        return hr < 0;
    //    }

    //    public static void SafeRelease(object o)
    //    {
    //        if (o != null)
    //        {
    //            if (Marshal.IsComObject(o))
    //            {
    //                int i = Marshal.ReleaseComObject(o);
    //            }
    //            else
    //            {
    //                IDisposable iDis = o as IDisposable;
    //                if (iDis != null)
    //                {
    //                    iDis.Dispose();
    //                }
    //                else
    //                {
    //                    throw new Exception("What the heck was that?");
    //                }
    //            }
    //        }
    //    }

    //}

    //#endregion

    #region Static Classes

    #endregion

    #region Classes

    //[StructLayout(LayoutKind.Sequential, Pack = 4), UnmanagedName("BITMAPINFOHEADER")]
    //internal class BitmapInfoHeader
    //{
    //    public int Size;
    //    public int Width;
    //    public int Height;
    //    public short Planes;
    //    public short BitCount;
    //    public int Compression;
    //    public int ImageSize;
    //    public int XPelsPerMeter;
    //    public int YPelsPerMeter;
    //    public int ClrUsed;
    //    public int ClrImportant;

    //    public IntPtr GetPtr()
    //    {
    //        IntPtr ip;

    //        // See what kind of BitmapInfoHeader object we've got
    //        if (this is BitmapInfoHeaderWithData)
    //        {
    //            int iBitmapInfoHeaderSize = Marshal.SizeOf(typeof(BitmapInfoHeader));

    //            // BitmapInfoHeaderWithData - Have to copy the array too
    //            BitmapInfoHeaderWithData pData = this as BitmapInfoHeaderWithData;

    //            // Account for copying the array.  This may result in us allocating more bytes than we
    //            // need (if cbSize < IntPtr.Size), but it prevents us from overrunning the buffer.
    //            int iUseSize = Math.Max(pData.bmiColors.Length * 4, IntPtr.Size);

    //            ip = Marshal.AllocCoTaskMem(iBitmapInfoHeaderSize + iUseSize);

    //            Marshal.StructureToPtr(pData, ip, false);

    //            //IntPtr ip2 = ip + iBitmapInfoHeaderSize;
    //            IntPtr ip2 = new IntPtr(ip.ToInt64() + iBitmapInfoHeaderSize);

    //            Marshal.Copy(pData.bmiColors, 0, ip2, pData.bmiColors.Length);
    //        }
    //        else if (this is BitmapInfoHeader)
    //        {
    //            int iBitmapInfoHeaderSize = Marshal.SizeOf(typeof(BitmapInfoHeader));

    //            // BitmapInfoHeader - just do a copy
    //            ip = Marshal.AllocCoTaskMem(iBitmapInfoHeaderSize);
    //            Marshal.StructureToPtr(this as BitmapInfoHeader, ip, false);
    //        }
    //        else
    //        {
    //            ip = IntPtr.Zero;
    //        }

    //        return ip;
    //    }

    //    public static BitmapInfoHeader PtrToBMI(IntPtr pNativeData)
    //    {
    //        int iEntries;
    //        int biCompression;
    //        int biClrUsed;
    //        int biBitCount;

    //        biBitCount = Marshal.ReadInt16(pNativeData, 14);
    //        biCompression = Marshal.ReadInt32(pNativeData, 16);
    //        biClrUsed = Marshal.ReadInt32(pNativeData, 32);

    //        if (biCompression == 3) // BI_BITFIELDS
    //        {
    //            iEntries = 3;
    //        }
    //        else if (biClrUsed > 0)
    //        {
    //            iEntries = biClrUsed;
    //        }
    //        else if (biBitCount <= 8)
    //        {
    //            iEntries = 1 << biBitCount;
    //        }
    //        else
    //        {
    //            iEntries = 0;
    //        }

    //        BitmapInfoHeader bmi;

    //        if (iEntries == 0)
    //        {
    //            // Create a simple BitmapInfoHeader struct
    //            bmi = new BitmapInfoHeader();
    //            Marshal.PtrToStructure(pNativeData, bmi);
    //        }
    //        else
    //        {
    //            BitmapInfoHeaderWithData ext = new BitmapInfoHeaderWithData();

    //            ext.Size = Marshal.ReadInt32(pNativeData, 0);
    //            ext.Width = Marshal.ReadInt32(pNativeData, 4);
    //            ext.Height = Marshal.ReadInt32(pNativeData, 8);
    //            ext.Planes = Marshal.ReadInt16(pNativeData, 12);
    //            ext.BitCount = Marshal.ReadInt16(pNativeData, 14);
    //            ext.Compression = Marshal.ReadInt32(pNativeData, 16);
    //            ext.ImageSize = Marshal.ReadInt32(pNativeData, 20);
    //            ext.XPelsPerMeter = Marshal.ReadInt32(pNativeData, 24);
    //            ext.YPelsPerMeter = Marshal.ReadInt32(pNativeData, 28);
    //            ext.ClrUsed = Marshal.ReadInt32(pNativeData, 32);
    //            ext.ClrImportant = Marshal.ReadInt32(pNativeData, 36);

    //            bmi = ext as BitmapInfoHeader;

    //            ext.bmiColors = new int[iEntries];
    //            //IntPtr ip2 = pNativeData + Marshal.SizeOf(typeof(BitmapInfoHeader));
    //            IntPtr ip2 = new IntPtr(pNativeData.ToInt64() + Marshal.SizeOf(typeof(BitmapInfoHeader)));
    //            Marshal.Copy(ip2, ext.bmiColors, 0, iEntries);
    //        }

    //        return bmi;
    //    }

    //    public void CopyFrom(BitmapInfoHeader bmi)
    //    {
    //        Size = bmi.Size;
    //        Width = bmi.Width;
    //        Height = bmi.Height;
    //        Planes = bmi.Planes;
    //        BitCount = bmi.BitCount;
    //        Compression = bmi.Compression;
    //        ImageSize = bmi.ImageSize;
    //        YPelsPerMeter = bmi.YPelsPerMeter;
    //        ClrUsed = bmi.ClrUsed;
    //        ClrImportant = bmi.ClrImportant;

    //        if (bmi is BitmapInfoHeaderWithData)
    //        {
    //            BitmapInfoHeaderWithData ext = this as BitmapInfoHeaderWithData;
    //            BitmapInfoHeaderWithData ext2 = bmi as BitmapInfoHeaderWithData;

    //            ext.bmiColors = new int[ext2.bmiColors.Length];
    //            ext2.bmiColors.CopyTo(ext.bmiColors, 0);
    //        }
    //    }
    //}

    //[StructLayout(LayoutKind.Sequential, Pack = 4), UnmanagedName("BITMAPINFO")]
    //internal sealed class BitmapInfoHeaderWithData : BitmapInfoHeader
    //{
    //    public int[] bmiColors;
    //}

    // Class to handle BITMAPINFO.  Only used by MFCalculateBitmapImageSize &
    // MFCreateVideoMediaTypeFromBitMapInfoHeader (as [In]) and 
    // IMFVideoDisplayControl::GetCurrentImage ([In, Out]).  Since
    // IMFVideoDisplayControl can be implemented on a managed class, we must
    // support nesting.
    //internal sealed class BMMarshaler : ICustomMarshaler
    //{
    //    private class MyProps
    //    {
    //        #region Data members

    //        public BitmapInfoHeader m_obj;
    //        public IntPtr m_ptr;

    //        private int m_InProcsss = 0;
    //        private bool m_IAllocated;
    //        private MyProps m_Parent = null;

    //        [ThreadStatic]
    //        private static MyProps m_CurrentProps;

    //        #endregion

    //        public int GetStage()
    //        {
    //            return m_InProcsss;
    //        }

    //        public void StageComplete()
    //        {
    //            m_InProcsss++;
    //        }

    //        public IntPtr Allocate()
    //        {
    //            IntPtr ip = m_obj.GetPtr();
    //            m_IAllocated = true;
    //            return ip;
    //        }

    //        public static MyProps AddLayer()
    //        {
    //            MyProps p = new MyProps();
    //            p.m_Parent = m_CurrentProps;
    //            m_CurrentProps = p;

    //            return p;
    //        }

    //        public static void SplitLayer()
    //        {
    //            MyProps t = AddLayer();
    //            MyProps p = t.m_Parent;

    //            t.m_InProcsss = 1;
    //            t.m_ptr = p.m_ptr;
    //            t.m_obj = p.m_obj;

    //            p.m_InProcsss = 1;
    //        }

    //        public static MyProps GetTop()
    //        {
    //            // If the member hasn't been initialized, do it now.  And no,
    //            // we can't do this in the constructor, since the constructor
    //            // may have been called on a different thread, and
    //            // m_CurrentProps is unique to each thread.
    //            if (m_CurrentProps == null)
    //            {
    //                m_CurrentProps = new MyProps();
    //            }

    //            return m_CurrentProps;
    //        }

    //        public void Clear()
    //        {
    //            if (m_IAllocated)
    //            {
    //                Marshal.FreeCoTaskMem(m_ptr);
    //                m_IAllocated = false;
    //            }

    //            // Never delete the last entry.
    //            if (m_Parent == null)
    //            {
    //                m_InProcsss = 0;
    //                m_obj = null;
    //                m_ptr = IntPtr.Zero;
    //            }
    //            else
    //            {
    //                m_obj = null;
    //                m_CurrentProps = m_Parent;
    //            }
    //        }
    //    }

    //    public IntPtr MarshalManagedToNative(object managedObj)
    //    {
    //        MyProps t = MyProps.GetTop();

    //        switch (t.GetStage())
    //        {
    //            case 0:
    //                {
    //                    t.m_obj = managedObj as BitmapInfoHeader;

    //                    t.m_ptr = t.Allocate();

    //                    break;
    //                }
    //            case 1:
    //                {
    //                    if (!System.Object.ReferenceEquals(t.m_obj, managedObj))
    //                    {
    //                        MyProps.AddLayer();

    //                        // Try this call again now that we have fixed
    //                        // m_CurrentProps.
    //                        return MarshalManagedToNative(managedObj);
    //                    }
    //                    Marshal.StructureToPtr(managedObj, t.m_ptr, false);
    //                    break;
    //                }
    //            case 2:
    //                {
    //                    MyProps.SplitLayer();

    //                    // Try this call again now that we have fixed
    //                    // m_CurrentProps.
    //                    return MarshalManagedToNative(managedObj);
    //                }
    //            default:
    //                {
    //                    // Environment.FailFast("Something horrible has " +
    //                    //"happened, probaby due to " +
    //                    //"marshaling of nested " +
    //                    //"PropVariant calls.");
    //                    break;
    //                }
    //        }
    //        t.StageComplete();

    //        return t.m_ptr;
    //    }

    //    // Called just after invoking the COM method.  The IntPtr is the same
    //    // one that just got returned from MarshalManagedToNative.  The return
    //    // value is unused.
    //    public object MarshalNativeToManaged(IntPtr pNativeData)
    //    {
    //        MyProps t = MyProps.GetTop();

    //        switch (t.GetStage())
    //        {
    //            case 0:
    //                {
    //                    t.m_obj = BitmapInfoHeader.PtrToBMI(pNativeData);
    //                    t.m_ptr = pNativeData;
    //                    break;
    //                }
    //            case 1:
    //                {
    //                    if (t.m_ptr != pNativeData)
    //                    {
    //                        // If we get here, we have already received a call
    //                        // to MarshalManagedToNative where we created an
    //                        // IntPtr and stored it into t.m_ptr.  But the
    //                        // value we just got passed here isn't the same
    //                        // one. Therefore instead of being the second half
    //                        // of a "Managed calling unmanaged" (as m_InProcsss
    //                        // led us to believe) this is really the first half
    //                        // of a nested "Unmanaged calling managed" (see
    //                        // Recursion in the comments at the top of this
    //                        // class).  Add another layer.
    //                        MyProps.AddLayer();

    //                        // Try this call again now that we have fixed
    //                        // m_CurrentProps.
    //                        return MarshalNativeToManaged(pNativeData);
    //                    }
    //                    BitmapInfoHeader bmi = BitmapInfoHeader.PtrToBMI(pNativeData);

    //                    t.m_obj.CopyFrom(bmi);
    //                    break;
    //                }
    //            case 2:
    //                {
    //                    // Apparently this is 'part 3' of a 2 part call.  Which
    //                    // means we are doing a nested call.  Normally we would
    //                    // catch the fact that this is a nested call with the
    //                    // (t.m_ptr != pNativeData) check above.  However, if
    //                    // the same PropVariant instance is being passed thru
    //                    // again, we end up here.  So, add a layer.
    //                    MyProps.SplitLayer();

    //                    // Try this call again now that we have fixed
    //                    // m_CurrentProps.
    //                    return MarshalNativeToManaged(pNativeData);
    //                }
    //            default:
    //                {
    //                    //Environment.FailFast("Something horrible has " +
    //                    //"happened, probaby due to " +
    //                    //"marshaling of nested " +
    //                    //"BMMarshaler calls.");
    //                    break;
    //                }
    //        }
    //        t.StageComplete();

    //        return t.m_obj;
    //    }

    //    public void CleanUpManagedData(object ManagedObj)
    //    {
    //        MyProps t = MyProps.GetTop();
    //        t.Clear();
    //    }

    //    public void CleanUpNativeData(IntPtr pNativeData)
    //    {
    //        MyProps t = MyProps.GetTop();
    //        t.Clear();
    //    }

    //    // The number of bytes to marshal out - never called
    //    public int GetNativeDataSize()
    //    {
    //        return -1;
    //    }

    //// This method is called by interop to create the custom marshaler.
    //// The (optional) cookie is the value specified in MarshalCookie="xxx",
    //// or "" if none is specified.
    //private static ICustomMarshaler GetInstance(string cookie)
    //{
    //    return new BMMarshaler();
    //}


    // PVMarshaler - Class to marshal PropVariants on parameters that
    // *output* PropVariants.

    // When defining parameters that use this marshaler, you must always
    // declare them as both [In] and [Out].  This will result in *both*
    // MarshalManagedToNative and MarshalNativeToManaged being called.  Since
    // the order they are called depends on exactly what's happening,
    // m_InProcess lets us know which way things are being called.
    //
    // Managed calling unmanaged: 
    // In this case, MarshalManagedToNative is called first with m_InProcess 
    // == 0.  When MarshalManagedToNative is called, we store the managed
    // object (so we know where to copy it back to), then we clear the variant,
    // allocate some COM memory and pass a pointer to the COM memory to the
    // native code.  When the native code is done, MarshalNativeToManaged gets
    // called (m_InProcess == 1) with the pointer to the COM memory.  At that
    // point, we copy the contents back into the (saved) managed object. After
    // that, CleanUpNativeData gets called and we release the COM memory.
    //
    // Unmanaged calling managed:
    // In this case, MarshalNativeToManaged is called first.  We store the
    // native pointer (so we know where to copy the result back to), then we
    // create a managed PropVariant and copy the native value into it.  When
    // the managed code is done, MarshalManagedToNative gets called with the
    // managed PropVariant we created.  At that point, we copy the contents
    // of the managed PropVariant back into the (saved) native pointer.
    //
    // Multi-threading:
    // When marshaling from managed to native, the first thing that happens
    // is the .Net creates an instance of the PVMarshaler class.  It then
    // calls MarshalManagedToNative to send you the managed object into which
    // the unmanaged data will eventually be stored. However it doesn't pass
    // the managed object again when MarshalNativeToManaged eventually gets
    // called.  No problem, you assume, I'll just store it as a data member
    // and use it when MarshalNativeToManaged get called.  Yeah, about that...
    // First of all, if several threads all start calling a method that uses
    // PVMarshaler, .Net WILL create multiple instances of this class.
    // However, it will then DESTRUCT all of them except 1, which it will use
    // from every thread.  Unless you are aware of this behavior and take
    // precautions, having multiple thread using the same instance results in
    // chaos.
    // Also be aware that if two different methods both use PVMarshaler (say
    // GetItem and GetItemByIndex on IMFAttributes), .Net may use the same
    // instance for both methods.  Unless they each have a unique MarshalCookie
    // string.  Using a unique MarshalCookie doesn't help with multi-threading,
    // but it does help keep the marshaling from one method call from
    // interfering with another.
    //
    // Recursion:
    // If managed code calls unmanaged code thru PVMarshaler, then that
    // unmanaged code in turn calls IMFAttribute::GetItem against a managed
    // object, what happens?  .Net will use a single instance of PVMarshaler to
    // handle both calls, even if the actual PropVariant used for the second
    // call is a different instance of the PropVariant class.  It can also use
    // the same managed thread id all the way thru (so using a simple
    // ThreadStatic is not sufficient to keep track of this).  So if you see a
    // call to MarshalNativeToManaged right after a call to
    // MarshalManagedToNative, it might be the second half of a 'normal' bit of
    // marshaling, or it could be the start of a nested call from unmanaged
    // back into managed.
    // There are 2 ways to detect nesting:
    // 1) If the pNativeData sent to MarshalNativeToManaged *isn't* the one
    // you returned from MarshalManagedToNative, you are nesting.
    // 2) m_InProcsss starts at 0.  MarshalManagedToNative increments it to 1.
    // Then MarshalNativeToManaged increments it to 2.  For non-nested, that
    // should be the end.  So if MarshalManagedToNative gets called with
    // m_InProcsss == 2, we are nesting.
    //
    // Warning!  You cannot assume that both marshaling routines will always
    // get called.  For example if calling from unmanaged to managed,
    // MarshalNativeToManaged will get called, but if the managed code throws
    // an exception, MarshalManagedToNative will not.  This can be a problem
    // since .Net REUSES instances of the marshaler.  So it is essential that
    // class members always get cleaned up in CleanUpManagedData &
    // CleanUpNativeData.
    //
    // All this helps explain the otherwise inexplicable complexity of this
    // class:  It uses a ThreadStatic variable to keep instance data from one
    // thread from interfering with another thread, nests instances of MyProps,
    // and uses 2 different methods to check for recursion (which in theory
    // could be nested quite deeply).

    //[StructLayout(LayoutKind.Sequential), UnmanagedName("MFT_REGISTER_TYPE_INFO")]
    //internal class MFTRegisterTypeInfo
    //{
    //    public Guid guidMajorType;
    //    public Guid guidSubtype;
    //}

    //[StructLayout(LayoutKind.Sequential), UnmanagedName("MFNetCredentialManagerGetParam")]
    //internal class MFNetCredentialManagerGetParam
    //{
    //    public HResult hrOp;
    //    [MarshalAs(UnmanagedType.Bool)]
    //    public bool fAllowLoggedOnUser;
    //    [MarshalAs(UnmanagedType.Bool)]
    //    public bool fClearTextPackage;
    //    [MarshalAs(UnmanagedType.LPWStr)]
    //    public string pszUrl;
    //    [MarshalAs(UnmanagedType.LPWStr)]
    //    public string pszSite;
    //    [MarshalAs(UnmanagedType.LPWStr)]
    //    public string pszRealm;
    //    [MarshalAs(UnmanagedType.LPWStr)]
    //    public string pszPackage;
    //    public int nRetries;
    //}

    #endregion

    #region Interfaces

    //[ComImport, SuppressUnmanagedCodeSecurity,
    //InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    //Guid("AD4C1B00-4BF7-422F-9175-756693D9130D")]
    //internal interface IMFByteStream
    //{
    //    [PreserveSig]
    //    HResult GetCapabilities(
    //        out MFByteStreamCapabilities pdwCapabilities
    //        );

    //    [PreserveSig]
    //    HResult GetLength(
    //        out long pqwLength
    //        );

    //    [PreserveSig]
    //    HResult SetLength(
    //        [In] long qwLength
    //        );

    //    [PreserveSig]
    //    HResult GetCurrentPosition(
    //        out long pqwPosition
    //        );

    //    [PreserveSig]
    //    HResult SetCurrentPosition(
    //        [In] long qwPosition
    //        );

    //    [PreserveSig]
    //    HResult IsEndOfStream(
    //        [MarshalAs(UnmanagedType.Bool)] out bool pfEndOfStream
    //        );

    //    [PreserveSig]
    //    HResult Read(
    //        IntPtr pb,
    //        [In] int cb,
    //        out int pcbRead
    //        );

    //    [PreserveSig]
    //    HResult BeginRead(
    //        IntPtr pb,
    //        [In] int cb,
    //        [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncCallback pCallback,
    //        [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkState
    //        );

    //    [PreserveSig]
    //    HResult EndRead(
    //        [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncResult pResult,
    //        out int pcbRead
    //        );

    //    [PreserveSig]
    //    HResult Write(
    //        IntPtr pb,
    //        [In] int cb,
    //        out int pcbWritten
    //        );

    //    [PreserveSig]
    //    HResult BeginWrite(
    //        IntPtr pb,
    //        [In] int cb,
    //        [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncCallback pCallback,
    //        [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkState
    //        );

    //    [PreserveSig]
    //    HResult EndWrite(
    //        [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncResult pResult,
    //        out int pcbWritten
    //        );

    //    [PreserveSig]
    //    HResult Seek(
    //        [In] MFByteStreamSeekOrigin SeekOrigin,
    //        [In] long llSeekOffset,
    //        [In] MFByteStreamSeekingFlags dwSeekFlags,
    //        out long pqwCurrentPosition
    //        );

    //    [PreserveSig]
    //    HResult Flush();

    //    [PreserveSig]
    //    HResult Close();
    //}

    //[ComImport, SuppressUnmanagedCodeSecurity,
    //InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    //Guid("D182108F-4EC6-443F-AA42-A71106EC825F")]
    //internal interface IMFMediaStream : IMFMediaEventGenerator
    //{
    //    #region IMFMediaEventGenerator methods

    //    [PreserveSig]
    //    new HResult GetEvent(
    //        [In] MFEventFlag dwFlags,
    //        [MarshalAs(UnmanagedType.Interface)] out IMFMediaEvent ppEvent
    //        );

    //    [PreserveSig]
    //    new HResult BeginGetEvent(
    //        [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncCallback pCallback,
    //        [In, MarshalAs(UnmanagedType.IUnknown)] object o
    //        );

    //    [PreserveSig]
    //    new HResult EndGetEvent(
    //        IMFAsyncResult pResult,
    //        out IMFMediaEvent ppEvent
    //        );

    //    [PreserveSig]
    //    new HResult QueueEvent(
    //        [In] MediaEventType met,
    //        [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidExtendedType,
    //        [In] HResult hrStatus,
    //        [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant pvValue
    //        );

    //    #endregion

    //    [PreserveSig]
    //    HResult GetMediaSource(
    //        [MarshalAs(UnmanagedType.Interface)] out IMFMediaSource ppMediaSource
    //        );

    //    [PreserveSig]
    //    HResult GetStreamDescriptor(
    //        [MarshalAs(UnmanagedType.Interface)] out IMFStreamDescriptor ppStreamDescriptor
    //        );

    //    [PreserveSig]
    //    HResult RequestSample(
    //        [In, MarshalAs(UnmanagedType.IUnknown)] object pToken
    //        );
    //}

    //[ComImport, SuppressUnmanagedCodeSecurity,
    //InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    //Guid("A5C6C53F-C202-4AA5-9695-175BA8C508A5")]
    //internal interface IMFVideoMixerControl
    //{
    //    [PreserveSig]
    //    HResult SetStreamZOrder(
    //        [In] int dwStreamID,
    //        [In] int dwZ
    //        );

    //    [PreserveSig]
    //    HResult GetStreamZOrder(
    //        [In] int dwStreamID,
    //        out int pdwZ
    //        );

    //    [PreserveSig]
    //    HResult SetStreamOutputRect(
    //        [In] int dwStreamID,
    //        [In] MFVideoNormalizedRect pnrcOutput
    //        );

    //    [PreserveSig]
    //    HResult GetStreamOutputRect(
    //        [In] int dwStreamID,
    //        [Out, MarshalAs(UnmanagedType.LPStruct)] MFVideoNormalizedRect pnrcOutput
    //        );
    //}

    //[ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    //InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    //Guid("A3F675D5-6119-4f7f-A100-1D8B280F0EFB")]
    //internal interface IMFVideoProcessorControl
    //{
    //    [PreserveSig]
    //    HResult SetBorderColor();

    //    [PreserveSig]
    //    HResult SetSourceRectangle();

    //    [PreserveSig]
    //    HResult SetDestinationRectangle();

    //    [PreserveSig]
    //    HResult SetMirror(MF_VIDEO_PROCESSOR_MIRROR eMirror);

    //    [PreserveSig]
    //    HResult SetRotation();

    //    [PreserveSig]
    //    HResult SetConstrictionSize();
    //}

    // This is the ASync version of IMFSourceReader.  The only difference is the ReadSample method, which must allow
    // the final 4 params to be null.

    //[ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    //Guid("7DC9D5F9-9ED9-44EC-9BBF-0600BB589FBB"),
    //InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //internal interface IMF2DBuffer
    //{
    //    [PreserveSig]
    //    HResult Lock2D(
    //        [Out] out IntPtr pbScanline0,
    //        out int plPitch
    //        );

    //    [PreserveSig]
    //    HResult Unlock2D();

    //    [PreserveSig]
    //    HResult GetScanline0AndPitch(
    //        out IntPtr pbScanline0,
    //        out int plPitch
    //        );

    //    [PreserveSig]
    //    HResult IsContiguousFormat(
    //        [MarshalAs(UnmanagedType.Bool)] out bool pfIsContiguous
    //        );

    //    [PreserveSig]
    //    HResult GetContiguousLength(
    //        out int pcbLength
    //        );

    //    [PreserveSig]
    //    HResult ContiguousCopyTo(
    //        IntPtr pbDestBuffer,
    //        [In] int cbDestBuffer
    //        );

    //    [PreserveSig]
    //    HResult ContiguousCopyFrom(
    //        [In] IntPtr pbSrcBuffer,
    //        [In] int cbSrcBuffer
    //        );
    //}


    // Authentication - not yet
    //[ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    //Guid("5B87EF6B-7ED8-434F-BA0E-184FAC1628D1"),
    //InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //internal interface IMFNetCredentialManager
    //{
    //    [PreserveSig]
    //    HResult BeginGetCredentials(
    //        [In] MFNetCredentialManagerGetParam pParam,
    //        [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncCallback pCallback,
    //        [In, MarshalAs(UnmanagedType.IUnknown)] object pState
    //        );

    //    [PreserveSig]
    //    HResult EndGetCredentials(
    //        [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncResult pResult,
    //        [MarshalAs(UnmanagedType.Interface)] out IMFNetCredential ppCred
    //        );

    //    [PreserveSig]
    //    HResult SetGood(
    //        [In, MarshalAs(UnmanagedType.Interface)] IMFNetCredential pCred,
    //        [In, MarshalAs(UnmanagedType.Bool)] bool fGood
    //        );
    //}

    //[ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    //InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    //Guid("5B87EF6A-7ED8-434F-BA0E-184FAC1628D1")]
    //internal interface IMFNetCredential
    //{
    //    [PreserveSig]
    //    HResult SetUser(
    //        [In, MarshalAs(UnmanagedType.LPArray)] byte[] pbData,
    //        [In] int cbData,
    //        [In, MarshalAs(UnmanagedType.Bool)] bool fDataIsEncrypted
    //        );

    //    [PreserveSig]
    //    HResult SetPassword(
    //        [In, MarshalAs(UnmanagedType.LPArray)] byte[] pbData,
    //        [In] int cbData,
    //        [In, MarshalAs(UnmanagedType.Bool)] bool fDataIsEncrypted
    //        );

    //    [PreserveSig]
    //    HResult GetUser(
    //        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pbData,
    //        [In, Out] MFInt pcbData,
    //        [In, MarshalAs(UnmanagedType.Bool)] bool fEncryptData
    //        );

    //    [PreserveSig]
    //    HResult GetPassword(
    //        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pbData,
    //        [In, Out] MFInt pcbData,
    //        [In, MarshalAs(UnmanagedType.Bool)] bool fEncryptData
    //        );

    //    [PreserveSig]
    //    HResult LoggedOnUser(
    //        [MarshalAs(UnmanagedType.Bool)] out bool pfLoggedOnUser
    //        );
    //}

    #endregion

    #endregion


    // ******************************** Windows Core Audio API - Com Import

    #region Windows Core Audio API

    #endregion

}
