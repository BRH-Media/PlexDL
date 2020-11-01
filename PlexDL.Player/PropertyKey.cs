using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential, Pack = 4), UnmanagedName("PROPERTYKEY")]
    internal sealed class PropertyKey
    {
        public Guid fmtid;
        public int pID;

        public PropertyKey()
        {
        }

        public PropertyKey(Guid f, int p)
        {
            fmtid = f;
            pID = p;
        }
    }
}