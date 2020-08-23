using System;
using System.Collections.Generic;
using PlexDL.Common.Enums;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzIndex
    {
        public PxzAuthor Author { get; set; } = new PxzAuthor();
        public Version FormatVersion { get; set; }
        public DevStatus BuildState { get; set; } = DevStatus.ProductionReady;
        public List<PxzRecordNaming> RecordReference { get; set; } = new List<PxzRecordNaming>();
    }
}