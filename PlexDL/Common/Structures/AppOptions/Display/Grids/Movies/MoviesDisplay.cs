using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids.Movies
{
    public class MoviesDisplay : ColumnAdapter
    {
        public MoviesDisplay()
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