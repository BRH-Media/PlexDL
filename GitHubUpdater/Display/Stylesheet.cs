using GitHubUpdater.Properties;
using System.IO;

namespace GitHubUpdater.Display
{
    public class Stylesheet
    {
        public string CssText { get; set; }
        public bool FromFile { get; set; } = true;
        public bool ToFile { get; set; } = true;
        private string DefaultCssFile { get; } = $@"{Globals.UpdateRootDir}\style.css";
        private string DefaultStyle { get; } = Resources.updateFormHtmlStyle;

        public Stylesheet(string file = @"", string css = @"")
        {
            var cssFile = file;
            var cssText = css;
            if (string.IsNullOrWhiteSpace(cssText))
                cssText = DefaultStyle;
            if (string.IsNullOrWhiteSpace(cssFile))
                cssFile = DefaultCssFile;
            if (FromFile)
            {
                if (File.Exists(cssFile))
                {
                    cssText = File.ReadAllText(cssFile);
                }
                else
                {
                    if (ToFile)
                    {
                        File.WriteAllText(cssFile, cssText);
                    }
                }
            }
            CssText = cssText;
        }
    }
}