using System.IO;
using GitHubUpdater.Properties;

namespace GitHubUpdater
{
    public class Stylesheet
    {
        public string CssText { get; set; } = @"";
        private string DefaultCssFile { get; } = $@"{Globals.UpdateRootDir}\style.css";
        private string DefaultStyle { get; } = Resources.updateFormHtmlStyle;

        public Stylesheet(string cssFile, string cssText = @"")
        {
            if (string.IsNullOrWhiteSpace(cssText))
                cssText = DefaultStyle;
            
        }

        public Stylesheet()
        {
            var cssFile = DefaultCssFile;
            var cssText = DefaultStyle;
        }
    }
}