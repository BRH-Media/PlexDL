﻿using PlexDL.Common.Enums;
using System;

namespace PlexDL.Common.Security
{
    public class ProtectedBytes : ProtectedData
    {
        public byte[] RawValue { get; set; }
        public ProtectionMode Mode { get; set; }

        public byte[] ProcessedValue => ProcessRawValue();

        public ProtectedBytes(byte[] data, ProtectionMode mode)
        {
            Mode = mode;
            RawValue = data;
        }

        private byte[] ProcessRawValue()
        {
            byte[] value = null;

            try
            {
                switch (Mode)
                {
                    case ProtectionMode.Decrypt:
                        value = DecryptData(RawValue);
                        break;

                    case ProtectionMode.Encrypt:
                        value = EncryptData(RawValue);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception)
            {
                //ignore the error
            }

            return value;
        }
    }
}