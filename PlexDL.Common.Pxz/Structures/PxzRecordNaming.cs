using PlexDL.Common.Pxz.Enums;
using System;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzRecordNaming
    {
        public PxzRecordType DataType { get; set; } = PxzRecordType.Xml;
        public string RecordName { get; set; } = $@"NewRecord{new Random().Next(1, 99999)}";
        public string StoredName { get; set; } = @"";
    }
}