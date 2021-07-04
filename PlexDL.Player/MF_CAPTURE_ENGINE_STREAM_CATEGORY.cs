namespace PlexDL.Player
{
    [UnmanagedName("MF_CAPTURE_ENGINE_STREAM_CATEGORY")]
    internal enum MF_CAPTURE_ENGINE_STREAM_CATEGORY
    {
        VideoPreview = 0x00000000,
        VideoCapture = 0x00000001,
        PhotoIndependent = 0x00000002,
        PhotoDependent = 0x00000003,
        Audio = 0x00000004,
        Unsupported = 0x00000005
    }
}