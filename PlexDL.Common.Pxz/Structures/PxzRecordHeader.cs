using PlexDL.Common.Pxz.Enum;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzRecordHeader
    {
        public PxzRecordSize Size { get; set; } = new PxzRecordSize();
        public PxzRecordChecksum Checksums { get; set; } = new PxzRecordChecksum();
        public PxzRecordType DataType { get; set; } = PxzRecordType.Xml;
        public PxzRecordNaming Naming { get; set; } = new PxzRecordNaming();
    }
}