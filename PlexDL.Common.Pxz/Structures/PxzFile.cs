using Ionic.Zip;
using PlexDL.Common.Pxz.Compressors;
using PlexDL.Common.Pxz.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzFile
    {
        public PxzIndex FileIndex { get; set; } = new PxzIndex();

        private List<PxzRecord> Records { get; } = new List<PxzRecord>();
        public string Location { get; set; } = @"";

        public PxzFile()
        {
            //blank initializer
        }

        public PxzFile(ICollection<PxzRecord> records)
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
                if (r.RecordName == recordName)
                {
                    var i = FileIndex.RecordReference.IndexOf(r);
                    Records.RemoveAt(i);
                }
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

            using (var archive = new ZipFile(path, Encoding.Default))
            {
                archive.AddEntry(idxName, idxByte);

                //add records folder
                archive.AddDirectoryByName(recName);

                //traverse the records list
                foreach (var r in Records)
                {
                    var raw = r.ToRawForm();
                    var fileName = $"{recName}/{r.Header.Naming.StoredName}";
                    archive.AddEntry(fileName, raw);
                }

                //finalise ZIP file
                archive.Save(path);
            }
        }

        public PxzRecord LoadRecord(string recordName)
        {
            if (Records.Count > 0 && FileIndex.RecordReference.Count > 0)
                foreach (var r in FileIndex.RecordReference)
                {
                    if (r.RecordName == recordName)
                    {
                        var i = FileIndex.RecordReference.IndexOf(r);
                        return Records[i];
                    }
                }

            return null;
        }

        public void Load(string path)
        {
            if (!File.Exists(path)) return;

            Location = path;
            Records.Clear();
            FileIndex.RecordReference.Clear();

            const string idxName = @"index";
            const string dirName = @"records";

            using (var zip = ZipFile.Read(path))
            {
                var idxFile = zip[idxName];

                string idxString;

                using (var idxStream = new MemoryStream())
                {
                    idxFile.Extract(idxStream);
                    idxStream.Position = 0;
                    idxString = new StreamReader(idxStream).ReadToEnd();
                }

                //Gzip decompress
                var idxByte = GZipCompressor.DecompressString(idxString);
                var index = Serializers.StringToPxzIndex(idxByte);

                //load all records into memory
                foreach (var r in index.RecordReference)
                {
                    var recName = $"{dirName}/{r.StoredName}";

                    var recFile = zip[recName];

                    using (var recStream = new MemoryStream())
                    {
                        recFile.Extract(recStream);
                        recStream.Position = 0;
                        var rec = PxzRecord.FromRawForm(new StreamReader(recStream).ReadToEnd());

                        Records.Add(rec);
                        FileIndex.RecordReference.Add(rec.Header.Naming);
                    }
                }

                //apply new values
                FileIndex = index;
            }
        }
    }
}