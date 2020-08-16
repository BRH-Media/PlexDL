using PlexDL.Common.Enums;
using System;
using System.Reflection;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzIndex
    {
        public PxzAuthor Author { get; set; } = new PxzAuthor();
        public Version FormatVersion { get; set; }
        public DevStatus BuildState { get; set; } = DevStatus.ProductionReady;
    }
}