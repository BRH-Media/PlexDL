using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the System Panels methods of the PlexDL.Player.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class SystemPanels : HideObjectMembers
    {
        #region Fields (SystemPanels Class)

        private Player _base;

        #endregion Fields (SystemPanels Class)

        internal SystemPanels(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Opens the System Audio Mixer Control Panel.
        /// </summary>
        public bool ShowAudioMixer()
        {
            return ShowAudioMixer(null);
        }

        /// <summary>
        /// Opens the System Audio Mixer Control Panel.
        /// </summary>
        /// <param name="centerForm">The control panel is centered on top of the specified form.</param>
        public bool ShowAudioMixer(Form centerForm)
        {
            _base._lastError = Player.NO_ERROR;
            return SafeNativeMethods.CenterSystemDialog("SndVol.exe", "", centerForm) || SafeNativeMethods.CenterSystemDialog("SndVol32.exe", "", centerForm);
        }

        /// <summary>
        /// Opens the System Sound Control Panel.
        /// </summary>
        public bool ShowAudioDevices()
        {
            return ShowAudioDevices(null);
        }

        /// <summary>
        /// Opens the System Sound Control Panel.
        /// </summary>
        /// <param name="centerForm">The control panel is centered on top of the specified form.</param>
        public bool ShowAudioDevices(Form centerForm)
        {
            _base._lastError = Player.NO_ERROR;
            return SafeNativeMethods.CenterSystemDialog("control", "mmsys.cpl,,0", centerForm);
        }

        /// <summary>
        /// Opens the System Sound Control Panel.
        /// </summary>
        public bool ShowAudioInputDevices()
        {
            return ShowAudioInputDevices(null);
        }

        /// <summary>
        /// Opens the System Sound Control Panel.
        /// </summary>
        /// <param name="centerForm">The control panel is centered on top of the specified form.</param>
        public bool ShowAudioInputDevices(Form centerForm)
        {
            _base._lastError = Player.NO_ERROR;
            return SafeNativeMethods.CenterSystemDialog("control", "mmsys.cpl,,1", centerForm);
        }

        /// <summary>
        /// Opens the System Display Control Panel.
        /// </summary>
        public bool ShowDisplaySettings()
        {
            return ShowDisplaySettings(null);
        }

        /// <summary>
        /// Opens the System Display Control Panel.
        /// </summary>
        /// <param name="centerForm">The control panel is centered on top of the specified form.</param>
        public bool ShowDisplaySettings(Form centerForm)
        {
            _base._lastError = Player.NO_ERROR;
            return SafeNativeMethods.CenterSystemDialog("control", "desk.cpl", centerForm);
        }
    }
}