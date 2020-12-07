namespace PlexDL.AltoHTTP.Enums
{
    /// <summary>
    /// Think of the units like stair steps
    /// </summary>
    public enum FromTo
    {
        BytesToKb = -1,
        KbToMb = -1,
        MbToGb = -1,
        MbToKb = 1,
        KbToBytes = 1,
        GbToMb = 1,
        KbToGb = -2,
        BytesToMb = -2,
        GbToKb = 2,
        MbToBytes = 2,
        BytesToGb = -3,
        GbToBytes = 3
    }
}