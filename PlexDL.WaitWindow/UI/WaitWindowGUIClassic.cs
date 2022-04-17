using System;
using System.Windows.Forms;

// ReSharper disable InconsistentNaming

namespace PlexDL.WaitWindow.UI
{
    /// <summary>
    ///     The dialogue displayed by a WaitWindow instance.
    /// </summary>
    public partial class WaitWindowGUIClassic : WaitWindowGUI
    {
        // Counts the amount of dots appended for the animation (max 3)
        private int _dotCount;

        public WaitWindowGUIClassic()
        {
            // In order for the designer to work, this needs to be here unfortunately.
        }
        public WaitWindowGUIClassic(WaitWindow parent) : base(parent)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
        }

        public new void SetMessage(string message)
        {
            // Set GUI label working text
            MessageLabel.Text = message;

            // Set internal working text variable
            _labelText = message;
        }

        private void WaitWindowGUI_Load(object sender, EventArgs e)
        {
            // Dot counter is reset
            _dotCount = 0;

            // Dot animation timer is started
            tmrDots.Start();
        }

        private void WaitWindowGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dot animation timer is stopped
            tmrDots.Stop();
        }

        private void TmrDots_Tick(object sender, EventArgs e)
        {
            // Check dot count (max 3)
            if (_dotCount < 3)
            {
                // Add new dot to counter
                _dotCount++;

                // Add new dot to working text
                MessageLabel.Text += '.';
            }
            else
            {
                // Reset dot counter
                _dotCount = 0;

                // Remove all dots from working text
                MessageLabel.Text = _labelText;
            }
        }
    }
}