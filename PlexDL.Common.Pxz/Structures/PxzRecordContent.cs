using PlexDL.Common.Enums;
using PlexDL.Common.Pxz.Compressors;
using PlexDL.Common.Security;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using PlexDL.Common.Security.Protection;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzRecordContent
    {
        /// <summary>
        /// Determines whether the record's contents have been encrypted via WDPAPI
        /// </summary>
        public bool Encrypted { get; set; }

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
                var r = !string.IsNullOrEmpty(raw) ? GZipCompressor.DecompressBytes(raw) : null;

                if (Encrypted && r != null)
                {
                    var provider = new ProtectedBytes(r, ProtectionMode.Decrypt);
                    r = provider.ProcessedValue;
                }

                TmpRecord = r;

                return r;
            }
        }

        /// <summary>
        /// rawData should NOT contain encrypted bytes; this is done here if applicable.
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="protectedData"></param>
        private void GenericConstructor(byte[] rawData, bool protectedData = false)
        {
            Encrypted = protectedData;

            //we'll need to encrypt everything if it's true
            if (protectedData)
            {
                var provider = new ProtectedBytes(rawData, ProtectionMode.Encrypt);
                rawData = provider.ProcessedValue;
            }

            RawRecord = GZipCompressor.CompressString(rawData);
        }

        /// <summary>
        /// Blank constructor
        /// </summary>
        public PxzRecordContent()
        {
            //blank initialiser
        }

        /// <summary>
        /// Empty constructor with protectedData switch
        /// </summary>
        /// <param name="protectedData"></param>
        public PxzRecordContent(bool protectedData = false)
        {
            Encrypted = protectedData;
        }

        /// <summary>
        /// Create a Record for an XmlDocument object
        /// </summary>
        /// <param name="data"></param>
        /// <param name="protectedData"></param>
        public PxzRecordContent(XmlNode data, bool protectedData = false)
        {
            var value = DataHelpers.StringToBytes(data.OuterXml);
            GenericConstructor(value, protectedData);
        }

        /// <summary>
        /// Create a Record for a byte[] array
        /// </summary>
        /// <param name="data"></param>
        /// <param name="protectedData"></param>
        public PxzRecordContent(byte[] data, bool protectedData = false)
        {
            var value = data;
            GenericConstructor(value, protectedData);
        }

        /// <summary>
        /// Create a Record for a string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="protectedData"></param>
        public PxzRecordContent(string data, bool protectedData = false)
        {
            var value = DataHelpers.StringToBytes(data);
            GenericConstructor(value, protectedData);
        }

        /// <summary>
        /// Create a Record for a Bitmap
        /// </summary>
        /// <param name="data"></param>
        /// <param name="protectedData"></param>
        public PxzRecordContent(Image data, bool protectedData = false)
        {
            var value = Utilities.ImageToByte(data);
            GenericConstructor(value, protectedData);
        }

        /// <summary>
        /// (Override) This takes bytes from Record and dumps them to an ASCII string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Encoding.ASCII.GetString(Record);
        }

        /// <summary>
        /// Re-encodes the raw Record bytes to ASCII, loads them to an XmlDocument then returns this XmlDocument object.
        /// </summary>
        /// <returns></returns>
        public XmlDocument ToXmlDocument()
        {
            var rawXml = Encoding.ASCII.GetString(Record);
            var doc = new XmlDocument();

            doc.LoadXml(rawXml);

            return doc;
        }

        /// <summary>
        /// Takes the Record bytes and dumps them to a Bitmap
        /// </summary>
        /// <returns></returns>
        public Image ToImage() => Utilities.ByteToImage(Record);
    }
}