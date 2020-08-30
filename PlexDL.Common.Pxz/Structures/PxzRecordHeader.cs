using PlexDL.Common.Pxz.Enum;
using System;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzRecordHeader
    {
        public PxzRecordSize Size { get; set; } = new PxzRecordSize();
        public PxzRecordChecksum Checksums { get; set; } = new PxzRecordChecksum();
        public PxzRecordNaming Naming { get; set; } = new PxzRecordNaming();
        public PxzRecordType DataType { get; set; } = PxzRecordType.Xml;
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}