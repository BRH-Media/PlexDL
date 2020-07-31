using PlexDL.Common.Enums;
using System;
using System.Security.Cryptography;

//using PlexDL.Common.Logging;

namespace PlexDL.Common.Security
{
    //This uses the Windows Data Protection API for the Current User to encrypt/decrypt strings.
    //It's near-impossible to decrypt this information if not logged in as the user that encrypted the original data.
    public class ProtectedString
    {
        public string RawValue { get; set; }
        public StringProtectionMode Mode { get; set; }

        public string ProcessedValue => ProcessRawValue();

        public ProtectedString(string data, StringProtectionMode mode)
        {
            Mode = mode;
            RawValue = data;
        }

        private string ProcessRawValue()
        {
            var value = "";

            try
            {
                switch (Mode)
                {
                    case StringProtectionMode.Decrypt:
                        value = DecryptRawValue();
                        break;

                    case StringProtectionMode.Encrypt:
                        value = EncryptRawValue();
                        break;
                }
            }
            catch (Exception)
            {
                //ignore the error
            }

            return value;
        }

        private string EncryptRawValue()
        {
            var value = "";

            try
            {
                var plainText = DataHelpers.StringToBytes(RawValue);
                var entropy = Entropy.GetEntropyBytes();
                var cipherText = ProtectedData.Protect(plainText, entropy,
                    DataProtectionScope.CurrentUser);
                var cipherBase64 = DataHelpers.BytesToBase64(cipherText);
                value = cipherBase64;
            }
            catch (Exception)
            {
                //ignore the error
            }

            return value;
        }

        private string DecryptRawValue()
        {
            var value = "";

            try
            {
                var cipherRaw = RawValue;
                var cipherText = DataHelpers.BytesFromBase64(cipherRaw);
                var entropy = Entropy.GetEntropyBytes();
                var plainBytes = ProtectedData.Unprotect(cipherText, entropy,
                    DataProtectionScope.CurrentUser);
                var plainText = DataHelpers.BytesToString(plainBytes);
                value = plainText;
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