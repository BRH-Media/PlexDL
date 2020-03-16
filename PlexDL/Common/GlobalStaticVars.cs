using MetroSet_UI;

namespace PlexDL.Common
{
    public static class GlobalStaticVars
    {
        public static StyleManager GlobalStyle { get; set; }  = new StyleManager()
        {
            CustomTheme = "C:\\Users\\baele\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml",
            Style = MetroSet_UI.Design.Style.Light,
            ThemeAuthor = null,
            ThemeName = null
        };
    }
}