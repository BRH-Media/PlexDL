using PlexDL.Common.Pxz.Compressors;
using PlexDL.Common.Pxz.Enum;
using PlexDL.Common.Pxz.Extensions;
using PlexDL.Common.Security;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzRecord
    {
        /// <summary>
        /// Whether or not this record is protected by WDPAPI
        /// </summary>
        [XmlIgnore]
        public bool ProtectedRecord { get; set; }

        /// <summary>
        /// Calculate MD5 checksums and see if this record has been tampered with
        /// </summary>
        [XmlIgnore]
        public bool ChecksumValid => Content.VerifyChecksum(Header.Checksums);

        /// <summary>
        /// Record header information
        /// </summary>
        public PxzRecordHeader Header { get; set; } = new PxzRecordHeader();

        /// <summary>
        /// The data of the record itself
        /// </summary>
        public PxzRecordContent Content { get; set; } = new PxzRecordContent();

        public PxzRecord()
        {
            //blank initialiser
        }

        public PxzRecord(bool protectedRecord = false)
        {
            ProtectedRecord = protectedRecord;
        }

        public PxzRecord(XmlNode data, string name, bool protectedRecord = false)
        {
            ProtectedRecord = protectedRecord;

            var content = new PxzRecordContent(data, protectedRecord);
            var header = new PxzRecordHeader
            {
                DataType = PxzRecordType.Xml,
                Checksums = new PxzRecordChecksum(content),
                Size = new PxzRecordSize(content),
                Naming = new PxzRecordNaming
                {
                    RecordName = name,
                    StoredName = $"{Md5Helper.CalculateMd5Hash(name)}.record"
                }
            };

            Header = header;
            Content = content;
        }

        public PxzRecord(byte[] data, string name, bool protectedRecord = false)
        {
            ProtectedRecord = protectedRecord;

            var content = new PxzRecordContent(data, protectedRecord);
            var header = new PxzRecordHeader
            {
                DataType = PxzRecordType.Byte,
                Checksums = new PxzRecordChecksum(content),
                Size = new PxzRecordSize(content),
                Naming = new PxzRecordNaming
                {
                    RecordName = name,
                    StoredName = $"{Md5Helper.CalculateMd5Hash(name)}.record"
                }
            };

            Header = header;
            Content = content;
        }

        public PxzRecord(string data, string name, bool protectedRecord = false)
        {
            ProtectedRecord = protectedRecord;

            var content = new PxzRecordContent(data, protectedRecord);
            var header = new PxzRecordHeader
            {
                DataType = PxzRecordType.Text,
                Checksums = new PxzRecordChecksum(content),
                Size = new PxzRecordSize(content),
                Naming = new PxzRecordNaming
                {
                    RecordName = name,
                    StoredName = $"{Md5Helper.CalculateMd5Hash(name)}.record"
                }
            };

            Header = header;
            Content = content;
        }

        public PxzRecord(Image data, string name, bool protectedRecord = false)
        {
            ProtectedRecord = protectedRecord;

            var content = new PxzRecordContent(data, protectedRecord);
            var header = new PxzRecordHeader
            {
                DataType = PxzRecordType.Bitmap,
                Checksums = new PxzRecordChecksum(content),
                Size = new PxzRecordSize(content),
                Naming = new PxzRecordNaming
                {
                    RecordName = name,
                    StoredName = $"{Md5Helper.CalculateMd5Hash(name)}.record"
                }
            };

            Header = header;
            Content = content;
        }

        /// <summary>
        /// Create a new record from a GZipped Base64 string
        /// </summary>
        /// <param name="compressedRaw">The raw representation of the PxzRecord to load</param>
        /// <returns></returns>
        public static PxzRecord FromRawForm(string compressedRaw)
        {
            var decompressedXml = GZipCompressor.DecompressString(compressedRaw);
            return Serializers.StringToPxzRecord(decompressedXml);
        }

        /// <summary>
        /// Convert the current record to a GZIpped Base64 string
        /// </summary>
        /// <returns></returns>
        public string ToRawForm()
        {
            var rawDoc = Serializers.PxzRecordToXml(this);
            var rawXml = rawDoc.OuterXml;
            return GZipCompressor.CompressString(rawXml);
        }

        /// <summary>
        /// Convert the current record to its XML-serialised form
        /// </summary>
        /// <returns></returns>
        public XmlDocument ToXmlForm()
        {
            return Serializers.PxzRecordToXml(this);
        }

        /// <summary>
        /// Create a new PxzRecord object from an XML-serialised PxzRecord
        /// </summary>
        /// <param name="rawXml"></param>
        /// <returns></returns>
        public static PxzRecord FromXmlForm(string rawXml)
        {
            return Serializers.StringToPxzRecord(rawXml);
        }
    }
}