﻿using PlexDL.Common.Enums;
using PlexDL.Common.Pxz.Compressors;
using PlexDL.Common.Security;
using PlexDL.Common.Security.Protection;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PlexDL.Common.Pxz.Structures.File.Record
{
    public class PxzRecordContent
    {
        /// <summary>
        /// Determines whether the record's contents have been encrypted via WDPAPI
        /// </summary>
        public bool Encrypted { get; set; }

        /// <summary>
        /// Raw compressed Gzipped bytes
        /// </summary>
        public byte[] RawRecord { get; set; }

        /// <summary>
        /// The original file name of the record (e.g. background.jpeg)
        /// </summary>
        public string FileName { get; set; } = @"";

        /// <summary>
        /// Updated automatically with decompressed data when you use Record
        /// </summary>
        [XmlIgnore]
        public byte[] TmpRecord { get; set; }

        /// <summary>
        /// Checks if TmpRecord is loaded, and if it is it returns TmpRecord, otherwise it executes and returns Record.<br />
        /// Note: This is the recommended way to access processed contents, as it conserves memory for you.
        /// </summary>
        [XmlIgnore]
        public byte[] AutoRecord => TmpRecord ?? Record;

        /// <summary>
        /// Process the raw record by decompressing it into a byte array OTF<br />
        /// Note: This is done on request, and is redone every time the value is requested. If you need to do this without recalculation,
        /// you can use TmpRecord after running this once or use AutoRecord instead.
        /// </summary>
        [XmlIgnore]
        public byte[] Record
        {
            get
            {
                //The record should be decompressed
                var raw = RawRecord;
                var r = raw?.Length > 0 ? GZipCompressor.DecompressBytes(raw) : null;

                //Check if the record is protected
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

            RawRecord = GZipCompressor.CompressBytes(rawData);
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
            var value = Conversion.StringToBytes(data.OuterXml);
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
            var value = Conversion.StringToBytes(data);
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
            return Encoding.ASCII.GetString(AutoRecord);
        }

        /// <summary>
        /// Re-encodes the raw Record bytes to ASCII, loads them to an XmlDocument then returns this XmlDocument object.
        /// </summary>
        /// <returns></returns>
        public XmlDocument ToXmlDocument()
        {
            var rawXml = Encoding.ASCII.GetString(AutoRecord);
            var doc = new XmlDocument();

            doc.LoadXml(rawXml);

            return doc;
        }

        /// <summary>
        /// Takes the Record bytes and dumps them to a Bitmap
        /// </summary>
        /// <returns></returns>
        public Image ToImage() => Utilities.ByteToImage(AutoRecord);
    }
}