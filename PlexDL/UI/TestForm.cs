using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            CenterInfoPanel();
        }

        private void CenterInfoPanel()
        {
            // Half the Testing Area either way to get the absolute middle-point.
            var middleHorizontalX = pnlTestingArea.Width / 2;
            var middleVerticalY = pnlTestingArea.Height / 2;

            // Half the panel to be centered either way to get the middle-point.
            var middlePanelX = pnlNothingInteresting.Width / 2;
            var middlePanelY = pnlNothingInteresting.Height / 2;

            // Positioning directly at middleHorizontalX and middleHorizontalY would offset
            // the object, due to how C# origins work (0,0 is top-right).
            // To correct this, we can offset it by half its Height and Width to effectively
            // make the origin in the center, rather than the top-right corner.
            var x = middleHorizontalX - middlePanelX;
            var y = middleVerticalY - middlePanelY;

            // Construct the new point.
            var newLocation = new Point(x, y);

            // And finally, apply the re-centering to the object.
            pnlNothingInteresting.Location = newLocation;
        }
    }
}