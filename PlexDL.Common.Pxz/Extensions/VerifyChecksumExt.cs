using PlexDL.Common.Pxz.Structures.File.Record;

namespace PlexDL.Common.Pxz.Extensions
{
    public static class VerifyChecksumExt
    {
        public static bool VerifyChecksum(this PxzRecordContent retrievedContent, PxzRecordChecksum originalChecksum)
        {
            var o = originalChecksum;
            var c = new PxzRecordChecksum(retrievedContent);
            return o.DecMd5 == c.DecMd5 && o.RawMd5 == c.RawMd5;
        }
    }
}