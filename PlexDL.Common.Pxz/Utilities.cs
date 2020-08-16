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

        public static PxzAuthor FromCurrent()
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