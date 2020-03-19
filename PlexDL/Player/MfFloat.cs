using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class MfFloat
    {
        private float Value;

        public MfFloat()
        : this(0)
        {
        }

        public MfFloat(float v)
        {
            Value = v;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator float(MfFloat l)
        {
            return l.Value;
        }

        public static implicit operator MfFloat(float l)
        {
            return new MfFloat(l);
        }
    }
}