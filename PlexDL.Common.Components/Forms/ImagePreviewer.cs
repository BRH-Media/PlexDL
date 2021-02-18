using System;
using System.Drawing;
using System.Windows.Forms;

#pragma warning disable 1591

namespace PlexDL.Common.Components.Forms
{
    public partial class ImagePreviewer : Form
    {
        public Image PreviewImage { get; set; }

        public ImagePreviewer()
            => InitializeComponent();

        public static void DisplayPreview(Image preview)
            => new ImagePreviewer { PreviewImage = preview }.ShowDialog();

        private void BtnOK_Click(object sender, EventArgs e)
            => Close();

        private void ImagePreviewer_Load(object sender, EventArgs e)
        {
            if (PreviewImage != null)
                picImagePreview.BackgroundImage = PreviewImage;
        }
    }
}