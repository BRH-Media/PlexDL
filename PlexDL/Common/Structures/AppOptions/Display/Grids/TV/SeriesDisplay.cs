using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids.TV
{
    public class SeriesDisplay : ColumnAdapter
    {
        public SeriesDisplay()
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