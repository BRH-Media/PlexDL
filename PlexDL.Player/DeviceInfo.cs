using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used as a base class for the device information classes.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class DeviceInfo : HideObjectMembers
    {
        #region Fields (Device Info Class)

        internal string _id;
        internal string _name;
        internal string _adapter;

        #endregion

        /// <summary>
        /// Gets the identifier of the device.
        /// </summary>
        public string Id { get { return _id; } }

        /// <summary>
        /// Gets the description of the device.
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Gets the name of the adapter to which the device is attached.
        /// </summary>
        public string Adapter { get { return _adapter; } }

        /// <summary>
        /// Returns a string that represents this device information.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} ({1})", _name, _adapter);
        }
    }
}