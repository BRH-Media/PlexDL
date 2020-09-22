using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// Internal class that is used to hide System.Object members in derived classes.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class HideObjectMembers
    {
        #region Hide Inherited System.Object members

        /// <summary>
        /// Gets the type of the current instance.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType() { return base.GetType(); } // this can't be hidden ???

        /// <summary>
        /// Serves as a hash function for a particular object.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return base.GetHashCode(); }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() { return base.ToString(); }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return base.Equals(obj); }

        #endregion
    }
}