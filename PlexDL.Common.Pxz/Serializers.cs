using PlexDL.Common.Pxz.Structures;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PlexDL.Common.Pxz
{
    public class Serializers
    {
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