using System;
using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids.Library
{
    [Serializable]
    public class LibraryDisplay : ColumnAdapter
    {
        public LibraryDisplay()
        {
            DisplayColumns = new List<string>
            {
                "title", "type"
            };

            DisplayCaptions = new List<string>
            {
                "Title", "Type"
            };
        }
    }
}