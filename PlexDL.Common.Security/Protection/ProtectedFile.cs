using PlexDL.Common.Enums;
using System.IO;
using System.Text;

namespace PlexDL.Common.Security.Protection
{
    public class ProtectedFile : ProtectedData
    {
        public string FileName { get; set; }

        private const string DELIMITER = @"protected__";

        public bool IsProtected => File.Exists(FileName) && File.ReadAllText(FileName).StartsWith(DELIMITER);

        public void WriteAllBytes(byte[] contents, bool encrypt = true)
        {
            //don't perform any operations on a null file
            if (string.IsNullOrEmpty(FileName)) return;

            try
            {
                //delete it if it already exists
                if (File.Exists(FileName))
                    File.Delete(FileName);

                var toWrite =
                    encrypt
                        ? Encoding.Default.GetBytes(Encrypt(contents))
                        : contents;
                File.WriteAllBytes(FileName, toWrite);
            }
            catch
            {
                //ignore all errors
            }
        }

        public void WriteAllText(string contents, bool encrypt = true)
        {
            //don't perform any operations on a null file
            if (string.IsNullOrEmpty(FileName)) return;

            try
            {
                //delete it if it already exists
                if (File.Exists(FileName))
                    File.Delete(FileName);

                var toWrite =
                    encrypt
                        ? Encrypt(contents)
                        : contents;

                File.WriteAllText(FileName, toWrite);
            }
            catch
            {
                //ignore all errors
            }
        }

        public string ReadAllText()
        {
            try
            {
                var c = File.ReadAllText(FileName);
                var value =
                    IsProtected
                        ? Decrypt(c)
                        : c;
                return value;
            }
            catch
            {
                //ignore all errors
            }

            //default
            return @"";
        }

        public byte[] ReadAllBytes()
        {
            try
            {
                var c = File.ReadAllBytes(FileName);
                var value =
                        IsProtected
                            ? Encoding.Default.GetBytes(Decrypt(c))
                            : c;
                return value;
            }
            catch
            {
                //ignore all errors
            }

            //default
            return null;
        }

        public string Encrypt(byte[] content)
        {
            return Encrypt(Encoding.Default.GetString(content));
        }

        public string Encrypt(string content)
        {
            try
            {
                var handler = new ProtectedString(content, ProtectionMode.Encrypt);
                return $"{DELIMITER}{handler.ProcessedValue}";
            }
            catch
            {
                //ignore all errors
            }

            //default
            return @"";
        }

        public string Decrypt(byte[] protectedContent)
        {
            return Decrypt(Encoding.Default.GetString(protectedContent));
        }

        public string Decrypt(string protectedContent)
        {
            try
            {
                var base64Contents = protectedContent.Remove(0, DELIMITER.Length);
                var handler = new ProtectedString(base64Contents, ProtectionMode.Decrypt);
                return handler.ProcessedValue;
            }
            catch
            {
                //ignore all errors
            }

            //default
            return @"";
        }

        public ProtectedFile(string fileName)
        {
            FileName = fileName;
        }
    }
}