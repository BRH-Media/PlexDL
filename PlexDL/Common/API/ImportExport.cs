using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;

namespace PlexDL.Common.API
{
    public static class ImportExport
    {
        public static void MetadataToFile(string fileName, PlexObject contentToExport, bool silent = false)
        {
            try
            {
                var xsSubmit = new XmlSerializer(typeof(PlexObject));
                var xmlSettings = new XmlWriterSettings();
                var sww = new StringWriter();
                xmlSettings.Indent = true;
                xmlSettings.IndentChars = "\t";
                xmlSettings.OmitXmlDeclaration = false;

                xsSubmit.Serialize(sww, contentToExport);

                File.WriteAllText(fileName, sww.ToString());

                if (!silent)
                    MessageBox.Show("Successfully exported metadata!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (!silent)
                    MessageBox.Show("An error occurred\n\n" + ex, "Metadata Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "XMLMetadataSaveError");
            }
        }

        public static PlexObject MetadataFromFile(string fileName, bool silent = false)
        {
            try
            {
                PlexObject subReq;

                var serializer = new XmlSerializer(typeof(PlexObject));

                var reader = new StreamReader(fileName);
                subReq = (PlexObject)serializer.Deserialize(reader);
                reader.Close();
                return subReq;
            }
            catch (Exception ex)
            {
                if (!silent)
                    MessageBox.Show("An error occurred\n\n" + ex, "Metadata Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "XMLMetadataLoadError");
                return new PlexObject();
            }
        }
    }
}