using PlexDL.Common.Security.Hashing;

namespace PlexDL.Common.Pxz.Structures.File.Record
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

            var dec = MD5Helper.CalculateMd5Hash(content.AutoRecord);
            if (dec == null) return;

            RawMd5 = MD5Helper.Md5ToHex(MD5Helper.CalculateMd5Hash(content.RawRecord));
            DecMd5 = MD5Helper.Md5ToHex(dec);
        }
    }
}