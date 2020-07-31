using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    /// <summary>
    /// ConstPropVariant is used for [In] parameters.  This is important since
    /// for [In] parameters, you must *not* clear the PropVariant.  The caller
    /// will need to do that himself.
    ///
    /// Likewise, if you want to store a copy of a ConstPropVariant, you should
    /// store it to a PropVariant using the PropVariant constructor that takes a
    /// ConstPropVariant.  If you try to store the ConstPropVariant, when the
    /// caller frees his copy, yours will no longer be valid.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal class ConstPropVariant : IDisposable
    {
        #region Declarations

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false), SuppressUnmanagedCodeSecurity]
        protected static extern void PropVariantCopy(
            [Out, MarshalAs(UnmanagedType.LPStruct)] PropVariant pvarDest,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant pvarSource
        );

        #endregion Declarations

        public enum VariantType : short
        {
            None = 0,
            Short = 2,
            Int32 = 3,
            Float = 4,
            Double = 5,
            IUnknown = 13,
            UByte = 17,
            UShort = 18,
            UInt32 = 19,
            Int64 = 20,
            UInt64 = 21,
            String = 31,
            Guid = 72,
            Blob = 0x1000 + 17,
            StringArray = 0x1000 + 31
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
        [StructLayout(LayoutKind.Sequential), UnmanagedName("BLOB")]
        protected struct Blob
        {
            public int cbSize;
            public IntPtr pBlobData;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
        [StructLayout(LayoutKind.Sequential), UnmanagedName("CALPWSTR")]
        protected struct CALPWstr
        {
            public int cElems;
            public IntPtr pElems;
        }

        #region Member variables

        [FieldOffset(0)]
        internal VariantType type;

        [FieldOffset(2)]
        protected short reserved1;

        [FieldOffset(4)]
        protected short reserved2;

        [FieldOffset(6)]
        protected short reserved3;

        [FieldOffset(8)]
        protected short iVal;

        [FieldOffset(8)]
        protected ushort uiVal;

        [FieldOffset(8)]
        protected byte bVal;

        [FieldOffset(8)]
        protected int intValue;

        [FieldOffset(8)]
        protected uint uintVal;

        [FieldOffset(8)]
        protected float fltVal;

        [FieldOffset(8)]
        internal long longValue;

        [FieldOffset(8)]
        protected ulong ulongValue;

        [FieldOffset(8)]
        protected double doubleValue;

        [FieldOffset(8)]
        protected Blob blobValue;

        [FieldOffset(8)]
        internal IntPtr ptr;

        [FieldOffset(8)]
        protected CALPWstr calpwstrVal;

        #endregion Member variables

        public ConstPropVariant()
            : this(VariantType.None)
        {
        }

        protected ConstPropVariant(VariantType v)
        {
            type = v;
        }

        public static explicit operator string(ConstPropVariant f)
        {
            return f.GetString();
        }

        public static explicit operator string[](ConstPropVariant f)
        {
            return f.GetStringArray();
        }

        public static explicit operator byte(ConstPropVariant f)
        {
            return f.GetUByte();
        }

        public static explicit operator short(ConstPropVariant f)
        {
            return f.GetShort();
        }

        //[CLSCompliant(false)]
        public static explicit operator ushort(ConstPropVariant f)
        {
            return f.GetUShort();
        }

        public static explicit operator int(ConstPropVariant f)
        {
            return f.GetInt();
        }

        //[CLSCompliant(false)]
        public static explicit operator uint(ConstPropVariant f)
        {
            return f.GetUInt();
        }

        public static explicit operator float(ConstPropVariant f)
        {
            return f.GetFloat();
        }

        public static explicit operator double(ConstPropVariant f)
        {
            return f.GetDouble();
        }

        public static explicit operator long(ConstPropVariant f)
        {
            return f.GetLong();
        }

        //[CLSCompliant(false)]
        public static explicit operator ulong(ConstPropVariant f)
        {
            return f.GetULong();
        }

        public static explicit operator Guid(ConstPropVariant f)
        {
            return f.GetGuid();
        }

        public static explicit operator byte[](ConstPropVariant f)
        {
            return f.GetBlob();
        }

        // I decided not to do implicits since perf is likely to be
        // better recycling the PropVariant, and the only way I can
        // see to support Implicit is to create a new PropVariant.
        // Also, since I can't free the previous instance, IUnknowns
        // will linger until the GC cleans up.  Not what I think I
        // want.

        public MFAttributeType GetMFAttributeType()
        {
            switch (type)
            {
                case VariantType.None:
                case VariantType.UInt32:
                case VariantType.UInt64:
                case VariantType.Double:
                case VariantType.Guid:
                case VariantType.String:
                case VariantType.Blob:
                case VariantType.IUnknown:
                    {
                        return (MFAttributeType)type;
                    }
                default:
                    {
                        //throw new Exception("Type is not a MFAttributeType");
                        return MFAttributeType.None;
                    }
            }
        }

        public VariantType GetVariantType()
        {
            return type;
        }

        public string[] GetStringArray()
        {
            if (type == VariantType.StringArray)
            {
                string[] sa;

                int iCount = calpwstrVal.cElems;
                sa = new string[iCount];

                for (int x = 0; x < iCount; x++)
                {
                    sa[x] = Marshal.PtrToStringUni(Marshal.ReadIntPtr(calpwstrVal.pElems, x * IntPtr.Size));
                }

                return sa;
            }
            //throw new ArgumentException("PropVariant contents not a string array");
            else return null;
        }

        public string GetString()
        {
            if (type == VariantType.String)
            {
                return Marshal.PtrToStringUni(ptr);
            }
            //throw new ArgumentException("PropVariant contents not a string");
            else return null;
        }

        public byte GetUByte()
        {
            if (type == VariantType.UByte)
            {
                return bVal;
            }
            //throw new ArgumentException("PropVariant contents not a byte");
            else return 0;
        }

        public short GetShort()
        {
            if (type == VariantType.Short)
            {
                return iVal;
            }
            //throw new ArgumentException("PropVariant contents not a Short");
            else return 0;
        }

        public ushort GetUShort()
        {
            if (type == VariantType.UShort)
            {
                return uiVal;
            }
            //throw new ArgumentException("PropVariant contents not a UShort");
            else return 0;
        }

        public int GetInt()
        {
            if (type == VariantType.Int32)
            {
                return intValue;
            }
            //throw new ArgumentException("PropVariant contents not an int32");
            else return 0;
        }

        public uint GetUInt()
        {
            if (type == VariantType.UInt32)
            {
                return uintVal;
            }
            //throw new ArgumentException("PropVariant contents not a uint32");
            else return 0;
        }

        public long GetLong()
        {
            if (type == VariantType.Int64)
            {
                return longValue;
            }
            //throw new ArgumentException("PropVariant contents not an int64");
            else return 0;
        }

        public ulong GetULong()
        {
            if (type == VariantType.UInt64)
            {
                return ulongValue;
            }
            //throw new ArgumentException("PropVariant contents not a uint64");
            else return 0;
        }

        public float GetFloat()
        {
            if (type == VariantType.Float)
            {
                return fltVal;
            }
            //throw new ArgumentException("PropVariant contents not a Float");
            else return 0;
        }

        public double GetDouble()
        {
            if (type == VariantType.Double)
            {
                return doubleValue;
            }
            //throw new ArgumentException("PropVariant contents not a double");
            else return 0;
        }

        public Guid GetGuid()
        {
            if (type == VariantType.Guid)
            {
                return (Guid)Marshal.PtrToStructure(ptr, typeof(Guid));
            }
            //throw new ArgumentException("PropVariant contents not a Guid");
            else return Guid.Empty;
        }

        public byte[] GetBlob()
        {
            if (type == VariantType.Blob)
            {
                byte[] b = new byte[blobValue.cbSize];

                if (blobValue.cbSize > 0)
                {
                    Marshal.Copy(blobValue.pBlobData, b, 0, blobValue.cbSize);
                }

                return b;
            }
            //throw new ArgumentException("PropVariant contents not a Blob");
            else return null;
        }

        public object GetBlob(Type t, int offset)
        {
            if (type == VariantType.Blob)
            {
                object o;

                if (blobValue.cbSize > offset)
                {
                    if (blobValue.cbSize >= Marshal.SizeOf(t) + offset)
                    {
                        //o = Marshal.PtrToStructure(blobValue.pBlobData + offset, t);
                        o = Marshal.PtrToStructure(new IntPtr(blobValue.pBlobData.ToInt64() + offset), t);
                    }
                    else
                    {
                        throw new ArgumentException("Blob wrong size");
                    }
                }
                else
                {
                    o = null;
                }

                return o;
            }
            //throw new ArgumentException("PropVariant contents not a Blob");
            else return null;
        }

        public object GetBlob(Type t)
        {
            return GetBlob(t, 0);
        }

        public object GetIUnknown()
        {
            if (type == VariantType.IUnknown)
            {
                if (ptr != IntPtr.Zero)
                {
                    return Marshal.GetObjectForIUnknown(ptr);
                }
                else
                {
                    return null;
                }
            }
            //throw new ArgumentException("PropVariant contents not an IUnknown");
            else return null;
        }

        public void Copy(PropVariant pdest)
        {
            if (pdest == null)
            {
                throw new Exception("Null PropVariant sent to Copy");
            }

            // Copy doesn't clear the dest.
            pdest.Clear();

            PropVariantCopy(pdest, this);
        }

        //public override string ToString()
        //{
        //    // This method is primarily intended for debugging so that a readable string will show
        //    // up in the output window
        //    string sRet;

        //    switch (type)
        //    {
        //        case VariantType.None:
        //            {
        //                sRet = "<Empty>";
        //                break;
        //            }

        //        case VariantType.Blob:
        //            {
        //                const string FormatString = "x2"; // Hex 2 digit format
        //                const int MaxEntries = 16;

        //                byte[] blob = GetBlob();

        //                // Number of bytes we're going to format
        //                int n = Math.Min(MaxEntries, blob.Length);

        //                if (n == 0)
        //                {
        //                    sRet = "<Empty Array>";
        //                }
        //                else
        //                {
        //                    // Only format the first MaxEntries bytes
        //                    sRet = blob[0].ToString(FormatString);
        //                    for (int i = 1; i < n; i++)
        //                    {
        //                        sRet += ',' + blob[i].ToString(FormatString);
        //                    }

        //                    // If the string is longer, add an indicator
        //                    if (blob.Length > n)
        //                    {
        //                        sRet += "...";
        //                    }
        //                }
        //                break;
        //            }

        //        case VariantType.Float:
        //            {
        //                sRet = GetFloat().ToString();
        //                break;
        //            }

        //        case VariantType.Double:
        //            {
        //                sRet = GetDouble().ToString();
        //                break;
        //            }

        //        case VariantType.Guid:
        //            {
        //                sRet = GetGuid().ToString();
        //                break;
        //            }

        //        case VariantType.IUnknown:
        //            {
        //                object o = GetIUnknown();
        //                if (o != null)
        //                {
        //                    sRet = GetIUnknown().ToString();
        //                }
        //                else
        //                {
        //                    sRet = "<null>";
        //                }
        //                break;
        //            }

        //        case VariantType.String:
        //            {
        //                sRet = GetString();
        //                break;
        //            }

        //        case VariantType.Short:
        //            {
        //                sRet = GetShort().ToString();
        //                break;
        //            }

        //        case VariantType.UByte:
        //            {
        //                sRet = GetUByte().ToString();
        //                break;
        //            }

        //        case VariantType.UShort:
        //            {
        //                sRet = GetUShort().ToString();
        //                break;
        //            }

        //        case VariantType.Int32:
        //            {
        //                sRet = GetInt().ToString();
        //                break;
        //            }

        //        case VariantType.UInt32:
        //            {
        //                sRet = GetUInt().ToString();
        //                break;
        //            }

        //        case VariantType.Int64:
        //            {
        //                sRet = GetLong().ToString();
        //                break;
        //            }

        //        case VariantType.UInt64:
        //            {
        //                sRet = GetULong().ToString();
        //                break;
        //            }

        //        case VariantType.StringArray:
        //            {
        //                sRet = "";
        //                foreach (string entry in GetStringArray())
        //                {
        //                    sRet += (sRet.Length == 0 ? "\"" : ",\"") + entry + '\"';
        //                }
        //                break;
        //            }
        //        default:
        //            {
        //                sRet = base.ToString();
        //                break;
        //            }
        //    }

        //    return sRet;
        //}

        //public override int GetHashCode()
        //{
        //    // Give a (slightly) better hash value in case someone uses PropVariants
        //    // in a hash table.
        //    int iRet;

        //    switch (type)
        //    {
        //        case VariantType.None:
        //            {
        //                iRet = base.GetHashCode();
        //                break;
        //            }

        //        case VariantType.Blob:
        //            {
        //                iRet = GetBlob().GetHashCode();
        //                break;
        //            }

        //        case VariantType.Float:
        //            {
        //                iRet = GetFloat().GetHashCode();
        //                break;
        //            }

        //        case VariantType.Double:
        //            {
        //                iRet = GetDouble().GetHashCode();
        //                break;
        //            }

        //        case VariantType.Guid:
        //            {
        //                iRet = GetGuid().GetHashCode();
        //                break;
        //            }

        //        case VariantType.IUnknown:
        //            {
        //                iRet = GetIUnknown().GetHashCode();
        //                break;
        //            }

        //        case VariantType.String:
        //            {
        //                iRet = GetString().GetHashCode();
        //                break;
        //            }

        //        case VariantType.UByte:
        //            {
        //                iRet = GetUByte().GetHashCode();
        //                break;
        //            }

        //        case VariantType.Short:
        //            {
        //                iRet = GetShort().GetHashCode();
        //                break;
        //            }

        //        case VariantType.UShort:
        //            {
        //                iRet = GetUShort().GetHashCode();
        //                break;
        //            }

        //        case VariantType.Int32:
        //            {
        //                iRet = GetInt().GetHashCode();
        //                break;
        //            }

        //        case VariantType.UInt32:
        //            {
        //                iRet = GetUInt().GetHashCode();
        //                break;
        //            }

        //        case VariantType.Int64:
        //            {
        //                iRet = GetLong().GetHashCode();
        //                break;
        //            }

        //        case VariantType.UInt64:
        //            {
        //                iRet = GetULong().GetHashCode();
        //                break;
        //            }

        //        case VariantType.StringArray:
        //            {
        //                iRet = GetStringArray().GetHashCode();
        //                break;
        //            }
        //        default:
        //            {
        //                iRet = base.GetHashCode();
        //                break;
        //            }
        //    }

        //    return iRet;
        //}

        //public override bool Equals(object obj)
        //{
        //    bool bRet;
        //    PropVariant p = obj as PropVariant;

        //    if ((((object)p) == null) || (p.type != type))
        //    {
        //        bRet = false;
        //    }
        //    else
        //    {
        //        switch (type)
        //        {
        //            case VariantType.None:
        //                {
        //                    bRet = true;
        //                    break;
        //                }

        //            case VariantType.Blob:
        //                {
        //                    byte[] b1;
        //                    byte[] b2;

        //                    b1 = GetBlob();
        //                    b2 = p.GetBlob();

        //                    if (b1.Length == b2.Length)
        //                    {
        //                        bRet = true;
        //                        for (int x = 0; x < b1.Length; x++)
        //                        {
        //                            if (b1[x] != b2[x])
        //                            {
        //                                bRet = false;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        bRet = false;
        //                    }
        //                    break;
        //                }

        //            case VariantType.Float:
        //                {
        //                    bRet = GetFloat() == p.GetFloat();
        //                    break;
        //                }

        //            case VariantType.Double:
        //                {
        //                    bRet = GetDouble() == p.GetDouble();
        //                    break;
        //                }

        //            case VariantType.Guid:
        //                {
        //                    bRet = GetGuid() == p.GetGuid();
        //                    break;
        //                }

        //            case VariantType.IUnknown:
        //                {
        //                    bRet = GetIUnknown() == p.GetIUnknown();
        //                    break;
        //                }

        //            case VariantType.String:
        //                {
        //                    bRet = GetString() == p.GetString();
        //                    break;
        //                }

        //            case VariantType.UByte:
        //                {
        //                    bRet = GetUByte() == p.GetUByte();
        //                    break;
        //                }

        //            case VariantType.Short:
        //                {
        //                    bRet = GetShort() == p.GetShort();
        //                    break;
        //                }

        //            case VariantType.UShort:
        //                {
        //                    bRet = GetUShort() == p.GetUShort();
        //                    break;
        //                }

        //            case VariantType.Int32:
        //                {
        //                    bRet = GetInt() == p.GetInt();
        //                    break;
        //                }

        //            case VariantType.UInt32:
        //                {
        //                    bRet = GetUInt() == p.GetUInt();
        //                    break;
        //                }

        //            case VariantType.Int64:
        //                {
        //                    bRet = GetLong() == p.GetLong();
        //                    break;
        //                }

        //            case VariantType.UInt64:
        //                {
        //                    bRet = GetULong() == p.GetULong();
        //                    break;
        //                }

        //            case VariantType.StringArray:
        //                {
        //                    string[] sa1;
        //                    string[] sa2;

        //                    sa1 = GetStringArray();
        //                    sa2 = p.GetStringArray();

        //                    if (sa1.Length == sa2.Length)
        //                    {
        //                        bRet = true;
        //                        for (int x = 0; x < sa1.Length; x++)
        //                        {
        //                            if (sa1[x] != sa2[x])
        //                            {
        //                                bRet = false;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        bRet = false;
        //                    }
        //                    break;
        //                }
        //            default:
        //                {
        //                    bRet = base.Equals(obj);
        //                    break;
        //                }
        //        }
        //    }

        //    return bRet;
        //}

        //public static bool operator ==(ConstPropVariant pv1, ConstPropVariant pv2)
        //{
        //    // If both are null, or both are same instance, return true.
        //    if (System.Object.ReferenceEquals(pv1, pv2))
        //    {
        //        return true;
        //    }

        //    // If one is null, but not both, return false.
        //    if (((object)pv1 == null) || ((object)pv2 == null))
        //    {
        //        return false;
        //    }

        //    return pv1.Equals(pv2);
        //}

        //public static bool operator !=(ConstPropVariant pv1, ConstPropVariant pv2)
        //{
        //    return !(pv1 == pv2);
        //}

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2216:DisposableTypesShouldDeclareFinalizer")]
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // If we are a ConstPropVariant, we must *not* call PropVariantClear.  That
            // would release the *caller's* copy of the data, which would probably make
            // him cranky.  If we are a PropVariant, the PropVariant.Dispose gets called
            // as well, which *does* do a PropVariantClear.
            type = VariantType.None;
#if DEBUG
            longValue = 0;
#endif
        }

        #endregion IDisposable Members
    }
}