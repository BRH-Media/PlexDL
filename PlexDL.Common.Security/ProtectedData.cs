using System;
using System.Security.Cryptography;

namespace PlexDL.Common.Security
{
    public class ProtectedData
    {
        public string EncryptData(string plainText)
        {
            var plainBytes = DataHelpers.StringToBytes(plainText);
            return DataHelpers.BytesToBase64(EncryptData(plainBytes));
        }

        public byte[] EncryptData(byte[] plainText)
        {
            byte[] value = null;

            try
            {
                var entropy = Entropy.GetEntropyBytes();
                var cipherText = System.Security.Cryptography.ProtectedData.Protect(plainText, entropy,
                    DataProtectionScope.CurrentUser);
                value = cipherText;
            }
            catch (Exception)
            {
                //ignore the error
            }

            return value;
        }

        public string DecryptData(string cipherText)
        {
            var cipherBytes = DataHelpers.BytesFromBase64(cipherText);
            return DataHelpers.BytesToString(DecryptData(cipherBytes));
        }

        public byte[] DecryptData(byte[] cipherText)
        {
            byte[] value = null;

            try
            {
                var entropy = Entropy.GetEntropyBytes();
                var plainBytes = System.Security.Cryptography.ProtectedData.Unprotect(cipherText, entropy,
                    DataProtectionScope.CurrentUser);
                value = plainBytes;
                //UIMessages.Info(plainText);
            }
            catch (Exception)
            {
                //ignore the error
            }

            return value;
        }
    }
}