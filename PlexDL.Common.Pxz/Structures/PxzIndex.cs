using PlexDL.Common.Enums;
using PlexDL.Common.Pxz.Structures.File.Record;
using System;
using System.Collections.Generic;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzIndex
    {
        /// <summary>
        /// PXZ author information; current user by default.
        /// </summary>
        public PxzAuthor Author { get; set; } = PxzAuthor.FromCurrent();

        /// <summary>
        /// PXZ file format version; based off of DLL versioning.
        /// </summary>
        public Version FormatVersion { get; set; } = Utilities.GetVersion();

        /// <summary>
        /// The current development status of PlexDL when this file was saved.
        /// </summary>
        public DevStatus BuildState { get; set; } = DevStatus.ProductionReady;

        /// <summary>
        /// The time that this PXZ file was created.
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Record referencing information. Contains the stored name and record name of each valid record.
        /// </summary>
        public List<PxzRecordNaming> RecordReference { get; set; } = new List<PxzRecordNaming>();
    }
}