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
using System.Security;
using System.Text;
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

    internal sealed class TaskbarIndicator
    {
        [ComImport()]
        [Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface ITaskbarList3
        {
            // ITaskbarList
            void HrInit();
            void AddTab(IntPtr hwnd);
            void DeleteTab(IntPtr hwnd);
            void ActivateTab(IntPtr hwnd);
            void SetActiveAlt(IntPtr hwnd);

            // ITaskbarList2
            void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

            // ITaskbarList3
            void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
            void SetProgressState(IntPtr hwnd, TaskbarProgressState state);
        }

        [ComImport()]
        [Guid("56fdf344-fd6d-11d0-958a-006097c9a090")]
        [ClassInterface(ClassInterfaceType.None)]
        internal class TaskbarInstance { }
    }

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

    internal enum HResult
    {
        #region COM HRESULTs

        S_OK = 0,
        S_FALSE = 1,

        //E_PENDING = unchecked((int)0x8000000A),

        ///// <summary>Catastrophic failure</summary>
        //E_UNEXPECTED = unchecked((int)0x8000FFFF),

        ///// <summary>Not implemented</summary>
        E_NOTIMPL = unchecked((int)0x80004001),

        ///// <summary>Ran out of memory</summary>
        //E_OUTOFMEMORY = unchecked((int)0x8007000E),

        ///// <summary>One or more arguments are invalid</summary>
        E_INVALIDARG = unchecked((int)0x80070057),

        ///// <summary>No such interface supported</summary>
        //E_NOINTERFACE = unchecked((int)0x80004002),

        ///// <summary>Invalid pointer</summary>
        //E_POINTER = unchecked((int)0x80004003),

        ///// <summary>Invalid handle</summary>
        //E_HANDLE = unchecked((int)0x80070006),

        ///// <summary>Operation aborted</summary>
        //E_ABORT = unchecked((int)0x80004004),

        ///// <summary>Unspecified error</summary>
        E_FAIL = unchecked((int)0x80004005),

        ///// <summary>General access denied error</summary>
        E_ACCESSDENIED = unchecked((int)0x80070005),

        ///// <summary>Wrong OS or OS version for application</summary>
        CO_E_WRONGOSFORAPP = unchecked((int)0x800401FA),

        ///// <summary>Operation timed out</summary>
        COR_E_TIMEOUT = unchecked((int)0x80131505),

        #endregion

        #region Win32 HRESULTs

        ///// <summary>The system cannot find the file specified.</summary>
        ///// <unmanaged>HRESULT_FROM_WIN32(ERROR_FILE_NOT_FOUND)</unmanaged>
        //WIN32_ERROR_FILE_NOT_FOUND = unchecked((int)0x80070002),

        ///// <summary>More data is available.</summary>
        ///// <unmanaged>HRESULT_FROM_WIN32(ERROR_MORE_DATA)</unmanaged>
        //WIN32_ERROR_MORE_DATA = unchecked((int)0x800700ea),

        ///// <summary>No more data is available.</summary>
        ///// <unmanaged>HRESULT_FROM_WIN32(ERROR_NO_MORE_ITEMS)</unmanaged>
        //WIN32_ERROR_NO_MORE_ITEMS = unchecked((int)0x80070103),

        ///// <summary>Element not found.</summary>
        ///// <unmanaged>HRESULT_FROM_WIN32(ERROR_NOT_FOUND)</unmanaged>
        //WIN32_ERROR_NOT_FOUND = unchecked((int)0x80070490),

        ERROR_FILE_NOT_FOUND            = 0x02,
        ERROR_INVALID_NAME              = unchecked((int)0x8007007B),
        ERROR_PATH_NOT_FOUND            = unchecked((int)0x80070003),
        ERROR_SYSTEM_DEVICE_NOT_FOUND   = 0x00003BC3,
        ERROR_DEVICE_NOT_CONNECTED      = 0x0000048F,
        ERROR_NOT_READY                 = 0x15,
        ERROR_BUSY                      = unchecked((int)0x800700AA),
        ERROR_INVALID_WINDOW_HANDLE     = 0x00000578,
        ERROR_SERVER_NOT_CONNECTED      = unchecked((int)0xC00D4287),
        ERROR_SERVER_NOT_FOUND          = 0x00000837,

        #endregion

        #region Structured Storage HRESULTs

        ///// <summary>The underlying file was converted to compound file format.</summary>
        //STG_S_CONVERTED = unchecked((int)0x00030200),

        ///// <summary>Multiple opens prevent consolidated. (commit succeeded).</summary>
        //STG_S_MULTIPLEOPENS = unchecked((int)0x00030204),

        ///// <summary>Consolidation of the storage file failed. (commit succeeded).</summary>
        //STG_S_CONSOLIDATIONFAILED = unchecked((int)0x00030205),

        ///// <summary>Consolidation of the storage file is inappropriate. (commit succeeded).</summary>
        //STG_S_CANNOTCONSOLIDATE = unchecked((int)0x00030206),

        ///// <summary>Unable to perform requested operation.</summary>
        //STG_E_INVALIDFUNCTION = unchecked((int)0x80030001),

        ///// <summary>The file could not be found.</summary>
        //STG_E_FILENOTFOUND = unchecked((int)0x80030002),

        ///// <summary>There are insufficient resources to open another file.</summary>
        //STG_E_TOOMANYOPENFILES = unchecked((int)0x80030004),

        ///// <summary>Access Denied.</summary>
        //STG_E_ACCESSDENIED = unchecked((int)0x80030005),

        ///// <summary>There is insufficient memory available to complete operation.</summary>
        //STG_E_INSUFFICIENTMEMORY = unchecked((int)0x80030008),

        ///// <summary>Invalid pointer error.</summary>
        //STG_E_INVALIDPOINTER = unchecked((int)0x80030009),

        ///// <summary>A disk error occurred during a write operation.</summary>
        //STG_E_WRITEFAULT = unchecked((int)0x8003001D),

        ///// <summary>A lock violation has occurred.</summary>
        //STG_E_LOCKVIOLATION = unchecked((int)0x80030021),

        ///// <summary>File already exists.</summary>
        //STG_E_FILEALREADYEXISTS = unchecked((int)0x80030050),

        ///// <summary>Invalid parameter error.</summary>
        //STG_E_INVALIDPARAMETER = unchecked((int)0x80030057),

        ///// <summary>There is insufficient disk space to complete operation.</summary>
        //STG_E_MEDIUMFULL = unchecked((int)0x80030070),

        ///// <summary>The name is not valid.</summary>
        //STG_E_INVALIDNAME = unchecked((int)0x800300FC),

        ///// <summary>Invalid flag error.</summary>
        //STG_E_INVALIDFLAG = unchecked((int)0x800300FF),

        ///// <summary>The storage has been changed since the last commit.</summary>
        //STG_E_NOTCURRENT = unchecked((int)0x80030101),

        ///// <summary>Attempted to use an object that has ceased to exist.</summary>
        //STG_E_REVERTED = unchecked((int)0x80030102),

        ///// <summary>Can't save.</summary>
        //STG_E_CANTSAVE = unchecked((int)0x80030103),

        #endregion

        #region Media Foundation HRESULTs

        //MF_E_PLATFORM_NOT_INITIALIZED = unchecked((int)0xC00D36B0),

        //MF_E_CAPTURE_ENGINE_ALL_EFFECTS_REMOVED = unchecked((int)0xC00DABE5),
        //MF_E_CAPTURE_NO_SAMPLES_IN_QUEUE = unchecked((int)0xC00DABEB),
        //MF_E_CAPTURE_PROPERTY_SET_DURING_PHOTO = unchecked((int)0xC00DABEA),
        //MF_E_CAPTURE_SOURCE_DEVICE_EXTENDEDPROP_OP_IN_PROGRESS = unchecked((int)0xC00DABE9),
        //MF_E_CAPTURE_SOURCE_NO_AUDIO_STREAM_PRESENT = unchecked((int)0xC00DABE8),
        //MF_E_CAPTURE_SOURCE_NO_INDEPENDENT_PHOTO_STREAM_PRESENT = unchecked((int)0xC00DABE6),
        //MF_E_CAPTURE_SOURCE_NO_VIDEO_STREAM_PRESENT = unchecked((int)0xC00DABE7),
        //MF_E_HARDWARE_DRM_UNSUPPORTED = unchecked((int)0xC00D3706),
        //MF_E_HDCP_AUTHENTICATION_FAILURE = unchecked((int)0xC00D7188),
        //MF_E_HDCP_LINK_FAILURE = unchecked((int)0xC00D7189),
        //MF_E_HW_ACCELERATED_THUMBNAIL_NOT_SUPPORTED = unchecked((int)0xC00DABEC),
        //MF_E_NET_COMPANION_DRIVER_DISCONNECT = unchecked((int)0xC00D4295),
        //MF_E_OPERATION_IN_PROGRESS = unchecked((int)0xC00D3705),
        //MF_E_SINK_HEADERS_NOT_FOUND = unchecked((int)0xC00D4A45),
        //MF_INDEX_SIZE_ERR = unchecked((int)0x80700001),
        //MF_INVALID_ACCESS_ERR = unchecked((int)0x8070000F),
        //MF_INVALID_STATE_ERR = unchecked((int)0x8070000B),
        //MF_NOT_FOUND_ERR = unchecked((int)0x80700008),
        //MF_NOT_SUPPORTED_ERR = unchecked((int)0x80700009),
        //MF_PARSE_ERR = unchecked((int)0x80700051),
        //MF_QUOTA_EXCEEDED_ERR = unchecked((int)0x80700016),
        //MF_SYNTAX_ERR = unchecked((int)0x8070000C),

        //MF_E_BUFFERTOOSMALL = unchecked((int)0xC00D36B1),

        //MF_E_INVALIDREQUEST = unchecked((int)0xC00D36B2),
        //MF_E_INVALIDSTREAMNUMBER = unchecked((int)0xC00D36B3),
        //MF_E_INVALIDMEDIATYPE = unchecked((int)0xC00D36B4),
        //MF_E_NOTACCEPTING = unchecked((int)0xC00D36B5),
        //MF_E_NOT_INITIALIZED = unchecked((int)0xC00D36B6),
        //MF_E_UNSUPPORTED_REPRESENTATION = unchecked((int)0xC00D36B7),
        MF_E_NO_MORE_TYPES = unchecked((int)0xC00D36B9),
        //MF_E_UNSUPPORTED_SERVICE = unchecked((int)0xC00D36BA),
        //MF_E_UNEXPECTED = unchecked((int)0xC00D36BB),
        //MF_E_INVALIDNAME = unchecked((int)0xC00D36BC),
        //MF_E_INVALIDTYPE = unchecked((int)0xC00D36BD),
        //MF_E_INVALID_FILE_FORMAT = unchecked((int)0xC00D36BE),
        //MF_E_INVALIDINDEX = unchecked((int)0xC00D36BF),
        //MF_E_INVALID_TIMESTAMP = unchecked((int)0xC00D36C0),
        //MF_E_UNSUPPORTED_SCHEME = unchecked((int)0xC00D36C3),
        MF_E_UNSUPPORTED_BYTESTREAM_TYPE = unchecked((int)0xC00D36C4),
        //MF_E_UNSUPPORTED_TIME_FORMAT = unchecked((int)0xC00D36C5),
        //MF_E_NO_SAMPLE_TIMESTAMP = unchecked((int)0xC00D36C8),
        //MF_E_NO_SAMPLE_DURATION = unchecked((int)0xC00D36C9),
        //MF_E_INVALID_STREAM_DATA = unchecked((int)0xC00D36CB),
        //MF_E_RT_UNAVAILABLE = unchecked((int)0xC00D36CF),
        MF_E_UNSUPPORTED_RATE = unchecked((int)0xC00D36D0),
        //MF_E_THINNING_UNSUPPORTED = unchecked((int)0xC00D36D1),
        //MF_E_REVERSE_UNSUPPORTED = unchecked((int)0xC00D36D2),
        //MF_E_UNSUPPORTED_RATE_TRANSITION = unchecked((int)0xC00D36D3),
        //MF_E_RATE_CHANGE_PREEMPTED = unchecked((int)0xC00D36D4),
        //MF_E_NOT_FOUND = unchecked((int)0xC00D36D5),
        MF_E_NOT_AVAILABLE = unchecked((int)0xC00D36D6),
        //MF_E_NO_CLOCK = unchecked((int)0xC00D36D7),
        //MF_S_MULTIPLE_BEGIN = unchecked((int)0x000D36D8),
        //MF_E_MULTIPLE_BEGIN = unchecked((int)0xC00D36D9),
        //MF_E_MULTIPLE_SUBSCRIBERS = unchecked((int)0xC00D36DA),
        //MF_E_TIMER_ORPHANED = unchecked((int)0xC00D36DB),
        MF_E_STATE_TRANSITION_PENDING = unchecked((int)0xC00D36DC),
        //MF_E_UNSUPPORTED_STATE_TRANSITION = unchecked((int)0xC00D36DD),
        //MF_E_UNRECOVERABLE_ERROR_OCCURRED = unchecked((int)0xC00D36DE),
        //MF_E_SAMPLE_HAS_TOO_MANY_BUFFERS = unchecked((int)0xC00D36DF),
        //MF_E_SAMPLE_NOT_WRITABLE = unchecked((int)0xC00D36E0),
        //MF_E_INVALID_KEY = unchecked((int)0xC00D36E2),
        //MF_E_BAD_STARTUP_VERSION = unchecked((int)0xC00D36E3),
        //MF_E_UNSUPPORTED_CAPTION = unchecked((int)0xC00D36E4),
        //MF_E_INVALID_POSITION = unchecked((int)0xC00D36E5),
        MF_E_ATTRIBUTENOTFOUND = unchecked((int)0xC00D36E6),
        //MF_E_PROPERTY_TYPE_NOT_ALLOWED = unchecked((int)0xC00D36E7),
        //MF_E_PROPERTY_TYPE_NOT_SUPPORTED = unchecked((int)0xC00D36E8),
        //MF_E_PROPERTY_EMPTY = unchecked((int)0xC00D36E9),
        //MF_E_PROPERTY_NOT_EMPTY = unchecked((int)0xC00D36EA),
        //MF_E_PROPERTY_VECTOR_NOT_ALLOWED = unchecked((int)0xC00D36EB),
        //MF_E_PROPERTY_VECTOR_REQUIRED = unchecked((int)0xC00D36EC),
        //MF_E_OPERATION_CANCELLED = unchecked((int)0xC00D36ED),
        //MF_E_BYTESTREAM_NOT_SEEKABLE = unchecked((int)0xC00D36EE),
        //MF_E_DISABLED_IN_SAFEMODE = unchecked((int)0xC00D36EF),
        //MF_E_CANNOT_PARSE_BYTESTREAM = unchecked((int)0xC00D36F0),
        //MF_E_SOURCERESOLVER_MUTUALLY_EXCLUSIVE_FLAGS = unchecked((int)0xC00D36F1),
        //MF_E_MEDIAPROC_WRONGSTATE = unchecked((int)0xC00D36F2),
        //MF_E_RT_THROUGHPUT_NOT_AVAILABLE = unchecked((int)0xC00D36F3),
        //MF_E_RT_TOO_MANY_CLASSES = unchecked((int)0xC00D36F4),
        //MF_E_RT_WOULDBLOCK = unchecked((int)0xC00D36F5),
        //MF_E_NO_BITPUMP = unchecked((int)0xC00D36F6),
        //MF_E_RT_OUTOFMEMORY = unchecked((int)0xC00D36F7),
        //MF_E_RT_WORKQUEUE_CLASS_NOT_SPECIFIED = unchecked((int)0xC00D36F8),
        //MF_E_INSUFFICIENT_BUFFER = unchecked((int)0xC00D7170),
        //MF_E_CANNOT_CREATE_SINK = unchecked((int)0xC00D36FA),
        //MF_E_BYTESTREAM_UNKNOWN_LENGTH = unchecked((int)0xC00D36FB),
        //MF_E_SESSION_PAUSEWHILESTOPPED = unchecked((int)0xC00D36FC),
        //MF_S_ACTIVATE_REPLACED = unchecked((int)0x000D36FD),
        //MF_E_FORMAT_CHANGE_NOT_SUPPORTED = unchecked((int)0xC00D36FE),
        //MF_E_INVALID_WORKQUEUE = unchecked((int)0xC00D36FF),
        //MF_E_DRM_UNSUPPORTED = unchecked((int)0xC00D3700),
        //MF_E_UNAUTHORIZED = unchecked((int)0xC00D3701),
        MF_E_OUT_OF_RANGE = unchecked((int)0xC00D3702),
        //MF_E_INVALID_CODEC_MERIT = unchecked((int)0xC00D3703),
        MF_E_HW_MFT_FAILED_START_STREAMING = unchecked((int)0xC00D3704),
        //MF_S_ASF_PARSEINPROGRESS = unchecked((int)0x400D3A98),
        //MF_E_ASF_PARSINGINCOMPLETE = unchecked((int)0xC00D3A98),
        //MF_E_ASF_MISSINGDATA = unchecked((int)0xC00D3A99),
        //MF_E_ASF_INVALIDDATA = unchecked((int)0xC00D3A9A),
        //MF_E_ASF_OPAQUEPACKET = unchecked((int)0xC00D3A9B),
        //MF_E_ASF_NOINDEX = unchecked((int)0xC00D3A9C),
        //MF_E_ASF_OUTOFRANGE = unchecked((int)0xC00D3A9D),
        //MF_E_ASF_INDEXNOTLOADED = unchecked((int)0xC00D3A9E),
        //MF_E_ASF_TOO_MANY_PAYLOADS = unchecked((int)0xC00D3A9F),
        //MF_E_ASF_UNSUPPORTED_STREAM_TYPE = unchecked((int)0xC00D3AA0),
        //MF_E_ASF_DROPPED_PACKET = unchecked((int)0xC00D3AA1),
        //MF_E_NO_EVENTS_AVAILABLE = unchecked((int)0xC00D3E80),
        //MF_E_INVALID_STATE_TRANSITION = unchecked((int)0xC00D3E82),
        //MF_E_END_OF_STREAM = unchecked((int)0xC00D3E84),
        //MF_E_SHUTDOWN = unchecked((int)0xC00D3E85),
        //MF_E_MP3_NOTFOUND = unchecked((int)0xC00D3E86),
        //MF_E_MP3_OUTOFDATA = unchecked((int)0xC00D3E87),
        //MF_E_MP3_NOTMP3 = unchecked((int)0xC00D3E88),
        //MF_E_MP3_NOTSUPPORTED = unchecked((int)0xC00D3E89),
        MF_E_NO_DURATION = unchecked((int)0xC00D3E8A),
        //MF_E_INVALID_FORMAT = unchecked((int)0xC00D3E8C),
        //MF_E_PROPERTY_NOT_FOUND = unchecked((int)0xC00D3E8D),
        //MF_E_PROPERTY_READ_ONLY = unchecked((int)0xC00D3E8E),
        //MF_E_PROPERTY_NOT_ALLOWED = unchecked((int)0xC00D3E8F),
        //MF_E_MEDIA_SOURCE_NOT_STARTED = unchecked((int)0xC00D3E91),
        //MF_E_UNSUPPORTED_FORMAT = unchecked((int)0xC00D3E98),
        //MF_E_MP3_BAD_CRC = unchecked((int)0xC00D3E99),
        //MF_E_NOT_PROTECTED = unchecked((int)0xC00D3E9A),
        //MF_E_MEDIA_SOURCE_WRONGSTATE = unchecked((int)0xC00D3E9B),
        MF_E_MEDIA_SOURCE_NO_STREAMS_SELECTED = unchecked((int)0xC00D3E9C),
        //MF_E_CANNOT_FIND_KEYFRAME_SAMPLE = unchecked((int)0xC00D3E9D),

        //MF_E_UNSUPPORTED_CHARACTERISTICS = unchecked((int)0xC00D3E9E),
        MF_E_NO_AUDIO_RECORDING_DEVICE = unchecked((int)0xC00D3E9F),
        //MF_E_AUDIO_RECORDING_DEVICE_IN_USE = unchecked((int)0xC00D3EA0),
        MF_E_AUDIO_RECORDING_DEVICE_INVALIDATED = unchecked((int)0xC00D3EA1),
        MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED = unchecked((int)0xC00D3EA2),
        //MF_E_VIDEO_RECORDING_DEVICE_PREEMPTED = unchecked((int)0xC00D3EA3),

        //MF_E_NETWORK_RESOURCE_FAILURE = unchecked((int)0xC00D4268),
        //MF_E_NET_WRITE = unchecked((int)0xC00D4269),
        //MF_E_NET_READ = unchecked((int)0xC00D426A),
        //MF_E_NET_REQUIRE_NETWORK = unchecked((int)0xC00D426B),
        //MF_E_NET_REQUIRE_ASYNC = unchecked((int)0xC00D426C),
        //MF_E_NET_BWLEVEL_NOT_SUPPORTED = unchecked((int)0xC00D426D),
        //MF_E_NET_STREAMGROUPS_NOT_SUPPORTED = unchecked((int)0xC00D426E),
        //MF_E_NET_MANUALSS_NOT_SUPPORTED = unchecked((int)0xC00D426F),
        //MF_E_NET_INVALID_PRESENTATION_DESCRIPTOR = unchecked((int)0xC00D4270),
        //MF_E_NET_CACHESTREAM_NOT_FOUND = unchecked((int)0xC00D4271),
        //MF_I_MANUAL_PROXY = unchecked((int)0x400D4272),
        //MF_E_NET_REQUIRE_INPUT = unchecked((int)0xC00D4274),
        //MF_E_NET_REDIRECT = unchecked((int)0xC00D4275),
        //MF_E_NET_REDIRECT_TO_PROXY = unchecked((int)0xC00D4276),
        //MF_E_NET_TOO_MANY_REDIRECTS = unchecked((int)0xC00D4277),
        //MF_E_NET_TIMEOUT = unchecked((int)0xC00D4278),
        //MF_E_NET_CLIENT_CLOSE = unchecked((int)0xC00D4279),
        //MF_E_NET_BAD_CONTROL_DATA = unchecked((int)0xC00D427A),
        //MF_E_NET_INCOMPATIBLE_SERVER = unchecked((int)0xC00D427B),
        //MF_E_NET_UNSAFE_URL = unchecked((int)0xC00D427C),
        //MF_E_NET_CACHE_NO_DATA = unchecked((int)0xC00D427D),
        //MF_E_NET_EOL = unchecked((int)0xC00D427E),
        //MF_E_NET_BAD_REQUEST = unchecked((int)0xC00D427F),
        //MF_E_NET_INTERNAL_SERVER_ERROR = unchecked((int)0xC00D4280),
        //MF_E_NET_SESSION_NOT_FOUND = unchecked((int)0xC00D4281),
        //MF_E_NET_NOCONNECTION = unchecked((int)0xC00D4282),
        //MF_E_NET_CONNECTION_FAILURE = unchecked((int)0xC00D4283),
        //MF_E_NET_INCOMPATIBLE_PUSHSERVER = unchecked((int)0xC00D4284),
        //MF_E_NET_SERVER_ACCESSDENIED = unchecked((int)0xC00D4285),
        //MF_E_NET_PROXY_ACCESSDENIED = unchecked((int)0xC00D4286),
        //MF_E_NET_CANNOTCONNECT = unchecked((int)0xC00D4287),
        //MF_E_NET_INVALID_PUSH_TEMPLATE = unchecked((int)0xC00D4288),
        //MF_E_NET_INVALID_PUSH_PUBLISHING_POINT = unchecked((int)0xC00D4289),
        //MF_E_NET_BUSY = unchecked((int)0xC00D428A),
        //MF_E_NET_RESOURCE_GONE = unchecked((int)0xC00D428B),
        //MF_E_NET_ERROR_FROM_PROXY = unchecked((int)0xC00D428C),
        //MF_E_NET_PROXY_TIMEOUT = unchecked((int)0xC00D428D),
        //MF_E_NET_SERVER_UNAVAILABLE = unchecked((int)0xC00D428E),
        //MF_E_NET_TOO_MUCH_DATA = unchecked((int)0xC00D428F),
        //MF_E_NET_SESSION_INVALID = unchecked((int)0xC00D4290),
        //MF_E_OFFLINE_MODE = unchecked((int)0xC00D4291),
        //MF_E_NET_UDP_BLOCKED = unchecked((int)0xC00D4292),
        //MF_E_NET_UNSUPPORTED_CONFIGURATION = unchecked((int)0xC00D4293),
        //MF_E_NET_PROTOCOL_DISABLED = unchecked((int)0xC00D4294),
        //MF_E_ALREADY_INITIALIZED = unchecked((int)0xC00D4650),
        //MF_E_BANDWIDTH_OVERRUN = unchecked((int)0xC00D4651),
        //MF_E_LATE_SAMPLE = unchecked((int)0xC00D4652),
        //MF_E_FLUSH_NEEDED = unchecked((int)0xC00D4653),
        //MF_E_INVALID_PROFILE = unchecked((int)0xC00D4654),
        //MF_E_INDEX_NOT_COMMITTED = unchecked((int)0xC00D4655),
        //MF_E_NO_INDEX = unchecked((int)0xC00D4656),
        //MF_E_CANNOT_INDEX_IN_PLACE = unchecked((int)0xC00D4657),
        //MF_E_MISSING_ASF_LEAKYBUCKET = unchecked((int)0xC00D4658),
        //MF_E_INVALID_ASF_STREAMID = unchecked((int)0xC00D4659),
        //MF_E_STREAMSINK_REMOVED = unchecked((int)0xC00D4A38),
        //MF_E_STREAMSINKS_OUT_OF_SYNC = unchecked((int)0xC00D4A3A),
        //MF_E_STREAMSINKS_FIXED = unchecked((int)0xC00D4A3B),
        //MF_E_STREAMSINK_EXISTS = unchecked((int)0xC00D4A3C),
        //MF_E_SAMPLEALLOCATOR_CANCELED = unchecked((int)0xC00D4A3D),
        //MF_E_SAMPLEALLOCATOR_EMPTY = unchecked((int)0xC00D4A3E),
        //MF_E_SINK_ALREADYSTOPPED = unchecked((int)0xC00D4A3F),
        //MF_E_ASF_FILESINK_BITRATE_UNKNOWN = unchecked((int)0xC00D4A40),
        //MF_E_SINK_NO_STREAMS = unchecked((int)0xC00D4A41),
        //MF_S_SINK_NOT_FINALIZED = unchecked((int)0x000D4A42),
        //MF_E_METADATA_TOO_LONG = unchecked((int)0xC00D4A43),
        //MF_E_SINK_NO_SAMPLES_PROCESSED = unchecked((int)0xC00D4A44),
        //MF_E_VIDEO_REN_NO_PROCAMP_HW = unchecked((int)0xC00D4E20),
        //MF_E_VIDEO_REN_NO_DEINTERLACE_HW = unchecked((int)0xC00D4E21),
        //MF_E_VIDEO_REN_COPYPROT_FAILED = unchecked((int)0xC00D4E22),
        //MF_E_VIDEO_REN_SURFACE_NOT_SHARED = unchecked((int)0xC00D4E23),
        //MF_E_VIDEO_DEVICE_LOCKED = unchecked((int)0xC00D4E24),
        //MF_E_NEW_VIDEO_DEVICE = unchecked((int)0xC00D4E25),
        //MF_E_NO_VIDEO_SAMPLE_AVAILABLE = unchecked((int)0xC00D4E26),
        MF_E_NO_AUDIO_PLAYBACK_DEVICE = unchecked((int)0xC00D4E84),
        //MF_E_AUDIO_PLAYBACK_DEVICE_IN_USE = unchecked((int)0xC00D4E85),
        //MF_E_AUDIO_PLAYBACK_DEVICE_INVALIDATED = unchecked((int)0xC00D4E86),
        //MF_E_AUDIO_SERVICE_NOT_RUNNING = unchecked((int)0xC00D4E87),
        //MF_E_TOPO_INVALID_OPTIONAL_NODE = unchecked((int)0xC00D520E),
        //MF_E_TOPO_CANNOT_FIND_DECRYPTOR = unchecked((int)0xC00D5211),
        //MF_E_TOPO_CODEC_NOT_FOUND = unchecked((int)0xC00D5212),
        MF_E_TOPO_CANNOT_CONNECT = unchecked((int)0xC00D5213),
        //MF_E_TOPO_UNSUPPORTED = unchecked((int)0xC00D5214),
        //MF_E_TOPO_INVALID_TIME_ATTRIBUTES = unchecked((int)0xC00D5215),
        //MF_E_TOPO_LOOPS_IN_TOPOLOGY = unchecked((int)0xC00D5216),
        //MF_E_TOPO_MISSING_PRESENTATION_DESCRIPTOR = unchecked((int)0xC00D5217),
        //MF_E_TOPO_MISSING_STREAM_DESCRIPTOR = unchecked((int)0xC00D5218),
        //MF_E_TOPO_STREAM_DESCRIPTOR_NOT_SELECTED = unchecked((int)0xC00D5219),
        //MF_E_TOPO_MISSING_SOURCE = unchecked((int)0xC00D521A),
        //MF_E_TOPO_SINK_ACTIVATES_UNSUPPORTED = unchecked((int)0xC00D521B),
        //MF_E_SEQUENCER_UNKNOWN_SEGMENT_ID = unchecked((int)0xC00D61AC),
        //MF_S_SEQUENCER_CONTEXT_CANCELED = unchecked((int)0x000D61AD),
        //MF_E_NO_SOURCE_IN_CACHE = unchecked((int)0xC00D61AE),
        //MF_S_SEQUENCER_SEGMENT_AT_END_OF_STREAM = unchecked((int)0x000D61AF),
        //MF_E_TRANSFORM_TYPE_NOT_SET = unchecked((int)0xC00D6D60),
        //MF_E_TRANSFORM_STREAM_CHANGE = unchecked((int)0xC00D6D61),
        //MF_E_TRANSFORM_INPUT_REMAINING = unchecked((int)0xC00D6D62),
        //MF_E_TRANSFORM_PROFILE_MISSING = unchecked((int)0xC00D6D63),
        //MF_E_TRANSFORM_PROFILE_INVALID_OR_CORRUPT = unchecked((int)0xC00D6D64),
        //MF_E_TRANSFORM_PROFILE_TRUNCATED = unchecked((int)0xC00D6D65),
        //MF_E_TRANSFORM_PROPERTY_PID_NOT_RECOGNIZED = unchecked((int)0xC00D6D66),
        //MF_E_TRANSFORM_PROPERTY_VARIANT_TYPE_WRONG = unchecked((int)0xC00D6D67),
        //MF_E_TRANSFORM_PROPERTY_NOT_WRITEABLE = unchecked((int)0xC00D6D68),
        //MF_E_TRANSFORM_PROPERTY_ARRAY_VALUE_WRONG_NUM_DIM = unchecked((int)0xC00D6D69),
        //MF_E_TRANSFORM_PROPERTY_VALUE_SIZE_WRONG = unchecked((int)0xC00D6D6A),
        //MF_E_TRANSFORM_PROPERTY_VALUE_OUT_OF_RANGE = unchecked((int)0xC00D6D6B),
        //MF_E_TRANSFORM_PROPERTY_VALUE_INCOMPATIBLE = unchecked((int)0xC00D6D6C),
        //MF_E_TRANSFORM_NOT_POSSIBLE_FOR_CURRENT_OUTPUT_MEDIATYPE = unchecked((int)0xC00D6D6D),
        //MF_E_TRANSFORM_NOT_POSSIBLE_FOR_CURRENT_INPUT_MEDIATYPE = unchecked((int)0xC00D6D6E),
        //MF_E_TRANSFORM_NOT_POSSIBLE_FOR_CURRENT_MEDIATYPE_COMBINATION = unchecked((int)0xC00D6D6F),
        //MF_E_TRANSFORM_CONFLICTS_WITH_OTHER_CURRENTLY_ENABLED_FEATURES = unchecked((int)0xC00D6D70),
        //MF_E_TRANSFORM_NEED_MORE_INPUT = unchecked((int)0xC00D6D72),
        //MF_E_TRANSFORM_NOT_POSSIBLE_FOR_CURRENT_SPKR_CONFIG = unchecked((int)0xC00D6D73),
        //MF_E_TRANSFORM_CANNOT_CHANGE_MEDIATYPE_WHILE_PROCESSING = unchecked((int)0xC00D6D74),
        //MF_S_TRANSFORM_DO_NOT_PROPAGATE_EVENT = unchecked((int)0x000D6D75),
        //MF_E_UNSUPPORTED_D3D_TYPE = unchecked((int)0xC00D6D76),
        //MF_E_TRANSFORM_ASYNC_LOCKED = unchecked((int)0xC00D6D77),
        //MF_E_TRANSFORM_CANNOT_INITIALIZE_ACM_DRIVER = unchecked((int)0xC00D6D78L),
        //MF_E_LICENSE_INCORRECT_RIGHTS = unchecked((int)0xC00D7148),
        //MF_E_LICENSE_OUTOFDATE = unchecked((int)0xC00D7149),
        //MF_E_LICENSE_REQUIRED = unchecked((int)0xC00D714A),
        //MF_E_DRM_HARDWARE_INCONSISTENT = unchecked((int)0xC00D714B),
        //MF_E_NO_CONTENT_PROTECTION_MANAGER = unchecked((int)0xC00D714C),
        //MF_E_LICENSE_RESTORE_NO_RIGHTS = unchecked((int)0xC00D714D),
        //MF_E_BACKUP_RESTRICTED_LICENSE = unchecked((int)0xC00D714E),
        //MF_E_LICENSE_RESTORE_NEEDS_INDIVIDUALIZATION = unchecked((int)0xC00D714F),
        //MF_S_PROTECTION_NOT_REQUIRED = unchecked((int)0x000D7150),
        //MF_E_COMPONENT_REVOKED = unchecked((int)0xC00D7151),
        //MF_E_TRUST_DISABLED = unchecked((int)0xC00D7152),
        //MF_E_WMDRMOTA_NO_ACTION = unchecked((int)0xC00D7153),
        //MF_E_WMDRMOTA_ACTION_ALREADY_SET = unchecked((int)0xC00D7154),
        //MF_E_WMDRMOTA_DRM_HEADER_NOT_AVAILABLE = unchecked((int)0xC00D7155),
        //MF_E_WMDRMOTA_DRM_ENCRYPTION_SCHEME_NOT_SUPPORTED = unchecked((int)0xC00D7156),
        //MF_E_WMDRMOTA_ACTION_MISMATCH = unchecked((int)0xC00D7157),
        //MF_E_WMDRMOTA_INVALID_POLICY = unchecked((int)0xC00D7158),
        //MF_E_POLICY_UNSUPPORTED = unchecked((int)0xC00D7159),
        //MF_E_OPL_NOT_SUPPORTED = unchecked((int)0xC00D715A),
        //MF_E_TOPOLOGY_VERIFICATION_FAILED = unchecked((int)0xC00D715B),
        //MF_E_SIGNATURE_VERIFICATION_FAILED = unchecked((int)0xC00D715C),
        //MF_E_DEBUGGING_NOT_ALLOWED = unchecked((int)0xC00D715D),
        //MF_E_CODE_EXPIRED = unchecked((int)0xC00D715E),
        //MF_E_GRL_VERSION_TOO_LOW = unchecked((int)0xC00D715F),
        //MF_E_GRL_RENEWAL_NOT_FOUND = unchecked((int)0xC00D7160),
        //MF_E_GRL_EXTENSIBLE_ENTRY_NOT_FOUND = unchecked((int)0xC00D7161),
        //MF_E_KERNEL_UNTRUSTED = unchecked((int)0xC00D7162),
        //MF_E_PEAUTH_UNTRUSTED = unchecked((int)0xC00D7163),
        //MF_E_NON_PE_PROCESS = unchecked((int)0xC00D7165),
        //MF_E_REBOOT_REQUIRED = unchecked((int)0xC00D7167),
        //MF_S_WAIT_FOR_POLICY_SET = unchecked((int)0x000D7168),
        //MF_S_VIDEO_DISABLED_WITH_UNKNOWN_SOFTWARE_OUTPUT = unchecked((int)0x000D7169),
        //MF_E_GRL_INVALID_FORMAT = unchecked((int)0xC00D716A),
        //MF_E_GRL_UNRECOGNIZED_FORMAT = unchecked((int)0xC00D716B),
        //MF_E_ALL_PROCESS_RESTART_REQUIRED = unchecked((int)0xC00D716C),
        //MF_E_PROCESS_RESTART_REQUIRED = unchecked((int)0xC00D716D),
        //MF_E_USERMODE_UNTRUSTED = unchecked((int)0xC00D716E),
        //MF_E_PEAUTH_SESSION_NOT_STARTED = unchecked((int)0xC00D716F),
        //MF_E_PEAUTH_PUBLICKEY_REVOKED = unchecked((int)0xC00D7171),
        //MF_E_GRL_ABSENT = unchecked((int)0xC00D7172),
        //MF_S_PE_TRUSTED = unchecked((int)0x000D7173),
        //MF_E_PE_UNTRUSTED = unchecked((int)0xC00D7174),
        //MF_E_PEAUTH_NOT_STARTED = unchecked((int)0xC00D7175),
        //MF_E_INCOMPATIBLE_SAMPLE_PROTECTION = unchecked((int)0xC00D7176),
        //MF_E_PE_SESSIONS_MAXED = unchecked((int)0xC00D7177),
        //MF_E_HIGH_SECURITY_LEVEL_CONTENT_NOT_ALLOWED = unchecked((int)0xC00D7178),
        //MF_E_TEST_SIGNED_COMPONENTS_NOT_ALLOWED = unchecked((int)0xC00D7179),
        //MF_E_ITA_UNSUPPORTED_ACTION = unchecked((int)0xC00D717A),
        //MF_E_ITA_ERROR_PARSING_SAP_PARAMETERS = unchecked((int)0xC00D717B),
        //MF_E_POLICY_MGR_ACTION_OUTOFBOUNDS = unchecked((int)0xC00D717C),
        //MF_E_BAD_OPL_STRUCTURE_FORMAT = unchecked((int)0xC00D717D),
        //MF_E_ITA_UNRECOGNIZED_ANALOG_VIDEO_PROTECTION_GUID = unchecked((int)0xC00D717E),
        //MF_E_NO_PMP_HOST = unchecked((int)0xC00D717F),
        //MF_E_ITA_OPL_DATA_NOT_INITIALIZED = unchecked((int)0xC00D7180),
        //MF_E_ITA_UNRECOGNIZED_ANALOG_VIDEO_OUTPUT = unchecked((int)0xC00D7181),
        //MF_E_ITA_UNRECOGNIZED_DIGITAL_VIDEO_OUTPUT = unchecked((int)0xC00D7182),

        //MF_E_RESOLUTION_REQUIRES_PMP_CREATION_CALLBACK = unchecked((int)0xC00D7183),
        //MF_E_INVALID_AKE_CHANNEL_PARAMETERS = unchecked((int)0xC00D7184),
        //MF_E_CONTENT_PROTECTION_SYSTEM_NOT_ENABLED = unchecked((int)0xC00D7185),
        //MF_E_UNSUPPORTED_CONTENT_PROTECTION_SYSTEM = unchecked((int)0xC00D7186),
        //MF_E_DRM_MIGRATION_NOT_SUPPORTED = unchecked((int)0xC00D7187),

        //MF_E_CLOCK_INVALID_CONTINUITY_KEY = unchecked((int)0xC00D9C40),
        //MF_E_CLOCK_NO_TIME_SOURCE = unchecked((int)0xC00D9C41),
        //MF_E_CLOCK_STATE_ALREADY_SET = unchecked((int)0xC00D9C42),
        //MF_E_CLOCK_NOT_SIMPLE = unchecked((int)0xC00D9C43),
        //MF_S_CLOCK_STOPPED = unchecked((int)0x000D9C44),
        //MF_E_NO_MORE_DROP_MODES = unchecked((int)0xC00DA028),
        //MF_E_NO_MORE_QUALITY_LEVELS = unchecked((int)0xC00DA029),
        //MF_E_DROPTIME_NOT_SUPPORTED = unchecked((int)0xC00DA02A),
        //MF_E_QUALITYKNOB_WAIT_LONGER = unchecked((int)0xC00DA02B),
        //MF_E_QM_INVALIDSTATE = unchecked((int)0xC00DA02C),
        //MF_E_TRANSCODE_NO_CONTAINERTYPE = unchecked((int)0xC00DA410),
        //MF_E_TRANSCODE_PROFILE_NO_MATCHING_STREAMS = unchecked((int)0xC00DA411),
        //MF_E_TRANSCODE_NO_MATCHING_ENCODER = unchecked((int)0xC00DA412),

        //MF_E_TRANSCODE_INVALID_PROFILE = unchecked((int)0xC00DA413),

        //MF_E_ALLOCATOR_NOT_INITIALIZED = unchecked((int)0xC00DA7F8),
        //MF_E_ALLOCATOR_NOT_COMMITED = unchecked((int)0xC00DA7F9),
        //MF_E_ALLOCATOR_ALREADY_COMMITED = unchecked((int)0xC00DA7FA),
        //MF_E_STREAM_ERROR = unchecked((int)0xC00DA7FB),
        //MF_E_INVALID_STREAM_STATE = unchecked((int)0xC00DA7FC),
        //MF_E_HW_STREAM_NOT_CONNECTED = unchecked((int)0xC00DA7FD),

        //MF_E_NO_CAPTURE_DEVICES_AVAILABLE = unchecked((int)0xC00DABE0),
        //MF_E_CAPTURE_SINK_OUTPUT_NOT_SET = unchecked((int)0xC00DABE1),
        //MF_E_CAPTURE_SINK_MIRROR_ERROR = unchecked((int)0xC00DABE2),
        //MF_E_CAPTURE_SINK_ROTATE_ERROR = unchecked((int)0xC00DABE3),
        //MF_E_CAPTURE_ENGINE_INVALID_OP = unchecked((int)0xC00DABE4),

        //MF_E_DXGI_DEVICE_NOT_INITIALIZED = unchecked((int)0x80041000),
        //MF_E_DXGI_NEW_VIDEO_DEVICE = unchecked((int)0x80041001),
        //MF_E_DXGI_VIDEO_DEVICE_LOCKED = unchecked((int)0x80041002),

        #endregion
    }

    [Flags, UnmanagedName("MF_EVENT_FLAG_* defines")]
    internal enum MFEventFlag
    {
        None = 0,
        NoWait = 0x00000001
    }

    [UnmanagedName("unnamed enum")]
    internal enum MediaEventType
    {
        MEUnknown = 0,
        MEError = 1,
        //MEExtendedType = 2,
        //MENonFatalError = 3,
        //MEGenericV1Anchor = MENonFatalError,
        //MESessionUnknown = 100,
        //MESessionTopologySet = 101,
        //MESessionTopologiesCleared = 102,
        //MESessionStarted = 103,
        //MESessionPaused = 104,
        //MESessionStopped = 105,
        MESessionClosed = 106,
        MESessionEnded = 107,
        //MESessionRateChanged = 108,
        //MESessionScrubSampleComplete = 109,
        //MESessionCapabilitiesChanged = 110,
        //MESessionTopologyStatus = 111,
        //MESessionNotifyPresentationTime = 112,
        //MENewPresentation = 113,
        //MELicenseAcquisitionStart = 114,
        //MELicenseAcquisitionCompleted = 115,
        //MEIndividualizationStart = 116,
        //MEIndividualizationCompleted = 117,
        //MEEnablerProgress = 118,
        //MEEnablerCompleted = 119,
        //MEPolicyError = 120,
        //MEPolicyReport = 121,
        //MEBufferingStarted = 122,
        //MEBufferingStopped = 123,
        //MEConnectStart = 124,
        //MEConnectEnd = 125,
        //MEReconnectStart = 126,
        //MEReconnectEnd = 127,
        //MERendererEvent = 128,
        //MESessionStreamSinkFormatChanged = 129,
        //MESessionV1Anchor = MESessionStreamSinkFormatChanged,
        //MESourceUnknown = 200,
        //MESourceStarted = 201,
        //MEStreamStarted = 202,
        //MESourceSeeked = 203,
        //MEStreamSeeked = 204,
        //MENewStream = 205,
        //MEUpdatedStream = 206,
        //MESourceStopped = 207,
        //MEStreamStopped = 208,
        //MESourcePaused = 209,
        //MEStreamPaused = 210,
        //MEEndOfPresentation = 211,
        //MEEndOfStream = 212,
        //MEMediaSample = 213,
        //MEStreamTick = 214,
        //MEStreamThinMode = 215,
        //MEStreamFormatChanged = 216,
        //MESourceRateChanged = 217,
        //MEEndOfPresentationSegment = 218,
        //MESourceCharacteristicsChanged = 219,
        //MESourceRateChangeRequested = 220,
        //MESourceMetadataChanged = 221,
        //MESequencerSourceTopologyUpdated = 222,
        //MESourceV1Anchor = MESequencerSourceTopologyUpdated,

        //MESinkUnknown = 300,
        //MEStreamSinkStarted = 301,
        //MEStreamSinkStopped = 302,
        //MEStreamSinkPaused = 303,
        //MEStreamSinkRateChanged = 304,
        //MEStreamSinkRequestSample = 305,
        //MEStreamSinkMarker = 306,
        //MEStreamSinkPrerolled = 307,
        //MEStreamSinkScrubSampleComplete = 308,
        //MEStreamSinkFormatChanged = 309,
        //MEStreamSinkDeviceChanged = 310,
        //MEQualityNotify = 311,
        //MESinkInvalidated = 312,
        //MEAudioSessionNameChanged = 313,
        //MEAudioSessionVolumeChanged = 314,
        //MEAudioSessionDeviceRemoved = 315,
        //MEAudioSessionServerShutdown = 316,
        //MEAudioSessionGroupingParamChanged = 317,
        //MEAudioSessionIconChanged = 318,
        //MEAudioSessionFormatChanged = 319,
        //MEAudioSessionDisconnected = 320,
        //MEAudioSessionExclusiveModeOverride = 321,
        //MESinkV1Anchor = MEAudioSessionExclusiveModeOverride,

        //MECaptureAudioSessionVolumeChanged = 322,
        MECaptureAudioSessionDeviceRemoved = 323,
        //MECaptureAudioSessionFormatChanged = 324,
        //MECaptureAudioSessionDisconnected = 325,
        //MECaptureAudioSessionExclusiveModeOverride = 326,
        //MECaptureAudioSessionServerShutdown = 327,
        //MESinkV2Anchor = MECaptureAudioSessionServerShutdown,

        //METrustUnknown = 400,
        //MEPolicyChanged = 401,
        //MEContentProtectionMessage = 402,
        //MEPolicySet = 403,
        //METrustV1Anchor = MEPolicySet,

        //MEWMDRMLicenseBackupCompleted = 500,
        //MEWMDRMLicenseBackupProgress = 501,
        //MEWMDRMLicenseRestoreCompleted = 502,
        //MEWMDRMLicenseRestoreProgress = 503,
        //MEWMDRMLicenseAcquisitionCompleted = 506,
        //MEWMDRMIndividualizationCompleted = 508,
        //MEWMDRMIndividualizationProgress = 513,
        //MEWMDRMProximityCompleted = 514,
        //MEWMDRMLicenseStoreCleaned = 515,
        //MEWMDRMRevocationDownloadCompleted = 516,
        //MEWMDRMV1Anchor = MEWMDRMRevocationDownloadCompleted,

        //METransformUnknown = 600,
        //METransformNeedInput,
        //METransformHaveOutput,
        //METransformDrainComplete,
        //METransformMarker,
        //METransformInputStreamStateChanged,
        //MEByteStreamCharacteristicsChanged = 700,
        MEVideoCaptureDeviceRemoved = 800,
        //MEVideoCaptureDevicePreempted = 801,
        //MEStreamSinkFormatInvalidated = 802,
        //MEEncodingParameters = 803,
        //MEContentProtectionMetadata = 900,
        //MEDeviceThermalStateChanged = 950,
        //MEReservedMax = 10000
    }

    [Flags, UnmanagedName("MFASYNC_* defines")]
    internal enum MFASync
    {
        None = 0,
        FastIOProcessingCallback = 0x00000001,
        SignalCallback = 0x00000002,
        BlockingCallback = 0x00000004,
        ReplyCallback = 0x00000008,
        LocalizeRemoteCallback = 0x00000010,
    }

    [UnmanagedName("MFASYNC_CALLBACK_QUEUE_ defines")]
    internal enum MFAsyncCallbackQueue
    {
        Undefined = 0x00000000,
        Standard = 0x00000001,
        RT = 0x00000002,
        IO = 0x00000003,
        Timer = 0x00000004,
        MultiThreaded = 0x00000005,
        LongFunction = 0x00000007,
        PrivateMask = unchecked((int)0xFFFF0000),
        All = unchecked((int)0xFFFFFFFF)
    }

    [UnmanagedName("MF_ATTRIBUTES_MATCH_TYPE")]
    internal enum MFAttributesMatchType
    {
        OurItems,
        TheirItems,
        AllItems,
        InterSection,
        Smaller
    }

    [UnmanagedName("MF_ATTRIBUTE_TYPE")]
    internal enum MFAttributeType
    {
        None = 0x0,
        Blob = 0x1011,
        Double = 0x5,
        Guid = 0x48,
        IUnknown = 13,
        String = 0x1f,
        Uint32 = 0x13,
        Uint64 = 0x15
    }

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

    [Flags, UnmanagedName("MFBYTESTREAM_SEEK_FLAG_ defines")]
    internal enum MFByteStreamSeekingFlags
    {
        None = 0,
        CancelPendingIO = 1
    }

    [UnmanagedName("MFBYTESTREAM_SEEK_ORIGIN")]
    internal enum MFByteStreamSeekOrigin
    {
        Begin,
        Current
    }

    [Flags, UnmanagedName("MFCLOCK_CHARACTERISTICS_FLAGS")]
    internal enum MFClockCharacteristicsFlags
    {
        None = 0,
        Frequency10Mhz = 0x2,
        AlwaysRunning = 0x4,
        IsSystemClock = 0x8
    }

    [Flags, UnmanagedName("MFCLOCK_RELATIONAL_FLAGS")]
    internal enum MFClockRelationalFlags
    {
        None = 0,
        JitterNeverAhead = 0x1
    }

    [UnmanagedName("MFCLOCK_STATE")]
    internal enum MFClockState
    {
        Invalid,
        Running,
        Stopped,
        Paused
    }

    [Flags, UnmanagedName("MF_MEDIATYPE_EQUAL_* defines")]
    internal enum MFMediaEqual
    {
        None = 0,
        MajorTypes = 0x00000001,
        FormatTypes = 0x00000002,
        FormatData = 0x00000004,
        FormatUserData = 0x00000008
    }

    [Flags, UnmanagedName("MFMEDIASOURCE_CHARACTERISTICS")]
    internal enum MFMediaSourceCharacteristics
    {
        None = 0,
        IsLive = 0x1,
        CanSeek = 0x2,
        CanPause = 0x4,
        HasSlowSeek = 0x8,
        HasMultiplePresentations = 0x10,
        CanSkipForward = 0x20,
        CanSkipBackward = 0x40,
        DoesNotUseNetwork = 0x80
    }

    [UnmanagedName("MF_OBJECT_TYPE")]
    internal enum MFObjectType
    {
        MediaSource,
        ByteStream,
        Invalid
    }

    [UnmanagedName("MFRATE_DIRECTION")]
    internal enum MFRateDirection
    {
        Forward = 0,
        Reverse
    }

    [Flags, UnmanagedName("MF_RESOLUTION_* defines")]
    internal enum MFResolution
    {
        None = 0x0,
        MediaSource = 0x00000001,
        ByteStream = 0x00000002,
        ContentDoesNotHaveToMatchExtensionOrMimeType = 0x00000010,
        KeepByteStreamAliveOnFail = 0x00000020,
        DisableLocalPlugins = 0x40,
        PluginControlPolicyApprovedOnly = 0x80,
        PluginControlPolicyWebOnly = 0x100,
        PluginControlPolicyWebOnlyEdgemode = 0x00000200,
        Read = 0x00010000,
        Write = 0x00020000,
    }

    [Flags, UnmanagedName("MFSESSIONCAP_* defines")]
    internal enum MFSessionCapabilities
    {
        None = 0x00000000,
        Start = 0x00000001,
        Seek = 0x00000002,
        Pause = 0x00000004,
        RateForward = 0x00000010,
        RateReverse = 0x00000020,
        DoesNotUseNetwork = 0x00000040
    }

    [Flags, UnmanagedName("MFSESSION_GETFULLTOPOLOGY_FLAGS")]
    internal enum MFSessionGetFullTopologyFlags
    {
        None = 0x0,
        Current = 0x1
    }

    [Flags, UnmanagedName("MFSESSION_SETTOPOLOGY_FLAGS")]
    internal enum MFSessionSetTopologyFlags
    {
        None = 0x0,
        Immediate = 0x1,
        NoResolution = 0x2,
        ClearCurrent = 0x4
    }

    [UnmanagedName("MFSTARTUP_* defines")]
    internal enum MFStartup
    {
        NoSocket = 0x1,
        Lite = 0x1,
        Full = 0
    }

    [UnmanagedName("MF_TOPOLOGY_TYPE")]
    internal enum MFTopologyType
    {
        Max = -1,
        OutputNode = 0,
        SourcestreamNode = 1,
        TeeNode = 3,
        TransformNode = 2
    }

    [Flags, UnmanagedName("MFVideoAspectRatioMode")]
    internal enum MFVideoAspectRatioMode
    {
        None = 0x00000000,
        PreservePicture = 0x00000001,
        PreservePixel = 0x00000002,
        NonLinearStretch = 0x00000004,
        Mask = 0x00000007
    }

    [Flags, UnmanagedName("MFVideoRenderPrefs")]
    internal enum MFVideoRenderPrefs
    {
        None = 0,
        DoNotRenderBorder = 0x00000001,
        DoNotClipToDevice = 0x00000002,
        AllowOutputThrottling = 0x00000004,
        ForceOutputThrottling = 0x00000008,
        ForceBatching = 0x00000010,
        AllowBatching = 0x00000020,
        ForceScaling = 0x00000040,
        AllowScaling = 0x00000080,
        DoNotRepaintOnStop = 0x00000100,
        Mask = 0x000001ff,
    }

    [Flags, UnmanagedName("DXVA2_ProcAmp_* defines")]
    internal enum DXVA2ProcAmp
    {
        None = 0,
        Brightness = 0x0001,
        Contrast = 0x0002,
        Hue = 0x0004,
        Saturation = 0x0008
    }

    [UnmanagedName("MF_SOURCE_READER_CONTROL_FLAG")]
    internal enum MF_SOURCE_READER_CONTROL_FLAG
    {
        None = 0,
        Drain = 0x00000001
    }

    [Flags, UnmanagedName("MF_SOURCE_READER_FLAG")]
    internal enum MF_SOURCE_READER_FLAG
    {
        None = 0,
        Error = 0x00000001,
        EndOfStream = 0x00000002,
        NewStream = 0x00000004,
        NativeMediaTypeChanged = 0x00000010,
        CurrentMediaTypeChanged = 0x00000020,
        AllEffectsRemoved = 0x00000200,
        StreamTick = 0x00000100
    }

    [Flags, UnmanagedName("MFBYTESTREAM_* defines")]
    internal enum MFByteStreamCapabilities
    {
        None = 0x00000000,
        IsReadable = 0x00000001,
        IsWritable = 0x00000002,
        IsSeekable = 0x00000004,
        IsRemote = 0x00000008,
        IsDirectory = 0x00000080,
        HasSlowSeek = 0x00000100,
        IsPartiallyDownloaded = 0x00000200,
        ShareWrite = 0x00000400,
        DoesNotUseNetwork = 0x00000800,
    }

    [UnmanagedName("STATFLAG")]
    internal enum STATFLAG
    {
        Default = 0,
        NoName = 1,
        NoOpen = 2
    }

    internal enum CameraControlProperty
    {
        Pan = 0,
        Tilt,
        Roll,
        Zoom,
        Exposure,
        Iris,
        Focus,
        Flash
    }

    [Flags]
    internal enum CameraControlFlags
    {
        None = 0x0,
        Auto = 0x0001,
        Manual = 0x0002
    }

    internal enum VideoProcAmpProperty
    {
        Brightness,
        Contrast,
        Hue,
        Saturation,
        Sharpness,
        Gamma,
        ColorEnable,
        WhiteBalance,
        BacklightCompensation,
        Gain,
        Multiplier,
        MultiplierLimit,
        WhiteBalanceComponent,
        PowerLineFrequency
    }

    [Flags]
    internal enum VideoProcAmpFlags
    {
        None = 0x0,
        Auto = 0x0001,
        Manual = 0x0002
    }

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

    [StructLayout(LayoutKind.Sequential), UnmanagedName("DXVA2_ValueRange")]
    internal struct DXVA2ValueRange
    {
        public int MinValue;
        public int MaxValue;
        public int DefaultValue;
        public int StepSize;
    }

    [StructLayout(LayoutKind.Sequential), UnmanagedName("DXVA2_VideoProcessorCaps")]
    internal struct DXVA2VideoProcessorCaps
    {
        public int DeviceCaps;
        public int InputPool;
        public int NumForwardRefSamples;
        public int NumBackwardRefSamples;
        public int Reserved;
        public int DeinterlaceTechnology;
        public int ProcAmpControlCaps;
        public int VideoProcessorOperations;
        public int NoiseFilterTechnology;
        public int DetailFilterTechnology;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class MFInt
    {
        protected int m_value;

        public MFInt()
            : this(0)
        {
        }

        public MFInt(int v)
        {
            m_value = v;
        }

        public int GetValue()
        {
            return m_value;
        }

        // While I *could* enable this code, it almost certainly won't do what you
        // think it will.  Generally you don't want to create a *new* instance of
        // MFInt and assign a value to it.  You want to assign a value to an
        // existing instance.  In order to do this automatically, .Net would have
        // to support overloading operator =.  But since it doesn't, use Assign()

        //public static implicit operator MFInt(int f)
        //{
        //    return new MFInt(f);
        //}

        public static implicit operator int(MFInt f)
        {
            return f.m_value;
        }

        public int ToInt32()
        {
            return m_value;
        }

        public void Assign(int f)
        {
            m_value = f;
        }

        public override string ToString()
        {
            return m_value.ToString();
        }

        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is MFInt)
            {
                return ((MFInt)obj).m_value == m_value;
            }

            return Convert.ToInt32(obj) == m_value;
        }
    }

    [StructLayout(LayoutKind.Sequential), UnmanagedName("MF_SINK_WRITER_STATISTICS")]
    internal struct MF_SINK_WRITER_STATISTICS
    {
        public int cb;

        public long llLastTimestampReceived;
        public long llLastTimestampEncoded;
        public long llLastTimestampProcessed;
        public long llLastStreamTickReceived;
        public long llLastSinkSampleRequest;

        public long qwNumSamplesReceived;
        public long qwNumSamplesEncoded;
        public long qwNumSamplesProcessed;
        public long qwNumStreamTicksReceived;

        public int dwByteCountQueued;
        public long qwByteCountProcessed;

        public int dwNumOutstandingSinkSampleRequests;

        public int dwAverageSampleRateReceived;
        public int dwAverageSampleRateEncoded;
        public int dwAverageSampleRateProcessed;
    }

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

    internal static class MFAttributesClsid
    {
        //// Audio Renderer Attributes
        public static readonly Guid MF_AUDIO_RENDERER_ATTRIBUTE_ENDPOINT_ID = new Guid(0xb10aaec3, 0xef71, 0x4cc3, 0xb8, 0x73, 0x5, 0xa9, 0xa0, 0x8b, 0x9f, 0x8e);
        //public static readonly Guid MF_AUDIO_RENDERER_ATTRIBUTE_ENDPOINT_ROLE = new Guid(0x6ba644ff, 0x27c5, 0x4d02, 0x98, 0x87, 0xc2, 0x86, 0x19, 0xfd, 0xb9, 0x1b);
        //public static readonly Guid MF_AUDIO_RENDERER_ATTRIBUTE_FLAGS = new Guid(0xede4b5e0, 0xf805, 0x4d6c, 0x99, 0xb3, 0xdb, 0x01, 0xbf, 0x95, 0xdf, 0xab);
        //public static readonly Guid MF_AUDIO_RENDERER_ATTRIBUTE_SESSION_ID = new Guid(0xede4b5e3, 0xf805, 0x4d6c, 0x99, 0xb3, 0xdb, 0x01, 0xbf, 0x95, 0xdf, 0xab);

        //// Byte Stream Attributes
        //public static readonly Guid MF_BYTESTREAM_ORIGIN_NAME = new Guid(0xfc358288, 0x3cb6, 0x460c, 0xa4, 0x24, 0xb6, 0x68, 0x12, 0x60, 0x37, 0x5a);
        //public static readonly Guid MF_BYTESTREAM_CONTENT_TYPE = new Guid(0xfc358289, 0x3cb6, 0x460c, 0xa4, 0x24, 0xb6, 0x68, 0x12, 0x60, 0x37, 0x5a);
        //public static readonly Guid MF_BYTESTREAM_DURATION = new Guid(0xfc35828a, 0x3cb6, 0x460c, 0xa4, 0x24, 0xb6, 0x68, 0x12, 0x60, 0x37, 0x5a);
        //public static readonly Guid MF_BYTESTREAM_LAST_MODIFIED_TIME = new Guid(0xfc35828b, 0x3cb6, 0x460c, 0xa4, 0x24, 0xb6, 0x68, 0x12, 0x60, 0x37, 0x5a);
        //public static readonly Guid MF_BYTESTREAM_IFO_FILE_URI = new Guid(0xfc35828c, 0x3cb6, 0x460c, 0xa4, 0x24, 0xb6, 0x68, 0x12, 0x60, 0x37, 0x5a);
        //public static readonly Guid MF_BYTESTREAM_DLNA_PROFILE_ID = new Guid(0xfc35828d, 0x3cb6, 0x460c, 0xa4, 0x24, 0xb6, 0x68, 0x12, 0x60, 0x37, 0x5a);
        //public static readonly Guid MF_BYTESTREAM_EFFECTIVE_URL = new Guid(0x9afa0209, 0x89d1, 0x42af, 0x84, 0x56, 0x1d, 0xe6, 0xb5, 0x62, 0xd6, 0x91);
        //public static readonly Guid MF_BYTESTREAM_TRANSCODED = new Guid(0xb6c5c282, 0x4dc9, 0x4db9, 0xab, 0x48, 0xcf, 0x3b, 0x6d, 0x8b, 0xc5, 0xe0);

        //// Enhanced Video Renderer Attributes
        //public static readonly Guid MF_ACTIVATE_CUSTOM_VIDEO_MIXER_ACTIVATE = new Guid(0xba491361, 0xbe50, 0x451e, 0x95, 0xab, 0x6d, 0x4a, 0xcc, 0xc7, 0xda, 0xd8);
        //public static readonly Guid MF_ACTIVATE_CUSTOM_VIDEO_MIXER_CLSID = new Guid(0xba491360, 0xbe50, 0x451e, 0x95, 0xab, 0x6d, 0x4a, 0xcc, 0xc7, 0xda, 0xd8);
        //public static readonly Guid MF_ACTIVATE_CUSTOM_VIDEO_MIXER_FLAGS = new Guid(0xba491362, 0xbe50, 0x451e, 0x95, 0xab, 0x6d, 0x4a, 0xcc, 0xc7, 0xda, 0xd8);
        //public static readonly Guid MF_ACTIVATE_CUSTOM_VIDEO_PRESENTER_ACTIVATE = new Guid(0xba491365, 0xbe50, 0x451e, 0x95, 0xab, 0x6d, 0x4a, 0xcc, 0xc7, 0xda, 0xd8);
        //public static readonly Guid MF_ACTIVATE_CUSTOM_VIDEO_PRESENTER_CLSID = new Guid(0xba491364, 0xbe50, 0x451e, 0x95, 0xab, 0x6d, 0x4a, 0xcc, 0xc7, 0xda, 0xd8);
        //public static readonly Guid MF_ACTIVATE_CUSTOM_VIDEO_PRESENTER_FLAGS = new Guid(0xba491366, 0xbe50, 0x451e, 0x95, 0xab, 0x6d, 0x4a, 0xcc, 0xc7, 0xda, 0xd8);
        //public static readonly Guid MF_ACTIVATE_VIDEO_WINDOW = new Guid(0x9a2dbbdd, 0xf57e, 0x4162, 0x82, 0xb9, 0x68, 0x31, 0x37, 0x76, 0x82, 0xd3);
        //public static readonly Guid MF_SA_REQUIRED_SAMPLE_COUNT = new Guid(0x18802c61, 0x324b, 0x4952, 0xab, 0xd0, 0x17, 0x6f, 0xf5, 0xc6, 0x96, 0xff);
        //public static readonly Guid MF_SA_REQUIRED_SAMPLE_COUNT_PROGRESSIVE = new Guid(0xb172d58e, 0xfa77, 0x4e48, 0x8d, 0x2a, 0x1d, 0xf2, 0xd8, 0x50, 0xea, 0xc2);
        //public static readonly Guid MF_SA_MINIMUM_OUTPUT_SAMPLE_COUNT = new Guid(0x851745d5, 0xc3d6, 0x476d, 0x95, 0x27, 0x49, 0x8e, 0xf2, 0xd1, 0xd, 0x18);
        //public static readonly Guid MF_SA_MINIMUM_OUTPUT_SAMPLE_COUNT_PROGRESSIVE = new Guid(0xf5523a5, 0x1cb2, 0x47c5, 0xa5, 0x50, 0x2e, 0xeb, 0x84, 0xb4, 0xd1, 0x4a);
        //public static readonly Guid VIDEO_ZOOM_RECT = new Guid(0x7aaa1638, 0x1b7f, 0x4c93, 0xbd, 0x89, 0x5b, 0x9c, 0x9f, 0xb6, 0xfc, 0xf0);

        // Event Attributes

        // removed by MS - public static readonly Guid MF_EVENT_FORMAT_CHANGE_REQUEST_SOURCE_SAR = new Guid(0xb26fbdfd, 0xc32c, 0x41fe, 0x9c, 0xf0, 0x8, 0x3c, 0xd5, 0xc7, 0xf8, 0xa4);

        //// MF_EVENT_DO_THINNING {321EA6FB-DAD9-46e4-B31D-D2EAE7090E30}
        //public static readonly Guid MF_EVENT_DO_THINNING = new Guid(0x321ea6fb, 0xdad9, 0x46e4, 0xb3, 0x1d, 0xd2, 0xea, 0xe7, 0x9, 0xe, 0x30);

        //// MF_EVENT_OUTPUT_NODE {830f1a8b-c060-46dd-a801-1c95dec9b107}
        //public static readonly Guid MF_EVENT_OUTPUT_NODE = new Guid(0x830f1a8b, 0xc060, 0x46dd, 0xa8, 0x01, 0x1c, 0x95, 0xde, 0xc9, 0xb1, 0x07);

        //// MF_EVENT_MFT_INPUT_STREAM_ID {F29C2CCA-7AE6-42d2-B284-BF837CC874E2}
        //public static readonly Guid MF_EVENT_MFT_INPUT_STREAM_ID = new Guid(0xf29c2cca, 0x7ae6, 0x42d2, 0xb2, 0x84, 0xbf, 0x83, 0x7c, 0xc8, 0x74, 0xe2);

        //// MF_EVENT_MFT_CONTEXT {B7CD31F1-899E-4b41-80C9-26A896D32977}
        //public static readonly Guid MF_EVENT_MFT_CONTEXT = new Guid(0xb7cd31f1, 0x899e, 0x4b41, 0x80, 0xc9, 0x26, 0xa8, 0x96, 0xd3, 0x29, 0x77);

        //// MF_EVENT_PRESENTATION_TIME_OFFSET {5AD914D1-9B45-4a8d-A2C0-81D1E50BFB07}
        //public static readonly Guid MF_EVENT_PRESENTATION_TIME_OFFSET = new Guid(0x5ad914d1, 0x9b45, 0x4a8d, 0xa2, 0xc0, 0x81, 0xd1, 0xe5, 0xb, 0xfb, 0x7);

        //// MF_EVENT_SCRUBSAMPLE_TIME {9AC712B3-DCB8-44d5-8D0C-37455A2782E3}
        //public static readonly Guid MF_EVENT_SCRUBSAMPLE_TIME = new Guid(0x9ac712b3, 0xdcb8, 0x44d5, 0x8d, 0xc, 0x37, 0x45, 0x5a, 0x27, 0x82, 0xe3);

        //// MF_EVENT_SESSIONCAPS {7E5EBCD0-11B8-4abe-AFAD-10F6599A7F42}
        //public static readonly Guid MF_EVENT_SESSIONCAPS = new Guid(0x7e5ebcd0, 0x11b8, 0x4abe, 0xaf, 0xad, 0x10, 0xf6, 0x59, 0x9a, 0x7f, 0x42);

        //// MF_EVENT_SESSIONCAPS_DELTA {7E5EBCD1-11B8-4abe-AFAD-10F6599A7F42}
        //// Type: UINT32
        //public static readonly Guid MF_EVENT_SESSIONCAPS_DELTA = new Guid(0x7e5ebcd1, 0x11b8, 0x4abe, 0xaf, 0xad, 0x10, 0xf6, 0x59, 0x9a, 0x7f, 0x42);

        //// MF_EVENT_SOURCE_ACTUAL_START {a8cc55a9-6b31-419f-845d-ffb351a2434b}
        //public static readonly Guid MF_EVENT_SOURCE_ACTUAL_START = new Guid(0xa8cc55a9, 0x6b31, 0x419f, 0x84, 0x5d, 0xff, 0xb3, 0x51, 0xa2, 0x43, 0x4b);

        //// MF_EVENT_SOURCE_CHARACTERISTICS {47DB8490-8B22-4f52-AFDA-9CE1B2D3CFA8}
        //public static readonly Guid MF_EVENT_SOURCE_CHARACTERISTICS = new Guid(0x47db8490, 0x8b22, 0x4f52, 0xaf, 0xda, 0x9c, 0xe1, 0xb2, 0xd3, 0xcf, 0xa8);

        //// MF_EVENT_SOURCE_CHARACTERISTICS_OLD {47DB8491-8B22-4f52-AFDA-9CE1B2D3CFA8}
        //public static readonly Guid MF_EVENT_SOURCE_CHARACTERISTICS_OLD = new Guid(0x47db8491, 0x8b22, 0x4f52, 0xaf, 0xda, 0x9c, 0xe1, 0xb2, 0xd3, 0xcf, 0xa8);

        //// MF_EVENT_SOURCE_FAKE_START {a8cc55a7-6b31-419f-845d-ffb351a2434b}
        //public static readonly Guid MF_EVENT_SOURCE_FAKE_START = new Guid(0xa8cc55a7, 0x6b31, 0x419f, 0x84, 0x5d, 0xff, 0xb3, 0x51, 0xa2, 0x43, 0x4b);

        //// MF_EVENT_SOURCE_PROJECTSTART {a8cc55a8-6b31-419f-845d-ffb351a2434b}
        //public static readonly Guid MF_EVENT_SOURCE_PROJECTSTART = new Guid(0xa8cc55a8, 0x6b31, 0x419f, 0x84, 0x5d, 0xff, 0xb3, 0x51, 0xa2, 0x43, 0x4b);

        //// MF_EVENT_SOURCE_TOPOLOGY_CANCELED {DB62F650-9A5E-4704-ACF3-563BC6A73364}
        //public static readonly Guid MF_EVENT_SOURCE_TOPOLOGY_CANCELED = new Guid(0xdb62f650, 0x9a5e, 0x4704, 0xac, 0xf3, 0x56, 0x3b, 0xc6, 0xa7, 0x33, 0x64);

        //// MF_EVENT_START_PRESENTATION_TIME {5AD914D0-9B45-4a8d-A2C0-81D1E50BFB07}
        //public static readonly Guid MF_EVENT_START_PRESENTATION_TIME = new Guid(0x5ad914d0, 0x9b45, 0x4a8d, 0xa2, 0xc0, 0x81, 0xd1, 0xe5, 0xb, 0xfb, 0x7);

        //// MF_EVENT_START_PRESENTATION_TIME_AT_OUTPUT {5AD914D2-9B45-4a8d-A2C0-81D1E50BFB07}
        //public static readonly Guid MF_EVENT_START_PRESENTATION_TIME_AT_OUTPUT = new Guid(0x5ad914d2, 0x9b45, 0x4a8d, 0xa2, 0xc0, 0x81, 0xd1, 0xe5, 0xb, 0xfb, 0x7);

        //// MF_EVENT_TOPOLOGY_STATUS {30C5018D-9A53-454b-AD9E-6D5F8FA7C43B}
        //public static readonly Guid MF_EVENT_TOPOLOGY_STATUS = new Guid(0x30c5018d, 0x9a53, 0x454b, 0xad, 0x9e, 0x6d, 0x5f, 0x8f, 0xa7, 0xc4, 0x3b);

        //// MF_EVENT_STREAM_METADATA_KEYDATA {CD59A4A1-4A3B-4BBD-8665-72A40FBEA776}
        //public static readonly Guid MF_EVENT_STREAM_METADATA_KEYDATA = new Guid(0xcd59a4a1, 0x4a3b, 0x4bbd, 0x86, 0x65, 0x72, 0xa4, 0xf, 0xbe, 0xa7, 0x76);

        //// MF_EVENT_STREAM_METADATA_CONTENT_KEYIDS {5063449D-CC29-4FC6-A75A-D247B35AF85C}
        //public static readonly Guid MF_EVENT_STREAM_METADATA_CONTENT_KEYIDS = new Guid(0x5063449d, 0xcc29, 0x4fc6, 0xa7, 0x5a, 0xd2, 0x47, 0xb3, 0x5a, 0xf8, 0x5c);

        //// MF_EVENT_STREAM_METADATA_SYSTEMID {1EA2EF64-BA16-4A36-8719-FE7560BA32AD}
        //public static readonly Guid MF_EVENT_STREAM_METADATA_SYSTEMID = new Guid(0x1ea2ef64, 0xba16, 0x4a36, 0x87, 0x19, 0xfe, 0x75, 0x60, 0xba, 0x32, 0xad);

        //public static readonly Guid MF_SESSION_APPROX_EVENT_OCCURRENCE_TIME = new Guid(0x190e852f, 0x6238, 0x42d1, 0xb5, 0xaf, 0x69, 0xea, 0x33, 0x8e, 0xf8, 0x50);

        //// Media Session Attributes

        //public static readonly Guid MF_SESSION_CONTENT_PROTECTION_MANAGER = new Guid(0x1e83d482, 0x1f1c, 0x4571, 0x84, 0x5, 0x88, 0xf4, 0xb2, 0x18, 0x1f, 0x74);
        public static readonly Guid MF_SESSION_GLOBAL_TIME = new Guid(0x1e83d482, 0x1f1c, 0x4571, 0x84, 0x5, 0x88, 0xf4, 0xb2, 0x18, 0x1f, 0x72);
        //public static readonly Guid MF_SESSION_QUALITY_MANAGER = new Guid(0x1e83d482, 0x1f1c, 0x4571, 0x84, 0x5, 0x88, 0xf4, 0xb2, 0x18, 0x1f, 0x73);
        //public static readonly Guid MF_SESSION_REMOTE_SOURCE_MODE = new Guid(0xf4033ef4, 0x9bb3, 0x4378, 0x94, 0x1f, 0x85, 0xa0, 0x85, 0x6b, 0xc2, 0x44);
        //public static readonly Guid MF_SESSION_SERVER_CONTEXT = new Guid(0xafe5b291, 0x50fa, 0x46e8, 0xb9, 0xbe, 0xc, 0xc, 0x3c, 0xe4, 0xb3, 0xa5);
        //public static readonly Guid MF_SESSION_TOPOLOADER = new Guid(0x1e83d482, 0x1f1c, 0x4571, 0x84, 0x5, 0x88, 0xf4, 0xb2, 0x18, 0x1f, 0x71);

        // Media Type Attributes

        //// {48eba18e-f8c9-4687-bf11-0a74c9f96a8f}   MF_MT_MAJOR_TYPE                {GUID}
        public static readonly Guid MF_MT_MAJOR_TYPE = new Guid(0x48eba18e, 0xf8c9, 0x4687, 0xbf, 0x11, 0x0a, 0x74, 0xc9, 0xf9, 0x6a, 0x8f);

        //// {f7e34c9a-42e8-4714-b74b-cb29d72c35e5}   MF_MT_SUBTYPE                   {GUID}
        public static readonly Guid MF_MT_SUBTYPE = new Guid(0xf7e34c9a, 0x42e8, 0x4714, 0xb7, 0x4b, 0xcb, 0x29, 0xd7, 0x2c, 0x35, 0xe5);

        //// {c9173739-5e56-461c-b713-46fb995cb95f}   MF_MT_ALL_SAMPLES_INDEPENDENT   {UINT32 (BOOL)}
        //public static readonly Guid MF_MT_ALL_SAMPLES_INDEPENDENT = new Guid(0xc9173739, 0x5e56, 0x461c, 0xb7, 0x13, 0x46, 0xfb, 0x99, 0x5c, 0xb9, 0x5f);

        //// {b8ebefaf-b718-4e04-b0a9-116775e3321b}   MF_MT_FIXED_SIZE_SAMPLES        {UINT32 (BOOL)}
        //public static readonly Guid MF_MT_FIXED_SIZE_SAMPLES = new Guid(0xb8ebefaf, 0xb718, 0x4e04, 0xb0, 0xa9, 0x11, 0x67, 0x75, 0xe3, 0x32, 0x1b);

        //// {3afd0cee-18f2-4ba5-a110-8bea502e1f92}   MF_MT_COMPRESSED                {UINT32 (BOOL)}
        //public static readonly Guid MF_MT_COMPRESSED = new Guid(0x3afd0cee, 0x18f2, 0x4ba5, 0xa1, 0x10, 0x8b, 0xea, 0x50, 0x2e, 0x1f, 0x92);

        //// {dad3ab78-1990-408b-bce2-eba673dacc10}   MF_MT_SAMPLE_SIZE               {UINT32}
        //public static readonly Guid MF_MT_SAMPLE_SIZE = new Guid(0xdad3ab78, 0x1990, 0x408b, 0xbc, 0xe2, 0xeb, 0xa6, 0x73, 0xda, 0xcc, 0x10);

        //// 4d3f7b23-d02f-4e6c-9bee-e4bf2c6c695d     MF_MT_WRAPPED_TYPE              {Blob}
        //public static readonly Guid MF_MT_WRAPPED_TYPE = new Guid(0x4d3f7b23, 0xd02f, 0x4e6c, 0x9b, 0xee, 0xe4, 0xbf, 0x2c, 0x6c, 0x69, 0x5d);

        // {37e48bf5-645e-4c5b-89de-ada9e29b696a}   MF_MT_AUDIO_NUM_CHANNELS            {UINT32}
        public static readonly Guid MF_MT_AUDIO_NUM_CHANNELS = new Guid(0x37e48bf5, 0x645e, 0x4c5b, 0x89, 0xde, 0xad, 0xa9, 0xe2, 0x9b, 0x69, 0x6a);

        // {5faeeae7-0290-4c31-9e8a-c534f68d9dba}   MF_MT_AUDIO_SAMPLES_PER_SECOND      {UINT32}
        public static readonly Guid MF_MT_AUDIO_SAMPLES_PER_SECOND = new Guid(0x5faeeae7, 0x0290, 0x4c31, 0x9e, 0x8a, 0xc5, 0x34, 0xf6, 0x8d, 0x9d, 0xba);

        // {fb3b724a-cfb5-4319-aefe-6e42b2406132}   MF_MT_AUDIO_FLOAT_SAMPLES_PER_SECOND {double}
        //public static readonly Guid MF_MT_AUDIO_FLOAT_SAMPLES_PER_SECOND = new Guid(0xfb3b724a, 0xcfb5, 0x4319, 0xae, 0xfe, 0x6e, 0x42, 0xb2, 0x40, 0x61, 0x32);

        // {1aab75c8-cfef-451c-ab95-ac034b8e1731}   MF_MT_AUDIO_AVG_BYTES_PER_SECOND    {UINT32}
        public static readonly Guid MF_MT_AUDIO_AVG_BYTES_PER_SECOND = new Guid(0x1aab75c8, 0xcfef, 0x451c, 0xab, 0x95, 0xac, 0x03, 0x4b, 0x8e, 0x17, 0x31);

        //// {322de230-9eeb-43bd-ab7a-ff412251541d}   MF_MT_AUDIO_BLOCK_ALIGNMENT         {UINT32}
        public static readonly Guid MF_MT_AUDIO_BLOCK_ALIGNMENT = new Guid(0x322de230, 0x9eeb, 0x43bd, 0xab, 0x7a, 0xff, 0x41, 0x22, 0x51, 0x54, 0x1d);

        // {f2deb57f-40fa-4764-aa33-ed4f2d1ff669}   MF_MT_AUDIO_BITS_PER_SAMPLE         {UINT32}
        public static readonly Guid MF_MT_AUDIO_BITS_PER_SAMPLE = new Guid(0xf2deb57f, 0x40fa, 0x4764, 0xaa, 0x33, 0xed, 0x4f, 0x2d, 0x1f, 0xf6, 0x69);

        //// {d9bf8d6a-9530-4b7c-9ddf-ff6fd58bbd06}   MF_MT_AUDIO_VALID_BITS_PER_SAMPLE   {UINT32}
        //public static readonly Guid MF_MT_AUDIO_VALID_BITS_PER_SAMPLE = new Guid(0xd9bf8d6a, 0x9530, 0x4b7c, 0x9d, 0xdf, 0xff, 0x6f, 0xd5, 0x8b, 0xbd, 0x06);

        //// {aab15aac-e13a-4995-9222-501ea15c6877}   MF_MT_AUDIO_SAMPLES_PER_BLOCK       {UINT32}
        //public static readonly Guid MF_MT_AUDIO_SAMPLES_PER_BLOCK = new Guid(0xaab15aac, 0xe13a, 0x4995, 0x92, 0x22, 0x50, 0x1e, 0xa1, 0x5c, 0x68, 0x77);

        //// {55fb5765-644a-4caf-8479-938983bb1588}`  MF_MT_AUDIO_CHANNEL_MASK            {UINT32}
        //public static readonly Guid MF_MT_AUDIO_CHANNEL_MASK = new Guid(0x55fb5765, 0x644a, 0x4caf, 0x84, 0x79, 0x93, 0x89, 0x83, 0xbb, 0x15, 0x88);

        //// {9d62927c-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_FOLDDOWN_MATRIX         {BLOB, MFFOLDDOWN_MATRIX}
        //public static readonly Guid MF_MT_AUDIO_FOLDDOWN_MATRIX = new Guid(0x9d62927c, 0x36be, 0x4cf2, 0xb5, 0xc4, 0xa3, 0x92, 0x6e, 0x3e, 0x87, 0x11);

        //// {0x9d62927d-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_WMADRC_PEAKREF         {UINT32}
        //public static readonly Guid MF_MT_AUDIO_WMADRC_PEAKREF = new Guid(0x9d62927d, 0x36be, 0x4cf2, 0xb5, 0xc4, 0xa3, 0x92, 0x6e, 0x3e, 0x87, 0x11);

        //// {0x9d62927e-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_WMADRC_PEAKTARGET        {UINT32}
        //public static readonly Guid MF_MT_AUDIO_WMADRC_PEAKTARGET = new Guid(0x9d62927e, 0x36be, 0x4cf2, 0xb5, 0xc4, 0xa3, 0x92, 0x6e, 0x3e, 0x87, 0x11);

        //// {0x9d62927f-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_WMADRC_AVGREF         {UINT32}
        //public static readonly Guid MF_MT_AUDIO_WMADRC_AVGREF = new Guid(0x9d62927f, 0x36be, 0x4cf2, 0xb5, 0xc4, 0xa3, 0x92, 0x6e, 0x3e, 0x87, 0x11);

        //// {0x9d629280-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_WMADRC_AVGTARGET      {UINT32}
        //public static readonly Guid MF_MT_AUDIO_WMADRC_AVGTARGET = new Guid(0x9d629280, 0x36be, 0x4cf2, 0xb5, 0xc4, 0xa3, 0x92, 0x6e, 0x3e, 0x87, 0x11);

        //// {a901aaba-e037-458a-bdf6-545be2074042}   MF_MT_AUDIO_PREFER_WAVEFORMATEX     {UINT32 (BOOL)}
        public static readonly Guid MF_MT_AUDIO_PREFER_WAVEFORMATEX = new Guid(0xa901aaba, 0xe037, 0x458a, 0xbd, 0xf6, 0x54, 0x5b, 0xe2, 0x07, 0x40, 0x42);

        //// {BFBABE79-7434-4d1c-94F0-72A3B9E17188} MF_MT_AAC_PAYLOAD_TYPE       {UINT32}
        //public static readonly Guid MF_MT_AAC_PAYLOAD_TYPE = new Guid(0xbfbabe79, 0x7434, 0x4d1c, 0x94, 0xf0, 0x72, 0xa3, 0xb9, 0xe1, 0x71, 0x88);

        //// {7632F0E6-9538-4d61-ACDA-EA29C8C14456} MF_MT_AAC_AUDIO_PROFILE_LEVEL_INDICATION       {UINT32}
        public static readonly Guid MF_MT_AAC_AUDIO_PROFILE_LEVEL_INDICATION = new Guid(0x7632f0e6, 0x9538, 0x4d61, 0xac, 0xda, 0xea, 0x29, 0xc8, 0xc1, 0x44, 0x56);



        // {1652c33d-d6b2-4012-b834-72030849a37d}   MF_MT_FRAME_SIZE                {UINT64 (HI32(Width),LO32(Height))}
        public static readonly Guid MF_MT_FRAME_SIZE = new Guid(0x1652c33d, 0xd6b2, 0x4012, 0xb8, 0x34, 0x72, 0x03, 0x08, 0x49, 0xa3, 0x7d);

        // {c459a2e8-3d2c-4e44-b132-fee5156c7bb0}   MF_MT_FRAME_RATE                {UINT64 (HI32(Numerator),LO32(Denominator))}
        public static readonly Guid MF_MT_FRAME_RATE = new Guid(0xc459a2e8, 0x3d2c, 0x4e44, 0xb1, 0x32, 0xfe, 0xe5, 0x15, 0x6c, 0x7b, 0xb0);

        //// {c6376a1e-8d0a-4027-be45-6d9a0ad39bb6}   MF_MT_PIXEL_ASPECT_RATIO        {UINT64 (HI32(Numerator),LO32(Denominator))}
        public static readonly Guid MF_MT_PIXEL_ASPECT_RATIO = new Guid(0xc6376a1e, 0x8d0a, 0x4027, 0xbe, 0x45, 0x6d, 0x9a, 0x0a, 0xd3, 0x9b, 0xb6);

        //// {8772f323-355a-4cc7-bb78-6d61a048ae82}   MF_MT_DRM_FLAGS                 {UINT32 (anyof MFVideoDRMFlags)}
        //public static readonly Guid MF_MT_DRM_FLAGS = new Guid(0x8772f323, 0x355a, 0x4cc7, 0xbb, 0x78, 0x6d, 0x61, 0xa0, 0x48, 0xae, 0x82);

        //// {24974215-1B7B-41e4-8625-AC469F2DEDAA}   MF_MT_TIMESTAMP_CAN_BE_DTS      {UINT32 (BOOL)}
        //public static readonly Guid MF_MT_TIMESTAMP_CAN_BE_DTS = new Guid(0x24974215, 0x1b7b, 0x41e4, 0x86, 0x25, 0xac, 0x46, 0x9f, 0x2d, 0xed, 0xaa);

        //// {A20AF9E8-928A-4B26-AAA9-F05C74CAC47C}   MF_MT_MPEG2_STANDARD            {UINT32 (0 for default MPEG2, 1  to use ATSC standard, 2 to use DVB standard, 3 to use ARIB standard)}
        //public static readonly Guid MF_MT_MPEG2_STANDARD = new Guid(0xa20af9e8, 0x928a, 0x4b26, 0xaa, 0xa9, 0xf0, 0x5c, 0x74, 0xca, 0xc4, 0x7c);

        //// {5229BA10-E29D-4F80-A59C-DF4F180207D2}   MF_MT_MPEG2_TIMECODE            {UINT32 (0 for no timecode, 1 to append an 4 byte timecode to the front of each transport packet)}
        //public static readonly Guid MF_MT_MPEG2_TIMECODE = new Guid(0x5229ba10, 0xe29d, 0x4f80, 0xa5, 0x9c, 0xdf, 0x4f, 0x18, 0x2, 0x7, 0xd2);

        //// {825D55E4-4F12-4197-9EB3-59B6E4710F06}   MF_MT_MPEG2_CONTENT_PACKET      {UINT32 (0 for no content packet, 1 to append a 14 byte Content Packet header according to the ARIB specification to the beginning a transport packet at 200-1000 ms intervals.)}
        //public static readonly Guid MF_MT_MPEG2_CONTENT_PACKET = new Guid(0x825d55e4, 0x4f12, 0x4197, 0x9e, 0xb3, 0x59, 0xb6, 0xe4, 0x71, 0xf, 0x6);

        ////
        //// VIDEO - H264 extra data
        ////

        //// {F5929986-4C45-4FBB-BB49-6CC534D05B9B}  {UINT32, UVC 1.5 H.264 format descriptor: bMaxCodecConfigDelay}
        //public static readonly Guid MF_MT_H264_MAX_CODEC_CONFIG_DELAY = new Guid(0xf5929986, 0x4c45, 0x4fbb, 0xbb, 0x49, 0x6c, 0xc5, 0x34, 0xd0, 0x5b, 0x9b);

        //// {C8BE1937-4D64-4549-8343-A8086C0BFDA5} {UINT32, UVC 1.5 H.264 format descriptor: bmSupportedSliceModes}
        //public static readonly Guid MF_MT_H264_SUPPORTED_SLICE_MODES = new Guid(0xc8be1937, 0x4d64, 0x4549, 0x83, 0x43, 0xa8, 0x8, 0x6c, 0xb, 0xfd, 0xa5);

        //// {89A52C01-F282-48D2-B522-22E6AE633199} {UINT32, UVC 1.5 H.264 format descriptor: bmSupportedSyncFrameTypes}
        //public static readonly Guid MF_MT_H264_SUPPORTED_SYNC_FRAME_TYPES = new Guid(0x89a52c01, 0xf282, 0x48d2, 0xb5, 0x22, 0x22, 0xe6, 0xae, 0x63, 0x31, 0x99);

        //// {E3854272-F715-4757-BA90-1B696C773457} {UINT32, UVC 1.5 H.264 format descriptor: bResolutionScaling}
        //public static readonly Guid MF_MT_H264_RESOLUTION_SCALING = new Guid(0xe3854272, 0xf715, 0x4757, 0xba, 0x90, 0x1b, 0x69, 0x6c, 0x77, 0x34, 0x57);

        //// {9EA2D63D-53F0-4A34-B94E-9DE49A078CB3} {UINT32, UVC 1.5 H.264 format descriptor: bSimulcastSupport}
        //public static readonly Guid MF_MT_H264_SIMULCAST_SUPPORT = new Guid(0x9ea2d63d, 0x53f0, 0x4a34, 0xb9, 0x4e, 0x9d, 0xe4, 0x9a, 0x7, 0x8c, 0xb3);

        //// {6A8AC47E-519C-4F18-9BB3-7EEAAEA5594D} {UINT32, UVC 1.5 H.264 format descriptor: bmSupportedRateControlModes}
        //public static readonly Guid MF_MT_H264_SUPPORTED_RATE_CONTROL_MODES = new Guid(0x6a8ac47e, 0x519c, 0x4f18, 0x9b, 0xb3, 0x7e, 0xea, 0xae, 0xa5, 0x59, 0x4d);

        //// {45256D30-7215-4576-9336-B0F1BCD59BB2}  {Blob of size 20 * sizeof(WORD), UVC 1.5 H.264 format descriptor: wMaxMBperSec*}
        //public static readonly Guid MF_MT_H264_MAX_MB_PER_SEC = new Guid(0x45256d30, 0x7215, 0x4576, 0x93, 0x36, 0xb0, 0xf1, 0xbc, 0xd5, 0x9b, 0xb2);

        //// {60B1A998-DC01-40CE-9736-ABA845A2DBDC}         {UINT32, UVC 1.5 H.264 frame descriptor: bmSupportedUsages}
        //public static readonly Guid MF_MT_H264_SUPPORTED_USAGES = new Guid(0x60b1a998, 0xdc01, 0x40ce, 0x97, 0x36, 0xab, 0xa8, 0x45, 0xa2, 0xdb, 0xdc);

        //// {BB3BD508-490A-11E0-99E4-1316DFD72085}         {UINT32, UVC 1.5 H.264 frame descriptor: bmCapabilities}
        //public static readonly Guid MF_MT_H264_CAPABILITIES = new Guid(0xbb3bd508, 0x490a, 0x11e0, 0x99, 0xe4, 0x13, 0x16, 0xdf, 0xd7, 0x20, 0x85);

        //// {F8993ABE-D937-4A8F-BBCA-6966FE9E1152}         {UINT32, UVC 1.5 H.264 frame descriptor: bmSVCCapabilities}
        //public static readonly Guid MF_MT_H264_SVC_CAPABILITIES = new Guid(0xf8993abe, 0xd937, 0x4a8f, 0xbb, 0xca, 0x69, 0x66, 0xfe, 0x9e, 0x11, 0x52);

        //// {359CE3A5-AF00-49CA-A2F4-2AC94CA82B61}         {UINT32, UVC 1.5 H.264 Probe/Commit Control: bUsage}
        //public static readonly Guid MF_MT_H264_USAGE = new Guid(0x359ce3a5, 0xaf00, 0x49ca, 0xa2, 0xf4, 0x2a, 0xc9, 0x4c, 0xa8, 0x2b, 0x61);

        ////{705177D8-45CB-11E0-AC7D-B91CE0D72085}          {UINT32, UVC 1.5 H.264 Probe/Commit Control: bmRateControlModes}
        //public static readonly Guid MF_MT_H264_RATE_CONTROL_MODES = new Guid(0x705177d8, 0x45cb, 0x11e0, 0xac, 0x7d, 0xb9, 0x1c, 0xe0, 0xd7, 0x20, 0x85);

        ////{85E299B2-90E3-4FE8-B2F5-C067E0BFE57A}          {UINT64, UVC 1.5 H.264 Probe/Commit Control: bmLayoutPerStream}
        //public static readonly Guid MF_MT_H264_LAYOUT_PER_STREAM = new Guid(0x85e299b2, 0x90e3, 0x4fe8, 0xb2, 0xf5, 0xc0, 0x67, 0xe0, 0xbf, 0xe5, 0x7a);

        //// {4d0e73e5-80ea-4354-a9d0-1176ceb028ea}   MF_MT_PAD_CONTROL_FLAGS         {UINT32 (oneof MFVideoPadFlags)}
        //public static readonly Guid MF_MT_PAD_CONTROL_FLAGS = new Guid(0x4d0e73e5, 0x80ea, 0x4354, 0xa9, 0xd0, 0x11, 0x76, 0xce, 0xb0, 0x28, 0xea);

        //// {68aca3cc-22d0-44e6-85f8-28167197fa38}   MF_MT_SOURCE_CONTENT_HINT       {UINT32 (oneof MFVideoSrcContentHintFlags)}
        //public static readonly Guid MF_MT_SOURCE_CONTENT_HINT = new Guid(0x68aca3cc, 0x22d0, 0x44e6, 0x85, 0xf8, 0x28, 0x16, 0x71, 0x97, 0xfa, 0x38);

        //// {65df2370-c773-4c33-aa64-843e068efb0c}   MF_MT_CHROMA_SITING             {UINT32 (anyof MFVideoChromaSubsampling)}
        //public static readonly Guid MF_MT_VIDEO_CHROMA_SITING = new Guid(0x65df2370, 0xc773, 0x4c33, 0xaa, 0x64, 0x84, 0x3e, 0x06, 0x8e, 0xfb, 0x0c);

        //// {e2724bb8-e676-4806-b4b2-a8d6efb44ccd}   MF_MT_INTERLACE_MODE            {UINT32 (oneof MFVideoInterlaceMode)}
        public static readonly Guid MF_MT_INTERLACE_MODE = new Guid(0xe2724bb8, 0xe676, 0x4806, 0xb4, 0xb2, 0xa8, 0xd6, 0xef, 0xb4, 0x4c, 0xcd);

        //// {5fb0fce9-be5c-4935-a811-ec838f8eed93}   MF_MT_TRANSFER_FUNCTION         {UINT32 (oneof MFVideoTransferFunction)}
        //public static readonly Guid MF_MT_TRANSFER_FUNCTION = new Guid(0x5fb0fce9, 0xbe5c, 0x4935, 0xa8, 0x11, 0xec, 0x83, 0x8f, 0x8e, 0xed, 0x93);

        //// {dbfbe4d7-0740-4ee0-8192-850ab0e21935}   MF_MT_VIDEO_PRIMARIES           {UINT32 (oneof MFVideoPrimaries)}
        //public static readonly Guid MF_MT_VIDEO_PRIMARIES = new Guid(0xdbfbe4d7, 0x0740, 0x4ee0, 0x81, 0x92, 0x85, 0x0a, 0xb0, 0xe2, 0x19, 0x35);

        //// {47537213-8cfb-4722-aa34-fbc9e24d77b8}   MF_MT_CUSTOM_VIDEO_PRIMARIES    {BLOB (MT_CUSTOM_VIDEO_PRIMARIES)}
        //public static readonly Guid MF_MT_CUSTOM_VIDEO_PRIMARIES = new Guid(0x47537213, 0x8cfb, 0x4722, 0xaa, 0x34, 0xfb, 0xc9, 0xe2, 0x4d, 0x77, 0xb8);

        //// {3e23d450-2c75-4d25-a00e-b91670d12327}   MF_MT_YUV_MATRIX                {UINT32 (oneof MFVideoTransferMatrix)}
        //public static readonly Guid MF_MT_YUV_MATRIX = new Guid(0x3e23d450, 0x2c75, 0x4d25, 0xa0, 0x0e, 0xb9, 0x16, 0x70, 0xd1, 0x23, 0x27);

        //// {53a0529c-890b-4216-8bf9-599367ad6d20}   MF_MT_VIDEO_LIGHTING            {UINT32 (oneof MFVideoLighting)}
        //public static readonly Guid MF_MT_VIDEO_LIGHTING = new Guid(0x53a0529c, 0x890b, 0x4216, 0x8b, 0xf9, 0x59, 0x93, 0x67, 0xad, 0x6d, 0x20);

        //// {c21b8ee5-b956-4071-8daf-325edf5cab11}   MF_MT_VIDEO_NOMINAL_RANGE       {UINT32 (oneof MFNominalRange)}
        //public static readonly Guid MF_MT_VIDEO_NOMINAL_RANGE = new Guid(0xc21b8ee5, 0xb956, 0x4071, 0x8d, 0xaf, 0x32, 0x5e, 0xdf, 0x5c, 0xab, 0x11);

        //// {66758743-7e5f-400d-980a-aa8596c85696}   MF_MT_GEOMETRIC_APERTURE        {BLOB (MFVideoArea)}
        //public static readonly Guid MF_MT_GEOMETRIC_APERTURE = new Guid(0x66758743, 0x7e5f, 0x400d, 0x98, 0x0a, 0xaa, 0x85, 0x96, 0xc8, 0x56, 0x96);

        //// {d7388766-18fe-48c6-a177-ee894867c8c4}   MF_MT_MINIMUM_DISPLAY_APERTURE  {BLOB (MFVideoArea)}
        //public static readonly Guid MF_MT_MINIMUM_DISPLAY_APERTURE = new Guid(0xd7388766, 0x18fe, 0x48c6, 0xa1, 0x77, 0xee, 0x89, 0x48, 0x67, 0xc8, 0xc4);

        //// {79614dde-9187-48fb-b8c7-4d52689de649}   MF_MT_PAN_SCAN_APERTURE         {BLOB (MFVideoArea)}
        //public static readonly Guid MF_MT_PAN_SCAN_APERTURE = new Guid(0x79614dde, 0x9187, 0x48fb, 0xb8, 0xc7, 0x4d, 0x52, 0x68, 0x9d, 0xe6, 0x49);

        //// {4b7f6bc3-8b13-40b2-a993-abf630b8204e}   MF_MT_PAN_SCAN_ENABLED          {UINT32 (BOOL)}
        //public static readonly Guid MF_MT_PAN_SCAN_ENABLED = new Guid(0x4b7f6bc3, 0x8b13, 0x40b2, 0xa9, 0x93, 0xab, 0xf6, 0x30, 0xb8, 0x20, 0x4e);

        //// {20332624-fb0d-4d9e-bd0d-cbf6786c102e}   MF_MT_AVG_BITRATE               {UINT32}
        public static readonly Guid MF_MT_AVG_BITRATE = new Guid(0x20332624, 0xfb0d, 0x4d9e, 0xbd, 0x0d, 0xcb, 0xf6, 0x78, 0x6c, 0x10, 0x2e);

        //// {799cabd6-3508-4db4-a3c7-569cd533deb1}   MF_MT_AVG_BIT_ERROR_RATE        {UINT32}
        //public static readonly Guid MF_MT_AVG_BIT_ERROR_RATE = new Guid(0x799cabd6, 0x3508, 0x4db4, 0xa3, 0xc7, 0x56, 0x9c, 0xd5, 0x33, 0xde, 0xb1);

        //// {c16eb52b-73a1-476f-8d62-839d6a020652}   MF_MT_MAX_KEYFRAME_SPACING      {UINT32}
        //public static readonly Guid MF_MT_MAX_KEYFRAME_SPACING = new Guid(0xc16eb52b, 0x73a1, 0x476f, 0x8d, 0x62, 0x83, 0x9d, 0x6a, 0x02, 0x06, 0x52);

        //// {644b4e48-1e02-4516-b0eb-c01ca9d49ac6}   MF_MT_DEFAULT_STRIDE            {UINT32 (INT32)} // in bytes
        //public static readonly Guid MF_MT_DEFAULT_STRIDE = new Guid(0x644b4e48, 0x1e02, 0x4516, 0xb0, 0xeb, 0xc0, 0x1c, 0xa9, 0xd4, 0x9a, 0xc6);

        //// {6d283f42-9846-4410-afd9-654d503b1a54}   MF_MT_PALETTE                   {BLOB (array of MFPaletteEntry - usually 256)}
        //public static readonly Guid MF_MT_PALETTE = new Guid(0x6d283f42, 0x9846, 0x4410, 0xaf, 0xd9, 0x65, 0x4d, 0x50, 0x3b, 0x1a, 0x54);

        //// {b6bc765f-4c3b-40a4-bd51-2535b66fe09d}   MF_MT_USER_DATA                 {BLOB}
        public static readonly Guid MF_MT_USER_DATA = new Guid(0xb6bc765f, 0x4c3b, 0x40a4, 0xbd, 0x51, 0x25, 0x35, 0xb6, 0x6f, 0xe0, 0x9d);

        //// {73d1072d-1870-4174-a063-29ff4ff6c11e}
        //public static readonly Guid MF_MT_AM_FORMAT_TYPE = new Guid(0x73d1072d, 0x1870, 0x4174, 0xa0, 0x63, 0x29, 0xff, 0x4f, 0xf6, 0xc1, 0x1e);

        //// {ad76a80b-2d5c-4e0b-b375-64e520137036}   MF_MT_VIDEO_PROFILE             {UINT32}    This is an alias of  MF_MT_MPEG2_PROFILE
        //public static readonly Guid MF_MT_VIDEO_PROFILE = new Guid(0xad76a80b, 0x2d5c, 0x4e0b, 0xb3, 0x75, 0x64, 0xe5, 0x20, 0x13, 0x70, 0x36);

        //// {96f66574-11c5-4015-8666-bff516436da7}   MF_MT_VIDEO_LEVEL               {UINT32}    This is an alias of  MF_MT_MPEG2_LEVEL
        //public static readonly Guid MF_MT_VIDEO_LEVEL = new Guid(0x96f66574, 0x11c5, 0x4015, 0x86, 0x66, 0xbf, 0xf5, 0x16, 0x43, 0x6d, 0xa7);

        //// {91f67885-4333-4280-97cd-bd5a6c03a06e}   MF_MT_MPEG_START_TIME_CODE      {UINT32}
        //public static readonly Guid MF_MT_MPEG_START_TIME_CODE = new Guid(0x91f67885, 0x4333, 0x4280, 0x97, 0xcd, 0xbd, 0x5a, 0x6c, 0x03, 0xa0, 0x6e);

        //// {ad76a80b-2d5c-4e0b-b375-64e520137036}   MF_MT_MPEG2_PROFILE             {UINT32 (oneof AM_MPEG2Profile)}
        //public static readonly Guid MF_MT_MPEG2_PROFILE = new Guid(0xad76a80b, 0x2d5c, 0x4e0b, 0xb3, 0x75, 0x64, 0xe5, 0x20, 0x13, 0x70, 0x36);

        //// {96f66574-11c5-4015-8666-bff516436da7}   MF_MT_MPEG2_LEVEL               {UINT32 (oneof AM_MPEG2Level)}
        //public static readonly Guid MF_MT_MPEG2_LEVEL = new Guid(0x96f66574, 0x11c5, 0x4015, 0x86, 0x66, 0xbf, 0xf5, 0x16, 0x43, 0x6d, 0xa7);

        //// {31e3991d-f701-4b2f-b426-8ae3bda9e04b}   MF_MT_MPEG2_FLAGS               {UINT32 (anyof AMMPEG2_xxx flags)}
        //public static readonly Guid MF_MT_MPEG2_FLAGS = new Guid(0x31e3991d, 0xf701, 0x4b2f, 0xb4, 0x26, 0x8a, 0xe3, 0xbd, 0xa9, 0xe0, 0x4b);

        //// {3c036de7-3ad0-4c9e-9216-ee6d6ac21cb3}   MF_MT_MPEG_SEQUENCE_HEADER      {BLOB}
        //public static readonly Guid MF_MT_MPEG_SEQUENCE_HEADER = new Guid(0x3c036de7, 0x3ad0, 0x4c9e, 0x92, 0x16, 0xee, 0x6d, 0x6a, 0xc2, 0x1c, 0xb3);

        //// {84bd5d88-0fb8-4ac8-be4b-a8848bef98f3}   MF_MT_DV_AAUX_SRC_PACK_0        {UINT32}
        //public static readonly Guid MF_MT_DV_AAUX_SRC_PACK_0 = new Guid(0x84bd5d88, 0x0fb8, 0x4ac8, 0xbe, 0x4b, 0xa8, 0x84, 0x8b, 0xef, 0x98, 0xf3);

        //// {f731004e-1dd1-4515-aabe-f0c06aa536ac}   MF_MT_DV_AAUX_CTRL_PACK_0       {UINT32}
        //public static readonly Guid MF_MT_DV_AAUX_CTRL_PACK_0 = new Guid(0xf731004e, 0x1dd1, 0x4515, 0xaa, 0xbe, 0xf0, 0xc0, 0x6a, 0xa5, 0x36, 0xac);

        //// {720e6544-0225-4003-a651-0196563a958e}   MF_MT_DV_AAUX_SRC_PACK_1        {UINT32}
        //public static readonly Guid MF_MT_DV_AAUX_SRC_PACK_1 = new Guid(0x720e6544, 0x0225, 0x4003, 0xa6, 0x51, 0x01, 0x96, 0x56, 0x3a, 0x95, 0x8e);

        //// {cd1f470d-1f04-4fe0-bfb9-d07ae0386ad8}   MF_MT_DV_AAUX_CTRL_PACK_1       {UINT32}
        //public static readonly Guid MF_MT_DV_AAUX_CTRL_PACK_1 = new Guid(0xcd1f470d, 0x1f04, 0x4fe0, 0xbf, 0xb9, 0xd0, 0x7a, 0xe0, 0x38, 0x6a, 0xd8);

        //// {41402d9d-7b57-43c6-b129-2cb997f15009}   MF_MT_DV_VAUX_SRC_PACK          {UINT32}
        //public static readonly Guid MF_MT_DV_VAUX_SRC_PACK = new Guid(0x41402d9d, 0x7b57, 0x43c6, 0xb1, 0x29, 0x2c, 0xb9, 0x97, 0xf1, 0x50, 0x09);

        //// {2f84e1c4-0da1-4788-938e-0dfbfbb34b48}   MF_MT_DV_VAUX_CTRL_PACK         {UINT32}
        //public static readonly Guid MF_MT_DV_VAUX_CTRL_PACK = new Guid(0x2f84e1c4, 0x0da1, 0x4788, 0x93, 0x8e, 0x0d, 0xfb, 0xfb, 0xb3, 0x4b, 0x48);

        //// {5315d8a0-87c5-4697-b793-666c67c49b}         MF_MT_VIDEO_3D_FORMAT           {UINT32 (anyof MFVideo3DFormat)}
        //public static readonly Guid MF_MT_VIDEO_3D_FORMAT = new Guid(0x5315d8a0, 0x87c5, 0x4697, 0xb7, 0x93, 0x66, 0x6, 0xc6, 0x7c, 0x4, 0x9b);

        //// {BB077E8A-DCBF-42eb-AF60-418DF98AA495}       MF_MT_VIDEO_3D_NUM_VIEW         {UINT32}
        //public static readonly Guid MF_MT_VIDEO_3D_NUM_VIEWS = new Guid(0xbb077e8a, 0xdcbf, 0x42eb, 0xaf, 0x60, 0x41, 0x8d, 0xf9, 0x8a, 0xa4, 0x95);

        //// {6D4B7BFF-5629-4404-948C-C634F4CE26D4}       MF_MT_VIDEO_3D_LEFT_IS_BASE     {UINT32}
        //public static readonly Guid MF_MT_VIDEO_3D_LEFT_IS_BASE = new Guid(0x6d4b7bff, 0x5629, 0x4404, 0x94, 0x8c, 0xc6, 0x34, 0xf4, 0xce, 0x26, 0xd4);

        //// {EC298493-0ADA-4ea1-A4FE-CBBD36CE9331}       MF_MT_VIDEO_3D_FIRST_IS_LEFT    {UINT32 (BOOL)}
        //public static readonly Guid MF_MT_VIDEO_3D_FIRST_IS_LEFT = new Guid(0xec298493, 0xada, 0x4ea1, 0xa4, 0xfe, 0xcb, 0xbd, 0x36, 0xce, 0x93, 0x31);

        //public static readonly Guid MF_MT_VIDEO_ROTATION = new Guid(0xc380465d, 0x2271, 0x428c, 0x9b, 0x83, 0xec, 0xea, 0x3b, 0x4a, 0x85, 0xc1);


        //// Sample Attributes

        //public static readonly Guid MFSampleExtension_DecodeTimestamp = new Guid(0x73a954d4, 0x9e2, 0x4861, 0xbe, 0xfc, 0x94, 0xbd, 0x97, 0xc0, 0x8e, 0x6e);

        //public static readonly Guid MFSampleExtension_VideoEncodeQP = new Guid(0xb2efe478, 0xf979, 0x4c66, 0xb9, 0x5e, 0xee, 0x2b, 0x82, 0xc8, 0x2f, 0x36);

        //public static readonly Guid MFSampleExtension_VideoEncodePictureType = new Guid(0x973704e6, 0xcd14, 0x483c, 0x8f, 0x20, 0xc9, 0xfc, 0x9, 0x28, 0xba, 0xd5);

        //public static readonly Guid MFSampleExtension_FrameCorruption = new Guid(0xb4dd4a8c, 0xbeb, 0x44c4, 0x8b, 0x75, 0xb0, 0x2b, 0x91, 0x3b, 0x4, 0xf0);

        //// {941ce0a3-6ae3-4dda-9a08-a64298340617}   MFSampleExtension_BottomFieldFirst
        //public static readonly Guid MFSampleExtension_BottomFieldFirst = new Guid(0x941ce0a3, 0x6ae3, 0x4dda, 0x9a, 0x08, 0xa6, 0x42, 0x98, 0x34, 0x06, 0x17);

        //// MFSampleExtension_CleanPoint {9cdf01d8-a0f0-43ba-b077-eaa06cbd728a}
        //public static readonly Guid MFSampleExtension_CleanPoint = new Guid(0x9cdf01d8, 0xa0f0, 0x43ba, 0xb0, 0x77, 0xea, 0xa0, 0x6c, 0xbd, 0x72, 0x8a);

        //// {6852465a-ae1c-4553-8e9b-c3420fcb1637}   MFSampleExtension_DerivedFromTopField
        //public static readonly Guid MFSampleExtension_DerivedFromTopField = new Guid(0x6852465a, 0xae1c, 0x4553, 0x8e, 0x9b, 0xc3, 0x42, 0x0f, 0xcb, 0x16, 0x37);

        //// MFSampleExtension_MeanAbsoluteDifference {1cdbde11-08b4-4311-a6dd-0f9f371907aa}
        //public static readonly Guid MFSampleExtension_MeanAbsoluteDifference = new Guid(0x1cdbde11, 0x08b4, 0x4311, 0xa6, 0xdd, 0x0f, 0x9f, 0x37, 0x19, 0x07, 0xaa);

        //// MFSampleExtension_LongTermReferenceFrameInfo {9154733f-e1bd-41bf-81d3-fcd918f71332}
        //public static readonly Guid MFSampleExtension_LongTermReferenceFrameInfo = new Guid(0x9154733f, 0xe1bd, 0x41bf, 0x81, 0xd3, 0xfc, 0xd9, 0x18, 0xf7, 0x13, 0x32);

        //// MFSampleExtension_ROIRectangle {3414a438-4998-4d2c-be82-be3ca0b24d43}
        //public static readonly Guid MFSampleExtension_ROIRectangle = new Guid(0x3414a438, 0x4998, 0x4d2c, 0xbe, 0x82, 0xbe, 0x3c, 0xa0, 0xb2, 0x4d, 0x43);

        //// MFSampleExtension_PhotoThumbnail {74BBC85C-C8BB-42DC-B586DA17FFD35DCC}
        //public static readonly Guid MFSampleExtension_PhotoThumbnail = new Guid(0x74BBC85C, 0xC8BB, 0x42DC, 0xB5, 0x86, 0xDA, 0x17, 0xFF, 0xD3, 0x5D, 0xCC);

        //// MFSampleExtension_PhotoThumbnailMediaType {61AD5420-EBF8-4143-89AF6BF25F672DEF}
        //public static readonly Guid MFSampleExtension_PhotoThumbnailMediaType = new Guid(0x61AD5420, 0xEBF8, 0x4143, 0x89, 0xAF, 0x6B, 0xF2, 0x5F, 0x67, 0x2D, 0xEF);

        //// MFSampleExtension_CaptureMetadata
        //public static readonly Guid MFSampleExtension_CaptureMetadata = new Guid(0x2EBE23A8, 0xFAF5, 0x444A, 0xA6, 0xA2, 0xEB, 0x81, 0x08, 0x80, 0xAB, 0x5D);

        //public static readonly Guid MF_CAPTURE_METADATA_PHOTO_FRAME_FLASH = new Guid(0x0F9DD6C6, 0x6003, 0x45D8, 0xBD, 0x59, 0xF1, 0xF5, 0x3E, 0x3D, 0x04, 0xE8);

        //// MFSampleExtension_Discontinuity {9cdf01d9-a0f0-43ba-b077-eaa06cbd728a}
        //public static readonly Guid MFSampleExtension_Discontinuity = new Guid(0x9cdf01d9, 0xa0f0, 0x43ba, 0xb0, 0x77, 0xea, 0xa0, 0x6c, 0xbd, 0x72, 0x8a);

        //// {b1d5830a-deb8-40e3-90fa-389943716461}   MFSampleExtension_Interlaced
        //public static readonly Guid MFSampleExtension_Interlaced = new Guid(0xb1d5830a, 0xdeb8, 0x40e3, 0x90, 0xfa, 0x38, 0x99, 0x43, 0x71, 0x64, 0x61);

        //// {304d257c-7493-4fbd-b149-9228de8d9a99}   MFSampleExtension_RepeatFirstField
        //public static readonly Guid MFSampleExtension_RepeatFirstField = new Guid(0x304d257c, 0x7493, 0x4fbd, 0xb1, 0x49, 0x92, 0x28, 0xde, 0x8d, 0x9a, 0x99);

        //// {9d85f816-658b-455a-bde0-9fa7e15ab8f9}   MFSampleExtension_SingleField
        //public static readonly Guid MFSampleExtension_SingleField = new Guid(0x9d85f816, 0x658b, 0x455a, 0xbd, 0xe0, 0x9f, 0xa7, 0xe1, 0x5a, 0xb8, 0xf9);

        //// MFSampleExtension_Token {8294da66-f328-4805-b551-00deb4c57a61}
        //public static readonly Guid MFSampleExtension_Token = new Guid(0x8294da66, 0xf328, 0x4805, 0xb5, 0x51, 0x00, 0xde, 0xb4, 0xc5, 0x7a, 0x61);

        //// MFSampleExtension_3DVideo                    {F86F97A4-DD54-4e2e-9A5E-55FC2D74A005}
        //public static readonly Guid MFSampleExtension_3DVideo = new Guid(0xf86f97a4, 0xdd54, 0x4e2e, 0x9a, 0x5e, 0x55, 0xfc, 0x2d, 0x74, 0xa0, 0x05);

        //// MFSampleExtension_3DVideo_SampleFormat       {08671772-E36F-4cff-97B3-D72E20987A48}
        //public static readonly Guid MFSampleExtension_3DVideo_SampleFormat = new Guid(0x8671772, 0xe36f, 0x4cff, 0x97, 0xb3, 0xd7, 0x2e, 0x20, 0x98, 0x7a, 0x48);

        //public static readonly Guid MFSampleExtension_MaxDecodeFrameSize = new Guid(0xd3cc654f, 0xf9f3, 0x4a13, 0x88, 0x9f, 0xf0, 0x4e, 0xb2, 0xb5, 0xb9, 0x57);
        //public static readonly Guid MFSampleExtension_AccumulatedNonRefPicPercent = new Guid(0x79ea74df, 0xa740, 0x445b, 0xbc, 0x98, 0xc9, 0xed, 0x1f, 0x26, 0xe, 0xee);
        //public static readonly Guid MFSampleExtension_Encryption_SubSample_Mapping = new Guid(0x8444F27A, 0x69A1, 0x48DA, 0xBD, 0x08, 0x11, 0xCE, 0xF3, 0x68, 0x30, 0xD2);
        //public static readonly Guid MFSampleExtension_Encryption_ClearSliceHeaderData = new Guid(0x5509a4f4, 0x320d, 0x4e6c, 0x8d, 0x1a, 0x94, 0xc6, 0x6d, 0xd2, 0xc, 0xb0);
        //public static readonly Guid MFSampleExtension_Encryption_HardwareProtection_KeyInfoID = new Guid(0x8cbfcceb, 0x94a5, 0x4de1, 0x82, 0x31, 0xa8, 0x5e, 0x47, 0xcf, 0x81, 0xe7);
        //public static readonly Guid MFSampleExtension_Encryption_HardwareProtection_KeyInfo = new Guid(0xb2372080, 0x455b, 0x4dd7, 0x99, 0x89, 0x1a, 0x95, 0x57, 0x84, 0xb7, 0x54);
        //public static readonly Guid MFSampleExtension_Encryption_HardwareProtection_VideoDecryptorContext = new Guid(0x693470c8, 0xe837, 0x47a0, 0x88, 0xcb, 0x53, 0x5b, 0x90, 0x5e, 0x35, 0x82);
        //public static readonly Guid MFSampleExtension_Encryption_Opaque_Data = new Guid(0x224d77e5, 0x1391, 0x4ffb, 0x9f, 0x41, 0xb4, 0x32, 0xf6, 0x8c, 0x61, 0x1d);
        //public static readonly Guid MFSampleExtension_NALULengthInfo = new Guid(0x19124E7C, 0xAD4B, 0x465F, 0xBB, 0x18, 0x20, 0x18, 0x62, 0x87, 0xB6, 0xAF);
        //public static readonly Guid MFSampleExtension_Encryption_NALUTypes = new Guid(0xb0f067c7, 0x714c, 0x416c, 0x8d, 0x59, 0x5f, 0x4d, 0xdf, 0x89, 0x13, 0xb6);
        //public static readonly Guid MFSampleExtension_Encryption_SPSPPSData = new Guid(0xaede0fa2, 0xe0c, 0x453c, 0xb7, 0xf3, 0xde, 0x86, 0x93, 0x36, 0x4d, 0x11);
        //public static readonly Guid MFSampleExtension_Encryption_SEIData = new Guid(0x3cf0e972, 0x4542, 0x4687, 0x99, 0x99, 0x58, 0x5f, 0x56, 0x5f, 0xba, 0x7d);
        //public static readonly Guid MFSampleExtension_Encryption_HardwareProtection = new Guid(0x9a2b2d2b, 0x8270, 0x43e3, 0x84, 0x48, 0x99, 0x4f, 0x42, 0x6e, 0x88, 0x86);
        //public static readonly Guid MFSampleExtension_ClosedCaption_CEA708 = new Guid(0x26f09068, 0xe744, 0x47dc, 0xaa, 0x03, 0xdb, 0xf2, 0x04, 0x03, 0xbd, 0xe6);
        //public static readonly Guid MFSampleExtension_DirtyRects = new Guid(0x9ba70225, 0xb342, 0x4e97, 0x91, 0x26, 0x0b, 0x56, 0x6a, 0xb7, 0xea, 0x7e);
        //public static readonly Guid MFSampleExtension_MoveRegions = new Guid(0xe2a6c693, 0x3a8b, 0x4b8d, 0x95, 0xd0, 0xf6, 0x02, 0x81, 0xa1, 0x2f, 0xb7);
        //public static readonly Guid MFSampleExtension_HDCP_FrameCounter = new Guid(0x9d389c60, 0xf507, 0x4aa6, 0xa4, 0xa, 0x71, 0x2, 0x7a, 0x2, 0xf3, 0xde);
        //public static readonly Guid MFSampleExtension_MDLCacheCookie = new Guid(0x5F002AF9, 0xD8F9, 0x41A3, 0xB6, 0xC3, 0xA2, 0xAD, 0x43, 0xF6, 0x47, 0xAD);

        //// Sample Grabber Sink Attributes
        //public static readonly Guid MF_SAMPLEGRABBERSINK_SAMPLE_TIME_OFFSET = new Guid(0x62e3d776, 0x8100, 0x4e03, 0xa6, 0xe8, 0xbd, 0x38, 0x57, 0xac, 0x9c, 0x47);

        // Stream descriptor Attributes

        public static readonly Guid MF_SD_LANGUAGE = new Guid(0xaf2180, 0xbdc2, 0x423c, 0xab, 0xca, 0xf5, 0x3, 0x59, 0x3b, 0xc1, 0x21);
        //public static readonly Guid MF_SD_PROTECTED = new Guid(0xaf2181, 0xbdc2, 0x423c, 0xab, 0xca, 0xf5, 0x3, 0x59, 0x3b, 0xc1, 0x21);
        //public static readonly Guid MF_SD_SAMI_LANGUAGE = new Guid(0x36fcb98a, 0x6cd0, 0x44cb, 0xac, 0xb9, 0xa8, 0xf5, 0x60, 0xd, 0xd0, 0xbb);

        // Topology Attributes
        //public static readonly Guid MF_TOPOLOGY_NO_MARKIN_MARKOUT = new Guid(0x7ed3f804, 0x86bb, 0x4b3f, 0xb7, 0xe4, 0x7c, 0xb4, 0x3a, 0xfd, 0x4b, 0x80);
        public static readonly Guid MF_TOPOLOGY_PROJECTSTART = new Guid(0x7ed3f802, 0x86bb, 0x4b3f, 0xb7, 0xe4, 0x7c, 0xb4, 0x3a, 0xfd, 0x4b, 0x80);
        public static readonly Guid MF_TOPOLOGY_PROJECTSTOP = new Guid(0x7ed3f803, 0x86bb, 0x4b3f, 0xb7, 0xe4, 0x7c, 0xb4, 0x3a, 0xfd, 0x4b, 0x80);
        //public static readonly Guid MF_TOPOLOGY_RESOLUTION_STATUS = new Guid(0x494bbcde, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);

        //// Topology Node Attributes
        //public static readonly Guid MF_TOPONODE_CONNECT_METHOD = new Guid(0x494bbcf1, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_D3DAWARE = new Guid(0x494bbced, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_DECODER = new Guid(0x494bbd02, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_DECRYPTOR = new Guid(0x494bbcfa, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_DISABLE_PREROLL = new Guid(0x14932f9e, 0x9087, 0x4bb4, 0x84, 0x12, 0x51, 0x67, 0x14, 0x5c, 0xbe, 0x04);
        //public static readonly Guid MF_TOPONODE_DISCARDABLE = new Guid(0x494bbcfb, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_DRAIN = new Guid(0x494bbce9, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_ERROR_MAJORTYPE = new Guid(0x494bbcfd, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_ERROR_SUBTYPE = new Guid(0x494bbcfe, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_ERRORCODE = new Guid(0x494bbcee, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_FLUSH = new Guid(0x494bbce8, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_LOCKED = new Guid(0x494bbcf7, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_MARKIN_HERE = new Guid(0x494bbd00, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_MARKOUT_HERE = new Guid(0x494bbd01, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_MEDIASTART = new Guid(0x835c58ea, 0xe075, 0x4bc7, 0xbc, 0xba, 0x4d, 0xe0, 0x00, 0xdf, 0x9a, 0xe6);
        //public static readonly Guid MF_TOPONODE_MEDIASTOP = new Guid(0x835c58eb, 0xe075, 0x4bc7, 0xbc, 0xba, 0x4d, 0xe0, 0x00, 0xdf, 0x9a, 0xe6);
        //public static readonly Guid MF_TOPONODE_NOSHUTDOWN_ON_REMOVE = new Guid(0x14932f9c, 0x9087, 0x4bb4, 0x84, 0x12, 0x51, 0x67, 0x14, 0x5c, 0xbe, 0x04);
        public static readonly Guid MF_TOPONODE_PRESENTATION_DESCRIPTOR = new Guid(0x835c58ed, 0xe075, 0x4bc7, 0xbc, 0xba, 0x4d, 0xe0, 0x00, 0xdf, 0x9a, 0xe6);
        //public static readonly Guid MF_TOPONODE_PRIMARYOUTPUT = new Guid(0x6304ef99, 0x16b2, 0x4ebe, 0x9d, 0x67, 0xe4, 0xc5, 0x39, 0xb3, 0xa2, 0x59);
        //public static readonly Guid MF_TOPONODE_RATELESS = new Guid(0x14932f9d, 0x9087, 0x4bb4, 0x84, 0x12, 0x51, 0x67, 0x14, 0x5c, 0xbe, 0x04);
        //public static readonly Guid MF_TOPONODE_SEQUENCE_ELEMENTID = new Guid(0x835c58ef, 0xe075, 0x4bc7, 0xbc, 0xba, 0x4d, 0xe0, 0x00, 0xdf, 0x9a, 0xe6);
        public static readonly Guid MF_TOPONODE_SOURCE = new Guid(0x835c58ec, 0xe075, 0x4bc7, 0xbc, 0xba, 0x4d, 0xe0, 0x00, 0xdf, 0x9a, 0xe6);
        public static readonly Guid MF_TOPONODE_STREAM_DESCRIPTOR = new Guid(0x835c58ee, 0xe075, 0x4bc7, 0xbc, 0xba, 0x4d, 0xe0, 0x00, 0xdf, 0x9a, 0xe6);
        //public static readonly Guid MF_TOPONODE_STREAMID = new Guid(0x14932f9b, 0x9087, 0x4bb4, 0x84, 0x12, 0x51, 0x67, 0x14, 0x5c, 0xbe, 0x04);
        //public static readonly Guid MF_TOPONODE_TRANSFORM_OBJECTID = new Guid(0x88dcc0c9, 0x293e, 0x4e8b, 0x9a, 0xeb, 0xa, 0xd6, 0x4c, 0xc0, 0x16, 0xb0);
        //public static readonly Guid MF_TOPONODE_WORKQUEUE_ID = new Guid(0x494bbcf8, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_WORKQUEUE_MMCSS_CLASS = new Guid(0x494bbcf9, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
        //public static readonly Guid MF_TOPONODE_WORKQUEUE_MMCSS_TASKID = new Guid(0x494bbcff, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);

        //// Transform Attributes
        //public static readonly Guid MF_ACTIVATE_MFT_LOCKED = new Guid(0xc1f6093c, 0x7f65, 0x4fbd, 0x9e, 0x39, 0x5f, 0xae, 0xc3, 0xc4, 0xfb, 0xd7);
        //public static readonly Guid MF_SA_D3D_AWARE = new Guid(0xeaa35c29, 0x775e, 0x488e, 0x9b, 0x61, 0xb3, 0x28, 0x3e, 0x49, 0x58, 0x3b);
        //public static readonly Guid MFT_SUPPORT_3DVIDEO = new Guid(0x93f81b1, 0x4f2e, 0x4631, 0x81, 0x68, 0x79, 0x34, 0x3, 0x2a, 0x1, 0xd3);

        //// {53476A11-3F13-49fb-AC42-EE2733C96741} MFT_SUPPORT_DYNAMIC_FORMAT_CHANGE {UINT32 (BOOL)}
        //public static readonly Guid MFT_SUPPORT_DYNAMIC_FORMAT_CHANGE = new Guid(0x53476a11, 0x3f13, 0x49fb, 0xac, 0x42, 0xee, 0x27, 0x33, 0xc9, 0x67, 0x41);

        //public static readonly Guid MFT_REMUX_MARK_I_PICTURE_AS_CLEAN_POINT = new Guid(0x364e8f85, 0x3f2e, 0x436c, 0xb2, 0xa2, 0x44, 0x40, 0xa0, 0x12, 0xa9, 0xe8);
        //public static readonly Guid MFT_ENCODER_SUPPORTS_CONFIG_EVENT = new Guid(0x86a355ae, 0x3a77, 0x4ec4, 0x9f, 0x31, 0x1, 0x14, 0x9a, 0x4e, 0x92, 0xde);

        //// Presentation Descriptor Attributes

        //public static readonly Guid MF_PD_APP_CONTEXT = new Guid(0x6c990d32, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
        public static readonly Guid MF_PD_DURATION = new Guid(0x6c990d33, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
        //public static readonly Guid MF_PD_LAST_MODIFIED_TIME = new Guid(0x6c990d38, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
        //public static readonly Guid MF_PD_MIME_TYPE = new Guid(0x6c990d37, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
        //public static readonly Guid MF_PD_PMPHOST_CONTEXT = new Guid(0x6c990d31, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
        //public static readonly Guid MF_PD_SAMI_STYLELIST = new Guid(0xe0b73c7f, 0x486d, 0x484e, 0x98, 0x72, 0x4d, 0xe5, 0x19, 0x2a, 0x7b, 0xf8);
        //public static readonly Guid MF_PD_TOTAL_FILE_SIZE = new Guid(0x6c990d34, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
        //public static readonly Guid MF_PD_AUDIO_ENCODING_BITRATE = new Guid(0x6c990d35, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
        //public static readonly Guid MF_PD_VIDEO_ENCODING_BITRATE = new Guid(0x6c990d36, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);

        //// wmcontainer.h Attributes
        //public static readonly Guid MFASFSampleExtension_SampleDuration = new Guid(0xc6bd9450, 0x867f, 0x4907, 0x83, 0xa3, 0xc7, 0x79, 0x21, 0xb7, 0x33, 0xad);
        //public static readonly Guid MFSampleExtension_SampleKeyID = new Guid(0x9ed713c8, 0x9b87, 0x4b26, 0x82, 0x97, 0xa9, 0x3b, 0x0c, 0x5a, 0x8a, 0xcc);

        //public static readonly Guid MF_PD_ASF_FILEPROPERTIES_FILE_ID = new Guid(0x3de649b4, 0xd76d, 0x4e66, 0x9e, 0xc9, 0x78, 0x12, 0xf, 0xb4, 0xc7, 0xe3);
        //public static readonly Guid MF_PD_ASF_FILEPROPERTIES_CREATION_TIME = new Guid(0x3de649b6, 0xd76d, 0x4e66, 0x9e, 0xc9, 0x78, 0x12, 0xf, 0xb4, 0xc7, 0xe3);
        //public static readonly Guid MF_PD_ASF_FILEPROPERTIES_PACKETS = new Guid(0x3de649b7, 0xd76d, 0x4e66, 0x9e, 0xc9, 0x78, 0x12, 0xf, 0xb4, 0xc7, 0xe3);
        //public static readonly Guid MF_PD_ASF_FILEPROPERTIES_PLAY_DURATION = new Guid(0x3de649b8, 0xd76d, 0x4e66, 0x9e, 0xc9, 0x78, 0x12, 0xf, 0xb4, 0xc7, 0xe3);
        //public static readonly Guid MF_PD_ASF_FILEPROPERTIES_SEND_DURATION = new Guid(0x3de649b9, 0xd76d, 0x4e66, 0x9e, 0xc9, 0x78, 0x12, 0xf, 0xb4, 0xc7, 0xe3);
        //public static readonly Guid MF_PD_ASF_FILEPROPERTIES_PREROLL = new Guid(0x3de649ba, 0xd76d, 0x4e66, 0x9e, 0xc9, 0x78, 0x12, 0xf, 0xb4, 0xc7, 0xe3);
        //public static readonly Guid MF_PD_ASF_FILEPROPERTIES_FLAGS = new Guid(0x3de649bb, 0xd76d, 0x4e66, 0x9e, 0xc9, 0x78, 0x12, 0xf, 0xb4, 0xc7, 0xe3);
        //public static readonly Guid MF_PD_ASF_FILEPROPERTIES_MIN_PACKET_SIZE = new Guid(0x3de649bc, 0xd76d, 0x4e66, 0x9e, 0xc9, 0x78, 0x12, 0xf, 0xb4, 0xc7, 0xe3);
        //public static readonly Guid MF_PD_ASF_FILEPROPERTIES_MAX_PACKET_SIZE = new Guid(0x3de649bd, 0xd76d, 0x4e66, 0x9e, 0xc9, 0x78, 0x12, 0xf, 0xb4, 0xc7, 0xe3);
        //public static readonly Guid MF_PD_ASF_FILEPROPERTIES_MAX_BITRATE = new Guid(0x3de649be, 0xd76d, 0x4e66, 0x9e, 0xc9, 0x78, 0x12, 0xf, 0xb4, 0xc7, 0xe3);
        //public static readonly Guid MF_PD_ASF_CONTENTENCRYPTION_TYPE = new Guid(0x8520fe3d, 0x277e, 0x46ea, 0x99, 0xe4, 0xe3, 0xa, 0x86, 0xdb, 0x12, 0xbe);
        //public static readonly Guid MF_PD_ASF_CONTENTENCRYPTION_KEYID = new Guid(0x8520fe3e, 0x277e, 0x46ea, 0x99, 0xe4, 0xe3, 0xa, 0x86, 0xdb, 0x12, 0xbe);
        //public static readonly Guid MF_PD_ASF_CONTENTENCRYPTION_SECRET_DATA = new Guid(0x8520fe3f, 0x277e, 0x46ea, 0x99, 0xe4, 0xe3, 0xa, 0x86, 0xdb, 0x12, 0xbe);
        //public static readonly Guid MF_PD_ASF_CONTENTENCRYPTION_LICENSE_URL = new Guid(0x8520fe40, 0x277e, 0x46ea, 0x99, 0xe4, 0xe3, 0xa, 0x86, 0xdb, 0x12, 0xbe);
        //public static readonly Guid MF_PD_ASF_CONTENTENCRYPTIONEX_ENCRYPTION_DATA = new Guid(0x62508be5, 0xecdf, 0x4924, 0xa3, 0x59, 0x72, 0xba, 0xb3, 0x39, 0x7b, 0x9d);
        //public static readonly Guid MF_PD_ASF_LANGLIST = new Guid(0xf23de43c, 0x9977, 0x460d, 0xa6, 0xec, 0x32, 0x93, 0x7f, 0x16, 0xf, 0x7d);
        //public static readonly Guid MF_PD_ASF_LANGLIST_LEGACYORDER = new Guid(0xf23de43d, 0x9977, 0x460d, 0xa6, 0xec, 0x32, 0x93, 0x7f, 0x16, 0xf, 0x7d);
        //public static readonly Guid MF_PD_ASF_MARKER = new Guid(0x5134330e, 0x83a6, 0x475e, 0xa9, 0xd5, 0x4f, 0xb8, 0x75, 0xfb, 0x2e, 0x31);
        //public static readonly Guid MF_PD_ASF_SCRIPT = new Guid(0xe29cd0d7, 0xd602, 0x4923, 0xa7, 0xfe, 0x73, 0xfd, 0x97, 0xec, 0xc6, 0x50);
        //public static readonly Guid MF_PD_ASF_CODECLIST = new Guid(0xe4bb3509, 0xc18d, 0x4df1, 0xbb, 0x99, 0x7a, 0x36, 0xb3, 0xcc, 0x41, 0x19);
        //public static readonly Guid MF_PD_ASF_METADATA_IS_VBR = new Guid(0x5fc6947a, 0xef60, 0x445d, 0xb4, 0x49, 0x44, 0x2e, 0xcc, 0x78, 0xb4, 0xc1);
        //public static readonly Guid MF_PD_ASF_METADATA_V8_VBRPEAK = new Guid(0x5fc6947b, 0xef60, 0x445d, 0xb4, 0x49, 0x44, 0x2e, 0xcc, 0x78, 0xb4, 0xc1);
        //public static readonly Guid MF_PD_ASF_METADATA_V8_BUFFERAVERAGE = new Guid(0x5fc6947c, 0xef60, 0x445d, 0xb4, 0x49, 0x44, 0x2e, 0xcc, 0x78, 0xb4, 0xc1);
        //public static readonly Guid MF_PD_ASF_METADATA_LEAKY_BUCKET_PAIRS = new Guid(0x5fc6947d, 0xef60, 0x445d, 0xb4, 0x49, 0x44, 0x2e, 0xcc, 0x78, 0xb4, 0xc1);
        //public static readonly Guid MF_PD_ASF_DATA_START_OFFSET = new Guid(0xe7d5b3e7, 0x1f29, 0x45d3, 0x88, 0x22, 0x3e, 0x78, 0xfa, 0xe2, 0x72, 0xed);
        //public static readonly Guid MF_PD_ASF_DATA_LENGTH = new Guid(0xe7d5b3e8, 0x1f29, 0x45d3, 0x88, 0x22, 0x3e, 0x78, 0xfa, 0xe2, 0x72, 0xed);
        //public static readonly Guid MF_SD_ASF_EXTSTRMPROP_LANGUAGE_ID_INDEX = new Guid(0x48f8a522, 0x305d, 0x422d, 0x85, 0x24, 0x25, 0x2, 0xdd, 0xa3, 0x36, 0x80);
        //public static readonly Guid MF_SD_ASF_EXTSTRMPROP_AVG_DATA_BITRATE = new Guid(0x48f8a523, 0x305d, 0x422d, 0x85, 0x24, 0x25, 0x2, 0xdd, 0xa3, 0x36, 0x80);
        //public static readonly Guid MF_SD_ASF_EXTSTRMPROP_AVG_BUFFERSIZE = new Guid(0x48f8a524, 0x305d, 0x422d, 0x85, 0x24, 0x25, 0x2, 0xdd, 0xa3, 0x36, 0x80);
        //public static readonly Guid MF_SD_ASF_EXTSTRMPROP_MAX_DATA_BITRATE = new Guid(0x48f8a525, 0x305d, 0x422d, 0x85, 0x24, 0x25, 0x2, 0xdd, 0xa3, 0x36, 0x80);
        //public static readonly Guid MF_SD_ASF_EXTSTRMPROP_MAX_BUFFERSIZE = new Guid(0x48f8a526, 0x305d, 0x422d, 0x85, 0x24, 0x25, 0x2, 0xdd, 0xa3, 0x36, 0x80);
        //public static readonly Guid MF_SD_ASF_STREAMBITRATES_BITRATE = new Guid(0xa8e182ed, 0xafc8, 0x43d0, 0xb0, 0xd1, 0xf6, 0x5b, 0xad, 0x9d, 0xa5, 0x58);
        //public static readonly Guid MF_SD_ASF_METADATA_DEVICE_CONFORMANCE_TEMPLATE = new Guid(0x245e929d, 0xc44e, 0x4f7e, 0xbb, 0x3c, 0x77, 0xd4, 0xdf, 0xd2, 0x7f, 0x8a);
        //public static readonly Guid MF_PD_ASF_INFO_HAS_AUDIO = new Guid(0x80e62295, 0x2296, 0x4a44, 0xb3, 0x1c, 0xd1, 0x3, 0xc6, 0xfe, 0xd2, 0x3c);
        //public static readonly Guid MF_PD_ASF_INFO_HAS_VIDEO = new Guid(0x80e62296, 0x2296, 0x4a44, 0xb3, 0x1c, 0xd1, 0x3, 0xc6, 0xfe, 0xd2, 0x3c);
        //public static readonly Guid MF_PD_ASF_INFO_HAS_NON_AUDIO_VIDEO = new Guid(0x80e62297, 0x2296, 0x4a44, 0xb3, 0x1c, 0xd1, 0x3, 0xc6, 0xfe, 0xd2, 0x3c);
        //public static readonly Guid MF_ASFSTREAMCONFIG_LEAKYBUCKET1 = new Guid(0xc69b5901, 0xea1a, 0x4c9b, 0xb6, 0x92, 0xe2, 0xa0, 0xd2, 0x9a, 0x8a, 0xdd);
        //public static readonly Guid MF_ASFSTREAMCONFIG_LEAKYBUCKET2 = new Guid(0xc69b5902, 0xea1a, 0x4c9b, 0xb6, 0x92, 0xe2, 0xa0, 0xd2, 0x9a, 0x8a, 0xdd);

        //// Arbitrary

        //// {9E6BD6F5-0109-4f95-84AC-9309153A19FC}   MF_MT_ARBITRARY_HEADER          {MT_ARBITRARY_HEADER}
        //public static readonly Guid MF_MT_ARBITRARY_HEADER = new Guid(0x9e6bd6f5, 0x109, 0x4f95, 0x84, 0xac, 0x93, 0x9, 0x15, 0x3a, 0x19, 0xfc);

        //// {5A75B249-0D7D-49a1-A1C3-E0D87F0CADE5}   MF_MT_ARBITRARY_FORMAT          {Blob}
        //public static readonly Guid MF_MT_ARBITRARY_FORMAT = new Guid(0x5a75b249, 0xd7d, 0x49a1, 0xa1, 0xc3, 0xe0, 0xd8, 0x7f, 0xc, 0xad, 0xe5);

        //// Image

        //// {ED062CF4-E34E-4922-BE99-934032133D7C}   MF_MT_IMAGE_LOSS_TOLERANT       {UINT32 (BOOL)}
        //public static readonly Guid MF_MT_IMAGE_LOSS_TOLERANT = new Guid(0xed062cf4, 0xe34e, 0x4922, 0xbe, 0x99, 0x93, 0x40, 0x32, 0x13, 0x3d, 0x7c);

        //// MPEG-4 Media Type Attributes

        //// {261E9D83-9529-4B8F-A111-8B9C950A81A9}   MF_MT_MPEG4_SAMPLE_DESCRIPTION   {BLOB}
        //public static readonly Guid MF_MT_MPEG4_SAMPLE_DESCRIPTION = new Guid(0x261e9d83, 0x9529, 0x4b8f, 0xa1, 0x11, 0x8b, 0x9c, 0x95, 0x0a, 0x81, 0xa9);

        //// {9aa7e155-b64a-4c1d-a500-455d600b6560}   MF_MT_MPEG4_CURRENT_SAMPLE_ENTRY {UINT32}
        //public static readonly Guid MF_MT_MPEG4_CURRENT_SAMPLE_ENTRY = new Guid(0x9aa7e155, 0xb64a, 0x4c1d, 0xa5, 0x00, 0x45, 0x5d, 0x60, 0x0b, 0x65, 0x60);

        //// Save original format information for AVI and WAV files

        //// {d7be3fe0-2bc7-492d-b843-61a1919b70c3}   MF_MT_ORIGINAL_4CC               (UINT32)
        //public static readonly Guid MF_MT_ORIGINAL_4CC = new Guid(0xd7be3fe0, 0x2bc7, 0x492d, 0xb8, 0x43, 0x61, 0xa1, 0x91, 0x9b, 0x70, 0xc3);

        //// {8cbbc843-9fd9-49c2-882f-a72586c408ad}   MF_MT_ORIGINAL_WAVE_FORMAT_TAG   (UINT32)
        //public static readonly Guid MF_MT_ORIGINAL_WAVE_FORMAT_TAG = new Guid(0x8cbbc843, 0x9fd9, 0x49c2, 0x88, 0x2f, 0xa7, 0x25, 0x86, 0xc4, 0x08, 0xad);

        //// Video Capture Media Type Attributes

        //// {D2E7558C-DC1F-403f-9A72-D28BB1EB3B5E}   MF_MT_FRAME_RATE_RANGE_MIN      {UINT64 (HI32(Numerator),LO32(Denominator))}
        //public static readonly Guid MF_MT_FRAME_RATE_RANGE_MIN = new Guid(0xd2e7558c, 0xdc1f, 0x403f, 0x9a, 0x72, 0xd2, 0x8b, 0xb1, 0xeb, 0x3b, 0x5e);

        //// {E3371D41-B4CF-4a05-BD4E-20B88BB2C4D6}   MF_MT_FRAME_RATE_RANGE_MAX      {UINT64 (HI32(Numerator),LO32(Denominator))}
        //public static readonly Guid MF_MT_FRAME_RATE_RANGE_MAX = new Guid(0xe3371d41, 0xb4cf, 0x4a05, 0xbd, 0x4e, 0x20, 0xb8, 0x8b, 0xb2, 0xc4, 0xd6);

        public static readonly Guid MF_LOW_LATENCY = new Guid(0x9c27891a, 0xed7a, 0x40e1, 0x88, 0xe8, 0xb2, 0x27, 0x27, 0xa0, 0x24, 0xee);

        //// {E3F2E203-D445-4B8C-9211-AE390D3BA017}  {UINT32} Maximum macroblocks per second that can be handled by MFT
        //public static readonly Guid MF_VIDEO_MAX_MB_PER_SEC = new Guid(0xe3f2e203, 0xd445, 0x4b8c, 0x92, 0x11, 0xae, 0x39, 0xd, 0x3b, 0xa0, 0x17);
        //public static readonly Guid MF_VIDEO_PROCESSOR_ALGORITHM = new Guid(0x4a0a1e1f, 0x272c, 0x4fb6, 0x9e, 0xb1, 0xdb, 0x33, 0xc, 0xbc, 0x97, 0xca);

        public static readonly Guid MF_TOPOLOGY_DXVA_MODE = new Guid(0x1e8d34f6, 0xf5ab, 0x4e23, 0xbb, 0x88, 0x87, 0x4a, 0xa3, 0xa1, 0xa7, 0x4d);
        //public static readonly Guid MF_TOPOLOGY_ENABLE_XVP_FOR_PLAYBACK = new Guid(0x1967731f, 0xcd78, 0x42fc, 0xb0, 0x26, 0x9, 0x92, 0xa5, 0x6e, 0x56, 0x93);

        //public static readonly Guid MF_TOPOLOGY_STATIC_PLAYBACK_OPTIMIZATIONS = new Guid(0xb86cac42, 0x41a6, 0x4b79, 0x89, 0x7a, 0x1a, 0xb0, 0xe5, 0x2b, 0x4a, 0x1b);
        //public static readonly Guid MF_TOPOLOGY_PLAYBACK_MAX_DIMS = new Guid(0x5715cf19, 0x5768, 0x44aa, 0xad, 0x6e, 0x87, 0x21, 0xf1, 0xb0, 0xf9, 0xbb);

        public static readonly Guid MF_TOPOLOGY_HARDWARE_MODE = new Guid(0xd2d362fd, 0x4e4f, 0x4191, 0xa5, 0x79, 0xc6, 0x18, 0xb6, 0x67, 0x6, 0xaf);
        //public static readonly Guid MF_TOPOLOGY_PLAYBACK_FRAMERATE = new Guid(0xc164737a, 0xc2b1, 0x4553, 0x83, 0xbb, 0x5a, 0x52, 0x60, 0x72, 0x44, 0x8f);
        //public static readonly Guid MF_TOPOLOGY_DYNAMIC_CHANGE_NOT_ALLOWED = new Guid(0xd529950b, 0xd484, 0x4527, 0xa9, 0xcd, 0xb1, 0x90, 0x95, 0x32, 0xb5, 0xb0);
        //public static readonly Guid MF_TOPOLOGY_ENUMERATE_SOURCE_TYPES = new Guid(0x6248c36d, 0x5d0b, 0x4f40, 0xa0, 0xbb, 0xb0, 0xb3, 0x05, 0xf7, 0x76, 0x98);
        //public static readonly Guid MF_TOPOLOGY_START_TIME_ON_PRESENTATION_SWITCH = new Guid(0xc8cc113f, 0x7951, 0x4548, 0xaa, 0xd6, 0x9e, 0xd6, 0x20, 0x2e, 0x62, 0xb3);

        //public static readonly Guid MF_PD_PLAYBACK_ELEMENT_ID = new Guid(0x6c990d39, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
        //public static readonly Guid MF_PD_PREFERRED_LANGUAGE = new Guid(0x6c990d3A, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
        //public static readonly Guid MF_PD_PLAYBACK_BOUNDARY_TIME = new Guid(0x6c990d3b, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
        //public static readonly Guid MF_PD_AUDIO_ISVARIABLEBITRATE = new Guid(0x33026ee0, 0xe387, 0x4582, 0xae, 0x0a, 0x34, 0xa2, 0xad, 0x3b, 0xaa, 0x18);

        public static readonly Guid MF_SD_STREAM_NAME = new Guid(0x4f1b099d, 0xd314, 0x41e5, 0xa7, 0x81, 0x7f, 0xef, 0xaa, 0x4c, 0x50, 0x1f);
        //public static readonly Guid MF_SD_MUTUALLY_EXCLUSIVE = new Guid(0x23ef79c, 0x388d, 0x487f, 0xac, 0x17, 0x69, 0x6c, 0xd6, 0xe3, 0xc6, 0xf5);

        //public static readonly Guid MF_SAMPLEGRABBERSINK_IGNORE_CLOCK = new Guid(0x0efda2c0, 0x2b69, 0x4e2e, 0xab, 0x8d, 0x46, 0xdc, 0xbf, 0xf7, 0xd2, 0x5d);
        //public static readonly Guid MF_BYTESTREAMHANDLER_ACCEPTS_SHARE_WRITE = new Guid(0xa6e1f733, 0x3001, 0x4915, 0x81, 0x50, 0x15, 0x58, 0xa2, 0x18, 0xe, 0xc8);

        public static readonly Guid MF_TRANSCODE_CONTAINERTYPE = new Guid(0x150ff23f, 0x4abc, 0x478b, 0xac, 0x4f, 0xe1, 0x91, 0x6f, 0xba, 0x1c, 0xca);
        //public static readonly Guid MF_TRANSCODE_SKIP_METADATA_TRANSFER = new Guid(0x4e4469ef, 0xb571, 0x4959, 0x8f, 0x83, 0x3d, 0xcf, 0xba, 0x33, 0xa3, 0x93);
        //public static readonly Guid MF_TRANSCODE_TOPOLOGYMODE = new Guid(0x3e3df610, 0x394a, 0x40b2, 0x9d, 0xea, 0x3b, 0xab, 0x65, 0xb, 0xeb, 0xf2);
        //public static readonly Guid MF_TRANSCODE_ADJUST_PROFILE = new Guid(0x9c37c21b, 0x60f, 0x487c, 0xa6, 0x90, 0x80, 0xd7, 0xf5, 0xd, 0x1c, 0x72);

        //public static readonly Guid MF_TRANSCODE_ENCODINGPROFILE = new Guid(0x6947787c, 0xf508, 0x4ea9, 0xb1, 0xe9, 0xa1, 0xfe, 0x3a, 0x49, 0xfb, 0xc9);
        //public static readonly Guid MF_TRANSCODE_QUALITYVSSPEED = new Guid(0x98332df8, 0x03cd, 0x476b, 0x89, 0xfa, 0x3f, 0x9e, 0x44, 0x2d, 0xec, 0x9f);
        //public static readonly Guid MF_TRANSCODE_DONOT_INSERT_ENCODER = new Guid(0xf45aa7ce, 0xab24, 0x4012, 0xa1, 0x1b, 0xdc, 0x82, 0x20, 0x20, 0x14, 0x10);

        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE = new Guid(0xc60ac5fe, 0x252a, 0x478f, 0xa0, 0xef, 0xbc, 0x8f, 0xa5, 0xf7, 0xca, 0xd3);
        //public static readonly Guid VideoInputDeviceCategory = new Guid(0x860BB310, 0x5D01, 0x11D0, 0xBD, 0x3B, 0x00, 0xA0, 0xC9, 0x11, 0xCE, 0x86);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID = new Guid(0x8ac3587a, 0x4ae7, 0x42d8, 0x99, 0xe0, 0x0a, 0x60, 0x13, 0xee, 0xf9, 0x0f);
        //public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_HW_SOURCE = new Guid(0xde7046ba, 0x54d6, 0x4487, 0xa2, 0xa4, 0xec, 0x7c, 0xd, 0x1b, 0xd1, 0x63);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME = new Guid(0x60d0e559, 0x52f8, 0x4fa2, 0xbb, 0xce, 0xac, 0xdb, 0x34, 0xa8, 0xec, 0x1);
        //public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_MEDIA_TYPE = new Guid(0x56a819ca, 0xc78, 0x4de4, 0xa0, 0xa7, 0x3d, 0xda, 0xba, 0xf, 0x24, 0xd4);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_CATEGORY = new Guid(0x77f0ae69, 0xc3bd, 0x4509, 0x94, 0x1d, 0x46, 0x7e, 0x4d, 0x24, 0x89, 0x9e);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK = new Guid(0x58f0aad8, 0x22bf, 0x4f8a, 0xbb, 0x3d, 0xd2, 0xc4, 0x97, 0x8c, 0x6e, 0x2f);
        //public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_MAX_BUFFERS = new Guid(0x7dd9b730, 0x4f2d, 0x41d5, 0x8f, 0x95, 0xc, 0xc9, 0xa9, 0x12, 0xba, 0x26);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_GUID = new Guid(0x14dd9a1c, 0x7cff, 0x41be, 0xb1, 0xb9, 0xba, 0x1a, 0xc6, 0xec, 0xb5, 0x71);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ENDPOINT_ID = new Guid(0x30da9258, 0xfeb9, 0x47a7, 0xa4, 0x53, 0x76, 0x3a, 0x7a, 0x8e, 0x1c, 0x5f);
        //public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ROLE = new Guid(0xbc9d118e, 0x8c67, 0x4a18, 0x85, 0xd4, 0x12, 0xd3, 0x0, 0x40, 0x5, 0x52);

        //public static readonly Guid MFSampleExtension_DeviceTimestamp = new Guid(0x8f3e35e7, 0x2dcd, 0x4887, 0x86, 0x22, 0x2a, 0x58, 0xba, 0xa6, 0x52, 0xb0);

        //public static readonly Guid MF_TRANSFORM_ASYNC = new Guid(0xf81a699a, 0x649a, 0x497d, 0x8c, 0x73, 0x29, 0xf8, 0xfe, 0xd6, 0xad, 0x7a);
        //public static readonly Guid MF_TRANSFORM_ASYNC_UNLOCK = new Guid(0xe5666d6b, 0x3422, 0x4eb6, 0xa4, 0x21, 0xda, 0x7d, 0xb1, 0xf8, 0xe2, 0x7);
        //public static readonly Guid MF_TRANSFORM_FLAGS_Attribute = new Guid(0x9359bb7e, 0x6275, 0x46c4, 0xa0, 0x25, 0x1c, 0x1, 0xe4, 0x5f, 0x1a, 0x86);
        //public static readonly Guid MF_TRANSFORM_CATEGORY_Attribute = new Guid(0xceabba49, 0x506d, 0x4757, 0xa6, 0xff, 0x66, 0xc1, 0x84, 0x98, 0x7e, 0x4e);
        //public static readonly Guid MFT_TRANSFORM_CLSID_Attribute = new Guid(0x6821c42b, 0x65a4, 0x4e82, 0x99, 0xbc, 0x9a, 0x88, 0x20, 0x5e, 0xcd, 0xc);
        //public static readonly Guid MFT_INPUT_TYPES_Attributes = new Guid(0x4276c9b1, 0x759d, 0x4bf3, 0x9c, 0xd0, 0xd, 0x72, 0x3d, 0x13, 0x8f, 0x96);
        //public static readonly Guid MFT_OUTPUT_TYPES_Attributes = new Guid(0x8eae8cf3, 0xa44f, 0x4306, 0xba, 0x5c, 0xbf, 0x5d, 0xda, 0x24, 0x28, 0x18);
        //public static readonly Guid MFT_ENUM_HARDWARE_URL_Attribute = new Guid(0x2fb866ac, 0xb078, 0x4942, 0xab, 0x6c, 0x0, 0x3d, 0x5, 0xcd, 0xa6, 0x74);
        //public static readonly Guid MFT_FRIENDLY_NAME_Attribute = new Guid(0x314ffbae, 0x5b41, 0x4c95, 0x9c, 0x19, 0x4e, 0x7d, 0x58, 0x6f, 0xac, 0xe3);
        //public static readonly Guid MFT_CONNECTED_STREAM_ATTRIBUTE = new Guid(0x71eeb820, 0xa59f, 0x4de2, 0xbc, 0xec, 0x38, 0xdb, 0x1d, 0xd6, 0x11, 0xa4);
        //public static readonly Guid MFT_CONNECTED_TO_HW_STREAM = new Guid(0x34e6e728, 0x6d6, 0x4491, 0xa5, 0x53, 0x47, 0x95, 0x65, 0xd, 0xb9, 0x12);
        //public static readonly Guid MFT_PREFERRED_OUTPUTTYPE_Attribute = new Guid(0x7e700499, 0x396a, 0x49ee, 0xb1, 0xb4, 0xf6, 0x28, 0x2, 0x1e, 0x8c, 0x9d);
        //public static readonly Guid MFT_PROCESS_LOCAL_Attribute = new Guid(0x543186e4, 0x4649, 0x4e65, 0xb5, 0x88, 0x4a, 0xa3, 0x52, 0xaf, 0xf3, 0x79);
        //public static readonly Guid MFT_PREFERRED_ENCODER_PROFILE = new Guid(0x53004909, 0x1ef5, 0x46d7, 0xa1, 0x8e, 0x5a, 0x75, 0xf8, 0xb5, 0x90, 0x5f);
        //public static readonly Guid MFT_HW_TIMESTAMP_WITH_QPC_Attribute = new Guid(0x8d030fb8, 0xcc43, 0x4258, 0xa2, 0x2e, 0x92, 0x10, 0xbe, 0xf8, 0x9b, 0xe4);
        //public static readonly Guid MFT_FIELDOFUSE_UNLOCK_Attribute = new Guid(0x8ec2e9fd, 0x9148, 0x410d, 0x83, 0x1e, 0x70, 0x24, 0x39, 0x46, 0x1a, 0x8e);
        //public static readonly Guid MFT_CODEC_MERIT_Attribute = new Guid(0x88a7cb15, 0x7b07, 0x4a34, 0x91, 0x28, 0xe6, 0x4c, 0x67, 0x3, 0xc4, 0xd3);
        //public static readonly Guid MFT_ENUM_TRANSCODE_ONLY_ATTRIBUTE = new Guid(0x111ea8cd, 0xb62a, 0x4bdb, 0x89, 0xf6, 0x67, 0xff, 0xcd, 0xc2, 0x45, 0x8b);

        //public static readonly Guid MF_MP2DLNA_USE_MMCSS = new Guid(0x54f3e2ee, 0xa2a2, 0x497d, 0x98, 0x34, 0x97, 0x3a, 0xfd, 0xe5, 0x21, 0xeb);
        //public static readonly Guid MF_MP2DLNA_VIDEO_BIT_RATE = new Guid(0xe88548de, 0x73b4, 0x42d7, 0x9c, 0x75, 0xad, 0xfa, 0xa, 0x2a, 0x6e, 0x4c);
        //public static readonly Guid MF_MP2DLNA_AUDIO_BIT_RATE = new Guid(0x2d1c070e, 0x2b5f, 0x4ab3, 0xa7, 0xe6, 0x8d, 0x94, 0x3b, 0xa8, 0xd0, 0x0a);
        //public static readonly Guid MF_MP2DLNA_ENCODE_QUALITY = new Guid(0xb52379d7, 0x1d46, 0x4fb6, 0xa3, 0x17, 0xa4, 0xa5, 0xf6, 0x09, 0x59, 0xf8);
        //public static readonly Guid MF_MP2DLNA_STATISTICS = new Guid(0x75e488a3, 0xd5ad, 0x4898, 0x85, 0xe0, 0xbc, 0xce, 0x24, 0xa7, 0x22, 0xd7);

        //public static readonly Guid MF_SINK_WRITER_ASYNC_CALLBACK = new Guid(0x48cb183e, 0x7b0b, 0x46f4, 0x82, 0x2e, 0x5e, 0x1d, 0x2d, 0xda, 0x43, 0x54);
        //public static readonly Guid MF_SINK_WRITER_DISABLE_THROTTLING = new Guid(0x08b845d8, 0x2b74, 0x4afe, 0x9d, 0x53, 0xbe, 0x16, 0xd2, 0xd5, 0xae, 0x4f);
        //public static readonly Guid MF_SINK_WRITER_D3D_MANAGER = new Guid(0xec822da2, 0xe1e9, 0x4b29, 0xa0, 0xd8, 0x56, 0x3c, 0x71, 0x9f, 0x52, 0x69);
        //public static readonly Guid MF_SINK_WRITER_ENCODER_CONFIG = new Guid(0xad91cd04, 0xa7cc, 0x4ac7, 0x99, 0xb6, 0xa5, 0x7b, 0x9a, 0x4a, 0x7c, 0x70);
        //public static readonly Guid MF_READWRITE_DISABLE_CONVERTERS = new Guid(0x98d5b065, 0x1374, 0x4847, 0x8d, 0x5d, 0x31, 0x52, 0x0f, 0xee, 0x71, 0x56);
        public static readonly Guid MF_READWRITE_ENABLE_HARDWARE_TRANSFORMS = new Guid(0xa634a91c, 0x822b, 0x41b9, 0xa4, 0x94, 0x4d, 0xe4, 0x64, 0x36, 0x12, 0xb0);
        //public static readonly Guid MF_READWRITE_MMCSS_CLASS = new Guid(0x39384300, 0xd0eb, 0x40b1, 0x87, 0xa0, 0x33, 0x18, 0x87, 0x1b, 0x5a, 0x53);
        //public static readonly Guid MF_READWRITE_MMCSS_PRIORITY = new Guid(0x43ad19ce, 0xf33f, 0x4ba9, 0xa5, 0x80, 0xe4, 0xcd, 0x12, 0xf2, 0xd1, 0x44);
        //public static readonly Guid MF_READWRITE_MMCSS_CLASS_AUDIO = new Guid(0x430847da, 0x0890, 0x4b0e, 0x93, 0x8c, 0x05, 0x43, 0x32, 0xc5, 0x47, 0xe1);
        //public static readonly Guid MF_READWRITE_MMCSS_PRIORITY_AUDIO = new Guid(0x273db885, 0x2de2, 0x4db2, 0xa6, 0xa7, 0xfd, 0xb6, 0x6f, 0xb4, 0x0b, 0x61);
        //public static readonly Guid MF_READWRITE_D3D_OPTIONAL = new Guid(0x216479d9, 0x3071, 0x42ca, 0xbb, 0x6c, 0x4c, 0x22, 0x10, 0x2e, 0x1d, 0x18);

        public static readonly Guid MF_SOURCE_READER_ASYNC_CALLBACK = new Guid(0x1e3dbeac, 0xbb43, 0x4c35, 0xb5, 0x07, 0xcd, 0x64, 0x44, 0x64, 0xc9, 0x65);
        //public static readonly Guid MF_SOURCE_READER_D3D_MANAGER = new Guid(0xec822da2, 0xe1e9, 0x4b29, 0xa0, 0xd8, 0x56, 0x3c, 0x71, 0x9f, 0x52, 0x69);
        //public static readonly Guid MF_SOURCE_READER_DISABLE_DXVA = new Guid(0xaa456cfd, 0x3943, 0x4a1e, 0xa7, 0x7d, 0x18, 0x38, 0xc0, 0xea, 0x2e, 0x35);
        //public static readonly Guid MF_SOURCE_READER_MEDIASOURCE_CONFIG = new Guid(0x9085abeb, 0x0354, 0x48f9, 0xab, 0xb5, 0x20, 0x0d, 0xf8, 0x38, 0xc6, 0x8e);
        //public static readonly Guid MF_SOURCE_READER_MEDIASOURCE_CHARACTERISTICS = new Guid(0x6d23f5c8, 0xc5d7, 0x4a9b, 0x99, 0x71, 0x5d, 0x11, 0xf8, 0xbc, 0xa8, 0x80);
        //public static readonly Guid MF_SOURCE_READER_ENABLE_VIDEO_PROCESSING = new Guid(0xfb394f3d, 0xccf1, 0x42ee, 0xbb, 0xb3, 0xf9, 0xb8, 0x45, 0xd5, 0x68, 0x1d);
        public static readonly Guid MF_SOURCE_READER_DISCONNECT_MEDIASOURCE_ON_SHUTDOWN = new Guid(0x56b67165, 0x219e, 0x456d, 0xa2, 0x2e, 0x2d, 0x30, 0x04, 0xc7, 0xfe, 0x56);
        //public static readonly Guid MF_SOURCE_READER_ENABLE_ADVANCED_VIDEO_PROCESSING = new Guid(0xf81da2c, 0xb537, 0x4672, 0xa8, 0xb2, 0xa6, 0x81, 0xb1, 0x73, 0x7, 0xa3);
        //public static readonly Guid MF_SOURCE_READER_DISABLE_CAMERA_PLUGINS = new Guid(0x9d3365dd, 0x58f, 0x4cfb, 0x9f, 0x97, 0xb3, 0x14, 0xcc, 0x99, 0xc8, 0xad);
        //public static readonly Guid MF_SOURCE_READER_ENABLE_TRANSCODE_ONLY_TRANSFORMS = new Guid(0xdfd4f008, 0xb5fd, 0x4e78, 0xae, 0x44, 0x62, 0xa1, 0xe6, 0x7b, 0xbe, 0x27);


        //// Misc W8 attributes
        //public static readonly Guid MF_ENABLE_3DVIDEO_OUTPUT = new Guid(0xbdad7bca, 0xe5f, 0x4b10, 0xab, 0x16, 0x26, 0xde, 0x38, 0x1b, 0x62, 0x93);
        //public static readonly Guid MF_SA_D3D11_BINDFLAGS = new Guid(0xeacf97ad, 0x065c, 0x4408, 0xbe, 0xe3, 0xfd, 0xcb, 0xfd, 0x12, 0x8b, 0xe2);
        //public static readonly Guid MF_SA_D3D11_USAGE = new Guid(0xe85fe442, 0x2ca3, 0x486e, 0xa9, 0xc7, 0x10, 0x9d, 0xda, 0x60, 0x98, 0x80);
        //public static readonly Guid MF_SA_D3D11_AWARE = new Guid(0x206b4fc8, 0xfcf9, 0x4c51, 0xaf, 0xe3, 0x97, 0x64, 0x36, 0x9e, 0x33, 0xa0);
        //public static readonly Guid MF_SA_D3D11_SHARED = new Guid(0x7b8f32c3, 0x6d96, 0x4b89, 0x92, 0x3, 0xdd, 0x38, 0xb6, 0x14, 0x14, 0xf3);
        //public static readonly Guid MF_SA_D3D11_SHARED_WITHOUT_MUTEX = new Guid(0x39dbd44d, 0x2e44, 0x4931, 0xa4, 0xc8, 0x35, 0x2d, 0x3d, 0xc4, 0x21, 0x15);
        //public static readonly Guid MF_SA_BUFFERS_PER_SAMPLE = new Guid(0x873c5171, 0x1e3d, 0x4e25, 0x98, 0x8d, 0xb4, 0x33, 0xce, 0x04, 0x19, 0x83);
        //public static readonly Guid MFT_DECODER_EXPOSE_OUTPUT_TYPES_IN_NATIVE_ORDER = new Guid(0xef80833f, 0xf8fa, 0x44d9, 0x80, 0xd8, 0x41, 0xed, 0x62, 0x32, 0x67, 0xc);
        //public static readonly Guid MFT_DECODER_FINAL_VIDEO_RESOLUTION_HINT = new Guid(0xdc2f8496, 0x15c4, 0x407a, 0xb6, 0xf0, 0x1b, 0x66, 0xab, 0x5f, 0xbf, 0x53);
        //public static readonly Guid MFT_ENUM_HARDWARE_VENDOR_ID_Attribute = new Guid(0x3aecb0cc, 0x35b, 0x4bcc, 0x81, 0x85, 0x2b, 0x8d, 0x55, 0x1e, 0xf3, 0xaf);
        //public static readonly Guid MF_WVC1_PROG_SINGLE_SLICE_CONTENT = new Guid(0x67EC2559, 0x0F2F, 0x4420, 0xA4, 0xDD, 0x2F, 0x8E, 0xE7, 0xA5, 0x73, 0x8B);
        //public static readonly Guid MF_PROGRESSIVE_CODING_CONTENT = new Guid(0x8F020EEA, 0x1508, 0x471F, 0x9D, 0xA6, 0x50, 0x7D, 0x7C, 0xFA, 0x40, 0xDB);
        //public static readonly Guid MF_NALU_LENGTH_SET = new Guid(0xA7911D53, 0x12A4, 0x4965, 0xAE, 0x70, 0x6E, 0xAD, 0xD6, 0xFF, 0x05, 0x51);
        //public static readonly Guid MF_NALU_LENGTH_INFORMATION = new Guid(0x19124E7C, 0xAD4B, 0x465F, 0xBB, 0x18, 0x20, 0x18, 0x62, 0x87, 0xB6, 0xAF);
        //public static readonly Guid MF_USER_DATA_PAYLOAD = new Guid(0xd1d4985d, 0xdc92, 0x457a, 0xb3, 0xa0, 0x65, 0x1a, 0x33, 0xa3, 0x10, 0x47);
        //public static readonly Guid MF_MPEG4SINK_SPSPPS_PASSTHROUGH = new Guid(0x5601a134, 0x2005, 0x4ad2, 0xb3, 0x7d, 0x22, 0xa6, 0xc5, 0x54, 0xde, 0xb2);
        //public static readonly Guid MF_MPEG4SINK_MOOV_BEFORE_MDAT = new Guid(0xf672e3ac, 0xe1e6, 0x4f10, 0xb5, 0xec, 0x5f, 0x3b, 0x30, 0x82, 0x88, 0x16);
        //public static readonly Guid MF_STREAM_SINK_SUPPORTS_HW_CONNECTION = new Guid(0x9b465cbf, 0x597, 0x4f9e, 0x9f, 0x3c, 0xb9, 0x7e, 0xee, 0xf9, 0x3, 0x59);
        //public static readonly Guid MF_STREAM_SINK_SUPPORTS_ROTATION = new Guid(0xb3e96280, 0xbd05, 0x41a5, 0x97, 0xad, 0x8a, 0x7f, 0xee, 0x24, 0xb9, 0x12);
        //public static readonly Guid MF_DISABLE_LOCALLY_REGISTERED_PLUGINS = new Guid(0x66b16da9, 0xadd4, 0x47e0, 0xa1, 0x6b, 0x5a, 0xf1, 0xfb, 0x48, 0x36, 0x34);
        //public static readonly Guid MF_LOCAL_PLUGIN_CONTROL_POLICY = new Guid(0xd91b0085, 0xc86d, 0x4f81, 0x88, 0x22, 0x8c, 0x68, 0xe1, 0xd7, 0xfa, 0x04);
        //public static readonly Guid MF_TOPONODE_WORKQUEUE_MMCSS_PRIORITY = new Guid(0x5001f840, 0x2816, 0x48f4, 0x93, 0x64, 0xad, 0x1e, 0xf6, 0x61, 0xa1, 0x23);
        //public static readonly Guid MF_TOPONODE_WORKQUEUE_ITEM_PRIORITY = new Guid(0xa1ff99be, 0x5e97, 0x4a53, 0xb4, 0x94, 0x56, 0x8c, 0x64, 0x2c, 0x0f, 0xf3);
        //public static readonly Guid MF_AUDIO_RENDERER_ATTRIBUTE_STREAM_CATEGORY = new Guid(0xa9770471, 0x92ec, 0x4df4, 0x94, 0xfe, 0x81, 0xc3, 0x6f, 0xc, 0x3a, 0x7a);
        //public static readonly Guid MFPROTECTION_PROTECTED_SURFACE = new Guid(0x4f5d9566, 0xe742, 0x4a25, 0x8d, 0x1f, 0xd2, 0x87, 0xb5, 0xfa, 0x0a, 0xde);
        //public static readonly Guid MFPROTECTION_DISABLE_SCREEN_SCRAPE = new Guid(0xa21179a4, 0xb7cd, 0x40d8, 0x96, 0x14, 0x8e, 0xf2, 0x37, 0x1b, 0xa7, 0x8d);
        //public static readonly Guid MFPROTECTION_VIDEO_FRAMES = new Guid(0x36a59cbc, 0x7401, 0x4a8c, 0xbc, 0x20, 0x46, 0xa7, 0xc9, 0xe5, 0x97, 0xf0);
        //public static readonly Guid MFPROTECTIONATTRIBUTE_BEST_EFFORT = new Guid(0xc8e06331, 0x75f0, 0x4ec1, 0x8e, 0x77, 0x17, 0x57, 0x8f, 0x77, 0x3b, 0x46);
        //public static readonly Guid MFPROTECTIONATTRIBUTE_FAIL_OVER = new Guid(0x8536abc5, 0x38f1, 0x4151, 0x9c, 0xce, 0xf5, 0x5d, 0x94, 0x12, 0x29, 0xac);
        //public static readonly Guid MFPROTECTION_GRAPHICS_TRANSFER_AES_ENCRYPTION = new Guid(0xc873de64, 0xd8a5, 0x49e6, 0x88, 0xbb, 0xfb, 0x96, 0x3f, 0xd3, 0xd4, 0xce);
        //public static readonly Guid MF_XVP_DISABLE_FRC = new Guid(0x2c0afa19, 0x7a97, 0x4d5a, 0x9e, 0xe8, 0x16, 0xd4, 0xfc, 0x51, 0x8d, 0x8c);
        //public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_SYMBOLIC_LINK = new Guid(0x98d24b5e, 0x5930, 0x4614, 0xb5, 0xa1, 0xf6, 0x0, 0xf9, 0x35, 0x5a, 0x78);
        //public static readonly Guid MF_DEVICESTREAM_IMAGE_STREAM = new Guid(0xa7ffb865, 0xe7b2, 0x43b0, 0x9f, 0x6f, 0x9a, 0xf2, 0xa0, 0xe5, 0xf, 0xc0);
        //public static readonly Guid MF_DEVICESTREAM_INDEPENDENT_IMAGE_STREAM = new Guid(0x3eeec7e, 0xd605, 0x4576, 0x8b, 0x29, 0x65, 0x80, 0xb4, 0x90, 0xd7, 0xd3);
        //public static readonly Guid MF_DEVICESTREAM_STREAM_ID = new Guid(0x11bd5120, 0xd124, 0x446b, 0x88, 0xe6, 0x17, 0x6, 0x2, 0x57, 0xff, 0xf9);
        //public static readonly Guid MF_DEVICESTREAM_STREAM_CATEGORY = new Guid(0x2939e7b8, 0xa62e, 0x4579, 0xb6, 0x74, 0xd4, 0x7, 0x3d, 0xfa, 0xbb, 0xba);
        //public static readonly Guid MF_DEVICESTREAM_TRANSFORM_STREAM_ID = new Guid(0xe63937b7, 0xdaaf, 0x4d49, 0x81, 0x5f, 0xd8, 0x26, 0xf8, 0xad, 0x31, 0xe7);
        //public static readonly Guid MF_DEVICESTREAM_EXTENSION_PLUGIN_CLSID = new Guid(0x048e6558, 0x60c4, 0x4173, 0xbd, 0x5b, 0x6a, 0x3c, 0xa2, 0x89, 0x6a, 0xee);
        //public static readonly Guid MF_DEVICESTREAM_EXTENSION_PLUGIN_CONNECTION_POINT = new Guid(0x37f9375c, 0xe664, 0x4ea4, 0xaa, 0xe4, 0xcb, 0x6d, 0x1d, 0xac, 0xa1, 0xf4);
        //public static readonly Guid MF_DEVICESTREAM_TAKEPHOTO_TRIGGER = new Guid(0x1d180e34, 0x538c, 0x4fbb, 0xa7, 0x5a, 0x85, 0x9a, 0xf7, 0xd2, 0x61, 0xa6);
        //public static readonly Guid MF_DEVICESTREAM_MAX_FRAME_BUFFERS = new Guid(0x1684cebe, 0x3175, 0x4985, 0x88, 0x2c, 0x0e, 0xfd, 0x3e, 0x8a, 0xc1, 0x1e);

        //// Windows X attributes
        //public static readonly Guid MF_CAPTURE_METADATA_FRAME_RAWSTREAM = new Guid(0x9252077B, 0x2680, 0x49B9, 0xAE, 0x02, 0xB1, 0x90, 0x75, 0x97, 0x3B, 0x70);
        //public static readonly Guid MF_CAPTURE_METADATA_FOCUSSTATE = new Guid(0xa87ee154, 0x997f, 0x465d, 0xb9, 0x1f, 0x29, 0xd5, 0x3b, 0x98, 0x2b, 0x88);
        //public static readonly Guid MF_CAPTURE_METADATA_REQUESTED_FRAME_SETTING_ID = new Guid(0xbb3716d9, 0x8a61, 0x47a4, 0x81, 0x97, 0x45, 0x9c, 0x7f, 0xf1, 0x74, 0xd5);
        //public static readonly Guid MF_CAPTURE_METADATA_EXPOSURE_TIME = new Guid(0x16b9ae99, 0xcd84, 0x4063, 0x87, 0x9d, 0xa2, 0x8c, 0x76, 0x33, 0x72, 0x9e);
        //public static readonly Guid MF_CAPTURE_METADATA_EXPOSURE_COMPENSATION = new Guid(0xd198aa75, 0x4b62, 0x4345, 0xab, 0xf3, 0x3c, 0x31, 0xfa, 0x12, 0xc2, 0x99);
        //public static readonly Guid MF_CAPTURE_METADATA_ISO_SPEED = new Guid(0xe528a68f, 0xb2e3, 0x44fe, 0x8b, 0x65, 0x7, 0xbf, 0x4b, 0x5a, 0x13, 0xff);
        //public static readonly Guid MF_CAPTURE_METADATA_LENS_POSITION = new Guid(0xb5fc8e86, 0x11d1, 0x4e70, 0x81, 0x9b, 0x72, 0x3a, 0x89, 0xfa, 0x45, 0x20);
        //public static readonly Guid MF_CAPTURE_METADATA_SCENE_MODE = new Guid(0x9cc3b54d, 0x5ed3, 0x4bae, 0xb3, 0x88, 0x76, 0x70, 0xae, 0xf5, 0x9e, 0x13);
        //public static readonly Guid MF_CAPTURE_METADATA_FLASH = new Guid(0x4a51520b, 0xfb36, 0x446c, 0x9d, 0xf2, 0x68, 0x17, 0x1b, 0x9a, 0x3, 0x89);
        //public static readonly Guid MF_CAPTURE_METADATA_FLASH_POWER = new Guid(0x9c0e0d49, 0x205, 0x491a, 0xbc, 0x9d, 0x2d, 0x6e, 0x1f, 0x4d, 0x56, 0x84);
        //public static readonly Guid MF_CAPTURE_METADATA_WHITEBALANCE = new Guid(0xc736fd77, 0xfb9, 0x4e2e, 0x97, 0xa2, 0xfc, 0xd4, 0x90, 0x73, 0x9e, 0xe9);
        //public static readonly Guid MF_CAPTURE_METADATA_ZOOMFACTOR = new Guid(0xe50b0b81, 0xe501, 0x42c2, 0xab, 0xf2, 0x85, 0x7e, 0xcb, 0x13, 0xfa, 0x5c);
        //public static readonly Guid MF_CAPTURE_METADATA_FACEROIS = new Guid(0x864f25a6, 0x349f, 0x46b1, 0xa3, 0xe, 0x54, 0xcc, 0x22, 0x92, 0x8a, 0x47);
        //public static readonly Guid MF_CAPTURE_METADATA_FACEROITIMESTAMPS = new Guid(0xe94d50cc, 0x3da0, 0x44d4, 0xbb, 0x34, 0x83, 0x19, 0x8a, 0x74, 0x18, 0x68);
        //public static readonly Guid MF_CAPTURE_METADATA_FACEROICHARACTERIZATIONS = new Guid(0xb927a1a8, 0x18ef, 0x46d3, 0xb3, 0xaf, 0x69, 0x37, 0x2f, 0x94, 0xd9, 0xb2);
        //public static readonly Guid MF_CAPTURE_METADATA_ISO_GAINS = new Guid(0x5802ac9, 0xe1d, 0x41c7, 0xa8, 0xc8, 0x7e, 0x73, 0x69, 0xf8, 0x4e, 0x1e);
        //public static readonly Guid MF_CAPTURE_METADATA_SENSORFRAMERATE = new Guid(0xdb51357e, 0x9d3d, 0x4962, 0xb0, 0x6d, 0x7, 0xce, 0x65, 0xd, 0x9a, 0xa);
        //public static readonly Guid MF_CAPTURE_METADATA_WHITEBALANCE_GAINS = new Guid(0xe7570c8f, 0x2dcb, 0x4c7c, 0xaa, 0xce, 0x22, 0xec, 0xe7, 0xcc, 0xe6, 0x47);
        //public static readonly Guid MF_CAPTURE_METADATA_HISTOGRAM = new Guid(0x85358432, 0x2ef6, 0x4ba9, 0xa3, 0xfb, 0x6, 0xd8, 0x29, 0x74, 0xb8, 0x95);

        //public static readonly Guid MF_SINK_VIDEO_PTS = new Guid(0x2162bde7, 0x421e, 0x4b90, 0x9b, 0x33, 0xe5, 0x8f, 0xbf, 0x1d, 0x58, 0xb6);
        //public static readonly Guid MF_SINK_VIDEO_NATIVE_WIDTH = new Guid(0xe6d6a707, 0x1505, 0x4747, 0x9b, 0x10, 0x72, 0xd2, 0xd1, 0x58, 0xcb, 0x3a);
        //public static readonly Guid MF_SINK_VIDEO_NATIVE_HEIGHT = new Guid(0xf0ca6705, 0x490c, 0x43e8, 0x94, 0x1c, 0xc0, 0xb3, 0x20, 0x6b, 0x9a, 0x65);
        //public static readonly Guid MF_SINK_VIDEO_DISPLAY_ASPECT_RATIO_NUMERATOR = new Guid(0xd0f33b22, 0xb78a, 0x4879, 0xb4, 0x55, 0xf0, 0x3e, 0xf3, 0xfa, 0x82, 0xcd);
        //public static readonly Guid MF_SINK_VIDEO_DISPLAY_ASPECT_RATIO_DENOMINATOR = new Guid(0x6ea1eb97, 0x1fe0, 0x4f10, 0xa6, 0xe4, 0x1f, 0x4f, 0x66, 0x15, 0x64, 0xe0);
        //public static readonly Guid MF_BD_MVC_PLANE_OFFSET_METADATA = new Guid(0x62a654e4, 0xb76c, 0x4901, 0x98, 0x23, 0x2c, 0xb6, 0x15, 0xd4, 0x73, 0x18);
        //public static readonly Guid MF_LUMA_KEY_ENABLE = new Guid(0x7369820f, 0x76de, 0x43ca, 0x92, 0x84, 0x47, 0xb8, 0xf3, 0x7e, 0x06, 0x49);
        //public static readonly Guid MF_LUMA_KEY_LOWER = new Guid(0x93d7b8d5, 0x0b81, 0x4715, 0xae, 0xa0, 0x87, 0x25, 0x87, 0x16, 0x21, 0xe9);
        //public static readonly Guid MF_LUMA_KEY_UPPER = new Guid(0xd09f39bb, 0x4602, 0x4c31, 0xa7, 0x06, 0xa1, 0x21, 0x71, 0xa5, 0x11, 0x0a);
        //public static readonly Guid MF_USER_EXTENDED_ATTRIBUTES = new Guid(0xc02abac6, 0xfeb2, 0x4541, 0x92, 0x2f, 0x92, 0x0b, 0x43, 0x70, 0x27, 0x22);
        //public static readonly Guid MF_INDEPENDENT_STILL_IMAGE = new Guid(0xea12af41, 0x0710, 0x42c9, 0xa1, 0x27, 0xda, 0xa3, 0xe7, 0x84, 0x83, 0xa5);
        //public static readonly Guid MF_MEDIA_PROTECTION_MANAGER_PROPERTIES = new Guid(0x38BD81A9, 0xACEA, 0x4C73, 0x89, 0xB2, 0x55, 0x32, 0xC0, 0xAE, 0xCA, 0x79);
        //public static readonly Guid MFPROTECTION_HARDWARE = new Guid(0x4ee7f0c1, 0x9ed7, 0x424f, 0xb6, 0xbe, 0x99, 0x6b, 0x33, 0x52, 0x88, 0x56);
        //public static readonly Guid MFPROTECTION_HDCP_WITH_TYPE_ENFORCEMENT = new Guid(0xa4a585e8, 0xed60, 0x442d, 0x81, 0x4d, 0xdb, 0x4d, 0x42, 0x20, 0xa0, 0x6d);
        //public static readonly Guid MF_XVP_CALLER_ALLOCATES_OUTPUT = new Guid(0x4a2cabc, 0xcab, 0x40b1, 0xa1, 0xb9, 0x75, 0xbc, 0x36, 0x58, 0xf0, 0x0);
        //public static readonly Guid MF_DEVICEMFT_EXTENSION_PLUGIN_CLSID = new Guid(0x844dbae, 0x34fa, 0x48a0, 0xa7, 0x83, 0x8e, 0x69, 0x6f, 0xb1, 0xc9, 0xa8);
        //public static readonly Guid MF_DEVICEMFT_CONNECTED_FILTER_KSCONTROL = new Guid(0x6a2c4fa6, 0xd179, 0x41cd, 0x95, 0x23, 0x82, 0x23, 0x71, 0xea, 0x40, 0xe5);
        //public static readonly Guid MF_DEVICEMFT_CONNECTED_PIN_KSCONTROL = new Guid(0xe63310f7, 0xb244, 0x4ef8, 0x9a, 0x7d, 0x24, 0xc7, 0x4e, 0x32, 0xeb, 0xd0);
        //public static readonly Guid MF_DEVICE_THERMAL_STATE_CHANGED = new Guid(0x70ccd0af, 0xfc9f, 0x4deb, 0xa8, 0x75, 0x9f, 0xec, 0xd1, 0x6c, 0x5b, 0xd4);
        //public static readonly Guid MF_ACCESS_CONTROLLED_MEDIASOURCE_SERVICE = new Guid(0x14a5031, 0x2f05, 0x4c6a, 0x9f, 0x9c, 0x7d, 0xd, 0xc4, 0xed, 0xa5, 0xf4);
        //public static readonly Guid MF_CONTENT_DECRYPTOR_SERVICE = new Guid(0x68a72927, 0xfc7b, 0x44ee, 0x85, 0xf4, 0x7c, 0x51, 0xbd, 0x55, 0xa6, 0x59);
        //public static readonly Guid MF_CONTENT_PROTECTION_DEVICE_SERVICE = new Guid(0xff58436f, 0x76a0, 0x41fe, 0xb5, 0x66, 0x10, 0xcc, 0x53, 0x96, 0x2e, 0xdd);
        //public static readonly Guid MF_SD_AUDIO_ENCODER_DELAY = new Guid(0x8e85422c, 0x73de, 0x403f, 0x9a, 0x35, 0x55, 0x0a, 0xd6, 0xe8, 0xb9, 0x51);
        //public static readonly Guid MF_SD_AUDIO_ENCODER_PADDING = new Guid(0x529c7f2c, 0xac4b, 0x4e3f, 0xbf, 0xc3, 0x09, 0x02, 0x19, 0x49, 0x82, 0xcb);

        //public static readonly Guid MFT_END_STREAMING_AWARE = new Guid(0x70fbc845, 0xb07e, 0x4089, 0xb0, 0x64, 0x39, 0x9d, 0xc6, 0x11, 0xf, 0x29);
        //public static readonly Guid MF_SA_D3D11_ALLOW_DYNAMIC_YUV_TEXTURE = new Guid(0xce06d49f, 0x613, 0x4b9d, 0x86, 0xa6, 0xd8, 0xc4, 0xf9, 0xc1, 0x0, 0x75);
        //public static readonly Guid MFT_DECODER_QUALITY_MANAGEMENT_CUSTOM_CONTROL = new Guid(0xa24e30d7, 0xde25, 0x4558, 0xbb, 0xfb, 0x71, 0x7, 0xa, 0x2d, 0x33, 0x2e);
        //public static readonly Guid MFT_DECODER_QUALITY_MANAGEMENT_RECOVERY_WITHOUT_ARTIFACTS = new Guid(0xd8980deb, 0xa48, 0x425f, 0x86, 0x23, 0x61, 0x1d, 0xb4, 0x1d, 0x38, 0x10);

        //public static readonly Guid MF_SOURCE_READER_D3D11_BIND_FLAGS = new Guid(0x33f3197b, 0xf73a, 0x4e14, 0x8d, 0x85, 0xe, 0x4c, 0x43, 0x68, 0x78, 0x8d);
        //public static readonly Guid MF_MEDIASINK_AUTOFINALIZE_SUPPORTED = new Guid(0x48c131be, 0x135a, 0x41cb, 0x82, 0x90, 0x3, 0x65, 0x25, 0x9, 0xc9, 0x99);
        //public static readonly Guid MF_MEDIASINK_ENABLE_AUTOFINALIZE = new Guid(0x34014265, 0xcb7e, 0x4cde, 0xac, 0x7c, 0xef, 0xfd, 0x3b, 0x3c, 0x25, 0x30);
        //public static readonly Guid MF_READWRITE_ENABLE_AUTOFINALIZE = new Guid(0xdd7ca129, 0x8cd1, 0x4dc5, 0x9d, 0xde, 0xce, 0x16, 0x86, 0x75, 0xde, 0x61);

        //public static readonly Guid MF_MEDIA_ENGINE_BROWSER_COMPATIBILITY_MODE_IE_EDGE = new Guid(0xa6f3e465, 0x3aca, 0x442c, 0xa3, 0xf0, 0xad, 0x6d, 0xda, 0xd8, 0x39, 0xae);
        //public static readonly Guid MF_MEDIA_ENGINE_TELEMETRY_APPLICATION_ID = new Guid(0x1e7b273b, 0xa7e4, 0x402a, 0x8f, 0x51, 0xc4, 0x8e, 0x88, 0xa2, 0xca, 0xbc);
        //public static readonly Guid MF_MEDIA_ENGINE_TIMEDTEXT = new Guid(0x805ea411, 0x92e0, 0x4e59, 0x9b, 0x6e, 0x5c, 0x7d, 0x79, 0x15, 0xe6, 0x4f);
        //public static readonly Guid MF_MEDIA_ENGINE_CONTINUE_ON_CODEC_ERROR = new Guid(0xdbcdb7f9, 0x48e4, 0x4295, 0xb7, 0x0d, 0xd5, 0x18, 0x23, 0x4e, 0xeb, 0x38);

        //public static readonly Guid MF_CAPTURE_ENGINE_CAMERA_STREAM_BLOCKED = new Guid(0xA4209417, 0x8D39, 0x46F3, 0xB7, 0x59, 0x59, 0x12, 0x52, 0x8F, 0x42, 0x07);
        //public static readonly Guid MF_CAPTURE_ENGINE_CAMERA_STREAM_UNBLOCKED = new Guid(0x9BE9EEF0, 0xCDAF, 0x4717, 0x85, 0x64, 0x83, 0x4A, 0xAE, 0x66, 0x41, 0x5C);
        //public static readonly Guid MF_CAPTURE_ENGINE_ENABLE_CAMERA_STREAMSTATE_NOTIFICATION = new Guid(0x4C808E9D, 0xAAED, 0x4713, 0x90, 0xFB, 0xCB, 0x24, 0x06, 0x4A, 0xB8, 0xDA);
        //public static readonly Guid MF_CAPTURE_ENGINE_SELECTEDCAMERAPROFILE = new Guid(0x03160B7E, 0x1C6F, 0x4DB2, 0xAD, 0x56, 0xA7, 0xC4, 0x30, 0xF8, 0x23, 0x92);
        //public static readonly Guid MF_CAPTURE_ENGINE_SELECTEDCAMERAPROFILE_INDEX = new Guid(0x3CE88613, 0x2214, 0x46C3, 0xB4, 0x17, 0x82, 0xF8, 0xA3, 0x13, 0xC9, 0xC3);

        //public static readonly Guid EVRConfig_AllowBatching = new Guid(0xe447df0a, 0x10ca, 0x4d17, 0xb1, 0x7e, 0x6a, 0x84, 0x0f, 0x8a, 0x3a, 0x4c);
        //public static readonly Guid EVRConfig_AllowDropToBob = new Guid(0xe447df02, 0x10ca, 0x4d17, 0xb1, 0x7e, 0x6a, 0x84, 0x0f, 0x8a, 0x3a, 0x4c);
        //public static readonly Guid EVRConfig_AllowDropToHalfInterlace = new Guid(0xe447df06, 0x10ca, 0x4d17, 0xb1, 0x7e, 0x6a, 0x84, 0x0f, 0x8a, 0x3a, 0x4c);
        //public static readonly Guid EVRConfig_AllowDropToThrottle = new Guid(0xe447df04, 0x10ca, 0x4d17, 0xb1, 0x7e, 0x6a, 0x84, 0x0f, 0x8a, 0x3a, 0x4c);
        //public static readonly Guid EVRConfig_AllowScaling = new Guid(0xe447df08, 0x10ca, 0x4d17, 0xb1, 0x7e, 0x6a, 0x84, 0x0f, 0x8a, 0x3a, 0x4c);
        //public static readonly Guid EVRConfig_ForceBatching = new Guid(0xe447df09, 0x10ca, 0x4d17, 0xb1, 0x7e, 0x6a, 0x84, 0x0f, 0x8a, 0x3a, 0x4c);
        //public static readonly Guid EVRConfig_ForceBob = new Guid(0xe447df01, 0x10ca, 0x4d17, 0xb1, 0x7e, 0x6a, 0x84, 0x0f, 0x8a, 0x3a, 0x4c);
        //public static readonly Guid EVRConfig_ForceHalfInterlace = new Guid(0xe447df05, 0x10ca, 0x4d17, 0xb1, 0x7e, 0x6a, 0x84, 0x0f, 0x8a, 0x3a, 0x4c);
        //public static readonly Guid EVRConfig_ForceScaling = new Guid(0xe447df07, 0x10ca, 0x4d17, 0xb1, 0x7e, 0x6a, 0x84, 0x0f, 0x8a, 0x3a, 0x4c);
        //public static readonly Guid EVRConfig_ForceThrottle = new Guid(0xe447df03, 0x10ca, 0x4d17, 0xb1, 0x7e, 0x6a, 0x84, 0x0f, 0x8a, 0x3a, 0x4c);
        //public static readonly Guid MF_ASFPROFILE_MAXPACKETSIZE = new Guid(0x22587627, 0x47de, 0x4168, 0x87, 0xf5, 0xb5, 0xaa, 0x9b, 0x12, 0xa8, 0xf0);
        //public static readonly Guid MF_ASFPROFILE_MINPACKETSIZE = new Guid(0x22587626, 0x47de, 0x4168, 0x87, 0xf5, 0xb5, 0xaa, 0x9b, 0x12, 0xa8, 0xf0);
        //public static readonly Guid MF_CAPTURE_ENGINE_D3D_MANAGER = new Guid(0x76e25e7b, 0xd595, 0x4283, 0x96, 0x2c, 0xc5, 0x94, 0xaf, 0xd7, 0x8d, 0xdf);
        //public static readonly Guid MF_CAPTURE_ENGINE_DECODER_MFT_FIELDOFUSE_UNLOCK_Attribute = new Guid(0x2b8ad2e8, 0x7acb, 0x4321, 0xa6, 0x06, 0x32, 0x5c, 0x42, 0x49, 0xf4, 0xfc);
        //public static readonly Guid MF_CAPTURE_ENGINE_DISABLE_DXVA = new Guid(0xf9818862, 0x179d, 0x433f, 0xa3, 0x2f, 0x74, 0xcb, 0xcf, 0x74, 0x46, 0x6d);
        //public static readonly Guid MF_CAPTURE_ENGINE_DISABLE_HARDWARE_TRANSFORMS = new Guid(0xb7c42a6b, 0x3207, 0x4495, 0xb4, 0xe7, 0x81, 0xf9, 0xc3, 0x5d, 0x59, 0x91);
        //public static readonly Guid MF_CAPTURE_ENGINE_ENCODER_MFT_FIELDOFUSE_UNLOCK_Attribute = new Guid(0x54c63a00, 0x78d5, 0x422f, 0xaa, 0x3e, 0x5e, 0x99, 0xac, 0x64, 0x92, 0x69);
        //public static readonly Guid MF_CAPTURE_ENGINE_EVENT_GENERATOR_GUID = new Guid(0xabfa8ad5, 0xfc6d, 0x4911, 0x87, 0xe0, 0x96, 0x19, 0x45, 0xf8, 0xf7, 0xce);
        //public static readonly Guid MF_CAPTURE_ENGINE_EVENT_STREAM_INDEX = new Guid(0x82697f44, 0xb1cf, 0x42eb, 0x97, 0x53, 0xf8, 0x6d, 0x64, 0x9c, 0x88, 0x65);
        //public static readonly Guid MF_CAPTURE_ENGINE_MEDIASOURCE_CONFIG = new Guid(0xbc6989d2, 0x0fc1, 0x46e1, 0xa7, 0x4f, 0xef, 0xd3, 0x6b, 0xc7, 0x88, 0xde);
        //public static readonly Guid MF_CAPTURE_ENGINE_OUTPUT_MEDIA_TYPE_SET = new Guid(0xcaaad994, 0x83ec, 0x45e9, 0xa3, 0x0a, 0x1f, 0x20, 0xaa, 0xdb, 0x98, 0x31);
        //public static readonly Guid MF_CAPTURE_ENGINE_RECORD_SINK_AUDIO_MAX_PROCESSED_SAMPLES = new Guid(0x9896e12a, 0xf707, 0x4500, 0xb6, 0xbd, 0xdb, 0x8e, 0xb8, 0x10, 0xb5, 0xf);
        //public static readonly Guid MF_CAPTURE_ENGINE_RECORD_SINK_AUDIO_MAX_UNPROCESSED_SAMPLES = new Guid(0x1cddb141, 0xa7f4, 0x4d58, 0x98, 0x96, 0x4d, 0x15, 0xa5, 0x3c, 0x4e, 0xfe);
        //public static readonly Guid MF_CAPTURE_ENGINE_RECORD_SINK_VIDEO_MAX_PROCESSED_SAMPLES = new Guid(0xe7b4a49e, 0x382c, 0x4aef, 0xa9, 0x46, 0xae, 0xd5, 0x49, 0xb, 0x71, 0x11);
        //public static readonly Guid MF_CAPTURE_ENGINE_RECORD_SINK_VIDEO_MAX_UNPROCESSED_SAMPLES = new Guid(0xb467f705, 0x7913, 0x4894, 0x9d, 0x42, 0xa2, 0x15, 0xfe, 0xa2, 0x3d, 0xa9);
        //public static readonly Guid MF_CAPTURE_ENGINE_USE_AUDIO_DEVICE_ONLY = new Guid(0x1c8077da, 0x8466, 0x4dc4, 0x8b, 0x8e, 0x27, 0x6b, 0x3f, 0x85, 0x92, 0x3b);
        //public static readonly Guid MF_CAPTURE_ENGINE_USE_VIDEO_DEVICE_ONLY = new Guid(0x7e025171, 0xcf32, 0x4f2e, 0x8f, 0x19, 0x41, 0x5, 0x77, 0xb7, 0x3a, 0x66);
        //public static readonly Guid MF_SOURCE_STREAM_SUPPORTS_HW_CONNECTION = new Guid(0xa38253aa, 0x6314, 0x42fd, 0xa3, 0xce, 0xbb, 0x27, 0xb6, 0x85, 0x99, 0x46);
        //public static readonly Guid MF_VIDEODSP_MODE = new Guid(0x16d720f0, 0x768c, 0x11de, 0x8a, 0x39, 0x08, 0x00, 0x20, 0x0c, 0x9a, 0x66);
        //public static readonly Guid MFASFSPLITTER_PACKET_BOUNDARY = new Guid(0xfe584a05, 0xe8d6, 0x42e3, 0xb1, 0x76, 0xf1, 0x21, 0x17, 0x5, 0xfb, 0x6f);
        //public static readonly Guid MFSampleExtension_DeviceReferenceSystemTime = new Guid(0x6523775a, 0xba2d, 0x405f, 0xb2, 0xc5, 0x01, 0xff, 0x88, 0xe2, 0xe8, 0xf6);
        //public static readonly Guid MFSampleExtension_VideoDSPMode = new Guid(0xc12d55cb, 0xd7d9, 0x476d, 0x81, 0xf3, 0x69, 0x11, 0x7f, 0x16, 0x3e, 0xa0);

        //public static readonly Guid MF_MEDIA_ENGINE_CALLBACK = new Guid(0xc60381b8, 0x83a4, 0x41f8, 0xa3, 0xd0, 0xde, 0x05, 0x07, 0x68, 0x49, 0xa9);
        //public static readonly Guid MF_MEDIA_ENGINE_DXGI_MANAGER = new Guid(0x065702da, 0x1094, 0x486d, 0x86, 0x17, 0xee, 0x7c, 0xc4, 0xee, 0x46, 0x48);
        //public static readonly Guid MF_MEDIA_ENGINE_EXTENSION = new Guid(0x3109fd46, 0x060d, 0x4b62, 0x8d, 0xcf, 0xfa, 0xff, 0x81, 0x13, 0x18, 0xd2);
        //public static readonly Guid MF_MEDIA_ENGINE_PLAYBACK_HWND = new Guid(0xd988879b, 0x67c9, 0x4d92, 0xba, 0xa7, 0x6e, 0xad, 0xd4, 0x46, 0x03, 0x9d);
        //public static readonly Guid MF_MEDIA_ENGINE_OPM_HWND = new Guid(0xa0be8ee7, 0x0572, 0x4f2c, 0xa8, 0x01, 0x2a, 0x15, 0x1b, 0xd3, 0xe7, 0x26);
        //public static readonly Guid MF_MEDIA_ENGINE_PLAYBACK_VISUAL = new Guid(0x6debd26f, 0x6ab9, 0x4d7e, 0xb0, 0xee, 0xc6, 0x1a, 0x73, 0xff, 0xad, 0x15);
        //public static readonly Guid MF_MEDIA_ENGINE_COREWINDOW = new Guid(0xfccae4dc, 0x0b7f, 0x41c2, 0x9f, 0x96, 0x46, 0x59, 0x94, 0x8a, 0xcd, 0xdc);
        //public static readonly Guid MF_MEDIA_ENGINE_VIDEO_OUTPUT_FORMAT = new Guid(0x5066893c, 0x8cf9, 0x42bc, 0x8b, 0x8a, 0x47, 0x22, 0x12, 0xe5, 0x27, 0x26);
        //public static readonly Guid MF_MEDIA_ENGINE_CONTENT_PROTECTION_FLAGS = new Guid(0xe0350223, 0x5aaf, 0x4d76, 0xa7, 0xc3, 0x06, 0xde, 0x70, 0x89, 0x4d, 0xb4);
        //public static readonly Guid MF_MEDIA_ENGINE_CONTENT_PROTECTION_MANAGER = new Guid(0xfdd6dfaa, 0xbd85, 0x4af3, 0x9e, 0x0f, 0xa0, 0x1d, 0x53, 0x9d, 0x87, 0x6a);
        //public static readonly Guid MF_MEDIA_ENGINE_AUDIO_ENDPOINT_ROLE = new Guid(0xd2cb93d1, 0x116a, 0x44f2, 0x93, 0x85, 0xf7, 0xd0, 0xfd, 0xa2, 0xfb, 0x46);
        //public static readonly Guid MF_MEDIA_ENGINE_AUDIO_CATEGORY = new Guid(0xc8d4c51d, 0x350e, 0x41f2, 0xba, 0x46, 0xfa, 0xeb, 0xbb, 0x08, 0x57, 0xf6);
        //public static readonly Guid MF_MEDIA_ENGINE_STREAM_CONTAINS_ALPHA_CHANNEL = new Guid(0x5cbfaf44, 0xd2b2, 0x4cfb, 0x80, 0xa7, 0xd4, 0x29, 0xc7, 0x4c, 0x78, 0x9d);
        //public static readonly Guid MF_MEDIA_ENGINE_BROWSER_COMPATIBILITY_MODE = new Guid(0x4e0212e2, 0xe18f, 0x41e1, 0x95, 0xe5, 0xc0, 0xe7, 0xe9, 0x23, 0x5b, 0xc3);
        //public static readonly Guid MF_MEDIA_ENGINE_SOURCE_RESOLVER_CONFIG_STORE = new Guid(0x0ac0c497, 0xb3c4, 0x48c9, 0x9c, 0xde, 0xbb, 0x8c, 0xa2, 0x44, 0x2c, 0xa3);
        //public static readonly Guid MF_MEDIA_ENGINE_NEEDKEY_CALLBACK = new Guid(0x7ea80843, 0xb6e4, 0x432c, 0x8e, 0xa4, 0x78, 0x48, 0xff, 0xe4, 0x22, 0x0e);
        //public static readonly Guid MF_MEDIA_ENGINE_TRACK_ID = new Guid(0x65bea312, 0x4043, 0x4815, 0x8e, 0xab, 0x44, 0xdc, 0xe2, 0xef, 0x8f, 0x2a);

        //public static readonly Guid MFPROTECTION_ACP = new Guid(0xc3fd11c6, 0xf8b7, 0x4d20, 0xb0, 0x08, 0x1d, 0xb1, 0x7d, 0x61, 0xf2, 0xda);
        //public static readonly Guid MFPROTECTION_CGMSA = new Guid(0xE57E69E9, 0x226B, 0x4d31, 0xB4, 0xE3, 0xD3, 0xDB, 0x00, 0x87, 0x36, 0xDD);
        //public static readonly Guid MFPROTECTION_CONSTRICTAUDIO = new Guid(0xffc99b44, 0xdf48, 0x4e16, 0x8e, 0x66, 0x09, 0x68, 0x92, 0xc1, 0x57, 0x8a);
        //public static readonly Guid MFPROTECTION_CONSTRICTVIDEO = new Guid(0x193370ce, 0xc5e4, 0x4c3a, 0x8a, 0x66, 0x69, 0x59, 0xb4, 0xda, 0x44, 0x42);
        //public static readonly Guid MFPROTECTION_CONSTRICTVIDEO_NOOPM = new Guid(0xa580e8cd, 0xc247, 0x4957, 0xb9, 0x83, 0x3c, 0x2e, 0xeb, 0xd1, 0xff, 0x59);
        //public static readonly Guid MFPROTECTION_DISABLE = new Guid(0x8cc6d81b, 0xfec6, 0x4d8f, 0x96, 0x4b, 0xcf, 0xba, 0x0b, 0x0d, 0xad, 0x0d);
        //public static readonly Guid MFPROTECTION_FFT = new Guid(0x462a56b2, 0x2866, 0x4bb6, 0x98, 0x0d, 0x6d, 0x8d, 0x9e, 0xdb, 0x1a, 0x8c);
        //public static readonly Guid MFPROTECTION_HDCP = new Guid(0xAE7CC03D, 0xC828, 0x4021, 0xac, 0xb7, 0xd5, 0x78, 0xd2, 0x7a, 0xaf, 0x13);
        //public static readonly Guid MFPROTECTION_TRUSTEDAUDIODRIVERS = new Guid(0x65bdf3d2, 0x0168, 0x4816, 0xa5, 0x33, 0x55, 0xd4, 0x7b, 0x02, 0x71, 0x01);
        //public static readonly Guid MFPROTECTION_WMDRMOTA = new Guid(0xa267a6a1, 0x362e, 0x47d0, 0x88, 0x05, 0x46, 0x28, 0x59, 0x8a, 0x23, 0xe4);
        //public static readonly Guid MFSampleExtension_Encryption_SampleID = new Guid(0x6698b84e, 0x0afa, 0x4330, 0xae, 0xb2, 0x1c, 0x0a, 0x98, 0xd7, 0xa4, 0x4d);
        //public static readonly Guid MFSampleExtension_Encryption_SubSampleMappingSplit = new Guid(0xfe0254b9, 0x2aa5, 0x4edc, 0x99, 0xf7, 0x17, 0xe8, 0x9d, 0xbf, 0x91, 0x74);
        //public static readonly Guid MFSampleExtension_PacketCrossOffsets = new Guid(0x2789671d, 0x389f, 0x40bb, 0x90, 0xd9, 0xc2, 0x82, 0xf7, 0x7f, 0x9a, 0xbd);
        //public static readonly Guid MFSampleExtension_Content_KeyID = new Guid(0xc6c7f5b0, 0xacca, 0x415b, 0x87, 0xd9, 0x10, 0x44, 0x14, 0x69, 0xef, 0xc6);
        //public static readonly Guid MF_MSE_ACTIVELIST_CALLBACK = new Guid(0x949bda0f, 0x4549, 0x46d5, 0xad, 0x7f, 0xb8, 0x46, 0xe1, 0xab, 0x16, 0x52);
        //public static readonly Guid MF_MSE_BUFFERLIST_CALLBACK = new Guid(0x42e669b0, 0xd60e, 0x4afb, 0xa8, 0x5b, 0xd8, 0xe5, 0xfe, 0x6b, 0xda, 0xb5);
        //public static readonly Guid MF_MSE_CALLBACK = new Guid(0x9063a7c0, 0x42c5, 0x4ffd, 0xa8, 0xa8, 0x6f, 0xcf, 0x9e, 0xa3, 0xd0, 0x0c);
        //public static readonly Guid MF_MT_VIDEO_3D = new Guid(0xcb5e88cf, 0x7b5b, 0x476b, 0x85, 0xaa, 0x1c, 0xa5, 0xae, 0x18, 0x75, 0x55);
    }

    internal static class MFExtern
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFShutdown();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFStartup(int Version, MFStartup dwFlags);

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        //[DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        //public static extern HResult MFCreatePresentationDescriptor(
        //    int cStreamDescriptors,
        //    [In, MarshalAs(UnmanagedType.LPArray)] IMFStreamDescriptor[] apStreamDescriptors,
        //    out IMFPresentationDescriptor ppPresentationDescriptor
        //);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFGetService(
            [In, MarshalAs(UnmanagedType.Interface)] object punkObject,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out, MarshalAs(UnmanagedType.Interface)] out object ppvObject
        );

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateVideoRendererActivate(
            IntPtr hwndVideo,
            out IMFActivate ppActivate
        );

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateTopologyNode(
            MFTopologyType NodeType,
            out IMFTopologyNode ppNode
        );

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateSourceResolver(
            out IMFSourceResolver ppISourceResolver
        );

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateMediaSession(
            IMFAttributes pConfiguration,
            out IMFMediaSession ppMediaSession
        );

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateTopology(
            out IMFTopology ppTopo
        );

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateAudioRendererActivate(
            out IMFActivate ppActivate
        );

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
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
            ulong ul = (ulong)unPacked;
            punHigh = (int)(ul >> 32);
            punLow = (int)(ul & 0xffffffff);
        }

        //[DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        //public static extern HResult MFTEnumEx(
        //    [In] Guid MFTransformCategory,
        //    //MFT_EnumFlag Flags,
        //    int Flags,
        //    [In, MarshalAs(UnmanagedType.LPStruct)] MFTRegisterTypeInfo pInputType,
        //    [In, MarshalAs(UnmanagedType.LPStruct)] MFTRegisterTypeInfo pOutputType,
        //    [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown, SizeParamIndex = 5)] out IMFActivate[] pppMFTActivate,
        //    //[Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown)] out IMFActivate[] pppMFTActivate,
        //    out int pnumMFTActivate
        //);

        //// use with NET 2.0, 3.0 and  3.5 (returns only 1 device) (because of SizeParamIndex)
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        //[DllImport("mf.dll", ExactSpelling = true, EntryPoint = "MFEnumDeviceSources"), SuppressUnmanagedCodeSecurity]
        //public static extern HResult MFEnumDeviceSourcesEx(
        //IMFAttributes pAttributes,
        //[MarshalAs(UnmanagedType.LPArray)] out IMFActivate[] pppSourceActivate,
        //out int pcSourceActivate);

        // Works only with .NET Framework version 4.0 or higher (because of SizeParamIndex)
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFEnumDeviceSources(
        IMFAttributes pAttributes,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out IMFActivate[] pppSourceActivate,
        out int pcSourceActivate);

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        //[DllImport("mf.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        //public static extern HResult MFCreateDeviceSourceActivate(
        //    IMFAttributes pAttributes,
        //    out IMFActivate ppActivate
        //);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Mfreadwrite.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Mfreadwrite.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateSinkWriterFromURL(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszOutputURL,
            IMFByteStream pByteStream,
            IMFAttributes pAttributes,
            out IMFSinkWriter ppSinkWriter
        );

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateMediaType(
            out IMFMediaType ppMFType
        );

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateCollection(
            out IMFCollection ppIMFCollection
        );

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mf.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateAggregateSource(
            IMFCollection pSourceCollection,
            out IMFMediaSource ppAggSource
        );

        public static HResult MFSetAttributeSize(
            IMFAttributes pAttributes,
            Guid guidKey,
            int unWidth,
            int unHeight
            )
        {
            return MFSetAttribute2UINT32asUINT64(pAttributes, guidKey, unWidth, unHeight);
        }

        public static HResult MFSetAttribute2UINT32asUINT64(
            IMFAttributes pAttributes,
            Guid guidKey,
            int unHigh32,
            int unLow32
            )
        {
            return pAttributes.SetUINT64(guidKey, Pack2UINT32AsUINT64(unHigh32, unLow32));
        }

        public static long Pack2UINT32AsUINT64(int unHigh, int unLow)
        {
            uint low = (uint)unLow;
            uint high = (uint)unHigh;

            ulong ul = (high);
            ul <<= 32;
            ul |= low;

            return (long)ul;
        }

        public static HResult MFSetAttributeRatio(
            IMFAttributes pAttributes,
            Guid guidKey,
            int unNumerator,
            int unDenominator
            )
        {
            return MFSetAttribute2UINT32asUINT64(pAttributes, guidKey, unNumerator, unDenominator);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateSample(
            out IMFSample ppIMFSample
        );

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCreateMemoryBuffer(
            int cbMaxLength,
            out IMFMediaBuffer ppBuffer
        );

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("evr.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern HResult MFCopyImage(
            IntPtr pDest,
            int lDestStride,
            IntPtr pSrc,
            int lSrcStride,
            int dwWidthInBytes,
            int dwLines
            );

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        //[DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        //public static extern HResult MFCreateMFByteStreamOnStream(
        //    IStream pStream,
        //    out IMFByteStream ppByteStream
        //);

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        //[DllImport("mfplat.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        //public static extern HResult MFCreateMFByteStreamOnStreamEx(
        //    [MarshalAs(UnmanagedType.IUnknown)] object punkStream,
        //    out IMFByteStream ppByteStream
        //);
    }

    internal static class MFMediaType
    {
        ///// <summary> From MFMediaType_Default </summary>
        //public static readonly Guid Default = new Guid(0x81A412E6, 0x8103, 0x4B06, 0x85, 0x7F, 0x18, 0x62, 0x78, 0x10, 0x24, 0xAC);
        /// <summary> From MFMediaType_Audio </summary>
        public static readonly Guid Audio = new Guid(0x73647561, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);
        /// <summary> From MFMediaType_Video </summary>
        public static readonly Guid Video = new Guid(0x73646976, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);
        ///// <summary> From MFMediaType_Protected </summary>
        //public static readonly Guid Protected = new Guid(0x7b4b6fe6, 0x9d04, 0x4494, 0xbe, 0x14, 0x7e, 0x0b, 0xd0, 0x76, 0xc8, 0xe4);
        ///// <summary> From MFMediaType_SAMI </summary>
        //public static readonly Guid SAMI = new Guid(0xe69669a0, 0x3dcd, 0x40cb, 0x9e, 0x2e, 0x37, 0x08, 0x38, 0x7c, 0x06, 0x16);
        ///// <summary> From MFMediaType_Script </summary>
        //public static readonly Guid Script = new Guid(0x72178C22, 0xE45B, 0x11D5, 0xBC, 0x2A, 0x00, 0xB0, 0xD0, 0xF3, 0xF4, 0xAB);
        ///// <summary> From MFMediaType_Image </summary>
        //public static readonly Guid Image = new Guid(0x72178C23, 0xE45B, 0x11D5, 0xBC, 0x2A, 0x00, 0xB0, 0xD0, 0xF3, 0xF4, 0xAB);
        ///// <summary> From MFMediaType_HTML </summary>
        //public static readonly Guid HTML = new Guid(0x72178C24, 0xE45B, 0x11D5, 0xBC, 0x2A, 0x00, 0xB0, 0xD0, 0xF3, 0xF4, 0xAB);
        ///// <summary> From MFMediaType_Binary </summary>
        //public static readonly Guid Binary = new Guid(0x72178C25, 0xE45B, 0x11D5, 0xBC, 0x2A, 0x00, 0xB0, 0xD0, 0xF3, 0xF4, 0xAB);
        ///// <summary> From MFMediaType_FileTransfer </summary>
        //public static readonly Guid FileTransfer = new Guid(0x72178C26, 0xE45B, 0x11D5, 0xBC, 0x2A, 0x00, 0xB0, 0xD0, 0xF3, 0xF4, 0xAB);
        ///// <summary> From MFMediaType_Stream </summary>
        //public static readonly Guid Stream = new Guid(0xe436eb83, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70);

        //public static readonly Guid Base = new Guid(0x00000000, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        public static readonly Guid PCM = new Guid(0x00000001, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid Float = new Guid(0x0003, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid DTS = new Guid(0x0008, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid Dolby_AC3_SPDIF = new Guid(0x0092, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid DRM = new Guid(0x0009, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid WMAudioV8 = new Guid(0x0161, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid WMAudioV9 = new Guid(0x0162, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid WMAudio_Lossless = new Guid(0x0163, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid WMASPDIF = new Guid(0x0164, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid MSP1 = new Guid(0x000A, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        public static readonly Guid MP3 = new Guid(0x0055, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71); // MFAudioFormat_MP3
        //public static readonly Guid MPEG = new Guid(0x0050, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        public static readonly Guid AAC = new Guid(0x1610, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71); // MFAudioFormat_AAC
        //public static readonly Guid ADTS = new Guid(0x1600, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid AMR_NB = new Guid(0x7361, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid AMR_WB = new Guid(0x7362, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid AMR_WP = new Guid(0x7363, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        //// {00000000-767a-494d-b478-f29d25dc9037}       MFMPEG4Format_Base
        //public static readonly Guid MFMPEG4Format = new Guid(0x00000000, 0x767a, 0x494d, 0xb4, 0x78, 0xf2, 0x9d, 0x25, 0xdc, 0x90, 0x37);

        public static readonly Guid RGB32 = new Guid(22, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid ARGB32 = new Guid(21, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        public static readonly Guid RGB24 = new Guid(20, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid RGB555 = new Guid(24, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid RGB565 = new Guid(23, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        public static readonly Guid RGB8 = new Guid(41, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid AI44 = new FourCC("AI44").ToMediaSubtype();
        //public static readonly Guid AYUV = new FourCC("AYUV").ToMediaSubtype();
        //public static readonly Guid YUY2 = new FourCC("YUY2").ToMediaSubtype();
        //public static readonly Guid YVYU = new FourCC("YVYU").ToMediaSubtype();
        //public static readonly Guid YVU9 = new FourCC("YVU9").ToMediaSubtype();
        //public static readonly Guid UYVY = new FourCC("UYVY").ToMediaSubtype();
        //public static readonly Guid NV11 = new FourCC("NV11").ToMediaSubtype();
        //public static readonly Guid NV12 = new FourCC("NV12").ToMediaSubtype();
        //public static readonly Guid YV12 = new FourCC("YV12").ToMediaSubtype();
        //public static readonly Guid I420 = new FourCC("I420").ToMediaSubtype();
        //public static readonly Guid IYUV = new FourCC("IYUV").ToMediaSubtype();
        //public static readonly Guid Y210 = new FourCC("Y210").ToMediaSubtype();
        //public static readonly Guid Y216 = new FourCC("Y216").ToMediaSubtype();
        //public static readonly Guid Y410 = new FourCC("Y410").ToMediaSubtype();
        //public static readonly Guid Y416 = new FourCC("Y416").ToMediaSubtype();
        //public static readonly Guid Y41P = new FourCC("Y41P").ToMediaSubtype();
        //public static readonly Guid Y41T = new FourCC("Y41T").ToMediaSubtype();
        //public static readonly Guid Y42T = new FourCC("Y42T").ToMediaSubtype();
        //public static readonly Guid P210 = new FourCC("P210").ToMediaSubtype();
        //public static readonly Guid P216 = new FourCC("P216").ToMediaSubtype();
        //public static readonly Guid P010 = new FourCC("P010").ToMediaSubtype();
        //public static readonly Guid P016 = new FourCC("P016").ToMediaSubtype();
        //public static readonly Guid v210 = new FourCC("v210").ToMediaSubtype();
        //public static readonly Guid v216 = new FourCC("v216").ToMediaSubtype();
        //public static readonly Guid v410 = new FourCC("v410").ToMediaSubtype();
        //public static readonly Guid MP43 = new FourCC("MP43").ToMediaSubtype();
        //public static readonly Guid MP4S = new FourCC("MP4S").ToMediaSubtype();
        //public static readonly Guid M4S2 = new FourCC("M4S2").ToMediaSubtype();
        //public static readonly Guid MP4V = new FourCC("MP4V").ToMediaSubtype();
        //public static readonly Guid WMV1 = new FourCC("WMV1").ToMediaSubtype();
        //public static readonly Guid WMV2 = new FourCC("WMV2").ToMediaSubtype();
        //public static readonly Guid WMV3 = new FourCC("WMV3").ToMediaSubtype();
        //public static readonly Guid WVC1 = new FourCC("WVC1").ToMediaSubtype();
        //public static readonly Guid MSS1 = new FourCC("MSS1").ToMediaSubtype();
        //public static readonly Guid MSS2 = new FourCC("MSS2").ToMediaSubtype();
        //public static readonly Guid MPG1 = new FourCC("MPG1").ToMediaSubtype();
        //public static readonly Guid DVSL = new FourCC("dvsl").ToMediaSubtype();
        //public static readonly Guid DVSD = new FourCC("dvsd").ToMediaSubtype();
        //public static readonly Guid DVHD = new FourCC("dvhd").ToMediaSubtype();
        //public static readonly Guid DV25 = new FourCC("dv25").ToMediaSubtype();
        //public static readonly Guid DV50 = new FourCC("dv50").ToMediaSubtype();
        //public static readonly Guid DVH1 = new FourCC("dvh1").ToMediaSubtype();
        //public static readonly Guid DVC = new FourCC("dvc ").ToMediaSubtype();
        public static readonly Guid H264 = new FourCC("H264").ToMediaSubtype();
        //public static readonly Guid MJPG = new FourCC("MJPG").ToMediaSubtype();
        //public static readonly Guid O420 = new FourCC("420O").ToMediaSubtype();
        //public static readonly Guid HEVC = new FourCC("HEVC").ToMediaSubtype();
        //public static readonly Guid HEVC_ES = new FourCC("HEVS").ToMediaSubtype();

        //public static readonly Guid H265 = new FourCC("H265").ToMediaSubtype();
        //public static readonly Guid VP80 = new FourCC("VP80").ToMediaSubtype();
        //public static readonly Guid VP90 = new FourCC("VP90").ToMediaSubtype();

        //public static readonly Guid FLAC = new FourCC("F1AC").ToMediaSubtype();
        //public static readonly Guid ALAC = new FourCC("ALAC").ToMediaSubtype();

        //public static readonly Guid MPEG2 = new Guid(0xe06d8026, 0xdb46, 0x11cf, 0xb4, 0xd1, 0x00, 0x80, 0x5f, 0x6c, 0xbb, 0xea);
        //public static readonly Guid MFVideoFormat_H264_ES = new Guid(0x3f40f4f0, 0x5622, 0x4ff8, 0xb6, 0xd8, 0xa1, 0x7a, 0x58, 0x4b, 0xee, 0x5e);
        //public static readonly Guid MFAudioFormat_Dolby_AC3 = new Guid(0xe06d802c, 0xdb46, 0x11cf, 0xb4, 0xd1, 0x00, 0x80, 0x05f, 0x6c, 0xbb, 0xea);
        //public static readonly Guid MFAudioFormat_Dolby_DDPlus = new Guid(0xa7fb87af, 0x2d02, 0x42fb, 0xa4, 0xd4, 0x5, 0xcd, 0x93, 0x84, 0x3b, 0xdd);
        //// removed by MS - public static readonly Guid MFAudioFormat_QCELP = new Guid(0x5E7F6D41, 0xB115, 0x11D0, 0xBA, 0x91, 0x00, 0x80, 0x5F, 0xB4, 0xB9, 0x7E);

        //public static readonly Guid MFAudioFormat_Vorbis = new Guid(0x8D2FD10B, 0x5841, 0x4a6b, 0x89, 0x05, 0x58, 0x8F, 0xEC, 0x1A, 0xDE, 0xD9);
        //public static readonly Guid MFAudioFormat_LPCM = new Guid(0xe06d8032, 0xdb46, 0x11cf, 0xb4, 0xd1, 0x00, 0x80, 0x5f, 0x6c, 0xbb, 0xea);
        //public static readonly Guid MFAudioFormat_PCM_HDCP = new Guid(0xa5e7ff01, 0x8411, 0x4acc, 0xa8, 0x65, 0x5f, 0x49, 0x41, 0x28, 0x8d, 0x80);
        //public static readonly Guid MFAudioFormat_Dolby_AC3_HDCP = new Guid(0x97663a80, 0x8ffb, 0x4445, 0xa6, 0xba, 0x79, 0x2d, 0x90, 0x8f, 0x49, 0x7f);
        //public static readonly Guid MFAudioFormat_AAC_HDCP = new Guid(0x419bce76, 0x8b72, 0x400f, 0xad, 0xeb, 0x84, 0xb5, 0x7d, 0x63, 0x48, 0x4d);
        //public static readonly Guid MFAudioFormat_ADTS_HDCP = new Guid(0xda4963a3, 0x14d8, 0x4dcf, 0x92, 0xb7, 0x19, 0x3e, 0xb8, 0x43, 0x63, 0xdb);
        //public static readonly Guid MFAudioFormat_Base_HDCP = new Guid(0x3884b5bc, 0xe277, 0x43fd, 0x98, 0x3d, 0x03, 0x8a, 0xa8, 0xd9, 0xb6, 0x05);
        //public static readonly Guid MFVideoFormat_H264_HDCP = new Guid(0x5d0ce9dd, 0x9817, 0x49da, 0xbd, 0xfd, 0xf5, 0xf5, 0xb9, 0x8f, 0x18, 0xa6);
        //public static readonly Guid MFVideoFormat_Base_HDCP = new Guid(0xeac3b9d5, 0xbd14, 0x4237, 0x8f, 0x1f, 0xba, 0xb4, 0x28, 0xe4, 0x93, 0x12);

        //public static readonly Guid MPEG2Transport = new Guid(0xe06d8023, 0xdb46, 0x11cf, 0xb4, 0xd1, 0x00, 0x80, 0x5f, 0x6c, 0xbb, 0xea);
        //public static readonly Guid MPEG2Program = new Guid(0x263067d1, 0xd330, 0x45dc, 0xb6, 0x69, 0x34, 0xd9, 0x86, 0xe4, 0xe3, 0xe1);

        //public static readonly Guid V216_MS = new Guid(0x36313256, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        //public static readonly Guid V410_MS = new Guid(0x30313456, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
    }

    internal static class MFServices
    {
        //public static readonly Guid MF_TIMECODE_SERVICE = new Guid(0xa0d502a7, 0x0eb3, 0x4885, 0xb1, 0xb9, 0x9f, 0xeb, 0x0d, 0x08, 0x34, 0x54);
        public static readonly Guid MF_PROPERTY_HANDLER_SERVICE = new Guid(0xa3face02, 0x32b8, 0x41dd, 0x90, 0xe7, 0x5f, 0xef, 0x7c, 0x89, 0x91, 0xb5);
        //public static readonly Guid MF_METADATA_PROVIDER_SERVICE = new Guid(0xdb214084, 0x58a4, 0x4d2e, 0xb8, 0x4f, 0x6f, 0x75, 0x5b, 0x2f, 0x7a, 0xd);
        //public static readonly Guid MF_PMP_SERVER_CONTEXT = new Guid(0x2f00c910, 0xd2cf, 0x4278, 0x8b, 0x6a, 0xd0, 0x77, 0xfa, 0xc3, 0xa2, 0x5f);
        //public static readonly Guid MF_QUALITY_SERVICES = new Guid(0xb7e2be11, 0x2f96, 0x4640, 0xb5, 0x2c, 0x28, 0x23, 0x65, 0xbd, 0xf1, 0x6c);
        public static readonly Guid MF_RATE_CONTROL_SERVICE = new Guid(0x866fa297, 0xb802, 0x4bf8, 0x9d, 0xc9, 0x5e, 0x3b, 0x6a, 0x9f, 0x53, 0xc9);
        //public static readonly Guid MF_REMOTE_PROXY = new Guid(0x2f00c90e, 0xd2cf, 0x4278, 0x8b, 0x6a, 0xd0, 0x77, 0xfa, 0xc3, 0xa2, 0x5f);
        //public static readonly Guid MF_SAMI_SERVICE = new Guid(0x49a89ae7, 0xb4d9, 0x4ef2, 0xaa, 0x5c, 0xf6, 0x5a, 0x3e, 0x5, 0xae, 0x4e);
        //public static readonly Guid MF_SOURCE_PRESENTATION_PROVIDER_SERVICE = new Guid(0xe002aadc, 0xf4af, 0x4ee5, 0x98, 0x47, 0x05, 0x3e, 0xdf, 0x84, 0x04, 0x26);
        //public static readonly Guid MF_TOPONODE_ATTRIBUTE_EDITOR_SERVICE = new Guid(0x65656e1a, 0x077f, 0x4472, 0x83, 0xef, 0x31, 0x6f, 0x11, 0xd5, 0x08, 0x7a);
        //public static readonly Guid MF_WORKQUEUE_SERVICES = new Guid(0x8e37d489, 0x41e0, 0x413a, 0x90, 0x68, 0x28, 0x7c, 0x88, 0x6d, 0x8d, 0xda);
        //public static readonly Guid MFNET_SAVEJOB_SERVICE = new Guid(0xb85a587f, 0x3d02, 0x4e52, 0x95, 0x65, 0x55, 0xd3, 0xec, 0x1e, 0x7f, 0xf7);
        //public static readonly Guid MFNETSOURCE_STATISTICS_SERVICE = new Guid(0x3cb1f275, 0x0505, 0x4c5d, 0xae, 0x71, 0x0a, 0x55, 0x63, 0x44, 0xef, 0xa1);
        //public static readonly Guid MR_AUDIO_POLICY_SERVICE = new Guid(0x911fd737, 0x6775, 0x4ab0, 0xa6, 0x14, 0x29, 0x78, 0x62, 0xfd, 0xac, 0x88);
        public static readonly Guid MR_POLICY_VOLUME_SERVICE = new Guid(0x1abaa2ac, 0x9d3b, 0x47c6, 0xab, 0x48, 0xc5, 0x95, 0x6, 0xde, 0x78, 0x4d);
        public static readonly Guid MR_STREAM_VOLUME_SERVICE = new Guid(0xf8b5fa2f, 0x32ef, 0x46f5, 0xb1, 0x72, 0x13, 0x21, 0x21, 0x2f, 0xb2, 0xc4);
        public static readonly Guid MR_VIDEO_RENDER_SERVICE = new Guid(0x1092a86c, 0xab1a, 0x459a, 0xa3, 0x36, 0x83, 0x1f, 0xbc, 0x4d, 0x11, 0xff);
        public static readonly Guid MR_VIDEO_MIXER_SERVICE = new Guid(0x73cd2fc, 0x6cf4, 0x40b7, 0x88, 0x59, 0xe8, 0x95, 0x52, 0xc8, 0x41, 0xf8);
        //public static readonly Guid MR_VIDEO_ACCELERATION_SERVICE = new Guid(0xefef5175, 0x5c7d, 0x4ce2, 0xbb, 0xbd, 0x34, 0xff, 0x8b, 0xca, 0x65, 0x54);
        //public static readonly Guid MR_BUFFER_SERVICE = new Guid(0xa562248c, 0x9ac6, 0x4ffc, 0x9f, 0xba, 0x3a, 0xf8, 0xf8, 0xad, 0x1a, 0x4d);
        //public static readonly Guid MF_PMP_SERVICE = new Guid(0x2F00C90C, 0xD2CF, 0x4278, 0x8B, 0x6A, 0xD0, 0x77, 0xFA, 0xC3, 0xA2, 0x5F);
        //public static readonly Guid MF_LOCAL_MFT_REGISTRATION_SERVICE = new Guid(0xddf5cf9c, 0x4506, 0x45aa, 0xab, 0xf0, 0x6d, 0x5d, 0x94, 0xdd, 0x1b, 0x4a);
        //public static readonly Guid MF_BYTESTREAM_SERVICE = new Guid(0xab025e2b, 0x16d9, 0x4180, 0xa1, 0x27, 0xba, 0x6c, 0x70, 0x15, 0x61, 0x61);
        //public static readonly Guid MF_WRAPPED_BUFFER_SERVICE = new Guid(0xab544072, 0xc269, 0x4ebc, 0xa5, 0x52, 0x1c, 0x3b, 0x32, 0xbe, 0xd5, 0xca);
        //public static readonly Guid MF_WRAPPED_SAMPLE_SERVICE = new Guid(0x31f52bf2, 0xd03e, 0x4048, 0x80, 0xd0, 0x9c, 0x10, 0x46, 0xd8, 0x7c, 0x61);
        //public static readonly Guid MF_MEDIASOURCE_SERVICE = new Guid(0xf09992f7, 0x9fba, 0x4c4a, 0xa3, 0x7f, 0x8c, 0x47, 0xb4, 0xe1, 0xdf, 0xe7);
        //public static readonly Guid GUID_PlayToService = new Guid(0xf6a8ff9d, 0x9e14, 0x41c9, 0xbf, 0x0f, 0x12, 0x0a, 0x2b, 0x3c, 0xe1, 0x20);
        //public static readonly Guid GUID_NativeDeviceService = new Guid(0xef71e53c, 0x52f4, 0x43c5, 0xb8, 0x6a, 0xad, 0x6c, 0xb2, 0x16, 0xa6, 0x1e);
        //public static readonly Guid MF_WRAPPED_OBJECT = new Guid(0x2b182c4c, 0xd6ac, 0x49f4, 0x89, 0x15, 0xf7, 0x18, 0x87, 0xdb, 0x70, 0xcd);
        //public static readonly Guid MF_SCRUBBING_SERVICE = new Guid(0xDD0AC3D8, 0x40E3, 0x4128, 0xAC, 0x48, 0xC0, 0xAD, 0xD0, 0x67, 0xB7, 0x14);
        public static readonly Guid MR_CAPTURE_POLICY_VOLUME_SERVICE = new Guid(0x24030acd, 0x107a, 0x4265, 0x97, 0x5c, 0x41, 0x4e, 0x33, 0xe6, 0x5f, 0x2a);
    }

    internal static class PropertyKeys
    {
        // Device Info
        internal static PropertyKey PKEY_Device_Description = new PropertyKey { fmtid = new Guid(unchecked((int)0xa45c254e), unchecked((short)0xdf1c), 0x4efd, 0x80, 0x20, 0x67, 0xd1, 0x46, 0xa8, 0x50, 0xe0), pID = 2 };
        internal static PropertyKey PKEY_DeviceInterface_FriendlyName = new PropertyKey { fmtid = new Guid(unchecked(0x026e516e), 0xb814, 0x414b, 0x83, 0xcd, 0x85, 0x6d, 0x6f, 0xef, 0x48, 0x22), pID = 2 };

        // Shell Metadata (MediaTags)
        internal static PropertyKey PKEY_Music_Artist = new PropertyKey { fmtid = new Guid(unchecked(0x56A3372E), 0xCE9C, 0x11D2, 0x9F, 0x0E, 0x00, 0x60, 0x97, 0xC6, 0x86, 0xF6), pID = 2 };
        internal static PropertyKey PKEY_Music_AlbumArtist = new PropertyKey { fmtid = new Guid(unchecked(0x56A3372E), 0xCE9C, 0x11D2, 0x9F, 0x0E, 0x00, 0x60, 0x97, 0xC6, 0x86, 0xF6), pID = 13 };
        internal static PropertyKey PKEY_Title = new PropertyKey { fmtid = new Guid(unchecked(0xF29F85E0), 0x4FF9, 0x1068, 0xAB, 0x91, 0x08, 0x00, 0x2B, 0x27, 0xB3, 0xD9), pID = 2 };
        internal static PropertyKey PKEY_Music_AlbumTitle = new PropertyKey { fmtid = new Guid(unchecked(0x56A3372E), 0xCE9C, 0x11D2, 0x9F, 0x0E, 0x00, 0x60, 0x97, 0xC6, 0x86, 0xF6), pID = 4 };
        internal static PropertyKey PKEY_Music_Genre = new PropertyKey { fmtid = new Guid(unchecked(0x56A3372E), 0xCE9C, 0x11D2, 0x9F, 0x0E, 0x00, 0x60, 0x97, 0xC6, 0x86, 0xF6), pID = 11 };
        internal static PropertyKey PKEY_Media_Duration = new PropertyKey { fmtid = new Guid(unchecked(0x64440490), 0x4C8B, 0x11D1, 0x8B, 0x70, 0x08, 0x00, 0x36, 0xB1, 0x1A, 0x03), pID = 3 };
        internal static PropertyKey PKEY_Music_TrackNumber = new PropertyKey { fmtid = new Guid(unchecked(0x56A3372E), 0xCE9C, 0x11D2, 0x9F, 0x0E, 0x00, 0x60, 0x97, 0xC6, 0x86, 0xF6), pID = 7 };
        internal static PropertyKey PKEY_Media_Year = new PropertyKey { fmtid = new Guid(unchecked(0x56A3372E), 0xCE9C, 0x11D2, 0x9F, 0x0E, 0x00, 0x60, 0x97, 0xC6, 0x86, 0xF6), pID = 5 };
        internal static PropertyKey PKEY_ThumbnailStream = new PropertyKey { fmtid = new Guid(unchecked(0xF29F85E0), 0x4FF9, 0x1068, 0xAB, 0x91, 0x08, 0x00, 0x2B, 0x27, 0xB3, 0xD9), pID = 27 };
    }

    internal static class MFTranscodeContainerType
    {
        //internal static readonly Guid ASF = new Guid(0x430f6f6e, 0xb6bf, 0x4fc1, 0xa0, 0xbd, 0x9e, 0xe4, 0x6e, 0xee, 0x2a, 0xfb);
        internal static readonly Guid MPEG4 = new Guid(0xdc6cd05d, 0xb9d0, 0x40ef, 0xbd, 0x35, 0xfa, 0x62, 0x2c, 0x1a, 0xb2, 0x8a);
        internal static readonly Guid MP3 = new Guid(0xe438b912, 0x83f1, 0x4de6, 0x9e, 0x3a, 0x9f, 0xfb, 0xc6, 0xdd, 0x24, 0xd1);
        //internal static readonly Guid x3GP = new Guid(0x34c50167, 0x4472, 0x4f34, 0x9e, 0xa0, 0xc4, 0x9f, 0xba, 0xcf, 0x03, 0x7d);
        //internal static readonly Guid AC3 = new Guid(0x6d8d91c3, 0x8c91, 0x4ed1, 0x87, 0x42, 0x8c, 0x34, 0x7d, 0x5b, 0x44, 0xd0);
        //internal static readonly Guid ADTS = new Guid(0x132fd27d, 0x0f02, 0x43de, 0xa3, 0x01, 0x38, 0xfb, 0xbb, 0xb3, 0x83, 0x4e);
        //internal static readonly Guid MPEG2 = new Guid(0xbfc2dbf9, 0x7bb4, 0x4f8f, 0xaf, 0xde, 0xe1, 0x12, 0xc4, 0x4b, 0xa8, 0x82);
        //internal static readonly Guid WAVE = new Guid(0x64c3453c, 0x0f26, 0x4741, 0xbe, 0x63, 0x87, 0xbd, 0xf8, 0xbb, 0x93, 0x5b);
        //internal static readonly Guid AVI = new Guid(0x7edfe8af, 0x402f, 0x4d76, 0xa3, 0x3c, 0x61, 0x9f, 0xd1, 0x57, 0xd0, 0xf1);
        //internal static readonly Guid FMPEG4 = new Guid(0x9ba876f1, 0x419f, 0x4b77, 0xa1, 0xe0, 0x35, 0x95, 0x9d, 0x9d, 0x40, 0x4);
        //internal static readonly Guid FLAC = new Guid(0x31344aa3, 0x05a9, 0x42b5, 0x90, 0x1b, 0x8e, 0x9d, 0x42, 0x57, 0xf7, 0x5e);
        //internal static readonly Guid AMR = new Guid(0x25d5ad3, 0x621a, 0x475b, 0x96, 0x4d, 0x66, 0xb1, 0xc8, 0x24, 0xf0, 0x79);
    }

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


    /// <summary>
    /// ConstPropVariant is used for [In] parameters.  This is important since
    /// for [In] parameters, you must *not* clear the PropVariant.  The caller
    /// will need to do that himself.
    ///
    /// Likewise, if you want to store a copy of a ConstPropVariant, you should
    /// store it to a PropVariant using the PropVariant constructor that takes a
    /// ConstPropVariant.  If you try to store the ConstPropVariant, when the
    /// caller frees his copy, yours will no longer be valid.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal class ConstPropVariant : IDisposable
    {
        #region Declarations

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false), SuppressUnmanagedCodeSecurity]
        protected static extern void PropVariantCopy(
            [Out, MarshalAs(UnmanagedType.LPStruct)] PropVariant pvarDest,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant pvarSource
            );

        #endregion

        public enum VariantType : short
        {
            None = 0,
            Short = 2,
            Int32 = 3,
            Float = 4,
            Double = 5,
            IUnknown = 13,
            UByte = 17,
            UShort = 18,
            UInt32 = 19,
            Int64 = 20,
            UInt64 = 21,
            String = 31,
            Guid = 72,
            Blob = 0x1000 + 17,
            StringArray = 0x1000 + 31
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
        [StructLayout(LayoutKind.Sequential), UnmanagedName("BLOB")]
        protected struct Blob
        {
            public int cbSize;
            public IntPtr pBlobData;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
        [StructLayout(LayoutKind.Sequential), UnmanagedName("CALPWSTR")]
        protected struct CALPWstr
        {
            public int cElems;
            public IntPtr pElems;
        }

        #region Member variables

        [FieldOffset(0)]
        internal VariantType type;

        [FieldOffset(2)]
        protected short reserved1;

        [FieldOffset(4)]
        protected short reserved2;

        [FieldOffset(6)]
        protected short reserved3;

        [FieldOffset(8)]
        protected short iVal;

        [FieldOffset(8)]
        protected ushort uiVal;

        [FieldOffset(8)]
        protected byte bVal;

        [FieldOffset(8)]
        protected int intValue;

        [FieldOffset(8)]
        protected uint uintVal;

        [FieldOffset(8)]
        protected float fltVal;

        [FieldOffset(8)]
        internal long longValue;

        [FieldOffset(8)]
        protected ulong ulongValue;

        [FieldOffset(8)]
        protected double doubleValue;

        [FieldOffset(8)]
        protected Blob blobValue;

        [FieldOffset(8)]
        internal IntPtr ptr;

        [FieldOffset(8)]
        protected CALPWstr calpwstrVal;

        #endregion

        public ConstPropVariant()
            : this(VariantType.None)
        {
        }

        protected ConstPropVariant(VariantType v)
        {
            type = v;
        }

        public static explicit operator string(ConstPropVariant f)
        {
            return f.GetString();
        }

        public static explicit operator string[](ConstPropVariant f)
        {
            return f.GetStringArray();
        }

        public static explicit operator byte(ConstPropVariant f)
        {
            return f.GetUByte();
        }

        public static explicit operator short(ConstPropVariant f)
        {
            return f.GetShort();
        }

        //[CLSCompliant(false)]
        public static explicit operator ushort(ConstPropVariant f)
        {
            return f.GetUShort();
        }

        public static explicit operator int(ConstPropVariant f)
        {
            return f.GetInt();
        }

        //[CLSCompliant(false)]
        public static explicit operator uint(ConstPropVariant f)
        {
            return f.GetUInt();
        }

        public static explicit operator float(ConstPropVariant f)
        {
            return f.GetFloat();
        }

        public static explicit operator double(ConstPropVariant f)
        {
            return f.GetDouble();
        }

        public static explicit operator long(ConstPropVariant f)
        {
            return f.GetLong();
        }

        //[CLSCompliant(false)]
        public static explicit operator ulong(ConstPropVariant f)
        {
            return f.GetULong();
        }

        public static explicit operator Guid(ConstPropVariant f)
        {
            return f.GetGuid();
        }

        public static explicit operator byte[](ConstPropVariant f)
        {
            return f.GetBlob();
        }

        // I decided not to do implicits since perf is likely to be
        // better recycling the PropVariant, and the only way I can
        // see to support Implicit is to create a new PropVariant.
        // Also, since I can't free the previous instance, IUnknowns
        // will linger until the GC cleans up.  Not what I think I
        // want.

        public MFAttributeType GetMFAttributeType()
        {
            switch (type)
            {
                case VariantType.None:
                case VariantType.UInt32:
                case VariantType.UInt64:
                case VariantType.Double:
                case VariantType.Guid:
                case VariantType.String:
                case VariantType.Blob:
                case VariantType.IUnknown:
                    {
                        return (MFAttributeType)type;
                    }
                default:
                    {
                        //throw new Exception("Type is not a MFAttributeType");
                        return MFAttributeType.None;
                    }
            }
        }

        public VariantType GetVariantType()
        {
            return type;
        }

        public string[] GetStringArray()
        {
            if (type == VariantType.StringArray)
            {
                string[] sa;

                int iCount = calpwstrVal.cElems;
                sa = new string[iCount];

                for (int x = 0; x < iCount; x++)
                {
                    sa[x] = Marshal.PtrToStringUni(Marshal.ReadIntPtr(calpwstrVal.pElems, x * IntPtr.Size));
                }

                return sa;
            }
            //throw new ArgumentException("PropVariant contents not a string array");
            else return null;
        }

        public string GetString()
        {
            if (type == VariantType.String)
            {
                return Marshal.PtrToStringUni(ptr);
            }
            //throw new ArgumentException("PropVariant contents not a string");
            else return null;
        }

        public byte GetUByte()
        {
            if (type == VariantType.UByte)
            {
                return bVal;
            }
            //throw new ArgumentException("PropVariant contents not a byte");
            else return 0;
        }

        public short GetShort()
        {
            if (type == VariantType.Short)
            {
                return iVal;
            }
            //throw new ArgumentException("PropVariant contents not a Short");
            else return 0;
        }

        public ushort GetUShort()
        {
            if (type == VariantType.UShort)
            {
                return uiVal;
            }
            //throw new ArgumentException("PropVariant contents not a UShort");
            else return 0;
        }

        public int GetInt()
        {
            if (type == VariantType.Int32)
            {
                return intValue;
            }
            //throw new ArgumentException("PropVariant contents not an int32");
            else return 0;
        }

        public uint GetUInt()
        {
            if (type == VariantType.UInt32)
            {
                return uintVal;
            }
            //throw new ArgumentException("PropVariant contents not a uint32");
            else return 0;
        }

        public long GetLong()
        {
            if (type == VariantType.Int64)
            {
                return longValue;
            }
            //throw new ArgumentException("PropVariant contents not an int64");
            else return 0;
        }

        public ulong GetULong()
        {
            if (type == VariantType.UInt64)
            {
                return ulongValue;
            }
            //throw new ArgumentException("PropVariant contents not a uint64");
            else return 0;
        }

        public float GetFloat()
        {
            if (type == VariantType.Float)
            {
                return fltVal;
            }
            //throw new ArgumentException("PropVariant contents not a Float");
            else return 0;
        }

        public double GetDouble()
        {
            if (type == VariantType.Double)
            {
                return doubleValue;
            }
            //throw new ArgumentException("PropVariant contents not a double");
            else return 0;
        }

        public Guid GetGuid()
        {
            if (type == VariantType.Guid)
            {
                return (Guid)Marshal.PtrToStructure(ptr, typeof(Guid));
            }
            //throw new ArgumentException("PropVariant contents not a Guid");
            else return Guid.Empty;
        }

        public byte[] GetBlob()
        {
            if (type == VariantType.Blob)
            {
                byte[] b = new byte[blobValue.cbSize];

                if (blobValue.cbSize > 0)
                {
                    Marshal.Copy(blobValue.pBlobData, b, 0, blobValue.cbSize);
                }

                return b;
            }
            //throw new ArgumentException("PropVariant contents not a Blob");
            else return null;
        }

        public object GetBlob(Type t, int offset)
        {
            if (type == VariantType.Blob)
            {
                object o;

                if (blobValue.cbSize > offset)
                {
                    if (blobValue.cbSize >= Marshal.SizeOf(t) + offset)
                    {
                        //o = Marshal.PtrToStructure(blobValue.pBlobData + offset, t);
                        o = Marshal.PtrToStructure(new IntPtr(blobValue.pBlobData.ToInt64() + offset), t);
                    }
                    else
                    {
                        throw new ArgumentException("Blob wrong size");
                    }
                }
                else
                {
                    o = null;
                }

                return o;
            }
            //throw new ArgumentException("PropVariant contents not a Blob");
            else return null;
        }

        public object GetBlob(Type t)
        {
            return GetBlob(t, 0);
        }

        public object GetIUnknown()
        {
            if (type == VariantType.IUnknown)
            {
                if (ptr != IntPtr.Zero)
                {
                    return Marshal.GetObjectForIUnknown(ptr);
                }
                else
                {
                    return null;
                }
            }
            //throw new ArgumentException("PropVariant contents not an IUnknown");
            else return null;
        }

        public void Copy(PropVariant pdest)
        {
            if (pdest == null)
            {
                throw new Exception("Null PropVariant sent to Copy");
            }

            // Copy doesn't clear the dest.
            pdest.Clear();

            PropVariantCopy(pdest, this);
        }

        //public override string ToString()
        //{
        //    // This method is primarily intended for debugging so that a readable string will show
        //    // up in the output window
        //    string sRet;

        //    switch (type)
        //    {
        //        case VariantType.None:
        //            {
        //                sRet = "<Empty>";
        //                break;
        //            }

        //        case VariantType.Blob:
        //            {
        //                const string FormatString = "x2"; // Hex 2 digit format
        //                const int MaxEntries = 16;

        //                byte[] blob = GetBlob();

        //                // Number of bytes we're going to format
        //                int n = Math.Min(MaxEntries, blob.Length);

        //                if (n == 0)
        //                {
        //                    sRet = "<Empty Array>";
        //                }
        //                else
        //                {
        //                    // Only format the first MaxEntries bytes
        //                    sRet = blob[0].ToString(FormatString);
        //                    for (int i = 1; i < n; i++)
        //                    {
        //                        sRet += ',' + blob[i].ToString(FormatString);
        //                    }

        //                    // If the string is longer, add an indicator
        //                    if (blob.Length > n)
        //                    {
        //                        sRet += "...";
        //                    }
        //                }
        //                break;
        //            }

        //        case VariantType.Float:
        //            {
        //                sRet = GetFloat().ToString();
        //                break;
        //            }

        //        case VariantType.Double:
        //            {
        //                sRet = GetDouble().ToString();
        //                break;
        //            }

        //        case VariantType.Guid:
        //            {
        //                sRet = GetGuid().ToString();
        //                break;
        //            }

        //        case VariantType.IUnknown:
        //            {
        //                object o = GetIUnknown();
        //                if (o != null)
        //                {
        //                    sRet = GetIUnknown().ToString();
        //                }
        //                else
        //                {
        //                    sRet = "<null>";
        //                }
        //                break;
        //            }

        //        case VariantType.String:
        //            {
        //                sRet = GetString();
        //                break;
        //            }

        //        case VariantType.Short:
        //            {
        //                sRet = GetShort().ToString();
        //                break;
        //            }

        //        case VariantType.UByte:
        //            {
        //                sRet = GetUByte().ToString();
        //                break;
        //            }

        //        case VariantType.UShort:
        //            {
        //                sRet = GetUShort().ToString();
        //                break;
        //            }

        //        case VariantType.Int32:
        //            {
        //                sRet = GetInt().ToString();
        //                break;
        //            }

        //        case VariantType.UInt32:
        //            {
        //                sRet = GetUInt().ToString();
        //                break;
        //            }

        //        case VariantType.Int64:
        //            {
        //                sRet = GetLong().ToString();
        //                break;
        //            }

        //        case VariantType.UInt64:
        //            {
        //                sRet = GetULong().ToString();
        //                break;
        //            }

        //        case VariantType.StringArray:
        //            {
        //                sRet = "";
        //                foreach (string entry in GetStringArray())
        //                {
        //                    sRet += (sRet.Length == 0 ? "\"" : ",\"") + entry + '\"';
        //                }
        //                break;
        //            }
        //        default:
        //            {
        //                sRet = base.ToString();
        //                break;
        //            }
        //    }

        //    return sRet;
        //}

        //public override int GetHashCode()
        //{
        //    // Give a (slightly) better hash value in case someone uses PropVariants
        //    // in a hash table.
        //    int iRet;

        //    switch (type)
        //    {
        //        case VariantType.None:
        //            {
        //                iRet = base.GetHashCode();
        //                break;
        //            }

        //        case VariantType.Blob:
        //            {
        //                iRet = GetBlob().GetHashCode();
        //                break;
        //            }

        //        case VariantType.Float:
        //            {
        //                iRet = GetFloat().GetHashCode();
        //                break;
        //            }

        //        case VariantType.Double:
        //            {
        //                iRet = GetDouble().GetHashCode();
        //                break;
        //            }

        //        case VariantType.Guid:
        //            {
        //                iRet = GetGuid().GetHashCode();
        //                break;
        //            }

        //        case VariantType.IUnknown:
        //            {
        //                iRet = GetIUnknown().GetHashCode();
        //                break;
        //            }

        //        case VariantType.String:
        //            {
        //                iRet = GetString().GetHashCode();
        //                break;
        //            }

        //        case VariantType.UByte:
        //            {
        //                iRet = GetUByte().GetHashCode();
        //                break;
        //            }

        //        case VariantType.Short:
        //            {
        //                iRet = GetShort().GetHashCode();
        //                break;
        //            }

        //        case VariantType.UShort:
        //            {
        //                iRet = GetUShort().GetHashCode();
        //                break;
        //            }

        //        case VariantType.Int32:
        //            {
        //                iRet = GetInt().GetHashCode();
        //                break;
        //            }

        //        case VariantType.UInt32:
        //            {
        //                iRet = GetUInt().GetHashCode();
        //                break;
        //            }

        //        case VariantType.Int64:
        //            {
        //                iRet = GetLong().GetHashCode();
        //                break;
        //            }

        //        case VariantType.UInt64:
        //            {
        //                iRet = GetULong().GetHashCode();
        //                break;
        //            }

        //        case VariantType.StringArray:
        //            {
        //                iRet = GetStringArray().GetHashCode();
        //                break;
        //            }
        //        default:
        //            {
        //                iRet = base.GetHashCode();
        //                break;
        //            }
        //    }

        //    return iRet;
        //}

        //public override bool Equals(object obj)
        //{
        //    bool bRet;
        //    PropVariant p = obj as PropVariant;

        //    if ((((object)p) == null) || (p.type != type))
        //    {
        //        bRet = false;
        //    }
        //    else
        //    {
        //        switch (type)
        //        {
        //            case VariantType.None:
        //                {
        //                    bRet = true;
        //                    break;
        //                }

        //            case VariantType.Blob:
        //                {
        //                    byte[] b1;
        //                    byte[] b2;

        //                    b1 = GetBlob();
        //                    b2 = p.GetBlob();

        //                    if (b1.Length == b2.Length)
        //                    {
        //                        bRet = true;
        //                        for (int x = 0; x < b1.Length; x++)
        //                        {
        //                            if (b1[x] != b2[x])
        //                            {
        //                                bRet = false;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        bRet = false;
        //                    }
        //                    break;
        //                }

        //            case VariantType.Float:
        //                {
        //                    bRet = GetFloat() == p.GetFloat();
        //                    break;
        //                }

        //            case VariantType.Double:
        //                {
        //                    bRet = GetDouble() == p.GetDouble();
        //                    break;
        //                }

        //            case VariantType.Guid:
        //                {
        //                    bRet = GetGuid() == p.GetGuid();
        //                    break;
        //                }

        //            case VariantType.IUnknown:
        //                {
        //                    bRet = GetIUnknown() == p.GetIUnknown();
        //                    break;
        //                }

        //            case VariantType.String:
        //                {
        //                    bRet = GetString() == p.GetString();
        //                    break;
        //                }

        //            case VariantType.UByte:
        //                {
        //                    bRet = GetUByte() == p.GetUByte();
        //                    break;
        //                }

        //            case VariantType.Short:
        //                {
        //                    bRet = GetShort() == p.GetShort();
        //                    break;
        //                }

        //            case VariantType.UShort:
        //                {
        //                    bRet = GetUShort() == p.GetUShort();
        //                    break;
        //                }

        //            case VariantType.Int32:
        //                {
        //                    bRet = GetInt() == p.GetInt();
        //                    break;
        //                }

        //            case VariantType.UInt32:
        //                {
        //                    bRet = GetUInt() == p.GetUInt();
        //                    break;
        //                }

        //            case VariantType.Int64:
        //                {
        //                    bRet = GetLong() == p.GetLong();
        //                    break;
        //                }

        //            case VariantType.UInt64:
        //                {
        //                    bRet = GetULong() == p.GetULong();
        //                    break;
        //                }

        //            case VariantType.StringArray:
        //                {
        //                    string[] sa1;
        //                    string[] sa2;

        //                    sa1 = GetStringArray();
        //                    sa2 = p.GetStringArray();

        //                    if (sa1.Length == sa2.Length)
        //                    {
        //                        bRet = true;
        //                        for (int x = 0; x < sa1.Length; x++)
        //                        {
        //                            if (sa1[x] != sa2[x])
        //                            {
        //                                bRet = false;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        bRet = false;
        //                    }
        //                    break;
        //                }
        //            default:
        //                {
        //                    bRet = base.Equals(obj);
        //                    break;
        //                }
        //        }
        //    }

        //    return bRet;
        //}

        //public static bool operator ==(ConstPropVariant pv1, ConstPropVariant pv2)
        //{
        //    // If both are null, or both are same instance, return true.
        //    if (System.Object.ReferenceEquals(pv1, pv2))
        //    {
        //        return true;
        //    }

        //    // If one is null, but not both, return false.
        //    if (((object)pv1 == null) || ((object)pv2 == null))
        //    {
        //        return false;
        //    }

        //    return pv1.Equals(pv2);
        //}

        //public static bool operator !=(ConstPropVariant pv1, ConstPropVariant pv2)
        //{
        //    return !(pv1 == pv2);
        //}

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2216:DisposableTypesShouldDeclareFinalizer")]
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // If we are a ConstPropVariant, we must *not* call PropVariantClear.  That
            // would release the *caller's* copy of the data, which would probably make
            // him cranky.  If we are a PropVariant, the PropVariant.Dispose gets called
            // as well, which *does* do a PropVariantClear.
            type = VariantType.None;
#if DEBUG
            longValue = 0;
#endif
        }

        #endregion
    }

    [StructLayout(LayoutKind.Sequential), UnmanagedName("DXVA2_ProcAmpValues")]
    internal sealed class DXVA2ProcAmpValues
    {
        public int Brightness;
        public int Contrast;
        public int Hue;
        public int Saturation;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class FourCC
    {
        private int m_fourCC;

        public FourCC(string fcc)
        {
            if (fcc.Length != 4)
            {
                throw new ArgumentException(fcc + " is not a valid FourCC");
            }

            byte[] asc = Encoding.ASCII.GetBytes(fcc);

            LoadFromBytes(asc[0], asc[1], asc[2], asc[3]);
        }

        public FourCC(char a, char b, char c, char d)
        {
            LoadFromBytes((byte)a, (byte)b, (byte)c, (byte)d);
        }

        public FourCC(int fcc)
        {
            m_fourCC = fcc;
        }

        public FourCC(byte a, byte b, byte c, byte d)
        {
            LoadFromBytes(a, b, c, d);
        }

        public FourCC(Guid g)
        {
            if (!IsA4ccSubtype(g))
            {
                throw new Exception("Not a FourCC Guid");
            }

            byte[] asc;
            asc = g.ToByteArray();

            LoadFromBytes(asc[0], asc[1], asc[2], asc[3]);
        }

        public void LoadFromBytes(byte a, byte b, byte c, byte d)
        {
            m_fourCC = a | (b << 8) | (c << 16) | (d << 24);
        }

        public int ToInt32()
        {
            return m_fourCC;
        }

        public static explicit operator int(FourCC f)
        {
            return f.ToInt32();
        }

        public static explicit operator Guid(FourCC f)
        {
            return f.ToMediaSubtype();
        }

        public Guid ToMediaSubtype()
        {
            return new Guid(m_fourCC, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        }

        public static bool operator ==(FourCC fcc1, FourCC fcc2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(fcc1, fcc2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)fcc1 == null) || ((object)fcc2 == null))
            {
                return false;
            }

            return fcc1.m_fourCC == fcc2.m_fourCC;
        }

        public static bool operator !=(FourCC fcc1, FourCC fcc2)
        {
            return !(fcc1 == fcc2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FourCC))
                return false;

            return (obj as FourCC).m_fourCC == m_fourCC;
        }

        public override int GetHashCode()
        {
            return m_fourCC.GetHashCode();
        }

        public override string ToString()
        {
            char c;
            char[] ca = new char[4];

            c = Convert.ToChar(m_fourCC & 255);
            if (!Char.IsLetterOrDigit(c))
            {
                c = ' ';
            }
            ca[0] = c;

            c = Convert.ToChar((m_fourCC >> 8) & 255);
            if (!Char.IsLetterOrDigit(c))
            {
                c = ' ';
            }
            ca[1] = c;

            c = Convert.ToChar((m_fourCC >> 16) & 255);
            if (!Char.IsLetterOrDigit(c))
            {
                c = ' ';
            }
            ca[2] = c;

            c = Convert.ToChar((m_fourCC >> 24) & 255);
            if (!Char.IsLetterOrDigit(c))
            {
                c = ' ';
            }
            ca[3] = c;

            string s = new string(ca);

            return s;
        }

        public static bool IsA4ccSubtype(Guid g)
        {
            return (g.ToString().EndsWith("-0000-0010-8000-00aa00389b71"));
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class MfFloat
    {
        private float Value;

        public MfFloat()
            : this(0)
        {
        }

        public MfFloat(float v)
        {
            Value = v;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator float(MfFloat l)
        {
            return l.Value;
        }

        public static implicit operator MfFloat(float l)
        {
            return new MfFloat(l);
        }
    }

    /// <summary>
    /// MFRect is a managed representation of the Win32 RECT structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class MFRect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        /// <summary>
        /// Empty contructor. Initialize all fields to 0
        /// </summary>
        public MFRect()
        {
        }

        /// <summary>
        /// A parametred constructor. Initialize fields with given values.
        /// </summary>
        /// <param name="l">the left value</param>
        /// <param name="t">the top value</param>
        /// <param name="r">the right value</param>
        /// <param name="b">the bottom value</param>
        public MFRect(int l, int t, int r, int b)
        {
            left = l;
            top = t;
            right = r;
            bottom = b;
        }

        /// <summary>
        /// A parametred constructor. Initialize fields with a given <see cref="System.Drawing.Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">A <see cref="System.Drawing.Rectangle"/></param>
        /// <remarks>
        /// Warning, MFRect define a rectangle by defining two of his corners and <see cref="System.Drawing.Rectangle"/> define a rectangle with his upper/left corner, his width and his height.
        /// </remarks>
        public MFRect(Rectangle rectangle)
            : this(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
        {
        }

        /// <summary>
        /// Provide de string representation of this MFRect instance
        /// </summary>
        /// <returns>A string formated like this : [left, top - right, bottom]</returns>
        public override string ToString()
        {
            return string.Format("[{0}, {1}] - [{2}, {3}]", left, top, right, bottom);
        }

        public override int GetHashCode()
        {
            return left.GetHashCode() |
                top.GetHashCode() |
                right.GetHashCode() |
                bottom.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is MFRect)
            {
                MFRect cmp = (MFRect)obj;

                return right == cmp.right && bottom == cmp.bottom && left == cmp.left && top == cmp.top;
            }

            if (obj is Rectangle)
            {
                Rectangle cmp = (Rectangle)obj;

                return right == cmp.Right && bottom == cmp.Bottom && left == cmp.Left && top == cmp.Top;
            }

            return false;
        }

        /// <summary>
        /// Checks to see if the rectangle is empty
        /// </summary>
        /// <returns>Returns true if the rectangle is empty</returns>
        public bool IsEmpty()
        {
            return (right <= left || bottom <= top);
        }

        /// <summary>
        /// Define implicit cast between MFRect and System.Drawing.Rectangle for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="MFRect.ToRectangle"/> for similar functionality.
        /// <code>
        ///   // Define a new Rectangle instance
        ///   Rectangle r = new Rectangle(0, 0, 100, 100);
        ///   // Do implicit cast between Rectangle and MFRect
        ///   MFRect mfR = r;
        ///
        ///   Console.WriteLine(mfR.ToString());
        /// </code>
        /// </summary>
        /// <param name="r">a MFRect to be cast</param>
        /// <returns>A casted System.Drawing.Rectangle</returns>
        public static implicit operator Rectangle(MFRect r)
        {
            return r.ToRectangle();
        }

        /// <summary>
        /// Define implicit cast between System.Drawing.Rectangle and MFRect for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="MFRect.FromRectangle"/> for similar functionality.
        /// <code>
        ///   // Define a new MFRect instance
        ///   MFRect mfR = new MFRect(0, 0, 100, 100);
        ///   // Do implicit cast between MFRect and Rectangle
        ///   Rectangle r = mfR;
        ///
        ///   Console.WriteLine(r.ToString());
        /// </code>
        /// </summary>
        /// <param name="r">A System.Drawing.Rectangle to be cast</param>
        /// <returns>A casted MFRect</returns>
        public static implicit operator MFRect(Rectangle r)
        {
            return new MFRect(r);
        }

        /// <summary>
        /// Get the System.Drawing.Rectangle equivalent to this MFRect instance.
        /// </summary>
        /// <returns>A System.Drawing.Rectangle</returns>
        public Rectangle ToRectangle()
        {
            return new Rectangle(left, top, (right - left), (bottom - top));
        }

        /// <summary>
        /// Get a new MFRect instance for a given <see cref="System.Drawing.Rectangle"/>
        /// </summary>
        /// <param name="r">The <see cref="System.Drawing.Rectangle"/> used to initialize this new MFGuid</param>
        /// <returns>A new instance of MFGuid</returns>
        public static MFRect FromRectangle(Rectangle r)
        {
            return new MFRect(r);
        }

        /// <summary>
        /// Copy the members from an MFRect into this object
        /// </summary>
        /// <param name="from">The rectangle from which to copy the values.</param>
        public void CopyFrom(MFRect from)
        {
            left = from.left;
            top = from.top;
            right = from.right;
            bottom = from.bottom;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4), UnmanagedName("SIZE")]
    internal sealed class MFSize
    {
        public int cx;
        public int cy;

        public MFSize()
        {
            cx = 0;
            cy = 0;
        }

        public MFSize(int iWidth, int iHeight)
        {
            cx = iWidth;
            cy = iHeight;
        }

        public int Width
        {
            get
            {
                return cx;
            }
            set
            {
                cx = value;
            }
        }
        public int Height
        {
            get
            {
                return cy;
            }
            set
            {
                cy = value;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4), UnmanagedName("MFVideoNormalizedRect")]
    internal sealed class MFVideoNormalizedRect
    {
        public float left;
        public float top;
        public float right;
        public float bottom;

        public MFVideoNormalizedRect()
        {
        }

        public MFVideoNormalizedRect(float l, float t, float r, float b)
        {
            left = l;
            top = t;
            right = r;
            bottom = b;
        }

        public override string ToString()
        {
            return string.Format("left = {0}, top = {1}, right = {2}, bottom = {3}", left, top, right, bottom);
        }

        public override int GetHashCode()
        {
            return left.GetHashCode() |
                top.GetHashCode() |
                right.GetHashCode() |
                bottom.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is MFVideoNormalizedRect)
            {
#pragma warning disable IDE0020 // Use pattern matching
                MFVideoNormalizedRect cmp = (MFVideoNormalizedRect)obj;
#pragma warning restore IDE0020 // Use pattern matching

                return right == cmp.right && bottom == cmp.bottom && left == cmp.left && top == cmp.top;
            }

            return false;
        }

        public bool IsEmpty()
        {
            return (right <= left || bottom <= top);
        }

        public void CopyFrom(MFVideoNormalizedRect from)
        {
            left = from.left;
            top = from.top;
            right = from.right;
            bottom = from.bottom;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4), UnmanagedName("PROPERTYKEY")]
    internal sealed class PropertyKey
    {
        public Guid fmtid;
        public int pID;

        public PropertyKey()
        {
        }

        public PropertyKey(Guid f, int p)
        {
            fmtid = f;
            pID = p;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    internal class PropVariant : ConstPropVariant
    {
        #region Declarations

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false), SuppressUnmanagedCodeSecurity]
        protected static extern void PropVariantClear([In, MarshalAs(UnmanagedType.LPStruct)] PropVariant pvar);

        #endregion

        public PropVariant()
            : base(VariantType.None)
        {
        }

        public PropVariant(string value)
            : base(VariantType.String)
        {
            ptr = Marshal.StringToCoTaskMemUni(value);
        }

        public PropVariant(string[] value)
            : base(VariantType.StringArray)
        {
            calpwstrVal.cElems = value.Length;
            calpwstrVal.pElems = Marshal.AllocCoTaskMem(IntPtr.Size * value.Length);

            for (int x = 0; x < value.Length; x++)
            {
                Marshal.WriteIntPtr(calpwstrVal.pElems, x * IntPtr.Size, Marshal.StringToCoTaskMemUni(value[x]));
            }
        }

        public PropVariant(byte value)
            : base(VariantType.UByte)
        {
            bVal = value;
        }

        public PropVariant(short value)
            : base(VariantType.Short)
        {
            iVal = value;
        }

        public PropVariant(ushort value)
            : base(VariantType.UShort)
        {
            uiVal = value;
        }

        public PropVariant(int value)
            : base(VariantType.Int32)
        {
            intValue = value;
        }

        public PropVariant(uint value)
            : base(VariantType.UInt32)
        {
            uintVal = value;
        }

        public PropVariant(float value)
            : base(VariantType.Float)
        {
            fltVal = value;
        }

        public PropVariant(double value)
            : base(VariantType.Double)
        {
            doubleValue = value;
        }

        public PropVariant(long value)
            : base(VariantType.Int64)
        {
            longValue = value;
        }

        public PropVariant(ulong value)
            : base(VariantType.UInt64)
        {
            ulongValue = value;
        }

        public PropVariant(Guid value)
            : base(VariantType.Guid)
        {
            ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(value));
            Marshal.StructureToPtr(value, ptr, false);
        }

        public PropVariant(byte[] value)
            : base(VariantType.Blob)
        {
            blobValue.cbSize = value.Length;
            blobValue.pBlobData = Marshal.AllocCoTaskMem(value.Length);
            Marshal.Copy(value, 0, blobValue.pBlobData, value.Length);
        }

        public PropVariant(object value)
            : base(VariantType.IUnknown)
        {
            if (value == null)
            {
                ptr = IntPtr.Zero;
            }
            else if (Marshal.IsComObject(value))
            {
                ptr = Marshal.GetIUnknownForObject(value);
            }
            else
            {
                type = VariantType.Blob;

                blobValue.cbSize = Marshal.SizeOf(value);
                blobValue.pBlobData = Marshal.AllocCoTaskMem(blobValue.cbSize);

                Marshal.StructureToPtr(value, blobValue.pBlobData, false);
            }
        }

        public PropVariant(IntPtr value)
            : base(VariantType.None)
        {
            Marshal.PtrToStructure(value, this);
        }

        public PropVariant(ConstPropVariant value)
        {
            if (value != null)
            {
                PropVariantCopy(this, value);
            }
            else
            {
                throw new NullReferenceException("null passed to PropVariant constructor");
            }
        }

        ~PropVariant()
        {
            Dispose(false);
        }

        public void Clear()
        {
            PropVariantClear(this);
        }

        #region IDisposable Members

        protected override void Dispose(bool disposing)
        {
            Clear();
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        #endregion
    }

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
    internal sealed class PVMarshaler : ICustomMarshaler
    {
        private class MyProps
        {
            public PropVariant m_obj;
            public IntPtr m_ptr;

            private int m_InProcsss;
            private bool m_IAllocated;
            private MyProps m_Parent = null;

            [ThreadStatic]
            private static MyProps[] m_CurrentProps;

            public int GetStage()
            {
                return m_InProcsss;
            }

            public void StageComplete()
            {
                m_InProcsss++;
            }

            public static MyProps AddLayer(int iIndex)
            {
                MyProps p = new MyProps();
                p.m_Parent = m_CurrentProps[iIndex];
                m_CurrentProps[iIndex] = p;

                return p;
            }

            public static void SplitLayer(int iIndex)
            {
                MyProps t = AddLayer(iIndex);
                MyProps p = t.m_Parent;

                t.m_InProcsss = 1;
                t.m_ptr = p.m_ptr;
                t.m_obj = p.m_obj;

                p.m_InProcsss = 1;
            }

            public static MyProps GetTop(int iIndex)
            {
                // If the member hasn't been initialized, do it now.  And no, we can't
                // do this in the PVMarshaler constructor, since the constructor may 
                // have been called on a different thread.
                if (m_CurrentProps == null)
                {
                    m_CurrentProps = new MyProps[MaxArgs];
                    for (int x = 0; x < MaxArgs; x++)
                    {
                        m_CurrentProps[x] = new MyProps();
                    }
                }
                return m_CurrentProps[iIndex];
            }

            public void Clear(int iIndex)
            {
                if (m_IAllocated)
                {
                    Marshal.FreeCoTaskMem(m_ptr);
                    m_IAllocated = false;
                }
                if (m_Parent == null)
                {
                    // Never delete the last entry.
                    m_InProcsss = 0;
                    m_obj = null;
                    m_ptr = IntPtr.Zero;
                }
                else
                {
                    m_obj = null;
                    m_CurrentProps[iIndex] = m_Parent;
                }
            }

            public IntPtr Alloc(int iSize)
            {
                IntPtr ip = Marshal.AllocCoTaskMem(iSize);
                m_IAllocated = true;
                return ip;
            }
        }

        private readonly int m_Index;

        // Max number of arguments in a single method call that can use
        // PVMarshaler.
        private const int MaxArgs = 2;

        private PVMarshaler(string cookie)
        {
            int iLen = cookie.Length;

            // On methods that have more than 1 PVMarshaler on a
            // single method, the cookie is in the form:
            // InterfaceName.MethodName.0 & InterfaceName.MethodName.1.
            if (cookie[iLen - 2] != '.')
            {
                m_Index = 0;
            }
            else
            {
                m_Index = int.Parse(cookie.Substring(iLen - 1));
            }
        }

        public IntPtr MarshalManagedToNative(object managedObj)
        {

            MyProps t = MyProps.GetTop(m_Index);

            switch (t.GetStage())
            {
                case 0:
                    {
                        // We are just starting a "Managed calling unmanaged"
                        // call.

                        // Cast the object back to a PropVariant and save it
                        // for use in MarshalNativeToManaged.
                        t.m_obj = managedObj as PropVariant;

                        // This could happen if (somehow) managedObj isn't a
                        // PropVariant.  During normal marshaling, the custom
                        // marshaler doesn't get called if the parameter is
                        // null.

                        // Release any memory currently allocated in the
                        // PropVariant.  In theory, the (managed) caller
                        // should have done this before making the call that
                        // got us here, but .Net programmers don't generally
                        // think that way.  To avoid any leaks, do it for them.
                        t.m_obj.Clear();

                        // Create an appropriately sized buffer (varies from
                        // x86 to x64).
                        int iSize = GetNativeDataSize();
                        t.m_ptr = t.Alloc(iSize);

                        // Copy in the (empty) PropVariant.  In theory we could
                        // just zero out the first 2 bytes (the VariantType),
                        // but since PropVariantClear wipes the whole struct,
                        // that's what we do here to be safe.
                        Marshal.StructureToPtr(t.m_obj, t.m_ptr, false);

                        break;
                    }
                case 1:
                    {
                        if (!System.Object.ReferenceEquals(t.m_obj, managedObj))
                        {
                            // If we get here, we have already received a call
                            // to MarshalNativeToManaged where we created a
                            // PropVariant and stored it into t.m_obj.  But
                            // the object we just got passed here isn't the
                            // same one.  Therefore instead of being the second
                            // half of an "Unmanaged calling managed" (as
                            // m_InProcsss led us to believe), this is really
                            // the first half of a nested "Managed calling
                            // unmanaged" (see Recursion in the comments at the
                            // top of this class).  Add another layer.
                            MyProps.AddLayer(m_Index);

                            // Try this call again now that we have fixed
                            // m_CurrentProps.
                            return MarshalManagedToNative(managedObj);
                        }

                        // This is (probably) the second half of "Unmanaged
                        // calling managed."  However, it could be the first
                        // half of a nested usage of PropVariants.  If it is a
                        // nested, we'll eventually figure that out in case 2.

                        // Copy the data from the managed object into the
                        // native pointer that we received in
                        // MarshalNativeToManaged.
                        Marshal.StructureToPtr(t.m_obj, t.m_ptr, false);

                        break;
                    }
                case 2:
                    {
                        // Apparently this is 'part 3' of a 2 part call.  Which
                        // means we are doing a nested call.  Normally we would
                        // catch the fact that this is a nested call with the
                        // ReferenceEquals check above.  However, if the same
                        // PropVariant instance is being passed thru again, we
                        // end up here.
                        // So, add a layer.
                        MyProps.SplitLayer(m_Index);

                        // Try this call again now that we have fixed
                        // m_CurrentProps.
                        return MarshalManagedToNative(managedObj);
                    }
                default:
                    {
                        Environment.FailFast("Something horrible has " +
                                             "happened, probaby due to " +
                                             "marshaling of nested " +
                                             "PropVariant calls.");
                        break;
                    }
            }
            t.StageComplete();

            return t.m_ptr;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            MyProps t = MyProps.GetTop(m_Index);

            switch (t.GetStage())
            {
                case 0:
                    {
                        // We are just starting a "Unmanaged calling managed"
                        // call.

                        // Caller should have cleared variant before calling
                        // us.  Might be acceptable for types *other* than
                        // IUnknown, String, Blob and StringArray, but it is
                        // still bad design.  We're checking for it, but we
                        // work around it.

                        // Read the 16bit VariantType.

                        // Create an empty managed PropVariant without using
                        // pNativeData.
                        t.m_obj = new PropVariant();

                        // Save the pointer for use in MarshalManagedToNative.
                        t.m_ptr = pNativeData;

                        break;
                    }
                case 1:
                    {
                        if (t.m_ptr != pNativeData)
                        {
                            // If we get here, we have already received a call
                            // to MarshalManagedToNative where we created an
                            // IntPtr and stored it into t.m_ptr.  But the
                            // value we just got passed here isn't the same
                            // one.  Therefore instead of being the second half
                            // of a "Managed calling unmanaged" (as m_InProcsss
                            // led us to believe) this is really the first half
                            // of a nested "Unmanaged calling managed" (see
                            // Recursion in the comments at the top of this
                            // class).  Add another layer.
                            MyProps.AddLayer(m_Index);

                            // Try this call again now that we have fixed
                            // m_CurrentProps.
                            return MarshalNativeToManaged(pNativeData);
                        }

                        // This is (probably) the second half of "Managed
                        // calling unmanaged."  However, it could be the first
                        // half of a nested usage of PropVariants.  If it is a
                        // nested, we'll eventually figure that out in case 2.

                        // Copy the data from the native pointer into the
                        // managed object that we received in
                        // MarshalManagedToNative.
                        Marshal.PtrToStructure(pNativeData, t.m_obj);

                        break;
                    }
                case 2:
                    {
                        // Apparently this is 'part 3' of a 2 part call.  Which
                        // means we are doing a nested call.  Normally we would
                        // catch the fact that this is a nested call with the
                        // (t.m_ptr != pNativeData) check above.  However, if
                        // the same PropVariant instance is being passed thru
                        // again, we end up here.  So, add a layer.
                        MyProps.SplitLayer(m_Index);

                        // Try this call again now that we have fixed
                        // m_CurrentProps.
                        return MarshalNativeToManaged(pNativeData);
                    }
                default:
                    {
                        Environment.FailFast("Something horrible has " +
                                             "happened, probaby due to " +
                                             "marshaling of nested " +
                                             "PropVariant calls.");
                        break;
                    }
            }
            t.StageComplete();

            return t.m_obj;
        }

        public void CleanUpManagedData(object ManagedObj)
        {
            // Note that if there are nested calls, one of the Cleanup*Data
            // methods will be called at the end of each pair:

            // MarshalNativeToManaged
            // MarshalManagedToNative
            // CleanUpManagedData
            //
            // or for recursion:
            //
            // MarshalManagedToNative 1
            // MarshalNativeToManaged 2
            // MarshalManagedToNative 2
            // CleanUpManagedData     2
            // MarshalNativeToManaged 1
            // CleanUpNativeData      1

            // Clear() either pops an entry, or clears
            // the values for the next call.
            MyProps t = MyProps.GetTop(m_Index);
            t.Clear(m_Index);
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            // Clear() either pops an entry, or clears
            // the values for the next call.
            MyProps t = MyProps.GetTop(m_Index);
            t.Clear(m_Index);
        }

        // The number of bytes to marshal.  Size varies between x86 and x64.
        public int GetNativeDataSize()
        {
            return Marshal.SizeOf(typeof(PropVariant));
        }

        // This method is called by interop to create the custom marshaler.
        // The (optional) cookie is the value specified in
        // MarshalCookie="asdf", or "" if none is specified.
#pragma warning disable IDE0051 // Remove unused private members
        private static ICustomMarshaler GetInstance(string cookie)
#pragma warning restore IDE0051 // Remove unused private members
        {
            return new PVMarshaler(cookie);
        }
    }

    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Class)]
    internal sealed class UnmanagedNameAttribute : System.Attribute
    {
        private string m_Name;

        public UnmanagedNameAttribute(string s)
        {
            m_Name = s;
        }

        public override string ToString()
        {
            return m_Name;
        }
    }

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

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("7FEE9E9A-4A89-47A6-899C-B6A53A70FB67"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFActivate : IMFAttributes
    {
        #region IMFAttributes methods

        [PreserveSig]
        new HResult GetItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFActivate.GetItem", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
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
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFActivate.GetItemByIndex", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
            );

        [PreserveSig]
        new HResult CopyAllItems(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest
            );

        #endregion

        [PreserveSig]
        HResult ActivateObject(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out object ppv
            );

        [PreserveSig]
        HResult ShutdownObject();

        [PreserveSig]
        HResult DetachObject();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("A27003CF-2354-4F2A-8D6A-AB7CFF15437E")]
    internal interface IMFAsyncCallback
    {
        [PreserveSig]
        HResult GetParameters(
            out MFASync pdwFlags,
            out MFAsyncCallbackQueue pdwQueue
            );

        [PreserveSig]
        HResult Invoke(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncResult pAsyncResult
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("AC6B7889-0740-4D51-8619-905994A55CC6")]
    internal interface IMFAsyncResult
    {
        [PreserveSig]
        HResult GetState(
            [MarshalAs(UnmanagedType.IUnknown)] out object ppunkState
            );

        [PreserveSig]
        HResult GetStatus();

        [PreserveSig]
        HResult SetStatus(
            [In, MarshalAs(UnmanagedType.Error)] HResult hrStatus
            );

        [PreserveSig]
        HResult GetObject(
            [MarshalAs(UnmanagedType.Interface)] out object ppObject
            );

        [PreserveSig]
        IntPtr GetStateNoAddRef();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("2CD2D921-C447-44A7-A13C-4ADABFC247E3"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFAttributes
    {
        [PreserveSig]
        HResult GetItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFAttributes.GetItem", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
            );

        [PreserveSig]
        HResult GetItemType(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out MFAttributeType pType
            );

        [PreserveSig]
        HResult CompareItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant Value,
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
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out int punValue
            );

        [PreserveSig]
        HResult GetUINT64(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out long punValue
            );

        [PreserveSig]
        HResult GetDouble(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out double pfValue
            );

        [PreserveSig]
        HResult GetGUID(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out Guid pguidValue
            );

        [PreserveSig]
        HResult GetStringLength(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out int pcchLength
            );

        [PreserveSig]
        HResult GetString(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszValue,
            int cchBufSize,
            out int pcchLength
            );

        [PreserveSig]
        HResult GetAllocatedString(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue,
            out int pcchLength
            );

        [PreserveSig]
        HResult GetBlobSize(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out int pcbBlobSize
            );

        [PreserveSig]
        HResult GetBlob(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pBuf,
            int cbBufSize,
            out int pcbBlobSize
            );

        // Use GetBlob instead of this
        [PreserveSig]
        HResult GetAllocatedBlob(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            out IntPtr ip,  // Read w/Marshal.Copy, Free w/Marshal.FreeCoTaskMem
            out int pcbSize
            );

        [PreserveSig]
        HResult GetUnknown(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppv
            );

        [PreserveSig]
        HResult SetItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant Value
            );

        [PreserveSig]
        HResult DeleteItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey
            );

        [PreserveSig]
        HResult DeleteAllItems();

        [PreserveSig]
        HResult SetUINT32(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            int unValue
            );

        [PreserveSig]
        HResult SetUINT64(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            long unValue
            );

        [PreserveSig]
        HResult SetDouble(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            double fValue
            );

        [PreserveSig]
        HResult SetGUID(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidValue
            );

        [PreserveSig]
        HResult SetString(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.LPWStr)] string wszValue
            );

        [PreserveSig]
        HResult SetBlob(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pBuf,
            int cbBufSize
            );

        [PreserveSig]
        HResult SetUnknown(
            [MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnknown
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
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFAttributes.GetItemByIndex", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
            );

        [PreserveSig]
        HResult CopyAllItems(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("76B1BBDB-4EC8-4F36-B106-70A9316DF593")]
    internal interface IMFAudioStreamVolume
    {
        [PreserveSig]
        HResult GetChannelCount(
            out int pdwCount
            );

        [PreserveSig]
        HResult SetChannelVolume(
            int dwIndex,
            float fLevel
            );

        [PreserveSig]
        HResult GetChannelVolume(
            int dwIndex,
            out float pfLevel
            );

        [PreserveSig]
        HResult SetAllVolumes(
            int dwCount,
            [In, MarshalAs(UnmanagedType.LPArray)] float[] pfVolumes
            );

        [PreserveSig]
        HResult GetAllVolumes(
            int dwCount,
            [Out, MarshalAs(UnmanagedType.LPArray)] float[] pfVolumes
            );
    }

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

    [ComImport, SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("2EB1E945-18B8-4139-9B1A-D5D584818530")]
    internal interface IMFClock
    {
        [PreserveSig]
        HResult GetClockCharacteristics(
            out MFClockCharacteristicsFlags pdwCharacteristics
            );

        [PreserveSig]
        HResult GetCorrelatedTime(
            [In] int dwReserved,
            out long pllClockTime,
            out long phnsSystemTime
            );

        [PreserveSig]
        HResult GetContinuityKey(
            out int pdwContinuityKey
            );

        [PreserveSig]
        HResult GetState(
            [In] int dwReserved,
            out MFClockState peClockState
            );

        //[PreserveSig]
        //HResult GetProperties(
        //    out MFClockProperties pClockProperties
        //    );
        [PreserveSig]
        HResult GetProperties();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("5BC8A76B-869A-46A3-9B03-FA218A66AEBE")]
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
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkElement
            );

        [PreserveSig]
        HResult RemoveElement(
            [In] int dwElementIndex,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnkElement
            );

        [PreserveSig]
        HResult InsertElementAt(
            [In] int dwIndex,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnknown
            );

        [PreserveSig]
        HResult RemoveAllElements();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("DF598932-F10C-4E39-BBA2-C308F101DAA3"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFMediaEvent : IMFAttributes
    {
        #region IMFAttributes methods

        [PreserveSig]
        new HResult GetItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFMediaEvent.GetItem", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
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
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFMediaEvent.GetItemByIndex", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
            );

        [PreserveSig]
        new HResult CopyAllItems(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest
            );

        #endregion

        [PreserveSig]
        HResult GetType(
            out MediaEventType pmet
            );

        [PreserveSig]
        HResult GetExtendedType(
            out Guid pguidExtendedType
            );

        [PreserveSig]
        HResult GetStatus(
            [MarshalAs(UnmanagedType.Error)] out HResult phrStatus
            );

        [PreserveSig]
        HResult GetValue(
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFMediaEvent.GetValue", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pvValue
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("2CD0BD52-BCD5-4B89-B62C-EADC0C031E7D"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFMediaEventGenerator
    {
        [PreserveSig]
        HResult GetEvent(
            [In] MFEventFlag dwFlags,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaEvent ppEvent
            );

        [PreserveSig]
        HResult BeginGetEvent(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncCallback pCallback,
            [In, MarshalAs(UnmanagedType.IUnknown)] object o
            );

        [PreserveSig]
        HResult EndGetEvent(
            IMFAsyncResult pResult,
            out IMFMediaEvent ppEvent
            );

        [PreserveSig]
        HResult QueueEvent(
            [In] MediaEventType met,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidExtendedType,
            [In] HResult hrStatus,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant pvValue
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("90377834-21D0-4DEE-8214-BA2E3E6C1127"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFMediaSession : IMFMediaEventGenerator
    {
        #region IMFMediaEventGenerator methods

        [PreserveSig]
        new HResult GetEvent(
            [In] MFEventFlag dwFlags,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaEvent ppEvent
            );

        [PreserveSig]
        new HResult BeginGetEvent(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncCallback pCallback,
            [In, MarshalAs(UnmanagedType.IUnknown)] object o
            );

        [PreserveSig]
        new HResult EndGetEvent(
            IMFAsyncResult pResult,
            out IMFMediaEvent ppEvent
            );

        [PreserveSig]
        new HResult QueueEvent(
            [In] MediaEventType met,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidExtendedType,
            [In] HResult hrStatus,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant pvValue
            );

        #endregion

        [PreserveSig]
        HResult SetTopology(
            [In] MFSessionSetTopologyFlags dwSetTopologyFlags,
            [In, MarshalAs(UnmanagedType.Interface)] IMFTopology pTopology
            );

        [PreserveSig]
        HResult ClearTopologies();

        [PreserveSig]
        HResult Start(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidTimeFormat,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant pvarStartPosition
            );

        [PreserveSig]
        HResult Pause();

        [PreserveSig]
        HResult Stop();

        [PreserveSig]
        HResult Close();

        [PreserveSig]
        HResult Shutdown();

        [PreserveSig]
        HResult GetClock(
            [MarshalAs(UnmanagedType.Interface)] out IMFClock ppClock
            );

        [PreserveSig]
        HResult GetSessionCapabilities(
            out MFSessionCapabilities pdwCaps
            );

        [PreserveSig]
        HResult GetFullTopology(
            [In] MFSessionGetFullTopologyFlags dwGetFullTopologyFlags,
            [In] long TopoId,
            [MarshalAs(UnmanagedType.Interface)] out IMFTopology ppFullTopology
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("279A808D-AEC7-40C8-9C6B-A6B492C78A66")]
    internal interface IMFMediaSource : IMFMediaEventGenerator
    {
        #region IMFMediaEventGenerator methods

        [PreserveSig]
        new HResult GetEvent(
            [In] MFEventFlag dwFlags,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaEvent ppEvent
            );

        [PreserveSig]
        new HResult BeginGetEvent(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncCallback pCallback,
            [In, MarshalAs(UnmanagedType.IUnknown)] object o
            );

        [PreserveSig]
        new HResult EndGetEvent(
            IMFAsyncResult pResult,
            out IMFMediaEvent ppEvent
            );

        [PreserveSig]
        new HResult QueueEvent(
            [In] MediaEventType met,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidExtendedType,
            [In] HResult hrStatus,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant pvValue
            );

        #endregion

        [PreserveSig]
        HResult GetCharacteristics(
            out MFMediaSourceCharacteristics pdwCharacteristics
            );

        [PreserveSig]
        HResult CreatePresentationDescriptor(
            out IMFPresentationDescriptor ppPresentationDescriptor
            );

        [PreserveSig]
        HResult Start(
            [In, MarshalAs(UnmanagedType.Interface)] IMFPresentationDescriptor pPresentationDescriptor,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidTimeFormat,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant pvarStartPosition
            );

        [PreserveSig]
        HResult Stop();

        [PreserveSig]
        HResult Pause();

        [PreserveSig]
        HResult Shutdown();
    }

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

    [ComImport, SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("44AE0FA8-EA31-4109-8D2E-4CAE4997C555")]
    internal interface IMFMediaType : IMFAttributes
    {
        #region IMFAttributes methods

        [PreserveSig]
        new HResult GetItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFMediaType.GetItem", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
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
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFMediaType.GetItemByIndex", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
            );

        [PreserveSig]
        new HResult CopyAllItems(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest
            );

        #endregion

        [PreserveSig]
        HResult GetMajorType(
            out Guid pguidMajorType
            );

        [PreserveSig]
        HResult IsCompressedFormat(
            [MarshalAs(UnmanagedType.Bool)] out bool pfCompressed
            );

        [PreserveSig]
        HResult IsEqual(
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaType pIMediaType,
            out MFMediaEqual pdwFlags
            );

        [PreserveSig]
        HResult GetRepresentation(
            [In, MarshalAs(UnmanagedType.Struct)] Guid guidRepresentation,
            out IntPtr ppvRepresentation
            );

        [PreserveSig]
        HResult FreeRepresentation(
            [In, MarshalAs(UnmanagedType.Struct)] Guid guidRepresentation,
            [In] IntPtr pvRepresentation
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("E93DCF6C-4B07-4E1E-8123-AA16ED6EADF5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFMediaTypeHandler
    {
        [PreserveSig]
        HResult IsMediaTypeSupported(
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaType pMediaType,
            IntPtr ppMediaType  //[MarshalAs(UnmanagedType.Interface)] out IMFMediaType ppMediaType
            );

        [PreserveSig]
        HResult GetMediaTypeCount(
            out int pdwTypeCount
            );

        [PreserveSig]
        HResult GetMediaTypeByIndex(
            [In] int dwIndex,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaType ppType
            );

        [PreserveSig]
        HResult SetCurrentMediaType(
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaType pMediaType
            );

        [PreserveSig]
        HResult GetCurrentMediaType(
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaType ppMediaType
            );

        [PreserveSig]
        HResult GetMajorType(
            out Guid pguidMajorType
            );
    }

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

        #endregion

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

    [ComImport, SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
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
            [In, MarshalAs(UnmanagedType.LPStruct)] PropertyKey key,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IPropertyStore.GetValue", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pv
            );

        [PreserveSig]
        HResult SetValue(
            [In, MarshalAs(UnmanagedType.LPStruct)] PropertyKey key,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant propvar
            );

        [PreserveSig]
        HResult Commit();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("88DDCD21-03C3-4275-91ED-55EE3929328F"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFRateControl
    {
        [PreserveSig]
        HResult SetRate(
            [In, MarshalAs(UnmanagedType.Bool)] bool fThin,
            [In] float flRate
            );

        [PreserveSig]
        HResult GetRate(
            [In, Out, MarshalAs(UnmanagedType.Bool)] ref bool pfThin,
            out float pflRate
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("0A9CCDBC-D797-4563-9667-94EC5D79292D"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFRateSupport
    {
        [PreserveSig]
        HResult GetSlowestRate(
            [In] MFRateDirection eDirection,
            [In, MarshalAs(UnmanagedType.Bool)] bool fThin,
            out float pflRate
            );

        [PreserveSig]
        HResult GetFastestRate(
            [In] MFRateDirection eDirection,
            [In, MarshalAs(UnmanagedType.Bool)] bool fThin,
            out float pflRate
            );

        [PreserveSig]
        HResult IsRateSupported(
            [In, MarshalAs(UnmanagedType.Bool)] bool fThin,
            [In] float flRate,
            [In, Out] MfFloat pflNearestSupportedRate
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("089EDF13-CF71-4338-8D13-9E569DBDC319"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFSimpleAudioVolume
    {
        [PreserveSig]
        HResult SetMasterVolume(
            [In] float fLevel
            );

        [PreserveSig]
        HResult GetMasterVolume(
            out float pfLevel
            );

        [PreserveSig]
        HResult SetMute(
            [In, MarshalAs(UnmanagedType.Bool)] bool bMute
            );

        [PreserveSig]
        HResult GetMute(
            [MarshalAs(UnmanagedType.Bool)] out bool pbMute
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("FBE5A32D-A497-4B61-BB85-97B1A848A6E3"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFSourceResolver
    {
        [PreserveSig]
        HResult CreateObjectFromURL(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
            [In] MFResolution dwFlags,
            IPropertyStore pProps,
            out MFObjectType pObjectType,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppObject
            );

        [PreserveSig]
        HResult CreateObjectFromByteStream(
            [In, MarshalAs(UnmanagedType.Interface)] IMFByteStream pByteStream,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
            [In] MFResolution dwFlags,
            [In, MarshalAs(UnmanagedType.Interface)] IPropertyStore pProps,
            out MFObjectType pObjectType,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppObject
            );

        [PreserveSig]
        HResult BeginCreateObjectFromURL(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
            MFResolution dwFlags,
            IPropertyStore pProps,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppIUnknownCancelCookie,
            IMFAsyncCallback pCallback,
            [In, MarshalAs(UnmanagedType.IUnknown)] object punkState
            );

        [PreserveSig]
        HResult EndCreateObjectFromURL(
            IMFAsyncResult pResult,
            out MFObjectType pObjectType,
            [MarshalAs(UnmanagedType.Interface)] out object ppObject
            );

        //[PreserveSig]
        //HResult BeginCreateObjectFromByteStream(
        //    [In, MarshalAs(UnmanagedType.Interface)] IMFByteStream pByteStream,
        //    [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
        //    [In] MFResolution dwFlags,
        //    IPropertyStore pProps,
        //    [MarshalAs(UnmanagedType.IUnknown)] out object ppIUnknownCancelCookie,
        //    IMFAsyncCallback pCallback,
        //    [MarshalAs(UnmanagedType.IUnknown)] object punkState
        //   );

        [PreserveSig]
        HResult EndCreateObjectFromByteStream(
            IMFAsyncResult pResult,
            out MFObjectType pObjectType,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppObject
            );

        [PreserveSig]
        HResult CancelObjectCreation(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pIUnknownCancelCookie
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56C03D9C-9DBB-45F5-AB4B-D80F47C05938"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFStreamDescriptor : IMFAttributes
    {
        #region IMFAttributes methods

        [PreserveSig]
        new HResult GetItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFStreamDescriptor.GetItem", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
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
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFStreamDescriptor.GetItemByIndex", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
            );

        [PreserveSig]
        new HResult CopyAllItems(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest
            );

        #endregion

        [PreserveSig]
        HResult GetStreamIdentifier(
            out int pdwStreamIdentifier
            );

        [PreserveSig]
        HResult GetMediaTypeHandler(
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaTypeHandler ppMediaTypeHandler
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("83CF873A-F6DA-4BC8-823F-BACFD55DC433")]
    internal interface IMFTopology : IMFAttributes
    {
        #region IMFAttributes methods

        [PreserveSig]
        new HResult GetItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFTopology.GetItem", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
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
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFTopology.GetItemByIndex", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
            );

        [PreserveSig]
        new HResult CopyAllItems(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest
            );

        #endregion

        [PreserveSig]
        HResult GetTopologyID(
            out long pID
            );

        [PreserveSig]
        HResult AddNode(
            [In, MarshalAs(UnmanagedType.Interface)] IMFTopologyNode pNode
            );

        [PreserveSig]
        HResult RemoveNode(
            [In, MarshalAs(UnmanagedType.Interface)] IMFTopologyNode pNode
            );

        [PreserveSig]
        HResult GetNodeCount(
            out short pwNodes
            );

        [PreserveSig]
        HResult GetNode(
            [In] short wIndex,
            [MarshalAs(UnmanagedType.Interface)] out IMFTopologyNode ppNode
            );

        [PreserveSig]
        HResult Clear();

        [PreserveSig]
        HResult CloneFrom(
            [In, MarshalAs(UnmanagedType.Interface)] IMFTopology pTopology
            );

        [PreserveSig]
        HResult GetNodeByID(
            [In] long qwTopoNodeID,
            [MarshalAs(UnmanagedType.Interface)] out IMFTopologyNode ppNode
            );

        [PreserveSig]
        HResult GetSourceNodeCollection(
            [MarshalAs(UnmanagedType.Interface)] out IMFCollection ppCollection
            );

        [PreserveSig]
        HResult GetOutputNodeCollection(
            [MarshalAs(UnmanagedType.Interface)] out IMFCollection ppCollection
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("83CF873A-F6DA-4BC8-823F-BACFD55DC430"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFTopologyNode : IMFAttributes
    {
        #region IMFAttributes methods

        [PreserveSig]
        new HResult GetItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFTopologyNode.GetItem", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
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
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFTopologyNode.GetItemByIndex", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
            );

        [PreserveSig]
        new HResult CopyAllItems(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest
            );

        #endregion

        [PreserveSig]
        HResult SetObject(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pObject
            );

        [PreserveSig]
        HResult GetObject(
            [MarshalAs(UnmanagedType.IUnknown)] out object ppObject
            );

        [PreserveSig]
        HResult GetNodeType(
            out MFTopologyType pType
            );

        [PreserveSig]
        HResult GetTopoNodeID(
            out long pID
            );

        [PreserveSig]
        HResult SetTopoNodeID(
            [In] long ullTopoID
            );

        [PreserveSig]
        HResult GetInputCount(
            out int pcInputs
            );

        [PreserveSig]
        HResult GetOutputCount(
            out int pcOutputs
            );

        [PreserveSig]
        HResult ConnectOutput(
            [In] int dwOutputIndex,
            [In, MarshalAs(UnmanagedType.Interface)] IMFTopologyNode pDownstreamNode,
            [In] int dwInputIndexOnDownstreamNode
            );

        [PreserveSig]
        HResult DisconnectOutput(
            [In] int dwOutputIndex
            );

        [PreserveSig]
        HResult GetInput(
            [In] int dwInputIndex,
            [MarshalAs(UnmanagedType.Interface)] out IMFTopologyNode ppUpstreamNode,
            out int pdwOutputIndexOnUpstreamNode
            );

        [PreserveSig]
        HResult GetOutput(
            [In] int dwOutputIndex,
            [MarshalAs(UnmanagedType.Interface)] out IMFTopologyNode ppDownstreamNode,
            out int pdwInputIndexOnDownstreamNode
            );

        [PreserveSig]
        HResult SetOutputPrefType(
            [In] int dwOutputIndex,
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaType pType
            );

        [PreserveSig]
        HResult GetOutputPrefType(
            [In] int dwOutputIndex,
            out IMFMediaType ppType
            );

        [PreserveSig]
        HResult SetInputPrefType(
            [In] int dwInputIndex,
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaType pType
            );

        [PreserveSig]
        HResult GetInputPrefType(
            [In] int dwInputIndex,
            out IMFMediaType ppType
            );

        [PreserveSig]
        HResult CloneFrom(
            [In, MarshalAs(UnmanagedType.Interface)] IMFTopologyNode pNode
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("A490B1E4-AB84-4D31-A1B2-181E03B1077A"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFVideoDisplayControl
    {
        [PreserveSig]
        HResult GetNativeVideoSize(
            [Out] MFSize pszVideo,
            [Out] MFSize pszARVideo
            );

        [PreserveSig]
        HResult GetIdealVideoSize(
            [Out] MFSize pszMin,
            [Out] MFSize pszMax
            );

        [PreserveSig]
        HResult SetVideoPosition(
            [In] MFVideoNormalizedRect pnrcSource,
            [In] MFRect prcDest
            );

        [PreserveSig]
        HResult GetVideoPosition(
            [Out] MFVideoNormalizedRect pnrcSource,
            [Out] MFRect prcDest
            );

        [PreserveSig]
        HResult SetAspectRatioMode(
            [In] MFVideoAspectRatioMode dwAspectRatioMode
            );

        [PreserveSig]
        HResult GetAspectRatioMode(
            out MFVideoAspectRatioMode pdwAspectRatioMode
            );

        [PreserveSig]
        HResult SetVideoWindow(
            [In] IntPtr hwndVideo
            );

        [PreserveSig]
        HResult GetVideoWindow(
            out IntPtr phwndVideo
            );

        [PreserveSig]
        HResult RepaintVideo();

        //[PreserveSig]
        //HResult GetCurrentImage(
        //    [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFVideoDisplayControl.GetCurrentImage", MarshalTypeRef = typeof(BMMarshaler))] BitmapInfoHeader pBih,
        //    out IntPtr pDib,
        //    out int pcbDib,
        //    out long pTimeStamp
        //    );
        [PreserveSig]
        HResult NotImpl();

        [PreserveSig]
        HResult SetBorderColor(
            [In] int Clr
            );

        [PreserveSig]
        HResult GetBorderColor(
            out int pClr
            );

        [PreserveSig]
        HResult SetRenderingPrefs(
            [In] MFVideoRenderPrefs dwRenderFlags
            );

        [PreserveSig]
        HResult GetRenderingPrefs(
            out MFVideoRenderPrefs pdwRenderFlags
            );

        [PreserveSig]
        HResult SetFullscreen(
            [In, MarshalAs(UnmanagedType.Bool)] bool fFullscreen
            );

        [PreserveSig]
        HResult GetFullscreen(
            [MarshalAs(UnmanagedType.Bool)] out bool pfFullscreen
            );
    }

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

    [ComImport, SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("6AB0000C-FECE-4d1f-A2AC-A9573530656E")]
    internal interface IMFVideoProcessor
    {
        [PreserveSig]
        HResult GetAvailableVideoProcessorModes(
            out int lpdwNumProcessingModes,
            //[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] out Guid[] ppVideoProcessingModes);
            [MarshalAs(UnmanagedType.LPArray)] out Guid[] ppVideoProcessingModes);

        [PreserveSig]
        HResult GetVideoProcessorCaps(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid lpVideoProcessorMode,
            out DXVA2VideoProcessorCaps lpVideoProcessorCaps);

        [PreserveSig]
        HResult GetVideoProcessorMode(
            out Guid lpMode);

        [PreserveSig]
        HResult SetVideoProcessorMode(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid lpMode);

        [PreserveSig]
        HResult GetProcAmpRange(
            DXVA2ProcAmp dwProperty,
            out DXVA2ValueRange pPropRange);

        [PreserveSig]
        HResult GetProcAmpValues(
            DXVA2ProcAmp dwFlags,
            [Out, MarshalAs(UnmanagedType.LPStruct)] DXVA2ProcAmpValues Values);

        [PreserveSig]
        HResult SetProcAmpValues(
            DXVA2ProcAmp dwFlags,
            [In] DXVA2ProcAmpValues pValues);

        //[PreserveSig]
        //HResult GetFilteringRange(
        //    DXVA2Filters dwProperty,
        //    out DXVA2ValueRange pPropRange);
        [PreserveSig]
        HResult GetFilteringRange();

        //[PreserveSig]
        //HResult GetFilteringValue(
        //    DXVA2Filters dwProperty,
        //    out int pValue);
        [PreserveSig]
        HResult GetFilteringValue();

        //[PreserveSig]
        //HResult SetFilteringValue(
        //    DXVA2Filters dwProperty,
        //    [In] ref int pValue);
        [PreserveSig]
        HResult SetFilteringValue();

        [PreserveSig]
        HResult GetBackgroundColor(
            out int lpClrBkg);

        [PreserveSig]
        HResult SetBackgroundColor(
            int ClrBkg);
    }

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

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("70ae66f2-c809-4e4f-8915-bdcb406b7993")]
    internal interface IMFSourceReader
    {
        [PreserveSig]
        HResult GetStreamSelection(
            int dwStreamIndex,
            [MarshalAs(UnmanagedType.Bool)] out bool pfSelected
        );

        [PreserveSig]
        HResult SetStreamSelection(
            int dwStreamIndex,
            [MarshalAs(UnmanagedType.Bool)] bool fSelected
        );

        [PreserveSig]
        HResult GetNativeMediaType(
            int dwStreamIndex,
            int dwMediaTypeIndex,
            out IMFMediaType ppMediaType
        );

        [PreserveSig]
        HResult GetCurrentMediaType(
            int dwStreamIndex,
            out IMFMediaType ppMediaType
        );

        [PreserveSig]
        HResult SetCurrentMediaType(
            int dwStreamIndex,
            [In, Out] MFInt pdwReserved,
            IMFMediaType pMediaType
        );

        [PreserveSig]
        HResult SetCurrentPosition(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidTimeFormat,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant varPosition
        );

        [PreserveSig]
        HResult ReadSample(
            int dwStreamIndex,
            MF_SOURCE_READER_CONTROL_FLAG dwControlFlags,
            out int pdwActualStreamIndex,
            out MF_SOURCE_READER_FLAG pdwStreamFlags,
            out long pllTimestamp,
            out IMFSample ppSample
        );

        [PreserveSig]
        HResult Flush(
            int dwStreamIndex
        );

        [PreserveSig]
        HResult GetServiceForStream(
            int dwStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject
        );

        [PreserveSig]
        HResult GetPresentationAttribute(
            int dwStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidAttribute,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFSourceReader.GetPresentationAttribute", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pvarAttribute
        );
    }

    // This is the ASync version of IMFSourceReader.  The only difference is the ReadSample method, which must allow
    // the final 4 params to be null.

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("70ae66f2-c809-4e4f-8915-bdcb406b7993")]
    internal interface IMFSourceReaderAsync
    {
        [PreserveSig]
        HResult GetStreamSelection(
            int dwStreamIndex,
            [MarshalAs(UnmanagedType.Bool)] out bool pfSelected
        );

        [PreserveSig]
        HResult SetStreamSelection(
            int dwStreamIndex,
            [MarshalAs(UnmanagedType.Bool)] bool fSelected
        );

        [PreserveSig]
        HResult GetNativeMediaType(
            int dwStreamIndex,
            int dwMediaTypeIndex,
            out IMFMediaType ppMediaType
        );

        [PreserveSig]
        HResult GetCurrentMediaType(
            int dwStreamIndex,
            out IMFMediaType ppMediaType
        );

        [PreserveSig]
        HResult SetCurrentMediaType(
            int dwStreamIndex,
            [In, Out] MFInt pdwReserved,
            IMFMediaType pMediaType
        );

        [PreserveSig]
        HResult SetCurrentPosition(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidTimeFormat,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant varPosition
        );

        [PreserveSig]
        HResult ReadSample(
            int dwStreamIndex,
            MF_SOURCE_READER_CONTROL_FLAG dwControlFlags,
            IntPtr pdwActualStreamIndex,
            IntPtr pdwStreamFlags,
            IntPtr pllTimestamp,
            IntPtr ppSample
        );

        [PreserveSig]
        HResult Flush(
            int dwStreamIndex
        );

        [PreserveSig]
        HResult GetServiceForStream(
            int dwStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject
        );

        [PreserveSig]
        HResult GetPresentationAttribute(
            int dwStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidAttribute,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFSourceReaderAsync.GetPresentationAttribute", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pvarAttribute
        );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("deec8d99-fa1d-4d82-84c2-2c8969944867")]
    internal interface IMFSourceReaderCallback
    {
        [PreserveSig]
        HResult OnReadSample(
            HResult hrStatus,
            int dwStreamIndex,
            MF_SOURCE_READER_FLAG dwStreamFlags,
            long llTimestamp,
            IMFSample pSample
        );

        [PreserveSig]
        HResult OnFlush(
            int dwStreamIndex
        );

        [PreserveSig]
        HResult OnEvent(
            int dwStreamIndex,
            IMFMediaEvent pEvent
        );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("C40A00F2-B93A-4D80-AE8C-5A1C634F58E4"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFSample : IMFAttributes
    {
        #region IMFAttributes methods

        [PreserveSig]
        new HResult GetItem(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFSample.GetItem", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
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
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFSample.GetItemByIndex", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pValue
            );

        [PreserveSig]
        new HResult CopyAllItems(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest
            );

        #endregion

        [PreserveSig]
        HResult GetSampleFlags(
            out int pdwSampleFlags // Must be zero
            );

        [PreserveSig]
        HResult SetSampleFlags(
            [In] int dwSampleFlags // Must be zero
            );

        [PreserveSig]
        HResult GetSampleTime(
            out long phnsSampleTime
            );

        [PreserveSig]
        HResult SetSampleTime(
            [In] long hnsSampleTime
            );

        [PreserveSig]
        HResult GetSampleDuration(
            out long phnsSampleDuration
            );

        [PreserveSig]
        HResult SetSampleDuration(
            [In] long hnsSampleDuration
            );

        [PreserveSig]
        HResult GetBufferCount(
            out int pdwBufferCount
            );

        [PreserveSig]
        HResult GetBufferByIndex(
            [In] int dwIndex,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaBuffer ppBuffer
            );

        [PreserveSig]
        HResult ConvertToContiguousBuffer(
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaBuffer ppBuffer
            );

        [PreserveSig]
        HResult AddBuffer(
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaBuffer pBuffer
            );

        [PreserveSig]
        HResult RemoveBufferByIndex(
            [In] int dwIndex
            );

        [PreserveSig]
        HResult RemoveAllBuffers();

        [PreserveSig]
        HResult GetTotalLength(
            out int pcbTotalLength
            );

        [PreserveSig]
        HResult CopyToBuffer(
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaBuffer pBuffer
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("045FA593-8799-42B8-BC8D-8968C6453507")]
    internal interface IMFMediaBuffer
    {
        [PreserveSig]
        HResult Lock(
            out IntPtr ppbBuffer,
            out int pcbMaxLength,
            out int pcbCurrentLength
            );

        [PreserveSig]
        HResult Unlock();

        [PreserveSig]
        HResult GetCurrentLength(
            out int pcbCurrentLength
            );

        [PreserveSig]
        HResult SetCurrentLength(
            [In] int cbCurrentLength
            );

        [PreserveSig]
        HResult GetMaxLength(
            out int pcbMaxLength
            );
    }

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

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("3137f1cd-fe5e-4805-a5d8-fb477448cb3d")]
    internal interface IMFSinkWriter
    {
        [PreserveSig]
        HResult AddStream(
            IMFMediaType pTargetMediaType,
            out int pdwStreamIndex
        );

        [PreserveSig]
        HResult SetInputMediaType(
            int dwStreamIndex,
            IMFMediaType pInputMediaType,
            IMFAttributes pEncodingParameters
        );

        [PreserveSig]
        HResult BeginWriting();

        [PreserveSig]
        HResult WriteSample(
            int dwStreamIndex,
            IMFSample pSample
        );

        [PreserveSig]
        HResult SendStreamTick(
            int dwStreamIndex,
            long llTimestamp
        );

        [PreserveSig]
        HResult PlaceMarker(
            int dwStreamIndex,
            IntPtr pvContext
        );

        [PreserveSig]
        HResult NotifyEndOfSegment(
            int dwStreamIndex
        );

        [PreserveSig]
        HResult Flush(
            int dwStreamIndex
        );

        [PreserveSig]
        HResult Finalize_();

        [PreserveSig]
        HResult GetServiceForStream(
            int dwStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject
        );

        [PreserveSig]
        HResult GetStatistics(
            int dwStreamIndex,
            out MF_SINK_WRITER_STATISTICS pStats
        );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("AD4C1B00-4BF7-422F-9175-756693D9130D")]
    internal interface IMFByteStream
    {
        [PreserveSig]
        HResult GetCapabilities(
            out MFByteStreamCapabilities pdwCapabilities
            );

        [PreserveSig]
        HResult GetLength(
            out long pqwLength
            );

        [PreserveSig]
        HResult SetLength(
            [In] long qwLength
            );

        [PreserveSig]
        HResult GetCurrentPosition(
            out long pqwPosition
            );

        [PreserveSig]
        HResult SetCurrentPosition(
            [In] long qwPosition
            );

        [PreserveSig]
        HResult IsEndOfStream(
            [MarshalAs(UnmanagedType.Bool)] out bool pfEndOfStream
            );

        [PreserveSig]
        HResult Read(
            IntPtr pb,
            [In] int cb,
            out int pcbRead
            );

        [PreserveSig]
        HResult BeginRead(
            IntPtr pb,
            [In] int cb,
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncCallback pCallback,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkState
            );

        [PreserveSig]
        HResult EndRead(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncResult pResult,
            out int pcbRead
            );

        [PreserveSig]
        HResult Write(
            IntPtr pb,
            [In] int cb,
            out int pcbWritten
            );

        [PreserveSig]
        HResult BeginWrite(
            IntPtr pb,
            [In] int cb,
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncCallback pCallback,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkState
            );

        [PreserveSig]
        HResult EndWrite(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncResult pResult,
            out int pcbWritten
            );

        [PreserveSig]
        HResult Seek(
            [In] MFByteStreamSeekOrigin SeekOrigin,
            [In] long llSeekOffset,
            [In] MFByteStreamSeekingFlags dwSeekFlags,
            out long pqwCurrentPosition
            );

        [PreserveSig]
        HResult Flush();

        [PreserveSig]
        HResult Close();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("0000000c-0000-0000-C000-000000000046")]
    internal interface ISequentialStream
    {
        [PreserveSig]
        HResult Read(
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] buffer,
            [In] int bytesCount,
            [In] IntPtr bytesRead
            );

        [PreserveSig]
        HResult Write(
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] buffer,
            [In] int bytesCount,
            [In] IntPtr bytesWritten
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("0c733a30-2a1c-11ce-ade5-00aa0044773d")]
    internal interface IStream : ISequentialStream
    {
        #region ISequentialStream Methods

        [PreserveSig]
        new HResult Read(
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] buffer,
            [In] int bytesCount,
            [In] IntPtr bytesRead
            );

        [PreserveSig]
        new HResult Write(
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] buffer,
            [In] int bytesCount,
            [In] IntPtr bytesWritten
            );

        #endregion

        [PreserveSig]
        HResult Seek(
            [In] long offset,
            [In] System.IO.SeekOrigin origin,
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
            [Out] out System.Runtime.InteropServices.ComTypes.STATSTG statstg,
            [In] STATFLAG statFlag
            );

        [PreserveSig]
        HResult Clone(
            [Out] out IStream clonedStream
            );
    }


    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("C6E13370-30AC-11d0-A18C-00A0C9118956")]
    internal interface IAMCameraControl
    {
        [PreserveSig]
        HResult GetRange(
            [In] CameraControlProperty Property,
            [Out] out int pMin,
            [Out] out int pMax,
            [Out] out int pSteppingDelta,
            [Out] out int pDefault,
            [Out] out CameraControlFlags pCapsFlags
            );

        [PreserveSig]
        HResult Set(
            [In] CameraControlProperty Property,
            [In] int lValue,
            [In] CameraControlFlags Flags
            );

        [PreserveSig]
        HResult Get(
            [In] CameraControlProperty Property,
            [Out] out int lValue,
            [Out] out CameraControlFlags Flags
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("C6E13360-30AC-11D0-A18C-00A0C9118956")]
    internal interface IAMVideoProcAmp
    {
        [PreserveSig]
        HResult GetRange(
            [In] VideoProcAmpProperty Property,
            [Out] out int pMin,
            [Out] out int pMax,
            [Out] out int pSteppingDelta,
            [Out] out int pDefault,
            [Out] out VideoProcAmpFlags pCapsFlags
            );

        [PreserveSig]
        HResult Set(
            [In] VideoProcAmpProperty Property,
            [In] int lValue,
            [In] VideoProcAmpFlags Flags
            );

        [PreserveSig]
        HResult Get(
            [In] VideoProcAmpProperty Property,
            [Out] out int lValue,
            [Out] out VideoProcAmpFlags Flags
            );
    }

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

    internal enum STGM : uint
    {
        STGM_READ = 0x0,
        STGM_WRITE = 0x1,
        STGM_READWRITE = 0x2
    }

    [Flags]
    internal enum DeviceState
    {
        Active = 0x00000001,
        Disabled = 0x00000002,
        NotPresent = 0x00000004,
        Unplugged = 0x00000008,
        All = 0x0000000F
    }

    internal enum EDataFlow
    {
        eRender,
        eCapture,
        eAll,
        EDataFlow_enum_count
    }

    internal enum ERole
    {
        eConsole,
        eMultimedia,
        eCommunications,
        ERole_enum_count
    }

    [ComImport]
    [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    internal class MMDeviceEnumerator
    {
    }

    [Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), TypeLibType(TypeLibTypeFlags.FNonExtensible)]
    [ComImport]
    internal interface IMMDeviceCollection
    {
        [PreserveSig]
        void GetCount(out uint pcDevices);
        [PreserveSig]
        void Item([In] uint nDevice, [MarshalAs(UnmanagedType.Interface)] out IMMDevice ppDevice);
    }

    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), TypeLibType(TypeLibTypeFlags.FNonExtensible)]
    [ComImport]
    internal interface IMMDeviceEnumerator
    {
        [PreserveSig]
        void EnumAudioEndpoints([ComAliasName("MMDevAPI.Interop.EDataFlow")] [In] EDataFlow dataFlow, [In] uint dwStateMask, [MarshalAs(UnmanagedType.Interface)] out IMMDeviceCollection ppDevices);
        [PreserveSig]
        void GetDefaultAudioEndpoint([ComAliasName("MMDevAPI.Interop.EDataFlow")] [In] EDataFlow dataFlow, [ComAliasName("MMDevAPI.Interop.ERole")] [In] ERole role, [MarshalAs(UnmanagedType.Interface)] out IMMDevice ppEndpoint);
        [PreserveSig]
        void GetDevice([MarshalAs(UnmanagedType.LPWStr)] [In] string pwstrId, [MarshalAs(UnmanagedType.Interface)] out IMMDevice ppDevice);
        [PreserveSig]
        void RegisterEndpointNotificationCallback([MarshalAs(UnmanagedType.Interface)] [In] IMMNotificationClient pClient);
        [PreserveSig]
        void UnregisterEndpointNotificationCallback([MarshalAs(UnmanagedType.Interface)] [In] IMMNotificationClient pClient);
    }

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

    [Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), TypeLibType(TypeLibTypeFlags.FNonExtensible)]
    [ComImport]
    internal interface IMMNotificationClient
    {
        [PreserveSig]
        void OnDeviceStateChanged([MarshalAs(UnmanagedType.LPWStr)] [In] string pwstrDeviceId, [In] DeviceState dwNewState);
        [PreserveSig]
        void OnDeviceAdded([MarshalAs(UnmanagedType.LPWStr)] [In] string pwstrDeviceId);
        [PreserveSig]
        void OnDeviceRemoved([MarshalAs(UnmanagedType.LPWStr)] [In] string pwstrDeviceId);
        [PreserveSig]
        void OnDefaultDeviceChanged([ComAliasName("MMDevAPI.Interop.EDataFlow")] [In] EDataFlow flow, [ComAliasName("MMDevAPI.Interop.ERole")] [In] ERole role, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwstrDefaultDeviceId);
        [PreserveSig]
        void OnPropertyValueChanged([MarshalAs(UnmanagedType.LPWStr)] [In] string pwstrDeviceId, [In] PropertyKey key);
    }

    [Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioMeterInformation
    {
        [PreserveSig]
        int GetPeakValue(out float pfPeak);
        [PreserveSig]
        int GetMeteringChannelCount(out int pnChannelCount);
        [PreserveSig]
        int GetChannelsPeakValues(int u32ChannelCount, [In] IntPtr afPeakValues);
        [PreserveSig]
        int QueryHardwareSupport(out int pdwHardwareSupportMask);
    };

    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioEndpointVolume
    {
        [PreserveSig]
        int NotImpl1();
        [PreserveSig]
        int NotImpl2();
        [PreserveSig]
        int GetChannelCount([Out] [MarshalAs(UnmanagedType.U4)] out UInt32 channelCount);
        [PreserveSig]
        int SetMasterVolumeLevel([In] [MarshalAs(UnmanagedType.R4)] float level, [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);
        [PreserveSig]
        int SetMasterVolumeLevelScalar([In] [MarshalAs(UnmanagedType.R4)] float level, [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);
        [PreserveSig]
        int GetMasterVolumeLevel([Out] [MarshalAs(UnmanagedType.R4)] out float level);
        [PreserveSig]
        int GetMasterVolumeLevelScalar([Out] [MarshalAs(UnmanagedType.R4)] out float level);
    }

    #endregion

}
