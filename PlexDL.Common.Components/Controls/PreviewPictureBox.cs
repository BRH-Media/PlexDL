using PlexDL.Common.Components.Forms;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.Common.Components.Controls
{
    public class PreviewPictureBox : PictureBox
    {
        [Category("PlexDL")]
        [Description("Whether or not the PictureBox displays an enlarged preview window on double-click")]
        public bool PreviewWindowEnabled { get; set; } = true;

        public PreviewPictureBox()
        {
            try
            {
                //setup default image
                BackgroundImageLayout = ImageLayout.Zoom;
                BackgroundImage = ResourceProvider.Properties.Resources.unavailable;

                //apply preview window handler
                DoubleClick += PreviewHandler;
            }
            catch
            {
                //nothing
            }
        }

        private void PreviewHandler(object sender, EventArgs e)
        {
            try
            {
                //null validation
                if (BackgroundImage != null && PreviewWindowEnabled)

                    //show preview
                    ImagePreviewer.DisplayPreview(BackgroundImage);
            }
            catch
            {
                //nothing
            }
        }
    }
}