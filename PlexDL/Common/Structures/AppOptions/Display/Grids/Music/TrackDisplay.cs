using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids.Music
{
    public class TrackDisplay : ColumnAdapter
    {
        public TrackDisplay()
        {
            DisplayColumns = new List<string>
            {
                "title", "studio", "year", "contentRating"
            };

            DisplayCaptions = new List<string>
            {
                "Title", "Studio", "Year", "Rating"
            };
        }
    }
}