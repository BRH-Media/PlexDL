using System.Drawing;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    /// <summary>
    /// MFRect is a managed representation of the Win32 RECT structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class MFRect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        /// <summary>
        /// Empty contructor. Initialize all fields to 0
        /// </summary>
        public MFRect()
        {
        }

        /// <summary>
        /// A parametred constructor. Initialize fields with given values.
        /// </summary>
        /// <param name="l">the left value</param>
        /// <param name="t">the top value</param>
        /// <param name="r">the right value</param>
        /// <param name="b">the bottom value</param>
        public MFRect(int l, int t, int r, int b)
        {
            left = l;
            top = t;
            right = r;
            bottom = b;
        }

        /// <summary>
        /// A parametred constructor. Initialize fields with a given <see cref="System.Drawing.Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">A <see cref="System.Drawing.Rectangle"/></param>
        /// <remarks>
        /// Warning, MFRect define a rectangle by defining two of his corners and <see cref="System.Drawing.Rectangle"/> define a rectangle with his upper/left corner, his width and his height.
        /// </remarks>
        public MFRect(Rectangle rectangle)
        : this(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
        {
        }

        /// <summary>
        /// Provide de string representation of this MFRect instance
        /// </summary>
        /// <returns>A string formated like this : [left, top - right, bottom]</returns>
        public override string ToString()
        {
            return string.Format("[{0}, {1}] - [{2}, {3}]", left, top, right, bottom);
        }

        public override int GetHashCode()
        {
            return left.GetHashCode() |
                   top.GetHashCode() |
                   right.GetHashCode() |
                   bottom.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is MFRect)
            {
                var cmp = (MFRect)obj;

                return right == cmp.right && bottom == cmp.bottom && left == cmp.left && top == cmp.top;
            }

            if (obj is Rectangle)
            {
                var cmp = (Rectangle)obj;

                return right == cmp.Right && bottom == cmp.Bottom && left == cmp.Left && top == cmp.Top;
            }

            return false;
        }

        /// <summary>
        /// Checks to see if the rectangle is empty
        /// </summary>
        /// <returns>Returns true if the rectangle is empty</returns>
        public bool IsEmpty()
        {
            return right <= left || bottom <= top;
        }

        /// <summary>
        /// Define implicit cast between MFRect and System.Drawing.Rectangle for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="MFRect.ToRectangle"/> for similar functionality.
        /// <code>
        ///   // Define a new Rectangle instance
        ///   Rectangle r = new Rectangle(0, 0, 100, 100);
        ///   // Do implicit cast between Rectangle and MFRect
        ///   MFRect mfR = r;
        ///
        ///   Console.WriteLine(mfR.ToString());
        /// </code>
        /// </summary>
        /// <param name="r">a MFRect to be cast</param>
        /// <returns>A casted System.Drawing.Rectangle</returns>
        public static implicit operator Rectangle(MFRect r)
        {
            return r.ToRectangle();
        }

        /// <summary>
        /// Define implicit cast between System.Drawing.Rectangle and MFRect for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="MFRect.FromRectangle"/> for similar functionality.
        /// <code>
        ///   // Define a new MFRect instance
        ///   MFRect mfR = new MFRect(0, 0, 100, 100);
        ///   // Do implicit cast between MFRect and Rectangle
        ///   Rectangle r = mfR;
        ///
        ///   Console.WriteLine(r.ToString());
        /// </code>
        /// </summary>
        /// <param name="r">A System.Drawing.Rectangle to be cast</param>
        /// <returns>A casted MFRect</returns>
        public static implicit operator MFRect(Rectangle r)
        {
            return new MFRect(r);
        }

        /// <summary>
        /// Get the System.Drawing.Rectangle equivalent to this MFRect instance.
        /// </summary>
        /// <returns>A System.Drawing.Rectangle</returns>
        public Rectangle ToRectangle()
        {
            return new Rectangle(left, top, right - left, bottom - top);
        }

        /// <summary>
        /// Get a new MFRect instance for a given <see cref="System.Drawing.Rectangle"/>
        /// </summary>
        /// <param name="r">The <see cref="System.Drawing.Rectangle"/> used to initialize this new MFGuid</param>
        /// <returns>A new instance of MFGuid</returns>
        public static MFRect FromRectangle(Rectangle r)
        {
            return new MFRect(r);
        }

        /// <summary>
        /// Copy the members from an MFRect into this object
        /// </summary>
        /// <param name="from">The rectangle from which to copy the values.</param>
        public void CopyFrom(MFRect from)
        {
            left = from.left;
            top = from.top;
            right = from.right;
            bottom = from.bottom;
        }
    }
}