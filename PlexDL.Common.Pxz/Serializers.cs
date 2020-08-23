using PlexDL.Common.Pxz.Structures;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PlexDL.Common.Pxz
{
    public class Serializers
    {
        public static XmlDocument PxzRecordToXml(PxzRecord record)
        {
            try
            {
                var xsSubmit = new XmlSerializer(typeof(PxzRecord));
                var xmlSettings = new XmlWriterSettings();
                var sww = new StringWriter();
                xmlSettings.Indent = true;
                xmlSettings.IndentChars = "\t";
                xmlSettings.OmitXmlDeclaration = false;

                xsSubmit.Serialize(sww, record);

                var doc = new XmlDocument();
                doc.LoadXml(sww.ToString());

                return doc;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static PxzRecord StringToPxzRecord(string record)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(PxzRecord));

                var reader = new StringReader(record);
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
                xmlSettings.Indent = true;
                xmlSettings.IndentChars = "\t";
                xmlSettings.OmitXmlDeclaration = false;

                xsSubmit.Serialize(sww, index);

                var doc = new XmlDocument();
                doc.LoadXml(sww.ToString());

                return doc;
            }
            catch
            {
                return null;
            }
        }

        public static PxzIndex StringToPxzIndex(string index)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(PxzIndex));

                var reader = new StringReader(index);
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