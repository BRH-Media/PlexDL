using Ionic.Zip;
using PlexDL.Common.Pxz.Compressors;
using PlexDL.Common.Pxz.Extensions;
using PlexDL.Common.Pxz.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UIHelpers;

namespace PlexDL.Common.Pxz.Structures
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
            //blank initializer
        }

        public PxzFile(IEnumerable<PxzRecord> records)
        {
            FileIndex.RecordReference.Clear();
            Records.Clear();

            foreach (var r in records)
            {
                FileIndex.RecordReference.Add(r.Header.Naming);
                Records.Add(r);
            }
        }

        /// <summary>
        /// Show the PXZ Explorer with the current PXZ file loaded
        /// </summary>
        public void ShowInformation()
        {
            PxzInformation.ShowPxzInformation(this);
        }

        /// <summary>
        /// Load a record from the PXZ index
        /// </summary>
        /// <param name="recordName">Exact name of the record to load (not stored name)</param>
        /// <returns></returns>
        public PxzRecord LoadRecord(string recordName)
        {
            if (Records.Count <= 0 || FileIndex.RecordReference.Count <= 0) return null;

            foreach (var r in FileIndex.RecordReference)
            {
                if (r.RecordName != recordName) continue;

                var i = FileIndex.RecordReference.IndexOf(r);
                return Records[i];
            }

            return null;
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
            if (Records.Contains(record)) Records.Remove(record);
        }

        /// <summary>
        /// Remove a PXZ record object by name
        /// </summary>
        /// <param name="recordName">Exact name of the record to delete (not stored name)</param>
        public void RemoveRecord(string recordName)
        {
            foreach (var r in FileIndex.RecordReference)
            {
                if (r.RecordName != recordName) continue;

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
            if (File.Exists(path))
                File.Delete(path);

            //new index attributes (don't override records though)
            FileIndex.Author = PxzAuthor.FromCurrent();
            FileIndex.FormatVersion = Utilities.GetVersion();

            var idxDoc = FileIndex.ToXml();

            //deflate the content to save room (poster isn't compressed via Zlib)
            var idxByte = GZipCompressor.CompressString(idxDoc.OuterXml);

            const string idxName = @"index";
            const string recName = @"records";

            using var archive = new ZipFile(path, Encoding.Default);

            archive.AddEntry(idxName, idxByte);

            //add records folder
            archive.AddDirectoryByName(recName);

            //traverse the records list
            foreach (var r in Records)
            {
                if (r == null) continue;

                var raw = r.ToRawForm();
                var fileName = $"{recName}/{r.Header.Naming.StoredName}";
                archive.AddEntry(fileName, raw);
            }

            //finalise ZIP file
            archive.Save(path);
        }

        /// <summary>
        /// Load an existing PXZ file from disk
        /// </summary>
        /// <param name="path">The file to load</param>
        public void Load(string path)
        {
            try
            {
                if (!File.Exists(path)) return;

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
                    UIMessages.Error(@"PXZ index couldn't be found or it wasn't valid");
                    return;
                }

                string idxString;

                //extract and save the index to a new stream
                using (var idxStream = new MemoryStream())
                {
                    idxFile.Extract(idxStream); //ERROR LINE
                    idxStream.Position = 0;
                    idxString = new StreamReader(idxStream).ReadToEnd();
                }

                //Gzip decompress
                var idxByte = GZipCompressor.DecompressString(idxString);
                var index = Serializers.StringToPxzIndex(idxByte);

                //if this gets flipped to true, it's potentially bad news
                var tamperedWith = false;

                //load all records into memory
                foreach (var recFile in index.RecordReference.Select(r => $"{dirName}/{r.StoredName}")
                    .Select(recName => zip[recName]))
                {
                    using var recStream = new MemoryStream();

                    //grab data from the file and store in memory
                    recFile.Extract(recStream);
                    recStream.Position = 0;

                    //open a new reader for the memory block
                    using var reader = new StreamReader(recStream);

                    //jump through the memory stream and turn it into Base64
                    var recRaw = reader.ReadToEnd();

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
                    UIMessages.Warning("Content has been modified\n\nThe MD5 checksums stored do not match the checksums calculated, which is an indication that the contents may have been altered outside of the PXZ handler. The file will continue loading after you close this message.");
            }
            catch (Exception ex)
            {
                UIMessages.Error(ex.ToString());
            }
        }
    }
}