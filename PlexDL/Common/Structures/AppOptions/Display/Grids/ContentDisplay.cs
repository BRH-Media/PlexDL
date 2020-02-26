using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids
{
    public class ContentDisplay
    {
        public List<string> ContentDisplayColumns { get; set; } = new List<string>() { "title", "studio", "year", "contentRating" };
        public List<string> ContentDisplayCaption { get; set; } = new List<string>() { "Title", "Studio", "Year", "Rating" };
    }
}