﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Webcam methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Webcam : HideObjectMembers
    {
        #region Fields (Webcam Class)

        private const int   NO_ERROR = 0;

        private Player      _base;
        //private VideoRecorder _recorderClass;

        private bool        _webcamSourceCreated;

        #endregion


        #region Public - Main / Playing / Device / AudioInput / Format / GetDevices / GetDefaultDevice / Update

        internal Webcam(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets a value indicating whether a webcam is playing (including paused webcam).
        /// <br/>Webcams can be played using the Player.Play method.
        /// </summary>
        public bool Playing
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._webcamMode;
            }
        }

        /// <summary>
        /// Gets or sets (changes (when not recording)) the playing webcam.
        /// <br/>Webcams can be played using the Player.Play method with more options.
        /// <br/>See also: Player.Webcam.GetDevices.
        /// </summary>
        public WebcamDevice Device
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._webcamDevice;
            }
            set
            {
                //if (!_base._recording)
                {
                    _base.Play(value, null, null);
                }
                //else _base._lastError = HResult.MF_E_INVALIDREQUEST;
            }
        }

        /// <summary>
        /// Gets or sets (when not recording) the audio input device of the playing webcam.
        /// <br/>See also: Player.AudioInput.GetDevices.
        /// </summary>
        public AudioInputDevice AudioInput
        {
            get
            {
                _base._lastError = NO_ERROR;
                if (_base._webcamMode) return _base._micDevice;
                return null;
            }
            set
            {
                if (_base._webcamMode && !_base._recording)
                {
                    //_base._lastError = NO_ERROR;
                    if (!((value == null && _base._micDevice == null) || (value != null && _base._micDevice != null && value._id == _base._micDevice._id)))
                    {
                        if (_base.pm_HasInputMeter)
                        {
                            _base.InputMeter_Close();
                            _base.pm_InputMeterPending = true;
                        }

                        AudioInputDevice oldDevice = _base._micDevice;
                        _base._micDevice = value;

                        _base.AV_UpdateTopology();

                        if (_base._lastError == NO_ERROR) _base._mediaAudioInputDeviceChanged?.Invoke(_base, EventArgs.Empty);
                        else _base._micDevice = oldDevice;
                    }
                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_INVALIDREQUEST;
            }
        }

        /// <summary>
        /// Gets or sets the video output format of the playing webcam.
        /// <br/>See also: Player.Webcam.GetFormats.
        /// </summary>
        public WebcamFormat Format
        {
            get
            {
                _base._lastError = NO_ERROR;
                if (_base._webcamFormat == null && _base._webcamDevice != null)
                {
                    try {_base._webcamFormat = GetCurrentFormat(_base._webcamDevice); }
                    catch { /* ignored */ }
                }
                return _base._webcamFormat;
            }
            set
            {
                if (value == null) _base._lastError = HResult.E_INVALIDARG;
                else
                {
                    // setting the format directly does not seem to work
                    if (_base._webcamMode)
                    {
                        //_base._lastError = NO_ERROR;
                        if ((value == null && _base._webcamFormat != null) ||
                            (value != null && _base._webcamFormat == null) ||
                            (value._typeIndex != _base._webcamFormat._typeIndex))
                        {
                            _base._webcamFormat = value;
                            _base.AV_UpdateTopology();
                            if (_base._hasDisplay)
                            {
                                _base._hasVideoBounds = false;
                                _base._display.Invalidate();
                            }
                            _base._mediaWebcamFormatChanged?.Invoke(_base, EventArgs.Empty);
                        }
                        _base._lastError = NO_ERROR;
                    }
                    else _base._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
                }
            }
        }

        /// <summary>
        /// Gets the number of enabled webcams of the system.
        /// <br/>See also: Player.Webcam.GetDevices.
        /// </summary>
        public int DeviceCount
        {
            get
            {
                HResult result;

                MFExtern.MFCreateAttributes(out IMFAttributes attributes, 1);
                attributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);

                result = MFExtern.MFEnumDeviceSources(attributes, out IMFActivate[] webcams, out int webcamCount);
                Marshal.ReleaseComObject(attributes);

                if (result == NO_ERROR && webcams != null)
                {
                    for (int i = 0; i < webcamCount; i++)
                    {
                        Marshal.ReleaseComObject(webcams[i]);
                    }
                }

                _base._lastError = NO_ERROR;
                return webcamCount;
            }
        }


