using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlexDL.Player
{
    internal sealed class MFCallback : IMFAsyncCallback
    {
        private Player              _basePlayer;
        private delegate void       EndOfMediaDelegate();
        private EndOfMediaDelegate  CallEndOfMedia;

        public MFCallback(Player player)
        {
            _basePlayer     = player;
            CallEndOfMedia  = new EndOfMediaDelegate(_basePlayer.AV_EndOfMedia);
        }

        public void Dispose()
        {
            _basePlayer     = null;
            CallEndOfMedia  = null;
        }

        public HResult GetParameters(out MFASync pdwFlags, out MFAsyncCallbackQueue pdwQueue)
        {
            pdwFlags = MFASync.FastIOProcessingCallback;
            pdwQueue = MFAsyncCallbackQueue.Standard;
            return HResult.S_OK;
        }

        public HResult Invoke(IMFAsyncResult result)
        {
            IMFMediaEvent   mediaEvent      = null;
            MediaEventType  mediaEventType  = MediaEventType.MEUnknown;
            bool getNext = true;

            try
            {
                _basePlayer.mf_MediaSession.EndGetEvent(result, out mediaEvent);
                mediaEvent.GetType(out mediaEventType);
                mediaEvent.GetStatus(out HResult errorCode);

                if (_basePlayer._playing)
                {
                    if (mediaEventType == MediaEventType.MEError
                        || (_basePlayer._webcamMode && mediaEventType == MediaEventType.MEVideoCaptureDeviceRemoved)
                        || (_basePlayer._micMode && mediaEventType == MediaEventType.MECaptureAudioSessionDeviceRemoved))
                        //if (errorCode < 0)
                    {
                        _basePlayer._lastError = errorCode;
                        errorCode = Player.NO_ERROR;
                        getNext = false;
                    }

                    if (errorCode >= 0)
                    {
                        if (!getNext || mediaEventType == MediaEventType.MESessionEnded)
                        {
                            if (getNext)
                            {
                                _basePlayer._lastError = Player.NO_ERROR;
                                if (!_basePlayer._repeat) getNext = false;
                            }

                            Control control = _basePlayer._display;
                            if (control == null)
                            {
                                FormCollection forms = Application.OpenForms;
                                if (forms != null && forms.Count > 0) control = forms[0];
                            }
                            if (control != null) control.BeginInvoke(CallEndOfMedia);
                            else _basePlayer.AV_EndOfMedia();
                        }
                    }
                    else _basePlayer._lastError = errorCode;
                }
                else _basePlayer._lastError = errorCode;
            }
            finally
            {
                if (getNext && mediaEventType != MediaEventType.MESessionClosed) _basePlayer.mf_MediaSession.BeginGetEvent(this, null);
                if (mediaEvent != null) Marshal.ReleaseComObject(mediaEvent);

                if (_basePlayer.mf_AwaitCallback)
                {
                    _basePlayer.mf_AwaitCallback = false;
                    _basePlayer.WaitForEvent.Set();
                }
                _basePlayer.mf_AwaitDoEvents = false;
            }
            return 0;
        }
    }
}