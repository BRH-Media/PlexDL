using System;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store webcam property information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class WebcamProperty : HideObjectMembers
    {
        #region Fields (WebcamProperty Class)

        //internal string _name;
        internal bool _supported;

        internal int _min;
        internal int _max;
        internal int _step;
        internal int _default;
        internal int _current;
        internal bool _autoSupport;
        internal bool _auto;

        #endregion Fields (WebcamProperty Class)

        internal WebcamProperty()
        {
        }

        ///// <summary>
        ///// The name of the property.
        ///// </summary>
        //public string Name
        //{ get { return _name; } }

        /// <summary>
        /// A value indicating whether the property is supported by the webcam.
        /// </summary>
        public bool Supported => _supported;

        /// <summary>
        /// The minimum value of the webcam property.
        /// </summary>
        public int Minimum => _min;

        /// <summary>
        /// The maximum value of the webcam property.
        /// </summary>
        public int Maximum => _max;

        /// <summary>
        /// The default value of the webcam property.
        /// </summary>
        public int Default => _default;

        /// <summary>
        /// The step size for the webcam property. The step size is the smallest increment by which the webcam property can change.
        /// </summary>
        public int StepSize => _step;

        /// <summary>
        /// Gets or sets the value of the webcam property. When set, the Automatic setting is set to false (manual control).
        /// </summary>
        public int Value
        {
            get => _current;
            set
            {
                _current = value;
                _auto = false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the property can be controlled automatically by the webcam. See also: WebcamProperty.AutoEnabled.
        /// </summary>
        public bool AutoSupport => _autoSupport;

        /// <summary>
        /// Gets or sets a value indicating whether the property is controlled automatically by the webcam. See also: WebcamProperty.AutoSupported.
        /// </summary>
        public bool AutoEnabled
        {
            get => _auto;
            set => _auto = value;
        }
    }
}