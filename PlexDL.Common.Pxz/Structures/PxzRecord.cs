using PlexDL.Common.Pxz.Compressors;
using PlexDL.Common.Pxz.Enum;
using PlexDL.Common.Pxz.Extensions;
using PlexDL.Common.Security;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using UIHelpers;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzRecord
    {
        [XmlIgnore]
        public bool ProtectedRecord { get; set; }

        [XmlIgnore]
        public bool ChecksumValid => Content.VerifyChecksum(Header.Checksums);

        public PxzRecordHeader Header { get; set; } = new PxzRecordHeader();
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

        public static PxzRecord FromRawForm(string compressedRaw)
        {
            var decompressedXml = GZipCompressor.DecompressString(compressedRaw);
            return Serializers.StringToPxzRecord(decompressedXml);
        }

        public string ToRawForm()
        {
            var rawDoc = Serializers.PxzRecordToXml(this);
            var rawXml = rawDoc.OuterXml;
            return GZipCompressor.CompressString(rawXml);
        }

        public XmlDocument ToXmlForm()
        {
            return Serializers.PxzRecordToXml(this);
        }

        public static PxzRecord FromXmlForm(string rawXml)
        {
            return Serializers.StringToPxzRecord(rawXml);
        }
    }
}