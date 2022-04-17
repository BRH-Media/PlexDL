using Ionic.Zip;
using PlexDL.Common.Enums;
using PlexDL.Common.Pxz.Compressors;
using PlexDL.Common.Pxz.Enums;
using PlexDL.Common.Pxz.Extensions;
using PlexDL.Common.Pxz.Structures.File.Record;
using PlexDL.Common.Pxz.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UIHelpers;

// ReSharper disable UnusedMember.Global
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable InvertIf

namespace PlexDL.Common.Pxz.Structures.File
{
    public class PxzFile
    {
        /// <summary>
        /// Global PXZ indexing information
        /// </summary>
        public PxzIndex FileIndex { get; set; } = new PxzIndex();

        /// <summary>
        /// A list of all records currently in this PXZ file
        /// </summary>
        public List<PxzRecord> Records { get; } = new List<PxzRecord>();

        /// <summary>
        /// The location on disk of the current PXZ file (blank if this is new)
        /// </summary>
        public string Location { get; set; } = @"";

        /// <summary>
        /// Whether or not to show messages during parse (e.g. error messages/warnings)
        /// </summary>
        [XmlIgnore]
        public bool ParseSilent { get; set; } = false;

        public PxzFile()
        {
            //blank initialiser
        }

        public PxzFile(DevStatus status)
        {
            //apply the build state
            FileIndex.BuildState = status;
        }

        public PxzFile(Version systemVersion, DevStatus status)
        {
            //apply the build state
            FileIndex.BuildState = status;

            //apply the PlexDL version
            FileIndex.FormatVersion = systemVersion;
        }

        public PxzFile(IEnumerable<PxzRecord> records, DevStatus status) : this(records,
            new Version(0, 0, 0, 0), status)
        {
            //proxied call; does not contain code.
        }

        public PxzFile(IEnumerable<PxzRecord> records, Version systemVersion,
            DevStatus status)
        {
            //apply the build state
            FileIndex.BuildState = status;

            //apply the PlexDL version
            FileIndex.FormatVersion = systemVersion;

            //clear all indexing information
            FileIndex.RecordReference.Clear();
            Records.Clear();

            //re-add the indexing information with the new values
            foreach (var r in records)
            {
                FileIndex.RecordReference.Add(r.Header.Naming);
                Records.Add(r);
            }
        }

        /// <summary>
        /// Select all records of a specific type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<PxzRecord> SelectType(PxzRecordType type)
        {
            try
            {
                if (Records.Count > 0 && FileIndex.RecordReference.Count > 0)
                {
                    //store all record matches here
                    var matchedRecords = new List<PxzRecord>();

                    //go through each reference
                    foreach (var recRef in FileIndex.RecordReference)
                    {
                        //perform match
                        if (recRef.DataType == type)
                        {
                            //load the record in its entirety into memory
                            foreach (var recObj in Records)
                                if (recObj.Header.Naming.RecordName == recRef.RecordName &&
                                    recObj.Header.Naming.StoredName == recRef.StoredName)
                                {
                                    matchedRecords.Add(recObj);
                                    break;
                                }
                        }
                    }

                    //return the matches
                    return matchedRecords;
                }
            }
            catch
            {
                //nothing
            }

            //default
            return new List<PxzRecord>();
        }

        /// <summary>
        /// Show the PXZ Explorer with the current PXZ file loaded
        /// </summary>
        public void ShowInformation()
        {
            PxzExplorer.ShowPxzExplorer(this);
        }

        /// <summary>
        /// Load a record from the PXZ index
        /// </summary>
        /// <param name="recordName">Exact name of the record to load (not stored name)</param>
        /// <returns></returns>
        public PxzRecord LoadRecord(string recordName)
        {
            if (Records.Count <= 0 || FileIndex.RecordReference.Count <= 0) return null;

            return (from r in FileIndex.RecordReference
                    where r.RecordName == recordName
                    select FileIndex.RecordReference.IndexOf(r)
                into i
                    select Records[i]).FirstOrDefault();
        }

        /// <summary>
        /// Add a new PXZ record to this file
        /// </summary>
        /// <param name="record"></param>
        public void AddRecord(PxzRecord record)
        {
            Records.Add(record);
            FileIndex.RecordReference.Add(record.Header.Naming);
        }

        /// <summary>
        /// Remove a PXZ record object by instance
        /// </summary>
        /// <param name="record">Exact instance of the record to delete</param>
        public void RemoveRecord(PxzRecord record)
        {
            if (Records.Contains(record))
                Records.Remove(record);
        }

