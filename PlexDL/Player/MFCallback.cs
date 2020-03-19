using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlexDL.Player
{
    internal sealed class MFCallback : IMFAsyncCallback
    {
        private Player _basePlayer;

        private delegate void EndOfMediaDelegate();

        private EndOfMediaDelegate CallEndOfMedia;

        public MFCallback(Player player)
        {
            _basePlayer = player;
            CallEndOfMedia = new EndOfMediaDelegate(_basePlayer.AV_EndOfMedia);
        }

        public void Dispose()
        {
            _basePlayer = null;
            CallEndOfMedia = null;
        }

        public HResult GetParameters(out MFASync pdwFlags, out MFAsyncCallbackQueue pdwQueue)
        {
            pdwFlags = MFASync.FastIOProcessingCallback;
            pdwQueue = MFAsyncCallbackQueue.Standard;
            return HResult.S_OK;
        }

        public HResult Invoke(IMFAsyncResult result)
        {
            IMFMediaEvent mediaEvent = null;
            MediaEventType mediaType = MediaEventType.MEUnknown;
            HResult hrStatus;

            try
            {
                _basePlayer.mf_MediaSession.EndGetEvent(result, out mediaEvent);
                mediaEvent.GetType(out mediaType);
                mediaEvent.GetStatus(out hrStatus);

                if (!_basePlayer._fileMode && mediaType == MediaEventType.MEVideoCaptureDeviceRemoved)
                {
                    _basePlayer._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
                    hrStatus = Player.NO_ERROR;
                    mediaType = MediaEventType.MEEndOfPresentation;
                }

                if (hrStatus >= 0)
                {
                    if (mediaType == MediaEventType.MEEndOfPresentation)
                    {
                        Control control = _basePlayer._display;
                        if (control == null)
                        {
                            FormCollection forms = Application.OpenForms;
                            if (forms != null && forms.Count > 0) control = forms[0];
                        }

                        if (control != null)
                        {
                            //control.BeginInvoke(new MethodInvoker(delegate { _basePlayer.AV_EndOfMedia(); }));
                            control.BeginInvoke(CallEndOfMedia);
                        }
                        else
                        {
                            _basePlayer.AV_EndOfMedia();
                        }
                    }
                }
                else _basePlayer._lastError = hrStatus;
            }
            finally
            {
                if (_basePlayer.mf_AwaitCallback)
                {
                    _basePlayer.mf_AwaitCallback = false;
                    _basePlayer.WaitForEvent.Set();
                }

                if (mediaType != MediaEventType.MESessionClosed) _basePlayer.mf_MediaSession.BeginGetEvent(this, null);
                if (mediaEvent != null) Marshal.ReleaseComObject(mediaEvent);
            }

            return 0;
        }
    }
}