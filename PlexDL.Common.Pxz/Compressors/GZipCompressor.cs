using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace PlexDL.Common.Pxz.Compressors
{
    public static class GZipCompressor
    {
        /// <summary>
        /// Compresses the byte data into a Base64 string
        /// </summary>
        /// <param name="buffer">The data to compress</param>
        /// <returns></returns>
        public static string CompressString(byte[] buffer)
        {
            return Convert.ToBase64String(CompressBytes(buffer));
        }

        /// <summary>
        /// Compresses the string into a Base64 string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string CompressString(string text)
        {
            return Convert.ToBase64String(CompressBytes(text));
        }

        /// <summary>
        /// Compresses the data into a byte array.
        /// </summary>
        /// <param name="buffer">The bytes to compress.</param>
        /// <returns></returns>
        public static byte[] CompressBytes(byte[] buffer)
        {
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);

            return gZipBuffer;
        }

        /// <summary>
        /// Compresses the string into a byte array.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static byte[] CompressBytes(string text)
        {
            var buffer = Encoding.UTF8.GetBytes(text);
            return CompressBytes(buffer);
        }

        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="compressedText">The compressed text.</param>
        /// <returns></returns>
        public static string DecompressString(string compressedText)
        {
            return Encoding.UTF8.GetString(DecompressBytes(compressedText));
        }

        /// <summary>
        /// Decompresses the string into a byte array.
        /// </summary>
        /// <param name="compressedText">The text to decompress (Base64)</param>
        /// <returns></returns>
        public static byte[] DecompressBytes(string compressedText)
        {
            var buffer = Convert.FromBase64String(compressedText);
            return DecompressBytes(buffer);
        }

        /// <summary>
        /// Decompresses the data into a byte array.
        /// </summary>
        /// <param name="buffer">The bytes to compress.</param>
        /// <returns></returns>
        public static byte[] DecompressBytes(byte[] buffer)
        {
            using (var memoryStream = new MemoryStream())
            {
                var dataLength = BitConverter.ToInt32(buffer, 0);
                memoryStream.Write(buffer, 4, buffer.Length - 4);

                var gZipBuffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(gZipBuffer, 0, gZipBuffer.Length);
                }

                return gZipBuffer;
            }
        }
    }
}