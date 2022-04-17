using PlexDL.Common.Pxz.Compressors;
using PlexDL.Common.Pxz.Structures;
using PlexDL.Common.Pxz.Structures.File.Record;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

// ReSharper disable RedundantIfElseBlock
// ReSharper disable InvertIf

namespace PlexDL.Common.Pxz
{
    public static class Serializers
    {
        /// <summary>
        /// Can automatically decompress XML if it is compressed
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public static string AutoXml(byte[] raw)
        {
            try
            {
                //byte conversion
                var rawString = Encoding.UTF8.GetString(raw);

                //validation
                if (!string.IsNullOrEmpty(rawString))
                {
                    //test for validity
                    var isAlreadyXml = IsValidXml(rawString);

                    //run accordingly
                    if (isAlreadyXml)
                        return rawString;
                    else
                    {
                        //decompression may now be required
                        var decompressed = Encoding.UTF8.GetString(GZipCompressor.DecompressBytes(raw));

                        //test for XML validity
                        if (IsValidXml(decompressed))
                            return decompressed;
                    }
                }
            }
            catch (Exception)
            {
                //nothing
            }

            //default
            return @"";
        }

        public static bool IsValidXml(string xml)
        {
            //validation
            if (!string.IsNullOrEmpty(xml) && xml.TrimStart().StartsWith("<"))
            {
                try
                {
                    //parse will fail if it's not valid XML
                    var doc = XDocument.Parse(xml);

                    //parse succeeded; must be valid XML.
                    return true;
                }
                catch (Exception)
                {
                    //parse failed; must be invalid XML.
                    return false;
                }
            }

            //default
            return false;
        }

        public static XmlDocument PxzRecordToXml(PxzRecord record)
        {
            try
            {
                var xsSubmit = new XmlSerializer(typeof(PxzRecord));
                var xmlSettings = new XmlWriterSettings();
                var sww = new StringWriter();
                var doc = new XmlDocument();

                xmlSettings.Indent = true;
                xmlSettings.IndentChars = "\t";
                xmlSettings.OmitXmlDeclaration = false;

                xsSubmit.Serialize(sww, record);
                doc.LoadXml(sww.ToString());

                return doc;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static PxzRecord BytesToPxzRecord(byte[] record)
        {
            try
            {
                var xmlRecord = AutoXml(record);
                var serializer = new XmlSerializer(typeof(PxzRecord));
                var reader = new StringReader(xmlRecord);
                var subReq = (PxzRecord)serializer.Deserialize(reader);

                reader.Close();

                return subReq;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static XmlDocument PxzIndexToXml(PxzIndex index)
        {
            try
            {
                var xsSubmit = new XmlSerializer(typeof(PxzIndex));
                var xmlSettings = new XmlWriterSettings();
                var sww = new StringWriter();
                var doc = new XmlDocument();

                xmlSettings.Indent = true;
                xmlSettings.IndentChars = "\t";
                xmlSettings.OmitXmlDeclaration = false;

                xsSubmit.Serialize(sww, index);
                doc.LoadXml(sww.ToString());

                return doc;
            }
            catch
            {
                return null;
            }
        }

        public static PxzIndex BytesToPxzIndex(byte[] index)
        {
            try
            {
                var xmlIndex = AutoXml(index);
                var serializer = new XmlSerializer(typeof(PxzIndex));
                var reader = new StringReader(xmlIndex);
                var subReq = (PxzIndex)serializer.Deserialize(reader);

                reader.Close();

                return subReq;
            }
            catch
            {
                return null;
            }
        }
    }
}