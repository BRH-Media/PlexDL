using Ionic.Zip;
using PlexDL.Common.Pxz.Compressors;
using PlexDL.Common.Pxz.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIHelpers;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzFile
    {
        public PxzIndex FileIndex { get; set; } = new PxzIndex();

        public List<PxzRecord> Records { get; } = new List<PxzRecord>();
        public string Location { get; set; } = @"";

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

        public void AddRecord(PxzRecord record)
        {
            Records.Add(record);
            FileIndex.RecordReference.Add(record.Header.Naming);
        }

        public void RemoveRecord(PxzRecord record)
        {
            if (Records.Contains(record)) Records.Remove(record);
        }

        public void RemoveRecord(string recordName)
        {
            foreach (var r in FileIndex.RecordReference)
            {
                if (r.RecordName != recordName) continue;

                var i = FileIndex.RecordReference.IndexOf(r);
                Records.RemoveAt(i);
            }
        }

        public void Save(string path)
        {
            //delete the existing file (if it exists)
            if (File.Exists(path))
                File.Delete(path);

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