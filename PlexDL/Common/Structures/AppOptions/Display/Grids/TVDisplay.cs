using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids
{
    public class TVDisplay
    {
        public List<string> TVDisplayColumns { get; set; } = new List<string>() { "title", "studio", "year", "contentRating" };
        public List<string> TVDisplayCaption { get; set; } = new List<string>() { "Title", "Studio", "Year", "Rating" };
    }
}