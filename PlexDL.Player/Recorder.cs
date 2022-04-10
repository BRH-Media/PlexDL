using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Recorder methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Recorder : HideObjectMembers
    {
        #region Fields (Recorder Class)

        private const int           NO_ERROR = 0;

        private Player              _base;

        private class MF_RecorderCallBack : IMFCaptureEngineOnEventCallback
        {
            #region Fields

            private Guid MF_CAPTURE_ENGINE_ERROR = new Guid(0x46b89fc6, 0x33cc, 0x4399, 0x9d, 0xad, 0x78, 0x4d, 0xe7, 0x7d, 0x58, 0x7c);

            private Recorder _base;
            private delegate void EndOfMediaDelegate();
            private EndOfMediaDelegate CallEndOfMedia;

            #endregion

            public MF_RecorderCallBack(Recorder recorder)
            {
                _base = recorder;
                CallEndOfMedia = new EndOfMediaDelegate(_base._base.AV_EndOfMedia);
            }

            public void Dispose()
            {
                try
                {
                    // TODO - reactivate 'old' callback?
                    if (_base._resetAfterStop)
                    {
                        _base._base.AV_UpdateTopology();
                        if (_base._base._hasOverlay) _base._base.AV_ShowOverlay();
                    }

                    _base = null;
                    CallEndOfMedia = null;
                }
                catch { /* ignored */ }
            }

            public HResult OnEvent(IMFMediaEvent mediaEvent)
            {
                if (_base.AwaitCallBack)
                {
                    mediaEvent.GetStatus(out _base.ErrorFromCallback);

                    _base.AwaitCallBack = false;
                    _base._base.WaitForEvent.Set();
                }
                else
                {
                    try
                    {
                        mediaEvent.GetStatus(out HResult status);

                        if (status != NO_ERROR && status != HResult.MF_E_VIDEO_RECORDING_DEVICE_PREEMPTED)
                        {
                            mediaEvent.GetExtendedType(out Guid mediaEventType);
                            if (mediaEventType == MF_CAPTURE_ENGINE_ERROR)
                            {
                                _base._base._lastError = status;
                                Control control = _base._base._display;
                                if (control == null)
                                {
                                    FormCollection forms = Application.OpenForms;
                                    if (forms != null && forms.Count > 0) control = forms[0];
                                }
                                if (control != null) control.BeginInvoke(CallEndOfMedia);
                                else _base._base.AV_EndOfMedia();
                            }
                        }
                    }
                    catch { /* ignored */ }
                }
                return HResult.S_OK;
            }
        }
        private MF_RecorderCallBack _callBack;
        private bool                AwaitCallBack;
        private HResult             ErrorFromCallback;

        private IMFCaptureEngine    _recorder;

        private bool                _audioOnly;
        private bool                _videoOnly;
        private VideoRotation       _rotation;


        private AudioFormat         _audioFormat        = Player.DEFAULT_RECORDER_AUDIO_FORMAT;
        private AudioBitRate        _audioBitRate       = Player.DEFAULT_RECORDER_AUDIO_BITRATE;
        private bool                _audioStereo        = Player.DEFAULT_AUDIO_TO_STEREO;
        private bool                _audioMono          = Player.DEFAULT_AUDIO_TO_MONO;


        private VideoFormat         _videoFormat        = Player.DEFAULT_RECORDER_VIDEO_FORMAT;
        private double              _videoScale         = 1;
        private int                 _videoFrameRate     = Player.DEFAULT_RECORDER_VIDEO_FRAMERATE;
        private int                 _videoBitRate       = Player.DEFAULT_RECORDER_VIDEO_BITRATE;

        private string              _fileName           = string.Empty;
        private string              _folderName         = Player.DEFAULT_RECORDER_FOLDER_NAME;
        private bool                _setFileExtension   = true;
        private bool                _setMetadata        = true;
        private bool                _resetAfterStop     = false;    // restore base callback just before or after recorder stop

        private RecordingInfo           _info;
        private bool                _hasAudio;
        private int                 _audioIndex;
        private bool                _hasVideo;
        private int                 _videoIndex;

        #endregion


        #region Main

        internal Recorder(Player player)
        {
            _base = player;

            // to prevent null exceptions for 'easy' users
            _info = new RecordingInfo
            {
                _hasAudio   = _hasAudio,
                _audio      = new AudioTrack(),
                _hasVideo   = _hasVideo,
                _video      = new VideoTrack()
            };
        }

        #endregion

        #region AudioOnly / VideoOnly / Rotation

        /// <summary>
        /// Gets or sets a value that indicates whether the recorder records only audio (and no video).
        /// <br/>Intended for use with webcams with audio input but also applies to other configurations.
        /// <br/>If set to true, the Player.Recorder.VideoOnly property is set to false.
        /// </summary>
        public bool AudioOnly
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _audioOnly;
            }
            set
            {
                _base._lastError = NO_ERROR;
                _audioOnly = value;
                if (value) _videoOnly = false;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the recorder records only video (and no audio).
        /// <br/>Intended for use with webcams with audio input but also applies to other configurations.
        /// <br/>If set to true, the Player.Recorder.AudioOnly property is set to false.
        /// </summary>
        public bool VideoOnly
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _videoOnly;
            }
            set
            {
                _base._lastError = NO_ERROR;
                _videoOnly = value;
                if (value) _audioOnly = false;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the video rotation for the next video recording.
        /// <br/>This setting differs from the Player.Video.Rotation setting.
        /// </summary>
        public VideoRotation Rotation
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _rotation;
            }
            set
            {
                _base._lastError = NO_ERROR;
                _rotation = value;
            }
        }

        #endregion

        #region Audio Format / Audio Bitrate / MonoToStereo / StereoToMono

        /// <summary>
        /// Gets or sets the audio format of the recorder (default: AAC (AudioFormat.AAC (.m4a))).
        /// <br/>The format may be changed by the recorder when video is also recorded and the set value is not supported.
        /// </summary>
        public AudioFormat AudioFormat
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _audioFormat;
            }
            set
            {
                if (!_base._recording)
                {
                    _base._lastError = NO_ERROR;
                    _audioFormat = value;
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
        }

        /// <summary>
        /// Gets or sets the audio bitrate of the recorder (default: 128 kb/s (Kbps_128)).
        /// <br/>The bitrate may be changed by the recorder if the set value is not supported by the set audio format.
        /// </summary>
        public AudioBitRate AudioBitRate
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _audioBitRate;
            }
            set
            {
                if (!_base._recording)
                {
                    _base._lastError = NO_ERROR;
                    _audioBitRate = value;
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the recorder records mono (1 channel) audio
        /// <br/>on 2 audio channels, if possible (default: false). 
        /// </summary>
        public bool MonoToStereo
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _audioStereo;
            }
            set
            {
                if (!_base._recording)
                {
                    _base._lastError = NO_ERROR;
                    _audioStereo = value;
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the recorder records stereo (2 channels) audio
        /// <br/>on 1 audio channel, if possible (default: false). 
        /// </summary>
        public bool StereoToMono
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _audioMono;
            }
            set
            {
                if (!_base._recording)
                {
                    _base._lastError = NO_ERROR;
                    _audioMono = value;
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
        }

        #endregion

        #region Video Format / Video Bitrate / Video Frame Rate / Video Scale

        /// <summary>
        /// Gets or sets the video format of the recorder (default: H.264 (VideoFormat.H264 (.mp4))).
        /// </summary>
        public VideoFormat VideoFormat
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _videoFormat;
            }
            set
            {
                if (!_base._recording)
                {
                    _base._lastError = NO_ERROR;
                    _videoFormat = value;
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
        }

        /// <summary>
        /// Gets or sets the video bitrate of the recorder in kilobits per second (default: 800 kb/s).
        /// <br/>The actual bitrate may differ from the set bitrate.
        /// </summary>
        public int VideoBitRate
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _videoBitRate;
            }
            set
            {
                if (!_base._recording)
                {
                    if (value < 10 || value > 100000)
                    {
                        _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                    }
                    else
                    {
                        _base._lastError = NO_ERROR;
                        _videoBitRate = value;
                    }
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
        }

        /// <summary>
        /// Gets or sets the video frame rate of the recorder in frames per second (default: -1 (frame rate of the webcam)).
        /// <br/>The actual frame rate may differ from the set frame rate.
        /// </summary>
        public int VideoFrameRate
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _videoFrameRate;
            }
            set
            {
                if (!_base._recording)
                {
                    if (value == -1 || (value >= 1 && value <= 300))
                    {
                        _base._lastError = NO_ERROR;
                        _videoFrameRate = value;
                    }
                    else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
        }

        /// <summary>
        /// Gets or sets the size of the video images recorded by the recorder relative to the original video size.
        /// <br/>Values from 0.1 to 2.0 (default: 1.0 (original size)).
        /// </summary>
        public double VideoScale
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _videoScale;
            }
            set
            {
                if (!_base._recording)
                {
                    if (value <= 0 || value > 2)
                    {
                        _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                    }
                    else
                    {
                        _base._lastError = NO_ERROR;
                        _videoScale = value;
                    }
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
        }

        #endregion

        #region Auto File Extension / Auto Metadata / Info

        /// <summary>
        /// Gets or sets a value that specifies whether the recorder is allowd to change
        /// <br/>the file extension of the specified file name for a recording (default: true).
        /// </summary>
        public bool AutoFileExtension
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _setFileExtension;
            }
            set
            {
                _setFileExtension = value;
                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the recorder adds metadata items "Title" (file name),
        /// <br/>"Album Artist" (Player.Name) and "Year" (current year) to new recordings (default: true).
        /// </summary>
        public bool AutoMetadata
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _setMetadata;
            }
            set
            {
                _setMetadata = value;
                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Gets information about the current or most recent recording of the recorder.
        /// <br/>The information may differ from the settings originally specified for the recording.
        /// </summary>
        public RecordingInfo Info
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _info;
            }
        }

        #endregion

        #region Recorder Start / Stop / Recording

        /// <summary>
        /// Starts recording the devices (webcam and/or audio input) played by the player to a file,
        /// <br/>named with the date and time, in the system's documents folder (PVS Recordings).
        /// </summary>
        public int Start()
        {
            return Start(CreateFileName());
        }

        /// <summary>
        /// Starts recording the devices (webcam and/or audio input) played by the player to the specified file.
        /// <br/>If the file already exists, it is overwritten.
        /// </summary>
        /// <param name="fileName">The path and file name of the recording.
        /// <br/>The file name extension may be changed by the recorder.</param>
        public int Start(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                _base._lastError = HResult.MF_E_INVALIDREQUEST;
                if (!_base._recording)
                {
                    if (_base._webcamMode)
                    {
                        VideoTrack[] tracks = _base.AV_GetVideoTracks();
                        _base._lastError = StartRecorder(fileName, (int)(tracks[0]._width * _videoScale), (int)(tracks[0]._height * _videoScale), _videoFrameRate == -1 ? tracks[0]._frameRate : _videoFrameRate);
                    }
                    else if (_base._micMode)
                    {
                        _base._lastError = StartRecorder(fileName, 0, 0, 0);
                    }
                }
            }
            else _base._lastError = HResult.ERROR_INVALID_NAME;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Stops recording the devices (webcam and/or audio input) played by the player.
        /// </summary>
        public int Stop()
        {
            StopRecorder();

            _base._lastError = NO_ERROR;
            return NO_ERROR;
        }

        /// <summary>
        /// Gets a value indicating whether the recorder is recording.
        /// </summary>
        public bool Recording
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._recording;
            }
        }

        #endregion

        #region StartRecorder / StopRecorder

        // creates and starts the recorder
        private HResult StartRecorder(string fileName, int width, int height, float frameRate)
        {
            HResult         result          = NO_ERROR;

            IMFMediaSource  mf_AudioSource  = null;
            IMFMediaType    mf_AudioType    = null;

            IMFMediaSource  mf_VideoSource  = null;
            IMFMediaType    mf_VideoType    = null;

            IMFAttributes   attributes      = null;

            IMFCaptureSink  sink            = null;

            // create capture engine factory
            IMFCaptureEngineClassFactory factory = (IMFCaptureEngineClassFactory)new MFCaptureEngineClassFactory();
            if (factory != null)
            {
                // create capture engine
                result = factory.CreateInstance(MFAttributesClsid.CLSID_MFCaptureEngine, typeof(IMFCaptureEngine).GUID, out object engine);
                if (result == NO_ERROR)
                {
                    _recorder           = (IMFCaptureEngine)engine;
                    ErrorFromCallback   = NO_ERROR;
                    _hasAudio           = false;
                    _hasVideo           = false;

                    // create sources
                    if (_base._webcamMode && !_audioOnly) //!(_audioOnly && _base._webcamAggregated))
                    {
                        _hasVideo = true;

                        // create video source
                        MFExtern.MFCreateAttributes(out IMFAttributes sourceAttr, 2);
                        sourceAttr.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
                        sourceAttr.SetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, _base._webcamDevice._id);

                        result = MFExtern.MFCreateDeviceSource(sourceAttr, out mf_VideoSource);
                        if (result == HResult.MF_E_ATTRIBUTENOTFOUND || result == HResult.ERROR_PATH_NOT_FOUND) result = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
                        Marshal.ReleaseComObject(sourceAttr);

                        if (result == HResult.MF_E_VIDEO_RECORDING_DEVICE_PREEMPTED) result = NO_ERROR;

                        if (result == NO_ERROR)
                        {
                            if (!_base._webcamAggregated || _videoOnly)
                            {
                                MFExtern.MFCreateAttributes(out attributes, 1);
                                attributes.SetUINT32(MFAttributesClsid.MF_CAPTURE_ENGINE_USE_VIDEO_DEVICE_ONLY, 1);
                            }
                            else
                            {
                                _hasAudio = true;

                                // create audio source
                                MFExtern.MFCreateAttributes(out sourceAttr, 2);
                                sourceAttr.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_GUID);
                                sourceAttr.SetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ENDPOINT_ID, _base._micDevice._id);
                                result = MFExtern.MFCreateDeviceSource(sourceAttr, out mf_AudioSource);
                                if (result == HResult.MF_E_ATTRIBUTENOTFOUND || result == HResult.ERROR_PATH_NOT_FOUND) result = HResult.MF_E_NO_AUDIO_RECORDING_DEVICE;
                                Marshal.ReleaseComObject(sourceAttr);

                                if (result == HResult.MF_E_VIDEO_RECORDING_DEVICE_PREEMPTED) result = NO_ERROR;
                            }
                        }
                    }
                    else //_micMode or audio only
                    {
                        if (_base._micDevice != null)
                        {
                            _hasAudio = true;

                            // create audio source
                            MFExtern.MFCreateAttributes(out attributes, 1);
                            attributes.SetUINT32(MFAttributesClsid.MF_CAPTURE_ENGINE_USE_AUDIO_DEVICE_ONLY, 1);

                            MFExtern.MFCreateAttributes(out IMFAttributes sourceAttr, 2);
                            sourceAttr.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_GUID);
                            sourceAttr.SetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ENDPOINT_ID, _base._micDevice._id);
                            result = MFExtern.MFCreateDeviceSource(sourceAttr, out mf_AudioSource);
                            if (result == HResult.MF_E_ATTRIBUTENOTFOUND || result == HResult.ERROR_PATH_NOT_FOUND) result = HResult.MF_E_NO_AUDIO_RECORDING_DEVICE;
                            Marshal.ReleaseComObject(sourceAttr);

                            if (result == HResult.MF_E_VIDEO_RECORDING_DEVICE_PREEMPTED) result = NO_ERROR;
                        }
                        //else result = HResult.MF_E_AUDIO_RECORDING_DEVICE_INVALIDATED;
                        else result = HResult.MF_E_NO_AUDIO_RECORDING_DEVICE;
                    }

                    if (result == NO_ERROR)
                    {
                        _callBack = new MF_RecorderCallBack(this);

                        AwaitCallBack = true;
                        result = _recorder.Initialize(_callBack, attributes, mf_AudioSource, mf_VideoSource);
                        _base.WaitForEvent.WaitOne(Player.TIMEOUT_30_SECONDS);

                        if (result == NO_ERROR) result = ErrorFromCallback;
                        if (result == HResult.MF_E_VIDEO_RECORDING_DEVICE_PREEMPTED) result = NO_ERROR;

                        if (result == NO_ERROR)
                        {
                            // get sink for recording audio and video
                            result = _recorder.GetSink(MF_CAPTURE_ENGINE_SINK_TYPE.Record, out sink);
                            if (result == HResult.MF_E_VIDEO_RECORDING_DEVICE_PREEMPTED) result = NO_ERROR;

                            if (result == NO_ERROR)
                            {
                                if (_hasVideo)
                                {
                                    mf_VideoType = CreateVideoMediaType(width, height, frameRate);
                                    result = ((IMFCaptureRecordSink)sink).AddStream(0, mf_VideoType, null, out _videoIndex);
                                    if (result == HResult.MF_E_VIDEO_RECORDING_DEVICE_PREEMPTED) result = NO_ERROR;

                                    if (_rotation != VideoRotation.None) ((IMFCaptureRecordSink)sink).SetRotation(_videoIndex, (int)_rotation);
                                }

                                if (result == NO_ERROR)
                                {
                                    if (_hasAudio)
                                    {
                                        //AudioFormat oldFormat = _audioFormat;
                                        //bool changedFormat = false;
                                        if (_hasVideo && _audioFormat == AudioFormat.WMA9 && _videoFormat != VideoFormat.WMV9)
                                        {
                                            _audioFormat = AudioFormat.AAC;
                                            //changedFormat = true;
                                        }

                                        AudioTrack[] tracks = _base.AV_GetAudioTracks();
                                        // TODO - change 1 channel (mono) to 2 (stereo) channels input and vice-versa?
                                        if (_audioStereo && tracks[0]._channelCount == 1) mf_AudioType = CreateAudioMediaType(tracks[0]._sampleRate, 2, (int)_audioBitRate);
                                        else if (_audioMono && tracks[0]._channelCount == 2) mf_AudioType = CreateAudioMediaType(tracks[0]._sampleRate, 1, (int)_audioBitRate);
                                        else mf_AudioType = CreateAudioMediaType(tracks[0]._sampleRate, tracks[0]._channelCount, (int)_audioBitRate);

                                        if (mf_AudioType == null) result = HResult.MF_E_NOT_AVAILABLE;
                                        else
                                        {
                                            result = ((IMFCaptureRecordSink)sink).AddStream(_hasVideo ? 2 : 0, mf_AudioType, null, out _audioIndex);
                                            if (result == HResult.MF_E_VIDEO_RECORDING_DEVICE_PREEMPTED) result = NO_ERROR;
                                        }
                                        //if (changedFormat) _audioFormat = oldFormat;
                                    }

                                    if (result == NO_ERROR)
                                    {
                                        if (_setFileExtension)
                                        {
                                            if (_hasVideo)
                                            {
                                                switch (_videoFormat)
                                                {
                                                    case VideoFormat.WMV9:
                                                        fileName = Path.ChangeExtension(fileName, Player.WMV_FILE_EXTENSION);
                                                        break;

                                                    default: // VideoType.H264, VideoType.H265
                                                        fileName = Path.ChangeExtension(fileName, Player.MP4_FILE_EXTENSION);
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                switch (_audioFormat)
                                                {
                                                    case AudioFormat.MP3:
                                                        fileName = Path.ChangeExtension(fileName, Player.MP3_FILE_EXTENSION);
                                                        break;

                                                    case AudioFormat.WMA9:
                                                        fileName = Path.ChangeExtension(fileName, Player.WMA_FILE_EXTENSION);
                                                        break;

                                                    default: // AudioFormat.AAC:
                                                        fileName = Path.ChangeExtension(fileName, Player.AAC_FILE_EXTENSION);
                                                        break;
                                                }
                                            }
                                        }

                                        // set output file
                                        result = ((IMFCaptureRecordSink)sink).SetOutputFileName(fileName);
                                        if (result == HResult.MF_E_VIDEO_RECORDING_DEVICE_PREEMPTED) result = NO_ERROR;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Marshal.ReleaseComObject(_recorder);
                        _recorder = null;
                    }
                }
                else result = HResult.MF_E_NOT_AVAILABLE;

                if (factory != null)        Marshal.ReleaseComObject(factory);

                if (sink != null)           Marshal.ReleaseComObject(sink);
                if (mf_AudioType != null)   Marshal.ReleaseComObject(mf_AudioType);
                if (mf_VideoType != null)   Marshal.ReleaseComObject(mf_VideoType);

                if (attributes != null)     Marshal.ReleaseComObject(attributes);

                if (mf_AudioSource != null) Marshal.ReleaseComObject(mf_AudioSource);
                if (mf_VideoSource != null) Marshal.ReleaseComObject(mf_VideoSource);

                if (result != NO_ERROR) StopRecorder();
                else
                {
                    AwaitCallBack = true;
                    result = _recorder.StartRecord();
                    _base.WaitForEvent.WaitOne(Player.TIMEOUT_30_SECONDS);

                    if (result == NO_ERROR) result = ErrorFromCallback;
                    if (result == HResult.MF_E_VIDEO_RECORDING_DEVICE_PREEMPTED) result = NO_ERROR;

                    if (result == NO_ERROR)
                    {
                        _base._deviceStart  = _base.PositionX;
                        _fileName           = fileName;
                        _base._recording    = true;

                        SetRecordingInfo();

                        _base._mediaDeviceRecorderStarted?.Invoke(_base, EventArgs.Empty);
                        result = NO_ERROR;
                    }
                    else StopRecorder();
                }
            }
            if (result == HResult.MF_E_VIDEO_RECORDING_DEVICE_PREEMPTED) result = NO_ERROR;

            _base._lastError = result;
            return result;
        }

        // stops and removes the recorder
        internal void StopRecorder()
        {
            if (_recorder != null)
            {
                if (_base._recording)
                {
                    if (!_resetAfterStop)
                    {
                        _base.AV_UpdateTopology();
                        if (_base._hasOverlay) _base.AV_ShowOverlay();
                    }

                    AwaitCallBack = true;
                    _recorder.StopRecord(true, false);
                    _base.WaitForEvent.WaitOne(Player.TIMEOUT_10_SECONDS);
                    _base._recording = false;

                    if(_setMetadata) AddMetadata();

                    _base._mediaDeviceRecorderStopped?.Invoke(this, EventArgs.Empty);

                    _base._deviceStart = _base.PositionX;
                }
                Marshal.ReleaseComObject(_recorder);
                _recorder = null;

                _fileName = string.Empty;
                _hasAudio = false;
                _hasVideo = false;

                _callBack.Dispose();
                _callBack = null;
            }
        }

        #endregion

        #region Create File Name / Create Media Types / Add Metadata / Set Recording Info

        private string CreateFileName()
        {
            string fileName = null;
            try
            {
                string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _folderName);
                string path = Path.Combine(folder, string.Format("Recordings {0:yyyy-MM-dd}", DateTime.Now));
                Directory.CreateDirectory(path);
                fileName = Path.Combine(path, string.Format("Recording {0:yyyy-MM-dd} at {0:HH-mm-ss}.mp4", DateTime.Now));
            }
            catch { /* ignored */ }
            return fileName;
        }

        private IMFMediaType CreateVideoMediaType(int width, int height, float frameRate)
        {
            MFExtern.MFCreateMediaType(out IMFMediaType mediaType);
            mediaType.SetGUID(MFAttributesClsid.MF_MT_MAJOR_TYPE, MFMediaType.Video);

            if (_videoFormat == VideoFormat.H264) mediaType.SetGUID(MFAttributesClsid.MF_MT_SUBTYPE, MFMediaType.H264);
            else if (_videoFormat == VideoFormat.H265) mediaType.SetGUID(MFAttributesClsid.MF_MT_SUBTYPE, MFMediaType.HEVC);
            else mediaType.SetGUID(MFAttributesClsid.MF_MT_SUBTYPE, MFMediaType.WMV3);

            mediaType.SetUINT32(MFAttributesClsid.MF_MT_AVG_BITRATE, _videoBitRate * 1000);
            mediaType.SetUINT32(MFAttributesClsid.MF_MT_INTERLACE_MODE, 2); // 2 = Progressive
            MFExtern.MFSetAttributeSize(mediaType, MFAttributesClsid.MF_MT_FRAME_SIZE, width, height);
            MFExtern.MFSetAttributeRatio(mediaType, MFAttributesClsid.MF_MT_FRAME_RATE, (int)(frameRate * 1000000), 1000000);
            MFExtern.MFSetAttributeRatio(mediaType, MFAttributesClsid.MF_MT_PIXEL_ASPECT_RATIO, 1, 1);

            return mediaType;
        }

        private IMFMediaType CreateAudioMediaType(int sampleRate, int numChannels, int bytesPerSecond)
        {
            IMFMediaType    mediaType = null;
            Guid            subType;

            switch (_audioFormat)
            {
                case AudioFormat.MP3:
                    subType = MFMediaType.MP3;
                    break;

                case AudioFormat.WMA9:
                    subType = MFMediaType.WMAudioV9;
                    break;

                default: // AudioFormat.AAC:
                    subType = MFMediaType.AAC;
                    break;
            }

            if (MFExtern.MFTranscodeGetAudioOutputAvailableTypes(subType, MFT_EnumFlag.All, null, out IMFCollection availableTypes) == NO_ERROR)
            {
                availableTypes.GetElementCount(out int count);
                if (count > 0)
                {
                    object          element;
                    IMFMediaType    type;
                    int             delta   = int.MaxValue;
                    int             index   = -1;

                    for (int i = 0; i < count; i++)
                    {
                        availableTypes.GetElement(i, out element);
                        type = (IMFMediaType)element;

                        type.GetUINT32(MFAttributesClsid.MF_MT_AUDIO_SAMPLES_PER_SECOND, out int typeSampleRate);
                        if (typeSampleRate == sampleRate)
                        {
                            type.GetUINT32(MFAttributesClsid.MF_MT_AUDIO_NUM_CHANNELS, out int typeNumChannels);
                            if (typeNumChannels == numChannels)
                            {
                                type.GetUINT32(MFAttributesClsid.MF_MT_AUDIO_AVG_BYTES_PER_SECOND, out int typeBytesPerSecond);
                                int diff = Math.Abs(typeBytesPerSecond - bytesPerSecond);
                                if (diff < delta)
                                {
                                    index = i;
                                    delta = diff;
                                    if (diff == 0) break;
                                }
                            }
                        }
                        Marshal.ReleaseComObject(element);
                    }
                    availableTypes.GetElement(index == -1 ? 0 : index, out element);
                    mediaType = (IMFMediaType)element;
                }
                Marshal.ReleaseComObject(availableTypes);
            }
            return mediaType;
        }

        private void AddMetadata()
        {
            Guid iid                = typeof(IPropertyStore).GUID;
            IPropertyStore store    = null;

            if (!string.IsNullOrWhiteSpace(_fileName))
            {
                try
                {
                    if (MFExtern.SHGetPropertyStoreFromParsingName(_info._fileName, IntPtr.Zero, GETPROPERTYSTOREFLAGS.GPS_READWRITE, ref iid, out store) == NO_ERROR)
                    {
                        PropVariant prop = new PropVariant(string.Format(Path.GetFileNameWithoutExtension(_fileName)));
                        store.SetValue(PropertyKeys.PKEY_Title, prop);
                        prop.Dispose();

                        if (_info._hasVideo)
                        {
                            prop = new PropVariant(_info._videoDeviceName);
                            store.SetValue(PropertyKeys.PKEY_Media_SubTitle, prop);
                            prop.Dispose();
                        }
                        else if (_info._hasAudio)
                        {
                            prop = new PropVariant(_info._audioDeviceName);
                            store.SetValue(PropertyKeys.PKEY_Media_SubTitle, prop);
                            prop.Dispose();
                        }

                        prop = new PropVariant(_info._recorderName);
                        store.SetValue(PropertyKeys.PKEY_Music_Artist, prop);
                        prop.Dispose();

                        prop = new PropVariant("");
                        store.SetValue(PropertyKeys.PKEY_Comment, prop);
                        prop.Dispose();

                        prop = new PropVariant(DateTime.Now.Year);
                        store.SetValue(PropertyKeys.PKEY_Media_Year, prop);
                        prop.Dispose();

                        store.Commit();
                    }
                }
                catch {  /* ignored */ }
                if (store != null) Marshal.ReleaseComObject(store);
            }
        }

        private void SetRecordingInfo()
        {
            if (_base._recording)
            {
                if (_recorder.GetSink(MF_CAPTURE_ENGINE_SINK_TYPE.Record, out IMFCaptureSink sink) == NO_ERROR)
                {
                    _info._audio    = null;
                    _info._video    = null;

                    _info = new RecordingInfo
                    {
                        _recorderName   = _base.Name,
                        _fileName       = _fileName,

                        _hasAudio       = _hasAudio,
                        _audio          = new AudioTrack(),

                        _hasVideo       = _hasVideo,
                        _video          = new VideoTrack()
                    };

                    if (_hasVideo)
                    {
                        _info._videoDeviceName = _base._webcamDevice._name;
                        _info._video._name = Player.VIDEO_TRACK_NAME + "1"; // for now, in future set by user

                        sink.GetOutputMediaType(_videoIndex, out IMFMediaType videoType);
                        videoType.GetGUID(MFAttributesClsid.MF_MT_SUBTYPE, out _info._video._mediaType);
                        MFExtern.MFGetAttributeRatio(videoType, MFAttributesClsid.MF_MT_FRAME_RATE, out int num, out int denum);
                        if (denum > 0) _info._video._frameRate = (float)(uint)num / denum;
                        MFExtern.MFGetAttributeRatio(videoType, MFAttributesClsid.MF_MT_FRAME_SIZE, out _info._video._width, out _info._video._height);
                        Marshal.ReleaseComObject(videoType);
                    }

                    if (_hasAudio)
                    {
                        _info._audioDeviceName = _base._micDevice.ToString();
                        _info._audio._name = Player.AUDIO_TRACK_NAME + "1"; // for now, in future set by user

                        sink.GetOutputMediaType(_audioIndex, out IMFMediaType audioType);
                        audioType.GetGUID(MFAttributesClsid.MF_MT_SUBTYPE, out _info._audio._mediaType);
                        audioType.GetUINT32(MFAttributesClsid.MF_MT_AUDIO_NUM_CHANNELS, out _info._audio._channelCount);
                        audioType.GetUINT32(MFAttributesClsid.MF_MT_AUDIO_SAMPLES_PER_SECOND, out _info._audio._sampleRate);
                        audioType.GetUINT32(MFAttributesClsid.MF_MT_AUDIO_BITS_PER_SAMPLE, out _info._audio._bitDepth);
                        audioType.GetUINT32(MFAttributesClsid.MF_MT_AUDIO_AVG_BYTES_PER_SECOND, out int bitRate);
                        _info._audio._bitRate = (int)((bitRate * 0.008) + 0.5);
                        Marshal.ReleaseComObject(audioType);
                    }

                    Marshal.ReleaseComObject(sink);
                }
            }
        }

        #endregion

    }
}