using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential), UnmanagedName("MFT_REGISTER_TYPE_INFO")]
    internal class MFTRegisterTypeInfo
    {
        public Guid guidMajorType;
        public Guid guidSubtype;
    }
}