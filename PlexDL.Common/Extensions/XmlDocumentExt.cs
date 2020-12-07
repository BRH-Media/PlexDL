using System.IO;
using System.Xml;

namespace PlexDL.Common.Extensions
{
    public static class XmlDocumentExt
    {
        public static XmlDocument ToXmlDocument(this XmlTextReader reader)
        {
            //Declare a new XMLDocument
            var xmlDoc = new XmlDocument();

            //load the document contents from the provided stream
            xmlDoc.Load(reader);

            //close the stream
            reader.Close();

            //return final doc
            return xmlDoc;
        }

        public static XmlDocument ToXmlDocument(this Stream dataStream)
        {
            //load stream into XMLReader
            var objXmlReader = new XmlTextReader(dataStream);

            //using the method above
            return objXmlReader.ToXmlDocument();
        }

        public static XmlDocument ToXmlDocument(this string xmlString)
        {
            //load string into XmlReader
            var objXml = new XmlDocument();
            objXml.LoadXml(xmlString);

            //using the method above
            return objXml;
        }
    }
}