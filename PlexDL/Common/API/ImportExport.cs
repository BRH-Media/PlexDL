using PlexDL.Common.Logging;
using PlexDL.Common.Pxz.Structures;
using PlexDL.Common.Structures.Plex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UIHelpers;

namespace PlexDL.Common.API
{
    public static class ImportExport
    {
        public static XmlDocument ToXml(this PlexObject obj)
        {
            try
            {
                var xsSubmit = new XmlSerializer(typeof(PlexObject));
                var xmlSettings = new XmlWriterSettings();
                var sww = new StringWriter();
                xmlSettings.Indent = true;
                xmlSettings.IndentChars = "\t";
                xmlSettings.OmitXmlDeclaration = false;

                xsSubmit.Serialize(sww, obj);

                var doc = new XmlDocument();
                doc.LoadXml(sww.ToString());

                return doc;
            }
            catch
            {
                return null;
            }
        }

        public static PlexObject FromXml(XmlDocument obj)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(PlexObject));

                var reader = new StringReader(obj.OuterXml);
                var subReq = (PlexObject)serializer.Deserialize(reader);
                reader.Close();

                return subReq;
            }
            catch
            {
                return null;
            }
        }

        public static void MetadataToFile(string fileName, PlexObject contentToExport, bool silent = false)
        {
            try
            {
                //try and obtain a poster
                var p = ImageHandler.GetPoster(contentToExport);

                var rawMetadata = new PxzRecord(contentToExport.RawMetadata, @"raw");
                var objMetadata = new PxzRecord(contentToExport.ToXml(), @"obj");
                var ptrMetadata = new PxzRecord(p, @"poster");

                var data = new List<PxzRecord>()
                {
                    rawMetadata,
                    objMetadata,
                    ptrMetadata
                };

                var pxz = new PxzFile(data);
                pxz.Save(fileName);

                if (!silent)
                    UIMessages.Info(@"Successfully exported metadata!");
            }
            catch (Exception ex)
            {
                if (!silent)
                    UIMessages.Error("An error occurred\n\n" + ex, @"Metadata Export Error");
                LoggingHelpers.RecordException(ex.Message, "XMLMetadataSaveError");
            }
        }

        public static PxzFile LoadMetadataArchive(string fileName)
        {
            try
            {
                var pxz = new PxzFile();
                pxz.Load(fileName);

                return pxz;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "XMLMetadataLoadError");
                return null;
            }
        }

        public static PlexObject MetadataFromFile(string fileName, bool silent = false)
        {
            return MetadataFromFile(LoadMetadataArchive(fileName), silent);
        }

        public static PlexObject MetadataFromFile(PxzFile file, bool silent = false)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(PlexObject));

                var rec = file.LoadRecord(@"obj");

                var reader = new StringReader(rec.Content.ToXmlDocument().OuterXml);
                var subReq = (PlexObject)serializer.Deserialize(reader);
                reader.Close();

                //apply raw XML from PXZ file
                subReq.RawMetadata = file.LoadRecord(@"raw").Content.ToXmlDocument();

                return subReq;
            }
            catch (Exception ex)
            {
                if (!silent)
                    UIMessages.Error("An error occurred\n\n" + ex, @"Metadata Load Error");
                LoggingHelpers.RecordException(ex.Message, "XMLMetadataLoadError");
                return new PlexObject();
            }
        }
    }
}