using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlexDL.Player
{
    internal sealed class MF_PlayerCallBack : IMFAsyncCallback
    {
        private Player              _base;
        private delegate void       EndOfMediaDelegate();
        private EndOfMediaDelegate  CallEndOfMedia;

        public MF_PlayerCallBack(Player player)
        {
            _base           = player;
            CallEndOfMedia  = new EndOfMediaDelegate(_base.AV_EndOfMedia);
        }

        public void Dispose()
        {
            _base           = null;
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
                _base.mf_MediaSession.EndGetEvent(result, out mediaEvent);
                mediaEvent.GetType(out mediaEventType);
                mediaEvent.GetStatus(out HResult errorCode);

                if (_base._playing)
                {
                    if (mediaEventType == MediaEventType.MEError
                        || (_base._webcamMode && mediaEventType == MediaEventType.MEVideoCaptureDeviceRemoved)
                        || (_base._micMode && mediaEventType == MediaEventType.MECaptureAudioSessionDeviceRemoved))
                    {
                        _base._lastError    = errorCode;
                        errorCode           = Player.NO_ERROR;
                        getNext             = false;
                    }

                    if (errorCode >= 0)
                    {
                        if (!getNext || mediaEventType == MediaEventType.MESessionEnded)
                        {
                            if (getNext)
                            {
                                _base._lastError = Player.NO_ERROR;
                                if (!_base._repeat && !_base._chapterMode && !_base._endPauseMode) getNext = false;
                            }

                            Control control = _base._display;
                            if (control == null)
                            {
                                FormCollection forms = Application.OpenForms;
                                if (forms != null && forms.Count > 0) control = forms[0];
                            }
                            if (control != null) control.BeginInvoke(CallEndOfMedia);
                            else _base.AV_EndOfMedia();
                        }
                    }
                    else _base._lastError = errorCode;
                }
                else _base._lastError = errorCode;
            }
            finally
            {
                if (getNext && mediaEventType != MediaEventType.MESessionClosed) _base.mf_MediaSession.BeginGetEvent(this, null);
                if (mediaEvent != null) Marshal.ReleaseComObject(mediaEvent);

                if (_base.mf_AwaitCallBack)
                {
                    _base.mf_AwaitCallBack = false;
                    _base.WaitForEvent.Set();
                }
                _base.mf_AwaitDoEvents = false;
            }
            return 0;
        }
    }
}