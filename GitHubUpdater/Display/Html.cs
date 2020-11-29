using GitHubUpdater.Properties;
using System.IO;

namespace GitHubUpdater.Display
{
    public class Html
    {
        public string HtmlText { get; set; }
        public bool FromFile { get; set; } = true;
        public bool ToFile { get; set; } = true;
        private string DefaultHtmlFile { get; } = $@"{Globals.UpdateRootDir}\update.htm";
        private string DefaultHtml { get; } = Resources.updateFormHtmlDocument;

        public Html(string file = @"", string html = @"")
        {
            var htmlFile = file;
            var htmlText = html;
            if (string.IsNullOrWhiteSpace(htmlText))
                htmlText = DefaultHtml;
            if (string.IsNullOrWhiteSpace(htmlFile))
                htmlFile = DefaultHtmlFile;
            if (FromFile)
            {
                if (File.Exists(htmlFile))
                {
                    htmlText = File.ReadAllText(htmlFile);
                }
                else
                {
                    if (ToFile)
                    {
                        File.WriteAllText(htmlFile, htmlText);
                    }
                }
            }
            HtmlText = htmlText;
        }
    }
}