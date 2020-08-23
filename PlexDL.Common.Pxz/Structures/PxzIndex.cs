using PlexDL.Common.Enums;
using System;
using System.Collections.Generic;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzIndex
    {
        public PxzAuthor Author { get; set; } = new PxzAuthor();
        public Version FormatVersion { get; set; } = Utilities.GetVersion();
        public DevStatus BuildState { get; set; } = DevStatus.ProductionReady;
        public List<PxzRecordNaming> RecordReference { get; set; } = new List<PxzRecordNaming>();
    }
}