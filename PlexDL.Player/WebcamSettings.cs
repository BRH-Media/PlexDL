using System;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to save and restore webcam settings.
    /// </summary>
    [CLSCompliant(true)]
    [Serializable]
    public sealed class WebcamSettings
    {
        internal string _webcamName;
        internal WebcamFormat _format;

        internal int _brightness;
        internal bool _autoBrightness;

        internal int _contrast;
        internal bool _autoContrast;

        internal int _hue;
        internal bool _autoHue;

        internal int _saturation;
        internal bool _autoSaturation;

        internal int _sharpness;
        internal bool _autoSharpness;

        internal int _gamma;
        internal bool _autoGamma;

        internal int _whiteBalance;
        internal bool _autoWhiteBalance;

        internal int _gain;
        internal bool _autoGain;

        internal int _zoom;
        internal bool _autoZoom;

        internal int _focus;
        internal bool _autoFocus;

        internal int _exposure;
        internal bool _autoExposure;

        internal int _iris;
        internal bool _autoIris;

        internal int _pan;
        internal bool _autoPan;

        internal int _tilt;
        internal bool _autoTilt;

        internal int _roll;
        internal bool _autoRoll;

        internal int _flash;
        internal bool _autoFlash;

        internal int _backlight;
        internal bool _autoBacklight;

        internal int _colorEnable;
        internal bool _autoColorEnable;

        internal int _powerLine;
        internal bool _autoPowerLine;
    }
}