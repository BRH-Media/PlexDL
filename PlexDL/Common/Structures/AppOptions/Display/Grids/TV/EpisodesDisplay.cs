using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids.TV
{
    public class EpisodesDisplay : ColumnAdapter
    {
        public EpisodesDisplay()
        {
            DisplayColumns = new List<string>
            {
                "title", "year", "contentRating"
            };

            DisplayCaptions = new List<string>
            {
                "Title", "Year", "Rating"
            };
        }
    }
}