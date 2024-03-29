﻿using PlexDL.Common.Pxz.Compressors;
using PlexDL.Common.Pxz.Enums;
using PlexDL.Common.Pxz.Extensions;
using System;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

// ReSharper disable UnusedMember.Global

namespace PlexDL.Common.Pxz.Structures.File.Record
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
            var content = new PxzRecordContent(data, protectedRecord);
            var header = new PxzRecordHeader
            {
                Checksums = new PxzRecordChecksum(content),
                Size = new PxzRecordSize(content),
                Naming = new PxzRecordNaming
                {
                    RecordName = name,
                    StoredName = $"{Guid.NewGuid()}.record",
                    DataType = PxzRecordType.Xml
                }
            };

            ProtectedRecord = protectedRecord;
            Header = header;
            Content = content;
        }

        public PxzRecord(byte[] data, string name, bool protectedRecord = false)
        {
            var content = new PxzRecordContent(data, protectedRecord);
            var header = new PxzRecordHeader
            {
                Checksums = new PxzRecordChecksum(content),
                Size = new PxzRecordSize(content),
                Naming = new PxzRecordNaming
                {
                    RecordName = name,
                    StoredName = $"{Guid.NewGuid()}.record",
                    DataType = PxzRecordType.Byte
                }
            };

            ProtectedRecord = protectedRecord;
            Header = header;
            Content = content;
        }

        public PxzRecord(string data, string name, PxzRecordType recordType = PxzRecordType.Text, bool protectedRecord = false)
        {
            var content = new PxzRecordContent(data, protectedRecord);
            var header = new PxzRecordHeader
            {
                Checksums = new PxzRecordChecksum(content),
                Size = new PxzRecordSize(content),
                Naming = new PxzRecordNaming
                {
                    RecordName = name,
                    StoredName = $"{Guid.NewGuid()}.record",
                    DataType = recordType
                }
            };

            ProtectedRecord = protectedRecord;
            Header = header;
            Content = content;
        }

        public PxzRecord(Image data, string name, bool protectedRecord = false)
        {
            var content = new PxzRecordContent(data, protectedRecord);
            var header = new PxzRecordHeader
            {
                Checksums = new PxzRecordChecksum(content),
                Size = new PxzRecordSize(content),
                Naming = new PxzRecordNaming
                {
                    RecordName = name,
                    StoredName = $"{Guid.NewGuid()}.record",
                    DataType = PxzRecordType.Bitmap
                }
            };

            ProtectedRecord = protectedRecord;
            Header = header;
            Content = content;
        }

        /// <summary>
        /// Create a new record from a GZipped Base64 string
        /// </summary>
        /// <param name="compressedRaw">The raw representation of the PxzRecord to load</param>
        /// <returns></returns>
        public static PxzRecord FromRawForm(byte[] compressedRaw)
        {
            var decompressedXml = GZipCompressor.DecompressBytes(compressedRaw);
            return Serializers.BytesToPxzRecord(decompressedXml);
        }

        /// <summary>
        /// Convert the current record to a GZipped byte array
        /// </summary>
        /// <returns></returns>
        public byte[] ToRawForm()
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
            return Serializers.BytesToPxzRecord(Encoding.UTF8.GetBytes(rawXml));
        }

        /// <summary>
        /// Decompresses, decrypts if necessary and extracts the record to a file
        /// </summary>
        /// <param name="filePath">The file to save to</param>
        /// <returns></returns>
        public bool ExtractRecord(string filePath)
        {
            try
            {
                //delete it if it already exists
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                //processes and stores the record's contents
                var content = Content.AutoRecord;

                //flush bytes to disk
                System.IO.File.WriteAllBytes(filePath, content);

                //success
                return true;
            }
            catch
            {
                //ignore all errors
            }

            //default
            return false;
        }
    }
}