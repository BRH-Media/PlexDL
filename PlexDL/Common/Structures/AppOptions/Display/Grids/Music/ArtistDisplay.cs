using System;
using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids.Music
{
    [Serializable]
    public class ArtistDisplay : ColumnAdapter
    {
        public ArtistDisplay()
        {
            DisplayColumns = new List<string>
            {
                "title"
            };

            DisplayCaptions = new List<string>
            {
                "Artist"
            };
        }
    }
}