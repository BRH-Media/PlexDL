using PlexDL.Common.Enums;
using PlexDL.Common.Pxz.Compressors;
using PlexDL.Common.Security;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzRecordContent
    {
        [XmlIgnore]
        private bool _encrypted = false;

        public PxzRecordContent()
        {
            //blank initialiser
        }

        public PxzRecordContent(XmlNode data)
        {
            var value = data.OuterXml;
            RawRecord = GZipCompressor.CompressString(value);
        }

        public PxzRecordContent(byte[] data)
        {
            RawRecord = GZipCompressor.CompressString(data);
        }

        public PxzRecordContent(string data)
        {
            RawRecord = GZipCompressor.CompressString(data);
        }

        public PxzRecordContent(Image data)
        {
            var value = Utilities.ImageToByte(data);
            RawRecord = GZipCompressor.CompressString(value);
        }

        public byte[] ToBytes() => Record;

        public override string ToString()
        {
            return Encoding.ASCII.GetString(Record);
        }

        public XmlDocument ToXmlDocument()
        {
            var rawXml = Encoding.ASCII.GetString(Record);

            var doc = new XmlDocument();
            doc.LoadXml(rawXml);

            return doc;
        }

        public Image ToImage() => Utilities.ByteToImage(Record);

        /// <summary>
        /// Determines whether the record's contents have been encrypted via DPAPI
        /// </summary>
        public bool Encrypted => _encrypted;

        /// <summary>
        /// Raw compressed base64-encoded (ASCII/UTF8) Gzipped bytes
        /// </summary>
        public string RawRecord { get; set; } = @"";

        /// <summary>
        /// Updated automatically with decompressed data when you use Record
        /// </summary>
        [XmlIgnore]
        public byte[] TmpRecord { get; set; }

        /// <summary>
        /// Process the raw record by decompressing it into a byte array OTF
        /// Note: This is done on request, and is redone every time the value is requested. If you need to do this without recalculation,
        /// you can use TmpRecord after running this once.
        /// </summary>
        [XmlIgnore]
        public byte[] Record
        {
            get
            {
                var raw = RawRecord;

                if (Encrypted)
                {
                    var provider = new ProtectedString(raw, StringProtectionMode.Decrypt);
                    raw = provider.ProcessedValue;
                }

                var r = !string.IsNullOrEmpty(raw) ? GZipCompressor.DecompressBytes(raw) : null;
                TmpRecord = r;
                return r;
            }
        }
    }
}