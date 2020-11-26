using Ionic.Zlib;
using System.IO;

namespace PlexDL.Common.Pxz.Compressors
{
    public static class ZlibCompressor
    {
        //https://stackoverflow.com/questions/37117985/gzipstream-memory-stream-to-file
        public static string CompressString(string inflatedString)
        {
            using var compressedMemStream = new MemoryStream();
            using (var originalStringStream = Utilities.GenerateStreamFromString(inflatedString))
            using (var compressionStream = new ZlibStream(
                compressedMemStream,
                CompressionMode.Compress,
                true))
            {
                originalStringStream.CopyTo(compressionStream);
            }
            compressedMemStream.Seek(0, SeekOrigin.Begin);

            return new StreamReader(compressedMemStream).ReadToEnd();
        }

        //https://stackoverflow.com/questions/37117985/gzipstream-memory-stream-to-file
        public static string DecompressString(string deflatedString)
        {
            using var compressedMemStream = new MemoryStream();
            using (var originalStringStream = Utilities.GenerateStreamFromString(deflatedString))
            using (var compressionStream = new ZlibStream(
                compressedMemStream,
                CompressionMode.Decompress,
                true))
            {
                originalStringStream.CopyTo(compressionStream);
            }
            compressedMemStream.Seek(0, SeekOrigin.Begin);

            return new StreamReader(compressedMemStream).ReadToEnd();
        }
    }
}