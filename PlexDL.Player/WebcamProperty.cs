using System;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store webcam property data.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class WebcamProperty : HideObjectMembers
    {
        #region Fields (Webcam Property Class)

        internal string _name;
        internal bool   _supported;
        internal int    _min;
        internal int    _max;
        internal int    _step;
        internal int    _default;
        internal int    _value;
        internal bool   _autoSupport;
        internal bool   _auto;

        internal bool                   _isProcAmp;
        internal CameraControlProperty  _controlProp;
        internal VideoProcAmpProperty   _procAmpProp;

        #endregion

        internal WebcamProperty() { }

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string Name
        { get { return _name; } }

        /// <summary>
        /// A value indicating whether the property is supported by the webcam.
        /// </summary>
        public bool Supported
        {
            get { return _supported; }
        }

        /// <summary>
        /// The minimum value of the webcam property.
        /// </summary>
        public int Minimum { get { return _min; } }

        /// <summary>
        /// The maximum value of the webcam property.
        /// </summary>
        public int Maximum { get { return _max; } }

        /// <summary>
        /// The default value of the webcam property.
        /// </summary>
        public int Default { get { return _default; } }

        /// <summary>
        /// The step size for the webcam property.
        /// <br/>The step size is the smallest increment by which the property can change.
        /// </summary>
        public int StepSize { get { return _step; } }

        /// <summary>
        /// Gets or sets the value of the webcam property.
        /// <br/>When set, WebcamProperty.AutoEnabled is set to false (manual control).
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                _auto = false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the property can be controlled automatically by the webcam.
        /// <br/>See also: WebcamProperty.AutoEnabled.
        /// </summary>
        public bool AutoSupport
        {
            get { return _autoSupport; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the property is controlled automatically by the webcam.
        /// <br/>See also: WebcamProperty.AutoSupported.
        /// </summary>
        public bool AutoEnabled
        {
            get { return _auto; }
            set { _auto = value; }
        }

    }
}