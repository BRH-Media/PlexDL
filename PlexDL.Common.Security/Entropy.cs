using System;
using System.IO;
using System.Security.Cryptography;

namespace PlexDL.Common.Security
{
    public static class Entropy
    {
        private static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string EntropyFileLocation { get; } = $@"{AppData}\.plexdl\.entropy";
        private static int EntropyByteLength { get; } = 20;

        public static byte[] GetEntropyBytes(bool forceNew = false)
        {
            byte[] value;

            if (!forceNew && File.Exists(EntropyFileLocation))
            {
                var read = File.ReadAllBytes(EntropyFileLocation);
                value = read.Length == EntropyByteLength ? read : NewEntropy();
            }
            else
                value = NewEntropy();

            return value;
        }

        private static byte[] NewEntropy(bool writeToFile = true)
        {
            byte[] value = null;

            try
            {
                //delete any existing stored entropy data
                if (File.Exists(EntropyFileLocation))
                    File.Delete(EntropyFileLocation);

                //pseudo-random entropy (initialisation vector)
                var entropy = new byte[EntropyByteLength];

                //setup the pseudo-random generator
                using (var crypto = new RNGCryptoServiceProvider())
                {
                    //fill the entropy array with pseudo-random bytes
                    crypto.GetBytes(entropy);
                }

                //write new entropy data to the file
                if (writeToFile)
                    File.WriteAllBytes(EntropyFileLocation, entropy);

                value = entropy;
            }
            catch (Exception)
            {
                //ignore the error
            }

            return value;
        }

        private static byte[] StoredEntropy()
        {
            byte[] value = null;

            try
            {
                if (File.Exists(EntropyFileLocation))
                    value = File.ReadAllBytes(EntropyFileLocation);
            }
            catch (Exception)
            {
                //ignore the error
            }

            return value;
        }
    }
}