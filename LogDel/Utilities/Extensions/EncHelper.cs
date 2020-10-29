using PlexDL.Common.Enums;
using PlexDL.Common.Security.Protection;
using System;

namespace LogDel.Utilities.Extensions
{
    public static class EncHelper
    {
        public static string DecryptString(this string cipherText)
        {
            try
            {
                var provider = new ProtectedString(cipherText, ProtectionMode.Decrypt);
                return provider.ProcessedValue;
            }
            catch (Exception)
            {
                //ignore the error
                return @"";
            }
        }

        public static string EncryptString(this string plainText)
        {
            try
            {
                var provider = new ProtectedString(plainText, ProtectionMode.Encrypt);
                return provider.ProcessedValue;
            }
            catch (Exception)
            {
                //ignore the error
                return @"";
            }
        }
    }
}