using PlexDL.Common.Structures.Plex;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.UI
{
    public static class UiUtils
    {
        public static void RunMetadataWindow(PlexObject metadata, bool appRun = false)
        {
            var form = new Metadata();
            if (metadata != null)
            {
                form.StreamingContent = metadata;

                if (appRun)
                    Application.Run(form);
                else
                    form.ShowDialog();
            }
            else
            {
                UIMessages.Error(@"Invalid PlexMovie Metadata File; the decoded data was null.",
                    @"Validation Error");
            }
        }

        public static void RunPlexDlHome(bool appRun = false)
        {
            var form = new Home();

            if (appRun)
                Application.Run(form);
            else
                form.ShowDialog();
        }

        public static void RunTestingWindow(bool appRun = false)
        {
            var form = new TestForm();

            if (appRun)
                Application.Run(form);
            else
                form.ShowDialog();
        }

        public static void RunTranslator(bool appRun = false)
        {
            var form = new Translator();

            if (appRun)
                Application.Run(form);
            else
                form.ShowDialog();
        }
    }
}