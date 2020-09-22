using PlexDL.Common.Logging;
using PlexDL.Common.Pxz.Structures;
using PlexDL.Common.Structures.Plex;
using PlexDL.WaitWindow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UIHelpers;

namespace PlexDL.Common.API
{
    public static class MetadataImportExport
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

        public static void MetadataToFile(string fileName, PlexObject contentToExport, Bitmap poster = null, bool silent = false)
        {
            try
            {
                //try and obtain a poster if one wasn't provided
                var p = poster ?? ImageHandler.GetPoster(contentToExport);

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
                LoggingHelpers.RecordException(ex.Message, "XmlMetadataSaveError");
            }
        }

        private static void LoadMetadataArchive(object sender, WaitWindowEventArgs e)
        {
            if (e.Arguments.Count > 0)
            {
                var fileName = (string)e.Arguments[0];
                e.Result = LoadMetadataArchive(fileName, false);
            }
        }

        public static PxzFile LoadMetadataArchive(string fileName, bool waitWindow = true)
        {
            if (waitWindow)
            {
                var waitArgs = new object[] { fileName };
                return (PxzFile)WaitWindow.WaitWindow.Show(LoadMetadataArchive, @"Loading PXZ", waitArgs);
            }
            else
            {
                try
                {
                    var pxz = new PxzFile();
                    pxz.Load(fileName);

                    return pxz;
                }
                catch (Exception ex)
                {
                    LoggingHelpers.RecordException(ex.Message, "XmlMetadataLoadError");
                    return null;
                }
            }
        }

        public static void MetadataFromFile(object sender, WaitWindowEventArgs e)
        {
            if (e.Arguments.Count > 1)
            {
                var file = (PxzFile)e.Arguments[0];
                var silent = (bool)e.Arguments[1];
                e.Result = MetadataFromFile(file, false, silent);
            }
        }

        public static PlexObject MetadataFromFile(string fileName, bool waitWindow = true, bool silent = false)
        {
            try
            {
                //there are two file-types: the legacy PMXML format and the new PXZ format
                var ext = Path.GetExtension(fileName);

                //decide which is which
                switch (ext)
                {
                    case @".pxz": //must be decompressed and processed first
                        return MetadataFromFile(LoadMetadataArchive(fileName, waitWindow), waitWindow, silent);

                    case @".pmxml": //can be directly loaded and deserialised
                        var doc = new XmlDocument();
                        doc.LoadXml(File.ReadAllText(fileName));
                        return FromXml(doc);

                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"XmlMetadataLoadError");
                return null;
            }
        }

        public static PlexObject MetadataFromFile(PxzFile file, bool waitWindow = true, bool silent = false)
        {
            if (waitWindow)
            {
                var waitArgs = new object[] { file, silent };
                return (PlexObject)WaitWindow.WaitWindow.Show(MetadataFromFile, @"Reading data", waitArgs);
            }
            else
            {
                if (file != null)
                {
                    try
                    {
                        //load the object record (encoded XML that contains metadata/attributes)
                        var rec = file.LoadRecord(@"obj");

                        //it'll be null if the load above failed for whatever reason
                        if (rec != null)
                        {
                            var obj = FromXml(rec.Content.ToXmlDocument());

                            //apply raw XML from PXZ file
                            obj.RawMetadata = file.LoadRecord(@"raw").Content.ToXmlDocument();

                            //return the new PlexObject
                            return obj;
                        }
                        else
                        {
                            //if it was null, we can't parse it. Return null as the PlexObject
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!silent)
                            UIMessages.Error("An error occurred\n\n" + ex, @"Metadata Load Error");
                        LoggingHelpers.RecordException(ex.Message, "XmlMetadataLoadError");
                        return new PlexObject();
                    }
                }
                else
                    //if it was null, we can't parse it. Return null as the PlexObject
                    return null;
            }
        }
    }
}