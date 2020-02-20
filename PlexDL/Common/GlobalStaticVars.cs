using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroSet_UI;
using PlexDL.UI;

namespace PlexDL.Common
{
    public class GlobalStaticVars
    {
        public static StyleManager GlobalStyle = new StyleManager()
        {
            CustomTheme = "C:\\Users\\baele\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml",
            Style = MetroSet_UI.Design.Style.Light,
            ThemeAuthor = null,
            ThemeName = null
        };
    }
}
