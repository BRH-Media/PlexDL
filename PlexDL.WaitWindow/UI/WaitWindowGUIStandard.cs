// ReSharper disable InconsistentNaming

namespace PlexDL.WaitWindow.UI
{
    public partial class WaitWindowGUIStandard : WaitWindowGUI
    {
        public WaitWindowGUIStandard()
        {
            // In order for the designer to work, this needs to be here unfortunately.
        }

        public WaitWindowGUIStandard(WaitWindow parent) : base(parent)
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
    }
}