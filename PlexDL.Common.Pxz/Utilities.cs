using PlexDL.Common.Pxz.Structures;
using System;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace PlexDL.Common.Pxz
{
    public static class Utilities
    {
        public static string FormatBytes(long bytes, bool includeSpace = false)
        {
            string[] suffix =
            {
                "B", "KB", "MB", "GB", "TB"
            };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < suffix.Length && bytes >= 1024; i++, bytes /= 1024)
                dblSByte = bytes / 1024.0;

            return includeSpace ? $"{dblSByte:0.##} {suffix[i]}" : $"{dblSByte:0.##}{suffix[i]}";
        }

        public static Version GetVersion()
        {
            var assembly = Assembly.GetCallingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return new Version(fileVersionInfo.ProductVersion);
        }

        //https://stackoverflow.com/questions/17833080/convert-string-to-filestream-in-c-sharp
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static byte[] ImageToByte(Image img)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static Bitmap ByteToImage(byte[] img)
        {
            using (var mStream = new MemoryStream(img))
            {
                return (Bitmap)Image.FromStream(mStream);
            }
        }

        public static PxzAuthor AuthorFromCurrent()
        {
            try
            {
                return new PxzAuthor
                {
                    MachineName = Environment.MachineName,
                    UserAccount = Environment.UserName,
                    DisplayName = CurrentDisplayName()
                };
            }
            catch
            {
                return null;
            }
        }

        public static string CurrentDisplayName()
        {
            var p = UserPrincipal.Current;
            return p.DisplayName;
        }
    }
}