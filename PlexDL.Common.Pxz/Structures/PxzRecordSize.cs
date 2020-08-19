namespace PlexDL.Common.Pxz.Structures
{
    public class PxzRecordSize
    {
        public long RawSize { get; set; } = 0;
        public long DecSize { get; set; } = 0;

        public PxzRecordSize()
        {
            //blank initialiser
        }

        public PxzRecordSize(PxzRecordContent content)
        {
            RawSize = content.RawRecord.Length;
            DecSize = RawSize > 0 ? content.Record.Length : 0;
        }
    }
}