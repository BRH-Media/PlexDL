using System;
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

        private Player _base;

        #endregion Fields (Webcam Class)

        #region Main / Playing / Device / Format / GetDevices / Update

        internal Webcam(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets a value indicating whether a webcam is playing (includes paused webcam). Use the Player.Play method to play a webcam.
        /// </summary>
        public bool Playing
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._webcamMode;
            }
        }

        /// <summary>
        /// Gets the playing webcam device. Use the Player.Play method to play a webcam.
        /// </summary>
        public WebcamDevice Device
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._webcamDevice;
            }
        }

        /// <summary>
        /// Gets or sets the audio input device of the playing webcam. See also: Player.AudioInput.GetDevices.
        /// </summary>
        public AudioInputDevice AudioInput
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (_base._webcamMode) return _base._micDevice;
                return null;
            }
            set
            {
                if (_base._webcamMode)
                {
                    _base._lastError = Player.NO_ERROR;
                    if ((value == null && _base._micDevice != null) ||
                        (value != null && _base._micDevice == null) ||
                        _base._micDevice._id != value._id)
                    {
                        _base._micDevice = value;
                        _base.AV_UpdateTopology();
                        if (_base._mediaAudioInputDeviceChanged != null) _base._mediaAudioInputDeviceChanged(_base, EventArgs.Empty);
                    }
                }
                else _base._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
            }
        }

        /// <summary>
        /// Gets or sets the video output format of the playing webcam.
        /// </summary>
        public WebcamFormat Format
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._webcamFormat;
            }
            set
            {
                if (value == null) _base._lastError = HResult.E_INVALIDARG;
                else
                {
                    if (_base._webcamMode)
                    {
                        _base._lastError = Player.NO_ERROR;
                        if ((value == null && _base._webcamFormat != null) ||
                            (value != null && _base._webcamFormat == null) ||
                            (value._typeIndex != _base._webcamFormat._typeIndex))
                        {
                            _base._webcamFormat = value;
                            _base.AV_UpdateTopology();
                            if (_base._mediaWebcamFormatChanged != null) _base._mediaWebcamFormatChanged(_base, EventArgs.Empty);
                        }
                    }
                    else _base._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
                }
            }
        }

        /// <summary>
        /// Returns a list of the system's enabled webcam devices. Returns null if no enabled webcam devices are present. Use the Player.Play method to play a webcam.
        /// </summary>
        public WebcamDevice[] GetDevices()
        {
            WebcamDevice[] devices = null;
            IMFAttributes attributes;
            IMFActivate[] webcams;
            int webcamCount;
            int length;
            HResult result;

            MFExtern.MFCreateAttributes(out attributes, 1);
            attributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
            if (Environment.Version.Major < 4) // fix for NET 2.0 and 3.5 - only 1 device (? SizeParamIndex error)
            {
                result = MFExtern.MFEnumDeviceSourcesEx(attributes, out webcams, out webcamCount);
                if (webcams != null) webcamCount = webcams.Length;
            }
            else result = MFExtern.MFEnumDeviceSources(attributes, out webcams, out webcamCount);

            if (result == Player.NO_ERROR && webcams != null)
            {
                devices = new WebcamDevice[webcamCount];

                for (int i = 0; i < webcamCount; i++)
                {
                    devices[i] = new WebcamDevice();

#pragma warning disable IDE0059 // Unnecessary assignment of a value
                    webcams[i].GetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME, _base._textBuffer1, _base._textBuffer1.Capacity,
                        out length);
                    devices[i]._name = _base._textBuffer1.ToString();
                    webcams[i].GetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, _base._textBuffer1,
                        _base._textBuffer1.Capacity, out length);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                    devices[i]._id = _base._textBuffer1.ToString();

                    Marshal.ReleaseComObject(webcams[i]);
                }
            }

            Marshal.ReleaseComObject(attributes);

            _base._lastError = result;
            return devices;
        }

        /// <summary>
        /// Updates or restores the audio and video playback of the playing webcam.
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            if (_base._webcamMode)
            {
                _base._lastError = Player.NO_ERROR;
                _base.AV_UpdateTopology();
            }
            else _base._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;

            return (int)_base._lastError;
        }

        #endregion Main / Playing / Device / Format / GetDevices / Update

        #region Private Video Control/ProcAmp Methods

        internal WebcamProperty GetControlProperties(CameraControlProperty property)
        {
            HResult result = HResult.ERROR_NOT_READY;

            WebcamProperty settings = new WebcamProperty();
            //settings._name = property.ToString();

            if (_base._webcamMode)
            {
                CameraControlFlags flags;

                IAMCameraControl control = _base.mf_MediaSource as IAMCameraControl;
                result = control.GetRange(property, out settings._min, out settings._max, out settings._step, out settings._default, out flags);

                if (result == Player.NO_ERROR)
                {
                    settings._supported = (flags & CameraControlFlags.Manual) != 0;
                    settings._autoSupport = (flags & CameraControlFlags.Auto) != 0;

                    control.Get(property, out settings._current, out flags);
                    settings._auto = (flags & CameraControlFlags.Auto) != 0;
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
                WebcamProperty settings = GetControlProperties(property);
                if (!settings._supported) result = HResult.MF_E_NOT_AVAILABLE;
                else if (value._auto && settings._auto) result = Player.NO_ERROR;
                else if (!value._auto && (value._current < settings._min || value._current > settings._max)) result = HResult.MF_E_OUT_OF_RANGE;

                if (result == HResult.ERROR_NOT_READY)
                {
                    try
                    {
                        result = ((IAMCameraControl)_base.mf_MediaSource).Set(property, value._current,
                            value._auto ? CameraControlFlags.Auto : CameraControlFlags.Manual);
                    }
                    catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
                }
            }

            _base._lastError = result;
        }

        internal WebcamProperty GetProcAmpProperties(VideoProcAmpProperty property)
        {
            HResult result = HResult.ERROR_NOT_READY;

            WebcamProperty settings = new WebcamProperty();
            //settings._name = property.ToString();

            if (_base._webcamMode)
            {
                VideoProcAmpFlags flags;

                IAMVideoProcAmp control = _base.mf_MediaSource as IAMVideoProcAmp;
                result = control.GetRange(property, out settings._min, out settings._max, out settings._step, out settings._default, out flags);

                if (result == Player.NO_ERROR)
                {
                    settings._supported = (flags & VideoProcAmpFlags.Manual) != 0;
                    settings._autoSupport = (flags & VideoProcAmpFlags.Auto) != 0;

                    control.Get(property, out settings._current, out flags);
                    settings._auto = (flags & VideoProcAmpFlags.Auto) != 0;
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
                WebcamProperty settings = GetProcAmpProperties(property);
                if (!settings._supported) result = HResult.MF_E_NOT_AVAILABLE;
                else if (value._auto && settings._auto) result = Player.NO_ERROR;
                else if (!value._auto && (value._current < settings._min || value._current > settings._max)) result = HResult.MF_E_OUT_OF_RANGE;

                if (result == HResult.ERROR_NOT_READY)
                {
                    try
                    {
                        result = (HResult)((IAMVideoProcAmp)_base.mf_MediaSource).Set(property, value._current,
                            value._auto ? VideoProcAmpFlags.Auto : VideoProcAmpFlags.Manual);
                    }
                    catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
                }
            }

            _base._lastError = result;
        }

        #endregion Private Video Control/ProcAmp Methods

        #region Video Control Properties

        /// <summary>
        /// Gets or sets the exposure property (if supported) of the active webcam (usage: get, examine, change, set). Values are in log base 2 seconds, for values less than zero the exposure time is 1/2^n seconds (eg. -3 = 1/8), and for values zero or above the exposure time is 2^n seconds (eg. 0 = 1 and 2 = 4).
        /// </summary>
        public WebcamProperty Exposure
        {
            get { return GetControlProperties(CameraControlProperty.Exposure); }
            set { SetControlProperties(CameraControlProperty.Exposure, value); }
        }

        /// <summary>
        /// Gets or sets the flash property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty Flash
        {
            get { return GetControlProperties(CameraControlProperty.Flash); }
            set { SetControlProperties(CameraControlProperty.Flash, value); }
        }

        /// <summary>
        /// Gets or sets the focus property (if supported) of the active webcam (usage: get, examine, change, set). Values represent the distance to the optimally focused target, in millimeters.
        /// </summary>
        public WebcamProperty Focus
        {
            get { return GetControlProperties(CameraControlProperty.Focus); }
            set { SetControlProperties(CameraControlProperty.Focus, value); }
        }

        /// <summary>
        /// Gets or sets the iris property (if supported) of the active webcam (usage: get, examine, change, set). Values are in units of f-stop * 10.
        /// </summary>
        public WebcamProperty Iris
        {
            get { return GetControlProperties(CameraControlProperty.Iris); }
            set { SetControlProperties(CameraControlProperty.Iris, value); }
        }

        /// <summary>
        /// Gets or sets the pan property (if supported) of the active webcam (usage: get, examine, change, set). Values are in degrees.
        /// </summary>
        public WebcamProperty Pan
        {
            get { return GetControlProperties(CameraControlProperty.Pan); }
            set { SetControlProperties(CameraControlProperty.Pan, value); }
        }

        /// <summary>
        /// Gets or sets the roll property (if supported) of the active webcam (usage: get, examine, change, set). Values are in degrees.
        /// </summary>
        public WebcamProperty Roll
        {
            get { return GetControlProperties(CameraControlProperty.Roll); }
            set { SetControlProperties(CameraControlProperty.Roll, value); }
        }

        /// <summary>
        /// Gets or sets the tilt property (if supported) of the active webcam (usage: get, examine, change, set). Values are in degrees.
        /// </summary>
        public WebcamProperty Tilt
        {
            get { return GetControlProperties(CameraControlProperty.Tilt); }
            set { SetControlProperties(CameraControlProperty.Tilt, value); }
        }

        /// <summary>
        /// Gets or sets the zoom property (if supported) of the active webcam (usage: get, examine, change, set). Values are in millimeters.
        /// </summary>
        public WebcamProperty Zoom
        {
            get { return GetControlProperties(CameraControlProperty.Zoom); }
            set { SetControlProperties(CameraControlProperty.Zoom, value); }
        }

        #endregion Video Control Properties

        #region Video ProcAmp Properties

        /// <summary>
        /// Gets or sets the backlight compensation property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty Backlight
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.BacklightCompensation); }
            set { SetProcAmpProperties(VideoProcAmpProperty.BacklightCompensation, value); }
        }

        /// <summary>
        /// Gets or sets the brightness property (if supported) of the active webcam (usage: get, examine, change, set). See also: Player.Video.Brightness.
        /// </summary>
        public WebcamProperty Brightness
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Brightness); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Brightness, value); }
        }

        /// <summary>
        /// Gets or sets the color enable property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty ColorEnable
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.ColorEnable); }
            set { SetProcAmpProperties(VideoProcAmpProperty.ColorEnable, value); }
        }

        /// <summary>
        /// Gets or sets the contrast property (if supported) of the active webcam (usage: get, examine, change, set). See also: Player.Video.Contrast.
        /// </summary>
        public WebcamProperty Contrast
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Contrast); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Contrast, value); }
        }

        /// <summary>
        /// Gets or sets the gain property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty Gain
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Gain); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Gain, value); }
        }

        /// <summary>
        /// Gets or sets the gamma property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty Gamma
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Gamma); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Gamma, value); }
        }

        /// <summary>
        /// Gets or sets the hue property (if supported) of the active webcam (usage: get, examine, change, set). See also: Player.Video.Hue.
        /// </summary>
        public WebcamProperty Hue
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Hue); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Hue, value); }
        }

        /// <summary>
        /// Gets or sets the saturation property (if supported) of the active webcam (usage: get, examine, change, set). See also: Player.Video.Saturation.
        /// </summary>
        public WebcamProperty Saturation
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Saturation); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Saturation, value); }
        }

        /// <summary>
        /// Gets or sets the sharpness property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty Sharpness
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.Sharpness); }
            set { SetProcAmpProperties(VideoProcAmpProperty.Sharpness, value); }
        }

        /// <summary>
        /// Gets or sets the whiteBalance property (if supported) of the active webcam (usage: get, examine, change, set).
        /// </summary>
        public WebcamProperty WhiteBalance
        {
            get { return GetProcAmpProperties(VideoProcAmpProperty.WhiteBalance); }
            set { SetProcAmpProperties(VideoProcAmpProperty.WhiteBalance, value); }
        }

        #endregion Video ProcAmp Properties

        #region Private Video Output Format Methods

        private WebcamFormat[] GetWebcamFormats(string webcamId, bool filter, bool exact, int minWidth, int minHeight, float minFrameRate)
        {
            IMFMediaSource source;
            List<WebcamFormat> list = null;

            HResult result = GetMediaSource(webcamId, out source);
            if (result == Player.NO_ERROR)
            {
                IMFSourceReader reader;

                result = MFExtern.MFCreateSourceReaderFromMediaSource(source, null, out reader);
                if (result == Player.NO_ERROR)
                {
                    HResult readResult = Player.NO_ERROR;
                    IMFMediaType type;

                    int streamIndex = 0;
                    int typeIndex = 0;

                    int num, denum;
                    int width, height;
                    float frameRate = 0;
                    bool match;

                    list = new List<WebcamFormat>(250);

                    while (readResult == Player.NO_ERROR)
                    {
                        readResult = reader.GetNativeMediaType(streamIndex, typeIndex, out type);
                        if (readResult == Player.NO_ERROR)
                        {
                            MFExtern.MFGetAttributeRatio(type, MFAttributesClsid.MF_MT_FRAME_RATE, out num, out denum);
                            if (denum > 0) frameRate = (float)num / denum;
                            MFExtern.MFGetAttributeRatio(type, MFAttributesClsid.MF_MT_FRAME_SIZE, out width, out height);

                            match = true;
                            if (filter)
                            {
                                if (exact)
                                {
                                    if ((minWidth != -1 && width != minWidth) || (minHeight != -1 && height != minHeight) ||
                                        (minFrameRate != -1 && frameRate != minFrameRate)) match = false;
                                }
                                else if ((minWidth != -1 && width < minWidth) || (minHeight != -1 && height < minHeight) ||
                                         (minFrameRate != -1 && frameRate < minFrameRate)) match = false;
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
                        //    readResult = Player.NO_ERROR;
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

        private bool FormatExists(List<WebcamFormat> list, int width, int height, float frameRate)
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

        private HResult GetMediaSource(string webcamId, out IMFMediaSource source)
        {
            IMFAttributes attributes;

            MFExtern.MFCreateAttributes(out attributes, 2);
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

        #endregion Private Video Output Format Methods

        #region Video Output Format Methods

        /// <summary>
        /// Returns the available video output formats of the playing webcam. The formats can be used with the Player.Webcam.Format and Player.Play methods.
        /// </summary>
        public WebcamFormat[] GetFormats()
        {
            if (!_base._webcamMode)
            {
                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                return null;
            }

            return GetWebcamFormats(_base._webcamDevice._id, false, false, 0, 0, 0);
        }

        /// <summary>
        /// Returns the available video output formats of the specified webcam. The formats can be used with the Player.Play methods for webcams.
        /// </summary>
        /// <param name="webcam">The webcam from which the video output formats must be obtained.</param>
        public WebcamFormat[] GetFormats(WebcamDevice webcam)
        {
            return GetWebcamFormats(webcam._id, false, false, 0, 0, 0);
        }

        /// <summary>
        /// Returns the available video output formats of the playing webcam that match the specified values. The formats can be used with the Player.Webcam.Format and Player.Play methods.
        /// </summary>
        /// <param name="exact">A value indicating whether the specified values must exactly match the webcam formats or whether they are minimum values.</param>
        /// <param name="width">The minimum width of the video frames. Use -1 to ignore the value.</param>
        /// <param name="height">The minimum height of the video frames. Use -1 to ignore the value.</param>
        /// <param name="frameRate">The minimum frame rate of the video output format. Use -1 to ignore the value.</param>
        public WebcamFormat[] GetFormats(bool exact, int width, int height, float frameRate)
        {
            if (!_base._webcamMode)
            {
                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                return null;
            }

            return GetWebcamFormats(_base._webcamDevice._id, true, exact, width, height, frameRate);
        }

        /// <summary>
        /// Returns the available video output formats (or null) of the specified webcam that match the specified values. The formats can be used with the Player.Play methods for webcams.
        /// </summary>
        /// <param name="webcam">The webcam from which the video output formats must be obtained.</param>
        /// <param name="exact">A value indicating whether the specified values must exactly match the webcam formats or whether they are minimum values.</param>
        /// <param name="width">The minimum width of the video frames. Use -1 to ignore the value.</param>
        /// <param name="height">The minimum height of the video frames. Use -1 to ignore the value.</param>
        /// <param name="frameRate">The minimum frame rate of the video output format. Use -1 to ignore the value.</param>
        public WebcamFormat[] GetFormats(WebcamDevice webcam, bool exact, int width, int height, float frameRate)
        {
            return GetWebcamFormats(webcam._id, true, exact, width, height, frameRate);
        }

        #endregion Video Output Format Methods
    }
}