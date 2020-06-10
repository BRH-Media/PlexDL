using System;
using System.Drawing;
using System.Windows.Forms;
using Application = GitHubUpdater.API.Application;

namespace GitHubUpdater
{
    public partial class Update : Form
    {
        public Application UpdateData { get; set; }

        public Update()
        {
            InitializeComponent();
        }

        private void Update_Load(object sender, EventArgs e)
        {
        }

        private void CenterTitle()
        {
            //calculate appropriate x-axis position
            var x = (Width / 2) - (lblUpdateTitle.Width / 2);

            //maintain existing y-axis position
            var y = lblUpdateTitle.Location.Y;

            //construct new location
            var newLocation = new Point(x, y);

            //apply new location to label
            lblUpdateTitle.Location = newLocation;
        }

        private void BtnMaybeLater_Click(object sender, EventArgs e)
        {
        }

        private void BtnDownloadUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}