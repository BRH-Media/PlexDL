using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class FourCC
    {
        private int m_fourCC;

        public FourCC(string fcc)
        {
            if (fcc.Length != 4)
                throw new ArgumentException(fcc + " is not a valid FourCC");

            var asc = Encoding.ASCII.GetBytes(fcc);

            LoadFromBytes(asc[0], asc[1], asc[2], asc[3]);
        }

        public FourCC(char a, char b, char c, char d)
        {
            LoadFromBytes((byte)a, (byte)b, (byte)c, (byte)d);
        }

        public FourCC(int fcc)
        {
            m_fourCC = fcc;
        }

        public FourCC(byte a, byte b, byte c, byte d)
        {
            LoadFromBytes(a, b, c, d);
        }

        public FourCC(Guid g)
        {
            if (!IsA4ccSubtype(g))
                throw new Exception("Not a FourCC Guid");

            byte[] asc;
            asc = g.ToByteArray();

            LoadFromBytes(asc[0], asc[1], asc[2], asc[3]);
        }

        public void LoadFromBytes(byte a, byte b, byte c, byte d)
        {
            m_fourCC = a | (b << 8) | (c << 16) | (d << 24);
        }

        public int ToInt32()
        {
            return m_fourCC;
        }

        public static explicit operator int(FourCC f)
        {
            return f.ToInt32();
        }

        public static explicit operator Guid(FourCC f)
        {
            return f.ToMediaSubtype();
        }

        public Guid ToMediaSubtype()
        {
            return new Guid(m_fourCC, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        }

        public static bool operator ==(FourCC fcc1, FourCC fcc2)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(fcc1, fcc2))
                return true;

            // If one is null, but not both, return false.
            if ((object)fcc1 == null || (object)fcc2 == null)
                return false;

            return fcc1.m_fourCC == fcc2.m_fourCC;
        }

        public static bool operator !=(FourCC fcc1, FourCC fcc2)
        {
            return !(fcc1 == fcc2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FourCC))
                return false;

            return (obj as FourCC).m_fourCC == m_fourCC;
        }

        public override int GetHashCode()
        {
            return m_fourCC.GetHashCode();
        }

        public override string ToString()
        {
            char c;
            var ca = new char[4];

            c = Convert.ToChar(m_fourCC & 255);
            if (!char.IsLetterOrDigit(c))
                c = ' ';

            ca[0] = c;

            c = Convert.ToChar((m_fourCC >> 8) & 255);
            if (!char.IsLetterOrDigit(c))
                c = ' ';

            ca[1] = c;

            c = Convert.ToChar((m_fourCC >> 16) & 255);
            if (!char.IsLetterOrDigit(c))
                c = ' ';

            ca[2] = c;

            c = Convert.ToChar((m_fourCC >> 24) & 255);
            if (!char.IsLetterOrDigit(c))
                c = ' ';

            ca[3] = c;

            var s = new string(ca);

            return s;
        }

        public static bool IsA4ccSubtype(Guid g)
        {
            return g.ToString().EndsWith("-0000-0010-8000-00aa00389b71");
        }
    }
}