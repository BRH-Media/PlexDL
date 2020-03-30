/****************************************************************

    PVS.MediaPlayer - Version 0.98.2
    February 2020, The Netherlands
    © Copyright 2020 PVS The Netherlands - licensed under The Code Project Open License (CPOL)

    PVS.MediaPlayer uses (part of) the Media Foundation .NET library by nowinskie and snarfle (https://sourceforge.net/projects/mfnet).
    Licensed under either Lesser General Public License v2.1 or BSD.  See license.txt or BSDL.txt for details (http://mfnet.sourceforge.net).

    ****************

    For use with Microsoft Windows 7 or higher, Microsoft .NET Framework version 2.0 or higher and WinForms (any CPU).
    Created with Microsoft Visual Studio.

    Article on CodeProject with information on the use of the PVS.MediaPlayer library:
   https://www.codeproject.com/Articles/109714/PVS-MediaPlayer-Audio-and-Video-Player-Library

    ****************

    The PVS.MediaPlayer library source code is divided into 8 files:

    1. Player.cs        - main source code
    2. SubClasses.cs    - various grouping and information classes
    3. Interop.cs       - unmanaged Win32 functions
    4. AudioDevices.cs  - audio devices and peak meters
    5. DisplayClones.cs - multiple video displays 
    6. CursorHide.cs    - hides the mouse cursor during inactivity
    7. Subtitles.cs     - subrip (.srt) subtitles
    8. Infolabel.cs     - custom ToolTip

    Required references:
    System
    System.Drawing
    System.Windows.Forms

    ****************

    This file: AudioDevices.cs

    Player Class
    Extension to file 'Player.cs'.

    Audio Output Peak Meter
    Audio Input Peak Meter
    Handle Changed System Audio Output Devices
    System Audio Notification Client EventHandler
    System Audio Notification Client
    Master Volume / ChannelCount

    ****************

    Many thanks to Microsoft (Windows, .NET Framework, Visual Studio and others), all the people
    writing about programming on the internet (a great source for ideas and solving problems),
    the websites publishing those or other writings about programming, the people responding to the
    PVS.MediaPlayer articles with comments and suggestions and, of course, the people at CodeProject.

    Special thanks to the creators of Media Foundation .NET for their great library.

    Special thanks to Sean Ewington and Deeksha Shenoy of CodeProject who also took care of publishing the many
    code updates and changes in the PVS.MediaPlayer articles in a friendly, fast, and highly competent manner.

    Peter Vegter
    February 2020, The Netherlands

    ****************************************************************/