#pragma warning disable IDE0059 // Unnecessary assignment of a value - GetDevices() and GetDefaultDevice()

        /// <summary>
        /// Returns the enabled webcams of the system.
        /// <br/>See also: Player.Webcam.DeviceCount.
        /// </summary>
        public WebcamDevice[] GetDevices()
        {
            WebcamDevice[] devices = null;
            HResult result;

            MFExtern.MFCreateAttributes(out IMFAttributes attributes, 1);
            attributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);

            result = MFExtern.MFEnumDeviceSources(attributes, out IMFActivate[] webcams, out int webcamCount);
            if (result == NO_ERROR)
            {
                if (webcams == null) result = HResult.MF_E_NO_CAPTURE_DEVICES_AVAILABLE;
                else
                {
                    devices = new WebcamDevice[webcamCount];
                    for (int i = 0; i < webcamCount; i++)
                    {
                        devices[i] = new WebcamDevice();
                        webcams[i].GetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME, _base._textBuffer1, _base._textBuffer1.Capacity, out int length);
                        devices[i]._name = _base._textBuffer1.ToString();
                        webcams[i].GetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, _base._textBuffer1, _base._textBuffer1.Capacity, out length);
                        devices[i]._id = _base._textBuffer1.ToString();

                        Marshal.ReleaseComObject(webcams[i]);
                    }
                }
            }
            Marshal.ReleaseComObject(attributes);

            _base._lastError = result;
            return devices;
        }

        /// <summary>
        /// Returns the system's default webcam.
        /// <br/>See also: Player.Webcam.GetDevices.
        /// </summary>
        public WebcamDevice GetDefaultDevice()
        {
            WebcamDevice device = null;
            HResult result;

            MFExtern.MFCreateAttributes(out IMFAttributes attributes, 1);
            attributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);

            result = MFExtern.MFEnumDeviceSources(attributes, out IMFActivate[] webcams, out int webcamCount);
            if (result == NO_ERROR)
            {
                if (webcams == null) result = HResult.MF_E_NO_CAPTURE_DEVICES_AVAILABLE;
                else
                {
                    device = new WebcamDevice();
                    webcams[0].GetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME, _base._textBuffer1, _base._textBuffer1.Capacity, out int length);
                    device._name = _base._textBuffer1.ToString();
                    webcams[0].GetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, _base._textBuffer1, _base._textBuffer1.Capacity, out length);
                    device._id = _base._textBuffer1.ToString();

                    for (int i = 0; i < webcamCount; i++)
                    {
                        Marshal.ReleaseComObject(webcams[i]);
                    }
                }
            }
            Marshal.ReleaseComObject(attributes);

            _base._lastError = result;
            return device;
        }

