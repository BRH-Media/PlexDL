using PlexDL.Common.Security;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzRecordChecksum
    {
        public string RawMd5 { get; set; } = @"";
        public string DecMd5 { get; set; } = @"";

        public PxzRecordChecksum()
        {
            //blank initialiser
        }

        public PxzRecordChecksum(PxzRecordContent content)
        {
            if (content.RawRecord.Length == 0) return;

            RawMd5 = Md5Helper.CalculateMd5Hash(content.RawRecord);
            DecMd5 = Md5Helper.Md5ToHex(Md5Helper.CalculateMd5Hash(content.Record));
        }
    }
}