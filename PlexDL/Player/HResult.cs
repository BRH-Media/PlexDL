namespace PlexDL.Player
{
    internal enum HResult
    {
        #region COM HRESULTs

        S_OK = 0,
        S_FALSE = 1,

        //E_PENDING = unchecked((int)0x8000000A),

        ///// <summary>Catastrophic failure</summary>
        //E_UNEXPECTED = unchecked((int)0x8000FFFF),

        /// <summary>Not implemented</summary>
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

        /// <summary>Unspecified error</summary>
        E_FAIL = unchecked((int)0x80004005),

        ///// <summary>General access denied error</summary>
        //E_ACCESSDENIED = unchecked((int)0x80070005),

        /// <summary>Wrong OS or OS version for application</summary>
        CO_E_WRONGOSFORAPP = unchecked((int)0x800401FA),

        /// <summary>Operation timed out</summary>
        COR_E_TIMEOUT = unchecked((int)0x80131505),

        #endregion COM HRESULTs

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

        ERROR_FILE_NOT_FOUND = 0x02,
        ERROR_INVALID_NAME = unchecked((int)0x8007007B),
        ERROR_PATH_NOT_FOUND = unchecked((int)0x80070003),
        ERROR_SYSTEM_DEVICE_NOT_FOUND = unchecked(0x00003BC3),
        ERROR_DEVICE_NOT_CONNECTED = 0x0000048F,
        ERROR_NOT_READY = 0x15,
        ERROR_BUSY = unchecked((int)0x800700AA),
        ERROR_INVALID_WINDOW_HANDLE = 0x00000578,

        #endregion Win32 HRESULTs

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

        #endregion Structured Storage HRESULTs

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
        //MF_E_UNSUPPORTED_BYTESTREAM_TYPE = unchecked((int)0xC00D36C4),
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
        //MF_E_AUDIO_RECORDING_DEVICE_INVALIDATED = unchecked((int)0xC00D3EA1),
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

        #endregion Media Foundation HRESULTs
    }
}