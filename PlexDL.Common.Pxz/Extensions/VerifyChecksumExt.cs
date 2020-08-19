using PlexDL.Common.Pxz.Structures;

namespace PlexDL.Common.Pxz.Extensions
{
    public static class VerifyChecksumExt
    {
        public static bool VerifyChecksum(this PxzRecordContent retrievedContent, PxzRecordChecksum originalChecksum)
        {
            return originalChecksum.Equals(new PxzRecordChecksum(retrievedContent));
        }
    }
}