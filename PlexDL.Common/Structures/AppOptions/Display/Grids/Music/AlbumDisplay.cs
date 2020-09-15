using System;
using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids.Music
{
    [Serializable]
    public class AlbumDisplay : ColumnAdapter
    {
        public AlbumDisplay()
        {
            DisplayColumns = new List<string>
            {
                "title", "studio", "year"
            };

            DisplayCaptions = new List<string>
            {
                "Album", "Record Label", "Year"
            };
        }
    }
}