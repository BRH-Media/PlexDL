using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids
{
    public class ContentDisplay : ColumnAdapter
    {
        public ContentDisplay()
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