        /// <summary>
        /// Remove a PXZ record object by name
        /// </summary>
        /// <param name="recordName">Exact name of the record to delete (not stored name)</param>
        public void RemoveRecord(string recordName)
        {
            foreach (var r in FileIndex.RecordReference)
            {
                if (r.RecordName != recordName)
                    continue;

                var i = FileIndex.RecordReference.IndexOf(r);
                Records.RemoveAt(i);
            }
        }

        /// <summary>
        /// Save this PXZ file to disk
        /// </summary>
        /// <param name="path">The file to save to/create</param>
        public void Save(string path)
        {
            //delete the existing file (if it exists)
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            //new index attributes (don't override records though)
            FileIndex.Author = PxzAuthor.FromCurrent();

            var idxDoc = FileIndex.ToXml();

            //deflate the content to save room (poster isn't compressed via Zlib)
            var idxByte = GZipCompressor.CompressString(idxDoc.OuterXml);

            //names of root format items
            const string idxName = @"index";
            const string recName = @"records";

            //create a new temporary Zip file (in memory, not on disk)
            using var archive = new ZipFile(path, Encoding.Default);

            //add the indexing information to the PXZ
            archive.AddEntry(idxName, idxByte);

            //add records folder
            archive.AddDirectoryByName(recName);

            //traverse the records list
            foreach (var r in Records)
            {
                //null records are skipped
                if (r == null)
                    continue;

                //convert to record string
                var raw = r.ToRawForm();

                //record name
                var fileName = $"{recName}/{r.Header.Naming.StoredName}";

                //add the ZIP entry of the file
                if (!archive.Any(entry => entry.FileName.EndsWith(fileName)))
                    archive.AddEntry(fileName, raw);
            }

            //finalise ZIP file
            archive.Save(path);

            //cull the residual archive from memory
            archive.Dispose();
        }

        /// <summary>
        /// Load an existing PXZ file from disk
        /// </summary>
        /// <param name="path">The file to load</param>
        public void Load(string path)
        {
            try
            {
                //can't load a non-existent file
                if (!System.IO.File.Exists(path)) return;

                //clear all lists and assign the new location
                Location = path;
                Records.Clear();
                FileIndex.RecordReference.Clear();

                //item names to load
                const string idxName = @"index";
                const string dirName = @"records";

                //read the PXZ file into memory
                using var zip = ZipFile.Read(path);

                //load the raw file index into memory
                var idxFile = zip[idxName];

                //the index can't be null!
                if (idxFile == null)
                {
                    if (!ParseSilent)
                        UIMessages.Error(@"PXZ index couldn't be found or it isn't valid");
                    return;
                }

                //raw gzip byte container
                byte[] idxData;

                //extract and save the index to a new stream
                using (var idxStream = new MemoryStream())
                {
                    idxFile.Extract(idxStream); //ERROR LINE
                    idxStream.Position = 0;
                    idxData = idxStream.ToArray();
                }

                //Gzip decompress
                var idxByte = GZipCompressor.DecompressBytes(idxData);
                var index = Serializers.BytesToPxzIndex(idxByte);

                //if this gets flipped to true, it's potentially bad news
                var tamperedWith = false;

                //load all records into memory
                foreach (var recFile in index.RecordReference.Select(
                        r => $"{dirName}/{r.StoredName}")
                    .Select(recName => zip[recName]))
                {
                    //byte stream for the raw record
                    using var recStream = new MemoryStream();

                    //grab data from the file and store in memory
                    recFile.Extract(recStream);
                    recStream.Position = 0;

                    //jump through the memory stream
                    var recRaw = recStream.ToArray();

                    //serialise rawXml into a usable PxzRecord
                    var rec = PxzRecord.FromRawForm(recRaw);

                    //sync Encrypted with ProtectedRecord (native object can't do this)
                    rec.ProtectedRecord = rec.Content.Encrypted;

                    //verify checksums on the record to ensure authenticity
                    tamperedWith = !rec.ChecksumValid;

                    //finally, index and store the record
                    Records.Add(rec);
                    FileIndex.RecordReference.Add(rec.Header.Naming);
                }

                //apply new values
                FileIndex = index;

                //check authenticity flag, and warn if it's been altered
                if (tamperedWith)
                    if (!ParseSilent)
                        UIMessages.Warning("Content has been modified\n\nThe MD5 checksums stored do not match the checksums calculated, which is an indication that the contents may have been altered outside of the PXZ handler. The file will continue loading after you close this message.");
            }
            catch (Exception ex)
            {
                if (!ParseSilent)
                    UIMessages.Error(ex.ToString());
            }
        }
    }
}