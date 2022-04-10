using System;
using System.ComponentModel;
using System.Drawing;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Motion Detection methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class MotionDetection : HideObjectMembers
    {
        #region Fields (MotionDetection Class)

        private const int   NO_ERROR = 0;

        private Player      _base;

        #endregion

		
        #region Main

        internal MotionDetection(Player player)
        {
            _base = player;
        }

        #endregion

        #region Enabled / Active / Start / Stop

        /// <summary>
        /// Gets a value indicating whether video motion detection is enabled (default: false).
        /// <br/>Video motion detection is enabled by subscribing to the Player.Events.MediaVideoMotionDetected event.
        /// <br/>Video motion detection must be enabled before it can be started with the Player.MotionDetection.Start method.
        /// </summary>
        public bool Enabled
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasMotionDetection;
            }
        }

        /// <summary>
        /// Gets a value indicating whether video motion detection is enabled and active (default: false).
        /// <br/>Video motion detection is activated with the Player.MotionDetection.Start method.
        /// </summary>
        public bool Active
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._motionDetectionActive;
            }
        }

        /// <summary>
        /// Starts video motion detection.
        /// <br/>Video motion detection can only be started when video motion detection is enabled and video is being played by the player.
        /// <br/>Video motion detection is stopped when media has finished playing or with the Player.MotionDetection.Stop method.
        /// <br/>See also: Player.MotionDetection.Enabled.
        /// </summary>
        public int Start()
        {
            if (_base._hasMotionDetection)
            {
                if (!_base._motionDetectionActive) _base.AV_StartMotionTimer();
                _base._lastError = NO_ERROR;
            }
            else _base._lastError = HResult.MF_E_INVALIDREQUEST;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Stops video motion detection.
        /// <br/>See also: Player.MotionDetection.Start.
        /// </summary>
        public int Stop()
        {
            if (_base._hasMotionDetection)
            {
                if (_base._motionDetectionActive) _base.AV_StopMotionTimer(false);
                _base._lastError = NO_ERROR;
            }
            else _base._lastError = HResult.MF_E_INVALIDREQUEST;

            return (int)_base._lastError;
        }

        #endregion

        #region Interval / No Motion Notifications / Sensitivity / Sensitivity Table / Threshold

        /// <summary>
        /// Gets or sets the time in milliseconds between two successive video motion detection checks (default: 500).
        /// <br/>Values from 100 to 5000 milliseconds.
        /// </summary>
        public int Interval
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._motionTimerInterval;
            }
            set
            {
                if (value >= 100 && value <= 5000)
                {
                    if (value != _base._motionTimerInterval)
                    {
                        _base._motionTimerInterval = value;
                        if (_base._motionDetectionActive) _base._motionTimer.Interval = value;
                    }
                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether to report that a certain amount of time has passed without motion being detected (default: 0).
        /// <br/>The notifications are also made via the MediaVideoMotionDetected event but with the motion property (e.Motion) set to 0.
        /// <br/>The duration of the inactivity period is indicated in seconds, a value of 0 (zero) disables the notifications.
        /// </summary>
        public int NoMotionNotification
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._motionIdleTime / 1000;
            }
            set
            {
                if (value >= 0)
                {
                    _base._motionIdleTime = value * 1000;
                    _base._motionIdleCounter = 0;

                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates how much change in the light intensity of a pixel is ignored by video motion detection (default: 10).
        /// <br/>This setting can be used to prevent 'false' video motion detection notifications.
        /// <br/>Values from 0 (no change ignored) to 255 (all changes ignored).
        /// <br/>See also: Player.MotionDetection.Threshold.
        /// </summary>
        public int Sensitivity
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._motionSensitivity;
            }
            set
            {
                if (value >= 0 && value <= 255)
                {
                    _base._lastError = NO_ERROR;
                    _base._motionSensitivity = value;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
        }

        /// <summary>
        /// Gets or sets the motion sensitivity for each individual pixel (area) in a video image (default: null).
        /// <br/>A video image is divided into 32 x 32 (1024) areas, each of which is considered a single pixel.
        /// <br/>The sensitivity table is a byte array of 1024 bytes where each byte represents the motion sensitivity of a pixel.
        /// <br/>A null value disables the use of a sensitivity table, otherwise the Player.MotionDetection.Sensitivity setting is ignored.
        /// <br/>See also: Player.MotionDetection.Sensitivity.
        /// </summary>
        public byte[] SensitivityTable
        {
            get
            {
                _base._lastError = NO_ERROR;
                return (byte[])_base._motionSensitivityTable.Clone();
            }
            set
            {
                _base._lastError = NO_ERROR;

                if (value == null)
                {
                    while (_base._motionDetectionBusy) ;
                    _base._hasMotionSensitivityTable = false;
                    _base._motionSensitivityTable = null;
                }
                else if (value.Length == Player.MOTION_TABLE_SIZE)
                {
                    while (_base._motionDetectionBusy) ;
                    _base._motionSensitivityTable = (byte[])value.Clone();
                    _base._hasMotionSensitivityTable = true;
                }
                else _base._lastError = HResult.E_INVALIDARG;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates what percentage of the changed pixels are ignored by video motion detection (default: 3).
        /// <br/>This setting can be used to prevent 'false' video motion detection notifications.
        /// <br/>Values from 0 (no changed pixels are ignored) to 100 percent (all changed pixels are ignored). 
        /// <br/>See also: Player.MotionDetection.Sensitivity.
        /// </summary>
        public int Threshold
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._motionThreshold;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _base._lastError = NO_ERROR;
                    _base._motionThreshold = value;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
        }

        #endregion

        #region DetectionArea / BlockedArea / HasFixedImage / SetfixedImage

        /// <summary>
        /// Gets or sets a value that indicates the area of the video image to be checked with video motion detection (default: RectangleF.Empty).
        /// <br/>The value is a normalized rectangle that indicates the position and size of the area within the video image.
        /// <br/>All values (X, Y, Width (= Right) and Height (= Bottom)) between 0.0 and 1.0 (inclusive).
        /// <br/>Reset to full video motion detection with RectangleF.Empty or when media has finished playing.
        /// </summary>
        public RectangleF DetectionArea
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._motionArea;
            }
            set
            {
                if (value == RectangleF.Empty)
                {
                    _base._motionArea = value;
                    _base._hasMotionArea = false;
                }
                else
                {
                    if (_base._hasMotionDetection)
                    {
                        if (value.X >= 0f && value.X < value.Width && value.Y >= 0f && value.Y < value.Height && value.Width <= 1f && value.Height <= 1f)
                        {
                            if (_base._motionDetectionActive)
                            {
                                while (_base._motionDetectionBusy) ;
                            }
                            _base._motionTable = null;
                            _base._motionArea = value;
                            _base._hasMotionArea = true;
                            _base._lastError = NO_ERROR;
                        }
                        else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                    }
                    else _base._lastError = HResult.MF_E_INVALIDREQUEST;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the area of the video image to be ignored with video motion detection (default: RectangleF.Empty).
        /// <br/>The value is a normalized rectangle that indicates the position and size of the area within the video image (limited precision).
        /// <br/>All values (X, Y, Width (= Right) and Height (= Bottom)) between 0.0 and 1.0 (inclusive).
        /// <br/>Reset to full video motion detection with RectangleF.Empty or when media has finished playing.
        /// </summary>
        public RectangleF BlockedArea
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._motionBlocked;
            }
            set
            {
                if (value == RectangleF.Empty)
                {
                    _base._motionBlocked = value;
                    _base._hasMotionBlocked = false;
                }
                else
                {
                    if (_base._hasMotionDetection)
                    {
                        if (value.X >= 0f && value.X < value.Width && value.Y >= 0f && value.Y < value.Height && value.Width <= 1f && value.Height <= 1f)
                        {
                            if (_base._motionDetectionActive)
                            {
                                while (_base._motionDetectionBusy) ;
                            }
                            _base._motionTable = null;
                            _base._motionBlocked = value;
                            _base._hasMotionBlocked = true;
                            _base._lastError = NO_ERROR;
                        }
                        else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                    }
                    else _base._lastError = HResult.MF_E_INVALIDREQUEST;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether video motion detection uses a fixed image (default: false).
        /// <br/>See also: Player.MotionDetection.SetFixedImage.
        /// </summary>
        public bool HasFixedImage
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasMotionFixedImage;
            }
        }

        /// <summary>
        /// Sets or removes the specified image as a fixed image against which all subsequent video images are compared with video motion detection.
        /// <br/>Reset to regular video motion detection with null or when media has finished playing.
        /// <br/>See also: Player.MotionDetection.HasFixedImage and Player.Events.MediaVideoMatchDetected.
        /// </summary>
        /// <param name="image">The image to be set as a fixed image or null to stop using a fixed image.</param>
        public int SetFixedImage(Image image)
        {
            _base._lastError = NO_ERROR;

            if (image != null)
            {
                if (_base._hasMotionDetection)
                {
                    if (_base._motionDetectionActive)
                    {
                        while (_base._motionDetectionBusy) ;
                    }
                    _base.AV_SetMotionTable(image);
                    _base._hasMotionFixedImage = true;
                }
                else _base._lastError = HResult.MF_E_INVALIDREQUEST;
            }

            return (int)_base._lastError;
        }

        #endregion

    }
}