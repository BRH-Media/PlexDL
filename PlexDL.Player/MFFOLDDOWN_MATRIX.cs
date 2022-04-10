using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Sequential), UnmanagedName("MFFOLDDOWN_MATRIX")]
    internal struct MFFOLDDOWN_MATRIX
    {
        public int cbSize;
        public int cSrcChannels;
        public int cDstChannels;
        public int dwChannelMask;
        public int[] Coeff;

        public byte[] ToArray()
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);

            writer.Write(cbSize);
            writer.Write(cSrcChannels);
            writer.Write(cDstChannels);
            writer.Write(dwChannelMask);
            if (Coeff != null)
            {
                for (int i = 0; i < Coeff.Length; i++) writer.Write(Coeff[i]);
            }

            byte[] byteArray = stream.ToArray();

            writer.Dispose();
            stream.Dispose();

            return byteArray;
        }
    }
}