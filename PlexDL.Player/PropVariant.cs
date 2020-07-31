using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [StructLayout(LayoutKind.Explicit)]
    internal class PropVariant : ConstPropVariant
    {
        #region Declarations

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false), SuppressUnmanagedCodeSecurity]
        protected static extern void PropVariantClear([In, MarshalAs(UnmanagedType.LPStruct)] PropVariant pvar);

        #endregion Declarations

        public PropVariant()
            : base(VariantType.None)
        {
        }

        public PropVariant(string value)
            : base(VariantType.String)
        {
            ptr = Marshal.StringToCoTaskMemUni(value);
        }

        public PropVariant(string[] value)
            : base(VariantType.StringArray)
        {
            calpwstrVal.cElems = value.Length;
            calpwstrVal.pElems = Marshal.AllocCoTaskMem(IntPtr.Size * value.Length);

            for (int x = 0; x < value.Length; x++)
            {
                Marshal.WriteIntPtr(calpwstrVal.pElems, x * IntPtr.Size, Marshal.StringToCoTaskMemUni(value[x]));
            }
        }

        public PropVariant(byte value)
            : base(VariantType.UByte)
        {
            bVal = value;
        }

        public PropVariant(short value)
            : base(VariantType.Short)
        {
            iVal = value;
        }

        public PropVariant(ushort value)
            : base(VariantType.UShort)
        {
            uiVal = value;
        }

        public PropVariant(int value)
            : base(VariantType.Int32)
        {
            intValue = value;
        }

        public PropVariant(uint value)
            : base(VariantType.UInt32)
        {
            uintVal = value;
        }

        public PropVariant(float value)
            : base(VariantType.Float)
        {
            fltVal = value;
        }

        public PropVariant(double value)
            : base(VariantType.Double)
        {
            doubleValue = value;
        }

        public PropVariant(long value)
            : base(VariantType.Int64)
        {
            longValue = value;
        }

        public PropVariant(ulong value)
            : base(VariantType.UInt64)
        {
            ulongValue = value;
        }

        public PropVariant(Guid value)
            : base(VariantType.Guid)
        {
            ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(value));
            Marshal.StructureToPtr(value, ptr, false);
        }

        public PropVariant(byte[] value)
            : base(VariantType.Blob)
        {
            blobValue.cbSize = value.Length;
            blobValue.pBlobData = Marshal.AllocCoTaskMem(value.Length);
            Marshal.Copy(value, 0, blobValue.pBlobData, value.Length);
        }

        public PropVariant(object value)
            : base(VariantType.IUnknown)
        {
            if (value == null)
            {
                ptr = IntPtr.Zero;
            }
            else if (Marshal.IsComObject(value))
            {
                ptr = Marshal.GetIUnknownForObject(value);
            }
            else
            {
                type = VariantType.Blob;

                blobValue.cbSize = Marshal.SizeOf(value);
                blobValue.pBlobData = Marshal.AllocCoTaskMem(blobValue.cbSize);

                Marshal.StructureToPtr(value, blobValue.pBlobData, false);
            }
        }

        public PropVariant(IntPtr value)
            : base(VariantType.None)
        {
            Marshal.PtrToStructure(value, this);
        }

        public PropVariant(ConstPropVariant value)
        {
            if (value != null)
            {
                PropVariantCopy(this, value);
            }
            else
            {
                throw new NullReferenceException("null passed to PropVariant constructor");
            }
        }

        ~PropVariant()
        {
            Dispose(false);
        }

        public void Clear()
        {
            PropVariantClear(this);
        }

        #region IDisposable Members

        protected override void Dispose(bool disposing)
        {
            Clear();
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        #endregion IDisposable Members
    }
}