#region Usings

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace PlexDL.Player
{

    public partial class Player
    {
        /*
            Audio Devices

            Handles system audio devices changes, master volume and channel info, and
            provides audio output peak level and audio input peak level values.
        */

        // ******************************** Audio Devices - Fields

        #region Audio Devices - Fields

        internal static Guid            IID_IAudioMeterInformation  = new Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064");
        internal static Guid            IID_IAudioEndpointVolume    = new Guid("5CDF2C82-841E-4546-9722-0CF74078229A");

        // audio output peak meter
        internal bool                   pm_HasPeakMeter;
        private IAudioMeterInformation  pm_PeakMeterInfo;
        private float[]                 pm_PeakMeterValues;
        internal float[]                pm_PeakMeterValuesStop;
        internal int                    pm_PeakMeterChannelCount;
        private float                   pm_PeakMeterMasterValue;
        private static object           pm_PeakMeterLock            = new object();

        // audio input peak meter
        internal bool                   pm_HasInputMeter;
        internal bool                   pm_InputMeterPending;
        private IAudioMeterInformation  pm_InputMeterInfo;
        private float[]                 pm_InputMeterValues;
        internal float[]                pm_InputMeterValuesStop;
        internal int                    pm_InputMeterChannelCount;
        private float                   pm_InputMeterMasterValue;

        private static SystemAudioDevicesEventArgs  pm_AudioDevicesEventArgs;
        private static AudioDevicesClient           pm_AudioDevicesCallback;

        private delegate void OnSystemDevicesChangedDelegate(SystemAudioDevicesEventArgs e);

        #endregion


        // ******************************** Audio Devices - Audio Output Peak Meter - Open / Close / GetValues

        #region  Audio Devices - Audio Output Peak Meter - Open / Close / GetValues

        internal bool PeakMeter_Open(AudioDevice device, bool change)
        {
            if (!pm_HasPeakMeter || change)
            {
                IMMDeviceEnumerator deviceEnumerator    = null;
                IMMDevice           levelDevice         = null;
                object              levelDeviceInfo;

                if (pm_HasPeakMeter) PeakMeter_Close();

                try
                {
                    deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
                    if (device == null) deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out levelDevice);
                    else deviceEnumerator.GetDevice(device._id, out levelDevice);

                    if (levelDevice != null)
                    {
                        levelDevice.Activate(ref IID_IAudioMeterInformation, 3, IntPtr.Zero, out levelDeviceInfo);
                        pm_PeakMeterInfo = (IAudioMeterInformation)levelDeviceInfo;

                        pm_PeakMeterInfo.GetMeteringChannelCount(out pm_PeakMeterChannelCount);
                        if (pm_PeakMeterChannelCount > MAX_AUDIO_CHANNELS) pm_PeakMeterChannelCount = MAX_AUDIO_CHANNELS;

                        if (pm_PeakMeterValues == null)
                        {
                            pm_PeakMeterValues      = new float[MAX_AUDIO_CHANNELS];
                            pm_PeakMeterValuesStop  = new float[MAX_AUDIO_CHANNELS];
                            for (int i = 0; i < MAX_AUDIO_CHANNELS; i++)
                            {
                                pm_PeakMeterValuesStop[i] = STOP_VALUE;
                            }
                        }
                        pm_HasPeakMeter = true;

                        StartSystemDevicesChangedHandlerCheck();
                    }
                }
                catch { /* ignore */
    }

                if (levelDevice != null)        Marshal.ReleaseComObject(levelDevice);
                if (deviceEnumerator != null)   Marshal.ReleaseComObject(deviceEnumerator);
            }
            return pm_HasPeakMeter;
        }

        internal void PeakMeter_Close()
        {
            if (pm_HasPeakMeter)
            {
                if (_playing && _mediaPeakLevelChanged != null)
                {
                    _outputLevelArgs._channelCount    = pm_PeakMeterChannelCount;
                    _outputLevelArgs._masterPeakValue = STOP_VALUE;
                    _outputLevelArgs._channelsValues  = pm_PeakMeterValuesStop;
                    _mediaPeakLevelChanged(this, _outputLevelArgs);
                }

                pm_PeakMeterChannelCount = 0;
                pm_PeakMeterMasterValue  = 0;
                pm_HasPeakMeter          = false;

                try
                {
                    Marshal.ReleaseComObject(pm_PeakMeterInfo);
                    pm_PeakMeterInfo = null;

                    StopSystemDevicesChangedHandlerCheck();
                }
                catch { /* ignore */ }
            }
        }

        // TODO - write directly to _peakLevelArgs?
        private void PeakMeter_GetValues()
        {
            if (pm_HasPeakMeter)
            {
                GCHandle values = GCHandle.Alloc(pm_PeakMeterValues, GCHandleType.Pinned);
                //pm_PeakMeterInfo.GetMeteringChannelCount(out pm_PeakMeterChannelCount);
                pm_PeakMeterInfo.GetChannelsPeakValues(pm_PeakMeterChannelCount, values.AddrOfPinnedObject());
                pm_PeakMeterInfo.GetPeakValue(out pm_PeakMeterMasterValue);
                values.Free();
            }
            else
            {
                for (int i = 0; i < MAX_AUDIO_CHANNELS; i++) { pm_PeakMeterValues[i] = STOP_VALUE; }
            }
        }

        #endregion


        // ******************************** Audio Devices - Audio Input Peak Meter - Open / Close / GetValues

        #region  Audio Devices - Audio Input Peak Meter - Open / Close / GetValues

        internal bool InputMeter_Open(AudioInputDevice device, bool change)
        {
            // tested for null at event subscription

            if (!pm_HasInputMeter || change)
            {
                IMMDeviceEnumerator deviceEnumerator = null;
                IMMDevice           levelDevice = null;
                object              levelDeviceInfo;

                if (pm_HasInputMeter) InputMeter_Close();

                try
                {
                    deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
                    deviceEnumerator.GetDevice(device._id, out levelDevice);

                    if (levelDevice != null)
                    {
                        levelDevice.Activate(ref IID_IAudioMeterInformation, 3, IntPtr.Zero, out levelDeviceInfo);
                        pm_InputMeterInfo = (IAudioMeterInformation)levelDeviceInfo;

                        pm_InputMeterInfo.GetMeteringChannelCount(out pm_InputMeterChannelCount);
                        if (pm_InputMeterChannelCount > MAX_AUDIO_CHANNELS) pm_InputMeterChannelCount = MAX_AUDIO_CHANNELS;

                        if (pm_InputMeterValues == null)
                        {
                            pm_InputMeterValues = new float[MAX_AUDIO_CHANNELS];
                            pm_InputMeterValuesStop = new float[MAX_AUDIO_CHANNELS];
                            for (int i = 0; i < MAX_AUDIO_CHANNELS; i++)
                            {
                                pm_InputMeterValuesStop[i] = STOP_VALUE;
                            }
                        }
                        pm_HasInputMeter = true;

                        //StartSystemDevicesChangedHandlerCheck();
                    }
                }
                catch
                { /* ignore */
                }

                if (levelDevice != null) Marshal.ReleaseComObject(levelDevice);
                if (deviceEnumerator != null) Marshal.ReleaseComObject(deviceEnumerator);
            }
            return pm_HasInputMeter;
        }

        internal void InputMeter_Close()
        {
            if (pm_HasInputMeter)
            {
                if (_playing && _mediaPeakLevelChanged != null)
                {
                    _inputLevelArgs._channelCount = pm_InputMeterChannelCount;
                    _inputLevelArgs._masterPeakValue = STOP_VALUE;
                    _inputLevelArgs._channelsValues = pm_InputMeterValuesStop;
                    _mediaInputLevelChanged(this, _inputLevelArgs);
                }

                pm_InputMeterChannelCount = 0;
                pm_InputMeterMasterValue = 0;
                pm_HasInputMeter = false;

                try
                {
                    Marshal.ReleaseComObject(pm_InputMeterInfo);
                    pm_InputMeterInfo = null;

                    //StopSystemDevicesChangedHandlerCheck();
                }
                catch { /* ignore */ }
            }
        }

        // TODO - write directly to _peakLevelArgs?
        private void InputMeter_GetValues()
        {
            if (pm_HasInputMeter)
            {
                GCHandle values = GCHandle.Alloc(pm_InputMeterValues, GCHandleType.Pinned);
                //pm_PeakMeterInfo.GetMeteringChannelCount(out pm_PeakMeterChannelCount);
                pm_InputMeterInfo.GetChannelsPeakValues(pm_InputMeterChannelCount, values.AddrOfPinnedObject());
                pm_InputMeterInfo.GetPeakValue(out pm_InputMeterMasterValue);
                values.Free();
            }
            else
            {
                for (int i = 0; i < MAX_AUDIO_CHANNELS; i++) { pm_InputMeterValues[i] = STOP_VALUE; }
            }
        }

        #endregion


        // ******************************** Audio Devices - Handle Changed System Audio Output Devices

        #region Audio Devices - Handle Changed Audio Output Devices

        // this is a sort of extension to the MediaAudioDeviceChanged event to also monitor system audio devices changes:
        // the MediaAudioDeviceChanged event is raised when the user changes the audio device of the player
        // or when the used audio device is changed due to changes in the system audio devices

        internal void StartSystemDevicesChangedHandlerCheck()
        {
            if (!_hasDeviceChangedHandler && (_audioDevice != null || _mediaAudioDeviceChanged != null || _masterSystemAudioDevicesChanged != null || pm_HasPeakMeter))
            {
                if (AudioDevicesClientOpen())
                {
                    _masterSystemAudioDevicesChanged += SystemDevicesChangedHandler;
                    _hasDeviceChangedHandler = true;
                }
            }
        }

        internal void StopSystemDevicesChangedHandlerCheck()
        {
            if (_hasDeviceChangedHandler && _audioDevice == null && _mediaAudioDeviceChanged == null && _masterSystemAudioDevicesChanged != null && !pm_HasPeakMeter)
            {
                _masterSystemAudioDevicesChanged -= SystemDevicesChangedHandler;
                AudioDevicesClientClose();
                _hasDeviceChangedHandler = false;
            }
        }

        private void SystemDevicesChangedHandler(object sender, SystemAudioDevicesEventArgs e)
        {
            if (e.IsInputDevice) return;

            IMMDeviceCollection deviceCollection;
            uint count;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.EnumAudioEndpoints(EDataFlow.eRender, (uint)DeviceState.Active, out deviceCollection);
            deviceCollection.GetCount(out count);
            Marshal.ReleaseComObject(deviceEnumerator);
            if (deviceCollection != null) Marshal.ReleaseComObject(deviceCollection);

            if (e._notification == SystemAudioDevicesNotification.Activated)
            {
                if (count == 1)
                {
                    if (_playing && !_paused)
                    {
                        // cannot resume playback
                        //if (pm_HasPeakMeter)
                        //{
                        //    PeakMeter_Open(_audioDevice, true);
                        //}
                        // maybe this
                        Control control = _display;
                        if (control == null)
                        {
                            FormCollection forms = Application.OpenForms;
                            if (forms != null && forms.Count > 0) control = forms[0];
                        }
                        if (control != null)
                        {
                            control.BeginInvoke(new MethodInvoker(delegate { AV_UpdateTopology(); }));
                        }
                    }
                    if (_mediaAudioDeviceChanged != null) _mediaAudioDeviceChanged(this, EventArgs.Empty);
                }
            }
            else if (_audioDevice == null)
            {
                if (pm_HasPeakMeter && e._notification == SystemAudioDevicesNotification.DefaultChanged)
                {
                    if (count != 0)
                    {
                        PeakMeter_Open(_audioDevice, true);
                    }
                    else pm_PeakMeterChannelCount = 0;
                    if (_mediaAudioDeviceChanged != null) _mediaAudioDeviceChanged(this, EventArgs.Empty);
                }
            }
            else
            {
                if (e._deviceId == _audioDevice.Id && (e._notification == SystemAudioDevicesNotification.Removed || e._notification == SystemAudioDevicesNotification.Disabled))
                {
                    if (count != 0)
                    {
                        _audioDevice = null;
                        if (pm_HasPeakMeter) PeakMeter_Open(_audioDevice, true);
                    }
                    else pm_PeakMeterChannelCount = 0;
                    if (_mediaAudioDeviceChanged != null) _mediaAudioDeviceChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion


        // ******************************** Audio Devices - System Audio Notification Client EventHandler

        #region Audio Devices - System Audio Notification Client EventHandler

        // handles the events from the System Audio Notification Client (one client for all Players) for this player only

        // gets the main ui thread for the events (so the end user doesn't have to use "InvokeRequired")
        // or should the "ISynchronizeInvoke" be used instead of the "control" method used here?
        internal void SystemAudioDevicesChanged(object sender, SystemAudioDevicesEventArgs e)
        {
            if (_mediaSystemAudioDevicesChanged != null)
            {
                Control control = _display;
                if (control == null)
                {
                    FormCollection forms = Application.OpenForms;
                    if (forms != null && forms.Count > 0) control = forms[0];
                }
                if (control != null)
                {
                    OnSystemDevicesChangedDelegate d = new OnSystemDevicesChangedDelegate(OnSystemAudioDevicesChanged);
                    control.BeginInvoke(d, new object[] { e });

                    // what's the best?
                    //control.Invoke(new Action<SystemAudioDevicesEventArgs>(RaiseDeviceChangedEvent), new object[] { e });
                    //control.Invoke((MethodInvoker)delegate { RaiseDeviceChangedEvent(e); });
                }
                else
                {
                    // no ui thread found, just fire away...
                    _mediaSystemAudioDevicesChanged(this, e);
                }
            }
        }

        // raise the SystemAudioDevicesChanged event on the player's main ui thread
        private void OnSystemAudioDevicesChanged(SystemAudioDevicesEventArgs e)
        {
            _mediaSystemAudioDevicesChanged(this, e);
        }

        #endregion


        // ******************************** Audio Devices - System Audio Notification Client

        #region Audio Devices - System Audio Notification Client

        // activate the System Audio Notification Client (1 client for all players)
        internal static bool AudioDevicesClientOpen()
        {
            bool result = true;
            if (pm_AudioDevicesCallback == null)
            {
                try
                {
                    IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)(new MMDeviceEnumerator());
                    pm_AudioDevicesCallback = new AudioDevicesClient();
                    deviceEnumerator.RegisterEndpointNotificationCallback(pm_AudioDevicesCallback);
                    pm_AudioDevicesEventArgs = new SystemAudioDevicesEventArgs();
                    if (deviceEnumerator != null) Marshal.ReleaseComObject(deviceEnumerator);
                }
                catch { result = false; }
            }
            return result;
        }

        // remove the System Audio Notification Client (1 client for all players)
        internal static void AudioDevicesClientClose()
        {
            if (pm_AudioDevicesCallback != null)
            {
                try
                {
                    IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)(new MMDeviceEnumerator());
                    deviceEnumerator.UnregisterEndpointNotificationCallback(pm_AudioDevicesCallback);
                    pm_AudioDevicesCallback = null;
                    pm_AudioDevicesEventArgs = null;
                    if (deviceEnumerator != null) { Marshal.ReleaseComObject(deviceEnumerator); deviceEnumerator = null; }
                }
                catch { /* ignore */ }
            }
        }

        // check if the changed device is an input device (otherwise it is assumed to be an output device)
        private static bool IsAudioInputDevice(string deviceId)
        {
            IMMDeviceCollection deviceCollection;
            IMMDevice device;
            string id;
            bool result = false;
            uint count;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.EnumAudioEndpoints(EDataFlow.eCapture, (uint)DeviceState.All, out deviceCollection);
            deviceCollection.GetCount(out count);
            Marshal.ReleaseComObject(deviceEnumerator);

            for (uint i = 0; i < count; i++)
            {
                deviceCollection.Item(i, out device);
                device.GetId(out id);
                Marshal.ReleaseComObject(device);
                if (deviceId == id)
                {
                    result = true;
                    break;
                }
            }
            if (deviceCollection != null) Marshal.ReleaseComObject(deviceCollection);
            return result;
        }

        // this class is called by the system when the audio devices have changed
        private sealed class AudioDevicesClient : IMMNotificationClient
        {
            public void OnDefaultDeviceChanged(EDataFlow dataFlow, ERole deviceRole, string defaultDeviceId)
            {
                if (deviceRole == ERole.eMultimedia && _masterSystemAudioDevicesChanged != null)
                {
                    pm_AudioDevicesEventArgs._inputDevice   = dataFlow == EDataFlow.eCapture;
                    pm_AudioDevicesEventArgs._deviceId      = defaultDeviceId;
                    pm_AudioDevicesEventArgs._notification  = SystemAudioDevicesNotification.DefaultChanged;
                    _masterSystemAudioDevicesChanged(null, pm_AudioDevicesEventArgs);
                }
            }

            public void OnDeviceAdded(string deviceId)
            {
                if (_masterSystemAudioDevicesChanged != null)
                {
                    pm_AudioDevicesEventArgs._inputDevice   = IsAudioInputDevice(deviceId);
                    pm_AudioDevicesEventArgs._deviceId      = deviceId;
                    pm_AudioDevicesEventArgs._notification  = SystemAudioDevicesNotification.Added;
                    _masterSystemAudioDevicesChanged(null, pm_AudioDevicesEventArgs);
                }
            }

            public void OnDeviceRemoved(string deviceId)
            {
                if (_masterSystemAudioDevicesChanged != null)
                {
                    pm_AudioDevicesEventArgs._inputDevice   = IsAudioInputDevice(deviceId);
                    pm_AudioDevicesEventArgs._deviceId      = deviceId;
                    pm_AudioDevicesEventArgs._notification  = SystemAudioDevicesNotification.Removed;
                    _masterSystemAudioDevicesChanged(null, pm_AudioDevicesEventArgs);
                }
            }

            public void OnDeviceStateChanged(string deviceId, DeviceState newState)
            {
                if (_masterSystemAudioDevicesChanged != null)
                {
                    pm_AudioDevicesEventArgs._inputDevice   = IsAudioInputDevice(deviceId);
                    pm_AudioDevicesEventArgs._deviceId      = deviceId;
                    if (newState == DeviceState.Active) pm_AudioDevicesEventArgs._notification = SystemAudioDevicesNotification.Activated;
                    else pm_AudioDevicesEventArgs._notification = SystemAudioDevicesNotification.Disabled;
                    _masterSystemAudioDevicesChanged(null, pm_AudioDevicesEventArgs);
                }
            }

            public void OnPropertyValueChanged(string deviceId, PropertyKey propertyKey)
            {
                // this fires many times - even if you click the sound icon in the system tray?
                // now only checks for a changed description (name) of a device
                if (_masterSystemAudioDevicesChanged != null && propertyKey.fmtid.Equals(PropertyKeys.PKEY_Device_Description.fmtid))
                {
                    pm_AudioDevicesEventArgs._inputDevice   = IsAudioInputDevice(deviceId);
                    pm_AudioDevicesEventArgs._deviceId      = deviceId;
                    pm_AudioDevicesEventArgs._notification  = SystemAudioDevicesNotification.DescriptionChanged;
                    _masterSystemAudioDevicesChanged(null, pm_AudioDevicesEventArgs);
                }
            }
        }

        #endregion


        // ******************************** Audio Devices - Master Volume / ChannelCount

        #region Audio Devices - Master Volume / ChannelCount

        internal static float AudioDevice_MasterVolume(AudioDevice device, float volume, bool set)
        {
            lock (pm_PeakMeterLock)
            {
                IMMDeviceEnumerator deviceEnumerator    = null;
                IMMDevice levelDevice                   = null;
                object levelDeviceInfo                  = null;
                float getVolume                         = 0;

                try
                {
                    deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
                    if (device == null) deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out levelDevice);
                    else deviceEnumerator.GetDevice(device._id, out levelDevice);

                    levelDevice.Activate(ref IID_IAudioEndpointVolume, 3, IntPtr.Zero, out levelDeviceInfo);
                    if (set)
                    {
                        // TODO set out of range?
                        if (volume <= 0)        volume = 0;
                        else if (volume > 1)    volume = 1;
                        ((IAudioEndpointVolume)levelDeviceInfo).SetMasterVolumeLevelScalar(volume, Guid.Empty);
                        getVolume = volume;
                    }
                    else
                    {
                        ((IAudioEndpointVolume)levelDeviceInfo).GetMasterVolumeLevelScalar(out getVolume);
                    }
                }
                catch { getVolume = -1; }

                if (levelDeviceInfo != null)    Marshal.ReleaseComObject(levelDeviceInfo);
                if (levelDevice != null)        Marshal.ReleaseComObject(levelDevice);
                if (deviceEnumerator != null)   Marshal.ReleaseComObject(deviceEnumerator);

                return getVolume;
            }
        }

        internal static int Device_GetChannelCount(DeviceInfo device)
        {
            lock (pm_PeakMeterLock)
            {
                IMMDeviceEnumerator deviceEnumerator = null;
                IMMDevice           levelDevice      = null;
                object              levelDeviceInfo  = null;
                uint                channels         = 0;

                try
                {
                    deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
                    if (device == null) deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out levelDevice);
                    else deviceEnumerator.GetDevice(device._id, out levelDevice);

                    levelDevice.Activate(ref IID_IAudioEndpointVolume, 3, IntPtr.Zero, out levelDeviceInfo);
                    ((IAudioEndpointVolume)levelDeviceInfo).GetChannelCount(out channels);
                }
                catch { /* ignore */ }

                if (levelDeviceInfo != null) Marshal.ReleaseComObject(levelDeviceInfo);
                if (levelDevice != null) Marshal.ReleaseComObject(levelDevice);
                if (deviceEnumerator != null) Marshal.ReleaseComObject(deviceEnumerator);

                return (int)channels;
            }
        }

        #endregion
    
    }
}
