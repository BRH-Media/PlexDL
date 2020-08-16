using Ionic.Zip;
using PlexDL.Common.Pxz.Compressors;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzFile
    {
        public XmlDocument RawMetadata { get; set; }
        public XmlDocument ObjMetadata { get; set; }
        public PxzIndex FileIndex { get; set; } = new PxzIndex();
        public Bitmap Poster { get; set; }

        public PxzFile()
        {
            //blank initializer
        }

        public PxzFile(XmlDocument raw, XmlDocument obj, Bitmap ptr = null, PxzIndex idx = null)
        {
            if (idx != null) FileIndex = idx;
            if (ptr != null)
                Poster = ptr;
            else
            {
                //make a new index (it wasn't specified)
                var a = Utilities.FromCurrent();
                var i = new PxzIndex
                {
                    Author = a,
                    FormatVersion = Utilities.GetVersion()
                };

                //apply newly generated index
                FileIndex = i;
            }
            RawMetadata = raw;
            ObjMetadata = obj;
        }

        public void Save(string path)
        {
            //delete the existing file (if it exists)
            if (File.Exists(path))
                File.Delete(path);

            var idxDoc = FileIndex.ToXml();
            var rawDoc = RawMetadata;
            var objDoc = ObjMetadata;

            //deflate the content to save room (poster isn't compressed via Zlib)
            var idxByte = GZipCompressor.CompressString(idxDoc.OuterXml);
            var rawByte = GZipCompressor.CompressString(rawDoc.OuterXml);
            var objByte = GZipCompressor.CompressString(objDoc.OuterXml);
            var ptrByte = Poster != null ? Utilities.ImageToByte(Poster) : null;

            const string idxName = @"idx";
            const string rawName = @"raw";
            const string objName = @"obj";
            const string ptrName = @"ptr";

            using (var archive = new ZipFile(path, Encoding.Default))
            {
                archive.AddEntry(idxName, idxByte);
                archive.AddEntry(rawName, rawByte);
                archive.AddEntry(objName, objByte);
                if (ptrByte != null) archive.AddEntry(ptrName, ptrByte);

                //finalise ZIP file
                archive.Save(path);
            }
        }

        public void Load(string path)
        {
            if (!File.Exists(path)) return;

            const string idxName = @"idx";
            const string rawName = @"raw";
            const string objName = @"obj";
            const string ptrName = @"ptr";

            using (var zip = ZipFile.Read(path))
            {
                var idxFile = zip[idxName];
                var rawFile = zip[rawName];
                var objFile = zip[objName];
                var ptrFile = zip.ContainsEntry(ptrName) ? zip[ptrName] : null;

                string idxString;
                string rawString;
                string objString;
                Bitmap ptrBytes = null;

                using (var idxStream = new MemoryStream())
                {
                    idxFile.Extract(idxStream);
                    idxStream.Position = 0;
                    idxString = new StreamReader(idxStream).ReadToEnd();
                }

                using (var rawStream = new MemoryStream())
                {
                    rawFile.Extract(rawStream);
                    rawStream.Position = 0;
                    rawString = new StreamReader(rawStream).ReadToEnd();
                }

                if (ptrFile != null)
                    using (var ptrStream = new MemoryStream())
                    {
                        ptrFile.Extract(ptrStream);
                        ptrStream.Position = 0;
                        ptrBytes = Utilities.ByteToImage(ptrStream.ToArray());
                    }

                using (var objStream = new MemoryStream())
                {
                    objFile.Extract(objStream);
                    objStream.Position = 0;
                    objString = new StreamReader(objStream).ReadToEnd();
                }

                //Zlib decompress
                var idxByte = GZipCompressor.DecompressString(idxString);
                var rawByte = GZipCompressor.DecompressString(rawString);
                var objByte = GZipCompressor.DecompressString(objString);

                //apply new values
                FileIndex = Serializers.StringToPxzIndex(idxByte);

                var objDoc = new XmlDocument();
                objDoc.LoadXml(objByte);

                var rawDoc = new XmlDocument();
                rawDoc.LoadXml(rawByte);

                RawMetadata = rawDoc;
                ObjMetadata = objDoc;
                Poster = ptrBytes;
            }
        }
    }
}