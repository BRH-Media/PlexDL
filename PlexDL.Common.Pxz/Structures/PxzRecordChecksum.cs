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
            if (content.AutoRecord.Length == 0) return;

            var dec = Md5Helper.CalculateMd5Hash(content.AutoRecord);
            if (dec == null) return;

            RawMd5 = Md5Helper.CalculateMd5Hash(content.RawRecord);
            DecMd5 = Md5Helper.Md5ToHex(dec);
        }
    }
}