#pragma warning restore IDE0059 // Unnecessary assignment of a value - GetDevices() and GetDefaultDevice()


        /// <summary>
        /// Updates or restores the audio and video of the playing webcam.
        /// <br/>For special use only, generally not required.
        /// </summary>
        public int Update()
        {
            if (_base._webcamMode)
            {
                _base.AV_UpdateTopology();
                if (_base._hasOverlay) _base.AV_ShowOverlay();
                _base._lastError = NO_ERROR;
            }
            else _base._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
            return (int)_base._lastError;
        }

        #endregion

        #region Public - SetProperty / UpdateProperty / ResetProperty / Settings / SetSettings

        private IMFMediaSource WebcamMediaSource
        {
            get
            {
                IMFMediaSource source;
                if (_base._webcamAggregated)
                {
                    MFExtern.MFCreateAttributes(out IMFAttributes attributes, 2);
                    attributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
                    attributes.SetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, _base._webcamDevice._id);

                    MFExtern.MFCreateDeviceSource(attributes, out source);
                    _webcamSourceCreated = true;
                    Marshal.ReleaseComObject(attributes);
                }
                else
                {
                    source = _base.mf_MediaSource;
                    _webcamSourceCreated = false;
                }
                return source;
            }
        }

        /// <summary>
        /// Sets the specified property of the playing webcam to the specified value or to automatic control,
        /// <br/>for example myPlayer.Webcam.SetProperty(myPlayer.Webcam.Brightness, 100, false).
        /// </summary>
        /// <param name="property">Specifies the webcam property,
        /// <br/>obtained with for example myPlayer.Webcam.Brightness.</param>
        /// <param name="value">The value to be set.</param>
        /// <param name="auto">If set to true, the value parameter is ignored
        /// <br/>and the automatic control of the property is enabled (if available).</param>
        public void SetProperty(WebcamProperty property, int value, bool auto)
        {
            if (_base._webcamMode)
            {
                if (property != null)
                {
                    WebcamProperty currentProp;
                    if (property._isProcAmp) currentProp = GetProcAmpProperties(property._procAmpProp);
                    else currentProp = GetControlProperties(property._controlProp);

                    if (currentProp.Supported)
                    {
                        if (auto || (value >= currentProp._min && value <= currentProp._max))
                        {
                            bool setProperty = true;
                            if (auto)
                            {
                                if (currentProp._auto) setProperty = false;
                            }
                            else if (value == currentProp._value && !currentProp.AutoEnabled)
                            {
                                setProperty = false;
                            }
                            if (setProperty)
                            {
                                try
                                {
                                    if (property._isProcAmp)
                                    {
#pragma warning disable IDE0019 // Use pattern matching
                                        IAMVideoProcAmp control = WebcamMediaSource as IAMVideoProcAmp;
#pragma warning restore IDE0019 // Use pattern matching
                                        if (control != null)
                                        {
                                            _base._lastError = control.Set(property._procAmpProp, value, auto ? VideoProcAmpFlags.Auto : VideoProcAmpFlags.Manual);
                                            if (_webcamSourceCreated) Marshal.ReleaseComObject(control);
                                        }
                                    }
                                    else
                                    {
#pragma warning disable IDE0019 // Use pattern matching
                                        IAMCameraControl control = WebcamMediaSource as IAMCameraControl;
#pragma warning restore IDE0019 // Use pattern matching
                                        if (control != null)
                                        {
                                            _base._lastError = control.Set(property._controlProp, value, auto ? CameraControlFlags.Auto : CameraControlFlags.Manual);
                                            if (_webcamSourceCreated) Marshal.ReleaseComObject(control);
                                        }
                                    }
                                    if (_base._lastError == NO_ERROR)
                                    {
                                        if (auto) property._auto = true;
                                        else property._value = value;
                                    }
                                }
                                catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                            }
                            else _base._lastError = NO_ERROR;
                        }
                        else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                    }
                    else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                }
                else _base._lastError = HResult.E_INVALIDARG;
            }
            else _base._lastError = HResult.MF_E_INVALIDREQUEST;
        }

        /// <summary>
        /// Updates the values in the specified (previously obtained) property data of the playing webcam.
        /// </summary>
        /// <param name="property">Specifies the webcam property data to update,
        /// <br/>previously obtained with for example myPlayer.Webcam.Brightness.</param>
        public void UpdatePropertyData(WebcamProperty property)
        {
            if (_base._webcamMode)
            {
                if (property != null)
                {
                    WebcamProperty currentProperty;

                    if (property._isProcAmp) currentProperty = GetProcAmpProperties(property._procAmpProp);
                    else currentProperty = GetControlProperties(property._controlProp);

                    property._value = currentProperty._value;
                    property._auto = currentProperty._auto;

                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.E_INVALIDARG;
            }
            else _base._lastError = HResult.MF_E_INVALIDREQUEST;
        }

        /// <summary>
        /// Sets the specified property of the playing webcam to the default value or (if available) to automatic control,
        /// <br/>for example myPlayer.Webcam.ResetProperty(myPlayer.Webcam.Brightness).
        /// </summary>
        /// <param name="property">Specifies the webcam property,
        /// <br/>obtained with for example myPlayer.Webcam.Brightness.</param>
        public void ResetProperty(WebcamProperty property)
        {
            SetProperty(property, property._default, property._autoSupport);
        }

        /// <summary>
        /// Gets or sets all the properties, including the video output format, of the playing webcam.
        /// <br/>For use with the player's webcam settings save and restore functions.
        /// <br/>See also: Player.Webcam.ApplySettings.
        /// </summary>
        public WebcamSettings Settings
        {
            get
            {
                WebcamSettings settings = null;
                if (_base._webcamMode)
                {
                    settings = new WebcamSettings
                    {
                        _webcamName = _base._webcamDevice.Name,
                        _format = _base._webcamFormat
                    };

                    WebcamProperty props = GetProcAmpProperties(VideoProcAmpProperty.BacklightCompensation);
                    settings._backlight = props._value;
                    settings._autoBacklight = props._auto;

                    props = GetProcAmpProperties(VideoProcAmpProperty.Brightness);
                    settings._brightness = props._value;
                    settings._autoBrightness = props._auto;

                    props = GetProcAmpProperties(VideoProcAmpProperty.ColorEnable);
                    settings._colorEnable = props._value;
                    settings._autoColorEnable = props._auto;

                    props = GetProcAmpProperties(VideoProcAmpProperty.Contrast);
                    settings._contrast = props._value;
                    settings._autoContrast = props._auto;

                    props = GetProcAmpProperties(VideoProcAmpProperty.Gain);
                    settings._gain = props._value;
                    settings._autoGain = props._auto;

                    props = GetProcAmpProperties(VideoProcAmpProperty.Gamma);
                    settings._gamma = props._value;
                    settings._autoGamma = props._auto;

                    props = GetProcAmpProperties(VideoProcAmpProperty.Hue);
                    settings._hue = props._value;
                    settings._autoHue = props._auto;

                    props = GetProcAmpProperties(VideoProcAmpProperty.PowerLineFrequency);
                    settings._powerLine = props._value;
                    settings._autoPowerLine = props._auto;

                    props = GetProcAmpProperties(VideoProcAmpProperty.Saturation);
                    settings._saturation = props._value;
                    settings._autoSaturation = props._auto;

                    props = GetProcAmpProperties(VideoProcAmpProperty.Sharpness);
                    settings._sharpness = props._value;
                    settings._autoSharpness = props._auto;

                    props = GetProcAmpProperties(VideoProcAmpProperty.WhiteBalance);
                    settings._whiteBalance = props._value;
                    settings._autoWhiteBalance = props._auto;


                    props = GetControlProperties(CameraControlProperty.Exposure);
                    settings._exposure = props._value;
                    settings._autoExposure = props._auto;

                    props = GetControlProperties(CameraControlProperty.Flash);
                    settings._flash = props._value;
                    settings._autoFlash = props._auto;

                    props = GetControlProperties(CameraControlProperty.Focus);
                    settings._focus = props._value;
                    settings._autoFocus = props._auto;

                    props = GetControlProperties(CameraControlProperty.Iris);
                    settings._iris = props._value;
                    settings._autoIris = props._auto;

                    props = GetControlProperties(CameraControlProperty.Pan);
                    settings._pan = props._value;
                    settings._autoPan = props._auto;

                    props = GetControlProperties(CameraControlProperty.Roll);
                    settings._roll = props._value;
                    settings._autoRoll = props._auto;

                    props = GetControlProperties(CameraControlProperty.Tilt);
                    settings._tilt = props._value;
                    settings._autoTilt = props._auto;

                    props = GetControlProperties(CameraControlProperty.Zoom);
                    settings._zoom = props._value;
                    settings._autoZoom = props._auto;

                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_INVALIDREQUEST;
                return settings;
            }
            set
            {
                ApplySettings(value, true, true, true);
            }
        }

        /// <summary>
        /// Applies previously obtained (saved) webcam settings selectively to the playing webcam played.
        /// <br/>For use with the player's webcam settings save and restore functions.
        /// <br/>See also: Player.Webcam.Settings.
        /// </summary>
        /// <param name="settings">The settings to be applied to the playing webcam.
        /// <br/>The settings must have been obtained earlier from the webcam, settings from other webcams cannot be used.</param>
        /// <param name="format">A value that indicates whether to apply the webcam video output format (size and fps).</param>
        /// /// <param name="procAmp">A value that indicates whether to apply the webcam video quality properties (such as brightness and contrast).</param>
        /// <param name="control">A value that indicates whether to apply the webcam camera control properties (such as focus and zoom).</param>
        public int ApplySettings(WebcamSettings settings, bool format, bool procAmp, bool control)
        {
            if (_base._webcamMode)
            {
                if (settings != null && string.Compare(settings._webcamName, _base._webcamDevice.Name, false) == 0)
                {
                    WebcamProperty props;

                    //if (format) { Format = settings._format; } see below

                    if (procAmp)
                    {
                        props = GetProcAmpProperties(VideoProcAmpProperty.BacklightCompensation);
                        SetProperty(props, settings._backlight, settings._autoBacklight);

                        props = GetProcAmpProperties(VideoProcAmpProperty.Brightness);
                        SetProperty(props, settings._brightness, settings._autoBrightness);

                        props = GetProcAmpProperties(VideoProcAmpProperty.ColorEnable);
                        SetProperty(props, settings._colorEnable, settings._autoColorEnable);

                        props = GetProcAmpProperties(VideoProcAmpProperty.Contrast);
                        SetProperty(props, settings._contrast, settings._autoContrast);

                        props = GetProcAmpProperties(VideoProcAmpProperty.Gain);
                        SetProperty(props, settings._gain, settings._autoGain);

                        props = GetProcAmpProperties(VideoProcAmpProperty.Gamma);
                        SetProperty(props, settings._gamma, settings._autoGamma);

                        props = GetProcAmpProperties(VideoProcAmpProperty.Hue);
                        SetProperty(props, settings._hue, settings._autoHue);

                        props = GetProcAmpProperties(VideoProcAmpProperty.PowerLineFrequency);
                        SetProperty(props, settings._powerLine, settings._autoPowerLine);

                        props = GetProcAmpProperties(VideoProcAmpProperty.Saturation);
                        SetProperty(props, settings._backlight, settings._autoBacklight);

                        props = GetProcAmpProperties(VideoProcAmpProperty.Sharpness);
                        SetProperty(props, settings._sharpness, settings._autoSharpness);

                        props = GetProcAmpProperties(VideoProcAmpProperty.WhiteBalance);
                        SetProperty(props, settings._whiteBalance, settings._autoWhiteBalance);
                    }

                    if (control)
                    {
                        props = GetControlProperties(CameraControlProperty.Exposure);
                        SetProperty(props, settings._exposure, settings._autoExposure);

                        props = GetControlProperties(CameraControlProperty.Flash);
                        SetProperty(props, settings._flash, settings._autoFlash);

                        props = GetControlProperties(CameraControlProperty.Focus);
                        SetProperty(props, settings._focus, settings._autoFocus);

                        props = GetControlProperties(CameraControlProperty.Iris);
                        SetProperty(props, settings._iris, settings._autoIris);

                        props = GetControlProperties(CameraControlProperty.Pan);
                        SetProperty(props, settings._pan, settings._autoPan);

                        props = GetControlProperties(CameraControlProperty.Roll);
                        SetProperty(props, settings._roll, settings._autoRoll);

                        props = GetControlProperties(CameraControlProperty.Tilt);
                        SetProperty(props, settings._tilt, settings._autoTilt);

                        props = GetControlProperties(CameraControlProperty.Zoom);
                        SetProperty(props, settings._zoom, settings._autoZoom);
                    }

                    if (format) { Format = settings._format; }

                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.E_INVALIDARG;
            }
            else _base._lastError = HResult.MF_E_INVALIDREQUEST;
            return (int)_base._lastError;
        }

        #endregion


        #region Private - Get/Set Video Control Properties / ProcAmp Properties

        internal WebcamProperty GetControlProperties(CameraControlProperty property)
        {
            HResult result = HResult.ERROR_NOT_READY;

            WebcamProperty settings = new WebcamProperty
            {
                _name = property.ToString(),
                _controlProp = property
            };

            if (_base._webcamMode)
            {
#pragma warning disable IDE0019 // Use pattern matching
                IAMCameraControl control = WebcamMediaSource as IAMCameraControl;
#pragma warning restore IDE0019 // Use pattern matching
                if (control != null)
                {
                    result = control.GetRange(property, out settings._min, out settings._max, out settings._step, out settings._default, out CameraControlFlags flags);

                    if (result == NO_ERROR)
                    {
                        settings._supported = (flags & CameraControlFlags.Manual) != 0;
                        settings._autoSupport = (flags & CameraControlFlags.Auto) != 0;

                        control.Get(property, out settings._value, out flags);
                        settings._auto = (flags & CameraControlFlags.Auto) != 0;
                    }

                    if (_webcamSourceCreated) Marshal.ReleaseComObject(control);
                }
            }
            _base._lastError = result;
            return settings;
        }

        internal void SetControlProperties(CameraControlProperty property, WebcamProperty value)
        {
            HResult result = HResult.ERROR_NOT_READY;
            if (_base._webcamMode)
            {
                if (value == null || value._isProcAmp || value._controlProp != property)
                {
                    result = HResult.E_INVALIDARG;
                }
                else
                {
                    WebcamProperty settings = GetControlProperties(property);
                    if (!settings._supported) result = HResult.MF_E_NOT_AVAILABLE;
                    else if (value._auto && settings._auto) result = NO_ERROR;
                    else if (!value._auto && (value._value < settings._min || value._value > settings._max)) result = HResult.MF_E_OUT_OF_RANGE;

                    if (result == HResult.ERROR_NOT_READY)
                    {
                        try
                        {
#pragma warning disable IDE0019 // Use pattern matching
                            IAMCameraControl control = WebcamMediaSource as IAMCameraControl;
#pragma warning restore IDE0019 // Use pattern matching
                            if (control != null)
                            {
                                result = control.Set(property, value._value, value._auto ? CameraControlFlags.Auto : CameraControlFlags.Manual);
                                if (_webcamSourceCreated) Marshal.ReleaseComObject(control);
                            }
                        }
                        catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
                    }
                }
            }
            _base._lastError = result;
        }

        internal WebcamProperty GetProcAmpProperties(VideoProcAmpProperty property)
        {
            HResult result = HResult.ERROR_NOT_READY;

            WebcamProperty settings = new WebcamProperty
            {
                _name = property.ToString(),
                _procAmpProp = property,
                _isProcAmp = true
            };

            if (_base._webcamMode)
            {
#pragma warning disable IDE0019 // Use pattern matching
                IAMVideoProcAmp control = WebcamMediaSource as IAMVideoProcAmp;
#pragma warning restore IDE0019 // Use pattern matching
                if (control != null)
                {
                    result = control.GetRange(property, out settings._min, out settings._max, out settings._step, out settings._default, out VideoProcAmpFlags flags);

                    if (result == NO_ERROR)
                    {
                        settings._supported = (flags & VideoProcAmpFlags.Manual) != 0;
                        settings._autoSupport = (flags & VideoProcAmpFlags.Auto) != 0;

                        control.Get(property, out settings._value, out flags);
                        settings._auto = (flags & VideoProcAmpFlags.Auto) != 0;
                    }
                    if (_webcamSourceCreated) Marshal.ReleaseComObject(control);
                }
            }
            _base._lastError = result;
            return settings;
        }

        internal void SetProcAmpProperties(VideoProcAmpProperty property, WebcamProperty value)
        {
            HResult result = HResult.ERROR_NOT_READY;
            if (_base._webcamMode)
            {
                if (value == null || !value._isProcAmp || value._procAmpProp != property)
                {
                    result = HResult.E_INVALIDARG;
                }
                else
                {
                    WebcamProperty props = GetProcAmpProperties(property);
                    if (!props._supported) result = HResult.MF_E_NOT_AVAILABLE;
                    else if (value._auto && props._auto) result = NO_ERROR;
                    else if (!value._auto && (value._value < props._min || value._value > props._max)) result = HResult.MF_E_OUT_OF_RANGE;

                    if (result == HResult.ERROR_NOT_READY)
                    {
                        try
                        {
#pragma warning disable IDE0019 // Use pattern matching
                            IAMVideoProcAmp control = WebcamMediaSource as IAMVideoProcAmp;
#pragma warning restore IDE0019 // Use pattern matching
                            if (control != null)
                            {
                                result = control.Set(property, value._value, value._auto ? VideoProcAmpFlags.Auto : VideoProcAmpFlags.Manual);
                                if (_webcamSourceCreated) Marshal.ReleaseComObject(control);
                            }
                        }
                        catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
                    }
                }
            }
            _base._lastError = result;
        }

        #endregion

        #region Public - Get/Set Video Control Properties

        /// <summary>
        /// Gets or sets the exposure property (if supported) of the playing webcam.
        /// <br/>Values are in log base 2 seconds,
        /// <br/>for values less than zero the exposure time is 1/2^n seconds (eg. -3 = 1/8),
        /// <br/>and for values zero or above the exposure time is 2^n seconds (eg. 0 = 1 and 2 = 4).
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty Exposure
        {
            get { return GetControlProperties(CameraControlProperty.Exposure); }
            set { SetControlProperties(CameraControlProperty.Exposure, value); }
        }

        /// <summary>
        /// Gets or sets the flash property (if supported) of the playing webcam.
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty Flash
        {
            get { return GetControlProperties(CameraControlProperty.Flash); }
            set { SetControlProperties(CameraControlProperty.Flash, value); }
        }

        /// <summary>
        /// Gets or sets the focus property (if supported) of the playing webcam.
        /// <br/>Values represent the distance to the optimally focused target, in millimeters.
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty Focus
        {
            get { return GetControlProperties(CameraControlProperty.Focus); }
            set { SetControlProperties(CameraControlProperty.Focus, value); }
        }

        /// <summary>
        /// Gets or sets the iris property (if supported) of the playing webcam.
        /// <br/>Values are in units of f-stop * 10 (a larger f-stop value will result in darker images).
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty Iris
        {
            get { return GetControlProperties(CameraControlProperty.Iris); }
            set { SetControlProperties(CameraControlProperty.Iris, value); }
        }

        /// <summary>
        /// Gets or sets the pan property (if supported) of the playing webcam.
        /// <br/>Values are in degrees.
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty Pan
        {
            get { return GetControlProperties(CameraControlProperty.Pan); }
            set { SetControlProperties(CameraControlProperty.Pan, value); }
        }

        /// <summary>
        /// Gets or sets the roll property (if supported) of the playing webcam.
        /// <br/>Values are in degrees.
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty Roll
        {
            get { return GetControlProperties(CameraControlProperty.Roll); }
            set { SetControlProperties(CameraControlProperty.Roll, value); }
        }

        /// <summary>
        /// Gets or sets the tilt property (if supported) of the playing webcam.
        /// <br/>Values are in degrees.
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty Tilt
        {
            get { return GetControlProperties(CameraControlProperty.Tilt); }
            set { SetControlProperties(CameraControlProperty.Tilt, value); }
        }

        /// <summary>
        /// Gets or sets the zoom property (if supported) of the playing webcam.
        /// <br/>Values are in millimeters.
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty Zoom
        {
            get { return GetControlProperties(CameraControlProperty.Zoom); }
            set { SetControlProperties(CameraControlProperty.Zoom, value); }
        }

        #endregion

        #region Public - Get/SetVideo ProcAmp Properties

        /// <summary>
        /// Gets or sets the backlight compensation property (if supported) of the playing webcam.
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty BacklightCompensation
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.BacklightCompensation); }
            set { SetProcAmpProperties(VideoProcAmpProperty.BacklightCompensation, value); }
        }

        /// <summary>
        /// Gets or sets the brightness property (if supported) of the playing webcam.
        /// <br/>See also: Player.Webcam.SetProperty and Player.Video.Brightness.
        /// </summary>
        public WebcamProperty Brightness
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Brightness); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Brightness, value); }
        }

        /// <summary>
        /// Gets or sets the color enable property (if supported) of the playing webcam.
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty ColorEnable
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.ColorEnable); }
            set { SetProcAmpProperties(VideoProcAmpProperty.ColorEnable, value); }
        }

        /// <summary>
        /// Gets or sets the contrast property (if supported) of the playing webcam.
        /// <br/>See also: Player.Webcam.SetProperty and Player.Video.Contrast.
        /// </summary>
        public WebcamProperty Contrast
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Contrast); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Contrast, value); }
        }

        /// <summary>
        /// Gets or sets the gain property (if supported) of the playing webcam.
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty Gain
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Gain); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Gain, value); }
        }

        /// <summary>
        /// Gets or sets the gamma property (if supported) of the playing webcam.
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty Gamma
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Gamma); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Gamma, value); }
        }

        /// <summary>
        /// Gets or sets the hue property (if supported) of the playing webcam.
        /// <br/>See also: Player.Webcam.SetProperty and Player.Video.Hue.
        /// </summary>
        public WebcamProperty Hue
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Hue); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Hue, value); }
        }

        /// <summary>
        /// Gets or sets the power line frequency property (if supported) of the playing webcam.
        /// <br/>Values: 0 = disabled, 1 = 50 Hz, 2 = 60 Hz, 3 = automatic).
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty PowerLineFrequency
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.PowerLineFrequency); }
            set { SetProcAmpProperties(VideoProcAmpProperty.PowerLineFrequency, value); }
        }

        /// <summary>
        /// Gets or sets the saturation property (if supported) of the playing webcam.
        /// <br/>See also: Player.Webcam.SetProperty and Player.Video.Saturation.
        /// </summary>
        public WebcamProperty Saturation
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Saturation); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Saturation, value); }
        }

        /// <summary>
        /// Gets or sets the sharpness property (if supported) of the playing webcam.
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty Sharpness
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Sharpness); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Sharpness, value); }
        }

        /// <summary>
        /// Gets or sets the white balance temperature property (if supported) of the playing webcam.
        /// <br/>See also: Player.Webcam.SetProperty.
        /// </summary>
        public WebcamProperty WhiteBalance
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.WhiteBalance); }
            set { SetProcAmpProperties(VideoProcAmpProperty.WhiteBalance, value); }
        }

        #endregion


        #region Private - Get Video Output Format

        private WebcamFormat[] GetWebcamFormats(string webcamId, bool filter, bool exact, int minWidth, int minHeight, float minFrameRate)
        {
            List<WebcamFormat> list = null;

            HResult result = GetMediaSource(webcamId, out IMFMediaSource source);
            if (result == NO_ERROR)
            {
                result = MFExtern.MFCreateSourceReaderFromMediaSource(source, null, out IMFSourceReader reader);
                if (result == NO_ERROR)
                {
                    HResult readResult = NO_ERROR;

                    int streamIndex = 0;
                    int typeIndex = 0;

                    float frameRate = 0;
                    bool match;

                    list = new List<WebcamFormat>(250);

                    while (readResult == NO_ERROR)
                    {
                        readResult = reader.GetNativeMediaType(streamIndex, typeIndex, out IMFMediaType type);
                        if (readResult == NO_ERROR)
                        {
                            MFExtern.MFGetAttributeRatio(type, MFAttributesClsid.MF_MT_FRAME_RATE, out int num, out int denum);
                            if (denum > 0) frameRate = (float)num / denum;
                            MFExtern.MFGetAttributeRatio(type, MFAttributesClsid.MF_MT_FRAME_SIZE, out int width, out int height);

                            match = true;
                            if (filter)
                            {
                                if (exact)
                                {
                                    if ((minWidth != -1 && width != minWidth) || (minHeight != -1 && height != minHeight) || (minFrameRate != -1 && frameRate != minFrameRate)) match = false;
                                }
                                else if ((minWidth != -1 && width < minWidth) || (minHeight != -1 && height < minHeight) || (minFrameRate != -1 && frameRate < minFrameRate)) match = false;
                            }

                            if (match && !FormatExists(list, width, height, frameRate))
                            {
                                list.Add(new WebcamFormat(streamIndex, typeIndex, width, height, frameRate));
                            }

                            typeIndex++;
                            Marshal.ReleaseComObject(type);
                        }
                        // read formats of 1 track (stream) only - can't switch tracks (?)
                        //else if (readResult == HResult.MF_E_NO_MORE_TYPES)
                        //{
                        //    readResult = NO_ERROR;
                        //    streamIndex++;
                        //    typeIndex = 0;
                        //}
                    }
                    if (reader != null) Marshal.ReleaseComObject(reader);
                }
                if (source != null) Marshal.ReleaseComObject(source);
            }

            _base._lastError = result;
            return (list == null || list.Count == 0) ? null : list.ToArray();
        }

        private static bool FormatExists(List<WebcamFormat> list, int width, int height, float frameRate)
        {
            bool exists = false;
            int length = list.Count;

            for (int i = 0; i < length; i++)
            {
                if (list[i]._width == width && list[i]._height == height && list[i]._frameRate == frameRate)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        private static HResult GetMediaSource(string webcamId, out IMFMediaSource source)
        {
            MFExtern.MFCreateAttributes(out IMFAttributes attributes, 2);
            attributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
            attributes.SetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, webcamId);

            HResult result = MFExtern.MFCreateDeviceSource(attributes, out source);
            if ((uint)result == 0xC00D36E6) result = HResult.ERROR_DEVICE_NOT_CONNECTED;

            Marshal.ReleaseComObject(attributes);
            return result;
        }

        internal WebcamFormat GetHighFormat(WebcamDevice webcam, bool photo)
        {
            WebcamFormat format = null;

            WebcamFormat[] formats = GetWebcamFormats(webcam._id, false, false, 0, 0, 0);
            if (formats != null)
            {
                format = formats[0];
                int count = formats.Length;
                int frameRate = photo ? 1 : 15;

                for (int i = 1; i < count; i++)
                {
                    if (formats[i]._width >= format._width &&
                        formats[i]._height >= format._height &&
                        formats[i]._frameRate >= format._frameRate)
                    {
                        format = formats[i];
                    }
                    else if (formats[i]._width > format._width &&
                             formats[i]._height > format._height &&
                             formats[i]._frameRate >= frameRate)
                    {
                        format = formats[i];
                    }
                }
            }
            return format;
        }

        internal WebcamFormat GetLowFormat(WebcamDevice webcam)
        {
            WebcamFormat format = null;

            WebcamFormat[] formats = GetWebcamFormats(webcam._id, false, false, 0, 0, 0);
            if (formats != null)
            {
                format = formats[0];
                int count = formats.Length;

                for (int i = 1; i < count; i++)
                {
                    if (formats[i]._width <= format._width &&
                        formats[i]._height <= format._height &&
                        formats[i]._height >= 100 &&
                        formats[i]._frameRate <= format._frameRate &&
                        formats[i]._frameRate >= 15)
                        format = formats[i];
                }
            }
            return format;
        }

        internal WebcamFormat GetCurrentFormat(WebcamDevice webcam)
        {
            WebcamFormat format     = null;
            bool         found      = false;
            float        frameRate  = 0;
            int          width      = 0;
            int          height     = 0;

            HResult result = GetMediaSource(webcam._id, out IMFMediaSource source);
            if (result == NO_ERROR)
            {
                result = MFExtern.MFCreateSourceReaderFromMediaSource(source, null, out IMFSourceReader reader);
                if (result == NO_ERROR)
                {
                    result = reader.GetCurrentMediaType(0, out IMFMediaType type);
                    if (result == NO_ERROR)
                    {
                        MFExtern.MFGetAttributeRatio(type, MFAttributesClsid.MF_MT_FRAME_RATE, out int num, out int denum);
                        if (denum > 0) frameRate = (float)num / denum;
                        MFExtern.MFGetAttributeRatio(type, MFAttributesClsid.MF_MT_FRAME_SIZE, out width, out height);
                        Marshal.ReleaseComObject(type);

                        found = true;
                    }
                    if (reader != null) Marshal.ReleaseComObject(reader);
                }
                if (source != null) Marshal.ReleaseComObject(source);
            }

            if (found)
            {
                WebcamFormat[] formats = GetWebcamFormats(webcam._id, true, true, width, height, frameRate);
                if (formats != null) format = formats[0];
            }
            return format;
        }

        internal WebcamFormat GetStandardFormat(WebcamDevice webcam)
        {
            WebcamFormat format = null;

            WebcamFormat[] formats = GetWebcamFormats(webcam._id, false, false, 0, 0, 0);
            if (formats != null)
            {
                format = formats[0];
            }
            return format;
        }

        #endregion

        #region Public - Get Video Output Formats

        /// <summary>
        /// Returns the default video output format of the playing webcam.
        /// <br/>The format can be used with the Player.Webcam.Format and Player.Play methods.
        /// </summary>
        public WebcamFormat GetDefaultFormat()
        {
            if (!_base._webcamMode)
            {
                _base._lastError = HResult.MF_E_INVALIDREQUEST;
                return null;
            }
            return GetStandardFormat(_base._webcamDevice);
        }

        /// <summary>
        /// Returns the default video output format of the specified webcam.
        /// <br/>The format can be used with the Player.Webcam.Format and Player.Play methods.
        /// </summary>
        /// <param name="webcam">The webcam whose default video output format is to be obtained.</param>
        public WebcamFormat GetDefaultFormat(WebcamDevice webcam)
        {
            if (webcam == null)
            {
                _base._lastError = HResult.MF_E_INVALIDREQUEST;
                return null;
            }
            return GetStandardFormat(webcam);
        }

        /// <summary>
        /// Returns the available video output formats of the playing webcam.
        /// <br/>The formats can be used with the Player.Webcam.Format and Player.Play methods.
        /// </summary>
        public WebcamFormat[] GetFormats()
        {
            if (!_base._webcamMode)
            {
                _base._lastError = HResult.MF_E_INVALIDREQUEST;
                return null;
            }
            return GetWebcamFormats(_base._webcamDevice._id, false, false, 0, 0, 0);
        }

        /// <summary>
        /// Returns the available video output formats of the specified webcam.
        /// <br/>The formats can be used with the Player.Play methods for webcams.
        /// </summary>
        /// <param name="webcam">The webcam whose video output formats are to be obtained.</param>
        public WebcamFormat[] GetFormats(WebcamDevice webcam)
        {
            if (webcam == null)
            {
                _base._lastError = HResult.MF_E_INVALIDREQUEST;
                return null;
            }
            return GetWebcamFormats(webcam._id, false, false, 0, 0, 0);
        }

        /// <summary>
        /// Returns the available video output formats of the playing webcam that match the specified values.
        /// <br/>The formats can be used with the Player.Webcam.Format and Player.Play methods.
        /// </summary>
        /// <param name="exact">A value that indicates whether the specified values must exactly match the webcam formats,
        /// <br/>or whether they are minimum values.</param>
        /// <param name="width">The (minimum) width of the video frames.
        /// <br/>A value of -1 indicates that this parameter should be ignored.</param>
        /// <param name="height">The (minimum) height of the video frames.
        /// <br/>A value of -1 indicates that this parameter should be ignored.</param>
        /// <param name="frameRate">The (minimum) frame rate of the video output format.
        /// <br/>A value of -1 indicates that this parameter should be ignored.</param>
        public WebcamFormat[] GetFormats(bool exact, int width, int height, float frameRate)
        {
            if (!_base._webcamMode)
            {
                _base._lastError = HResult.MF_E_INVALIDREQUEST;
                return null;
            }
            return GetWebcamFormats(_base._webcamDevice._id, true, exact, width, height, frameRate);
        }

        /// <summary>
        /// Returns the available video output formats of the specified webcam that match the specified values.
        /// <br/>The formats can be used with the Player.Play methods for webcams.
        /// </summary>
        /// <param name="webcam">The webcam whose video output formats are to be obtained.</param>
        /// <param name="exact">A value that indicates whether the specified values must exactly match the webcam formats,
        /// <br/>or whether they are minimum values.</param>
        /// <param name="width">The (minimum) width of the video frames.
        /// <br/>A value of -1 indicates that this parameter should be ignored.</param>
        /// <param name="height">The (minimum) height of the video frames.
        /// <br/>A value of -1 indicates that this parameter should be ignored.</param>
        /// <param name="frameRate">The (minimum) frame rate of the video output format.
        /// <br/>A value of -1 indicates that this parameter should be ignored.</param>
        public WebcamFormat[] GetFormats(WebcamDevice webcam, bool exact, int width, int height, float frameRate)
        {
            return GetWebcamFormats(webcam._id, true, exact, width, height, frameRate);
        }

        #endregion


        //#region Public - Video Recorder

        ///// <summary>
        ///// Provides access to the webcam video recorder settings of the player (for example, Player.Webcam.Recorder.Start).
        ///// </summary>
        //public VideoRecorder Recorder
        //{
        //    get
        //    {
        //        if (_base._webcamRecorderClass == null) _base._webcamRecorderClass = new VideoRecorder(_base, true);
        //        return _base._webcamRecorderClass;
        //    }
        //}

        //#endregion

    }
}