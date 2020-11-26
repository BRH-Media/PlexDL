using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Pxz.Enums;
using PlexDL.Common.Pxz.Structures;
using PlexDL.Common.Security;
using PlexDL.Common.Structures.Plex;
using PlexDL.ResourceProvider.Properties;
using PlexDL.WaitWindow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using UIHelpers;

// ReSharper disable InconsistentNaming

namespace PlexDL.Common.API.IO
{
    public static class MetadataIO
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

                //create each new PXZ record in memory
                var rawMetadata = new PxzRecord(contentToExport.RawMetadata, @"raw");
                var objMetadata = new PxzRecord(contentToExport.ToXml(), @"obj");
                var ptrMetadata = new PxzRecord(p, @"poster");

                //the records to save to the PXZ file are contained in a list
                var data = new List<PxzRecord>
                {
                    rawMetadata,
                    objMetadata,
                    ptrMetadata
                };

                //export the actor images (if any)
                if (contentToExport.Actors != null)
                    if (contentToExport.Actors.Count > 0)
                    {
                        //loop through each actor and attempt an image download
                        foreach (var a in contentToExport.Actors)
                        {
                            //download
                            var image = ImageHandler.GetImageFromUrl(a.ThumbnailUri);

                            //verify
                            if (image != Resources.image_not_available_png_8)
                            {
                                //create a new record for the image
                                var record = new PxzRecord(image, $"actor_{Md5Helper.CalculateMd5Hash(a.ThumbnailUri)}");

                                //add it to the collection
                                data.Add(record);
                            }
                        }
                    }

                //embedded in PXZ indexing information
                var plexdlVersion = Assembly.GetEntryAssembly()?.GetName().Version;

                //initialise the PXZ file and flush it to disk
                var pxz = new PxzFile(data, plexdlVersion, BuildState.State);
                pxz.Save(fileName);

                //show a message indicating success if allowed
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
            }

            //default
            return null;
        }

        public static PlexObject MetadataFromFile(PxzFile file, bool waitWindow = true, bool silent = false)
        {
            if (waitWindow)
            {
                var waitArgs = new object[] { file, silent };
                return (PlexObject)WaitWindow.WaitWindow.Show(MetadataFromFile, @"Reading data", waitArgs);
            }

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

                        //apply poster from PXZ file
                        obj.StreamInformation.ContentThumbnail = file.LoadRecord(@"poster").Content.ToImage();

                        //check if the PXZ has any actor thumbnails stored
                        var bitmaps = file.SelectType(PxzRecordType.Bitmap);

                        //find any actor images
                        if (bitmaps != null)
                            foreach (var b in bitmaps)
                            {
                                //check if the record is an actor thumbnail
                                if (b.Header.Naming.RecordName.StartsWith(@"actor_"))
                                {
                                    //find out which actor this image belongs to
                                    foreach (var a in obj.Actors)
                                    {
                                        //record naming is just the URL hashed
                                        var recordName = $"actor_{Md5Helper.CalculateMd5Hash(a.ThumbnailUri)}";

                                        //verify
                                        if (recordName == b.Header.Naming.RecordName)
                                        {
                                            //apply image
                                            a.Thumbnail = b.Content.ToImage();
                                            break;
                                        }
                                    }
                                }
                            }

                        //return the new PlexObject
                        return obj;
                    }
                }
                catch (Exception ex)
                {
                    if (!silent)
                        UIMessages.Error("An error occurred\n\n" + ex, @"Metadata Load Error");
                    LoggingHelpers.RecordException(ex.Message, "XmlMetadataLoadError");
                }
            }

            //default
            return null;
        }
    }
}