using System;
using System.Xml.Serialization;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzRecordSize
    {
        public double RawSize { get; set; }
        public double DecSize { get; set; }

        [XmlIgnore]
        public double Ratio => Math.Round(RawSize / DecSize * 100, 2);

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